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
    /// <summary>(260502Cl) 一般点 ○ の実半径 (pixel)。RenderGeneralPositions で CircleRadiusFraction × cellSize から再計算され、本クラスのクラスタ化・split 線・ラベル配置などから参照される。フィールド初期値はセル算出前のフォールバック。</summary>
    private static float CircleRadius = 4.25f;

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

    /// <summary>(260503Cl) 立方晶系で 円縁・コンマ点・ラベル文字を結晶軸色 (a=赤, b=緑, c=青) で統一着色するための色定義と、対応する Brush / Pen。
    /// 立方晶以外では投影軸方向 (= projVariable) のラベルが暗黙化 (<c>+</c>, <c>½+</c> 等) され、円・ラベルは黒のまま (DrawClusters の isCubic 分岐で抑制)。</summary>
    private static readonly Color XVariableColor = Color.FromArgb(180, 0, 0);
    private static readonly Color YVariableColor = Color.FromArgb(0, 130, 0);
    private static readonly Color ZVariableColor = Color.FromArgb(0, 0, 180);
    private static readonly Brush XVariableBrush = new SolidBrush(XVariableColor);
    private static readonly Brush YVariableBrush = new SolidBrush(YVariableColor);
    private static readonly Brush ZVariableBrush = new SolidBrush(ZVariableColor);
    private static readonly Pen XVariablePen = new(XVariableColor, CirclePenWidth);
    private static readonly Pen YVariablePen = new(YVariableColor, CirclePenWidth);
    private static readonly Pen ZVariablePen = new(ZVariableColor, CirclePenWidth);
    #endregion

    /// <summary>(260503Cl 追加) 変数文字 x/y/z に対応する Brush を返す。z 以外の文字でない場合は z 用 (青) を fallback。</summary>
    private static Brush GetVariableBrush(char v) => v switch
    {
        'x' => XVariableBrush,
        'y' => YVariableBrush,
        _   => ZVariableBrush,
    };

    /// <summary>(260503Cl 追加) 変数文字 x/y/z に対応する Pen を返す。</summary>
    private static Pen GetVariablePen(char v) => v switch
    {
        'x' => XVariablePen,
        'y' => YVariablePen,
        _   => ZVariablePen,
    };

    /// <summary>(260503Cl 追加) ラベル末尾の変数文字 (x/y/z) で着色用 Brush を選ぶ。
    /// 変数文字を伴わない暗黙ラベル (<c>+</c>, <c>½+</c> 等) は defaultBrush を返す
    /// (立方晶では呼出側で projVariable の色を渡し、非立方晶では黒を渡すことで色制御)。</summary>
    private static Brush GetLabelBrush(string label, Brush defaultBrush)
    {
        if (label.Length == 0) return defaultBrush;
        return label[^1] switch
        {
            'x' => XVariableBrush,
            'y' => YVariableBrush,
            'z' => ZVariableBrush,
            _   => defaultBrush,
        };
    }

    /// <summary>(260502Cl) 結晶系で切り替える test 点。各結晶系で対称性確認に適した代表点。一般位置図でしか使わないため Common から本クラスへ移動。</summary>
    private static (double X, double Y, double Z) GetTestPoint(Symmetry sym) => sym.CrystalSystemNumber switch
    {
        2 => (0.06, 0.20, 0.14),       // monoclinic
        4 => (0.06, 0.20, 0.10),       // tetragonal
        5 or 6 => (0.22, 0.06, 0.10),  // trigonal / hexagonal
        7 => (0.05, 0.15, 0.22),  // cubic
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
            float scale = sym.CrystalSystemNumber == 7 ? CubicScale : 1f;
            CircleRadius = (float)(CircleRadiusFraction * cellSize) * scale;
            var labelFont = sym.CrystalSystemNumber == 7 ? CubicClusterLabelFont : ClusterLabelFont;
            var (tx, ty, tz) = GetTestPoint(sym);
            char projVariable = actualAxis switch
            {
                ProjectionAxis.A => 'x',
                ProjectionAxis.B => 'y',
                _                => 'z',
            };
            var allPoints = SymmetryStatic.WyckoffPositions[seriesNumber][0].GeneratePositions(tx, ty, tz);
            var placements = new List<Placement>();
            foreach (var p in allPoints)
                CollectPlacements(placements, layout, p, proj, tx, ty, tz);
            if (sym.CrystalSystemNumber == 7) DrawCubicTriangles(g, layout, proj, allPoints, tx, ty, tz);
            DrawClusters(g, placements, labelFont, scale, projVariable);
        }
        finally { g.Dispose(); }
        return bmp;
    }
    #endregion

    #region 等価点の収集
    /// <summary>1 等価点 (および境界 EdgeReplicate 以内の隣接ユニット複製) を canvas 座標に写像して収集。</summary>
    private static void CollectPlacements(List<Placement> placements, CellLayout c, Vector3D p, Projection proj,
                                          double testX, double testY, double testZ)
    {
        var (sx, sy, sz) = proj.ToScreen(p.X, p.Y, p.Z);
        bool mirrored = p.Operation.Order < 0;
        string label = ComputeDepthLabel(p.Operation, sz, proj, testX, testY, testZ);
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

    #region 立方晶系 [111] 3 回回転 orbit の三角形描画 (260503Cl 追加)
    /// <summary>(260503Cl 追加) 立方晶系で原点を通る [111] 3 回回転による初期三角形 T = {(x,y,z), (z,x,y), (y,z,x)}
    /// と、フル対称群の各元 g がそれを写した g·T = {g·P, g·(RP), g·(R²P)} を、薄い灰色の三角形として描画する。
    /// [111] 軸は全立方晶空間群で原点通過・Seitz 並進 0 のため、R(P)=(P.Z,P.X,P.Y)、R²(P)=(P.Y,P.Z,P.X) と座標の純粋な巡回置換になる。
    /// g·T は g·R·g⁻¹ という共役 3 回回転に関する orbit (一般に [111] 系統 4 軸のいずれか) であり、
    /// 旧実装の「点 P_i の [111] 3 回 orbit」とは異なる集合になる点に注意。三角形数は |G|/|H| = N/3 個。</summary>
    private static readonly Pen CubicTrianglePen = new(Color.FromArgb(190, 190, 190), 0.6f);

    private static void DrawCubicTriangles(Graphics g, CellLayout c, Projection proj, Vector3D[] allPoints,
                                           double testX, double testY, double testZ)
    {
        for (int i = 0; i < allPoints.Length; i++)
        {
            var pi = allPoints[i];
            var op = pi.Operation;
            var st = op.SeitzTranslation;
            var (m1x, m1y, m1z) = op.ApplyMatrix(testZ, testX, testY); // g_i · (R · test)
            var (m2x, m2y, m2z) = op.ApplyMatrix(testY, testZ, testX); // g_i · (R² · test)
            int j = FindCubicOrbitPartner(allPoints, m1x + st.U, m1y + st.V, m1z + st.W);
            int k = FindCubicOrbitPartner(allPoints, m2x + st.U, m2y + st.V, m2z + st.W);
            // 同じ三角形を 3 頂点分ヒットするため i 最小のときだけ描画 (dedupe)。
            if (j < 0 || k < 0 || i > j || i > k) continue;
            var (saX, saY, _) = proj.ToScreen(pi.X, pi.Y, pi.Z);
            var (sbX, sbY, _) = proj.ToScreen(allPoints[j].X, allPoints[j].Y, allPoints[j].Z);
            var (scX, scY, _) = proj.ToScreen(allPoints[k].X, allPoints[k].Y, allPoints[k].Z);
            g.DrawPolygon(CubicTrianglePen, [c.ToScreen(saX, saY), c.ToScreen(sbX, sbY), c.ToScreen(scX, scY)]);
        }
    }

    /// <summary>(260503Cl 追加) 一般位置リスト内で (x,y,z) と mod 1 でほぼ一致する点の index を返す (見つからなければ -1)。</summary>
    private static int FindCubicOrbitPartner(Vector3D[] list, double x, double y, double z)
    {
        const double eps = 1e-6;
        for (int i = 0; i < list.Length; i++)
        {
            var p = list[i];
            if (Math.Abs(CenterMod1(p.X - x)) < eps
                && Math.Abs(CenterMod1(p.Y - y)) < eps
                && Math.Abs(CenterMod1(p.Z - z)) < eps) return i;
        }
        return -1;
    }
    #endregion

    #region クラスタ化と描画
    /// <summary>ITC 規約で proper=○、improper=コンマ ○、混在=split circle として描画。proper を左上、improper を右上に積み上げ。</summary>
    private enum Corner { UR, LR, UL, LL }
    private readonly record struct ClusterInfo(float Cx, float Cy, List<string> Proper, List<string> Improper, bool Split, bool HasImproper);

    /// <summary>(260503Cl) クラスタごとに代表変数 (x/y/z) を決め、円縁・コンマ点・ラベル文字をその変数の結晶軸色で着色して描画する。</summary>
    private static void DrawClusters(Graphics g, List<Placement> placements, Font labelFont, float scale, char projVariable)
    {
        using var fill = new SolidBrush(Color.White);
        var sizes = new Dictionary<string, SizeF>();
        foreach (var p in placements)
            if (!sizes.ContainsKey(p.Label)) sizes[p.Label] = g.MeasureString(p.Label, labelFont);
        var infos = BuildClusters(placements);
        float dotR = CommaDotR * scale;
        var vars = infos.Select(info => ClusterVariable(info, projVariable)).ToArray();
        for (int i = 0; i < infos.Count; i++)
            DrawClusterCircle(g, infos[i], GetVariablePen(vars[i]), fill, GetVariableBrush(vars[i]), dotR);
        for (int i = 0; i < infos.Count; i++)
            DrawClusterLabels(g, infos, i, sizes, labelFont, GetVariableBrush(vars[i]));
    }

    /// <summary>(260503Cl 追加) クラスタの代表変数文字を決定する。Proper / Improper のラベル先頭から x/y/z で終わる最初のラベルの末尾文字を返す。
    /// 全ラベルが暗黙形式 (現状の ComputeDepthLabel では発生しない) の場合は投影軸変数 (projVariable) を返す。</summary>
    private static char ClusterVariable(ClusterInfo info, char projVariable)
    {
        foreach (var label in info.Proper)
            if (label.Length > 0 && label[^1] is 'x' or 'y' or 'z') return label[^1];
        foreach (var label in info.Improper)
            if (label.Length > 0 && label[^1] is 'x' or 'y' or 'z') return label[^1];
        return projVariable;
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

    private static void DrawClusterCircle(Graphics g, ClusterInfo info, Pen pen, Brush fill, Brush commaBrush, float dotR)
    {
        float cx = info.Cx, cy = info.Cy;
        g.FillEllipse(fill, cx - CircleRadius, cy - CircleRadius, 2 * CircleRadius, 2 * CircleRadius);
        g.DrawEllipse(pen, cx - CircleRadius, cy - CircleRadius, 2 * CircleRadius, 2 * CircleRadius);

        if (info.Split)
        {
            g.DrawLine(pen, cx, cy - CircleRadius, cx, cy + CircleRadius);
            g.FillEllipse(commaBrush, cx + CircleRadius * CommaSplitOffsetX - dotR, cy - dotR, 2 * dotR, 2 * dotR);
        }
        else if (info.HasImproper)
            g.FillEllipse(commaBrush, cx - dotR, cy - dotR, 2 * dotR, 2 * dotR);
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
            g.DrawString(labels[i], font, GetLabelBrush(labels[i], brush), x, y);
        }
    }
    #endregion

    #region depth ラベル
    /// <summary>(260503Cl) ITC 風の depth ラベル。投影軸方向の depth を <c>a·x + b·y + c·z + t</c>
    /// (a,b,c ∈ {-1,0,+1}) のアフィン式として抽出し、 <c>&lt;tFrac&gt;&lt;±&gt;&lt;variable&gt;</c> 形式に文字列化する。
    /// 例: <c>+z</c>, <c>−z</c>, <c>½+z</c>, <c>+y</c>, <c>−x</c>, <c>½−x</c>。
    /// 立方晶系では [111] 3 回回転で投影軸変数が x/y/z 間で入れ替わるため、変数文字を常に含める。</summary>
    private static string ComputeDepthLabel(SymmetryOperation op, double displayedSz, Projection proj,
                                            double testX, double testY, double testZ)
    {
        // R を単位ベクトル e_x/e_y/e_z に作用させ、投影軸 row (a, b, c) を取得。
        ReadOnlySpan<(double X, double Y, double Z)> e = [(1, 0, 0), (0, 1, 0), (0, 0, 1)];
        Span<double> coef = stackalloc double[3];
        for (int j = 0; j < 3; j++)
        {
            var (rx, ry, rz) = op.ApplyMatrix(e[j].X, e[j].Y, e[j].Z);
            coef[j] = proj.ToScreen(rx, ry, rz).Sz;
        }
        int idx = Math.Abs(coef[0]) > 0.5 ? 0 : Math.Abs(coef[1]) > 0.5 ? 1 : 2;
        char variable = "xyz"[idx];
        int sign = coef[idx] > 0 ? 1 : -1;

        // 並進 t = displayedSz - rTestSz (mod 1)。R·test の depth 成分との差が Seitz 並進に等しい。
        var (rTx, rTy, rTz) = op.ApplyMatrix(testX, testY, testZ);
        var (_, _, rTestSz) = proj.ToScreen(rTx, rTy, rTz);
        return TZToFraction(Mod1(displayedSz - rTestSz)) + (sign > 0 ? "+" : "−") + variable;
    }
    #endregion
}
