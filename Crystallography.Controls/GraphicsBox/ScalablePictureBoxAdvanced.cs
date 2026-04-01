using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Crystallography.Controls;

[Serializable]
public partial class ScalablePictureBoxAdvanced : CaptureUserControlBase
{
    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public bool SkipDrawing { get => scalablePictureBox.SkipDrawing; set => scalablePictureBox.SkipDrawing = value; }

    public ScalablePictureBoxAdvanced()
    {

        InitializeComponent();

        graphControl.VerticalLines = new[] { new PointD(0, 0), new PointD(0, 0) };
        comboBoxGradient.SelectedIndex = 0;
        comboBoxScale1.SelectedIndex = 1;
        comboBoxScale2.SelectedIndex = 0;
    }

    #region プロパティ

    /// <summary>VisualStudioデザイナーの編集の時はTrue</summary>
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

    /// <summary>スクロールバーが表示されているかどうか</summary>
    public bool ScrollBarVisible { get => scalablePictureBox.ScrollBarVisible; }

    /// <summary>スクロールバーをログスケールで動かすかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool LogScaleBar { get => trackBarAdvancedMaximum.LogScrollBar; set => trackBarAdvancedMaximum.LogScrollBar = trackBarAdvancedMinimum.LogScrollBar = value; }

    /// <summary>描画しているソース画像の範囲を取得/設定する</summary>
    public RectangleD DrawingArea { get => scalablePictureBox.DrawingArea; }

    /// <summary>マウス位置の情報を表示するかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool MousePositionLabelVisible
    {
        get => label.Visible;
        set
        {
            label.Visible = value;
            UpdateUpperPanelVisibility(); // (260322Ch) panelUpper の表示条件を helper へ寄せる
        }
    }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool MagInfoVisible { set => panelMagInfo.Visible = value; get => panelMagInfo.Visible; }

    /// <summary>コピーボタンを表示するかどうか</summary>
    //public bool CopyButtonVisible
    //{
    //    get => buttonCopyToClipBoard.Visible;
    //    set
    //    {
    //        buttonCopyToClipBoard.Visible = value;
    //        panelUpper.Visible = MousePositionLabelVisible || CopyButtonVisible;
    //    }
    //}

    /// <summary> Polarity, Scale, Colorを表示するかどうか (互換性確保のために残している) </summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Gradient")]
    public bool ShowGradiaent { get=> flowLayoutPanelGradient.Visible; set=> flowLayoutPanelGradient.Visible=value; }

    [Category("Gradient")]
    /// <summary>Polarity, Scale, Colorを表示するかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool GradiaentVisible { get => flowLayoutPanelGradient.Visible; set => flowLayoutPanelGradient.Visible = value; }

    /// <summary>スケール(Log, Linear) 切り替えコンボボックスを表示するかどうか </summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Gradient")]
    public bool ScaleVisible { set => flowLayoutPanelScale.Visible = value; get => flowLayoutPanelScale.Visible; }

    /// <summary>カラー切り替えコンボボックスを表示するかどうか </summary>
    // (260322Ch) WFO1000: Microsoft ??????????????????? ???????????
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Gradient")]
    public bool ColorVisible { set => flowLayoutPanelColor.Visible = value; get => flowLayoutPanelColor.Visible; }
   
    /// <summary>ネガ/ポジ切り替えコンボボックスを表示するかどうか </summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Gradient")]
    public bool PolarityVisible { set => flowLayoutPanelPolarity.Visible = value; get => flowLayoutPanelPolarity.Visible; }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool TrackBarVisible { set => panelTrackBar.Visible = value; get => panelTrackBar.Visible; }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool FrequencyGraphVisible { set => graphControl.Visible = value; get => graphControl.Visible; }

    [Category("Image Filter")]
    /// <summary>ImageFilterを有効にするかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool ImageFilterVisible { set => flowLayoutPanelImageFilter.Visible = value; get => flowLayoutPanelImageFilter.Visible; }

    [Category("Image Filter")]
    /// <summary>GaussianFilterを有効にするかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool ImageFilter_GaussianBlur { set => checkBoxGaussianBlur.Checked = value; get => checkBoxGaussianBlur.Checked; }

    [Category("Image Filter")]
    /// <summary>GaussianFilterを有効にするかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool ImageFilter_GaussianBlurVisible { set => checkBoxGaussianBlur.Visible = value; get => checkBoxGaussianBlur.Visible; }

    [Category("Image Filter")]
    /// <summary>GaussianFilterの</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public double ImageFilter_GaussianBlurRadius { set => numericBoxGaussianFWHM.Value = value; get => numericBoxGaussianFWHM.Value; }

    [Category("Image Filter")]
    /// <summary>Dust＆Scratchesを有効にするかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool ImageFilter_DustAndScratches { set => checkBoxDustScratches.Checked = value; get => checkBoxDustScratches.Checked; }

    [Category("Image Filter")]
    /// <summary>Dust＆Scratchesを有効にするかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool ImageFilter_DustAndScratchesVisible { set => checkBoxDustScratches.Visible = value; get => checkBoxDustScratches.Visible; }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Image Filter")]
    public double ImageFilter_DustAndScratchesRadius { set => numericBoxDustScratchesRadius.Value = value; get => numericBoxDustScratchesRadius.Value; }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Image Filter")]
    public double ImageFilter_DustAndScratchesThreshold { set => numericBoxDustScratchesThreshold.Value = value; get => numericBoxDustScratchesThreshold.Value; }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool VisibleGradient { set => flowLayoutPanelGradient.Visible = value; get => flowLayoutPanelGradient.Visible; }

    //[System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)] // 260329Cl 変更: Visible→Hidden+ReadOnly (Designer.csへのシリアライズを抑止。resources.ApplyResources()で設定したSizeを上書きしてしまうため)
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    [System.ComponentModel.Browsable(true)]
    [System.ComponentModel.ReadOnly(true)]
    [System.ComponentModel.Description("現在の画像表示領域サイズ (読み取り専用)。サイズ変更はコントロールのSizeプロパティで行ってください。")]
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
        get => scalablePictureBox.PseudoBitmap;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public (double Zoom, PointD Center) ZoomAndCenter
    {
        set { scalablePictureBox.ZoomAndCenter = value; }
        get { return scalablePictureBox.ZoomAndCenter; }
    }

    /// <summary>ZoomやCenter位置を固定するかどうか</summary>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool FixZoomAndCenter { get => scalablePictureBox.FixZoomAndCenter; set => scalablePictureBox.FixZoomAndCenter = value; }

    /// <summary>新しい画像を設定したとき、現在の強度レンジをその画像のデータ範囲へクランプするかどうか</summary>
    /// <remarks>
    /// true のときは、新しい画像の実データ範囲 <c>[dataMin, dataMax]</c> に合わせて
    /// <c>LowerIntensity</c> / <c>UpperIntensity</c> を調整する。 false のときは、
    /// 既存の <c>LowerIntensity</c> / <c>UpperIntensity</c> を優先して保持し、
    /// それらを含めるように <c>MinimumIntensity</c> / <c>MaximumIntensity</c> を広げる。 // (260322Ch)
    /// </remarks>
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Intensity")]
    // public bool ClampIntensityRange { get; set; } = true; // (260322Ch) 旧名
    public bool ClampIntensityRangeToNewData { get; set; } = true; // (260322Ch) 既定では従来どおり新しい画像データ範囲へクランプする


    #region  Intensity
  
    private double upperIntensity = 255;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Intensity")]
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

            UpdateIntensityGraphPosition(isUpper: true, value); // (260322Ch) graph 側の同期処理を共通化する

            PseudoBitmap.MaxValue = value;
            RedrawScalablePictureBox(); // (260322Ch) 再描画経路を一箇所に揃える

            BrightnessAndColorChanged?.Invoke(this, EventArgs.Empty); // (260322Ch) 共通 EventArgs を使う
        }
        get => upperIntensity;
    }

    private double lowerIntensity = 0;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Intensity")]
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

            UpdateIntensityGraphPosition(isUpper: false, value); // (260322Ch) lower 側も同じ helper で graph と同期する

            PseudoBitmap.MinValue = value;
            RedrawScalablePictureBox(); // (260322Ch) 再描画経路を一箇所に揃える

            BrightnessAndColorChanged?.Invoke(this, EventArgs.Empty); // (260322Ch) 共通 EventArgs を使う
        }
        get => lowerIntensity;
    }

    //画像中の最大強度
    private double maximumIntensity = 255;

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Intensity")]
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

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Intensity")]
    public double MinimumIntensity
    {
        get => minimumIntensity;
        set
        {
            minimumIntensity = value;
            trackBarAdvancedMinimum.Minimum = trackBarAdvancedMaximum.Minimum = MinimumIntensity;
        }
    }

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    [Category("Intensity")]
    public int DecimalPlacesForIntensity { get => trackBarAdvancedMaximum.DecimalPlaces; set => trackBarAdvancedMaximum.DecimalPlaces = trackBarAdvancedMinimum.DecimalPlaces = value; }

    #endregion

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ScalablePictureBox ScalablePictureBox => scalablePictureBox;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<ScalablePictureBox.Symbol> Symbols { get => scalablePictureBox.Symbols; set => scalablePictureBox.Symbols = value; }

    private double progress = 0;

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
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

    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
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

    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool StatusVisible { set => statusStrip1.Visible = value; get => statusStrip1.Visible; }


    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Visible)]
    public bool TitleVisible { get => scalablePictureBox.TitleVisible; set => scalablePictureBox.TitleVisible = value; }
    [System.ComponentModel.Browsable(false)]
    [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
    public (string Text, Font Font, Color Color1, Color Color2) Title { get => scalablePictureBox.Title; set => scalablePictureBox.Title = value; }


    #endregion プロパティ

    private void UpdateUpperPanelVisibility()
    {
        panelUpper.Visible = MousePositionLabelVisible; // (260322Ch) 現状の表示条件を一箇所へ寄せ、将来 CopyButtonVisible 復帰時も直しやすくする
    }

    private void UpdateResolutionLabel()
    {
        labelResolution.Text = scalablePictureBox.Zoom.ToString("g4"); // (260322Ch) ズーム表示更新を helper 化する
    }

    private void RedrawScalablePictureBox()
    {
        scalablePictureBox.drawPictureBox(); // (260322Ch) drawPictureBox 呼び出し箇所を共通化する
    }

    private bool InvokeMouseEventHandler(MouseEvent handler, object sender, MouseEventArgs e, PointD pt)
        => handler != null && handler(sender, e, pt); // (260322Ch) 中継イベントの null 判定を共通化する

    private void UpdateIntensityGraphPosition(bool isUpper, double value)
    {
        int index = isUpper
            ? (graphControl.VerticalLines[0].X > graphControl.VerticalLines[1].X ? 0 : 1)
            : (graphControl.VerticalLines[0].X < graphControl.VerticalLines[1].X ? 0 : 1);
        graphControl.VerticalLines[index].X = value;
        graphControl.Draw(); // (260322Ch) graph の vertical line 再描画を一箇所へ集約する
    }

    private void ApplyScaleSelectionToPseudoBitmap()
    {
        var linear = comboBoxScale1.SelectedIndex == 1;

        if (comboBoxScale2.SelectedIndex == 0)//Gray
            PseudoBitmap.SetScaleGray(linear);
        else if (comboBoxScale2.SelectedIndex == 1)//Cold-Warm
            PseudoBitmap.SetScaleColdWarm(linear);
        else if (comboBoxScale2.SelectedIndex == 2)//Spectrum
            PseudoBitmap.SetScaleSpectrum(linear);
        else if (comboBoxScale2.SelectedIndex == 3)//Fire
            PseudoBitmap.SetScaleFire(linear);
    }

    private void SyncScaleSelectionFromPseudoBitmap()
    {
        comboBoxGradient.SelectedIndex = PseudoBitmap.IsNegative ? 1 : 0;
        comboBoxScale2.SelectedIndex = PseudoBitmap.GrayScale ? 0 : 1;
        if (PseudoBitmap.GrayScale)
            comboBoxScale1.SelectedIndex = PseudoBitmap.ColorScale == PseudoBitmap.ColorScaleGrayLog ? 0 : 1;
        else
            comboBoxScale1.SelectedIndex = PseudoBitmap.ColorScale == PseudoBitmap.ColorScaleColdWarmLog ? 0 : 1; // (260322Ch) 現状サポートする scale との対応を helper へまとめる
    }

    private void ApplyIntensityState(double min, double max, double lower, double upper)
    {
        // MaximumIntensity = max;
        // MinimumIntensity = min;
        // MaximumIntensity = max;
        // UpperIntensity = upper;
        // LowerIntensity = lower;
        // UpperIntensity = upper;
        MaximumIntensity = max;
        MinimumIntensity = min;
        MaximumIntensity = max; // (260322Ch) 旧コードの再代入を helper へ閉じ込め、trackbar/designer 同期の癖を維持する
        UpperIntensity = upper;
        LowerIntensity = lower;
        UpperIntensity = upper;
        if (PseudoBitmap.MaxValue != upper)
            PseudoBitmap.MaxValue = upper;
        if (PseudoBitmap.MinValue != lower)
            PseudoBitmap.MinValue = lower;
    }
    /// <summary>画像更新時に使う intensity 関連 4 値を決める。</summary>
    /// <remarks>
    /// 返り値の <c>Lower</c> / <c>Upper</c> は実際に表示へ使うレンジ、
    /// <c>Minimum</c> / <c>Maximum</c> は trackbar が取り得る許容範囲。
    /// <c>ClampIntensityRangeToNewData</c> が false のときは、新データ範囲より外でも
    /// 既存の表示レンジを維持できるように <c>Minimum</c> / <c>Maximum</c> を広げる。 // (260322Ch)
    /// </remarks>
    private (double Minimum, double Maximum, double Lower, double Upper) ResolveIntensityRangeOnImageUpdate(double dataMin, double dataMax, double requestedLower, double requestedUpper)
    {
        var lower = requestedLower;
        var upper = requestedUpper;

        // if (ClampIntensityRange) // (260322Ch) 旧名
        if (ClampIntensityRangeToNewData)
        {
            // if (upper > dataMax || upper < dataMin || upper < lower)
            //     upper = dataMax;
            if (upper > dataMax || upper < dataMin || upper < lower)
                upper = dataMax; // (260322Ch) 既定では従来どおり新しい画像の上限へ合わせる

            // if (lower < dataMin || lower < dataMin || lower > upper)
            //     lower = dataMin;
            if (lower < dataMin || lower < dataMin || lower > upper)
                lower = dataMin; // (260322Ch) 既定では従来どおり新しい画像の下限へ合わせる

            return (dataMin, dataMax, lower, upper);
        }

        if (upper < lower)
        {
            // upper = dataMax;
            // lower = dataMin;
            (lower, upper) = (Math.Min(lower, upper), Math.Max(lower, upper)); // (260322Ch) クランプ無効時でもレンジの前後関係だけは保つ
        }

        // minimum / maximum は新データ範囲と表示レンジの外側を採用し、
        // lower / upper 自体は既存表示レンジをそのまま維持する。 // (260322Ch)
        return (Math.Min(dataMin, lower), Math.Max(dataMax, upper), lower, upper); // (260322Ch) 現在レンジを維持できるよう trackbar 許容範囲も拡張する
    }

    private void UpdateIntensityGraphBounds(double minimum, double maximum)
    {
        graphControl.MinimalX = Math.Min(graphControl.MinimalX, minimum); // (260322Ch) クランプ無効時は保持レンジも histogram 上に載るようにする
        graphControl.MaximalX = Math.Max(graphControl.MaximalX, maximum);
        graphControl.LowerX = graphControl.MinimalX;
        graphControl.UpperX = graphControl.MaximalX;
    }

    private void UpdateMousePositionInfo(PointD pt)
    {
        if (PseudoBitmap == null)
            return;

        string text = "X: " + ((int)pt.X).ToString("0000") + ", Y: " + ((int)pt.Y).ToString();
        if (scalablePictureBox.PseudoBitmap.IsSrcGray)
            text += ", Value: " + PseudoBitmap.GetPixelRawValue(pt);
        label.Text = text;
        UpdateResolutionLabel(); // (260322Ch) mouse move と倍率ボタンのズーム表示更新を同じ helper へ寄せる
    }

    #region イベント

    /// <summary>マウスイベント用のデリゲート</summary>
    /// <param name="sender">sender</param>
    /// <param name="e">e</param>
    /// <param name="pt">ソース画像座標</param>
    /// <returns>その後のイベントをキャンセルする場合: true</returns>
    public delegate bool MouseEvent(object sender, MouseEventArgs e, PointD pt);

    public event MouseEvent MouseMove2;

    public event MouseEvent MouseUp2;

    public event MouseEvent MouseDown2;

    public event MouseEvent MouseWheel2;

    /// <summary>マウスが押されたとき。コントロール本体でのその後のイベントをキャンセルする場合、返り値はtrue</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="pt"></param>
    /// <returns></returns>
    private bool scalablePictureBox_MouseDown2(object sender, MouseEventArgs e, PointD pt)
    {
        return InvokeMouseEventHandler(MouseDown2, sender, e, pt); // (260322Ch) event 中継の null 判定を helper 化する
    }

    /// <summary>マウスが上がったとき。コントロール本体でのその後のイベントをキャンセルする場合、返り値はtrue</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="pt"></param>
    /// <returns></returns>
    private bool scalablePictureBox_MouseUp2(object sender, MouseEventArgs e, PointD pt)
    {
        return InvokeMouseEventHandler(MouseUp2, sender, e, pt); // (260322Ch) event 中継の null 判定を helper 化する
    }

    /// <summary>マウスホイールが回ったとき。コントロール本体でのその後のイベントをキャンセルする場合、返り値はtrue</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="pt"></param>
    /// <returns></returns>
    private bool scalablePictureBox_MouseWheel2(object sender, MouseEventArgs e, PointD pt)
    {
        return InvokeMouseEventHandler(MouseWheel2, sender, e, pt); // (260322Ch) event 中継の null 判定を helper 化する
    }

    /// <summary>マウスが動いた時に画面上部にマウス位置、強度を示す。コントロール本体でのその後のイベントをキャンセルする場合、返り値はtrue</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="pt"></param>
    /// <returns></returns>
    private bool scalablePictureBox1_MouseMove2(object sender, MouseEventArgs e, PointD pt)
    {
        UpdateMousePositionInfo(pt); // (260322Ch) マウス位置ラベル更新を helper へまとめる
        scalablePictureBox.Refresh();
        return InvokeMouseEventHandler(MouseMove2, sender, e, pt); // (260322Ch) event 中継の null 判定を helper 化する
    }

    /// <summary>輝度が変更された時のイベントハンドラー</summary>
    public event EventHandler BrightnessAndColorChanged;

    public delegate void DrawingAreaChangedEvent(object sender, double zoom, PointD center);

    public event DrawingAreaChangedEvent DrawingAreaChanged;

    private void scalablePictureBox_DrawingAreaChanged(object sender, double zoom, PointD center)
    {
        DrawingAreaChanged?.Invoke(sender, zoom, center);
    }

    /// <summary>ステータスラベルやプログレスバーが変更されたとき（現在はフィルター処理のために使用）</summary>
    public event EventHandler StatusChanged;

    /// <summary>Filter(Gaussian BlurやDust & Scrach)が変更されたとき</summary>
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
        SyncScaleSelectionFromPseudoBitmap(); // (260322Ch) UI への scale 反映を helper へ集約する

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

        // if (upper > max || upper < min || upper < lower)
        //     upper = max;
        // if (lower < min || lower < min || lower > upper)
        //     lower = min;
        var (minimumIntensity, maximumIntensity, resolvedLower, resolvedUpper)
            = ResolveIntensityRangeOnImageUpdate(min, max, lower, upper); // (260322Ch) 画像更新時の intensity clamp 可否をプロパティで切り替える
        lower = resolvedLower;
        upper = resolvedUpper;
        UpdateIntensityGraphBounds(minimumIntensity, maximumIntensity); // (260322Ch) 保持した intensity range を graph / trackbar 両方で扱えるようにする

        graphControl.VerticalLines = [new PointD(lower, double.NaN), new PointD(upper, double.NaN)];

        //書き換えの干渉が起こっているようなので、同じのを繰り返す
        ApplyIntensityState(minimumIntensity, maximumIntensity, lower, upper); // (260322Ch) clamp 無効時は保持レンジを含む trackbar 範囲で初期化する
        UpdateResolutionLabel(); // (260322Ch) 初期化直後の倍率表示もここで揃える
        this.Refresh();
        graphControl.Draw();
    }

    private bool skipEvent = false;

    private void graphControl_LinePositionChanged()
    {
        if (skipEvent) return;
        if (graphControl.VerticalLines[1].X == graphControl.VerticalLines[0].X) return;
        skipEvent = true;
        LowerIntensity = Math.Min(graphControl.VerticalLines[0].X, graphControl.VerticalLines[1].X);
        UpperIntensity = Math.Max(graphControl.VerticalLines[0].X, graphControl.VerticalLines[1].X);
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
        if (!SkipDrawing)
            RedrawScalablePictureBox(); // (260322Ch) public wrapper も helper を通す
    }

    private void comboBoxScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (skipEvent) return;
        PseudoBitmap.IsNegative = comboBoxGradient.SelectedIndex == 1;
        ApplyScaleSelectionToPseudoBitmap(); // (260322Ch) combo box 選択からの scale 適用を helper へまとめる
        RedrawScalablePictureBox();
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

    public void ProcessImageFilter()
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
            PseudoBitmap.SetBlurImage(numericBoxGaussianFWHM.Value / 2, PseudoBitmap.BlurModeEnum.Gaussian, originalFlag);
            originalFlag = false;
            StatusLabel += "Gaussian Blur: " + (sw.ElapsedMilliseconds / 1000.0).ToString("f3") + "msec.  ";
        }
        if (originalFlag)
        {
            PseudoBitmap.SetOriginalGray();
            StatusLabel = "";
        }

        RedrawScalablePictureBox(); // (260322Ch) filter 後の再描画も helper 経由に揃える
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
    /// <summary>クライアントのPointをソースのPointに変換</summary>
    /// <param name="clientPt"></param>
    /// <returns></returns>
    public PointD ConvertToSrcPt(Point clientPt) => scalablePictureBox.ConvertToSrcPt(clientPt);

    /// <summary>クライアントのPointDをソースのPointDに変換</summary>
    /// <param name="clientPt"></param>
    /// <returns></returns>
    public PointD ConvertToSrcPt(PointD clientPt) => scalablePictureBox.ConvertToSrcPt(clientPt);

    /// <summary>クライアントのRectangleをSrcのRectangleに変換</summary>
    /// <param name="clientRect"></param>
    /// <returns></returns>
    public RectangleD ConvertToSrcRect(Rectangle clientRect) => scalablePictureBox.ConvertToSrcRect(clientRect);

    /// <summary>クライアントのRectangleDをSrcのRectangleDに変換</summary>
    /// <param name="clientRect"></param>
    /// <returns></returns>
    public RectangleD ConvertToSrcRect(RectangleD clientRect) => scalablePictureBox.ConvertToSrcRect(clientRect);

    /// <summary>ソースのPointDをクライアントのPointDに変換</summary>
    /// <param name="srcPt"></param>
    /// <returns></returns>
    public PointD ConvertToClientPt(PointD srcPt) => scalablePictureBox.ConvertToClientPt(srcPt);

    /// <summary>ソースのRectangleDをクライアントのRectangleDに変換</summary>
    /// <param name="srcRect"></param>
    /// <returns></returns>
    public RectangleD ConvertToClientRect(RectangleD srcRect) => scalablePictureBox.ConvertToClientRect(srcRect);
    #endregion 座標変換関連

    #region 画像の保存/コピー関連 scalablePictureBoxで定義されているものを呼び出すだけ
    public void SaveAsPNG() => scalablePictureBox.SaveAsPNG();

    public void CopyAsBitmap() => scalablePictureBox.CopyAsBitmap();

    public void SaveAsMetafile() => scalablePictureBox.SaveAsMetafile();

    public void copyAsMetafile() => scalablePictureBox.CopyAsMetafile();

    #endregion

    private void buttonMag_Click(object sender, EventArgs e)
    {
        var name = (sender as Button).Name;
        if (name.Contains("Mag1"))
            scalablePictureBox.Zoom = 1;
        else if (name.Contains("Mag2"))
            scalablePictureBox.Zoom = 2;
        else if (name.Contains("Mag4"))
            scalablePictureBox.Zoom = 4;
        else if (name.Contains("Mag_2"))
            scalablePictureBox.Zoom = 0.5;
        else if (name.Contains("Mag_4"))
            scalablePictureBox.Zoom = 0.25;
        else if (name.Contains("Mag_8"))
            scalablePictureBox.Zoom = 0.125;
        else if (name.Contains("Mag_16"))
            scalablePictureBox.Zoom = 0.0625;
        UpdateResolutionLabel(); // (260322Ch) 倍率ボタン後の表示更新を helper へ寄せる
    }
}

