using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls;

public partial class PoleFigureControl2 : UserControl
{
    private StereonetProjectionMode stereonetProjectionMode = StereonetProjectionMode.Schmidt;
    private StereonetDirection stereonetDirecion = StereonetDirection.Equrtor;
    private bool showOneDeg = false;
    private bool showTenDeg = true;

    private Color backGroundColor = Color.White;
    private Color OneDegColor = Color.FromArgb(192, 192, 255);
    private Color TenDegColor = Color.FromArgb(128, 128, 255);
    private Color NinetyDegColor = Color.FromArgb(0, 0, 255);

    private OpenTK.Vector3d[] vectors = null;
    /// <summary>
    /// 
    /// </summary>
    public OpenTK.Vector3d[] Vectors
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

    public (PointD Point, double Radius, Color Color, bool Fill, string Text)[] Symbols = null;


    public PoleFigureControl2()
    {
        InitializeComponent();
        comboBoxScale.SelectedIndex = 0;
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
        if(vectors == null) return; 

        //まず、指定された解像度でpixelsを生成
        var div1 =  (int) (90.0/(double)numericUpDownResolution.Value);//放射方向の分割数
        Pixels = new double[div1][];
        var areas = new double[div1];
        for (int i = 0; i < div1; i++)
        {
            var circumference_length = 2 * Math.PI * (i + 0.5) / div1;//Pixels[i]の円周の長さ (外側と内側の平均)
            var div2 = Math.Max(3, (int)(circumference_length * div1));
            Pixels[i] = new double[div2];
            areas[i] = Math.PI * (2 * i + 1) / div1 / div1 / div2;
        }

        foreach (var v in vectors.Where(v => v.Z >= 0))
        {
            // vが単位ベクトルの時、 (v.X / Sqrt(1 + v.Z), v.Y / Sqrt(1 + v.Z) )が シュミットネット上の点
            v.Normalize();

            var (x, y) = (v.X / Math.Sqrt(1 + v.Z), v.Y / Math.Sqrt(1 + v.Z));

            var i = (int)(Math.Sqrt(x * x + y * y) * div1);
            i = Math.Min(Math.Max(i, 0), div1 - 1);
            var j = (int)((Math.Atan2(y, x) + Math.PI) / 2 / Math.PI * Pixels[i].Length);
            j = Math.Min(Math.Max(j, 0), Pixels[i].Length - 1);

            Pixels[i][j] += 1 / areas[i];
        }
    }

    public void DrawDensity(Graphics g, double[][] pixels)
    {
        if (pixels == null) return;

        var average = pixels.Sum(e => e.Sum()) / pixels.Sum(e => e.Length);
        var max = pixels.Max(e=>e.Max() / average);
        var min = pixels.Min(e => e.Min() / average);
        double fullScale = average * Math.Pow(10, (double)numericUpDownFullscale.Value);
        textBox1.Text = "Max: " + max.ToString("g3") + ";   Min: " + min.ToString("g3");

        //最大値をaverage*設定値に規格化して塗りつぶし
        (byte R, byte G, byte B)[] scale;
        if (comboBoxColor.SelectedIndex == 1)
            scale = comboBoxScale.SelectedIndex == 0 ? PseudoBitmap.ColorScaleGrayLiner : PseudoBitmap.ColorScaleGrayLog;
        else
            scale = comboBoxScale.SelectedIndex == 0 ? PseudoBitmap.ColorScaleColdWarmLiner : PseudoBitmap.ColorScaleColdWarmLog;

        for (int i = pixels.Length - 1; i >= 0; i--)
            for (int j = 0; j < pixels[i].Length; j++)
            {
                int density = Math.Min(65535, Math.Max((int)(pixels[i][j] / fullScale * 65535), 0));

                g.FillPie(new SolidBrush(Color.FromArgb(scale[density].R, scale[density].G, scale[density].B)),
                    -(i + 1.0) / pixels.Length, -(i + 1.0) / pixels.Length, (i + 1.0) / pixels.Length * 2, (i + 1.0) / pixels.Length * 2,
                    (double)j / pixels[i].Length * 360.0, 1.0 / pixels[i].Length * 360.0);
            }

        if(Symbols !=null)
            foreach (var s in Symbols)
            {
                var p = new PointD(s.Point.X,-s.Point.Y);
                if (s.Fill)
                    g.FillCircle(s.Color, p, s.Radius, 255);
                if (s.Text != "")
                    g.DrawString(s.Text, new Font("Tahoma", 0.08f), new SolidBrush(s.Color), p.ToPointF());
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
        if (Symbols != null)
            foreach (var s in Symbols.Where(s=>s.Point.Length2<=1))
            {
                var p = new PointD(s.Point.X,-s.Point.Y);
                if (s.Fill)
                    g.FillCircle(s.Color, p, s.Radius, 255);
                else
                    g.DrawCircle(s.Color, p, s.Radius);
                if (s.Text != "")
                    g.DrawString(s.Text, new Font("Tahoma", 0.08f), new SolidBrush(s.Color), p.ToPointF());
            }
    }


    public void DrawColorScale()
    {
        Bitmap bmp = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
        Graphics gScale = Graphics.FromImage(bmp);

        (byte R, byte G, byte B)[] scale;
        if (comboBoxColor.SelectedIndex == 1)
            scale = comboBoxScale.SelectedIndex == 0 ? PseudoBitmap.ColorScaleGrayLiner : PseudoBitmap.ColorScaleGrayLog;
        else
            scale = comboBoxScale.SelectedIndex == 0 ? PseudoBitmap.ColorScaleColdWarmLiner : PseudoBitmap.ColorScaleColdWarmLog;
        
        for (int i = 0; i < pictureBox1.ClientSize.Width; i++)
        {
            int dens = (int)((double)i / (double)pictureBox1.ClientSize.Width * 65536);
            gScale.DrawLine(new Pen(Color.FromArgb(scale[dens].R, scale[dens].G, scale[dens].B)), new Point(i, 0), new Point(i, pictureBox.ClientSize.Height));
        }
        pictureBox1.Image= bmp;

    }

    public object lockObject = new();

    private void numericUpDownResolution_ValueChanged(object sender, EventArgs e)
    {
        numericUpDownResolution.Increment = (decimal)Math.Pow(10, Math.Floor(Math.Log10((double)numericUpDownResolution.Value)));
        Draw();
    }

    private void numericUpDownResolution_Click(object sender, EventArgs e)
    {
        GeneratePixels();
        Draw();
    }

    private void numericUpDownFullScale_ValueChanged(object sender, EventArgs e) => Draw();

    private void Combobox_SelectedIndexChanged(object sender, EventArgs e) => Draw();



}