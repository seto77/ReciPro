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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEBSD));
            waveLengthControl1 = new WaveLengthControl();
            buttonCalc = new System.Windows.Forms.Button();
            numericBoxSampleTilt = new NumericBox();
            buttonViewIsometric = new System.Windows.Forms.Button();
            buttonViewAlongBeam = new System.Windows.Forms.Button();
            paneltTrajectory = new System.Windows.Forms.Panel();
            graphControlEnergyProfile = new GraphControl();
            numericBoxCalcNum = new NumericBox();
            numericBoxDrawNum = new NumericBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            graphControlDepthProfile = new GraphControl();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            groupBox3 = new System.Windows.Forms.GroupBox();
            graphControlDepthEBSD = new GraphControl();
            groupBox4 = new System.Windows.Forms.GroupBox();
            graphControlDistance = new GraphControl();
            poleFigureControl = new PoleFigureControl2();
            checkBoxDrawAxes = new System.Windows.Forms.CheckBox();
            checkBoxDrawGuidCircles = new System.Windows.Forms.CheckBox();
            button1 = new System.Windows.Forms.Button();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxDrawAbsorved = new System.Windows.Forms.CheckBox();
            groupBox5 = new System.Windows.Forms.GroupBox();
            checkBoxDrawAxesInStereonet = new System.Windows.Forms.CheckBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            labelBSEenergy = new System.Windows.Forms.Label();
            labelBSEratio = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            checkBoxDrawPathAfterEscape = new System.Windows.Forms.CheckBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            labelMeanFreePath = new System.Windows.Forms.Label();
            labelStoppingPower = new System.Windows.Forms.Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // waveLengthControl1
            // 
            resources.ApplyResources(waveLengthControl1, "waveLengthControl1");
            waveLengthControl1.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl1.Energy = 20D;
            waveLengthControl1.Monochrome = true;
            waveLengthControl1.Name = "waveLengthControl1";
            waveLengthControl1.ShowWaveSource = false;
            waveLengthControl1.WaveLength = 0.0085885141045000009D;
            waveLengthControl1.WaveSource = WaveSource.Electron;
            waveLengthControl1.XrayWaveSourceElementNumber = 0;
            waveLengthControl1.XrayWaveSourceLine = XrayLine.Ka1;
            // 
            // buttonCalc
            // 
            resources.ApplyResources(buttonCalc, "buttonCalc");
            buttonCalc.BackColor = System.Drawing.Color.SteelBlue;
            buttonCalc.ForeColor = System.Drawing.Color.White;
            buttonCalc.Name = "buttonCalc";
            buttonCalc.UseVisualStyleBackColor = false;
            buttonCalc.Click += buttonCaluculate_Click;
            // 
            // numericBoxSampleTilt
            // 
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxSampleTilt, "numericBoxSampleTilt");
            numericBoxSampleTilt.Maximum = 90D;
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.RadianValue = 1.2217304763960306D;
            numericBoxSampleTilt.RoundErrorAccuracy = -1;
            numericBoxSampleTilt.ShowUpDown = true;
            numericBoxSampleTilt.Value = 70D;
            // 
            // buttonViewIsometric
            // 
            resources.ApplyResources(buttonViewIsometric, "buttonViewIsometric");
            buttonViewIsometric.Name = "buttonViewIsometric";
            buttonViewIsometric.UseVisualStyleBackColor = true;
            buttonViewIsometric.Click += buttonViewIsometric_Click;
            // 
            // buttonViewAlongBeam
            // 
            resources.ApplyResources(buttonViewAlongBeam, "buttonViewAlongBeam");
            buttonViewAlongBeam.Name = "buttonViewAlongBeam";
            buttonViewAlongBeam.UseVisualStyleBackColor = true;
            buttonViewAlongBeam.Click += buttonViewAlongBeam_Click;
            // 
            // paneltTrajectory
            // 
            resources.ApplyResources(paneltTrajectory, "paneltTrajectory");
            paneltTrajectory.Name = "paneltTrajectory";
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
            resources.ApplyResources(graphControlEnergyProfile, "graphControlEnergyProfile");
            graphControlEnergyProfile.DrawingRange = (RectangleD)resources.GetObject("graphControlEnergyProfile.DrawingRange");
            graphControlEnergyProfile.FixRangeHorizontal = false;
            graphControlEnergyProfile.FixRangeVertical = false;
            graphControlEnergyProfile.GraphTitle = "";
            graphControlEnergyProfile.Interpolation = false;
            graphControlEnergyProfile.IsIntegerX = false;
            graphControlEnergyProfile.IsIntegerY = false;
            graphControlEnergyProfile.LabelX = "kev: ";
            graphControlEnergyProfile.LabelY = "freq.: ";
            graphControlEnergyProfile.LeftMargin = 0F;
            graphControlEnergyProfile.LineWidth = 1F;
            graphControlEnergyProfile.LowerX = 0D;
            graphControlEnergyProfile.LowerY = 0D;
            graphControlEnergyProfile.MaximalX = 1D;
            graphControlEnergyProfile.MaximalY = 1D;
            graphControlEnergyProfile.MinimalX = 0D;
            graphControlEnergyProfile.MinimalY = 0D;
            graphControlEnergyProfile.Mode = GraphControl.DrawingMode.Histogram;
            graphControlEnergyProfile.MousePositionVisible = true;
            graphControlEnergyProfile.MousePositionXDigit = 3;
            graphControlEnergyProfile.MousePositionYDigit = 3;
            graphControlEnergyProfile.Name = "graphControlEnergyProfile";
            graphControlEnergyProfile.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlEnergyProfile.Profile = null;
            graphControlEnergyProfile.Smoothing = false;
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
            // numericBoxCalcNum
            // 
            numericBoxCalcNum.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalcNum.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxCalcNum, "numericBoxCalcNum");
            numericBoxCalcNum.Maximum = 1000000D;
            numericBoxCalcNum.Minimum = 100D;
            numericBoxCalcNum.Name = "numericBoxCalcNum";
            numericBoxCalcNum.RadianValue = 872.66462599716476D;
            numericBoxCalcNum.RoundErrorAccuracy = -1;
            numericBoxCalcNum.ShowUpDown = true;
            numericBoxCalcNum.SmartIncrement = true;
            numericBoxCalcNum.Value = 50000D;
            // 
            // numericBoxDrawNum
            // 
            numericBoxDrawNum.BackColor = System.Drawing.Color.Transparent;
            numericBoxDrawNum.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxDrawNum, "numericBoxDrawNum");
            numericBoxDrawNum.Maximum = 100000D;
            numericBoxDrawNum.Minimum = 100D;
            numericBoxDrawNum.Name = "numericBoxDrawNum";
            numericBoxDrawNum.RadianValue = 8.7266462599716466D;
            numericBoxDrawNum.RoundErrorAccuracy = -1;
            numericBoxDrawNum.ShowUpDown = true;
            numericBoxDrawNum.SmartIncrement = true;
            numericBoxDrawNum.Value = 500D;
            numericBoxDrawNum.ValueChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(graphControlEnergyProfile);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(graphControlDepthProfile);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
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
            resources.ApplyResources(graphControlDepthProfile, "graphControlDepthProfile");
            graphControlDepthProfile.DrawingRange = (RectangleD)resources.GetObject("graphControlDepthProfile.DrawingRange");
            graphControlDepthProfile.FixRangeHorizontal = false;
            graphControlDepthProfile.FixRangeVertical = false;
            graphControlDepthProfile.GraphTitle = "";
            graphControlDepthProfile.Interpolation = false;
            graphControlDepthProfile.IsIntegerX = false;
            graphControlDepthProfile.IsIntegerY = false;
            graphControlDepthProfile.LabelX = "µm: ";
            graphControlDepthProfile.LabelY = "freq.:";
            graphControlDepthProfile.LeftMargin = 0F;
            graphControlDepthProfile.LineWidth = 1F;
            graphControlDepthProfile.LowerX = 0D;
            graphControlDepthProfile.LowerY = 0D;
            graphControlDepthProfile.MaximalX = 1D;
            graphControlDepthProfile.MaximalY = 1D;
            graphControlDepthProfile.MinimalX = 0D;
            graphControlDepthProfile.MinimalY = 0D;
            graphControlDepthProfile.Mode = GraphControl.DrawingMode.Histogram;
            graphControlDepthProfile.MousePositionVisible = true;
            graphControlDepthProfile.MousePositionXDigit = 3;
            graphControlDepthProfile.MousePositionYDigit = 3;
            graphControlDepthProfile.Name = "graphControlDepthProfile";
            graphControlDepthProfile.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlDepthProfile.Profile = null;
            graphControlDepthProfile.Smoothing = false;
            graphControlDepthProfile.UnitX = "µm";
            graphControlDepthProfile.UnitY = "%";
            graphControlDepthProfile.UpperPanelFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            graphControlDepthProfile.UpperPanelVisible = true;
            graphControlDepthProfile.UpperX = 1D;
            graphControlDepthProfile.UpperY = 1D;
            graphControlDepthProfile.UseLineWidth = true;
            graphControlDepthProfile.VerticalLineColor = System.Drawing.Color.Red;
            graphControlDepthProfile.XLog = false;
            graphControlDepthProfile.YLog = false;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1 });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(graphControlDepthEBSD);
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // graphControlDepthEBSD
            // 
            graphControlDepthEBSD.AllowMouseOperation = true;
            graphControlDepthEBSD.AxisLineColor = System.Drawing.Color.Gray;
            graphControlDepthEBSD.AxisTextColor = System.Drawing.Color.Black;
            graphControlDepthEBSD.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControlDepthEBSD.AxisXTextVisible = true;
            graphControlDepthEBSD.AxisYTextVisible = true;
            graphControlDepthEBSD.BackgroundColor = System.Drawing.Color.White;
            graphControlDepthEBSD.BottomMargin = 0D;
            graphControlDepthEBSD.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlDepthEBSD.DivisionLineXVisible = true;
            graphControlDepthEBSD.DivisionLineYVisible = true;
            resources.ApplyResources(graphControlDepthEBSD, "graphControlDepthEBSD");
            graphControlDepthEBSD.DrawingRange = (RectangleD)resources.GetObject("graphControlDepthEBSD.DrawingRange");
            graphControlDepthEBSD.FixRangeHorizontal = false;
            graphControlDepthEBSD.FixRangeVertical = false;
            graphControlDepthEBSD.GraphTitle = "";
            graphControlDepthEBSD.Interpolation = false;
            graphControlDepthEBSD.IsIntegerX = false;
            graphControlDepthEBSD.IsIntegerY = false;
            graphControlDepthEBSD.LabelX = "nm: ";
            graphControlDepthEBSD.LabelY = "freq.:";
            graphControlDepthEBSD.LeftMargin = 0F;
            graphControlDepthEBSD.LineWidth = 1F;
            graphControlDepthEBSD.LowerX = 0D;
            graphControlDepthEBSD.LowerY = 0D;
            graphControlDepthEBSD.MaximalX = 1D;
            graphControlDepthEBSD.MaximalY = 1D;
            graphControlDepthEBSD.MinimalX = 0D;
            graphControlDepthEBSD.MinimalY = 0D;
            graphControlDepthEBSD.Mode = GraphControl.DrawingMode.Histogram;
            graphControlDepthEBSD.MousePositionVisible = true;
            graphControlDepthEBSD.MousePositionXDigit = 3;
            graphControlDepthEBSD.MousePositionYDigit = 3;
            graphControlDepthEBSD.Name = "graphControlDepthEBSD";
            graphControlDepthEBSD.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlDepthEBSD.Profile = null;
            graphControlDepthEBSD.Smoothing = false;
            graphControlDepthEBSD.UnitX = "nm";
            graphControlDepthEBSD.UnitY = "%";
            graphControlDepthEBSD.UpperPanelFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            graphControlDepthEBSD.UpperPanelVisible = true;
            graphControlDepthEBSD.UpperX = 1D;
            graphControlDepthEBSD.UpperY = 1D;
            graphControlDepthEBSD.UseLineWidth = true;
            graphControlDepthEBSD.VerticalLineColor = System.Drawing.Color.Red;
            graphControlDepthEBSD.XLog = false;
            graphControlDepthEBSD.YLog = false;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(graphControlDistance);
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            // 
            // graphControlDistance
            // 
            graphControlDistance.AllowMouseOperation = true;
            graphControlDistance.AxisLineColor = System.Drawing.Color.Gray;
            graphControlDistance.AxisTextColor = System.Drawing.Color.Black;
            graphControlDistance.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControlDistance.AxisXTextVisible = true;
            graphControlDistance.AxisYTextVisible = true;
            graphControlDistance.BackgroundColor = System.Drawing.Color.White;
            graphControlDistance.BottomMargin = 0D;
            graphControlDistance.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlDistance.DivisionLineXVisible = true;
            graphControlDistance.DivisionLineYVisible = true;
            resources.ApplyResources(graphControlDistance, "graphControlDistance");
            graphControlDistance.DrawingRange = (RectangleD)resources.GetObject("graphControlDistance.DrawingRange");
            graphControlDistance.FixRangeHorizontal = false;
            graphControlDistance.FixRangeVertical = false;
            graphControlDistance.GraphTitle = "";
            graphControlDistance.Interpolation = false;
            graphControlDistance.IsIntegerX = false;
            graphControlDistance.IsIntegerY = false;
            graphControlDistance.LabelX = "µm: ";
            graphControlDistance.LabelY = "freq.:";
            graphControlDistance.LeftMargin = 0F;
            graphControlDistance.LineWidth = 1F;
            graphControlDistance.LowerX = 0D;
            graphControlDistance.LowerY = 0D;
            graphControlDistance.MaximalX = 1D;
            graphControlDistance.MaximalY = 1D;
            graphControlDistance.MinimalX = 0D;
            graphControlDistance.MinimalY = 0D;
            graphControlDistance.Mode = GraphControl.DrawingMode.Histogram;
            graphControlDistance.MousePositionVisible = true;
            graphControlDistance.MousePositionXDigit = 3;
            graphControlDistance.MousePositionYDigit = 3;
            graphControlDistance.Name = "graphControlDistance";
            graphControlDistance.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlDistance.Profile = null;
            graphControlDistance.Smoothing = false;
            graphControlDistance.UnitX = "nm";
            graphControlDistance.UnitY = "%";
            graphControlDistance.UpperPanelFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            graphControlDistance.UpperPanelVisible = true;
            graphControlDistance.UpperX = 1D;
            graphControlDistance.UpperY = 1D;
            graphControlDistance.UseLineWidth = true;
            graphControlDistance.VerticalLineColor = System.Drawing.Color.Red;
            graphControlDistance.XLog = false;
            graphControlDistance.YLog = false;
            // 
            // poleFigureControl
            // 
            resources.ApplyResources(poleFigureControl, "poleFigureControl");
            poleFigureControl.Name = "poleFigureControl";
            poleFigureControl.Vectors = null;
            // 
            // checkBoxDrawAxes
            // 
            resources.ApplyResources(checkBoxDrawAxes, "checkBoxDrawAxes");
            checkBoxDrawAxes.Checked = true;
            checkBoxDrawAxes.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxes.Name = "checkBoxDrawAxes";
            checkBoxDrawAxes.UseVisualStyleBackColor = true;
            checkBoxDrawAxes.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // checkBoxDrawGuidCircles
            // 
            resources.ApplyResources(checkBoxDrawGuidCircles, "checkBoxDrawGuidCircles");
            checkBoxDrawGuidCircles.Checked = true;
            checkBoxDrawGuidCircles.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawGuidCircles.Name = "checkBoxDrawGuidCircles";
            checkBoxDrawGuidCircles.UseVisualStyleBackColor = true;
            checkBoxDrawGuidCircles.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(buttonViewAlongBeam);
            flowLayoutPanel1.Controls.Add(buttonViewIsometric);
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(groupBox1);
            flowLayoutPanel2.Controls.Add(groupBox4);
            flowLayoutPanel2.Controls.Add(groupBox2);
            flowLayoutPanel2.Controls.Add(groupBox3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // checkBoxDrawAbsorved
            // 
            resources.ApplyResources(checkBoxDrawAbsorved, "checkBoxDrawAbsorved");
            checkBoxDrawAbsorved.Name = "checkBoxDrawAbsorved";
            checkBoxDrawAbsorved.UseVisualStyleBackColor = true;
            checkBoxDrawAbsorved.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(poleFigureControl);
            groupBox5.Controls.Add(checkBoxDrawAxesInStereonet);
            resources.ApplyResources(groupBox5, "groupBox5");
            groupBox5.Name = "groupBox5";
            groupBox5.TabStop = false;
            // 
            // checkBoxDrawAxesInStereonet
            // 
            resources.ApplyResources(checkBoxDrawAxesInStereonet, "checkBoxDrawAxesInStereonet");
            checkBoxDrawAxesInStereonet.Checked = true;
            checkBoxDrawAxesInStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxesInStereonet.Name = "checkBoxDrawAxesInStereonet";
            checkBoxDrawAxesInStereonet.UseVisualStyleBackColor = true;
            checkBoxDrawAxesInStereonet.CheckedChanged += checkBoxDrawAxesInStereonet_CheckedChanged;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(labelBSEenergy);
            groupBox6.Controls.Add(labelStoppingPower);
            groupBox6.Controls.Add(labelMeanFreePath);
            groupBox6.Controls.Add(labelBSEratio);
            groupBox6.Controls.Add(label3);
            groupBox6.Controls.Add(label5);
            groupBox6.Controls.Add(label4);
            groupBox6.Controls.Add(label2);
            resources.ApplyResources(groupBox6, "groupBox6");
            groupBox6.Name = "groupBox6";
            groupBox6.TabStop = false;
            // 
            // labelBSEenergy
            // 
            resources.ApplyResources(labelBSEenergy, "labelBSEenergy");
            labelBSEenergy.Name = "labelBSEenergy";
            // 
            // labelBSEratio
            // 
            resources.ApplyResources(labelBSEratio, "labelBSEratio");
            labelBSEratio.Name = "labelBSEratio";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // checkBoxDrawPathAfterEscape
            // 
            resources.ApplyResources(checkBoxDrawPathAfterEscape, "checkBoxDrawPathAfterEscape");
            checkBoxDrawPathAfterEscape.Checked = true;
            checkBoxDrawPathAfterEscape.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawPathAfterEscape.Name = "checkBoxDrawPathAfterEscape";
            checkBoxDrawPathAfterEscape.UseVisualStyleBackColor = true;
            checkBoxDrawPathAfterEscape.CheckedChanged += checkBoxDrawAxes_CheckedChanged;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // labelMeanFreePath
            // 
            resources.ApplyResources(labelMeanFreePath, "labelMeanFreePath");
            labelMeanFreePath.Name = "labelMeanFreePath";
            // 
            // labelStoppingPower
            // 
            resources.ApplyResources(labelStoppingPower, "labelStoppingPower");
            labelStoppingPower.Name = "labelStoppingPower";
            // 
            // FormEBSD
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(numericBoxCalcNum);
            Controls.Add(label1);
            Controls.Add(numericBoxSampleTilt);
            Controls.Add(waveLengthControl1);
            Controls.Add(checkBoxDrawPathAfterEscape);
            Controls.Add(checkBoxDrawAbsorved);
            Controls.Add(checkBoxDrawGuidCircles);
            Controls.Add(checkBoxDrawAxes);
            Controls.Add(numericBoxDrawNum);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(buttonCalc);
            Controls.Add(statusStrip1);
            Controls.Add(paneltTrajectory);
            Name = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            Load += FormEBSD_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WaveLengthControl waveLengthControl1;
        private System.Windows.Forms.Button buttonCalc;
        private NumericBox numericBoxSampleTilt;
        private System.Windows.Forms.Button buttonViewIsometric;
        private System.Windows.Forms.Button buttonViewAlongBeam;
        private System.Windows.Forms.Panel paneltTrajectory;
        private GraphControl graphControlEnergyProfile;
        private NumericBox numericBoxCalcNum;
        private NumericBox numericBoxDrawNum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private GraphControl graphControlDepthProfile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private GraphControl graphControlDepthEBSD;
        private System.Windows.Forms.GroupBox groupBox4;
        private GraphControl graphControlDistance;
        private PoleFigureControl2 poleFigureControl;
        private System.Windows.Forms.Panel panelAxes;
        private System.Windows.Forms.CheckBox checkBoxDrawAxes;
        private System.Windows.Forms.CheckBox checkBoxDrawGuidCircles;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.CheckBox checkBoxDrawAbsorved;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBoxDrawAxesInStereonet;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelBSEratio;
        private System.Windows.Forms.Label labelBSEenergy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxDrawPathAfterEscape;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelMeanFreePath;
        private System.Windows.Forms.Label labelStoppingPower;
    }
}