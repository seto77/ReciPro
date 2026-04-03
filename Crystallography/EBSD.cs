#region using
using System;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
#endregion
namespace Crystallography;

/// <summary>
/// MasterPattern の作成処理を UI から切り離して管理するクラス。
/// BetheMethod による EBSD ディスク計算と、その結果を plane 配列へ並べ替える処理を担当する。
/// </summary>
public class EBSD
{
    /// <summary>MasterPattern 作成時に必要な条件をひとまとめにしたリクエスト。</summary>
    public sealed class MasterPatternBuildRequest
    {
        /// <summary>MasterPattern 作成条件を初期化する。</summary>
        public MasterPatternBuildRequest(
            Crystal crystal,
            int maxNumOfBloch,
            double[] energies,
            double[] depths,
            int gridSize,
            BetheMethod.Solver solver = BetheMethod.Solver.Auto,
            int thread = 1,
            bool useNonLocalAbsorption = false,
            bool includeTDSBackground = false)
        {
            Crystal = crystal ?? throw new ArgumentNullException(nameof(crystal));
            MaxNumOfBloch = maxNumOfBloch;
            Energies = energies?.ToArray() ?? [];
            Depths = depths?.ToArray() ?? [];
            GridSize = gridSize;
            GridType = MasterPattern.ShouldUseHexGrid(crystal.Symmetry)                 ? MasterPattern.Types.Hexagonal                : MasterPattern.Types.Square;// 260331Cl 追加: 結晶系から自動判定
            Solver = solver;
            Thread = thread;
            UseNonLocalAbsorption = useNonLocalAbsorption;
            IncludeTDSBackground = includeTDSBackground;
        }

        /// <summary>計算対象の結晶。</summary>
        public Crystal Crystal { get; }

        /// <summary>Bethe 計算で使うブロッホ波の最大数。</summary>
        public int MaxNumOfBloch { get; }

        /// <summary>計算対象のエネルギー列。</summary>
        public double[] Energies { get; }

        /// <summary>計算対象の深さ列。</summary>
        public double[] Depths { get; }

        /// <summary>Rosca-Lambert 平面の一辺の分割数。</summary>
        public int GridSize { get; }

        /// <summary>格子タイプ（正方/六方）。結晶系から自動判定。260331Cl 追加</summary>
        public MasterPattern.Types GridType { get; }

        /// <summary>BetheMethod で使う solver 種別。</summary>
        public BetheMethod.Solver Solver { get; }

        /// <summary>BetheMethod で使うスレッド数。</summary>
        public int Thread { get; }

        /// <summary>非局所吸収を有効にするかどうか。</summary>
        public bool UseNonLocalAbsorption { get; }

        /// <summary>TDS 背景を含めるかどうか。</summary>
        public bool IncludeTDSBackground { get; }

        /// <summary>Bethe 計算側の進捗を規格化するための基準値。</summary>
        public int DivisionCount => Math.Max(1, GridSize * GridSize * Math.Max(1, Energies.Length) * 2); // (260321Ch) MasterPattern は常に全球計算する

        /// <summary> 非同期実行中に UI 側の配列が変更されても影響しないよう、配列も複製して保持する。 </summary>
        public MasterPatternBuildRequest Clone()
            => new(Crystal, MaxNumOfBloch, Energies, Depths, GridSize, Solver, Thread, UseNonLocalAbsorption, IncludeTDSBackground);
    }

    /// <summary>MasterPattern 作成中の進捗情報。</summary>
    public sealed class MasterPatternProgressChangedEventArgs : EventArgs
    {
        /// <summary>進捗情報を初期化する。</summary>
        public MasterPatternProgressChangedEventArgs(int progressPercentage, object userState)
        {
            ProgressPercentage = progressPercentage;
            UserState = userState;
        }

        /// <summary>0 から 100 までの正規化済み進捗率。</summary>
        public int ProgressPercentage { get; }

        /// <summary>進捗表示用の補足文字列。</summary>
        public object UserState { get; }
    }

    /// <summary>MasterPattern 作成完了時の結果。</summary>
    public sealed class MasterPatternCompletedEventArgs : EventArgs
    {
        /// <summary>完了結果を初期化する。</summary>
        public MasterPatternCompletedEventArgs(MasterPatternBuildRequest request, MasterPattern masterPattern, Exception error, bool cancelled)
        {
            Request = request;
            MasterPattern = masterPattern;
            Error = error;
            Cancelled = cancelled;
        }

        /// <summary>今回の build 条件。</summary>
        public MasterPatternBuildRequest Request { get; }

        /// <summary>作成された MasterPattern。失敗またはキャンセル時は null。</summary>
        public MasterPattern MasterPattern { get; }

        /// <summary>失敗時の例外。成功時は null。</summary>
        public Exception Error { get; }

        /// <summary>キャンセルされたかどうか。</summary>
        public bool Cancelled { get; }
    }

    /// <summary>BetheMethod の出力ディスクを plane 配列へ並べ替える worker へ渡す引数。</summary>
    private sealed class MasterPatternCompilationArgument
    {
        public MasterPatternCompilationArgument(MasterPatternBuildRequest request, BetheMethod.CBED_Disk[][] disks)
        {
            Request = request;
            Disks = disks;
        }

        public MasterPatternBuildRequest Request { get; }
        public BetheMethod.CBED_Disk[][] Disks { get; }
    }

    /// <summary>plane 変換 worker の完了結果。</summary>
    private sealed class MasterPatternCompilationResult
    {
        public MasterPatternCompilationResult(MasterPatternBuildRequest request, MasterPattern masterPattern)
        {
            Request = request;
            MasterPattern = masterPattern;
        }

        public MasterPatternBuildRequest Request { get; }
        public MasterPattern MasterPattern { get; }
    }

    private const int BetheStageWeight = 99; // (260321Ch) 全体進捗のうち Bethe 計算へ割り当てる比率
    private const int CompileStageWeight = 1; // (260321Ch) 全体進捗のうち plane 変換へ割り当てる比率

    private BetheMethod masterPatternBethe = null; // (260321Ch) MasterPattern 専用の Bethe solver

    /// <summary>
    /// BetheMethod の結果を MasterPattern plane 配列へ変換するための worker。
    /// 旧案では RunWorkerCompleted 内で同期変換していたが、重い処理を UI スレッドへ戻さないために独立させた。
    /// </summary>
    public readonly BackgroundWorker bwMasterPattern;

    /// <summary>MasterPattern 管理用 worker 群を初期化する。</summary>
    public EBSD()
    {
        bwMasterPattern = new BackgroundWorker
        {
            WorkerSupportsCancellation = true,
            WorkerReportsProgress = true,
        };
        bwMasterPattern.DoWork += MasterPattern_DoWork;
        bwMasterPattern.ProgressChanged += MasterPattern_ProgressChanged;
        bwMasterPattern.RunWorkerCompleted += MasterPattern_RunWorkerCompleted;
    }

    /// <summary>最後に正常終了した MasterPattern。</summary>
    public MasterPattern MasterPattern { get; private set; } = null;

    private MasterPatternBuildRequest currentMasterPatternBuildRequest = null; // (260327Ch) build 条件はクラス内部だけで保持する

    /// <summary>Bethe 計算中または plane 変換中かどうかを返す。</summary>
    //public bool IsBuilding => bwMasterPattern.IsBusy || masterPatternBethe?.bwEBSD?.IsBusy == true || masterPatternBethe?.bwEBSDNew?.IsBusy == true; // (260321Ch) 新旧どちらの EBSD worker でも build 中と見なす
    public bool IsBuilding => bwMasterPattern.IsBusy || masterPatternBethe?.bwEBSDNew?.IsBusy == true; // (260327Ch) 旧 bwEBSD はお蔵入りにしたので ebsdNew 側だけを見る

    /// <summary>MasterPattern 作成中の進捗通知。</summary>
    public event EventHandler<MasterPatternProgressChangedEventArgs> MasterPatternProgressChanged;

    /// <summary>MasterPattern 作成完了通知。</summary>
    public event EventHandler<MasterPatternCompletedEventArgs> MasterPatternCompleted;

    /// <summary>
    /// MasterPattern 作成を開始する。
    /// まず BetheMethod で EBSD ディスクを作り、その完了後に別 worker で plane 配列へ変換する。
    /// </summary>
    public bool RunMasterPatternBuild(MasterPatternBuildRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        if (IsBuilding)
            return false;

        DetachMasterPatternBetheHandlers(); // (260321Ch) 前回の solver が残っていてもイベント配線を解除してから張り直す
        currentMasterPatternBuildRequest = request.Clone(); // (260327Ch)
        MasterPattern = null;

        // 260331Cl 格子タイプに応じた方向生成
        var gs = currentMasterPatternBuildRequest.GridSize;
        var isHex = currentMasterPatternBuildRequest.GridType == MasterPattern.Types.Hexagonal;
        var positiveDirections = isHex
            ? MasterPattern.CreateDirectionsHex(gs, MasterPattern.Hemisphere.PositiveZ)
            : MasterPattern.CreateDirectionsSquare(gs, MasterPattern.Hemisphere.PositiveZ);
        var negativeDirections = isHex
            ? MasterPattern.CreateDirectionsHex(gs, MasterPattern.Hemisphere.NegativeZ)
            : MasterPattern.CreateDirectionsSquare(gs, MasterPattern.Hemisphere.NegativeZ);
        var directions = new Vector3DBase[positiveDirections.Length + negativeDirections.Length]; // (260321Ch) 全球分の方向配列をここで素直に連結する
        Array.Copy(positiveDirections, 0, directions, 0, positiveDirections.Length);
        Array.Copy(negativeDirections, 0, directions, positiveDirections.Length, negativeDirections.Length);
        masterPatternBethe = new BetheMethod(currentMasterPatternBuildRequest.Crystal)
        {
            EbsdRepresentativeDirectionBlockSize = currentMasterPatternBuildRequest.GridSize <= 512 ? 1
                : currentMasterPatternBuildRequest.GridSize <= 1024 ? 3 : 5, // (260321Ch) 解像度に応じて 1 / 3 / 5 をその場で決める
            UseLocalSurfacePerBeamDirection = false // (260321Ch) 新経路では結晶を回すので、既存 worker 側の局所表面近似は使わない
        };
        masterPatternBethe.EBSD_ProgressChanged += MasterPatternBethe_EBSD_ProgressChanged;
        masterPatternBethe.EBSD_Completed += MasterPatternBethe_EBSD_Completed;

        try
        {
            masterPatternBethe.RunEBSDNew(currentMasterPatternBuildRequest.MaxNumOfBloch, currentMasterPatternBuildRequest.Energies, Matrix3D.IdentityMatrix, currentMasterPatternBuildRequest.Depths, directions, currentMasterPatternBuildRequest.Solver, currentMasterPatternBuildRequest.Thread, currentMasterPatternBuildRequest.UseNonLocalAbsorption, currentMasterPatternBuildRequest.IncludeTDSBackground);
            return true;
        }
        catch
        {
            DetachMasterPatternBetheHandlers();
            masterPatternBethe = null;
            throw;
        }
    }

    /// <summary>
    /// 進行中の MasterPattern 作成をキャンセルする。
    /// Bethe 計算中なら Bethe 側を、plane 変換中なら専用 worker 側を止める。
    /// </summary>
    public void CancelMasterPatternBuild()
    {
        if (masterPatternBethe?.bwEBSDNew?.IsBusy == true)
            masterPatternBethe.bwEBSDNew.CancelAsync(); // (260327Ch) 1 か所しか使わない helper はここへ畳み込む
        if (bwMasterPattern.IsBusy)
            bwMasterPattern.CancelAsync();
    }

    /// <summary>BetheMethod から届いた進捗を MasterPattern 全体の進捗へ換算して通知する。</summary>
    private void MasterPatternBethe_EBSD_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        var request = currentMasterPatternBuildRequest; // (260327Ch)
        if (request == null)
            return;

        var current = Math.Max(1, e.ProgressPercentage);
        var total = request.DivisionCount;
        var progress = Math.Min(BetheStageWeight, (int)Math.Round((double)current / total * BetheStageWeight));
        MasterPatternProgressChanged?.Invoke(this, new MasterPatternProgressChangedEventArgs(progress, e.UserState)); // (260327Ch) 未使用の詳細カウンタ通知を削除
    }

    /// <summary>
    /// BetheMethod の完了後に plane 変換 worker を起動する。
    /// 以前はここで同期的に MasterPattern を組み立てていた。
    /// </summary>
    private void MasterPatternBethe_EBSD_Completed(object sender, RunWorkerCompletedEventArgs e)
    {
        var request = currentMasterPatternBuildRequest; // (260327Ch)
        var completedBethe = masterPatternBethe;

        DetachMasterPatternBetheHandlers();
        masterPatternBethe = null;

        Exception error = e.Error;
        if (!e.Cancelled && error == null && completedBethe?.Disks != null && request != null)
        {
            try
            {
                bwMasterPattern.RunWorkerAsync(new MasterPatternCompilationArgument(request, completedBethe.Disks));
                return;
            }
            catch (Exception ex)
            {
                error = ex;
            }
        }

        MasterPattern = null;
        MasterPatternCompleted?.Invoke(this, new MasterPatternCompletedEventArgs(request, null, error, e.Cancelled));
    }

    /// <summary>BetheMethod の出力ディスクを plane 配列へ並べ替え、MasterPattern を生成する。</summary>
    private void MasterPattern_DoWork(object sender, DoWorkEventArgs e)
    {
        var argument = e.Argument as MasterPatternCompilationArgument;
        if (argument == null)
            throw new ArgumentNullException(nameof(e.Argument));

        var request = argument.Request;
        var planeCount = request.Energies.Length * request.Depths.Length;
        var planeCountForProgress = Math.Max(1, planeCount * 2); // (260321Ch) MasterPattern は常に +Z / -Z の 2 面を持つ
        var reportStep = Math.Max(1, planeCountForProgress / 100);
        var positivePlanes = new float[planeCount][];
        var negativePlanes = new float[planeCount][];
        var count = 0;

        bwMasterPattern.ReportProgress(0, "Compiling master planes"); // (260327Ch) 未使用の plane 数 state を持たない

        for (int energyIndex = 0; energyIndex < request.Energies.Length; energyIndex++)
            for (int depthIndex = 0; depthIndex < request.Depths.Length; depthIndex++)
            {
                if (bwMasterPattern.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                var planeIndex = energyIndex * request.Depths.Length + depthIndex;
                var disk = argument.Disks[energyIndex][depthIndex];
                if (disk?.Amplitudes == null)
                {
                    positivePlanes[planeIndex] = [];
                    negativePlanes[planeIndex] = [];
                    count += 2; // (260321Ch)
                }
                else
                {
                    var amplitudes = disk.Amplitudes;
                    var hemisphereLength = request.GridSize * request.GridSize;
                    // var positivePlane = new float[hemisphereLength]; // (260402Ch) 変更前
                    // var negativePlane = new float[hemisphereLength]; // (260402Ch) 変更前
                    var positivePlane = GC.AllocateUninitializedArray<float>(hemisphereLength); // (260402Ch) 直後に全要素を書き切るため未初期化で確保
                    var negativePlane = GC.AllocateUninitializedArray<float>(hemisphereLength); // (260402Ch)
                    for (int i = 0; i < hemisphereLength; i++)
                    {
                        var amplitude = i < amplitudes.Length ? amplitudes[i] : Complex.Zero; // (260321Ch) 念のため不足時は 0 で埋める
                        positivePlane[i] = (float)(amplitude.Real * amplitude.Real + amplitude.Imaginary * amplitude.Imaginary);
                    }
                    for (int i = 0; i < hemisphereLength; i++)
                    {
                        int srcIndex = i + hemisphereLength;
                        var amplitude = srcIndex < amplitudes.Length ? amplitudes[srcIndex] : Complex.Zero; // (260321Ch)
                        negativePlane[i] = (float)(amplitude.Real * amplitude.Real + amplitude.Imaginary * amplitude.Imaginary);
                    }
                    positivePlanes[planeIndex] = positivePlane;
                    negativePlanes[planeIndex] = negativePlane;
                    count += 2; // (260321Ch)
                }

                if (count == planeCountForProgress || count == 1 || count % reportStep == 0)
                {
                    var progress = Math.Min(100, (int)Math.Round((double)count / planeCountForProgress * 100.0));
                    bwMasterPattern.ReportProgress(progress, "Compiling master planes"); // (260327Ch)
                }
            }

        e.Result = new MasterPatternCompilationResult(
            request,
            // new EbsdMasterPattern(request.GridSize, [.. request.Energies], [.. request.Depths], request.Hemisphere, planes)); // (260321Ch) 旧案: 単一半球の plane 配列だけを保持していた
            new MasterPattern(request.GridSize, [.. request.Energies], [.. request.Depths], positivePlanes, negativePlanes, request.GridType)); // 260331Cl gridType 追加
    }

    /// <summary>plane 変換 worker の進捗を MasterPattern 全体の進捗へ換算して通知する。</summary>
    private void MasterPattern_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        var request = currentMasterPatternBuildRequest; // (260327Ch)
        if (request == null)
            return;

        var localProgress = Math.Clamp(e.ProgressPercentage, 0, 100);
        var progress = BetheStageWeight + Math.Min(CompileStageWeight, (int)Math.Round(localProgress / 100.0 * CompileStageWeight));
        MasterPatternProgressChanged?.Invoke(this, new MasterPatternProgressChangedEventArgs(progress, e.UserState)); // (260327Ch)
    }

    /// <summary>plane 変換 worker の完了結果を受け取り、公開イベントへ流す。</summary>
    private void MasterPattern_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        var request = currentMasterPatternBuildRequest; // (260327Ch)

        if (e.Error != null)
        {
            MasterPattern = null;
            MasterPatternCompleted?.Invoke(this, new MasterPatternCompletedEventArgs(request, null, e.Error, false));
            return;
        }

        if (e.Cancelled)
        {
            MasterPattern = null;
            MasterPatternCompleted?.Invoke(this, new MasterPatternCompletedEventArgs(request, null, null, true));
            return;
        }

        var result = e.Result as MasterPatternCompilationResult;
        MasterPattern = result?.MasterPattern;
        MasterPatternCompleted?.Invoke(this, new MasterPatternCompletedEventArgs(result?.Request ?? request, MasterPattern, null, false));
    }

    /// <summary>使い終わった BetheMethod のイベント配線を解除する。</summary>
    private void DetachMasterPatternBetheHandlers()
    {
        if (masterPatternBethe == null)
            return;

        masterPatternBethe.EBSD_ProgressChanged -= MasterPatternBethe_EBSD_ProgressChanged;
        masterPatternBethe.EBSD_Completed -= MasterPatternBethe_EBSD_Completed;
    }
}

/// <summary>
/// Rosca-Lambert 等積投影上に保持する EBSD MasterPattern。
/// 正方格子または六方格子を選択可能。
/// 1 つの energy-depth 組に対して 1 枚の plane を持つ。
/// </summary>
public sealed class MasterPattern
{
    #region 両グリッド共通

    public static double SqrtPI = Math.Sqrt(Math.PI);

    /// <summary>等積正方形投影の境界値</summary>
    public static readonly double SquareLimit = Math.Sqrt(Math.PI / 2.0); // (260321Ch) 

    /// <summary>六方格子を有効にするフラグ。false のとき全結晶系で正方格子を使う。260331Cl 追加</summary>
    public static bool UseHexGridEnabled = true; // 260331Cl デバッグ用: true で六方格子有効

    /// <summary>結晶系に応じて六方格子を使うべきか判定する。260331Cl 追加</summary>
    public static bool ShouldUseHexGrid(Symmetry sym) => UseHexGridEnabled && (sym.CrystalSystemNumber == 5 || sym.CrystalSystemNumber == 6);

    /// <summary>MasterPattern が対応する半球。 </summary>
    public enum Hemisphere { NegativeZ = -1, PositiveZ = 1, }

    /// <summary> MasterPattern の格子タイプ </summary>
    public enum Types { Square, Hexagonal }

    /// <summary>一辺の分割数。正方格子: gridSize、六方格子: 2N+1。</summary>
    public int GridSize { get; }

    /// <summary>格子タイプ。260331Cl 追加</summary>
    public Types GridType { get; }

    /// <summary>六方格子の半径 N (GridSize = 2N+1)。正方格子では 0。260331Cl 追加</summary>
    public int HexRadius => GridType == Types.Hexagonal ? (GridSize - 1) / 2 : 0;

    /// <summary>保持しているエネルギー列。</summary>
    public double[] Energies { get; }

    /// <summary>保持している深さ列。</summary>
    public double[] Depths { get; }

    /// <summary>plane 本体。添字は energy-major で並ぶ。</summary>
    public float[][] PositivePlanes { get; } // (260321Ch) +Z 半球の plane 配列
    public float[][] NegativePlanes { get; } // (260321Ch) -Z 半球の plane 配列

    /// <summary> plane 数を返す。 </summary>
    public int PlaneCount => Energies.Length * Depths.Length;

    /// <summary> MasterPattern 本体を初期化する。 </summary>
    public MasterPattern(int gridSize, double[] energies, double[] depths, float[][] positivePlanes, float[][] negativePlanes,
        Types gridType = Types.Square) // 260331Cl gridType 追加
    {
        GridSize = gridSize;
        GridType = gridType;
        Energies = energies ?? [];
        Depths = depths ?? [];
        PositivePlanes = positivePlanes ?? [];
        NegativePlanes = negativePlanes ?? [];
    }
 
    /// <summary>
    /// 指定した energy-depth 組の plane を返す。
    /// 範囲外のときは null を返す。
    /// </summary>
    public float[] GetPlane(Hemisphere hemisphere, int energyIndex, int depthIndex)
    {
        if ((uint)energyIndex >= (uint)Energies.Length || (uint)depthIndex >= (uint)Depths.Length)
            return null;

        var planeIndex = energyIndex * Depths.Length + depthIndex;
        var planes = hemisphere == Hemisphere.PositiveZ ? PositivePlanes : NegativePlanes;
        return (uint)planeIndex < (uint)planes.Length ? planes[planeIndex] : null;
    }

    #endregion

    #region SquareGrid 関連

    /// <summary>Rosca-Lambert 格子の各セル中心に対応する出射方向を生成する。</summary>
    public static Vector3DBase[] CreateDirectionsSquare(int gridSize, Hemisphere hemisphere)
    {
        var directions = new Vector3DBase[gridSize * gridSize];
        var step = 2.0 * SquareLimit / gridSize;
        for (int h = 0; h < gridSize; h++)
            for (int w = 0; w < gridSize; w++)
            {
                var a = -SquareLimit + (w + 0.5) * step;
                var b = SquareLimit - (h + 0.5) * step; // (260321Ch) preview 表示の上方向が +Y に見えるよう Y を反転する
                directions[h * gridSize + w] = RoscaLambertToSphereSquare(a, b, hemisphere);
            }
        return directions;
    }

    /// <summary>球面方向を Rosca-Lambert 等積正方形上の座標 (a, b) へ逆変換する。260325Cl 追加</summary>
    public static (double a, double b) SphereToRoscaLambertSquare(double x, double y, double z)
    {
        double len = Math.Sqrt(x * x + y * y + z * z);
        if (len < 1e-15)
            return (0, 0);
        x /= len; y /= len; z /= len;

        // 球面→ディスク (A, B)
        double radialScale = z >= 0 ? Math.Sqrt(Math.Max(0.0, (1.0 + z) / 2.0))
                                    : Math.Sqrt(Math.Max(0.0, (1.0 - z) / 2.0));
        if (radialScale < 1e-15)
            return (0, 0);

        double A = x / radialScale, B = y / radialScale;

        // ディスク→正方形 (a, b): Shirley/Rosca 逆変換
        // 順変換では A = (2a/√π)cos(θ), B = (2a/√π)sin(θ) なので B/A = tan(θ)。
        // a の符号によらず成立するため atan(B/A) を使う (atan2 は a<0 で θ±π を返し誤り)。
        double a, b;
        if (Math.Abs(A) < 1e-15 && Math.Abs(B) < 1e-15)
            (a, b) = (0, 0);
        else if (Math.Abs(A) >= Math.Abs(B))
        {
            double r = Math.Sqrt(A * A + B * B);
            a = Math.Sign(A) * r * SqrtPI / 2.0;
            b = 4.0 * a / Math.PI * Math.Atan(B / A); // 260325Cl: Atan2→Atan に修正
        }
        else
        {
            double r = Math.Sqrt(A * A + B * B);
            b = Math.Sign(B) * r * SqrtPI / 2.0;
            a = 4.0 * b / Math.PI * Math.Atan(A / B); // 260325Cl: Atan2→Atan に修正
        }
        return (a, b);
    }

    /// <summary>Rosca-Lambert 正方形座標 (a, b) で plane 上の強度をバイリニア補間する。260325Cl 追加</summary>
    public static float InterpolatePlaneSquare(float[] plane, int gridSize, double a, double b)
    {
        if (plane == null || plane.Length != gridSize * gridSize || gridSize <= 0)
            return 0;

        var step = 2.0 * SquareLimit / gridSize;
        var w = (a + SquareLimit) / step - 0.5;
        var h = (SquareLimit - b) / step - 0.5;

        int w0 = (int)Math.Floor(w), h0 = (int)Math.Floor(h);
        double fw = w - w0, fh = h - h0;
        int w1 = w0 + 1, h1 = h0 + 1;

        w0 = Math.Clamp(w0, 0, gridSize - 1);
        w1 = Math.Clamp(w1, 0, gridSize - 1);
        h0 = Math.Clamp(h0, 0, gridSize - 1);
        h1 = Math.Clamp(h1, 0, gridSize - 1);

        var v00 = plane[h0 * gridSize + w0];
        var v10 = plane[h0 * gridSize + w1];
        var v01 = plane[h1 * gridSize + w0];
        var v11 = plane[h1 * gridSize + w1];

        return (float)((1 - fw) * (1 - fh) * v00 + fw * (1 - fh) * v10 + (1 - fw) * fh * v01 + fw * fh * v11);
    }

    /// <summary>Rosca-Lambert 等積正方形上の座標を球面方向へ変換する。</summary>
    public static Vector3DBase RoscaLambertToSphereSquare(double a, double b, Hemisphere hemisphere)
    {
        if (Math.Abs(a) < 1.0E-15 && Math.Abs(b) < 1.0E-15)
            return hemisphere == Hemisphere.PositiveZ ? new Vector3DBase(0, 0, 1) : new Vector3DBase(0, 0, -1);

        double A, B;
        if (Math.Abs(b) <= Math.Abs(a))
        {
            // (260321Ch) Rosca の square -> disk 変換式。|b|<=|a| の枝。
            var theta = b * Math.PI / (4.0 * a);
            var (sin, cos) = Math.SinCos(theta);
            A = 2.0 * a / SqrtPI * cos;
            B = 2.0 * a / SqrtPI * sin;
        }
        else
        {
            // (260321Ch) Rosca の square -> disk 変換式。|a|<=|b| の枝。
            var theta = a * Math.PI / (4.0 * b);
            var (sin, cos) = Math.SinCos(theta);
            A = 2.0 * b / SqrtPI * sin;
            B = 2.0 * b / SqrtPI * cos;
        }

        var rho2 = A * A + B * B;
        var radialScale = Math.Sqrt(Math.Max(0.0, 1.0 - rho2 / 4.0));
        var z = hemisphere == Hemisphere.PositiveZ ? 1.0 - rho2 / 2.0 : -1.0 + rho2 / 2.0;
        return new Vector3DBase(radialScale * A, radialScale * B, z);
    }

    // 260331Cl 追加 (BetheMethod.cs から移動): Rosca-Lambert 正方格子の対称操作関連コード

    // (260327Ch) Rosca-Lambert 正方格子上で厳密に index を写せる 4/mmm 系の対称操作 (Symmetry Operation) だけを列挙する
    internal enum SymmOper
    {
        #region
        /// <summary> 何も変換しない恒等操作</summary>
        Identity,
        /// <summary>  正方格子中心まわりの Z 軸 180 度回転</summary>
        RotZ180,
        /// <summary> 正方格子中心まわりの Z 軸 90 度回転</summary>
        RotZ90,
        /// <summary>  正方格子中心まわりの Z 軸 270 度回転</summary>
        RotZ270,
        /// <summary>  正方格子の左右反転</summary>
        MirrorX,
        /// <summary> 正方格子の上下反転</summary>
        MirrorY,
        /// <summary> 対角線 y = -x に関する鏡映</summary>
        MirrorDiagXY,
        /// <summary> 対角線 y = x に関する鏡映</summary>
        MirrorDiagX_Y,
        /// <summary> 格子座標は保ったまま半球だけを反転する鏡映</summary>
        MirrorZ,
        /// <summary> Z 軸 180 度回転と半球反転を同時に行う反転操作</summary>
        Inversion,
        /// <summary> Z 軸 90 度回転後に半球を反転する合成操作</summary>
        RotZ90MirrorZ,
        /// <summary> Z 軸 270 度回転後に半球を反転する合成操作</summary>
        RotZ270MirrorZ,
        /// <summary> X 軸 180 度回転に対応し、半球も反転する</summary>
        RotX180,
        /// <summary> Y 軸 180 度回転に対応し、半球も反転する</summary>
        RotY180,
        /// <summary> 対角軸 [110] まわり 180 度回転に対応し、半球も反転する</summary>
        RotDiagXY180,
        /// <summary>  対角軸 [1-10] まわり 180 度回転に対応し、半球も反転する</summary>
        RotDiagX_Y180,
        #endregion
    }

    /// <summary>
    /// 現在の point group から、Rosca-Lambert 正方格子上で補間なしに使える対称操作だけを返す。
    /// 単斜晶は sym.MainAxis で主軸方向を判定し、対応する SymmOper を返す。 // (260331Cl)
    /// </summary>
    internal static SymmOper[] GetMasterPatternSquareSymmetryOperations(Symmetry sym)
        => sym.PointGroupNumber switch
        {
            #region
            0 or 1 => [SymmOper.Identity],
            2 => [SymmOper.Identity, SymmOper.Inversion],
            3 => sym.MainAxis switch // 260331Cl 点群 2: 主軸方向の 2 回回転
            {
                "a" => [SymmOper.Identity, SymmOper.RotX180],
                "b" => [SymmOper.Identity, SymmOper.RotY180],
                _ => [SymmOper.Identity, SymmOper.RotZ180],
            },
            4 => sym.MainAxis switch // 260331Cl 点群 m: 主軸に垂直な鏡映
            {
                "a" => [SymmOper.Identity, SymmOper.MirrorX],
                "b" => [SymmOper.Identity, SymmOper.MirrorY],
                _ => [SymmOper.Identity, SymmOper.MirrorZ],
            },
            5 => sym.MainAxis switch // 260331Cl 点群 2/m: 2 回回転 + 鏡映 + 反転
            {
                "a" => [SymmOper.Identity, SymmOper.RotX180, SymmOper.MirrorX, SymmOper.Inversion],
                "b" => [SymmOper.Identity, SymmOper.RotY180, SymmOper.MirrorY, SymmOper.Inversion],
                _ => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.MirrorZ, SymmOper.Inversion],
            },
            6 => [SymmOper.Identity, SymmOper.RotX180, SymmOper.RotY180, SymmOper.RotZ180],
            7 => sym.PointGroupHMStr switch
            {
                "2mm" => [SymmOper.Identity, SymmOper.RotX180, SymmOper.MirrorY, SymmOper.MirrorZ],
                "m2m" => [SymmOper.Identity, SymmOper.RotY180, SymmOper.MirrorX, SymmOper.MirrorZ],
                _ => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.MirrorX, SymmOper.MirrorY],
            },
            8 => [SymmOper.Identity, SymmOper.RotX180, SymmOper.RotY180, SymmOper.RotZ180, SymmOper.MirrorX, SymmOper.MirrorY, SymmOper.MirrorZ, SymmOper.Inversion],
            9 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.RotZ90, SymmOper.RotZ270],
            10 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.RotZ90MirrorZ, SymmOper.RotZ270MirrorZ],
            11 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.RotZ90, SymmOper.RotZ270, SymmOper.MirrorZ, SymmOper.Inversion, SymmOper.RotZ90MirrorZ, SymmOper.RotZ270MirrorZ],
            12 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.RotZ90, SymmOper.RotZ270, SymmOper.RotX180, SymmOper.RotY180, SymmOper.RotDiagXY180, SymmOper.RotDiagX_Y180],
            13 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.RotZ90, SymmOper.RotZ270, SymmOper.MirrorX, SymmOper.MirrorY, SymmOper.MirrorDiagXY, SymmOper.MirrorDiagX_Y],
            // 260331Cl 修正: -42m と -4m2 で C2'/σ_d の方向が異なる
            14 => sym.StrSE2p == "2"
                ? [SymmOper.Identity, SymmOper.RotZ180, SymmOper.MirrorDiagXY, SymmOper.MirrorDiagX_Y, SymmOper.RotZ90MirrorZ, SymmOper.RotZ270MirrorZ, SymmOper.RotX180, SymmOper.RotY180]             // -42m: C2' ∥ a,b + σ_d ∥ diag
                : [SymmOper.Identity, SymmOper.RotZ180, SymmOper.MirrorX, SymmOper.MirrorY, SymmOper.RotZ90MirrorZ, SymmOper.RotZ270MirrorZ, SymmOper.RotDiagXY180, SymmOper.RotDiagX_Y180], // -4m2: σ ⊥ a,b + C2' ∥ diag
            15 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.RotZ90, SymmOper.RotZ270, SymmOper.MirrorX, SymmOper.MirrorY, SymmOper.MirrorDiagXY, SymmOper.MirrorDiagX_Y, SymmOper.MirrorZ, SymmOper.Inversion, SymmOper.RotZ90MirrorZ, SymmOper.RotZ270MirrorZ, SymmOper.RotX180, SymmOper.RotY180, SymmOper.RotDiagXY180, SymmOper.RotDiagX_Y180],
            16 => [SymmOper.Identity],
            17 => [SymmOper.Identity, SymmOper.Inversion],
            18 => [SymmOper.Identity],
            19 => [SymmOper.Identity],
            20 => [SymmOper.Identity, SymmOper.Inversion],
            21 => [SymmOper.Identity, SymmOper.RotZ180],
            22 => [SymmOper.Identity, SymmOper.MirrorZ],
            23 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.MirrorZ, SymmOper.Inversion],
            24 => [SymmOper.Identity, SymmOper.RotZ180],
            25 => [SymmOper.Identity, SymmOper.RotZ180],
            26 => [SymmOper.Identity, SymmOper.MirrorZ],
            27 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.MirrorZ, SymmOper.Inversion],
            28 => [SymmOper.Identity, SymmOper.RotX180, SymmOper.RotY180, SymmOper.RotZ180],
            29 => [SymmOper.Identity, SymmOper.RotX180, SymmOper.RotY180, SymmOper.RotZ180, SymmOper.MirrorX, SymmOper.MirrorY, SymmOper.MirrorZ, SymmOper.Inversion],
            30 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.RotZ90, SymmOper.RotZ270, SymmOper.RotX180, SymmOper.RotY180, SymmOper.RotDiagXY180, SymmOper.RotDiagX_Y180],
            31 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.MirrorDiagXY, SymmOper.MirrorDiagX_Y, SymmOper.RotZ90MirrorZ, SymmOper.RotZ270MirrorZ, SymmOper.RotX180, SymmOper.RotY180],
            32 => [SymmOper.Identity, SymmOper.RotZ180, SymmOper.RotZ90, SymmOper.RotZ270, SymmOper.MirrorX, SymmOper.MirrorY, SymmOper.MirrorDiagXY, SymmOper.MirrorDiagX_Y, SymmOper.MirrorZ, SymmOper.Inversion, SymmOper.RotZ90MirrorZ, SymmOper.RotZ270MirrorZ, SymmOper.RotX180, SymmOper.RotY180, SymmOper.RotDiagXY180, SymmOper.RotDiagX_Y180],
            _ => [SymmOper.Identity],
            #endregion
        };

    /// <summary>Rosca-Lambert の 2 半球正方格子 index に、4/mmm 系の厳密対称操作を適用する。</summary>
    internal static int TransformMasterPatternSquareIndex(int index, int hemisphereLength, int gridSize, SymmOper operation)
    {
        var sphere = index / hemisphereLength;
        var localIndex = index - sphere * hemisphereLength;
        int w = localIndex % gridSize, h = localIndex / gridSize;
        (int Sphere, int W, int H) = operation switch //  半球反転も含めて switch 式にまとめる
        {
            SymmOper.Identity => (sphere, w, h),
            SymmOper.RotZ180 => (sphere, gridSize - 1 - w, gridSize - 1 - h),
            SymmOper.RotZ90 => (sphere, h, gridSize - 1 - w),
            SymmOper.RotZ270 => (sphere, gridSize - 1 - h, w),
            SymmOper.MirrorX => (sphere, gridSize - 1 - w, h),
            SymmOper.MirrorY => (sphere, w, gridSize - 1 - h),
            SymmOper.MirrorDiagXY => (sphere, gridSize - 1 - h, gridSize - 1 - w),
            SymmOper.MirrorDiagX_Y => (sphere, h, w),
            SymmOper.MirrorZ => (1 - sphere, w, h),
            SymmOper.Inversion => (1 - sphere, gridSize - 1 - w, gridSize - 1 - h),
            SymmOper.RotZ90MirrorZ => (1 - sphere, h, gridSize - 1 - w),
            SymmOper.RotZ270MirrorZ => (1 - sphere, gridSize - 1 - h, w),
            SymmOper.RotX180 => (1 - sphere, w, gridSize - 1 - h),
            SymmOper.RotY180 => (1 - sphere, gridSize - 1 - w, h),
            SymmOper.RotDiagXY180 => (1 - sphere, gridSize - 1 - h, gridSize - 1 - w),
            SymmOper.RotDiagX_Y180 => (1 - sphere, h, w),
            _ => (sphere, w, h),
        };

        return Sphere * hemisphereLength + H * gridSize + W;
    }

    #endregion

    #region HexGrid 関連

    /// <summary>軸座標 (u, v) が六方格子の有効領域内か判定する。260331Cl 追加</summary>
    public static bool IsValidHexCell(int u, int v, int N) => Math.Abs(u) <= N && Math.Abs(v) <= N && Math.Abs(u + v) <= N;

    /// <summary>軸座標 (u, v) を菱形配列の線形インデックスに変換する。260331Cl 追加</summary>
    public static int HexLinearIndex(int u, int v, int N) => (v + N) * (2 * N + 1) + (u + N);

    /// <summary>線形インデックスを軸座標 (u, v) に逆変換する。260331Cl 追加</summary>
    public static (int u, int v) HexFromLinearIndex(int index, int N)
    {
        int side = 2 * N + 1;
        int v = index / side - N;
        int u = index % side - N;
        return (u, v);
    }

    // --- 六方格子の対称操作 ---

    /// <summary>
    /// 六方格子上で厳密に index を写せる 6/mmm 系の対称操作。260331Cl 追加
    /// 軸座標 (u, v) に対する変換を列挙する。半球反転との組み合わせも含む。
    /// </summary>
    internal enum HexSymmOper
    {
        #region
        // --- 半球保存 (12 操作) ---
        /// <summary>恒等操作 (u, v)</summary>
        Identity,
        /// <summary>60° 回転 (u,v)→(-v, u+v)</summary>
        RotZ60,
        /// <summary>120° 回転 (u,v)→(-u-v, u)</summary>
        RotZ120,
        /// <summary>180° 回転 (u,v)→(-u, -v)</summary>
        RotZ180,
        /// <summary>240° 回転 (u,v)→(v, -u-v)</summary>
        RotZ240,
        /// <summary>300° 回転 (u,v)→(u+v, -u)</summary>
        RotZ300,
        /// <summary>A0 軸 (0°) に直交する鏡映 (鏡映線 90°): (u,v)→(-u-v, v)</summary>
        MirrorA0,
        /// <summary>A1 軸 (30°) に直交する鏡映 (鏡映線 120°): (u,v)→(-v, -u)</summary>
        MirrorA1,
        /// <summary>A2 軸 (60°) に直交する鏡映 (鏡映線 150°): (u,v)→(u, -u-v)</summary>
        MirrorA2,
        /// <summary>A3 軸 (90°) に直交する鏡映 (鏡映線 0°): (u,v)→(u+v, -v)</summary>
        MirrorA3,
        /// <summary>A4 軸 (120°) に直交する鏡映 (鏡映線 30°): (u,v)→(v, u)</summary>
        MirrorA4,
        /// <summary>A5 軸 (150°) に直交する鏡映 (鏡映線 60°): (u,v)→(-u, u+v)</summary>
        MirrorA5,

        // --- 半球反転を伴う操作 (12 操作) ---
        /// <summary>半球反転のみ (σ_h)</summary>
        MirrorZ,
        /// <summary>60° 回転 + 半球反転 (S₆)</summary>
        RotZ60MirrorZ,
        /// <summary>120° 回転 + 半球反転 (S₃)</summary>
        RotZ120MirrorZ,
        /// <summary>180° 回転 + 半球反転 (= 反転 i)</summary>
        Inversion,
        /// <summary>240° 回転 + 半球反転 (S₃⁻¹)</summary>
        RotZ240MirrorZ,
        /// <summary>300° 回転 + 半球反転 (S₆⁻¹)</summary>
        RotZ300MirrorZ,
        /// <summary>A0 軸 (0°) 周りの 180° 回転 (C₂')</summary>
        RotA0_180,
        /// <summary>A1 軸 (30°) 周りの 180° 回転 (C₂')</summary>
        RotA1_180,
        /// <summary>A2 軸 (60°) 周りの 180° 回転 (C₂')</summary>
        RotA2_180,
        /// <summary>A3 軸 (90°) 周りの 180° 回転 (C₂')</summary>
        RotA3_180,
        /// <summary>A4 軸 (120°) 周りの 180° 回転 (C₂')</summary>
        RotA4_180,
        /// <summary>A5 軸 (150°) 周りの 180° 回転 (C₂')</summary>
        RotA5_180,
        #endregion
    }

    /// <summary>
    /// 現在の point group から、六方格子上で補間なしに使える対称操作を返す。260331Cl 追加
    /// 点群番号 16-27 (trigonal/hexagonal) が対象。
    /// </summary>
    internal static HexSymmOper[] GetMasterPatternHexSymmetryOperations(Symmetry sym)
        => sym.PointGroupNumber switch
        {
            #region
            // 3 (C3)
            16 => [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240],

            // -3 (C3i)
            17 => [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240,
                   HexSymmOper.Inversion, HexSymmOper.RotZ60MirrorZ, HexSymmOper.RotZ300MirrorZ],

            // 32 (D3): C2' 軸の方向で分岐
            18 => sym.StrSE2p != "1"
                // "321"
                ? [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240,
                   HexSymmOper.RotA1_180, HexSymmOper.RotA3_180, HexSymmOper.RotA5_180]
                // "312"
                : [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240,
                   HexSymmOper.RotA0_180, HexSymmOper.RotA2_180,  HexSymmOper.RotA4_180],

            // 3m (C3v): 鏡映面の方向で分岐
            19 => sym.StrSE2p != "1"
                // "3m1": σ ⊥ a (a は奇数軸方向)
                ? [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240,
                   HexSymmOper.MirrorA1, HexSymmOper.MirrorA3, HexSymmOper.MirrorA5]
                // "31m": σ ⊥ [1-10] (偶数軸方向)
                : [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240,
                   HexSymmOper.MirrorA0, HexSymmOper.MirrorA2, HexSymmOper.MirrorA4],

            // -3m (D3d): MirrorAn と Rot180An は同じ index (σ_d ⊥ C2')
            20 => sym.StrSE2p != "1"
                // "-3m1"あるいは"-3m"
                ? [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240,
                   HexSymmOper.Inversion, HexSymmOper.RotZ60MirrorZ, HexSymmOper.RotZ300MirrorZ,
                   HexSymmOper.MirrorA1, HexSymmOper.MirrorA3, HexSymmOper.MirrorA5,
                   HexSymmOper.RotA1_180, HexSymmOper.RotA3_180, HexSymmOper.RotA5_180]
                // "-31m"
                : [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240,
                   HexSymmOper.Inversion, HexSymmOper.RotZ60MirrorZ, HexSymmOper.RotZ300MirrorZ,
                   HexSymmOper.MirrorA0, HexSymmOper.MirrorA2, HexSymmOper.MirrorA4,
                   HexSymmOper.RotA0_180, HexSymmOper.RotA2_180, HexSymmOper.RotA4_180],

            // 6 (C6)
            21 => [HexSymmOper.Identity, HexSymmOper.RotZ60, HexSymmOper.RotZ120,
                   HexSymmOper.RotZ180, HexSymmOper.RotZ240, HexSymmOper.RotZ300],

            // -6 (C3h)
            22 => [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240,
                   HexSymmOper.MirrorZ, HexSymmOper.RotZ120MirrorZ, HexSymmOper.RotZ240MirrorZ],

            // 6/m (C6h)
            23 => [HexSymmOper.Identity, HexSymmOper.RotZ60, HexSymmOper.RotZ120,
                   HexSymmOper.RotZ180, HexSymmOper.RotZ240, HexSymmOper.RotZ300,
                   HexSymmOper.MirrorZ, HexSymmOper.RotZ60MirrorZ, HexSymmOper.RotZ120MirrorZ,
                   HexSymmOper.Inversion, HexSymmOper.RotZ240MirrorZ, HexSymmOper.RotZ300MirrorZ],

            // 622 (D6): 全 6 軸の C2'
            24 => [HexSymmOper.Identity, HexSymmOper.RotZ60, HexSymmOper.RotZ120,
                   HexSymmOper.RotZ180, HexSymmOper.RotZ240, HexSymmOper.RotZ300,
                   HexSymmOper.RotA0_180, HexSymmOper.RotA1_180, HexSymmOper.RotA2_180,
                   HexSymmOper.RotA3_180, HexSymmOper.RotA4_180, HexSymmOper.RotA5_180],

            // 6mm (C6v): 全 6 軸の鏡映
            25 => [HexSymmOper.Identity, HexSymmOper.RotZ60, HexSymmOper.RotZ120,
                   HexSymmOper.RotZ180, HexSymmOper.RotZ240, HexSymmOper.RotZ300,
                   HexSymmOper.MirrorA0, HexSymmOper.MirrorA1, HexSymmOper.MirrorA2,
                   HexSymmOper.MirrorA3, HexSymmOper.MirrorA4, HexSymmOper.MirrorA5],

            // -6m2/-62m (D3h): D3h では MirrorAn と Rot180An は index が 3 ずれる (σ_v ∥ C2')
            26 => sym.StrSE2p == "2"
                // P-62m: C2' ∥ a (奇数)
                ? [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240,
                   HexSymmOper.MirrorA0, HexSymmOper.MirrorA2, HexSymmOper.MirrorA4,
                   HexSymmOper.MirrorZ, HexSymmOper.RotZ120MirrorZ, HexSymmOper.RotZ240MirrorZ,
                   HexSymmOper.RotA1_180, HexSymmOper.RotA3_180, HexSymmOper.RotA5_180]
                // P-6m2: σ_v ⊥ a (奇数)
                : [HexSymmOper.Identity, HexSymmOper.RotZ120, HexSymmOper.RotZ240,
                   HexSymmOper.MirrorA1, HexSymmOper.MirrorA3, HexSymmOper.MirrorA5,
                   HexSymmOper.MirrorZ, HexSymmOper.RotZ120MirrorZ, HexSymmOper.RotZ240MirrorZ,
                   HexSymmOper.RotA0_180, HexSymmOper.RotA2_180, HexSymmOper.RotA4_180],

            // 6/mmm (D6h): 全操作
            27 => [HexSymmOper.Identity, HexSymmOper.RotZ60, HexSymmOper.RotZ120,
                   HexSymmOper.RotZ180, HexSymmOper.RotZ240, HexSymmOper.RotZ300,
                   HexSymmOper.MirrorA0, HexSymmOper.MirrorA1, HexSymmOper.MirrorA2,
                   HexSymmOper.MirrorA3, HexSymmOper.MirrorA4, HexSymmOper.MirrorA5,
                   HexSymmOper.MirrorZ, HexSymmOper.RotZ60MirrorZ, HexSymmOper.RotZ120MirrorZ,
                   HexSymmOper.Inversion, HexSymmOper.RotZ240MirrorZ, HexSymmOper.RotZ300MirrorZ,
                   HexSymmOper.RotA0_180, HexSymmOper.RotA1_180, HexSymmOper.RotA2_180,
                   HexSymmOper.RotA3_180, HexSymmOper.RotA4_180, HexSymmOper.RotA5_180],
            _ => [HexSymmOper.Identity],
            #endregion
        };

    /// <summary>六方格子の 2 半球菱形配列 index に対称操作を適用する。260331Cl 追加</summary>
    internal static int TransformMasterPatternHexIndex(int index, int hemisphereLength, int gridSize, HexSymmOper operation)
    {
        int N = (gridSize - 1) / 2;
        int sphere = index / hemisphereLength;
        int localIndex = index - sphere * hemisphereLength;
        var (u, v) = HexFromLinearIndex(localIndex, N);

        // 軸座標変換を適用
        (int Sphere, int U, int V) = operation switch
        {
            HexSymmOper.Identity => (sphere, u, v),
            HexSymmOper.RotZ60 => (sphere, -v, u + v),
            HexSymmOper.RotZ120 => (sphere, -u - v, u),
            HexSymmOper.RotZ180 => (sphere, -u, -v),
            HexSymmOper.RotZ240 => (sphere, v, -u - v),
            HexSymmOper.RotZ300 => (sphere, u + v, -u),
            HexSymmOper.MirrorA0 => (sphere, -u - v, v),      // ⊥ 0° → 鏡映線 90°
            HexSymmOper.MirrorA1 => (sphere, -v, -u),         // ⊥ 30° → 鏡映線 120°
            HexSymmOper.MirrorA2 => (sphere, u, -u - v),      // ⊥ 60° → 鏡映線 150°
            HexSymmOper.MirrorA3 => (sphere, u + v, -v),      // ⊥ 90° → 鏡映線 0°
            HexSymmOper.MirrorA4 => (sphere, v, u),            // ⊥ 120° → 鏡映線 30°
            HexSymmOper.MirrorA5 => (sphere, -u, u + v),      // ⊥ 150° → 鏡映線 60°
            HexSymmOper.MirrorZ => (1 - sphere, u, v),
            HexSymmOper.RotZ60MirrorZ => (1 - sphere, -v, u + v),
            HexSymmOper.RotZ120MirrorZ => (1 - sphere, -u - v, u),
            HexSymmOper.Inversion => (1 - sphere, -u, -v),
            HexSymmOper.RotZ240MirrorZ => (1 - sphere, v, -u - v),
            HexSymmOper.RotZ300MirrorZ => (1 - sphere, u + v, -u),
            HexSymmOper.RotA0_180 => (1 - sphere, u + v, -v),  // 0°
            HexSymmOper.RotA1_180 => (1 - sphere, v, u),       // 30°
            HexSymmOper.RotA2_180 => (1 - sphere, -u, u + v),  // 60°
            HexSymmOper.RotA3_180 => (1 - sphere, -u - v, v),  // 90°
            HexSymmOper.RotA4_180 => (1 - sphere, -v, -u),     // 120°
            HexSymmOper.RotA5_180 => (1 - sphere, u, -u - v),  // 150°
            _ => (sphere, u, v),
        };

        return Sphere * hemisphereLength + HexLinearIndex(U, V, N);
    }

    // --- 六方格子 Rosca-Lambert 変換 ---

    // EMsoft Lambert.f90 / constants.f90 由来の定数
    static readonly double Hex_srt = Math.Sqrt(3.0) / 2.0;                               // sqrt(3)/2
    static readonly double Hex_isrt = 1.0 / Math.Sqrt(3.0);                               // 1/sqrt(3)
    static readonly double Hex_preb = Math.Pow(3.0, 0.25) * Math.Sqrt(2.0 / Math.PI);     // 3^(1/4) * sqrt(2/π): sextant 0 の forward 変換係数
    static readonly double Hex_prec = Math.PI / (2.0 * Math.Sqrt(3.0));                   // π / (2√3): sextant 0 の角度スケール
    static readonly double Hex_pree = Math.Pow(3.0, -0.25);                                // 3^(-1/4): sextant 0 の inverse 変換係数
    static readonly double Hex_pref = Math.Sqrt(6.0 / Math.PI);                           // sqrt(6/π): sextant 0 の角度逆変換係数
    /// <summary>六角形の内接円半径 (apothem)。</summary>
    static readonly double Hex_alpha = Math.Sqrt(Math.PI) / Math.Pow(3.0, 0.25);           // sqrt(π) / 3^(1/4)
    /// <summary>六角形の外接円半径 (circumradius) = 辺長。HexagonToDisk/DiskToHexagon が返す座標のスケール。</summary>
    // 260331Cl 修正: Hex_alpha (apothem) ではなく Hex_preg (circumradius) をグリッドスペーシングに使用
    public static readonly double Hex_preg = 2.0 * Math.Sqrt(Math.PI) / Math.Pow(3.0, 0.75); // 2√π / 3^(3/4)

    /// <summary>
    /// 六方格子の軸座標 (u, v) と直交 Lambert 座標 (x, y) の変換用スペーシングを返す。
    /// N リング分で六角形の外接円半径 Hex_preg をカバーする。
    /// </summary>
    public static double HexSpacing(int N) => N > 0 ? Hex_preg / N : Hex_preg;

    /// <summary>六方格子の軸座標を直交座標 (x, y) に変換する。</summary>
    public static (double x, double y) HexAxialToCartesian(int u, int v, double spacing) => ((u + v * 0.5) * spacing, v * Hex_srt * spacing);

    /// <summary>直交座標を六方格子の分数軸座標 (uf, vf) に変換する。</summary>
    public static (double uf, double vf) HexCartesianToAxial(double x, double y, double spacing)
    {
        double vf = y / (Hex_srt * spacing);
        double uf = x / spacing - vf * 0.5;
        return (uf, vf);
    }

    /// <summary>直交座標 (x, y) から sextant (0-5) を判定する。EMsoft GetSextant に準拠。</summary>
    static int GetSextant(double x, double y)
    {
        double xx = Math.Abs(x) * Hex_isrt; // |x| / sqrt(3)
        if (x >= 0)
        {
            if (Math.Abs(y) <= xx) return 0;
            return y > 0 ? 1 : 5;
        }
        else
        {
            if (Math.Abs(y) <= xx) return 3;
            return y > 0 ? 2 : 4;
        }
    }

    // 260331Cl: sextant 間の 60° 回転行列 (cos60=0.5, sin60=srt)
    // sextant k への回転: R(k*60°), 逆回転: R(-k*60°)
    static (double x, double y) RotateToSextant0(double x, double y, int sextant)
    {
        return sextant switch
        {
            0 => (x, y),
            1 => (x * 0.5 + y * Hex_srt, -x * Hex_srt + y * 0.5),
            2 => (-x * 0.5 + y * Hex_srt, -x * Hex_srt - y * 0.5),
            3 => (-x, -y),
            4 => (-x * 0.5 - y * Hex_srt, x * Hex_srt - y * 0.5),
            5 => (x * 0.5 - y * Hex_srt, x * Hex_srt + y * 0.5),
            _ => (x, y),
        };
    }
    static (double x, double y) RotateFromSextant0(double x, double y, int sextant)
    {
        return sextant switch
        {
            0 => (x, y),
            1 => (x * 0.5 - y * Hex_srt, x * Hex_srt + y * 0.5),
            2 => (-x * 0.5 - y * Hex_srt, x * Hex_srt - y * 0.5),
            3 => (-x, -y),
            4 => (-x * 0.5 + y * Hex_srt, -x * Hex_srt - y * 0.5),
            5 => (x * 0.5 + y * Hex_srt, -x * Hex_srt + y * 0.5),
            _ => (x, y),
        };
    }

    /// <summary>
    /// 六角形座標 (hx, hy) を Lambert 等積投影で円盤座標 (A, B) に変換する。
    /// EMsoft Lambert2DHexForward に準拠: sextant 0 に回転 → 変換 → 回転を戻す。
    /// 260331Cl 修正: sextant 0 統一方式に書き換え
    /// </summary>
    static (double A, double B) HexagonToDisk(double hx, double hy)
    {
        if (Math.Abs(hx) < 1e-15 && Math.Abs(hy) < 1e-15)
            return (0, 0);

        double X = hy, Y = hx; // EMsoft 座標入れ替え
        int sextant = GetSextant(X, Y);

        // sextant 0 に回転
        var (X0, Y0) = RotateToSextant0(X, Y, sextant);

        // sextant 0 の変換: 三角形 → 扇形
        if (Math.Abs(X0) < 1e-15)
            return (0, 0);
        double q = Y0 * Hex_prec / X0;
        var (sinQ, cosQ) = Math.SinCos(q);
        double XX0 = Hex_preb * X0 * cosQ;
        double YY0 = Hex_preb * X0 * sinQ;

        // sextant 0 から元の sextant に回転を戻す
        var (XX, YY) = RotateFromSextant0(XX0, YY0, sextant);
        return (YY, XX); // 座標入れ替えを戻す
    }

    /// <summary>
    /// 円盤座標 (A, B) を六角形座標 (hx, hy) に逆変換する。
    /// EMsoft Lambert2DHexInverse に準拠: sextant 0 に回転 → 逆変換 → 回転を戻す。
    /// 260331Cl 修正: sextant 0 統一方式に書き換え
    /// </summary>
    static (double hx, double hy) DiskToHexagon(double A, double B)
    {
        if (Math.Abs(A) < 1e-15 && Math.Abs(B) < 1e-15)
            return (0, 0);

        double XX = B, YY = A; // EMsoft 座標入れ替え
        int sextant = GetSextant(XX, YY);

        // sextant 0 に回転
        var (XX0, YY0) = RotateToSextant0(XX, YY, sextant);

        // sextant 0 の逆変換: 扇形 → 三角形
        if (Math.Abs(XX0) < 1e-15)
            return (0, 0);
        double r0 = Math.Sqrt(XX0 * XX0 + YY0 * YY0);
        double xHex0 = Hex_pree * r0 * SqrtPI / Math.Sqrt(2.0);  // = r0 / preb (∵ pree * sqrtPI / sqrt2 = 1/preb)
        double yHex0 = Hex_pree * r0 * Hex_pref * Math.Atan2(YY0, XX0); // Atan2 で全象限対応

        // sextant 0 から元の sextant に回転を戻す
        var (xHex, yHex) = RotateFromSextant0(xHex0, yHex0, sextant);
        return (yHex, xHex); // 座標入れ替えを戻す
    }

    /// <summary>六方格子の直交座標 (hx, hy) を球面方向に変換する。260331Cl 追加</summary>
    public static Vector3DBase RoscaLambertToSphereHexSquare(double hx, double hy, Hemisphere hemisphere)
    {
        if (Math.Abs(hx) < 1e-15 && Math.Abs(hy) < 1e-15)
            return hemisphere == Hemisphere.PositiveZ ? new Vector3DBase(0, 0, 1) : new Vector3DBase(0, 0, -1);

        var (A, B) = HexagonToDisk(hx, hy);

        // 円盤 → 球面 (Lambert 逆投影、正方格子版と同一)
        double rho2 = A * A + B * B;
        double radialScale = Math.Sqrt(Math.Max(0.0, 1.0 - rho2 / 4.0));
        double z = hemisphere == Hemisphere.PositiveZ ? 1.0 - rho2 / 2.0 : -1.0 + rho2 / 2.0;
        return new Vector3DBase(radialScale * A, radialScale * B, z);
    }

    /// <summary>球面方向を六方格子の直交座標 (hx, hy) に逆変換する。260331Cl 追加</summary>
    public static (double hx, double hy) SphereToRoscaLambertHex(double x, double y, double z)
    {
        double len = Math.Sqrt(x * x + y * y + z * z);
        if (len < 1e-15)
            return (0, 0);
        x /= len; y /= len; z /= len;

        // 球面 → 円盤 (Lambert 投影)
        double absZ = Math.Abs(z);
        double q = Math.Sqrt(Math.Max(0.0, 2.0 / (1.0 + absZ)));
        double A = q * x, B = q * y;

        return DiskToHexagon(A, B);
    }

    /// <summary>
    /// 六方格子の各有効セル中心に対応する出射方向を生成する。260331Cl 追加
    /// 無効セル (|u+v| > N) には null を格納する。
    /// </summary>
    public static Vector3DBase[] CreateDirectionsHex(int gridSize, Hemisphere hemisphere)
    {
        int N = (gridSize - 1) / 2;
        double spacing = HexSpacing(N);
        var directions = new Vector3DBase[gridSize * gridSize];
        for (int v = -N; v <= N; v++)
            for (int u = -N; u <= N; u++)
            {
                int idx = HexLinearIndex(u, v, N);
                if (!IsValidHexCell(u, v, N))
                {
                    directions[idx] = null; // 無効セル: BetheMethod でスキップされる
                    continue;
                }
                var (hx, hy) = HexAxialToCartesian(u, v, spacing);
                directions[idx] = RoscaLambertToSphereHexSquare(hx, hy, hemisphere);
            }
        return directions;
    }

    /// <summary>
    /// 六方格子上で 3 近傍バリセントリック補間を行う。260331Cl 追加
    /// (hx, hy) は六方格子の直交座標。
    /// </summary>
    public static float InterpolatePlaneHex(float[] plane, int gridSize, double hx, double hy)
    {
        if (plane == null || plane.Length != gridSize * gridSize || gridSize <= 1)
            return 0;

        int N = (gridSize - 1) / 2;
        double spacing = HexSpacing(N);
        var (uf, vf) = HexCartesianToAxial(hx, hy, spacing);

        // Cube rounding で最近接セルを決定
        double sf = -uf - vf;
        int ru = (int)Math.Round(uf), rv = (int)Math.Round(vf), rs = (int)Math.Round(sf);
        double du = Math.Abs(ru - uf), dv = Math.Abs(rv - vf), ds = Math.Abs(rs - sf);
        if (du > dv && du > ds)
            ru = -rv - rs;
        else if (dv > ds)
            rv = -ru - rs;
        // else: rs を調整するが u, v のみ使うので不要

        // 最近接セル (ru, rv) からの分数オフセット
        double fu = uf - ru, fv = vf - rv;

        // 分数オフセットから包含三角形を判定し、隣接 2 セルを決定
        // 六方格子のセル間は単位ベクトル e_u=(1,0) と e_v=(0,1)（軸座標）
        // 6 つの三角形は対角線 fu+fv=0, fu=0, fv=0 で分割される
        int u1, v1, u2, v2;
        if (fu + fv >= 0)
        {
            if (fu >= 0 && fv >= 0) { u1 = ru + 1; v1 = rv; u2 = ru; v2 = rv + 1; }        // sextant 0: e_u 方向と e_v 方向
            else if (fu >= 0) { u1 = ru + 1; v1 = rv; u2 = ru + 1; v2 = rv - 1; }     // sextant 5: e_u 方向と e_u-e_v 方向
            else { u1 = ru; v1 = rv + 1; u2 = ru - 1; v2 = rv + 1; }     // sextant 1: e_v 方向と -e_u+e_v 方向
        }
        else
        {
            if (fu <= 0 && fv <= 0) { u1 = ru - 1; v1 = rv; u2 = ru; v2 = rv - 1; }        // sextant 3: -e_u 方向と -e_v 方向
            else if (fu <= 0) { u1 = ru - 1; v1 = rv; u2 = ru - 1; v2 = rv + 1; }     // sextant 2: -e_u 方向と -e_u+e_v 方向
            else { u1 = ru; v1 = rv - 1; u2 = ru + 1; v2 = rv - 1; }     // sextant 4: -e_v 方向と e_u-e_v 方向
        }

        // バリセントリック重み: 三角形 (ru,rv)-(u1,v1)-(u2,v2) 内の点 (uf, vf) の重み
        // 辺ベクトル: d1 = (u1-ru, v1-rv), d2 = (u2-ru, v2-rv)
        double d1u = u1 - ru, d1v = v1 - rv;
        double d2u = u2 - ru, d2v = v2 - rv;
        double det = d1u * d2v - d1v * d2u;
        double w1, w2, w0;
        if (Math.Abs(det) < 1e-15)
        {
            w0 = 1; w1 = 0; w2 = 0; // 退化三角形
        }
        else
        {
            w1 = (fu * d2v - fv * d2u) / det;
            w2 = (d1u * fv - d1v * fu) / det;
            w0 = 1.0 - w1 - w2;
        }

        // 各セルの値を取得（無効セルは 0）
        float val0 = 0, val1 = 0, val2 = 0;
        if (IsValidHexCell(ru, rv, N))
            val0 = plane[HexLinearIndex(ru, rv, N)];
        if (IsValidHexCell(u1, v1, N))
            val1 = plane[HexLinearIndex(u1, v1, N)];
        if (IsValidHexCell(u2, v2, N))
            val2 = plane[HexLinearIndex(u2, v2, N)];

        return (float)(w0 * val0 + w1 * val1 + w2 * val2);
    }

    /// <summary>
    /// 六方格子の直交座標 (hx, hy) に対して、3 近傍インデックスとバリセントリック重みを返す。260331Cl 追加
    /// ルックアップテーブル構築用。idx0/idx1/idx2 は plane 配列の線形インデックス。
    /// </summary>
    public static void GetHexBarycentricLookup(double hx, double hy, int gridSize,
        out int idx0, out int idx1, out int idx2, out float w0, out float w1, out float w2)
    {
        int N = (gridSize - 1) / 2;
        double spacing = HexSpacing(N);
        var (uf, vf) = HexCartesianToAxial(hx, hy, spacing);

        // Cube rounding
        double sf = -uf - vf;
        int ru = (int)Math.Round(uf), rv = (int)Math.Round(vf), rs = (int)Math.Round(sf);
        double du = Math.Abs(ru - uf), dv = Math.Abs(rv - vf), ds = Math.Abs(rs - sf);
        if (du > dv && du > ds) ru = -rv - rs;
        else if (dv > ds) rv = -ru - rs;

        double fu = uf - ru, fv = vf - rv;

        int u1, v1, u2, v2;
        if (fu + fv >= 0)
        {
            if (fu >= 0 && fv >= 0) { u1 = ru + 1; v1 = rv; u2 = ru; v2 = rv + 1; }
            else if (fu >= 0) { u1 = ru + 1; v1 = rv; u2 = ru + 1; v2 = rv - 1; }
            else { u1 = ru; v1 = rv + 1; u2 = ru - 1; v2 = rv + 1; }
        }
        else
        {
            if (fu <= 0 && fv <= 0) { u1 = ru - 1; v1 = rv; u2 = ru; v2 = rv - 1; }
            else if (fu <= 0) { u1 = ru - 1; v1 = rv; u2 = ru - 1; v2 = rv + 1; }
            else { u1 = ru; v1 = rv - 1; u2 = ru + 1; v2 = rv - 1; }
        }

        double d1u = u1 - ru, d1v = v1 - rv, d2u = u2 - ru, d2v = v2 - rv;
        double det = d1u * d2v - d1v * d2u;
        if (Math.Abs(det) < 1e-15)
        { w0 = 1; w1 = 0; w2 = 0; }
        else
        {
            w1 = (float)((fu * d2v - fv * d2u) / det);
            w2 = (float)((d1u * fv - d1v * fu) / det);
            w0 = 1f - w1 - w2;
        }

        // 無効セルは index 0 (値は 0 なので重みが消える)
        idx0 = IsValidHexCell(ru, rv, N) ? HexLinearIndex(ru, rv, N) : 0;
        idx1 = IsValidHexCell(u1, v1, N) ? HexLinearIndex(u1, v1, N) : 0;
        idx2 = IsValidHexCell(u2, v2, N) ? HexLinearIndex(u2, v2, N) : 0;

        // 無効セル参照時は重みを 0 にする
        if (!IsValidHexCell(ru, rv, N)) { w1 += w0 * 0.5f; w2 += w0 * 0.5f; w0 = 0; }
        if (!IsValidHexCell(u1, v1, N)) { w0 += w1 * 0.5f; w2 += w1 * 0.5f; w1 = 0; }
        if (!IsValidHexCell(u2, v2, N)) { w0 += w2 * 0.5f; w1 += w2 * 0.5f; w2 = 0; }
    }

    /// <summary>
    /// 六方格子の plane データを六方格子座標系のまま N×N 正方画像に描画する。260331Cl 追加
    /// N = gridSize とし、六方格子の最大対角線（2×Hex_preg）が画像幅に収まるようにする。
    /// 六方格子領域外のピクセルは 0 になる。
    /// </summary>
    public static float[] RenderHexPlaneToImage(float[] hexPlane, int gridSize)
    {
        if (hexPlane == null || hexPlane.Length != gridSize * gridSize || gridSize <= 1)
            return new float[gridSize * gridSize];

        int N = (gridSize - 1) / 2;
        double extent = Hex_preg; // 六方格子の外接円半径 = 最大対角線の半分
        double step = 2.0 * extent / gridSize;
        // var image = new float[gridSize * gridSize]; // (260402Ch) 変更前
        var image = GC.AllocateUninitializedArray<float>(gridSize * gridSize); // (260402Ch) 全画素を補間結果で埋める
        for (int py = 0; py < gridSize; py++)
            for (int px = 0; px < gridSize; px++)
            {
                double hx = -extent + (px + 0.5) * step;
                double hy = extent - (py + 0.5) * step; // Y 軸は画面上方が正
                image[py * gridSize + px] = InterpolatePlaneHex(hexPlane, gridSize, hx, hy);
            }
        return image;
    }

    /// <summary>
    /// 六方格子の plane データを正方格子にリサンプリングする。260331Cl 追加
    /// 出力は gridSize × gridSize の正方 Rosca-Lambert 座標系。
    /// </summary>
    public static float[] ResampleHexToSquarePlane(float[] hexPlane, int gridSize)
    {
        if (hexPlane == null || hexPlane.Length != gridSize * gridSize || gridSize <= 1)
            return new float[gridSize * gridSize];

        // var squarePlane = new float[gridSize * gridSize]; // (260402Ch) 変更前
        var squarePlane = GC.AllocateUninitializedArray<float>(gridSize * gridSize); // (260402Ch) 全画素を書き切る
        double step = 2.0 * SquareLimit / gridSize;
        for (int h = 0; h < gridSize; h++)
            for (int w = 0; w < gridSize; w++)
            {
                double a = -SquareLimit + (w + 0.5) * step;
                double b = SquareLimit - (h + 0.5) * step;
                // 正方格子座標 → 球面方向 → 六方格子座標 → 補間
                var dir = RoscaLambertToSphereSquare(a, b, Hemisphere.PositiveZ);
                var (hx, hy) = SphereToRoscaLambertHex(dir.X, dir.Y, dir.Z);
                squarePlane[h * gridSize + w] = InterpolatePlaneHex(hexPlane, gridSize, hx, hy);
            }
        return squarePlane;
    }

    #endregion
}

