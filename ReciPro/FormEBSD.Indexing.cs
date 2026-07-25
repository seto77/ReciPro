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

    /// <summary>解析系ボタンの相互排他 (実行中の二重起動・stale 結果の適用を防ぐ)。260724Cl 追加</summary>
    private bool indexingBusy = false;

    /// <summary>指数付け用反射リストの d 下限 (nm)。260724Cl (/simplify) 追加: 反射生成とステータス表示に二重ハードコードされていた値を一元化 (将来 UI 化候補)</summary>
    private const double KikuchiDLimit = 0.15;

    /// <summary>指数付け結果の世代番号。InvalidateIndexingResults で進み、await 跨ぎで失効した結果の適用を弾く。260725Cl 追加:
    /// indexingBusy は Find/Calibrate ボタンしか無効化しないため、await 中の画像 D&amp;D や検出器幾何の変更で失効させたはずの
    /// 候補が、探索完了後に無条件で復活していた (誤データ表示。クラッシュはしない)</summary>
    private int indexingGeneration = 0;

    /// <summary>画像/幾何の変更で方位候補を失効させる。260724Cl 追加 (Codex 指摘: stale 結果の誤適用防止)</summary>
    //260724Cl シグネチャ変更: バンド検出廃止に伴い clearBands 引数を削除。旧: private void InvalidateIndexingResults(bool clearBands)
    private void InvalidateIndexingResults()
    {
        indexingGeneration++; // 260725Cl 追加: 実行中の探索結果を失効させる
        orientationCandidates = null;
        if (candidateGridInitialized)
        {
            skipCandidateSelectionEvent = true;
            try { dataGridViewEbsdCandidates.Rows.Clear(); }
            finally { skipCandidateSelectionEvent = false; }
        }
    }

    private bool TryBeginIndexing()
    {
        if (indexingBusy) return false;
        indexingBusy = true;
        buttonFindOrientation.Enabled = buttonCalibrateGeometry.Enabled = false; //260724Cl: 廃止 2 ボタンを除去
        return true;
    }

    private void EndIndexing()
    {
        indexingBusy = false;
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
        if (!TryBeginIndexing()) return;
        int generation = indexingGeneration; // 260725Cl 追加: await 中に画像・幾何が変わったら結果を捨てる
        toolStripStatusLabelSummary.Text = "Searching orientation candidates...";
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

            var sw = System.Diagnostics.Stopwatch.StartNew();
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
                        properSymmetries: properSyms);
                    //260725Cl (/simplify): 候補ごとの ScoreOrientation はカタログを毎回組み直していた → 一括版で 1 回に (スコアは同一)
                    var radonZ = EbsdRadonIndexer.ScoreOrientations(map, geom, reflections, [.. cands.Select(c => c.Rotation)], SatCap);
                    for (int i = 0; i < cands.Count; i++)
                        cands[i].Score = radonZ[i];
                }
                else
                    cands = EbsdRadonIndexer.Index(map, geom, reflections, wl, maxCandidates: 10, saturateCap: refineByZncc ? SatCap : 0);
                if (refineByZncc && cands.Count > 0)
                {
                    var projector = new EbsdPatternProjector(ctx.Geom, ctx.Rw, ctx.Rh);
                    var buf = new double[ctx.Rw * ctx.Rh];
                    var (refRobust, _, _) = EbsdPatternScorer.PrepareReferenceRobust(values, iw, ih, 160);
                    foreach (var c in cands) //全候補の robust ZNCC (未精密化 — 精密化はどの方位でも ZNCC を伸ばすため判別には使えない)
                    {
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
                        projector.Project(ctx.Mp, EbsdIndexer.PerturbRotation(top.Rotation, v[0], v[1], v[2]), ctx.Pos, ctx.Neg, buf);
                        return -EbsdPatternScorer.Zncc(refRobust, EbsdPatternScorer.RobustPreprocess(buf, ctx.Rw, ctx.Rh));
                    }
                    var (b2, v2, _) = EbsdPatternScorer.NelderMead(Score, [0, 0, 0], [0.25, 0.25, 0.25], 120);
                    var rFin = EbsdIndexer.PerturbRotation(top.Rotation, b2[0], b2[1], b2[2]);
                    //260725Cl (/simplify): ガードの 2 回採点も一括版へ (旧: ScoreOrientation ×2 でカタログを 2 回構築)
                    var guard = EbsdRadonIndexer.ScoreOrientations(map, geom, reflections, [rFin, top.Rotation], SatCap);
                    if (guard[0] >= guard[1] - 0.2)
                    { top.Rotation = rFin; top.Zncc = -v2; }
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
            });
            sw.Stop();

            //260725Cl 追加: 探索中に実測画像の差し替えや検出器幾何の変更があった場合、この結果は既に失効しているので適用しない
            if (generation != indexingGeneration)
            {
                toolStripStatusLabelSummary.Text = "Orientation search discarded (the image or geometry changed)";
                return;
            }
            orientationCandidates = candidates;
            FillCandidateGrid();
            //260724Cl: 使用モードを明示 (Codex 裁定)。旧: (refineByZncc ? " (ZNCC refined)" : "")
            toolStripStatusLabelSummary.Text = $"Orientation search: {candidates.Count} candidates" +
                (useDictionary ? " (Dictionary + ZNCC combo)" : refineByZncc ? " (Radon + ZNCC combo)" : " (Radon only)"); //260724Cl: Dictionary モード表示追加
            toolStripStatusLabelDetail.Text = $"{sw.Elapsed.TotalMilliseconds:f0} ms, {reflections.Length} reflections (d>{KikuchiDLimit * 10:0.#}A). Click a row to apply the orientation."; //260724Cl (/simplify): 表示値を定数から導出
        }
        catch (Exception ex)
        {
            toolStripStatusLabelSummary.Text = "Orientation search failed";
            toolStripStatusLabelDetail.Text = ex.Message;
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
                    g.Rows.Add(i, double.IsFinite(c.Score) ? $"{c.Score:f1}" : "-", $"{c.AssignedBands}/{c.TotalBands}",
                        double.IsNaN(c.Zncc) ? "-" : $"{c.Zncc:f3}", c.HklText); //260724Cl: AssignmentText (band:hkl) → HklText
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
        if (!TryBeginIndexing()) return;
        int generation = indexingGeneration; // 260725Cl 追加: await 中に実測画像・幾何が変わったら較正結果を書き戻さない (旧画像に合わせた幾何の誤適用防止)
        toolStripStatusLabelSummary.Text = "Calibrating detector geometry (PC/DD + orientation)...";
        try
        {
            var ctx = SnapshotMatchingContext();
            double detTilt = DetTilt, smpTilt = SmpTilt, xm = DetectorXMirror, pixelSize = ctx.Geom.PixelSize;
            int imgW = expPbmp.Width, imgH = expPbmp.Height;
            var (footU0, footV0) = ctx.Geom.PatternCenterMm; //260724Cl (/simplify): PC 式の手書き重複 (-DetX, -(DetY cosδ+DetZ sinδ)) を幾何オブジェクトへ一元化
            double dd0 = ctx.Geom.CameraLength;
            double physW = DetHalfWidth * 2, physH = DetHalfHeight * 2;
            if (dd0 < 1E-3) { toolStripStatusLabelSummary.Text = "Invalid camera length"; EndIndexing(); return; }

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var result = await Task.Run(() =>
            {
                var r0 = ctx.R0;
                double fu = footU0, fv = footV0, lnDd = Math.Log(dd0);
                var buf = new double[ctx.Rw * ctx.Rh];
                int evalTotal = 0;

                EbsdDetectorGeometry MakeGeom(double u, double v, double ld)
                {
                    var (dx, dy, dz) = EbsdDetectorGeometry.FromPatternCenter(u, v, Math.Exp(ld), detTilt);
                    return new EbsdDetectorGeometry(detTilt, dx, dy, dz, pixelSize, imgW, imgH, xm, smpTilt);
                }
                double ScoreWith(EbsdPatternProjector proj, Matrix3D rot)
                {
                    proj.Project(ctx.Mp, rot, ctx.Pos, ctx.Neg, buf);
                    return -EbsdPatternScorer.Zncc(ctx.Ref, buf);
                }
                double startZncc = -ScoreWith(new EbsdPatternProjector(MakeGeom(fu, fv, lnDd), ctx.Rw, ctx.Rh), r0);

                for (int round = 0; round < 2; round++)
                {
                    //① 幾何固定で方位 (粗 0.7°)
                    var projFixed = new EbsdPatternProjector(MakeGeom(fu, fv, lnDd), ctx.Rw, ctx.Rh);
                    var (bo, _, eo) = EbsdPatternScorer.NelderMead(v => ScoreWith(projFixed, EbsdIndexer.PerturbRotation(r0, v[0], v[1], v[2])), [0, 0, 0], [0.7, 0.7, 0.7], 150);
                    r0 = EbsdIndexer.PerturbRotation(r0, bo[0], bo[1], bo[2]); evalTotal += eo;

                    //② 方位固定で幾何 (dU, dV [mm], dlnDD)。ステップ = 検出器幅/高の 1%、lnDD 0.02
                    //260724Cl: 単一パターンの PC-DD-方位縮退で非物理領域へ流れないよう soft bounds (初期値から W/H の 25%・DD ±40% でペナルティ)
                    var rFixed = r0;
                    var (bg, _, eg) = EbsdPatternScorer.NelderMead(
                        v => (Math.Abs(v[0]) > physW * 0.25 || Math.Abs(v[1]) > physH * 0.25 || Math.Abs(v[2]) > 0.35)
                            ? 10 + Math.Abs(v[0]) / physW + Math.Abs(v[1]) / physH + Math.Abs(v[2])
                            : ScoreWith(new EbsdPatternProjector(MakeGeom(fu + v[0], fv + v[1], lnDd + v[2]), ctx.Rw, ctx.Rh), rFixed),
                        [0, 0, 0], [physW * 0.01, physH * 0.01, 0.02], 120);
                    fu += bg[0]; fv += bg[1]; lnDd += bg[2]; evalTotal += eg;
                }
                //仕上げの方位微調整 (0.2°)
                var projFinal = new EbsdPatternProjector(MakeGeom(fu, fv, lnDd), ctx.Rw, ctx.Rh);
                var (bf, vf, ef) = EbsdPatternScorer.NelderMead(v => ScoreWith(projFinal, EbsdIndexer.PerturbRotation(r0, v[0], v[1], v[2])), [0, 0, 0], [0.2, 0.2, 0.2], 100);
                r0 = EbsdIndexer.PerturbRotation(r0, bf[0], bf[1], bf[2]); evalTotal += ef;

                return (Rot: r0, Fu: fu, Fv: fv, Dd: Math.Exp(lnDd), Zncc: -vf, ZnccStart: startZncc, Evals: evalTotal);
            });
            sw.Stop();

            //260725Cl 追加: 較正中に実測画像の差し替えや幾何の変更があった場合、この結果は失効しているので書き戻さない
            if (generation != indexingGeneration)
            {
                toolStripStatusLabelSummary.Text = "Geometry calibration discarded (the image or geometry changed)";
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
            FormMain.SetRotation(result.Rot); //Draw は SetRotation → FormMain 経由で走る

            toolStripStatusLabelSummary.Text = $"Geometry calibrated: ZNCC {result.ZnccStart:f3} → {result.Zncc:f3}";
            toolStripStatusLabelDetail.Text = $"PC ({footU0:f2},{footV0:f2})→({result.Fu:f2},{result.Fv:f2}) mm, DD {dd0:f2}→{result.Dd:f2} mm, {result.Evals} evals, {sw.Elapsed.TotalMilliseconds:f0} ms. Tilt is kept fixed (single-pattern gauge).";
        }
        catch (Exception ex)
        {
            toolStripStatusLabelSummary.Text = "Geometry calibration failed";
            toolStripStatusLabelDetail.Text = ex.Message;
        }
        finally { EndIndexing(); }
    }

    #endregion
}
