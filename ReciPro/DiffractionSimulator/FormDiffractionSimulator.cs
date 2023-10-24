#region using
using BitMiracle.LibTiff.Classic;
using Crystallography;
using Crystallography.OpenGL;
using MathNet.Numerics;
using OpenTK;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReciPro.FormDiffractionSimulatorDynamicCompression;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using V3 = OpenTK.Vector3d;
#endregion

namespace ReciPro;

public partial class FormDiffractionSimulator : Form
{
    #region フィールド、プロパティ

    public FormMain formMain;
    public FormDiffractionSimulatorCBED FormDiffractionSimulatorCBED;
    public FormDiffractionSimulatorGeometry FormDiffractionSimulatorGeometry;
    public FormDiffractionSimulatorDynamicCompression FormDiffractionSimulatorDynamicCompression;
    public FormDiffractionSpotInfo FormDiffractionBeamTable;

    private GLControlAlpha glControl;

    #region 計算モード
    public enum CalcModes { Excitation, Kinematical, Dynamical }
    /// <summary>
    /// 計算モードを取得/設定
    /// </summary>
    public CalcModes CalcMode
    {
        get
        {
            if (radioButtonIntensityExcitation.Checked) return CalcModes.Excitation;
            else if (radioButtonIntensityKinematical.Checked) return CalcModes.Kinematical;
            else return CalcModes.Dynamical;
        }
        set
        {
            if (value == CalcModes.Excitation)
                radioButtonIntensityExcitation.Checked = true;
            else if (value == CalcModes.Kinematical)
                radioButtonIntensityKinematical.Checked = true;
            else
            {
                Source = WaveSource.Electron;
                radioButtonIntensityDynamical.Checked = true;
            }
        }
    }
    #endregion

    #region 光学モード
    public enum BeamModes { Parallel, PrecessionElectron, PrecessionXray, Convergence }
    /// <summary>
    /// 光学系を取得/設定
    /// </summary>
    public BeamModes BeamMode
    {
        get
        {
            if (radioButtonBeamParallel.Checked) return BeamModes.Parallel;
            else if (radioButtonBeamPrecessionElectron.Checked) return BeamModes.PrecessionElectron;
            else if (radioButtonBeamPrecessionXray.Checked) return BeamModes.PrecessionElectron;
            else return BeamModes.Convergence;
        }
        set
        {
            if (value == BeamModes.Parallel)
                radioButtonBeamParallel.Checked = true;
            else if (value == BeamModes.PrecessionXray)
            {
                Source = WaveSource.Xray;
                radioButtonBeamPrecessionXray.Checked = true;
            }
            else if (value == BeamModes.PrecessionElectron)
            {
                Source = WaveSource.Electron;
                radioButtonBeamPrecessionElectron.Checked = true;
            }
            else
            {
                Source = WaveSource.Electron;
                radioButtonBeamConvergence.Checked = true;
            }
        }
    }
    #endregion

    public double EwaldRadius => 1 / WaveLength;
    public double WaveLength { get => waveLengthControl.WaveLength; set => waveLengthControl.WaveLength = value; }
    public double Energy { get => waveLengthControl.Energy; set => waveLengthControl.Energy = value; }

    public WaveSource Source { get => waveLengthControl.WaveSource; set => waveLengthControl.WaveSource = value; }

    public double ExcitationError => numericBoxSpotRadius.Value;

    public double Thickness { get => numericBoxThickness.Value; set => numericBoxThickness.Value = value; }
    public int NumberOfDiffractedWaves { get => numericBoxNumOfBlochWave.ValueInteger; set => numericBoxNumOfBlochWave.Value = value; }

    private Font font => new("Tahoma", (float)(trackBarStrSize.Value * Resolution / 10.0));


    public bool DynamicCompressionMode { get; set; } = false;
    public List<double[]> DynamicCompression_SpotInformation = new();

    /*public double CameraLength1
    {
        set { FormDiffractionSimulatorGeometry.CameraLength1 = value; }
        get { return FormDiffractionSimulatorGeometry == null ? 0 : FormDiffractionSimulatorGeometry.CameraLength1; }
    }*/

    #region 検出器、表示画面のプロパティ
    /// <summary>
    /// 画面解像度 mm/pix
    /// </summary>
    public double Resolution
    {
        set
        {
            if (value > numericBoxResolution.Maximum)
                numericBoxResolution.Value = numericBoxResolution.Maximum;
            else if (value < numericBoxResolution.Minimum)
                numericBoxResolution.Value = numericBoxResolution.Minimum;
            else
                numericBoxResolution.Value = value;
            Draw();
        }
        get => numericBoxResolution.Value;
    }
    public int ClientWidth { get => numericBoxClientWidth.ValueInteger; set => numericBoxClientWidth.Value = value; }
    public int ClientHeight { get => numericBoxClientHeight.ValueInteger; set => numericBoxClientHeight.Value = value; }

    public double CameraLength2
    {
        set { FormDiffractionSimulatorGeometry.CameraLength2 = value; Draw(); }
        get => FormDiffractionSimulatorGeometry == null ? 0 : FormDiffractionSimulatorGeometry.CameraLength2;
    }

    public double Tau { set => FormDiffractionSimulatorGeometry.Tau = value; get => FormDiffractionSimulatorGeometry == null ? 0 : FormDiffractionSimulatorGeometry.Tau; }

    public double Phi { set => FormDiffractionSimulatorGeometry.Phi = value; get => FormDiffractionSimulatorGeometry == null ? 0 : FormDiffractionSimulatorGeometry.Phi; }

    public double CosTau => FormDiffractionSimulatorGeometry.CosTau;
    public double CosTauSquare => FormDiffractionSimulatorGeometry.CosTauSquare;
    public double SinTau => FormDiffractionSimulatorGeometry.SinTau;
    public double SinTauSquare => FormDiffractionSimulatorGeometry.SinTauSquare;
    public double CosPhi => FormDiffractionSimulatorGeometry.CosPhi;
    public double CosPhiSquare => FormDiffractionSimulatorGeometry.CosPhiSquare;
    public double SinPhi => FormDiffractionSimulatorGeometry.SinPhi;
    public double SinPhiSquare => FormDiffractionSimulatorGeometry.SinPhiSquare;

    /// <summary>
    /// (CosPhi, SinPhi, 0) の周りに Tau回転する行列
    ///  Cos2Phi * (1 - CosTau) + CosTau | CosPhi * SinPhi * (1 - CosTau)  |  SinPhi * SinTau
    ///  CosPhi * SinPhi * (1 - CosTau)  | Sin2Phi * (1 - CosTau) + CosTau | -CosPhi * SinTau
    /// -SinPhi * SinTau                 | cosPhi  * sinTau                |  CosTau 
    /// この行列をv＝(X,Y,CL2)に作用させると、検出器座標(X,Y)を実空間座標に変換できる。
    /// </summary>
    public Matrix3D DetectorRotation => FormDiffractionSimulatorGeometry == null ? new Matrix3D() : FormDiffractionSimulatorGeometry.DetectorRotation;


    public Matrix3D DetectorRotationInv => FormDiffractionSimulatorGeometry.DetectorRotationInv;
    /// <summary>
    /// 画像の中心。検出器(Detector)座標系(Foot原点)で表現
    /// </summary>
    public PointD Foot { get; set; } = new PointD(0, 0);

    #endregion

    /// <summary>
    /// コントロールイベントをスキップする
    /// </summary>
    public bool SkipEvent { get; set; } = false;


    private bool skipDrawing = false;
    /// <summary>
    /// 描画をスキップする (Draw関数に入ったらすぐに判定) (コントロールイベントをスキップする場合は、SkipEventを使う)
    /// </summary>
    public bool SkipDrawing { set { skipDrawing = value; if (!value) Draw(); } get => skipDrawing; }

    /// <summary>
    /// スポット位置などは計算するが、最終的なレンダリングを行うかどうか。
    /// </summary>
    public bool SkipRendering { get; set; } = false;

    private Size lastPanelSize { get; set; } = new Size(0, 0);

    /// <summary>
    /// このフラグがtrueの時は、計算をキャンセルする
    /// </summary>
    public bool CancelSetVector { get; set; } = false;

    #endregion

    #region コンストラクタ、ロード、クローズ

    public FormDiffractionSimulator()
    {
        InitializeComponent();
        this.SetStyle(ControlStyles.DoubleBuffer, true);
        this.SetStyle(ControlStyles.UserPaint, true);
        this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

        timerBlinkSpot.Tag = true;
        timerBlinkKikuchiLine.Tag = true;
        timerBlinkDebyeRing.Tag = true;
        timerBlinkScale.Tag = true;

        if (FormDiffractionSimulatorGeometry == null)
        {
            lastPanelSize = graphicsBox.ClientSize;

            FormDiffractionSimulatorGeometry = new FormDiffractionSimulatorGeometry { FormDiffractionSimulator = this };
            FormDiffractionSimulatorGeometry.comboBoxGradient.SelectedIndex = 0;
            FormDiffractionSimulatorGeometry.comboBoxScale1.SelectedIndex = 1;
            FormDiffractionSimulatorGeometry.comboBoxScale2.SelectedIndex = 0;
            FormDiffractionSimulatorGeometry.VisibleChanged += FormDiffractionSimulatorGeometry_VisibleChanged;

            FormDiffractionBeamTable = new FormDiffractionSpotInfo { FormDiffractionSimulator = this };

            FormDiffractionSimulatorDynamicCompression = new FormDiffractionSimulatorDynamicCompression { FormDiffractionSimulator = this };
        }

        if (FormDiffractionSimulatorCBED == null)
            FormDiffractionSimulatorCBED = new FormDiffractionSimulatorCBED
            {
                FormDiffractionSimulator = this,
                Owner = this
            };

    }

    //ロードされたとき
    public void FormElectronDiffraction_Load(object sender, EventArgs e)
    {
        comboBoxScaleColorScale.SelectedIndex = 0;
        comboBoxCenter.SelectedIndex = 0;
        splitContainer1.SplitterDistance = splitContainer1.Width / 2;
        splitContainer1.Panel2Collapsed = true;
        Draw();

        WaveLengthControl_WaveSourceChanged(sender, e);

        glControl = new GLControlAlpha(GLControlAlpha.FragShaders.ZSORT)
        {
            AllowMouseRotation = true,
            AllowMouseScaling = true,
            AllowMouseTranslating = true,
            Name = "glControlAxes",
            ProjectionMode = GLControlAlpha.ProjectionModes.Orhographic,
            ProjWidth = 15,
            //NodeCoefficient = 30,
            //MaxHeight = 1440,
            //MaxWidth = 2560,
            RotationMode = GLControlAlpha.RotationModes.Object,
            Dock = DockStyle.Fill,
            LightPosition = new V3(100, 100, 100),
            BorderStyle = BorderStyle.Fixed3D,
            BackgroundColor = colorControlBackGround.Color,
        };
        splitContainer1.Panel2.Controls.Add(glControl);
        glControl.BringToFront();

    }

    //クローズ
    private void FormElectronDiffraction_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
        formMain.toolStripButtonDiffractionSingle.Checked = false;
        this.Visible = false;
    }

    #endregion

    #region VisibleChanged
    private void FormDiffractionSimulatorGeometry_VisibleChanged(object sender, EventArgs e)
        => numericUpDownCamaraLength2.Enabled = !FormDiffractionSimulatorGeometry.Visible;

    //Visibleが変更されたとき
    private void FormElectronDiffraction_VisibleChanged(object sender, EventArgs e)
    {
        if (Visible)
        {
            Foot = new PointD(0, 0);
            SetVector();

            Draw();

            if (radioButtonBeamConvergence.Checked)
                FormDiffractionSimulatorCBED.Visible = true;

            tabControl.BringToFront();

            formMain.toolStripButtonDiffractionSingle.Checked = true;
        }
        else
        {
            FormDiffractionBeamTable.Visible = false;
            FormDiffractionSimulatorGeometry.Visible = false;
            FormDiffractionSimulatorCBED.Visible = false;
        }
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

    #region Draw関数。ここから、DrawScale, DrawKikuchiLine, DrawDebyeRing, DrawDiffractionSpotsが呼び出される。

    private void Draw(object sender, EventArgs e) => Draw();

    /// <summary>
    /// 逆空間描画関数
    /// </summary>
    /// <param name="g">Graphicsオブジェクトを指定</param>
    /// <param name="drawLabel">ラベルを書く時は、true</param>
    /// <param name="drawOverlappedImage">オーバーラップイメージを描く時はtrue. ただし、trueでも、画像がセットされていない場合は描かない　</param>
    public void Draw(Graphics g = null, bool drawLabel = true, bool drawOverlappedImage = true)
    {
        if (this.InvokeRequired)//別スレッドから呼び出されたとき Invokeして呼びなおす
        {
            this.Invoke(new Action(() => Draw(g, drawLabel, drawOverlappedImage)), null);
            return;
        }

        if (SkipDrawing) return;

        var sw = new Stopwatch();
        sw.Start();

        if (formMain == null || formMain.Crystal == null || FormDiffractionSimulatorGeometry == null || formMain.Crystal.A * formMain.Crystal.B * formMain.Crystal.C == 0)
            return;

        if (g == null)//グラフィックスボックスに描画する場合
            g = graphicsBox.Graphics;

        if (!SetProjection(g))
            return;

        //背景を描画
        if (drawOverlappedImage)
        {
            var topleft = convertScreenToDetector(new Point(0, 0));
            var bottomright = convertScreenToDetector(new Point(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height));
            g.FillRectangle(new SolidBrush(colorControlBackGround.Color), new RectangleF((float)topleft.X, (float)topleft.Y, (float)(bottomright.X - topleft.X), (float)(bottomright.Y - topleft.Y)));
        }

        g.SmoothingMode = SmoothingMode.None;

        g.InterpolationMode = InterpolationMode.NearestNeighbor;
        g.PixelOffsetMode = PixelOffsetMode.Half;

        var fdsg = FormDiffractionSimulatorGeometry;
        var start = new PointD(-fdsg.DetectorPixelSize * fdsg.FootX, -fdsg.DetectorPixelSize * fdsg.FootY);
        var end = new PointD(fdsg.DetectorPixelSize * (fdsg.DetectorWidth - fdsg.FootX), fdsg.DetectorPixelSize * (fdsg.DetectorHeight - fdsg.FootY));
        //画像の重ね合わせ
        if (drawOverlappedImage && fdsg.ShowDetectorArea && fdsg.OverlappedImage != null)
        {
            var cm = new ColorMatrix();//ColorMatrixオブジェクトの作成
            cm.Matrix00 = cm.Matrix11 = cm.Matrix22 = cm.Matrix44 = 1;
            cm.Matrix33 = fdsg.ImageOpacity;
            var ia = new ImageAttributes();//ImageAttributesオブジェクトの作成
            ia.SetColorMatrix(cm);  //ColorMatrixを設定する
            var dest = new PointF[] { start.ToPointF(), new PointF((float)end.X, (float)start.Y), new PointF((float)start.X, (float)end.Y) };//左上、右上、左下の順番
            g.DrawImage(fdsg.OverlappedImage, dest, new RectangleF(0, 0, fdsg.OverlappedImage.Width, fdsg.OverlappedImage.Height), GraphicsUnit.Pixel, ia);
        }

        //CBEDの重ね合わせ
        if (FormDiffractionSimulatorCBED.Visible && FormDiffractionSimulatorCBED.Disks != null)
        {
            foreach (var disk in FormDiffractionSimulatorCBED.Disks.Where(d => d.Bitmap != null))
            {
                start = disk.Center - disk.Size / 2;
                end = disk.Center + disk.Size / 2; ;
                var dest = new PointF[] { start.ToPointF(), new PointF((float)end.X, (float)start.Y), new PointF((float)start.X, (float)end.Y) };//左上、右上、左下の順番
                g.DrawImage(disk.Bitmap, dest, new RectangleF(0, 0, disk.PixelSize.Width, disk.PixelSize.Width), GraphicsUnit.Pixel);
            }
        }

        g.SmoothingMode = SmoothingMode.HighQuality;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        if (toolStripButtonScale.Checked && (bool)timerBlinkScale?.Tag)
            DrawScale(g);

        if (toolStripButtonKikuchiLines.Checked && (bool)timerBlinkKikuchiLine?.Tag)
            DrawKikuchiLine(g);

        if (toolStripButtonDebyeRing.Checked && (bool)timerBlinkDebyeRing?.Tag)
            DrawDebyeRing(g);

        if (toolStripButtonDiffractionSpots.Checked && (bool)timerBlinkSpot?.Tag)
            DrawDiffractionSpots(g, drawLabel);

        //検出器の枠線
        if (fdsg.ShowDetectorArea)
        {
            start = new PointD(-fdsg.DetectorPixelSize * fdsg.FootX, -fdsg.DetectorPixelSize * fdsg.FootY);
            end = new PointD(fdsg.DetectorPixelSize * (fdsg.DetectorWidth - fdsg.FootX), fdsg.DetectorPixelSize * (fdsg.DetectorHeight - fdsg.FootY));
            var pen = new Pen(Brushes.LightGreen, (float)Resolution);
            g.DrawRectangle(pen, (float)Math.Min(start.X, end.X), (float)Math.Min(start.Y, end.Y), (float)Math.Abs(start.X - end.X), (float)Math.Abs(start.Y - end.Y));
        }

        //マウスの選択範囲描画
        if (MouseRangingMode)
        {
            var pen = new Pen(Brushes.Gray, (float)Resolution) { DashStyle = DashStyle.Dash };
            var rangeStart = convertScreenToDetector(MouseRangeStart).ToPointF();
            var rangeEnd = convertScreenToDetector(MouseRangeEnd).ToPointF();
            g.DrawRectangle(pen, Math.Min(rangeStart.X, rangeEnd.X), Math.Min(rangeStart.Y, rangeEnd.Y), Math.Abs(rangeStart.X - rangeEnd.X), Math.Abs(rangeStart.Y - rangeEnd.Y));
        }

        //対物絞りの範囲を示す円
        if (formMain.toolStripButtonImageSimulator.Checked && formMain.FormImageSimulator.ImageMode == FormImageSimulator.ImageModes.HRTEM
            && !double.IsInfinity(formMain.FormImageSimulator.ObjAperRadius))
        {
            var aperR = CameraLength2 * Math.Tan(formMain.FormImageSimulator.ObjAperRadius);
            var aperX = CameraLength2 * Math.Tan(formMain.FormImageSimulator.ObjAperX);
            var aperY = CameraLength2 * Math.Tan(formMain.FormImageSimulator.ObjAperY);

            var pen = new Pen(Brushes.LightGreen, (float)Resolution);
            g.DrawEllipse(pen, (float)(aperX - aperR), (float)(-aperY - aperR), (float)(aperR * 2), (float)(aperR * 2));
        }

        graphicsBox.Refresh();

        toolStripStatusLabelTimeForDrawing.Text = $"Time for drawing objects: {sw.ElapsedMilliseconds} ms.  ";

        if (FormDiffractionBeamTable.Visible)
        {
            if (radioButtonIntensityDynamical.Checked)
                FormDiffractionBeamTable.SetTable(Energy, formMain.Crystal.Bethe.Beams);
            else
                FormDiffractionBeamTable.SetTable(Energy, formMain.Crystal);
        }

        Draw3D();
    }

    /// <summary>
    /// 与えられた点集合 pts の中から、もっとも指定した方向に近い点を返す. deg 0 : 右, deg 90: 下, deg 180: 左, deg -90:上
    /// </summary>
    /// <param name="list"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    private static PointD getLabelPosition(IEnumerable<PointD> list, PointD origin, double deg)
    {
        var residual = double.PositiveInfinity;
        var result = new PointD(float.NaN, float.NaN);
        foreach (var p in list)
        {
            var dev = Math.Abs((deg / 180) * Math.PI - Math.Atan2(p.Y - origin.Y, p.X - origin.X));
            if (residual > dev)
            {
                residual = dev;
                result = p;
            }
        }
        return result;
    }
    #endregion

    #region DrawDiffractionSpots

    /// <summary>
    /// 回折スポットおよび指数ラベルの描画
    /// </summary>
    /// <param name="graphics">描画対象のグラフィックオブジェクト</param>
    /// <param name="drawLabel">ラベルを描画するかどうか</param>
    /// <param name="outputOnlySpotInformation">このフラグがTrueの場合は、画面描画は行わずにspotの情報だけを返す</param>
    public (double X, double Y, double Intensity, double Sigma)[] DrawDiffractionSpots(Graphics graphics, bool drawLabel = true, bool outputOnlySpotInformation = false)
    {
        if (radioButtonPointSpread.Checked && graphics != null)
            graphics.SmoothingMode = SmoothingMode.None;

        var spotInformation = new List<(double X, double Y, double Intensity, double Sigma)>();

        var alphaCoeff = (double)trackBarSpotOpacity.Value / trackBarSpotOpacity.Maximum;

        bool logScale = checkBoxLogScale.Checked;

        var radiusCBED = Math.Tan(FormDiffractionSimulatorCBED.AlphaMax) * CameraLength2;

        #region ガウス関数で描画するローカル関数
        //int bgR = colorControlBackGround.Color.R, bgG = colorControlBackGround.Color.G, bgB = colorControlBackGround.Color.B;
        int gradation = 32;
        double fillCircleSpread(Color c, PointD pt, double intensity, double sigma)
        {
            //計算する二次元ガウス関数は、 f(x,y) = intensity/ (2 pi sigma^2) *  e^{- (x^2+y^2) /2/sigma^2)
            //intensityはスポットの積分強度、sは半値幅

            double sigma2 = sigma * sigma, coeff1 = 1 / (2 * Math.PI * sigma2);

            var maxI = intensity * coeff1;
            if (maxI <= 1.0 / gradation) return 0;//もし、最大強度が1/gradiationより小さかったら、何もせずに戻る

            double minRadius = 0;

            if (maxI > 1)//もし中心付近が飽和する場合(強度が1以上)は、強度が　1/gradiation ～ 1 の半径範囲をgradationで分割
                minRadius = sigma * Math.Sqrt(-2 * Math.Log(2 * Math.PI * sigma2 / intensity));

            var maxRadius = sigma * Math.Sqrt(-2 * Math.Log(1 / coeff1 / intensity * (1.0 / gradation))) * 1.5;//強度が　1/2*gradiation　になる半径を求める

            //minRからmaxRまで、円を描画
            for (int j = 0; j < gradation; j++)
            {
                double ratio1 = (double)(j + 1) / gradation, ratio2 = (double)(j + 2) / gradation;
                double radius1 = ratio1 * minRadius + (1 - ratio1) * maxRadius, radius2 = ratio2 * minRadius + (1 - ratio2) * maxRadius;

                var intensity2 = intensity * coeff1 * Math.Exp(-(radius1 * radius1) / 2 / sigma2);

                var alpha = (int)(255 * intensity2 * alphaCoeff);
                if (comboBoxScaleColorScale.SelectedIndex != 0)
                {
                    var index = Math.Min((int)(intensity2 * 65535), 65535);
                    c = comboBoxScaleColorScale.SelectedIndex switch
                    {
                        1 => Color.FromArgb(PseudoBitmap.ColorScaleColdWarmLiner[index].R, PseudoBitmap.ColorScaleColdWarmLiner[index].G, PseudoBitmap.ColorScaleColdWarmLiner[index].B),
                        2 => Color.FromArgb(PseudoBitmap.ColorScaleSpectrumLiner[index].R, PseudoBitmap.ColorScaleSpectrumLiner[index].G, PseudoBitmap.ColorScaleSpectrumLiner[index].B),
                        _ => Color.FromArgb(PseudoBitmap.ColorScaleFireLiner[index].R, PseudoBitmap.ColorScaleFireLiner[index].G, PseudoBitmap.ColorScaleFireLiner[index].B),
                    };
                    alpha = 255;
                }

                var brush = new SolidBrush(Color.FromArgb(alpha, c));
                if (j < gradation - 1 && radius2 > 0)
                {
                    var path = new GraphicsPath();
                    path.AddArc((float)(pt.X - radius1), (float)(pt.Y - radius1), (float)(radius1 * 2), (float)(radius1 * 2), 0, 360);
                    path.AddArc((float)(pt.X - radius2), (float)(pt.Y - radius2), (float)(radius2 * 2), (float)(radius2 * 2), 0, -360);
                    graphics.FillPath(brush, path);
                }
                else
                {
                    graphics.FillEllipse(brush, (float)(pt.X - radius1), (float)(pt.Y - radius1), (float)(2 * radius1), (float)(2 * radius1));
                    return maxRadius;
                }
            }
            return maxRadius;
        }
        #endregion

        #region 3次元ガウス関数のメモ書き

        /*  次の関数
        I/ { s^3 * (2 pi)^(3/2)} * exp{ -(x^2+y^2+z^2) /(2 s^2) }
        は、積分値が I で、σがsの3次元ガウス関数である。

        z=Z の平面で切り取った断面は、
         I/ { s^3 * (2 pi)^(3/2)} * exp{ -Z^2 /(2 s^2)} * exp{ -(x^2+y^2) /(2 s^2) }
        という形になる。
        これは、二次元積分値が I/{ s * (2pi)^(1/2)} * exp{ -Z^2 /(2 s^2)} で σがsの二次元ガウス関数と等しい  */

        #endregion 3次元ガウス関数のメモ書き

        var spotRadiusOnDetector = CameraLength2 * Math.Tan(2 * Math.Asin(WaveLength * ExcitationError / 2));
        var spotRadiusOnDetectorF = (float)spotRadiusOnDetector;
        var error2 = ExcitationError * ExcitationError;
        var sqrtTwoPI = Math.Sqrt(2 * Math.PI);
        var linearCoeff = Math.Pow(trackBarIntensityForPointSpread.Value / 400.0, 6) * 100;
        var logCoeff = 16.0 * trackBarIntensityForPointSpread.Value / trackBarIntensityForPointSpread.Maximum;

        if (Source == WaveSource.Xray)
        {
            linearCoeff *= 1000;
            logCoeff *= 1000000;
        }

        var bethe = radioButtonIntensityDynamical.Checked;
        var sw = new Stopwatch();
        foreach (var crystal in formMain.Crystals)
        {
            List<Vector3D> gVector;

            if (bethe)//ベーテ法による動力学回折の場合
            {
                sw.Start();

                var blochNum = FormDiffractionSimulatorCBED.Visible ? FormDiffractionSimulatorCBED.MaxNumOfBloch : numericBoxNumOfBlochWave.ValueInteger;

                if (radioButtonBeamPrecessionElectron.Checked)//電子プリセッションの場合
                {
                    var eigenValues = crystal.Bethe.EigenValuesPED;

                    var gPED = crystal.Bethe.GetPrecessionElectronDiffraction(blochNum, Energy, crystal.RotationMatrix, numericBoxThickness.Value,
                        numericBoxPED_Semiangle.Value / 1000, numericBoxPED_Step.ValueInteger);
                    gVector = gPED.Select(g => g.ConvertToVector3D()).ToList();

                    if (eigenValues == null || eigenValues[0] != crystal.Bethe.EigenValuesPED[0])
                        toolStripStatusLabelTimeForBethe.Text = $"Time for solving dynamic effects (PED): {sw.ElapsedMilliseconds} ms.  ";
                }
                else//パラレルかCBEDの場合
                {
                    var eigenValues = crystal.Bethe.EigenValues;

                    var gBethe = crystal.Bethe.GetDifractedBeamAmpriltudes(blochNum, Energy, crystal.RotationMatrix, numericBoxThickness.Value);
                    gVector = gBethe.Select(g => g.ConvertToVector3D()).ToList();

                    if (eigenValues != crystal.Bethe.EigenValues)
                        toolStripStatusLabelTimeForBethe.Text = $"Time for solving dynamic effects: {sw.ElapsedMilliseconds} ms.  ";
                }
                var max = gVector.Max(g => double.IsInfinity(g.d) ? 0 : g.RawIntensity);
                //gVector = gVector.Select(g => { g.RelativeIntensity = g.RawIntensity / max; return g; }).ToList();//20220915以下に変更。合ってるかな。
                gVector.ForEach(g => g.RelativeIntensity = g.RawIntensity / max);

                if (formMain.Crystals.Length == 1)
                    foreach (var g in CollectionsMarshal.AsSpan(gVector))
                    {
                        var ext = crystal.Symmetry.CheckExtinctionRule(g.Index);
                        g.Argb = ext.Length == 0 ? colorControlNoCondition.Argb : colorControlScrewGlide.Argb;
                    }
                else
                    foreach (var g in CollectionsMarshal.AsSpan(gVector))
                        g.Argb = crystal.Argb;
            }
            else
                gVector = crystal.VectorOfG.ToList();


            //もしdyamicalな計算で、SkipRenderingがtrueの時はここでおしまい
            if (SkipRendering && bethe)
                gVector.Clear();


            gVector.ForEach(g => g.Flag2 = false);

            //描画するスポットを決める

            foreach (var g in gVector.Where(g => g.Flag1))
            {
                var vec = bethe ? g : crystal.RotationMatrix * g;//ベーテ法で計算する際には、すでに回転後の座標になっている。

                //逆空間 <=>実空間で、Y,Zの符号が反転していることに注意
                if (-vec.Z < (radioButtonPointSpread.Checked ? 3 * ExcitationError : ExcitationError))
                {
                    double L2 = (vec.X * vec.X) + (vec.Y * vec.Y), dev = 0.0;

                    if (radioButtonBeamPrecessionXray.Checked)//X線プリセッションの時
                        g.Tag = dev = -vec.Z;
                    else if (!bethe)//動力学電子回折ではないとき
                        g.Tag = dev = EwaldRadius - Math.Sqrt(L2 + (-vec.Z + EwaldRadius) * (-vec.Z + EwaldRadius));

                    var dev2 = dev * dev;

                    if (SinPhi * SinTau * vec.X + CosPhi * SinTau * vec.Y + CosTau * (-vec.Z + EwaldRadius) > 0)
                    //(vec.X, -vec.Y, -vec.Z + EwaldRadius) と(SinPhi*SinTau, -CosPhi*sinTau, cosTau) の内積が0より大きい = 前方散乱)
                    {
                        var pt = convertReciprocalToDetector(vec);

                        if (IsScreenArea(pt))
                        {
                            //CBEDモードの時,黄色いガイドサークルを表示
                            if (FormDiffractionSimulatorCBED.Visible)
                            {
                                if (FormDiffractionSimulatorCBED.DrawGuideCircles)
                                {
                                    if (!FormDiffractionSimulatorCBED.LACBED || g.Index == (0, 0, 0))
                                        graphics.DrawCircle(Color.Yellow, pt, radiusCBED);
                                    else
                                        graphics.DrawCross(new Pen(Color.Yellow, (float)Resolution), pt, spotRadiusOnDetector);
                                }
                            }
                            //ダイナミックコンプレッションモードがONの時は、描画しないで強度と座標だけを格納する
                            else if (outputOnlySpotInformation && IsScreenArea(pt))
                            {
                                double sigma = spotRadiusOnDetector, sigma2 = sigma * sigma;
                                var intensity = g.RelativeIntensity / (sigma * sqrtTwoPI) * Math.Exp(-dev2 / error2 / 2);
                                if (intensity > 1E-10)
                                    spotInformation.Add((pt.X, pt.Y, intensity, sigma));
                            }
                            //点広がり関数で描画するとき
                            else if (radioButtonPointSpread.Checked)
                            {
                                if (bethe || Math.Abs(dev) < 3 * ExcitationError)
                                {
                                    g.Flag2 = true;
                                    //もしg.RelativeIntensity=1で、かつcoeff=1の時、sigmaの半分のところで強度が255になるように関数の形を調整
                                    double sigma = spotRadiusOnDetector, sigma2 = sigma * sigma, intensity;
                                    if (!logScale)
                                        intensity = bethe ?
                                            g.RelativeIntensity * linearCoeff :
                                            g.RelativeIntensity / (sigma * sqrtTwoPI) * Math.Exp(-dev2 / error2 / 2) * linearCoeff;
                                    else
                                        intensity = bethe ?
                                            (Math.Log10(g.RelativeIntensity) + logCoeff) :
                                            (Math.Log10(g.RelativeIntensity) + logCoeff) / (sigma * sqrtTwoPI) * Math.Exp(-dev2 / error2 / 2);

                                    if (!double.IsNaN(intensity))
                                    {
                                        var radius = fillCircleSpread(Color.FromArgb(g.Argb), pt, intensity, sigma);
                                        if (drawLabel && trackBarStrSize.Value != 1 && intensity / (2 * Math.PI * sigma * sigma) > 0.5)
                                            DrawDiffractionSpotsLabel(graphics, g, pt, radius, (double)g.Tag);
                                    }
                                }
                            }
                            //円で塗りつぶすとき
                            else
                            {
                                //逆空間における逆格子点の半径
                                var sphereRadius = bethe ?
                                    ExcitationError * Math.Sqrt(g.RelativeIntensity) ://ベーテ法の場合は、相対強度の平方根が半径に比例
                                    ExcitationError * Math.Pow(g.RelativeIntensity, 1.0 / 3.0);//excitaion only あるいは Kinematicの場合は、半径に相対強度の1/3乗を掛ける

                                if (bethe || Math.Abs(dev) < sphereRadius)
                                {
                                    g.Flag2 = true;
                                    var sectionRadius = bethe ?
                                        sphereRadius : //ベーテ法の場合はそのまま
                                        Math.Sqrt(sphereRadius * sphereRadius - dev2);//excitaion only あるいは Kinematicの場合は、エワルド球に切られた断面上の、逆格子点の半径
                                    var r = CameraLength2 * WaveLength * sectionRadius;
                                    graphics.FillCircle(Color.FromArgb(g.Argb), pt, r, (int)(alphaCoeff * 255));
                                    if (drawLabel && trackBarStrSize.Value != 1 && r > spotRadiusOnDetector * 0.4f)
                                        DrawDiffractionSpotsLabel(graphics, g, pt, r, (double)g.Tag);
                                }
                            }
                        }
                    }
                }
            }
        }
        if (outputOnlySpotInformation)
            return spotInformation.ToArray();

        graphics.SmoothingMode = SmoothingMode.HighQuality;

        if (FormDiffractionSimulatorCBED.Visible)//CBEDモードの時は、ここでキャンセル
            return null;

        //ダイレクトスポットの描画
        var ptOrigin = convertReciprocalToDetector(new Vector3DBase(0, 0, 0));
        if (IsScreenArea(ptOrigin))
        {
            graphics.DrawCross(new Pen(colorControlOrigin.Color, (float)Resolution), ptOrigin, spotRadiusOnDetector);
            if (toolStripButtonIndexLabels.Checked && trackBarStrSize.Value != 1 && !radioButtonIntensityDynamical.Checked)
                graphics.DrawString("0 0 0", font, new SolidBrush(Color.FromArgb((int)(alphaCoeff * 255), colorControlOrigin.Color)), (float)(ptOrigin.X + spotRadiusOnDetectorF / 2f), (float)(ptOrigin.Y + spotRadiusOnDetectorF / 2f));
        }
        //垂線の足の描画
        if (Tau != 0 && IsScreenArea(new PointD(0, 0)))
        {
            graphics.DrawCross(new Pen(colorControlFoot.Color, (float)Resolution), 0, 0, spotRadiusOnDetector);
            if (toolStripButtonIndexLabels.Checked && trackBarStrSize.Value != 1)
                graphics.DrawString("foot", font, new SolidBrush(Color.FromArgb((int)(alphaCoeff * 255), colorControlFoot.Color)), spotRadiusOnDetectorF / 2f, spotRadiusOnDetectorF / 2f);
        }
        return null;
    }
    #endregion

    #region DrawDiffractionSpostLabel

    public void DrawDiffractionSpotsLabel(Graphics graphics, Vector3D g, PointD pt, double radius, double error)
    {
        double alphaCoeff = (double)trackBarSpotOpacity.Value / trackBarSpotOpacity.Maximum;
        var sb = new StringBuilder();
        if (toolStripButtonIndexLabels.Checked) sb.AppendLine(g.Text);
        if (toolStripButtonDspacing.Checked) sb.AppendLine($"{g.d * 10:#.###} Å");
        if (toolStripButtonDspacingInv.Checked) sb.AppendLine($"{1 / g.d:#.###} /nm");
        if (toolStripButtonDistance.Checked) sb.AppendLine($"{CameraLength2 * Math.Tan(2 * Math.Asin(WaveLength / g.d / 2)):#.###} mm");
        if (toolStripButtonExcitationError.Checked) sb.AppendLine($"{error:f3} /nm");

        if (toolStripButtonFg.Checked)
        {
            if (radioButtonIntensityKinematical.Checked)
                sb.AppendLine($"{g.RelativeIntensity * 100:#.#} %");
            if (radioButtonIntensityDynamical.Checked)
                sb.AppendLine($"{g.RelativeIntensity * 100:#.#} %, ({g.F.Real:0.###} + {g.F.Imaginary:0.###}i)");
        }

        var brush = new SolidBrush(Color.FromArgb((int)(alphaCoeff * 255), colorControlString.Color));
        graphics.DrawString(sb.ToString(), font, brush, (float)(pt.X + radius / 1.4142 + 3 * Resolution), (float)(pt.Y + radius / 1.4142 + 3 * Resolution));
    }

    #endregion

    #region DrawKikuchiLine

    private void DrawKikuchiLine(Graphics graphics)
    {
        var penExcess = new Pen(new SolidBrush(colorControlExcessLine.Color), (float)(trackBarLineWidth.Value * Resolution / 2000f));
        var penDefect = new Pen(new SolidBrush(colorControlDefectLine.Color), (float)(trackBarLineWidth.Value * Resolution / 2000f));
        var diag = Resolution * Math.Sqrt(graphicsBox.ClientSize.Width * graphicsBox.ClientSize.Width + graphicsBox.ClientSize.Height * graphicsBox.ClientSize.Height) / 2;

        foreach (var crystal in formMain.Crystals)
            foreach (var g in crystal.VectorOfG_KikuchiLine)
            {
                double sinTheta = WaveLength * g.Length / 2, sin2Theta = sinTheta * sinTheta;

                Vector3DBase vec1 = crystal.RotationMatrix * g;

                //bool excess は、excess の時に+1, そうでないときはfalse
                int sign = Math.Abs(vec1.Z) < 1E-4 ? 0 : (vec1.Z > 0 ? 1 : -1);

                //vec2は、検出器法線がZ軸と一致するようにX軸を回転軸に回転させたベクトル
                var vec2 = Matrix3D.Rot(new Vector3DBase(1, 0, 0), Tau) * vec1;

                //vec3は、検出器法線(Z軸)を軸としてpsiだけ回転させて、(0,y,z)の形になるようにしたベクトル
                var psi = Math.Atan2(vec2.X, vec2.Y);
                var vec3 = Matrix3D.Rot(new Vector3DBase(0, 0, 1), psi) * vec2;

                //vec3normは、vec3を規格化したベクトル
                var vec3norm = vec3.Normarize();
                double sinPhi = vec3norm.Y, sin2Phi = sinPhi * sinPhi;
                double cosPhi = vec3norm.Z, cos2Phi = cosPhi * cosPhi;

                double P = (sin2Phi - sin2Theta) / (CameraLength2 * CameraLength2 * (1 - sin2Theta)), Psqrt = Math.Sqrt(P);
                double Q = P * (sin2Phi - sin2Theta) / sin2Theta, Qsqrt = Math.Sqrt(Q);
                double Y = CameraLength2 * sinPhi * cosPhi / (sin2Phi - sin2Theta);

                //現在のMatrixを保存
                var original = graphics.Transform;

                graphics.RotateTransform((float)(psi / Math.PI * 180));
                graphics.TranslateTransform(0, -(float)Y);

                if (!double.IsNaN(Psqrt) && !double.IsNaN(Qsqrt))
                {
                    // y= sinh(x) の逆関数は x = log{y+ sqrt(y*y+1)}
                    double omegaMax = Math.Log(diag * Psqrt + Math.Sqrt(diag * Psqrt * diag * Psqrt + 1)) * 2;
                    List<PointF> pts1 = new(), pts2 = new();
                    for (double omega = -omegaMax; omega < omegaMax; omega += omegaMax / 500)
                    {
                        float x = (float)(Math.Sinh(omega) / Psqrt), y = (float)(Math.Cosh(omega) / Qsqrt);
                        pts1.Add(new PointF(x, y));
                        pts2.Add(new PointF(x, -y));
                    }
                    try
                    {
                        if (!checkBoxKikuchiLine_Kinematical.Checked)
                            _draw(sign, pts1, pts2, g.Text);
                        else if (g.RelativeIntensity > 0)
                        {
                            penExcess.Color = Blend(colorControlExcessLine.Color, colorControlBackGround.Color, g.RelativeIntensity);
                            penDefect.Color = Blend(colorControlDefectLine.Color, colorControlBackGround.Color, g.RelativeIntensity);
                            _draw(sign, pts1, pts2, g.Text);
                        }
                    }
                    catch { }
                }

                graphics.Transform = original;
            }
        //菊池線とラベルを描画するローカル関数
        void _draw(int sign, List<PointF> pts1, List<PointF> pts2, string label)
        {
            // if (sign == 0)
            {
                graphics.DrawLines(penDefect, pts1.Select(p => new PointF(p.X, p.Y + (float)Resolution)).ToArray());
                graphics.DrawLines(penDefect, pts2.Select(p => new PointF(p.X, p.Y - (float)Resolution)).ToArray());
                graphics.DrawLines(penExcess, pts1.Select(p => new PointF(p.X, p.Y - (float)Resolution)).ToArray());
                graphics.DrawLines(penExcess, pts2.Select(p => new PointF(p.X, p.Y + (float)Resolution)).ToArray());
            }
            //else
            //{
            //    graphics.DrawLines(sign > 0 ? penExcess : penDefect, pts1.ToArray());
            //    graphics.DrawLines(sign > 0 ? penDefect : penExcess, pts2.ToArray());
            //}
            //if (toolStripButtonIndexLabels.Checked)
            //    graphics.DrawString(label, font, new SolidBrush(colorControlString.Color), sign > 0 ? pts1[pts1.Count / 2]: pts2[pts2.Count / 2]);
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

    #region DrawDebyeRing
    private void DrawDebyeRing(Graphics g)
    {
        int width = graphicsBox.ClientSize.Width, height = graphicsBox.ClientSize.Height;
        if (width == 0 || height == 0)
            return;

        var cornerDetector = new[] { convertScreenToDetector(0, 0), convertScreenToDetector(width, 0), convertScreenToDetector(width, height), convertScreenToDetector(0, height) };
        var originSrc = convertReciprocalToDetector(new Vector3DBase(0, 0, 0));

        int bgR = colorControlBackGround.Color.R, bgG = colorControlBackGround.Color.G, bgB = colorControlBackGround.Color.B;
        int ringR = colorControlDebyeRing.Color.R, ringG = colorControlDebyeRing.Color.G, ringB = colorControlDebyeRing.Color.B;
        for (int n = 0; n < formMain.Crystal.Plane.Count; n++)
        {
            var intensity = formMain.Crystal.Plane[n].Intensity;
            if (checkBoxDebyeRingIgnoreIntensity.Checked)
                intensity = 1;

            var twoTheta = 2 * Math.Asin(WaveLength / 2 / formMain.Crystal.Plane[n].d);

            var ptsArray = Geometriy.ConicSection(twoTheta, Phi, Tau, CameraLength2, cornerDetector[0], cornerDetector[2]);

            var red = (int)(ringR * intensity + bgR * (1 - intensity));
            var green = (int)(ringG * intensity + bgG * (1 - intensity));
            var blue = (int)(ringB * intensity + bgB * (1 - intensity));
            var pen = new Pen(new SolidBrush(Color.FromArgb(red, green, blue)), (float)(trackBarDebyeRingWidth.Value * Resolution / 2f));

            foreach (var pts in ptsArray)
                g.DrawLines(pen, pts.ToArray());

            var labelPosition = getLabelPosition(ptsArray.SelectMany(p => p).Where(p => IsScreenArea(p, 5)), originSrc, -90);
            if (checkBoxDebyeRingLabel.Checked && !double.IsNaN(labelPosition.X))
                g.DrawString("{" + formMain.Crystal.Plane[n].strHKL.Replace(" + ", "} + {") + "}", font, new SolidBrush(colorControlString.Color), labelPosition.ToPointF());
        }
    }
    #endregion

    #region DrawScale
    private void DrawScale(Graphics g)
    {
        int width = graphicsBox.ClientSize.Width, height = graphicsBox.ClientSize.Height;
        if (width == 0 || height == 0)
            return;

        var cornerDetector = new[] { convertScreenToDetector(0, 0), convertScreenToDetector(width, 0), convertScreenToDetector(width, height), convertScreenToDetector(0, height) };
        var cornerReals = new[] { convertScreenToReal(0, 0), convertScreenToReal(width, 0), convertScreenToReal(width, height), convertScreenToReal(0, height) };
        var originSrc = convertReciprocalToDetector(new Vector3DBase(0, 0, 0));
        var originInside = IsScreenArea(originSrc);

        //Azimuthのスケールライン ここから
        int azimuthStep = radioButtonScaleDivisionFine.Checked ? 5 : radioButtonScaleDivisionMedium.Checked ? 15 : 30;
        var pen = new Pen(new SolidBrush(colorControlScaleAzimuth.Color), (float)(trackBarScaleLineWidth.Value * Resolution / 2f));

        var length = new[] { (cornerReals[0]- cornerReals[1]).Length, (cornerReals[1] - cornerReals[2]).Length,
                (cornerReals[2] - cornerReals[3]).Length, (cornerReals[3] - cornerReals[0]).Length };

        for (double n = 0; n < 180; n += azimuthStep)
        {
            pen.DashStyle = n % 10 == 0 ? DashStyle.Dash : DashStyle.Dot;

            var crossList = new List<(PointD pt, int index)>();
            double cos = Math.Cos(n / 180.0 * Math.PI), sin = Math.Sin(n / 180.0 * Math.PI);
            //n度傾いた平面と、画像のエッジの交点を求める
            for (int i = 0; i < 4; i++)
            {
                //  0 - 1
                //  |   |
                //  3 - 2 
                var j = i < 3 ? i + 1 : 0;
                var cross = Geometriy.GetCrossPoint(cos, sin, 0, 0, cornerReals[i], cornerReals[j]);
                double length1 = (cornerReals[i] - cross).Length, length2 = (cornerReals[j] - cross).Length;
                if (length1 + length2 < length[i] * 1.001)
                    crossList.Add((length1 / length[i] * cornerDetector[j] + length2 / length[i] * cornerDetector[i], i));
                if (crossList.Count == 2)
                {
                    g.DrawLine(pen, crossList[0].pt.ToPointF(), crossList[1].pt.ToPointF());

                    if (checkBoxScaleLabel.Checked)//ラベル描画
                    {
                        if (!originInside)//ダイレクトスポットが描画範囲内に含まれていないときは 中心に近い点は削除
                            crossList.Remove((crossList[0].pt - originSrc).Length > (crossList[1].pt - originSrc).Length ?
                                crossList[1] : crossList[0]);

                        foreach (var (pt, index) in crossList)
                        {
                            double xx = pt.X - originSrc.X, yy = pt.X - originSrc.X;
                            var str = (xx > 1E-6) || (xx > -1E-6 && yy > 1E-6) ? n.ToString("g12") : (n - 180).ToString("g12");
                            var shift = new PointD(index == 1 ? -3 : 0, index == 2 ? -2 : 0) * font.Size;
                            g.DrawString(str + "°", font, new SolidBrush(colorControlScaleAzimuth.Color), (pt + shift).ToPointF());
                        }
                    }
                    break;
                }
            }
        }
        //Azimuthのスケールライン ここまで

        //ここから2θのスケールラインの描画

        //2θの最大/最小値
        double max2Theta = 0, min2Theta = 0.0;
        var edges = new List<Vector3DBase>();
        edges.AddRange(Enumerable.Range(0, width).Select(w => convertScreenToReal(w, 0)));
        edges.AddRange(Enumerable.Range(0, width).Select(w => convertScreenToReal(w, height)));
        edges.AddRange(Enumerable.Range(0, height).Select(h => convertScreenToReal(0, h)));
        edges.AddRange(Enumerable.Range(0, height).Select(h => convertScreenToReal(width, h)));
        if (!originInside)
            min2Theta = edges.Select(p => Math.Atan2(Math.Sqrt(p.X2Y2), p.Z)).Min() / Math.PI * 180.0;
        max2Theta = edges.Select(p => Math.Atan2(Math.Sqrt(p.X2Y2), p.Z)).Max() / Math.PI * 180.0;

        if (radioButtonBeamPrecessionXray.Checked)//X線プリセッションの場合
        {
            if (!originInside)
                min2Theta = edges.Select(p => 2 * Math.Asin(Math.Sqrt(p.X2Y2) / CameraLength2 / 2)).Min() / Math.PI * 180.0;
            max2Theta = edges.Select(p => 2 * Math.Asin(Math.Sqrt(p.X2Y2) / CameraLength2 / 2)).Max() / Math.PI * 180.0;
            if (double.IsNaN(min2Theta)) min2Theta = 0;
            if (double.IsNaN(max2Theta)) max2Theta = 175;
        }

        //2θの最大/最小値　ここまで

        //分割幅をきめる　ここから
        //fineのときは20分割以上、mediumは10分割以上、coarseは5分割以上になるように調節
        double dev = max2Theta - min2Theta;
        int thereshold = radioButtonScaleDivisionFine.Checked ? 30 : radioButtonScaleDivisionMedium.Checked ? 15 : 5;
        int stepInteger = 5, stepPow = 0;
        for (stepPow = (int)Math.Log10(dev) + 1; stepPow > -7; stepPow--)
        {
            if (dev / (stepInteger = 5) / Math.Pow(10, stepPow) > thereshold) break;
            if (dev / (stepInteger = 2) / Math.Pow(10, stepPow) > thereshold) break;
            if (dev / (stepInteger = 1) / Math.Pow(10, stepPow) > thereshold) break;
        }
        //分割幅をきめる　ここまで

        int startN = (int)(min2Theta / stepInteger / Math.Pow(10, stepPow));
        int endN = (int)(max2Theta / stepInteger / Math.Pow(10, stepPow)) + 1;

        pen.Brush = new SolidBrush(colorControlScale2Theta.Color);

        for (double n = Math.Max(1, startN); n < endN; n++)
        {
            var twoTheta = n * stepInteger * Math.Pow(10, stepPow);
            var ptsArray = Geometriy.ConicSection(twoTheta / 180 * Math.PI, Phi, Tau, CameraLength2, cornerDetector[0], cornerDetector[2]);
            if (radioButtonBeamPrecessionXray.Checked)//X線プリセッションの場合
            {
                ptsArray = new List<List<PointD>>();
                ptsArray.Add(Enumerable.Range(0, 3600).Select(i => 2 * CameraLength2 * Math.Sin(twoTheta / 360 * Math.PI) * new PointD(Math.Cos(i / 1800.0 * Math.PI), Math.Sin(i / 1800.0 * Math.PI))).ToList());
            }

            foreach (var pts in ptsArray)
                g.DrawLines(pen, pts.ToArray());

            var labelPosition = getLabelPosition(ptsArray.SelectMany(p => p).Where(p => IsScreenArea(p, 20)), originSrc, -135);
            if (checkBoxScaleLabel.Checked && !double.IsNaN(labelPosition.X))
                g.DrawString(twoTheta.ToString("g12") + "°", font, new SolidBrush(colorControlScale2Theta.Color), labelPosition.ToPointF());
        }
    }
    #endregion

    #region リサイズ関連イベント
    private void FormDiffractionSimulator_ResizeBegin(object sender, EventArgs e)
    {
        SuspendLayout();
    }

    private void FormElectronDiffraction_ResizeEnd(object sender, EventArgs e)
    {
        ResumeLayout();

        if (SkipEvent) return;

        if (graphicsBox.ClientSize.Width == 0 || graphicsBox.ClientSize.Height == 0) return; //最小化されたときなど
        SetVector();
        Draw();

        SkipEvent = true;
        numericBoxClientWidth.Value = graphicsBox.ClientSize.Width;
        numericBoxClientHeight.Value = graphicsBox.ClientSize.Height;
        SkipEvent = false;

        lastPanelSize = graphicsBox.ClientSize;

    }
    #endregion

    #region 一般的なイベント

    private void FormDiffractionSimulator_Paint(object sender, PaintEventArgs e) => Draw();

    //解像度が変更されたときに逆格子点を計算しなおす
    private void numericUpDownResolution_ValueChanged(object sender, EventArgs e)
    {
        if (Visible == false) return;
        SetProjection();
        SetVector();
        Draw();
    }

    /// <summary>
    /// 画像サイズのnumericBoxが変更されたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void NumericBoxClientSize_ValueChanged(object sender, EventArgs e)
    {
        if (SkipEvent) return;

        var dW = numericBoxClientWidth.ValueInteger - graphicsBox.ClientSize.Width;
        var dH = numericBoxClientHeight.ValueInteger - graphicsBox.ClientSize.Height;
        this.Size = new Size(this.Size.Width + dW, this.Size.Height + dH);

    }

    /// <summary>
    /// カメラ長2がこのフォームから変更されたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numericUpDownCamaraLength2_ValueChanged(object sender, EventArgs e)
    {
        if (!FormDiffractionSimulatorGeometry.Visible)
            FormDiffractionSimulatorGeometry.CameraLength2 = (double)numericUpDownCamaraLength2.Value;
    }

    private void buttonDetailedGeometry_Click(object sender, EventArgs e)
    {
        FormDiffractionSimulatorGeometry.Visible = true;
        FormDiffractionSimulatorGeometry.BringToFront();
    }


    private void checkBoxMousePositionDetailes_CheckedChanged(object sender, EventArgs e) => tableLayoutPanelMousePotionDetailed.Visible = checkBoxMousePositionDetailes.Checked;

    private void numericBoxKikuchiLineThreshold_ValueChanged(object sender, EventArgs e)
    {
        SetVector();
        Draw();
    }

    private void checkBoxKikuchiLine_Kinematical_CheckedChanged(object sender, EventArgs e)
    {
        Draw();
    }

    #endregion

    #region 逆格子ベクトルを初期化. 
    //逆格子ベクトルを設定する
    public void SetVector(bool renewCrystal = false)
    {
        if (formMain == null) return;
        if (CancelSetVector) return;
        var sw = new Stopwatch();
        sw.Start();

        var minD = new[] {
                1 / convertScreenToReciprocal(0, 0, false).Length,
                1 / convertScreenToReciprocal(0, graphicsBox.ClientSize.Height, false).Length,
                1 / convertScreenToReciprocal(graphicsBox.ClientSize.Width, 0, false).Length,
                1 / convertScreenToReciprocal(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height, false).Length
            }.Min();

        if (toolStripButtonDiffractionSpots.Checked)
        {
            if (toolStripMenuItemBackLaue.Checked)//Back Laueのとき
                minD = WaveLength / 2;

            foreach (var crystal in formMain.Crystals)
            {
                crystal.SetVectorOfG(minD, radioButtonIntensityKinematical.Checked ? Source : WaveSource.None);

                var latticeType = crystal.Symmetry.LatticeTypeStr;

                var noConditionColor = formMain.Crystals.Length == 1 && !checkBoxUseCrystalColor.Checked ? colorControlNoCondition.Color.ToArgb() : crystal.Argb;
                foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length == 0))
                {
                    gtemp.Flag1 = true;
                    gtemp.Argb = noConditionColor;
                }

                var latticeColor = colorControlForbiddenLattice.Color.ToArgb();
                foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length > 0 && g.Extinction[0] == latticeType))
                {
                    gtemp.Flag1 = !checkBoxExtinctionLattice.Checked;
                    gtemp.Argb = latticeColor;
                }

                var screwGlideColor = colorControlScrewGlide.Color.ToArgb();
                foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length > 0 && g.Extinction[0] != latticeType))
                {
                    gtemp.Flag1 = !checkBoxExtinctionAll.Checked;
                    gtemp.Argb = screwGlideColor;
                }
            }
        }

        if (toolStripButtonDebyeRing.Checked)
        {
            formMain.Crystal.SetPlanes(double.PositiveInfinity, minD, true, true, false, true, HorizontalAxis.d, 0.00000000, WaveLength);
            formMain.Crystal.SetPeakIntensity(Source, WaveColor.Monochrome, WaveLength, null);
            for (int j = 0; j < formMain.Crystal.Plane.Count; j++)
                if (formMain.Crystal.Plane[j].Intensity < 1E-6)
                    formMain.Crystal.Plane.RemoveAt(j--);
        }

        if (toolStripButtonKikuchiLines.Checked)
            formMain.Crystal.SetVectorOfG_KikuchiLine(numericBoxKikuchiLineThreshold.Value, Source);

        toolStripStatusLabelTimeForSearchingG.Text = $"Time for searching g-vectors: {sw.ElapsedMilliseconds} ms.  ";
    }
    #endregion

    #region 座標変換

    /// <summary>
    /// 座標変換 画面(Screen)上の点(pixel)を検出器(Detector)上の位置 (mm)に変換
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private PointD convertScreenToDetector(in int x, in int y) => new(
            (x - graphicsBox.ClientSize.Width / 2.0) * Resolution - Foot.X,
            (y - graphicsBox.ClientSize.Height / 2.0) * Resolution - Foot.Y);

    /// <summary>
    /// 座標変換 画面(Screen)上の点(pixel)を検出器(Detector)上の位置 (mm)に変換
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    private PointD convertScreenToDetector(in Point p) => convertScreenToDetector(p.X, p.Y);

    /// <summary>
    /// 座標変換 画面(Screen)上の点(pixel) を 実空間座標(mm, ３次元座標)に変換
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private Vector3DBase convertScreenToReal(in int x, in int y)
    {
        var p = convertScreenToDetector(x, y);//まずフィルム上の位置を取得
        return convertDetectorToReal(p);//実空間の座標に変換
    }

    /// <summary>
    /// 座標系変換 画面(Client)上の点(pixel) を 逆空間上の点(mm^-1)に変換 　回転している場合はOriginal座標系に戻して変換。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private Vector3DBase convertScreenToReciprocal(int x, int y, bool originalCoordinate)
        => convertRealToReciprocal(convertScreenToReal(x, y), originalCoordinate);

    /// <summary>
    /// フィルム(Src)上の位置 (mm)を座標系変換 画面(Client)上の点(pixel)に変換
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private PointD convertDetectorToScreen(in double x, in double y) => new(
            (x + Foot.X) / Resolution + graphicsBox.ClientSize.Width / 2.0,
            (y + Foot.Y) / Resolution + graphicsBox.ClientSize.Height / 2.0);

    /// <summary>
    /// 検出器(Detector)上の位置 (mm)を画面(Screen)上の点(pixel)に変換
    /// </summary>
    /// <param name="pt"></param>
    /// <returns></returns>
    private PointD convertDetectorToScreen(in PointD pt) => convertDetectorToScreen(pt.X, pt.Y);

    /// <summary>
    /// 座標変換 検出器(Detector)上の点(Foot中心, mm単位) を 実空間座標(mm単位, ３次元座標)に変換
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private Vector3DBase convertDetectorToReal(in double x, in double y) =>
    #region 座標変換の計算式
            // (CosPhi, SinPhi, 0) の周りに Tau回転する行列は、
            //   Cos2Phi * (1 - CosTau) + CosTau | CosPhi * SinPhi * (1 - CosTau)  |  SinPhi * SinTau
            //   CosPhi * SinPhi * (1 - CosTau)  | Sin2Phi * (1 - CosTau) + CosTau | -CosPhi * SinTau
            //  -SinPhi * SinTau                 | cosPhi  * sinTau                |  CosTau  
            //この行列を(x,y,CameraLength2)に作用させればよい
    #endregion
            radioButtonBeamPrecessionXray.Checked ? new Vector3DBase(x, y, CameraLength2) : DetectorRotation * new Vector3DBase(x, y, CameraLength2);

    private Vector3DBase convertDetectorToReal(in PointD pt) => convertDetectorToReal(pt.X, pt.Y);

    /// <summary>
    /// 実空間座標(mm単位, ３次元座標)を逆空間座標に変換
    /// </summary>
    /// <param name="v"></param>
    /// <param name="originalCoordinate"></param>
    /// <returns></returns>
    private Vector3DBase convertRealToReciprocal(Vector3DBase v, bool originalCoordinate)
    {
        if (radioButtonBeamPrecessionXray.Checked)
        {
            var rVec = new Vector3DBase(v.X, v.Y, 0) / CameraLength2 * EwaldRadius;
            return originalCoordinate ? formMain.Crystal.RotationMatrix.Inverse() * rVec : rVec;
        }
        else
        {
            var len = Math.Sqrt(v.X2Y2);
            var twoTheta = Math.Atan2(len, v.Z);

            double sinTheta = Math.Sin(twoTheta / 2), sinThetaSquare = sinTheta * sinTheta;
            var Z = EwaldRadius * (1 - Math.Cos(twoTheta));

            var temp = 1 / len * Math.Sqrt((4 * sinThetaSquare * EwaldRadius * EwaldRadius) - Z * Z);
            double X = v.X * temp, Y = -v.Y * temp;

            return originalCoordinate ? formMain.Crystal.RotationMatrix.Inverse() * new Vector3DBase(X, Y, Z) : new Vector3DBase(X, Y, Z);
        }
    }

    /// <summary>
    /// 逆空間座標を実空間座標に変換。　 逆空間座標のy,zの符号を反転することに注意
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    public Vector3DBase ConvertReciprocalToReal(Vector3DBase g)
        => Geometriy.GetCrossPoint(SinPhi * SinTau, -CosPhi * SinTau, CosTau, CameraLength2, new Vector3DBase(g.X, -g.Y, EwaldRadius - g.Z));

    // return p * d / (a * p.X + b * p.Y + c * p.Z);

    /// <summary>
    /// 逆空間座標を検出器座標に変換。　 逆空間座標のy,zの符号を反転することに注意
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    private PointD convertReciprocalToDetector(Vector3DBase g)
    {
        if (radioButtonBeamPrecessionXray.Checked)//X線プリセッション。　検出器の傾きは考慮しない。
        {
            if (g.X == 0 && g.Y == 0)
                return new PointD(0, 0);
            //Z=0平面 (=初期フィルム面)と、gのなす角をφとする
            var len = g.Length;
            var sinφ = g.Z / len;
            var cosφ = Math.Sqrt(1 - sinφ * sinφ);
            var sinθ = len / 2 / EwaldRadius;// 2d sinθ = 2/len sinθ = 1/EwaldRadius  ==> sinθ = len / 2 / EwaldRadius;
            var cosθ = Math.Sqrt(1 - sinθ * sinθ);

            var x = CameraLength2 / EwaldRadius * len * cosθ / (cosφ * cosθ - sinφ * sinθ);

            return new PointD(g.X, -g.Y) / Math.Sqrt(g.X2Y2) * x;
        }

        var v = DetectorRotationInv * new Vector3DBase(g.X, -g.Y, EwaldRadius - g.Z);
        var coeff = CameraLength2 / v.Z;
        return new PointD(v.X * coeff, v.Y * coeff);
    }


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

    #endregion 座標変換

    #region formMainから結晶を設定されたとき
    //formMainから結晶を設定されたとき
    internal void SetCrystal()
    {
        SetVector(true);
        Draw();
    }
    #endregion

    #region graphicsBoxのイベント

    private bool MouseRangingMode = false;
    private Point MouseRangeStart, MouseRangeEnd;//, startAnimation;
    private void graphicsBox_MouseDown(object sender, MouseEventArgs e)
    {
        graphicsBox.Focus();
        if (e.Button == MouseButtons.Right && e.Button != MouseButtons.Left)
        {
            MouseRangingMode = true;
            MouseRangeStart = new Point(e.X, e.Y);
            return;
        }
        else if (e.Button == MouseButtons.Left && e.Button != MouseButtons.Right && e.Clicks == 2)
        {
            //まずフィルム上の位置を逆空間点に変換
            var inversePos = convertScreenToReciprocal(e.X, e.Y, true);
            //座標を反転
            var gVector = formMain.Crystal.VectorOfG;
            int num = -1;
            var minLength = double.PositiveInfinity;
            for (int i = 0; i < gVector.Count; i++)
            {
                if (minLength > (gVector[i] - inversePos).Length2)
                {
                    minLength = (gVector[i] - inversePos).Length2;
                    num = i;
                }
            }

            var vec = formMain.Crystal.RotationMatrix * gVector[num];
            var dev = Math.Abs(EwaldRadius - Math.Sqrt(vec.X * vec.X + vec.Y * vec.Y + (vec.Z - EwaldRadius) * (vec.Z - EwaldRadius)));

            MessageBox.Show(
                $"Index: {gVector[num].Index.h} {gVector[num].Index.k} {gVector[num].Index.l}\r\n" +
                $"d-spacing: {gVector[num].d:f4} nm\r\n" +
                $"Length: {1 / gVector[num].d:f4} /nm\r\n" +
                $"Coordinate (/nm): {vec.X:f4}, {vec.Y:f4}, {vec.Z:f4}\r\n" +
                $"Excitation error: {dev:f5} /nm\r\n" +
                $"Structure factor (magnitude): {gVector[num].F.Magnitude:f5}\r\n" +
                $"Structure factor (real, imaginary): {gVector[num].F.Real:f5}, {gVector[num].F.Imaginary:f5}"
                , "Information on the clicked g vector");
        }
    }

    private void graphicsBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Middle)
        {
            SetVector();
            Draw();
        }

        if (MouseRangingMode)
        {
            MouseRangingMode = false;
            MouseRangeEnd = new Point(e.X, e.Y);

            var ptStart = convertScreenToDetector(MouseRangeStart);
            var ptEnd = convertScreenToDetector(MouseRangeEnd);

            if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) < 2 && Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) < 2)
            {//選択範囲があまりに小さすぎたら縮小
                if (checkBoxFixCenter.Checked)
                    Foot = FixedCenter;
                else
                    Foot = -(ptStart + ptEnd) / 2;
                Resolution *= 1.2;
            }
            else if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) > 10 && Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) > 10)
            {
                //現在のmagと中心位置から、新しいmagと中心位置を決定する

                if (checkBoxFixCenter.Checked)
                    Foot = FixedCenter;
                else
                    Foot = -(ptStart + ptEnd) / 2;
                Resolution = (Math.Abs(ptStart.X - ptEnd.X) / graphicsBox.ClientSize.Width + Math.Abs(ptStart.Y - ptEnd.Y) / graphicsBox.ClientSize.Height) / 2;
            }
        }
        else
            Draw();
    }

    private PointD lastMousePositionDetector = new();
    private Point lastMousePositionScreen = new();

    private void graphicsBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
        //マウスポインタの情報を表示

        var detectorPos = convertScreenToDetector(e.X, e.Y);
        labelMousePositionDetector.Text = $"({detectorPos.X:f3}, {detectorPos.Y:f3})";

        var realPos = convertDetectorToReal(detectorPos.X, detectorPos.Y);
        labelMousePositionReal.Text = $"{realPos.X:f3}, {realPos.Y:f3}, {realPos.Z:f3})";

        var invPos = convertRealToReciprocal(realPos, false);
        labelMousePositionReciprocal.Text = $"({invPos.X:f3}, {invPos.Y:f3}, {invPos.Z:f3})";

        labelDinv.Text = $"d⁻¹: {invPos.Length:f4} nm⁻¹";
        var d = 1.0 / invPos.Length;
        labelD.Text = $"d: {d:f4} nm";
        var twoThetaRad = 2 * Math.Asin(WaveLength / 2 / d);
        var twoThetaDeg = twoThetaRad / Math.PI * 180;
        labelTwoThetaDeg.Text = $"2θ: {twoThetaDeg:g4}°";
        labelTwoThetaRad.Text = $"2θ: {(twoThetaRad < 0.1 ? $"{twoThetaRad * 1000:g4} mrad" : $"{twoThetaRad:g4} rad")}";

        if (splitContainer1.Panel1.Controls.GetChildIndex(graphicsBox) != 0 && e.X > tabControl.Width || e.Y > tabControl.Height - 20)
        {
            splitContainer1.BringToFront();
            graphicsBox.Refresh();
        }

        //左ボタンが押されながらマウスが動いたとき
        if (e.Button == MouseButtons.Left)
        {
            if ((e.X - graphicsBox.ClientSize.Width / 2) * (e.X - graphicsBox.ClientSize.Width / 2) + (e.Y - graphicsBox.ClientSize.Height / 2) * (e.Y - graphicsBox.ClientSize.Height / 2)
                < Math.Min(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height) * Math.Min(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height) * 0.18)
            {
                double angle = Math.Atan(new PointD(lastMousePositionDetector.X - detectorPos.X, lastMousePositionDetector.Y - detectorPos.Y).Length / CameraLength2 * Resolution) * trackBarRotationSpeed.Value / 50.0;
                formMain.Rotate((detectorPos.Y - lastMousePositionDetector.Y, detectorPos.X - lastMousePositionDetector.X, 0), angle);
            }
            else
            {
                formMain.Rotate((0, 0, 1), -Math.Atan2(lastMousePositionDetector.X, lastMousePositionDetector.Y) + Math.Atan2(detectorPos.X, detectorPos.Y));
            }
        }
        //真ん中ボタンが押されながらマウスが動いたとき
        else if (e.Button == MouseButtons.Middle)
        {
            //コントロールキーが押されていなくて、かつ中心位置が固定でないとき
            if ((ModifierKeys & Keys.Control) != Keys.Control && !checkBoxFixCenter.Checked)
            {
                Foot = new PointD(Foot.X + (e.X - lastMousePositionScreen.X) * Resolution, Foot.Y + (e.Y - lastMousePositionScreen.Y) * Resolution);
                Draw(null, false);
            }
            //コントロールキーが押されていて、かつ検出器エリアが表示の時
            else if ((ModifierKeys & Keys.Control) == Keys.Control && FormDiffractionSimulatorGeometry.ShowDetectorArea)
            {
                FormDiffractionSimulatorGeometry.FootX += (lastMousePositionScreen.X - e.X) * Resolution / FormDiffractionSimulatorGeometry.DetectorPixelSize;
                FormDiffractionSimulatorGeometry.FootY += (lastMousePositionScreen.Y - e.Y) * Resolution / FormDiffractionSimulatorGeometry.DetectorPixelSize;
                if (FormDiffractionSimulatorGeometry.Visible)
                    FormDiffractionSimulatorGeometry.Refresh();
                Draw(null, false);
            }
        }
        else if (e.Button == MouseButtons.Right && MouseRangingMode)
        {
            MouseRangeEnd = new Point(e.X, e.Y);
            Draw(null, false);
        }

        lastMousePositionDetector = detectorPos;
        lastMousePositionScreen = new Point(e.X, e.Y);
    }

    private void graphicsBox_Resize(object sender, EventArgs e) => Draw();

    #endregion graphicsBoxのイベント

    #region その他メニューアイテム
    /// <summary>
    /// ダイナミックコンプレッション
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void dynamicCompressionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FormDiffractionSimulatorGeometry.Visible = true;
        FormDiffractionSimulatorDynamicCompression.Visible = true;
    }

    /// <summary>
    /// ベーテ法を説明するPDFを表示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void basicConceptOfBethesMethodToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        var f = new FormPDF(appPath + @"\doc\bethe.pdf");
        f.ShowDialog();
    }

    /// <summary>
    /// プリセットメニュー
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void presetToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var name = (sender as ToolStripMenuItem).Text.Split(" ", true);
        if (name[0].Contains("Electron"))
        {
            Source = WaveSource.Electron;
            Energy = name[1].ToDouble();

        }
        else if (name[0].Contains("X-ray"))
        {
            Source = WaveSource.Xray;
            if (name.Length == 3)
            {
                waveLengthControl.XrayWaveSourceElementNumber = 0;
                Energy = name[1].ToDouble();
            }
            else
            {
                if (name[3] == "(MoKα₁)")
                {
                    waveLengthControl.XrayWaveSourceElementNumber = 42;
                    waveLengthControl.XrayWaveSourceLine = XrayLine.Ka1;
                }
                else if (name[3] == "(CuKα₁)")
                {
                    waveLengthControl.XrayWaveSourceElementNumber = 29;
                    waveLengthControl.XrayWaveSourceLine = XrayLine.Ka1;
                }

            }
        }

    }

    #endregion

    #region 印刷関係

    private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (pageSetupDialog1.ShowDialog() == DialogResult.OK)
            printDocument1.PrinterSettings = pageSetupDialog1.PrinterSettings;
    }

    // 印刷プレビューを表示
    private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e) =>
        printPreviewDialog1.ShowDialog();

    private void printToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (printDialog1.ShowDialog() == DialogResult.OK)
            printDocument1.Print();
    }

    private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        System.Drawing.Printing.PageSettings ps = printDocument1.PrinterSettings.DefaultPageSettings;
        //用紙サイズ取得 このサイズは1/100インチ
        float height = (ps.PaperSize.Height - ps.Margins.Top - ps.Margins.Bottom) / 100f;
        float width = (ps.PaperSize.Width - ps.Margins.Left - ps.Margins.Right) / 100f;

        if (printDocument1.PrinterSettings.DefaultPageSettings.Landscape)
            (height, width) = (width, height);//縦横を逆転
        double originalReso = numericBoxResolution.Value;
        switch (MessageBox.Show("Real scale printing ?", "Print Option", MessageBoxButtons.YesNoCancel))
        {
            case DialogResult.Yes:
                numericBoxResolution.Value = (300 / 25.4);
                break;

            case DialogResult.Cancel: return;
        }

        /*
         //解像度300dpiのときのイメージサイズは
         glString.Font = new Font("Tahoma", trackBarStrSize.Value / 10f);
         Bitmap bmp = glAlpha.GenerateBitmap(panelSimulation, (int)(width * 300), (int)(height * 300));

         bmp.SetResolution(300, 300);

         e.Graphics.PageUnit = GraphicsUnit.Inch;
         e.Graphics.DrawImage(bmp, new PointF(ps.Margins.Top / 100f, ps.Margins.Left / 100f));
         e.HasMorePages = false;

         numericUpDownResolution.Value=originalReso;
         glString.Font = new Font("Tahoma", trackBarStrSize.Value / 10f);
         */
    }

    #endregion 印刷関係

    #region ToolStripButton, StatusStrip 関連のイベント
    private void toolStripButtonDiffractionSpots_CheckedChanged(object sender, EventArgs e)
    {
        SetVector();

        if (sender is ToolStripButton button)
        {
            var text = button.Text;
            if (text.Contains("Spot"))
                groupBoxSpotProperty.Enabled = button.Checked;
            else if (text.Contains("Kikuchi") || text.Contains("Kikuchi") || text.Contains("Scale"))
            {
                TabPage page;
                if (button.Name.Contains("Kikuchi"))
                    page = tabPageKikuchi;
                else if (button.Name.Contains("Debye"))
                    page = tabPageDebye;
                else
                    page = tabPageScale;

                if (button.Checked)
                {
                    tabControl.SelectedTab = page;
                    tabControl.BringToFront();
                }
            }
            tabControl.Refresh();
        }
        Draw();
    }

    private void statusStrip1_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right && e.Clicks == 2)
        {
            var fdsg = FormDiffractionSimulatorGeometry;
            var sb = new StringBuilder();
            sb.AppendLine($"Crystal:\t{formMain.Crystal.Name}");
            sb.AppendLine($"Euler Phi:\t{formMain.Phi / Math.PI * 180:f3}");
            sb.AppendLine($"Euler Theta:\t{formMain.Theta / Math.PI * 180:f3}");
            sb.AppendLine($"Euler Psi:\t{formMain.Psi / Math.PI * 180:f3}");
            sb.AppendLine($"Monitor resolution:\t{Resolution}");
            sb.AppendLine($"Camera Length2:  {CameraLength2}");

            sb.AppendLine($"Spot shape:\t{(radioButtonCircleArea.Checked ? "Solid sphere" : "Gaussian")}");
            sb.AppendLine($"Radius or Sigma:\t{numericBoxSpotRadius.Value}");
            sb.AppendLine($"Intensity calculation:\t{(radioButtonIntensityExcitation.Checked ? "Excitation error only" : "Kinematical")}");
            sb.AppendLine($"Tau:\t{Tau / Math.PI * 180}");
            sb.AppendLine($"Image name:\t{fdsg.textBoxFileName.Text}");
            sb.AppendLine($"Detector width:\t{fdsg.DetectorWidth}");
            sb.AppendLine($"Detector height:\t{fdsg.DetectorHeight}");
            sb.AppendLine($"Detector pixel size:\t{fdsg.DetectorPixelSize}");
            sb.AppendLine($"Detector foot X:\t{fdsg.FootX}");
            sb.AppendLine($"Detector foot Y:\t{fdsg.FootY}");

            Clipboard.SetDataObject(sb.ToString());
        }
    }
    #endregion

    #region TabControl関連のイベント
    private void tabControl_Click(object sender, EventArgs e)
    {
        tabControl.BringToFront();
        graphicsBox.Refresh();
    }

    private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
    {
        if (
            (!toolStripButtonDebyeRing.Checked && e.TabPage.Name == tabPageDebye.Name) ||
            (!toolStripButtonScale.Checked && e.TabPage.Name == tabPageScale.Name) ||
             (!toolStripButtonKikuchiLines.Checked && e.TabPage.Name == tabPageKikuchi.Name)
            )
            e.Cancel = true;

    }

    private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
    {
        var tab = (TabControl)sender;
        //タブページのテキストを取得
        var txt = tab.TabPages[e.Index].Text;

        //StringFormatを作成 //水平垂直方向の中央に、行が完全に表示されるようにする
        var sf = new StringFormat()
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.LineLimit
        };
        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //背景の描画
        if (tab.SelectedIndex == e.Index)
            e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
        else
            e.Graphics.FillRectangle(new SolidBrush(tabControl.BackColor), e.Bounds);

        //文字色を設定
        var brush = new SolidBrush(tabControl.ForeColor);
        if ((!toolStripButtonDebyeRing.Checked && txt == tabPageDebye.Text) ||
            (!toolStripButtonScale.Checked && txt == tabPageScale.Text) ||
             (!toolStripButtonKikuchiLines.Checked && txt == tabPageKikuchi.Text))
            brush = new SolidBrush(Color.Gray);

        //Textの描画
        e.Graphics.DrawString(txt, tabControl.Font, brush, e.Bounds, sf);

    }

    #endregion

    #region ドラッグドロップイベント
    public void FormDiffractionSimulator_DragDrop(object sender, DragEventArgs e)
        => FormDiffractionSimulatorGeometry.FormDiffractionSimulatorGeometry_DragDrop(sender, e);

    private void FormDiffractionSimulator_DragEnter(object sender, DragEventArgs e)
        => e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;
    #endregion

    #region ビーム光学系の設定変更イベント

    #region 波長コントロールのイベント
    private void waveLengthControl_WavelengthChanged(object sender, EventArgs e)
    {
        if (this.Visible == false) return;

        SetVector();
        Draw();
    }

    private void WaveLengthControl_WaveSourceChanged(object sender, EventArgs e)
    {
        //線源が変更されたら無条件で平行ビームに変更
        radioButtonBeamParallel.Checked = true;

        if (Source == WaveSource.Electron)//電子線が選択された場合
        {
            radioButtonBeamConvergence.Visible = radioButtonBeamPrecessionElectron.Visible = true;//収束と歳差(電子)は表示
            radioButtonBeamPrecessionXray.Visible = false;//歳差(X線)は非表示
            radioButtonIntensityDynamical.Visible = true;//動力学計算は表示
        }
        else if (Source == WaveSource.Xray)//X線が選択された場合
        {
            radioButtonBeamConvergence.Visible = radioButtonBeamPrecessionElectron.Visible = false;//収束と歳差(電子)は非表示
            radioButtonBeamPrecessionXray.Visible = true;//歳差(X線)は表示
            radioButtonIntensityDynamical.Visible = false;//動力学計算は非表示
            if (radioButtonIntensityDynamical.Checked)//動力学計算が選択されていた場合は運動学に変更
                radioButtonIntensityKinematical.Checked = true;
        }
        else//中性子が選択された場合
        {
            radioButtonBeamConvergence.Visible = radioButtonBeamPrecessionElectron.Visible = false;//収束と歳差(電子)は非表示
            radioButtonBeamPrecessionXray.Visible = false;//歳差(X線)は非表示
            radioButtonIntensityDynamical.Visible = false;//動力学計算は非表示
            if (radioButtonIntensityDynamical.Checked)//動力学計算が選択されていた場合は運動学に変更
                radioButtonIntensityKinematical.Checked = true;
        }
    }
    #endregion

    /// <summary>
    /// カラースケールの変更
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void comboBoxScaleColorScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        flowLayoutPanelSpotColor.Visible = comboBoxScaleColorScale.SelectedIndex == 0;
        Draw();
    }

    private void checkBoxUseCrystalColor_CheckedChanged(object sender, EventArgs e)
    {
        SetVector();
        Draw();
    }

    /// <summary>
    /// Gaussian / Solid sphereの切り替え
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void radioButtonPointSpread_CheckedChanged(object sender, EventArgs e)
    {
        flowLayoutPanelGaussianOption.Visible = radioButtonPointSpread.Checked;
        SetVector();
        trackBarIntensityForPointSpread.Enabled = radioButtonPointSpread.Checked;
        checkBoxLogScale.Enabled = radioButtonPointSpread.Checked;

        Draw();
    }

    /// <summary>
    /// Optics (平行、歳差(電子)、歳差(X線)、収束)が変更されたとき 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void radioButtonBeamParallel_CheckedChanged(object sender, EventArgs e)
    {
        if (radioButtonBeamParallel.Checked)//平行
        {
            radioButtonIntensityExcitation.Visible = radioButtonIntensityKinematical.Visible = true;

            flowLayoutPanelPED.Visible = false;
            FormDiffractionSimulatorCBED.Visible = false;

            flowLayoutPanelBethe.Enabled = true;
            flowLayoutPanelBethe.Enabled = flowLayoutPanelAppearance.Enabled = true;
        }
        else if (radioButtonBeamPrecessionElectron.Checked)//歳差(電子)
        {
            radioButtonIntensityExcitation.Visible = radioButtonIntensityKinematical.Visible = false;

            radioButtonIntensityDynamical.Checked = true;

            flowLayoutPanelPED.Visible = true;
            FormDiffractionSimulatorCBED.Visible = false;

            flowLayoutPanelBethe.Enabled = flowLayoutPanelAppearance.Enabled = true;
        }
        else if (radioButtonBeamConvergence.Checked)//収束
        {
            radioButtonIntensityExcitation.Visible = radioButtonIntensityKinematical.Visible = false;

            radioButtonIntensityDynamical.Checked = true;

            flowLayoutPanelPED.Visible = false;

            FormDiffractionSimulatorCBED.Visible = true;

            flowLayoutPanelBethe.Enabled = flowLayoutPanelAppearance.Enabled = false;
        }
        else if (radioButtonBeamPrecessionXray.Checked)//歳差(X線)
        {
            radioButtonIntensityExcitation.Visible = true;
            radioButtonIntensityKinematical.Visible = true;

            flowLayoutPanelPED.Visible = false;

            FormDiffractionSimulatorCBED.Visible = false;
            flowLayoutPanelBethe.Enabled = flowLayoutPanelAppearance.Enabled = true;
        }

        //PEDラジオボタンのチェック状況によって、PED設定パネルの表示変更
        flowLayoutPanelPED.Visible = radioButtonBeamPrecessionElectron.Checked;

        //ブロッホ波設定パネルの表示変更
        flowLayoutPanelBethe.Visible = radioButtonIntensityDynamical.Checked;

        SetVector();
        Draw();
    }


    /// <summary>
    /// 計算方法 (励起誤差、運動学、動力学)のラジオボタンが変更されたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void radioButtonIntensityCalculationMethod_CheckedChanged(object sender, EventArgs e)
    {
        //ファイルメニューアイテムの変更
        saveDetectorAreaToolStripMenuItem.Visible = copyDetectorAreaToolStripMenuItem.Visible = FormDiffractionSimulatorGeometry.ShowDetectorArea;
        saveCBEDPatternToolStripMenuItem.Visible = copyCBEDPatternToolStripMenuItem.Visible = radioButtonBeamConvergence.Checked;

        if (radioButtonIntensityExcitation.Checked) //励起誤差のみの場合
        {
            flowLayoutPanelExtinctionOption.Visible = true;

            flowLayoutPanelBethe.Visible = false;
        }
        else if (radioButtonIntensityKinematical.Checked)  // 運動学的
        {
            flowLayoutPanelExtinctionOption.Visible = false;

            flowLayoutPanelBethe.Visible = false;
        }
        else //動力学的 
        {
            flowLayoutPanelExtinctionOption.Visible = false;
            flowLayoutPanelBethe.Visible = true;
        }

        SetVector();
        Draw();
    }

    private void checkBoxExtinctionAll_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxExtinctionAll.Checked)
        {
            checkBoxExtinctionLattice.Enabled = false;
            checkBoxExtinctionLattice.Checked = true;
        }
        else
            checkBoxExtinctionLattice.Enabled = true;
        SetVector();
        Draw();
    }


    /// <summary>
    /// ビーム詳細情報がクリックされたとき
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ButtonDetailsOfSpots_Click(object sender, EventArgs e)
    {
        FormDiffractionBeamTable.Visible = true;
        FormDiffractionBeamTable.BringToFront();
        if (radioButtonIntensityDynamical.Checked)
            FormDiffractionBeamTable.SetTable(Energy, formMain.Crystal.Bethe.Beams);
        else
            FormDiffractionBeamTable.SetTable(Energy, formMain.Crystal);
    }

    #endregion

    #region 中心位置設定関連
    private void buttonResetCenter_Click_1(object sender, EventArgs e)
    {
        Foot = FixedCenter;
        Draw();
    }

    private PointD FixedCenter
    {
        get
        {
            if (comboBoxCenter.SelectedIndex == 0)
                return new PointD(0, 0);
            else if (comboBoxCenter.SelectedIndex == 1)
                return -convertReciprocalToDetector(new Vector3DBase(0, 0, 0));
            else
            {
                var fdsg = FormDiffractionSimulatorGeometry;

                if (FormDiffractionSimulatorGeometry.ShowDetectorArea)
                    return new PointD(fdsg.FootX - fdsg.DetectorWidth / 2.0, fdsg.FootY - fdsg.DetectorHeight / 2.0) * fdsg.DetectorPixelSize;
                else
                    return new PointD(0, 0);
            }
        }
    }


    private void checkBoxFixCenter_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxFixCenter.Checked)
            buttonResetCenter.PerformClick();
        buttonResetCenter.Enabled = !checkBoxFixCenter.Checked;
    }

    private void radioButtonCenterTo_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxFixCenter.Checked && (sender as RadioButton).Checked)
            buttonResetCenter_Click_1(sender, e);
    }

    #endregion

    #region タイマー関連
    private void toolStripButtonDiffractionSpots_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Clicks == 2 && e.Button == MouseButtons.Right && ((ToolStripButton)sender).Checked)
        {
            System.Windows.Forms.Timer timer;
            if ((ToolStripButton)sender == toolStripButtonDiffractionSpots)
                timer = timerBlinkSpot;
            else if ((ToolStripButton)sender == toolStripButtonKikuchiLines)
                timer = timerBlinkKikuchiLine;
            else if ((ToolStripButton)sender == toolStripButtonDebyeRing)
                timer = timerBlinkDebyeRing;
            else
                timer = timerBlinkScale;

            if (!timer.Enabled)
                timer.Start();
            else
            {
                ((ToolStripButton)sender).ForeColor = SystemColors.MenuHighlight;
                timer.Stop();
                timer.Tag = true;
                Draw();
            }
        }
    }

    private void timerBlinkSpot_Tick(object sender, EventArgs e)
    {
        var timer = (System.Windows.Forms.Timer)sender;
        timer.Tag = !(bool)timer.Tag;
        toolStripButtonDiffractionSpots.ForeColor = (bool)timer.Tag ? SystemColors.MenuHighlight : SystemColors.Info;
        Draw();
    }

    private void timerBlinkKikuchiLine_Tick(object sender, EventArgs e)
    {
        var timer = (System.Windows.Forms.Timer)sender;
        timer.Tag = !(bool)timer.Tag;
        toolStripButtonKikuchiLines.ForeColor = (bool)timer.Tag ? SystemColors.MenuHighlight : SystemColors.Info;
        Draw();
    }

    private void timerBlinkDebyering_Tick(object sender, EventArgs e)
    {
        var timer = (System.Windows.Forms.Timer)sender;
        timer.Tag = !(bool)timer.Tag;
        toolStripButtonDebyeRing.ForeColor = (bool)timer.Tag ? SystemColors.MenuHighlight : SystemColors.Info;
        Draw();
    }

    private void timerBlinkScale_Tick(object sender, EventArgs e)
    {
        var timer = (System.Windows.Forms.Timer)sender;
        timer.Tag = !(bool)timer.Tag;
        toolStripButtonScale.ForeColor = (bool)timer.Tag ? SystemColors.MenuHighlight : SystemColors.Info;
        Draw();
    }
    #endregion

    #region 保存、コピー関連
    private void saveAsImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopy(true, true, true);

    private void saveAsMetafileToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopy(true, false, true);

    private void saveDetectorAsImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(true, true);

    private void saveDetectorAsMetafileToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(true, false);

    private void copyAsImageToolStripMenuItem1_Click(object sender, EventArgs e) => SaveOrCopy(false, true, true);

    private void copyAsMetafileToolStripMenuItem1_Click(object sender, EventArgs e) => SaveOrCopy(false, false, true);

    private void copyDetectorAsImageWithOverlappeImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(false, true);

    private void copyDetectorAsMetafileWithOverlappedImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(false, false);

    private void saveCBEDasPngToolStripMenuItem_Click(object sender, EventArgs e) => saveCBEDasTiffToolStripMenuItem_Click(sender, e);

    private void saveCBEDasTiffToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var png = ((ToolStripItem)sender).Text.Contains("PNG");

        SaveFileDialog dlg = new() { Filter = png ? "*.png|*.png" : "*.tif|*.tif" };

        if (FormDiffractionSimulatorCBED.Disks != null && FormDiffractionSimulatorCBED.Disks.Length != 0 && dlg.ShowDialog() == DialogResult.OK)
        {
            var name = dlg.FileName[0..^4];//拡張子を除去
            for (int t = FormDiffractionSimulatorCBED.trackBarOutputThickness.Minimum; t <= FormDiffractionSimulatorCBED.trackBarOutputThickness.Maximum; t++)
            {
                FormDiffractionSimulatorCBED.trackBarOutputThickness.Value = t;
                var thickness = FormDiffractionSimulatorCBED.ThicknessArray[t];
                foreach (var disk in FormDiffractionSimulatorCBED.Disks)
                {
                    var info = $" ({thickness}nm_{disk.H}_{disk.K}_{disk.L})";
                    if (png)
                        disk.Bitmap.Save(name + info + ".png", ImageFormat.Png);
                    else
                    {
                        //Crystallography.Tiff.Writer(name + info + ".tif", disk.PBitmap.SrcValuesGray, 3, disk.PixelSize.Width);

                        using var image = BitMiracle.LibTiff.Classic.Tiff.Open(name + info + ".tif", "w");
                        int height = disk.PixelSize.Width, width = disk.PixelSize.Width;
                        image.SetField(TiffTag.IMAGEWIDTH, width);
                        image.SetField(TiffTag.IMAGELENGTH, height);
                        image.SetField(TiffTag.SAMPLESPERPIXEL, 1);
                        image.SetField(TiffTag.SAMPLEFORMAT, SampleFormat.IEEEFP);
                        image.SetField(TiffTag.BITSPERSAMPLE, 32);
                        image.SetField(TiffTag.ROWSPERSTRIP, width);
                        //image.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);
                        image.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISBLACK);
                        image.SetField(TiffTag.COMPRESSION, Compression.LZW);
                        //image.SetField(TiffTag.FILLORDER, FillOrder.MSB2LSB);
                        var src = disk.PBitmap.SrcValuesGray.Select(v => (float)v).ToArray();
                        for (int i = 0; i < height; i++)
                        {
                            var buffer = new byte[width * sizeof(float)];
                            Buffer.BlockCopy(src, i * width * sizeof(float), buffer, 0, buffer.Length);
                            image.WriteScanline(buffer, i);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Collective image, TIFF
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void asCollectiveImageTiffFormatToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!FormDiffractionSimulatorCBED.Visible || FormDiffractionSimulatorCBED.Disks == null)
            return;

        SaveFileDialog dlg = new() { Filter = "*.tif|*.tif" };
        if (dlg.ShowDialog() != DialogResult.OK) return;

        var name = dlg.FileName[0..^4];//拡張子を除去

        for (int t = FormDiffractionSimulatorCBED.trackBarOutputThickness.Minimum; t <= FormDiffractionSimulatorCBED.trackBarOutputThickness.Maximum; t++)
        {
            FormDiffractionSimulatorCBED.trackBarOutputThickness.Value = t;
            var thickness = FormDiffractionSimulatorCBED.ThicknessArray[t];

            var disk0 = FormDiffractionSimulatorCBED.Disks.Where(d => d.Bitmap != null).First();
            var resolution = disk0.Size.Width / disk0.PixelSize.Width;//1ピクセル辺りの長さ (mm/pixel)

            //まず、作成する画像の縦横ピクセル数を決める
            var top_left = convertScreenToDetector(0, 0);
            var bottom_rihgt = convertScreenToDetector(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height);
            var width = (int)((bottom_rihgt.X - top_left.X) / resolution + 1);
            var height = (int)((bottom_rihgt.Y - top_left.Y) / resolution + 1);
            //作成する画像の中心座標
            var center = new PointD(-top_left.X, -top_left.Y) / resolution;

            //画像の情報を格納する行列を作成
            var imageArray = new float[width * height];

            if (FormDiffractionSimulatorCBED.Visible && FormDiffractionSimulatorCBED.Disks != null)
                foreach (var disk in FormDiffractionSimulatorCBED.Disks.Where(d => d.Bitmap != null))
                {
                    var diskCenterInPixel = new Point((int)(disk.Center.X / resolution + center.X + 0.5), (int)(disk.Center.Y / resolution + center.Y + 0.5));
                    var diskWidthInPixel = disk.PixelSize.Width;
                    var diskHeightInPixel = disk.PixelSize.Height;

                    var src = disk.PBitmap.SrcValuesGray.Select(v => (float)v).ToArray();
                    for (int y = 0; y < diskHeightInPixel; y++)
                        for (int x = 0; x < diskWidthInPixel; x++)
                        {
                            var posX = diskCenterInPixel.X + x - diskWidthInPixel / 2;
                            var posY = diskCenterInPixel.Y + y - diskHeightInPixel / 2;
                            if ((uint)posX < (uint)width && (uint)posY < (uint)height && imageArray[posY * width + posX] == 0)
                                imageArray[posY * width + posX] = src[y * diskWidthInPixel + x];
                        }
                }


            var info = $" ({thickness}nm)";
            using var image = BitMiracle.LibTiff.Classic.Tiff.Open(name + info + ".tif", "w");
            image.SetField(TiffTag.IMAGEWIDTH, width);
            image.SetField(TiffTag.IMAGELENGTH, height);
            image.SetField(TiffTag.SAMPLESPERPIXEL, 1);
            image.SetField(TiffTag.SAMPLEFORMAT, SampleFormat.IEEEFP);
            image.SetField(TiffTag.BITSPERSAMPLE, 32);
            image.SetField(TiffTag.ROWSPERSTRIP, width);
            //image.SetField(TiffTag.PLANARCONFIG, PlanarConfig.CONTIG);
            image.SetField(TiffTag.PHOTOMETRIC, Photometric.MINISBLACK);
            image.SetField(TiffTag.COMPRESSION, Compression.LZW);
            //image.SetField(TiffTag.FILLORDER, FillOrder.MSB2LSB);
            for (int i = 0; i < height; i++)
            {
                var buffer = new byte[width * sizeof(float)];
                Buffer.BlockCopy(imageArray, i * width * sizeof(float), buffer, 0, buffer.Length);
                image.WriteScanline(buffer, i);
            }
        }


    }

    private void saveCBEDasMetafileToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(true, false);
    private void copyCBEDasImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(false, true);

    //private void copyCBEDasMetafileToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(false, false);
    private void saveCBEDasCollectiveImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(true, true);

    private void SaveOrCopy(bool save, bool isImage, bool drawOverlappedImage)
    {
        if (isImage)
        {
            var bmp = new Bitmap(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height);
            var g = Graphics.FromImage(bmp);
            Draw(g, true, drawOverlappedImage);
            if (bmp != null)
            {
                if (save)
                {
                    SaveFileDialog dlg = new() { Filter = "*.png|*.png" };
                    if (dlg.ShowDialog() == DialogResult.OK)
                        bmp.Save(dlg.FileName, ImageFormat.Png);
                }
                else
                {
                    Clipboard.SetDataObject(bmp);
                }
            }
        }
        else
        {
            using Graphics grfx = CreateGraphics();
            IntPtr ipHdc = grfx.GetHdc();
            MemoryStream ms = new();
            Metafile mf = new(ms, ipHdc, EmfType.EmfPlusDual);
            grfx.ReleaseHdc(ipHdc);
            grfx.Dispose();
            var g = Graphics.FromImage(mf);
            Draw(g, true, drawOverlappedImage);
            g.Dispose();

            if (save)
            {
                SaveFileDialog dlg = new() { Filter = "*.emf|*.emf" };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    FileStream fsm = new(dlg.FileName, FileMode.Create, FileAccess.Write);
                    fsm.Write(ms.GetBuffer(), 0, (int)ms.Length);
                    fsm.Close();
                }
            }
            else
                ClipboardMetafileHelper.PutEnhMetafileOnClipboard(this.Handle, mf);
        }
    }

    private void SaveOrCopyDetector(bool save, bool asImage, bool asTiff = false)
    {
        bool drawOverlappedImage;
        if (FormDiffractionSimulatorGeometry.ShowDetectorArea && FormDiffractionSimulatorGeometry.OverlappedImage != null)
        {
            drawOverlappedImage = MessageBox.Show("Include the overlapped image?", "Copy option", MessageBoxButtons.YesNo) == DialogResult.Yes;

            graphicsBox.Visible = false;
            var originalSize = graphicsBox.Size;
            var originalResolution = Resolution;
            var originalFoot = new PointD(Foot.X, Foot.Y);
            var originalStringSize = trackBarStrSize.Value;

            var fdsg = FormDiffractionSimulatorGeometry;

            Resolution = fdsg.DetectorPixelSize;
            graphicsBox.ClientSize = new Size(fdsg.DetectorWidth, fdsg.DetectorHeight);
            Foot = new PointD((fdsg.FootX - fdsg.DetectorWidth / 2.0) * fdsg.DetectorPixelSize, (fdsg.FootY - fdsg.DetectorHeight / 2.0) * fdsg.DetectorPixelSize);

            int strSize = (int)(originalResolution / Resolution * originalStringSize);
            if (strSize > trackBarStrSize.Maximum)
                trackBarStrSize.Value = trackBarStrSize.Maximum;
            else if (strSize < trackBarStrSize.Minimum)
                trackBarStrSize.Value = trackBarStrSize.Minimum;
            else
                trackBarStrSize.Value = strSize;

            SetVector();

            SaveOrCopy(save, asImage, drawOverlappedImage);

            graphicsBox.Size = originalSize;
            Resolution = originalResolution;
            trackBarStrSize.Value = originalStringSize;
            Foot = originalFoot;
            SetVector();
            graphicsBox.Visible = true;
            Draw();
            graphicsBox.Refresh();
        }
        else if (FormDiffractionSimulatorCBED.Visible && FormDiffractionSimulatorCBED.Disks != null)
        {
            drawOverlappedImage = true;

            graphicsBox.Visible = false;
            var originalSize = graphicsBox.Size;
            var originalResolution = Resolution;
            var originalFoot = new PointD(Foot.X, Foot.Y);

            Resolution = FormDiffractionSimulatorCBED.ImagePixelSize;

            var coeff = originalResolution / Resolution;


            graphicsBox.ClientSize = new Size((int)(originalSize.Width * coeff), (int)(originalSize.Height * coeff));
            Foot = new PointD(0, 0);

            SaveOrCopy(save, asImage, drawOverlappedImage);

            graphicsBox.Size = originalSize;
            Resolution = originalResolution;
            Foot = originalFoot;
            SetVector();
            graphicsBox.Visible = true;
            Draw();
            graphicsBox.Refresh();
        }
    }

    #endregion

    #region テスト用コード
    private void Button1_Click(object sender, EventArgs e)
    {
        var step = 0.1;
        var range = 4;
        //-4シグマから+4シグマまで、0.1シグマステップで。

        var sum = new double[graphicsBox.ClientSize.Width * graphicsBox.ClientSize.Height];

        for (double s = -range; s <= range; s += step)
        {
            waveLengthControl.Energy = numericBoxAcc.Value + numericBoxAcc.Value * numericBoxDev.Value / 100.0 * s;

            var bmp = new Bitmap(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height);
            Draw(Graphics.FromImage(bmp), false, false);
            var gray = BitmapConverter.ToByteGray(bmp);

            var temp = gray.Select(intensity => intensity * step / Math.Sqrt(2.0 * Math.PI) * Math.Exp(-s * s / 2)).ToArray();

            for (int i = 0; i < sum.Length; i++)
                sum[i] += temp[i];
        }
        var destBmp = BitmapConverter.FromArrayToBitmap(sum.Select(s => (byte)Math.Min(s, 255)).ToArray(), graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height);
        graphicsBox.Image = destBmp;
        Clipboard.SetDataObject(destBmp);

    }

    private void flowLayoutPanelAppearance_Paint(object sender, PaintEventArgs e)
    {

    }



    private void Button2_Click(object sender, EventArgs e)
    {
        if (radioButtonBeamParallel.Checked && radioButtonIntensityDynamical.Checked)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < 300; i++)
            {
                numericBoxThickness.Value = i;
                Draw();
                var intensity = formMain.Crystal.Bethe.Beams[0].Psi.MagnitudeSquared();

                sb.AppendLine($"{i}\t{intensity}");
            }

            Clipboard.SetDataObject(sb.ToString());

        }
    }

    #endregion

    private void checkBoxReciprocalSpace_CheckedChanged(object sender, EventArgs e)
    {
        splitContainer1.Panel2Collapsed = !checkBoxReciprocalSpace.Checked;
    }

    private readonly object lockObj = new();
    private List<GLObject> ewaldList = new();
    private (double maxAngle, double ewaldRadius, bool Precession) beforeEwald = (0, 0, false);
    private void Draw3D()
    {

        var r = EwaldRadius;
        var ewaldCenter = new Vector3D(0, 0, r);


        if (glControl == null || splitContainer1.Panel2Collapsed) return;
        glControl.DeleteAllObjects();
        if (glControl.BackgroundColor != colorControl3D_Background.Color)
            glControl.BackgroundColor = colorControl3D_Background.Color;

        var glObjects = new List<GLObject>();

        var precession = radioButtonBeamPrecessionXray.Checked;

        List<Vector3D> gVector;

        gVector = formMain.Crystal.VectorOfG.ToList();
        gVector.Sort((g1, g2) => g1.Length2.CompareTo(g2.Length2));
        var maxG = Math.Sqrt(gVector.Max(g => g.Length2)) * 0.75;
        var maxAngle = precession ? Math.Atan(maxG / r) : Math.Asin(maxG / 2 / r) * 2;

        var shift = precession ? new V3(0, 0, 0) : new V3(0, 0, -r * (1 - Math.Cos(maxAngle)) / 2);
        #region エワルド球の描画
        if (checkBox3D_EwaldSphere.Checked)
        {
            if (beforeEwald != (maxAngle, r, precession))
            {
                beforeEwald = (maxAngle, r, precession);
                ewaldList.Clear();
                int div1 = 45, div2 = 60;
                var rot = Enumerable.Range(0, div2 + 1).ToList().Select(n => Matrix3D.Rot(new V3(0, 0, 1), 2 * Math.PI * n / div2)).ToList();

                //X-ray precessionの時はsinではなくtanで計算することに注意
                var sin = Enumerable.Range(0, div1).ToList().Select(n => precession ? Math.Tan(maxAngle / div1 * n) : Math.Sin(maxAngle / div1 * n)).ToList();
                //X-ray precessionの時はcosではなくすべて1として計算することに注意
                var cos = Enumerable.Range(0, div1).ToList().Select(n => precession ? 1 : Math.Cos(maxAngle / div1 * n)).ToList();

                Parallel.For(1, div1, j =>
                {
                    var listObj = new List<GLObject>();
                    var mat = new Material(colorControl3D_EwaldSphere.Color, ((float)(div1 - j) / div1 + 0.3f) / 1.3f);
                    if (j % 5 == 0)
                        listObj.Add(new Disk(new V3(0, 0, r * (1 - cos[j])) + shift, new V3(0, 0, 1), r * sin[j], 1f, mat, DrawingMode.Edges, 90));

                    V3 v1 = new V3(r * sin[j - 1], 0, r * (1 - cos[j - 1])) + shift, v2 = new V3(r * sin[j], 0, r * (1 - cos[j])) + shift;
                    for (int i = 0; i < div2; i++)
                    {
                        V3 rot1V1 = rot[i] * v1, rot1V2 = rot[i] * v2;
                        if (j == 1)
                            listObj.Add(new Triangle(rot1V1, rot1V2, rot[i + 1] * v2, mat, DrawingMode.Surfaces));
                        else
                            listObj.Add(new Polygon(new[] { rot1V1, rot1V2, rot[i + 1] * v2, rot[i + 1] * v1 }, mat, DrawingMode.Surfaces));

                        if (i % 5 == 0)
                            listObj.Add(new Lines(new V3[] { rot1V1, rot1V2 }, 1f, mat));
                    }
                    lock (lockObj)
                        ewaldList.AddRange(listObj);
                });
            }
            glObjects.AddRange(ewaldList);
        }
        #endregion

        #region 逆格子点の描画
        var spotList = new List<Sphere>();
        var textList = new List<(string text, float fontSize, V3 position, double popout, bool whiteEdge, Material mat)>();

        var maxF = radioButtonIntensityExcitation.Checked ? 1 : gVector.Max(g => g.F.MagnitudeSquared());
        var spotRadius = numericBox3D_SpotRadius.Value;
        var thredshold = numericBoxReciprocalThreshold.Value * 0.01;
        spotList.Add(new Sphere(new V3(0, 0, 0) + shift, spotRadius, new Material(colorControl3D_Origin.Color, 1), DrawingMode.Surfaces));

        Color colNear = colorControl3D_SpotsNear.Color, colFar = colorControl3D_SpotsFar.Color;
        Color colScrewOrGlide = colorControlScrewGlide.Color, colLattice = colorControlForbiddenLattice.Color, colGeneral = colorControlNoCondition.Color;
        
        var matText = new Material(colorControl3D_lText.Color);
        
        var transCoef = trackBar3D_Transparency.Value* trackBar3D_Transparency.Value;
        Parallel.ForEach(gVector.Where(g => /*g.Length2 < maxG * maxG &&*/ (g.Flag1 || radioButtonIntensityExcitation.Checked)),new ParallelOptions() { MaxDegreeOfParallelism = 1 }, g =>
        {
            var vec = formMain.Crystal.RotationMatrix * g;//ベーテ法で計算する際には、すでに回転後の座標になっている。
            double dev = precession ? Math.Abs(vec.Z) : Math.Abs((vec - ewaldCenter).Length - r);

            if(g.Text=="0 0 4")
            {

            }

            //スポット強度。励起誤差モードの時は、中心からの距離に比例。kinematicalモードの時は構造因子の二乗
            var F2 = radioButtonIntensityExcitation.Checked ? Math.Max(0.1,maxF * (1 - g.Length / maxG)) : g.F.MagnitudeSquared();
            if (F2 < maxF * thredshold || F2==0)
                return;

            var col = colFar; ;
            if (radioButtonIntensityExcitation.Checked)
            {
                if (g.Extinction.Length == 0)
                    col = colGeneral;
                else if (g.Extinction[0].Length == 1)
                {
                    if (checkBoxExtinctionLattice.Checked)
                        return;
                    col = colLattice;
                }
                else
                {
                    if (checkBoxExtinctionAll.Checked)
                        return;
                    col = colScrewOrGlide;
                }
            }

            if (checkBox3D_EwaldSphere.Checked && dev < spotRadius)
                col = Miscellaneous.BlendColor(colFar, colNear, dev / spotRadius);
            var trans = 1.0;
            if (checkBox3D_MakeSpotsTransparent.Checked)
                trans = dev / r < 100.0 / transCoef ? Math.Min(1 - dev / r * transCoef / 100.0, 1.0) : 0;
            if (trans == 0f)
                return;

            var radius = Math.Pow(F2 / maxF, 1.0 / 3.0) * spotRadius; //逆格子点（構造因子の二乗の1/3状の半径）
            var spot = new Sphere(vec.ToOpenTK() + shift, radius, new Material(col, trans), DrawingMode.Surfaces);
            lock (lockObj)
                spotList.Add(spot);

            if (checkBox3D_ShowIndices.Checked && dev < spotRadius)
                lock (lockObj)
                    textList.Add((g.Text, 9f, vec.ToOpenTK() + shift, radius + 0.1, false, matText));
        });
        glObjects.AddRange(spotList);
        glObjects.AddRange(textList.Select(e => new TextObject(e.text, e.fontSize, e.position, e.popout, e.whiteEdge, e.mat)));
        #endregion


        #region ガイドの描画
        if (checkBox3D_DirectionGuide.Checked)
        {
            glObjects.AddRange(new GLObject[]
            {
                new Cylinder(new V3(0, 0, 0.5 + spotRadius) + shift, new V3(0, 0, 2), 0.075, new Material(colorControl3D_beamDirection.Color), DrawingMode.Surfaces),
                new Cone(new V3(0, 0, spotRadius) + shift, new V3(0, 0, 0.5), 0.2, new Material(colorControl3D_beamDirection.Color), DrawingMode.Surfaces),
                new TextObject("Beam", 13, new V3(0, 0, 2.5 + spotRadius) + shift, 5, true, matText),

                new Cylinder(new V3(0, 0, 0) + shift, new V3(0, 1.5, 0), 0.075, new Material(colorControl3D_topDirection.Color), DrawingMode.Surfaces),
                new Cone(new V3(0, 2, 0) + shift, new V3(0, -0.5, 0), 0.2, new Material(colorControl3D_topDirection.Color), DrawingMode.Surfaces),
                new TextObject("Top", 13, new V3(0, 2, 0) + shift, 5, true, matText),

                new Cylinder(new V3(0, 0, 0) + shift, new V3(1.5, 0, 0), 0.075, new Material(colorControl3D_rightDirection.Color), DrawingMode.Surfaces),
                new Cone(new V3(2, 0, 0) + shift, new V3(-0.5, 0, 0), 0.2, new Material(colorControl3D_rightDirection.Color), DrawingMode.Surfaces),
                new TextObject("right", 13, new V3(2, 0, 0) + shift, 5, true, matText)
            });
        }

        #endregion

        glControl.AddObjects(glObjects);
        glControl.Refresh();
    }

    private void checkBoxShowEwaldSphere_CheckedChanged(object sender, EventArgs e) => Draw3D();

    private void buttonTopLeft_Click(object sender, EventArgs e)
    {
        var v = (sender as Button).Name switch
        {
            "buttonTopRight" => new Vector3DBase(-1, 1, 0),
            "buttonRight" => new Vector3DBase(0, 1, 0),
            "buttonBottomRight" => new Vector3DBase(1, 1, 0),
            "buttonBottom" => new Vector3DBase(1, 0, 0),
            "buttonBottomLeft" => new Vector3DBase(1, -1, 0),
            "buttonLeft" => new Vector3DBase(0, -1, 0),
            "buttonTopLeft" => new Vector3DBase(-1, -1, 0),
            "buttonTop" => new Vector3DBase(-1, 0, 0),
            "buttonClock" => new Vector3DBase(0, 0, -1),
            "buttonAntiClock" => new Vector3DBase(0, 0, 1),
            _ => new Vector3DBase(0, 0, 1)
        };
        var rot = Matrix3D.Rot(v, numericBoxStep.RadianValue);
        glControl.WorldMatrix *= new Matrix4d(rot.ToMatrix());
    }

    private void buttonResetAngle_Click(object sender, EventArgs e)
    {
        glControl.WorldMatrix = Matrix4d.Identity;
    }

    private void colorControlReciprocalBackground_ColorChanged(object sender, EventArgs e) => Draw3D();

    private void trackBar1_ValueChanged(object sender, EventArgs e) => Draw3D();

    private void numericBoxReciprocalThreshold_ValueChanged(object sender, EventArgs e) => Draw3D();

    private void numericBox3D_SpotRadius_ValueChanged(object sender, EventArgs e) => Draw3D();
}