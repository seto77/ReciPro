// 260501Cl: 空間群の対称要素 (左図) と一般位置 (右図) を ITC Vol.A 風に GDI+ 描画する基底クラス。
// 派生 SymmetryDiagramElements / SymmetryDiagramPositions が共通利用するヘルパーを保持する。
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Crystallography.Controls;

/// <summary>描画時の投影軸 (depth 方向となる結晶軸)。
/// 投影軸の切替えはユーザー側で選べるのは <b>直方晶系のみ</b>。
/// 単斜は unique 軸 (2 回回転 / m 面の軸)、三斜・正方・三方・六方・立方は c 軸固定。</summary>
public enum ProjectionAxis { A, B, C }

/// <summary>Diagram 描画の共通関数・プロパティを保持する基底クラス。
/// 投影、セルジオメトリ、補助線、test 点、定数、ユーティリティを提供。</summary>
public abstract class SymmetryDiagramCommon
{
    #region 定数
    /// <summary>(260502Cl) 単位胞外側の余白 (pixel)。上下左右独立。外部から書き換え可能。</summary>
    public static float CellMarginLeft = 80f, CellMarginRight = 60f, CellMarginTop = 80f, CellMarginBottom = 60f;
    /// <summary>(260505Cl) 立方晶系のみ全方向余白に追加する加算値 (pixel)。in-plane 4 回軸の平行四辺形などがセル外側に張り出すため。</summary>
    public static float ExtraMarginForCubic = 35f;
    protected const double EdgeReplicate = 0.05, FracEps = 0.01;
    // (260502Cl) CircleRadiusFraction / CircleRadius は SymmetryDiagramPositions 内でしか使われないため当該クラスへ移動。

    /// <summary>(260505Cl 整理) <see cref="NewBitmap"/> の出力 bitmap 最小サイズ (pixel)。clientSize がこれ未満でもこのサイズでバッファを確保する。</summary>
    private const int MinBitmapSize = 16;
    /// <summary>(260505Cl 整理) <see cref="DrawUpperLeftQuadrantLabel"/> の左上ラベルのオフセットとフォント。</summary>
    private const float QuadrantLabelOffset = 4f;
    private static readonly Font QuadrantLabelFont = new("Segoe UI", 8f);
    /// <summary>(260505Ch) セル軸ラベル ("o", a, b, c) とセル枠の隙間 (pixel)。</summary>
    private const float AxisLabelGap = 2f;

    //----------------------------------------------------------------------
    // 単位胞 (DrawCellAndAxes) の枠線・補助線
    //----------------------------------------------------------------------
    /// <summary>(260502Cl) 単位胞の四辺輪郭 (実線) の色。両図 (対称要素図 / 一般位置図) で共通。</summary>
    public static Color CellOutlineColor = Color.SkyBlue;
    /// <summary>(260502Cl) 単位胞の四辺輪郭の線幅 (pixel)。</summary>
    public static float CellOutlinePenWidth = 1f;
    /// <summary>(260502Cl) セル内部の補助線 (半セル分割線、または三方/六方の対角線) の色。</summary>
    public static Color CellGuideLineColor = Color.LightBlue;
    /// <summary>(260502Cl) セル内部の補助線の線幅 (pixel)。破線スタイルで描画。</summary>
    public static float CellGuideLinePenWidth = 0.7f;

    //----------------------------------------------------------------------
    // 図中フォント (260502Cl) — 全ての描画箇所で共有して使う長寿命インスタンス。
    //----------------------------------------------------------------------
    /// <summary>高さラベル (¼, ½ などの分数文字)。inversion 中心、紙面内軸矢印、紙面平行 mirror 等で使用。</summary>
    protected static readonly Font HeightLabelFont = new("Times New Roman",  13f);
    /// <summary>セル枠の 軸ラベル (a, b, c の文字)。</summary>
    protected static readonly Font AxisLabelFont = new("Times New Roman", 13f, FontStyle.Italic);

    /// <summary>一般位置図のクラスタラベル (proper/improper の添字など)。</summary>
    protected static readonly Font ClusterLabelFont = new("Times New Roman",  13f);
    /// <summary>空図時のエラーメッセージ表示用。</summary>
    protected static readonly Font ErrorMessageFont = new("Segoe UI", 9f);

    /// <summary>高さラベル用の典型分数 (8 分は I4_1/acd 等で必要)。
    /// (260502Cl) 12 分系は Unicode に precomposed 一文字版が無いため、superscript + fraction slash (U+2044) + subscript の合成で擬似的に一文字化。</summary>
    protected static readonly (double V, string S)[] FracTable =
        [(.5, "½"), (1.0/3, "⅓"), (2.0/3, "⅔"), (.25, "¼"), (.75, "¾"),
         (1.0/6, "⅙"), (5.0/6, "⅚"), (1.0/8, "⅛"), (3.0/8, "⅜"), (5.0/8, "⅝"), (7.0/8, "⅞"),
         (1.0/12, "¹⁄₁₂"), (5.0/12, "⁵⁄₁₂"), (7.0/12, "⁷⁄₁₂"), (11.0/12, "¹¹⁄₁₂")];

    private sealed class TightTextGlyph
    {
        public TightTextGlyph(GraphicsPath path, RectangleF bounds)
        {
            Path = path;
            Bounds = bounds;
            Size = new SizeF(bounds.Width, bounds.Height);
        }

        public GraphicsPath Path { get; }
        public RectangleF Bounds { get; }
        public SizeF Size { get; }
    }

    // 260510Cl: 描画スレッドからの並列読み出しを serialize しないよう ConcurrentDictionary に変更。
    private static readonly ConcurrentDictionary<(string Text, string Family, FontStyle Style, float EmSize), TightTextGlyph> TightTextCache = BuildInitialTightTextCache();
    #endregion

    #region tight text
    /// <summary>GDI+ MeasureString が含む左右上下の余白を避け、glyph outline の正味 bounds を返す。260510Ch</summary>
    protected static SizeF MeasureTightString(Graphics g, string text, Font font)
        => GetTightTextGlyph(text, font).Size;

    /// <summary>glyph outline の正味左上が (x,y) になるように描画する。260510Ch</summary>
    protected static void DrawTightString(Graphics g, Brush fill, string text, Font font, float x, float y)
    {
        // 260510Cl: cached path + g.TranslateTransform で glyph clone と Matrix 確保を避ける。
        var glyph = GetTightTextGlyph(text, font);
        var state = g.Save();
        try
        {
            g.TranslateTransform(x - glyph.Bounds.Left, y - glyph.Bounds.Top);
            g.FillPath(fill, glyph.Path);
        }
        finally { g.Restore(state); }
    }

    private static ConcurrentDictionary<(string Text, string Family, FontStyle Style, float EmSize), TightTextGlyph> BuildInitialTightTextCache()
    {
        var dict = new ConcurrentDictionary<(string, string, FontStyle, float), TightTextGlyph>();
        foreach (var text in FracTable.Select(f => f.S).Distinct())
            dict[TightTextKey(text, HeightLabelFont)] = CreateTightTextGlyph(text, HeightLabelFont);
        return dict;
    }

    private static TightTextGlyph GetTightTextGlyph(string text, Font font)
        => TightTextCache.GetOrAdd(TightTextKey(text, font), _ => CreateTightTextGlyph(text, font));

    private static (string Text, string Family, FontStyle Style, float EmSize) TightTextKey(string text, Font font)
        => (text, font.FontFamily.Name, font.Style, FontEmSizePixels(font));

    private static TightTextGlyph CreateTightTextGlyph(string text, Font font)
    {
        var path = new GraphicsPath();
        path.AddString(text, font.FontFamily, (int)font.Style, FontEmSizePixels(font), PointF.Empty, StringFormat.GenericTypographic);
        return new TightTextGlyph(path, path.GetBounds());
    }

    private static float FontEmSizePixels(Font font)
        => font.Unit switch
        {
            GraphicsUnit.Pixel => font.Size,
            GraphicsUnit.Point => font.SizeInPoints * 96f / 72f,
            GraphicsUnit.Inch => font.Size * 96f,
            GraphicsUnit.Millimeter => font.Size * 96f / 25.4f,
            GraphicsUnit.Document => font.Size * 96f / 300f,
            _ => font.SizeInPoints * 96f / 72f,
        };
    #endregion

    #region 投影
    /// <summary>結晶座標 (x,y,z) → 投影面 (Sx, Sy, depth Sz)。ITC 慣用 (C 投影で Horz=b 右, Vert=a 下, depth=c)。A/B も cyclic permutation。</summary>
    protected readonly record struct Projection(ProjectionAxis Axis, string HorzLabel, string VertLabel)
    {
        public (double Sx, double Sy, double Sz) ToScreen(double x, double y, double z) => Axis switch
        {
            ProjectionAxis.C => (y, x, z),
            ProjectionAxis.A => (z, y, x),
            ProjectionAxis.B => (x, z, y),
            _ => default,
        };
    }

    protected static Projection GetProjection(ProjectionAxis axis) => axis switch
    {
        ProjectionAxis.C => new(axis, "b", "a"),
        ProjectionAxis.A => new(axis, "c", "b"),
        ProjectionAxis.B => new(axis, "a", "c"),
        _ => throw new ArgumentOutOfRangeException(nameof(axis)),
    };

    /// <summary>結晶系制約で投影軸を正規化。直方=ユーザー指定、単斜=unique 軸、それ以外=c。
    /// (260506Cl) public 化: FormSymmetryInformation の radioButtonDirection* 既定選択用。</summary>
    public static ProjectionAxis ResolveProjectionAxis(Symmetry sym, ProjectionAxis preferred) => sym.CrystalSystemNumber switch
    {
        3 => preferred,
        2 => sym.MainAxis is { Length: > 0 } s ? s[0] switch { 'a' => ProjectionAxis.A, 'c' => ProjectionAxis.C, _ => ProjectionAxis.B }
                                              : ProjectionAxis.B,
        _ => ProjectionAxis.C,
    };

    /// <summary>結晶座標 (u,v,w) を投影面 (Sx, Sy) に写像 (C: Sx=v, Sy=u 等)。</summary>
    protected static (double Sx, double Sy) ProjectVector(double u, double v, double w, ProjectionAxis axis) => axis switch
    {
        ProjectionAxis.C => (v, u),
        ProjectionAxis.A => (w, v),
        ProjectionAxis.B => (u, w),
        _ => (0, 0),
    };

    /// <summary>(260503Ch) 投影 depth 成分だけを返す。</summary>
    protected static double ProjectedDepth(double x, double y, double z, ProjectionAxis axis) => axis switch
    {
        ProjectionAxis.C => z,
        ProjectionAxis.A => x,
        ProjectionAxis.B => y,
        _ => 0,
    };
    #endregion

    #region セルのジオメトリ + 輪郭描画
    /// <summary>セル (平行四辺形) の screen 座標。</summary>
    protected readonly record struct CellLayout(PointF TopLeft, PointF Horz, PointF Vert)
    {
        public PointF ToScreen(double sx, double sy) => new(
            TopLeft.X + (float)sx * Horz.X + (float)sy * Vert.X,
            TopLeft.Y + (float)sx * Horz.Y + (float)sy * Vert.Y);
    }

    /// <summary>Horz–Vert 軸間角度 (度): 三斜/単斜=105°(β)、三方/六方=120°、それ以外=90°。</summary>
    protected static double GetCellAngleDeg(Symmetry sym) => sym.CrystalSystemNumber switch
    {
        1 or 2 => 105.0, 5 or 6 => 120.0, _ => 90.0,
    };

    /// <summary>(Horz, Vert) の格子定数比。代表値: ortho 1:1.1:1.2、tet c=1.3、trig/hex c=1.4。</summary>
    protected static (double HorzLen, double VertLen) GetCellLengths(Symmetry sym, ProjectionAxis projAxis)
    {
        double a = 0.9, b = 0.9, c = 0.9;
        switch (sym.CrystalSystemNumber)
        {
            case 3: b = 1.0; c = 1.1; break;//ortho
            case 4: c = 1.0; break;//tetra
            case 5: case 6: c = 1.4; break;//trigonal, hexagonal
        }
        return projAxis switch
        {
            ProjectionAxis.C => (b, a), ProjectionAxis.A => (c, b), ProjectionAxis.B => (a, c), _ => (1.0, 1.0),
        };
    }

    /// <summary>(260505Cl) 立方晶 F 格子の場合の判定。F-cubic では対称要素図/一般位置図ともに upper-left 1/4 領域だけを描く。</summary>
    protected static bool IsCubicFLattice(Symmetry sym) => sym.CrystalSystemNumber == 7 && sym.LatticeTypeStr == "F";

    /// <summary>(260506Cl) m-3m / -43m の判定。stereonet inset を描く立方晶高対称群かどうか。</summary>
    protected static bool IsCubicHighSym(Symmetry sym) => sym.PointGroupHMStr is "m-3m" or "-43m";

    /// <summary>(260505Cl) <paramref name="halfQuadrant"/> = true で、(0,0)〜(1/2,1/2) の領域 (upper-left 1/4) を通常セルと同じ表示位置・大きさに描く。
    /// scale を 2× にすることで 1/4 領域の外枠が通常セルの外枠位置に一致する。</summary>
    protected static CellLayout ComputeCellLayout(Size canvas, Symmetry sym, ProjectionAxis projAxis, bool halfQuadrant = false)
    {
        double rad = GetCellAngleDeg(sym) * Math.PI / 180.0;
        double cosA = Math.Cos(rad), sinA = Math.Sin(rad);
        var (hLen, vLen) = GetCellLengths(sym, projAxis);
        // (260502Cl) 上下左右で余白を独立に取る。
        // (260503Cl) 立方晶系では in-plane 4 回軸の平行四辺形などがセル外側に張り出すため、上下左右の余白を ExtraMarginForCubic 加算。
        float extra = sym.CrystalSystemNumber == 7 ? ExtraMarginForCubic : 0f;
        float ml = CellMarginLeft   + extra;
        float mr = CellMarginRight  + extra;
        float mt = CellMarginTop    + extra;
        float mb = CellMarginBottom + extra;
        float availW = Math.Max(8f, canvas.Width  - ml - mr);
        float availH = Math.Max(8f, canvas.Height - mt - mb);
        double scale = Math.Min(availW / (hLen + Math.Abs(cosA) * vLen), availH / (sinA * vLen));
        if (halfQuadrant) scale *= 2; // 260505Cl: 1/4 領域だけを canvas 一杯に出すため scale を 2× する。
        float horzLen = (float)(hLen * scale), vertLen = (float)(vLen * scale);
        float bboxW = (float)((hLen + Math.Abs(cosA) * vLen) * scale);
        float bboxH = (float)(sinA * vLen * scale);
        float ox, oy;
        if (halfQuadrant)
        {
            // 260505Ch: 表示される 1/4 領域 (= full-cell bbox の半分) を通常セルと同じ位置へセンタリングする。
            ox = ml + (availW - bboxW * 0.5f) / 2f + (cosA < 0 ? -(float)cosA * vertLen * 0.5f : 0);
            oy = mt + (availH - bboxH * 0.5f) / 2f;
        }
        else
        {
            ox = ml + (availW - bboxW) / 2f + (cosA < 0 ? -(float)cosA * vertLen : 0);
            oy = mt + (availH - bboxH) / 2f;
        }
        return new(new PointF(ox, oy), new PointF(horzLen, 0f), new PointF((float)cosA * vertLen, (float)sinA * vertLen));
    }

    /// <summary>(260505Cl) "Upper left quadrant only" のラベルを canvas 左上に小さく描く。</summary>
    protected static void DrawUpperLeftQuadrantLabel(Graphics g)
    {
        using var brush = new SolidBrush(Color.Gray);
        g.DrawString("Upper left quadrant only", QuadrantLabelFont, brush, QuadrantLabelOffset, QuadrantLabelOffset);
    }

    // 旧: F 格子の後段 clip 処理は廃止。(260505Ch) 各描画ロジックへ maxS=0.5 を渡して最初から 1/4 領域だけを生成する。

    /// <summary>セルの輪郭、補助線 (三方/六方は対角、それ以外は半セル分割線)、軸ラベル ("o", Horz, Vert) を描画。
    /// (260505Cl) showAxisLabels=false で軸ラベル ("o", a, b 等) を描画しない (対称要素図用)。</summary>
    protected static void DrawCellAndAxes(Graphics g, CellLayout c, Projection proj, Symmetry sym, bool halfQuadrant = false, bool showAxisLabels = true)
    {
        double maxS = halfQuadrant ? 0.5 : 1.0; // (260505Ch) F 格子では upper-left 1/4 の輪郭だけを単位胞枠として表示する。
        using (var d = new Pen(CellGuideLineColor, CellGuideLinePenWidth) { DashStyle = DashStyle.Dash }) // (260502Cl)
        {
            if (halfQuadrant) // (260505Ch) F 格子の表示 1/4 領域内に 1/4 補助線を入れる。
            {
                g.DrawLine(d, c.ToScreen(0.25, 0), c.ToScreen(0.25, 0.5));
                g.DrawLine(d, c.ToScreen(0, 0.25), c.ToScreen(0.5, 0.25));
            }
            else if (sym.CrystalSystemNumber is 5 or 6) g.DrawLine(d, c.TopLeft, c.ToScreen(1, 1));
            else { g.DrawLine(d, c.ToScreen(0.5, 0), c.ToScreen(0.5, 1)); g.DrawLine(d, c.ToScreen(0, 0.5), c.ToScreen(1, 0.5)); }
        }
        var tr = c.ToScreen(maxS, 0); var bl = c.ToScreen(0, maxS); var br = c.ToScreen(maxS, maxS);
        using (var pen = new Pen(CellOutlineColor, CellOutlinePenWidth)) g.DrawPolygon(pen, [c.TopLeft, tr, br, bl]); // (260502Cl)

        // (260502Cl) フォントは Common 冒頭の AxisLabelFont / OriginLabelFont を共有使用。
        // (260505Cl) showAxisLabels=false で軸ラベルをスキップ。対称要素図ではラベルを出さず、一般位置図側だけで表示する。
        if (!showAxisLabels) return;
        using var brush = new SolidBrush(Color.Black);
        // 260510Cl: 高さラベルと同じ tight glyph bounds を使い、AxisLabelGap が GDI+ の余白で食われないようにする。
        var oSz = MeasureTightString(g, "o", AxisLabelFont);
        var hSz = MeasureTightString(g, proj.HorzLabel, AxisLabelFont);
        var vSz = MeasureTightString(g, proj.VertLabel, AxisLabelFont);
        DrawTightString(g, brush, "o", AxisLabelFont, c.TopLeft.X - oSz.Width - AxisLabelGap, c.TopLeft.Y - oSz.Height - AxisLabelGap);
        DrawTightString(g, brush, proj.HorzLabel, AxisLabelFont, tr.X + AxisLabelGap, tr.Y - hSz.Height - AxisLabelGap);
        DrawTightString(g, brush, proj.VertLabel, AxisLabelFont, bl.X - vSz.Width - AxisLabelGap, bl.Y + AxisLabelGap);
    }
    #endregion

    #region 共通ユーティリティ
    protected static double Mod1(double x) => x - Math.Floor(x);

    /// <summary>(260503Ch) 単位胞境界上の点を、表示セルと隣接セルの両方へ複製するための代表列。
    /// 内部点は 1 点だけ、境界近傍点は 3×3 のうち描画範囲に入るコピーだけを返す。</summary>
    protected static IEnumerable<(double Sx, double Sy)> EdgeReplicatedPoints(double sx, double sy, double maxS = 1.0)
    {
        bool nearEdge = Math.Min(sx, 1 - sx) < EdgeReplicate || Math.Min(sy, 1 - sy) < EdgeReplicate;
        for (int dx = -1; dx <= 1; dx++) for (int dy = -1; dy <= 1; dy++)
        {
            if ((dx != 0 || dy != 0) && !nearEdge) continue;
            double x = sx + dx, y = sy + dy;
            if (maxS < 1.0 - 1e-9 && (x < -1e-6 || x > maxS + 1e-6 || y < -1e-6 || y > maxS + 1e-6))
                continue; // (260505Ch) F 格子では描画対象自体を upper-left 1/4 に限定する。
            if (x >= -EdgeReplicate && x <= 1 + EdgeReplicate && y >= -EdgeReplicate && y <= 1 + EdgeReplicate)
                yield return (x, y);
        }
    }

    /// <summary>0 < frac < 1 の典型分数ラベル。0 近傍は null。</summary>
    protected static string HeightLabel(double sz)
    {
        double m = Mod1(sz);
        return m < FracEps || m > 1 - FracEps ? null : TZToFraction(m);
    }

    protected static string TZToFraction(double t)
    {
        if (t < FracEps || t > 1 - FracEps) return "";
        foreach (var (v, s) in FracTable) if (Math.Abs(t - v) < FracEps) return s;
        return $"{t:0.00}";
    }

    /// <summary>(260504Ch) (sx,sy) を通り (dSx,dSy) 方向の直線をセル [0,maxS]² の媒介変数区間でクリップ。
    /// 旧実装はセル外で辺と平行な直線を tMin=1 にするだけで、tMax が無限大のままなら有効扱いになることがあった。</summary>
    protected static bool TryClipLineThroughUnitCell(double sx, double sy, double dSx, double dSy,
                                                     out double tMin, out double tMax, double maxS = 1.0)
    {
        tMin = double.NegativeInfinity;
        tMax = double.PositiveInfinity;
        if (Math.Abs(dSx) < 1e-12 && Math.Abs(dSy) < 1e-12) return false; // (260504Ch) 退化方向では ±Infinity*0 に落とさない。
        return ClipCoordinate(sx, dSx, ref tMin, ref tMax, maxS)
            && ClipCoordinate(sy, dSy, ref tMin, ref tMax, maxS)
            && tMin <= tMax + 1e-12;

        static bool ClipCoordinate(double s, double d, ref double tMin, ref double tMax, double maxS)
        {
            const double eps = 1e-9;
            if (Math.Abs(d) < eps)
                return s >= -eps && s <= maxS + eps;
            double t1 = -s / d, t2 = (maxS - s) / d;
            if (t1 > t2) (t1, t2) = (t2, t1);
            if (t1 > tMin) tMin = t1;
            if (t2 < tMax) tMax = t2;
            return true;
        }
    }

    /// <summary>(sx,sy) を通り (dSx,dSy) 方向の直線をセル [0,maxS]² でクリップ。</summary>
    protected static (PointF? Start, PointF? End) SpanLineThroughCell(CellLayout c, double sx, double sy, double dSx, double dSy,
                                                                      double maxS = 1.0)
    {
        if (!TryClipLineThroughUnitCell(sx, sy, dSx, dSy, out double tMin, out double tMax, maxS))
            return (null, null);
        return (c.ToScreen(sx + tMin * dSx, sy + tMin * dSy), c.ToScreen(sx + tMax * dSx, sy + tMax * dSy));
    }

    protected static Bitmap NewBitmap(Size size, out Graphics g)
    {
        var bmp = new Bitmap(Math.Max(size.Width, MinBitmapSize), Math.Max(size.Height, MinBitmapSize));
        g = Graphics.FromImage(bmp);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        g.Clear(Color.White);
        return bmp;
    }

    /// <summary>tri/mono/ortho/tet/trig/hex/cubic (1-7) に対応。
    /// (260502Cl) trigonal Rho 設定は同一空間群の Hex 設定にリダイレクトして同じ図を描く。
    /// (260502Cl 追加) 立方晶系 (7) を許可。当面は P23 など低対称な空間群から実装中。</summary>
    protected static bool TryGetSym(int seriesNumber, out Symmetry sym, out int resolvedSeriesNumber, out string msg)
    {
        sym = default; msg = null; resolvedSeriesNumber = seriesNumber;
        if (seriesNumber <= 0 || seriesNumber >= SymmetryStatic.TotalSpaceGroupNumber) return false;
        resolvedSeriesNumber = SymmetryElementsTable.ResolveRhoToHex(seriesNumber); // (260504Ch) Rho→Hex 解決を対称要素テーブルと共有。
        sym = SymmetryStatic.Symmetries[resolvedSeriesNumber];
        if (sym.CrystalSystemNumber is not (1 or 2 or 3 or 4 or 5 or 6 or 7))
        {
            msg = $"({sym.CrystalSystemStr} not yet supported)";
            return false;
        }
        return true;
    }

    protected static void DrawCenteredText(Graphics g, Size size, string text, Color color)
    {
        // (260502Cl) ErrorMessageFont を共有使用。
        using var brush = new SolidBrush(color);
        var sz = g.MeasureString(text, ErrorMessageFont);
        g.DrawString(text, ErrorMessageFont, brush, (size.Width - sz.Width) / 2, (size.Height - sz.Height) / 2);
    }
    #endregion
}
