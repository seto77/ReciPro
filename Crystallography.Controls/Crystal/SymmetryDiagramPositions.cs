// 260501Cl: 一般位置 (右図) を ITC Vol.A 風に GDI+ 描画する子クラス。
// 等価点をクラスタ化し、ITC 規約 (proper=○、improper=コンマ ○、混在=split circle) で描画。高さラベルは ComputeDepthLabel で算出。
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using static Crystallography.SymmetryElementsTable;

namespace Crystallography.Controls;

public class SymmetryDiagramPositions : SymmetryDiagramCommon
{
    #region 定数
    // (260502Cl) 一般点 (右図) の描画寸法をクラス冒頭に集約。単位は全て pixel。

    /// <summary>(260502Cl) 一般点 ○ の半径をセル寸法 (a, b の短い方) に対する比率で指定。RenderGeneralPositions で実 pixel 値 (CircleRadius) に換算される。</summary>
    private const double CircleRadiusFraction = 0.0225;
    // private static float CircleRadius = 4.25f; // 旧: 描画ごとの半径を static 一時状態で共有していた。

    /// <summary>一般点 ○ の縁線および split 円の縦区切り線の線幅。</summary>
    private const float CirclePenWidth     = 1.2f;
    /// <summary>improper (鏡映で写された等価点) を示す内部コンマ点の半径 (基準値)。立方晶では CubicScale で縮小される。</summary>
    private const float CommaDotR          = 2.2f;
    /// <summary>Split 円 (proper と improper が同位置にある場合) で右半分内に置くコンマ点の中心 X オフセット (CircleRadius 比)。中心 0 = ○ 中心、+0.45 ≒ 右半円の中ほど。</summary>
    private const float CommaSplitOffsetX  = 0.45f;
    /// <summary>等価点を「同じ位置」とみなしてひとつのクラスタにまとめる距離しきい値 (CircleRadius 比)。0.6 ⇒ 中心間距離が ○ 半径の 60% 未満なら同クラスタ。</summary>
    private const float ClusterTolerance   = 0.6f;
    /// <summary>(260502Cl) 立方晶系は等価点が多く重なりやすいため、円・コンマ点・ラベルフォントを縮める。</summary>
    private const float CubicScale         = 0.8f;
    /// <summary>(260502Cl) 立方晶用の高さラベルフォント (ClusterLabelFont の CubicScale 倍)。</summary>
    private static readonly Font CubicClusterLabelFont = new("Times New Roman", 13f * CubicScale);

    // (260503Cl) 結晶軸 a/b/c の色 (VESTA / CrystalMaker 慣用: a=赤, b=緑, c=青)。
    //   index 0/1/2 が ProjectionAxis.A/B/C および 変数 x/y/z と一対一対応する点が肝で、
    //   ラベル末尾文字や投影軸 enum はすべて同じ index でこの 3 要素配列を引ける。
    //   立方晶以外では深さ方向の変数文字がラベル文字列に出ないため、円・ラベルは projVariable の色 (= 投影軸色) で塗られる。
    private static readonly Color[]  AxisColors  = [Color.FromArgb(180, 0, 0), Color.FromArgb(0, 130, 0), Color.FromArgb(0, 0, 180)];
    private static readonly Brush[]  AxisBrushes = [.. AxisColors.Select(c => (Brush)new SolidBrush(c))];
    private static readonly Pen[]    AxisPens    = [.. AxisColors.Select(c => new Pen(c, CirclePenWidth))];

    /// <summary>(260503Cl) 立方晶 [111] 3 回回転 orbit 三角形の薄灰色 Pen。</summary>
    private static readonly Pen CubicTrianglePen = new(Color.FromArgb(190, 190, 190), 0.6f);

    /// <summary>(260505Cl 整理) クラスタ円縁とラベル間の隙間 (px)。水平方向は文字列の左右に LabelGapH、垂直方向は kerning 補正で LabelGapV を加減算。</summary>
    private const float LabelGapH = 1f;
    private const float LabelGapV = 4f;
    #endregion

    /// <summary>(260503Cl) ラベル末尾文字 (x/y/z) を軸 index (0/1/2) に変換。
    /// 変数を伴わない暗黙ラベル (<c>+</c>, <c>½+</c> 等) や空文字列は -1。
    /// "xyz" の文字位置と AxisColors / AxisBrushes / AxisPens の index が完全一致しているのが要点で、
    /// 着色とクラスタ代表変数の決定が同じ 1 関数で済む。</summary>
    private static int LabelAxisIndex(string label) => label.Length > 0 ? "xyz".IndexOf(label[^1]) : -1;

    /// <summary>(260502Cl) 結晶系で切り替える test 点。各結晶系で対称性確認に適した代表点。一般位置図でしか使わないため Common から本クラスへ移動。
    /// (260506Cl) public 化: FormSymmetryInformation の numericBoxPosition* に既定値を流し込むため。</summary>
    public static (double X, double Y, double Z) GetTestPoint(Symmetry sym) => sym.CrystalSystemNumber switch
    {
        2 => (0.06, 0.20, 0.14),       // monoclinic
        4 => (0.06, 0.20, 0.10),       // tetragonal
        5 or 6 => (0.22, 0.06, 0.10),  // trigonal / hexagonal
        7 => (0.05, 0.15, 0.22),  // cubic
        _ => (0.05, 0.10, 0.20),
    };

    #region 公開 API
    /// <summary>新規 <see cref="Bitmap"/> を確保して一般位置図を描画して返す。
    /// (260506Cl) <paramref name="testPoint"/> を渡すと既定の <see cref="GetTestPoint"/> を上書きしてユーザー指定の一般位置で描画する。</summary>
    public static Bitmap RenderGeneralPositions(int seriesNumber, Size clientSize, ProjectionAxis axis = ProjectionAxis.C,
                                                (double X, double Y, double Z)? testPoint = null)
    {
        var bmp = NewBitmap(clientSize, out var g);
        try { DrawGeneralPositions(g, bmp.Size, seriesNumber, axis, testPoint); } // (260504Cl) NewBitmap が 16px 未満をクランプするので bmp.Size を渡す
        finally { g.Dispose(); }
        return bmp;
    }

    /// <summary>(260504Cl 追加) 与えられた <see cref="Graphics"/> 上に一般位置図を描画する。
    /// 呼び出し側で背景クリア・<see cref="Graphics.SmoothingMode"/> 等の初期化を行うこと。
    /// (260506Cl) <paramref name="testPoint"/> を渡すと既定の <see cref="GetTestPoint"/> を上書きする。</summary>
    public static void DrawGeneralPositions(Graphics g, Size clientSize, int seriesNumber, ProjectionAxis axis = ProjectionAxis.C,
                                            (double X, double Y, double Z)? testPoint = null)
    {
        if (!TryGetSym(seriesNumber, out var sym, out seriesNumber, out var msg))
        {
            if (msg != null) DrawCenteredText(g, clientSize, msg, Color.Gray);
            return;
        }
        var actualAxis = ResolveProjectionAxis(sym, axis);
        var proj = GetProjection(actualAxis);
        // 260505Cl: 立方晶 F 格子は upper-left 1/4 領域だけ描画。
        bool halfQuadrant = IsCubicFLattice(sym);
        double displayMaxS = halfQuadrant ? 0.5 : 1.0; // (260505Ch) clip ではなく描画対象座標そのものを制限する。
        var layout = ComputeCellLayout(clientSize, sym, actualAxis, halfQuadrant);
        if (halfQuadrant) DrawUpperLeftQuadrantLabel(g);
        DrawCellAndAxes(g, layout, proj, sym, halfQuadrant);
        // 立方晶系 (= 7) は等価点が密集しがちなので円・コンマ点・フォントを CubicScale 倍に縮小し、
        // [111] 軸 orbit の三角形も追加で描く。それ以外の結晶系は scale=1 で従来通り。
        bool isCubic = sym.CrystalSystemNumber == 7;
        double cellSize = Math.Min(
            Math.Sqrt(layout.Horz.X * layout.Horz.X + layout.Horz.Y * layout.Horz.Y),
            Math.Sqrt(layout.Vert.X * layout.Vert.X + layout.Vert.Y * layout.Vert.Y));
        float scale = isCubic ? CubicScale : 1f;
        float circleRadius = (float)(CircleRadiusFraction * cellSize) * scale; // (260505Ch) 描画ごとの値として渡し、static 状態を持たない。
        var labelFont = isCubic ? CubicClusterLabelFont : ClusterLabelFont;
        var (tx, ty, tz) = testPoint ?? GetTestPoint(sym); // 260506Cl: ユーザー指定があればそれを使用
        // ProjectionAxis enum の値 (A=0, B=1, C=2) と AxisBrushes/Pens の index、変数 "xyz" の文字位置はすべて一致。
        int projAxisIdx = (int)actualAxis;
        var allPoints = SymmetryStatic.WyckoffPositions[seriesNumber][0].GeneratePositions(tx, ty, tz);
        var placements = new List<Placement>();
        foreach (var p in allPoints)
            CollectPlacements(placements, layout, p, proj, tx, ty, tz, displayMaxS);
        // 旧: quadrantClip による後段の表示範囲制限は廃止。
        if (isCubic) DrawCubicTriangles(g, layout, proj, allPoints, tx, ty, tz, displayMaxS);
        DrawClusters(g, placements, labelFont, scale, projAxisIdx, circleRadius);
    }
    #endregion

    #region 等価点の収集
    /// <summary>1 等価点 (および境界 EdgeReplicate 以内の隣接ユニット複製) を canvas 座標に写像して収集。</summary>
    private static void CollectPlacements(List<Placement> placements, CellLayout c, Vector3D p, Projection proj,
                                          double testX, double testY, double testZ, double displayMaxS = 1.0)
    {
        var (sx, sy, sz) = proj.ToScreen(p.X, p.Y, p.Z);
        bool mirrored = p.Operation.Order < 0;
        string label = ComputeDepthLabel(p.Operation, sz, proj, testX, testY, testZ);
        foreach (var (x, y) in EdgeReplicatedPoints(sx, sy, displayMaxS))
        {
            var pt = c.ToScreen(x, y);
            placements.Add(new(pt.X, pt.Y, mirrored, label));
        }
    }

    private readonly record struct Placement(float Px, float Py, bool Mirrored, string Label);
    #endregion

    #region 立方晶系 [111] 3 回回転 orbit の三角形描画 (260503Cl 追加)
    /// <summary>(260503Cl 追加) 立方晶系で原点を通る [111] 3 回回転による初期三角形 T = {(x,y,z), (z,x,y), (y,z,x)}
    /// と、フル対称群の各元 g がそれを写した g·T = {g·P, g·(RP), g·(R²P)} を、薄い灰色の三角形として描画する。
    /// [111] 軸は全立方晶空間群で原点通過・Seitz 並進 0 のため、R(P)=(P.Z,P.X,P.Y)、R²(P)=(P.Y,P.Z,P.X) と座標の純粋な巡回置換になる。
    /// g·T は g·R·g⁻¹ という共役 3 回回転に関する orbit (一般に [111] 系統 4 軸のいずれか) であり、
    /// 旧実装の「点 P_i の [111] 3 回 orbit」とは異なる集合になる点に注意。三角形数は |G|/|H| = N/3 個。</summary>
    private static void DrawCubicTriangles(Graphics g, CellLayout c, Projection proj, Vector3D[] allPoints,
                                           double testX, double testY, double testZ, double displayMaxS = 1.0)
    {
        for (int i = 0; i < allPoints.Length; i++)
        {
            // g_i (= pi.Operation) を初期三角形 T = {test, R·test, R²·test} の各頂点に作用させ、
            // 一致する allPoints の index を引く (中心化並進は op.SeitzTranslation に含まれる)。
            var op = allPoints[i].Operation;
            var t = op.SeitzTranslation;
            int j = FindOrbitPartner(allPoints, op.ApplyMatrix(testZ, testX, testY), t); // g_i · (R · test) = g_i applied to (z, x, y)
            int k = FindOrbitPartner(allPoints, op.ApplyMatrix(testY, testZ, testX), t); // g_i · (R² · test)
            // 同じ三角形を 3 頂点分 (i, j, k のどれを起点にしても) 走査するため、i 最小のときだけ描画して重複を排除。
            if (j < 0 || k < 0 || i > j || i > k) continue;
            DrawTriangle(allPoints[i], allPoints[j], allPoints[k]);
        }

        void DrawTriangle(Vector3D p0, Vector3D p1, Vector3D p2)
        {
            var a = ProjectFraction(p0);
            var b = ProjectFraction(p1);
            var c0 = ProjectFraction(p2);
            if (displayMaxS >= 1.0 - 1e-9)
            {
                g.DrawPolygon(CubicTrianglePen, [c.ToScreen(a.Sx, a.Sy), c.ToScreen(b.Sx, b.Sy), c.ToScreen(c0.Sx, c0.Sy)]);
                return;
            }
            DrawClippedEdge(a, b);
            DrawClippedEdge(b, c0);
            DrawClippedEdge(c0, a);
        }

        (double Sx, double Sy) ProjectFraction(Vector3D p)
        {
            var (sx, sy, _) = proj.ToScreen(p.X, p.Y, p.Z);
            return (sx, sy);
        }

        // 三角形辺は [0,1] 区間の有限線分なので、Common の無限直線クリップ結果を [0,1] と交差させる。
        void DrawClippedEdge((double Sx, double Sy) p0, (double Sx, double Sy) p1)
        {
            double dx = p1.Sx - p0.Sx, dy = p1.Sy - p0.Sy;
            if (!TryClipLineThroughUnitCell(p0.Sx, p0.Sy, dx, dy, out double tMin, out double tMax, displayMaxS)) return;
            double t0 = Math.Max(0, tMin), t1 = Math.Min(1, tMax);
            if (t1 < t0) return;
            var a = c.ToScreen(p0.Sx + t0 * dx, p0.Sy + t0 * dy);
            var b = c.ToScreen(p0.Sx + t1 * dx, p0.Sy + t1 * dy);
            g.DrawLine(CubicTrianglePen, a, b); // (260505Ch) F 格子は三角形辺も [0,1/2]² の範囲だけを直接描く。
        }
    }

    /// <summary>(260503Cl) 等価点リストの中で、ターゲット (matrix · test + translation) と mod 1 で一致する点の index を返す (見つからなければ -1)。
    /// 立方晶系空間群では中心化並進が混ざるので、座標は単位胞 mod 1 で比較する必要がある。</summary>
    private static int FindOrbitPartner(Vector3D[] list, (double X, double Y, double Z) m, (double U, double V, double W) t)
    {
        const double eps = 1e-6;
        double tx = m.X + t.U, ty = m.Y + t.V, tz = m.Z + t.W;
        for (int i = 0; i < list.Length; i++)
        {
            var p = list[i];
            if (Math.Abs(CenterMod1(p.X - tx)) < eps
             && Math.Abs(CenterMod1(p.Y - ty)) < eps
             && Math.Abs(CenterMod1(p.Z - tz)) < eps) return i;
        }
        return -1;
    }
    #endregion

    #region クラスタ化と描画
    /// <summary>ITC 規約で proper=○、improper=コンマ ○、混在=split circle として描画。proper を左上、improper を右上に積み上げ。</summary>
    private enum Corner { UR, LR, UL, LL }

    /// <summary>クラスタ = 同じ投影位置に集まる Placement 群。Proper/Improper は一意ラベル列を昇順に並べたもの。
    /// Split (proper と improper が同位置で並ぶ) と HasImproper はラベル数から派生するので別フィールドで持たない。</summary>
    private readonly record struct ClusterInfo(float Cx, float Cy, List<string> Proper, List<string> Improper)
    {
        public bool Split       => Proper.Count > 0 && Improper.Count > 0;
        public bool HasImproper => Improper.Count > 0;
    }

    private static readonly Corner[] LabelCorners = [Corner.UR, Corner.LR, Corner.UL, Corner.LL];
    private static readonly (Corner L, Corner R)[] SplitLabelCornerPairs =
        [(Corner.UL, Corner.UR), (Corner.LL, Corner.LR), (Corner.UL, Corner.LR), (Corner.LL, Corner.UR)];

    /// <summary>(260503Cl) クラスタごとに代表軸 (a/b/c = 0/1/2) を決め、円縁・コンマ点・ラベル文字をその軸の結晶軸色で着色して描画する。
    /// クラスタ内の proper / improper の各ラベル末尾 (x/y/z) から軸 index を抽出し、最初に見つかったものを採用。
    /// 全ラベルが暗黙形式 (=変数文字なし) なら投影軸 index (= projAxisIdx) を fallback に使う。</summary>
    private static void DrawClusters(Graphics g, List<Placement> placements, Font labelFont, float scale, int projAxisIdx,
                                     float circleRadius)
    {
        using var fill = new SolidBrush(Color.White);
        var sizes = new Dictionary<string, SizeF>();
        foreach (var p in placements)
            // 旧: if (!sizes.ContainsKey(p.Label)) sizes[p.Label] = g.MeasureString(p.Label, labelFont);
            if (!sizes.ContainsKey(p.Label)) sizes[p.Label] = MeasureTightString(g, p.Label, labelFont); // 260510Ch: Elements と同じ tight glyph bounds。
        var infos = BuildClusters(placements, circleRadius);
        float dotR = CommaDotR * scale;
        var axes = new int[infos.Count];
        for (int i = 0; i < infos.Count; i++)
            axes[i] = ClusterAxisIndex(infos[i], projAxisIdx);
        for (int i = 0; i < infos.Count; i++)
            DrawClusterCircle(g, infos[i], AxisPens[axes[i]], fill, AxisBrushes[axes[i]], dotR, circleRadius);
        for (int i = 0; i < infos.Count; i++)
            DrawClusterLabels(g, infos, i, sizes, labelFont, AxisBrushes[axes[i]], circleRadius);
    }

    private static int ClusterAxisIndex(ClusterInfo info, int fallback)
    {
        foreach (var label in info.Proper) { int idx = LabelAxisIndex(label); if (idx >= 0) return idx; }
        foreach (var label in info.Improper) { int idx = LabelAxisIndex(label); if (idx >= 0) return idx; }
        return fallback;
    }

    /// <summary>近接する Placement (画面座標 ≤ tol) を 1 つの cluster にまとめる。
    /// 走査順は前から後ろへ greedy で、未割当の点を seed にしてその近傍を吸収。tol = CircleRadius × ClusterTolerance。</summary>
    private static List<ClusterInfo> BuildClusters(List<Placement> placements, float circleRadius)
    {
        float tol = circleRadius * ClusterTolerance;
        var assigned = new bool[placements.Count];
        var infos = new List<ClusterInfo>();
        for (int s = 0; s < placements.Count; s++)
        {
            if (assigned[s]) continue;
            var seed = placements[s];
            var cluster = new List<Placement> { seed };
            assigned[s] = true;
            for (int i = s + 1; i < placements.Count; i++)
                if (!assigned[i] && Math.Abs(placements[i].Px - seed.Px) < tol && Math.Abs(placements[i].Py - seed.Py) < tol)
                {
                    cluster.Add(placements[i]);
                    assigned[i] = true;
                }
            float cx = cluster.Average(m => m.Px), cy = cluster.Average(m => m.Py);
            infos.Add(new(cx, cy, LabelsBy(cluster, mirrored: false), LabelsBy(cluster, mirrored: true)));
        }
        return infos;

        static List<string> LabelsBy(List<Placement> cluster, bool mirrored)
            => cluster.Where(m => m.Mirrored == mirrored).Select(m => m.Label).Distinct().OrderBy(l => l).ToList();
    }

    private static void DrawClusterCircle(Graphics g, ClusterInfo info, Pen pen, Brush fill, Brush commaBrush, float dotR,
                                          float circleRadius)
    {
        float cx = info.Cx, cy = info.Cy;
        g.FillEllipse(fill, cx - circleRadius, cy - circleRadius, 2 * circleRadius, 2 * circleRadius);
        g.DrawEllipse(pen, cx - circleRadius, cy - circleRadius, 2 * circleRadius, 2 * circleRadius);

        if (info.Split)
        {
            g.DrawLine(pen, cx, cy - circleRadius, cx, cy + circleRadius);
            g.FillEllipse(commaBrush, cx + circleRadius * CommaSplitOffsetX - dotR, cy - dotR, 2 * dotR, 2 * dotR);
        }
        else if (info.HasImproper)
            g.FillEllipse(commaBrush, cx - dotR, cy - dotR, 2 * dotR, 2 * dotR);
    }

    /// <summary>クラスタのラベルを近隣の円との重なりが最小の隅に描く。Split は左右固定で上下選択、単独は 4 隅自由。</summary>
    private static void DrawClusterLabels(Graphics g, List<ClusterInfo> infos, int selfIdx,
                                          Dictionary<string, SizeF> sizes, Font font, Brush brush, float circleRadius)
    {
        var info = infos[selfIdx];
        if (info.Split)
        {
            int best = int.MaxValue;
            (Corner L, Corner R) bestPair = SplitLabelCornerPairs[0];
            foreach (var pair in SplitLabelCornerPairs)
            {
                int o = CountOverlaps(infos, selfIdx, info.Proper, pair.L, sizes, circleRadius)
                      + CountOverlaps(infos, selfIdx, info.Improper, pair.R, sizes, circleRadius);
                if (o < best) { best = o; bestPair = pair; if (o == 0) break; }
            }
            StackLabelsAt(g, font, brush, info.Proper, bestPair.L, info.Cx, info.Cy, sizes, circleRadius);
            StackLabelsAt(g, font, brush, info.Improper, bestPair.R, info.Cx, info.Cy, sizes, circleRadius);
        }
        else
        {
            var labels = info.HasImproper ? info.Improper : info.Proper;
            if (labels.Count == 0) return;
            int best = int.MaxValue;
            Corner bestCorner = LabelCorners[0];
            foreach (var c in LabelCorners)
            {
                int o = CountOverlaps(infos, selfIdx, labels, c, sizes, circleRadius);
                if (o < best) { best = o; bestCorner = c; if (o == 0) break; }
            }
            StackLabelsAt(g, font, brush, labels, bestCorner, info.Cx, info.Cy, sizes, circleRadius);
        }
    }

    /// <summary>指定 corner にラベル列を置いた矩形と他クラスタの円との重なり数を返す。</summary>
    private static int CountOverlaps(List<ClusterInfo> infos, int selfIdx, List<string> labels, Corner corner,
                                     Dictionary<string, SizeF> sizes, float circleRadius)
    {
        if (labels.Count == 0) return 0;
        var self = infos[selfIdx];
        bool isUpper = corner is Corner.UR or Corner.UL, isLeft = corner is Corner.UL or Corner.LL;
        float w = labels.Max(l => sizes[l].Width), h = labels.Sum(l => sizes[l].Height);
        float rectL = isLeft ? self.Cx - w - LabelGapH : self.Cx + LabelGapH;
        float rectT = isUpper ? self.Cy - circleRadius - h + LabelGapV : self.Cy + circleRadius - LabelGapV;
        float rectR = rectL + w, rectB = rectT + h;
        int count = 0;
        for (int j = 0; j < infos.Count; j++)
        {
            if (j == selfIdx) continue;
            float dx = Math.Max(rectL, Math.Min(infos[j].Cx, rectR)) - infos[j].Cx;
            float dy = Math.Max(rectT, Math.Min(infos[j].Cy, rectB)) - infos[j].Cy;
            if (dx * dx + dy * dy < circleRadius * circleRadius) count++;
        }
        return count;
    }

    private static void StackLabelsAt(Graphics g, Font font, Brush brush, List<string> labels,
                                      Corner corner, float cx, float cy, Dictionary<string, SizeF> sizes,
                                      float circleRadius)
    {
        if (labels.Count == 0) return;
        bool isUpper = corner is Corner.UR or Corner.UL, isLeft = corner is Corner.UL or Corner.LL;
        for (int i = 0; i < labels.Count; i++)
        {
            var sz = sizes[labels[i]];
            float x = isLeft ? cx - sz.Width - LabelGapH : cx + LabelGapH;
            float y = isUpper ? cy - circleRadius - (i + 1) * sz.Height + LabelGapV : cy + circleRadius + i * sz.Height - LabelGapV;
            int idx = LabelAxisIndex(labels[i]);
            // g.DrawString(labels[i], font, idx >= 0 ? AxisBrushes[idx] : brush, x, y); // 旧: DrawString の余白を含む配置。
            DrawTightString(g, idx >= 0 ? AxisBrushes[idx] : brush, labels[i], font, x, y); // 260510Ch
        }
    }
    #endregion

    #region depth ラベル
    /// <summary>(260503Cl) ITC 風の depth ラベル。投影軸方向の depth を <c>a·x + b·y + c·z + t</c>
    /// (a,b,c ∈ {-1,0,+1}) のアフィン式として抽出し、 <c>&lt;tFrac&gt;&lt;±&gt;&lt;variable&gt;</c> 形式に文字列化する。
    /// 例: <c>+z</c>, <c>−z</c>, <c>½+z</c>, <c>+y</c>, <c>−x</c>, <c>½−x</c>。
    /// 立方晶系では [111] 3 回回転で投影軸変数が x/y/z 間で入れ替わるため、変数文字を常に含める。
    /// (260504Cl) 立方晶系以外では depth 変数は常に投影軸変数 (例: C 投影なら z) 固定なので末尾変数文字を省略。</summary>
    private static string ComputeDepthLabel(SymmetryOperation op, double displayedSz, Projection proj,
                                            double testX, double testY, double testZ)
    {
        // 投影軸 row の係数 (a, b, c) は、R · e_x / R · e_y / R · e_z をそれぞれ投影 depth に流したもの。
        // 立方晶では R が permutation 行列なので、3 値のうち 1 つだけが ±1、残りは 0 になる。
        double a = ProbeDepth(1, 0, 0), b = ProbeDepth(0, 1, 0), c = ProbeDepth(0, 0, 1);
        int idx = Math.Abs(a) > 0.5 ? 0 : Math.Abs(b) > 0.5 ? 1 : 2;
        double coef = idx == 0 ? a : idx == 1 ? b : c;

        // 並進 t = displayedSz − R(test) の depth 成分 (mod 1)。アフィン式から線形部分を引けば残りが Seitz 並進。
        var (rTx, rTy, rTz) = op.ApplyMatrix(testX, testY, testZ);
        double rTestSz = proj.ToScreen(rTx, rTy, rTz).Sz;
        bool isCubic = SymmetryStatic.Symmetries[op.SeriesNumber].CrystalSystemNumber == 7;
        string varStr = isCubic ? "xyz"[idx].ToString() : "";
        return TZToFraction(Mod1(displayedSz - rTestSz)) + (coef > 0 ? "+" : "−") + varStr;

        double ProbeDepth(double ex, double ey, double ez)
        {
            var (rx, ry, rz) = op.ApplyMatrix(ex, ey, ez);
            return proj.ToScreen(rx, ry, rz).Sz;
        }
    }
    #endregion
}
