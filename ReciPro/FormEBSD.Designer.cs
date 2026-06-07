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
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            panelGeometry = new System.Windows.Forms.Panel();
            numericBoxSampleTilt = new NumericBox();
            waveLengthControl = new WaveLengthControl();
            flowLayoutPanelViewAlong = new System.Windows.Forms.FlowLayoutPanel();
            buttonViewQuarter = new System.Windows.Forms.Button();
            buttonViewFromSurfaceNormal = new System.Windows.Forms.Button();
            buttonFromX = new System.Windows.Forms.Button();
            buttonViewFromZ = new System.Windows.Forms.Button();
            buttonSimulateBSE = new System.Windows.Forms.Button();
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
            groupBoxOutput = new System.Windows.Forms.GroupBox();
            flowLayoutPanelOutputRange = new System.Windows.Forms.FlowLayoutPanel();
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
            flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            labelMasterPatternGrid = new System.Windows.Forms.Label();
            comboBoxMasterPatternGrid = new System.Windows.Forms.ComboBox();
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
            numericBoxMasterPatternEnergy = new NumericBox();
            trackBarMasterPatternEnergy = new System.Windows.Forms.TrackBar();
            numericBoxMasterPatternDepth = new NumericBox();
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
            indexControl = new IndexControl();
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
            flowLayoutPanel9.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
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
            captureExtender.SetCapture(panelGeometry, true); // 260524Cl 追加: Wiki 用クロップ (SEM/検出器の 3D ジオメトリ図)
            panelGeometry.Name = "panelGeometry";
            // 
            // numericBoxSampleTilt
            // 
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxSampleTilt, "numericBoxSampleTilt");
            toolTip.SetToolTip(numericBoxSampleTilt, resources.GetString("numericBoxSampleTilt.ToolTip")); // 260531Cl
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
            toolTip.SetToolTip(waveLengthControl, resources.GetString("waveLengthControl.ToolTip")); // 260531Cl
            waveLengthControl.DirectionWhole = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl.Energy = 20D;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = false;
            waveLengthControl.WaveLength = 0.008588514105D;
            waveLengthControl.WaveSource = WaveSource.Electron;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
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
            toolTip.SetToolTip(buttonViewQuarter, resources.GetString("buttonViewQuarter.ToolTip")); // 260531Cl
            buttonViewQuarter.Name = "buttonViewQuarter";
            buttonViewQuarter.UseVisualStyleBackColor = true;
            buttonViewQuarter.Click += buttonViewQuarter_Click;
            // 
            // buttonViewFromSurfaceNormal
            // 
            resources.ApplyResources(buttonViewFromSurfaceNormal, "buttonViewFromSurfaceNormal");
            toolTip.SetToolTip(buttonViewFromSurfaceNormal, resources.GetString("buttonViewFromSurfaceNormal.ToolTip")); // 260531Cl
            buttonViewFromSurfaceNormal.Name = "buttonViewFromSurfaceNormal";
            buttonViewFromSurfaceNormal.UseVisualStyleBackColor = true;
            buttonViewFromSurfaceNormal.Click += buttonFromSurfaceNormal_Click;
            // 
            // buttonFromX
            // 
            resources.ApplyResources(buttonFromX, "buttonFromX");
            toolTip.SetToolTip(buttonFromX, resources.GetString("buttonFromX.ToolTip")); // 260531Cl
            buttonFromX.Name = "buttonFromX";
            buttonFromX.UseVisualStyleBackColor = true;
            buttonFromX.Click += buttonViewFromX_Click;
            // 
            // buttonViewFromZ
            // 
            resources.ApplyResources(buttonViewFromZ, "buttonViewFromZ");
            toolTip.SetToolTip(buttonViewFromZ, resources.GetString("buttonViewFromZ.ToolTip")); // 260531Cl
            buttonViewFromZ.Name = "buttonViewFromZ";
            buttonViewFromZ.UseVisualStyleBackColor = true;
            buttonViewFromZ.Click += buttonViewFromZ_Click;
            // 
            // buttonSimulateBSE
            // 
            buttonSimulateBSE.BackColor = System.Drawing.Color.SteelBlue;
            buttonSimulateBSE.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(buttonSimulateBSE, "buttonSimulateBSE");
            toolTip.SetToolTip(buttonSimulateBSE, resources.GetString("buttonSimulateBSE.ToolTip")); // 260531Cl
            buttonSimulateBSE.Name = "buttonSimulateBSE";
            buttonSimulateBSE.UseVisualStyleBackColor = false;
            buttonSimulateBSE.Click += buttonBSE_Click;
            // 
            // buttonFitNistElasticSampler
            // 
            resources.ApplyResources(buttonFitNistElasticSampler, "buttonFitNistElasticSampler");
            toolTip.SetToolTip(buttonFitNistElasticSampler, resources.GetString("buttonFitNistElasticSampler.ToolTip")); // 260531Cl
            buttonFitNistElasticSampler.Name = "buttonFitNistElasticSampler";
            buttonFitNistElasticSampler.UseVisualStyleBackColor = true;
            buttonFitNistElasticSampler.Click += buttonFitNistElasticSampler_Click;
            // 
            // graphControlDepthProfile
            // 
            resources.ApplyResources(graphControlDepthProfile, "graphControlDepthProfile");
            graphControlDepthProfile.GraphTitle = "";
            graphControlDepthProfile.MousePositionVisible = false;
            graphControlDepthProfile.Name = "graphControlDepthProfile";
            graphControlDepthProfile.UpperPanelVisible = false;
            // 
            // poleFigureControl
            // 
            resources.ApplyResources(poleFigureControl, "poleFigureControl");
            poleFigureControl.Name = "poleFigureControl";
            // 
            // graphControlEnergyProfile
            // 
            resources.ApplyResources(graphControlEnergyProfile, "graphControlEnergyProfile");
            graphControlEnergyProfile.GraphTitle = "";
            graphControlEnergyProfile.MousePositionVisible = false;
            graphControlEnergyProfile.Name = "graphControlEnergyProfile";
            graphControlEnergyProfile.UpperPanelVisible = false;
            // 
            // checkBoxDrawAxesInStereonet
            // 
            resources.ApplyResources(checkBoxDrawAxesInStereonet, "checkBoxDrawAxesInStereonet");
            toolTip.SetToolTip(checkBoxDrawAxesInStereonet, resources.GetString("checkBoxDrawAxesInStereonet.ToolTip")); // 260531Cl
            checkBoxDrawAxesInStereonet.BackColor = System.Drawing.Color.White;
            checkBoxDrawAxesInStereonet.Checked = true;
            checkBoxDrawAxesInStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxesInStereonet.Name = "checkBoxDrawAxesInStereonet";
            checkBoxDrawAxesInStereonet.UseVisualStyleBackColor = false;
            // 
            // numericBoxDetTilt
            // 
            numericBoxDetTilt.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxDetTilt, "numericBoxDetTilt");
            toolTip.SetToolTip(numericBoxDetTilt, resources.GetString("numericBoxDetTilt.ToolTip")); // 260531Cl
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
            numericBoxDetRadius.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxDetRadius, "numericBoxDetRadius");
            toolTip.SetToolTip(numericBoxDetRadius, resources.GetString("numericBoxDetRadius.ToolTip")); // 260531Cl
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
            numericBoxZofDet.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxZofDet, "numericBoxZofDet");
            toolTip.SetToolTip(numericBoxZofDet, resources.GetString("numericBoxZofDet.ToolTip")); // 260531Cl
            numericBoxZofDet.Maximum = 1000D;
            numericBoxZofDet.Minimum = -1000D;
            numericBoxZofDet.Name = "numericBoxZofDet";
            numericBoxZofDet.ShowUpDown = true;
            numericBoxZofDet.UpDown_Increment = 10D;
            numericBoxZofDet.ValueChanged += numericBoxDetRadius_ValueChanged;
            // 
            // numericBoxYofDet
            // 
            numericBoxYofDet.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxYofDet, "numericBoxYofDet");
            toolTip.SetToolTip(numericBoxYofDet, resources.GetString("numericBoxYofDet.ToolTip")); // 260531Cl
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
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip")); // 260531Cl
            label2.Name = "label2";
            // 
            // graphicsBox
            // 
            resources.ApplyResources(graphicsBox, "graphicsBox");
            graphicsBox.BackColor = System.Drawing.Color.Transparent;
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            graphicsBox.Fonts = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.TabStop = false;
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            graphicsBox.MouseUp += graphicsBox_MouseUp;
            // 
            // trackBarStrSize
            // 
            resources.ApplyResources(trackBarStrSize, "trackBarStrSize");
            toolTip.SetToolTip(trackBarStrSize, resources.GetString("trackBarStrSize.ToolTip")); // 260531Cl
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
            toolTip.SetToolTip(colorControlExcessLine, resources.GetString("colorControlExcessLine.ToolTip")); // 260531Cl
            colorControlExcessLine.BackColor = System.Drawing.SystemColors.Control;
            colorControlExcessLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControlExcessLine.Color = System.Drawing.Color.FromArgb(224, 224, 224);
            colorControlExcessLine.Name = "colorControlExcessLine";
            colorControlExcessLine.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // trackBarLineWidth
            // 
            resources.ApplyResources(trackBarLineWidth, "trackBarLineWidth");
            toolTip.SetToolTip(trackBarLineWidth, resources.GetString("trackBarLineWidth.ToolTip")); // 260531Cl
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
            toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip")); // 260531Cl
            label11.Name = "label11";
            // 
            // colorControlString
            // 
            resources.ApplyResources(colorControlString, "colorControlString");
            colorControlString.BackColor = System.Drawing.SystemColors.Control;
            colorControlString.BoxSize = new System.Drawing.Size(20, 20);
            colorControlString.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlString.Name = "colorControlString";
            toolTip.SetToolTip(colorControlString, resources.GetString("colorControlString.ToolTip")); // (260531Ch)
            colorControlString.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // colorControlBackGround
            // 
            resources.ApplyResources(colorControlBackGround, "colorControlBackGround");
            colorControlBackGround.BackColor = System.Drawing.SystemColors.Control;
            colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(32, 32, 32);
            colorControlBackGround.Name = "colorControlBackGround";
            toolTip.SetToolTip(colorControlBackGround, resources.GetString("colorControlBackGround.ToolTip")); // (260531Ch)
            colorControlBackGround.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // radioButtonKikuchiThresholdOfStructureFactor
            // 
            resources.ApplyResources(radioButtonKikuchiThresholdOfStructureFactor, "radioButtonKikuchiThresholdOfStructureFactor");
            toolTip.SetToolTip(radioButtonKikuchiThresholdOfStructureFactor, resources.GetString("radioButtonKikuchiThresholdOfStructureFactor.ToolTip")); // 260531Cl
            radioButtonKikuchiThresholdOfStructureFactor.Checked = true;
            radioButtonKikuchiThresholdOfStructureFactor.Name = "radioButtonKikuchiThresholdOfStructureFactor";
            radioButtonKikuchiThresholdOfStructureFactor.TabStop = true;
            radioButtonKikuchiThresholdOfStructureFactor.UseVisualStyleBackColor = true;
            radioButtonKikuchiThresholdOfStructureFactor.CheckedChanged += radioButtonKikuchiThresholdOfStructureFactor_CheckedChanged;
            // 
            // checkBoxKikuchiLine_Kinematical
            // 
            resources.ApplyResources(checkBoxKikuchiLine_Kinematical, "checkBoxKikuchiLine_Kinematical");
            toolTip.SetToolTip(checkBoxKikuchiLine_Kinematical, resources.GetString("checkBoxKikuchiLine_Kinematical.ToolTip")); // 260531Cl
            checkBoxKikuchiLine_Kinematical.Checked = true;
            checkBoxKikuchiLine_Kinematical.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxKikuchiLine_Kinematical.Name = "checkBoxKikuchiLine_Kinematical";
            checkBoxKikuchiLine_Kinematical.UseVisualStyleBackColor = true;
            checkBoxKikuchiLine_Kinematical.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // radioButtonKikuchiThresholdOfLength
            // 
            resources.ApplyResources(radioButtonKikuchiThresholdOfLength, "radioButtonKikuchiThresholdOfLength");
            toolTip.SetToolTip(radioButtonKikuchiThresholdOfLength, resources.GetString("radioButtonKikuchiThresholdOfLength.ToolTip")); // 260531Cl
            radioButtonKikuchiThresholdOfLength.Name = "radioButtonKikuchiThresholdOfLength";
            radioButtonKikuchiThresholdOfLength.UseVisualStyleBackColor = true;
            // 
            // numericBoxKikuchiThresholdOfStructureFactor
            // 
            numericBoxKikuchiThresholdOfStructureFactor.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxKikuchiThresholdOfStructureFactor, "numericBoxKikuchiThresholdOfStructureFactor");
            toolTip.SetToolTip(numericBoxKikuchiThresholdOfStructureFactor, resources.GetString("numericBoxKikuchiThresholdOfStructureFactor.ToolTip")); // 260531Cl
            numericBoxKikuchiThresholdOfStructureFactor.Maximum = 1000D;
            numericBoxKikuchiThresholdOfStructureFactor.Minimum = 1D;
            numericBoxKikuchiThresholdOfStructureFactor.Name = "numericBoxKikuchiThresholdOfStructureFactor";
            numericBoxKikuchiThresholdOfStructureFactor.RadianValue = 0.69813170079773179D;
            numericBoxKikuchiThresholdOfStructureFactor.ShowUpDown = true;
            numericBoxKikuchiThresholdOfStructureFactor.SmartIncrement = true;
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
            toolTip.SetToolTip(numericBoxKikuchiThresholdOfLength, resources.GetString("numericBoxKikuchiThresholdOfLength.ToolTip")); // (260531Ch)
            numericBoxKikuchiThresholdOfLength.RadianValue = 0.17453292519943295D;
            numericBoxKikuchiThresholdOfLength.ShowUpDown = true;
            numericBoxKikuchiThresholdOfLength.SmartIncrement = true;
            numericBoxKikuchiThresholdOfLength.Value = 10D;
            numericBoxKikuchiThresholdOfLength.ValueChanged += numericBoxKikuchiThresholdOfStructureFactor_ValueChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip")); // 260531Cl
            label1.Name = "label1";
            // 
            // numericBoxThicknessStep
            // 
            resources.ApplyResources(numericBoxThicknessStep, "numericBoxThicknessStep");
            numericBoxThicknessStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.DecimalPlaces = 2;
            numericBoxThicknessStep.Maximum = 10D;
            numericBoxThicknessStep.Minimum = 0.001D;
            numericBoxThicknessStep.Name = "numericBoxThicknessStep";
            toolTip.SetToolTip(numericBoxThicknessStep, resources.GetString("numericBoxThicknessStep.ToolTip")); // (260531Ch)
            numericBoxThicknessStep.RadianValue = 0.017453292519943295D;
            numericBoxThicknessStep.ShowUpDown = true;
            numericBoxThicknessStep.SmartIncrement = true;
            numericBoxThicknessStep.ThousandsSeparator = true;
            numericBoxThicknessStep.Value = 1D;
            numericBoxThicknessStep.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxMaxNumOfG
            // 
            resources.ApplyResources(numericBoxMaxNumOfG, "numericBoxMaxNumOfG");
            numericBoxMaxNumOfG.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxNumOfG.Maximum = 2048D;
            numericBoxMaxNumOfG.Minimum = 1D;
            numericBoxMaxNumOfG.Name = "numericBoxMaxNumOfG";
            toolTip.SetToolTip(numericBoxMaxNumOfG, resources.GetString("numericBoxMaxNumOfG.ToolTip")); // (260531Ch)
            numericBoxMaxNumOfG.RadianValue = 0.55850536063818546D;
            numericBoxMaxNumOfG.ShowUpDown = true;
            numericBoxMaxNumOfG.SmartIncrement = true;
            numericBoxMaxNumOfG.ThousandsSeparator = true;
            numericBoxMaxNumOfG.Value = 32D;
            // 
            // checkBoxNonLocalAbsorption
            // 
            resources.ApplyResources(checkBoxNonLocalAbsorption, "checkBoxNonLocalAbsorption");
            toolTip.SetToolTip(checkBoxNonLocalAbsorption, resources.GetString("checkBoxNonLocalAbsorption.ToolTip")); // 260531Cl
            checkBoxNonLocalAbsorption.Name = "checkBoxNonLocalAbsorption";
            checkBoxNonLocalAbsorption.UseVisualStyleBackColor = true;
            // 
            // checkBoxTDSBackground
            // 
            resources.ApplyResources(checkBoxTDSBackground, "checkBoxTDSBackground");
            toolTip.SetToolTip(checkBoxTDSBackground, resources.GetString("checkBoxTDSBackground.ToolTip")); // 260531Cl
            checkBoxTDSBackground.Name = "checkBoxTDSBackground";
            checkBoxTDSBackground.UseVisualStyleBackColor = true;
            // 
            // numericBoxThicknessStart
            // 
            resources.ApplyResources(numericBoxThicknessStart, "numericBoxThicknessStart");
            numericBoxThicknessStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.DecimalPlaces = 2;
            numericBoxThicknessStart.Maximum = 1000D;
            numericBoxThicknessStart.Minimum = 0.001D;
            numericBoxThicknessStart.Name = "numericBoxThicknessStart";
            toolTip.SetToolTip(numericBoxThicknessStart, resources.GetString("numericBoxThicknessStart.ToolTip")); // (260531Ch)
            numericBoxThicknessStart.RadianValue = 0.017453292519943295D;
            numericBoxThicknessStart.ShowUpDown = true;
            numericBoxThicknessStart.SmartIncrement = true;
            numericBoxThicknessStart.ThousandsSeparator = true;
            numericBoxThicknessStart.Value = 1D;
            numericBoxThicknessStart.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxThicknessEnd
            // 
            resources.ApplyResources(numericBoxThicknessEnd, "numericBoxThicknessEnd");
            numericBoxThicknessEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.DecimalPlaces = 2;
            numericBoxThicknessEnd.Maximum = 1000D;
            numericBoxThicknessEnd.Minimum = 0.001D;
            numericBoxThicknessEnd.Name = "numericBoxThicknessEnd";
            toolTip.SetToolTip(numericBoxThicknessEnd, resources.GetString("numericBoxThicknessEnd.ToolTip")); // (260531Ch)
            numericBoxThicknessEnd.RadianValue = 0.87266462599716477D;
            numericBoxThicknessEnd.ShowUpDown = true;
            numericBoxThicknessEnd.SmartIncrement = true;
            numericBoxThicknessEnd.ThousandsSeparator = true;
            numericBoxThicknessEnd.Value = 50D;
            numericBoxThicknessEnd.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // buttonStop
            // 
            resources.ApplyResources(buttonStop, "buttonStop");
            toolTip.SetToolTip(buttonStop, resources.GetString("buttonStop.ToolTip")); // 260531Cl
            buttonStop.BackColor = System.Drawing.Color.IndianRed;
            buttonStop.ForeColor = System.Drawing.Color.White;
            buttonStop.Name = "buttonStop";
            buttonStop.UseVisualStyleBackColor = false;
            // 
            // groupBoxOutput
            // 
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
            resources.ApplyResources(groupBoxOutput, "groupBoxOutput");
            groupBoxOutput.Name = "groupBoxOutput";
            groupBoxOutput.TabStop = false;
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
            // numericBoxEnergy
            // 
            numericBoxEnergy.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxEnergy, "numericBoxEnergy");
            toolTip.SetToolTip(numericBoxEnergy, resources.GetString("numericBoxEnergy.ToolTip")); // 260531Cl
            numericBoxEnergy.Name = "numericBoxEnergy";
            numericBoxEnergy.ReadOnly = true;
            numericBoxEnergy.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarOutputEnergy
            // 
            resources.ApplyResources(trackBarOutputEnergy, "trackBarOutputEnergy");
            toolTip.SetToolTip(trackBarOutputEnergy, resources.GetString("trackBarOutputEnergy.ToolTip")); // 260531Cl
            trackBarOutputEnergy.LargeChange = 1;
            trackBarOutputEnergy.Maximum = 5;
            trackBarOutputEnergy.Name = "trackBarOutputEnergy";
            trackBarOutputEnergy.ValueChanged += trackBarOutputEnergy_ValueChanged;
            // 
            // numericBoxDepth
            // 
            numericBoxDepth.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxDepth, "numericBoxDepth");
            toolTip.SetToolTip(numericBoxDepth, resources.GetString("numericBoxDepth.ToolTip")); // 260531Cl
            numericBoxDepth.Name = "numericBoxDepth";
            numericBoxDepth.ReadOnly = true;
            numericBoxDepth.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarOutputThickness
            // 
            resources.ApplyResources(trackBarOutputThickness, "trackBarOutputThickness");
            toolTip.SetToolTip(trackBarOutputThickness, resources.GetString("trackBarOutputThickness.ToolTip")); // 260531Cl
            trackBarOutputThickness.LargeChange = 1;
            trackBarOutputThickness.Maximum = 9;
            trackBarOutputThickness.Name = "trackBarOutputThickness";
            trackBarOutputThickness.ValueChanged += TrackBarOutputThickness_Scroll;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip")); // 260531Cl
            label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip")); // 260531Cl
            label4.Name = "label4";
            // 
            // checkBoxWithBSEDistribution
            // 
            resources.ApplyResources(checkBoxWithBSEDistribution, "checkBoxWithBSEDistribution");
            toolTip.SetToolTip(checkBoxWithBSEDistribution, resources.GetString("checkBoxWithBSEDistribution.ToolTip")); // 260531Cl
            checkBoxWithBSEDistribution.Checked = true;
            checkBoxWithBSEDistribution.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxWithBSEDistribution.Name = "checkBoxWithBSEDistribution";
            checkBoxWithBSEDistribution.UseVisualStyleBackColor = true;
            checkBoxWithBSEDistribution.CheckedChanged += checkBoxWithBSEDistribution_CheckedChanged;
            // 
            // comboBoxGradient
            // 
            comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxGradient, "comboBoxGradient");
            toolTip.SetToolTip(comboBoxGradient, resources.GetString("comboBoxGradient.ToolTip")); // 260531Cl
            comboBoxGradient.FormattingEnabled = true;
            comboBoxGradient.Items.AddRange(new object[] { resources.GetString("comboBoxGradient.Items"), resources.GetString("comboBoxGradient.Items1") });
            comboBoxGradient.Name = "comboBoxGradient";
            comboBoxGradient.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // comboBoxScale
            // 
            comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxScale, "comboBoxScale");
            toolTip.SetToolTip(comboBoxScale, resources.GetString("comboBoxScale.ToolTip")); // 260531Cl
            comboBoxScale.FormattingEnabled = true;
            comboBoxScale.Items.AddRange(new object[] { resources.GetString("comboBoxScale.Items"), resources.GetString("comboBoxScale.Items1"), resources.GetString("comboBoxScale.Items2"), resources.GetString("comboBoxScale.Items3") });
            comboBoxScale.Name = "comboBoxScale";
            comboBoxScale.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // trackBarIntensityBrightnessMax
            // 
            resources.ApplyResources(trackBarIntensityBrightnessMax, "trackBarIntensityBrightnessMax");
            toolTip.SetToolTip(trackBarIntensityBrightnessMax, resources.GetString("trackBarIntensityBrightnessMax.ToolTip")); // 260531Cl
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
            toolTip.SetToolTip(trackBarIntensityBrightnessMin, resources.GetString("trackBarIntensityBrightnessMin.ToolTip")); // 260531Cl
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
            toolTip.SetToolTip(label8, resources.GetString("label8.ToolTip")); // 260531Cl
            label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip")); // 260531Cl
            label7.Name = "label7";
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            toolTip.SetToolTip(label10, resources.GetString("label10.ToolTip")); // 260531Cl
            label10.Name = "label10";
            // 
            // checkBoxShowOverlays
            // 
            resources.ApplyResources(checkBoxShowOverlays, "checkBoxShowOverlays");
            toolTip.SetToolTip(checkBoxShowOverlays, resources.GetString("checkBoxShowOverlays.ToolTip")); // 260531Cl
            checkBoxShowOverlays.Checked = true;
            checkBoxShowOverlays.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowOverlays.Name = "checkBoxShowOverlays";
            checkBoxShowOverlays.UseVisualStyleBackColor = true;
            checkBoxShowOverlays.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // buttonCopyImage
            // 
            resources.ApplyResources(buttonCopyImage, "buttonCopyImage");
            toolTip.SetToolTip(buttonCopyImage, resources.GetString("buttonCopyImage.ToolTip")); // 260531Cl
            buttonCopyImage.Name = "buttonCopyImage";
            buttonCopyImage.UseVisualStyleBackColor = true;
            buttonCopyImage.Click += buttonCopyImage_Click;
            // 
            // numericBoxEnergyEnd
            // 
            resources.ApplyResources(numericBoxEnergyEnd, "numericBoxEnergyEnd");
            numericBoxEnergyEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyEnd.DecimalPlaces = 2;
            numericBoxEnergyEnd.Maximum = 1000D;
            numericBoxEnergyEnd.Minimum = 0.001D;
            numericBoxEnergyEnd.Name = "numericBoxEnergyEnd";
            toolTip.SetToolTip(numericBoxEnergyEnd, resources.GetString("numericBoxEnergyEnd.ToolTip")); // (260531Ch)
            numericBoxEnergyEnd.RadianValue = 0.26179938779914941D;
            numericBoxEnergyEnd.ShowUpDown = true;
            numericBoxEnergyEnd.SmartIncrement = true;
            numericBoxEnergyEnd.ThousandsSeparator = true;
            numericBoxEnergyEnd.Value = 15D;
            numericBoxEnergyEnd.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // numericBoxEnergyStart
            // 
            resources.ApplyResources(numericBoxEnergyStart, "numericBoxEnergyStart");
            numericBoxEnergyStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.DecimalPlaces = 2;
            numericBoxEnergyStart.Maximum = 1000D;
            numericBoxEnergyStart.Minimum = 1D;
            numericBoxEnergyStart.Name = "numericBoxEnergyStart";
            toolTip.SetToolTip(numericBoxEnergyStart, resources.GetString("numericBoxEnergyStart.ToolTip")); // (260531Ch)
            numericBoxEnergyStart.RadianValue = 0.3490658503988659D;
            numericBoxEnergyStart.ShowUpDown = true;
            numericBoxEnergyStart.SmartIncrement = true;
            numericBoxEnergyStart.ThousandsSeparator = true;
            numericBoxEnergyStart.Value = 20D;
            numericBoxEnergyStart.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // numericBoxEnergyStep
            // 
            resources.ApplyResources(numericBoxEnergyStep, "numericBoxEnergyStep");
            numericBoxEnergyStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStep.DecimalPlaces = 2;
            numericBoxEnergyStep.Maximum = 10D;
            numericBoxEnergyStep.Minimum = 0.001D;
            numericBoxEnergyStep.Name = "numericBoxEnergyStep";
            toolTip.SetToolTip(numericBoxEnergyStep, resources.GetString("numericBoxEnergyStep.ToolTip")); // (260531Ch)
            numericBoxEnergyStep.RadianValue = 0.017453292519943295D;
            numericBoxEnergyStep.ShowUpDown = true;
            numericBoxEnergyStep.SmartIncrement = true;
            numericBoxEnergyStep.ThousandsSeparator = true;
            numericBoxEnergyStep.Value = 1D;
            numericBoxEnergyStep.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip")); // 260531Cl
            label13.BackColor = System.Drawing.Color.White;
            label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            toolTip.SetToolTip(label14, resources.GetString("label14.ToolTip")); // 260531Cl
            label14.BackColor = System.Drawing.Color.White;
            label14.Name = "label14";
            // 
            // checkBoxShowDyanmicalEBSD
            // 
            resources.ApplyResources(checkBoxShowDyanmicalEBSD, "checkBoxShowDyanmicalEBSD");
            toolTip.SetToolTip(checkBoxShowDyanmicalEBSD, resources.GetString("checkBoxShowDyanmicalEBSD.ToolTip")); // 260531Cl
            checkBoxShowDyanmicalEBSD.Checked = true;
            checkBoxShowDyanmicalEBSD.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowDyanmicalEBSD.Name = "checkBoxShowDyanmicalEBSD";
            checkBoxShowDyanmicalEBSD.UseVisualStyleBackColor = true;
            checkBoxShowDyanmicalEBSD.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxDrawDetectorOutline
            // 
            resources.ApplyResources(checkBoxDrawDetectorOutline, "checkBoxDrawDetectorOutline");
            toolTip.SetToolTip(checkBoxDrawDetectorOutline, resources.GetString("checkBoxDrawDetectorOutline.ToolTip")); // 260531Cl
            checkBoxDrawDetectorOutline.Checked = true;
            checkBoxDrawDetectorOutline.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawDetectorOutline.Name = "checkBoxDrawDetectorOutline";
            checkBoxDrawDetectorOutline.UseVisualStyleBackColor = true;
            checkBoxDrawDetectorOutline.CheckedChanged += checkBoxDrawDetectorOutline_CheckedChanged;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.HotTrack = true;
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.SystemColors.Control;
            tabPage1.Controls.Add(groupBoxEBSDGeometry);
            tabPage1.Controls.Add(groupBoxSampleCondition);
            tabPage1.Controls.Add(flowLayoutPanelViewAlong);
            tabPage1.Controls.Add(panelGeometry);
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.Name = "tabPage1";
            // 
            // groupBoxEBSDGeometry
            // 
            groupBoxEBSDGeometry.Controls.Add(label2);
            groupBoxEBSDGeometry.Controls.Add(label17);
            groupBoxEBSDGeometry.Controls.Add(label16);
            groupBoxEBSDGeometry.Controls.Add(numericBoxYofDet);
            groupBoxEBSDGeometry.Controls.Add(numericBoxDetRadius);
            groupBoxEBSDGeometry.Controls.Add(numericBoxZofDet);
            groupBoxEBSDGeometry.Controls.Add(numericBoxDetTilt);
            resources.ApplyResources(groupBoxEBSDGeometry, "groupBoxEBSDGeometry");
            captureExtender.SetCapture(groupBoxEBSDGeometry, true); // 260524Cl 追加: Wiki 用クロップ (検出器ジオメトリ)
            groupBoxEBSDGeometry.Name = "groupBoxEBSDGeometry";
            groupBoxEBSDGeometry.TabStop = false;
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            toolTip.SetToolTip(label17, resources.GetString("label17.ToolTip")); // 260531Cl
            label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            toolTip.SetToolTip(label16, resources.GetString("label16.ToolTip")); // 260531Cl
            label16.Name = "label16";
            // 
            // groupBoxSampleCondition
            // 
            groupBoxSampleCondition.Controls.Add(waveLengthControl);
            groupBoxSampleCondition.Controls.Add(numericBoxSampleTilt);
            resources.ApplyResources(groupBoxSampleCondition, "groupBoxSampleCondition");
            captureExtender.SetCapture(groupBoxSampleCondition, true); // 260524Cl 追加: Wiki 用クロップ (SEM・試料条件)
            groupBoxSampleCondition.Name = "groupBoxSampleCondition";
            groupBoxSampleCondition.TabStop = false;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.SystemColors.Control;
            tabPage2.Controls.Add(label15);
            tabPage2.Controls.Add(buttonSimulateBSE);
            tabPage2.Controls.Add(label13);
            tabPage2.Controls.Add(checkBoxDrawAxesInStereonet);
            tabPage2.Controls.Add(label14);
            tabPage2.Controls.Add(poleFigureControl);
            tabPage2.Controls.Add(graphControlDepthProfile);
            tabPage2.Controls.Add(graphControlEnergyProfile);
            resources.ApplyResources(tabPage2, "tabPage2");
            captureExtender.SetCapture(tabPage2, true); // 260524Cl 追加: Wiki 用クロップ (BSE 分布タブ)
            tabPage2.Name = "tabPage2";
            // 
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            toolTip.SetToolTip(label15, resources.GetString("label15.ToolTip")); // 260531Cl
            label15.BackColor = System.Drawing.Color.White;
            label15.Name = "label15";
            // 
            // tabPage3
            // 
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
            resources.ApplyResources(tabPage3, "tabPage3");
            captureExtender.SetCapture(tabPage3, true); // 260524Cl 追加: Wiki 用クロップ (オーバーレイ タブ)
            tabPage3.Name = "tabPage3";
            // 
            // groupBoxDetectorOutline
            // 
            groupBoxDetectorOutline.Controls.Add(checkBoxShowMesh);
            groupBoxDetectorOutline.Controls.Add(checkBoxShowCircle);
            resources.ApplyResources(groupBoxDetectorOutline, "groupBoxDetectorOutline");
            groupBoxDetectorOutline.Name = "groupBoxDetectorOutline";
            groupBoxDetectorOutline.TabStop = false;
            // 
            // checkBoxShowMesh
            // 
            resources.ApplyResources(checkBoxShowMesh, "checkBoxShowMesh");
            toolTip.SetToolTip(checkBoxShowMesh, resources.GetString("checkBoxShowMesh.ToolTip")); // 260531Cl
            checkBoxShowMesh.Checked = true;
            checkBoxShowMesh.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowMesh.Name = "checkBoxShowMesh";
            checkBoxShowMesh.UseVisualStyleBackColor = true;
            checkBoxShowMesh.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxShowCircle
            // 
            resources.ApplyResources(checkBoxShowCircle, "checkBoxShowCircle");
            toolTip.SetToolTip(checkBoxShowCircle, resources.GetString("checkBoxShowCircle.ToolTip")); // 260531Cl
            checkBoxShowCircle.Checked = true;
            checkBoxShowCircle.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowCircle.Name = "checkBoxShowCircle";
            checkBoxShowCircle.UseVisualStyleBackColor = true;
            checkBoxShowCircle.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(trackBarStrSize);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(colorControlString);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // checkBoxShowKikuchiLines
            // 
            resources.ApplyResources(checkBoxShowKikuchiLines, "checkBoxShowKikuchiLines");
            toolTip.SetToolTip(checkBoxShowKikuchiLines, resources.GetString("checkBoxShowKikuchiLines.ToolTip")); // 260531Cl
            checkBoxShowKikuchiLines.Checked = true;
            checkBoxShowKikuchiLines.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowKikuchiLines.Name = "checkBoxShowKikuchiLines";
            checkBoxShowKikuchiLines.UseVisualStyleBackColor = true;
            checkBoxShowKikuchiLines.CheckedChanged += checkBoxShowKikuchiLines_CheckedChanged;
            // 
            // groupBoxLatticePlanes
            // 
            groupBoxLatticePlanes.Controls.Add(radioButtonKikuchiThresholdOfLength);
            groupBoxLatticePlanes.Controls.Add(radioButtonKikuchiThresholdOfStructureFactor);
            groupBoxLatticePlanes.Controls.Add(numericBoxKikuchiThresholdOfStructureFactor);
            groupBoxLatticePlanes.Controls.Add(numericBoxKikuchiThresholdOfLength);
            resources.ApplyResources(groupBoxLatticePlanes, "groupBoxLatticePlanes");
            groupBoxLatticePlanes.Name = "groupBoxLatticePlanes";
            groupBoxLatticePlanes.TabStop = false;
            // 
            // groupBoxKikuchiLines
            // 
            groupBoxKikuchiLines.Controls.Add(colorControlExcessLine);
            groupBoxKikuchiLines.Controls.Add(checkBoxKikuchiLine_Kinematical);
            groupBoxKikuchiLines.Controls.Add(trackBarLineWidth);
            groupBoxKikuchiLines.Controls.Add(label11);
            resources.ApplyResources(groupBoxKikuchiLines, "groupBoxKikuchiLines");
            groupBoxKikuchiLines.Name = "groupBoxKikuchiLines";
            groupBoxKikuchiLines.TabStop = false;
            // 
            // checkBoxShowGIndices
            // 
            resources.ApplyResources(checkBoxShowGIndices, "checkBoxShowGIndices");
            toolTip.SetToolTip(checkBoxShowGIndices, resources.GetString("checkBoxShowGIndices.ToolTip")); // 260531Cl
            checkBoxShowGIndices.Name = "checkBoxShowGIndices";
            checkBoxShowGIndices.UseVisualStyleBackColor = true;
            checkBoxShowGIndices.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxShowZoneAxisIndices
            // 
            resources.ApplyResources(checkBoxShowZoneAxisIndices, "checkBoxShowZoneAxisIndices");
            toolTip.SetToolTip(checkBoxShowZoneAxisIndices, resources.GetString("checkBoxShowZoneAxisIndices.ToolTip")); // 260531Cl
            checkBoxShowZoneAxisIndices.Checked = true;
            checkBoxShowZoneAxisIndices.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowZoneAxisIndices.Name = "checkBoxShowZoneAxisIndices";
            checkBoxShowZoneAxisIndices.UseVisualStyleBackColor = true;
            checkBoxShowZoneAxisIndices.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // groupBoxSimulationParameters
            // 
            resources.ApplyResources(groupBoxSimulationParameters, "groupBoxSimulationParameters");
            captureExtender.SetCapture(groupBoxSimulationParameters, true); // 260524Cl 追加: Wiki 用クロップ (動力学計算パラメータ)
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
            // labelMasterPatternGrid
            // 
            resources.ApplyResources(labelMasterPatternGrid, "labelMasterPatternGrid");
            toolTip.SetToolTip(labelMasterPatternGrid, resources.GetString("labelMasterPatternGrid.ToolTip")); // 260531Cl
            labelMasterPatternGrid.Name = "labelMasterPatternGrid";
            // 
            // comboBoxMasterPatternGrid
            // 
            comboBoxMasterPatternGrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxMasterPatternGrid, "comboBoxMasterPatternGrid");
            toolTip.SetToolTip(comboBoxMasterPatternGrid, resources.GetString("comboBoxMasterPatternGrid.ToolTip")); // 260531Cl
            comboBoxMasterPatternGrid.FormattingEnabled = true;
            comboBoxMasterPatternGrid.Items.AddRange(new object[] { resources.GetString("comboBoxMasterPatternGrid.Items"), resources.GetString("comboBoxMasterPatternGrid.Items1"), resources.GetString("comboBoxMasterPatternGrid.Items2"), resources.GetString("comboBoxMasterPatternGrid.Items3"), resources.GetString("comboBoxMasterPatternGrid.Items4"), resources.GetString("comboBoxMasterPatternGrid.Items5"), resources.GetString("comboBoxMasterPatternGrid.Items6"), resources.GetString("comboBoxMasterPatternGrid.Items7"), resources.GetString("comboBoxMasterPatternGrid.Items8"), resources.GetString("comboBoxMasterPatternGrid.Items9") });
            comboBoxMasterPatternGrid.Name = "comboBoxMasterPatternGrid";
            comboBoxMasterPatternGrid.SelectedIndexChanged += MasterPatternSelectionChanged;
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
            statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3 });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            resources.ApplyResources(toolStripProgressBar, "toolStripProgressBar");
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            resources.ApplyResources(toolStripStatusLabel2, "toolStripStatusLabel2");
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            resources.ApplyResources(toolStripStatusLabel3, "toolStripStatusLabel3");
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
            // numericBoxMasterPatternEnergy
            // 
            numericBoxMasterPatternEnergy.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxMasterPatternEnergy, "numericBoxMasterPatternEnergy");
            toolTip.SetToolTip(numericBoxMasterPatternEnergy, resources.GetString("numericBoxMasterPatternEnergy.ToolTip")); // 260531Cl
            numericBoxMasterPatternEnergy.Name = "numericBoxMasterPatternEnergy";
            numericBoxMasterPatternEnergy.ReadOnly = true;
            numericBoxMasterPatternEnergy.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarMasterPatternEnergy
            // 
            resources.ApplyResources(trackBarMasterPatternEnergy, "trackBarMasterPatternEnergy");
            toolTip.SetToolTip(trackBarMasterPatternEnergy, resources.GetString("trackBarMasterPatternEnergy.ToolTip")); // 260531Cl
            trackBarMasterPatternEnergy.LargeChange = 1;
            trackBarMasterPatternEnergy.Maximum = 0;
            trackBarMasterPatternEnergy.Name = "trackBarMasterPatternEnergy";
            trackBarMasterPatternEnergy.ValueChanged += MasterPatternSelectionChanged;
            // 
            // numericBoxMasterPatternDepth
            // 
            numericBoxMasterPatternDepth.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxMasterPatternDepth, "numericBoxMasterPatternDepth");
            toolTip.SetToolTip(numericBoxMasterPatternDepth, resources.GetString("numericBoxMasterPatternDepth.ToolTip")); // 260531Cl
            numericBoxMasterPatternDepth.Name = "numericBoxMasterPatternDepth";
            numericBoxMasterPatternDepth.ReadOnly = true;
            numericBoxMasterPatternDepth.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarMasterPatternDepth
            // 
            resources.ApplyResources(trackBarMasterPatternDepth, "trackBarMasterPatternDepth");
            toolTip.SetToolTip(trackBarMasterPatternDepth, resources.GetString("trackBarMasterPatternDepth.ToolTip")); // 260531Cl
            trackBarMasterPatternDepth.LargeChange = 1;
            trackBarMasterPatternDepth.Maximum = 0;
            trackBarMasterPatternDepth.Name = "trackBarMasterPatternDepth";
            trackBarMasterPatternDepth.ValueChanged += MasterPatternSelectionChanged;
            // 
            // labelMasterPattern2DHemisphere
            // 
            resources.ApplyResources(labelMasterPattern2DHemisphere, "labelMasterPattern2DHemisphere");
            toolTip.SetToolTip(labelMasterPattern2DHemisphere, resources.GetString("labelMasterPattern2DHemisphere.ToolTip")); // 260531Cl
            labelMasterPattern2DHemisphere.Name = "labelMasterPattern2DHemisphere";
            // 
            // comboBoxMasterPattern2DHemisphere
            // 
            comboBoxMasterPattern2DHemisphere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxMasterPattern2DHemisphere, "comboBoxMasterPattern2DHemisphere");
            toolTip.SetToolTip(comboBoxMasterPattern2DHemisphere, resources.GetString("comboBoxMasterPattern2DHemisphere.ToolTip")); // 260531Cl
            comboBoxMasterPattern2DHemisphere.FormattingEnabled = true;
            comboBoxMasterPattern2DHemisphere.Items.AddRange(new object[] { resources.GetString("comboBoxMasterPattern2DHemisphere.Items"), resources.GetString("comboBoxMasterPattern2DHemisphere.Items1") });
            comboBoxMasterPattern2DHemisphere.Name = "comboBoxMasterPattern2DHemisphere";
            comboBoxMasterPattern2DHemisphere.SelectedIndexChanged += MasterPatternSelectionChanged;
            // 
            // buttonCreateMasterPattern
            // 
            resources.ApplyResources(buttonCreateMasterPattern, "buttonCreateMasterPattern");
            toolTip.SetToolTip(buttonCreateMasterPattern, resources.GetString("buttonCreateMasterPattern.ToolTip")); // 260531Cl
            buttonCreateMasterPattern.BackColor = System.Drawing.Color.SteelBlue;
            buttonCreateMasterPattern.ForeColor = System.Drawing.Color.White;
            buttonCreateMasterPattern.Name = "buttonCreateMasterPattern";
            buttonCreateMasterPattern.UseVisualStyleBackColor = false;
            buttonCreateMasterPattern.Click += buttonCreateMasterPattern_Click;
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
            captureExtender.SetCapture(groupBoxMasterPattern, true); // 260524Cl 追加: Wiki 用クロップ (マスターパターン 2D/3D 表示)
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
            toolTip.SetToolTip(buttonMasterPattern2DCopy, resources.GetString("buttonMasterPattern2DCopy.ToolTip")); // 260531Cl
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
            toolTip.SetToolTip(buttonMasterPattern3DCopy, resources.GetString("buttonMasterPattern3DCopy.ToolTip")); // 260531Cl
            buttonMasterPattern3DCopy.Name = "buttonMasterPattern3DCopy";
            buttonMasterPattern3DCopy.UseVisualStyleBackColor = true;
            // 
            // checkBoxMasterPattern3DAxisLabel
            // 
            resources.ApplyResources(checkBoxMasterPattern3DAxisLabel, "checkBoxMasterPattern3DAxisLabel");
            toolTip.SetToolTip(checkBoxMasterPattern3DAxisLabel, resources.GetString("checkBoxMasterPattern3DAxisLabel.ToolTip")); // 260531Cl
            checkBoxMasterPattern3DAxisLabel.Checked = true;
            checkBoxMasterPattern3DAxisLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxMasterPattern3DAxisLabel.Name = "checkBoxMasterPattern3DAxisLabel";
            checkBoxMasterPattern3DAxisLabel.UseVisualStyleBackColor = true;
            checkBoxMasterPattern3DAxisLabel.CheckedChanged += checkBoxMasterPattern3DAxisLabel_CheckedChanged;
            // 
            // checkBoxMasterPattern3DAxisArrows
            // 
            resources.ApplyResources(checkBoxMasterPattern3DAxisArrows, "checkBoxMasterPattern3DAxisArrows");
            toolTip.SetToolTip(checkBoxMasterPattern3DAxisArrows, resources.GetString("checkBoxMasterPattern3DAxisArrows.ToolTip")); // 260531Cl
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
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(indexControl);
            flowLayoutPanelMasterPattern3DViewAlong.Name = "flowLayoutPanelMasterPattern3DViewAlong";
            // 
            // buttonMasterPattern3DViewAlong
            // 
            resources.ApplyResources(buttonMasterPattern3DViewAlong, "buttonMasterPattern3DViewAlong");
            toolTip.SetToolTip(buttonMasterPattern3DViewAlong, resources.GetString("buttonMasterPattern3DViewAlong.ToolTip")); // 260531Cl
            buttonMasterPattern3DViewAlong.Name = "buttonMasterPattern3DViewAlong";
            buttonMasterPattern3DViewAlong.UseVisualStyleBackColor = true;
            buttonMasterPattern3DViewAlong.Click += buttonMasterPattern3DViewAlong_Click;
            // 
            // indexControl
            // 
            resources.ApplyResources(indexControl, "indexControl");
            toolTip.SetToolTip(indexControl, resources.GetString("indexControl.ToolTip")); // 260531Cl
            indexControl.LabelVisible = false;
            indexControl.Name = "indexControl";
            indexControl.Values = ((int, int, int))resources.GetObject("indexControl.Values");
            // 
            // groupBoxEBSDPattern
            // 
            groupBoxEBSDPattern.Controls.Add(graphicsBox);
            groupBoxEBSDPattern.Controls.Add(flowLayoutPanel1);
            groupBoxEBSDPattern.Controls.Add(groupBoxOutput);
            resources.ApplyResources(groupBoxEBSDPattern, "groupBoxEBSDPattern");
            captureExtender.SetCapture(groupBoxEBSDPattern, true); // 260524Cl 追加: Wiki 用クロップ (EBSD パターン・出力パラメータ)
            groupBoxEBSDPattern.Name = "groupBoxEBSDPattern";
            groupBoxEBSDPattern.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(buttonCopyImage);
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
            flowLayoutPanel9.ResumeLayout(false);
            flowLayoutPanel9.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            flowLayoutPanelMasterPatternSelectors.ResumeLayout(false);
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
        private NumericBox numericBoxKikuchiThresholdOfStructureFactor;
        private NumericBox numericBoxKikuchiThresholdOfLength;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.GroupBox groupBoxKikuchiLines;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxDetectorOutline;
        private System.Windows.Forms.CheckBox checkBoxShowCircle;
        private System.Windows.Forms.CheckBox checkBoxShowMesh;
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
    }
}

