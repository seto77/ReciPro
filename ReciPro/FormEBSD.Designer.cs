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
        // (260520Cl) typo fix: numericBoxKikuchiThreadSholdOfStructureFactor -> numericBoxKikuchiThresholdOfStructureFactor (旧 typo "ThreadShold")
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEBSD));
            toolTip = new System.Windows.Forms.ToolTip(components);
            numericBoxSampleTilt = new NumericBox();
            waveLengthControl = new WaveLengthControl();
            buttonViewQuarter = new System.Windows.Forms.Button();
            buttonViewFromSurfaceNormal = new System.Windows.Forms.Button();
            buttonFromX = new System.Windows.Forms.Button();
            buttonViewFromZ = new System.Windows.Forms.Button();
            buttonSimulateBSE = new System.Windows.Forms.Button();
            buttonFitNistElasticSampler = new System.Windows.Forms.Button();
            checkBoxDrawAxesInStereonet = new System.Windows.Forms.CheckBox();
            numericBoxDetTilt = new NumericBox();
            numericBoxZofDet = new NumericBox();
            numericBoxYofDet = new NumericBox();
            trackBarStrSize = new System.Windows.Forms.TrackBar();
            colorControlExcessLine = new ColorControl();
            trackBarLineWidth = new System.Windows.Forms.TrackBar();
            label11 = new System.Windows.Forms.Label();
            colorControlString = new ColorControl();
            colorControlBackGround = new ColorControl();
            radioButtonKikuchiThresholdOfStructureFactor = new System.Windows.Forms.RadioButton();
            checkBoxKikuchiLine_Kinematical = new System.Windows.Forms.CheckBox();
            radioButtonKikuchiThresholdOfLength = new System.Windows.Forms.RadioButton();
            numericBoxKikuchiThresholdOfStructureFactor = new NumericBox();
            numericBoxKikuchiThresholdOfLength = new NumericBox();
            label1 = new System.Windows.Forms.Label();
            numericBoxThicknessStep = new NumericBox();
            numericBoxMaxNumOfG = new NumericBox();
            checkBoxNonLocalAbsorption = new System.Windows.Forms.CheckBox();
            checkBoxTDSBackground = new System.Windows.Forms.CheckBox();
            numericBoxThicknessStart = new NumericBox();
            numericBoxThicknessEnd = new NumericBox();
            buttonStop = new System.Windows.Forms.Button();
            numericBoxEnergy = new NumericBox();
            trackBarOutputEnergy = new System.Windows.Forms.TrackBar();
            numericBoxDepth = new NumericBox();
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
            buttonCopyImage = new System.Windows.Forms.Button();
            numericBoxEnergyEnd = new NumericBox();
            numericBoxEnergyStart = new NumericBox();
            numericBoxEnergyStep = new NumericBox();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            checkBoxShowDyanmicalEBSD = new System.Windows.Forms.CheckBox();
            checkBoxDrawDetectorOutline = new System.Windows.Forms.CheckBox();
            label15 = new System.Windows.Forms.Label();
            checkBoxShowKikuchiLines = new System.Windows.Forms.CheckBox();
            checkBoxShowGIndices = new System.Windows.Forms.CheckBox();
            checkBoxShowZoneAxisIndices = new System.Windows.Forms.CheckBox();
            labelMasterPatternGrid = new System.Windows.Forms.Label();
            comboBoxMasterPatternGrid = new System.Windows.Forms.ComboBox();
            numericBoxMasterPatternEnergy = new NumericBox();
            trackBarMasterPatternEnergy = new System.Windows.Forms.TrackBar();
            numericBoxMasterPatternDepth = new NumericBox();
            trackBarMasterPatternDepth = new System.Windows.Forms.TrackBar();
            labelMasterPattern2DHemisphere = new System.Windows.Forms.Label();
            comboBoxMasterPattern2DHemisphere = new System.Windows.Forms.ComboBox();
            buttonCreateMasterPattern = new System.Windows.Forms.Button();
            buttonMasterPattern2DCopy = new System.Windows.Forms.Button();
            buttonMasterPattern3DCopy = new System.Windows.Forms.Button();
            checkBoxMasterPattern3DAxisLabel = new System.Windows.Forms.CheckBox();
            checkBoxMasterPattern3DAxisArrows = new System.Windows.Forms.CheckBox();
            buttonMasterPattern3DViewAlong = new System.Windows.Forms.Button();
            indexControl = new IndexControl();
            checkBoxShowMesh = new System.Windows.Forms.CheckBox();
            checkBoxShowCircle = new System.Windows.Forms.CheckBox();
            checkBoxFlipDetectorLeftRight = new System.Windows.Forms.CheckBox();
            panelGeometry = new System.Windows.Forms.Panel();
            flowLayoutPanelViewAlong = new System.Windows.Forms.FlowLayoutPanel();
            graphControlDepthProfile = new GraphControl();
            poleFigureControl = new PoleFigureControl2();
            graphControlEnergyProfile = new GraphControl();
            graphicsBox = new GraphicsBox(components);
            flowLayoutPanel20 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel19 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelOutputRange = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel18 = new System.Windows.Forms.FlowLayoutPanel();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPageGeometry = new System.Windows.Forms.TabPage();
            groupBoxEBSDGeometry = new System.Windows.Forms.GroupBox();
            flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel17 = new System.Windows.Forms.FlowLayoutPanel();
            label5 = new System.Windows.Forms.Label();
            numericBoxDetWidth = new NumericBox();
            numericBoxDetHeight = new NumericBox();
            label6 = new System.Windows.Forms.Label();
            numericBoxDetResolution = new NumericBox();
            flowLayoutPanel16 = new System.Windows.Forms.FlowLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            numericBoxXofDet = new NumericBox();
            groupBoxSampleCondition = new System.Windows.Forms.GroupBox();
            flowLayoutPanel12 = new System.Windows.Forms.FlowLayoutPanel();
            tabPageBseDistribution = new System.Windows.Forms.TabPage();
            tabPageOverlays = new System.Windows.Forms.TabPage();
            flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel1DetectorOutline = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel1KikuchiLines = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxLatticePlanes = new System.Windows.Forms.GroupBox();
            flowLayoutPanel14 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel13 = new System.Windows.Forms.FlowLayoutPanel();
            groupBox2 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel15 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxShowExperimentalImage = new System.Windows.Forms.CheckBox();
            flowLayoutPanelExperimentalImage = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelExpMinInt = new System.Windows.Forms.FlowLayoutPanel();
            labelExpMinInt = new System.Windows.Forms.Label();
            trackBarExpImageMinInt = new System.Windows.Forms.TrackBar();
            flowLayoutPanelExpMaxInt = new System.Windows.Forms.FlowLayoutPanel();
            labelExpMaxInt = new System.Windows.Forms.Label();
            trackBarExpImageMaxInt = new System.Windows.Forms.TrackBar();
            flowLayoutPanelExpOpacity = new System.Windows.Forms.FlowLayoutPanel();
            labelExpOpacity = new System.Windows.Forms.Label();
            trackBarExpImageOpacity = new System.Windows.Forms.TrackBar();
            buttonFindOrientation = new System.Windows.Forms.Button();
            dataGridViewEbsdCandidates = new System.Windows.Forms.DataGridView();
            buttonCalibrateGeometry = new System.Windows.Forms.Button();
            groupBoxSimulationParameters = new System.Windows.Forms.GroupBox();
            flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            scalablePictureBoxAdvancedMasterPattern2D = new ScalablePictureBoxAdvanced();
            flowLayoutPanelMasterPatternSelectors = new System.Windows.Forms.FlowLayoutPanel();
            panelMasterPattern3D = new System.Windows.Forms.Panel();
            panelMasterPattern3DAxes = new System.Windows.Forms.Panel();
            groupBoxMasterPattern = new System.Windows.Forms.GroupBox();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelMasterPattern3DViewAlong = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxEBSDPattern = new System.Windows.Forms.GroupBox();
            tabControl2 = new System.Windows.Forms.TabControl();
            tabPageOutputParameter = new System.Windows.Forms.TabPage();
            tabPageExperimentalImage = new System.Windows.Forms.TabPage();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxResolution = new NumericBox();
            sizeControl = new SizeControl();
            panel1 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputEnergy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternEnergy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternDepth).BeginInit();
            flowLayoutPanelViewAlong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            flowLayoutPanel20.SuspendLayout();
            flowLayoutPanel19.SuspendLayout();
            flowLayoutPanelOutputRange.SuspendLayout();
            flowLayoutPanel18.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageGeometry.SuspendLayout();
            groupBoxEBSDGeometry.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            flowLayoutPanel17.SuspendLayout();
            flowLayoutPanel16.SuspendLayout();
            groupBoxSampleCondition.SuspendLayout();
            flowLayoutPanel12.SuspendLayout();
            tabPageBseDistribution.SuspendLayout();
            tabPageOverlays.SuspendLayout();
            flowLayoutPanel10.SuspendLayout();
            flowLayoutPanel1DetectorOutline.SuspendLayout();
            flowLayoutPanel1KikuchiLines.SuspendLayout();
            groupBoxLatticePlanes.SuspendLayout();
            flowLayoutPanel14.SuspendLayout();
            flowLayoutPanel13.SuspendLayout();
            groupBox2.SuspendLayout();
            flowLayoutPanel15.SuspendLayout();
            flowLayoutPanelExperimentalImage.SuspendLayout();
            flowLayoutPanelExpMinInt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageMinInt).BeginInit();
            flowLayoutPanelExpMaxInt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageMaxInt).BeginInit();
            flowLayoutPanelExpOpacity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageOpacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEbsdCandidates).BeginInit();
            groupBoxSimulationParameters.SuspendLayout();
            flowLayoutPanel9.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            statusStrip1.SuspendLayout();
            flowLayoutPanelMasterPatternSelectors.SuspendLayout();
            panelMasterPattern3D.SuspendLayout();
            groupBoxMasterPattern.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanelMasterPattern3DViewAlong.SuspendLayout();
            groupBoxEBSDPattern.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPageOutputParameter.SuspendLayout();
            tabPageExperimentalImage.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 100;
            // 
            // numericBoxSampleTilt
            // 
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxSampleTilt, "numericBoxSampleTilt");
            numericBoxSampleTilt.Maximum = 0D;
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxSampleTilt, resources.GetString("numericBoxSampleTilt.ToolTip"));
            numericBoxSampleTilt.UpDown_Increment = 10D;
            numericBoxSampleTilt.Value = -70D;
            numericBoxSampleTilt.ValueBoxWidth = 50;
            numericBoxSampleTilt.ValueChanged += numericBoxSampleTilt_ValueChanged;
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            waveLengthControl.Energy = 20D;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = false;
            toolTip.SetToolTip(waveLengthControl, resources.GetString("waveLengthControl.ToolTip"));
            waveLengthControl.WaveLength = 0.008588514105D;
            waveLengthControl.WaveSource = WaveSource.Electron;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            waveLengthControl.WavelengthChanged += waveLengthControl_WavelengthChanged;
            // 
            // buttonViewQuarter
            // 
            resources.ApplyResources(buttonViewQuarter, "buttonViewQuarter");
            buttonViewQuarter.Name = "buttonViewQuarter";
            toolTip.SetToolTip(buttonViewQuarter, resources.GetString("buttonViewQuarter.ToolTip"));
            buttonViewQuarter.UseVisualStyleBackColor = true;
            buttonViewQuarter.Click += buttonViewQuarter_Click;
            // 
            // buttonViewFromSurfaceNormal
            // 
            resources.ApplyResources(buttonViewFromSurfaceNormal, "buttonViewFromSurfaceNormal");
            buttonViewFromSurfaceNormal.Name = "buttonViewFromSurfaceNormal";
            toolTip.SetToolTip(buttonViewFromSurfaceNormal, resources.GetString("buttonViewFromSurfaceNormal.ToolTip"));
            buttonViewFromSurfaceNormal.UseVisualStyleBackColor = true;
            buttonViewFromSurfaceNormal.Click += buttonFromSurfaceNormal_Click;
            // 
            // buttonFromX
            // 
            resources.ApplyResources(buttonFromX, "buttonFromX");
            buttonFromX.Name = "buttonFromX";
            toolTip.SetToolTip(buttonFromX, resources.GetString("buttonFromX.ToolTip"));
            buttonFromX.UseVisualStyleBackColor = true;
            buttonFromX.Click += buttonViewFromX_Click;
            // 
            // buttonViewFromZ
            // 
            resources.ApplyResources(buttonViewFromZ, "buttonViewFromZ");
            buttonViewFromZ.Name = "buttonViewFromZ";
            toolTip.SetToolTip(buttonViewFromZ, resources.GetString("buttonViewFromZ.ToolTip"));
            buttonViewFromZ.UseVisualStyleBackColor = true;
            buttonViewFromZ.Click += buttonViewFromZ_Click;
            // 
            // buttonSimulateBSE
            // 
            resources.ApplyResources(buttonSimulateBSE, "buttonSimulateBSE");
            buttonSimulateBSE.BackColor = System.Drawing.Color.SteelBlue;
            buttonSimulateBSE.ForeColor = System.Drawing.Color.White;
            buttonSimulateBSE.Name = "buttonSimulateBSE";
            toolTip.SetToolTip(buttonSimulateBSE, resources.GetString("buttonSimulateBSE.ToolTip"));
            buttonSimulateBSE.UseVisualStyleBackColor = false;
            buttonSimulateBSE.Click += buttonBSE_Click;
            // 
            // buttonFitNistElasticSampler
            // 
            resources.ApplyResources(buttonFitNistElasticSampler, "buttonFitNistElasticSampler");
            buttonFitNistElasticSampler.Name = "buttonFitNistElasticSampler";
            toolTip.SetToolTip(buttonFitNistElasticSampler, resources.GetString("buttonFitNistElasticSampler.ToolTip"));
            buttonFitNistElasticSampler.UseVisualStyleBackColor = true;
            buttonFitNistElasticSampler.Click += buttonFitNistElasticSampler_Click;
            // 
            // checkBoxDrawAxesInStereonet
            // 
            resources.ApplyResources(checkBoxDrawAxesInStereonet, "checkBoxDrawAxesInStereonet");
            checkBoxDrawAxesInStereonet.BackColor = System.Drawing.Color.White;
            checkBoxDrawAxesInStereonet.Checked = true;
            checkBoxDrawAxesInStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxesInStereonet.Name = "checkBoxDrawAxesInStereonet";
            toolTip.SetToolTip(checkBoxDrawAxesInStereonet, resources.GetString("checkBoxDrawAxesInStereonet.ToolTip"));
            checkBoxDrawAxesInStereonet.UseVisualStyleBackColor = false;
            // 
            // numericBoxDetTilt
            // 
            numericBoxDetTilt.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxDetTilt, "numericBoxDetTilt");
            numericBoxDetTilt.Maximum = 180D;
            numericBoxDetTilt.Minimum = 0D;
            numericBoxDetTilt.Name = "numericBoxDetTilt";
            numericBoxDetTilt.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxDetTilt, resources.GetString("numericBoxDetTilt.ToolTip"));
            numericBoxDetTilt.UpDown_Increment = 10D;
            numericBoxDetTilt.Value = 90D;
            numericBoxDetTilt.ValueBoxWidth = 50;
            numericBoxDetTilt.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // numericBoxZofDet
            // 
            numericBoxZofDet.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxZofDet, "numericBoxZofDet");
            numericBoxZofDet.Maximum = 1000D;
            numericBoxZofDet.Minimum = -1000D;
            numericBoxZofDet.Name = "numericBoxZofDet";
            numericBoxZofDet.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxZofDet, resources.GetString("numericBoxZofDet.ToolTip"));
            numericBoxZofDet.Value = 27D;
            numericBoxZofDet.ValueBoxWidth = 55;
            numericBoxZofDet.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // numericBoxYofDet
            // 
            numericBoxYofDet.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxYofDet, "numericBoxYofDet");
            numericBoxYofDet.Maximum = 1000D;
            numericBoxYofDet.Minimum = -1000D;
            numericBoxYofDet.Name = "numericBoxYofDet";
            numericBoxYofDet.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxYofDet, resources.GetString("numericBoxYofDet.ToolTip"));
            numericBoxYofDet.Value = -36D;
            numericBoxYofDet.ValueBoxWidth = 55;
            numericBoxYofDet.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
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
            toolTip.SetToolTip(trackBarStrSize, resources.GetString("trackBarStrSize.ToolTip"));
            trackBarStrSize.Value = 80;
            trackBarStrSize.ValueChanged += colorControlExcessLine_ColorChanged;
            // 
            // colorControlExcessLine
            // 
            resources.ApplyResources(colorControlExcessLine, "colorControlExcessLine");
            colorControlExcessLine.BackColor = System.Drawing.SystemColors.Control;
            colorControlExcessLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControlExcessLine.Color = System.Drawing.Color.FromArgb(255, 128, 0);
            colorControlExcessLine.Name = "colorControlExcessLine";
            toolTip.SetToolTip(colorControlExcessLine, resources.GetString("colorControlExcessLine.ToolTip"));
            colorControlExcessLine.ColorChanged += colorControlExcessLine_ColorChanged;
            colorControlExcessLine.Load += colorControlExcessLine_Load;
            // 
            // trackBarLineWidth
            // 
            resources.ApplyResources(trackBarLineWidth, "trackBarLineWidth");
            trackBarLineWidth.Maximum = 10000;
            trackBarLineWidth.Minimum = 1;
            trackBarLineWidth.Name = "trackBarLineWidth";
            trackBarLineWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarLineWidth, resources.GetString("trackBarLineWidth.ToolTip"));
            trackBarLineWidth.Value = 1;
            trackBarLineWidth.ValueChanged += colorControlExcessLine_ColorChanged;
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip"));
            // 
            // colorControlString
            // 
            resources.ApplyResources(colorControlString, "colorControlString");
            colorControlString.BackColor = System.Drawing.SystemColors.Control;
            colorControlString.BoxSize = new System.Drawing.Size(20, 20);
            colorControlString.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlString.Name = "colorControlString";
            toolTip.SetToolTip(colorControlString, resources.GetString("colorControlString.ToolTip"));
            colorControlString.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // colorControlBackGround
            // 
            resources.ApplyResources(colorControlBackGround, "colorControlBackGround");
            colorControlBackGround.BackColor = System.Drawing.SystemColors.Control;
            colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(32, 32, 32);
            colorControlBackGround.Name = "colorControlBackGround";
            toolTip.SetToolTip(colorControlBackGround, resources.GetString("colorControlBackGround.ToolTip"));
            colorControlBackGround.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // radioButtonKikuchiThresholdOfStructureFactor
            // 
            resources.ApplyResources(radioButtonKikuchiThresholdOfStructureFactor, "radioButtonKikuchiThresholdOfStructureFactor");
            radioButtonKikuchiThresholdOfStructureFactor.Checked = true;
            radioButtonKikuchiThresholdOfStructureFactor.Name = "radioButtonKikuchiThresholdOfStructureFactor";
            radioButtonKikuchiThresholdOfStructureFactor.TabStop = true;
            toolTip.SetToolTip(radioButtonKikuchiThresholdOfStructureFactor, resources.GetString("radioButtonKikuchiThresholdOfStructureFactor.ToolTip"));
            radioButtonKikuchiThresholdOfStructureFactor.UseVisualStyleBackColor = true;
            radioButtonKikuchiThresholdOfStructureFactor.CheckedChanged += radioButtonKikuchiThresholdOfStructureFactor_CheckedChanged;
            // 
            // checkBoxKikuchiLine_Kinematical
            // 
            resources.ApplyResources(checkBoxKikuchiLine_Kinematical, "checkBoxKikuchiLine_Kinematical");
            checkBoxKikuchiLine_Kinematical.Checked = true;
            checkBoxKikuchiLine_Kinematical.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxKikuchiLine_Kinematical.Name = "checkBoxKikuchiLine_Kinematical";
            toolTip.SetToolTip(checkBoxKikuchiLine_Kinematical, resources.GetString("checkBoxKikuchiLine_Kinematical.ToolTip"));
            checkBoxKikuchiLine_Kinematical.UseVisualStyleBackColor = true;
            checkBoxKikuchiLine_Kinematical.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // radioButtonKikuchiThresholdOfLength
            // 
            resources.ApplyResources(radioButtonKikuchiThresholdOfLength, "radioButtonKikuchiThresholdOfLength");
            radioButtonKikuchiThresholdOfLength.Name = "radioButtonKikuchiThresholdOfLength";
            toolTip.SetToolTip(radioButtonKikuchiThresholdOfLength, resources.GetString("radioButtonKikuchiThresholdOfLength.ToolTip"));
            radioButtonKikuchiThresholdOfLength.UseVisualStyleBackColor = true;
            // 
            // numericBoxKikuchiThresholdOfStructureFactor
            // 
            numericBoxKikuchiThresholdOfStructureFactor.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxKikuchiThresholdOfStructureFactor, "numericBoxKikuchiThresholdOfStructureFactor");
            numericBoxKikuchiThresholdOfStructureFactor.Maximum = 1000D;
            numericBoxKikuchiThresholdOfStructureFactor.Minimum = 1D;
            numericBoxKikuchiThresholdOfStructureFactor.Name = "numericBoxKikuchiThresholdOfStructureFactor";
            numericBoxKikuchiThresholdOfStructureFactor.ShowUpDown = true;
            numericBoxKikuchiThresholdOfStructureFactor.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxKikuchiThresholdOfStructureFactor, resources.GetString("numericBoxKikuchiThresholdOfStructureFactor.ToolTip"));
            numericBoxKikuchiThresholdOfStructureFactor.Value = 40D;
            numericBoxKikuchiThresholdOfStructureFactor.ValueChanged += numericBoxKikuchiThresholdOfStructureFactor_ValueChanged;
            // 
            // numericBoxKikuchiThresholdOfLength
            // 
            numericBoxKikuchiThresholdOfLength.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxKikuchiThresholdOfLength, "numericBoxKikuchiThresholdOfLength");
            numericBoxKikuchiThresholdOfLength.Maximum = 100D;
            numericBoxKikuchiThresholdOfLength.Minimum = 0D;
            numericBoxKikuchiThresholdOfLength.Name = "numericBoxKikuchiThresholdOfLength";
            numericBoxKikuchiThresholdOfLength.ShowUpDown = true;
            numericBoxKikuchiThresholdOfLength.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxKikuchiThresholdOfLength, resources.GetString("numericBoxKikuchiThresholdOfLength.ToolTip"));
            numericBoxKikuchiThresholdOfLength.Value = 10D;
            numericBoxKikuchiThresholdOfLength.ValueChanged += numericBoxKikuchiThresholdOfStructureFactor_ValueChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // numericBoxThicknessStep
            // 
            numericBoxThicknessStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxThicknessStep, "numericBoxThicknessStep");
            numericBoxThicknessStep.Maximum = 10D;
            numericBoxThicknessStep.Minimum = 0.001D;
            numericBoxThicknessStep.Name = "numericBoxThicknessStep";
            numericBoxThicknessStep.ShowUpDown = true;
            numericBoxThicknessStep.SmartIncrement = true;
            numericBoxThicknessStep.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessStep, resources.GetString("numericBoxThicknessStep.ToolTip"));
            numericBoxThicknessStep.Value = 1D;
            numericBoxThicknessStep.ValueBoxWidth = 40;
            numericBoxThicknessStep.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxMaxNumOfG
            // 
            numericBoxMaxNumOfG.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxMaxNumOfG, "numericBoxMaxNumOfG");
            numericBoxMaxNumOfG.Maximum = 2048D;
            numericBoxMaxNumOfG.Minimum = 1D;
            numericBoxMaxNumOfG.Name = "numericBoxMaxNumOfG";
            numericBoxMaxNumOfG.ShowUpDown = true;
            numericBoxMaxNumOfG.SmartIncrement = true;
            numericBoxMaxNumOfG.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxMaxNumOfG, resources.GetString("numericBoxMaxNumOfG.ToolTip"));
            numericBoxMaxNumOfG.Value = 32D;
            numericBoxMaxNumOfG.ValueBoxWidth = 40;
            // 
            // checkBoxNonLocalAbsorption
            // 
            resources.ApplyResources(checkBoxNonLocalAbsorption, "checkBoxNonLocalAbsorption");
            checkBoxNonLocalAbsorption.Name = "checkBoxNonLocalAbsorption";
            toolTip.SetToolTip(checkBoxNonLocalAbsorption, resources.GetString("checkBoxNonLocalAbsorption.ToolTip"));
            checkBoxNonLocalAbsorption.UseVisualStyleBackColor = true;
            // 
            // checkBoxTDSBackground
            // 
            resources.ApplyResources(checkBoxTDSBackground, "checkBoxTDSBackground");
            checkBoxTDSBackground.Name = "checkBoxTDSBackground";
            toolTip.SetToolTip(checkBoxTDSBackground, resources.GetString("checkBoxTDSBackground.ToolTip"));
            checkBoxTDSBackground.UseVisualStyleBackColor = true;
            // 
            // numericBoxThicknessStart
            // 
            numericBoxThicknessStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxThicknessStart, "numericBoxThicknessStart");
            numericBoxThicknessStart.Maximum = 1000D;
            numericBoxThicknessStart.Minimum = 0.001D;
            numericBoxThicknessStart.Name = "numericBoxThicknessStart";
            numericBoxThicknessStart.ShowUpDown = true;
            numericBoxThicknessStart.SmartIncrement = true;
            numericBoxThicknessStart.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessStart, resources.GetString("numericBoxThicknessStart.ToolTip"));
            numericBoxThicknessStart.Value = 1D;
            numericBoxThicknessStart.ValueBoxWidth = 40;
            numericBoxThicknessStart.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxThicknessEnd
            // 
            numericBoxThicknessEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxThicknessEnd, "numericBoxThicknessEnd");
            numericBoxThicknessEnd.Maximum = 1000D;
            numericBoxThicknessEnd.Minimum = 0.001D;
            numericBoxThicknessEnd.Name = "numericBoxThicknessEnd";
            numericBoxThicknessEnd.ShowUpDown = true;
            numericBoxThicknessEnd.SmartIncrement = true;
            numericBoxThicknessEnd.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessEnd, resources.GetString("numericBoxThicknessEnd.ToolTip"));
            numericBoxThicknessEnd.Value = 50D;
            numericBoxThicknessEnd.ValueBoxWidth = 40;
            numericBoxThicknessEnd.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // buttonStop
            // 
            resources.ApplyResources(buttonStop, "buttonStop");
            buttonStop.BackColor = System.Drawing.Color.IndianRed;
            buttonStop.ForeColor = System.Drawing.Color.White;
            buttonStop.Name = "buttonStop";
            toolTip.SetToolTip(buttonStop, resources.GetString("buttonStop.ToolTip"));
            buttonStop.UseVisualStyleBackColor = false;
            // 
            // numericBoxEnergy
            // 
            numericBoxEnergy.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxEnergy, "numericBoxEnergy");
            numericBoxEnergy.Name = "numericBoxEnergy";
            numericBoxEnergy.ReadOnly = true;
            toolTip.SetToolTip(numericBoxEnergy, resources.GetString("numericBoxEnergy.ToolTip"));
            numericBoxEnergy.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarOutputEnergy
            // 
            resources.ApplyResources(trackBarOutputEnergy, "trackBarOutputEnergy");
            trackBarOutputEnergy.LargeChange = 1;
            trackBarOutputEnergy.Maximum = 5;
            trackBarOutputEnergy.Name = "trackBarOutputEnergy";
            toolTip.SetToolTip(trackBarOutputEnergy, resources.GetString("trackBarOutputEnergy.ToolTip"));
            trackBarOutputEnergy.ValueChanged += trackBarOutputEnergy_ValueChanged;
            // 
            // numericBoxDepth
            // 
            numericBoxDepth.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxDepth, "numericBoxDepth");
            numericBoxDepth.Name = "numericBoxDepth";
            numericBoxDepth.ReadOnly = true;
            toolTip.SetToolTip(numericBoxDepth, resources.GetString("numericBoxDepth.ToolTip"));
            numericBoxDepth.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarOutputThickness
            // 
            resources.ApplyResources(trackBarOutputThickness, "trackBarOutputThickness");
            trackBarOutputThickness.LargeChange = 1;
            trackBarOutputThickness.Maximum = 9;
            trackBarOutputThickness.Name = "trackBarOutputThickness";
            toolTip.SetToolTip(trackBarOutputThickness, resources.GetString("trackBarOutputThickness.ToolTip"));
            trackBarOutputThickness.ValueChanged += TrackBarOutputThickness_Scroll;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // checkBoxWithBSEDistribution
            // 
            resources.ApplyResources(checkBoxWithBSEDistribution, "checkBoxWithBSEDistribution");
            checkBoxWithBSEDistribution.Checked = true;
            checkBoxWithBSEDistribution.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxWithBSEDistribution.Name = "checkBoxWithBSEDistribution";
            toolTip.SetToolTip(checkBoxWithBSEDistribution, resources.GetString("checkBoxWithBSEDistribution.ToolTip"));
            checkBoxWithBSEDistribution.UseVisualStyleBackColor = true;
            checkBoxWithBSEDistribution.CheckedChanged += checkBoxWithBSEDistribution_CheckedChanged;
            // 
            // comboBoxGradient
            // 
            comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxGradient, "comboBoxGradient");
            comboBoxGradient.FormattingEnabled = true;
            comboBoxGradient.Items.AddRange(new object[] { resources.GetString("comboBoxGradient.Items"), resources.GetString("comboBoxGradient.Items1") });
            comboBoxGradient.Name = "comboBoxGradient";
            toolTip.SetToolTip(comboBoxGradient, resources.GetString("comboBoxGradient.ToolTip"));
            comboBoxGradient.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // comboBoxScale
            // 
            comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxScale, "comboBoxScale");
            comboBoxScale.FormattingEnabled = true;
            comboBoxScale.Items.AddRange(new object[] { resources.GetString("comboBoxScale.Items"), resources.GetString("comboBoxScale.Items1"), resources.GetString("comboBoxScale.Items2"), resources.GetString("comboBoxScale.Items3") });
            comboBoxScale.Name = "comboBoxScale";
            toolTip.SetToolTip(comboBoxScale, resources.GetString("comboBoxScale.ToolTip"));
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
            toolTip.SetToolTip(trackBarIntensityBrightnessMax, resources.GetString("trackBarIntensityBrightnessMax.ToolTip"));
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
            toolTip.SetToolTip(trackBarIntensityBrightnessMin, resources.GetString("trackBarIntensityBrightnessMin.ToolTip"));
            trackBarIntensityBrightnessMin.ValueChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            toolTip.SetToolTip(label8, resources.GetString("label8.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            toolTip.SetToolTip(label10, resources.GetString("label10.ToolTip"));
            // 
            // checkBoxShowOverlays
            // 
            resources.ApplyResources(checkBoxShowOverlays, "checkBoxShowOverlays");
            checkBoxShowOverlays.Checked = true;
            checkBoxShowOverlays.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowOverlays.Name = "checkBoxShowOverlays";
            toolTip.SetToolTip(checkBoxShowOverlays, resources.GetString("checkBoxShowOverlays.ToolTip"));
            checkBoxShowOverlays.UseVisualStyleBackColor = true;
            checkBoxShowOverlays.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // buttonCopyImage
            // 
            resources.ApplyResources(buttonCopyImage, "buttonCopyImage");
            buttonCopyImage.Name = "buttonCopyImage";
            toolTip.SetToolTip(buttonCopyImage, resources.GetString("buttonCopyImage.ToolTip"));
            buttonCopyImage.UseVisualStyleBackColor = true;
            buttonCopyImage.Click += buttonCopyImage_Click;
            // 
            // numericBoxEnergyEnd
            // 
            numericBoxEnergyEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyEnd.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxEnergyEnd, "numericBoxEnergyEnd");
            numericBoxEnergyEnd.Maximum = 1000D;
            numericBoxEnergyEnd.Minimum = 0.001D;
            numericBoxEnergyEnd.Name = "numericBoxEnergyEnd";
            numericBoxEnergyEnd.ShowUpDown = true;
            numericBoxEnergyEnd.SmartIncrement = true;
            numericBoxEnergyEnd.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxEnergyEnd, resources.GetString("numericBoxEnergyEnd.ToolTip"));
            numericBoxEnergyEnd.Value = 15D;
            numericBoxEnergyEnd.ValueBoxWidth = 45;
            numericBoxEnergyEnd.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // numericBoxEnergyStart
            // 
            numericBoxEnergyStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxEnergyStart, "numericBoxEnergyStart");
            numericBoxEnergyStart.Maximum = 1000D;
            numericBoxEnergyStart.Minimum = 1D;
            numericBoxEnergyStart.Name = "numericBoxEnergyStart";
            numericBoxEnergyStart.ShowUpDown = true;
            numericBoxEnergyStart.SmartIncrement = true;
            numericBoxEnergyStart.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxEnergyStart, resources.GetString("numericBoxEnergyStart.ToolTip"));
            numericBoxEnergyStart.Value = 20D;
            numericBoxEnergyStart.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.ValueBoxWidth = 45;
            numericBoxEnergyStart.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // numericBoxEnergyStep
            // 
            numericBoxEnergyStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStep.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxEnergyStep, "numericBoxEnergyStep");
            numericBoxEnergyStep.Maximum = 10D;
            numericBoxEnergyStep.Minimum = 0.001D;
            numericBoxEnergyStep.Name = "numericBoxEnergyStep";
            numericBoxEnergyStep.ShowUpDown = true;
            numericBoxEnergyStep.SmartIncrement = true;
            numericBoxEnergyStep.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxEnergyStep, resources.GetString("numericBoxEnergyStep.ToolTip"));
            numericBoxEnergyStep.Value = 1D;
            numericBoxEnergyStep.ValueBoxWidth = 40;
            numericBoxEnergyStep.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.BackColor = System.Drawing.Color.White;
            label13.Name = "label13";
            toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip"));
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            label14.BackColor = System.Drawing.Color.White;
            label14.Name = "label14";
            toolTip.SetToolTip(label14, resources.GetString("label14.ToolTip"));
            // 
            // checkBoxShowDyanmicalEBSD
            // 
            resources.ApplyResources(checkBoxShowDyanmicalEBSD, "checkBoxShowDyanmicalEBSD");
            checkBoxShowDyanmicalEBSD.Checked = true;
            checkBoxShowDyanmicalEBSD.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowDyanmicalEBSD.Name = "checkBoxShowDyanmicalEBSD";
            toolTip.SetToolTip(checkBoxShowDyanmicalEBSD, resources.GetString("checkBoxShowDyanmicalEBSD.ToolTip"));
            checkBoxShowDyanmicalEBSD.UseVisualStyleBackColor = true;
            checkBoxShowDyanmicalEBSD.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxDrawDetectorOutline
            // 
            resources.ApplyResources(checkBoxDrawDetectorOutline, "checkBoxDrawDetectorOutline");
            checkBoxDrawDetectorOutline.Checked = true;
            checkBoxDrawDetectorOutline.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawDetectorOutline.Name = "checkBoxDrawDetectorOutline";
            toolTip.SetToolTip(checkBoxDrawDetectorOutline, resources.GetString("checkBoxDrawDetectorOutline.ToolTip"));
            checkBoxDrawDetectorOutline.UseVisualStyleBackColor = true;
            checkBoxDrawDetectorOutline.CheckedChanged += checkBoxDrawDetectorOutline_CheckedChanged;
            // 
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            label15.BackColor = System.Drawing.Color.White;
            label15.Name = "label15";
            toolTip.SetToolTip(label15, resources.GetString("label15.ToolTip"));
            // 
            // checkBoxShowKikuchiLines
            // 
            resources.ApplyResources(checkBoxShowKikuchiLines, "checkBoxShowKikuchiLines");
            checkBoxShowKikuchiLines.Checked = true;
            checkBoxShowKikuchiLines.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowKikuchiLines.Name = "checkBoxShowKikuchiLines";
            toolTip.SetToolTip(checkBoxShowKikuchiLines, resources.GetString("checkBoxShowKikuchiLines.ToolTip"));
            checkBoxShowKikuchiLines.UseVisualStyleBackColor = true;
            checkBoxShowKikuchiLines.CheckedChanged += checkBoxShowKikuchiLines_CheckedChanged;
            // 
            // checkBoxShowGIndices
            // 
            resources.ApplyResources(checkBoxShowGIndices, "checkBoxShowGIndices");
            checkBoxShowGIndices.Name = "checkBoxShowGIndices";
            toolTip.SetToolTip(checkBoxShowGIndices, resources.GetString("checkBoxShowGIndices.ToolTip"));
            checkBoxShowGIndices.UseVisualStyleBackColor = true;
            checkBoxShowGIndices.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxShowZoneAxisIndices
            // 
            resources.ApplyResources(checkBoxShowZoneAxisIndices, "checkBoxShowZoneAxisIndices");
            checkBoxShowZoneAxisIndices.Checked = true;
            checkBoxShowZoneAxisIndices.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowZoneAxisIndices.Name = "checkBoxShowZoneAxisIndices";
            toolTip.SetToolTip(checkBoxShowZoneAxisIndices, resources.GetString("checkBoxShowZoneAxisIndices.ToolTip"));
            checkBoxShowZoneAxisIndices.UseVisualStyleBackColor = true;
            checkBoxShowZoneAxisIndices.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // labelMasterPatternGrid
            // 
            resources.ApplyResources(labelMasterPatternGrid, "labelMasterPatternGrid");
            labelMasterPatternGrid.Name = "labelMasterPatternGrid";
            toolTip.SetToolTip(labelMasterPatternGrid, resources.GetString("labelMasterPatternGrid.ToolTip"));
            // 
            // comboBoxMasterPatternGrid
            // 
            comboBoxMasterPatternGrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxMasterPatternGrid, "comboBoxMasterPatternGrid");
            comboBoxMasterPatternGrid.FormattingEnabled = true;
            comboBoxMasterPatternGrid.Items.AddRange(new object[] { resources.GetString("comboBoxMasterPatternGrid.Items"), resources.GetString("comboBoxMasterPatternGrid.Items1"), resources.GetString("comboBoxMasterPatternGrid.Items2"), resources.GetString("comboBoxMasterPatternGrid.Items3"), resources.GetString("comboBoxMasterPatternGrid.Items4"), resources.GetString("comboBoxMasterPatternGrid.Items5"), resources.GetString("comboBoxMasterPatternGrid.Items6"), resources.GetString("comboBoxMasterPatternGrid.Items7"), resources.GetString("comboBoxMasterPatternGrid.Items8"), resources.GetString("comboBoxMasterPatternGrid.Items9") });
            comboBoxMasterPatternGrid.Name = "comboBoxMasterPatternGrid";
            toolTip.SetToolTip(comboBoxMasterPatternGrid, resources.GetString("comboBoxMasterPatternGrid.ToolTip"));
            comboBoxMasterPatternGrid.SelectedIndexChanged += MasterPatternSelectionChanged;
            // 
            // numericBoxMasterPatternEnergy
            // 
            numericBoxMasterPatternEnergy.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxMasterPatternEnergy, "numericBoxMasterPatternEnergy");
            numericBoxMasterPatternEnergy.Name = "numericBoxMasterPatternEnergy";
            numericBoxMasterPatternEnergy.ReadOnly = true;
            toolTip.SetToolTip(numericBoxMasterPatternEnergy, resources.GetString("numericBoxMasterPatternEnergy.ToolTip"));
            numericBoxMasterPatternEnergy.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarMasterPatternEnergy
            // 
            resources.ApplyResources(trackBarMasterPatternEnergy, "trackBarMasterPatternEnergy");
            trackBarMasterPatternEnergy.LargeChange = 1;
            trackBarMasterPatternEnergy.Maximum = 0;
            trackBarMasterPatternEnergy.Name = "trackBarMasterPatternEnergy";
            toolTip.SetToolTip(trackBarMasterPatternEnergy, resources.GetString("trackBarMasterPatternEnergy.ToolTip"));
            trackBarMasterPatternEnergy.ValueChanged += MasterPatternSelectionChanged;
            // 
            // numericBoxMasterPatternDepth
            // 
            numericBoxMasterPatternDepth.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxMasterPatternDepth, "numericBoxMasterPatternDepth");
            numericBoxMasterPatternDepth.Name = "numericBoxMasterPatternDepth";
            numericBoxMasterPatternDepth.ReadOnly = true;
            toolTip.SetToolTip(numericBoxMasterPatternDepth, resources.GetString("numericBoxMasterPatternDepth.ToolTip"));
            numericBoxMasterPatternDepth.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarMasterPatternDepth
            // 
            resources.ApplyResources(trackBarMasterPatternDepth, "trackBarMasterPatternDepth");
            trackBarMasterPatternDepth.LargeChange = 1;
            trackBarMasterPatternDepth.Maximum = 0;
            trackBarMasterPatternDepth.Name = "trackBarMasterPatternDepth";
            toolTip.SetToolTip(trackBarMasterPatternDepth, resources.GetString("trackBarMasterPatternDepth.ToolTip"));
            trackBarMasterPatternDepth.ValueChanged += MasterPatternSelectionChanged;
            // 
            // labelMasterPattern2DHemisphere
            // 
            resources.ApplyResources(labelMasterPattern2DHemisphere, "labelMasterPattern2DHemisphere");
            labelMasterPattern2DHemisphere.Name = "labelMasterPattern2DHemisphere";
            toolTip.SetToolTip(labelMasterPattern2DHemisphere, resources.GetString("labelMasterPattern2DHemisphere.ToolTip"));
            // 
            // comboBoxMasterPattern2DHemisphere
            // 
            comboBoxMasterPattern2DHemisphere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxMasterPattern2DHemisphere, "comboBoxMasterPattern2DHemisphere");
            comboBoxMasterPattern2DHemisphere.FormattingEnabled = true;
            comboBoxMasterPattern2DHemisphere.Items.AddRange(new object[] { resources.GetString("comboBoxMasterPattern2DHemisphere.Items"), resources.GetString("comboBoxMasterPattern2DHemisphere.Items1") });
            comboBoxMasterPattern2DHemisphere.Name = "comboBoxMasterPattern2DHemisphere";
            toolTip.SetToolTip(comboBoxMasterPattern2DHemisphere, resources.GetString("comboBoxMasterPattern2DHemisphere.ToolTip"));
            comboBoxMasterPattern2DHemisphere.SelectedIndexChanged += MasterPatternSelectionChanged;
            // 
            // buttonCreateMasterPattern
            // 
            resources.ApplyResources(buttonCreateMasterPattern, "buttonCreateMasterPattern");
            buttonCreateMasterPattern.BackColor = System.Drawing.Color.SteelBlue;
            buttonCreateMasterPattern.ForeColor = System.Drawing.Color.White;
            buttonCreateMasterPattern.Name = "buttonCreateMasterPattern";
            toolTip.SetToolTip(buttonCreateMasterPattern, resources.GetString("buttonCreateMasterPattern.ToolTip"));
            buttonCreateMasterPattern.UseVisualStyleBackColor = false;
            buttonCreateMasterPattern.Click += buttonCreateMasterPattern_Click;
            // 
            // buttonMasterPattern2DCopy
            // 
            resources.ApplyResources(buttonMasterPattern2DCopy, "buttonMasterPattern2DCopy");
            buttonMasterPattern2DCopy.Name = "buttonMasterPattern2DCopy";
            toolTip.SetToolTip(buttonMasterPattern2DCopy, resources.GetString("buttonMasterPattern2DCopy.ToolTip"));
            buttonMasterPattern2DCopy.UseVisualStyleBackColor = true;
            // 
            // buttonMasterPattern3DCopy
            // 
            resources.ApplyResources(buttonMasterPattern3DCopy, "buttonMasterPattern3DCopy");
            buttonMasterPattern3DCopy.Name = "buttonMasterPattern3DCopy";
            toolTip.SetToolTip(buttonMasterPattern3DCopy, resources.GetString("buttonMasterPattern3DCopy.ToolTip"));
            buttonMasterPattern3DCopy.UseVisualStyleBackColor = true;
            // 
            // checkBoxMasterPattern3DAxisLabel
            // 
            resources.ApplyResources(checkBoxMasterPattern3DAxisLabel, "checkBoxMasterPattern3DAxisLabel");
            checkBoxMasterPattern3DAxisLabel.Checked = true;
            checkBoxMasterPattern3DAxisLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxMasterPattern3DAxisLabel.Name = "checkBoxMasterPattern3DAxisLabel";
            toolTip.SetToolTip(checkBoxMasterPattern3DAxisLabel, resources.GetString("checkBoxMasterPattern3DAxisLabel.ToolTip"));
            checkBoxMasterPattern3DAxisLabel.UseVisualStyleBackColor = true;
            checkBoxMasterPattern3DAxisLabel.CheckedChanged += checkBoxMasterPattern3DAxisLabel_CheckedChanged;
            // 
            // checkBoxMasterPattern3DAxisArrows
            // 
            resources.ApplyResources(checkBoxMasterPattern3DAxisArrows, "checkBoxMasterPattern3DAxisArrows");
            checkBoxMasterPattern3DAxisArrows.Checked = true;
            checkBoxMasterPattern3DAxisArrows.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxMasterPattern3DAxisArrows.Name = "checkBoxMasterPattern3DAxisArrows";
            toolTip.SetToolTip(checkBoxMasterPattern3DAxisArrows, resources.GetString("checkBoxMasterPattern3DAxisArrows.ToolTip"));
            checkBoxMasterPattern3DAxisArrows.UseVisualStyleBackColor = true;
            checkBoxMasterPattern3DAxisArrows.CheckedChanged += checkBoxMasterPattern3DAxisArrows_CheckedChanged;
            // 
            // buttonMasterPattern3DViewAlong
            // 
            resources.ApplyResources(buttonMasterPattern3DViewAlong, "buttonMasterPattern3DViewAlong");
            buttonMasterPattern3DViewAlong.Name = "buttonMasterPattern3DViewAlong";
            toolTip.SetToolTip(buttonMasterPattern3DViewAlong, resources.GetString("buttonMasterPattern3DViewAlong.ToolTip"));
            buttonMasterPattern3DViewAlong.UseVisualStyleBackColor = true;
            buttonMasterPattern3DViewAlong.Click += buttonMasterPattern3DViewAlong_Click;
            // 
            // indexControl
            // 
            resources.ApplyResources(indexControl, "indexControl");
            indexControl.LabelVisible = false;
            indexControl.Name = "indexControl";
            toolTip.SetToolTip(indexControl, resources.GetString("indexControl.ToolTip"));
            // 
            // checkBoxShowMesh
            // 
            resources.ApplyResources(checkBoxShowMesh, "checkBoxShowMesh");
            checkBoxShowMesh.Name = "checkBoxShowMesh";
            toolTip.SetToolTip(checkBoxShowMesh, resources.GetString("checkBoxShowMesh.ToolTip"));
            checkBoxShowMesh.UseVisualStyleBackColor = true;
            checkBoxShowMesh.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxShowCircle
            // 
            resources.ApplyResources(checkBoxShowCircle, "checkBoxShowCircle");
            checkBoxShowCircle.Checked = true;
            checkBoxShowCircle.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowCircle.Name = "checkBoxShowCircle";
            toolTip.SetToolTip(checkBoxShowCircle, resources.GetString("checkBoxShowCircle.ToolTip"));
            checkBoxShowCircle.UseVisualStyleBackColor = true;
            checkBoxShowCircle.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxFlipDetectorLeftRight
            // 
            resources.ApplyResources(checkBoxFlipDetectorLeftRight, "checkBoxFlipDetectorLeftRight");
            checkBoxFlipDetectorLeftRight.Name = "checkBoxFlipDetectorLeftRight";
            checkBoxFlipDetectorLeftRight.UseVisualStyleBackColor = true;
            checkBoxFlipDetectorLeftRight.CheckedChanged += checkBoxFlipDetectorLeftRight_CheckedChanged;
            // 
            // panelGeometry
            // 
            captureExtender.SetCapture(panelGeometry, true);
            resources.ApplyResources(panelGeometry, "panelGeometry");
            panelGeometry.Name = "panelGeometry";
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
            // graphControlDepthProfile
            // 
            resources.ApplyResources(graphControlDepthProfile, "graphControlDepthProfile");
            graphControlDepthProfile.Name = "graphControlDepthProfile";
            // 
            // poleFigureControl
            // 
            resources.ApplyResources(poleFigureControl, "poleFigureControl");
            poleFigureControl.Name = "poleFigureControl";
            // 
            // graphControlEnergyProfile
            // 
            resources.ApplyResources(graphControlEnergyProfile, "graphControlEnergyProfile");
            graphControlEnergyProfile.Name = "graphControlEnergyProfile";
            // 
            // graphicsBox
            // 
            graphicsBox.AllowDrop = true;
            graphicsBox.BackColor = System.Drawing.Color.Transparent;
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(graphicsBox, "graphicsBox");
            graphicsBox.Fonts = new System.Drawing.Font("Segoe UI", 9.75F);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.TabStop = false;
            graphicsBox.PaintOverlay += graphicsBox_PaintOverlay;
            graphicsBox.DragDrop += FormEBSD_DragDrop;
            graphicsBox.DragEnter += FormEBSD_DragEnter;
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            graphicsBox.MouseUp += graphicsBox_MouseUp;
            graphicsBox.Resize += graphicsBox_Resize;
            // 
            // flowLayoutPanel20
            // 
            resources.ApplyResources(flowLayoutPanel20, "flowLayoutPanel20");
            flowLayoutPanel20.Controls.Add(label3);
            flowLayoutPanel20.Controls.Add(comboBoxGradient);
            flowLayoutPanel20.Controls.Add(label4);
            flowLayoutPanel20.Controls.Add(comboBoxScale);
            flowLayoutPanel20.Name = "flowLayoutPanel20";
            // 
            // flowLayoutPanel19
            // 
            resources.ApplyResources(flowLayoutPanel19, "flowLayoutPanel19");
            flowLayoutPanel19.Controls.Add(label10);
            flowLayoutPanel19.Controls.Add(label7);
            flowLayoutPanel19.Controls.Add(trackBarIntensityBrightnessMin);
            flowLayoutPanel19.Controls.Add(label8);
            flowLayoutPanel19.Controls.Add(trackBarIntensityBrightnessMax);
            flowLayoutPanel19.Name = "flowLayoutPanel19";
            // 
            // flowLayoutPanelOutputRange
            // 
            resources.ApplyResources(flowLayoutPanelOutputRange, "flowLayoutPanelOutputRange");
            flowLayoutPanelOutputRange.Controls.Add(numericBoxEnergy);
            flowLayoutPanelOutputRange.Controls.Add(trackBarOutputEnergy);
            flowLayoutPanelOutputRange.Controls.Add(numericBoxDepth);
            flowLayoutPanelOutputRange.Controls.Add(trackBarOutputThickness);
            flowLayoutPanelOutputRange.Name = "flowLayoutPanelOutputRange";
            // 
            // flowLayoutPanel18
            // 
            flowLayoutPanel18.Controls.Add(checkBoxWithBSEDistribution);
            resources.ApplyResources(flowLayoutPanel18, "flowLayoutPanel18");
            flowLayoutPanel18.Name = "flowLayoutPanel18";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageGeometry);
            tabControl1.Controls.Add(tabPageBseDistribution);
            tabControl1.Controls.Add(tabPageOverlays);
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.HotTrack = true;
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // tabPageGeometry
            // 
            tabPageGeometry.BackColor = System.Drawing.SystemColors.Control;
            tabPageGeometry.Controls.Add(panelGeometry);
            tabPageGeometry.Controls.Add(groupBoxEBSDGeometry);
            tabPageGeometry.Controls.Add(groupBoxSampleCondition);
            tabPageGeometry.Controls.Add(flowLayoutPanelViewAlong);
            resources.ApplyResources(tabPageGeometry, "tabPageGeometry");
            tabPageGeometry.Name = "tabPageGeometry";
            // 
            // groupBoxEBSDGeometry
            // 
            resources.ApplyResources(groupBoxEBSDGeometry, "groupBoxEBSDGeometry");
            captureExtender.SetCapture(groupBoxEBSDGeometry, true);
            groupBoxEBSDGeometry.Controls.Add(flowLayoutPanel11);
            groupBoxEBSDGeometry.Name = "groupBoxEBSDGeometry";
            groupBoxEBSDGeometry.TabStop = false;
            // 
            // flowLayoutPanel11
            // 
            resources.ApplyResources(flowLayoutPanel11, "flowLayoutPanel11");
            flowLayoutPanel11.Controls.Add(flowLayoutPanel17);
            flowLayoutPanel11.Controls.Add(flowLayoutPanel16);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            // 
            // flowLayoutPanel17
            // 
            resources.ApplyResources(flowLayoutPanel17, "flowLayoutPanel17");
            flowLayoutPanel17.Controls.Add(label5);
            flowLayoutPanel17.Controls.Add(numericBoxDetTilt);
            flowLayoutPanel17.Controls.Add(numericBoxDetWidth);
            flowLayoutPanel17.Controls.Add(numericBoxDetHeight);
            flowLayoutPanel17.Controls.Add(label6);
            flowLayoutPanel17.Controls.Add(numericBoxDetResolution);
            flowLayoutPanel17.Name = "flowLayoutPanel17";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // numericBoxDetWidth
            // 
            numericBoxDetWidth.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxDetWidth, "numericBoxDetWidth");
            numericBoxDetWidth.Maximum = 4096D;
            numericBoxDetWidth.Minimum = 1D;
            numericBoxDetWidth.Name = "numericBoxDetWidth";
            numericBoxDetWidth.ShowUpDown = true;
            numericBoxDetWidth.UpDown_Increment = 10D;
            numericBoxDetWidth.Value = 400D;
            numericBoxDetWidth.ValueBoxWidth = 40;
            numericBoxDetWidth.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // numericBoxDetHeight
            // 
            numericBoxDetHeight.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxDetHeight, "numericBoxDetHeight");
            numericBoxDetHeight.Maximum = 4096D;
            numericBoxDetHeight.Minimum = 1D;
            numericBoxDetHeight.Name = "numericBoxDetHeight";
            numericBoxDetHeight.ShowUpDown = true;
            numericBoxDetHeight.UpDown_Increment = 10D;
            numericBoxDetHeight.Value = 400D;
            numericBoxDetHeight.ValueBoxWidth = 40;
            numericBoxDetHeight.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // numericBoxDetResolution
            // 
            numericBoxDetResolution.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxDetResolution, "numericBoxDetResolution");
            numericBoxDetResolution.Maximum = 10D;
            numericBoxDetResolution.Minimum = 0.0001D;
            numericBoxDetResolution.Name = "numericBoxDetResolution";
            numericBoxDetResolution.ShowUpDown = true;
            numericBoxDetResolution.UpDown_Increment = 0.01D;
            numericBoxDetResolution.Value = 0.1D;
            numericBoxDetResolution.ValueBoxWidth = 50;
            numericBoxDetResolution.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // flowLayoutPanel16
            // 
            resources.ApplyResources(flowLayoutPanel16, "flowLayoutPanel16");
            flowLayoutPanel16.Controls.Add(label2);
            flowLayoutPanel16.Controls.Add(numericBoxXofDet);
            flowLayoutPanel16.Controls.Add(numericBoxYofDet);
            flowLayoutPanel16.Controls.Add(numericBoxZofDet);
            flowLayoutPanel16.Name = "flowLayoutPanel16";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // numericBoxXofDet
            // 
            numericBoxXofDet.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxXofDet, "numericBoxXofDet");
            numericBoxXofDet.Maximum = 1000D;
            numericBoxXofDet.Minimum = -1000D;
            numericBoxXofDet.Name = "numericBoxXofDet";
            numericBoxXofDet.ShowUpDown = true;
            numericBoxXofDet.Value = 1D;
            numericBoxXofDet.ValueBoxWidth = 55;
            numericBoxXofDet.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // groupBoxSampleCondition
            // 
            resources.ApplyResources(groupBoxSampleCondition, "groupBoxSampleCondition");
            captureExtender.SetCapture(groupBoxSampleCondition, true);
            groupBoxSampleCondition.Controls.Add(flowLayoutPanel12);
            groupBoxSampleCondition.Name = "groupBoxSampleCondition";
            groupBoxSampleCondition.TabStop = false;
            // 
            // flowLayoutPanel12
            // 
            resources.ApplyResources(flowLayoutPanel12, "flowLayoutPanel12");
            flowLayoutPanel12.Controls.Add(waveLengthControl);
            flowLayoutPanel12.Controls.Add(numericBoxSampleTilt);
            flowLayoutPanel12.Name = "flowLayoutPanel12";
            // 
            // tabPageBseDistribution
            // 
            tabPageBseDistribution.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageBseDistribution, true);
            tabPageBseDistribution.Controls.Add(label15);
            tabPageBseDistribution.Controls.Add(buttonSimulateBSE);
            tabPageBseDistribution.Controls.Add(label13);
            tabPageBseDistribution.Controls.Add(checkBoxDrawAxesInStereonet);
            tabPageBseDistribution.Controls.Add(label14);
            tabPageBseDistribution.Controls.Add(poleFigureControl);
            tabPageBseDistribution.Controls.Add(graphControlDepthProfile);
            tabPageBseDistribution.Controls.Add(graphControlEnergyProfile);
            resources.ApplyResources(tabPageBseDistribution, "tabPageBseDistribution");
            tabPageBseDistribution.Name = "tabPageBseDistribution";
            // 
            // tabPageOverlays
            // 
            tabPageOverlays.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageOverlays, true);
            tabPageOverlays.Controls.Add(flowLayoutPanel10);
            resources.ApplyResources(tabPageOverlays, "tabPageOverlays");
            tabPageOverlays.Name = "tabPageOverlays";
            // 
            // flowLayoutPanel10
            // 
            flowLayoutPanel10.Controls.Add(colorControlBackGround);
            flowLayoutPanel10.Controls.Add(checkBoxDrawDetectorOutline);
            flowLayoutPanel10.Controls.Add(flowLayoutPanel1DetectorOutline);
            flowLayoutPanel10.Controls.Add(checkBoxShowKikuchiLines);
            flowLayoutPanel10.Controls.Add(flowLayoutPanel1KikuchiLines);
            flowLayoutPanel10.Controls.Add(groupBoxLatticePlanes);
            flowLayoutPanel10.Controls.Add(checkBoxShowGIndices);
            flowLayoutPanel10.Controls.Add(checkBoxShowZoneAxisIndices);
            flowLayoutPanel10.Controls.Add(groupBox2);
            resources.ApplyResources(flowLayoutPanel10, "flowLayoutPanel10");
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            // 
            // flowLayoutPanel1DetectorOutline
            // 
            resources.ApplyResources(flowLayoutPanel1DetectorOutline, "flowLayoutPanel1DetectorOutline");
            flowLayoutPanel1DetectorOutline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowLayoutPanel1DetectorOutline.Controls.Add(checkBoxShowCircle);
            flowLayoutPanel1DetectorOutline.Controls.Add(checkBoxShowMesh);
            flowLayoutPanel1DetectorOutline.Name = "flowLayoutPanel1DetectorOutline";
            // 
            // flowLayoutPanel1KikuchiLines
            // 
            resources.ApplyResources(flowLayoutPanel1KikuchiLines, "flowLayoutPanel1KikuchiLines");
            flowLayoutPanel1KikuchiLines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowLayoutPanel1KikuchiLines.Controls.Add(label11);
            flowLayoutPanel1KikuchiLines.Controls.Add(trackBarLineWidth);
            flowLayoutPanel1KikuchiLines.Controls.Add(colorControlExcessLine);
            flowLayoutPanel1KikuchiLines.Controls.Add(checkBoxKikuchiLine_Kinematical);
            flowLayoutPanel1KikuchiLines.Name = "flowLayoutPanel1KikuchiLines";
            // 
            // groupBoxLatticePlanes
            // 
            resources.ApplyResources(groupBoxLatticePlanes, "groupBoxLatticePlanes");
            groupBoxLatticePlanes.Controls.Add(flowLayoutPanel14);
            groupBoxLatticePlanes.Controls.Add(flowLayoutPanel13);
            groupBoxLatticePlanes.Name = "groupBoxLatticePlanes";
            groupBoxLatticePlanes.TabStop = false;
            // 
            // flowLayoutPanel14
            // 
            resources.ApplyResources(flowLayoutPanel14, "flowLayoutPanel14");
            flowLayoutPanel14.Controls.Add(radioButtonKikuchiThresholdOfLength);
            flowLayoutPanel14.Controls.Add(numericBoxKikuchiThresholdOfLength);
            flowLayoutPanel14.Name = "flowLayoutPanel14";
            // 
            // flowLayoutPanel13
            // 
            resources.ApplyResources(flowLayoutPanel13, "flowLayoutPanel13");
            flowLayoutPanel13.Controls.Add(radioButtonKikuchiThresholdOfStructureFactor);
            flowLayoutPanel13.Controls.Add(numericBoxKikuchiThresholdOfStructureFactor);
            flowLayoutPanel13.Name = "flowLayoutPanel13";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(flowLayoutPanel15);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // flowLayoutPanel15
            // 
            resources.ApplyResources(flowLayoutPanel15, "flowLayoutPanel15");
            flowLayoutPanel15.Controls.Add(label1);
            flowLayoutPanel15.Controls.Add(trackBarStrSize);
            flowLayoutPanel15.Controls.Add(colorControlString);
            flowLayoutPanel15.Name = "flowLayoutPanel15";
            // 
            // checkBoxShowExperimentalImage
            // 
            resources.ApplyResources(checkBoxShowExperimentalImage, "checkBoxShowExperimentalImage");
            checkBoxShowExperimentalImage.Checked = true;
            checkBoxShowExperimentalImage.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowExperimentalImage.Name = "checkBoxShowExperimentalImage";
            checkBoxShowExperimentalImage.UseVisualStyleBackColor = true;
            checkBoxShowExperimentalImage.CheckedChanged += checkBoxShowExperimentalImage_CheckedChanged;
            // 
            // flowLayoutPanelExperimentalImage
            // 
            resources.ApplyResources(flowLayoutPanelExperimentalImage, "flowLayoutPanelExperimentalImage");
            flowLayoutPanelExperimentalImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowLayoutPanelExperimentalImage.Controls.Add(flowLayoutPanelExpMinInt);
            flowLayoutPanelExperimentalImage.Controls.Add(flowLayoutPanelExpMaxInt);
            flowLayoutPanelExperimentalImage.Name = "flowLayoutPanelExperimentalImage";
            // 
            // flowLayoutPanelExpMinInt
            // 
            resources.ApplyResources(flowLayoutPanelExpMinInt, "flowLayoutPanelExpMinInt");
            flowLayoutPanelExpMinInt.Controls.Add(labelExpMinInt);
            flowLayoutPanelExpMinInt.Controls.Add(trackBarExpImageMinInt);
            flowLayoutPanelExpMinInt.Name = "flowLayoutPanelExpMinInt";
            // 
            // labelExpMinInt
            // 
            resources.ApplyResources(labelExpMinInt, "labelExpMinInt");
            labelExpMinInt.Name = "labelExpMinInt";
            // 
            // trackBarExpImageMinInt
            // 
            resources.ApplyResources(trackBarExpImageMinInt, "trackBarExpImageMinInt");
            trackBarExpImageMinInt.LargeChange = 50;
            trackBarExpImageMinInt.Maximum = 1000;
            trackBarExpImageMinInt.Name = "trackBarExpImageMinInt";
            trackBarExpImageMinInt.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarExpImageMinInt.ValueChanged += trackBarExpImageIntensity_ValueChanged;
            // 
            // flowLayoutPanelExpMaxInt
            // 
            resources.ApplyResources(flowLayoutPanelExpMaxInt, "flowLayoutPanelExpMaxInt");
            flowLayoutPanelExpMaxInt.Controls.Add(labelExpMaxInt);
            flowLayoutPanelExpMaxInt.Controls.Add(trackBarExpImageMaxInt);
            flowLayoutPanelExpMaxInt.Name = "flowLayoutPanelExpMaxInt";
            // 
            // labelExpMaxInt
            // 
            resources.ApplyResources(labelExpMaxInt, "labelExpMaxInt");
            labelExpMaxInt.Name = "labelExpMaxInt";
            // 
            // trackBarExpImageMaxInt
            // 
            resources.ApplyResources(trackBarExpImageMaxInt, "trackBarExpImageMaxInt");
            trackBarExpImageMaxInt.LargeChange = 50;
            trackBarExpImageMaxInt.Maximum = 1000;
            trackBarExpImageMaxInt.Minimum = 1;
            trackBarExpImageMaxInt.Name = "trackBarExpImageMaxInt";
            trackBarExpImageMaxInt.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarExpImageMaxInt.Value = 1000;
            trackBarExpImageMaxInt.ValueChanged += trackBarExpImageIntensity_ValueChanged;
            // 
            // flowLayoutPanelExpOpacity
            // 
            resources.ApplyResources(flowLayoutPanelExpOpacity, "flowLayoutPanelExpOpacity");
            flowLayoutPanelExpOpacity.Controls.Add(labelExpOpacity);
            flowLayoutPanelExpOpacity.Controls.Add(trackBarExpImageOpacity);
            flowLayoutPanelExpOpacity.Name = "flowLayoutPanelExpOpacity";
            // 
            // labelExpOpacity
            // 
            resources.ApplyResources(labelExpOpacity, "labelExpOpacity");
            labelExpOpacity.Name = "labelExpOpacity";
            // 
            // trackBarExpImageOpacity
            // 
            resources.ApplyResources(trackBarExpImageOpacity, "trackBarExpImageOpacity");
            trackBarExpImageOpacity.LargeChange = 10;
            trackBarExpImageOpacity.Maximum = 100;
            trackBarExpImageOpacity.Name = "trackBarExpImageOpacity";
            trackBarExpImageOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarExpImageOpacity.Value = 100;
            trackBarExpImageOpacity.ValueChanged += trackBarExpImageOpacity_ValueChanged;
            //
            // buttonFindOrientation
            // 
            resources.ApplyResources(buttonFindOrientation, "buttonFindOrientation");
            buttonFindOrientation.Name = "buttonFindOrientation";
            buttonFindOrientation.UseVisualStyleBackColor = true;
            buttonFindOrientation.Click += buttonFindOrientation_Click;
            // 
            // dataGridViewEbsdCandidates
            // 
            dataGridViewEbsdCandidates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(dataGridViewEbsdCandidates, "dataGridViewEbsdCandidates");
            dataGridViewEbsdCandidates.Name = "dataGridViewEbsdCandidates";
            //
            // buttonCalibrateGeometry
            // 
            resources.ApplyResources(buttonCalibrateGeometry, "buttonCalibrateGeometry");
            buttonCalibrateGeometry.Name = "buttonCalibrateGeometry";
            buttonCalibrateGeometry.UseVisualStyleBackColor = true;
            buttonCalibrateGeometry.Click += buttonCalibrateGeometry_Click;
            // 
            // groupBoxSimulationParameters
            // 
            resources.ApplyResources(groupBoxSimulationParameters, "groupBoxSimulationParameters");
            captureExtender.SetCapture(groupBoxSimulationParameters, true);
            groupBoxSimulationParameters.Controls.Add(flowLayoutPanel9);
            groupBoxSimulationParameters.Name = "groupBoxSimulationParameters";
            groupBoxSimulationParameters.TabStop = false;
            // 
            // flowLayoutPanel9
            // 
            resources.ApplyResources(flowLayoutPanel9, "flowLayoutPanel9");
            flowLayoutPanel9.Controls.Add(flowLayoutPanel2);
            flowLayoutPanel9.Controls.Add(flowLayoutPanel7);
            flowLayoutPanel9.Controls.Add(flowLayoutPanel6);
            flowLayoutPanel9.Controls.Add(flowLayoutPanel8);
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(numericBoxMaxNumOfG);
            flowLayoutPanel2.Controls.Add(labelMasterPatternGrid);
            flowLayoutPanel2.Controls.Add(comboBoxMasterPatternGrid);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // flowLayoutPanel7
            // 
            resources.ApplyResources(flowLayoutPanel7, "flowLayoutPanel7");
            flowLayoutPanel7.Controls.Add(numericBoxEnergyStart);
            flowLayoutPanel7.Controls.Add(numericBoxEnergyEnd);
            flowLayoutPanel7.Controls.Add(numericBoxEnergyStep);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            // 
            // flowLayoutPanel6
            // 
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Controls.Add(numericBoxThicknessStart);
            flowLayoutPanel6.Controls.Add(numericBoxThicknessEnd);
            flowLayoutPanel6.Controls.Add(numericBoxThicknessStep);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            // 
            // flowLayoutPanel8
            // 
            resources.ApplyResources(flowLayoutPanel8, "flowLayoutPanel8");
            flowLayoutPanel8.Controls.Add(checkBoxNonLocalAbsorption);
            flowLayoutPanel8.Controls.Add(checkBoxTDSBackground);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
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
            scalablePictureBoxAdvancedMasterPattern2D.ClampIntensityRangeToNewData = false;
            scalablePictureBoxAdvancedMasterPattern2D.DecimalPlacesForIntensity = 5;
            resources.ApplyResources(scalablePictureBoxAdvancedMasterPattern2D, "scalablePictureBoxAdvancedMasterPattern2D");
            scalablePictureBoxAdvancedMasterPattern2D.FrequencyGraphVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_DustAndScratchesVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_GaussianBlurVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilterVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.MagInfoVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.MaximumIntensity = 1D;
            scalablePictureBoxAdvancedMasterPattern2D.MousePositionLabelVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.Name = "scalablePictureBoxAdvancedMasterPattern2D";
            scalablePictureBoxAdvancedMasterPattern2D.ScaleVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.StatusVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.UpperIntensity = 1D;
            scalablePictureBoxAdvancedMasterPattern2D.BrightnessAndColorChanged += scalablePictureBoxAdvancedMasterPattern2D_BrightnessAndColorChanged;
            // 
            // flowLayoutPanelMasterPatternSelectors
            // 
            resources.ApplyResources(flowLayoutPanelMasterPatternSelectors, "flowLayoutPanelMasterPatternSelectors");
            flowLayoutPanelMasterPatternSelectors.Controls.Add(numericBoxMasterPatternEnergy);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(trackBarMasterPatternEnergy);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(numericBoxMasterPatternDepth);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(trackBarMasterPatternDepth);
            flowLayoutPanelMasterPatternSelectors.Name = "flowLayoutPanelMasterPatternSelectors";
            // 
            // panelMasterPattern3D
            // 
            panelMasterPattern3D.BackColor = System.Drawing.SystemColors.Control;
            panelMasterPattern3D.Controls.Add(panelMasterPattern3DAxes);
            resources.ApplyResources(panelMasterPattern3D, "panelMasterPattern3D");
            panelMasterPattern3D.Name = "panelMasterPattern3D";
            // 
            // panelMasterPattern3DAxes
            // 
            panelMasterPattern3DAxes.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(panelMasterPattern3DAxes, "panelMasterPattern3DAxes");
            panelMasterPattern3DAxes.Name = "panelMasterPattern3DAxes";
            // 
            // groupBoxMasterPattern
            // 
            captureExtender.SetCapture(groupBoxMasterPattern, true);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanel5);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanel4);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanel3);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanelMasterPattern3DViewAlong);
            groupBoxMasterPattern.Controls.Add(groupBoxSimulationParameters);
            groupBoxMasterPattern.Controls.Add(scalablePictureBoxAdvancedMasterPattern2D);
            groupBoxMasterPattern.Controls.Add(panelMasterPattern3D);
            groupBoxMasterPattern.Controls.Add(buttonFitNistElasticSampler);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanelMasterPatternSelectors);
            resources.ApplyResources(groupBoxMasterPattern, "groupBoxMasterPattern");
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
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(buttonMasterPattern3DCopy);
            flowLayoutPanel4.Controls.Add(checkBoxMasterPattern3DAxisLabel);
            flowLayoutPanel4.Controls.Add(checkBoxMasterPattern3DAxisArrows);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
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
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(indexControl);
            flowLayoutPanelMasterPattern3DViewAlong.Name = "flowLayoutPanelMasterPattern3DViewAlong";
            // 
            // groupBoxEBSDPattern
            // 
            captureExtender.SetCapture(groupBoxEBSDPattern, true);
            groupBoxEBSDPattern.Controls.Add(graphicsBox);
            groupBoxEBSDPattern.Controls.Add(tabControl2);
            groupBoxEBSDPattern.Controls.Add(flowLayoutPanel1);
            resources.ApplyResources(groupBoxEBSDPattern, "groupBoxEBSDPattern");
            groupBoxEBSDPattern.Name = "groupBoxEBSDPattern";
            groupBoxEBSDPattern.TabStop = false;
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPageOutputParameter);
            tabControl2.Controls.Add(tabPageExperimentalImage);
            resources.ApplyResources(tabControl2, "tabControl2");
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            // 
            // tabPageOutputParameter
            // 
            tabPageOutputParameter.BackColor = System.Drawing.SystemColors.Control;
            tabPageOutputParameter.Controls.Add(flowLayoutPanel20);
            tabPageOutputParameter.Controls.Add(flowLayoutPanel19);
            tabPageOutputParameter.Controls.Add(flowLayoutPanelOutputRange);
            tabPageOutputParameter.Controls.Add(flowLayoutPanel18);
            resources.ApplyResources(tabPageOutputParameter, "tabPageOutputParameter");
            tabPageOutputParameter.Name = "tabPageOutputParameter";
            // 
            // tabPageExperimentalImage
            // 
            tabPageExperimentalImage.BackColor = System.Drawing.SystemColors.Control;
            tabPageExperimentalImage.Controls.Add(dataGridViewEbsdCandidates);
            tabPageExperimentalImage.Controls.Add(flowLayoutPanelExpOpacity);
            tabPageExperimentalImage.Controls.Add(flowLayoutPanelExperimentalImage);
            tabPageExperimentalImage.Controls.Add(buttonCalibrateGeometry);
            tabPageExperimentalImage.Controls.Add(buttonFindOrientation);
            resources.ApplyResources(tabPageExperimentalImage, "tabPageExperimentalImage");
            tabPageExperimentalImage.Name = "tabPageExperimentalImage";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(buttonCopyImage);
            flowLayoutPanel1.Controls.Add(checkBoxShowOverlays);
            flowLayoutPanel1.Controls.Add(checkBoxShowDyanmicalEBSD);
            flowLayoutPanel1.Controls.Add(checkBoxShowExperimentalImage);
            flowLayoutPanel1.Controls.Add(checkBoxFlipDetectorLeftRight);
            flowLayoutPanel1.Controls.Add(numericBoxResolution);
            flowLayoutPanel1.Controls.Add(sizeControl);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // numericBoxResolution
            // 
            numericBoxResolution.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxResolution, "numericBoxResolution");
            numericBoxResolution.Maximum = 10D;
            numericBoxResolution.Minimum = 0.0001D;
            numericBoxResolution.Name = "numericBoxResolution";
            numericBoxResolution.Value = 0.1D;
            numericBoxResolution.ValueBoxWidth = 40;
            numericBoxResolution.ValueChanged += numericBoxResolution_ValueChanged;
            // 
            // sizeControl
            // 
            resources.ApplyResources(sizeControl, "sizeControl");
            sizeControl.Name = "sizeControl";
            sizeControl.ValueChanged += sizeControl_ValueChanged;
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
            AllowDrop = true;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(groupBoxEBSDPattern);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(groupBoxMasterPattern);
            Controls.Add(tabControl1);
            Controls.Add(panel4);
            Controls.Add(statusStrip1);
            Name = "FormEBSD";
            FormClosing += FormEBSD_FormClosing;
            Load += FormEBSD_Load;
            VisibleChanged += FormEBSD_VisibleChanged;
            DragDrop += FormEBSD_DragDrop;
            DragEnter += FormEBSD_DragEnter;
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputEnergy).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternEnergy).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternDepth).EndInit();
            flowLayoutPanelViewAlong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            flowLayoutPanel20.ResumeLayout(false);
            flowLayoutPanel20.PerformLayout();
            flowLayoutPanel19.ResumeLayout(false);
            flowLayoutPanel19.PerformLayout();
            flowLayoutPanelOutputRange.ResumeLayout(false);
            flowLayoutPanel18.ResumeLayout(false);
            flowLayoutPanel18.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPageGeometry.ResumeLayout(false);
            tabPageGeometry.PerformLayout();
            groupBoxEBSDGeometry.ResumeLayout(false);
            groupBoxEBSDGeometry.PerformLayout();
            flowLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel11.PerformLayout();
            flowLayoutPanel17.ResumeLayout(false);
            flowLayoutPanel17.PerformLayout();
            flowLayoutPanel16.ResumeLayout(false);
            flowLayoutPanel16.PerformLayout();
            groupBoxSampleCondition.ResumeLayout(false);
            groupBoxSampleCondition.PerformLayout();
            flowLayoutPanel12.ResumeLayout(false);
            flowLayoutPanel12.PerformLayout();
            tabPageBseDistribution.ResumeLayout(false);
            tabPageBseDistribution.PerformLayout();
            tabPageOverlays.ResumeLayout(false);
            flowLayoutPanel10.ResumeLayout(false);
            flowLayoutPanel10.PerformLayout();
            flowLayoutPanel1DetectorOutline.ResumeLayout(false);
            flowLayoutPanel1DetectorOutline.PerformLayout();
            flowLayoutPanel1KikuchiLines.ResumeLayout(false);
            flowLayoutPanel1KikuchiLines.PerformLayout();
            groupBoxLatticePlanes.ResumeLayout(false);
            groupBoxLatticePlanes.PerformLayout();
            flowLayoutPanel14.ResumeLayout(false);
            flowLayoutPanel14.PerformLayout();
            flowLayoutPanel13.ResumeLayout(false);
            flowLayoutPanel13.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            flowLayoutPanel15.ResumeLayout(false);
            flowLayoutPanel15.PerformLayout();
            flowLayoutPanelExperimentalImage.ResumeLayout(false);
            flowLayoutPanelExperimentalImage.PerformLayout();
            flowLayoutPanelExpMinInt.ResumeLayout(false);
            flowLayoutPanelExpMinInt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageMinInt).EndInit();
            flowLayoutPanelExpMaxInt.ResumeLayout(false);
            flowLayoutPanelExpMaxInt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageMaxInt).EndInit();
            flowLayoutPanelExpOpacity.ResumeLayout(false);
            flowLayoutPanelExpOpacity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageOpacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEbsdCandidates).EndInit();
            groupBoxSimulationParameters.ResumeLayout(false);
            groupBoxSimulationParameters.PerformLayout();
            flowLayoutPanel9.ResumeLayout(false);
            flowLayoutPanel9.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            flowLayoutPanelMasterPatternSelectors.ResumeLayout(false);
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
            tabControl2.ResumeLayout(false);
            tabPageOutputParameter.ResumeLayout(false);
            tabPageOutputParameter.PerformLayout();
            tabPageExperimentalImage.ResumeLayout(false);
            tabPageExperimentalImage.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)

        private System.Windows.Forms.Panel panelGeometry;
        private NumericBox numericBoxSampleTilt;
        private WaveLengthControl waveLengthControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelViewAlong;
        private System.Windows.Forms.Button buttonViewFromZ;
        private System.Windows.Forms.Button buttonFromX;
        private System.Windows.Forms.Button buttonViewFromSurfaceNormal;
        private System.Windows.Forms.Button buttonSimulateBSE;
        private System.Windows.Forms.Button buttonFitNistElasticSampler;
        private PoleFigureControl2 poleFigureControl;
        private System.Windows.Forms.CheckBox checkBoxDrawAxesInStereonet;
        private NumericBox numericBoxDetTilt;
        private NumericBox numericBoxZofDet;
        private NumericBox numericBoxYofDet;
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
        private NumericBox numericBoxKikuchiThresholdOfStructureFactor;
        private NumericBox numericBoxKikuchiThresholdOfLength;
        private System.Windows.Forms.Label label1;
        private NumericBox numericBoxThicknessStep;
        private NumericBox numericBoxMaxNumOfG;
        private NumericBox numericBoxThicknessStart;
        private NumericBox numericBoxThicknessEnd;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox comboBoxGradient;
        public System.Windows.Forms.ComboBox comboBoxScale;
        public System.Windows.Forms.TrackBar trackBarOutputThickness;
        private System.Windows.Forms.TrackBar trackBarIntensityBrightnessMax;
        private System.Windows.Forms.TrackBar trackBarIntensityBrightnessMin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxShowOverlays;
        private System.Windows.Forms.Button buttonCopyImage;
        private GraphControl graphControlEnergyProfile;
        private GraphControl graphControlDepthProfile;
        public System.Windows.Forms.TrackBar trackBarOutputEnergy;
        private NumericBox numericBoxEnergyEnd;
        private NumericBox numericBoxEnergyStart;
        private NumericBox numericBoxEnergyStep;
        private System.Windows.Forms.Button buttonViewQuarter;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBoxShowDyanmicalEBSD;
        private System.Windows.Forms.CheckBox checkBoxDrawDetectorOutline;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeometry;
        private System.Windows.Forms.TabPage tabPageBseDistribution;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBoxSimulationParameters;
        private System.Windows.Forms.TabPage tabPageOverlays;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.CheckBox checkBoxNonLocalAbsorption;
        private System.Windows.Forms.CheckBox checkBoxTDSBackground;
        private System.Windows.Forms.CheckBox checkBoxWithBSEDistribution;
        private System.Windows.Forms.CheckBox checkBoxFlipDetectorLeftRight; // 260718Cl 追加: 検出器を背面から見た左右反転
        private System.Windows.Forms.GroupBox groupBoxLatticePlanes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOutputRange;
        private System.Windows.Forms.Panel panelMasterPattern3D;
        private ScalablePictureBoxAdvanced scalablePictureBoxAdvancedMasterPattern2D;
        private System.Windows.Forms.Label labelMasterPatternGrid;
        private System.Windows.Forms.ComboBox comboBoxMasterPatternGrid;
        private System.Windows.Forms.Label labelMasterPattern2DHemisphere;
        private System.Windows.Forms.ComboBox comboBoxMasterPattern2DHemisphere;
        private System.Windows.Forms.TrackBar trackBarMasterPatternEnergy;
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
        private System.Windows.Forms.Button buttonMasterPattern3DViewAlong;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMasterPattern3DViewAlong;
        // 260517Cl 削除: IndexControl 化により [u v w] のブラケット表示は IndexControl 内部 (labelLaTexStart/End) が担うため、外側ラベルは不要に。
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.CheckBox checkBoxShowGIndices;
        private System.Windows.Forms.CheckBox checkBoxShowZoneAxisIndices;
        private System.Windows.Forms.CheckBox checkBoxShowKikuchiLines;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private IndexControl indexControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private NumericBox numericBoxMasterPatternEnergy;
        private NumericBox numericBoxMasterPatternDepth;
        private NumericBox numericBoxEnergy;
        private NumericBox numericBoxDepth;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1DetectorOutline;
        private System.Windows.Forms.CheckBox checkBoxShowCircle;
        private System.Windows.Forms.CheckBox checkBoxShowMesh;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1KikuchiLines;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel12;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel14;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel13;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel15;
        private NumericBox numericBoxXofDet;
        private SizeControl sizeControl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel16;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel17;
        private System.Windows.Forms.Label label5;
        private NumericBox numericBoxDetWidth;
        private NumericBox numericBoxDetHeight;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel18;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel20;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel19;
        private NumericBox numericBoxResolution;
        private System.Windows.Forms.Label label6;
        private NumericBox numericBoxDetResolution;
        private System.Windows.Forms.CheckBox checkBoxShowExperimentalImage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExperimentalImage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExpOpacity;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExpMaxInt;
        private System.Windows.Forms.Label labelExpOpacity;
        private System.Windows.Forms.Label labelExpMaxInt;
        private System.Windows.Forms.TrackBar trackBarExpImageOpacity;
        private System.Windows.Forms.TrackBar trackBarExpImageMaxInt;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExpMinInt;
        private System.Windows.Forms.Label labelExpMinInt;
        private System.Windows.Forms.TrackBar trackBarExpImageMinInt;
        private System.Windows.Forms.Button buttonFindOrientation;
        private System.Windows.Forms.Button buttonCalibrateGeometry;
        private System.Windows.Forms.DataGridView dataGridViewEbsdCandidates;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPageOutputParameter;
        private System.Windows.Forms.TabPage tabPageExperimentalImage;
    }
}

