namespace ReciPro
{
    partial class FormPolycrystallineDiffractionSimulator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPolycrystallineDiffractionSimulator));
            this.backgroundWorkerMain = new System.ComponentModel.BackgroundWorker();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSimulateDebyeRing = new System.Windows.Forms.Button();
            this.tabControlCrystals = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.crystalControl1 = new Crystallography.Controls.CrystalControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.buttonLoadSetting = new System.Windows.Forms.Button();
            this.buttonSaveCurrentSetting = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBoxCrystalNumPerStepThreshold = new System.Windows.Forms.CheckBox();
            this.numericBoxCrystalNumPerStep = new Crystallography.Controls.NumericBox();
            this.numericBoxInheritabiliry = new Crystallography.Controls.NumericBox();
            this.checkBoxInheritabiliryThreshold = new System.Windows.Forms.CheckBox();
            this.numericBoxInheritabiliryThreshold = new Crystallography.Controls.NumericBox();
            this.checkBoxDirectionalDensityThreshold = new System.Windows.Forms.CheckBox();
            this.numericBoxDirectionalDensity = new Crystallography.Controls.NumericBox();
            this.numericBoxCrystalNumPerStepThreshold = new Crystallography.Controls.NumericBox();
            this.numericBoxDirectionalDensityThreshold = new Crystallography.Controls.NumericBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.checkBoxRefineConvergence = new System.Windows.Forms.CheckBox();
            this.checkBoxRefineStress = new System.Windows.Forms.CheckBox();
            this.checkBoxRefineCenterOffset = new System.Windows.Forms.CheckBox();
            this.checkBoxRefinePreferredOrientation = new System.Windows.Forms.CheckBox();
            this.checkBoxRefineFilmBlur = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBoxAutomaticallyChangeParameter = new System.Windows.Forms.CheckBox();
            this.numericUpDownChangeParameterThreshold = new System.Windows.Forms.NumericUpDown();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.graphControlResidual = new Crystallography.Controls.GraphControl();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.buttonSearchUnrelatedOrientations = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.checkBoxYusaGonioScan = new System.Windows.Forms.CheckBox();
            this.numericBoxRxSpeed = new Crystallography.Controls.NumericBox();
            this.checkBoxYusaGonio_ValidRx = new System.Windows.Forms.CheckBox();
            this.label52 = new System.Windows.Forms.Label();
            this.numericBoxYusaGonioRySpeed = new Crystallography.Controls.NumericBox();
            this.label51 = new System.Windows.Forms.Label();
            this.numericBoxYusaGonioRzSpeed = new Crystallography.Controls.NumericBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.radioButtonZigzagScan = new System.Windows.Forms.RadioButton();
            this.label54 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.numericBoxYusaGonioRyStep = new Crystallography.Controls.NumericBox();
            this.label55 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.numericBoxYusaGonioRyOscillation = new Crystallography.Controls.NumericBox();
            this.label56 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.numericBoxYusaGonioRzOscillation = new Crystallography.Controls.NumericBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label58 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxScale2 = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.comboBoxGradient = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.comboBoxScale1 = new System.Windows.Forms.ComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.listBoxReferrence = new System.Windows.Forms.ListBox();
            this.buttonAddRefferencePattern = new System.Windows.Forms.Button();
            this.buttonRemoveReferrencePattern = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.diffractionPatternControlSimulation = new ReciPro.DiffractionPatternControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelProgress = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControlCrystals.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChangeParameterThreshold)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorkerMain
            // 
            this.backgroundWorkerMain.WorkerReportsProgress = true;
            this.backgroundWorkerMain.WorkerSupportsCancellation = true;
            this.backgroundWorkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMain_DoWork);
            this.backgroundWorkerMain.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerMain_ProgressChanged);
            this.backgroundWorkerMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMain_RunWorkerCompleted);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox6);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox7);
            this.splitContainer1.Panel1MinSize = 334;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Size = new System.Drawing.Size(1538, 911);
            this.splitContainer1.SplitterDistance = 532;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 132;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.buttonSimulateDebyeRing);
            this.groupBox1.Controls.Add(this.tabControlCrystals);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox1.Size = new System.Drawing.Size(532, 609);
            this.groupBox1.TabIndex = 129;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crystal property";
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(468, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(38, 26);
            this.button1.TabIndex = 1003;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSimulateDebyeRing
            // 
            this.buttonSimulateDebyeRing.AutoSize = true;
            this.buttonSimulateDebyeRing.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSimulateDebyeRing.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSimulateDebyeRing.Location = new System.Drawing.Point(155, 15);
            this.buttonSimulateDebyeRing.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonSimulateDebyeRing.Name = "buttonSimulateDebyeRing";
            this.buttonSimulateDebyeRing.Size = new System.Drawing.Size(177, 26);
            this.buttonSimulateDebyeRing.TabIndex = 1003;
            this.buttonSimulateDebyeRing.Text = "Simulate Debye ring pattern";
            this.buttonSimulateDebyeRing.UseVisualStyleBackColor = true;
            this.buttonSimulateDebyeRing.Click += new System.EventHandler(this.buttonSimulateDebyeRing_Click);
            // 
            // tabControlCrystals
            // 
            this.tabControlCrystals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlCrystals.Controls.Add(this.tabPage2);
            this.tabControlCrystals.Controls.Add(this.tabPage3);
            this.tabControlCrystals.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tabControlCrystals.Location = new System.Drawing.Point(5, 50);
            this.tabControlCrystals.Multiline = true;
            this.tabControlCrystals.Name = "tabControlCrystals";
            this.tabControlCrystals.SelectedIndex = 0;
            this.tabControlCrystals.Size = new System.Drawing.Size(522, 553);
            this.tabControlCrystals.TabIndex = 1005;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.crystalControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(514, 523);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // crystalControl1
            // 
            this.crystalControl1.A = 0D;
            this.crystalControl1.AllowDrop = true;
            this.crystalControl1.Alpha = 0D;
            this.crystalControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.crystalControl1.B = 0D;
            this.crystalControl1.Beta = 0D;
            this.crystalControl1.C = 0D;
            this.crystalControl1.DefaultTabNumber = 0;
            this.crystalControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalControl1.Enabled = false;
            this.crystalControl1.Gamma = 0D;
            this.crystalControl1.Location = new System.Drawing.Point(3, 3);
            this.crystalControl1.Margin = new System.Windows.Forms.Padding(0);
            this.crystalControl1.Name = "crystalControl1";
            this.crystalControl1.ScatteringFactorVisible = false;
            this.crystalControl1.Size = new System.Drawing.Size(508, 517);
            this.crystalControl1.SkipEvent = false;
            this.crystalControl1.SymmetryInformationVisible = false;
            this.crystalControl1.SymmetrySeriesNumber = 0;
            this.crystalControl1.TabIndex = 1002;
            this.crystalControl1.VisibleAtomTab = true;
            this.crystalControl1.VisibleBasicInfoTab = true;
            this.crystalControl1.VisibleBondsPolyhedraTab = false;
            this.crystalControl1.VisibleBoundTab = false;
            this.crystalControl1.VisibleElasticityTab = true;
            this.crystalControl1.VisibleEOSTab = false;
            this.crystalControl1.VisibleLatticePlaneTab = false;
            this.crystalControl1.VisiblePolycrystallineTab = true;
            this.crystalControl1.VisibleReferenceTab = false;
            this.crystalControl1.VisibleStressStrainTab = true;
            this.crystalControl1.VisibleChanged += new System.EventHandler(this.crystalControl1_VisibleChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(192, 70);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.buttonLoadSetting);
            this.groupBox6.Controls.Add(this.buttonSaveCurrentSetting);
            this.groupBox6.Controls.Add(this.buttonSearch);
            this.groupBox6.Controls.Add(this.tabControl3);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox6.Location = new System.Drawing.Point(0, 609);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox6.Size = new System.Drawing.Size(532, 302);
            this.groupBox6.TabIndex = 129;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Fitting orientations";
            // 
            // buttonLoadSetting
            // 
            this.buttonLoadSetting.AutoSize = true;
            this.buttonLoadSetting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonLoadSetting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonLoadSetting.Location = new System.Drawing.Point(291, 21);
            this.buttonLoadSetting.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonLoadSetting.Name = "buttonLoadSetting";
            this.buttonLoadSetting.Size = new System.Drawing.Size(86, 26);
            this.buttonLoadSetting.TabIndex = 68;
            this.buttonLoadSetting.Text = "Load setting";
            this.buttonLoadSetting.UseVisualStyleBackColor = true;
            this.buttonLoadSetting.Click += new System.EventHandler(this.buttonLoadSetting_Click);
            // 
            // buttonSaveCurrentSetting
            // 
            this.buttonSaveCurrentSetting.AutoSize = true;
            this.buttonSaveCurrentSetting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSaveCurrentSetting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonSaveCurrentSetting.Location = new System.Drawing.Point(388, 21);
            this.buttonSaveCurrentSetting.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonSaveCurrentSetting.Name = "buttonSaveCurrentSetting";
            this.buttonSaveCurrentSetting.Size = new System.Drawing.Size(132, 26);
            this.buttonSaveCurrentSetting.TabIndex = 68;
            this.buttonSaveCurrentSetting.Text = "Save current setting";
            this.buttonSaveCurrentSetting.UseVisualStyleBackColor = true;
            this.buttonSaveCurrentSetting.Click += new System.EventHandler(this.buttonSaveCurrentSetting_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.AutoSize = true;
            this.buttonSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSearch.Location = new System.Drawing.Point(14, 21);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(146, 26);
            this.buttonSearch.TabIndex = 68;
            this.buttonSearch.Text = "Search Orientations";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Controls.Add(this.tabPage6);
            this.tabControl3.Controls.Add(this.tabPage8);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl3.Location = new System.Drawing.Point(5, 57);
            this.tabControl3.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(522, 239);
            this.tabControl3.TabIndex = 166;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.groupBox8);
            this.tabPage4.Controls.Add(this.checkBox6);
            this.tabPage4.Controls.Add(this.checkBoxAutomaticallyChangeParameter);
            this.tabPage4.Controls.Add(this.numericUpDownChangeParameterThreshold);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(514, 210);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Refinement option";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBoxCrystalNumPerStepThreshold);
            this.groupBox4.Controls.Add(this.numericBoxCrystalNumPerStep);
            this.groupBox4.Controls.Add(this.numericBoxInheritabiliry);
            this.groupBox4.Controls.Add(this.checkBoxInheritabiliryThreshold);
            this.groupBox4.Controls.Add(this.numericBoxInheritabiliryThreshold);
            this.groupBox4.Controls.Add(this.checkBoxDirectionalDensityThreshold);
            this.groupBox4.Controls.Add(this.numericBoxDirectionalDensity);
            this.groupBox4.Controls.Add(this.numericBoxCrystalNumPerStepThreshold);
            this.groupBox4.Controls.Add(this.numericBoxDirectionalDensityThreshold);
            this.groupBox4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox4.Location = new System.Drawing.Point(138, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(371, 121);
            this.groupBox4.TabIndex = 409;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Fitting parameters for preferred orientatin";
            // 
            // checkBoxCrystalNumPerStepThreshold
            // 
            this.checkBoxCrystalNumPerStepThreshold.AutoSize = true;
            this.checkBoxCrystalNumPerStepThreshold.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxCrystalNumPerStepThreshold.Location = new System.Drawing.Point(201, 28);
            this.checkBoxCrystalNumPerStepThreshold.Name = "checkBoxCrystalNumPerStepThreshold";
            this.checkBoxCrystalNumPerStepThreshold.Size = new System.Drawing.Size(83, 20);
            this.checkBoxCrystalNumPerStepThreshold.TabIndex = 408;
            this.checkBoxCrystalNumPerStepThreshold.Text = "Threshold";
            this.checkBoxCrystalNumPerStepThreshold.UseVisualStyleBackColor = true;
            this.checkBoxCrystalNumPerStepThreshold.CheckedChanged += new System.EventHandler(this.checkBoxCrystalNumPerStepThreshold_CheckedChanged);
            // 
            // numericBoxCrystalNumPerStep
            // 
            this.numericBoxCrystalNumPerStep.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCrystalNumPerStep.DecimalPlaces = 3;
            this.numericBoxCrystalNumPerStep.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxCrystalNumPerStep.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCrystalNumPerStep.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxCrystalNumPerStep.FooterText = "%";
            this.numericBoxCrystalNumPerStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCrystalNumPerStep.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxCrystalNumPerStep.HeaderText = "Num per Step";
            this.numericBoxCrystalNumPerStep.Location = new System.Drawing.Point(18, 27);
            this.numericBoxCrystalNumPerStep.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCrystalNumPerStep.Maximum = 10D;
            this.numericBoxCrystalNumPerStep.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCrystalNumPerStep.Minimum = 1E-06D;
            this.numericBoxCrystalNumPerStep.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxCrystalNumPerStep.Name = "numericBoxCrystalNumPerStep";
            this.numericBoxCrystalNumPerStep.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCrystalNumPerStep.RadianValue = 0.0087266462599716477D;
            this.numericBoxCrystalNumPerStep.RoundErrorAccuracy = -1;
            this.numericBoxCrystalNumPerStep.ShowUpDown = true;
            this.numericBoxCrystalNumPerStep.Size = new System.Drawing.Size(168, 25);
            this.numericBoxCrystalNumPerStep.SkipEventDuringInput = false;
            this.numericBoxCrystalNumPerStep.SmartIncrement = true;
            this.numericBoxCrystalNumPerStep.TabIndex = 407;
            this.numericBoxCrystalNumPerStep.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxCrystalNumPerStep.ThonsandsSeparator = true;
            this.numericBoxCrystalNumPerStep.Value = 0.5D;
            // 
            // numericBoxInheritabiliry
            // 
            this.numericBoxInheritabiliry.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxInheritabiliry.DecimalPlaces = 2;
            this.numericBoxInheritabiliry.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxInheritabiliry.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxInheritabiliry.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxInheritabiliry.FooterText = "%";
            this.numericBoxInheritabiliry.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxInheritabiliry.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxInheritabiliry.HeaderText = "Inheritability";
            this.numericBoxInheritabiliry.Location = new System.Drawing.Point(18, 57);
            this.numericBoxInheritabiliry.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxInheritabiliry.Maximum = 100D;
            this.numericBoxInheritabiliry.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxInheritabiliry.Minimum = 0D;
            this.numericBoxInheritabiliry.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxInheritabiliry.Name = "numericBoxInheritabiliry";
            this.numericBoxInheritabiliry.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxInheritabiliry.RadianValue = 0.17453292519943295D;
            this.numericBoxInheritabiliry.RoundErrorAccuracy = -1;
            this.numericBoxInheritabiliry.ShowUpDown = true;
            this.numericBoxInheritabiliry.Size = new System.Drawing.Size(168, 25);
            this.numericBoxInheritabiliry.SkipEventDuringInput = false;
            this.numericBoxInheritabiliry.SmartIncrement = true;
            this.numericBoxInheritabiliry.TabIndex = 407;
            this.numericBoxInheritabiliry.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxInheritabiliry.ThonsandsSeparator = true;
            this.numericBoxInheritabiliry.Value = 10D;
            // 
            // checkBoxInheritabiliryThreshold
            // 
            this.checkBoxInheritabiliryThreshold.AutoSize = true;
            this.checkBoxInheritabiliryThreshold.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxInheritabiliryThreshold.Location = new System.Drawing.Point(201, 58);
            this.checkBoxInheritabiliryThreshold.Name = "checkBoxInheritabiliryThreshold";
            this.checkBoxInheritabiliryThreshold.Size = new System.Drawing.Size(83, 20);
            this.checkBoxInheritabiliryThreshold.TabIndex = 408;
            this.checkBoxInheritabiliryThreshold.Text = "Threshold";
            this.checkBoxInheritabiliryThreshold.UseVisualStyleBackColor = true;
            this.checkBoxInheritabiliryThreshold.CheckedChanged += new System.EventHandler(this.checkBoxInheritabiliryThreshold_CheckedChanged);
            // 
            // numericBoxInheritabiliryThreshold
            // 
            this.numericBoxInheritabiliryThreshold.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxInheritabiliryThreshold.DecimalPlaces = 2;
            this.numericBoxInheritabiliryThreshold.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxInheritabiliryThreshold.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxInheritabiliryThreshold.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxInheritabiliryThreshold.FooterText = "%";
            this.numericBoxInheritabiliryThreshold.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxInheritabiliryThreshold.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxInheritabiliryThreshold.Location = new System.Drawing.Point(288, 57);
            this.numericBoxInheritabiliryThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxInheritabiliryThreshold.Maximum = 100D;
            this.numericBoxInheritabiliryThreshold.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxInheritabiliryThreshold.Minimum = 0D;
            this.numericBoxInheritabiliryThreshold.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxInheritabiliryThreshold.Name = "numericBoxInheritabiliryThreshold";
            this.numericBoxInheritabiliryThreshold.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxInheritabiliryThreshold.RadianValue = 1.6755160819145563D;
            this.numericBoxInheritabiliryThreshold.RoundErrorAccuracy = -1;
            this.numericBoxInheritabiliryThreshold.ShowUpDown = true;
            this.numericBoxInheritabiliryThreshold.Size = new System.Drawing.Size(79, 25);
            this.numericBoxInheritabiliryThreshold.SkipEventDuringInput = false;
            this.numericBoxInheritabiliryThreshold.SmartIncrement = true;
            this.numericBoxInheritabiliryThreshold.TabIndex = 407;
            this.numericBoxInheritabiliryThreshold.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxInheritabiliryThreshold.ThonsandsSeparator = true;
            this.numericBoxInheritabiliryThreshold.Value = 96D;
            this.numericBoxInheritabiliryThreshold.Visible = false;
            // 
            // checkBoxDirectionalDensityThreshold
            // 
            this.checkBoxDirectionalDensityThreshold.AutoSize = true;
            this.checkBoxDirectionalDensityThreshold.Checked = true;
            this.checkBoxDirectionalDensityThreshold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDirectionalDensityThreshold.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxDirectionalDensityThreshold.Location = new System.Drawing.Point(201, 88);
            this.checkBoxDirectionalDensityThreshold.Name = "checkBoxDirectionalDensityThreshold";
            this.checkBoxDirectionalDensityThreshold.Size = new System.Drawing.Size(83, 20);
            this.checkBoxDirectionalDensityThreshold.TabIndex = 408;
            this.checkBoxDirectionalDensityThreshold.Text = "Threshold";
            this.checkBoxDirectionalDensityThreshold.UseVisualStyleBackColor = true;
            this.checkBoxDirectionalDensityThreshold.CheckedChanged += new System.EventHandler(this.checkBoxDirectionalDensityThreshold_CheckedChanged);
            // 
            // numericBoxDirectionalDensity
            // 
            this.numericBoxDirectionalDensity.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDirectionalDensity.DecimalPlaces = 2;
            this.numericBoxDirectionalDensity.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDirectionalDensity.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDirectionalDensity.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDirectionalDensity.FooterText = "°";
            this.numericBoxDirectionalDensity.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDirectionalDensity.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDirectionalDensity.HeaderText = "Directional density";
            this.numericBoxDirectionalDensity.Location = new System.Drawing.Point(2, 88);
            this.numericBoxDirectionalDensity.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDirectionalDensity.Maximum = 720D;
            this.numericBoxDirectionalDensity.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDirectionalDensity.Minimum = 0.1D;
            this.numericBoxDirectionalDensity.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxDirectionalDensity.Name = "numericBoxDirectionalDensity";
            this.numericBoxDirectionalDensity.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDirectionalDensity.RadianValue = 0.3490658503988659D;
            this.numericBoxDirectionalDensity.RoundErrorAccuracy = -1;
            this.numericBoxDirectionalDensity.ShowUpDown = true;
            this.numericBoxDirectionalDensity.Size = new System.Drawing.Size(179, 25);
            this.numericBoxDirectionalDensity.SkipEventDuringInput = false;
            this.numericBoxDirectionalDensity.SmartIncrement = true;
            this.numericBoxDirectionalDensity.TabIndex = 407;
            this.numericBoxDirectionalDensity.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDirectionalDensity.ThonsandsSeparator = true;
            this.numericBoxDirectionalDensity.Value = 20D;
            // 
            // numericBoxCrystalNumPerStepThreshold
            // 
            this.numericBoxCrystalNumPerStepThreshold.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCrystalNumPerStepThreshold.DecimalPlaces = 3;
            this.numericBoxCrystalNumPerStepThreshold.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxCrystalNumPerStepThreshold.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCrystalNumPerStepThreshold.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxCrystalNumPerStepThreshold.FooterText = "%";
            this.numericBoxCrystalNumPerStepThreshold.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCrystalNumPerStepThreshold.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxCrystalNumPerStepThreshold.Location = new System.Drawing.Point(288, 28);
            this.numericBoxCrystalNumPerStepThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCrystalNumPerStepThreshold.Maximum = 10D;
            this.numericBoxCrystalNumPerStepThreshold.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCrystalNumPerStepThreshold.Minimum = 1E-06D;
            this.numericBoxCrystalNumPerStepThreshold.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxCrystalNumPerStepThreshold.Name = "numericBoxCrystalNumPerStepThreshold";
            this.numericBoxCrystalNumPerStepThreshold.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCrystalNumPerStepThreshold.RadianValue = 0.0004363323129985824D;
            this.numericBoxCrystalNumPerStepThreshold.RoundErrorAccuracy = -1;
            this.numericBoxCrystalNumPerStepThreshold.ShowUpDown = true;
            this.numericBoxCrystalNumPerStepThreshold.Size = new System.Drawing.Size(79, 25);
            this.numericBoxCrystalNumPerStepThreshold.SkipEventDuringInput = false;
            this.numericBoxCrystalNumPerStepThreshold.SmartIncrement = true;
            this.numericBoxCrystalNumPerStepThreshold.TabIndex = 407;
            this.numericBoxCrystalNumPerStepThreshold.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxCrystalNumPerStepThreshold.ThonsandsSeparator = true;
            this.numericBoxCrystalNumPerStepThreshold.Value = 0.025D;
            this.numericBoxCrystalNumPerStepThreshold.Visible = false;
            // 
            // numericBoxDirectionalDensityThreshold
            // 
            this.numericBoxDirectionalDensityThreshold.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDirectionalDensityThreshold.DecimalPlaces = 2;
            this.numericBoxDirectionalDensityThreshold.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDirectionalDensityThreshold.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDirectionalDensityThreshold.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDirectionalDensityThreshold.FooterText = "°";
            this.numericBoxDirectionalDensityThreshold.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDirectionalDensityThreshold.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDirectionalDensityThreshold.Location = new System.Drawing.Point(288, 88);
            this.numericBoxDirectionalDensityThreshold.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDirectionalDensityThreshold.Maximum = 720D;
            this.numericBoxDirectionalDensityThreshold.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDirectionalDensityThreshold.Minimum = 0.1D;
            this.numericBoxDirectionalDensityThreshold.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxDirectionalDensityThreshold.Name = "numericBoxDirectionalDensityThreshold";
            this.numericBoxDirectionalDensityThreshold.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDirectionalDensityThreshold.RadianValue = 0.017453292519943295D;
            this.numericBoxDirectionalDensityThreshold.RoundErrorAccuracy = -1;
            this.numericBoxDirectionalDensityThreshold.ShowUpDown = true;
            this.numericBoxDirectionalDensityThreshold.Size = new System.Drawing.Size(75, 25);
            this.numericBoxDirectionalDensityThreshold.SkipEventDuringInput = false;
            this.numericBoxDirectionalDensityThreshold.SmartIncrement = true;
            this.numericBoxDirectionalDensityThreshold.TabIndex = 407;
            this.numericBoxDirectionalDensityThreshold.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDirectionalDensityThreshold.ThonsandsSeparator = true;
            this.numericBoxDirectionalDensityThreshold.Value = 1D;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.checkBoxRefineConvergence);
            this.groupBox8.Controls.Add(this.checkBoxRefineStress);
            this.groupBox8.Controls.Add(this.checkBoxRefineCenterOffset);
            this.groupBox8.Controls.Add(this.checkBoxRefinePreferredOrientation);
            this.groupBox8.Controls.Add(this.checkBoxRefineFilmBlur);
            this.groupBox8.Location = new System.Drawing.Point(4, 4);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(127, 198);
            this.groupBox8.TabIndex = 166;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Fitting Option";
            // 
            // checkBoxRefineConvergence
            // 
            this.checkBoxRefineConvergence.AutoSize = true;
            this.checkBoxRefineConvergence.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRefineConvergence.Location = new System.Drawing.Point(8, 62);
            this.checkBoxRefineConvergence.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRefineConvergence.Name = "checkBoxRefineConvergence";
            this.checkBoxRefineConvergence.Size = new System.Drawing.Size(102, 36);
            this.checkBoxRefineConvergence.TabIndex = 409;
            this.checkBoxRefineConvergence.Text = "Beam\r\n convergence";
            this.checkBoxRefineConvergence.UseVisualStyleBackColor = true;
            // 
            // checkBoxRefineStress
            // 
            this.checkBoxRefineStress.AutoSize = true;
            this.checkBoxRefineStress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRefineStress.Location = new System.Drawing.Point(8, 161);
            this.checkBoxRefineStress.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRefineStress.Name = "checkBoxRefineStress";
            this.checkBoxRefineStress.Size = new System.Drawing.Size(62, 20);
            this.checkBoxRefineStress.TabIndex = 412;
            this.checkBoxRefineStress.Text = "Stress";
            this.checkBoxRefineStress.UseVisualStyleBackColor = true;
            // 
            // checkBoxRefineCenterOffset
            // 
            this.checkBoxRefineCenterOffset.AutoSize = true;
            this.checkBoxRefineCenterOffset.Checked = true;
            this.checkBoxRefineCenterOffset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRefineCenterOffset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRefineCenterOffset.Location = new System.Drawing.Point(8, 133);
            this.checkBoxRefineCenterOffset.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRefineCenterOffset.Name = "checkBoxRefineCenterOffset";
            this.checkBoxRefineCenterOffset.Size = new System.Drawing.Size(100, 20);
            this.checkBoxRefineCenterOffset.TabIndex = 411;
            this.checkBoxRefineCenterOffset.Text = "Center offset";
            this.checkBoxRefineCenterOffset.UseVisualStyleBackColor = true;
            // 
            // checkBoxRefinePreferredOrientation
            // 
            this.checkBoxRefinePreferredOrientation.AutoSize = true;
            this.checkBoxRefinePreferredOrientation.Checked = true;
            this.checkBoxRefinePreferredOrientation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRefinePreferredOrientation.Enabled = false;
            this.checkBoxRefinePreferredOrientation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRefinePreferredOrientation.Location = new System.Drawing.Point(8, 22);
            this.checkBoxRefinePreferredOrientation.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRefinePreferredOrientation.Name = "checkBoxRefinePreferredOrientation";
            this.checkBoxRefinePreferredOrientation.Size = new System.Drawing.Size(91, 36);
            this.checkBoxRefinePreferredOrientation.TabIndex = 408;
            this.checkBoxRefinePreferredOrientation.Text = "Preferred \r\n orientation";
            this.checkBoxRefinePreferredOrientation.UseVisualStyleBackColor = true;
            // 
            // checkBoxRefineFilmBlur
            // 
            this.checkBoxRefineFilmBlur.AutoSize = true;
            this.checkBoxRefineFilmBlur.Checked = true;
            this.checkBoxRefineFilmBlur.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRefineFilmBlur.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRefineFilmBlur.Location = new System.Drawing.Point(8, 105);
            this.checkBoxRefineFilmBlur.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxRefineFilmBlur.Name = "checkBoxRefineFilmBlur";
            this.checkBoxRefineFilmBlur.Size = new System.Drawing.Size(76, 20);
            this.checkBoxRefineFilmBlur.TabIndex = 410;
            this.checkBoxRefineFilmBlur.Text = "Film blur";
            this.checkBoxRefineFilmBlur.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Checked = true;
            this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox6.Enabled = false;
            this.checkBox6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox6.Location = new System.Drawing.Point(145, 180);
            this.checkBox6.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(174, 20);
            this.checkBox6.TabIndex = 406;
            this.checkBox6.Text = "Automatically save setting";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutomaticallyChangeParameter
            // 
            this.checkBoxAutomaticallyChangeParameter.AutoSize = true;
            this.checkBoxAutomaticallyChangeParameter.Checked = true;
            this.checkBoxAutomaticallyChangeParameter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutomaticallyChangeParameter.Enabled = false;
            this.checkBoxAutomaticallyChangeParameter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxAutomaticallyChangeParameter.Location = new System.Drawing.Point(146, 146);
            this.checkBoxAutomaticallyChangeParameter.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxAutomaticallyChangeParameter.Name = "checkBoxAutomaticallyChangeParameter";
            this.checkBoxAutomaticallyChangeParameter.Size = new System.Drawing.Size(211, 20);
            this.checkBoxAutomaticallyChangeParameter.TabIndex = 405;
            this.checkBoxAutomaticallyChangeParameter.Text = "Automatically change parameter";
            this.checkBoxAutomaticallyChangeParameter.UseVisualStyleBackColor = true;
            // 
            // numericUpDownChangeParameterThreshold
            // 
            this.numericUpDownChangeParameterThreshold.DecimalPlaces = 2;
            this.numericUpDownChangeParameterThreshold.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownChangeParameterThreshold.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.numericUpDownChangeParameterThreshold.Location = new System.Drawing.Point(363, 146);
            this.numericUpDownChangeParameterThreshold.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.numericUpDownChangeParameterThreshold.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownChangeParameterThreshold.Name = "numericUpDownChangeParameterThreshold";
            this.numericUpDownChangeParameterThreshold.Size = new System.Drawing.Size(52, 22);
            this.numericUpDownChangeParameterThreshold.TabIndex = 404;
            this.numericUpDownChangeParameterThreshold.ThousandsSeparator = true;
            this.numericUpDownChangeParameterThreshold.Value = new decimal(new int[] {
            6,
            0,
            0,
            65536});
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.textBox1);
            this.tabPage6.Controls.Add(this.graphControlResidual);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage6.Size = new System.Drawing.Size(514, 210);
            this.tabPage6.TabIndex = 1;
            this.tabPage6.Text = "Refinment results";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(3, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(174, 190);
            this.textBox1.TabIndex = 164;
            // 
            // graphControlResidual
            // 
            this.graphControlResidual.AllowMouseOperation = true;
            this.graphControlResidual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphControlResidual.BackgroundColor = System.Drawing.Color.White;
            this.graphControlResidual.BottomMargin = 0D;
            this.graphControlResidual.AxisLineColor = System.Drawing.Color.Gray;
            this.graphControlResidual.DivisionLineColor = System.Drawing.Color.LightGray;
            this.graphControlResidual.FixRangeHorizontal = false;
            this.graphControlResidual.FixRangeVertical = false;
            this.graphControlResidual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.graphControlResidual.GraphTitle = "";
            this.graphControlResidual.AxisXTextVisible = true;
            this.graphControlResidual.Interpolation = false;
            this.graphControlResidual.IsIntegerX = false;
            this.graphControlResidual.IsIntegerY = false;
            this.graphControlResidual.LabelX = "X:";
            this.graphControlResidual.LabelY = "Y:";
            this.graphControlResidual.LeftMargin = 0F;
            this.graphControlResidual.VerticalLineColor = System.Drawing.Color.Red;
            this.graphControlResidual.LineWidth = 1F;
            this.graphControlResidual.Location = new System.Drawing.Point(184, 4);
            this.graphControlResidual.LowerX = 0D;
            this.graphControlResidual.LowerY = 0D;
            this.graphControlResidual.Margin = new System.Windows.Forms.Padding(4);
            this.graphControlResidual.MaximalX = 1D;
            this.graphControlResidual.MaximalY = 1D;
            this.graphControlResidual.MinimalX = 0D;
            this.graphControlResidual.MinimalY = 0D;
            this.graphControlResidual.Mode = Crystallography.Controls.GraphControl.DrawingMode.Line;
            this.graphControlResidual.MousePositionVisible = true;
            this.graphControlResidual.Name = "graphControlResidual";
            this.graphControlResidual.OriginPosition = new System.Drawing.Point(40, 20);
            this.graphControlResidual.Size = new System.Drawing.Size(322, 189);
            this.graphControlResidual.Smoothing = false;
            this.graphControlResidual.TabIndex = 163;
            this.graphControlResidual.UpperPanelFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.graphControlResidual.UnitX = "";
            this.graphControlResidual.UnitY = "";
            this.graphControlResidual.UpperPanelVisible = false;
            this.graphControlResidual.UpperX = 1D;
            this.graphControlResidual.UpperY = 1D;
            this.graphControlResidual.UseLineWidth = true;
            this.graphControlResidual.AxisYTextVisible = true;
            this.graphControlResidual.XLog = false;
            this.graphControlResidual.DivisionLineXVisible = true;
            this.graphControlResidual.YLog = false;
            this.graphControlResidual.DivisionLineYVisible = true;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.buttonSearchUnrelatedOrientations);
            this.tabPage8.Location = new System.Drawing.Point(4, 25);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage8.Size = new System.Drawing.Size(514, 210);
            this.tabPage8.TabIndex = 2;
            this.tabPage8.Text = "Debug";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // buttonSearchUnrelatedOrientations
            // 
            this.buttonSearchUnrelatedOrientations.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSearchUnrelatedOrientations.Location = new System.Drawing.Point(5, 32);
            this.buttonSearchUnrelatedOrientations.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonSearchUnrelatedOrientations.Name = "buttonSearchUnrelatedOrientations";
            this.buttonSearchUnrelatedOrientations.Size = new System.Drawing.Size(281, 35);
            this.buttonSearchUnrelatedOrientations.TabIndex = 68;
            this.buttonSearchUnrelatedOrientations.Text = "Search unrelated orientation";
            this.buttonSearchUnrelatedOrientations.UseVisualStyleBackColor = true;
            this.buttonSearchUnrelatedOrientations.Click += new System.EventHandler(this.buttonSearchUnrelatedOrientations_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.checkBoxYusaGonioScan);
            this.groupBox7.Controls.Add(this.numericBoxRxSpeed);
            this.groupBox7.Controls.Add(this.checkBoxYusaGonio_ValidRx);
            this.groupBox7.Controls.Add(this.label52);
            this.groupBox7.Controls.Add(this.numericBoxYusaGonioRySpeed);
            this.groupBox7.Controls.Add(this.label51);
            this.groupBox7.Controls.Add(this.numericBoxYusaGonioRzSpeed);
            this.groupBox7.Controls.Add(this.label53);
            this.groupBox7.Controls.Add(this.label50);
            this.groupBox7.Controls.Add(this.radioButtonZigzagScan);
            this.groupBox7.Controls.Add(this.label54);
            this.groupBox7.Controls.Add(this.label49);
            this.groupBox7.Controls.Add(this.numericBoxYusaGonioRyStep);
            this.groupBox7.Controls.Add(this.label55);
            this.groupBox7.Controls.Add(this.label48);
            this.groupBox7.Controls.Add(this.numericBoxYusaGonioRyOscillation);
            this.groupBox7.Controls.Add(this.label56);
            this.groupBox7.Controls.Add(this.label60);
            this.groupBox7.Controls.Add(this.label47);
            this.groupBox7.Controls.Add(this.numericBoxYusaGonioRzOscillation);
            this.groupBox7.Controls.Add(this.label57);
            this.groupBox7.Controls.Add(this.label59);
            this.groupBox7.Controls.Add(this.checkBox2);
            this.groupBox7.Controls.Add(this.checkBox3);
            this.groupBox7.Controls.Add(this.label58);
            this.groupBox7.Location = new System.Drawing.Point(6, 1026);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(312, 182);
            this.groupBox7.TabIndex = 1001;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "groupBox7";
            // 
            // checkBoxYusaGonioScan
            // 
            this.checkBoxYusaGonioScan.AutoSize = true;
            this.checkBoxYusaGonioScan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxYusaGonioScan.Location = new System.Drawing.Point(8, 25);
            this.checkBoxYusaGonioScan.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxYusaGonioScan.Name = "checkBoxYusaGonioScan";
            this.checkBoxYusaGonioScan.Size = new System.Drawing.Size(142, 20);
            this.checkBoxYusaGonioScan.TabIndex = 101;
            this.checkBoxYusaGonioScan.Text = "Use YusaGonio Scan";
            this.checkBoxYusaGonioScan.UseVisualStyleBackColor = true;
            // 
            // numericBoxRxSpeed
            // 
            this.numericBoxRxSpeed.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxRxSpeed.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRxSpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxRxSpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxRxSpeed.Location = new System.Drawing.Point(126, 94);
            this.numericBoxRxSpeed.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxRxSpeed.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxRxSpeed.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxRxSpeed.Name = "numericBoxRxSpeed";
            this.numericBoxRxSpeed.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxRxSpeed.RadianValue = 0.31415926535897931D;
            this.numericBoxRxSpeed.RoundErrorAccuracy = -1;
            this.numericBoxRxSpeed.Size = new System.Drawing.Size(61, 24);
            this.numericBoxRxSpeed.SkipEventDuringInput = false;
            this.numericBoxRxSpeed.SmartIncrement = true;
            this.numericBoxRxSpeed.TabIndex = 103;
            this.numericBoxRxSpeed.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRxSpeed.ThonsandsSeparator = true;
            this.numericBoxRxSpeed.Value = 18D;
            // 
            // checkBoxYusaGonio_ValidRx
            // 
            this.checkBoxYusaGonio_ValidRx.AutoSize = true;
            this.checkBoxYusaGonio_ValidRx.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxYusaGonio_ValidRx.Location = new System.Drawing.Point(28, 70);
            this.checkBoxYusaGonio_ValidRx.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxYusaGonio_ValidRx.Name = "checkBoxYusaGonio_ValidRx";
            this.checkBoxYusaGonio_ValidRx.Size = new System.Drawing.Size(63, 20);
            this.checkBoxYusaGonio_ValidRx.TabIndex = 102;
            this.checkBoxYusaGonio_ValidRx.Text = "Rx (φ)";
            this.checkBoxYusaGonio_ValidRx.UseVisualStyleBackColor = true;
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label52.Location = new System.Drawing.Point(20, 99);
            this.label52.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(79, 16);
            this.label52.TabIndex = 14;
            this.label52.Text = "motor speed";
            // 
            // numericBoxYusaGonioRySpeed
            // 
            this.numericBoxYusaGonioRySpeed.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRySpeed.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxYusaGonioRySpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRySpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRySpeed.Location = new System.Drawing.Point(149, 276);
            this.numericBoxYusaGonioRySpeed.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxYusaGonioRySpeed.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxYusaGonioRySpeed.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxYusaGonioRySpeed.Name = "numericBoxYusaGonioRySpeed";
            this.numericBoxYusaGonioRySpeed.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxYusaGonioRySpeed.RadianValue = 0.017453292519943295D;
            this.numericBoxYusaGonioRySpeed.RoundErrorAccuracy = -1;
            this.numericBoxYusaGonioRySpeed.Size = new System.Drawing.Size(61, 24);
            this.numericBoxYusaGonioRySpeed.SkipEventDuringInput = false;
            this.numericBoxYusaGonioRySpeed.SmartIncrement = true;
            this.numericBoxYusaGonioRySpeed.TabIndex = 109;
            this.numericBoxYusaGonioRySpeed.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxYusaGonioRySpeed.ThonsandsSeparator = true;
            this.numericBoxYusaGonioRySpeed.Value = 1D;
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label51.Location = new System.Drawing.Point(44, 226);
            this.label51.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(63, 16);
            this.label51.TabIndex = 16;
            this.label51.Text = "oscillation";
            // 
            // numericBoxYusaGonioRzSpeed
            // 
            this.numericBoxYusaGonioRzSpeed.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRzSpeed.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxYusaGonioRzSpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRzSpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRzSpeed.Location = new System.Drawing.Point(149, 196);
            this.numericBoxYusaGonioRzSpeed.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxYusaGonioRzSpeed.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxYusaGonioRzSpeed.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxYusaGonioRzSpeed.Name = "numericBoxYusaGonioRzSpeed";
            this.numericBoxYusaGonioRzSpeed.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxYusaGonioRzSpeed.RadianValue = 0.034906585039886591D;
            this.numericBoxYusaGonioRzSpeed.RoundErrorAccuracy = -1;
            this.numericBoxYusaGonioRzSpeed.Size = new System.Drawing.Size(61, 24);
            this.numericBoxYusaGonioRzSpeed.SkipEventDuringInput = false;
            this.numericBoxYusaGonioRzSpeed.SmartIncrement = true;
            this.numericBoxYusaGonioRzSpeed.TabIndex = 106;
            this.numericBoxYusaGonioRzSpeed.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxYusaGonioRzSpeed.ThonsandsSeparator = true;
            this.numericBoxYusaGonioRzSpeed.Value = 2D;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label53.Location = new System.Drawing.Point(214, 341);
            this.label53.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(29, 13);
            this.label53.TabIndex = 15;
            this.label53.Text = "deg.";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label50.Location = new System.Drawing.Point(44, 311);
            this.label50.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(63, 16);
            this.label50.TabIndex = 18;
            this.label50.Text = "oscillation";
            // 
            // radioButtonZigzagScan
            // 
            this.radioButtonZigzagScan.Checked = true;
            this.radioButtonZigzagScan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonZigzagScan.Location = new System.Drawing.Point(28, 122);
            this.radioButtonZigzagScan.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonZigzagScan.Name = "radioButtonZigzagScan";
            this.radioButtonZigzagScan.Size = new System.Drawing.Size(205, 52);
            this.radioButtonZigzagScan.TabIndex = 104;
            this.radioButtonZigzagScan.TabStop = true;
            this.radioButtonZigzagScan.Text = "+θ > +ω > -θ > +ω .... (Zigzag scan)";
            this.radioButtonZigzagScan.UseVisualStyleBackColor = true;
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label54.Location = new System.Drawing.Point(42, 281);
            this.label54.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(79, 16);
            this.label54.TabIndex = 19;
            this.label54.Text = "motor speed";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label49.Location = new System.Drawing.Point(60, 342);
            this.label49.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(31, 16);
            this.label49.TabIndex = 25;
            this.label49.Text = "step";
            // 
            // numericBoxYusaGonioRyStep
            // 
            this.numericBoxYusaGonioRyStep.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRyStep.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxYusaGonioRyStep.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRyStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRyStep.Location = new System.Drawing.Point(149, 336);
            this.numericBoxYusaGonioRyStep.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxYusaGonioRyStep.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxYusaGonioRyStep.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxYusaGonioRyStep.Name = "numericBoxYusaGonioRyStep";
            this.numericBoxYusaGonioRyStep.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxYusaGonioRyStep.RadianValue = 0.0034906585039886592D;
            this.numericBoxYusaGonioRyStep.RoundErrorAccuracy = -1;
            this.numericBoxYusaGonioRyStep.Size = new System.Drawing.Size(61, 24);
            this.numericBoxYusaGonioRyStep.SkipEventDuringInput = false;
            this.numericBoxYusaGonioRyStep.SmartIncrement = true;
            this.numericBoxYusaGonioRyStep.TabIndex = 111;
            this.numericBoxYusaGonioRyStep.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxYusaGonioRyStep.ThonsandsSeparator = true;
            this.numericBoxYusaGonioRyStep.Value = 0.2D;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label55.Location = new System.Drawing.Point(214, 310);
            this.label55.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(29, 13);
            this.label55.TabIndex = 23;
            this.label55.Text = "deg.";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label48.Location = new System.Drawing.Point(128, 231);
            this.label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(15, 16);
            this.label48.TabIndex = 27;
            this.label48.Text = "±";
            // 
            // numericBoxYusaGonioRyOscillation
            // 
            this.numericBoxYusaGonioRyOscillation.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRyOscillation.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxYusaGonioRyOscillation.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRyOscillation.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRyOscillation.Location = new System.Drawing.Point(149, 306);
            this.numericBoxYusaGonioRyOscillation.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxYusaGonioRyOscillation.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxYusaGonioRyOscillation.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxYusaGonioRyOscillation.Name = "numericBoxYusaGonioRyOscillation";
            this.numericBoxYusaGonioRyOscillation.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxYusaGonioRyOscillation.RadianValue = 0.069813170079773182D;
            this.numericBoxYusaGonioRyOscillation.RoundErrorAccuracy = -1;
            this.numericBoxYusaGonioRyOscillation.Size = new System.Drawing.Size(61, 24);
            this.numericBoxYusaGonioRyOscillation.SkipEventDuringInput = false;
            this.numericBoxYusaGonioRyOscillation.SmartIncrement = true;
            this.numericBoxYusaGonioRyOscillation.TabIndex = 110;
            this.numericBoxYusaGonioRyOscillation.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxYusaGonioRyOscillation.ThonsandsSeparator = true;
            this.numericBoxYusaGonioRyOscillation.Value = 4D;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label56.Location = new System.Drawing.Point(42, 196);
            this.label56.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(79, 16);
            this.label56.TabIndex = 24;
            this.label56.Text = "motor speed";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label60.Location = new System.Drawing.Point(192, 99);
            this.label60.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(68, 16);
            this.label60.TabIndex = 1000;
            this.label60.Text = "deg. / sec.";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label47.Location = new System.Drawing.Point(128, 311);
            this.label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(15, 16);
            this.label47.TabIndex = 26;
            this.label47.Text = "±";
            // 
            // numericBoxYusaGonioRzOscillation
            // 
            this.numericBoxYusaGonioRzOscillation.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRzOscillation.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxYusaGonioRzOscillation.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRzOscillation.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxYusaGonioRzOscillation.Location = new System.Drawing.Point(149, 226);
            this.numericBoxYusaGonioRzOscillation.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxYusaGonioRzOscillation.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxYusaGonioRzOscillation.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxYusaGonioRzOscillation.Name = "numericBoxYusaGonioRzOscillation";
            this.numericBoxYusaGonioRzOscillation.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxYusaGonioRzOscillation.RadianValue = 0.069813170079773182D;
            this.numericBoxYusaGonioRzOscillation.RoundErrorAccuracy = -1;
            this.numericBoxYusaGonioRzOscillation.Size = new System.Drawing.Size(61, 24);
            this.numericBoxYusaGonioRzOscillation.SkipEventDuringInput = false;
            this.numericBoxYusaGonioRzOscillation.SmartIncrement = true;
            this.numericBoxYusaGonioRzOscillation.TabIndex = 107;
            this.numericBoxYusaGonioRzOscillation.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxYusaGonioRzOscillation.ThonsandsSeparator = true;
            this.numericBoxYusaGonioRzOscillation.Value = 4D;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label57.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label57.Location = new System.Drawing.Point(214, 230);
            this.label57.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(29, 13);
            this.label57.TabIndex = 1000;
            this.label57.Text = "deg.";
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label59.Location = new System.Drawing.Point(214, 201);
            this.label59.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(59, 13);
            this.label59.TabIndex = 1000;
            this.label59.Text = "deg. / sec.";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox2.Location = new System.Drawing.Point(44, 171);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(61, 20);
            this.checkBox2.TabIndex = 105;
            this.checkBox2.Text = "Rz (θ)";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBox3.Location = new System.Drawing.Point(44, 255);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(63, 20);
            this.checkBox3.TabIndex = 108;
            this.checkBox3.Text = "Ry (ω)";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label58.Location = new System.Drawing.Point(214, 281);
            this.label58.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(59, 13);
            this.label58.TabIndex = 20;
            this.label58.Text = "deg. / sec.";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.listBoxReferrence);
            this.groupBox3.Controls.Add(this.buttonAddRefferencePattern);
            this.groupBox3.Controls.Add(this.buttonRemoveReferrencePattern);
            this.groupBox3.Controls.Add(this.tabControl1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox3.Size = new System.Drawing.Size(1001, 911);
            this.groupBox3.TabIndex = 129;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Simulated / refference patterns";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxScale2);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.comboBoxGradient);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.comboBoxScale1);
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(305, 28);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(650, 60);
            this.groupBox2.TabIndex = 130;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Appearance";
            // 
            // comboBoxScale2
            // 
            this.comboBoxScale2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScale2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxScale2.FormattingEnabled = true;
            this.comboBoxScale2.Items.AddRange(new object[] {
            "Gray scale",
            "Cold-Warm scale"});
            this.comboBoxScale2.Location = new System.Drawing.Point(498, 22);
            this.comboBoxScale2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.comboBoxScale2.Name = "comboBoxScale2";
            this.comboBoxScale2.Size = new System.Drawing.Size(134, 22);
            this.comboBoxScale2.TabIndex = 303;
            this.comboBoxScale2.SelectedIndexChanged += new System.EventHandler(this.comboBoxScale_SelectedIndexChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label26.Location = new System.Drawing.Point(432, 26);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(46, 14);
            this.label26.TabIndex = 152;
            this.label26.Text = "Scale 2";
            // 
            // comboBoxGradient
            // 
            this.comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGradient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxGradient.FormattingEnabled = true;
            this.comboBoxGradient.Items.AddRange(new object[] {
            "Positive",
            "Negative"});
            this.comboBoxGradient.Location = new System.Drawing.Point(80, 22);
            this.comboBoxGradient.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.comboBoxGradient.Name = "comboBoxGradient";
            this.comboBoxGradient.Size = new System.Drawing.Size(135, 22);
            this.comboBoxGradient.TabIndex = 301;
            this.comboBoxGradient.SelectedIndexChanged += new System.EventHandler(this.comboBoxScale_SelectedIndexChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label30.Location = new System.Drawing.Point(8, 26);
            this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(53, 14);
            this.label30.TabIndex = 149;
            this.label30.Text = "Gradient";
            // 
            // comboBoxScale1
            // 
            this.comboBoxScale1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScale1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxScale1.FormattingEnabled = true;
            this.comboBoxScale1.Items.AddRange(new object[] {
            "Log Scale",
            "Liner Scale"});
            this.comboBoxScale1.Location = new System.Drawing.Point(289, 22);
            this.comboBoxScale1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.comboBoxScale1.Name = "comboBoxScale1";
            this.comboBoxScale1.Size = new System.Drawing.Size(135, 22);
            this.comboBoxScale1.TabIndex = 302;
            this.comboBoxScale1.SelectedIndexChanged += new System.EventHandler(this.comboBoxScale_SelectedIndexChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label28.Location = new System.Drawing.Point(224, 26);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(46, 14);
            this.label28.TabIndex = 151;
            this.label28.Text = "Scale 1";
            // 
            // listBoxReferrence
            // 
            this.listBoxReferrence.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.listBoxReferrence.FormattingEnabled = true;
            this.listBoxReferrence.ItemHeight = 16;
            this.listBoxReferrence.Location = new System.Drawing.Point(10, 29);
            this.listBoxReferrence.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.listBoxReferrence.Name = "listBoxReferrence";
            this.listBoxReferrence.Size = new System.Drawing.Size(190, 36);
            this.listBoxReferrence.TabIndex = 162;
            this.listBoxReferrence.SelectedIndexChanged += new System.EventHandler(this.listBoxReferrence_SelectedIndexChanged);
            // 
            // buttonAddRefferencePattern
            // 
            this.buttonAddRefferencePattern.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAddRefferencePattern.Location = new System.Drawing.Point(204, 28);
            this.buttonAddRefferencePattern.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.buttonAddRefferencePattern.Name = "buttonAddRefferencePattern";
            this.buttonAddRefferencePattern.Size = new System.Drawing.Size(94, 34);
            this.buttonAddRefferencePattern.TabIndex = 142;
            this.buttonAddRefferencePattern.Text = "Add";
            this.buttonAddRefferencePattern.UseVisualStyleBackColor = true;
            this.buttonAddRefferencePattern.Click += new System.EventHandler(this.buttonAddRefferencePattern_Click);
            // 
            // buttonRemoveReferrencePattern
            // 
            this.buttonRemoveReferrencePattern.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRemoveReferrencePattern.Location = new System.Drawing.Point(204, 61);
            this.buttonRemoveReferrencePattern.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.buttonRemoveReferrencePattern.Name = "buttonRemoveReferrencePattern";
            this.buttonRemoveReferrencePattern.Size = new System.Drawing.Size(94, 34);
            this.buttonRemoveReferrencePattern.TabIndex = 142;
            this.buttonRemoveReferrencePattern.Text = "Remove";
            this.buttonRemoveReferrencePattern.UseVisualStyleBackColor = true;
            this.buttonRemoveReferrencePattern.Click += new System.EventHandler(this.buttonRemoveReferrencePattern_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.tabControl1.Location = new System.Drawing.Point(9, 103);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(987, 805);
            this.tabControl1.TabIndex = 162;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            this.tabControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseDoubleClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.diffractionPatternControlSimulation);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.tabPage1.Size = new System.Drawing.Size(979, 776);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Simulated Pattern";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // diffractionPatternControlSimulation
            // 
            this.diffractionPatternControlSimulation.BackColor = System.Drawing.SystemColors.Control;
            this.diffractionPatternControlSimulation.Cameralength = 450D;
            this.diffractionPatternControlSimulation.Center = ((Crystallography.PointD)(resources.GetObject("diffractionPatternControlSimulation.Center")));
            this.diffractionPatternControlSimulation.Convergence = 0.0008726646259971648D;
            this.diffractionPatternControlSimulation.ConvergenceDegree = 0.05D;
            this.diffractionPatternControlSimulation.Crystals = null;
            this.diffractionPatternControlSimulation.DetectorProperty = ((Crystallography.AreaDetector)(resources.GetObject("diffractionPatternControlSimulation.DetectorProperty")));
            this.diffractionPatternControlSimulation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diffractionPatternControlSimulation.FilmBlur = 200D;
            this.diffractionPatternControlSimulation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.diffractionPatternControlSimulation.ImageHeight = 900;
            this.diffractionPatternControlSimulation.ImageWidth = 900;
            this.diffractionPatternControlSimulation.IsReferrenceImage = false;
            this.diffractionPatternControlSimulation.Location = new System.Drawing.Point(4, 2);
            this.diffractionPatternControlSimulation.Margin = new System.Windows.Forms.Padding(4);
            this.diffractionPatternControlSimulation.Monochromaticity = 1E-05D;
            this.diffractionPatternControlSimulation.Name = "diffractionPatternControlSimulation";
            this.diffractionPatternControlSimulation.Phi = 0D;
            this.diffractionPatternControlSimulation.Resolution = 0.3D;
            this.diffractionPatternControlSimulation.SimulationCheck = false;
            this.diffractionPatternControlSimulation.Size = new System.Drawing.Size(971, 772);
            this.diffractionPatternControlSimulation.TabIndex = 0;
            this.diffractionPatternControlSimulation.Tau = 0D;
            this.diffractionPatternControlSimulation.Wavelength = 0.041328040768899996D;
            this.diffractionPatternControlSimulation.WaveProperty = ((Crystallography.WaveProperty)(resources.GetObject("diffractionPatternControlSimulation.WaveProperty")));
            this.diffractionPatternControlSimulation.WaveSource = Crystallography.WaveSource.Xray;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar,
            this.toolStripStatusLabelProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 911);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1538, 22);
            this.statusStrip1.TabIndex = 133;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(10, 17);
            this.toolStripStatusLabel.Text = " ";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(125, 16);
            // 
            // toolStripStatusLabelProgress
            // 
            this.toolStripStatusLabelProgress.Name = "toolStripStatusLabelProgress";
            this.toolStripStatusLabelProgress.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelProgress.Text = "  ";
            // 
            // FormPolycrystallineDiffractionSimulator
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1538, 933);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "FormPolycrystallineDiffractionSimulator";
            this.Text = "Powder Diffraction Simulator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPolycrystallineDiffractionSimulator_FormClosing);
            this.Load += new System.EventHandler(this.FormPolycrystallineDiffractionSimulator_Load);
            this.VisibleChanged += new System.EventHandler(this.FormPolycrystallineDiffractionSimulator_VisibleChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormPolycrystallineDiffractionSimulator_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormPolycrystallineDiffractionSimulator_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControlCrystals.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabControl3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChangeParameterThreshold)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSearch;
        public System.Windows.Forms.ComboBox comboBoxScale2;
        public System.Windows.Forms.ComboBox comboBoxScale1;
        public System.Windows.Forms.ComboBox comboBoxGradient;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button buttonAddRefferencePattern;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox listBoxReferrence;
        private System.Windows.Forms.Button buttonRemoveReferrencePattern;
        private DiffractionPatternControl diffractionPatternControlSimulation;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMain;
        private Crystallography.Controls.GraphControl graphControlResidual;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonSaveCurrentSetting;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonLoadSetting;
        private System.Windows.Forms.TabControl tabControl3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBoxAutomaticallyChangeParameter;
        private System.Windows.Forms.CheckBox checkBoxRefineConvergence;
        private System.Windows.Forms.CheckBox checkBoxRefineFilmBlur;
        private System.Windows.Forms.CheckBox checkBoxRefineCenterOffset;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.CheckBox checkBoxRefineStress;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox checkBoxRefinePreferredOrientation;
        public Crystallography.Controls.NumericBox numericBoxRxSpeed;
        public Crystallography.Controls.NumericBox numericBoxYusaGonioRySpeed;
        public Crystallography.Controls.NumericBox numericBoxYusaGonioRzSpeed;
        public System.Windows.Forms.RadioButton radioButtonZigzagScan;
        public Crystallography.Controls.NumericBox numericBoxYusaGonioRyStep;
        public Crystallography.Controls.NumericBox numericBoxYusaGonioRyOscillation;
        public Crystallography.Controls.NumericBox numericBoxYusaGonioRzOscillation;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.CheckBox checkBoxYusaGonio_ValidRx;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.CheckBox checkBoxYusaGonioScan;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.Button buttonSearchUnrelatedOrientations;
        private System.Windows.Forms.GroupBox groupBox7;
        private Crystallography.Controls.CrystalControl crystalControl1;
        private System.Windows.Forms.Button buttonSimulateDebyeRing;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProgress;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.NumericUpDown numericUpDownChangeParameterThreshold;
        private System.Windows.Forms.TabControl tabControlCrystals;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button1;
        private Crystallography.Controls.NumericBox numericBoxCrystalNumPerStep;
        private Crystallography.Controls.NumericBox numericBoxInheritabiliry;
        private Crystallography.Controls.NumericBox numericBoxDirectionalDensity;
        private System.Windows.Forms.CheckBox checkBoxCrystalNumPerStepThreshold;
        private System.Windows.Forms.CheckBox checkBoxInheritabiliryThreshold;
        private System.Windows.Forms.CheckBox checkBoxDirectionalDensityThreshold;
        private Crystallography.Controls.NumericBox numericBoxCrystalNumPerStepThreshold;
        private Crystallography.Controls.NumericBox numericBoxDirectionalDensityThreshold;
        private Crystallography.Controls.NumericBox numericBoxInheritabiliryThreshold;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}