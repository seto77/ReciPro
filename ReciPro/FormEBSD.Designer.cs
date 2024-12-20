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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEBSD));
            panelGeometry = new System.Windows.Forms.Panel();
            numericBoxSampleTilt = new NumericBox();
            waveLengthControl = new WaveLengthControl();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonViewQuarter = new System.Windows.Forms.Button();
            buttonViewFromSurfaceNormal = new System.Windows.Forms.Button();
            buttonFromX = new System.Windows.Forms.Button();
            buttonViewFromZ = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            groupBox5 = new System.Windows.Forms.GroupBox();
            radioButtonStandardDeviation = new System.Windows.Forms.RadioButton();
            radioButtonAverageEnergy = new System.Windows.Forms.RadioButton();
            radioButtonFrequency = new System.Windows.Forms.RadioButton();
            poleFigureControl = new PoleFigureControl2();
            checkBoxDrawAxesInStereonet = new System.Windows.Forms.CheckBox();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            numericBoxDetTilt = new NumericBox();
            numericBoxDetRadius = new NumericBox();
            numericBoxZofDet = new NumericBox();
            numericBoxYofDet = new NumericBox();
            label2 = new System.Windows.Forms.Label();
            graphicsBox = new ImagingSolution.Control.GraphicsBox(components);
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
            buttonSimulateEBSD = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            groupBox = new System.Windows.Forms.GroupBox();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxThicknessStep = new NumericBox();
            numericBoxMaxNumOfG = new NumericBox();
            numericBoxThicknessStart = new NumericBox();
            numericBoxThicknessEnd = new NumericBox();
            numericBoxDiskDiameter = new NumericBox();
            buttonStop = new System.Windows.Forms.Button();
            groupBoxOutput = new System.Windows.Forms.GroupBox();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            comboBoxGradient = new System.Windows.Forms.ComboBox();
            comboBoxScale = new System.Windows.Forms.ComboBox();
            trackBarOutputEnergy = new System.Windows.Forms.TrackBar();
            trackBarOutputThickness = new System.Windows.Forms.TrackBar();
            trackBarIntensityBrightnessMax = new System.Windows.Forms.TrackBar();
            textBoxEnergy = new System.Windows.Forms.TextBox();
            trackBarIntensityBrightnessMin = new System.Windows.Forms.TrackBar();
            label12 = new System.Windows.Forms.Label();
            textBoxThickness = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            checkBoxDrawKikuchiLines = new System.Windows.Forms.CheckBox();
            buttonSaveImage = new System.Windows.Forms.Button();
            graphControlEnergyProfile = new GraphControl();
            graphControlDepthProfile = new GraphControl();
            numericBoxEnergyEnd = new NumericBox();
            numericBoxEnergyStart = new NumericBox();
            numericBoxEnergyStep = new NumericBox();
            button1 = new System.Windows.Forms.Button();
            buttonCopyEnergyProfile = new System.Windows.Forms.Button();
            buttonDepthProfile = new System.Windows.Forms.Button();
            label13 = new System.Windows.Forms.Label();
            flowLayoutPanel1.SuspendLayout();
            groupBox5.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).BeginInit();
            groupBox.SuspendLayout();
            groupBoxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputEnergy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).BeginInit();
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
            waveLengthControl.WavelengthChanged += waveLengthControl_WavelengthChanged;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(buttonViewQuarter);
            flowLayoutPanel1.Controls.Add(buttonViewFromSurfaceNormal);
            flowLayoutPanel1.Controls.Add(buttonFromX);
            flowLayoutPanel1.Controls.Add(buttonViewFromZ);
            flowLayoutPanel1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            flowLayoutPanel1.Location = new System.Drawing.Point(6, 430);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(300, 54);
            flowLayoutPanel1.TabIndex = 112;
            // 
            // buttonViewQuarter
            // 
            buttonViewQuarter.AutoSize = true;
            buttonViewQuarter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonViewQuarter.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonViewQuarter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonViewQuarter.Location = new System.Drawing.Point(0, 0);
            buttonViewQuarter.Margin = new System.Windows.Forms.Padding(0);
            buttonViewQuarter.Name = "buttonViewQuarter";
            buttonViewQuarter.Size = new System.Drawing.Size(84, 25);
            buttonViewQuarter.TabIndex = 98;
            buttonViewQuarter.Text = "Quarter view";
            buttonViewQuarter.UseVisualStyleBackColor = true;
            buttonViewQuarter.Click += buttonViewQuarter_Click;
            // 
            // buttonViewFromSurfaceNormal
            // 
            buttonViewFromSurfaceNormal.AutoSize = true;
            buttonViewFromSurfaceNormal.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonViewFromSurfaceNormal.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonViewFromSurfaceNormal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonViewFromSurfaceNormal.Location = new System.Drawing.Point(84, 0);
            buttonViewFromSurfaceNormal.Margin = new System.Windows.Forms.Padding(0);
            buttonViewFromSurfaceNormal.Name = "buttonViewFromSurfaceNormal";
            buttonViewFromSurfaceNormal.Size = new System.Drawing.Size(147, 25);
            buttonViewFromSurfaceNormal.TabIndex = 98;
            buttonViewFromSurfaceNormal.Text = "From the surface normal";
            buttonViewFromSurfaceNormal.UseVisualStyleBackColor = true;
            buttonViewFromSurfaceNormal.Click += buttonFromSurfaceNormal_Click;
            // 
            // buttonFromX
            // 
            buttonFromX.AutoSize = true;
            buttonFromX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonFromX.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonFromX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonFromX.Location = new System.Drawing.Point(0, 25);
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
            buttonViewFromZ.Location = new System.Drawing.Point(130, 25);
            buttonViewFromZ.Margin = new System.Windows.Forms.Padding(0);
            buttonViewFromZ.Name = "buttonViewFromZ";
            buttonViewFromZ.Size = new System.Drawing.Size(154, 25);
            buttonViewFromZ.TabIndex = 99;
            buttonViewFromZ.Text = "From Z (=beam direction)";
            buttonViewFromZ.UseVisualStyleBackColor = true;
            buttonViewFromZ.Click += buttonViewFromZ_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(312, 527);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 23);
            button2.TabIndex = 113;
            button2.Text = "Calc BSE";
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
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusLabel1, toolStripStatusLabel2 });
            statusStrip1.Location = new System.Drawing.Point(0, 787);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(1513, 22);
            statusStrip1.TabIndex = 115;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new System.Drawing.Size(118, 17);
            toolStripStatusLabel2.Text = "toolStripStatusLabel2";
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
            numericBoxZofDet.Location = new System.Drawing.Point(117, 98);
            numericBoxZofDet.Margin = new System.Windows.Forms.Padding(0);
            numericBoxZofDet.Maximum = 1000D;
            numericBoxZofDet.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxZofDet.Minimum = -1000D;
            numericBoxZofDet.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxZofDet.Name = "numericBoxZofDet";
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
            numericBoxYofDet.Location = new System.Drawing.Point(117, 70);
            numericBoxYofDet.Margin = new System.Windows.Forms.Padding(0);
            numericBoxYofDet.Maximum = 1000D;
            numericBoxYofDet.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxYofDet.Minimum = -1000D;
            numericBoxYofDet.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxYofDet.Name = "numericBoxYofDet";
            numericBoxYofDet.RadianValue = -0.52359877559829882D;
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
            graphicsBox.Size = new System.Drawing.Size(388, 388);
            graphicsBox.TabIndex = 117;
            graphicsBox.TabStop = false;
            graphicsBox.WaitOnLoad = true;
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            graphicsBox.MouseUp += graphicsBox_MouseUp;
            // 
            // trackBarStrSize
            // 
            trackBarStrSize.AutoSize = false;
            trackBarStrSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            trackBarStrSize.LargeChange = 50;
            trackBarStrSize.Location = new System.Drawing.Point(96, 19);
            trackBarStrSize.Maximum = 200;
            trackBarStrSize.Minimum = 1;
            trackBarStrSize.Name = "trackBarStrSize";
            trackBarStrSize.Size = new System.Drawing.Size(70, 20);
            trackBarStrSize.SmallChange = 10;
            trackBarStrSize.TabIndex = 0;
            trackBarStrSize.TickFrequency = 500;
            trackBarStrSize.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarStrSize.Value = 80;
            trackBarStrSize.ValueChanged += colorControlExcessLine_ColorChanged;
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
            colorControlExcessLine.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlExcessLine.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlExcessLine.FooterMargin = new System.Windows.Forms.Padding(0);
            colorControlExcessLine.FooterText = "Kikuchi line";
            colorControlExcessLine.Green = 224;
            colorControlExcessLine.GreenF = 0.8784314F;
            colorControlExcessLine.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlExcessLine.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlExcessLine.Location = new System.Drawing.Point(21, 103);
            colorControlExcessLine.Margin = new System.Windows.Forms.Padding(0);
            colorControlExcessLine.Name = "colorControlExcessLine";
            colorControlExcessLine.Red = 224;
            colorControlExcessLine.RedF = 0.8784314F;
            colorControlExcessLine.Size = new System.Drawing.Size(92, 20);
            colorControlExcessLine.TabIndex = 119;
            colorControlExcessLine.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // trackBarLineWidth
            // 
            trackBarLineWidth.AutoSize = false;
            trackBarLineWidth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            trackBarLineWidth.Location = new System.Drawing.Point(96, 45);
            trackBarLineWidth.Maximum = 10000;
            trackBarLineWidth.Minimum = 1;
            trackBarLineWidth.Name = "trackBarLineWidth";
            trackBarLineWidth.Size = new System.Drawing.Size(70, 20);
            trackBarLineWidth.TabIndex = 120;
            trackBarLineWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarLineWidth.Value = 4000;
            trackBarLineWidth.ValueChanged += colorControlExcessLine_ColorChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label11.Location = new System.Drawing.Point(21, 44);
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
            colorControlString.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlString.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlString.FooterMargin = new System.Windows.Forms.Padding(0);
            colorControlString.FooterText = "Text";
            colorControlString.Green = 255;
            colorControlString.GreenF = 1F;
            colorControlString.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlString.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlString.Location = new System.Drawing.Point(21, 74);
            colorControlString.Margin = new System.Windows.Forms.Padding(0);
            colorControlString.Name = "colorControlString";
            colorControlString.Red = 255;
            colorControlString.RedF = 1F;
            colorControlString.Size = new System.Drawing.Size(49, 20);
            colorControlString.TabIndex = 122;
            colorControlString.ToolTip = "Set a color of strings";
            colorControlString.ColorChanged += colorControlExcessLine_ColorChanged;
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
            colorControlBackGround.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            colorControlBackGround.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlBackGround.FooterMargin = new System.Windows.Forms.Padding(0);
            colorControlBackGround.FooterText = "Background";
            colorControlBackGround.Green = 32;
            colorControlBackGround.GreenF = 0.1254902F;
            colorControlBackGround.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            colorControlBackGround.HeaderMargin = new System.Windows.Forms.Padding(0);
            colorControlBackGround.Location = new System.Drawing.Point(75, 74);
            colorControlBackGround.Margin = new System.Windows.Forms.Padding(0);
            colorControlBackGround.Name = "colorControlBackGround";
            colorControlBackGround.Red = 32;
            colorControlBackGround.RedF = 0.1254902F;
            colorControlBackGround.Size = new System.Drawing.Size(91, 20);
            colorControlBackGround.TabIndex = 123;
            colorControlBackGround.ToolTip = "Set a background color";
            colorControlBackGround.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // radioButtonKikuchiThresholdOfStructureFactor
            // 
            radioButtonKikuchiThresholdOfStructureFactor.AutoSize = true;
            radioButtonKikuchiThresholdOfStructureFactor.Checked = true;
            radioButtonKikuchiThresholdOfStructureFactor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonKikuchiThresholdOfStructureFactor.Location = new System.Drawing.Point(5, 135);
            radioButtonKikuchiThresholdOfStructureFactor.Name = "radioButtonKikuchiThresholdOfStructureFactor";
            radioButtonKikuchiThresholdOfStructureFactor.Size = new System.Drawing.Size(107, 19);
            radioButtonKikuchiThresholdOfStructureFactor.TabIndex = 127;
            radioButtonKikuchiThresholdOfStructureFactor.TabStop = true;
            radioButtonKikuchiThresholdOfStructureFactor.Text = "Structure factor";
            radioButtonKikuchiThresholdOfStructureFactor.UseVisualStyleBackColor = true;
            radioButtonKikuchiThresholdOfStructureFactor.CheckedChanged += radioButtonKikuchiThresholdOfStructureFactor_CheckedChanged;
            // 
            // checkBoxKikuchiLine_Kinematical
            // 
            checkBoxKikuchiLine_Kinematical.AutoSize = true;
            checkBoxKikuchiLine_Kinematical.Checked = true;
            checkBoxKikuchiLine_Kinematical.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxKikuchiLine_Kinematical.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxKikuchiLine_Kinematical.Location = new System.Drawing.Point(12, 234);
            checkBoxKikuchiLine_Kinematical.Name = "checkBoxKikuchiLine_Kinematical";
            checkBoxKikuchiLine_Kinematical.Size = new System.Drawing.Size(166, 34);
            checkBoxKikuchiLine_Kinematical.TabIndex = 126;
            checkBoxKikuchiLine_Kinematical.Text = "Reflect the structure factor\r\n in the Kikuchi line density";
            checkBoxKikuchiLine_Kinematical.UseVisualStyleBackColor = true;
            checkBoxKikuchiLine_Kinematical.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // radioButtonKikuchiThresholdOfLength
            // 
            radioButtonKikuchiThresholdOfLength.AutoSize = true;
            radioButtonKikuchiThresholdOfLength.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonKikuchiThresholdOfLength.Location = new System.Drawing.Point(5, 187);
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
            numericBoxKikuchiThreadSholdOfStructureFactor.Location = new System.Drawing.Point(64, 155);
            numericBoxKikuchiThreadSholdOfStructureFactor.Margin = new System.Windows.Forms.Padding(0);
            numericBoxKikuchiThreadSholdOfStructureFactor.Maximum = 1000D;
            numericBoxKikuchiThreadSholdOfStructureFactor.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxKikuchiThreadSholdOfStructureFactor.Minimum = 1D;
            numericBoxKikuchiThreadSholdOfStructureFactor.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxKikuchiThreadSholdOfStructureFactor.Name = "numericBoxKikuchiThreadSholdOfStructureFactor";
            numericBoxKikuchiThreadSholdOfStructureFactor.RadianValue = 1.7453292519943295D;
            numericBoxKikuchiThreadSholdOfStructureFactor.ShowUpDown = true;
            numericBoxKikuchiThreadSholdOfStructureFactor.Size = new System.Drawing.Size(102, 25);
            numericBoxKikuchiThreadSholdOfStructureFactor.SmartIncrement = true;
            numericBoxKikuchiThreadSholdOfStructureFactor.TabIndex = 124;
            numericBoxKikuchiThreadSholdOfStructureFactor.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxKikuchiThreadSholdOfStructureFactor.Value = 100D;
            numericBoxKikuchiThreadSholdOfStructureFactor.ValueChanged += numericBoxKikuchiThreadSholdOfStructureFactor_ValueChanged;
            // 
            // numericBoxKikuchiThresholdOfLength
            // 
            numericBoxKikuchiThresholdOfLength.BackColor = System.Drawing.Color.Transparent;
            numericBoxKikuchiThresholdOfLength.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxKikuchiThresholdOfLength.FooterText = "nm⁻¹";
            numericBoxKikuchiThresholdOfLength.HeaderText = "<";
            numericBoxKikuchiThresholdOfLength.Location = new System.Drawing.Point(64, 209);
            numericBoxKikuchiThresholdOfLength.Margin = new System.Windows.Forms.Padding(0);
            numericBoxKikuchiThresholdOfLength.Maximum = 100D;
            numericBoxKikuchiThresholdOfLength.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxKikuchiThresholdOfLength.Minimum = 0D;
            numericBoxKikuchiThresholdOfLength.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxKikuchiThresholdOfLength.Name = "numericBoxKikuchiThresholdOfLength";
            numericBoxKikuchiThresholdOfLength.RadianValue = 0.17453292519943295D;
            numericBoxKikuchiThresholdOfLength.ShowUpDown = true;
            numericBoxKikuchiThresholdOfLength.Size = new System.Drawing.Size(105, 25);
            numericBoxKikuchiThresholdOfLength.SmartIncrement = true;
            numericBoxKikuchiThresholdOfLength.TabIndex = 125;
            numericBoxKikuchiThresholdOfLength.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxKikuchiThresholdOfLength.ToolTip = "Set a threshold of d-spacings of Kikuchi lines.\r\nKikuchi lines under this value are simulated. \r\n";
            numericBoxKikuchiThresholdOfLength.Value = 10D;
            numericBoxKikuchiThresholdOfLength.ValueChanged += numericBoxKikuchiThreadSholdOfStructureFactor_ValueChanged;
            // 
            // buttonSimulateEBSD
            // 
            buttonSimulateEBSD.AutoSize = true;
            buttonSimulateEBSD.BackColor = System.Drawing.Color.SteelBlue;
            buttonSimulateEBSD.ForeColor = System.Drawing.Color.White;
            buttonSimulateEBSD.Location = new System.Drawing.Point(1268, 291);
            buttonSimulateEBSD.Name = "buttonSimulateEBSD";
            buttonSimulateEBSD.Size = new System.Drawing.Size(93, 26);
            buttonSimulateEBSD.TabIndex = 129;
            buttonSimulateEBSD.Text = "Simulate EBSD";
            buttonSimulateEBSD.UseVisualStyleBackColor = false;
            buttonSimulateEBSD.Click += buttonSimulateEBSD_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label1.Location = new System.Drawing.Point(21, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(68, 17);
            label1.TabIndex = 121;
            label1.Text = "String size";
            // 
            // groupBox
            // 
            groupBox.Controls.Add(label1);
            groupBox.Controls.Add(trackBarStrSize);
            groupBox.Controls.Add(colorControlExcessLine);
            groupBox.Controls.Add(label11);
            groupBox.Controls.Add(radioButtonKikuchiThresholdOfStructureFactor);
            groupBox.Controls.Add(trackBarLineWidth);
            groupBox.Controls.Add(checkBoxKikuchiLine_Kinematical);
            groupBox.Controls.Add(colorControlString);
            groupBox.Controls.Add(radioButtonKikuchiThresholdOfLength);
            groupBox.Controls.Add(colorControlBackGround);
            groupBox.Controls.Add(numericBoxKikuchiThreadSholdOfStructureFactor);
            groupBox.Controls.Add(numericBoxKikuchiThresholdOfLength);
            groupBox.Location = new System.Drawing.Point(1062, 45);
            groupBox.Name = "groupBox";
            groupBox.Size = new System.Drawing.Size(184, 272);
            groupBox.TabIndex = 130;
            groupBox.TabStop = false;
            groupBox.Text = "Kikuchi line properties";
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel2.Location = new System.Drawing.Point(789, 279);
            flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(0, 0);
            flowLayoutPanel2.TabIndex = 137;
            // 
            // numericBoxThicknessStep
            // 
            numericBoxThicknessStep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxThicknessStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxThicknessStep.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.FooterText = "nm";
            numericBoxThicknessStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.HeaderText = "with step of";
            numericBoxThicknessStep.Location = new System.Drawing.Point(1343, 153);
            numericBoxThicknessStep.Margin = new System.Windows.Forms.Padding(0);
            numericBoxThicknessStep.Maximum = 1000D;
            numericBoxThicknessStep.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxThicknessStep.Minimum = 1D;
            numericBoxThicknessStep.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxThicknessStep.Name = "numericBoxThicknessStep";
            numericBoxThicknessStep.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxThicknessStep.RadianValue = 0.017453292519943295D;
            numericBoxThicknessStep.ShowUpDown = true;
            numericBoxThicknessStep.Size = new System.Drawing.Size(149, 27);
            numericBoxThicknessStep.SmartIncrement = true;
            numericBoxThicknessStep.TabIndex = 135;
            numericBoxThicknessStep.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxThicknessStep.ThonsandsSeparator = true;
            numericBoxThicknessStep.ToolTip = "Set a range and step of the sample thichnesses";
            numericBoxThicknessStep.Value = 1D;
            numericBoxThicknessStep.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxMaxNumOfG
            // 
            numericBoxMaxNumOfG.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxMaxNumOfG.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxNumOfG.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxMaxNumOfG.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxNumOfG.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxNumOfG.HeaderText = "Number of diffracted waves";
            numericBoxMaxNumOfG.Location = new System.Drawing.Point(1266, 45);
            numericBoxMaxNumOfG.Margin = new System.Windows.Forms.Padding(0);
            numericBoxMaxNumOfG.Maximum = 2048D;
            numericBoxMaxNumOfG.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxMaxNumOfG.Minimum = 1D;
            numericBoxMaxNumOfG.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxMaxNumOfG.Name = "numericBoxMaxNumOfG";
            numericBoxMaxNumOfG.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxMaxNumOfG.RadianValue = 0.55850536063818546D;
            numericBoxMaxNumOfG.ShowUpDown = true;
            numericBoxMaxNumOfG.Size = new System.Drawing.Size(221, 25);
            numericBoxMaxNumOfG.SmartIncrement = true;
            numericBoxMaxNumOfG.TabIndex = 131;
            numericBoxMaxNumOfG.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxMaxNumOfG.ThonsandsSeparator = true;
            numericBoxMaxNumOfG.ToolTip = "Set a number of diffracted waves to be calculated";
            numericBoxMaxNumOfG.Value = 32D;
            // 
            // numericBoxThicknessStart
            // 
            numericBoxThicknessStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxThicknessStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxThicknessStart.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.HeaderText = "Thickness from";
            numericBoxThicknessStart.Location = new System.Drawing.Point(1268, 126);
            numericBoxThicknessStart.Margin = new System.Windows.Forms.Padding(0);
            numericBoxThicknessStart.Maximum = 1000D;
            numericBoxThicknessStart.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxThicknessStart.Minimum = 1D;
            numericBoxThicknessStart.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxThicknessStart.Name = "numericBoxThicknessStart";
            numericBoxThicknessStart.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxThicknessStart.RadianValue = 0.017453292519943295D;
            numericBoxThicknessStart.ShowUpDown = true;
            numericBoxThicknessStart.Size = new System.Drawing.Size(147, 27);
            numericBoxThicknessStart.SmartIncrement = true;
            numericBoxThicknessStart.TabIndex = 133;
            numericBoxThicknessStart.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxThicknessStart.ThonsandsSeparator = true;
            numericBoxThicknessStart.ToolTip = "Set a range and step of the sample thichnesses";
            numericBoxThicknessStart.Value = 1D;
            numericBoxThicknessStart.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxThicknessEnd
            // 
            numericBoxThicknessEnd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxThicknessEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxThicknessEnd.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.HeaderText = "to";
            numericBoxThicknessEnd.Location = new System.Drawing.Point(1417, 126);
            numericBoxThicknessEnd.Margin = new System.Windows.Forms.Padding(0);
            numericBoxThicknessEnd.Maximum = 1000D;
            numericBoxThicknessEnd.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxThicknessEnd.Minimum = 1D;
            numericBoxThicknessEnd.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxThicknessEnd.Name = "numericBoxThicknessEnd";
            numericBoxThicknessEnd.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxThicknessEnd.RadianValue = 1.7453292519943295D;
            numericBoxThicknessEnd.ShowUpDown = true;
            numericBoxThicknessEnd.Size = new System.Drawing.Size(75, 27);
            numericBoxThicknessEnd.SmartIncrement = true;
            numericBoxThicknessEnd.TabIndex = 134;
            numericBoxThicknessEnd.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxThicknessEnd.ThonsandsSeparator = true;
            numericBoxThicknessEnd.ToolTip = "Set a range and step of the sample thichnesses";
            numericBoxThicknessEnd.Value = 100D;
            numericBoxThicknessEnd.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxDiskDiameter
            // 
            numericBoxDiskDiameter.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDiskDiameter.DecimalPlaces = 0;
            numericBoxDiskDiameter.Font = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDiskDiameter.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDiskDiameter.FooterText = "pixels";
            numericBoxDiskDiameter.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDiskDiameter.HeaderText = "Diameter";
            numericBoxDiskDiameter.Location = new System.Drawing.Point(1268, 77);
            numericBoxDiskDiameter.Margin = new System.Windows.Forms.Padding(0);
            numericBoxDiskDiameter.Maximum = 1024D;
            numericBoxDiskDiameter.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxDiskDiameter.Minimum = 16D;
            numericBoxDiskDiameter.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxDiskDiameter.Name = "numericBoxDiskDiameter";
            numericBoxDiskDiameter.RadianValue = 2.2340214425527418D;
            numericBoxDiskDiameter.ShowUpDown = true;
            numericBoxDiskDiameter.Size = new System.Drawing.Size(154, 27);
            numericBoxDiskDiameter.SmartIncrement = true;
            numericBoxDiskDiameter.TabIndex = 132;
            numericBoxDiskDiameter.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxDiskDiameter.ThonsandsSeparator = true;
            numericBoxDiskDiameter.ToolTip = "Set a number of divisions along the diameter.\r\nThe number of two-dimensional divisions is displayed on the right";
            numericBoxDiskDiameter.Value = 128D;
            // 
            // buttonStop
            // 
            buttonStop.BackColor = System.Drawing.Color.IndianRed;
            buttonStop.ForeColor = System.Drawing.Color.White;
            buttonStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            buttonStop.Location = new System.Drawing.Point(1367, 291);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new System.Drawing.Size(92, 26);
            buttonStop.TabIndex = 138;
            buttonStop.Text = "Stop";
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Visible = false;
            // 
            // groupBoxOutput
            // 
            groupBoxOutput.Controls.Add(label3);
            groupBoxOutput.Controls.Add(label4);
            groupBoxOutput.Controls.Add(comboBoxGradient);
            groupBoxOutput.Controls.Add(comboBoxScale);
            groupBoxOutput.Controls.Add(trackBarOutputEnergy);
            groupBoxOutput.Controls.Add(trackBarOutputThickness);
            groupBoxOutput.Controls.Add(trackBarIntensityBrightnessMax);
            groupBoxOutput.Controls.Add(textBoxEnergy);
            groupBoxOutput.Controls.Add(trackBarIntensityBrightnessMin);
            groupBoxOutput.Controls.Add(label12);
            groupBoxOutput.Controls.Add(textBoxThickness);
            groupBoxOutput.Controls.Add(label9);
            groupBoxOutput.Controls.Add(label6);
            groupBoxOutput.Controls.Add(label5);
            groupBoxOutput.Controls.Add(label8);
            groupBoxOutput.Controls.Add(label7);
            groupBoxOutput.Controls.Add(label10);
            groupBoxOutput.Enabled = false;
            groupBoxOutput.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold);
            groupBoxOutput.Location = new System.Drawing.Point(1074, 334);
            groupBoxOutput.Name = "groupBoxOutput";
            groupBoxOutput.Size = new System.Drawing.Size(373, 200);
            groupBoxOutput.TabIndex = 139;
            groupBoxOutput.TabStop = false;
            groupBoxOutput.Text = "Output parameters";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Enabled = false;
            label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label3.Location = new System.Drawing.Point(68, 168);
            label3.Margin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 15);
            label3.TabIndex = 6;
            label3.Text = "Gradient";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Enabled = false;
            label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label4.Location = new System.Drawing.Point(214, 168);
            label4.Margin = new System.Windows.Forms.Padding(2, 2, 0, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(36, 15);
            label4.TabIndex = 44;
            label4.Text = "Color";
            // 
            // comboBoxGradient
            // 
            comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxGradient.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            comboBoxGradient.FormattingEnabled = true;
            comboBoxGradient.Items.AddRange(new object[] { "Positive", "Negative" });
            comboBoxGradient.Location = new System.Drawing.Point(126, 165);
            comboBoxGradient.Margin = new System.Windows.Forms.Padding(0);
            comboBoxGradient.Name = "comboBoxGradient";
            comboBoxGradient.Size = new System.Drawing.Size(82, 23);
            comboBoxGradient.TabIndex = 7;
            comboBoxGradient.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // comboBoxScale
            // 
            comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            comboBoxScale.FormattingEnabled = true;
            comboBoxScale.Items.AddRange(new object[] { "Gray", "Cold-Warm", "Spectrum", "FIre" });
            comboBoxScale.Location = new System.Drawing.Point(254, 165);
            comboBoxScale.Margin = new System.Windows.Forms.Padding(0);
            comboBoxScale.Name = "comboBoxScale";
            comboBoxScale.Size = new System.Drawing.Size(112, 23);
            comboBoxScale.TabIndex = 8;
            comboBoxScale.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // trackBarOutputEnergy
            // 
            trackBarOutputEnergy.AutoSize = false;
            trackBarOutputEnergy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            trackBarOutputEnergy.LargeChange = 1;
            trackBarOutputEnergy.Location = new System.Drawing.Point(132, 28);
            trackBarOutputEnergy.Maximum = 5;
            trackBarOutputEnergy.Name = "trackBarOutputEnergy";
            trackBarOutputEnergy.Size = new System.Drawing.Size(234, 18);
            trackBarOutputEnergy.TabIndex = 2;
            trackBarOutputEnergy.ValueChanged += trackBarOutputEnergy_ValueChanged;
            // 
            // trackBarOutputThickness
            // 
            trackBarOutputThickness.AutoSize = false;
            trackBarOutputThickness.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            trackBarOutputThickness.LargeChange = 1;
            trackBarOutputThickness.Location = new System.Drawing.Point(132, 63);
            trackBarOutputThickness.Maximum = 9;
            trackBarOutputThickness.Name = "trackBarOutputThickness";
            trackBarOutputThickness.Size = new System.Drawing.Size(234, 18);
            trackBarOutputThickness.TabIndex = 2;
            trackBarOutputThickness.ValueChanged += TrackBarOutputThickness_Scroll;
            // 
            // trackBarIntensityBrightnessMax
            // 
            trackBarIntensityBrightnessMax.AutoSize = false;
            trackBarIntensityBrightnessMax.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            trackBarIntensityBrightnessMax.LargeChange = 10000;
            trackBarIntensityBrightnessMax.Location = new System.Drawing.Point(133, 132);
            trackBarIntensityBrightnessMax.Maximum = 1000000;
            trackBarIntensityBrightnessMax.Minimum = 1;
            trackBarIntensityBrightnessMax.Name = "trackBarIntensityBrightnessMax";
            trackBarIntensityBrightnessMax.Size = new System.Drawing.Size(234, 18);
            trackBarIntensityBrightnessMax.SmallChange = 100000;
            trackBarIntensityBrightnessMax.TabIndex = 4;
            trackBarIntensityBrightnessMax.TickFrequency = 20000;
            trackBarIntensityBrightnessMax.Value = 1000000;
            trackBarIntensityBrightnessMax.ValueChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // textBoxEnergy
            // 
            textBoxEnergy.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            textBoxEnergy.Location = new System.Drawing.Point(69, 25);
            textBoxEnergy.Name = "textBoxEnergy";
            textBoxEnergy.ReadOnly = true;
            textBoxEnergy.Size = new System.Drawing.Size(36, 25);
            textBoxEnergy.TabIndex = 1;
            textBoxEnergy.Text = "20";
            // 
            // trackBarIntensityBrightnessMin
            // 
            trackBarIntensityBrightnessMin.AutoSize = false;
            trackBarIntensityBrightnessMin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            trackBarIntensityBrightnessMin.LargeChange = 10000;
            trackBarIntensityBrightnessMin.Location = new System.Drawing.Point(133, 108);
            trackBarIntensityBrightnessMin.Maximum = 999999;
            trackBarIntensityBrightnessMin.Name = "trackBarIntensityBrightnessMin";
            trackBarIntensityBrightnessMin.Size = new System.Drawing.Size(234, 18);
            trackBarIntensityBrightnessMin.SmallChange = 100000;
            trackBarIntensityBrightnessMin.TabIndex = 3;
            trackBarIntensityBrightnessMin.TickFrequency = 20000;
            trackBarIntensityBrightnessMin.ValueChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label12.Location = new System.Drawing.Point(104, 28);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(22, 17);
            label12.TabIndex = 30;
            label12.Text = "kV";
            // 
            // textBoxThickness
            // 
            textBoxThickness.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            textBoxThickness.Location = new System.Drawing.Point(69, 60);
            textBoxThickness.Name = "textBoxThickness";
            textBoxThickness.ReadOnly = true;
            textBoxThickness.Size = new System.Drawing.Size(36, 25);
            textBoxThickness.TabIndex = 1;
            textBoxThickness.Text = "20";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label9.Location = new System.Drawing.Point(9, 28);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(48, 17);
            label9.TabIndex = 30;
            label9.Text = "Energy";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label6.Location = new System.Drawing.Point(104, 63);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(26, 17);
            label6.TabIndex = 30;
            label6.Text = "nm";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label5.Location = new System.Drawing.Point(9, 63);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(63, 17);
            label5.TabIndex = 30;
            label5.Text = "Thickness";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label8.Location = new System.Drawing.Point(94, 132);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(33, 17);
            label8.TabIndex = 30;
            label8.Text = "Max";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label7.Location = new System.Drawing.Point(94, 108);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(30, 17);
            label7.TabIndex = 30;
            label7.Text = "Min";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label10.Location = new System.Drawing.Point(20, 122);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(68, 17);
            label10.TabIndex = 30;
            label10.Text = "Brightness";
            // 
            // checkBoxDrawKikuchiLines
            // 
            checkBoxDrawKikuchiLines.AutoSize = true;
            checkBoxDrawKikuchiLines.Checked = true;
            checkBoxDrawKikuchiLines.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawKikuchiLines.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxDrawKikuchiLines.Location = new System.Drawing.Point(1072, 16);
            checkBoxDrawKikuchiLines.Name = "checkBoxDrawKikuchiLines";
            checkBoxDrawKikuchiLines.Size = new System.Drawing.Size(122, 19);
            checkBoxDrawKikuchiLines.TabIndex = 126;
            checkBoxDrawKikuchiLines.Text = "Draw Kikuchi lines";
            checkBoxDrawKikuchiLines.UseVisualStyleBackColor = true;
            checkBoxDrawKikuchiLines.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // buttonSaveImage
            // 
            buttonSaveImage.AutoSize = true;
            buttonSaveImage.Location = new System.Drawing.Point(940, 36);
            buttonSaveImage.Name = "buttonSaveImage";
            buttonSaveImage.Size = new System.Drawing.Size(81, 25);
            buttonSaveImage.TabIndex = 113;
            buttonSaveImage.Text = "Copy image";
            buttonSaveImage.UseVisualStyleBackColor = true;
            buttonSaveImage.Click += buttonSaveImage_Click;
            // 
            // graphControlEnergyProfile
            // 
            graphControlEnergyProfile.AllowMouseOperation = true;
            graphControlEnergyProfile.AxisLineColor = System.Drawing.Color.Gray;
            graphControlEnergyProfile.AxisTextColor = System.Drawing.Color.Black;
            graphControlEnergyProfile.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControlEnergyProfile.AxisXTextVisible = true;
            graphControlEnergyProfile.AxisYTextVisible = true;
            graphControlEnergyProfile.BackgroundColor = System.Drawing.Color.White;
            graphControlEnergyProfile.BottomMargin = 0D;
            graphControlEnergyProfile.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlEnergyProfile.DivisionLineXVisible = true;
            graphControlEnergyProfile.DivisionLineYVisible = true;
            graphControlEnergyProfile.DrawingRange = (RectangleD)resources.GetObject("graphControlEnergyProfile.DrawingRange");
            graphControlEnergyProfile.FixRangeHorizontal = false;
            graphControlEnergyProfile.FixRangeVertical = false;
            graphControlEnergyProfile.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            graphControlEnergyProfile.GraphTitle = "";
            graphControlEnergyProfile.Interpolation = false;
            graphControlEnergyProfile.IsIntegerX = false;
            graphControlEnergyProfile.IsIntegerY = false;
            graphControlEnergyProfile.LabelX = "X:";
            graphControlEnergyProfile.LabelY = "Y:";
            graphControlEnergyProfile.LeftMargin = 0F;
            graphControlEnergyProfile.LineWidth = 1F;
            graphControlEnergyProfile.Location = new System.Drawing.Point(136, 570);
            graphControlEnergyProfile.LowerX = 0D;
            graphControlEnergyProfile.LowerY = 0D;
            graphControlEnergyProfile.MaximalX = 1D;
            graphControlEnergyProfile.MaximalY = 1D;
            graphControlEnergyProfile.MinimalX = 0D;
            graphControlEnergyProfile.MinimalY = 0D;
            graphControlEnergyProfile.Mode = GraphControl.DrawingMode.Line;
            graphControlEnergyProfile.MousePositionVisible = true;
            graphControlEnergyProfile.MousePositionXDigit = -1;
            graphControlEnergyProfile.MousePositionYDigit = -1;
            graphControlEnergyProfile.Name = "graphControlEnergyProfile";
            graphControlEnergyProfile.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlEnergyProfile.Profile = null;
            graphControlEnergyProfile.Size = new System.Drawing.Size(400, 200);
            graphControlEnergyProfile.Smoothing = false;
            graphControlEnergyProfile.TabIndex = 140;
            graphControlEnergyProfile.UnitX = "";
            graphControlEnergyProfile.UnitY = "";
            graphControlEnergyProfile.UpperPanelFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            graphControlEnergyProfile.UpperPanelVisible = true;
            graphControlEnergyProfile.UpperX = 1D;
            graphControlEnergyProfile.UpperY = 1D;
            graphControlEnergyProfile.UseLineWidth = true;
            graphControlEnergyProfile.VerticalLineColor = System.Drawing.Color.Red;
            graphControlEnergyProfile.XLog = false;
            graphControlEnergyProfile.YLog = false;
            // 
            // graphControlDepthProfile
            // 
            graphControlDepthProfile.AllowMouseOperation = true;
            graphControlDepthProfile.AxisLineColor = System.Drawing.Color.Gray;
            graphControlDepthProfile.AxisTextColor = System.Drawing.Color.Black;
            graphControlDepthProfile.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControlDepthProfile.AxisXTextVisible = true;
            graphControlDepthProfile.AxisYTextVisible = true;
            graphControlDepthProfile.BackgroundColor = System.Drawing.Color.White;
            graphControlDepthProfile.BottomMargin = 0D;
            graphControlDepthProfile.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlDepthProfile.DivisionLineXVisible = true;
            graphControlDepthProfile.DivisionLineYVisible = true;
            graphControlDepthProfile.DrawingRange = (RectangleD)resources.GetObject("graphControlDepthProfile.DrawingRange");
            graphControlDepthProfile.FixRangeHorizontal = false;
            graphControlDepthProfile.FixRangeVertical = false;
            graphControlDepthProfile.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            graphControlDepthProfile.GraphTitle = "";
            graphControlDepthProfile.Interpolation = false;
            graphControlDepthProfile.IsIntegerX = false;
            graphControlDepthProfile.IsIntegerY = false;
            graphControlDepthProfile.LabelX = "X:";
            graphControlDepthProfile.LabelY = "Y:";
            graphControlDepthProfile.LeftMargin = 0F;
            graphControlDepthProfile.LineWidth = 1F;
            graphControlDepthProfile.Location = new System.Drawing.Point(556, 570);
            graphControlDepthProfile.LowerX = 0D;
            graphControlDepthProfile.LowerY = 0D;
            graphControlDepthProfile.MaximalX = 1D;
            graphControlDepthProfile.MaximalY = 1D;
            graphControlDepthProfile.MinimalX = 0D;
            graphControlDepthProfile.MinimalY = 0D;
            graphControlDepthProfile.Mode = GraphControl.DrawingMode.Line;
            graphControlDepthProfile.MousePositionVisible = true;
            graphControlDepthProfile.MousePositionXDigit = -1;
            graphControlDepthProfile.MousePositionYDigit = -1;
            graphControlDepthProfile.Name = "graphControlDepthProfile";
            graphControlDepthProfile.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlDepthProfile.Profile = null;
            graphControlDepthProfile.Size = new System.Drawing.Size(400, 200);
            graphControlDepthProfile.Smoothing = false;
            graphControlDepthProfile.TabIndex = 140;
            graphControlDepthProfile.UnitX = "";
            graphControlDepthProfile.UnitY = "";
            graphControlDepthProfile.UpperPanelFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            graphControlDepthProfile.UpperPanelVisible = true;
            graphControlDepthProfile.UpperX = 1D;
            graphControlDepthProfile.UpperY = 1D;
            graphControlDepthProfile.UseLineWidth = true;
            graphControlDepthProfile.VerticalLineColor = System.Drawing.Color.Red;
            graphControlDepthProfile.XLog = false;
            graphControlDepthProfile.YLog = false;
            // 
            // numericBoxEnergyEnd
            // 
            numericBoxEnergyEnd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxEnergyEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyEnd.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxEnergyEnd.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyEnd.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyEnd.HeaderText = "to";
            numericBoxEnergyEnd.Location = new System.Drawing.Point(1393, 198);
            numericBoxEnergyEnd.Margin = new System.Windows.Forms.Padding(0);
            numericBoxEnergyEnd.Maximum = 1000D;
            numericBoxEnergyEnd.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxEnergyEnd.Minimum = 1D;
            numericBoxEnergyEnd.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxEnergyEnd.Name = "numericBoxEnergyEnd";
            numericBoxEnergyEnd.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxEnergyEnd.RadianValue = 0.33161255787892263D;
            numericBoxEnergyEnd.ShowUpDown = true;
            numericBoxEnergyEnd.Size = new System.Drawing.Size(75, 27);
            numericBoxEnergyEnd.SmartIncrement = true;
            numericBoxEnergyEnd.TabIndex = 134;
            numericBoxEnergyEnd.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxEnergyEnd.ThonsandsSeparator = true;
            numericBoxEnergyEnd.ToolTip = "Set a range and step of the sample thichnesses";
            numericBoxEnergyEnd.Value = 19D;
            numericBoxEnergyEnd.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // numericBoxEnergyStart
            // 
            numericBoxEnergyStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxEnergyStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxEnergyStart.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.HeaderText = "Energy from";
            numericBoxEnergyStart.Location = new System.Drawing.Point(1263, 198);
            numericBoxEnergyStart.Margin = new System.Windows.Forms.Padding(0);
            numericBoxEnergyStart.Maximum = 1000D;
            numericBoxEnergyStart.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxEnergyStart.Minimum = 1D;
            numericBoxEnergyStart.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxEnergyStart.Name = "numericBoxEnergyStart";
            numericBoxEnergyStart.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxEnergyStart.RadianValue = 0.3490658503988659D;
            numericBoxEnergyStart.ShowUpDown = true;
            numericBoxEnergyStart.Size = new System.Drawing.Size(130, 27);
            numericBoxEnergyStart.SmartIncrement = true;
            numericBoxEnergyStart.TabIndex = 133;
            numericBoxEnergyStart.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxEnergyStart.ThonsandsSeparator = true;
            numericBoxEnergyStart.ToolTip = "Set a range and step of the sample thichnesses";
            numericBoxEnergyStart.Value = 20D;
            numericBoxEnergyStart.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // numericBoxEnergyStep
            // 
            numericBoxEnergyStep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxEnergyStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStep.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxEnergyStep.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStep.FooterText = "kV";
            numericBoxEnergyStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStep.HeaderText = "with step of";
            numericBoxEnergyStep.Location = new System.Drawing.Point(1338, 225);
            numericBoxEnergyStep.Margin = new System.Windows.Forms.Padding(0);
            numericBoxEnergyStep.Maximum = 10D;
            numericBoxEnergyStep.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxEnergyStep.Minimum = 0.01D;
            numericBoxEnergyStep.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxEnergyStep.Name = "numericBoxEnergyStep";
            numericBoxEnergyStep.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxEnergyStep.RadianValue = 0.0034906585039886592D;
            numericBoxEnergyStep.ShowUpDown = true;
            numericBoxEnergyStep.Size = new System.Drawing.Size(149, 27);
            numericBoxEnergyStep.SmartIncrement = true;
            numericBoxEnergyStep.TabIndex = 135;
            numericBoxEnergyStep.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxEnergyStep.ThonsandsSeparator = true;
            numericBoxEnergyStep.ToolTip = "Set a range and step of the sample thichnesses";
            numericBoxEnergyStep.Value = 0.2D;
            numericBoxEnergyStep.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.BackColor = System.Drawing.Color.SteelBlue;
            button1.ForeColor = System.Drawing.Color.White;
            button1.Location = new System.Drawing.Point(851, 525);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(93, 26);
            button1.TabIndex = 129;
            button1.Text = "calc";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // buttonCopyEnergyProfile
            // 
            buttonCopyEnergyProfile.AutoSize = true;
            buttonCopyEnergyProfile.Location = new System.Drawing.Point(136, 539);
            buttonCopyEnergyProfile.Name = "buttonCopyEnergyProfile";
            buttonCopyEnergyProfile.Size = new System.Drawing.Size(81, 25);
            buttonCopyEnergyProfile.TabIndex = 113;
            buttonCopyEnergyProfile.Text = "Copy image";
            buttonCopyEnergyProfile.UseVisualStyleBackColor = true;
            buttonCopyEnergyProfile.Click += buttonCopyEnergyProfile_Click;
            // 
            // buttonDepthProfile
            // 
            buttonDepthProfile.AutoSize = true;
            buttonDepthProfile.Location = new System.Drawing.Point(559, 539);
            buttonDepthProfile.Name = "buttonDepthProfile";
            buttonDepthProfile.Size = new System.Drawing.Size(81, 25);
            buttonDepthProfile.TabIndex = 113;
            buttonDepthProfile.Text = "Copy image";
            buttonDepthProfile.UseVisualStyleBackColor = true;
            buttonDepthProfile.Click += buttonDepthProfile_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label13.Location = new System.Drawing.Point(702, 547);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(73, 17);
            label13.TabIndex = 121;
            label13.Text = "Depth (nm)";
            // 
            // FormEBSD
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1513, 809);
            Controls.Add(label13);
            Controls.Add(panelGeometry);
            Controls.Add(graphControlDepthProfile);
            Controls.Add(graphControlEnergyProfile);
            Controls.Add(groupBoxOutput);
            Controls.Add(buttonStop);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(numericBoxEnergyStep);
            Controls.Add(numericBoxThicknessStep);
            Controls.Add(numericBoxMaxNumOfG);
            Controls.Add(numericBoxEnergyStart);
            Controls.Add(numericBoxThicknessStart);
            Controls.Add(checkBoxDrawKikuchiLines);
            Controls.Add(numericBoxEnergyEnd);
            Controls.Add(numericBoxThicknessEnd);
            Controls.Add(numericBoxDiskDiameter);
            Controls.Add(groupBox);
            Controls.Add(button1);
            Controls.Add(buttonSimulateEBSD);
            Controls.Add(graphicsBox);
            Controls.Add(label2);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox5);
            Controls.Add(buttonDepthProfile);
            Controls.Add(buttonCopyEnergyProfile);
            Controls.Add(buttonSaveImage);
            Controls.Add(button2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(numericBoxYofDet);
            Controls.Add(numericBoxZofDet);
            Controls.Add(numericBoxDetRadius);
            Controls.Add(numericBoxDetTilt);
            Controls.Add(numericBoxSampleTilt);
            Controls.Add(waveLengthControl);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Name = "FormEBSD";
            Text = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            Load += FormEBSD_Load;
            VisibleChanged += FormEBSD_VisibleChanged;
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).EndInit();
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            groupBoxOutput.ResumeLayout(false);
            groupBoxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputEnergy).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).EndInit();
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
        private System.Windows.Forms.Button buttonViewFromSurfaceNormal;
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
        private System.Windows.Forms.Button buttonSimulateEBSD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private NumericBox numericBoxThicknessStep;
        private NumericBox numericBoxMaxNumOfG;
        private NumericBox numericBoxThicknessStart;
        private NumericBox numericBoxThicknessEnd;
        private NumericBox numericBoxDiskDiameter;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox comboBoxGradient;
        public System.Windows.Forms.ComboBox comboBoxScale;
        public System.Windows.Forms.TrackBar trackBarOutputThickness;
        private System.Windows.Forms.TrackBar trackBarIntensityBrightnessMax;
        private System.Windows.Forms.TrackBar trackBarIntensityBrightnessMin;
        public System.Windows.Forms.TextBox textBoxThickness;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxDrawKikuchiLines;
        private System.Windows.Forms.Button buttonSaveImage;
        private GraphControl graphControlEnergyProfile;
        private GraphControl graphControlDepthProfile;
        public System.Windows.Forms.TrackBar trackBarOutputEnergy;
        public System.Windows.Forms.TextBox textBoxEnergy;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private NumericBox numericBoxEnergyEnd;
        private NumericBox numericBoxEnergyStart;
        private NumericBox numericBoxEnergyStep;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonCopyEnergyProfile;
        private System.Windows.Forms.Button buttonDepthProfile;
        private System.Windows.Forms.Button buttonViewQuarter;
        private System.Windows.Forms.Label label13;
    }
}