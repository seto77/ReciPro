// 260501Cl: 対称要素 (左図) を ITC Vol.A 風に GDI+ 描画する子クラス。
// 反転中心 / 紙面垂直 2(2_1) 軸 / 紙面内 2(2_1) 軸 / 紙面垂直 mirror/glide / 紙面平行 mirror をサポート。
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
// (260502Cl) Vector3DBase / Matrix3D を短縮エイリアスで参照する。
using Vec = Crystallography.Vector3DBase;
using Mat = Crystallography.Matrix3D;

namespace Crystallography.Controls;

public class SymmetryDiagramElements : SymmetryDiagramCommon
{
    #region 定数
    // (260502Cl) 個別シンボル描画で使う寸法・線幅をクラス冒頭に集約。
    // 単位は全て pixel。ITC Vol.A の表記慣用に沿う形で大きさを決めている。

    //----------------------------------------------------------------------
    // 線幅 (Pen.Width)
    //----------------------------------------------------------------------
    /// <summary>一般用ペン幅。セル枠線・反転中心 ○ の縁線などに使用。</summary>
    private const float DefaultPenWidth        = 1.2f;
    /// <summary>紙面垂直 mirror / glide の線幅。実線 (m) / 長破線 (a,b) / 短点線 (c) / dash-dot (n) の 4 種が同じ太さで描かれる。</summary>
    private const float MirrorPenWidth         = 2.2f;
    /// <summary>紙面垂直 3/4/6 回 多角形シンボルの輪郭線幅。screw fin (指示棒) も同じ太さで描く。</summary>
    private const float OutlinePenWidth        = 1.2f;
    /// <summary>紙面垂直 2(2_1) 軸 lens (vesica piscis) の screw 表示用 円弧の線幅。screw=true のとき lens 上下に短弧を重ねて描画。</summary>
    private const float ScrewFinPenWidth       = 1.2f;
    /// <summary>紙面内 2(2_1) 軸を示す矢印 (セル外側に飛び出す矢) の軸線・矢頭の線幅。</summary>
    private const float InPlaneAxisPenWidth    = 1.4f;
    /// <summary>紙面平行 mirror の corner bracket (セル左上外側に配置する L 字記号) の線幅。映進矢印もこのペンで描画。</summary>
    private const float CornerBracketPenWidth  = 1.6f;
    /// <summary>(260502Ch) 単位胞境界と重なる mirror/glide を視認しやすく重ね描きするときの最低線幅。MirrorPenWidth がこれより細い場合のみ太らせる。</summary>
    private const float BoundaryEdgeMinWidth   = 3.0f;
    /// <summary>(260502Ch) 線記号 (mirror/glide や紙面内 2 軸) の上に紙面垂直点記号 (lens, 多角形) が重なるとき、点記号の周囲に敷く白いハロー (縁取り) の線幅。背景の mirror 線を白で消して点記号を読みやすくする。</summary>
    private const float SymbolHaloPenWidth     = 4.8f;

    //----------------------------------------------------------------------
    // 紙面垂直 2 (2_1) 軸 — vesica piscis (同径 2 円の重なり) lens シンボル
    //----------------------------------------------------------------------
    /// <summary>lens の半縦寸法 (中心から尖端まで)。lens の縦方向「長径」 = 2 × TwofoldHalfH。</summary>
    private const float TwofoldHalfH    = 8f;
    /// <summary>lens の半横寸法 (中心から最大幅まで)。lens の横方向「短径」 = 2 × TwofoldHalfW。HalfH/HalfW 比で lens の細長さが決まる。</summary>
    private const float TwofoldHalfW    = 4f;
    /// <summary>lens 上下に重ねる screw 弧のスイープ角 (度)。screw=true のとき右弧・左弧をそれぞれこの角度ぶん描画。</summary>
    private const float ScrewFinSweepDeg = 30f;

    //----------------------------------------------------------------------
    // 紙面垂直 3/4/6 回 (および -3, -4, -6) — 正多角形シンボル
    //----------------------------------------------------------------------
    /// <summary>3 回回転 (▲) の外接円半径。-3 (黒塗り三角 + 中心白丸) も同サイズ。</summary>
    private const float ThreeFoldRadius = 5.625f;
    /// <summary>4 回回転 (■) の外接円半径。-4 (白塗り四角 + 内部 lens) も同サイズ。</summary>
    private const float FourFoldRadius  = 7.2f;
    /// <summary>6 回回転 (⬢) の外接円半径。-6 (白塗り六角 + 内接三角) も同サイズ。</summary>
    private const float SixFoldRadius   = 6.0f;
    /// <summary>n_k 螺旋を示す指示棒 (フィン) の長さ。多角形頂点から外向きに伸ばす。</summary>
    private const float ScrewFinTailLen = 5f;
    /// <summary>-3 (回反 3) の中心に置く小さな白丸の半径。三角形を黒で塗った上に重ねて中心の "反転" を示す。</summary>
    private const float MinusThreeCenterDotR    = 2f;
    /// <summary>-4 (回反 4) で四角形内部に重ねる lens の縮小率。DrawTwofoldPerp に scale として渡し、本来の TwofoldHalfH/HalfW を 0.8 倍にしてから描画。</summary>
    private const float MinusFourInnerLensScale = 0.8f;

    //----------------------------------------------------------------------
    // 反転中心 — 白丸 ○
    //----------------------------------------------------------------------
    /// <summary>反転中心 ○ の半径。黒縁・白塗り。z != 0 のときは右上に高さラベルを併記。</summary>
    private const float InversionR = 3.5f;

    //----------------------------------------------------------------------
    // 紙面内 2 (2_1) 軸 — セル境界外側に飛び出す矢印
    //----------------------------------------------------------------------
    /// <summary>軸が交差するセル枠 (anchor) から矢頭までの矢印長。セル外側方向に伸ばす。</summary>
    private const float InPlaneArrowExt    = 32f;
    /// <summary>矢頭の長さ (tip から矢印基線方向の base 点までの距離)。</summary>
    private const float ArrowHeadLen       = 7f;
    /// <summary>矢頭の半幅 (base 点から左右羽根までの直交距離)。screw のときは片羽根のみ描く半欠け矢頭になる。</summary>
    private const float ArrowHeadHalfWidth = 3f;

    //----------------------------------------------------------------------
    // 紙面平行 mirror — セル左上外側の corner bracket (L 字)
    //----------------------------------------------------------------------
    /// <summary>corner bracket の各腕 (a 方向と b 方向) の長さ。bracket は apex から 2 本の腕がセル軸に沿って伸びる L 字。</summary>
    private const float CornerBracketArmLen = 22f;
    /// <summary>セル左上頂点と corner bracket の apex の間の余白 (a, b 各方向)。apex 位置は TopLeft から (gap + armLen) ぶん外側。</summary>
    private const float CornerBracketGap    = 22f;
    /// <summary>映進 mirror のとき bracket の apex から伸ばす glide 矢印の長さ (CornerBracketArmLen 比)。腕より僅かに短くして矢頭が腕の外に出ないよう調整。</summary>
    private const float GlideArrowScale     = 0.92f;

    // /// <summary>(I−R_xy)⁻¹ for c 軸 proper rotation。key=(|Order|, Sense), value=(a, b, c, d) of 2×2 inverse matrix.</summary>
    // private static readonly (int O, bool S, double A, double B, double C, double D)[] InvMatTable =
    // [
    //     (3, true,  2.0/3, -1.0/3,  1.0/3, 1.0/3), (3, false, 1.0/3, 1.0/3, -1.0/3, 2.0/3),
    //     (4, true,  0.5,   -0.5,    0.5,   0.5  ), (4, false, 0.5,   0.5,   -0.5,   0.5  ),
    //     (6, true,  1,     -1,      1,     0    ), (6, false, 0,     1,     -1,     1    ),
    // ];
    // (260502Ch) c 軸専用の逆行列表ではなく、下の (I-R) 線形分解で任意方向の等価要素を扱う。
    #endregion

    #region 公開 API
    /// <summary>左側の対称要素図を描画。反転中心 / 紙面垂直 2(2_1) 軸 (lens) / 紙面内 2(2_1) 軸 (セル外矢印) / 紙面垂直 mirror/glide (線種別) / 紙面平行 mirror (左上 corner bracket)。</summary>
    public static Bitmap RenderSymmetryElements(int seriesNumber, Size clientSize, ProjectionAxis axis = ProjectionAxis.C)
    {
        var bmp = NewBitmap(clientSize, out var g);
        try
        {
            if (!TryGetSym(seriesNumber, out var sym, out var msg))
            {
                if (msg != null) DrawCenteredText(g, bmp.Size, msg, Color.Gray);
                return bmp;
            }
            var actualAxis = ResolveProjectionAxis(sym, axis);
            var proj = GetProjection(actualAxis);
            var layout = ComputeCellLayout(bmp.Size, sym, actualAxis);
            DrawCellAndAxes(g, layout, proj, sym);
            // R-lattice 等で base op + centering の screw 系列が data に無い場合は runtime で synthetic op を生成
            // var ops = ExpandWithCentering(SymmetryStatic.WyckoffPositions[seriesNumber][0].PositionOperations);
            // (260502Ch) P 格子の trigonal/hexagonal でも ApplyMatrix が正しい基底を使えるよう、series number を付与してから処理する。
            var baseOps = SymmetryStatic.WyckoffPositions[seriesNumber][0].PositionOperations;
            var ops = ExpandWithCentering(baseOps?.Select(op => new SymmetryOperation(op, seriesNumber)).ToArray());
            if (ops != null) ops = ops.Select(op => new SymmetryOperation(op, seriesNumber)).ToArray(); // (260502Ch) synthetic op も series-aware に戻す
            if (ops == null) return bmp;
            using var pen = new Pen(Color.Black, DefaultPenWidth);
            using var mirrorPen   = new Pen(Color.Black, MirrorPenWidth);                                                                              // 純鏡面: 実線
            using var inPlanePen  = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [5f, 3f] };                     // a/b glide: 長破線
            using var depthPen    = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [1f, 2.5f] };                   // c-glide: 短点線
            using var diagPen     = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [5f, 2.5f, 1f, 2.5f] };         // n-glide: dash-dot
            using var fill = new SolidBrush(Color.Black);
            using var white = new SolidBrush(Color.White);
            var parallelMirrors = new HashSet<(double Height, bool Glide, double GlideSx, double GlideSy)>();
            var drawnMirrorPlanes = new HashSet<(long Nx, long Ny, long D, int Style)>(); // (260502Ch) 対称操作で展開した等価 mirror/glide 面の重複描画を抑制
            var boundaryMirrorEdges = new HashSet<(int Edge, int Style)>(); // (260502Ch) セル境界に一致する mirror/glide は最後に枠へ重ね描きする
            // var drawnInPlaneAxisArrows = new HashSet<(long Px, long Py, long Ux, long Uy, bool Screw, string Label)>(); // (260502Ch) 等価な紙面内 2/2_1 軸矢印の重複抑制
            // (260502Cl) 紙面内 2(2_1) 軸の矢印は draft に集約してから描画。同じ矢印位置が複数高さで現れたら、ITC 慣用に従い最小高さ (Mod1) を採用するため。
            var inPlaneAxisDrafts = new Dictionary<(long Px, long Py, long Ux, long Uy, bool Screw), InPlaneAxisArrowDraft>();
            // covered2: 3/4/6 回位置→2 回抑制、covered3: 6 回位置→3 回抑制 (高次が低次を含意)
            var covered2 = CollectHigherRotationPositions(ops, proj, actualAxis, 3);
            var covered3 = CollectHigherRotationPositions(ops, proj, actualAxis, 6);
            var inversions = CollectInversions(seriesNumber, proj, layout);
            // foreach (var op in ops)
            //     DrawSymmetryElement(g, layout, proj, actualAxis, op, pen, mirrorPen, inPlanePen, depthPen, diagPen, fill, white,
            //         parallelMirrors, covered2, covered3, ops, drawnMirrorPlanes, boundaryMirrorEdges, drawnInPlaneAxisArrows);
            // (260502Ch) 線/面/紙面内軸を先に描き、紙面垂直な回転・螺旋・回反の点記号は最後の pass に回す。
            foreach (var op in ops)
                DrawSymmetryElement(g, layout, proj, actualAxis, op, pen, mirrorPen, inPlanePen, depthPen, diagPen, fill, white,
                    parallelMirrors, covered2, covered3, ops, drawnMirrorPlanes, boundaryMirrorEdges, inPlaneAxisDrafts,
                    skipPerpPointMarks: true);
            DrawInversions(g, inversions, pen, white, fill);
            DrawParallelMirrorStack(g, layout, parallelMirrors, fill);
            // (260502Cl) DrawBoundaryMirrorEdges は不要との判断で一旦無効化
            // DrawBoundaryMirrorEdges(g, layout, boundaryMirrorEdges, mirrorPen, inPlanePen, depthPen, diagPen); // (260502Ch)
            DrawCollectedInPlaneAxisArrows(g, fill, inPlaneAxisDrafts); // (260502Cl)
            foreach (var op in ops)
                DrawSymmetryElement(g, layout, proj, actualAxis, op, pen, mirrorPen, inPlanePen, depthPen, diagPen, fill, white,
                    parallelMirrors, covered2, covered3, ops, drawOnlyPerpPointMarks: true); // (260502Ch)
        }
        finally { g.Dispose(); }
        return bmp;
    }
    #endregion

    #region centering 展開と axis 列挙
    /// <summary>R-lattice 等の centering を runtime で展開し、同じ線形部を持つ等価 Seitz 操作を生成する。(260502Ch)</summary>
    private static SymmetryOperation[] ExpandWithCentering(SymmetryOperation[] ops)
    {
        if (ops == null) return null;
        var cents = new List<(double U, double V, double W)>();
        foreach (var op in ops)
        {
            if (op.Order != 1) continue;
            var (cu, cv, cw) = op.Position;
            if (Math.Abs(cu) + Math.Abs(cv) + Math.Abs(cw) > 1e-6) cents.Add((cu, cv, cw));
        }
        if (cents.Count == 0) return ops;
        var result = new List<SymmetryOperation>(ops.Length * (cents.Count + 1));
        var seen = new HashSet<(int O, bool S, int DU, int DV, int DW, long PX, long PY, long PZ, long TU, long TV, long TW)>();
        foreach (var op in ops) AddOperation(op);
        // 各 c 軸 proper rotation × centering で synthetic op を生成。new_Seitz の xy を (I−R_xy)⁻¹ で軸位置に変換、z は screw 成分。
        // (260502Ch) c 軸限定ではなく、centering L を (I-R)s + residual に分けて全方向の等価操作へ拡張する。
        foreach (var op in ops)
        {
            foreach (var c in cents)
            {
                if (Math.Abs(c.U) + Math.Abs(c.V) + Math.Abs(c.W) < 1e-9) continue;
                if (TryCreateCenteredOperation(op, c, out var centered)) AddOperation(centered);
            }
        }
        return result.ToArray();

        void AddOperation(SymmetryOperation op)
        {
            var key = (op.Order, op.Sense, op.Direction.U, op.Direction.V, op.Direction.W,
                (long)Math.Round(Mod1(op.Position.U) * 1e6), (long)Math.Round(Mod1(op.Position.V) * 1e6), (long)Math.Round(Mod1(op.Position.W) * 1e6),
                (long)Math.Round(CenterMod1(op.IntrinsicTranslation.U) * 1e6), (long)Math.Round(CenterMod1(op.IntrinsicTranslation.V) * 1e6),
                (long)Math.Round(CenterMod1(op.IntrinsicTranslation.W) * 1e6));
            if (seen.Add(key)) result.Add(op);
        }
    }

    /// <summary>n_k 螺旋の (FinCount, EdgeStep)。k=round(IT_along·N) を計算し、N-fold pinwheel の edge 方向は N−k。
    /// gcd(N,k)>1 (4_2/6_2/6_3/6_4) は ITC 規約に従い fin 数を減らした特例形に。</summary>
    private static (int FinCount, int EdgeStep) ScrewParams(SymmetryOperation op, (double U, double V, double W)? intrinsicTranslation = null)
    {
        int N = Math.Abs(op.Order);
        if (N < 2) return (0, 0);
        // var (du, dv, dw) = op.Direction;
        // double along = (tu * du + tv * dv + tw * dw) / (du * du + dv * dv + dw * dw);
        // (260502Ch) screw 成分も (I-R)^T の零空間で読む。斜交基底や等価操作での軸方向成分に依存させる。
        if (!TryGetAxisFraction(op, intrinsicTranslation ?? op.IntrinsicTranslation, out double along)) return (0, 0);
        if (along < 1e-3 || along > 1 - 1e-3) return (0, 0);
        int k = ((int)Math.Round(along * N)) % N;
        if (k == 0) return (0, 0);
        if (N == 4 && k == 2) return (2, 1);   // 4_2: 4_3 から R/L 削除
        if (N == 6 && k == 2) return (3, 5);   // 6_2: 6_1 から UR/UL/B 削除
        if (N == 6 && k == 3) return (2, 1);   // 6_3: 6_5 から T/B のみ
        if (N == 6 && k == 4) return (3, 1);   // 6_4: 6_5 から UR/UL/B 削除
        return (N, N - k);
    }

    /// <summary>紙面内 2 回軸用に、格子並進込みの intrinsic translation から 2_1 かを判定する。(260502Ch)</summary>
    private static bool IsScrewAxis(SymmetryOperation op, (double U, double V, double W) intrinsicTranslation)
    {
        if (!TryGetAxisFraction(op, intrinsicTranslation, out double along)) return false;
        return Math.Abs(along) > 1e-3 && Math.Abs(Math.Abs(along) - 1) > 1e-3;
    }

    private static bool TryGetAxisFraction(SymmetryOperation op, (double U, double V, double W) translation, out double along)
    {
        along = 0;
        var a = BuildIMinusR(op);
        if (a.Rank() != 2 || !TryFindAxisCovector(a, out var n)) return false;
        // (260502Cl) (int,int,int) / (double,double,double) → Vec implicit 変換を使う。
        Vec d = op.Direction;
        double nd = n * d; // Vec * Vec = 内積
        if (Math.Abs(nd) < 1e-12) return false;
        along = Mod1(n * (Vec)translation / nd);
        return true;
    }

    private static bool TryCreateCenteredOperation(SymmetryOperation op, (double U, double V, double W) centering, out SymmetryOperation centered)
    {
        centered = default;
        if (op.Order == 1) return false;
        var a = BuildIMinusR(op);
        Vec lattice = centering; // (260502Cl) implicit 変換
        bool ok;
        Vec shift, residual;
        if (a.Rank() == 2 && op.Order > 0)
        {
            ok = TryDecomposeAxisLatticeTranslation(op, lattice, out shift, out residual);
        }
        else
        {
            ok = TrySolveLinear(a, lattice, out shift);
            residual = lattice - a * shift; // (260502Cl) lattice - (I-R)·shift = 軸方向残差
        }
        if (!ok) return false;

        var p = op.Position;
        var it = op.IntrinsicTranslation;
        centered = new SymmetryOperation(op.Order, op.Sense ? 1 : -1, op.Direction,
            (Mod1(p.U + shift.X), Mod1(p.V + shift.Y), Mod1(p.W + shift.Z)),
            (CenterMod1(it.U + residual.X), CenterMod1(it.V + residual.Y), CenterMod1(it.W + residual.Z))); // (260502Ch)
        return true;
    }

    // (260502Cl) 線形代数層を Mat / Vec ベースに書き換え。tuple ベースの inline 演算を operator/メソッドに集約。

    /// <summary>I - R を Mat で返す。R は op の線形部 (回転 / 回反)。列ベクトルは I-R が (e_i) に作用した結果。</summary>
    private static Mat BuildIMinusR(SymmetryOperation op)
    {
        // (260502Cl) op.ApplyMatrix(Vec) で R·e_i を直接 Vec で取得。
        var rU = op.ApplyMatrix(new Vec(1, 0, 0));
        var rV = op.ApplyMatrix(new Vec(0, 1, 0));
        var rW = op.ApplyMatrix(new Vec(0, 0, 1));
        return new Mat(new Vec(1, 0, 0) - rU, new Vec(0, 1, 0) - rV, new Vec(0, 0, 1) - rW);
    }

    // (260502Cl) MatrixRank ヘルパーは Mat.Rank() に置き換えたため削除。

    private static bool TryFindAxisCovector(Mat a, out Vec n)
    {
        var (c0, c1, c2) = (a.Column(0), a.Column(1), a.Column(2));
        n = c0.Cross(c1);
        double n2 = n * n;
        var cand = c0.Cross(c2);
        double cand2 = cand * cand;
        if (cand2 > n2) { n = cand; n2 = cand2; }
        cand = c1.Cross(c2);
        cand2 = cand * cand;
        if (cand2 > n2) { n = cand; n2 = cand2; }
        return n2 > 1e-12;
    }

    private static bool TrySolveLinear(Mat a, Vec b, out Vec x)
    {
        x = Vec.Zero;
        int rank = a.Rank();
        if (rank == 0) return b * b < 1e-12;
        if (rank == 1) return TrySolveRankOne(a, b, out x);
        if (rank == 2) return TrySolveRankTwo(a, b, out x);

        // フルランク: Cramer の公式で解く。Mat.Determinant(Vec, Vec, Vec) で 3 列ベクトルから直接計算。
        var (c0, c1, c2) = (a.Column(0), a.Column(1), a.Column(2));
        double det = a.Determinant();
        if (Math.Abs(det) < 1e-12) return false;
        x = new Vec(
            Mat.Determinant(b, c1, c2) / det,
            Mat.Determinant(c0, b, c2) / det,
            Mat.Determinant(c0, c1, b) / det);
        var residual = a * x - b;
        return residual * residual < 1e-10;
    }

    private static bool TrySolveRankOne(Mat a, Vec b, out Vec x)
    {
        x = Vec.Zero;
        double best = 0;
        int bestCol = -1;
        for (int i = 0; i < 3; i++)
        {
            double n2 = a.Column(i) * a.Column(i);
            if (n2 > best) { best = n2; bestCol = i; }
        }
        if (bestCol < 0 || best < 1e-12) return b * b < 1e-12;
        double v = a.Column(bestCol) * b / best;
        var values = new double[3];
        values[bestCol] = v;
        x = new Vec(values[0], values[1], values[2]);
        var residual = a * x - b;
        return residual * residual < 1e-10;
    }

    /// <summary>op の幾何位置を格子同値性から全列挙し、screw 成分も各同値操作ごとに保持する。(260502Ch)</summary>
    private static IEnumerable<AxisInstance> EnumerateAxisInstances(SymmetryOperation op)
    {
        var seen = new HashSet<(long X, long Y, long Z, long TU, long TV, long TW, bool Screw)>();
        var (px, py, pz) = op.Position;
        for (int tx = 0; tx <= 1; tx++) for (int ty = 0; ty <= 1; ty++) for (int tz = 0; tz <= 1; tz++)
        {
            if (!TryCreateTranslatedAxisInstance(op, (tx, ty, tz), out var axis)) continue;
            var key = ((long)Math.Round(axis.X * 1e6), (long)Math.Round(axis.Y * 1e6), (long)Math.Round(axis.Z * 1e6),
                (long)Math.Round(axis.IntrinsicTranslation.U * 1e6), (long)Math.Round(axis.IntrinsicTranslation.V * 1e6),
                (long)Math.Round(axis.IntrinsicTranslation.W * 1e6), axis.Screw);
            if (seen.Add(key)) yield return axis;
        }

        bool TryCreateTranslatedAxisInstance(SymmetryOperation source, (double X, double Y, double Z) lattice, out AxisInstance axis)
        {
            axis = default;
            var a = BuildIMinusR(source);
            var latticeVec = new Vec(lattice.X, lattice.Y, lattice.Z); // (260502Cl)
            Vec shift, axial;
            (double U, double V, double W) rawIt;
            if (a.Rank() == 2 && source.Order > 0)
            {
                if (!TryDecomposeAxisLatticeTranslation(source, latticeVec, out shift, out axial)) return false;
                rawIt = (source.IntrinsicTranslation.U + axial.X,
                         source.IntrinsicTranslation.V + axial.Y,
                         source.IntrinsicTranslation.W + axial.Z);
            }
            else
            {
                if (!TrySolveLinear(a, latticeVec, out shift)) return false;
                rawIt = source.IntrinsicTranslation;
            }
            var it = (U: CenterMod1(rawIt.U), V: CenterMod1(rawIt.V), W: CenterMod1(rawIt.W));
            axis = new AxisInstance(Mod1(px + shift.X), Mod1(py + shift.Y), Mod1(pz + shift.Z),
                source.Direction, it, IsScrewAxis(source, rawIt));
            return true;
        }
    }

    // /// <summary>op + lattice T のシフト量。反転: T/2、2 回: T_perp/2、c 軸 3/4 回: (I−R_xy)⁻¹·T_xy。6 回は 1 位置のみ。</summary>
    // private static (double X, double Y, double Z) ComputeAxisShift(SymmetryOperation op, double tx, double ty, double tz) ...
    // (260502Ch) ComputeAxisShift の個別式は EnumerateAxisInstances の (I-R) 一般解法へ置き換えた。

    /// <summary>紙面垂直 mirror/glide の代表面を操作から取り出す。
    /// (260502Ch) T=t+L を (I−R)p + g に分解し、斜交基底でも glide 成分を保つ。</summary>
    private readonly record struct MirrorPlane(double Px, double Py, double Pz, double GlideU, double GlideV, double GlideW,
                                               (int U, int V, int W) Direction);

    /// <summary>紙面内回転軸の 1 インスタンス。(260502Ch)</summary>
    private readonly record struct AxisInstance(double X, double Y, double Z,
                                                (int U, int V, int W) Direction,
                                                (double U, double V, double W) IntrinsicTranslation,
                                                bool Screw);

    /// <summary>2 回軸を lattice translation T 込みで列挙し、T の軸方向成分を 2_1 判定へ残す。(260502Ch)</summary>
    private static IEnumerable<AxisInstance> EnumerateTwofoldAxisInstances(SymmetryOperation op)
    {
        // var (px, py, pz) = op.Position;
        // for (int tx = 0; tx <= 1; tx++) ... TryDecomposeAxisLatticeTranslation(...)
        // (260502Ch) 2 回軸も点記号と同じ一般 axis 列挙へ統一し、空間群ごとの補正分岐を持たない。
        foreach (var axis in EnumerateAxisInstances(op)) yield return axis;
    }

    /// <summary>L を (I-R) による軸位置シフトと軸方向成分へ分解する。空間群固有の見た目判定を避けるための一般処理。(260502Ch)</summary>
    private static bool TryDecomposeAxisLatticeTranslation(SymmetryOperation op, Vec lattice,
                                                           out Vec shift, out Vec axial)
    {
        shift = Vec.Zero; axial = Vec.Zero;
        var a = BuildIMinusR(op);
        if (a.Rank() != 2 || !TryFindAxisCovector(a, out var n)) return false; // (260502Ch)

        Vec d = op.Direction; // (260502Cl) implicit 変換
        double nd = n * d;
        if (Math.Abs(nd) < 1e-12) return false;
        double beta = (n * lattice) / nd;
        axial = beta * d;
        var rhs = lattice - axial;
        return TrySolveRankTwo(a, rhs, out shift);
    }

    private static bool TrySolveRankTwo(Mat a, Vec b, out Vec x)
    {
        x = Vec.Zero;
        double bestResidual = double.PositiveInfinity;
        var rhs = new[] { b.X, b.Y, b.Z };
        for (int fixedCol = 0; fixedCol < 3; fixedCol++)
        {
            var cols = Enumerable.Range(0, 3).Where(i => i != fixedCol).ToArray();
            for (int r0 = 0; r0 < 2; r0++) for (int r1 = r0 + 1; r1 < 3; r1++)
            {
                // (260502Cl) Mat[row, col] indexer を使用。
                double det = a[r0, cols[0]] * a[r1, cols[1]] - a[r0, cols[1]] * a[r1, cols[0]];
                if (Math.Abs(det) < 1e-12) continue;
                double v0 = (rhs[r0] * a[r1, cols[1]] - a[r0, cols[1]] * rhs[r1]) / det;
                double v1 = (a[r0, cols[0]] * rhs[r1] - rhs[r0] * a[r1, cols[0]]) / det;
                var candidate = new double[3];
                candidate[cols[0]] = v0;
                candidate[cols[1]] = v1;
                var cv = new Vec(candidate[0], candidate[1], candidate[2]);
                var err = a * cv - b;
                double residual = err * err;
                if (residual >= bestResidual) continue;
                bestResidual = residual;
                x = cv;
            }
        }
        return bestResidual < 1e-10;
    }

    private static IEnumerable<MirrorPlane> EnumerateMirrorPlanes(SymmetryOperation op)
    {
        // var (u, v, w) = op.Direction;
        // int n2 = u * u + v * v + w * w;
        // var (tx, ty, tz) = op.SeitzTranslation;
        // double tn = tx * u + ty * v + tz * w, tk = tn / n2;
        // double tpU = tx - tk * u, tpV = ty - tk * v, tpW = tz - tk * w;
        // (260502Ch) 旧実装は fractional 成分の n・P=c で分解していたため、hex/trigonal の斜交基底で a/b 法線の glide 成分を落としていた。
        // (260502Cl) Mat に R をまとめておくと R·t を `R * t` と書ける。op.ApplyMatrix(Vec) の Vec オーバーロードを使用。
        var R = new Mat(
            op.ApplyMatrix(new Vec(1, 0, 0)),
            op.ApplyMatrix(new Vec(0, 1, 0)),
            op.ApplyMatrix(new Vec(0, 0, 1)));
        var t0 = op.SeitzTranslation;
        var planes = new Dictionary<(long X, long Y, long Z), MirrorPlane>();

        for (int lx = -2; lx <= 2; lx++) for (int ly = -2; ly <= 2; ly++) for (int lz = -2; lz <= 2; lz++)
        {
            var t = new Vec(t0.U + lx, t0.V + ly, t0.W + lz);
            var rt = R * t;
            var n = (t - rt) * 0.5; // 平面法線方向の代表 (lattice 同値類のキー)
            var glide = (t + rt) * 0.5;
            var key = ((long)Math.Round(n.X * 1e6), (long)Math.Round(n.Y * 1e6), (long)Math.Round(n.Z * 1e6));
            var plane = new MirrorPlane(t.X / 2.0, t.Y / 2.0, t.Z / 2.0,
                CenterMod1(glide.X), CenterMod1(glide.Y), CenterMod1(glide.Z),
                op.Direction);
            if (!planes.TryGetValue(key, out var current) || GlideScore(plane) < GlideScore(current))
                planes[key] = plane;
        }

        foreach (var plane in planes.Values)
            yield return plane;

        static double GlideScore(MirrorPlane p) => Math.Abs(p.GlideU) + Math.Abs(p.GlideV) + Math.Abs(p.GlideW);
    }

    /// <summary>(260502Ch) 代表 mirror/glide 面を proper symmetry operation で写し、点群対称性で等価な面を列挙する。</summary>
    private static IEnumerable<MirrorPlane> EnumerateEquivalentMirrorPlanes(MirrorPlane seed, SymmetryOperation[] ops)
    {
        if (ops == null)
        {
            yield return seed;
            yield break;
        }

        var seen = new HashSet<(long Px, long Py, long Pz, long Gu, long Gv, long Gw, int Du, int Dv, int Dw)>();
        foreach (var op in ops)
        {
            if (op.Order <= 0) continue;
            var p = TransformPoint(op, seed.Px, seed.Py, seed.Pz);
            var g = TransformVector(op, seed.GlideU, seed.GlideV, seed.GlideW);
            var d = NormalizeDirection(TransformVector(op, seed.Direction.U, seed.Direction.V, seed.Direction.W));
            if (d == (0, 0, 0)) continue;
            var eq = new MirrorPlane(p.X, p.Y, p.Z, CenterMod1(g.X), CenterMod1(g.Y), CenterMod1(g.Z), d);
            var key = ((long)Math.Round(eq.Px * 1e6), (long)Math.Round(eq.Py * 1e6), (long)Math.Round(eq.Pz * 1e6),
                (long)Math.Round(eq.GlideU * 1e6), (long)Math.Round(eq.GlideV * 1e6), (long)Math.Round(eq.GlideW * 1e6),
                eq.Direction.U, eq.Direction.V, eq.Direction.W);
            if (seen.Add(key)) yield return eq;
        }
        // (260502Cl) TransformPoint/TransformVector/CenterMod1/NormalizeDirection/Gcd は top-level と重複していたため削除。下記の private 定義を共有。
    }

    /// <summary>代表の紙面内回転軸を空間群操作で共役し、等価な軸を列挙する。(260502Ch)</summary>
    private static IEnumerable<AxisInstance> EnumerateEquivalentAxisInstances(AxisInstance seed, SymmetryOperation[] ops)
    {
        if (ops == null)
        {
            yield return seed;
            yield break;
        }

        var seen = new HashSet<(long X, long Y, long Z, long GU, long GV, long GW, int DU, int DV, int DW, bool Screw)>();
        foreach (var op in ops)
        {
            var p = TransformPoint(op, seed.X, seed.Y, seed.Z);
            var g = TransformVector(op, seed.IntrinsicTranslation.U, seed.IntrinsicTranslation.V, seed.IntrinsicTranslation.W);
            var d = NormalizeDirection(TransformVector(op, seed.Direction.U, seed.Direction.V, seed.Direction.W));
            if (d == (0, 0, 0)) continue;
            var eq = new AxisInstance(Mod1(p.X), Mod1(p.Y), Mod1(p.Z), d,
                (CenterMod1(g.X), CenterMod1(g.Y), CenterMod1(g.Z)), seed.Screw); // (260502Ch) screw/pure の種別は共役で保存される
            var key = ((long)Math.Round(eq.X * 1e6), (long)Math.Round(eq.Y * 1e6), (long)Math.Round(eq.Z * 1e6),
                (long)Math.Round(eq.IntrinsicTranslation.U * 1e6), (long)Math.Round(eq.IntrinsicTranslation.V * 1e6),
                (long)Math.Round(eq.IntrinsicTranslation.W * 1e6), eq.Direction.U, eq.Direction.V, eq.Direction.W, eq.Screw);
            if (seen.Add(key)) yield return eq;
        }
    }

    /// <summary>(260502Cl) op を点に作用させて R·p + t を Vec で返す。SymmetryOperation.ApplyAffine を呼ぶ薄いラッパー。</summary>
    private static Vec TransformPoint(SymmetryOperation op, double x, double y, double z)
        => op.ApplyAffine(new Vec(x, y, z));

    /// <summary>(260502Cl) op の線形部 R を作用させて R·v を Vec で返す (並進無視)。</summary>
    private static Vec TransformVector(SymmetryOperation op, double x, double y, double z)
        => op.ApplyMatrix(new Vec(x, y, z));

    private static double CenterMod1(double x)
    {
        x -= Math.Round(x);
        return Math.Abs(x) < 1e-9 ? 0 : x;
    }

    private static (int U, int V, int W) NormalizeDirection(Vec d)
    {
        int u = (int)Math.Round(d.X), v = (int)Math.Round(d.Y), w = (int)Math.Round(d.Z);
        int gcd = Gcd(Gcd(Math.Abs(u), Math.Abs(v)), Math.Abs(w));
        if (gcd > 1) { u /= gcd; v /= gcd; w /= gcd; }
        if (u < 0 || (u == 0 && v < 0) || (u == 0 && v == 0 && w < 0))
            (u, v, w) = (-u, -v, -w);
        return (u, v, w);
    }

    private static int Gcd(int a, int b)
    {
        while (b != 0) (a, b) = (b, a % b);
        return a;
    }

    /// <summary>反転中心を Wyckoff position から抽出。site sym に '-' を持ち全 free=false の WP が反転中心 (centering 込み)。
    /// 同一 xy の複数 z は最小 z だけを ITC 慣用に従い表示。</summary>
    private static List<(PointF Pt, double MinZ)> CollectInversions(int seriesNumber, Projection proj, CellLayout layout)
    {
        var byKey = new Dictionary<(int, int), (double sxF, double syF, double minZ)>();
        foreach (var wp in SymmetryStatic.WyckoffPositions[seriesNumber])
        {
            if (string.IsNullOrEmpty(wp.SiteSymmetry) || !wp.SiteSymmetry.Contains('-')) continue;
            if (wp.Free.X || wp.Free.Y || wp.Free.Z) continue;
            foreach (var p in wp.GeneratePositions(0, 0, 0))
            {
                var (sx, sy, sz) = proj.ToScreen(p.X, p.Y, p.Z);
                bool nearEdge = Math.Min(sx, 1 - sx) < EdgeReplicate || Math.Min(sy, 1 - sy) < EdgeReplicate;
                for (int dx = -1; dx <= 1; dx++) for (int dy = -1; dy <= 1; dy++)
                {
                    if ((dx != 0 || dy != 0) && !nearEdge) continue;
                    double sxF = sx + dx, syF = sy + dy;
                    if (sxF < -EdgeReplicate || sxF > 1 + EdgeReplicate || syF < -EdgeReplicate || syF > 1 + EdgeReplicate) continue;
                    var key = ((int)Math.Round(sxF * 10000), (int)Math.Round(syF * 10000));
                    double mz = Mod1(sz);
                    if (!byKey.TryGetValue(key, out var cur) || mz < cur.minZ) byKey[key] = (sxF, syF, mz);
                }
            }
        }
        var list = new List<(PointF, double)>(byKey.Count);
        foreach (var (_, v) in byKey) list.Add((layout.ToScreen(v.sxF, v.syF), v.minZ));
        return list;
    }

    /// <summary>絶対次数 ≥ minOrder の proper rotation (perp) の position 集合。低次シンボル抑制用 (3/4/6 回→2 回、6 回→3 回)。</summary>
    private static HashSet<(int, int)> CollectHigherRotationPositions(SymmetryOperation[] ops, Projection proj, ProjectionAxis projAxis, int minOrder)
    {
        var set = new HashSet<(int, int)>();
        foreach (var op in ops)
        {
            int absO = Math.Abs(op.Order);
            if (op.Order < 0 || absO < minOrder || absO is not (3 or 4 or 6)) continue;
            if (!IsAxisPerpendicularToProjection(op.Direction, projAxis)) continue;
            foreach (var axis in EnumerateAxisInstances(op)) // (260502Ch)
            {
                var (sx, sy, _) = proj.ToScreen(axis.X, axis.Y, axis.Z);
                set.Add(((int)Math.Round(Mod1(sx) * 10000), (int)Math.Round(Mod1(sy) * 10000)));
            }
        }
        return set;
    }
    #endregion

    #region dispatcher
    /// <summary>軸 d が投影軸に沿うか (perp) 判定。投影軸成分のみ非零 → perp。</summary>
    private static bool IsAxisPerpendicularToProjection((int U, int V, int W) d, ProjectionAxis a) => a switch
    {
        ProjectionAxis.C => d is (0, 0, not 0),
        ProjectionAxis.A => d is (not 0, 0, 0),
        _ => d is (0, not 0, 0),
    };

    /// <summary>op を投影面上の幾何記号として描画 (lattice 同型位置も列挙)。紙面平行 mirror は parallelMirrors に集約。</summary>
    private static void DrawSymmetryElement(Graphics g, CellLayout c, Projection proj, ProjectionAxis projAxis,
                                            SymmetryOperation op, Pen pen, Pen mirrorPen, Pen inPlanePen, Pen depthPen, Pen diagPen,
                                            Brush fill, Brush white,
                                            HashSet<(double Height, bool Glide, double GlideSx, double GlideSy)> parallelMirrors,
                                            HashSet<(int, int)> covered2, HashSet<(int, int)> covered3,
                                            SymmetryOperation[] symmetryOps = null,
                                            HashSet<(long Nx, long Ny, long D, int Style)> drawnMirrorPlanes = null,
                                            HashSet<(int Edge, int Style)> boundaryMirrorEdges = null,
                                            // HashSet<(long Px, long Py, long Ux, long Uy, bool Screw, string Label)> drawnInPlaneAxisArrows = null,
                                            Dictionary<(long Px, long Py, long Ux, long Uy, bool Screw), InPlaneAxisArrowDraft> inPlaneAxisDrafts = null, // (260502Cl)
                                            bool drawOnlyPerpPointMarks = false, bool skipPerpPointMarks = false)
    {
        int o = op.Order, absO = Math.Abs(o);
        if (o == 1 || o == -1) return;                       // 反転は WP-based で別途処理、Order=1 は無視
        if (!op.Sense && (absO is 3 or 4 or 6)) return;      // Sense=false の高次回転は同軸逆冪なので skip

        bool perp = IsAxisPerpendicularToProjection(op.Direction, projAxis);
        // in-plane: 投影軸成分=0 で他成分が非零
        var d = op.Direction;
        bool inPlane = projAxis switch
        {
            ProjectionAxis.C => d.W == 0 && (d.U != 0 || d.V != 0),
            ProjectionAxis.A => d.U == 0 && (d.V != 0 || d.W != 0),
            _ => d.V == 0 && (d.U != 0 || d.W != 0),
        };
        bool isMirror = (o == -2);
        var it = op.IntrinsicTranslation;
        bool glide = Math.Abs(it.U) + Math.Abs(it.V) + Math.Abs(it.W) > 1e-6;
        bool isPointMark = !isMirror && perp && (absO is 2 or 3 or 4 or 6);

        if (drawOnlyPerpPointMarks && !isPointMark) return; // (260502Ch) 最終 pass は紙面垂直点記号だけ描く
        if (skipPerpPointMarks && isPointMark) return;      // (260502Ch) 先行 pass では線記号を点記号で隠さない

        // 紙面平行 mirror: 高さと投影面 glide のみ集約 (DrawParallelMirrorStack で一括描画)
        if (isMirror && perp)
        {
            var (_, _, opSz) = proj.ToScreen(op.Position.U, op.Position.V, op.Position.W);
            var (gSx, gSy) = ProjectVector(it.U, it.V, it.W, projAxis);
            parallelMirrors.Add((Mod1(opSz), glide, gSx, gSy));
            return;
        }

        // 紙面内 2 (2_1) 軸: lattice translation と空間群対称で等価な軸を展開して描画 (260502Ch)
        if (absO == 2 && !isMirror && inPlane)
        {
            // var byXY = new Dictionary<(double, double), double>();
            // foreach (var (X, Y, Z) in EnumerateAxisPositions(op))
            // {
            //     var (sx, sy, sz) = proj.ToScreen(X, Y, Z);
            //     var key = (Math.Round(sx, 5), Math.Round(sy, 5));
            //     double mz = Mod1(sz);
            //     if (!byXY.TryGetValue(key, out var cur) || mz < cur) byXY[key] = mz;
            // }
            foreach (var seed in EnumerateTwofoldAxisInstances(op))
            {
                foreach (var axis in EnumerateEquivalentAxisInstances(seed, symmetryOps))
                {
                    var (sx, sy, sz) = proj.ToScreen(axis.X, axis.Y, axis.Z);
                    // DrawTwofoldInPlane(g, c, sx, sy, Mod1(sz), axis.Direction, projAxis, axis.Screw, fill, drawnInPlaneAxisArrows); // (260502Ch)
                    CollectInPlaneAxisArrows(c, sx, sy, Mod1(sz), axis.Direction, projAxis, axis.Screw, inPlaneAxisDrafts); // (260502Cl) draft 集約方式に変更し最小高さを採用
                }
            }
            return;
        }

        // 紙面垂直 mirror/glide: 代表面を取り出し、proper symmetry operation の orbit として等価面を描画 (260502Ch)
        if (isMirror && inPlane)
        {
            foreach (var pl in EnumerateMirrorPlanes(op))
            {
                foreach (var eq in EnumerateEquivalentMirrorPlanes(pl, symmetryOps)) // (260502Ch) 3/4/6 回などの点群対称で等価な面も描画
                {
                    var (sx, sy, _) = proj.ToScreen(eq.Px, eq.Py, eq.Pz);
                    DrawMirrorPerpToScreen(g, c, sx, sy, eq.Direction,
                        (eq.GlideU, eq.GlideV, eq.GlideW), proj, mirrorPen, inPlanePen, depthPen, diagPen,
                        drawnMirrorPlanes, boundaryMirrorEdges); // (260502Ch)
                }
            }
            return;
        }

        if (!isPointMark) return;
        foreach (var axis in EnumerateAxisInstances(op)) // (260502Ch)
        {
            var (sx, sy, _) = proj.ToScreen(axis.X, axis.Y, axis.Z);
            // 同位置の高次回転で低次シンボル抑制
            var key = ((int)Math.Round(Mod1(sx) * 10000), (int)Math.Round(Mod1(sy) * 10000));
            if (absO == 2 && covered2.Contains(key)) continue;
            if (absO == 3 && o > 0 && covered3.Contains(key)) continue;
            bool nearEdge = Math.Min(sx, 1 - sx) < EdgeReplicate || Math.Min(sy, 1 - sy) < EdgeReplicate;
            for (int dx = -1; dx <= 1; dx++) for (int dy = -1; dy <= 1; dy++)
            {
                if ((dx != 0 || dy != 0) && !nearEdge) continue;
                double dxf = sx + dx, dyf = sy + dy;
                if (dxf < -EdgeReplicate || dxf > 1 + EdgeReplicate || dyf < -EdgeReplicate || dyf > 1 + EdgeReplicate) continue;
                var pt = c.ToScreen(dxf, dyf);
                var (finCount, edgeStep) = ScrewParams(op, axis.IntrinsicTranslation); // (260502Ch)
                if (absO == 2) DrawTwofoldPerp(g, fill, pt, axis.Screw);
                else if (absO == 3) DrawRotationPerp(g, fill, white, pt, o, finCount, edgeStep, 3, ThreeFoldRadius);
                else if (absO == 4) DrawRotationPerp(g, fill, white, pt, o, finCount, edgeStep, 4, FourFoldRadius);
                else if (absO == 6) DrawRotationPerp(g, fill, white, pt, o, finCount, edgeStep, 6, SixFoldRadius);
            }
        }
    }

    // (260502Cl) EdgeReplicateInPlaneAxis は未呼出のため削除。代わりに CollectInPlaneAxisArrows 内の 5x5 セル展開で境界軸を含めて拾う。
    #endregion

    #region 個別シンボル描画
    /// <summary>反転中心を白丸で描画、z!=0 で高さラベルを併記。</summary>
    private static void DrawInversions(Graphics g, List<(PointF Pt, double MinZ)> inversions, Pen pen, Brush white, Brush fill)
    {
        if (inversions.Count == 0) return;
        // (260502Cl) Common.HeightLabelFont を共有使用。
        foreach (var (pt, z) in inversions)
        {
            g.FillEllipse(white, pt.X - InversionR, pt.Y - InversionR, 2 * InversionR, 2 * InversionR);
            g.DrawEllipse(pen, pt.X - InversionR, pt.Y - InversionR, 2 * InversionR, 2 * InversionR);
            string h = HeightLabel(z);
            if (h == null) continue;
            g.DrawString(h, HeightLabelFont, fill, pt.X + InversionR + 1, pt.Y - InversionR - g.MeasureString(h, HeightLabelFont).Height + 2);
        }
    }

    /// <summary>紙面垂直 2 (2_1) 軸: vesica piscis lens (同径 2 円の重なり) を塗り潰し。screw=互い違い円弧。scale は -4 から呼ぶ際の縮小率。</summary>
    private static void DrawTwofoldPerp(Graphics g, Brush fill, PointF pt, bool screw, float scale = 1f)
    {
        float halfW = TwofoldHalfW * scale, halfH = TwofoldHalfH * scale;
        float r = (halfW * halfW + halfH * halfH) / (2 * halfW), d = r - halfW;
        float halfAngle = (float)(Math.Atan2(halfH, d) * 180.0 / Math.PI);
        var rightRect = new RectangleF(pt.X + d - r, pt.Y - r, 2 * r, 2 * r);
        var leftRect  = new RectangleF(pt.X - d - r, pt.Y - r, 2 * r, 2 * r);
        using (var path = new GraphicsPath())
        {
            path.AddArc(rightRect, 180f + halfAngle, -2 * halfAngle);
            path.AddArc(leftRect, halfAngle, -2 * halfAngle);
            path.CloseFigure();
            using (var halo = new Pen(Color.White, SymbolHaloPenWidth) { LineJoin = LineJoin.Round }) // (260502Ch)
                g.DrawPath(halo, path);
            g.FillPath(fill, path);
        }
        if (screw)
        {
            using var pen = new Pen(Color.Black, ScrewFinPenWidth);
            g.DrawArc(pen, rightRect, 180f + halfAngle, ScrewFinSweepDeg);
            g.DrawArc(pen, leftRect, halfAngle, ScrewFinSweepDeg);
        }
    }

    /// <summary>中心 c から半径 r の正 N 角形 (頂点 0 を真上)。3/4/6 回回転シンボル共通。</summary>
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

    /// <summary>n_k 螺旋の指示棒。頂点 (j*placeStep)%N から edge (i−edgeStep)→i 方向に延長。placeStep=N/finCount で等間隔配置。</summary>
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

    /// <summary>紙面垂直 3/4/6 回回転 + 反転 (-N) 共通描画。輪郭はフィンと同じ太さ。-3=黒塗り+中心白丸、-4=白塗り+lens、-6=白塗り+内部三角形。</summary>
    private static void DrawRotationPerp(Graphics g, Brush fill, Brush white, PointF pt, int order, int finCount, int edgeStep, int N, float radius)
    {
        var poly = RegularPolygon(pt, N, radius);
        // using var outline = new Pen(Color.Black, 1.8f);
        // (260502Ch) 多角形の白い輪郭を先に敷き、その後に本体と screw fin を描く。
        using var halo = new Pen(Color.White, SymbolHaloPenWidth) { LineJoin = LineJoin.Round };
        using var outline = new Pen(Color.Black, OutlinePenWidth) { LineJoin = LineJoin.Round };
        g.DrawPolygon(halo, poly);
        if (order > 0)
        {
            g.FillPolygon(fill, poly);
            g.DrawPolygon(outline, poly);
            DrawScrewFins(g, outline, poly, finCount, edgeStep, tailLen: ScrewFinTailLen);
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
        else g.FillPolygon(fill, RegularPolygon(pt, 3, radius)); // -6: 三角形を六角形に内接
        g.DrawPolygon(outline, poly);
    }

    // (260502Cl) 紙面内 2(2_1) 軸の矢印 draft。後段の DrawCollectedInPlaneAxisArrows で実描画。
    private readonly record struct InPlaneAxisArrowDraft(PointF Anchor, double OutUx, double OutUy, bool Screw, double Sz);

    /// <summary>(260502Cl) 紙面内 2(2_1) 軸の矢印を draft に集約。同一 (位置, 方向, screw) で複数高さがあれば最小 sz を残す。</summary>
    private static void CollectInPlaneAxisArrows(CellLayout c, double sx, double sy, double sz,
                                                  (int U, int V, int W) dir, ProjectionAxis projAxis, bool screw,
                                                  Dictionary<(long Px, long Py, long Ux, long Uy, bool Screw), InPlaneAxisArrowDraft> drafts)
    {
        if (drafts == null) return;
        var (dSx, dSy) = ProjectVector(dir.U, dir.V, dir.W, projAxis);
        double axisX = dSx * c.Horz.X + dSy * c.Vert.X, axisY = dSx * c.Horz.Y + dSy * c.Vert.Y;
        double axisLen = Math.Sqrt(axisX * axisX + axisY * axisY);
        if (axisLen < 1e-6) return;
        double ux = axisX / axisLen, uy = axisY / axisLen;

        for (int ox = -2; ox <= 2; ox++) for (int oy = -2; oy <= 2; oy++) // (260502Ch) 角に接する隣接セル由来の 2 軸も拾う
            ClipAxisArrows(sx + ox, sy + oy);

        void ClipAxisArrows(double lineSx, double lineSy)
        {
            if (!TryClip(lineSx, lineSy, dSx, dSy, out double tMin, out double tMax)) return;
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
            if (drafts.TryGetValue(key, out var existing) && existing.Sz <= sz) return; // (260502Cl) 既存が同等以下なら据え置き
            drafts[key] = new InPlaneAxisArrowDraft(p, outUx, outUy, screw, sz);
        }

        static bool TryClip(double x, double y, double dx, double dy, out double tMin, out double tMax)
        {
            tMin = double.NegativeInfinity; tMax = double.PositiveInfinity;
            UpdateInterval(x, dx, ref tMin, ref tMax);
            UpdateInterval(y, dy, ref tMin, ref tMax);
            return tMin <= tMax;

            static void UpdateInterval(double s, double d, ref double tMin, ref double tMax)
            {
                if (Math.Abs(d) < 1e-9) { if (s < -1e-9 || s > 1 + 1e-9) tMin = 1; return; }
                double t1 = -s / d, t2 = (1 - s) / d;
                if (t1 > t2) (t1, t2) = (t2, t1);
                if (t1 > tMin) tMin = t1;
                if (t2 < tMax) tMax = t2;
            }
        }

        static bool InsideCell(double x, double y) => x > 1e-6 && x < 1 - 1e-6 && y > 1e-6 && y < 1 - 1e-6;
        static bool OnCellBoundary(double x, double y) => x > -1e-6 && x < 1 + 1e-6 && y > -1e-6 && y < 1 + 1e-6 &&
                                                          (Math.Abs(x) < 1e-6 || Math.Abs(x - 1) < 1e-6 || Math.Abs(y) < 1e-6 || Math.Abs(y - 1) < 1e-6);
        static double NormalizeBoundary(double x)
        {
            if (Math.Abs(x) < 1e-6) return 0;
            if (Math.Abs(x - 1) < 1e-6) return 1;
            return x;
        }
    }

    /// <summary>(260502Cl) draft 化された紙面内 2(2_1) 軸矢印を描画。最小高さ採用済み。</summary>
    private static void DrawCollectedInPlaneAxisArrows(Graphics g, Brush fill,
        Dictionary<(long Px, long Py, long Ux, long Uy, bool Screw), InPlaneAxisArrowDraft> drafts)
    {
        if (drafts == null || drafts.Count == 0) return;
        const float arrowExt = InPlaneArrowExt; // (260502Cl) クラス冒頭の定数を使用
        using var pen = new Pen(Color.Black, InPlaneAxisPenWidth);
        // (260502Cl) Common.HeightLabelFont を共有使用。
        using var brush = new SolidBrush(Color.Black);
        foreach (var d in drafts.Values)
        {
            var tip = new PointF((float)(d.Anchor.X + arrowExt * d.OutUx), (float)(d.Anchor.Y + arrowExt * d.OutUy));
            g.DrawLine(pen, d.Anchor, tip);
            DrawArrowhead(g, fill, tip, d.OutUx, d.OutUy, halfHead: d.Screw);
            string h = HeightLabel(d.Sz);
            if (h == null) continue;
            var lbl = g.MeasureString(h, HeightLabelFont);
            bool horiz = Math.Abs(d.OutUy) < 0.1;
            float lx = horiz ? tip.X - lbl.Width / 2 : tip.X - lbl.Width - 2;
            float ly = horiz ? tip.Y + 3 : tip.Y - lbl.Height / 2;
            g.DrawString(h, HeightLabelFont, brush, lx, ly);
        }
    }

    /// <summary>tip 位置に向き (ux, uy) の矢頭を描画。halfHead=true で screw 用の半欠け矢頭。</summary>
    private static void DrawArrowhead(Graphics g, Brush fill, PointF tip, double ux, double uy, bool halfHead)
    {
        const float aLen = ArrowHeadLen, aH = ArrowHeadHalfWidth; // (260502Cl) クラス冒頭の定数を使用
        float bx = (float)(tip.X - aLen * ux), by = (float)(tip.Y - aLen * uy);
        PointF left = new((float)(bx - aH * uy), (float)(by + aH * ux));
        PointF right = new((float)(bx + aH * uy), (float)(by - aH * ux));
        g.FillPolygon(fill, halfHead ? [tip, new PointF(bx, by), left] : [tip, left, right]);
    }

    /// <summary>紙面平行 mirror を IUCR corner bracket (左上外側) で描画。腕はセル基底軸と平行 (β 角追従)。映進面は対角線矢印追加、高さラベル併記。</summary>
    private static void DrawParallelMirrorStack(Graphics g, CellLayout c, HashSet<(double Height, bool Glide, double GlideSx, double GlideSy)> markers, Brush fill)
    {
        if (markers.Count == 0) return;
        const float armLen = CornerBracketArmLen, gap = CornerBracketGap, offset = armLen + gap; // (260502Cl) クラス冒頭の定数を使用
        double hLen = Math.Sqrt(c.Horz.X * c.Horz.X + c.Horz.Y * c.Horz.Y);
        double vLen = Math.Sqrt(c.Vert.X * c.Vert.X + c.Vert.Y * c.Vert.Y);
        if (hLen < 1e-3 || vLen < 1e-3) return;
        float hUx = (float)(c.Horz.X / hLen), hUy = (float)(c.Horz.Y / hLen);
        float vUx = (float)(c.Vert.X / vLen), vUy = (float)(c.Vert.Y / vLen);
        var apex = new PointF(c.TopLeft.X - offset * (hUx + vUx), c.TopLeft.Y - offset * (hUy + vUy));
        using var pen = new Pen(Color.Black, CornerBracketPenWidth);
        g.DrawLine(pen, apex, new PointF(apex.X + armLen * hUx, apex.Y + armLen * hUy));
        g.DrawLine(pen, apex, new PointF(apex.X + armLen * vUx, apex.Y + armLen * vUy));

        var gm = markers.FirstOrDefault(m => Math.Abs(m.GlideSx) > 1e-3 || Math.Abs(m.GlideSy) > 1e-3);
        if (gm.GlideSx != 0 || gm.GlideSy != 0)
        {
            float dx = (float)(gm.GlideSx * c.Horz.X + gm.GlideSy * c.Vert.X);
            float dy = (float)(gm.GlideSx * c.Horz.Y + gm.GlideSy * c.Vert.Y);
            double dlen = Math.Sqrt(dx * dx + dy * dy);
            if (dlen > 0.5)
            {
                float arrLen = armLen * GlideArrowScale;
                var end = new PointF((float)(apex.X + dx * arrLen / dlen), (float)(apex.Y + dy * arrLen / dlen));
                g.DrawLine(pen, apex, end);
                DrawArrowhead(g, fill, end, dx / dlen, dy / dlen, halfHead: false);
            }
        }
        var labels = markers.Select(m => m.Height).Distinct().OrderBy(h => h).Select(HeightLabel).Where(s => s != null).ToList();
        if (labels.Count == 0) return;
        // (260502Cl) Common.HeightLabelFont を共有使用。
        using var brush = new SolidBrush(Color.Black);
        string lbl = string.Join(", ", labels);
        var ls = g.MeasureString(lbl, HeightLabelFont);
        g.DrawString(lbl, HeightLabelFont, brush, apex.X - ls.Width - 2, apex.Y - ls.Height / 2);
    }

    /// <summary>紙面垂直 mirror/glide を法線直交直線で描画。IT 分解で線種選択 (純=実線、a/b-glide=長破線、c-glide=短点線、n-glide=dash-dot)。
    /// 直交を CellLayout (実空間 cartesian) で計算するので hex 120° / mono 105° の非直交セルでも正しい角度になる。</summary>
    private static void DrawMirrorPerpToScreen(Graphics g, CellLayout c, double sx, double sy,
                                               (int U, int V, int W) dir, (double U, double V, double W) it,
                                               Projection proj, Pen mirrorPen, Pen inPlanePen, Pen depthPen, Pen diagPen,
                                               HashSet<(long Nx, long Ny, long D, int Style)> drawnMirrorPlanes = null,
                                               HashSet<(int Edge, int Style)> boundaryMirrorEdges = null)
    {
        // n を fractional (dSx, dSy) → cartesian (n_X, n_Y) に変換、90° 回転で perpendicular_cart、fractional に戻す。
        var (dSx, dSy) = ProjectVector(dir.U, dir.V, dir.W, proj.Axis);
        double nX = dSx * c.Horz.X + dSy * c.Vert.X, nY = dSx * c.Horz.Y + dSy * c.Vert.Y;
        double pX = -nY, pY = nX;
        double det = c.Horz.X * c.Vert.Y - c.Vert.X * c.Horz.Y;
        if (Math.Abs(det) < 1e-9) return;
        double perpSx = (c.Vert.Y * pX - c.Vert.X * pY) / det;
        double perpSy = (-c.Horz.Y * pX + c.Horz.X * pY) / det;
        const double edgeLineEps = 1e-6; // (260502Ch) 斜交セルの丸め誤差込みで境界平行を判定
        // (260502Cl) hex/trig で cos(120°) の精度誤差により perpS* が 1e-8 オーダーになる。SpanLineThroughCell の
        // 1e-9 閾値では非零扱いとなり微小 d で除算して縮退線を返し、隣接辺の dedup key を先取りしてしまうため 0 へ丸める。
        if (Math.Abs(perpSx) < edgeLineEps) perpSx = 0;
        if (Math.Abs(perpSy) < edgeLineEps) perpSy = 0;
        var (gSx, gSy, gSz) = proj.ToScreen(it.U, it.V, it.W);
        bool hasInPlane = Math.Abs(gSx) > 1e-3 || Math.Abs(gSy) > 1e-3;
        bool hasDepth = Math.Abs(gSz) > 1e-3;
        int style = (hasInPlane, hasDepth) switch
        {
            (false, false) => 0, (true, false) => 1, (false, true) => 2, _ => 3,
        };
        Pen pen = style switch
        {
            0 => mirrorPen, 1 => inPlanePen, 2 => depthPen, _ => diagPen,
        };
        DrawClippedMirrorLine(sx, sy);
        // (260502Ch) 境界上の mirror/glide は単位胞の対辺にも同じ対称要素として表示する。
        // (260502Cl) 旧実装は sx+1 / sx-1 にずらして再描画していたが、NormalizeCellBoundary が s=1 を 0 に折り畳むため右辺/下辺が drawnMirrorPlanes の dedup で落ちていた。
        // 直接 lineSx=0 と lineSx=1 (および lineSy=0 と lineSy=1) を渡し、両辺に明示的に描画する。
        if (perpSx == 0)
        {
            double normSx = NormalizeCellBoundary(sx); // (260502Cl)
            if (normSx < EdgeReplicate || 1 - normSx < EdgeReplicate)
            {
                boundaryMirrorEdges?.Add((0, style)); // (260502Ch)
                boundaryMirrorEdges?.Add((1, style)); // (260502Ch)
                DrawClippedMirrorLine(0, sy); // (260502Cl) 左辺
                DrawClippedMirrorLine(1, sy); // (260502Cl) 右辺
            }
        }
        if (perpSy == 0)
        {
            double normSy = NormalizeCellBoundary(sy); // (260502Cl)
            if (normSy < EdgeReplicate || 1 - normSy < EdgeReplicate)
            {
                boundaryMirrorEdges?.Add((2, style)); // (260502Ch)
                boundaryMirrorEdges?.Add((3, style)); // (260502Ch)
                DrawClippedMirrorLine(sx, 0); // (260502Cl) 上辺
                DrawClippedMirrorLine(sx, 1); // (260502Cl) 下辺
            }
        }

        void DrawClippedMirrorLine(double lineSx, double lineSy)
        {
            lineSx = NormalizeCellBoundary(lineSx); // (260502Ch) 1.0000000002 などの境界丸め誤差で右/下辺を落とさない
            lineSy = NormalizeCellBoundary(lineSy); // (260502Ch)
            var (start, end) = SpanLineThroughCell(c, lineSx, lineSy, perpSx, perpSy);
            if (!start.HasValue || !end.HasValue) return;
            double nLen = Math.Sqrt(nX * nX + nY * nY);
            if (nLen < 1e-9) return;
            if (drawnMirrorPlanes != null)
            {
                double ux = nX / nLen, uy = nY / nLen;
                if (ux < -1e-9 || (Math.Abs(ux) < 1e-9 && uy < 0)) { ux = -ux; uy = -uy; }
                var pt = c.ToScreen(lineSx, lineSy);
                var key = ((long)Math.Round(ux * 1e6), (long)Math.Round(uy * 1e6), (long)Math.Round((ux * pt.X + uy * pt.Y) * 1000), style);
                if (!drawnMirrorPlanes.Add(key)) return;
            }
            g.DrawLine(pen, start.Value, end.Value);
        }

        static double NormalizeCellBoundary(double s)
        {
            // if (Math.Abs(s) < 1e-8) return 0;
            // if (Math.Abs(s - 1) < 1e-8) return 1;
            // return s;
            // (260502Ch) 対称操作で写した代表点が -1 や 2 でも、周期同値な単位胞内の線として扱う。
            // (260502Cl) ただし s≈1 (右辺/下辺) は 0 に折り畳まず 1 のまま残す。drawnMirrorPlanes の dedup キーが
            // 左辺と右辺で別になり、両辺それぞれに描画できるようにするため。
            if (Math.Abs(s - 1) < 1e-8) return 1;
            double m = s - Math.Floor(s);
            if (m < 1e-8) return 0;
            if (m > 1 - 1e-8) return 1; // (260502Cl) s=2.0000002 のような誤差付き右辺も 1 として保つ
            return m;
        }
    }

    #endregion
}
