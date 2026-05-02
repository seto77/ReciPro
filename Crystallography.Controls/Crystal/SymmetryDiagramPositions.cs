// 260501Cl: 一般位置 (右図) を ITC Vol.A 風に GDI+ 描画する子クラス。
// 等価点をクラスタ化し、ITC 規約 (proper=○、improper=コンマ ○、混在=split circle) で描画。高さラベルは ComputeDepthLabel で算出。
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Crystallography.Controls;

public class SymmetryDiagramPositions : SymmetryDiagramCommon
{
    #region 定数
    // (260502Cl) 一般点 (右図) の描画寸法をクラス冒頭に集約。単位は全て pixel。

    /// <summary>(260502Cl) 一般点 ○ の半径をセル寸法 (a, b の短い方) に対する比率で指定。RenderGeneralPositions で実 pixel 値 (CircleRadius) に換算される。</summary>
    private const double CircleRadiusFraction = 0.0225;
    /// <summary>(260502Cl) 一般点 ○ の実半径 (pixel)。RenderGeneralPositions で CircleRadiusFraction × cellSize から再計算され、本クラスのクラスタ化・split 線・ラベル配置などから参照される。フィールド初期値はセル算出前のフォールバック。</summary>
    private static float CircleRadius = 4.25f;

    /// <summary>一般点 ○ の縁線および split 円の縦区切り線の線幅。</summary>
    private const float CirclePenWidth     = 1.2f;
    /// <summary>improper (鏡映で写された等価点) を示す内部コンマ点の半径。○ の中心 (HasImproper のとき) または右半円の中ほど (Split のとき) に黒丸として描画。</summary>
    private const float CommaDotR          = 2.2f;
    /// <summary>Split 円 (proper と improper が同位置にある場合) で右半分内に置くコンマ点の中心 X オフセット (CircleRadius 比)。中心 0 = ○ 中心、+0.45 ≒ 右半円の中ほど。</summary>
    private const float CommaSplitOffsetX  = 0.45f;
    /// <summary>等価点を「同じ位置」とみなしてひとつのクラスタにまとめる距離しきい値 (CircleRadius 比)。0.6 ⇒ 中心間距離が ○ 半径の 60% 未満なら同クラスタ。</summary>
    private const float ClusterTolerance   = 0.6f;
    #endregion

    /// <summary>(260502Cl) 結晶系で切り替える test 点。各結晶系で対称性確認に適した代表点。一般位置図でしか使わないため Common から本クラスへ移動。</summary>
    private static (double X, double Y, double Z) GetTestPoint(Symmetry sym) => sym.CrystalSystemNumber switch
    {
        2 => (0.06, 0.20, 0.14),       // monoclinic
        4 => (0.06, 0.20, 0.10),       // tetragonal
        5 or 6 => (0.22, 0.06, 0.10),  // trigonal / hexagonal
        _ => (0.05, 0.10, 0.20),
    };

    #region 公開 API
    /// <summary>右側の一般位置図を描画。</summary>
    public static Bitmap RenderGeneralPositions(int seriesNumber, Size clientSize, ProjectionAxis axis = ProjectionAxis.C)
    {
        var bmp = NewBitmap(clientSize, out var g);
        try
        {
            if (!TryGetSym(seriesNumber, out var sym, out seriesNumber, out var msg))
            {
                if (msg != null) DrawCenteredText(g, bmp.Size, msg, Color.Gray);
                return bmp;
            }
            var actualAxis = ResolveProjectionAxis(sym, axis);
            var proj = GetProjection(actualAxis);
            var layout = ComputeCellLayout(bmp.Size, sym, actualAxis);
            DrawCellAndAxes(g, layout, proj, sym);
            double cellSize = Math.Min(
                Math.Sqrt(layout.Horz.X * layout.Horz.X + layout.Horz.Y * layout.Horz.Y),
                Math.Sqrt(layout.Vert.X * layout.Vert.X + layout.Vert.Y * layout.Vert.Y));
            CircleRadius = (float)(CircleRadiusFraction * cellSize);
            var (tx, ty, tz) = GetTestPoint(sym);
            var (_, _, testSz) = proj.ToScreen(tx, ty, tz);
            var placements = new List<Placement>();
            foreach (var p in SymmetryStatic.WyckoffPositions[seriesNumber][0].GeneratePositions(tx, ty, tz))
                CollectPlacements(placements, layout, p, proj, tx, ty, tz, testSz);
            DrawClusters(g, placements);
        }
        finally { g.Dispose(); }
        return bmp;
    }
    #endregion

    #region 等価点の収集
    /// <summary>1 等価点 (および境界 EdgeReplicate 以内の隣接ユニット複製) を canvas 座標に写像して収集。</summary>
    private static void CollectPlacements(List<Placement> placements, CellLayout c, Vector3D p, Projection proj,
                                          double testX, double testY, double testZ, double testSz)
    {
        var (sx, sy, sz) = proj.ToScreen(p.X, p.Y, p.Z);
        bool mirrored = p.Operation.Order < 0;
        string label = ComputeDepthLabel(p.Operation, sz, proj, testX, testY, testZ, testSz);
        bool nearEdge = Math.Min(sx, 1 - sx) < EdgeReplicate || Math.Min(sy, 1 - sy) < EdgeReplicate;
        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
            {
                if ((dx != 0 || dy != 0) && !nearEdge) continue;
                double X = sx + dx, Y = sy + dy;
                if (X < -EdgeReplicate || X > 1 + EdgeReplicate || Y < -EdgeReplicate || Y > 1 + EdgeReplicate) continue;
                var pt = c.ToScreen(X, Y);
                placements.Add(new(pt.X, pt.Y, mirrored, label));
            }
    }

    private readonly record struct Placement(float Px, float Py, bool Mirrored, string Label);
    #endregion

    #region クラスタ化と描画
    /// <summary>ITC 規約で proper=○、improper=コンマ ○、混在=split circle として描画。proper を左上、improper を右上に積み上げ。</summary>
    private enum Corner { UR, LR, UL, LL }
    private readonly record struct ClusterInfo(float Cx, float Cy, List<string> Proper, List<string> Improper, bool Split, bool HasImproper);

    private static void DrawClusters(Graphics g, List<Placement> placements)
    {
        using var pen = new Pen(Color.Black, CirclePenWidth);
        using var fill = new SolidBrush(Color.White);
        using var black = new SolidBrush(Color.Black);
        // (260502Cl) Common.ClusterLabelFont を共有使用。
        var sizes = new Dictionary<string, SizeF>();
        foreach (var p in placements)
            if (!sizes.ContainsKey(p.Label)) sizes[p.Label] = g.MeasureString(p.Label, ClusterLabelFont);
        var infos = BuildClusters(placements);
        foreach (var info in infos) DrawClusterCircle(g, info, pen, fill, black);
        for (int i = 0; i < infos.Count; i++) DrawClusterLabels(g, infos, i, sizes, ClusterLabelFont, black);
    }

    private static List<ClusterInfo> BuildClusters(List<Placement> placements)
    {
        float tol = CircleRadius * ClusterTolerance;
        var rest = new List<Placement>(placements);
        var infos = new List<ClusterInfo>();
        while (rest.Count > 0)
        {
            var seed = rest[0];
            var cluster = new List<Placement> { seed };
            rest.RemoveAt(0);
            for (int i = rest.Count - 1; i >= 0; i--)
                if (Math.Abs(rest[i].Px - seed.Px) < tol && Math.Abs(rest[i].Py - seed.Py) < tol)
                {
                    cluster.Add(rest[i]); rest.RemoveAt(i);
                }
            float cx = cluster.Average(m => m.Px), cy = cluster.Average(m => m.Py);
            var prop = cluster.Where(m => !m.Mirrored).Select(m => m.Label).Distinct().OrderBy(l => l).ToList();
            var impr = cluster.Where(m => m.Mirrored).Select(m => m.Label).Distinct().OrderBy(l => l).ToList();
            infos.Add(new(cx, cy, prop, impr, prop.Count > 0 && impr.Count > 0, impr.Count > 0));
        }
        return infos;
    }

    private static void DrawClusterCircle(Graphics g, ClusterInfo info, Pen pen, Brush fill, Brush black)
    {
        float cx = info.Cx, cy = info.Cy;
        g.FillEllipse(fill, cx - CircleRadius, cy - CircleRadius, 2 * CircleRadius, 2 * CircleRadius);
        g.DrawEllipse(pen, cx - CircleRadius, cy - CircleRadius, 2 * CircleRadius, 2 * CircleRadius);

        const float dotR = CommaDotR; // (260502Cl) クラス冒頭の定数を使用
        if (info.Split)
        {
            g.DrawLine(pen, cx, cy - CircleRadius, cx, cy + CircleRadius);
            g.FillEllipse(black, cx + CircleRadius * CommaSplitOffsetX - dotR, cy - dotR, 2 * dotR, 2 * dotR);
        }
        else if (info.HasImproper)
            g.FillEllipse(black, cx - dotR, cy - dotR, 2 * dotR, 2 * dotR);
    }

    /// <summary>クラスタのラベルを近隣の円との重なりが最小の隅に描く。Split は左右固定で上下選択、単独は 4 隅自由。</summary>
    private static void DrawClusterLabels(Graphics g, List<ClusterInfo> infos, int selfIdx,
                                          Dictionary<string, SizeF> sizes, Font font, Brush brush)
    {
        var info = infos[selfIdx];
        if (info.Split)
        {
            (Corner L, Corner R)[] pairs = [(Corner.UL, Corner.UR), (Corner.LL, Corner.LR), (Corner.UL, Corner.LR), (Corner.LL, Corner.UR)];
            int best = int.MaxValue; (Corner L, Corner R) bestPair = pairs[0];
            foreach (var pair in pairs)
            {
                int o = CountOverlaps(infos, selfIdx, info.Proper, pair.L, sizes) + CountOverlaps(infos, selfIdx, info.Improper, pair.R, sizes);
                if (o < best) { best = o; bestPair = pair; if (o == 0) break; }
            }
            StackLabelsAt(g, font, brush, info.Proper, bestPair.L, info.Cx, info.Cy, sizes);
            StackLabelsAt(g, font, brush, info.Improper, bestPair.R, info.Cx, info.Cy, sizes);
        }
        else
        {
            var labels = info.HasImproper ? info.Improper : info.Proper;
            if (labels.Count == 0) return;
            Corner[] candidates = [Corner.UR, Corner.LR, Corner.UL, Corner.LL];
            int best = int.MaxValue; Corner bestCorner = candidates[0];
            foreach (var c in candidates)
            {
                int o = CountOverlaps(infos, selfIdx, labels, c, sizes);
                if (o < best) { best = o; bestCorner = c; if (o == 0) break; }
            }
            StackLabelsAt(g, font, brush, labels, bestCorner, info.Cx, info.Cy, sizes);
        }
    }

    /// <summary>指定 corner にラベル列を置いた矩形と他クラスタの円との重なり数を返す。</summary>
    private static int CountOverlaps(List<ClusterInfo> infos, int selfIdx, List<string> labels, Corner corner, Dictionary<string, SizeF> sizes)
    {
        if (labels.Count == 0) return 0;
        var self = infos[selfIdx];
        bool isUpper = corner is Corner.UR or Corner.UL, isLeft = corner is Corner.UL or Corner.LL;
        float w = labels.Max(l => sizes[l].Width), h = labels.Sum(l => sizes[l].Height);
        float rectL = isLeft ? self.Cx - w - 1 : self.Cx + 1;
        float rectT = isUpper ? self.Cy - CircleRadius - h + 4 : self.Cy + CircleRadius - 4;
        float rectR = rectL + w, rectB = rectT + h;
        int count = 0;
        for (int j = 0; j < infos.Count; j++)
        {
            if (j == selfIdx) continue;
            float dx = Math.Max(rectL, Math.Min(infos[j].Cx, rectR)) - infos[j].Cx;
            float dy = Math.Max(rectT, Math.Min(infos[j].Cy, rectB)) - infos[j].Cy;
            if (dx * dx + dy * dy < CircleRadius * CircleRadius) count++;
        }
        return count;
    }

    private static void StackLabelsAt(Graphics g, Font font, Brush brush, List<string> labels,
                                      Corner corner, float cx, float cy, Dictionary<string, SizeF> sizes)
    {
        if (labels.Count == 0) return;
        bool isUpper = corner is Corner.UR or Corner.UL, isLeft = corner is Corner.UL or Corner.LL;
        for (int i = 0; i < labels.Count; i++)
        {
            var sz = sizes[labels[i]];
            float x = isLeft ? cx - sz.Width - 1 : cx + 1;
            float y = isUpper ? cy - CircleRadius - (i + 1) * sz.Height + 4 : cy + CircleRadius + i * sz.Height - 4;
            g.DrawString(labels[i], font, brush, x, y);
        }
    }
    #endregion

    #region depth ラベル
    /// <summary>ITC 風の depth ラベル ("+", "−", "½+", "¼+" 等)。表示 sz と R·test の depth 差から Seitz 並進を取り出して prefix。</summary>
    private static string ComputeDepthLabel(SymmetryOperation op, double displayedSz, Projection proj,
                                            double testX, double testY, double testZ, double testSz)
    {
        var (rTx, rTy, rTz) = op.ApplyMatrix(testX, testY, testZ);
        var (_, _, rTestSz) = proj.ToScreen(rTx, rTy, rTz);
        bool depthPositive = Math.Abs(rTestSz - testSz) < Math.Abs(rTestSz + testSz);
        return TZToFraction(Mod1(displayedSz - rTestSz)) + (depthPositive ? "+" : "−");
    }
    #endregion
}
