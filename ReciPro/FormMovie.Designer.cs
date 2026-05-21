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
        captureExtender.SetCapture(this, true); // 260521Cl 追加: GUI監査キャプチャ対象 (フォーム全体)
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMovie));
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
        radioButtonCurrent = new System.Windows.Forms.RadioButton();
        groupBoxDirection = new System.Windows.Forms.GroupBox();
        radioButtonH264 = new System.Windows.Forms.RadioButton();
        radioButtonH265 = new System.Windows.Forms.RadioButton();
        comboBoxSpeed = new System.Windows.Forms.ComboBox();
        label1 = new System.Windows.Forms.Label();
        indexControl = new IndexControl();
        tableLayoutPanelCurrent.SuspendLayout();
        tableLayoutPanel2.SuspendLayout();
        groupBoxDirection.SuspendLayout();
        SuspendLayout();
        // 
        // buttonOK
        // 
        buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        //buttonOK.Location = new System.Drawing.Point(100, 351);// 260520Cl 変更前 (縦並び右側)
        buttonOK.Location = new System.Drawing.Point(15, 382);// 260520Cl 変更: 横並び左OKに
        buttonOK.Name = "buttonOK";
        //buttonOK.Size = new System.Drawing.Size(75, 23);// 260520Cl 変更前
        buttonOK.Size = new System.Drawing.Size(75, 25);// 260520Cl 変更: 高さ統一(25)
        buttonOK.TabIndex = 0;
        buttonOK.Text = "OK";
        buttonOK.UseVisualStyleBackColor = true;
        buttonOK.Click += buttonOK_Click;
        // 
        // buttonCancel
        // 
        buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        //buttonCancel.Location = new System.Drawing.Point(100, 328);// 260520Cl 変更前 (縦並びOKの上)
        buttonCancel.Location = new System.Drawing.Point(95, 382);// 260520Cl 変更: 横並び右Cancelに
        buttonCancel.Name = "buttonCancel";
        //buttonCancel.Size = new System.Drawing.Size(75, 23);// 260520Cl 変更前
        buttonCancel.Size = new System.Drawing.Size(75, 25);// 260520Cl 変更: 高さ統一(25)
        buttonCancel.TabIndex = 0;
        buttonCancel.Text = "Cancel";
        buttonCancel.UseVisualStyleBackColor = true;
        buttonCancel.Click += buttonCancel_Click;
        // 
        // numericBoxSpeed
        // 
        numericBoxSpeed.BackColor = System.Drawing.Color.Transparent;
        numericBoxSpeed.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
        numericBoxSpeed.FooterPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxSpeed.FooterText = "°/s"; // 260521Cl Phase7: 単位統一 °/sec→°/s
        numericBoxSpeed.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
        numericBoxSpeed.HeaderPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxSpeed.HeaderText = "Speed";
        numericBoxSpeed.Location = new System.Drawing.Point(9, 6);
        numericBoxSpeed.Margin = new System.Windows.Forms.Padding(0);
        numericBoxSpeed.Maximum = 360D;
        numericBoxSpeed.MaximumSize = new System.Drawing.Size(1000, 30);
        numericBoxSpeed.Minimum = 0D;
        numericBoxSpeed.MinimumSize = new System.Drawing.Size(1, 20);
        numericBoxSpeed.Name = "numericBoxSpeed";
        numericBoxSpeed.RadianValue = 0.52359877559829882D;
        numericBoxSpeed.ShowUpDown = true;
        numericBoxSpeed.Size = new System.Drawing.Size(166, 27);
        numericBoxSpeed.SkipEventDuringInput = false;
        numericBoxSpeed.SmartIncrement = true;
        numericBoxSpeed.TabIndex = 1;
        numericBoxSpeed.ValueFontSize = 9F;
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
        buttonAntiClock.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
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
        buttonClock.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
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
        buttonTopLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
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
        buttonLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
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
        buttonBottomLeft.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
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
        buttonBottom.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
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
        buttonBottomRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
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
        buttonTop.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
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
        buttonTopRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
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
        buttonRight.Font = new System.Drawing.Font("Segoe UI Symbol", 14.25F);
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
        numericBoxDuration.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
        numericBoxDuration.FooterPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxDuration.FooterText = "s"; // 260521Cl Phase7: 単位統一 sec→s
        numericBoxDuration.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
        numericBoxDuration.HeaderPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
        numericBoxDuration.HeaderText = "Duration";
        numericBoxDuration.Location = new System.Drawing.Point(9, 34);
        numericBoxDuration.Margin = new System.Windows.Forms.Padding(0);
        numericBoxDuration.Maximum = 360D;
        numericBoxDuration.MaximumSize = new System.Drawing.Size(1000, 30);
        numericBoxDuration.Minimum = 0.01D;
        numericBoxDuration.MinimumSize = new System.Drawing.Size(1, 20);
        numericBoxDuration.Name = "numericBoxDuration";
        numericBoxDuration.RadianValue = 0.20943951023931953D;
        numericBoxDuration.ShowUpDown = true;
        numericBoxDuration.Size = new System.Drawing.Size(156, 27);
        numericBoxDuration.SkipEventDuringInput = false;
        numericBoxDuration.SmartIncrement = true;
        numericBoxDuration.TabIndex = 1;
        numericBoxDuration.ValueFontSize = 9F;
        numericBoxDuration.TrimEndZero = true;
        numericBoxDuration.Value = 12D;
        // 
        // radioButtonAxis
        // 
        radioButtonAxis.AutoSize = true;
        radioButtonAxis.Location = new System.Drawing.Point(6, 136);
        radioButtonAxis.Name = "radioButtonAxis";
        radioButtonAxis.Size = new System.Drawing.Size(105, 19);
        radioButtonAxis.TabIndex = 86;
        radioButtonAxis.Text = "Direction index";
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
        radioButtonCurrent.UseVisualStyleBackColor = true;
        radioButtonCurrent.CheckedChanged += radioButtonCurrent_CheckedChanged;
        // 
        // groupBoxDirection
        // 
        groupBoxDirection.Controls.Add(indexControl);
        groupBoxDirection.Controls.Add(radioButtonCurrent);
        groupBoxDirection.Controls.Add(tableLayoutPanelCurrent);
        groupBoxDirection.Controls.Add(radioButtonAxis);
        groupBoxDirection.Controls.Add(radioButtonPlane);
        groupBoxDirection.Location = new System.Drawing.Point(9, 64);
        groupBoxDirection.Name = "groupBoxDirection";
        groupBoxDirection.Size = new System.Drawing.Size(164, 231);
        groupBoxDirection.TabIndex = 89;
        groupBoxDirection.TabStop = false;
        groupBoxDirection.Text = "Direction";
        // 
        // radioButtonH264
        // 
        radioButtonH264.AutoSize = true;
        radioButtonH264.Location = new System.Drawing.Point(8, 302);
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
        radioButtonH265.Location = new System.Drawing.Point(66, 303);
        radioButtonH265.Name = "radioButtonH265";
        radioButtonH265.Size = new System.Drawing.Size(52, 19);
        radioButtonH265.TabIndex = 90;
        radioButtonH265.TabStop = true;
        radioButtonH265.Text = "H265";
        radioButtonH265.UseVisualStyleBackColor = true;
        // 
        // comboBoxSpeed
        // 
        comboBoxSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBoxSpeed.FormattingEnabled = true;
        comboBoxSpeed.Items.AddRange(new object[] { "ultrafast", "superfast", "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow" });
        comboBoxSpeed.Location = new System.Drawing.Point(9, 350);
        comboBoxSpeed.Name = "comboBoxSpeed";
        comboBoxSpeed.Size = new System.Drawing.Size(75, 23);
        comboBoxSpeed.TabIndex = 91;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(9, 332);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(80, 15);
        label1.TabIndex = 92;
        label1.Text = "Encode speed";
        // 
        // indexControl1
        // 
        indexControl.AutoSize = true;
        indexControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        indexControl.BoxWidth = 40;
        indexControl.Bracket = IndexControl.BracketEnum.Round;
        indexControl.Location = new System.Drawing.Point(7, 183);
        indexControl.Margin = new System.Windows.Forms.Padding(0);
        indexControl.Mode = IndexControl.ModeEnum.Plane;
        indexControl.Name = "indexControl1";
        indexControl.Size = new System.Drawing.Size(134, 41);
        indexControl.SubScript = "";
        indexControl.TabIndex = 93;
        indexControl.UpDownWidth = 17;
        indexControl.Values = ((int, int, int))resources.GetObject("indexControl1.Values");
        indexControl.ValueChanged += numericBoxAxisU_ValueChanged; // 260517Cl 追加: IndexControl 化で取りこぼした購読を復元
        //
        // FormMovie
        // 
        AcceptButton = buttonOK;// 260520Cl 追加: Enter で OK (従来 Enter/Esc 無効だった)
        CancelButton = buttonCancel;// 260520Cl 追加: Esc で Cancel
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        AutoSize = true;
        AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        //ClientSize = new System.Drawing.Size(180, 378);// 260520Cl 変更前
        ClientSize = new System.Drawing.Size(180, 412);// 260520Cl 変更: OK/Cancel横並び行を下部に追加
        ControlBox = false;
        Controls.Add(label1);
        Controls.Add(comboBoxSpeed);
        Controls.Add(radioButtonH265);
        Controls.Add(radioButtonH264);
        Controls.Add(groupBoxDirection);
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
        groupBoxDirection.ResumeLayout(false);
        groupBoxDirection.PerformLayout();
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
    private System.Windows.Forms.RadioButton radioButtonCurrent;
    private System.Windows.Forms.GroupBox groupBoxDirection;
    private System.Windows.Forms.RadioButton radioButtonH264;
    private System.Windows.Forms.RadioButton radioButtonH265;
    private System.Windows.Forms.ComboBox comboBoxSpeed;
    private System.Windows.Forms.Label label1;
    private IndexControl indexControl;
}