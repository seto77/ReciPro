using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Crystallography;

namespace Crystallography.Controls
{
    [Serializable]
    public partial class ScalablePictureBoxAdvanced : UserControl
    {
        public ScalablePictureBoxAdvanced()
        {
           
            InitializeComponent();

            graphControl.LineList = new[] { new PointD(0, 0), new PointD(0, 0) };
            comboBoxGradient.SelectedIndex = 0;
            comboBoxScale1.SelectedIndex = 1;
            comboBoxScale2.SelectedIndex = 0;
        }

        #region プロパティ

        /// <summary>
        /// VisualStudioデザイナーの編集の時はTrue
        /// </summary>
        public new bool DesignMode
        {
            get
            {
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    return true;
                Control ctrl = this;
                while (ctrl != null)
                {
                    if (ctrl.Site != null && ctrl.Site.DesignMode)
                        return true;
                    ctrl = ctrl.Parent;
                }
                return false;
            }
        }

        /// <summary>
        /// スクロールバーが表示されているかどうか
        /// </summary>
        public bool ScrollBarVisible { get => scalablePictureBox.ScrollBarVisible; }

        /// <summary>
        /// スクロールバーをログスケールで動かすかどうか
        /// </summary>
        public bool LogScaleBar { get => trackBarAdvancedMaximum.LogScrollBar; set => trackBarAdvancedMaximum.LogScrollBar = trackBarAdvancedMinimum.LogScrollBar = value; }

        /// <summary>
        /// 描画しているソース画像の範囲を取得/設定する
        /// </summary>
        public RectangleD DrawingArea { get => scalablePictureBox.DrawingArea; }

        /// <summary>
        /// マウス位置の情報を表示するかどうか
        /// </summary>
        public bool MousePositionLabelVisible
        {
            get => label.Visible;
            set
            {
                label.Visible = value;
                panelUpper.Visible = MousePositionLabelVisible || CopyButtonVisible;
            }
        }

        /// <summary>
        /// コピーボタンを表示するかどうか
        /// </summary>
        public bool CopyButtonVisible
        {
            get => buttonCopyToClipBoard.Visible;
            set
            {
                buttonCopyToClipBoard.Visible = value;
                panelUpper.Visible = MousePositionLabelVisible || CopyButtonVisible;
            }
        }

        /// <summary>
        /// メモリを表示するかどうか
        /// </summary>
        public bool ShowGradiaent { get; set; } = true;

        public bool TrackBarVisible { set => panelTrackBar.Visible = value; get => panelTrackBar.Visible; }

        public bool FrequencyGraphVisible { set => graphControl.Visible = value; get => graphControl.Visible; }

        [Category("Image Filter")]
        /// <summary>
        /// ImageFilterを有効にするかどうか
        /// </summary>
        public bool ImageFilterVisible { set => flowLayoutPanelImageFilter.Visible = value; get => flowLayoutPanelImageFilter.Visible; }

        [Category("Image Filter")]
        /// <summary>
        /// GaussianFilterを有効にするかどうか
        /// </summary>
        public bool ImageFilter_GaussianBlur { set => checkBoxGaussianBlur.Checked = value; get => checkBoxGaussianBlur.Checked; }

        [Category("Image Filter")]
        /// <summary>
        /// GaussianFilterを有効にするかどうか
        /// </summary>
        public bool ImageFilter_GaussianBlurVisible { set => checkBoxGaussianBlur.Visible = value; get => checkBoxGaussianBlur.Visible; }

        [Category("Image Filter")]
        /// <summary>
        /// GaussianFilterの
        /// </summary>
        public double ImageFilter_GaussianBlurRadius { set => numericBoxGaussianFWHM.Value = value; get => numericBoxGaussianFWHM.Value; }

        [Category("Image Filter")]
        /// <summary>
        /// Dust＆Scratchesを有効にするかどうか
        /// </summary>
        public bool ImageFilter_DustAndScratches { set => checkBoxDustScratches.Checked = value; get => checkBoxDustScratches.Checked; }

        [Category("Image Filter")]
        /// <summary>
        /// Dust＆Scratchesを有効にするかどうか
        /// </summary>
        public bool ImageFilter_DustAndScratchesVisible { set => checkBoxDustScratches.Visible = value; get => checkBoxDustScratches.Visible; }

        [Category("Image Filter")]
        public double ImageFilter_DustAndScratchesRadius { set => numericBoxDustScratchesRadius.Value = value; get => numericBoxDustScratchesRadius.Value; }

        [Category("Image Filter")]
        public double ImageFilter_DustAndScratchesThreshold { set => numericBoxDustScratchesThreshold.Value = value; get => numericBoxDustScratchesThreshold.Value; }

        public bool VisibleGradient { set => flowLayoutPanelGradient.Visible = value; get => flowLayoutPanelGradient.Visible; }

        public Size PictureSize
        {
            set
            {
                this.Width = value.Width;
                this.Height = (this.Height - scalablePictureBox.Size.Height) + value.Height;
            }
            get => scalablePictureBox.Size;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PseudoBitmap PseudoBitmap
        {
            set
            {
                scalablePictureBox.PseudoBitmap = value;
                Initialize();
            }
            get { return scalablePictureBox.PseudoBitmap; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public (double Zoom, PointD Center) ZoomAndCenter
        {
            set { scalablePictureBox.ZoomAndCenter = value; }
            get { return scalablePictureBox.ZoomAndCenter; }
        }

        /// <summary>
        /// ZoomやCenter位置を固定するかどうか
        /// </summary>
        public bool FixZoomAndCenter { get => scalablePictureBox.FixZoomAndCenter; set => scalablePictureBox.FixZoomAndCenter = value; }

        private double upperIntensity = 255;

        public double UpperIntensity
        {
            set
            {
                if (value > MaximumIntensity)
                    value = MaximumIntensity;
                else if (value < LowerIntensity)
                    value = LowerIntensity;
                upperIntensity = value;
                trackBarAdvancedMaximum.Value = value;

                int n = graphControl.LineList[0].X > graphControl.LineList[1].X ? 0 : 1;
                graphControl.LineList[n].X = value;
                graphControl.Draw();

                PseudoBitmap.MaxValue = value;
                scalablePictureBox.drawPictureBox();

                BrightnessAndColorChanged?.Invoke(this, new EventArgs());
            }
            get => upperIntensity;
        }

        private double lowerIntensity = 0;

        public double LowerIntensity
        {
            set
            {
                if (value > UpperIntensity)
                    value = UpperIntensity;
                else if (value < MinimumIntensity)
                    value = MinimumIntensity;
                lowerIntensity = value;
                trackBarAdvancedMinimum.Value = value;

                int n = graphControl.LineList[0].X < graphControl.LineList[1].X ? 0 : 1;
                graphControl.LineList[n].X = value;
                graphControl.Draw();

                PseudoBitmap.MinValue = value;
                scalablePictureBox.drawPictureBox();

                BrightnessAndColorChanged?.Invoke(this, new EventArgs());
            }
            get => lowerIntensity;
        }

        //画像中の最大強度
        private double maximumIntensity = 255;

        public double MaximumIntensity
        {
            get => maximumIntensity;
            set
            {
                maximumIntensity = value;
                trackBarAdvancedMinimum.Maximum = trackBarAdvancedMaximum.Maximum = maximumIntensity;
            }
        }

        //画像中の最小強度
        private double minimumIntensity = 0;

        public double MinimumIntensity
        {
            get => minimumIntensity;
            set
            {
                minimumIntensity = value;
                trackBarAdvancedMinimum.Minimum = trackBarAdvancedMaximum.Minimum = MinimumIntensity;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScalablePictureBox ScalablePictureBox { get { return scalablePictureBox; } }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<ScalablePictureBox.Symbol> Symbols { get { return scalablePictureBox.Symbols; } set { scalablePictureBox.Symbols = value; } }

        private double progress = 0;

        public double StatusProgress
        {
            set
            {
                if (progress != value)
                {
                    progress = value;
                    toolStripProgressBar1.Value = (int)(toolStripProgressBar1.Maximum * value);
                    StatusChanged?.Invoke(this, null);
                }
            }
            get => progress;
        }

        public string StatusLabel
        {
            set
            {
                if (toolStripStatusLabel1.Text != value)
                {
                    toolStripStatusLabel1.Text = value;
                    StatusChanged?.Invoke(this, null);
                }
            }
            get => toolStripStatusLabel1.Text;
        }

        public bool StatusVisible { set => statusStrip1.Visible = value; get => statusStrip1.Visible; }

        #endregion プロパティ

        #region イベント

        /// <summary>
        /// マウスイベント用のデリゲート
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        /// <param name="pt">ソース画像座標</param>
        /// <returns>その後のイベントをキャンセルする場合: true</returns>
        public delegate bool MouseEvent(object sender, MouseEventArgs e, PointD pt);

        public event MouseEvent MouseMove2;

        public event MouseEvent MouseUp2;

        public event MouseEvent MouseDown2;

        public event MouseEvent MouseWheel2;

        /// <summary>
        /// マウスが押されたとき。コントロール本体でのその後のイベントをキャンセルする場合、返り値はtrue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool scalablePictureBox_MouseDown2(object sender, MouseEventArgs e, PointD pt)
        {
            if (MouseDown2 != null) return MouseDown2(sender, e, pt);
            else return false;
        }

        /// <summary>
        /// マウスが上がったとき。コントロール本体でのその後のイベントをキャンセルする場合、返り値はtrue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool scalablePictureBox_MouseUp2(object sender, MouseEventArgs e, PointD pt)
        {
            if (MouseUp2 != null) return MouseUp2(sender, e, pt);
            else return false;
        }

        /// <summary>
        /// マウスホイールが回ったとき。コントロール本体でのその後のイベントをキャンセルする場合、返り値はtrue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool scalablePictureBox_MouseWheel2(object sender, MouseEventArgs e, PointD pt)
        {
            if (MouseWheel2 != null) return MouseWheel2(sender, e, pt);
            else return false;
        }

        /// <summary>
        /// マウスが動いた時に画面上部にマウス位置、強度を示す。コントロール本体でのその後のイベントをキャンセルする場合、返り値はtrue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool scalablePictureBox1_MouseMove2(object sender, MouseEventArgs e, PointD pt)
        {
            if (PseudoBitmap != null)
            {
                string text = "X: " + ((int)pt.X).ToString("0000") + ", Y: " + ((int)pt.Y).ToString();

                if (scalablePictureBox.PseudoBitmap.IsSrcGray)
                    text += ", Value: " + PseudoBitmap.GetPixelRawValue(pt);
                label.Text = text;
            }
            scalablePictureBox.Refresh();
            if (MouseMove2 != null)
                return MouseMove2(sender, e, pt);
            else
                return false;
        }

        /// <summary>
        /// 輝度が変更された時のイベントハンドラー
        /// </summary>
        public event EventHandler BrightnessAndColorChanged;

        public delegate void DrawingAreaChangedEvent(object sender, double zoom, PointD center);

        public event DrawingAreaChangedEvent DrawingAreaChanged;

        private void scalablePictureBox_DrawingAreaChanged(object sender, double zoom, PointD center)
        {
            DrawingAreaChanged?.Invoke(sender, zoom, center);
        }

        /// <summary>
        /// ステータスラベルやプログレスバーが変更されたとき（現在はフィルター処理のために使用）
        /// </summary>
        public event EventHandler StatusChanged;

        /// <summary>
        /// Filter(Gaussian BlurやDust & Scrach)が変更されたとき
        /// </summary>
        public event EventHandler FilterChanged;


        #endregion イベント

        //画像が読み込まれた時の初期化作業
        private void Initialize()
        {
            if (PseudoBitmap.SrcValuesGrayOriginal == null || PseudoBitmap.SrcValuesGrayOriginal.Length == 0) return;
            double max = PseudoBitmap.SrcValuesGrayOriginal.Max();
            double min = PseudoBitmap.SrcValuesGrayOriginal.Min();

            //まず、頻度ダイアグラムを作成
            if (FrequencyGraphVisible && PseudoBitmap.FrequencyProfile == null)
            {
                var temp = Statistics.GetFrequency(PseudoBitmap.SrcValuesGrayOriginal);
                if (temp == null) return;

                PseudoBitmap.FrequencyProfile = new Profile();
                for (int i = 0; i < temp.Count; i++)
                    PseudoBitmap.FrequencyProfile.Pt.Add(new PointD(temp.Keys[i], temp.Values[i]));
                //見やすいように上限、下限を広げる
                PseudoBitmap.FrequencyProfile.Pt.Insert(0, new PointD(min - (max - min) * 0.05, 0));
                PseudoBitmap.FrequencyProfile.Pt.Add(new PointD(max + (max - min) * 0.05, 0));
            }

            //画像のスケールの判定
            skipEvent = true;
            comboBoxGradient.SelectedIndex = PseudoBitmap.IsNegative ? 1 : 0;
            comboBoxScale2.SelectedIndex = PseudoBitmap.GrayScale ? 0 : 1;
            if (PseudoBitmap.GrayScale)
                comboBoxScale1.SelectedIndex = PseudoBitmap.ColorScale == PseudoBitmap.ColorScaleGrayLog ? 0 : 1;
            else
                comboBoxScale1.SelectedIndex = PseudoBitmap.ColorScale == PseudoBitmap.ColorScaleColdWarmLog ? 0 : 1;

            //Blurの判定
            //checkBoxGaussianBlur.Checked = PseudoBitmap.BlurMode == Crystallography.PseudoBitmap.BlurModeEnum.Gaussian;
            //numericUpDownGaussianRadius.Value = (decimal)PseudoBitmap.BlurRadius;

            skipEvent = false;

            graphControl.Profile = PseudoBitmap.FrequencyProfile;
            graphControl.UpperX = graphControl.MaximalX;
            graphControl.LowerX = graphControl.MinimalX;

            //最大、最小強度などを設定
            double upper = PseudoBitmap.MaxValue;
            double lower = PseudoBitmap.MinValue;

            if (upper > max || upper < min || upper < lower)
                upper = max;

            if (lower < min || lower < min || lower > upper)
                lower = min;

            graphControl.LineList = new[] { new PointD(lower, double.NaN), new PointD(upper, double.NaN) };

            //書き換えの干渉が起こっているようなので、同じのを繰り返す
            MaximumIntensity = max;
            MinimumIntensity = min;
            MaximumIntensity = max;
            UpperIntensity = upper;
            LowerIntensity = lower;
            UpperIntensity = upper;
            if (PseudoBitmap.MaxValue != upper)
                PseudoBitmap.MaxValue = upper;
            if (PseudoBitmap.MinValue != lower)
                PseudoBitmap.MinValue = lower;
            this.Refresh();
            graphControl.Draw();
        }

        private bool skipEvent = false;

        private void graphControl_LinePositionChanged()
        {
            if (skipEvent) return;
            if (graphControl.LineList[1].X == graphControl.LineList[0].X) return;
            skipEvent = true;
            LowerIntensity = Math.Min(graphControl.LineList[0].X, graphControl.LineList[1].X);
            UpperIntensity = Math.Max(graphControl.LineList[0].X, graphControl.LineList[1].X);
            skipEvent = false;
        }

        private bool trackBarAdvancedMinimum_ValueChanged(object sender, double value)
        {
            if (skipEvent) return true;
            skipEvent = true;
            LowerIntensity = trackBarAdvancedMinimum.Value;
            skipEvent = false;
            return default;
        }

        private bool trackBarAdvancedMaximum_ValueChanged(object sender, double value)
        {
            if (skipEvent) return true;
            skipEvent = true;
            UpperIntensity = trackBarAdvancedMaximum.Value;
            skipEvent = false;
            return default;
        }

        public void DrawPictureBox()
        {
            scalablePictureBox.drawPictureBox();
        }

        private void comboBoxScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (skipEvent) return;
            PseudoBitmap.IsNegative = comboBoxGradient.SelectedIndex == 1;
            if (comboBoxScale1.SelectedIndex == 0 && comboBoxScale2.SelectedIndex == 0)//Log & Gray
                PseudoBitmap.SetScaleGray(false);
            else if (comboBoxScale1.SelectedIndex == 1 && comboBoxScale2.SelectedIndex == 0)//Liner & Gray
                PseudoBitmap.SetScaleGray(true);
            else if (comboBoxScale1.SelectedIndex == 0 && comboBoxScale2.SelectedIndex == 1)//log & Color
                PseudoBitmap.SetScaleColdWarm(false);
            else if (comboBoxScale1.SelectedIndex == 1 && comboBoxScale2.SelectedIndex == 1)//Liner & Color
                PseudoBitmap.SetScaleColdWarm(true);

            scalablePictureBox.drawPictureBox();
            BrightnessAndColorChanged?.Invoke(sender, e);
        }

        #region ImageFilter関連

        private void imageFilterProperty_Changed(object sender, EventArgs e)
        {
            if (skipEvent) return;

            numericBoxGaussianFWHM.Visible = checkBoxGaussianBlur.Checked;
            //PseudoBitmap.BlurRadius = numericBoxGaussianRadius.Value;

            numericBoxDustScratchesRadius.Visible = numericBoxDustScratchesThreshold.Visible = checkBoxDustScratches.Checked;
            //PseudoBitmap.BlurMode = flowLayoutPanelGaussianBlur2.Enabled ? Crystallography.PseudoBitmap.BlurModeEnum.Gaussian : Crystallography.PseudoBitmap.BlurModeEnum.None;
            ProcessImageFilter();
            FilterChanged?.Invoke(sender, e);
        }

        private void ProcessImageFilter()
        {
            Stopwatch sw = new Stopwatch();

            bool originalFlag = true;
            StatusLabel = "Elapsed time:    ";
            sw.Start();
            if (checkBoxDustScratches.Checked)
            {
                PseudoBitmap.SetDustAndScratches(numericBoxDustScratchesRadius.Value, numericBoxDustScratchesThreshold.Value, originalFlag);
                originalFlag = false;
                StatusLabel += "Dust && Scratches: " + (sw.ElapsedMilliseconds / 1000.0).ToString("f3") + "msec.  ";
            }
            sw.Restart();
            if (checkBoxGaussianBlur.Checked)
            {
                PseudoBitmap.SetBlurImage(numericBoxGaussianFWHM.Value/2, PseudoBitmap.BlurModeEnum.Gaussian, originalFlag);
                originalFlag = false;
                StatusLabel += "Gaussian Blur: " + (sw.ElapsedMilliseconds / 1000.0).ToString("f3") + "msec.  ";
            }
            if (originalFlag)
            {
                PseudoBitmap.SetOriginalGray();
                StatusLabel = "";
            }

            scalablePictureBox.drawPictureBox();
        }

        #endregion ImageFilter関連

        public event PaintEventHandler Paint2;

        private void scalablePictureBox_Paint2(object sender, PaintEventArgs e)
        {
            if (DesignMode)
                return;

            Paint2?.Invoke(sender, e);
        }

        private void buttonCopyToClipBoard_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(PseudoBitmap.GetImage(new RectangleD(0, 0, PseudoBitmap.Width, PseudoBitmap.Height), new Size(PseudoBitmap.Width, PseudoBitmap.Height)));
        }

        public void ReadImage(string fileName)
        {
            if (ImageIO.ReadImage(fileName))
            {
                PseudoBitmap = new PseudoBitmap(Ring.Intensity.ToArray(), Ring.SrcImgSize.Width);

                ProcessImageFilter();


            }
        }

        private void ScalablePictureBoxAdvanced_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            Array.Sort(fileName);

            //まず、フォルダかどうかを判断する
            if (fileName.Length == 1 && Directory.Exists(fileName[0]))
            {//フォルダの場合
            }
            else
            {//ファイル群の場合
                ReadImage(fileName[0]);
            }
        }

        private void ScalablePictureBoxAdvanced_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.Data.GetData(DataFormats.FileDrop) != null) ? DragDropEffects.Copy : DragDropEffects.None;
        }



        #region 座標変換関連 scalablePictureBoxで定義されているものを呼び出すだけ
        /// <summary>
        /// クライアントのPointをソースのPointに変換
        /// </summary>
        /// <param name="clientPt"></param>
        /// <returns></returns>
        public PointD ConvertToSrcPt(Point clientPt) => scalablePictureBox.ConvertToSrcPt(clientPt);

        /// <summary>
        /// クライアントのPointDをソースのPointDに変換
        /// </summary>
        /// <param name="clientPt"></param>
        /// <returns></returns>
        public PointD ConvertToSrcPt(PointD clientPt) => scalablePictureBox.ConvertToSrcPt(clientPt);

        /// <summary>
        /// クライアントのRectangleをSrcのRectangleに変換
        /// </summary>
        /// <param name="clientRect"></param>
        /// <returns></returns>
        public RectangleD ConvertToSrcRect(Rectangle clientRect) => scalablePictureBox.ConvertToSrcRect(clientRect);

        /// <summary>
        /// クライアントのRectangleDをSrcのRectangleDに変換
        /// </summary>
        /// <param name="clientRect"></param>
        /// <returns></returns>
        public RectangleD ConvertToSrcRect(RectangleD clientRect) => scalablePictureBox.ConvertToSrcRect(clientRect);

        /// <summary>
        /// ソースのPointDをクライアントのPointDに変換
        /// </summary>
        /// <param name="srcPt"></param>
        /// <returns></returns>
        public PointD ConvertToClientPt(PointD srcPt) => scalablePictureBox.ConvertToClientPt(srcPt);

        /// <summary>
        /// ソースのRectangleDをクライアントのRectangleDに変換
        /// </summary>
        /// <param name="srcRect"></param>
        /// <returns></returns>
        public RectangleD ConvertToClientRect(RectangleD srcRect) => scalablePictureBox.ConvertToClientRect(srcRect);
        #endregion 座標変換関連
    }
}