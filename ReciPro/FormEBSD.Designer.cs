namespace ReciPro
{
    partial class FormEBSD
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
        // (260323Ch) renamed numeric container controls:
        // groupBox1 -> groupBoxSimulationParameters
        // groupBox2 -> groupBoxLatticePlanes
        // groupBox3 -> groupBoxMasterPattern
        // groupBox4 -> groupBoxSampleCondition
        // groupBox5 -> groupBoxEBSDGeometry
        // groupBox6 -> groupBoxEBSDPattern
        // flowLayoutPanel1 -> flowLayoutPanelViewAlong
        // flowLayoutPanel4 -> flowLayoutPanelOutputRange
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEBSD));
            panelGeometry = new System.Windows.Forms.Panel();
            numericBoxSampleTilt = new NumericBox();
            waveLengthControl = new WaveLengthControl();
            flowLayoutPanelViewAlong = new System.Windows.Forms.FlowLayoutPanel();
            buttonViewQuarter = new System.Windows.Forms.Button();
            buttonViewFromSurfaceNormal = new System.Windows.Forms.Button();
            buttonFromX = new System.Windows.Forms.Button();
            buttonViewFromZ = new System.Windows.Forms.Button();
            buttonCalcBSE = new System.Windows.Forms.Button();
            buttonFitNistElasticSampler = new System.Windows.Forms.Button();
            graphControlDepthProfile = new GraphControl();
            poleFigureControl = new PoleFigureControl2();
            graphControlEnergyProfile = new GraphControl();
            checkBoxDrawAxesInStereonet = new System.Windows.Forms.CheckBox();
            numericBoxDetTilt = new NumericBox();
            numericBoxDetRadius = new NumericBox();
            numericBoxZofDet = new NumericBox();
            numericBoxYofDet = new NumericBox();
            label2 = new System.Windows.Forms.Label();
            graphicsBox = new GraphicsBox(components);
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
            label1 = new System.Windows.Forms.Label();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxThicknessStep = new NumericBox();
            numericBoxMaxNumOfG = new NumericBox();
            checkBoxNonLocalAbsorption = new System.Windows.Forms.CheckBox();
            checkBoxTDSBackground = new System.Windows.Forms.CheckBox();
            numericBoxThicknessStart = new NumericBox();
            numericBoxThicknessEnd = new NumericBox();
            buttonStop = new System.Windows.Forms.Button();
            groupBoxOutput = new System.Windows.Forms.GroupBox();
            flowLayoutPanelOutputRange = new System.Windows.Forms.FlowLayoutPanel();
            label9 = new System.Windows.Forms.Label();
            textBoxEnergy = new System.Windows.Forms.TextBox();
            label12 = new System.Windows.Forms.Label();
            trackBarOutputEnergy = new System.Windows.Forms.TrackBar();
            label5 = new System.Windows.Forms.Label();
            textBoxThickness = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            trackBarOutputThickness = new System.Windows.Forms.TrackBar();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            checkBoxWithBSEDistribution = new System.Windows.Forms.CheckBox();
            comboBoxGradient = new System.Windows.Forms.ComboBox();
            comboBoxScale = new System.Windows.Forms.ComboBox();
            trackBarIntensityBrightnessMax = new System.Windows.Forms.TrackBar();
            trackBarIntensityBrightnessMin = new System.Windows.Forms.TrackBar();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            checkBoxShowOverlays = new System.Windows.Forms.CheckBox();
            buttonSaveImage = new System.Windows.Forms.Button();
            numericBoxEnergyEnd = new NumericBox();
            numericBoxEnergyStart = new NumericBox();
            numericBoxEnergyStep = new NumericBox();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            checkBoxShowDyanmicalEBSD = new System.Windows.Forms.CheckBox();
            checkBoxDrawDetectorOutline = new System.Windows.Forms.CheckBox();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            groupBoxEBSDGeometry = new System.Windows.Forms.GroupBox();
            label17 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            groupBoxSampleCondition = new System.Windows.Forms.GroupBox();
            tabPage2 = new System.Windows.Forms.TabPage();
            label15 = new System.Windows.Forms.Label();
            tabPage3 = new System.Windows.Forms.TabPage();
            groupBoxDetectorOutline = new System.Windows.Forms.GroupBox();
            checkBoxShowMesh = new System.Windows.Forms.CheckBox();
            checkBoxShowCircle = new System.Windows.Forms.CheckBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            checkBoxShowKikuchiLines = new System.Windows.Forms.CheckBox();
            groupBoxLatticePlanes = new System.Windows.Forms.GroupBox();
            groupBoxKikuchiLines = new System.Windows.Forms.GroupBox();
            checkBoxShowGIndices = new System.Windows.Forms.CheckBox();
            checkBoxShowZoneAxisIndices = new System.Windows.Forms.CheckBox();
            groupBoxSimulationParameters = new System.Windows.Forms.GroupBox();
            labelMasterPatternGrid = new System.Windows.Forms.Label();
            comboBoxMasterPatternGrid = new System.Windows.Forms.ComboBox();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            scalablePictureBoxAdvancedMasterPattern2D = new ScalablePictureBoxAdvanced();
            flowLayoutPanelMasterPatternSelectors = new System.Windows.Forms.FlowLayoutPanel();
            labelMasterPatternEnergy = new System.Windows.Forms.Label();
            textBoxMasterPatternEnergy = new System.Windows.Forms.TextBox();
            labelMasterPatternEnergyUnit = new System.Windows.Forms.Label();
            trackBarMasterPatternEnergy = new System.Windows.Forms.TrackBar();
            labelMasterPatternDepth = new System.Windows.Forms.Label();
            textBoxMasterPatternDepth = new System.Windows.Forms.TextBox();
            labelMasterPatternDepthUnit = new System.Windows.Forms.Label();
            trackBarMasterPatternDepth = new System.Windows.Forms.TrackBar();
            labelMasterPattern2DHemisphere = new System.Windows.Forms.Label();
            comboBoxMasterPattern2DHemisphere = new System.Windows.Forms.ComboBox();
            buttonCreateMasterPattern = new System.Windows.Forms.Button();
            panelMasterPattern3D = new System.Windows.Forms.Panel();
            panelMasterPattern3DAxes = new System.Windows.Forms.Panel();
            groupBoxMasterPattern = new System.Windows.Forms.GroupBox();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            buttonMasterPattern2DCopy = new System.Windows.Forms.Button();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            buttonMasterPattern3DCopy = new System.Windows.Forms.Button();
            checkBoxMasterPattern3DAxisLabel = new System.Windows.Forms.CheckBox();
            checkBoxMasterPattern3DAxisArrows = new System.Windows.Forms.CheckBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelMasterPattern3DViewAlong = new System.Windows.Forms.FlowLayoutPanel();
            buttonMasterPattern3DViewAlong = new System.Windows.Forms.Button();
            labelMasterPattern3DViewAlongOpenBracket = new System.Windows.Forms.Label();
            numericBoxMasterPattern3DViewAlongU = new NumericBox();
            numericBoxMasterPattern3DViewAlongV = new NumericBox();
            numericBoxMasterPattern3DViewAlongW = new NumericBox();
            labelMasterPattern3DViewAlongCloseBracket = new System.Windows.Forms.Label();
            groupBoxEBSDPattern = new System.Windows.Forms.GroupBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            flowLayoutPanelViewAlong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).BeginInit();
            groupBoxOutput.SuspendLayout();
            flowLayoutPanelOutputRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputEnergy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBoxEBSDGeometry.SuspendLayout();
            groupBoxSampleCondition.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            groupBoxDetectorOutline.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBoxLatticePlanes.SuspendLayout();
            groupBoxKikuchiLines.SuspendLayout();
            groupBoxSimulationParameters.SuspendLayout();
            statusStrip1.SuspendLayout();
            flowLayoutPanelMasterPatternSelectors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternEnergy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternDepth).BeginInit();
            panelMasterPattern3D.SuspendLayout();
            groupBoxMasterPattern.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanelMasterPattern3DViewAlong.SuspendLayout();
            groupBoxEBSDPattern.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelGeometry
            // 
            resources.ApplyResources(panelGeometry, "panelGeometry");
            panelGeometry.Name = "panelGeometry";
            // 
            // numericBoxSampleTilt
            // 
            resources.ApplyResources(numericBoxSampleTilt, "numericBoxSampleTilt");
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            numericBoxSampleTilt.Maximum = 0D;
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.RadianValue = -1.2217304763960306D;
            numericBoxSampleTilt.ShowUpDown = true;
            numericBoxSampleTilt.UpDown_Increment = 10D;
            numericBoxSampleTilt.Value = -70D;
            numericBoxSampleTilt.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            waveLengthControl.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl.Energy = 20D;
            waveLengthControl.Monochrome = true;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = false;
            waveLengthControl.WaveLength = 0.0085885141045000009D;
            waveLengthControl.WaveSource = WaveSource.Electron;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            waveLengthControl.XrayWaveSourceLine = XrayLine.Ka1;
            waveLengthControl.WavelengthChanged += waveLengthControl_WavelengthChanged;
            // 
            // flowLayoutPanelViewAlong
            // 
            resources.ApplyResources(flowLayoutPanelViewAlong, "flowLayoutPanelViewAlong");
            flowLayoutPanelViewAlong.Controls.Add(buttonViewQuarter);
            flowLayoutPanelViewAlong.Controls.Add(buttonViewFromSurfaceNormal);
            flowLayoutPanelViewAlong.Controls.Add(buttonFromX);
            flowLayoutPanelViewAlong.Controls.Add(buttonViewFromZ);
            flowLayoutPanelViewAlong.Name = "flowLayoutPanelViewAlong";
            // 
            // buttonViewQuarter
            // 
            resources.ApplyResources(buttonViewQuarter, "buttonViewQuarter");
            buttonViewQuarter.Name = "buttonViewQuarter";
            buttonViewQuarter.UseVisualStyleBackColor = true;
            buttonViewQuarter.Click += buttonViewQuarter_Click;
            // 
            // buttonViewFromSurfaceNormal
            // 
            resources.ApplyResources(buttonViewFromSurfaceNormal, "buttonViewFromSurfaceNormal");
            buttonViewFromSurfaceNormal.Name = "buttonViewFromSurfaceNormal";
            buttonViewFromSurfaceNormal.UseVisualStyleBackColor = true;
            buttonViewFromSurfaceNormal.Click += buttonFromSurfaceNormal_Click;
            // 
            // buttonFromX
            // 
            resources.ApplyResources(buttonFromX, "buttonFromX");
            buttonFromX.Name = "buttonFromX";
            buttonFromX.UseVisualStyleBackColor = true;
            buttonFromX.Click += buttonViewFromX_Click;
            // 
            // buttonViewFromZ
            // 
            resources.ApplyResources(buttonViewFromZ, "buttonViewFromZ");
            buttonViewFromZ.Name = "buttonViewFromZ";
            buttonViewFromZ.UseVisualStyleBackColor = true;
            buttonViewFromZ.Click += buttonViewFromZ_Click;
            // 
            // buttonCalcBSE
            // 
            resources.ApplyResources(buttonCalcBSE, "buttonCalcBSE");
            buttonCalcBSE.Name = "buttonCalcBSE";
            buttonCalcBSE.UseVisualStyleBackColor = true;
            buttonCalcBSE.Click += buttonBSE_Click;
            // 
            // buttonFitNistElasticSampler
            // 
            resources.ApplyResources(buttonFitNistElasticSampler, "buttonFitNistElasticSampler");
            buttonFitNistElasticSampler.BackColor = System.Drawing.Color.DarkOliveGreen;
            buttonFitNistElasticSampler.ForeColor = System.Drawing.Color.White;
            buttonFitNistElasticSampler.Name = "buttonFitNistElasticSampler";
            buttonFitNistElasticSampler.UseVisualStyleBackColor = false;
            buttonFitNistElasticSampler.Click += buttonFitNistElasticSampler_Click;
            // 
            // graphControlDepthProfile
            // 
            resources.ApplyResources(graphControlDepthProfile, "graphControlDepthProfile");
            graphControlDepthProfile.AllowMouseOperation = true;
            graphControlDepthProfile.AxisLineColor = System.Drawing.Color.Gray;
            graphControlDepthProfile.AxisTextColor = System.Drawing.Color.Black;
            graphControlDepthProfile.AxisTextFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlDepthProfile.AxisXTextVisible = true;
            graphControlDepthProfile.AxisYTextVisible = true;
            graphControlDepthProfile.BackgroundColor = System.Drawing.Color.White;
            graphControlDepthProfile.BottomMargin = 0D;
            graphControlDepthProfile.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlDepthProfile.DivisionLineXVisible = true;
            graphControlDepthProfile.DivisionLineYVisible = true;
            graphControlDepthProfile.FixRangeHorizontal = false;
            graphControlDepthProfile.FixRangeVertical = false;
            graphControlDepthProfile.GraphTitle = "";
            graphControlDepthProfile.IsIntegerX = false;
            graphControlDepthProfile.IsIntegerY = false;
            graphControlDepthProfile.LabelX = "X:";
            graphControlDepthProfile.LabelY = "Y:";
            graphControlDepthProfile.LeftMargin = 0F;
            graphControlDepthProfile.LineWidth = 1F;
            graphControlDepthProfile.LowerX = 0D;
            graphControlDepthProfile.LowerY = 0D;
            graphControlDepthProfile.MaximalX = 1D;
            graphControlDepthProfile.MaximalY = 1D;
            graphControlDepthProfile.MinimalX = 0D;
            graphControlDepthProfile.MinimalY = 0D;
            graphControlDepthProfile.Mode = GraphControl.DrawingMode.Line;
            graphControlDepthProfile.MousePositionVisible = false;
            graphControlDepthProfile.MousePositionXDigit = -1;
            graphControlDepthProfile.MousePositionYDigit = -1;
            graphControlDepthProfile.Name = "graphControlDepthProfile";
            graphControlDepthProfile.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlDepthProfile.UnitX = "";
            graphControlDepthProfile.UnitY = "";
            graphControlDepthProfile.UpperPanelFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlDepthProfile.UpperPanelVisible = false;
            graphControlDepthProfile.UpperX = 1D;
            graphControlDepthProfile.UpperY = 1D;
            graphControlDepthProfile.UseLineWidth = true;
            graphControlDepthProfile.VerticalLineColor = System.Drawing.Color.Red;
            graphControlDepthProfile.XLog = false;
            graphControlDepthProfile.YLog = false;
            // 
            // poleFigureControl
            // 
            resources.ApplyResources(poleFigureControl, "poleFigureControl");
            poleFigureControl.Name = "poleFigureControl";
            // 
            // graphControlEnergyProfile
            // 
            resources.ApplyResources(graphControlEnergyProfile, "graphControlEnergyProfile");
            graphControlEnergyProfile.AllowMouseOperation = true;
            graphControlEnergyProfile.AxisLineColor = System.Drawing.Color.Gray;
            graphControlEnergyProfile.AxisTextColor = System.Drawing.Color.Black;
            graphControlEnergyProfile.AxisTextFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlEnergyProfile.AxisXTextVisible = true;
            graphControlEnergyProfile.AxisYTextVisible = true;
            graphControlEnergyProfile.BackgroundColor = System.Drawing.Color.White;
            graphControlEnergyProfile.BottomMargin = 0D;
            graphControlEnergyProfile.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlEnergyProfile.DivisionLineXVisible = true;
            graphControlEnergyProfile.DivisionLineYVisible = true;
            graphControlEnergyProfile.FixRangeHorizontal = false;
            graphControlEnergyProfile.FixRangeVertical = false;
            graphControlEnergyProfile.GraphTitle = "";
            graphControlEnergyProfile.IsIntegerX = false;
            graphControlEnergyProfile.IsIntegerY = false;
            graphControlEnergyProfile.LabelX = "X:";
            graphControlEnergyProfile.LabelY = "Y:";
            graphControlEnergyProfile.LeftMargin = 0F;
            graphControlEnergyProfile.LineWidth = 1F;
            graphControlEnergyProfile.LowerX = 0D;
            graphControlEnergyProfile.LowerY = 0D;
            graphControlEnergyProfile.MaximalX = 1D;
            graphControlEnergyProfile.MaximalY = 1D;
            graphControlEnergyProfile.MinimalX = 0D;
            graphControlEnergyProfile.MinimalY = 0D;
            graphControlEnergyProfile.Mode = GraphControl.DrawingMode.Line;
            graphControlEnergyProfile.MousePositionVisible = false;
            graphControlEnergyProfile.MousePositionXDigit = -1;
            graphControlEnergyProfile.MousePositionYDigit = -1;
            graphControlEnergyProfile.Name = "graphControlEnergyProfile";
            graphControlEnergyProfile.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlEnergyProfile.UnitX = "";
            graphControlEnergyProfile.UnitY = "";
            graphControlEnergyProfile.UpperPanelFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlEnergyProfile.UpperPanelVisible = false;
            graphControlEnergyProfile.UpperX = 1D;
            graphControlEnergyProfile.UpperY = 1D;
            graphControlEnergyProfile.UseLineWidth = true;
            graphControlEnergyProfile.VerticalLineColor = System.Drawing.Color.Red;
            graphControlEnergyProfile.XLog = false;
            graphControlEnergyProfile.YLog = false;
            // 
            // checkBoxDrawAxesInStereonet
            // 
            resources.ApplyResources(checkBoxDrawAxesInStereonet, "checkBoxDrawAxesInStereonet");
            checkBoxDrawAxesInStereonet.BackColor = System.Drawing.Color.White;
            checkBoxDrawAxesInStereonet.Checked = true;
            checkBoxDrawAxesInStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxesInStereonet.Name = "checkBoxDrawAxesInStereonet";
            checkBoxDrawAxesInStereonet.UseVisualStyleBackColor = false;
            // 
            // numericBoxDetTilt
            // 
            resources.ApplyResources(numericBoxDetTilt, "numericBoxDetTilt");
            numericBoxDetTilt.BackColor = System.Drawing.Color.Transparent;
            numericBoxDetTilt.Maximum = 180D;
            numericBoxDetTilt.Minimum = 0D;
            numericBoxDetTilt.Name = "numericBoxDetTilt";
            numericBoxDetTilt.RadianValue = 1.5707963267948966D;
            numericBoxDetTilt.ShowUpDown = true;
            numericBoxDetTilt.UpDown_Increment = 10D;
            numericBoxDetTilt.Value = 90D;
            numericBoxDetTilt.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // numericBoxDetRadius
            // 
            resources.ApplyResources(numericBoxDetRadius, "numericBoxDetRadius");
            numericBoxDetRadius.BackColor = System.Drawing.Color.Transparent;
            numericBoxDetRadius.Maximum = 180D;
            numericBoxDetRadius.Minimum = 0D;
            numericBoxDetRadius.Name = "numericBoxDetRadius";
            numericBoxDetRadius.RadianValue = 0.43633231299858238D;
            numericBoxDetRadius.ShowUpDown = true;
            numericBoxDetRadius.UpDown_Increment = 10D;
            numericBoxDetRadius.Value = 25D;
            numericBoxDetRadius.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // numericBoxZofDet
            // 
            resources.ApplyResources(numericBoxZofDet, "numericBoxZofDet");
            numericBoxZofDet.BackColor = System.Drawing.Color.Transparent;
            numericBoxZofDet.Maximum = 1000D;
            numericBoxZofDet.Minimum = -1000D;
            numericBoxZofDet.Name = "numericBoxZofDet";
            numericBoxZofDet.ShowUpDown = true;
            numericBoxZofDet.UpDown_Increment = 10D;
            numericBoxZofDet.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // numericBoxYofDet
            // 
            resources.ApplyResources(numericBoxYofDet, "numericBoxYofDet");
            numericBoxYofDet.BackColor = System.Drawing.Color.Transparent;
            numericBoxYofDet.Maximum = 1000D;
            numericBoxYofDet.Minimum = -1000D;
            numericBoxYofDet.Name = "numericBoxYofDet";
            numericBoxYofDet.RadianValue = -0.52359877559829882D;
            numericBoxYofDet.ShowUpDown = true;
            numericBoxYofDet.UpDown_Increment = 10D;
            numericBoxYofDet.Value = -30D;
            numericBoxYofDet.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // graphicsBox
            // 
            resources.ApplyResources(graphicsBox, "graphicsBox");
            graphicsBox.BackColor = System.Drawing.Color.Transparent;
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            graphicsBox.Fonts = new System.Drawing.Font("Segoe UI Variable Text", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.TabStop = false;
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            graphicsBox.MouseUp += graphicsBox_MouseUp;
            // 
            // trackBarStrSize
            // 
            resources.ApplyResources(trackBarStrSize, "trackBarStrSize");
            trackBarStrSize.LargeChange = 50;
            trackBarStrSize.Maximum = 200;
            trackBarStrSize.Minimum = 1;
            trackBarStrSize.Name = "trackBarStrSize";
            trackBarStrSize.SmallChange = 10;
            trackBarStrSize.TickFrequency = 500;
            trackBarStrSize.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarStrSize.Value = 80;
            trackBarStrSize.ValueChanged += colorControlExcessLine_ColorChanged;
            // 
            // colorControlExcessLine
            // 
            resources.ApplyResources(colorControlExcessLine, "colorControlExcessLine");
            colorControlExcessLine.Argb = -2039584;
            colorControlExcessLine.BackColor = System.Drawing.SystemColors.Control;
            colorControlExcessLine.Blue = 224;
            colorControlExcessLine.BlueF = 0.8784314F;
            colorControlExcessLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControlExcessLine.Color = System.Drawing.Color.FromArgb(224, 224, 224);
            colorControlExcessLine.Green = 224;
            colorControlExcessLine.GreenF = 0.8784314F;
            colorControlExcessLine.Name = "colorControlExcessLine";
            colorControlExcessLine.Red = 224;
            colorControlExcessLine.RedF = 0.8784314F;
            colorControlExcessLine.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // trackBarLineWidth
            // 
            resources.ApplyResources(trackBarLineWidth, "trackBarLineWidth");
            trackBarLineWidth.Maximum = 10000;
            trackBarLineWidth.Minimum = 1;
            trackBarLineWidth.Name = "trackBarLineWidth";
            trackBarLineWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarLineWidth.Value = 1;
            trackBarLineWidth.ValueChanged += colorControlExcessLine_ColorChanged;
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            // 
            // colorControlString
            // 
            resources.ApplyResources(colorControlString, "colorControlString");
            colorControlString.Argb = -1;
            colorControlString.BackColor = System.Drawing.SystemColors.Control;
            colorControlString.Blue = 255;
            colorControlString.BlueF = 1F;
            colorControlString.BoxSize = new System.Drawing.Size(20, 20);
            colorControlString.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlString.Green = 255;
            colorControlString.GreenF = 1F;
            colorControlString.Name = "colorControlString";
            colorControlString.Red = 255;
            colorControlString.RedF = 1F;
            colorControlString.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // colorControlBackGround
            // 
            resources.ApplyResources(colorControlBackGround, "colorControlBackGround");
            colorControlBackGround.Argb = -14671840;
            colorControlBackGround.BackColor = System.Drawing.SystemColors.Control;
            colorControlBackGround.Blue = 32;
            colorControlBackGround.BlueF = 0.1254902F;
            colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(32, 32, 32);
            colorControlBackGround.Green = 32;
            colorControlBackGround.GreenF = 0.1254902F;
            colorControlBackGround.Name = "colorControlBackGround";
            colorControlBackGround.Red = 32;
            colorControlBackGround.RedF = 0.1254902F;
            colorControlBackGround.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // radioButtonKikuchiThresholdOfStructureFactor
            // 
            resources.ApplyResources(radioButtonKikuchiThresholdOfStructureFactor, "radioButtonKikuchiThresholdOfStructureFactor");
            radioButtonKikuchiThresholdOfStructureFactor.Checked = true;
            radioButtonKikuchiThresholdOfStructureFactor.Name = "radioButtonKikuchiThresholdOfStructureFactor";
            radioButtonKikuchiThresholdOfStructureFactor.TabStop = true;
            radioButtonKikuchiThresholdOfStructureFactor.UseVisualStyleBackColor = true;
            radioButtonKikuchiThresholdOfStructureFactor.CheckedChanged += radioButtonKikuchiThresholdOfStructureFactor_CheckedChanged;
            // 
            // checkBoxKikuchiLine_Kinematical
            // 
            resources.ApplyResources(checkBoxKikuchiLine_Kinematical, "checkBoxKikuchiLine_Kinematical");
            checkBoxKikuchiLine_Kinematical.Checked = true;
            checkBoxKikuchiLine_Kinematical.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxKikuchiLine_Kinematical.Name = "checkBoxKikuchiLine_Kinematical";
            checkBoxKikuchiLine_Kinematical.UseVisualStyleBackColor = true;
            checkBoxKikuchiLine_Kinematical.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // radioButtonKikuchiThresholdOfLength
            // 
            resources.ApplyResources(radioButtonKikuchiThresholdOfLength, "radioButtonKikuchiThresholdOfLength");
            radioButtonKikuchiThresholdOfLength.Name = "radioButtonKikuchiThresholdOfLength";
            radioButtonKikuchiThresholdOfLength.UseVisualStyleBackColor = true;
            // 
            // numericBoxKikuchiThreadSholdOfStructureFactor
            // 
            resources.ApplyResources(numericBoxKikuchiThreadSholdOfStructureFactor, "numericBoxKikuchiThreadSholdOfStructureFactor");
            numericBoxKikuchiThreadSholdOfStructureFactor.BackColor = System.Drawing.Color.Transparent;
            numericBoxKikuchiThreadSholdOfStructureFactor.Maximum = 1000D;
            numericBoxKikuchiThreadSholdOfStructureFactor.Minimum = 1D;
            numericBoxKikuchiThreadSholdOfStructureFactor.Name = "numericBoxKikuchiThreadSholdOfStructureFactor";
            numericBoxKikuchiThreadSholdOfStructureFactor.RadianValue = 0.69813170079773179D;
            numericBoxKikuchiThreadSholdOfStructureFactor.ShowUpDown = true;
            numericBoxKikuchiThreadSholdOfStructureFactor.SmartIncrement = true;
            numericBoxKikuchiThreadSholdOfStructureFactor.Value = 40D;
            numericBoxKikuchiThreadSholdOfStructureFactor.ValueChanged += numericBoxKikuchiThreadSholdOfStructureFactor_ValueChanged;
            // 
            // numericBoxKikuchiThresholdOfLength
            // 
            resources.ApplyResources(numericBoxKikuchiThresholdOfLength, "numericBoxKikuchiThresholdOfLength");
            numericBoxKikuchiThresholdOfLength.BackColor = System.Drawing.Color.Transparent;
            numericBoxKikuchiThresholdOfLength.Maximum = 100D;
            numericBoxKikuchiThresholdOfLength.Minimum = 0D;
            numericBoxKikuchiThresholdOfLength.Name = "numericBoxKikuchiThresholdOfLength";
            numericBoxKikuchiThresholdOfLength.RadianValue = 0.17453292519943295D;
            numericBoxKikuchiThresholdOfLength.ShowUpDown = true;
            numericBoxKikuchiThresholdOfLength.SmartIncrement = true;
            numericBoxKikuchiThresholdOfLength.Value = 10D;
            numericBoxKikuchiThresholdOfLength.ValueChanged += numericBoxKikuchiThreadSholdOfStructureFactor_ValueChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // numericBoxThicknessStep
            // 
            resources.ApplyResources(numericBoxThicknessStep, "numericBoxThicknessStep");
            numericBoxThicknessStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.DecimalPlaces = 2;
            numericBoxThicknessStep.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.Maximum = 10D;
            numericBoxThicknessStep.Minimum = 0.001D;
            numericBoxThicknessStep.Name = "numericBoxThicknessStep";
            numericBoxThicknessStep.RadianValue = 0.017453292519943295D;
            numericBoxThicknessStep.ShowUpDown = true;
            numericBoxThicknessStep.SmartIncrement = true;
            numericBoxThicknessStep.ThonsandsSeparator = true;
            numericBoxThicknessStep.Value = 1D;
            numericBoxThicknessStep.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxMaxNumOfG
            // 
            resources.ApplyResources(numericBoxMaxNumOfG, "numericBoxMaxNumOfG");
            numericBoxMaxNumOfG.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxNumOfG.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxNumOfG.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxNumOfG.Maximum = 2048D;
            numericBoxMaxNumOfG.Minimum = 1D;
            numericBoxMaxNumOfG.Name = "numericBoxMaxNumOfG";
            numericBoxMaxNumOfG.RadianValue = 0.55850536063818546D;
            numericBoxMaxNumOfG.ShowUpDown = true;
            numericBoxMaxNumOfG.SmartIncrement = true;
            numericBoxMaxNumOfG.ThonsandsSeparator = true;
            numericBoxMaxNumOfG.Value = 32D;
            // 
            // checkBoxNonLocalAbsorption
            // 
            resources.ApplyResources(checkBoxNonLocalAbsorption, "checkBoxNonLocalAbsorption");
            checkBoxNonLocalAbsorption.Name = "checkBoxNonLocalAbsorption";
            checkBoxNonLocalAbsorption.UseVisualStyleBackColor = true;
            // 
            // checkBoxTDSBackground
            // 
            resources.ApplyResources(checkBoxTDSBackground, "checkBoxTDSBackground");
            checkBoxTDSBackground.Name = "checkBoxTDSBackground";
            checkBoxTDSBackground.UseVisualStyleBackColor = true;
            // 
            // numericBoxThicknessStart
            // 
            resources.ApplyResources(numericBoxThicknessStart, "numericBoxThicknessStart");
            numericBoxThicknessStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.DecimalPlaces = 2;
            numericBoxThicknessStart.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.Maximum = 1000D;
            numericBoxThicknessStart.Minimum = 0.001D;
            numericBoxThicknessStart.Name = "numericBoxThicknessStart";
            numericBoxThicknessStart.RadianValue = 0.017453292519943295D;
            numericBoxThicknessStart.ShowUpDown = true;
            numericBoxThicknessStart.SmartIncrement = true;
            numericBoxThicknessStart.ThonsandsSeparator = true;
            numericBoxThicknessStart.Value = 1D;
            numericBoxThicknessStart.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxThicknessEnd
            // 
            resources.ApplyResources(numericBoxThicknessEnd, "numericBoxThicknessEnd");
            numericBoxThicknessEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.DecimalPlaces = 2;
            numericBoxThicknessEnd.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.Maximum = 1000D;
            numericBoxThicknessEnd.Minimum = 0.001D;
            numericBoxThicknessEnd.Name = "numericBoxThicknessEnd";
            numericBoxThicknessEnd.RadianValue = 0.87266462599716477D;
            numericBoxThicknessEnd.ShowUpDown = true;
            numericBoxThicknessEnd.SmartIncrement = true;
            numericBoxThicknessEnd.ThonsandsSeparator = true;
            numericBoxThicknessEnd.Value = 50D;
            numericBoxThicknessEnd.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // buttonStop
            // 
            resources.ApplyResources(buttonStop, "buttonStop");
            buttonStop.BackColor = System.Drawing.Color.IndianRed;
            buttonStop.ForeColor = System.Drawing.Color.White;
            buttonStop.Name = "buttonStop";
            buttonStop.UseVisualStyleBackColor = false;
            // 
            // groupBoxOutput
            // 
            resources.ApplyResources(groupBoxOutput, "groupBoxOutput");
            groupBoxOutput.Controls.Add(flowLayoutPanelOutputRange);
            groupBoxOutput.Controls.Add(label3);
            groupBoxOutput.Controls.Add(label4);
            groupBoxOutput.Controls.Add(checkBoxWithBSEDistribution);
            groupBoxOutput.Controls.Add(comboBoxGradient);
            groupBoxOutput.Controls.Add(comboBoxScale);
            groupBoxOutput.Controls.Add(trackBarIntensityBrightnessMax);
            groupBoxOutput.Controls.Add(trackBarIntensityBrightnessMin);
            groupBoxOutput.Controls.Add(label8);
            groupBoxOutput.Controls.Add(label7);
            groupBoxOutput.Controls.Add(label10);
            groupBoxOutput.Name = "groupBoxOutput";
            groupBoxOutput.TabStop = false;
            // 
            // flowLayoutPanelOutputRange
            // 
            resources.ApplyResources(flowLayoutPanelOutputRange, "flowLayoutPanelOutputRange");
            flowLayoutPanelOutputRange.Controls.Add(label9);
            flowLayoutPanelOutputRange.Controls.Add(textBoxEnergy);
            flowLayoutPanelOutputRange.Controls.Add(label12);
            flowLayoutPanelOutputRange.Controls.Add(trackBarOutputEnergy);
            flowLayoutPanelOutputRange.Controls.Add(label5);
            flowLayoutPanelOutputRange.Controls.Add(textBoxThickness);
            flowLayoutPanelOutputRange.Controls.Add(label6);
            flowLayoutPanelOutputRange.Controls.Add(trackBarOutputThickness);
            flowLayoutPanelOutputRange.Name = "flowLayoutPanelOutputRange";
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            // 
            // textBoxEnergy
            // 
            resources.ApplyResources(textBoxEnergy, "textBoxEnergy");
            textBoxEnergy.Name = "textBoxEnergy";
            textBoxEnergy.ReadOnly = true;
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            label12.Name = "label12";
            // 
            // trackBarOutputEnergy
            // 
            resources.ApplyResources(trackBarOutputEnergy, "trackBarOutputEnergy");
            trackBarOutputEnergy.LargeChange = 1;
            trackBarOutputEnergy.Maximum = 5;
            trackBarOutputEnergy.Name = "trackBarOutputEnergy";
            trackBarOutputEnergy.ValueChanged += trackBarOutputEnergy_ValueChanged;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // textBoxThickness
            // 
            resources.ApplyResources(textBoxThickness, "textBoxThickness");
            textBoxThickness.Name = "textBoxThickness";
            textBoxThickness.ReadOnly = true;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // trackBarOutputThickness
            // 
            resources.ApplyResources(trackBarOutputThickness, "trackBarOutputThickness");
            trackBarOutputThickness.LargeChange = 1;
            trackBarOutputThickness.Maximum = 9;
            trackBarOutputThickness.Name = "trackBarOutputThickness";
            trackBarOutputThickness.ValueChanged += TrackBarOutputThickness_Scroll;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // checkBoxWithBSEDistribution
            // 
            resources.ApplyResources(checkBoxWithBSEDistribution, "checkBoxWithBSEDistribution");
            checkBoxWithBSEDistribution.Checked = true;
            checkBoxWithBSEDistribution.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxWithBSEDistribution.Name = "checkBoxWithBSEDistribution";
            checkBoxWithBSEDistribution.UseVisualStyleBackColor = true;
            checkBoxWithBSEDistribution.CheckedChanged += checkBoxWithBSEDistribution_CheckedChanged;
            // 
            // comboBoxGradient
            // 
            resources.ApplyResources(comboBoxGradient, "comboBoxGradient");
            comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxGradient.FormattingEnabled = true;
            comboBoxGradient.Items.AddRange(new object[] { resources.GetString("comboBoxGradient.Items"), resources.GetString("comboBoxGradient.Items1") });
            comboBoxGradient.Name = "comboBoxGradient";
            comboBoxGradient.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // comboBoxScale
            // 
            resources.ApplyResources(comboBoxScale, "comboBoxScale");
            comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale.FormattingEnabled = true;
            comboBoxScale.Items.AddRange(new object[] { resources.GetString("comboBoxScale.Items"), resources.GetString("comboBoxScale.Items1"), resources.GetString("comboBoxScale.Items2"), resources.GetString("comboBoxScale.Items3") });
            comboBoxScale.Name = "comboBoxScale";
            comboBoxScale.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // trackBarIntensityBrightnessMax
            // 
            resources.ApplyResources(trackBarIntensityBrightnessMax, "trackBarIntensityBrightnessMax");
            trackBarIntensityBrightnessMax.LargeChange = 10000;
            trackBarIntensityBrightnessMax.Maximum = 1000000;
            trackBarIntensityBrightnessMax.Minimum = 1;
            trackBarIntensityBrightnessMax.Name = "trackBarIntensityBrightnessMax";
            trackBarIntensityBrightnessMax.SmallChange = 100000;
            trackBarIntensityBrightnessMax.TickFrequency = 20000;
            trackBarIntensityBrightnessMax.Value = 1000000;
            trackBarIntensityBrightnessMax.ValueChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // trackBarIntensityBrightnessMin
            // 
            resources.ApplyResources(trackBarIntensityBrightnessMin, "trackBarIntensityBrightnessMin");
            trackBarIntensityBrightnessMin.LargeChange = 10000;
            trackBarIntensityBrightnessMin.Maximum = 999999;
            trackBarIntensityBrightnessMin.Name = "trackBarIntensityBrightnessMin";
            trackBarIntensityBrightnessMin.SmallChange = 100000;
            trackBarIntensityBrightnessMin.TickFrequency = 20000;
            trackBarIntensityBrightnessMin.ValueChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            // 
            // checkBoxShowOverlays
            // 
            resources.ApplyResources(checkBoxShowOverlays, "checkBoxShowOverlays");
            checkBoxShowOverlays.Checked = true;
            checkBoxShowOverlays.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowOverlays.Name = "checkBoxShowOverlays";
            checkBoxShowOverlays.UseVisualStyleBackColor = true;
            checkBoxShowOverlays.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // buttonSaveImage
            // 
            resources.ApplyResources(buttonSaveImage, "buttonSaveImage");
            buttonSaveImage.Name = "buttonSaveImage";
            buttonSaveImage.UseVisualStyleBackColor = true;
            buttonSaveImage.Click += buttonSaveImage_Click;
            // 
            // numericBoxEnergyEnd
            // 
            resources.ApplyResources(numericBoxEnergyEnd, "numericBoxEnergyEnd");
            numericBoxEnergyEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyEnd.DecimalPlaces = 2;
            numericBoxEnergyEnd.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyEnd.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyEnd.Maximum = 1000D;
            numericBoxEnergyEnd.Minimum = 0.001D;
            numericBoxEnergyEnd.Name = "numericBoxEnergyEnd";
            numericBoxEnergyEnd.RadianValue = 0.26179938779914941D;
            numericBoxEnergyEnd.ShowUpDown = true;
            numericBoxEnergyEnd.SmartIncrement = true;
            numericBoxEnergyEnd.ThonsandsSeparator = true;
            numericBoxEnergyEnd.Value = 15D;
            numericBoxEnergyEnd.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // numericBoxEnergyStart
            // 
            resources.ApplyResources(numericBoxEnergyStart, "numericBoxEnergyStart");
            numericBoxEnergyStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.DecimalPlaces = 2;
            numericBoxEnergyStart.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.Maximum = 1000D;
            numericBoxEnergyStart.Minimum = 1D;
            numericBoxEnergyStart.Name = "numericBoxEnergyStart";
            numericBoxEnergyStart.RadianValue = 0.3490658503988659D;
            numericBoxEnergyStart.ShowUpDown = true;
            numericBoxEnergyStart.SmartIncrement = true;
            numericBoxEnergyStart.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.ThonsandsSeparator = true;
            numericBoxEnergyStart.Value = 20D;
            numericBoxEnergyStart.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // numericBoxEnergyStep
            // 
            resources.ApplyResources(numericBoxEnergyStep, "numericBoxEnergyStep");
            numericBoxEnergyStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStep.DecimalPlaces = 2;
            numericBoxEnergyStep.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStep.Maximum = 10D;
            numericBoxEnergyStep.Minimum = 0.001D;
            numericBoxEnergyStep.Name = "numericBoxEnergyStep";
            numericBoxEnergyStep.RadianValue = 0.017453292519943295D;
            numericBoxEnergyStep.ShowUpDown = true;
            numericBoxEnergyStep.SmartIncrement = true;
            numericBoxEnergyStep.ThonsandsSeparator = true;
            numericBoxEnergyStep.Value = 1D;
            numericBoxEnergyStep.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.BackColor = System.Drawing.Color.White;
            label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            label14.BackColor = System.Drawing.Color.White;
            label14.Name = "label14";
            // 
            // checkBoxShowDyanmicalEBSD
            // 
            resources.ApplyResources(checkBoxShowDyanmicalEBSD, "checkBoxShowDyanmicalEBSD");
            checkBoxShowDyanmicalEBSD.Checked = true;
            checkBoxShowDyanmicalEBSD.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowDyanmicalEBSD.Name = "checkBoxShowDyanmicalEBSD";
            checkBoxShowDyanmicalEBSD.UseVisualStyleBackColor = true;
            checkBoxShowDyanmicalEBSD.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxDrawDetectorOutline
            // 
            resources.ApplyResources(checkBoxDrawDetectorOutline, "checkBoxDrawDetectorOutline");
            checkBoxDrawDetectorOutline.Checked = true;
            checkBoxDrawDetectorOutline.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawDetectorOutline.Name = "checkBoxDrawDetectorOutline";
            checkBoxDrawDetectorOutline.UseVisualStyleBackColor = true;
            checkBoxDrawDetectorOutline.CheckedChanged += checkBoxDrawDetectorOutline_CheckedChanged;
            // 
            // tabControl1
            // 
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.HotTrack = true;
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.BackColor = System.Drawing.SystemColors.Control;
            tabPage1.Controls.Add(groupBoxEBSDGeometry);
            tabPage1.Controls.Add(groupBoxSampleCondition);
            tabPage1.Controls.Add(flowLayoutPanelViewAlong);
            tabPage1.Controls.Add(panelGeometry);
            tabPage1.Name = "tabPage1";
            // 
            // groupBoxEBSDGeometry
            // 
            resources.ApplyResources(groupBoxEBSDGeometry, "groupBoxEBSDGeometry");
            groupBoxEBSDGeometry.Controls.Add(label2);
            groupBoxEBSDGeometry.Controls.Add(label17);
            groupBoxEBSDGeometry.Controls.Add(label16);
            groupBoxEBSDGeometry.Controls.Add(numericBoxYofDet);
            groupBoxEBSDGeometry.Controls.Add(numericBoxDetRadius);
            groupBoxEBSDGeometry.Controls.Add(numericBoxZofDet);
            groupBoxEBSDGeometry.Controls.Add(numericBoxDetTilt);
            groupBoxEBSDGeometry.Name = "groupBoxEBSDGeometry";
            groupBoxEBSDGeometry.TabStop = false;
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            label16.Name = "label16";
            // 
            // groupBoxSampleCondition
            // 
            resources.ApplyResources(groupBoxSampleCondition, "groupBoxSampleCondition");
            groupBoxSampleCondition.Controls.Add(waveLengthControl);
            groupBoxSampleCondition.Controls.Add(numericBoxSampleTilt);
            groupBoxSampleCondition.Name = "groupBoxSampleCondition";
            groupBoxSampleCondition.TabStop = false;
            // 
            // tabPage2
            // 
            resources.ApplyResources(tabPage2, "tabPage2");
            tabPage2.BackColor = System.Drawing.SystemColors.Control;
            tabPage2.Controls.Add(label15);
            tabPage2.Controls.Add(buttonCalcBSE);
            tabPage2.Controls.Add(label13);
            tabPage2.Controls.Add(checkBoxDrawAxesInStereonet);
            tabPage2.Controls.Add(label14);
            tabPage2.Controls.Add(poleFigureControl);
            tabPage2.Controls.Add(graphControlDepthProfile);
            tabPage2.Controls.Add(graphControlEnergyProfile);
            tabPage2.Name = "tabPage2";
            // 
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            label15.BackColor = System.Drawing.Color.White;
            label15.Name = "label15";
            // 
            // tabPage3
            // 
            resources.ApplyResources(tabPage3, "tabPage3");
            tabPage3.BackColor = System.Drawing.SystemColors.Control;
            tabPage3.Controls.Add(checkBoxDrawDetectorOutline);
            tabPage3.Controls.Add(groupBoxDetectorOutline);
            tabPage3.Controls.Add(groupBox2);
            tabPage3.Controls.Add(checkBoxShowKikuchiLines);
            tabPage3.Controls.Add(groupBoxLatticePlanes);
            tabPage3.Controls.Add(groupBoxKikuchiLines);
            tabPage3.Controls.Add(checkBoxShowGIndices);
            tabPage3.Controls.Add(colorControlBackGround);
            tabPage3.Controls.Add(checkBoxShowZoneAxisIndices);
            tabPage3.Name = "tabPage3";
            // 
            // groupBoxDetectorOutline
            // 
            resources.ApplyResources(groupBoxDetectorOutline, "groupBoxDetectorOutline");
            groupBoxDetectorOutline.Controls.Add(checkBoxShowMesh);
            groupBoxDetectorOutline.Controls.Add(checkBoxShowCircle);
            groupBoxDetectorOutline.Name = "groupBoxDetectorOutline";
            groupBoxDetectorOutline.TabStop = false;
            // 
            // checkBoxShowMesh
            // 
            resources.ApplyResources(checkBoxShowMesh, "checkBoxShowMesh");
            checkBoxShowMesh.Checked = true;
            checkBoxShowMesh.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowMesh.Name = "checkBoxShowMesh";
            checkBoxShowMesh.UseVisualStyleBackColor = true;
            checkBoxShowMesh.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxShowCircle
            // 
            resources.ApplyResources(checkBoxShowCircle, "checkBoxShowCircle");
            checkBoxShowCircle.Checked = true;
            checkBoxShowCircle.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowCircle.Name = "checkBoxShowCircle";
            checkBoxShowCircle.UseVisualStyleBackColor = true;
            checkBoxShowCircle.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(trackBarStrSize);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(colorControlString);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // checkBoxShowKikuchiLines
            // 
            resources.ApplyResources(checkBoxShowKikuchiLines, "checkBoxShowKikuchiLines");
            checkBoxShowKikuchiLines.Checked = true;
            checkBoxShowKikuchiLines.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowKikuchiLines.Name = "checkBoxShowKikuchiLines";
            checkBoxShowKikuchiLines.UseVisualStyleBackColor = true;
            checkBoxShowKikuchiLines.CheckedChanged += checkBoxShowKikuchiLines_CheckedChanged;
            // 
            // groupBoxLatticePlanes
            // 
            resources.ApplyResources(groupBoxLatticePlanes, "groupBoxLatticePlanes");
            groupBoxLatticePlanes.Controls.Add(radioButtonKikuchiThresholdOfLength);
            groupBoxLatticePlanes.Controls.Add(radioButtonKikuchiThresholdOfStructureFactor);
            groupBoxLatticePlanes.Controls.Add(numericBoxKikuchiThreadSholdOfStructureFactor);
            groupBoxLatticePlanes.Controls.Add(numericBoxKikuchiThresholdOfLength);
            groupBoxLatticePlanes.Name = "groupBoxLatticePlanes";
            groupBoxLatticePlanes.TabStop = false;
            // 
            // groupBoxKikuchiLines
            // 
            resources.ApplyResources(groupBoxKikuchiLines, "groupBoxKikuchiLines");
            groupBoxKikuchiLines.Controls.Add(colorControlExcessLine);
            groupBoxKikuchiLines.Controls.Add(checkBoxKikuchiLine_Kinematical);
            groupBoxKikuchiLines.Controls.Add(trackBarLineWidth);
            groupBoxKikuchiLines.Controls.Add(label11);
            groupBoxKikuchiLines.Name = "groupBoxKikuchiLines";
            groupBoxKikuchiLines.TabStop = false;
            // 
            // checkBoxShowGIndices
            // 
            resources.ApplyResources(checkBoxShowGIndices, "checkBoxShowGIndices");
            checkBoxShowGIndices.Name = "checkBoxShowGIndices";
            checkBoxShowGIndices.UseVisualStyleBackColor = true;
            checkBoxShowGIndices.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxShowZoneAxisIndices
            // 
            resources.ApplyResources(checkBoxShowZoneAxisIndices, "checkBoxShowZoneAxisIndices");
            checkBoxShowZoneAxisIndices.Checked = true;
            checkBoxShowZoneAxisIndices.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowZoneAxisIndices.Name = "checkBoxShowZoneAxisIndices";
            checkBoxShowZoneAxisIndices.UseVisualStyleBackColor = true;
            checkBoxShowZoneAxisIndices.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // groupBoxSimulationParameters
            // 
            resources.ApplyResources(groupBoxSimulationParameters, "groupBoxSimulationParameters");
            groupBoxSimulationParameters.Controls.Add(numericBoxMaxNumOfG);
            groupBoxSimulationParameters.Controls.Add(labelMasterPatternGrid);
            groupBoxSimulationParameters.Controls.Add(checkBoxNonLocalAbsorption);
            groupBoxSimulationParameters.Controls.Add(comboBoxMasterPatternGrid);
            groupBoxSimulationParameters.Controls.Add(numericBoxThicknessStep);
            groupBoxSimulationParameters.Controls.Add(numericBoxEnergyStep);
            groupBoxSimulationParameters.Controls.Add(numericBoxEnergyStart);
            groupBoxSimulationParameters.Controls.Add(checkBoxTDSBackground);
            groupBoxSimulationParameters.Controls.Add(numericBoxThicknessStart);
            groupBoxSimulationParameters.Controls.Add(numericBoxEnergyEnd);
            groupBoxSimulationParameters.Controls.Add(numericBoxThicknessEnd);
            groupBoxSimulationParameters.Name = "groupBoxSimulationParameters";
            groupBoxSimulationParameters.TabStop = false;
            // 
            // labelMasterPatternGrid
            // 
            resources.ApplyResources(labelMasterPatternGrid, "labelMasterPatternGrid");
            labelMasterPatternGrid.Name = "labelMasterPatternGrid";
            // 
            // comboBoxMasterPatternGrid
            // 
            resources.ApplyResources(comboBoxMasterPatternGrid, "comboBoxMasterPatternGrid");
            comboBoxMasterPatternGrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxMasterPatternGrid.FormattingEnabled = true;
            comboBoxMasterPatternGrid.Items.AddRange(new object[] { resources.GetString("comboBoxMasterPatternGrid.Items"), resources.GetString("comboBoxMasterPatternGrid.Items1"), resources.GetString("comboBoxMasterPatternGrid.Items2"), resources.GetString("comboBoxMasterPatternGrid.Items3"), resources.GetString("comboBoxMasterPatternGrid.Items4"), resources.GetString("comboBoxMasterPatternGrid.Items5"), resources.GetString("comboBoxMasterPatternGrid.Items6"), resources.GetString("comboBoxMasterPatternGrid.Items7"), resources.GetString("comboBoxMasterPatternGrid.Items8"), resources.GetString("comboBoxMasterPatternGrid.Items9") });
            comboBoxMasterPatternGrid.Name = "comboBoxMasterPatternGrid";
            comboBoxMasterPatternGrid.SelectedIndexChanged += MasterPatternSelectionChanged;
            // 
            // statusStrip1
            // 
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3 });
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            resources.ApplyResources(toolStripProgressBar, "toolStripProgressBar");
            toolStripProgressBar.Name = "toolStripProgressBar";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // toolStripStatusLabel2
            // 
            resources.ApplyResources(toolStripStatusLabel2, "toolStripStatusLabel2");
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            resources.ApplyResources(toolStripStatusLabel3, "toolStripStatusLabel3");
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            // 
            // scalablePictureBoxAdvancedMasterPattern2D
            // 
            resources.ApplyResources(scalablePictureBoxAdvancedMasterPattern2D, "scalablePictureBoxAdvancedMasterPattern2D");
            scalablePictureBoxAdvancedMasterPattern2D.ClampIntensityRangeToNewData = false;
            scalablePictureBoxAdvancedMasterPattern2D.ColorVisible = true;
            scalablePictureBoxAdvancedMasterPattern2D.DecimalPlacesForIntensity = 5;
            scalablePictureBoxAdvancedMasterPattern2D.FixZoomAndCenter = false;
            scalablePictureBoxAdvancedMasterPattern2D.FrequencyGraphVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.GradiaentVisible = true;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_DustAndScratches = false;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_DustAndScratchesRadius = 1D;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_DustAndScratchesThreshold = 3D;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_DustAndScratchesVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_GaussianBlur = false;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_GaussianBlurRadius = 1D;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_GaussianBlurVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilterVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.LogScaleBar = false;
            scalablePictureBoxAdvancedMasterPattern2D.LowerIntensity = 0D;
            scalablePictureBoxAdvancedMasterPattern2D.MagInfoVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.MaximumIntensity = 1D;
            scalablePictureBoxAdvancedMasterPattern2D.MinimumIntensity = 0D;
            scalablePictureBoxAdvancedMasterPattern2D.MousePositionLabelVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.Name = "scalablePictureBoxAdvancedMasterPattern2D";
            scalablePictureBoxAdvancedMasterPattern2D.PolarityVisible = true;
            scalablePictureBoxAdvancedMasterPattern2D.ScaleVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.ShowGradiaent = true;
            scalablePictureBoxAdvancedMasterPattern2D.StatusVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.TitleVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.TrackBarVisible = true;
            scalablePictureBoxAdvancedMasterPattern2D.UpperIntensity = 1D;
            scalablePictureBoxAdvancedMasterPattern2D.VisibleGradient = true;
            scalablePictureBoxAdvancedMasterPattern2D.BrightnessAndColorChanged += scalablePictureBoxAdvancedMasterPattern2D_BrightnessAndColorChanged;
            // 
            // flowLayoutPanelMasterPatternSelectors
            // 
            resources.ApplyResources(flowLayoutPanelMasterPatternSelectors, "flowLayoutPanelMasterPatternSelectors");
            flowLayoutPanelMasterPatternSelectors.Controls.Add(labelMasterPatternEnergy);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(textBoxMasterPatternEnergy);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(labelMasterPatternEnergyUnit);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(trackBarMasterPatternEnergy);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(labelMasterPatternDepth);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(textBoxMasterPatternDepth);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(labelMasterPatternDepthUnit);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(trackBarMasterPatternDepth);
            flowLayoutPanelMasterPatternSelectors.Name = "flowLayoutPanelMasterPatternSelectors";
            // 
            // labelMasterPatternEnergy
            // 
            resources.ApplyResources(labelMasterPatternEnergy, "labelMasterPatternEnergy");
            labelMasterPatternEnergy.Name = "labelMasterPatternEnergy";
            // 
            // textBoxMasterPatternEnergy
            // 
            resources.ApplyResources(textBoxMasterPatternEnergy, "textBoxMasterPatternEnergy");
            textBoxMasterPatternEnergy.Name = "textBoxMasterPatternEnergy";
            textBoxMasterPatternEnergy.ReadOnly = true;
            // 
            // labelMasterPatternEnergyUnit
            // 
            resources.ApplyResources(labelMasterPatternEnergyUnit, "labelMasterPatternEnergyUnit");
            labelMasterPatternEnergyUnit.Name = "labelMasterPatternEnergyUnit";
            // 
            // trackBarMasterPatternEnergy
            // 
            resources.ApplyResources(trackBarMasterPatternEnergy, "trackBarMasterPatternEnergy");
            trackBarMasterPatternEnergy.LargeChange = 1;
            trackBarMasterPatternEnergy.Maximum = 0;
            trackBarMasterPatternEnergy.Name = "trackBarMasterPatternEnergy";
            trackBarMasterPatternEnergy.ValueChanged += MasterPatternSelectionChanged;
            // 
            // labelMasterPatternDepth
            // 
            resources.ApplyResources(labelMasterPatternDepth, "labelMasterPatternDepth");
            labelMasterPatternDepth.Name = "labelMasterPatternDepth";
            // 
            // textBoxMasterPatternDepth
            // 
            resources.ApplyResources(textBoxMasterPatternDepth, "textBoxMasterPatternDepth");
            textBoxMasterPatternDepth.Name = "textBoxMasterPatternDepth";
            textBoxMasterPatternDepth.ReadOnly = true;
            // 
            // labelMasterPatternDepthUnit
            // 
            resources.ApplyResources(labelMasterPatternDepthUnit, "labelMasterPatternDepthUnit");
            labelMasterPatternDepthUnit.Name = "labelMasterPatternDepthUnit";
            // 
            // trackBarMasterPatternDepth
            // 
            resources.ApplyResources(trackBarMasterPatternDepth, "trackBarMasterPatternDepth");
            trackBarMasterPatternDepth.LargeChange = 1;
            trackBarMasterPatternDepth.Maximum = 0;
            trackBarMasterPatternDepth.Name = "trackBarMasterPatternDepth";
            trackBarMasterPatternDepth.ValueChanged += MasterPatternSelectionChanged;
            // 
            // labelMasterPattern2DHemisphere
            // 
            resources.ApplyResources(labelMasterPattern2DHemisphere, "labelMasterPattern2DHemisphere");
            labelMasterPattern2DHemisphere.Name = "labelMasterPattern2DHemisphere";
            // 
            // comboBoxMasterPattern2DHemisphere
            // 
            resources.ApplyResources(comboBoxMasterPattern2DHemisphere, "comboBoxMasterPattern2DHemisphere");
            comboBoxMasterPattern2DHemisphere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxMasterPattern2DHemisphere.FormattingEnabled = true;
            comboBoxMasterPattern2DHemisphere.Items.AddRange(new object[] { resources.GetString("comboBoxMasterPattern2DHemisphere.Items"), resources.GetString("comboBoxMasterPattern2DHemisphere.Items1") });
            comboBoxMasterPattern2DHemisphere.Name = "comboBoxMasterPattern2DHemisphere";
            comboBoxMasterPattern2DHemisphere.SelectedIndexChanged += MasterPatternSelectionChanged;
            // 
            // buttonCreateMasterPattern
            // 
            resources.ApplyResources(buttonCreateMasterPattern, "buttonCreateMasterPattern");
            buttonCreateMasterPattern.BackColor = System.Drawing.Color.SteelBlue;
            buttonCreateMasterPattern.ForeColor = System.Drawing.Color.White;
            buttonCreateMasterPattern.Name = "buttonCreateMasterPattern";
            buttonCreateMasterPattern.UseVisualStyleBackColor = false;
            buttonCreateMasterPattern.Click += buttonCreateMasterPattern_Click;
            // 
            // panelMasterPattern3D
            // 
            resources.ApplyResources(panelMasterPattern3D, "panelMasterPattern3D");
            panelMasterPattern3D.BackColor = System.Drawing.SystemColors.Control;
            panelMasterPattern3D.Controls.Add(panelMasterPattern3DAxes);
            panelMasterPattern3D.Name = "panelMasterPattern3D";
            // 
            // panelMasterPattern3DAxes
            // 
            resources.ApplyResources(panelMasterPattern3DAxes, "panelMasterPattern3DAxes");
            panelMasterPattern3DAxes.BackColor = System.Drawing.SystemColors.Control;
            panelMasterPattern3DAxes.Name = "panelMasterPattern3DAxes";
            // 
            // groupBoxMasterPattern
            // 
            resources.ApplyResources(groupBoxMasterPattern, "groupBoxMasterPattern");
            groupBoxMasterPattern.Controls.Add(flowLayoutPanel5);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanel4);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanel3);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanelMasterPattern3DViewAlong);
            groupBoxMasterPattern.Controls.Add(groupBoxSimulationParameters);
            groupBoxMasterPattern.Controls.Add(scalablePictureBoxAdvancedMasterPattern2D);
            groupBoxMasterPattern.Controls.Add(panelMasterPattern3D);
            groupBoxMasterPattern.Controls.Add(buttonFitNistElasticSampler);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanelMasterPatternSelectors);
            groupBoxMasterPattern.Name = "groupBoxMasterPattern";
            groupBoxMasterPattern.TabStop = false;
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(buttonMasterPattern2DCopy);
            flowLayoutPanel5.Controls.Add(labelMasterPattern2DHemisphere);
            flowLayoutPanel5.Controls.Add(comboBoxMasterPattern2DHemisphere);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            // 
            // buttonMasterPattern2DCopy
            // 
            resources.ApplyResources(buttonMasterPattern2DCopy, "buttonMasterPattern2DCopy");
            buttonMasterPattern2DCopy.Name = "buttonMasterPattern2DCopy";
            buttonMasterPattern2DCopy.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(buttonMasterPattern3DCopy);
            flowLayoutPanel4.Controls.Add(checkBoxMasterPattern3DAxisLabel);
            flowLayoutPanel4.Controls.Add(checkBoxMasterPattern3DAxisArrows);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // buttonMasterPattern3DCopy
            // 
            resources.ApplyResources(buttonMasterPattern3DCopy, "buttonMasterPattern3DCopy");
            buttonMasterPattern3DCopy.Name = "buttonMasterPattern3DCopy";
            buttonMasterPattern3DCopy.UseVisualStyleBackColor = true;
            // 
            // checkBoxMasterPattern3DAxisLabel
            // 
            resources.ApplyResources(checkBoxMasterPattern3DAxisLabel, "checkBoxMasterPattern3DAxisLabel");
            checkBoxMasterPattern3DAxisLabel.Checked = true;
            checkBoxMasterPattern3DAxisLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxMasterPattern3DAxisLabel.Name = "checkBoxMasterPattern3DAxisLabel";
            checkBoxMasterPattern3DAxisLabel.UseVisualStyleBackColor = true;
            checkBoxMasterPattern3DAxisLabel.CheckedChanged += checkBoxMasterPattern3DAxisLabel_CheckedChanged;
            // 
            // checkBoxMasterPattern3DAxisArrows
            // 
            resources.ApplyResources(checkBoxMasterPattern3DAxisArrows, "checkBoxMasterPattern3DAxisArrows");
            checkBoxMasterPattern3DAxisArrows.Checked = true;
            checkBoxMasterPattern3DAxisArrows.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxMasterPattern3DAxisArrows.Name = "checkBoxMasterPattern3DAxisArrows";
            checkBoxMasterPattern3DAxisArrows.UseVisualStyleBackColor = true;
            checkBoxMasterPattern3DAxisArrows.CheckedChanged += checkBoxMasterPattern3DAxisArrows_CheckedChanged;
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(buttonCreateMasterPattern);
            flowLayoutPanel3.Controls.Add(buttonStop);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // flowLayoutPanelMasterPattern3DViewAlong
            // 
            resources.ApplyResources(flowLayoutPanelMasterPattern3DViewAlong, "flowLayoutPanelMasterPattern3DViewAlong");
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(buttonMasterPattern3DViewAlong);
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(labelMasterPattern3DViewAlongOpenBracket);
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(numericBoxMasterPattern3DViewAlongU);
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(numericBoxMasterPattern3DViewAlongV);
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(numericBoxMasterPattern3DViewAlongW);
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(labelMasterPattern3DViewAlongCloseBracket);
            flowLayoutPanelMasterPattern3DViewAlong.Name = "flowLayoutPanelMasterPattern3DViewAlong";
            // 
            // buttonMasterPattern3DViewAlong
            // 
            resources.ApplyResources(buttonMasterPattern3DViewAlong, "buttonMasterPattern3DViewAlong");
            buttonMasterPattern3DViewAlong.Name = "buttonMasterPattern3DViewAlong";
            buttonMasterPattern3DViewAlong.UseVisualStyleBackColor = true;
            buttonMasterPattern3DViewAlong.Click += buttonMasterPattern3DViewAlong_Click;
            // 
            // labelMasterPattern3DViewAlongOpenBracket
            // 
            resources.ApplyResources(labelMasterPattern3DViewAlongOpenBracket, "labelMasterPattern3DViewAlongOpenBracket");
            labelMasterPattern3DViewAlongOpenBracket.Name = "labelMasterPattern3DViewAlongOpenBracket";
            // 
            // numericBoxMasterPattern3DViewAlongU
            // 
            resources.ApplyResources(numericBoxMasterPattern3DViewAlongU, "numericBoxMasterPattern3DViewAlongU");
            numericBoxMasterPattern3DViewAlongU.BackColor = System.Drawing.Color.Transparent;
            numericBoxMasterPattern3DViewAlongU.Maximum = 9D;
            numericBoxMasterPattern3DViewAlongU.Minimum = -9D;
            numericBoxMasterPattern3DViewAlongU.Name = "numericBoxMasterPattern3DViewAlongU";
            numericBoxMasterPattern3DViewAlongU.RadianValue = 0.017453292519943295D;
            numericBoxMasterPattern3DViewAlongU.ShowUpDown = true;
            numericBoxMasterPattern3DViewAlongU.SkipEventDuringInput = false;
            numericBoxMasterPattern3DViewAlongU.TextFontSize = 9F;
            numericBoxMasterPattern3DViewAlongU.ThonsandsSeparator = true;
            numericBoxMasterPattern3DViewAlongU.Value = 1D;
            // 
            // numericBoxMasterPattern3DViewAlongV
            // 
            resources.ApplyResources(numericBoxMasterPattern3DViewAlongV, "numericBoxMasterPattern3DViewAlongV");
            numericBoxMasterPattern3DViewAlongV.BackColor = System.Drawing.Color.Transparent;
            numericBoxMasterPattern3DViewAlongV.Maximum = 9D;
            numericBoxMasterPattern3DViewAlongV.Minimum = -9D;
            numericBoxMasterPattern3DViewAlongV.Name = "numericBoxMasterPattern3DViewAlongV";
            numericBoxMasterPattern3DViewAlongV.RadianValue = 0.017453292519943295D;
            numericBoxMasterPattern3DViewAlongV.ShowUpDown = true;
            numericBoxMasterPattern3DViewAlongV.SkipEventDuringInput = false;
            numericBoxMasterPattern3DViewAlongV.TextFontSize = 9F;
            numericBoxMasterPattern3DViewAlongV.ThonsandsSeparator = true;
            numericBoxMasterPattern3DViewAlongV.Value = 1D;
            // 
            // numericBoxMasterPattern3DViewAlongW
            // 
            resources.ApplyResources(numericBoxMasterPattern3DViewAlongW, "numericBoxMasterPattern3DViewAlongW");
            numericBoxMasterPattern3DViewAlongW.BackColor = System.Drawing.Color.Transparent;
            numericBoxMasterPattern3DViewAlongW.Maximum = 9D;
            numericBoxMasterPattern3DViewAlongW.Minimum = -9D;
            numericBoxMasterPattern3DViewAlongW.Name = "numericBoxMasterPattern3DViewAlongW";
            numericBoxMasterPattern3DViewAlongW.RadianValue = 0.017453292519943295D;
            numericBoxMasterPattern3DViewAlongW.ShowUpDown = true;
            numericBoxMasterPattern3DViewAlongW.SkipEventDuringInput = false;
            numericBoxMasterPattern3DViewAlongW.TextFontSize = 9F;
            numericBoxMasterPattern3DViewAlongW.ThonsandsSeparator = true;
            numericBoxMasterPattern3DViewAlongW.Value = 1D;
            // 
            // labelMasterPattern3DViewAlongCloseBracket
            // 
            resources.ApplyResources(labelMasterPattern3DViewAlongCloseBracket, "labelMasterPattern3DViewAlongCloseBracket");
            labelMasterPattern3DViewAlongCloseBracket.Name = "labelMasterPattern3DViewAlongCloseBracket";
            // 
            // groupBoxEBSDPattern
            // 
            resources.ApplyResources(groupBoxEBSDPattern, "groupBoxEBSDPattern");
            groupBoxEBSDPattern.Controls.Add(graphicsBox);
            groupBoxEBSDPattern.Controls.Add(flowLayoutPanel1);
            groupBoxEBSDPattern.Controls.Add(groupBoxOutput);
            groupBoxEBSDPattern.Name = "groupBoxEBSDPattern";
            groupBoxEBSDPattern.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(buttonSaveImage);
            flowLayoutPanel1.Controls.Add(checkBoxShowOverlays);
            flowLayoutPanel1.Controls.Add(checkBoxShowDyanmicalEBSD);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // panel3
            // 
            resources.ApplyResources(panel3, "panel3");
            panel3.Name = "panel3";
            // 
            // panel4
            // 
            resources.ApplyResources(panel4, "panel4");
            panel4.Name = "panel4";
            // 
            // FormEBSD
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(groupBoxEBSDPattern);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(groupBoxMasterPattern);
            Controls.Add(tabControl1);
            Controls.Add(flowLayoutPanel2);
            Controls.Add(panel4);
            Controls.Add(statusStrip1);
            Name = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            Load += FormEBSD_Load;
            VisibleChanged += FormEBSD_VisibleChanged;
            flowLayoutPanelViewAlong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).EndInit();
            groupBoxOutput.ResumeLayout(false);
            groupBoxOutput.PerformLayout();
            flowLayoutPanelOutputRange.ResumeLayout(false);
            flowLayoutPanelOutputRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputEnergy).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBoxEBSDGeometry.ResumeLayout(false);
            groupBoxEBSDGeometry.PerformLayout();
            groupBoxSampleCondition.ResumeLayout(false);
            groupBoxSampleCondition.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            groupBoxDetectorOutline.ResumeLayout(false);
            groupBoxDetectorOutline.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBoxLatticePlanes.ResumeLayout(false);
            groupBoxLatticePlanes.PerformLayout();
            groupBoxKikuchiLines.ResumeLayout(false);
            groupBoxKikuchiLines.PerformLayout();
            groupBoxSimulationParameters.ResumeLayout(false);
            groupBoxSimulationParameters.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            flowLayoutPanelMasterPatternSelectors.ResumeLayout(false);
            flowLayoutPanelMasterPatternSelectors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternEnergy).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternDepth).EndInit();
            panelMasterPattern3D.ResumeLayout(false);
            groupBoxMasterPattern.ResumeLayout(false);
            groupBoxMasterPattern.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanelMasterPattern3DViewAlong.ResumeLayout(false);
            flowLayoutPanelMasterPattern3DViewAlong.PerformLayout();
            groupBoxEBSDPattern.ResumeLayout(false);
            groupBoxEBSDPattern.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panelGeometry;
        private NumericBox numericBoxSampleTilt;
        private WaveLengthControl waveLengthControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelViewAlong;
        private System.Windows.Forms.Button buttonViewFromZ;
        private System.Windows.Forms.Button buttonFromX;
        private System.Windows.Forms.Button buttonViewFromSurfaceNormal;
        private System.Windows.Forms.Button buttonCalcBSE;
        private System.Windows.Forms.Button buttonFitNistElasticSampler;
        private PoleFigureControl2 poleFigureControl;
        private System.Windows.Forms.CheckBox checkBoxDrawAxesInStereonet;
        private NumericBox numericBoxDetTilt;
        private NumericBox numericBoxDetRadius;
        private NumericBox numericBoxZofDet;
        private NumericBox numericBoxYofDet;
        private System.Windows.Forms.Label label2;
        // public ImagingSolution.Control.GraphicsBox graphicsBox; // (260322Ch) 旧 GraphicsBox 型
        // public Crystallography.Controls.GraphicBox2 graphicsBox; // (260322Ch) 仮名 GraphicBox2
        public Crystallography.Controls.GraphicsBox graphicsBox; // (260322Ch) 正式名 GraphicBox へ移行
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private NumericBox numericBoxThicknessStep;
        private NumericBox numericBoxMaxNumOfG;
        private NumericBox numericBoxThicknessStart;
        private NumericBox numericBoxThicknessEnd;
        private System.Windows.Forms.Button buttonStop;
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
        private System.Windows.Forms.CheckBox checkBoxShowOverlays;
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
        private System.Windows.Forms.Button buttonViewQuarter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBoxShowDyanmicalEBSD;
        private System.Windows.Forms.CheckBox checkBoxDrawDetectorOutline;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBoxSimulationParameters;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.CheckBox checkBoxNonLocalAbsorption;
        private System.Windows.Forms.CheckBox checkBoxTDSBackground;
        private System.Windows.Forms.CheckBox checkBoxWithBSEDistribution;
        private System.Windows.Forms.GroupBox groupBoxLatticePlanes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOutputRange;
        private System.Windows.Forms.Panel panelMasterPattern3D;
        private ScalablePictureBoxAdvanced scalablePictureBoxAdvancedMasterPattern2D;
        private System.Windows.Forms.Label labelMasterPatternGrid;
        private System.Windows.Forms.ComboBox comboBoxMasterPatternGrid;
        private System.Windows.Forms.Label labelMasterPattern2DHemisphere;
        private System.Windows.Forms.ComboBox comboBoxMasterPattern2DHemisphere;
        private System.Windows.Forms.Label labelMasterPatternEnergy;
        private System.Windows.Forms.TextBox textBoxMasterPatternEnergy;
        private System.Windows.Forms.Label labelMasterPatternEnergyUnit;
        private System.Windows.Forms.TrackBar trackBarMasterPatternEnergy;
        private System.Windows.Forms.Label labelMasterPatternDepth;
        private System.Windows.Forms.TextBox textBoxMasterPatternDepth;
        private System.Windows.Forms.Label labelMasterPatternDepthUnit;
        private System.Windows.Forms.TrackBar trackBarMasterPatternDepth;
        private System.Windows.Forms.Button buttonCreateMasterPattern;
        // private System.Windows.Forms.Label labelMasterPatternInfo; // 260406Cl 廃止
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMasterPatternSelectors;
        private System.Windows.Forms.GroupBox groupBoxMasterPattern;
        private System.Windows.Forms.Button buttonMasterPattern3DCopy;
        private System.Windows.Forms.Button buttonMasterPattern2DCopy;
        private System.Windows.Forms.GroupBox groupBoxSampleCondition;
        private System.Windows.Forms.GroupBox groupBoxEBSDGeometry;
        private System.Windows.Forms.GroupBox groupBoxEBSDPattern;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBoxMasterPattern3DAxisLabel;
        private System.Windows.Forms.CheckBox checkBoxMasterPattern3DAxisArrows;
        private System.Windows.Forms.Panel panelMasterPattern3DAxes;
        private NumericBox numericBoxMasterPattern3DViewAlongU;
        private NumericBox numericBoxMasterPattern3DViewAlongW;
        private System.Windows.Forms.Button buttonMasterPattern3DViewAlong;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMasterPattern3DViewAlong;
        private NumericBox numericBoxMasterPattern3DViewAlongV;
        private System.Windows.Forms.Label labelMasterPattern3DViewAlongOpenBracket;
        private System.Windows.Forms.Label labelMasterPattern3DViewAlongCloseBracket;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.CheckBox checkBoxShowGIndices;
        private System.Windows.Forms.CheckBox checkBoxShowZoneAxisIndices;
        private System.Windows.Forms.CheckBox checkBoxShowKikuchiLines;
        private System.Windows.Forms.GroupBox groupBoxKikuchiLines;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxDetectorOutline;
        private System.Windows.Forms.CheckBox checkBoxShowCircle;
        private System.Windows.Forms.CheckBox checkBoxShowMesh;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
    }
}

