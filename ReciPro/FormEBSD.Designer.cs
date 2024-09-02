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
            panelGeometry = new System.Windows.Forms.Panel();
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
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
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
            // panelGeometry
            // 
            resources.ApplyResources(panelGeometry, "panelGeometry");
            panelGeometry.Name = "panelGeometry";
            // 
            // buttonCalc
            // 
            resources.ApplyResources(buttonCalc, "buttonCalc");
            buttonCalc.Name = "buttonCalc";
            buttonCalc.UseVisualStyleBackColor = true;
            buttonCalc.Click += button1_Click;
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
            numericBoxSampleTilt.ValueChanged += numericBoxSampleTilt_ValueChanged;
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
            // FormEBSD
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(statusStrip1);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(numericBoxDrawNum);
            Controls.Add(numericBoxCalcNum);
            Controls.Add(buttonViewIsometric);
            Controls.Add(buttonViewAlongBeam);
            Controls.Add(numericBoxSampleTilt);
            Controls.Add(buttonCalc);
            Controls.Add(paneltTrajectory);
            Controls.Add(panelGeometry);
            Controls.Add(waveLengthControl1);
            Name = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            Load += FormEBSD_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WaveLengthControl waveLengthControl1;
        private System.Windows.Forms.Panel panelGeometry;
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
    }
}