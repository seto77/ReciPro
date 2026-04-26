using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crystallography.Controls;

internal enum StereonetProjectionMode { Wulff, Schmidt }
internal enum StereonetDirection { Equrtor, Pole }

public partial class PoleFigureControl : CaptureUserControlBase
{
    private StereonetProjectionMode stereonetProjectionMode = StereonetProjectionMode.Schmidt;
    private StereonetDirection stereonetDirecion = StereonetDirection.Equrtor;
    private bool showOneDeg = false;
    private bool showTenDeg = true;

    private Color OneDegColor = Color.FromArgb(192, 192, 255);
    private Color TenDegColor = Color.FromArgb(128, 128, 255);
    private Color NinetyDegColor = Color.FromArgb(0, 0, 255);

    private Crystal crystal;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Crystal Crystal
    {
        get => crystal;
        set { crystal = value; Draw(true); }
    }

    private double[][] pixels;
    private double magnification = 1;
    private PointD center = new(0, 0);

    public PoleFigureControl()
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

    public void Draw(bool renewDensityPixels)
    {
        magnification = Math.Min(pictureBox.Width, pictureBox.Height) / 2.2;
        if (pictureBox.ClientSize.Width == 0 || pictureBox.ClientSize.Height == 0) return;

        // 前回の Image を解放してから差し替え (bmp は pictureBox.Image に握られて生存するため g のみ using)
        var oldImage = pictureBox.Image;
        var bmp = new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Transform = new Matrix((float)magnification, 0, 0, -(float)magnification,
                (float)(pictureBox.ClientSize.Width / 2.0 - center.X * magnification),
                (float)(pictureBox.ClientSize.Height / 2.0 - center.Y * magnification));
            g.Clear(Color.White);

            if (renewDensityPixels)
                pixels = generateDensityArrayNormal(Math.PI / 180.0 * (double)numericUpDownResolution.Value);
            if (pixels != null)
                DrawDensity(g, pixels);
            DrawOutline(g);
        }
        pictureBox.Image = bmp;
        oldImage?.Dispose();
    }

    private void pictureBox_DoubleClick(object sender, EventArgs e)
    {
        const float mag = 500;
        using var bmp = new Bitmap(1024, 1024);
        using (var g = Graphics.FromImage(bmp))
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Transform = new Matrix(mag, 0, 0, -mag, 512, 512);
            g.Clear(Color.White);
            pixels = generateDensityArrayNormal(Math.PI / 180.0 * (double)numericUpDownResolution.Value);
            DrawDensity(g, pixels);
            DrawOutline(g);
        }
        Clipboard.SetDataObject(bmp);
    }

    public void DrawDensity(Graphics g, double[][] pixels)
    {
        if (pixels == null) return;

        double sum = 0;
        int count = 0;
        for (int i = 0; i < pixels.Length; i++)
            for (int j = 0; j < pixels[i].Length; j++, count++)
                sum += pixels[i][j];
        var average = sum / count;
        var fullScale = average * Math.Pow(10, (double)numericUpDownFullscale.Value);

        double max = double.NegativeInfinity, min = double.PositiveInfinity;
        for (int i = 0; i < pixels.Length; i++)
            for (int j = 0; j < pixels[i].Length; j++)
            {
                max = Math.Max(max, pixels[i][j] / average);
                min = Math.Min(min, pixels[i][j] / average);
            }
        try { textBox1.Text = $"Max: {max:g3};   Min: {min:g3}"; } catch { }

        // 最大値を average * 設定値に規格化して塗りつぶし
        (byte R, byte G, byte B)[] scale;
        if (comboBoxColor.SelectedIndex == 1)
            scale = comboBoxScale.SelectedIndex == 0 ? PseudoBitmap.ColorScaleGrayLiner : PseudoBitmap.ColorScaleGrayLog;
        else
        {
            // 旧コードは PseudoBitmap.ColorScale* を直接 Array.Reverse していて、static 配列を破壊するバグがあった
            var src = comboBoxScale.SelectedIndex == 0 ? PseudoBitmap.ColorScaleColdWarmLiner : PseudoBitmap.ColorScaleColdWarmLog;
            scale = [.. src];
            Array.Reverse(scale);
        }

        for (int i = pixels.Length - 1; i >= 0; i--)
            for (int j = 0; j < pixels[i].Length; j++)
            {
                int density = pixels[i][j] < fullScale ? (int)(65535 - pixels[i][j] / fullScale * 65535) : 0;
                using var brush = new SolidBrush(Color.FromArgb(scale[density].R, scale[density].G, scale[density].B));
                g.FillPie(brush,
                    -(i + 1.0) / pixels.Length, -(i + 1.0) / pixels.Length,
                    (i + 1.0) / pixels.Length * 2, (i + 1.0) / pixels.Length * 2,
                    (double)j / pixels[i].Length * 360.0, 1.0 / pixels[i].Length * 360.0);
            }

        using var gScale = pictureBox1.CreateGraphics();
        for (int i = 0; i < pictureBox1.ClientSize.Width; i++)
        {
            int dens = (int)((double)i / pictureBox1.ClientSize.Width * 65536);
            using var pen = new Pen(Color.FromArgb(scale[dens].R, scale[dens].G, scale[dens].B));
            gScale.DrawLine(pen, new Point(i, 0), new Point(i, pictureBox.ClientSize.Height));
        }
    }

    #region DrawOutline 輪郭を描画する

    private Pen MakeGridPen(int w) => StereonetDrawing.MakeGridPen(w, TenDegColor, OneDegColor);

    public void DrawOutline(Graphics g)
    {
        if (stereonetDirecion == StereonetDirection.Equrtor)
        {
            if (stereonetProjectionMode == StereonetProjectionMode.Wulff)
            {
                if (showTenDeg)
                {
                    for (int w = 1; w < 90; w++)
                    {
                        if (!showOneDeg && w % 10 != 0) continue;
                        using var pen = MakeGridPen(w);
                        double theta = w * Math.PI / 180.0, cos = Math.Cos(theta), sin = Math.Sin(theta), tan = Math.Tan(theta);
                        g.DrawArc(pen, -cos / (1 + sin), -1 / cos, 2 / cos, 2 / cos, Math.PI / 180 * (w + 90), Math.PI / 180 * (180 - 2 * w));
                        g.DrawArc(pen, -cos / (1 - sin), -1 / cos, 2 / cos, 2 / cos, Math.PI / 180 * (w - 90), Math.PI / 180 * (180 - 2 * w));
                        g.DrawArc(pen, -tan, -cos / (1 - sin), 2 * tan, 2 * tan, Math.PI / 180 * w, Math.PI / 180 * (180 - 2 * w));
                        g.DrawArc(pen, -tan, cos / (1 + sin), 2 * tan, 2 * tan, Math.PI / 180 * (w + 180), Math.PI / 180 * (180 - 2 * w));
                    }
                }
            }
            else // Schmidt
            {
                const int div = 1000;
                var pt1 = new PointD[div];
                var pt2 = new PointD[div];

                StereonetDrawing.DrawMeridians(g, div, pt1, pt2, latRange: false, showOneDeg, TenDegColor, OneDegColor);
                StereonetDrawing.DrawMeridians(g, div, pt1, pt2, latRange: true, showOneDeg, TenDegColor, OneDegColor);
            }
        }
        else // 極モード
        {
            for (int w = 1; w < 180; w++)
            {
                if (!showOneDeg && w % 10 != 0) continue;
                using var pen = MakeGridPen(w);
                double theta = w * Math.PI / 180.0;
                g.DrawLine(pen, -Math.Cos(theta), -Math.Sin(theta), Math.Cos(theta), Math.Sin(theta));
                if (w < 90)
                {
                    double radius = stereonetProjectionMode == StereonetProjectionMode.Wulff
                        ? Math.Tan(theta / 2)
                        : Math.Sin(Math.PI / 4 - theta / 2) * Math.Sqrt(2);
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

    private int justBeforeX = int.MaxValue, justBeforeY = int.MaxValue, justBeforeZ = int.MaxValue;
    private double justBeforeResolution = double.NaN;
    private bool justBeforePoleFigureMode = true;
    private bool justBeforePlanesMode = true;
    private int justBeforeCrystallineNumber = -1;
    private IEnumerable<(int Radial, int Sector)>[] Index;

    public Lock lockObject = new();

    public double[][] generateDensityArrayNormal(double angleResolution)
    {
        if (crystal?.Crystallites == null) return null;

        int x = (int)numericUpDown1.Value, y = (int)numericUpDown2.Value, z = (int)numericUpDown3.Value;

        // pixels を初期化
        int radialDivision = (int)(Math.PI / angleResolution / 2);
        var pixels = new double[radialDivision][];
        for (int i = 0; i < radialDivision; i++)
        {
            double circumference = (i + 0.5) / radialDivision * Math.PI * 2;
            int sectorDivision = (int)Math.Round(circumference * radialDivision, MidpointRounding.ToEven);
            pixels[i] = new double[sectorDivision];
        }

        // 前回の条件と同じならキャッシュした Index で計算
        bool sameAsLast = Index != null && Index.Length == Crystal.Crystallites.Rotations.Length
            && x == justBeforeX && y == justBeforeY && z == justBeforeZ
            && angleResolution == justBeforeResolution
            && crystal.Crystallites.Rotations.Length == justBeforeCrystallineNumber
            && justBeforePoleFigureMode == radioButtonPoleFigure.Checked
            && justBeforePlanesMode == radioButtonPlanes.Checked;

        if (sameAsLast)
        {
            for (int i = 0; i < crystal.Crystallites.Rotations.Length; i++)
                foreach (var (radial, sector) in Index[i])
                    pixels[radial][sector] += crystal.Crystallites.Density[i] * crystal.Crystallites.SolidAngle[i];
            NormalizePixels(pixels);
            return pixels;
        }

        if (Index == null || Index.Length != Crystal.Crystallites.Rotations.Length)
            Index = new IEnumerable<(int Radial, int Sector)>[Crystal.Crystallites.Rotations.Length];

        justBeforeX = x; justBeforeY = y; justBeforeZ = z;
        justBeforeCrystallineNumber = crystal.Crystallites.Rotations.Length;
        justBeforeResolution = angleResolution;
        justBeforePoleFigureMode = radioButtonPoleFigure.Checked;
        justBeforePlanesMode = radioButtonPlanes.Checked;

        var sym = crystal.Symmetry;
        Vector3DBase[] srcVector;

        if (radioButtonPoleFigure.Checked)
        {
            // 計算する面/軸指数と等価な指数を算出
            if (radioButtonPlanes.Checked)
            {
                var indices = SymmetryStatic.GenerateEquivalentPlanes((x, y, z), sym);
                srcVector = indices.Select(idx =>
                {
                    var v = crystal.A_Star * idx.H + crystal.B_Star * idx.K + crystal.C_Star * idx.L;
                    return v.Length2 > 0 ? v / v.Length : v;
                }).ToArray();
            }
            else
            {
                var indices = SymmetryStatic.GenerateEquivalentAxes((x, y, z), sym);
                srcVector = indices.Select(idx =>
                {
                    var v = crystal.A_Axis * idx.U + crystal.B_Axis * idx.V + crystal.C_Axis * idx.W;
                    return v.Length2 > 0 ? v / v.Length : v;
                }).ToArray();
            }
        }
        else
        {
            var v0 = new Vector3DBase(x, y, z);
            srcVector = [v0.Length2 > 0 ? v0 / v0.Length : v0];
        }

        Parallel.For(0, crystal.Crystallites.Rotations.Length, i =>
        {
            var rot = crystal.Crystallites.Rotations[i] * Crystallite.TiltMatrix;
            var vectors = radioButtonPoleFigure.Checked
                ? srcVector.Select(src => rot * src)
                : divideVector(rot.Transpose() * srcVector[0], sym);

            Index[i] = vectors.Where(v => v.Z > 0).Select(v =>
            {
                var radial = (int)Math.Round(Math.Sqrt((v.X * v.X + v.Y * v.Y) / (1 + v.Z)) * radialDivision - 0.5, MidpointRounding.ToEven);
                var sector = (int)Math.Round(Math.Atan2(v.Y, v.X) / 2 / Math.PI * pixels[radial].Length, MidpointRounding.ToEven);
                if (sector < 0) sector += pixels[radial].Length;
                return (radial, sector);
            }).ToArray(); // ToArray を付けないと遅延評価のままで race
        });

        for (int i = 0; i < crystal.Crystallites.Rotations.Length; i++)
        {
            var density = crystal.Crystallites.Density[i] * crystal.Crystallites.SolidAngle[i];
            foreach (var (radial, sector) in Index[i])
                pixels[radial][sector] += density;
        }

        NormalizePixels(pixels);
        return pixels;
    }

    private static void NormalizePixels(double[][] pixels)
    {
        for (int radial = 0; radial < pixels.Length; radial++)
        {
            var area = (1.0 + 2 * radial) / pixels[radial].Length;
            for (int sector = 0; sector < pixels[radial].Length; sector++)
                pixels[radial][sector] /= 10000 * area;
        }
    }

    private static Vector3DBase[] divideVector(Vector3DBase baseVec, Symmetry sym)
    {
        List<Vector3DBase> vec = [];
        double x = baseVec.X, y = baseVec.Y, z = baseVec.Z;
        double sqrt3 = Math.Sqrt(3);

        // 上半 6 個 + その符号反転 6 個 (6/m / -3 系) を生成するヘルパー
        void AddTrigonalUpper()
        {
            vec.Add(new Vector3DBase(+x, +y, +z));
            vec.Add(new Vector3DBase((-x - sqrt3 * y) / 2, (sqrt3 * x - y) / 2, +z));
            vec.Add(new Vector3DBase((-x + sqrt3 * y) / 2, (-sqrt3 * x - y) / 2, +z));
        }
        void AddNegations(int from, int count)
        {
            for (int i = from; i < from + count; i++) vec.Add(-vec[i]);
        }

        switch (sym.LaueGroupNumber)
        {
            case 0: // unknown
                vec.Add(baseVec);
                break;

            case 1: // -1
                vec.Add(new Vector3DBase(x, y, z));
                vec.Add(new Vector3DBase(-x, -y, -z));
                break;

            case 2: // 2/m
                vec.Add(new Vector3DBase(x, y, z));
                vec.Add(new Vector3DBase(-x, -y, -z));
                switch (sym.MainAxis)
                {
                    case "a":
                        vec.Add(new Vector3DBase(-x, y, z));
                        vec.Add(new Vector3DBase(x, -y, -z));
                        break;
                    case "b":
                        vec.Add(new Vector3DBase(x, -y, z));
                        vec.Add(new Vector3DBase(-x, y, -z));
                        break;
                    case "c":
                        vec.Add(new Vector3DBase(x, y, -z));
                        vec.Add(new Vector3DBase(-x, -y, z));
                        break;
                }
                break;

            case 3: // mmm
                vec.Add(new Vector3DBase(x, y, z));
                vec.Add(new Vector3DBase(-x, -y, -z));
                vec.Add(new Vector3DBase(-x, y, z));
                vec.Add(new Vector3DBase(x, -y, -z));
                vec.Add(new Vector3DBase(x, -y, z));
                vec.Add(new Vector3DBase(-x, y, -z));
                vec.Add(new Vector3DBase(x, y, -z));
                vec.Add(new Vector3DBase(-x, -y, z));
                break;

            case 4: // 4/m
                vec.Add(new Vector3DBase(+x, +y, +z));
                vec.Add(new Vector3DBase(-x, -y, +z));
                vec.Add(new Vector3DBase(-y, +x, +z));
                vec.Add(new Vector3DBase(+y, -x, +z));
                vec.Add(new Vector3DBase(+x, +y, -z));
                vec.Add(new Vector3DBase(-x, -y, -z));
                vec.Add(new Vector3DBase(-y, +x, -z));
                vec.Add(new Vector3DBase(+y, -x, -z));
                break;

            case 5: // 4/mmm
                vec.Add(new Vector3DBase(+x, +y, +z));
                vec.Add(new Vector3DBase(-x, -y, +z));
                vec.Add(new Vector3DBase(-y, +x, +z));
                vec.Add(new Vector3DBase(+y, -x, +z));
                vec.Add(new Vector3DBase(+x, -y, +z));
                vec.Add(new Vector3DBase(-x, +y, +z));
                vec.Add(new Vector3DBase(+y, +x, +z));
                vec.Add(new Vector3DBase(-y, -x, +z));
                vec.Add(new Vector3DBase(+x, +y, -z));
                vec.Add(new Vector3DBase(-x, -y, -z));
                vec.Add(new Vector3DBase(-y, +x, -z));
                vec.Add(new Vector3DBase(+y, -x, -z));
                vec.Add(new Vector3DBase(+x, -y, -z));
                vec.Add(new Vector3DBase(-x, +y, -z));
                vec.Add(new Vector3DBase(+y, +x, -z));
                vec.Add(new Vector3DBase(-y, -x, -z));
                break;

            case 6: // -3
                if (sym.SpaceGroupHMsubStr != "R") // Hexa セル
                {
                    AddTrigonalUpper();
                    AddNegations(0, 3);
                }
                // Rhombo セルは未完成
                break;

            case 7: // -3m (一部未完成)
                if (sym.SpaceGroupHMsubStr != "R") // Hexa セル
                {
                    AddTrigonalUpper();
                    AddNegations(0, 3);
                    if (sym.SpaceGroupHallStr.Contains('"')) // 3m1 の場合
                    {
                        vec.Add(new Vector3DBase(-x, +y, +z));
                        vec.Add(new Vector3DBase(-(-x - sqrt3 * y) / 2, (sqrt3 * x - y) / 2, +z));
                        vec.Add(new Vector3DBase(-(-x + sqrt3 * y) / 2, (-sqrt3 * x - y) / 2, +z));
                    }
                    else
                    {
                        vec.Add(new Vector3DBase(+x, -y, +z));
                        vec.Add(new Vector3DBase((-x - sqrt3 * y) / 2, -(sqrt3 * x - y) / 2, +z));
                        vec.Add(new Vector3DBase((-x + sqrt3 * y) / 2, -(-sqrt3 * x - y) / 2, +z));
                    }
                    AddNegations(6, 3);
                }
                break;

            case 8: // 6/m
                AddTrigonalUpper();
                vec.Add(new Vector3DBase(-x, -y, +z));
                vec.Add(new Vector3DBase(-(-x - sqrt3 * y) / 2, -(sqrt3 * x - y) / 2, +z));
                vec.Add(new Vector3DBase(-(-x + sqrt3 * y) / 2, -(-sqrt3 * x - y) / 2, +z));
                AddNegations(0, 6);
                break;

            case 9: // 6/mmm
                AddTrigonalUpper();
                vec.Add(new Vector3DBase(-x, -y, +z));
                vec.Add(new Vector3DBase(-(-x - sqrt3 * y) / 2, -(sqrt3 * x - y) / 2, +z));
                vec.Add(new Vector3DBase(-(-x + sqrt3 * y) / 2, -(-sqrt3 * x - y) / 2, +z));
                vec.Add(new Vector3DBase(-x, +y, +z));
                vec.Add(new Vector3DBase(-(-x - sqrt3 * y) / 2, (sqrt3 * x - y) / 2, +z));
                vec.Add(new Vector3DBase(-(-x + sqrt3 * y) / 2, (-sqrt3 * x - y) / 2, +z));
                vec.Add(new Vector3DBase(+x, -y, +z));
                vec.Add(new Vector3DBase((-x - sqrt3 * y) / 2, -(sqrt3 * x - y) / 2, +z));
                vec.Add(new Vector3DBase((-x + sqrt3 * y) / 2, -(-sqrt3 * x - y) / 2, +z));
                AddNegations(0, 12);
                break;

            case 10: // m3
            case 11: // m3m
                Vector3DBase[] m3 = [
                    new(+x, +y, +z), new(-x, -y, +z), new(-x, +y, -z), new(+x, -y, -z),
                    new(+z, +x, +y), new(-z, -x, +y), new(-z, +x, -y), new(+z, -x, -y),
                    new(+y, +z, +x), new(-y, -z, +x), new(-y, +z, -x), new(+y, -z, -x),
                    new(-x, -y, -z), new(+x, +y, -z), new(+x, -y, +z), new(-x, +y, +z),
                    new(-z, -x, -y), new(+z, +x, -y), new(+z, -x, +y), new(-z, +x, +y),
                    new(-y, -z, -x), new(+y, +z, -x), new(+y, -z, +x), new(-y, +z, +x),
                ];
                vec.AddRange(m3);
                if (sym.LaueGroupNumber == 11)
                {
                    Vector3DBase[] m3m = [
                        new(+y, +x, +z), new(-y, -x, +z), new(-y, +x, -z), new(+y, -x, -z),
                        new(+z, +y, +x), new(-z, -y, +x), new(-z, +y, -x), new(+z, -y, -x),
                        new(+x, +z, +y), new(-x, -z, +y), new(-x, +z, -y), new(+x, -z, -y),
                        new(-y, -x, -z), new(+y, +x, -z), new(+y, -x, +z), new(-y, +x, +z),
                        new(-z, -y, -x), new(+z, +y, -x), new(+z, -y, +x), new(-z, +y, +x),
                        new(-x, -z, -y), new(+x, +z, -y), new(+x, -z, +y), new(-x, +z, +y),
                    ];
                    vec.AddRange(m3m);
                }
                break;
        }
        return [.. vec];
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e)
    {
        numericUpDownResolution.Increment = (decimal)Math.Pow(10, Math.Floor(Math.Log10((double)numericUpDownResolution.Value)));
        Draw(true);
    }

    private void numericUpDownResolution_Click(object sender, EventArgs e) { }

    private void radioButtonAxes_CheckedChanged(object sender, EventArgs e)
    {
        (labelU.Text, labelV.Text, labelW.Text) = radioButtonAxes.Checked ? ("u", "v", "w") : ("h", "k", "l");
        Draw(true);
    }

    private void numericUpDown4_ValueChanged(object sender, EventArgs e) => Draw(false);
    private void Combobox_SelectedIndexChanged(object sender, EventArgs e) => Draw(false);

    private void radioButtonPoleFigure_CheckedChanged(object sender, EventArgs e)
    {
        radioButtonAxes.Visible = radioButtonPlanes.Visible = radioButtonPoleFigure.Checked;
        if (radioButtonPoleFigure.Checked)
            radioButtonAxes_CheckedChanged(sender, e);
        else
            (labelU.Text, labelV.Text, labelW.Text) = ("x", "y", "z");
        Draw(true);
    }

    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) => Draw(false);
}
