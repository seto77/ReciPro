using System;

namespace ReciPro
{
    partial class FormImageSimulator
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
                scaleImage.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImageSimulator));
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            panel1 = new System.Windows.Forms.Panel();
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
            panel2 = new System.Windows.Forms.Panel();
            groupBox3 = new System.Windows.Forms.GroupBox();
            numericBoxGaussianBlurRadius = new NumericBox();
            trackBarAdvancedMax = new TrackBarAdvanced();
            trackBarAdvancedMin = new TrackBarAdvanced();
            checkBoxGaussianBlur = new System.Windows.Forms.CheckBox();
            label25 = new System.Windows.Forms.Label();
            comboBoxScaleColorScale = new System.Windows.Forms.ComboBox();
            panel7 = new System.Windows.Forms.Panel();
            groupBoxNormalization = new System.Windows.Forms.GroupBox();
            checkBoxNormarizeIndividually = new System.Windows.Forms.CheckBox();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxIntensityMin = new System.Windows.Forms.CheckBox();
            numericBoxIntensityMin = new NumericBox();
            checkBoxIntensityMax = new System.Windows.Forms.CheckBox();
            numericBoxIntensityMax = new NumericBox();
            panel9 = new System.Windows.Forms.Panel();
            groupBoxSTEMoption3 = new System.Windows.Forms.GroupBox();
            radioButtonSTEM_target_TDS = new System.Windows.Forms.RadioButton();
            radioButtonSTEM_target_elas = new System.Windows.Forms.RadioButton();
            radioButtonSTEM_target_both = new System.Windows.Forms.RadioButton();
            groupBox2 = new System.Windows.Forms.GroupBox();
            colorControlScale = new ColorControl();
            numericBoxScaleLength = new NumericBox();
            checkBoxShowScale = new System.Windows.Forms.CheckBox();
            colorControlLabel = new ColorControl();
            numericBoxLabelFontSize = new NumericBox();
            checkBoxShowLabel = new System.Windows.Forms.CheckBox();
            checkBoxShowUnitcell = new System.Windows.Forms.CheckBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            panel3 = new System.Windows.Forms.Panel();
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
            panel5 = new System.Windows.Forms.Panel();
            checkBoxSerialThickness = new System.Windows.Forms.CheckBox();
            checkBoxSerialDefocus = new System.Windows.Forms.CheckBox();
            flowLayoutPanelHorizontalDirection = new System.Windows.Forms.FlowLayoutPanel();
            label6 = new System.Windows.Forms.Label();
            radioButtonHorizontalDefocus = new System.Windows.Forms.RadioButton();
            radioButtonHorizontalThickness = new System.Windows.Forms.RadioButton();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonSingleMode = new System.Windows.Forms.RadioButton();
            radioButtonSerialMode = new System.Windows.Forms.RadioButton();
            groupBoxPotentialOption = new System.Windows.Forms.GroupBox();
            flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
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
            numericBoxSTEM_AngleResolution = new NumericBox();
            numericBoxSliceThicknessForInelasticSTEM = new NumericBox();
            flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxHREMoption2 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonModeQuasiCoherent = new System.Windows.Forms.RadioButton();
            radioButtonModeTransmissionCrossCoefficient = new System.Windows.Forms.RadioButton();
            label32 = new System.Windows.Forms.Label();
            panel6 = new System.Windows.Forms.Panel();
            groupBox8 = new System.Windows.Forms.GroupBox();
            numericBoxHeight = new NumericBox();
            numericBoxWidth = new NumericBox();
            numericBoxResolution = new NumericBox();
            panel8 = new System.Windows.Forms.Panel();
            groupBox7 = new System.Windows.Forms.GroupBox();
            numericBoxNumOfBlochWave = new NumericBox();
            groupBoxOpticalProperty = new System.Windows.Forms.GroupBox();
            groupBoxSTEMoption1 = new System.Windows.Forms.GroupBox();
            contextMenuStripSTEM = new System.Windows.Forms.ContextMenuStrip(components);
            typicalBF02MradToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            typicalABF1224MradToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            typicalLAADF2560MradToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            typicalHAADF80250MradToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            label5 = new System.Windows.Forms.Label();
            label34 = new System.Windows.Forms.Label();
            numericBoxSTEM_DetectorOuterAngle = new NumericBox();
            numericBoxEffectiveSourceSize = new NumericBox();
            numericBoxSTEM_ConvergenceAngle = new NumericBox();
            flowLayoutPanel15 = new System.Windows.Forms.FlowLayoutPanel();
            textBoxOuterRadius = new System.Windows.Forms.TextBox();
            label38 = new System.Windows.Forms.Label();
            flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            textBoxInnerRadius = new System.Windows.Forms.TextBox();
            label37 = new System.Windows.Forms.Label();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            textBoxConvRadius = new System.Windows.Forms.TextBox();
            label36 = new System.Windows.Forms.Label();
            numericBoxSTEM_DetectorInnerAngle = new NumericBox();
            label1 = new System.Windows.Forms.Label();
            groupBoxHREMoption1 = new System.Windows.Forms.GroupBox();
            checkBoxOpenAperture = new System.Windows.Forms.CheckBox();
            numericBoxObjAperX = new NumericBox();
            numericBoxObjAperRadius = new NumericBox();
            numericBoxObjAperY = new NumericBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            textBoxNumOfSpots = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            buttonDetailsOfSpots = new System.Windows.Forms.Button();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            textBoxObjAperRadius = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            groupBox4 = new System.Windows.Forms.GroupBox();
            contextMenuStripTEMcondition = new System.Windows.Forms.ContextMenuStrip(components);
            setoZeroDefocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            setoScherzerDefocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            setAllAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            presets1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            presets2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            presets3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            presets4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            label35 = new System.Windows.Forms.Label();
            checkBoxCTF = new System.Windows.Forms.CheckBox();
            numericBoxCc = new NumericBox();
            numericBoxDeltaV = new NumericBox();
            numericBoxBetaAgnle = new NumericBox();
            numericBoxCs = new NumericBox();
            numericBoxDefocus = new NumericBox();
            numericBoxAccVol = new NumericBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            label3 = new System.Windows.Forms.Label();
            textBoxScherzer = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            flowLayoutPanel14 = new System.Windows.Forms.FlowLayoutPanel();
            groupBox6 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel16 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonHRTEM = new System.Windows.Forms.RadioButton();
            radioButtonSTEM = new System.Windows.Forms.RadioButton();
            radioButtonProjectedPotential = new System.Windows.Forms.RadioButton();
            groupBoxSampleProperty = new System.Windows.Forms.GroupBox();
            numericBoxThickness = new NumericBox();
            panel4 = new System.Windows.Forms.Panel();
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
            readTEMParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxScaleOfIntensity).BeginInit();
            panel2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBoxNormalization.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            groupBoxSTEMoption3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            panel3.SuspendLayout();
            groupBoxSerialImage.SuspendLayout();
            panelSerial.SuspendLayout();
            panelSerialDefocus.SuspendLayout();
            panelSerialThickness.SuspendLayout();
            panel5.SuspendLayout();
            flowLayoutPanelHorizontalDirection.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            groupBoxPotentialOption.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            flowLayoutPanelMagAndPhase.SuspendLayout();
            panelPhaseScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhaseScale).BeginInit();
            flowLayoutPanelRealAndImaiginary.SuspendLayout();
            groupBoxSTEMoption2.SuspendLayout();
            groupBoxHREMoption2.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            panel6.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBoxOpticalProperty.SuspendLayout();
            groupBoxSTEMoption1.SuspendLayout();
            contextMenuStripSTEM.SuspendLayout();
            flowLayoutPanel15.SuspendLayout();
            flowLayoutPanel9.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            groupBoxHREMoption1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            groupBox4.SuspendLayout();
            contextMenuStripTEMcondition.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel14.SuspendLayout();
            groupBox6.SuspendLayout();
            flowLayoutPanel16.SuspendLayout();
            groupBoxSampleProperty.SuspendLayout();
            panel4.SuspendLayout();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(splitContainer1.Panel1, "splitContainer1.Panel1");
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel);
            splitContainer1.Panel1.Controls.Add(panel1);
            splitContainer1.Panel1.Controls.Add(panel2);
            splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            toolTip.SetToolTip(splitContainer1.Panel1, resources.GetString("splitContainer1.Panel1.ToolTip"));
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(splitContainer1.Panel2, "splitContainer1.Panel2");
            splitContainer1.Panel2.Controls.Add(groupBox1);
            splitContainer1.Panel2.Controls.Add(groupBoxOpticalProperty);
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel14);
            splitContainer1.Panel2.Controls.Add(panel4);
            splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            toolTip.SetToolTip(splitContainer1.Panel2, resources.GetString("splitContainer1.Panel2.ToolTip"));
            toolTip.SetToolTip(splitContainer1, resources.GetString("splitContainer1.ToolTip"));
            // 
            // tableLayoutPanel
            // 
            resources.ApplyResources(tableLayoutPanel, "tableLayoutPanel");
            tableLayoutPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            tableLayoutPanel.CausesValidation = false;
            tableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel.Name = "tableLayoutPanel";
            toolTip.SetToolTip(tableLayoutPanel, resources.GetString("tableLayoutPanel.ToolTip"));
            tableLayoutPanel.Enter += tableLayoutPanel_Enter;
            tableLayoutPanel.Leave += tableLayoutPanel_Leave;
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Controls.Add(pictureBoxScaleOfIntensity);
            panel1.Controls.Add(labelMousePositionValue);
            panel1.Controls.Add(label33);
            panel1.Controls.Add(labelMousePositionY);
            panel1.Controls.Add(labelMousePositionX);
            panel1.Controls.Add(label31);
            panel1.Controls.Add(label27);
            panel1.Controls.Add(label30);
            panel1.Controls.Add(label29);
            panel1.Controls.Add(label26);
            panel1.Controls.Add(label28);
            panel1.Name = "panel1";
            toolTip.SetToolTip(panel1, resources.GetString("panel1.ToolTip"));
            // 
            // pictureBoxScaleOfIntensity
            // 
            resources.ApplyResources(pictureBoxScaleOfIntensity, "pictureBoxScaleOfIntensity");
            pictureBoxScaleOfIntensity.Name = "pictureBoxScaleOfIntensity";
            pictureBoxScaleOfIntensity.TabStop = false;
            toolTip.SetToolTip(pictureBoxScaleOfIntensity, resources.GetString("pictureBoxScaleOfIntensity.ToolTip"));
            // 
            // labelMousePositionValue
            // 
            resources.ApplyResources(labelMousePositionValue, "labelMousePositionValue");
            labelMousePositionValue.Name = "labelMousePositionValue";
            toolTip.SetToolTip(labelMousePositionValue, resources.GetString("labelMousePositionValue.ToolTip"));
            // 
            // label33
            // 
            resources.ApplyResources(label33, "label33");
            label33.Name = "label33";
            toolTip.SetToolTip(label33, resources.GetString("label33.ToolTip"));
            // 
            // labelMousePositionY
            // 
            resources.ApplyResources(labelMousePositionY, "labelMousePositionY");
            labelMousePositionY.Name = "labelMousePositionY";
            toolTip.SetToolTip(labelMousePositionY, resources.GetString("labelMousePositionY.ToolTip"));
            // 
            // labelMousePositionX
            // 
            resources.ApplyResources(labelMousePositionX, "labelMousePositionX");
            labelMousePositionX.Name = "labelMousePositionX";
            toolTip.SetToolTip(labelMousePositionX, resources.GetString("labelMousePositionX.ToolTip"));
            // 
            // label31
            // 
            resources.ApplyResources(label31, "label31");
            label31.Name = "label31";
            toolTip.SetToolTip(label31, resources.GetString("label31.ToolTip"));
            // 
            // label27
            // 
            resources.ApplyResources(label27, "label27");
            label27.Name = "label27";
            toolTip.SetToolTip(label27, resources.GetString("label27.ToolTip"));
            // 
            // label30
            // 
            resources.ApplyResources(label30, "label30");
            label30.Name = "label30";
            toolTip.SetToolTip(label30, resources.GetString("label30.ToolTip"));
            // 
            // label29
            // 
            resources.ApplyResources(label29, "label29");
            label29.Name = "label29";
            toolTip.SetToolTip(label29, resources.GetString("label29.ToolTip"));
            // 
            // label26
            // 
            resources.ApplyResources(label26, "label26");
            label26.Name = "label26";
            toolTip.SetToolTip(label26, resources.GetString("label26.ToolTip"));
            // 
            // label28
            // 
            resources.ApplyResources(label28, "label28");
            label28.Name = "label28";
            toolTip.SetToolTip(label28, resources.GetString("label28.ToolTip"));
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Controls.Add(groupBox3);
            panel2.Controls.Add(panel7);
            panel2.Controls.Add(groupBoxNormalization);
            panel2.Controls.Add(panel9);
            panel2.Controls.Add(groupBoxSTEMoption3);
            panel2.Controls.Add(groupBox2);
            panel2.Name = "panel2";
            toolTip.SetToolTip(panel2, resources.GetString("panel2.ToolTip"));
            // 
            // groupBox3
            // 
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Controls.Add(numericBoxGaussianBlurRadius);
            groupBox3.Controls.Add(trackBarAdvancedMax);
            groupBox3.Controls.Add(trackBarAdvancedMin);
            groupBox3.Controls.Add(checkBoxGaussianBlur);
            groupBox3.Controls.Add(label25);
            groupBox3.Controls.Add(comboBoxScaleColorScale);
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            toolTip.SetToolTip(groupBox3, resources.GetString("groupBox3.ToolTip"));
            // 
            // numericBoxGaussianBlurRadius
            // 
            resources.ApplyResources(numericBoxGaussianBlurRadius, "numericBoxGaussianBlurRadius");
            numericBoxGaussianBlurRadius.BackColor = System.Drawing.SystemColors.Control;
            numericBoxGaussianBlurRadius.DecimalPlaces = 1;
            numericBoxGaussianBlurRadius.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxGaussianBlurRadius.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxGaussianBlurRadius.Maximum = 100D;
            numericBoxGaussianBlurRadius.Minimum = 0D;
            numericBoxGaussianBlurRadius.Name = "numericBoxGaussianBlurRadius";
            numericBoxGaussianBlurRadius.RadianValue = 0.3490658503988659D;
            numericBoxGaussianBlurRadius.RoundErrorAccuracy = -1;
            numericBoxGaussianBlurRadius.ShowUpDown = true;
            numericBoxGaussianBlurRadius.SkipEventDuringInput = false;
            numericBoxGaussianBlurRadius.SmartIncrement = true;
            numericBoxGaussianBlurRadius.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxGaussianBlurRadius, resources.GetString("numericBoxGaussianBlurRadius.ToolTip"));
            numericBoxGaussianBlurRadius.Value = 20D;
            numericBoxGaussianBlurRadius.ValueChanged += CheckBoxGaussianBlur_CheckedChanged;
            // 
            // trackBarAdvancedMax
            // 
            resources.ApplyResources(trackBarAdvancedMax, "trackBarAdvancedMax");
            trackBarAdvancedMax.ControlHeight = 23;
            trackBarAdvancedMax.DecimalPlaces = -1;
            trackBarAdvancedMax.LogScrollBar = false;
            trackBarAdvancedMax.Maximum = 1D;
            trackBarAdvancedMax.Minimum = 0D;
            trackBarAdvancedMax.Name = "trackBarAdvancedMax";
            trackBarAdvancedMax.NumericBoxSize = 95;
            trackBarAdvancedMax.Orientation = System.Windows.Forms.Orientation.Vertical;
            trackBarAdvancedMax.Smart_Increment = true;
            trackBarAdvancedMax.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            toolTip.SetToolTip(trackBarAdvancedMax, resources.GetString("trackBarAdvancedMax.ToolTip"));
            trackBarAdvancedMax.UpDown_Increment = 0.01D;
            trackBarAdvancedMax.Value = 1D;
            trackBarAdvancedMax.ValueChanged += TrackBarAdvancedMin_ValueChanged;
            // 
            // trackBarAdvancedMin
            // 
            resources.ApplyResources(trackBarAdvancedMin, "trackBarAdvancedMin");
            trackBarAdvancedMin.ControlHeight = 23;
            trackBarAdvancedMin.DecimalPlaces = -1;
            trackBarAdvancedMin.LogScrollBar = false;
            trackBarAdvancedMin.Maximum = 65535D;
            trackBarAdvancedMin.Minimum = 0D;
            trackBarAdvancedMin.Name = "trackBarAdvancedMin";
            trackBarAdvancedMin.NumericBoxSize = 95;
            trackBarAdvancedMin.Orientation = System.Windows.Forms.Orientation.Vertical;
            trackBarAdvancedMin.Smart_Increment = true;
            trackBarAdvancedMin.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            toolTip.SetToolTip(trackBarAdvancedMin, resources.GetString("trackBarAdvancedMin.ToolTip"));
            trackBarAdvancedMin.UpDown_Increment = 1D;
            trackBarAdvancedMin.Value = 0D;
            trackBarAdvancedMin.ValueChanged += TrackBarAdvancedMin_ValueChanged;
            // 
            // checkBoxGaussianBlur
            // 
            resources.ApplyResources(checkBoxGaussianBlur, "checkBoxGaussianBlur");
            checkBoxGaussianBlur.Name = "checkBoxGaussianBlur";
            toolTip.SetToolTip(checkBoxGaussianBlur, resources.GetString("checkBoxGaussianBlur.ToolTip"));
            checkBoxGaussianBlur.UseVisualStyleBackColor = true;
            checkBoxGaussianBlur.CheckedChanged += CheckBoxGaussianBlur_CheckedChanged;
            // 
            // label25
            // 
            resources.ApplyResources(label25, "label25");
            label25.Name = "label25";
            toolTip.SetToolTip(label25, resources.GetString("label25.ToolTip"));
            // 
            // comboBoxScaleColorScale
            // 
            resources.ApplyResources(comboBoxScaleColorScale, "comboBoxScaleColorScale");
            comboBoxScaleColorScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScaleColorScale.FormattingEnabled = true;
            comboBoxScaleColorScale.Items.AddRange(new object[] { resources.GetString("comboBoxScaleColorScale.Items"), resources.GetString("comboBoxScaleColorScale.Items1") });
            comboBoxScaleColorScale.Name = "comboBoxScaleColorScale";
            toolTip.SetToolTip(comboBoxScaleColorScale, resources.GetString("comboBoxScaleColorScale.ToolTip"));
            comboBoxScaleColorScale.SelectedIndexChanged += ComboBoxScaleColorScale_SelectedIndexChanged;
            // 
            // panel7
            // 
            resources.ApplyResources(panel7, "panel7");
            panel7.Name = "panel7";
            toolTip.SetToolTip(panel7, resources.GetString("panel7.ToolTip"));
            // 
            // groupBoxNormalization
            // 
            resources.ApplyResources(groupBoxNormalization, "groupBoxNormalization");
            groupBoxNormalization.Controls.Add(checkBoxNormarizeIndividually);
            groupBoxNormalization.Controls.Add(flowLayoutPanel6);
            groupBoxNormalization.Name = "groupBoxNormalization";
            groupBoxNormalization.TabStop = false;
            toolTip.SetToolTip(groupBoxNormalization, resources.GetString("groupBoxNormalization.ToolTip"));
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
            // flowLayoutPanel6
            // 
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Controls.Add(checkBoxIntensityMin);
            flowLayoutPanel6.Controls.Add(numericBoxIntensityMin);
            flowLayoutPanel6.Controls.Add(checkBoxIntensityMax);
            flowLayoutPanel6.Controls.Add(numericBoxIntensityMax);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            toolTip.SetToolTip(flowLayoutPanel6, resources.GetString("flowLayoutPanel6.ToolTip"));
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
            resources.ApplyResources(numericBoxIntensityMin, "numericBoxIntensityMin");
            numericBoxIntensityMin.BackColor = System.Drawing.SystemColors.Control;
            numericBoxIntensityMin.DecimalPlaces = 0;
            numericBoxIntensityMin.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxIntensityMin.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxIntensityMin.Maximum = 65535D;
            numericBoxIntensityMin.Minimum = 0D;
            numericBoxIntensityMin.Name = "numericBoxIntensityMin";
            numericBoxIntensityMin.RoundErrorAccuracy = -1;
            numericBoxIntensityMin.ShowUpDown = true;
            numericBoxIntensityMin.SmartIncrement = true;
            numericBoxIntensityMin.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxIntensityMin, resources.GetString("numericBoxIntensityMin.ToolTip"));
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
            resources.ApplyResources(numericBoxIntensityMax, "numericBoxIntensityMax");
            numericBoxIntensityMax.BackColor = System.Drawing.SystemColors.Control;
            numericBoxIntensityMax.DecimalPlaces = 0;
            numericBoxIntensityMax.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxIntensityMax.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxIntensityMax.Maximum = 65535D;
            numericBoxIntensityMax.Minimum = 1D;
            numericBoxIntensityMax.Name = "numericBoxIntensityMax";
            numericBoxIntensityMax.RadianValue = 0.017453292519943295D;
            numericBoxIntensityMax.RoundErrorAccuracy = -1;
            numericBoxIntensityMax.ShowUpDown = true;
            numericBoxIntensityMax.SmartIncrement = true;
            numericBoxIntensityMax.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxIntensityMax, resources.GetString("numericBoxIntensityMax.ToolTip"));
            numericBoxIntensityMax.Value = 1D;
            numericBoxIntensityMax.ValueChanged += checkBoxIntensityMin_CheckedChanged;
            // 
            // panel9
            // 
            resources.ApplyResources(panel9, "panel9");
            panel9.Name = "panel9";
            toolTip.SetToolTip(panel9, resources.GetString("panel9.ToolTip"));
            // 
            // groupBoxSTEMoption3
            // 
            resources.ApplyResources(groupBoxSTEMoption3, "groupBoxSTEMoption3");
            groupBoxSTEMoption3.Controls.Add(radioButtonSTEM_target_TDS);
            groupBoxSTEMoption3.Controls.Add(radioButtonSTEM_target_elas);
            groupBoxSTEMoption3.Controls.Add(radioButtonSTEM_target_both);
            groupBoxSTEMoption3.Name = "groupBoxSTEMoption3";
            groupBoxSTEMoption3.TabStop = false;
            toolTip.SetToolTip(groupBoxSTEMoption3, resources.GetString("groupBoxSTEMoption3.ToolTip"));
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
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(colorControlScale);
            groupBox2.Controls.Add(numericBoxScaleLength);
            groupBox2.Controls.Add(checkBoxShowScale);
            groupBox2.Controls.Add(colorControlLabel);
            groupBox2.Controls.Add(numericBoxLabelFontSize);
            groupBox2.Controls.Add(checkBoxShowLabel);
            groupBox2.Controls.Add(checkBoxShowUnitcell);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            toolTip.SetToolTip(groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // colorControlScale
            // 
            resources.ApplyResources(colorControlScale, "colorControlScale");
            colorControlScale.Argb = -7877126;
            colorControlScale.Blue = 250;
            colorControlScale.BlueF = 0.980392158F;
            colorControlScale.BoxSize = new System.Drawing.Size(20, 20);
            colorControlScale.Color = System.Drawing.Color.FromArgb(135, 205, 250);
            colorControlScale.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlScale.Green = 205;
            colorControlScale.GreenF = 0.8039216F;
            colorControlScale.Name = "colorControlScale";
            colorControlScale.Red = 135;
            colorControlScale.RedF = 0.5294118F;
            toolTip.SetToolTip(colorControlScale, resources.GetString("colorControlScale.ToolTip"));
            colorControlScale.ColorChanged += CheckBoxShowLabel_CheckedChanged;
            // 
            // numericBoxScaleLength
            // 
            resources.ApplyResources(numericBoxScaleLength, "numericBoxScaleLength");
            numericBoxScaleLength.BackColor = System.Drawing.SystemColors.Control;
            numericBoxScaleLength.DecimalPlaces = 1;
            numericBoxScaleLength.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxScaleLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxScaleLength.Maximum = 100D;
            numericBoxScaleLength.Minimum = 0.2D;
            numericBoxScaleLength.Name = "numericBoxScaleLength";
            numericBoxScaleLength.RadianValue = 0.0087266462599716477D;
            numericBoxScaleLength.RoundErrorAccuracy = -1;
            numericBoxScaleLength.ShowUpDown = true;
            numericBoxScaleLength.SkipEventDuringInput = false;
            numericBoxScaleLength.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxScaleLength, resources.GetString("numericBoxScaleLength.ToolTip"));
            numericBoxScaleLength.UpDown_Increment = 0.2D;
            numericBoxScaleLength.Value = 0.5D;
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
            colorControlLabel.Argb = -5374161;
            colorControlLabel.Blue = 47;
            colorControlLabel.BlueF = 0.184313729F;
            colorControlLabel.BoxSize = new System.Drawing.Size(20, 20);
            colorControlLabel.Color = System.Drawing.Color.FromArgb(173, 255, 47);
            colorControlLabel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlLabel.Green = 255;
            colorControlLabel.GreenF = 1F;
            colorControlLabel.Name = "colorControlLabel";
            colorControlLabel.Red = 173;
            colorControlLabel.RedF = 0.6784314F;
            toolTip.SetToolTip(colorControlLabel, resources.GetString("colorControlLabel.ToolTip"));
            colorControlLabel.ColorChanged += CheckBoxShowLabel_CheckedChanged;
            // 
            // numericBoxLabelFontSize
            // 
            resources.ApplyResources(numericBoxLabelFontSize, "numericBoxLabelFontSize");
            numericBoxLabelFontSize.BackColor = System.Drawing.SystemColors.Control;
            numericBoxLabelFontSize.DecimalPlaces = 0;
            numericBoxLabelFontSize.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxLabelFontSize.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxLabelFontSize.Maximum = 20D;
            numericBoxLabelFontSize.Minimum = 1D;
            numericBoxLabelFontSize.Name = "numericBoxLabelFontSize";
            numericBoxLabelFontSize.RadianValue = 0.15707963267948966D;
            numericBoxLabelFontSize.RoundErrorAccuracy = -1;
            numericBoxLabelFontSize.ShowUpDown = true;
            numericBoxLabelFontSize.SkipEventDuringInput = false;
            numericBoxLabelFontSize.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxLabelFontSize, resources.GetString("numericBoxLabelFontSize.ToolTip"));
            numericBoxLabelFontSize.Value = 9D;
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
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(panel3);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            toolTip.SetToolTip(groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // panel3
            // 
            resources.ApplyResources(panel3, "panel3");
            panel3.Controls.Add(groupBoxSerialImage);
            panel3.Controls.Add(groupBoxPotentialOption);
            panel3.Controls.Add(groupBoxSTEMoption2);
            panel3.Controls.Add(groupBoxHREMoption2);
            panel3.Controls.Add(panel6);
            panel3.Name = "panel3";
            toolTip.SetToolTip(panel3, resources.GetString("panel3.ToolTip"));
            // 
            // groupBoxSerialImage
            // 
            resources.ApplyResources(groupBoxSerialImage, "groupBoxSerialImage");
            groupBoxSerialImage.Controls.Add(panelSerial);
            groupBoxSerialImage.Controls.Add(flowLayoutPanel4);
            groupBoxSerialImage.Name = "groupBoxSerialImage";
            groupBoxSerialImage.TabStop = false;
            toolTip.SetToolTip(groupBoxSerialImage, resources.GetString("groupBoxSerialImage.ToolTip"));
            // 
            // panelSerial
            // 
            resources.ApplyResources(panelSerial, "panelSerial");
            panelSerial.Controls.Add(panelSerialDefocus);
            panelSerial.Controls.Add(panelSerialThickness);
            panelSerial.Controls.Add(panel5);
            panelSerial.Controls.Add(flowLayoutPanelHorizontalDirection);
            panelSerial.Name = "panelSerial";
            toolTip.SetToolTip(panelSerial, resources.GetString("panelSerial.ToolTip"));
            // 
            // panelSerialDefocus
            // 
            resources.ApplyResources(panelSerialDefocus, "panelSerialDefocus");
            panelSerialDefocus.Controls.Add(numericBoxDefocusNum);
            panelSerialDefocus.Controls.Add(numericBoxDefocusStep);
            panelSerialDefocus.Controls.Add(numericBoxDefocusStart);
            panelSerialDefocus.Controls.Add(textBoxDefocusList);
            panelSerialDefocus.Name = "panelSerialDefocus";
            toolTip.SetToolTip(panelSerialDefocus, resources.GetString("panelSerialDefocus.ToolTip"));
            // 
            // numericBoxDefocusNum
            // 
            resources.ApplyResources(numericBoxDefocusNum, "numericBoxDefocusNum");
            numericBoxDefocusNum.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusNum.DecimalPlaces = 0;
            numericBoxDefocusNum.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusNum.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusNum.Maximum = 20D;
            numericBoxDefocusNum.Minimum = 1D;
            numericBoxDefocusNum.Name = "numericBoxDefocusNum";
            numericBoxDefocusNum.RadianValue = 0.069813170079773182D;
            numericBoxDefocusNum.RoundErrorAccuracy = -1;
            numericBoxDefocusNum.ShowUpDown = true;
            numericBoxDefocusNum.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxDefocusNum, resources.GetString("numericBoxDefocusNum.ToolTip"));
            numericBoxDefocusNum.Value = 4D;
            numericBoxDefocusNum.ValueChanged += NumericBoxDefocusSerial_ValueChanged;
            // 
            // numericBoxDefocusStep
            // 
            resources.ApplyResources(numericBoxDefocusStep, "numericBoxDefocusStep");
            numericBoxDefocusStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusStep.DecimalPlaces = 1;
            numericBoxDefocusStep.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusStep.Maximum = 100D;
            numericBoxDefocusStep.Minimum = -100D;
            numericBoxDefocusStep.Name = "numericBoxDefocusStep";
            numericBoxDefocusStep.RadianValue = -0.3490658503988659D;
            numericBoxDefocusStep.RoundErrorAccuracy = -1;
            numericBoxDefocusStep.ShowUpDown = true;
            numericBoxDefocusStep.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxDefocusStep, resources.GetString("numericBoxDefocusStep.ToolTip"));
            numericBoxDefocusStep.UpDown_Increment = 10D;
            numericBoxDefocusStep.Value = -20D;
            numericBoxDefocusStep.ValueChanged += NumericBoxDefocusSerial_ValueChanged;
            // 
            // numericBoxDefocusStart
            // 
            resources.ApplyResources(numericBoxDefocusStart, "numericBoxDefocusStart");
            numericBoxDefocusStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusStart.DecimalPlaces = 1;
            numericBoxDefocusStart.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusStart.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocusStart.Maximum = 1000D;
            numericBoxDefocusStart.Minimum = -1000D;
            numericBoxDefocusStart.Name = "numericBoxDefocusStart";
            numericBoxDefocusStart.RadianValue = -1.2217304763960306D;
            numericBoxDefocusStart.RoundErrorAccuracy = -1;
            numericBoxDefocusStart.ShowUpDown = true;
            numericBoxDefocusStart.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxDefocusStart, resources.GetString("numericBoxDefocusStart.ToolTip"));
            numericBoxDefocusStart.UpDown_Increment = 10D;
            numericBoxDefocusStart.Value = -70D;
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
            resources.ApplyResources(panelSerialThickness, "panelSerialThickness");
            panelSerialThickness.Controls.Add(numericBoxThicknessNum);
            panelSerialThickness.Controls.Add(numericBoxThicknessStep);
            panelSerialThickness.Controls.Add(numericBoxThicknessStart);
            panelSerialThickness.Controls.Add(textBoxThicknessList);
            panelSerialThickness.Name = "panelSerialThickness";
            toolTip.SetToolTip(panelSerialThickness, resources.GetString("panelSerialThickness.ToolTip"));
            // 
            // numericBoxThicknessNum
            // 
            resources.ApplyResources(numericBoxThicknessNum, "numericBoxThicknessNum");
            numericBoxThicknessNum.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessNum.DecimalPlaces = 0;
            numericBoxThicknessNum.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessNum.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessNum.Maximum = 20D;
            numericBoxThicknessNum.Minimum = 0.1D;
            numericBoxThicknessNum.Name = "numericBoxThicknessNum";
            numericBoxThicknessNum.RadianValue = 0.069813170079773182D;
            numericBoxThicknessNum.RoundErrorAccuracy = -1;
            numericBoxThicknessNum.ShowUpDown = true;
            numericBoxThicknessNum.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessNum, resources.GetString("numericBoxThicknessNum.ToolTip"));
            numericBoxThicknessNum.Value = 4D;
            numericBoxThicknessNum.ValueChanged += NumericBoxThicknessSerial_ValueChanged;
            // 
            // numericBoxThicknessStep
            // 
            resources.ApplyResources(numericBoxThicknessStep, "numericBoxThicknessStep");
            numericBoxThicknessStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.DecimalPlaces = 1;
            numericBoxThicknessStep.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.Maximum = 1000D;
            numericBoxThicknessStep.Minimum = 1D;
            numericBoxThicknessStep.Name = "numericBoxThicknessStep";
            numericBoxThicknessStep.RadianValue = 0.3490658503988659D;
            numericBoxThicknessStep.RoundErrorAccuracy = -1;
            numericBoxThicknessStep.ShowUpDown = true;
            numericBoxThicknessStep.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessStep, resources.GetString("numericBoxThicknessStep.ToolTip"));
            numericBoxThicknessStep.UpDown_Increment = 10D;
            numericBoxThicknessStep.Value = 20D;
            numericBoxThicknessStep.ValueChanged += NumericBoxThicknessSerial_ValueChanged;
            // 
            // numericBoxThicknessStart
            // 
            resources.ApplyResources(numericBoxThicknessStart, "numericBoxThicknessStart");
            numericBoxThicknessStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.DecimalPlaces = 1;
            numericBoxThicknessStart.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.Maximum = 1000D;
            numericBoxThicknessStart.Minimum = 0.1D;
            numericBoxThicknessStart.Name = "numericBoxThicknessStart";
            numericBoxThicknessStart.RadianValue = 0.3490658503988659D;
            numericBoxThicknessStart.RoundErrorAccuracy = -1;
            numericBoxThicknessStart.ShowUpDown = true;
            numericBoxThicknessStart.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessStart, resources.GetString("numericBoxThicknessStart.ToolTip"));
            numericBoxThicknessStart.UpDown_Increment = 10D;
            numericBoxThicknessStart.Value = 20D;
            numericBoxThicknessStart.ValueChanged += NumericBoxThicknessSerial_ValueChanged;
            // 
            // textBoxThicknessList
            // 
            resources.ApplyResources(textBoxThicknessList, "textBoxThicknessList");
            textBoxThicknessList.Name = "textBoxThicknessList";
            toolTip.SetToolTip(textBoxThicknessList, resources.GetString("textBoxThicknessList.ToolTip"));
            // 
            // panel5
            // 
            resources.ApplyResources(panel5, "panel5");
            panel5.Controls.Add(checkBoxSerialThickness);
            panel5.Controls.Add(checkBoxSerialDefocus);
            panel5.Name = "panel5";
            toolTip.SetToolTip(panel5, resources.GetString("panel5.ToolTip"));
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
            toolTip.SetToolTip(flowLayoutPanelHorizontalDirection, resources.GetString("flowLayoutPanelHorizontalDirection.ToolTip"));
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
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(radioButtonSingleMode);
            flowLayoutPanel4.Controls.Add(radioButtonSerialMode);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            toolTip.SetToolTip(flowLayoutPanel4, resources.GetString("flowLayoutPanel4.ToolTip"));
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
            groupBoxPotentialOption.Controls.Add(flowLayoutPanel11);
            groupBoxPotentialOption.Controls.Add(checkBoxPotentialUgPrime);
            groupBoxPotentialOption.Controls.Add(checkBoxPotentialUg);
            groupBoxPotentialOption.Name = "groupBoxPotentialOption";
            groupBoxPotentialOption.TabStop = false;
            toolTip.SetToolTip(groupBoxPotentialOption, resources.GetString("groupBoxPotentialOption.ToolTip"));
            // 
            // flowLayoutPanel11
            // 
            resources.ApplyResources(flowLayoutPanel11, "flowLayoutPanel11");
            flowLayoutPanel11.Controls.Add(radioButtonPotentialModeMagAndPhase);
            flowLayoutPanel11.Controls.Add(flowLayoutPanelMagAndPhase);
            flowLayoutPanel11.Controls.Add(panelPhaseScale);
            flowLayoutPanel11.Controls.Add(radioButtonPotentialModeRealAndImag);
            flowLayoutPanel11.Controls.Add(flowLayoutPanelRealAndImaiginary);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            toolTip.SetToolTip(flowLayoutPanel11, resources.GetString("flowLayoutPanel11.ToolTip"));
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
            toolTip.SetToolTip(flowLayoutPanelMagAndPhase, resources.GetString("flowLayoutPanelMagAndPhase.ToolTip"));
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
            toolTip.SetToolTip(panelPhaseScale, resources.GetString("panelPhaseScale.ToolTip"));
            // 
            // label24
            // 
            resources.ApplyResources(label24, "label24");
            label24.Name = "label24";
            toolTip.SetToolTip(label24, resources.GetString("label24.ToolTip"));
            // 
            // label23
            // 
            resources.ApplyResources(label23, "label23");
            label23.Name = "label23";
            toolTip.SetToolTip(label23, resources.GetString("label23.ToolTip"));
            // 
            // label22
            // 
            resources.ApplyResources(label22, "label22");
            label22.Name = "label22";
            toolTip.SetToolTip(label22, resources.GetString("label22.ToolTip"));
            // 
            // label21
            // 
            resources.ApplyResources(label21, "label21");
            label21.Name = "label21";
            toolTip.SetToolTip(label21, resources.GetString("label21.ToolTip"));
            // 
            // label20
            // 
            resources.ApplyResources(label20, "label20");
            label20.Name = "label20";
            toolTip.SetToolTip(label20, resources.GetString("label20.ToolTip"));
            // 
            // label19
            // 
            resources.ApplyResources(label19, "label19");
            label19.Name = "label19";
            toolTip.SetToolTip(label19, resources.GetString("label19.ToolTip"));
            // 
            // label18
            // 
            resources.ApplyResources(label18, "label18");
            label18.Name = "label18";
            toolTip.SetToolTip(label18, resources.GetString("label18.ToolTip"));
            // 
            // pictureBoxPhaseScale
            // 
            resources.ApplyResources(pictureBoxPhaseScale, "pictureBoxPhaseScale");
            pictureBoxPhaseScale.Name = "pictureBoxPhaseScale";
            pictureBoxPhaseScale.TabStop = false;
            toolTip.SetToolTip(pictureBoxPhaseScale, resources.GetString("pictureBoxPhaseScale.ToolTip"));
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            toolTip.SetToolTip(label17, resources.GetString("label17.ToolTip"));
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            label16.Name = "label16";
            toolTip.SetToolTip(label16, resources.GetString("label16.ToolTip"));
            // 
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            label15.Name = "label15";
            toolTip.SetToolTip(label15, resources.GetString("label15.ToolTip"));
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            label14.Name = "label14";
            toolTip.SetToolTip(label14, resources.GetString("label14.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip"));
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            label12.Name = "label12";
            toolTip.SetToolTip(label12, resources.GetString("label12.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            toolTip.SetToolTip(label10, resources.GetString("label10.ToolTip"));
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
            toolTip.SetToolTip(flowLayoutPanelRealAndImaiginary, resources.GetString("flowLayoutPanelRealAndImaiginary.ToolTip"));
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
            groupBoxSTEMoption2.Controls.Add(numericBoxSTEM_AngleResolution);
            groupBoxSTEMoption2.Controls.Add(numericBoxSliceThicknessForInelasticSTEM);
            groupBoxSTEMoption2.Controls.Add(flowLayoutPanel10);
            groupBoxSTEMoption2.Name = "groupBoxSTEMoption2";
            groupBoxSTEMoption2.TabStop = false;
            toolTip.SetToolTip(groupBoxSTEMoption2, resources.GetString("groupBoxSTEMoption2.ToolTip"));
            // 
            // numericBoxSTEM_AngleResolution
            // 
            resources.ApplyResources(numericBoxSTEM_AngleResolution, "numericBoxSTEM_AngleResolution");
            numericBoxSTEM_AngleResolution.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_AngleResolution.DecimalPlaces = 3;
            numericBoxSTEM_AngleResolution.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_AngleResolution.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_AngleResolution.Maximum = 1D;
            numericBoxSTEM_AngleResolution.Minimum = 0.001D;
            numericBoxSTEM_AngleResolution.Name = "numericBoxSTEM_AngleResolution";
            numericBoxSTEM_AngleResolution.RadianValue = 0.0069813170079773184D;
            numericBoxSTEM_AngleResolution.RoundErrorAccuracy = -1;
            numericBoxSTEM_AngleResolution.ShowUpDown = true;
            numericBoxSTEM_AngleResolution.SmartIncrement = true;
            numericBoxSTEM_AngleResolution.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxSTEM_AngleResolution, resources.GetString("numericBoxSTEM_AngleResolution.ToolTip"));
            numericBoxSTEM_AngleResolution.Value = 0.4D;
            // 
            // numericBoxSliceThicknessForInelasticSTEM
            // 
            resources.ApplyResources(numericBoxSliceThicknessForInelasticSTEM, "numericBoxSliceThicknessForInelasticSTEM");
            numericBoxSliceThicknessForInelasticSTEM.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSliceThicknessForInelasticSTEM.DecimalPlaces = 1;
            numericBoxSliceThicknessForInelasticSTEM.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxSliceThicknessForInelasticSTEM.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxSliceThicknessForInelasticSTEM.Maximum = 10D;
            numericBoxSliceThicknessForInelasticSTEM.Minimum = 0.1D;
            numericBoxSliceThicknessForInelasticSTEM.Name = "numericBoxSliceThicknessForInelasticSTEM";
            numericBoxSliceThicknessForInelasticSTEM.RadianValue = 0.017453292519943295D;
            numericBoxSliceThicknessForInelasticSTEM.RoundErrorAccuracy = -1;
            numericBoxSliceThicknessForInelasticSTEM.ShowUpDown = true;
            numericBoxSliceThicknessForInelasticSTEM.SmartIncrement = true;
            numericBoxSliceThicknessForInelasticSTEM.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxSliceThicknessForInelasticSTEM, resources.GetString("numericBoxSliceThicknessForInelasticSTEM.ToolTip"));
            numericBoxSliceThicknessForInelasticSTEM.Value = 1D;
            // 
            // flowLayoutPanel10
            // 
            resources.ApplyResources(flowLayoutPanel10, "flowLayoutPanel10");
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            toolTip.SetToolTip(flowLayoutPanel10, resources.GetString("flowLayoutPanel10.ToolTip"));
            // 
            // groupBoxHREMoption2
            // 
            resources.ApplyResources(groupBoxHREMoption2, "groupBoxHREMoption2");
            groupBoxHREMoption2.Controls.Add(flowLayoutPanel8);
            groupBoxHREMoption2.Controls.Add(label32);
            groupBoxHREMoption2.Name = "groupBoxHREMoption2";
            groupBoxHREMoption2.TabStop = false;
            toolTip.SetToolTip(groupBoxHREMoption2, resources.GetString("groupBoxHREMoption2.ToolTip"));
            // 
            // flowLayoutPanel8
            // 
            resources.ApplyResources(flowLayoutPanel8, "flowLayoutPanel8");
            flowLayoutPanel8.Controls.Add(radioButtonModeQuasiCoherent);
            flowLayoutPanel8.Controls.Add(radioButtonModeTransmissionCrossCoefficient);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            toolTip.SetToolTip(flowLayoutPanel8, resources.GetString("flowLayoutPanel8.ToolTip"));
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
            // label32
            // 
            resources.ApplyResources(label32, "label32");
            label32.ForeColor = System.Drawing.Color.Black;
            label32.Name = "label32";
            toolTip.SetToolTip(label32, resources.GetString("label32.ToolTip"));
            // 
            // panel6
            // 
            resources.ApplyResources(panel6, "panel6");
            panel6.Controls.Add(groupBox8);
            panel6.Controls.Add(panel8);
            panel6.Controls.Add(groupBox7);
            panel6.Name = "panel6";
            toolTip.SetToolTip(panel6, resources.GetString("panel6.ToolTip"));
            // 
            // groupBox8
            // 
            resources.ApplyResources(groupBox8, "groupBox8");
            groupBox8.Controls.Add(numericBoxHeight);
            groupBox8.Controls.Add(numericBoxWidth);
            groupBox8.Controls.Add(numericBoxResolution);
            groupBox8.Name = "groupBox8";
            groupBox8.TabStop = false;
            toolTip.SetToolTip(groupBox8, resources.GetString("groupBox8.ToolTip"));
            // 
            // numericBoxHeight
            // 
            resources.ApplyResources(numericBoxHeight, "numericBoxHeight");
            numericBoxHeight.BackColor = System.Drawing.SystemColors.Control;
            numericBoxHeight.DecimalPlaces = 0;
            numericBoxHeight.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxHeight.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxHeight.Maximum = 2048D;
            numericBoxHeight.Minimum = 8D;
            numericBoxHeight.Name = "numericBoxHeight";
            numericBoxHeight.RadianValue = 8.9360857702109673D;
            numericBoxHeight.RoundErrorAccuracy = -1;
            numericBoxHeight.ShowUpDown = true;
            numericBoxHeight.SmartIncrement = true;
            numericBoxHeight.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxHeight, resources.GetString("numericBoxHeight.ToolTip"));
            numericBoxHeight.Value = 512D;
            // 
            // numericBoxWidth
            // 
            resources.ApplyResources(numericBoxWidth, "numericBoxWidth");
            numericBoxWidth.BackColor = System.Drawing.SystemColors.Control;
            numericBoxWidth.DecimalPlaces = 0;
            numericBoxWidth.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxWidth.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxWidth.Maximum = 2048D;
            numericBoxWidth.Minimum = 8D;
            numericBoxWidth.Name = "numericBoxWidth";
            numericBoxWidth.RadianValue = 8.9360857702109673D;
            numericBoxWidth.RoundErrorAccuracy = -1;
            numericBoxWidth.ShowUpDown = true;
            numericBoxWidth.SmartIncrement = true;
            numericBoxWidth.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxWidth, resources.GetString("numericBoxWidth.ToolTip"));
            numericBoxWidth.Value = 512D;
            // 
            // numericBoxResolution
            // 
            resources.ApplyResources(numericBoxResolution, "numericBoxResolution");
            numericBoxResolution.BackColor = System.Drawing.SystemColors.Control;
            numericBoxResolution.DecimalPlaces = 3;
            numericBoxResolution.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxResolution.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxResolution.Maximum = 100D;
            numericBoxResolution.Minimum = 0.01D;
            numericBoxResolution.Name = "numericBoxResolution";
            numericBoxResolution.RadianValue = 0.034906585039886591D;
            numericBoxResolution.RoundErrorAccuracy = -1;
            numericBoxResolution.ShowUpDown = true;
            numericBoxResolution.SmartIncrement = true;
            numericBoxResolution.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxResolution, resources.GetString("numericBoxResolution.ToolTip"));
            numericBoxResolution.Value = 2D;
            // 
            // panel8
            // 
            resources.ApplyResources(panel8, "panel8");
            panel8.Name = "panel8";
            toolTip.SetToolTip(panel8, resources.GetString("panel8.ToolTip"));
            // 
            // groupBox7
            // 
            resources.ApplyResources(groupBox7, "groupBox7");
            groupBox7.Controls.Add(numericBoxNumOfBlochWave);
            groupBox7.Name = "groupBox7";
            groupBox7.TabStop = false;
            toolTip.SetToolTip(groupBox7, resources.GetString("groupBox7.ToolTip"));
            // 
            // numericBoxNumOfBlochWave
            // 
            resources.ApplyResources(numericBoxNumOfBlochWave, "numericBoxNumOfBlochWave");
            numericBoxNumOfBlochWave.BackColor = System.Drawing.SystemColors.Control;
            numericBoxNumOfBlochWave.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxNumOfBlochWave.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxNumOfBlochWave.Maximum = 1024D;
            numericBoxNumOfBlochWave.Minimum = 2D;
            numericBoxNumOfBlochWave.Name = "numericBoxNumOfBlochWave";
            numericBoxNumOfBlochWave.RadianValue = 1.3962634015954636D;
            numericBoxNumOfBlochWave.RoundErrorAccuracy = -1;
            numericBoxNumOfBlochWave.ShowUpDown = true;
            numericBoxNumOfBlochWave.SmartIncrement = true;
            numericBoxNumOfBlochWave.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxNumOfBlochWave, resources.GetString("numericBoxNumOfBlochWave.ToolTip"));
            numericBoxNumOfBlochWave.Value = 80D;
            numericBoxNumOfBlochWave.ValueChanged += NumericBoxNumOfBlochWave_ValueChanged;
            // 
            // groupBoxOpticalProperty
            // 
            resources.ApplyResources(groupBoxOpticalProperty, "groupBoxOpticalProperty");
            groupBoxOpticalProperty.Controls.Add(groupBoxSTEMoption1);
            groupBoxOpticalProperty.Controls.Add(groupBoxHREMoption1);
            groupBoxOpticalProperty.Controls.Add(groupBox4);
            groupBoxOpticalProperty.Name = "groupBoxOpticalProperty";
            groupBoxOpticalProperty.TabStop = false;
            toolTip.SetToolTip(groupBoxOpticalProperty, resources.GetString("groupBoxOpticalProperty.ToolTip"));
            // 
            // groupBoxSTEMoption1
            // 
            resources.ApplyResources(groupBoxSTEMoption1, "groupBoxSTEMoption1");
            groupBoxSTEMoption1.ContextMenuStrip = contextMenuStripSTEM;
            groupBoxSTEMoption1.Controls.Add(label5);
            groupBoxSTEMoption1.Controls.Add(label34);
            groupBoxSTEMoption1.Controls.Add(numericBoxSTEM_DetectorOuterAngle);
            groupBoxSTEMoption1.Controls.Add(numericBoxEffectiveSourceSize);
            groupBoxSTEMoption1.Controls.Add(numericBoxSTEM_ConvergenceAngle);
            groupBoxSTEMoption1.Controls.Add(flowLayoutPanel15);
            groupBoxSTEMoption1.Controls.Add(flowLayoutPanel9);
            groupBoxSTEMoption1.Controls.Add(flowLayoutPanel2);
            groupBoxSTEMoption1.Controls.Add(numericBoxSTEM_DetectorInnerAngle);
            groupBoxSTEMoption1.Controls.Add(label1);
            groupBoxSTEMoption1.Name = "groupBoxSTEMoption1";
            groupBoxSTEMoption1.TabStop = false;
            toolTip.SetToolTip(groupBoxSTEMoption1, resources.GetString("groupBoxSTEMoption1.ToolTip"));
            // 
            // contextMenuStripSTEM
            // 
            resources.ApplyResources(contextMenuStripSTEM, "contextMenuStripSTEM");
            contextMenuStripSTEM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { typicalBF02MradToolStripMenuItem, typicalABF1224MradToolStripMenuItem, typicalLAADF2560MradToolStripMenuItem, typicalHAADF80250MradToolStripMenuItem });
            contextMenuStripSTEM.Name = "contextMenuStripSTEM";
            toolTip.SetToolTip(contextMenuStripSTEM, resources.GetString("contextMenuStripSTEM.ToolTip"));
            // 
            // typicalBF02MradToolStripMenuItem
            // 
            resources.ApplyResources(typicalBF02MradToolStripMenuItem, "typicalBF02MradToolStripMenuItem");
            typicalBF02MradToolStripMenuItem.Name = "typicalBF02MradToolStripMenuItem";
            typicalBF02MradToolStripMenuItem.Click += typicalBF02MradToolStripMenuItem_Click;
            // 
            // typicalABF1224MradToolStripMenuItem
            // 
            resources.ApplyResources(typicalABF1224MradToolStripMenuItem, "typicalABF1224MradToolStripMenuItem");
            typicalABF1224MradToolStripMenuItem.Name = "typicalABF1224MradToolStripMenuItem";
            typicalABF1224MradToolStripMenuItem.Click += typicalABF1224MradToolStripMenuItem_Click;
            // 
            // typicalLAADF2560MradToolStripMenuItem
            // 
            resources.ApplyResources(typicalLAADF2560MradToolStripMenuItem, "typicalLAADF2560MradToolStripMenuItem");
            typicalLAADF2560MradToolStripMenuItem.Name = "typicalLAADF2560MradToolStripMenuItem";
            typicalLAADF2560MradToolStripMenuItem.Click += typicalLAADF2560MradToolStripMenuItem_Click;
            // 
            // typicalHAADF80250MradToolStripMenuItem
            // 
            resources.ApplyResources(typicalHAADF80250MradToolStripMenuItem, "typicalHAADF80250MradToolStripMenuItem");
            typicalHAADF80250MradToolStripMenuItem.Name = "typicalHAADF80250MradToolStripMenuItem";
            typicalHAADF80250MradToolStripMenuItem.Click += typicalHAADF80250MradToolStripMenuItem_Click;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.ForeColor = System.Drawing.SystemColors.ControlText;
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // label34
            // 
            resources.ApplyResources(label34, "label34");
            label34.ForeColor = System.Drawing.SystemColors.ControlText;
            label34.Name = "label34";
            toolTip.SetToolTip(label34, resources.GetString("label34.ToolTip"));
            // 
            // numericBoxSTEM_DetectorOuterAngle
            // 
            resources.ApplyResources(numericBoxSTEM_DetectorOuterAngle, "numericBoxSTEM_DetectorOuterAngle");
            numericBoxSTEM_DetectorOuterAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_DetectorOuterAngle.DecimalPlaces = 1;
            numericBoxSTEM_DetectorOuterAngle.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_DetectorOuterAngle.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_DetectorOuterAngle.Maximum = 1570D;
            numericBoxSTEM_DetectorOuterAngle.Minimum = 0.5D;
            numericBoxSTEM_DetectorOuterAngle.Name = "numericBoxSTEM_DetectorOuterAngle";
            numericBoxSTEM_DetectorOuterAngle.RadianValue = 0.3490658503988659D;
            numericBoxSTEM_DetectorOuterAngle.RoundErrorAccuracy = -1;
            numericBoxSTEM_DetectorOuterAngle.ShowUpDown = true;
            numericBoxSTEM_DetectorOuterAngle.SmartIncrement = true;
            numericBoxSTEM_DetectorOuterAngle.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxSTEM_DetectorOuterAngle, resources.GetString("numericBoxSTEM_DetectorOuterAngle.ToolTip"));
            numericBoxSTEM_DetectorOuterAngle.UpDown_Increment = 0.5D;
            numericBoxSTEM_DetectorOuterAngle.Value = 20D;
            numericBoxSTEM_DetectorOuterAngle.ValueChanged += numericBoxSTEM_ConvergenceAngle_ValueChanged;
            // 
            // numericBoxEffectiveSourceSize
            // 
            resources.ApplyResources(numericBoxEffectiveSourceSize, "numericBoxEffectiveSourceSize");
            numericBoxEffectiveSourceSize.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEffectiveSourceSize.DecimalPlaces = 1;
            numericBoxEffectiveSourceSize.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxEffectiveSourceSize.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxEffectiveSourceSize.Maximum = 1000D;
            numericBoxEffectiveSourceSize.Minimum = 0D;
            numericBoxEffectiveSourceSize.Name = "numericBoxEffectiveSourceSize";
            numericBoxEffectiveSourceSize.RadianValue = 0.3490658503988659D;
            numericBoxEffectiveSourceSize.RestrictLimitValue = false;
            numericBoxEffectiveSourceSize.RoundErrorAccuracy = -1;
            numericBoxEffectiveSourceSize.ShowUpDown = true;
            numericBoxEffectiveSourceSize.SmartIncrement = true;
            numericBoxEffectiveSourceSize.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxEffectiveSourceSize, resources.GetString("numericBoxEffectiveSourceSize.ToolTip"));
            numericBoxEffectiveSourceSize.UpDown_Increment = 0.1D;
            numericBoxEffectiveSourceSize.Value = 20D;
            numericBoxEffectiveSourceSize.ValueChanged += NumericBoxTEMproperty_ValueChanged;
            // 
            // numericBoxSTEM_ConvergenceAngle
            // 
            resources.ApplyResources(numericBoxSTEM_ConvergenceAngle, "numericBoxSTEM_ConvergenceAngle");
            numericBoxSTEM_ConvergenceAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_ConvergenceAngle.DecimalPlaces = 1;
            numericBoxSTEM_ConvergenceAngle.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_ConvergenceAngle.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_ConvergenceAngle.Maximum = 1570D;
            numericBoxSTEM_ConvergenceAngle.Minimum = 0.1D;
            numericBoxSTEM_ConvergenceAngle.Name = "numericBoxSTEM_ConvergenceAngle";
            numericBoxSTEM_ConvergenceAngle.RadianValue = 0.43633231299858238D;
            numericBoxSTEM_ConvergenceAngle.RoundErrorAccuracy = -1;
            numericBoxSTEM_ConvergenceAngle.ShowUpDown = true;
            numericBoxSTEM_ConvergenceAngle.SmartIncrement = true;
            numericBoxSTEM_ConvergenceAngle.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxSTEM_ConvergenceAngle, resources.GetString("numericBoxSTEM_ConvergenceAngle.ToolTip"));
            numericBoxSTEM_ConvergenceAngle.UpDown_Increment = 0.5D;
            numericBoxSTEM_ConvergenceAngle.Value = 25D;
            numericBoxSTEM_ConvergenceAngle.ValueChanged += numericBoxSTEM_ConvergenceAngle_ValueChanged;
            // 
            // flowLayoutPanel15
            // 
            resources.ApplyResources(flowLayoutPanel15, "flowLayoutPanel15");
            flowLayoutPanel15.Controls.Add(textBoxOuterRadius);
            flowLayoutPanel15.Controls.Add(label38);
            flowLayoutPanel15.Name = "flowLayoutPanel15";
            toolTip.SetToolTip(flowLayoutPanel15, resources.GetString("flowLayoutPanel15.ToolTip"));
            // 
            // textBoxOuterRadius
            // 
            resources.ApplyResources(textBoxOuterRadius, "textBoxOuterRadius");
            textBoxOuterRadius.BackColor = System.Drawing.SystemColors.InactiveCaption;
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
            // flowLayoutPanel9
            // 
            resources.ApplyResources(flowLayoutPanel9, "flowLayoutPanel9");
            flowLayoutPanel9.Controls.Add(textBoxInnerRadius);
            flowLayoutPanel9.Controls.Add(label37);
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            toolTip.SetToolTip(flowLayoutPanel9, resources.GetString("flowLayoutPanel9.ToolTip"));
            // 
            // textBoxInnerRadius
            // 
            resources.ApplyResources(textBoxInnerRadius, "textBoxInnerRadius");
            textBoxInnerRadius.BackColor = System.Drawing.SystemColors.InactiveCaption;
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
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(textBoxConvRadius);
            flowLayoutPanel2.Controls.Add(label36);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            toolTip.SetToolTip(flowLayoutPanel2, resources.GetString("flowLayoutPanel2.ToolTip"));
            // 
            // textBoxConvRadius
            // 
            resources.ApplyResources(textBoxConvRadius, "textBoxConvRadius");
            textBoxConvRadius.BackColor = System.Drawing.SystemColors.InactiveCaption;
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
            // numericBoxSTEM_DetectorInnerAngle
            // 
            resources.ApplyResources(numericBoxSTEM_DetectorInnerAngle, "numericBoxSTEM_DetectorInnerAngle");
            numericBoxSTEM_DetectorInnerAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_DetectorInnerAngle.DecimalPlaces = 1;
            numericBoxSTEM_DetectorInnerAngle.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_DetectorInnerAngle.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxSTEM_DetectorInnerAngle.Maximum = 1570D;
            numericBoxSTEM_DetectorInnerAngle.Minimum = 0D;
            numericBoxSTEM_DetectorInnerAngle.Name = "numericBoxSTEM_DetectorInnerAngle";
            numericBoxSTEM_DetectorInnerAngle.RoundErrorAccuracy = -1;
            numericBoxSTEM_DetectorInnerAngle.ShowUpDown = true;
            numericBoxSTEM_DetectorInnerAngle.SmartIncrement = true;
            numericBoxSTEM_DetectorInnerAngle.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxSTEM_DetectorInnerAngle, resources.GetString("numericBoxSTEM_DetectorInnerAngle.ToolTip"));
            numericBoxSTEM_DetectorInnerAngle.UpDown_Increment = 0.5D;
            numericBoxSTEM_DetectorInnerAngle.ValueChanged += numericBoxSTEM_ConvergenceAngle_ValueChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.ForeColor = System.Drawing.SystemColors.ControlText;
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // groupBoxHREMoption1
            // 
            resources.ApplyResources(groupBoxHREMoption1, "groupBoxHREMoption1");
            groupBoxHREMoption1.Controls.Add(checkBoxOpenAperture);
            groupBoxHREMoption1.Controls.Add(numericBoxObjAperX);
            groupBoxHREMoption1.Controls.Add(numericBoxObjAperRadius);
            groupBoxHREMoption1.Controls.Add(numericBoxObjAperY);
            groupBoxHREMoption1.Controls.Add(flowLayoutPanel1);
            groupBoxHREMoption1.Controls.Add(flowLayoutPanel5);
            groupBoxHREMoption1.Controls.Add(label8);
            groupBoxHREMoption1.Name = "groupBoxHREMoption1";
            groupBoxHREMoption1.TabStop = false;
            toolTip.SetToolTip(groupBoxHREMoption1, resources.GetString("groupBoxHREMoption1.ToolTip"));
            // 
            // checkBoxOpenAperture
            // 
            resources.ApplyResources(checkBoxOpenAperture, "checkBoxOpenAperture");
            checkBoxOpenAperture.Name = "checkBoxOpenAperture";
            toolTip.SetToolTip(checkBoxOpenAperture, resources.GetString("checkBoxOpenAperture.ToolTip"));
            checkBoxOpenAperture.UseVisualStyleBackColor = true;
            checkBoxOpenAperture.CheckedChanged += NumericBoxObjAperRadius_ValueChanged;
            // 
            // numericBoxObjAperX
            // 
            resources.ApplyResources(numericBoxObjAperX, "numericBoxObjAperX");
            numericBoxObjAperX.BackColor = System.Drawing.SystemColors.Control;
            numericBoxObjAperX.DecimalPlaces = 1;
            numericBoxObjAperX.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxObjAperX.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxObjAperX.Maximum = 100D;
            numericBoxObjAperX.Minimum = -100D;
            numericBoxObjAperX.Name = "numericBoxObjAperX";
            numericBoxObjAperX.RoundErrorAccuracy = -1;
            numericBoxObjAperX.ShowUpDown = true;
            numericBoxObjAperX.SmartIncrement = true;
            numericBoxObjAperX.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxObjAperX, resources.GetString("numericBoxObjAperX.ToolTip"));
            numericBoxObjAperX.UpDown_Increment = 0.5D;
            numericBoxObjAperX.ValueChanged += NumericBoxObjAperRadius_ValueChanged;
            // 
            // numericBoxObjAperRadius
            // 
            resources.ApplyResources(numericBoxObjAperRadius, "numericBoxObjAperRadius");
            numericBoxObjAperRadius.BackColor = System.Drawing.SystemColors.Control;
            numericBoxObjAperRadius.DecimalPlaces = 1;
            numericBoxObjAperRadius.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxObjAperRadius.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxObjAperRadius.Maximum = 500D;
            numericBoxObjAperRadius.Minimum = 0.5D;
            numericBoxObjAperRadius.Name = "numericBoxObjAperRadius";
            numericBoxObjAperRadius.RadianValue = 0.20943951023931953D;
            numericBoxObjAperRadius.RoundErrorAccuracy = -1;
            numericBoxObjAperRadius.ShowUpDown = true;
            numericBoxObjAperRadius.SmartIncrement = true;
            numericBoxObjAperRadius.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxObjAperRadius, resources.GetString("numericBoxObjAperRadius.ToolTip"));
            numericBoxObjAperRadius.UpDown_Increment = 0.5D;
            numericBoxObjAperRadius.Value = 12D;
            numericBoxObjAperRadius.ValueChanged += NumericBoxObjAperRadius_ValueChanged;
            // 
            // numericBoxObjAperY
            // 
            resources.ApplyResources(numericBoxObjAperY, "numericBoxObjAperY");
            numericBoxObjAperY.BackColor = System.Drawing.SystemColors.Control;
            numericBoxObjAperY.DecimalPlaces = 1;
            numericBoxObjAperY.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxObjAperY.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxObjAperY.Maximum = 100D;
            numericBoxObjAperY.Minimum = -100D;
            numericBoxObjAperY.Name = "numericBoxObjAperY";
            numericBoxObjAperY.RoundErrorAccuracy = -1;
            numericBoxObjAperY.ShowUpDown = true;
            numericBoxObjAperY.SmartIncrement = true;
            numericBoxObjAperY.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxObjAperY, resources.GetString("numericBoxObjAperY.ToolTip"));
            numericBoxObjAperY.UpDown_Increment = 0.5D;
            numericBoxObjAperY.ValueChanged += NumericBoxObjAperRadius_ValueChanged;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(textBoxNumOfSpots);
            flowLayoutPanel1.Controls.Add(label9);
            flowLayoutPanel1.Controls.Add(buttonDetailsOfSpots);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            toolTip.SetToolTip(flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
            // 
            // textBoxNumOfSpots
            // 
            resources.ApplyResources(textBoxNumOfSpots, "textBoxNumOfSpots");
            textBoxNumOfSpots.BackColor = System.Drawing.SystemColors.InactiveCaption;
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
            // buttonDetailsOfSpots
            // 
            resources.ApplyResources(buttonDetailsOfSpots, "buttonDetailsOfSpots");
            buttonDetailsOfSpots.Name = "buttonDetailsOfSpots";
            toolTip.SetToolTip(buttonDetailsOfSpots, resources.GetString("buttonDetailsOfSpots.ToolTip"));
            buttonDetailsOfSpots.UseVisualStyleBackColor = true;
            buttonDetailsOfSpots.Click += ButtonDetailsOfSpots_Click;
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(textBoxObjAperRadius);
            flowLayoutPanel5.Controls.Add(label7);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            toolTip.SetToolTip(flowLayoutPanel5, resources.GetString("flowLayoutPanel5.ToolTip"));
            // 
            // textBoxObjAperRadius
            // 
            resources.ApplyResources(textBoxObjAperRadius, "textBoxObjAperRadius");
            textBoxObjAperRadius.BackColor = System.Drawing.SystemColors.InactiveCaption;
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
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.ForeColor = System.Drawing.Color.DimGray;
            label8.Name = "label8";
            toolTip.SetToolTip(label8, resources.GetString("label8.ToolTip"));
            // 
            // groupBox4
            // 
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.ContextMenuStrip = contextMenuStripTEMcondition;
            groupBox4.Controls.Add(label35);
            groupBox4.Controls.Add(checkBoxCTF);
            groupBox4.Controls.Add(numericBoxCc);
            groupBox4.Controls.Add(numericBoxDeltaV);
            groupBox4.Controls.Add(numericBoxBetaAgnle);
            groupBox4.Controls.Add(numericBoxCs);
            groupBox4.Controls.Add(numericBoxDefocus);
            groupBox4.Controls.Add(numericBoxAccVol);
            groupBox4.Controls.Add(flowLayoutPanel3);
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            toolTip.SetToolTip(groupBox4, resources.GetString("groupBox4.ToolTip"));
            // 
            // contextMenuStripTEMcondition
            // 
            resources.ApplyResources(contextMenuStripTEMcondition, "contextMenuStripTEMcondition");
            contextMenuStripTEMcondition.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { setoZeroDefocusToolStripMenuItem, setoScherzerDefocusToolStripMenuItem, setAllAToolStripMenuItem, toolStripSeparator6, presets1ToolStripMenuItem, presets2ToolStripMenuItem, presets3ToolStripMenuItem, presets4ToolStripMenuItem });
            contextMenuStripTEMcondition.Name = "contextMenuStripInherentProperty";
            toolTip.SetToolTip(contextMenuStripTEMcondition, resources.GetString("contextMenuStripTEMcondition.ToolTip"));
            // 
            // setoZeroDefocusToolStripMenuItem
            // 
            resources.ApplyResources(setoZeroDefocusToolStripMenuItem, "setoZeroDefocusToolStripMenuItem");
            setoZeroDefocusToolStripMenuItem.Name = "setoZeroDefocusToolStripMenuItem";
            setoZeroDefocusToolStripMenuItem.Click += setoZeroDefocusToolStripMenuItem_Click;
            // 
            // setoScherzerDefocusToolStripMenuItem
            // 
            resources.ApplyResources(setoScherzerDefocusToolStripMenuItem, "setoScherzerDefocusToolStripMenuItem");
            setoScherzerDefocusToolStripMenuItem.Name = "setoScherzerDefocusToolStripMenuItem";
            setoScherzerDefocusToolStripMenuItem.Click += setoScherzerDefocusToolStripMenuItem_Click;
            // 
            // setAllAToolStripMenuItem
            // 
            resources.ApplyResources(setAllAToolStripMenuItem, "setAllAToolStripMenuItem");
            setAllAToolStripMenuItem.Name = "setAllAToolStripMenuItem";
            setAllAToolStripMenuItem.Click += zeroAllToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
            toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // presets1ToolStripMenuItem
            // 
            resources.ApplyResources(presets1ToolStripMenuItem, "presets1ToolStripMenuItem");
            presets1ToolStripMenuItem.Name = "presets1ToolStripMenuItem";
            presets1ToolStripMenuItem.Click += presets1ToolStripMenuItem_Click;
            // 
            // presets2ToolStripMenuItem
            // 
            resources.ApplyResources(presets2ToolStripMenuItem, "presets2ToolStripMenuItem");
            presets2ToolStripMenuItem.Name = "presets2ToolStripMenuItem";
            presets2ToolStripMenuItem.Click += presets2ToolStripMenuItem_Click;
            // 
            // presets3ToolStripMenuItem
            // 
            resources.ApplyResources(presets3ToolStripMenuItem, "presets3ToolStripMenuItem");
            presets3ToolStripMenuItem.Name = "presets3ToolStripMenuItem";
            presets3ToolStripMenuItem.Click += presets3ToolStripMenuItem_Click;
            // 
            // presets4ToolStripMenuItem
            // 
            resources.ApplyResources(presets4ToolStripMenuItem, "presets4ToolStripMenuItem");
            presets4ToolStripMenuItem.Name = "presets4ToolStripMenuItem";
            presets4ToolStripMenuItem.Click += presets4ToolStripMenuItem_Click;
            // 
            // label35
            // 
            resources.ApplyResources(label35, "label35");
            label35.ForeColor = System.Drawing.SystemColors.ControlText;
            label35.Name = "label35";
            toolTip.SetToolTip(label35, resources.GetString("label35.ToolTip"));
            // 
            // checkBoxCTF
            // 
            resources.ApplyResources(checkBoxCTF, "checkBoxCTF");
            checkBoxCTF.Name = "checkBoxCTF";
            toolTip.SetToolTip(checkBoxCTF, resources.GetString("checkBoxCTF.ToolTip"));
            checkBoxCTF.UseVisualStyleBackColor = true;
            checkBoxCTF.CheckedChanged += checkBoxShowLensFunctionGraph_CheckedChanged;
            // 
            // numericBoxCc
            // 
            resources.ApplyResources(numericBoxCc, "numericBoxCc");
            numericBoxCc.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCc.DecimalPlaces = 2;
            numericBoxCc.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCc.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCc.Maximum = 10D;
            numericBoxCc.Minimum = 0D;
            numericBoxCc.Name = "numericBoxCc";
            numericBoxCc.RadianValue = 0.024434609527920613D;
            numericBoxCc.RestrictLimitValue = false;
            numericBoxCc.RoundErrorAccuracy = -1;
            numericBoxCc.ShowUpDown = true;
            numericBoxCc.SmartIncrement = true;
            numericBoxCc.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxCc, resources.GetString("numericBoxCc.ToolTip"));
            numericBoxCc.UpDown_Increment = 0.1D;
            numericBoxCc.Value = 1.4D;
            numericBoxCc.ValueChanged += NumericBoxTEMproperty_ValueChanged;
            // 
            // numericBoxDeltaV
            // 
            resources.ApplyResources(numericBoxDeltaV, "numericBoxDeltaV");
            numericBoxDeltaV.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDeltaV.DecimalPlaces = 2;
            numericBoxDeltaV.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDeltaV.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDeltaV.Maximum = 10D;
            numericBoxDeltaV.Minimum = 0D;
            numericBoxDeltaV.Name = "numericBoxDeltaV";
            numericBoxDeltaV.RadianValue = 0.013962634015954637D;
            numericBoxDeltaV.RestrictLimitValue = false;
            numericBoxDeltaV.RoundErrorAccuracy = -1;
            numericBoxDeltaV.ShowUpDown = true;
            numericBoxDeltaV.SmartIncrement = true;
            numericBoxDeltaV.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxDeltaV, resources.GetString("numericBoxDeltaV.ToolTip"));
            numericBoxDeltaV.UpDown_Increment = 0.1D;
            numericBoxDeltaV.Value = 0.8D;
            numericBoxDeltaV.ValueChanged += NumericBoxTEMproperty_ValueChanged;
            // 
            // numericBoxBetaAgnle
            // 
            resources.ApplyResources(numericBoxBetaAgnle, "numericBoxBetaAgnle");
            numericBoxBetaAgnle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBetaAgnle.DecimalPlaces = 2;
            numericBoxBetaAgnle.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBetaAgnle.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBetaAgnle.Maximum = 100D;
            numericBoxBetaAgnle.Minimum = 0D;
            numericBoxBetaAgnle.Name = "numericBoxBetaAgnle";
            numericBoxBetaAgnle.RoundErrorAccuracy = -1;
            numericBoxBetaAgnle.ShowUpDown = true;
            numericBoxBetaAgnle.SmartIncrement = true;
            numericBoxBetaAgnle.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxBetaAgnle, resources.GetString("numericBoxBetaAgnle.ToolTip"));
            numericBoxBetaAgnle.UpDown_Increment = 0.05D;
            numericBoxBetaAgnle.ValueChanged += NumericBoxTEMproperty_ValueChanged;
            // 
            // numericBoxCs
            // 
            resources.ApplyResources(numericBoxCs, "numericBoxCs");
            numericBoxCs.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCs.DecimalPlaces = 2;
            numericBoxCs.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCs.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCs.Maximum = 20D;
            numericBoxCs.Minimum = -20D;
            numericBoxCs.Name = "numericBoxCs";
            numericBoxCs.RadianValue = 0.017453292519943295D;
            numericBoxCs.RoundErrorAccuracy = -1;
            numericBoxCs.ShowUpDown = true;
            numericBoxCs.SmartIncrement = true;
            numericBoxCs.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxCs, resources.GetString("numericBoxCs.ToolTip"));
            numericBoxCs.UpDown_Increment = 0.1D;
            numericBoxCs.Value = 1D;
            numericBoxCs.ValueChanged += NumericBoxCs_ValueChanged;
            // 
            // numericBoxDefocus
            // 
            resources.ApplyResources(numericBoxDefocus, "numericBoxDefocus");
            numericBoxDefocus.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocus.DecimalPlaces = 2;
            numericBoxDefocus.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocus.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDefocus.Maximum = 1000D;
            numericBoxDefocus.Minimum = -1000D;
            numericBoxDefocus.Name = "numericBoxDefocus";
            numericBoxDefocus.RadianValue = -1.0088003076527223D;
            numericBoxDefocus.RoundErrorAccuracy = -1;
            numericBoxDefocus.ShowUpDown = true;
            numericBoxDefocus.SmartIncrement = true;
            numericBoxDefocus.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxDefocus, resources.GetString("numericBoxDefocus.ToolTip"));
            numericBoxDefocus.Value = -57.8D;
            numericBoxDefocus.ValueChanged += NumericBoxDefocus_ValueChanged;
            // 
            // numericBoxAccVol
            // 
            resources.ApplyResources(numericBoxAccVol, "numericBoxAccVol");
            numericBoxAccVol.BackColor = System.Drawing.SystemColors.Control;
            numericBoxAccVol.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxAccVol.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxAccVol.Maximum = 1000D;
            numericBoxAccVol.Minimum = 1D;
            numericBoxAccVol.Name = "numericBoxAccVol";
            numericBoxAccVol.RadianValue = 3.4906585039886591D;
            numericBoxAccVol.RoundErrorAccuracy = -1;
            numericBoxAccVol.ShowUpDown = true;
            numericBoxAccVol.SmartIncrement = true;
            numericBoxAccVol.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxAccVol, resources.GetString("numericBoxAccVol.ToolTip"));
            numericBoxAccVol.Value = 200D;
            numericBoxAccVol.ValueChanged += NumericBoxAccVol_ValueChanged;
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(label3);
            flowLayoutPanel3.Controls.Add(textBoxScherzer);
            flowLayoutPanel3.Controls.Add(label4);
            flowLayoutPanel3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            toolTip.SetToolTip(flowLayoutPanel3, resources.GetString("flowLayoutPanel3.ToolTip"));
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
            resources.ApplyResources(textBoxScherzer, "textBoxScherzer");
            textBoxScherzer.BackColor = System.Drawing.SystemColors.InactiveCaption;
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
            // flowLayoutPanel14
            // 
            resources.ApplyResources(flowLayoutPanel14, "flowLayoutPanel14");
            flowLayoutPanel14.Controls.Add(groupBox6);
            flowLayoutPanel14.Controls.Add(groupBoxSampleProperty);
            flowLayoutPanel14.Name = "flowLayoutPanel14";
            toolTip.SetToolTip(flowLayoutPanel14, resources.GetString("flowLayoutPanel14.ToolTip"));
            // 
            // groupBox6
            // 
            resources.ApplyResources(groupBox6, "groupBox6");
            groupBox6.Controls.Add(flowLayoutPanel16);
            groupBox6.Name = "groupBox6";
            groupBox6.TabStop = false;
            toolTip.SetToolTip(groupBox6, resources.GetString("groupBox6.ToolTip"));
            // 
            // flowLayoutPanel16
            // 
            resources.ApplyResources(flowLayoutPanel16, "flowLayoutPanel16");
            flowLayoutPanel16.Controls.Add(radioButtonHRTEM);
            flowLayoutPanel16.Controls.Add(radioButtonSTEM);
            flowLayoutPanel16.Controls.Add(radioButtonProjectedPotential);
            flowLayoutPanel16.Name = "flowLayoutPanel16";
            toolTip.SetToolTip(flowLayoutPanel16, resources.GetString("flowLayoutPanel16.ToolTip"));
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
            resources.ApplyResources(groupBoxSampleProperty, "groupBoxSampleProperty");
            groupBoxSampleProperty.Controls.Add(numericBoxThickness);
            groupBoxSampleProperty.Name = "groupBoxSampleProperty";
            groupBoxSampleProperty.TabStop = false;
            toolTip.SetToolTip(groupBoxSampleProperty, resources.GetString("groupBoxSampleProperty.ToolTip"));
            // 
            // numericBoxThickness
            // 
            resources.ApplyResources(numericBoxThickness, "numericBoxThickness");
            numericBoxThickness.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThickness.DecimalPlaces = 2;
            numericBoxThickness.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThickness.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThickness.Maximum = 1000D;
            numericBoxThickness.Minimum = 0.001D;
            numericBoxThickness.Name = "numericBoxThickness";
            numericBoxThickness.RadianValue = 0.3490658503988659D;
            numericBoxThickness.RoundErrorAccuracy = -1;
            numericBoxThickness.ShowUpDown = true;
            numericBoxThickness.SmartIncrement = true;
            numericBoxThickness.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxThickness, resources.GetString("numericBoxThickness.ToolTip"));
            numericBoxThickness.Value = 20D;
            numericBoxThickness.ValueChanged += NumericBoxThickness_ValueChanged;
            // 
            // panel4
            // 
            resources.ApplyResources(panel4, "panel4");
            panel4.Controls.Add(checkBoxPreset);
            panel4.Controls.Add(checkBoxRealTimeSimulation);
            panel4.Controls.Add(buttonSimulate);
            panel4.Name = "panel4";
            toolTip.SetToolTip(panel4, resources.GetString("panel4.ToolTip"));
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
            toolTip.SetToolTip(menuStrip1, resources.GetString("menuStrip1.ToolTip"));
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemSave, copyImageToolStripMenuItem, toolStripMenuItemOverprintSymbols, toolStripSeparator1, readTEMParameterToolStripMenuItem, saveTEMParametersToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // toolStripMenuItemSave
            // 
            resources.ApplyResources(toolStripMenuItemSave, "toolStripMenuItemSave");
            toolStripMenuItemSave.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemSavePNG, toolStripMenuItemSaveTIFF, toolStripMenuItemSaveMetafile, toolStripMenuItemSaveIndividually });
            toolStripMenuItemSave.Name = "toolStripMenuItemSave";
            // 
            // toolStripMenuItemSavePNG
            // 
            resources.ApplyResources(toolStripMenuItemSavePNG, "toolStripMenuItemSavePNG");
            toolStripMenuItemSavePNG.Name = "toolStripMenuItemSavePNG";
            toolStripMenuItemSavePNG.Click += ToolStripMenuItemSavePNG_Click;
            // 
            // toolStripMenuItemSaveTIFF
            // 
            resources.ApplyResources(toolStripMenuItemSaveTIFF, "toolStripMenuItemSaveTIFF");
            toolStripMenuItemSaveTIFF.Name = "toolStripMenuItemSaveTIFF";
            toolStripMenuItemSaveTIFF.Click += ToolStripMenuItemSaveTIFF_Click;
            // 
            // toolStripMenuItemSaveMetafile
            // 
            resources.ApplyResources(toolStripMenuItemSaveMetafile, "toolStripMenuItemSaveMetafile");
            toolStripMenuItemSaveMetafile.Name = "toolStripMenuItemSaveMetafile";
            toolStripMenuItemSaveMetafile.Click += ToolStripMenuItemSaveMetafile_Click;
            // 
            // toolStripMenuItemSaveIndividually
            // 
            resources.ApplyResources(toolStripMenuItemSaveIndividually, "toolStripMenuItemSaveIndividually");
            toolStripMenuItemSaveIndividually.Checked = true;
            toolStripMenuItemSaveIndividually.CheckOnClick = true;
            toolStripMenuItemSaveIndividually.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripMenuItemSaveIndividually.Name = "toolStripMenuItemSaveIndividually";
            // 
            // copyImageToolStripMenuItem
            // 
            resources.ApplyResources(copyImageToolStripMenuItem, "copyImageToolStripMenuItem");
            copyImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemCopyImage, toolStripMenuItemCopyMetafile });
            copyImageToolStripMenuItem.Name = "copyImageToolStripMenuItem";
            // 
            // toolStripMenuItemCopyImage
            // 
            resources.ApplyResources(toolStripMenuItemCopyImage, "toolStripMenuItemCopyImage");
            toolStripMenuItemCopyImage.Name = "toolStripMenuItemCopyImage";
            toolStripMenuItemCopyImage.Click += ToolStripMenuItemCopyImage_Click;
            // 
            // toolStripMenuItemCopyMetafile
            // 
            resources.ApplyResources(toolStripMenuItemCopyMetafile, "toolStripMenuItemCopyMetafile");
            toolStripMenuItemCopyMetafile.Name = "toolStripMenuItemCopyMetafile";
            toolStripMenuItemCopyMetafile.Click += ToolStripMenuItemCopyMetafile_Click;
            // 
            // toolStripMenuItemOverprintSymbols
            // 
            resources.ApplyResources(toolStripMenuItemOverprintSymbols, "toolStripMenuItemOverprintSymbols");
            toolStripMenuItemOverprintSymbols.Checked = true;
            toolStripMenuItemOverprintSymbols.CheckOnClick = true;
            toolStripMenuItemOverprintSymbols.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripMenuItemOverprintSymbols.Name = "toolStripMenuItemOverprintSymbols";
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // readTEMParameterToolStripMenuItem
            // 
            resources.ApplyResources(readTEMParameterToolStripMenuItem, "readTEMParameterToolStripMenuItem");
            readTEMParameterToolStripMenuItem.Name = "readTEMParameterToolStripMenuItem";
            // 
            // saveTEMParametersToolStripMenuItem
            // 
            resources.ApplyResources(saveTEMParametersToolStripMenuItem, "saveTEMParametersToolStripMenuItem");
            saveTEMParametersToolStripMenuItem.Name = "saveTEMParametersToolStripMenuItem";
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { detailsOfHRTEMSimulationToolStripMenuItem, toolStripSeparator2, calculationLibraryToolStripMenuItem, toolStripComboBoxCaclulationLibrary });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // detailsOfHRTEMSimulationToolStripMenuItem
            // 
            resources.ApplyResources(detailsOfHRTEMSimulationToolStripMenuItem, "detailsOfHRTEMSimulationToolStripMenuItem");
            detailsOfHRTEMSimulationToolStripMenuItem.Name = "detailsOfHRTEMSimulationToolStripMenuItem";
            detailsOfHRTEMSimulationToolStripMenuItem.Click += DetailsOfHRTEMSimulationToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // calculationLibraryToolStripMenuItem
            // 
            resources.ApplyResources(calculationLibraryToolStripMenuItem, "calculationLibraryToolStripMenuItem");
            calculationLibraryToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            calculationLibraryToolStripMenuItem.Name = "calculationLibraryToolStripMenuItem";
            // 
            // toolStripComboBoxCaclulationLibrary
            // 
            resources.ApplyResources(toolStripComboBoxCaclulationLibrary, "toolStripComboBoxCaclulationLibrary");
            toolStripComboBoxCaclulationLibrary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxCaclulationLibrary.Items.AddRange(new object[] { resources.GetString("toolStripComboBoxCaclulationLibrary.Items"), resources.GetString("toolStripComboBoxCaclulationLibrary.Items1") });
            toolStripComboBoxCaclulationLibrary.Margin = new System.Windows.Forms.Padding(20, 2, 2, 2);
            toolStripComboBoxCaclulationLibrary.Name = "toolStripComboBoxCaclulationLibrary";
            // 
            // buttonStop
            // 
            resources.ApplyResources(buttonStop, "buttonStop");
            buttonStop.BackColor = System.Drawing.Color.IndianRed;
            buttonStop.ForeColor = System.Drawing.Color.White;
            buttonStop.Name = "buttonStop";
            toolTip.SetToolTip(buttonStop, resources.GetString("buttonStop.ToolTip"));
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Click += buttonStop_Click;
            // 
            // statusStrip1
            // 
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3 });
            statusStrip1.Name = "statusStrip1";
            toolTip.SetToolTip(statusStrip1, resources.GetString("statusStrip1.ToolTip"));
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
            // FormImageSimulator
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(buttonStop);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "FormImageSimulator";
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            FormClosing += FormImageSimulator_FormClosing;
            Load += FormImageSimulator_Load;
            VisibleChanged += FormImageSimulator_VisibleChanged;
            KeyDown += FormImageSimulator_KeyDown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxScaleOfIntensity).EndInit();
            panel2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBoxNormalization.ResumeLayout(false);
            groupBoxNormalization.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            groupBoxSTEMoption3.ResumeLayout(false);
            groupBoxSTEMoption3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            groupBoxSerialImage.ResumeLayout(false);
            groupBoxSerialImage.PerformLayout();
            panelSerial.ResumeLayout(false);
            panelSerial.PerformLayout();
            panelSerialDefocus.ResumeLayout(false);
            panelSerialDefocus.PerformLayout();
            panelSerialThickness.ResumeLayout(false);
            panelSerialThickness.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            flowLayoutPanelHorizontalDirection.ResumeLayout(false);
            flowLayoutPanelHorizontalDirection.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            groupBoxPotentialOption.ResumeLayout(false);
            groupBoxPotentialOption.PerformLayout();
            flowLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel11.PerformLayout();
            flowLayoutPanelMagAndPhase.ResumeLayout(false);
            flowLayoutPanelMagAndPhase.PerformLayout();
            panelPhaseScale.ResumeLayout(false);
            panelPhaseScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhaseScale).EndInit();
            flowLayoutPanelRealAndImaiginary.ResumeLayout(false);
            flowLayoutPanelRealAndImaiginary.PerformLayout();
            groupBoxSTEMoption2.ResumeLayout(false);
            groupBoxSTEMoption2.PerformLayout();
            groupBoxHREMoption2.ResumeLayout(false);
            groupBoxHREMoption2.PerformLayout();
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            panel6.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBoxOpticalProperty.ResumeLayout(false);
            groupBoxSTEMoption1.ResumeLayout(false);
            groupBoxSTEMoption1.PerformLayout();
            contextMenuStripSTEM.ResumeLayout(false);
            flowLayoutPanel15.ResumeLayout(false);
            flowLayoutPanel15.PerformLayout();
            flowLayoutPanel9.ResumeLayout(false);
            flowLayoutPanel9.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            groupBoxHREMoption1.ResumeLayout(false);
            groupBoxHREMoption1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            contextMenuStripTEMcondition.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel14.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            flowLayoutPanel16.ResumeLayout(false);
            flowLayoutPanel16.PerformLayout();
            groupBoxSampleProperty.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
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
        private Crystallography.Controls.NumericBox numericBoxBetaAgnle;
        private Crystallography.Controls.NumericBox numericBoxDeltaV;
        private Crystallography.Controls.NumericBox numericBoxCc;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBoxOpticalProperty;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private Crystallography.Controls.NumericBox numericBoxObjAperY;
        private Crystallography.Controls.NumericBox numericBoxObjAperX;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxScherzer;
        private System.Windows.Forms.GroupBox groupBoxHREMoption1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
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
        private System.Windows.Forms.ToolStripMenuItem readTEMParameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTEMParametersToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.GroupBox groupBox8;
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
        private System.Windows.Forms.Panel panel2;
        private Crystallography.Controls.TrackBarAdvanced trackBarAdvancedMax;
        private Crystallography.Controls.TrackBarAdvanced trackBarAdvancedMin;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.ComboBox comboBoxScaleColorScale;
        private System.Windows.Forms.CheckBox checkBoxGaussianBlur;
        private Crystallography.Controls.NumericBox numericBoxGaussianBlurRadius;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMagAndPhase;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowMagAndPhase;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowMag;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowPhase;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRealAndImaiginary;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowRealAndImag;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowReal;
        private System.Windows.Forms.RadioButton radioButtonPotentialShowImag;
        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox7;
        private NumericBox numericBoxIntensityMax;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.CheckBox checkBoxIntensityMin;
        private NumericBox numericBoxIntensityMin;
        private NumericBox numericBoxSTEM_ConvergenceAngle;
        private NumericBox numericBoxSTEM_DetectorOuterAngle;
        private NumericBox numericBoxSTEM_DetectorInnerAngle;
        private NumericBox numericBoxSTEM_AngleResolution;
        private System.Windows.Forms.GroupBox groupBoxSTEMoption2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.CheckBox checkBoxCTF;
        private System.Windows.Forms.Label label4;
        private NumericBox numericBoxSliceThicknessForInelasticSTEM;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel14;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButtonProjectedPotential;
        private System.Windows.Forms.RadioButton radioButtonSTEM;
        private System.Windows.Forms.RadioButton radioButtonHRTEM;
        private System.Windows.Forms.GroupBox groupBoxSampleProperty;
        private NumericBox numericBoxThickness;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label32;
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
        private NumericBox numericBoxEffectiveSourceSize;
        private System.Windows.Forms.ToolStripMenuItem setAllAToolStripMenuItem;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.ToolStripMenuItem presets1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presets2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presets3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem presets4ToolStripMenuItem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBoxIntensityMax;
        private System.Windows.Forms.GroupBox groupBoxSTEMoption3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxPreset;
        private System.Windows.Forms.RadioButton radioButtonSTEM_target_TDS;
        private System.Windows.Forms.RadioButton radioButtonSTEM_target_elas;
        private System.Windows.Forms.RadioButton radioButtonSTEM_target_both;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel15;
        private System.Windows.Forms.TextBox textBoxOuterRadius;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.TextBox textBoxInnerRadius;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TextBox textBoxConvRadius;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel16;
        private System.Windows.Forms.Panel panel6;
        private NumericBox numericBoxResolution;
        private NumericBox numericBoxWidth;
        private NumericBox numericBoxHeight;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
    }
}