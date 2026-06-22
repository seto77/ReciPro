using System;

namespace ReciPro
{
    partial class FormImageSimulator
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
                scaleImage.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        // (260323Ch) renamed numeric container controls:
        // groupBox1 -> groupBoxSimulation
        // groupBox2 -> groupBoxDisplay
        // groupBox3 -> groupBoxAdjust
        // groupBox4 -> groupBoxTEMConditions
        // groupBox6 -> groupBoxImageMode
        // groupBox7 -> groupBoxDiffractedWaves
        // groupBox8 -> groupBoxImageProperty
        // panel1 -> panelImageStatus
        // panel2 -> panelDisplaySettings
        // flowLayoutPanel6 -> flowLayoutPanelIntensityRange
        // panel3 -> panelModeOptions
        // panel5 -> panelSerialSettings
        // flowLayoutPanel4 -> flowLayoutPanelSimulationMode
        // flowLayoutPanel11 -> flowLayoutPanelPotentialMode
        // panel6 -> panelImageProperties
        // flowLayoutPanel15 -> flowLayoutPanelOuterRadius
        // flowLayoutPanel9 -> flowLayoutPanelInnerRadius
        // flowLayoutPanel2 -> flowLayoutPanelConvergenceRadius
        // flowLayoutPanel1 -> flowLayoutPanelSpotCount
        // flowLayoutPanel5 -> flowLayoutPanelObjectiveAperture
        // flowLayoutPanel3 -> flowLayoutPanelScherzer
        // flowLayoutPanel14 -> flowLayoutPanelModeSelection
        // flowLayoutPanel16 -> flowLayoutPanelImageType
        // panel4 -> panelSimulationActions
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageSimulator));
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            panelImageStatus = new System.Windows.Forms.Panel();
            pictureBoxScaleOfIntensity = new System.Windows.Forms.PictureBox();
            labelMousePositionValue = new System.Windows.Forms.Label();
            label33 = new System.Windows.Forms.Label();
            labelMousePositionY = new System.Windows.Forms.Label();
            labelMousePositionX = new System.Windows.Forms.Label();
            label31 = new System.Windows.Forms.Label();
            label27 = new System.Windows.Forms.Label();
            label30 = new System.Windows.Forms.Label();
            label29 = new System.Windows.Forms.Label();
            label26 = new System.Windows.Forms.Label();
            label28 = new System.Windows.Forms.Label();
            panelDisplaySettings = new System.Windows.Forms.Panel();
            groupBoxAdjust = new System.Windows.Forms.GroupBox();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            label25 = new System.Windows.Forms.Label();
            comboBoxScaleColorScale = new System.Windows.Forms.ComboBox();
            checkBoxGaussianBlur = new System.Windows.Forms.CheckBox();
            numericBoxGaussianBlurRadius = new NumericBox();
            trackBarAdvancedMax = new TrackBarAdvanced();
            trackBarAdvancedMin = new TrackBarAdvanced();
            panel1 = new System.Windows.Forms.Panel();
            groupBoxNormalization = new System.Windows.Forms.GroupBox();
            checkBoxNormarizeIndividually = new System.Windows.Forms.CheckBox();
            flowLayoutPanelIntensityRange = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxIntensityMin = new System.Windows.Forms.CheckBox();
            numericBoxIntensityMin = new NumericBox();
            checkBoxIntensityMax = new System.Windows.Forms.CheckBox();
            numericBoxIntensityMax = new NumericBox();
            panel9 = new System.Windows.Forms.Panel();
            groupBoxSTEMoption3 = new System.Windows.Forms.GroupBox();
            radioButtonSTEM_target_TDS = new System.Windows.Forms.RadioButton();
            radioButtonSTEM_target_elas = new System.Windows.Forms.RadioButton();
            radioButtonSTEM_target_both = new System.Windows.Forms.RadioButton();
            groupBoxDisplay = new System.Windows.Forms.GroupBox();
            colorControlScale = new ColorControl();
            numericBoxScaleLength = new NumericBox();
            checkBoxShowScale = new System.Windows.Forms.CheckBox();
            colorControlLabel = new ColorControl();
            numericBoxLabelFontSize = new NumericBox();
            checkBoxShowLabel = new System.Windows.Forms.CheckBox();
            checkBoxShowUnitcell = new System.Windows.Forms.CheckBox();
            groupBoxSimulation = new System.Windows.Forms.GroupBox();
            panelModeOptions = new System.Windows.Forms.Panel();
            groupBoxSerialImage = new System.Windows.Forms.GroupBox();
            panelSerial = new System.Windows.Forms.Panel();
            panelSerialDefocus = new System.Windows.Forms.Panel();
            numericBoxDefocusNum = new NumericBox();
            numericBoxDefocusStep = new NumericBox();
            numericBoxDefocusStart = new NumericBox();
            textBoxDefocusList = new System.Windows.Forms.TextBox();
            panelSerialThickness = new System.Windows.Forms.Panel();
            numericBoxThicknessNum = new NumericBox();
            numericBoxThicknessStep = new NumericBox();
            numericBoxThicknessStart = new NumericBox();
            textBoxThicknessList = new System.Windows.Forms.TextBox();
            panelSerialSettings = new System.Windows.Forms.Panel();
            checkBoxSerialThickness = new System.Windows.Forms.CheckBox();
            checkBoxSerialDefocus = new System.Windows.Forms.CheckBox();
            flowLayoutPanelHorizontalDirection = new System.Windows.Forms.FlowLayoutPanel();
            label6 = new System.Windows.Forms.Label();
            radioButtonHorizontalDefocus = new System.Windows.Forms.RadioButton();
            radioButtonHorizontalThickness = new System.Windows.Forms.RadioButton();
            flowLayoutPanelSimulationMode = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonSingleMode = new System.Windows.Forms.RadioButton();
            radioButtonSerialMode = new System.Windows.Forms.RadioButton();
            groupBoxPotentialOption = new System.Windows.Forms.GroupBox();
            flowLayoutPanelPotentialMode = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonPotentialModeMagAndPhase = new System.Windows.Forms.RadioButton();
            flowLayoutPanelMagAndPhase = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonPotentialShowMagAndPhase = new System.Windows.Forms.RadioButton();
            radioButtonPotentialShowMag = new System.Windows.Forms.RadioButton();
            radioButtonPotentialShowPhase = new System.Windows.Forms.RadioButton();
            panelPhaseScale = new System.Windows.Forms.Panel();
            label24 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            pictureBoxPhaseScale = new System.Windows.Forms.PictureBox();
            label17 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            radioButtonPotentialModeRealAndImag = new System.Windows.Forms.RadioButton();
            flowLayoutPanelRealAndImaiginary = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonPotentialShowRealAndImag = new System.Windows.Forms.RadioButton();
            radioButtonPotentialShowReal = new System.Windows.Forms.RadioButton();
            radioButtonPotentialShowImag = new System.Windows.Forms.RadioButton();
            checkBoxPotentialUgPrime = new System.Windows.Forms.CheckBox();
            checkBoxPotentialUg = new System.Windows.Forms.CheckBox();
            groupBoxSTEMoption2 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxSTEM_AngleResolution = new NumericBox();
            numericBoxSTEM_SliceThicknessForInelastic = new NumericBox();
            groupBoxHREMoption2 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonModeQuasiCoherent = new System.Windows.Forms.RadioButton();
            radioButtonModeTransmissionCrossCoefficient = new System.Windows.Forms.RadioButton();
            panelImageProperties = new System.Windows.Forms.Panel();
            groupBoxImageProperty = new System.Windows.Forms.GroupBox();
            numericBoxResolution = new NumericBox();
            sizeControl1 = new SizeControl();
            panel8 = new System.Windows.Forms.Panel();
            groupBoxDiffractedWaves = new System.Windows.Forms.GroupBox();
            numericBoxNumOfBlochWave = new NumericBox();
            groupBoxOpticalProperty = new System.Windows.Forms.GroupBox();
            groupBoxSTEMoption1 = new System.Windows.Forms.GroupBox();
            contextMenuStripSTEM = new System.Windows.Forms.ContextMenuStrip(components);
            typicalBF02MradToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            typicalABF1224MradToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            typicalLAADF2560MradToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            typicalHAADF80250MradToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            label34 = new System.Windows.Forms.Label();
            flowLayoutPanelConvergenceRadius = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxSTEM_ConvergenceAngle = new NumericBox();
            textBoxConvRadius = new System.Windows.Forms.TextBox();
            label36 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            flowLayoutPanelOuterRadius = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxSTEM_DetectorOuterAngle = new NumericBox();
            textBoxOuterRadius = new System.Windows.Forms.TextBox();
            label38 = new System.Windows.Forms.Label();
            flowLayoutPanelInnerRadius = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxSTEM_DetectorInnerAngle = new NumericBox();
            textBoxInnerRadius = new System.Windows.Forms.TextBox();
            label37 = new System.Windows.Forms.Label();
            numericBoxSTEM_EffectiveSourceSize = new NumericBox();
            groupBoxHREMoption1 = new System.Windows.Forms.GroupBox();
            flowLayoutPanelSpotCount = new System.Windows.Forms.FlowLayoutPanel();
            buttonDetailsOfSpots = new System.Windows.Forms.Button();
            label8 = new System.Windows.Forms.Label();
            flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxHRTEM_ObjAperX = new NumericBox();
            numericBoxHRTEM_ObjAperY = new NumericBox();
            textBoxNumOfSpots = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxObjAperRadius = new NumericBox();
            checkBoxOpenAperture = new System.Windows.Forms.CheckBox();
            flowLayoutPanelObjectiveAperture = new System.Windows.Forms.FlowLayoutPanel();
            textBoxObjAperRadius = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            groupBoxTEMConditions = new System.Windows.Forms.GroupBox();
            contextMenuStripTEMcondition = new System.Windows.Forms.ContextMenuStrip(components);
            setoZeroDefocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            setoScherzerDefocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            setAllAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            presets1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            presets2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            presets3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            presets4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            checkBoxCTF = new System.Windows.Forms.CheckBox();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxCs = new NumericBox();
            numericBoxCc = new NumericBox();
            numericBoxHRTEM_BetaAgnle = new NumericBox();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxDefocus = new NumericBox();
            flowLayoutPanelScherzer = new System.Windows.Forms.FlowLayoutPanel();
            label3 = new System.Windows.Forms.Label();
            textBoxScherzer = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxAccVol = new NumericBox();
            numericBoxDeltaV = new NumericBox();
            flowLayoutPanelModeSelection = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxImageMode = new System.Windows.Forms.GroupBox();
            flowLayoutPanelImageType = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonHRTEM = new System.Windows.Forms.RadioButton();
            radioButtonSTEM = new System.Windows.Forms.RadioButton();
            radioButton1 = new System.Windows.Forms.RadioButton();
            radioButtonProjectedPotential = new System.Windows.Forms.RadioButton();
            groupBoxSampleProperty = new System.Windows.Forms.GroupBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            numericBoxThickness = new NumericBox();
            panelSimulationActions = new System.Windows.Forms.Panel();
            checkBoxPreset = new System.Windows.Forms.CheckBox();
            checkBoxRealTimeSimulation = new System.Windows.Forms.CheckBox();
            buttonSimulate = new System.Windows.Forms.Button();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemSavePNG = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemSaveTIFF = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemSaveMetafile = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemSaveIndividually = new System.Windows.Forms.ToolStripMenuItem();
            copyImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemCopyImage = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemCopyMetafile = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemOverprintSymbols = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            loadTEMParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveTEMParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            detailsOfHRTEMSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            calculationLibraryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripComboBoxCaclulationLibrary = new System.Windows.Forms.ToolStripComboBox();
            toolTip = new System.Windows.Forms.ToolTip(components);
            buttonStop = new System.Windows.Forms.Button();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panelImageStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxScaleOfIntensity).BeginInit();
            panelDisplaySettings.SuspendLayout();
            groupBoxAdjust.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            groupBoxNormalization.SuspendLayout();
            flowLayoutPanelIntensityRange.SuspendLayout();
            groupBoxSTEMoption3.SuspendLayout();
            groupBoxDisplay.SuspendLayout();
            groupBoxSimulation.SuspendLayout();
            panelModeOptions.SuspendLayout();
            groupBoxSerialImage.SuspendLayout();
            panelSerial.SuspendLayout();
            panelSerialDefocus.SuspendLayout();
            panelSerialThickness.SuspendLayout();
            panelSerialSettings.SuspendLayout();
            flowLayoutPanelHorizontalDirection.SuspendLayout();
            flowLayoutPanelSimulationMode.SuspendLayout();
            groupBoxPotentialOption.SuspendLayout();
            flowLayoutPanelPotentialMode.SuspendLayout();
            flowLayoutPanelMagAndPhase.SuspendLayout();
            panelPhaseScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhaseScale).BeginInit();
            flowLayoutPanelRealAndImaiginary.SuspendLayout();
            groupBoxSTEMoption2.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            groupBoxHREMoption2.SuspendLayout();
            flowLayoutPanel10.SuspendLayout();
            panelImageProperties.SuspendLayout();
            groupBoxImageProperty.SuspendLayout();
            groupBoxDiffractedWaves.SuspendLayout();
            groupBoxOpticalProperty.SuspendLayout();
            groupBoxSTEMoption1.SuspendLayout();
            contextMenuStripSTEM.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            flowLayoutPanelConvergenceRadius.SuspendLayout();
            flowLayoutPanelOuterRadius.SuspendLayout();
            flowLayoutPanelInnerRadius.SuspendLayout();
            groupBoxHREMoption1.SuspendLayout();
            flowLayoutPanelSpotCount.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanelObjectiveAperture.SuspendLayout();
            groupBoxTEMConditions.SuspendLayout();
            contextMenuStripTEMcondition.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanelScherzer.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanelModeSelection.SuspendLayout();
            groupBoxImageMode.SuspendLayout();
            flowLayoutPanelImageType.SuspendLayout();
            groupBoxSampleProperty.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panelSimulationActions.SuspendLayout();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel);
            splitContainer1.Panel1.Controls.Add(panelImageStatus);
            splitContainer1.Panel1.Controls.Add(panelDisplaySettings);
            splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBoxSimulation);
            splitContainer1.Panel2.Controls.Add(groupBoxOpticalProperty);
            splitContainer1.Panel2.Controls.Add(flowLayoutPanelModeSelection);
            splitContainer1.Panel2.Controls.Add(panelSimulationActions);
            splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            resources.ApplyResources(splitContainer1.Panel2, "splitContainer1.Panel2");
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(tableLayoutPanel, "tableLayoutPanel");
            tableLayoutPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            tableLayoutPanel.CausesValidation = false;
            tableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.Enter += TableLayoutPanel_Enter;
            tableLayoutPanel.Leave += TableLayoutPanel_Leave;
            // 
            // panelImageStatus
            // 
            panelImageStatus.Controls.Add(pictureBoxScaleOfIntensity);
            panelImageStatus.Controls.Add(labelMousePositionValue);
            panelImageStatus.Controls.Add(label33);
            panelImageStatus.Controls.Add(labelMousePositionY);
            panelImageStatus.Controls.Add(labelMousePositionX);
            panelImageStatus.Controls.Add(label31);
            panelImageStatus.Controls.Add(label27);
            panelImageStatus.Controls.Add(label30);
            panelImageStatus.Controls.Add(label29);
            panelImageStatus.Controls.Add(label26);
            panelImageStatus.Controls.Add(label28);
            resources.ApplyResources(panelImageStatus, "panelImageStatus");
            panelImageStatus.Name = "panelImageStatus";
            // 
            // pictureBoxScaleOfIntensity
            // 
            resources.ApplyResources(pictureBoxScaleOfIntensity, "pictureBoxScaleOfIntensity");
            pictureBoxScaleOfIntensity.Name = "pictureBoxScaleOfIntensity";
            pictureBoxScaleOfIntensity.TabStop = false;
            // 
            // labelMousePositionValue
            // 
            resources.ApplyResources(labelMousePositionValue, "labelMousePositionValue");
            labelMousePositionValue.Name = "labelMousePositionValue";
            // 
            // label33
            // 
            resources.ApplyResources(label33, "label33");
            label33.Name = "label33";
            // 
            // labelMousePositionY
            // 
            resources.ApplyResources(labelMousePositionY, "labelMousePositionY");
            labelMousePositionY.Name = "labelMousePositionY";
            // 
            // labelMousePositionX
            // 
            resources.ApplyResources(labelMousePositionX, "labelMousePositionX");
            labelMousePositionX.Name = "labelMousePositionX";
            // 
            // label31
            // 
            resources.ApplyResources(label31, "label31");
            label31.Name = "label31";
            // 
            // label27
            // 
            resources.ApplyResources(label27, "label27");
            label27.Name = "label27";
            // 
            // label30
            // 
            resources.ApplyResources(label30, "label30");
            label30.Name = "label30";
            // 
            // label29
            // 
            resources.ApplyResources(label29, "label29");
            label29.Name = "label29";
            // 
            // label26
            // 
            resources.ApplyResources(label26, "label26");
            label26.Name = "label26";
            // 
            // label28
            // 
            resources.ApplyResources(label28, "label28");
            label28.Name = "label28";
            // 
            // panelDisplaySettings
            // 
            panelDisplaySettings.Controls.Add(groupBoxAdjust);
            panelDisplaySettings.Controls.Add(panel1);
            panelDisplaySettings.Controls.Add(groupBoxNormalization);
            panelDisplaySettings.Controls.Add(panel9);
            panelDisplaySettings.Controls.Add(groupBoxSTEMoption3);
            panelDisplaySettings.Controls.Add(groupBoxDisplay);
            resources.ApplyResources(panelDisplaySettings, "panelDisplaySettings");
            panelDisplaySettings.Name = "panelDisplaySettings";
            // 
            // groupBoxAdjust
            // 
            captureExtender.SetCapture(groupBoxAdjust, true);
            groupBoxAdjust.Controls.Add(flowLayoutPanel2);
            groupBoxAdjust.Controls.Add(trackBarAdvancedMax);
            groupBoxAdjust.Controls.Add(trackBarAdvancedMin);
            resources.ApplyResources(groupBoxAdjust, "groupBoxAdjust");
            groupBoxAdjust.Name = "groupBoxAdjust";
            groupBoxAdjust.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(label25);
            flowLayoutPanel2.Controls.Add(comboBoxScaleColorScale);
            flowLayoutPanel2.Controls.Add(checkBoxGaussianBlur);
            flowLayoutPanel2.Controls.Add(numericBoxGaussianBlurRadius);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // label25
            // 
            resources.ApplyResources(label25, "label25");
            label25.Name = "label25";
            toolTip.SetToolTip(label25, resources.GetString("label25.ToolTip"));
            // 
            // comboBoxScaleColorScale
            // 
            comboBoxScaleColorScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxScaleColorScale, "comboBoxScaleColorScale");
            comboBoxScaleColorScale.FormattingEnabled = true;
            comboBoxScaleColorScale.Items.AddRange(new object[] { resources.GetString("comboBoxScaleColorScale.Items"), resources.GetString("comboBoxScaleColorScale.Items1") });
            comboBoxScaleColorScale.Name = "comboBoxScaleColorScale";
            toolTip.SetToolTip(comboBoxScaleColorScale, resources.GetString("comboBoxScaleColorScale.ToolTip"));
            comboBoxScaleColorScale.SelectedIndexChanged += ComboBoxScaleColorScale_SelectedIndexChanged;
            // 
            // checkBoxGaussianBlur
            // 
            resources.ApplyResources(checkBoxGaussianBlur, "checkBoxGaussianBlur");
            checkBoxGaussianBlur.Name = "checkBoxGaussianBlur";
            toolTip.SetToolTip(checkBoxGaussianBlur, resources.GetString("checkBoxGaussianBlur.ToolTip"));
            checkBoxGaussianBlur.UseVisualStyleBackColor = true;
            checkBoxGaussianBlur.CheckedChanged += CheckBoxGaussianBlur_CheckedChanged;
            // 
            // numericBoxGaussianBlurRadius
            // 
            numericBoxGaussianBlurRadius.BackColor = System.Drawing.SystemColors.Control;
            numericBoxGaussianBlurRadius.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxGaussianBlurRadius, "numericBoxGaussianBlurRadius");
            numericBoxGaussianBlurRadius.Maximum = 100D;
            numericBoxGaussianBlurRadius.Minimum = 0D;
            numericBoxGaussianBlurRadius.Name = "numericBoxGaussianBlurRadius";
            numericBoxGaussianBlurRadius.RadianValue = 0.3490658503988659D;
            numericBoxGaussianBlurRadius.ShowUpDown = true;
            numericBoxGaussianBlurRadius.SkipEventDuringInput = false;
            numericBoxGaussianBlurRadius.SmartIncrement = true;
            numericBoxGaussianBlurRadius.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxGaussianBlurRadius, resources.GetString("numericBoxGaussianBlurRadius.ToolTip"));
            numericBoxGaussianBlurRadius.Value = 20D;
            numericBoxGaussianBlurRadius.ValueFontSize = 9F;
            numericBoxGaussianBlurRadius.ValueChanged += CheckBoxGaussianBlur_CheckedChanged;
            // 
            // trackBarAdvancedMax
            // 
            resources.ApplyResources(trackBarAdvancedMax, "trackBarAdvancedMax");
            trackBarAdvancedMax.ControlHeight = 25;
            trackBarAdvancedMax.Maximum = 1D;
            trackBarAdvancedMax.Minimum = 0D;
            trackBarAdvancedMax.Name = "trackBarAdvancedMax";
            trackBarAdvancedMax.NumericBoxSize = 95;
            toolTip.SetToolTip(trackBarAdvancedMax, resources.GetString("trackBarAdvancedMax.ToolTip"));
            trackBarAdvancedMax.UpDown_Increment = 0.01D;
            trackBarAdvancedMax.Value = 1D;
            trackBarAdvancedMax.ValueChanged += TrackBarAdvancedMin_ValueChanged;
            // 
            // trackBarAdvancedMin
            // 
            resources.ApplyResources(trackBarAdvancedMin, "trackBarAdvancedMin");
            trackBarAdvancedMin.ControlHeight = 25;
            trackBarAdvancedMin.Maximum = 65535D;
            trackBarAdvancedMin.Minimum = 0D;
            trackBarAdvancedMin.Name = "trackBarAdvancedMin";
            trackBarAdvancedMin.NumericBoxSize = 95;
            toolTip.SetToolTip(trackBarAdvancedMin, resources.GetString("trackBarAdvancedMin.ToolTip"));
            trackBarAdvancedMin.ValueChanged += TrackBarAdvancedMin_ValueChanged;
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // groupBoxNormalization
            // 
            resources.ApplyResources(groupBoxNormalization, "groupBoxNormalization");
            captureExtender.SetCapture(groupBoxNormalization, true);
            groupBoxNormalization.Controls.Add(checkBoxNormarizeIndividually);
            groupBoxNormalization.Controls.Add(flowLayoutPanelIntensityRange);
            groupBoxNormalization.Name = "groupBoxNormalization";
            groupBoxNormalization.TabStop = false;
            // 
            // checkBoxNormarizeIndividually
            // 
            resources.ApplyResources(checkBoxNormarizeIndividually, "checkBoxNormarizeIndividually");
            checkBoxNormarizeIndividually.Checked = true;
            checkBoxNormarizeIndividually.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxNormarizeIndividually.Name = "checkBoxNormarizeIndividually";
            toolTip.SetToolTip(checkBoxNormarizeIndividually, resources.GetString("checkBoxNormarizeIndividually.ToolTip"));
            checkBoxNormarizeIndividually.UseVisualStyleBackColor = true;
            checkBoxNormarizeIndividually.CheckedChanged += checkBoxIntensityMin_CheckedChanged;
            // 
            // flowLayoutPanelIntensityRange
            // 
            resources.ApplyResources(flowLayoutPanelIntensityRange, "flowLayoutPanelIntensityRange");
            flowLayoutPanelIntensityRange.Controls.Add(checkBoxIntensityMin);
            flowLayoutPanelIntensityRange.Controls.Add(numericBoxIntensityMin);
            flowLayoutPanelIntensityRange.Controls.Add(checkBoxIntensityMax);
            flowLayoutPanelIntensityRange.Controls.Add(numericBoxIntensityMax);
            flowLayoutPanelIntensityRange.Name = "flowLayoutPanelIntensityRange";
            // 
            // checkBoxIntensityMin
            // 
            resources.ApplyResources(checkBoxIntensityMin, "checkBoxIntensityMin");
            checkBoxIntensityMin.Checked = true;
            checkBoxIntensityMin.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxIntensityMin.Name = "checkBoxIntensityMin";
            toolTip.SetToolTip(checkBoxIntensityMin, resources.GetString("checkBoxIntensityMin.ToolTip"));
            checkBoxIntensityMin.UseVisualStyleBackColor = true;
            checkBoxIntensityMin.CheckedChanged += checkBoxIntensityMin_CheckedChanged;
            // 
            // numericBoxIntensityMin
            // 
            numericBoxIntensityMin.BackColor = System.Drawing.SystemColors.Control;
            numericBoxIntensityMin.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxIntensityMin, "numericBoxIntensityMin");
            numericBoxIntensityMin.Maximum = 65535D;
            numericBoxIntensityMin.Minimum = 0D;
            numericBoxIntensityMin.Name = "numericBoxIntensityMin";
            numericBoxIntensityMin.ShowUpDown = true;
            numericBoxIntensityMin.SmartIncrement = true;
            numericBoxIntensityMin.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxIntensityMin, resources.GetString("numericBoxIntensityMin.ToolTip"));
            numericBoxIntensityMin.ValueFontSize = 9F;
            numericBoxIntensityMin.ValueChanged += checkBoxIntensityMin_CheckedChanged;
            // 
            // checkBoxIntensityMax
            // 
            resources.ApplyResources(checkBoxIntensityMax, "checkBoxIntensityMax");
            checkBoxIntensityMax.Checked = true;
            checkBoxIntensityMax.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxIntensityMax.Name = "checkBoxIntensityMax";
            toolTip.SetToolTip(checkBoxIntensityMax, resources.GetString("checkBoxIntensityMax.ToolTip"));
            checkBoxIntensityMax.UseVisualStyleBackColor = true;
            checkBoxIntensityMax.CheckedChanged += checkBoxIntensityMin_CheckedChanged;
            // 
            // numericBoxIntensityMax
            // 
            numericBoxIntensityMax.BackColor = System.Drawing.SystemColors.Control;
            numericBoxIntensityMax.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxIntensityMax, "numericBoxIntensityMax");
            numericBoxIntensityMax.Maximum = 65535D;
            numericBoxIntensityMax.Minimum = 1D;
            numericBoxIntensityMax.Name = "numericBoxIntensityMax";
            numericBoxIntensityMax.RadianValue = 0.017453292519943295D;
            numericBoxIntensityMax.ShowUpDown = true;
            numericBoxIntensityMax.SmartIncrement = true;
            numericBoxIntensityMax.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxIntensityMax, resources.GetString("numericBoxIntensityMax.ToolTip"));
            numericBoxIntensityMax.Value = 1D;
            numericBoxIntensityMax.ValueFontSize = 9F;
            numericBoxIntensityMax.ValueChanged += checkBoxIntensityMin_CheckedChanged;
            // 
            // panel9
            // 
            resources.ApplyResources(panel9, "panel9");
            panel9.Name = "panel9";
            // 
            // groupBoxSTEMoption3
            // 
            captureExtender.SetCapture(groupBoxSTEMoption3, true);
            groupBoxSTEMoption3.Controls.Add(radioButtonSTEM_target_TDS);
            groupBoxSTEMoption3.Controls.Add(radioButtonSTEM_target_elas);
            groupBoxSTEMoption3.Controls.Add(radioButtonSTEM_target_both);
            resources.ApplyResources(groupBoxSTEMoption3, "groupBoxSTEMoption3");
            groupBoxSTEMoption3.Name = "groupBoxSTEMoption3";
            groupBoxSTEMoption3.TabStop = false;
            // 
            // radioButtonSTEM_target_TDS
            // 
            resources.ApplyResources(radioButtonSTEM_target_TDS, "radioButtonSTEM_target_TDS");
            radioButtonSTEM_target_TDS.Name = "radioButtonSTEM_target_TDS";
            toolTip.SetToolTip(radioButtonSTEM_target_TDS, resources.GetString("radioButtonSTEM_target_TDS.ToolTip"));
            radioButtonSTEM_target_TDS.UseVisualStyleBackColor = true;
            radioButtonSTEM_target_TDS.CheckedChanged += radioButtonSTEM_target_both_CheckedChanged;
            // 
            // radioButtonSTEM_target_elas
            // 
            resources.ApplyResources(radioButtonSTEM_target_elas, "radioButtonSTEM_target_elas");
            radioButtonSTEM_target_elas.Name = "radioButtonSTEM_target_elas";
            toolTip.SetToolTip(radioButtonSTEM_target_elas, resources.GetString("radioButtonSTEM_target_elas.ToolTip"));
            radioButtonSTEM_target_elas.UseVisualStyleBackColor = true;
            radioButtonSTEM_target_elas.CheckedChanged += radioButtonSTEM_target_both_CheckedChanged;
            // 
            // radioButtonSTEM_target_both
            // 
            resources.ApplyResources(radioButtonSTEM_target_both, "radioButtonSTEM_target_both");
            radioButtonSTEM_target_both.Checked = true;
            radioButtonSTEM_target_both.Name = "radioButtonSTEM_target_both";
            radioButtonSTEM_target_both.TabStop = true;
            toolTip.SetToolTip(radioButtonSTEM_target_both, resources.GetString("radioButtonSTEM_target_both.ToolTip"));
            radioButtonSTEM_target_both.UseVisualStyleBackColor = true;
            radioButtonSTEM_target_both.CheckedChanged += radioButtonSTEM_target_both_CheckedChanged;
            // 
            // groupBoxDisplay
            // 
            captureExtender.SetCapture(groupBoxDisplay, true);
            groupBoxDisplay.Controls.Add(colorControlScale);
            groupBoxDisplay.Controls.Add(numericBoxScaleLength);
            groupBoxDisplay.Controls.Add(checkBoxShowScale);
            groupBoxDisplay.Controls.Add(colorControlLabel);
            groupBoxDisplay.Controls.Add(numericBoxLabelFontSize);
            groupBoxDisplay.Controls.Add(checkBoxShowLabel);
            groupBoxDisplay.Controls.Add(checkBoxShowUnitcell);
            resources.ApplyResources(groupBoxDisplay, "groupBoxDisplay");
            groupBoxDisplay.Name = "groupBoxDisplay";
            groupBoxDisplay.TabStop = false;
            // 
            // colorControlScale
            // 
            resources.ApplyResources(colorControlScale, "colorControlScale");
            colorControlScale.BackColor = System.Drawing.SystemColors.Control;
            colorControlScale.BoxSize = new System.Drawing.Size(20, 20);
            colorControlScale.Color = System.Drawing.Color.FromArgb(135, 205, 250);
            colorControlScale.Name = "colorControlScale";
            toolTip.SetToolTip(colorControlScale, resources.GetString("colorControlScale.ToolTip1"));
            colorControlScale.ColorChanged += CheckBoxShowLabel_CheckedChanged;
            // 
            // numericBoxScaleLength
            // 
            numericBoxScaleLength.BackColor = System.Drawing.SystemColors.Control;
            numericBoxScaleLength.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxScaleLength, "numericBoxScaleLength");
            numericBoxScaleLength.Maximum = 100D;
            numericBoxScaleLength.Minimum = 0.2D;
            numericBoxScaleLength.Name = "numericBoxScaleLength";
            numericBoxScaleLength.RadianValue = 0.0087266462599716477D;
            numericBoxScaleLength.ShowUpDown = true;
            numericBoxScaleLength.SkipEventDuringInput = false;
            numericBoxScaleLength.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxScaleLength, resources.GetString("numericBoxScaleLength.ToolTip"));
            numericBoxScaleLength.UpDown_Increment = 0.2D;
            numericBoxScaleLength.Value = 0.5D;
            numericBoxScaleLength.ValueFontSize = 9F;
            numericBoxScaleLength.ValueChanged += CheckBoxShowLabel_CheckedChanged;
            // 
            // checkBoxShowScale
            // 
            resources.ApplyResources(checkBoxShowScale, "checkBoxShowScale");
            checkBoxShowScale.Checked = true;
            checkBoxShowScale.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowScale.Name = "checkBoxShowScale";
            toolTip.SetToolTip(checkBoxShowScale, resources.GetString("checkBoxShowScale.ToolTip"));
            checkBoxShowScale.UseVisualStyleBackColor = true;
            checkBoxShowScale.CheckedChanged += CheckBoxShowLabel_CheckedChanged;
            // 
            // colorControlLabel
            // 
            resources.ApplyResources(colorControlLabel, "colorControlLabel");
            colorControlLabel.BackColor = System.Drawing.SystemColors.Control;
            colorControlLabel.BoxSize = new System.Drawing.Size(20, 20);
            colorControlLabel.Color = System.Drawing.Color.FromArgb(173, 255, 47);
            colorControlLabel.Name = "colorControlLabel";
            toolTip.SetToolTip(colorControlLabel, resources.GetString("colorControlLabel.ToolTip1"));
            colorControlLabel.ColorChanged += CheckBoxShowLabel_CheckedChanged;
            // 
            // numericBoxLabelFontSize
            // 
            numericBoxLabelFontSize.BackColor = System.Drawing.SystemColors.Control;
            numericBoxLabelFontSize.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxLabelFontSize, "numericBoxLabelFontSize");
            numericBoxLabelFontSize.Maximum = 20D;
            numericBoxLabelFontSize.Minimum = 1D;
            numericBoxLabelFontSize.Name = "numericBoxLabelFontSize";
            numericBoxLabelFontSize.RadianValue = 0.15707963267948966D;
            numericBoxLabelFontSize.ShowUpDown = true;
            numericBoxLabelFontSize.SkipEventDuringInput = false;
            numericBoxLabelFontSize.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxLabelFontSize, resources.GetString("numericBoxLabelFontSize.ToolTip"));
            numericBoxLabelFontSize.Value = 9D;
            numericBoxLabelFontSize.ValueFontSize = 9F;
            numericBoxLabelFontSize.ValueChanged += CheckBoxShowLabel_CheckedChanged;
            // 
            // checkBoxShowLabel
            // 
            resources.ApplyResources(checkBoxShowLabel, "checkBoxShowLabel");
            checkBoxShowLabel.Checked = true;
            checkBoxShowLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowLabel.Name = "checkBoxShowLabel";
            toolTip.SetToolTip(checkBoxShowLabel, resources.GetString("checkBoxShowLabel.ToolTip"));
            checkBoxShowLabel.UseVisualStyleBackColor = true;
            checkBoxShowLabel.CheckedChanged += CheckBoxShowLabel_CheckedChanged;
            // 
            // checkBoxShowUnitcell
            // 
            resources.ApplyResources(checkBoxShowUnitcell, "checkBoxShowUnitcell");
            checkBoxShowUnitcell.Checked = true;
            checkBoxShowUnitcell.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowUnitcell.Name = "checkBoxShowUnitcell";
            toolTip.SetToolTip(checkBoxShowUnitcell, resources.GetString("checkBoxShowUnitcell.ToolTip"));
            checkBoxShowUnitcell.UseVisualStyleBackColor = true;
            checkBoxShowUnitcell.CheckedChanged += CheckBoxShowLabel_CheckedChanged;
            // 
            // groupBoxSimulation
            // 
            resources.ApplyResources(groupBoxSimulation, "groupBoxSimulation");
            captureExtender.SetCapture(groupBoxSimulation, true);
            groupBoxSimulation.Controls.Add(panelModeOptions);
            groupBoxSimulation.Name = "groupBoxSimulation";
            groupBoxSimulation.TabStop = false;
            // 
            // panelModeOptions
            // 
            resources.ApplyResources(panelModeOptions, "panelModeOptions");
            panelModeOptions.Controls.Add(groupBoxSerialImage);
            panelModeOptions.Controls.Add(groupBoxPotentialOption);
            panelModeOptions.Controls.Add(groupBoxSTEMoption2);
            panelModeOptions.Controls.Add(groupBoxHREMoption2);
            panelModeOptions.Controls.Add(panelImageProperties);
            panelModeOptions.Name = "panelModeOptions";
            // 
            // groupBoxSerialImage
            // 
            resources.ApplyResources(groupBoxSerialImage, "groupBoxSerialImage");
            captureExtender.SetCapture(groupBoxSerialImage, true);
            groupBoxSerialImage.Controls.Add(panelSerial);
            groupBoxSerialImage.Controls.Add(flowLayoutPanelSimulationMode);
            groupBoxSerialImage.Name = "groupBoxSerialImage";
            groupBoxSerialImage.TabStop = false;
            // 
            // panelSerial
            // 
            panelSerial.Controls.Add(panelSerialDefocus);
            panelSerial.Controls.Add(panelSerialThickness);
            panelSerial.Controls.Add(panelSerialSettings);
            panelSerial.Controls.Add(flowLayoutPanelHorizontalDirection);
            resources.ApplyResources(panelSerial, "panelSerial");
            panelSerial.Name = "panelSerial";
            // 
            // panelSerialDefocus
            // 
            panelSerialDefocus.Controls.Add(numericBoxDefocusNum);
            panelSerialDefocus.Controls.Add(numericBoxDefocusStep);
            panelSerialDefocus.Controls.Add(numericBoxDefocusStart);
            panelSerialDefocus.Controls.Add(textBoxDefocusList);
            resources.ApplyResources(panelSerialDefocus, "panelSerialDefocus");
            panelSerialDefocus.Name = "panelSerialDefocus";
            // 
            // numericBoxDefocusNum
            // 
            numericBoxDefocusNum.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusNum.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxDefocusNum, "numericBoxDefocusNum");
            numericBoxDefocusNum.Maximum = 20D;
            numericBoxDefocusNum.Minimum = 1D;
            numericBoxDefocusNum.Name = "numericBoxDefocusNum";
            numericBoxDefocusNum.RadianValue = 0.069813170079773182D;
            numericBoxDefocusNum.ShowUpDown = true;
            numericBoxDefocusNum.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxDefocusNum, resources.GetString("numericBoxDefocusNum.ToolTip"));
            numericBoxDefocusNum.Value = 4D;
            numericBoxDefocusNum.ValueFontSize = 9F;
            numericBoxDefocusNum.ValueChanged += NumericBoxDefocusSerial_ValueChanged;
            // 
            // numericBoxDefocusStep
            // 
            numericBoxDefocusStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusStep.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxDefocusStep, "numericBoxDefocusStep");
            numericBoxDefocusStep.Maximum = 100D;
            numericBoxDefocusStep.Minimum = -100D;
            numericBoxDefocusStep.Name = "numericBoxDefocusStep";
            numericBoxDefocusStep.RadianValue = -0.3490658503988659D;
            numericBoxDefocusStep.ShowUpDown = true;
            numericBoxDefocusStep.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxDefocusStep, resources.GetString("numericBoxDefocusStep.ToolTip"));
            numericBoxDefocusStep.UpDown_Increment = 10D;
            numericBoxDefocusStep.Value = -20D;
            numericBoxDefocusStep.ValueFontSize = 9F;
            numericBoxDefocusStep.ValueChanged += NumericBoxDefocusSerial_ValueChanged;
            // 
            // numericBoxDefocusStart
            // 
            numericBoxDefocusStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusStart.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxDefocusStart, "numericBoxDefocusStart");
            numericBoxDefocusStart.Maximum = 1000D;
            numericBoxDefocusStart.Minimum = -1000D;
            numericBoxDefocusStart.Name = "numericBoxDefocusStart";
            numericBoxDefocusStart.RadianValue = -1.2217304763960306D;
            numericBoxDefocusStart.ShowUpDown = true;
            numericBoxDefocusStart.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxDefocusStart, resources.GetString("numericBoxDefocusStart.ToolTip"));
            numericBoxDefocusStart.UpDown_Increment = 10D;
            numericBoxDefocusStart.Value = -70D;
            numericBoxDefocusStart.ValueFontSize = 9F;
            numericBoxDefocusStart.ValueChanged += NumericBoxDefocusSerial_ValueChanged;
            // 
            // textBoxDefocusList
            // 
            resources.ApplyResources(textBoxDefocusList, "textBoxDefocusList");
            textBoxDefocusList.Name = "textBoxDefocusList";
            toolTip.SetToolTip(textBoxDefocusList, resources.GetString("textBoxDefocusList.ToolTip"));
            // 
            // panelSerialThickness
            // 
            panelSerialThickness.Controls.Add(numericBoxThicknessNum);
            panelSerialThickness.Controls.Add(numericBoxThicknessStep);
            panelSerialThickness.Controls.Add(numericBoxThicknessStart);
            panelSerialThickness.Controls.Add(textBoxThicknessList);
            resources.ApplyResources(panelSerialThickness, "panelSerialThickness");
            panelSerialThickness.Name = "panelSerialThickness";
            // 
            // numericBoxThicknessNum
            // 
            numericBoxThicknessNum.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessNum.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxThicknessNum, "numericBoxThicknessNum");
            numericBoxThicknessNum.Maximum = 20D;
            numericBoxThicknessNum.Minimum = 0.1D;
            numericBoxThicknessNum.Name = "numericBoxThicknessNum";
            numericBoxThicknessNum.RadianValue = 0.069813170079773182D;
            numericBoxThicknessNum.ShowUpDown = true;
            numericBoxThicknessNum.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessNum, resources.GetString("numericBoxThicknessNum.ToolTip"));
            numericBoxThicknessNum.Value = 4D;
            numericBoxThicknessNum.ValueFontSize = 9F;
            numericBoxThicknessNum.ValueChanged += NumericBoxThicknessSerial_ValueChanged;
            // 
            // numericBoxThicknessStep
            // 
            numericBoxThicknessStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxThicknessStep, "numericBoxThicknessStep");
            numericBoxThicknessStep.Maximum = 1000D;
            numericBoxThicknessStep.Minimum = 1D;
            numericBoxThicknessStep.Name = "numericBoxThicknessStep";
            numericBoxThicknessStep.RadianValue = 0.3490658503988659D;
            numericBoxThicknessStep.ShowUpDown = true;
            numericBoxThicknessStep.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessStep, resources.GetString("numericBoxThicknessStep.ToolTip"));
            numericBoxThicknessStep.UpDown_Increment = 10D;
            numericBoxThicknessStep.Value = 20D;
            numericBoxThicknessStep.ValueFontSize = 9F;
            numericBoxThicknessStep.ValueChanged += NumericBoxThicknessSerial_ValueChanged;
            // 
            // numericBoxThicknessStart
            // 
            numericBoxThicknessStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxThicknessStart, "numericBoxThicknessStart");
            numericBoxThicknessStart.Maximum = 1000D;
            numericBoxThicknessStart.Minimum = 0.1D;
            numericBoxThicknessStart.Name = "numericBoxThicknessStart";
            numericBoxThicknessStart.RadianValue = 0.3490658503988659D;
            numericBoxThicknessStart.ShowUpDown = true;
            numericBoxThicknessStart.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessStart, resources.GetString("numericBoxThicknessStart.ToolTip"));
            numericBoxThicknessStart.UpDown_Increment = 10D;
            numericBoxThicknessStart.Value = 20D;
            numericBoxThicknessStart.ValueFontSize = 9F;
            numericBoxThicknessStart.ValueChanged += NumericBoxThicknessSerial_ValueChanged;
            // 
            // textBoxThicknessList
            // 
            resources.ApplyResources(textBoxThicknessList, "textBoxThicknessList");
            textBoxThicknessList.Name = "textBoxThicknessList";
            toolTip.SetToolTip(textBoxThicknessList, resources.GetString("textBoxThicknessList.ToolTip"));
            // 
            // panelSerialSettings
            // 
            resources.ApplyResources(panelSerialSettings, "panelSerialSettings");
            panelSerialSettings.Controls.Add(checkBoxSerialThickness);
            panelSerialSettings.Controls.Add(checkBoxSerialDefocus);
            panelSerialSettings.Name = "panelSerialSettings";
            // 
            // checkBoxSerialThickness
            // 
            resources.ApplyResources(checkBoxSerialThickness, "checkBoxSerialThickness");
            checkBoxSerialThickness.Checked = true;
            checkBoxSerialThickness.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxSerialThickness.Name = "checkBoxSerialThickness";
            toolTip.SetToolTip(checkBoxSerialThickness, resources.GetString("checkBoxSerialThickness.ToolTip"));
            checkBoxSerialThickness.UseVisualStyleBackColor = true;
            checkBoxSerialThickness.CheckedChanged += CheckBoxSerialDefocus_CheckedChanged;
            // 
            // checkBoxSerialDefocus
            // 
            resources.ApplyResources(checkBoxSerialDefocus, "checkBoxSerialDefocus");
            checkBoxSerialDefocus.Checked = true;
            checkBoxSerialDefocus.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxSerialDefocus.Name = "checkBoxSerialDefocus";
            toolTip.SetToolTip(checkBoxSerialDefocus, resources.GetString("checkBoxSerialDefocus.ToolTip"));
            checkBoxSerialDefocus.UseVisualStyleBackColor = true;
            checkBoxSerialDefocus.CheckedChanged += CheckBoxSerialDefocus_CheckedChanged;
            // 
            // flowLayoutPanelHorizontalDirection
            // 
            resources.ApplyResources(flowLayoutPanelHorizontalDirection, "flowLayoutPanelHorizontalDirection");
            flowLayoutPanelHorizontalDirection.Controls.Add(label6);
            flowLayoutPanelHorizontalDirection.Controls.Add(radioButtonHorizontalDefocus);
            flowLayoutPanelHorizontalDirection.Controls.Add(radioButtonHorizontalThickness);
            flowLayoutPanelHorizontalDirection.Name = "flowLayoutPanelHorizontalDirection";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.ForeColor = System.Drawing.Color.Black;
            label6.Name = "label6";
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // radioButtonHorizontalDefocus
            // 
            resources.ApplyResources(radioButtonHorizontalDefocus, "radioButtonHorizontalDefocus");
            radioButtonHorizontalDefocus.Checked = true;
            radioButtonHorizontalDefocus.Name = "radioButtonHorizontalDefocus";
            radioButtonHorizontalDefocus.TabStop = true;
            toolTip.SetToolTip(radioButtonHorizontalDefocus, resources.GetString("radioButtonHorizontalDefocus.ToolTip"));
            radioButtonHorizontalDefocus.UseVisualStyleBackColor = true;
            // 
            // radioButtonHorizontalThickness
            // 
            resources.ApplyResources(radioButtonHorizontalThickness, "radioButtonHorizontalThickness");
            radioButtonHorizontalThickness.Name = "radioButtonHorizontalThickness";
            toolTip.SetToolTip(radioButtonHorizontalThickness, resources.GetString("radioButtonHorizontalThickness.ToolTip"));
            radioButtonHorizontalThickness.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelSimulationMode
            // 
            resources.ApplyResources(flowLayoutPanelSimulationMode, "flowLayoutPanelSimulationMode");
            flowLayoutPanelSimulationMode.Controls.Add(radioButtonSingleMode);
            flowLayoutPanelSimulationMode.Controls.Add(radioButtonSerialMode);
            flowLayoutPanelSimulationMode.Name = "flowLayoutPanelSimulationMode";
            // 
            // radioButtonSingleMode
            // 
            resources.ApplyResources(radioButtonSingleMode, "radioButtonSingleMode");
            radioButtonSingleMode.Checked = true;
            radioButtonSingleMode.Name = "radioButtonSingleMode";
            radioButtonSingleMode.TabStop = true;
            toolTip.SetToolTip(radioButtonSingleMode, resources.GetString("radioButtonSingleMode.ToolTip"));
            radioButtonSingleMode.UseVisualStyleBackColor = true;
            radioButtonSingleMode.CheckedChanged += CheckBoxSerialDefocus_CheckedChanged;
            // 
            // radioButtonSerialMode
            // 
            resources.ApplyResources(radioButtonSerialMode, "radioButtonSerialMode");
            radioButtonSerialMode.Name = "radioButtonSerialMode";
            toolTip.SetToolTip(radioButtonSerialMode, resources.GetString("radioButtonSerialMode.ToolTip"));
            radioButtonSerialMode.UseVisualStyleBackColor = true;
            // 
            // groupBoxPotentialOption
            // 
            resources.ApplyResources(groupBoxPotentialOption, "groupBoxPotentialOption");
            captureExtender.SetCapture(groupBoxPotentialOption, true);
            groupBoxPotentialOption.Controls.Add(flowLayoutPanelPotentialMode);
            groupBoxPotentialOption.Controls.Add(checkBoxPotentialUgPrime);
            groupBoxPotentialOption.Controls.Add(checkBoxPotentialUg);
            groupBoxPotentialOption.Name = "groupBoxPotentialOption";
            groupBoxPotentialOption.TabStop = false;
            // 
            // flowLayoutPanelPotentialMode
            // 
            resources.ApplyResources(flowLayoutPanelPotentialMode, "flowLayoutPanelPotentialMode");
            flowLayoutPanelPotentialMode.Controls.Add(radioButtonPotentialModeMagAndPhase);
            flowLayoutPanelPotentialMode.Controls.Add(flowLayoutPanelMagAndPhase);
            flowLayoutPanelPotentialMode.Controls.Add(panelPhaseScale);
            flowLayoutPanelPotentialMode.Controls.Add(radioButtonPotentialModeRealAndImag);
            flowLayoutPanelPotentialMode.Controls.Add(flowLayoutPanelRealAndImaiginary);
            flowLayoutPanelPotentialMode.Name = "flowLayoutPanelPotentialMode";
            // 
            // radioButtonPotentialModeMagAndPhase
            // 
            resources.ApplyResources(radioButtonPotentialModeMagAndPhase, "radioButtonPotentialModeMagAndPhase");
            radioButtonPotentialModeMagAndPhase.Checked = true;
            radioButtonPotentialModeMagAndPhase.Name = "radioButtonPotentialModeMagAndPhase";
            radioButtonPotentialModeMagAndPhase.TabStop = true;
            toolTip.SetToolTip(radioButtonPotentialModeMagAndPhase, resources.GetString("radioButtonPotentialModeMagAndPhase.ToolTip"));
            radioButtonPotentialModeMagAndPhase.UseVisualStyleBackColor = true;
            radioButtonPotentialModeMagAndPhase.CheckedChanged += RadioButtonPotentialAsMagnitudeAndPhase_CheckedChanged;
            // 
            // flowLayoutPanelMagAndPhase
            // 
            resources.ApplyResources(flowLayoutPanelMagAndPhase, "flowLayoutPanelMagAndPhase");
            flowLayoutPanelMagAndPhase.Controls.Add(radioButtonPotentialShowMagAndPhase);
            flowLayoutPanelMagAndPhase.Controls.Add(radioButtonPotentialShowMag);
            flowLayoutPanelMagAndPhase.Controls.Add(radioButtonPotentialShowPhase);
            flowLayoutPanelMagAndPhase.Name = "flowLayoutPanelMagAndPhase";
            // 
            // radioButtonPotentialShowMagAndPhase
            // 
            resources.ApplyResources(radioButtonPotentialShowMagAndPhase, "radioButtonPotentialShowMagAndPhase");
            radioButtonPotentialShowMagAndPhase.Checked = true;
            radioButtonPotentialShowMagAndPhase.Name = "radioButtonPotentialShowMagAndPhase";
            radioButtonPotentialShowMagAndPhase.TabStop = true;
            toolTip.SetToolTip(radioButtonPotentialShowMagAndPhase, resources.GetString("radioButtonPotentialShowMagAndPhase.ToolTip"));
            radioButtonPotentialShowMagAndPhase.UseVisualStyleBackColor = true;
            // 
            // radioButtonPotentialShowMag
            // 
            resources.ApplyResources(radioButtonPotentialShowMag, "radioButtonPotentialShowMag");
            radioButtonPotentialShowMag.Name = "radioButtonPotentialShowMag";
            toolTip.SetToolTip(radioButtonPotentialShowMag, resources.GetString("radioButtonPotentialShowMag.ToolTip"));
            radioButtonPotentialShowMag.UseVisualStyleBackColor = true;
            // 
            // radioButtonPotentialShowPhase
            // 
            resources.ApplyResources(radioButtonPotentialShowPhase, "radioButtonPotentialShowPhase");
            radioButtonPotentialShowPhase.ForeColor = System.Drawing.SystemColors.ControlText;
            radioButtonPotentialShowPhase.Name = "radioButtonPotentialShowPhase";
            toolTip.SetToolTip(radioButtonPotentialShowPhase, resources.GetString("radioButtonPotentialShowPhase.ToolTip"));
            radioButtonPotentialShowPhase.UseVisualStyleBackColor = true;
            radioButtonPotentialShowPhase.CheckedChanged += radioButtonPotentialShowPhase_CheckedChanged;
            // 
            // panelPhaseScale
            // 
            resources.ApplyResources(panelPhaseScale, "panelPhaseScale");
            panelPhaseScale.Controls.Add(label24);
            panelPhaseScale.Controls.Add(label23);
            panelPhaseScale.Controls.Add(label22);
            panelPhaseScale.Controls.Add(label21);
            panelPhaseScale.Controls.Add(label20);
            panelPhaseScale.Controls.Add(label19);
            panelPhaseScale.Controls.Add(label18);
            panelPhaseScale.Controls.Add(pictureBoxPhaseScale);
            panelPhaseScale.Controls.Add(label17);
            panelPhaseScale.Controls.Add(label16);
            panelPhaseScale.Controls.Add(label15);
            panelPhaseScale.Controls.Add(label14);
            panelPhaseScale.Controls.Add(label11);
            panelPhaseScale.Controls.Add(label12);
            panelPhaseScale.Controls.Add(label13);
            panelPhaseScale.Controls.Add(label10);
            panelPhaseScale.Name = "panelPhaseScale";
            // 
            // label24
            // 
            resources.ApplyResources(label24, "label24");
            label24.Name = "label24";
            // 
            // label23
            // 
            resources.ApplyResources(label23, "label23");
            label23.Name = "label23";
            // 
            // label22
            // 
            resources.ApplyResources(label22, "label22");
            label22.Name = "label22";
            // 
            // label21
            // 
            resources.ApplyResources(label21, "label21");
            label21.Name = "label21";
            // 
            // label20
            // 
            resources.ApplyResources(label20, "label20");
            label20.Name = "label20";
            // 
            // label19
            // 
            resources.ApplyResources(label19, "label19");
            label19.Name = "label19";
            // 
            // label18
            // 
            resources.ApplyResources(label18, "label18");
            label18.Name = "label18";
            // 
            // pictureBoxPhaseScale
            // 
            resources.ApplyResources(pictureBoxPhaseScale, "pictureBoxPhaseScale");
            pictureBoxPhaseScale.Name = "pictureBoxPhaseScale";
            pictureBoxPhaseScale.TabStop = false;
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
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            label14.Name = "label14";
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            // 
            // radioButtonPotentialModeRealAndImag
            // 
            resources.ApplyResources(radioButtonPotentialModeRealAndImag, "radioButtonPotentialModeRealAndImag");
            radioButtonPotentialModeRealAndImag.Name = "radioButtonPotentialModeRealAndImag";
            toolTip.SetToolTip(radioButtonPotentialModeRealAndImag, resources.GetString("radioButtonPotentialModeRealAndImag.ToolTip"));
            radioButtonPotentialModeRealAndImag.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelRealAndImaiginary
            // 
            resources.ApplyResources(flowLayoutPanelRealAndImaiginary, "flowLayoutPanelRealAndImaiginary");
            flowLayoutPanelRealAndImaiginary.Controls.Add(radioButtonPotentialShowRealAndImag);
            flowLayoutPanelRealAndImaiginary.Controls.Add(radioButtonPotentialShowReal);
            flowLayoutPanelRealAndImaiginary.Controls.Add(radioButtonPotentialShowImag);
            flowLayoutPanelRealAndImaiginary.Name = "flowLayoutPanelRealAndImaiginary";
            // 
            // radioButtonPotentialShowRealAndImag
            // 
            resources.ApplyResources(radioButtonPotentialShowRealAndImag, "radioButtonPotentialShowRealAndImag");
            radioButtonPotentialShowRealAndImag.Checked = true;
            radioButtonPotentialShowRealAndImag.Name = "radioButtonPotentialShowRealAndImag";
            radioButtonPotentialShowRealAndImag.TabStop = true;
            toolTip.SetToolTip(radioButtonPotentialShowRealAndImag, resources.GetString("radioButtonPotentialShowRealAndImag.ToolTip"));
            radioButtonPotentialShowRealAndImag.UseVisualStyleBackColor = true;
            // 
            // radioButtonPotentialShowReal
            // 
            resources.ApplyResources(radioButtonPotentialShowReal, "radioButtonPotentialShowReal");
            radioButtonPotentialShowReal.Name = "radioButtonPotentialShowReal";
            toolTip.SetToolTip(radioButtonPotentialShowReal, resources.GetString("radioButtonPotentialShowReal.ToolTip"));
            radioButtonPotentialShowReal.UseVisualStyleBackColor = true;
            // 
            // radioButtonPotentialShowImag
            // 
            resources.ApplyResources(radioButtonPotentialShowImag, "radioButtonPotentialShowImag");
            radioButtonPotentialShowImag.Name = "radioButtonPotentialShowImag";
            toolTip.SetToolTip(radioButtonPotentialShowImag, resources.GetString("radioButtonPotentialShowImag.ToolTip"));
            radioButtonPotentialShowImag.UseVisualStyleBackColor = true;
            // 
            // checkBoxPotentialUgPrime
            // 
            resources.ApplyResources(checkBoxPotentialUgPrime, "checkBoxPotentialUgPrime");
            checkBoxPotentialUgPrime.Checked = true;
            checkBoxPotentialUgPrime.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxPotentialUgPrime.Name = "checkBoxPotentialUgPrime";
            toolTip.SetToolTip(checkBoxPotentialUgPrime, resources.GetString("checkBoxPotentialUgPrime.ToolTip"));
            checkBoxPotentialUgPrime.UseVisualStyleBackColor = true;
            // 
            // checkBoxPotentialUg
            // 
            resources.ApplyResources(checkBoxPotentialUg, "checkBoxPotentialUg");
            checkBoxPotentialUg.Checked = true;
            checkBoxPotentialUg.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxPotentialUg.Name = "checkBoxPotentialUg";
            toolTip.SetToolTip(checkBoxPotentialUg, resources.GetString("checkBoxPotentialUg.ToolTip"));
            checkBoxPotentialUg.UseVisualStyleBackColor = true;
            // 
            // groupBoxSTEMoption2
            // 
            resources.ApplyResources(groupBoxSTEMoption2, "groupBoxSTEMoption2");
            captureExtender.SetCapture(groupBoxSTEMoption2, true);
            groupBoxSTEMoption2.Controls.Add(flowLayoutPanel11);
            groupBoxSTEMoption2.Name = "groupBoxSTEMoption2";
            groupBoxSTEMoption2.TabStop = false;
            // 
            // flowLayoutPanel11
            // 
            resources.ApplyResources(flowLayoutPanel11, "flowLayoutPanel11");
            flowLayoutPanel11.Controls.Add(numericBoxSTEM_AngleResolution);
            flowLayoutPanel11.Controls.Add(numericBoxSTEM_SliceThicknessForInelastic);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            // 
            // numericBoxSTEM_AngleResolution
            // 
            numericBoxSTEM_AngleResolution.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_AngleResolution.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxSTEM_AngleResolution, "numericBoxSTEM_AngleResolution");
            numericBoxSTEM_AngleResolution.Maximum = 1D;
            numericBoxSTEM_AngleResolution.Minimum = 0.001D;
            numericBoxSTEM_AngleResolution.Name = "numericBoxSTEM_AngleResolution";
            numericBoxSTEM_AngleResolution.RadianValue = 0.0069813170079773184D;
            numericBoxSTEM_AngleResolution.ShowUpDown = true;
            numericBoxSTEM_AngleResolution.SmartIncrement = true;
            numericBoxSTEM_AngleResolution.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxSTEM_AngleResolution, resources.GetString("numericBoxSTEM_AngleResolution.ToolTip"));
            numericBoxSTEM_AngleResolution.Value = 0.4D;
            numericBoxSTEM_AngleResolution.ValueBoxWidth = 36;
            numericBoxSTEM_AngleResolution.ValueFontSize = 9F;
            // 
            // numericBoxSTEM_SliceThicknessForInelastic
            // 
            numericBoxSTEM_SliceThicknessForInelastic.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_SliceThicknessForInelastic.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxSTEM_SliceThicknessForInelastic, "numericBoxSTEM_SliceThicknessForInelastic");
            numericBoxSTEM_SliceThicknessForInelastic.Maximum = 10D;
            numericBoxSTEM_SliceThicknessForInelastic.Minimum = 0.1D;
            numericBoxSTEM_SliceThicknessForInelastic.Name = "numericBoxSTEM_SliceThicknessForInelastic";
            numericBoxSTEM_SliceThicknessForInelastic.RadianValue = 0.017453292519943295D;
            numericBoxSTEM_SliceThicknessForInelastic.ShowUpDown = true;
            numericBoxSTEM_SliceThicknessForInelastic.SmartIncrement = true;
            numericBoxSTEM_SliceThicknessForInelastic.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxSTEM_SliceThicknessForInelastic, resources.GetString("numericBoxSTEM_SliceThicknessForInelastic.ToolTip"));
            numericBoxSTEM_SliceThicknessForInelastic.Value = 1D;
            numericBoxSTEM_SliceThicknessForInelastic.ValueBoxWidth = 36;
            numericBoxSTEM_SliceThicknessForInelastic.ValueFontSize = 9F;
            // 
            // groupBoxHREMoption2
            // 
            resources.ApplyResources(groupBoxHREMoption2, "groupBoxHREMoption2");
            captureExtender.SetCapture(groupBoxHREMoption2, true);
            groupBoxHREMoption2.Controls.Add(flowLayoutPanel10);
            groupBoxHREMoption2.Name = "groupBoxHREMoption2";
            groupBoxHREMoption2.TabStop = false;
            // 
            // flowLayoutPanel10
            // 
            resources.ApplyResources(flowLayoutPanel10, "flowLayoutPanel10");
            flowLayoutPanel10.Controls.Add(radioButtonModeQuasiCoherent);
            flowLayoutPanel10.Controls.Add(radioButtonModeTransmissionCrossCoefficient);
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            // 
            // radioButtonModeQuasiCoherent
            // 
            resources.ApplyResources(radioButtonModeQuasiCoherent, "radioButtonModeQuasiCoherent");
            radioButtonModeQuasiCoherent.Checked = true;
            radioButtonModeQuasiCoherent.Name = "radioButtonModeQuasiCoherent";
            radioButtonModeQuasiCoherent.TabStop = true;
            toolTip.SetToolTip(radioButtonModeQuasiCoherent, resources.GetString("radioButtonModeQuasiCoherent.ToolTip"));
            radioButtonModeQuasiCoherent.UseVisualStyleBackColor = true;
            // 
            // radioButtonModeTransmissionCrossCoefficient
            // 
            resources.ApplyResources(radioButtonModeTransmissionCrossCoefficient, "radioButtonModeTransmissionCrossCoefficient");
            radioButtonModeTransmissionCrossCoefficient.Name = "radioButtonModeTransmissionCrossCoefficient";
            toolTip.SetToolTip(radioButtonModeTransmissionCrossCoefficient, resources.GetString("radioButtonModeTransmissionCrossCoefficient.ToolTip"));
            radioButtonModeTransmissionCrossCoefficient.UseVisualStyleBackColor = true;
            // 
            // panelImageProperties
            // 
            resources.ApplyResources(panelImageProperties, "panelImageProperties");
            panelImageProperties.Controls.Add(groupBoxImageProperty);
            panelImageProperties.Controls.Add(panel8);
            panelImageProperties.Controls.Add(groupBoxDiffractedWaves);
            panelImageProperties.Name = "panelImageProperties";
            // 
            // groupBoxImageProperty
            // 
            resources.ApplyResources(groupBoxImageProperty, "groupBoxImageProperty");
            captureExtender.SetCapture(groupBoxImageProperty, true);
            groupBoxImageProperty.Controls.Add(numericBoxResolution);
            groupBoxImageProperty.Controls.Add(sizeControl1);
            groupBoxImageProperty.Name = "groupBoxImageProperty";
            groupBoxImageProperty.TabStop = false;
            // 
            // numericBoxResolution
            // 
            numericBoxResolution.BackColor = System.Drawing.SystemColors.Control;
            numericBoxResolution.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxResolution, "numericBoxResolution");
            numericBoxResolution.Maximum = 100D;
            numericBoxResolution.Minimum = 0.01D;
            numericBoxResolution.Name = "numericBoxResolution";
            numericBoxResolution.RadianValue = 0.034906585039886591D;
            numericBoxResolution.ShowUpDown = true;
            numericBoxResolution.SmartIncrement = true;
            numericBoxResolution.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxResolution, resources.GetString("numericBoxResolution.ToolTip"));
            numericBoxResolution.Value = 2D;
            numericBoxResolution.ValueFontSize = 9F;
            // 
            // sizeControl1
            // 
            resources.ApplyResources(sizeControl1, "sizeControl1");
            sizeControl1.Maximum = 2048;
            sizeControl1.Minimum = 8;
            sizeControl1.Name = "sizeControl1";
            sizeControl1.Value = new System.Drawing.Size(512, 512);
            sizeControl1.ValueFontSize = 9F;
            // 
            // panel8
            // 
            resources.ApplyResources(panel8, "panel8");
            panel8.Name = "panel8";
            // 
            // groupBoxDiffractedWaves
            // 
            resources.ApplyResources(groupBoxDiffractedWaves, "groupBoxDiffractedWaves");
            captureExtender.SetCapture(groupBoxDiffractedWaves, true);
            groupBoxDiffractedWaves.Controls.Add(numericBoxNumOfBlochWave);
            groupBoxDiffractedWaves.Name = "groupBoxDiffractedWaves";
            groupBoxDiffractedWaves.TabStop = false;
            // 
            // numericBoxNumOfBlochWave
            // 
            numericBoxNumOfBlochWave.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxNumOfBlochWave, "numericBoxNumOfBlochWave");
            numericBoxNumOfBlochWave.Maximum = 1024D;
            numericBoxNumOfBlochWave.Minimum = 2D;
            numericBoxNumOfBlochWave.Name = "numericBoxNumOfBlochWave";
            numericBoxNumOfBlochWave.RadianValue = 1.3962634015954636D;
            numericBoxNumOfBlochWave.ShowUpDown = true;
            numericBoxNumOfBlochWave.SmartIncrement = true;
            numericBoxNumOfBlochWave.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxNumOfBlochWave, resources.GetString("numericBoxNumOfBlochWave.ToolTip"));
            numericBoxNumOfBlochWave.Value = 80D;
            numericBoxNumOfBlochWave.ValueBoxWidth = 36;
            numericBoxNumOfBlochWave.ValueFontSize = 9F;
            numericBoxNumOfBlochWave.ValueChanged += NumericBoxNumOfBlochWave_ValueChanged;
            // 
            // groupBoxOpticalProperty
            // 
            resources.ApplyResources(groupBoxOpticalProperty, "groupBoxOpticalProperty");
            groupBoxOpticalProperty.Controls.Add(groupBoxSTEMoption1);
            groupBoxOpticalProperty.Controls.Add(groupBoxHREMoption1);
            groupBoxOpticalProperty.Controls.Add(groupBoxTEMConditions);
            groupBoxOpticalProperty.Name = "groupBoxOpticalProperty";
            groupBoxOpticalProperty.TabStop = false;
            // 
            // groupBoxSTEMoption1
            // 
            resources.ApplyResources(groupBoxSTEMoption1, "groupBoxSTEMoption1");
            captureExtender.SetCapture(groupBoxSTEMoption1, true);
            groupBoxSTEMoption1.ContextMenuStrip = contextMenuStripSTEM;
            groupBoxSTEMoption1.Controls.Add(flowLayoutPanel8);
            groupBoxSTEMoption1.Name = "groupBoxSTEMoption1";
            groupBoxSTEMoption1.TabStop = false;
            // 
            // contextMenuStripSTEM
            // 
            contextMenuStripSTEM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { typicalBF02MradToolStripMenuItem, typicalABF1224MradToolStripMenuItem, typicalLAADF2560MradToolStripMenuItem, typicalHAADF80250MradToolStripMenuItem });
            contextMenuStripSTEM.Name = "contextMenuStripSTEM";
            resources.ApplyResources(contextMenuStripSTEM, "contextMenuStripSTEM");
            // 
            // typicalBF02MradToolStripMenuItem
            // 
            typicalBF02MradToolStripMenuItem.Name = "typicalBF02MradToolStripMenuItem";
            resources.ApplyResources(typicalBF02MradToolStripMenuItem, "typicalBF02MradToolStripMenuItem");
            typicalBF02MradToolStripMenuItem.Click += typicalBF02MradToolStripMenuItem_Click;
            // 
            // typicalABF1224MradToolStripMenuItem
            // 
            typicalABF1224MradToolStripMenuItem.Name = "typicalABF1224MradToolStripMenuItem";
            resources.ApplyResources(typicalABF1224MradToolStripMenuItem, "typicalABF1224MradToolStripMenuItem");
            typicalABF1224MradToolStripMenuItem.Click += typicalABF1224MradToolStripMenuItem_Click;
            // 
            // typicalLAADF2560MradToolStripMenuItem
            // 
            typicalLAADF2560MradToolStripMenuItem.Name = "typicalLAADF2560MradToolStripMenuItem";
            resources.ApplyResources(typicalLAADF2560MradToolStripMenuItem, "typicalLAADF2560MradToolStripMenuItem");
            typicalLAADF2560MradToolStripMenuItem.Click += typicalLAADF2560MradToolStripMenuItem_Click;
            // 
            // typicalHAADF80250MradToolStripMenuItem
            // 
            typicalHAADF80250MradToolStripMenuItem.Name = "typicalHAADF80250MradToolStripMenuItem";
            resources.ApplyResources(typicalHAADF80250MradToolStripMenuItem, "typicalHAADF80250MradToolStripMenuItem");
            typicalHAADF80250MradToolStripMenuItem.Click += typicalHAADF80250MradToolStripMenuItem_Click;
            // 
            // flowLayoutPanel8
            // 
            resources.ApplyResources(flowLayoutPanel8, "flowLayoutPanel8");
            flowLayoutPanel8.Controls.Add(label34);
            flowLayoutPanel8.Controls.Add(flowLayoutPanelConvergenceRadius);
            flowLayoutPanel8.Controls.Add(label1);
            flowLayoutPanel8.Controls.Add(flowLayoutPanelOuterRadius);
            flowLayoutPanel8.Controls.Add(flowLayoutPanelInnerRadius);
            flowLayoutPanel8.Controls.Add(numericBoxSTEM_EffectiveSourceSize);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            // 
            // label34
            // 
            resources.ApplyResources(label34, "label34");
            label34.ForeColor = System.Drawing.SystemColors.ControlText;
            label34.Name = "label34";
            toolTip.SetToolTip(label34, resources.GetString("label34.ToolTip"));
            // 
            // flowLayoutPanelConvergenceRadius
            // 
            resources.ApplyResources(flowLayoutPanelConvergenceRadius, "flowLayoutPanelConvergenceRadius");
            flowLayoutPanelConvergenceRadius.Controls.Add(numericBoxSTEM_ConvergenceAngle);
            flowLayoutPanelConvergenceRadius.Controls.Add(textBoxConvRadius);
            flowLayoutPanelConvergenceRadius.Controls.Add(label36);
            flowLayoutPanelConvergenceRadius.Name = "flowLayoutPanelConvergenceRadius";
            // 
            // numericBoxSTEM_ConvergenceAngle
            // 
            numericBoxSTEM_ConvergenceAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_ConvergenceAngle.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxSTEM_ConvergenceAngle, "numericBoxSTEM_ConvergenceAngle");
            numericBoxSTEM_ConvergenceAngle.Maximum = 1570D;
            numericBoxSTEM_ConvergenceAngle.Minimum = 0.1D;
            numericBoxSTEM_ConvergenceAngle.Name = "numericBoxSTEM_ConvergenceAngle";
            numericBoxSTEM_ConvergenceAngle.RadianValue = 0.43633231299858238D;
            numericBoxSTEM_ConvergenceAngle.ShowUpDown = true;
            numericBoxSTEM_ConvergenceAngle.SmartIncrement = true;
            numericBoxSTEM_ConvergenceAngle.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxSTEM_ConvergenceAngle, resources.GetString("numericBoxSTEM_ConvergenceAngle.ToolTip"));
            numericBoxSTEM_ConvergenceAngle.UpDown_Increment = 0.5D;
            numericBoxSTEM_ConvergenceAngle.Value = 25D;
            numericBoxSTEM_ConvergenceAngle.ValueBoxWidth = 50;
            numericBoxSTEM_ConvergenceAngle.ValueFontSize = 9F;
            numericBoxSTEM_ConvergenceAngle.ValueChanged += numericBoxSTEM_ConvergenceAngle_ValueChanged;
            // 
            // textBoxConvRadius
            // 
            textBoxConvRadius.BackColor = System.Drawing.SystemColors.InactiveCaption;
            resources.ApplyResources(textBoxConvRadius, "textBoxConvRadius");
            textBoxConvRadius.ForeColor = System.Drawing.Color.DimGray;
            textBoxConvRadius.Name = "textBoxConvRadius";
            textBoxConvRadius.ReadOnly = true;
            toolTip.SetToolTip(textBoxConvRadius, resources.GetString("textBoxConvRadius.ToolTip"));
            // 
            // label36
            // 
            resources.ApplyResources(label36, "label36");
            label36.ForeColor = System.Drawing.Color.Black;
            label36.Name = "label36";
            toolTip.SetToolTip(label36, resources.GetString("label36.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.ForeColor = System.Drawing.SystemColors.ControlText;
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // flowLayoutPanelOuterRadius
            // 
            resources.ApplyResources(flowLayoutPanelOuterRadius, "flowLayoutPanelOuterRadius");
            flowLayoutPanelOuterRadius.Controls.Add(numericBoxSTEM_DetectorOuterAngle);
            flowLayoutPanelOuterRadius.Controls.Add(textBoxOuterRadius);
            flowLayoutPanelOuterRadius.Controls.Add(label38);
            flowLayoutPanelOuterRadius.Name = "flowLayoutPanelOuterRadius";
            // 
            // numericBoxSTEM_DetectorOuterAngle
            // 
            numericBoxSTEM_DetectorOuterAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_DetectorOuterAngle.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxSTEM_DetectorOuterAngle, "numericBoxSTEM_DetectorOuterAngle");
            numericBoxSTEM_DetectorOuterAngle.Maximum = 1570D;
            numericBoxSTEM_DetectorOuterAngle.Minimum = 0.5D;
            numericBoxSTEM_DetectorOuterAngle.Name = "numericBoxSTEM_DetectorOuterAngle";
            numericBoxSTEM_DetectorOuterAngle.RadianValue = 0.3490658503988659D;
            numericBoxSTEM_DetectorOuterAngle.ShowUpDown = true;
            numericBoxSTEM_DetectorOuterAngle.SmartIncrement = true;
            numericBoxSTEM_DetectorOuterAngle.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxSTEM_DetectorOuterAngle, resources.GetString("numericBoxSTEM_DetectorOuterAngle.ToolTip"));
            numericBoxSTEM_DetectorOuterAngle.UpDown_Increment = 0.5D;
            numericBoxSTEM_DetectorOuterAngle.Value = 20D;
            numericBoxSTEM_DetectorOuterAngle.ValueBoxWidth = 50;
            numericBoxSTEM_DetectorOuterAngle.ValueFontSize = 9F;
            numericBoxSTEM_DetectorOuterAngle.ValueChanged += numericBoxSTEM_ConvergenceAngle_ValueChanged;
            // 
            // textBoxOuterRadius
            // 
            textBoxOuterRadius.BackColor = System.Drawing.SystemColors.InactiveCaption;
            resources.ApplyResources(textBoxOuterRadius, "textBoxOuterRadius");
            textBoxOuterRadius.ForeColor = System.Drawing.Color.DimGray;
            textBoxOuterRadius.Name = "textBoxOuterRadius";
            textBoxOuterRadius.ReadOnly = true;
            toolTip.SetToolTip(textBoxOuterRadius, resources.GetString("textBoxOuterRadius.ToolTip"));
            // 
            // label38
            // 
            resources.ApplyResources(label38, "label38");
            label38.ForeColor = System.Drawing.Color.Black;
            label38.Name = "label38";
            toolTip.SetToolTip(label38, resources.GetString("label38.ToolTip"));
            // 
            // flowLayoutPanelInnerRadius
            // 
            resources.ApplyResources(flowLayoutPanelInnerRadius, "flowLayoutPanelInnerRadius");
            flowLayoutPanelInnerRadius.Controls.Add(numericBoxSTEM_DetectorInnerAngle);
            flowLayoutPanelInnerRadius.Controls.Add(textBoxInnerRadius);
            flowLayoutPanelInnerRadius.Controls.Add(label37);
            flowLayoutPanelInnerRadius.Name = "flowLayoutPanelInnerRadius";
            // 
            // numericBoxSTEM_DetectorInnerAngle
            // 
            numericBoxSTEM_DetectorInnerAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_DetectorInnerAngle.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxSTEM_DetectorInnerAngle, "numericBoxSTEM_DetectorInnerAngle");
            numericBoxSTEM_DetectorInnerAngle.Maximum = 1570D;
            numericBoxSTEM_DetectorInnerAngle.Minimum = 0D;
            numericBoxSTEM_DetectorInnerAngle.Name = "numericBoxSTEM_DetectorInnerAngle";
            numericBoxSTEM_DetectorInnerAngle.ShowUpDown = true;
            numericBoxSTEM_DetectorInnerAngle.SmartIncrement = true;
            numericBoxSTEM_DetectorInnerAngle.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxSTEM_DetectorInnerAngle, resources.GetString("numericBoxSTEM_DetectorInnerAngle.ToolTip"));
            numericBoxSTEM_DetectorInnerAngle.UpDown_Increment = 0.5D;
            numericBoxSTEM_DetectorInnerAngle.ValueBoxWidth = 50;
            numericBoxSTEM_DetectorInnerAngle.ValueFontSize = 9F;
            numericBoxSTEM_DetectorInnerAngle.ValueChanged += numericBoxSTEM_ConvergenceAngle_ValueChanged;
            // 
            // textBoxInnerRadius
            // 
            textBoxInnerRadius.BackColor = System.Drawing.SystemColors.InactiveCaption;
            resources.ApplyResources(textBoxInnerRadius, "textBoxInnerRadius");
            textBoxInnerRadius.ForeColor = System.Drawing.Color.DimGray;
            textBoxInnerRadius.Name = "textBoxInnerRadius";
            textBoxInnerRadius.ReadOnly = true;
            toolTip.SetToolTip(textBoxInnerRadius, resources.GetString("textBoxInnerRadius.ToolTip"));
            // 
            // label37
            // 
            resources.ApplyResources(label37, "label37");
            label37.ForeColor = System.Drawing.Color.Black;
            label37.Name = "label37";
            toolTip.SetToolTip(label37, resources.GetString("label37.ToolTip"));
            // 
            // numericBoxSTEM_EffectiveSourceSize
            // 
            numericBoxSTEM_EffectiveSourceSize.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_EffectiveSourceSize.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxSTEM_EffectiveSourceSize, "numericBoxSTEM_EffectiveSourceSize");
            numericBoxSTEM_EffectiveSourceSize.Maximum = 1000D;
            numericBoxSTEM_EffectiveSourceSize.Minimum = 0D;
            numericBoxSTEM_EffectiveSourceSize.Name = "numericBoxSTEM_EffectiveSourceSize";
            numericBoxSTEM_EffectiveSourceSize.RadianValue = 0.3490658503988659D;
            numericBoxSTEM_EffectiveSourceSize.RestrictLimitValue = false;
            numericBoxSTEM_EffectiveSourceSize.ShowUpDown = true;
            numericBoxSTEM_EffectiveSourceSize.SmartIncrement = true;
            numericBoxSTEM_EffectiveSourceSize.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxSTEM_EffectiveSourceSize, resources.GetString("numericBoxSTEM_EffectiveSourceSize.ToolTip"));
            numericBoxSTEM_EffectiveSourceSize.UpDown_Increment = 0.1D;
            numericBoxSTEM_EffectiveSourceSize.Value = 20D;
            numericBoxSTEM_EffectiveSourceSize.ValueBoxWidth = 36;
            numericBoxSTEM_EffectiveSourceSize.ValueFontSize = 9F;
            numericBoxSTEM_EffectiveSourceSize.ValueChanged += NumericBoxTEMproperty_ValueChanged;
            // 
            // groupBoxHREMoption1
            // 
            resources.ApplyResources(groupBoxHREMoption1, "groupBoxHREMoption1");
            captureExtender.SetCapture(groupBoxHREMoption1, true);
            groupBoxHREMoption1.Controls.Add(flowLayoutPanelSpotCount);
            groupBoxHREMoption1.Controls.Add(flowLayoutPanel7);
            groupBoxHREMoption1.Controls.Add(flowLayoutPanel6);
            groupBoxHREMoption1.Name = "groupBoxHREMoption1";
            groupBoxHREMoption1.TabStop = false;
            // 
            // flowLayoutPanelSpotCount
            // 
            resources.ApplyResources(flowLayoutPanelSpotCount, "flowLayoutPanelSpotCount");
            flowLayoutPanelSpotCount.Controls.Add(buttonDetailsOfSpots);
            flowLayoutPanelSpotCount.Controls.Add(label8);
            flowLayoutPanelSpotCount.Name = "flowLayoutPanelSpotCount";
            // 
            // buttonDetailsOfSpots
            // 
            resources.ApplyResources(buttonDetailsOfSpots, "buttonDetailsOfSpots");
            buttonDetailsOfSpots.Name = "buttonDetailsOfSpots";
            toolTip.SetToolTip(buttonDetailsOfSpots, resources.GetString("buttonDetailsOfSpots.ToolTip"));
            buttonDetailsOfSpots.UseVisualStyleBackColor = true;
            buttonDetailsOfSpots.Click += ButtonDetailsOfSpots_Click;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.ForeColor = System.Drawing.Color.DimGray;
            label8.Name = "label8";
            // 
            // flowLayoutPanel7
            // 
            resources.ApplyResources(flowLayoutPanel7, "flowLayoutPanel7");
            flowLayoutPanel7.Controls.Add(numericBoxHRTEM_ObjAperX);
            flowLayoutPanel7.Controls.Add(numericBoxHRTEM_ObjAperY);
            flowLayoutPanel7.Controls.Add(textBoxNumOfSpots);
            flowLayoutPanel7.Controls.Add(label9);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            // 
            // numericBoxHRTEM_ObjAperX
            // 
            numericBoxHRTEM_ObjAperX.BackColor = System.Drawing.SystemColors.Control;
            numericBoxHRTEM_ObjAperX.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxHRTEM_ObjAperX, "numericBoxHRTEM_ObjAperX");
            numericBoxHRTEM_ObjAperX.Maximum = 100D;
            numericBoxHRTEM_ObjAperX.Minimum = -100D;
            numericBoxHRTEM_ObjAperX.Name = "numericBoxHRTEM_ObjAperX";
            numericBoxHRTEM_ObjAperX.ShowUpDown = true;
            numericBoxHRTEM_ObjAperX.SmartIncrement = true;
            numericBoxHRTEM_ObjAperX.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxHRTEM_ObjAperX, resources.GetString("numericBoxHRTEM_ObjAperX.ToolTip"));
            numericBoxHRTEM_ObjAperX.UpDown_Increment = 0.5D;
            numericBoxHRTEM_ObjAperX.ValueBoxWidth = 33;
            numericBoxHRTEM_ObjAperX.ValueFontSize = 9F;
            numericBoxHRTEM_ObjAperX.ValueChanged += NumericBoxObjAperRadius_ValueChanged;
            // 
            // numericBoxHRTEM_ObjAperY
            // 
            numericBoxHRTEM_ObjAperY.BackColor = System.Drawing.SystemColors.Control;
            numericBoxHRTEM_ObjAperY.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxHRTEM_ObjAperY, "numericBoxHRTEM_ObjAperY");
            numericBoxHRTEM_ObjAperY.Maximum = 100D;
            numericBoxHRTEM_ObjAperY.Minimum = -100D;
            numericBoxHRTEM_ObjAperY.Name = "numericBoxHRTEM_ObjAperY";
            numericBoxHRTEM_ObjAperY.ShowUpDown = true;
            numericBoxHRTEM_ObjAperY.SmartIncrement = true;
            numericBoxHRTEM_ObjAperY.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxHRTEM_ObjAperY, resources.GetString("numericBoxHRTEM_ObjAperY.ToolTip"));
            numericBoxHRTEM_ObjAperY.UpDown_Increment = 0.5D;
            numericBoxHRTEM_ObjAperY.ValueBoxWidth = 33;
            numericBoxHRTEM_ObjAperY.ValueFontSize = 9F;
            numericBoxHRTEM_ObjAperY.ValueChanged += NumericBoxObjAperRadius_ValueChanged;
            // 
            // textBoxNumOfSpots
            // 
            textBoxNumOfSpots.BackColor = System.Drawing.SystemColors.InactiveCaption;
            resources.ApplyResources(textBoxNumOfSpots, "textBoxNumOfSpots");
            textBoxNumOfSpots.ForeColor = System.Drawing.Color.DimGray;
            textBoxNumOfSpots.Name = "textBoxNumOfSpots";
            textBoxNumOfSpots.ReadOnly = true;
            toolTip.SetToolTip(textBoxNumOfSpots, resources.GetString("textBoxNumOfSpots.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.ForeColor = System.Drawing.Color.Black;
            label9.Name = "label9";
            toolTip.SetToolTip(label9, resources.GetString("label9.ToolTip"));
            // 
            // flowLayoutPanel6
            // 
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Controls.Add(numericBoxObjAperRadius);
            flowLayoutPanel6.Controls.Add(checkBoxOpenAperture);
            flowLayoutPanel6.Controls.Add(flowLayoutPanelObjectiveAperture);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            // 
            // numericBoxObjAperRadius
            // 
            numericBoxObjAperRadius.BackColor = System.Drawing.SystemColors.Control;
            numericBoxObjAperRadius.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxObjAperRadius, "numericBoxObjAperRadius");
            numericBoxObjAperRadius.Maximum = 500D;
            numericBoxObjAperRadius.Minimum = 0.5D;
            numericBoxObjAperRadius.Name = "numericBoxObjAperRadius";
            numericBoxObjAperRadius.RadianValue = 0.20943951023931953D;
            numericBoxObjAperRadius.ShowUpDown = true;
            numericBoxObjAperRadius.SmartIncrement = true;
            numericBoxObjAperRadius.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxObjAperRadius, resources.GetString("numericBoxObjAperRadius.ToolTip"));
            numericBoxObjAperRadius.UpDown_Increment = 0.5D;
            numericBoxObjAperRadius.Value = 12D;
            numericBoxObjAperRadius.ValueBoxWidth = 32;
            numericBoxObjAperRadius.ValueFontSize = 9F;
            numericBoxObjAperRadius.ValueChanged += NumericBoxObjAperRadius_ValueChanged;
            // 
            // checkBoxOpenAperture
            // 
            resources.ApplyResources(checkBoxOpenAperture, "checkBoxOpenAperture");
            checkBoxOpenAperture.Name = "checkBoxOpenAperture";
            toolTip.SetToolTip(checkBoxOpenAperture, resources.GetString("checkBoxOpenAperture.ToolTip"));
            checkBoxOpenAperture.UseVisualStyleBackColor = true;
            checkBoxOpenAperture.CheckedChanged += NumericBoxObjAperRadius_ValueChanged;
            // 
            // flowLayoutPanelObjectiveAperture
            // 
            resources.ApplyResources(flowLayoutPanelObjectiveAperture, "flowLayoutPanelObjectiveAperture");
            flowLayoutPanelObjectiveAperture.Controls.Add(textBoxObjAperRadius);
            flowLayoutPanelObjectiveAperture.Controls.Add(label7);
            flowLayoutPanelObjectiveAperture.Name = "flowLayoutPanelObjectiveAperture";
            // 
            // textBoxObjAperRadius
            // 
            textBoxObjAperRadius.BackColor = System.Drawing.SystemColors.InactiveCaption;
            resources.ApplyResources(textBoxObjAperRadius, "textBoxObjAperRadius");
            textBoxObjAperRadius.ForeColor = System.Drawing.Color.DimGray;
            textBoxObjAperRadius.Name = "textBoxObjAperRadius";
            textBoxObjAperRadius.ReadOnly = true;
            toolTip.SetToolTip(textBoxObjAperRadius, resources.GetString("textBoxObjAperRadius.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.ForeColor = System.Drawing.Color.Black;
            label7.Name = "label7";
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip"));
            // 
            // groupBoxTEMConditions
            // 
            resources.ApplyResources(groupBoxTEMConditions, "groupBoxTEMConditions");
            captureExtender.SetCapture(groupBoxTEMConditions, true);
            groupBoxTEMConditions.ContextMenuStrip = contextMenuStripTEMcondition;
            groupBoxTEMConditions.Controls.Add(checkBoxCTF);
            groupBoxTEMConditions.Controls.Add(flowLayoutPanel5);
            groupBoxTEMConditions.Controls.Add(flowLayoutPanel4);
            groupBoxTEMConditions.Controls.Add(flowLayoutPanel3);
            groupBoxTEMConditions.Name = "groupBoxTEMConditions";
            groupBoxTEMConditions.TabStop = false;
            // 
            // contextMenuStripTEMcondition
            // 
            contextMenuStripTEMcondition.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { setoZeroDefocusToolStripMenuItem, setoScherzerDefocusToolStripMenuItem, setAllAToolStripMenuItem, toolStripSeparator6, presets1ToolStripMenuItem, presets2ToolStripMenuItem, presets3ToolStripMenuItem, presets4ToolStripMenuItem });
            contextMenuStripTEMcondition.Name = "contextMenuStripInherentProperty";
            resources.ApplyResources(contextMenuStripTEMcondition, "contextMenuStripTEMcondition");
            // 
            // setoZeroDefocusToolStripMenuItem
            // 
            setoZeroDefocusToolStripMenuItem.Name = "setoZeroDefocusToolStripMenuItem";
            resources.ApplyResources(setoZeroDefocusToolStripMenuItem, "setoZeroDefocusToolStripMenuItem");
            setoZeroDefocusToolStripMenuItem.Click += setZeroDefocusToolStripMenuItem_Click;
            // 
            // setoScherzerDefocusToolStripMenuItem
            // 
            setoScherzerDefocusToolStripMenuItem.Name = "setoScherzerDefocusToolStripMenuItem";
            resources.ApplyResources(setoScherzerDefocusToolStripMenuItem, "setoScherzerDefocusToolStripMenuItem");
            setoScherzerDefocusToolStripMenuItem.Click += setScherzerDefocusToolStripMenuItem_Click;
            // 
            // setAllAToolStripMenuItem
            // 
            setAllAToolStripMenuItem.Name = "setAllAToolStripMenuItem";
            resources.ApplyResources(setAllAToolStripMenuItem, "setAllAToolStripMenuItem");
            setAllAToolStripMenuItem.Click += zeroAllToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
            // 
            // presets1ToolStripMenuItem
            // 
            presets1ToolStripMenuItem.Name = "presets1ToolStripMenuItem";
            resources.ApplyResources(presets1ToolStripMenuItem, "presets1ToolStripMenuItem");
            presets1ToolStripMenuItem.Click += presets1ToolStripMenuItem_Click;
            // 
            // presets2ToolStripMenuItem
            // 
            presets2ToolStripMenuItem.Name = "presets2ToolStripMenuItem";
            resources.ApplyResources(presets2ToolStripMenuItem, "presets2ToolStripMenuItem");
            presets2ToolStripMenuItem.Click += presets2ToolStripMenuItem_Click;
            // 
            // presets3ToolStripMenuItem
            // 
            presets3ToolStripMenuItem.Name = "presets3ToolStripMenuItem";
            resources.ApplyResources(presets3ToolStripMenuItem, "presets3ToolStripMenuItem");
            presets3ToolStripMenuItem.Click += presets3ToolStripMenuItem_Click;
            // 
            // presets4ToolStripMenuItem
            // 
            presets4ToolStripMenuItem.Name = "presets4ToolStripMenuItem";
            resources.ApplyResources(presets4ToolStripMenuItem, "presets4ToolStripMenuItem");
            presets4ToolStripMenuItem.Click += presets4ToolStripMenuItem_Click;
            // 
            // checkBoxCTF
            // 
            resources.ApplyResources(checkBoxCTF, "checkBoxCTF");
            checkBoxCTF.Name = "checkBoxCTF";
            toolTip.SetToolTip(checkBoxCTF, resources.GetString("checkBoxCTF.ToolTip"));
            checkBoxCTF.UseVisualStyleBackColor = true;
            checkBoxCTF.CheckedChanged += checkBoxShowLensFunctionGraph_CheckedChanged;
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(numericBoxCs);
            flowLayoutPanel5.Controls.Add(numericBoxCc);
            flowLayoutPanel5.Controls.Add(numericBoxHRTEM_BetaAgnle);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            // 
            // numericBoxCs
            // 
            numericBoxCs.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCs.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxCs, "numericBoxCs");
            numericBoxCs.Maximum = 20D;
            numericBoxCs.Minimum = -20D;
            numericBoxCs.Name = "numericBoxCs";
            numericBoxCs.RadianValue = 0.017453292519943295D;
            numericBoxCs.ShowUpDown = true;
            numericBoxCs.SmartIncrement = true;
            numericBoxCs.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxCs, resources.GetString("numericBoxCs.ToolTip"));
            numericBoxCs.UpDown_Increment = 0.1D;
            numericBoxCs.Value = 1D;
            numericBoxCs.ValueBoxWidth = 36;
            numericBoxCs.ValueFontSize = 9F;
            numericBoxCs.ValueChanged += NumericBoxCs_ValueChanged;
            // 
            // numericBoxCc
            // 
            numericBoxCc.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCc.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxCc, "numericBoxCc");
            numericBoxCc.Maximum = 10D;
            numericBoxCc.Minimum = 0D;
            numericBoxCc.Name = "numericBoxCc";
            numericBoxCc.RadianValue = 0.024434609527920613D;
            numericBoxCc.RestrictLimitValue = false;
            numericBoxCc.ShowUpDown = true;
            numericBoxCc.SmartIncrement = true;
            numericBoxCc.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxCc, resources.GetString("numericBoxCc.ToolTip"));
            numericBoxCc.UpDown_Increment = 0.1D;
            numericBoxCc.Value = 1.4D;
            numericBoxCc.ValueBoxWidth = 36;
            numericBoxCc.ValueFontSize = 9F;
            numericBoxCc.ValueChanged += NumericBoxTEMproperty_ValueChanged;
            // 
            // numericBoxHRTEM_BetaAgnle
            // 
            numericBoxHRTEM_BetaAgnle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxHRTEM_BetaAgnle.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxHRTEM_BetaAgnle, "numericBoxHRTEM_BetaAgnle");
            numericBoxHRTEM_BetaAgnle.Maximum = 100D;
            numericBoxHRTEM_BetaAgnle.Minimum = 0D;
            numericBoxHRTEM_BetaAgnle.Name = "numericBoxHRTEM_BetaAgnle";
            numericBoxHRTEM_BetaAgnle.ShowUpDown = true;
            numericBoxHRTEM_BetaAgnle.SmartIncrement = true;
            numericBoxHRTEM_BetaAgnle.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxHRTEM_BetaAgnle, resources.GetString("numericBoxHRTEM_BetaAgnle.ToolTip"));
            numericBoxHRTEM_BetaAgnle.UpDown_Increment = 0.05D;
            numericBoxHRTEM_BetaAgnle.ValueBoxWidth = 36;
            numericBoxHRTEM_BetaAgnle.ValueFontSize = 9F;
            numericBoxHRTEM_BetaAgnle.ValueChanged += NumericBoxTEMproperty_ValueChanged;
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(numericBoxDefocus);
            flowLayoutPanel4.Controls.Add(flowLayoutPanelScherzer);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // numericBoxDefocus
            // 
            numericBoxDefocus.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocus.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxDefocus, "numericBoxDefocus");
            numericBoxDefocus.Maximum = 1000D;
            numericBoxDefocus.Minimum = -1000D;
            numericBoxDefocus.Name = "numericBoxDefocus";
            numericBoxDefocus.RadianValue = -1.0088003076527223D;
            numericBoxDefocus.ShowUpDown = true;
            numericBoxDefocus.SmartIncrement = true;
            numericBoxDefocus.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxDefocus, resources.GetString("numericBoxDefocus.ToolTip"));
            numericBoxDefocus.Value = -57.8D;
            numericBoxDefocus.ValueBoxWidth = 40;
            numericBoxDefocus.ValueFontSize = 9F;
            numericBoxDefocus.ValueChanged += NumericBoxDefocus_ValueChanged;
            // 
            // flowLayoutPanelScherzer
            // 
            resources.ApplyResources(flowLayoutPanelScherzer, "flowLayoutPanelScherzer");
            flowLayoutPanelScherzer.Controls.Add(label3);
            flowLayoutPanelScherzer.Controls.Add(textBoxScherzer);
            flowLayoutPanelScherzer.Controls.Add(label4);
            flowLayoutPanelScherzer.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            flowLayoutPanelScherzer.Name = "flowLayoutPanelScherzer";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip"));
            // 
            // textBoxScherzer
            // 
            textBoxScherzer.BackColor = System.Drawing.SystemColors.InactiveCaption;
            resources.ApplyResources(textBoxScherzer, "textBoxScherzer");
            textBoxScherzer.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            textBoxScherzer.Name = "textBoxScherzer";
            toolTip.SetToolTip(textBoxScherzer, resources.GetString("textBoxScherzer.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            label4.Name = "label4";
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(numericBoxAccVol);
            flowLayoutPanel3.Controls.Add(numericBoxDeltaV);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // numericBoxAccVol
            // 
            numericBoxAccVol.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxAccVol, "numericBoxAccVol");
            numericBoxAccVol.Maximum = 1000D;
            numericBoxAccVol.Minimum = 1D;
            numericBoxAccVol.Name = "numericBoxAccVol";
            numericBoxAccVol.RadianValue = 3.4906585039886591D;
            numericBoxAccVol.ShowUpDown = true;
            numericBoxAccVol.SmartIncrement = true;
            numericBoxAccVol.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxAccVol, resources.GetString("numericBoxAccVol.ToolTip"));
            numericBoxAccVol.Value = 200D;
            numericBoxAccVol.ValueBoxWidth = 36;
            numericBoxAccVol.ValueFontSize = 9F;
            numericBoxAccVol.ValueChanged += NumericBoxAccVol_ValueChanged;
            // 
            // numericBoxDeltaV
            // 
            numericBoxDeltaV.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDeltaV.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxDeltaV, "numericBoxDeltaV");
            numericBoxDeltaV.Maximum = 10D;
            numericBoxDeltaV.Minimum = 0D;
            numericBoxDeltaV.Name = "numericBoxDeltaV";
            numericBoxDeltaV.RadianValue = 0.013962634015954637D;
            numericBoxDeltaV.RestrictLimitValue = false;
            numericBoxDeltaV.ShowUpDown = true;
            numericBoxDeltaV.SmartIncrement = true;
            numericBoxDeltaV.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxDeltaV, resources.GetString("numericBoxDeltaV.ToolTip"));
            numericBoxDeltaV.UpDown_Increment = 0.1D;
            numericBoxDeltaV.Value = 0.8D;
            numericBoxDeltaV.ValueBoxWidth = 36;
            numericBoxDeltaV.ValueFontSize = 9F;
            numericBoxDeltaV.ValueChanged += NumericBoxTEMproperty_ValueChanged;
            // 
            // flowLayoutPanelModeSelection
            // 
            resources.ApplyResources(flowLayoutPanelModeSelection, "flowLayoutPanelModeSelection");
            flowLayoutPanelModeSelection.Controls.Add(groupBoxImageMode);
            flowLayoutPanelModeSelection.Controls.Add(groupBoxSampleProperty);
            flowLayoutPanelModeSelection.Name = "flowLayoutPanelModeSelection";
            // 
            // groupBoxImageMode
            // 
            captureExtender.SetCapture(groupBoxImageMode, true);
            groupBoxImageMode.Controls.Add(flowLayoutPanelImageType);
            resources.ApplyResources(groupBoxImageMode, "groupBoxImageMode");
            groupBoxImageMode.Name = "groupBoxImageMode";
            groupBoxImageMode.TabStop = false;
            // 
            // flowLayoutPanelImageType
            // 
            resources.ApplyResources(flowLayoutPanelImageType, "flowLayoutPanelImageType");
            flowLayoutPanelImageType.Controls.Add(radioButtonHRTEM);
            flowLayoutPanelImageType.Controls.Add(radioButtonSTEM);
            flowLayoutPanelImageType.Controls.Add(radioButton1);
            flowLayoutPanelImageType.Controls.Add(radioButtonProjectedPotential);
            flowLayoutPanelImageType.Name = "flowLayoutPanelImageType";
            // 
            // radioButtonHRTEM
            // 
            resources.ApplyResources(radioButtonHRTEM, "radioButtonHRTEM");
            radioButtonHRTEM.Checked = true;
            radioButtonHRTEM.Name = "radioButtonHRTEM";
            radioButtonHRTEM.TabStop = true;
            toolTip.SetToolTip(radioButtonHRTEM, resources.GetString("radioButtonHRTEM.ToolTip"));
            radioButtonHRTEM.UseVisualStyleBackColor = true;
            radioButtonHRTEM.CheckedChanged += RadioButtonHRTEM_CheckedChanged;
            // 
            // radioButtonSTEM
            // 
            resources.ApplyResources(radioButtonSTEM, "radioButtonSTEM");
            radioButtonSTEM.Name = "radioButtonSTEM";
            toolTip.SetToolTip(radioButtonSTEM, resources.GetString("radioButtonSTEM.ToolTip"));
            radioButtonSTEM.UseVisualStyleBackColor = true;
            radioButtonSTEM.CheckedChanged += RadioButtonHRTEM_CheckedChanged;
            // 
            // radioButton1
            // 
            resources.ApplyResources(radioButton1, "radioButton1");
            radioButton1.Name = "radioButton1";
            toolTip.SetToolTip(radioButton1, resources.GetString("radioButton1.ToolTip"));
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += RadioButtonHRTEM_CheckedChanged;
            // 
            // radioButtonProjectedPotential
            // 
            resources.ApplyResources(radioButtonProjectedPotential, "radioButtonProjectedPotential");
            radioButtonProjectedPotential.Name = "radioButtonProjectedPotential";
            toolTip.SetToolTip(radioButtonProjectedPotential, resources.GetString("radioButtonProjectedPotential.ToolTip"));
            radioButtonProjectedPotential.UseVisualStyleBackColor = true;
            radioButtonProjectedPotential.CheckedChanged += RadioButtonHRTEM_CheckedChanged;
            // 
            // groupBoxSampleProperty
            // 
            captureExtender.SetCapture(groupBoxSampleProperty, true);
            groupBoxSampleProperty.Controls.Add(flowLayoutPanel1);
            resources.ApplyResources(groupBoxSampleProperty, "groupBoxSampleProperty");
            groupBoxSampleProperty.Name = "groupBoxSampleProperty";
            groupBoxSampleProperty.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(numericBoxThickness);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.ForeColor = System.Drawing.SystemColors.ControlText;
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // numericBoxThickness
            // 
            numericBoxThickness.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThickness.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxThickness, "numericBoxThickness");
            numericBoxThickness.Maximum = 1000D;
            numericBoxThickness.Minimum = 0.001D;
            numericBoxThickness.Name = "numericBoxThickness";
            numericBoxThickness.RadianValue = 0.3490658503988659D;
            numericBoxThickness.ShowUpDown = true;
            numericBoxThickness.SmartIncrement = true;
            numericBoxThickness.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThickness, resources.GetString("numericBoxThickness.ToolTip"));
            numericBoxThickness.Value = 20D;
            numericBoxThickness.ValueBoxWidth = 50;
            numericBoxThickness.ValueFontSize = 9F;
            numericBoxThickness.ValueChanged += NumericBoxThickness_ValueChanged;
            // 
            // panelSimulationActions
            // 
            captureExtender.SetCapture(panelSimulationActions, true);
            panelSimulationActions.Controls.Add(checkBoxPreset);
            panelSimulationActions.Controls.Add(checkBoxRealTimeSimulation);
            panelSimulationActions.Controls.Add(buttonSimulate);
            resources.ApplyResources(panelSimulationActions, "panelSimulationActions");
            panelSimulationActions.Name = "panelSimulationActions";
            // 
            // checkBoxPreset
            // 
            resources.ApplyResources(checkBoxPreset, "checkBoxPreset");
            checkBoxPreset.Name = "checkBoxPreset";
            toolTip.SetToolTip(checkBoxPreset, resources.GetString("checkBoxPreset.ToolTip"));
            checkBoxPreset.UseVisualStyleBackColor = true;
            checkBoxPreset.CheckedChanged += checkBoxPreset_CheckedChanged;
            // 
            // checkBoxRealTimeSimulation
            // 
            resources.ApplyResources(checkBoxRealTimeSimulation, "checkBoxRealTimeSimulation");
            checkBoxRealTimeSimulation.Name = "checkBoxRealTimeSimulation";
            toolTip.SetToolTip(checkBoxRealTimeSimulation, resources.GetString("checkBoxRealTimeSimulation.ToolTip"));
            checkBoxRealTimeSimulation.UseVisualStyleBackColor = true;
            // 
            // buttonSimulate
            // 
            resources.ApplyResources(buttonSimulate, "buttonSimulate");
            buttonSimulate.BackColor = System.Drawing.Color.SteelBlue;
            buttonSimulate.ForeColor = System.Drawing.Color.White;
            buttonSimulate.Name = "buttonSimulate";
            toolTip.SetToolTip(buttonSimulate, resources.GetString("buttonSimulate.ToolTip"));
            buttonSimulate.UseVisualStyleBackColor = false;
            buttonSimulate.Click += ButtonSimulate_Click;
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            captureExtender.SetCapture(fileToolStripMenuItem, true);
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemSave, copyImageToolStripMenuItem, toolStripMenuItemOverprintSymbols, toolStripSeparator1, loadTEMParameterToolStripMenuItem, saveTEMParametersToolStripMenuItem });
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // toolStripMenuItemSave
            // 
            toolStripMenuItemSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemSavePNG, toolStripMenuItemSaveTIFF, toolStripMenuItemSaveMetafile, toolStripMenuItemSaveIndividually });
            toolStripMenuItemSave.Name = "toolStripMenuItemSave";
            resources.ApplyResources(toolStripMenuItemSave, "toolStripMenuItemSave");
            // 
            // toolStripMenuItemSavePNG
            // 
            toolStripMenuItemSavePNG.Name = "toolStripMenuItemSavePNG";
            resources.ApplyResources(toolStripMenuItemSavePNG, "toolStripMenuItemSavePNG");
            toolStripMenuItemSavePNG.Click += ToolStripMenuItemSavePNG_Click;
            // 
            // toolStripMenuItemSaveTIFF
            // 
            toolStripMenuItemSaveTIFF.Name = "toolStripMenuItemSaveTIFF";
            resources.ApplyResources(toolStripMenuItemSaveTIFF, "toolStripMenuItemSaveTIFF");
            toolStripMenuItemSaveTIFF.Click += ToolStripMenuItemSaveTIFF_Click;
            // 
            // toolStripMenuItemSaveMetafile
            // 
            toolStripMenuItemSaveMetafile.Name = "toolStripMenuItemSaveMetafile";
            resources.ApplyResources(toolStripMenuItemSaveMetafile, "toolStripMenuItemSaveMetafile");
            toolStripMenuItemSaveMetafile.Click += ToolStripMenuItemSaveMetafile_Click;
            // 
            // toolStripMenuItemSaveIndividually
            // 
            toolStripMenuItemSaveIndividually.Checked = true;
            toolStripMenuItemSaveIndividually.CheckOnClick = true;
            toolStripMenuItemSaveIndividually.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripMenuItemSaveIndividually.Name = "toolStripMenuItemSaveIndividually";
            resources.ApplyResources(toolStripMenuItemSaveIndividually, "toolStripMenuItemSaveIndividually");
            // 
            // copyImageToolStripMenuItem
            // 
            copyImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemCopyImage, toolStripMenuItemCopyMetafile });
            copyImageToolStripMenuItem.Name = "copyImageToolStripMenuItem";
            resources.ApplyResources(copyImageToolStripMenuItem, "copyImageToolStripMenuItem");
            // 
            // toolStripMenuItemCopyImage
            // 
            toolStripMenuItemCopyImage.Name = "toolStripMenuItemCopyImage";
            resources.ApplyResources(toolStripMenuItemCopyImage, "toolStripMenuItemCopyImage");
            toolStripMenuItemCopyImage.Click += ToolStripMenuItemCopyImage_Click;
            // 
            // toolStripMenuItemCopyMetafile
            // 
            toolStripMenuItemCopyMetafile.Name = "toolStripMenuItemCopyMetafile";
            resources.ApplyResources(toolStripMenuItemCopyMetafile, "toolStripMenuItemCopyMetafile");
            toolStripMenuItemCopyMetafile.Click += ToolStripMenuItemCopyMetafile_Click;
            // 
            // toolStripMenuItemOverprintSymbols
            // 
            toolStripMenuItemOverprintSymbols.Checked = true;
            toolStripMenuItemOverprintSymbols.CheckOnClick = true;
            toolStripMenuItemOverprintSymbols.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripMenuItemOverprintSymbols.Name = "toolStripMenuItemOverprintSymbols";
            resources.ApplyResources(toolStripMenuItemOverprintSymbols, "toolStripMenuItemOverprintSymbols");
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // loadTEMParameterToolStripMenuItem
            // 
            resources.ApplyResources(loadTEMParameterToolStripMenuItem, "loadTEMParameterToolStripMenuItem");
            loadTEMParameterToolStripMenuItem.Name = "loadTEMParameterToolStripMenuItem";
            // 
            // saveTEMParametersToolStripMenuItem
            // 
            resources.ApplyResources(saveTEMParametersToolStripMenuItem, "saveTEMParametersToolStripMenuItem");
            saveTEMParametersToolStripMenuItem.Name = "saveTEMParametersToolStripMenuItem";
            // 
            // helpToolStripMenuItem
            // 
            captureExtender.SetCapture(helpToolStripMenuItem, true);
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { detailsOfHRTEMSimulationToolStripMenuItem, toolStripSeparator2, calculationLibraryToolStripMenuItem, toolStripComboBoxCaclulationLibrary });
            resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // detailsOfHRTEMSimulationToolStripMenuItem
            // 
            detailsOfHRTEMSimulationToolStripMenuItem.Name = "detailsOfHRTEMSimulationToolStripMenuItem";
            resources.ApplyResources(detailsOfHRTEMSimulationToolStripMenuItem, "detailsOfHRTEMSimulationToolStripMenuItem");
            detailsOfHRTEMSimulationToolStripMenuItem.Click += DetailsOfHRTEMSimulationToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // calculationLibraryToolStripMenuItem
            // 
            calculationLibraryToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            calculationLibraryToolStripMenuItem.Name = "calculationLibraryToolStripMenuItem";
            resources.ApplyResources(calculationLibraryToolStripMenuItem, "calculationLibraryToolStripMenuItem");
            // 
            // toolStripComboBoxCaclulationLibrary
            // 
            toolStripComboBoxCaclulationLibrary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxCaclulationLibrary.Items.AddRange(new object[] { resources.GetString("toolStripComboBoxCaclulationLibrary.Items"), resources.GetString("toolStripComboBoxCaclulationLibrary.Items1") });
            toolStripComboBoxCaclulationLibrary.Margin = new System.Windows.Forms.Padding(20, 2, 2, 2);
            toolStripComboBoxCaclulationLibrary.Name = "toolStripComboBoxCaclulationLibrary";
            resources.ApplyResources(toolStripComboBoxCaclulationLibrary, "toolStripComboBoxCaclulationLibrary");
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 100;
            // 
            // buttonStop
            // 
            buttonStop.BackColor = System.Drawing.Color.IndianRed;
            resources.ApplyResources(buttonStop, "buttonStop");
            buttonStop.ForeColor = System.Drawing.Color.White;
            buttonStop.Name = "buttonStop";
            toolTip.SetToolTip(buttonStop, resources.GetString("buttonStop.ToolTip"));
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Click += buttonStop_Click;
            // 
            // statusStrip1
            // 
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
            // FormImageSimulator
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(buttonStop);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "FormImageSimulator";
            FormClosing += FormImageSimulator_FormClosing;
            Load += FormImageSimulator_Load;
            VisibleChanged += FormImageSimulator_VisibleChanged;
            KeyDown += FormImageSimulator_KeyDown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panelImageStatus.ResumeLayout(false);
            panelImageStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxScaleOfIntensity).EndInit();
            panelDisplaySettings.ResumeLayout(false);
            groupBoxAdjust.ResumeLayout(false);
            groupBoxAdjust.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            groupBoxNormalization.ResumeLayout(false);
            groupBoxNormalization.PerformLayout();
            flowLayoutPanelIntensityRange.ResumeLayout(false);
            flowLayoutPanelIntensityRange.PerformLayout();
            groupBoxSTEMoption3.ResumeLayout(false);
            groupBoxSTEMoption3.PerformLayout();
            groupBoxDisplay.ResumeLayout(false);
            groupBoxDisplay.PerformLayout();
            groupBoxSimulation.ResumeLayout(false);
            groupBoxSimulation.PerformLayout();
            panelModeOptions.ResumeLayout(false);
            panelModeOptions.PerformLayout();
            groupBoxSerialImage.ResumeLayout(false);
            groupBoxSerialImage.PerformLayout();
            panelSerial.ResumeLayout(false);
            panelSerial.PerformLayout();
            panelSerialDefocus.ResumeLayout(false);
            panelSerialDefocus.PerformLayout();
            panelSerialThickness.ResumeLayout(false);
            panelSerialThickness.PerformLayout();
            panelSerialSettings.ResumeLayout(false);
            panelSerialSettings.PerformLayout();
            flowLayoutPanelHorizontalDirection.ResumeLayout(false);
            flowLayoutPanelHorizontalDirection.PerformLayout();
            flowLayoutPanelSimulationMode.ResumeLayout(false);
            flowLayoutPanelSimulationMode.PerformLayout();
            groupBoxPotentialOption.ResumeLayout(false);
            groupBoxPotentialOption.PerformLayout();
            flowLayoutPanelPotentialMode.ResumeLayout(false);
            flowLayoutPanelPotentialMode.PerformLayout();
            flowLayoutPanelMagAndPhase.ResumeLayout(false);
            flowLayoutPanelMagAndPhase.PerformLayout();
            panelPhaseScale.ResumeLayout(false);
            panelPhaseScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhaseScale).EndInit();
            flowLayoutPanelRealAndImaiginary.ResumeLayout(false);
            flowLayoutPanelRealAndImaiginary.PerformLayout();
            groupBoxSTEMoption2.ResumeLayout(false);
            groupBoxSTEMoption2.PerformLayout();
            flowLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel11.PerformLayout();
            groupBoxHREMoption2.ResumeLayout(false);
            groupBoxHREMoption2.PerformLayout();
            flowLayoutPanel10.ResumeLayout(false);
            flowLayoutPanel10.PerformLayout();
            panelImageProperties.ResumeLayout(false);
            panelImageProperties.PerformLayout();
            groupBoxImageProperty.ResumeLayout(false);
            groupBoxImageProperty.PerformLayout();
            groupBoxDiffractedWaves.ResumeLayout(false);
            groupBoxDiffractedWaves.PerformLayout();
            groupBoxOpticalProperty.ResumeLayout(false);
            groupBoxOpticalProperty.PerformLayout();
            groupBoxSTEMoption1.ResumeLayout(false);
            groupBoxSTEMoption1.PerformLayout();
            contextMenuStripSTEM.ResumeLayout(false);
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            flowLayoutPanelConvergenceRadius.ResumeLayout(false);
            flowLayoutPanelConvergenceRadius.PerformLayout();
            flowLayoutPanelOuterRadius.ResumeLayout(false);
            flowLayoutPanelOuterRadius.PerformLayout();
            flowLayoutPanelInnerRadius.ResumeLayout(false);
            flowLayoutPanelInnerRadius.PerformLayout();
            groupBoxHREMoption1.ResumeLayout(false);
            groupBoxHREMoption1.PerformLayout();
            flowLayoutPanelSpotCount.ResumeLayout(false);
            flowLayoutPanelSpotCount.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanelObjectiveAperture.ResumeLayout(false);
            flowLayoutPanelObjectiveAperture.PerformLayout();
            groupBoxTEMConditions.ResumeLayout(false);
            groupBoxTEMConditions.PerformLayout();
            contextMenuStripTEMcondition.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanelScherzer.ResumeLayout(false);
            flowLayoutPanelScherzer.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanelModeSelection.ResumeLayout(false);
            groupBoxImageMode.ResumeLayout(false);
            groupBoxImageMode.PerformLayout();
            flowLayoutPanelImageType.ResumeLayout(false);
            flowLayoutPanelImageType.PerformLayout();
            groupBoxSampleProperty.ResumeLayout(false);
            groupBoxSampleProperty.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panelSimulationActions.ResumeLayout(false);
            panelSimulationActions.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Crystallography.Controls.NumericBox numericBoxNumOfBlochWave;
        private Crystallography.Controls.NumericBox numericBoxObjAperRadius;
        private System.Windows.Forms.Button buttonSimulate;
        private Crystallography.Controls.NumericBox numericBoxAccVol;
        private Crystallography.Controls.NumericBox numericBoxDefocus;
        private Crystallography.Controls.NumericBox numericBoxCs;
        private Crystallography.Controls.NumericBox numericBoxHRTEM_BetaAgnle;
        private Crystallography.Controls.NumericBox numericBoxDeltaV;
        private Crystallography.Controls.NumericBox numericBoxCc;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxOpticalProperty;
        private System.Windows.Forms.GroupBox groupBoxSimulation;
        private System.Windows.Forms.RadioButton radioButtonSingleMode;
        private System.Windows.Forms.RadioButton radioButtonSerialMode;
        private Crystallography.Controls.NumericBox numericBoxDefocusStart;
        private Crystallography.Controls.NumericBox numericBoxDefocusStep;
        private Crystallography.Controls.NumericBox numericBoxDefocusNum;
        private System.Windows.Forms.TextBox textBoxDefocusList;
        private Crystallography.Controls.NumericBox numericBoxThicknessStart;
        private Crystallography.Controls.NumericBox numericBoxThicknessNum;
        private Crystallography.Controls.NumericBox numericBoxThicknessStep;
        private System.Windows.Forms.CheckBox checkBoxSerialDefocus;
        private System.Windows.Forms.CheckBox checkBoxSerialThickness;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBoxHREMoption2;
        private System.Windows.Forms.RadioButton radioButtonModeTransmissionCrossCoefficient;
        private System.Windows.Forms.RadioButton radioButtonModeQuasiCoherent;
        private System.Windows.Forms.CheckBox checkBoxShowLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private Crystallography.Controls.NumericBox numericBoxHRTEM_ObjAperY;
        private Crystallography.Controls.NumericBox numericBoxHRTEM_ObjAperX;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelScherzer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxScherzer;
        private System.Windows.Forms.GroupBox groupBoxHREMoption1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelObjectiveAperture;
        private System.Windows.Forms.TextBox textBoxObjAperRadius;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxShowUnitcell;
        private System.Windows.Forms.RadioButton radioButtonHorizontalThickness;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelHorizontalDirection;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButtonHorizontalDefocus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.CheckBox checkBoxShowScale;
        private Crystallography.Controls.NumericBox numericBoxScaleLength;
        private System.Windows.Forms.TextBox textBoxThicknessList;
        private Crystallography.Controls.NumericBox numericBoxLabelFontSize;
        private Crystallography.Controls.ColorControl colorControlLabel;
        private Crystallography.Controls.ColorControl colorControlScale;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSavePNG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveTIFF;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveMetafile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveIndividually;
        private System.Windows.Forms.ToolStripMenuItem copyImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyImage;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyMetafile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOverprintSymbols;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem loadTEMParameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTEMParametersToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSpotCount;
        private System.Windows.Forms.TextBox textBoxNumOfSpots;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonDetailsOfSpots;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsOfHRTEMSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem calculationLibraryToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxCaclulationLibrary;
        private System.Windows.Forms.CheckBox checkBoxOpenAperture;
        private System.Windows.Forms.GroupBox groupBoxImageProperty;
        private System.Windows.Forms.GroupBox groupBoxSerialImage;
        private System.Windows.Forms.Panel panelSerial;
        private System.Windows.Forms.Panel panelSerialDefocus;
        private System.Windows.Forms.Panel panelSerialThickness;
        private System.Windows.Forms.CheckBox checkBoxPotentialUg;
        private System.Windows.Forms.GroupBox groupBoxPotentialOption;
        private System.Windows.Forms.CheckBox checkBoxPotentialUgPrime;
        private System.Windows.Forms.RadioButton radioButtonPotentialModeRealAndImag;
        private System.Windows.Forms.RadioButton radioButtonPotentialModeMagAndPhase;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBoxPhaseScale;
        private System.Windows.Forms.Panel panelPhaseScale;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panelDisplaySettings;
        private Crystallography.Controls.TrackBarAdvanced trackBarAdvancedMax;
        private Crystallography.Controls.TrackBarAdvanced trackBarAdvancedMin;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.ComboBox comboBoxScaleColorScale;
        private System.Windows.Forms.CheckBox checkBoxGaussianBlur;
        private Crystallography.Controls.NumericBox numericBoxGaussianBlurRadius;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPotentialMode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMagAndPhase;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowMagAndPhase;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowMag;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowPhase;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRealAndImaiginary;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowRealAndImag;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowReal;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowImag;
        private System.Windows.Forms.Panel panelImageStatus;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.PictureBox pictureBoxScaleOfIntensity;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label labelMousePositionValue;
        private System.Windows.Forms.Label labelMousePositionY;
        private System.Windows.Forms.Label labelMousePositionX;
        private System.Windows.Forms.CheckBox checkBoxRealTimeSimulation;
        private System.Windows.Forms.GroupBox groupBoxTEMConditions;
        private System.Windows.Forms.GroupBox groupBoxDiffractedWaves;
        private NumericBox numericBoxIntensityMax;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelIntensityRange;
        private System.Windows.Forms.CheckBox checkBoxIntensityMin;
        private NumericBox numericBoxIntensityMin;
        private NumericBox numericBoxSTEM_ConvergenceAngle;
        private NumericBox numericBoxSTEM_DetectorOuterAngle;
        private NumericBox numericBoxSTEM_DetectorInnerAngle;
        private NumericBox numericBoxSTEM_AngleResolution;
        private System.Windows.Forms.GroupBox groupBoxSTEMoption2;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.CheckBox checkBoxCTF;
        private System.Windows.Forms.Label label4;
        private NumericBox numericBoxSTEM_SliceThicknessForInelastic;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelModeSelection;
        private System.Windows.Forms.GroupBox groupBoxImageMode;
        private System.Windows.Forms.RadioButton radioButtonProjectedPotential;
        private System.Windows.Forms.RadioButton radioButtonSTEM;
        private System.Windows.Forms.RadioButton radioButtonHRTEM;
        private System.Windows.Forms.GroupBox groupBoxSampleProperty;
        private NumericBox numericBoxThickness;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSimulationMode;
        private System.Windows.Forms.Panel panelModeOptions;
        private System.Windows.Forms.GroupBox groupBoxSTEMoption1;
        private System.Windows.Forms.GroupBox groupBoxNormalization;
        private System.Windows.Forms.CheckBox checkBoxNormarizeIndividually;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTEMcondition;
        private System.Windows.Forms.ToolStripMenuItem setoZeroDefocusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setoScherzerDefocusToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSTEM;
        private System.Windows.Forms.ToolStripMenuItem typicalBF02MradToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem typicalABF1224MradToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem typicalLAADF2560MradToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem typicalHAADF80250MradToolStripMenuItem;
        private NumericBox numericBoxSTEM_EffectiveSourceSize;
        private System.Windows.Forms.ToolStripMenuItem setAllAToolStripMenuItem;
        private System.Windows.Forms.Panel panelSerialSettings;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem presets1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presets2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presets3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presets4ToolStripMenuItem;
        private System.Windows.Forms.Panel panelSimulationActions;
        private System.Windows.Forms.CheckBox checkBoxIntensityMax;
        private System.Windows.Forms.GroupBox groupBoxSTEMoption3;
        private System.Windows.Forms.GroupBox groupBoxAdjust;
        private System.Windows.Forms.GroupBox groupBoxDisplay;
        private System.Windows.Forms.CheckBox checkBoxPreset;
        private System.Windows.Forms.RadioButton radioButtonSTEM_target_TDS;
        private System.Windows.Forms.RadioButton radioButtonSTEM_target_elas;
        private System.Windows.Forms.RadioButton radioButtonSTEM_target_both;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOuterRadius;
        private System.Windows.Forms.TextBox textBoxOuterRadius;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelInnerRadius;
        private System.Windows.Forms.TextBox textBoxInnerRadius;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelConvergenceRadius;
        private System.Windows.Forms.TextBox textBoxConvRadius;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelImageType;
        private System.Windows.Forms.Panel panelImageProperties;
        private NumericBox numericBoxResolution;
        // 260521Cl: numericBoxWidth/Height は sizeControl1 へ置換したため削除
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        // 260521Cl: flowLayoutPanel2 は sizeControl1 へ置換したため削除
        private SizeControl sizeControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel11;
    }
}
