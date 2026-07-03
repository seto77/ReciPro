namespace ReciPro;

partial class FormMovie
{
    /// <summary>Required designer variable.</summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>Clean up any resources being used.</summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMovie)); // 260703Cl 追加: ツールチップの多言語 resx を復元 (260531Cl 方式)
        components = new System.ComponentModel.Container();
        toolTip = new System.Windows.Forms.ToolTip(components);
        buttonOK = new System.Windows.Forms.Button();
        buttonCancel = new System.Windows.Forms.Button();
        numericBoxSpeed = new NumericBox();
        buttonAntiClock = new System.Windows.Forms.Button();
        buttonClock = new System.Windows.Forms.Button();
        buttonTopLeft = new System.Windows.Forms.Button();
        buttonLeft = new System.Windows.Forms.Button();
        buttonBottomLeft = new System.Windows.Forms.Button();
        buttonBottom = new System.Windows.Forms.Button();
        buttonBottomRight = new System.Windows.Forms.Button();
        buttonTop = new System.Windows.Forms.Button();
        buttonTopRight = new System.Windows.Forms.Button();
        buttonRight = new System.Windows.Forms.Button();
        numericBoxDuration = new NumericBox();
        numericBoxFps = new NumericBox();
        checkBoxIncludeFinalFrame = new System.Windows.Forms.CheckBox();
        numericBoxQuality = new NumericBox();
        radioButtonAxis = new System.Windows.Forms.RadioButton();
        radioButtonPlane = new System.Windows.Forms.RadioButton();
        radioButtonCurrent = new System.Windows.Forms.RadioButton();
        radioButtonH264 = new System.Windows.Forms.RadioButton();
        radioButtonH265 = new System.Windows.Forms.RadioButton();
        comboBoxSpeed = new System.Windows.Forms.ComboBox();
        label1 = new System.Windows.Forms.Label();
        indexControl = new IndexControl();
        checkBoxRotation = new System.Windows.Forms.CheckBox();
        checkBoxTranslation = new System.Windows.Forms.CheckBox();
        tableLayoutPanelCurrent = new System.Windows.Forms.TableLayoutPanel();
        tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
        groupBoxDirection = new System.Windows.Forms.GroupBox();
        flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
        flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
        flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
        numericBoxTranslationSpeed = new NumericBox();
        flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
        flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
        tableLayoutPanelCurrent.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        groupBoxDirection.SuspendLayout();
        flowLayoutPanel1.SuspendLayout();
        flowLayoutPanel2.SuspendLayout();
        flowLayoutPanel3.SuspendLayout();
        flowLayoutPanel4.SuspendLayout();
        flowLayoutPanel5.SuspendLayout();
        SuspendLayout();
        // 
        // toolTip
        // 
        toolTip.AutoPopDelay = 10000;
        toolTip.InitialDelay = 500;
        toolTip.IsBalloon = true;
        toolTip.ReshowDelay = 100;
        // 
        // buttonOK
        // 
        buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        buttonOK.Location = new System.Drawing.Point(17, 3);
        buttonOK.Name = "buttonOK";
        buttonOK.Size = new System.Drawing.Size(75, 25);
        buttonOK.TabIndex = 0;
        buttonOK.Text = "OK";
        toolTip.SetToolTip(buttonOK, resources.GetString("buttonOK.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonOK.UseVisualStyleBackColor = true;
        buttonOK.Click += buttonOK_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        buttonCancel.Location = new System.Drawing.Point(98, 3);
        buttonCancel.Name = "buttonCancel";
        buttonCancel.Size = new System.Drawing.Size(75, 25);
        buttonCancel.TabIndex = 0;
        buttonCancel.Text = "Cancel";
        toolTip.SetToolTip(buttonCancel, resources.GetString("buttonCancel.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonCancel.UseVisualStyleBackColor = true;
        buttonCancel.Click += buttonCancel_Click;
        // 
        // numericBoxSpeed
        // 
        numericBoxSpeed.BackColor = System.Drawing.Color.Transparent;
        numericBoxSpeed.FooterPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxSpeed.FooterText = "°/s";
        numericBoxSpeed.HeaderPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxSpeed.HeaderText = "Speed";
        numericBoxSpeed.Location = new System.Drawing.Point(0, 31);
        numericBoxSpeed.Margin = new System.Windows.Forms.Padding(0);
        numericBoxSpeed.Maximum = 360D;
        numericBoxSpeed.MaximumSize = new System.Drawing.Size(1000, 30);
        numericBoxSpeed.Minimum = -360D;
        numericBoxSpeed.MinimumSize = new System.Drawing.Size(1, 20);
        numericBoxSpeed.Name = "numericBoxSpeed";
        numericBoxSpeed.RadianValue = 0.52359877559829882D;
        numericBoxSpeed.ShowUpDown = true;
        numericBoxSpeed.Size = new System.Drawing.Size(131, 27);
        numericBoxSpeed.SkipEventDuringInput = false;
        numericBoxSpeed.SmartIncrement = true;
        numericBoxSpeed.TabIndex = 1;
        toolTip.SetToolTip(numericBoxSpeed, resources.GetString("numericBoxSpeed.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        numericBoxSpeed.TrimEndZero = true;
        numericBoxSpeed.Value = 30D;
        numericBoxSpeed.ValueBoxWidth = 45;
        numericBoxSpeed.ValueFontSize = 9F;
        // 
        // buttonAntiClock
        // 
        buttonAntiClock.BackColor = System.Drawing.SystemColors.Control;
        buttonAntiClock.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonAntiClock.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
        buttonAntiClock.ForeColor = System.Drawing.Color.Gray;
        buttonAntiClock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        buttonAntiClock.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonAntiClock.Location = new System.Drawing.Point(39, 0);
        buttonAntiClock.Margin = new System.Windows.Forms.Padding(0);
        buttonAntiClock.Name = "buttonAntiClock";
        buttonAntiClock.Size = new System.Drawing.Size(39, 30);
        buttonAntiClock.TabIndex = 1;
        buttonAntiClock.Text = "↺";
        toolTip.SetToolTip(buttonAntiClock, resources.GetString("buttonAntiClock.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonAntiClock.UseVisualStyleBackColor = false;
        buttonAntiClock.Click += buttonDirection_Click;
        // 
        // buttonClock
        // 
        buttonClock.BackColor = System.Drawing.SystemColors.Control;
        buttonClock.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonClock.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
        buttonClock.ForeColor = System.Drawing.Color.Gray;
        buttonClock.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonClock.Location = new System.Drawing.Point(0, 0);
        buttonClock.Margin = new System.Windows.Forms.Padding(0);
        buttonClock.Name = "buttonClock";
        buttonClock.Size = new System.Drawing.Size(39, 30);
        buttonClock.TabIndex = 0;
        buttonClock.Text = "↻";
        toolTip.SetToolTip(buttonClock, resources.GetString("buttonClock.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonClock.UseVisualStyleBackColor = false;
        buttonClock.Click += buttonDirection_Click;
        // 
        // buttonTopLeft
        // 
        buttonTopLeft.BackColor = System.Drawing.SystemColors.Control;
        buttonTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonTopLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
        buttonTopLeft.ForeColor = System.Drawing.Color.Gray;
        buttonTopLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonTopLeft.Location = new System.Drawing.Point(0, 0);
        buttonTopLeft.Margin = new System.Windows.Forms.Padding(0);
        buttonTopLeft.Name = "buttonTopLeft";
        buttonTopLeft.Size = new System.Drawing.Size(39, 30);
        buttonTopLeft.TabIndex = 0;
        buttonTopLeft.Text = "↖";
        toolTip.SetToolTip(buttonTopLeft, resources.GetString("buttonTopLeft.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonTopLeft.UseVisualStyleBackColor = false;
        buttonTopLeft.Click += buttonDirection_Click;
        // 
        // buttonLeft
        // 
        buttonLeft.BackColor = System.Drawing.SystemColors.Control;
        buttonLeft.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
        buttonLeft.ForeColor = System.Drawing.Color.Gray;
        buttonLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonLeft.Location = new System.Drawing.Point(0, 30);
        buttonLeft.Margin = new System.Windows.Forms.Padding(0);
        buttonLeft.Name = "buttonLeft";
        buttonLeft.Size = new System.Drawing.Size(39, 30);
        buttonLeft.TabIndex = 1;
        buttonLeft.Text = "←";
        toolTip.SetToolTip(buttonLeft, resources.GetString("buttonLeft.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonLeft.UseVisualStyleBackColor = false;
        buttonLeft.Click += buttonDirection_Click;
        // 
        // buttonBottomLeft
        // 
        buttonBottomLeft.BackColor = System.Drawing.SystemColors.Control;
        buttonBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonBottomLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
        buttonBottomLeft.ForeColor = System.Drawing.Color.Gray;
        buttonBottomLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonBottomLeft.Location = new System.Drawing.Point(0, 60);
        buttonBottomLeft.Margin = new System.Windows.Forms.Padding(0);
        buttonBottomLeft.Name = "buttonBottomLeft";
        buttonBottomLeft.Size = new System.Drawing.Size(39, 31);
        buttonBottomLeft.TabIndex = 2;
        buttonBottomLeft.Text = "↙";
        toolTip.SetToolTip(buttonBottomLeft, resources.GetString("buttonBottomLeft.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonBottomLeft.UseVisualStyleBackColor = false;
        buttonBottomLeft.Click += buttonDirection_Click;
        // 
        // buttonBottom
        // 
        buttonBottom.BackColor = System.Drawing.SystemColors.Control;
        buttonBottom.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonBottom.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
        buttonBottom.ForeColor = System.Drawing.Color.Gray;
        buttonBottom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonBottom.Location = new System.Drawing.Point(39, 60);
        buttonBottom.Margin = new System.Windows.Forms.Padding(0);
        buttonBottom.Name = "buttonBottom";
        buttonBottom.Size = new System.Drawing.Size(78, 31);
        buttonBottom.TabIndex = 4;
        buttonBottom.Text = "↓";
        toolTip.SetToolTip(buttonBottom, resources.GetString("buttonBottom.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonBottom.UseVisualStyleBackColor = false;
        buttonBottom.Click += buttonDirection_Click;
        // 
        // buttonBottomRight
        // 
        buttonBottomRight.BackColor = System.Drawing.SystemColors.Control;
        buttonBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonBottomRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
        buttonBottomRight.ForeColor = System.Drawing.Color.Gray;
        buttonBottomRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonBottomRight.Location = new System.Drawing.Point(117, 60);
        buttonBottomRight.Margin = new System.Windows.Forms.Padding(0);
        buttonBottomRight.Name = "buttonBottomRight";
        buttonBottomRight.Size = new System.Drawing.Size(39, 31);
        buttonBottomRight.TabIndex = 7;
        buttonBottomRight.Text = "↘";
        toolTip.SetToolTip(buttonBottomRight, resources.GetString("buttonBottomRight.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonBottomRight.UseVisualStyleBackColor = false;
        buttonBottomRight.Click += buttonDirection_Click;
        // 
        // buttonTop
        // 
        buttonTop.BackColor = System.Drawing.SystemColors.Control;
        buttonTop.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonTop.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
        buttonTop.ForeColor = System.Drawing.Color.Gray;
        buttonTop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonTop.Location = new System.Drawing.Point(39, 0);
        buttonTop.Margin = new System.Windows.Forms.Padding(0);
        buttonTop.Name = "buttonTop";
        buttonTop.Size = new System.Drawing.Size(78, 30);
        buttonTop.TabIndex = 3;
        buttonTop.Text = "↑";
        toolTip.SetToolTip(buttonTop, resources.GetString("buttonTop.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonTop.UseVisualStyleBackColor = false;
        buttonTop.Click += buttonDirection_Click;
        // 
        // buttonTopRight
        // 
        buttonTopRight.BackColor = System.Drawing.SystemColors.Control;
        buttonTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonTopRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
        buttonTopRight.ForeColor = System.Drawing.Color.Gray;
        buttonTopRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonTopRight.Location = new System.Drawing.Point(117, 0);
        buttonTopRight.Margin = new System.Windows.Forms.Padding(0);
        buttonTopRight.Name = "buttonTopRight";
        buttonTopRight.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
        buttonTopRight.Size = new System.Drawing.Size(39, 30);
        buttonTopRight.TabIndex = 5;
        buttonTopRight.Text = "↗";
        toolTip.SetToolTip(buttonTopRight, resources.GetString("buttonTopRight.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonTopRight.UseVisualStyleBackColor = false;
        buttonTopRight.Click += buttonDirection_Click;
        // 
        // buttonRight
        // 
        buttonRight.BackColor = System.Drawing.SystemColors.Control;
        buttonRight.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
        buttonRight.ForeColor = System.Drawing.Color.Blue;
        buttonRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonRight.Location = new System.Drawing.Point(117, 30);
        buttonRight.Margin = new System.Windows.Forms.Padding(0);
        buttonRight.Name = "buttonRight";
        buttonRight.Size = new System.Drawing.Size(39, 30);
        buttonRight.TabIndex = 6;
        buttonRight.Text = "→";
        toolTip.SetToolTip(buttonRight, resources.GetString("buttonRight.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        buttonRight.UseVisualStyleBackColor = false;
        buttonRight.Click += buttonDirection_Click;
        // 
        // numericBoxDuration
        // 
        numericBoxDuration.BackColor = System.Drawing.Color.Transparent;
        numericBoxDuration.FooterPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxDuration.FooterText = "s";
        numericBoxDuration.HeaderPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxDuration.HeaderText = "Duration";
        numericBoxDuration.Location = new System.Drawing.Point(0, 85);
        numericBoxDuration.Margin = new System.Windows.Forms.Padding(0);
        numericBoxDuration.Maximum = 360D;
        numericBoxDuration.MaximumSize = new System.Drawing.Size(1000, 30);
        numericBoxDuration.Minimum = 0.01D;
        numericBoxDuration.MinimumSize = new System.Drawing.Size(1, 20);
        numericBoxDuration.Name = "numericBoxDuration";
        numericBoxDuration.RadianValue = 0.20943951023931953D;
        numericBoxDuration.ShowUpDown = true;
        numericBoxDuration.Size = new System.Drawing.Size(149, 27);
        numericBoxDuration.SkipEventDuringInput = false;
        numericBoxDuration.SmartIncrement = true;
        numericBoxDuration.TabIndex = 1;
        toolTip.SetToolTip(numericBoxDuration, resources.GetString("numericBoxDuration.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        numericBoxDuration.TrimEndZero = true;
        numericBoxDuration.Value = 12D;
        numericBoxDuration.ValueBoxWidth = 60;
        numericBoxDuration.ValueFontSize = 9F;
        // 
        // numericBoxFps
        // 
        numericBoxFps.BackColor = System.Drawing.Color.Transparent;
        numericBoxFps.DecimalPlaces = 0;
        numericBoxFps.FooterPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxFps.FooterText = "fps";
        numericBoxFps.HeaderPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxFps.HeaderText = "FPS";
        numericBoxFps.Location = new System.Drawing.Point(0, 374);
        numericBoxFps.Margin = new System.Windows.Forms.Padding(0);
        numericBoxFps.Maximum = 120D;
        numericBoxFps.MaximumSize = new System.Drawing.Size(1000, 30);
        numericBoxFps.Minimum = 1D;
        numericBoxFps.MinimumSize = new System.Drawing.Size(1, 20);
        numericBoxFps.Name = "numericBoxFps";
        numericBoxFps.RadianValue = 0.43633231299858238D;
        numericBoxFps.ShowUpDown = true;
        numericBoxFps.Size = new System.Drawing.Size(116, 27);
        numericBoxFps.SkipEventDuringInput = false;
        numericBoxFps.SmartIncrement = true;
        numericBoxFps.TabIndex = 1;
        toolTip.SetToolTip(numericBoxFps, resources.GetString("numericBoxFps.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        numericBoxFps.TrimEndZero = true;
        numericBoxFps.Value = 25D;
        numericBoxFps.ValueBoxWidth = 45;
        numericBoxFps.ValueFontSize = 9F;
        // 
        // checkBoxIncludeFinalFrame
        // 
        checkBoxIncludeFinalFrame.AutoSize = true;
        checkBoxIncludeFinalFrame.Location = new System.Drawing.Point(3, 115);
        checkBoxIncludeFinalFrame.Name = "checkBoxIncludeFinalFrame";
        checkBoxIncludeFinalFrame.Size = new System.Drawing.Size(123, 19);
        checkBoxIncludeFinalFrame.TabIndex = 98;
        checkBoxIncludeFinalFrame.Text = "Include final frame";
        toolTip.SetToolTip(checkBoxIncludeFinalFrame, resources.GetString("checkBoxIncludeFinalFrame.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        checkBoxIncludeFinalFrame.UseVisualStyleBackColor = true;
        // 
        // numericBoxQuality
        // 
        numericBoxQuality.BackColor = System.Drawing.Color.Transparent;
        numericBoxQuality.DecimalPlaces = 0;
        numericBoxQuality.FooterPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
        numericBoxQuality.HeaderPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
        numericBoxQuality.HeaderText = "Quality";
        numericBoxQuality.Location = new System.Drawing.Point(0, 432);
        numericBoxQuality.Margin = new System.Windows.Forms.Padding(0);
        numericBoxQuality.Maximum = 100D;
        numericBoxQuality.MaximumSize = new System.Drawing.Size(1000, 30);
        numericBoxQuality.Minimum = 1D;
        numericBoxQuality.MinimumSize = new System.Drawing.Size(1, 20);
        numericBoxQuality.Name = "numericBoxQuality";
        numericBoxQuality.RadianValue = 1.5707963267948966D;
        numericBoxQuality.ShowUpDown = true;
        numericBoxQuality.Size = new System.Drawing.Size(100, 27);
        numericBoxQuality.SkipEventDuringInput = false;
        numericBoxQuality.SmartIncrement = true;
        numericBoxQuality.TabIndex = 1;
        toolTip.SetToolTip(numericBoxQuality, resources.GetString("numericBoxQuality.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        numericBoxQuality.TrimEndZero = true;
        numericBoxQuality.UpDown_Increment = 5D;
        numericBoxQuality.Value = 90D;
        numericBoxQuality.ValueBoxWidth = 35;
        numericBoxQuality.ValueFontSize = 9F;
        // 
        // radioButtonAxis
        // 
        radioButtonAxis.AutoSize = true;
        radioButtonAxis.Location = new System.Drawing.Point(6, 136);
        radioButtonAxis.Name = "radioButtonAxis";
        radioButtonAxis.Size = new System.Drawing.Size(105, 19);
        radioButtonAxis.TabIndex = 86;
        radioButtonAxis.Text = "Direction index";
        toolTip.SetToolTip(radioButtonAxis, resources.GetString("radioButtonAxis.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        radioButtonAxis.UseVisualStyleBackColor = true;
        radioButtonAxis.CheckedChanged += radioButtonCurrent_CheckedChanged;
        // 
        // radioButtonPlane
        // 
        radioButtonPlane.AutoSize = true;
        radioButtonPlane.Location = new System.Drawing.Point(6, 161);
        radioButtonPlane.Name = "radioButtonPlane";
        radioButtonPlane.Size = new System.Drawing.Size(92, 19);
        radioButtonPlane.TabIndex = 86;
        radioButtonPlane.Text = "Lattice plane";
        toolTip.SetToolTip(radioButtonPlane, resources.GetString("radioButtonPlane.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        radioButtonPlane.UseVisualStyleBackColor = true;
        radioButtonPlane.CheckedChanged += radioButtonCurrent_CheckedChanged;
        // 
        // radioButtonCurrent
        // 
        radioButtonCurrent.AutoSize = true;
        radioButtonCurrent.Checked = true;
        radioButtonCurrent.Location = new System.Drawing.Point(6, 18);
        radioButtonCurrent.Name = "radioButtonCurrent";
        radioButtonCurrent.Size = new System.Drawing.Size(121, 19);
        radioButtonCurrent.TabIndex = 86;
        radioButtonCurrent.TabStop = true;
        radioButtonCurrent.Text = "Current projection";
        toolTip.SetToolTip(radioButtonCurrent, resources.GetString("radioButtonCurrent.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        radioButtonCurrent.UseVisualStyleBackColor = true;
        radioButtonCurrent.CheckedChanged += radioButtonCurrent_CheckedChanged;
        // 
        // radioButtonH264
        // 
        radioButtonH264.AutoSize = true;
        radioButtonH264.Location = new System.Drawing.Point(3, 3);
        radioButtonH264.Name = "radioButtonH264";
        radioButtonH264.Size = new System.Drawing.Size(52, 19);
        radioButtonH264.TabIndex = 90;
        radioButtonH264.Text = "H264";
        toolTip.SetToolTip(radioButtonH264, resources.GetString("radioButtonH264.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        radioButtonH264.UseVisualStyleBackColor = true;
        // 
        // radioButtonH265
        // 
        radioButtonH265.AutoSize = true;
        radioButtonH265.Checked = true;
        radioButtonH265.Location = new System.Drawing.Point(61, 3);
        radioButtonH265.Name = "radioButtonH265";
        radioButtonH265.Size = new System.Drawing.Size(52, 19);
        radioButtonH265.TabIndex = 90;
        radioButtonH265.TabStop = true;
        radioButtonH265.Text = "H265";
        toolTip.SetToolTip(radioButtonH265, resources.GetString("radioButtonH265.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        radioButtonH265.UseVisualStyleBackColor = true;
        // 
        // comboBoxSpeed
        // 
        comboBoxSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBoxSpeed.FormattingEnabled = true;
        comboBoxSpeed.Items.AddRange(new object[] { "ultrafast", "superfast", "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow" });
        comboBoxSpeed.Location = new System.Drawing.Point(86, 0);
        comboBoxSpeed.Margin = new System.Windows.Forms.Padding(0);
        comboBoxSpeed.Name = "comboBoxSpeed";
        comboBoxSpeed.Size = new System.Drawing.Size(75, 23);
        comboBoxSpeed.TabIndex = 91;
        toolTip.SetToolTip(comboBoxSpeed, resources.GetString("comboBoxSpeed.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(3, 3);
        label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(80, 15);
        label1.TabIndex = 92;
        label1.Text = "Encode speed";
        toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        // 
        // indexControl
        // 
        indexControl.AutoSize = true;
        indexControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        indexControl.BoxWidth = 40;
        indexControl.Location = new System.Drawing.Point(7, 183);
        indexControl.Margin = new System.Windows.Forms.Padding(0);
        indexControl.Name = "indexControl";
        indexControl.Size = new System.Drawing.Size(134, 41);
        indexControl.TabIndex = 93;
        toolTip.SetToolTip(indexControl, resources.GetString("indexControl.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        indexControl.ValueChanged += numericBoxAxisU_ValueChanged;
        // 
        // checkBoxRotation
        // 
        checkBoxRotation.AutoSize = true;
        checkBoxRotation.Checked = true;
        checkBoxRotation.CheckState = System.Windows.Forms.CheckState.Checked;
        checkBoxRotation.Location = new System.Drawing.Point(3, 3);
        checkBoxRotation.Name = "checkBoxRotation";
        checkBoxRotation.Size = new System.Drawing.Size(71, 19);
        checkBoxRotation.TabIndex = 86;
        checkBoxRotation.Text = "Rotation";
        toolTip.SetToolTip(checkBoxRotation, resources.GetString("checkBoxRotation.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        checkBoxRotation.UseVisualStyleBackColor = true;
        checkBoxRotation.CheckedChanged += motionCheckBox_CheckedChanged;
        // 
        // checkBoxTranslation
        // 
        checkBoxTranslation.AutoSize = true;
        checkBoxTranslation.Location = new System.Drawing.Point(80, 3);
        checkBoxTranslation.Name = "checkBoxTranslation";
        checkBoxTranslation.Size = new System.Drawing.Size(83, 19);
        checkBoxTranslation.TabIndex = 86;
        checkBoxTranslation.Text = "Translation";
        toolTip.SetToolTip(checkBoxTranslation, resources.GetString("checkBoxTranslation.ToolTip")); // 260703Cl 多言語 resx へ復元 (260531Cl 方式)
        checkBoxTranslation.UseVisualStyleBackColor = true;
        checkBoxTranslation.CheckedChanged += motionCheckBox_CheckedChanged;
        // 
        // tableLayoutPanelCurrent
        // 
        tableLayoutPanelCurrent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tableLayoutPanelCurrent.ColumnCount = 3;
        tableLayoutPanelCurrent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        tableLayoutPanelCurrent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanelCurrent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
        tableLayoutPanelCurrent.Controls.Add(tableLayoutPanel2, 1, 1);
        tableLayoutPanelCurrent.Controls.Add(buttonTopLeft, 0, 0);
        tableLayoutPanelCurrent.Controls.Add(buttonLeft, 0, 1);
        tableLayoutPanelCurrent.Controls.Add(buttonBottomLeft, 0, 2);
        tableLayoutPanelCurrent.Controls.Add(buttonBottom, 1, 2);
        tableLayoutPanelCurrent.Controls.Add(buttonBottomRight, 2, 2);
        tableLayoutPanelCurrent.Controls.Add(buttonTop, 1, 0);
        tableLayoutPanelCurrent.Controls.Add(buttonTopRight, 2, 0);
        tableLayoutPanelCurrent.Controls.Add(buttonRight, 2, 1);
        tableLayoutPanelCurrent.Location = new System.Drawing.Point(4, 40);
        tableLayoutPanelCurrent.Margin = new System.Windows.Forms.Padding(1);
        tableLayoutPanelCurrent.Name = "tableLayoutPanelCurrent";
        tableLayoutPanelCurrent.RowCount = 3;
        tableLayoutPanelCurrent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanelCurrent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanelCurrent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanelCurrent.Size = new System.Drawing.Size(156, 91);
        tableLayoutPanelCurrent.TabIndex = 85;
        // 
        // tableLayoutPanel2
        // 
        tableLayoutPanel2.ColumnCount = 2;
        tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel2.Controls.Add(buttonAntiClock, 1, 0);
        tableLayoutPanel2.Controls.Add(buttonClock, 0, 0);
        tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
        tableLayoutPanel2.Location = new System.Drawing.Point(39, 30);
        tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
        tableLayoutPanel2.Name = "tableLayoutPanel2";
        tableLayoutPanel2.RowCount = 1;
        tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
        tableLayoutPanel2.Size = new System.Drawing.Size(78, 30);
        tableLayoutPanel2.TabIndex = 85;
        // 
        // groupBoxDirection
        // 
        groupBoxDirection.Controls.Add(indexControl);
        groupBoxDirection.Controls.Add(radioButtonCurrent);
        groupBoxDirection.Controls.Add(tableLayoutPanelCurrent);
        groupBoxDirection.Controls.Add(radioButtonAxis);
        groupBoxDirection.Controls.Add(radioButtonPlane);
        groupBoxDirection.Location = new System.Drawing.Point(3, 140);
        groupBoxDirection.Name = "groupBoxDirection";
        groupBoxDirection.Size = new System.Drawing.Size(164, 231);
        groupBoxDirection.TabIndex = 89;
        groupBoxDirection.TabStop = false;
        groupBoxDirection.Text = "Direction";
        // 
        // flowLayoutPanel1
        // 
        flowLayoutPanel1.AutoSize = true;
        flowLayoutPanel1.Controls.Add(buttonCancel);
        flowLayoutPanel1.Controls.Add(buttonOK);
        flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
        flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        flowLayoutPanel1.Location = new System.Drawing.Point(0, 511);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        flowLayoutPanel1.Size = new System.Drawing.Size(176, 31);
        flowLayoutPanel1.TabIndex = 93;
        // 
        // flowLayoutPanel2
        // 
        flowLayoutPanel2.AutoSize = true;
        flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        flowLayoutPanel2.Controls.Add(checkBoxRotation);
        flowLayoutPanel2.Controls.Add(checkBoxTranslation);
        flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
        flowLayoutPanel2.Name = "flowLayoutPanel2";
        flowLayoutPanel2.Size = new System.Drawing.Size(166, 25);
        flowLayoutPanel2.TabIndex = 94;
        // 
        // flowLayoutPanel3
        // 
        flowLayoutPanel3.AutoSize = true;
        flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        flowLayoutPanel3.Controls.Add(flowLayoutPanel2);
        flowLayoutPanel3.Controls.Add(numericBoxSpeed);
        flowLayoutPanel3.Controls.Add(numericBoxTranslationSpeed);
        flowLayoutPanel3.Controls.Add(numericBoxDuration);
        flowLayoutPanel3.Controls.Add(checkBoxIncludeFinalFrame);
        flowLayoutPanel3.Controls.Add(groupBoxDirection);
        flowLayoutPanel3.Controls.Add(numericBoxFps);
        flowLayoutPanel3.Controls.Add(flowLayoutPanel4);
        flowLayoutPanel3.Controls.Add(numericBoxQuality);
        flowLayoutPanel3.Controls.Add(flowLayoutPanel5);
        flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
        flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
        flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
        flowLayoutPanel3.Name = "flowLayoutPanel3";
        flowLayoutPanel3.Size = new System.Drawing.Size(176, 488);
        flowLayoutPanel3.TabIndex = 95;
        flowLayoutPanel3.WrapContents = false;
        // 
        // numericBoxTranslationSpeed
        // 
        numericBoxTranslationSpeed.BackColor = System.Drawing.Color.Transparent;
        numericBoxTranslationSpeed.FooterPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxTranslationSpeed.FooterText = "periods/s";
        numericBoxTranslationSpeed.HeaderPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxTranslationSpeed.HeaderText = "Speed";
        numericBoxTranslationSpeed.Location = new System.Drawing.Point(0, 58);
        numericBoxTranslationSpeed.Margin = new System.Windows.Forms.Padding(0);
        numericBoxTranslationSpeed.Maximum = 10D;
        numericBoxTranslationSpeed.MaximumSize = new System.Drawing.Size(1000, 30);
        numericBoxTranslationSpeed.Minimum = -10D;
        numericBoxTranslationSpeed.MinimumSize = new System.Drawing.Size(1, 20);
        numericBoxTranslationSpeed.Name = "numericBoxTranslationSpeed";
        numericBoxTranslationSpeed.RadianValue = 0.0034906585039886592D;
        numericBoxTranslationSpeed.ShowUpDown = true;
        numericBoxTranslationSpeed.Size = new System.Drawing.Size(171, 27);
        numericBoxTranslationSpeed.SkipEventDuringInput = false;
        numericBoxTranslationSpeed.SmartIncrement = true;
        toolTip.SetToolTip(numericBoxTranslationSpeed, resources.GetString("numericBoxTranslationSpeed.ToolTip")); // 260703Cl 追加
        numericBoxTranslationSpeed.TabIndex = 1;
        numericBoxTranslationSpeed.TrimEndZero = true;
        numericBoxTranslationSpeed.Value = 0.2D;
        numericBoxTranslationSpeed.ValueBoxWidth = 45;
        numericBoxTranslationSpeed.ValueFontSize = 9F;
        numericBoxTranslationSpeed.Visible = false;
        // 
        // flowLayoutPanel4
        // 
        flowLayoutPanel4.AutoSize = true;
        flowLayoutPanel4.Controls.Add(radioButtonH264);
        flowLayoutPanel4.Controls.Add(radioButtonH265);
        flowLayoutPanel4.Location = new System.Drawing.Point(3, 404);
        flowLayoutPanel4.Name = "flowLayoutPanel4";
        flowLayoutPanel4.Size = new System.Drawing.Size(116, 25);
        flowLayoutPanel4.TabIndex = 96;
        // 
        // flowLayoutPanel5
        // 
        flowLayoutPanel5.AutoSize = true;
        flowLayoutPanel5.Controls.Add(label1);
        flowLayoutPanel5.Controls.Add(comboBoxSpeed);
        flowLayoutPanel5.Location = new System.Drawing.Point(3, 462);
        flowLayoutPanel5.Name = "flowLayoutPanel5";
        flowLayoutPanel5.Size = new System.Drawing.Size(161, 23);
        flowLayoutPanel5.TabIndex = 97;
        // 
        // FormMovie
        // 
        AcceptButton = buttonOK;
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        AutoSize = true; // 260703Cl 復元: 再シリアライズで消失 (Rotation+Translation 両 ON 時のクリップ防止)
        AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        CancelButton = buttonCancel;
        captureExtender.SetCapture(this, true);
        ClientSize = new System.Drawing.Size(176, 542);
        ControlBox = false;
        Controls.Add(flowLayoutPanel3);
        Controls.Add(flowLayoutPanel1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormMovie";
        ShowIcon = false;
        StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        Text = "Movie setting";
        tableLayoutPanelCurrent.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        groupBoxDirection.ResumeLayout(false);
        groupBoxDirection.PerformLayout();
        flowLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel2.ResumeLayout(false);
        flowLayoutPanel2.PerformLayout();
        flowLayoutPanel3.ResumeLayout(false);
        flowLayoutPanel3.PerformLayout();
        flowLayoutPanel4.ResumeLayout(false);
        flowLayoutPanel4.PerformLayout();
        flowLayoutPanel5.ResumeLayout(false);
        flowLayoutPanel5.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.ToolTip toolTip; // 260531Cl
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;
    private NumericBox numericBoxSpeed;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCurrent;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.Button buttonAntiClock;
    private System.Windows.Forms.Button buttonClock;
    private System.Windows.Forms.Button buttonTopLeft;
    private System.Windows.Forms.Button buttonLeft;
    private System.Windows.Forms.Button buttonBottomLeft;
    private System.Windows.Forms.Button buttonBottom;
    private System.Windows.Forms.Button buttonBottomRight;
    private System.Windows.Forms.Button buttonTop;
    private System.Windows.Forms.Button buttonTopRight;
    private System.Windows.Forms.Button buttonRight;
    private NumericBox numericBoxDuration;
    private NumericBox numericBoxFps; // (260629Ch)
    private System.Windows.Forms.CheckBox checkBoxIncludeFinalFrame; // (260629Ch)
    private NumericBox numericBoxQuality; // (260629Ch)
    private System.Windows.Forms.RadioButton radioButtonAxis;
    private System.Windows.Forms.RadioButton radioButtonPlane;
    private System.Windows.Forms.RadioButton radioButtonCurrent;
    private System.Windows.Forms.GroupBox groupBoxDirection;
    private System.Windows.Forms.RadioButton radioButtonH264;
    private System.Windows.Forms.RadioButton radioButtonH265;
    private System.Windows.Forms.ComboBox comboBoxSpeed;
    private System.Windows.Forms.Label label1;
    private IndexControl indexControl;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.CheckBox checkBoxRotation; // (260629Ch)
    private System.Windows.Forms.CheckBox checkBoxTranslation; // (260629Ch)
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
    private NumericBox numericBoxTranslationSpeed;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
}
