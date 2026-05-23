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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            scalablePictureBoxAdvanced = new ScalablePictureBoxAdvanced();
            groupBoxSpot = new System.Windows.Forms.GroupBox();
            panelSpotActions = new System.Windows.Forms.Panel();
            dataGridViewSpots = new DpiAwareDataGridView();
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
            panel3 = new System.Windows.Forms.Panel();
            flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxDetailsOfSpot = new System.Windows.Forms.CheckBox();
            checkBoxDetailsOfFunction = new System.Windows.Forms.CheckBox();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            buttonGlobalFit = new System.Windows.Forms.Button();
            buttonDonut = new System.Windows.Forms.Button();
            numericBoxDonut = new NumericBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            buttonDeleteSpot = new System.Windows.Forms.Button();
            buttonClearSpots = new System.Windows.Forms.Button();
            buttonSaveToFile = new System.Windows.Forms.Button();
            buttonCopyToClipboard = new System.Windows.Forms.Button();
            buttonCopmprehensiveFitting = new System.Windows.Forms.Button();
            flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxShowObsSpotSymbol = new System.Windows.Forms.CheckBox();
            checkBoxShowObsSpotLabel = new System.Windows.Forms.CheckBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            buttonFindSpots = new System.Windows.Forms.Button();
            numericBoxNumberOfSpots = new NumericBox();
            numericBoxNearestNeighbor = new NumericBox();
            numericBoxFittingRange = new NumericBox();
            buttonResetRangeForAllSpots = new System.Windows.Forms.Button();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            panel1 = new System.Windows.Forms.Panel();
            groupBoxOptics = new System.Windows.Forms.GroupBox();
            radioButtonPixelSizeUnitInverse = new System.Windows.Forms.RadioButton();
            radioButtonPixelSizeUnitReal = new System.Windows.Forms.RadioButton();
            numericBoxCameraLength = new NumericBox();
            numericBoxPixelSize = new NumericBox();
            waveLengthControl1 = new WaveLengthControl();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxShowDebyeRing = new System.Windows.Forms.CheckBox();
            checkBoxGuideCircles = new System.Windows.Forms.CheckBox();
            groupBoxIndex = new System.Windows.Forms.GroupBox();
            buttonIdentifySpots = new System.Windows.Forms.Button();
            flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
            buttonRefineThicknessAndDirection = new System.Windows.Forms.Button();
            numericBoxDiffractedWaves = new NumericBox();
            numericBoxSemiangle = new NumericBox();
            buttonStop = new System.Windows.Forms.Button();
            flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxAcceptableError = new NumericBox();
            checkBoxIgnoreMultipleDiffraction = new System.Windows.Forms.CheckBox();
            flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonSingleGrain = new System.Windows.Forms.RadioButton();
            radioButtonMultiGrain = new System.Windows.Forms.RadioButton();
            numericBoxMaxGrainNum = new NumericBox();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxShowZoneAxis = new System.Windows.Forms.CheckBox();
            checkBoxShowCalcSpotSymbol = new System.Windows.Forms.CheckBox();
            checkBoxShowCalcSpotLabel = new System.Windows.Forms.CheckBox();
            dataGridViewGrains = new DpiAwareDataGridView();
            noDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            CrystalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            assignedSpotsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bindingSourceGrains = new System.Windows.Forms.BindingSource(components);
            dataGridViewCandidates = new DpiAwareDataGridView();
            noDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            AssignedSpots = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bindingSourceCandidates = new System.Windows.Forms.BindingSource(components);
            menuStrip = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            panel3.SuspendLayout();
            flowLayoutPanel10.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel9.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            groupBoxOptics.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            groupBoxIndex.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGrains).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceGrains).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCandidates).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceCandidates).BeginInit();
            menuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(splitContainer1.Panel1, "splitContainer1.Panel1");
            splitContainer1.Panel1.Controls.Add(scalablePictureBoxAdvanced);
            splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Default;
            toolTip.SetToolTip(splitContainer1.Panel1, resources.GetString("splitContainer1.Panel1.ToolTip"));
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(splitContainer1.Panel2, "splitContainer1.Panel2");
            splitContainer1.Panel2.Controls.Add(groupBoxSpot);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Panel2.Controls.Add(groupBoxIndex);
            splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Arrow;
            toolTip.SetToolTip(splitContainer1.Panel2, resources.GetString("splitContainer1.Panel2.ToolTip"));
            toolTip.SetToolTip(splitContainer1, resources.GetString("splitContainer1.ToolTip"));
            // 
            // scalablePictureBoxAdvanced
            // 
            resources.ApplyResources(scalablePictureBoxAdvanced, "scalablePictureBoxAdvanced");
            scalablePictureBoxAdvanced.ClampIntensityRangeToNewData = true;
            scalablePictureBoxAdvanced.ColorVisible = true;
            scalablePictureBoxAdvanced.DecimalPlacesForIntensity = 0;
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
            toolTip.SetToolTip(scalablePictureBoxAdvanced, resources.GetString("scalablePictureBoxAdvanced.ToolTip"));
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
            toolTip.SetToolTip(groupBoxSpot, resources.GetString("groupBoxSpot.ToolTip"));
            // 
            // panelSpotActions
            // 
            resources.ApplyResources(panelSpotActions, "panelSpotActions");
            panelSpotActions.Controls.Add(dataGridViewSpots);
            panelSpotActions.Controls.Add(panel3);
            panelSpotActions.Controls.Add(flowLayoutPanel1);
            panelSpotActions.Name = "panelSpotActions";
            toolTip.SetToolTip(panelSpotActions, resources.GetString("panelSpotActions.ToolTip"));
            // 
            // dataGridViewSpots
            // 
            resources.ApplyResources(dataGridViewSpots, "dataGridViewSpots");
            dataGridViewSpots.AllowUserToAddRows = false;
            dataGridViewSpots.AllowUserToDeleteRows = false;
            dataGridViewSpots.AllowUserToResizeRows = false;
            dataGridViewSpots.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
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
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewSpots.DefaultCellStyle = dataGridViewCellStyle14;
            dataGridViewSpots.MultiSelect = false;
            dataGridViewSpots.Name = "dataGridViewSpots";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewSpots.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            dataGridViewSpots.RowHeadersVisible = false;
            dataGridViewSpots.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewSpots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            toolTip.SetToolTip(dataGridViewSpots, resources.GetString("dataGridViewSpots.ToolTip"));
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
            // panel3
            // 
            resources.ApplyResources(panel3, "panel3");
            panel3.Controls.Add(flowLayoutPanel10);
            panel3.Controls.Add(flowLayoutPanel2);
            panel3.Controls.Add(flowLayoutPanel3);
            panel3.Controls.Add(flowLayoutPanel9);
            panel3.Name = "panel3";
            toolTip.SetToolTip(panel3, resources.GetString("panel3.ToolTip"));
            // 
            // flowLayoutPanel10
            // 
            resources.ApplyResources(flowLayoutPanel10, "flowLayoutPanel10");
            flowLayoutPanel10.Controls.Add(checkBoxDetailsOfSpot);
            flowLayoutPanel10.Controls.Add(checkBoxDetailsOfFunction);
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            toolTip.SetToolTip(flowLayoutPanel10, resources.GetString("flowLayoutPanel10.ToolTip"));
            // 
            // checkBoxDetailsOfSpot
            // 
            resources.ApplyResources(checkBoxDetailsOfSpot, "checkBoxDetailsOfSpot");
            checkBoxDetailsOfSpot.Name = "checkBoxDetailsOfSpot";
            toolTip.SetToolTip(checkBoxDetailsOfSpot, resources.GetString("checkBoxDetailsOfSpot.ToolTip"));
            checkBoxDetailsOfSpot.UseVisualStyleBackColor = true;
            checkBoxDetailsOfSpot.CheckedChanged += checkBoxDetailsOfSpot_CheckedChanged;
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
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(buttonGlobalFit);
            flowLayoutPanel2.Controls.Add(buttonDonut);
            flowLayoutPanel2.Controls.Add(numericBoxDonut);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            toolTip.SetToolTip(flowLayoutPanel2, resources.GetString("flowLayoutPanel2.ToolTip"));
            // 
            // buttonGlobalFit
            // 
            resources.ApplyResources(buttonGlobalFit, "buttonGlobalFit");
            buttonGlobalFit.Name = "buttonGlobalFit";
            toolTip.SetToolTip(buttonGlobalFit, resources.GetString("buttonGlobalFit.ToolTip"));
            buttonGlobalFit.UseVisualStyleBackColor = true;
            buttonGlobalFit.Click += ButtonGlobalFit_Click;
            // 
            // buttonDonut
            // 
            resources.ApplyResources(buttonDonut, "buttonDonut");
            buttonDonut.Name = "buttonDonut";
            toolTip.SetToolTip(buttonDonut, resources.GetString("buttonDonut.ToolTip"));
            buttonDonut.UseVisualStyleBackColor = true;
            buttonDonut.Click += buttonDonut_Click;
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
            numericBoxDonut.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxDonut, resources.GetString("numericBoxDonut.ToolTip"));
            numericBoxDonut.Value = 5D;
            numericBoxDonut.ValueFontSize = 8F;
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(buttonDeleteSpot);
            flowLayoutPanel3.Controls.Add(buttonClearSpots);
            flowLayoutPanel3.Controls.Add(buttonSaveToFile);
            flowLayoutPanel3.Controls.Add(buttonCopyToClipboard);
            flowLayoutPanel3.Controls.Add(buttonCopmprehensiveFitting);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            toolTip.SetToolTip(flowLayoutPanel3, resources.GetString("flowLayoutPanel3.ToolTip"));
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
            // buttonSaveToFile
            // 
            resources.ApplyResources(buttonSaveToFile, "buttonSaveToFile");
            buttonSaveToFile.Name = "buttonSaveToFile";
            toolTip.SetToolTip(buttonSaveToFile, resources.GetString("buttonSaveToFile.ToolTip"));
            buttonSaveToFile.UseVisualStyleBackColor = true;
            buttonSaveToFile.Click += buttonCopyToClipboard_Click;
            // 
            // buttonCopyToClipboard
            // 
            resources.ApplyResources(buttonCopyToClipboard, "buttonCopyToClipboard");
            buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            toolTip.SetToolTip(buttonCopyToClipboard, resources.GetString("buttonCopyToClipboard.ToolTip"));
            buttonCopyToClipboard.UseVisualStyleBackColor = true;
            buttonCopyToClipboard.Click += buttonCopyToClipboard_Click;
            // 
            // buttonCopmprehensiveFitting
            // 
            resources.ApplyResources(buttonCopmprehensiveFitting, "buttonCopmprehensiveFitting");
            buttonCopmprehensiveFitting.Name = "buttonCopmprehensiveFitting";
            toolTip.SetToolTip(buttonCopmprehensiveFitting, resources.GetString("buttonCopmprehensiveFitting.ToolTip"));
            buttonCopmprehensiveFitting.UseVisualStyleBackColor = true;
            buttonCopmprehensiveFitting.Click += buttonRefit_Click;
            // 
            // flowLayoutPanel9
            // 
            resources.ApplyResources(flowLayoutPanel9, "flowLayoutPanel9");
            flowLayoutPanel9.Controls.Add(checkBoxShowObsSpotSymbol);
            flowLayoutPanel9.Controls.Add(checkBoxShowObsSpotLabel);
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            toolTip.SetToolTip(flowLayoutPanel9, resources.GetString("flowLayoutPanel9.ToolTip"));
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
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(buttonFindSpots);
            flowLayoutPanel1.Controls.Add(numericBoxNumberOfSpots);
            flowLayoutPanel1.Controls.Add(numericBoxNearestNeighbor);
            flowLayoutPanel1.Controls.Add(numericBoxFittingRange);
            flowLayoutPanel1.Controls.Add(buttonResetRangeForAllSpots);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            toolTip.SetToolTip(flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
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
            numericBoxNumberOfSpots.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxNumberOfSpots, resources.GetString("numericBoxNumberOfSpots.ToolTip"));
            numericBoxNumberOfSpots.Value = 30D;
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
            numericBoxNearestNeighbor.ThousandsSeparator = true;
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
            numericBoxFittingRange.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxFittingRange, resources.GetString("numericBoxFittingRange.ToolTip"));
            numericBoxFittingRange.Value = 20D;
            // 
            // buttonResetRangeForAllSpots
            // 
            resources.ApplyResources(buttonResetRangeForAllSpots, "buttonResetRangeForAllSpots");
            buttonResetRangeForAllSpots.Name = "buttonResetRangeForAllSpots";
            toolTip.SetToolTip(buttonResetRangeForAllSpots, resources.GetString("buttonResetRangeForAllSpots.ToolTip"));
            buttonResetRangeForAllSpots.UseVisualStyleBackColor = true;
            buttonResetRangeForAllSpots.Click += ButtonResetRangeForAllSpots_Click;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Image = Properties.Resources.TwoDimensionalPseudoVoigt;
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            toolTip.SetToolTip(pictureBox1, resources.GetString("pictureBox1.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Controls.Add(groupBoxOptics);
            panel1.Controls.Add(flowLayoutPanel5);
            panel1.Name = "panel1";
            toolTip.SetToolTip(panel1, resources.GetString("panel1.ToolTip"));
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
            toolTip.SetToolTip(groupBoxOptics, resources.GetString("groupBoxOptics.ToolTip"));
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
            numericBoxCameraLength.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxCameraLength, resources.GetString("numericBoxCameraLength.ToolTip1"));
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
            numericBoxPixelSize.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxPixelSize, resources.GetString("numericBoxPixelSize.ToolTip1"));
            numericBoxPixelSize.Value = 0.05D;
            numericBoxPixelSize.ValueChanged += NumericBoxPixelSize_ValueChanged;
            // 
            // waveLengthControl1
            // 
            resources.ApplyResources(waveLengthControl1, "waveLengthControl1");
            waveLengthControl1.Direction = System.Windows.Forms.FlowDirection.LeftToRight;
            waveLengthControl1.Energy = 494.36741727D;
            waveLengthControl1.Monochrome = true;
            waveLengthControl1.Name = "waveLengthControl1";
            waveLengthControl1.ShowWaveSource = true;
            toolTip.SetToolTip(waveLengthControl1, resources.GetString("waveLengthControl1.ToolTip"));
            waveLengthControl1.WaveLength = 0.0025079347460000003D;
            waveLengthControl1.WaveSource = WaveSource.Xray;
            waveLengthControl1.XrayWaveSourceElementNumber = 0;
            waveLengthControl1.XrayWaveSourceLine = XrayLine.Ka1;
            waveLengthControl1.WavelengthChanged += WaveLengthControl1_WavelengthChanged;
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(checkBoxShowDebyeRing);
            flowLayoutPanel5.Controls.Add(checkBoxGuideCircles);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            toolTip.SetToolTip(flowLayoutPanel5, resources.GetString("flowLayoutPanel5.ToolTip"));
            // 
            // checkBoxShowDebyeRing
            // 
            resources.ApplyResources(checkBoxShowDebyeRing, "checkBoxShowDebyeRing");
            checkBoxShowDebyeRing.Name = "checkBoxShowDebyeRing";
            toolTip.SetToolTip(checkBoxShowDebyeRing, resources.GetString("checkBoxShowDebyeRing.ToolTip"));
            checkBoxShowDebyeRing.UseVisualStyleBackColor = true;
            checkBoxShowDebyeRing.CheckedChanged += checkBoxShowDebyeRing_CheckedChanged;
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
            // groupBoxIndex
            // 
            resources.ApplyResources(groupBoxIndex, "groupBoxIndex");
            captureExtender.SetCapture(groupBoxIndex, true);
            groupBoxIndex.Controls.Add(buttonIdentifySpots);
            groupBoxIndex.Controls.Add(flowLayoutPanel11);
            groupBoxIndex.Controls.Add(buttonStop);
            groupBoxIndex.Controls.Add(flowLayoutPanel8);
            groupBoxIndex.Controls.Add(flowLayoutPanel7);
            groupBoxIndex.Controls.Add(flowLayoutPanel4);
            groupBoxIndex.Controls.Add(dataGridViewGrains);
            groupBoxIndex.Controls.Add(dataGridViewCandidates);
            groupBoxIndex.Name = "groupBoxIndex";
            groupBoxIndex.TabStop = false;
            toolTip.SetToolTip(groupBoxIndex, resources.GetString("groupBoxIndex.ToolTip"));
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
            // flowLayoutPanel11
            // 
            resources.ApplyResources(flowLayoutPanel11, "flowLayoutPanel11");
            flowLayoutPanel11.Controls.Add(buttonRefineThicknessAndDirection);
            flowLayoutPanel11.Controls.Add(numericBoxDiffractedWaves);
            flowLayoutPanel11.Controls.Add(numericBoxSemiangle);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            toolTip.SetToolTip(flowLayoutPanel11, resources.GetString("flowLayoutPanel11.ToolTip"));
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
            // numericBoxDiffractedWaves
            // 
            resources.ApplyResources(numericBoxDiffractedWaves, "numericBoxDiffractedWaves");
            numericBoxDiffractedWaves.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDiffractedWaves.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDiffractedWaves.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDiffractedWaves.Maximum = 2048D;
            numericBoxDiffractedWaves.Minimum = 1D;
            numericBoxDiffractedWaves.Name = "numericBoxDiffractedWaves";
            numericBoxDiffractedWaves.RadianValue = 6.9813170079773181D;
            numericBoxDiffractedWaves.ShowUpDown = true;
            numericBoxDiffractedWaves.SmartIncrement = true;
            numericBoxDiffractedWaves.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxDiffractedWaves, resources.GetString("numericBoxDiffractedWaves.ToolTip"));
            numericBoxDiffractedWaves.Value = 400D;
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
            numericBoxSemiangle.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxSemiangle, resources.GetString("numericBoxSemiangle.ToolTip"));
            numericBoxSemiangle.Value = 2D;
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
            // flowLayoutPanel8
            // 
            resources.ApplyResources(flowLayoutPanel8, "flowLayoutPanel8");
            flowLayoutPanel8.Controls.Add(numericBoxAcceptableError);
            flowLayoutPanel8.Controls.Add(checkBoxIgnoreMultipleDiffraction);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            toolTip.SetToolTip(flowLayoutPanel8, resources.GetString("flowLayoutPanel8.ToolTip"));
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
            numericBoxAcceptableError.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxAcceptableError, resources.GetString("numericBoxAcceptableError.ToolTip"));
            numericBoxAcceptableError.Value = 2D;
            // 
            // checkBoxIgnoreMultipleDiffraction
            // 
            resources.ApplyResources(checkBoxIgnoreMultipleDiffraction, "checkBoxIgnoreMultipleDiffraction");
            checkBoxIgnoreMultipleDiffraction.Name = "checkBoxIgnoreMultipleDiffraction";
            toolTip.SetToolTip(checkBoxIgnoreMultipleDiffraction, resources.GetString("checkBoxIgnoreMultipleDiffraction.ToolTip"));
            checkBoxIgnoreMultipleDiffraction.UseVisualStyleBackColor = true;
            checkBoxIgnoreMultipleDiffraction.CheckedChanged += checkBoxShowObsSpots_CheckedChanged;
            // 
            // flowLayoutPanel7
            // 
            resources.ApplyResources(flowLayoutPanel7, "flowLayoutPanel7");
            flowLayoutPanel7.Controls.Add(radioButtonSingleGrain);
            flowLayoutPanel7.Controls.Add(radioButtonMultiGrain);
            flowLayoutPanel7.Controls.Add(numericBoxMaxGrainNum);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            toolTip.SetToolTip(flowLayoutPanel7, resources.GetString("flowLayoutPanel7.ToolTip"));
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
            // radioButtonMultiGrain
            // 
            resources.ApplyResources(radioButtonMultiGrain, "radioButtonMultiGrain");
            radioButtonMultiGrain.Name = "radioButtonMultiGrain";
            toolTip.SetToolTip(radioButtonMultiGrain, resources.GetString("radioButtonMultiGrain.ToolTip"));
            radioButtonMultiGrain.UseVisualStyleBackColor = true;
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
            numericBoxMaxGrainNum.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxMaxGrainNum, resources.GetString("numericBoxMaxGrainNum.ToolTip"));
            numericBoxMaxGrainNum.Value = 2D;
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(checkBoxShowZoneAxis);
            flowLayoutPanel4.Controls.Add(checkBoxShowCalcSpotSymbol);
            flowLayoutPanel4.Controls.Add(checkBoxShowCalcSpotLabel);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            toolTip.SetToolTip(flowLayoutPanel4, resources.GetString("flowLayoutPanel4.ToolTip"));
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
            resources.ApplyResources(dataGridViewGrains, "dataGridViewGrains");
            dataGridViewGrains.AllowUserToAddRows = false;
            dataGridViewGrains.AllowUserToDeleteRows = false;
            dataGridViewGrains.AllowUserToResizeRows = false;
            dataGridViewGrains.AutoGenerateColumns = false;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewGrains.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            dataGridViewGrains.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewGrains.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { noDataGridViewTextBoxColumn2, CrystalName, assignedSpotsDataGridViewTextBoxColumn });
            dataGridViewGrains.DataSource = bindingSourceGrains;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewGrains.DefaultCellStyle = dataGridViewCellStyle17;
            dataGridViewGrains.Name = "dataGridViewGrains";
            dataGridViewGrains.ReadOnly = true;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewGrains.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            dataGridViewGrains.RowHeadersVisible = false;
            dataGridViewGrains.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            toolTip.SetToolTip(dataGridViewGrains, resources.GetString("dataGridViewGrains.ToolTip"));
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
            resources.ApplyResources(dataGridViewCandidates, "dataGridViewCandidates");
            dataGridViewCandidates.AllowUserToAddRows = false;
            dataGridViewCandidates.AllowUserToDeleteRows = false;
            dataGridViewCandidates.AllowUserToResizeRows = false;
            dataGridViewCandidates.AutoGenerateColumns = false;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewCandidates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            dataGridViewCandidates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCandidates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { noDataGridViewTextBoxColumn1, AssignedSpots });
            dataGridViewCandidates.DataSource = bindingSourceCandidates;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridViewCandidates.DefaultCellStyle = dataGridViewCellStyle20;
            dataGridViewCandidates.MultiSelect = false;
            dataGridViewCandidates.Name = "dataGridViewCandidates";
            dataGridViewCandidates.ReadOnly = true;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridViewCandidates.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            dataGridViewCandidates.RowHeadersVisible = false;
            dataGridViewCandidates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            toolTip.SetToolTip(dataGridViewCandidates, resources.GetString("dataGridViewCandidates.ToolTip"));
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
            // menuStrip
            // 
            resources.ApplyResources(menuStrip, "menuStrip");
            menuStrip.GripMargin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, shortcutHintsToolStripMenuItem });
            menuStrip.Name = "menuStrip";
            toolTip.SetToolTip(menuStrip, resources.GetString("menuStrip.ToolTip"));
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            captureExtender.SetCapture(fileToolStripMenuItem, true);
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { loadToolStripMenuItem, saveToolStripMenuItem, copyToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // loadToolStripMenuItem
            // 
            resources.ApplyResources(loadToolStripMenuItem, "loadToolStripMenuItem");
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(saveToolStripMenuItem, "saveToolStripMenuItem");
            saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveAsMetafileToolStripMenuItem, saveAsBitmapToolStripMenuItem1 });
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            // 
            // saveAsMetafileToolStripMenuItem
            // 
            resources.ApplyResources(saveAsMetafileToolStripMenuItem, "saveAsMetafileToolStripMenuItem");
            saveAsMetafileToolStripMenuItem.Name = "saveAsMetafileToolStripMenuItem";
            saveAsMetafileToolStripMenuItem.Click += saveAsMetafileToolStripMenuItem_Click;
            // 
            // saveAsBitmapToolStripMenuItem1
            // 
            resources.ApplyResources(saveAsBitmapToolStripMenuItem1, "saveAsBitmapToolStripMenuItem1");
            saveAsBitmapToolStripMenuItem1.Name = "saveAsBitmapToolStripMenuItem1";
            saveAsBitmapToolStripMenuItem1.Click += saveAsBitmapToolStripMenuItem1_Click;
            // 
            // copyToolStripMenuItem
            // 
            resources.ApplyResources(copyToolStripMenuItem, "copyToolStripMenuItem");
            copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { copyAsMetafileToolStripMenuItem, copyAsBitmapToolStripMenuItem });
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            // 
            // copyAsMetafileToolStripMenuItem
            // 
            resources.ApplyResources(copyAsMetafileToolStripMenuItem, "copyAsMetafileToolStripMenuItem");
            copyAsMetafileToolStripMenuItem.Name = "copyAsMetafileToolStripMenuItem";
            copyAsMetafileToolStripMenuItem.Click += copyAsMetafileToolStripMenuItem_Click;
            // 
            // copyAsBitmapToolStripMenuItem
            // 
            resources.ApplyResources(copyAsBitmapToolStripMenuItem, "copyAsBitmapToolStripMenuItem");
            copyAsBitmapToolStripMenuItem.Name = "copyAsBitmapToolStripMenuItem";
            copyAsBitmapToolStripMenuItem.Click += copyAsBitmapToolStripMenuItem_Click;
            // 
            // shortcutHintsToolStripMenuItem
            // 
            resources.ApplyResources(shortcutHintsToolStripMenuItem, "shortcutHintsToolStripMenuItem");
            shortcutHintsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { doubleClickAddSpotToolStripMenuItem, toolStripMenuItem7, toolStripMenuItem1, toolStripMenuItem6, toolStripMenuItem5, toolStripMenuItem4, toolStripMenuItem3 });
            shortcutHintsToolStripMenuItem.Name = "shortcutHintsToolStripMenuItem";
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
            resources.ApplyResources(statusStrip, "statusStrip");
            statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusLabelImageFilter, toolStripStatusLabelFindSpot, toolStripStatusLabelIdentifySpot, toolStripStatusLabelRefine });
            statusStrip.Name = "statusStrip";
            toolTip.SetToolTip(statusStrip, resources.GetString("statusStrip.ToolTip"));
            // 
            // toolStripProgressBar
            // 
            resources.ApplyResources(toolStripProgressBar, "toolStripProgressBar");
            toolStripProgressBar.Maximum = 10000;
            toolStripProgressBar.Name = "toolStripProgressBar";
            // 
            // toolStripStatusLabelImageFilter
            // 
            resources.ApplyResources(toolStripStatusLabelImageFilter, "toolStripStatusLabelImageFilter");
            toolStripStatusLabelImageFilter.Name = "toolStripStatusLabelImageFilter";
            // 
            // toolStripStatusLabelFindSpot
            // 
            resources.ApplyResources(toolStripStatusLabelFindSpot, "toolStripStatusLabelFindSpot");
            toolStripStatusLabelFindSpot.Name = "toolStripStatusLabelFindSpot";
            // 
            // toolStripStatusLabelIdentifySpot
            // 
            resources.ApplyResources(toolStripStatusLabelIdentifySpot, "toolStripStatusLabelIdentifySpot");
            toolStripStatusLabelIdentifySpot.Name = "toolStripStatusLabelIdentifySpot";
            // 
            // toolStripStatusLabelRefine
            // 
            resources.ApplyResources(toolStripStatusLabelRefine, "toolStripStatusLabelRefine");
            toolStripStatusLabelRefine.Name = "toolStripStatusLabelRefine";
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
            resources.ApplyResources(this, "$this");
            AllowDrop = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            KeyPreview = true;
            MainMenuStrip = menuStrip;
            Name = "FormSpotIDV2";
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            FormClosing += FormSpotID_FormClosing;
            Load += FormSpotID_Load;
            DragDrop += FormSpotID_DragDrop;
            DragEnter += FormSpotID_DragEnter;
            KeyDown += FormSpotIDV2_KeyDown;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBoxSpot.ResumeLayout(false);
            panelSpotActions.ResumeLayout(false);
            panelSpotActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSpots).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceObsSpots).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            flowLayoutPanel10.ResumeLayout(false);
            flowLayoutPanel10.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel9.ResumeLayout(false);
            flowLayoutPanel9.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBoxOptics.ResumeLayout(false);
            groupBoxOptics.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            groupBoxIndex.ResumeLayout(false);
            groupBoxIndex.PerformLayout();
            flowLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel11.PerformLayout();
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewGrains).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceGrains).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewCandidates).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceCandidates).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private WaveLengthControl waveLengthControl1;
        private System.Windows.Forms.GroupBox groupBoxOptics;
        private NumericBox numericBoxCameraLength;
        private NumericBox numericBoxPixelSize;
        private System.Windows.Forms.Button buttonIdentifySpots;
        private System.Windows.Forms.GroupBox groupBoxIndex;
        // private System.Windows.Forms.DataGridView dataGridViewCandidates; // 260518Ch 旧実装: DPI変更時に列幅が追従しない
        private Crystallography.Controls.DpiAwareDataGridView dataGridViewCandidates; // 260518Ch
        private System.Windows.Forms.BindingSource bindingSourceCandidates;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFindSpot;
        private NumericBox numericBoxAcceptableError;
        private System.Windows.Forms.RadioButton radioButtonMultiGrain;
        private System.Windows.Forms.RadioButton radioButtonSingleGrain;
        // private System.Windows.Forms.DataGridView dataGridViewGrains; // 260518Ch 旧実装: DPI変更時に列幅が追従しない
        private Crystallography.Controls.DpiAwareDataGridView dataGridViewGrains; // 260518Ch
        private System.Windows.Forms.BindingSource bindingSourceGrains;
        private NumericBox numericBoxMaxGrainNum;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSpotID;
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
        private NumericBox numericBoxDiffractedWaves;
        private NumericBox numericBoxSemiangle;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImageFilter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRefine;
        private System.Windows.Forms.GroupBox groupBoxSpot;
        // private System.Windows.Forms.DataGridView dataGridViewSpots; // 260518Ch 旧実装: DPI変更時に列幅が追従しない
        private Crystallography.Controls.DpiAwareDataGridView dataGridViewSpots; // 260518Ch
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel11;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn noDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CrystalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn assignedSpotsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn noDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssignedSpots;
    }
}
