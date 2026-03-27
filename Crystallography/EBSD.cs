using System;
using System.ComponentModel;
using System.Linq;
using System.Numerics;

namespace Crystallography
{
    /// <summary>
    /// MasterPattern の作成処理を UI から切り離して管理するクラス。
    /// BetheMethod による EBSD ディスク計算と、その結果を plane 配列へ並べ替える処理を担当する。
    /// </summary>
    public class EBSD
    {
        /// <summary>
        /// MasterPattern 作成時に必要な条件をひとまとめにしたリクエスト。
        /// </summary>
        public sealed class MasterPatternBuildRequest
        {
            /// <summary>
            /// MasterPattern 作成条件を初期化する。
            /// </summary>
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

            /// <summary>BetheMethod で使う solver 種別。</summary>
            public BetheMethod.Solver Solver { get; }

            /// <summary>BetheMethod で使うスレッド数。</summary>
            public int Thread { get; }

            /// <summary>非局所吸収を有効にするかどうか。</summary>
            public bool UseNonLocalAbsorption { get; }

            /// <summary>TDS 背景を含めるかどうか。</summary>
            public bool IncludeTDSBackground { get; }

            /// <summary>
            /// Bethe 計算側の進捗を規格化するための基準値。
            /// </summary>
            public int DivisionCount => Math.Max(1, GridSize * GridSize * Math.Max(1, Energies.Length) * 2); // (260321Ch) MasterPattern は常に全球計算する

            /// <summary>
            /// リクエスト内容を複製する。
            /// 非同期実行中に UI 側の配列が変更されても影響しないよう、配列も複製して保持する。
            /// </summary>
            public MasterPatternBuildRequest Clone()
                => new(Crystal, MaxNumOfBloch, Energies, Depths, GridSize, Solver, Thread, UseNonLocalAbsorption, IncludeTDSBackground);
        }

        /// <summary>
        /// MasterPattern 作成中の進捗情報。
        /// </summary>
        public sealed class MasterPatternProgressChangedEventArgs : EventArgs
        {
            /// <summary>
            /// 進捗情報を初期化する。
            /// </summary>
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

        /// <summary>
        /// MasterPattern 作成完了時の結果。
        /// </summary>
        public sealed class MasterPatternCompletedEventArgs : EventArgs
        {
            /// <summary>
            /// 完了結果を初期化する。
            /// </summary>
            public MasterPatternCompletedEventArgs(MasterPatternBuildRequest request, EbsdMasterPattern masterPattern, Exception error, bool cancelled)
            {
                Request = request;
                MasterPattern = masterPattern;
                Error = error;
                Cancelled = cancelled;
            }

            /// <summary>今回の build 条件。</summary>
            public MasterPatternBuildRequest Request { get; }

            /// <summary>作成された MasterPattern。失敗またはキャンセル時は null。</summary>
            public EbsdMasterPattern MasterPattern { get; }

            /// <summary>失敗時の例外。成功時は null。</summary>
            public Exception Error { get; }

            /// <summary>キャンセルされたかどうか。</summary>
            public bool Cancelled { get; }
        }

        /// <summary>
        /// BetheMethod の出力ディスクを plane 配列へ並べ替える worker へ渡す引数。
        /// </summary>
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

        /// <summary>
        /// plane 変換 worker の完了結果。
        /// </summary>
        private sealed class MasterPatternCompilationResult
        {
            public MasterPatternCompilationResult(MasterPatternBuildRequest request, EbsdMasterPattern masterPattern)
            {
                Request = request;
                MasterPattern = masterPattern;
            }

            public MasterPatternBuildRequest Request { get; }
            public EbsdMasterPattern MasterPattern { get; }
        }

        private const int BetheStageWeight = 99; // (260321Ch) 全体進捗のうち Bethe 計算へ割り当てる比率
        private const int CompileStageWeight = 1; // (260321Ch) 全体進捗のうち plane 変換へ割り当てる比率

        private BetheMethod masterPatternBethe = null; // (260321Ch) MasterPattern 専用の Bethe solver

        /// <summary>
        /// BetheMethod の結果を MasterPattern plane 配列へ変換するための worker。
        /// 旧案では RunWorkerCompleted 内で同期変換していたが、重い処理を UI スレッドへ戻さないために独立させた。
        /// </summary>
        public readonly BackgroundWorker bwMasterPattern;

        /// <summary>
        /// MasterPattern 管理用 worker 群を初期化する。
        /// </summary>
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

        /// <summary>
        /// 最後に正常終了した MasterPattern。
        /// </summary>
        public EbsdMasterPattern MasterPattern { get; private set; } = null;

        private MasterPatternBuildRequest currentMasterPatternBuildRequest = null; // (260327Ch) build 条件はクラス内部だけで保持する

        /// <summary>
        /// Bethe 計算中または plane 変換中かどうかを返す。
        /// </summary>
        //public bool IsBuilding => bwMasterPattern.IsBusy || masterPatternBethe?.bwEBSD?.IsBusy == true || masterPatternBethe?.bwEBSDNew?.IsBusy == true; // (260321Ch) 新旧どちらの EBSD worker でも build 中と見なす
        public bool IsBuilding => bwMasterPattern.IsBusy || masterPatternBethe?.bwEBSDNew?.IsBusy == true; // (260327Ch) 旧 bwEBSD はお蔵入りにしたので ebsdNew 側だけを見る

        /// <summary>
        /// MasterPattern 作成中の進捗通知。
        /// </summary>
        public event EventHandler<MasterPatternProgressChangedEventArgs> MasterPatternProgressChanged;

        /// <summary>
        /// MasterPattern 作成完了通知。
        /// </summary>
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

            var positiveDirections = EbsdMasterPattern.CreateDirections(currentMasterPatternBuildRequest.GridSize, EbsdMasterPatternHemisphere.PositiveZ);
            var negativeDirections = EbsdMasterPattern.CreateDirections(currentMasterPatternBuildRequest.GridSize, EbsdMasterPatternHemisphere.NegativeZ);
            var directions = new Vector3DBase[positiveDirections.Length + negativeDirections.Length]; // (260321Ch) 全球分の方向配列をここで素直に連結する
            Array.Copy(positiveDirections, 0, directions, 0, positiveDirections.Length);
            Array.Copy(negativeDirections, 0, directions, positiveDirections.Length, negativeDirections.Length);
            masterPatternBethe = new BetheMethod(currentMasterPatternBuildRequest.Crystal);
            masterPatternBethe.EbsdRepresentativeDirectionBlockSize = currentMasterPatternBuildRequest.GridSize <= 512 ? 1
                : currentMasterPatternBuildRequest.GridSize <= 1024 ? 3
                : 5; // (260321Ch) 解像度に応じて 1 / 3 / 5 をその場で決める
            // masterPatternBethe.UseLocalSurfacePerBeamDirection = true; // (260321Ch) 旧案: 既存 ebsd_DoWork に局所表面近似を持ち込んでいた
            masterPatternBethe.UseLocalSurfacePerBeamDirection = false; // (260321Ch) 新経路では結晶を回すので、既存 worker 側の局所表面近似は使わない
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
            #region お蔵入り // (260327Ch) 旧 bwEBSD 側の cancel 経路は退避
            //if (masterPatternBethe?.bwEBSD?.IsBusy == true)
            //    masterPatternBethe.CancelEBSD();
            #endregion
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
                    // MasterPattern = EbsdMasterPattern.FromDisks(completedBethe.Disks, request.Energies, request.Depths, request.GridSize, request.Hemisphere); // (260321Ch) 旧案: 完了イベント内で同期的に plane を組み立てていた
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

        /// <summary>
        /// BetheMethod の出力ディスクを plane 配列へ並べ替え、MasterPattern を生成する。
        /// </summary>
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
                        var positivePlane = new float[hemisphereLength];
                        var negativePlane = new float[hemisphereLength];
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
                new EbsdMasterPattern(request.GridSize, [.. request.Energies], [.. request.Depths], positivePlanes, negativePlanes));
        }

        /// <summary>
        /// plane 変換 worker の進捗を MasterPattern 全体の進捗へ換算して通知する。
        /// </summary>
        private void MasterPattern_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var request = currentMasterPatternBuildRequest; // (260327Ch)
            if (request == null)
                return;

            var localProgress = Math.Clamp(e.ProgressPercentage, 0, 100);
            var progress = BetheStageWeight + Math.Min(CompileStageWeight, (int)Math.Round(localProgress / 100.0 * CompileStageWeight));
            MasterPatternProgressChanged?.Invoke(this, new MasterPatternProgressChangedEventArgs(progress, e.UserState)); // (260327Ch)
        }

        /// <summary>
        /// plane 変換 worker の完了結果を受け取り、公開イベントへ流す。
        /// </summary>
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

        /// <summary>
        /// 使い終わった BetheMethod のイベント配線を解除する。
        /// </summary>
        private void DetachMasterPatternBetheHandlers()
        {
            if (masterPatternBethe == null)
                return;

            masterPatternBethe.EBSD_ProgressChanged -= MasterPatternBethe_EBSD_ProgressChanged;
            masterPatternBethe.EBSD_Completed -= MasterPatternBethe_EBSD_Completed;
        }
    }

    /// <summary>
    /// Rosca-Lambert 等積正方形投影上に保持する EBSD MasterPattern。
    /// 1 つの energy-depth 組に対して 1 枚の plane を持つ。
    /// </summary>
    public sealed class EbsdMasterPattern
    {
        public static readonly double SquareLimit = Math.Sqrt(Math.PI / 2.0); // (260321Ch) 等積正方形投影の境界値

        /// <summary>一辺の分割数。</summary>
        public int GridSize { get; }

        /// <summary>保持しているエネルギー列。</summary>
        public double[] Energies { get; }

        /// <summary>保持している深さ列。</summary>
        public double[] Depths { get; }

        /// <summary>対応する半球。</summary>

        /// <summary>plane 本体。添字は energy-major で並ぶ。</summary>
        public float[][] PositivePlanes { get; } // (260321Ch) +Z 半球の plane 配列
        public float[][] NegativePlanes { get; } // (260321Ch) -Z 半球の plane 配列

        /// <summary> plane 数を返す。 </summary>
        public int PlaneCount => Energies.Length * Depths.Length;

        /// <summary> MasterPattern 本体を初期化する。 </summary>
        public EbsdMasterPattern(int gridSize, double[] energies, double[] depths, float[][] positivePlanes, float[][] negativePlanes)
        {
            GridSize = gridSize;
            Energies = energies ?? [];
            Depths = depths ?? [];
            PositivePlanes = positivePlanes ?? [];
            NegativePlanes = negativePlanes ?? [];
        }

        /// <summary>
        /// 指定した energy-depth 組の plane を返す。
        /// 範囲外のときは null を返す。
        /// </summary>
        public float[] GetPlane(EbsdMasterPatternHemisphere hemisphere, int energyIndex, int depthIndex)
        {
            if ((uint)energyIndex >= (uint)Energies.Length || (uint)depthIndex >= (uint)Depths.Length)
                return null;

            var planeIndex = energyIndex * Depths.Length + depthIndex;
            var planes = hemisphere == EbsdMasterPatternHemisphere.PositiveZ ? PositivePlanes : NegativePlanes;
            return (uint)planeIndex < (uint)planes.Length ? planes[planeIndex] : null;
        }

        /// <summary>
        /// Rosca-Lambert 格子の各セル中心に対応する出射方向を生成する。
        /// </summary>
        public static Vector3DBase[] CreateDirections(int gridSize, EbsdMasterPatternHemisphere hemisphere)
        {
            var directions = new Vector3DBase[gridSize * gridSize];
            var step = 2.0 * SquareLimit / gridSize;
            for (int h = 0; h < gridSize; h++)
                for (int w = 0; w < gridSize; w++)
                {
                    var a = -SquareLimit + (w + 0.5) * step;
                    var b = SquareLimit - (h + 0.5) * step; // (260321Ch) preview 表示の上方向が +Y に見えるよう Y を反転する
                    directions[h * gridSize + w] = RoscaLambertToSphere(a, b, hemisphere);
                }
            return directions;
        }

        public static double SqrtPI = Math.Sqrt(Math.PI);

        /// <summary>
        /// 球面方向を Rosca-Lambert 等積正方形上の座標 (a, b) へ逆変換する。260325Cl 追加
        /// </summary>
        public static (double a, double b) SphereToRoscaLambert(double x, double y, double z)
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

        /// <summary>
        /// Rosca-Lambert 正方形座標 (a, b) で plane 上の強度をバイリニア補間する。260325Cl 追加
        /// </summary>
        public static float InterpolatePlane(float[] plane, int gridSize, double a, double b)
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

        /// <summary>
        /// Rosca-Lambert 等積正方形上の座標を球面方向へ変換する。
        /// </summary>
        public static Vector3DBase RoscaLambertToSphere(double a, double b, EbsdMasterPatternHemisphere hemisphere)
        {
            if (Math.Abs(a) < 1.0E-15 && Math.Abs(b) < 1.0E-15)
                return hemisphere == EbsdMasterPatternHemisphere.PositiveZ ? new Vector3DBase(0, 0, 1) : new Vector3DBase(0, 0, -1);

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
            var z = hemisphere == EbsdMasterPatternHemisphere.PositiveZ ? 1.0 - rho2 / 2.0 : -1.0 + rho2 / 2.0;
            return new Vector3DBase(radialScale * A, radialScale * B, z);
        }
    }

    /// <summary>
    /// MasterPattern が対応する半球。
    /// </summary>
    public enum EbsdMasterPatternHemisphere
    {
        NegativeZ = -1,
        PositiveZ = 1,
    }
}

