using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using V3 = OpenTK.Mathematics.Vector3d;
using V4 = OpenTK.Mathematics.Vector4d;

namespace Crystallography.Controls;

public partial class PoleFigureControl2 : UserControl
{
    /// <summary>
    /// Histogram:方位ベクトルの頻度,  Average: 方位ベクトルが持つ値の平均値,  Sigma: 方位ベクトルが持つ値の標準偏差 
    /// </summary>
    public enum DrawingModeEnum { Histogram, Average, Sigma }

    public DrawingModeEnum DrawingMode = DrawingModeEnum.Histogram;

    private StereonetProjectionMode stereonetProjectionMode = StereonetProjectionMode.Schmidt;
    private StereonetDirection stereonetDirecion = StereonetDirection.Equrtor;
    private bool showOneDeg = false;
    private bool showTenDeg = true;

    private Color backGroundColor = Color.White;
    private Color OneDegColor = Color.FromArgb(192, 192, 255);
    private Color TenDegColor = Color.FromArgb(128, 128, 255);
    private Color NinetyDegColor = Color.FromArgb(0, 0, 255);

    private V4[] vectors = null;
    /// <summary>
    /// 
    /// </summary>
    public V4[] Vectors
    {
        get => vectors;
        set
        {
            if (value != null)
            {
                vectors = value;
                GeneratePixels();
                Draw();
            }
        }
    }

    private double[][] Pixels = null;

    private double magnification = 1;
    private PointD center = new(0, 0);

    //記号や線など
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
        if (pictureBox.ClientSize.Width != 0 && pictureBox.ClientSize.Height != 0)
        {
            Bitmap bmp = new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Transform = new System.Drawing.Drawing2D.Matrix((float)magnification, 0, 0, (float)magnification,
                (float)(pictureBox.ClientSize.Width / 2.0 - center.X * magnification), (float)(pictureBox.ClientSize.Height / 2.0 - center.Y * magnification));

            g.Clear(Color.White);
            DrawDensity(g, Pixels);
            DrawOutline(g);
            DrawSymbols(g);
            DrawColorScale();
            pictureBox.Image = bmp;
        }
    }

    private void pictureBox_DoubleClick(object sender, EventArgs e)
    {
        if (Pixels == null) return;
        float mag = 480;
        Bitmap bmp = new(1024, 1024);
        Graphics g = Graphics.FromImage(bmp);
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        g.Transform = new System.Drawing.Drawing2D.Matrix(mag, 0, 0, mag, 512, 512);
        g.Clear(Color.White);
        DrawDensity(g, Pixels);
        DrawOutline(g);
        DrawSymbols(g);
        Clipboard.SetDataObject(bmp);
    }

    /// <summary>
    /// Vectors[]から、Pixels[][]を生成
    /// </summary>
    private void GeneratePixels()
    {
        if (vectors == null) return;

        //まず、指定された解像度でpixelsを生成
        var div1 = (int)(90.0 / numericBoxResolution.Value);//放射方向の分割数
        Pixels = new double[div1][];
        var areas = new double[div1];
        for (int i = 0; i < div1; i++)
        {
            var circumference_length = 2 * Math.PI * (i + 0.5) / div1;//Pixels[i]の円周の長さ (外側と内側の平均)
            var div2 = Math.Max(3, (int)(circumference_length * div1));
            Pixels[i] = new double[div2];
            areas[i] = Math.PI * (2 * i + 1) / div1 / div1 / div2;
        }

        //ベクトルからPixelのインデックスを計算するファンクション
        //iが放射方向、jが円周に沿った方向
        var f = new Func<V3, (int i, int j)>(v =>
        {
            v.Normalize();
            // vが単位ベクトルの時、 (v.X / Sqrt(1 + v.Z), v.Y / Sqrt(1 + v.Z) )が シュミットネット上の点
            var (x, y) = (v.X / Math.Sqrt(1 + v.Z), v.Y / Math.Sqrt(1 + v.Z));
            var i = (int)(Math.Sqrt(x * x + y * y) * div1);
            i = Math.Min(Math.Max(i, 0), div1 - 1);
            var j = (int)((Math.Atan2(y, x) + Math.PI) / 2 / Math.PI * Pixels[i].Length);
            j = Math.Min(Math.Max(j, 0), Pixels[i].Length - 1);
            return (i, j);
        });

        if (DrawingMode == DrawingModeEnum.Histogram)
        {
            foreach (var v in vectors.Select(v => new V3(v.X, v.Y, v.Z)).Where(v => v.Z >= 0))
            {
                var (i, j) = f(v);
                Pixels[i][j] += 1 / areas[i];
            }
        }
        else if (DrawingMode == DrawingModeEnum.Average)
        {
            var count = new int[Pixels.Length][];
            for (int i = 0; i < count.Length; i++)
                count[i] = new int[Pixels[i].Length];

            foreach (var v in vectors.Where(v => v.Z >= 0))
            {
                var (i, j) = f(new(v.X, v.Y, v.Z));
                Pixels[i][j] += v.W;
                count[i][j]++;
            }
            for (int i = 0; i < div1; i++)
                for (int j = 0; j < Pixels[i].Length; j++)
                    if (count[i][j]!=0)
                    Pixels[i][j] /= count[i][j];
        }
        else
        {
           var tmp = new List<double> [Pixels.Length][];
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = new List<double>[Pixels[i].Length];
                for (int j = 0; j < tmp[i].Length; j++)
                    tmp[i][j] = [];
            }

            foreach (var v in vectors.Where(v => v.Z >= 0))
            {
                var (i, j) = f(new(v.X, v.Y, v.Z));
                tmp[i][j].Add(v.W);
            }
            for (int i = 0; i < div1; i++)
                for (int j = 0; j < Pixels[i].Length; j++)
                    if (tmp[i][j].Count > 1)
                        Pixels[i][j] = Statistics.Deviation([.. tmp[i][j]]);
        }

        var max = Pixels.Max(e => e.Max());
        var log10 = Math.Floor(Math.Log10(max));
        max = (int)(max / Math.Pow(10, log10 - 3) + 0.5) * Math.Pow(10, log10 - 3);
        numericBoxMax.Value = max;

        var min = Pixels.Min(e => e.Min());
        if(min>0)
        {
            log10 = Math.Floor(Math.Log10(min));
            min = (int)(min / Math.Pow(10, log10 - 3) - 0.5) * Math.Pow(10, log10 - 3);
        }
        numericBoxMin.Value = min;


    }

    public void DrawDensity(Graphics g, double[][] pixels)
    {
        if (pixels == null) return;
        var max = pixels.Max(e => e.Max());
        var min = pixels.Min(e => e.Min());
        label1.Text = $"Max: {max:g5};   Min: {min:g5}";

        var scale = comboBoxColor.SelectedIndex == 0 ? PseudoBitmap.ColorScaleColdWarmLiner : PseudoBitmap.ColorScaleGrayLiner;

        for (int i = pixels.Length - 1; i >= 0; i--)
            for (int j = 0; j < pixels[i].Length; j++)
            {
                var val = (pixels[i][j] - numericBoxMin.Value) / (numericBoxMax.Value - numericBoxMin.Value);

                int density = Math.Min(65535, Math.Max((int)(val * 65535), 0));

                g.FillPie(new SolidBrush(Color.FromArgb(scale[density].R, scale[density].G, scale[density].B)),
                    -(i + 1.0) / pixels.Length, -(i + 1.0) / pixels.Length,
                    (i + 1.0) / pixels.Length * 2, (i + 1.0) / pixels.Length * 2,
                    -(double)(j + 1) / pixels[i].Length * 360.0,
                    1.0 / pixels[i].Length * 360.0);
            }
    }

    #region DrawOutline 輪郭を描画する)

    public void DrawOutline(Graphics g)
    {
        if (stereonetDirecion == StereonetDirection.Equrtor)//赤道モードのとき
        {

            int div = 1000;
            PointD[] pt1 = new PointD[div];
            PointD[] pt2 = new PointD[div];

            double[] cos2 = new double[div];
            double[] sin2 = new double[div];

            for (int i = 0; i < div; i++)
            {
                double d = (double)i / div * Math.PI - Math.PI / 2;
                cos2[i] = Math.Cos(d);
                sin2[i] = Math.Sin(d);
            }

            for (int w = 1; w < 90; w++)
                if (showOneDeg || w % 10 == 0)
                {
                    Pen pen = w % 10 == 0 ? new Pen(new SolidBrush(TenDegColor), 0.002f) : new Pen(new SolidBrush(OneDegColor), 0.0005f);
                    pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    double theta = w * Math.PI / 180.0, cos1 = Math.Cos(theta), sin1 = Math.Sin(theta);
                    for (int i = 0; i < div; i++)
                    {
                        pt1[i] = 1 / Math.Sqrt(1 + cos2[i] * sin1) * new PointD(sin2[i] * sin1, cos1);
                        pt2[i] = -pt1[i];
                    }
                    g.DrawLines(pen, pt1);
                    g.DrawLines(pen, pt2);
                }

            for (int i = 0; i < div; i++)
            {
                double d = (double)i / div * Math.PI;
                cos2[i] = Math.Cos(d);
                sin2[i] = Math.Sin(d);
            }

            for (int w = 1; w < 90; w++)
                if (showOneDeg || w % 10 == 0)
                {
                    Pen pen = w % 10 == 0 ? new Pen(new SolidBrush(TenDegColor), 0.002f) : new Pen(new SolidBrush(OneDegColor), 0.0005f);
                    pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    double theta = w * Math.PI / 180.0, cos1 = Math.Cos(theta), sin1 = Math.Sin(theta);
                    for (int i = 0; i < div; i++)
                    {
                        pt1[i] = 1 / Math.Sqrt(1 + cos1 * sin2[i]) * new PointD(sin1 * sin2[i], cos2[i]);
                        pt2[i] = -pt1[i];
                    }
                    g.DrawLines(pen, pt1);
                    g.DrawLines(pen, pt2);
                }
        }
        else//極モードのとき
        {

            for (int w = 1; w < 180; w++)
                if (showOneDeg || w % 10 == 0)
                {
                    Pen pen = w % 10 == 0 ? new Pen(new SolidBrush(TenDegColor), 0.002f) : new Pen(new SolidBrush(OneDegColor), 0.0005f);
                    double theta = w * Math.PI / 180.0;
                    g.DrawLine(pen, -Math.Cos(theta), -Math.Sin(theta), Math.Cos(theta), Math.Sin(theta));
                    if (w < 90)
                    {
                        double radius = Math.Sin(Math.PI / 4 - theta / 2) * Math.Sqrt(2);
                        g.DrawArc(pen, -radius, -radius, 2 * radius, 2 * radius, 0, 2 * Math.PI);
                    }
                }
        }
        g.DrawArc(new Pen(new SolidBrush(NinetyDegColor), 0.004f), -1, -1, 2, 2, 0, 360);
        g.DrawLine(new Pen(new SolidBrush(NinetyDegColor), 0.004f), 0f, -1f, 0f, 1f);
        g.DrawLine(new Pen(new SolidBrush(NinetyDegColor), 0.004f), -1f, 0f, 1f, 0f);
    }

    #endregion DrawOutline 輪郭を描画する)

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
                    g.DrawString(s.Text, new Font("Tahoma", 0.08f), new SolidBrush(s.Color), p.ToPointF());
            }
        if (Lines != null)
            foreach (var l in Lines)
                g.DrawLines(new Pen(l.Color, (float)(l.LineWidth / magnification)), l.Point.Select(e => new PointD(e.X, e.Y)).ToArray());
    }


    public void DrawColorScale()
    {
        //横 320 縦 35で描画
        Bitmap bmp = new(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
        Graphics gScale = Graphics.FromImage(bmp);
        gScale.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

        var scale = comboBoxColor.SelectedIndex == 0 ? PseudoBitmap.ColorScaleColdWarmLiner : PseudoBitmap.ColorScaleGrayLiner;
        int scaleWidth = 280, scaleHeight = 15, leftMargin = 20, barLength = 5;
        for (int i = 0; i < scaleWidth; i++)
        {
            int dens = (int)(65536.0 * i / scaleWidth);
            gScale.DrawLine(new Pen(Color.FromArgb(scale[dens].R, scale[dens].G, scale[dens].B)), new Point(leftMargin + i, 0), new Point(leftMargin + i, scaleHeight));
        }

        for (int i = 0; i < 5; i++)
        {
            var x = i / 4f * scaleWidth;
            var val = (numericBoxMax.Value - numericBoxMin.Value) * i / 4.0 + numericBoxMin.Value;
            gScale.DrawLine(new Pen(Color.Black), new PointF(leftMargin + x, scaleHeight), new PointF(leftMargin + x, scaleHeight + barLength));
            gScale.DrawStringWithAlignment($"{val:g6}", new Font("Tahoma", 8f), Color.Black,
                new PointD(leftMargin + x, scaleHeight + barLength), new Size(100, 20)
                , HorizontalAlignment.Center, System.Windows.Forms.VisualStyles.VerticalAlignment.Top);
        }
        pictureBox1.Image = bmp;
    }

    public object lockObject = new();

    private void numericUpDownResolution_Click(object sender, EventArgs e)
    {
        GeneratePixels();
        Draw();
    }
    private void Combobox_SelectedIndexChanged(object sender, EventArgs e) => Draw();

    private void numericBoxMax_ValueChanged(object sender, EventArgs e) => Draw();


}