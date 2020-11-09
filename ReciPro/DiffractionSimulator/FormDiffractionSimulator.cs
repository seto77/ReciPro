#region using
using Crystallography;
using Crystallography.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
#endregion

namespace ReciPro
{
    public partial class FormDiffractionSimulator : Form
    {
        #region フィールド、プロパティ

        public enum DrawingMode { None, Kinematical, BetheSAED, BetheCBED }

        public FormMain formMain;

        public double EwaldRadius => 1 / WaveLength;
        public double WaveLength => waveLengthControl.WaveLength;
        public double Energy => waveLengthControl.Energy;
        public double ExcitationError => numericBoxSpotRadius.Value;
        public int ClientWidth => numericBoxClientWidth.ValueInteger;
        public int ClientHeight => numericBoxClientHeight.ValueInteger;

        public double Thickness { get => numericBoxThickness.Value; set => numericBoxThickness.Value = value; }

        private Font font => new Font("Tahoma", (float)(trackBarStrSize.Value * Resolution / 10.0));

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


        public DrawingMode Mode
        {
            get
            {
                if (radioButtonIntensityExcitation.Checked) return DrawingMode.None;
                else if (radioButtonIntensityKinematical.Checked) return DrawingMode.Kinematical;
                else if (radioButtonIntensityBethe.Checked) return DrawingMode.BetheSAED;
                else return DrawingMode.BetheCBED;
            }
        }

        public FormDiffractionSimulatorCBED FormDiffractionSimulatorCBED;

        public FormDiffractionSimulatorGeometry FormDiffractionSimulatorGeometry;
        public FormDiffractionSimulatorDynamicCompression FormDiffractionSimulatorDynamicCompression;

        public FormDiffractionSpotInfo FormDiffractionBeamTable;

        public bool DynamicCompressionMode { get; set; } = false;
        public List<double[]> DynamicCompression_SpotInformation = new List<double[]>();

        /*public double CameraLength1
        {
            set { FormDiffractionSimulatorGeometry.CameraLength1 = value; }
            get { return FormDiffractionSimulatorGeometry == null ? 0 : FormDiffractionSimulatorGeometry.CameraLength1; }
        }*/

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
        public PointD DisplayCenter { get; set; } = new PointD(0, 0);

        /// <summary>
        /// コントロールイベントをスキップする
        /// </summary>
        public bool SkipEvent { get; set; } = false;


        private bool skipDrawing = false;
        /// <summary>
        /// 描画をスキップする (コントロールイベントをスキップする場合は、SkipEventを使う)
        /// </summary>
        public bool SkipDrawing { set { skipDrawing = value; if (!value) Draw(); } get => skipDrawing; }

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
        }

        //ロードされたとき
        public void FormElectronDiffraction_Load(object sender, EventArgs e)
        {
            comboBoxScaleColorScale.SelectedIndex = 0;

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
                FormDiffractionSimulatorCBED = new FormDiffractionSimulatorCBED { FormDiffractionSimulator = this };

            Draw();
        }

        private void FormElectronDiffraction_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            formMain.toolStripButtonElectronDiffraction.Checked = false;
        }

        #endregion

        #region VisibleChanged

        private void FormDiffractionSimulatorGeometry_VisibleChanged(object sender, EventArgs e)
            => numericUpDownCamaraLength2.Enabled = !FormDiffractionSimulatorGeometry.Visible;

        //Visibleが変更されたとき
        private void FormElectronDiffraction_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                DisplayCenter = new PointD(0, 0);
                SetVector();
                tabControl.BringToFront();
                Draw();
                graphicsBox.Refresh();

                if (radioButtonBeamConvergence.Checked)
                    FormDiffractionSimulatorCBED.Visible = true;
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
                    (float)(graphicsBox.ClientSize.Width / 2.0 + DisplayCenter.X / Resolution),
                    (float)(graphicsBox.ClientSize.Height / 2.0 + DisplayCenter.Y / Resolution));
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
            if (FormDiffractionSimulatorCBED.Visible && FormDiffractionSimulatorCBED.CBED_Image != null)
            {
                var cbed = FormDiffractionSimulatorCBED;
                start = new PointD(-cbed.ImagePixelSize * cbed.ImageCenterX, -cbed.ImagePixelSize * cbed.ImageCenterY);
                end = new PointD(cbed.ImagePixelSize * (cbed.ImageWidth - cbed.ImageCenterX), cbed.ImagePixelSize * (cbed.ImageHeight - cbed.ImageCenterY));
                var dest = new PointF[] { start.ToPointF(), new PointF((float)end.X, (float)start.Y), new PointF((float)start.X, (float)end.Y) };//左上、右上、左下の順番
                g.DrawImage(cbed.CBED_Image, dest, new RectangleF(0, 0, cbed.ImageWidth, cbed.ImageHeight), GraphicsUnit.Pixel);
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
            if (formMain.toolStripButtonImageSimulation.Checked && formMain.FormImageSimulator.ImageMode == FormImageSimulator.ImageModes.HRTEM
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

            if (FormDiffractionBeamTable.Visible && radioButtonIntensityBethe.Checked)
                FormDiffractionBeamTable.SetTable(Energy, formMain.Crystal.Bethe.Beams);
        }

        /// <summary>
        /// 与えられた点集合 pts の中から、もっとも指定した方向に近い点を返す. deg 0 : 右, deg 90: 下, deg 180: 左, deg -90:上
        /// </summary>
        /// <param name="list"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        private PointD getLabelPosition(IEnumerable<PointD> list, PointD origin, double deg)
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

            static void fillCircle(Graphics graphics, Color c, PointD pt, double radius, int alpha)
            {
                if (Math.Abs(pt.X) < 1E6 && Math.Abs(pt.Y) < 1E6)
                    graphics.FillEllipse(new SolidBrush(Color.FromArgb(alpha, c)), (float)(pt.X - radius), (float)(pt.Y - radius), (float)(2 * radius), (float)(2 * radius));
            }

            static void drawCircle(Graphics graphics, Color c, PointD pt, double radius)
            {
                if (Math.Abs(pt.X) < 1E6 && Math.Abs(pt.Y) < 1E6)
                    graphics.DrawEllipse(new Pen(c, 0.0001f), (float)(pt.X - radius), (float)(pt.Y - radius), (float)(2 * radius), (float)(2 * radius));
            }

            var radiusCBED = Math.Tan(FormDiffractionSimulatorCBED.AlphaMax) * CameraLength2;

            #region ガウス関数で描画するローカル関数
            int bgR = colorControlBackGround.Color.R, bgG = colorControlBackGround.Color.G, bgB = colorControlBackGround.Color.B;
            double fillCircleSpread(Color c, PointD pt, double intensity, double sigma)
            {
                //計算する二次元ガウス関数は、 f(x,y) = intensity/ (2 pi sigma^2) *  e^{- (x^2+y^2) /2/sigma^2)
                //intensityはスポットの積分強度、sは半値幅
                int gradation = 32;
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
                    if (comboBoxScaleColorScale.SelectedIndex == 1)
                    {
                        var index = Math.Min((int)(intensity2 * 65535), 65535);
                        c = Color.FromArgb(PseudoBitmap.BrightnessScaleLinerColorR[index], PseudoBitmap.BrightnessScaleLinerColorG[index], PseudoBitmap.BrightnessScaleLinerColorB[index]);
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
            var error2 = ExcitationError * ExcitationError;
            var sqrtTwoPI = Math.Sqrt(2 * Math.PI);
            var linearCoeff = Math.Pow(trackBarIntensityForPointSpread.Value / 400.0, 6) * 100;
            var logCoeff = 16.0 * trackBarIntensityForPointSpread.Value / trackBarIntensityForPointSpread.Maximum;

            if (waveLengthControl.WaveSource == WaveSource.Xray)
            {
                linearCoeff *= 1000;
                logCoeff *= 1000000;
            }

            var bethe = radioButtonIntensityBethe.Checked;
            var sw = new Stopwatch();
            foreach (var crystal in formMain.Crystals)
            {
                Vector3D[] gVector;

                if (bethe)//ベーテ法による動力学回折の場合
                {
                    sw.Start();

                    var blochNum = FormDiffractionSimulatorCBED.Visible ? FormDiffractionSimulatorCBED.MaxNumOfBloch : numericBoxNumOfBlochWave.ValueInteger;

                    if (radioButtonBeamPrecession.Checked)//プリセッションの場合
                    {
                        var eigenValues = crystal.Bethe.EigenValuesPED;//電子線の場合

                        var gPED = crystal.Bethe.GetPrecessionElectronDiffraction(blochNum, waveLengthControl.Energy, crystal.RotationMatrix, numericBoxThickness.Value,
                            numericBoxPED_Semiangle.Value / 1000, numericBoxPED_Step.ValueInteger);
                        gVector = gPED.Select(g => g.ConvertToVector3D()).ToArray();

                        if (eigenValues == null || eigenValues[0] != crystal.Bethe.EigenValuesPED[0])
                            toolStripStatusLabelTimeForBethe.Text = $"Time for solving dynamic effects (PED): {sw.ElapsedMilliseconds} ms.  ";
                    }
                    else//パラレルかCBEDの場合
                    {
                        var eigenValues = crystal.Bethe.EigenValues;

                        var gBethe = crystal.Bethe.GetDifractedBeamAmpriltudes(blochNum, waveLengthControl.Energy, crystal.RotationMatrix, numericBoxThickness.Value);
                        gVector = gBethe.Select(g => g.ConvertToVector3D()).ToArray();

                        if (eigenValues != crystal.Bethe.EigenValues)
                            toolStripStatusLabelTimeForBethe.Text = $"Time for solving dynamic effects: {sw.ElapsedMilliseconds} ms.  ";
                    }
                    var max = gVector.Max(g => double.IsInfinity(g.d) ? 0 : g.RawIntensity);
                    gVector = gVector.Select(g => { g.RelativeIntensity = g.RawIntensity / max; return g; }).ToArray();

                    foreach (var g in gVector)
                        g.Argb = formMain.Crystals.Length == 1 ? colorControlNoCondition.Argb : crystal.Argb;
                }
                else
                    gVector = crystal.VectorOfG.ToArray();

                //描画するスポットを決める
                foreach (var g in gVector.Where(g => g.Flag))
                {
                    var vec = bethe ? g : crystal.RotationMatrix * g;//ベーテ法で計算する際には、すでに回転後の座標になっている。

                    //逆空間 <=>実空間で、Y,Zの符号が反転していることに注意
                    if (-vec.Z < (radioButtonPointSpread.Checked ? 3 * ExcitationError : ExcitationError))
                    {
                        double L2 = (vec.X * vec.X) + (vec.Y * vec.Y), dev = 0.0;
                        if (!bethe)
                            g.Tag = dev = EwaldRadius - Math.Sqrt(L2 + (-vec.Z + EwaldRadius) * (-vec.Z + EwaldRadius));

                        var dev2 = dev * dev;
                        if (SinPhi * SinTau * vec.X + CosPhi * SinTau * vec.Y + CosTau * (-vec.Z + EwaldRadius) > 0)
                        //(vec.X, -vec.Y, -vec.Z + EwaldRadius) と(SinPhi*SinTau, -CosPhi*sinTau, cosTau) の内積が0より大きい = 前方散乱)
                        {
                            var pt = convertReciprocalToDetector(vec);
                            if (IsScreenArea(pt))
                            {
                                //CBEDモードの時
                                if (FormDiffractionSimulatorCBED.Visible)
                                {
                                    if (FormDiffractionSimulatorCBED.DrawGuideCircles && Math.Abs(dev) < 3 * ExcitationError && g.RawIntensity > 1E-20)//黄色いガイドサークルを表示
                                        drawCircle(graphics, Color.Yellow, pt, radiusCBED);
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
                                        var sectionRadius = bethe ?
                                            sphereRadius : //ベーテ法の場合はそのまま
                                            Math.Sqrt(sphereRadius * sphereRadius - dev2);//excitaion only あるいは Kinematicの場合は、エワルド球に切られた断面上の、逆格子点の半径
                                        var r = CameraLength2 * WaveLength * sectionRadius;
                                        fillCircle(graphics, Color.FromArgb(g.Argb), pt, r, (int)(alphaCoeff * 255));
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

            var l = (float)spotRadiusOnDetector;

            graphics.SmoothingMode = SmoothingMode.HighQuality;

            if (FormDiffractionSimulatorCBED.Visible)//CBEDモードの時は、ここでキャンセル
                return null;

            //ダイレクトスポットの描画
            var ptOrigin = convertReciprocalToDetector(new Vector3DBase(0, 0, 0));
            if (IsScreenArea(ptOrigin))
            {
                var penOrigin = new Pen(colorControlOrigin.Color, (float)Resolution);
                graphics.DrawLine(penOrigin, ptOrigin.X - l / 2f, ptOrigin.Y - l / 2f, ptOrigin.X + l / 2f, ptOrigin.Y + l / 2f);
                graphics.DrawLine(penOrigin, ptOrigin.X + l / 2f, ptOrigin.Y - l / 2f, ptOrigin.X - l / 2f, ptOrigin.Y + l / 2f);
                //fillCircle(pictureBoxOrigin.BackColor, ptOrigin, l);
                if (toolStripButtonIndexLabels.Checked && trackBarStrSize.Value != 1 && !radioButtonIntensityBethe.Checked)
                    graphics.DrawString("0 0 0", font, new SolidBrush(Color.FromArgb((int)(alphaCoeff * 255), colorControlOrigin.Color)), (float)(ptOrigin.X + l / 2f), (float)(ptOrigin.Y + l / 2f));
                //ダイレクトスポットの描画ここまで
            }
            //垂線の足の描画
            if (Tau != 0 && IsScreenArea(new PointD(0, 0)))
            {
                var penFoot = new Pen(colorControlFoot.Color, (float)Resolution);

                graphics.DrawLine(penFoot, -l / 2f, -l / 2f, l / 2f, l / 2f);
                graphics.DrawLine(penFoot, +l / 2f, -l / 2f, -l / 2f, l / 2f);

                if (toolStripButtonIndexLabels.Checked && trackBarStrSize.Value != 1)
                    graphics.DrawString("foot", font, new SolidBrush(Color.FromArgb((int)(alphaCoeff * 255), colorControlFoot.Color)), l / 2f, l / 2f);
            }
            //垂線の足の描画ここまで
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
            if (toolStripButtonDistance.Checked) sb.AppendLine($"{CameraLength2 * Math.Tan(2 * Math.Asin(WaveLength / g.d / 2)):#.###} mm");
            if (toolStripButtonExcitationError.Checked) sb.AppendLine($"{error:f3} /nm");

            if (toolStripButtonFg.Checked)
            {
                if (radioButtonIntensityKinematical.Checked)
                    sb.AppendLine($"{g.RelativeIntensity * 100:#.#} %");
                if (radioButtonIntensityBethe.Checked)
                    sb.AppendLine($"{g.RelativeIntensity * 100:#.#} %, ({g.F.Real:0.###} + {g.F.Imaginary:0.###}i)");
            }
            graphics.DrawString(sb.ToString(), font, new SolidBrush(Color.FromArgb((int)(alphaCoeff * 255), colorControlString.Color)), (float)(pt.X + radius / 1.4142 + 3 * Resolution), (float)(pt.Y + radius / 1.4142 + 3 * Resolution));
        }

        #endregion

        #region DrawKikuchiLine

        private void DrawKikuchiLine(Graphics graphics)
        {
            var penExcess = new Pen(new SolidBrush(colorControlExcessLine.Color), (float)(trackBarLineWidth.Value * Resolution / 2000f));
            var penDefect = new Pen(new SolidBrush(colorControlDefectLine.Color), (float)(trackBarLineWidth.Value * Resolution / 2000f));
            var diag = Resolution * Math.Sqrt(graphicsBox.ClientSize.Width * graphicsBox.ClientSize.Width + graphicsBox.ClientSize.Height * graphicsBox.ClientSize.Height) / 2;
            //var threshold = 
            foreach (var crystal in formMain.Crystals)
                foreach (var g in crystal.VectorOfG_KikuchiLine)
                {
                    double sinTheta = WaveLength * g.Length / 2, sin2Theta = sinTheta * sinTheta;

                    Vector3DBase vec1 = crystal.RotationMatrix * g;

                    //bool excess は、excess の時にtrue, そうでないときはfalse
                    var excess = vec1.Z < 0;

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
                        List<PointF> pts1 = new List<PointF>(), pts2 = new List<PointF>();
                        for (double omega = -omegaMax; omega < omegaMax; omega += omegaMax / 500)
                        {
                            float x = (float)(Math.Sinh(omega) / Psqrt), y = (float)(Math.Cosh(omega) / Qsqrt);
                            pts1.Add(new PointF(x, y));
                            pts2.Add(new PointF(x, -y));
                        }
                        try
                        {
                            graphics.DrawLines(excess ? penExcess : penDefect, pts1.ToArray());
                            graphics.DrawLines(excess ? penDefect : penExcess, pts2.ToArray());
                            if (toolStripButtonIndexLabels.Checked)
                            {
                                graphics.DrawString(g.Text, font, new SolidBrush(colorControlString.Color), pts1[pts1.Count / 2]);
                                graphics.DrawString(g.Text, font, new SolidBrush(colorControlString.Color), pts2[pts2.Count / 2]);
                            }
                        }
                        catch { }
                    }

                    graphics.Transform = original;
                }
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

                int red = (int)(ringR * intensity + bgR * (1 - intensity));
                int green = (int)(ringG * intensity + bgG * (1 - intensity));
                int blue = (int)(ringB * intensity + bgB * (1 - intensity));
                Pen pen = new Pen(new SolidBrush(Color.FromArgb(red, green, blue)), (float)(this.trackBarDebyeRingWidth.Value * Resolution / 2f));

                foreach (var pts in ptsArray)
                    g.DrawLines(pen, pts.ToArray());

                var labelPosition = getLabelPosition(ptsArray.SelectMany(p => p).Where(p => IsScreenArea(p, 5)), originSrc, -90);
                if (checkBoxScaleLabel.Checked && !double.IsNaN(labelPosition.X))
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
                                var str = (xx > 1E-6) || (xx > -1E-6 && yy > 1E-6) ? n.ToString() : (n - 180).ToString();
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
                foreach (var pts in ptsArray)
                    g.DrawLines(pen, pts.ToArray());

                var labelPosition = getLabelPosition(ptsArray.SelectMany(p => p).Where(p => IsScreenArea(p, 20)), originSrc, -135);
                if (checkBoxScaleLabel.Checked && !double.IsNaN(labelPosition.X))
                    g.DrawString(twoTheta.ToString() + "°", font, new SolidBrush(colorControlScale2Theta.Color), labelPosition.ToPointF());
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


        private void checkBoxMousePositionDetailes_CheckedChanged(object sender, EventArgs e) => labelMousePositionReciprocal.Visible =
                labelMousePositionDetector.Visible =
                labelMousePositionReal.Visible =
                checkBoxMousePositionDetailes.Checked;

        private void numericBoxKikuchiLineThreshold_ValueChanged(object sender, EventArgs e)
        {
            SetVector();
            Draw();
        }

        #endregion

        #region 逆格子ベクトルを初期化. 
        //逆格子ベクトルを設定する
        public void SetVector(bool renewCrystal = false)
        {
            if (formMain == null) return;
            if (CancelSetVector) return;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            double d1 = 1 / convertScreenToReciprocal(0, 0, false).Length;
            double d2 = 1 / convertScreenToReciprocal(0, graphicsBox.ClientSize.Height, false).Length;
            double d3 = 1 / convertScreenToReciprocal(graphicsBox.ClientSize.Width, 0, false).Length;
            double d4 = 1 / convertScreenToReciprocal(graphicsBox.ClientSize.Width, graphicsBox.ClientSize.Height, false).Length;
            double minD = new[] { d1, d2, d3, d4 }.Min();
            //double maxD = new[] { d1, d2, d3, d4 }.Max();
            WaveSource w = waveLengthControl.WaveSource;
            if (toolStripButtonDiffractionSpots.Checked)
            {
                if (toolStripMenuItemBackLaue.Checked)//Back Laueのとき
                    minD = WaveLength / 2;

                for (int i = 0; i < formMain.Crystals.Length; i++)
                {
                    Crystal crystal = formMain.Crystals[i];
                    crystal.SetVectorOfG(minD, radioButtonIntensityKinematical.Checked ? w : WaveSource.None);

                    int noConditionColor = formMain.Crystals.Length == 1 && !checkBoxUseCrystalColor.Checked ? colorControlNoCondition.Color.ToArgb() : crystal.Argb;
                    int screwGlideColor = colorControlScrewGlide.Color.ToArgb();
                    int latticeColor = colorControlForbiddenLattice.Color.ToArgb();
                    string latticeType = crystal.Symmetry.LatticeTypeStr;

                    foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length == 0))
                    {
                        gtemp.Flag = true;
                        gtemp.Argb = noConditionColor;
                    }

                    if (!checkBoxExtinctionLattice.Checked)
                    {
                        foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length > 0 && g.Extinction[0] == latticeType))
                        {
                            gtemp.Flag = true;
                            gtemp.Argb = latticeColor;
                        }
                    }

                    if (!checkBoxExtinctionAll.Checked)
                    {
                        foreach (var gtemp in crystal.VectorOfG.Where(g => g.Extinction.Length > 0 && g.Extinction[0] != latticeType))
                        {
                            gtemp.Flag = true;
                            gtemp.Argb = screwGlideColor;
                        }
                    }
                }
            }

            if (toolStripButtonDebyeRing.Checked)
            {
                formMain.Crystal.SetPlanes(double.PositiveInfinity, minD, true, true, false, true, HorizontalAxis.d, 0.00000000, WaveLength);
                formMain.Crystal.SetPeakIntensity(w, WaveColor.Monochrome, WaveLength, null);
                for (int j = 0; j < formMain.Crystal.Plane.Count; j++)
                    if (formMain.Crystal.Plane[j].Intensity < 1E-6)
                        formMain.Crystal.Plane.RemoveAt(j--);
            }

            if (toolStripButtonKikuchiLines.Checked)
                formMain.Crystal.SetVectorOfG_KikuchiLine(numericBoxKikuchiLineThreshold.Value, WaveLength);

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
        private PointD convertScreenToDetector(int x, int y) => new PointD(
                (x - graphicsBox.ClientSize.Width / 2.0) * Resolution - DisplayCenter.X,
                (y - graphicsBox.ClientSize.Height / 2.0) * Resolution - DisplayCenter.Y);

        /// <summary>
        /// 座標変換 画面(Screen)上の点(pixel)を検出器(Detector)上の位置 (mm)に変換
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private PointD convertScreenToDetector(Point p) => convertScreenToDetector(p.X, p.Y);

        /// <summary>
        /// 座標変換 画面(Screen)上の点(pixel) を 実空間座標(mm, ３次元座標)に変換
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Vector3DBase convertScreenToReal(int x, int y)
        {
            PointD p = convertScreenToDetector(x, y);//まずフィルム上の位置を取得
            return convertDetectorToReal(p.X, p.Y);//実空間の座標に変換
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
        private PointD convertDetectorToScreen(double x, double y) => new PointD(
                (x + DisplayCenter.X) / Resolution + graphicsBox.ClientSize.Width / 2.0,
                (y + DisplayCenter.Y) / Resolution + graphicsBox.ClientSize.Height / 2.0);


        /// <summary>
        /// 検出器(Detector)上の位置 (mm)を画面(Screen)上の点(pixel)に変換
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private PointD convertDetectorToScreen(PointD pt) => convertDetectorToScreen(pt.X, pt.Y);

        /// <summary>
        /// 座標変換 検出器(Detector)上の点(Foot中心, mm単位) を 実空間座標(mm単位, ３次元座標)に変換
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Vector3DBase convertDetectorToReal(double x, double y) =>
        #region 座標変換の計算式
            // (CosPhi, SinPhi, 0) の周りに Tau回転する行列は、
            //   Cos2Phi * (1 - CosTau) + CosTau | CosPhi * SinPhi * (1 - CosTau)  |  SinPhi * SinTau
            //   CosPhi * SinPhi * (1 - CosTau)  | Sin2Phi * (1 - CosTau) + CosTau | -CosPhi * SinTau
            //  -SinPhi * SinTau                 | cosPhi  * sinTau                |  CosTau  
            //この行列を(x,y,CameraLength2)に作用させればよい
        #endregion
            DetectorRotation * new Vector3DBase(x, y, CameraLength2);


        /// <summary>
        /// 実空間座標(mm単位, ３次元座標)を逆空間座標に変換
        /// </summary>
        /// <param name="v"></param>
        /// <param name="originalCoordinate"></param>
        /// <returns></returns>
        private Vector3DBase convertRealToReciprocal(Vector3DBase v, bool originalCoordinate)
        {
            var len = Math.Sqrt(v.X2Y2);
            var twoTheta = Math.Atan2(len, v.Z);

            double sinTheta = Math.Sin(twoTheta / 2), sinThetaSquare = sinTheta * sinTheta;
            var Z = EwaldRadius * (1 - Math.Cos(twoTheta));

            var temp = 1 / len * Math.Sqrt((4 * sinThetaSquare * EwaldRadius * EwaldRadius) - Z * Z);
            double X = v.X * temp, Y = -v.Y * temp;

            return originalCoordinate ? formMain.Crystal.RotationMatrix.Inverse() * new Vector3DBase(X, Y, Z) : new Vector3DBase(X, Y, Z);

        }

        /// <summary>
        /// 逆空間座標を実空間座標に変換。　 逆空間座標のy,zの符号を反転することに注意
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public Vector3DBase convertReciprocalToReal(Vector3DBase g)
            => Geometriy.GetCrossPoint(SinPhi * SinTau, -CosPhi * SinTau, CosTau, CameraLength2, new Vector3DBase(g.X, -g.Y, EwaldRadius - g.Z));

        // return p * d / (a * p.X + b * p.Y + c * p.Z);

        /// <summary>
        /// 逆空間座標を検出器座標に変換。　 逆空間座標のy,zの符号を反転することに注意
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        private PointD convertReciprocalToDetector(Vector3DBase g)
        {
            var v = DetectorRotationInv * new Vector3DBase(g.X, -g.Y, EwaldRadius - g.Z);
            var coeff = CameraLength2 / v.Z;
            return new PointD(v.X, v.Y) * coeff;
        }


        /// <summary>
        /// 検出器座標で与えられた座標ptが、画面内に含まれるかどうかを返す
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool IsScreenArea(PointD pt, int margin = 0)
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
                    $"index: {gVector[num].Index.h} {gVector[num].Index.k} {gVector[num].Index.l}\r\n" +
                    $"d-spacing: {gVector[num].d:f4} nm\r\n" +
                    $"Inverse coordinate (1/nm): {vec.X:f3} ,{vec.Y:f3} ,{vec.Z:f3}\r\n"
                    + $"Exitation error: {dev:f4} /nm");
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
                        DisplayCenter = FixedCenter;
                    else
                        DisplayCenter = -(ptStart + ptEnd) / 2;
                    Resolution *= 1.2;
                }
                else if (Math.Abs(MouseRangeEnd.X - MouseRangeStart.X) > 10 && Math.Abs(MouseRangeEnd.Y - MouseRangeStart.Y) > 10)
                {
                    //現在のmagと中心位置から、新しいmagと中心位置を決定する

                    if (checkBoxFixCenter.Checked)
                        DisplayCenter = FixedCenter;
                    else
                        DisplayCenter = -(ptStart + ptEnd) / 2;
                    Resolution = (Math.Abs(ptStart.X - ptEnd.X) / graphicsBox.ClientSize.Width + Math.Abs(ptStart.Y - ptEnd.Y) / graphicsBox.ClientSize.Height) / 2;
                }
            }
            else
                Draw();
        }

        private PointD lastMousePositionDetector = new PointD();
        private Point lastMousePositionScreen = new Point();

        private void graphicsBox_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //マウスポインタの情報を表示

            var detectorPos = convertScreenToDetector(e.X, e.Y);
            labelMousePositionDetector.Text = $"Detector Coord. (origin: foot):　{detectorPos.X:f3} mm, {detectorPos.Y:f3} mm";

            var realPos = convertDetectorToReal(detectorPos.X, detectorPos.Y);
            labelMousePositionReal.Text = $"Real Coord. (origin: sample):　{realPos.X:f3} mm, {realPos.Y:f3} mm, {realPos.Z:f3} mm";


            var reciprocalPos = convertRealToReciprocal(realPos, false);
            labelMousePositionReciprocal.Text = $"Reciprocal Coord. :{reciprocalPos.X:f3} /nm, {reciprocalPos.Y:f3} /nm, {reciprocalPos.Z:f3} /nm";

            labelDinv.Text = $"1/d: {reciprocalPos.Length:f4} /nm";
            var d = 1.0 / reciprocalPos.Length;
            labelD.Text = $"d: {d:f4} nm";
            var twoThetaRad = 2 * Math.Asin(WaveLength / 2 / d);
            var twoThetaDeg = twoThetaRad / Math.PI * 180;
            labelTwoTheta.Text = $"2θ: {(twoThetaRad < 0.1 ? $"{twoThetaRad * 1000:g4} mrad" : $"{twoThetaRad:g4} rad")},  {twoThetaDeg:g4}°";

            if (panelMain.Controls.GetChildIndex(graphicsBox)!=0 && e.X > tabControl.Width || e.Y > tabControl.Height - 20)
            {
                graphicsBox.BringToFront();
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
                if ((Control.ModifierKeys & Keys.Control) != Keys.Control && !checkBoxFixCenter.Checked)
                {
                    DisplayCenter = new PointD(DisplayCenter.X + (e.X - lastMousePositionScreen.X) * Resolution, DisplayCenter.Y + (e.Y - lastMousePositionScreen.Y) * Resolution);
                    Draw(null, false);
                }
                //コントロールキーが押されていて、かつ検出器エリアが表示の時
                else if ((Control.ModifierKeys & Keys.Control) == Keys.Control && FormDiffractionSimulatorGeometry.ShowDetectorArea)
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

        private void graphicsBox_Resize(object sender, EventArgs e)
        {
            Draw();
        }

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
            var name = (sender as ToolStripMenuItem).Text.Split(" ");
            if (name[0].Contains("Electron"))
            {
                waveLengthControl.WaveSource = WaveSource.Electron;
                waveLengthControl.Energy = name[1].ToDouble();

            }
            else if (name[0].Contains("X-ray"))
            {
                waveLengthControl.WaveSource = WaveSource.Xray;
                if (name.Length == 3)
                {
                    waveLengthControl.XrayWaveSourceElementNumber = 0;
                    waveLengthControl.Energy = name[1].ToDouble();
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
            {//縦横を逆転
                float temp = width; width = height; height = temp;
            }
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
                else if(text.Contains("Kikuchi")||text.Contains("Kikuchi")||text.Contains("Scale"))
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
                    else if (tabControl.SelectedTab == page)
                        tabControl.SelectedTab = tabPageWave;
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

        #region 波長コントロールのイベント
        private void waveLengthControl_WavelengthChanged(object sender, EventArgs e)
        {
            if (this.Visible == false) return;

            SetVector();

            Draw();
        }

        private void WaveLengthControl_WaveSourceChanged(object sender, EventArgs e)
        {
            if (waveLengthControl.WaveSource == WaveSource.Electron)
            {
                radioButtonBeamConvergence.Enabled = radioButtonBeamPrecession.Enabled = true;
                radioButtonIntensityBethe.Enabled = true;
            }
            else
            {
                radioButtonBeamConvergence.Enabled = radioButtonBeamPrecession.Enabled = false;

                if (radioButtonBeamConvergence.Checked || radioButtonBeamPrecession.Checked)
                    radioButtonBeamParallel.Checked = true;

                radioButtonIntensityBethe.Enabled = false;
                if (radioButtonIntensityBethe.Checked)
                    radioButtonIntensityKinematical.Checked = true;

            }
            radioButtonBeamParallel.Checked = true;
        }
        #endregion

        #region ドラッグドロップイベント
        public void FormDiffractionSimulator_DragDrop(object sender, DragEventArgs e) 
            => FormDiffractionSimulatorGeometry.FormDiffractionSimulatorGeometry_DragDrop(sender, e);

        private void FormDiffractionSimulator_DragEnter(object sender, DragEventArgs e)
            => e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;
        #endregion

        #region スポット関連の設定変更イベント
        
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
            flowLayoutPanelGaussianOption.Enabled = radioButtonPointSpread.Checked;
            SetVector();
            trackBarIntensityForPointSpread.Enabled = radioButtonPointSpread.Checked;
            checkBoxLogScale.Enabled = radioButtonPointSpread.Checked;

            Draw();
        }

        /// <summary>
        /// 計算方法 (励起誤差、運動学、動力学)のラジオボタンが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonIntensityCalculationMethod_CheckedChanged(object sender, EventArgs e)
        {
            flowLayoutPanelExtinctionOption.Enabled = radioButtonIntensityExcitation.Checked;

            colorControlScrewGlide.Enabled = colorControlForbiddenLattice.Visible = radioButtonIntensityExcitation.Checked;

            buttonDetailsOfSpots.Enabled = radioButtonIntensityBethe.Checked;

            formMain.Crystal.Bethe.MaxNumOfBloch = 0;

            FormDiffractionSimulatorCBED.Visible = radioButtonBeamConvergence.Checked;
            numericBoxSpotRadius.Enabled = !radioButtonBeamConvergence.Checked;

            saveDetectorAreaToolStripMenuItem.Visible = copyDetectorAreaToolStripMenuItem.Visible = FormDiffractionSimulatorGeometry.ShowDetectorArea;

            saveCBEDPatternToolStripMenuItem.Visible = copyCBEDPatternToolStripMenuItem.Visible = radioButtonBeamConvergence.Checked;

            if (radioButtonBeamConvergence.Checked)
                radioButtonPointSpread.Checked = true;

            if (!radioButtonIntensityBethe.Checked)
                FormDiffractionBeamTable.Visible = false;

            flowLayoutPanelPED.Enabled = radioButtonBeamPrecession.Checked;

            if (radioButtonBeamConvergence.Checked || radioButtonBeamPrecession.Checked)
            {

                radioButtonIntensityExcitation.Enabled = false;
                radioButtonIntensityKinematical.Enabled = false;
                radioButtonIntensityBethe.Checked = true;
            }
            else
            {
                radioButtonIntensityExcitation.Enabled = true;
                radioButtonIntensityKinematical.Enabled = true;
            }
            flowLayoutPanelBethe.Enabled = radioButtonIntensityBethe.Checked;



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

  

        private void ButtonDetailsOfSpots_Click(object sender, EventArgs e)
        {
            FormDiffractionBeamTable.Visible = true;
            FormDiffractionBeamTable.BringToFront();
            if (radioButtonIntensityBethe.Checked)
                FormDiffractionBeamTable.SetTable(waveLengthControl.Energy, formMain.Crystal.Bethe.Beams);
        }


        #endregion

        #region 中心位置設定関連
        private void buttonResetCenter_Click_1(object sender, EventArgs e)
        {
            DisplayCenter = FixedCenter;

            Draw();
        }

        private PointD FixedCenter
        {
            get
            {
                if (radioButtonCenterToFoot.Checked)
                    return new PointD(0, 0);
                else if (radioButtonCenterToDirect.Checked)
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
                Timer timer;
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
            var timer = (Timer)sender;
            timer.Tag = !(bool)timer.Tag;
            toolStripButtonDiffractionSpots.ForeColor = (bool)timer.Tag ? SystemColors.MenuHighlight : SystemColors.Info;
            Draw();
        }

        private void timerBlinkKikuchiLine_Tick(object sender, EventArgs e)
        {
            var timer = (Timer)sender;
            timer.Tag = !(bool)timer.Tag;
            toolStripButtonKikuchiLines.ForeColor = (bool)timer.Tag ? SystemColors.MenuHighlight : SystemColors.Info;
            Draw();
        }

        private void timerBlinkDebyering_Tick(object sender, EventArgs e)
        {
            var timer = (Timer)sender;
            timer.Tag = !(bool)timer.Tag;
            toolStripButtonDebyeRing.ForeColor = (bool)timer.Tag ? SystemColors.MenuHighlight : SystemColors.Info;
            Draw();
        }

        private void timerBlinkScale_Tick(object sender, EventArgs e)
        {
            var timer = (Timer)sender;
            timer.Tag = !(bool)timer.Tag;
            toolStripButtonScale.ForeColor = (bool)timer.Tag ? SystemColors.MenuHighlight : SystemColors.Info;
            Draw();
        }
        #endregion

        #region 保存、コピー関連ｎ
        private void saveAsImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopy(true, true, true);

        private void saveAsMetafileToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopy(true, false, true);

        private void saveDetectorAsImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(true, true);

        private void saveDetectorAsMetafileToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(true, false);

        private void copyAsImageToolStripMenuItem1_Click(object sender, EventArgs e) => SaveOrCopy(false, true, true);

        private void copyAsMetafileToolStripMenuItem1_Click(object sender, EventArgs e) => SaveOrCopy(false, false, true);

        private void copyDetectorAsImageWithOverlappeImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(false, true);

        private void copyDetectorAsMetafileWithOverlappedImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(false, false);

        private void saveCBEDasPngToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(true, true);

        private void saveCBEDasTiffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = "*.tif|*.tif" };
            if (FormDiffractionSimulatorCBED.SrcImage != null && FormDiffractionSimulatorCBED.SrcImage.Length != 0 && dlg.ShowDialog() == DialogResult.OK)
                Tiff.Writer(dlg.FileName, FormDiffractionSimulatorCBED.SrcImage, 3, FormDiffractionSimulatorCBED.CBED_Image.Width);
        }

        private void saveCBEDasMetafileToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(true, false);

        private void copyCBEDasImageToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(false, true);

        private void copyCBEDasMetafileToolStripMenuItem_Click(object sender, EventArgs e) => SaveOrCopyDetector(false, false);

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
                        SaveFileDialog dlg = new SaveFileDialog { Filter = "*.png|*.png" };
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
                Graphics grfx = CreateGraphics();
                IntPtr ipHdc = grfx.GetHdc();
                MemoryStream ms = new MemoryStream();
                Metafile mf = new Metafile(ms, ipHdc, EmfType.EmfPlusDual);
                grfx.ReleaseHdc(ipHdc);
                grfx.Dispose();
                var g = Graphics.FromImage(mf);
                Draw(g, true, drawOverlappedImage);
                g.Dispose();

                if (save)
                {
                    SaveFileDialog dlg = new SaveFileDialog { Filter = "*.emf|*.emf" };
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        FileStream fsm = new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write);
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
            var drawOverlappedImage = false;

            if (FormDiffractionSimulatorGeometry.ShowDetectorArea && FormDiffractionSimulatorGeometry.OverlappedImage != null)
            {
                drawOverlappedImage = MessageBox.Show("Include the overlapped image?", "Copy option", MessageBoxButtons.YesNo) == DialogResult.Yes;

                graphicsBox.Visible = false;
                var originalSize = graphicsBox.Size;
                var originalResolution = Resolution;
                var originalFoot = new PointD(DisplayCenter.X, DisplayCenter.Y);
                var originalStringSize = trackBarStrSize.Value;

                var fdsg = FormDiffractionSimulatorGeometry;

                Resolution = fdsg.DetectorPixelSize;
                graphicsBox.ClientSize = new Size(fdsg.DetectorWidth, fdsg.DetectorHeight);
                DisplayCenter = new PointD((fdsg.FootX - fdsg.DetectorWidth / 2.0) * fdsg.DetectorPixelSize, (fdsg.FootY - fdsg.DetectorHeight / 2.0) * fdsg.DetectorPixelSize);

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
                DisplayCenter = originalFoot;
                SetVector();
                graphicsBox.Visible = true;
                Draw();
                graphicsBox.Refresh();
            }
            else if (FormDiffractionSimulatorCBED.Visible && FormDiffractionSimulatorCBED.CBED_Image != null)
            {
                drawOverlappedImage = true;

                graphicsBox.Visible = false;
                var originalSize = graphicsBox.Size;
                var originalResolution = Resolution;
                var originalFoot = new PointD(DisplayCenter.X, DisplayCenter.Y);

                Resolution = FormDiffractionSimulatorCBED.ImagePixelSize;
                graphicsBox.ClientSize = new Size(FormDiffractionSimulatorCBED.ImageWidth, FormDiffractionSimulatorCBED.ImageHeight);
                DisplayCenter = new PointD(0, 0);

                SaveOrCopy(save, asImage, drawOverlappedImage);

                graphicsBox.Size = originalSize;
                Resolution = originalResolution;
                DisplayCenter = originalFoot;
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

   

        private void Button2_Click(object sender, EventArgs e)
        {
            if (radioButtonBeamParallel.Checked && radioButtonIntensityBethe.Checked)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 300; i++)
                {
                    numericBoxThickness.Value = i;
                    Draw();
                    var intensity = formMain.Crystal.Bethe.Beams[0].Psi.Magnitude2();

                    sb.AppendLine($"{i}\t{intensity}");
                }

                Clipboard.SetDataObject(sb.ToString());

            }
        }

        #endregion

    }
}