#region using

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Buffers.ArrayPool<System.Numerics.Complex>;
using static System.Numerics.Complex;
using DMat = MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix;
using DVec = MathNet.Numerics.LinearAlgebra.Complex.DenseVector;
#endregion

namespace Crystallography;

/// <summary>
/// Bethe法による動力学計算を提供するクラス。すべて、単位はnm
/// </summary>
[Serializable]
public class BetheMethod
{
    #region static readonly field
    private static readonly Complex One = Complex.One;
    private const double TwoPi = 2 * Math.PI;
    private static readonly Complex TwoPiI = TwoPi * ImaginaryOne;
    private static readonly Complex PiI = Math.PI * ImaginaryOne;
    private const double PiSq = Math.PI * Math.PI;
    /// <summary>
    /// (001)ベクトル
    /// </summary>
    private static readonly Vector3DBase zNorm = new(0, 0, 1);
    public static readonly bool EigenEnabled, MklEnabled, BlasEnabled, CudaEnabled;

    public static readonly int ProcessorCount = Environment.ProcessorCount;
    #endregion

    #region フィールド、プロパティ

    private double AccVoltage { get; set; }
    private Crystal Crystal { get; }
    /// <summary>
    /// 結晶の方位
    /// </summary>
    private Matrix3D BaseRotation { get; set; } = null;
    public double AlphaMax { get; set; }
    public double Cs { get; set; }
    public double Defocus { get; set; }
    public Vector3DBase[] BeamDirections { get; set; }
    public int RotationArrayValidLength { get; set; } = 0;

    /// <summary>
    /// サンプル表面(から内部への)の法線単位ベクトル. ReciProの座標系は、画面右が+X、上が+Y,手前が+Zなので、初期値は(0,0,-1)
    /// </summary>
    public Vector3D Surface { get; set; } = new Vector3D(0, 0, -1);
    public int MaxNumOfBloch { get; set; }
    public double Thickness { get; set; }
    public double[] Thicknesses { get; set; }
    public enum Solver { Eigen_MKL, Eigen_Eigen, MtxExp_MKL, MtxExp_Eigen, Auto }

    public Complex[] EigenValues { get; set; }
    public Complex[] EigenVectors { get; set; }
    public Complex[] EigenVectorsInverse { get; set; }

    public DVec[] EigenValuesPED { get; set; }
    public DMat[] EigenVectorsPED { get; set; }
    public DMat[] EigenVectorsInversePED { get; set; }

    [NonSerialized]
    public Beam[][] BeamsPED;
    public double SemianglePED { get; set; }

    public bool IsBusy => bwCBED == null || bwCBED.IsBusy;

    /// <summary>
    /// CBEDのディスク情報 Disks[Z_index][G_index]
    /// </summary>
    [XmlIgnore]
    public CBED_Disk[][] Disks { get; set; }

    [NonSerialized]
    public Beam[] Beams;

    [NonSerialized]
    private readonly BackgroundWorker bwCBED = new();
    public event ProgressChangedEventHandler CbedProgressChanged;
    public event RunWorkerCompletedEventHandler CbedCompleted;

    [NonSerialized]
    private readonly BackgroundWorker bwSTEM = new();
    public event ProgressChangedEventHandler StemProgressChanged;
    public event RunWorkerCompletedEventHandler StemCompleted;

    private readonly object lockObj1 = new();
    private readonly object lockObj2 = new();


    public double[][][] STEM_Image;

    #endregion

    #region コンストラクタ

    static BetheMethod()
    {
        EigenEnabled = NativeWrapper.Enabled;
        BlasEnabled = Control.TryUseNativeOpenBLAS();
        MklEnabled = Control.TryUseNativeMKL();
        CudaEnabled = Control.TryUseNativeCUDA();
    }
    public BetheMethod(Crystal crystal)
    {
        Crystal = crystal;

        bwCBED = new BackgroundWorker
        {
            WorkerSupportsCancellation = true,
            WorkerReportsProgress = true
        };
        bwCBED.RunWorkerCompleted += Cbed_RunWorkerCompleted;
        bwCBED.ProgressChanged += Cbed_ProgressChanged;
        bwCBED.DoWork += cbed_DoWork;

        bwSTEM = new BackgroundWorker
        {
            WorkerSupportsCancellation = true,
            WorkerReportsProgress = true,
        };
        bwSTEM.RunWorkerCompleted += Stem_RunWorkerCompleted;
        bwSTEM.ProgressChanged += Stem_ProgressChanged;
        bwSTEM.DoWork += stem_DoWork;
    }
    #endregion

    #region CBED
    private void Cbed_ProgressChanged(object sender, ProgressChangedEventArgs e) => CbedProgressChanged?.Invoke(sender, e);

    private void Cbed_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => CbedCompleted?.Invoke(sender, e);

    public void CancelCBED()
    {
        if (bwCBED.IsBusy)
            bwCBED.CancelAsync();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="maxNumOfBloch"></param>
    /// <param name="voltage">加速電圧(kV)</param>
    /// <param name="rotation">基準となる方位</param>
    /// <param name="thickness">厚みの配列</param>
    /// <param name="beamRotations">基準となる方位に乗算する方位配列</param>
    public void RunCBED(int maxNumOfBloch, double voltage, Matrix3D rotation, double[] thickness, Vector3DBase[] beamDirections, Solver solver = Solver.Auto, int thread = 1)
    {
        MaxNumOfBloch = maxNumOfBloch;
        AccVoltage = voltage;
        //Wavelength = UniversalConstants.Convert.EnergyToElectronWaveLength(voltage);
        BaseRotation = new Matrix3D(rotation);
        BeamDirections = beamDirections;
        Thicknesses = thickness;

        //var cuda = Control.TryUseNativeCUDA();
        bwCBED.RunWorkerAsync((solver, thread));
    }

    /// <summary>
    /// CBED計算
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private unsafe void cbed_DoWork(object sender, DoWorkEventArgs e)
    {
        var (solver, thread) = ((Solver, int))e.Argument;

        //波数を計算
        var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(AccVoltage);
        //U0を計算
        var u0 = getU(AccVoltage).Real.Real;
        //k0ベクトルを計算
        var vecK0 = getVecK0(kvac, u0);
        //計算対象のg-Vectorsを決める。indexが小さく、かつsg(励起誤差)の小さいg-vectorを抽出する
        Beams = Find_gVectors(BaseRotation, vecK0);

        //入射面での波動関数を定義
        var psi0 = new DVec(Enumerable.Range(0, Beams.Length).ToList().Select(g => g == 0 ? One : 0).ToArray());
        //ポテンシャルマトリックスを初期化
        uDictionary.Clear();
        var potentialMatrix = getPotentialMatrix(Beams);
        //有効なRotationだけを選択
        int width = (int)Math.Sqrt(BeamDirections.Length);
        double radius = width / 2.0;
        bool inside(int i) => (i % width - radius + 0.5) * (i % width - radius + 0.5) + (i / width - radius + 0.5) * (i / width - radius + 0.5) <= radius * radius;
        var beamDirectionsValid = BeamDirections.Where((rot, i) => inside(i)).ToList();

        RotationArrayValidLength = beamDirectionsValid.Count;

        //進捗状況報告用の各種定数を初期化
        int count = 0;

        #region solver, thread の設定
        if (solver == Solver.Auto || (!EigenEnabled && (solver == Solver.Eigen_Eigen || solver == Solver.MtxExp_Eigen)))
        {
            if (EigenEnabled)
                (solver, thread) = (Solver.MtxExp_Eigen, ProcessorCount);
            else
                (solver, thread) = (Solver.Eigen_MKL, MklEnabled ? Math.Max(1, ProcessorCount / 4) : ProcessorCount);
        }
        var reportString = $"{solver}{thread}";
        #endregion

        int bLen = Beams.Length, tLen = Thicknesses.Length;
        var beamDirectionsP = beamDirectionsValid.AsParallel().WithDegreeOfParallelism(thread);

        //ここからdiskValid[t*tLen +g]を計算.
        var diskAmplitudeValid = beamDirectionsP.Select(beamDirection =>
        {
            if (bwCBED.CancellationPending) return null;
            //var rotZ = beamDirection * zNorm;
            var coeff = Math.Abs(1.0 / beamDirection.Z); // = 1/cosTau

            var vecK0 = getVecK0(kvac, u0, beamDirection);

            var eigenMatrix = Shared.Rent(bLen * bLen);
            var beams = ArrayPool<Beam>.Shared.Rent(bLen);
            try
            {
                reset_gVectors(bLen, Beams, BaseRotation, vecK0, ref beams);//BeamsのPやQをリセット
                getEigenMatrix(bLen, beams, ref eigenMatrix, potentialMatrix);//ポテンシャル行列をセット //コスト高い
                Complex[] result;

                //ポテンシャル行列の固有値、固有ベクトルを取得し、resultに格納
                //Eigen＿Eigenの場合
                if (solver == Solver.Eigen_Eigen && EigenEnabled)
                    result = NativeWrapper.CBEDSolver_Eigen(eigenMatrix, psi0.Values, Thicknesses, coeff);
                //Eigen_MKL あるいは Eigen_Managedの場合    
                else if (solver == Solver.Eigen_MKL)
                {
                    var evd = new DMat(bLen, bLen, eigenMatrix.AsSpan()[..(bLen * bLen)].ToArray()).Evd(Symmetricity.Asymmetric);
                    var alpha = evd.EigenVectors.LU().Solve(psi0);

                    var resultMat = new DMat(bLen, tLen);
                    for (int t = 0; t < tLen; t++)
                    {
                        //ガンマの対称行列×アルファを作成
                        var gammmaAlpha = new DVec(evd.EigenValues.Select((ev, i) => Exp(TwoPiI * ev * Thicknesses[t] * coeff) * alpha[i]).ToArray());
                        //深さtにおけるψを求める
                        resultMat.SetColumn(t, evd.EigenVectors.Multiply(gammmaAlpha));
                    }
                    result = resultMat.Values;
                }
                //MtxExp_Eigenの場合
                else if (solver == Solver.MtxExp_Eigen && EigenEnabled)
                    result = NativeWrapper.CBEDSolver_MatExp(eigenMatrix, psi0.Values, Thicknesses, coeff);
                //MtxExp_MKLの場合 
                else
                {
                    var resultMat = new DMat(bLen, tLen);
                    var matExp = (DMat)(TwoPiI * coeff * Thicknesses[0] * new DMat(bLen, bLen, eigenMatrix.AsSpan()[..(bLen * bLen)].ToArray())).Exponential();
                    var vec = matExp.Multiply(psi0);
                    resultMat.SetColumn(0, vec);

                    if (tLen > 1)
                    {
                        if (Thicknesses[1] - Thicknesses[0] == Thicknesses[0])
                            matExp = (DMat)(TwoPiI * coeff * (Thicknesses[1] - Thicknesses[0]) * new DMat(bLen, bLen, eigenMatrix.AsSpan()[..(bLen * bLen)].ToArray())).Exponential();
                        for (int t = 1; t < tLen; t++)
                        {
                            vec = (DVec)matExp.Multiply(vec);
                            resultMat.SetColumn(t, vec);
                        }
                    }
                    result = resultMat.Values;
                }
                //出射面での境界条件を考慮した位相にするため、以下のように変更 (20220803)
                for (int t = 0; t < tLen; t++)
                    for (int b = 0; b < bLen; b++)
                        result[t * bLen + b] *= Exp(PiI * (beams[b].P - 2 * kvac * Surface.Z) * Thicknesses[t]);
                Interlocked.Increment(ref count);
                if (count % 10 == 0) bwCBED.ReportProgress(count, reportString);//進捗状況を報告
                return result;
            }
            finally { Shared.Return(eigenMatrix); ArrayPool<Beam>.Shared.Return(beams); }
        }).ToArray();

        //無効なRotationも再び組み込んでdisk[RotationIndex][Z_index][G_index]を構築
        var diskAmplitude = new List<Complex[]>();
        for (int i = 0, j = 0; i < BeamDirections.Length; i++)
            diskAmplitude.Add(inside(i) ? diskAmplitudeValid[j++] : null);//有効(円内)のピクセルを追加し、無効なものにはnull

        count = 0;
        bwCBED.ReportProgress(0, "Compiling disks");
        //diskをコンパイルする
        Disks = new CBED_Disk[Thicknesses.Length][];
        Parallel.For(0, Thicknesses.Length, t =>
        {
            Disks[t] = new CBED_Disk[Beams.Length];
            for (int g = 0; g < Beams.Length; g++)
            {
                var amplitudes = new Complex[BeamDirections.Length];
                for (int r = 0; r < BeamDirections.Length; r++)
                    if (diskAmplitude[r] != null)
                        amplitudes[r] = diskAmplitude[r][t * bLen + g];

                Disks[t][g] = new CBED_Disk(new[] { Beams[g].H, Beams[g].K, Beams[g].L }, Beams[g].Vec, Thicknesses[t], amplitudes);
            }
        });

        //ここから、diskの重なり合いを計算

        //まず、各ディスクを構成するピクセルの座標を計算
        var diskTemp = new (RectangleD Rect, PointD[] Pos)[Beams.Length];
        Parallel.For(0, Beams.Length, g =>
        //for(int g = 0; g < Beams.Length; g++)
            {
                if (!bwCBED.CancellationPending)
                {
                    var pos = new PointD[BeamDirections.Length];
                    for (int r = 0; r < pos.Length; r++)
                    {
                        //Ewald球中心(試料)から見た、逆格子ベクトルの方向
                        var vec = kvac * BeamDirections[r] + Disks[0][g].G;
                        //var vec = BeamDirections[r] * (new Vector3DBase(0, 0, kvac) - Disks[0][g].G);
                        //var vec = BeamDirections[r] - Disks[0][g].G;
                        pos[r] = new PointD(vec.X / vec.Z, vec.Y / vec.Z); //カメラ長 1 を想定した検出器上のピクセルの座標値を格納
                    }
                    diskTemp[g] = (new RectangleD(new PointD(pos.Min(p => p.X), pos.Min(p => p.Y)), new PointD(pos.Max(p => p.X), pos.Max(p => p.Y))), pos);
                }
            });

        //g1のディスク中のピクセル(r1)に対して、他のディスク(g2)の重なるピクセル(r2)を足し合わせていく。
        Parallel.For(0, Beams.Length, g1 =>
        //for(int g1=0; g1<Beams.Length;g1++)
        {
            if (!bwCBED.CancellationPending)
            {
                var intensities = new double[Thicknesses.Length][];
                for (int t = 0; t < Thicknesses.Length; t++)
                    intensities[t] = Disks[t][g1].RawAmplitudes.Select(a => a.MagnitudeSquared()).ToArray();

                for (int r1 = 0; r1 < BeamDirections.Length; r1++)
                {
                    if (Disks[0][g1].RawAmplitudes[r1] != 0)
                    {
                        var pos = diskTemp[g1].Pos[r1];
                        for (int g2 = 0; g2 < Beams.Length; g2++)
                            if (g2 != g1 && diskTemp[g2].Rect.IsInsde(pos))
                            {
                                var r2 = getIndex(pos, diskTemp[g2].Pos, width);
                                if (r2 >= 0 && Disks[0][g2].RawAmplitudes[r2] != 0)
                                    for (int t = 0; t < Thicknesses.Length; t++)
                                        intensities[t][r1] += Disks[t][g2].RawAmplitudes[r2].MagnitudeSquared();
                            }
                    }
                }

                for (int t = 0; t < Thicknesses.Length; t++)
                    Disks[t][g1].Amplitudes = intensities[t].Select(intensity => new Complex(Math.Sqrt(intensity), 0)).ToArray();
            }
            bwCBED.ReportProgress(Interlocked.Increment(ref count) * 1000 / Beams.Length, "Compiling disks");//進捗状況を報告
        });

        if (bwCBED.CancellationPending)
            e.Cancel = true;
    }

    /// <summary>
    /// EBSD計算用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void cbed_DoWork2(object sender, DoWorkEventArgs e)
    {
        var (solver, thread, cs) = ((Solver, int, double))e.Argument;

        //波数を計算
        var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(AccVoltage);
        //U0を計算
        var u0 = getU(AccVoltage).Real.Real;
        int width = (int)Math.Sqrt(BeamDirections.Length);
        double radius = width / 2.0;
        bool inside(int i) => (i % width - radius + 0.5) * (i % width - radius + 0.5) + (i / width - radius + 0.5) * (i / width - radius + 0.5) <= radius * radius;
        //var beamRotationsValid = BeamRotations.Where((rot, i) => inside(i)).ToList();

        //RotationArrayValidLength = beamRotationsValid.Count;
        gDic.Clear();
        //進捗状況報告用の各種定数を初期化
        int count = 0;

        #region solver, thread の設定
        if (solver == Solver.Auto || (!EigenEnabled && (solver == Solver.Eigen_Eigen || solver == Solver.MtxExp_Eigen)))
        {
            if (EigenEnabled)
                (solver, thread) = (Solver.MtxExp_Eigen, ProcessorCount);
            else
                (solver, thread) = (Solver.Eigen_MKL, MklEnabled ? Math.Max(1, ProcessorCount / 4) : ProcessorCount);
        }
        var reportString = $"{solver}{thread}";
        #endregion

        var beamDirectionsP = BeamDirections.AsParallel().WithDegreeOfParallelism(thread);

        //diskAmplitude[r][t][g]
        var diskAmplitude = beamDirectionsP.Select((beamDirection, i) =>
        {
            if (!inside(i)) return (null, null);

            if (bwCBED.CancellationPending) return (null, null);
            //var rotZ = beamDirection * zNorm;
            //var coeff = 1.0 / rotZ.Z; // = 1/cosTau
            var coeff = Math.Abs(1.0 / beamDirection.Z); // = 1/cosTau

            var vecK0 = getVecK0(kvac, u0, beamDirection);

            var beams = Find_gVectors(BaseRotation, vecK0, MaxNumOfBloch, true);
            var potentialMatrix = getEigenMatrix(beams);
            var len = beams.Length;
            //入射面での波動関数を定義
            var psi0 = new DVec(Enumerable.Range(0, len).ToList().Select(g => g == 0 ? One : 0).ToArray());

            Complex[] result;

            //ポテンシャル行列の固有値、固有ベクトルを取得し、resultに格納
            #region 各ソルバーによる計算
            //Eigen＿Eigenの場合
            if (solver == Solver.Eigen_Eigen && EigenEnabled)
                result = NativeWrapper.CBEDSolver_Eigen(potentialMatrix, psi0.ToArray(), Thicknesses, coeff);
            //Eigen_MKL あるいは Eigen_Managedの場合    
            else if (solver == Solver.Eigen_MKL)
            {
                var evd = new DMat(len, len, potentialMatrix).Evd(Symmetricity.Asymmetric);
                var alpha = evd.EigenVectors.LU().Solve(psi0);
                var resultMat = new DMat(len, Thicknesses.Length);
                for (int t = 0; t < Thicknesses.Length; t++)
                {
                    //ガンマの対称行列×アルファを作成
                    var gammmaAlpha = new DVec(evd.EigenValues.Select((ev, i) => Exp(TwoPiI * ev * t * coeff) * alpha[i]).ToArray());
                    //深さtにおけるψを求める
                    resultMat.SetColumn(t, evd.EigenVectors.Multiply(gammmaAlpha));
                }
                result = resultMat.Values;
            }
            //MtxExp_Eigenの場合
            else if (solver == Solver.MtxExp_Eigen && EigenEnabled)
                result = NativeWrapper.CBEDSolver_MatExp(potentialMatrix, psi0.ToArray(), Thicknesses, coeff);
            //MtxExp_MKLの場合 
            else
            {
                var resultMat = new DMat(len, Thicknesses.Length);
                var matExp = (DMat)(TwoPiI * coeff * Thicknesses[0] * new DMat(len, len, potentialMatrix)).Exponential();
                var vec = matExp.Multiply(psi0);
                resultMat.SetColumn(0, vec);

                if (Thicknesses.Length > 1)
                {
                    if (Thicknesses[1] - Thicknesses[0] == Thicknesses[0])
                        matExp = (DMat)(TwoPiI * coeff * (Thicknesses[1] - Thicknesses[0]) * new DMat(len, len, potentialMatrix)).Exponential();
                    for (int t = 1; t < Thicknesses.Length; t++)
                    {
                        vec = (DVec)matExp.Multiply(vec);
                        resultMat.SetColumn(t, vec);
                    }
                }
                result = resultMat.Values;
            }
            //出射面での境界条件を考慮した位相にするため、以下のように変更 (20220803)
            for (int t = 0; t < Thicknesses.Length; t++)
                for (int b = 0; b < beams.Length; b++)
                    result[t * beams.Length + b] *= Exp(PiI * (beams[b].P - 2 * kvac * Surface.Z) * Thicknesses[t]);
            #endregion

            bwCBED.ReportProgress(Interlocked.Increment(ref count), reportString);//進捗状況を報告
            return (result, beams);
        }).ToArray();

        count = 0;
        bwCBED.ReportProgress(0, "Compiling disks");

        var directDiskIntensities = new double[Thicknesses.Length][];
        for (int t = 0; t < Thicknesses.Length; t++)
        {
            directDiskIntensities[t] = new double[BeamDirections.Length];
            for (int r = 0; r < directDiskIntensities[t].Length; r++)
                if (diskAmplitude[r].result != null)
                    directDiskIntensities[t][r] = diskAmplitude[r].result[t * diskAmplitude[r].beams.Length + 0].MagnitudeSquared();
        }

        var directDiskPositions = new PointD[BeamDirections.Length];
        for (int r = 0; r < BeamDirections.Length; r++)
        {
            //var vec = BeamDirections[r] * new Vector3DBase(0, 0, kvac);//Ewald球中心(試料)から見た、逆格子ベクトルの方向
            var vec = kvac * BeamDirections[r];//Ewald球中心(試料)から見た、逆格子ベクトルの方向
            directDiskPositions[r] = new PointD(vec.X / vec.Z, vec.Y / vec.Z); //カメラ長 1 を想定した検出器上のピクセルの座標値を格納
        }
        double xMax = directDiskPositions.Max(p => p.X), xMin = directDiskPositions.Min(p => p.X);
        double yMax = directDiskPositions.Max(p => p.Y), yMin = directDiskPositions.Min(p => p.Y);

        Parallel.For(0, BeamDirections.Length, r1 =>
        {
            if (diskAmplitude[r1].result != null)
            {
                for (int g = 1; g < diskAmplitude[r1].beams.Length; g++)
                {
                    //var vec = BeamDirections[r1] * (new Vector3DBase(0, 0, kvac) - diskAmplitude[r1].beams[g].Vec);//Ewald球中心(試料)から見た、逆格子ベクトルの方向
                    var vec = kvac * BeamDirections[r1] - diskAmplitude[r1].beams[g].Vec;//Ewald球中心(試料)から見た、逆格子ベクトルの方向
                    double posX = vec.X / vec.Z, posY = vec.Y / vec.Z; //カメラ長 1 を想定した検出器上のピクセルの座標値を格納
                    if (posX < xMax && posX > xMin && posY < yMax && posY > yMin)
                    {
                        var r2 = getIndex(new PointD(posX, posY), directDiskPositions, width);
                        if (r2 >= 0 && directDiskIntensities[0][r2] != 0)
                            lock (lockObj1)
                                for (int t = 0; t < Thicknesses.Length; t++)
                                    directDiskIntensities[t][r2] += diskAmplitude[r1].result[t * diskAmplitude[r1].beams.Length + g].MagnitudeSquared();
                    }
                }
            }
            bwCBED.ReportProgress(Interlocked.Increment(ref count) * 1000 / BeamDirections.Length, "Compiling disks");
        });

        Disks = new CBED_Disk[Thicknesses.Length][];
        for (int t = 0; t < Thicknesses.Length; t++)
        {
            Disks[t] = new[] { new CBED_Disk(new[] { 0, 0, 0 }, new Vector3DBase(0,0,0), Thicknesses[t],
                    directDiskIntensities[t].Select(intensity => new Complex(Math.Sqrt(intensity), 0)).ToArray()) };
            Disks[t][0].Amplitudes = Disks[t][0].RawAmplitudes;
        }

        if (bwCBED.CancellationPending)
            e.Cancel = true;
    }

    //与えられたposに最も近いインデックスを返す
    static int getIndex(PointD pos, PointD[] posList, int width)
    {
        var w2 = width * width;
        int i = w2 / 2, j = i - 1;//中心から、縦横に検索
        double min = (pos - posList[i]).Length2, temp = min;

        while (i != j)
        {
            j = i;
            if (i + 1 < w2 && (temp = (pos - posList[i + 1]).Length2) < min)
                i++;
            else if (i - 1 >= 0 && (temp = (pos - posList[i - 1]).Length2) < min)
                i--;
            min = Math.Min(min, temp);

            if (i + width < w2 && (temp = (pos - posList[i + width]).Length2) < min)
                i += width;
            else if (i - width >= 0 && (temp = (pos - posList[i - width]).Length2) < min)
                i -= width;
            min = Math.Min(min, temp);
        }
        if (i / width == 0 || i / width == width - 1 || i % width == 0 || i % width == width - 1)
            return -1;
        else
            return i;
    }

    #endregion

    #region 平行ビーム電子回折

    /// <summary>
    /// 平行ビームの電子回折計算
    /// </summary>
    /// <param name="maxNumOfBloch"></param>
    /// <param name="voltage"></param>
    /// <param name="rotation"></param>
    /// <param name="thickness"></param>
    /// <returns></returns>
    public Beam[] GetDifractedBeamAmpriltudes(int maxNumOfBloch, double voltage, Matrix3D rotation, double thickness)
    {
        if (AccVoltage != voltage)
            uDictionary.Clear();

        //波数を計算
        var k_vac = UniversalConstants.Convert.EnergyToElectronWaveNumber(voltage);
        //U0を計算
        var u0 = getU(voltage).Real.Real;
        var vecK0 = getVecK0(k_vac, u0);

        int dim;
        if (MaxNumOfBloch != maxNumOfBloch || AccVoltage != voltage || EigenValues == null || Beams.Length != EigenValues.Length || EigenVectors == null || !rotation.Equals(BaseRotation))
        {
            MaxNumOfBloch = maxNumOfBloch;
            AccVoltage = voltage;
            BaseRotation = new Matrix3D(rotation);
            Thickness = thickness;

            //uDictionary.Clear();
            //計算対象のg-Vectorsを決める。
            Beams = Find_gVectors(BaseRotation, vecK0);

            if (Beams == null || Beams.Length == 0) return Array.Empty<Beam>();

            var potentialMatrix = getEigenMatrix(Beams);
            dim = Beams.Length;
            //A行列に関する固有値、固有ベクトルを取得 
            if (EigenEnabled || maxNumOfBloch < 400)
            {
                (EigenValues, EigenVectors) = NativeWrapper.EigenSolver(dim, potentialMatrix);
                EigenVectorsInverse = NativeWrapper.Inverse(Beams.Length, EigenVectors);
            }
            else
            {
                var evd = new DMat(dim, dim, potentialMatrix).Evd(Symmetricity.Asymmetric);
                EigenValues = ((DVec)evd.EigenValues).Values;
                EigenVectors = ((DMat)evd.EigenVectors).Values;
                EigenVectorsInverse = ((DMat)evd.EigenVectors.Inverse()).Values;
            }
        }

        dim = Beams.Length;

        var psi0 = new DVec(new Complex[dim]) { [0] = 1 };//入射面での波動関数を定義

        var alpha = new DMat(dim, dim, EigenVectorsInverse).Multiply(psi0);//アルファベクトルを求める

        //ガンマの対称行列×アルファを作成
        var gamma_alpha = new DVec(Enumerable.Range(0, dim).Select(n => Exp(TwoPiI * EigenValues[n] * thickness) * alpha[n]).ToArray());

        //出射面での境界条件を考慮した位相にするため、以下の1行を追加 (20190827)
        var p = new DiagonalMatrix(dim, dim, Beams.Select(b => Exp(PiI * (b.P - 2 * k_vac * Surface.Z) * thickness)).ToArray());

        //深さZにおけるψを求める
        var psi_atZ = p.Multiply(new DMat(dim, dim, EigenVectors).Multiply(gamma_alpha));

        for (int i = 0; i < Beams.Length && i < dim; i++)
            Beams[i].Psi = psi_atZ[i];

        return Beams;
    }

    #endregion

    #region Precession electron diffraction

    /// <summary>
    /// PEDの計算
    /// </summary>
    /// <param name="maxNumOfBloch"></param>
    /// <param name="voltage"></param>
    /// <param name="baseRotation"></param>
    /// <param name="thickness"></param>
    /// <param name="semiangle"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    public Beam[] GetPrecessionElectronDiffraction(int maxNumOfBloch, double voltage, Matrix3D baseRotation, double thickness, double semiangle, int step)
    {
        //波数を計算
        var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(voltage);
        //U0を計算
        var u0 = getU(voltage).Real.Real;

        if (AccVoltage != voltage)
            uDictionary.Clear();

        var useEigen = EigenEnabled && maxNumOfBloch < 400;

        var stepP = Enumerable.Range(0, step).ToList().AsParallel().WithDegreeOfParallelism(useEigen ? Environment.ProcessorCount : Math.Max(1, Environment.ProcessorCount / 4));
        if (MaxNumOfBloch != maxNumOfBloch || AccVoltage != voltage || EigenValuesPED == null || EigenValuesPED.Length != step
            || EigenVectorsPED == null || EigenVectorsPED.Length != step || semiangle != SemianglePED || !baseRotation.Equals(BaseRotation))
        {
            MaxNumOfBloch = maxNumOfBloch;
            AccVoltage = voltage;
            BaseRotation = new Matrix3D(baseRotation);
            Thickness = thickness;
            SemianglePED = semiangle;

            EigenValuesPED = new DVec[step];
            EigenVectorsPED = new DMat[step];
            EigenVectorsInversePED = new DMat[step];
            BeamsPED = new Beam[step][];

            gDic.Clear();
            stepP.ForAll(k =>
               {
                   var rotAngle = 2.0 * Math.PI * k / step;
                   var beamRotation = Matrix3D.Rot(new Vector3DBase(Math.Cos(rotAngle), Math.Sin(rotAngle), 0), SemianglePED);
                   //計算対象のg-Vectorsを決める。
                   var potentialMatrix = Array.Empty<Complex>();
                   var vecK0 = getVecK0(kvac, u0, beamRotation * new Vector3D(0, 0, -1));
                   BeamsPED[k] = Find_gVectors(BaseRotation, vecK0, MaxNumOfBloch, true);
                   var len = BeamsPED[k].Length;
                   potentialMatrix = getEigenMatrix(BeamsPED[k]);
                   var dim = BeamsPED[k].Length;
                   //A行列に関する固有値、固有ベクトルを取得 
                   if (useEigen)
                   {//Eigenを使う場合
                       var (val, vec) = NativeWrapper.EigenSolver(dim, potentialMatrix);
                       (EigenValuesPED[k], EigenVectorsPED[k]) = (new DVec(val), new DMat(len, len, vec));
                       EigenVectorsInversePED[k] = new DMat(len, len, NativeWrapper.Inverse(len, EigenVectorsPED[k].Values));
                   }
                   else
                   {//MKLを使う場合
                       var evd = new DMat(dim, dim, potentialMatrix).Evd(Symmetricity.Asymmetric);
                       EigenValuesPED[k] = (DVec)evd.EigenValues;
                       EigenVectorsPED[k] = (DMat)evd.EigenVectors;
                       EigenVectorsInversePED[k] = (DMat)EigenVectorsPED[k].Inverse();
                   }
               });
        }

        //各方向でのbeamの振幅を求める
        stepP.ForAll(k =>
        {
            if (EigenValuesPED[k] != null)
            {
                var len = EigenValuesPED[k].Count;
                var psi0 = new DVec(new Complex[len]) { [0] = 1 };//入射面での波動関数を定義
                var alpha = EigenVectorsInversePED[k].Multiply(psi0);//アルファベクトルを求める
                                                                     //ガンマの対称行列×アルファを作成
                var gamma_alpha = new DVec(Enumerable.Range(0, len).Select(n => Exp(TwoPiI * EigenValuesPED[k][n] * thickness) * alpha[n]).ToArray());
                //深さZにおけるψを求める
                var psi_atZ = EigenVectorsPED[k].Multiply(gamma_alpha);
                for (int i = 0; i < BeamsPED[k].Length && i < len; i++)
                    BeamsPED[k][i].Psi = psi_atZ[i];
            }
        });

        //最後に全てのビームをまとめる
        var compiled = new Dictionary<(int h, int k, int l), Beam>();
        foreach (var beamsEach in BeamsPED.Where(beams => beams != null))
            foreach (var beam in beamsEach)
            {
                if (!compiled.ContainsKey(beam.Index))
                {
                    compiled.Add(beam.Index, beam);
                    compiled[beam.Index].intensity = beam.Psi.MagnitudeSquared() / step;
                }
                else
                    compiled[beam.Index].intensity += beam.Psi.MagnitudeSquared() / step;
            }

        //基準の方位でP,Q,Sなどを再セット
        var mat = BaseRotation * Crystal.MatrixInverse.Transpose();
        var beams = compiled.Values.ToList();
        for (int i = 0; i < beams.Count; i++)
        {
            var g = mat * beams[i].Index;
            var (Q, P) = getQP(g, kvac, u0);
            var psi = new Complex(Math.Sqrt(beams[i].intensity), 0);
            beams[i] = new Beam(beams[i].Index, g, (beams[i].Ureal, beams[i].Uimag), (Q, P)) { Psi = psi };
        }

        //並び替え
        beams.Sort((a, b) =>
        {
            var c = a.Rating - b.Rating;
            return (c > 0) ? 1 : (c < 0) ? -1 : 0;
        });
        Beams = beams.ToArray();
        return Beams;
    }

    #endregion

    #region STEM シミュレーション
    private void Stem_ProgressChanged(object sender, ProgressChangedEventArgs e) => StemProgressChanged?.Invoke(sender, e);

    private void Stem_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => StemCompleted?.Invoke(sender, e);

    public void CancelSTEM()
    {
        if (bwSTEM.IsBusy)
            bwSTEM.CancelAsync();
    }
    public void RunSTEM(int maxNumOfBloch, double voltage, double cs, Size imageSize, double resolution,
        Matrix3D baseRotation, double[] thickness, double[] defocusses,
        Vector3DBase[] beamDirections, double convergenceAngle, double detAngleInner, double detAngleOuter,
        bool calcElas, bool calcInel, Solver solver = Solver.Auto, int thread = 1)
    {
        //MaxNumOfBloch = maxNumOfBloch;
        MaxNumOfBloch = 10000;

        AccVoltage = voltage;
        //Wavelength = UniversalConstants.Convert.EnergyToElectronWaveLength(voltage);
        BaseRotation = new Matrix3D(baseRotation);
        BeamDirections = beamDirections;
        Thicknesses = thickness;
        if(!bwSTEM.IsBusy) 
            bwSTEM.RunWorkerAsync((solver, thread, cs, convergenceAngle, detAngleInner, detAngleOuter, defocusses, imageSize, resolution, calcElas, calcInel));
    }
    public void stem_DoWork(object sender, DoWorkEventArgs e)
    {
        //MathNetの行列の内部は、1列目の要素、2列目の要素、という順番で格納されている
        var (solver, thread, cs, convergenceAngle, detAngleInner, detAngleOuter, defocusses, imageSize, resolution, calcElas, calcInel)
            = ((Solver, int, double, double, double, double, double[], Size, double, bool, bool))e.Argument;

        var diameterPix = (int)Math.Sqrt(BeamDirections.Length);
        var radiusPix = diameterPix / 2.0;
        bool inside(int i) => (i % diameterPix - radiusPix + 0.5) * (i % diameterPix - radiusPix + 0.5) + (i / diameterPix - radiusPix + 0.5) * (i / diameterPix - radiusPix + 0.5) <= radiusPix * radiusPix;
        gDic.Clear();

        //波数を計算
        var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(AccVoltage);
        //U0を計算
        var u0 = getU(AccVoltage).Real.Real;
        //k0ベクトルを計算
        var vecK0 = getVecK0(kvac, u0);
        //計算対象のg-Vectorsを決める。indexが小さく、かつsg(励起誤差)の小さいg-vectorを抽出する
        Beams = Find_gVectors(BaseRotation, vecK0);

        #region 検証コード 30nm^-1 以上のビームは削除  25nm^-1 = 62.7mrad
        var _beam = new List<Beam>();
        foreach (var beam in Beams)
            if (Math.Abs(beam.Vec.Z) < 1.0E-10 && beam.Vec.X2Y2 < 25 * 25)
                _beam.Add(beam);
        Beams =_beam.ToArray();
        #endregion

        int dLen = defocusses.Length, tLen = Thicknesses.Length, bLen = Beams.Length;
        #region 検証用コード
        //uDictionary.Clear();

        //var temp1 = new (Complex real, Complex imag)[Beams.Length * Beams.Length];
        //int n = 0;
        //for (int i = 0; i < Beams.Length; i++)
        //    for (int j = 0; j < Beams.Length; j++)
        //        temp1[n++] = getU(AccVoltage, Beams[i] - Beams[j]);

        //uDictionary.Clear();
        //var temp2 = new (Complex real, Complex imag)[Beams.Length * Beams.Length];

        //double sum = 0;

        //n = 0;
        //for (int i = 0; i < Beams.Length; i++)
        //    for (int j = 0; j < Beams.Length; j++)
        //    {
        //        temp2[n++] = getU(AccVoltage, Beams[i] - Beams[j], null, 0, 1);
        //        sum += Math.Pow(temp2[n - 1].imag.Real - temp1[n - 1].imag.Real, 2);
        //    }
        #endregion

        //入射面での波動関数を定義
        var psi0 = Enumerable.Range(0, Beams.Length).ToList().Select(g => g == 0 ? One : 0).ToArray();
        //ポテンシャルマトリックスを初期化
        uDictionary.Clear();
        var potentialMatrix = getPotentialMatrix(Beams);

        #region solver, thread の設定
        if (solver == Solver.Auto || (!EigenEnabled && (solver == Solver.Eigen_Eigen || solver == Solver.MtxExp_Eigen)))
        {
            if (EigenEnabled)
                (solver, thread) = (Solver.MtxExp_Eigen, ProcessorCount);
            else
                (solver, thread) = (Solver.Eigen_MKL, MklEnabled ? Math.Max(1, ProcessorCount / 4) : ProcessorCount);
        }

        (solver, thread) = EigenEnabled && bLen < 512 ? (Solver.Eigen_Eigen, ProcessorCount) : (Solver.Eigen_MKL, Math.Max(1, ProcessorCount / 4));

        var reportString = $"{solver}{thread}";
        #endregion

        #region 固有値固有ベクトルの計算
        Complex[][] eVectors = new Complex[BeamDirections.Length][], eValues = new Complex[BeamDirections.Length][], αs = new Complex[BeamDirections.Length][];
        var k_vec = GC.AllocateUninitializedArray<Vector3DBase>(BeamDirections.Length); 
        //進捗状況報告用の各種定数を初期化
        int count = 0;
        //Transmission coefficient tc[k][t][g]
        var tc = BeamDirections.AsParallel().WithDegreeOfParallelism(thread).Select((beamDirection, i) =>
        {
            var coeff = 1;// Math.Abs(1.0 / beamDirection.Z); // = 1/cosTau

            var vecK0 = getVecK0(kvac, u0, beamDirection);
            k_vec[i] = vecK0;

            if (!inside(i) || bwSTEM.CancellationPending) return null;
            if (Interlocked.Increment(ref count) % 10 == 0) bwSTEM.ReportProgress(count, reportString);//進捗状況を報告

            var eigenMatrix = Shared.Rent(bLen * bLen);
            var beams =ArrayPool<Beam>.Shared.Rent(bLen);
            try
            {
                reset_gVectors(bLen, Beams, BaseRotation, vecK0, ref beams);
                getEigenMatrix(bLen, beams, ref eigenMatrix, potentialMatrix);

                //ポテンシャル行列の固有値、固有ベクトルを取得し、resultに格納
                Complex[] result;
                #region 各ソルバーによる計算
                //Eigen＿Eigenの場合
                if (solver == Solver.Eigen_Eigen && EigenEnabled)
                    (eValues[i], eVectors[i], αs[i], result) = NativeWrapper.CBEDSolver2(eigenMatrix, psi0, Thicknesses, coeff);
                //Eigen_MKL あるいは Eigen_Managedの場合    
                else
                {
                    var evd = new DMat(bLen, bLen, eigenMatrix).Evd(Symmetricity.Asymmetric);
                    (eValues[i], eVectors[i]) = ((evd.EigenValues as DVec).Values, (evd.EigenVectors as DMat).Values);
                    αs[i] = ((DVec)(evd.EigenVectors as DMat).LU().Solve(new DVec(psi0))).Values;// NativeWrapper.PartialPivLuSolve(bLen, eVectors[i], psi0);
                    var tg = new DMat(bLen, tLen);
                    for (int t = 0; t < tLen; t++)
                    {
                        var gammmaAlpha = new DVec(eValues[i].Select((ev, g) => Exp(TwoPiI * ev * Thicknesses[t] * coeff) * αs[i][g]).ToArray());//ガンマの対称行列×アルファを作成
                        tg.SetColumn(t, evd.EigenVectors * gammmaAlpha);//深さtにおけるψを求める
                    }
                    result = tg.Values;
                }
                #endregion

                //位相を考慮して、return
                var _tc = Thicknesses.Select((thickness,t) => new Complex[bLen]).ToArray();
                for (int t = 0; t < tLen; t++)
                    for (int g = 0; g < bLen; g++)
                        _tc[t][g] = result[t * bLen + g] * Exp(PiI * (beams[g].P + 2 * kvac * beamDirection.Z) * Thicknesses[t]);//かなり近い
                return _tc;
            }
            finally { Shared.Return(eigenMatrix); ArrayPool<Beam>.Shared.Return(beams); }

        }).ToArray();
        #endregion

        if (bwSTEM.CancellationPending) { e.Cancel = true; return; }

        #region I(Q)の計算

        var k_xy = k_vec.Select(e => e.ToPointD).ToArray();
        var k_z = k_vec.Select(e => e.Z).ToArray();

        #region レンズ収差関数 W
        double rambda = UniversalConstants.Convert.EnergyToElectronWaveLength(AccVoltage), rambda2 = rambda * rambda;
        //double W(in PointD k, in double defocus) => Math.PI * rambda * k.Length2 * (cs * rambda2 * k.Length2 / 2.0 + defocus);
        double W(in PointD q, in double defocus) => 0;
        #endregion

        #region 収束絞り関数 A
        double conv = Math.Sin(convergenceAngle) / rambda, conv2 = conv * conv;
        bool A(in PointD k) => k.Length2 <= conv2;
        #endregion

        #region 検出器関数 D
        double inner = Math.Sin(detAngleInner) / rambda, inner2 = inner * inner, outer = Math.Sin(detAngleOuter) / rambda, outer2 = outer * outer;
        bool D(in PointD k) => k.Length2 >= inner2 && k.Length2 <= outer2;
        #endregion

        #region qList, g_q_indexを作成
        //qList　計算対象のQを網羅 
        var mat = BaseRotation * Crystal.MatrixInverse.Transpose();
        var qList = Beams.AsParallel().SelectMany(e1 => Beams.Select(e2 => (e1 - e2).Index)).Distinct()
            .Select(e => new Beam(e, mat * e)).Where(e => k_xy.Any(e2 => A(e2) && A(e2 + e.Vec.ToPointD))).OrderBy(e => e.Vec.ToPointD.Length2).ToList();
        //if(qList.Count > Beams.Length)
        //    qList.RemoveRange(Beams.Length, qList.Count - Beams.Length);

        //g_q_index (あるq[m]に対して、g-qの反射は、Beams配列で何番目か)
        var g_qIndex = qList.Select(q => Beams.Select((g1, n) => (g: n, g_q: Array.FindIndex(Beams, g2 => g2.Index == (g1 - q).Index))).Where(e => e.g_q != -1).ToArray()).ToArray();
        #endregion

        #region U行列の計算 
        count = 0;
        var U = new Complex[qList.Count][];
        uDictionary.Clear();
        if (calcInel) //U行列を作成
            Parallel.For(0, qList.Count, m =>
            {
                bwSTEM.ReportProgress((int)(1000.0 * Interlocked.Increment(ref count) / qList.Count ) , "Calculating U matrix");//状況を報告
                if (bwSTEM.CancellationPending) { e.Cancel = true; return; }

                var gamma = 1 + UniversalConstants.e0 * AccVoltage * 1E3 / UniversalConstants.m0 / UniversalConstants.c2;
                var k0 = UniversalConstants.Convert.EnergyToElectronWaveNumber(AccVoltage);
                U[m] = new Complex[bLen * bLen];
                int k = 0;
                for (int i = 0; i < bLen; i++)
                    for (int j = 0; j < bLen; j++)
                        U[m][k++] = getU(AccVoltage, Beams[j] - Beams[i] + qList[m], null, detAngleInner, detAngleOuter).Imag ;//局所形式の場合
                        //U[m][k] = getU(AccVoltage, q , -Beams[j] + Beams[i], detAngleInner, detAngleOuter).Imag;//非局所形式の場合は、これでいいのか？大塚さんに要確認。
            });
        #endregion

        //有効なディスクを判定するフラグ
        var flag = tc.Select(e => e != null).ToArray();
        //最大のK値(計算したK0ベクトルの中で最もXY成分が大きいもの)を求める。収束角ではないことに注意(5%大きい)。
        var maxK = k_xy.Max(e => e.X);
        double coeff1 = radiusPix - 0.5, coeff2 = (radiusPix - 0.5) / maxK, coeff3 = (uint)(diameterPix - 1);
        count = 0;

        //I_Elas, I_Inelの準備
        Complex[,,] I_Elas = new Complex[qList.Count, tLen, dLen], I_Inel = new Complex[qList.Count, tLen, dLen];

        //必要な情報だけを追加してParallelにしたtcP
        var tcP = tc.AsParallel().Select((e, i) => (index: i, result: e, K: k_xy[i])).Where(e => e.result != null && A(e.K));
        //I_Elas, I_Inelの計算
        tcP.ForAll(_tc =>
        //foreach (var _disk2 in disk2)
        {
            if (bwSTEM.CancellationPending) return;
            if (Interlocked.Increment(ref count) % 10 == 0) bwSTEM.ReportProgress(count, "Calculating I(Q)");//状況を報告
            
            var (index, tc_k, K) = _tc;
            Complex[] c_k = eVectors[index], α_k = αs[index], λ_k = eValues[index];
            double kz_k = k_z[index];

            Complex[] TDS = Shared.Rent(bLen * bLen), c_kq = Shared.Rent(bLen * bLen), λ_kq = Shared.Rent(bLen), α_kq = Shared.Rent(bLen), exp_k = Shared.Rent(bLen), exp_kq = Shared.Rent(bLen);
            try
            {
                foreach (var (m, KQ) in qList.Select((b, i) => (m: i, KQ: K + b.Vec.ToPointD)).Where(e => A(e.KQ)))
                {
                    var lenz = defocusses.Select(d => Exp(-ImaginaryOne * W(K, d)) * Exp(ImaginaryOne * W(KQ, d))).ToArray();

                    #region K+Qベクトルに対応するインデックスと比率を計算
                    double dX = KQ.X * coeff2 + coeff1, dY = -KQ.Y * coeff2 + coeff1;//K+Q の X,Y座標(実数)
                    int x = (int)(Math.Floor(dX)), y = (int)(Math.Floor(dY));//左上近接のX,Y座標(整数)
                    int n0 = y * diameterPix + x, n1 = n0 + 1, n2 = n0 + diameterPix, n3 = n2 + 1;//それぞれのインデックス
                    double xx = dX - x, yy = dY - y;
                    double r0 = (1 - xx) * (1 - yy), r1 = xx * (1 - yy), r2 = (1 - xx) * yy, r3 = xx * yy;//比率を計算
                    #endregion

                    if ((uint)x < coeff3 && (uint)y < coeff3 && flag[n0] && flag[n1] && flag[n2] && flag[n3])//4つのインデックスが範囲内であることを判定
                    {
                        #region 弾性散乱を計算する場合
                        if (calcElas)
                        {
                            var i_Elas = new Complex[tLen];
                            foreach (var (g, g_q) in g_qIndex[m].Where(e => D(Beams[e.g].Vec.ToPointD + K)))
                                for (int t = 0; t < tLen; t++)
                                    i_Elas[t] += tc_k[t][g] * (r0 * tc[n0][t][g_q] + r1 * tc[n1][t][g_q] + r2 * tc[n2][t][g_q] + r3 * tc[n3][t][g_q]).Conjugate();

                            lock (lockObj1)
                                for (int t = 0; t < tLen; t++)
                                    for (int d = 0; d < dLen; d++)
                                        I_Elas[m, t, d] += i_Elas[t] * lenz[d];
                        }
                        #endregion

                        #region 非弾性を計算する場合
                        if (calcInel)
                        {
                            //C(K+Q)をブレンドし、C(K+Q)*^T × U(Q) × C(K)をTDSに格納　(ひとまとめにした関数も作ったが、別々にやった方が早い)
                            NativeWrapper.Blend(bLen * bLen, eVectors[n0], eVectors[n1], eVectors[n2], eVectors[n3], r0, r1, r2, r3, ref c_kq);
                            NativeWrapper.AdjointMul_Mul_Mul(bLen, c_kq, U[m], c_k, ref TDS);
                            //NativeWrapper.BlendAdjointMul_Mul_Mul(bLen, eVectors[n0], eVectors[n1], eVectors[n2], eVectors[n3], r0, r1, r2, r3, U[m], c_k, ref TDS);

                            //α(K+Q)*を作成
                            NativeWrapper.BlendAndConjugate(bLen, αs[n0], αs[n1], αs[n2], αs[n3], r0, r1, r2, r3, ref α_kq);

                            //λ(K+Q)*を作成
                            NativeWrapper.BlendAndConjugate(bLen, eValues[n0], eValues[n1], eValues[n2], eValues[n3], r0, r1, r2, r3, ref λ_kq);

                            //kz(K)とkz(K+Q)を作成
                            double kz_kq = r0 * k_z[n0] + r1 * k_z[n1] + r2 * k_z[n2] + r3 * k_z[n3];

                            //kqの変数にあらかじめ係数を演算しておく。kの方は再利用するのでまずい。
                            for (int i = 0; i < bLen; i++)
                                λ_kq[i] += (kz_kq - kz_k);//λ(K+Q)に[kz(K+Q)-kz(K)]をあらかじめ加えておく

                            //B行列の中身を計算し、アダマール積を取る //69波、qList制限、256解像度で 5-6secくらい
                            for (int t = 0; t < tLen; t++)
                            {
                                for (int j = 0; j < bLen; j++)
                                {
                                    //exp_k[j] = Exp(ImaginaryOne * λ_k[j] * Thicknesses[t]);
                                    //exp_kq[j] = Exp(-ImaginaryOne * λ_kq[j] * Thicknesses[t]);
                                    exp_k[j] = Exp(TwoPiI * λ_k[j] * Thicknesses[t]);
                                    exp_kq[j] = Exp(-TwoPiI * λ_kq[j] * Thicknesses[t]);
                                }
                                Complex temp = 0.0;
                                int l = 0;
                                for (int i = 0; i < bLen; i++)
                                    for (int j = 0; j < bLen; j++)
                                        temp += -ImaginaryOne * α_k[j] * α_kq[i] * (exp_k[j] * exp_kq[i] - 1) / (λ_k[j] - λ_kq[i]) * TDS[l++];//B行列は作らず、直接アダマール積を取る どちらが正しい?
                                lock (lockObj2)
                                    for (int d = 0; d < dLen; d++)
                                        I_Inel[m, t, d] += temp / kvac * lenz[d];
                            }
                        }
                        #endregion
                    }
                }
            }
            finally { Shared.Return(TDS); Shared.Return(c_kq); Shared.Return(λ_kq); Shared.Return(α_kq); Shared.Return(exp_k); Shared.Return(exp_kq); }
            
        });
        #endregion

        if (bwSTEM.CancellationPending) { e.Cancel = true; return; }

        //imagesを初期化
        int width = imageSize.Width, height = imageSize.Height;
        STEM_Image = Thicknesses.Select(e => defocusses.Select(e2 => new double[width * height]).ToArray()).ToArray();

        #region 各ピクセルの計算
        double cX = width / 2.0, cY = height / 2.0;
        var shift = (Crystal.RotationMatrix * (Crystal.A_Axis + Crystal.B_Axis + Crystal.C_Axis) / 2).ToPointD;
        Parallel.For(0, height, y =>
        {
            for (int x = 0; x < width; x++)
            {
                var rVec = new PointD(-resolution * (x - cX), -resolution * (height - y - 1 - cY)) + shift;
                for (int t = 0; t < tLen; t++)
                    for (int d = 0; d < dLen; d++)
                    {
                        Complex elas = new(), inel = new();
                        for (int m = 0; m < qList.Count; m++)
                        {
                            var tmp = Exp(qList[m].Vec.ToPointD * rVec * TwoPiI) / radiusPix / radiusPix;
                            elas += I_Elas[m, t, d] * tmp;
                            inel += I_Inel[m, t, d] * tmp;
                        }
                        STEM_Image[t][d][x + y * width] = elas.Magnitude + inel.Magnitude;
                    }
            }
        });
        #endregion

        return;
    }
    #endregion

    #region ポテンシャルイメージ

    public double[][] GetPotentialImage(IEnumerable<Beam> beams, Size size, double res, bool phase = true)
    {
        int width = size.Width, height = size.Height;
        //gList[gNUm]を全て計算
        var gList = beams.Select(b => (b.Ureal, b.Uimag, b.Vec.ToPointD)).ToList();
        //imagesを初期化

        var images = Enumerable.Range(0, 4).ToList().Select(d => new double[width * height]).ToArray();

        var shift = Crystal.RotationMatrix * (Crystal.A_Axis + Crystal.B_Axis + Crystal.C_Axis) / 2;
        double cX = width / 2.0, cY = height / 2.0;
        Parallel.For(0, width * height, n =>
        {
            //単位格子軸の0.5倍だけシフトさせておく
            var r = new PointD(-(n % width - cX) * res + shift.X, -(height - 1 - n / width - cY) * res + shift.Y);
            var sums = new Complex[2];
            foreach (var (uCry, uTher, vec) in gList)
            {
                var exp = Exp(vec * r * TwoPiI);
                sums[0] += uCry * exp;
                sums[1] += uTher * exp;
            }
            for (var i = 0; i < sums.Length; i++)
            {
                images[i * 2][n] = phase ? sums[i].Magnitude : sums[i].Real;
                images[i * 2 + 1][n] = phase ? sums[i].Phase : sums[i].Imaginary;
            }
        });
        return images;
    }
    #endregion

    #region HRTEM Simulation
    /// <summary>
    /// HRTEMイメージをシミュレーションする
    /// </summary>
    /// <param name="beams"></param>
    /// <param name="size">イメージのサイズ</param>
    /// <param name="res">イメージの解像度 (nm/pix)</param>
    /// <param name="cs">球面収差 (nm)</param>
    /// <param name="aper">対物絞りの半頂角 (mrad)</param>
    /// <param name="beta">Illumination semiangle (単位は rad)</param>
    /// <param name="delta">Cc * ΔV/V 単位は nm</param>
    /// <param name="defocus">配列で与える. 単位はnm</param>
    /// <param name="quasiMode">trueの時はQuasi-coherent mode, falseの時はTransmission cross coefficient </param>
    /// <returns>double[defocusNum][pixels]</returns>
    public double[][] GetHRTEMImage(Beam[] beams, Size size, double res, double cs, double beta, double delta, double[] defocusses,
                                   bool quasiMode = true, bool native = true)
    {
        int width = size.Width, height = size.Height;
        double rambda = UniversalConstants.Convert.EnergyToElectronWaveLength(AccVoltage), rambdaSq = rambda * rambda;
        double deltaSq = delta * delta, betaSq = beta * beta;
        var k = new PointD(0, 0);//入射ベクトルKのXY成分
        var defLen = defocusses.Length;

        //gList[gNUm]を全て計算
        var gList = new List<(Complex Psi, PointD Vec, Complex[] Lenz)>();
        if (quasiMode)//Quasi-coherent modeの時
            foreach (var g in beams)
            {
                var qSq = (k + g.Vec.ToPointD).Length2;
                gList.Add((g.Psi, k + g.Vec.ToPointD, defocusses.Select(defocus =>
                    Exp(-PiI * rambda * qSq * (cs * rambdaSq * qSq / 2.0 + defocus))//球面収差
                    * Math.Exp(-PiSq * betaSq * qSq * (defocus + rambdaSq * cs * qSq) * (defocus + rambdaSq * cs * qSq))//Ec 時間的インコヒーレンス
                    * Math.Exp(-PiSq * rambdaSq * deltaSq * qSq * qSq / 2)//Es 空間的インコヒーレンス
                     ).ToArray()));
            }

        else//Transmission cross coefficient modelの時
        {
            var vecDic = new Dictionary<(int H, int K, int L), PointD>();
            for (var gNum = 0; gNum < beams.Length; gNum++)
                for (var hNum = gNum; hNum < beams.Length; hNum++)
                {
                    Beam g = beams[gNum], h = beams[hNum];
                    PointD q1 = k + g.Vec.ToPointD, q2 = k + h.Vec.ToPointD;
                    double q1Sq = q1.Length2, q2Sq = q2.Length2;
                    //gNum==hNumの時は、g.Psi.Magnitude2() が画素に伝わるだけなので、最後に強度を0~2^16に規格化する場合は、あってもなくても関係ない
                    var psi = gNum == hNum ? g.Psi.MagnitudeSquared() : 2 * g.Psi * Conjugate(h.Psi);

                    //indexが同じものがあるかどうかを検索し、無い場合のみvecを計算する
                    var index = (g.H - h.H, g.K - h.K, g.L - h.L);
                    if (!vecDic.TryGetValue(index, out var vec))
                    {
                        vec = (g.Vec - h.Vec).ToPointD;
                        vecDic.Add(index, vec);
                    }

                    var lenz = defocusses.Select(defocus =>
                        Exp(-PiI * rambda * q1Sq * (cs * rambdaSq * q1Sq / 2.0 + defocus)) *  //Kai1
                        Exp(PiI * rambda * q2Sq * (cs * rambdaSq * q2Sq / 2.0 + defocus)) *  //kai2
                        Math.Exp(-PiSq * betaSq * (defocus * (q1 - q2) + rambdaSq * cs * (q1Sq * q1 - q2Sq * q2)).Length2) *  //Es 空間的インコヒーレンス
                        Math.Exp(-PiSq * rambdaSq * deltaSq * (q1Sq - q2Sq) * (q1Sq - q2Sq) / 2.0) //Ec 時間的インコヒーレンス
                        ).ToArray();

                    gList.Add((psi, vec, lenz));
                }
        }
        //gListを並び替え
        gList.Sort((g1, g2) =>
        {
            double x = g1.Vec.X - g2.Vec.X, y = g1.Vec.Y - g2.Vec.Y;
            return (y > 0 || (y == 0 && x > 0)) ? 1 : (y < 0 || (y == 0 && x < 0)) ? -1 : 0;
        });

        //imagesを初期化
        var images = defocusses.Select(d => new double[width * height]).ToArray();

        //各ピクセルの計算
        double cX = width / 2.0, cY = height / 2.0;
        var shift = Crystal.RotationMatrix * (Crystal.A_Axis + Crystal.B_Axis + Crystal.C_Axis) / 2;
        if (native && NativeWrapper.Enabled)//ネイティブC++で実行 3倍速い
        {
            var (gPsi, gVec, gLenz) = NativeWrapper.HRTEM_Helper(gList);
            int divTotal = Environment.ProcessorCount * 4, step = width * height / divTotal;
            Parallel.For(0, divTotal, div =>
            {
                int start = step * div, count = div == divTotal - 1 ? width * height - start : step;
                var rVec = Enumerable.Range(start, count).SelectMany(n => new[] { -res * (n % width - cX) + shift.X, -res * (height - n / width - 1 - cY) + shift.Y }).ToArray();
                var results = NativeWrapper.HRTEM_Solver(gPsi, gVec, gLenz, rVec, quasiMode);
                for (var i = 0; i < defLen; i++)
                    Array.Copy(results, i * count, images[i], start, count);
            });
        }
        else//Managed
        {
            Parallel.For(0, width * height, n =>
            {
                PointD r = new(-(n % width - cX) * res + shift.X, -(height - n / width - 1 - cY) * res + shift.Y), _vec = new(double.NaN, double.NaN);
                var sums = new Complex[defLen];
                var exp = new Complex(0, 0);
                foreach (var (Psi, Vec, Lenz) in gList)
                {
                    if (_vec != Vec)
                        exp = Exp(Vec * r * TwoPiI);

                    for (var (i, f) = (0, Psi * exp); i < defLen; i++)
                        sums[i] += f * Lenz[i];

                    _vec = Vec;
                }
                for (var i = 0; i < defLen; i++)
                    images[i][n] = quasiMode ? sums[i].MagnitudeSquared() : Math.Abs(sums[i].Real);
            });
        }

        //20220519 上下左右が反転しているみたいなので、その対処
        for (int i = 0; i < images.Length; i++)
            Array.Reverse(images[i]);

        return images;
    }
    #endregion Image Simulation

    #region ポテンシャル U
    /// <summary>
    /// ポテンシャルUを求める。h, inner, outerを省略した場合は、通常のポテンシャルを計算する。
    /// hのみを省略すると局所形式の非弾性ポテンシャル、全て省略しない場合は非局所形式のポテンシャルを計算。
    /// </summary>
    /// <param name="kV"></param>
    /// <param name="g"></param>
    /// <param name="h"></param>
    /// <param name="inner"></param>
    /// <param name="outer"></param>
    /// <returns></returns>
    public (Complex Real, Complex Imag) getU(in double kV, in Beam g, in Beam h = null, double inner = double.NaN, double outer = double.NaN)
    {
        var index = h != null ? (g.H - h.H, g.K - h.K, g.L - h.L) : g.Index;
        var key1 = compose(g.Index);
        var key2 = h != null ? compose(h.Index) : int.MaxValue;
        if (!uDictionary.TryGetValue((key1, key2), out (Complex real, Complex imag) U))
        {
            var s2 = h != null ? (g.Vec - h.Vec).Length2 / 4 : g.Vec.Length2 / 4;
            var k0 = UniversalConstants.Convert.EnergyToElectronWaveNumber(kV);
            double a = Crystal.A, b = Crystal.B, c = Crystal.C;

            Complex fReal = 0, fImag = 0;
            foreach (var atoms in Crystal.Atoms)
            {
                var es = AtomStatic.ElectronScatteringPeng[atoms.AtomicNumber][atoms.SubNumberElectron];//5 gaussian
                //var es = AtomStatic.ElectronScatteringEightGaussian[atoms.AtomicNumber];//8 gaussian  
                var real = es.Factor(s2);//弾性散乱因子
                #region お蔵
                //var m = atoms.Dsf.UseIso || index == (0, 0, 0) ? atoms.Dsf.Biso : 0;
                //if (!atoms.Dsf.UseIso && double.IsNaN(m) && index == (0, 0, 0))// 非等方でg = 0、かつmがNaNの時 Acta Cryst. (1959). 12, 609 , Hamilton の式に従って、Bisoを計算
                //    m = (atoms.Dsf.B11 * a * a + atoms.Dsf.B22 * b * b + atoms.Dsf.B33 * c * c + 2 * atoms.Dsf.B12 * a * b + 2 * atoms.Dsf.B23 * b * c + 2 * atoms.Dsf.B31 * c * a) * 4.0 / 3.0;


                //if (atoms.Dsf.UseIso || index == (0, 0, 0))
                //    imag = (double.IsNaN(inner * outer)) ? es.FactorImaginary(kV, s2, m) :
                //        h == null ? es.FactorImaginaryAnnular(kV, g.Vec, m, inner, outer) : es.FactorImaginaryAnnular(kV, g.Vec, h.Vec, m, inner, outer);//非弾性散乱因子 答えは無次元
                #endregion
                double imag = double.NaN, m = double.NaN;
                foreach (var atom in atoms.Atom)
                {
                    if((!atoms.Dsf.UseIso && index != (0, 0, 0)) || double.IsNaN(imag))//非等方でg≠0の時、あるいは初めての時
                    {
                        if (atoms.Dsf.UseIso)
                            m = atoms.Dsf.Biso;
                        else if (index == (0, 0, 0))
                            m = double.IsNaN(atoms.Dsf.Biso) ? atoms.Dsf.Biso000 : atoms.Dsf.Biso;
                        else
                        {
                            var (H, K, L) = atom.Operation.ConvertPlaneIndex(index);
                            m = (atoms.Dsf.B11 * H * H + atoms.Dsf.B22 * K * K + atoms.Dsf.B33 * L * L + 2 * atoms.Dsf.B12 * H * K + 2 * atoms.Dsf.B23 * K * L + 2 * atoms.Dsf.B31 * L * H) / s2;
                        }
                        if (double.IsNaN(m)) 
                            m = 0;
                        imag = (double.IsNaN(inner * outer)) ? es.FactorImaginary(kV, s2, m) :
                            h == null ? es.FactorImaginaryAnnular(kV, g.Vec, m, inner, outer) : es.FactorImaginaryAnnular(kV, g.Vec, h.Vec, m, inner, outer);//非弾性散乱因子 答えは無次元
                    }
                    #region お蔵
                    //if (!atoms.Dsf.UseIso && index != (0, 0, 0))
                    //{
                    //    var (H, K, L) = atom.Operation.ConvertPlaneIndex(index);
                    //    m = (atoms.Dsf.B11 * H * H + atoms.Dsf.B22 * K * K + atoms.Dsf.B33 * L * L + 2 * atoms.Dsf.B12 * H * K + 2 * atoms.Dsf.B23 * K * L + 2 * atoms.Dsf.B31 * L * H) / s2;
                    //    imag = (double.IsNaN(inner * outer)) ? es.FactorImaginary(kV, s2, m) :
                    //        h == null ? es.FactorImaginaryAnnular(kV, g.Vec, m, inner, outer) : es.FactorImaginaryAnnular(kV, g.Vec, h.Vec, m, inner, outer);//非弾性散乱因子 答えは無次元
                    //}
                    //if (double.IsNaN(m)) m = 0;
                    #endregion
                    var d = Exp(-m * s2 - TwoPiI * (atom * index)) * atoms.Occ;
                    fReal += real * d;
                    fImag += imag * d;
                }
            }

            //係数については、 Kirklandの教科書のp120参照
            //相対論補正
            var gamma = 1 + UniversalConstants.e0 * kV * 1E3 / UniversalConstants.m0 / UniversalConstants.c2;

            U = (fReal * gamma / Math.PI / Crystal.Volume, fImag * gamma / Math.PI / Crystal.Volume);
            if (kV > 0)
                lock (lockObj1)
                    uDictionary.TryAdd((key1, key2), U);
        }
        return U;
    }
    /// <summary>
    /// 局所ポテンシャル形式で計算
    /// </summary>
    /// <param name="g"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public (Complex Real, Complex Imag) getU(Beam g) => getU(AccVoltage, g);

    /// <summary>
    /// g=0 のuの値を得る
    /// </summary>
    /// <param name="voltage"></param>
    /// <returns></returns>
    public (Complex Real, Complex Imag) getU(double voltage) => getU(voltage, new Beam((0, 0, 0), new Vector3DBase(0, 0, 0)));

    private readonly Dictionary<(int Key1, int Key2), (Complex Real, Complex Imag)> uDictionary = new();
    #endregion

    #region ポテンシャルのマトリックス
    /// <summary>
    /// ポテンシャルマトリックスを求める. k0の単位はnm^-1. 
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    private Complex[] getPotentialMatrix(Beam[] b)
    {
        var potentialMatrix = GC.AllocateUninitializedArray<Complex>(b.Length * b.Length);//A行列確保
        getPotentialMatrix(b.Length, b, ref potentialMatrix);
        return potentialMatrix;
    }

    private void getPotentialMatrix(int dim, Beam[] b, ref Complex[] potentialMatrix)
    {
        //A行列を決定
        for (int col = 0; col < dim; col++)
            for (int row = 0; row < dim; row++)
            {
                var (Real, Imag) = getU(b[col] - b[row]);
                potentialMatrix[row + col * dim] = row == col ? ImaginaryOne * Imag : Real + ImaginaryOne * Imag;
            }
    }
    #endregion

    #region 固有値問題対象のマトリックス
    /// <summary>
    /// 固有値問題マトリックスを求める. k0の単位はnm^-1. パフォーマンス上の理由から、一次元配列にしている。
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    private Complex[] getEigenMatrix(Beam[] b, Complex[] potentialMatrix = null)
    {
        var eigenMatrix = GC.AllocateUninitializedArray<Complex>(b.Length * b.Length);//A行列確保
        getEigenMatrix(b.Length, b, ref eigenMatrix, potentialMatrix);
        return eigenMatrix;
    }
    private void getEigenMatrix(int dim, Beam[] b, ref Complex[] eigenMatrix, Complex[] potentialMatrix = null)
    {
        if (potentialMatrix == null || potentialMatrix.Length != dim * dim)
            potentialMatrix = getPotentialMatrix(b);

        //A行列を決定
        for (int col = 0; col < dim; col++)
        {
            for (int row = 0; row < dim; row++)
                eigenMatrix[row + col * dim] = potentialMatrix[row + col * dim] / b[col].P;
            eigenMatrix[col * dim + col] += b[col].Q / b[col].P;
        }
    }


    #endregion

    #region 候補となるg vectorsの検索
    static readonly (int h, int k, int l)[] directionF = new[] { (1, 1, 1), (1, 1, -1), (1, -1, 1), (1, -1, -1), (-1, 1, 1), (-1, 1, -1), (-1, -1, 1), (-1, -1, -1) };
    static readonly (int h, int k, int l)[] directionA = new[] { (0, 1, 1), (0, 1, -1), (0, -1, 1), (0, -1, -1), (1, 0, 0), (-1, 0, 0) };
    static readonly (int h, int k, int l)[] directionB = new[] { (1, 0, 1), (1, 0, -1), (-1, 0, 1), (-1, 0, -1), (0, 1, 0), (0, -1, 0) };
    static readonly (int h, int k, int l)[] directionC = new[] { (1, 1, 0), (1, -1, 0), (-1, 1, 0), (-1, -1, 0), (0, 0, 1), (0, 0, -1) };
    static readonly (int h, int k, int l)[] directionI = new[] { (1, 1, 0), (1, -1, 0), (-1, 1, 0), (-1, -1, 0), (0, 1, 1), (0, 1, -1), (0, -1, 1), (0, -1, -1), (1, 0, 1), (1, 0, -1), (-1, 0, 1), (-1, 0, -1) };
    static readonly (int h, int k, int l)[] directionRH = new[] { (1, 0, 1), (0, -1, 1), (-1, 1, 1), (-1, 0, -1), (0, 1, -1), (1, -1, -1) };
    static readonly (int h, int k, int l)[] directionHex = new[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (1, -1, 0), (-1, 1, 0), (0, 0, 1), (0, 0, -1) };
    static readonly (int h, int k, int l)[] directionP = new[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) };

    readonly Dictionary<(int H, int K, int L), Vector3DBase> gDic = new();
    static int compose(in int h, in int k, in int l) => ((h + 255) << 20) + ((k + 255) << 10) + l + 255;
    static int compose(in (int h, int k, int l) index) => ((index.h + 255) << 20) + ((index.k + 255) << 10) + index.l + 255;
    static (int h, int k, int l) decompose(in int key) => ((key >> 20) - 255, ((key << 12) >> 22) - 255, ((key << 22) >> 22) - 255);

    /// <summary>
    /// 候補となるgVectorを検索する.
    /// </summary>
    /// <param name="baseRotation">結晶方位</param>
    /// <param name="vecK0">ビーム方位</param>
    /// <param name="maxNumOfBloch">指定しない場合は MaxNumOfBloch を使用 </param>
    /// <param name="use_gDictionary">ビーム方位や結晶方位が変化していない場合はtrue</param>
    /// <returns></returns>
    public Beam[] Find_gVectors(Matrix3D baseRotation, Vector3DBase vecK0, int maxNumOfBloch = -1, bool use_gDictionary = false)
    {
        if (!use_gDictionary)
            gDic.Clear();

        if (maxNumOfBloch == -1)
            maxNumOfBloch = MaxNumOfBloch;
        var mat = baseRotation * Crystal.MatrixInverse.Transpose();
        var direction = Array.Empty<(int h, int k, int l)>();

        #region directionを初期化
        if (Crystal.Symmetry.LatticeTypeStr == "F") direction = directionF;
        else if (Crystal.Symmetry.LatticeTypeStr == "A") direction = directionA;
        else if (Crystal.Symmetry.LatticeTypeStr == "B") direction = directionB;
        else if (Crystal.Symmetry.LatticeTypeStr == "C") direction = directionC;
        else if (Crystal.Symmetry.LatticeTypeStr == "I") direction = directionI;
        else if (Crystal.Symmetry.LatticeTypeStr == "R" && Crystal.Symmetry.SpaceGroupHMsubStr == "H") direction = directionRH;
        else if (Crystal.Symmetry.CrystalSystemStr == "trigonal" || Crystal.Symmetry.CrystalSystemStr == "hexagonal") direction = directionHex;
        else direction = directionP;
        #endregion directionを初期化

        var (q0, p0) = getQP(new Vector3DBase(0, 0, 0), vecK0);
        var beams = new List<Beam>(maxNumOfBloch * 6) { { new Beam((0, 0, 0), new Vector3DBase(0, 0, 0), getU(AccVoltage), (q0, p0)) } };
        var outer = new List<((int H, int K, int L) key, double gLen)> { ((0, 0, 0), 0) };
        var whole = new HashSet<(int H, int K, int L)> { (0, 0, 0) };

        var shift = direction.Select(dir => (mat * dir).Length).Max() * 1.01;

        double k0_2 = vecK0.Length2, k0 = vecK0.Length;
        var maxQ = Math.Abs(k0_2 - (k0 + shift) * (k0 + shift));

        Vector3DBase g;
        while (beams.Count < maxNumOfBloch * 20 && whole.Count < 1000000 && outer.Count > 0)
        {
           var min = outer[0].gLen + shift;
           var end = outer.FindLastIndex(o => o.gLen - min < shift * 2);
            
            foreach (var (key, gLen) in CollectionsMarshal.AsSpan(outer)[..(end +1)])
            {
                (int h1, int k1, int l1) = key;
                foreach ((int h2, int k2, int l2) in direction)
                {
                    var index = (h1 + h2, k1 + k2, l1 + l2);
                    if (whole.Add(index))
                    {
                        if (!use_gDictionary)
                            g = mat * index;
                        else if (!gDic.TryGetValue(index, out g)) //ビーム方位や結晶方位が変化していない場合はDictionaryを利用して  g = mat * indexの計算を短縮
                        {
                            g = mat * index;
                            lock (lockObj2)
                                gDic.TryAdd(index, g);
                        }

                        var v = g + vecK0;
                        var vLen2 = v.Length2;

                        var (q, p) = (k0_2 - vLen2, 2 * Surface * v);
                        if (Math.Abs(g.Z) < 0.1)//検証コード
                        if (Math.Abs(q) < maxQ)
                            beams.Add(new Beam(index, g, getU(AccVoltage, new Beam(index, g)), (q, p)));
                        outer.Add((index, g.Length));
                    }
                }
            }
            outer.RemoveRange(0, end + 1);
            outer.Sort((o1, o2) => o1.gLen.CompareTo(o2.gLen));
        }

        //indexが小さく、かつQg(励起誤差)の小さいg-vectorを抽出する
        beams.Sort((a, b) => a.Rating.CompareTo(b.Rating));

        if (beams.Count > maxNumOfBloch + 1)
            beams.RemoveRange(maxNumOfBloch + 1, beams.Count - maxNumOfBloch - 1);

        //X,Y座標が同じものを削除
        for (int i = 0; i < beams.Count; i++)
        {
            var bi = beams[i];
            for (int j = i + 1; j < beams.Count; j++)
            {
                var bj = beams[j];
                if (Math.Abs(bi.Vec.X - bj.Vec.X) < 1E-6 && Math.Abs(bi.Vec.Y - bj.Vec.Y) < 1E-6)
                {
                    if (Math.Abs(bi.S) > Math.Abs(bj.S))
                    {
                        beams.RemoveAt(i--);//iの方を除去
                        break;
                    }
                    else
                        beams.RemoveAt(j--);
                }
            }
        }

        int n = beams.Count - 1;
        for (int i = beams.Count - 1; i >= 1; i--)
            if (Math.Abs(beams[i].Rating - beams[i - 1].Rating) > 1E-6)
            {
                n = i;
                break;
            }
        beams.RemoveRange(n, beams.Count - n);

        return beams.ToArray();
    }

    #endregion

    #region 絞りの内部にあるビームのみ選び取る (HRTEM シミュレータから呼ばれる)
    public static Beam[] ExtractInsideBeams(Beam[] beams, double acc, double radius, double shiftX, double shiftY)
    {
        if (double.IsInfinity(radius))
            return beams.ToArray();
        else
        {
            var rambda = UniversalConstants.Convert.EnergyToElectronWaveLength(acc);
            var center = new PointD(2 * Math.Sin(shiftX / 2) / rambda, 2 * Math.Sin(shiftY / 2) / rambda);
            var r = 2 * Math.Sin(radius / 2) / rambda;
            return beams.Where(b => (b.Vec.ToPointD - center).Length2 < r * r).ToArray();
        }
    }
    #endregion

    #region P, Q のリセットやゲット

    /// <summary>
    /// 引数のBeamsとrotationをもとに、PとQだけセットして返す。ほかのパラメータは放置.
    /// </summary>
    /// <param name="baseRotation"></param>
    /// <param name="k0"></param>
    /// <returns></returns>
    public Beam[] reset_gVectors(Beam[] beams, Matrix3D baseRotation, Vector3DBase vecK0)
    {
        var newBeams = new Beam[beams.Length];
        reset_gVectors(beams.Length, beams, baseRotation, vecK0, ref newBeams);
        return newBeams;
    }

    public void reset_gVectors(int dim, Beam[] beams, Matrix3D baseRotation, Vector3DBase vecK0, ref Beam[] newBeams)
    {
        var mat = baseRotation * Crystal.MatrixInverse.Transpose();
        for (int i = 0; i < dim; i++)
        {
            var g = mat * beams[i].Index;
            var prms = getQP(g, vecK0);
            newBeams[i] = new Beam(prms);
        }
    }



    private static double getQ(in Vector3DBase g, in Vector3DBase vecK0) => vecK0.Length2 - (vecK0 + g).Length2;

    private double getP(in Vector3DBase g, in Vector3DBase vecK0) => 2 * Surface * (vecK0 + g);

    private (double Q, double P) getQP(in Vector3DBase g, in Vector3DBase vecK0) => (getQ(g, vecK0), getP(g, vecK0));

    public (double Q, double P) getQP(in Vector3DBase g, double kvac, double u0, Vector3DBase beamDirection = null)
        => getQP(g, getVecK0(kvac, u0, beamDirection));

    #endregion

    #region K0ベクトルを求める
    /// <summary>
    /// K0ベクトルを求める。K0ベクトルは、XY方向を保存したままZ方向のみ変化する。
    /// </summary>
    /// <param name="beamRotation"></param>
    /// <param name="kvac"></param>
    /// <param name="u0"></param>
    /// <returns></returns>
    public Vector3DBase getVecK0(double kvac, double u0, Vector3DBase beamDirection = null)
    {
        // |k0|^2 - |kvac|^2 = u0
        // vecK0 = vecKvac + x * vecSurface
        //   =>   x^2 + 2 x * vecKvac - u0 = 0
        // を満たすxを求めれば良い。
        var vecKvac = (beamDirection == null) ? new Vector3DBase(0, 0, -kvac) : kvac * beamDirection;
        var b = Surface * vecKvac;
        var x = Math.Sqrt(b * b + u0) - b;
        return vecKvac + x * Surface;
    }

    #endregion

    #region Beamクラス

    public class Beam
    {
        /// <summary>
        /// 指数
        /// </summary>
        public int H => Index.H;
        public int K => Index.K;
        public int L => Index.L;

        /// <summary>
        /// 指数
        /// </summary>
        public (int H, int K, int L) Index;

        /// <summary>
        /// 逆格子ベクトル
        /// </summary>
        public Vector3DBase Vec;

        /// <summary>
        /// 励起誤差
        /// </summary>
        public double S => Math.Sqrt(P * P / 4 + Q) - P / 2;

        public Complex Ureal;

        public Complex Uimag;

        /// <summary>
        /// k0^2 - (k0 + g)^2 = - g (2 k0 +g) (2 k0 S 励起誤差とわずかに定義が違う)
        /// </summary>
        public double Q;

        /// <summary>
        /// 2 n (k0 + g) 大塚さんの資料参考
        /// </summary>
        public double P;

        /// <summary>
        /// 振幅
        /// </summary>
        public Complex Psi;

        /// <summary>
        /// 強度を保存する(PED計算の時のみ使用)
        /// </summary>
        public double intensity = 0;


        /// <summary>
        /// レンズ関数 
        /// 球面収差関数 × 時間的インコヒーレンス包絡関数 × 空間的インコヒーレンス包絡関数 
        /// </summary>
        public Complex Lenz = new(1, 0);

        /// <summary>
        /// 評価値
        /// </summary>
        public double Rating => Vec.Length * Q * Q;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="hkl">指数</param>
        /// <param name="vec">逆格子ベクトル</param>
        /// <param name="s">励起誤差</param>
        public Beam(in (int H, int K, int L) index, Vector3DBase vec, in (Complex Real, Complex Imag) f, in (double Q, double P) prms)
        {
            Index = index;
            Vec = vec;
            Ureal = f.Real;
            Uimag = f.Imag;
            Q = prms.Q;
            P = prms.P;
        }

        public Beam(in (int H, int K, int L) index, Vector3DBase vec)
        {
            Index = index;
            Vec = vec;
        }

        public Beam(Vector3D vec)
        {
            Index = vec.Index;
            Vec = vec;
        }

        public Beam(double q, double p)
        {
            Q = q;
            P = p;
        }
        public Beam((double Q, double P) prms) : this(prms.Q, prms.P) { }

        public Vector3D ConvertToVector3D()
        {
            var g = new Vector3D(Vec.X, Vec.Y, Vec.Z);
            g.d = 1 / g.Length;
            g.Text = $"{H} {K} {L}";
            g.Index = (H, K, L);
            g.F = Psi;
            g.RawIntensity = Psi.MagnitudeSquared();
            g.Tag = S;
            g.Flag1 = true;
            g.Argb = Color.White.ToArgb();
            return g;
        }

        public static Beam operator -(Beam b1) => new((-b1.H, -b1.K, -b1.L), -b1.Vec);
        public static Beam operator -(Beam b1, Beam b2) => new((b1.H - b2.H, b1.K - b2.K, b1.L - b2.L), b1.Vec - b2.Vec);
        public static Beam operator +(Beam b1, Beam b2) => new((b1.H + b2.H, b1.K + b2.K, b1.L + b2.L), b1.Vec + b2.Vec);

        public override string ToString()
        {
            return $"{H} {K} {L}, (x, y, z)=({Vec.X}, {Vec.Y}, {Vec.Z}), Length={Vec.Length}, Q={Q} ";
        }
    }

    #endregion

    #region CBED_Diskクラス
    public class CBED_Disk
    {
        /// <summary>
        /// 指数
        /// </summary>
        public readonly int H, K, L;

        /// <summary>
        /// 厚み
        /// </summary>
        public readonly double Thickness;

        public readonly Vector3DBase G;

        /// <summary>
        /// 振幅を格納した配列
        /// </summary>
        public Complex[] Amplitudes;

        public readonly Complex[] RawAmplitudes;

        public CBED_Disk(int[] hkl, Vector3DBase vec, double thickness, Complex[] amplitudes)
        {
            H = hkl[0];
            K = hkl[1];
            L = hkl[2];
            G = vec;
            Thickness = thickness;
            //Amplitudes = amplitudes;
            RawAmplitudes = amplitudes;
        }
    }
    #endregion

}
