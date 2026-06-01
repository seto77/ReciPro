namespace Crystallography.Controls
{
    partial class ScalablePictureBoxAdvanced
    {
        /// <summary>必要なデザイナー変数です。</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>使用中のリソースをすべてクリーンアップします。</summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container(); // (260531Ch)
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScalablePictureBoxAdvanced));
            panelTrackBar = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            trackBarAdvancedMinimum = new TrackBarAdvanced();
            trackBarAdvancedMaximum = new TrackBarAdvanced();
            label = new System.Windows.Forms.Label();
            comboBoxScale2 = new System.Windows.Forms.ComboBox();
            comboBoxScale1 = new System.Windows.Forms.ComboBox();
            comboBoxGradient = new System.Windows.Forms.ComboBox();
            label7 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            flowLayoutPanelGradient = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelPolarity = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelScale = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelColor = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelImageFilter = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelDustScratches = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxDustScratches = new System.Windows.Forms.CheckBox();
            numericBoxDustScratchesRadius = new NumericBox();
            numericBoxDustScratchesThreshold = new NumericBox();
            flowLayoutPanelGaussianBlur2 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxGaussianBlur = new System.Windows.Forms.CheckBox();
            numericBoxGaussianFWHM = new NumericBox();
            panelUpper = new System.Windows.Forms.Panel();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            scalablePictureBox = new ScalablePictureBox();
            graphControl = new GraphControl();
            panelMagInfo = new System.Windows.Forms.Panel();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonMag1 = new System.Windows.Forms.Button();
            buttonMag2 = new System.Windows.Forms.Button();
            buttonMag4 = new System.Windows.Forms.Button();
            buttonMag_2 = new System.Windows.Forms.Button();
            buttonMag_4 = new System.Windows.Forms.Button();
            buttonMag_8 = new System.Windows.Forms.Button();
            buttonMag_16 = new System.Windows.Forms.Button();
            label14 = new System.Windows.Forms.Label();
            labelResolution = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            panelTrackBar.SuspendLayout();
            flowLayoutPanelGradient.SuspendLayout();
            flowLayoutPanelPolarity.SuspendLayout();
            flowLayoutPanelScale.SuspendLayout();
            flowLayoutPanelColor.SuspendLayout();
            flowLayoutPanelImageFilter.SuspendLayout();
            flowLayoutPanelDustScratches.SuspendLayout();
            flowLayoutPanelGaussianBlur2.SuspendLayout();
            panelUpper.SuspendLayout();
            statusStrip1.SuspendLayout();
            panelMagInfo.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelTrackBar
            // 
            resources.ApplyResources(panelTrackBar, "panelTrackBar");
            panelTrackBar.Controls.Add(label2);
            panelTrackBar.Controls.Add(label3);
            panelTrackBar.Controls.Add(trackBarAdvancedMinimum);
            panelTrackBar.Controls.Add(trackBarAdvancedMaximum);
            panelTrackBar.Name = "panelTrackBar";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip")); // 260531Cl
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip")); // 260531Cl
            label3.Name = "label3";
            // 
            // trackBarAdvancedMinimum
            // 
            resources.ApplyResources(trackBarAdvancedMinimum, "trackBarAdvancedMinimum");
            toolTip.SetToolTip(trackBarAdvancedMinimum, resources.GetString("trackBarAdvancedMinimum.ToolTip")); // 260531Cl
            trackBarAdvancedMinimum.ControlHeight = 22;
            trackBarAdvancedMinimum.DecimalPlaces = 0;
            trackBarAdvancedMinimum.LogScrollBar = false;
            trackBarAdvancedMinimum.Maximum = 65D;
            trackBarAdvancedMinimum.Minimum = 0D;
            trackBarAdvancedMinimum.Name = "trackBarAdvancedMinimum";
            trackBarAdvancedMinimum.NumericBoxSize = 84;
            trackBarAdvancedMinimum.Orientation = System.Windows.Forms.Orientation.Vertical;
            trackBarAdvancedMinimum.Smart_Increment = true;
            trackBarAdvancedMinimum.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            trackBarAdvancedMinimum.UpDown_Increment = 1D;
            trackBarAdvancedMinimum.Value = 0D;
            trackBarAdvancedMinimum.ValueChanged += trackBarAdvancedMinimum_ValueChanged;
            // 
            // trackBarAdvancedMaximum
            // 
            resources.ApplyResources(trackBarAdvancedMaximum, "trackBarAdvancedMaximum");
            toolTip.SetToolTip(trackBarAdvancedMaximum, resources.GetString("trackBarAdvancedMaximum.ToolTip")); // 260531Cl
            trackBarAdvancedMaximum.ControlHeight = 22;
            trackBarAdvancedMaximum.DecimalPlaces = 0;
            trackBarAdvancedMaximum.LogScrollBar = false;
            trackBarAdvancedMaximum.Maximum = 65535D;
            trackBarAdvancedMaximum.Minimum = 0D;
            trackBarAdvancedMaximum.Name = "trackBarAdvancedMaximum";
            trackBarAdvancedMaximum.NumericBoxSize = 84;
            trackBarAdvancedMaximum.Orientation = System.Windows.Forms.Orientation.Vertical;
            trackBarAdvancedMaximum.Smart_Increment = true;
            trackBarAdvancedMaximum.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            trackBarAdvancedMaximum.UpDown_Increment = 1D;
            trackBarAdvancedMaximum.Value = 0D;
            trackBarAdvancedMaximum.ValueChanged += trackBarAdvancedMaximum_ValueChanged;
            // 
            // label
            // 
            resources.ApplyResources(label, "label");
            toolTip.SetToolTip(label, resources.GetString("label.ToolTip")); // 260531Cl
            label.Name = "label";
            // 
            // comboBoxScale2
            // 
            comboBoxScale2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale2.FormattingEnabled = true;
            comboBoxScale2.Items.AddRange(new object[] { resources.GetString("comboBoxScale2.Items"), resources.GetString("comboBoxScale2.Items1"), resources.GetString("comboBoxScale2.Items2"), resources.GetString("comboBoxScale2.Items3") });
            resources.ApplyResources(comboBoxScale2, "comboBoxScale2");
            toolTip.SetToolTip(comboBoxScale2, resources.GetString("comboBoxScale2.ToolTip")); // 260531Cl
            comboBoxScale2.Name = "comboBoxScale2";
            comboBoxScale2.SelectedIndexChanged += comboBoxScale_SelectedIndexChanged;
            // 
            // comboBoxScale1
            // 
            comboBoxScale1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale1.FormattingEnabled = true;
            comboBoxScale1.Items.AddRange(new object[] { resources.GetString("comboBoxScale1.Items"), resources.GetString("comboBoxScale1.Items1") });
            resources.ApplyResources(comboBoxScale1, "comboBoxScale1");
            toolTip.SetToolTip(comboBoxScale1, resources.GetString("comboBoxScale1.ToolTip")); // 260531Cl
            comboBoxScale1.Name = "comboBoxScale1";
            comboBoxScale1.SelectedIndexChanged += comboBoxScale_SelectedIndexChanged;
            // 
            // comboBoxGradient
            // 
            comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxGradient.FormattingEnabled = true;
            comboBoxGradient.Items.AddRange(new object[] { resources.GetString("comboBoxGradient.Items"), resources.GetString("comboBoxGradient.Items1") });
            resources.ApplyResources(comboBoxGradient, "comboBoxGradient");
            toolTip.SetToolTip(comboBoxGradient, resources.GetString("comboBoxGradient.ToolTip")); // 260531Cl
            comboBoxGradient.Name = "comboBoxGradient";
            comboBoxGradient.SelectedIndexChanged += comboBoxScale_SelectedIndexChanged;
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip")); // 260531Cl
            label7.Name = "label7";
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            toolTip.SetToolTip(label9, resources.GetString("label9.ToolTip")); // 260531Cl
            label9.Name = "label9";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip")); // 260531Cl
            label5.Name = "label5";
            // 
            // flowLayoutPanelGradient
            // 
            resources.ApplyResources(flowLayoutPanelGradient, "flowLayoutPanelGradient");
            flowLayoutPanelGradient.Controls.Add(flowLayoutPanelPolarity);
            flowLayoutPanelGradient.Controls.Add(flowLayoutPanelScale);
            flowLayoutPanelGradient.Controls.Add(flowLayoutPanelColor);
            flowLayoutPanelGradient.Name = "flowLayoutPanelGradient";
            // 
            // flowLayoutPanelPolarity
            // 
            resources.ApplyResources(flowLayoutPanelPolarity, "flowLayoutPanelPolarity");
            flowLayoutPanelPolarity.Controls.Add(label5);
            flowLayoutPanelPolarity.Controls.Add(comboBoxGradient);
            flowLayoutPanelPolarity.Name = "flowLayoutPanelPolarity";
            // 
            // flowLayoutPanelScale
            // 
            resources.ApplyResources(flowLayoutPanelScale, "flowLayoutPanelScale");
            flowLayoutPanelScale.Controls.Add(label7);
            flowLayoutPanelScale.Controls.Add(comboBoxScale1);
            flowLayoutPanelScale.Name = "flowLayoutPanelScale";
            // 
            // flowLayoutPanelColor
            // 
            resources.ApplyResources(flowLayoutPanelColor, "flowLayoutPanelColor");
            flowLayoutPanelColor.Controls.Add(label9);
            flowLayoutPanelColor.Controls.Add(comboBoxScale2);
            flowLayoutPanelColor.Name = "flowLayoutPanelColor";
            // 
            // flowLayoutPanelImageFilter
            // 
            resources.ApplyResources(flowLayoutPanelImageFilter, "flowLayoutPanelImageFilter");
            flowLayoutPanelImageFilter.Controls.Add(flowLayoutPanelDustScratches);
            flowLayoutPanelImageFilter.Controls.Add(flowLayoutPanelGaussianBlur2);
            flowLayoutPanelImageFilter.Name = "flowLayoutPanelImageFilter";
            // 
            // flowLayoutPanelDustScratches
            // 
            resources.ApplyResources(flowLayoutPanelDustScratches, "flowLayoutPanelDustScratches");
            flowLayoutPanelDustScratches.Controls.Add(checkBoxDustScratches);
            flowLayoutPanelDustScratches.Controls.Add(numericBoxDustScratchesRadius);
            flowLayoutPanelDustScratches.Controls.Add(numericBoxDustScratchesThreshold);
            flowLayoutPanelDustScratches.Name = "flowLayoutPanelDustScratches";
            // 
            // checkBoxDustScratches
            // 
            resources.ApplyResources(checkBoxDustScratches, "checkBoxDustScratches");
            toolTip.SetToolTip(checkBoxDustScratches, resources.GetString("checkBoxDustScratches.ToolTip")); // 260531Cl
            checkBoxDustScratches.Name = "checkBoxDustScratches";
            checkBoxDustScratches.UseVisualStyleBackColor = true;
            checkBoxDustScratches.CheckedChanged += imageFilterProperty_Changed;
            // 
            // numericBoxDustScratchesRadius
            // 
            numericBoxDustScratchesRadius.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDustScratchesRadius.DecimalPlaces = 1;
            numericBoxDustScratchesRadius.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxDustScratchesRadius, "numericBoxDustScratchesRadius");
            toolTip.SetToolTip(numericBoxDustScratchesRadius, resources.GetString("numericBoxDustScratchesRadius.ToolTip")); // 260531Cl
            numericBoxDustScratchesRadius.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDustScratchesRadius.Maximum = 5D;
            numericBoxDustScratchesRadius.Minimum = 0D;
            numericBoxDustScratchesRadius.Name = "numericBoxDustScratchesRadius";
            numericBoxDustScratchesRadius.RadianValue = 0.017453292519943295D;
            numericBoxDustScratchesRadius.ShowUpDown = true;
            numericBoxDustScratchesRadius.SkipEventDuringInput = false;
            numericBoxDustScratchesRadius.ThousandsSeparator = true;
            numericBoxDustScratchesRadius.UpDown_Increment = 0.5D;
            numericBoxDustScratchesRadius.Value = 1D;
            numericBoxDustScratchesRadius.ValueChanged += imageFilterProperty_Changed;
            // 
            // numericBoxDustScratchesThreshold
            // 
            numericBoxDustScratchesThreshold.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDustScratchesThreshold.DecimalPlaces = 0;
            numericBoxDustScratchesThreshold.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxDustScratchesThreshold, "numericBoxDustScratchesThreshold");
            toolTip.SetToolTip(numericBoxDustScratchesThreshold, resources.GetString("numericBoxDustScratchesThreshold.ToolTip")); // 260531Cl
            numericBoxDustScratchesThreshold.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDustScratchesThreshold.Maximum = 10D;
            numericBoxDustScratchesThreshold.Minimum = 0D;
            numericBoxDustScratchesThreshold.Name = "numericBoxDustScratchesThreshold";
            numericBoxDustScratchesThreshold.RadianValue = 0.052359877559829883D;
            numericBoxDustScratchesThreshold.ShowUpDown = true;
            numericBoxDustScratchesThreshold.SkipEventDuringInput = false;
            numericBoxDustScratchesThreshold.SmartIncrement = true;
            numericBoxDustScratchesThreshold.ThousandsSeparator = true;
            numericBoxDustScratchesThreshold.Value = 3D;
            numericBoxDustScratchesThreshold.ValueChanged += imageFilterProperty_Changed;
            // 
            // flowLayoutPanelGaussianBlur2
            // 
            resources.ApplyResources(flowLayoutPanelGaussianBlur2, "flowLayoutPanelGaussianBlur2");
            flowLayoutPanelGaussianBlur2.Controls.Add(checkBoxGaussianBlur);
            flowLayoutPanelGaussianBlur2.Controls.Add(numericBoxGaussianFWHM);
            flowLayoutPanelGaussianBlur2.Name = "flowLayoutPanelGaussianBlur2";
            // 
            // checkBoxGaussianBlur
            // 
            resources.ApplyResources(checkBoxGaussianBlur, "checkBoxGaussianBlur");
            toolTip.SetToolTip(checkBoxGaussianBlur, resources.GetString("checkBoxGaussianBlur.ToolTip")); // 260531Cl
            checkBoxGaussianBlur.Name = "checkBoxGaussianBlur";
            checkBoxGaussianBlur.UseVisualStyleBackColor = true;
            checkBoxGaussianBlur.CheckedChanged += imageFilterProperty_Changed;
            // 
            // numericBoxGaussianFWHM
            // 
            numericBoxGaussianFWHM.BackColor = System.Drawing.SystemColors.Control;
            numericBoxGaussianFWHM.DecimalPlaces = 1;
            numericBoxGaussianFWHM.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxGaussianFWHM, "numericBoxGaussianFWHM");
            toolTip.SetToolTip(numericBoxGaussianFWHM, resources.GetString("numericBoxGaussianFWHM.ToolTip")); // 260531Cl
            numericBoxGaussianFWHM.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxGaussianFWHM.Maximum = 100D;
            numericBoxGaussianFWHM.Minimum = 0D;
            numericBoxGaussianFWHM.Name = "numericBoxGaussianFWHM";
            numericBoxGaussianFWHM.RadianValue = 0.017453292519943295D;
            numericBoxGaussianFWHM.ShowUpDown = true;
            numericBoxGaussianFWHM.SkipEventDuringInput = false;
            numericBoxGaussianFWHM.SmartIncrement = true;
            numericBoxGaussianFWHM.ThousandsSeparator = true;
            numericBoxGaussianFWHM.Value = 1D;
            numericBoxGaussianFWHM.ValueChanged += imageFilterProperty_Changed;
            // 
            // panelUpper
            // 
            panelUpper.Controls.Add(label);
            resources.ApplyResources(panelUpper, "panelUpper");
            panelUpper.Name = "panelUpper";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // scalablePictureBox
            // 
            resources.ApplyResources(scalablePictureBox, "scalablePictureBox");
            scalablePictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            scalablePictureBox.FixZoomAndCenter = false;
            scalablePictureBox.FocusEventEnabled = false;
            scalablePictureBox.HorizontalFlip = false;
            scalablePictureBox.ManualSpotMode = false;
            scalablePictureBox.MouseScaling = true;
            scalablePictureBox.MouseTranslation = true;
            scalablePictureBox.Name = "scalablePictureBox";
            scalablePictureBox.ShowAreaRectangle = false;
            scalablePictureBox.ShowRimRentangle = false;
            scalablePictureBox.TitleVisible = false;
            scalablePictureBox.VerticalFlip = false;
            scalablePictureBox.Zoom = 128D;
            scalablePictureBox.Paint2 += scalablePictureBox_Paint2;
            scalablePictureBox.MouseMove2 += scalablePictureBox1_MouseMove2;
            scalablePictureBox.MouseUp2 += scalablePictureBox_MouseUp2;
            scalablePictureBox.MouseDown2 += scalablePictureBox_MouseDown2;
            scalablePictureBox.MouseWheel2 += scalablePictureBox_MouseWheel2;
            scalablePictureBox.DrawingAreaChanged += scalablePictureBox_DrawingAreaChanged;
            // 
            // graphControl
            // 
            graphControl.AllowMouseOperation = true;
            graphControl.AxisLineColor = System.Drawing.Color.Gray;
            graphControl.AxisTextColor = System.Drawing.Color.Black;
            graphControl.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControl.AxisXTextVisible = true;
            graphControl.AxisYTextVisible = true;
            graphControl.BackgroundColor = System.Drawing.Color.White;
            graphControl.BottomMargin = 0D;
            graphControl.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControl.DivisionLineXVisible = true;
            graphControl.DivisionLineYVisible = true;
            resources.ApplyResources(graphControl, "graphControl");
            graphControl.FixRangeHorizontal = false;
            graphControl.FixRangeVertical = false;
            graphControl.GraphTitle = "";
            graphControl.IsIntegerX = false;
            graphControl.IsIntegerY = false;
            graphControl.LabelX = "X:";
            graphControl.LabelY = "Y:";
            graphControl.LeftMargin = 0F;
            graphControl.LineWidth = 1F;
            graphControl.LowerX = 0D;
            graphControl.LowerY = 0D;
            graphControl.MaximalX = 1D;
            graphControl.MaximalY = 1D;
            graphControl.MinimalX = 0D;
            graphControl.MinimalY = 0D;
            graphControl.Mode = GraphControl.DrawingMode.Histogram;
            graphControl.MousePositionVisible = false;
            graphControl.MousePositionXDigit = -1;
            graphControl.MousePositionYDigit = -1;
            graphControl.Name = "graphControl";
            graphControl.OriginPosition = new System.Drawing.Point(20, 20);
            graphControl.UnitX = "";
            graphControl.UnitY = "";
            // 260518Cl: Designer 再保存で日本語名 "BIZ UDPゴシック" に書き換わったのを英名 "BIZ UDPGothic" に戻す
            // (英語環境ではフォント名解決に失敗し fallback になるため英名固定)。
            graphControl.UpperPanelFont = new System.Drawing.Font("Segoe UI", 7F); // 260522Cl 変更: Yu Gothic UI → Segoe UI (英語GUI)
            graphControl.UpperPanelVisible = false;
            graphControl.UpperX = 1D;
            graphControl.UpperY = 1D;
            graphControl.UseLineWidth = true;
            graphControl.VerticalLineColor = System.Drawing.Color.Red;
            graphControl.XLog = false;
            graphControl.YLog = true;
            graphControl.LinePositionChanged += graphControl_LinePositionChanged;
            // 
            // panelMagInfo
            // 
            resources.ApplyResources(panelMagInfo, "panelMagInfo");
            panelMagInfo.Controls.Add(flowLayoutPanel1);
            panelMagInfo.Controls.Add(label14);
            panelMagInfo.Controls.Add(labelResolution);
            panelMagInfo.Controls.Add(label1);
            panelMagInfo.Name = "panelMagInfo";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(buttonMag1);
            flowLayoutPanel1.Controls.Add(buttonMag2);
            flowLayoutPanel1.Controls.Add(buttonMag4);
            flowLayoutPanel1.Controls.Add(buttonMag_2);
            flowLayoutPanel1.Controls.Add(buttonMag_4);
            flowLayoutPanel1.Controls.Add(buttonMag_8);
            flowLayoutPanel1.Controls.Add(buttonMag_16);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // buttonMag1
            // 
            resources.ApplyResources(buttonMag1, "buttonMag1");
            toolTip.SetToolTip(buttonMag1, resources.GetString("buttonMag1.ToolTip")); // 260531Cl
            buttonMag1.Name = "buttonMag1";
            buttonMag1.UseVisualStyleBackColor = true;
            buttonMag1.Click += buttonMag_Click;
            // 
            // buttonMag2
            // 
            resources.ApplyResources(buttonMag2, "buttonMag2");
            toolTip.SetToolTip(buttonMag2, resources.GetString("buttonMag2.ToolTip")); // 260531Cl
            buttonMag2.Name = "buttonMag2";
            buttonMag2.UseVisualStyleBackColor = true;
            buttonMag2.Click += buttonMag_Click;
            // 
            // buttonMag4
            // 
            resources.ApplyResources(buttonMag4, "buttonMag4");
            toolTip.SetToolTip(buttonMag4, resources.GetString("buttonMag4.ToolTip")); // 260531Cl
            buttonMag4.Name = "buttonMag4";
            buttonMag4.UseVisualStyleBackColor = true;
            buttonMag4.Click += buttonMag_Click;
            // 
            // buttonMag_2
            // 
            resources.ApplyResources(buttonMag_2, "buttonMag_2");
            toolTip.SetToolTip(buttonMag_2, resources.GetString("buttonMag_2.ToolTip")); // 260531Cl
            buttonMag_2.Name = "buttonMag_2";
            buttonMag_2.UseVisualStyleBackColor = true;
            buttonMag_2.Click += buttonMag_Click;
            // 
            // buttonMag_4
            // 
            resources.ApplyResources(buttonMag_4, "buttonMag_4");
            toolTip.SetToolTip(buttonMag_4, resources.GetString("buttonMag_4.ToolTip")); // 260531Cl
            buttonMag_4.Name = "buttonMag_4";
            buttonMag_4.UseVisualStyleBackColor = true;
            buttonMag_4.Click += buttonMag_Click;
            // 
            // buttonMag_8
            // 
            resources.ApplyResources(buttonMag_8, "buttonMag_8");
            toolTip.SetToolTip(buttonMag_8, resources.GetString("buttonMag_8.ToolTip")); // 260531Cl
            buttonMag_8.Name = "buttonMag_8";
            buttonMag_8.UseVisualStyleBackColor = true;
            buttonMag_8.Click += buttonMag_Click;
            // 
            // buttonMag_16
            // 
            resources.ApplyResources(buttonMag_16, "buttonMag_16");
            toolTip.SetToolTip(buttonMag_16, resources.GetString("buttonMag_16.ToolTip")); // 260531Cl
            buttonMag_16.Name = "buttonMag_16";
            buttonMag_16.UseVisualStyleBackColor = true;
            buttonMag_16.Click += buttonMag_Click;
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            toolTip.SetToolTip(label14, resources.GetString("label14.ToolTip")); // 260531Cl
            label14.Name = "label14";
            // 
            // labelResolution
            // 
            resources.ApplyResources(labelResolution, "labelResolution");
            toolTip.SetToolTip(labelResolution, resources.GetString("labelResolution.ToolTip")); // 260531Cl
            labelResolution.Name = "labelResolution";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip")); // 260531Cl
            label1.Name = "label1";
            // 
            // ScalablePictureBoxAdvanced
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(scalablePictureBox);
            Controls.Add(panelMagInfo);
            Controls.Add(panelTrackBar);
            Controls.Add(graphControl);
            Controls.Add(flowLayoutPanelGradient);
            Controls.Add(flowLayoutPanelImageFilter);
            Controls.Add(panelUpper);
            Controls.Add(statusStrip1);
            Name = "ScalablePictureBoxAdvanced";
            DragDrop += ScalablePictureBoxAdvanced_DragDrop;
            DragEnter += ScalablePictureBoxAdvanced_DragEnter;
            panelTrackBar.ResumeLayout(false);
            panelTrackBar.PerformLayout();
            flowLayoutPanelGradient.ResumeLayout(false);
            flowLayoutPanelGradient.PerformLayout();
            flowLayoutPanelPolarity.ResumeLayout(false);
            flowLayoutPanelPolarity.PerformLayout();
            flowLayoutPanelScale.ResumeLayout(false);
            flowLayoutPanelScale.PerformLayout();
            flowLayoutPanelColor.ResumeLayout(false);
            flowLayoutPanelColor.PerformLayout();
            flowLayoutPanelImageFilter.ResumeLayout(false);
            flowLayoutPanelImageFilter.PerformLayout();
            flowLayoutPanelDustScratches.ResumeLayout(false);
            flowLayoutPanelDustScratches.PerformLayout();
            flowLayoutPanelGaussianBlur2.ResumeLayout(false);
            flowLayoutPanelGaussianBlur2.PerformLayout();
            panelUpper.ResumeLayout(false);
            panelUpper.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panelMagInfo.ResumeLayout(false);
            panelMagInfo.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)

        private ScalablePictureBox scalablePictureBox;
        private GraphControl graphControl;
        private TrackBarAdvanced trackBarAdvancedMaximum;
        private System.Windows.Forms.Panel panelTrackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private TrackBarAdvanced trackBarAdvancedMinimum;
        private System.Windows.Forms.Label label;
        public System.Windows.Forms.ComboBox comboBoxScale2;
        public System.Windows.Forms.ComboBox comboBoxScale1;
        public System.Windows.Forms.ComboBox comboBoxGradient;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGradient;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelImageFilter;
        private System.Windows.Forms.CheckBox checkBoxGaussianBlur;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGaussianBlur2;
        private System.Windows.Forms.Panel panelUpper;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDustScratches;
        private System.Windows.Forms.CheckBox checkBoxDustScratches;
        private NumericBox numericBoxDustScratchesThreshold;
        private NumericBox numericBoxDustScratchesRadius;
        private NumericBox numericBoxGaussianFWHM;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panelMagInfo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label labelResolution;
        private System.Windows.Forms.Button buttonMag1;
        private System.Windows.Forms.Button buttonMag2;
        private System.Windows.Forms.Button buttonMag4;
        private System.Windows.Forms.Button buttonMag_2;
        private System.Windows.Forms.Button buttonMag_4;
        private System.Windows.Forms.Button buttonMag_8;
        private System.Windows.Forms.Button buttonMag_16;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPolarity;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelScale;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelColor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
