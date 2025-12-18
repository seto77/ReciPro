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
            numericBoxPrimaryAxisDirection = new NumericBox();
            label2 = new System.Windows.Forms.Label();
            numericBox1 = new NumericBox();
            numericBox3 = new NumericBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            button2 = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            numericBox16 = new NumericBox();
            numericBox9 = new NumericBox();
            numericBox15 = new NumericBox();
            numericBox11 = new NumericBox();
            label13 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            numericBox14 = new NumericBox();
            numericBox10 = new NumericBox();
            numericBox13 = new NumericBox();
            numericBox8 = new NumericBox();
            numericBox12 = new NumericBox();
            numericBox7 = new NumericBox();
            radioButtonTiltY_Plus = new System.Windows.Forms.RadioButton();
            radioButtonTiltY_Minus = new System.Windows.Forms.RadioButton();
            groupBox2 = new System.Windows.Forms.GroupBox();
            buttonLink = new System.Windows.Forms.Button();
            groupBox5 = new System.Windows.Forms.GroupBox();
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
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            groupBox1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
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
            label1.Location = new System.Drawing.Point(8, 21);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(147, 34);
            label1.TabIndex = 82;
            label1.Text = "Direction of the primary\r\n rotation axis (Tilt-X)";
            // 
            // numericBoxPrimaryAxisDirection
            // 
            numericBoxPrimaryAxisDirection.BackColor = System.Drawing.Color.Transparent;
            numericBoxPrimaryAxisDirection.DecimalPlaces = 1;
            numericBoxPrimaryAxisDirection.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxPrimaryAxisDirection.FooterText = "°";
            numericBoxPrimaryAxisDirection.Location = new System.Drawing.Point(163, 28);
            numericBoxPrimaryAxisDirection.Margin = new System.Windows.Forms.Padding(0);
            numericBoxPrimaryAxisDirection.Maximum = 180D;
            numericBoxPrimaryAxisDirection.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxPrimaryAxisDirection.Minimum = -180D;
            numericBoxPrimaryAxisDirection.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxPrimaryAxisDirection.Name = "numericBoxPrimaryAxisDirection";
            numericBoxPrimaryAxisDirection.RadianValue = -0.78539816339744828D;
            numericBoxPrimaryAxisDirection.ShowUpDown = true;
            numericBoxPrimaryAxisDirection.Size = new System.Drawing.Size(69, 25);
            numericBoxPrimaryAxisDirection.TabIndex = 81;
            numericBoxPrimaryAxisDirection.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxPrimaryAxisDirection.Value = -45D;
            numericBoxPrimaryAxisDirection.ValueChanged += numericBoxPrimaryAxisDirection_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label2.Location = new System.Drawing.Point(6, 25);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(144, 17);
            label2.TabIndex = 82;
            label2.Text = "Primary rotation (Tilt-X)";
            // 
            // numericBox1
            // 
            numericBox1.BackColor = System.Drawing.Color.Transparent;
            numericBox1.DecimalPlaces = 1;
            numericBox1.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBox1.FooterText = "°";
            numericBox1.Location = new System.Drawing.Point(168, 21);
            numericBox1.Margin = new System.Windows.Forms.Padding(0);
            numericBox1.Maximum = 180D;
            numericBox1.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBox1.Minimum = -180D;
            numericBox1.MinimumSize = new System.Drawing.Size(1, 20);
            numericBox1.Name = "numericBox1";
            numericBox1.ShowUpDown = true;
            numericBox1.Size = new System.Drawing.Size(69, 25);
            numericBox1.TabIndex = 81;
            numericBox1.TextFont = new System.Drawing.Font("メイリオ", 9F);
            // 
            // numericBox3
            // 
            numericBox3.BackColor = System.Drawing.Color.Transparent;
            numericBox3.DecimalPlaces = 1;
            numericBox3.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBox3.FooterText = "°";
            numericBox3.Location = new System.Drawing.Point(168, 50);
            numericBox3.Margin = new System.Windows.Forms.Padding(0);
            numericBox3.Maximum = 180D;
            numericBox3.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBox3.Minimum = -180D;
            numericBox3.MinimumSize = new System.Drawing.Size(1, 20);
            numericBox3.Name = "numericBox3";
            numericBox3.ShowUpDown = true;
            numericBox3.Size = new System.Drawing.Size(69, 25);
            numericBox3.TabIndex = 81;
            numericBox3.TextFont = new System.Drawing.Font("メイリオ", 9F);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label3.Location = new System.Drawing.Point(6, 54);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(160, 17);
            label3.TabIndex = 82;
            label3.Text = "Secondary rotation (Tilt-Y)";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label4.Location = new System.Drawing.Point(8, 60);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(135, 34);
            label4.TabIndex = 82;
            label4.Text = "Polarity of the second\r\n rotation axis (Tilt-Y)";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox4);
            groupBox1.Controls.Add(radioButtonTiltY_Plus);
            groupBox1.Controls.Add(radioButtonTiltY_Minus);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(numericBoxPrimaryAxisDirection);
            groupBox1.Controls.Add(label4);
            groupBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            groupBox1.Location = new System.Drawing.Point(407, 136);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(247, 266);
            groupBox1.TabIndex = 83;
            groupBox1.TabStop = false;
            groupBox1.Text = "TEM-specific settings";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(button2);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(numericBox16);
            groupBox4.Controls.Add(numericBox9);
            groupBox4.Controls.Add(numericBox15);
            groupBox4.Controls.Add(numericBox11);
            groupBox4.Controls.Add(label13);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(label8);
            groupBox4.Controls.Add(label7);
            groupBox4.Controls.Add(numericBox14);
            groupBox4.Controls.Add(numericBox10);
            groupBox4.Controls.Add(numericBox13);
            groupBox4.Controls.Add(numericBox8);
            groupBox4.Controls.Add(numericBox12);
            groupBox4.Controls.Add(numericBox7);
            groupBox4.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            groupBox4.Location = new System.Drawing.Point(6, 102);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(235, 158);
            groupBox4.TabIndex = 84;
            groupBox4.TabStop = false;
            groupBox4.Text = "Calibration";
            // 
            // button2
            // 
            button2.AutoSize = true;
            button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button2.Location = new System.Drawing.Point(158, 127);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(70, 27);
            button2.TabIndex = 84;
            button2.Text = "Calibrate";
            button2.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label6.Location = new System.Drawing.Point(6, 20);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(194, 30);
            label6.TabIndex = 82;
            label6.Text = "Enter two sets of zone axis and\r\n holder angles, then press Calibrate.";
            // 
            // numericBox16
            // 
            numericBox16.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBox16.BackColor = System.Drawing.Color.Transparent;
            numericBox16.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBox16.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox16.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox16.Location = new System.Drawing.Point(109, 101);
            numericBox16.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox16.Maximum = 20D;
            numericBox16.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBox16.Minimum = -20D;
            numericBox16.MinimumSize = new System.Drawing.Size(1, 18);
            numericBox16.Name = "numericBox16";
            numericBox16.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox16.Size = new System.Drawing.Size(26, 25);
            numericBox16.SkipEventDuringInput = false;
            numericBox16.TabIndex = 81;
            numericBox16.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox16.ThonsandsSeparator = true;
            // 
            // numericBox9
            // 
            numericBox9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBox9.BackColor = System.Drawing.Color.Transparent;
            numericBox9.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBox9.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox9.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox9.Location = new System.Drawing.Point(109, 74);
            numericBox9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox9.Maximum = 20D;
            numericBox9.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBox9.Minimum = -20D;
            numericBox9.MinimumSize = new System.Drawing.Size(1, 18);
            numericBox9.Name = "numericBox9";
            numericBox9.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox9.Size = new System.Drawing.Size(26, 25);
            numericBox9.SkipEventDuringInput = false;
            numericBox9.TabIndex = 81;
            numericBox9.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox9.ThonsandsSeparator = true;
            // 
            // numericBox15
            // 
            numericBox15.BackColor = System.Drawing.Color.Transparent;
            numericBox15.DecimalPlaces = 1;
            numericBox15.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBox15.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox15.FooterText = "°";
            numericBox15.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox15.Location = new System.Drawing.Point(184, 101);
            numericBox15.Margin = new System.Windows.Forms.Padding(0);
            numericBox15.Maximum = 180D;
            numericBox15.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBox15.Minimum = -180D;
            numericBox15.MinimumSize = new System.Drawing.Size(1, 18);
            numericBox15.Name = "numericBox15";
            numericBox15.RestrictLimitValue = false;
            numericBox15.Size = new System.Drawing.Size(46, 25);
            numericBox15.TabIndex = 81;
            numericBox15.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            // 
            // numericBox11
            // 
            numericBox11.BackColor = System.Drawing.Color.Transparent;
            numericBox11.DecimalPlaces = 1;
            numericBox11.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBox11.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox11.FooterText = "°";
            numericBox11.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox11.Location = new System.Drawing.Point(184, 74);
            numericBox11.Margin = new System.Windows.Forms.Padding(0);
            numericBox11.Maximum = 180D;
            numericBox11.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBox11.Minimum = -180D;
            numericBox11.MinimumSize = new System.Drawing.Size(1, 18);
            numericBox11.Name = "numericBox11";
            numericBox11.RestrictLimitValue = false;
            numericBox11.Size = new System.Drawing.Size(46, 25);
            numericBox11.TabIndex = 81;
            numericBox11.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label13.Location = new System.Drawing.Point(35, 105);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(20, 15);
            label13.TabIndex = 82;
            label13.Text = "#2";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label12.Location = new System.Drawing.Point(35, 77);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(20, 15);
            label12.TabIndex = 82;
            label12.Text = "#1";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label11.Location = new System.Drawing.Point(181, 56);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(36, 15);
            label11.TabIndex = 82;
            label11.Text = "Tilt-Y";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label10.Location = new System.Drawing.Point(135, 56);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(36, 15);
            label10.TabIndex = 82;
            label10.Text = "Tilt-X";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            label9.Location = new System.Drawing.Point(109, 56);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(16, 15);
            label9.TabIndex = 82;
            label9.Text = "w";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            label8.Location = new System.Drawing.Point(84, 56);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(13, 15);
            label8.TabIndex = 82;
            label8.Text = "v";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            label7.Location = new System.Drawing.Point(58, 56);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(14, 15);
            label7.TabIndex = 82;
            label7.Text = "u";
            // 
            // numericBox14
            // 
            numericBox14.BackColor = System.Drawing.Color.Transparent;
            numericBox14.DecimalPlaces = 1;
            numericBox14.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBox14.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox14.FooterText = "°";
            numericBox14.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox14.Location = new System.Drawing.Point(136, 101);
            numericBox14.Margin = new System.Windows.Forms.Padding(0);
            numericBox14.Maximum = 180D;
            numericBox14.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBox14.Minimum = -180D;
            numericBox14.MinimumSize = new System.Drawing.Size(1, 18);
            numericBox14.Name = "numericBox14";
            numericBox14.RestrictLimitValue = false;
            numericBox14.Size = new System.Drawing.Size(46, 25);
            numericBox14.TabIndex = 81;
            numericBox14.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            // 
            // numericBox10
            // 
            numericBox10.BackColor = System.Drawing.Color.Transparent;
            numericBox10.DecimalPlaces = 1;
            numericBox10.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBox10.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox10.FooterText = "°";
            numericBox10.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox10.Location = new System.Drawing.Point(136, 74);
            numericBox10.Margin = new System.Windows.Forms.Padding(0);
            numericBox10.Maximum = 180D;
            numericBox10.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBox10.Minimum = -180D;
            numericBox10.MinimumSize = new System.Drawing.Size(1, 18);
            numericBox10.Name = "numericBox10";
            numericBox10.RestrictLimitValue = false;
            numericBox10.Size = new System.Drawing.Size(46, 25);
            numericBox10.TabIndex = 81;
            numericBox10.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            // 
            // numericBox13
            // 
            numericBox13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBox13.BackColor = System.Drawing.Color.Transparent;
            numericBox13.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBox13.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox13.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox13.Location = new System.Drawing.Point(83, 101);
            numericBox13.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox13.Maximum = 20D;
            numericBox13.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBox13.Minimum = -20D;
            numericBox13.MinimumSize = new System.Drawing.Size(1, 18);
            numericBox13.Name = "numericBox13";
            numericBox13.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox13.Size = new System.Drawing.Size(26, 25);
            numericBox13.SkipEventDuringInput = false;
            numericBox13.TabIndex = 82;
            numericBox13.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox13.ThonsandsSeparator = true;
            // 
            // numericBox8
            // 
            numericBox8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBox8.BackColor = System.Drawing.Color.Transparent;
            numericBox8.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBox8.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox8.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox8.Location = new System.Drawing.Point(83, 74);
            numericBox8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox8.Maximum = 20D;
            numericBox8.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBox8.Minimum = -20D;
            numericBox8.MinimumSize = new System.Drawing.Size(1, 18);
            numericBox8.Name = "numericBox8";
            numericBox8.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox8.Size = new System.Drawing.Size(26, 25);
            numericBox8.SkipEventDuringInput = false;
            numericBox8.TabIndex = 82;
            numericBox8.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox8.ThonsandsSeparator = true;
            // 
            // numericBox12
            // 
            numericBox12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBox12.BackColor = System.Drawing.Color.Transparent;
            numericBox12.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBox12.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox12.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox12.Location = new System.Drawing.Point(57, 101);
            numericBox12.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox12.Maximum = 20D;
            numericBox12.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBox12.Minimum = -20D;
            numericBox12.MinimumSize = new System.Drawing.Size(1, 18);
            numericBox12.Name = "numericBox12";
            numericBox12.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox12.Size = new System.Drawing.Size(26, 25);
            numericBox12.SkipEventDuringInput = false;
            numericBox12.TabIndex = 83;
            numericBox12.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox12.ThonsandsSeparator = true;
            // 
            // numericBox7
            // 
            numericBox7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBox7.BackColor = System.Drawing.Color.Transparent;
            numericBox7.Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            numericBox7.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox7.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox7.Location = new System.Drawing.Point(57, 74);
            numericBox7.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox7.Maximum = 20D;
            numericBox7.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBox7.Minimum = -20D;
            numericBox7.MinimumSize = new System.Drawing.Size(1, 18);
            numericBox7.Name = "numericBox7";
            numericBox7.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBox7.Size = new System.Drawing.Size(26, 25);
            numericBox7.SkipEventDuringInput = false;
            numericBox7.TabIndex = 83;
            numericBox7.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBox7.ThonsandsSeparator = true;
            // 
            // radioButtonTiltY_Plus
            // 
            radioButtonTiltY_Plus.AutoSize = true;
            radioButtonTiltY_Plus.Checked = true;
            radioButtonTiltY_Plus.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            radioButtonTiltY_Plus.Location = new System.Drawing.Point(163, 70);
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
            radioButtonTiltY_Minus.Location = new System.Drawing.Point(203, 70);
            radioButtonTiltY_Minus.Name = "radioButtonTiltY_Minus";
            radioButtonTiltY_Minus.Size = new System.Drawing.Size(32, 21);
            radioButtonTiltY_Minus.TabIndex = 83;
            radioButtonTiltY_Minus.Text = "-";
            radioButtonTiltY_Minus.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(buttonLink);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(numericBox1);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(numericBox3);
            groupBox2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            groupBox2.Location = new System.Drawing.Point(407, 2);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(247, 129);
            groupBox2.TabIndex = 84;
            groupBox2.TabStop = false;
            groupBox2.Text = "Holder condition";
            // 
            // buttonLink
            // 
            buttonLink.AutoSize = true;
            buttonLink.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonLink.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            buttonLink.Location = new System.Drawing.Point(48, 79);
            buttonLink.Name = "buttonLink";
            buttonLink.Size = new System.Drawing.Size(181, 44);
            buttonLink.TabIndex = 83;
            buttonLink.Text = "Link the above values to\r\n the current crystal direction";
            buttonLink.UseVisualStyleBackColor = true;
            buttonLink.Click += buttonLink_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(flowLayoutPanel1);
            groupBox5.Controls.Add(numericBoxDrawingArea);
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
            groupBox5.Size = new System.Drawing.Size(650, 94);
            groupBox5.TabIndex = 84;
            groupBox5.TabStop = false;
            groupBox5.Text = "Stereonet properties";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
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
            flowLayoutPanel1.Location = new System.Drawing.Point(7, 66);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(571, 20);
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
            colorControl90DegLine.Location = new System.Drawing.Point(160, 0);
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
            colorControl10DegLine.Location = new System.Drawing.Point(207, 0);
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
            colorControl1DegLine.Location = new System.Drawing.Point(254, 0);
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
            colorControlUniqueAxis.Location = new System.Drawing.Point(295, 0);
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
            colorControlGeneralAxis.Location = new System.Drawing.Point(356, 0);
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
            colorControlTiltX.Location = new System.Drawing.Point(453, 0);
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
            colorControlTiltY.Location = new System.Drawing.Point(512, 0);
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
            numericBoxDrawingArea.Location = new System.Drawing.Point(7, 28);
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
            // checkBoxTiltDirections
            // 
            checkBoxTiltDirections.AutoSize = true;
            checkBoxTiltDirections.Checked = true;
            checkBoxTiltDirections.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxTiltDirections.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            checkBoxTiltDirections.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxTiltDirections.Location = new System.Drawing.Point(403, 38);
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
            checkBox1DegLine.Location = new System.Drawing.Point(511, 17);
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
            checkBoxShowIndexLabels.Location = new System.Drawing.Point(403, 17);
            checkBoxShowIndexLabels.Name = "checkBoxShowIndexLabels";
            checkBoxShowIndexLabels.Size = new System.Drawing.Size(96, 21);
            checkBoxShowIndexLabels.TabIndex = 84;
            checkBoxShowIndexLabels.Text = "Index labels";
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
            // FormDiffractionSimulatorHolder
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(658, 500);
            Controls.Add(label1MousePosition);
            Controls.Add(groupBox5);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(graphicsBox);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            Name = "FormDiffractionSimulatorHolder";
            Text = "TEM Holder Simulation";
            Load += FormDiffractionSimulatorHolder_Load;
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public ImagingSolution.Control.GraphicsBox graphicsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonTiltY_Plus;
        private System.Windows.Forms.RadioButton radioButtonTiltY_Minus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonLink;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
     
        private NumericBox numericBoxPrimaryAxisDirection;
        private NumericBox numericBox1;
        private NumericBox numericBox3;
        private NumericBox numericBoxV;
        private NumericBox numericBoxW;
        private NumericBox numericBoxU;
        private NumericBox numericBox16;
        private NumericBox numericBox9;
        private NumericBox numericBox15;
        private NumericBox numericBox11;
        private NumericBox numericBox14;
        private NumericBox numericBox10;
        private NumericBox numericBox13;
        private NumericBox numericBox8;
        private NumericBox numericBox12;
        private NumericBox numericBox7;
        private NumericBox numericBoxDrawingArea;

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxShowIndexLabels;

        private System.Windows.Forms.Button button2;
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

        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBoxTiltDirections;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public ColorControl colorControlHolder;
        private System.Windows.Forms.Label label1MousePosition;
    }
}