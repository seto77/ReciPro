using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Crystallography.Controls
{
    internal enum StereonetProjectionMode
    {
        Wulff, Schmidt
    }

    internal enum StereonetDirection
    {
        Equrtor, Pole
    }

    public partial class PoleFigureControl : UserControl
    {
        private StereonetProjectionMode stereonetProjectionMode = StereonetProjectionMode.Schmidt;
        private StereonetDirection stereonetDirecion = StereonetDirection.Equrtor;
        private bool showOneDeg = false;
        private bool showTenDeg = true;

        private Color backGroundColor = Color.White;
        private Color OneDegColor = Color.FromArgb(192, 192, 255);
        private Color TenDegColor = Color.FromArgb(128, 128, 255);
        private Color NinetyDegColor = Color.FromArgb(0, 0, 255);

        private Crystal crystal;

        public Crystal Crystal
        {
            set { crystal = value; Draw(true); }
            get { return crystal; }
        }

        private double[][] pixels;

        private double magnification = 1;
        private PointD center = new PointD(0, 0);

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
            if (pictureBox.ClientSize.Width != 0 && pictureBox.ClientSize.Height != 0)
            {
                // if (tabControl1.TabIndex == 1)
                //     graphControlMindex.Profile = Mindex();
                // else
                {
                    Bitmap bmp = new Bitmap(pictureBox.ClientSize.Width, pictureBox.ClientSize.Height);
                    Graphics g = Graphics.FromImage(bmp);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.Transform = new System.Drawing.Drawing2D.Matrix((float)magnification, 0, 0, -(float)magnification,
                        (float)(pictureBox.ClientSize.Width / 2.0 - center.X * magnification), (float)(pictureBox.ClientSize.Height / 2.0 - center.Y * magnification));

                    g.Clear(Color.White);

                    if (renewDensityPixels)
                        pixels = generateDensityArrayNormal(Math.PI / 180.0 * (double)numericUpDownResolution.Value);
                    if (pixels != null)
                        DrawDensity(g, pixels);

                    DrawOutline(g);

                    pictureBox.Image = bmp;
                }
            }
        }

        private void pictureBox_DoubleClick(object sender, EventArgs e)
        {
            float mag = 500;
            Bitmap bmp = new Bitmap(1024, 1024);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Transform = new System.Drawing.Drawing2D.Matrix(mag, 0, 0, -mag, 512, 512);
            g.Clear(Color.White);
            pixels = generateDensityArrayNormal(Math.PI / 180.0 * (double)numericUpDownResolution.Value);
            DrawDensity(g, pixels);
            DrawOutline(g);
            Clipboard.SetDataObject(bmp);
        }

        public void DrawDensity(Graphics g, double[][] pixels)
        {
            if (pixels == null) return;
            double average = 0;
            double fullScale = 0;
            double max = double.NegativeInfinity, min = double.PositiveInfinity;
            double sum = 0;
            int count = 0;

            for (int i = 0; i < pixels.Length; i++)
                for (int j = 0; j < pixels[i].Length; j++, count++)
                    sum += pixels[i][j];
            average = sum / count;
            fullScale = average * Math.Pow(10, (double)numericUpDownFullscale.Value);

            for (int i = 0; i < pixels.Length; i++)
                for (int j = 0; j < pixels[i].Length; j++)
                {
                    max = Math.Max(max, pixels[i][j] / average);
                    min = Math.Min(min, pixels[i][j] / average);
                }
            try
            {
                textBox1.Text = "Max: " + max.ToString("g3") + ";   Min: " + min.ToString("g3");
            }
            catch { }

            //最大値をaverage*設定値に規格化して塗りつぶし

            byte[] scaleR = new byte[65536];// = radioButtonGray.Checked ? PseudoBitmap.BrightnessScaleLiner : PseudoBitmap.BrightnessScaleLinerColorR;
            byte[] scaleG = new byte[65536];//= radioButtonGray.Checked ? PseudoBitmap.BrightnessScaleLiner : PseudoBitmap.BrightnessScaleLinerColorG;
            byte[] scaleB = new byte[65536];//= radioButtonGray.Checked ? PseudoBitmap.BrightnessScaleLiner : PseudoBitmap.BrightnessScaleLinerColorB;

            if (comboBoxColor.SelectedIndex == 1)
            {
                if (comboBoxScale.SelectedIndex == 0)
                {
                    Array.Copy(PseudoBitmap.BrightnessScaleLiner, scaleR, scaleR.Length);
                    Array.Copy(PseudoBitmap.BrightnessScaleLiner, scaleG, scaleG.Length);
                    Array.Copy(PseudoBitmap.BrightnessScaleLiner, scaleB, scaleB.Length);
                }
                else
                {
                    Array.Copy(PseudoBitmap.BrightnessScaleLog, scaleR, scaleR.Length);
                    Array.Copy(PseudoBitmap.BrightnessScaleLog, scaleG, scaleG.Length);
                    Array.Copy(PseudoBitmap.BrightnessScaleLog, scaleB, scaleB.Length);
                }
            }
            else
            {
                if (comboBoxScale.SelectedIndex == 0)
                {
                    Array.Copy(PseudoBitmap.BrightnessScaleLinerColorR, scaleR, scaleR.Length);
                    Array.Copy(PseudoBitmap.BrightnessScaleLinerColorG, scaleG, scaleG.Length);
                    Array.Copy(PseudoBitmap.BrightnessScaleLinerColorB, scaleB, scaleB.Length);
                }
                else
                {
                    Array.Copy(PseudoBitmap.BrightnessScaleLogColorR, scaleR, scaleR.Length);
                    Array.Copy(PseudoBitmap.BrightnessScaleLogColorG, scaleG, scaleG.Length);
                    Array.Copy(PseudoBitmap.BrightnessScaleLogColorB, scaleB, scaleB.Length);
                }
                Array.Reverse(scaleR);
                Array.Reverse(scaleG);
                Array.Reverse(scaleB);
            }

            for (int i = pixels.Length - 1; i >= 0; i--)
                for (int j = 0; j < pixels[i].Length; j++)
                {
                    int density = pixels[i][j] < fullScale ? (int)(65535 - pixels[i][j] / fullScale * 65535) : 0;
                    g.FillPie(new SolidBrush(Color.FromArgb(scaleR[density], scaleG[density], scaleB[density])),
                        -(i + 1.0) / pixels.Length, -(i + 1.0) / pixels.Length, (i + 1.0) / pixels.Length * 2, (i + 1.0) / pixels.Length * 2,
                    (double)j / pixels[i].Length * 360.0, 1.0 / pixels[i].Length * 360.0);
                }

            if (comboBoxColor.SelectedIndex == 1)
            {
                Array.Copy(PseudoBitmap.BrightnessScaleLiner, scaleR, scaleR.Length);
                Array.Copy(PseudoBitmap.BrightnessScaleLiner, scaleG, scaleG.Length);
                Array.Copy(PseudoBitmap.BrightnessScaleLiner, scaleB, scaleB.Length);
                Array.Reverse(scaleR);
                Array.Reverse(scaleG);
                Array.Reverse(scaleB);
            }
            else
            {
                Array.Copy(PseudoBitmap.BrightnessScaleLinerColorR, scaleR, scaleR.Length);
                Array.Copy(PseudoBitmap.BrightnessScaleLinerColorG, scaleG, scaleG.Length);
                Array.Copy(PseudoBitmap.BrightnessScaleLinerColorB, scaleB, scaleB.Length);
            }
            Graphics gScale = pictureBox1.CreateGraphics();
            for (int i = 0; i < pictureBox1.ClientSize.Width; i++)
            {
                int dens = (int)((double)i / (double)pictureBox1.ClientSize.Width * 65536);
                gScale.DrawLine(new Pen(Color.FromArgb(scaleR[dens], scaleG[dens], scaleB[dens])), new Point(i, 0), new Point(i, pictureBox.ClientSize.Height));
            }
        }

        #region DrawOutline 輪郭を描画する)

        public void DrawOutline(Graphics g)
        {
            if (stereonetDirecion == StereonetDirection.Equrtor)//赤道モードのとき
            {
                if (stereonetProjectionMode == StereonetProjectionMode.Wulff)//Wulffネット
                {
                    if (showTenDeg)
                    {
                        for (int w = 1; w < 90; w++)
                            if (showOneDeg || w % 10 == 0)
                            {
                                Pen pen = w % 10 == 0 ? new Pen(new SolidBrush(TenDegColor), 0.002f) : new Pen(new SolidBrush(OneDegColor), 0.0005f);
                                double theta = w * Math.PI / 180.0, cos = Math.Cos(theta), sin = Math.Sin(theta), tan = Math.Tan(theta);

                                g.DrawArc(pen, -cos / (1 + sin), -1 / cos, 2 / cos, 2 / cos, Math.PI / 180 * (w + 90), Math.PI / 180 * (180 - 2 * w));
                                g.DrawArc(pen, -cos / (1 - sin), -1 / cos, 2 / cos, 2 / cos, Math.PI / 180 * (w - 90), Math.PI / 180 * (180 - 2 * w));

                                g.DrawArc(pen, -tan, -cos / (1 - sin), 2 * tan, 2 * tan, Math.PI / 180 * w, Math.PI / 180 * (180 - 2 * w));
                                g.DrawArc(pen, -tan, cos / (1 + sin), 2 * tan, 2 * tan, Math.PI / 180 * (w + 180), Math.PI / 180 * (180 - 2 * w));
                            }
                    }
                }
                else//Schmidtネット
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
            }
            else//極モードのとき
            {
                if (stereonetProjectionMode == StereonetProjectionMode.Wulff)//Wulffネット
                {
                    for (int w = 1; w < 180; w++)
                        if (showOneDeg || w % 10 == 0)
                        {
                            Pen pen = w % 10 == 0 ? new Pen(new SolidBrush(TenDegColor), 0.002f) : new Pen(new SolidBrush(OneDegColor), 0.0005f);
                            double theta = w * Math.PI / 180.0;
                            g.DrawLine(pen, -Math.Cos(theta), -Math.Sin(theta), Math.Cos(theta), Math.Sin(theta));
                            if (w < 90)
                                g.DrawArc(pen, -Math.Tan(theta / 2), -Math.Tan(theta / 2), 2 * Math.Tan(theta / 2), 2 * Math.Tan(theta / 2), 0, 2 * Math.PI);
                        }
                }

                else//Schmidtネット
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
            }
            g.DrawArc(new Pen(new SolidBrush(NinetyDegColor), 0.004f), -1, -1, 2, 2, 0, 360);
            g.DrawLine(new Pen(new SolidBrush(NinetyDegColor), 0.004f), 0f, -1f, 0f, 1f);
            g.DrawLine(new Pen(new SolidBrush(NinetyDegColor), 0.004f), -1f, 0f, 1f, 0f);
        }

        #endregion DrawOutline 輪郭を描画する)

        private int justBeforeX = int.MaxValue, justBeforeY = int.MaxValue, justBeforeZ = int.MaxValue;
        private double justBeforeResolution = double.NaN;
        private bool justBeforePoleFigureMode = true;
        private bool justBeforePlanesMode = true;
        private int justBeforeCrystallineNumber = -1;
        private IEnumerable<(int Radial, int Sector)>[] Index;

        public object lockObject = new object();

        public double[][] generateDensityArrayNormal(double angleResolution)
        {
            if (crystal == null || crystal.Crystallites == null) return null;

            int x = (int)numericUpDown1.Value, y = (int)numericUpDown2.Value, z = (int)numericUpDown3.Value;

            //double[][] pixelsを初期化
            int radialDivision = (int)(Math.PI / angleResolution / 2);
            double[][] pixels = new double[radialDivision][];
            for (int i = 0; i < radialDivision; i++)
            {
                double circumference = (i + 0.5) / radialDivision * Math.PI * 2;
                int sectorDivision = (int)Math.Round(circumference * radialDivision, MidpointRounding.ToEven);
                pixels[i] = Enumerable.Repeat(0.0, sectorDivision).ToArray(); 
            }

            //前回の条件と同じとき
            if (Index != null && Index.Length == Crystal.Crystallites.Rotations.Length)
            {
                if (x == justBeforeX && y == justBeforeY && z == justBeforeZ && angleResolution == justBeforeResolution && crystal.Crystallites.Rotations.Length == justBeforeCrystallineNumber
                  && justBeforePoleFigureMode == radioButtonPoleFigure.Checked && justBeforePlanesMode == radioButtonPlanes.Checked)
                {
                    for (int i = 0; i < crystal.Crystallites.Rotations.Length; i++)
                        foreach (var (Radial, Sector) in Index[i])
                            pixels[Radial][Sector] += crystal.Crystallites.Density[i] * crystal.Crystallites.SolidAngle[i];
                    for (int radial = 0; radial < pixels.Length; radial++)
                    {
                        double area = (1.0 + 2 * radial) / pixels[radial].Length;
                        for (int sector = 0; sector < pixels[radial].Length; sector++)
                            pixels[radial][sector] /= 10000 * area;
                    }
                    return pixels;
                }
            }
            else
                Index = new (int Radial, int Sector)[Crystal.Crystallites.Rotations.Length][];
            justBeforeX = x; justBeforeY = y; justBeforeZ = z; justBeforeCrystallineNumber = crystal.Crystallites.Rotations.Length;
            justBeforeResolution = angleResolution; justBeforePoleFigureMode = radioButtonPoleFigure.Checked; justBeforePlanesMode = radioButtonPlanes.Checked;

            Symmetry sym = crystal.Symmetry;
            Vector3DBase[] srcVector;

            //Normal
            if (radioButtonPoleFigure.Checked)
            {
                if (radioButtonPlanes.Checked)
                {//計算する面指数と等価な指数を算出
                    var indices = SymmetryStatic.GenerateEquivalentPlanes(x, y, z, sym);
                    srcVector = new Vector3DBase[indices.Length];
                    for (int k = 0; k < indices.Length; k++)
                    {
                        srcVector[k] = crystal.A_Star * indices[k].H + crystal.B_Star * indices[k].K + crystal.C_Star * indices[k].L;
                        if (srcVector[k].Length2 > 0)
                            srcVector[k] /= srcVector[k].Length;
                    }
                }
                else
                {//計算する軸指数と等価な指数を算出
                    var indices = SymmetryStatic.GenerateEquivalentAxes(x, y, z, sym);
                    srcVector = new Vector3DBase[indices.Length];
                    for (int k = 0; k < indices.Length; k++)
                    {
                        srcVector[k] = crystal.A_Axis * indices[k].U + crystal.B_Axis * indices[k].V + crystal.C_Axis * indices[k].W;
                        if (srcVector[k].Length2 > 0)
                            srcVector[k] /= srcVector[k].Length;
                    }
                }
            }
            //Inverse
            else
            {
                srcVector = new Vector3DBase[] { new Vector3DBase(x, y, z) };
                if (srcVector[0].Length2 > 0)
                    srcVector[0] /= srcVector[0].Length;
            }

            Parallel.For(0, crystal.Crystallites.Rotations.Length, i =>
            {
                var rot = crystal.Crystallites.Rotations[i] * Crystallite.TiltMatrix;
                var vectors = radioButtonPoleFigure.Checked
                    ? srcVector.Select(src => rot * src)//PoleFigureのとき
                    : divideVector(rot.Transpose() * srcVector[0], sym);//InversePoleFigureのとき

                Index[i] = vectors.Where(v => v.Z > 0).Select(v =>
                {
                    var radial = (int)Math.Round(Math.Sqrt((v.X * v.X + v.Y * v.Y)/ (1 + v.Z)) * radialDivision - 0.5, MidpointRounding.ToEven);
                    var sector = (int)Math.Round(Math.Atan2(v.Y, v.X) / 2 / Math.PI * pixels[radial].Length, MidpointRounding.ToEven);
                    if (sector < 0)
                        sector += pixels[radial].Length;
                    return (radial, sector);
                }).ToArray();//ToArrayをつけないと、エラー
            });

            for (int i = 0; i < crystal.Crystallites.Rotations.Length; i++)
            {
                var density = crystal.Crystallites.Density[i] * crystal.Crystallites.SolidAngle[i];
                foreach (var (Radial, Sector) in Index[i])
                    pixels[Radial][Sector] += density;
            }

            //最後に面積を計算して規格化
            for (int radial = 0; radial < pixels.Length; radial++)
            {
                var area = (1.0 + 2 * radial) / pixels[radial].Length;
                for (int sector = 0; sector < pixels[radial].Length; sector++)
                    pixels[radial][sector] /= 10000 * area;
            }

            return pixels;
        }

        private IEnumerable< Vector3DBase > divideVector(Vector3DBase baseVec, Symmetry sym)
        {
            List<Vector3DBase> vec = new List<Vector3DBase>();
            double x = baseVec.X, y = baseVec.Y, z = baseVec.Z;
            double sqrt3 = Math.Sqrt(3);
            switch (sym.LaueGroupNumber)
            {
                case 0://unknown
                    vec.Add(baseVec);
                    break;

                case 1://-1
                    vec.Add(new Vector3DBase(x, y, z));
                    vec.Add(new Vector3DBase(-x, -y, -z));
                    break;

                case 2:// 2/m
                    switch (sym.MainAxis)
                    {
                        case "a":
                            vec.Add(new Vector3DBase(x, y, z));
                            vec.Add(new Vector3DBase(-x, -y, -z));
                            vec.Add(new Vector3DBase(-x, y, z));
                            vec.Add(new Vector3DBase(x, -y, -z));
                            break;

                        case "b":
                            vec.Add(new Vector3DBase(x, y, z));
                            vec.Add(new Vector3DBase(-x, -y, -z));
                            vec.Add(new Vector3DBase(x, -y, z));
                            vec.Add(new Vector3DBase(-x, y, -z));
                            break;

                        case "c":
                            vec.Add(new Vector3DBase(x, y, z));
                            vec.Add(new Vector3DBase(-x, -y, -z));
                            vec.Add(new Vector3DBase(x, y, -z));
                            vec.Add(new Vector3DBase(-x, -y, z));
                            break;
                    }
                    break;

                case 3:// mmm
                    vec.Add(new Vector3DBase(x, y, z));
                    vec.Add(new Vector3DBase(-x, -y, -z));
                    vec.Add(new Vector3DBase(-x, y, z));
                    vec.Add(new Vector3DBase(x, -y, -z));
                    vec.Add(new Vector3DBase(x, -y, z));
                    vec.Add(new Vector3DBase(-x, y, -z));
                    vec.Add(new Vector3DBase(x, y, -z));
                    vec.Add(new Vector3DBase(-x, -y, z));
                    break;

                case 4: //4/m
                    vec.Add(new Vector3DBase(+x, +y, +z));
                    vec.Add(new Vector3DBase(-x, -y, +z));
                    vec.Add(new Vector3DBase(-y, +x, +z));
                    vec.Add(new Vector3DBase(+y, -x, +z));

                    vec.Add(new Vector3DBase(+x, +y, -z));
                    vec.Add(new Vector3DBase(-x, -y, -z));
                    vec.Add(new Vector3DBase(-y, +x, -z));
                    vec.Add(new Vector3DBase(+y, -x, -z));
                    break;

                case 5: //4/mmm

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

                case 6: //-3
                    if (sym.SpaceGroupHMsubStr != "R")//Hexaセルの場合
                    {
                        vec.Add(new Vector3DBase(+x, +y, +z));
                        vec.Add(new Vector3DBase(+(-x - sqrt3 * y) / 2, +(+sqrt3 * x - y) / 2, +z));
                        vec.Add(new Vector3DBase(+(-x + sqrt3 * y) / 2, +(-sqrt3 * x - y) / 2, +z));

                        vec.Add(-vec[0]);
                        vec.Add(-vec[1]);
                        vec.Add(-vec[2]);
                    }
                    else//Rhomboセルの場合 未完成
                    { }
                    break;

                case 7: //-3m 未完成
                    if (sym.SpaceGroupHMsubStr != "R")//Hexaセルの場合
                    {
                        vec.Add(new Vector3DBase(+x, +y, +z));
                        vec.Add(new Vector3DBase(+(-x - sqrt3 * y) / 2, +(+sqrt3 * x - y) / 2, +z));
                        vec.Add(new Vector3DBase(+(-x + sqrt3 * y) / 2, +(-sqrt3 * x - y) / 2, +z));

                        vec.Add(-vec[0]);
                        vec.Add(-vec[1]);
                        vec.Add(-vec[2]);
                        if (sym.SpaceGroupHallStr.Contains("\""))//3m1の場合
                        {
                            vec.Add(new Vector3DBase(-x, +y, +z));
                            vec.Add(new Vector3DBase(-(-x - sqrt3 * y) / 2, +(+sqrt3 * x - y) / 2, +z));
                            vec.Add(new Vector3DBase(-(-x + sqrt3 * y) / 2, +(-sqrt3 * x - y) / 2, +z));
                        }
                        else
                        {
                            vec.Add(new Vector3DBase(+x, -y, +z));
                            vec.Add(new Vector3DBase(+(-x - sqrt3 * y) / 2, -(+sqrt3 * x - y) / 2, +z));
                            vec.Add(new Vector3DBase(+(-x + sqrt3 * y) / 2, -(-sqrt3 * x - y) / 2, +z));
                        }
                        vec.Add(-vec[6]);
                        vec.Add(-vec[7]);
                        vec.Add(-vec[8]);
                    }
                    else //Rhomboセルの場合 未完成
                    {
                    }
                    break;

                case 8://6/m
                    vec.Add(new Vector3DBase(+x, +y, +z));
                    vec.Add(new Vector3DBase(+(-x - sqrt3 * y) / 2, +(+sqrt3 * x - y) / 2, +z));
                    vec.Add(new Vector3DBase(+(-x + sqrt3 * y) / 2, +(-sqrt3 * x - y) / 2, +z));
                    vec.Add(new Vector3DBase(-x, -y, +z));
                    vec.Add(new Vector3DBase(-(-x - sqrt3 * y) / 2, -(+sqrt3 * x - y) / 2, +z));
                    vec.Add(new Vector3DBase(-(-x + sqrt3 * y) / 2, -(-sqrt3 * x - y) / 2, +z));

                    vec.Add(-vec[0]);
                    vec.Add(-vec[1]);
                    vec.Add(-vec[2]);
                    vec.Add(-vec[3]);
                    vec.Add(-vec[4]);
                    vec.Add(-vec[5]);
                    break;

                case 9://6/mmm
                    vec.Add(new Vector3DBase(+x, +y, +z));
                    vec.Add(new Vector3DBase(+(-x - sqrt3 * y) / 2, +(+sqrt3 * x - y) / 2, +z));
                    vec.Add(new Vector3DBase(+(-x + sqrt3 * y) / 2, +(-sqrt3 * x - y) / 2, +z));
                    vec.Add(new Vector3DBase(-x, -y, +z));
                    vec.Add(new Vector3DBase(-(-x - sqrt3 * y) / 2, -(+sqrt3 * x - y) / 2, +z));
                    vec.Add(new Vector3DBase(-(-x + sqrt3 * y) / 2, -(-sqrt3 * x - y) / 2, +z));
                    vec.Add(new Vector3DBase(-x, +y, +z));
                    vec.Add(new Vector3DBase(-(-x - sqrt3 * y) / 2, +(+sqrt3 * x - y) / 2, +z));
                    vec.Add(new Vector3DBase(-(-x + sqrt3 * y) / 2, +(-sqrt3 * x - y) / 2, +z));
                    vec.Add(new Vector3DBase(+x, -y, +z));
                    vec.Add(new Vector3DBase(+(-x - sqrt3 * y) / 2, -(+sqrt3 * x - y) / 2, +z));
                    vec.Add(new Vector3DBase(+(-x + sqrt3 * y) / 2, -(-sqrt3 * x - y) / 2, +z));

                    vec.Add(-vec[0]);
                    vec.Add(-vec[1]);
                    vec.Add(-vec[2]);
                    vec.Add(-vec[3]);
                    vec.Add(-vec[4]);
                    vec.Add(-vec[5]);
                    vec.Add(-vec[6]);
                    vec.Add(-vec[7]);
                    vec.Add(-vec[8]);
                    vec.Add(-vec[9]);
                    vec.Add(-vec[10]);
                    vec.Add(-vec[11]);
                    break;

                case 10://m3
                    vec.Add(new Vector3DBase(+x, +y, +z));
                    vec.Add(new Vector3DBase(-x, -y, +z));
                    vec.Add(new Vector3DBase(-x, +y, -z));
                    vec.Add(new Vector3DBase(+x, -y, -z));

                    vec.Add(new Vector3DBase(+z, +x, +y));
                    vec.Add(new Vector3DBase(-z, -x, +y));
                    vec.Add(new Vector3DBase(-z, +x, -y));
                    vec.Add(new Vector3DBase(+z, -x, -y));

                    vec.Add(new Vector3DBase(+y, +z, +x));
                    vec.Add(new Vector3DBase(-y, -z, +x));
                    vec.Add(new Vector3DBase(-y, +z, -x));
                    vec.Add(new Vector3DBase(+y, -z, -x));

                    vec.Add(new Vector3DBase(-x, -y, -z));
                    vec.Add(new Vector3DBase(+x, +y, -z));
                    vec.Add(new Vector3DBase(+x, -y, +z));
                    vec.Add(new Vector3DBase(-x, +y, +z));

                    vec.Add(new Vector3DBase(-z, -x, -y));
                    vec.Add(new Vector3DBase(+z, +x, -y));
                    vec.Add(new Vector3DBase(+z, -x, +y));
                    vec.Add(new Vector3DBase(-z, +x, +y));

                    vec.Add(new Vector3DBase(-y, -z, -x));
                    vec.Add(new Vector3DBase(+y, +z, -x));
                    vec.Add(new Vector3DBase(+y, -z, +x));
                    vec.Add(new Vector3DBase(-y, +z, +x));
                    break;

                case 11://m3m
                    vec.Add(new Vector3DBase(+x, +y, +z));
                    vec.Add(new Vector3DBase(-x, -y, +z));
                    vec.Add(new Vector3DBase(-x, +y, -z));
                    vec.Add(new Vector3DBase(+x, -y, -z));

                    vec.Add(new Vector3DBase(+z, +x, +y));
                    vec.Add(new Vector3DBase(-z, -x, +y));
                    vec.Add(new Vector3DBase(-z, +x, -y));
                    vec.Add(new Vector3DBase(+z, -x, -y));

                    vec.Add(new Vector3DBase(+y, +z, +x));
                    vec.Add(new Vector3DBase(-y, -z, +x));
                    vec.Add(new Vector3DBase(-y, +z, -x));
                    vec.Add(new Vector3DBase(+y, -z, -x));

                    vec.Add(new Vector3DBase(-x, -y, -z));
                    vec.Add(new Vector3DBase(+x, +y, -z));
                    vec.Add(new Vector3DBase(+x, -y, +z));
                    vec.Add(new Vector3DBase(-x, +y, +z));

                    vec.Add(new Vector3DBase(-z, -x, -y));
                    vec.Add(new Vector3DBase(+z, +x, -y));
                    vec.Add(new Vector3DBase(+z, -x, +y));
                    vec.Add(new Vector3DBase(-z, +x, +y));

                    vec.Add(new Vector3DBase(-y, -z, -x));
                    vec.Add(new Vector3DBase(+y, +z, -x));
                    vec.Add(new Vector3DBase(+y, -z, +x));
                    vec.Add(new Vector3DBase(-y, +z, +x));

                    vec.Add(new Vector3DBase(+y, +x, +z));
                    vec.Add(new Vector3DBase(-y, -x, +z));
                    vec.Add(new Vector3DBase(-y, +x, -z));
                    vec.Add(new Vector3DBase(+y, -x, -z));

                    vec.Add(new Vector3DBase(+z, +y, +x));
                    vec.Add(new Vector3DBase(-z, -y, +x));
                    vec.Add(new Vector3DBase(-z, +y, -x));
                    vec.Add(new Vector3DBase(+z, -y, -x));

                    vec.Add(new Vector3DBase(+x, +z, +y));
                    vec.Add(new Vector3DBase(-x, -z, +y));
                    vec.Add(new Vector3DBase(-x, +z, -y));
                    vec.Add(new Vector3DBase(+x, -z, -y));

                    vec.Add(new Vector3DBase(-y, -x, -z));
                    vec.Add(new Vector3DBase(+y, +x, -z));
                    vec.Add(new Vector3DBase(+y, -x, +z));
                    vec.Add(new Vector3DBase(-y, +x, +z));

                    vec.Add(new Vector3DBase(-z, -y, -x));
                    vec.Add(new Vector3DBase(+z, +y, -x));
                    vec.Add(new Vector3DBase(+z, -y, +x));
                    vec.Add(new Vector3DBase(-z, +y, +x));

                    vec.Add(new Vector3DBase(-x, -z, -y));
                    vec.Add(new Vector3DBase(+x, +z, -y));
                    vec.Add(new Vector3DBase(+x, -z, +y));
                    vec.Add(new Vector3DBase(-x, +z, +y));
                    break;
            }
            return vec;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownResolution.Increment = (decimal)Math.Pow(10, Math.Floor(Math.Log10((double)numericUpDownResolution.Value)));

            Draw(true);
        }

        private void numericUpDownResolution_Click(object sender, EventArgs e)
        {
        }

        private void radioButtonAxes_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonAxes.Checked)
            {
                labelU.Text = "u";
                labelV.Text = "v";
                labelW.Text = "w";
            }
            else
            {
                labelU.Text = "h";
                labelV.Text = "k";
                labelW.Text = "l";
            }

            Draw(true);
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            Draw(false);
        }

        private void Combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw(false);
        }

        private void radioButtonPoleFigure_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonAxes.Visible = radioButtonPlanes.Visible = radioButtonPoleFigure.Checked;
            if (radioButtonPoleFigure.Checked)
                radioButtonAxes_CheckedChanged(sender, e);
            else
            {
                labelU.Text = "x";
                labelV.Text = "y";
                labelW.Text = "z";
            }
            Draw(true);
        }

        private Profile Mindex()
        {
            if (crystal == null || crystal.Crystallites == null) return null;
            Profile p = new Profile();
            for (int i = 0; i <= 120; i++)
                p.Pt.Add(new PointD(i, 0));

            Random rn = new Random();

            var a = new Vector3DBase(1, 0, 0);
            var b = new Vector3DBase(0, 1, 0);
            var c = new Vector3DBase(0, 0, 1);

            var v1 = divideVector(a, crystal.Symmetry).ToArray();
            var v2 = divideVector(b, crystal.Symmetry).ToArray();
            var v3 = divideVector(c, crystal.Symmetry).ToArray();
            List<Matrix3D> symmetryMat = new List<Matrix3D>();
            for (int i = 0; i < v1.Length; i++)
                if (new Matrix3D(v1[i], v2[i], v3[i]).Determinant() > 0)
                    symmetryMat.Add(new Matrix3D(v1[i], v2[i], v3[i]));

            Parallel.For(0, crystal.Crystallites.TotalCrystalline, i =>
            {
                Matrix3D m1, m2;
                lock (lockObject)
                {
                    m1 = crystal.Crystallites.Rotations[rn.Next(crystal.Crystallites.TotalCrystalline)];
                    m2 = crystal.Crystallites.Rotations[rn.Next(crystal.Crystallites.TotalCrystalline)];
                }
                double dia = double.MinValue;
                Matrix3D r = m1.Transpose() * m2;
                for (int j = 0; j < symmetryMat.Count; j++)
                {
                    Matrix3D r2 = r * symmetryMat[j];
                    dia = Math.Max((r2.E11 + r2.E22 + r2.E33 - 1) / 2, dia);
                    dia = Math.Max((-r2.E11 - r2.E22 + r2.E33 - 1) / 2, dia);
                    dia = Math.Max((r2.E11 - r2.E22 - r2.E33 - 1) / 2, dia);
                    dia = Math.Max((-r2.E11 + r2.E22 - r2.E33 - 1) / 2, dia);
                }
                if (dia >= -1 && dia <= 1)
                {
                    int angle = (int)Math.Round(Math.Acos(dia) / Math.PI * 180, MidpointRounding.AwayFromZero);
                    if (angle >= 0 && angle <= 120)
                        p.Pt[angle] += new PointD(0,1);
                }
            });
            for (int i = 0; i <= 120; i++)
                p.Pt[i] = new PointD(p.Pt[i].X, p.Pt[i].Y / crystal.Crystallites.TotalCrystalline);

            return p;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Draw(false);
        }

        private void buttonGenerateRandomOrientations_Click(object sender, EventArgs e)
        {
        }
    }
}