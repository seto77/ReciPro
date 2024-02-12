namespace ReciPro;

partial class FormMovie
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
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
        buttonOK = new System.Windows.Forms.Button();
        buttonCancel = new System.Windows.Forms.Button();
        numericBoxSpeed = new NumericBox();
        tableLayoutPanelCurrent = new System.Windows.Forms.TableLayoutPanel();
        tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
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
        radioButtonAxis = new System.Windows.Forms.RadioButton();
        radioButtonPlane = new System.Windows.Forms.RadioButton();
        tableLayoutPanelAxis = new System.Windows.Forms.TableLayoutPanel();
        numericBoxAxisU = new NumericBox();
        label9 = new System.Windows.Forms.Label();
        label10 = new System.Windows.Forms.Label();
        numericBoxAxisV = new NumericBox();
        numericBoxAxisW = new NumericBox();
        tableLayoutPanelPlane = new System.Windows.Forms.TableLayoutPanel();
        numericBoxPlaneL = new NumericBox();
        numericBoxPlaneH = new NumericBox();
        numericBoxPlaneK = new NumericBox();
        label11 = new System.Windows.Forms.Label();
        label12 = new System.Windows.Forms.Label();
        radioButtonCurrent = new System.Windows.Forms.RadioButton();
        groupBox1 = new System.Windows.Forms.GroupBox();
        radioButtonH264 = new System.Windows.Forms.RadioButton();
        radioButtonH265 = new System.Windows.Forms.RadioButton();
        tableLayoutPanelCurrent.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        tableLayoutPanelAxis.SuspendLayout();
        tableLayoutPanelPlane.SuspendLayout();
        groupBox1.SuspendLayout();
        SuspendLayout();
        // 
        // buttonOK
        // 
        buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        buttonOK.Location = new System.Drawing.Point(100, 341);
        buttonOK.Name = "buttonOK";
        buttonOK.Size = new System.Drawing.Size(75, 23);
        buttonOK.TabIndex = 0;
        buttonOK.Text = "OK";
        buttonOK.UseVisualStyleBackColor = true;
        buttonOK.Click += buttonOK_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        buttonCancel.Location = new System.Drawing.Point(100, 318);
        buttonCancel.Name = "buttonCancel";
        buttonCancel.Size = new System.Drawing.Size(75, 23);
        buttonCancel.TabIndex = 0;
        buttonCancel.Text = "Cancel";
        buttonCancel.UseVisualStyleBackColor = true;
        buttonCancel.Click += buttonCancel_Click;
        // 
        // numericBoxSpeed
        // 
        numericBoxSpeed.BackColor = System.Drawing.Color.Transparent;
        numericBoxSpeed.Font = new System.Drawing.Font("メイリオ", 9F);
        numericBoxSpeed.FooterText = "°/sec";
        numericBoxSpeed.HeaderText = "Speed";
        numericBoxSpeed.Location = new System.Drawing.Point(9, 6);
        numericBoxSpeed.Margin = new System.Windows.Forms.Padding(0);
        numericBoxSpeed.Maximum = 360D;
        numericBoxSpeed.MaximumSize = new System.Drawing.Size(1000, 30);
        numericBoxSpeed.Minimum = 0D;
        numericBoxSpeed.MinimumSize = new System.Drawing.Size(1, 20);
        numericBoxSpeed.Name = "numericBoxSpeed";
        numericBoxSpeed.RadianValue = 0.52359877559829882D;
        numericBoxSpeed.RoundErrorAccuracy = -1;
        numericBoxSpeed.ShowUpDown = true;
        numericBoxSpeed.Size = new System.Drawing.Size(166, 27);
        numericBoxSpeed.SkipEventDuringInput = false;
        numericBoxSpeed.SmartIncrement = true;
        numericBoxSpeed.TabIndex = 1;
        numericBoxSpeed.TextFont = new System.Drawing.Font("メイリオ", 9F);
        numericBoxSpeed.TrimEndZero = true;
        numericBoxSpeed.Value = 30D;
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
        // buttonAntiClock
        // 
        buttonAntiClock.BackColor = System.Drawing.SystemColors.Control;
        buttonAntiClock.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonAntiClock.Font = new System.Drawing.Font("Segoe UI Symbol", 14F);
        buttonAntiClock.ForeColor = System.Drawing.Color.Gray;
        buttonAntiClock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
        buttonAntiClock.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonAntiClock.Location = new System.Drawing.Point(39, 0);
        buttonAntiClock.Margin = new System.Windows.Forms.Padding(0);
        buttonAntiClock.Name = "buttonAntiClock";
        buttonAntiClock.Size = new System.Drawing.Size(39, 30);
        buttonAntiClock.TabIndex = 1;
        buttonAntiClock.Text = "⭯";
        buttonAntiClock.UseVisualStyleBackColor = false;
        buttonAntiClock.Click += buttonDirection_Click;
        // 
        // buttonClock
        // 
        buttonClock.BackColor = System.Drawing.SystemColors.Control;
        buttonClock.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonClock.Font = new System.Drawing.Font("Segoe UI Symbol", 14F);
        buttonClock.ForeColor = System.Drawing.Color.Gray;
        buttonClock.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonClock.Location = new System.Drawing.Point(0, 0);
        buttonClock.Margin = new System.Windows.Forms.Padding(0);
        buttonClock.Name = "buttonClock";
        buttonClock.Size = new System.Drawing.Size(39, 30);
        buttonClock.TabIndex = 0;
        buttonClock.Text = "⭮";
        buttonClock.UseVisualStyleBackColor = false;
        buttonClock.Click += buttonDirection_Click;
        // 
        // buttonTopLeft
        // 
        buttonTopLeft.BackColor = System.Drawing.SystemColors.Control;
        buttonTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonTopLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14F);
        buttonTopLeft.ForeColor = System.Drawing.Color.Gray;
        buttonTopLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonTopLeft.Location = new System.Drawing.Point(0, 0);
        buttonTopLeft.Margin = new System.Windows.Forms.Padding(0);
        buttonTopLeft.Name = "buttonTopLeft";
        buttonTopLeft.Size = new System.Drawing.Size(39, 30);
        buttonTopLeft.TabIndex = 0;
        buttonTopLeft.Text = "⭦";
        buttonTopLeft.UseVisualStyleBackColor = false;
        buttonTopLeft.Click += buttonDirection_Click;
        // 
        // buttonLeft
        // 
        buttonLeft.BackColor = System.Drawing.SystemColors.Control;
        buttonLeft.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14F);
        buttonLeft.ForeColor = System.Drawing.Color.Gray;
        buttonLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonLeft.Location = new System.Drawing.Point(0, 30);
        buttonLeft.Margin = new System.Windows.Forms.Padding(0);
        buttonLeft.Name = "buttonLeft";
        buttonLeft.Size = new System.Drawing.Size(39, 30);
        buttonLeft.TabIndex = 1;
        buttonLeft.Text = "⭠";
        buttonLeft.UseVisualStyleBackColor = false;
        buttonLeft.Click += buttonDirection_Click;
        // 
        // buttonBottomLeft
        // 
        buttonBottomLeft.BackColor = System.Drawing.SystemColors.Control;
        buttonBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonBottomLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14F);
        buttonBottomLeft.ForeColor = System.Drawing.Color.Gray;
        buttonBottomLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonBottomLeft.Location = new System.Drawing.Point(0, 60);
        buttonBottomLeft.Margin = new System.Windows.Forms.Padding(0);
        buttonBottomLeft.Name = "buttonBottomLeft";
        buttonBottomLeft.Size = new System.Drawing.Size(39, 31);
        buttonBottomLeft.TabIndex = 2;
        buttonBottomLeft.Text = "⭩";
        buttonBottomLeft.UseVisualStyleBackColor = false;
        buttonBottomLeft.Click += buttonDirection_Click;
        // 
        // buttonBottom
        // 
        buttonBottom.BackColor = System.Drawing.SystemColors.Control;
        buttonBottom.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonBottom.Font = new System.Drawing.Font("Segoe UI Symbol", 14F);
        buttonBottom.ForeColor = System.Drawing.Color.Gray;
        buttonBottom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonBottom.Location = new System.Drawing.Point(39, 60);
        buttonBottom.Margin = new System.Windows.Forms.Padding(0);
        buttonBottom.Name = "buttonBottom";
        buttonBottom.Size = new System.Drawing.Size(78, 31);
        buttonBottom.TabIndex = 4;
        buttonBottom.Text = "⭣";
        buttonBottom.UseVisualStyleBackColor = false;
        buttonBottom.Click += buttonDirection_Click;
        // 
        // buttonBottomRight
        // 
        buttonBottomRight.BackColor = System.Drawing.SystemColors.Control;
        buttonBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonBottomRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14F);
        buttonBottomRight.ForeColor = System.Drawing.Color.Gray;
        buttonBottomRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonBottomRight.Location = new System.Drawing.Point(117, 60);
        buttonBottomRight.Margin = new System.Windows.Forms.Padding(0);
        buttonBottomRight.Name = "buttonBottomRight";
        buttonBottomRight.Size = new System.Drawing.Size(39, 31);
        buttonBottomRight.TabIndex = 7;
        buttonBottomRight.Text = "⭨";
        buttonBottomRight.UseVisualStyleBackColor = false;
        buttonBottomRight.Click += buttonDirection_Click;
        // 
        // buttonTop
        // 
        buttonTop.BackColor = System.Drawing.SystemColors.Control;
        buttonTop.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonTop.Font = new System.Drawing.Font("Segoe UI Symbol", 14F);
        buttonTop.ForeColor = System.Drawing.Color.Gray;
        buttonTop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonTop.Location = new System.Drawing.Point(39, 0);
        buttonTop.Margin = new System.Windows.Forms.Padding(0);
        buttonTop.Name = "buttonTop";
        buttonTop.Size = new System.Drawing.Size(78, 30);
        buttonTop.TabIndex = 3;
        buttonTop.Text = "⭡";
        buttonTop.UseVisualStyleBackColor = false;
        buttonTop.Click += buttonDirection_Click;
        // 
        // buttonTopRight
        // 
        buttonTopRight.BackColor = System.Drawing.SystemColors.Control;
        buttonTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonTopRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14F);
        buttonTopRight.ForeColor = System.Drawing.Color.Gray;
        buttonTopRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonTopRight.Location = new System.Drawing.Point(117, 0);
        buttonTopRight.Margin = new System.Windows.Forms.Padding(0);
        buttonTopRight.Name = "buttonTopRight";
        buttonTopRight.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
        buttonTopRight.Size = new System.Drawing.Size(39, 30);
        buttonTopRight.TabIndex = 5;
        buttonTopRight.Text = "⭧";
        buttonTopRight.UseVisualStyleBackColor = false;
        buttonTopRight.Click += buttonDirection_Click;
        // 
        // buttonRight
        // 
        buttonRight.BackColor = System.Drawing.SystemColors.Control;
        buttonRight.Dock = System.Windows.Forms.DockStyle.Fill;
        buttonRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14F);
        buttonRight.ForeColor = System.Drawing.Color.Blue;
        buttonRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        buttonRight.Location = new System.Drawing.Point(117, 30);
        buttonRight.Margin = new System.Windows.Forms.Padding(0);
        buttonRight.Name = "buttonRight";
        buttonRight.Size = new System.Drawing.Size(39, 30);
        buttonRight.TabIndex = 6;
        buttonRight.Text = "⭢";
        buttonRight.UseVisualStyleBackColor = false;
        buttonRight.Click += buttonDirection_Click;
        // 
        // numericBoxDuration
        // 
        numericBoxDuration.BackColor = System.Drawing.Color.Transparent;
        numericBoxDuration.Font = new System.Drawing.Font("メイリオ", 9F);
        numericBoxDuration.FooterText = "sec";
        numericBoxDuration.HeaderText = "Duration";
        numericBoxDuration.Location = new System.Drawing.Point(9, 34);
        numericBoxDuration.Margin = new System.Windows.Forms.Padding(0);
        numericBoxDuration.Maximum = 360D;
        numericBoxDuration.MaximumSize = new System.Drawing.Size(1000, 30);
        numericBoxDuration.Minimum = 0.01D;
        numericBoxDuration.MinimumSize = new System.Drawing.Size(1, 20);
        numericBoxDuration.Name = "numericBoxDuration";
        numericBoxDuration.RadianValue = 0.20943951023931953D;
        numericBoxDuration.RoundErrorAccuracy = -1;
        numericBoxDuration.ShowUpDown = true;
        numericBoxDuration.Size = new System.Drawing.Size(156, 27);
        numericBoxDuration.SkipEventDuringInput = false;
        numericBoxDuration.SmartIncrement = true;
        numericBoxDuration.TabIndex = 1;
        numericBoxDuration.TextFont = new System.Drawing.Font("メイリオ", 9F);
        numericBoxDuration.TrimEndZero = true;
        numericBoxDuration.Value = 12D;
        // 
        // radioButtonAxis
        // 
        radioButtonAxis.AutoSize = true;
        radioButtonAxis.Location = new System.Drawing.Point(6, 136);
        radioButtonAxis.Name = "radioButtonAxis";
        radioButtonAxis.Size = new System.Drawing.Size(83, 19);
        radioButtonAxis.TabIndex = 86;
        radioButtonAxis.Text = "Crystal axis";
        radioButtonAxis.UseVisualStyleBackColor = true;
        radioButtonAxis.CheckedChanged += radioButtonCurrent_CheckedChanged;
        // 
        // radioButtonPlane
        // 
        radioButtonPlane.AutoSize = true;
        radioButtonPlane.Location = new System.Drawing.Point(6, 192);
        radioButtonPlane.Name = "radioButtonPlane";
        radioButtonPlane.Size = new System.Drawing.Size(92, 19);
        radioButtonPlane.TabIndex = 86;
        radioButtonPlane.Text = "Crystal plane";
        radioButtonPlane.UseVisualStyleBackColor = true;
        radioButtonPlane.CheckedChanged += radioButtonCurrent_CheckedChanged;
        // 
        // tableLayoutPanelAxis
        // 
        tableLayoutPanelAxis.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        tableLayoutPanelAxis.ColumnCount = 5;
        tableLayoutPanelAxis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
        tableLayoutPanelAxis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanelAxis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanelAxis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanelAxis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
        tableLayoutPanelAxis.Controls.Add(numericBoxAxisU, 1, 0);
        tableLayoutPanelAxis.Controls.Add(label9, 0, 0);
        tableLayoutPanelAxis.Controls.Add(label10, 4, 0);
        tableLayoutPanelAxis.Controls.Add(numericBoxAxisV, 2, 0);
        tableLayoutPanelAxis.Controls.Add(numericBoxAxisW, 3, 0);
        tableLayoutPanelAxis.Enabled = false;
        tableLayoutPanelAxis.Location = new System.Drawing.Point(4, 158);
        tableLayoutPanelAxis.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
        tableLayoutPanelAxis.MinimumSize = new System.Drawing.Size(0, 29);
        tableLayoutPanelAxis.Name = "tableLayoutPanelAxis";
        tableLayoutPanelAxis.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
        tableLayoutPanelAxis.RowCount = 1;
        tableLayoutPanelAxis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanelAxis.Size = new System.Drawing.Size(158, 29);
        tableLayoutPanelAxis.TabIndex = 87;
        // 
        // numericBoxAxisU
        // 
        numericBoxAxisU.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        numericBoxAxisU.BackColor = System.Drawing.SystemColors.Control;
        numericBoxAxisU.Dock = System.Windows.Forms.DockStyle.Fill;
        numericBoxAxisU.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxAxisU.FooterBackColor = System.Drawing.SystemColors.Control;
        numericBoxAxisU.HeaderBackColor = System.Drawing.SystemColors.Control;
        numericBoxAxisU.Location = new System.Drawing.Point(12, 3);
        numericBoxAxisU.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxAxisU.Maximum = 50D;
        numericBoxAxisU.MaximumSize = new System.Drawing.Size(1000, 28);
        numericBoxAxisU.Minimum = -50D;
        numericBoxAxisU.MinimumSize = new System.Drawing.Size(1, 18);
        numericBoxAxisU.Name = "numericBoxAxisU";
        numericBoxAxisU.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
        numericBoxAxisU.RoundErrorAccuracy = -1;
        numericBoxAxisU.ShowUpDown = true;
        numericBoxAxisU.Size = new System.Drawing.Size(44, 26);
        numericBoxAxisU.SkipEventDuringInput = false;
        numericBoxAxisU.TabIndex = 0;
        numericBoxAxisU.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxAxisU.ThonsandsSeparator = true;
        numericBoxAxisU.ToolTip = "Set crystal plane";
        numericBoxAxisU.ValueChanged += numericBoxAxisU_ValueChanged;
        // 
        // label9
        // 
        label9.AutoSize = true;
        label9.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
        label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        label9.Location = new System.Drawing.Point(1, 0);
        label9.Margin = new System.Windows.Forms.Padding(0);
        label9.Name = "label9";
        label9.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
        label9.Size = new System.Drawing.Size(11, 19);
        label9.TabIndex = 3;
        label9.Text = "[";
        // 
        // label10
        // 
        label10.AutoSize = true;
        label10.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
        label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        label10.Location = new System.Drawing.Point(144, 0);
        label10.Margin = new System.Windows.Forms.Padding(0);
        label10.Name = "label10";
        label10.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
        label10.Size = new System.Drawing.Size(11, 19);
        label10.TabIndex = 84;
        label10.Text = "]";
        // 
        // numericBoxAxisV
        // 
        numericBoxAxisV.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        numericBoxAxisV.BackColor = System.Drawing.SystemColors.Control;
        numericBoxAxisV.Dock = System.Windows.Forms.DockStyle.Fill;
        numericBoxAxisV.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxAxisV.FooterBackColor = System.Drawing.SystemColors.Control;
        numericBoxAxisV.HeaderBackColor = System.Drawing.SystemColors.Control;
        numericBoxAxisV.Location = new System.Drawing.Point(56, 3);
        numericBoxAxisV.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxAxisV.Maximum = 50D;
        numericBoxAxisV.MaximumSize = new System.Drawing.Size(1000, 28);
        numericBoxAxisV.Minimum = -50D;
        numericBoxAxisV.MinimumSize = new System.Drawing.Size(1, 18);
        numericBoxAxisV.Name = "numericBoxAxisV";
        numericBoxAxisV.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
        numericBoxAxisV.RoundErrorAccuracy = -1;
        numericBoxAxisV.ShowUpDown = true;
        numericBoxAxisV.Size = new System.Drawing.Size(44, 26);
        numericBoxAxisV.SkipEventDuringInput = false;
        numericBoxAxisV.TabIndex = 1;
        numericBoxAxisV.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxAxisV.ThonsandsSeparator = true;
        numericBoxAxisV.ToolTip = "Set crystal plane";
        numericBoxAxisV.ValueChanged += numericBoxAxisU_ValueChanged;
        // 
        // numericBoxAxisW
        // 
        numericBoxAxisW.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        numericBoxAxisW.BackColor = System.Drawing.SystemColors.Control;
        numericBoxAxisW.DecimalPlaces = 0;
        numericBoxAxisW.Dock = System.Windows.Forms.DockStyle.Fill;
        numericBoxAxisW.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxAxisW.FooterBackColor = System.Drawing.SystemColors.Control;
        numericBoxAxisW.HeaderBackColor = System.Drawing.SystemColors.Control;
        numericBoxAxisW.Location = new System.Drawing.Point(100, 3);
        numericBoxAxisW.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxAxisW.Maximum = 50D;
        numericBoxAxisW.MaximumSize = new System.Drawing.Size(1000, 28);
        numericBoxAxisW.Minimum = -50D;
        numericBoxAxisW.MinimumSize = new System.Drawing.Size(1, 18);
        numericBoxAxisW.Name = "numericBoxAxisW";
        numericBoxAxisW.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
        numericBoxAxisW.RoundErrorAccuracy = -1;
        numericBoxAxisW.ShowUpDown = true;
        numericBoxAxisW.Size = new System.Drawing.Size(44, 26);
        numericBoxAxisW.SkipEventDuringInput = false;
        numericBoxAxisW.TabIndex = 2;
        numericBoxAxisW.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxAxisW.ThonsandsSeparator = true;
        numericBoxAxisW.ToolTip = "Set crystal plane";
        numericBoxAxisW.ValueChanged += numericBoxAxisU_ValueChanged;
        // 
        // tableLayoutPanelPlane
        // 
        tableLayoutPanelPlane.AutoSize = true;
        tableLayoutPanelPlane.ColumnCount = 5;
        tableLayoutPanelPlane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
        tableLayoutPanelPlane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanelPlane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanelPlane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
        tableLayoutPanelPlane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
        tableLayoutPanelPlane.Controls.Add(numericBoxPlaneL, 3, 0);
        tableLayoutPanelPlane.Controls.Add(numericBoxPlaneH, 0, 0);
        tableLayoutPanelPlane.Controls.Add(numericBoxPlaneK, 0, 0);
        tableLayoutPanelPlane.Controls.Add(label11, 4, 0);
        tableLayoutPanelPlane.Controls.Add(label12, 0, 0);
        tableLayoutPanelPlane.Enabled = false;
        tableLayoutPanelPlane.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
        tableLayoutPanelPlane.Location = new System.Drawing.Point(4, 214);
        tableLayoutPanelPlane.Margin = new System.Windows.Forms.Padding(0);
        tableLayoutPanelPlane.MinimumSize = new System.Drawing.Size(0, 29);
        tableLayoutPanelPlane.Name = "tableLayoutPanelPlane";
        tableLayoutPanelPlane.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
        tableLayoutPanelPlane.RowCount = 1;
        tableLayoutPanelPlane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        tableLayoutPanelPlane.Size = new System.Drawing.Size(158, 29);
        tableLayoutPanelPlane.TabIndex = 88;
        // 
        // numericBoxPlaneL
        // 
        numericBoxPlaneL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        numericBoxPlaneL.BackColor = System.Drawing.SystemColors.Control;
        numericBoxPlaneL.Dock = System.Windows.Forms.DockStyle.Fill;
        numericBoxPlaneL.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxPlaneL.FooterBackColor = System.Drawing.SystemColors.Control;
        numericBoxPlaneL.HeaderBackColor = System.Drawing.SystemColors.Control;
        numericBoxPlaneL.Location = new System.Drawing.Point(100, 3);
        numericBoxPlaneL.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxPlaneL.Maximum = 50D;
        numericBoxPlaneL.MaximumSize = new System.Drawing.Size(1000, 28);
        numericBoxPlaneL.Minimum = -50D;
        numericBoxPlaneL.MinimumSize = new System.Drawing.Size(1, 18);
        numericBoxPlaneL.Name = "numericBoxPlaneL";
        numericBoxPlaneL.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
        numericBoxPlaneL.RoundErrorAccuracy = -1;
        numericBoxPlaneL.ShowUpDown = true;
        numericBoxPlaneL.Size = new System.Drawing.Size(44, 26);
        numericBoxPlaneL.SkipEventDuringInput = false;
        numericBoxPlaneL.TabIndex = 2;
        numericBoxPlaneL.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxPlaneL.ThonsandsSeparator = true;
        numericBoxPlaneL.ValueChanged += numericBoxAxisU_ValueChanged;
        // 
        // numericBoxPlaneH
        // 
        numericBoxPlaneH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        numericBoxPlaneH.BackColor = System.Drawing.SystemColors.Control;
        numericBoxPlaneH.Dock = System.Windows.Forms.DockStyle.Fill;
        numericBoxPlaneH.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxPlaneH.FooterBackColor = System.Drawing.SystemColors.Control;
        numericBoxPlaneH.HeaderBackColor = System.Drawing.SystemColors.Control;
        numericBoxPlaneH.Location = new System.Drawing.Point(12, 3);
        numericBoxPlaneH.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxPlaneH.Maximum = 50D;
        numericBoxPlaneH.MaximumSize = new System.Drawing.Size(1000, 28);
        numericBoxPlaneH.Minimum = -50D;
        numericBoxPlaneH.MinimumSize = new System.Drawing.Size(1, 18);
        numericBoxPlaneH.Name = "numericBoxPlaneH";
        numericBoxPlaneH.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
        numericBoxPlaneH.RoundErrorAccuracy = -1;
        numericBoxPlaneH.ShowUpDown = true;
        numericBoxPlaneH.Size = new System.Drawing.Size(44, 26);
        numericBoxPlaneH.SkipEventDuringInput = false;
        numericBoxPlaneH.TabIndex = 0;
        numericBoxPlaneH.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxPlaneH.ThonsandsSeparator = true;
        numericBoxPlaneH.ToolTip = "Set crystal plane";
        numericBoxPlaneH.ValueChanged += numericBoxAxisU_ValueChanged;
        // 
        // numericBoxPlaneK
        // 
        numericBoxPlaneK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        numericBoxPlaneK.BackColor = System.Drawing.SystemColors.Control;
        numericBoxPlaneK.Dock = System.Windows.Forms.DockStyle.Fill;
        numericBoxPlaneK.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxPlaneK.FooterBackColor = System.Drawing.SystemColors.Control;
        numericBoxPlaneK.HeaderBackColor = System.Drawing.SystemColors.Control;
        numericBoxPlaneK.Location = new System.Drawing.Point(56, 3);
        numericBoxPlaneK.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxPlaneK.Maximum = 50D;
        numericBoxPlaneK.MaximumSize = new System.Drawing.Size(1000, 28);
        numericBoxPlaneK.Minimum = -50D;
        numericBoxPlaneK.MinimumSize = new System.Drawing.Size(1, 18);
        numericBoxPlaneK.Name = "numericBoxPlaneK";
        numericBoxPlaneK.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
        numericBoxPlaneK.RoundErrorAccuracy = -1;
        numericBoxPlaneK.ShowUpDown = true;
        numericBoxPlaneK.Size = new System.Drawing.Size(44, 26);
        numericBoxPlaneK.SkipEventDuringInput = false;
        numericBoxPlaneK.TabIndex = 1;
        numericBoxPlaneK.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
        numericBoxPlaneK.ThonsandsSeparator = true;
        numericBoxPlaneK.ValueChanged += numericBoxAxisU_ValueChanged;
        // 
        // label11
        // 
        label11.AutoSize = true;
        label11.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
        label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        label11.Location = new System.Drawing.Point(144, 0);
        label11.Margin = new System.Windows.Forms.Padding(0);
        label11.Name = "label11";
        label11.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
        label11.Size = new System.Drawing.Size(11, 19);
        label11.TabIndex = 84;
        label11.Text = ")";
        // 
        // label12
        // 
        label12.AutoSize = true;
        label12.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
        label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        label12.Location = new System.Drawing.Point(1, 0);
        label12.Margin = new System.Windows.Forms.Padding(0);
        label12.Name = "label12";
        label12.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
        label12.Size = new System.Drawing.Size(11, 19);
        label12.TabIndex = 3;
        label12.Text = "(";
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
        radioButtonCurrent.UseVisualStyleBackColor = true;
        radioButtonCurrent.CheckedChanged += radioButtonCurrent_CheckedChanged;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(radioButtonCurrent);
        groupBox1.Controls.Add(tableLayoutPanelPlane);
        groupBox1.Controls.Add(tableLayoutPanelCurrent);
        groupBox1.Controls.Add(tableLayoutPanelAxis);
        groupBox1.Controls.Add(radioButtonAxis);
        groupBox1.Controls.Add(radioButtonPlane);
        groupBox1.Location = new System.Drawing.Point(9, 64);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(164, 249);
        groupBox1.TabIndex = 89;
        groupBox1.TabStop = false;
        groupBox1.Text = "Direction";
        // 
        // radioButtonH264
        // 
        radioButtonH264.AutoSize = true;
        radioButtonH264.Location = new System.Drawing.Point(12, 318);
        radioButtonH264.Name = "radioButtonH264";
        radioButtonH264.Size = new System.Drawing.Size(52, 19);
        radioButtonH264.TabIndex = 90;
        radioButtonH264.Text = "H264";
        radioButtonH264.UseVisualStyleBackColor = true;
        // 
        // radioButtonH265
        // 
        radioButtonH265.AutoSize = true;
        radioButtonH265.Checked = true;
        radioButtonH265.Location = new System.Drawing.Point(12, 343);
        radioButtonH265.Name = "radioButtonH265";
        radioButtonH265.Size = new System.Drawing.Size(52, 19);
        radioButtonH265.TabIndex = 90;
        radioButtonH265.TabStop = true;
        radioButtonH265.Text = "H265";
        radioButtonH265.UseVisualStyleBackColor = true;
        // 
        // FormMovie
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        AutoSize = true;
        AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        ClientSize = new System.Drawing.Size(180, 367);
        ControlBox = false;
        Controls.Add(radioButtonH265);
        Controls.Add(radioButtonH264);
        Controls.Add(groupBox1);
        Controls.Add(numericBoxDuration);
        Controls.Add(numericBoxSpeed);
        Controls.Add(buttonCancel);
        Controls.Add(buttonOK);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormMovie";
        ShowIcon = false;
        StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        Text = "Movie setting";
        tableLayoutPanelCurrent.ResumeLayout(false);
        tableLayoutPanel2.ResumeLayout(false);
        tableLayoutPanelAxis.ResumeLayout(false);
        tableLayoutPanelAxis.PerformLayout();
        tableLayoutPanelPlane.ResumeLayout(false);
        tableLayoutPanelPlane.PerformLayout();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

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
    private System.Windows.Forms.RadioButton radioButtonAxis;
    private System.Windows.Forms.RadioButton radioButtonPlane;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAxis;
    private NumericBox numericBoxAxisU;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label10;
    private NumericBox numericBoxAxisV;
    private NumericBox numericBoxAxisW;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanelPlane;
    private NumericBox numericBoxPlaneL;
    private NumericBox numericBoxPlaneH;
    private NumericBox numericBoxPlaneK;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.RadioButton radioButtonCurrent;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radioButtonH264;
    private System.Windows.Forms.RadioButton radioButtonH265;
}