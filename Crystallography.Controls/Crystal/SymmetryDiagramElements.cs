// 260501Cl: 対称要素 (左図) を ITC Vol.A 風に GDI+ 描画。
// 反転中心 / 紙面垂直 2(2_1) 軸 / 紙面内 2(2_1) 軸 / 紙面垂直 mirror/glide / 紙面平行 mirror をサポート。
// 260502Cl: ロジック温存のままデッドコード除去・引数統合・ヘルパー集約でスリム化。
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Vec = Crystallography.Vector3DBase;
using Mat = Crystallography.Matrix3D;

namespace Crystallography.Controls;

public class SymmetryDiagramElements : SymmetryDiagramCommon
{
    #region 定数 (単位は全て pixel。ITC Vol.A 慣用)
    // 線幅
    private const float DefaultPenWidth       = 1.2f;  // セル枠線・反転中心 ○
    private const float MirrorPenWidth        = 1.8f;  // 紙面垂直な mirror/glide の線幅
    private const float OutlinePenWidth       = 1.2f;  // 多角形輪郭・screw fin
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

    // 反転中心
    private const float InversionR = 2.5f;

    // 紙面内 2(2_1) 矢印
    private const float InPlaneArrowExt    = 32f;
    private const float ArrowHeadLen       = 7f;
    private const float ArrowHeadHalfWidth = 3f;
    private const float DGlideDotR         = 1.7f; // (260502Ch) 紙面垂直 d-glide の dot 半径。
    private const float DGlidePatternPitch = 64f;  // (260502Ch) dot-dash-dot-dash-dot-arrow 反復間隔。
    private const float EGlideDotDashUnit  = 2.6f;  // (260502Ch) DashPattern で dot-dot-dash の間隔を作る基準。

    // 紙面平行 mirror corner bracket
    private const float CornerBracketArmLen = 22f;
    private const float CornerBracketGap    = 45f; // (260502Cl) bracket を単位胞頂点からより離す (旧 22f)
    private const float CornerBracketStep   = 8f; // (260502Ch) 2 つ目以降の bracket をセル基底軸方向へずらす距離。
    private const float GlideArrowLineShorten = 5f; // (260502Ch) 映進矢印の線を矢頭より手前で止め、先端をつぶさない。
    #endregion

    #region context
    /// <summary>(260502Cl) DrawSymmetryElement の引数群を集約。Pen/Brush/HashSet を 1 回作って全 op で共有する。</summary>
    private sealed class ElementsContext
    {
        public Graphics G;
        public CellLayout C;
        public Projection Proj;
        public SymmetryOperation[] Ops;
        public List<(double U, double V, double W)> Centerings; // (260502Cl) R-/F-/I-centering 等の格子並進ベクトル。EnumerateMirrorPlanes が glide 縮約に用いる。
        public Pen Pen, MirrorPen, InPlanePen, DepthPen, DiagPen, EPen;
        public Brush Fill, White;
        public HashSet<(double Height, bool Glide, double GlideSx, double GlideSy)> ParallelMirrors;
        public List<PerpendicularMirrorDraft> PerpendicularMirrors;
        public HashSet<(int, int)> Covered2, Covered3;
        public HashSet<(int N, int Sx10000, int Sy10000)> ProperRotations; // (260502Cl) -N 抑制用 (proper N が同位置にあれば -N を描かない)
        public HashSet<(long Nx, long Ny, long D, int Style)> DrawnMirrorPlanes;
        public Dictionary<(long Px, long Py, long Ux, long Uy, bool Screw), InPlaneAxisArrowDraft> InPlaneAxisDrafts;
    }
    #endregion

    #region 公開 API
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

            var ops = ExpandWithCentering(SymmetryStatic.WyckoffPositions[seriesNumber][0].PositionOperations, seriesNumber);
            if (ops == null) return bmp;

            using var pen        = new Pen(Color.Black, DefaultPenWidth);
            using var mirrorPen  = new Pen(Color.Black, MirrorPenWidth);
            using var inPlanePen = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [5f, 3f] };
            using var depthPen   = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [1f, 2.5f] };
            using var diagPen    = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [5f, 2.5f, 1f, 2.5f] };
            using var ePen       = new Pen(Color.Black, MirrorPenWidth) // (260502Ch) e-glide: dot-dot-dash は DashPattern で表現。
            {
                DashStyle = DashStyle.Custom,
                DashCap = DashCap.Round,
                DashPattern = [0.1f, EGlideDotDashUnit, 0.1f, EGlideDotDashUnit, 5.0f, EGlideDotDashUnit]
            };
            using var fill  = new SolidBrush(Color.Black);
            using var white = new SolidBrush(Color.White);

            // (260502Cl) R-/F-/I-centering 等の格子並進ベクトル。Order=1 かつ非ゼロ Position の op が centering 並進を持つ。
            var centerings = ops
                .Where(o => o.Order == 1 && Math.Abs(o.Position.U) + Math.Abs(o.Position.V) + Math.Abs(o.Position.W) > 1e-6)
                .Select(o => (o.Position.U, o.Position.V, o.Position.W))
                .Distinct()
                .ToList();

            var ctx = new ElementsContext
            {
                G = g, C = layout, Proj = proj, Ops = ops,
                Centerings = centerings,
                Pen = pen, MirrorPen = mirrorPen, InPlanePen = inPlanePen, DepthPen = depthPen, DiagPen = diagPen, EPen = ePen,
                Fill = fill, White = white,
                ParallelMirrors = [],
                PerpendicularMirrors = [], // (260502Ch) e-glide 判定のため、紙面垂直 mirror/glide も一度集約する。
                Covered2 = CollectHigherRotationPositions(ops, proj, actualAxis, 3),
                Covered3 = CollectHigherRotationPositions(ops, proj, actualAxis, 6),
                ProperRotations = CollectProperRotationPositions(ops, proj, actualAxis), // (260502Cl)
                DrawnMirrorPlanes = [],
                InPlaneAxisDrafts = [],
            };

            // pass1: 線/面/紙面内軸 → 紙面平行 mirror 集約 → 紙面内 2 軸描画 → pass2: 紙面垂直点記号 → 反転中心 (260502Cl: 反転中心は最後の pass で halo 付き)
            foreach (var op in ops) DrawSymmetryElement(ctx, op, skipPerpPointMarks: true);
            DrawCollectedPerpendicularMirrorPlanes(ctx); // (260502Ch) 紙面垂直 e-glide を紙面平行 e-glide と同じ double-glide ロジックで確定してから描画。
            DrawParallelMirrorStack(g, layout, ctx.ParallelMirrors, fill);
            DrawCollectedInPlaneAxisArrows(g, fill, ctx.InPlaneAxisDrafts);
            foreach (var op in ops) DrawSymmetryElement(ctx, op, drawOnlyPerpPointMarks: true);
            DrawInversions(g, CollectInversions(seriesNumber, proj, layout), pen, white, fill);
        }
        finally { g.Dispose(); }
        return bmp;
    }
    #endregion

    #region centering 展開と axis 列挙
    /// <summary>R-lattice 等の centering を runtime で展開し、(I-R) 線形分解で全方向の等価操作を生成する。</summary>
    private static SymmetryOperation[] ExpandWithCentering(SymmetryOperation[] baseOps, int seriesNumber)
    {
        if (baseOps == null) return null;
        // (260502Cl) series-aware 化を内側で 1 回だけ実施 (旧実装は外側で再ラップしていた)
        var ops = baseOps.Select(op => new SymmetryOperation(op, seriesNumber)).ToArray();
        var cents = new List<(double U, double V, double W)>();
        foreach (var op in ops)
        {
            if (op.Order != 1) continue;
            var (cu, cv, cw) = op.Position;
            if (Math.Abs(cu) + Math.Abs(cv) + Math.Abs(cw) > 1e-6) cents.Add((cu, cv, cw));
        }
        if (cents.Count == 0) return ops;

        var result = new List<SymmetryOperation>(ops.Length * (cents.Count + 1));
        var seen = new HashSet<(int, bool, int, int, int, long, long, long, long, long, long)>();

        bool TryAdd(SymmetryOperation op)
        {
            var key = (op.Order, op.Sense, op.Direction.U, op.Direction.V, op.Direction.W,
                R6(Mod1(op.Position.U)), R6(Mod1(op.Position.V)), R6(Mod1(op.Position.W)),
                R6(CenterMod1(op.IntrinsicTranslation.U)), R6(CenterMod1(op.IntrinsicTranslation.V)),
                R6(CenterMod1(op.IntrinsicTranslation.W)));
            if (!seen.Add(key)) return false;
            result.Add(op);
            return true;
        }

        foreach (var op in ops) TryAdd(op);
        foreach (var op in ops)
            foreach (var c in cents)
                if (TryCreateCenteredOperation(op, c, out var centered))
                    TryAdd(new SymmetryOperation(centered, seriesNumber));
        return result.ToArray();
    }

    /// <summary>(260502Cl) Math.Round(x * 1e6) を long 化する dedup キー用ヘルパー。</summary>
    private static long R6(double x) => (long)Math.Round(x * 1e6);

    /// <summary>n_k 螺旋の (FinCount, EdgeStep)。gcd(N,k)>1 (4_2/6_2/6_3/6_4) は ITC 規約に従い fin 数を減らした特例形に。</summary>
    private static (int FinCount, int EdgeStep) PinwheelFins(int N, int k) => (N, k) switch
    {
        (_, 0) => (0, 0),
        (4, 2) => (2, 1), // 4_2
        (6, 2) => (3, 5), // 6_2
        (6, 3) => (2, 1), // 6_3
        (6, 4) => (3, 1), // 6_4
        _ => (N, N - k),
    };

    /// <summary>op の intrinsic translation 軸方向成分から (FinCount, EdgeStep) を導出。</summary>
    private static (int FinCount, int EdgeStep) ScrewParams(SymmetryOperation op, (double U, double V, double W)? intrinsicTranslation = null)
    {
        int N = Math.Abs(op.Order);
        if (N < 2) return (0, 0);
        if (!TryGetAxisFraction(op, intrinsicTranslation ?? op.IntrinsicTranslation, out double along)) return (0, 0);
        if (along < 1e-3 || along > 1 - 1e-3) return (0, 0);
        int k = ((int)Math.Round(along * N)) % N;
        return PinwheelFins(N, k);
    }

    private static bool IsScrewAxis(SymmetryOperation op, (double U, double V, double W) intrinsicTranslation)
        => TryGetAxisFraction(op, intrinsicTranslation, out double along) &&
           Math.Abs(along) > 1e-3 && Math.Abs(Math.Abs(along) - 1) > 1e-3;

    private static bool TryGetAxisFraction(SymmetryOperation op, (double U, double V, double W) translation, out double along)
    {
        along = 0;
        var a = BuildIMinusR(op);
        if (a.Rank() != 2 || !TryFindAxisCovector(a, out var n)) return false;
        Vec d = op.Direction;
        double nd = n * d;
        if (Math.Abs(nd) < 1e-12) return false;
        along = Mod1(n * (Vec)translation / nd);
        return true;
    }

    private static bool TryCreateCenteredOperation(SymmetryOperation op, (double U, double V, double W) centering, out SymmetryOperation centered)
    {
        centered = default;
        if (op.Order == 1) return false;
        if (Math.Abs(centering.U) + Math.Abs(centering.V) + Math.Abs(centering.W) < 1e-9) return false;
        var a = BuildIMinusR(op);
        Vec lattice = centering;
        Vec shift, residual;
        bool ok;
        if (a.Rank() == 2 && op.Order > 0)
            ok = TryDecomposeAxisLatticeTranslation(op, lattice, out shift, out residual);
        else if (op.Order == -2)
            ok = TryDecomposeMirrorLatticeTranslation(op, lattice, out shift, out residual);
        else
        {
            //ok = TrySolveLinear(a, lattice, out shift);
            //residual = lattice - a * shift;
            // (260502Ch) mirror/glide は rank 1 で面内 glide 成分が残るため、上の専用分解で処理する。
            ok = TrySolveLinear(a, lattice, out shift);
            residual = lattice - a * shift;
        }
        if (!ok) return false;
        var p = op.Position;
        var it = op.IntrinsicTranslation;
        centered = new SymmetryOperation(op.Order, op.Sense ? 1 : -1, op.Direction,
            (Mod1(p.U + shift.X), Mod1(p.V + shift.Y), Mod1(p.W + shift.Z)),
            (CenterMod1(it.U + residual.X), CenterMod1(it.V + residual.Y), CenterMod1(it.W + residual.Z)));
        return true;
    }

    /// <summary>(260502Ch) mirror/glide に centering を足すと、法線方向は面位置のシフト、面内成分は glide として残る。</summary>
    private static bool TryDecomposeMirrorLatticeTranslation(SymmetryOperation op, Vec lattice, out Vec shift, out Vec residual)
    {
        var r = op.ApplyMatrix(lattice);
        shift = new Vec(lattice.X * 0.5, lattice.Y * 0.5, lattice.Z * 0.5);
        residual = new Vec((lattice.X + r.X) * 0.5, (lattice.Y + r.Y) * 0.5, (lattice.Z + r.Z) * 0.5);
        var err = BuildIMinusR(op) * shift + residual - lattice;
        return err * err < 1e-10;
    }

    /// <summary>I - R を Mat で返す。R は op の線形部 (回転 / 回反)。</summary>
    private static Mat BuildIMinusR(SymmetryOperation op) => new(
        new Vec(1, 0, 0) - op.ApplyMatrix(new Vec(1, 0, 0)),
        new Vec(0, 1, 0) - op.ApplyMatrix(new Vec(0, 1, 0)),
        new Vec(0, 0, 1) - op.ApplyMatrix(new Vec(0, 0, 1)));

    private static bool TryFindAxisCovector(Mat a, out Vec n)
    {
        var (c0, c1, c2) = (a.Column(0), a.Column(1), a.Column(2));
        n = c0.Cross(c1); double n2 = n * n;
        var cand = c0.Cross(c2); double cn2 = cand * cand;
        if (cn2 > n2) { n = cand; n2 = cn2; }
        cand = c1.Cross(c2); cn2 = cand * cand;
        if (cn2 > n2) { n = cand; n2 = cn2; }
        return n2 > 1e-12;
    }

    private static bool TrySolveLinear(Mat a, Vec b, out Vec x)
    {
        x = Vec.Zero;
        int rank = a.Rank();
        if (rank == 0) return b * b < 1e-12;
        if (rank == 2) return TrySolveRankTwo(a, b, out x);
        if (rank == 1)
        {
            int best = -1; double bestN2 = 0;
            for (int i = 0; i < 3; i++)
            {
                double n2 = a.Column(i) * a.Column(i);
                if (n2 > bestN2) { bestN2 = n2; best = i; }
            }
            if (best < 0) return b * b < 1e-12;
            double v = a.Column(best) * b / bestN2;
            Span<double> values = stackalloc double[3]; values[best] = v;
            x = new Vec(values[0], values[1], values[2]);
            var r = a * x - b;
            return r * r < 1e-10;
        }
        // rank == 3 → Cramer
        var (cc0, cc1, cc2) = (a.Column(0), a.Column(1), a.Column(2));
        double det = a.Determinant();
        if (Math.Abs(det) < 1e-12) return false;
        x = new Vec(Mat.Determinant(b, cc1, cc2) / det,
                    Mat.Determinant(cc0, b, cc2) / det,
                    Mat.Determinant(cc0, cc1, b) / det);
        var rr = a * x - b;
        return rr * rr < 1e-10;
    }

    private static bool TrySolveRankTwo(Mat a, Vec b, out Vec x)
    {
        x = Vec.Zero;
        double bestResidual = double.PositiveInfinity;
        Span<double> rhs = [b.X, b.Y, b.Z];
        // (260502Cl) cand は常に 3 要素。loop 内で stackalloc するとスタック消費が膨らむため外に出して各反復で 0 クリア。
        Span<double> cand = stackalloc double[3];
        for (int fixedCol = 0; fixedCol < 3; fixedCol++)
        {
            int c0 = fixedCol == 0 ? 1 : 0;
            int c1 = fixedCol == 2 ? 1 : 2;
            for (int r0 = 0; r0 < 2; r0++) for (int r1 = r0 + 1; r1 < 3; r1++)
            {
                double det = a[r0, c0] * a[r1, c1] - a[r0, c1] * a[r1, c0];
                if (Math.Abs(det) < 1e-12) continue;
                double v0 = (rhs[r0] * a[r1, c1] - a[r0, c1] * rhs[r1]) / det;
                double v1 = (a[r0, c0] * rhs[r1] - rhs[r0] * a[r1, c0]) / det;
                cand.Clear();
                cand[c0] = v0; cand[c1] = v1;
                var cv = new Vec(cand[0], cand[1], cand[2]);
                var err = a * cv - b;
                double residual = err * err;
                if (residual >= bestResidual) continue;
                bestResidual = residual; x = cv;
            }
        }
        return bestResidual < 1e-10;
    }

    /// <summary>op の幾何位置を格子同値性から全列挙し、screw 成分も各同値操作ごとに保持する。</summary>
    private static IEnumerable<AxisInstance> EnumerateAxisInstances(SymmetryOperation op)
    {
        var seen = new HashSet<(long, long, long, long, long, long, bool)>();
        var (px, py, pz) = op.Position;
        var aMat = BuildIMinusR(op);
        bool useDecomp = aMat.Rank() == 2 && op.Order > 0;
        for (int tx = 0; tx <= 1; tx++) for (int ty = 0; ty <= 1; ty++) for (int tz = 0; tz <= 1; tz++)
        {
            Vec lattice = new(tx, ty, tz);
            Vec shift; (double U, double V, double W) rawIt;
            if (useDecomp)
            {
                if (!TryDecomposeAxisLatticeTranslation(op, lattice, out shift, out var axial)) continue;
                rawIt = (op.IntrinsicTranslation.U + axial.X, op.IntrinsicTranslation.V + axial.Y, op.IntrinsicTranslation.W + axial.Z);
            }
            else
            {
                if (!TrySolveLinear(aMat, lattice, out shift)) continue;
                rawIt = op.IntrinsicTranslation;
            }
            var it = (U: CenterMod1(rawIt.U), V: CenterMod1(rawIt.V), W: CenterMod1(rawIt.W));
            var axis = new AxisInstance(Mod1(px + shift.X), Mod1(py + shift.Y), Mod1(pz + shift.Z), op.Direction, it, IsScrewAxis(op, rawIt));
            var key = (R6(axis.X), R6(axis.Y), R6(axis.Z),
                R6(axis.IntrinsicTranslation.U), R6(axis.IntrinsicTranslation.V), R6(axis.IntrinsicTranslation.W), axis.Screw);
            if (seen.Add(key)) yield return axis;
        }
    }

    /// <summary>紙面垂直 mirror/glide の代表面。T=t+L を (I−R)p + g に分解し、斜交基底でも glide 成分を保つ。</summary>
    private readonly record struct MirrorPlane(double Px, double Py, double Pz, double GlideU, double GlideV, double GlideW,
                                               (int U, int V, int W) Direction);

    /// <summary>(260502Ch) 紙面垂直 mirror/glide の描画前集約用。</summary>
    private readonly record struct PerpendicularMirrorDraft(double Sx, double Sy,
                                                            (int U, int V, int W) Direction,
                                                            (double U, double V, double W) Glide);

    /// <summary>紙面内回転軸の 1 インスタンス。</summary>
    private readonly record struct AxisInstance(double X, double Y, double Z,
                                                (int U, int V, int W) Direction,
                                                (double U, double V, double W) IntrinsicTranslation,
                                                bool Screw);

    /// <summary>L を (I-R) による軸位置シフトと軸方向成分へ分解する。</summary>
    private static bool TryDecomposeAxisLatticeTranslation(SymmetryOperation op, Vec lattice, out Vec shift, out Vec axial)
    {
        shift = Vec.Zero; axial = Vec.Zero;
        var a = BuildIMinusR(op);
        if (a.Rank() != 2 || !TryFindAxisCovector(a, out var n)) return false;
        Vec d = op.Direction;
        double nd = n * d;
        if (Math.Abs(nd) < 1e-12) return false;
        double beta = (n * lattice) / nd;
        axial = beta * d;
        return TrySolveRankTwo(a, lattice - axial, out shift);
    }

    private static IEnumerable<MirrorPlane> EnumerateMirrorPlanes(SymmetryOperation op, IReadOnlyList<(double U, double V, double W)> centerings = null)
    {
        var R = new Mat(op.ApplyMatrix(new Vec(1, 0, 0)),
                        op.ApplyMatrix(new Vec(0, 1, 0)),
                        op.ApplyMatrix(new Vec(0, 0, 1)));
        var t0 = op.SeitzTranslation;
        var planes = new Dictionary<(long, long, long), MirrorPlane>();
        // (260502Cl) (0,0,0) と centering 並進を allowed lattice として全部試す。R-centering 等の存在で
        // 純 mirror / a-glide 等の (depth 成分なし) 表現が見つかるようにする (R3m, R-3m での誤 n-glide 表示の修正)。
        var lattices = new List<(double U, double V, double W)> { (0, 0, 0) };
        if (centerings != null) lattices.AddRange(centerings);
        foreach (var lat in lattices)
            for (int lx = -2; lx <= 2; lx++) for (int ly = -2; ly <= 2; ly++) for (int lz = -2; lz <= 2; lz++)
            {
                var t = new Vec(t0.U + lat.U + lx, t0.V + lat.V + ly, t0.W + lat.W + lz);
                var rt = R * t;
                var n = (t - rt) * 0.5;     // 平面法線方向の代表 (lattice 同値類のキー)
                var glide = (t + rt) * 0.5;
                var key = (R6(n.X), R6(n.Y), R6(n.Z));
                var plane = new MirrorPlane(t.X / 2.0, t.Y / 2.0, t.Z / 2.0,
                    CenterMod1(glide.X), CenterMod1(glide.Y), CenterMod1(glide.Z), op.Direction);
                if (!planes.TryGetValue(key, out var current) || GlideScore(plane) < GlideScore(current))
                    planes[key] = plane;
            }
        foreach (var plane in planes.Values) yield return plane;

        static double GlideScore(MirrorPlane p) => Math.Abs(p.GlideU) + Math.Abs(p.GlideV) + Math.Abs(p.GlideW);
    }

    /// <summary>代表 mirror/glide 面を proper symmetry operation で写し、点群対称で等価な面を列挙する。</summary>
    private static IEnumerable<MirrorPlane> EnumerateEquivalentMirrorPlanes(MirrorPlane seed, SymmetryOperation[] ops)
    {
        if (ops == null) { yield return seed; yield break; }
        var seen = new HashSet<(long, long, long, long, long, long, int, int, int)>();
        foreach (var op in ops)
        {
            if (op.Order <= 0) continue;
            var p = op.ApplyAffine(new Vec(seed.Px, seed.Py, seed.Pz));
            var g = op.ApplyMatrix(new Vec(seed.GlideU, seed.GlideV, seed.GlideW));
            var d = NormalizeDirection(op.ApplyMatrix(new Vec(seed.Direction.U, seed.Direction.V, seed.Direction.W)));
            if (d == (0, 0, 0)) continue;
            var eq = new MirrorPlane(p.X, p.Y, p.Z, CenterMod1(g.X), CenterMod1(g.Y), CenterMod1(g.Z), d);
            var key = (R6(eq.Px), R6(eq.Py), R6(eq.Pz), R6(eq.GlideU), R6(eq.GlideV), R6(eq.GlideW),
                eq.Direction.U, eq.Direction.V, eq.Direction.W);
            if (seen.Add(key)) yield return eq;
        }
    }

    /// <summary>代表の紙面内回転軸を空間群操作で共役し、等価な軸を列挙する。</summary>
    private static IEnumerable<AxisInstance> EnumerateEquivalentAxisInstances(AxisInstance seed, SymmetryOperation[] ops)
    {
        if (ops == null) { yield return seed; yield break; }
        var seen = new HashSet<(long, long, long, long, long, long, int, int, int, bool)>();
        foreach (var op in ops)
        {
            var p = op.ApplyAffine(new Vec(seed.X, seed.Y, seed.Z));
            var g = op.ApplyMatrix(new Vec(seed.IntrinsicTranslation.U, seed.IntrinsicTranslation.V, seed.IntrinsicTranslation.W));
            var d = NormalizeDirection(op.ApplyMatrix(new Vec(seed.Direction.U, seed.Direction.V, seed.Direction.W)));
            if (d == (0, 0, 0)) continue;
            var eq = new AxisInstance(Mod1(p.X), Mod1(p.Y), Mod1(p.Z), d,
                (CenterMod1(g.X), CenterMod1(g.Y), CenterMod1(g.Z)), seed.Screw);
            var key = (R6(eq.X), R6(eq.Y), R6(eq.Z),
                R6(eq.IntrinsicTranslation.U), R6(eq.IntrinsicTranslation.V), R6(eq.IntrinsicTranslation.W),
                eq.Direction.U, eq.Direction.V, eq.Direction.W, eq.Screw);
            if (seen.Add(key)) yield return eq;
        }
    }

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
        if (u < 0 || (u == 0 && v < 0) || (u == 0 && v == 0 && w < 0)) (u, v, w) = (-u, -v, -w);
        return (u, v, w);
    }

    private static int Gcd(int a, int b)
    {
        while (b != 0) (a, b) = (b, a % b);
        return a;
    }

    /// <summary>反転中心を Wyckoff position から抽出。site sym が中心対称で全 free=false の WP が反転中心 (centering 込み)。
    /// 同一 xy の複数 z は最小 z だけを ITC 慣用に従い表示。</summary>
    private static List<(PointF Pt, double MinZ)> CollectInversions(int seriesNumber, Projection proj, CellLayout layout)
    {
        var byKey = new Dictionary<(int, int), (double sxF, double syF, double minZ)>();
        foreach (var wp in SymmetryStatic.WyckoffPositions[seriesNumber])
        {
            //if (string.IsNullOrEmpty(wp.SiteSymmetry) || !wp.SiteSymmetry.Contains('-')) continue;
            // (260502Ch) "-4" は反転中心を持たず、"2/m" や "4/m" は '-' を含まないため、中心対称な site symmetry を明示判定する。
            if (!HasInversionCenter(wp.SiteSymmetry)) continue;
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

    /// <summary>(260502Ch) 反転中心を含む結晶学的点群だけを true にする。".2/m." などの向き指定ドットは判定前に除く。</summary>
    private static bool HasInversionCenter(string siteSymmetry)
    {
        if (string.IsNullOrEmpty(siteSymmetry)) return false;
        return siteSymmetry.Replace(".", "") is "-1" or "2/m" or "mmm" or "4/m" or "4/mmm"
            or "-3" or "-3m" or "6/m" or "6/mmm" or "m-3" or "m-3m";
    }

    /// <summary>絶対次数 ≥ minOrder の proper rotation (perp) の position 集合。低次シンボル抑制用。</summary>
    private static HashSet<(int, int)> CollectHigherRotationPositions(SymmetryOperation[] ops, Projection proj, ProjectionAxis projAxis, int minOrder)
    {
        var set = new HashSet<(int, int)>();
        foreach (var op in ops)
        {
            int absO = Math.Abs(op.Order);
            if (op.Order < 0 || absO < minOrder || absO is not (3 or 4 or 6)) continue;
            if (!IsAxisPerpendicularToProjection(op.Direction, projAxis)) continue;
            foreach (var axis in EnumerateAxisInstances(op))
            {
                var (sx, sy, _) = proj.ToScreen(axis.X, axis.Y, axis.Z);
                set.Add(((int)Math.Round(Mod1(sx) * 10000), (int)Math.Round(Mod1(sy) * 10000)));
            }
        }
        return set;
    }

    /// <summary>(260502Cl) 同位置の proper N-fold (N=2,3,4,6) を記録。-N 描画時に対応する +N があれば skip するため。</summary>
    private static HashSet<(int N, int Sx10000, int Sy10000)> CollectProperRotationPositions(SymmetryOperation[] ops, Projection proj, ProjectionAxis projAxis)
    {
        var set = new HashSet<(int, int, int)>();
        foreach (var op in ops)
        {
            int absO = Math.Abs(op.Order);
            if (op.Order <= 0 || absO is not (2 or 3 or 4 or 6)) continue;
            if (!IsAxisPerpendicularToProjection(op.Direction, projAxis)) continue;
            foreach (var axis in EnumerateAxisInstances(op))
            {
                if (axis.Screw) continue; // pure proper rotation のみ (screw は -N を抑制しない)
                var (sx, sy, _) = proj.ToScreen(axis.X, axis.Y, axis.Z);
                set.Add((absO, (int)Math.Round(Mod1(sx) * 10000), (int)Math.Round(Mod1(sy) * 10000)));
            }
        }
        return set;
    }
    #endregion

    #region dispatcher
    private static bool IsAxisPerpendicularToProjection((int U, int V, int W) d, ProjectionAxis a) => a switch
    {
        ProjectionAxis.C => d is (0, 0, not 0),
        ProjectionAxis.A => d is (not 0, 0, 0),
        _ => d is (0, not 0, 0),
    };

    /// <summary>op を投影面上の幾何記号として描画。紙面平行 mirror は ctx.ParallelMirrors に集約。</summary>
    private static void DrawSymmetryElement(ElementsContext ctx, SymmetryOperation op,
                                            bool drawOnlyPerpPointMarks = false, bool skipPerpPointMarks = false)
    {
        int o = op.Order, absO = Math.Abs(o);
        if (o == 1 || o == -1) return;                 // 反転は WP-based で別途処理
        if (!op.Sense && (absO is 3 or 4 or 6)) return; // Sense=false の高次回転は同軸逆冪なので skip

        bool perp = IsAxisPerpendicularToProjection(op.Direction, ctx.Proj.Axis);
        var d = op.Direction;
        bool inPlane = ctx.Proj.Axis switch
        {
            ProjectionAxis.C => d.W == 0 && (d.U != 0 || d.V != 0),
            ProjectionAxis.A => d.U == 0 && (d.V != 0 || d.W != 0),
            _ => d.V == 0 && (d.U != 0 || d.W != 0),
        };
        bool isMirror = (o == -2);
        var it = op.IntrinsicTranslation;
        bool glide = Math.Abs(it.U) + Math.Abs(it.V) + Math.Abs(it.W) > 1e-6;
        bool isPointMark = !isMirror && perp && (absO is 2 or 3 or 4 or 6);

        if (drawOnlyPerpPointMarks && !isPointMark) return;
        if (skipPerpPointMarks && isPointMark) return;

        // 紙面平行 mirror: 高さと投影面 glide のみ集約 (DrawParallelMirrorStack で一括描画)
        if (isMirror && perp)
        {
            var (_, _, opSz) = ctx.Proj.ToScreen(op.Position.U, op.Position.V, op.Position.W);
            var (gSx, gSy) = ProjectVector(it.U, it.V, it.W, ctx.Proj.Axis);
            ctx.ParallelMirrors.Add((Mod1(opSz), glide, gSx, gSy));
            return;
        }

        // 紙面内 2(2_1) 軸: lattice translation と空間群対称で等価な軸を展開して描画
        if (absO == 2 && !isMirror && inPlane)
        {
            foreach (var seed in EnumerateAxisInstances(op))
                foreach (var axis in EnumerateEquivalentAxisInstances(seed, ctx.Ops))
                {
                    var (sx, sy, sz) = ctx.Proj.ToScreen(axis.X, axis.Y, axis.Z);
                    CollectInPlaneAxisArrows(ctx.C, sx, sy, Mod1(sz), axis.Direction, ctx.Proj.Axis, axis.Screw, ctx.InPlaneAxisDrafts);
                }
            return;
        }

        // 紙面垂直 mirror/glide: 代表面を取り出し、proper symmetry operation の orbit として等価面を描画
        if (isMirror && inPlane)
        {
            foreach (var pl in EnumerateMirrorPlanes(op, ctx.Centerings)) // (260502Cl) centering 込みで lattice 並進を列挙
                foreach (var eq in EnumerateEquivalentMirrorPlanes(pl, ctx.Ops))
                {
                    var (sx, sy, _) = ctx.Proj.ToScreen(eq.Px, eq.Py, eq.Pz);
                    ctx.PerpendicularMirrors.Add(new(sx, sy, eq.Direction, (eq.GlideU, eq.GlideV, eq.GlideW))); // (260502Ch)
                }
            return;
        }

        if (!isPointMark) return;
        foreach (var axis in EnumerateAxisInstances(op))
        {
            var (sx, sy, sz) = ctx.Proj.ToScreen(axis.X, axis.Y, axis.Z);
            var key = ((int)Math.Round(Mod1(sx) * 10000), (int)Math.Round(Mod1(sy) * 10000));
            if (absO == 2 && ctx.Covered2.Contains(key)) continue;
            if (absO == 3 && o > 0 && ctx.Covered3.Contains(key)) continue;
            // (260502Cl) -N at same position as proper +N → +N を優先 (反転中心が両者を結ぶので -N は重複)
            if (o < 0 && ctx.ProperRotations.Contains((absO, key.Item1, key.Item2))) continue;

            // -N (N=3,4,6) で inversion 点の z_c ≠ 0 のときは N_k 螺旋 + inversion(z=0) と等価。
            // proper +N_k シンボルとして描画する (反転中心は z=0 で別途 CollectInversions により描画)。
            int order = o;
            (int finCount, int edgeStep) screw = (-1, -1); // sentinel: ScrewParams にフォールバック
            if (o < 0 && absO is (3 or 4 or 6))
            {
                double zc = Mod1(sz);
                if (Math.Abs(zc) > 1e-3 && Math.Abs(zc - 1) > 1e-3)
                {
                    int kk = ((int)Math.Round(Mod1(2 * zc) * absO)) % absO;
                    if (kk != 0)
                    {
                        order = absO;
                        screw = PinwheelFins(absO, kk);
                    }
                }
            }

            bool nearEdge = Math.Min(sx, 1 - sx) < EdgeReplicate || Math.Min(sy, 1 - sy) < EdgeReplicate;
            for (int dx = -1; dx <= 1; dx++) for (int dy = -1; dy <= 1; dy++)
            {
                if ((dx != 0 || dy != 0) && !nearEdge) continue;
                double dxf = sx + dx, dyf = sy + dy;
                if (dxf < -EdgeReplicate || dxf > 1 + EdgeReplicate || dyf < -EdgeReplicate || dyf > 1 + EdgeReplicate) continue;
                var pt = ctx.C.ToScreen(dxf, dyf);
                var (finCount, edgeStep) = screw.finCount >= 0 ? screw : ScrewParams(op, axis.IntrinsicTranslation);
                if (absO == 2) DrawTwofoldPerp(ctx.G, ctx.Fill, pt, axis.Screw);
                else if (absO == 3) DrawRotationPerp(ctx.G, ctx.Fill, ctx.White, pt, order, finCount, edgeStep, 3, ThreeFoldRadius);
                else if (absO == 4) DrawRotationPerp(ctx.G, ctx.Fill, ctx.White, pt, order, finCount, edgeStep, 4, FourFoldRadius);
                else if (absO == 6) DrawRotationPerp(ctx.G, ctx.Fill, ctx.White, pt, order, finCount, edgeStep, 6, SixFoldRadius);
            }
        }
    }
    #endregion

    #region 個別シンボル描画
    /// <summary>反転中心を白丸 (黒縁) で描画、z!=0 で高さラベルを併記。(260502Cl) 描画パスの最後に呼ぶので、白塗りで下層の点記号を punch out して見える化する。</summary>
    private static void DrawInversions(Graphics g, List<(PointF Pt, double MinZ)> inversions, Pen pen, Brush white, Brush fill)
    {
        if (inversions.Count == 0) return;
        foreach (var (pt, z) in inversions)
        {
            g.FillEllipse(white, pt.X - InversionR, pt.Y - InversionR, 2 * InversionR, 2 * InversionR);
            g.DrawEllipse(pen, pt.X - InversionR, pt.Y - InversionR, 2 * InversionR, 2 * InversionR);
            string h = HeightLabel(z);
            if (h == null) continue;
            g.DrawString(h, HeightLabelFont, fill, pt.X + InversionR + 1, pt.Y - InversionR - g.MeasureString(h, HeightLabelFont).Height + 2);
        }
    }

    /// <summary>紙面垂直 2(2_1) 軸: vesica piscis lens を塗り潰し。screw=互い違い円弧。-4 から呼ぶ際は scale で縮小。</summary>
    private static void DrawTwofoldPerp(Graphics g, Brush fill, PointF pt, bool screw, float scale = 1f)
    {
        float halfW = TwofoldHalfW * scale, halfH = TwofoldHalfH * scale;
        float r = (halfW * halfW + halfH * halfH) / (2 * halfW), d = r - halfW;
        float halfAngle = (float)(Math.Atan2(halfH, d) * 180.0 / Math.PI);
        var rightRect = new RectangleF(pt.X + d - r, pt.Y - r, 2 * r, 2 * r);
        var leftRect  = new RectangleF(pt.X - d - r, pt.Y - r, 2 * r, 2 * r);
        using var path = new GraphicsPath();
        path.AddArc(rightRect, 180f + halfAngle, -2 * halfAngle);
        path.AddArc(leftRect, halfAngle, -2 * halfAngle);
        path.CloseFigure();
        using (var halo = new Pen(Color.White, SymbolHaloPenWidth) { LineJoin = LineJoin.Round })
            g.DrawPath(halo, path);
        g.FillPath(fill, path);
        if (!screw) return;
        using var pen = new Pen(Color.Black, ScrewFinPenWidth);
        g.DrawArc(pen, rightRect, 180f + halfAngle, ScrewFinSweepDeg);
        g.DrawArc(pen, leftRect, halfAngle, ScrewFinSweepDeg);
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

    /// <summary>(260502Cl) 紙面内 2(2_1) 軸の矢印 draft。後段の DrawCollectedInPlaneAxisArrows で実描画。</summary>
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
        foreach (var d in drafts.Values)
        {
            var tip = new PointF((float)(d.Anchor.X + InPlaneArrowExt * d.OutUx), (float)(d.Anchor.Y + InPlaneArrowExt * d.OutUy));
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

    private static void DrawArrowhead(Graphics g, Brush fill, PointF tip, double ux, double uy, bool halfHead)
    {
        float bx = (float)(tip.X - ArrowHeadLen * ux), by = (float)(tip.Y - ArrowHeadLen * uy);
        PointF left  = new((float)(bx - ArrowHeadHalfWidth * uy), (float)(by + ArrowHeadHalfWidth * ux));
        PointF right = new((float)(bx + ArrowHeadHalfWidth * uy), (float)(by - ArrowHeadHalfWidth * ux));
        g.FillPolygon(fill, halfHead ? [tip, new PointF(bx, by), left] : [tip, left, right]);
    }

    /// <summary>紙面平行 mirror/glide を IUCR corner bracket で描画。mirror があれば左上、glide は右下に分ける。</summary>
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

        // (260502Ch) mirror は高さ 0 を優先し、glide は glide 方向ごとに低い高さを採用した代表記号にまとめる。
        // (260502Cl) e-glide (Ccce 等) は同じ高さに 2 方向の glide が共存するので、その場合は 1 個の bracket で 2 本の矢印を描く。
        var symbols = new List<(double Height, double GlideSx, double GlideSy, double GlideSx2, double GlideSy2, bool NGlide, bool DGlide, int DiamondScore)>();
        var mirrorHeights = markers
            .Where(m => !HasInPlaneGlide(m))
            .Select(m => HeightKey(m.Height))
            .Distinct()
            .OrderBy(h => h)
            .ToList();
        if (mirrorHeights.Count > 0)
            symbols.Add((mirrorHeights[0], 0, 0, 0, 0, false, false, 0));

        // 1 方向ごとに代表 glide を取り、そのあと同じ高さでの直交ペアを e-glide にマージ。
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
            int merged = -1;
            for (int i = 0; i < list.Count - 1 && merged < 0; i++)
                for (int j = i + 1; j < list.Count; j++)
                    if (IsDoubleGlidePair(list[i].Sx, list[i].Sy, 0, list[j].Sx, list[j].Sy, 0)) // (260502Ch) 紙面垂直 e-glide と共通の判定。
                    {
                        symbols.Add((heightGrp.Key, list[i].Sx, list[i].Sy, list[j].Sx, list[j].Sy, false, false, 0));
                        list.RemoveAt(j); list.RemoveAt(i);
                        merged = i;
                        break;
                    }
            foreach (var rep in list)
                symbols.Add((rep.Height, rep.Sx, rep.Sy, 0, 0, rep.NG, rep.DG, rep.DS));
        }

        bool hasDiamondGlide = symbols.Any(s => s.DGlide);
        var orderedSymbols = symbols
            .Select((s, i) => (Symbol: s, Index: i))
            .OrderBy(x => hasDiamondGlide ? x.Symbol.DiamondScore : GetRightDownScore(x.Symbol))
            .ThenBy(x => x.Symbol.Height)
            .ThenBy(x => Math.Atan2(x.Symbol.GlideSy, x.Symbol.GlideSx))
            .Select(x => x.Symbol)
            .ToList();
        for (int i = 0; i < orderedSymbols.Count; i++)
        {
            var marker = orderedSymbols[i];
            // (260502Cl) 左上 (i=0) の bracket は記号の左下にラベルを置く。それ以外は従来通り右側 (中央)。
            DrawBracket(new PointF(apex0.X + CornerBracketStep * i * (hUx + vUx), apex0.Y + CornerBracketStep * i * (hUy + vUy)),
                marker.Height, marker.GlideSx, marker.GlideSy, marker.GlideSx2, marker.GlideSy2, labelAtBottomLeft: i == 0);
        }

        void DrawBracket(PointF apex, double height, double glideSx, double glideSy, double glideSx2, double glideSy2, bool labelAtBottomLeft)
        {
            var hEnd = new PointF(apex.X + armLen * hUx, apex.Y + armLen * hUy);
            var vEnd = new PointF(apex.X + armLen * vUx, apex.Y + armLen * vUy);
            // (260502Cl) 矢印は最大 2 本まで (e-glide 用)。
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
                // (260502Ch) 映進方向が bracket の腕と重なる場合は、腕自体も矢頭より手前で止める。
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

            // (260502Cl) 高さラベル: 0 なら省略。labelAtBottomLeft (左上 bracket) は下向き腕の終端 vEnd の下・左脇、それ以外は右側 (中央) に置く。
            string lbl = HeightLabel(height);
            if (lbl == null) return;
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
        {
            return IsQuarterGlideComponent(glideSx) && IsQuarterGlideComponent(glideSy);
        }

        void NormalizeDiamondGlideDirection(ref double glideSx, ref double glideSy)
        {
            if (!IsDGlide(glideSx, glideSy)) return;
            double dx = glideSx * c.Horz.X + glideSy * c.Vert.X;
            double dy = glideSx * c.Horz.Y + glideSy * c.Vert.Y;
            if (dy < 0) { glideSx = -glideSx; glideSy = -glideSy; }
        }

        int GetDiamondArrowScore(double glideSx, double glideSy)
        {
            if (!IsDGlide(glideSx, glideSy)) return 0;
            double dx = glideSx * c.Horz.X + glideSy * c.Vert.X;
            double dy = glideSx * c.Horz.Y + glideSy * c.Vert.Y;
            if (dy > 0 && dx < 0) return -1; // (260502Ch) 左下矢印は左上側へ。
            if (dy > 0 && dx > 0) return 1;  // (260502Ch) 右下矢印は右下側へ。
            return 0;
        }

        static int GetRightDownScore((double Height, double GlideSx, double GlideSy, double GlideSx2, double GlideSy2, bool NGlide, bool DGlide, int DiamondScore) symbol)
            => symbol.NGlide ? 2 : 0;

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

    /// <summary>(260502Ch) 紙面垂直 mirror/glide を幾何線ごとに集約し、double-glide ペアを e-glide として描画する。</summary>
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
            // (260502Cl) d-glide は最優先 — 該当 draft を描いて他は捨てる。
            for (int i = 0; i < drafts.Count; i++)
            {
                var (gSx, gSy, gSz) = ctx.Proj.ToScreen(drafts[i].Glide.U, drafts[i].Glide.V, drafts[i].Glide.W);
                if (!IsPerpendicularDGlide(gSx, gSy, gSz)) continue;
                DrawMirrorPerpToScreen(ctx, drafts[i].Sx, drafts[i].Sy, drafts[i].Direction, drafts[i].Glide);
                goto nextGroup;
            }
            // double-glide ペアなら e-glide 1 個に統合。
            if (TryFindDoubleGlideDraft(ctx.Proj, drafts, out var eDraft))
            {
                DrawMirrorPerpToScreen(ctx, eDraft.Sx, eDraft.Sy, eDraft.Direction, eDraft.Glide, forceEGlide: true);
                continue;
            }
            // (260502Cl) 同一線位置に複数 draft が来た場合は glide score (|gSx|+|gSy|+|gSz|) が最小の 1 個のみ描く。
            // R-3m 等で R-centering 由来の (重複) c-glide / mixed-glide 表現を捨て、純 mirror や a/b-glide を残すため。
            var best = drafts.OrderBy(d =>
            {
                var (gSx, gSy, gSz) = ctx.Proj.ToScreen(d.Glide.U, d.Glide.V, d.Glide.W);
                return Math.Abs(gSx) + Math.Abs(gSy) + Math.Abs(gSz);
            }).First();
            DrawMirrorPerpToScreen(ctx, best.Sx, best.Sy, best.Direction, best.Glide);
            nextGroup:;
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
        bool eGlide = forceEGlide; // (260502Ch) e は DrawCollectedPerpendicularMirrorPlanes で double-glide ペアから判定済み。
        int style = dGlide ? 4 : eGlide ? 5 : (hasInPlane, hasDepth) switch { (false, false) => 0, (true, false) => 1, (false, true) => 2, _ => 3 };
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

    /// <summary>(260502Ch) e-glide 判定の共通部。互いに独立な単一方向 half-glide が 2 本ある場合を double-glide とする。</summary>
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

    /// <summary>(260502Ch) d-glide 矢印の基準方向。(0,+1/4,+1/4) は C 投影で右向き (+Y = +b) になる。</summary>
    private static (float X, float Y) GetDGlideArrowDirection(CellLayout c, double gSx, double gSy, double gSz)
    {
        double depthSign = CenterMod1(gSz) < 0 ? -1 : 1;
        return ((float)(depthSign * (gSx * c.Horz.X + gSy * c.Vert.X)),
                (float)(depthSign * (gSx * c.Horz.Y + gSy * c.Vert.Y)));
    }

    /// <summary>(260502Ch) 紙面垂直 d-glide の dot-dash-dot-dash-dot-arrow 反復線。</summary>
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
            Arrow(baseT + 44f, baseT + 44f + arrowLen, forward: true); // (260502Ch) 一本の d-glide 線内では矢印方向を変えない。
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

        void Arrow(float t1, float t2, bool forward)
        {
            if (t2 > len || t2 <= t1) return;
            if (forward)
            {
                var tip = Pt(t2);
                var lineEnd = Pt(Math.Max(t1, t2 - GlideArrowLineShorten));
                g.DrawLine(pen, Pt(t1), lineEnd);
                DrawArrowhead(g, fill, tip, ux, uy, halfHead: false);
            }
            else
            {
                var tip = Pt(t1);
                var lineEnd = Pt(Math.Min(t2, t1 + GlideArrowLineShorten));
                g.DrawLine(pen, lineEnd, Pt(t2));
                DrawArrowhead(g, fill, tip, -ux, -uy, halfHead: false);
            }
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
