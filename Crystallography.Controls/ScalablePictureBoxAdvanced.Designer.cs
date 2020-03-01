namespace Crystallography.Controls
{
    partial class ScalablePictureBoxAdvanced
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
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
            this.panelTrackBar = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarAdvancedMinimum = new Crystallography.Controls.TrackBarAdvanced();
            this.trackBarAdvancedMaximum = new Crystallography.Controls.TrackBarAdvanced();
            this.label = new System.Windows.Forms.Label();
            this.comboBoxScale2 = new System.Windows.Forms.ComboBox();
            this.comboBoxScale1 = new System.Windows.Forms.ComboBox();
            this.comboBoxGradient = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanelGradient = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelImageFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelDustScratches = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxDustScratches = new System.Windows.Forms.CheckBox();
            this.numericBoxDustScratchesRadius = new Crystallography.Controls.NumericBox();
            this.numericBoxDustScratchesThreshold = new Crystallography.Controls.NumericBox();
            this.flowLayoutPanelGaussianBlur2 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxGaussianBlur = new System.Windows.Forms.CheckBox();
            this.numericBoxGaussianRadius = new Crystallography.Controls.NumericBox();
            this.panelUpper = new System.Windows.Forms.Panel();
            this.buttonCopyToClipBoard = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.scalablePictureBox = new Crystallography.Controls.ScalablePictureBox();
            this.graphControl = new Crystallography.Controls.GraphControl();
            this.panelTrackBar.SuspendLayout();
            this.flowLayoutPanelGradient.SuspendLayout();
            this.flowLayoutPanelImageFilter.SuspendLayout();
            this.flowLayoutPanelDustScratches.SuspendLayout();
            this.flowLayoutPanelGaussianBlur2.SuspendLayout();
            this.panelUpper.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTrackBar
            // 
            this.panelTrackBar.AutoSize = true;
            this.panelTrackBar.Controls.Add(this.label2);
            this.panelTrackBar.Controls.Add(this.label3);
            this.panelTrackBar.Controls.Add(this.trackBarAdvancedMinimum);
            this.panelTrackBar.Controls.Add(this.trackBarAdvancedMaximum);
            this.panelTrackBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTrackBar.Location = new System.Drawing.Point(0, 354);
            this.panelTrackBar.Margin = new System.Windows.Forms.Padding(2);
            this.panelTrackBar.Name = "panelTrackBar";
            this.panelTrackBar.Size = new System.Drawing.Size(397, 54);
            this.panelTrackBar.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Min.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Max.";
            // 
            // trackBarAdvancedMinimum
            // 
            this.trackBarAdvancedMinimum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarAdvancedMinimum.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.trackBarAdvancedMinimum.ControlHeight = 22;
            this.trackBarAdvancedMinimum.DecimalPlaces = 0;
            this.trackBarAdvancedMinimum.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.trackBarAdvancedMinimum.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.trackBarAdvancedMinimum.FooterText = "";
            this.trackBarAdvancedMinimum.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.trackBarAdvancedMinimum.HeaderText = "";
            this.trackBarAdvancedMinimum.Location = new System.Drawing.Point(42, 3);
            this.trackBarAdvancedMinimum.LogScrollBar = false;
            this.trackBarAdvancedMinimum.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.trackBarAdvancedMinimum.Maximum = 65D;
            this.trackBarAdvancedMinimum.MaximumSize = new System.Drawing.Size(1000, 25);
            this.trackBarAdvancedMinimum.Minimum = 0D;
            this.trackBarAdvancedMinimum.MinimumSize = new System.Drawing.Size(1, 22);
            this.trackBarAdvancedMinimum.Name = "trackBarAdvancedMinimum";
            this.trackBarAdvancedMinimum.NumericBoxSize = 84;
            this.trackBarAdvancedMinimum.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAdvancedMinimum.Size = new System.Drawing.Size(352, 22);
            this.trackBarAdvancedMinimum.Smart_Increment = true;
            this.trackBarAdvancedMinimum.TabIndex = 4;
            this.trackBarAdvancedMinimum.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.trackBarAdvancedMinimum.UpDown_Increment = 1D;
            this.trackBarAdvancedMinimum.Value = 0D;
            this.trackBarAdvancedMinimum.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(this.trackBarAdvancedMinimum_ValueChanged);
            // 
            // trackBarAdvancedMaximum
            // 
            this.trackBarAdvancedMaximum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarAdvancedMaximum.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.trackBarAdvancedMaximum.ControlHeight = 22;
            this.trackBarAdvancedMaximum.DecimalPlaces = 0;
            this.trackBarAdvancedMaximum.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.trackBarAdvancedMaximum.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.trackBarAdvancedMaximum.FooterText = "";
            this.trackBarAdvancedMaximum.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.trackBarAdvancedMaximum.HeaderText = "";
            this.trackBarAdvancedMaximum.Location = new System.Drawing.Point(42, 29);
            this.trackBarAdvancedMaximum.LogScrollBar = false;
            this.trackBarAdvancedMaximum.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.trackBarAdvancedMaximum.Maximum = 65535D;
            this.trackBarAdvancedMaximum.MaximumSize = new System.Drawing.Size(1000, 25);
            this.trackBarAdvancedMaximum.Minimum = 0D;
            this.trackBarAdvancedMaximum.MinimumSize = new System.Drawing.Size(1, 22);
            this.trackBarAdvancedMaximum.Name = "trackBarAdvancedMaximum";
            this.trackBarAdvancedMaximum.NumericBoxSize = 84;
            this.trackBarAdvancedMaximum.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAdvancedMaximum.Size = new System.Drawing.Size(352, 22);
            this.trackBarAdvancedMaximum.Smart_Increment = true;
            this.trackBarAdvancedMaximum.TabIndex = 4;
            this.trackBarAdvancedMaximum.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.trackBarAdvancedMaximum.UpDown_Increment = 1D;
            this.trackBarAdvancedMaximum.Value = 0D;
            this.trackBarAdvancedMaximum.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(this.trackBarAdvancedMaximum_ValueChanged);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Dock = System.Windows.Forms.DockStyle.Left;
            this.label.Location = new System.Drawing.Point(0, 2);
            this.label.Margin = new System.Windows.Forms.Padding(2, 2, 2, 0);
            this.label.Name = "label";
            this.label.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.label.Size = new System.Drawing.Size(0, 17);
            this.label.TabIndex = 9;
            // 
            // comboBoxScale2
            // 
            this.comboBoxScale2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScale2.FormattingEnabled = true;
            this.comboBoxScale2.Items.AddRange(new object[] {
            "Gray scale",
            "Cold-Warm scale"});
            this.comboBoxScale2.Location = new System.Drawing.Point(282, 2);
            this.comboBoxScale2.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxScale2.Name = "comboBoxScale2";
            this.comboBoxScale2.Size = new System.Drawing.Size(97, 23);
            this.comboBoxScale2.TabIndex = 36;
            this.comboBoxScale2.SelectedIndexChanged += new System.EventHandler(this.comboBoxScale_SelectedIndexChanged);
            // 
            // comboBoxScale1
            // 
            this.comboBoxScale1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScale1.FormattingEnabled = true;
            this.comboBoxScale1.Items.AddRange(new object[] {
            "Log Scale",
            "Liner Scale"});
            this.comboBoxScale1.Location = new System.Drawing.Point(164, 2);
            this.comboBoxScale1.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxScale1.Name = "comboBoxScale1";
            this.comboBoxScale1.Size = new System.Drawing.Size(80, 23);
            this.comboBoxScale1.TabIndex = 35;
            this.comboBoxScale1.SelectedIndexChanged += new System.EventHandler(this.comboBoxScale_SelectedIndexChanged);
            // 
            // comboBoxGradient
            // 
            this.comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGradient.FormattingEnabled = true;
            this.comboBoxGradient.Items.AddRange(new object[] {
            "Positive ",
            "Negative"});
            this.comboBoxGradient.Location = new System.Drawing.Point(54, 2);
            this.comboBoxGradient.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxGradient.Name = "comboBoxGradient";
            this.comboBoxGradient.Size = new System.Drawing.Size(74, 23);
            this.comboBoxGradient.TabIndex = 34;
            this.comboBoxGradient.SelectedIndexChanged += new System.EventHandler(this.comboBoxScale_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(130, 4);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 15);
            this.label7.TabIndex = 37;
            this.label7.Text = "Scale";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(246, 4);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 15);
            this.label9.TabIndex = 38;
            this.label9.Text = "Color";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(2, 4);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 39;
            this.label5.Text = "Gradient";
            // 
            // flowLayoutPanelGradient
            // 
            this.flowLayoutPanelGradient.AutoSize = true;
            this.flowLayoutPanelGradient.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelGradient.Controls.Add(this.label5);
            this.flowLayoutPanelGradient.Controls.Add(this.comboBoxGradient);
            this.flowLayoutPanelGradient.Controls.Add(this.label7);
            this.flowLayoutPanelGradient.Controls.Add(this.comboBoxScale1);
            this.flowLayoutPanelGradient.Controls.Add(this.label9);
            this.flowLayoutPanelGradient.Controls.Add(this.comboBoxScale2);
            this.flowLayoutPanelGradient.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelGradient.Location = new System.Drawing.Point(0, 478);
            this.flowLayoutPanelGradient.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelGradient.Name = "flowLayoutPanelGradient";
            this.flowLayoutPanelGradient.Padding = new System.Windows.Forms.Padding(0, 2, 0, 4);
            this.flowLayoutPanelGradient.Size = new System.Drawing.Size(397, 29);
            this.flowLayoutPanelGradient.TabIndex = 11;
            // 
            // flowLayoutPanelImageFilter
            // 
            this.flowLayoutPanelImageFilter.AutoSize = true;
            this.flowLayoutPanelImageFilter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelImageFilter.Controls.Add(this.flowLayoutPanelDustScratches);
            this.flowLayoutPanelImageFilter.Controls.Add(this.flowLayoutPanelGaussianBlur2);
            this.flowLayoutPanelImageFilter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelImageFilter.Location = new System.Drawing.Point(0, 507);
            this.flowLayoutPanelImageFilter.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelImageFilter.Name = "flowLayoutPanelImageFilter";
            this.flowLayoutPanelImageFilter.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.flowLayoutPanelImageFilter.Size = new System.Drawing.Size(397, 54);
            this.flowLayoutPanelImageFilter.TabIndex = 12;
            // 
            // flowLayoutPanelDustScratches
            // 
            this.flowLayoutPanelDustScratches.AutoSize = true;
            this.flowLayoutPanelDustScratches.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelDustScratches.Controls.Add(this.checkBoxDustScratches);
            this.flowLayoutPanelDustScratches.Controls.Add(this.numericBoxDustScratchesRadius);
            this.flowLayoutPanelDustScratches.Controls.Add(this.numericBoxDustScratchesThreshold);
            this.flowLayoutPanelDustScratches.Location = new System.Drawing.Point(0, 2);
            this.flowLayoutPanelDustScratches.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelDustScratches.Name = "flowLayoutPanelDustScratches";
            this.flowLayoutPanelDustScratches.Size = new System.Drawing.Size(353, 25);
            this.flowLayoutPanelDustScratches.TabIndex = 42;
            // 
            // checkBoxDustScratches
            // 
            this.checkBoxDustScratches.AutoSize = true;
            this.checkBoxDustScratches.Location = new System.Drawing.Point(2, 2);
            this.checkBoxDustScratches.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxDustScratches.Name = "checkBoxDustScratches";
            this.checkBoxDustScratches.Size = new System.Drawing.Size(110, 19);
            this.checkBoxDustScratches.TabIndex = 40;
            this.checkBoxDustScratches.Text = "Dust&&Scratches";
            this.checkBoxDustScratches.UseVisualStyleBackColor = true;
            this.checkBoxDustScratches.CheckedChanged += new System.EventHandler(this.imageFilterProperty_Changed);
            // 
            // numericBoxDustScratchesRadius
            // 
            this.numericBoxDustScratchesRadius.AllowMouseControl = false;
            this.numericBoxDustScratchesRadius.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDustScratchesRadius.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDustScratchesRadius.DecimalPlaces = 1;
            this.numericBoxDustScratchesRadius.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxDustScratchesRadius.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxDustScratchesRadius.FooterText = "pix.";
            this.numericBoxDustScratchesRadius.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxDustScratchesRadius.HeaderText = "Radius";
            this.numericBoxDustScratchesRadius.Location = new System.Drawing.Point(114, 0);
            this.numericBoxDustScratchesRadius.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDustScratchesRadius.Maximum = 5D;
            this.numericBoxDustScratchesRadius.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDustScratchesRadius.Minimum = 0D;
            this.numericBoxDustScratchesRadius.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDustScratchesRadius.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxDustScratchesRadius.MouseSpeed = 1D;
            this.numericBoxDustScratchesRadius.Multiline = false;
            this.numericBoxDustScratchesRadius.Name = "numericBoxDustScratchesRadius";
            this.numericBoxDustScratchesRadius.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDustScratchesRadius.RadianValue = 0.017453292519943295D;
            this.numericBoxDustScratchesRadius.ReadOnly = false;
            this.numericBoxDustScratchesRadius.RestrictLimitValue = true;
            this.numericBoxDustScratchesRadius.ShowFraction = false;
            this.numericBoxDustScratchesRadius.ShowPositiveSign = false;
            this.numericBoxDustScratchesRadius.ShowUpDown = true;
            this.numericBoxDustScratchesRadius.Size = new System.Drawing.Size(123, 25);
            this.numericBoxDustScratchesRadius.SkipEventDuringInput = false;
            this.numericBoxDustScratchesRadius.SmartIncrement = false;
            this.numericBoxDustScratchesRadius.TabIndex = 43;
            this.numericBoxDustScratchesRadius.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDustScratchesRadius.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDustScratchesRadius.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDustScratchesRadius.ThonsandsSeparator = true;
            this.numericBoxDustScratchesRadius.ToolTip = "";
            this.numericBoxDustScratchesRadius.UpDown_Increment = 0.5D;
            this.numericBoxDustScratchesRadius.Value = 1D;
            this.numericBoxDustScratchesRadius.Visible = false;
            this.numericBoxDustScratchesRadius.WordWrap = true;
            this.numericBoxDustScratchesRadius.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.imageFilterProperty_Changed);
            // 
            // numericBoxDustScratchesThreshold
            // 
            this.numericBoxDustScratchesThreshold.AllowMouseControl = false;
            this.numericBoxDustScratchesThreshold.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDustScratchesThreshold.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDustScratchesThreshold.DecimalPlaces = 0;
            this.numericBoxDustScratchesThreshold.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxDustScratchesThreshold.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxDustScratchesThreshold.FooterText = "";
            this.numericBoxDustScratchesThreshold.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxDustScratchesThreshold.HeaderText = "Threshold";
            this.numericBoxDustScratchesThreshold.Location = new System.Drawing.Point(237, 0);
            this.numericBoxDustScratchesThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDustScratchesThreshold.Maximum = 10D;
            this.numericBoxDustScratchesThreshold.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDustScratchesThreshold.Minimum = 0D;
            this.numericBoxDustScratchesThreshold.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDustScratchesThreshold.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxDustScratchesThreshold.MouseSpeed = 1D;
            this.numericBoxDustScratchesThreshold.Multiline = false;
            this.numericBoxDustScratchesThreshold.Name = "numericBoxDustScratchesThreshold";
            this.numericBoxDustScratchesThreshold.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.numericBoxDustScratchesThreshold.RadianValue = 0.052359877559829883D;
            this.numericBoxDustScratchesThreshold.ReadOnly = false;
            this.numericBoxDustScratchesThreshold.RestrictLimitValue = true;
            this.numericBoxDustScratchesThreshold.ShowFraction = false;
            this.numericBoxDustScratchesThreshold.ShowPositiveSign = false;
            this.numericBoxDustScratchesThreshold.ShowUpDown = true;
            this.numericBoxDustScratchesThreshold.Size = new System.Drawing.Size(116, 25);
            this.numericBoxDustScratchesThreshold.SkipEventDuringInput = false;
            this.numericBoxDustScratchesThreshold.SmartIncrement = true;
            this.numericBoxDustScratchesThreshold.TabIndex = 43;
            this.numericBoxDustScratchesThreshold.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDustScratchesThreshold.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDustScratchesThreshold.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDustScratchesThreshold.ThonsandsSeparator = true;
            this.numericBoxDustScratchesThreshold.ToolTip = "";
            this.numericBoxDustScratchesThreshold.UpDown_Increment = 1D;
            this.numericBoxDustScratchesThreshold.Value = 3D;
            this.numericBoxDustScratchesThreshold.Visible = false;
            this.numericBoxDustScratchesThreshold.WordWrap = true;
            this.numericBoxDustScratchesThreshold.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.imageFilterProperty_Changed);
            // 
            // flowLayoutPanelGaussianBlur2
            // 
            this.flowLayoutPanelGaussianBlur2.AutoSize = true;
            this.flowLayoutPanelGaussianBlur2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelGaussianBlur2.Controls.Add(this.checkBoxGaussianBlur);
            this.flowLayoutPanelGaussianBlur2.Controls.Add(this.numericBoxGaussianRadius);
            this.flowLayoutPanelGaussianBlur2.Location = new System.Drawing.Point(0, 27);
            this.flowLayoutPanelGaussianBlur2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelGaussianBlur2.Name = "flowLayoutPanelGaussianBlur2";
            this.flowLayoutPanelGaussianBlur2.Size = new System.Drawing.Size(264, 25);
            this.flowLayoutPanelGaussianBlur2.TabIndex = 42;
            // 
            // checkBoxGaussianBlur
            // 
            this.checkBoxGaussianBlur.AutoSize = true;
            this.checkBoxGaussianBlur.Location = new System.Drawing.Point(2, 2);
            this.checkBoxGaussianBlur.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxGaussianBlur.Name = "checkBoxGaussianBlur";
            this.checkBoxGaussianBlur.Size = new System.Drawing.Size(97, 19);
            this.checkBoxGaussianBlur.TabIndex = 40;
            this.checkBoxGaussianBlur.Text = "Gaussian Blur";
            this.checkBoxGaussianBlur.UseVisualStyleBackColor = true;
            this.checkBoxGaussianBlur.CheckedChanged += new System.EventHandler(this.imageFilterProperty_Changed);
            // 
            // numericBoxGaussianRadius
            // 
            this.numericBoxGaussianRadius.AllowMouseControl = false;
            this.numericBoxGaussianRadius.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxGaussianRadius.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGaussianRadius.DecimalPlaces = 1;
            this.numericBoxGaussianRadius.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxGaussianRadius.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxGaussianRadius.FooterText = "pix.";
            this.numericBoxGaussianRadius.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxGaussianRadius.HeaderText = "Radius (HWHM)";
            this.numericBoxGaussianRadius.Location = new System.Drawing.Point(101, 0);
            this.numericBoxGaussianRadius.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxGaussianRadius.Maximum = 100D;
            this.numericBoxGaussianRadius.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxGaussianRadius.Minimum = 0D;
            this.numericBoxGaussianRadius.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxGaussianRadius.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxGaussianRadius.MouseSpeed = 1D;
            this.numericBoxGaussianRadius.Multiline = false;
            this.numericBoxGaussianRadius.Name = "numericBoxGaussianRadius";
            this.numericBoxGaussianRadius.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxGaussianRadius.RadianValue = 0.017453292519943295D;
            this.numericBoxGaussianRadius.ReadOnly = false;
            this.numericBoxGaussianRadius.RestrictLimitValue = true;
            this.numericBoxGaussianRadius.ShowFraction = false;
            this.numericBoxGaussianRadius.ShowPositiveSign = false;
            this.numericBoxGaussianRadius.ShowUpDown = true;
            this.numericBoxGaussianRadius.Size = new System.Drawing.Size(163, 25);
            this.numericBoxGaussianRadius.SkipEventDuringInput = false;
            this.numericBoxGaussianRadius.SmartIncrement = true;
            this.numericBoxGaussianRadius.TabIndex = 43;
            this.numericBoxGaussianRadius.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxGaussianRadius.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxGaussianRadius.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxGaussianRadius.ThonsandsSeparator = true;
            this.numericBoxGaussianRadius.ToolTip = "";
            this.numericBoxGaussianRadius.UpDown_Increment = 1D;
            this.numericBoxGaussianRadius.Value = 1D;
            this.numericBoxGaussianRadius.Visible = false;
            this.numericBoxGaussianRadius.WordWrap = true;
            this.numericBoxGaussianRadius.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.imageFilterProperty_Changed);
            // 
            // panelUpper
            // 
            this.panelUpper.Controls.Add(this.buttonCopyToClipBoard);
            this.panelUpper.Controls.Add(this.label);
            this.panelUpper.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUpper.Location = new System.Drawing.Point(0, 0);
            this.panelUpper.Margin = new System.Windows.Forms.Padding(2);
            this.panelUpper.Name = "panelUpper";
            this.panelUpper.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.panelUpper.Size = new System.Drawing.Size(397, 26);
            this.panelUpper.TabIndex = 13;
            // 
            // buttonCopyToClipBoard
            // 
            this.buttonCopyToClipBoard.AutoSize = true;
            this.buttonCopyToClipBoard.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCopyToClipBoard.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonCopyToClipBoard.Location = new System.Drawing.Point(282, 2);
            this.buttonCopyToClipBoard.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCopyToClipBoard.Name = "buttonCopyToClipBoard";
            this.buttonCopyToClipBoard.Size = new System.Drawing.Size(115, 22);
            this.buttonCopyToClipBoard.TabIndex = 10;
            this.buttonCopyToClipBoard.Text = "Copy To ClipBoard";
            this.buttonCopyToClipBoard.UseVisualStyleBackColor = true;
            this.buttonCopyToClipBoard.Click += new System.EventHandler(this.buttonCopyToClipBoard_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 561);
            this.statusStrip1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(397, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel1.Text = " ";
            // 
            // scalablePictureBox
            // 
            this.scalablePictureBox.AutoSize = true;
            this.scalablePictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.scalablePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scalablePictureBox.FixZoomAndCenter = false;
            this.scalablePictureBox.FocusEventEnabled = false;
            this.scalablePictureBox.HorizontalFlip = false;
            this.scalablePictureBox.Location = new System.Drawing.Point(0, 26);
            this.scalablePictureBox.ManualSpotMode = false;
            this.scalablePictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.scalablePictureBox.MouseScaling = true;
            this.scalablePictureBox.MouseTranslation = true;
            this.scalablePictureBox.Name = "scalablePictureBox";
            this.scalablePictureBox.ShowAreaRectangle = false;
            this.scalablePictureBox.ShowRimRentangle = false;
            this.scalablePictureBox.Size = new System.Drawing.Size(397, 328);
            this.scalablePictureBox.TabIndex = 0;
            this.scalablePictureBox.VerticalFlip = false;
            this.scalablePictureBox.Zoom = 128D;
            this.scalablePictureBox.Paint2 += new System.Windows.Forms.PaintEventHandler(this.scalablePictureBox_Paint2);
            this.scalablePictureBox.MouseMove2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox1_MouseMove2);
            this.scalablePictureBox.MouseUp2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox_MouseUp2);
            this.scalablePictureBox.MouseDown2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox_MouseDown2);
            this.scalablePictureBox.MouseWheel2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox_MouseWheel2);
            this.scalablePictureBox.DrawingAreaChanged += new Crystallography.Controls.ScalablePictureBox.DrawingAreaChangedEvent(this.scalablePictureBox_DrawingAreaChanged);
            // 
            // graphControl
            // 
            this.graphControl.AllowMouseOperation = true;
            this.graphControl.BackgroundColor = System.Drawing.Color.White;
            this.graphControl.BottomMargin = 0D;
            this.graphControl.DivisionLineColor = System.Drawing.Color.Gray;
            this.graphControl.DivisionSubLineColor = System.Drawing.Color.LightGray;
            this.graphControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.graphControl.FixRangeHorizontal = false;
            this.graphControl.FixRangeVertical = false;
            this.graphControl.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphControl.GraphName = "";
            this.graphControl.HorizontalGradiationTextVisivle = true;
            this.graphControl.Interpolation = false;
            this.graphControl.IsIntegerX = false;
            this.graphControl.IsIntegerY = false;
            this.graphControl.LabelX = "X:";
            this.graphControl.LabelY = "Y:";
            this.graphControl.LeftMargin = 0F;
            this.graphControl.LineColor = System.Drawing.Color.Red;
            this.graphControl.LineWidth = 1F;
            this.graphControl.Location = new System.Drawing.Point(0, 408);
            this.graphControl.LowerX = 0D;
            this.graphControl.LowerY = 0D;
            this.graphControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.graphControl.MaximalX = 1D;
            this.graphControl.MaximalY = 1D;
            this.graphControl.MinimalX = 0D;
            this.graphControl.MinimalY = 0D;
            this.graphControl.Mode = Crystallography.Controls.GraphControl.DrawingMode.Histogram;
            this.graphControl.MousePositionVisible = true;
            this.graphControl.Name = "graphControl";
            this.graphControl.OriginPosition = new System.Drawing.Point(20, 20);
            this.graphControl.Size = new System.Drawing.Size(397, 70);
            this.graphControl.Smoothing = false;
            this.graphControl.TabIndex = 1;
            this.graphControl.TextFont = new System.Drawing.Font("メイリオ", 7F);
            this.graphControl.UnitX = "";
            this.graphControl.UnitY = "";
            this.graphControl.UpperText = "";
            this.graphControl.UpperTextVisible = false;
            this.graphControl.UpperX = 1D;
            this.graphControl.UpperY = 1D;
            this.graphControl.UseLineWidth = true;
            this.graphControl.VerticalGradiationTextVisivle = true;
            this.graphControl.XLog = false;
            this.graphControl.XScaleLineVisible = true;
            this.graphControl.YLog = true;
            this.graphControl.YScaleLineVisible = true;
            this.graphControl.LinePositionChanged += new Crystallography.Controls.GraphControl.LinePositionChengedEventHandler(this.graphControl_LinePositionChanged);
            // 
            // ScalablePictureBoxAdvanced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.scalablePictureBox);
            this.Controls.Add(this.panelTrackBar);
            this.Controls.Add(this.graphControl);
            this.Controls.Add(this.flowLayoutPanelGradient);
            this.Controls.Add(this.flowLayoutPanelImageFilter);
            this.Controls.Add(this.panelUpper);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ScalablePictureBoxAdvanced";
            this.Size = new System.Drawing.Size(397, 583);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ScalablePictureBoxAdvanced_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ScalablePictureBoxAdvanced_DragEnter);
            this.panelTrackBar.ResumeLayout(false);
            this.panelTrackBar.PerformLayout();
            this.flowLayoutPanelGradient.ResumeLayout(false);
            this.flowLayoutPanelGradient.PerformLayout();
            this.flowLayoutPanelImageFilter.ResumeLayout(false);
            this.flowLayoutPanelImageFilter.PerformLayout();
            this.flowLayoutPanelDustScratches.ResumeLayout(false);
            this.flowLayoutPanelDustScratches.PerformLayout();
            this.flowLayoutPanelGaussianBlur2.ResumeLayout(false);
            this.flowLayoutPanelGaussianBlur2.PerformLayout();
            this.panelUpper.ResumeLayout(false);
            this.panelUpper.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

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
        private System.Windows.Forms.Button buttonCopyToClipBoard;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDustScratches;
        private System.Windows.Forms.CheckBox checkBoxDustScratches;
        private NumericBox numericBoxDustScratchesThreshold;
        private NumericBox numericBoxDustScratchesRadius;
        private NumericBox numericBoxGaussianRadius;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
