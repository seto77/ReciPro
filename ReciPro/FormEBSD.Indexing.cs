#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace ReciPro;

/// <summary>
/// 260724Cl 追加: 実測 EBSD パターンの指数付け UI (方位候補の探索と適用)。
/// 260725Cl 訂正: コントロールの置き場は確定済み — 中央列 EBSD pattern 配下の tabControlPatternSettings →
/// Experimental image タブ (探索エンジンのラジオ・Find/Calibrate ボタン・候補 DataGridView)。旧 doc の「Overlays タブに仮置き」は解消。
/// 設計正本 = .project-guidance/ReciPro_EBSD物理・幾何レビュー.md §7。
/// 260724Cl 方針転換 (作者指示): バンドの離散検出 (Detect bands) と中心線表示・Optimize orientation ボタンを廃止し、
/// 「Find orientation candidates」に一本化。裏で Radon 証拠マップへの運動学的テンプレート照合 (EbsdRadonIndexer) で方位を直接探索し、
/// 動力学 MasterPattern が生成済みなら上位候補へ ZNCC 精密化を自動連結する。
/// コアアルゴリズムは Crystallography/EBSD/ (EbsdBandDetector.ComputeRadonMap, EbsdRadonIndexer, EbsdDetectorGeometry)。ここは UI orchestration のみ。
/// </summary>
public partial class FormEBSD
{
    //260724Cl 廃止: private List<EbsdBand> detectedBands (バンド離散検出の撤廃に伴い、中心線オーバーレイ表示ごと削除)

    /// <summary>指数付け候補 (スコア降順)</summary>
    private List<EbsdOrientationCandidate> orientationCandidates = null;

    private bool candidateGridInitialized = false;
    private bool skipCandidateSelectionEvent = false;

    /// <summary>探索・較正の実行状態 (実行中フラグ・世代番号・キャンセル要求)。260726Cl: UI から切り離して単体検証できるようにした
    /// (旧: indexingBusy / indexingGeneration / indexingCts の 3 フィールドを直接操作。正本 §6 P1)</summary>
    private readonly EbsdIndexingSession indexingSession = new();

    /// <summary>解析系ボタンの相互排他 (実行中の二重起動・stale 結果の適用を防ぐ)。260724Cl 追加。260726Cl: session へ委譲</summary>
    private bool indexingBusy => indexingSession.Busy;

    /// <summary>指数付け用反射リストの d 下限 (nm)。260724Cl (/simplify) 追加: 反射生成とステータス表示に二重ハードコードされていた値を一元化 (将来 UI 化候補)</summary>
    private const double KikuchiDLimit = 0.15;

    /// <summary>幾何較正の交互最適化 (方位 ⇄ PC/DD) の最大ラウンド数。260725Cl: 2 固定 → 10 → 20 (作者指示: 10 でも十分速い)。
    /// PC・DD・方位は単一パターンで強く相関しており、交互法は谷底でジグザグするため 2 ラウンドでは収束の保証が無かった。
    /// 実際には CalibrationZnccTolerance で早期終了するので、上限まで回るのは収束が遅い配置のときだけ</summary>
    private const int MaxCalibrationRounds = 20;

    /// <summary>1 ラウンドの ZNCC 改善がこれ未満なら収束とみなして較正を打ち切る。260725Cl 追加</summary>
    private const double CalibrationZnccTolerance = 1E-4;

    /// <summary>方位仕上げ (Find のトップ候補・較正の最終段) の Nelder-Mead 初期ステップ [°]。260725Cl 追加: 0.2 → 0.1 (作者指示)。
    /// 両者で同じ値を使う — 目的関数だけでなくステップも揃えないと、Find と Calibrate を繰り返したときに方位が微妙に往復する</summary>
    private const double OrientationPolishStepDeg = 0.1;

    /// <summary>較正の多点開始の点数。260726Cl: 10 → 200 → 40 (作者指示)。
    /// 1 点あたり 0.2 秒程度なので全体で 8 秒前後。200 点で ±8% を探しても最良は現在の幾何のままだったので、日常はこの点数で足りる</summary>
    private const int CalibrationStartCount = 40;

    /// <summary>多点開始の振れ幅。PC は検出器幅・高さに対する割合、DD は lnDD の絶対値 (0.08 ≈ 8%)。260726Cl 追加。
    /// 当初の PC ±1%・lnDD ±0.02 では実機で 200 点すべてが同じ谷に落ち (best #0、200 within 1E-3、spread 0.0007)、
    /// 多点開始が機能していなかった。作者が観測した別の谷はもっと離れているので広げる。
    /// 較正のソフト境界 (初期値から W/H の 25%、lnDD 0.35) の内側に収めること</summary>
    private const double CalibrationStartSpreadPc = 0.08, CalibrationStartSpreadLnDd = 0.08;

    /// <summary>較正の多点開始オフセット。単位は PC が検出器幅・高さの 1%、DD が lnDD 0.02 (≈2%)。260726Cl 追加 (作者要望)。
    /// 乱数を使わず決定的にする (同じ入力なら同じ結果)。[0] は現在の幾何そのもの、以降は Halton 列で [-1,1]³ を準一様に埋める。
    /// 局所解が多く (初期 DetX/Y/Z で最終スコアが 0.3 程度ばらつく)、同時最適化でも壁は越えられないので、開始点を変えて拾う。
    /// 260726Cl 変更: 旧は軸方向 6 点+対角 3 点の手書き 10 点。点数を増やすには系統的な列が要る</summary>
    private static readonly (double U, double V, double D)[] CalibrationStartOffsets = BuildCalibrationStartOffsets(CalibrationStartCount);

    private static (double U, double V, double D)[] BuildCalibrationStartOffsets(int count)
    {
        //Halton 列 (基数 2,3,5) を [0,1) → [-1,1] へ。低食い違い列なので、点数を増やすほど隙間なく埋まる
        static double Halton(int index, int b)
        {
            double f = 1, r = 0;
            for (int i = index; i > 0; i /= b) { f /= b; r += f * (i % b); }
            return r;
        }
        var offsets = new (double U, double V, double D)[count];
        offsets[0] = (0, 0, 0); //現在の幾何そのもの
        for (int i = 1; i < count; i++)
            offsets[i] = (2 * Halton(i, 2) - 1, 2 * Halton(i, 3) - 1, 2 * Halton(i, 5) - 1);
        return offsets;
    }

    /// <summary>較正の最後に行う 6 変数 (PC_u, PC_v, lnDD, 方位 3) 同時最適化の評価上限。260726Cl 追加。
    /// 6 次元なので交互法の 3 変数段 (120-150) より多く要る。1 評価ごとに projector を作り直す重い段だが、
    /// 交互法では下れない斜めの谷をここで下る</summary>
    private const int JointPolishMaxEval = 600;

    //260725Cl: 世代番号は「indexingBusy が Find/Calibrate ボタンしか無効化しないため、await 中の画像 D&D や
    //検出器幾何の変更で失効させたはずの候補が探索完了後に復活していた」問題への対策。
    //260726Cl 削除: 世代番号 indexingGeneration とキャンセル要求 indexingCts は EbsdIndexingSession へ移した
    //(失効の規則を UI から切り離して EbsdCheck で検証できるようにするため。正本 §6 P1)。ここには表示とグリッド操作だけが残る

    //260724Cl シグネチャ変更: バンド検出廃止に伴い clearBands 引数を削除。旧: private void InvalidateIndexingResults(bool clearBands)
    //260725Cl シグネチャ変更: announceCancel 追加。旧: private void InvalidateIndexingResults()
    /// <summary>画像/幾何の変更で方位候補を失効させる。260724Cl 追加 (Codex 指摘: stale 結果の誤適用防止)</summary>
    /// <param name="announceCancel">実行中なら中止要求をステータスバーへ出す。較正が自分の書き戻し後に呼ぶ場合だけ false</param>
    private void InvalidateIndexingResults(bool announceCancel = true)
    {
        //260725Cl 追加 (作者実機指摘): 探索中に幾何などを変えても画面が無反応に見えたので、中止要求を出した時点で表示する
        //(実際の停止はワーカーが次の中止チェックに到達するまで数十 ms 遅れる)
        if (announceCancel && indexingBusy) toolStripStatusLabelSummary.Text = "Canceling...";
        //260726Cl: 世代を進めて実行中の探索・較正も止める (旧: indexingGeneration++ と indexingCts?.Cancel() を直接操作)
        indexingSession.Invalidate();
        orientationCandidates = null;
        if (candidateGridInitialized)
        {
            skipCandidateSelectionEvent = true;
            try { dataGridViewEbsdCandidates.Rows.Clear(); }
            finally { skipCandidateSelectionEvent = false; }
        }
    }

    /// <summary>直近に進捗行を書き換えた時刻 (探索開始からの ms)。UI 更新の間引きに使う。260725Cl 追加</summary>
    private long lastIndexingProgressMs;

    /// <summary>進捗行に添える段の名前 (較正の "start 3/10" など)。260726Cl 追加</summary>
    private string indexingStage = "";

    //260726Cl 削除 (作者指示「2 分タイマーなど必要ない。常に最新の情報が出ていればよい」):
    //一時的なピン留め (statusPinnedUntilTick / SetPinnedStatus) は廃止。代わりに DrawEBSD 側で、
    //パターンの中身が変わらない再描画ではステータスを書き換えないようにした (FormEBSD.cs の lastEbsdRenderStatusKey)

    /// <summary>
    /// 探索・較正の進捗と経過時間をステータスバーへ出す (MasterPattern/MC と同じ canonical 進捗行)。260725Cl 追加
    /// (作者実機指摘: 探索中にプログレスバーが動かず、経過時間も出ていなかった)。
    /// ワーカースレッドから呼ばれるが、コントロールへの反映は StatusBarHelper 側が自動 Invoke する。
    /// </summary>
    //260726Cl シグネチャ変更: stage 追加 (較正の多点開始で "start 3/10" を出す)。旧: ReportIndexingProgress(double, Stopwatch)
    private void ReportIndexingProgress(double ratio, System.Diagnostics.Stopwatch sw, string stage = null)
    {
        if (!indexingBusy) return; //完了後に遅れて届いた通知で最終表示を壊さない
        if (stage != null) indexingStage = stage;
        long now = sw.ElapsedMilliseconds;
        if (stage == null && ratio < 1 && now - lastIndexingProgressMs < 200) return; //UI 更新は毎秒 5 回まで (段が変わったときは間引かない)
        lastIndexingProgressMs = now;
        StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, ratio, indexingStage, sw.Elapsed, showRemaining: true);
    }

    /// <summary>探索・較正の終了を進捗行へ書く。完了は 100%、中止・失敗はバーを戻して理由と経過時間だけ残す。260725Cl 追加</summary>
    private void FinishIndexingProgress(System.Diagnostics.Stopwatch sw, string canceledOrFailed = null)
    {
        if (canceledOrFailed == null)
            StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 1.0, "", sw.Elapsed);
        else
        {
            toolStripProgressBar.Value = 0;
            toolStripStatusLabelProgress.Text = $"{canceledOrFailed} after {StatusBarHelper.FormatElapsed(sw.Elapsed)}";
        }
    }

    //260726Cl シグネチャ変更: 世代番号とトークンを out で返す (旧: 呼び出し側が indexingGeneration / indexingCts.Token を直読み)
    private bool TryBeginIndexing(out System.Threading.CancellationToken cancel, out int generation)
    {
        if (!indexingSession.TryBegin(out cancel, out generation)) return false;
        lastIndexingProgressMs = 0; //260725Cl
        indexingStage = ""; //260726Cl
        StatusBarHelper.SetProgress(toolStripProgressBar, toolStripStatusLabelProgress, 0, "", TimeSpan.Zero, showRemaining: true); //260725Cl
        buttonFindOrientation.Enabled = buttonCalibrateGeometry.Enabled = false; //260724Cl: 廃止 2 ボタンを除去
        return true;
    }

    private void EndIndexing()
    {
        indexingSession.End(); //260726Cl: CTS の破棄と実行中フラグの解除
        buttonFindOrientation.Enabled = buttonCalibrateGeometry.Enabled = true; //260724Cl: 廃止 2 ボタンを除去
    }

    /// <summary>現在の UI 値から、実測画像のピクセルグリッドを基準にした検出器幾何スナップショットを作る</summary>
    private EbsdDetectorGeometry BuildDetectorGeometry(int imageWidth, int imageHeight)
        => new(DetTilt, DetX, DetY, DetZ, DetHalfWidth * 2 / imageWidth, imageWidth, imageHeight, DetectorXMirror, SmpTilt);

    //260724Cl 廃止: buttonDetectBands_Click / DrawDetectedBands (バンド離散検出とその中心線・縁点オーバーレイの撤廃。
    //検出パイプライン自体は EbsdBandDetector.Detect として Crystallography 側に残置 — 検証ハーネスが使用)

    #region 方位候補の探索 (Radon テンプレート照合 + ZNCC 自動精密化)

    private async void buttonFindOrientation_Click(object sender, EventArgs e)
    {
        if (expPbmp == null)
        {
            MessageBox.Show(this, "Load an experimental image first (drag && drop an image file).", "Find orientation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        //260724Cl 追加: Dictionary indexing (Primary) は MasterPattern 由来の辞書パターンと総当たり比較するため生成済みが必須
        bool useDictionary = radioButtonIndexingDictionary.Checked;
        if (useDictionary && MasterPattern == null)
        {
            MessageBox.Show(this, "Dictionary search requires the dynamical master pattern. Build it first, or use Radon search.", "Find orientation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        //260725Cl: generation = await 中に画像・幾何が変わったら結果を捨てるための世代番号、cancel = 無効化時に実計算も止めるトークン
        if (!TryBeginIndexing(out var cancel, out int generation)) return; //260726Cl: session から受け取る
        toolStripStatusLabelSummary.Text = "Searching orientation candidates...";
        var sw = System.Diagnostics.Stopwatch.StartNew(); //260725Cl: 中止・失敗時にも経過時間を出すので try の外で開始する
        try
        {
            var geom = BuildDetectorGeometry(expPbmp.Width, expPbmp.Height);
            var crystal = Crystal;

            //指数付け用の反射リスト (d>1.5Å) を UI スレッド上で一時生成し、表示用 VectorOfG_KikuchiLine は退避→復元する
            //(描画スレッドと共有される crystal 状態をワーカーから書き換えないため。生成は数十 ms。例外時も finally で必ず復元)
            var backup = crystal.VectorOfG_KikuchiLine;
            Vector3D[] reflections;
            try
            {
                crystal.SetVectorOfG_KikuchiLine(KikuchiDLimit, waveLengthControl.WaveSource);
                reflections = [.. crystal.VectorOfG_KikuchiLine];
            }
            finally { crystal.VectorOfG_KikuchiLine = backup; }

            var values = expPbmp.SrcValuesGray;
            int iw = expPbmp.Width, ih = expPbmp.Height;
            double wl = WaveLength; //nm (pair-angle シードの幅尤度用)

            //動力学 MasterPattern が生成済みなら ZNCC 複合ランクを自動連結 (旧 Optimize orientation ボタン相当。260724Cl 作者指示)
            //260724Cl 改訂 (ベンチ+Codex 裁定、指示書 §2.1): 生 ZNCC の再ランクは有害 (シミュレーションの heavy-tailed 生強度が支配し正解方位が偽方位に負ける) と実測で判明。
            //  ① Radon 採点は複合前提のとき証拠飽和 cap=8 (少数強リッジ支配の抑制。単独では 5-2_22 のトップが劣化するため複合とセットでのみ使う)
            //  ② 実測・シミュレーション両方に RobustPreprocess を掛けた ZNCC を候補集合内で標準化し、combo = zRadon + 0.5·clip(z,±2) で再ランク
            //  ③ ZNCC 精密化は複合トップ 1 件のみ ±0.25° (ガード: Radon z 低下 >0.2 で棄却)。ベンチ 3 画像で複合トップ全勝 (12/20, 5/15, 11/14)
            bool refineByZncc = MasterPattern != null;
            var ctx = refineByZncc ? SnapshotMatchingContext() : default;
            //260725Cl: UI スレッドで結晶状態をスナップショット。FZ 除外は proper 回転 1 個 (monoclinic C2) のみ実測検証済みなので、
            //cubic/hex 等の高対称系は pruning on/off の候補一致を検証するまで安全側で無効化する (Codex 裁定 260725。実測パターンが揃ったら解除)
            var properSyms = useDictionary && EbsdDictionaryIndexer.GetProperRotations(crystal) is { Length: 1 } syms ? syms : null;

            void Report(double r) => ReportIndexingProgress(r, sw); //260725Cl: 粗探索から進捗と経過時間を受ける
            var candidates = await Task.Run(() =>
            {
                const double SatCap = 8, ZnccCoef = 0.5; //260724Cl: EbsdIndexCheck ハーネスの係数スイープで決定 (プラトー 0.4-1.0 の中央寄り)
                var map = EbsdBandDetector.ComputeRadonMap(values, iw, ih);
                //260724Cl 追加: 探索エンジン切替 (ラジオボタン、作者指示)。Dictionary = MasterPattern 辞書の総当たり ZNCC (Primary indexing)。
                //候補には Radon z を後付けし、以降の複合ランク+ガード付きトップ精密化は両エンジン共通
                List<EbsdOrientationCandidate> cands;
                if (useDictionary)
                {
                    //260724Cl: thoroughCoarse=true (粗段も 96px 完全 robust 総当たり)。作者方針=辞書はパワープレーで精度優先。
                    //ベンチ (正しい共通幾何+MC 合成): 3 画像とも辞書トップ=正解系 (14/20・13/15・11/14、5-2_22 では Radon 経路を上回る)
                    //260725Cl: properSymmetries (点群 proper 回転の FZ 除外) + 面内分解プロジェクション + SIMD 前処理で
                    //12.5s→**2.4〜2.8s/画像** (結果は同一、C2 重複候補も解消)。260725Cl 訂正: 旧コメントの「→4.3s」は中間段階の値
                    //cands = EbsdDictionaryIndexer.Index(ctx.Mp, ctx.Pos, ctx.Neg, ctx.Geom, values, iw, ih, coarseStepDeg: 3, maxCandidates: 10, thoroughCoarse: true); //260725Cl 変更前
                    cands = EbsdDictionaryIndexer.Index(ctx.Mp, ctx.Pos, ctx.Neg, ctx.Geom, values, iw, ih, coarseStepDeg: 3, maxCandidates: 10, thoroughCoarse: true,
                        properSymmetries: properSyms, cancel: cancel, progress: Report); //260725Ch (progress は 260725Cl)
                    //260725Cl (/simplify): 候補ごとの ScoreOrientation はカタログを毎回組み直していた → 一括版で 1 回に (スコアは同一)
                    var radonZ = EbsdRadonIndexer.ScoreOrientations(map, geom, reflections, [.. cands.Select(c => c.Rotation)], SatCap);
                    for (int i = 0; i < cands.Count; i++)
                        cands[i].Score = radonZ[i];
                }
                else
                    cands = EbsdRadonIndexer.Index(map, geom, reflections, wl, maxCandidates: 10, saturateCap: refineByZncc ? SatCap : 0, cancel: cancel, progress: Report); //260725Ch (progress は 260725Cl)
                if (refineByZncc && cands.Count > 0)
                {
                    var projector = new EbsdPatternProjector(ctx.Geom, ctx.Rw, ctx.Rh);
                    var buf = new double[ctx.Rw * ctx.Rh];
                    var (refRobust, _, _) = EbsdPatternScorer.PrepareReferenceRobust(values, iw, ih, 160);
                    foreach (var c in cands) //全候補の robust ZNCC (未精密化 — 精密化はどの方位でも ZNCC を伸ばすため判別には使えない)
                    {
                        cancel.ThrowIfCancellationRequested(); //260725Ch
                        projector.Project(ctx.Mp, c.Rotation, ctx.Pos, ctx.Neg, buf);
                        c.Zncc = EbsdPatternScorer.Zncc(refRobust, EbsdPatternScorer.RobustPreprocess(buf, ctx.Rw, ctx.Rh));
                    }
                    //候補集合内で ZNCC を標準化 → 複合ランク (Radon の幾何証拠を主、ZNCC は ±2σ クリップの補助)
                    double mZ = cands.Average(c => c.Zncc);
                    double sZ = Math.Sqrt(Math.Max(cands.Average(c => (c.Zncc - mZ) * (c.Zncc - mZ)), 1E-12));
                    cands = [.. cands.OrderByDescending(c => c.Score + ZnccCoef * Math.Clamp((c.Zncc - mZ) / sZ, -2, 2))];
                    //複合トップのみ ZNCC 精密化 (±0.25°)。Radon z が 0.2 超劣化する精密化は棄却 (誤収束ガード)
                    var top = cands[0];
                    double Score(double[] v)
                    {
                        cancel.ThrowIfCancellationRequested(); //260725Ch: Nelder-Mead の評価境界で停止
                        projector.Project(ctx.Mp, EbsdIndexer.PerturbRotation(top.Rotation, v[0], v[1], v[2]), ctx.Pos, ctx.Neg, buf);
                        return -EbsdPatternScorer.Zncc(refRobust, EbsdPatternScorer.RobustPreprocess(buf, ctx.Rw, ctx.Rh));
                    }
                    var (b2, v2, _) = EbsdPatternScorer.NelderMead(Score, [0, 0, 0], [0.25, 0.25, 0.25], 120);
                    var rFin = EbsdIndexer.PerturbRotation(top.Rotation, b2[0], b2[1], b2[2]);
                    //260725Cl (/simplify): ガードの 2 回採点も一括版へ (旧: ScoreOrientation ×2 でカタログを 2 回構築)
                    var guard = EbsdRadonIndexer.ScoreOrientations(map, geom, reflections, [rFin, top.Rotation], SatCap);
                    if (guard[0] >= guard[1] - 0.2)
                    { top.Rotation = rFin; top.Zncc = -v2; }

                    //260725Cl 追加 (作者指示): ここまでは「候補の順位付け」のための保守的な微調整 (robust ZNCC を ±0.25° だけ)。
                    //順位が確定したあとの最終方位は、Calibrate geometry と同じ目的関数 (素の前処理での ZNCC = ctx.Ref) と
                    //同じステップ (0.7°→0.2°) で仕上げ直す。両者の目的関数が違うと「Find→トップ選択→Calibrate→再び Find」を
                    //繰り返したときに方位が 2 値を約 1° で往復して収束しない (作者の実機報告)。順位付けの保護 (±0.25°+ガード) は
                    //誤候補を ZNCC で押し上げないためのもので、最終方位の精度のためのものではない、という切り分け。
                    double ScoreRaw(double[] v)
                    {
                        cancel.ThrowIfCancellationRequested();
                        projector.Project(ctx.Mp, EbsdIndexer.PerturbRotation(top.Rotation, v[0], v[1], v[2]), ctx.Pos, ctx.Neg, buf);
                        return -EbsdPatternScorer.Zncc(ctx.Ref, buf);
                    }
                    var (p1, _, _) = EbsdPatternScorer.NelderMead(ScoreRaw, [0, 0, 0], [0.7, 0.7, 0.7], 150);
                    //260725Cl 変更: 仕上げステップ 0.2 → OrientationPolishStepDeg (0.1、作者指示)。較正の最終段と同じ値を使う
                    var (p2, _, _) = EbsdPatternScorer.NelderMead(ScoreRaw, p1, [OrientationPolishStepDeg, OrientationPolishStepDeg, OrientationPolishStepDeg], 100);
                    var rPolished = EbsdIndexer.PerturbRotation(top.Rotation, p2[0], p2[1], p2[2]);
                    //仕上げでも同じ誤収束ガード (Radon の幾何証拠を 0.2 超失うなら採用しない)
                    var guardPolish = EbsdRadonIndexer.ScoreOrientations(map, geom, reflections, [rPolished, top.Rotation], SatCap);
                    if (guardPolish[0] >= guardPolish[1] - 0.2)
                    {
                        top.Rotation = rPolished;
                        //表示中の ZNCC 列は順位付けに使った robust 値なので、仕上げ後の方位で取り直して列と方位の意味を一致させる
                        projector.Project(ctx.Mp, top.Rotation, ctx.Pos, ctx.Neg, buf);
                        top.Zncc = EbsdPatternScorer.Zncc(refRobust, EbsdPatternScorer.RobustPreprocess(buf, ctx.Rw, ctx.Rh));
                    }
                }
                #region お蔵入り //260724Cl: 旧 ZNCC 連結 (上位 5 候補を ±1° 精密化して ZNCC 降順に再ランク)。精密化 ZNCC は誤方位ほど伸び正解を落とすため廃止
                //if (refineByZncc && cands.Count > 0)
                //{
                //    var projector = new EbsdPatternProjector(ctx.Geom, ctx.Rw, ctx.Rh);
                //    var buf = new double[ctx.Rw * ctx.Rh];
                //    foreach (var c in cands.Take(5)) //ZNCC は上位 5 候補のみ (1 候補 ~250 評価)
                //    {
                //        double Score(double[] v)
                //        {
                //            projector.Project(ctx.Mp, PerturbRotation(c.Rotation, v[0], v[1], v[2]), ctx.Pos, ctx.Neg, buf);
                //            return -EbsdPatternScorer.Zncc(ctx.Ref, buf);
                //        }
                //        var (b1, _, _) = EbsdPatternScorer.NelderMead(Score, [0, 0, 0], [1.0, 1.0, 1.0], 150);
                //        var (b2, v2, _) = EbsdPatternScorer.NelderMead(Score, b1, [0.25, 0.25, 0.25], 100);
                //        c.Rotation = PerturbRotation(c.Rotation, b2[0], b2[1], b2[2]);
                //        c.Zncc = -v2;
                //    }
                //    cands = [.. cands.OrderByDescending(c => double.IsNaN(c.Zncc) ? double.MinValue : c.Zncc)];
                //}
                #endregion
                return cands;
            }, cancel); //260725Ch
            sw.Stop();

            //260725Cl 追加: 探索中に実測画像の差し替えや検出器幾何の変更があった場合、この結果は既に失効しているので適用しない
            if (!indexingSession.IsCurrent(generation)) //260726Cl: session へ委譲
            {
                toolStripStatusLabelSummary.Text = "Orientation search discarded (the image or geometry changed)";
                FinishIndexingProgress(sw, "Canceled"); //260725Cl
                return;
            }
            orientationCandidates = candidates;
            FillCandidateGrid();
            FinishIndexingProgress(sw); //260725Cl: 進捗行を 100% で締める
            //260724Cl: 使用モードを明示 (Codex 裁定)。旧: (refineByZncc ? " (ZNCC refined)" : "")
            toolStripStatusLabelSummary.Text = $"Orientation search: {candidates.Count} candidates" +
                (useDictionary ? " (Dictionary + ZNCC combo)" : refineByZncc ? " (Radon + ZNCC combo)" : " (Radon only)"); //260724Cl: Dictionary モード表示追加
            toolStripStatusLabelDetail.Text = $"{sw.Elapsed.TotalMilliseconds:f0} ms, {reflections.Length} reflections (d>{KikuchiDLimit * 10:0.#}A). Click a row to apply the orientation."; //260724Cl (/simplify): 表示値を定数から導出
        }
        catch (OperationCanceledException) //260725Ch: 入力変更による正常な中止を失敗表示にしない
        {
            toolStripStatusLabelSummary.Text = indexingSession.IsCurrent(generation) ? "Orientation search canceled" : "Orientation search discarded (the image or geometry changed)";
            toolStripStatusLabelDetail.Text = "";
            FinishIndexingProgress(sw, "Canceled"); //260725Cl
        }
        catch (Exception ex)
        {
            toolStripStatusLabelSummary.Text = "Orientation search failed";
            toolStripStatusLabelDetail.Text = ex.Message;
            FinishIndexingProgress(sw, "Failed"); //260725Cl
        }
        finally { EndIndexing(); }
    }

    private void EnsureCandidateGridColumns()
    {
        if (candidateGridInitialized) return;
        candidateGridInitialized = true;
        var g = dataGridViewEbsdCandidates;
        g.AllowUserToAddRows = false;
        g.ReadOnly = true;
        g.RowHeadersVisible = false;
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        g.MultiSelect = false;
        g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        //260724Cl: Radon 方位探索用に列を再構成 (Score=SNR z 値、Bands=強い証拠を持つ予測バンド/視野内予測バンド、RMS° 列は廃止)
        //260725Cl 変更: 幅を DPI 換算 + 最終列を Fill に。旧実装は 96 DPI 前提の生ピクセル固定だったため、
        //フォントだけ DPI で拡大して高 DPI でヘッダが文字切れし、かつ固定合計幅がグリッド実幅を超えて常時横スクロールになっていた
        //(BeamInteraction 4 表の [project_minitable_readonly_grid] と同型の問題)
        int Dpi(int px96) => (int)Math.Round(px96 * DeviceDpi / 96.0);
        //旧: Width = 24 / 46 / 44 / 48 / 284 (px 固定)
        g.Columns.AddRange(
            new DataGridViewTextBoxColumn { HeaderText = "#", Width = Dpi(28) },
            new DataGridViewTextBoxColumn { HeaderText = "Score", Width = Dpi(52) },
            new DataGridViewTextBoxColumn { HeaderText = "Bands", Width = Dpi(52) },
            new DataGridViewTextBoxColumn { HeaderText = "ZNCC", Width = Dpi(52) },
            //残り幅を最終列が吸収する (hkl 列は内容が可変長なので Fill が自然。横スクロールバーも出なくなる)
            new DataGridViewTextBoxColumn { HeaderText = "Strong bands (hkl)", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, MinimumWidth = Dpi(120) });
        g.SelectionChanged += dataGridViewEbsdCandidates_SelectionChanged;
    }

    private void FillCandidateGrid()
    {
        EnsureCandidateGridColumns();
        var g = dataGridViewEbsdCandidates;
        skipCandidateSelectionEvent = true;
        try
        {
            g.Rows.Clear();
            if (orientationCandidates != null)
                foreach (var (c, i) in orientationCandidates.Select((c, i) => (c, i)))
                    //260725Cl: Score も非有限ガード (辞書経路は ScoreOrientations の double.MinValue センチネル
                    //= 視野内予測バンドが 4 本未満のとき をそのまま入れるため、309 桁の数値がセルに出るのを防ぐ)
                    //g.Rows.Add(i, double.IsFinite(c.Score) ? $"{c.Score:f1}" : "-", $"{c.AssignedBands}/{c.TotalBands}", //260725Ch 変更前: double.MinValue は finite なので 309 桁表示になっていた
                    //    double.IsNaN(c.Zncc) ? "-" : $"{c.Zncc:f3}", c.HklText);
                    g.Rows.Add(i, c.Score != double.MinValue && double.IsFinite(c.Score) ? $"{c.Score:f1}" : "-", $"{c.AssignedBands}/{c.TotalBands}", //260725Ch
                        double.IsFinite(c.Zncc) ? $"{c.Zncc:f3}" : "-", c.HklText); //260724Cl: AssignmentText (band:hkl) → HklText
            g.ClearSelection();
        }
        finally { skipCandidateSelectionEvent = false; }
    }

    /// <summary>候補行の選択で方位を全アプリへ適用 (シミュレーションが実測に重なって描画される)</summary>
    private void dataGridViewEbsdCandidates_SelectionChanged(object sender, EventArgs e)
    {
        if (skipCandidateSelectionEvent || orientationCandidates == null || dataGridViewEbsdCandidates.SelectedRows.Count == 0) return;
        int idx = dataGridViewEbsdCandidates.SelectedRows[0].Index;
        if ((uint)idx < (uint)orientationCandidates.Count)
            FormMain.SetRotation(orientationCandidates[idx].Rotation);
    }

    #endregion

    #region ZNCC ヘルパ・検出器幾何較正 (動力学 MasterPattern 必須)

    //260725Cl (/simplify): ローカル PerturbRotation は EbsdIndexer.PerturbRotation へ統合 (Crystallography 側の
    //EbsdDictionaryIndexer.Perturb・EbsdRadonIndexer.Perturb と 3 重複していた。式・演算順・規約 (試料系左摂動) は同一)。旧:
    ///// <summary>方位摂動: R(ω) = Rot(ω̂,|ω|)·R0 (試料系左摂動、単位 deg)。FormMain.Rotate の左乗算と同じ規約</summary>
    //private static Matrix3D PerturbRotation(Matrix3D r0, double wxDeg, double wyDeg, double wzDeg)
    //{
    //    double wx = wxDeg * Math.PI / 180, wy = wyDeg * Math.PI / 180, wz = wzDeg * Math.PI / 180;
    //    double len = Math.Sqrt(wx * wx + wy * wy + wz * wz);
    //    if (len < 1E-12) return r0;
    //    return Matrix3D.Rot((wx / len, wy / len, wz / len), len) * r0;
    //}

    /// <summary>MC 重み合成パターンのキャッシュ (MasterPattern と mcDistribution の組が同一なら再利用、合成は ~100ms)。260724Cl 追加</summary>
    private (MasterPattern Mp, EbsdMonteCarloDistribution Dist, float[] Pos, float[] Neg) composedPatternCache;

    /// <summary>ZNCC 系操作に必要な状態を UI スレッド上でスナップショットする (ワーカーからコントロールを読まないため)。260724Cl 追加</summary>
    private (EbsdDetectorGeometry Geom, MasterPattern Mp, float[] Pos, float[] Neg, double[] Ref, int Rw, int Rh, Matrix3D R0) SnapshotMatchingContext()
    {
        var geom = BuildDetectorGeometry(expPbmp.Width, expPbmp.Height);
        var mp = MasterPattern;
        float[] pos, neg;
        //260724Cl 改訂 (作者指示「エネルギー 1 点はまずい」): MC 分布があれば全ビン平均重みの微分合成パターン
        //(実稼働の表示合成 model 2 のグローバル近似) を ZNCC 比較に使う。単一スライスより実測との相関が上がることをハーネスで実証。
        //MC 未実行 (通常は MasterPattern build 前段で必ず走る) 時のみ旧来の trackBar 選択単一スライスへフォールバック
        if (mcDistribution != null)
        {
            if (!ReferenceEquals(composedPatternCache.Mp, mp) || !ReferenceEquals(composedPatternCache.Dist, mcDistribution))
            {
                var (p, n) = mcDistribution.ComposeGlobalWeightedPattern(mp);
                composedPatternCache = (mp, mcDistribution, p, n);
            }
            (pos, neg) = (composedPatternCache.Pos, composedPatternCache.Neg);
        }
        else
        {
            int eIdx = Math.Clamp(trackBarOutputEnergy.Value, 0, mp.Energies.Length - 1);
            int dIdx = Math.Clamp(trackBarOutputThickness.Value, 0, mp.Depths.Length - 1);
            pos = mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, eIdx, dIdx);
            neg = mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, eIdx, dIdx);
        }
        var (refData, rw, rh) = EbsdPatternScorer.PrepareReference(expPbmp.SrcValuesGray, expPbmp.Width, expPbmp.Height, 160);
        return (geom, mp, pos, neg, refData, rw, rh, new Matrix3D(Crystal.RotationMatrix));
    }

    private bool CheckMatchingPrerequisites(string title)
    {
        if (expPbmp == null)
        {
            MessageBox.Show(this, "Load an experimental image first (drag && drop an image file).", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        if (MasterPattern == null)
        {
            MessageBox.Show(this, "Build the dynamical master pattern first (this function compares simulated and experimental patterns pixel by pixel).", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }
        return true;
    }

    //260724Cl 廃止: buttonOptimizeOrientation_Click (Find orientation candidates の ZNCC 自動連結へ統合。作者指示)

    /// <summary>
    /// 検出器のパターンセンター (PC) と検出器距離 (DD) を較正する (方位も交互に微調整)。DetTilt は固定。260724Cl 追加
    /// 単一パターンでは DetTilt と方位 X 回転がゲージ自由度になるため Tilt は較正しない (設計正本 §7.2 / Codex 裁定)。
    /// 最適化は (PC_u, PC_v, ln DD) と方位 3 変数の alternating fit。結果は DetX/DetY/DetZ へ逆変換して書き戻す。
    /// </summary>
    private async void buttonCalibrateGeometry_Click(object sender, EventArgs e)
    {
        if (!CheckMatchingPrerequisites("Calibrate detector geometry")) return;
        //260725Cl: generation = await 中に実測画像・幾何が変わったら較正結果を書き戻さないための世代番号 (旧画像に合わせた幾何の誤適用防止)
        if (!TryBeginIndexing(out var cancel, out int generation)) return; //260726Cl: session から受け取る
        toolStripStatusLabelSummary.Text = "Calibrating detector geometry (PC/DD + orientation)...";
        var sw = System.Diagnostics.Stopwatch.StartNew(); //260725Cl: 中止・失敗時にも経過時間を出すので try の外で開始する
        try
        {
            var ctx = SnapshotMatchingContext();
            double detTilt = DetTilt, smpTilt = SmpTilt, xm = DetectorXMirror, pixelSize = ctx.Geom.PixelSize;
            int imgW = expPbmp.Width, imgH = expPbmp.Height;
            var (footU0, footV0) = ctx.Geom.PatternCenterMm; //260724Cl (/simplify): PC 式の手書き重複 (-DetX, -(DetY cosδ+DetZ sinδ)) を幾何オブジェクトへ一元化
            double dd0 = ctx.Geom.CameraLength;
            double physW = DetHalfWidth * 2, physH = DetHalfHeight * 2;
            //if (dd0 < 1E-3) { toolStripStatusLabelSummary.Text = "Invalid camera length"; EndIndexing(); return; } //260725Ch 変更前: finally でも二重に EndIndexing していた
            if (dd0 < 1E-3) { toolStripStatusLabelSummary.Text = "Invalid camera length"; return; } //260725Ch

            var result = await Task.Run(() =>
            {
                var buf = new double[ctx.Rw * ctx.Rh];
                int evalTotal = 0;
                //260726Cl 変更 (作者報告「プログレスバーの挙動がおかしい」): 旧実装は「評価回数 / 静的な予算」で進捗を出していたが、
                //予算は最大ラウンド (20) を使い切る前提なのに実際は 1-2 ラウンドで収束するため、バーは 3 割ほどで止まって最後に 100% へ飛んでいた
                //(実測 334,551 評価 / 予算 1,220,000)。**完了した開始点の数**を主軸にし、実行中の開始点の内側だけを
                //「これまでの 1 点あたり実測平均」で按分する。1 点目だけは実測が無いので静的な予算で見積もる。
                //旧: int evalBudget = CalibrationStartOffsets.Length * PerStartBudget; ratio = evalsDone / evalBudget
                int evalsDone = 0;
                const int PerStartBudget = MaxCalibrationRounds * (150 + 120) + 100 + JointPolishMaxEval;
                int completedStarts = 0, evalsAtStartBegin = 0;
                double avgEvalsPerStart = PerStartBudget;

                EbsdDetectorGeometry MakeGeom(double u, double v, double ld)
                {
                    var (dx, dy, dz) = EbsdDetectorGeometry.FromPatternCenter(u, v, Math.Exp(ld), detTilt);
                    return new EbsdDetectorGeometry(detTilt, dx, dy, dz, pixelSize, imgW, imgH, xm, smpTilt);
                }
                double ScoreWith(EbsdPatternProjector proj, Matrix3D rot)
                {
                    cancel.ThrowIfCancellationRequested(); //260725Ch: 各評価の投影前に中止を反映
                    //260726Cl: 完了した開始点 + 実行中の開始点の按分。NM は逐次なので単純加算で足りる
                    evalsDone++;
                    double inCurrentStart = Math.Min(0.99, (evalsDone - evalsAtStartBegin) / Math.Max(1, avgEvalsPerStart));
                    ReportIndexingProgress(Math.Min(0.99, (completedStarts + inCurrentStart) / CalibrationStartOffsets.Length), sw);
                    proj.Project(ctx.Mp, rot, ctx.Pos, ctx.Neg, buf);
                    return -EbsdPatternScorer.Zncc(ctx.Ref, buf);
                }
                double startZncc = -ScoreWith(new EbsdPatternProjector(MakeGeom(footU0, footV0, Math.Log(dd0)), ctx.Rw, ctx.Rh), ctx.R0);

                //260726Cl 追加 (作者要望): 1 開始点ぶんの較正 (交互法 → 方位仕上げ → 6 変数同時) を関数化し、多点開始から呼ぶ
                (double Zncc, double Fu, double Fv, double LnDd, Matrix3D Rot, int Rounds, bool Converged, double JointGain) RunFrom(double fu, double fv, double lnDd)
                {
                    var r0 = ctx.R0;
                    //260725Cl 変更 (作者指示): 旧 for (int round = 0; round < 2; round++) — 2 ラウンド固定で収束判定なし。
                    //PC・DD・方位の相関で交互法はジグザグするため、改善が止まるまで最大 MaxCalibrationRounds 回まわす
                    int roundsUsed = 0;
                    bool converged = false;
                    double prevZncc = -ScoreWith(new EbsdPatternProjector(MakeGeom(fu, fv, lnDd), ctx.Rw, ctx.Rh), r0);
                    for (int round = 0; round < MaxCalibrationRounds; round++)
                    {
                        cancel.ThrowIfCancellationRequested(); //260725Ch
                        //① 幾何固定で方位 (粗 0.7°)
                        var projFixed = new EbsdPatternProjector(MakeGeom(fu, fv, lnDd), ctx.Rw, ctx.Rh);
                        var (bo, _, eo) = EbsdPatternScorer.NelderMead(v => ScoreWith(projFixed, EbsdIndexer.PerturbRotation(r0, v[0], v[1], v[2])), [0, 0, 0], [0.7, 0.7, 0.7], 150);
                        r0 = EbsdIndexer.PerturbRotation(r0, bo[0], bo[1], bo[2]); evalTotal += eo;

                        //② 方位固定で幾何 (dU, dV [mm], dlnDD)。ステップ = 検出器幅/高の 1%、lnDD 0.02
                        //260724Cl: 単一パターンの PC-DD-方位縮退で非物理領域へ流れないよう soft bounds (初期値から W/H の 25%・DD ±40% でペナルティ)
                        var rFixed = r0;
                        var (bg, vg, eg) = EbsdPatternScorer.NelderMead(
                            v => (Math.Abs(v[0]) > physW * 0.25 || Math.Abs(v[1]) > physH * 0.25 || Math.Abs(v[2]) > 0.35)
                                ? 10 + Math.Abs(v[0]) / physW + Math.Abs(v[1]) / physH + Math.Abs(v[2])
                                : ScoreWith(new EbsdPatternProjector(MakeGeom(fu + v[0], fv + v[1], lnDd + v[2]), ctx.Rw, ctx.Rh), rFixed),
                            [0, 0, 0], [physW * 0.01, physH * 0.01, 0.02], 120);
                        fu += bg[0]; fv += bg[1]; lnDd += bg[2]; evalTotal += eg;
                        roundsUsed = round + 1;

                        //260725Cl: このラウンドの ZNCC 到達点で収束判定 (soft bounds のペナルティ値が返った場合は改善なしとして扱われる)
                        double zncc = -vg;
                        if (zncc - prevZncc < CalibrationZnccTolerance) { converged = true; break; }
                        prevZncc = zncc;
                    }
                    //仕上げの方位微調整。260725Cl 変更: 0.2° → OrientationPolishStepDeg (0.1°、作者指示)。Find の仕上げ段と同じ値
                    var projFinal = new EbsdPatternProjector(MakeGeom(fu, fv, lnDd), ctx.Rw, ctx.Rh);
                    var (bf, vf, ef) = EbsdPatternScorer.NelderMead(v => ScoreWith(projFinal, EbsdIndexer.PerturbRotation(r0, v[0], v[1], v[2])),
                        [0, 0, 0], [OrientationPolishStepDeg, OrientationPolishStepDeg, OrientationPolishStepDeg], 100);
                    r0 = EbsdIndexer.PerturbRotation(r0, bf[0], bf[1], bf[2]); evalTotal += ef;

                    //260726Cl 追加 (作者要望): 6 変数 (PC_u, PC_v, lnDD, 方位 3) の同時最適化を仕上げに 1 段。
                    //交互法は変数を片方ずつしか動かせないので、相関のある谷では斜め方向に下れずジグザグして止まる。
                    //実機報告でも初期 DetX/Y/Z を変えると最終スコアが 20.0〜20.3 程度ばらついていた。
                    //開始点 (増分ゼロ) が初期シンプレックスの頂点 0 で、NelderMead は最良頂点を返すので、この段で悪化することはない。
                    //ソフト境界は交互法の②と同じ判定を増分に対して掛ける (この段の増分は小さいので通常は発火しない)。
                    var rBase = r0;
                    double fuBase = fu, fvBase = fv, lnDdBase = lnDd;
                    double ScoreJoint(double[] v)
                    {
                        if (Math.Abs(v[0]) > physW * 0.25 || Math.Abs(v[1]) > physH * 0.25 || Math.Abs(v[2]) > 0.35)
                            return 10 + Math.Abs(v[0]) / physW + Math.Abs(v[1]) / physH + Math.Abs(v[2]);
                        return ScoreWith(new EbsdPatternProjector(MakeGeom(fuBase + v[0], fvBase + v[1], lnDdBase + v[2]), ctx.Rw, ctx.Rh),
                            EbsdIndexer.PerturbRotation(rBase, v[3], v[4], v[5]));
                    }
                    //幾何側は交互法②の半分のステップ (もう最適点の近くにいる)、方位側は仕上げと同じ 0.1°
                    var (bj, vj, ej) = EbsdPatternScorer.NelderMead(ScoreJoint, [0, 0, 0, 0, 0, 0],
                        [physW * 0.005, physH * 0.005, 0.01, OrientationPolishStepDeg, OrientationPolishStepDeg, OrientationPolishStepDeg], JointPolishMaxEval);
                    fu = fuBase + bj[0]; fv = fvBase + bj[1]; lnDd = lnDdBase + bj[2];
                    r0 = EbsdIndexer.PerturbRotation(rBase, bj[3], bj[4], bj[5]); evalTotal += ej;

                    return (Zncc: -vj, Fu: fu, Fv: fv, LnDd: lnDd, Rot: r0, Rounds: roundsUsed, Converged: converged,
                        JointGain: -vj - -vf); //260726Cl: 同時最適化が交互法の到達点からどれだけ伸ばしたか
                }

                //260726Cl 追加 (作者要望): 多点開始。局所解が多く、初期 DetX/Y/Z を変えると最終スコアが 0.3 程度ばらつくため、
                //現在の幾何と、そこから決定的に振った開始点から同じ較正を走らせ、最も ZNCC の高い解を採る。
                //同時最適化は交互法の停滞は解消するが局所解の壁は越えないので、壁の向こう側は開始点を変えて拾うしかない
                (double Zncc, double Fu, double Fv, double LnDd, Matrix3D Rot, int Rounds, bool Converged, double JointGain) bestRun = default;
                int bestIndex = -1;
                double worstZncc = double.MaxValue;
                var runs = new (double Zncc, double Fu, double Fv, double Dd)[CalibrationStartOffsets.Length]; //260726Cl: 最良解へ到達した点の数と、その幾何の広がりを見るため
                for (int s = 0; s < CalibrationStartOffsets.Length; s++)
                {
                    cancel.ThrowIfCancellationRequested();
                    ReportIndexingProgress(Math.Min(0.99, (double)s / CalibrationStartOffsets.Length), sw, $"start {s + 1}/{CalibrationStartOffsets.Length}"); //260726Cl
                    var (ou, ov, od) = CalibrationStartOffsets[s];
                    //260726Cl 変更: 振れ幅を定数化 (旧 physW*0.01 / physH*0.01 / 0.02 は狭すぎて全点が同じ谷に落ちていた)
                    var run = RunFrom(footU0 + ou * physW * CalibrationStartSpreadPc, footV0 + ov * physH * CalibrationStartSpreadPc,
                        Math.Log(dd0) + od * CalibrationStartSpreadLnDd);
                    runs[s] = (run.Zncc, run.Fu, run.Fv, Math.Exp(run.LnDd));
                    worstZncc = Math.Min(worstZncc, run.Zncc);
                    if (bestIndex < 0 || run.Zncc > bestRun.Zncc) { bestRun = run; bestIndex = s; }
                    //260726Cl: 進捗の按分に使う「1 点あたりの実測評価数」を更新する
                    completedStarts = s + 1;
                    avgEvalsPerStart = (double)evalsDone / completedStarts;
                    evalsAtStartBegin = evalsDone;
                }
                //最良から 1E-3 以内に入った開始点の数 = 最良解の basin の広さ。spread (最良−最悪) だけだと外れ値に引きずられる
                var near = runs.Where(r => r.Zncc >= bestRun.Zncc - 1E-3).ToArray();
                //260726Cl 追加 (作者要望): その集団の PC・DD の広がり (半値幅) = ZNCC で幾何がどこまで決まっているか。
                //ZNCC 1E-3 以内で PC が数 mm 動くなら、単一パターンでは幾何がその精度までしか決まっていない (正本 §2.4)
                double flatU = (near.Max(r => r.Fu) - near.Min(r => r.Fu)) / 2;
                double flatV = (near.Max(r => r.Fv) - near.Min(r => r.Fv)) / 2;
                double flatDd = (near.Max(r => r.Dd) - near.Min(r => r.Dd)) / 2;

                return (Rot: bestRun.Rot, Fu: bestRun.Fu, Fv: bestRun.Fv, Dd: Math.Exp(bestRun.LnDd), Zncc: bestRun.Zncc, ZnccStart: startZncc,
                    Evals: evalTotal, Rounds: bestRun.Rounds, Converged: bestRun.Converged, JointGain: bestRun.JointGain,
                    Starts: CalibrationStartOffsets.Length, BestIndex: bestIndex, Spread: bestRun.Zncc - worstZncc, NearBest: near.Length, //260726Cl: 局所解のばらつきを可視化
                    FlatU: flatU, FlatV: flatV, FlatDd: flatDd); //260726Cl: ZNCC が同等な解の集団における PC・DD の広がり (半値幅、mm)
            }, cancel); //260725Ch
            sw.Stop();

            //260725Cl 追加: 較正中に実測画像の差し替えや幾何の変更があった場合、この結果は失効しているので書き戻さない
            if (!indexingSession.IsCurrent(generation)) //260726Cl: session へ委譲
            {
                toolStripStatusLabelSummary.Text = "Geometry calibration discarded (the image or geometry changed)";
                FinishIndexingProgress(sw, "Canceled"); //260725Cl
                return;
            }

            //DetX/DetY/DetZ へ逆変換して書き戻し (DetTilt 固定)。numericBox の範囲へクランプ (260724Cl)
            var (detX, detY, detZ) = EbsdDetectorGeometry.FromPatternCenter(result.Fu, result.Fv, result.Dd, detTilt);
            skipDetectorGeometryEvent = true;
            try
            {
                numericBoxXofDet.Value = Math.Clamp(detX, numericBoxXofDet.Minimum, numericBoxXofDet.Maximum);
                numericBoxYofDet.Value = Math.Clamp(detY, numericBoxYofDet.Minimum, numericBoxYofDet.Maximum);
                numericBoxZofDet.Value = Math.Clamp(detZ, numericBoxZofDet.Minimum, numericBoxZofDet.Maximum);
            }
            finally { skipDetectorGeometryEvent = false; }
            UpdateEbsdTiltCoeffs();
            RebinMcDistribution();
            InvalidateIndexingResults(announceCancel: false); //260725Ch: 較正前の幾何で得た候補を残さず、実行世代も進める (260725Cl: これは自分の書き戻しなので "Canceling..." は出さない)
            FormMain.SetRotation(result.Rot); //Draw は SetRotation → FormMain 経由で走る
            FinishIndexingProgress(sw); //260725Cl: 進捗行を 100% で締める (InvalidateIndexingResults の "Canceling..." より後に出す)

            //260726Cl 変更 (作者報告「最後に消える」の真因): StatusStrip はフォーム幅 (1424px) に収まらない項目を描画しないので、
            //長い文字列を入れると書いた瞬間に見えなくなる。旧 Detail は 220 文字あった。表示は 2 ラベル合計 160 文字程度に収める。
            //旧 Detail: "PC (...)→(...) mm, DD ...→... mm, best of N starts (#k, m within 1E-3, spread ...), r/R rounds (...), joint 6-var ..., E evals, T ms. Tilt is kept fixed (single-pattern gauge)."
            toolStripStatusLabelSummary.Text = $"Geometry calibrated: ZNCC {result.ZnccStart:f3} → {result.Zncc:f3}" +
                $" (best #{result.BestIndex}/{result.Starts}, {result.NearBest} within 1E-3)"; //260726Cl: 多点開始。within が少ないほど局所解が深い
            //260725Cl: 交互最適化のラウンド数と収束可否 (上限に張り付くなら未収束の疑い)。260726Cl: evals と傾斜の注記は冗長なので削除
            //260726Cl: flat = ZNCC が最良と 1E-3 以内で並ぶ解の集団における PC・DD の広がり (半値幅)。単一パターンで幾何がどこまで決まるかの実測値
            toolStripStatusLabelDetail.Text = $"ΔPC ({result.Fu - footU0:+0.00;-0.00},{result.Fv - footV0:+0.00;-0.00}), ΔDD {result.Dd - dd0:+0.00;-0.00} mm; " +
                $"flat ±{result.FlatU:f2}/±{result.FlatV:f2} PC, ±{result.FlatDd:f2} DD; " +
                $"{result.Rounds}/{MaxCalibrationRounds} rounds{(result.Converged ? "" : " (limit)")}, joint {(result.JointGain > 0 ? "+" : "")}{result.JointGain:f4}, {sw.Elapsed.TotalSeconds:f1} s";
        }
        catch (OperationCanceledException) //260725Ch
        {
            toolStripStatusLabelSummary.Text = indexingSession.IsCurrent(generation) ? "Geometry calibration canceled" : "Geometry calibration discarded (the image or geometry changed)";
            toolStripStatusLabelDetail.Text = "";
            FinishIndexingProgress(sw, "Canceled"); //260725Cl
        }
        catch (Exception ex)
        {
            toolStripStatusLabelSummary.Text = "Geometry calibration failed";
            toolStripStatusLabelDetail.Text = ex.Message;
            FinishIndexingProgress(sw, "Failed"); //260725Cl
        }
        finally { EndIndexing(); }
    }

    #endregion
}
