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

    // 紙面垂直 3/4/6 と -4 — 正多角形。260512Ch: -3/-6 は principal 軸から外し、3 + -1 / 3 + m で表現。
    private const float ThreeFoldRadius         = 5.625f;
    private const float FourFoldRadius          = 7.2f;
    private const float SixFoldRadius           = 6.0f;
    private const float ScrewFinTailLen         = 5f;
    // private const float MinusThreeCenterDotR    = 2f; // 旧: -3 点記号の中心白丸。260512Ch: -3 を独立描画しないため未使用。
    private const float MinusFourInnerLensScale = 0.8f;

    // 立方晶 [111] / [110] 系 斜め回転軸 (foot 黒丸 / shaft1 / 中央記号 (3回=三角, 2回=lens) / 白丸 / shaft2)。
    // 260504Cl: 旧 DiagonalShaft1Len/2Len は 2 回軸でも共用していたため Diagonal... に改名。
    // 斜め 2 回軸・3 回軸の shaft 全長 (= shaft1 + shaft2) は同一とし、斜め鏡映の inset stereonet 半径もこれに揃える。
    private const float DiagonalShaft1Len      = 22f;    // foot 黒丸 → 中央記号までの距離 (260503Cl)
    private const float DiagonalShaft2Len      = 7.2f;   // 中央記号 → 反対側 tail までの距離 (260503Cl: 旧 12 × 0.6)
    private const float DiagonalShaftTotalLen  = DiagonalShaft1Len + DiagonalShaft2Len; // = 29.2 (260504Cl)
    private const float DiagThreefoldTriLeg    = 16.5f;  // 三角の脚長
    private const float DiagThreefoldHaloWidth = 3.6f;   // shaft の白縁取り太さ (= 直径)
    private const float DiagThreefoldFootRatio = 1.25f;  // foot 黒丸の半径 / 白丸の半径

    // 反転中心
    private const float InversionR = 2.5f;

    // 260504Cl 追加: 立方晶 m-3m / -43m 系の斜め鏡映 (例: (011), (101), (110)) を inset stereonet で表示する。
    // 輪郭半径 (1× 基準) は斜め 2/3 回軸の shaft 全長 (= DiagonalShaftTotalLen) と同じ値。
    // 実際の inset 半径は呼び出し側で指定し、ヘルパー内では r / DiagonalStereonetRadius を全体倍率として用いる。
    private const float DiagonalStereonetRadius = DiagonalShaftTotalLen;
    private const float DiagonalStereonetInsetScale = 1.8f; // 260505Cl: 実 inset の半径倍率 (= 1.2 × 1.25 → 260505Cl さらに 1.2 倍 = 1.8)
    // 260505Cl: 輪郭半径だけを 1.2 倍したいので 2/3 回軸シンボルの倍率は分離。旧 InsetScale (1.5) と同じ値を保持。
    private const float DiagonalStereonetSymbolScale = 1.5f;
    private const float CubicStereonetInsetRadius = DiagonalStereonetRadius * DiagonalStereonetInsetScale; // (260506Cl)

    // (260506Cl) stereonet inset の補助大円 4 本 (cell 輪郭と同じ色・線幅)。各 inset で同じ HKL を描画するので static readonly で再利用。
    private static readonly (int H, int K, int L)[] CubicStereonetAuxiliaryHkls = [(1, 0, 1), (1, 0, -1), (0, 1, 1), (0, 1, -1)];

    // 紙面内 2(2_1) 矢印 (260503Cl: 紙面平行 4 回軸と統一デザインに合わせて shaft 0.75 倍 / 三角幅 0.75 倍。さらに shaft 0.6 倍に短縮)
    private const float InPlaneArrowExt    = 14.4f;   // = 32 × 0.75 × 0.6
    private const float ArrowHeadLen       = 7f;
    private const float ArrowHeadHalfWidth = 2.25f;   // = 3 × 0.75
    private const float ArrowHeadCentroidToTipFactor = 2f / 3f; // (260505Ch) 三角矢じりの重心→tip 距離 / 矢じり長。
    /// <summary>(260505Cl 整理) 紙面内 2/4/-4 軸の lineEnd → tip オフセット (= 三角矢じり / 平行四辺形ヘッド共通の食い込み量)。</summary>
    private const float ArrowVisualHeadOffset = ArrowHeadLen * ArrowHeadCentroidToTipFactor; // (260505Ch)
    /// <summary>(260505Cl 整理) anchor から「視覚的 tip 位置」までのオフセット。2 回軸三角先端も 4 回軸平行四辺形右端もこの位置で揃う。</summary>
    private const float ArrowVisualTipOffset = InPlaneArrowExt + ArrowVisualHeadOffset;
    /// <summary>(260505Cl 整理) 4 回軸 shaft の追加長 (shaft 右端で 2 回軸 tip と揃える分)。</summary>
    private const float Shaft4Extra = ArrowHeadLen / 6f;
    /// <summary>(260505Cl 整理) anchor 重複時の axis 方向ずらし量 (px)。
    /// 重複が無い場合のセル枠線への食い込み回避 default、斜め軸 foot との重なり、紙面垂直軸点記号との重なりで切り替える。</summary>
    private const float InPlaneArrowDefaultShift       = 4f;
    private const float InPlaneArrowDiagonalFootShift  = 35f; // 斜め軸 foot は shaft1 (= 22) の先まで張り出すため大きめ。
    private const float InPlaneArrowPerpendicularShift = 16f; // 紙面垂直軸点記号は半径数 px なので中程度。
    /// <summary>(260505Cl 整理) 同じ anchor に複数軸 (2/2_1 並列、4_n/4/-4 並列) があるときの軸間 perpendicular pitch (px)。</summary>
    private const float InPlaneArrowGroupPitch4    = 18f; // 4 系列が並ぶとき (平行四辺形ヘッドが大きい)
    private const float InPlaneArrowGroupPitch2    = 7f;  // 2 系列のみ
    /// <summary>(260505Cl 整理) m-3m / -43m 系の stereonet inset 輪郭外側に紙面内軸矢印を出すときの追加マージン (px)。</summary>
    private const float StereonetArrowOutsideGap = 4f;
    private const float DGlideDotR         = 1.7f;
    private const float DGlidePatternPitch = 64f;
    /// <summary>(260505Cl 整理) 紙面垂直 d-glide パターンの dash 長 / arrow 長 (1 周期 = DGlidePatternPitch 内で繰り返し)。</summary>
    private const float DGlidePatternDashLen  = 9f;
    private const float DGlidePatternArrowLen = 13f;
    /// <summary>(260505Ch) d-glide 反復パターン内の dot/dash/arrow 開始位置 (px)。</summary>
    private const float DGlidePatternDot1Offset   = 3f;
    private const float DGlidePatternDash1Offset  = 9f;
    private const float DGlidePatternDot2Offset   = 22f;
    private const float DGlidePatternDash2Offset  = 28f;
    private const float DGlidePatternDot3Offset   = 41f;
    private const float DGlidePatternArrowOffset  = 47f;
    private const float EGlideDotDashUnit  = 2.6f;
    /// <summary>(260505Cl 整理) 2_1 螺旋 fin (円弧) 描画の overlap 角 (deg)。レンズ角 AA 境界の 1 px すき間を埋めるためレンズ側に食い込ませる。</summary>
    private const float TwofoldFinOverlapDeg = 6f;
    /// <summary>(260505Cl 整理) stereonet 上の d-glide 大円に置く接線アローの位置 (大円中心から見た角度、deg)。</summary>
    private static readonly double[] StereonetDGlideArrowAnglesRad =
        [-35.0 * Math.PI / 180.0, 0, 35.0 * Math.PI / 180.0];

    // 紙面内 4 回軸の平行四辺形定数 (1x scale): 左右辺長 = 2*vHalf、上下辺は slant = ArrowHeadLen*tan(30°)。
    private const float InPlaneFourfoldVHalf = 6f;
    private const float InPlaneFourfoldSlant = ArrowHeadLen * 0.5773502692f;
    // 立方晶 stereonet inset 専用の図形パラメータ。全体倍率は r (stereonet 半径) / DiagonalStereonetRadius (= 1× 基準 29.2 px) として helper 内で計算する。
    private const float StereonetTwofoldOverallScale = 0.8f;   // 2 回軸 lens 全体の倍率 (長軸・短軸とも)
    private const float StereonetTwofoldWidthScale   = 0.75f;  // 2 回軸 lens の幅 (短軸方向) のみ細める追加倍率
    private const float StereonetThreefoldApexDeg    = 80f;    // 3 回軸 二等辺三角の apex 角 (80°-50°-50°)
    private const float StereonetFinScale            = 0.8f;   // 2_1 螺旋 fin (円弧) の長さ倍率
    private const float StereonetDGlideArrowScale    = 1.2f;   // (260505Ch) 斜め d 映進アローを紙面垂直 d-glide の 1.2 倍にする。
    private const int StereonetAxisInPlaneShift      = 3;      // (260505Ch) stereonet 上の 2/3 回軸を大円交点側へ寄せる in-plane 倍率。
    private const int StereonetAxisDepthShift        = 2;
    private const int StereonetMirrorInPlaneShift    = 2;      // (260505Ch) stereonet 補助/鏡映大円の視認性 shift 倍率。
    private const int StereonetMirrorDepthShift      = 3;

    // 紙面平行 mirror corner bracket
    private const float CornerBracketArmLen = 22f;
    private const float CornerBracketGap    = 45f;
    private const float CornerBracketStep   = 8f;
    private const float CornerBracketCubicExtraGap = 15f; // (260505Cl) 立方晶系では紙面平行軸矢印と bracket が干渉するため、cell 角からさらに離す。
    private const float GlideArrowLineShorten = 5f;

    // 高さラベル
    // private const float HeightLabelGapX = 1f;
    // private const float HeightLabelGapY = 2f;
    private const float HeightLabelGapX = 1f; // 260510Ch
    private const float HeightLabelGapY = 1f; // 260510Ch
    private const float InPlaneAxisLabelGap = 1f;
    private const float ParallelMirrorLabelGap = 2f;
    // private const float MinusFourHeightLabelRadiusScale = 0.5f; // (260505Ch) 立方晶 -4 の高さラベルを外接四角形より内側へ寄せる。
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
        public HashSet<(long Nx, long Ny, long D, MirrorGlideStyle Style)> DrawnSymmetryPlanes; // 260510Ch: 線種を enum で保持。(260512Ch) plane/inset 共通 style へ統合。
        public HashSet<(long X, long Y)> StereonetAnchorKeys; // (260505Cl) inset 半径は CubicStereonetInsetRadius 定数を直接使う。
        public double DisplayMaxS; // (260505Ch) F 格子は [0, 1/2]²、それ以外は [0, 1]² を描画対象にする。
        public bool AllowEGlide; // 260510Ch: e-glide 記号は投影面の基底が直交する場合だけ許可する。
        public string SpaceGroupHM; // 260510Ch: 立方晶の ITA defining symbol resolver 用。
    }

    private readonly record struct PerpendicularMirrorDraft(double Sx, double Sy,
                                                            (int U, int V, int W) Direction,
                                                            (double U, double V, double W) Glide);

    private readonly record struct ParallelMirrorSymbol(double Height,
                                                        double GlideSx, double GlideSy,
                                                        double GlideSx2, double GlideSy2,
                                                        bool NGlide, bool DGlide, int DiamondScore);

    // (260512Ch) mirror/glide の分類結果を共通 enum に集約し、描画先ごとの pen/shape 選択だけを分ける。
    private enum MirrorGlideStyle
    {
        Mirror,
        AxialInPlane,
        AxialDepth,
        DiagonalGlide, // 紙面垂直線でのみ使う、in-plane + depth 成分を持つ通常 glide。
        DGlide,
        EGlide,
        NGlide, // 260510Ch: n-glide は d-glide と同じ dash-dot 系線だが矢印を持たない。
        None,   // 260510Ch: ITA 図に出さない幾何線。
    }

    private const double GlideZeroEps = 1e-6; // (260512Ch) glide coset 判定用のゼロ閾値。
    private const double GlideFractionEps = FracEps; // (260512Ch) 1/2, 1/4 成分の判定幅を既存の分数判定へ揃える。

    /// <summary>(260512Ch) glide 並進を [-1/2, 1/2] の coset 代表として扱う共通ヘルパ。</summary>
    private readonly record struct GlideCoset(double X, double Y, double Z)
    {
        public static GlideCoset Centered(double x, double y, double z)
            => new(CenterMod1(x), CenterMod1(y), CenterMod1(z));

        public double L1 => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        public bool IsZero => L1 < GlideZeroEps;
        public int HalfComponentCount => (IsHalfComponent(X) ? 1 : 0) + (IsHalfComponent(Y) ? 1 : 0) + (IsHalfComponent(Z) ? 1 : 0);
        public int QuarterComponentCount => (IsQuarterComponent(X) ? 1 : 0) + (IsQuarterComponent(Y) ? 1 : 0) + (IsQuarterComponent(Z) ? 1 : 0);
        public bool IsHalfVector => !IsZero && IsZeroOrHalf(X) && IsZeroOrHalf(Y) && IsZeroOrHalf(Z);

        public bool SameAs(GlideCoset other)
            => Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z) < GlideZeroEps;

        public bool OppositeOf(GlideCoset other)
            => Math.Abs(X + other.X) + Math.Abs(Y + other.Y) + Math.Abs(Z + other.Z) < GlideZeroEps;

        public static bool IsQuarterComponent(double v)
            => Math.Abs(Math.Abs(CenterMod1(v)) - 0.25) < GlideFractionEps;

        private static bool IsHalfComponent(double v)
            => Math.Abs(Math.Abs(v) - 0.5) < GlideFractionEps;

        private static bool IsZeroOrHalf(double v)
            => Math.Abs(v) < GlideZeroEps || IsHalfComponent(v);
    }

    #endregion

    #region 公開 API
    /// <summary>新規 <see cref="Bitmap"/> を確保して対称要素図を描画して返す。</summary>
    public static Bitmap RenderSymmetryElements(int seriesNumber, Size clientSize, ProjectionAxis axis = ProjectionAxis.C)
    {
        var bmp = NewBitmap(clientSize, out var g);
        try { DrawSymmetryElements(g, bmp.Size, seriesNumber, axis); } // (260504Cl) NewBitmap が 16px 未満をクランプするので bmp.Size を渡す
        finally { g.Dispose(); }
        return bmp;
    }

    /// <summary>(260504Cl 追加) 与えられた <see cref="Graphics"/> 上に対称要素図を描画する。
    /// 呼び出し側で背景クリア・<see cref="Graphics.SmoothingMode"/> 等の初期化を行うこと。</summary>
    public static void DrawSymmetryElements(Graphics g, Size clientSize, int seriesNumber, ProjectionAxis axis = ProjectionAxis.C)
    {
        if (!TryGetSym(seriesNumber, out var sym, out seriesNumber, out var msg))
        {
            if (msg != null) DrawCenteredText(g, clientSize, msg, Color.Gray);
            return;
        }
        var actualAxis = ResolveProjectionAxis(sym, axis);
        var proj = GetProjection(actualAxis);
        // 260505Cl: 立方晶 F 格子は upper-left 1/4 領域だけを描画する (図が混雑するため)。
        bool halfQuadrant = IsCubicFLattice(sym);
        double displayMaxS = halfQuadrant ? 0.5 : 1.0; // (260505Ch) clip ではなく描画対象座標そのものを制限する。
        var layout = ComputeCellLayout(clientSize, sym, actualAxis, halfQuadrant);
        DrawCellAndAxes(g, layout, proj, sym, halfQuadrant, showAxisLabels: false); // (260505Cl) 対称要素図では軸ラベル ("o", a, b 等) を出さない。一般位置図側だけで表示。
        if (halfQuadrant) DrawUpperLeftQuadrantLabel(g);

        var table = SymmetryElementsTable.Get(seriesNumber);
        if (table == null) return;

        using var pen        = new Pen(Color.Black, DefaultPenWidth);
        using var mirrorPen  = new Pen(Color.Black, MirrorPenWidth);
        using var inPlanePen = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [5f, 3f] };
        using var depthPen   = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [1f, 2.5f] };
        using var diagPen    = new Pen(Color.Black, MirrorPenWidth) { DashStyle = DashStyle.Custom, DashPattern = [5f, 2.5f, 1f, 2.5f] };
        using var ePen       = new Pen(Color.Black, MirrorPenWidth)
        {
            DashStyle = DashStyle.Custom,
            DashCap = DashCap.Round,
            // DashPattern = [0.1f, EGlideDotDashUnit, 0.1f, EGlideDotDashUnit, 5.0f, EGlideDotDashUnit] // 旧: dot-dot-dash
            DashPattern = [5.0f, EGlideDotDashUnit, 0.1f, EGlideDotDashUnit, 0.1f, EGlideDotDashUnit] // (260505Ch) e-glide は dash-dot-dot
        };
        using var fill  = new SolidBrush(Color.Black);
        using var white = new SolidBrush(Color.White);
        bool isCubic     = sym.CrystalSystemNumber == 7;
        bool isCubicHigh = IsCubicHighSym(sym); // (260506Cl) stereonet inset を持つ立方晶高対称群。
        var stereonetPositions = isCubicHigh ? GetStereonetDrawPositions(sym) : null; // (260506Cl) 1 描画中に複数箇所で使うのでキャッシュ。
        double basisDot = layout.Horz.X * layout.Vert.X + layout.Horz.Y * layout.Vert.Y; // 260510Ch
        double basisLen = Math.Sqrt((layout.Horz.X * layout.Horz.X + layout.Horz.Y * layout.Horz.Y) *
                                    (layout.Vert.X * layout.Vert.X + layout.Vert.Y * layout.Vert.Y)); // 260510Ch
        bool allowEGlide = basisLen > 1e-9 && Math.Abs(basisDot / basisLen) < 1e-6; // 260510Ch: 結晶系番号ではなく投影 metric で判断。

        var ctx = new ElementsContext
        {
            G = g, C = layout, Proj = proj,
            Pen = pen, MirrorPen = mirrorPen, InPlanePen = inPlanePen,
            DepthPen = depthPen, DiagPen = diagPen, EPen = ePen,
            Fill = fill, White = white,
            PerpendicularMirrors = [],
            DrawnSymmetryPlanes = [],
            StereonetAnchorKeys = isCubicHigh ? CollectStereonetAnchorKeys(layout, stereonetPositions) : null,
            DisplayMaxS = displayMaxS,
            // 旧: AllowEGlide = sym.CrystalSystemNumber != 5 && sym.CrystalSystemNumber != 6;
            AllowEGlide = allowEGlide, // 260510Ch: e-glide の可否は「投影面基底が直交するか」という一般条件へ集約。
            SpaceGroupHM = sym.SpaceGroupHMStr, // 260510Ch
        };

        // 旧: quadrantClip による後段の表示範囲制限は廃止。
        // 紙面平行 mirror 集約 / 紙面垂直 mirror draft 集約 / 紙面内 2/4/-4 軸 draft 集約。
        var parallelMirrors = new HashSet<(double Height, bool Glide, double GlideSx, double GlideSy)>();
        // (260503Cl) key に order を追加して 2 / 4 / -4 を別々に集める。draw 時に高次優先で重複を抑制。
        var inPlaneAxisDrafts = new Dictionary<(long, long, long, long, int, bool), InPlaneAxisArrowDraft>();
        foreach (var mp in table.PrincipalSymmetryPlanes) // 260513Ch: projection 非依存の主対称面を使い、m/e/d/n の主従整理を core 側へ寄せる。
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
        // foreach (var ax in table.SymmetryAxes) // 旧: 操作由来の従属 2/3 回軸も含めて収集していた。
        foreach (var ax in table.PrincipalSymmetryAxes) // 260512Ch: 対称要素としての主軸だけを描画対象にする。
        {
            int absO = Math.Abs(ax.Order);
            // 紙面内軸として扱うのは |order| = 2 (proper のみ; -2 は mirror) と |order| = 4 (proper / screw / -4)。
            if (absO != 2 && absO != 4) continue;
            if (absO == 2 && ax.Order < 0) continue;
            if (!IsAxisInPlane(ax.Direction, actualAxis)) continue;
            var (sx, sy, sz) = proj.ToScreen(ax.X, ax.Y, ax.Z);
            CollectInPlaneAxisArrows(layout, sx, sy, Mod1(sz), ax.Direction, actualAxis,
                ax.Order, ax.Screw, ax.FinCount, ax.EdgeStep, inPlaneAxisDrafts, displayMaxS);
        }

        DrawCollectedPerpendicularSymmetryPlanes(ctx);
        DrawParallelMirrorStack(g, layout, parallelMirrors, fill, isCubic: isCubic, allowEGlide: allowEGlide); // (260505Cl) cubic は紙面平行軸との干渉回避で bracket を離す。260510Ch: e-glide 可否を共通化。
        // (260503Cl) 紙面平行軸の anchor が斜め回転軸の foot / 紙面垂直回転軸 (4_2 など) の position と重なる場合、
        // 矢印を axis 方向にずらして点記号と重ならないようにする。
        var diagonalFootKeys = CollectDiagonalAxisFootKeys(table, layout, actualAxis, displayMaxS);
        var perpendicularAxisKeys = CollectPerpendicularAxisPositionKeys(table, layout, actualAxis, displayMaxS);
        // 260505Cl: m-3m / -43m 系では stereonet inset の輪郭を避けるため in-plane axis を radius+gap px 前進させる。
        float stereonetArrowShift = isCubicHigh
            ? CubicStereonetInsetRadius + StereonetArrowOutsideGap
            : 0f;
        DrawCollectedInPlaneAxisArrows(g, fill, inPlaneAxisDrafts, diagonalFootKeys, perpendicularAxisKeys,
                                       ctx.StereonetAnchorKeys, stereonetArrowShift);
        // 260502Cl 追加: 立方晶 [111] 系 体対角 3 回軸の描画。
        // 同位置で垂直回転軸 (lens 等) と重なる場合は垂直軸を上に出すため、こちらを先に描く。
        // 260505Cl: m-3m / -43m 系では各 stereonet 位置に inset を描画し、
        // それらの位置にある斜め 3 回軸の cell-side 描画はスキップ。
        HashSet<(long, long)> stereonetSkipKeys = isCubicHigh ? ComputeStereonetSkipKeys(stereonetPositions) : null;
        DrawDiagonalRotationMarks(ctx, table, actualAxis, skipPositionKeys: stereonetSkipKeys);
        // 旧: DrawPerpendicularRotationMarks(ctx, table, actualAxis, isCubic: isCubic); // (260505Cl) cubic 限定で 4_n と -4 の重なりは -4 を優先。
        DrawPerpendicularRotationMarks(ctx, table, actualAxis, isCubic: isCubic); // 260510Cl: cubic 限定で -4 と -1 が同位置に重なる場合は -4 を抑制 (ITA: 4_n と -1 で表記)。Fd-3m(1) vertex のように -1 が同位置に無い -4 はそのまま描画。
        DrawInversions(ctx, table.InversionCenters);
        if (isCubicHigh) DrawCubicStereonetInsets(ctx, table, actualAxis, stereonetPositions);
    }
    #endregion

    #region 紙面垂直 点記号
    /// <summary>軸方向が投影軸に平行な軸を点記号として描画。低次は高次に隠され、-N と同位置の +N があれば -N を捨て、-N(z≠0) は +N_k に置換。
    /// 旧: (260505Cl) isCubic=true で cubic 限定の優先規則: 同位置に -4 と 4_n (4_1/4_2/4_3) が重なる場合、4_n を抑制し -4 を残す。
    /// 旧: この場合 -4 は螺旋置換せずそのまま描き、反転中心の高さラベルを記号横に併記する。
    /// 260510Cl: ITA 規約は逆向き — -4 と -1 は同一軸 (同一投影位置) に重ねず、4_n と -1 だけで表記する。
    /// したがって isCubic=true では同投影位置に -1 (反転中心) を持つ -4 を描画スキップし、4_n はそのまま描く。
    /// Fd-3m(1) vertex のように -1 が同位置に無い -4 (例: site 対称 -43m) はそのまま描画。</summary>
    private static void DrawPerpendicularRotationMarks(ElementsContext ctx, Crystallography.SymmetryElementsTable table,
                                                       ProjectionAxis projAxis, bool isCubic = false)
    {
        // var axes = table.SymmetryAxes; // 旧: 高次軸に含まれる低次軸もここで後段抑制していた。
        var axes = table.PrincipalSymmetryAxes; // 260512Ch
        // 同位置の高次 proper rotation 集合 (低次抑制 / -4 抑制用)。260512Ch: -3/-6 は principal 軸に来ない。
        var covered2 = new HashSet<(int, int)>();
        var covered3 = new HashSet<(int, int)>();
        var properRotations = new HashSet<(int N, int Sx, int Sy)>();
        // 旧: (260505Cl) cubic で 4_n を抑制するための -4 射影位置集合。各 -4 軸の高さは 2 周目で各 axis の sz から直接読む。
        // 旧: var minusFourKeys = isCubic ? new HashSet<(int, int)>() : null;
        // 260510Cl: ITA 規約に合わせ「-1 と同位置の -4 を抑制」する向きへ反転。-1 (反転中心) の投影位置を集める。
        var inversionKeys = isCubic
            ? table.InversionCenters
                .Select(c => ctx.Proj.ToScreen(c.X, c.Y, c.Z))
                .Select(t => ((int)Math.Round(Mod1(t.Sx) * 10000), (int)Math.Round(Mod1(t.Sy) * 10000)))
                .ToHashSet()
            : null; // 260510Cl: foreach + Add を LINQ 化。
        var cubicMinusFourWithoutInversionKeys = isCubic ? new HashSet<(int, int)>() : null; // 260510Ch
        var cubicMinusFourHeights = isCubic ? new Dictionary<(int, int), double>() : null; // 260510Ch
        // 旧: -6 は 3/m と等価として minusSixKeys/minusSixHeights を集め、高さ付き -6 を描いていた。
        // 260512Ch: -6 は独立軸から外し、3 + m の構成要素表示に寄せるため -6 専用状態は不要。
        foreach (var ax in axes)
        {
            if (!IsAxisPerpendicularToProjection(ax.Direction, projAxis)) continue;
            var (sx, sy, sz) = ctx.Proj.ToScreen(ax.X, ax.Y, ax.Z);
            var key = ((int)Math.Round(Mod1(sx) * 10000), (int)Math.Round(Mod1(sy) * 10000));
            if (isCubic && ax.Order == -4 && !inversionKeys.Contains(key))
            {
                cubicMinusFourWithoutInversionKeys.Add(key); // 260510Ch: -1 が同軸に無い -4 は ITA で -4 として残す。
                double h4 = Mod1(sz);
                if (h4 > FracEps && h4 < 1 - FracEps &&
                    (!cubicMinusFourHeights.TryGetValue(key, out double curH4) || h4 < curH4))
                    cubicMinusFourHeights[key] = h4; // 260510Ch: 同じ -4 投影位置の高さ表示は最小の非ゼロ高さ 1 個だけにする。
            }
            // 旧: (260505Cl) cubic 限定で -4 位置だけ別収集して 4_n を抑制していた。
            // 旧: bool isCubicMinusFour = isCubic && ax.Order == -4;
            // 旧: if (ax.Order <= 0 && !isCubicMinusFour) continue;
            // 旧: if (isCubicMinusFour) { minusFourKeys.Add(key); continue; }
            // 260510Cl: -4 抑制判定は inversionKeys (反転中心の投影位置) と照合するので、ここでは proper rotation のみ収集すれば足りる。
            if (ax.Order <= 0) continue;
            int absO = ax.Order;
            if (absO is 3 or 4 or 6) covered2.Add(key); // (260503Ch) [ITA-D1] 同位置の高次記号が defining symbol になる場合、低次 2 回記号は描かない。
            if (absO == 6) covered3.Add(key); // (260503Ch) [ITA-D1] 6 回記号が defining symbol になる場合、同位置の 3 回記号は描かない。
            if (!ax.Screw && absO is (2 or 3 or 4 or 6)) properRotations.Add((absO, key.Item1, key.Item2)); // (260503Ch) [ITA-D1] 同位置に proper N があれば -N は別途重ねない。
        }

        var drawnMinusFourKeys = new HashSet<(int, int)>(); // 260510Ch
        foreach (var ax in axes)
        {
            int o = ax.Order, absO = Math.Abs(o);
            if (absO is not (2 or 3 or 4 or 6)) continue;
            if (!IsAxisPerpendicularToProjection(ax.Direction, projAxis)) continue;
            var (sx, sy, sz) = ctx.Proj.ToScreen(ax.X, ax.Y, ax.Z);
            var key = ((int)Math.Round(Mod1(sx) * 10000), (int)Math.Round(Mod1(sy) * 10000));
            bool keepMinusFourWithLabel = isCubic && o == -4 && !inversionKeys.Contains(key); // 260510Ch
            if (isCubic && o > 0 && absO == 4 && cubicMinusFourWithoutInversionKeys.Contains(key)) continue; // 260510Ch: -1 が無い同軸 -4 は 4_n より -4 を優先。
            if (keepMinusFourWithLabel && !drawnMinusFourKeys.Add(key)) continue; // 260510Ch: -4 高さラベルを同一投影位置で 1 個に畳む。
            if (absO == 2 && covered2.Contains(key)) continue; // (260503Ch) [ITA-D1] 高次点記号が同じ位置を定義するため、2 回点記号を省く。
            if (absO == 3 && o > 0 && covered3.Contains(key)) continue; // (260503Ch) [ITA-D1] 6 回点記号が同じ位置を定義するため、3 回点記号を省く。
            // 旧: if (o < 0 && properRotations.Contains((absO, key.Item1, key.Item2))) continue;
            if (o < 0 && properRotations.Contains((absO, key.Item1, key.Item2)) && !keepMinusFourWithLabel) continue; // (260503Ch) [ITA-D1] proper N と同位置の -N は重ねない。260512Ch: principal 側の負軸は -4 のみ。
            // 旧: if (isCubic && o > 0 && absO == 4 && ax.Screw && minusFourKeys.Contains(key)) continue; // (260505Cl) cubic で -4 と同位置の 4_n は抑制。
            if (isCubic && o == -4 && inversionKeys.Contains(key)) continue; // 260510Cl: ITA 規約 — cubic で -4 と -1 が同投影位置に重なる場合は -4 を描かない (4_n + -1 で表記)。Fd-3m(1) のように -1 が無い -4 はそのまま描画。

            // -4 で inversion 点 z_c ≠ 0 のときは 4_k 螺旋 + inversion(z=0) と等価 (反転中心は別途描画)。
            int order = o;
            int finCount = ax.FinCount, edgeStep = ax.EdgeStep;
            // 旧: (260505Cl) cubic で 4_n を抑制した位置の -4 は螺旋に置換せず -4 のまま描き、反転中心高さを併記。
            // 旧: bool keepMinusFourWithLabel = isCubic && o == -4 && minusFourKeys.Contains(key);
            // 260510Ch: -1 が同軸にある場合だけ -4 を suppress し、-1 が無い -4 は高さ付き -4 として残す。
            // 260510Ch: -1 が同軸にある -4 は suppress 済み。-1 が無い -4 は screw 置換せず高さ付き -4 として描く。
            if (o < 0 && absO == 4 && !keepMinusFourWithLabel)
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
            // 旧: string label = keepMinusFourWithLabel ? HeightLabel(Mod1(sz)) : keepMinusSixWithLabel ? HeightLabel(minusSixHeights[key]) : null;
            // string label = keepMinusSixWithLabel ? HeightLabel(minusSixHeights[key]) : null; // 旧: -1 無し -4 まで screw 置換してしまい、Fd-3m(1) の -4 高さ表示が消えていた。
            string label = keepMinusFourWithLabel && cubicMinusFourHeights.TryGetValue(key, out double minH4)
                ? HeightLabel(minH4)
                : null; // 260512Ch: -6 高さラベルは不要。
            float labelRadius = FourFoldRadius;
            if (label != null && order > 0 && finCount > 0) labelRadius += ScrewFinTailLen; // 260510Ch: screw fin が記号外へ伸びる分も高さラベル距離に含める。
            // 旧: float labelRadiusScale = keepMinusFourWithLabel ? 0.5f : keepMinusSixWithLabel ? 0.5f : 1f;
            // float labelRadiusScale = 1f; // 旧: -4 も通常半径にしたため高さラベルが離れすぎた。
            float labelRadiusScale = keepMinusFourWithLabel ? 0.5f : 1f; // 260510Ch

            foreach (var (dxf, dyf) in EdgeReplicatedPoints(sx, sy, ctx.DisplayMaxS))
            {
                var pt = ctx.C.ToScreen(dxf, dyf);
                if (absO == 2) DrawTwofoldPerp(ctx.G, ctx.Fill, pt, ax.Screw);
                else if (absO == 3) DrawRotationPerp(ctx.G, ctx.Fill, ctx.White, pt, order, finCount, edgeStep, 3, ThreeFoldRadius);
                else if (absO == 4) DrawRotationPerp(ctx.G, ctx.Fill, ctx.White, pt, order, finCount, edgeStep, 4, FourFoldRadius);
                else if (absO == 6) DrawRotationPerp(ctx.G, ctx.Fill, ctx.White, pt, order, finCount, edgeStep, 6, SixFoldRadius);
                if (label != null) DrawHeightLabel(ctx.G, ctx.Fill, pt, labelRadius, label, radiusScale: labelRadiusScale);
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
            foreach (var (sxF, syF) in EdgeReplicatedPoints(sx, sy, ctx.DisplayMaxS))
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
            if (h != null) DrawHeightLabel(ctx.G, ctx.Fill, pt, InversionR, h);
        }
    }

    /// <summary>(260505Cl) 点記号 (中心 pt、半径 symbolR) の右上に高さラベル h を描く。記号外側 + 1 px に左端、記号上 + 2 px に下端を合わせる。
    /// (260505Ch) radiusScale: ラベル位置算出に使う実効半径倍率。立方晶 -4 のように外接四角形の角が空いている記号は小さめにして記号寄りに引き寄せる。</summary>
    private static void DrawHeightLabel(Graphics g, Brush fill, PointF pt, float symbolR, string h, float radiusScale = 1f)
    {
        float r = symbolR * radiusScale;
        // 旧: var sz = g.MeasureString(h, HeightLabelFont);
        var sz = MeasureTightString(g, h, HeightLabelFont); // 260510Ch: glyph 正味サイズ。
        // g.DrawString(h, HeightLabelFont, fill, pt.X + r + HeightLabelGapX, pt.Y - r - sz.Height + HeightLabelGapY); // 旧
        DrawTightString(g, fill, h, HeightLabelFont, pt.X + r + HeightLabelGapX, pt.Y - r - sz.Height + HeightLabelGapY); // 260510Ch
    }
    #endregion

    #region 軸方向の分類
    private static bool IsAxisPerpendicularToProjection((int U, int V, int W) d, ProjectionAxis a)
    {
        // (260504Ch) ProjectionAxis ごとの switch を、投影面成分 0 + depth 成分非 0 の一般判定へ整理。
        var (sx, sy) = ProjectVector(d.U, d.V, d.W, a);
        return sx == 0 && sy == 0 && ProjectedDepth(d.U, d.V, d.W, a) != 0;
    }

    private static bool IsAxisInPlane((int U, int V, int W) d, ProjectionAxis a)
    {
        var (sx, sy) = ProjectVector(d.U, d.V, d.W, a);
        return ProjectedDepth(d.U, d.V, d.W, a) == 0 && (sx != 0 || sy != 0);
    }

    // 260502Cl 追加: 立方晶系 [111] 系の体対角 3 回軸など、紙面に対し斜め (depth と in-plane の両方に成分を持つ) な軸の判定。
    private static bool IsAxisDiagonalToProjection((int U, int V, int W) d, ProjectionAxis a)
        => !IsAxisPerpendicularToProjection(d, a) && !IsAxisInPlane(d, a);
    #endregion

    #region 軸プリミティブ (紙面垂直 lens / 正多角形 / 螺旋 fin)
    // 260510Cl: 紙面垂直/斜め/inset stereonet で共有する図形プリミティブを 1 region に集約。
    /// <summary>紙面垂直 2 (2_1) 軸: vesica piscis lens を塗り潰し。screw=互い違い円弧。-4 から呼ぶ際は scale で縮小。
    /// (260504Cl) widthScale: 長軸はそのままで幅 (短軸方向) のみ独立に倍率を掛ける。
    /// (260504Cl) finSweepScale: 2_1 螺旋 fin (円弧) の sweep 角度の倍率。stereonet 用に短くする。</summary>
    private static void DrawTwofoldPerp(Graphics g, Brush fill, PointF pt, bool screw, float scale = 1f, float rotationDeg = 0f,
                                        float widthScale = 1f, float finSweepScale = 1f)
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

            float halfW = TwofoldHalfW * scale * widthScale, halfH = TwofoldHalfH * scale; // (260504Cl) 幅のみ widthScale で独立調整
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
            // (260503Cl) 2_1 螺旋: フィンをレンズ側に食い込ませた上でフィンにハローを巻く。
            //   こうすると レンズ角での AA 境界 1px すき間 (= フィン途切れ) も解消し、
            //   フィン基部がレンズ body に確実に接続する。描画順は: lens ハロー → フィンハロー → レンズ塗り → フィン本体。
            //   レンズ塗りで overlap 部のフィンハローを上書きするので、フィンハローはレンズ外側だけに残る。
            float rightFinStart = 180f + halfAngle - TwofoldFinOverlapDeg;
            float leftFinStart  = halfAngle - TwofoldFinOverlapDeg;
            // 260504Cl: 可視部分 (= ScrewFinSweepDeg) のみ finSweepScale でスケール、overlap は不変。
            float finSweep      = ScrewFinSweepDeg * finSweepScale + TwofoldFinOverlapDeg;
            using (var lensHalo = new Pen(Color.White, SymbolHaloPenWidth) { LineJoin = LineJoin.Round })
                g.DrawPath(lensHalo, path); // (260505Ch) mirror/glide 線上の 2_1 lens が埋もれないよう白縁を復帰。
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
        => [.. Enumerable.Range(0, N).Select(i =>
        {
            double th = -Math.PI / 2 + i * 2 * Math.PI / N;
            return new PointF(c.X + (float)(r * Math.Cos(th)), c.Y + (float)(r * Math.Sin(th)));
        })]; // 260510Cl: ループを Enumerable.Range + collection expression へ。

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

    /// <summary>紙面垂直 3/4/6 回と -4 回反を描く。260512Ch: -3/-6 は principal 軸から外れたためここでは描かない。</summary>
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
        // 旧: N == 3 なら -3 (黒三角 + 中心白丸)、N == 6 なら -6 (白六角 + 内接黒三角) を描いていた。
        if (N != 4) return; // 260512Ch: 負の 3/6 回反は独立描画しない。
        g.FillPolygon(white, poly);
        DrawTwofoldPerp(g, fill, pt, screw: false, scale: MinusFourInnerLensScale);
        g.DrawPolygon(outline, poly);
    }
    #endregion

    #region 紙面斜め 回転軸 (立方晶 [111]/[110] 系)
    /// <summary>紙面に対し斜め (例: 立方晶 [111], [101]) の 2/3 回回転軸 (proper / screw) を描画。-N 等は未対応。
    /// foot 位置は axis の depth=0 平面との交点に取る (SymmetryElementsTable 格納 position は軸線上の任意点なので)。
    /// (260505Cl) skipPositionKeys: 与えられた (Mod1(sx), Mod1(sy)) キーに該当する位置の cell-side 描画はスキップ。</summary>
    private static void DrawDiagonalRotationMarks(ElementsContext ctx, Crystallography.SymmetryElementsTable table,
                                                  ProjectionAxis projAxis, HashSet<(long, long)> skipPositionKeys = null)
    {
        // var axes = table.SymmetryAxes; // 旧
        var axes = table.PrincipalSymmetryAxes; // 260512Ch
        var drawnAxes = new HashSet<(long Sx, long Sy, int Order, int U, int V, int W, bool Screw, int Fin, int Edge)>();

        foreach (var ax in axes)
        {
            if (ax.Order is not (2 or 3)) continue;
            if (!IsAxisDiagonalToProjection(ax.Direction, projAxis)) continue;

            int finCount = ax.FinCount, edgeStep = ax.EdgeStep;

            var (u, v, w) = ax.Direction;
            if (ProjectedDepth(u, v, w, projAxis) < 0)
                (u, v, w) = (-u, -v, -w); // (260505Ch) shaft 方向だけ depth 正側へ統一し、3_1/3_2 の edgeStep は変換しない。
            var (dSx, dSy) = ProjectVector(u, v, w, projAxis);
            float dirX = (float)(dSx * ctx.C.Horz.X + dSy * ctx.C.Vert.X), dirY = (float)(dSx * ctx.C.Horz.Y + dSy * ctx.C.Vert.Y);
            float dlen = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            if (dlen < 1e-3f) continue;
            dirX /= dlen; dirY /= dlen;

            if (!TryGetDiagonalAxisFootprint(ax, projAxis, out double sx, out double sy)) continue; // (260505Ch) 斜め 3 回軸も素の axis から foot を取る。
            var footKey = PeriodicPositionKey(sx, sy); // (260505Ch) 1.0 近傍を 0.0 に折り畳み、stereonet foot と周期同値に比較する。
            if (skipPositionKeys != null && skipPositionKeys.Contains(footKey)) continue;
            if (!drawnAxes.Add((footKey.X, footKey.Y, ax.Order, u, v, w, ax.Screw, finCount, edgeStep))) continue;

            foreach (var (dxf, dyf) in EdgeReplicatedPoints(sx, sy, ctx.DisplayMaxS))
            {
                var anchor = ctx.C.ToScreen(dxf, dyf);
                if (ax.Order == 2) DrawDiagonalTwofoldPerp(ctx.G, ctx.Fill, anchor, dirX, dirY, ax.Screw);
                else DrawDiagonalThreefoldPerp(ctx.G, ctx.Fill, anchor, dirX, dirY, finCount, edgeStep);
            }
        }
    }

    private static bool TryGetDiagonalAxisFootprint(SymmetryAxis ax, ProjectionAxis projAxis, out double sx, out double sy)
    {
        sx = sy = 0;
        double depthPos = ProjectedDepth(ax.X, ax.Y, ax.Z, projAxis);
        double depthDir = ProjectedDepth(ax.Direction.U, ax.Direction.V, ax.Direction.W, projAxis);
        if (Math.Abs(depthDir) < 1e-9) return false;
        double t = -depthPos / depthDir;
        double x0 = ax.X + t * ax.Direction.U, y0 = ax.Y + t * ax.Direction.V, z0 = ax.Z + t * ax.Direction.W;
        var (px, py, _) = GetProjection(projAxis).ToScreen(x0, y0, z0);
        sx = Mod1(px); sy = Mod1(py);
        return true;
    }

    /// <summary>立方晶 [111] 系 体対角 3 回回転軸を ITC Vol.A 風に描画。foot 黒丸 → shaft1 → 三角 (重心に白丸) → shaft2 (dir 方向, 端 round) の構造。
    /// 三角は dir と CCW 90° 方向の 2 脚から成る直角三角形を画面 CCW 45° 回転、重心を shaft1 の端点に一致させる。
    /// finCount/edgeStep が非零なら 3_k 螺旋として三角の各頂点に fin (DrawScrewFins と同形式) を生やす。</summary>
    private static void DrawDiagonalThreefoldPerp(Graphics g, Brush fill, PointF anchor, float dirX, float dirY,
                                                  int finCount = 0, int edgeStep = 0)
    {
        PointF lineEnd = new(anchor.X + dirX * DiagonalShaft1Len, anchor.Y + dirY * DiagonalShaft1Len);
        PointF tail    = new(lineEnd.X + dirX * DiagonalShaft2Len, lineEnd.Y + dirY * DiagonalShaft2Len);

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
        PointF lineEnd = new(anchor.X + dirX * DiagonalShaft1Len, anchor.Y + dirY * DiagonalShaft1Len);
        PointF tail    = new(lineEnd.X + dirX * DiagonalShaft2Len, lineEnd.Y + dirY * DiagonalShaft2Len);

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

    #region 紙面内 2(2_1) / 4(4_n) / -4 軸 矢印
    /// <summary>(260503Cl 更新) Order / FinCount / EdgeStep を保持して draw 時に頭部を分岐する。</summary>
    private readonly record struct InPlaneAxisArrowDraft(PointF Anchor, double OutUx, double OutUy,
                                                          int Order, bool Screw, int FinCount, int EdgeStep, double Sz);

    /// <summary>紙面内 2/4/-4 軸の矢印を draft に集約。同一 (位置, 方向, order, screw) で複数高さがあれば最小 sz を残す。</summary>
    private static void CollectInPlaneAxisArrows(CellLayout c, double sx, double sy, double sz,
                                                  (int U, int V, int W) dir, ProjectionAxis projAxis,
                                                  int order, bool screw, int finCount, int edgeStep,
                                                  Dictionary<(long, long, long, long, int, bool), InPlaneAxisArrowDraft> drafts,
                                                  double displayMaxS = 1.0)
    {
        var (dSx, dSy) = ProjectVector(dir.U, dir.V, dir.W, projAxis);
        double axisX = dSx * c.Horz.X + dSy * c.Vert.X, axisY = dSx * c.Horz.Y + dSy * c.Vert.Y;
        double axisLen = Math.Sqrt(axisX * axisX + axisY * axisY);
        if (axisLen < 1e-6) return;
        double ux = axisX / axisLen, uy = axisY / axisLen;

        // 角に接する隣接セル由来の軸も拾うため 5×5 セル走査
        for (int ox = -2; ox <= 2; ox++) for (int oy = -2; oy <= 2; oy++)
            ClipAxisArrows(sx + ox, sy + oy);

        void ClipAxisArrows(double lineSx, double lineSy)
        {
            if (!TryClipLineThroughUnitCell(lineSx, lineSy, dSx, dSy, out double tMin, out double tMax, displayMaxS)) return; // (260505Ch) F 格子では [0,1/2]² に対して直接クリップ。
            if (Math.Abs(tMax - tMin) < 1e-8)
            {
                double px = lineSx + tMin * dSx, py = lineSy + tMin * dSy;
                if (!OnCellBoundary(px, py, displayMaxS)) return;
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
            if (InsideCell(px + sign * dSx * eps, py + sign * dSy * eps, displayMaxS)) return;
            AddArrow(px, py, sign * ux, sign * uy);
        }

        void AddArrow(double px, double py, double outUx, double outUy)
        {
            px = NormalizeBoundary(px, displayMaxS); py = NormalizeBoundary(py, displayMaxS);
            if (!OnCellBoundary(px, py, displayMaxS)) return;
            var p = c.ToScreen(px, py);
            var key = ((long)Math.Round(p.X * 1000), (long)Math.Round(p.Y * 1000),
                (long)Math.Round(outUx * 1000), (long)Math.Round(outUy * 1000), order, screw);
            if (drafts.TryGetValue(key, out var existing) && existing.Sz <= sz) return;
            drafts[key] = new InPlaneAxisArrowDraft(p, outUx, outUy, order, screw, finCount, edgeStep, sz);
        }
    }

    private static bool InsideCell(double x, double y, double maxS = 1.0) => x > 1e-6 && x < maxS - 1e-6 && y > 1e-6 && y < maxS - 1e-6;
    private static bool OnCellBoundary(double x, double y, double maxS = 1.0) =>
        x > -1e-6 && x < maxS + 1e-6 && y > -1e-6 && y < maxS + 1e-6 &&
        (Math.Abs(x) < 1e-6 || Math.Abs(x - maxS) < 1e-6 || Math.Abs(y) < 1e-6 || Math.Abs(y - maxS) < 1e-6);
    private static double NormalizeBoundary(double x, double maxS = 1.0) =>
        Math.Abs(x) < 1e-6 ? 0 : Math.Abs(x - maxS) < 1e-6 ? maxS : x;

    /// <summary>(260503Cl 追加) 斜め回転軸 (3/2 + 立方晶 [111]/[110] 系) の foot を screen 座標 key にして返す。
    /// 紙面平行軸が同位置に来た場合、平行四辺形などが foot 黒丸を覆ってしまうため、ずらし判定に使う。</summary>
    private static HashSet<(long X, long Y)> CollectDiagonalAxisFootKeys(Crystallography.SymmetryElementsTable table,
                                                                          CellLayout layout, ProjectionAxis projAxis,
                                                                          double displayMaxS = 1.0)
    {
        // 260510Cl: out 引数を含むため LINQ 1 行化はしないが、内側ループは EdgeReplicatedPoints で陳述化。
        var result = new HashSet<(long, long)>();
        // foreach (var ax in table.SymmetryAxes) // 旧
        foreach (var ax in table.PrincipalSymmetryAxes) // 260512Ch
        {
            if (ax.Order is not (2 or 3)) continue;
            if (!IsAxisDiagonalToProjection(ax.Direction, projAxis)) continue;
            if (!TryGetDiagonalAxisFootprint(ax, projAxis, out double sx, out double sy)) continue;
            foreach (var (dxf, dyf) in EdgeReplicatedPoints(sx, sy, displayMaxS))
                result.Add(ScreenPointKey(layout.ToScreen(dxf, dyf)));
        }
        return result;
    }

    /// <summary>(260505Cl) m-3m / -43m 系で stereonet inset を描く位置のスクリーン pixel キー集合。
    /// in-plane axis arrow を stereonet 輪郭の外側へずらす判定に用いる。</summary>
    private static HashSet<(long X, long Y)> CollectStereonetAnchorKeys(CellLayout layout, (double Sx, double Sy)[] positions)
        => [.. positions.Select(p => ScreenPointKey(layout.ToScreen(p.Sx, p.Sy)))]; // 260510Cl: foreach + Add を collection expr へ。

    /// <summary>(260503Cl 追加) 紙面垂直回転軸 (2/3/4/6 と螺旋形・反転形) の position を screen 座標 key にして返す。
    /// 紙面平行軸の anchor が同位置に来ると、垂直軸の点記号 (lens / 多角形) と重なるため、shift 判定に使う。</summary>
    private static HashSet<(long X, long Y)> CollectPerpendicularAxisPositionKeys(Crystallography.SymmetryElementsTable table,
                                                                                   CellLayout layout, ProjectionAxis projAxis,
                                                                                   double displayMaxS = 1.0)
    {
        var result = new HashSet<(long, long)>();
        var proj = GetProjection(projAxis);
        // foreach (var ax in table.SymmetryAxes) // 旧
        foreach (var ax in table.PrincipalSymmetryAxes) // 260512Ch
        {
            int absO = Math.Abs(ax.Order);
            if (absO is not (2 or 3 or 4 or 6)) continue;
            if (!IsAxisPerpendicularToProjection(ax.Direction, projAxis)) continue;
            var (sx, sy, _) = proj.ToScreen(ax.X, ax.Y, ax.Z);
            foreach (var (dxf, dyf) in EdgeReplicatedPoints(sx, sy, displayMaxS))
                result.Add(ScreenPointKey(layout.ToScreen(dxf, dyf))); // (260505Cl)
        }
        return result;
    }

    private static void DrawCollectedInPlaneAxisArrows(Graphics g, Brush fill,
        Dictionary<(long, long, long, long, int, bool), InPlaneAxisArrowDraft> drafts,
        HashSet<(long X, long Y)> diagonalFootKeys,
        HashSet<(long X, long Y)> perpendicularAxisKeys,
        HashSet<(long X, long Y)> stereonetAnchorKeys = null,
        float stereonetArrowShift = 0f)
    {
        if (drafts.Count == 0) return;
        using var pen = new Pen(Color.Black, InPlaneAxisPenWidth);
        using var white = new SolidBrush(Color.White); // (260503Cl) -4 軸の白塗り平行四辺形 / 2/2_1 と並列時の labels には使わない。
        foreach (var group in drafts.Values
            .GroupBy(d => ((long)Math.Round(d.Anchor.X * 1000), (long)Math.Round(d.Anchor.Y * 1000),
                           (long)Math.Round(d.OutUx * 1000), (long)Math.Round(d.OutUy * 1000)))) // (260502Ch)
        {
            var allDrafts = group.ToList();
            // (260503Cl) ITC: 同位置・同方向・同高さで proper 4 (4 / 4_n) と -4 が共存する場合、proper 4 を優先 (Pm-3m の 4 + -4、Fm-3c の 4_2 + -4 等)。
            // 260510Cl: HashSet new + Where().ToList() 再代入を ToHashSet + List.RemoveAll に。
            var properFourSzKeys = allDrafts
                .Where(d => d.Order == 4)
                .Select(d => (long)Math.Round(d.Sz * 1000))
                .ToHashSet();
            allDrafts.RemoveAll(d => d.Order == -4 && properFourSzKeys.Contains((long)Math.Round(d.Sz * 1000)));
            // (260503Cl) ITC: 同位置・同方向で |order|=4 の軸が存在する場合、|order|=2 の点記号は描かない (4 が defining symbol)。
            bool hasFourfold = allDrafts.Any(d => Math.Abs(d.Order) == 4);
            var list = (hasFourfold ? allDrafts.Where(d => Math.Abs(d.Order) == 4) : allDrafts.AsEnumerable())
                .OrderBy(d => d.Sz).ThenBy(d => d.Screw ? 0 : 1).ToList();
            // (260503Cl) anchor が斜め軸 foot / 垂直軸 position と一致するか判定 (group 内で共通)。両方該当時は大きい方を採用。
            //   重複が無い場合は InPlaneArrowDefaultShift を使う。
            float axisShift = InPlaneArrowDefaultShift;
            if (list.Count > 0)
            {
                var anchorKey = ScreenPointKey(list[0].Anchor);
                // 260505Cl: stereonet inset があれば優先 (shift = radius + StereonetArrowOutsideGap、他より大きい)。
                if (stereonetAnchorKeys != null && stereonetAnchorKeys.Contains(anchorKey))
                    axisShift = stereonetArrowShift;
                else if (diagonalFootKeys.Contains(anchorKey))
                    axisShift = InPlaneArrowDiagonalFootShift;
                else if (perpendicularAxisKeys.Contains(anchorKey))
                    axisShift = InPlaneArrowPerpendicularShift;
            }
            // (260505Cl) 4_n / 4 / -4 が並ぶときは平行四辺形ヘッドが大きいので perpendicular pitch を 2 倍にして重なりを回避。
            double perpPitch = hasFourfold ? InPlaneArrowGroupPitch4 : InPlaneArrowGroupPitch2;
            for (int i = 0; i < list.Count; i++)
            {
                var d = list[i];
                double perpOffset = list.Count == 1 ? 0 : (i - (list.Count - 1) / 2.0) * perpPitch; // (260502Ch) 同じ投影線上の 2 / 2_1 / 4_n を少し perpendicular に並べる。
                float ox = (float)(-d.OutUy * perpOffset), oy = (float)(d.OutUx * perpOffset);
                // (260503Cl) axis 方向 (OutUx, OutUy) に axisShift だけ前進させる。
                ox += (float)(d.OutUx * axisShift);
                oy += (float)(d.OutUy * axisShift);
                var anchor = new PointF(d.Anchor.X + ox, d.Anchor.Y + oy);
                int absO = Math.Abs(d.Order);
                float shaftLen = absO == 4 ? InPlaneArrowExt + Shaft4Extra : InPlaneArrowExt;
                // (260503Cl) shaft 右端 = lineEnd。各記号 (2/2_1=三角重心、4/4_n/-4=平行四辺形中心) はここに合わせて配置。
                var lineEnd = new PointF((float)(anchor.X + shaftLen * d.OutUx), (float)(anchor.Y + shaftLen * d.OutUy));
                g.DrawLine(pen, anchor, lineEnd);
                if (absO == 2)
                    DrawInPlaneAxisArrowhead(g, fill, pen, lineEnd, d.OutUx, d.OutUy, halfHead: d.Screw);
                else if (d.Order == 4)
                    DrawInPlaneFourfoldHead(g, fill, pen, lineEnd, d.OutUx, d.OutUy, d.FinCount, d.EdgeStep);
                else if (d.Order == -4)
                    DrawInPlaneMinusFourfoldHead(g, fill, white, pen, lineEnd, d.OutUx, d.OutUy);
                string h = HeightLabel(d.Sz);
                if (h == null) continue; // (260503Ch) [ITA-D2] 高さ 0 は無標記、非ゼロ代表高さだけを表示する。
                // 旧: var lbl = g.MeasureString(h, HeightLabelFont);
                var lbl = MeasureTightString(g, h, HeightLabelFont); // 260510Ch: GDI+ の余白込み MeasureString ではなく glyph 正味サイズを使う。
                // (260502Ch) 非ゼロ高さは矢印先端にくっつけて表示する。
                // (260503Cl) 4 回軸も 2 回軸も visualTip = anchor + ArrowVisualTipOffset*dir で同じ位置に到達する。
                var tip = new PointF((float)(anchor.X + ArrowVisualTipOffset * d.OutUx), (float)(anchor.Y + ArrowVisualTipOffset * d.OutUy));
                bool horiz = Math.Abs(d.OutUx) >= Math.Abs(d.OutUy);
                float lx = horiz
                    ? (d.OutUx >= 0 ? tip.X + InPlaneAxisLabelGap : tip.X - lbl.Width - InPlaneAxisLabelGap)
                    : tip.X - lbl.Width / 2;
                float ly = horiz
                    ? tip.Y - lbl.Height / 2
                    : (d.OutUy >= 0 ? tip.Y + InPlaneAxisLabelGap : tip.Y - lbl.Height - InPlaneAxisLabelGap);
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
                    lx = ox > 0 ? Math.Max(lx, midX) : Math.Min(lx, midX - lbl.Width); // 260510Ch
                }
                // g.DrawString(h, HeightLabelFont, fill, lx, ly); // 旧: glyph 外側の余白を含む座標で描いていた。
                DrawTightString(g, fill, h, HeightLabelFont, lx, ly); // (260505Ch) 呼び出し元の黒 brush を再利用。260510Ch: tight bbox 基準。
            }
        }
    }

    // (260504Ch) glide arrow は常に full head。2_1 軸の half head は DrawInPlaneAxisArrowhead に閉じ込める。
    // (260506Cl) scale=1 を既定値として、stereonet inset 用の倍率付き呼び出しもこの 1 関数で処理する。
    private static void DrawArrowhead(Graphics g, Brush fill, PointF tip, double ux, double uy, float scale = 1f)
    {
        float len = ArrowHeadLen * scale, hw = ArrowHeadHalfWidth * scale;
        float bx = (float)(tip.X - len * ux), by = (float)(tip.Y - len * uy);
        PointF left  = new((float)(bx - hw * uy), (float)(by + hw * ux));
        PointF right = new((float)(bx + hw * uy), (float)(by - hw * ux));
        g.FillPolygon(fill, [tip, left, right]);
    }

    /// <summary>(260503Cl 追加) 紙面内 2/2_1 軸の三角矢じり。<paramref name="lineEnd"/> は shaft 右端 = 三角の重心。
    /// sharp tip は (ux, uy) 方向にさらに 2*ArrowHeadLen/3 伸び、FillPolygon に加え DrawPolygon で shaft と同太さの輪郭線を引く。
    /// <paramref name="halfHead"/> の場合は下半分のみ (2_1 螺旋用)。</summary>
    private static void DrawInPlaneAxisArrowhead(Graphics g, Brush fill, Pen pen, PointF lineEnd, double ux, double uy, bool halfHead)
    {
        PointF tip = new((float)(lineEnd.X + ArrowVisualHeadOffset * ux), (float)(lineEnd.Y + ArrowVisualHeadOffset * uy));
        float bx = (float)(tip.X - ArrowHeadLen * ux), by = (float)(tip.Y - ArrowHeadLen * uy);
        PointF left  = new((float)(bx - ArrowHeadHalfWidth * uy), (float)(by + ArrowHeadHalfWidth * ux));
        PointF right = new((float)(bx + ArrowHeadHalfWidth * uy), (float)(by - ArrowHeadHalfWidth * ux));
        PointF[] poly = halfHead ? [tip, new PointF(bx, by), left] : [tip, left, right];
        g.FillPolygon(fill, poly);
        g.DrawPolygon(pen, poly);
    }

    /// <summary>(260503Cl 追加) 紙面内 4 / 4_n 軸の頭部 (塗り潰し平行四辺形 + 頂点フィン)。
    /// 中心 = <paramref name="center"/> (= shaft 右端)。axis 方向 (ux, uy) の +x が右になる局所系で平行四辺形を組み、
    /// finCount/edgeStep が非零なら DrawScrewFins と同形式のフィンを生やす。</summary>
    private static void DrawInPlaneFourfoldHead(Graphics g, Brush fill, Pen pen, PointF center, double ux, double uy,
                                                int finCount, int edgeStep)
    {
        var poly = BuildInPlaneFourfoldParallelogram(center, ux, uy);
        g.FillPolygon(fill, poly);
        g.DrawPolygon(pen, poly);
        if (finCount > 0) DrawScrewFins(g, pen, poly, finCount, edgeStep, ScrewFinTailLen);
    }

    /// <summary>(260503Cl 追加) 紙面内 -4 軸の頭部 (白塗り平行四辺形 + 黒輪郭 + 内部の塗り潰し楕円)。
    /// 楕円の長軸は平行四辺形の左上頂点 (tl) と右下頂点 (br) を結ぶ対角線。長径 = 対角長、短径 = 長径/4。
    /// 楕円は FillEllipse のみで、輪郭は描かない。</summary>
    private static void DrawInPlaneMinusFourfoldHead(Graphics g, Brush fill, Brush white, Pen pen, PointF center, double ux, double uy)
    {
        var poly = BuildInPlaneFourfoldParallelogram(center, ux, uy);
        g.FillPolygon(white, poly);
        g.DrawPolygon(pen, poly);
        // poly インデックス 1 = tl, 3 = br。
        PointF tl = poly[1], br = poly[3];
        float dx = br.X - tl.X, dy = br.Y - tl.Y;
        float diagLen = (float)Math.Sqrt(dx * dx + dy * dy);
        float majorR = diagLen / 2f, minorR = majorR / 4f;
        float ecx = (tl.X + br.X) / 2f, ecy = (tl.Y + br.Y) / 2f;
        float angleDeg = (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI);
        var state = g.Save();
        try
        {
            g.TranslateTransform(ecx, ecy);
            g.RotateTransform(angleDeg);
            g.FillEllipse(fill, -majorR, -minorR, 2f * majorR, 2f * minorR);
        }
        finally { g.Restore(state); }
    }

    /// <summary>(260503Cl 追加) 紙面内 4 回軸の塗り潰し平行四辺形の頂点列 [bl, tl, tr, br] を返す。
    /// 軸方向 (ux, uy) を局所 +x、左 perpendicular (CCW 90°) を局所 +y とし、左右辺は局所 y 軸に平行 (= axis に垂直)、
    /// 上下辺は 8→2 方向 (右へ進むと axis 垂直方向に slant ぶん移動)。</summary>
    private static PointF[] BuildInPlaneFourfoldParallelogram(PointF center, double ux, double uy)
    {
        // 局所 +y = axis に垂直で「上向き」(axis を +x として CCW 90° 回転)。
        // 平行四辺形は局所座標で左右辺 x = ±paraW/2、左辺中点 y = +slant/2、右辺中点 y = -slant/2、上下辺は y = midY ± vHalf。
        const float paraW = ArrowHeadLen;
        const float vHalf = InPlaneFourfoldVHalf;
        const float slant = InPlaneFourfoldSlant;
        // 局所 perpendicular ベクトル (axis を CCW 90° 回転 = (-uy, ux))。
        double px = -uy, py = ux;
        // 局所点 (lx, ly) = center + lx * (ux, uy) + ly * (px, py)。
        PointF Local(float lx, float ly) => new(
            (float)(center.X + lx * ux + ly * px),
            (float)(center.Y + lx * uy + ly * py));
        // 左辺中点 y_local = +slant/2 (= 局所「上」方向 = axis 進行方向に対し CCW 90°), 右辺中点 y_local = -slant/2。
        // bl = 左辺下 (y_local 大)、tl = 左辺上 (y_local 小)、tr = 右辺上、br = 右辺下。
        // ただし test 設計に合わせて画面の「上下」と局所「上下」を整合させる必要がある。
        // test (ux=1, uy=0): perpendicular (px, py) = (0, 1) → 局所 +y は画面 +y (= 下)。
        //   このとき bl = 左下 = (lEdgeX, lMidY + paraVHalf) = 局所 (-paraW/2, +slant/2 + vHalf)。
        //   tl = 左上 = 局所 (-paraW/2, +slant/2 − vHalf)。整合する。
        return
        [
            Local(-paraW / 2f,  slant / 2f + vHalf), // bl
            Local(-paraW / 2f,  slant / 2f - vHalf), // tl
            Local( paraW / 2f, -slant / 2f - vHalf), // tr
            Local( paraW / 2f, -slant / 2f + vHalf), // br
        ];
    }
    #endregion

    #region 紙面平行 mirror corner bracket
    /// <summary>紙面平行 mirror/glide を IUCR corner bracket で描画。mirror があれば左上、glide は右下に分ける。
    /// (260503Ch) [ITA-D2], [ITA-D4] 高さは代表 h だけを選び、同高さの直交 glide は e-glide bracket へ統合する。
    /// (260505Cl) isCubic=true で立方晶用に bracket を cell 角からさらに離す (紙面平行軸矢印との干渉回避)。</summary>
    private static void DrawParallelMirrorStack(Graphics g, CellLayout c, HashSet<(double Height, bool Glide, double GlideSx, double GlideSy)> markers,
                                                Brush fill, bool isCubic = false, bool allowEGlide = true)
    {
        if (markers.Count == 0) return;
        const float armLen = CornerBracketArmLen;
        float offset = CornerBracketArmLen + CornerBracketGap + (isCubic ? CornerBracketCubicExtraGap : 0f);
        double hLen = Math.Sqrt(c.Horz.X * c.Horz.X + c.Horz.Y * c.Horz.Y);
        double vLen = Math.Sqrt(c.Vert.X * c.Vert.X + c.Vert.Y * c.Vert.Y);
        if (hLen < 1e-3 || vLen < 1e-3) return;
        float hUx = (float)(c.Horz.X / hLen), hUy = (float)(c.Horz.Y / hLen);
        float vUx = (float)(c.Vert.X / vLen), vUy = (float)(c.Vert.Y / vLen);
        var apex0 = new PointF(c.TopLeft.X - offset * (hUx + vUx), c.TopLeft.Y - offset * (hUy + vUy));
        using var pen = new Pen(Color.Black, CornerBracketPenWidth);

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

        var glideReps = markers.Where(HasInPlaneGlide) .GroupBy(GlideKey) .Select(grp =>
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
                    if (allowEGlide && IsDoubleGlidePair(list[i].Sx, list[i].Sy, 0, list[j].Sx, list[j].Sy, 0))
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
            float maxX = Math.Max(apex.X, Math.Max(hEnd.X, vEnd.X));
            foreach (var (tip, lineEndPt, ux, uy) in arrows)
            {
                maxX = Math.Max(maxX, tip.X);
                g.DrawLine(pen, apex, lineEndPt);
                DrawArrowhead(g, fill, tip, ux, uy);
            }

            // 高さラベル: 0 なら省略。labelAtBottomLeft (左上 bracket) は下向き腕の終端 vEnd の下・左脇、それ以外は右側 (中央) に置く。
            string lbl = HeightLabel(height);
            if (lbl == null) return; // (260503Ch) [ITA-D2] 高さ 0 は無標記、非ゼロ代表高さだけを表示する。
            // 旧: var ls = g.MeasureString(lbl, HeightLabelFont);
            var ls = MeasureTightString(g, lbl, HeightLabelFont); // 260510Ch: glyph 正味サイズ。
            float labelX = labelAtBottomLeft ? vEnd.X - ls.Width - ParallelMirrorLabelGap : maxX + ParallelMirrorLabelGap;
            float labelY = labelAtBottomLeft ? vEnd.Y + ParallelMirrorLabelGap            : ((apex.Y + hEnd.Y) - ls.Height) / 2;
            // g.DrawString(lbl, HeightLabelFont, fill, labelX, labelY); // 旧: DrawString/MeasureString の余白を含んでいた。
            DrawTightString(g, fill, lbl, HeightLabelFont, labelX, labelY); // (260505Ch) 呼び出し元の黒 brush を再利用。260510Ch: tight bbox 基準。
        }

        static bool HasInPlaneGlide((double Height, bool Glide, double GlideSx, double GlideSy) m)
            => m.Glide && (Math.Abs(m.GlideSx) > 1e-3 || Math.Abs(m.GlideSy) > 1e-3);

        static bool IsNGlide((double Height, bool Glide, double GlideSx, double GlideSy) m)
        {
            double sx = CenterMod1(m.GlideSx), sy = CenterMod1(m.GlideSy);
            return Math.Abs(sx) > 1e-3 && Math.Abs(sy) > 1e-3 && !IsDGlide(sx, sy);
        }

        static bool IsDGlide(double glideSx, double glideSy)
            => GlideCoset.IsQuarterComponent(glideSx) && GlideCoset.IsQuarterComponent(glideSy); // (260512Ch)

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
    /// <summary>紙面垂直 mirror/glide を幾何線ごとに集約し、ITA 向け defining graphical symbol 1 つへ畳み込んで描画。
    /// (260503Ch) [ITA-D1], [ITA-D4] 同じ幾何面に属する複数操作は、defining graphical symbol 1 個へ畳み込む。</summary>
    private static void DrawCollectedPerpendicularSymmetryPlanes(ElementsContext ctx)
    {
        if (ctx.PerpendicularMirrors.Count == 0) return;
        var groups = ctx.PerpendicularMirrors
            .Select(d => (Draft: d, Key: GetPerpendicularMirrorLineKey(ctx, d.Sx, d.Sy, d.Direction)))
            .Where(x => x.Key.HasValue)
            .GroupBy(x => x.Key.Value);

        foreach (var group in groups)
        {
            var drafts = group.Select(x => x.Draft).ToList();
            if (!TrySelectPerpendicularMirrorDraft(ctx, drafts, out var best, out var style)) continue; // 260510Ch
            DrawMirrorPerpToScreen(ctx, best.Sx, best.Sy, best.Direction, best.Glide, styleOverride: style); // 260510Ch
        }
    }

    private static bool TrySelectPerpendicularMirrorDraft(ElementsContext ctx, List<PerpendicularMirrorDraft> drafts,
                                                          out PerpendicularMirrorDraft best,
                                                          out MirrorGlideStyle? styleOverride)
    {
        styleOverride = null;
        best = default;
        if (drafts.Count == 0) return false;

        if (TryResolveCubicPerpendicularMirrorStyle(ctx, drafts, out var resolvedStyle)) // 260510Ch
        {
            if (resolvedStyle == MirrorGlideStyle.None) return false;
            best = SelectDraftForPerpendicularStyle(ctx.Proj, drafts, resolvedStyle);
            styleOverride = resolvedStyle;
            return true;
        }

        // 260510Ch: 個別の cubic ITA resolver に該当しない群では既存の優先順位を維持し、回帰範囲を抑える。
        foreach (var d in drafts)
        {
            var (gSx, gSy, gSz) = ctx.Proj.ToScreen(d.Glide.U, d.Glide.V, d.Glide.W);
            if (GetMirrorGlideStyle(gSx, gSy, gSz) == MirrorGlideStyle.DGlide)
            {
                best = d;
                return true;
            }
        }
        if (ctx.AllowEGlide && TryFindDoubleGlideDraft(ctx.Proj, drafts, out var eDraft))
        {
            best = eDraft;
            styleOverride = MirrorGlideStyle.EGlide;
            return true; // (260503Ch) [ITA-D4] 直交する half-glide pair は e-glide style で示す。
        }

        bool latticeNodeLine = IsIntegralPerpendicularLineOffset(ctx, drafts[0]);
        best = drafts.OrderBy(d => PerpendicularMirrorPriority(ctx.Proj, d, latticeNodeLine)).First(); // 260510Ch
        return true;
    }

    /// <summary>(260513Ch) 0, 1/4, 1/2, 3/4, odd-eighth 位相判定を名前付きで保持する。</summary>
    private readonly record struct FractionalPhase(double Value)
    {
        public bool Zero => IsFraction(Value, 0, 1);
        public bool Quarter => IsFraction(Value, 1, 4);
        public bool Half => IsFraction(Value, 1, 2);
        public bool ThreeQuarter => IsFraction(Value, 3, 4);
        public bool ZeroOrHalf => Zero || Half;
        public bool QuarterOrThreeQuarter => Quarter || ThreeQuarter;
        public bool OddEighth => IsFraction(Value, 1, 8) || IsFraction(Value, 3, 8) ||
                                 IsFraction(Value, 5, 8) || IsFraction(Value, 7, 8);
        public bool IsQuarterStep => Zero || Quarter || Half || ThreeQuarter;
    }

    /// <summary>(260513Ch) 投影後の mirror/glide 線を、線種判定に必要な正規化済み整数法線と位相で保持する。</summary>
    private readonly record struct ProjectedMirrorLine(int Nx, int Ny, double Offset)
    {
        public bool IsAxis => (Nx == 0) != (Ny == 0);
        public bool IsDiagonal => Nx != 0 && Ny != 0 && Math.Abs(Nx) == Math.Abs(Ny);
    }

    /// <summary>(260513Ch) cubic 紙面垂直 mirror/glide の raw 位相と、必要な群だけで使う origin-choice 補正位相。</summary>
    private readonly record struct CubicPerpendicularPhase(FractionalPhase Raw, FractionalPhase Adjusted, bool HasAdjusted)
    {
        public bool Zero => Raw.Zero;
        public bool Half => Raw.Half;
        public bool ZeroOrHalf => Raw.ZeroOrHalf;
        public bool QuarterOrThreeQuarter => Raw.QuarterOrThreeQuarter;
        public bool OddEighth => Raw.OddEighth;
        public bool AdjustedZero => HasAdjusted && Adjusted.Zero;
        public bool AdjustedHalf => HasAdjusted && Adjusted.Half;
        public bool AdjustedQuarter => HasAdjusted && Adjusted.QuarterOrThreeQuarter;

        public static CubicPerpendicularPhase Create(string hm, ProjectedMirrorLine line)
        {
            var raw = new FractionalPhase(line.Offset);
            bool hasAdjusted = RequiresPerpendicularOriginChoiceAdjustment(hm);
            var adjusted = hasAdjusted
                ? new FractionalPhase(AdjustCubicOriginChoiceOffset(hm, line))
                : default;
            return new(raw, adjusted, hasAdjusted);
        }
    }

    /// <summary>ITA 図で cubic 系の defining graphical symbol を個別に補正する。260510Ch</summary>
    private static bool TryResolveCubicPerpendicularMirrorStyle(ElementsContext ctx, List<PerpendicularMirrorDraft> drafts,
                                                                out MirrorGlideStyle style)
    {
        style = default;
        if (!TryGetProjectedMirrorLine(ctx, drafts[0], out var line)) return false;

        if (!line.IsAxis && !line.IsDiagonal) return false;

        string hm = ctx.SpaceGroupHM ?? "";
        var phase = CubicPerpendicularPhase.Create(hm, line); // (260513Ch)
        if (line.IsDiagonal && IsForbiddenCubicDiagonalPerpendicularPhase(hm, phase)) // 260510Ch
        {
            style = MirrorGlideStyle.None;
            return true;
        }
        bool hasMirrorCandidate = drafts.Any(d =>
        {
            var (gSx, gSy, gSz) = ctx.Proj.ToScreen(d.Glide.U, d.Glide.V, d.Glide.W);
            return GetMirrorGlideStyle(gSx, gSy, gSz) == MirrorGlideStyle.Mirror;
        }); // 260510Ch: 同一幾何線に純鏡映候補があれば、ITA の defining symbol は原則 m。

        switch (hm)
        {
            case "Fm-3m":
                if (phase.ZeroOrHalf) { style = MirrorGlideStyle.Mirror; return true; }
                if (line.IsDiagonal && phase.QuarterOrThreeQuarter) { style = MirrorGlideStyle.NGlide; return true; } // 260510Ch
                if (line.IsAxis && phase.QuarterOrThreeQuarter) { style = MirrorGlideStyle.None; return true; }
                break;

            case "Fm-3c":
                if (line.IsAxis && phase.ZeroOrHalf) { style = MirrorGlideStyle.Mirror; return true; }
                if (line.IsAxis && phase.QuarterOrThreeQuarter) { style = MirrorGlideStyle.None; return true; }
                if (line.IsDiagonal && phase.ZeroOrHalf) { style = MirrorGlideStyle.AxialDepth; return true; }
                if (line.IsDiagonal && phase.QuarterOrThreeQuarter) { style = MirrorGlideStyle.AxialInPlane; return true; } // 260510Ch
                break;

            case "Fd-3m(1)":
                if (line.IsDiagonal && phase.ZeroOrHalf) { style = MirrorGlideStyle.Mirror; return true; }
                break;

            case "Fd-3m(2)":
                if (line.IsDiagonal && phase.OddEighth) { style = MirrorGlideStyle.None; return true; }
                if (line.IsDiagonal && phase.ZeroOrHalf) { style = MirrorGlideStyle.Mirror; return true; }
                break;

            case "Fd-3c(1)":
                if (line.IsDiagonal && phase.ZeroOrHalf) { style = MirrorGlideStyle.AxialDepth; return true; }
                break;

            case "Fd-3c(2)":
                if (line.IsDiagonal && phase.OddEighth) { style = MirrorGlideStyle.None; return true; }
                if (line.IsDiagonal) { style = MirrorGlideStyle.AxialDepth; return true; }
                break;

            case "Im-3m":
                if (line.IsAxis && phase.QuarterOrThreeQuarter) { style = MirrorGlideStyle.NGlide; return true; } // 260510Ch
                if (line.IsDiagonal && TryResolveZeroHalfPhase(phase.Raw, MirrorGlideStyle.Mirror, MirrorGlideStyle.EGlide, out style)) return true; // (260513Ch)
                break;

            case "I-43m":
                if (line.IsDiagonal && phase.Half) { style = MirrorGlideStyle.NGlide; return true; } // 260510Ch
                break;

            case "Ia-3d":
                if (line.IsAxis && phase.ZeroOrHalf) // 260510Ch: a=0,1/2 と b=0,1/2 を同パターンで処理。
                {
                    // 260510Ch: C 投影では sy=a, sx=b。a=0,1/2 (法線 (0,1)) は dot、b=0,1/2 (法線 (1,0)) は dash。
                    style = line.Nx == 0 ? MirrorGlideStyle.AxialDepth : MirrorGlideStyle.AxialInPlane; // (260513Ch)
                    return true;
                }
                if (line.IsAxis && phase.QuarterOrThreeQuarter) // 260510Ch: zero/half と逆パターン。
                {
                    style = line.Nx == 0 ? MirrorGlideStyle.AxialInPlane : MirrorGlideStyle.AxialDepth; // (260513Ch)
                    return true;
                }
                if (line.IsDiagonal && phase.QuarterOrThreeQuarter) // 260510Ch: ITA 図に出ない diagonal quarter 系を両向きとも抑制。
                {
                    style = MirrorGlideStyle.None;
                    return true;
                }
                break;

            case "Pm-3n":
                if (line.IsDiagonal && TryResolveZeroHalfPhase(phase.Raw, MirrorGlideStyle.NGlide, MirrorGlideStyle.AxialDepth, out style)) return true; // (260513Ch)
                break;

            case "Pn-3n(1)":
            case "Pn-3n(2)":
            case "P-43n":
                if (line.IsAxis && phase.AdjustedQuarter) { style = MirrorGlideStyle.NGlide; return true; } // 260510Ch
                if (line.IsDiagonal && TryResolveAdjustedPhase(phase, MirrorGlideStyle.NGlide, MirrorGlideStyle.AxialDepth, MirrorGlideStyle.None, out style)) return true; // (260513Ch)
                break;

            case "Pn-3m(1)":
            case "Pn-3m(2)":
                if (line.IsAxis && phase.AdjustedQuarter) { style = MirrorGlideStyle.NGlide; return true; } // 260510Ch
                if (line.IsDiagonal && TryResolveAdjustedPhase(phase, MirrorGlideStyle.Mirror, MirrorGlideStyle.AxialDepth, MirrorGlideStyle.None, out style)) return true; // (260513Ch)
                break;

            case "F-43c":
                if (line.IsDiagonal && phase.Zero) { style = MirrorGlideStyle.AxialDepth; return true; }
                break;

            case "I-43d":
                if (line.IsDiagonal && phase.QuarterOrThreeQuarter) { style = MirrorGlideStyle.None; return true; } // 260510Ch: ITA 図に出ない diagonal quarter lines を抑制。
                break;
        }
        if (hasMirrorCandidate)
        {
            style = MirrorGlideStyle.Mirror;
            return true;
        }
        return false;
    }

    private static bool IsForbiddenCubicDiagonalPerpendicularPhase(string hm, CubicPerpendicularPhase phase)
    {
        // 260510Ch: cubic の紙面垂直 {110} 系では、P/I 格子の 1/4 位相、および F 格子の 1/8 位相に
        // 対称面は存在しない。ここで先に落とし、後段の hasMirrorCandidate fallback で m として復活させない。
        if (string.IsNullOrEmpty(hm)) return false;
        char lattice = char.ToUpperInvariant(hm[0]);
        if (lattice is 'P' or 'I')
            return phase.QuarterOrThreeQuarter;
        if (lattice == 'F')
            return phase.OddEighth;
        return false;
    }

    /// <summary>(260513Ch) 0/1/2 位相を、呼び出し側が指定した mirror/glide style に変換する。</summary>
    private static bool TryResolveZeroHalfPhase(FractionalPhase phase, MirrorGlideStyle zeroStyle,
                                                MirrorGlideStyle halfStyle, out MirrorGlideStyle style)
    {
        style = default;
        if (phase.Zero) { style = zeroStyle; return true; }
        if (phase.Half) { style = halfStyle; return true; }
        return false;
    }

    /// <summary>(260513Ch) origin-choice 補正後の 0/1/2/1/4 位相を、群別 style に変換する。</summary>
    private static bool TryResolveAdjustedPhase(CubicPerpendicularPhase phase, MirrorGlideStyle zeroStyle,
                                                MirrorGlideStyle halfStyle, MirrorGlideStyle quarterStyle,
                                                out MirrorGlideStyle style)
    {
        style = default;
        if (phase.AdjustedZero) { style = zeroStyle; return true; }
        if (phase.AdjustedHalf) { style = halfStyle; return true; }
        if (phase.AdjustedQuarter) { style = quarterStyle; return true; }
        return false;
    }

    private static PerpendicularMirrorDraft SelectDraftForPerpendicularStyle(Projection proj, List<PerpendicularMirrorDraft> drafts,
                                                                             MirrorGlideStyle style)
    {
        // 260510Ch: forced style の描画では幾何情報だけが必要な場合があるため、該当 glide 候補が無ければ最小 glide 候補へ fallback。
        return drafts
            .OrderBy(d => DraftMatchesStyle(proj, d, style) ? 0 : 1)
            .ThenBy(d => Math.Abs(d.Glide.U) + Math.Abs(d.Glide.V) + Math.Abs(d.Glide.W))
            .First();
    }

    private static bool DraftMatchesStyle(Projection proj, PerpendicularMirrorDraft draft, MirrorGlideStyle style)
    {
        var (gSx, gSy, gSz) = proj.ToScreen(draft.Glide.U, draft.Glide.V, draft.Glide.W);
        var draftStyle = GetMirrorGlideStyle(gSx, gSy, gSz);
        return style == MirrorGlideStyle.NGlide
            ? draftStyle == MirrorGlideStyle.DiagonalGlide
            : draftStyle == style;
    }

    private static bool TryGetProjectedMirrorLine(ElementsContext ctx, PerpendicularMirrorDraft draft,
                                                  out ProjectedMirrorLine line)
    {
        line = default;
        var (dSx, dSy) = ProjectVector(draft.Direction.U, draft.Direction.V, draft.Direction.W, ctx.Proj.Axis);
        int nx = (int)Math.Round(dSx);
        int ny = (int)Math.Round(dSy);
        if (Math.Abs(dSx - nx) > 1e-6 || Math.Abs(dSy - ny) > 1e-6) return false;
        int gcd = Gcd(Math.Abs(nx), Math.Abs(ny));
        if (gcd == 0) return false;
        double offset = nx * NormalizeCellBoundary(draft.Sx) + ny * NormalizeCellBoundary(draft.Sy);
        nx /= gcd;
        ny /= gcd;
        offset /= gcd;
        if (nx < 0 || (nx == 0 && ny < 0))
        {
            nx = -nx;
            ny = -ny;
            offset = -offset;
        }
        offset = Mod1(offset);
        if (offset > 1 - FracEps) offset = 0;
        line = new(nx, ny, offset); // (260513Ch)
        return true;

        static int Gcd(int a, int b)
        {
            while (b != 0) (a, b) = (b, a % b);
            return a;
        }
    }

    private static bool IsFraction(double value, int numerator, int denominator)
    {
        double target = (double)numerator / denominator;
        double diff = Math.Abs(Mod1(value - target));
        return diff < 1e-6 || Math.Abs(diff - 1) < 1e-6;
    }

    private static (int Priority, double Score) PerpendicularMirrorPriority(Projection proj, PerpendicularMirrorDraft draft,
                                                                            bool latticeNodeLine)
    {
        var (gSx, gSy, gSz) = proj.ToScreen(draft.Glide.U, draft.Glide.V, draft.Glide.W);
        var style = GetMirrorGlideStyle(gSx, gSy, gSz);
        // 260510Ch: 紙面内 glide を優先し、格子節点線では m/c を n より優先する。
        int priority = style switch
        {
            MirrorGlideStyle.AxialInPlane => 0,
            MirrorGlideStyle.Mirror => 1,
            MirrorGlideStyle.AxialDepth => latticeNodeLine ? 2 : 3,
            MirrorGlideStyle.DiagonalGlide => latticeNodeLine ? 3 : 2,
            _ => 4
        };
        return (priority, Math.Abs(gSx) + Math.Abs(gSy) + Math.Abs(gSz));
    }

    private static bool IsIntegralPerpendicularLineOffset(ElementsContext ctx, PerpendicularMirrorDraft draft)
    {
        var (dSx, dSy) = ProjectVector(draft.Direction.U, draft.Direction.V, draft.Direction.W, ctx.Proj.Axis);
        double offset = dSx * NormalizeCellBoundary(draft.Sx) + dSy * NormalizeCellBoundary(draft.Sy);
        return Math.Abs(CenterMod1(offset)) < 1e-6;
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
    private static void DrawMirrorPerpToScreen(ElementsContext ctx, double sx, double sy, (int U, int V, int W) dir, (double U, double V, double W) it,
                                               MirrorGlideStyle? styleOverride = null)
    {
        var c = ctx.C;
        if (!TryGetMirrorPerpGeometry(c, ctx.Proj.Axis, dir, out double perpSx, out double perpSy, out double nX, out double nY)) return;
        var (gSx, gSy, gSz) = ctx.Proj.ToScreen(it.U, it.V, it.W);
        var style = styleOverride ?? GetMirrorGlideStyle(gSx, gSy, gSz); // 260510Ch
        if (style == MirrorGlideStyle.None) return; // 260510Ch
        bool dGlide = style == MirrorGlideStyle.DGlide;
        Pen pen = style switch
        {
            MirrorGlideStyle.Mirror => ctx.MirrorPen,
            MirrorGlideStyle.AxialInPlane => ctx.InPlanePen,
            MirrorGlideStyle.AxialDepth => ctx.DepthPen,
            MirrorGlideStyle.DiagonalGlide => ctx.DiagPen,
            MirrorGlideStyle.EGlide => ctx.EPen,
            MirrorGlideStyle.NGlide => ctx.DiagPen, // 260510Ch: n-glide は dash-dot 線、矢印なし。
            _ => ctx.MirrorPen
        };

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
            var (start, end) = SpanLineThroughCell(c, lineSx, lineSy, perpSx, perpSy, ctx.DisplayMaxS); // (260505Ch) F 格子は線分そのものを 1/4 領域で切る。
            if (!start.HasValue || !end.HasValue) return;
            var startPt = start.Value;
            var endPt = end.Value;
            ExtendLineForStereonetEndpoints(ctx, ref startPt, ref endPt);
            double nLen = Math.Sqrt(nX * nX + nY * nY);
            if (nLen < 1e-9) return;
            double ux = nX / nLen, uy = nY / nLen;
            if (ux < -1e-9 || (Math.Abs(ux) < 1e-9 && uy < 0)) { ux = -ux; uy = -uy; }
            var pt = c.ToScreen(lineSx, lineSy);
            var key = (R6(ux), R6(uy), (long)Math.Round((ux * pt.X + uy * pt.Y) * 1000), style);
            if (!ctx.DrawnSymmetryPlanes.Add(key)) return;
            if (dGlide)
            {
                var (arrowX, arrowY) = GetDGlideArrowDirection(c, gSx, gSy, gSz);
                DrawDGlidePerpLine(ctx.G, pen, ctx.Fill, startPt, endPt, arrowX, arrowY);
                return;
            }
            ctx.G.DrawLine(pen, startPt, endPt);
        }
    }

    /// <summary>(260505Ch) mirror/glide 線分の端点が stereonet inset 中心なら、輪郭まで届くよう半径分だけ外側へ延長する。</summary>
    private static void ExtendLineForStereonetEndpoints(ElementsContext ctx, ref PointF start, ref PointF end)
    {
        if (ctx.StereonetAnchorKeys == null) return;
        float dx = end.X - start.X, dy = end.Y - start.Y;
        float len = (float)Math.Sqrt(dx * dx + dy * dy);
        if (len < 1e-3f) return;
        float ux = dx / len, uy = dy / len;
        if (ctx.StereonetAnchorKeys.Contains(ScreenPointKey(start)))
            start = new PointF(start.X - ux * CubicStereonetInsetRadius, start.Y - uy * CubicStereonetInsetRadius);
        if (ctx.StereonetAnchorKeys.Contains(ScreenPointKey(end)))
            end = new PointF(end.X + ux * CubicStereonetInsetRadius, end.Y + uy * CubicStereonetInsetRadius);
    }

    private static (long X, long Y) ScreenPointKey(PointF p)
        => ((long)Math.Round(p.X * 1000), (long)Math.Round(p.Y * 1000));

    /// <summary>e-glide 判定。同一幾何面に複数の half-glide coset がある場合を double-glide とする。</summary>
    private static bool IsDoubleGlidePair(double x1, double y1, double z1, double x2, double y2, double z2)
    {
        var g1 = GlideCoset.Centered(x1, y1, z1); // (260512Ch)
        var g2 = GlideCoset.Centered(x2, y2, z2); // (260512Ch)
        if (!g1.IsHalfVector || !g2.IsHalfVector) return false; // (260505Ch) e は half-glide coset の重なりとして判定する。
        if (g1.SameAs(g2)) return false;
        if (g1.OppositeOf(g2))
            return g1.HalfComponentCount == 1; // (260505Ch) axial half-glide の ± pair は e、face-diagonal half-glide の ± pair は通常 glide。
        return true; // (260505Ch) 非平行な half-glide coset が同一幾何面に載る場合は e。
    }

    private static bool IsPerpendicularDGlide(double gSx, double gSy, double gSz)
        => GlideCoset.IsQuarterComponent(gSz) && (GlideCoset.IsQuarterComponent(gSx) || GlideCoset.IsQuarterComponent(gSy));

    private static MirrorGlideStyle GetMirrorGlideStyle(double gSx, double gSy, double gSz)
    {
        if (IsPerpendicularDGlide(gSx, gSy, gSz)) return MirrorGlideStyle.DGlide;
        bool hasInPlane = Math.Abs(gSx) > 1e-3 || Math.Abs(gSy) > 1e-3;
        bool hasDepth = Math.Abs(gSz) > 1e-3;
        return (hasInPlane, hasDepth) switch
        {
            (false, false) => MirrorGlideStyle.Mirror,
            (true, false) => MirrorGlideStyle.AxialInPlane,
            (false, true) => MirrorGlideStyle.AxialDepth,
            _ => MirrorGlideStyle.DiagonalGlide
        }; // 260510Ch: 紙面垂直 mirror/glide の線種分類を描画・代表選択で共有する。
    }

    private static bool TryGetMirrorPerpGeometry(CellLayout c, ProjectionAxis axis, (int U, int V, int W) dir,
                                                 out double perpSx, out double perpSy, out double nX, out double nY)
    {
        var (dSx, dSy) = ProjectVector(dir.U, dir.V, dir.W, axis);
        // 260510Ch: dir は Miller 面指数なので、投影面内では dSx*Sx + dSy*Sy = const の共変係数として扱う。
        // 直接格子ベクトルとして足すと hex/trig の (h k i l) 回転同値面で線方向が崩れる。
        double lineSx = -dSy, lineSy = dSx;
        double pX = lineSx * c.Horz.X + lineSy * c.Vert.X;
        double pY = lineSx * c.Horz.Y + lineSy * c.Vert.Y;
        nX = -pY;
        nY = pX;
        perpSx = perpSy = 0;
        if (Math.Abs(lineSx) < 1e-12 && Math.Abs(lineSy) < 1e-12) return false;
        perpSx = lineSx;
        perpSy = lineSy;
        // 260510Ch: 投影変換由来の微小成分は線分 clip 前に 0 へ丸める。
        // これを残すと微小 d で除算して縮退線を返し、隣接辺の dedup key を先取りしてしまう。
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

        for (int i = 0; ; i++)
        {
            float baseT = i * DGlidePatternPitch; // (260505Ch) パターン内 offset は冒頭定数へ集約。
            if (baseT + DGlidePatternDot1Offset >= len) break;
            Dot(baseT + DGlidePatternDot1Offset);
            Dash(baseT + DGlidePatternDash1Offset, baseT + DGlidePatternDash1Offset + DGlidePatternDashLen);
            Dot(baseT + DGlidePatternDot2Offset);
            Dash(baseT + DGlidePatternDash2Offset, baseT + DGlidePatternDash2Offset + DGlidePatternDashLen);
            Dot(baseT + DGlidePatternDot3Offset);
            Arrow(baseT + DGlidePatternArrowOffset, baseT + DGlidePatternArrowOffset + DGlidePatternArrowLen);
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
            DrawArrowhead(g, fill, tip, ux, uy);
        }
    }

    /// <summary>s≈1 (右辺/下辺) は 0 に折り畳まず 1 のまま残す: drawnSymmetryPlanes の dedup キーが左/右辺で別になり両辺それぞれに描画できる。</summary>
    private static double NormalizeCellBoundary(double s)
    {
        if (Math.Abs(s - 1) < 1e-8) return 1;
        double m = s - Math.Floor(s);
        if (m < 1e-8) return 0;
        if (m > 1 - 1e-8) return 1;
        return m;
    }
    #endregion

    #region 立方晶 m-3m / -43m 用 斜め鏡映 stereonet (260504Cl 追加)
    /// <summary>(260505Cl 追加) m-3m / -43m 系空間群の stereonet 描画位置リスト (lattice type で切り替え)。
    /// 非 F: cell 全周の (0,0)〜(1,1) 9 点 (4 隅 + 4 辺中点 + 中心)。
    /// F: 図が混雑するので upper-left 1/4 領域に同様の 9 点 (1/2 → 1/4 にスケール)。</summary>
    private static (double Sx, double Sy)[] GetStereonetDrawPositions(Symmetry sym)
    {
        bool isF = sym.LatticeTypeStr == "F";
        return isF
            ? [(0, 0), (0.25, 0), (0, 0.25), (0.25, 0.25), (0.5, 0), (0, 0.5), (0.5, 0.5), (0.25, 0.5), (0.5, 0.25)]
            : [(0, 0), (0.5, 0), (0, 0.5), (0.5, 0.5), (1, 0), (1, 1), (0, 1), (0.5, 1), (1, 0.5)];
    }

    /// <summary>(260505Cl 追加) Mod1 し dedup した stereonet 位置キー集合。cell-side の 3 回軸描画スキップ判定に使う。</summary>
    private static HashSet<(long, long)> ComputeStereonetSkipKeys((double Sx, double Sy)[] positions)
        => [.. positions.Select(p => PeriodicPositionKey(p.Sx, p.Sy))]; // 260510Cl: foreach + Add を collection expr へ。

    /// <summary>(260505Ch) unit-cell 周期位置の比較キー。1.0 近傍を 0.0 に折り畳み、境界上の foot を同一視する。</summary>
    private static (long X, long Y) PeriodicPositionKey(double sx, double sy)
        => (R6Periodic(sx), R6Periodic(sy));

    private static long R6Periodic(double s)
    {
        double m = Mod1(s);
        if (m < FracEps || m > 1 - FracEps) m = 0;
        return R6(m);
    }

    /// <summary>(260505Cl 追加) m-3m / -43m 系空間群の指定位置に inset stereonet を描画する。
    /// 各 inset には: 輪郭円、補助大円 4 本 ((101)/(10-1)/(011)/(01-1))、site (z=0 厳密) を通る実鏡映/d 映進、3 回軸を載せる。
    /// 補助線は cell 輪郭と同じ細色線で先描き。実鏡映が同じ平面に存在すれば mirrorPen で上塗りされて見えなくなる (設計)。</summary>
    private static void DrawCubicStereonetInsets(ElementsContext ctx, Crystallography.SymmetryElementsTable table,
                                                 ProjectionAxis projAxis, (double Sx, double Sy)[] positions)
    {
        var diagonalMirrors = table.SymmetryPlanes
            .Where(mp => IsAxisDiagonalToProjection(mp.Normal, projAxis))
            .ToList(); // (260505Ch) perpendicular/in-plane の否定形ではなく diagonal 判定へ統一。
        // 260505Cl: ITA Vol.A Pm-3m 等で inset に出ている斜め 2 回軸も拾う。260512Ch: principal 軸だけを対象にする。
        var diagonalAxes = table.PrincipalSymmetryAxes // 260512Ch
            .Where(ax => ax.Order is 2 or 3 && IsAxisDiagonalToProjection(ax.Direction, projAxis))
            .ToList(); // (260505Ch) cell-side の斜め軸判定と同じ条件に揃える。

        float radius = CubicStereonetInsetRadius;
        using var outlinePen = new Pen(CellOutlineColor, CellOutlinePenWidth);
        // 260510Cl: 旧来 dGlidePen/axialDashPen/axialDotPen/ePen を新規生成していたが、ctx.DiagPen / InPlanePen / DepthPen / EPen と
        // dash pattern が完全一致するため context 側のペンを再利用する。
        var dGlidePen    = ctx.DiagPen;     // dot-dash 大円 (d-glide / n-glide)
        var axialDashPen = ctx.InPlanePen;  // 1/2 along 紙面内軸: 破線
        var axialDotPen  = ctx.DepthPen;    // 1/2 along 紙面と斜め: 点線
        var ePen         = ctx.EPen;        // e-glide (double glide): dash-dot-dot

        // 補助線として描く 4 つの大円 (cell 輪郭と同じ色・線幅)。実鏡映があれば後段の mirrorPen 描画で上塗りされる。
        foreach (var (sxScreen, syScreen) in positions)
        {
            // site 判定は Mod1 で正規化した screen 位置 + Sz=0 を結晶座標に変換 (260506Cl: 1 行 switch にインライン化)。
            double sxKey = Mod1(sxScreen), syKey = Mod1(syScreen); // 260510Ch: F 格子も内部座標は通常セルの (a,b) のまま扱い、縮小は表示だけに限定する。
            double axisSxKey = Mod1(sxScreen), axisSyKey = Mod1(syScreen); // 260510Ch: 斜め軸は実際の upper-left quadrant 位置で正しく拾えていたため、面用の 1/2 縮小補正を掛けない。
            var (xc, yc, zc) = projAxis switch
            {
                ProjectionAxis.C => (syKey, sxKey, 0.0),
                ProjectionAxis.A => (0.0, syKey, sxKey),
                ProjectionAxis.B => (sxKey, 0.0, syKey),
                _ => (0.0, 0.0, 0.0)
            };
            var (axisXc, axisYc, axisZc) = projAxis switch
            {
                ProjectionAxis.C => (axisSyKey, axisSxKey, 0.0),
                ProjectionAxis.A => (0.0, axisSyKey, axisSxKey),
                ProjectionAxis.B => (axisSxKey, 0.0, axisSyKey),
                _ => (0.0, 0.0, 0.0)
            }; // 260510Ch

            // この site を通る (h,k,l) ごとに glide ベクトルを集約 → e-glide 検出のため。
            var groupedGlides = new Dictionary<(int H, int K, int L), List<GlideCoset>>(); // (260512Ch)
            foreach (var mp in diagonalMirrors)
            {
                if (!PlaneIntersectsProjectionColumn(mp, xc, yc, zc, projAxis)) continue; // 260510Ch: stereonet inset は投影点の depth column と交わる斜め面を拾う。
                var hkl = NormalizeMillerIndices(mp.Normal);
                if (!groupedGlides.TryGetValue(hkl, out var list))
                    groupedGlides[hkl] = list = [];
                list.Add(GlideCoset.Centered(mp.Glide.U, mp.Glide.V, mp.Glide.W)); // (260512Ch) glide coset 代表として保持。
            }
            // (h,k,l) 単位で style を確定。
            var siteMirrors = groupedGlides
                .Select(group => (Hkl: group.Key, Style: ResolveStereonetGroupStyle(group.Value, projAxis, ctx.SpaceGroupHM, group.Key, xc, yc, zc))) // 260510Ch
                .Where(group => group.Style != MirrorGlideStyle.None)
                .ToList(); // (260505Ch) grouped glides → site mirror style の配線を一段に整理。260510Ch: table で site を通る実在候補だけを描く。

            // 260505Cl: Order を含めて 2 回軸/3 回軸を同列に dedup。Screw も保持し DrawStereonetTwofold に渡す。
            // (260505Cl 整理) 立方晶高対称群の stereonet 中心を通る 3 回軸は <111> proper のみで、3_1/3_2 螺旋は現れないため FinCount/EdgeStep は持たない。
            // 260510Cl: foreach + 手動 dedup を Where + DistinctBy へ。
            var siteAxes = diagonalAxes
                .Where(ax => AxisPassesThroughSite(ax, axisXc, axisYc, axisZc))
                .DistinctBy(ax => (ax.Direction.U, ax.Direction.V, ax.Direction.W, ax.Order))
                .Select(ax => (ax.Direction, ax.Order, ax.Screw))
                .ToList();

            var center = ctx.C.ToScreen(sxScreen, syScreen);
            // 1) 輪郭 (常に)
            ctx.G.DrawEllipse(outlinePen, center.X - radius, center.Y - radius, 2 * radius, 2 * radius);
            // 2) 補助大円 4 本 (常に)
            foreach (var aux in CubicStereonetAuxiliaryHkls)
                DrawMirrorGreatCircle(ctx.G, outlinePen, center, radius, aux);
            // 3) 実鏡映/glide (style 別) — 補助線を上塗り
            foreach (var (hkl, style) in siteMirrors)
            {
                switch (style)
                {
                    case MirrorGlideStyle.Mirror:       DrawMirrorGreatCircle(ctx.G, ctx.MirrorPen, center, radius, hkl); break;
                    case MirrorGlideStyle.AxialInPlane: DrawMirrorGreatCircle(ctx.G, axialDashPen,  center, radius, hkl); break;
                    case MirrorGlideStyle.AxialDepth:   DrawMirrorGreatCircle(ctx.G, axialDotPen,   center, radius, hkl); break;
                    case MirrorGlideStyle.EGlide:       DrawMirrorGreatCircle(ctx.G, ePen,          center, radius, hkl); break;
                    case MirrorGlideStyle.NGlide:       DrawMirrorGreatCircle(ctx.G, dGlidePen,     center, radius, hkl); break; // (260505Ch) n-glide は d と同じ dash-dot 大円だが矢印は付けない。
                    case MirrorGlideStyle.DGlide:      DrawDGlideGreatCircle(ctx.G, dGlidePen, ctx.Fill, center, radius, hkl); break;
                    case MirrorGlideStyle.None: break; // 260510Ch
                }
            }
            // 4) 実 2/3 回軸 (260505Cl: Pm-3m など face-diagonal 2 回軸を inset に追加)
            foreach (var (dir, order, screw) in siteAxes)
            {
                if (order == 3)
                    DrawStereonetThreefold(ctx.G, ctx.Fill, center, radius, dir);
                else // order == 2
                    DrawStereonetTwofold(ctx.G, ctx.Fill, center, radius, dir, screw);
            }
        }
    }

    /// <summary>(260505Cl) 同一 (h,k,l) 平面に対し、site で集めた glide ベクトル群から最終 style を決める。
    /// 純鏡映を含めば Mirror、独立な非零 glide が 2 つ以上で EGlide、1 つだけなら glide ベクトルから AxialInPlane/AxialDepth/NGlide/Diamond を分類。</summary>
    private static MirrorGlideStyle ResolveStereonetGroupStyle(List<GlideCoset> glides, ProjectionAxis projAxis,
                                                                  string spaceGroupHM = null,
                                                                  (int H, int K, int L) hkl = default,
                                                                  double siteX = 0, double siteY = 0, double siteZ = 0)
    {
        bool hasPure = false;
        var distinctNonZero = new List<GlideCoset>(); // (260512Ch)
        foreach (var g in glides)
        {
            if (g.IsZero) { hasPure = true; continue; }
            // 260510Cl: 内側 foreach + found フラグを Any() へ。
            if (!distinctNonZero.Any(d => d.SameAs(g)))
                distinctNonZero.Add(g);
        }
        bool allowEGlide = AllowsDiagonalEGlide(spaceGroupHM); // 260510Ch
        bool allowDGlide = AllowsDiagonalDGlide(spaceGroupHM); // 260510Ch

        // 260510Ch: 両者は origin shift だけの差なので、h*x+k*y+l*z の原点補正 phase へ共通化し、Pn/Pm 系も含めて単一リゾルバへ集約。
        if (TryResolveCubicStereonetPhaseStyle(spaceGroupHM, hkl, siteX, siteY, siteZ, out var phaseStyle)) // 260510Ch
            return phaseStyle;

        if (allowEGlide && TryResolveBodyCenteredDiagonalStereonetStyle(hkl, siteX, siteY, siteZ, out var bodyCenteredStyle))
            return bodyCenteredStyle; // 260510Ch

        // 260510Ch: stereonet でも純鏡映候補があれば mirror を defining symbol とする。
        if (hasPure) return MirrorGlideStyle.Mirror;

        // 複数の half-glide coset が同じ幾何面に載る場合 → e-glide。
        // 260510Ch: 斜め e-glide は Im-3m / I-43m 限定、かつ pure mirror が無い site だけ。
        if (allowEGlide)
            for (int i = 0; i < distinctNonZero.Count - 1; i++)
                for (int j = i + 1; j < distinctNonZero.Count; j++)
                    if (IsDoubleGlidePair(distinctNonZero[i].X, distinctNonZero[i].Y, distinctNonZero[i].Z,
                                          distinctNonZero[j].X, distinctNonZero[j].Y, distinctNonZero[j].Z))
                        return MirrorGlideStyle.EGlide;
        if (distinctNonZero.Count == 0) return MirrorGlideStyle.Mirror; // safety

        // 260510Ch: 同じ hkl/site に複数 glide coset が載る群 (Fd-3c, Pm-3n など) では、列挙順で代表を決めると
        // e/d/n/dot/dash が不安定に入れ替わる。全候補を分類してから ITA で許可される defining symbol を選ぶ。
        var candidateStyles = distinctNonZero
            .Select(g => ClassifyStereonetGlideVector(g, projAxis, allowDGlide, AllowsDiagonalNGlide(spaceGroupHM)))
            .Distinct()
            .ToList();
        if (candidateStyles.Contains(MirrorGlideStyle.DGlide)) return MirrorGlideStyle.DGlide;
        if (candidateStyles.Contains(MirrorGlideStyle.NGlide)) return MirrorGlideStyle.NGlide;
        if (candidateStyles.Contains(MirrorGlideStyle.AxialDepth)) return MirrorGlideStyle.AxialDepth;
        if (candidateStyles.Contains(MirrorGlideStyle.AxialInPlane)) return MirrorGlideStyle.AxialInPlane;
        return MirrorGlideStyle.None;
    }

    private static bool TryResolveCubicStereonetPhaseStyle(string spaceGroupHM,
                                                           (int H, int K, int L) hkl,
                                                           double siteX, double siteY, double siteZ,
                                                           out MirrorGlideStyle style)
    {
        style = default;
        hkl = NormalizeMillerIndices(hkl);
        if (hkl.L == 0 || ((hkl.H == 0) == (hkl.K == 0))) return false;

        var phase = new FractionalPhase(CubicDiagonalColumnPhase(spaceGroupHM, hkl, siteX, siteY, siteZ)); // (260513Ch)
        if (!phase.IsQuarterStep) return false;

        switch (spaceGroupHM)
        {
            case "Fm-3m":
            case "F-43m":
            case "Fd-3m(1)":
            case "Fd-3m(2)":
                style = phase.QuarterOrThreeQuarter ? MirrorGlideStyle.NGlide : MirrorGlideStyle.Mirror;
                return true;

            case "Fm-3c":
            case "F-43c":
            case "Fd-3c(1)":
            case "Fd-3c(2)":
                style = phase.QuarterOrThreeQuarter ? MirrorGlideStyle.AxialDepth : MirrorGlideStyle.AxialInPlane;
                return true;

            case "Pn-3m(1)":
            case "Pn-3m(2)":
            case "Pm-3m":
            case "P-43m":
                return TryResolveZeroHalfPhase(phase, MirrorGlideStyle.Mirror, MirrorGlideStyle.AxialDepth, out style); // (260513Ch)

            case "Pm-3n":
            case "Pn-3n(1)":
            case "Pn-3n(2)":
            case "P-43n":
                return TryResolveZeroHalfPhase(phase, MirrorGlideStyle.NGlide, MirrorGlideStyle.AxialInPlane, out style); // (260513Ch)
        }
        return false;
    }

    private static double AdjustCubicOriginChoiceOffset(string hm, ProjectedMirrorLine line)
    {
        // 260510Ch: Pn-3n/Pn-3m の origin choice (2) は (1/4,1/4,1/4) origin shift として、
        // screen normal (nx,ny)=(k,h) の h+k 位相分を choice (1) 側の判定位相へ戻す。
        if (hm is "Pn-3n(2)" or "Pn-3m(2)")
            return Mod1(line.Offset + 0.25 * (line.Nx + line.Ny)); // (260513Ch)
        return line.Offset;
    }

    private static bool RequiresPerpendicularOriginChoiceAdjustment(string hm)
        => hm is "Pn-3n(1)" or "Pn-3n(2)" or "P-43n" or "Pn-3m(1)" or "Pn-3m(2)"; // (260513Ch)

    private static double CubicStereonetOriginChoiceShift(string spaceGroupHM)
    {
        // 260510Ch: stereonet inset は depth column と斜め面の交点を見ているため、origin choice の差は
        // h*x+k*y+l*z の面位相に入れる。Fd の choice (2) は ITA 設定上 1/8 shift として現れる。
        if (spaceGroupHM is "Fd-3m(2)" or "Fd-3c(2)") return 0.125;
        if (spaceGroupHM is "Pn-3m(2)" or "Pn-3n(2)") return 0.25;
        return 0.0;
    }

    private static double CubicDiagonalColumnPhase(string spaceGroupHM, (int H, int K, int L) hkl, double siteX, double siteY, double siteZ)
    {
        hkl = NormalizeMillerIndices(hkl);
        double origin = CubicStereonetOriginChoiceShift(spaceGroupHM); // 260510Ch
        double phase = hkl.H * siteX + hkl.K * siteY + hkl.L * siteZ
                     - (hkl.H + hkl.K + hkl.L) * origin; // 260510Ch
        return Mod1(phase);
    }

    private static bool AllowsDiagonalDGlide(string spaceGroupHM)
        => spaceGroupHM is "Ia-3d" or "I-43d"; // 260510Ch: ITA 確認事項。斜め d 映進はこの 2 群のみ。

    private static bool AllowsDiagonalEGlide(string spaceGroupHM)
        => spaceGroupHM is "Im-3m" or "I-43m"; // 260510Ch: ITA 確認事項。斜め e 映進はこの I 格子 2 群のみ。

    private static bool AllowsDiagonalNGlide(string spaceGroupHM)
        => spaceGroupHM?.Contains('n', StringComparison.OrdinalIgnoreCase) == true; // 260510Ch: quarter 系を機械的に n にせず、n を defining symbol に持つ群だけ許可。

    private static MirrorGlideStyle ClassifyStereonetGlideVector(GlideCoset glide, ProjectionAxis projAxis,
                                                                    bool allowDGlide, bool allowNGlide)
    {
        int quarterCount = glide.QuarterComponentCount; // (260512Ch)
        int halfCount = glide.HalfComponentCount; // (260512Ch)
        if (quarterCount >= 2)
        {
            // 260510Ch: ITA 確認事項に従い、d は Ia-3d/I-43d 限定。その他の quarter 系は depth 有無で dot/dash へ落とす。
            if (allowDGlide && halfCount == 0) return MirrorGlideStyle.DGlide;
            if (allowNGlide && halfCount > 0) return MirrorGlideStyle.NGlide;
        }
        if (allowNGlide && halfCount >= 2) return MirrorGlideStyle.NGlide; // 260510Ch: Pm-3n などの diagonal half-glide pair。

        double depthGlide = ProjectedDepth(glide.X, glide.Y, glide.Z, projAxis);
        return Math.Abs(depthGlide) > 1e-6 ? MirrorGlideStyle.AxialDepth : MirrorGlideStyle.AxialInPlane;
    }

    private static bool TryResolveBodyCenteredDiagonalStereonetStyle((int H, int K, int L) hkl,
                                                                     double siteX, double siteY, double siteZ,
                                                                     out MirrorGlideStyle style)
    {
        // 260510Ch: I 格子の m-3m/-43m 系では、斜め面の site 位相 h*x+k*y+l*z が
        // 整数なら mirror、半整数なら e-glide になる。Im-3m の edge/center stereonet をこの一式で処理する。
        style = default;
        var phase = new FractionalPhase(Mod1(hkl.H * siteX + hkl.K * siteY + hkl.L * siteZ)); // (260513Ch)
        return TryResolveZeroHalfPhase(phase, MirrorGlideStyle.Mirror, MirrorGlideStyle.EGlide, out style); // (260513Ch)
    }

    /// <summary>(260505Cl) 鏡映/glide 平面 mp が site (xc, yc, zc) を通るか厳密判定 (lattice 周期を含む)。</summary>
    private static bool PlanePassesThroughSite(SymmetryPlane mp, double xc, double yc, double zc)
    {
        double h = mp.Normal.U, k = mp.Normal.V, l = mp.Normal.W;
        double c = h * mp.X + k * mp.Y + l * mp.Z;
        double residual = h * xc + k * yc + l * zc - c;
        residual -= Math.Round(residual); // mod 1
        return Math.Abs(residual) < 1e-6;
    }

    /// <summary>stereonet inset は投影点を通る depth column の対称性を表すため、斜め面は depth を動かして交差判定する。260510Ch</summary>
    private static bool PlaneIntersectsProjectionColumn(SymmetryPlane mp, double xc, double yc, double zc, ProjectionAxis projAxis)
    {
        var depthCoeff = ProjectedDepth(mp.Normal.U, mp.Normal.V, mp.Normal.W, projAxis);
        if (Math.Abs(depthCoeff) < 1e-9) return PlanePassesThroughSite(mp, xc, yc, zc);
        return true;
    }

    /// <summary>(260505Cl) 軸 ax の line {(X+tU, Y+tV, Z+tW)} が site (xc, yc, zc) を通るか厳密判定 (lattice 周期を含む)。</summary>
    private static bool AxisPassesThroughSite(SymmetryAxis ax, double xc, double yc, double zc)
    {
        int U = ax.Direction.U, V = ax.Direction.V, W = ax.Direction.W;
        // 各座標方向の格子並進を試し、その方向で t を求めて他軸が整数オフセットに収まるか確認。
        if (W != 0)
        {
            for (int lz = -1; lz <= 1; lz++)
            {
                double t = (zc + lz - ax.Z) / W;
                if (FractionalIsZero(xc - (ax.X + t * U)) && FractionalIsZero(yc - (ax.Y + t * V))) return true;
            }
        }
        if (V != 0)
        {
            for (int ly = -1; ly <= 1; ly++)
            {
                double t = (yc + ly - ax.Y) / V;
                if (FractionalIsZero(xc - (ax.X + t * U)) && FractionalIsZero(zc - (ax.Z + t * W))) return true;
            }
        }
        if (U != 0)
        {
            for (int lx = -1; lx <= 1; lx++)
            {
                double t = (xc + lx - ax.X) / U;
                if (FractionalIsZero(yc - (ax.Y + t * V)) && FractionalIsZero(zc - (ax.Z + t * W))) return true;
            }
        }
        return false;

        static bool FractionalIsZero(double v) => Math.Abs(v - Math.Round(v)) < 1e-6;
    }

    /// <summary>(260505Cl 追加) Miller indices を、l > 0 を優先する正規形に揃える ((h,k,l) と (-h,-k,-l) は同一平面なので dedup 用)。</summary>
    private static (int H, int K, int L) NormalizeMillerIndices((int U, int V, int W) n)
    {
        int h = n.U, k = n.V, l = n.W;
        if (l < 0 || (l == 0 && h < 0) || (l == 0 && h == 0 && k < 0))
        {
            h = -h; k = -k; l = -l;
        }
        return (h, k, l);
    }

    /// <summary>(260504Cl 追加) 結晶方向 [uvw] を上半球側に正規化し、stereo 投影位置を返す共通ヘルパ。</summary>
    private static PointF StereonetPosition(PointF stereonetCenter, float r, (int U, int V, int W) dir,
                                            out double inPlaneSx, out double inPlaneSy)
    {
        // 結晶 (u,v,w) → screen (Sx=v, Sy=u, Sz=w)。C 投影前提。
        double sx = dir.V, sy = dir.U, sz = dir.W;
        double norm = Math.Sqrt(sx * sx + sy * sy + sz * sz);
        if (norm < 1e-12) { inPlaneSx = inPlaneSy = 0; return stereonetCenter; }
        sx /= norm; sy /= norm; sz /= norm;
        if (sz < 0) { sx = -sx; sy = -sy; sz = -sz; } // 上半球側に正規化
        inPlaneSx = sx; inPlaneSy = sy;
        double denom = 1.0 + sz;
        return new PointF(stereonetCenter.X + r * (float)(sx / denom),
                          stereonetCenter.Y + r * (float)(sy / denom));
    }

    /// <summary>(260504Cl 追加) 結晶方向 [uvw] の 2 回軸を stereonet 上に描画する。
    /// (260505Cl) 視認性のため <011> 系の軸位置は <023>/<203> 系へ shift (in-plane 成分を 3×、out-of-plane 成分を 2× して strict 投影に渡す)。
    /// mirror 大円の (h,k,l)→(2h,2k,3l) shift と整合させる選択。
    /// screw=true で 2_1 螺旋 (互い違い円弧 fin) として描く。</summary>
    private static void DrawStereonetTwofold(Graphics g, Brush fill, PointF stereonetCenter, float r,
                                             (int U, int V, int W) dir, bool screw = false)
    {
        // var displayDir = (dir.U * 2, dir.V * 2, dir.W); // 旧: <011>→<021> shift (mirror 大円が (h,k,2l) shift だった頃)
        var displayDir = (dir.U * StereonetAxisInPlaneShift, dir.V * StereonetAxisInPlaneShift, dir.W * StereonetAxisDepthShift); // C 投影での <011>→<023> shift。mirror 大円の shift と整合。
        var pt = StereonetPosition(stereonetCenter, r, displayDir, out double sx, out double sy);
        float rotationDeg = Math.Abs(sx) + Math.Abs(sy) < 1e-9
            ? 0f
            : (float)(Math.Atan2(sy, sx) * 180.0 / Math.PI);
        // float overallScale = r / DiagonalStereonetRadius; // 260505Cl 旧: 輪郭半径と一緒にシンボルも拡大していた
        float overallScale = DiagonalStereonetSymbolScale; // 260505Cl: 輪郭 1.2 倍に追従させずシンボルは旧値を保持
        DrawTwofoldPerp(g, fill, pt, screw: screw,
                        scale: overallScale * StereonetTwofoldOverallScale,
                        rotationDeg: rotationDeg,
                        widthScale: StereonetTwofoldWidthScale,
                        finSweepScale: StereonetFinScale);
    }

    /// <summary>(260504Cl 追加) 結晶方向 [uvw] の 3 回軸を stereonet 上に描画する。
    /// (260505Cl) 視認性のため <111> 系を <332> 系に shift (in-plane 成分を 3×、out-of-plane 成分を 2× して strict 投影に渡す)。
    /// mirror 大円の (h,k,l)→(2h,2k,3l) shift により 3 本の {110} 系大円が [332] で交差するので、その交点に 3 回軸を載せる。
    /// (260505Cl 整理) 立方晶高対称群の stereonet 中心を通る 3 回軸は <111> proper のみで、3_1/3_2 螺旋は現れないため fin は出さない。</summary>
    private static void DrawStereonetThreefold(Graphics g, Brush fill, PointF stereonetCenter, float r,
                                               (int U, int V, int W) dir)
    {
        // var displayDir = (dir.U * 2, dir.V * 2, dir.W); // 旧: <111>→<221> shift (mirror が (h,k,2l) shift で大円交点が [221] だった頃)
        var displayDir = (dir.U * StereonetAxisInPlaneShift, dir.V * StereonetAxisInPlaneShift, dir.W * StereonetAxisDepthShift); // <111>→<332> shift。mirror 大円 3 本の交点と一致。
        var pt = StereonetPosition(stereonetCenter, r, displayDir, out _, out _);
        // float overallScale = r / DiagonalStereonetRadius; // 260505Cl 旧: 輪郭半径と一緒に三角もスケール
        float overallScale = DiagonalStereonetSymbolScale; // 260505Cl: 輪郭 1.2 倍に追従させずシンボルは旧値を保持
        float radius = ThreeFoldRadius * overallScale;

        // apex 頂点を stereonet 中心方向に向ける (atan2 は screen y-down で OK)
        double apexAngleRad = Math.Atan2(stereonetCenter.Y - pt.Y, stereonetCenter.X - pt.X);
        // 円周角の定理より、底辺頂点は apex から円周角 ±(180° - apex 角) の位置 (= 80° apex なら ±100°)。
        double baseDeltaRad = (180.0 - StereonetThreefoldApexDeg) * Math.PI / 180.0;

        // 外接円中心 (vertex 配置の幾何中心) と重心は一致しないので、重心が pt に来るよう外接円中心を逆方向に shift する。
        // 重心は外接円中心から apex 方向に radius * (1 + 2 cos(baseDelta)) / 3 ずれる。
        double centroidOffset = radius * (1.0 + 2.0 * Math.Cos(baseDeltaRad)) / 3.0;
        double cx = pt.X - centroidOffset * Math.Cos(apexAngleRad);
        double cy = pt.Y - centroidOffset * Math.Sin(apexAngleRad);
        var poly = new[]
        {
            new PointF((float)(cx + radius * Math.Cos(apexAngleRad)),                 (float)(cy + radius * Math.Sin(apexAngleRad))),
            new PointF((float)(cx + radius * Math.Cos(apexAngleRad + baseDeltaRad)),  (float)(cy + radius * Math.Sin(apexAngleRad + baseDeltaRad))),
            new PointF((float)(cx + radius * Math.Cos(apexAngleRad - baseDeltaRad)),  (float)(cy + radius * Math.Sin(apexAngleRad - baseDeltaRad))),
        };

        using var halo    = new Pen(Color.White, SymbolHaloPenWidth) { LineJoin = LineJoin.Round };
        using var outline = new Pen(Color.Black, OutlinePenWidth)    { LineJoin = LineJoin.Round };
        g.DrawPolygon(halo, poly);
        g.FillPolygon(fill, poly);
        g.DrawPolygon(outline, poly);
    }

    /// <summary>(260506Cl) Miller (h,k,l) を上半球側に正規化した stereonet 法線 + 大円幾何を返す。
    /// <paramref name="isDiameter"/> = true で「投影軸を含む面 → 直径線」(法線 (nx,ny) に直交する単位ベクトル (dx,dy))、
    /// false で「半径 r/n_z の円弧」(中心 gcCx/gcCy、半径 gcR)。norm=0 の縮退入力では false を返す。</summary>
    private static bool TryProjectGreatCircle((int H, int K, int L) hkl, PointF center, float r,
                                               out bool isDiameter, out double dx, out double dy,
                                               out float gcCx, out float gcCy, out float gcR)
    {
        // 260505Cl: 視認性のため (h,k,l) → (2h,2k,3l) shift。<011>→<023> 等で apex を外側へ寄せる。
        // 結晶 (h,k,l) → screen 法線 (Sx=k, Sy=h, Sz=l)。C 投影前提。
        double nx = hkl.K * StereonetMirrorInPlaneShift,
               ny = hkl.H * StereonetMirrorInPlaneShift,
               nz = hkl.L * StereonetMirrorDepthShift;
        double norm = Math.Sqrt(nx * nx + ny * ny + nz * nz);
        isDiameter = false; dx = dy = 0; gcCx = gcCy = gcR = 0;
        if (norm < 1e-12) return false;
        nx /= norm; ny /= norm; nz /= norm;
        if (nz < 0) { nx = -nx; ny = -ny; nz = -nz; } // 上半球側に正規化
        if (nz < 1e-9)
        {
            isDiameter = true; dx = -ny; dy = nx;
            return true;
        }
        // 単位 stereonet 上での大円: center=(nx/nz, ny/nz), radius=1/nz。
        gcCx = center.X + r * (float)(nx / nz);
        gcCy = center.Y + r * (float)(ny / nz);
        gcR  = r * (float)(1.0 / nz);
        return true;
    }

    /// <summary>(260505Ch) 大円本体 (直径線 or 円弧) を描く。
    /// 円弧は見えている弧そのものを DrawArc し、dash-dot の位相が隠れた楕円部分に引きずられないようにする。</summary>
    private static void DrawGreatCircleArc(Graphics g, Pen pen, PointF center, float r,
                                           bool isDiameter, double dx, double dy, float gcCx, float gcCy, float gcR)
    {
        if (isDiameter)
        {
            g.DrawLine(pen,
                center.X + r * (float)dx, center.Y + r * (float)dy,
                center.X - r * (float)dx, center.Y - r * (float)dy);
            return;
        }
        // 旧: clip を使っていた時代の GraphicsState 保存は不要。(260505Ch)
        double dcx = center.X - gcCx, dcy = center.Y - gcCy;
        double d = Math.Sqrt(dcx * dcx + dcy * dcy);
        if (d < 1e-6 || gcR < 1e-6)
        {
            g.DrawEllipse(pen, gcCx - gcR, gcCy - gcR, 2 * gcR, 2 * gcR);
            return;
        }
        // 旧: 大きな楕円を stereonet 円で clip していたため、dash phase が見えている弧の始端ではなく楕円の 0° から始まっていた。
        // using var clipPath = new GraphicsPath();
        // clipPath.AddEllipse(center.X - r, center.Y - r, 2 * r, 2 * r);
        // g.SetClip(clipPath, CombineMode.Intersect);
        // g.DrawEllipse(pen, gcCx - gcR, gcCy - gcR, 2 * gcR, 2 * gcR);
        double apexAngle = Math.Atan2(dcy, dcx);
        double halfSweep = Math.Acos(Math.Clamp(d / gcR, -1.0, 1.0));
        float startDeg = (float)((apexAngle - halfSweep) * 180.0 / Math.PI);
        float sweepDeg = (float)(2.0 * halfSweep * 180.0 / Math.PI);
        g.DrawArc(pen, gcCx - gcR, gcCy - gcR, 2 * gcR, 2 * gcR, startDeg, sweepDeg); // (260505Ch) 見えている弧そのものを描き、dash-dot の位相を始端に揃える。
    }

    /// <summary>(cx,cy) 中心・半径 r の単位 stereonet (上半球を南極から赤道面へ投影) 上に、
    /// Miller (h,k,l) の鏡映面の大円を描画する。equator 内側だけクリップして描く。
    /// 法線が紙面と平行 (l=0 で C 投影) なら直径線、それ以外は半径 r/|n_c| の円弧。</summary>
    private static void DrawMirrorGreatCircle(Graphics g, Pen pen, PointF stereonetCenter, float r, (int H, int K, int L) hkl)
    {
        if (!TryProjectGreatCircle(hkl, stereonetCenter, r, out bool isDiameter, out double dx, out double dy,
                                   out float gcCx, out float gcCy, out float gcR)) return;
        DrawGreatCircleArc(g, pen, stereonetCenter, r, isDiameter, dx, dy, gcCx, gcCy, gcR);
    }

    /// <summary>(260504Cl 追加) 斜め d 映進面の大円を stereonet 上に描画する。
    /// d-glide dash パターンの大円に加え、矢印重心が弧の -35°, 0, +35° に来るよう接線方向のアローを 3 個配置する。
    /// アローはクリップから外して描画 (apex の dash と重なって視認しにくいのを避ける)。</summary>
    private static void DrawDGlideGreatCircle(Graphics g, Pen pen, Brush fill, PointF stereonetCenter, float r, (int H, int K, int L) hkl)
    {
        if (!TryProjectGreatCircle(hkl, stereonetCenter, r, out bool isDiameter, out double dx, out double dy,
                                   out float gcCx, out float gcCy, out float gcR)) return;
        DrawGreatCircleArc(g, pen, stereonetCenter, r, isDiameter, dx, dy, gcCx, gcCy, gcR);
        if (isDiameter) return;

        // アローを clip 外で描画。接線方向 (大円中心から見た角度の +90° 方向) で置く。
        double apexAngle = Math.Atan2(stereonetCenter.Y - gcCy, stereonetCenter.X - gcCx);
        foreach (double off in StereonetDGlideArrowAnglesRad)
        {
            double a = apexAngle + off;
            float centroidX = gcCx + gcR * (float)Math.Cos(a);
            float centroidY = gcCy + gcR * (float)Math.Sin(a);
            float dxp = centroidX - stereonetCenter.X, dyp = centroidY - stereonetCenter.Y;
            if (dxp * dxp + dyp * dyp > r * r) continue;
            double tx = -Math.Sin(a), ty = Math.Cos(a);
            PointF tip = new((float)(centroidX + tx * ArrowHeadLen * StereonetDGlideArrowScale * ArrowHeadCentroidToTipFactor),
                             (float)(centroidY + ty * ArrowHeadLen * StereonetDGlideArrowScale * ArrowHeadCentroidToTipFactor)); // (260505Ch) DrawArrowhead は tip 指定なので、1.2 倍矢印の重心位置から逆算する。
            // DrawArrowhead(g, fill, new PointF(px, py), -Math.Sin(a), Math.Cos(a), StereonetDGlideArrowScale); // 旧: stereonet d-glide 矢印だけ 1.5 倍。
            // DrawArrowhead(g, fill, tip, tx, ty); // 旧: 紙面垂直 d-glide と同じ矢印サイズに揃える。
            DrawArrowhead(g, fill, tip, tx, ty, StereonetDGlideArrowScale); // (260505Ch)
        }
    }
    #endregion
}
