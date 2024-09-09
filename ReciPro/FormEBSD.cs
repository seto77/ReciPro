#region using
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crystallography.OpenGL;
using OpenTK;
using V3 = OpenTK.Vector3d;
using V4 = OpenTK.Vector4d;
using M3 = OpenTK.Matrix3d;
using C4 = OpenTK.Graphics.Color4;
using System.Diagnostics;
using System.Drawing.Drawing2D;
#endregion

namespace ReciPro;

public partial class FormEBSD : Form
{
    #region フィールド、プロパティ
    public FormMain FormMain;
    public GLControlAlpha glControlGeo;
    private Stopwatch sw = new();

    public double WaveLength { get => waveLengthControl.WaveLength; set => waveLengthControl.WaveLength = value; }

    public double DetTilt => numericBoxDetTilt.RadianValue;

    public double SmpTilt => numericBoxSampleTilt.RadianValue;

    /// <summary>
    /// 試料から検出器までの距離
    /// </summary>
    public double CameraLength2 => Math.Abs(numericBoxYofDet.Value * Math.Sin(DetTilt) - numericBoxZofDet.Value * Math.Cos(DetTilt));


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
            var c = new V3(numericBoxYofDet.Value, numericBoxZofDet.Value, 1);

            var len = (f - c).Length;
            double cos = Math.Cos(-DetTilt), sin = Math.Sin(-DetTilt), y = numericBoxYofDet.Value, z = numericBoxZofDet.Value; ;

            var rot = new M3(cos, -sin, y - y * cos + z * sin,
                             sin, cos, z - y * sin - z * cos,
                             0, 0, 1);

            return rot.Mult(f).X > c.X ? new PointD(0, len) : new PointD(0, -len);
        }
    }

    /// <summary>
    /// 画面解像度 mm/pix
    /// </summary>
    public double Resolution => 2.0 * numericBoxDetRadius.Value / graphicsBox.ClientRectangle.Width;


    #endregion

    #region コンストラクタ、ロード、クローズ
    public FormEBSD()
    {
        InitializeComponent();

        glControlGeo = new GLControlAlpha()
        {
            AllowMouseRotation = true,
            AllowMouseScaling = true,
            AllowMouseTranslating = false,
            Name = "glControlAxes",
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 200.0,
            RotationMode = GLControlAlpha.RotationModes.Object,
            Dock = DockStyle.Fill,
            LightPosition = new V3(100, 100, 100),
            BorderStyle = BorderStyle.Fixed3D,

            WorldMatrix = Matrix4d.CreateRotationY(-Math.PI / 2) * Matrix4d.CreateRotationZ(-Math.PI / 2),
        };
        panelGeometry.Controls.Add(glControlGeo);

    }

    private void FormEBSD_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        FormMain.toolStripButtonEBSD.Checked = false;
        Visible = false;
    }
    #endregion

    #region 入射電子、試料、検出器の幾何学を3Dで表示
    /// <summary>
    /// 試料と電子線が交差する位置は常に(0,0,0)
    /// </summary>
    public void DrawGeometry()
    {
        var glObjects = new List<GLObject>();

        //試料の傾き
        var samRot = Matrix3D.RotX(numericBoxSampleTilt.RadianValue);
        //試料を示す直方体
        var sample = new Parallelepiped(samRot * new V3(-15, -15, -2), samRot * new V3(30, 0, 0), samRot * new V3(0, 30, 0), samRot * new V3(0, 0, 2), new Material(C4.AliceBlue), DrawingMode.SurfacesAndEdges);
        glObjects.Add(sample);

        //検出器の傾き
        var detTilt = numericBoxDetTilt.RadianValue;
        var detR = numericBoxDetRadius.Value;
        double detY = -numericBoxYofDet.Value, detZ = -numericBoxZofDet.Value;
        var detector = new Cylinder(new V3(0, detY, detZ), new V3(0, Math.Sin(detTilt), -Math.Cos(detTilt)), detR, new Material(C4.GreenYellow, 0.7), DrawingMode.Surfaces, true, 2, 180);
        glObjects.Add(detector);

        //各種スケール

        //if (checkBoxDrawAxes.Checked)
        {
            glControlGeo.MakeCurrent();
            var len = 50;
            //X軸
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(len, 0, 0)], 3f, new Material(C4.OrangeRed)));
            glControlGeo.MakeCurrent();
            glObjects.Add(new TextObject("+X", 10f, new V3(len, 0, 0), 100, true, new Material(C4.OrangeRed)));

            //Y軸
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, -len, 0)], 3f, new Material(C4.YellowGreen)));
            glControlGeo.MakeCurrent();
            glObjects.Add(new TextObject("+Y", 10f, new V3(0, -len, 0), 100, true, new Material(C4.YellowGreen)));

            //Z軸 = beam
            glObjects.Add(new Lines([new V3(0, 0, 0), new V3(0, 0, -len)], 3f, new Material(C4.MediumPurple)));
            glControlGeo.MakeCurrent();
            glObjects.Add(new TextObject("+Z (=beam)", 10f, new V3(0, 0, -len), 100, true, new Material(C4.MediumPurple)));
        }

        glObjects.AddRange(Enumerable.Range(0, 60).Select(e =>
        {
            var θ = e / 30.0 * Math.PI;
            var p = M3.CreateRotationX(-detTilt).Mult(numericBoxDetRadius.Value * new V3(-Math.Sin(θ), Math.Cos(θ), 0));
            return new Lines([new V3(0, 0, 0), new(p.X, p.Y + detY, p.Z + detZ)], 1f, new Material(C4.Yellow, 0.7));
        }));

        //電子線方向を示す矢印
        glObjects.Add(new Cone(new V3(0, 0, 0), new V3(0, 0, 100), 5, new Material(C4.Yellow, 0.7), DrawingMode.Surfaces) { IgnoreNormalSides = true });

        //結晶のa, b, c軸を表す矢印

        glControlGeo.DeleteAllObjects();
        glControlGeo.AddObjects(glObjects);
        glControlGeo.Refresh();
        //OpenGL描画ここまで

        //ステレオネット上に検出器の輪郭を描画
        M3 samRot2 = M3.CreateRotationX(numericBoxSampleTilt.RadianValue), detRot = M3.CreateRotationX(-detTilt);
        var step = 60;
        var f = new Func<int, PointD>(i =>
        {
            var θ = 2.0 * Math.PI * i / step;
            var p = detRot.Mult(detR * new V3(Math.Sin(θ), Math.Cos(θ), 0));
            return Stereonet.ConvertVectorToSchmidt(samRot2.Mult(p + new V3(0, detY, detZ)));
        });
        poleFigureControl.Lines = [(Enumerable.Range(0, step + 1).Select(e => f(e)).ToArray(), 2, Color.Red)];
        poleFigureControl.Draw();
        //ステレオネット上に検出器の輪郭を描画 ここまで

    }
    #endregion

    #region 3Dレンダリングの視点変更
    private void buttonViewFromZ_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = Matrix4d.Identity;

    private void buttonViewFromX_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = Matrix4d.CreateRotationY(-Math.PI / 2) * Matrix4d.CreateRotationZ(-Math.PI / 2);

    private void buttonFromSurfaceNormal_Click(object sender, EventArgs e) => glControlGeo.WorldMatrix = Matrix4d.CreateRotationX(-numericBoxSampleTilt.RadianValue);

    #endregion

    #region その他のイベント

    /// <summary>
    /// サンプルや検出器の幾何学条件が変更されたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericBoxDetRadius_ValueChanged(object sender, EventArgs e) => DrawGeometry();

    #endregion

    #region 逆空間描画関数
    /// <summary>
    /// 逆空間描画関数
    /// </summary>
    /// <param name="g">Graphicsオブジェクトを指定</param>
    /// <param name="drawLabel">ラベルを書く時は、true</param>
    /// <param name="drawOverlappedImage">オーバーラップイメージを描く時はtrue. ただし、trueでも、画像がセットされていない場合は描かない　</param>
    public void Draw(Graphics g = null)
    {
        if (this.InvokeRequired)//別スレッドから呼び出されたとき Invokeして呼びなおす
        {
            this.Invoke(new Action(() => Draw(g)), null);
            return;
        }

        var sw = new Stopwatch();
        sw.Start();

        //グラフィックスボックスに描画する場合
        g ??= graphicsBox.Graphics;

        if (!SetProjection(g))
            return;

        g.Clear(colorControlBackGround.Color);
        g.SmoothingMode = SmoothingMode.None;

        g.InterpolationMode = InterpolationMode.NearestNeighbor;
        g.PixelOffsetMode = PixelOffsetMode.Half;

        //CBEDの重ね合わせ
        //if (FormDiffractionSimulatorCBED.Visible && FormDiffractionSimulatorCBED.Disks != null)
        //{
        //    foreach (var disk in FormDiffractionSimulatorCBED.Disks.Where(d => d.Bitmap != null))
        //    {
        //        start = disk.Center - disk.Size / 2;
        //        end = disk.Center + disk.Size / 2; ;
        //        var dest = new PointF[] { start.ToPointF(), new((float)end.X, (float)start.Y), new((float)start.X, (float)end.Y) };//左上、右上、左下の順番
        //        g.DrawImage(disk.Bitmap, dest, new RectangleF(0, 0, disk.PixelSize.Width, disk.PixelSize.Width), GraphicsUnit.Pixel);
        //    }
        //}

        g.SmoothingMode = SmoothingMode.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        DrawKikuchiLine(g);

        graphicsBox.Refresh();
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

    #region DrawKikuchiLine

    private void DrawKikuchiLine(Graphics graphics)
    {
        var penExcess = new Pen(new SolidBrush(colorControlExcessLine.Color), (float)(trackBarLineWidth.Value * Resolution / 2000f));
        var diag = Resolution * Math.Sqrt(graphicsBox.ClientSize.Width * graphicsBox.ClientSize.Width + graphicsBox.ClientSize.Height * graphicsBox.ClientSize.Height) / 2;
        var font = new System.Drawing.Font("Tahoma", (float)(trackBarStrSize.Value / 8.0 * Resolution));
        var brush = new SolidBrush(colorControlString.Color);

        var Tau = numericBoxDetTilt.RadianValue - numericBoxSampleTilt.RadianValue;

        foreach (var g in FormMain.Crystal.VectorOfG_KikuchiLine)
        {
            double sinTheta = WaveLength * g.Length / 2, sin2Theta = sinTheta * sinTheta;

            Vector3DBase vec1 = FormMain.Crystal.RotationMatrix * g;

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

    #region 逆格子ベクトル, 菊池線, デバイリングの初期化
    //逆格子ベクトルを設定する
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
            FormMain.Crystal.VectorOfG_KikuchiLine =
            FormMain.Crystal.VectorOfG.Where(g => g.Length < numericBoxKikuchiThresholdOfLength.Value).OrderByDescending(g => g.Length).ToList();
        }
        else
        {
            var list = FormMain.Crystal.VectorOfG.OrderByDescending(g => g.RelativeIntensity).ToList();
            var max = Math.Min(numericBoxKikuchiThreadSholdOfStructureFactor.ValueInteger, FormMain.Crystal.VectorOfG.Length);
            while (max + 1 < FormMain.Crystal.VectorOfG.Length)
            {
                if (SymmetryStatic.CheckEquivalentPlanes(list[max - 1].Index, list[max].Index, FormMain.Crystal.Symmetry))
                    max++;
                else
                    break;
            }
            FormMain.Crystal.VectorOfG_KikuchiLine = list[0..max];
            FormMain.Crystal.VectorOfG_KikuchiLine.Reverse();
        }
    }
    #endregion

    private void button1_Click(object sender, EventArgs e)
    {
        SetVector();
        Draw();
    }

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
        double tilt = numericBoxSampleTilt.RadianValue, cosTilt = Math.Cos(tilt), sinTilt = Math.Sin(tilt);

        var monte = new MonteCarlo(Z, A, ρ, energy, tilt);

        //飛程計算ループ
        sw.Restart();
        var loop = 20_000;
        var bse = new (V3 pos, V3 vec, double e)[loop];
        Parallel.For(0, loop, i => bse[i] = monte.GetBackscatteredElectrons());

        toolStripStatusLabel1.Text = $"{sw.ElapsedMilliseconds} msec. ellapsed for {loop} backscattered electrons.";

        //ステレオネット描画
        if (radioButtonFrequency.Checked)
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Histogram;
        else if (radioButtonAverageEnergy.Checked)
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Average;
        else
            poleFigureControl.DrawingMode = PoleFigureControl2.DrawingModeEnum.Sigma;
        M3 rot = M3.CreateRotationX(tilt);
        poleFigureControl.Vectors = bse.Where(e => e.e > monte.ThresholdKev).Select(e => new V4(rot.Mult(e.vec), e.e)).ToArray();
    }

}
