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
            button1 = new System.Windows.Forms.Button();
            numericBoxSampleTilt = new NumericBox();
            buttonViewIsometric = new System.Windows.Forms.Button();
            buttonViewAlongBeam = new System.Windows.Forms.Button();
            paneltTrajectory = new System.Windows.Forms.Panel();
            graphControl1 = new GraphControl();
            numericBoxCalcNum = new NumericBox();
            numericBoxDrawNum = new NumericBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            graphControl2 = new GraphControl();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            statusStrip1.SuspendLayout();
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
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
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
            // graphControl1
            // 
            graphControl1.AllowMouseOperation = true;
            graphControl1.AxisLineColor = System.Drawing.Color.Gray;
            graphControl1.AxisTextColor = System.Drawing.Color.Black;
            graphControl1.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControl1.AxisXTextVisible = true;
            graphControl1.AxisYTextVisible = true;
            graphControl1.BackgroundColor = System.Drawing.Color.White;
            graphControl1.BottomMargin = 0D;
            graphControl1.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControl1.DivisionLineXVisible = true;
            graphControl1.DivisionLineYVisible = true;
            resources.ApplyResources(graphControl1, "graphControl1");
            graphControl1.DrawingRange = (RectangleD)resources.GetObject("graphControl1.DrawingRange");
            graphControl1.FixRangeHorizontal = false;
            graphControl1.FixRangeVertical = false;
            graphControl1.GraphTitle = "";
            graphControl1.Interpolation = false;
            graphControl1.IsIntegerX = false;
            graphControl1.IsIntegerY = false;
            graphControl1.LabelX = "X:";
            graphControl1.LabelY = "Y:";
            graphControl1.LeftMargin = 0F;
            graphControl1.LineWidth = 1F;
            graphControl1.LowerX = 0D;
            graphControl1.LowerY = 0D;
            graphControl1.MaximalX = 1D;
            graphControl1.MaximalY = 1D;
            graphControl1.MinimalX = 0D;
            graphControl1.MinimalY = 0D;
            graphControl1.Mode = GraphControl.DrawingMode.Histogram;
            graphControl1.MousePositionVisible = true;
            graphControl1.MousePositionXDigit = -1;
            graphControl1.MousePositionYDigit = -1;
            graphControl1.Name = "graphControl1";
            graphControl1.OriginPosition = new System.Drawing.Point(40, 20);
            graphControl1.Profile = null;
            graphControl1.Smoothing = false;
            graphControl1.UnitX = "";
            graphControl1.UnitY = "";
            graphControl1.UpperPanelFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            graphControl1.UpperPanelVisible = true;
            graphControl1.UpperX = 1D;
            graphControl1.UpperY = 1D;
            graphControl1.UseLineWidth = true;
            graphControl1.VerticalLineColor = System.Drawing.Color.Red;
            graphControl1.XLog = false;
            graphControl1.YLog = false;
            // 
            // numericBoxCalcNum
            // 
            numericBoxCalcNum.BackColor = System.Drawing.Color.Transparent;
            numericBoxCalcNum.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxCalcNum, "numericBoxCalcNum");
            numericBoxCalcNum.Maximum = 100000D;
            numericBoxCalcNum.Minimum = 100D;
            numericBoxCalcNum.Name = "numericBoxCalcNum";
            numericBoxCalcNum.RadianValue = 174.53292519943295D;
            numericBoxCalcNum.RoundErrorAccuracy = -1;
            numericBoxCalcNum.ShowUpDown = true;
            numericBoxCalcNum.SmartIncrement = true;
            numericBoxCalcNum.Value = 10000D;
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
            groupBox1.Controls.Add(graphControl1);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(graphControl2);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // graphControl2
            // 
            graphControl2.AllowMouseOperation = true;
            graphControl2.AxisLineColor = System.Drawing.Color.Gray;
            graphControl2.AxisTextColor = System.Drawing.Color.Black;
            graphControl2.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControl2.AxisXTextVisible = true;
            graphControl2.AxisYTextVisible = true;
            graphControl2.BackgroundColor = System.Drawing.Color.White;
            graphControl2.BottomMargin = 0D;
            graphControl2.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControl2.DivisionLineXVisible = true;
            graphControl2.DivisionLineYVisible = true;
            resources.ApplyResources(graphControl2, "graphControl2");
            graphControl2.DrawingRange = (RectangleD)resources.GetObject("graphControl2.DrawingRange");
            graphControl2.FixRangeHorizontal = false;
            graphControl2.FixRangeVertical = false;
            graphControl2.GraphTitle = "";
            graphControl2.Interpolation = false;
            graphControl2.IsIntegerX = false;
            graphControl2.IsIntegerY = false;
            graphControl2.LabelX = "X:";
            graphControl2.LabelY = "Y:";
            graphControl2.LeftMargin = 0F;
            graphControl2.LineWidth = 1F;
            graphControl2.LowerX = 0D;
            graphControl2.LowerY = 0D;
            graphControl2.MaximalX = 1D;
            graphControl2.MaximalY = 1D;
            graphControl2.MinimalX = 0D;
            graphControl2.MinimalY = 0D;
            graphControl2.Mode = GraphControl.DrawingMode.Histogram;
            graphControl2.MousePositionVisible = true;
            graphControl2.MousePositionXDigit = -1;
            graphControl2.MousePositionYDigit = -1;
            graphControl2.Name = "graphControl2";
            graphControl2.OriginPosition = new System.Drawing.Point(40, 20);
            graphControl2.Profile = null;
            graphControl2.Smoothing = false;
            graphControl2.UnitX = "";
            graphControl2.UnitY = "";
            graphControl2.UpperPanelFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            graphControl2.UpperPanelVisible = true;
            graphControl2.UpperX = 1D;
            graphControl2.UpperY = 1D;
            graphControl2.UseLineWidth = true;
            graphControl2.VerticalLineColor = System.Drawing.Color.Red;
            graphControl2.XLog = false;
            graphControl2.YLog = false;
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
            // FormEBSD
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(statusStrip1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(numericBoxDrawNum);
            Controls.Add(numericBoxCalcNum);
            Controls.Add(buttonViewIsometric);
            Controls.Add(buttonViewAlongBeam);
            Controls.Add(numericBoxSampleTilt);
            Controls.Add(button1);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private WaveLengthControl waveLengthControl1;
        private System.Windows.Forms.Panel panelGeometry;
        private System.Windows.Forms.Button button1;
        private NumericBox numericBoxSampleTilt;
        private System.Windows.Forms.Button buttonViewIsometric;
        private System.Windows.Forms.Button buttonViewAlongBeam;
        private System.Windows.Forms.Panel paneltTrajectory;
        private GraphControl graphControl1;
        private NumericBox numericBoxCalcNum;
        private NumericBox numericBoxDrawNum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private GraphControl graphControl2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}