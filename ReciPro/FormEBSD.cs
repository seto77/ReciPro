#region using
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crystallography.OpenGL;
using V3 = OpenTK.Mathematics.Vector3d;
using V4 = OpenTK.Mathematics.Vector4d;
using M3 = OpenTK.Mathematics.Matrix3d;
using M4 = OpenTK.Mathematics.Matrix4d;
using C4 = OpenTK.Mathematics.Color4;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using MathNet.Numerics;
using System.ComponentModel;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System;
using static IronPython.Modules._ast;
using Microsoft.Scripting.Utils;
#endregion

namespace ReciPro;

public partial class FormEBSD : Form
{
    #region フィールド、プロパティ
    public FormMain FormMain;
    public GLControlAlpha glControlGeo;
    private readonly Stopwatch sw1 =new(), sw2 = new();
    private readonly Timer timer = new();

    /// <summary>
    /// 飛程計算の際の打ち切りエネルギー (kev)
    /// </summary>
    private double EnergyThreshold = 2;
    public double WaveLength { get => waveLengthControl.WaveLength; set => waveLengthControl.WaveLength = value; }

    public double DetTilt => numericBoxDetTilt.RadianValue;
    public double DetR => numericBoxDetRadius.Value;
    public double DetY => numericBoxYofDet.Value;
    public double DetZ => numericBoxZofDet.Value;

    public double SmpTilt => numericBoxSampleTilt.RadianValue;

    (double Depth, V3 Vec, PointD Position, double Energy)[] BSEs = [];

    public Crystal Crystal => FormMain.Crystal;

    /// <summary>
    /// 試料から検出器までの距離
    /// </summary>
    public double CameraLength2 => Math.Abs(DetY * Math.Sin(DetTilt) - DetZ * Math.Cos(DetTilt));

    /// <summary>
    /// 画像の中心。検出器(Detector)座標系(Foot原点)で表現
    /// </summary>
    public PointD Foot
    {
        get
        {
            //垂線の足の実空間座標座標
            var f = new V3(-CameraLength2 * Math.Sin(DetTilt), CameraLength2 * Math.Cos(DetTilt), 1);
            //検出器の中心座標
            var c = new V3(DetY, DetZ, 1);

            var len = (f - c).Length;
            double cos = Math.Cos(-DetTilt), sin = Math.Sin(-DetTilt);

            var rot = new M3(cos, -sin, DetY - DetY * cos + DetZ * sin,
                             sin, cos, DetZ - DetY * sin - DetZ * cos,
                             0, 0, 1);

            return (rot*f).X > c.X ? new PointD(0, len) : new PointD(0, -len);
        }
    }

    /// <summary>
    /// 画面解像度 mm/pix
    /// </summary>
    public double Resolution => 2.0 * numericBoxDetRadius.Value / graphicsBox.ClientRectangle.Width;
    public float ResolutionF => (float)Resolution;

    public int MaxNumOfBloch => numericBoxMaxNumOfG.ValueInteger;
    private double Voltage => waveLengthControl.Energy;

    //private int DivisionNumber => (int)(numericBoxDiskDiameter.ValueInteger * numericBoxDiskDiameter.ValueInteger * Math.PI / 4.0 * EnergyArray.Length);
    private int DivisionNumber => (int)(numericBoxDiskDiameter.ValueInteger * numericBoxDiskDiameter.ValueInteger * EnergyArray.Length);
    public double[] ThicknessArray
    {
        get
        {
            var thicknessArray = new List<double>();
            for (double thickness = numericBoxThicknessStart.Value; thickness <= numericBoxThicknessEnd.Value; thickness += numericBoxThicknessStep.Value)
                thicknessArray.Add(thickness);
            return [.. thicknessArray];
        }
    }

    private Vector3DBase[] Directions;

    private PseudoBitmap Pbmp = null;

    private double[] EnergyArray
    {
        get
        {
            var energyArray = new List<double>();
            for (double energy = numericBoxEnergyStart.Value; energy >= numericBoxEnergyEnd.Value - 0.0000001; energy -= numericBoxEnergyStep.Value)
                energyArray.Add(energy);
            return [.. energyArray];
        }
    }

    public int DetectorDivision = 5;


    #endregion

    #region コンストラクタ、ロード、クローズ
    public FormEBSD()
    {
        InitializeComponent();

        
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        graphicsBox.Refresh();
    }

    private void FormEBSD_Load(object sender, EventArgs e)
    {
        glControlGeo = new GLControlAlpha()
        {
            AllowMouseRotation = true,
            AllowMouseScaling = true,
            AllowMouseTranslating = false,
            Name = "glControlAxes",
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 120.0,
            RotationMode = GLControlAlpha.RotationModes.Object,
            Dock = DockStyle.Fill,
            LightPosition = new V3(100, 100, 100),
            BorderStyle = BorderStyle.Fixed3D,

            WorldMatrix = M4.CreateRotationZ(-Math.PI / 2 * 0.2) * M4.CreateRotationY(-Math.PI / 2 * 0.8) * M4.CreateRotationZ(-Math.PI / 2),
        };
        panelGeometry.Controls.Add(glControlGeo);

        timer.Interval = 1000;
        timer.Tick += Timer_Tick;
        timer.Start();

        SetVector();
        DrawGeometry();
        comboBoxGradient.SelectedIndex = 1;
        comboBoxScale.SelectedIndex = 0;
        NumericBoxEnergyStart_ValueChanged(sender, e);
        NumericBoxThicknessStart_ValueChanged(sender, e);
    }

    private void FormEBSD_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonEBSD.Checked = false;
        Visible = false;
    }
    #endregion

    #region OpenGLで入射電子、試料、検出器の幾何学を描画し、ステレオネット上に検出器の輪郭を描画
    /// <summary>
    /// 試料と電子線が交差する位置は常に(0,0,0)
    /// </summary>
    public void DrawGeometry(int i = -1, int j = -1)
    {
        #region OpenGLによる3D描画
        var glObjects = new List<GLObject>();

        //試料の傾き
        var samRot = Matrix3D.RotX(SmpTilt);
        //試料を示す直方体
        var sample = new Parallelepiped(samRot * new V3(-15, -15, -1), samRot * new V3(30, 0, 0), samRot * new V3(0, 30, 0), samRot * new V3(0, 0, 1), new Material(C4.AliceBlue), DrawingMode.SurfacesAndEdges);
        glObjects.Add(sample);

        //検出器の傾き
        var detector = new Cylinder(new V3(0, -DetY, -DetZ), new V3(0, Math.Sin(DetTilt), -Math.Cos(DetTilt)), DetR, new Material(C4.GreenYellow, 0.7), DrawingMode.Surfaces, true, 2, 180);
        glObjects.Add(detector);

        //XYZ軸
        var len = 50;
        //X軸
        glObjects.Add(new Lines([new V3(0, 0, 0), new V3(len, 0, 0)], 3f, new Material(C4.OrangeRed)));
        glObjects.Add(new TextObject("+X", 10f, new V3(len, 0, 0), 100, true, new Material(C4.OrangeRed), glControlGeo));

        //Y軸
        glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, -len, 0)], 3f, new Material(C4.YellowGreen)));
        glObjects.Add(new TextObject("+Y", 10f, new V3(0, -len, 0), 100, true, new Material(C4.YellowGreen), glControlGeo));

        //Z軸 = beam
        glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, 0, -len)], 3f, new Material(C4.MediumPurple)));
        glObjects.Add(new TextObject("+Z (=beam)", 10f, new V3(0, 0, -len), 100, true, new Material(C4.MediumPurple), glControlGeo));

        //照射点から検出器の縁への黄色線
        glObjects.AddRange(Enumerable.Range(0, 30).Select(e =>
        {
            var θ = e / 15.0 * Math.PI;
            var p = M3.CreateRotationX(-DetTilt) * (DetR * new V3(-Math.Sin(θ), Math.Cos(θ), 0));
            return new Lines([new V3(0, 0, 0), new(p.X, p.Y - DetY, p.Z - DetZ)], 1f, new Material(C4.Yellow, 0.7));
        }));

        //電子線方向を示す矢印
        glObjects.Add(new Cone(new V3(0, 0, 0), new V3(0, 0, 100), 5, new Material(C4.Yellow, 0.7), DrawingMode.Surfaces) { IgnoreNormalSides = true });

        //結晶のa, b, c軸を表す矢印
        var max = new[] { Crystal.A, Crystal.B, Crystal.C }.Max();
        var vec = new[] { Crystal.A_Axis, Crystal.B_Axis, Crystal.C_Axis };
        C4[] color = [C4.Red, C4.Green, C4.Blue];
        string[] label = ["a", "b", "c"];
        for (int n = 0; n < 3; n++)
        {
            var v = samRot * Crystal.RotationMatrix * vec[n] / max * 10;
            glObjects.Add(new Cylinder(-v, v * 2 - 2 * v.Normarize(), 0.4, new Material(color[n]), DrawingMode.Surfaces));
            glObjects.Add(new Cone(v, -2 * v.Normarize(), 0.8, new Material(color[n]), DrawingMode.Surfaces));
            glObjects.Add(new TextObject(label[n], 13f, v + 0.1 * v.Normarize(), 0.5, true, new Material(color[n]), glControlGeo));
        }
        glObjects.Add(new Sphere(new V3(0, 0, 0), 1.2, new Material(C4.Gray), DrawingMode.Surfaces));

        glControlGeo.DeleteAllObjects();
        glControlGeo.AddObjects(glObjects);
        glControlGeo.Refresh();
        #endregion OpenGL描画ここまで

        #region ステレオネット上に検出器の輪郭を描画

        var lines = new List<(PointD[], double, Color)>();
        M3 samRot2 = M3.CreateRotationX(SmpTilt), detRot = M3.CreateRotationX(-DetTilt);
        var f1 = new Func<double, double, PointD>((x, y)
            => Stereonet.ConvertVectorToSchmidt(samRot2 * (detRot * (DetR * new V3(x, y, 0)) + new V3(0, -DetY, -DetZ))));

        var step = 60;
        var range = Enumerable.Range(0, step + 1).Select(e => (double)e);
        lines.Add((
            range.Select(n => 2.0 * Math.PI * n / step).Select(Θ => f1(Math.Sin(Θ), Math.Cos(Θ))).ToArray(),
            2, Color.Yellow));

        int div = DetectorDivision;
        for (int n = 0; n < div + 1; n++)
        {
            lines.Add((range.Select(n => 1 - 2 * n / step).Select(x => f1(2.0 * n / div - 1, x)).ToArray(), 1, Color.Orange));
            lines.Add((range.Select(n => 1 - 2 * n / step).Select(x => f1(x, 2.0 * n / div - 1)).ToArray(), 1, Color.Orange));
        }

        if ((uint)i < (uint)DetectorDivision && (uint)j < (uint)DetectorDivision)
        {
            var r1 = range.Select(n => n / step);
            lines.Add((
                [
                ..r1.Select(x => f1(2.0 * i / div - 1, 2.0 * (- j - 1 + x)/ div + 1)),
                ..r1.Select(x => f1(2.0 * (i + x) / div - 1, 2.0 * (- j) / div + 1 )),
                ..r1.Select(x => f1(2.0 * (i + 1) / div - 1, 2.0 * (- j - x) / div + 1)),
                ..r1.Select(x => f1(2.0 * (i + 1 - x) / div - 1, 2.0 * (- j - 1) / div + 1 )),
                ], 3, Color.Orange));
        }

        poleFigureControl.Lines = [.. lines];

        poleFigureControl.Draw();
        #endregion ステレオネット上に検出器の輪郭を描画 ここまで

    }
    #endregion

    #region 3Dレンダリングの視点変更
    private void buttonViewFromZ_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = M4.Identity;

    private void buttonViewFromX_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = M4.CreateRotationY(-Math.PI / 2) * M4.CreateRotationZ(-Math.PI / 2);

    private void buttonFromSurfaceNormal_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = M4.CreateRotationX(-numericBoxSampleTilt.RadianValue);

    private void buttonViewQuarter_Click(object sender, EventArgs e)
        => glControlGeo.WorldMatrix = M4.CreateRotationZ(-Math.PI / 2 * 0.2) * M4.CreateRotationY(-Math.PI / 2 * 0.8) * M4.CreateRotationZ(-Math.PI / 2);

    #endregion

    #region その他のイベント

    /// <summary>
    /// FormMainから、結晶が変更されたときに呼び出される
    /// </summary>
    public void SetCrystal()
    {
        SetVector();
        Draw();
    }

    /// <summary>
    /// サンプルや検出器の幾何学条件が変更されたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericBoxDetRadius_ValueChanged(object sender, EventArgs e)
    {
        DrawGeometry();
        Draw();
    }

    private void FormEBSD_VisibleChanged(object sender, EventArgs e)
    {
        SetVector();
        DrawGeometry();
    }

    private void radioButtonKikuchiThresholdOfStructureFactor_CheckedChanged(object sender, EventArgs e)
    {
        if (sender is RadioButton b && b.Checked)
        {
            SetVector();
            Draw();
        }
    }

    private void numericBoxKikuchiThreadSholdOfStructureFactor_ValueChanged(object sender, EventArgs e) => SetVector();

    private void colorControlExcessLine_ColorChanged(object sender, EventArgs e) => Draw();

    private void waveLengthControl_WavelengthChanged(object sender, EventArgs e) => SetVector();

    #endregion

    #region 描画関数
    /// <summary>
    /// 描画関数
    /// </summary>
    public void Draw(Graphics g = null)
    {
        
            DrawKikuchiLine();

        DrawGeometry();
    }
    #endregion

    #region プロジェクション行列の設定
    /// <summary>
    /// プロジェクション行列の設定を行う。
    /// </summary>
    public bool SetProjection(Graphics g = null)
    {
        if (g != null && graphicsBox.ClientSize.Width != 0 && graphicsBox.ClientSize.Height != 0)
            try
            {
                g.Transform = new Matrix(
                (float)(1 / Resolution), 0, 0, (float)(1 / Resolution),
                (float)(graphicsBox.ClientSize.Width / 2.0 + Foot.X / Resolution),
                (float)(graphicsBox.ClientSize.Height / 2.0 + Foot.Y / Resolution));
            }
            catch { return false; }
        return true;
    }
    #endregion

    #region 菊池線(運動学的) graphicBox を描画

    /// <summary>
    /// 
    /// </summary>
    /// <param name="graphics"></param>
    private void DrawKikuchiLine(Graphics graphics=null, int i=-1,int j=-1)
    {
        if (InvokeRequired)//別スレッドから呼び出されたとき Invokeして呼びなおす
        {
            Invoke(new Action(() => DrawKikuchiLine(graphics, i, j)), null);
            return;
        }
        //グラフィックスボックスに描画する場合
        graphics ??= graphicsBox.Graphics;
        
        if (!SetProjection(graphics)) return;

        graphics.Clear(colorControlBackGround.Color);

        if (checkBoxDrawKikuchiLineDynamical.Checked && Pbmp != null)
        {
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.PixelOffsetMode = PixelOffsetMode.Half;

            var bmp = Pbmp.GetImage(new RectangleD(0, 0, Pbmp.Width, Pbmp.Height), graphicsBox.ClientSize);
            graphics.DrawImage(bmp, new RectangleD(-DetR, -DetR - Foot.Y, DetR * 2, DetR * 2));
        }

        
        if (checkBoxDrawKikuchiLinesKinematical.Checked)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            var penExcess = new Pen(new SolidBrush(colorControlExcessLine.Color), (float)(trackBarLineWidth.Value * Resolution / 2000f));
            var diag = Resolution * Math.Sqrt(graphicsBox.ClientSize.Width * graphicsBox.ClientSize.Width + graphicsBox.ClientSize.Height * graphicsBox.ClientSize.Height) / 2;
            var font = new Font("Tahoma", (float)(trackBarStrSize.Value / 8.0 * Resolution));
            var brush = new SolidBrush(colorControlString.Color);

            var Tau = numericBoxDetTilt.RadianValue - numericBoxSampleTilt.RadianValue;

            foreach (var g in Crystal.VectorOfG_KikuchiLine)
            {
                double sinTheta = WaveLength * g.Length / 2, sin2Theta = sinTheta * sinTheta;

                Vector3DBase vec1 = Crystal.RotationMatrix * g;

                //vec2は、検出器法線がZ軸と一致するようにX軸を回転軸に回転させたベクトル
                var vec2 = Matrix3D.Rot(new Vector3DBase(1, 0, 0), -Tau) * vec1;

                //vec3は、検出器法線(Z軸)を軸としてpsiだけ回転させて、(0,y,z)の形になるようにしたベクトル
                var psi = Math.Atan2(vec2.X, vec2.Y);
                double sinPsi = Math.Sin(psi), cosPsi = Math.Cos(psi);
                var vec3 = Matrix3D.Rot(new Vector3DBase(0, 0, 1), psi) * vec2;

                //vec3normは、vec3を規格化したベクトル
                var vec3norm = vec3.Normarize();
                double sinPhi = vec3norm.Y, sin2Phi = sinPhi * sinPhi;
                double cosPhi = vec3norm.Z;

                double P = (sin2Phi - sin2Theta) / (CameraLength2 * CameraLength2 * (1 - sin2Theta)), Psqrt = Math.Sqrt(P);
                double Q = P * (sin2Phi - sin2Theta) / sin2Theta, Qsqrt = Math.Sqrt(Q);
                double Y = CameraLength2 * sinPhi * cosPhi / (sin2Phi - sin2Theta);

                if (!double.IsNaN(Psqrt) && !double.IsNaN(Qsqrt))
                {
                    // y= sinh(x) の逆関数は x = log{y+ sqrt(y*y+1)}
                    double omegaMax = Math.Log(diag * Psqrt + Math.Sqrt(diag * Psqrt * diag * Psqrt + 1)) * 2;
                    var pts = new List<PointD>();
                    for (double omega = -omegaMax; omega < omegaMax; omega += omegaMax / 500)
                    {
                        double x = Math.Sinh(omega) / Psqrt, y = -Math.Cosh(omega) / Qsqrt;
                        var pt = new PointD(cosPsi * x - sinPsi * (y - Y), sinPsi * x + cosPsi * (y - Y));

                        if (IsScreenArea(pt))
                            pts.Add(pt);
                    }

                    if (pts.Count > 1)
                    {
                        if (checkBoxKikuchiLine_Kinematical.Checked)
                            penExcess.Color = Blend(colorControlExcessLine.Color, colorControlBackGround.Color, g.RelativeIntensity);
                        graphics.DrawLines(penExcess, pts.ToArray());

                        //ラベル描画
                        //if (toolStripButtonIndexLabels.Checked)
                        {
                            //まず傾きをみて線のどちら側にラベルを付けるかを決める。θは -π ~ +πの範囲で調節
                            var original = graphics.Transform;
                            var θ = Math.Atan2(pts[^1].Y - pts[0].Y, pts[^1].X - pts[0].X);
                            if (-Math.PI / 2 < θ && θ < Math.PI / 2)
                            {
                                graphics.TranslateTransform(pts[0].X, pts[0].Y);
                                graphics.RotateTransform(θ);
                            }
                            else
                            {
                                graphics.TranslateTransform(pts[^1].X, pts[^1].Y);
                                graphics.RotateTransform(θ + Math.PI);
                            }
                            graphics.DrawString(g.Text, font, brush, new PointF(0, 0));
                            graphics.Transform = original;
                        }
                    }
                }
            }
        }

        if (checkBoxDrawDetectorOutline.Checked)
        {
            //検出器を示す円を描画
            graphics.DrawArc(new Pen(Color.Yellow, ResolutionF * 2), -DetR, -DetR - Foot.Y, DetR * 2, DetR * 2, 0, 360);
            //検出器の分割線
            for (int n = 0; n < DetectorDivision; n++)
            {
                var x = 2.0 * n / DetectorDivision - 1;
                graphics.DrawLine(new Pen(Color.Orange, ResolutionF), -DetR, x * DetR - Foot.Y, DetR, x * DetR - Foot.Y);
                graphics.DrawLine(new Pen(Color.Orange, ResolutionF), x * DetR, -DetR - Foot.Y, x * DetR, DetR - Foot.Y);
            }
            if ((uint)i < (uint)DetectorDivision && (uint)j < (uint)DetectorDivision)
            {
                double x = 2.0 * i / DetectorDivision - 1, y = 2.0 * j / DetectorDivision - 1;

                graphics.FillRectangle(new SolidBrush(Color.FromArgb(32, Color.Orange)), DetR * x, DetR * y - Foot.Y, 2 * DetR / DetectorDivision, 2 * DetR / DetectorDivision);
            }
        }
        graphicsBox.Refresh();
    }

    /// <summary>Blends the specified colors together.</summary>
    /// <param name="color">Color to blend onto the background color.</param>
    /// <param name="backColor">Color to blend the other color onto.</param>
    /// <param name="amount">How much of <paramref name="color"/> to keep,
    /// “on top of” <paramref name="backColor"/>.</param>
    /// <returns>The blended colors.</returns>
    public static Color Blend(Color color, Color backColor, double amount)
    {
        byte r = (byte)((color.R * amount) + backColor.R * (1 - amount));
        byte g = (byte)((color.G * amount) + backColor.G * (1 - amount));
        byte b = (byte)((color.B * amount) + backColor.B * (1 - amount));
        return Color.FromArgb(r, g, b);
    }

    #endregion
    
    #region 座標変換

    /// <summary>
    /// 検出器座標で与えられた座標ptが、画面内に含まれるかどうかを返す
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    private bool IsScreenArea(in PointD pt, int margin = 0)
    {
        var clientPt = convertDetectorToScreen(pt);
        return clientPt.X > margin && clientPt.Y > margin
            && clientPt.X < graphicsBox.ClientRectangle.Width - margin
            && clientPt.Y < graphicsBox.ClientRectangle.Height - margin;
    }

    /// <summary>
    /// フィルム(Src)上の位置 (mm)を座標系変換 画面(Client)上の点(pixel)に変換
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private PointD convertDetectorToScreen(in double x, in double y)
    {
        double px = (x + Foot.X) / Resolution + graphicsBox.ClientSize.Width / 2.0;
        double py = (y + Foot.Y) / Resolution + graphicsBox.ClientSize.Height / 2.0;
        return new(px, py);
    }

    /// <summary>
    /// 検出器(Detector)上の位置 (mm)を画面(Screen)上の点(pixel)に変換
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    private PointD convertDetectorToScreen(in PointD pt) => convertDetectorToScreen(pt.X, pt.Y);
    #endregion
    
    #region 菊池線 graphicsBoxのイベント (graphicsBox上のマウスイベントも含む)

    private bool MouseRangingMode = false;
    private Point MouseRangeStart, MouseRangeEnd;//, startAnimation;
    private void graphicsBox_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Clicks == 2)
        {
            var size = graphicsBox.ClientSize;
            var i = e.Location.X * DetectorDivision / size.Width;
            var j = e.Location.Y * DetectorDivision / size.Height;
            if ((uint)i < (uint)DetectorDivision && (uint)j < (uint)DetectorDivision)
            {
                //
                DrawKikuchiLine(null, i, j);
                DrawGeometry(i, j);
                CalcStatistics(i, j);
            }
        }
    }

    private void graphicsBox_MouseUp(object sender, MouseEventArgs e)
    {

    }

    private PointD lastMousePos = new();

    private void graphicsBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        var mousePos = new PointD(e.X, e.Y);
        var center = new PointD(graphicsBox.ClientSize.Width / 2.0, graphicsBox.ClientSize.Height / 2.0);
        //左ボタンが押されながらマウスが動いたとき
        if (e.Button == MouseButtons.Left)
        {
            if ((e.X - graphicsBox.ClientSize.Width / 2) * (e.X - graphicsBox.ClientSize.Width / 2) + (e.Y - graphicsBox.ClientSize.Height / 2) * (e.Y - graphicsBox.ClientSize.Height / 2)
                < Math.Min(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height) * Math.Min(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height) * 0.18)
            {
                if (mousePos != lastMousePos)
                {
                    var devPos = mousePos - lastMousePos;
                    var devAngle = Math.Atan((mousePos - lastMousePos).Length * Resolution / CameraLength2);
                    FormMain.Rotate((-1 * devPos.Y, -Math.Cos(SmpTilt - DetTilt) * devPos.X, Math.Sin(SmpTilt - DetTilt) * devPos.X), devAngle);
                }
            }
            else
                FormMain.Rotate((0, Math.Sin(SmpTilt - DetTilt), Math.Cos(SmpTilt - DetTilt)), -Math.Atan2(lastMousePos.X - center.X, lastMousePos.Y - center.Y) + Math.Atan2(mousePos.X - center.X, mousePos.Y - center.Y));
            //Draw関数は、FormMain.Rotateを呼び出した後、FormMainから呼ばれる
        }
        lastMousePos = mousePos;
    }

    private void graphicsBox_Resize(object sender, EventArgs e) => Draw();

    #endregion graphicsBoxのイベント

    #region 菊池線を初期化。最後にDraw()も呼び出す。
    /// <summary>
    /// 菊池線を初期化。最後にDraw()も呼び出す。
    /// </summary>
    /// <param name="renewCrystal"></param>
    public void SetVector(bool renewCrystal = false)
    {
        if (FormMain == null) return;
        var sw = new Stopwatch();
        sw.Start();
        int width = graphicsBox.ClientSize.Width, height = graphicsBox.ClientSize.Height;

        var crystal = FormMain.Crystal;
        crystal.SetVectorOfG(0, 2 / WaveLength, waveLengthControl.WaveSource);

        var latticeType = crystal.Symmetry.LatticeTypeStr;

        foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length == 0))
            gtemp.Flag1 = true;

        foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length > 0 && g.Extinction[0] == latticeType))
            gtemp.Flag1 = false;

        foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length > 0 && g.Extinction[0] != latticeType))
            gtemp.Flag1 = false;

        if (radioButtonKikuchiThresholdOfLength.Checked)
        {
            Crystal.VectorOfG_KikuchiLine =
            [.. Crystal.VectorOfG.Where(g => g.Length < numericBoxKikuchiThresholdOfLength.Value).OrderByDescending(g => g.Length)];
        }
        else
        {
            var list = Crystal.VectorOfG.OrderByDescending(g => g.RelativeIntensity).ToList();
            var max = Math.Min(numericBoxKikuchiThreadSholdOfStructureFactor.ValueInteger, Crystal.VectorOfG.Length);
            while (max + 1 < FormMain.Crystal.VectorOfG.Length)
            {
                if (SymmetryStatic.CheckEquivalentPlanes(list[max - 1].Index, list[max].Index, Crystal.Symmetry))
                    max++;
                else
                    break;
            }
            Crystal.VectorOfG_KikuchiLine = list[0..max];
            Crystal.VectorOfG_KikuchiLine.Reverse();
        }
        Draw();
    }
    #endregion

    #region モンテカルロ法による飛程シミュレーション
    /// <summary>
    /// モンテカルロによる飛程シミュレーション
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button2_Click(object sender, EventArgs e)
    {
        var cry = FormMain.Crystal;
        cry.GetFormulaAndDensity();
        var sum1 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity * a.AtomicNumber);
        var sum2 = cry.Atoms.Sum(a => AtomStatic.AtomicWeight(a.AtomicNumber) * a.Multiplicity);
        var sum3 = cry.Atoms.Sum(a => a.Multiplicity);
        //試料の平均原子番号. 各元素の重量比で加重平均//double Z = 79;// 79 29 13;
        double Z = sum1 / sum2;
        //試料の平均原子量 (g/mol)
        double A = sum2 / sum3; //196.96 63.55 26.98;
        //試料の密度 (g/cm^3)
        double ρ = cry.Density; // 19.32 8.96 2.70 
        //入射電子のエネルギー (kev)
        double energy = waveLengthControl.Energy;
        //サンプルの傾き
        double cosTilt = Math.Cos(SmpTilt), sinTilt = Math.Sin(SmpTilt);

        var monte = new MonteCarlo(Z, A, ρ, energy, SmpTilt, EnergyThreshold);

        //飛程計算ループ
        sw1.Restart();
        var loop = 5_000_000;
        M3 smpRot = M3.CreateRotationX(SmpTilt);
        BSEs = ParallelEnumerable.Range(0, loop)
            .Select(_ => monte.GetBackscatteredElectrons())
            .Where(e => e.e > EnergyThreshold)
            .Select(e => (e.d,e.v, Stereonet.ConvertVectorToSchmidt(smpRot * e.v),e.e))
            .ToArray();
        toolStripStatusLabel1.Text = $"{sw1.ElapsedMilliseconds} msec. ellapsed for {loop:#,0} backscattered electrons.";

        //ステレオネット描画
        if (radioButtonFrequency.Checked)
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Histogram;
        else if (radioButtonAverageEnergy.Checked)
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Average;
        else
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Sigma;

        M3 rot = M3.CreateRotationX(SmpTilt);
        poleFigureControl.Vectors = BSEs.Select(e => new V4(rot * e.Vec, e.Energy)).ToArray();

        CalcStatistics();

    }
    #endregion

    #region 統計情報を計算しグラフ化
    public void CalcStatistics(int i=-1, int j=-1)
    {
        if (BSEs != null && BSEs.Length > 1 && poleFigureControl.Lines != null && poleFigureControl.Lines.Length > 0)
        {
            double energy = waveLengthControl.Energy;

            M3 smpRot = M3.CreateRotationX(SmpTilt), detRot = M3.CreateRotationX(-DetTilt);
            double cosTilt = Math.Cos(SmpTilt), sinTilt = Math.Sin(SmpTilt);

            #region 検出器の範囲内におさまるbseを抽出し、変数bseに格納
            PointD[] area=[];
            var areaStep = 120;
            var f = new Func<double, double, PointD>((x, y) 
                => Stereonet.ConvertVectorToSchmidt(smpRot * (detRot * (DetR * new V3(x, y, 0)) + new V3(0, -DetY, -DetZ))));
            if ((uint)i < (uint)DetectorDivision && (uint)j < (uint)DetectorDivision)//
            {
                var div = DetectorDivision;
                var r1 = Enumerable.Range(0, areaStep).Select(n => (double)n / areaStep);
                area =
                [
                    ..r1.Select(x => f(2.0 * i / div - 1, 2.0 * (- j - 1 + x)/ div + 1)),
                    ..r1.Select(x => f(2.0 * (i + x) / div - 1, 2.0 * (- j) / div + 1 )),
                    ..r1.Select(x => f(2.0 * (i + 1) / div - 1, 2.0 * (- j - x) / div + 1)),
                    ..r1.Select(x => f(2.0 * (i + 1 - x) / div - 1, 2.0 * (- j - 1) / div + 1 ))
                ];
            }
            else
                area = [.. Enumerable.Range(0, areaStep).Select(n => 2.0 * Math.PI * n / areaStep).Select(Θ => f(Math.Sin(Θ), Math.Cos(Θ)))];

            //ある立体角に収まるbseだけを抽出
            var bse2 = BSEs.AsParallel().Where(e =>
            Geometry.InsidePolygonalArea(area, e.Position)).ToArray();
            #endregion
           // bse2 = bse2.Where(e => e[^1].e > energy - 2.5 && e[^1].e < energy - 1.5 && e.Length>2).ToArray();
            
            var count = bse2.Length;
            //エネルギー分布を描画 ここから
            //if(false)
            {
                double step = 0.25;//kev単位
                double lower = 0, upper = (energy - EnergyThreshold);
                int nBuckets = (int)((upper - lower) / step);
                var histogram = new MathNet.Numerics.Statistics.Histogram(bse2.Select(e => energy - e.Energy ), nBuckets, lower, lower + nBuckets * step);
                var pts = new List<PointD>();
                for (int n = 0; n < histogram.BucketCount; n++)
                    pts.Add(new PointD((histogram[n].UpperBound + histogram[n].LowerBound) / 2, (double)histogram[n].Count / count));
                //pts.Add(new PointD(energy*1000 + step / 2, 0));
                graphControlEnergyProfile.ClearProfile();
                graphControlEnergyProfile.Profile = new Profile(pts);
                graphControlEnergyProfile.MaximalX = upper;
                graphControlEnergyProfile.UpperX = upper * 0.5;
                graphControlEnergyProfile.Draw();
            }
            //エネルギー分布を描画 ここまで

            //最大深さ分布　ここから
            {
                //var depths = bse2.Select(e1 => 1000.0 * e1.Max(e2 => sinTilt * e2.p.Y - cosTilt * e2.p.Z));
                var depths = bse2.Select(e => e.Depth);
                double lower = 0, upper = depths.Max();
                double step = 1;//nm単位
                int nBuckets = (int)((upper - lower) / step + 1);
                var histogram = new MathNet.Numerics.Statistics.Histogram(depths, nBuckets, lower, lower + nBuckets * step);
                var pts = new List<PointD>();
                for (int n = 0; n < histogram.BucketCount; n++)
                    pts.Add(new PointD((histogram[n].UpperBound + histogram[n].LowerBound) / 2, (double)histogram[n].Count / count));
                graphControlDepthProfile.ClearProfile();
                graphControlDepthProfile.Profile = new Profile(pts);
                graphControlDepthProfile.UpperX = upper * 0.5;
                graphControlDepthProfile.Draw();
            }
        }

    }
    #endregion

    #region 入力パラメータ関連
    private void NumericBoxThicknessStart_ValueChanged(object sender, EventArgs e)
    {
        trackBarOutputThickness.Maximum = ThicknessArray.Length - 1;
        trackBarOutputThickness.Value = 0;
    }

    private void NumericBoxEnergyStart_ValueChanged(object sender, EventArgs e)
    {
        trackBarOutputEnergy.Maximum = EnergyArray.Length - 1;
        trackBarOutputEnergy.Value = 0;
    }

    #endregion

    #region 現在のパラメータでEBSDを動力学計算
    private void buttonSimulateEBSD_Click(object sender, EventArgs e)
    {
        if (Crystal.Bethe.IsCBED_Busy) return;

        buttonStop.Visible = true;
        sw1.Restart();
        //FormDiffractionSimulator.SkipDrawing = true;
     

        //方位配列を作る 
        
        double cos = Math.Cos(DetTilt), sin = Math.Sin(DetTilt);
        var rotDet = new M4(1, 0, 0, 0,
                            0, cos, -sin, DetY - DetY * cos + DetZ * sin,
                            0, sin, cos, DetZ - DetY * sin - DetZ * cos,
                            0, 0, 0, 1);

        var rotSmp = M4.CreateRotationX(numericBoxSampleTilt.RadianValue + Math.PI);

        var size = numericBoxDiskDiameter.ValueInteger;
        var directions = new List<Vector3DBase>();
        for (int h = 0; h < size; h++)
            for (int w = 0; w < size; w++)
            {
                //検出器の座標
                var vec = rotDet * new V4(DetR * (2 * w + 1) / size - DetR, DetR * (2 * h + 1) / size - DetR + DetY, DetZ, 1);
                vec = rotSmp * vec;

                directions.Add(new Vector3DBase(-vec.X, -vec.Y, -vec.Z).Normarize());
            }

        Directions = [.. directions];

        var solver = BetheMethod.Solver.MtxExp_Eigen;
        Crystal.Bethe.EBSD_Completed += Bethe_EBSD_Completed;
        Crystal.Bethe.EBSD_ProgressChanged += Bethe_EBSD_ProgressChanged;
        Crystal.Bethe.RunEBSD(MaxNumOfBloch, EnergyArray, Crystal.RotationMatrix, ThicknessArray, Directions, solver, 32);
    }

    #region BackgroundWorkerからのProgressChanged, Completed

    private bool skipProgressChangedEvent = false;
    private void Bethe_EBSD_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if (skipProgressChangedEvent) return;
        skipProgressChangedEvent = true;

        var current = e.ProgressPercentage;
        var message = (string)e.UserState;
        if (message.StartsWith("Compiling disks", StringComparison.Ordinal))
        {
            //if (sw1.IsRunning)
            //{
            //    sw1.Stop();
            //    sw2.Restart();
            //}
            //var sec = sw2.ElapsedMilliseconds / 1000.0;
            //var totalsec = sec + sw1.ElapsedMilliseconds / 1000.0;
            //toolStripProgressBar.Value = current / 10;
            //toolStripStatusLabel2.Text = "Compiling disks:";
            //toolStripStatusLabel1.Text = "Ellapsed time : " + totalsec.ToString("f2") + " s.,  ";
            //toolStripStatusLabel1.Text += $"{current / 10.0:f1} % completed,  wait for more {sec * (1000 - current) / current:f2} s.";
        }
        else
        {
            var sec = sw1.ElapsedMilliseconds / 1000.0;
            var progress = (int)(100.0 * current / DivisionNumber);
            if (progress <= 100)
                toolStripProgressBar.Value = (int)(100.0 * current / DivisionNumber);
            toolStripStatusLabel2.Text = message;
            toolStripStatusLabel1.Text = "Ellapsed time : " + sec.ToString("f2") + " s.,  time/pixel: ";
            toolStripStatusLabel1.Text += sec / current > 0.9 ? $"{sec / current:f2} s.,  " : $"{sec / current * 1000:f2} ms., ";
            toolStripStatusLabel1.Text += $"{100.0 * current / DivisionNumber:f1} % completed,  wait for {sec * (DivisionNumber - current) / current:f2} s.";
        }
        Application.DoEvents();
        skipProgressChangedEvent = false;
    }

    private void Bethe_EBSD_Completed(object sender, RunWorkerCompletedEventArgs e)
    {
        buttonStop.Visible = false;
        Crystal.Bethe.EBSD_Completed -= Bethe_EBSD_Completed;
        Crystal.Bethe.EBSD_ProgressChanged -= Bethe_EBSD_ProgressChanged;
        sw2.Stop();
        var sec1 = sw1.ElapsedMilliseconds / 1000.0;
        var sec2 = sw2.ElapsedMilliseconds / 1000.0;

        if (!e.Cancelled)
        {
            toolStripStatusLabel2.Text = "100% completed!  ";
            toolStripStatusLabel1.Text = $"Total time: {sec1 + sec2:f2} s.   ";
            toolStripStatusLabel1.Text += $"Bloch problem: {sec1:f2} s. (";
            toolStripStatusLabel1.Text += sec1 / DivisionNumber > 1 ? $"{sec1 / DivisionNumber:f2} s " : $"{sec1 / DivisionNumber * 1000:f2} ms ";
            toolStripStatusLabel1.Text += $"/pixes).   Compiling disks: {sec2:f2} s.";

            groupBoxOutput.Enabled = true;
            generateImage();
        }
        else
        {
            toolStripStatusLabel1.Text = "Time ellapsed: " + (sec1 + sec2).ToString("f2") + " sec.,  Manually interupted.";
            groupBoxOutput.Enabled = false;
        }
        Application.DoEvents();
    }

    #endregion

    #endregion

    #region EBSD計算後、画像を生成
    private void generateImage(bool resetDisks = true)
    {
        if (Crystal.Bethe.Disks == null || trackBarOutputEnergy.Value >= Crystal.Bethe.Disks.Length || trackBarOutputThickness.Value >= Crystal.Bethe.Disks[0].Length)
            return;

        var disk = Crystal.Bethe.Disks[trackBarOutputEnergy.Value][trackBarOutputThickness.Value];
        Pbmp = new PseudoBitmap(disk.Amplitudes.Select(e => e.MagnitudeSquared()).ToArray(), numericBoxDiskDiameter.ValueInteger) { AlphaEnabled = true };

        Pbmp.FilterAlfha = Pbmp.SrcValuesGrayOriginal.Select(e => e == 0 ? (byte)0 : (byte)255).ToList();

        AdjustImage();

    }

    private void AdjustImage()
    {
        if (Pbmp == null)
            return;
        var colorScale = comboBoxScale.SelectedIndex;
        //GrayかColorか
        if (colorScale == 0)
            Pbmp.SetScaleGray();
        else if (colorScale == 1)
            Pbmp.SetScaleColdWarm();
        else if (colorScale == 2)
            Pbmp.SetScaleSpectrum();
        else
            Pbmp.SetScaleFire();

        //Negativeかどうか
        Pbmp.IsNegative = comboBoxGradient.SelectedIndex == 1;

        var maxRatio = (double)trackBarIntensityBrightnessMax.Value / trackBarIntensityBrightnessMax.Maximum;
        var minRatio = (double)trackBarIntensityBrightnessMin.Value / trackBarIntensityBrightnessMin.Maximum;

        double max = Pbmp.SrcValuesGray.Max(), min = Pbmp.SrcValuesGray.Min(), dev = max - min;

        Pbmp.MaxValue = dev * maxRatio + min;
        Pbmp.MinValue = dev * minRatio + min;

        Draw();
        Application.DoEvents();
    }


    #endregion

    #region 画像出力パラメータのイベント

    private void TrackBarOutputThickness_Scroll(object sender, EventArgs e)
    {
        if (Crystal.Bethe.Disks == null || Crystal.Bethe.Disks.Length < 1 || trackBarOutputThickness.Value >= Crystal.Bethe.Disks[0].Length)
            return;
        textBoxThickness.Text = ThicknessArray[trackBarOutputThickness.Value].ToString();
        generateImage();
    }
    private void trackBarOutputEnergy_ValueChanged(object sender, EventArgs e)
    {
        if (Crystal.Bethe.Disks == null || trackBarOutputEnergy.Value >= Crystal.Bethe.Disks.Length || trackBarOutputEnergy.Value < 0)
            return;

        textBoxEnergy.Text = EnergyArray[trackBarOutputEnergy.Value].ToString();
        generateImage();
    }
    private void trackBarIntensityBrightnessMax_ValueChanged(object sender, EventArgs e) => AdjustImage();

    #endregion

    private void buttonSaveImage_Click(object sender, EventArgs e)
    {
        if (Pbmp != null)
            Clipboard.SetDataObject(Pbmp.GetImage());
    }

    #region 画像を生成
    private void button1_Click(object sender, EventArgs e)
    {
        var imgSize = (int)Math.Sqrt(Crystal.Bethe.Disks[0][0].Amplitudes.Length);

        M3 smpRot = M3.CreateRotationX(SmpTilt), detRot = M3.CreateRotationX(-DetTilt);
        double cosTilt = Math.Cos(SmpTilt), sinTilt = Math.Sin(SmpTilt);

        PointD[] area = [];
        var areaStep = 32;
        var f = new Func<double, double, PointD>((x, y)
            => Stereonet.ConvertVectorToSchmidt(smpRot * (detRot * (DetR * new V3(x, y, 0)) + new V3(0, -DetY, -DetZ))));
        area = [.. Enumerable.Range(0, areaStep).Select(n => 2.0 * Math.PI * n / areaStep).Select(Θ => f(Math.Sin(Θ), Math.Cos(Θ)))];
        //まず検出器に入る電子を抽出し、これをbse1とする
        var bse1 = BSEs.AsParallel()
            .Select(e=> (e.Depth,e.Position,e.Energy))
            .Where(e =>Geometry.InsidePolygonalArea(area, e.Position)).ToArray();
        var div = 15;//DetectorDivision;
        var r1 = Enumerable.Range(0, areaStep).Select(n => (double)n / areaStep);
        double[] values = new double[Pbmp.SrcValuesGrayOriginal.Length];

        int[,][] mask = new int[div, div][];
        for (int i = 0; i < div; i++)
            for (int j = 0; j < div; j++)
                mask[i, j] = Enumerable.Range(0, imgSize * imgSize).Where(k =>
                {
                    double x = (k % imgSize) / (double)imgSize * div;
                    double y = (k / imgSize) / (double)imgSize * div;
                    return (x >= i && x < i + 1 && y >= j && y < j + 1);
                }).ToArray();

        for (int i = 0; i < div; i++)
            for (int j = 0; j < div; j++)
            {
                area = [..r1.Select(x => f(2.0 * i / div - 1, 2.0 * (- j - 1 + x)/ div + 1)),
                        ..r1.Select(x => f(2.0 * (i + x) / div - 1, 2.0 * (- j) / div + 1 )),
                        ..r1.Select(x => f(2.0 * (i + 1) / div - 1, 2.0 * (- j - x) / div + 1)),
                        ..r1.Select(x => f(2.0 * (i + 1 - x) / div - 1, 2.0 * (- j - 1) / div + 1 ))];

                //検出器の(i,j)位置に該当する電子だけを抽出し、これをbse2とする
                var bse2 = bse1.AsParallel()
                    .Where(e => Geometry.InsidePolygonalArea(area, e.Position))
                    .Select(e=> (e.Depth,e.Energy)).ToArray();

                for (int eIndex = 0; eIndex < EnergyArray.Length - 1; eIndex++)
                {
                    //bse2の中から特定のエネルギーを抽出し、これをbse3とする 
                    var depths = bse2.Where(e => EnergyArray[eIndex] > e.Energy && EnergyArray[eIndex + 1] < e.Energy).Select(e=>e.Depth).ToArray();

                    //bse3に対する最大深さ分布　ここから
                    double lower = ThicknessArray[0] - numericBoxThicknessStep.Value / 2, upper = ThicknessArray[^1] + numericBoxThicknessStep.Value / 2;
                    double step = numericBoxThicknessStep.Value;//nm単位
                    int nBuckets = (int)((upper - lower) / step + 1);
                    var histogram = new MathNet.Numerics.Statistics.Histogram(depths, nBuckets, lower, lower + nBuckets * step);
                    for (int t = 0; t < ThicknessArray.Length; t++)
                    {
                        foreach (var k in mask[i, j])
                            values[k] += histogram[t].Count * Crystal.Bethe.Disks[eIndex][t].Amplitudes[k].MagnitudeSquared();
                    }
                }
            }
        Pbmp = new PseudoBitmap(values, numericBoxDiskDiameter.ValueInteger) { AlphaEnabled = true };
        Pbmp.FilterAlfha = Pbmp.SrcValuesGrayOriginal.Select(e => e == 0 ? (byte)0 : (byte)255).ToList();

        AdjustImage();
    }
    #endregion


    #region グラフをコピー
    private void buttonCopyEnergyProfile_Click(object sender, EventArgs e)
    {
        if (graphControlEnergyProfile.Profile == null) return;

        var pt = graphControlEnergyProfile.Profile.Pt;
        var sb = new StringBuilder();
        for (int i = 0; i < pt.Count; i++)
            sb.AppendLine(pt[i].X + "\t" + pt[i].Y);

        Clipboard.SetDataObject(sb.ToString());
    }

    private void buttonDepthProfile_Click(object sender, EventArgs e)
    {
        if (graphControlDepthProfile.Profile == null) return;

        var pt = graphControlDepthProfile.Profile.Pt;
        var sb = new StringBuilder();
        for (int i = 0; i < pt.Count; i++)
            sb.AppendLine(pt[i].X + "\t" + pt[i].Y);

        Clipboard.SetDataObject(sb.ToString());
    }
    #endregion

}
