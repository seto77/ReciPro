using System;

namespace ReciPro
{
    partial class FormSpotIDV2
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
        // groupBox1 -> groupBoxSpot
        // groupBox2 -> groupBoxOptics
        // groupBox3 -> groupBoxIndex
        // panel2 -> panelSpotActions
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSpotIDV2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            scalablePictureBoxAdvanced = new ScalablePictureBoxAdvanced();
            groupBoxSpot = new System.Windows.Forms.GroupBox();
            panelSpotActions = new System.Windows.Forms.Panel();
            buttonDeleteSpot = new System.Windows.Forms.Button();
            buttonClearSpots = new System.Windows.Forms.Button();
            buttonCopyToClipboard = new System.Windows.Forms.Button();
            buttonSaveToFile = new System.Windows.Forms.Button();
            checkBoxDetailsOfFunction = new System.Windows.Forms.CheckBox();
            checkBoxDetailsOfSpot = new System.Windows.Forms.CheckBox();
            checkBoxShowObsSpotSymbol = new System.Windows.Forms.CheckBox();
            checkBoxShowObsSpotLabel = new System.Windows.Forms.CheckBox();
            buttonCopmprehensiveFitting = new System.Windows.Forms.Button();
            buttonResetRangeForAllSpots = new System.Windows.Forms.Button();
            numericBoxNumberOfSpots = new NumericBox();
            buttonFindSpots = new System.Windows.Forms.Button();
            numericBoxNearestNeighbor = new NumericBox();
            numericBoxFittingRange = new NumericBox();
            buttonGlobalFit = new System.Windows.Forms.Button();
            numericBoxDonut = new NumericBox();
            buttonDonut = new System.Windows.Forms.Button();
            dataGridViewSpots = new System.Windows.Forms.DataGridView();
            Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            noDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Range = new System.Windows.Forms.DataGridViewTextBoxColumn();
            x0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            y0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            H1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            H2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            θ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            η = new System.Windows.Forms.DataGridViewTextBoxColumn();
            A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            B0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Bx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            By = new System.Windows.Forms.DataGridViewTextBoxColumn();
            R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            d = new System.Windows.Forms.DataGridViewTextBoxColumn();
            hkl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            bindingSourceObsSpots = new System.Windows.Forms.BindingSource(components);
            dataSet = new DataSetReciPro();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            groupBoxIndex = new System.Windows.Forms.GroupBox();
            buttonIdentifySpots = new System.Windows.Forms.Button();
            numericBoxSemiangle = new NumericBox();
            numericBoxMaxNumOfG = new NumericBox();
            numericBoxAcceptableError = new NumericBox();
            numericBoxMaxGrainNum = new NumericBox();
            checkBoxShowZoneAxis = new System.Windows.Forms.CheckBox();
            checkBoxShowCalcSpotSymbol = new System.Windows.Forms.CheckBox();
            radioButtonMultiGrain = new System.Windows.Forms.RadioButton();
            radioButtonSingleGrain = new System.Windows.Forms.RadioButton();
            checkBoxShowCalcSpotLabel = new System.Windows.Forms.CheckBox();
            dataGridViewGrains = new System.Windows.Forms.DataGridView();
            noDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            CrystalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            assignedSpotsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bindingSourceGrains = new System.Windows.Forms.BindingSource(components);
            dataGridViewCandidates = new System.Windows.Forms.DataGridView();
            noDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            AssignedSpots = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bindingSourceCandidates = new System.Windows.Forms.BindingSource(components);
            checkBoxIgnoreMultipleDiffraction = new System.Windows.Forms.CheckBox();
            buttonRefineThicknessAndDirection = new System.Windows.Forms.Button();
            buttonStop = new System.Windows.Forms.Button();
            groupBoxOptics = new System.Windows.Forms.GroupBox();
            radioButtonPixelSizeUnitInverse = new System.Windows.Forms.RadioButton();
            radioButtonPixelSizeUnitReal = new System.Windows.Forms.RadioButton();
            numericBoxCameraLength = new NumericBox();
            numericBoxPixelSize = new NumericBox();
            waveLengthControl1 = new WaveLengthControl();
            checkBoxGuideCircles = new System.Windows.Forms.CheckBox();
            checkBoxShowDebyeRing = new System.Windows.Forms.CheckBox();
            menuStrip = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsBitmapToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyAsMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyAsBitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            shortcutHintsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            doubleClickAddSpotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            statusStrip = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabelImageFilter = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelFindSpot = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelIdentifySpot = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelRefine = new System.Windows.Forms.ToolStripStatusLabel();
            backgroundWorkerSpotID = new System.ComponentModel.BackgroundWorker();
            toolTip = new System.Windows.Forms.ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBoxSpot.SuspendLayout();
            panelSpotActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSpots).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceObsSpots).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBoxIndex.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGrains).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceGrains).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCandidates).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceCandidates).BeginInit();
            groupBoxOptics.SuspendLayout();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(scalablePictureBoxAdvanced);
            splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBoxSpot);
            splitContainer1.Panel2.Controls.Add(groupBoxIndex);
            splitContainer1.Panel2.Controls.Add(groupBoxOptics);
            splitContainer1.Panel2.Controls.Add(checkBoxGuideCircles);
            splitContainer1.Panel2.Controls.Add(checkBoxShowDebyeRing);
            splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            // 
            // scalablePictureBoxAdvanced
            // 
            scalablePictureBoxAdvanced.ClampIntensityRangeToNewData = true;
            scalablePictureBoxAdvanced.ColorVisible = true;
            scalablePictureBoxAdvanced.DecimalPlacesForIntensity = 0;
            resources.ApplyResources(scalablePictureBoxAdvanced, "scalablePictureBoxAdvanced");
            scalablePictureBoxAdvanced.FixZoomAndCenter = false;
            scalablePictureBoxAdvanced.FrequencyGraphVisible = false;
            scalablePictureBoxAdvanced.GradiaentVisible = true;
            scalablePictureBoxAdvanced.ImageFilter_DustAndScratches = true;
            scalablePictureBoxAdvanced.ImageFilter_DustAndScratchesRadius = 1.5D;
            scalablePictureBoxAdvanced.ImageFilter_DustAndScratchesThreshold = 3D;
            scalablePictureBoxAdvanced.ImageFilter_DustAndScratchesVisible = true;
            scalablePictureBoxAdvanced.ImageFilter_GaussianBlur = true;
            scalablePictureBoxAdvanced.ImageFilter_GaussianBlurRadius = 3D;
            scalablePictureBoxAdvanced.ImageFilter_GaussianBlurVisible = true;
            scalablePictureBoxAdvanced.ImageFilterVisible = true;
            scalablePictureBoxAdvanced.LogScaleBar = true;
            scalablePictureBoxAdvanced.LowerIntensity = 0D;
            scalablePictureBoxAdvanced.MagInfoVisible = true;
            scalablePictureBoxAdvanced.MaximumIntensity = 18285.576171875D;
            scalablePictureBoxAdvanced.MinimumIntensity = -2306.3408203125D;
            scalablePictureBoxAdvanced.MousePositionLabelVisible = true;
            scalablePictureBoxAdvanced.Name = "scalablePictureBoxAdvanced";
            scalablePictureBoxAdvanced.PolarityVisible = true;
            scalablePictureBoxAdvanced.ScaleVisible = true;
            scalablePictureBoxAdvanced.ShowGradiaent = true;
            scalablePictureBoxAdvanced.StatusVisible = false;
            scalablePictureBoxAdvanced.TitleVisible = false;
            scalablePictureBoxAdvanced.TrackBarVisible = true;
            scalablePictureBoxAdvanced.UpperIntensity = 255D;
            scalablePictureBoxAdvanced.VisibleGradient = true;
            scalablePictureBoxAdvanced.MouseDown2 += scalablePictureBoxAdvanced1_MouseDown2;
            scalablePictureBoxAdvanced.StatusChanged += scalablePictureBoxAdvanced_StatusChanged;
            scalablePictureBoxAdvanced.FilterChanged += ScalablePictureBoxAdvanced_FilterChanged;
            // 
            // groupBoxSpot
            // 
            resources.ApplyResources(groupBoxSpot, "groupBoxSpot");
            captureExtender.SetCapture(groupBoxSpot, true);
            groupBoxSpot.Controls.Add(panelSpotActions);
            groupBoxSpot.Controls.Add(pictureBox1);
            groupBoxSpot.Name = "groupBoxSpot";
            groupBoxSpot.TabStop = false;
            // 
            // panelSpotActions
            // 
            panelSpotActions.Controls.Add(buttonDeleteSpot);
            panelSpotActions.Controls.Add(buttonClearSpots);
            panelSpotActions.Controls.Add(buttonCopyToClipboard);
            panelSpotActions.Controls.Add(buttonSaveToFile);
            panelSpotActions.Controls.Add(checkBoxDetailsOfFunction);
            panelSpotActions.Controls.Add(checkBoxDetailsOfSpot);
            panelSpotActions.Controls.Add(checkBoxShowObsSpotSymbol);
            panelSpotActions.Controls.Add(checkBoxShowObsSpotLabel);
            panelSpotActions.Controls.Add(buttonCopmprehensiveFitting);
            panelSpotActions.Controls.Add(buttonResetRangeForAllSpots);
            panelSpotActions.Controls.Add(numericBoxNumberOfSpots);
            panelSpotActions.Controls.Add(buttonFindSpots);
            panelSpotActions.Controls.Add(numericBoxNearestNeighbor);
            panelSpotActions.Controls.Add(numericBoxFittingRange);
            panelSpotActions.Controls.Add(buttonGlobalFit);
            panelSpotActions.Controls.Add(numericBoxDonut);
            panelSpotActions.Controls.Add(buttonDonut);
            panelSpotActions.Controls.Add(dataGridViewSpots);
            resources.ApplyResources(panelSpotActions, "panelSpotActions");
            panelSpotActions.Name = "panelSpotActions";
            // 
            // buttonDeleteSpot
            // 
            resources.ApplyResources(buttonDeleteSpot, "buttonDeleteSpot");
            buttonDeleteSpot.BackColor = System.Drawing.Color.IndianRed;
            buttonDeleteSpot.ForeColor = System.Drawing.Color.White;
            buttonDeleteSpot.Name = "buttonDeleteSpot";
            toolTip.SetToolTip(buttonDeleteSpot, resources.GetString("buttonDeleteSpot.ToolTip"));
            buttonDeleteSpot.UseVisualStyleBackColor = false;
            buttonDeleteSpot.Click += buttonDeleteSpot_Click;
            // 
            // buttonClearSpots
            // 
            resources.ApplyResources(buttonClearSpots, "buttonClearSpots");
            buttonClearSpots.BackColor = System.Drawing.Color.IndianRed;
            buttonClearSpots.ForeColor = System.Drawing.Color.White;
            buttonClearSpots.Name = "buttonClearSpots";
            toolTip.SetToolTip(buttonClearSpots, resources.GetString("buttonClearSpots.ToolTip"));
            buttonClearSpots.UseVisualStyleBackColor = false;
            buttonClearSpots.Click += buttonClearSpots_Click;
            // 
            // buttonCopyToClipboard
            // 
            resources.ApplyResources(buttonCopyToClipboard, "buttonCopyToClipboard");
            buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            toolTip.SetToolTip(buttonCopyToClipboard, resources.GetString("buttonCopyToClipboard.ToolTip"));
            buttonCopyToClipboard.UseVisualStyleBackColor = true;
            buttonCopyToClipboard.Click += buttonCopyToClipboard_Click;
            // 
            // buttonSaveToFile
            // 
            resources.ApplyResources(buttonSaveToFile, "buttonSaveToFile");
            buttonSaveToFile.Name = "buttonSaveToFile";
            toolTip.SetToolTip(buttonSaveToFile, resources.GetString("buttonSaveToFile.ToolTip"));
            buttonSaveToFile.UseVisualStyleBackColor = true;
            buttonSaveToFile.Click += buttonCopyToClipboard_Click;
            // 
            // checkBoxDetailsOfFunction
            // 
            resources.ApplyResources(checkBoxDetailsOfFunction, "checkBoxDetailsOfFunction");
            checkBoxDetailsOfFunction.Checked = true;
            checkBoxDetailsOfFunction.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDetailsOfFunction.Name = "checkBoxDetailsOfFunction";
            toolTip.SetToolTip(checkBoxDetailsOfFunction, resources.GetString("checkBoxDetailsOfFunction.ToolTip"));
            checkBoxDetailsOfFunction.UseVisualStyleBackColor = true;
            checkBoxDetailsOfFunction.CheckedChanged += checkBoxDetailsOfFunction_CheckedChanged;
            // 
            // checkBoxDetailsOfSpot
            // 
            resources.ApplyResources(checkBoxDetailsOfSpot, "checkBoxDetailsOfSpot");
            checkBoxDetailsOfSpot.Name = "checkBoxDetailsOfSpot";
            toolTip.SetToolTip(checkBoxDetailsOfSpot, resources.GetString("checkBoxDetailsOfSpot.ToolTip"));
            checkBoxDetailsOfSpot.UseVisualStyleBackColor = true;
            checkBoxDetailsOfSpot.CheckedChanged += checkBoxDetailsOfSpot_CheckedChanged;
            // 
            // checkBoxShowObsSpotSymbol
            // 
            resources.ApplyResources(checkBoxShowObsSpotSymbol, "checkBoxShowObsSpotSymbol");
            checkBoxShowObsSpotSymbol.Checked = true;
            checkBoxShowObsSpotSymbol.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowObsSpotSymbol.Name = "checkBoxShowObsSpotSymbol";
            toolTip.SetToolTip(checkBoxShowObsSpotSymbol, resources.GetString("checkBoxShowObsSpotSymbol.ToolTip"));
            checkBoxShowObsSpotSymbol.UseVisualStyleBackColor = true;
            checkBoxShowObsSpotSymbol.CheckedChanged += checkBoxShowObsSpots_CheckedChanged;
            // 
            // checkBoxShowObsSpotLabel
            // 
            resources.ApplyResources(checkBoxShowObsSpotLabel, "checkBoxShowObsSpotLabel");
            checkBoxShowObsSpotLabel.Checked = true;
            checkBoxShowObsSpotLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowObsSpotLabel.Name = "checkBoxShowObsSpotLabel";
            toolTip.SetToolTip(checkBoxShowObsSpotLabel, resources.GetString("checkBoxShowObsSpotLabel.ToolTip"));
            checkBoxShowObsSpotLabel.UseVisualStyleBackColor = true;
            checkBoxShowObsSpotLabel.CheckedChanged += checkBoxShowObsSpots_CheckedChanged;
            // 
            // buttonCopmprehensiveFitting
            // 
            resources.ApplyResources(buttonCopmprehensiveFitting, "buttonCopmprehensiveFitting");
            buttonCopmprehensiveFitting.Name = "buttonCopmprehensiveFitting";
            toolTip.SetToolTip(buttonCopmprehensiveFitting, resources.GetString("buttonCopmprehensiveFitting.ToolTip"));
            buttonCopmprehensiveFitting.UseVisualStyleBackColor = true;
            buttonCopmprehensiveFitting.Click += buttonRefit_Click;
            // 
            // buttonResetRangeForAllSpots
            // 
            resources.ApplyResources(buttonResetRangeForAllSpots, "buttonResetRangeForAllSpots");
            buttonResetRangeForAllSpots.Name = "buttonResetRangeForAllSpots";
            toolTip.SetToolTip(buttonResetRangeForAllSpots, resources.GetString("buttonResetRangeForAllSpots.ToolTip"));
            buttonResetRangeForAllSpots.UseVisualStyleBackColor = true;
            buttonResetRangeForAllSpots.Click += ButtonResetRangeForAllSpots_Click;
            // 
            // numericBoxNumberOfSpots
            // 
            resources.ApplyResources(numericBoxNumberOfSpots, "numericBoxNumberOfSpots");
            numericBoxNumberOfSpots.BackColor = System.Drawing.SystemColors.Control;
            numericBoxNumberOfSpots.DecimalPlaces = 0;
            numericBoxNumberOfSpots.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxNumberOfSpots.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxNumberOfSpots.Maximum = 1000D;
            numericBoxNumberOfSpots.Minimum = 1D;
            numericBoxNumberOfSpots.Name = "numericBoxNumberOfSpots";
            numericBoxNumberOfSpots.RadianValue = 0.52359877559829882D;
            numericBoxNumberOfSpots.ShowUpDown = true;
            numericBoxNumberOfSpots.SkipEventDuringInput = false;
            numericBoxNumberOfSpots.SmartIncrement = true;
            numericBoxNumberOfSpots.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxNumberOfSpots, resources.GetString("numericBoxNumberOfSpots.ToolTip"));
            numericBoxNumberOfSpots.Value = 30D;
            // 
            // buttonFindSpots
            // 
            resources.ApplyResources(buttonFindSpots, "buttonFindSpots");
            buttonFindSpots.BackColor = System.Drawing.Color.SteelBlue;
            buttonFindSpots.ForeColor = System.Drawing.Color.White;
            buttonFindSpots.Name = "buttonFindSpots";
            toolTip.SetToolTip(buttonFindSpots, resources.GetString("buttonFindSpots.ToolTip"));
            buttonFindSpots.UseVisualStyleBackColor = false;
            buttonFindSpots.Click += buttonFindSpots_Click;
            // 
            // numericBoxNearestNeighbor
            // 
            resources.ApplyResources(numericBoxNearestNeighbor, "numericBoxNearestNeighbor");
            numericBoxNearestNeighbor.BackColor = System.Drawing.SystemColors.Control;
            numericBoxNearestNeighbor.DecimalPlaces = 0;
            numericBoxNearestNeighbor.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxNearestNeighbor.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxNearestNeighbor.Maximum = 1000D;
            numericBoxNearestNeighbor.Minimum = 1D;
            numericBoxNearestNeighbor.Name = "numericBoxNearestNeighbor";
            numericBoxNearestNeighbor.RadianValue = 0.17453292519943295D;
            numericBoxNearestNeighbor.ShowUpDown = true;
            numericBoxNearestNeighbor.SkipEventDuringInput = false;
            numericBoxNearestNeighbor.SmartIncrement = true;
            numericBoxNearestNeighbor.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxNearestNeighbor, resources.GetString("numericBoxNearestNeighbor.ToolTip"));
            numericBoxNearestNeighbor.Value = 10D;
            // 
            // numericBoxFittingRange
            // 
            resources.ApplyResources(numericBoxFittingRange, "numericBoxFittingRange");
            numericBoxFittingRange.BackColor = System.Drawing.SystemColors.Control;
            numericBoxFittingRange.DecimalPlaces = 1;
            numericBoxFittingRange.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxFittingRange.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxFittingRange.Maximum = 100D;
            numericBoxFittingRange.Minimum = 0D;
            numericBoxFittingRange.Name = "numericBoxFittingRange";
            numericBoxFittingRange.RadianValue = 0.3490658503988659D;
            numericBoxFittingRange.ShowUpDown = true;
            numericBoxFittingRange.SkipEventDuringInput = false;
            numericBoxFittingRange.SmartIncrement = true;
            numericBoxFittingRange.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxFittingRange, resources.GetString("numericBoxFittingRange.ToolTip"));
            numericBoxFittingRange.Value = 20D;
            // 
            // buttonGlobalFit
            // 
            resources.ApplyResources(buttonGlobalFit, "buttonGlobalFit");
            buttonGlobalFit.Name = "buttonGlobalFit";
            toolTip.SetToolTip(buttonGlobalFit, resources.GetString("buttonGlobalFit.ToolTip"));
            buttonGlobalFit.UseVisualStyleBackColor = true;
            buttonGlobalFit.Click += ButtonGlobalFit_Click;
            // 
            // numericBoxDonut
            // 
            resources.ApplyResources(numericBoxDonut, "numericBoxDonut");
            numericBoxDonut.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDonut.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDonut.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDonut.Maximum = 100D;
            numericBoxDonut.Minimum = 1D;
            numericBoxDonut.Name = "numericBoxDonut";
            numericBoxDonut.RadianValue = 0.087266462599716474D;
            numericBoxDonut.ShowUpDown = true;
            numericBoxDonut.SkipEventDuringInput = false;
            numericBoxDonut.SmartIncrement = true;
            numericBoxDonut.ThonsandsSeparator = true;
            numericBoxDonut.Value = 5D;
            // 
            // buttonDonut
            // 
            resources.ApplyResources(buttonDonut, "buttonDonut");
            buttonDonut.Name = "buttonDonut";
            toolTip.SetToolTip(buttonDonut, resources.GetString("buttonDonut.ToolTip"));
            buttonDonut.UseVisualStyleBackColor = true;
            buttonDonut.Click += buttonDonut_Click;
            // 
            // dataGridViewSpots
            // 
            dataGridViewSpots.AllowUserToAddRows = false;
            dataGridViewSpots.AllowUserToDeleteRows = false;
            dataGridViewSpots.AllowUserToResizeRows = false;
            resources.ApplyResources(dataGridViewSpots, "dataGridViewSpots");
            dataGridViewSpots.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewSpots.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewSpots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSpots.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Column2, noDataGridViewTextBoxColumn, Range, x0, y0, H1, H2, θ, η, A, B0, Bx, By, R, d, hkl, Column1 });
            dataGridViewSpots.DataSource = bindingSourceObsSpots;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("メイリオ", 9F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewSpots.DefaultCellStyle = dataGridViewCellStyle14;
            dataGridViewSpots.MultiSelect = false;
            dataGridViewSpots.Name = "dataGridViewSpots";
            dataGridViewSpots.RowHeadersVisible = false;
            dataGridViewSpots.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewSpots.RowTemplate.Height = 21;
            dataGridViewSpots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSpots.CellContentClick += dataGridViewSpots_CellContentClick;
            dataGridViewSpots.RowHeaderMouseDoubleClick += DataGridViewSpots_RowHeaderMouseDoubleClick;
            // 
            // Column2
            // 
            Column2.DataPropertyName = "Direct";
            resources.ApplyResources(Column2, "Column2");
            Column2.Name = "Column2";
            Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // noDataGridViewTextBoxColumn
            // 
            noDataGridViewTextBoxColumn.DataPropertyName = "No";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            noDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(noDataGridViewTextBoxColumn, "noDataGridViewTextBoxColumn");
            noDataGridViewTextBoxColumn.Name = "noDataGridViewTextBoxColumn";
            noDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Range
            // 
            Range.DataPropertyName = "Range";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "N1";
            dataGridViewCellStyle3.NullValue = null;
            Range.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(Range, "Range");
            Range.Name = "Range";
            // 
            // x0
            // 
            x0.DataPropertyName = "x0";
            dataGridViewCellStyle4.Format = "N1";
            dataGridViewCellStyle4.NullValue = null;
            x0.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(x0, "x0");
            x0.Name = "x0";
            // 
            // y0
            // 
            y0.DataPropertyName = "y0";
            dataGridViewCellStyle5.Format = "N1";
            dataGridViewCellStyle5.NullValue = null;
            y0.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(y0, "y0");
            y0.Name = "y0";
            // 
            // H1
            // 
            H1.DataPropertyName = "H1";
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            H1.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(H1, "H1");
            H1.Name = "H1";
            // 
            // H2
            // 
            H2.DataPropertyName = "H2";
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            H2.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(H2, "H2");
            H2.Name = "H2";
            // 
            // θ
            // 
            θ.DataPropertyName = "θ";
            dataGridViewCellStyle8.Format = "N1";
            dataGridViewCellStyle8.NullValue = null;
            θ.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(θ, "θ");
            θ.Name = "θ";
            // 
            // η
            // 
            η.DataPropertyName = "η";
            resources.ApplyResources(η, "η");
            η.Name = "η";
            // 
            // A
            // 
            A.DataPropertyName = "A";
            dataGridViewCellStyle9.Format = "0.0000E0";
            dataGridViewCellStyle9.NullValue = null;
            A.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(A, "A");
            A.Name = "A";
            // 
            // B0
            // 
            B0.DataPropertyName = "B0";
            resources.ApplyResources(B0, "B0");
            B0.Name = "B0";
            // 
            // Bx
            // 
            Bx.DataPropertyName = "Bx";
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = null;
            Bx.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(Bx, "Bx");
            Bx.Name = "Bx";
            // 
            // By
            // 
            By.DataPropertyName = "By";
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            By.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(By, "By");
            By.Name = "By";
            // 
            // R
            // 
            R.DataPropertyName = "R";
            resources.ApplyResources(R, "R");
            R.Name = "R";
            R.ReadOnly = true;
            // 
            // d
            // 
            d.DataPropertyName = "d";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N4";
            dataGridViewCellStyle12.NullValue = null;
            d.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(d, "d");
            d.Name = "d";
            d.ReadOnly = true;
            // 
            // hkl
            // 
            hkl.DataPropertyName = "HKL";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            hkl.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(hkl, "hkl");
            hkl.Name = "hkl";
            hkl.ReadOnly = true;
            // 
            // Column1
            // 
            Column1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(Column1, "Column1");
            Column1.Name = "Column1";
            Column1.Text = "Fit";
            Column1.UseColumnTextForButtonValue = true;
            // 
            // bindingSourceObsSpots
            // 
            bindingSourceObsSpots.DataMember = "DataTableSpot";
            bindingSourceObsSpots.DataSource = dataSet;
            bindingSourceObsSpots.CurrentChanged += bindingSourceSpot_CurrentChanged;
            bindingSourceObsSpots.ListChanged += bindingSourceObsSpots_ListChanged;
            // 
            // dataSet
            // 
            dataSet.DataSetName = "DataSet";
            dataSet.Namespace = "http://tempuri.org/DataSet.xsd";
            dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Image = Properties.Resources.TwoDimensionalPseudoVoigt;
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // groupBoxIndex
            // 
            resources.ApplyResources(groupBoxIndex, "groupBoxIndex");
            captureExtender.SetCapture(groupBoxIndex, true);
            groupBoxIndex.Controls.Add(buttonIdentifySpots);
            groupBoxIndex.Controls.Add(numericBoxSemiangle);
            groupBoxIndex.Controls.Add(numericBoxMaxNumOfG);
            groupBoxIndex.Controls.Add(numericBoxAcceptableError);
            groupBoxIndex.Controls.Add(numericBoxMaxGrainNum);
            groupBoxIndex.Controls.Add(checkBoxShowZoneAxis);
            groupBoxIndex.Controls.Add(checkBoxShowCalcSpotSymbol);
            groupBoxIndex.Controls.Add(radioButtonMultiGrain);
            groupBoxIndex.Controls.Add(radioButtonSingleGrain);
            groupBoxIndex.Controls.Add(checkBoxShowCalcSpotLabel);
            groupBoxIndex.Controls.Add(dataGridViewGrains);
            groupBoxIndex.Controls.Add(dataGridViewCandidates);
            groupBoxIndex.Controls.Add(checkBoxIgnoreMultipleDiffraction);
            groupBoxIndex.Controls.Add(buttonRefineThicknessAndDirection);
            groupBoxIndex.Controls.Add(buttonStop);
            groupBoxIndex.Name = "groupBoxIndex";
            groupBoxIndex.TabStop = false;
            // 
            // buttonIdentifySpots
            // 
            resources.ApplyResources(buttonIdentifySpots, "buttonIdentifySpots");
            buttonIdentifySpots.BackColor = System.Drawing.Color.SteelBlue;
            buttonIdentifySpots.ForeColor = System.Drawing.Color.White;
            buttonIdentifySpots.Name = "buttonIdentifySpots";
            toolTip.SetToolTip(buttonIdentifySpots, resources.GetString("buttonIdentifySpots.ToolTip"));
            buttonIdentifySpots.UseVisualStyleBackColor = false;
            buttonIdentifySpots.Click += buttonIdentifySpots_Click;
            // 
            // numericBoxSemiangle
            // 
            resources.ApplyResources(numericBoxSemiangle, "numericBoxSemiangle");
            numericBoxSemiangle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSemiangle.DecimalPlaces = 1;
            numericBoxSemiangle.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxSemiangle.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxSemiangle.Maximum = 10D;
            numericBoxSemiangle.Minimum = 1D;
            numericBoxSemiangle.Name = "numericBoxSemiangle";
            numericBoxSemiangle.RadianValue = 0.034906585039886591D;
            numericBoxSemiangle.ShowUpDown = true;
            numericBoxSemiangle.SmartIncrement = true;
            numericBoxSemiangle.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxSemiangle, resources.GetString("numericBoxSemiangle.ToolTip"));
            numericBoxSemiangle.Value = 2D;
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
            numericBoxMaxNumOfG.RadianValue = 6.9813170079773181D;
            numericBoxMaxNumOfG.ShowUpDown = true;
            numericBoxMaxNumOfG.SmartIncrement = true;
            numericBoxMaxNumOfG.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxMaxNumOfG, resources.GetString("numericBoxMaxNumOfG.ToolTip"));
            numericBoxMaxNumOfG.Value = 400D;
            // 
            // numericBoxAcceptableError
            // 
            resources.ApplyResources(numericBoxAcceptableError, "numericBoxAcceptableError");
            numericBoxAcceptableError.BackColor = System.Drawing.SystemColors.Control;
            numericBoxAcceptableError.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxAcceptableError.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxAcceptableError.Maximum = 10D;
            numericBoxAcceptableError.Minimum = 0.1D;
            numericBoxAcceptableError.Name = "numericBoxAcceptableError";
            numericBoxAcceptableError.RadianValue = 0.034906585039886591D;
            numericBoxAcceptableError.ShowUpDown = true;
            numericBoxAcceptableError.SkipEventDuringInput = false;
            numericBoxAcceptableError.SmartIncrement = true;
            numericBoxAcceptableError.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxAcceptableError, resources.GetString("numericBoxAcceptableError.ToolTip"));
            numericBoxAcceptableError.Value = 2D;
            // 
            // numericBoxMaxGrainNum
            // 
            resources.ApplyResources(numericBoxMaxGrainNum, "numericBoxMaxGrainNum");
            numericBoxMaxGrainNum.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxGrainNum.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxGrainNum.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxGrainNum.Maximum = 10D;
            numericBoxMaxGrainNum.Minimum = 0.1D;
            numericBoxMaxGrainNum.Name = "numericBoxMaxGrainNum";
            numericBoxMaxGrainNum.RadianValue = 0.034906585039886591D;
            numericBoxMaxGrainNum.ShowUpDown = true;
            numericBoxMaxGrainNum.SkipEventDuringInput = false;
            numericBoxMaxGrainNum.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxMaxGrainNum, resources.GetString("numericBoxMaxGrainNum.ToolTip"));
            numericBoxMaxGrainNum.Value = 2D;
            // 
            // checkBoxShowZoneAxis
            // 
            resources.ApplyResources(checkBoxShowZoneAxis, "checkBoxShowZoneAxis");
            checkBoxShowZoneAxis.Checked = true;
            checkBoxShowZoneAxis.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowZoneAxis.Name = "checkBoxShowZoneAxis";
            toolTip.SetToolTip(checkBoxShowZoneAxis, resources.GetString("checkBoxShowZoneAxis.ToolTip"));
            checkBoxShowZoneAxis.UseVisualStyleBackColor = true;
            checkBoxShowZoneAxis.CheckedChanged += checkBoxShowObsSpots_CheckedChanged;
            // 
            // checkBoxShowCalcSpotSymbol
            // 
            resources.ApplyResources(checkBoxShowCalcSpotSymbol, "checkBoxShowCalcSpotSymbol");
            checkBoxShowCalcSpotSymbol.Checked = true;
            checkBoxShowCalcSpotSymbol.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowCalcSpotSymbol.Name = "checkBoxShowCalcSpotSymbol";
            toolTip.SetToolTip(checkBoxShowCalcSpotSymbol, resources.GetString("checkBoxShowCalcSpotSymbol.ToolTip"));
            checkBoxShowCalcSpotSymbol.UseVisualStyleBackColor = true;
            checkBoxShowCalcSpotSymbol.CheckedChanged += checkBoxShowObsSpots_CheckedChanged;
            // 
            // radioButtonMultiGrain
            // 
            resources.ApplyResources(radioButtonMultiGrain, "radioButtonMultiGrain");
            radioButtonMultiGrain.Name = "radioButtonMultiGrain";
            toolTip.SetToolTip(radioButtonMultiGrain, resources.GetString("radioButtonMultiGrain.ToolTip"));
            radioButtonMultiGrain.UseVisualStyleBackColor = true;
            // 
            // radioButtonSingleGrain
            // 
            resources.ApplyResources(radioButtonSingleGrain, "radioButtonSingleGrain");
            radioButtonSingleGrain.Checked = true;
            radioButtonSingleGrain.Name = "radioButtonSingleGrain";
            radioButtonSingleGrain.TabStop = true;
            toolTip.SetToolTip(radioButtonSingleGrain, resources.GetString("radioButtonSingleGrain.ToolTip"));
            radioButtonSingleGrain.UseVisualStyleBackColor = true;
            radioButtonSingleGrain.CheckedChanged += radioButtonSingleGrain_CheckedChanged;
            // 
            // checkBoxShowCalcSpotLabel
            // 
            resources.ApplyResources(checkBoxShowCalcSpotLabel, "checkBoxShowCalcSpotLabel");
            checkBoxShowCalcSpotLabel.Name = "checkBoxShowCalcSpotLabel";
            toolTip.SetToolTip(checkBoxShowCalcSpotLabel, resources.GetString("checkBoxShowCalcSpotLabel.ToolTip"));
            checkBoxShowCalcSpotLabel.UseVisualStyleBackColor = true;
            checkBoxShowCalcSpotLabel.CheckedChanged += checkBoxShowObsSpots_CheckedChanged;
            // 
            // dataGridViewGrains
            // 
            dataGridViewGrains.AllowUserToAddRows = false;
            dataGridViewGrains.AllowUserToDeleteRows = false;
            dataGridViewGrains.AllowUserToResizeRows = false;
            resources.ApplyResources(dataGridViewGrains, "dataGridViewGrains");
            dataGridViewGrains.AutoGenerateColumns = false;
            dataGridViewGrains.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewGrains.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { noDataGridViewTextBoxColumn2, CrystalName, assignedSpotsDataGridViewTextBoxColumn });
            dataGridViewGrains.DataSource = bindingSourceGrains;
            dataGridViewGrains.Name = "dataGridViewGrains";
            dataGridViewGrains.ReadOnly = true;
            dataGridViewGrains.RowHeadersVisible = false;
            dataGridViewGrains.RowTemplate.Height = 21;
            dataGridViewGrains.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // noDataGridViewTextBoxColumn2
            // 
            noDataGridViewTextBoxColumn2.DataPropertyName = "No";
            resources.ApplyResources(noDataGridViewTextBoxColumn2, "noDataGridViewTextBoxColumn2");
            noDataGridViewTextBoxColumn2.Name = "noDataGridViewTextBoxColumn2";
            noDataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // CrystalName
            // 
            CrystalName.DataPropertyName = "CrystalName";
            resources.ApplyResources(CrystalName, "CrystalName");
            CrystalName.Name = "CrystalName";
            CrystalName.ReadOnly = true;
            // 
            // assignedSpotsDataGridViewTextBoxColumn
            // 
            assignedSpotsDataGridViewTextBoxColumn.DataPropertyName = "AssignedSpots";
            resources.ApplyResources(assignedSpotsDataGridViewTextBoxColumn, "assignedSpotsDataGridViewTextBoxColumn");
            assignedSpotsDataGridViewTextBoxColumn.Name = "assignedSpotsDataGridViewTextBoxColumn";
            assignedSpotsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSourceGrains
            // 
            bindingSourceGrains.DataMember = "DataTableGrain";
            bindingSourceGrains.DataSource = dataSet;
            bindingSourceGrains.CurrentChanged += bindingSourceGrains_CurrentChanged;
            // 
            // dataGridViewCandidates
            // 
            dataGridViewCandidates.AllowUserToAddRows = false;
            dataGridViewCandidates.AllowUserToDeleteRows = false;
            dataGridViewCandidates.AllowUserToResizeRows = false;
            resources.ApplyResources(dataGridViewCandidates, "dataGridViewCandidates");
            dataGridViewCandidates.AutoGenerateColumns = false;
            dataGridViewCandidates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCandidates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { noDataGridViewTextBoxColumn1, AssignedSpots });
            dataGridViewCandidates.DataSource = bindingSourceCandidates;
            dataGridViewCandidates.MultiSelect = false;
            dataGridViewCandidates.Name = "dataGridViewCandidates";
            dataGridViewCandidates.ReadOnly = true;
            dataGridViewCandidates.RowHeadersVisible = false;
            dataGridViewCandidates.RowTemplate.Height = 21;
            dataGridViewCandidates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // noDataGridViewTextBoxColumn1
            // 
            noDataGridViewTextBoxColumn1.DataPropertyName = "No";
            resources.ApplyResources(noDataGridViewTextBoxColumn1, "noDataGridViewTextBoxColumn1");
            noDataGridViewTextBoxColumn1.Name = "noDataGridViewTextBoxColumn1";
            noDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // AssignedSpots
            // 
            AssignedSpots.DataPropertyName = "AssignedSpots";
            resources.ApplyResources(AssignedSpots, "AssignedSpots");
            AssignedSpots.Name = "AssignedSpots";
            AssignedSpots.ReadOnly = true;
            // 
            // bindingSourceCandidates
            // 
            bindingSourceCandidates.DataMember = "DataTableCandidate";
            bindingSourceCandidates.DataSource = dataSet;
            bindingSourceCandidates.CurrentChanged += bindingSourceCandidates_CurrentChanged;
            // 
            // checkBoxIgnoreMultipleDiffraction
            // 
            resources.ApplyResources(checkBoxIgnoreMultipleDiffraction, "checkBoxIgnoreMultipleDiffraction");
            checkBoxIgnoreMultipleDiffraction.Name = "checkBoxIgnoreMultipleDiffraction";
            toolTip.SetToolTip(checkBoxIgnoreMultipleDiffraction, resources.GetString("checkBoxIgnoreMultipleDiffraction.ToolTip"));
            checkBoxIgnoreMultipleDiffraction.UseVisualStyleBackColor = true;
            checkBoxIgnoreMultipleDiffraction.CheckedChanged += checkBoxShowObsSpots_CheckedChanged;
            // 
            // buttonRefineThicknessAndDirection
            // 
            resources.ApplyResources(buttonRefineThicknessAndDirection, "buttonRefineThicknessAndDirection");
            buttonRefineThicknessAndDirection.BackColor = System.Drawing.Color.SteelBlue;
            buttonRefineThicknessAndDirection.ForeColor = System.Drawing.Color.White;
            buttonRefineThicknessAndDirection.Name = "buttonRefineThicknessAndDirection";
            toolTip.SetToolTip(buttonRefineThicknessAndDirection, resources.GetString("buttonRefineThicknessAndDirection.ToolTip"));
            buttonRefineThicknessAndDirection.UseVisualStyleBackColor = false;
            buttonRefineThicknessAndDirection.Click += ButtonRefineThicknessAndDirection_Click;
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
            // groupBoxOptics
            // 
            resources.ApplyResources(groupBoxOptics, "groupBoxOptics");
            captureExtender.SetCapture(groupBoxOptics, true);
            groupBoxOptics.Controls.Add(radioButtonPixelSizeUnitInverse);
            groupBoxOptics.Controls.Add(radioButtonPixelSizeUnitReal);
            groupBoxOptics.Controls.Add(numericBoxCameraLength);
            groupBoxOptics.Controls.Add(numericBoxPixelSize);
            groupBoxOptics.Controls.Add(waveLengthControl1);
            groupBoxOptics.Name = "groupBoxOptics";
            groupBoxOptics.TabStop = false;
            // 
            // radioButtonPixelSizeUnitInverse
            // 
            resources.ApplyResources(radioButtonPixelSizeUnitInverse, "radioButtonPixelSizeUnitInverse");
            radioButtonPixelSizeUnitInverse.Name = "radioButtonPixelSizeUnitInverse";
            toolTip.SetToolTip(radioButtonPixelSizeUnitInverse, resources.GetString("radioButtonPixelSizeUnitInverse.ToolTip"));
            radioButtonPixelSizeUnitInverse.UseVisualStyleBackColor = true;
            // 
            // radioButtonPixelSizeUnitReal
            // 
            resources.ApplyResources(radioButtonPixelSizeUnitReal, "radioButtonPixelSizeUnitReal");
            radioButtonPixelSizeUnitReal.Checked = true;
            radioButtonPixelSizeUnitReal.Name = "radioButtonPixelSizeUnitReal";
            radioButtonPixelSizeUnitReal.TabStop = true;
            toolTip.SetToolTip(radioButtonPixelSizeUnitReal, resources.GetString("radioButtonPixelSizeUnitReal.ToolTip"));
            radioButtonPixelSizeUnitReal.UseVisualStyleBackColor = true;
            radioButtonPixelSizeUnitReal.CheckedChanged += radioButtonPixelSizeUnitReal_CheckedChanged;
            // 
            // numericBoxCameraLength
            // 
            resources.ApplyResources(numericBoxCameraLength, "numericBoxCameraLength");
            numericBoxCameraLength.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength.Maximum = 10000D;
            numericBoxCameraLength.Minimum = 0D;
            numericBoxCameraLength.Name = "numericBoxCameraLength";
            numericBoxCameraLength.RadianValue = 17.453292519943293D;
            numericBoxCameraLength.SkipEventDuringInput = false;
            numericBoxCameraLength.SmartIncrement = true;
            numericBoxCameraLength.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxCameraLength, resources.GetString("numericBoxCameraLength.ToolTip"));
            numericBoxCameraLength.Value = 1000D;
            numericBoxCameraLength.ValueChanged += NumericBoxCameraLength_ValueChanged;
            // 
            // numericBoxPixelSize
            // 
            resources.ApplyResources(numericBoxPixelSize, "numericBoxPixelSize");
            numericBoxPixelSize.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelSize.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelSize.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelSize.Maximum = 100D;
            numericBoxPixelSize.Minimum = 0D;
            numericBoxPixelSize.Name = "numericBoxPixelSize";
            numericBoxPixelSize.RadianValue = 0.0008726646259971648D;
            numericBoxPixelSize.SkipEventDuringInput = false;
            numericBoxPixelSize.SmartIncrement = true;
            numericBoxPixelSize.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxPixelSize, resources.GetString("numericBoxPixelSize.ToolTip"));
            numericBoxPixelSize.Value = 0.05D;
            numericBoxPixelSize.ValueChanged += NumericBoxPixelSize_ValueChanged;
            // 
            // waveLengthControl1
            // 
            resources.ApplyResources(waveLengthControl1, "waveLengthControl1");
            waveLengthControl1.Direction = System.Windows.Forms.FlowDirection.LeftToRight;
            waveLengthControl1.Energy = 494.36741737D;
            waveLengthControl1.Monochrome = true;
            waveLengthControl1.Name = "waveLengthControl1";
            waveLengthControl1.ShowWaveSource = true;
            waveLengthControl1.WaveLength = 0.0025079347455D;
            waveLengthControl1.WaveSource = WaveSource.Xray;
            waveLengthControl1.XrayWaveSourceElementNumber = 0;
            waveLengthControl1.XrayWaveSourceLine = XrayLine.Ka1;
            waveLengthControl1.WavelengthChanged += WaveLengthControl1_WavelengthChanged;
            // 
            // checkBoxGuideCircles
            // 
            resources.ApplyResources(checkBoxGuideCircles, "checkBoxGuideCircles");
            checkBoxGuideCircles.Checked = true;
            checkBoxGuideCircles.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxGuideCircles.Name = "checkBoxGuideCircles";
            toolTip.SetToolTip(checkBoxGuideCircles, resources.GetString("checkBoxGuideCircles.ToolTip"));
            checkBoxGuideCircles.UseVisualStyleBackColor = true;
            checkBoxGuideCircles.CheckedChanged += checkBoxGuideCircles_CheckedChanged;
            // 
            // checkBoxShowDebyeRing
            // 
            resources.ApplyResources(checkBoxShowDebyeRing, "checkBoxShowDebyeRing");
            checkBoxShowDebyeRing.Name = "checkBoxShowDebyeRing";
            toolTip.SetToolTip(checkBoxShowDebyeRing, resources.GetString("checkBoxShowDebyeRing.ToolTip"));
            checkBoxShowDebyeRing.UseVisualStyleBackColor = true;
            checkBoxShowDebyeRing.CheckedChanged += checkBoxShowDebyeRing_CheckedChanged;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, shortcutHintsToolStripMenuItem });
            resources.ApplyResources(menuStrip, "menuStrip");
            menuStrip.Name = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            captureExtender.SetCapture(fileToolStripMenuItem, true);
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { readToolStripMenuItem, saveToolStripMenuItem, copyToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // readToolStripMenuItem
            // 
            readToolStripMenuItem.Name = "readToolStripMenuItem";
            resources.ApplyResources(readToolStripMenuItem, "readToolStripMenuItem");
            readToolStripMenuItem.Click += readToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveAsMetafileToolStripMenuItem, saveAsBitmapToolStripMenuItem1 });
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(saveToolStripMenuItem, "saveToolStripMenuItem");
            // 
            // saveAsMetafileToolStripMenuItem
            // 
            saveAsMetafileToolStripMenuItem.Name = "saveAsMetafileToolStripMenuItem";
            resources.ApplyResources(saveAsMetafileToolStripMenuItem, "saveAsMetafileToolStripMenuItem");
            saveAsMetafileToolStripMenuItem.Click += saveAsMetafileToolStripMenuItem_Click;
            // 
            // saveAsBitmapToolStripMenuItem1
            // 
            saveAsBitmapToolStripMenuItem1.Name = "saveAsBitmapToolStripMenuItem1";
            resources.ApplyResources(saveAsBitmapToolStripMenuItem1, "saveAsBitmapToolStripMenuItem1");
            saveAsBitmapToolStripMenuItem1.Click += saveAsBitmapToolStripMenuItem1_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { copyAsMetafileToolStripMenuItem, copyAsBitmapToolStripMenuItem });
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            resources.ApplyResources(copyToolStripMenuItem, "copyToolStripMenuItem");
            // 
            // copyAsMetafileToolStripMenuItem
            // 
            copyAsMetafileToolStripMenuItem.Name = "copyAsMetafileToolStripMenuItem";
            resources.ApplyResources(copyAsMetafileToolStripMenuItem, "copyAsMetafileToolStripMenuItem");
            copyAsMetafileToolStripMenuItem.Click += copyAsMetafileToolStripMenuItem_Click;
            // 
            // copyAsBitmapToolStripMenuItem
            // 
            copyAsBitmapToolStripMenuItem.Name = "copyAsBitmapToolStripMenuItem";
            resources.ApplyResources(copyAsBitmapToolStripMenuItem, "copyAsBitmapToolStripMenuItem");
            copyAsBitmapToolStripMenuItem.Click += copyAsBitmapToolStripMenuItem_Click;
            // 
            // shortcutHintsToolStripMenuItem
            // 
            shortcutHintsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { doubleClickAddSpotToolStripMenuItem, toolStripMenuItem7, toolStripMenuItem1, toolStripMenuItem6, toolStripMenuItem5, toolStripMenuItem4, toolStripMenuItem3 });
            shortcutHintsToolStripMenuItem.Name = "shortcutHintsToolStripMenuItem";
            resources.ApplyResources(shortcutHintsToolStripMenuItem, "shortcutHintsToolStripMenuItem");
            // 
            // doubleClickAddSpotToolStripMenuItem
            // 
            resources.ApplyResources(doubleClickAddSpotToolStripMenuItem, "doubleClickAddSpotToolStripMenuItem");
            doubleClickAddSpotToolStripMenuItem.Name = "doubleClickAddSpotToolStripMenuItem";
            // 
            // toolStripMenuItem7
            // 
            resources.ApplyResources(toolStripMenuItem7, "toolStripMenuItem7");
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // toolStripMenuItem6
            // 
            resources.ApplyResources(toolStripMenuItem6, "toolStripMenuItem6");
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            // 
            // toolStripMenuItem5
            // 
            resources.ApplyResources(toolStripMenuItem5, "toolStripMenuItem5");
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            // 
            // toolStripMenuItem4
            // 
            resources.ApplyResources(toolStripMenuItem4, "toolStripMenuItem4");
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            // 
            // toolStripMenuItem3
            // 
            resources.ApplyResources(toolStripMenuItem3, "toolStripMenuItem3");
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusLabelImageFilter, toolStripStatusLabelFindSpot, toolStripStatusLabelIdentifySpot, toolStripStatusLabelRefine });
            resources.ApplyResources(statusStrip, "statusStrip");
            statusStrip.Name = "statusStrip";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Maximum = 10000;
            toolStripProgressBar.Name = "toolStripProgressBar";
            resources.ApplyResources(toolStripProgressBar, "toolStripProgressBar");
            // 
            // toolStripStatusLabelImageFilter
            // 
            toolStripStatusLabelImageFilter.Name = "toolStripStatusLabelImageFilter";
            resources.ApplyResources(toolStripStatusLabelImageFilter, "toolStripStatusLabelImageFilter");
            // 
            // toolStripStatusLabelFindSpot
            // 
            toolStripStatusLabelFindSpot.Name = "toolStripStatusLabelFindSpot";
            resources.ApplyResources(toolStripStatusLabelFindSpot, "toolStripStatusLabelFindSpot");
            // 
            // toolStripStatusLabelIdentifySpot
            // 
            toolStripStatusLabelIdentifySpot.Name = "toolStripStatusLabelIdentifySpot";
            resources.ApplyResources(toolStripStatusLabelIdentifySpot, "toolStripStatusLabelIdentifySpot");
            // 
            // toolStripStatusLabelRefine
            // 
            toolStripStatusLabelRefine.Name = "toolStripStatusLabelRefine";
            resources.ApplyResources(toolStripStatusLabelRefine, "toolStripStatusLabelRefine");
            // 
            // backgroundWorkerSpotID
            // 
            backgroundWorkerSpotID.WorkerReportsProgress = true;
            backgroundWorkerSpotID.WorkerSupportsCancellation = true;
            backgroundWorkerSpotID.DoWork += backgroundWorkerSpotID_DoWork;
            backgroundWorkerSpotID.ProgressChanged += backgroundWorkerSpotID_ProgressChanged;
            backgroundWorkerSpotID.RunWorkerCompleted += backgroundWorkerSpotID_RunWorkerCompleted;
            // 
            // FormSpotIDV2
            // 
            AllowDrop = true;
            resources.ApplyResources(this, "$this");
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F); // 260329Cl 追加: 96dpi基準
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            KeyPreview = true;
            MainMenuStrip = menuStrip;
            Name = "FormSpotIDV2";
            FormClosing += FormSpotID_FormClosing;
            Load += FormSpotID_Load;
            DragDrop += FormSpotID_DragDrop;
            DragEnter += FormSpotID_DragEnter;
            KeyDown += FormSpotIDV2_KeyDown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBoxSpot.ResumeLayout(false);
            panelSpotActions.ResumeLayout(false);
            panelSpotActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSpots).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceObsSpots).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBoxIndex.ResumeLayout(false);
            groupBoxIndex.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGrains).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceGrains).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCandidates).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceCandidates).EndInit();
            groupBoxOptics.ResumeLayout(false);
            groupBoxOptics.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private WaveLengthControl waveLengthControl1;
        private System.Windows.Forms.GroupBox groupBoxOptics;
        private NumericBox numericBoxCameraLength;
        private NumericBox numericBoxPixelSize;
        private System.Windows.Forms.Button buttonIdentifySpots;
        private System.Windows.Forms.GroupBox groupBoxIndex;
        private System.Windows.Forms.DataGridView dataGridViewCandidates;
        private System.Windows.Forms.BindingSource bindingSourceCandidates;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFindSpot;
        private NumericBox numericBoxAcceptableError;
        private System.Windows.Forms.RadioButton radioButtonMultiGrain;
        private System.Windows.Forms.RadioButton radioButtonSingleGrain;
        private System.Windows.Forms.DataGridView dataGridViewGrains;
        private System.Windows.Forms.BindingSource bindingSourceGrains;
        private System.Windows.Forms.DataGridViewTextBoxColumn noDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssignedSpots;
        private NumericBox numericBoxMaxGrainNum;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSpotID;
        private System.Windows.Forms.DataGridViewTextBoxColumn noDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CrystalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn assignedSpotsDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.CheckBox checkBoxIgnoreMultipleDiffraction;
        private System.Windows.Forms.CheckBox checkBoxShowDebyeRing;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelIdentifySpot;
        public System.Windows.Forms.BindingSource bindingSourceObsSpots;
        public DataSetReciPro dataSet;
        public ScalablePictureBoxAdvanced scalablePictureBoxAdvanced;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem shortcutHintsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doubleClickAddSpotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.CheckBox checkBoxShowCalcSpotSymbol;
        private System.Windows.Forms.CheckBox checkBoxShowCalcSpotLabel;
        private System.Windows.Forms.Button buttonRefineThicknessAndDirection;
        private NumericBox numericBoxMaxNumOfG;
        private NumericBox numericBoxSemiangle;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImageFilter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRefine;
        private System.Windows.Forms.GroupBox groupBoxSpot;
        private System.Windows.Forms.DataGridView dataGridViewSpots;
        private System.Windows.Forms.CheckBox checkBoxShowObsSpotSymbol;
        private System.Windows.Forms.CheckBox checkBoxShowObsSpotLabel;
        private System.Windows.Forms.Button buttonSaveToFile;
        private NumericBox numericBoxNearestNeighbor;
        private NumericBox numericBoxNumberOfSpots;
        public NumericBox numericBoxFittingRange;
        private System.Windows.Forms.Button buttonCopyToClipboard;
        private System.Windows.Forms.Button buttonFindSpots;
        private System.Windows.Forms.Button buttonGlobalFit;
        private System.Windows.Forms.Button buttonResetRangeForAllSpots;
        private System.Windows.Forms.Button buttonCopmprehensiveFitting;
        private System.Windows.Forms.Button buttonClearSpots;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonDonut;
        private NumericBox numericBoxDonut;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panelSpotActions;
        private System.Windows.Forms.CheckBox checkBoxDetailsOfFunction;
        public System.Windows.Forms.CheckBox checkBoxDetailsOfSpot;
        private System.Windows.Forms.Button buttonDeleteSpot;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn noDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Range;
        private System.Windows.Forms.DataGridViewTextBoxColumn x0;
        private System.Windows.Forms.DataGridViewTextBoxColumn y0;
        private System.Windows.Forms.DataGridViewTextBoxColumn H1;
        private System.Windows.Forms.DataGridViewTextBoxColumn H2;
        private System.Windows.Forms.DataGridViewTextBoxColumn θ;
        private System.Windows.Forms.DataGridViewTextBoxColumn η;
        private System.Windows.Forms.DataGridViewTextBoxColumn A;
        private System.Windows.Forms.DataGridViewTextBoxColumn B0;
        private System.Windows.Forms.DataGridViewTextBoxColumn Bx;
        private System.Windows.Forms.DataGridViewTextBoxColumn By;
        private System.Windows.Forms.DataGridViewTextBoxColumn R;
        private System.Windows.Forms.DataGridViewTextBoxColumn d;
        private System.Windows.Forms.DataGridViewTextBoxColumn hkl;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMetafileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsBitmapToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAsMetafileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAsBitmapToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxShowZoneAxis;
        private System.Windows.Forms.RadioButton radioButtonPixelSizeUnitInverse;
        private System.Windows.Forms.RadioButton radioButtonPixelSizeUnitReal;
        private System.Windows.Forms.CheckBox checkBoxGuideCircles;
    }
}
