// 260501Cl: 対称要素 (左図) を ITC Vol.A 風に GDI+ 描画。
// 反転中心 / 紙面垂直 2(2_1) 軸 / 紙面内 2(2_1) 軸 / 紙面垂直 mirror/glide / 紙面平行 mirror をサポート。
// 260502Cl: 対称要素列挙は Crystallography.SymmetryElementsTable に集約。本ファイルは描画専任。
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using static Crystallography.SymmetryElementsTable;

namespace Crystallography.Controls;

public class SymmetryDiagramElements : SymmetryDiagramCommon
{
    #region 定数 (単位は全て pixel。ITC Vol.A 慣用)
    // 線幅
    private const float DefaultPenWidth       = 1.2f;  // セル枠線・反転中心 ○
    private const float MirrorPenWidth        = 1.8f;  // 紙面垂直な mirror/glide の線幅
    private const float OutlinePenWidth       = 1.2f;  // 多角形輪郭の線幅・screw fin
    private const float ScrewFinPenWidth      = 1.2f;  // 2_1 lens 上下の弧
    private const float InPlaneAxisPenWidth   = 1.4f;
    private const float CornerBracketPenWidth = 1.6f;
    private const float SymbolHaloPenWidth    = 4.8f;  // 線記号上に置く点記号の白縁取り

    // 紙面垂直 2(2_1) — vesica piscis lens
    private const float TwofoldHalfH     = 8f;
    private const float TwofoldHalfW     = 4f;
    private const float ScrewFinSweepDeg = 30f;

    // 紙面垂直 3/4/6 (-3, -4, -6) — 正多角形
    private const float ThreeFoldRadius         = 5.625f;
    private const float FourFoldRadius          = 7.2f;
    private const float SixFoldRadius           = 6.0f;
    private const float ScrewFinTailLen         = 5f;
    private const float MinusThreeCenterDotR    = 2f;
    private const float MinusFourInnerLensScale = 0.8f;

    // 立方晶 [111] 系 体対角 3 回軸 (foot 黒丸 / shaft1 / triangle / 白丸 / shaft2)。
    private const float DiagThreefoldShaft1Len = 24f;    // foot 黒丸 → 重心 (= 三角中心) までの距離
    private const float DiagThreefoldShaft2Len = 12f;    // 重心 → 反対側 tail までの距離
    private const float DiagThreefoldTriLeg    = 16.5f;  // 三角の脚長
    private const float DiagThreefoldHaloWidth = 3.6f;   // shaft の白縁取り太さ (= 直径)
    private const float DiagThreefoldFootRatio = 1.25f;  // foot 黒丸の半径 / 白丸の半径

    // 反転中心
    private const float InversionR = 2.5f;

    // 紙面内 2(2_1) 矢印
    private const float InPlaneArrowExt    = 32f;
    private const float ArrowHeadLen       = 7f;
    private const float ArrowHeadHalfWidth = 3f;
    private const float DGlideDotR         = 1.7f;
    private const float DGlidePatternPitch = 64f;
    private const float EGlideDotDashUnit  = 2.6f;

    // 紙面平行 mirror corner bracket
    private const float CornerBracketArmLen = 22f;
    private const float CornerBracketGap    = 45f;
    private const float CornerBracketStep   = 8f;
    private const float GlideArrowLineShorten = 5f;
    #endregion

    #region context
    /// <summary>1 描画中だけ共有する Pen / Brush / dedup state。</summary>
    private sealed class ElementsContext
    {
        public Graphics G;
        public CellLayout C;
        public Projection Proj;
        public Pen Pen, MirrorPen, InPlanePen, DepthPen, DiagPen, EPen;
        public Brush Fill, White;
        public List<PerpendicularMirrorDraft> PerpendicularMirrors;
        public HashSet<(long Nx, long Ny, long D, int Style)> DrawnMirrorPlanes;
    }

    private readonly record struct PerpendicularMirrorDraft(double Sx, double Sy,
                                                            (int U, int V, int W) Direction,
                                                            (double U, double V, double W) Glide);

    private readonly record struct ParallelMirrorSymbol(double Height,
                                                        double GlideSx, double GlideSy,
                                                        double GlideSx2, double GlideSy2,
                                                        bool NGlide, bool DGlide, int DiamondScore);

    #endregion

    #region 公開 API
    public static Bitmap RenderSymmetryElements(int seriesNumber, Size clientSize, ProjectionAxis axis = ProjectionAxis.C)
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

            var table = SymmetryElementsTable.Get(seriesNumber);
            if (table == null) return bmp;

            using var pen        = new Pen(Color.Black, DefaultPenWidth);
            using var mirrorPen  = new Pen(Color.Black, MirrorPenWidth);
            using var inPlanePen = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [5f, 3f] };
            using var depthPen   = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [1f, 2.5f] };
            using var diagPen    = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [5f, 2.5f, 1f, 2.5f] };
            using var ePen       = new Pen(Color.Black, MirrorPenWidth)
            {
                DashStyle = DashStyle.Custom,
                DashCap = DashCap.Round,
                DashPattern = [0.1f, EGlideDotDashUnit, 0.1f, EGlideDotDashUnit, 5.0f, EGlideDotDashUnit]
            };
            using var fill  = new SolidBrush(Color.Black);
            using var white = new SolidBrush(Color.White);

            var ctx = new ElementsContext
            {
                G = g, C = layout, Proj = proj,
                Pen = pen, MirrorPen = mirrorPen, InPlanePen = inPlanePen,
                DepthPen = depthPen, DiagPen = diagPen, EPen = ePen,
                Fill = fill, White = white,
                PerpendicularMirrors = [],
                DrawnMirrorPlanes = [],
            };

            // 紙面平行 mirror 集約 / 紙面垂直 mirror draft 集約 / 紙面内 2 軸 draft 集約。
            var parallelMirrors = new HashSet<(double Height, bool Glide, double GlideSx, double GlideSy)>();
            var inPlaneAxisDrafts = new Dictionary<(long, long, long, long, bool), InPlaneAxisArrowDraft>();
            foreach (var mp in table.MirrorPlanes)
            {
                bool perp = IsAxisPerpendicularToProjection(mp.Normal, actualAxis);
                bool inPlane = IsAxisInPlane(mp.Normal, actualAxis);
                if (perp)
                {
                    var (_, _, sz) = proj.ToScreen(mp.X, mp.Y, mp.Z);
                    var (gSx, gSy) = ProjectVector(mp.Glide.U, mp.Glide.V, mp.Glide.W, actualAxis);
                    bool hasGlide = Math.Abs(mp.Glide.U) + Math.Abs(mp.Glide.V) + Math.Abs(mp.Glide.W) > 1e-6;
                    parallelMirrors.Add((Mod1(sz), hasGlide, gSx, gSy));
                }
                else if (inPlane)
                {
                    var (sx, sy, _) = proj.ToScreen(mp.X, mp.Y, mp.Z);
                    ctx.PerpendicularMirrors.Add(new(sx, sy, mp.Normal, mp.Glide));
                }
            }
            foreach (var ax in table.RotationAxes)
            {
                if (Math.Abs(ax.Order) != 2 || ax.Order < 0) continue;
                if (!IsAxisInPlane(ax.Direction, actualAxis)) continue;
                var (sx, sy, sz) = proj.ToScreen(ax.X, ax.Y, ax.Z);
                CollectInPlaneAxisArrows(layout, sx, sy, Mod1(sz), ax.Direction, actualAxis, ax.Screw, inPlaneAxisDrafts);
            }

            DrawCollectedPerpendicularMirrorPlanes(ctx);
            DrawParallelMirrorStack(g, layout, parallelMirrors, fill);
            DrawCollectedInPlaneAxisArrows(g, fill, inPlaneAxisDrafts);
            // 260502Cl 追加: 立方晶 [111] 系 体対角 3 回軸の描画。
            // 同位置で垂直回転軸 (lens 等) と重なる場合は垂直軸を上に出すため、こちらを先に描く。
            DrawDiagonalRotationMarks(ctx, table, actualAxis);
            DrawPerpendicularRotationMarks(ctx, table, actualAxis);
            DrawInversions(ctx, table.InversionCenters);
        }
        finally { g.Dispose(); }
        return bmp;
    }
    #endregion

    #region 紙面垂直 点記号
    /// <summary>軸方向が投影軸に平行な軸を点記号として描画。低次は高次に隠され、-N と同位置の +N があれば -N を捨て、-N(z≠0) は +N_k に置換。</summary>
    private static void DrawPerpendicularRotationMarks(ElementsContext ctx, Crystallography.SymmetryElementsTable table,
                                                       ProjectionAxis projAxis)
    {
        var axes = table.RotationAxes;
        // 同位置の高次 proper rotation 集合 (低次抑制 / -N 抑制用)。
        var covered2 = new HashSet<(int, int)>();
        var covered3 = new HashSet<(int, int)>();
        var properRotations = new HashSet<(int N, int Sx, int Sy)>();
        foreach (var ax in axes)
        {
            if (ax.Order <= 0 || !IsAxisPerpendicularToProjection(ax.Direction, projAxis)) continue;
            int absO = ax.Order;
            var (sx, sy, _) = ctx.Proj.ToScreen(ax.X, ax.Y, ax.Z);
            var key = ((int)Math.Round(Mod1(sx) * 10000), (int)Math.Round(Mod1(sy) * 10000));
            if (absO is 3 or 4 or 6) covered2.Add(key); // (260503Ch) [ITA-D1] 同位置の高次記号が defining symbol になる場合、低次 2 回記号は描かない。
            if (absO == 6) covered3.Add(key); // (260503Ch) [ITA-D1] 6 回記号が defining symbol になる場合、同位置の 3 回記号は描かない。
            if (!ax.Screw && absO is (2 or 3 or 4 or 6)) properRotations.Add((absO, key.Item1, key.Item2)); // (260503Ch) [ITA-D1] 同位置に proper N があれば -N は別途重ねない。
        }

        foreach (var ax in axes)
        {
            int o = ax.Order, absO = Math.Abs(o);
            if (absO is not (2 or 3 or 4 or 6)) continue;
            if (!IsAxisPerpendicularToProjection(ax.Direction, projAxis)) continue;
            var (sx, sy, sz) = ctx.Proj.ToScreen(ax.X, ax.Y, ax.Z);
            var key = ((int)Math.Round(Mod1(sx) * 10000), (int)Math.Round(Mod1(sy) * 10000));
            if (absO == 2 && covered2.Contains(key)) continue; // (260503Ch) [ITA-D1] 高次点記号が同じ位置を定義するため、2 回点記号を省く。
            if (absO == 3 && o > 0 && covered3.Contains(key)) continue; // (260503Ch) [ITA-D1] 6 回点記号が同じ位置を定義するため、3 回点記号を省く。
            if (o < 0 && properRotations.Contains((absO, key.Item1, key.Item2))) continue; // (260503Ch) [ITA-D1] proper N と同位置の -N は、反転中心側の記号と重複させない。

            // -N (N=3,4,6) で inversion 点 z_c ≠ 0 のときは N_k 螺旋 + inversion(z=0) と等価 (反転中心は別途描画)。
            int order = o;
            int finCount = ax.FinCount, edgeStep = ax.EdgeStep;
            if (o < 0 && absO is (3 or 4 or 6))
            {
                double zc = Mod1(sz);
                if (Math.Abs(zc) > 1e-3 && Math.Abs(zc - 1) > 1e-3)
                {
                    int kk = ((int)Math.Round(Mod1(2 * zc) * absO)) % absO;
                    if (kk != 0)
                    {
                        order = absO;
                        (finCount, edgeStep) = SymmetryElementsTable.PinwheelFins(absO, kk);
                    }
                }
            }

            foreach (var (dxf, dyf) in EdgeReplicatedPoints(sx, sy))
            {
                var pt = ctx.C.ToScreen(dxf, dyf);
                if (absO == 2) DrawTwofoldPerp(ctx.G, ctx.Fill, pt, ax.Screw);
                else if (absO == 3) DrawRotationPerp(ctx.G, ctx.Fill, ctx.White, pt, order, finCount, edgeStep, 3, ThreeFoldRadius);
                else if (absO == 4) DrawRotationPerp(ctx.G, ctx.Fill, ctx.White, pt, order, finCount, edgeStep, 4, FourFoldRadius);
                else if (absO == 6) DrawRotationPerp(ctx.G, ctx.Fill, ctx.White, pt, order, finCount, edgeStep, 6, SixFoldRadius);
            }
        }
    }

    #endregion

    #region 反転中心
    /// <summary>反転中心を白丸 (黒縁) で描画、z!=0 で高さラベルを併記。
    /// (260502Cl) 描画パスの最後に呼ぶので、白塗りで下層の点記号を punch out して見える化する。
    /// 同一 2D 位置に複数の反転中心が射影される場合、最小高さのみ採用 (ITC 慣用)。</summary>
    private static void DrawInversions(ElementsContext ctx, InversionCenter[] centers)
    {
        if (centers.Length == 0) return;
        var byKey = new Dictionary<(int, int), (double sxF, double syF, double minZ)>();
        foreach (var c in centers)
        {
            var (sx, sy, sz) = ctx.Proj.ToScreen(c.X, c.Y, c.Z);
            foreach (var (sxF, syF) in EdgeReplicatedPoints(sx, sy))
            {
                var key = ((int)Math.Round(sxF * 10000), (int)Math.Round(syF * 10000));
                double mz = Mod1(sz);
                if (!byKey.TryGetValue(key, out var cur) || mz < cur.minZ) byKey[key] = (sxF, syF, mz); // (260503Ch) [ITA-D2] 同一投影位置では代表高さ h だけを表示する。
            }
        }
        foreach (var v in byKey.Values)
        {
            var pt = ctx.C.ToScreen(v.sxF, v.syF);
            ctx.G.FillEllipse(ctx.White, pt.X - InversionR, pt.Y - InversionR, 2 * InversionR, 2 * InversionR);
            ctx.G.DrawEllipse(ctx.Pen, pt.X - InversionR, pt.Y - InversionR, 2 * InversionR, 2 * InversionR);
            string h = HeightLabel(v.minZ);
            if (h == null) continue;
            ctx.G.DrawString(h, HeightLabelFont, ctx.Fill,
                pt.X + InversionR + 1, pt.Y - InversionR - ctx.G.MeasureString(h, HeightLabelFont).Height + 2);
        }
    }
    #endregion

    #region 軸方向の分類
    private static bool IsAxisPerpendicularToProjection((int U, int V, int W) d, ProjectionAxis a) => a switch
    {
        ProjectionAxis.C => d is (0, 0, not 0),
        ProjectionAxis.A => d is (not 0, 0, 0),
        _ => d is (0, not 0, 0),
    };

    private static bool IsAxisInPlane((int U, int V, int W) d, ProjectionAxis a) => a switch
    {
        ProjectionAxis.C => d.W == 0 && (d.U != 0 || d.V != 0),
        ProjectionAxis.A => d.U == 0 && (d.V != 0 || d.W != 0),
        _ => d.V == 0 && (d.U != 0 || d.W != 0),
    };

    // 260502Cl 追加: 立方晶系 [111] 系の体対角 3 回軸など、紙面に対し斜め (depth と in-plane の両方に成分を持つ) な軸の判定。
    private static bool IsAxisDiagonalToProjection((int U, int V, int W) d, ProjectionAxis a)
        => !IsAxisPerpendicularToProjection(d, a) && !IsAxisInPlane(d, a);
    #endregion

    #region 紙面垂直 2(2_1) 軸 lens / 紙面垂直 3/4/6 多角形
    /// <summary>紙面垂直 2 (2_1) 軸: vesica piscis lens を塗り潰し。screw=互い違い円弧。-4 から呼ぶ際は scale で縮小。</summary>
    private static void DrawTwofoldPerp(Graphics g, Brush fill, PointF pt, bool screw, float scale = 1f, float rotationDeg = 0f)
    {
        var state = g.Save(); // (260502Ch) 斜め 2 回軸では lens を投影軸方向に応じて回転させる。
        try
        {
            if (Math.Abs(rotationDeg) > 1e-3f)
            {
                g.TranslateTransform(pt.X, pt.Y);
                g.RotateTransform(rotationDeg);
                g.TranslateTransform(-pt.X, -pt.Y);
            }

            float halfW = TwofoldHalfW * scale, halfH = TwofoldHalfH * scale;
            float r = (halfW * halfW + halfH * halfH) / (2 * halfW), d = r - halfW;
            float halfAngle = (float)(Math.Atan2(halfH, d) * 180.0 / Math.PI);
            var rightRect = new RectangleF(pt.X + d - r, pt.Y - r, 2 * r, 2 * r);
            var leftRect  = new RectangleF(pt.X - d - r, pt.Y - r, 2 * r, 2 * r);
            using var path = new GraphicsPath();
            path.AddArc(rightRect, 180f + halfAngle, -2 * halfAngle);
            path.AddArc(leftRect, halfAngle, -2 * halfAngle);
            path.CloseFigure();
            if (!screw)
            {
                // 純 2 回軸: レンズに白ハローを巻いて下地と分離する。
                using var halo = new Pen(Color.White, SymbolHaloPenWidth) { LineJoin = LineJoin.Round };
                g.DrawPath(halo, path);
                g.FillPath(fill, path);
                return;
            }
            // (260503Cl) 2_1 螺旋: レンズ自身のハローを止めて、フィンをレンズ側に食い込ませた上でフィンにハローを巻く。
            //   こうすると レンズ角での AA 境界 1px すき間 (= フィン途切れ) も解消し、
            //   フィン基部がレンズ body に確実に接続する。描画順は: フィンハロー → レンズ塗り → フィン本体。
            //   レンズ塗りで overlap 部のフィンハローを上書きするので、フィンハローはレンズ外側だけに残る。
            const float finOverlapDeg = 6f;
            float rightFinStart = 180f + halfAngle - finOverlapDeg;
            float leftFinStart  = halfAngle - finOverlapDeg;
            float finSweep      = ScrewFinSweepDeg + finOverlapDeg;
            using (var finHalo = new Pen(Color.White, SymbolHaloPenWidth) { LineJoin = LineJoin.Round })
            {
                g.DrawArc(finHalo, rightRect, rightFinStart, finSweep);
                g.DrawArc(finHalo, leftRect, leftFinStart, finSweep);
            }
            g.FillPath(fill, path);
            using var finPen = new Pen(Color.Black, ScrewFinPenWidth);
            g.DrawArc(finPen, rightRect, rightFinStart, finSweep);
            g.DrawArc(finPen, leftRect, leftFinStart, finSweep);
        }
        finally
        {
            g.Restore(state);
        }
    }

    /// <summary>中心 c から半径 r の正 N 角形 (頂点 0 を真上)。</summary>
    private static PointF[] RegularPolygon(PointF c, int N, float r)
    {
        var poly = new PointF[N];
        for (int i = 0; i < N; i++)
        {
            double th = -Math.PI / 2 + i * 2 * Math.PI / N;
            poly[i] = new PointF(c.X + (float)(r * Math.Cos(th)), c.Y + (float)(r * Math.Sin(th)));
        }
        return poly;
    }

    /// <summary>n_k 螺旋の指示棒。頂点 (j*placeStep)%N から edge (i−edgeStep)→i 方向に延長。</summary>
    private static void DrawScrewFins(Graphics g, Pen pen, PointF[] poly, int finCount, int edgeStep, float tailLen)
    {
        if (finCount == 0) return;
        int N = poly.Length, placeStep = N / finCount;
        for (int j = 0; j < finCount; j++)
        {
            int i = (j * placeStep) % N;
            int prev = (i - edgeStep + N * edgeStep) % N;
            float ex = poly[i].X - poly[prev].X, ey = poly[i].Y - poly[prev].Y;
            float k = tailLen / (float)Math.Sqrt(ex * ex + ey * ey);
            g.DrawLine(pen, poly[i].X, poly[i].Y, poly[i].X + ex * k, poly[i].Y + ey * k);
        }
    }

    /// <summary>紙面垂直 3/4/6 回 + 反転 (-N) 共通。-3=黒+中心白丸、-4=白+lens、-6=白+内接三角形。</summary>
    private static void DrawRotationPerp(Graphics g, Brush fill, Brush white, PointF pt, int order, int finCount, int edgeStep, int N, float radius)
    {
        var poly = RegularPolygon(pt, N, radius);
        using var halo = new Pen(Color.White, SymbolHaloPenWidth) { LineJoin = LineJoin.Round };
        using var outline = new Pen(Color.Black, OutlinePenWidth) { LineJoin = LineJoin.Round };
        g.DrawPolygon(halo, poly);
        if (order > 0)
        {
            g.FillPolygon(fill, poly);
            g.DrawPolygon(outline, poly);
            DrawScrewFins(g, outline, poly, finCount, edgeStep, ScrewFinTailLen);
            return;
        }
        if (N == 3)
        {
            g.FillPolygon(fill, poly);
            g.DrawPolygon(outline, poly);
            g.FillEllipse(white, pt.X - MinusThreeCenterDotR, pt.Y - MinusThreeCenterDotR, 2 * MinusThreeCenterDotR, 2 * MinusThreeCenterDotR);
            return;
        }
        g.FillPolygon(white, poly);
        if (N == 4) DrawTwofoldPerp(g, fill, pt, screw: false, scale: MinusFourInnerLensScale);
        else g.FillPolygon(fill, RegularPolygon(pt, 3, radius));
        g.DrawPolygon(outline, poly);
    }

    /// <summary>紙面に対し斜め (例: 立方晶 [111], [101]) の 2/3 回回転軸 (proper / screw) を描画。-N 等は未対応。
    /// foot 位置は axis の depth=0 平面との交点に取る (SymmetryElementsTable 格納 position は軸線上の任意点なので)。</summary>
    private static void DrawDiagonalRotationMarks(ElementsContext ctx, Crystallography.SymmetryElementsTable table,
                                                  ProjectionAxis projAxis)
    {
        var axes = table.RotationAxes;
        // (260503Cl) ITA は同じ foot から複数の [111] 系統方向の軸が伸びる図を描く (例: I23 (1/3,1/3) で 3_1 + 3_2)。
        //   方向と (FinCount, EdgeStep) を含めて key とすることで、同じ foot で別方向 / 別 k の軸を別々に描き残す。
        //   centered 展開で同一 (Direction, Position, IT) の重複だけは弾く。
        var drawnAxes = new HashSet<(long Sx, long Sy, int Order, int U, int V, int W, bool Screw, int Fin, int Edge)>();
        foreach (var ax in axes)
        {
            if (ax.Order is not (2 or 3)) continue; // (260502Ch) P432 系の斜め 2 / 2_1 軸も 3 回軸と同じ anchor/shaft で描く。
            if (!IsAxisDiagonalToProjection(ax.Direction, projAxis)) continue;

            int finCount = ax.FinCount, edgeStep = ax.EdgeStep;

            // axis の (U, V, W) を depth 成分が正になるよう符号反転、紙面 (Sx, Sy) 成分を実 pixel 方向に変換して正規化。
            var (u, v, w) = ax.Direction;
            if (ProjectedDepth(u, v, w, projAxis) < 0)
                (u, v, w) = (-u, -v, -w);
            var (dSx, dSy) = ProjectVector(u, v, w, projAxis);
            float dirX = (float)(dSx * ctx.C.Horz.X + dSy * ctx.C.Vert.X);
            float dirY = (float)(dSx * ctx.C.Horz.Y + dSy * ctx.C.Vert.Y);
            float dlen = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            if (dlen < 1e-3f) continue;
            dirX /= dlen; dirY /= dlen;

            // 軸 (X+t·U, Y+t·V, Z+t·W) を depth=0 にする t を求め、その時の (X, Y, Z) を投影位置とする。
            if (!TryGetDiagonalAxisFootprint(ax, projAxis, out double sx, out double sy)) continue;
            if (!drawnAxes.Add((R6(sx), R6(sy), ax.Order, u, v, w, ax.Screw, finCount, edgeStep))) continue;

            foreach (var (dxf, dyf) in EdgeReplicatedPoints(sx, sy))
            {
                // DrawDiagonalThreefoldPerp(ctx.G, ctx.Fill, ctx.C.ToScreen(dxf, dyf), dirX, dirY, ax.FinCount, ax.EdgeStep);
                var anchor = ctx.C.ToScreen(dxf, dyf);
                if (ax.Order == 2) DrawDiagonalTwofoldPerp(ctx.G, ctx.Fill, anchor, dirX, dirY, ax.Screw); // (260502Ch)
                else DrawDiagonalThreefoldPerp(ctx.G, ctx.Fill, anchor, dirX, dirY, finCount, edgeStep);
            }
        }
    }

    private static bool TryGetDiagonalAxisFootprint(RotationAxis ax, ProjectionAxis projAxis, out double sx, out double sy)
    {
        sx = sy = 0;
        double depthPos = ProjectedDepth(ax.X, ax.Y, ax.Z, projAxis);
        double depthDir = ProjectedDepth(ax.Direction.U, ax.Direction.V, ax.Direction.W, projAxis);
        if (Math.Abs(depthDir) < 1e-9) return false;
        double t = -depthPos / depthDir;
        double x0 = ax.X + t * ax.Direction.U;
        double y0 = ax.Y + t * ax.Direction.V;
        double z0 = ax.Z + t * ax.Direction.W;
        var (px, py, _) = GetProjection(projAxis).ToScreen(x0, y0, z0);
        sx = Mod1(px); sy = Mod1(py);
        return true;
    }

    private static bool IsFraction(double actual, double expected)
    {
        double d = Math.Abs(Mod1(actual - expected));
        return d < FracEps || Math.Abs(d - 1) < FracEps;
    }

    /// <summary>立方晶 [111] 系 体対角 3 回回転軸を ITC Vol.A 風に描画。foot 黒丸 → shaft1 → 三角 (重心に白丸) → shaft2 (dir 方向, 端 round) の構造。
    /// 三角は dir と CCW 90° 方向の 2 脚から成る直角三角形を画面 CCW 45° 回転、重心を shaft1 の端点に一致させる。
    /// finCount/edgeStep が非零なら 3_k 螺旋として三角の各頂点に fin (DrawScrewFins と同形式) を生やす。</summary>
    private static void DrawDiagonalThreefoldPerp(Graphics g, Brush fill, PointF anchor, float dirX, float dirY,
                                                  int finCount = 0, int edgeStep = 0)
    {
        PointF lineEnd = new(anchor.X + dirX * DiagThreefoldShaft1Len, anchor.Y + dirY * DiagThreefoldShaft1Len);
        PointF tail    = new(lineEnd.X + dirX * DiagThreefoldShaft2Len, lineEnd.Y + dirY * DiagThreefoldShaft2Len);

        const float invSqrt2 = 0.7071067811865475f;
        float l1x = (dirX + dirY) * invSqrt2, l1y = (-dirX + dirY) * invSqrt2;
        float l2x = (dirX - dirY) * invSqrt2, l2y = ( dirX + dirY) * invSqrt2;

        float triLeg = DiagThreefoldTriLeg;
        float legSumX = (l1x + l2x) * triLeg / 3f, legSumY = (l1y + l2y) * triLeg / 3f;
        float cornerX = lineEnd.X - legSumX, cornerY = lineEnd.Y - legSumY;
        PointF[] tri =
        [
            new(cornerX, cornerY),
            new(cornerX + l1x * triLeg, cornerY + l1y * triLeg),
            new(cornerX + l2x * triLeg, cornerY + l2y * triLeg),
        ];

        float halo = DiagThreefoldHaloWidth;
        float dotR = halo * 0.5f;
        using var haloPen    = new Pen(Color.White, halo)        { LineJoin = LineJoin.Round };
        using var triHaloPen = new Pen(Color.White, halo * 0.5f) { LineJoin = LineJoin.Round };
        using var shaft2Pen  = new Pen(Color.Black, OutlinePenWidth) { StartCap = LineCap.Round, EndCap = LineCap.Round };
        using var blackPen   = new Pen(Color.Black, OutlinePenWidth);
        using var white      = new SolidBrush(Color.White);

        // 三角に黒 outline を被せて fin との辺アライメントを揃える (fin pen と同太さ)。
        g.DrawLine(haloPen, anchor, lineEnd);
        g.DrawLine(blackPen, anchor, lineEnd);
        g.DrawPolygon(triHaloPen, tri);
        g.FillPolygon(fill, tri);
        g.DrawPolygon(blackPen, tri);
        if (finCount > 0) DrawScrewFins(g, blackPen, tri, finCount, edgeStep, ScrewFinTailLen);
        g.FillEllipse(white, lineEnd.X - dotR, lineEnd.Y - dotR, 2 * dotR, 2 * dotR);
        g.DrawLine(haloPen, lineEnd, tail);
        g.DrawLine(shaft2Pen, lineEnd, tail);
        float footR = dotR * DiagThreefoldFootRatio;
        g.FillEllipse(fill, anchor.X - footR, anchor.Y - footR, 2 * footR, 2 * footR);
    }

    /// <summary>(260502Ch) 立方晶 [101]/[011] 系などの斜め 2/2_1 軸を ITC Vol.A 風に描画。
    /// shaft/foot は斜め 3 回軸と同じ要領、中心記号は通常の 2 回軸 lens をそのまま使う。</summary>
    private static void DrawDiagonalTwofoldPerp(Graphics g, Brush fill, PointF anchor, float dirX, float dirY, bool screw)
    {
        PointF lineEnd = new(anchor.X + dirX * DiagThreefoldShaft1Len, anchor.Y + dirY * DiagThreefoldShaft1Len);
        PointF tail    = new(lineEnd.X + dirX * DiagThreefoldShaft2Len, lineEnd.Y + dirY * DiagThreefoldShaft2Len);

        float halo = DiagThreefoldHaloWidth;
        float dotR = halo * 0.5f;
        using var haloPen   = new Pen(Color.White, halo) { LineJoin = LineJoin.Round };
        using var shaft2Pen = new Pen(Color.Black, OutlinePenWidth) { StartCap = LineCap.Round, EndCap = LineCap.Round };
        using var blackPen  = new Pen(Color.Black, OutlinePenWidth);
        using var white     = new SolidBrush(Color.White);

        g.DrawLine(haloPen, anchor, lineEnd);
        g.DrawLine(blackPen, anchor, lineEnd);
        float lensRotationDeg = (float)(Math.Atan2(dirY, dirX) * 180.0 / Math.PI); // (260502Ch) 水平 shaft は現状維持、垂直 shaft は 90 度回転。
        DrawTwofoldPerp(g, fill, lineEnd, screw, rotationDeg: lensRotationDeg);
        g.FillEllipse(white, lineEnd.X - dotR, lineEnd.Y - dotR, 2 * dotR, 2 * dotR);
        g.DrawLine(haloPen, lineEnd, tail);
        g.DrawLine(shaft2Pen, lineEnd, tail);
        float footR = dotR * DiagThreefoldFootRatio;
        g.FillEllipse(fill, anchor.X - footR, anchor.Y - footR, 2 * footR, 2 * footR);
    }
    #endregion

    #region 紙面内 2(2_1) 軸 矢印
    private readonly record struct InPlaneAxisArrowDraft(PointF Anchor, double OutUx, double OutUy, bool Screw, double Sz);

    /// <summary>紙面内 2(2_1) 軸の矢印を draft に集約。同一 (位置, 方向, screw) で複数高さがあれば最小 sz を残す。</summary>
    private static void CollectInPlaneAxisArrows(CellLayout c, double sx, double sy, double sz,
                                                  (int U, int V, int W) dir, ProjectionAxis projAxis, bool screw,
                                                  Dictionary<(long, long, long, long, bool), InPlaneAxisArrowDraft> drafts)
    {
        var (dSx, dSy) = ProjectVector(dir.U, dir.V, dir.W, projAxis);
        double axisX = dSx * c.Horz.X + dSy * c.Vert.X, axisY = dSx * c.Horz.Y + dSy * c.Vert.Y;
        double axisLen = Math.Sqrt(axisX * axisX + axisY * axisY);
        if (axisLen < 1e-6) return;
        double ux = axisX / axisLen, uy = axisY / axisLen;

        // 角に接する隣接セル由来の 2 軸も拾うため 5×5 セル走査
        for (int ox = -2; ox <= 2; ox++) for (int oy = -2; oy <= 2; oy++)
            ClipAxisArrows(sx + ox, sy + oy);

        void ClipAxisArrows(double lineSx, double lineSy)
        {
            if (!TryClipUnitCell(lineSx, lineSy, dSx, dSy, out double tMin, out double tMax)) return;
            if (Math.Abs(tMax - tMin) < 1e-8)
            {
                double px = lineSx + tMin * dSx, py = lineSy + tMin * dSy;
                if (!OnCellBoundary(px, py)) return;
                TryAddTouchArrow(px, py, 1);
                TryAddTouchArrow(px, py, -1);
                return;
            }
            AddArrow(lineSx + tMin * dSx, lineSy + tMin * dSy, -ux, -uy);
            AddArrow(lineSx + tMax * dSx, lineSy + tMax * dSy, ux, uy);
        }

        void TryAddTouchArrow(double px, double py, int sign)
        {
            const double eps = 1e-4;
            if (InsideCell(px + sign * dSx * eps, py + sign * dSy * eps)) return;
            AddArrow(px, py, sign * ux, sign * uy);
        }

        void AddArrow(double px, double py, double outUx, double outUy)
        {
            px = NormalizeBoundary(px); py = NormalizeBoundary(py);
            if (!OnCellBoundary(px, py)) return;
            var p = c.ToScreen(px, py);
            var key = ((long)Math.Round(p.X * 1000), (long)Math.Round(p.Y * 1000),
                (long)Math.Round(outUx * 1000), (long)Math.Round(outUy * 1000), screw);
            if (drafts.TryGetValue(key, out var existing) && existing.Sz <= sz) return;
            drafts[key] = new InPlaneAxisArrowDraft(p, outUx, outUy, screw, sz);
        }
    }

    private static bool TryClipUnitCell(double x, double y, double dx, double dy, out double tMin, out double tMax)
    {
        tMin = double.NegativeInfinity; tMax = double.PositiveInfinity;
        Update(x, dx, ref tMin, ref tMax);
        Update(y, dy, ref tMin, ref tMax);
        return tMin <= tMax;

        static void Update(double s, double d, ref double tMin, ref double tMax)
        {
            if (Math.Abs(d) < 1e-9) { if (s < -1e-9 || s > 1 + 1e-9) tMin = 1; return; }
            double t1 = -s / d, t2 = (1 - s) / d;
            if (t1 > t2) (t1, t2) = (t2, t1);
            if (t1 > tMin) tMin = t1;
            if (t2 < tMax) tMax = t2;
        }
    }

    private static bool InsideCell(double x, double y) => x > 1e-6 && x < 1 - 1e-6 && y > 1e-6 && y < 1 - 1e-6;
    private static bool OnCellBoundary(double x, double y) =>
        x > -1e-6 && x < 1 + 1e-6 && y > -1e-6 && y < 1 + 1e-6 &&
        (Math.Abs(x) < 1e-6 || Math.Abs(x - 1) < 1e-6 || Math.Abs(y) < 1e-6 || Math.Abs(y - 1) < 1e-6);
    private static double NormalizeBoundary(double x) =>
        Math.Abs(x) < 1e-6 ? 0 : Math.Abs(x - 1) < 1e-6 ? 1 : x;

    private static void DrawCollectedInPlaneAxisArrows(Graphics g, Brush fill,
        Dictionary<(long, long, long, long, bool), InPlaneAxisArrowDraft> drafts)
    {
        if (drafts.Count == 0) return;
        using var pen = new Pen(Color.Black, InPlaneAxisPenWidth);
        using var brush = new SolidBrush(Color.Black);
        foreach (var group in drafts.Values
            .GroupBy(d => ((long)Math.Round(d.Anchor.X * 1000), (long)Math.Round(d.Anchor.Y * 1000),
                           (long)Math.Round(d.OutUx * 1000), (long)Math.Round(d.OutUy * 1000)))) // (260502Ch)
        {
            var list = group.OrderBy(d => d.Sz).ThenBy(d => d.Screw ? 0 : 1).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                var d = list[i];
                double offset = list.Count == 1 ? 0 : (i - (list.Count - 1) / 2.0) * 7.0; // (260502Ch) 同じ投影線上の 2 / 2_1 を少し並べる。
                float ox = (float)(-d.OutUy * offset), oy = (float)(d.OutUx * offset);
                var anchor = new PointF(d.Anchor.X + ox, d.Anchor.Y + oy);
                var tip = new PointF((float)(anchor.X + InPlaneArrowExt * d.OutUx), (float)(anchor.Y + InPlaneArrowExt * d.OutUy));
                g.DrawLine(pen, anchor, tip);
                DrawArrowhead(g, fill, tip, d.OutUx, d.OutUy, halfHead: d.Screw);
                string h = HeightLabel(d.Sz);
                if (h == null) continue; // (260503Ch) [ITA-D2] 高さ 0 は無標記、非ゼロ代表高さだけを表示する。
                var lbl = g.MeasureString(h, HeightLabelFont);
                // (260502Ch) 非ゼロ高さは矢印先端にくっつけて表示する。
                bool horiz = Math.Abs(d.OutUx) >= Math.Abs(d.OutUy);
                float lx = horiz
                    ? (d.OutUx >= 0 ? tip.X + 1 : tip.X - lbl.Width - 1)
                    : tip.X - lbl.Width / 2;
                float ly = horiz
                    ? tip.Y - lbl.Height / 2
                    : (d.OutUy >= 0 ? tip.Y + 1 : tip.Y - lbl.Height - 1);
                // (260502Cl) 同じ投影線上に複数軸 (2 / 2_1 並列) があるときは、ラベルが
                // 隣接軸領域へはみ出さないよう、自軸側に寄せる (中心線 midline で clamp)。
                if (horiz && oy != 0f)
                {
                    float midY = tip.Y - oy;
                    ly = oy > 0 ? Math.Max(ly, midY) : Math.Min(ly, midY - lbl.Height);
                }
                else if (!horiz && ox != 0f)
                {
                    float midX = tip.X - ox;
                    lx = ox > 0 ? Math.Max(lx, midX) : Math.Min(lx, midX - lbl.Width);
                }
                g.DrawString(h, HeightLabelFont, brush, lx, ly);
            }
        }
    }

    private static void DrawArrowhead(Graphics g, Brush fill, PointF tip, double ux, double uy, bool halfHead)
    {
        float bx = (float)(tip.X - ArrowHeadLen * ux), by = (float)(tip.Y - ArrowHeadLen * uy);
        PointF left  = new((float)(bx - ArrowHeadHalfWidth * uy), (float)(by + ArrowHeadHalfWidth * ux));
        PointF right = new((float)(bx + ArrowHeadHalfWidth * uy), (float)(by - ArrowHeadHalfWidth * ux));
        g.FillPolygon(fill, halfHead ? [tip, new PointF(bx, by), left] : [tip, left, right]);
    }
    #endregion

    #region 紙面平行 mirror corner bracket
    /// <summary>紙面平行 mirror/glide を IUCR corner bracket で描画。mirror があれば左上、glide は右下に分ける。
    /// (260503Ch) [ITA-D2], [ITA-D4] 高さは代表 h だけを選び、同高さの直交 glide は e-glide bracket へ統合する。</summary>
    private static void DrawParallelMirrorStack(Graphics g, CellLayout c, HashSet<(double Height, bool Glide, double GlideSx, double GlideSy)> markers, Brush fill)
    {
        if (markers.Count == 0) return;
        const float armLen = CornerBracketArmLen;
        const float offset = CornerBracketArmLen + CornerBracketGap;
        double hLen = Math.Sqrt(c.Horz.X * c.Horz.X + c.Horz.Y * c.Horz.Y);
        double vLen = Math.Sqrt(c.Vert.X * c.Vert.X + c.Vert.Y * c.Vert.Y);
        if (hLen < 1e-3 || vLen < 1e-3) return;
        float hUx = (float)(c.Horz.X / hLen), hUy = (float)(c.Horz.Y / hLen);
        float vUx = (float)(c.Vert.X / vLen), vUy = (float)(c.Vert.Y / vLen);
        var apex0 = new PointF(c.TopLeft.X - offset * (hUx + vUx), c.TopLeft.Y - offset * (hUy + vUy));
        using var pen = new Pen(Color.Black, CornerBracketPenWidth);
        using var brush = new SolidBrush(Color.Black);

        // (260503Ch) [ITA-D2], [ITA-D4] mirror は高さ 0 を優先、glide は方向ごとに低い高さを採用、e-glide (Ccce 等) は同高さの直交ペアを 1 個の bracket に統合。
        var symbols = new List<ParallelMirrorSymbol>();
        var mirrorHeights = markers
            .Where(m => !HasInPlaneGlide(m))
            .Select(m => HeightKey(m.Height))
            .Distinct()
            .OrderBy(h => h)
            .ToList();
        if (mirrorHeights.Count > 0)
            symbols.Add(new(mirrorHeights[0], 0, 0, 0, 0, false, false, 0));

        var glideReps = markers
            .Where(HasInPlaneGlide)
            .GroupBy(GlideKey)
            .Select(grp =>
            {
                var marker = grp.OrderBy(m => HeightKey(m.Height)).First();
                double sx = marker.GlideSx, sy = marker.GlideSy;
                NormalizeDiamondGlideDirection(ref sx, ref sy);
                return (Height: HeightKey(marker.Height), Sx: sx, Sy: sy, NG: IsNGlide(marker),
                    DG: IsDGlide(sx, sy), DS: GetDiamondArrowScore(sx, sy));
            })
            .ToList();
        foreach (var heightGrp in glideReps.GroupBy(g => g.Height))
        {
            var list = heightGrp.ToList();
            // (260502Ch) [ITA-D1] Pn-3 などで同一高さに現れる ±対角 n-glide は、紙面平行 bracket としては同じ情報なので 1 つに畳む。
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (!list[i].NG) continue;
                for (int j = 0; j < i; j++)
                    if (list[j].NG && R6(Math.Abs(list[j].Sx)) == R6(Math.Abs(list[i].Sx)) &&
                        R6(Math.Abs(list[j].Sy)) == R6(Math.Abs(list[i].Sy)))
                    {
                        list.RemoveAt(i);
                        break;
                    }
            }
            int merged = -1;
            for (int i = 0; i < list.Count - 1 && merged < 0; i++)
                for (int j = i + 1; j < list.Count; j++)
                    if (IsDoubleGlidePair(list[i].Sx, list[i].Sy, 0, list[j].Sx, list[j].Sy, 0))
                    {
                        // (260503Ch) [ITA-D4] centered cell の直交 glide pair は、2 本の別 bracket ではなく double-glide 記号 1 個で示す。
                        symbols.Add(new(heightGrp.Key, list[i].Sx, list[i].Sy, list[j].Sx, list[j].Sy, false, false, 0));
                        list.RemoveAt(j); list.RemoveAt(i);
                        merged = i;
                        break;
                    }
            foreach (var rep in list)
                symbols.Add(new(rep.Height, rep.Sx, rep.Sy, 0, 0, rep.NG, rep.DG, rep.DS));
        }

        bool hasDiamondGlide = symbols.Any(s => s.DGlide);
        var orderedSymbols = symbols
            .OrderBy(s => hasDiamondGlide ? s.DiamondScore : (s.NGlide ? 2 : 0))
            .ThenBy(s => s.Height)
            .ThenBy(s => Math.Atan2(s.GlideSy, s.GlideSx))
            .ToList();
        for (int i = 0; i < orderedSymbols.Count; i++)
        {
            var marker = orderedSymbols[i];
            // 左上 (i=0) の bracket は記号の左下にラベルを置く。それ以外は従来通り右側 (中央)。
            DrawBracket(new PointF(apex0.X + CornerBracketStep * i * (hUx + vUx), apex0.Y + CornerBracketStep * i * (hUy + vUy)),
                marker.Height, marker.GlideSx, marker.GlideSy, marker.GlideSx2, marker.GlideSy2, labelAtBottomLeft: i == 0);
        }

        void DrawBracket(PointF apex, double height, double glideSx, double glideSy, double glideSx2, double glideSy2, bool labelAtBottomLeft)
        {
            var hEnd = new PointF(apex.X + armLen * hUx, apex.Y + armLen * hUy);
            var vEnd = new PointF(apex.X + armLen * vUx, apex.Y + armLen * vUy);
            // 矢印は最大 2 本まで (e-glide 用)。
            var arrows = new List<(PointF Tip, PointF LineEnd, double Ux, double Uy)>();
            TryAddGlide(glideSx, glideSy);
            TryAddGlide(glideSx2, glideSy2);

            void TryAddGlide(double gSx, double gSy)
            {
                if (Math.Abs(gSx) <= 1e-3 && Math.Abs(gSy) <= 1e-3) return;
                float dx = (float)(gSx * c.Horz.X + gSy * c.Vert.X);
                float dy = (float)(gSx * c.Horz.Y + gSy * c.Vert.Y);
                double dlen = Math.Sqrt(dx * dx + dy * dy);
                if (dlen <= 0.5) return;
                double ux = dx / dlen, uy = dy / dlen;
                var end = new PointF((float)(apex.X + ux * armLen), (float)(apex.Y + uy * armLen));
                var lineEnd = new PointF((float)(end.X - ux * GlideArrowLineShorten), (float)(end.Y - uy * GlideArrowLineShorten));
                arrows.Add((end, lineEnd, ux, uy));
                // 映進方向が bracket の腕と重なる場合は、腕自体も矢頭より手前で止める。
                if (ux * hUx + uy * hUy > 0.98) hEnd = lineEnd;
                if (ux * vUx + uy * vUy > 0.98) vEnd = lineEnd;
            }

            g.DrawLine(pen, apex, hEnd);
            g.DrawLine(pen, apex, vEnd);
            float minX = Math.Min(apex.X, Math.Min(hEnd.X, vEnd.X));
            float maxX = Math.Max(apex.X, Math.Max(hEnd.X, vEnd.X));
            float maxY = Math.Max(apex.Y, Math.Max(hEnd.Y, vEnd.Y));
            foreach (var (tip, lineEndPt, ux, uy) in arrows)
            {
                minX = Math.Min(minX, tip.X);
                maxX = Math.Max(maxX, tip.X);
                maxY = Math.Max(maxY, tip.Y);
                g.DrawLine(pen, apex, lineEndPt);
                DrawArrowhead(g, fill, tip, ux, uy, halfHead: false);
            }

            // 高さラベル: 0 なら省略。labelAtBottomLeft (左上 bracket) は下向き腕の終端 vEnd の下・左脇、それ以外は右側 (中央) に置く。
            string lbl = HeightLabel(height);
            if (lbl == null) return; // (260503Ch) [ITA-D2] 高さ 0 は無標記、非ゼロ代表高さだけを表示する。
            var ls = g.MeasureString(lbl, HeightLabelFont);
            float labelX = labelAtBottomLeft ? vEnd.X - ls.Width - 2 : maxX + 2;
            float labelY = labelAtBottomLeft ? vEnd.Y + 2            : ((apex.Y + hEnd.Y) - ls.Height) / 2;
            g.DrawString(lbl, HeightLabelFont, brush, labelX, labelY);
        }

        static bool HasInPlaneGlide((double Height, bool Glide, double GlideSx, double GlideSy) m)
            => m.Glide && (Math.Abs(m.GlideSx) > 1e-3 || Math.Abs(m.GlideSy) > 1e-3);

        static bool IsNGlide((double Height, bool Glide, double GlideSx, double GlideSy) m)
        {
            double sx = CenterMod1(m.GlideSx), sy = CenterMod1(m.GlideSy);
            return Math.Abs(sx) > 1e-3 && Math.Abs(sy) > 1e-3 && !IsDGlide(sx, sy);
        }

        static bool IsDGlide(double glideSx, double glideSy)
            => IsQuarterGlideComponent(glideSx) && IsQuarterGlideComponent(glideSy);

        void NormalizeDiamondGlideDirection(ref double glideSx, ref double glideSy)
        {
            if (!IsDGlide(glideSx, glideSy)) return;
            double dy = glideSx * c.Horz.Y + glideSy * c.Vert.Y;
            if (dy < 0) { glideSx = -glideSx; glideSy = -glideSy; }
        }

        int GetDiamondArrowScore(double glideSx, double glideSy)
        {
            if (!IsDGlide(glideSx, glideSy)) return 0;
            double dx = glideSx * c.Horz.X + glideSy * c.Vert.X;
            double dy = glideSx * c.Horz.Y + glideSy * c.Vert.Y;
            if (dy > 0 && dx < 0) return -1;
            if (dy > 0 && dx > 0) return 1;
            return 0;
        }

        static double HeightKey(double height)
        {
            double h = Math.Round(Mod1(height), 6);
            return h > 1 - FracEps ? 0 : h;
        }

        static (long, long) GlideKey((double Height, bool Glide, double GlideSx, double GlideSy) m)
        {
            double sx = CenterMod1(m.GlideSx), sy = CenterMod1(m.GlideSy);
            if (sx < -1e-9 || (Math.Abs(sx) < 1e-9 && sy < 0)) { sx = -sx; sy = -sy; }
            return (R6(sx), R6(sy));
        }
    }
    #endregion

    #region 紙面垂直 mirror/glide
    /// <summary>紙面垂直 mirror/glide を幾何線ごとに集約し、d-glide 優先 → e-glide ペア → glide score 最小、の順に 1 つだけ描画。
    /// (260503Ch) [ITA-D1], [ITA-D4] 同じ幾何面に属する複数操作は、defining graphical symbol 1 個へ畳み込む。</summary>
    private static void DrawCollectedPerpendicularMirrorPlanes(ElementsContext ctx)
    {
        if (ctx.PerpendicularMirrors.Count == 0) return;
        var groups = ctx.PerpendicularMirrors
            .Select(d => (Draft: d, Key: GetPerpendicularMirrorLineKey(ctx, d.Sx, d.Sy, d.Direction)))
            .Where(x => x.Key.HasValue)
            .GroupBy(x => x.Key.Value);

        foreach (var group in groups)
        {
            var drafts = group.Select(x => x.Draft).ToList();
            // (260503Ch) [ITA-D4] d-glide 最優先 → e-glide ペア → glide score 最小、の順に 1 つだけ描く。
            var dDraft = drafts.FirstOrDefault(d =>
            {
                var (gSx, gSy, gSz) = ctx.Proj.ToScreen(d.Glide.U, d.Glide.V, d.Glide.W);
                return IsPerpendicularDGlide(gSx, gSy, gSz);
            });
            if (dDraft != default)
                DrawMirrorPerpToScreen(ctx, dDraft.Sx, dDraft.Sy, dDraft.Direction, dDraft.Glide);
            else if (TryFindDoubleGlideDraft(ctx.Proj, drafts, out var eDraft))
                DrawMirrorPerpToScreen(ctx, eDraft.Sx, eDraft.Sy, eDraft.Direction, eDraft.Glide, forceEGlide: true); // (260503Ch) [ITA-D4] 直交する half-glide pair は e-glide style で示す。
            else
            {
                // (260503Ch) [ITA-D1] R-3m 等の重複 glide 表現を捨て、純 mirror や a/b-glide を残すため glide score 最小を採用。
                var best = drafts.OrderBy(d =>
                {
                    var (gSx, gSy, gSz) = ctx.Proj.ToScreen(d.Glide.U, d.Glide.V, d.Glide.W);
                    return Math.Abs(gSx) + Math.Abs(gSy) + Math.Abs(gSz);
                }).First();
                DrawMirrorPerpToScreen(ctx, best.Sx, best.Sy, best.Direction, best.Glide);
            }
        }
    }

    private static bool TryFindDoubleGlideDraft(Projection proj, List<PerpendicularMirrorDraft> drafts, out PerpendicularMirrorDraft draft)
    {
        for (int i = 0; i < drafts.Count - 1; i++)
            for (int j = i + 1; j < drafts.Count; j++)
            {
                var a = proj.ToScreen(drafts[i].Glide.U, drafts[i].Glide.V, drafts[i].Glide.W);
                var b = proj.ToScreen(drafts[j].Glide.U, drafts[j].Glide.V, drafts[j].Glide.W);
                if (!IsDoubleGlidePair(a.Sx, a.Sy, a.Sz, b.Sx, b.Sy, b.Sz)) continue;
                draft = drafts[i];
                return true;
            }
        draft = default;
        return false;
    }

    private static (long Nx, long Ny, long D)? GetPerpendicularMirrorLineKey(ElementsContext ctx, double sx, double sy, (int U, int V, int W) dir)
    {
        if (!TryGetMirrorPerpGeometry(ctx.C, ctx.Proj.Axis, dir, out _, out _, out double nX, out double nY)) return null;
        double nLen = Math.Sqrt(nX * nX + nY * nY);
        if (nLen < 1e-9) return null;
        double ux = nX / nLen, uy = nY / nLen;
        if (ux < -1e-9 || (Math.Abs(ux) < 1e-9 && uy < 0)) { ux = -ux; uy = -uy; }
        var pt = ctx.C.ToScreen(NormalizeCellBoundary(sx), NormalizeCellBoundary(sy));
        return (R6(ux), R6(uy), (long)Math.Round((ux * pt.X + uy * pt.Y) * 1000));
    }

    /// <summary>紙面垂直 mirror/glide を法線直交直線で描画。線種は IT 分解で選択 (純=実線、a/b=長破線、c=短点線、n=dash-dot、d=dot-dash-dot-dash-dot-arrow、e=dot-dot-dash)。
    /// 直交を CellLayout (実空間 cartesian) で計算するので非直交セルでも正しい角度になる。</summary>
    private static void DrawMirrorPerpToScreen(ElementsContext ctx, double sx, double sy, (int U, int V, int W) dir, (double U, double V, double W) it, bool forceEGlide = false)
    {
        var c = ctx.C;
        if (!TryGetMirrorPerpGeometry(c, ctx.Proj.Axis, dir, out double perpSx, out double perpSy, out double nX, out double nY)) return;
        var (gSx, gSy, gSz) = ctx.Proj.ToScreen(it.U, it.V, it.W);
        bool hasInPlane = Math.Abs(gSx) > 1e-3 || Math.Abs(gSy) > 1e-3;
        bool hasDepth   = Math.Abs(gSz) > 1e-3;
        bool dGlide = IsPerpendicularDGlide(gSx, gSy, gSz);
        int style = dGlide ? 4 : forceEGlide ? 5 : (hasInPlane, hasDepth) switch { (false, false) => 0, (true, false) => 1, (false, true) => 2, _ => 3 }; // (260503Ch) [ITA-D4]
        Pen pen = style switch { 0 => ctx.MirrorPen, 1 => ctx.InPlanePen, 2 => ctx.DepthPen, 3 => ctx.DiagPen, 5 => ctx.EPen, _ => ctx.MirrorPen };

        Draw(sx, sy);
        // 境界上の mirror/glide は単位胞の対辺にも同じ対称要素として表示する。
        if (perpSx == 0)
        {
            double normSx = NormalizeCellBoundary(sx);
            if (normSx < EdgeReplicate || 1 - normSx < EdgeReplicate) { Draw(0, sy); Draw(1, sy); }
        }
        if (perpSy == 0)
        {
            double normSy = NormalizeCellBoundary(sy);
            if (normSy < EdgeReplicate || 1 - normSy < EdgeReplicate) { Draw(sx, 0); Draw(sx, 1); }
        }

        void Draw(double lineSx, double lineSy)
        {
            lineSx = NormalizeCellBoundary(lineSx);
            lineSy = NormalizeCellBoundary(lineSy);
            var (start, end) = SpanLineThroughCell(c, lineSx, lineSy, perpSx, perpSy);
            if (!start.HasValue || !end.HasValue) return;
            double nLen = Math.Sqrt(nX * nX + nY * nY);
            if (nLen < 1e-9) return;
            double ux = nX / nLen, uy = nY / nLen;
            if (ux < -1e-9 || (Math.Abs(ux) < 1e-9 && uy < 0)) { ux = -ux; uy = -uy; }
            var pt = c.ToScreen(lineSx, lineSy);
            var key = (R6(ux), R6(uy), (long)Math.Round((ux * pt.X + uy * pt.Y) * 1000), style);
            if (!ctx.DrawnMirrorPlanes.Add(key)) return;
            if (dGlide)
            {
                var (arrowX, arrowY) = GetDGlideArrowDirection(c, gSx, gSy, gSz);
                DrawDGlidePerpLine(ctx.G, pen, ctx.Fill, start.Value, end.Value, arrowX, arrowY);
                return;
            }
            ctx.G.DrawLine(pen, start.Value, end.Value);
        }
    }

    private static bool IsQuarterGlideComponent(double v)
    {
        double a = Math.Abs(CenterMod1(v));
        return Math.Abs(a - 0.25) < FracEps;
    }

    private static bool IsHalfGlideComponent(double v)
    {
        double a = Math.Abs(CenterMod1(v));
        return Math.Abs(a - 0.5) < FracEps;
    }

    private static bool HasGlideComponent(double v) => Math.Abs(CenterMod1(v)) > 1e-3;

    private static bool IsSimpleHalfGlideVector(double x, double y, double z)
    {
        int nonZero = (HasGlideComponent(x) ? 1 : 0) + (HasGlideComponent(y) ? 1 : 0) + (HasGlideComponent(z) ? 1 : 0);
        int half = (IsHalfGlideComponent(x) ? 1 : 0) + (IsHalfGlideComponent(y) ? 1 : 0) + (IsHalfGlideComponent(z) ? 1 : 0);
        return nonZero == 1 && half == 1;
    }

    /// <summary>e-glide 判定。互いに独立な単一方向 half-glide 2 本ある場合を double-glide とする。</summary>
    private static bool IsDoubleGlidePair(double x1, double y1, double z1, double x2, double y2, double z2)
    {
        if (!IsSimpleHalfGlideVector(x1, y1, z1) || !IsSimpleHalfGlideVector(x2, y2, z2)) return false;
        double cx = y1 * z2 - z1 * y2, cy = z1 * x2 - x1 * z2, cz = x1 * y2 - y1 * x2;
        return cx * cx + cy * cy + cz * cz > 1e-6;
    }

    private static bool IsPerpendicularDGlide(double gSx, double gSy, double gSz)
        => IsQuarterGlideComponent(gSz) && (IsQuarterGlideComponent(gSx) || IsQuarterGlideComponent(gSy));

    private static bool TryGetMirrorPerpGeometry(CellLayout c, ProjectionAxis axis, (int U, int V, int W) dir,
                                                 out double perpSx, out double perpSy, out double nX, out double nY)
    {
        var (dSx, dSy) = ProjectVector(dir.U, dir.V, dir.W, axis);
        nX = dSx * c.Horz.X + dSy * c.Vert.X;
        nY = dSx * c.Horz.Y + dSy * c.Vert.Y;
        double pX = -nY, pY = nX;
        double det = c.Horz.X * c.Vert.Y - c.Vert.X * c.Horz.Y;
        perpSx = perpSy = 0;
        if (Math.Abs(det) < 1e-9) return false;
        perpSx = (c.Vert.Y * pX - c.Vert.X * pY) / det;
        perpSy = (-c.Horz.Y * pX + c.Horz.X * pY) / det;
        // hex/trig で cos(120°) 精度誤差により perpS* が 1e-8 オーダーになる。SpanLineThroughCell の 1e-9 閾値では非零扱いになり、
        // 微小 d で除算して縮退線を返し、隣接辺の dedup key を先取りしてしまうため 0 へ丸める。
        const double edgeLineEps = 1e-6;
        if (Math.Abs(perpSx) < edgeLineEps) perpSx = 0;
        if (Math.Abs(perpSy) < edgeLineEps) perpSy = 0;
        return true;
    }

    /// <summary>d-glide 矢印の基準方向。(0,+1/4,+1/4) は C 投影で右向き (+Y = +b) になる。</summary>
    private static (float X, float Y) GetDGlideArrowDirection(CellLayout c, double gSx, double gSy, double gSz)
    {
        double depthSign = CenterMod1(gSz) < 0 ? -1 : 1;
        return ((float)(depthSign * (gSx * c.Horz.X + gSy * c.Vert.X)),
                (float)(depthSign * (gSx * c.Horz.Y + gSy * c.Vert.Y)));
    }

    /// <summary>紙面垂直 d-glide の dot-dash-dot-dash-dot-arrow 反復線。</summary>
    private static void DrawDGlidePerpLine(Graphics g, Pen pen, Brush fill, PointF start, PointF end, float arrowX, float arrowY)
    {
        double dx = end.X - start.X, dy = end.Y - start.Y;
        double len = Math.Sqrt(dx * dx + dy * dy);
        if (len < 1) return;
        double ux = dx / len, uy = dy / len;
        double arrowLen0 = Math.Sqrt(arrowX * arrowX + arrowY * arrowY);
        if (arrowLen0 > 1e-6 && ux * arrowX / arrowLen0 + uy * arrowY / arrowLen0 < 0)
        {
            (start, end) = (end, start);
            ux = -ux; uy = -uy;
        }

        const float dashLen = 9f;
        const float arrowLen = 13f;
        for (int i = 0; ; i++)
        {
            float baseT = 3f + i * DGlidePatternPitch;
            if (baseT >= len) break;
            Dot(baseT);
            Dash(baseT + 6f, baseT + 6f + dashLen);
            Dot(baseT + 19f);
            Dash(baseT + 25f, baseT + 25f + dashLen);
            Dot(baseT + 38f);
            Arrow(baseT + 44f, baseT + 44f + arrowLen);
        }

        PointF Pt(double t) => new((float)(start.X + ux * t), (float)(start.Y + uy * t));

        void Dot(float t)
        {
            if (t < 0 || t > len) return;
            var p = Pt(t);
            g.FillEllipse(fill, p.X - DGlideDotR, p.Y - DGlideDotR, 2 * DGlideDotR, 2 * DGlideDotR);
        }

        void Dash(float t1, float t2)
        {
            t1 = Math.Max(0, t1); t2 = Math.Min((float)len, t2);
            if (t2 <= t1) return;
            g.DrawLine(pen, Pt(t1), Pt(t2));
        }

        void Arrow(float t1, float t2)
        {
            if (t2 > len || t2 <= t1) return;
            var tip = Pt(t2);
            var lineEnd = Pt(Math.Max(t1, t2 - GlideArrowLineShorten));
            g.DrawLine(pen, Pt(t1), lineEnd);
            DrawArrowhead(g, fill, tip, ux, uy, halfHead: false);
        }
    }

    /// <summary>s≈1 (右辺/下辺) は 0 に折り畳まず 1 のまま残す: drawnMirrorPlanes の dedup キーが左/右辺で別になり両辺それぞれに描画できる。</summary>
    private static double NormalizeCellBoundary(double s)
    {
        if (Math.Abs(s - 1) < 1e-8) return 1;
        double m = s - Math.Floor(s);
        if (m < 1e-8) return 0;
        if (m > 1 - 1e-8) return 1;
        return m;
    }
    #endregion

}
