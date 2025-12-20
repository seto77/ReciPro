namespace ReciPro
{
    partial class FormDiffractionSimulatorHolder
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
            components = new System.ComponentModel.Container();
            graphicsBox = new ImagingSolution.Control.GraphicsBox(components);
            label1 = new System.Windows.Forms.Label();
            numericBoxTiltXDirection = new NumericBox();
            numericBoxLinkTiltX = new NumericBox();
            numericBoxLinkTiltY = new NumericBox();
            label4 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            buttonCalibrate = new System.Windows.Forms.Button();
            numericBoxCalibW2 = new NumericBox();
            numericBoxCalibW1 = new NumericBox();
            numericBoxCalibTiltY2 = new NumericBox();
            numericBoxCalibTiltY1 = new NumericBox();
            label13 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            numericBoxCalibTiltX2 = new NumericBox();
            numericBoxCalibTiltX1 = new NumericBox();
            numericBoxCalibV2 = new NumericBox();
            numericBoxCalibV1 = new NumericBox();
            numericBoxCalibU2 = new NumericBox();
            numericBoxCalibU1 = new NumericBox();
            radioButtonTiltY_Plus = new System.Windows.Forms.RadioButton();
            radioButtonTiltY_Minus = new System.Windows.Forms.RadioButton();
            groupBox2 = new System.Windows.Forms.GroupBox();
            buttonRotate180 = new System.Windows.Forms.Button();
            buttonLink = new System.Windows.Forms.Button();
            groupBox5 = new System.Windows.Forms.GroupBox();
            label18 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            trackBarPointSize = new System.Windows.Forms.TrackBar();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            colorControlBackGround = new ColorControl();
            colorControlHolder = new ColorControl();
            colorControl90DegLine = new ColorControl();
            colorControl10DegLine = new ColorControl();
            colorControl1DegLine = new ColorControl();
            colorControlUniqueAxis = new ColorControl();
            colorControlGeneralAxis = new ColorControl();
            colorControlTiltX = new ColorControl();
            colorControlTiltY = new ColorControl();
            numericBoxDrawingArea = new NumericBox();
            trackBarStrSize = new System.Windows.Forms.TrackBar();
            checkBoxTiltDirections = new System.Windows.Forms.CheckBox();
            checkBox1DegLine = new System.Windows.Forms.CheckBox();
            checkBoxShowIndexLabels = new System.Windows.Forms.CheckBox();
            numericBoxW = new NumericBox();
            label16 = new System.Windows.Forms.Label();
            numericBoxV = new NumericBox();
            label15 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            numericBoxU = new NumericBox();
            label5 = new System.Windows.Forms.Label();
            label1MousePosition = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            numericBoxArrowStep = new NumericBox();
            label19 = new System.Windows.Forms.Label();
            numericBoxTiltX = new NumericBox();
            label20 = new System.Windows.Forms.Label();
            numericBoxTiltY = new NumericBox();
            checkBoxEnableArrow = new System.Windows.Forms.CheckBox();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // graphicsBox
            // 
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            graphicsBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            graphicsBox.Location = new System.Drawing.Point(2, 2);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.Size = new System.Drawing.Size(400, 400);
            graphicsBox.TabIndex = 80;
            graphicsBox.TabStop = false;
            graphicsBox.WaitOnLoad = true;
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label1.Location = new System.Drawing.Point(10, 25);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(131, 17);
            label1.TabIndex = 82;
            label1.Text = "Direction of the Tilt X";
            // 
            // numericBoxTiltXDirection
            // 
            numericBoxTiltXDirection.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltXDirection.DecimalPlaces = 1;
            numericBoxTiltXDirection.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxTiltXDirection.FooterText = "°";
            numericBoxTiltXDirection.Location = new System.Drawing.Point(150, 21);
            numericBoxTiltXDirection.Margin = new System.Windows.Forms.Padding(0);
            numericBoxTiltXDirection.Maximum = 180D;
            numericBoxTiltXDirection.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxTiltXDirection.Minimum = -180D;
            numericBoxTiltXDirection.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxTiltXDirection.Name = "numericBoxTiltXDirection";
            numericBoxTiltXDirection.RadianValue = -0.50614548307835561D;
            numericBoxTiltXDirection.ShowUpDown = true;
            numericBoxTiltXDirection.Size = new System.Drawing.Size(69, 25);
            numericBoxTiltXDirection.TabIndex = 81;
            numericBoxTiltXDirection.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxTiltXDirection.Value = -29D;
            numericBoxTiltXDirection.ValueChanged += numericBoxPrimaryAxisDirection_ValueChanged;
            // 
            // numericBoxLinkTiltX
            // 
            numericBoxLinkTiltX.BackColor = System.Drawing.Color.Transparent;
            numericBoxLinkTiltX.DecimalPlaces = 1;
            numericBoxLinkTiltX.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxLinkTiltX.FooterText = "°";
            numericBoxLinkTiltX.HeaderText = "Tilt X";
            numericBoxLinkTiltX.Location = new System.Drawing.Point(14, 25);
            numericBoxLinkTiltX.Margin = new System.Windows.Forms.Padding(0);
            numericBoxLinkTiltX.Maximum = 180D;
            numericBoxLinkTiltX.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxLinkTiltX.Minimum = -180D;
            numericBoxLinkTiltX.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxLinkTiltX.Name = "numericBoxLinkTiltX";
            numericBoxLinkTiltX.Size = new System.Drawing.Size(82, 25);
            numericBoxLinkTiltX.TabIndex = 81;
            numericBoxLinkTiltX.TextFont = new System.Drawing.Font("メイリオ", 9F);
            // 
            // numericBoxLinkTiltY
            // 
            numericBoxLinkTiltY.BackColor = System.Drawing.Color.Transparent;
            numericBoxLinkTiltY.DecimalPlaces = 1;
            numericBoxLinkTiltY.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxLinkTiltY.FooterText = "°";
            numericBoxLinkTiltY.HeaderText = "Tilt Y";
            numericBoxLinkTiltY.Location = new System.Drawing.Point(14, 52);
            numericBoxLinkTiltY.Margin = new System.Windows.Forms.Padding(0);
            numericBoxLinkTiltY.Maximum = 180D;
            numericBoxLinkTiltY.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxLinkTiltY.Minimum = -180D;
            numericBoxLinkTiltY.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxLinkTiltY.Name = "numericBoxLinkTiltY";
            numericBoxLinkTiltY.Size = new System.Drawing.Size(82, 25);
            numericBoxLinkTiltY.TabIndex = 81;
            numericBoxLinkTiltY.TextFont = new System.Drawing.Font("メイリオ", 9F);
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label4.Location = new System.Drawing.Point(10, 52);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(121, 17);
            label4.TabIndex = 82;
            label4.Text = "Polarity of the Tilt Y";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Controls.Add(radioButtonTiltY_Plus);
            groupBox1.Controls.Add(radioButtonTiltY_Minus);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(numericBoxTiltXDirection);
            groupBox1.Controls.Add(label4);
            groupBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(408, 201);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(235, 205);
            groupBox1.TabIndex = 83;
            groupBox1.TabStop = false;
            groupBox1.Text = "TEM-specific settings";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(buttonCalibrate);
            groupBox4.Controls.Add(numericBoxCalibW2);
            groupBox4.Controls.Add(numericBoxCalibW1);
            groupBox4.Controls.Add(numericBoxCalibTiltY2);
            groupBox4.Controls.Add(numericBoxCalibTiltY1);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(label2);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(numericBoxCalibTiltX2);
            groupBox4.Controls.Add(numericBoxCalibTiltX1);
            groupBox4.Controls.Add(numericBoxCalibV2);
            groupBox4.Controls.Add(numericBoxCalibV1);
            groupBox4.Controls.Add(numericBoxCalibU2);
            groupBox4.Controls.Add(numericBoxCalibU1);
            groupBox4.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            groupBox4.Location = new System.Drawing.Point(7, 76);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(220, 123);
            groupBox4.TabIndex = 84;
            groupBox4.TabStop = false;
            groupBox4.Text = "Calibration";
            // 
            // buttonCalibrate
            // 
            buttonCalibrate.AutoSize = true;
            buttonCalibrate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonCalibrate.Location = new System.Drawing.Point(145, 90);
            buttonCalibrate.Name = "buttonCalibrate";
            buttonCalibrate.Size = new System.Drawing.Size(70, 27);
            buttonCalibrate.TabIndex = 84;
            buttonCalibrate.Text = "Calibrate";
            buttonCalibrate.UseVisualStyleBackColor = true;
            buttonCalibrate.Click += buttonCalibrate_Click;
            // 
            // numericBoxCalibW2
            // 
            numericBoxCalibW2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxCalibW2.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalibW2.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBoxCalibW2.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibW2.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibW2.Location = new System.Drawing.Point(94, 63);
            numericBoxCalibW2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibW2.Maximum = 20D;
            numericBoxCalibW2.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxCalibW2.Minimum = -20D;
            numericBoxCalibW2.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxCalibW2.Name = "numericBoxCalibW2";
            numericBoxCalibW2.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibW2.Size = new System.Drawing.Size(26, 25);
            numericBoxCalibW2.SkipEventDuringInput = false;
            numericBoxCalibW2.TabIndex = 81;
            numericBoxCalibW2.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibW2.ThonsandsSeparator = true;
            // 
            // numericBoxCalibW1
            // 
            numericBoxCalibW1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxCalibW1.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalibW1.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBoxCalibW1.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibW1.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibW1.Location = new System.Drawing.Point(94, 36);
            numericBoxCalibW1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibW1.Maximum = 20D;
            numericBoxCalibW1.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxCalibW1.Minimum = -20D;
            numericBoxCalibW1.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxCalibW1.Name = "numericBoxCalibW1";
            numericBoxCalibW1.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibW1.Size = new System.Drawing.Size(26, 25);
            numericBoxCalibW1.SkipEventDuringInput = false;
            numericBoxCalibW1.TabIndex = 81;
            numericBoxCalibW1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibW1.ThonsandsSeparator = true;
            // 
            // numericBoxCalibTiltY2
            // 
            numericBoxCalibTiltY2.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalibTiltY2.DecimalPlaces = 1;
            numericBoxCalibTiltY2.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxCalibTiltY2.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibTiltY2.FooterText = "°";
            numericBoxCalibTiltY2.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibTiltY2.Location = new System.Drawing.Point(166, 63);
            numericBoxCalibTiltY2.Margin = new System.Windows.Forms.Padding(0);
            numericBoxCalibTiltY2.Maximum = 180D;
            numericBoxCalibTiltY2.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxCalibTiltY2.Minimum = -180D;
            numericBoxCalibTiltY2.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxCalibTiltY2.Name = "numericBoxCalibTiltY2";
            numericBoxCalibTiltY2.RestrictLimitValue = false;
            numericBoxCalibTiltY2.Size = new System.Drawing.Size(46, 25);
            numericBoxCalibTiltY2.TabIndex = 81;
            numericBoxCalibTiltY2.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            // 
            // numericBoxCalibTiltY1
            // 
            numericBoxCalibTiltY1.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalibTiltY1.DecimalPlaces = 1;
            numericBoxCalibTiltY1.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxCalibTiltY1.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibTiltY1.FooterText = "°";
            numericBoxCalibTiltY1.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibTiltY1.Location = new System.Drawing.Point(166, 36);
            numericBoxCalibTiltY1.Margin = new System.Windows.Forms.Padding(0);
            numericBoxCalibTiltY1.Maximum = 180D;
            numericBoxCalibTiltY1.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxCalibTiltY1.Minimum = -180D;
            numericBoxCalibTiltY1.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxCalibTiltY1.Name = "numericBoxCalibTiltY1";
            numericBoxCalibTiltY1.RestrictLimitValue = false;
            numericBoxCalibTiltY1.Size = new System.Drawing.Size(46, 25);
            numericBoxCalibTiltY1.TabIndex = 81;
            numericBoxCalibTiltY1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label13.Location = new System.Drawing.Point(22, 67);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(20, 15);
            label13.TabIndex = 82;
            label13.Text = "#2";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label12.Location = new System.Drawing.Point(22, 39);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(20, 15);
            label12.TabIndex = 82;
            label12.Text = "#1";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label11.Location = new System.Drawing.Point(163, 18);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(36, 15);
            label11.TabIndex = 82;
            label11.Text = "Tilt-Y";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Symbol", 7F);
            label2.Location = new System.Drawing.Point(5, 90);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(140, 24);
            label2.TabIndex = 82;
            label2.Text = "Enter 2 sets of zone axis and\r\n holder angles, then \"Calibrate\"";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label10.Location = new System.Drawing.Point(119, 18);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(36, 15);
            label10.TabIndex = 82;
            label10.Text = "Tilt-X";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            label9.Location = new System.Drawing.Point(94, 18);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(16, 15);
            label9.TabIndex = 82;
            label9.Text = "w";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            label8.Location = new System.Drawing.Point(70, 18);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(13, 15);
            label8.TabIndex = 82;
            label8.Text = "v";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            label7.Location = new System.Drawing.Point(45, 18);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(14, 15);
            label7.TabIndex = 82;
            label7.Text = "u";
            // 
            // numericBoxCalibTiltX2
            // 
            numericBoxCalibTiltX2.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalibTiltX2.DecimalPlaces = 1;
            numericBoxCalibTiltX2.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxCalibTiltX2.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibTiltX2.FooterText = "°";
            numericBoxCalibTiltX2.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibTiltX2.Location = new System.Drawing.Point(120, 63);
            numericBoxCalibTiltX2.Margin = new System.Windows.Forms.Padding(0);
            numericBoxCalibTiltX2.Maximum = 180D;
            numericBoxCalibTiltX2.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxCalibTiltX2.Minimum = -180D;
            numericBoxCalibTiltX2.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxCalibTiltX2.Name = "numericBoxCalibTiltX2";
            numericBoxCalibTiltX2.RestrictLimitValue = false;
            numericBoxCalibTiltX2.Size = new System.Drawing.Size(46, 25);
            numericBoxCalibTiltX2.TabIndex = 81;
            numericBoxCalibTiltX2.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            // 
            // numericBoxCalibTiltX1
            // 
            numericBoxCalibTiltX1.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalibTiltX1.DecimalPlaces = 1;
            numericBoxCalibTiltX1.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxCalibTiltX1.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibTiltX1.FooterText = "°";
            numericBoxCalibTiltX1.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibTiltX1.Location = new System.Drawing.Point(120, 36);
            numericBoxCalibTiltX1.Margin = new System.Windows.Forms.Padding(0);
            numericBoxCalibTiltX1.Maximum = 180D;
            numericBoxCalibTiltX1.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxCalibTiltX1.Minimum = -180D;
            numericBoxCalibTiltX1.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxCalibTiltX1.Name = "numericBoxCalibTiltX1";
            numericBoxCalibTiltX1.RestrictLimitValue = false;
            numericBoxCalibTiltX1.Size = new System.Drawing.Size(46, 25);
            numericBoxCalibTiltX1.TabIndex = 81;
            numericBoxCalibTiltX1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            // 
            // numericBoxCalibV2
            // 
            numericBoxCalibV2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxCalibV2.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalibV2.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBoxCalibV2.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibV2.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibV2.Location = new System.Drawing.Point(69, 63);
            numericBoxCalibV2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibV2.Maximum = 20D;
            numericBoxCalibV2.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxCalibV2.Minimum = -20D;
            numericBoxCalibV2.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxCalibV2.Name = "numericBoxCalibV2";
            numericBoxCalibV2.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibV2.Size = new System.Drawing.Size(26, 25);
            numericBoxCalibV2.SkipEventDuringInput = false;
            numericBoxCalibV2.TabIndex = 82;
            numericBoxCalibV2.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibV2.ThonsandsSeparator = true;
            // 
            // numericBoxCalibV1
            // 
            numericBoxCalibV1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxCalibV1.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalibV1.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBoxCalibV1.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibV1.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibV1.Location = new System.Drawing.Point(69, 36);
            numericBoxCalibV1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibV1.Maximum = 20D;
            numericBoxCalibV1.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxCalibV1.Minimum = -20D;
            numericBoxCalibV1.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxCalibV1.Name = "numericBoxCalibV1";
            numericBoxCalibV1.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibV1.Size = new System.Drawing.Size(26, 25);
            numericBoxCalibV1.SkipEventDuringInput = false;
            numericBoxCalibV1.TabIndex = 82;
            numericBoxCalibV1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibV1.ThonsandsSeparator = true;
            // 
            // numericBoxCalibU2
            // 
            numericBoxCalibU2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxCalibU2.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalibU2.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBoxCalibU2.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibU2.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibU2.Location = new System.Drawing.Point(44, 63);
            numericBoxCalibU2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibU2.Maximum = 20D;
            numericBoxCalibU2.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxCalibU2.Minimum = -20D;
            numericBoxCalibU2.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxCalibU2.Name = "numericBoxCalibU2";
            numericBoxCalibU2.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibU2.Size = new System.Drawing.Size(26, 25);
            numericBoxCalibU2.SkipEventDuringInput = false;
            numericBoxCalibU2.TabIndex = 83;
            numericBoxCalibU2.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibU2.ThonsandsSeparator = true;
            // 
            // numericBoxCalibU1
            // 
            numericBoxCalibU1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxCalibU1.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalibU1.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBoxCalibU1.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibU1.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibU1.Location = new System.Drawing.Point(44, 36);
            numericBoxCalibU1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibU1.Maximum = 20D;
            numericBoxCalibU1.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxCalibU1.Minimum = -20D;
            numericBoxCalibU1.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxCalibU1.Name = "numericBoxCalibU1";
            numericBoxCalibU1.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxCalibU1.Size = new System.Drawing.Size(26, 25);
            numericBoxCalibU1.SkipEventDuringInput = false;
            numericBoxCalibU1.TabIndex = 83;
            numericBoxCalibU1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxCalibU1.ThonsandsSeparator = true;
            // 
            // radioButtonTiltY_Plus
            // 
            radioButtonTiltY_Plus.AutoSize = true;
            radioButtonTiltY_Plus.Checked = true;
            radioButtonTiltY_Plus.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            radioButtonTiltY_Plus.Location = new System.Drawing.Point(150, 50);
            radioButtonTiltY_Plus.Name = "radioButtonTiltY_Plus";
            radioButtonTiltY_Plus.Size = new System.Drawing.Size(35, 21);
            radioButtonTiltY_Plus.TabIndex = 83;
            radioButtonTiltY_Plus.TabStop = true;
            radioButtonTiltY_Plus.Text = "+";
            radioButtonTiltY_Plus.UseVisualStyleBackColor = true;
            radioButtonTiltY_Plus.CheckedChanged += numericBoxPrimaryAxisDirection_ValueChanged;
            // 
            // radioButtonTiltY_Minus
            // 
            radioButtonTiltY_Minus.AutoSize = true;
            radioButtonTiltY_Minus.Location = new System.Drawing.Point(190, 50);
            radioButtonTiltY_Minus.Name = "radioButtonTiltY_Minus";
            radioButtonTiltY_Minus.Size = new System.Drawing.Size(32, 21);
            radioButtonTiltY_Minus.TabIndex = 83;
            radioButtonTiltY_Minus.Text = "-";
            radioButtonTiltY_Minus.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(buttonRotate180);
            groupBox2.Controls.Add(buttonLink);
            groupBox2.Controls.Add(numericBoxLinkTiltX);
            groupBox2.Controls.Add(numericBoxLinkTiltY);
            groupBox2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            groupBox2.Location = new System.Drawing.Point(408, 108);
            groupBox2.Margin = new System.Windows.Forms.Padding(0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(234, 90);
            groupBox2.TabIndex = 84;
            groupBox2.TabStop = false;
            groupBox2.Text = "Link";
            // 
            // buttonRotate180
            // 
            buttonRotate180.AutoSize = true;
            buttonRotate180.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonRotate180.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonRotate180.Location = new System.Drawing.Point(117, 14);
            buttonRotate180.Margin = new System.Windows.Forms.Padding(0);
            buttonRotate180.Name = "buttonRotate180";
            buttonRotate180.Size = new System.Drawing.Size(88, 25);
            buttonRotate180.TabIndex = 83;
            buttonRotate180.Text = "⭮ rotate 180°";
            buttonRotate180.UseVisualStyleBackColor = true;
            buttonRotate180.Click += buttonRotate180_Click;
            // 
            // buttonLink
            // 
            buttonLink.AutoSize = true;
            buttonLink.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonLink.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonLink.Location = new System.Drawing.Point(105, 43);
            buttonLink.Margin = new System.Windows.Forms.Padding(0);
            buttonLink.Name = "buttonLink";
            buttonLink.Size = new System.Drawing.Size(114, 40);
            buttonLink.TabIndex = 83;
            buttonLink.Text = "Link to the current\r\n crystal direction";
            buttonLink.UseVisualStyleBackColor = true;
            buttonLink.Click += buttonLink_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label18);
            groupBox5.Controls.Add(label17);
            groupBox5.Controls.Add(trackBarPointSize);
            groupBox5.Controls.Add(flowLayoutPanel1);
            groupBox5.Controls.Add(numericBoxDrawingArea);
            groupBox5.Controls.Add(trackBarStrSize);
            groupBox5.Controls.Add(checkBoxTiltDirections);
            groupBox5.Controls.Add(checkBox1DegLine);
            groupBox5.Controls.Add(checkBoxShowIndexLabels);
            groupBox5.Controls.Add(numericBoxW);
            groupBox5.Controls.Add(label16);
            groupBox5.Controls.Add(numericBoxV);
            groupBox5.Controls.Add(label15);
            groupBox5.Controls.Add(label14);
            groupBox5.Controls.Add(numericBoxU);
            groupBox5.Controls.Add(label5);
            groupBox5.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            groupBox5.Location = new System.Drawing.Point(4, 404);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(638, 104);
            groupBox5.TabIndex = 84;
            groupBox5.TabStop = false;
            groupBox5.Text = "Stereonet properties";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label18.Location = new System.Drawing.Point(153, 81);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(63, 17);
            label18.TabIndex = 69;
            label18.Text = "Point size";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label17.Location = new System.Drawing.Point(1, 81);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(68, 17);
            label17.TabIndex = 68;
            label17.Text = "String size";
            // 
            // trackBarPointSize
            // 
            trackBarPointSize.AutoSize = false;
            trackBarPointSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            trackBarPointSize.Location = new System.Drawing.Point(215, 82);
            trackBarPointSize.Maximum = 20;
            trackBarPointSize.Minimum = 1;
            trackBarPointSize.Name = "trackBarPointSize";
            trackBarPointSize.Size = new System.Drawing.Size(80, 16);
            trackBarPointSize.TabIndex = 65;
            trackBarPointSize.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarPointSize.Value = 5;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(colorControlBackGround);
            flowLayoutPanel1.Controls.Add(colorControlHolder);
            flowLayoutPanel1.Controls.Add(colorControl90DegLine);
            flowLayoutPanel1.Controls.Add(colorControl10DegLine);
            flowLayoutPanel1.Controls.Add(colorControl1DegLine);
            flowLayoutPanel1.Controls.Add(colorControlUniqueAxis);
            flowLayoutPanel1.Controls.Add(colorControlGeneralAxis);
            flowLayoutPanel1.Controls.Add(colorControlTiltX);
            flowLayoutPanel1.Controls.Add(colorControlTiltY);
            flowLayoutPanel1.Location = new System.Drawing.Point(472, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(162, 86);
            flowLayoutPanel1.TabIndex = 93;
            // 
            // colorControlBackGround
            // 
            colorControlBackGround.Argb = -1;
            colorControlBackGround.AutoSize = true;
            colorControlBackGround.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControlBackGround.BackColor = System.Drawing.Color.White;
            colorControlBackGround.Blue = 255;
            colorControlBackGround.BlueF = 1F;
            colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlBackGround.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            colorControlBackGround.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlBackGround.FooterMargin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            colorControlBackGround.FooterText = "Background";
            colorControlBackGround.Green = 255;
            colorControlBackGround.GreenF = 1F;
            colorControlBackGround.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlBackGround.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlBackGround.Location = new System.Drawing.Point(0, 0);
            colorControlBackGround.Margin = new System.Windows.Forms.Padding(0);
            colorControlBackGround.Name = "colorControlBackGround";
            colorControlBackGround.Red = 255;
            colorControlBackGround.RedF = 1F;
            colorControlBackGround.Size = new System.Drawing.Size(94, 20);
            colorControlBackGround.TabIndex = 88;
            colorControlBackGround.TabStop = false;
            colorControlBackGround.ToolTip = "背景色";
            colorControlBackGround.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlHolder
            // 
            colorControlHolder.Argb = -32768;
            colorControlHolder.AutoSize = true;
            colorControlHolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControlHolder.BackColor = System.Drawing.Color.White;
            colorControlHolder.Blue = 0;
            colorControlHolder.BlueF = 0F;
            colorControlHolder.BoxSize = new System.Drawing.Size(20, 20);
            colorControlHolder.Color = System.Drawing.Color.FromArgb(255, 128, 0);
            colorControlHolder.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            colorControlHolder.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlHolder.FooterMargin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            colorControlHolder.FooterText = "Holder";
            colorControlHolder.Green = 128;
            colorControlHolder.GreenF = 0.5019608F;
            colorControlHolder.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlHolder.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlHolder.Location = new System.Drawing.Point(94, 0);
            colorControlHolder.Margin = new System.Windows.Forms.Padding(0);
            colorControlHolder.Name = "colorControlHolder";
            colorControlHolder.Red = 255;
            colorControlHolder.RedF = 1F;
            colorControlHolder.Size = new System.Drawing.Size(66, 20);
            colorControlHolder.TabIndex = 88;
            colorControlHolder.TabStop = false;
            colorControlHolder.ToolTip = "背景色";
            colorControlHolder.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl90DegLine
            // 
            colorControl90DegLine.Argb = -16776961;
            colorControl90DegLine.AutoSize = true;
            colorControl90DegLine.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControl90DegLine.BackColor = System.Drawing.Color.Blue;
            colorControl90DegLine.Blue = 255;
            colorControl90DegLine.BlueF = 1F;
            colorControl90DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl90DegLine.Color = System.Drawing.Color.FromArgb(0, 0, 255);
            colorControl90DegLine.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            colorControl90DegLine.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControl90DegLine.FooterMargin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            colorControl90DegLine.FooterText = "90°";
            colorControl90DegLine.Green = 0;
            colorControl90DegLine.GreenF = 0F;
            colorControl90DegLine.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControl90DegLine.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControl90DegLine.Location = new System.Drawing.Point(0, 20);
            colorControl90DegLine.Margin = new System.Windows.Forms.Padding(0);
            colorControl90DegLine.Name = "colorControl90DegLine";
            colorControl90DegLine.Red = 0;
            colorControl90DegLine.RedF = 0F;
            colorControl90DegLine.Size = new System.Drawing.Size(47, 20);
            colorControl90DegLine.TabIndex = 91;
            colorControl90DegLine.TabStop = false;
            colorControl90DegLine.ToolTip = "90度線の色";
            colorControl90DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl10DegLine
            // 
            colorControl10DegLine.Argb = -8355585;
            colorControl10DegLine.AutoSize = true;
            colorControl10DegLine.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControl10DegLine.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
            colorControl10DegLine.Blue = 255;
            colorControl10DegLine.BlueF = 1F;
            colorControl10DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl10DegLine.Color = System.Drawing.Color.FromArgb(128, 128, 255);
            colorControl10DegLine.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            colorControl10DegLine.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControl10DegLine.FooterMargin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            colorControl10DegLine.FooterText = "10°";
            colorControl10DegLine.Green = 128;
            colorControl10DegLine.GreenF = 0.5019608F;
            colorControl10DegLine.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControl10DegLine.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControl10DegLine.Location = new System.Drawing.Point(47, 20);
            colorControl10DegLine.Margin = new System.Windows.Forms.Padding(0);
            colorControl10DegLine.Name = "colorControl10DegLine";
            colorControl10DegLine.Red = 128;
            colorControl10DegLine.RedF = 0.5019608F;
            colorControl10DegLine.Size = new System.Drawing.Size(47, 20);
            colorControl10DegLine.TabIndex = 89;
            colorControl10DegLine.TabStop = false;
            colorControl10DegLine.ToolTip = "10度線の色";
            colorControl10DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl1DegLine
            // 
            colorControl1DegLine.Argb = -4144897;
            colorControl1DegLine.AutoSize = true;
            colorControl1DegLine.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControl1DegLine.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControl1DegLine.Blue = 255;
            colorControl1DegLine.BlueF = 1F;
            colorControl1DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl1DegLine.Color = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControl1DegLine.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            colorControl1DegLine.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControl1DegLine.FooterMargin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            colorControl1DegLine.FooterText = "1°";
            colorControl1DegLine.Green = 192;
            colorControl1DegLine.GreenF = 0.7529412F;
            colorControl1DegLine.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControl1DegLine.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControl1DegLine.Location = new System.Drawing.Point(94, 20);
            colorControl1DegLine.Margin = new System.Windows.Forms.Padding(0);
            colorControl1DegLine.Name = "colorControl1DegLine";
            colorControl1DegLine.Red = 192;
            colorControl1DegLine.RedF = 0.7529412F;
            colorControl1DegLine.Size = new System.Drawing.Size(41, 20);
            colorControl1DegLine.TabIndex = 90;
            colorControl1DegLine.TabStop = false;
            colorControl1DegLine.ToolTip = "1度線の色\r\n「Show 1°line」がチェックされているとき有効";
            colorControl1DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlUniqueAxis
            // 
            colorControlUniqueAxis.Argb = -7667712;
            colorControlUniqueAxis.AutoSize = true;
            colorControlUniqueAxis.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControlUniqueAxis.BackColor = System.Drawing.Color.Red;
            colorControlUniqueAxis.Blue = 0;
            colorControlUniqueAxis.BlueF = 0F;
            colorControlUniqueAxis.BoxSize = new System.Drawing.Size(20, 20);
            colorControlUniqueAxis.Color = System.Drawing.Color.FromArgb(139, 0, 0);
            colorControlUniqueAxis.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            colorControlUniqueAxis.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlUniqueAxis.FooterMargin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            colorControlUniqueAxis.FooterText = "a, b, c";
            colorControlUniqueAxis.Green = 0;
            colorControlUniqueAxis.GreenF = 0F;
            colorControlUniqueAxis.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlUniqueAxis.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlUniqueAxis.Location = new System.Drawing.Point(0, 40);
            colorControlUniqueAxis.Margin = new System.Windows.Forms.Padding(0);
            colorControlUniqueAxis.Name = "colorControlUniqueAxis";
            colorControlUniqueAxis.Red = 139;
            colorControlUniqueAxis.RedF = 0.545098066F;
            colorControlUniqueAxis.Size = new System.Drawing.Size(61, 20);
            colorControlUniqueAxis.TabIndex = 86;
            colorControlUniqueAxis.TabStop = false;
            colorControlUniqueAxis.ToolTip = "軸表示時の[100], [010], [001]の表示色";
            colorControlUniqueAxis.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlGeneralAxis
            // 
            colorControlGeneralAxis.Argb = -65536;
            colorControlGeneralAxis.AutoSize = true;
            colorControlGeneralAxis.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControlGeneralAxis.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
            colorControlGeneralAxis.Blue = 0;
            colorControlGeneralAxis.BlueF = 0F;
            colorControlGeneralAxis.BoxSize = new System.Drawing.Size(20, 20);
            colorControlGeneralAxis.Color = System.Drawing.Color.FromArgb(255, 0, 0);
            colorControlGeneralAxis.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            colorControlGeneralAxis.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlGeneralAxis.FooterMargin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            colorControlGeneralAxis.FooterText = "General Axes";
            colorControlGeneralAxis.Green = 0;
            colorControlGeneralAxis.GreenF = 0F;
            colorControlGeneralAxis.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlGeneralAxis.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlGeneralAxis.Location = new System.Drawing.Point(61, 40);
            colorControlGeneralAxis.Margin = new System.Windows.Forms.Padding(0);
            colorControlGeneralAxis.Name = "colorControlGeneralAxis";
            colorControlGeneralAxis.Red = 255;
            colorControlGeneralAxis.RedF = 1F;
            colorControlGeneralAxis.Size = new System.Drawing.Size(97, 20);
            colorControlGeneralAxis.TabIndex = 87;
            colorControlGeneralAxis.TabStop = false;
            colorControlGeneralAxis.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlTiltX
            // 
            colorControlTiltX.Argb = -16726016;
            colorControlTiltX.AutoSize = true;
            colorControlTiltX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControlTiltX.BackColor = System.Drawing.Color.Lime;
            colorControlTiltX.Blue = 0;
            colorControlTiltX.BlueF = 0F;
            colorControlTiltX.BoxSize = new System.Drawing.Size(20, 20);
            colorControlTiltX.Color = System.Drawing.Color.FromArgb(0, 200, 0);
            colorControlTiltX.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            colorControlTiltX.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlTiltX.FooterMargin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            colorControlTiltX.FooterText = "Tilt-X";
            colorControlTiltX.Green = 200;
            colorControlTiltX.GreenF = 0.784313738F;
            colorControlTiltX.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlTiltX.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlTiltX.Location = new System.Drawing.Point(0, 60);
            colorControlTiltX.Margin = new System.Windows.Forms.Padding(0);
            colorControlTiltX.Name = "colorControlTiltX";
            colorControlTiltX.Red = 0;
            colorControlTiltX.RedF = 0F;
            colorControlTiltX.Size = new System.Drawing.Size(59, 20);
            colorControlTiltX.TabIndex = 92;
            colorControlTiltX.TabStop = false;
            // 
            // colorControlTiltY
            // 
            colorControlTiltY.Argb = -65281;
            colorControlTiltY.AutoSize = true;
            colorControlTiltY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControlTiltY.BackColor = System.Drawing.Color.Lime;
            colorControlTiltY.Blue = 255;
            colorControlTiltY.BlueF = 1F;
            colorControlTiltY.BoxSize = new System.Drawing.Size(20, 20);
            colorControlTiltY.Color = System.Drawing.Color.FromArgb(255, 0, 255);
            colorControlTiltY.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            colorControlTiltY.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlTiltY.FooterMargin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            colorControlTiltY.FooterText = "Tilt-Y";
            colorControlTiltY.Green = 0;
            colorControlTiltY.GreenF = 0F;
            colorControlTiltY.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlTiltY.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlTiltY.Location = new System.Drawing.Point(59, 60);
            colorControlTiltY.Margin = new System.Windows.Forms.Padding(0);
            colorControlTiltY.Name = "colorControlTiltY";
            colorControlTiltY.Red = 255;
            colorControlTiltY.RedF = 1F;
            colorControlTiltY.Size = new System.Drawing.Size(59, 20);
            colorControlTiltY.TabIndex = 92;
            colorControlTiltY.TabStop = false;
            // 
            // numericBoxDrawingArea
            // 
            numericBoxDrawingArea.BackColor = System.Drawing.Color.Transparent;
            numericBoxDrawingArea.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDrawingArea.FooterText = "°";
            numericBoxDrawingArea.HeaderText = "Drawing area";
            numericBoxDrawingArea.Location = new System.Drawing.Point(7, 27);
            numericBoxDrawingArea.Margin = new System.Windows.Forms.Padding(0);
            numericBoxDrawingArea.Maximum = 90D;
            numericBoxDrawingArea.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxDrawingArea.Minimum = 1D;
            numericBoxDrawingArea.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxDrawingArea.Name = "numericBoxDrawingArea";
            numericBoxDrawingArea.RadianValue = 0.52359877559829882D;
            numericBoxDrawingArea.ShowUpDown = true;
            numericBoxDrawingArea.Size = new System.Drawing.Size(141, 25);
            numericBoxDrawingArea.TabIndex = 81;
            numericBoxDrawingArea.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDrawingArea.Value = 30D;
            numericBoxDrawingArea.ValueChanged += numericBoxDrawingArea_ValueChanged;
            // 
            // trackBarStrSize
            // 
            trackBarStrSize.AutoSize = false;
            trackBarStrSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            trackBarStrSize.Location = new System.Drawing.Point(66, 82);
            trackBarStrSize.Maximum = 200;
            trackBarStrSize.Minimum = 1;
            trackBarStrSize.Name = "trackBarStrSize";
            trackBarStrSize.Size = new System.Drawing.Size(80, 16);
            trackBarStrSize.TabIndex = 66;
            trackBarStrSize.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarStrSize.Value = 80;
            // 
            // checkBoxTiltDirections
            // 
            checkBoxTiltDirections.AutoSize = true;
            checkBoxTiltDirections.Checked = true;
            checkBoxTiltDirections.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxTiltDirections.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            checkBoxTiltDirections.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxTiltDirections.Location = new System.Drawing.Point(237, 59);
            checkBoxTiltDirections.Margin = new System.Windows.Forms.Padding(0);
            checkBoxTiltDirections.Name = "checkBoxTiltDirections";
            checkBoxTiltDirections.Size = new System.Drawing.Size(137, 21);
            checkBoxTiltDirections.TabIndex = 85;
            checkBoxTiltDirections.Text = "Show tilt directions";
            checkBoxTiltDirections.CheckedChanged += checkBox1DegLine_CheckedChanged;
            // 
            // checkBox1DegLine
            // 
            checkBox1DegLine.AutoSize = true;
            checkBox1DegLine.Checked = true;
            checkBox1DegLine.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox1DegLine.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            checkBox1DegLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBox1DegLine.Location = new System.Drawing.Point(7, 59);
            checkBox1DegLine.Margin = new System.Windows.Forms.Padding(0);
            checkBox1DegLine.Name = "checkBox1DegLine";
            checkBox1DegLine.Size = new System.Drawing.Size(98, 21);
            checkBox1DegLine.TabIndex = 85;
            checkBox1DegLine.Text = "Show 1° line";
            checkBox1DegLine.CheckedChanged += checkBox1DegLine_CheckedChanged;
            // 
            // checkBoxShowIndexLabels
            // 
            checkBoxShowIndexLabels.AutoSize = true;
            checkBoxShowIndexLabels.Checked = true;
            checkBoxShowIndexLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowIndexLabels.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            checkBoxShowIndexLabels.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxShowIndexLabels.Location = new System.Drawing.Point(108, 59);
            checkBoxShowIndexLabels.Name = "checkBoxShowIndexLabels";
            checkBoxShowIndexLabels.Size = new System.Drawing.Size(131, 21);
            checkBoxShowIndexLabels.TabIndex = 84;
            checkBoxShowIndexLabels.Text = "Show Index labels";
            checkBoxShowIndexLabels.UseVisualStyleBackColor = true;
            // 
            // numericBoxW
            // 
            numericBoxW.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxW.BackColor = System.Drawing.Color.Transparent;
            numericBoxW.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBoxW.HeaderText = "±";
            numericBoxW.Location = new System.Drawing.Point(339, 27);
            numericBoxW.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxW.Maximum = 20D;
            numericBoxW.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxW.Minimum = 0D;
            numericBoxW.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxW.Name = "numericBoxW";
            numericBoxW.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxW.RadianValue = 0.034906585039886591D;
            numericBoxW.ShowUpDown = true;
            numericBoxW.Size = new System.Drawing.Size(54, 25);
            numericBoxW.SkipEventDuringInput = false;
            numericBoxW.TabIndex = 81;
            numericBoxW.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxW.ThonsandsSeparator = true;
            numericBoxW.Value = 2D;
            numericBoxW.ReadOnlyChanged += numericBoxU_ValueChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            label16.Location = new System.Drawing.Point(358, 12);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(16, 15);
            label16.TabIndex = 82;
            label16.Text = "w";
            // 
            // numericBoxV
            // 
            numericBoxV.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxV.BackColor = System.Drawing.Color.Transparent;
            numericBoxV.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBoxV.HeaderText = "±";
            numericBoxV.Location = new System.Drawing.Point(283, 27);
            numericBoxV.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxV.Maximum = 20D;
            numericBoxV.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxV.Minimum = 0D;
            numericBoxV.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxV.Name = "numericBoxV";
            numericBoxV.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxV.RadianValue = 0.034906585039886591D;
            numericBoxV.ShowUpDown = true;
            numericBoxV.Size = new System.Drawing.Size(54, 25);
            numericBoxV.SkipEventDuringInput = false;
            numericBoxV.TabIndex = 82;
            numericBoxV.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxV.ThonsandsSeparator = true;
            numericBoxV.Value = 2D;
            numericBoxV.ValueChanged += numericBoxU_ValueChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            label15.Location = new System.Drawing.Point(302, 12);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(13, 15);
            label15.TabIndex = 82;
            label15.Text = "v";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            label14.Location = new System.Drawing.Point(246, 12);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(14, 15);
            label14.TabIndex = 82;
            label14.Text = "u";
            // 
            // numericBoxU
            // 
            numericBoxU.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxU.BackColor = System.Drawing.Color.Transparent;
            numericBoxU.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBoxU.HeaderText = "±";
            numericBoxU.Location = new System.Drawing.Point(227, 27);
            numericBoxU.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxU.Maximum = 20D;
            numericBoxU.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxU.Minimum = 0D;
            numericBoxU.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxU.Name = "numericBoxU";
            numericBoxU.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxU.RadianValue = 0.034906585039886591D;
            numericBoxU.ShowUpDown = true;
            numericBoxU.Size = new System.Drawing.Size(54, 25);
            numericBoxU.SkipEventDuringInput = false;
            numericBoxU.TabIndex = 83;
            numericBoxU.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxU.ThonsandsSeparator = true;
            numericBoxU.Value = 2D;
            numericBoxU.ValueChanged += numericBoxU_ValueChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label5.Location = new System.Drawing.Point(151, 29);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(77, 17);
            label5.TabIndex = 82;
            label5.Text = "Index range";
            // 
            // label1MousePosition
            // 
            label1MousePosition.AutoSize = true;
            label1MousePosition.BackColor = System.Drawing.Color.White;
            label1MousePosition.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label1MousePosition.Location = new System.Drawing.Point(4, 4);
            label1MousePosition.Margin = new System.Windows.Forms.Padding(0);
            label1MousePosition.Name = "label1MousePosition";
            label1MousePosition.Size = new System.Drawing.Size(147, 15);
            label1MousePosition.TabIndex = 82;
            label1MousePosition.Text = "Tilt-X: ##.#°    Tilt-Y:##.# °";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(numericBoxArrowStep);
            groupBox3.Controls.Add(label19);
            groupBox3.Controls.Add(numericBoxTiltX);
            groupBox3.Controls.Add(label20);
            groupBox3.Controls.Add(numericBoxTiltY);
            groupBox3.Controls.Add(checkBoxEnableArrow);
            groupBox3.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            groupBox3.Location = new System.Drawing.Point(407, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(235, 105);
            groupBox3.TabIndex = 84;
            groupBox3.TabStop = false;
            groupBox3.Text = "Holder angles";
            // 
            // numericBoxArrowStep
            // 
            numericBoxArrowStep.BackColor = System.Drawing.Color.Transparent;
            numericBoxArrowStep.DecimalPlaces = 1;
            numericBoxArrowStep.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxArrowStep.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxArrowStep.FooterPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            numericBoxArrowStep.FooterText = "°)";
            numericBoxArrowStep.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxArrowStep.HeaderPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            numericBoxArrowStep.HeaderText = "(step ";
            numericBoxArrowStep.Location = new System.Drawing.Point(136, 75);
            numericBoxArrowStep.Margin = new System.Windows.Forms.Padding(0);
            numericBoxArrowStep.Maximum = 2D;
            numericBoxArrowStep.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxArrowStep.Minimum = 0.1D;
            numericBoxArrowStep.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxArrowStep.Name = "numericBoxArrowStep";
            numericBoxArrowStep.RadianValue = 0.0034906585039886592D;
            numericBoxArrowStep.ShowUpDown = true;
            numericBoxArrowStep.Size = new System.Drawing.Size(95, 23);
            numericBoxArrowStep.TabIndex = 81;
            numericBoxArrowStep.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxArrowStep.UpDown_Increment = 0.1D;
            numericBoxArrowStep.Value = 0.2D;
            numericBoxArrowStep.ValueChanged += numericBoxTilt_ValueChanged;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label19.Location = new System.Drawing.Point(6, 23);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(143, 17);
            label19.TabIndex = 82;
            label19.Text = "Tilt X (Primary rotation)";
            // 
            // numericBoxTiltX
            // 
            numericBoxTiltX.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltX.DecimalPlaces = 1;
            numericBoxTiltX.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxTiltX.FooterText = "°";
            numericBoxTiltX.Location = new System.Drawing.Point(166, 19);
            numericBoxTiltX.Margin = new System.Windows.Forms.Padding(0);
            numericBoxTiltX.Maximum = 180D;
            numericBoxTiltX.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxTiltX.Minimum = -180D;
            numericBoxTiltX.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxTiltX.Name = "numericBoxTiltX";
            numericBoxTiltX.ShowUpDown = true;
            numericBoxTiltX.Size = new System.Drawing.Size(69, 25);
            numericBoxTiltX.TabIndex = 81;
            numericBoxTiltX.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxTiltX.ValueChanged += numericBoxTilt_ValueChanged;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label20.Location = new System.Drawing.Point(6, 49);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(159, 17);
            label20.TabIndex = 82;
            label20.Text = "Tilt Y (Secondary rotation)";
            // 
            // numericBoxTiltY
            // 
            numericBoxTiltY.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltY.DecimalPlaces = 1;
            numericBoxTiltY.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxTiltY.FooterText = "°";
            numericBoxTiltY.Location = new System.Drawing.Point(166, 47);
            numericBoxTiltY.Margin = new System.Windows.Forms.Padding(0);
            numericBoxTiltY.Maximum = 180D;
            numericBoxTiltY.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxTiltY.Minimum = -180D;
            numericBoxTiltY.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxTiltY.Name = "numericBoxTiltY";
            numericBoxTiltY.ShowUpDown = true;
            numericBoxTiltY.Size = new System.Drawing.Size(69, 25);
            numericBoxTiltY.TabIndex = 81;
            numericBoxTiltY.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxTiltY.ValueChanged += numericBoxTilt_ValueChanged;
            // 
            // checkBoxEnableArrow
            // 
            checkBoxEnableArrow.AutoSize = true;
            checkBoxEnableArrow.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            checkBoxEnableArrow.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxEnableArrow.Location = new System.Drawing.Point(21, 77);
            checkBoxEnableArrow.Name = "checkBoxEnableArrow";
            checkBoxEnableArrow.Size = new System.Drawing.Size(120, 19);
            checkBoxEnableArrow.TabIndex = 84;
            checkBoxEnableArrow.Text = "Enable arrow keys";
            checkBoxEnableArrow.UseVisualStyleBackColor = true;
            checkBoxEnableArrow.CheckedChanged += checkBoxEnableArrow_CheckedChanged;
            // 
            // FormDiffractionSimulatorHolder
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(647, 513);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1MousePosition);
            Controls.Add(groupBox5);
            Controls.Add(graphicsBox);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            KeyPreview = true;
            Name = "FormDiffractionSimulatorHolder";
            Text = "TEM Holder Simulation";
            FormClosing += FormDiffractionSimulatorHolder_FormClosing;
            Load += FormDiffractionSimulatorHolder_Load;
            KeyDown += FormDiffractionSimulatorHolder_KeyDown;
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public ImagingSolution.Control.GraphicsBox graphicsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonTiltY_Plus;
        private System.Windows.Forms.RadioButton radioButtonTiltY_Minus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonLink;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
     
   
        private NumericBox numericBoxCalibTiltY2;
        private NumericBox numericBoxCalibTiltX2;
        private NumericBox numericBoxCalibTiltY1;
        private NumericBox numericBoxCalibW2;
        private NumericBox numericBoxCalibW1;
        private NumericBox numericBoxCalibTiltX1;
        private NumericBox numericBoxCalibV2;
        private NumericBox numericBoxCalibV1;
        private NumericBox numericBoxCalibU2;
        private NumericBox numericBoxCalibU1;
        private NumericBox numericBoxLinkTiltX;
        private NumericBox numericBoxLinkTiltY;
        private NumericBox numericBoxV;
        private NumericBox numericBoxW;
        private NumericBox numericBoxU;
        private NumericBox numericBoxDrawingArea;
        private NumericBox numericBoxTiltXDirection;
        private NumericBox numericBoxTiltX;
        private NumericBox numericBoxTiltY;

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxShowIndexLabels;

        private System.Windows.Forms.Button buttonCalibrate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox1DegLine;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;

        public ColorControl colorControlUniqueAxis;
        public ColorControl colorControlGeneralAxis;
        public ColorControl colorControlBackGround;
        public ColorControl colorControl10DegLine;
        public ColorControl colorControl1DegLine;
        public ColorControl colorControl90DegLine;
        public ColorControl colorControlTiltX;
        public ColorControl colorControlTiltY;
        public ColorControl colorControlHolder;


        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBoxTiltDirections;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1MousePosition;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TrackBar trackBarPointSize;
        private System.Windows.Forms.TrackBar trackBarStrSize;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonRotate180;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxEnableArrow;
        private NumericBox numericBoxArrowStep;
    }
}