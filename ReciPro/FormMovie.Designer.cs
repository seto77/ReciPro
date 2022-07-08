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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericBoxSpeed = new Crystallography.Controls.NumericBox();
            this.tableLayoutPanelCurrent = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAntiClock = new System.Windows.Forms.Button();
            this.buttonClock = new System.Windows.Forms.Button();
            this.buttonTopLeft = new System.Windows.Forms.Button();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonBottomLeft = new System.Windows.Forms.Button();
            this.buttonBottom = new System.Windows.Forms.Button();
            this.buttonBottomRight = new System.Windows.Forms.Button();
            this.buttonTop = new System.Windows.Forms.Button();
            this.buttonTopRight = new System.Windows.Forms.Button();
            this.buttonRight = new System.Windows.Forms.Button();
            this.numericBoxDuration = new Crystallography.Controls.NumericBox();
            this.radioButtonAxis = new System.Windows.Forms.RadioButton();
            this.radioButtonPlane = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanelAxis = new System.Windows.Forms.TableLayoutPanel();
            this.numericBoxAxisU = new Crystallography.Controls.NumericBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numericBoxAxisV = new Crystallography.Controls.NumericBox();
            this.numericBoxAxisW = new Crystallography.Controls.NumericBox();
            this.tableLayoutPanelPlane = new System.Windows.Forms.TableLayoutPanel();
            this.numericBoxPlaneL = new Crystallography.Controls.NumericBox();
            this.numericBoxPlaneH = new Crystallography.Controls.NumericBox();
            this.numericBoxPlaneK = new Crystallography.Controls.NumericBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.radioButtonCurrent = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelCurrent.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanelAxis.SuspendLayout();
            this.tableLayoutPanelPlane.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(13, 318);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(94, 318);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // numericBoxSpeed
            // 
            this.numericBoxSpeed.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxSpeed.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxSpeed.FooterText = "°/sec";
            this.numericBoxSpeed.HeaderText = "Speed";
            this.numericBoxSpeed.Location = new System.Drawing.Point(9, 6);
            this.numericBoxSpeed.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxSpeed.Maximum = 360D;
            this.numericBoxSpeed.MaximumSize = new System.Drawing.Size(1000, 27);
            this.numericBoxSpeed.Minimum = 0D;
            this.numericBoxSpeed.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxSpeed.Name = "numericBoxSpeed";
            this.numericBoxSpeed.RadianValue = 0.52359877559829882D;
            this.numericBoxSpeed.RoundErrorAccuracy = -1;
            this.numericBoxSpeed.ShowUpDown = true;
            this.numericBoxSpeed.Size = new System.Drawing.Size(166, 27);
            this.numericBoxSpeed.SkipEventDuringInput = false;
            this.numericBoxSpeed.SmartIncrement = true;
            this.numericBoxSpeed.TabIndex = 1;
            this.numericBoxSpeed.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxSpeed.TrimEndZero = true;
            this.numericBoxSpeed.Value = 30D;
            // 
            // tableLayoutPanelCurrent
            // 
            this.tableLayoutPanelCurrent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelCurrent.ColumnCount = 3;
            this.tableLayoutPanelCurrent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelCurrent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelCurrent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelCurrent.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanelCurrent.Controls.Add(this.buttonTopLeft, 0, 0);
            this.tableLayoutPanelCurrent.Controls.Add(this.buttonLeft, 0, 1);
            this.tableLayoutPanelCurrent.Controls.Add(this.buttonBottomLeft, 0, 2);
            this.tableLayoutPanelCurrent.Controls.Add(this.buttonBottom, 1, 2);
            this.tableLayoutPanelCurrent.Controls.Add(this.buttonBottomRight, 2, 2);
            this.tableLayoutPanelCurrent.Controls.Add(this.buttonTop, 1, 0);
            this.tableLayoutPanelCurrent.Controls.Add(this.buttonTopRight, 2, 0);
            this.tableLayoutPanelCurrent.Controls.Add(this.buttonRight, 2, 1);
            this.tableLayoutPanelCurrent.Location = new System.Drawing.Point(4, 40);
            this.tableLayoutPanelCurrent.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanelCurrent.Name = "tableLayoutPanelCurrent";
            this.tableLayoutPanelCurrent.RowCount = 3;
            this.tableLayoutPanelCurrent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelCurrent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelCurrent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelCurrent.Size = new System.Drawing.Size(156, 91);
            this.tableLayoutPanelCurrent.TabIndex = 85;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.buttonAntiClock, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonClock, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(39, 30);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(78, 30);
            this.tableLayoutPanel2.TabIndex = 85;
            // 
            // buttonAntiClock
            // 
            this.buttonAntiClock.BackColor = System.Drawing.SystemColors.Control;
            this.buttonAntiClock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonAntiClock.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAntiClock.ForeColor = System.Drawing.Color.Gray;
            this.buttonAntiClock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAntiClock.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAntiClock.Location = new System.Drawing.Point(39, 0);
            this.buttonAntiClock.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAntiClock.Name = "buttonAntiClock";
            this.buttonAntiClock.Size = new System.Drawing.Size(39, 30);
            this.buttonAntiClock.TabIndex = 1;
            this.buttonAntiClock.Text = "⭯";
            this.buttonAntiClock.UseVisualStyleBackColor = false;
            this.buttonAntiClock.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonClock
            // 
            this.buttonClock.BackColor = System.Drawing.SystemColors.Control;
            this.buttonClock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonClock.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonClock.ForeColor = System.Drawing.Color.Gray;
            this.buttonClock.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonClock.Location = new System.Drawing.Point(0, 0);
            this.buttonClock.Margin = new System.Windows.Forms.Padding(0);
            this.buttonClock.Name = "buttonClock";
            this.buttonClock.Size = new System.Drawing.Size(39, 30);
            this.buttonClock.TabIndex = 0;
            this.buttonClock.Text = "⭮";
            this.buttonClock.UseVisualStyleBackColor = false;
            this.buttonClock.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonTopLeft
            // 
            this.buttonTopLeft.BackColor = System.Drawing.SystemColors.Control;
            this.buttonTopLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTopLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonTopLeft.ForeColor = System.Drawing.Color.Gray;
            this.buttonTopLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonTopLeft.Location = new System.Drawing.Point(0, 0);
            this.buttonTopLeft.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTopLeft.Name = "buttonTopLeft";
            this.buttonTopLeft.Size = new System.Drawing.Size(39, 30);
            this.buttonTopLeft.TabIndex = 0;
            this.buttonTopLeft.Text = "⭦";
            this.buttonTopLeft.UseVisualStyleBackColor = false;
            this.buttonTopLeft.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonLeft
            // 
            this.buttonLeft.BackColor = System.Drawing.SystemColors.Control;
            this.buttonLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonLeft.ForeColor = System.Drawing.Color.Gray;
            this.buttonLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonLeft.Location = new System.Drawing.Point(0, 30);
            this.buttonLeft.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(39, 30);
            this.buttonLeft.TabIndex = 1;
            this.buttonLeft.Text = "⭠";
            this.buttonLeft.UseVisualStyleBackColor = false;
            this.buttonLeft.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonBottomLeft
            // 
            this.buttonBottomLeft.BackColor = System.Drawing.SystemColors.Control;
            this.buttonBottomLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBottomLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonBottomLeft.ForeColor = System.Drawing.Color.Gray;
            this.buttonBottomLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBottomLeft.Location = new System.Drawing.Point(0, 60);
            this.buttonBottomLeft.Margin = new System.Windows.Forms.Padding(0);
            this.buttonBottomLeft.Name = "buttonBottomLeft";
            this.buttonBottomLeft.Size = new System.Drawing.Size(39, 31);
            this.buttonBottomLeft.TabIndex = 2;
            this.buttonBottomLeft.Text = "⭩";
            this.buttonBottomLeft.UseVisualStyleBackColor = false;
            this.buttonBottomLeft.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonBottom
            // 
            this.buttonBottom.BackColor = System.Drawing.SystemColors.Control;
            this.buttonBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBottom.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonBottom.ForeColor = System.Drawing.Color.Gray;
            this.buttonBottom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBottom.Location = new System.Drawing.Point(39, 60);
            this.buttonBottom.Margin = new System.Windows.Forms.Padding(0);
            this.buttonBottom.Name = "buttonBottom";
            this.buttonBottom.Size = new System.Drawing.Size(78, 31);
            this.buttonBottom.TabIndex = 4;
            this.buttonBottom.Text = "⭣";
            this.buttonBottom.UseVisualStyleBackColor = false;
            this.buttonBottom.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonBottomRight
            // 
            this.buttonBottomRight.BackColor = System.Drawing.SystemColors.Control;
            this.buttonBottomRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonBottomRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonBottomRight.ForeColor = System.Drawing.Color.Gray;
            this.buttonBottomRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonBottomRight.Location = new System.Drawing.Point(117, 60);
            this.buttonBottomRight.Margin = new System.Windows.Forms.Padding(0);
            this.buttonBottomRight.Name = "buttonBottomRight";
            this.buttonBottomRight.Size = new System.Drawing.Size(39, 31);
            this.buttonBottomRight.TabIndex = 7;
            this.buttonBottomRight.Text = "⭨";
            this.buttonBottomRight.UseVisualStyleBackColor = false;
            this.buttonBottomRight.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonTop
            // 
            this.buttonTop.BackColor = System.Drawing.SystemColors.Control;
            this.buttonTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTop.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonTop.ForeColor = System.Drawing.Color.Gray;
            this.buttonTop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonTop.Location = new System.Drawing.Point(39, 0);
            this.buttonTop.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTop.Name = "buttonTop";
            this.buttonTop.Size = new System.Drawing.Size(78, 30);
            this.buttonTop.TabIndex = 3;
            this.buttonTop.Text = "⭡";
            this.buttonTop.UseVisualStyleBackColor = false;
            this.buttonTop.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonTopRight
            // 
            this.buttonTopRight.BackColor = System.Drawing.SystemColors.Control;
            this.buttonTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTopRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonTopRight.ForeColor = System.Drawing.Color.Gray;
            this.buttonTopRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonTopRight.Location = new System.Drawing.Point(117, 0);
            this.buttonTopRight.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTopRight.Name = "buttonTopRight";
            this.buttonTopRight.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.buttonTopRight.Size = new System.Drawing.Size(39, 30);
            this.buttonTopRight.TabIndex = 5;
            this.buttonTopRight.Text = "⭧";
            this.buttonTopRight.UseVisualStyleBackColor = false;
            this.buttonTopRight.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // buttonRight
            // 
            this.buttonRight.BackColor = System.Drawing.SystemColors.Control;
            this.buttonRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRight.ForeColor = System.Drawing.Color.Blue;
            this.buttonRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonRight.Location = new System.Drawing.Point(117, 30);
            this.buttonRight.Margin = new System.Windows.Forms.Padding(0);
            this.buttonRight.Name = "buttonRight";
            this.buttonRight.Size = new System.Drawing.Size(39, 30);
            this.buttonRight.TabIndex = 6;
            this.buttonRight.Text = "⭢";
            this.buttonRight.UseVisualStyleBackColor = false;
            this.buttonRight.Click += new System.EventHandler(this.buttonDirection_Click);
            // 
            // numericBoxDuration
            // 
            this.numericBoxDuration.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxDuration.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDuration.FooterText = "sec";
            this.numericBoxDuration.HeaderText = "Duration";
            this.numericBoxDuration.Location = new System.Drawing.Point(9, 34);
            this.numericBoxDuration.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDuration.Maximum = 360D;
            this.numericBoxDuration.MaximumSize = new System.Drawing.Size(1000, 27);
            this.numericBoxDuration.Minimum = 1D;
            this.numericBoxDuration.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDuration.Name = "numericBoxDuration";
            this.numericBoxDuration.RadianValue = 0.20943951023931953D;
            this.numericBoxDuration.RoundErrorAccuracy = -1;
            this.numericBoxDuration.ShowUpDown = true;
            this.numericBoxDuration.Size = new System.Drawing.Size(156, 27);
            this.numericBoxDuration.SkipEventDuringInput = false;
            this.numericBoxDuration.SmartIncrement = true;
            this.numericBoxDuration.TabIndex = 1;
            this.numericBoxDuration.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDuration.TrimEndZero = true;
            this.numericBoxDuration.Value = 12D;
            // 
            // radioButtonAxis
            // 
            this.radioButtonAxis.AutoSize = true;
            this.radioButtonAxis.Location = new System.Drawing.Point(6, 136);
            this.radioButtonAxis.Name = "radioButtonAxis";
            this.radioButtonAxis.Size = new System.Drawing.Size(83, 19);
            this.radioButtonAxis.TabIndex = 86;
            this.radioButtonAxis.Text = "Crystal axis";
            this.radioButtonAxis.UseVisualStyleBackColor = true;
            this.radioButtonAxis.CheckedChanged += new System.EventHandler(this.radioButtonCurrent_CheckedChanged);
            // 
            // radioButtonPlane
            // 
            this.radioButtonPlane.AutoSize = true;
            this.radioButtonPlane.Location = new System.Drawing.Point(6, 192);
            this.radioButtonPlane.Name = "radioButtonPlane";
            this.radioButtonPlane.Size = new System.Drawing.Size(92, 19);
            this.radioButtonPlane.TabIndex = 86;
            this.radioButtonPlane.Text = "Crystal plane";
            this.radioButtonPlane.UseVisualStyleBackColor = true;
            this.radioButtonPlane.CheckedChanged += new System.EventHandler(this.radioButtonCurrent_CheckedChanged);
            // 
            // tableLayoutPanelAxis
            // 
            this.tableLayoutPanelAxis.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelAxis.ColumnCount = 5;
            this.tableLayoutPanelAxis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelAxis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelAxis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelAxis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelAxis.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelAxis.Controls.Add(this.numericBoxAxisU, 1, 0);
            this.tableLayoutPanelAxis.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanelAxis.Controls.Add(this.label10, 4, 0);
            this.tableLayoutPanelAxis.Controls.Add(this.numericBoxAxisV, 2, 0);
            this.tableLayoutPanelAxis.Controls.Add(this.numericBoxAxisW, 3, 0);
            this.tableLayoutPanelAxis.Enabled = false;
            this.tableLayoutPanelAxis.Location = new System.Drawing.Point(4, 158);
            this.tableLayoutPanelAxis.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.tableLayoutPanelAxis.MinimumSize = new System.Drawing.Size(0, 29);
            this.tableLayoutPanelAxis.Name = "tableLayoutPanelAxis";
            this.tableLayoutPanelAxis.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.tableLayoutPanelAxis.RowCount = 1;
            this.tableLayoutPanelAxis.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelAxis.Size = new System.Drawing.Size(158, 29);
            this.tableLayoutPanelAxis.TabIndex = 87;
            // 
            // numericBoxAxisU
            // 
            this.numericBoxAxisU.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxAxisU.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAxisU.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxAxisU.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxAxisU.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAxisU.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAxisU.Location = new System.Drawing.Point(12, 3);
            this.numericBoxAxisU.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.numericBoxAxisU.Maximum = 50D;
            this.numericBoxAxisU.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxAxisU.Minimum = -50D;
            this.numericBoxAxisU.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxAxisU.Name = "numericBoxAxisU";
            this.numericBoxAxisU.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxAxisU.RoundErrorAccuracy = -1;
            this.numericBoxAxisU.ShowUpDown = true;
            this.numericBoxAxisU.Size = new System.Drawing.Size(44, 25);
            this.numericBoxAxisU.SkipEventDuringInput = false;
            this.numericBoxAxisU.TabIndex = 0;
            this.numericBoxAxisU.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxAxisU.ThonsandsSeparator = true;
            this.numericBoxAxisU.ToolTip = "Set crystal plane";
            this.numericBoxAxisU.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxAxisU_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(1, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label9.Size = new System.Drawing.Size(11, 19);
            this.label9.TabIndex = 3;
            this.label9.Text = "[";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(144, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label10.Size = new System.Drawing.Size(11, 19);
            this.label10.TabIndex = 84;
            this.label10.Text = "]";
            // 
            // numericBoxAxisV
            // 
            this.numericBoxAxisV.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxAxisV.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAxisV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxAxisV.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxAxisV.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAxisV.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAxisV.Location = new System.Drawing.Point(56, 3);
            this.numericBoxAxisV.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.numericBoxAxisV.Maximum = 50D;
            this.numericBoxAxisV.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxAxisV.Minimum = -50D;
            this.numericBoxAxisV.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxAxisV.Name = "numericBoxAxisV";
            this.numericBoxAxisV.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxAxisV.RoundErrorAccuracy = -1;
            this.numericBoxAxisV.ShowUpDown = true;
            this.numericBoxAxisV.Size = new System.Drawing.Size(44, 25);
            this.numericBoxAxisV.SkipEventDuringInput = false;
            this.numericBoxAxisV.TabIndex = 1;
            this.numericBoxAxisV.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxAxisV.ThonsandsSeparator = true;
            this.numericBoxAxisV.ToolTip = "Set crystal plane";
            this.numericBoxAxisV.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxAxisU_ValueChanged);
            // 
            // numericBoxAxisW
            // 
            this.numericBoxAxisW.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxAxisW.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAxisW.DecimalPlaces = 0;
            this.numericBoxAxisW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxAxisW.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxAxisW.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAxisW.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAxisW.Location = new System.Drawing.Point(100, 3);
            this.numericBoxAxisW.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.numericBoxAxisW.Maximum = 50D;
            this.numericBoxAxisW.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxAxisW.Minimum = -50D;
            this.numericBoxAxisW.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxAxisW.Name = "numericBoxAxisW";
            this.numericBoxAxisW.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxAxisW.RoundErrorAccuracy = -1;
            this.numericBoxAxisW.ShowUpDown = true;
            this.numericBoxAxisW.Size = new System.Drawing.Size(44, 25);
            this.numericBoxAxisW.SkipEventDuringInput = false;
            this.numericBoxAxisW.TabIndex = 2;
            this.numericBoxAxisW.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxAxisW.ThonsandsSeparator = true;
            this.numericBoxAxisW.ToolTip = "Set crystal plane";
            this.numericBoxAxisW.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxAxisU_ValueChanged);
            // 
            // tableLayoutPanelPlane
            // 
            this.tableLayoutPanelPlane.AutoSize = true;
            this.tableLayoutPanelPlane.ColumnCount = 5;
            this.tableLayoutPanelPlane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelPlane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelPlane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelPlane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelPlane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelPlane.Controls.Add(this.numericBoxPlaneL, 3, 0);
            this.tableLayoutPanelPlane.Controls.Add(this.numericBoxPlaneH, 0, 0);
            this.tableLayoutPanelPlane.Controls.Add(this.numericBoxPlaneK, 0, 0);
            this.tableLayoutPanelPlane.Controls.Add(this.label11, 4, 0);
            this.tableLayoutPanelPlane.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanelPlane.Enabled = false;
            this.tableLayoutPanelPlane.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tableLayoutPanelPlane.Location = new System.Drawing.Point(4, 214);
            this.tableLayoutPanelPlane.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelPlane.MinimumSize = new System.Drawing.Size(0, 29);
            this.tableLayoutPanelPlane.Name = "tableLayoutPanelPlane";
            this.tableLayoutPanelPlane.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.tableLayoutPanelPlane.RowCount = 1;
            this.tableLayoutPanelPlane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelPlane.Size = new System.Drawing.Size(158, 29);
            this.tableLayoutPanelPlane.TabIndex = 88;
            // 
            // numericBoxPlaneL
            // 
            this.numericBoxPlaneL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxPlaneL.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPlaneL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxPlaneL.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPlaneL.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPlaneL.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPlaneL.Location = new System.Drawing.Point(100, 3);
            this.numericBoxPlaneL.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.numericBoxPlaneL.Maximum = 50D;
            this.numericBoxPlaneL.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPlaneL.Minimum = -50D;
            this.numericBoxPlaneL.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxPlaneL.Name = "numericBoxPlaneL";
            this.numericBoxPlaneL.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPlaneL.RoundErrorAccuracy = -1;
            this.numericBoxPlaneL.ShowUpDown = true;
            this.numericBoxPlaneL.Size = new System.Drawing.Size(44, 25);
            this.numericBoxPlaneL.SkipEventDuringInput = false;
            this.numericBoxPlaneL.TabIndex = 2;
            this.numericBoxPlaneL.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPlaneL.ThonsandsSeparator = true;
            this.numericBoxPlaneL.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPlaneH_ValueChanged);
            // 
            // numericBoxPlaneH
            // 
            this.numericBoxPlaneH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxPlaneH.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPlaneH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxPlaneH.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPlaneH.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPlaneH.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPlaneH.Location = new System.Drawing.Point(12, 3);
            this.numericBoxPlaneH.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.numericBoxPlaneH.Maximum = 50D;
            this.numericBoxPlaneH.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPlaneH.Minimum = -50D;
            this.numericBoxPlaneH.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxPlaneH.Name = "numericBoxPlaneH";
            this.numericBoxPlaneH.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPlaneH.RoundErrorAccuracy = -1;
            this.numericBoxPlaneH.ShowUpDown = true;
            this.numericBoxPlaneH.Size = new System.Drawing.Size(44, 25);
            this.numericBoxPlaneH.SkipEventDuringInput = false;
            this.numericBoxPlaneH.TabIndex = 0;
            this.numericBoxPlaneH.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPlaneH.ThonsandsSeparator = true;
            this.numericBoxPlaneH.ToolTip = "Set crystal plane";
            this.numericBoxPlaneH.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPlaneH_ValueChanged);
            // 
            // numericBoxPlaneK
            // 
            this.numericBoxPlaneK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxPlaneK.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPlaneK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxPlaneK.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPlaneK.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPlaneK.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPlaneK.Location = new System.Drawing.Point(56, 3);
            this.numericBoxPlaneK.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.numericBoxPlaneK.Maximum = 50D;
            this.numericBoxPlaneK.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPlaneK.Minimum = -50D;
            this.numericBoxPlaneK.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxPlaneK.Name = "numericBoxPlaneK";
            this.numericBoxPlaneK.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPlaneK.RoundErrorAccuracy = -1;
            this.numericBoxPlaneK.ShowUpDown = true;
            this.numericBoxPlaneK.Size = new System.Drawing.Size(44, 25);
            this.numericBoxPlaneK.SkipEventDuringInput = false;
            this.numericBoxPlaneK.TabIndex = 1;
            this.numericBoxPlaneK.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPlaneK.ThonsandsSeparator = true;
            this.numericBoxPlaneK.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPlaneH_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(144, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label11.Size = new System.Drawing.Size(11, 19);
            this.label11.TabIndex = 84;
            this.label11.Text = ")";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(1, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.label12.Size = new System.Drawing.Size(11, 19);
            this.label12.TabIndex = 3;
            this.label12.Text = "(";
            // 
            // radioButtonCurrent
            // 
            this.radioButtonCurrent.AutoSize = true;
            this.radioButtonCurrent.Checked = true;
            this.radioButtonCurrent.Location = new System.Drawing.Point(6, 18);
            this.radioButtonCurrent.Name = "radioButtonCurrent";
            this.radioButtonCurrent.Size = new System.Drawing.Size(121, 19);
            this.radioButtonCurrent.TabIndex = 86;
            this.radioButtonCurrent.TabStop = true;
            this.radioButtonCurrent.Text = "Current projection";
            this.radioButtonCurrent.UseVisualStyleBackColor = true;
            this.radioButtonCurrent.CheckedChanged += new System.EventHandler(this.radioButtonCurrent_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonCurrent);
            this.groupBox1.Controls.Add(this.tableLayoutPanelPlane);
            this.groupBox1.Controls.Add(this.tableLayoutPanelCurrent);
            this.groupBox1.Controls.Add(this.tableLayoutPanelAxis);
            this.groupBox1.Controls.Add(this.radioButtonAxis);
            this.groupBox1.Controls.Add(this.radioButtonPlane);
            this.groupBox1.Location = new System.Drawing.Point(9, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 249);
            this.groupBox1.TabIndex = 89;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Direction";
            // 
            // FormMovie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(178, 346);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.numericBoxDuration);
            this.Controls.Add(this.numericBoxSpeed);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMovie";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Movie setting";
            this.tableLayoutPanelCurrent.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanelAxis.ResumeLayout(false);
            this.tableLayoutPanelAxis.PerformLayout();
            this.tableLayoutPanelPlane.ResumeLayout(false);
            this.tableLayoutPanelPlane.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

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
}