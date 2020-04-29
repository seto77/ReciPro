using System;

namespace ReciPro
{
    partial class FormSpotID
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutHintsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleClickAddSpotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxShowDebyeRing = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonIdentifySpots = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowCalcSpotSymbol = new System.Windows.Forms.CheckBox();
            this.radioButtonMultiGrain = new System.Windows.Forms.RadioButton();
            this.radioButtonSingleGrain = new System.Windows.Forms.RadioButton();
            this.checkBoxShowCalcSpotLabel = new System.Windows.Forms.CheckBox();
            this.dataGridViewGrains = new System.Windows.Forms.DataGridView();
            this.CrystalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCandidates = new System.Windows.Forms.DataGridView();
            this.AssignedSpots = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBoxIgnoreMultipleDiffraction = new System.Windows.Forms.CheckBox();
            this.buttonRefineThicknessAndDirection = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelImageFilter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelFindSpot = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelIdentifySpot = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelRefine = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorkerSpotID = new System.ComponentModel.BackgroundWorker();
            this.buttonPixelToPixel = new System.Windows.Forms.Button();
            this.buttonCopyMetafile = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxDetailsSpot = new System.Windows.Forms.CheckBox();
            this.dataGridViewSpots = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Range = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.y0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.H1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.H2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.θ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.η = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.B0 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Bx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.By = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.d = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hkl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.checkBoxShowObsSpotSymbol = new System.Windows.Forms.CheckBox();
            this.checkBoxShowObsSpotLabel = new System.Windows.Forms.CheckBox();
            this.buttonSaveToFile = new System.Windows.Forms.Button();
            this.buttonCopyToClipboad = new System.Windows.Forms.Button();
            this.buttonFindSpots = new System.Windows.Forms.Button();
            this.buttonGlobalFit = new System.Windows.Forms.Button();
            this.buttonResetRangeForAllSpots = new System.Windows.Forms.Button();
            this.buttonCopmprehensiveFitting = new System.Windows.Forms.Button();
            this.buttonClearSpots = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonDonut = new System.Windows.Forms.Button();
            this.noDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceObsSpots = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet = new ReciPro.DataSetReciPro();
            this.noDataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.assignedSpotsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceGrains = new System.Windows.Forms.BindingSource(this.components);
            this.noDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceCandidates = new System.Windows.Forms.BindingSource(this.components);
            this.scalablePictureBoxAdvanced = new Crystallography.Controls.ScalablePictureBoxAdvanced();
            this.numericBoxDonut = new Crystallography.Controls.NumericBox();
            this.numericBoxNearestNeighbor = new Crystallography.Controls.NumericBox();
            this.numericBoxNumberOfSpots = new Crystallography.Controls.NumericBox();
            this.numericBoxFittingRange = new Crystallography.Controls.NumericBox();
            this.numericBoxSemiangle = new Crystallography.Controls.NumericBox();
            this.numericBoxMaxNumOfG = new Crystallography.Controls.NumericBox();
            this.numericBoxAcceptableError = new Crystallography.Controls.NumericBox();
            this.numericBoxMaxGrainNum = new Crystallography.Controls.NumericBox();
            this.numericBoxCameraLength = new Crystallography.Controls.NumericBox();
            this.numericBoxPixelSize = new Crystallography.Controls.NumericBox();
            this.waveLengthControl1 = new Crystallography.Controls.WaveLengthControl();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGrains)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCandidates)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceObsSpots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceGrains)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCandidates)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.shortcutHintsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1385, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.readToolStripMenuItem.Text = "Read";
            // 
            // shortcutHintsToolStripMenuItem
            // 
            this.shortcutHintsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.doubleClickAddSpotToolStripMenuItem,
            this.toolStripMenuItem7,
            this.toolStripMenuItem1,
            this.toolStripMenuItem6,
            this.toolStripMenuItem5,
            this.toolStripMenuItem4,
            this.toolStripMenuItem3});
            this.shortcutHintsToolStripMenuItem.Name = "shortcutHintsToolStripMenuItem";
            this.shortcutHintsToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.shortcutHintsToolStripMenuItem.Text = "Shortcut Hints";
            // 
            // doubleClickAddSpotToolStripMenuItem
            // 
            this.doubleClickAddSpotToolStripMenuItem.Enabled = false;
            this.doubleClickAddSpotToolStripMenuItem.Name = "doubleClickAddSpotToolStripMenuItem";
            this.doubleClickAddSpotToolStripMenuItem.Size = new System.Drawing.Size(283, 22);
            this.doubleClickAddSpotToolStripMenuItem.Text = "Left drag :Translate";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Enabled = false;
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItem7.Text = "Right drag: Zoom in";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Enabled = false;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItem1.Text = "Right click : Zoom out";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Enabled = false;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItem6.Text = "Left click : Select spot";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Enabled = false;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItem5.Text = "Left double click : Add spot ";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Enabled = false;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItem4.Text = "Ctrl + Left double click: Add direct spot ";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(283, 22);
            this.toolStripMenuItem3.Text = "Ctrl + Right click : Remove spot ";
            // 
            // checkBoxShowDebyeRing
            // 
            this.checkBoxShowDebyeRing.AutoSize = true;
            this.checkBoxShowDebyeRing.Enabled = false;
            this.checkBoxShowDebyeRing.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxShowDebyeRing.Location = new System.Drawing.Point(7, 5);
            this.checkBoxShowDebyeRing.Name = "checkBoxShowDebyeRing";
            this.checkBoxShowDebyeRing.Size = new System.Drawing.Size(132, 21);
            this.checkBoxShowDebyeRing.TabIndex = 6;
            this.checkBoxShowDebyeRing.Text = "Show Debye rings";
            this.checkBoxShowDebyeRing.UseVisualStyleBackColor = true;
            this.checkBoxShowDebyeRing.CheckedChanged += new System.EventHandler(this.checkBoxShowDebyeRing_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.numericBoxCameraLength);
            this.groupBox2.Controls.Add(this.numericBoxPixelSize);
            this.groupBox2.Controls.Add(this.waveLengthControl1);
            this.groupBox2.Location = new System.Drawing.Point(196, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(587, 102);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Optics";
            // 
            // buttonIdentifySpots
            // 
            this.buttonIdentifySpots.AutoSize = true;
            this.buttonIdentifySpots.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonIdentifySpots.ForeColor = System.Drawing.Color.White;
            this.buttonIdentifySpots.Location = new System.Drawing.Point(2, 49);
            this.buttonIdentifySpots.Name = "buttonIdentifySpots";
            this.buttonIdentifySpots.Size = new System.Drawing.Size(97, 27);
            this.buttonIdentifySpots.TabIndex = 2;
            this.buttonIdentifySpots.Text = "Identify Spots";
            this.buttonIdentifySpots.UseVisualStyleBackColor = false;
            this.buttonIdentifySpots.Click += new System.EventHandler(this.buttonIdentifySpots_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.numericBoxSemiangle);
            this.groupBox3.Controls.Add(this.numericBoxMaxNumOfG);
            this.groupBox3.Controls.Add(this.numericBoxAcceptableError);
            this.groupBox3.Controls.Add(this.numericBoxMaxGrainNum);
            this.groupBox3.Controls.Add(this.checkBoxShowCalcSpotSymbol);
            this.groupBox3.Controls.Add(this.radioButtonMultiGrain);
            this.groupBox3.Controls.Add(this.radioButtonSingleGrain);
            this.groupBox3.Controls.Add(this.checkBoxShowCalcSpotLabel);
            this.groupBox3.Controls.Add(this.dataGridViewGrains);
            this.groupBox3.Controls.Add(this.dataGridViewCandidates);
            this.groupBox3.Controls.Add(this.checkBoxIgnoreMultipleDiffraction);
            this.groupBox3.Controls.Add(this.buttonRefineThicknessAndDirection);
            this.groupBox3.Controls.Add(this.buttonIdentifySpots);
            this.groupBox3.Controls.Add(this.buttonStop);
            this.groupBox3.Location = new System.Drawing.Point(7, 574);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 185);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Index";
            // 
            // checkBoxShowCalcSpotSymbol
            // 
            this.checkBoxShowCalcSpotSymbol.AutoSize = true;
            this.checkBoxShowCalcSpotSymbol.Checked = true;
            this.checkBoxShowCalcSpotSymbol.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowCalcSpotSymbol.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxShowCalcSpotSymbol.Location = new System.Drawing.Point(8, 22);
            this.checkBoxShowCalcSpotSymbol.Name = "checkBoxShowCalcSpotSymbol";
            this.checkBoxShowCalcSpotSymbol.Size = new System.Drawing.Size(104, 21);
            this.checkBoxShowCalcSpotSymbol.TabIndex = 6;
            this.checkBoxShowCalcSpotSymbol.Text = "Show symbol";
            this.checkBoxShowCalcSpotSymbol.UseVisualStyleBackColor = true;
            this.checkBoxShowCalcSpotSymbol.CheckedChanged += new System.EventHandler(this.checkBoxShowObsSpots_CheckedChanged);
            // 
            // radioButtonMultiGrain
            // 
            this.radioButtonMultiGrain.AutoSize = true;
            this.radioButtonMultiGrain.Location = new System.Drawing.Point(102, 103);
            this.radioButtonMultiGrain.Name = "radioButtonMultiGrain";
            this.radioButtonMultiGrain.Size = new System.Drawing.Size(95, 21);
            this.radioButtonMultiGrain.TabIndex = 8;
            this.radioButtonMultiGrain.Text = "Multi grains";
            this.radioButtonMultiGrain.UseVisualStyleBackColor = true;
            // 
            // radioButtonSingleGrain
            // 
            this.radioButtonSingleGrain.AutoSize = true;
            this.radioButtonSingleGrain.Checked = true;
            this.radioButtonSingleGrain.Location = new System.Drawing.Point(6, 103);
            this.radioButtonSingleGrain.Name = "radioButtonSingleGrain";
            this.radioButtonSingleGrain.Size = new System.Drawing.Size(95, 21);
            this.radioButtonSingleGrain.TabIndex = 8;
            this.radioButtonSingleGrain.TabStop = true;
            this.radioButtonSingleGrain.Text = "Single grain";
            this.radioButtonSingleGrain.UseVisualStyleBackColor = true;
            this.radioButtonSingleGrain.CheckedChanged += new System.EventHandler(this.radioButtonSingleGrain_CheckedChanged);
            // 
            // checkBoxShowCalcSpotLabel
            // 
            this.checkBoxShowCalcSpotLabel.AutoSize = true;
            this.checkBoxShowCalcSpotLabel.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxShowCalcSpotLabel.Location = new System.Drawing.Point(118, 22);
            this.checkBoxShowCalcSpotLabel.Name = "checkBoxShowCalcSpotLabel";
            this.checkBoxShowCalcSpotLabel.Size = new System.Drawing.Size(90, 21);
            this.checkBoxShowCalcSpotLabel.TabIndex = 6;
            this.checkBoxShowCalcSpotLabel.Text = "Show label";
            this.checkBoxShowCalcSpotLabel.UseVisualStyleBackColor = true;
            this.checkBoxShowCalcSpotLabel.CheckedChanged += new System.EventHandler(this.checkBoxShowObsSpots_CheckedChanged);
            // 
            // dataGridViewGrains
            // 
            this.dataGridViewGrains.AllowUserToAddRows = false;
            this.dataGridViewGrains.AllowUserToDeleteRows = false;
            this.dataGridViewGrains.AllowUserToResizeRows = false;
            this.dataGridViewGrains.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewGrains.AutoGenerateColumns = false;
            this.dataGridViewGrains.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewGrains.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.noDataGridViewTextBoxColumn2,
            this.CrystalName,
            this.assignedSpotsDataGridViewTextBoxColumn});
            this.dataGridViewGrains.DataSource = this.bindingSourceGrains;
            this.dataGridViewGrains.Location = new System.Drawing.Point(449, 13);
            this.dataGridViewGrains.Name = "dataGridViewGrains";
            this.dataGridViewGrains.ReadOnly = true;
            this.dataGridViewGrains.RowHeadersVisible = false;
            this.dataGridViewGrains.RowTemplate.Height = 21;
            this.dataGridViewGrains.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewGrains.Size = new System.Drawing.Size(321, 111);
            this.dataGridViewGrains.TabIndex = 5;
            // 
            // CrystalName
            // 
            this.CrystalName.DataPropertyName = "CrystalName";
            this.CrystalName.HeaderText = "CrystalName";
            this.CrystalName.Name = "CrystalName";
            this.CrystalName.ReadOnly = true;
            // 
            // dataGridViewCandidates
            // 
            this.dataGridViewCandidates.AllowUserToAddRows = false;
            this.dataGridViewCandidates.AllowUserToDeleteRows = false;
            this.dataGridViewCandidates.AllowUserToResizeRows = false;
            this.dataGridViewCandidates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewCandidates.AutoGenerateColumns = false;
            this.dataGridViewCandidates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCandidates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.noDataGridViewTextBoxColumn1,
            this.AssignedSpots});
            this.dataGridViewCandidates.DataSource = this.bindingSourceCandidates;
            this.dataGridViewCandidates.Location = new System.Drawing.Point(286, 13);
            this.dataGridViewCandidates.MultiSelect = false;
            this.dataGridViewCandidates.Name = "dataGridViewCandidates";
            this.dataGridViewCandidates.ReadOnly = true;
            this.dataGridViewCandidates.RowHeadersVisible = false;
            this.dataGridViewCandidates.RowTemplate.Height = 21;
            this.dataGridViewCandidates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewCandidates.Size = new System.Drawing.Size(157, 111);
            this.dataGridViewCandidates.TabIndex = 5;
            // 
            // AssignedSpots
            // 
            this.AssignedSpots.DataPropertyName = "AssignedSpots";
            this.AssignedSpots.HeaderText = "Assigned Spots";
            this.AssignedSpots.Name = "AssignedSpots";
            this.AssignedSpots.ReadOnly = true;
            // 
            // checkBoxIgnoreMultipleDiffraction
            // 
            this.checkBoxIgnoreMultipleDiffraction.AutoSize = true;
            this.checkBoxIgnoreMultipleDiffraction.Location = new System.Drawing.Point(6, 79);
            this.checkBoxIgnoreMultipleDiffraction.Name = "checkBoxIgnoreMultipleDiffraction";
            this.checkBoxIgnoreMultipleDiffraction.Size = new System.Drawing.Size(180, 21);
            this.checkBoxIgnoreMultipleDiffraction.TabIndex = 6;
            this.checkBoxIgnoreMultipleDiffraction.Text = "Ignore Multiple Diffraction";
            this.checkBoxIgnoreMultipleDiffraction.UseVisualStyleBackColor = true;
            this.checkBoxIgnoreMultipleDiffraction.CheckedChanged += new System.EventHandler(this.checkBoxShowObsSpots_CheckedChanged);
            // 
            // buttonRefineThicknessAndDirection
            // 
            this.buttonRefineThicknessAndDirection.AutoSize = true;
            this.buttonRefineThicknessAndDirection.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonRefineThicknessAndDirection.ForeColor = System.Drawing.Color.White;
            this.buttonRefineThicknessAndDirection.Location = new System.Drawing.Point(511, 155);
            this.buttonRefineThicknessAndDirection.Name = "buttonRefineThicknessAndDirection";
            this.buttonRefineThicknessAndDirection.Size = new System.Drawing.Size(191, 27);
            this.buttonRefineThicknessAndDirection.TabIndex = 2;
            this.buttonRefineThicknessAndDirection.Text = "Refine thickness and direction";
            this.buttonRefineThicknessAndDirection.UseVisualStyleBackColor = false;
            this.buttonRefineThicknessAndDirection.Click += new System.EventHandler(this.ButtonRefineThicknessAndDirection_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.AutoSize = true;
            this.buttonStop.BackColor = System.Drawing.Color.IndianRed;
            this.buttonStop.ForeColor = System.Drawing.Color.White;
            this.buttonStop.Location = new System.Drawing.Point(2, 49);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(97, 27);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabelImageFilter,
            this.toolStripStatusLabelFindSpot,
            this.toolStripStatusLabelIdentifySpot,
            this.toolStripStatusLabelRefine});
            this.statusStrip1.Location = new System.Drawing.Point(0, 785);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1385, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Maximum = 10000;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabelImageFilter
            // 
            this.toolStripStatusLabelImageFilter.Name = "toolStripStatusLabelImageFilter";
            this.toolStripStatusLabelImageFilter.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabelImageFilter.Text = "     ";
            // 
            // toolStripStatusLabelFindSpot
            // 
            this.toolStripStatusLabelFindSpot.Name = "toolStripStatusLabelFindSpot";
            this.toolStripStatusLabelFindSpot.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabelFindSpot.Text = "      ";
            // 
            // toolStripStatusLabelIdentifySpot
            // 
            this.toolStripStatusLabelIdentifySpot.Name = "toolStripStatusLabelIdentifySpot";
            this.toolStripStatusLabelIdentifySpot.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabelIdentifySpot.Text = "     ";
            // 
            // toolStripStatusLabelRefine
            // 
            this.toolStripStatusLabelRefine.Name = "toolStripStatusLabelRefine";
            this.toolStripStatusLabelRefine.Size = new System.Drawing.Size(22, 17);
            this.toolStripStatusLabelRefine.Text = "     ";
            // 
            // backgroundWorkerSpotID
            // 
            this.backgroundWorkerSpotID.WorkerReportsProgress = true;
            this.backgroundWorkerSpotID.WorkerSupportsCancellation = true;
            this.backgroundWorkerSpotID.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSpotID_DoWork);
            this.backgroundWorkerSpotID.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerSpotID_ProgressChanged);
            this.backgroundWorkerSpotID.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerSpotID_RunWorkerCompleted);
            // 
            // buttonPixelToPixel
            // 
            this.buttonPixelToPixel.AutoSize = true;
            this.buttonPixelToPixel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonPixelToPixel.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.buttonPixelToPixel.Location = new System.Drawing.Point(62, 3);
            this.buttonPixelToPixel.Name = "buttonPixelToPixel";
            this.buttonPixelToPixel.Size = new System.Drawing.Size(82, 25);
            this.buttonPixelToPixel.TabIndex = 11;
            this.buttonPixelToPixel.Text = "Pixel to Pixel";
            this.buttonPixelToPixel.UseVisualStyleBackColor = true;
            this.buttonPixelToPixel.Click += new System.EventHandler(this.buttonPixelToPixel_Click);
            // 
            // buttonCopyMetafile
            // 
            this.buttonCopyMetafile.AutoSize = true;
            this.buttonCopyMetafile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCopyMetafile.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.buttonCopyMetafile.Location = new System.Drawing.Point(3, 3);
            this.buttonCopyMetafile.Name = "buttonCopyMetafile";
            this.buttonCopyMetafile.Size = new System.Drawing.Size(60, 25);
            this.buttonCopyMetafile.TabIndex = 11;
            this.buttonCopyMetafile.Text = "Metafile";
            this.buttonCopyMetafile.UseVisualStyleBackColor = true;
            this.buttonCopyMetafile.Click += new System.EventHandler(this.buttonCopyMetafile_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.buttonCopyMetafile);
            this.splitContainer1.Panel1.Controls.Add(this.buttonPixelToPixel);
            this.splitContainer1.Panel1.Controls.Add(this.scalablePictureBoxAdvanced);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxShowDebyeRing);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1385, 761);
            this.splitContainer1.SplitterDistance = 595;
            this.splitContainer1.TabIndex = 12;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.numericBoxDonut);
            this.groupBox1.Controls.Add(this.checkBoxDetailsSpot);
            this.groupBox1.Controls.Add(this.dataGridViewSpots);
            this.groupBox1.Controls.Add(this.checkBoxShowObsSpotSymbol);
            this.groupBox1.Controls.Add(this.checkBoxShowObsSpotLabel);
            this.groupBox1.Controls.Add(this.buttonSaveToFile);
            this.groupBox1.Controls.Add(this.numericBoxNearestNeighbor);
            this.groupBox1.Controls.Add(this.numericBoxNumberOfSpots);
            this.groupBox1.Controls.Add(this.numericBoxFittingRange);
            this.groupBox1.Controls.Add(this.buttonCopyToClipboad);
            this.groupBox1.Controls.Add(this.buttonFindSpots);
            this.groupBox1.Controls.Add(this.buttonDonut);
            this.groupBox1.Controls.Add(this.buttonGlobalFit);
            this.groupBox1.Controls.Add(this.buttonResetRangeForAllSpots);
            this.groupBox1.Controls.Add(this.buttonCopmprehensiveFitting);
            this.groupBox1.Controls.Add(this.buttonClearSpots);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.groupBox1.Location = new System.Drawing.Point(7, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 464);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spot";
            // 
            // checkBoxDetailsSpot
            // 
            this.checkBoxDetailsSpot.AutoSize = true;
            this.checkBoxDetailsSpot.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxDetailsSpot.Location = new System.Drawing.Point(551, 19);
            this.checkBoxDetailsSpot.Name = "checkBoxDetailsSpot";
            this.checkBoxDetailsSpot.Size = new System.Drawing.Size(115, 38);
            this.checkBoxDetailsSpot.TabIndex = 6;
            this.checkBoxDetailsSpot.Text = "Details of the\r\n selected spots";
            this.checkBoxDetailsSpot.UseVisualStyleBackColor = true;
            this.checkBoxDetailsSpot.CheckedChanged += new System.EventHandler(this.checkBoxDetailsSpot_CheckedChanged);
            // 
            // dataGridViewSpots
            // 
            this.dataGridViewSpots.AllowUserToDeleteRows = false;
            this.dataGridViewSpots.AllowUserToResizeRows = false;
            this.dataGridViewSpots.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSpots.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSpots.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSpots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSpots.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.noDataGridViewTextBoxColumn,
            this.Range,
            this.x0,
            this.y0,
            this.H1,
            this.H2,
            this.θ,
            this.η,
            this.A,
            this.B0,
            this.Bx,
            this.By,
            this.R,
            this.d,
            this.hkl,
            this.Column1});
            this.dataGridViewSpots.DataSource = this.bindingSourceObsSpots;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.Format = "N2";
            dataGridViewCellStyle14.NullValue = null;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSpots.DefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewSpots.Location = new System.Drawing.Point(3, 71);
            this.dataGridViewSpots.MultiSelect = false;
            this.dataGridViewSpots.Name = "dataGridViewSpots";
            this.dataGridViewSpots.RowHeadersWidth = 20;
            this.dataGridViewSpots.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewSpots.RowTemplate.Height = 21;
            this.dataGridViewSpots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSpots.Size = new System.Drawing.Size(767, 304);
            this.dataGridViewSpots.TabIndex = 5;
            this.dataGridViewSpots.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSpots_CellContentClick);
            this.dataGridViewSpots.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewSpots_RowHeaderMouseDoubleClick);
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Direct";
            this.Column2.HeaderText = "Di- rect";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column2.Width = 31;
            // 
            // Range
            // 
            this.Range.DataPropertyName = "Range";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Format = "N1";
            dataGridViewCellStyle3.NullValue = null;
            this.Range.DefaultCellStyle = dataGridViewCellStyle3;
            this.Range.HeaderText = "Range (radius)";
            this.Range.Name = "Range";
            this.Range.Width = 48;
            // 
            // x0
            // 
            this.x0.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.x0.DataPropertyName = "x0";
            dataGridViewCellStyle4.Format = "N1";
            dataGridViewCellStyle4.NullValue = null;
            this.x0.DefaultCellStyle = dataGridViewCellStyle4;
            this.x0.HeaderText = "x0";
            this.x0.Name = "x0";
            this.x0.Width = 46;
            // 
            // y0
            // 
            this.y0.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.y0.DataPropertyName = "y0";
            dataGridViewCellStyle5.Format = "N1";
            dataGridViewCellStyle5.NullValue = null;
            this.y0.DefaultCellStyle = dataGridViewCellStyle5;
            this.y0.HeaderText = "y0";
            this.y0.Name = "y0";
            this.y0.Width = 46;
            // 
            // H1
            // 
            this.H1.DataPropertyName = "H1";
            dataGridViewCellStyle6.Format = "N2";
            dataGridViewCellStyle6.NullValue = null;
            this.H1.DefaultCellStyle = dataGridViewCellStyle6;
            this.H1.HeaderText = "H1";
            this.H1.Name = "H1";
            this.H1.Width = 36;
            // 
            // H2
            // 
            this.H2.DataPropertyName = "H2";
            dataGridViewCellStyle7.Format = "N2";
            dataGridViewCellStyle7.NullValue = null;
            this.H2.DefaultCellStyle = dataGridViewCellStyle7;
            this.H2.HeaderText = "H2";
            this.H2.Name = "H2";
            this.H2.Width = 36;
            // 
            // θ
            // 
            this.θ.DataPropertyName = "θ";
            dataGridViewCellStyle8.Format = "N1";
            dataGridViewCellStyle8.NullValue = null;
            this.θ.DefaultCellStyle = dataGridViewCellStyle8;
            this.θ.HeaderText = "θ (°)";
            this.θ.Name = "θ";
            this.θ.Width = 40;
            // 
            // η
            // 
            this.η.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.η.DataPropertyName = "η";
            this.η.HeaderText = "η";
            this.η.Name = "η";
            this.η.Width = 40;
            // 
            // A
            // 
            this.A.DataPropertyName = "A";
            dataGridViewCellStyle9.Format = "0.0000E0";
            dataGridViewCellStyle9.NullValue = null;
            this.A.DefaultCellStyle = dataGridViewCellStyle9;
            this.A.HeaderText = "A (intensity)";
            this.A.Name = "A";
            this.A.Width = 68;
            // 
            // B0
            // 
            this.B0.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.B0.DataPropertyName = "B0";
            this.B0.HeaderText = "B0";
            this.B0.Name = "B0";
            this.B0.Width = 47;
            // 
            // Bx
            // 
            this.Bx.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Bx.DataPropertyName = "Bx";
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = null;
            this.Bx.DefaultCellStyle = dataGridViewCellStyle10;
            this.Bx.HeaderText = "Bx";
            this.Bx.Name = "Bx";
            this.Bx.Width = 46;
            // 
            // By
            // 
            this.By.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.By.DataPropertyName = "By";
            dataGridViewCellStyle11.Format = "N2";
            dataGridViewCellStyle11.NullValue = null;
            this.By.DefaultCellStyle = dataGridViewCellStyle11;
            this.By.HeaderText = "By";
            this.By.Name = "By";
            this.By.Width = 46;
            // 
            // R
            // 
            this.R.DataPropertyName = "R";
            this.R.HeaderText = "R (%)";
            this.R.Name = "R";
            this.R.ReadOnly = true;
            this.R.Width = 40;
            // 
            // d
            // 
            this.d.DataPropertyName = "d";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N4";
            dataGridViewCellStyle12.NullValue = null;
            this.d.DefaultCellStyle = dataGridViewCellStyle12;
            this.d.HeaderText = "d (nm)";
            this.d.Name = "d";
            this.d.ReadOnly = true;
            this.d.Width = 50;
            // 
            // hkl
            // 
            this.hkl.DataPropertyName = "HKL";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.hkl.DefaultCellStyle = dataGridViewCellStyle13;
            this.hkl.HeaderText = "hkl";
            this.hkl.Name = "hkl";
            this.hkl.ReadOnly = true;
            this.hkl.Width = 60;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Column1.HeaderText = "Fit    ";
            this.Column1.Name = "Column1";
            this.Column1.Text = "Fit";
            this.Column1.UseColumnTextForButtonValue = true;
            this.Column1.Width = 28;
            // 
            // checkBoxShowObsSpotSymbol
            // 
            this.checkBoxShowObsSpotSymbol.AutoSize = true;
            this.checkBoxShowObsSpotSymbol.Checked = true;
            this.checkBoxShowObsSpotSymbol.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowObsSpotSymbol.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxShowObsSpotSymbol.Location = new System.Drawing.Point(9, 19);
            this.checkBoxShowObsSpotSymbol.Name = "checkBoxShowObsSpotSymbol";
            this.checkBoxShowObsSpotSymbol.Size = new System.Drawing.Size(104, 21);
            this.checkBoxShowObsSpotSymbol.TabIndex = 6;
            this.checkBoxShowObsSpotSymbol.Text = "Show symbol";
            this.checkBoxShowObsSpotSymbol.UseVisualStyleBackColor = true;
            this.checkBoxShowObsSpotSymbol.CheckedChanged += new System.EventHandler(this.checkBoxShowObsSpots_CheckedChanged);
            // 
            // checkBoxShowObsSpotLabel
            // 
            this.checkBoxShowObsSpotLabel.AutoSize = true;
            this.checkBoxShowObsSpotLabel.Checked = true;
            this.checkBoxShowObsSpotLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowObsSpotLabel.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxShowObsSpotLabel.Location = new System.Drawing.Point(9, 44);
            this.checkBoxShowObsSpotLabel.Name = "checkBoxShowObsSpotLabel";
            this.checkBoxShowObsSpotLabel.Size = new System.Drawing.Size(90, 21);
            this.checkBoxShowObsSpotLabel.TabIndex = 6;
            this.checkBoxShowObsSpotLabel.Text = "Show label";
            this.checkBoxShowObsSpotLabel.UseVisualStyleBackColor = true;
            this.checkBoxShowObsSpotLabel.CheckedChanged += new System.EventHandler(this.checkBoxShowObsSpots_CheckedChanged);
            // 
            // buttonSaveToFile
            // 
            this.buttonSaveToFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveToFile.AutoSize = true;
            this.buttonSaveToFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSaveToFile.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.buttonSaveToFile.Location = new System.Drawing.Point(677, 40);
            this.buttonSaveToFile.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSaveToFile.Name = "buttonSaveToFile";
            this.buttonSaveToFile.Size = new System.Drawing.Size(45, 27);
            this.buttonSaveToFile.TabIndex = 2;
            this.buttonSaveToFile.Text = "Save";
            this.buttonSaveToFile.UseVisualStyleBackColor = true;
            this.buttonSaveToFile.Click += new System.EventHandler(this.buttonCopyToClipboad_Click);
            // 
            // buttonCopyToClipboad
            // 
            this.buttonCopyToClipboad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopyToClipboad.AutoSize = true;
            this.buttonCopyToClipboad.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCopyToClipboad.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.buttonCopyToClipboad.Location = new System.Drawing.Point(722, 40);
            this.buttonCopyToClipboad.Margin = new System.Windows.Forms.Padding(0);
            this.buttonCopyToClipboad.Name = "buttonCopyToClipboad";
            this.buttonCopyToClipboad.Size = new System.Drawing.Size(48, 27);
            this.buttonCopyToClipboad.TabIndex = 2;
            this.buttonCopyToClipboad.Text = "Copy";
            this.buttonCopyToClipboad.UseVisualStyleBackColor = true;
            this.buttonCopyToClipboad.Click += new System.EventHandler(this.buttonCopyToClipboad_Click);
            // 
            // buttonFindSpots
            // 
            this.buttonFindSpots.AutoSize = true;
            this.buttonFindSpots.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonFindSpots.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonFindSpots.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.buttonFindSpots.ForeColor = System.Drawing.Color.White;
            this.buttonFindSpots.Location = new System.Drawing.Point(132, 12);
            this.buttonFindSpots.Name = "buttonFindSpots";
            this.buttonFindSpots.Size = new System.Drawing.Size(91, 27);
            this.buttonFindSpots.TabIndex = 2;
            this.buttonFindSpots.Text = "Detect spots";
            this.buttonFindSpots.UseVisualStyleBackColor = false;
            this.buttonFindSpots.Click += new System.EventHandler(this.buttonFindSpots_Click);
            // 
            // buttonGlobalFit
            // 
            this.buttonGlobalFit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGlobalFit.AutoSize = true;
            this.buttonGlobalFit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonGlobalFit.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            this.buttonGlobalFit.Location = new System.Drawing.Point(627, 409);
            this.buttonGlobalFit.Name = "buttonGlobalFit";
            this.buttonGlobalFit.Size = new System.Drawing.Size(140, 23);
            this.buttonGlobalFit.TabIndex = 2;
            this.buttonGlobalFit.Text = "Global fit (experimental)";
            this.buttonGlobalFit.UseVisualStyleBackColor = true;
            this.buttonGlobalFit.Click += new System.EventHandler(this.ButtonGlobalFit_Click);
            // 
            // buttonResetRangeForAllSpots
            // 
            this.buttonResetRangeForAllSpots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetRangeForAllSpots.AutoSize = true;
            this.buttonResetRangeForAllSpots.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonResetRangeForAllSpots.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.buttonResetRangeForAllSpots.Location = new System.Drawing.Point(443, 12);
            this.buttonResetRangeForAllSpots.Name = "buttonResetRangeForAllSpots";
            this.buttonResetRangeForAllSpots.Size = new System.Drawing.Size(88, 27);
            this.buttonResetRangeForAllSpots.TabIndex = 2;
            this.buttonResetRangeForAllSpots.Text = "Reset range";
            this.buttonResetRangeForAllSpots.UseVisualStyleBackColor = true;
            this.buttonResetRangeForAllSpots.Click += new System.EventHandler(this.ButtonResetRangeForAllSpots_Click);
            // 
            // buttonCopmprehensiveFitting
            // 
            this.buttonCopmprehensiveFitting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopmprehensiveFitting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCopmprehensiveFitting.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.buttonCopmprehensiveFitting.Location = new System.Drawing.Point(634, 380);
            this.buttonCopmprehensiveFitting.Name = "buttonCopmprehensiveFitting";
            this.buttonCopmprehensiveFitting.Size = new System.Drawing.Size(136, 27);
            this.buttonCopmprehensiveFitting.TabIndex = 2;
            this.buttonCopmprehensiveFitting.Text = "Re-fit all";
            this.buttonCopmprehensiveFitting.UseVisualStyleBackColor = true;
            this.buttonCopmprehensiveFitting.Click += new System.EventHandler(this.buttonRefit_Click);
            // 
            // buttonClearSpots
            // 
            this.buttonClearSpots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearSpots.AutoSize = true;
            this.buttonClearSpots.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonClearSpots.BackColor = System.Drawing.Color.IndianRed;
            this.buttonClearSpots.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.buttonClearSpots.ForeColor = System.Drawing.Color.White;
            this.buttonClearSpots.Location = new System.Drawing.Point(670, 12);
            this.buttonClearSpots.Name = "buttonClearSpots";
            this.buttonClearSpots.Size = new System.Drawing.Size(101, 27);
            this.buttonClearSpots.TabIndex = 2;
            this.buttonClearSpots.Text = "Clear all spots";
            this.buttonClearSpots.UseVisualStyleBackColor = false;
            this.buttonClearSpots.Click += new System.EventHandler(this.buttonClearSpots_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::ReciPro.Properties.Resources.TwoDimensionalPseudoVoigt;
            this.pictureBox1.Location = new System.Drawing.Point(1, 381);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(598, 77);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // buttonDonut
            // 
            this.buttonDonut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDonut.AutoSize = true;
            this.buttonDonut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonDonut.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            this.buttonDonut.Location = new System.Drawing.Point(604, 432);
            this.buttonDonut.Name = "buttonDonut";
            this.buttonDonut.Size = new System.Drawing.Size(125, 23);
            this.buttonDonut.TabIndex = 2;
            this.buttonDonut.Text = "Donut (experimental)";
            this.buttonDonut.UseVisualStyleBackColor = true;
            this.buttonDonut.Click += new System.EventHandler(this.buttonDonut_Click);
            // 
            // noDataGridViewTextBoxColumn
            // 
            this.noDataGridViewTextBoxColumn.DataPropertyName = "No";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.noDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.noDataGridViewTextBoxColumn.HeaderText = "No";
            this.noDataGridViewTextBoxColumn.Name = "noDataGridViewTextBoxColumn";
            this.noDataGridViewTextBoxColumn.ReadOnly = true;
            this.noDataGridViewTextBoxColumn.Width = 28;
            // 
            // bindingSourceObsSpots
            // 
            this.bindingSourceObsSpots.DataMember = "DataTableSpot";
            this.bindingSourceObsSpots.DataSource = this.DataSet;
            this.bindingSourceObsSpots.CurrentChanged += new System.EventHandler(this.bindingSourceSpot_CurrentChanged);
            this.bindingSourceObsSpots.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bindingSourceObsSpots_ListChanged);
            // 
            // DataSet
            // 
            this.DataSet.DataSetName = "DataSet";
            this.DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // noDataGridViewTextBoxColumn2
            // 
            this.noDataGridViewTextBoxColumn2.DataPropertyName = "No";
            this.noDataGridViewTextBoxColumn2.HeaderText = "No";
            this.noDataGridViewTextBoxColumn2.Name = "noDataGridViewTextBoxColumn2";
            this.noDataGridViewTextBoxColumn2.ReadOnly = true;
            this.noDataGridViewTextBoxColumn2.Width = 30;
            // 
            // assignedSpotsDataGridViewTextBoxColumn
            // 
            this.assignedSpotsDataGridViewTextBoxColumn.DataPropertyName = "AssignedSpots";
            this.assignedSpotsDataGridViewTextBoxColumn.HeaderText = "AssignedSpots";
            this.assignedSpotsDataGridViewTextBoxColumn.Name = "assignedSpotsDataGridViewTextBoxColumn";
            this.assignedSpotsDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bindingSourceGrains
            // 
            this.bindingSourceGrains.DataMember = "DataTableGrain";
            this.bindingSourceGrains.DataSource = this.DataSet;
            this.bindingSourceGrains.CurrentChanged += new System.EventHandler(this.bindingSourceGrains_CurrentChanged);
            // 
            // noDataGridViewTextBoxColumn1
            // 
            this.noDataGridViewTextBoxColumn1.DataPropertyName = "No";
            this.noDataGridViewTextBoxColumn1.HeaderText = "No";
            this.noDataGridViewTextBoxColumn1.Name = "noDataGridViewTextBoxColumn1";
            this.noDataGridViewTextBoxColumn1.ReadOnly = true;
            this.noDataGridViewTextBoxColumn1.Width = 30;
            // 
            // bindingSourceCandidates
            // 
            this.bindingSourceCandidates.DataMember = "DataTableCandidate";
            this.bindingSourceCandidates.DataSource = this.DataSet;
            this.bindingSourceCandidates.CurrentChanged += new System.EventHandler(this.bindingSourceCandidates_CurrentChanged);
            // 
            // scalablePictureBoxAdvanced
            // 
            this.scalablePictureBoxAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scalablePictureBoxAdvanced.CopyButtonVisible = true;
            this.scalablePictureBoxAdvanced.FixZoomAndCenter = false;
            this.scalablePictureBoxAdvanced.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.scalablePictureBoxAdvanced.FrequencyGraphVisible = false;
            this.scalablePictureBoxAdvanced.ImageFilter_DustAndScratches = true;
            this.scalablePictureBoxAdvanced.ImageFilter_DustAndScratchesRadius = 1.5D;
            this.scalablePictureBoxAdvanced.ImageFilter_DustAndScratchesThreshold = 3D;
            this.scalablePictureBoxAdvanced.ImageFilter_DustAndScratchesVisible = true;
            this.scalablePictureBoxAdvanced.ImageFilter_GaussianBlur = true;
            this.scalablePictureBoxAdvanced.ImageFilter_GaussianBlurRadius = 3D;
            this.scalablePictureBoxAdvanced.ImageFilter_GaussianBlurVisible = true;
            this.scalablePictureBoxAdvanced.ImageFilterVisible = true;
            this.scalablePictureBoxAdvanced.Location = new System.Drawing.Point(1, 26);
            this.scalablePictureBoxAdvanced.LogScaleBar = true;
            this.scalablePictureBoxAdvanced.LowerIntensity = 0D;
            this.scalablePictureBoxAdvanced.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.scalablePictureBoxAdvanced.MaximumIntensity = 18285.576171875D;
            this.scalablePictureBoxAdvanced.MinimumIntensity = -2306.3408203125D;
            this.scalablePictureBoxAdvanced.MousePositionLabelVisible = true;
            this.scalablePictureBoxAdvanced.Name = "scalablePictureBoxAdvanced";
            this.scalablePictureBoxAdvanced.PictureSize = new System.Drawing.Size(594, 567);
            this.scalablePictureBoxAdvanced.ShowGradiaent = true;
            this.scalablePictureBoxAdvanced.Size = new System.Drawing.Size(594, 735);
            this.scalablePictureBoxAdvanced.StatusLabel = "Elapsed time:    Dust && Scratches: 0.123msec.  Gaussian Blur: 0.205msec.  ";
            this.scalablePictureBoxAdvanced.StatusProgress = 0D;
            this.scalablePictureBoxAdvanced.StatusVisible = false;
            this.scalablePictureBoxAdvanced.TabIndex = 0;
            this.scalablePictureBoxAdvanced.TrackBarVisible = true;
            this.scalablePictureBoxAdvanced.UpperIntensity = 255D;
            this.scalablePictureBoxAdvanced.VisibleGradient = true;
            this.scalablePictureBoxAdvanced.MouseDown2 += new Crystallography.Controls.ScalablePictureBoxAdvanced.MouseEvent(this.scalablePictureBoxAdvanced1_MouseDown2);
            this.scalablePictureBoxAdvanced.StatusChanged += new System.EventHandler(this.scalablePictureBoxAdvanced_StatusChanged);
            this.scalablePictureBoxAdvanced.FilterChanged += new System.EventHandler(this.ScalablePictureBoxAdvanced_FilterChanged);
            // 
            // numericBoxDonut
            // 
            this.numericBoxDonut.AllowMouseControl = false;
            this.numericBoxDonut.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDonut.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDonut.DecimalPlaces = -2;
            this.numericBoxDonut.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDonut.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDonut.FooterText = "";
            this.numericBoxDonut.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDonut.HeaderText = "";
            this.numericBoxDonut.Location = new System.Drawing.Point(730, 433);
            this.numericBoxDonut.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDonut.Maximum = 100D;
            this.numericBoxDonut.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxDonut.Minimum = 1D;
            this.numericBoxDonut.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxDonut.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxDonut.MouseSpeed = 1D;
            this.numericBoxDonut.Multiline = false;
            this.numericBoxDonut.Name = "numericBoxDonut";
            this.numericBoxDonut.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDonut.RadianValue = 0.087266462599716474D;
            this.numericBoxDonut.ReadOnly = false;
            this.numericBoxDonut.RestrictLimitValue = true;
            this.numericBoxDonut.ShowFraction = false;
            this.numericBoxDonut.ShowPositiveSign = false;
            this.numericBoxDonut.ShowUpDown = true;
            this.numericBoxDonut.Size = new System.Drawing.Size(44, 22);
            this.numericBoxDonut.SkipEventDuringInput = false;
            this.numericBoxDonut.SmartIncrement = true;
            this.numericBoxDonut.TabIndex = 9;
            this.numericBoxDonut.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDonut.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDonut.TextFont = new System.Drawing.Font("Segoe UI Symbol", 8F);
            this.numericBoxDonut.ThonsandsSeparator = true;
            this.numericBoxDonut.ToolTip = "";
            this.numericBoxDonut.UpDown_Increment = 1D;
            this.numericBoxDonut.Value = 5D;
            this.numericBoxDonut.WordWrap = true;
            // 
            // numericBoxNearestNeighbor
            // 
            this.numericBoxNearestNeighbor.AllowMouseControl = false;
            this.numericBoxNearestNeighbor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxNearestNeighbor.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxNearestNeighbor.DecimalPlaces = 0;
            this.numericBoxNearestNeighbor.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxNearestNeighbor.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxNearestNeighbor.FooterText = "pix.";
            this.numericBoxNearestNeighbor.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxNearestNeighbor.HeaderText = "Nearest neighbor  >";
            this.numericBoxNearestNeighbor.Location = new System.Drawing.Point(318, 43);
            this.numericBoxNearestNeighbor.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxNearestNeighbor.Maximum = 1000D;
            this.numericBoxNearestNeighbor.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxNearestNeighbor.Minimum = 1D;
            this.numericBoxNearestNeighbor.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxNearestNeighbor.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxNearestNeighbor.MouseSpeed = 1D;
            this.numericBoxNearestNeighbor.Multiline = false;
            this.numericBoxNearestNeighbor.Name = "numericBoxNearestNeighbor";
            this.numericBoxNearestNeighbor.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxNearestNeighbor.RadianValue = 0.17453292519943295D;
            this.numericBoxNearestNeighbor.ReadOnly = false;
            this.numericBoxNearestNeighbor.RestrictLimitValue = true;
            this.numericBoxNearestNeighbor.ShowFraction = false;
            this.numericBoxNearestNeighbor.ShowPositiveSign = false;
            this.numericBoxNearestNeighbor.ShowUpDown = true;
            this.numericBoxNearestNeighbor.Size = new System.Drawing.Size(199, 25);
            this.numericBoxNearestNeighbor.SkipEventDuringInput = false;
            this.numericBoxNearestNeighbor.SmartIncrement = true;
            this.numericBoxNearestNeighbor.TabIndex = 3;
            this.numericBoxNearestNeighbor.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxNearestNeighbor.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxNearestNeighbor.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxNearestNeighbor.ThonsandsSeparator = true;
            this.numericBoxNearestNeighbor.ToolTip = "";
            this.numericBoxNearestNeighbor.UpDown_Increment = 1D;
            this.numericBoxNearestNeighbor.Value = 10D;
            this.numericBoxNearestNeighbor.WordWrap = true;
            // 
            // numericBoxNumberOfSpots
            // 
            this.numericBoxNumberOfSpots.AllowMouseControl = false;
            this.numericBoxNumberOfSpots.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxNumberOfSpots.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxNumberOfSpots.DecimalPlaces = 0;
            this.numericBoxNumberOfSpots.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxNumberOfSpots.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxNumberOfSpots.FooterText = "";
            this.numericBoxNumberOfSpots.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxNumberOfSpots.HeaderText = "Number";
            this.numericBoxNumberOfSpots.Location = new System.Drawing.Point(209, 43);
            this.numericBoxNumberOfSpots.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxNumberOfSpots.Maximum = 1000D;
            this.numericBoxNumberOfSpots.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxNumberOfSpots.Minimum = 1D;
            this.numericBoxNumberOfSpots.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxNumberOfSpots.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxNumberOfSpots.MouseSpeed = 1D;
            this.numericBoxNumberOfSpots.Multiline = false;
            this.numericBoxNumberOfSpots.Name = "numericBoxNumberOfSpots";
            this.numericBoxNumberOfSpots.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxNumberOfSpots.RadianValue = 0.52359877559829882D;
            this.numericBoxNumberOfSpots.ReadOnly = false;
            this.numericBoxNumberOfSpots.RestrictLimitValue = true;
            this.numericBoxNumberOfSpots.ShowFraction = false;
            this.numericBoxNumberOfSpots.ShowPositiveSign = false;
            this.numericBoxNumberOfSpots.ShowUpDown = true;
            this.numericBoxNumberOfSpots.Size = new System.Drawing.Size(99, 25);
            this.numericBoxNumberOfSpots.SkipEventDuringInput = false;
            this.numericBoxNumberOfSpots.SmartIncrement = true;
            this.numericBoxNumberOfSpots.TabIndex = 3;
            this.numericBoxNumberOfSpots.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxNumberOfSpots.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxNumberOfSpots.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxNumberOfSpots.ThonsandsSeparator = true;
            this.numericBoxNumberOfSpots.ToolTip = "";
            this.numericBoxNumberOfSpots.UpDown_Increment = 1D;
            this.numericBoxNumberOfSpots.Value = 30D;
            this.numericBoxNumberOfSpots.WordWrap = true;
            // 
            // numericBoxFittingRange
            // 
            this.numericBoxFittingRange.AllowMouseControl = false;
            this.numericBoxFittingRange.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxFittingRange.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFittingRange.DecimalPlaces = 1;
            this.numericBoxFittingRange.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxFittingRange.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxFittingRange.FooterText = "pix.";
            this.numericBoxFittingRange.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxFittingRange.HeaderText = "Fitting range (radius)";
            this.numericBoxFittingRange.Location = new System.Drawing.Point(230, 14);
            this.numericBoxFittingRange.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxFittingRange.Maximum = 100D;
            this.numericBoxFittingRange.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxFittingRange.Minimum = 0D;
            this.numericBoxFittingRange.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxFittingRange.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxFittingRange.MouseSpeed = 1D;
            this.numericBoxFittingRange.Multiline = false;
            this.numericBoxFittingRange.Name = "numericBoxFittingRange";
            this.numericBoxFittingRange.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxFittingRange.RadianValue = 0.3490658503988659D;
            this.numericBoxFittingRange.ReadOnly = false;
            this.numericBoxFittingRange.RestrictLimitValue = true;
            this.numericBoxFittingRange.ShowFraction = false;
            this.numericBoxFittingRange.ShowPositiveSign = false;
            this.numericBoxFittingRange.ShowUpDown = true;
            this.numericBoxFittingRange.Size = new System.Drawing.Size(210, 25);
            this.numericBoxFittingRange.SkipEventDuringInput = false;
            this.numericBoxFittingRange.SmartIncrement = true;
            this.numericBoxFittingRange.TabIndex = 3;
            this.numericBoxFittingRange.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxFittingRange.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxFittingRange.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxFittingRange.ThonsandsSeparator = true;
            this.numericBoxFittingRange.ToolTip = "";
            this.numericBoxFittingRange.UpDown_Increment = 1D;
            this.numericBoxFittingRange.Value = 20D;
            this.numericBoxFittingRange.WordWrap = true;
            this.numericBoxFittingRange.Load += new System.EventHandler(this.numericBoxFittingRange_Load);
            // 
            // numericBoxSemiangle
            // 
            this.numericBoxSemiangle.AllowMouseControl = false;
            this.numericBoxSemiangle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxSemiangle.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxSemiangle.DecimalPlaces = 1;
            this.numericBoxSemiangle.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSemiangle.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSemiangle.FooterText = "mrad";
            this.numericBoxSemiangle.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 10F);
            this.numericBoxSemiangle.HeaderText = "Semiangle";
            this.numericBoxSemiangle.Location = new System.Drawing.Point(544, 127);
            this.numericBoxSemiangle.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxSemiangle.Maximum = 10D;
            this.numericBoxSemiangle.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxSemiangle.Minimum = 1D;
            this.numericBoxSemiangle.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxSemiangle.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxSemiangle.MouseSpeed = 1D;
            this.numericBoxSemiangle.Multiline = false;
            this.numericBoxSemiangle.Name = "numericBoxSemiangle";
            this.numericBoxSemiangle.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxSemiangle.RadianValue = 0.034906585039886591D;
            this.numericBoxSemiangle.ReadOnly = false;
            this.numericBoxSemiangle.RestrictLimitValue = true;
            this.numericBoxSemiangle.ShowFraction = false;
            this.numericBoxSemiangle.ShowPositiveSign = false;
            this.numericBoxSemiangle.ShowUpDown = true;
            this.numericBoxSemiangle.Size = new System.Drawing.Size(157, 25);
            this.numericBoxSemiangle.SkipEventDuringInput = true;
            this.numericBoxSemiangle.SmartIncrement = true;
            this.numericBoxSemiangle.TabIndex = 30;
            this.numericBoxSemiangle.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxSemiangle.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxSemiangle.TextFont = new System.Drawing.Font("Segoe UI Symbol", 10F);
            this.numericBoxSemiangle.ThonsandsSeparator = true;
            this.numericBoxSemiangle.ToolTip = "";
            this.numericBoxSemiangle.UpDown_Increment = 1D;
            this.numericBoxSemiangle.Value = 2D;
            this.numericBoxSemiangle.WordWrap = true;
            // 
            // numericBoxMaxNumOfG
            // 
            this.numericBoxMaxNumOfG.AllowMouseControl = false;
            this.numericBoxMaxNumOfG.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxMaxNumOfG.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMaxNumOfG.DecimalPlaces = -2;
            this.numericBoxMaxNumOfG.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMaxNumOfG.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMaxNumOfG.FooterText = "";
            this.numericBoxMaxNumOfG.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 10F);
            this.numericBoxMaxNumOfG.HeaderText = "Bloch waves";
            this.numericBoxMaxNumOfG.Location = new System.Drawing.Point(406, 127);
            this.numericBoxMaxNumOfG.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxMaxNumOfG.Maximum = 2048D;
            this.numericBoxMaxNumOfG.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxMaxNumOfG.Minimum = 1D;
            this.numericBoxMaxNumOfG.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxMaxNumOfG.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxMaxNumOfG.MouseSpeed = 1D;
            this.numericBoxMaxNumOfG.Multiline = false;
            this.numericBoxMaxNumOfG.Name = "numericBoxMaxNumOfG";
            this.numericBoxMaxNumOfG.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxMaxNumOfG.RadianValue = 6.9813170079773181D;
            this.numericBoxMaxNumOfG.ReadOnly = false;
            this.numericBoxMaxNumOfG.RestrictLimitValue = true;
            this.numericBoxMaxNumOfG.ShowFraction = false;
            this.numericBoxMaxNumOfG.ShowPositiveSign = false;
            this.numericBoxMaxNumOfG.ShowUpDown = true;
            this.numericBoxMaxNumOfG.Size = new System.Drawing.Size(138, 25);
            this.numericBoxMaxNumOfG.SkipEventDuringInput = true;
            this.numericBoxMaxNumOfG.SmartIncrement = true;
            this.numericBoxMaxNumOfG.TabIndex = 30;
            this.numericBoxMaxNumOfG.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxMaxNumOfG.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxMaxNumOfG.TextFont = new System.Drawing.Font("Segoe UI Symbol", 10F);
            this.numericBoxMaxNumOfG.ThonsandsSeparator = true;
            this.numericBoxMaxNumOfG.ToolTip = "";
            this.numericBoxMaxNumOfG.UpDown_Increment = 1D;
            this.numericBoxMaxNumOfG.Value = 400D;
            this.numericBoxMaxNumOfG.WordWrap = true;
            // 
            // numericBoxAcceptableError
            // 
            this.numericBoxAcceptableError.AllowMouseControl = false;
            this.numericBoxAcceptableError.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxAcceptableError.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAcceptableError.DecimalPlaces = -2;
            this.numericBoxAcceptableError.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxAcceptableError.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxAcceptableError.FooterText = "%";
            this.numericBoxAcceptableError.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxAcceptableError.HeaderText = "Acceptable error";
            this.numericBoxAcceptableError.Location = new System.Drawing.Point(102, 51);
            this.numericBoxAcceptableError.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxAcceptableError.Maximum = 10D;
            this.numericBoxAcceptableError.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxAcceptableError.Minimum = 0.1D;
            this.numericBoxAcceptableError.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxAcceptableError.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxAcceptableError.MouseSpeed = 1D;
            this.numericBoxAcceptableError.Multiline = false;
            this.numericBoxAcceptableError.Name = "numericBoxAcceptableError";
            this.numericBoxAcceptableError.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxAcceptableError.RadianValue = 0.034906585039886591D;
            this.numericBoxAcceptableError.ReadOnly = false;
            this.numericBoxAcceptableError.RestrictLimitValue = true;
            this.numericBoxAcceptableError.ShowFraction = false;
            this.numericBoxAcceptableError.ShowPositiveSign = false;
            this.numericBoxAcceptableError.ShowUpDown = true;
            this.numericBoxAcceptableError.Size = new System.Drawing.Size(168, 25);
            this.numericBoxAcceptableError.SkipEventDuringInput = false;
            this.numericBoxAcceptableError.SmartIncrement = true;
            this.numericBoxAcceptableError.TabIndex = 7;
            this.numericBoxAcceptableError.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxAcceptableError.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxAcceptableError.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxAcceptableError.ThonsandsSeparator = true;
            this.numericBoxAcceptableError.ToolTip = "";
            this.numericBoxAcceptableError.UpDown_Increment = 1D;
            this.numericBoxAcceptableError.Value = 2D;
            this.numericBoxAcceptableError.WordWrap = true;
            // 
            // numericBoxMaxGrainNum
            // 
            this.numericBoxMaxGrainNum.AllowMouseControl = false;
            this.numericBoxMaxGrainNum.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxMaxGrainNum.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMaxGrainNum.DecimalPlaces = -2;
            this.numericBoxMaxGrainNum.Enabled = false;
            this.numericBoxMaxGrainNum.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMaxGrainNum.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMaxGrainNum.FooterText = "";
            this.numericBoxMaxGrainNum.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMaxGrainNum.HeaderText = "Max. No. of grains";
            this.numericBoxMaxGrainNum.Location = new System.Drawing.Point(40, 127);
            this.numericBoxMaxGrainNum.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.numericBoxMaxGrainNum.Maximum = 10D;
            this.numericBoxMaxGrainNum.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxMaxGrainNum.Minimum = 0.1D;
            this.numericBoxMaxGrainNum.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxMaxGrainNum.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxMaxGrainNum.MouseSpeed = 1D;
            this.numericBoxMaxGrainNum.Multiline = false;
            this.numericBoxMaxGrainNum.Name = "numericBoxMaxGrainNum";
            this.numericBoxMaxGrainNum.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxMaxGrainNum.RadianValue = 0.034906585039886591D;
            this.numericBoxMaxGrainNum.ReadOnly = false;
            this.numericBoxMaxGrainNum.RestrictLimitValue = true;
            this.numericBoxMaxGrainNum.ShowFraction = false;
            this.numericBoxMaxGrainNum.ShowPositiveSign = false;
            this.numericBoxMaxGrainNum.ShowUpDown = true;
            this.numericBoxMaxGrainNum.Size = new System.Drawing.Size(168, 25);
            this.numericBoxMaxGrainNum.SkipEventDuringInput = false;
            this.numericBoxMaxGrainNum.SmartIncrement = false;
            this.numericBoxMaxGrainNum.TabIndex = 7;
            this.numericBoxMaxGrainNum.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxMaxGrainNum.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxMaxGrainNum.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMaxGrainNum.ThonsandsSeparator = true;
            this.numericBoxMaxGrainNum.ToolTip = "";
            this.numericBoxMaxGrainNum.UpDown_Increment = 1D;
            this.numericBoxMaxGrainNum.Value = 2D;
            this.numericBoxMaxGrainNum.WordWrap = true;
            // 
            // numericBoxCameraLength
            // 
            this.numericBoxCameraLength.AllowMouseControl = false;
            this.numericBoxCameraLength.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCameraLength.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCameraLength.DecimalPlaces = -2;
            this.numericBoxCameraLength.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCameraLength.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCameraLength.FooterText = "mm";
            this.numericBoxCameraLength.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCameraLength.HeaderText = "Camera Length";
            this.numericBoxCameraLength.Location = new System.Drawing.Point(242, 20);
            this.numericBoxCameraLength.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCameraLength.Maximum = 10000D;
            this.numericBoxCameraLength.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCameraLength.Minimum = 0D;
            this.numericBoxCameraLength.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCameraLength.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxCameraLength.MouseSpeed = 1D;
            this.numericBoxCameraLength.Multiline = false;
            this.numericBoxCameraLength.Name = "numericBoxCameraLength";
            this.numericBoxCameraLength.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCameraLength.RadianValue = 17.453292519943293D;
            this.numericBoxCameraLength.ReadOnly = false;
            this.numericBoxCameraLength.RestrictLimitValue = true;
            this.numericBoxCameraLength.ShowFraction = false;
            this.numericBoxCameraLength.ShowPositiveSign = false;
            this.numericBoxCameraLength.ShowUpDown = false;
            this.numericBoxCameraLength.Size = new System.Drawing.Size(205, 25);
            this.numericBoxCameraLength.SkipEventDuringInput = false;
            this.numericBoxCameraLength.SmartIncrement = true;
            this.numericBoxCameraLength.TabIndex = 3;
            this.numericBoxCameraLength.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCameraLength.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCameraLength.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCameraLength.ThonsandsSeparator = true;
            this.numericBoxCameraLength.ToolTip = "";
            this.numericBoxCameraLength.UpDown_Increment = 1D;
            this.numericBoxCameraLength.Value = 1000D;
            this.numericBoxCameraLength.WordWrap = true;
            // 
            // numericBoxPixelSize
            // 
            this.numericBoxPixelSize.AllowMouseControl = false;
            this.numericBoxPixelSize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxPixelSize.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelSize.DecimalPlaces = -2;
            this.numericBoxPixelSize.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelSize.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelSize.FooterText = "mm";
            this.numericBoxPixelSize.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelSize.HeaderText = "Pixel Size";
            this.numericBoxPixelSize.Location = new System.Drawing.Point(279, 56);
            this.numericBoxPixelSize.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelSize.Maximum = 100D;
            this.numericBoxPixelSize.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPixelSize.Minimum = 0D;
            this.numericBoxPixelSize.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPixelSize.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxPixelSize.MouseSpeed = 1D;
            this.numericBoxPixelSize.Multiline = false;
            this.numericBoxPixelSize.Name = "numericBoxPixelSize";
            this.numericBoxPixelSize.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelSize.RadianValue = 0.0008726646259971648D;
            this.numericBoxPixelSize.ReadOnly = false;
            this.numericBoxPixelSize.RestrictLimitValue = true;
            this.numericBoxPixelSize.ShowFraction = false;
            this.numericBoxPixelSize.ShowPositiveSign = false;
            this.numericBoxPixelSize.ShowUpDown = false;
            this.numericBoxPixelSize.Size = new System.Drawing.Size(168, 25);
            this.numericBoxPixelSize.SkipEventDuringInput = false;
            this.numericBoxPixelSize.SmartIncrement = true;
            this.numericBoxPixelSize.TabIndex = 3;
            this.numericBoxPixelSize.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxPixelSize.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxPixelSize.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPixelSize.ThonsandsSeparator = true;
            this.numericBoxPixelSize.ToolTip = "";
            this.numericBoxPixelSize.UpDown_Increment = 1D;
            this.numericBoxPixelSize.Value = 0.05D;
            this.numericBoxPixelSize.WordWrap = true;
            // 
            // waveLengthControl1
            // 
            this.waveLengthControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.waveLengthControl1.Energy = 199.99999999999997D;
            this.waveLengthControl1.Font = new System.Drawing.Font("Arial", 9F);
            this.waveLengthControl1.Location = new System.Drawing.Point(9, 18);
            this.waveLengthControl1.Margin = new System.Windows.Forms.Padding(0);
            this.waveLengthControl1.MinimumSize = new System.Drawing.Size(190, 0);
            this.waveLengthControl1.Name = "waveLengthControl1";
            this.waveLengthControl1.ShowWaveSource = true;
            this.waveLengthControl1.Size = new System.Drawing.Size(221, 79);
            this.waveLengthControl1.TabIndex = 6;
            this.waveLengthControl1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.waveLengthControl1.WaveLength = 0.00250793474552456D;
            this.waveLengthControl1.WaveSource = Crystallography.WaveSource.Electron;
            this.waveLengthControl1.XrayWaveSourceElementNumber = 0;
            this.waveLengthControl1.XrayWaveSourceLine = Crystallography.XrayLine.Ka1;
            // 
            // FormSpotID
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1385, 807);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormSpotID";
            this.Text = "Spot ID";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSpotID_FormClosing);
            this.Load += new System.EventHandler(this.FormSpotID_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormSpotID_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormSpotID_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewGrains)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCandidates)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSpots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceObsSpots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceGrains)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceCandidates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private Crystallography.Controls.WaveLengthControl waveLengthControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Crystallography.Controls.NumericBox numericBoxCameraLength;
        private Crystallography.Controls.NumericBox numericBoxPixelSize;
        private System.Windows.Forms.Button buttonIdentifySpots;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridViewCandidates;
        private System.Windows.Forms.BindingSource bindingSourceCandidates;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFindSpot;
        private Crystallography.Controls.NumericBox numericBoxAcceptableError;
        private System.Windows.Forms.RadioButton radioButtonMultiGrain;
        private System.Windows.Forms.RadioButton radioButtonSingleGrain;
        private System.Windows.Forms.DataGridView dataGridViewGrains;
        private System.Windows.Forms.BindingSource bindingSourceGrains;
        private System.Windows.Forms.DataGridViewTextBoxColumn noDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssignedSpots;
        private Crystallography.Controls.NumericBox numericBoxMaxGrainNum;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSpotID;
        private System.Windows.Forms.DataGridViewTextBoxColumn noDataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn CrystalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn assignedSpotsDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.CheckBox checkBoxIgnoreMultipleDiffraction;
        private System.Windows.Forms.Button buttonPixelToPixel;
        private System.Windows.Forms.CheckBox checkBoxShowDebyeRing;
        private System.Windows.Forms.Button buttonCopyMetafile;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelIdentifySpot;
        public System.Windows.Forms.BindingSource bindingSourceObsSpots;
        public DataSetReciPro DataSet;
        public Crystallography.Controls.ScalablePictureBoxAdvanced scalablePictureBoxAdvanced;
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
        private Crystallography.Controls.NumericBox numericBoxMaxNumOfG;
        private Crystallography.Controls.NumericBox numericBoxSemiangle;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelImageFilter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelRefine;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.CheckBox checkBoxDetailsSpot;
        private System.Windows.Forms.DataGridView dataGridViewSpots;
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
        private System.Windows.Forms.CheckBox checkBoxShowObsSpotSymbol;
        private System.Windows.Forms.CheckBox checkBoxShowObsSpotLabel;
        private System.Windows.Forms.Button buttonSaveToFile;
        private Crystallography.Controls.NumericBox numericBoxNearestNeighbor;
        private Crystallography.Controls.NumericBox numericBoxNumberOfSpots;
        public Crystallography.Controls.NumericBox numericBoxFittingRange;
        private System.Windows.Forms.Button buttonCopyToClipboad;
        private System.Windows.Forms.Button buttonFindSpots;
        private System.Windows.Forms.Button buttonGlobalFit;
        private System.Windows.Forms.Button buttonResetRangeForAllSpots;
        private System.Windows.Forms.Button buttonCopmprehensiveFitting;
        private System.Windows.Forms.Button buttonClearSpots;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonDonut;
        private Crystallography.Controls.NumericBox numericBoxDonut;
    }
}