#region using
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Buffers;
using System.Collections.Frozen;
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
    private const double TwoPi = Constants.Pi2;
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
    /// CBEDのディスク情報 Disks[Z(thickness)_index][G_index], EBSDのときは [Voltage][Z(thickness)_index]
    /// </summary>
    [XmlIgnore]
    public CBED_Disk[][] Disks { get; set; }

    [NonSerialized]
    public Beam[] Beams;

    [NonSerialized]
    private readonly BackgroundWorker bwCBED = new();
    public event ProgressChangedEventHandler CBED_ProgressChanged;
    public event RunWorkerCompletedEventHandler CBED_Completed;

    [NonSerialized]
    private readonly BackgroundWorker bwEBSD = new();
    public event ProgressChangedEventHandler EBSD_ProgressChanged;
    public event RunWorkerCompletedEventHandler EBSD_Completed;

    [NonSerialized]
    private readonly BackgroundWorker bwSTEM = new();
    public event ProgressChangedEventHandler StemProgressChanged;
    public event RunWorkerCompletedEventHandler StemCompleted;

    private readonly object lockObj1 = new();
    private readonly object lockObj2 = new();

    /// <summary>
    /// Result_STEM_Ela[thickness][defocus]
    /// </summary>
    public (Size Size, double Resolution, double[] Thicknesses, double[] Defocusses, Matrix3D rot, double[][][] ImageBoth, double[][][] ImageEla, double[][][] ImageTDS) ResultSTEM;

    /// <summary>
    /// Result_STEM_TDS[thickness][defocus]
    /// </summary>
    public (Size Size, double Resolution, double[] Thicknesses, double[] Defocusses, Matrix3D rot, double[][][] Image) ResultHRTEM;

    /// <summary>
    /// 
    /// </summary>
    public (int Width, int Height, double Resolution, double[][][] Image) Result_Potential;

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

        bwEBSD = new BackgroundWorker
        {
            WorkerSupportsCancellation = true,
            WorkerReportsProgress = true
        };
        bwEBSD.RunWorkerCompleted += Ebsd_RunWorkerCompleted;
        bwEBSD.ProgressChanged += Ebsd_ProgressChanged;
        bwEBSD.DoWork += ebsd_DoWork;

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
    private void Cbed_ProgressChanged(object sender, ProgressChangedEventArgs e) => CBED_ProgressChanged?.Invoke(sender, e);

    private void Cbed_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => CBED_Completed?.Invoke(sender, e);

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
    public void RunCBED(int maxNumOfBloch, double voltage, Matrix3D rotation,
        double[] thickness, Vector3DBase[] beamDirections, bool LACBED, Solver solver = Solver.Auto, int thread = 1)
    {
        MaxNumOfBloch = maxNumOfBloch;
        AccVoltage = voltage;
        BaseRotation = new Matrix3D(rotation);
        BeamDirections = beamDirections;
        Thicknesses = thickness;

        bwCBED.RunWorkerAsync((LACBED, solver, thread));
    }

    /// <summary>
    /// CBED計算
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private unsafe void cbed_DoWork(object sender, DoWorkEventArgs e)
    {
        var (LACBED, solver, thread) = ((bool, Solver, int))e.Argument;

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
                    result = NativeWrapper.CBEDSolver_Eigen(eigenMatrix, psi0.Values, Thicknesses);
                //Eigen_MKL あるいは Eigen_Managedの場合    
                else if (solver == Solver.Eigen_MKL)
                {
                    var evd = new DMat(bLen, bLen, eigenMatrix.AsSpan()[..(bLen * bLen)].ToArray()).Evd(Symmetricity.Asymmetric);
                    var alpha = evd.EigenVectors.LU().Solve(psi0);

                    var resultMat = new DMat(bLen, tLen);
                    for (int t = 0; t < tLen; t++)
                    {
                        //ガンマの対称行列×アルファを作成
                        var gammmaAlpha = new DVec(evd.EigenValues.Select((ev, i) => Exp(TwoPiI * ev * Thicknesses[t]) * alpha[i]).ToArray());
                        //深さtにおけるψを求める
                        resultMat.SetColumn(t, evd.EigenVectors.Multiply(gammmaAlpha));
                    }
                    result = resultMat.Values;
                }
                //MtxExp_Eigenの場合
                else if (solver == Solver.MtxExp_Eigen && EigenEnabled)
                    result = NativeWrapper.CBEDSolver_MatExp(eigenMatrix, psi0.Values, Thicknesses);
                //MtxExp_MKLの場合 
                else
                {
                    var resultMat = new DMat(bLen, tLen);
                    var matExp = (DMat)(TwoPiI * Thicknesses[0] * new DMat(bLen, bLen, eigenMatrix.AsSpan()[..(bLen * bLen)].ToArray())).Exponential();
                    var vec = matExp.Multiply(psi0);
                    resultMat.SetColumn(0, vec);

                    if (tLen > 1)
                    {
                        if (Thicknesses[1] - Thicknesses[0] == Thicknesses[0])
                            matExp = (DMat)(TwoPiI * (Thicknesses[1] - Thicknesses[0]) * new DMat(bLen, bLen, eigenMatrix.AsSpan()[..(bLen * bLen)].ToArray())).Exponential();
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


        Disks = new CBED_Disk[Thicknesses.Length][];

        if (LACBED)//LACBEDモードの時は000を作成しておしまい。
        {
            Parallel.For(0, Thicknesses.Length, t =>
            {
                Disks[t] = new CBED_Disk[1];
                var amplitudes = new Complex[BeamDirections.Length];
                for (int r = 0; r < BeamDirections.Length; r++)
                    if (diskAmplitude[r] != null)
                        amplitudes[r] = diskAmplitude[r][t * bLen];

                Disks[t][0] = new CBED_Disk([Beams[0].H, Beams[0].K, Beams[0].L], Beams[0].Vec, Thicknesses[t], amplitudes) { Amplitudes = amplitudes };
            });

        }
        else//通常のCBEDの場合はdiskをコンパイルする
        {
            Parallel.For(0, Thicknesses.Length, t =>
            {
                Disks[t] = new CBED_Disk[Beams.Length];
                for (int g = 0; g < Beams.Length; g++)
                {
                    var amplitudes = new Complex[BeamDirections.Length];
                    for (int r = 0; r < BeamDirections.Length; r++)
                        if (diskAmplitude[r] != null)
                            amplitudes[r] = diskAmplitude[r][t * bLen + g];

                    Disks[t][g] = new CBED_Disk([Beams[g].H, Beams[g].K, Beams[g].L], Beams[g].Vec, Thicknesses[t], amplitudes);

                }
            });

            //ここから、diskの重なり合いを計算
            if (!LACBED)
            {
                //まず、各ディスクを構成するピクセルの座標を計算
                var diskTemp = new (RectangleD Rect, PointD[] Pos)[Beams.Length];
                Parallel.For(0, Beams.Length, g =>
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
            }
        }

        if (bwCBED.CancellationPending)
            e.Cancel = true;
    }
    #endregion

    #region EBSD
    private void Ebsd_ProgressChanged(object sender, ProgressChangedEventArgs e) => EBSD_ProgressChanged?.Invoke(sender, e);

    private void Ebsd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => EBSD_Completed?.Invoke(sender, e);

    public void CancelEBSD()
    {
        if (bwEBSD.IsBusy)
            bwEBSD.CancelAsync();
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="maxNumOfBloch"></param>
    /// <param name="voltage">加速電圧(kV)</param>
    /// <param name="rotation">基準となる方位</param>
    /// <param name="thickness">厚みの配列</param>
    /// <param name="beamRotations">基準となる方位に乗算する方位配列</param>
    public void RunEBSD(int maxNumOfBloch, double[] voltages, Matrix3D rotation, double[] thickness, Vector3DBase[] beamDirections, Solver solver = Solver.Auto, int thread = 1)
    {
        MaxNumOfBloch = maxNumOfBloch;

        BaseRotation = new Matrix3D(rotation);
        BeamDirections = beamDirections;
        Thicknesses = thickness;

        bwEBSD.RunWorkerAsync((solver, thread, voltages));
    }

    /// <summary>
    /// EBSD計算用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ebsd_DoWork(object sender, DoWorkEventArgs e)
    {
        var (solver, thread, voltages) = ((Solver, int, double[]))e.Argument;

        Disks = new CBED_Disk[voltages.Length][];
        int count = 0;

        var beamDirectionsP = BeamDirections.AsParallel().WithDegreeOfParallelism(thread);
        int width = (int)Math.Sqrt(BeamDirections.Length);
        double radius = width / 2.0;
        bool inside(int i) => (i % width - radius + 0.5) * (i % width - radius + 0.5) + (i / width - radius + 0.5) * (i / width - radius + 0.5) <= radius * radius;

        for (int vIndex = 0; vIndex < voltages.Length; vIndex++)
        {
            AccVoltage = voltages[vIndex];
            //波数を計算
            var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(AccVoltage);
            //U0を計算
            var u0 = getU(AccVoltage).Real.Real;
            gDic.Clear();
            //進捗状況報告用の各種定数を初期化

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

            //diskAmplitude[r][t][g]
            var diskAmplitude = beamDirectionsP.Select((beamDirection, i) =>
            {
                if (!inside(i)) return (null, null);

                if (bwEBSD.CancellationPending) return (null, null);
                var coeff = Math.Abs(1.0 / beamDirection.Z); // = 1/cosTau

                var vecK0 = getVecK0(kvac, u0, beamDirection);

                //var beams = Find_gVectors(BaseRotation, vecK0, MaxNumOfBloch, true);
                var beams = Find_gVectors(BaseRotation, vecK0, MaxNumOfBloch);
                var potentialMatrix = getEigenMatrix(beams);
                var len = beams.Length;
                //入射面での波動関数を定義
                var psi0 = new DVec(Enumerable.Range(0, len).ToList().Select(g => g == 0 ? One : 0).ToArray());

                Complex[] result;

                //ポテンシャル行列の固有値、固有ベクトルを取得し、resultに格納
                #region 各ソルバーによる計算
                //Eigen＿Eigenの場合
                if (solver == Solver.Eigen_Eigen && EigenEnabled)
                    result = NativeWrapper.CBEDSolver_Eigen(potentialMatrix, [.. psi0], Thicknesses);
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
                    result = NativeWrapper.CBEDSolver_MatExp(potentialMatrix, [.. psi0], Thicknesses);
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

                bwEBSD.ReportProgress(Interlocked.Increment(ref count), reportString);//進捗状況を報告
                return (result, beams);
            }).ToArray();

            //count = 0;
            //bwEBSD.ReportProgress(0, "Compiling disks");

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
                //bwEBSD.ReportProgress(Interlocked.Increment(ref count) * 1000 / BeamDirections.Length, "Compiling disks");
            });

            Disks[vIndex] = new CBED_Disk[Thicknesses.Length];
            for (int t = 0; t < Thicknesses.Length; t++)
            {
                Disks[vIndex][t] = new CBED_Disk([0, 0, 0], new Vector3DBase(0, 0, 0), Thicknesses[t],
                    directDiskIntensities[t].Select(intensity => new Complex(Math.Sqrt(intensity), 0)).ToArray());
                Disks[vIndex][t].Amplitudes = Disks[vIndex][t].RawAmplitudes;
            }

            if (bwEBSD.CancellationPending)
                e.Cancel = true;
        }
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

            if (Beams == null || Beams.Length == 0) return [];

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

        //出射面での境界条件を考慮した位相にする (20230827)
        var p = new DiagonalMatrix(dim, dim, Beams.Select(b => Exp(PiI * b.P * thickness)).ToArray());

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
                if (!compiled.TryGetValue(beam.Index, out Beam value))
                {
                    compiled.Add(beam.Index, beam);
                    compiled[beam.Index].intensity = beam.Psi.MagnitudeSquared() / step;
                }
                else
                    value.intensity += beam.Psi.MagnitudeSquared() / step;
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
        Beams = [.. beams];
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
    public void RunSTEM(int maxNumOfBloch, double voltage, double cs, double delta, double sliceThickness, Size imageSize, double resolution, double sourceSize,
        Matrix3D baseRotation, double[] thicknesses, double[] defocusses,
        Vector3DBase[] beamDirections, double convergenceAngle, double detAngleInner, double detAngleOuter,
        Solver solver = Solver.Auto, int thread = 1)
    {
        MaxNumOfBloch = maxNumOfBloch;
        //MaxNumOfBloch = 10000;//検証用コード

        AccVoltage = voltage;
        //Wavelength = UniversalConstants.Convert.EnergyToElectronWaveLength(voltage);
        BaseRotation = new Matrix3D(baseRotation);
        BeamDirections = beamDirections;
        Thicknesses = thicknesses;
        if (!bwSTEM.IsBusy)
            bwSTEM.RunWorkerAsync((solver, thread, cs, delta, sliceThickness, convergenceAngle, detAngleInner, detAngleOuter, thicknesses, defocusses, imageSize, resolution, sourceSize));
    }
    public unsafe void stem_DoWork(object sender, DoWorkEventArgs e)
    {
        //MathNetの行列の内部は、1列目の要素、2列目の要素、という順番で格納されている

        var (solver, thread, cs, delta, sliceThickness, convergenceAngle, detAngleInner, detAngleOuter, thicknesses, defocusses, imageSize, resolution, sourceSize)
            = ((Solver, int, double, double, double, double, double, double, double[], double[], Size, double, double))e.Argument;

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
        Beams = [.. Find_gVectors(BaseRotation, vecK0).OrderBy(e => e.Vec.Length2)];

        #region 検証コード 25nm^-1 以上のビームは削除  25nm^-1 = 62.7mrad
        //var _beam = new List<Beam>();
        //foreach (var beam in Beams)
        //    if (Math.Abs(beam.Vec.Z) < 1.0E-10 && beam.Vec.X2Y2 < 20 * 20)
        //        _beam.Add(beam);
        //Beams = _beam.ToArray();
        #endregion

        #region 検証用コード
        //uDictionary.Clear();

        //var temp1 = new (Complex real, Complex imag)[Beams.Length * Beams.Length];
        //int n = 0;
        //for (int i = 0; i < Beams.Length; i++)
        //    for (int j = 0; j < Beams.Length; j++)
        //        temp1[n++] = getU(AccVoltage, Beams[i] - Beams[j]);

        //uDictionary.Clear();
        //var temp2 = new (Complex real, Complex imag)[Beams.Length * Beams.Length];

        //uDictionary.Clear();
        //var xx = getU(AccVoltage, Beams[0] - Beams[0], null, 0.06, 0.12).Imag * 1 / 1.39139014900906 * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733;
        //uDictionary.Clear();
        //var yy = getU(AccVoltage, Beams[0] - Beams[0], null, 0.00, 1.0).Imag * 1 / 1.39139014900906 * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733;

        //n = 0;
        //for (int i = 0; i < Beams.Length; i++)
        //    for (int j = 0; j < Beams.Length; j++)
        //    {
        //        temp2[n++] = getU(AccVoltage, Beams[i] - Beams[j], null, 0.06, 120);
        //        _sum += Math.Pow(temp2[n - 1].imag.Real - temp1[n - 1].imag.Real, 2);
        //    }
        #endregion

        int dLen = defocusses.Length, tLen = thicknesses.Length, bLen = Beams.Length;

        //入射面での波動関数を定義
        var psi0 = Enumerable.Range(0, Beams.Length).ToList().Select(g => g == 0 ? -One : 0).ToArray();
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

        Complex[][] eVec = new Complex[BeamDirections.Length][], eVal = new Complex[BeamDirections.Length][], α = new Complex[BeamDirections.Length][];
        var k_vec = GC.AllocateUninitializedArray<Vector3DBase>(BeamDirections.Length);
        var kg_z = new double[BeamDirections.Length][];
        //進捗状況報告用の各種定数を初期化
        int count = 0;
        var tc = BeamDirections.AsParallel().WithDegreeOfParallelism(thread).Select((beamDirection, i) =>
        {
            var vecK0 = getVecK0(kvac, u0, beamDirection);
            k_vec[i] = vecK0;

            if (Interlocked.Increment(ref count) % 10 == 0) bwSTEM.ReportProgress((int)(1E6 * count / BeamDirections.Length), reportString);//進捗状況を報告
            if (bwSTEM.CancellationPending) { e.Cancel = true; return null; }

            if (!inside(i)) return null;

            var eigenMatrix = GC.AllocateUninitializedArray<Complex>(bLen * bLen);// Shared.Rent(bLen * bLen);
            var beams = ArrayPool<Beam>.Shared.Rent(bLen);
            try
            {
                reset_gVectors(bLen, Beams, BaseRotation, vecK0, ref beams);
                getEigenMatrix(bLen, beams, ref eigenMatrix, potentialMatrix);

                //ポテンシャル行列の固有値、固有ベクトルを取得し、resultに格納
                Complex[] result;
                #region 各ソルバーによる計算
                //Eigen＿Eigenの場合
                if (solver == Solver.Eigen_Eigen && EigenEnabled)
                    (eVal[i], eVec[i], α[i], result) = NativeWrapper.CBEDSolver2(eigenMatrix, psi0, thicknesses);
                //Eigen_MKL あるいは Eigen_Managedの場合    
                else
                {
                    var evd = new DMat(bLen, bLen, eigenMatrix).Evd(Symmetricity.Asymmetric);
                    (eVal[i], eVec[i]) = ((evd.EigenValues as DVec).Values, (evd.EigenVectors as DMat).Values);
                    α[i] = ((DVec)(evd.EigenVectors as DMat).LU().Solve(new DVec(psi0))).Values;// NativeWrapper.PartialPivLuSolve(bLen, eVectors[i], psi0);
                    var tg = new DMat(bLen, tLen);
                    for (int t = 0; t < tLen; t++)
                    {
                        var gammmaAlpha = new DVec(eVal[i].Select((ev, g) => Exp(TwoPiI * ev * thicknesses[t]) * α[i][g]).ToArray());//ガンマの対称行列×アルファを作成
                        tg.SetColumn(t, evd.EigenVectors * gammmaAlpha);//深さtにおけるψを求める
                    }
                    result = tg.Values;
                }
                #endregion

                kg_z[i] = beams.Where(e => e != null).Select(e => e.P / 2).ToArray();

                //位相を考慮して、return
                var _tc = thicknesses.Select((thickness, t) => new Complex[bLen]).ToArray();
                for (int t = 0; t < tLen; t++)
                    for (int g = 0; g < bLen; g++)
                        _tc[t][g] = result[t * bLen + g] * Exp(TwoPiI * kg_z[i][g] * thicknesses[t]);
                return _tc;
            }
            finally { ArrayPool<Beam>.Shared.Return(beams); }

        }).ToArray();
        #endregion

        var k_xy = k_vec.Select(e => e.ToPointD).ToArray();
        //var k_z = k_vec.Select(e => e.Z).ToArray();

        #region レンズ収差関数、収束絞り関数、検出器関数
        //レンズ収差関数 W
        double lambda = UniversalConstants.Convert.EnergyToElectronWaveLength(AccVoltage), lambda2 = lambda * lambda;
        Complex Lenz(in PointD k, in PointD kq, in double defocus)
        {
            //大塚さんの資料「瀬戸先生・質問3への回答(瀬戸追記).docx」を参照
            double k2 = k.Length2, kq2 = kq.Length2;
            //球面収差の部分
            var tmp1 = -PiI * lambda * ((k2 - kq2) * defocus + cs * lambda2 * (k2 * k2 - kq2 * kq2) / 2);
            //色収差の部分
            var tmp2 = Math.PI * lambda * delta * (kq2 - k2);
            tmp2 = -tmp2 * tmp2;
            return Exp(tmp1 + tmp2);
        }
        //double W(in PointD q, in double defocus) => 0;//検証用コード

        //収束絞り関数 A
        double conv = Math.Sin(convergenceAngle) / lambda, conv2 = conv * conv;
        bool A(in PointD k) => k.Length2 <= conv2;

        // 検出器関数 D
        double inner = Math.Sin(detAngleInner) / lambda, inner2 = inner * inner, outer = Math.Sin(detAngleOuter) / lambda, outer2 = outer * outer;
        bool D(in PointD k) => k.Length2 >= inner2 && k.Length2 <= outer2;

        #endregion

        #region qList, g_q_indexを作成
        //qList　計算対象のQを網羅 
        var mat = BaseRotation * Crystal.MatrixInverse.Transpose();
        var qList = Beams.AsParallel().SelectMany(e1 => Beams.Select(e2 => (e1 - e2).Index)).Distinct()
            .Select(e => new Beam(e, mat * e)).Where(e => k_xy.Any(e2 => A(e2) && A(e2 + e.Vec.ToPointD))).OrderBy(e => e.Vec.Length2).ToList();

        //if(qList.Count > Beams.Length)
        //    qList.RemoveRange(Beams.Length, qList.Count - Beams.Length);

        //g_q_index (あるq[m]に対して、g-qの反射は、Beams配列で何番目か)
        var g_qIndex = qList.Select(q => Beams.Select((g1, n) => (g: n, g_q: Array.FindIndex(Beams, g2 => g2.Index == (g1 - q).Index))).Where(e => e.g_q != -1).ToArray()).ToArray();
        #endregion

        #region 検証用コード
        //var sb = new StringBuilder();
        //foreach (var q in qList)
        //{
        //    sb.Append($"({q.H} {q.K} {q.L})\t");

        //    uDictionary.Clear();
        //    var (real, imag) = getU(AccVoltage, q);
        //    real *= 1 / 1.39139014900906 * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733;
        //    imag *= 1 / 1.39139014900906 * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733;
        //    sb.Append($"{real.Real}\t{real.Imaginary}\t{imag.Real}\t{imag.Imaginary}\t");

        //    uDictionary.Clear();
        //    (real, imag) = getU(AccVoltage, q, null, 0.0, 1.0);
        //    real *= 1 / 1.39139014900906 * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733;
        //    imag *= 1 / 1.39139014900906 * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733;

        //    sb.Append($"{imag.Real}\t{imag.Imaginary}\t");

        //    uDictionary.Clear();
        //    (real, imag) = getU(AccVoltage, q, null, 0.06, 0.12);
        //    real *= 1 / 1.39139014900906 * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733;
        //    imag *= 1 / 1.39139014900906 * 6.62606896 * 6.62606896 / 2 / 9.1093897 / 1.60217733;

        //    sb.Append($"{imag.Real}\t{imag.Imaginary}\r\n");
        //}
        //;
        //Clipboard.SetDataObject(sb.ToString());

        #endregion

        //必要な情報だけを追加してParallelにしたtcP
        var tcP = tc.AsParallel().Select((e, i) => (index: i, result: e, xy: k_xy[i])).Where(e => e.result != null && A(e.xy)).Select(e => e.index);//.WithDegreeOfParallelism(1);

        #region listを計算
        var list = new List<(int qIndex, int[] N, double[] R, Complex[] Lenz)>[tc.Length];
        //有効なディスクを判定するフラグ
        var flag = tc.Select(e => e != null).ToArray();
        //最大のK値(計算したK0ベクトルの中で最もXY成分が大きいもの)を求める。収束角ではないことに注意(5%大きい)。
        var maxK = k_xy.Max(e => e.X);
        double coeff1 = radiusPix - 0.5, coeff2 = (radiusPix - 0.5) / maxK, coeff3 = (uint)(diameterPix - 1);
        tcP.ForAll(kIndex =>
        {
            list[kIndex] = [];
            foreach (var (qIndex, KQ) in qList.Select((b, i) => (m: i, KQ: k_xy[kIndex] + b.Vec.ToPointD)).Where(e => A(e.KQ)))
            {
                double dX = KQ.X * coeff2 + coeff1, dY = -KQ.Y * coeff2 + coeff1;//K+Q の X,Y座標(実数)
                int x = (int)(Math.Floor(dX)), y = (int)(Math.Floor(dY));//左上近接のX,Y座標(整数)
                int n0 = y * diameterPix + x, n1 = n0 + 1, n2 = n0 + diameterPix, n3 = n2 + 1;//それぞれのインデックス
                if ((uint)x < coeff3 && (uint)y < coeff3 && flag[n0] && flag[n1] && flag[n2] && flag[n3])//4つのインデックスが範囲内であることを判定
                {
                    double xx = dX - x, yy = dY - y;
                    var r = new double[] { (1 - xx) * (1 - yy), xx * (1 - yy), (1 - xx) * yy, xx * yy };//比率を計算
                    var lenz = defocusses.Select(d => Lenz(k_xy[kIndex], KQ, d)).ToArray();
                    list[kIndex].Add((qIndex, new int[] { n0, n1, n2, n3 }, r, lenz));
                }
            }
        });
        #endregion

        #region 弾性散乱 の計算
        bwSTEM.ReportProgress(0, "Calculating I_elastic(Q)");//状況を報告
        Complex[,,] I_Elas = new Complex[qList.Count, tLen, dLen];
        count = 0;
        tcP.ForAll(kIndex =>
        {
            foreach (var (qIndex, n, r, lenz) in CollectionsMarshal.AsSpan(list[kIndex]))
            {
                var i_Elas = new Complex[tLen];
                foreach (var (g, g_q) in g_qIndex[qIndex].Where(e => D(Beams[e.g].Vec.ToPointD + k_xy[kIndex])))
                    for (int t = 0; t < tLen; t++)
                    {
                        //i_Elas[t] += 1;
                        i_Elas[t] += tc[kIndex][t][g] * (r[0] * tc[n[0]][t][g_q] + r[1] * tc[n[1]][t][g_q] + r[2] * tc[n[2]][t][g_q] + r[3] * tc[n[3]][t][g_q]).Conjugate();
                    }
                lock (lockObj1)
                    for (int t = 0; t < tLen; t++)
                        for (int d = 0; d < dLen; d++)
                            I_Elas[qIndex, t, d] += i_Elas[t] * lenz[d];
            }
            if (bwSTEM.CancellationPending) return;
            if (Interlocked.Increment(ref count) % 10 == 0) bwSTEM.ReportProgress((int)(1E6 * count / tcP.Count()), "Calculating I_elastic(Q)");//状況を報告
        });
        #endregion

        #region 非弾性散乱を計算する場合
        var I_Inel = new Complex[qList.Count, tLen, dLen];

        bwSTEM.ReportProgress(0, "Calculating U matrix");//状況を報告
        var bLen2 = bLen * bLen;
        #region U行列の計算 
        count = 0;
        var U = new Complex[qList.Count * bLen2];
        uDictionary.Clear();

        //マルチスレッドの効率を上げるため、まずqList[qIndex] + Beams[i] - Beams[j]の重複を除く
        //var tmpDic = new Dictionary<(int h, int k, int l), (Beam b, int q, int i, int j)>();
        //for (int q = 0; q < qList.Count; q++)
        //    for (int j = 0; j < bLen; j++)
        //        for (int i = 0; i < bLen; i++)
        //        {
        //            var b = qList[q] + Beams[i] - Beams[j];
        //            tmpDic.TryAdd(b.Index, (b, q, i, j));
        //        }
        //tmpDic.AsParallel().ForAll(d =>
        //{
        //    getU(AccVoltage, d.Value.b, null, detAngleInner, detAngleOuter);
        //    if (Interlocked.Increment(ref count) % 10 == 0) bwSTEM.ReportProgress((int)(1E6 * count / tmpDic.Count), "Calculating U matrix");//状況を報告
        //    if (bwSTEM.CancellationPending) { e.Cancel = true; return; }
        //});

        Parallel.For(0, qList.Count, qIndex =>
        {
            //U[qIndex] = GC.AllocateUninitializedArray<Complex>(bLen * bLen);
            for (int j = 0; j < bLen; j++)
            {
                for (int i = 0; i < bLen; i++)
                {
                    //局所形式
                    U[qIndex * bLen2 + j * bLen + i] = getU(AccVoltage, qList[qIndex] + Beams[i] - Beams[j], null, detAngleInner, detAngleOuter).Imag.Conjugate();//共役とると、なぜかいい感じ。
                    //非局所形式
                    //U[qIndex * bLen2 + j * bLen + i] = getU(AccVoltage, qList[qIndex], -Beams[i] + Beams[j], detAngleInner, detAngleOuter).Imag.Conjugate();
                    //U[m][k++] = getU(AccVoltage, qList[m], -Beams[i] + Beams[j], detAngleInner, detAngleOuter).Imag;//非局所形式の場合
                }
                if (Interlocked.Increment(ref count) % 10 == 0) bwSTEM.ReportProgress((int)(1E6 * count / qList.Count / bLen), "Calculating U matrix");//状況を報告
                if (bwSTEM.CancellationPending) { e.Cancel = true; return; }
            }
        });
        #endregion

        var PiecewiseQuadrature = true;
        if (PiecewiseQuadrature)
        #region 区分求積法アルゴリズム
        {
            bwSTEM.ReportProgress(0, "Calculating I_inelastic(Q)");

            #region 各厚みを、指定された厚み程度で切り分ける
            var _thick = new double[Thicknesses.Length][];
            var tStep = new double[Thicknesses.Length];
            for (int t = 0; t < Thicknesses.Length; t++)
            {
                var start = t == 0 ? 0 : Thicknesses[t - 1];
                var slices = Math.Max(1, (int)((Thicknesses[t] - start) / sliceThickness));
                tStep[t] = (Thicknesses[t] - start) / slices;
                _thick[t] = Enumerable.Range(1, slices).Select(e => start + tStep[t] * e).ToArray();
            }
            #endregion

            #region あらかじめeVecにαを掛けておく。
            Parallel.For(0, tc.Length, kIndex =>
            {
                if (eVal[kIndex] != null)
                    for (int col = 0; col < bLen; col++)
                        for (int row = 0; row < bLen; row++)
                            eVec[kIndex][col * bLen + row] *= α[kIndex][col];
            });
            #endregion

            #region 各種変数の設定
            var tc_k = GC.AllocateUninitializedArray<Complex>(tc.Length * bLen);
            var validTc = list.Where(e1 => e1 != null).SelectMany(e2 => e2.SelectMany(e3 => e3.N)).Distinct().ToList().AsParallel();
            var total = _thick.Sum(e => e.Length) * tcP.Count();
            count = 0;
            #endregion

            #region メインのループ
            for (int t = 0; t < Thicknesses.Length; t++)
            {
                var sum = new Complex[qList.Count * dLen];//ゼロ初期化が必要
                foreach (var thickness in _thick[t])
                {
                    if (bwSTEM.CancellationPending) return;

                    #region まず厚み_thick[t][_t]における透過係数_tc_kを計算
                    validTc.ForAll(kIndex =>
                    {
                        #region この内容をNativeコードで実行
                        //Complex[] exp_kgz = new Complex[bLen], exp_λ = new Complex[bLen];
                        //for (int i = 0; i < bLen; i++)
                        //{
                        //    exp_kgz[i] = Exp(TwoPiI * kg_z[kIndex][i] * thickness);
                        //    exp_λ[i] = Exp(TwoPiI * eVal[kIndex][i] * thickness);
                        //    tc_k[kIndex][i] = 0;
                        //}

                        //for (int g = 0; g < bLen; g++)
                        //    for (int j = 0; j < bLen; j++)
                        //        tc_k[kIndex][g] += eVec[kIndex][j * bLen + g] * exp_kgz[g] * exp_λ[j];
                        #endregion
                        fixed (Complex* _tc_k = tc_k, _eVal = eVal[kIndex], _eVec = eVec[kIndex])
                        fixed (double* _kg_z = kg_z[kIndex])
                            NativeWrapper.GenerateTC1(bLen, thickness, _kg_z, _eVal, _eVec, _tc_k + kIndex * bLen);
                    });
                    #endregion

                    tcP.ForAll(kIndex =>
                    {
                        Complex[] sumTmp = Shared.Rent(list[kIndex].Count * dLen), tc_kq = Shared.Rent(bLen);
                        try
                        {
                            fixed (Complex* _tc_k = tc_k, _U = U, _tc_kq = tc_kq)
                                for (int i = 0; i < list[kIndex].Count; i++)
                                {
                                    var (qIndex, n, r, lenz) = list[kIndex][i];
                                    //厚み_thick[t][_t]における透過係数_tc_kqを計算
                                    NativeWrapper.BlendAndConjugate(bLen, _tc_k + n[0] * bLen, _tc_k + n[1] * bLen, _tc_k + n[2] * bLen, _tc_k + n[3] * bLen, r[0], r[1], r[2], r[3], _tc_kq);

                                    var tmp = NativeWrapper.RowVec_SqMat_ColVec(bLen, _tc_kq, _U + qIndex * bLen2, _tc_k + kIndex * bLen);

                                    for (int dIndex = 0; dIndex < dLen; dIndex++)
                                        sumTmp[i * dLen + dIndex] = tmp * lenz[dIndex];
                                }
                            lock (lockObj1)
                                for (int i = 0; i < list[kIndex].Count; i++)
                                    for (int d = 0; d < dLen; d++)
                                        sum[list[kIndex][i].qIndex * dLen + d] += sumTmp[i * dLen + d];
                        }
                        finally { Shared.Return(sumTmp); Shared.Return(tc_kq); }

                        if (Interlocked.Increment(ref count) % 1000 == 0) bwSTEM.ReportProgress((int)(1E6 / total * count), "Calculating I_inelastic(Q)");//状況を報告
                    });

                }

                var coeff = 2 * Math.PI / kvac * tStep[t];
                Parallel.For(0, qList.Count, qIndex =>
                {
                    for (int dIndex = 0; dIndex < dLen; dIndex++)
                    {
                        I_Inel[qIndex, t, dIndex] = sum[qIndex * dLen + dIndex] * coeff;
                        if (t > 0)
                            I_Inel[qIndex, t, dIndex] += I_Inel[qIndex, t - 1, dIndex];
                    }
                });
            }

            #endregion
            #endregion

            #region 解析的に非弾性を計算する場合 
            //else
            //{
            //    //ZOLZのみだったらこれでいいが、HOLZや晶帯軸から傾いている場合に対応できていない。
            //    //対応しようとするとものすごい計算コスト。あきらめるか。

            //    //固有値・ベクトルをブレンドするのではなく、最後にブレンドする。

            //    //最初にeVecにαを掛けておく
            //    Parallel.For(0, tc.Length, kIndex =>
            //    {
            //        if (eVal[kIndex] != null)
            //            for (int col = 0; col < bLen; col++)
            //                for (int row = 0; row < bLen; row++)
            //                    eVec[kIndex][col * bLen + row] *= α[kIndex][col];
            //    });

            //    //複素共役なC, λ, αを用意
            //    Complex[][] C = eVec, _C = new Complex[tc.Length][], λ = eVal, _λ = new Complex[tc.Length][];
            //    Complex[][] exp = new Complex[tc.Length][], _exp = new Complex[tc.Length][];
            //    list.AsParallel().Where(e1 => e1 != null).SelectMany(e2 => e2.SelectMany(e3 => e3.N)).Distinct().ForAll(kIndex =>
            //    {
            //        _C[kIndex] = (new DMat(bLen, bLen, C[kIndex]).ConjugateTranspose() as DMat).Values;
            //        _λ[kIndex] = (new DVec(λ[kIndex]).Conjugate() as DVec).Values;

            //        exp[kIndex] = new Complex[tLen * bLen];
            //        _exp[kIndex] = new Complex[tLen * bLen];
            //        for (int t = 0; t < tLen; t++)
            //            for (int j = 0; j < bLen; j++)
            //            {
            //                exp[kIndex][t * bLen + j] = Exp(TwoPiI * (λ[kIndex][j] + kg_z[kIndex][j]) * Thicknesses[t]);
            //                _exp[kIndex][t * bLen + j] = exp[kIndex][t * bLen + j].Conjugate();
            //            }
            //    });


            //    var total = tcP.Count();
            //    count = 0;
            //    tcP.ForAll(kIndex =>
            //    {
            //        if (bwSTEM.CancellationPending) return;

            //        Complex[] λ_k = λ[kIndex], exp_k = exp[kIndex];
            //        double[] kz_k = kg_z[kIndex];

            //        Complex[] TDS = Shared.Rent(bLen * bLen), tmpMat = Shared.Rent(bLen * bLen);
            //        try
            //        {
            //            foreach (var (qIndex, n, r, lenz) in list[kIndex])
            //            {
            //                NativeWrapper.MultiplyMxM(bLen, U[qIndex], C[kIndex], ref tmpMat);
            //                var tmpSum = new Complex[tLen];
            //                for (int m = 0; m < n.Length; m++)
            //                {
            //                    NativeWrapper.MultiplyMxM(bLen, _C[n[m]], tmpMat, ref TDS);

            //                    Complex[] λ_kq = _λ[n[m]], exp_kq = _exp[n[m]];
            //                    double[] kz_kq = kg_z[n[m]];

            //                    //B行列の中身を計算し、アダマール積を取る
            //                    for (int t = 0; t < tLen; t++)
            //                    {
            //                        int l = 0;
            //                        for (int j = 0; j < bLen; j++)
            //                            for (int i = 0; i < bLen; i++)
            //                                tmpSum[t] += r[m] * ((exp_k[t * bLen + j] * exp_kq[t * bLen + i] - 1) / TwoPiI / (kz_k[j] - kz_kq[i] + λ_k[j] - λ_kq[i])) * TDS[l++];//B行列は作らず、直接アダマール積を取る
            //                    }
            //                }
            //                lock (lockObj2)
            //                    for (int t = 0; t < tLen; t++)
            //                        for (int d = 0; d < dLen; d++)
            //                            I_Inel[qIndex, t, d] += tmpSum[t] / kvac * lenz[d];
            //            }
            //        }
            //        finally { Shared.Return(TDS); Shared.Return(tmpMat); }
            //        if (Interlocked.Increment(ref count) % 10 == 0) bwSTEM.ReportProgress((int)(1000000.0 / total * count), "Calculating I_inelastic(Q)");//状況を報告
            //    });

            //}
            #endregion
        }
        if (bwSTEM.CancellationPending) { e.Cancel = true; return; }
        #endregion

        #region 各ピクセルの計算
        //imagesを初期化
        int width = imageSize.Width, height = imageSize.Height;
        var image_ela = Thicknesses.Select(e => defocusses.Select(e2 => GC.AllocateUninitializedArray<double>(width * height)).ToArray()).ToArray();
        var image_tds = Thicknesses.Select(e => defocusses.Select(e2 => GC.AllocateUninitializedArray<double>(width * height)).ToArray()).ToArray();

        double cX = width / 2.0, cY = height / 2.0, radiusPix2 = radiusPix * radiusPix;
        var shift = (Crystal.RotationMatrix * (Crystal.A_Axis + Crystal.B_Axis + Crystal.C_Axis) / 2).ToPointD;
        Parallel.For(0, height, y =>
        {
            for (int x = 0; x < width; x++)
            {
                var rVec = new PointD(-resolution * (x - cX), resolution * (y - cY)) + shift;//X座標はマイナス。
                for (int t = 0; t < Thicknesses.Length; t++)
                    for (int d = 0; d < dLen; d++)
                    {
                        Complex elas = new(), tds = new();
                        for (int qIndex = 0; qIndex < qList.Count; qIndex++)
                        {
                            var tmp = Exp(qList[qIndex].Vec.ToPointD * rVec * TwoPiI);
                            elas += I_Elas[qIndex, t, d] * tmp;
                            tds += I_Inel[qIndex, t, d] * tmp;
                        }
                        image_ela[t][d][x + y * width] = elas.Magnitude / radiusPix2;
                        image_tds[t][d][x + y * width] = tds.Magnitude / radiusPix2;
                    }
            }
        });

        //ガウスブラーを適用
        if (sourceSize > 0)
            for (int t = 0; t < Thicknesses.Length; t++)
                for (int d = 0; d < dLen; d++)
                {
                    ImageProcess.GaussianBlurFast(ref image_ela[t][d], width, sourceSize / resolution);
                    ImageProcess.GaussianBlurFast(ref image_tds[t][d], width, sourceSize / resolution);
                }

        var image_both = Thicknesses.Select(e => defocusses.Select(e2 => GC.AllocateUninitializedArray<double>(width * height)).ToArray()).ToArray();
        for (int t = 0; t < Thicknesses.Length; t++)
            for (int d = 0; d < dLen; d++)
                for (int i = 0; i < width * height; i++)
                    image_both[t][d][i] = image_ela[t][d][i] + image_tds[t][d][i];

        ResultSTEM = (new Size(width, height), resolution, thicknesses.ToArray(), defocusses.ToArray(), BaseRotation, image_both, image_ela, image_tds);

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
    /// 
    /// </summary>
    /// <param name="BlochNum">ブロッホ波の数</param>
    /// <param name="AccVol">加速電圧(kv)</param>
    /// <param name="rot"></param>
    /// <param name="aperture">対物絞りの半頂角 (mrad), シフト</param>
    /// <param name="size">イメージのサイズ</param>
    /// <param name="res">イメージの解像度 (nm/pix)</param>
    /// <param name="cs">球面収差 (nm)</param>
    /// <param name="beta">Illumination semiangle (単位は rad)</param>
    /// <param name="delta">Cc * ΔV/V 単位は nm</param>
    /// <param name="thicknesses">配列で与える. 単位はnm</param>
    /// <param name="defocusses">配列で与える. 単位はnm</param>
    /// <param name="quasiMode">trueの時はQuasi-coherent mode, falseの時はTransmission cross coefficient</param>
    /// <param name="native"></param>
    public void GetHRTEMImage(int BlochNum, double AccVol, Matrix3D rot, (double R, double X, double Y) aperture, Size size, double res, double cs, double beta, double delta, double[] thicknesses, double[] defocusses,
                                   bool quasiMode = true, bool native = true)
    {
        int width = size.Width, height = size.Height;
        double rambda = UniversalConstants.Convert.EnergyToElectronWaveLength(AccVoltage), rambdaSq = rambda * rambda;
        double deltaSq = delta * delta, betaSq = beta * beta;
        var k = new PointD(0, 0);//入射ベクトルKのXY成分
        var defLen = defocusses.Length;
        var tLen = thicknesses.Length;

        var _images = new double[tLen][][];
        for (int t = 0; t < tLen; t++)
        {
            var beams = GetDifractedBeamAmpriltudes(BlochNum, AccVol, rot, thicknesses[t]);//2回目以降は固有値計算済みなので高速
            beams = ExtractInsideBeams(beams, AccVol, aperture.R, aperture.X, aperture.Y);

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
            _images[t] = defocusses.Select(d => new double[width * height]).ToArray();

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
                    var rVec = Enumerable.Range(start, count).SelectMany(n => new[] { -res * (n % width - cX) + shift.X, res * (n / width - cY) + shift.Y }).ToArray();//X座標はマイナス。
                    var results = NativeWrapper.HRTEM_Solver(gPsi, gVec, gLenz, rVec, quasiMode);
                    for (var i = 0; i < defLen; i++)
                        Array.Copy(results, i * count, _images[t][i], start, count);
                });
            }
            else//Managed
            {
                Parallel.For(0, width * height, n =>
                {
                    PointD r = new(-(n % width - cX) * res + shift.X, (n / width - cY) * res + shift.Y), _vec = new(double.NaN, double.NaN);//X座標はマイナス。
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
                        _images[t][i][n] = quasiMode ? sums[i].MagnitudeSquared() : Math.Abs(sums[i].Real);
                });
            }
            //return images;
        }

        ResultHRTEM = (new Size(width, height), res, thicknesses.ToArray(), defocusses.ToArray(), new Matrix3D(rot), _images);
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
        //if (!uDictionary.TryGetValue((key1, key2), out (Complex real, Complex imag) U))
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

                var dsf = atoms.Dsf;
                var zero = dsf.IsZero;
                double imag = zero ? 0 : double.NaN, m = zero ? 0 : double.NaN;
                foreach (var atom in atoms.Atom)
                {
                    if (!zero)
                    {
                        if ((!dsf.UseIso && index != (0, 0, 0)) || double.IsNaN(imag))//非等方でg≠0の時、あるいは初めての時
                        {
                            if (dsf.UseIso)
                                m = dsf.Biso;
                            else if (index == (0, 0, 0))
                                m = double.IsNaN(dsf.Biso) ? dsf.Biso000 : dsf.Biso;
                            else
                            {
                                var (H, K, L) = atom.Operation.ConvertPlaneIndex(index);
                                m = (dsf.B11 * H * H + dsf.B22 * K * K + dsf.B33 * L * L + 2 * dsf.B12 * H * K + 2 * dsf.B23 * K * L + 2 * dsf.B31 * L * H) / s2;
                            }
                            if (double.IsNaN(m))
                                m = 0;

                            imag = m == 0 ? 0 : (double.IsNaN(inner * outer)) ? es.FactorImaginary(kV, s2, m) :
                                h == null ? es.FactorImaginaryAnnular(kV, g.Vec, m, inner, outer) : es.FactorImaginaryAnnular(kV, g.Vec, h.Vec, m, inner, outer);//非弾性散乱因子 答えは無次元
                        }
                    }
                    var d = Exp(-m * s2 + TwoPiI * (atom * index)) * atoms.Occ; //20240524 位相項 (TwoPiI・・・)の符号をプラスに変更 (これで、対称心の結晶の計算が上手くいくはず 三菱・中村)
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

    private readonly Dictionary<(int Key1, int Key2), (Complex Real, Complex Imag)> uDictionary = [];
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
    static readonly FrozenSet<(int h, int k, int l)> directionF = new[] { (1, 1, 1), (1, 1, -1), (1, -1, 1), (1, -1, -1), (-1, 1, 1), (-1, 1, -1), (-1, -1, 1), (-1, -1, -1) }.ToFrozenSet();
    static readonly FrozenSet<(int h, int k, int l)> directionA = new[] { (0, 1, 1), (0, 1, -1), (0, -1, 1), (0, -1, -1), (1, 0, 0), (-1, 0, 0) }.ToFrozenSet();
    static readonly FrozenSet<(int h, int k, int l)> directionB = new[] { (1, 0, 1), (1, 0, -1), (-1, 0, 1), (-1, 0, -1), (0, 1, 0), (0, -1, 0) }.ToFrozenSet();
    static readonly FrozenSet<(int h, int k, int l)> directionC = new[] { (1, 1, 0), (1, -1, 0), (-1, 1, 0), (-1, -1, 0), (0, 0, 1), (0, 0, -1) }.ToFrozenSet();
    static readonly FrozenSet<(int h, int k, int l)> directionI = new[] { (1, 1, 0), (1, -1, 0), (-1, 1, 0), (-1, -1, 0), (0, 1, 1), (0, 1, -1), (0, -1, 1), (0, -1, -1), (1, 0, 1), (1, 0, -1), (-1, 0, 1), (-1, 0, -1) }.ToFrozenSet();
    static readonly FrozenSet<(int h, int k, int l)> directionRH = new[] { (1, 0, 1), (0, -1, 1), (-1, 1, 1), (-1, 0, -1), (0, 1, -1), (1, -1, -1) }.ToFrozenSet();
    static readonly FrozenSet<(int h, int k, int l)> directionHex = new[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (1, -1, 0), (-1, 1, 0), (0, 0, 1), (0, 0, -1) }.ToFrozenSet();
    static readonly FrozenSet<(int h, int k, int l)> directionP = new[] { (1, 0, 0), (-1, 0, 0), (0, 1, 0), (0, -1, 0), (0, 0, 1), (0, 0, -1) }.ToFrozenSet();

    readonly Dictionary<(int H, int K, int L), Vector3DBase> gDic = [];
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
        FrozenSet<(int h, int k, int l)> direction;
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

            foreach (var (key, gLen) in CollectionsMarshal.AsSpan(outer)[..(end + 1)])
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

        return [.. beams];
    }

    #endregion

    #region 絞りの内部にあるビームのみ選び取る (HRTEM シミュレータから呼ばれる)
    public static Beam[] ExtractInsideBeams(Beam[] beams, double acc, double radius, double shiftX, double shiftY)
    {
        if (double.IsInfinity(radius))
            return [.. beams];
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
    public class CBED_Disk(int[] hkl, Vector3DBase vec, double thickness, Complex[] amplitudes)
    {
        /// <summary>
        /// 指数
        /// </summary>
        public readonly int H = hkl[0], K = hkl[1], L = hkl[2];

        /// <summary>
        /// 厚み
        /// </summary>
        public readonly double Thickness = thickness;

        public readonly Vector3DBase G = vec;

        /// <summary>
        /// 振幅を格納した配列
        /// </summary>
        public Complex[] Amplitudes;

        public readonly Complex[] RawAmplitudes = amplitudes;
    }
    #endregion

}
