namespace ReciPro
{
    partial class FormEBSD
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
            panelGeometry = new System.Windows.Forms.Panel();
            numericBoxSampleTilt = new NumericBox();
            waveLengthControl = new WaveLengthControl();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonFromX = new System.Windows.Forms.Button();
            buttonViewFromZ = new System.Windows.Forms.Button();
            buttonViweFromSurfaceNormal = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            groupBox5 = new System.Windows.Forms.GroupBox();
            radioButtonStandardDeviation = new System.Windows.Forms.RadioButton();
            radioButtonAverageEnergy = new System.Windows.Forms.RadioButton();
            radioButtonFrequency = new System.Windows.Forms.RadioButton();
            poleFigureControl = new PoleFigureControl2();
            checkBoxDrawAxesInStereonet = new System.Windows.Forms.CheckBox();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            numericBoxDetTilt = new NumericBox();
            numericBoxDetRadius = new NumericBox();
            numericBoxZofDet = new NumericBox();
            numericBoxYofDet = new NumericBox();
            label2 = new System.Windows.Forms.Label();
            graphicsBox = new ImagingSolution.Control.GraphicsBox(components);
            groupBox4 = new System.Windows.Forms.GroupBox();
            trackBarStrSize = new System.Windows.Forms.TrackBar();
            colorControlExcessLine = new ColorControl();
            trackBarLineWidth = new System.Windows.Forms.TrackBar();
            label11 = new System.Windows.Forms.Label();
            colorControlString = new ColorControl();
            colorControlBackGround = new ColorControl();
            radioButtonKikuchiThresholdOfStructureFactor = new System.Windows.Forms.RadioButton();
            checkBoxKikuchiLine_Kinematical = new System.Windows.Forms.CheckBox();
            radioButtonKikuchiThresholdOfLength = new System.Windows.Forms.RadioButton();
            numericBoxKikuchiThreadSholdOfStructureFactor = new NumericBox();
            numericBoxKikuchiThresholdOfLength = new NumericBox();
            button1 = new System.Windows.Forms.Button();
            flowLayoutPanel1.SuspendLayout();
            groupBox5.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).BeginInit();
            SuspendLayout();
            // 
            // panelGeometry
            // 
            panelGeometry.Location = new System.Drawing.Point(6, 127);
            panelGeometry.Name = "panelGeometry";
            panelGeometry.Size = new System.Drawing.Size(300, 300);
            panelGeometry.TabIndex = 0;
            // 
            // numericBoxSampleTilt
            // 
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            numericBoxSampleTilt.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxSampleTilt.FooterText = "°";
            numericBoxSampleTilt.HeaderText = "Sample tilt";
            numericBoxSampleTilt.Location = new System.Drawing.Point(3, 9);
            numericBoxSampleTilt.Margin = new System.Windows.Forms.Padding(0);
            numericBoxSampleTilt.Maximum = 0D;
            numericBoxSampleTilt.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.RadianValue = -1.2217304763960306D;
            numericBoxSampleTilt.RoundErrorAccuracy = -1;
            numericBoxSampleTilt.ShowUpDown = true;
            numericBoxSampleTilt.Size = new System.Drawing.Size(128, 26);
            numericBoxSampleTilt.TabIndex = 111;
            numericBoxSampleTilt.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxSampleTilt.UpDown_Increment = 10D;
            numericBoxSampleTilt.Value = -70D;
            numericBoxSampleTilt.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // waveLengthControl
            // 
            waveLengthControl.AutoSize = true;
            waveLengthControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            waveLengthControl.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl.Energy = 20D;
            waveLengthControl.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            waveLengthControl.Location = new System.Drawing.Point(663, 9);
            waveLengthControl.Margin = new System.Windows.Forms.Padding(0);
            waveLengthControl.MaximumSize = new System.Drawing.Size(500, 500);
            waveLengthControl.MinimumSize = new System.Drawing.Size(210, 0);
            waveLengthControl.Monochrome = true;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = false;
            waveLengthControl.Size = new System.Drawing.Size(210, 55);
            waveLengthControl.TabIndex = 109;
            waveLengthControl.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            waveLengthControl.WaveLength = 0.0085885141045000009D;
            waveLengthControl.WaveSource = WaveSource.Electron;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            waveLengthControl.XrayWaveSourceLine = XrayLine.Ka1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(buttonFromX);
            flowLayoutPanel1.Controls.Add(buttonViewFromZ);
            flowLayoutPanel1.Controls.Add(buttonViweFromSurfaceNormal);
            flowLayoutPanel1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            flowLayoutPanel1.Location = new System.Drawing.Point(6, 430);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(300, 52);
            flowLayoutPanel1.TabIndex = 112;
            // 
            // buttonFromX
            // 
            buttonFromX.AutoSize = true;
            buttonFromX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonFromX.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonFromX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonFromX.Location = new System.Drawing.Point(0, 0);
            buttonFromX.Margin = new System.Windows.Forms.Padding(0);
            buttonFromX.Name = "buttonFromX";
            buttonFromX.Size = new System.Drawing.Size(130, 25);
            buttonFromX.TabIndex = 98;
            buttonFromX.Text = "From X (rotation axis)";
            buttonFromX.UseVisualStyleBackColor = true;
            buttonFromX.Click += buttonViewFromX_Click;
            // 
            // buttonViewFromZ
            // 
            buttonViewFromZ.AutoSize = true;
            buttonViewFromZ.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonViewFromZ.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonViewFromZ.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonViewFromZ.Location = new System.Drawing.Point(130, 0);
            buttonViewFromZ.Margin = new System.Windows.Forms.Padding(0);
            buttonViewFromZ.Name = "buttonViewFromZ";
            buttonViewFromZ.Size = new System.Drawing.Size(154, 25);
            buttonViewFromZ.TabIndex = 99;
            buttonViewFromZ.Text = "From Z (=beam direction)";
            buttonViewFromZ.UseVisualStyleBackColor = true;
            buttonViewFromZ.Click += buttonViewFromZ_Click;
            // 
            // buttonViweFromSurfaceNormal
            // 
            buttonViweFromSurfaceNormal.AutoSize = true;
            buttonViweFromSurfaceNormal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonViweFromSurfaceNormal.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonViweFromSurfaceNormal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonViweFromSurfaceNormal.Location = new System.Drawing.Point(0, 25);
            buttonViweFromSurfaceNormal.Margin = new System.Windows.Forms.Padding(0);
            buttonViweFromSurfaceNormal.Name = "buttonViweFromSurfaceNormal";
            buttonViweFromSurfaceNormal.Size = new System.Drawing.Size(147, 25);
            buttonViweFromSurfaceNormal.TabIndex = 98;
            buttonViweFromSurfaceNormal.Text = "From the surface normal";
            buttonViweFromSurfaceNormal.UseVisualStyleBackColor = true;
            buttonViweFromSurfaceNormal.Click += buttonFromSurfaceNormal_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(312, 527);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 113;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(radioButtonStandardDeviation);
            groupBox5.Controls.Add(radioButtonAverageEnergy);
            groupBox5.Controls.Add(radioButtonFrequency);
            groupBox5.Controls.Add(poleFigureControl);
            groupBox5.Controls.Add(checkBoxDrawAxesInStereonet);
            groupBox5.Location = new System.Drawing.Point(312, 9);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(333, 512);
            groupBox5.TabIndex = 114;
            groupBox5.TabStop = false;
            groupBox5.Text = "Direction distribution of BSEs (the center of stereonet corresponds to the surface normal direction)";
            // 
            // radioButtonStandardDeviation
            // 
            radioButtonStandardDeviation.AutoSize = true;
            radioButtonStandardDeviation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonStandardDeviation.Location = new System.Drawing.Point(6, 60);
            radioButtonStandardDeviation.Name = "radioButtonStandardDeviation";
            radioButtonStandardDeviation.Size = new System.Drawing.Size(177, 19);
            radioButtonStandardDeviation.TabIndex = 106;
            radioButtonStandardDeviation.TabStop = true;
            radioButtonStandardDeviation.Text = "Standard deviation of energy";
            radioButtonStandardDeviation.UseVisualStyleBackColor = true;
            // 
            // radioButtonAverageEnergy
            // 
            radioButtonAverageEnergy.AutoSize = true;
            radioButtonAverageEnergy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonAverageEnergy.Location = new System.Drawing.Point(103, 39);
            radioButtonAverageEnergy.Name = "radioButtonAverageEnergy";
            radioButtonAverageEnergy.Size = new System.Drawing.Size(107, 19);
            radioButtonAverageEnergy.TabIndex = 106;
            radioButtonAverageEnergy.TabStop = true;
            radioButtonAverageEnergy.Text = "Average energy";
            radioButtonAverageEnergy.UseVisualStyleBackColor = true;
            // 
            // radioButtonFrequency
            // 
            radioButtonFrequency.AutoSize = true;
            radioButtonFrequency.Checked = true;
            radioButtonFrequency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonFrequency.Location = new System.Drawing.Point(6, 39);
            radioButtonFrequency.Name = "radioButtonFrequency";
            radioButtonFrequency.Size = new System.Drawing.Size(80, 19);
            radioButtonFrequency.TabIndex = 106;
            radioButtonFrequency.TabStop = true;
            radioButtonFrequency.Text = "Frequency";
            radioButtonFrequency.UseVisualStyleBackColor = true;
            // 
            // poleFigureControl
            // 
            poleFigureControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            poleFigureControl.Location = new System.Drawing.Point(6, 87);
            poleFigureControl.Margin = new System.Windows.Forms.Padding(4);
            poleFigureControl.Name = "poleFigureControl";
            poleFigureControl.Size = new System.Drawing.Size(322, 416);
            poleFigureControl.TabIndex = 104;
            poleFigureControl.Vectors = null;
            // 
            // checkBoxDrawAxesInStereonet
            // 
            checkBoxDrawAxesInStereonet.AutoSize = true;
            checkBoxDrawAxesInStereonet.Checked = true;
            checkBoxDrawAxesInStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxesInStereonet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxDrawAxesInStereonet.Location = new System.Drawing.Point(270, 43);
            checkBoxDrawAxesInStereonet.Name = "checkBoxDrawAxesInStereonet";
            checkBoxDrawAxesInStereonet.Size = new System.Drawing.Size(53, 34);
            checkBoxDrawAxesInStereonet.TabIndex = 105;
            checkBoxDrawAxesInStereonet.Text = "Draw\r\n axes";
            checkBoxDrawAxesInStereonet.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new System.Drawing.Point(0, 557);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(1382, 22);
            statusStrip1.TabIndex = 115;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // numericBoxDetTilt
            // 
            numericBoxDetTilt.BackColor = System.Drawing.Color.Transparent;
            numericBoxDetTilt.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDetTilt.FooterText = "°";
            numericBoxDetTilt.HeaderText = "Detector tilt";
            numericBoxDetTilt.Location = new System.Drawing.Point(142, 9);
            numericBoxDetTilt.Margin = new System.Windows.Forms.Padding(0);
            numericBoxDetTilt.Maximum = 180D;
            numericBoxDetTilt.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxDetTilt.Minimum = 0D;
            numericBoxDetTilt.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxDetTilt.Name = "numericBoxDetTilt";
            numericBoxDetTilt.RadianValue = 1.5707963267948966D;
            numericBoxDetTilt.RoundErrorAccuracy = -1;
            numericBoxDetTilt.ShowUpDown = true;
            numericBoxDetTilt.Size = new System.Drawing.Size(137, 26);
            numericBoxDetTilt.TabIndex = 111;
            numericBoxDetTilt.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDetTilt.UpDown_Increment = 10D;
            numericBoxDetTilt.Value = 90D;
            numericBoxDetTilt.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // numericBoxDetRadius
            // 
            numericBoxDetRadius.BackColor = System.Drawing.Color.Transparent;
            numericBoxDetRadius.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDetRadius.FooterText = "mm";
            numericBoxDetRadius.HeaderText = "Detector radius";
            numericBoxDetRadius.Location = new System.Drawing.Point(6, 40);
            numericBoxDetRadius.Margin = new System.Windows.Forms.Padding(0);
            numericBoxDetRadius.Maximum = 180D;
            numericBoxDetRadius.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxDetRadius.Minimum = 0D;
            numericBoxDetRadius.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxDetRadius.Name = "numericBoxDetRadius";
            numericBoxDetRadius.RadianValue = 0.52359877559829882D;
            numericBoxDetRadius.RoundErrorAccuracy = -1;
            numericBoxDetRadius.ShowUpDown = true;
            numericBoxDetRadius.Size = new System.Drawing.Size(175, 26);
            numericBoxDetRadius.TabIndex = 111;
            numericBoxDetRadius.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDetRadius.UpDown_Increment = 10D;
            numericBoxDetRadius.Value = 30D;
            numericBoxDetRadius.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // numericBoxZofDet
            // 
            numericBoxZofDet.BackColor = System.Drawing.Color.Transparent;
            numericBoxZofDet.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxZofDet.FooterText = "mm";
            numericBoxZofDet.HeaderText = "Z";
            numericBoxZofDet.Location = new System.Drawing.Point(107, 98);
            numericBoxZofDet.Margin = new System.Windows.Forms.Padding(0);
            numericBoxZofDet.Maximum = 1000D;
            numericBoxZofDet.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxZofDet.Minimum = -1000D;
            numericBoxZofDet.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxZofDet.Name = "numericBoxZofDet";
            numericBoxZofDet.RoundErrorAccuracy = -1;
            numericBoxZofDet.ShowUpDown = true;
            numericBoxZofDet.Size = new System.Drawing.Size(102, 25);
            numericBoxZofDet.TabIndex = 111;
            numericBoxZofDet.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxZofDet.UpDown_Increment = 10D;
            numericBoxZofDet.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // numericBoxYofDet
            // 
            numericBoxYofDet.BackColor = System.Drawing.Color.Transparent;
            numericBoxYofDet.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxYofDet.FooterText = "mm";
            numericBoxYofDet.HeaderText = "Y";
            numericBoxYofDet.Location = new System.Drawing.Point(107, 71);
            numericBoxYofDet.Margin = new System.Windows.Forms.Padding(0);
            numericBoxYofDet.Maximum = 1000D;
            numericBoxYofDet.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxYofDet.Minimum = -1000D;
            numericBoxYofDet.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxYofDet.Name = "numericBoxYofDet";
            numericBoxYofDet.RadianValue = -0.52359877559829882D;
            numericBoxYofDet.RoundErrorAccuracy = -1;
            numericBoxYofDet.ShowUpDown = true;
            numericBoxYofDet.Size = new System.Drawing.Size(102, 25);
            numericBoxYofDet.TabIndex = 111;
            numericBoxYofDet.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxYofDet.UpDown_Increment = 10D;
            numericBoxYofDet.Value = -30D;
            numericBoxYofDet.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label2.Location = new System.Drawing.Point(3, 76);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(101, 34);
            label2.TabIndex = 116;
            label2.Text = "Coordinates of\r\n detector center";
            // 
            // graphicsBox
            // 
            graphicsBox.BackColor = System.Drawing.Color.Transparent;
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            graphicsBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            graphicsBox.Location = new System.Drawing.Point(663, 69);
            graphicsBox.Margin = new System.Windows.Forms.Padding(0, 40, 0, 0);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.Size = new System.Drawing.Size(480, 480);
            graphicsBox.TabIndex = 117;
            graphicsBox.TabStop = false;
            graphicsBox.WaitOnLoad = true;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(trackBarStrSize);
            groupBox4.Location = new System.Drawing.Point(1174, 30);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(110, 56);
            groupBox4.TabIndex = 118;
            groupBox4.TabStop = false;
            groupBox4.Text = "String size";
            // 
            // trackBarStrSize
            // 
            trackBarStrSize.AutoSize = false;
            trackBarStrSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            trackBarStrSize.LargeChange = 50;
            trackBarStrSize.Location = new System.Drawing.Point(9, 27);
            trackBarStrSize.Maximum = 200;
            trackBarStrSize.Minimum = 1;
            trackBarStrSize.Name = "trackBarStrSize";
            trackBarStrSize.Size = new System.Drawing.Size(95, 20);
            trackBarStrSize.SmallChange = 10;
            trackBarStrSize.TabIndex = 0;
            trackBarStrSize.TickFrequency = 500;
            trackBarStrSize.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarStrSize.Value = 80;
            // 
            // colorControlExcessLine
            // 
            colorControlExcessLine.Argb = -2039584;
            colorControlExcessLine.AutoSize = true;
            colorControlExcessLine.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControlExcessLine.BackColor = System.Drawing.SystemColors.Control;
            colorControlExcessLine.Blue = 224;
            colorControlExcessLine.BlueF = 0.8784314F;
            colorControlExcessLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControlExcessLine.Color = System.Drawing.Color.FromArgb(224, 224, 224);
            colorControlExcessLine.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlExcessLine.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlExcessLine.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlExcessLine.FooterMargin = new System.Windows.Forms.Padding(0);
            colorControlExcessLine.FooterText = "Kikuchi line color";
            colorControlExcessLine.Green = 224;
            colorControlExcessLine.GreenF = 0.8784314F;
            colorControlExcessLine.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlExcessLine.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlExcessLine.HeaderText = "";
            colorControlExcessLine.Inversion = false;
            colorControlExcessLine.Location = new System.Drawing.Point(1174, 199);
            colorControlExcessLine.Margin = new System.Windows.Forms.Padding(0);
            colorControlExcessLine.Name = "colorControlExcessLine";
            colorControlExcessLine.Red = 224;
            colorControlExcessLine.RedF = 0.8784314F;
            colorControlExcessLine.Size = new System.Drawing.Size(126, 20);
            colorControlExcessLine.TabIndex = 119;
            colorControlExcessLine.ToolTip = "";
            // 
            // trackBarLineWidth
            // 
            trackBarLineWidth.AutoSize = false;
            trackBarLineWidth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            trackBarLineWidth.Location = new System.Drawing.Point(1249, 128);
            trackBarLineWidth.Maximum = 10000;
            trackBarLineWidth.Minimum = 1;
            trackBarLineWidth.Name = "trackBarLineWidth";
            trackBarLineWidth.Size = new System.Drawing.Size(70, 16);
            trackBarLineWidth.TabIndex = 120;
            trackBarLineWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarLineWidth.Value = 4000;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label11.Location = new System.Drawing.Point(1174, 127);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(69, 17);
            label11.TabIndex = 121;
            label11.Text = "Line Width";
            // 
            // colorControlString
            // 
            colorControlString.Argb = -1;
            colorControlString.AutoSize = true;
            colorControlString.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControlString.BackColor = System.Drawing.SystemColors.Control;
            colorControlString.Blue = 255;
            colorControlString.BlueF = 1F;
            colorControlString.BoxSize = new System.Drawing.Size(20, 20);
            colorControlString.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlString.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlString.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlString.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlString.FooterMargin = new System.Windows.Forms.Padding(0);
            colorControlString.FooterText = "Text";
            colorControlString.Green = 255;
            colorControlString.GreenF = 1F;
            colorControlString.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlString.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlString.HeaderText = "";
            colorControlString.Inversion = false;
            colorControlString.Location = new System.Drawing.Point(1174, 157);
            colorControlString.Margin = new System.Windows.Forms.Padding(0);
            colorControlString.Name = "colorControlString";
            colorControlString.Red = 255;
            colorControlString.RedF = 1F;
            colorControlString.Size = new System.Drawing.Size(49, 20);
            colorControlString.TabIndex = 122;
            colorControlString.ToolTip = "Set a color of strings";
            // 
            // colorControlBackGround
            // 
            colorControlBackGround.Argb = -14671840;
            colorControlBackGround.AutoSize = true;
            colorControlBackGround.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            colorControlBackGround.BackColor = System.Drawing.SystemColors.Control;
            colorControlBackGround.Blue = 32;
            colorControlBackGround.BlueF = 0.1254902F;
            colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(32, 32, 32);
            colorControlBackGround.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlBackGround.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlBackGround.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlBackGround.FooterMargin = new System.Windows.Forms.Padding(0);
            colorControlBackGround.FooterText = "Background";
            colorControlBackGround.Green = 32;
            colorControlBackGround.GreenF = 0.1254902F;
            colorControlBackGround.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlBackGround.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlBackGround.HeaderText = "";
            colorControlBackGround.Inversion = false;
            colorControlBackGround.Location = new System.Drawing.Point(1174, 179);
            colorControlBackGround.Margin = new System.Windows.Forms.Padding(0);
            colorControlBackGround.Name = "colorControlBackGround";
            colorControlBackGround.Red = 32;
            colorControlBackGround.RedF = 0.1254902F;
            colorControlBackGround.Size = new System.Drawing.Size(91, 20);
            colorControlBackGround.TabIndex = 123;
            colorControlBackGround.ToolTip = "Set a background color";
            // 
            // radioButtonKikuchiThresholdOfStructureFactor
            // 
            radioButtonKikuchiThresholdOfStructureFactor.AutoSize = true;
            radioButtonKikuchiThresholdOfStructureFactor.Checked = true;
            radioButtonKikuchiThresholdOfStructureFactor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonKikuchiThresholdOfStructureFactor.Location = new System.Drawing.Point(1158, 242);
            radioButtonKikuchiThresholdOfStructureFactor.Name = "radioButtonKikuchiThresholdOfStructureFactor";
            radioButtonKikuchiThresholdOfStructureFactor.Size = new System.Drawing.Size(107, 19);
            radioButtonKikuchiThresholdOfStructureFactor.TabIndex = 127;
            radioButtonKikuchiThresholdOfStructureFactor.TabStop = true;
            radioButtonKikuchiThresholdOfStructureFactor.Text = "Structure factor";
            radioButtonKikuchiThresholdOfStructureFactor.UseVisualStyleBackColor = true;
            // 
            // checkBoxKikuchiLine_Kinematical
            // 
            checkBoxKikuchiLine_Kinematical.AutoSize = true;
            checkBoxKikuchiLine_Kinematical.Checked = true;
            checkBoxKikuchiLine_Kinematical.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxKikuchiLine_Kinematical.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxKikuchiLine_Kinematical.Location = new System.Drawing.Point(1165, 296);
            checkBoxKikuchiLine_Kinematical.Name = "checkBoxKikuchiLine_Kinematical";
            checkBoxKikuchiLine_Kinematical.Size = new System.Drawing.Size(166, 34);
            checkBoxKikuchiLine_Kinematical.TabIndex = 126;
            checkBoxKikuchiLine_Kinematical.Text = "Reflect the structure factor\r\n in the Kikuchi line density";
            checkBoxKikuchiLine_Kinematical.UseVisualStyleBackColor = true;
            // 
            // radioButtonKikuchiThresholdOfLength
            // 
            radioButtonKikuchiThresholdOfLength.AutoSize = true;
            radioButtonKikuchiThresholdOfLength.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonKikuchiThresholdOfLength.Location = new System.Drawing.Point(1158, 269);
            radioButtonKikuchiThresholdOfLength.Name = "radioButtonKikuchiThresholdOfLength";
            radioButtonKikuchiThresholdOfLength.Size = new System.Drawing.Size(113, 19);
            radioButtonKikuchiThresholdOfLength.TabIndex = 128;
            radioButtonKikuchiThresholdOfLength.Text = "Threshold of 1/d";
            radioButtonKikuchiThresholdOfLength.UseVisualStyleBackColor = true;
            // 
            // numericBoxKikuchiThreadSholdOfStructureFactor
            // 
            numericBoxKikuchiThreadSholdOfStructureFactor.BackColor = System.Drawing.Color.Transparent;
            numericBoxKikuchiThreadSholdOfStructureFactor.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxKikuchiThreadSholdOfStructureFactor.HeaderText = "Top";
            numericBoxKikuchiThreadSholdOfStructureFactor.Location = new System.Drawing.Point(1273, 242);
            numericBoxKikuchiThreadSholdOfStructureFactor.Margin = new System.Windows.Forms.Padding(0);
            numericBoxKikuchiThreadSholdOfStructureFactor.Maximum = 1000D;
            numericBoxKikuchiThreadSholdOfStructureFactor.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxKikuchiThreadSholdOfStructureFactor.Minimum = 1D;
            numericBoxKikuchiThreadSholdOfStructureFactor.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxKikuchiThreadSholdOfStructureFactor.Name = "numericBoxKikuchiThreadSholdOfStructureFactor";
            numericBoxKikuchiThreadSholdOfStructureFactor.RadianValue = 1.7453292519943295D;
            numericBoxKikuchiThreadSholdOfStructureFactor.RoundErrorAccuracy = -1;
            numericBoxKikuchiThreadSholdOfStructureFactor.ShowUpDown = true;
            numericBoxKikuchiThreadSholdOfStructureFactor.Size = new System.Drawing.Size(102, 25);
            numericBoxKikuchiThreadSholdOfStructureFactor.SmartIncrement = true;
            numericBoxKikuchiThreadSholdOfStructureFactor.TabIndex = 124;
            numericBoxKikuchiThreadSholdOfStructureFactor.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxKikuchiThreadSholdOfStructureFactor.Value = 100D;
            // 
            // numericBoxKikuchiThresholdOfLength
            // 
            numericBoxKikuchiThresholdOfLength.BackColor = System.Drawing.Color.Transparent;
            numericBoxKikuchiThresholdOfLength.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxKikuchiThresholdOfLength.FooterText = "nm⁻¹";
            numericBoxKikuchiThresholdOfLength.HeaderText = "<";
            numericBoxKikuchiThresholdOfLength.Location = new System.Drawing.Point(1279, 268);
            numericBoxKikuchiThresholdOfLength.Margin = new System.Windows.Forms.Padding(0);
            numericBoxKikuchiThresholdOfLength.Maximum = 100D;
            numericBoxKikuchiThresholdOfLength.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxKikuchiThresholdOfLength.Minimum = 0D;
            numericBoxKikuchiThresholdOfLength.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxKikuchiThresholdOfLength.Name = "numericBoxKikuchiThresholdOfLength";
            numericBoxKikuchiThresholdOfLength.RadianValue = 0.17453292519943295D;
            numericBoxKikuchiThresholdOfLength.RoundErrorAccuracy = -1;
            numericBoxKikuchiThresholdOfLength.ShowUpDown = true;
            numericBoxKikuchiThresholdOfLength.Size = new System.Drawing.Size(105, 25);
            numericBoxKikuchiThresholdOfLength.SmartIncrement = true;
            numericBoxKikuchiThresholdOfLength.TabIndex = 125;
            numericBoxKikuchiThresholdOfLength.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxKikuchiThresholdOfLength.ToolTip = "Set a threshold of d-spacings of Kikuchi lines.\r\nKikuchi lines under this value are simulated. \r\n";
            numericBoxKikuchiThresholdOfLength.Value = 10D;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(958, 21);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 23);
            button1.TabIndex = 129;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormEBSD
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1382, 579);
            Controls.Add(button1);
            Controls.Add(radioButtonKikuchiThresholdOfStructureFactor);
            Controls.Add(checkBoxKikuchiLine_Kinematical);
            Controls.Add(radioButtonKikuchiThresholdOfLength);
            Controls.Add(numericBoxKikuchiThreadSholdOfStructureFactor);
            Controls.Add(numericBoxKikuchiThresholdOfLength);
            Controls.Add(colorControlBackGround);
            Controls.Add(colorControlString);
            Controls.Add(trackBarLineWidth);
            Controls.Add(label11);
            Controls.Add(colorControlExcessLine);
            Controls.Add(groupBox4);
            Controls.Add(graphicsBox);
            Controls.Add(label2);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox5);
            Controls.Add(button2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(numericBoxYofDet);
            Controls.Add(numericBoxZofDet);
            Controls.Add(numericBoxDetRadius);
            Controls.Add(numericBoxDetTilt);
            Controls.Add(numericBoxSampleTilt);
            Controls.Add(waveLengthControl);
            Controls.Add(panelGeometry);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Name = "FormEBSD";
            Text = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelGeometry;
        private NumericBox numericBoxSampleTilt;
        private WaveLengthControl waveLengthControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button buttonViewFromZ;
        private System.Windows.Forms.Button buttonFromX;
        private System.Windows.Forms.Button buttonViweFromSurfaceNormal;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButtonStandardDeviation;
        private System.Windows.Forms.RadioButton radioButtonAverageEnergy;
        private System.Windows.Forms.RadioButton radioButtonFrequency;
        private PoleFigureControl2 poleFigureControl;
        private System.Windows.Forms.CheckBox checkBoxDrawAxesInStereonet;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private NumericBox numericBoxDetTilt;
        private NumericBox numericBoxDetRadius;
        private NumericBox numericBoxZofDet;
        private NumericBox numericBoxYofDet;
        private System.Windows.Forms.Label label2;
        public ImagingSolution.Control.GraphicsBox graphicsBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TrackBar trackBarStrSize;
        public ColorControl colorControlExcessLine;
        private System.Windows.Forms.TrackBar trackBarLineWidth;
        private System.Windows.Forms.Label label11;
        public ColorControl colorControlString;
        public ColorControl colorControlBackGround;
        private System.Windows.Forms.RadioButton radioButtonKikuchiThresholdOfStructureFactor;
        private System.Windows.Forms.CheckBox checkBoxKikuchiLine_Kinematical;
        private System.Windows.Forms.RadioButton radioButtonKikuchiThresholdOfLength;
        private NumericBox numericBoxKikuchiThreadSholdOfStructureFactor;
        private NumericBox numericBoxKikuchiThresholdOfLength;
        private System.Windows.Forms.Button button1;
    }
}