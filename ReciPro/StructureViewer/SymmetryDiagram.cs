using Crystallography;
using Crystallography.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using ZLinq;
using C4 = OpenTK.Mathematics.Color4;
using M3d = OpenTK.Mathematics.Matrix3d;
using V3 = OpenTK.Mathematics.Vector3d;
using V4 = OpenTK.Mathematics.Vector4d;

namespace ReciPro;

internal static class SymmetryDiagram
{
    #region 定数・型定義

    // --- 数値判定用閾値 ----------------------------------------------------
    private const double Tolerance = 1e-5;
    private const double ToleranceSquared = Tolerance * Tolerance; // 1e-10。零判定・長さ判定・近接判定の共通閾値。
    private const double InsideTol = 1e-7;                          // 半空間 inside 判定の余裕。
    private const int MaxTranslationSpan = 16;                      // 周期セル列挙の (a, b, c) 範囲上限。
    private const double RefVecParallelThreshold = 0.85;            // 直交基底構築時、normal と reference 軸の平行判定閾値 (|cosθ| 上限)。

    // --- 描画 bounds 拡張係数 (lattice plane と同じ logic; lattice 側は 1.2 倍) ---
    private const double ExtendedBoundsFactor = 1.45;       // 軸線・鏡映 bracket の延長
    private const double AxisSymbolBoundsFactor = 1.4;      // 軸種別シンボル配置位置

    // --- シンボル半径・形状サイズ ----------------------------------------
    // 「× scale」と書いてあるものは scale (= cell 軸長基準) との積、「× radius」は呼び出し側 radius との積。
    private const double AxisSymbolRadiusFactor = 0.045;    // 軸シンボル半径 (× scale)
    private const double InversionRadiusFactor = 0.005;     // 対称心球半径 (× scale)
    private const double Rotoinversion4LiftFactor = 0.02;   // 回反 -4 内側多角形を法線方向に持ち上げる量 (× radius, z-fighting 回避)

    // 軸対称鏡映面 (新スタイル) — cell 軸長基準
    private const double PlaneCornerOffset = 0.5 * ExtendedBoundsFactor; // = 0.725 (cell 半分 × 1.45)
    private const double PlaneBracketArm = 0.15;            // 平行四辺形辺長 = bracket 腕長 (× u, v 軸長)
    private const double PlaneArrowheadLengthFactor = 0.2;  // arrowhead 全長 = (armWorldU + armWorldV) × この値
    private const double PlaneArrowheadAspect = 0.3;        // arrowhead 半幅 = headLength × この値

    // 非軸対称鏡映面 (oblique) — scale または extent 基準
    private const double ObliqueBracketExtentRatio = 0.25;  // bracket half = extent × この値 (preferred)
    private const double ObliqueBracketHalfMin = 0.10;      // bracket half の下限 (× scale)
    private const double ObliqueBracketHalfMax = 0.20;      // bracket half の上限 (× scale)
    private const double ObliqueBracketHalfCap = 0.45;      // bracket half の絶対上限 (× extent)
    private const double ObliqueArrowLengthFactor = 0.055;  // glide arrowhead 全長 (× scale)
    private const double ObliqueArrowHalfWidthFactor = 0.022; // glide arrowhead 半幅 (× scale)

    // XY 単位テンプレート (静的シンボル定義用)
    private const double TemplatePolygonRadius = 0.5;       // 多角形外接円半径
    private const double TemplatePolygonFinRadius = 0.7;    // Fin を含む全体外接円半径
    private const double TemplateBracketArm = 0.45;         // oblique bracket の L 字腕長
    private const double TemplateGlideArrowHalfLength = 0.75; // glide arrow shaft 半長

    // --- 線幅 ------------------------------------------------------------
    private const float AxisLineWidth = 2.0f;               // 軸線 / 軸シンボル輪郭・フィン
    private const float PlaneLineWidth = 2.0f;              // 軸対称面 bracket
    private const float ObliqueLineWidth = 2.2f;            // oblique 鏡映 bracket・glide shaft

    // --- マテリアル設定 ---------------------------------------------------
    // 全シンボル共通の基本マテリアル (黒・グレー・白で色のみ差し替え)。
    private const float MaterialAmbient = 0.35f;
    private const float MaterialDiffuse = 0.45f;
    private const float MaterialSpecular = 0.25f;
    private const float MaterialSpecularPower = 3f;
    private const float MaterialEmission = 0.10f;
    // 軸対称面の半透明グレー塗り (平行四辺形)
    private const double FillOpacity = 0.5;
    private const float FillSpecular = 0.0f;
    private const float FillSpecularPower = 1.0f;
    private const float FillEmission = 0.30f;

    private readonly record struct TranslationRange(int MinA, int MaxA, int MinB, int MaxB, int MinC, int MaxC);

    /// <summary>鏡映面の種別。260510Ch: 旧 AxisA/AxisB/Diagonal*/Hex* の個別分類は廃止し、W=0 を Vertical に集約。</summary>
    private enum PlaneType { AxisC, Vertical }

    #endregion

    #region 公開 API (CreateObjects)

    // 260509Ch: 透明度や光沢違いもここでまとめ、Material 初期化の散在を避ける。
    private static Material MakeMaterial(C4 c, double opacity = 1.0,
                                         float specular = MaterialSpecular,
                                         float specularPower = MaterialSpecularPower,
                                         float emission = MaterialEmission) => new(c, opacity)
    {
        Ambient = MaterialAmbient, Diffuse = MaterialDiffuse,
        Specular = specular, SpecularPower = specularPower, Emission = emission,
    };

    /// <summary>SymmetryElementsTable の fractional 座標を StructureViewer 用 GLObject に変換する。</summary>
    internal static List<GLObject> CreateObjects(Crystal crystal, V3 shift, IReadOnlyList<V4> bounds, double symbolScale = 0)
    {
        if (SymmetryElementsTable.Get(crystal.SymmetrySeriesNumber) is not { } table || bounds.Count == 0) return [];

        // crystal.MatrixReal は Matrix3D(A_Axis, B_Axis, C_Axis) で列が a, b, c。
        // ToMatrix() で OpenTK の Matrix3d に変換すると FormStructureViewer 側で組み立てる axes と一致する。
        var axes = crystal.MatrixReal.ToMatrix();
        double scale = symbolScale > 0 ? symbolScale
            : Math.Max(1.0, new[] { axes.Column0.Length, axes.Column1.Length, axes.Column2.Length }
                .Where(v => v > ToleranceSquared).DefaultIfEmpty(1.0).Min());

        var range = GetTranslationRange(axes, shift, bounds);
        var (black, gray, white) = (MakeMaterial(C4.Black), MakeMaterial(C4.Gray), MakeMaterial(C4.White));

        // 260509Ch: 空リスト生成と AddRange の三段積みを、返却時の collection expression に集約。
        return
        [
            .. GenerateSymmetryAxes(table.SymmetryAxes, axes, shift, bounds, range, scale, black, gray),
            .. GenerateSymmetryPlanes(table.SymmetryPlanes, axes, shift, bounds, range, scale, black),
            .. GenerateInversionCenters(table.InversionCenters, axes, shift, bounds, range, scale, white),
        ];
    }

    #endregion

    #region 回転軸シンボル (Axis)

    /// <summary>回転軸 (rotation / screw / -4 回反) を全 cell 分列挙し、軸線 (gray) とシンボル (black) を追加する。</summary>
    private static List<GLObject> GenerateSymmetryAxes(SymmetryAxis[] axesTable, M3d axes, V3 shift,
                                        IReadOnlyList<V4> bounds, TranslationRange range, double scale, Material black, Material gray)
    {
        var objects = new List<GLObject>();
        var drawn = new HashSet<(long, long, long, long, long, long)>(); // 260509Cl: 向き不問 quantized HashSet で重複判定。
        double symbolRadius = scale * AxisSymbolRadiusFactor;

        var lineBounds = bounds.Select(b => new V4(b.X, b.Y, b.Z, b.W * ExtendedBoundsFactor)).ToArray();
        var symbolBounds = bounds.Select(b => new V4(b.X, b.Y, b.Z, b.W * AxisSymbolBoundsFactor)).ToArray();

        // 同位置・同方向に複数次数の軸が列挙される (例: P4 の 4 回軸には C₄² = 2 回軸が同位置に存在) ため、
        // 高次軸を先に処理して低次軸を線分 dedup で抑制する。-4 は独立記号として proper 4 より優先する。
        // -6/-3 は描画上 3 回軸扱いなので proper 6/3 と同線上にある場合は proper を優先 (P6/m の 6 回記号が消えないように)。260509Ch
        // priority: -4 → 0, proper → 1, それ以外 (-3 / -6) → 2
        foreach (var ax in axesTable.OrderByDescending(a => Math.Abs(a.Order))
                                    .ThenBy(a => a.Order == -4 ? 0 : a.Order > 0 ? 1 : 2))
        {
            if (Math.Abs(ax.Order) is not (2 or 3 or 4 or 6)) continue;
            if (!TryNormalize(axes * new V3(ax.Direction.U, ax.Direction.V, ax.Direction.W), out var axisDir)) continue;

            // -3 / -6 は 3 回回転と同じ扱いにする (対称心 / 鏡映は別パスで描画されるため独立シンボル不要)。
            int effOrder = ax.Order is -3 or -6 ? 3 : ax.Order;
            bool effScrew = ax.Order is not (-3 or -6) && ax.Screw;

            foreach (var cell in EnumerateCells(range))
            {
                var origin = axes * new V3(ax.X + cell.X, ax.Y + cell.Y, ax.Z + cell.Z) - shift;
                if (!TryClipLine(origin, axisDir, lineBounds, out var lineStart, out var lineEnd)) continue;
                if ((lineEnd - lineStart).Length < symbolRadius) continue;

                bool lineNew = drawn.Add(SegmentKey(lineStart, lineEnd));
                if (lineNew)
                    AddLine(objects, lineStart, lineEnd, AxisLineWidth, gray);

                if (ax.Order == -4)
                {
                    // -4 は線分の両端ではなく、各 cell の軸上の作用点 (origin) に 1 個だけ配置する。
                    if (IsPointInsideBounds(origin, symbolBounds))
                        objects.AddRange(GenerateAxisSymbol(origin, axisDir, ax.Order, ax.Screw, ax.FinCount, ax.EdgeStep, symbolRadius, black));
                }
                else if (lineNew && TryClipLine(origin, axisDir, symbolBounds, out var symStart, out var symEnd))
                {
                    objects.AddRange(GenerateAxisSymbol(symStart, axisDir, effOrder, effScrew, ax.FinCount, ax.EdgeStep, symbolRadius, black));
                    objects.AddRange(GenerateAxisSymbol(symEnd, axisDir, effOrder, effScrew, ax.FinCount, ax.EdgeStep, symbolRadius, black));
                }
            }
        }
        return objects;
    }

    /// <summary>軸種別 (2/3/4/6 回転・らせん・-4 回反) を判定して該当シンボルを生成する。
    /// proper rotation / screw は static V3[][] テンプレート (先頭=塗りつぶし多角形、続き=open Lines)。
    /// -3 / -6 は呼び出し側で 3 回軸に置換されるためここには来ない。-4 は内側 + 外側輪郭。</summary>
    private static List<GLObject> GenerateAxisSymbol(V3 center, V3 normal, int order, bool screw, int finCount,
                                                     int edgeStep, double radius, Material black)
    {
        // normal に直交する正規直交基底 (u, v) を作る
        var refVec = Math.Abs(V3.Dot(normal, new V3(0, 0, 1))) < RefVecParallelThreshold ? new V3(0, 0, 1) : new V3(0, 1, 0);
        TryNormalize(V3.Cross(refVec, normal), out var u);
        var v = V3.Cross(normal, u);
        int n = Math.Abs(order);

        if (order > 0)
        {
            V3[][] rotation = n switch { 3 => Rotation3, 4 => Rotation4, 6 => Rotation6, _ => Rotation2 };
            // (FinCount, EdgeStep) で 3_1/3_2, 4_1/4_2/4_3, 6_1〜6_5 を一意判別。EdgeStep は k 値そのものではなく旋回方向 (chirality) のマーカー。
            // 詳細は SymmetryElementsTable.PinwheelFins の XML doc を参照。
            // 例: 6_2 は (3, 5) — fin 数 3 + 右巻きマーカー 5 (6_1 と同じ)。
            V3[][] template = (n, screw, finCount, edgeStep) switch
            {
                (2, false, _, _) => Rotation2,
                (2, true, _, _) => Screw2_1,
                (3, false, _, _) => Rotation3,
                (3, true, 3, 2) => Screw3_1,
                (3, true, 3, 1) => Screw3_2,
                (4, false, _, _) => Rotation4,
                (4, true, 4, 3) => Screw4_1,
                (4, true, 2, 1) => Screw4_2,
                (4, true, 4, 1) => Screw4_3,
                (6, false, _, _) => Rotation6,
                (6, true, 6, 5) => Screw6_1,
                (6, true, 3, 5) => Screw6_2,
                (6, true, 2, 1) => Screw6_3,
                (6, true, 3, 1) => Screw6_4,
                (6, true, 6, 1) => Screw6_5,
                _ => rotation,
            };
            return RenderTemplate(template, center, u, v, radius, black);
        }

        // 回反 -4 (内側レンズ + 外側四角輪郭)。-3 / -6 はここには来ない (3 回軸に置換済み)。
        if (n != 4)
            throw new InvalidOperationException($"Unsupported rotoinversion order: {order} (-3 / -6 は 3 回軸として扱う)");

        var liftedCenter = center + normal * (radius * Rotoinversion4LiftFactor);
        var outerVerts = AffineTransformXY(Rotoinversion4[1], center, u, v, radius);
        return
        [
            new Polygon(AffineTransformXY(Rotoinversion4[0], liftedCenter, u, v, radius), black, DrawingMode.Surfaces)
            {
                IgnoreNormalSides = true, ShowClippedSection = false,
            },
            new Lines([.. outerVerts, outerVerts[0]], AxisLineWidth, black), // 末尾に先頭頂点を追加して LineStrip を閉ループ化
        ];
    }

    /// <summary>V3[][] テンプレートをアフィン変換して GLObject 列を返す:
    /// 先頭は「surface 塗りつぶし + edge 輪郭」、続く要素は open Lines。</summary>
    private static List<GLObject> RenderTemplate(V3[][] template, V3 center, V3 u, V3 v, double scale, Material mat)
    {
        var fillVerts = AffineTransformXY(template[0], center, u, v, scale);
        // 260509Ch: template は全呼び出しで非空なので空チェックを外し、戻り値生成を直列化。
        return
        [
            new Polygon(fillVerts, mat, DrawingMode.Surfaces) { IgnoreNormalSides = true, ShowClippedSection = false },
            new Polygon(fillVerts, mat, DrawingMode.Edges) { IgnoreNormalSides = true, ShowClippedSection = false, LineWidth = AxisLineWidth },
            .. template.Skip(1).Select(line => new Lines(AffineTransformXY(line, center, u, v, scale), AxisLineWidth, mat)),
        ];
    }

    #endregion

    #region 鏡映面シンボル (Plane)

    /// <summary>鏡映面 (mirror / glide) を全 cell 分列挙し、平行四辺形 + bracket + glide arrow からなる GLObject 列を返す。
    /// 軸対称面 (法線が cell 軸 / 対角 / 六方 [120][210] に沿う) は 4 隅に bracket を配置した新スタイル、
    /// それ以外は clipped polygon centroid に corner bracket + glide arrow を描く従来描画。260509Cl 仕様変更</summary>
    private static List<GLObject> GenerateSymmetryPlanes(SymmetryPlane[] planes, M3d axes, V3 shift,
                                        IReadOnlyList<V4> bounds, TranslationRange range, double scale, Material black)
    {
        var objects = new List<GLObject>();
        var drawn = new Dictionary<(long, long, long, long), int>(); // 260509Ch: 平面 dedup と、その面に描画済みの glide 方向 mask。
        var invAxes = M3d.Invert(axes); // 260510Ch: Miller 面法線 hkl は逆格子側 A^{-T} で実空間化する。
        var boundsArray = bounds.Select(b => new[] { b.X, b.Y, b.Z, b.W }).ToArray();
        var extendedBounds = bounds.Select(b => new V4(b.X, b.Y, b.Z, b.W * ExtendedBoundsFactor)).ToArray();
        var extendedBoundsArray = extendedBounds.Select(b => new[] { b.X, b.Y, b.Z, b.W }).ToArray();

        // 260509Cl: 描画 bounds の clipped 多角形頂点は plane に依存しないので一度だけ計算し、各 plane では射影 max のみ取る。
        var boundsVertices = ValueEnumerable.Range(0, boundsArray.Length)
            .Select(i => Geometry.GetClippedPolygon(i, boundsArray))
            .Where(c => c != null)
            .SelectMany(c => c.Select(p => new V3(p[0], p[1], p[2])))
            .ToArray();

        var grayFill = MakeMaterial(C4.Gray, FillOpacity, FillSpecular, FillSpecularPower, FillEmission);

        foreach (var mp in planes)
        {
            // 旧: if (!TryNormalize(axes * new V3(mp.Normal.U, mp.Normal.V, mp.Normal.W), out var normalWorld)) continue;
            // 260510Ch: SymmetryPlane.Normal は Miller 面指数なので、直接格子ではなく逆格子ベクトル A^{-T}·hkl で実空間化する。
            if (!TryNormalize(invAxes.Row0 * mp.Normal.U + invAxes.Row1 * mp.Normal.V + invAxes.Row2 * mp.Normal.W, out var normalWorld)) continue;

            if (GetPlaneType(mp.Normal) is { } planeType)
                GenerateAxisAlignedPlane(mp, planeType, axes, shift, bounds, boundsArray, extendedBounds, boundsVertices, range,
                                         normalWorld, black, grayFill, drawn, objects);
            else
            {
                var glide = axes * new V3(mp.Glide.U, mp.Glide.V, mp.Glide.W);
                GenerateObliquePlane(mp, axes, shift, extendedBoundsArray, range, scale,
                                     normalWorld, glide - normalWorld * V3.Dot(glide, normalWorld), black, drawn, objects);
            }
        }
        return objects;
    }

    /// <summary>軸対称鏡映面の新スタイル描画 (260509Cl 追加):
    /// 各 cell の 4 隅 (cell 中心から ±0.725·u, ±0.725·v) に bracket と平行四辺形を配置。
    /// 対角面では bracket 対角線方向 ray と 1.45 倍 bounds の交点に bracket 頂点を置く (260509Ch)。
    /// 映進面は腕の先端 (軸映進) または対角線の先端 (n / d 映進) に平面三角形 arrowhead を付ける。</summary>
    private static void GenerateAxisAlignedPlane(SymmetryPlane mp, PlaneType type, M3d axes, V3 shift,
                                                  IReadOnlyList<V4> bounds, double[][] boundsArray, IReadOnlyList<V4> extendedBounds,
                                                  V3[] boundsVertices,
                                                  TranslationRange range, V3 normalWorld,
                                                  Material black, Material grayFill,
                                                   Dictionary<(long, long, long, long), int> drawn,
                                                  List<GLObject> objects)
    {
        // 面内軸 u, v を plane type で決定。
        // 260510Ch: AxisA/AxisB/Diagonal/Hex* の個別分類を廃止し、Miller 面指数 (h,k,0) から (k,-h,0) で面内方向を 1 式に統一。
        (V3 u, V3 v) = type switch
        {
            PlaneType.AxisC => (axes.Column0, axes.Column1),
            PlaneType.Vertical => (axes.Column2, axes * new V3(mp.Normal.V, -mp.Normal.U, 0)),
            _ => (default, default),
        };
        var uHat = u.Normalized();
        var vHat = v.Normalized();

        bool rayClippedPlane = type != PlaneType.AxisC; // W=0 の垂直鏡面。260509Ch

        double armWorldU = PlaneBracketArm * u.Length;
        double armWorldV = PlaneBracketArm * v.Length;
        double headLength = (armWorldU + armWorldV) * PlaneArrowheadLengthFactor;
        double headHalfWidth = headLength * PlaneArrowheadAspect;

        // 映進面の種別: 面内 glide のフラクショナル成分が片方のみ → 軸映進、両方非ゼロ → 対角 (n / d)。
        // 260510Ch: Miller 面 (h,k,0) の面内 glide 成分は (k,-h,0) 方向の射影で表せるため、AxisC と Vertical の 2 式に統一。
        var (gu, gv) = type switch
        {
            PlaneType.AxisC => (mp.Glide.U, mp.Glide.V),
            PlaneType.Vertical => (mp.Glide.W, mp.Normal.V * mp.Glide.U - mp.Normal.U * mp.Glide.V),
            _ => (0, 0),
        };
        const double glideTol = 1e-6;
        bool hasU = Math.Abs(gu) > glideTol;
        bool hasV = Math.Abs(gv) > glideTol;
        int glideMask = hasU && hasV ? 4 : (hasU ? 1 : 0) | (hasV ? 2 : 0); // 260509Ch: 1=u, 2=v, 4=対角(n/d)。

        // 周期境界上の mirror 重複除去用: bounds 頂点の normalWorld 射影最大値。260509Cl: 頂点列は呼び出し側で 1 回のみ計算済み。
        double? maxPlaneProjection = rayClippedPlane && boundsVertices.Length > 0
            ? boundsVertices.Max(p => (double?)V3.Dot(normalWorld, p))
            : null;

        foreach (var cell in EnumerateCells(range))
        {
            // 表示中心: AxisC は cell 中心 (0.5, 0.5)、垂直面は SymmetryPlane の代表点 (六方晶対応)。260509Ch
            var fracCenter = rayClippedPlane
                ? new V3(mp.X + cell.X, mp.Y + cell.Y, mp.Z + cell.Z)
                : new V3(cell.X + 0.5, cell.Y + 0.5, mp.Z + cell.Z);
            var center = axes * fracCenter - shift;
            var planePoint = axes * new V3(mp.X + cell.X, mp.Y + cell.Y, mp.Z + cell.Z) - shift;
            double d = -V3.Dot(normalWorld, planePoint);

            V3 bracketRayOrigin = center;
            if (rayClippedPlane)
            {
                // 旧処理: if (maxPlaneProjection.HasValue && -d >= maxPlaneProjection.Value - Tolerance) continue;
                // (260509Ch) P2/m などで最大側の格子境界上にある鏡映面も見せるため、境界一致は許容し、明確に外側の面だけ除外する。
                if (maxPlaneProjection.HasValue && -d > maxPlaneProjection.Value + InsideTol) continue;

                // 描画 bounds とこの面の交差有無で採否を決め、clipped polygon の重心を bracket の参照点に取る。260509Ch
                var clipped = Geometry.GetClippedPolygon([normalWorld.X, normalWorld.Y, normalWorld.Z, d], boundsArray);
                if (clipped == null || clipped.Length == 0) continue;
                bracketRayOrigin = clipped.Select(p => new V3(p[0], p[1], p[2])).Aggregate((a, b) => a + b) / clipped.Length;
            }
            else if (!IsPointInsideBounds(center, bounds))
                continue;

            // 同一平面 (n, d) は 1 セット (4 brackets) のみ描画。e 映進で別方向 glide が後から来た場合は矢印だけ追加。260509Ch
            var planeKey = PlaneKey(normalWorld, d);
            bool drawPlane = !drawn.TryGetValue(planeKey, out int drawnGlideMask);
            int arrowsToDraw = glideMask & ~drawnGlideMask;
            if (!drawPlane && arrowsToDraw == 0) continue;
            drawn[planeKey] = drawnGlideMask | glideMask;

            foreach (var (su, sv) in new[] { (-1, -1), (1, -1), (1, 1), (-1, 1) })
            {
                var bracketCorner = center + u * (su * PlaneCornerOffset) + v * (sv * PlaneCornerOffset);
                if (rayClippedPlane)
                {
                    // 対角線方向 ray と拡張 bounds の交点を頂点にする。260509Ch
                    var rayDir = u * su + v * sv;
                    if (TryNormalize(rayDir, out rayDir) &&
                        TryClipLine(bracketRayOrigin, rayDir, extendedBounds, out var rayStart, out var rayEnd))
                        bracketCorner = V3.Dot(rayEnd - bracketRayOrigin, rayDir) >=
                                        V3.Dot(rayStart - bracketRayOrigin, rayDir) ? rayEnd : rayStart;
                }

                var dirU = uHat * -su;       // 腕方向 = cell 中心向き
                var dirV = vHat * -sv;
                var armEndU = bracketCorner + dirU * armWorldU;
                var armEndV = bracketCorner + dirV * armWorldV;
                var p11 = armEndU + dirV * armWorldV; // 平行四辺形の対角頂点

                if (drawPlane)
                {
                    objects.Add(new Polygon([bracketCorner, armEndU, p11, armEndV], grayFill, DrawingMode.Surfaces)
                    {
                        IgnoreNormalSides = true, ShowClippedSection = false,
                    });
                    AddLine(objects, bracketCorner, armEndU, PlaneLineWidth, black);
                    AddLine(objects, bracketCorner, armEndV, PlaneLineWidth, black);
                }

                if ((arrowsToDraw & 1) != 0)
                    objects.Add(BuildArrowhead(armEndU, dirU, normalWorld, headLength, headHalfWidth, black));
                if ((arrowsToDraw & 2) != 0)
                    objects.Add(BuildArrowhead(armEndV, dirV, normalWorld, headLength, headHalfWidth, black));
                if ((arrowsToDraw & 4) != 0)
                {
                    AddLine(objects, bracketCorner, p11, PlaneLineWidth, black);
                    var diagDir = (p11 - bracketCorner).Normalized();
                    objects.Add(BuildArrowhead(p11, diagDir, normalWorld, headLength, headHalfWidth, black));
                }
            }
        }
    }

    /// <summary>非軸対称鏡映面 (oblique) の従来描画: clipped polygon centroid に corner bracket + glide arrow。260509Cl 追加</summary>
    private static void GenerateObliquePlane(SymmetryPlane mp, M3d axes, V3 shift,
                                              double[][] boundsArray, TranslationRange range, double scale,
                                              V3 normalWorld, V3 inPlaneGlide,
                                              Material black, Dictionary<(long, long, long, long), int> drawn, List<GLObject> objects)
    {
        bool hasGlide = TryNormalize(inPlaneGlide, out var glideDir);

        foreach (var cell in EnumerateCells(range))
        {
            var point = axes * new V3(mp.X + cell.X, mp.Y + cell.Y, mp.Z + cell.Z) - shift;
            double d = -V3.Dot(normalWorld, point);

            var clipped = Geometry.GetClippedPolygon([normalWorld.X, normalWorld.Y, normalWorld.Z, d], boundsArray);
            if (clipped == null || clipped.Length < 3) continue;
            var vertices = clipped.Select(p => new V3(p[0], p[1], p[2])).ToArray();

            // 法線 ±符号不問の重複判定: 同じ平面は (n, d) でも (-n, -d) でも一致。260509Ch: axis plane と同じ dictionary を使う。
            var planeKey = PlaneKey(normalWorld, d);
            if (drawn.ContainsKey(planeKey)) continue;
            drawn[planeKey] = hasGlide ? 1 : 0;

            var bracketCenter = vertices.Aggregate((a, b) => a + b) / vertices.Length;

            // bracket 平面の正規直交基底。glide があればその方向を u に揃える
            var bu = glideDir;
            if (!hasGlide)
            {
                var refVec = Math.Abs(V3.Dot(normalWorld, new V3(0, 0, 1))) < RefVecParallelThreshold ? new V3(0, 0, 1) : new V3(0, 1, 0);
                TryNormalize(V3.Cross(refVec, normalWorld), out bu);
            }
            var bv = V3.Cross(normalWorld, bu);

            double extent = vertices.Max(p => (p - bracketCenter).Length);
            double half = Math.Clamp(extent * ObliqueBracketExtentRatio, scale * ObliqueBracketHalfMin, scale * ObliqueBracketHalfMax);
            if (extent > ToleranceSquared) half = Math.Min(half, extent * ObliqueBracketHalfCap);

            foreach (var (s, e) in BracketLinesXY)
                AddLine(objects, TransformXY(s, bracketCenter, bu, bv, half),
                                 TransformXY(e, bracketCenter, bu, bv, half), ObliqueLineWidth, black);

            if (hasGlide)
            {
                var arrowStart = TransformXY(GlideArrowShaftXY.Start, bracketCenter, bu, bv, half);
                var arrowEnd = TransformXY(GlideArrowShaftXY.End, bracketCenter, bu, bv, half);
                AddLine(objects, arrowStart, arrowEnd, ObliqueLineWidth, black);
                objects.Add(BuildArrowhead(arrowEnd, glideDir, normalWorld, scale * ObliqueArrowLengthFactor, scale * ObliqueArrowHalfWidthFactor, black));
            }
        }
    }

    /// <summary>SymmetryPlane.Normal が basal 面または c 軸を含む垂直面なら分類する。260510Ch</summary>
    private static PlaneType? GetPlaneType((int U, int V, int W) n)
    {
        // 旧: AxisA/AxisB/Diagonal/Hex120/Hex210 を個別パターンで列挙していた。
        // Miller hkl では W=0 の面内方向は常に (k,-h,0) で得られるため、3/6 回転同値面も同じ式で扱える。
        if (n.U == 0 && n.V == 0 && n.W != 0) return PlaneType.AxisC;
        if (n.W == 0 && (n.U != 0 || n.V != 0)) return PlaneType.Vertical;
        return null;
    }

    #endregion

    #region 対称心シンボル (Inversion Center)

    /// <summary>対称心 (inversion center) を全 cell 分列挙し、白い小球の GLObject 列を返す。</summary>
    private static List<GLObject> GenerateInversionCenters(InversionCenter[] centers, M3d axes, V3 shift,
                                            IReadOnlyList<V4> bounds, TranslationRange range, double scale, Material white)
    {
        var objects = new List<GLObject>();
        var drawn = new HashSet<(long, long, long)>(); // 260509Cl: List + linear scan を quantized HashSet に変更し O(N²) → O(N) に。
        double radius = scale * InversionRadiusFactor;
        foreach (var inv in centers)
            foreach (var cell in EnumerateCells(range))
            {
                var point = axes * new V3(inv.X + cell.X, inv.Y + cell.Y, inv.Z + cell.Z) - shift;
                if (!IsPointInsideBounds(point, bounds)) continue;
                if (!drawn.Add((SymmetryElementsTable.R6(point.X), SymmetryElementsTable.R6(point.Y), SymmetryElementsTable.R6(point.Z)))) continue;
                objects.Add(new Sphere(point, radius, white, DrawingMode.Surfaces));
            }
        return objects;
    }

    #endregion

    #region 静的シンボルテンプレート (XY 平面 / 単位スケール)

    // 対称要素シンボルの頂点情報を XY 平面 (Z=0) で静的に定義し、
    // 描画時は TransformXY / AffineTransformXY で (center, u, v) 平面 + 任意 scale に写す。

    /// <summary>軸種別シンボルの静的テンプレート (V3[][]):
    /// 先頭の V3[] は塗りつぶし多角形、続く V3[] は open Lines (フィン / 弧)。
    /// 回反 (-4) の内側多角形は Rotation2[0] を再利用する。</summary>
    private static readonly V3[][] Rotation2 = [BuildLens()];
    private static readonly V3[][] Rotation3 = [BuildRegularPolygon(3, TemplatePolygonRadius)];
    private static readonly V3[][] Rotation4 = [BuildRegularPolygon(4, TemplatePolygonRadius)];
    private static readonly V3[][] Rotation6 = [BuildRegularPolygon(6, TemplatePolygonRadius)];
    private static readonly V3[][] Screw2_1 = [BuildLens(), BuildLensFin(true), BuildLensFin(false)];

    private static readonly V3[][] Screw3_1 = [Rotation3[0], .. ValueEnumerable.Range(0, 3).Select(i => BuildRegularPolygonFin(3, i, true,  TemplatePolygonRadius, TemplatePolygonFinRadius))];
    private static readonly V3[][] Screw3_2 = [Rotation3[0], .. ValueEnumerable.Range(0, 3).Select(i => BuildRegularPolygonFin(3, i, false, TemplatePolygonRadius, TemplatePolygonFinRadius))];
    private static readonly V3[][] Screw4_1 = [Rotation4[0], .. ValueEnumerable.Range(0, 4).Select(i => BuildRegularPolygonFin(4, i, true,  TemplatePolygonRadius, TemplatePolygonFinRadius))];
    private static readonly V3[][] Screw4_2 = [Rotation4[0], .. new[] { 0, 2 }       .Select(i => BuildRegularPolygonFin(4, i, false, TemplatePolygonRadius, TemplatePolygonFinRadius))];
    private static readonly V3[][] Screw4_3 = [Rotation4[0], .. ValueEnumerable.Range(0, 4).Select(i => BuildRegularPolygonFin(4, i, false, TemplatePolygonRadius, TemplatePolygonFinRadius))];
    private static readonly V3[][] Screw6_1 = [Rotation6[0], .. ValueEnumerable.Range(0, 6).Select(i => BuildRegularPolygonFin(6, i, true,  TemplatePolygonRadius, TemplatePolygonFinRadius))];
    private static readonly V3[][] Screw6_2 = [Rotation6[0], .. new[] { 0, 2, 4 }    .Select(i => BuildRegularPolygonFin(6, i, true,  TemplatePolygonRadius, TemplatePolygonFinRadius))];
    private static readonly V3[][] Screw6_3 = [Rotation6[0], .. new[] { 0, 3 }       .Select(i => BuildRegularPolygonFin(6, i, false, TemplatePolygonRadius, TemplatePolygonFinRadius))];
    private static readonly V3[][] Screw6_4 = [Rotation6[0], .. new[] { 0, 2, 4 }    .Select(i => BuildRegularPolygonFin(6, i, false, TemplatePolygonRadius, TemplatePolygonFinRadius))];
    private static readonly V3[][] Screw6_5 = [Rotation6[0], .. ValueEnumerable.Range(0, 6).Select(i => BuildRegularPolygonFin(6, i, false, TemplatePolygonRadius, TemplatePolygonFinRadius))];

    private static readonly V3[][] Rotoinversion4 = [BuildLens(), Rotation4[0]];

    /// <summary>鏡映面 bracket の 8 本の L 字線分。half = 1, arm = TemplateBracketArm 基準。</summary>
    private static readonly (V3 Start, V3 End)[] BracketLinesXY =
        [.. from sx in new[] { -1, 1 }
            from sy in new[] { -1, 1 }
            let corner = new V3(sx, sy, 0)
            from line in new[] { (corner, corner - new V3(sx * TemplateBracketArm, 0, 0)),
                                 (corner, corner - new V3(0, sy * TemplateBracketArm, 0)) }
            select line];

    /// <summary>glide arrow の shaft。half = 1 基準で ±TemplateGlideArrowHalfLength。</summary>
    private static readonly (V3 Start, V3 End) GlideArrowShaftXY =
        (new V3(-TemplateGlideArrowHalfLength, 0, 0), new V3(TemplateGlideArrowHalfLength, 0, 0));

    /// <summary>-30°〜+30° の弧と、それを原点反転した弧の 2 本でレンズ形を作る。</summary>
    private static V3[] BuildLens()
    {
        const int slices = 60;
        double x = Math.Sqrt(3) / 2;
        var upper = ValueEnumerable.Range(0, slices).Select(i =>
        {
            var (sin, cos) = Math.SinCos(i * Math.PI / 3 / slices - Math.PI / 6);
            return new V3(cos - x, sin, 0);
        }).ToArray();
        return [.. upper, .. upper.Select(p => -p)];
    }

    /// <summary>+30°〜+45° の弧 (upper=true) または -30°〜-45° の弧 (upper=false)。</summary>
    private static V3[] BuildLensFin(bool upper)
    {
        const int slices = 15;
        double x = Math.Sqrt(3) / 2;
        return [.. ValueEnumerable.Range(0, slices+1).Select(i =>
        {
            var (sin, cos) = Math.SinCos(i * Math.PI / 12 / slices + Math.PI / 6);
            return upper ? new V3(cos - x, sin, 0) : new V3(-cos + x, -sin, 0);
        })];
    }

    /// <summary>正 n 角形の頂点 i (0-based, -π/2 起点・反時計回り) を半径 r の円上に取る。260509Cl 追加</summary>
    private static V3 PolygonVertex(int n, int i, double r)
    {
        var (sin, cos) = Math.SinCos(-Math.PI / 2.0 + i * 2.0 * Math.PI / n);
        return new V3(r * cos, r * sin, 0);
    }

    /// <summary>正 n 角形。</summary>
    private static V3[] BuildRegularPolygon(int n, double r) =>
        [.. ValueEnumerable.Range(0, n).Select(i => PolygonVertex(n, i, r))];

    /// <summary>正 n 角形の頂点 m から、隣接辺を外側へ延長した直線上を伸びる 1 本の Fin
    /// (open 線分: 始点 = 頂点 m, 終点 = 半径 r2 の円上)。
    /// dir=true は隣接頂点 m+1 と反対方向 = 時計回り方向に, dir=false は m-1 と反対方向 = 反時計回り方向に伸びる。
    /// 先端の長さは |vertex + t·d|² = r2² の正根として求める。260509Cl 追加</summary>
    private static V3[] BuildRegularPolygonFin(int n, int m, bool dir, double r1, double r2)
    {
        var vertex = PolygonVertex(n, m, r1);
        var neighbor = PolygonVertex(n, dir ? m + 1 : m - 1, r1);
        var diff = vertex - neighbor;
        double chord = diff.Length;
        if (chord < ToleranceSquared) return [vertex, vertex];
        var d = diff / chord;
        double vdotd = V3.Dot(vertex, d);
        double tailLen = -vdotd + Math.Sqrt(Math.Max(0.0, vdotd * vdotd + r2 * r2 - r1 * r1));
        return [vertex, vertex + d * tailLen];
    }

    #endregion

    #region GLObject 構築ヘルパー

    /// <summary>2 点間の直線を Lines として追加。</summary>
    private static void AddLine(List<GLObject> objects, V3 start, V3 end, float width, Material mat)
        => objects.Add(new Lines([start, end], width, mat));

    // 260509Cl 追加: 点が全 half-space bounds の内側 (b·p + w ≥ -InsideTol) にあるかを判定する。
    private static bool IsPointInsideBounds(V3 point, IReadOnlyList<V4> bounds)
        => bounds.All(b => b.X * point.X + b.Y * point.Y + b.Z * point.Z + b.W >= -InsideTol);

    // 260509Cl: 線分 dedup 用 quantized key。両端点を lex 順で正規化して向き不問にする。
    private static (long, long, long, long, long, long) SegmentKey(V3 a, V3 b)
    {
        long ax = SymmetryElementsTable.R6(a.X), ay = SymmetryElementsTable.R6(a.Y), az = SymmetryElementsTable.R6(a.Z);
        long bx = SymmetryElementsTable.R6(b.X), by = SymmetryElementsTable.R6(b.Y), bz = SymmetryElementsTable.R6(b.Z);
        bool aFirst = ax < bx || (ax == bx && (ay < by || (ay == by && az <= bz)));
        return aFirst ? (ax, ay, az, bx, by, bz) : (bx, by, bz, ax, ay, az);
    }

    // 260509Cl: 平面 dedup 用 quantized key。法線符号 ± 不問になるよう先頭非零成分が正になる側へ揃える。
    private static (long, long, long, long) PlaneKey(V3 normal, double d)
    {
        long nx = SymmetryElementsTable.R6(normal.X), ny = SymmetryElementsTable.R6(normal.Y);
        long nz = SymmetryElementsTable.R6(normal.Z), dl = SymmetryElementsTable.R6(d);
        bool flip = nx < 0 || (nx == 0 && (ny < 0 || (ny == 0 && nz < 0)));
        return flip ? (-nx, -ny, -nz, -dl) : (nx, ny, nz, dl);
    }

    /// <summary>tip を頂点とする平面三角形 arrowhead を生成する (映進面の矢じり用)。
    /// dir = arrow 方向 (unit, 平面内), normal = 平面法線 (unit)。底辺は dir に直交し平面内に取る。260509Cl 追加</summary>
    private static Polygon BuildArrowhead(V3 tip, V3 dir, V3 normal, double headLength, double headHalfWidth, Material mat)
    {
        if (!TryNormalize(V3.Cross(normal, dir), out var perp)) perp = default;
        var baseCenter = tip - dir * headLength;
        return new Polygon([tip, baseCenter + perp * headHalfWidth, baseCenter - perp * headHalfWidth], mat, DrawingMode.Surfaces)
        {
            IgnoreNormalSides = true, ShowClippedSection = false,
        };
    }

    #endregion

    #region アフィン変換 / 幾何ユーティリティ

    /// <summary>XY 平面上の 1 点 (Z=0) を、3D 平面 (center, u, v) へスケール scale で写す。
    /// 写像: (x, y, 0) → center + u·x·scale + v·y·scale。</summary>
    private static V3 TransformXY(V3 p, V3 center, V3 u, V3 v, double scale)
        => center + u * (p.X * scale) + v * (p.Y * scale);

    /// <summary>XY 平面上のテンプレート (Z=0) を、3D 平面 (center, u, v) へスケール scale で写す。</summary>
    private static V3[] AffineTransformXY(V3[] xyTemplate, V3 center, V3 u, V3 v, double scale) =>
        [.. xyTemplate.Select(p => TransformXY(p, center, u, v, scale))];

    /// <summary>ベクトルを正規化する。長さが ToleranceSquared 未満なら false を返し out は default。</summary>
    private static bool TryNormalize(V3 value, out V3 normalized)
    {
        double len = value.Length;
        if (len < ToleranceSquared) { normalized = default; return false; }
        normalized = value / len;
        return true;
    }

    /// <summary>原点 origin 通り direction 方向の直線を、半空間 bounds で clip する (Liang–Barsky)。</summary>
    private static bool TryClipLine(V3 origin, V3 direction, IReadOnlyList<V4> bounds, out V3 start, out V3 end)
    {
        double tMin = double.NegativeInfinity, tMax = double.PositiveInfinity;
        foreach (var b in bounds)
        {
            double p = b.X * origin.X + b.Y * origin.Y + b.Z * origin.Z + b.W;
            double q = b.X * direction.X + b.Y * direction.Y + b.Z * direction.Z;
            if (Math.Abs(q) < ToleranceSquared)
            {
                if (p < -ToleranceSquared) { start = end = default; return false; }
                continue;
            }
            double t = -p / q;
            if (q > 0) tMin = Math.Max(tMin, t);
            else tMax = Math.Min(tMax, t);
            if (tMin > tMax + ToleranceSquared) { start = end = default; return false; }
        }
        if (double.IsInfinity(tMin) || double.IsInfinity(tMax)) { start = end = default; return false; }
        start = origin + direction * tMin;
        end = origin + direction * tMax;
        return true;
    }

    #endregion

    #region 周期セル列挙 (GetTranslationRange / EnumerateCells)

    /// <summary>bounds 内に入る格子点を列挙するための (a, b, c) 範囲を求める。MaxTranslationSpan で上限を制限。</summary>
    private static TranslationRange GetTranslationRange(M3d axes, V3 shift, IReadOnlyList<V4> bounds)
    {
        var boundsArray = bounds.Select(b => new[] { b.X, b.Y, b.Z, b.W }).ToArray();
        var invAxes = M3d.Invert(axes);
        var fracPoints = ValueEnumerable.Range(0, boundsArray.Length)
            .Select(i => Geometry.GetClippedPolygon(i, boundsArray))
            .Where(c => c != null)
            .SelectMany(c => c.Select(p => invAxes * (new V3(p[0], p[1], p[2]) + shift)))
            .ToArray();

        if (fracPoints.Length == 0) return new TranslationRange(-1, 1, -1, 1, -1, 1);

        var a = ClampRange((int)Math.Floor(fracPoints.Min(p => p.X)) - 1, (int)Math.Ceiling(fracPoints.Max(p => p.X)) + 1);
        var b = ClampRange((int)Math.Floor(fracPoints.Min(p => p.Y)) - 1, (int)Math.Ceiling(fracPoints.Max(p => p.Y)) + 1);
        var c = ClampRange((int)Math.Floor(fracPoints.Min(p => p.Z)) - 1, (int)Math.Ceiling(fracPoints.Max(p => p.Z)) + 1);
        return new TranslationRange(a.Min, a.Max, b.Min, b.Max, c.Min, c.Max);

        static (int Min, int Max) ClampRange(int min, int max)
        {
            if (max - min <= MaxTranslationSpan) return (min, max);
            int center = (int)Math.Round((min + max) * 0.5);
            int half = MaxTranslationSpan / 2;
            return (center - half, center + half);
        }
    }

    /// <summary>TranslationRange 内の整数格子点 (a, b, c) を列挙する。</summary>
    private static IEnumerable<(int X, int Y, int Z)> EnumerateCells(TranslationRange range)
    {
        for (int x = range.MinA; x <= range.MaxA; x++)
            for (int y = range.MinB; y <= range.MaxB; y++)
                for (int z = range.MinC; z <= range.MaxC; z++)
                    yield return (x, y, z);
    }

    #endregion
}
