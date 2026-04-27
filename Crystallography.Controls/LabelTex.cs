using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using WpfMath.Parsers;
using WpfMath.Rendering;
using XamlMath;
using WpfColor = System.Windows.Media.Color;
using WpfDrawingVisual = System.Windows.Media.DrawingVisual;
using WpfInt32Rect = System.Windows.Int32Rect;
using WpfPen = System.Windows.Media.Pen;
using WpfPixelFormats = System.Windows.Media.PixelFormats;
using WpfRenderTargetBitmap = System.Windows.Media.Imaging.RenderTargetBitmap;
using WpfSolidColorBrush = System.Windows.Media.SolidColorBrush;
using WpfTranslateTransform = System.Windows.Media.TranslateTransform;

namespace Crystallography.Controls;

// (260427Ch) WinForms の Label と同じように Text へ LaTeX 数式を書ける軽量表示コントロール。
[DefaultProperty(nameof(Text))]
[DefaultEvent(nameof(Click))]
[ToolboxItem(true)]
public class LabelTex : Control
{
    private Bitmap renderedBitmap;
    private bool renderAttempted;
    private string renderError;
    private ContentAlignment textAlign = ContentAlignment.MiddleLeft;
    private TexStyle texStyle = TexStyle.Display;
    private double thickness;

    public LabelTex()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint
               | ControlStyles.OptimizedDoubleBuffer
               | ControlStyles.ResizeRedraw
               | ControlStyles.SupportsTransparentBackColor
               | ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.Selectable, false);

        Size = DefaultSize;
    }

    protected override Size DefaultSize => new(100, 23);

    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Bindable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public override string Text
    {
        get => base.Text;
        set => base.Text = value;
    }

    [Category("Appearance")]
    [DefaultValue(ContentAlignment.MiddleLeft)]
    [Description("レンダリングした LaTeX の配置を指定します。")]
    public ContentAlignment TextAlign
    {
        get => textAlign;
        set
        {
            if (textAlign == value) return;
            textAlign = value;
            Invalidate();
        }
    }

    [Category("Appearance")]
    [DefaultValue(TexStyle.Display)]
    [Description("LaTeX の TeX style を指定します。")]
    public TexStyle TexStyle
    {
        get => texStyle;
        set
        {
            if (texStyle == value) return;
            texStyle = value;
            ClearRenderedBitmap();
        }
    }

    [Category("Appearance")]
    [DefaultValue(0.0)]
    [Description("LaTeX 描画に重ねる縁取りの太さを device-independent pixel 単位で指定します。0 のときは通常描画です。")]
    public double Thickness
    {
        get => thickness;
        set
        {
            var normalized = Math.Max(0.0, value);
            if (thickness == normalized) return;
            thickness = normalized;
            ClearRenderedBitmap();
        }
    }

    public override bool AutoSize
    {
        get => base.AutoSize;
        set
        {
            if (base.AutoSize == value) return;
            base.AutoSize = value;
            if (value) AdjustSize();
        }
    }

    public override Size GetPreferredSize(Size proposedSize)
    {
        EnsureRenderedBitmap();
        var contentSize = renderedBitmap != null ? renderedBitmap.Size : MeasureFallbackText(proposedSize);
        return new Size(contentSize.Width + Padding.Horizontal, contentSize.Height + Padding.Vertical);
    }

    protected override void OnTextChanged(EventArgs e)
    {
        ClearRenderedBitmap();
        base.OnTextChanged(e);
    }

    protected override void OnFontChanged(EventArgs e)
    {
        ClearRenderedBitmap();
        base.OnFontChanged(e);
    }

    protected override void OnForeColorChanged(EventArgs e)
    {
        ClearRenderedBitmap();
        base.OnForeColorChanged(e);
    }

    protected override void OnPaddingChanged(EventArgs e)
    {
        ClearRenderedBitmap();
        base.OnPaddingChanged(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        EnsureRenderedBitmap();

        if (renderedBitmap != null)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.DrawImage(renderedBitmap, GetAlignedRectangle(renderedBitmap.Size));
            return;
        }

        DrawFallbackText(e.Graphics);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            renderedBitmap?.Dispose();
        base.Dispose(disposing);
    }

    private void ClearRenderedBitmap()
    {
        renderedBitmap?.Dispose();
        renderedBitmap = null;
        renderAttempted = false;
        renderError = null;

        if (AutoSize && !IsDisposed)
            AdjustSize();
        Invalidate();
    }

    private void EnsureRenderedBitmap()
    {
        if (renderAttempted) return;

        renderAttempted = true;
        renderError = null;
        var latex = NormalizeLatex(Text);
        if (latex.Length == 0)
            return;

        try
        {
            var parser = WpfTeXFormulaParser.Instance;
            var formula = parser.Parse(latex);
            var scale = Math.Max(1.0, Font.SizeInPoints * 96.0 / 72.0);
            var dpi = Math.Max(96.0, DeviceDpi);
            var brush = ToWpfBrush(ForeColor);
            var environment = WpfTeXEnvironment.Create(texStyle, scale, Font.FontFamily.Name, brush, null);
            renderedBitmap = thickness <= 0.0
                ? ToDrawingBitmap(formula.RenderToBitmap(environment, scale, 0.0, 0.0, dpi), dpi)
                : RenderGeometryBitmap(formula, environment, scale, dpi, brush, thickness); // (260427Ch) 細い数式を必要時だけ stroke で太らせる。
        }
        catch (Exception ex)
        {
            renderError = ex.Message; // (260427Ch) Designer 上で例外を表に出さず、通常テキスト描画へ退避する。
        }
    }

    private static readonly (string Open, string Close)[] LatexDelimiters = // (260427Cl) 数式区切りの定型ペア。
    [
        ("$$", "$$"),
        (@"\(", @"\)"),
        (@"\[", @"\]"),
        ("$", "$"),
    ];

    private static string NormalizeLatex(string text)
    {
        var latex = (text ?? string.Empty).Trim();
        foreach (var (open, close) in LatexDelimiters)
            if (latex.Length >= open.Length + close.Length && latex.StartsWith(open) && latex.EndsWith(close))
                return latex[open.Length..^close.Length].Trim();
        return latex;
    }

    private static WpfSolidColorBrush ToWpfBrush(Color color)
    {
        var brush = new WpfSolidColorBrush(WpfColor.FromArgb(color.A, color.R, color.G, color.B));
        if (brush.CanFreeze)
            brush.Freeze();
        return brush;
    }

    private static Bitmap RenderGeometryBitmap(TexFormula formula, TexEnvironment environment, double scale, double dpi, WpfSolidColorBrush brush, double thickness)
    {
        var geometry = formula.RenderToGeometry(environment, scale, 0.0, 0.0);
        var bounds = geometry.Bounds;
        if (bounds.IsEmpty)
            return new Bitmap(1, 1);

        var margin = Math.Max(1.0, Math.Ceiling(thickness));
        var widthDip = Math.Max(1.0, bounds.Width + margin * 2.0);
        var heightDip = Math.Max(1.0, bounds.Height + margin * 2.0);
        var pixelWidth = Math.Max(1, (int)Math.Ceiling(widthDip * dpi / 96.0));
        var pixelHeight = Math.Max(1, (int)Math.Ceiling(heightDip * dpi / 96.0));

        var pen = new WpfPen(brush, thickness);
        if (pen.CanFreeze)
            pen.Freeze();

        var visual = new WpfDrawingVisual();
        using (var context = visual.RenderOpen())
        {
            context.PushTransform(new WpfTranslateTransform(-bounds.X + margin, -bounds.Y + margin));
            context.DrawGeometry(brush, pen, geometry);
            context.Pop();
        }

        var bitmapSource = new WpfRenderTargetBitmap(pixelWidth, pixelHeight, dpi, dpi, WpfPixelFormats.Pbgra32);
        bitmapSource.Render(visual);
        return ToDrawingBitmap(bitmapSource, dpi);
    }

    // (260427Cl) PNG エンコード/デコード往復を避け、CopyPixels で直接 GDI ビットマップへ転写する。
    private static Bitmap ToDrawingBitmap(BitmapSource bitmapSource, double dpi)
    {
        var source = bitmapSource.Format == WpfPixelFormats.Pbgra32
            ? bitmapSource
            : new FormatConvertedBitmap(bitmapSource, WpfPixelFormats.Pbgra32, null, 0);

        var bitmap = new Bitmap(source.PixelWidth, source.PixelHeight, PixelFormat.Format32bppPArgb);
        var data = bitmap.LockBits(
            new Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.WriteOnly, bitmap.PixelFormat);
        try
        {
            source.CopyPixels(WpfInt32Rect.Empty, data.Scan0, data.Stride * data.Height, data.Stride);
        }
        finally
        {
            bitmap.UnlockBits(data);
        }
        bitmap.SetResolution((float)dpi, (float)dpi);
        return bitmap;
    }

    private Size MeasureFallbackText(Size proposedSize)
    {
        if (string.IsNullOrEmpty(Text))
            return Size.Empty;

        var bounds = proposedSize.Width > 0 && proposedSize.Height > 0 ? proposedSize : Size.Empty;
        return TextRenderer.MeasureText(Text, Font, bounds, GetTextFormatFlags());
    }

    private void DrawFallbackText(Graphics graphics)
    {
        if (string.IsNullOrEmpty(Text)) return;
        var color = renderError == null ? ForeColor : Color.Firebrick;
        TextRenderer.DrawText(graphics, Text, Font, DisplayRectangle, color, GetTextFormatFlags());
    }

    private Rectangle GetAlignedRectangle(Size contentSize)
    {
        var bounds = new Rectangle(
            Padding.Left,
            Padding.Top,
            Math.Max(0, Width - Padding.Horizontal),
            Math.Max(0, Height - Padding.Vertical));

        var x = textAlign switch
        {
            ContentAlignment.TopCenter or ContentAlignment.MiddleCenter or ContentAlignment.BottomCenter => bounds.Left + (bounds.Width - contentSize.Width) / 2,
            ContentAlignment.TopRight or ContentAlignment.MiddleRight or ContentAlignment.BottomRight => bounds.Right - contentSize.Width,
            _ => bounds.Left,
        };
        var y = textAlign switch
        {
            ContentAlignment.MiddleLeft or ContentAlignment.MiddleCenter or ContentAlignment.MiddleRight => bounds.Top + (bounds.Height - contentSize.Height) / 2,
            ContentAlignment.BottomLeft or ContentAlignment.BottomCenter or ContentAlignment.BottomRight => bounds.Bottom - contentSize.Height,
            _ => bounds.Top,
        };

        return new Rectangle(x, y, contentSize.Width, contentSize.Height);
    }

    private TextFormatFlags GetTextFormatFlags()
    {
        var flags = TextFormatFlags.NoPadding | TextFormatFlags.SingleLine;
        flags |= textAlign switch
        {
            ContentAlignment.TopCenter or ContentAlignment.MiddleCenter or ContentAlignment.BottomCenter => TextFormatFlags.HorizontalCenter,
            ContentAlignment.TopRight or ContentAlignment.MiddleRight or ContentAlignment.BottomRight => TextFormatFlags.Right,
            _ => TextFormatFlags.Left,
        };
        flags |= textAlign switch
        {
            ContentAlignment.MiddleLeft or ContentAlignment.MiddleCenter or ContentAlignment.MiddleRight => TextFormatFlags.VerticalCenter,
            ContentAlignment.BottomLeft or ContentAlignment.BottomCenter or ContentAlignment.BottomRight => TextFormatFlags.Bottom,
            _ => TextFormatFlags.Top,
        };
        if (RightToLeft == RightToLeft.Yes)
            flags |= TextFormatFlags.RightToLeft;
        return flags;
    }

    private void AdjustSize()
    {
        Size = GetPreferredSize(Size.Empty);
    }
}
