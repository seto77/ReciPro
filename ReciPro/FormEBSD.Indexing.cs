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
/// コントロールは Overlays タブに仮置き (後で適切な場所へ移動予定)。設計正本 = .project-guidance/ReciPro_EBSD物理・幾何レビュー.md §7。
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

    /// <summary>画像/幾何の変更で方位候補を失効させる。260724Cl 追加 (Codex 指摘: stale 結果の誤適用防止)</summary>
    //260724Cl シグネチャ変更: バンド検出廃止に伴い clearBands 引数を削除。旧: private void InvalidateIndexingResults(bool clearBands)
    private void InvalidateIndexingResults()
    {
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
        if (!TryBeginIndexing()) return;
        toolStripStatusLabel2.Text = "Searching orientation candidates...";
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

            //動力学 MasterPattern が生成済みなら ZNCC 精密化を自動連結 (旧 Optimize orientation ボタン相当。260724Cl 作者指示)
            bool refineByZncc = MasterPattern != null;
            var ctx = refineByZncc ? SnapshotMatchingContext() : default;

            var sw = System.Diagnostics.Stopwatch.StartNew();
            var candidates = await Task.Run(() =>
            {
                var map = EbsdBandDetector.ComputeRadonMap(values, iw, ih);
                var cands = EbsdRadonIndexer.Index(map, geom, reflections, wl, maxCandidates: 10);
                if (refineByZncc && cands.Count > 0)
                {
                    var projector = new EbsdPatternProjector(ctx.Geom, ctx.Rw, ctx.Rh);
                    var buf = new double[ctx.Rw * ctx.Rh];
                    foreach (var c in cands.Take(5)) //ZNCC は上位 5 候補のみ (1 候補 ~250 評価)
                    {
                        double Score(double[] v)
                        {
                            projector.Project(ctx.Mp, PerturbRotation(c.Rotation, v[0], v[1], v[2]), ctx.Pos, ctx.Neg, buf);
                            return -EbsdPatternScorer.Zncc(ctx.Ref, buf);
                        }
                        var (b1, _, _) = EbsdPatternScorer.NelderMead(Score, [0, 0, 0], [1.0, 1.0, 1.0], 150);
                        var (b2, v2, _) = EbsdPatternScorer.NelderMead(Score, b1, [0.25, 0.25, 0.25], 100);
                        c.Rotation = PerturbRotation(c.Rotation, b2[0], b2[1], b2[2]);
                        c.Zncc = -v2;
                    }
                    cands = [.. cands.OrderByDescending(c => double.IsNaN(c.Zncc) ? double.MinValue : c.Zncc)];
                }
                return cands;
            });
            sw.Stop();

            orientationCandidates = candidates;
            FillCandidateGrid();
            toolStripStatusLabel2.Text = $"Orientation search: {candidates.Count} candidates" + (refineByZncc ? " (ZNCC refined)" : "");
            toolStripStatusLabel3.Text = $"{sw.Elapsed.TotalMilliseconds:f0} ms, {reflections.Length} reflections (d>{KikuchiDLimit * 10:0.#}A). Click a row to apply the orientation."; //260724Cl (/simplify): 表示値を定数から導出
        }
        catch (Exception ex)
        {
            toolStripStatusLabel2.Text = "Orientation search failed";
            toolStripStatusLabel3.Text = ex.Message;
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
        g.Columns.AddRange(
            new DataGridViewTextBoxColumn { HeaderText = "#", Width = 24 },
            new DataGridViewTextBoxColumn { HeaderText = "Score", Width = 46 },
            new DataGridViewTextBoxColumn { HeaderText = "Bands", Width = 44 },
            new DataGridViewTextBoxColumn { HeaderText = "ZNCC", Width = 48 },
            new DataGridViewTextBoxColumn { HeaderText = "Strong bands (hkl)", Width = 284 });
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
                    g.Rows.Add(i, $"{c.Score:f1}", $"{c.AssignedBands}/{c.TotalBands}",
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

    /// <summary>方位摂動: R(ω) = Rot(ω̂,|ω|)·R0 (試料系左摂動、単位 deg)。FormMain.Rotate の左乗算と同じ規約</summary>
    private static Matrix3D PerturbRotation(Matrix3D r0, double wxDeg, double wyDeg, double wzDeg)
    {
        double wx = wxDeg * Math.PI / 180, wy = wyDeg * Math.PI / 180, wz = wzDeg * Math.PI / 180;
        double len = Math.Sqrt(wx * wx + wy * wy + wz * wz);
        if (len < 1E-12) return r0;
        return Matrix3D.Rot((wx / len, wy / len, wz / len), len) * r0;
    }

    /// <summary>ZNCC 系操作に必要な状態を UI スレッド上でスナップショットする (ワーカーからコントロールを読まないため)。260724Cl 追加</summary>
    private (EbsdDetectorGeometry Geom, MasterPattern Mp, float[] Pos, float[] Neg, double[] Ref, int Rw, int Rh, Matrix3D R0) SnapshotMatchingContext()
    {
        var geom = BuildDetectorGeometry(expPbmp.Width, expPbmp.Height);
        var mp = MasterPattern;
        int eIdx = Math.Clamp(trackBarOutputEnergy.Value, 0, mp.Energies.Length - 1);
        int dIdx = Math.Clamp(trackBarOutputThickness.Value, 0, mp.Depths.Length - 1);
        var pos = mp.GetPlane(MasterPattern.Hemisphere.PositiveZ, eIdx, dIdx);
        var neg = mp.GetPlane(MasterPattern.Hemisphere.NegativeZ, eIdx, dIdx);
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
        toolStripStatusLabel2.Text = "Calibrating detector geometry (PC/DD + orientation)...";
        try
        {
            var ctx = SnapshotMatchingContext();
            double detTilt = DetTilt, smpTilt = SmpTilt, xm = DetectorXMirror, pixelSize = ctx.Geom.PixelSize;
            int imgW = expPbmp.Width, imgH = expPbmp.Height;
            var (footU0, footV0) = ctx.Geom.PatternCenterMm; //260724Cl (/simplify): PC 式の手書き重複 (-DetX, -(DetY cosδ+DetZ sinδ)) を幾何オブジェクトへ一元化
            double dd0 = ctx.Geom.CameraLength;
            double physW = DetHalfWidth * 2, physH = DetHalfHeight * 2;
            if (dd0 < 1E-3) { toolStripStatusLabel2.Text = "Invalid camera length"; EndIndexing(); return; }

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
                    var (bo, _, eo) = EbsdPatternScorer.NelderMead(v => ScoreWith(projFixed, PerturbRotation(r0, v[0], v[1], v[2])), [0, 0, 0], [0.7, 0.7, 0.7], 150);
                    r0 = PerturbRotation(r0, bo[0], bo[1], bo[2]); evalTotal += eo;

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
                var (bf, vf, ef) = EbsdPatternScorer.NelderMead(v => ScoreWith(projFinal, PerturbRotation(r0, v[0], v[1], v[2])), [0, 0, 0], [0.2, 0.2, 0.2], 100);
                r0 = PerturbRotation(r0, bf[0], bf[1], bf[2]); evalTotal += ef;

                return (Rot: r0, Fu: fu, Fv: fv, Dd: Math.Exp(lnDd), Zncc: -vf, ZnccStart: startZncc, Evals: evalTotal);
            });
            sw.Stop();

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

            toolStripStatusLabel2.Text = $"Geometry calibrated: ZNCC {result.ZnccStart:f3} → {result.Zncc:f3}";
            toolStripStatusLabel3.Text = $"PC ({footU0:f2},{footV0:f2})→({result.Fu:f2},{result.Fv:f2}) mm, DD {dd0:f2}→{result.Dd:f2} mm, {result.Evals} evals, {sw.Elapsed.TotalMilliseconds:f0} ms. Tilt is kept fixed (single-pattern gauge).";
        }
        catch (Exception ex)
        {
            toolStripStatusLabel2.Text = "Geometry calibration failed";
            toolStripStatusLabel3.Text = ex.Message;
        }
        finally { EndIndexing(); }
    }

    #endregion
}
