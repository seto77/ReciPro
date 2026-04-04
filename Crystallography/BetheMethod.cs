#region using
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using System;
using System.Buffers;
using System.Collections.Concurrent;
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
using ZLinq;
using static System.Buffers.ArrayPool<System.Numerics.Complex>;
using static System.Numerics.Complex;
using DMat = MathNet.Numerics.LinearAlgebra.Complex.DenseMatrix;
using DVec = MathNet.Numerics.LinearAlgebra.Complex.DenseVector;
#endregion

namespace Crystallography;

/// <summary>Bethe法による動力学計算を提供するクラス。すべて、単位はnm</summary>
[Serializable]
public class BetheMethod
{
    #region static readonly field
    private static readonly Complex One = Complex.One;
    private const double TwoPi = Constants.Pi2;
    private static readonly Complex TwoPiI = TwoPi * ImaginaryOne;
    private static readonly Complex PiI = Math.PI * ImaginaryOne;
    private const double PiSq = Math.PI * Math.PI;
    // a1 * b1 + a2 * b2 + a3 * b3
    private static double Dot3(double a1, double b1, double a2, double b2, double a3, double b3)
        => Math.FusedMultiplyAdd(a1, b1, Math.FusedMultiplyAdd(a2, b2, a3 * b3));
    /// <summary>(001)ベクトル</summary>
    private static readonly Vector3DBase zNorm = new(0, 0, 1);
    public static bool EigenEnabled, MklEnabled, BlasEnabled, CudaEnabled;

    public static readonly int ProcessorCount = Environment.ProcessorCount;
    #endregion

    #region フィールド、プロパティ

    /// <summary>加速電圧 単位はkV</summary>
    private double AccVoltage { get; set; }
    private Crystal Crystal { get; }
    /// <summary>結晶の方位</summary>
    private Matrix3D BaseRotation { get; set; } = null;
    public double AlphaMax { get; set; }
    public double Cs { get; set; }
    public double Defocus { get; set; }
    public Vector3DBase[] BeamDirections { get; set; }
    public int RotationArrayValidLength { get; set; } = 0;

    /// <summary>サンプル表面(から内部への)の法線単位ベクトル. ReciProの座標系は、画面右が+X、上が+Y,手前が+Zなので、初期値は(0,0,-1)</summary>==null
    public Vector3D Surface { get; set; } = new Vector3D(0, 0, -1);
    public int MaxNumOfBloch { get; set; }
    public double Thickness { get; set; }
    public double[] Thicknesses { get; set; }

    // 260318Cl 追加: BFS 探索結果のキャッシュ (baseRotation が同じなら再利用)
    private (int key, double gX, double gY, double gZ, double gLen)[] gCache;
    private (double, double, double, double, double, double, double, double, double) matCache;

    // 260316Cl 追加
    /// <summary>
    /// EBSD計算で非局所形式の吸収ポテンシャルを使用するかどうか。
    /// true の場合、U'(g, g') が g と g' の両方に依存する非局所形式で計算される。
    /// false の場合、U'(g-g') のみに依存する従来の局所形式で計算される。
    /// 重い元素を含む結晶では非局所形式のほうが実験パターンとの一致が良い。
    /// ただし計算コストは大幅に増加する（各行列要素で2D数値積分が必要）。
    /// </summary>
    public bool UseNonLocalAbsorption { get; set; }

    // 260316Cl 追加
    /// <summary>
    /// EBSD計算で TDS バックグラウンドを含めるかどうか。
    /// true の場合、Bloch波から TDS により失われた電子がインコヒーレントに
    /// 後方脱出する効果を滑らかなバックグラウンドとして加算する。
    /// 重い元素ではバックグラウンドが相対的に大きく、菊池バンドのコントラストが低下する。
    /// </summary>
    public bool IncludeTDSBackground { get; set; }

    /// <summary>
    /// EBSD 計算で、近傍方向ごとに Find_gVectors の事前計算結果を共有するブロックサイズ。
    /// 通常の detector pattern では 3 程度が高速だが、MasterPattern のように球面対称性を
    /// 直接見る用途では 1 にして近似を切るほうが安全。
    /// </summary>
    public int EbsdRepresentativeDirectionBlockSize { get; set; } = 3; // (260321Ch)

    /// <summary>
    /// true のとき、EBSD 計算の各方向について局所的な表面法線をその方向から決める。
    /// 固定された平面表面に対する EBSD では false のまま使い、
    /// 結晶固定の MasterPattern では true にして「方向ごとに接球面を持つ」近似へ切り替える。
    /// </summary>
    public bool UseLocalSurfacePerBeamDirection { get; set; } = false; // (260321Ch)
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

    public bool IsCBED_Busy => bwCBED is null || bwCBED.IsBusy;
    public bool IsSTEM_Busy => bwSTEM is null || bwSTEM.IsBusy;
    #region お蔵入り // (260327Ch) 旧 bwEBSD の状態参照は ebsdNew 本命化に伴い退避
    //public bool IsEBSD_Busy => bwEBSD is null || bwEBSD.IsBusy;
    #endregion
    public bool IsEBSDNew_Busy => bwEBSDNew is null || bwEBSDNew.IsBusy; // (260327Ch) 以後はこちらを EBSD の本命 worker の稼働状態として使う

    /// <summary>CBEDのディスク情報 Disks[Z(thickness)_index][G_index], EBSDのときは [Voltage][Z(thickness)_index]</summary>
    [XmlIgnore]
    public CBED_Disk[][] Disks { get; set; }

    [NonSerialized]
    public Beam[] Beams;

    [NonSerialized]
    public readonly BackgroundWorker bwCBED = new();
    public event ProgressChangedEventHandler CBED_ProgressChanged;
    public event RunWorkerCompletedEventHandler CBED_Completed;

    #region お蔵入り // (260327Ch) 旧 EBSD worker は残すがコメントアウトして退避
    //[NonSerialized]
    //public readonly BackgroundWorker bwEBSD = new();
    #endregion
    [NonSerialized]
    public readonly BackgroundWorker bwEBSDNew = new(); // (260327Ch) 旧 bwEBSD の後継としてこちらを EBSD の本命 worker にする
    public event ProgressChangedEventHandler EBSD_ProgressChanged;
    public event RunWorkerCompletedEventHandler EBSD_Completed;

    [NonSerialized]
    public readonly BackgroundWorker bwSTEM = new();
    public event ProgressChangedEventHandler StemProgressChanged;
    public event RunWorkerCompletedEventHandler StemCompleted;

    private readonly Lock lockObj1 = new(), lockObj2 = new();

    /// <summary>Result_STEM_Ela[thickness][defocus]</summary>
    public (Size Size, double Resolution, double[] Thicknesses, double[] Defocusses, Matrix3D rot, double[][][] ImageBoth, double[][][] ImageEla, double[][][] ImageTDS) ResultSTEM;

    /// <summary>Result_STEM_TDS[thickness][defocus]</summary>
    public (Size Size, double Resolution, double[] Thicknesses, double[] Defocusses, Matrix3D rot, double[][][] Image) ResultHRTEM;

    /// <summary></summary>
    public (int Width, int Height, double Resolution, double[][][] Image) Result_Potential;

    #endregion

    #region コンストラクタ

    static BetheMethod()
    {
        EigenEnabled = NativeWrapper.Enabled;
        BlasEnabled = MathNet.Numerics.Control.TryUseNativeOpenBLAS();
        MklEnabled = MathNet.Numerics.Control.TryUseNativeMKL();
        CudaEnabled = MathNet.Numerics.Control.TryUseNativeCUDA();
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

        #region お蔵入り
        //bwEBSD = new BackgroundWorker
        //{
        //    WorkerSupportsCancellation = true,
        //    WorkerReportsProgress = true
        //};
        //bwEBSD.RunWorkerCompleted += Ebsd_RunWorkerCompleted;
        //bwEBSD.ProgressChanged += Ebsd_ProgressChanged;
        //bwEBSD.DoWork += ebsd_DoWork;
        #endregion

        bwEBSDNew = new BackgroundWorker
        {
            WorkerSupportsCancellation = true,
            WorkerReportsProgress = true
        };
        bwEBSDNew.RunWorkerCompleted += Ebsd_RunWorkerCompleted;
        bwEBSDNew.ProgressChanged += Ebsd_ProgressChanged;
        bwEBSDNew.DoWork += ebsdNew_DoWork; // (260327Ch) ebsdNew_DoWork を EBSD の本命 worker として配線する

        bwSTEM = new BackgroundWorker
        {
            WorkerSupportsCancellation = true,
            WorkerReportsProgress = true,
        };
        bwSTEM.RunWorkerCompleted += Stem_RunWorkerCompleted;
        bwSTEM.ProgressChanged += Stem_ProgressChanged;
        bwSTEM.DoWork += StemDoWork;
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

    /// <summary></summary>
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

    /// <summary>CBED計算</summary>
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
        var psi0 = new DVec([.. ValueEnumerable.Range(0, Beams.Length).Select(g => g == 0 ? One : 0)]);
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
        int progressStep = Beams.Length switch
        {
            < 100 => 400,
            < 200 => 200,
            < 300 => 50,
            _ => 5
        };

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
                        var gammmaAlpha = new DVec([.. evd.EigenValues.Select((ev, i) => Exp(TwoPiI * ev * Thicknesses[t]) * alpha[i])]);
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

                if (Interlocked.Increment(ref count) % progressStep == 0)
                    bwCBED.ReportProgress(count, reportString);//進捗状況を報告

                return result;
            }
            finally { Shared.Return(eigenMatrix); ArrayPool<Beam>.Shared.Return(beams); }
        }).ToArray();

        //無効なRotationも再び組み込んでdisk[RotationIndex][Z_index][G_index]を構築
        var diskAmplitude = new Complex[BeamDirections.Length][];
        for (int i = 0, j = 0; i < BeamDirections.Length; i++)
            diskAmplitude[i] = inside(i) ? diskAmplitudeValid[j++] : null;//有効(円内)のピクセルを追加し、無効なものにはnull

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
                    if (diskAmplitude[r] is not null)
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
                        if (diskAmplitude[r] is not null)
                            amplitudes[r] = diskAmplitude[r][t * bLen + g];

                    Disks[t][g] = new CBED_Disk([Beams[g].H, Beams[g].K, Beams[g].L], Beams[g].Vec, Thicknesses[t], amplitudes);

                }
            });

            //ここから、diskの重なり合いを計算
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
            Parallel.For(0, Beams.Length, g1 =>  //for(int g1=0; g1<Beams.Length;g1++)
            {
                if (!bwCBED.CancellationPending)
                {
                    var intensities = new double[Thicknesses.Length][];
                    for (int t = 0; t < Thicknesses.Length; t++)
                        intensities[t] = [.. Disks[t][g1].RawAmplitudes.Select(a => a.MagnitudeSquared())];

                    for (int r1 = 0; r1 < BeamDirections.Length; r1++)
                    {
                        if (Disks[0][g1].RawAmplitudes[r1] != 0)
                        {
                            var pos = diskTemp[g1].Pos[r1];
                            for (int g2 = 0; g2 < Beams.Length; g2++)
                                if (g2 != g1 && diskTemp[g2].Rect.IsInside(pos))
                                {
                                    var r2 = getIndex(pos.X,pos.Y, diskTemp[g2].Pos, width);
                                    if (r2 >= 0 && Disks[0][g2].RawAmplitudes[r2] != 0)
                                        for (int t = 0; t < Thicknesses.Length; t++)
                                            intensities[t][r1] += Disks[t][g2].RawAmplitudes[r2].MagnitudeSquared();
                                }
                        }
                    }

                    for (int t = 0; t < Thicknesses.Length; t++)
                        Disks[t][g1].Amplitudes = [.. intensities[t].Select(intensity => new Complex(Math.Sqrt(intensity), 0))];
                }
                bwCBED.ReportProgress(Interlocked.Increment(ref count) * 1000 / Beams.Length, "Compiling disks");//進捗状況を報告
            });
        }

        if (bwCBED.CancellationPending)
            e.Cancel = true;
    }

    private static readonly int[] pow = [4, 1]; 
    //与えられたposに最も近いインデックスを返す
    static int getIndex(in double x, double y, in PointD[] pts, int w)
    {
        var w2 = (uint)(w * w);
        int i = (int)w2 / 2, m;
        double min = (x - pts[i].X) * (x - pts[i].X) + (y - pts[i].Y) * (y - pts[i].Y), temp;

        bool flag;
        //中心から、縦横に検索
        foreach (var n in pow)
            do
            {
                flag = false;

                if (((uint)(m = i + n * w) < w2 && (temp = (x - pts[m].X) * (x - pts[m].X) + (y - pts[m].Y) * (y - pts[m].Y)) < min) ||
                   ((uint)(m = i - n * w) < w2 && (temp = (x - pts[m].X) * (x - pts[m].X) + (y - pts[m].Y) * (y - pts[m].Y)) < min))
                {
                    i = m; min = temp; flag = true;
                }

                if (((uint)(m = i + n) < w2 && (temp = (x - pts[m].X) * (x - pts[m].X) + (y - pts[m].Y) * (y - pts[m].Y)) < min) ||
                    ((uint)(m = i - n) < w2 && (temp = (x - pts[m].X) * (x - pts[m].X) + (y - pts[m].Y) * (y - pts[m].Y)) < min))
                {
                    i = m; min = temp; flag = true;
                }

            } while (flag);

        return i / w == 0 || i / w == w - 1 || i % w == 0 || i % w == w - 1 ? -1 : i;
    }
    #endregion

    #region EBSD
    private void Ebsd_ProgressChanged(object sender, ProgressChangedEventArgs e) => EBSD_ProgressChanged?.Invoke(sender, e);

    private void Ebsd_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) => EBSD_Completed?.Invoke(sender, e);

    #region お蔵入り // (260327Ch) 旧 bwEBSD 起動・停止 API は ebsdNew 本命化に伴い退避
    //public void CancelEBSD()
    //{
    //    if (bwEBSD.IsBusy)
    //        bwEBSD.CancelAsync();
    //}
    #endregion

    /// <summary>
    /// EBSD 計算を開始する本命 API。
    /// 旧 RunEBSD はお蔵入りとし、以後はこの worker を使う。
    /// </summary>
    /// <param name="maxNumOfBloch"></param>
    /// <param name="voltage">加速電圧(kV)</param>
    /// <param name="rotation">基準となる方位</param>
    /// <param name="thickness">厚みの配列</param>
    /// <param name="beamRotations">基準となる方位に乗算する方位配列</param>
    // 260316Cl useNonLocalAbsorption, includeTDSBackground 引数を追加
    #region お蔵入り // (260327Ch) 旧 RunEBSD は旧 worker 前提なのでコメントアウトして残す
    //public void RunEBSD(int maxNumOfBloch, double[] voltages, Matrix3D rotation, double[] thickness, Vector3DBase[] beamDirections, Solver solver = Solver.Auto, int thread = 1, bool useNonLocalAbsorption = false, bool includeTDSBackground = false)
    //{
    //    MaxNumOfBloch = maxNumOfBloch;
    //    UseNonLocalAbsorption = useNonLocalAbsorption;
    //    IncludeTDSBackground = includeTDSBackground;
    //
    //    BaseRotation = new Matrix3D(rotation);
    //    BeamDirections = beamDirections;
    //    Thicknesses = thickness;
    //
    //    bwEBSD.RunWorkerAsync((solver, thread, voltages));
    //}
    #endregion

    /// <summary>
    /// crystal-fixed master 向けの EBSD 計算を開始する。
    /// ebsdNew が本命になったので、以後はこちらを使う。
    /// </summary>
    public void RunEBSDNew(int maxNumOfBloch, double[] voltages, Matrix3D rotation, double[] thickness, Vector3DBase[] beamDirections, Solver solver = Solver.Auto, int thread = 1, bool useNonLocalAbsorption = false, bool includeTDSBackground = false)
    {
        MaxNumOfBloch = maxNumOfBloch;
        UseNonLocalAbsorption = useNonLocalAbsorption;
        IncludeTDSBackground = includeTDSBackground;

        BaseRotation = new Matrix3D(rotation);
        BeamDirections = beamDirections;
        Thicknesses = thickness;

        bwEBSDNew.RunWorkerAsync((solver, thread, voltages)); // (260321Ch) 新アルゴリズムは専用 worker で動かす
    }

    /// <summary>
    /// 現在の EBSD 計算で使う表面法線を返す。
    /// 通常の EBSD では固定 Surface を返し、MasterPattern 用の局所表面モードでは
    /// 各方向に対して接球面の内向き法線 -beamDirection を返す。
    /// </summary>
    private Vector3DBase GetEbsdSurfaceNormal(Vector3DBase beamDirection)
    {
        if (!UseLocalSurfacePerBeamDirection || beamDirection == null)
            return Surface;

        // return Surface; // (260321Ch) 旧案: すべての方向で同じ平面表面法線を使っていた
        return Vector3DBase.Normarize(-beamDirection); // (260321Ch) 結晶固定 master では各方向の局所法線を使う
    }

    /// <summary>
    /// crystal-fixed master pattern の 1 方向を、既存の固定表面 EBSD で解ける
    /// 等価な beam / surface 条件へ変換する。
    /// 結晶側を回す代わりに、逆回転を beam / surface 側へ押し戻すことで
    /// BaseRotation を固定し、Find_gVectors の gCache を再利用できるようにする。
    /// </summary>
    private static (Vector3DBase BeamDirection, Vector3DBase Surface) GetCrystalFixedMasterEquivalentBeamAndSurface(
        Vector3DBase beamDirection, Vector3DBase referenceSurface, Vector3DBase referenceBeamDirection, Vector3DBase referenceAxisU, Vector3DBase referenceAxisV)
    {
        var localSurface = beamDirection == null ? Vector3DBase.Normarize(referenceSurface) : Vector3DBase.Normarize(-beamDirection); // (260321Ch) exit direction に対する内向き法線
        var (localAxisU, localAxisV) = GetSurfaceTangentialAxes(localSurface);
        var crystalToReference = Geometry.GetRotation(localAxisU, localAxisV, referenceAxisU, referenceAxisV); // (260321Ch) 旧 GetCrystalFixedMasterRotation をここへ畳み込む
        var referenceToCrystal = crystalToReference.Transpose(); // (260321Ch) 回転行列なので転置を使って軽く戻す
        var equivalentSurface = Vector3DBase.Normarize(referenceToCrystal * referenceSurface);
        var equivalentBeamDirection = Vector3DBase.Normarize(referenceToCrystal * referenceBeamDirection);
        return (equivalentBeamDirection, equivalentSurface);
    }

    /// <summary>原子位置とビーム集合から、EBSD solver に渡す位相因子行列を column-major で作る。</summary>
    private Complex[] CreatePhaseFactors((double x, double y, double z, double sigma)[] atomArray, int nAtoms, Beam[] beams)
    {
        var beamCount = beams?.Length ?? 0;
        var phaseNG = Shared.Rent(nAtoms * beamCount); // (260321Ch) MasterPattern の代表方向前計算では ArrayPool を使って GC 負荷を下げる
        for (int n = 0; n < nAtoms; n++)
        {
            var (xn, yn, zn, _) = atomArray[n];
            for (int g = 0; g < beamCount; g++)
            {
                var (h, k, l) = beams[g].Index;
                var (sin, cos) = Math.SinCos(TwoPi * (h * xn + k * yn + l * zn)); // 260321Cl: 中間 Complex 不要・Math.SinCos 直接呼び出し
                phaseNG[g * nAtoms + n] = new Complex(cos, sin);
            }
        }
        return phaseNG; // (260321Ch)
    }

    /// <summary>1 つのビーム集合に対する後方散乱 TDS 行列を作る。</summary>
    private Complex[] CreateMasterPatternMuBack(double voltage, Beam[] beams)
    {
        var beamCount = beams?.Length ?? 0;
        // var muBack = new Complex[beamCount * beamCount]; // (260321Ch) 旧実装: 代表方向ごとに新規確保していた
        var muBack = Shared.Rent(beamCount * beamCount); // (260321Ch) TDS 行列も pooled buffer に載せる
        var localCache = new Dictionary<int, Complex>();
        for (int col = 0; col < beamCount; col++)
            for (int row = 0; row < beamCount; row++)
            {
                var key = compose(beams[row].H - beams[col].H, beams[row].K - beams[col].K, beams[row].L - beams[col].L);
                if (!localCache.TryGetValue(key, out var val))
                {
                    val = getU(voltage, beams[row] - beams[col], null, Math.PI / 2, Math.PI, 30, 12).Imag.Conjugate();
                    localCache[key] = val;
                }
                muBack[col * beamCount + row] = val;
            }
        return muBack; // (260321Ch)
    }

    /// <summary>MasterPattern 用のポテンシャル行列を ArrayPool から確保して構築する。</summary>
    private Complex[] RentMasterPatternPotentialMatrix(Beam[] beams)
    {
        var beamCount = beams?.Length ?? 0;
        var potentialMatrix = Shared.Rent(beamCount * beamCount); // (260321Ch)
        if (UseNonLocalAbsorption)
            getPotentialMatrix(beamCount, beams, ref potentialMatrix, 0, Math.PI); // (260321Ch)
        else
            getPotentialMatrix(beamCount, beams, ref potentialMatrix); // (260321Ch)
        return potentialMatrix;
    }

    /// <summary>MasterPattern 前計算で借りた Complex 配列を返却する。</summary>
    private static void ReturnMasterPatternBuffer(Complex[] buffer)
    {
        if (buffer != null)
            Shared.Return(buffer); // (260321Ch)
    }

    // 260331Cl SymmOper, GetMasterPatternSquareSymmetryOperations, TransformMasterPatternSquareIndex は EbsdMasterPattern へ移動

    /// <summary>
    /// 格子上の各方向を、その orbit 内で最小 index の exact representative へ写す。 // (260327Ch)
    /// 六方格子の場合、無効セル (|u+v| > N) は -1 にマッピングされスキップ対象となる。 // 260331Cl
    /// </summary>
    private int[] CreateMasterPatternSymmetryRepresentativeDirectionMapping(int beamDirectionCount, int hemisphereLength, int gridSize)
    {
        if (beamDirectionCount <= 0 || hemisphereLength <= 0 || gridSize <= 0)
            return [];

        bool isHex = MasterPattern.ShouldUseHexGrid(Crystal.Symmetry); // 260331Cl
        int N = isHex ? (gridSize - 1) / 2 : 0;

        var mapping = new int[beamDirectionCount];
        for (int i = 0; i < beamDirectionCount; i++)
            mapping[i] = i;

        // 260331Cl 六方格子の無効セルを -1 にマーク
        if (isHex)
        {
            for (int i = 0; i < beamDirectionCount; i++)
            {
                int localIndex = i % hemisphereLength;
                var (u, v) = MasterPattern.HexFromLinearIndex(localIndex, N);
                if (!MasterPattern.IsValidHexCell(u, v, N))
                    mapping[i] = -1;
            }
        }

        if (isHex)
        {
            var hexOps = MasterPattern.GetMasterPatternHexSymmetryOperations(Crystal.Symmetry);
            if (hexOps.Length <= 1)
                return mapping;

            for (int i = 0; i < beamDirectionCount; i++)
            {
                if (mapping[i] == -1) continue; // 無効セルはスキップ
                var representativeIndex = i;
                for (int opIndex = 1; opIndex < hexOps.Length; opIndex++)
                {
                    var transformedIndex = MasterPattern.TransformMasterPatternHexIndex(i, hemisphereLength, gridSize, hexOps[opIndex]);
                    if (transformedIndex < representativeIndex)
                        representativeIndex = transformedIndex;
                }
                mapping[i] = representativeIndex;
            }
        }
        else
        {
            var operations = MasterPattern.GetMasterPatternSquareSymmetryOperations(Crystal.Symmetry);
            if (operations.Length <= 1)
                return mapping;

            for (int i = 0; i < beamDirectionCount; i++)
            {
                var representativeIndex = i;
                for (int opIndex = 1; opIndex < operations.Length; opIndex++)
                {
                    var transformedIndex = MasterPattern.TransformMasterPatternSquareIndex(i, hemisphereLength, gridSize, operations[opIndex]);
                    if (transformedIndex < representativeIndex)
                        representativeIndex = transformedIndex;
                }
                mapping[i] = representativeIndex;
            }
        }
        return mapping;
    }

    #region お蔵入り // (260327Ch) 旧 ebsd_DoWork と専用 helper は ebsdNew 本命化に伴いコメントアウトして退避
    /*
    /// <summary>
    /// BeamDirections が 1 個の正方格子なのか、+Z/-Z の 2 枚の正方格子なのかを判定する。
    /// MasterPattern の全球計算では半球ごとに独立した格子として扱わないと代表方向の共有が崩れ、
    /// 横縞のようなアーティファクトが出るため、この判定を使って mapping を切り替える。
    /// </summary>
    private static bool TryGetBeamDirectionSquareLayout(int beamDirectionCount, out int hemisphereCount, out int gridSize)
    {
        hemisphereCount = 1;
        gridSize = (int)Math.Sqrt(Math.Max(0, beamDirectionCount));
        if (gridSize > 0 && gridSize * gridSize == beamDirectionCount)
            return true;

        if (beamDirectionCount % 2 == 0)
        {
            var hemisphereLength = beamDirectionCount / 2;
            var hemisphereGridSize = (int)Math.Sqrt(hemisphereLength);
            if (hemisphereGridSize > 0 && hemisphereGridSize * hemisphereGridSize == hemisphereLength)
            {
                hemisphereCount = 2; // (260321Ch) 全球 MasterPattern では +Z と -Z を別々の正方格子として扱う
                gridSize = hemisphereGridSize;
                return true;
            }
        }

        hemisphereCount = 1;
        gridSize = Math.Max(1, gridSize);
        return false;
    }

    /// <summary>
    /// EBSD の事前計算で共有する代表方向の index を作る。
    /// 全球 MasterPattern の場合でも、各半球内で近傍方向をまとめるようにして
    /// Rosca-Lambert 格子の行列構造が崩れないようにする。
    /// </summary>
    private static int[] CreateEbsdRepresentativeDirectionMapping(int beamDirectionCount, int blockSize)
    {
        if (beamDirectionCount <= 0)
            return [];

        if (TryGetBeamDirectionSquareLayout(beamDirectionCount, out var hemisphereCount, out var gridSize))
        {
            var mapping = new int[beamDirectionCount];
            var hemisphereLength = gridSize * gridSize;
            for (int i = 0; i < beamDirectionCount; i++)
            {
                var hemisphereIndex = Math.Min(hemisphereCount - 1, i / hemisphereLength);
                var localIndex = i - hemisphereIndex * hemisphereLength;
                var w = localIndex % gridSize;
                var h = localIndex / gridSize;
                var wIndex = Math.Min((w / blockSize) * blockSize + blockSize / 2, gridSize - 1);
                var hIndex = Math.Min((h / blockSize) * blockSize + blockSize / 2, gridSize - 1);
                mapping[i] = hemisphereIndex * hemisphereLength + wIndex + hIndex * gridSize; // (260321Ch)
            }
            return mapping;
        }

        // int width = (int)Math.Sqrt(beamDirectionCount); // (260321Ch) 旧案: 全方向を 1 枚の正方格子と仮定していた
        var fallbackWidth = Math.Max(1, (int)Math.Sqrt(beamDirectionCount));
        var fallbackHeight = Math.Max(1, (int)Math.Ceiling((double)beamDirectionCount / fallbackWidth));
        var results = new int[beamDirectionCount];
        for (int i = 0; i < beamDirectionCount; i++)
        {
            var w = i % fallbackWidth;
            var h = Math.Min(fallbackHeight - 1, i / fallbackWidth);
            var wIndex = Math.Min((w / blockSize) * blockSize + blockSize / 2, fallbackWidth - 1);
            var hIndex = Math.Min((h / blockSize) * blockSize + blockSize / 2, fallbackHeight - 1);
            results[i] = Math.Min(beamDirectionCount - 1, wIndex + hIndex * fallbackWidth); // (260321Ch) 非正方格子では安全側に index を丸める
        }
        return results;
    }

    /// <summary>
    /// EBSD (Electron Backscatter Diffraction) 計算用
    ///
    /// 【物理的背景】
    /// EBSDでは、入射電子が結晶内部の原子と非弾性散乱し、原子位置に「点光源」が生じる。この点光源から放出された電子が、
    /// 結晶のポテンシャル中をコヒーレントに回折しながら結晶表面に到達し、検出器で菊池パターンとして観測される。
    ///
    /// これはCBED（収束電子回折）とは根本的に異なる。CBEDでは外部から平面波が入射するのに対し、EBSDでは結晶内部が光源である。
    ///
    /// 【相反定理 (Reciprocity Theorem) の利用】
    /// 検出器方向 k̂_f に向かう電子の強度を直接計算する代わりに、相反定理により「k̂_f 方向から逆向きに平面波を入射させたときの、
    /// 結晶内部の原子位置における波動関数の強度」を計算する。これにより、CBED と同じ固有値問題の枠組みを利用できる。
    ///
    /// 【強度の計算式】
    /// I(k̂_f, t) = Σ_n σ_n  Σ_{j,j'} β_n^(j) conj(β_n^(j')) F_{jj'}(t)
    ///
    /// ここで：
    ///   n : 原子のインデックス（全対称等価位置を含む）
    ///   j, j' : ブロッホ状態のインデックス
    ///   σ_n : 原子 n の後方散乱断面積 (∝ Z² × Occ)
    ///   β_n^(j) = α_j × μ_n^(j) : 励起振幅 × ブロッホ波場
    ///   α_j = [C⁻¹]_{j,0} : 平面波 ψ_0 に対するブロッホ状態 j の励起振幅
    ///   μ_n^(j) = Σ_g C_g^(j) exp(2πi g·r_n) : 原子位置 r_n でのブロッホ波場
    ///   C_g^(j) : 固有ベクトル（ブロッホ状態 j の平面波 g 成分）
    ///   g·r_n = h_g x_n + k_g y_n + l_g z_n : ミラー指数と分率座標の内積
    ///
    ///   F_{jj'}(t) = [exp(λ_{jj'} t) - 1] / λ_{jj'} : 厚さ依存因子
    ///   λ_{jj'} = 2πi(γ_j - conj(γ_j')) : ブロッホ状態間の結合定数
    ///   γ_j : 固有値（ブロッホ状態 j の結晶内波数の z 成分）
    ///
    /// 【交差項の物理的意味】
    ///   j = j' (対角項) : F_{jj}(t) は単調関数 → パターンの全体的なコントラスト
    ///   j ≠ j' (交差項) : F_{jj'}(t) は振動関数 → 菊池バンドの厚さ依存変化
    ///   交差項がないと、パターン形状が厚さに依存しなくなる。
    ///
    /// 【計算効率の工夫】
    /// 上式を S_{jj'} = Σ_n σ_n β_n^(j) conj(β_n^(j')) と分離すると、
    ///   I(t) = Σ_{j,j'} S_{jj'} × F_{jj'}(t) = Tr(S · F(t))
    /// となり、原子依存部(S)と厚さ依存部(F)が分離される。
    /// S は方向ごとに1回、F は厚さごとに計算するだけでよい。
    ///
    /// さらに、位相因子 exp(2πi g·r_n) はビームのミラー指数と原子の分率座標
    /// だけで決まり検出器方向に依存しないため、beamsPreliminary の段階で
    /// 事前計算してキャッシュし、全検出器方向で再利用する。
    ///
    /// β と S の計算は行列積として定式化でき (B = P·C·diag(α), S = B†·diag(σ)·B)、
    /// ネイティブの Eigen ライブラリの BLAS ルーチンで高速に実行される。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //private void ebsd_DoWork(object sender, DoWorkEventArgs e)
    //{
    //    //UseNonLocalAbsorption=true;
    //    //IncludeTDSBackground=true;

    //    var (solver, thread, voltages) = ((Solver, int, double[]))e.Argument;

    //    Disks = new CBED_Disk[voltages.Length][];
    //    int count = 0;

    //    var beamDirectionsP = BeamDirections.AsParallel();
    //    // int width = (int)Math.Sqrt(BeamDirections.Length); // (260321Ch) 旧案: 方向集合が常に 1 枚の正方格子だと仮定していた
    //    // int height = width; // (260321Ch)
    //    // double radius = width / 2.0; // (260321Ch)

    //    #region solver, thread の設定
    //    // CBED と同じソルバー選択ロジック。
    //    // Eigen ネイティブライブラリが利用可能なら優先的に使用する。
    //    if (solver == Solver.Auto || (!EigenEnabled && (solver == Solver.Eigen_Eigen || solver == Solver.MtxExp_Eigen)))
    //    {
    //        if (EigenEnabled)
    //            (solver, thread) = (Solver.Eigen_Eigen, ProcessorCount);
    //        else
    //            (solver, thread) = (Solver.Eigen_MKL, MklEnabled ? Math.Max(1, ProcessorCount / 4) : ProcessorCount);
    //    }
    //    var reportString = $"{solver}{thread}";
    //    #endregion

    //    #region 原子情報の事前準備
    //    // 結晶内の全原子位置（対称操作で生成された等価位置を含む）と、
    //    // 各原子の後方散乱断面積 σ を収集する。
    //    //
    //    // 後方散乱断面積は Rutherford 散乱の近似としてBrowning (1994) の弾性散乱断面積の
    //    // 経験的フィット σ ∝ Z^1.7 × Occ とする。
    //    // Z : 原子番号, Occ : 占有率
    //    // これは、重い原子ほど強く後方散乱するという物理を反映している。
    //    var atomSites = new List<(double x, double y, double z, double sigma)>();
    //    foreach (var atoms in Crystal.Atoms)
    //    {
    //        double sigma = Math.Pow(atoms.AtomicNumber, 1.7) * atoms.Occ;
    //        foreach (var atom in atoms.Atom)   // atoms.Atom[] は対称操作で展開された全等価位置
    //            atomSites.Add((atom.X, atom.Y, atom.Z, sigma));
    //    }
    //    var atomArray = atomSites.ToArray();
    //    int nAtoms = atomArray.Length;

    //    // σ 配列は全検出器方向で共通なので、ループ外で1回だけ作成する。
    //    var sigmaArray = new double[nAtoms];
    //    for (int n = 0; n < nAtoms; n++)
    //        sigmaArray[n] = atomArray[n].sigma;

    //    // TDS バックグラウンド格納用 (IncludeTDSBackground=true のとき使用)
    //    double[][] ebsdBackground = null;
    //    #endregion

    //    // 加速電圧ごとのループ (電圧依存性を調べる場合に複数)
    //    for (int vIndex = 0; vIndex < voltages.Length; vIndex++)
    //    {
    //        AccVoltage = voltages[vIndex];

    //        // 真空中の電子の波数 k_vac (相対論補正込み)
    //        var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(AccVoltage);

    //        // 結晶内平均ポテンシャル U_0 (屈折効果を与える)
    //        // 結晶に入った電子は U_0 分だけ運動エネルギーが増加し、波数が変化する。
    //        var u0 = getU(AccVoltage).Real.Real;

    //        // ポテンシャルのキャッシュをクリア
    //        uDictionary.Clear();

    //        #region beamsPreliminary: g ベクトルの事前計算
    //        // 検出器ピクセルを grid×grid のブロックに分割し、各ブロックの中心方向で
    //        // 代表的な g ベクトル集合を計算する。
    //        // Find_gVectors は計算コストが高い (全逆格子点を探索) ため、
    //        // 全ピクセルで個別に呼ぶのではなくグリッド化して計算量を削減する。
    //        //
    //        // また、位相因子 P[n,g] = exp(2πi g·r_n) をここで事前計算する。
    //        // この値はビームのミラー指数 (h,k,l) と原子の分率座標 (x,y,z) だけで決まり、
    //        // 検出器方向（入射方向）には依存しない。
    //        // したがって、全検出器方向で使い回すことで Complex.Exp の呼び出しを大幅に削減できる。
    //        //
    //        // 格納順序: column-major (Eigen の既定に合わせる)
    //        //   phaseNG[g * nAtoms + n] = P(n, g) = exp(2πi(h_g x_n + k_g y_n + l_g z_n))
    //        //
    //        // 位相因子の符号について:
    //        //   getU では構造因子を exp(+2πi g·r_n) で定義 (2024/05/24 に変更済み)。
    //        //   ブロッホ波の平面波展開 ψ^(j)(r) = Σ_g C_g^(j) exp(+2πi g·r) と同じ符号。
    //        //   ポテンシャルのフーリエ逆変換 V(r) = Σ_g U_g exp(-2πi g·r) とは逆符号だが、
    //        //   これはフーリエ変換/逆変換の規約の違いであり、物理的に整合している。
    //        // var grid = Math.Max(1, EbsdRepresentativeDirectionBlockSize); // (260321Ch) 旧案: master 用の試験条件を既存 ebsd_DoWork に持ち込んでいた
    //        var grid = 3; // (260321Ch) 既存 ebsd_DoWork は従来どおり固定表面・3x3 代表方向共有に戻す
    //        // var mapping = beamDirectionsP.Select((_, i) => { ... }).ToArray(); // (260321Ch) 旧案: 全球でも 1 枚の正方格子として index を丸めていた
    //        var mapping = CreateEbsdRepresentativeDirectionMapping(BeamDirections.Length, grid); // (260321Ch) 全球時は半球ごとに代表方向を共有する

    //        var mappingSet = new HashSet<int>(mapping);// HashSet<int> に変換することで O(1) のルックアップになる。

    //        var beamsPreliminary = beamDirectionsP.Select((e, i) =>
    //            {

    //                if (!mappingSet.Contains(i))
    //                    return (null, null, null);

    //                // var localSurface = GetEbsdSurfaceNormal(e); // (260321Ch) 旧案: master 用の局所表面法線を既存 ebsd_DoWork に導入していた
    //                // var beams = Find_gVectors(BaseRotation, getVecK0(kvac, u0, e, localSurface), localSurface, MaxNumOfBloch); // (260321Ch)
    //                var beams = Find_gVectors(BaseRotation, getVecK0(kvac, u0, e), MaxNumOfBloch); // (260321Ch) 既存経路は固定表面版を維持する
    //                var potentialMatrix = UseNonLocalAbsorption ? getPotentialMatrix(beams, 0, Math.PI) : getPotentialMatrix(beams);

    //                var bLen = beams.Length;
    //                var phaseNG = new Complex[nAtoms * bLen];
    //                for (int n = 0; n < nAtoms; n++)
    //                {
    //                    var (xn, yn, zn, _) = atomArray[n];
    //                    for (int g = 0; g < bLen; g++)
    //                    {
    //                        var (h, k, l) = beams[g].Index;
    //                        phaseNG[g * nAtoms + n] = Exp(TwoPiI * (h * xn + k * yn + l * zn));
    //                    }
    //                }
    //                return (beams, potentialMatrix, phaseNG);
    //            }).ToArray();
    //        #endregion

    //        #region muBack: TDS 後方散乱行列の事前計算 (260316Cl 追加)
    //        // STEM 整合型 TDS バックグラウンドを計算するため、各グリッドセルの
    //        // ビーム集合に対して U'_back 行列を事前計算する。
    //        // U'_back[i,j] = getU(kV, g_i-g_j, null, π/2, π).Imag.Conjugate()
    //        // は後方半球 (θ∈[π/2,π]) への TDS 散乱行列要素。
    //        //
    //        // uDictionary のキャッシュは角度範囲を区別しないため、
    //        // 標準ポテンシャル計算と衝突しないよう前後で Clear する。
    //        Complex[][] muBackArrays = null;
    //        double tdsCoeff = 0;
    //        if (IncludeTDSBackground)
    //        {
    //            tdsCoeff = 2 * Math.PI / kvac;
    //            uDictionary.Clear();
    //            muBackArrays = new Complex[beamsPreliminary.Length][];

    //            // 全グリッドセルを並列計算。uDictionary (ConcurrentDictionary) は
    //            // スレッドセーフなので、異なるグリッドセル間でキャッシュを共有できる。
    //            Parallel.ForEach(mappingSet, idx =>
    //            {
    //                var bp = beamsPreliminary[idx].Item1;
    //                if (bp == null) return;
    //                var baseLen = bp.Length;
    //                var muBack = new Complex[baseLen * baseLen];

    //                // Toeplitz 構造: muBack[i,j] = f(g_i - g_j)。
    //                // ローカルキャッシュで同一 g-g' 差の重複計算と Beam 生成を回避。
    //                var localCache = new Dictionary<int, Complex>();
    //                for (int col = 0; col < baseLen; col++)
    //                    for (int row = 0; row < baseLen; row++)
    //                    {
    //                        var key = compose(bp[row].H - bp[col].H, bp[row].K - bp[col].K, bp[row].L - bp[col].L);
    //                        if (!localCache.TryGetValue(key, out var val))
    //                        {
    //                            val = getU(AccVoltage, bp[row] - bp[col], null, Math.PI / 2, Math.PI, 30, 12).Imag.Conjugate();
    //                            localCache[key] = val;
    //                        }
    //                        muBack[col * baseLen + row] = val;
    //                    }
    //                muBackArrays[idx] = muBack;
    //            });
    //            uDictionary.Clear();
    //        }
    //        #endregion

    //        #region 各検出器方向での EBSD 強度計算
    //        // 各検出器方向 k̂_f に対して、相反定理により「k̂_f から逆向きに平面波を
    //        // 入射させたときの、原子位置でのブロッホ波場強度」を計算する。
    //        //
    //        // 処理の流れ:
    //        //   1. k̂_f に対応する K₀ ベクトルを計算
    //        //   2. 各ビームの励起誤差 (P, Q) を再計算 (reset_gVectors)
    //        //   3. 固有値問題マトリックス A を構築 (getEigenMatrix)
    //        //   4. A の固有値 γ_j と固有ベクトル C_g^(j) を求める
    //        //   5. 励起振幅 α_j = [C⁻¹]_{j,0} を計算
    //        //   6. S 行列と F 行列から各厚さでの強度 I(t) を計算
    //        //     (ネイティブの場合は 5-6 を NativeWrapper.EBSDSolver で一括実行)

    //        if (IncludeTDSBackground)
    //            ebsdBackground = new double[BeamDirections.Length][];

    //        var ebsdIntensity = beamDirectionsP.Select((beamDirection, i) =>
    //        {
    //            if (bwEBSD.CancellationPending) return null;

    //            // 結晶内での K₀ ベクトルを計算。
    //            // K₀ は真空中の波数ベクトル k_vac を、結晶の平均内部ポテンシャル U₀ による
    //            // 屈折を考慮して修正したもの。表面法線方向の成分のみが変化する。
    //            // var localSurface = GetEbsdSurfaceNormal(beamDirection); // (260321Ch) 旧案: master 用の局所表面法線を既存 ebsd_DoWork に導入していた
    //            // var vecK0 = getVecK0(kvac, u0, beamDirection, localSurface); // (260321Ch)
    //            var vecK0 = getVecK0(kvac, u0, beamDirection); // (260321Ch) 既存経路は固定表面の屈折モデルへ戻す

    //            // この検出器方向に対応するグリッドセルの事前計算結果を取得
    //            var (beamsBase, potentialMatrix, phaseNG) = beamsPreliminary[mapping[i]];

    //            // 各ビームの励起誤差 (P, Q) を、この検出器方向の K₀ に対して再計算する。
    //            // P = 2 n·(K₀+g) : ビームが出射面から出ていく条件 (P > 0)
    //            // Q = K₀² - |K₀+g|² : 励起誤差に関連する量 (ブラッグ条件で Q = 0)
    //            // P > 0 のビームのみ選択する (出射面から出ないビームは物理的に寄与しない)。
    //            // var beams = reset_gVectors(beamsBase, BaseRotation, vecK0, localSurface).Where(e => e.P > 0).ToArray(); // (260321Ch) 旧案: master 用の局所表面法線で P を判定していた
    //            var beams = reset_gVectors(beamsBase, BaseRotation, vecK0).Where(e => e.P > 0).ToArray(); // (260321Ch) 既存経路は固定表面版へ戻す

    //            var bLen = beams.Length;
    //            if (bLen == 0) return null;

    //            var psi0Native = new Complex[bLen];
    //            psi0Native[0] = One;

    //            var eigenMatrix = Shared.Rent(bLen * bLen);
    //            try
    //            {
    //                // 固有値問題マトリックス A を構築。
    //                // A_{gg'} = U_{g'-g}/P_{g'} + δ_{gg'} Q_g/P_g
    //                // ここで U_{g'-g} は結晶ポテンシャルのフーリエ係数。
    //                // この行列の固有値 γ_j がブロッホ状態の結晶内波数 z 成分、
    //                // 固有ベクトル C_g^(j) がブロッホ状態の平面波成分を与える。
    //                getEigenMatrix(bLen, beams, ref eigenMatrix, potentialMatrix);

    //                Complex[] eigenValues, eigenVectors, alpha;

    //                if (solver == Solver.Eigen_Eigen && EigenEnabled)
    //                {
    //                    // Eigen ネイティブライブラリで固有値分解
    //                    (eigenValues, eigenVectors) = NativeWrapper.EigenSolver(bLen, eigenMatrix);

    //                    // 逆行列 C⁻¹ を計算し、α_j = [C⁻¹]_{j,0} (0列目の j 行目) を取得。
    //                    // α_j は「平面波 ψ₀ = (1,0,...,0)ᵀ がブロッホ状態 j をどれだけ励起するか」
    //                    // を表す。column-major なので [C⁻¹]_{j,0} = eigenVectorsInv[j]。
    //                    // var eigenVectorsInv = NativeWrapper.Inverse(bLen, eigenVectors); // (260321Ch) 旧実装: α のためだけに逆行列全体を作っていた
    //                    // alpha = new Complex[bLen];
    //                    // for (int j = 0; j < bLen; j++)
    //                    //     alpha[j] = eigenVectorsInv[j];
    //                    alpha = NativeWrapper.PartialPivLuSolve(bLen, eigenVectors, psi0Native); // (260321Ch)
    //                }
    //                else
    //                {
    //                    // MathNet.Numerics による固有値分解 (MKL バックエンド or マネージド)
    //                    var evd = new DMat(bLen, bLen, eigenMatrix.AsSpan()[..(bLen * bLen)].ToArray()).Evd(Symmetricity.Asymmetric);
    //                    eigenValues = ((DVec)evd.EigenValues).Values;
    //                    eigenVectors = ((DMat)evd.EigenVectors).Values;

    //                    // LU 分解で C α = ψ₀ を直接解く (逆行列を明示的に作らない)
    //                    var psi0 = new DVec(new Complex[bLen]) { [0] = One };
    //                    alpha = ((DVec)(evd.EigenVectors as DMat).LU().Solve(psi0)).Values;
    //                }

    //                // beamsBase → beams のインデックスマッピング。
    //                // reset_gVectors 後に P > 0 でフィルタされたため、beams は beamsBase の
    //                // 部分集合になっている。事前計算した phaseNG は beamsBase のインデックスを
    //                // 使っているので、beams[g] が beamsBase の何番目に対応するかを調べる。
    //                // (reset_gVectors は P, Q のみ再計算し、ミラー指数 Index は不変)
    //                var baseLen = beamsBase.Length;
    //                var gMap = new int[bLen];
    //                for (int g = 0; g < bLen; g++)
    //                {
    //                    var idx = beams[g].Index;
    //                    for (int gb = 0; gb < baseLen; gb++)
    //                        if (beamsBase[gb].Index == idx) { gMap[g] = gb; break; }
    //                }

    //                // phaseNG からフィルタ後のビームに対応する列だけを抽出する。
    //                // column-major: phaseFiltered[g * nAtoms + n] = P(n, g)
    //                // これにより、C++ の Eigen::Map<MatrixXcd>(phaseFiltered, nAtoms, bLen) で
    //                // 正しく nAtoms × bLen の行列として読み込まれる。
    //                var phaseFiltered = new Complex[nAtoms * bLen];
    //                for (int n = 0; n < nAtoms; n++)
    //                    for (int g = 0; g < bLen; g++)
    //                        phaseFiltered[g * nAtoms + n] = phaseNG[gMap[g] * nAtoms + n];

    //                // TDS 後方散乱行列を beamsBase からフィルタ後のビームに抽出
    //                Complex[] muBackFiltered = null;
    //                if (IncludeTDSBackground && muBackArrays?[mapping[i]] is Complex[] muBackBase)
    //                {
    //                    muBackFiltered = new Complex[bLen * bLen];
    //                    for (int col = 0; col < bLen; col++)
    //                        for (int row = 0; row < bLen; row++)
    //                            muBackFiltered[col * bLen + row] = muBackBase[gMap[col] * baseLen + gMap[row]];
    //                }

    //                // EBSD 強度を計算。
    //                // ネイティブ版 (_EBSDSolver) では、以下の処理が Eigen の行列演算で一括実行される:
    //                //   B = P · C · diag(α)         ... β_n^(j) の行列 (nAtoms × bLen)
    //                //   S = B† · diag(σ) · B        ... S 行列 (bLen × bLen, Hermitian)
    //                //   I(t) = Σ_{jj'} S_{jj'} F_{jj'}(t)  ... 各厚さの強度
    //                // 260316Cl TDS 一括計算対応: EigenEnabled && TDS の場合は _EBSDSolverWithTDS で
    //                // 弾性 + TDS を一度に計算し、F 行列 (exp(λt)) の重複を排除。
    //                // 260316Cl以前のコード:
    //                //double[] intensity;
    //                //if (EigenEnabled)
    //                //    intensity = NativeWrapper.EBSDSolver(
    //                //        eigenValues, eigenVectors, alpha,
    //                //        phaseFiltered, sigmaArray, Thicknesses);
    //                //else
    //                //    intensity = EBSDSolverManaged(
    //                //        bLen, nAtoms, eigenValues, eigenVectors, alpha,
    //                //        phaseFiltered, sigmaArray, Thicknesses);
    //                double[] intensity;
    //                if (EigenEnabled && muBackFiltered != null)
    //                {
    //                    // Eigen BLAS で弾性 + TDS を一括計算。
    //                    // C†×U'_back×C を Eigen の行列積で高速実行し、
    //                    // 弾性信号と TDS で同じ F_{jj'}(t) を共有して exp(λt) の重複を排除。
    //                    var result = NativeWrapper.EBSDSolverWithTDS(eigenValues, eigenVectors, alpha, phaseFiltered, sigmaArray, muBackFiltered, tdsCoeff, Thicknesses);
    //                    intensity = result.intensity;
    //                    ebsdBackground[i] = result.tdsIntensity;
    //                }
    //                else if (EigenEnabled)
    //                {
    //                    intensity = NativeWrapper.EBSDSolver(eigenValues, eigenVectors, alpha, phaseFiltered, sigmaArray, Thicknesses);
    //                }
    //                else
    //                {
    //                    intensity = EBSDSolverManaged(bLen, nAtoms, eigenValues, eigenVectors, alpha, phaseFiltered, sigmaArray, Thicknesses);
    //                    // Managed フォールバック時の TDS 計算
    //                    if (muBackFiltered != null)
    //                        ebsdBackground[i] = ComputeTDSMatrixBackground(bLen, eigenValues, eigenVectors, alpha, muBackFiltered, tdsCoeff, Thicknesses);
    //                }

    //                // 旧実装では Disks への格納時に実数強度へ落とすため表面位相の差は見えないが、
    //                // crystal-fixed master では局所表面法線に応じた境界条件を明示的に残しておく。
    //                // for (int t = 0; t < Thicknesses.Length; t++) // (260321Ch) 旧案: 固定 Surface.Z を使っていた
    //                //     for (int b = 0; b < bLen; b++)
    //                //         result[t * bLen + b] *= Exp(PiI * (beams[b].P - 2 * kvac * Surface.Z) * Thicknesses[t]);

    //                if (Interlocked.Increment(ref count) % 50 == 0)
    //                    bwEBSD.ReportProgress(count, reportString);

    //                return intensity;
    //            }
    //            finally { Shared.Return(eigenMatrix); }
    //        }).ToArray();
    //        #endregion

    //        #region Disk への格納
    //        // 各厚さの結果を CBED_Disk 構造に格納する。
    //        // CBED_Disk.Amplitudes は本来は振幅(Complex)だが、EBSD では強度の
    //        // 平方根を実部に入れ、虚部を 0 とすることで既存の描画コードと互換性を保つ。
    //        Disks[vIndex] = new CBED_Disk[Thicknesses.Length];
    //        Parallel.For(0, Thicknesses.Length, t =>
    //        {
    //            var amplitudes = new Complex[BeamDirections.Length];
    //            for (int r = 0; r < BeamDirections.Length; r++)
    //                if (ebsdIntensity[r] is not null)
    //                {
    //                    // 260316Cl TDS バックグラウンドを加算 (STEM 整合型)。
    //                    // 260316Cl以前のコード:
    //                    //amplitudes[r] = new Complex(Math.Sqrt(Math.Max(0, ebsdIntensity[r][t])), 0);
    //                    var signal = ebsdIntensity[r][t];
    //                    var tds = ebsdBackground?[r]?[t] ?? 0;
    //                    amplitudes[r] = new Complex(Math.Sqrt(Math.Max(0, signal + tds)), 0);
    //                }

    //            Disks[vIndex][t] = new CBED_Disk([0, 0, 0], new Vector3DBase(0, 0, 0),
    //                Thicknesses[t], amplitudes);
    //            Disks[vIndex][t].Amplitudes = Disks[vIndex][t].RawAmplitudes;
    //        });
    //        #endregion

    //        if (bwEBSD.CancellationPending)
    //            e.Cancel = true;
    //    }
    //}
    */

    #endregion

    /// <summary>
    /// crystal-fixed master pattern 用の新しい EBSD 計算経路。
    /// 各 exit direction ごとに結晶を固定表面座標系へ回してから既存の固定表面 EBSD を解くことで、
    /// Find_gVectors の候補選別と K0 計算を「局所表面近似」に頼らず扱う。
    /// </summary>
    private void ebsdNew_DoWork(object sender, DoWorkEventArgs e)
    {
        var (solver, thread, voltages) = ((Solver, int, double[]))e.Argument;

        Disks = new CBED_Disk[voltages.Length][];
        int count = 0;

        #region solver, thread の設定
        if (solver == Solver.Auto || (!EigenEnabled && (solver == Solver.Eigen_Eigen || solver == Solver.MtxExp_Eigen)))
        {
            if (EigenEnabled)
                (solver, thread) = (Solver.Eigen_Eigen, ProcessorCount);
            else
                (solver, thread) = (Solver.Eigen_MKL, MklEnabled ? Math.Max(1, ProcessorCount / 4) : ProcessorCount);
        }
        var reportString = $"{solver}{thread}";
        // var beamDirectionsP = BeamDirections.AsParallel().WithDegreeOfParallelism(thread); // (260321Ch) 旧実装: PLINQ の Select(...).ToArray() を使っていた
        var directionOptions = new ParallelOptions { MaxDegreeOfParallelism = thread }; // (260321Ch) 明示的な Parallel.For で各方向を埋める
        #endregion

        #region 原子情報の事前準備
        var atomSites = new List<(double x, double y, double z, double sigma)>();
        foreach (var atoms in Crystal.Atoms)
        {
            double sigma = Math.Pow(atoms.AtomicNumber, 1.7) * atoms.Occ;
            foreach (var atom in atoms.Atom)
                atomSites.Add((atom.X, atom.Y, atom.Z, sigma));
        }
        var atomArray = atomSites.ToArray();
        int nAtoms = atomArray.Length;

        var sigmaArray = new double[nAtoms];
        for (int n = 0; n < nAtoms; n++)
            sigmaArray[n] = atomArray[n].sigma;

        double[][] ebsdBackground = null;
        #endregion

        if (BeamDirections.Length % 2 != 0)
            throw new InvalidOperationException("MasterPattern directions must contain both hemispheres."); // (260327Ch)
        var hemisphereLength = BeamDirections.Length / 2;
        var directionGridSize = (int)Math.Sqrt(hemisphereLength);
        if (directionGridSize * directionGridSize != hemisphereLength)
            throw new InvalidOperationException("MasterPattern directions must be stored as two hemisphere grids."); // (260327Ch)
        var symmetryRepresentativeMapping = CreateMasterPatternSymmetryRepresentativeDirectionMapping(BeamDirections.Length, hemisphereLength, directionGridSize); // (260327Ch) exact grid symmetry representative
        var symmetryRepresentativeWeights = new int[BeamDirections.Length]; // (260327Ch) 進捗報告と埋め戻しに使う orbit サイズ
        var symmetryRepresentativeIndices = new List<int>();
        for (int i = 0; i < BeamDirections.Length; i++)
        {
            var representativeIndex = symmetryRepresentativeMapping[i];
            if (representativeIndex < 0) continue; // 260331Cl 六方格子の無効セルをスキップ
            symmetryRepresentativeWeights[representativeIndex]++;
            if (representativeIndex == i)
                symmetryRepresentativeIndices.Add(i); // (260327Ch) orbit ごとの最小 index を代表点に採用
        }

        for (int vIndex = 0; vIndex < voltages.Length; vIndex++)
        {
            AccVoltage = voltages[vIndex];

            var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(AccVoltage);
            var u0 = getU(AccVoltage).Real.Real;
            uDictionary.Clear();

            var referenceSurface = Vector3DBase.Normarize(Surface); // (260321Ch) 新経路では固定表面系を基準に、等価な beam / surface 条件へ変換する
            var referenceBeamDirection = Vector3DBase.Normarize(-referenceSurface); // (260321Ch) 固定表面に対する法線出射方向
            var (referenceAxisU, referenceAxisV) = GetSurfaceTangentialAxes(referenceSurface); // (260321Ch) 接平面の方位も含めて局所座標系を固定する

            var blockSize = Math.Max(1, EbsdRepresentativeDirectionBlockSize); // (260321Ch) MasterPattern 用の代表方向共有サイズ
            if (IncludeTDSBackground)
                ebsdBackground = new double[BeamDirections.Length][];
            double tdsCoeff = IncludeTDSBackground ? 2 * Math.PI / kvac : 0; // (260321Ch)
            double[][] ebsdIntensity;

            #region beamsPreliminary
            // if (grid == 1 && !IncludeTDSBackground) { ... } else { ... } // (260321Ch) 旧実装: blockSize=1 だけ専用経路に分けていた
            // if (BeamDirections.Length % 2 != 0) throw ... // (260327Ch) 電圧ごとに不変なのでループ外へ移動
            // var hemisphereLength = BeamDirections.Length / 2; // (260327Ch)
            // var directionGridSize = (int)Math.Sqrt(hemisphereLength); // (260327Ch)
            var mapping = new int[BeamDirections.Length];
            Array.Fill(mapping, -1); // (260327Ch) symmetry representative だけ preliminary mapping を持つ
            var representativeIndices = new List<int>();
            var representativeIndexToDense = new Dictionary<int, int>();
            foreach (var symmetryRepresentativeIndex in symmetryRepresentativeIndices)
            {
                var hemisphereOffset = symmetryRepresentativeIndex / hemisphereLength * hemisphereLength;
                var localIndex = symmetryRepresentativeIndex - hemisphereOffset;
                var w = localIndex % directionGridSize;
                var h = localIndex / directionGridSize;
                var wIndex = Math.Min((w / blockSize) * blockSize + blockSize / 2, directionGridSize - 1);
                var hIndex = Math.Min((h / blockSize) * blockSize + blockSize / 2, directionGridSize - 1);
                var representativeIndex = hemisphereOffset + wIndex + hIndex * directionGridSize; // (260327Ch) symmetry representative に対してだけ近傍共有を作る
                if (!representativeIndexToDense.TryGetValue(representativeIndex, out var denseIndex))
                {
                    denseIndex = representativeIndices.Count;
                    representativeIndexToDense.Add(representativeIndex, denseIndex);
                    representativeIndices.Add(representativeIndex);
                }
                mapping[symmetryRepresentativeIndex] = denseIndex;
            }

            var beamsPreliminary = new (Vector3DBase EquivalentSurface, Vector3DBase EquivalentVecK0, Beam[] Beams, Complex[] PotentialMatrix, Complex[] PhaseNG)[representativeIndices.Count];
            var representativeOptions = directionOptions;
            Complex[][] muBackArrays = null;
            try
            {
                Parallel.For(0, representativeIndices.Count, representativeOptions, (denseIndex, state) =>
                {
                    if (bwEBSDNew.CancellationPending)
                    {
                        state.Stop();
                        return;
                    }

                    var representativeIndex = representativeIndices[denseIndex];
                    var direction = BeamDirections[representativeIndex];
                    var (equivalentBeamDirection, equivalentSurface) = GetCrystalFixedMasterEquivalentBeamAndSurface(direction, referenceSurface, referenceBeamDirection, referenceAxisU, referenceAxisV); // (260321Ch)
                    var equivalentVecK0 = getVecK0(kvac, u0, equivalentBeamDirection, equivalentSurface); // (260321Ch)
                    var beams = Find_gVectors(BaseRotation, equivalentVecK0, equivalentSurface, MaxNumOfBloch); // (260321Ch) BaseRotation を固定して gCache を再利用する
                    // var potentialMatrix = UseNonLocalAbsorption ? getPotentialMatrix(beams, 0, Math.PI) : getPotentialMatrix(beams); // (260321Ch) 旧実装
                    var potentialMatrix = RentMasterPatternPotentialMatrix(beams); // (260321Ch)
                    var phaseNG = CreatePhaseFactors(atomArray, nAtoms, beams); // (260321Ch)
                    beamsPreliminary[denseIndex] = (equivalentSurface, equivalentVecK0, beams, potentialMatrix, phaseNG);
                });

                #endregion

                #region muBack
                if (IncludeTDSBackground)
                {
                    uDictionary.Clear();
                    muBackArrays = new Complex[beamsPreliminary.Length][];

                    Parallel.For(0, beamsPreliminary.Length, representativeOptions, idx =>
                    {
                        var bp = beamsPreliminary[idx].Beams;
                        if (bp == null) return;
                        muBackArrays[idx] = CreateMasterPatternMuBack(AccVoltage, bp); // (260321Ch)
                    });
                    uDictionary.Clear();
                }
                #endregion

                #region 各方向での EBSD 強度計算
                // ebsdIntensity = beamDirectionsP.Select((beamDirection, i) => ...).ToArray(); // (260321Ch) 旧実装: PLINQ ベース
                var threadLocalBeams = new ThreadLocal<Beam[]>(() => null, true); // (260321Ch) 方向ごとの rent/return をやめ、スレッドごとに作業配列を再利用する
                var threadLocalGMap = new ThreadLocal<int[]>(() => null, true); // (260321Ch)
                var threadLocalPhaseFiltered = new ThreadLocal<Complex[]>(() => null, true); // (260321Ch)
                var threadLocalMuBackFiltered = new ThreadLocal<Complex[]>(() => null, true); // (260321Ch)
                var threadLocalEigenMatrix = new ThreadLocal<Complex[]>(() => null, true); // (260321Ch)
                var threadLocalPsi0 = new ThreadLocal<Complex[]>(() => null, true); // (260321Ch)
                var threadLocalAlpha = new ThreadLocal<Complex[]>(() => null, true); // (260321Ch)
                var threadLocalEigenValues = new ThreadLocal<Complex[]>(() => null, true); // (260321Ch)
                var threadLocalEigenVectors = new ThreadLocal<Complex[]>(() => null, true); // (260321Ch)
                try
                {
                    ebsdIntensity = new double[BeamDirections.Length][];
                    Parallel.For(0, symmetryRepresentativeIndices.Count, directionOptions, (representativeDenseIndex, state) =>
                    {
                        if (bwEBSDNew.CancellationPending)
                        {
                            state.Stop();
                            return;
                        }

                        var i = symmetryRepresentativeIndices[representativeDenseIndex]; // (260327Ch) exact symmetry representative だけ動力学計算する

                        // 260321Cl: beamsBase / potentialMatrix / phaseNG は代表方向から共有 (Phase 1 の blockSize 近似)
                        //           equivalentVecK0 / equivalentSurface は各方向で個別計算 (バグ修正: 旧実装は代表の K0 を全方向に流用していた)
                        var (_, _, beamsBase, potentialMatrix, phaseNG) = beamsPreliminary[mapping[i]];
                        var muBackBase = IncludeTDSBackground ? muBackArrays?[mapping[i]] : null;
                        var baseLen = beamsBase?.Length ?? 0;
                        if (baseLen == 0)
                            return;

                        // 260321Cl 追加: 各方向固有の等価ビーム方向・表面法線・K0 を計算
                        var direction_i = BeamDirections[i];
                        var (equivalentBeamDirection_i, equivalentSurface_i) = GetCrystalFixedMasterEquivalentBeamAndSurface(
                            direction_i, referenceSurface, referenceBeamDirection, referenceAxisU, referenceAxisV);
                        var equivalentVecK0_i = getVecK0(kvac, u0, equivalentBeamDirection_i, equivalentSurface_i);

                        // var beams = ArrayPool<Beam>.Shared.Rent(baseLen); // (260321Ch) 旧実装: 方向ごとに rent/return していた
                        var beams = threadLocalBeams.Value;
                        if (beams is null || beams.Length < baseLen)
                        {
                            if (beams != null)
                                ArrayPool<Beam>.Shared.Return(beams); // (260321Ch)
                            beams = ArrayPool<Beam>.Shared.Rent(baseLen); // (260321Ch)
                            threadLocalBeams.Value = beams; // (260321Ch)
                        }

                        var bLen = 0;
                        double[] intensity;
                        double[] background = null;

                        reset_gVectors(baseLen, beamsBase, BaseRotation, equivalentVecK0_i, equivalentSurface_i, ref beams); // 260321Cl: 旧 equivalentVecK0/Surface → 各方向固有の値

                        for (int beamIndex = 0; beamIndex < baseLen; beamIndex++)
                            if (beams[beamIndex].P > 0)
                                beams[bLen++] = beams[beamIndex]; // (260321Ch) 元の順序を保ったまま前方へ詰める

                        if (bLen == 0)
                            return;

                        var gMap = threadLocalGMap.Value;
                        if (gMap is null || gMap.Length < bLen)
                        {
                            if (gMap != null)
                                ArrayPool<int>.Shared.Return(gMap); // (260321Ch)
                            gMap = ArrayPool<int>.Shared.Rent(bLen); // (260321Ch)
                            threadLocalGMap.Value = gMap; // (260321Ch)
                        }

                        int baseIndex = 0;
                        for (int g = 0; g < bLen; g++)
                        {
                            var idx = beams[g].Index;
                            while (baseIndex < baseLen && beamsBase[baseIndex].Index != idx)
                                baseIndex++;
                            if (baseIndex >= baseLen)
                                throw new InvalidOperationException("Filtered beam could not be mapped back to beamsBase."); // (260321Ch)
                            gMap[g] = baseIndex;
                        }

                        int phaseFilteredLength = nAtoms * bLen;
                        var phaseFiltered = threadLocalPhaseFiltered.Value;
                        if (phaseFiltered is null || phaseFiltered.Length < phaseFilteredLength)
                        {
                            if (phaseFiltered != null)
                                Shared.Return(phaseFiltered); // (260321Ch)
                            phaseFiltered = Shared.Rent(phaseFilteredLength); // (260321Ch)
                            threadLocalPhaseFiltered.Value = phaseFiltered; // (260321Ch)
                        }
                        var phaseFilteredSpan = phaseFiltered.AsSpan(0, phaseFilteredLength); // (260321Ch) Span で利用範囲だけを扱う
                        for (int g = 0; g < bLen; g++)
                            phaseNG.AsSpan(gMap[g] * nAtoms, nAtoms).CopyTo(phaseFilteredSpan.Slice(g * nAtoms, nAtoms)); // (260321Ch)

                        Complex[] muBackFiltered = null;
                        if (muBackBase != null)
                        {
                            int muBackFilteredLength = bLen * bLen;
                            muBackFiltered = threadLocalMuBackFiltered.Value;
                            if (muBackFiltered is null || muBackFiltered.Length < muBackFilteredLength)
                            {
                                if (muBackFiltered != null)
                                    Shared.Return(muBackFiltered); // (260321Ch)
                                muBackFiltered = Shared.Rent(muBackFilteredLength); // (260321Ch)
                                threadLocalMuBackFiltered.Value = muBackFiltered; // (260321Ch)
                            }
                            for (int col = 0; col < bLen; col++)
                            {
                                int dstColOffset = col * bLen;
                                int srcColOffset = gMap[col] * baseLen;
                                for (int row = 0; row < bLen; row++)
                                    muBackFiltered[dstColOffset + row] = muBackBase[srcColOffset + gMap[row]]; // (260321Ch)
                            }
                        }

                        int eigenMatrixLength = bLen * bLen;
                        var eigenMatrix = threadLocalEigenMatrix.Value;
                        if (eigenMatrix is null || eigenMatrix.Length < eigenMatrixLength)
                        {
                            if (eigenMatrix != null)
                                Shared.Return(eigenMatrix); // (260321Ch)
                            eigenMatrix = Shared.Rent(eigenMatrixLength); // (260321Ch)
                            threadLocalEigenMatrix.Value = eigenMatrix; // (260321Ch)
                        }
                        getEigenMatrix(bLen, beams, ref eigenMatrix, potentialMatrix);

                        Complex[] eigenValues, eigenVectors, alpha;
                        if (solver == Solver.Eigen_Eigen && EigenEnabled)
                        {
                            eigenValues = threadLocalEigenValues.Value;
                            if (eigenValues is null || eigenValues.Length < bLen)
                            {
                                if (eigenValues != null)
                                    Shared.Return(eigenValues); // (260321Ch)
                                eigenValues = Shared.Rent(bLen); // (260321Ch)
                                threadLocalEigenValues.Value = eigenValues; // (260321Ch)
                            }

                            eigenVectors = threadLocalEigenVectors.Value;
                            if (eigenVectors is null || eigenVectors.Length < eigenMatrixLength)
                            {
                                if (eigenVectors != null)
                                    Shared.Return(eigenVectors); // (260321Ch)
                                eigenVectors = Shared.Rent(eigenMatrixLength); // (260321Ch)
                                threadLocalEigenVectors.Value = eigenVectors; // (260321Ch)
                            }

                            NativeWrapper.EigenSolver(bLen, eigenMatrix, ref eigenValues, ref eigenVectors); // (260321Ch) 固有値・固有ベクトルもスレッドごとに再利用する

                            // var psi0Native2 = new Complex[bLen]; // (260321Ch) 旧実装: 方向ごとに毎回 new していた
                            var psi0Native2 = threadLocalPsi0.Value;
                            if (psi0Native2 is null || psi0Native2.Length < bLen)
                            {
                                if (psi0Native2 != null)
                                    Shared.Return(psi0Native2); // (260321Ch)
                                psi0Native2 = Shared.Rent(bLen); // (260321Ch)
                                threadLocalPsi0.Value = psi0Native2; // (260321Ch)
                            }
                            psi0Native2.AsSpan(0, bLen).Clear(); // (260321Ch)
                            psi0Native2[0] = One; // (260321Ch)

                            alpha = threadLocalAlpha.Value;
                            if (alpha is null || alpha.Length < bLen)
                            {
                                if (alpha != null)
                                    Shared.Return(alpha); // (260321Ch)
                                alpha = Shared.Rent(bLen); // (260321Ch)
                                threadLocalAlpha.Value = alpha; // (260321Ch)
                            }
                            NativeWrapper.PartialPivLuSolve(bLen, eigenVectors, psi0Native2, ref alpha); // (260321Ch)
                        }
                        else
                        {
                            var evd = new DMat(bLen, bLen, eigenMatrix.AsSpan(0, eigenMatrixLength).ToArray()).Evd(Symmetricity.Asymmetric);
                            eigenValues = ((DVec)evd.EigenValues).Values;
                            eigenVectors = ((DMat)evd.EigenVectors).Values;

                            var psi0 = new DVec(new Complex[bLen]) { [0] = One };
                            alpha = ((DVec)(evd.EigenVectors as DMat).LU().Solve(psi0)).Values;
                        }

                        if (EigenEnabled && muBackFiltered != null)
                        {
                            var result = NativeWrapper.EBSDSolverWithTDS(bLen, eigenValues, eigenVectors, alpha, phaseFiltered, sigmaArray, muBackFiltered, tdsCoeff, Thicknesses); // (260321Ch)
                            intensity = result.intensity;
                            background = result.tdsIntensity;
                        }
                        else if (EigenEnabled)
                        {
                            intensity = NativeWrapper.EBSDSolver(bLen, eigenValues, eigenVectors, alpha, phaseFiltered, sigmaArray, Thicknesses); // (260321Ch)
                        }
                        else
                        {
                            intensity = EBSDSolverManaged(bLen, nAtoms, eigenValues, eigenVectors, alpha, phaseFiltered, sigmaArray, Thicknesses);
                            if (muBackFiltered != null)
                                background = ComputeTDSMatrixBackground(bLen, eigenValues, eigenVectors, alpha, muBackFiltered, tdsCoeff, Thicknesses);
                        }

                        if (background != null)
                            ebsdBackground[i] = background;

                        ebsdIntensity[i] = intensity;

                        var completedDirectionCount = Interlocked.Add(ref count, symmetryRepresentativeWeights[i]); // (260327Ch) progress は full grid 基準で進める
                        if (completedDirectionCount / 50 != (completedDirectionCount - symmetryRepresentativeWeights[i]) / 50
                            || completedDirectionCount == BeamDirections.Length * (vIndex + 1))
                            bwEBSDNew.ReportProgress(completedDirectionCount, reportString);
                    });

                    for (int i = 0; i < BeamDirections.Length; i++)
                    {
                        var representativeIndex = symmetryRepresentativeMapping[i];
                        if (representativeIndex < 0 || representativeIndex == i) // 260331Cl 無効セル (-1) もスキップ
                            continue;

                        ebsdIntensity[i] = ebsdIntensity[representativeIndex]; // (260327Ch) 未計算方向は exact representative から埋め戻す
                        if (ebsdBackground != null)
                            ebsdBackground[i] = ebsdBackground[representativeIndex]; // (260327Ch)
                    }
                }
                finally
                {
                    foreach (var beams in threadLocalBeams.Values.OfType<Beam[]>()) // (260403Ch) null 除外は OfType に寄せる
                        ArrayPool<Beam>.Shared.Return(beams);
                    foreach (var gMap in threadLocalGMap.Values.OfType<int[]>())
                        ArrayPool<int>.Shared.Return(gMap);
                    foreach (var phaseFiltered in threadLocalPhaseFiltered.Values.OfType<Complex[]>())
                        Shared.Return(phaseFiltered);
                    foreach (var muBackFiltered in threadLocalMuBackFiltered.Values.OfType<Complex[]>())
                        Shared.Return(muBackFiltered);
                    foreach (var eigenMatrix in threadLocalEigenMatrix.Values.OfType<Complex[]>())
                        Shared.Return(eigenMatrix);
                    foreach (var psi0 in threadLocalPsi0.Values.OfType<Complex[]>())
                        Shared.Return(psi0);
                    foreach (var alpha in threadLocalAlpha.Values.OfType<Complex[]>())
                        Shared.Return(alpha);
                    foreach (var eigenValues in threadLocalEigenValues.Values.OfType<Complex[]>())
                        Shared.Return(eigenValues);
                    foreach (var eigenVectors in threadLocalEigenVectors.Values.OfType<Complex[]>())
                        Shared.Return(eigenVectors);

                    threadLocalBeams.Dispose(); // (260321Ch)
                    threadLocalGMap.Dispose(); // (260321Ch)
                    threadLocalPhaseFiltered.Dispose(); // (260321Ch)
                    threadLocalMuBackFiltered.Dispose(); // (260321Ch)
                    threadLocalEigenMatrix.Dispose(); // (260321Ch)
                    threadLocalPsi0.Dispose(); // (260321Ch)
                    threadLocalAlpha.Dispose(); // (260321Ch)
                    threadLocalEigenValues.Dispose(); // (260321Ch)
                    threadLocalEigenVectors.Dispose(); // (260321Ch)
                }
                #endregion
            }
            finally
            {
                foreach (var preliminary in beamsPreliminary)
                {
                    ReturnMasterPatternBuffer(preliminary.PotentialMatrix); // (260321Ch)
                    ReturnMasterPatternBuffer(preliminary.PhaseNG); // (260321Ch)
                }

                if (muBackArrays != null)
                    foreach (var muBack in muBackArrays)
                        ReturnMasterPatternBuffer(muBack); // (260321Ch)
            }

            #region Disk への格納
            Disks[vIndex] = new CBED_Disk[Thicknesses.Length];
            Parallel.For(0, Thicknesses.Length, t =>
            {
                var amplitudes = new Complex[BeamDirections.Length];
                for (int r = 0; r < BeamDirections.Length; r++)
                    if (ebsdIntensity[r] is not null)
                    {
                        var signal = ebsdIntensity[r][t];
                        var tds = ebsdBackground?[r]?[t] ?? 0;
                        amplitudes[r] = new Complex(Math.Sqrt(Math.Max(0, signal + tds)), 0);
                    }

                Disks[vIndex][t] = new CBED_Disk([0, 0, 0], new Vector3DBase(0, 0, 0),
                    Thicknesses[t], amplitudes);
                Disks[vIndex][t].Amplitudes = Disks[vIndex][t].RawAmplitudes;
            });
            #endregion

            if (bwEBSDNew.CancellationPending)
                e.Cancel = true;
        }
    }

    /// <summary>
    /// EigenEnabled = false (ネイティブライブラリなし) の場合のフォールバック。
    /// マネージドコードで EBSD 強度を計算する。
    ///
    /// 計算手順:
    ///   1. S 行列の構築 : S_{jj'} = Σ_n σ_n β_n^(j) conj(β_n^(j'))
    ///   2. λ の事前計算 : λ_{jj'} = 2πi(γ_j - conj(γ_j'))
    ///   3. 強度の計算   : I(t) = Real{ Σ_{jj'} S_{jj'} × F_{jj'}(t) }
    ///
    /// ネイティブ版 (_EBSDSolver) と数学的に等価だが、C# の手動ループで実行されるため
    /// Eigen の BLAS ルーチンを使うネイティブ版より遅い。
    /// </summary>
    /// <param name="bLen">ブロッホ波の数 (= 考慮する逆格子ベクトルの数)</param>
    /// <param name="nAtoms">原子の数 (対称等価位置を含む)</param>
    /// <param name="eigenValues">固有値 γ_j (bLen 個)</param>
    /// <param name="eigenVectors">固有ベクトル C_g^(j) (bLen×bLen, column-major)</param>
    /// <param name="alpha">励起振幅 α_j (bLen 個)</param>
    /// <param name="phaseNG">位相因子 P[n,g] = exp(2πi g·r_n) (nAtoms×bLen, column-major)</param>
    /// <param name="sigma">後方散乱断面積 σ_n (nAtoms 個)</param>
    /// <param name="thicknesses">厚さの配列 (tLen 個, nm 単位)</param>
    /// <returns>各厚さでの EBSD 強度 (tLen 個)</returns>
    private static double[] EBSDSolverManaged(
        int bLen, int nAtoms,
        Complex[] eigenValues, Complex[] eigenVectors, Complex[] alpha,
        Complex[] phaseNG, double[] sigma, double[] thicknesses)
    {
        int tLen = thicknesses.Length;

        #region Step 1: S 行列の計算
        // ================================================================
        // Step 1: S 行列の計算
        //
        // S_{jj'} = Σ_n σ_n β_n^(j) conj(β_n^(j'))
        //
        // β_n^(j) = α_j × μ_n^(j)
        //         = α_j × Σ_g C_g^(j) exp(2πi g·r_n)
        //
        // 物理的意味:
        //   μ_n^(j) はブロッホ状態 j の波動関数を原子位置 r_n で評価した値。
        //   α_j はそのブロッホ状態の励起振幅。
        //   β_n^(j) はこれらの積で、「原子 n における状態 j の寄与」を表す。
        //   S_{jj'} は全原子にわたる β の相関を集約したもの。
        //
        // 行列積としての解釈:
        //   B[n,j] = β_n^(j) = Σ_g P[n,g] × C[g,j] × α_j
        //   S = B† diag(σ) B
        //   ネイティブ版ではこの行列積を Eigen の BLAS で高速に計算する。
        //
        // メモリ最適化:
        //   B 全体 (nAtoms×bLen) を保持せず、原子ごとに β_n を
        //   stackalloc した Span に計算し、即座に S に蓄積する。
        //   これにより GC プレッシャーを回避する。
        // ================================================================
        var S = new Complex[bLen * bLen];
        Span<Complex> betaN = stackalloc Complex[bLen];
        for (int n = 0; n < nAtoms; n++)
        {
            double sig = sigma[n];
            // β_n^(j) を原子 n について計算
            for (int j = 0; j < bLen; j++)
            {
                // μ_n^(j) = Σ_g C_g^(j) × P(n,g)
                // eigenVectors[j * bLen + g] = C_g^(j) (column-major: j列目のg行目)
                // phaseNG[g * nAtoms + n] = P(n,g) (column-major: g列目のn行目)
                Complex mu = 0;
                for (int g = 0; g < bLen; g++)
                    mu += eigenVectors[j * bLen + g] * phaseNG[g * nAtoms + n];
                betaN[j] = alpha[j] * mu;
            }

            // S に蓄積: S_{jj'} += σ_n × β_n^(j) × conj(β_n^(j'))
            // S は Hermitian (S_{jj'} = conj(S_{j'j})) だが、
            // 最終的に Tr(S·F) の実部だけを使うので、全要素を計算して問題ない。
            for (int j = 0; j < bLen; j++)
            {
                var bj = betaN[j];
                for (int jp = 0; jp < bLen; jp++)
                    S[j * bLen + jp] += sig * bj * betaN[jp].Conjugate();
            }
        }
        #endregion

        #region Step 2: λ_{jj'} の事前計算
        // ================================================================
        // Step 2: λ_{jj'} の事前計算
        //
        // λ_{jj'} = 2πi (γ_j - conj(γ_j'))
        //         = 2πi (Re(γ_j) - Re(γ_j')) + 2πi · i · (Im(γ_j) + Im(γ_j'))
        //         = 2πi (Re(γ_j) - Re(γ_j')) - 2π (Im(γ_j) + Im(γ_j'))
        //
        // 実部: -2π(Im(γ_j) + Im(γ_j'))  → 減衰を制御
        //   Im(γ_j) > 0 なので実部は負 → exp(λt) は t とともに減衰
        //
        // 虚部: 2π(Re(γ_j) - Re(γ_j'))   → 振動を制御
        //   j ≠ j' のとき Re(γ_j) ≠ Re(γ_j') → 振動的な厚さ依存性
        //   これが菊池バンドの厚さ変化を生む
        //
        // j = j' のとき:
        //   λ_{jj} = -4π Im(γ_j) (純実数、負)
        //   F_{jj}(t) = [1 - exp(-4π Im(γ_j) t)] / (4π Im(γ_j))
        //   → 単調に t とともに増加（飽和あり）
        //
        // 吸収の物理 (電子回折の場合):
        //   Im(γ_j) > 0 のブロッホ状態は結晶内で減衰する。
        //   type-1 状態（原子位置に集中）は TDS による吸収が最大 → Im(γ) が最大。
        //   これはX線のボルマン効果（type-1 が最小吸収）とは逆。
        // ================================================================
        var lambda = new Complex[bLen * bLen];
        for (int j = 0; j < bLen; j++)
            for (int jp = 0; jp < bLen; jp++)
                lambda[j * bLen + jp] = TwoPiI * (eigenValues[j] - eigenValues[jp].Conjugate());
        #endregion

        #region Step 3: 各厚さでの強度計算
        // ================================================================
        // Step 3: 各厚さでの強度計算
        //
        // I(t) = Real{ Σ_{jj'} S_{jj'} × F_{jj'}(t) }
        //
        // F_{jj'}(t) = [exp(λ_{jj'} t) - 1] / λ_{jj'}
        //
        // λ_{jj'} ≈ 0 の場合 (縮退した固有値):
        //   ロピタルの定理より F → t
        //
        // I(t) は S の Hermitian 性から理論的に実数。
        // 数値誤差で微小な負値が出ることがあるため、0 でクランプする。
        // ================================================================
        var intensity = new double[tLen];
        for (int t = 0; t < tLen; t++)
        {
            double thick = thicknesses[t];
            Complex sum = 0;
            for (int j = 0; j < bLen; j++)
                for (int jp = 0; jp < bLen; jp++)
                {
                    var lam = lambda[j * bLen + jp];
                    var F = lam.MagnitudeSquared() < 1e-30 ? thick : (Exp(lam * thick) - One) / lam;
                    sum += S[j * bLen + jp] * F;
                }
            intensity[t] = Math.Max(0, sum.Real);
        }
        #endregion

        return intensity;
    }


    /// <summary>
    /// STEM 整合型 TDS バックグラウンドを U' 行列形式で計算する。 (260316Cl 追加)
    /// EigenEnabled=false の Managed フォールバック時に使用。
    /// EigenEnabled=true の場合は _EBSDSolverWithTDS (C++ Eigen) で一括計算される。
    ///
    /// 【物理モデル — STEM と同じ枠組み】
    /// Bloch波が結晶内を伝搬する際、TDS により散乱される電子のうち
    /// 後方半球 (θ ∈ [π/2, π]) に散乱される成分を計算する。
    /// STEM-HAADF で検証済みの手法 (tc†·U'·tc の深さ積分) と同じ行列形式を用いる。
    ///
    /// 【計算式】
    ///   I_TDS(t) = coeff × Re{ Σ_{jj'} M_{jj'} × F_{jj'}(t) }
    ///
    /// ここで:
    ///   M_{jj'} = α_j* × [C† · U'_back · C]_{jj'} × α_{j'}
    ///   F_{jj'}(t) = [exp(λ_{jj'} t) - 1] / λ_{jj'}   ← コヒーレント信号と同じ F 行列
    ///   λ_{jj'} = 2πi (γ_j - conj(γ_{j'}))
    ///   coeff = 2π / k_vac
    ///
    /// U'_back は後方散乱半球の TDS 散乱行列:
    ///   U'_back[i,j] = getU(kV, g_i - g_j, null, π/2, π).Imag.Conjugate()
    ///   FactorImaginaryAnnular で θ ∈ [π/2, π] の角度範囲を積分。
    ///
    /// 【STEM との整合性】
    ///   STEM: I_TDS = ∫ tc†(z) · U'_det · tc(z) dz  (検出器角度範囲)
    ///   EBSD: I_TDS = ∫ tc†(z) · U'_back · tc(z) dz (後方散乱半球)
    ///   同じ F 行列を使うため、コヒーレント信号と同じ 1/μ スケーリングを持ち、
    ///   信号と同じオーダーの強度が得られる。
    /// </summary>
    private static double[] ComputeTDSMatrixBackground(
        int bLen, Complex[] eigenValues, Complex[] eigenVectors, Complex[] alpha,
        Complex[] muBack, double coeff, double[] thicknesses)
    {
        int tLen = thicknesses.Length;
        int bLen2 = bLen * bLen;
        var tdsIntensity = new double[tLen];

        // ArrayPool で一時行列をレンタルし GC 圧力を削減
        // (この関数は全検出器方向から並列呼び出しされるため重要)
        var tmp = Shared.Rent(bLen2);
        var M = Shared.Rent(bLen2);
        try
        {
            // Step 1: tmp = C† · muBack  (bLen × bLen)  — O(bLen³)
            // eigenVectors は column-major: eigenVectors[j * bLen + g] = C_g^(j)
            // muBack は column-major: muBack[col * bLen + row] = U'_back[row, col]
            // tmp[j, g2] = Σ_g conj(C[g,j]) × muBack[g, g2]
            for (int j = 0; j < bLen; j++)
                for (int g2 = 0; g2 < bLen; g2++)
                {
                    Complex s = 0;
                    for (int g = 0; g < bLen; g++)
                        s += eigenVectors[j * bLen + g].Conjugate() * muBack[g2 * bLen + g];
                    tmp[g2 * bLen + j] = s;
                }

            // Step 2: M_{jj'} = coeff × α_j* × [tmp · C]_{jj'} × α_{j'}  — O(bLen³)
            // M は厚さに依存しないため、厚さループの外で1回だけ計算する。
            for (int j = 0; j < bLen; j++)
            {
                var alphaJ_conj = coeff * alpha[j].Conjugate();
                for (int jp = 0; jp < bLen; jp++)
                {
                    Complex s = 0;
                    for (int g2 = 0; g2 < bLen; g2++)
                        s += tmp[g2 * bLen + j] * eigenVectors[jp * bLen + g2];
                    M[j * bLen + jp] = alphaJ_conj * s * alpha[jp];
                }
            }

            // Step 3: I_TDS(t) = Re{ Σ_{jj'} M_{jj'} × F_{jj'}(t) }  — O(bLen² × tLen)
            for (int t = 0; t < tLen; t++)
            {
                double thick = thicknesses[t];
                Complex sum = 0;
                for (int j = 0; j < bLen; j++)
                {
                    var gammaJ = eigenValues[j];
                    for (int jp = 0; jp < bLen; jp++)
                    {
                        var lam = TwoPiI * (gammaJ - eigenValues[jp].Conjugate());
                        var F = lam.MagnitudeSquared() < 1e-30 ? thick : (Exp(lam * thick) - One) / lam;
                        sum += M[j * bLen + jp] * F;
                    }
                }
                tdsIntensity[t] = Math.Max(0, sum.Real);
            }
        }
        finally { Shared.Return(tmp); Shared.Return(M); }

        return tdsIntensity;
    }

    #region お蔵入り // (260327Ch) 旧 EBSD solver と専用 helper は参照されなくなったのでコメントアウトして退避
    /*
    /// <summary>EBSD計算用 ずっと悩んでいたバージョン。 結局Claudeに助けてもらって、上のバージョンに落ち着いた。</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ebsd_DoWork_old(object sender, DoWorkEventArgs e)
    {
        var (solver, thread, voltages) = ((Solver, int, double[]))e.Argument;

        Disks = new CBED_Disk[voltages.Length][];
        int count = 0;

        var beamDirectionsP = BeamDirections.AsParallel();
        int width = (int)Math.Sqrt(BeamDirections.Length);
        double radius = width / 2.0;

        var lockObjs = ValueEnumerable.Range(0, BeamDirections.Length).Select(_ => new Lock()).ToArray();

        //bool inside(int i) => (i % width - radius + 0.5) * (i % width - radius + 0.5) + (i / width - radius + 0.5) * (i / width - radius + 0.5) <= radius * radius;
        //bool inside(int i) => true;

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

        for (int vIndex = 0; vIndex < voltages.Length; vIndex++)
        {
            AccVoltage = voltages[vIndex];
            //波数を計算
            var kvac = UniversalConstants.Convert.EnergyToElectronWaveNumber(AccVoltage);
            //U0を計算
            var u0 = getU(AccVoltage).Real.Real;
            uDictionary.Clear();

            //beamsの計算コストが非常に高いので、4×4のグリッドを作って、中心ピクセルのbeamsで代表する
            var grid = 2;
            var beamsPreliminary = beamDirectionsP
                .Where((e, i) => (i % width) % grid == grid / 2 && (i / width) % grid == grid / 2)
                .Select(e =>
                {
                    var beams = Find_gVectors(BaseRotation, getVecK0(kvac, u0, e), MaxNumOfBloch);
                    var potentialMatrix = getPotentialMatrix(beams);
                    return (beams, potentialMatrix);
                }).ToArray();
            //ここまで

            //diskAmplitude[r][t][g]
            var diskAmplitude = beamDirectionsP.WithDegreeOfParallelism(Math.Max(thread / 2, 1)).Select((beamDirection, i) =>
            {
                //if (!inside(i)) return (null, null);
                if (bwEBSD.CancellationPending) return (null, null);

                var coeff = Math.Abs(1.0 / beamDirection.Z); // = 1/cosTau
                var vecK0 = getVecK0(kvac, u0, beamDirection);

                //var beams = Find_gVectors(BaseRotation, vecK0, MaxNumOfBloch);
                var (beamsBase, potentialMatrix) = beamsPreliminary[(i / width) / grid * (width / grid) + (i % width) / grid];
                var beams = reset_gVectors(beamsBase, BaseRotation, vecK0).Where(e => e.P > 0).ToArray();//BeamsのPやQをリセット

                var bLen = beams.Length;
                var eigenMatrix = Shared.Rent(bLen * bLen);
                getEigenMatrix(bLen, beams, ref eigenMatrix, potentialMatrix);//ポテンシャル行列をセット //コスト高い

                //入射面での波動関数を定義
                var psi0 = new DVec([.. ValueEnumerable.Range(0, bLen).Select(g => g == 0 ? One : 0)]);

                Complex[] result;

                //ポテンシャル行列の固有値、固有ベクトルを取得し、resultに格納
                #region 各ソルバーによる計算
                //Eigen＿Eigenの場合
                if (solver == Solver.Eigen_Eigen && EigenEnabled)
                    result = NativeWrapper.CBEDSolver_Eigen(eigenMatrix, [.. psi0], Thicknesses);
                //Eigen_MKL あるいは Eigen_Managedの場合    
                else if (solver == Solver.Eigen_MKL)
                {
                    var evd = new DMat(bLen, bLen, eigenMatrix).Evd(Symmetricity.Asymmetric);
                    var alpha = evd.EigenVectors.LU().Solve(psi0);
                    var resultMat = new DMat(bLen, Thicknesses.Length);
                    for (int t = 0; t < Thicknesses.Length; t++)
                    {
                        //ガンマの対称行列×アルファを作成
                        var gammmaAlpha = new DVec([.. evd.EigenValues.Select((ev, i) => Exp(TwoPiI * ev * t * coeff) * alpha[i])]);
                        //深さtにおけるψを求める
                        resultMat.SetColumn(t, evd.EigenVectors.Multiply(gammmaAlpha));
                    }
                    result = resultMat.Values;
                }
                //MtxExp_Eigenの場合
                else if (solver == Solver.MtxExp_Eigen && EigenEnabled)
                    result = NativeWrapper.CBEDSolver_MatExp(eigenMatrix, [.. psi0], Thicknesses);
                //MtxExp_MKLの場合 
                else
                {
                    var resultMat = new DMat(bLen, Thicknesses.Length);
                    var matExp = (DMat)(TwoPiI * coeff * Thicknesses[0] * new DMat(bLen, bLen, eigenMatrix)).Exponential();
                    var vec = matExp.Multiply(psi0);
                    resultMat.SetColumn(0, vec);

                    if (Thicknesses.Length > 1)
                    {
                        if (Thicknesses[1] - Thicknesses[0] == Thicknesses[0])
                            matExp = (DMat)(TwoPiI * coeff * (Thicknesses[1] - Thicknesses[0]) * new DMat(bLen, bLen, eigenMatrix)).Exponential();
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
                    for (int b = 0; b < bLen; b++)
                        result[t * bLen + b] *= Exp(PiI * (beams[b].P - 2 * kvac * Surface.Z) * Thicknesses[t]);
                #endregion

                Shared.Return(eigenMatrix);//eigenMatrixを返却

                if (Interlocked.Increment(ref count) % 50 == 0)
                    bwEBSD.ReportProgress(count, reportString);//進捗状況を報告
                return (result, beams);
            }).ToArray();

            //count = 0;
            //bwEBSD.ReportProgress(0, "Compiling disks");

            var directDiskIntensities = new double[Thicknesses.Length][];
            Parallel.For(0, Thicknesses.Length, t =>
            {
                directDiskIntensities[t] = new double[BeamDirections.Length];
                for (int r = 0; r < directDiskIntensities[t].Length; r++)
                    if (diskAmplitude[r].result is not null)
                        directDiskIntensities[t][r] = diskAmplitude[r].result[t * diskAmplitude[r].beams.Length + 0].MagnitudeSquared();
            });

            var directDiskPositions = new PointD[BeamDirections.Length];
            Parallel.For(0, BeamDirections.Length, r =>
            {
                //var vec = BeamDirections[r] * new Vector3DBase(0, 0, kvac);//Ewald球中心(試料)から見た、逆格子ベクトルの方向
                var vec = kvac * BeamDirections[r];//Ewald球中心(試料)から見た、逆格子ベクトルの方向
                directDiskPositions[r] = new PointD(vec.X / vec.Z, vec.Y / vec.Z); //カメラ長 1 を想定した検出器上のピクセルの座標値を格納
            });

            double xMax = directDiskPositions.Max(e => e.X), xMin = directDiskPositions.Min(e => e.X);
            double yMax = directDiskPositions.Max(e => e.Y), yMin = directDiskPositions.Min(e => e.Y);

            //r1方向でg番目のベクトルに対応するダイレクト方向r2を調べ、強度をインコヒーレントに加算
            //for(int r1=0; r1< BeamDirections.Length; r1++)
            Parallel.For(0, BeamDirections.Length, r1 =>
            {
                var (result, beams) = diskAmplitude[r1];
                if (result is not null)
                    for (int g = 1; g < beams.Length; g++)
                    {
                        var vec = kvac * BeamDirections[r1] - beams[g].Vec;//Ewald球中心(試料)から見た、逆格子ベクトルの方向
                        double posX = vec.X / vec.Z, posY = vec.Y / vec.Z; //カメラ長 1 を想定した検出器上のピクセルの座標値を格納
                        if (posX < xMax && posX > xMin && posY < yMax && posY > yMin)
                        {
                            var r2 = getIndex(posX, posY, directDiskPositions, width);
                            if (r2 >= 0)
                                lock (lockObjs[r2])
                                    for (int t = 0; t < Thicknesses.Length; t++)
                                        directDiskIntensities[t][r2] += result[t * beams.Length + g].MagnitudeSquared();
                        }
                    }
            });

            Disks[vIndex] = new CBED_Disk[Thicknesses.Length];
            Parallel.For(0, Thicknesses.Length, t =>
            {
                Disks[vIndex][t] = new CBED_Disk([0, 0, 0], new Vector3DBase(0, 0, 0), Thicknesses[t],
                    [.. directDiskIntensities[t].Select(intensity => new Complex(Math.Sqrt(intensity), 0))]);
                Disks[vIndex][t].Amplitudes = Disks[vIndex][t].RawAmplitudes;
            });

            if (bwEBSD.CancellationPending)
                e.Cancel = true;
        }
    }

    */
    #endregion


 

    #endregion

    #region 平行ビーム電子回折

    /// <summary>平行ビームの電子回折計算</summary>
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
            if (EigenEnabled && maxNumOfBloch < 400)
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
        var gamma_alpha = new DVec([.. ValueEnumerable.Range(0, dim).Select(n => Exp(TwoPiI * EigenValues[n] * thickness) * alpha[n])]);

        //出射面での境界条件を考慮した位相にする (20230827)
        var p = new DiagonalMatrix(dim, dim, [.. Beams.Select(b => Exp(PiI * b.P * thickness))]);

        //深さZにおけるψを求める
        var psi_atZ = p.Multiply(new DMat(dim, dim, EigenVectors).Multiply(gamma_alpha));

        for (int i = 0; i < Beams.Length && i < dim; i++)
            Beams[i].Psi = psi_atZ[i];

        return Beams;
    }

    #endregion

    #region Precession electron diffraction

    /// <summary>PEDの計算</summary>
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

        var stepP = ParallelEnumerable.Range(0, step).WithDegreeOfParallelism(useEigen ? Environment.ProcessorCount : Math.Max(1, Environment.ProcessorCount / 4));

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

            stepP.ForAll(k =>
            {
                var (sin, cos) = Math.SinCos(2.0 * Math.PI * k / step);
                var beamRotation = Matrix3D.Rot(new Vector3DBase(cos, sin, 0), SemianglePED);
                //計算対象のg-Vectorsを決める。
                var potentialMatrix = Array.Empty<Complex>();
                var vecK0 = getVecK0(kvac, u0, beamRotation * new Vector3D(0, 0, -1));
                BeamsPED[k] = Find_gVectors(BaseRotation, vecK0, MaxNumOfBloch);
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
            if (EigenValuesPED[k] is not null)
            {
                var len = EigenValuesPED[k].Count;
                var psi0 = new DVec(new Complex[len]) { [0] = 1 };//入射面での波動関数を定義
                var alpha = EigenVectorsInversePED[k].Multiply(psi0);//アルファベクトルを求める
                                                                     //ガンマの対称行列×アルファを作成
                var gamma_alpha = new DVec([.. ValueEnumerable.Range(0, len).Select(n => Exp(TwoPiI * EigenValuesPED[k][n] * thickness) * alpha[n])]);
                //深さZにおけるψを求める
                var psi_atZ = EigenVectorsPED[k].Multiply(gamma_alpha);
                for (int i = 0; i < BeamsPED[k].Length && i < len; i++)
                    BeamsPED[k][i].Psi = psi_atZ[i];
            }
        });

        //最後に全てのビームをまとめる
        var compiled = new Dictionary<(int h, int k, int l), Beam>();
        foreach (var beamsEach in BeamsPED.Where(beams => beams is not null))
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
        var mat = BaseRotation * Crystal.MatrixInverseTransposed;
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

        AccVoltage = voltage;
        //Wavelength = UniversalConstants.Convert.EnergyToElectronWaveLength(voltage);
        BaseRotation = new Matrix3D(baseRotation);
        BeamDirections = beamDirections;
        Thicknesses = thicknesses;
        if (!bwSTEM.IsBusy)
            bwSTEM.RunWorkerAsync((solver, thread, cs, delta, sliceThickness, convergenceAngle, detAngleInner, detAngleOuter, thicknesses, defocusses, imageSize, resolution, sourceSize));
    }
    public unsafe void StemDoWork(object sender, DoWorkEventArgs e)
    {
        //MathNetの行列の内部は、1列目の要素、2列目の要素、という順番で格納されている

        var (solver, thread, cs, delta, sliceThickness, convergenceAngle, detAngleInner, detAngleOuter, thicknesses, defocusses, imageSize, resolution, sourceSize)
            = ((Solver, int, double, double, double, double, double, double, double[], double[], Size, double, double))e.Argument;

        var diameterPix = (int)Math.Sqrt(BeamDirections.Length);
        var radiusPix = diameterPix / 2.0;
        bool inside(int i) => (i % diameterPix - radiusPix + 0.5) * (i % diameterPix - radiusPix + 0.5) + (i / diameterPix - radiusPix + 0.5) * (i / diameterPix - radiusPix + 0.5) <= radiusPix * radiusPix;

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
        var psi0 = ValueEnumerable.Range(0, Beams.Length).Select(g => g == 0 ? -One : 0).ToArray();
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
                        var gammmaAlpha = new DVec([.. eVal[i].Select((ev, g) => Exp(TwoPiI * ev * thicknesses[t]) * α[i][g])]);//ガンマの対称行列×アルファを作成
                        tg.SetColumn(t, evd.EigenVectors * gammmaAlpha);//深さtにおけるψを求める
                    }
                    result = tg.Values;
                }
                #endregion

                kg_z[i] = [.. beams.Where(e => e is not null).Select(e => e.P / 2)];

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
        var mat = BaseRotation * Crystal.MatrixInverseTransposed;
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
        //260317Cl 変更: tcPArray.Lengthがループ内で毎回列挙されるのを防ぐためToArrayで具体化
        //var tcP = tc.AsParallel().Select((e, i) => (index: i, result: e, xy: k_xy[i])).Where(e => e.result is not null && A(e.xy)).Select(e => e.index);//.WithDegreeOfParallelism(1);
        var tcPArray = tc.Select((e, i) => (index: i, result: e, xy: k_xy[i])).Where(e => e.result is not null && A(e.xy)).Select(e => e.index).ToArray();
        var tcP = tcPArray.AsParallel();

        #region listを計算
        var list = new List<(int qIndex, int[] N, double[] R, Complex[] Lenz)>[tc.Length];
        //有効なディスクを判定するフラグ
        var flag = tc.Select(e => e is not null).ToArray();
        //最大のK値(計算したK0ベクトルの中で最もXY成分が大きいもの)を求める。収束角ではないことに注意(5%大きい)。
        var maxK = k_xy.Max(e => e.X);
        double coeff1 = radiusPix - 0.5, coeff2 = (radiusPix - 0.5) / maxK, coeff3 = (uint)(diameterPix - 1);
        tcP.ForAll(kIndex =>
        {
            list[kIndex] = [];
            foreach (var (qIndex, KQ) in qList.Select((b, i) => (m: i, KQ: k_xy[kIndex] + b.Vec.ToPointD)).Where(e => A(e.KQ)))
            {
                // double dX = KQ.X * coeff2 + coeff1, dY = -KQ.Y * coeff2 + coeff1;
                double dX = Math.FusedMultiplyAdd(KQ.X, coeff2, coeff1), dY = Math.FusedMultiplyAdd(-KQ.Y, coeff2, coeff1);//K+Q の X,Y座標(実数)
                int x = (int)(Math.Floor(dX)), y = (int)(Math.Floor(dY));//左上近接のX,Y座標(整数)
                int n0 = y * diameterPix + x, n1 = n0 + 1, n2 = n0 + diameterPix, n3 = n2 + 1;//それぞれのインデックス
                if ((uint)x < coeff3 && (uint)y < coeff3 && flag[n0] && flag[n1] && flag[n2] && flag[n3])//4つのインデックスが範囲内であることを判定
                {
                    double xx = dX - x, yy = dY - y;
                    // var r = new double[] { (1 - xx) * (1 - yy), xx * (1 - yy), (1 - xx) * yy, xx * yy };
                    var r = new double[]
                    {
                        Math.FusedMultiplyAdd(-xx, 1 - yy, 1 - yy),
                        Math.FusedMultiplyAdd(-xx, yy, xx),
                        Math.FusedMultiplyAdd(-xx, yy, yy),
                        xx * yy
                    };//比率を計算
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
        var threadLocalIElas = new ThreadLocal<Complex[]>(() => null, true); // (260403Ch) 弾性項の一時配列はスレッドごとに使い回す
        try
        {
            tcP.ForAll(kIndex =>
            {
                var iElas = threadLocalIElas.Value ??= Shared.Rent(tLen); // (260403Ch) tLen はこの呼び出し中で固定

                foreach (var (qIndex, n, r, lenz) in CollectionsMarshal.AsSpan(list[kIndex]))
                {
                    iElas.AsSpan(0, tLen).Clear(); // (260403Ch) 使用範囲だけ初期化する
                    foreach (var (g, g_q) in g_qIndex[qIndex].Where(e => D(Beams[e.g].Vec.ToPointD + k_xy[kIndex])))
                        for (int t = 0; t < tLen; t++)
                        {
                            //i_Elas[t] += 1;
                            iElas[t] += tc[kIndex][t][g] * (r[0] * tc[n[0]][t][g_q] + r[1] * tc[n[1]][t][g_q] + r[2] * tc[n[2]][t][g_q] + r[3] * tc[n[3]][t][g_q]).Conjugate();
                        }
                    lock (lockObj1)
                        for (int t = 0; t < tLen; t++)
                            for (int d = 0; d < dLen; d++)
                                I_Elas[qIndex, t, d] += iElas[t] * lenz[d];
                }
                if (bwSTEM.CancellationPending) return;
                if (Interlocked.Increment(ref count) % 10 == 0) bwSTEM.ReportProgress((int)(1E6 * count / tcPArray.Length), "Calculating I_elastic(Q)");//状況を報告
            });
        }
        finally
        {
            foreach (var iElas in threadLocalIElas.Values.OfType<Complex[]>()) // (260403Ch)
                Shared.Return(iElas);
            threadLocalIElas.Dispose(); // (260403Ch)
        }
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
                _thick[t] = [.. ValueEnumerable.Range(1, slices).Select(e => start + tStep[t] * e)];
            }
            #endregion

            #region あらかじめeVecにαを掛けておく。
            Parallel.For(0, tc.Length, kIndex =>
            {
                if (eVal[kIndex] is not null)
                    for (int col = 0; col < bLen; col++)
                        for (int row = 0; row < bLen; row++)
                            eVec[kIndex][col * bLen + row] *= α[kIndex][col];
            });
            #endregion

            #region 各種変数の設定
            var tc_k = GC.AllocateUninitializedArray<Complex>(tc.Length * bLen);
            var validTc = list.Where(e1 => e1 is not null).SelectMany(e2 => e2.SelectMany(e3 => e3.N)).Distinct().ToList().AsParallel();
            var total = _thick.Sum(e => e.Length) * tcPArray.Length;
            count = 0;
            #endregion

            #region メインのループ
            var sumLen = qList.Count * dLen; // 260402Cl ArrayPool 化
            var threadLocalExpKgz = new ThreadLocal<Complex[]>(() => null, true); // (260402Ch) 非 Eigen 経路の指数配列はスレッド単位で再利用する
            var threadLocalExpLambda = new ThreadLocal<Complex[]>(() => null, true); // (260402Ch)
            var threadLocalSumTmp = new ThreadLocal<Complex[]>(() => null, true); // (260403Ch) 非弾性項の一時配列をスレッドごとに再利用する
            var threadLocalTcKq = new ThreadLocal<Complex[]>(() => null, true); // (260403Ch)
            try
            {
                for (int t = 0; t < Thicknesses.Length; t++)
                {
                    //var sum = new Complex[qList.Count * dLen];//ゼロ初期化が必要 // 260402Cl 変更前
                    var sum = Shared.Rent(sumLen); // 260402Cl ArrayPool 化
                    Array.Clear(sum, 0, sumLen); // ゼロ初期化が必要 (+=で累積)
                    try
                    {
                        foreach (var thickness in _thick[t])
                        {
                            if (bwSTEM.CancellationPending) return;

                            #region まず厚み_thick[t][_t]における透過係数_tc_kを計算
                            //validTc = validTc.WithDegreeOfParallelism(1);
                            validTc.ForAll(kIndex =>
                            {
                                if (EigenEnabled)
                                {
                                    fixed (Complex* _tc_k = tc_k, _eVal = eVal[kIndex], _eVec = eVec[kIndex])
                                    fixed (double* _kg_z = kg_z[kIndex])
                                        NativeWrapper.GenerateTC1(bLen, thickness, _kg_z, _eVal, _eVec, _tc_k + kIndex * bLen);
                                }
                                else
                                {
                                    var expKgz = threadLocalExpKgz.Value ??= Shared.Rent(bLen); // (260403Ch) bLen はこの呼び出し中で固定

                                    var expLambda = threadLocalExpLambda.Value ??= Shared.Rent(bLen); // (260403Ch) bLen はこの呼び出し中で固定

                                    for (int i = 0; i < bLen; i++)
                                    {
                                        expKgz[i] = Exp(TwoPiI * kg_z[kIndex][i] * thickness);
                                        expLambda[i] = Exp(TwoPiI * eVal[kIndex][i] * thickness);
                                        tc_k[kIndex * bLen + i] = 0;
                                    }

                                    for (int g = 0; g < bLen; g++)
                                        for (int j = 0; j < bLen; j++)
                                            tc_k[kIndex * bLen + g] += eVec[kIndex][j * bLen + g] * expKgz[g] * expLambda[j];
                                }
                            });
                            #endregion

                            tcP.ForAll(kIndex =>
                            {
                                var sumTmpLength = list[kIndex].Count * dLen;
                                var sumTmp = threadLocalSumTmp.Value;
                                if (sumTmp is null || sumTmp.Length < sumTmpLength)
                                {
                                    if (sumTmp != null)
                                        Shared.Return(sumTmp); // (260403Ch)
                                    // Complex[] sumTmp = Shared.Rent(list[kIndex].Count * dLen), tc_kq = Shared.Rent(bLen); // (260403Ch) 変更前
                                    sumTmp = Shared.Rent(sumTmpLength); // (260403Ch) スレッドごとの一時バッファを使い回す
                                    threadLocalSumTmp.Value = sumTmp; // (260403Ch)
                                }

                                var tc_kq = threadLocalTcKq.Value ??= Shared.Rent(bLen); // (260403Ch) bLen はこの呼び出し中で固定

                                fixed (Complex* _tc_k = tc_k, _U = U, _tc_kq = tc_kq)
                                    for (int i = 0; i < list[kIndex].Count; i++)
                                    {
                                        Complex tmp;
                                        var (qIndex, n, r, lenz) = list[kIndex][i];
                                        //厚み_thick[t][_t]における透過係数_tc_kqを計算
                                        if (EigenEnabled)
                                        {
                                            NativeWrapper.BlendAndConjugate(bLen, _tc_k + n[0] * bLen, _tc_k + n[1] * bLen, _tc_k + n[2] * bLen, _tc_k + n[3] * bLen, r[0], r[1], r[2], r[3], _tc_kq);
                                            tmp = NativeWrapper.RowVec_SqMat_ColVec(bLen, _tc_kq, _U + qIndex * bLen2, _tc_k + kIndex * bLen);
                                        }
                                        else
                                        {
                                            MathNet.Numerics.LinearAlgebra.Vector<Complex> tc_kq_vec = new DVec(bLen);
                                            for (int j = 0; j < 4; j++)
                                                tc_kq_vec += (new DVec(tc_k[(n[j] * bLen)..((n[j] + 1) * bLen)]).Conjugate() * r[j]);
                                            tmp = tc_kq_vec * (new DMat(bLen, bLen, U[(qIndex * bLen2)..((qIndex + 1) * bLen2)]) * new DVec(tc_k[(kIndex * bLen)..((kIndex + 1) * bLen)]));
                                        }
                                        for (int dIndex = 0; dIndex < dLen; dIndex++)
                                            sumTmp[i * dLen + dIndex] = tmp * lenz[dIndex];
                                    }
                                lock (lockObj1)
                                    for (int i = 0; i < list[kIndex].Count; i++)
                                        for (int d = 0; d < dLen; d++)
                                            sum[list[kIndex][i].qIndex * dLen + d] += sumTmp[i * dLen + d];

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
                    finally
                    {
                        Shared.Return(sum); // (260402Ch) cancel / 例外時も返却する
                    }
                }
            }
            finally
            {
                foreach (var expKgz in threadLocalExpKgz.Values.OfType<Complex[]>()) // (260403Ch)
                    Shared.Return(expKgz);
                foreach (var expLambda in threadLocalExpLambda.Values.OfType<Complex[]>())
                    Shared.Return(expLambda);
                foreach (var sumTmp in threadLocalSumTmp.Values.OfType<Complex[]>())
                    Shared.Return(sumTmp);
                foreach (var tcKq in threadLocalTcKq.Values.OfType<Complex[]>())
                    Shared.Return(tcKq);
                threadLocalExpKgz.Dispose(); // (260402Ch)
                threadLocalExpLambda.Dispose(); // (260402Ch)
                threadLocalSumTmp.Dispose(); // (260403Ch)
                threadLocalTcKq.Dispose(); // (260403Ch)
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


            //    var total = tcPArray.Length;
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
                var rVec = new PointD(resolution * (x - cX), -resolution * (y - cY)) + shift;//Y座標はマイナス。
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

        var images = ValueEnumerable.Range(0, 4).Select(d => new double[width * height]).ToArray();

        var shift = Crystal.RotationMatrix * (Crystal.A_Axis + Crystal.B_Axis + Crystal.C_Axis) / 2;
        double cX = width / 2.0, cY = height / 2.0;
        Parallel.For(0, width * height, n =>
        {
            //単位格子軸の0.5倍だけシフトさせておく
            var r = new PointD(-(n % width - cX) * res + shift.X, -(height - 1 - n / width - cY) * res + shift.Y);
            //var sums = new Complex[2]; // 260402Cl 変更前
            Complex sumCry = default, sumTher = default; // 260402Cl ヒープ割り当て廃止
            foreach (var (uCry, uTher, vec) in gList)
            {
                var exp = Exp(vec * r * TwoPiI);
                sumCry += uCry * exp; // 260402Cl sums[0] → sumCry
                sumTher += uTher * exp; // 260402Cl sums[1] → sumTher
            }
            images[0][n] = phase ? sumCry.Magnitude : sumCry.Real; // 260402Cl 展開
            images[1][n] = phase ? sumCry.Phase : sumCry.Imaginary;
            images[2][n] = phase ? sumTher.Magnitude : sumTher.Real;
            images[3][n] = phase ? sumTher.Phase : sumTher.Imaginary;
        });
        return images;
    }
    #endregion

    #region HRTEM Simulation

    /// <summary></summary>
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
            _images[t] = [.. defocusses.Select(d => new double[width * height])];

            //各ピクセルの計算
            double cX = width / 2.0, cY = height / 2.0;
            var shift = Crystal.RotationMatrix * (Crystal.A_Axis + Crystal.B_Axis + Crystal.C_Axis) / 2;
            if (native && EigenEnabled)//ネイティブC++で実行 3倍速い
            {
                var (gPsi, gVec, gLenz) = NativeWrapper.HRTEM_Helper(gList);
                int divTotal = Environment.ProcessorCount * 4, step = width * height / divTotal;
                var threadLocalRVec = new ThreadLocal<double[]>(() => null, true); // (260403Ch) ネイティブ solver 入力座標をスレッドごとに再利用する
                try
                {
                    Parallel.For(0, divTotal, div =>
                    {
                        int start = step * div, count = div == divTotal - 1 ? width * height - start : step;
                        int vecLength = count * 2;
                        var rVec = threadLocalRVec.Value;
                        if (rVec is null || rVec.Length < vecLength)
                        {
                            if (rVec != null)
                                ArrayPool<double>.Shared.Return(rVec); // (260403Ch)
                            // var rVec = ValueEnumerable.Range(start, count).SelectMany(n => new[] { res * (n % width - cX) + shift.X, -res * (n / width - cY) + shift.Y }).ToArray(); // (260403Ch) 変更前
                            rVec = ArrayPool<double>.Shared.Rent(vecLength); // (260403Ch)
                            threadLocalRVec.Value = rVec; // (260403Ch)
                        }
                        for (int n = 0; n < count; n++)
                        {
                            int pixel = start + n;
                            int offset = n * 2;
                            rVec[offset] = res * (pixel % width - cX) + shift.X; // (260403Ch) Y座標はマイナス。
                            rVec[offset + 1] = -res * (pixel / width - cY) + shift.Y; // (260403Ch)
                        }
                        var results = NativeWrapper.HRTEM_Solver(gPsi, gVec, gLenz, rVec, quasiMode, vecLength); // (260403Ch) ArrayPool バッファの使用長を明示する
                        for (var i = 0; i < defLen; i++)
                            //260317Cl 変更: Array.Copy → Span.CopyTo
                            results.AsSpan(i * count, count).CopyTo(_images[t][i].AsSpan(start, count));
                    });
                }
                finally
                {
                    foreach (var rVec in threadLocalRVec.Values.OfType<double[]>()) // (260403Ch)
                        ArrayPool<double>.Shared.Return(rVec);
                    threadLocalRVec.Dispose(); // (260403Ch)
                }
            }
            else//Managed
            {
                var threadLocalSums = new ThreadLocal<Complex[]>(() => null, true); // (260402Ch) ピクセルごとの一時配列をスレッド単位で再利用する
                try
                {
                    Parallel.For(0, width * height, n =>
                    {
                        PointD r = new((n % width - cX) * res + shift.X, -(n / width - cY) * res + shift.Y), _vec = new(double.NaN, double.NaN);//Y座標はマイナス。
                        var sums = threadLocalSums.Value ??= Shared.Rent(defLen); // (260403Ch) defLen はこの呼び出し中で固定
                        sums.AsSpan(0, defLen).Clear(); // (260402Ch) 各画素の加算前に使用範囲だけ初期化する
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
                finally
                {
                    foreach (var sums in threadLocalSums.Values.OfType<Complex[]>()) // (260403Ch)
                        Shared.Return(sums);
                    threadLocalSums.Dispose(); // (260402Ch)
                }
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
    // 260316Cl nTheta, nPhi 引数を追加 (後方散乱用に求積点数を削減するため)
    // 260316Cl以前のシグネチャ:
    // public (Complex Real, Complex Imag) getU(in double kV, in Beam g, in Beam h = null, double inner = double.NaN, double outer = double.NaN)
    public (Complex Real, Complex Imag) getU(in double kV, in Beam g, in Beam h = null, double inner = double.NaN, double outer = double.NaN, int nTheta = 60, int nPhi = 20)
    {
        var key1 = compose(g.Index);
        var key2 = h is null ? int.MaxValue : compose(h.Index);
        if (!uDictionary.TryGetValue((key1, key2), out (Complex real, Complex imag) U))
        {
            var index = h is null ? g.Index : g.Index.Minus(h.Index);// (g.H - h.H, g.K - h.K, g.L - h.L) ;

            var s2 = h is null ? g.Vec.Length2 / 4 : (g.Vec - h.Vec).Length2 / 4;
            //var k0 = UniversalConstants.Convert.EnergyToElectronWaveNumber(kV);
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

                            imag = m == 0 ? 0 : double.IsNaN(inner * outer) ? es.FactorImaginary(kV, s2, m) :
                                h is null ? es.FactorImaginaryAnnular(kV, g.Vec, m, inner, outer, nTheta, nPhi) : es.FactorImaginaryAnnular(kV, g.Vec, h.Vec, m, inner, outer, nTheta, nPhi);//非弾性散乱因子 答えは無次元
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
                uDictionary.TryAdd((key1, key2), U);
        }
        return U;
    }
    /// <summary>局所ポテンシャル形式で計算</summary>
    /// <param name="g"></param>
    /// <param name="h"></param>
    /// <returns></returns>
    public (Complex Real, Complex Imag) getU(Beam g) => getU(AccVoltage, g);

    /// <summary>g=0 のuの値を得る</summary>
    /// <param name="voltage"></param>
    /// <returns></returns>
    public (Complex Real, Complex Imag) getU(double voltage) => getU(voltage, new Beam((0, 0, 0), new Vector3DBase(0, 0, 0)));

    /// <summary>
    /// 260316Cl 追加
    /// Find_gVectors の最終ループ向け最適化オーバーロード (h=null, inner/outer=NaN の場合)。
    /// uDictionary のキャッシュヒット時は new Beam() のヒープ割り当てを一切行わない。
    /// キャッシュミス時 (初回のみ) は既存メソッドに委譲するため、ロジックの重複はない。
    /// Beam は class (参照型) なので、旧実装 getU(kV, new Beam(index, vec)) は
    /// Find_gVectors の最終ループで count 回のヒープ割り当てを発生させていた。
    /// </summary>
    public (Complex Real, Complex Imag) getU(in double kV, in (int h, int k, int l) index, in Vector3DBase vec)
    {
        var key1 = compose(index);
        const int key2 = int.MaxValue; // h = null に相当するキー
        // キャッシュヒット → Beam 生成なしで即返却 (= 割り当てゼロ)
        if (uDictionary.TryGetValue((key1, key2), out (Complex real, Complex imag) U))
            return U;
        // キャッシュミス (初回のみ) → 既存メソッドに委譲。一時 Beam は 1 度だけ生成される。
        return getU(kV, new Beam(index, vec));
    }

    private readonly ConcurrentDictionary<(int Key1, int Key2), (Complex Real, Complex Imag)> uDictionary = [];
    #endregion

    #region ポテンシャルのマトリックス
    /// <summary>ポテンシャルマトリックスを求める. k0の単位はnm^-1.</summary>
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

    /// <summary>
    /// 260316Cl 追加
    /// 非局所形式のポテンシャルマトリックスを求める。
    /// 弾性散乱因子(実部)は局所形式と同一 (g-g' のみに依存)。
    /// 吸収ポテンシャル(虚部)は非局所形式で計算: U'(g, g') は g と g' の絶対位置に依存する。
    ///
    /// 物理的背景:
    ///   局所形式: U'_{gg'} = U'(g-g')  → TDS散乱の角度分布を無視
    ///   非局所形式: U'_{gg'} = U'(g, g') → TDS散乱の角度分布を正確に反映
    ///   重い元素では散乱因子が前方に強くピークするため、
    ///   異なる g, g' ペアでの TDS カップリングが大きく変わる。
    ///   局所形式ではこの差を捉えられないため、パターンの強度分布が不正確になる。
    ///
    /// 計算コスト: 局所形式では Toeplitz 構造により ~N 個のユニークな計算で済むが、
    ///   非局所形式では N² 個の各要素ごとに2D数値積分 (Gauss-Legendre) が必要。
    /// </summary>
    /// <param name="b">ビーム配列</param>
    /// <param name="nonLocalInner">TDS積分の内角 [rad] (通常 0)</param>
    /// <param name="nonLocalOuter">TDS積分の外角 [rad] (全球: π)</param>
    private Complex[] getPotentialMatrix(Beam[] b, double nonLocalInner, double nonLocalOuter)
    {
        var potentialMatrix = GC.AllocateUninitializedArray<Complex>(b.Length * b.Length);
        getPotentialMatrix(b.Length, b, ref potentialMatrix, nonLocalInner, nonLocalOuter);
        return potentialMatrix;
    }

    private void getPotentialMatrix(int dim, Beam[] b, ref Complex[] potentialMatrix,
        double nonLocalInner, double nonLocalOuter)
    {
        for (int col = 0; col < dim; col++)
            for (int row = 0; row < dim; row++)
            {
                // getU(kV, g, h, inner, outer) は h != null のとき非局所形式で虚部を計算:
                //   実部: f_e(|g-h|²/4) × 位相因子  → 局所形式と同一
                //   虚部: ∫∫ f(k-g)f(k-h)(1-exp(-2M...)) sinθ dθdφ → g,h の両方に依存
                var (Real, Imag) = getU(AccVoltage, b[col], b[row], nonLocalInner, nonLocalOuter);
                potentialMatrix[row + col * dim] = row == col ? ImaginaryOne * Imag : Real + ImaginaryOne * Imag;
            }
    }
    #endregion

    #region 固有値問題対象のマトリックス
    /// <summary>固有値問題マトリックスを求める. k0の単位はnm^-1. パフォーマンス上の理由から、一次元配列にしている。</summary>
    /// <param name="b"></param>
    /// <returns></returns>
    private Complex[] getEigenMatrix(Beam[] b, Complex[] potentialMatrix = null)
    {
        var eigenMatrix = GC.AllocateUninitializedArray<Complex>(b.Length * b.Length);//A行列確保
        getEigenMatrix(b.Length, b, ref eigenMatrix, potentialMatrix);
        return eigenMatrix;
    }
    /// <summary>固有値問題マトリックスを求める. k0の単位はnm^-1. パフォーマンス上の理由から、一次元配列にしている。メモリ節約したい場合はeigenMatrixをShared.Rentして渡すこと。</summary>
    /// <param name="dim"></param>
    /// <param name="b"></param>
    /// <param name="eigenMatrix"></param>
    /// <param name="potentialMatrix"></param>
    private void getEigenMatrix(int dim, Beam[] b, ref Complex[] eigenMatrix, Complex[] potentialMatrix = null)
    {
        bool isNull = potentialMatrix is null || potentialMatrix.Length != dim * dim;
        if (isNull)
        {
            potentialMatrix = Shared.Rent(dim * dim);//potentialMatrixをレンタル
            getPotentialMatrix(dim, b, ref potentialMatrix);
        }
        //A行列を決定
        // 260321Cl 変更前:
        // for (int col = 0; col < dim; col++)
        // {
        //     for (int row = 0; row < dim; row++)
        //         eigenMatrix[row + col * dim] = potentialMatrix[row + col * dim] / b[col].P;
        //     eigenMatrix[col * dim + col] += b[col].Q / b[col].P;
        // }
        for (int col = 0; col < dim; col++) // 260321Cl: 逆数を事前計算し除算→乗算へ（div は mul より約4倍遅い）
        {
            var invP = 1.0 / b[col].P;
            var colBase = col * dim;
            for (int row = 0; row < dim; row++)
                eigenMatrix[colBase + row] = potentialMatrix[colBase + row] * invP;
            eigenMatrix[colBase + col] += b[col].Q * invP; // 260321Cl: 対角成分に Q/P を加算
        }
        if (isNull)
            Shared.Return(potentialMatrix);//potentialMatrixを返却
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
    static int compose(int h, int k, int l) => ((h + 255) << 20) + ((k + 255) << 10) + l + 255;
    static int compose((int h, int k, int l) index) => ((index.h + 255) << 20) + ((index.k + 255) << 10) + index.l + 255;
    static (int h, int k, int l) decompose(int key) => ((key >> 20) - 255, ((key << 12) >> 22) - 255, ((key << 22) >> 22) - 255);

    /// <summary>格子タイプに対応する direction セットを返す。260318Cl 追加</summary>
    private FrozenSet<(int h, int k, int l)> GetDirection()
    {
        if (Crystal.Symmetry.LatticeTypeStr == "F") return directionF;
        else if (Crystal.Symmetry.LatticeTypeStr == "A") return directionA;
        else if (Crystal.Symmetry.LatticeTypeStr == "B") return directionB;
        else if (Crystal.Symmetry.LatticeTypeStr == "C") return directionC;
        else if (Crystal.Symmetry.LatticeTypeStr == "I") return directionI;
        else if (Crystal.Symmetry.LatticeTypeStr == "R" && Crystal.Symmetry.SpaceGroupHMsubStr == "H") return directionRH;
        else if (Crystal.Symmetry.CrystalSystemStr == "trigonal" || Crystal.Symmetry.CrystalSystemStr == "hexagonal") return directionHex;
        else return directionP;
    }

    /// <summary>
    /// baseRotation に基づく BFS 探索キャッシュを構築する。エワルド球テストは行わず、
    /// 逆格子点の座標 (gX, gY, gZ) と gLen を記録する。260318Cl 追加
    /// </summary>
    private void Build_gCache(Matrix3D baseRotation, int cacheLimit)
    {
        var direction = GetDirection();

        var mat = baseRotation * Crystal.MatrixInverseTransposed;
        var (m11, m12, m13, m21, m22, m23, m31, m32, m33) = mat.Tuple;
        matCache = mat.Tuple;

        var shift = direction.Max(dir => (mat * dir).Length) * 0.5;

        var outer = new PriorityQueue<int, double>();
        outer.Enqueue(compose(0, 0, 0), 0);
        var whole = new HashSet<int>(cacheLimit * 2) { compose(0, 0, 0) };
        var candidates = new List<(int key, double gX, double gY, double gZ, double gLen)>(cacheLimit) { (compose(0, 0, 0), 0, 0, 0, 0) };

        while (whole.Count < cacheLimit && outer.Count > 0)
        {
            if (!outer.TryPeek(out _, out var minGlen))
                break;
            var min = minGlen + shift * 3;
            while (outer.Count > 0 && outer.TryPeek(out _, out var currentGlen) && currentGlen < min)
            {
                outer.TryDequeue(out var key, out _);
                var (h1, k1, l1) = decompose(key);
                foreach (var (h2, k2, l2) in direction)
                {
                    int h = h1 + h2, k = k1 + k2, l = l1 + l2;
                    var newKey = compose(h, k, l);
                    if (whole.Add(newKey))
                    {
                        double gX = Dot3(m11, h, m12, k, m13, l), gY = Dot3(m21, h, m22, k, m23, l), gZ = Dot3(m31, h, m32, k, m33, l);
                        double gLen = Math.Sqrt(gX * gX + gY * gY + gZ * gZ);
                        candidates.Add((newKey, gX, gY, gZ, gLen));
                        outer.Enqueue(newKey, gLen);
                    }
                }
            }
        }

        gCache = [.. candidates];
    }

    /// <summary>候補となるgVectorを検索する.</summary>
    /// <param name="baseRotation">結晶方位</param>
    /// <param name="vecK0">ビーム方位</param>
    /// <param name="maxNumOfBloch">指定しない場合は MaxNumOfBloch を使用 </param>
    /// <returns></returns>
    public Beam[] Find_gVectors(Matrix3D baseRotation, Vector3DBase vecK0, int maxNumOfBloch = -1)
        => Find_gVectors(baseRotation, vecK0, Surface, maxNumOfBloch); // (260321Ch) 旧来の固定表面経路は wrapper として残す

    /// <summary>
    /// 候補となる gVector を検索する。
    /// 固定表面 EBSD では Surface を、結晶固定 master では方向ごとの局所表面法線を与える。
    /// </summary>
    public Beam[] Find_gVectors(Matrix3D baseRotation, Vector3DBase vecK0, Vector3DBase surface, int maxNumOfBloch = -1)
    {
        if (maxNumOfBloch == -1)
            maxNumOfBloch = MaxNumOfBloch;

        // 260318Cl 変更: BFS 探索 (Phase 1) とエワルド球スクリーニング (Phase 2) を分離
        // baseRotationとmaxNumOfBloch が BFS キャッシュを再利用し、vecK0 依存のスクリーニングのみ実行

        var mat = baseRotation * Crystal.MatrixInverseTransposed;

        #region Phase 1: キャッシュ構築 (baseRotation が変わった場合のみ)
        var cacheLimit = Math.Max(maxNumOfBloch * 64, 50_000);
        if (gCache == null || matCache != mat.Tuple || gCache.Length < cacheLimit)
            Build_gCache(baseRotation, cacheLimit);
        #endregion

        #region Phase 2: キャッシュ済み候補に対してエワルド球テストのみ実行
        var limit = maxNumOfBloch * 8;
        var pool = ArrayPool<(int cacheIndex, float rating)>.Shared.Rent(limit); // cacheIndex slot にキャッシュインデックスを格納
        var beamsSpan = pool.AsSpan(0, limit);

        double k0_2 = vecK0.Length2, k0 = vecK0.Length;
        var shift = GetDirection().Max(dir => (mat * dir).Length) * 0.5;
        var maxQ = Math.Abs(k0_2 - (k0 + shift) * (k0 + shift));

        var (kX, kY, kZ) = vecK0.Tuple;
        // var (sX, sY, sZ) = Surface.Tuple; // (260321Ch) 旧案: 常に固定平面表面法線を使っていた
        var (sX, sY, sZ) = surface.Tuple;
        int count = 0;
        for (int i = 0; i < gCache.Length && count < limit; i++)
        {
            var (_, gX, gY, gZ, gLen) = gCache[i];
            double vX = gX + kX, vY = gY + kY, vZ = gZ + kZ;
            double q = k0_2 - Dot3(vX, vX, vY, vY, vZ, vZ);

            if (Math.Abs(q) < maxQ && Dot3(sX, vX, sY, vY, sZ, vZ) > 0)
                beamsSpan[count++] = (i, (float)(gLen * q * q)); // キャッシュインデックスを格納
        }
        beamsSpan = beamsSpan[..count];
        #endregion

        count = Math.Min(count, maxNumOfBloch + 1);
        QuickSelect.Execute(beamsSpan, count, static (a, b) => a.rating.CompareTo(b.rating));

        var beams = GC.AllocateUninitializedArray<Beam>(count).AsSpan();
        for (int i = 0; i < count; i++)
        {
            var (key, gX, gY, gZ, _) = gCache[beamsSpan[i].cacheIndex];
            var (h, k, l) = decompose(key);
            double vX = gX + kX, vY = gY + kY, vZ = gZ + kZ;
            double q = k0_2 - Dot3(vX, vX, vY, vY, vZ, vZ), p = 2 * Dot3(sX, vX, sY, vY, sZ, vZ);
            var g = new Vector3DBase(gX, gY, gZ);
            beams[i] = new Beam((h, k, l), g, getU(AccVoltage, (h, k, l), g), (q, p));
        }
        ArrayPool<(int key, float rating)>.Shared.Return(pool);

        beams.Sort(static (a, b) => a.Rating.CompareTo(b.Rating));

        #region 接平面内座標が同じものを削除
        const double duplicateTol = 1E-6;
        var uniqueXY = new HashSet<long>(beams.Length * 2);
        var (axisU, axisV) = GetSurfaceTangentialAxes(surface); // (260321Ch) local-surface master ではグローバル XY ではなく接平面基底で重複を判定する
        int uniqueCount = 0;
        for (int i = 0; i < beams.Length; i++)
        {
            var b = beams[i];
            // long xKey = (long)Math.Round(b.Vec.X / duplicateTol, MidpointRounding.AwayFromZero); // (260321Ch) 旧案: 固定 (001) 表面を仮定してグローバル XY で比較していた
            // long yKey = (long)Math.Round(b.Vec.Y / duplicateTol, MidpointRounding.AwayFromZero); // (260321Ch)
            var tangentialU = b.Vec * axisU; // (260321Ch) 方向ごとの局所表面に沿った第 1 基底への射影
            var tangentialV = b.Vec * axisV; // (260321Ch) 方向ごとの局所表面に沿った第 2 基底への射影
            long xKey = (long)Math.Round(tangentialU / duplicateTol, MidpointRounding.AwayFromZero);
            long yKey = (long)Math.Round(tangentialV / duplicateTol, MidpointRounding.AwayFromZero);
            long xyKey = (xKey << 32) ^ (yKey & 0xFFFFFFFFL);

            if (uniqueXY.Add(xyKey))
                beams[uniqueCount++] = b;
        }
        beams = beams[..uniqueCount];
        #endregion

        int n = beams.Length - 1;
        for (int i = beams.Length - 1; i >= 1; i--)
            if (Math.Abs(beams[i].Rating - beams[i - 1].Rating) > 1E-6)
            {
                n = i;
                break;
            }
        return [.. beams[..n]];
    }

    /// <summary>
    /// 指定した表面法線に対する接平面直交基底を返す。
    /// fixed-surface EBSD では XY 軸に近い基底が返り、local-surface master では
    /// 各方向で自然に回る接平面座標系が得られる。
    /// </summary>
    private static (Vector3DBase AxisU, Vector3DBase AxisV) GetSurfaceTangentialAxes(Vector3DBase surface)
    {
        var normal = Vector3DBase.Normarize(surface ?? new Vector3DBase(0, 0, 1));
        var reference = Math.Abs(normal.Z) < 0.9
            ? new Vector3DBase(0, 0, 1)
            : new Vector3DBase(0, 1, 0); // (260321Ch) 法線とほぼ平行にならない参照軸を選ぶ
        var axisU = Vector3DBase.VectorProduct(reference, normal);
        if (axisU.Length2 < 1E-20)
            axisU = Vector3DBase.VectorProduct(new Vector3DBase(1, 0, 0), normal); // (260321Ch) 念のための退避経路
        axisU = Vector3DBase.Normarize(axisU);
        var axisV = Vector3DBase.Normarize(Vector3DBase.VectorProduct(normal, axisU));
        return (axisU, axisV);
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
            return [.. beams.Where(b => (b.Vec.ToPointD - center).Length2 < r * r)];
        }
    }
    #endregion

    #region P, Q のリセットやゲット

    /// <summary>引数のBeamsとrotationをもとに、PとQだけセットして返す。ほかのパラメータは放置.</summary>
    /// <param name="baseRotation"></param>
    /// <param name="k0"></param>
    /// <returns></returns>
    public Beam[] reset_gVectors(Beam[] beams, Matrix3D baseRotation, Vector3DBase vecK0)
    {
        var newBeams = GC.AllocateUninitializedArray<Beam>(beams.Length);
        reset_gVectors(beams.Length, beams, baseRotation, vecK0, Surface, ref newBeams); // (260321Ch) 固定表面版 wrapper
        return newBeams;
    }

    /// <summary>引数の Beams と rotation をもとに、指定した表面法線で P と Q を再計算する。</summary>
    public Beam[] reset_gVectors(Beam[] beams, Matrix3D baseRotation, Vector3DBase vecK0, Vector3DBase surface)
    {
        var newBeams = GC.AllocateUninitializedArray<Beam>(beams.Length);
        reset_gVectors(beams.Length, beams, baseRotation, vecK0, surface, ref newBeams); // (260321Ch)
        return newBeams;
    }

    public void reset_gVectors(int dim, Beam[] beams, Matrix3D baseRotation, Vector3DBase vecK0, ref Beam[] newBeams)
        => reset_gVectors(dim, beams, baseRotation, vecK0, Surface, ref newBeams); // (260321Ch) 固定表面版 wrapper

    /// <summary>指定した表面法線で P, Q を再計算する内部実装。</summary>
    public void reset_gVectors(int dim, Beam[] beams, Matrix3D baseRotation, Vector3DBase vecK0, Vector3DBase surface, ref Beam[] newBeams)
    {
        // var mat = baseRotation * Crystal.MatrixInverseTransposed; // (260321Ch) 旧実装: 毎回 g = mat * hkl を再計算していた
        //var (m11, m12, m13, m21, m22, m23, m31, m32, m33) = mat.Tuple;
        for (int i = 0; i < dim; i++)
        {
            // var g = mat * beams[i].Index; // (260321Ch) 旧実装
            var g = beams[i].Vec; // (260321Ch) beamsBase は同じ baseRotation から作られているので既存 Vec をそのまま使う
            //var (h, k, l) = beams[i].Index;
            //double gX = m11 * h + m12 * k + m13 * l, gY = m21 * h + m22 * k + m23 * l, gZ = m31 * h + m32 * k + m33 * l;

            // var prms = getQP(g, vecK0); // (260321Ch) 旧案: 固定表面法線で P, Q を求めていた
            var prms = getQP(g, vecK0, surface);
            //var prms = getQP(beams[i].Vec, vecK0);
            // newBeams[i] = new Beam(beams[i].Index, beams[i].Vec, (beams[i].Ureal, beams[i].Uimag), prms); // (260321Ch) 旧実装: 毎回 Beam を new していた
            if (newBeams[i] == null)
                newBeams[i] = new Beam(beams[i].Index, beams[i].Vec, (beams[i].Ureal, beams[i].Uimag), prms); // (260321Ch) 初回だけ生成する
            else
            {
                newBeams[i].Index = beams[i].Index; // (260321Ch)
                newBeams[i].Vec = beams[i].Vec; // (260321Ch)
                newBeams[i].Ureal = beams[i].Ureal; // (260321Ch)
                newBeams[i].Uimag = beams[i].Uimag; // (260321Ch)
                newBeams[i].Q = prms.Q; // (260321Ch)
                newBeams[i].P = prms.P; // (260321Ch)
                newBeams[i].Psi = default; // (260321Ch)
                newBeams[i].intensity = 0; // (260321Ch)
                newBeams[i].Lenz = new Complex(1, 0); // (260321Ch)
            }
        }
    }



    private static double getQ(in Vector3DBase g, in Vector3DBase vecK0) => vecK0.Length2 - (vecK0 + g).Length2;

    private double getP(in Vector3DBase g, in Vector3DBase vecK0) => getP(g, vecK0, Surface); // (260321Ch) 固定表面版 wrapper

    /// <summary>指定した表面法線に対する P を求める。</summary>
    private static double getP(in Vector3DBase g, in Vector3DBase vecK0, Vector3DBase surface) => 2 * surface * (vecK0 + g);

    private (double Q, double P) getQP(in Vector3DBase g, in Vector3DBase vecK0) => getQP(g, vecK0, Surface); // (260321Ch) 固定表面版 wrapper

    /// <summary>指定した表面法線に対する Q, P を求める。</summary>
    private static (double Q, double P) getQP(in Vector3DBase g, in Vector3DBase vecK0, Vector3DBase surface) => (getQ(g, vecK0), getP(g, vecK0, surface));

    public (double Q, double P) getQP(in Vector3DBase g, double kvac, double u0, Vector3DBase beamDirection = null)
        => getQP(g, getVecK0(kvac, u0, beamDirection), GetEbsdSurfaceNormal(beamDirection)); // (260321Ch) local-surface mode に追従する

    #endregion

    #region K0ベクトルを求める
    /// <summary>K0ベクトルを求める。K0ベクトルは、XY方向を保存したままZ方向のみ変化する。</summary>
    /// <param name="beamRotation"></param>
    /// <param name="kvac"></param>
    /// <param name="u0"></param>
    /// <returns></returns>
    public Vector3DBase getVecK0(double kvac, double u0, Vector3DBase beamDirection = null)
        => getVecK0(kvac, u0, beamDirection, GetEbsdSurfaceNormal(beamDirection)); // (260321Ch) local-surface mode に追従する

    /// <summary>指定した表面法線に対して K0 ベクトルを求める。</summary>
    public Vector3DBase getVecK0(double kvac, double u0, Vector3DBase beamDirection, Vector3DBase surface)
    {
        // |k0|^2 - |kvac|^2 = u0
        // vecK0 = vecKvac + x * vecSurface
        //   =>   x^2 + 2 x * vecKvac - u0 = 0
        // を満たすxを求めれば良い。
        var vecKvac = (beamDirection == null) ? new Vector3DBase(0, 0, -kvac) : kvac * beamDirection;
        // var b = Surface * vecKvac; // (260321Ch) 旧案: 固定平面表面に対する屈折のみを考えていた
        var b = surface * vecKvac;
        var x = Math.Sqrt(b * b + u0) - b;
        return vecKvac + x * surface; // (260321Ch)
    }

    #endregion

    #region Beamクラス

    public class Beam
    {
        /// <summary>指数 h</summary>
        public int H => Index.H;
        /// <summary>指数 k</summary>
        public int K => Index.K;

        /// <summary>指数 l</summary>
        public int L => Index.L;

        /// <summary>指数 hkl</summary>
        public (int H, int K, int L) Index;

        /// <summary>逆格子ベクトル</summary>
        public Vector3DBase Vec;

        /// <summary>励起誤差</summary>
        public double S => Math.Sqrt(P * P / 4 + Q) - P / 2;

        public Complex Ureal;

        public Complex Uimag;

        /// <summary>k0^2 - (k0 + g)^2 = - g (2 k0 +g) (2 k0 S 励起誤差とわずかに定義が違う)</summary>
        public double Q;

        /// <summary>2 n (k0 + g) 大塚さんの資料参考</summary>
        public double P;

        /// <summary>振幅</summary>
        public Complex Psi;

        /// <summary>強度を保存する(PED計算の時のみ使用)</summary>
        public double intensity = 0;


        /// <summary>
        /// レンズ関数 
        /// 球面収差関数 × 時間的インコヒーレンス包絡関数 × 空間的インコヒーレンス包絡関数 
        /// </summary>
        public Complex Lenz = new(1, 0);

        /// <summary>評価値</summary>
        public double Rating => Vec.Length * Q * Q;

        /// <summary>コンストラクタ</summary>
        /// <param name="hkl">指数</param>
        /// <param name="vec">逆格子ベクトル</param>
        /// <param name="s">励起誤差</param>
        public Beam(in (int H, int K, int L) index, Vector3DBase vec, in (Complex Real, Complex Imag) u, in (double Q, double P) prms)
        {
            Index = index;
            Vec = vec;
            Ureal = u.Real;
            Uimag = u.Imag;
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
             => $"{H} {K} {L}, (x, y, z)=({Vec.X}, {Vec.Y}, {Vec.Z}), Length={Vec.Length}, Q={Q} ";
    }

    #endregion

    #region CBED_Diskクラス
    public class CBED_Disk(int[] hkl, Vector3DBase vec, double thickness, Complex[] amplitudes)
    {
        /// <summary>指数</summary>
        public readonly int H = hkl[0], K = hkl[1], L = hkl[2];

        /// <summary>厚み</summary>
        public readonly double Thickness = thickness;

        public readonly Vector3DBase G = vec;

        /// <summary>振幅を格納した配列</summary>
        public Complex[] Amplitudes;

        public readonly Complex[] RawAmplitudes = amplitudes;
    }
    #endregion

}

