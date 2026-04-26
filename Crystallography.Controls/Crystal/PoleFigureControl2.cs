using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using V4 = OpenTK.Mathematics.Vector4d;

namespace Crystallography.Controls;

public partial class PoleFigureControl2 : UserControlBase
{
    /// <summary>Histogram: 方位ベクトルの頻度,  Average: 方位ベクトルが持つ値の平均値,  Sigma: 方位ベクトルが持つ値の標準偏差</summary>
    public enum DrawingModeEnum { Histogram, Average, Sigma }

    public DrawingModeEnum DrawingMode = DrawingModeEnum.Histogram;

    private StereonetDirection stereonetDirecion = StereonetDirection.Equrtor;
    private bool showOneDeg = false;

    private Color OneDegColor = Color.FromArgb(192, 192, 255);
    private Color TenDegColor = Color.FromArgb(128, 128, 255);
    private Color NinetyDegColor = Color.FromArgb(0, 0, 255);

    private V4[] vectors = null;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public V4[] Vectors
    {
        get => vectors;
        set
        {
            if (value == null) return;
            vectors = value;
            GeneratePixels();
            Draw();
        }
    }

    private double[][] Pixels = null;
    private double magnification = 1;
    private PointD center = new(0, 0);

    public (PointD Point, double Radius, Color Color, bool Fill, string Text)[] Circles = null;
    public (PointD[] Point, double LineWidth, Color Color)[] Lines = null;

    public PoleFigureControl2()
    {
        InitializeComponent();
        comboBoxColor.SelectedIndex = 0;
    }

    private void PoleFigureControl_Load(object sender, EventArgs e)
    {
        magnification = Math.Min(pictureBox.Width, pictureBox.Height) / 2.4;
        center = new PointD(0, 0);
    }

    public void Draw()
    {
        magnification = Math.Min(pictureBox.Width, pictureBox.Height) / 2.4;
        if (pictureBox.ClientSize.Width == 0 || pictureBox.ClientSize.Height == 0) return;

        var oldImage = pictureBox.Image;
        var bmp = new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Transform = new Matrix((float)magnification, 0, 0, (float)magnification,
                (float)(pictureBox.ClientSize.Width / 2.0 - center.X * magnification),
                (float)(pictureBox.ClientSize.Height / 2.0 - center.Y * magnification));

            g.Clear(Color.White);
            DrawDensity(g, Pixels);
            DrawOutline(g);
            DrawSymbols(g);
            DrawColorScale();
        }
        pictureBox.Image = bmp;
        oldImage?.Dispose();
    }

    private void pictureBox_DoubleClick(object sender, EventArgs e)
    {
        if (Pixels == null) return;
        const float mag = 480;
        using var bmp = new Bitmap(1024, 1024);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Transform = new Matrix(mag, 0, 0, mag, 512, 512);
            g.Clear(Color.White);
            DrawDensity(g, Pixels);
            DrawOutline(g);
            DrawSymbols(g);
        }
        Clipboard.SetDataObject(bmp);
    }

    private readonly Lock lockObj = new();

    /// <summary>Vectors[] から Pixels[][] を生成</summary>
    private void GeneratePixels()
    {
        if (vectors == null) return;

        // 指定された解像度で Pixels を生成
        var div1 = (int)(90.0 / numericBoxResolution.Value); // 放射方向の分割数
        Pixels = new double[div1][];
        var areas = new double[div1];
        for (int i = 0; i < div1; i++)
        {
            var circumference = 2 * Math.PI * (i + 0.5) / div1;
            var div2 = Math.Max(3, (int)(circumference * div1));
            Pixels[i] = new double[div2];
            areas[i] = Math.PI * (2 * i + 1) / div1 / div1 / div2;
        }

        // ベクトル → (放射方向 i, 円周方向 j) の Pixel index
        (int i, int j) ToIndex(V4 v)
        {
            var len = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            double X = v.X / len, Y = v.Y / len, Z = v.Z / len;
            // 単位ベクトルなら (X / √(1+Z), Y / √(1+Z)) がシュミットネット上の点
            var (x, y) = (X / Math.Sqrt(1 + Z), Y / Math.Sqrt(1 + Z));
            var i = Math.Clamp((int)(Math.Sqrt(x * x + y * y) * div1), 0, div1 - 1);
            var j = Math.Clamp((int)((Math.Atan2(y, x) + Math.PI) / 2 / Math.PI * Pixels[i].Length), 0, Pixels[i].Length - 1);
            return (i, j);
        }

        if (DrawingMode == DrawingModeEnum.Histogram)
        {
            vectors.AsParallel().Where(v => v.Z >= 0).ForAll(v =>
            {
                var (i, j) = ToIndex(v);
                lock (lockObj) Pixels[i][j] += 1 / areas[i];
            });
        }
        else if (DrawingMode == DrawingModeEnum.Average)
        {
            var count = new int[Pixels.Length][];
            for (int i = 0; i < count.Length; i++)
                count[i] = new int[Pixels[i].Length];

            vectors.AsParallel().Where(v => v.Z >= 0).ForAll(v =>
            {
                var (i, j) = ToIndex(v);
                lock (lockObj)
                {
                    Pixels[i][j] += v.W;
                    count[i][j]++;
                }
            });

            for (int i = 0; i < div1; i++)
                for (int j = 0; j < Pixels[i].Length; j++)
                    if (count[i][j] != 0)
                        Pixels[i][j] /= count[i][j];
        }
        else
        {
            var tmp = new List<double>[Pixels.Length][];
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = new List<double>[Pixels[i].Length];
                for (int j = 0; j < tmp[i].Length; j++)
                    tmp[i][j] = [];
            }

            vectors.AsParallel().Where(v => v.Z >= 0).ForAll(v =>
            {
                var (i, j) = ToIndex(v);
                lock (lockObj) tmp[i][j].Add(v.W);
            });

            for (int i = 0; i < div1; i++)
                for (int j = 0; j < Pixels[i].Length; j++)
                    if (tmp[i][j].Count > 1)
                        Pixels[i][j] = Statistics.Deviation([.. tmp[i][j]]);
        }

        var max = Pixels.Max(e => e.Max());
        var log10 = Math.Floor(Math.Log10(max));
        max = (int)(max / Math.Pow(10, log10 - 3) + 0.5) * Math.Pow(10, log10 - 3);

        var min = Pixels.Min(e => e.Min());
        if (min > 0)
        {
            log10 = Math.Floor(Math.Log10(min));
            min = (int)(min / Math.Pow(10, log10 - 3) - 0.5) * Math.Pow(10, log10 - 3);
        }

        numericBoxMax.Value = max;
        numericBoxMin.Value = min;
    }

    public void DrawDensity(Graphics g, double[][] pixels)
    {
        if (pixels == null) return;
        var max = pixels.Max(e => e.Max());
        var min = pixels.Min(e => e.Min());
        label1.Text = $"Max: {max:g5};   Min: {min:g5}";

        var scale = comboBoxColor.SelectedIndex == 0 ? PseudoBitmap.ColorScaleColdWarmLiner : PseudoBitmap.ColorScaleGrayLiner;
        var range = numericBoxMax.Value - numericBoxMin.Value;

        for (int i = pixels.Length - 1; i >= 0; i--)
            for (int j = 0; j < pixels[i].Length; j++)
            {
                var val = (pixels[i][j] - numericBoxMin.Value) / range;
                int density = Math.Clamp((int)(val * 65535), 0, 65535);
                using var brush = new SolidBrush(Color.FromArgb(scale[density].R, scale[density].G, scale[density].B));
                g.FillPie(brush,
                    -(i + 1.0) / pixels.Length, -(i + 1.0) / pixels.Length,
                    (i + 1.0) / pixels.Length * 2, (i + 1.0) / pixels.Length * 2,
                    -(double)(j + 1) / pixels[i].Length * 360.0,
                    1.0 / pixels[i].Length * 360.0);
            }
    }

    #region DrawOutline 輪郭を描画する

    private Pen MakeGridPen(int w) => StereonetDrawing.MakeGridPen(w, TenDegColor, OneDegColor);

    public void DrawOutline(Graphics g)
    {
        if (stereonetDirecion == StereonetDirection.Equrtor)
        {
            const int div = 1000;
            var pt1 = new PointD[div];
            var pt2 = new PointD[div];

            // 経線・緯線を 2 系統 (xz 平面 / yz 平面) で描画
            StereonetDrawing.DrawMeridians(g, div, pt1, pt2, latRange: false, showOneDeg, TenDegColor, OneDegColor);
            StereonetDrawing.DrawMeridians(g, div, pt1, pt2, latRange: true, showOneDeg, TenDegColor, OneDegColor);
        }
        else
        {
            for (int w = 1; w < 180; w++)
            {
                if (!showOneDeg && w % 10 != 0) continue;
                using var pen = MakeGridPen(w);
                double theta = w * Math.PI / 180.0;
                g.DrawLine(pen, -Math.Cos(theta), -Math.Sin(theta), Math.Cos(theta), Math.Sin(theta));
                if (w < 90)
                {
                    double radius = Math.Sin(Math.PI / 4 - theta / 2) * Math.Sqrt(2);
                    g.DrawArc(pen, -radius, -radius, 2 * radius, 2 * radius, 0, 2 * Math.PI);
                }
            }
        }
        using var ninety = new Pen(NinetyDegColor, 0.004f);
        g.DrawArc(ninety, -1, -1, 2, 2, 0, 360);
        g.DrawLine(ninety, 0f, -1f, 0f, 1f);
        g.DrawLine(ninety, -1f, 0f, 1f, 0f);
    }

    #endregion

    public void DrawSymbols(Graphics g)
    {
        if (Circles != null)
            foreach (var s in Circles.Where(s => s.Point.Length2 <= 1))
            {
                var p = new PointD(s.Point.X, s.Point.Y);
                if (s.Fill)
                    g.FillCircle(s.Color, p, s.Radius, 255);
                else
                    g.DrawCircle(s.Color, p, s.Radius);
                if (s.Text != "")
                {
                    using var font = new Font("Tahoma", 0.08f);
                    using var brush = new SolidBrush(s.Color);
                    g.DrawString(s.Text, font, brush, p.ToPointF());
                }
            }
        if (Lines != null)
            foreach (var l in Lines)
            {
                using var pen = new Pen(l.Color, (float)(l.LineWidth / magnification));
                g.DrawLines(pen, l.Point.Select(e => new PointD(e.X, e.Y)).ToArray());
            }
    }

    public void DrawColorScale()
    {
        var oldImage = pictureBox1.Image;
        var bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
        using (var gScale = Graphics.FromImage(bmp))
        {
            gScale.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            var scale = comboBoxColor.SelectedIndex == 0 ? PseudoBitmap.ColorScaleColdWarmLiner : PseudoBitmap.ColorScaleGrayLiner;
            const int scaleWidth = 280, scaleHeight = 15, leftMargin = 20, barLength = 5;

            for (int i = 0; i < scaleWidth; i++)
            {
                int dens = (int)(65536.0 * i / scaleWidth);
                using var pen = new Pen(Color.FromArgb(scale[dens].R, scale[dens].G, scale[dens].B));
                gScale.DrawLine(pen, new Point(leftMargin + i, 0), new Point(leftMargin + i, scaleHeight));
            }

            using var tickFont = new Font("Tahoma", 8f);
            for (int i = 0; i < 5; i++)
            {
                var x = i / 4f * scaleWidth;
                var val = (numericBoxMax.Value - numericBoxMin.Value) * i / 4.0 + numericBoxMin.Value;
                gScale.DrawLine(Pens.Black, new PointF(leftMargin + x, scaleHeight), new PointF(leftMargin + x, scaleHeight + barLength));
                gScale.DrawStringWithAlignment($"{val:g6}", tickFont, Color.Black,
                    new PointD(leftMargin + x, scaleHeight + barLength), new Size(100, 20),
                    HorizontalAlignment.Center, System.Windows.Forms.VisualStyles.VerticalAlignment.Top);
            }
        }
        pictureBox1.Image = bmp;
        oldImage?.Dispose();
    }

    private void numericUpDownResolution_Click(object sender, EventArgs e)
    {
        GeneratePixels();
        Draw();
    }
    private void Combobox_SelectedIndexChanged(object sender, EventArgs e) => Draw();
    private void numericBoxMax_ValueChanged(object sender, EventArgs e) => Draw();
}
