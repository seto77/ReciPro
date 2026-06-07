namespace ReciPro
{
    partial class FormPolycrystallineDiffractionSimulator
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
        // groupBox1 -> groupBoxCrystalProperty
        // groupBox2 -> groupBoxAppearance
        // groupBox3 -> groupBoxPatterns
        // groupBox4 -> groupBoxPreferredOrientation
        // groupBox6 -> groupBoxOrientationFitting
        // groupBox7 -> groupBoxGonioScan
        // groupBox8 -> groupBoxFittingOptions
        private void InitializeComponent()
        {
            captureExtender.SetCapture(this, true); // 260521Cl 追加: GUI監査キャプチャ対象 (フォーム全体)
            components = new System.ComponentModel.Container();
            toolTip = new System.Windows.Forms.ToolTip(components); // 260531Cl
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPolycrystallineDiffractionSimulator));
            backgroundWorkerMain = new System.ComponentModel.BackgroundWorker();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            groupBoxCrystalProperty = new System.Windows.Forms.GroupBox();
            button1 = new System.Windows.Forms.Button();
            buttonSimulateDebyeRing = new System.Windows.Forms.Button();
            tabControlCrystals = new System.Windows.Forms.TabControl();
            tabPage2 = new System.Windows.Forms.TabPage();
            crystalControl1 = new Crystallography.Controls.CrystalControl();
            groupBoxOrientationFitting = new System.Windows.Forms.GroupBox();
            buttonLoadSetting = new System.Windows.Forms.Button();
            buttonSaveCurrentSetting = new System.Windows.Forms.Button();
            buttonSearch = new System.Windows.Forms.Button();
            tabControl3 = new System.Windows.Forms.TabControl();
            tabPage4 = new System.Windows.Forms.TabPage();
            groupBoxPreferredOrientation = new System.Windows.Forms.GroupBox();
            checkBoxCrystalNumPerStepThreshold = new System.Windows.Forms.CheckBox();
            numericBoxCrystalNumPerStep = new Crystallography.Controls.NumericBox();
            numericBoxInheritabiliry = new Crystallography.Controls.NumericBox();
            checkBoxInheritabiliryThreshold = new System.Windows.Forms.CheckBox();
            numericBoxInheritabiliryThreshold = new Crystallography.Controls.NumericBox();
            checkBoxDirectionalDensityThreshold = new System.Windows.Forms.CheckBox();
            numericBoxDirectionalDensity = new Crystallography.Controls.NumericBox();
            numericBoxCrystalNumPerStepThreshold = new Crystallography.Controls.NumericBox();
            numericBoxDirectionalDensityThreshold = new Crystallography.Controls.NumericBox();
            groupBoxFittingOptions = new System.Windows.Forms.GroupBox();
            checkBoxRefineConvergence = new System.Windows.Forms.CheckBox();
            checkBoxRefineStress = new System.Windows.Forms.CheckBox();
            checkBoxRefineCenterOffset = new System.Windows.Forms.CheckBox();
            checkBoxRefinePreferredOrientation = new System.Windows.Forms.CheckBox();
            checkBoxRefineFilmBlur = new System.Windows.Forms.CheckBox();
            checkBox6 = new System.Windows.Forms.CheckBox();
            checkBoxAutomaticallyChangeParameter = new System.Windows.Forms.CheckBox();
            numericBoxChangeParameterThreshold = new Crystallography.Controls.NumericBox();                                                       // 260522Cl 変更: NumericUpDown → NumericBox
            tabPage6 = new System.Windows.Forms.TabPage();
            textBox1 = new System.Windows.Forms.TextBox();
            graphControlResidual = new Crystallography.Controls.GraphControl();
            tabPage8 = new System.Windows.Forms.TabPage();
            buttonSearchUnrelatedOrientations = new System.Windows.Forms.Button();
            groupBoxGonioScan = new System.Windows.Forms.GroupBox();
            checkBoxYusaGonioScan = new System.Windows.Forms.CheckBox();
            numericBoxRxSpeed = new Crystallography.Controls.NumericBox();
            checkBoxYusaGonio_ValidRx = new System.Windows.Forms.CheckBox();
            label52 = new System.Windows.Forms.Label();
            numericBoxYusaGonioRySpeed = new Crystallography.Controls.NumericBox();
            label51 = new System.Windows.Forms.Label();
            numericBoxYusaGonioRzSpeed = new Crystallography.Controls.NumericBox();
            label53 = new System.Windows.Forms.Label();
            label50 = new System.Windows.Forms.Label();
            radioButtonZigzagScan = new System.Windows.Forms.RadioButton();
            label54 = new System.Windows.Forms.Label();
            label49 = new System.Windows.Forms.Label();
            numericBoxYusaGonioRyStep = new Crystallography.Controls.NumericBox();
            label55 = new System.Windows.Forms.Label();
            label48 = new System.Windows.Forms.Label();
            numericBoxYusaGonioRyOscillation = new Crystallography.Controls.NumericBox();
            label56 = new System.Windows.Forms.Label();
            label60 = new System.Windows.Forms.Label();
            label47 = new System.Windows.Forms.Label();
            numericBoxYusaGonioRzOscillation = new Crystallography.Controls.NumericBox();
            label57 = new System.Windows.Forms.Label();
            label59 = new System.Windows.Forms.Label();
            checkBox2 = new System.Windows.Forms.CheckBox();
            checkBox3 = new System.Windows.Forms.CheckBox();
            label58 = new System.Windows.Forms.Label();
            groupBoxPatterns = new System.Windows.Forms.GroupBox();
            groupBoxAppearance = new System.Windows.Forms.GroupBox();
            comboBoxScale2 = new System.Windows.Forms.ComboBox();
            label26 = new System.Windows.Forms.Label();
            comboBoxGradient = new System.Windows.Forms.ComboBox();
            label30 = new System.Windows.Forms.Label();
            comboBoxScale1 = new System.Windows.Forms.ComboBox();
            label28 = new System.Windows.Forms.Label();
            listBoxReferrence = new System.Windows.Forms.ListBox();
            buttonAddRefferencePattern = new System.Windows.Forms.Button();
            buttonRemoveReferrencePattern = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            diffractionPatternControlSimulation = new ReciPro.DiffractionPatternControl();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabelProgress = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBoxCrystalProperty.SuspendLayout();
            tabControlCrystals.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBoxOrientationFitting.SuspendLayout();
            tabControl3.SuspendLayout();
            tabPage4.SuspendLayout();
            groupBoxPreferredOrientation.SuspendLayout();
            groupBoxFittingOptions.SuspendLayout();
            tabPage6.SuspendLayout();
            tabPage8.SuspendLayout();
            groupBoxGonioScan.SuspendLayout();
            groupBoxPatterns.SuspendLayout();
            groupBoxAppearance.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // backgroundWorkerMain
            // 
            backgroundWorkerMain.WorkerReportsProgress = true;
            backgroundWorkerMain.WorkerSupportsCancellation = true;
            backgroundWorkerMain.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorkerMain_DoWork);
            backgroundWorkerMain.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(backgroundWorkerMain_ProgressChanged);
            backgroundWorkerMain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorkerMain_RunWorkerCompleted);
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBoxCrystalProperty);
            splitContainer1.Panel1.Controls.Add(groupBoxOrientationFitting);
            splitContainer1.Panel1.Controls.Add(groupBoxGonioScan);
            splitContainer1.Panel1MinSize = 334;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBoxPatterns);
            splitContainer1.Size = new System.Drawing.Size(1538, 911);
            splitContainer1.SplitterDistance = 532;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 132;
            // 
            // groupBoxCrystalProperty
            // 
            groupBoxCrystalProperty.Controls.Add(button1);
            groupBoxCrystalProperty.Controls.Add(buttonSimulateDebyeRing);
            groupBoxCrystalProperty.Controls.Add(tabControlCrystals);
            groupBoxCrystalProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBoxCrystalProperty.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            groupBoxCrystalProperty.Location = new System.Drawing.Point(0, 0);
            groupBoxCrystalProperty.Margin = new System.Windows.Forms.Padding(0);
            groupBoxCrystalProperty.Name = "groupBoxCrystalProperty";
            groupBoxCrystalProperty.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            groupBoxCrystalProperty.Size = new System.Drawing.Size(532, 609);
            groupBoxCrystalProperty.TabIndex = 129;
            groupBoxCrystalProperty.TabStop = false;
            groupBoxCrystalProperty.Text = "Crystal property";
            // 
            // button1
            // 
            button1.AutoSize = true;
            button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            button1.Location = new System.Drawing.Point(468, 15);
            button1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            button1.Name = "button1";
            toolTip.SetToolTip(button1, resources.GetString("button1.ToolTip")); // 260531Cl
            button1.Size = new System.Drawing.Size(38, 26);
            button1.TabIndex = 1003;
            button1.Text = "test";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new System.EventHandler(button1_Click);
            // 
            // buttonSimulateDebyeRing
            // 
            buttonSimulateDebyeRing.AutoSize = true;
            buttonSimulateDebyeRing.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonSimulateDebyeRing.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonSimulateDebyeRing.Location = new System.Drawing.Point(155, 15);
            buttonSimulateDebyeRing.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonSimulateDebyeRing.Name = "buttonSimulateDebyeRing";
            toolTip.SetToolTip(buttonSimulateDebyeRing, resources.GetString("buttonSimulateDebyeRing.ToolTip")); // 260531Cl
            buttonSimulateDebyeRing.Size = new System.Drawing.Size(177, 26);
            buttonSimulateDebyeRing.TabIndex = 1003;
            buttonSimulateDebyeRing.Text = "Simulate Debye ring pattern";
            buttonSimulateDebyeRing.BackColor = System.Drawing.Color.SteelBlue; // 260520Cl: 主要アクション色を統一
            buttonSimulateDebyeRing.ForeColor = System.Drawing.Color.White;
            buttonSimulateDebyeRing.UseVisualStyleBackColor = false;
            buttonSimulateDebyeRing.Click += new System.EventHandler(buttonSimulateDebyeRing_Click);
            // 
            // tabControlCrystals
            // 
            tabControlCrystals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            tabControlCrystals.Controls.Add(tabPage2);
            tabControlCrystals.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            tabControlCrystals.Location = new System.Drawing.Point(5, 50);
            tabControlCrystals.Multiline = true;
            tabControlCrystals.Name = "tabControlCrystals";
            tabControlCrystals.SelectedIndex = 0;
            tabControlCrystals.Size = new System.Drawing.Size(522, 553);
            tabControlCrystals.TabIndex = 1005;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(crystalControl1);
            tabPage2.Location = new System.Drawing.Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(514, 523);
            tabPage2.TabIndex = 0;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // crystalControl1
            // 
            crystalControl1.AllowDrop = true;
            crystalControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            crystalControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            crystalControl1.Enabled = false;
            crystalControl1.Location = new System.Drawing.Point(3, 3);
            crystalControl1.Margin = new System.Windows.Forms.Padding(0);
            crystalControl1.Name = "crystalControl1";
            crystalControl1.Size = new System.Drawing.Size(508, 517);
            crystalControl1.SkipEvent = false;
            crystalControl1.TabIndex = 1002;
            crystalControl1.VisibleBondsPolyhedraTab = false;
            crystalControl1.VisibleEOSTab = false;
            crystalControl1.VisiblePolycrystallineTab = true;
            crystalControl1.VisibleReferenceTab = false;
            crystalControl1.VisibleStressStrainTab = true;
            crystalControl1.VisibleChanged += new System.EventHandler(crystalControl1_VisibleChanged);
            // 
            // groupBoxOrientationFitting
            // 
            groupBoxOrientationFitting.Controls.Add(buttonLoadSetting);
            groupBoxOrientationFitting.Controls.Add(buttonSaveCurrentSetting);
            groupBoxOrientationFitting.Controls.Add(buttonSearch);
            groupBoxOrientationFitting.Controls.Add(tabControl3);
            groupBoxOrientationFitting.Dock = System.Windows.Forms.DockStyle.Bottom;
            groupBoxOrientationFitting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            groupBoxOrientationFitting.Location = new System.Drawing.Point(0, 609);
            groupBoxOrientationFitting.Margin = new System.Windows.Forms.Padding(0);
            groupBoxOrientationFitting.Name = "groupBoxOrientationFitting";
            groupBoxOrientationFitting.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            groupBoxOrientationFitting.Size = new System.Drawing.Size(532, 302);
            groupBoxOrientationFitting.TabIndex = 129;
            groupBoxOrientationFitting.TabStop = false;
            groupBoxOrientationFitting.Text = "Fitting orientations";
            // 
            // buttonLoadSetting
            // 
            buttonLoadSetting.AutoSize = true;
            buttonLoadSetting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonLoadSetting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonLoadSetting.Location = new System.Drawing.Point(291, 21);
            buttonLoadSetting.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonLoadSetting.Name = "buttonLoadSetting";
            toolTip.SetToolTip(buttonLoadSetting, resources.GetString("buttonLoadSetting.ToolTip")); // 260531Cl
            buttonLoadSetting.Size = new System.Drawing.Size(86, 26);
            buttonLoadSetting.TabIndex = 68;
            buttonLoadSetting.Text = "Load setting";
            buttonLoadSetting.UseVisualStyleBackColor = true;
            buttonLoadSetting.Click += new System.EventHandler(buttonLoadSetting_Click);
            // 
            // buttonSaveCurrentSetting
            // 
            buttonSaveCurrentSetting.AutoSize = true;
            buttonSaveCurrentSetting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonSaveCurrentSetting.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonSaveCurrentSetting.Location = new System.Drawing.Point(388, 21);
            buttonSaveCurrentSetting.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonSaveCurrentSetting.Name = "buttonSaveCurrentSetting";
            toolTip.SetToolTip(buttonSaveCurrentSetting, resources.GetString("buttonSaveCurrentSetting.ToolTip")); // 260531Cl
            buttonSaveCurrentSetting.Size = new System.Drawing.Size(132, 26);
            buttonSaveCurrentSetting.TabIndex = 68;
            buttonSaveCurrentSetting.Text = "Save current setting";
            buttonSaveCurrentSetting.UseVisualStyleBackColor = true;
            buttonSaveCurrentSetting.Click += new System.EventHandler(buttonSaveCurrentSetting_Click);
            // 
            // buttonSearch
            // 
            buttonSearch.AutoSize = true;
            buttonSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            buttonSearch.Location = new System.Drawing.Point(14, 21);
            buttonSearch.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonSearch.Name = "buttonSearch";
            toolTip.SetToolTip(buttonSearch, resources.GetString("buttonSearch.ToolTip")); // 260531Cl
            buttonSearch.Size = new System.Drawing.Size(146, 26);
            buttonSearch.TabIndex = 68;
            buttonSearch.Text = "Search Orientations";
            buttonSearch.BackColor = System.Drawing.Color.SteelBlue; // 260520Cl 追加: 主要アクション(検索)を水色に統一
            buttonSearch.ForeColor = System.Drawing.Color.White; // 260520Cl 追加
            buttonSearch.UseVisualStyleBackColor = false; // 260520Cl 変更: BackColor有効化のため
            buttonSearch.Click += new System.EventHandler(buttonSearch_Click);
            // 
            // tabControl3
            // 
            tabControl3.Controls.Add(tabPage4);
            tabControl3.Controls.Add(tabPage6);
            tabControl3.Controls.Add(tabPage8);
            tabControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            tabControl3.Location = new System.Drawing.Point(5, 57);
            tabControl3.Margin = new System.Windows.Forms.Padding(4);
            tabControl3.Name = "tabControl3";
            tabControl3.SelectedIndex = 0;
            tabControl3.Size = new System.Drawing.Size(522, 239);
            tabControl3.TabIndex = 166;
            // 
            // tabPage4
            // 
            tabPage4.BackColor = System.Drawing.SystemColors.Control;
            tabPage4.Controls.Add(groupBoxPreferredOrientation);
            tabPage4.Controls.Add(groupBoxFittingOptions);
            tabPage4.Controls.Add(checkBox6);
            tabPage4.Controls.Add(checkBoxAutomaticallyChangeParameter);
            tabPage4.Controls.Add(numericBoxChangeParameterThreshold);
            tabPage4.Location = new System.Drawing.Point(4, 25);
            tabPage4.Margin = new System.Windows.Forms.Padding(4);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new System.Windows.Forms.Padding(4);
            tabPage4.Size = new System.Drawing.Size(514, 210);
            tabPage4.TabIndex = 0;
            tabPage4.Text = "Refinement option";
            // 
            // groupBoxPreferredOrientation
            // 
            groupBoxPreferredOrientation.Controls.Add(checkBoxCrystalNumPerStepThreshold);
            groupBoxPreferredOrientation.Controls.Add(numericBoxCrystalNumPerStep);
            groupBoxPreferredOrientation.Controls.Add(numericBoxInheritabiliry);
            groupBoxPreferredOrientation.Controls.Add(checkBoxInheritabiliryThreshold);
            groupBoxPreferredOrientation.Controls.Add(numericBoxInheritabiliryThreshold);
            groupBoxPreferredOrientation.Controls.Add(checkBoxDirectionalDensityThreshold);
            groupBoxPreferredOrientation.Controls.Add(numericBoxDirectionalDensity);
            groupBoxPreferredOrientation.Controls.Add(numericBoxCrystalNumPerStepThreshold);
            groupBoxPreferredOrientation.Controls.Add(numericBoxDirectionalDensityThreshold);
            groupBoxPreferredOrientation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            groupBoxPreferredOrientation.Location = new System.Drawing.Point(138, 4);
            groupBoxPreferredOrientation.Name = "groupBoxPreferredOrientation";
            groupBoxPreferredOrientation.Size = new System.Drawing.Size(371, 121);
            groupBoxPreferredOrientation.TabIndex = 409;
            groupBoxPreferredOrientation.TabStop = false;
            groupBoxPreferredOrientation.Text = "Fitting parameters for preferred orientation"; // 260521Cl Phase7: orientatin→orientation typo
            // 
            // checkBoxCrystalNumPerStepThreshold
            // 
            checkBoxCrystalNumPerStepThreshold.AutoSize = true;
            checkBoxCrystalNumPerStepThreshold.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxCrystalNumPerStepThreshold.Location = new System.Drawing.Point(201, 28);
            checkBoxCrystalNumPerStepThreshold.Name = "checkBoxCrystalNumPerStepThreshold";
            toolTip.SetToolTip(checkBoxCrystalNumPerStepThreshold, resources.GetString("checkBoxCrystalNumPerStepThreshold.ToolTip")); // 260531Cl
            checkBoxCrystalNumPerStepThreshold.Size = new System.Drawing.Size(83, 20);
            checkBoxCrystalNumPerStepThreshold.TabIndex = 408;
            checkBoxCrystalNumPerStepThreshold.Text = "Threshold";
            checkBoxCrystalNumPerStepThreshold.UseVisualStyleBackColor = true;
            checkBoxCrystalNumPerStepThreshold.CheckedChanged += new System.EventHandler(checkBoxCrystalNumPerStepThreshold_CheckedChanged);
            // 
            // numericBoxCrystalNumPerStep
            // 
            numericBoxCrystalNumPerStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCrystalNumPerStep.DecimalPlaces = 3;
            numericBoxCrystalNumPerStep.FooterFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxCrystalNumPerStep.FooterText = "%";
            numericBoxCrystalNumPerStep.HeaderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxCrystalNumPerStep.HeaderText = "Num per Step";
            numericBoxCrystalNumPerStep.Location = new System.Drawing.Point(18, 27);
            numericBoxCrystalNumPerStep.Margin = new System.Windows.Forms.Padding(0);
            numericBoxCrystalNumPerStep.Maximum = 10D;
            numericBoxCrystalNumPerStep.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxCrystalNumPerStep.Minimum = 1E-06D;
            numericBoxCrystalNumPerStep.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxCrystalNumPerStep.Name = "numericBoxCrystalNumPerStep";
            toolTip.SetToolTip(numericBoxCrystalNumPerStep, resources.GetString("numericBoxCrystalNumPerStep.ToolTip")); // 260531Cl
            numericBoxCrystalNumPerStep.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxCrystalNumPerStep.RadianValue = 0.0087266462599716477D;
            numericBoxCrystalNumPerStep.ShowUpDown = true;
            numericBoxCrystalNumPerStep.Size = new System.Drawing.Size(168, 25);
            numericBoxCrystalNumPerStep.SkipEventDuringInput = false;
            numericBoxCrystalNumPerStep.SmartIncrement = true;
            numericBoxCrystalNumPerStep.TabIndex = 407;
            numericBoxCrystalNumPerStep.ValueFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxCrystalNumPerStep.ThousandsSeparator = true;
            numericBoxCrystalNumPerStep.Value = 0.5D;
            // 
            // numericBoxInheritabiliry
            // 
            numericBoxInheritabiliry.BackColor = System.Drawing.SystemColors.Control;
            numericBoxInheritabiliry.DecimalPlaces = 2;
            numericBoxInheritabiliry.FooterFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxInheritabiliry.FooterText = "%";
            numericBoxInheritabiliry.HeaderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxInheritabiliry.HeaderText = "Inheritability";
            numericBoxInheritabiliry.Location = new System.Drawing.Point(18, 57);
            numericBoxInheritabiliry.Margin = new System.Windows.Forms.Padding(0);
            numericBoxInheritabiliry.Maximum = 100D;
            numericBoxInheritabiliry.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxInheritabiliry.Minimum = 0D;
            numericBoxInheritabiliry.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxInheritabiliry.Name = "numericBoxInheritabiliry";
            toolTip.SetToolTip(numericBoxInheritabiliry, resources.GetString("numericBoxInheritabiliry.ToolTip")); // 260531Cl
            numericBoxInheritabiliry.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxInheritabiliry.RadianValue = 0.17453292519943295D;
            numericBoxInheritabiliry.ShowUpDown = true;
            numericBoxInheritabiliry.Size = new System.Drawing.Size(168, 25);
            numericBoxInheritabiliry.SkipEventDuringInput = false;
            numericBoxInheritabiliry.SmartIncrement = true;
            numericBoxInheritabiliry.TabIndex = 407;
            numericBoxInheritabiliry.ValueFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxInheritabiliry.ThousandsSeparator = true;
            numericBoxInheritabiliry.Value = 10D;
            // 
            // checkBoxInheritabiliryThreshold
            // 
            checkBoxInheritabiliryThreshold.AutoSize = true;
            checkBoxInheritabiliryThreshold.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxInheritabiliryThreshold.Location = new System.Drawing.Point(201, 58);
            checkBoxInheritabiliryThreshold.Name = "checkBoxInheritabiliryThreshold";
            toolTip.SetToolTip(checkBoxInheritabiliryThreshold, resources.GetString("checkBoxInheritabiliryThreshold.ToolTip")); // 260531Cl
            checkBoxInheritabiliryThreshold.Size = new System.Drawing.Size(83, 20);
            checkBoxInheritabiliryThreshold.TabIndex = 408;
            checkBoxInheritabiliryThreshold.Text = "Threshold";
            checkBoxInheritabiliryThreshold.UseVisualStyleBackColor = true;
            checkBoxInheritabiliryThreshold.CheckedChanged += new System.EventHandler(checkBoxInheritabiliryThreshold_CheckedChanged);
            // 
            // numericBoxInheritabiliryThreshold
            // 
            numericBoxInheritabiliryThreshold.BackColor = System.Drawing.SystemColors.Control;
            numericBoxInheritabiliryThreshold.DecimalPlaces = 2;
            numericBoxInheritabiliryThreshold.FooterFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxInheritabiliryThreshold.FooterText = "%";
            numericBoxInheritabiliryThreshold.HeaderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxInheritabiliryThreshold.Location = new System.Drawing.Point(288, 57);
            numericBoxInheritabiliryThreshold.Margin = new System.Windows.Forms.Padding(0);
            numericBoxInheritabiliryThreshold.Maximum = 100D;
            numericBoxInheritabiliryThreshold.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxInheritabiliryThreshold.Minimum = 0D;
            numericBoxInheritabiliryThreshold.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxInheritabiliryThreshold.Name = "numericBoxInheritabiliryThreshold";
            toolTip.SetToolTip(numericBoxInheritabiliryThreshold, resources.GetString("numericBoxInheritabiliryThreshold.ToolTip")); // 260531Cl
            numericBoxInheritabiliryThreshold.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxInheritabiliryThreshold.RadianValue = 1.6755160819145563D;
            numericBoxInheritabiliryThreshold.ShowUpDown = true;
            numericBoxInheritabiliryThreshold.Size = new System.Drawing.Size(79, 25);
            numericBoxInheritabiliryThreshold.SkipEventDuringInput = false;
            numericBoxInheritabiliryThreshold.SmartIncrement = true;
            numericBoxInheritabiliryThreshold.TabIndex = 407;
            numericBoxInheritabiliryThreshold.ValueFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxInheritabiliryThreshold.ThousandsSeparator = true;
            numericBoxInheritabiliryThreshold.Value = 96D;
            numericBoxInheritabiliryThreshold.Visible = false;
            // 
            // checkBoxDirectionalDensityThreshold
            // 
            checkBoxDirectionalDensityThreshold.AutoSize = true;
            checkBoxDirectionalDensityThreshold.Checked = true;
            checkBoxDirectionalDensityThreshold.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDirectionalDensityThreshold.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxDirectionalDensityThreshold.Location = new System.Drawing.Point(201, 88);
            checkBoxDirectionalDensityThreshold.Name = "checkBoxDirectionalDensityThreshold";
            toolTip.SetToolTip(checkBoxDirectionalDensityThreshold, resources.GetString("checkBoxDirectionalDensityThreshold.ToolTip")); // 260531Cl
            checkBoxDirectionalDensityThreshold.Size = new System.Drawing.Size(83, 20);
            checkBoxDirectionalDensityThreshold.TabIndex = 408;
            checkBoxDirectionalDensityThreshold.Text = "Threshold";
            checkBoxDirectionalDensityThreshold.UseVisualStyleBackColor = true;
            checkBoxDirectionalDensityThreshold.CheckedChanged += new System.EventHandler(checkBoxDirectionalDensityThreshold_CheckedChanged);
            // 
            // numericBoxDirectionalDensity
            // 
            numericBoxDirectionalDensity.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDirectionalDensity.DecimalPlaces = 2;
            numericBoxDirectionalDensity.FooterFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxDirectionalDensity.FooterText = "°";
            numericBoxDirectionalDensity.HeaderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxDirectionalDensity.HeaderText = "Directional density";
            numericBoxDirectionalDensity.Location = new System.Drawing.Point(2, 88);
            numericBoxDirectionalDensity.Margin = new System.Windows.Forms.Padding(0);
            numericBoxDirectionalDensity.Maximum = 720D;
            numericBoxDirectionalDensity.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxDirectionalDensity.Minimum = 0.1D;
            numericBoxDirectionalDensity.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxDirectionalDensity.Name = "numericBoxDirectionalDensity";
            toolTip.SetToolTip(numericBoxDirectionalDensity, resources.GetString("numericBoxDirectionalDensity.ToolTip")); // 260531Cl
            numericBoxDirectionalDensity.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxDirectionalDensity.RadianValue = 0.3490658503988659D;
            numericBoxDirectionalDensity.ShowUpDown = true;
            numericBoxDirectionalDensity.Size = new System.Drawing.Size(179, 25);
            numericBoxDirectionalDensity.SkipEventDuringInput = false;
            numericBoxDirectionalDensity.SmartIncrement = true;
            numericBoxDirectionalDensity.TabIndex = 407;
            numericBoxDirectionalDensity.ValueFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxDirectionalDensity.ThousandsSeparator = true;
            numericBoxDirectionalDensity.Value = 20D;
            // 
            // numericBoxCrystalNumPerStepThreshold
            // 
            numericBoxCrystalNumPerStepThreshold.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCrystalNumPerStepThreshold.DecimalPlaces = 3;
            numericBoxCrystalNumPerStepThreshold.FooterFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxCrystalNumPerStepThreshold.FooterText = "%";
            numericBoxCrystalNumPerStepThreshold.HeaderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxCrystalNumPerStepThreshold.Location = new System.Drawing.Point(288, 28);
            numericBoxCrystalNumPerStepThreshold.Margin = new System.Windows.Forms.Padding(0);
            numericBoxCrystalNumPerStepThreshold.Maximum = 10D;
            numericBoxCrystalNumPerStepThreshold.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxCrystalNumPerStepThreshold.Minimum = 1E-06D;
            numericBoxCrystalNumPerStepThreshold.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxCrystalNumPerStepThreshold.Name = "numericBoxCrystalNumPerStepThreshold";
            toolTip.SetToolTip(numericBoxCrystalNumPerStepThreshold, resources.GetString("numericBoxCrystalNumPerStepThreshold.ToolTip")); // 260531Cl
            numericBoxCrystalNumPerStepThreshold.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxCrystalNumPerStepThreshold.RadianValue = 0.0004363323129985824D;
            numericBoxCrystalNumPerStepThreshold.ShowUpDown = true;
            numericBoxCrystalNumPerStepThreshold.Size = new System.Drawing.Size(79, 25);
            numericBoxCrystalNumPerStepThreshold.SkipEventDuringInput = false;
            numericBoxCrystalNumPerStepThreshold.SmartIncrement = true;
            numericBoxCrystalNumPerStepThreshold.TabIndex = 407;
            numericBoxCrystalNumPerStepThreshold.ValueFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxCrystalNumPerStepThreshold.ThousandsSeparator = true;
            numericBoxCrystalNumPerStepThreshold.Value = 0.025D;
            numericBoxCrystalNumPerStepThreshold.Visible = false;
            // 
            // numericBoxDirectionalDensityThreshold
            // 
            numericBoxDirectionalDensityThreshold.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDirectionalDensityThreshold.DecimalPlaces = 2;
            numericBoxDirectionalDensityThreshold.FooterFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxDirectionalDensityThreshold.FooterText = "°";
            numericBoxDirectionalDensityThreshold.HeaderFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxDirectionalDensityThreshold.Location = new System.Drawing.Point(288, 88);
            numericBoxDirectionalDensityThreshold.Margin = new System.Windows.Forms.Padding(0);
            numericBoxDirectionalDensityThreshold.Maximum = 720D;
            numericBoxDirectionalDensityThreshold.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxDirectionalDensityThreshold.Minimum = 0.1D;
            numericBoxDirectionalDensityThreshold.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxDirectionalDensityThreshold.Name = "numericBoxDirectionalDensityThreshold";
            toolTip.SetToolTip(numericBoxDirectionalDensityThreshold, resources.GetString("numericBoxDirectionalDensityThreshold.ToolTip")); // 260531Cl
            numericBoxDirectionalDensityThreshold.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxDirectionalDensityThreshold.RadianValue = 0.017453292519943295D;
            numericBoxDirectionalDensityThreshold.ShowUpDown = true;
            numericBoxDirectionalDensityThreshold.Size = new System.Drawing.Size(75, 25);
            numericBoxDirectionalDensityThreshold.SkipEventDuringInput = false;
            numericBoxDirectionalDensityThreshold.SmartIncrement = true;
            numericBoxDirectionalDensityThreshold.TabIndex = 407;
            numericBoxDirectionalDensityThreshold.ValueFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxDirectionalDensityThreshold.ThousandsSeparator = true;
            numericBoxDirectionalDensityThreshold.Value = 1D;
            // 
            // groupBoxFittingOptions
            // 
            groupBoxFittingOptions.Controls.Add(checkBoxRefineConvergence);
            groupBoxFittingOptions.Controls.Add(checkBoxRefineStress);
            groupBoxFittingOptions.Controls.Add(checkBoxRefineCenterOffset);
            groupBoxFittingOptions.Controls.Add(checkBoxRefinePreferredOrientation);
            groupBoxFittingOptions.Controls.Add(checkBoxRefineFilmBlur);
            groupBoxFittingOptions.Location = new System.Drawing.Point(4, 4);
            groupBoxFittingOptions.Margin = new System.Windows.Forms.Padding(4);
            groupBoxFittingOptions.Name = "groupBoxFittingOptions";
            groupBoxFittingOptions.Padding = new System.Windows.Forms.Padding(4);
            groupBoxFittingOptions.Size = new System.Drawing.Size(127, 198);
            groupBoxFittingOptions.TabIndex = 166;
            groupBoxFittingOptions.TabStop = false;
            groupBoxFittingOptions.Text = "Fitting option"; // 260521Cl Phase7: Title→sentence case
            // 
            // checkBoxRefineConvergence
            // 
            checkBoxRefineConvergence.AutoSize = true;
            checkBoxRefineConvergence.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxRefineConvergence.Location = new System.Drawing.Point(8, 62);
            checkBoxRefineConvergence.Margin = new System.Windows.Forms.Padding(4);
            checkBoxRefineConvergence.Name = "checkBoxRefineConvergence";
            toolTip.SetToolTip(checkBoxRefineConvergence, resources.GetString("checkBoxRefineConvergence.ToolTip")); // 260531Cl
            checkBoxRefineConvergence.Size = new System.Drawing.Size(102, 36);
            checkBoxRefineConvergence.TabIndex = 409;
            checkBoxRefineConvergence.Text = "Beam\r\n convergence";
            checkBoxRefineConvergence.UseVisualStyleBackColor = true;
            // 
            // checkBoxRefineStress
            // 
            checkBoxRefineStress.AutoSize = true;
            checkBoxRefineStress.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxRefineStress.Location = new System.Drawing.Point(8, 161);
            checkBoxRefineStress.Margin = new System.Windows.Forms.Padding(4);
            checkBoxRefineStress.Name = "checkBoxRefineStress";
            toolTip.SetToolTip(checkBoxRefineStress, resources.GetString("checkBoxRefineStress.ToolTip")); // 260531Cl
            checkBoxRefineStress.Size = new System.Drawing.Size(62, 20);
            checkBoxRefineStress.TabIndex = 412;
            checkBoxRefineStress.Text = "Stress";
            checkBoxRefineStress.UseVisualStyleBackColor = true;
            // 
            // checkBoxRefineCenterOffset
            // 
            checkBoxRefineCenterOffset.AutoSize = true;
            checkBoxRefineCenterOffset.Checked = true;
            checkBoxRefineCenterOffset.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxRefineCenterOffset.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxRefineCenterOffset.Location = new System.Drawing.Point(8, 133);
            checkBoxRefineCenterOffset.Margin = new System.Windows.Forms.Padding(4);
            checkBoxRefineCenterOffset.Name = "checkBoxRefineCenterOffset";
            toolTip.SetToolTip(checkBoxRefineCenterOffset, resources.GetString("checkBoxRefineCenterOffset.ToolTip")); // 260531Cl
            checkBoxRefineCenterOffset.Size = new System.Drawing.Size(100, 20);
            checkBoxRefineCenterOffset.TabIndex = 411;
            checkBoxRefineCenterOffset.Text = "Center offset";
            checkBoxRefineCenterOffset.UseVisualStyleBackColor = true;
            // 
            // checkBoxRefinePreferredOrientation
            // 
            checkBoxRefinePreferredOrientation.AutoSize = true;
            checkBoxRefinePreferredOrientation.Checked = true;
            checkBoxRefinePreferredOrientation.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxRefinePreferredOrientation.Enabled = false;
            checkBoxRefinePreferredOrientation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxRefinePreferredOrientation.Location = new System.Drawing.Point(8, 22);
            checkBoxRefinePreferredOrientation.Margin = new System.Windows.Forms.Padding(4);
            checkBoxRefinePreferredOrientation.Name = "checkBoxRefinePreferredOrientation";
            toolTip.SetToolTip(checkBoxRefinePreferredOrientation, resources.GetString("checkBoxRefinePreferredOrientation.ToolTip")); // 260531Cl
            checkBoxRefinePreferredOrientation.Size = new System.Drawing.Size(91, 36);
            checkBoxRefinePreferredOrientation.TabIndex = 408;
            checkBoxRefinePreferredOrientation.Text = "Preferred \r\n orientation";
            checkBoxRefinePreferredOrientation.UseVisualStyleBackColor = true;
            // 
            // checkBoxRefineFilmBlur
            // 
            checkBoxRefineFilmBlur.AutoSize = true;
            checkBoxRefineFilmBlur.Checked = true;
            checkBoxRefineFilmBlur.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxRefineFilmBlur.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxRefineFilmBlur.Location = new System.Drawing.Point(8, 105);
            checkBoxRefineFilmBlur.Margin = new System.Windows.Forms.Padding(4);
            checkBoxRefineFilmBlur.Name = "checkBoxRefineFilmBlur";
            toolTip.SetToolTip(checkBoxRefineFilmBlur, resources.GetString("checkBoxRefineFilmBlur.ToolTip")); // 260531Cl
            checkBoxRefineFilmBlur.Size = new System.Drawing.Size(76, 20);
            checkBoxRefineFilmBlur.TabIndex = 410;
            checkBoxRefineFilmBlur.Text = "Film blur";
            checkBoxRefineFilmBlur.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Checked = true;
            checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox6.Enabled = false;
            checkBox6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBox6.Location = new System.Drawing.Point(145, 180);
            checkBox6.Margin = new System.Windows.Forms.Padding(4);
            checkBox6.Name = "checkBox6";
            toolTip.SetToolTip(checkBox6, resources.GetString("checkBox6.ToolTip")); // 260531Cl
            checkBox6.Size = new System.Drawing.Size(174, 20);
            checkBox6.TabIndex = 406;
            checkBox6.Text = "Automatically save setting";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutomaticallyChangeParameter
            // 
            checkBoxAutomaticallyChangeParameter.AutoSize = true;
            checkBoxAutomaticallyChangeParameter.Checked = true;
            checkBoxAutomaticallyChangeParameter.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxAutomaticallyChangeParameter.Enabled = false;
            checkBoxAutomaticallyChangeParameter.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            checkBoxAutomaticallyChangeParameter.Location = new System.Drawing.Point(146, 146);
            checkBoxAutomaticallyChangeParameter.Margin = new System.Windows.Forms.Padding(4);
            checkBoxAutomaticallyChangeParameter.Name = "checkBoxAutomaticallyChangeParameter";
            toolTip.SetToolTip(checkBoxAutomaticallyChangeParameter, resources.GetString("checkBoxAutomaticallyChangeParameter.ToolTip")); // 260531Cl
            checkBoxAutomaticallyChangeParameter.Size = new System.Drawing.Size(211, 20);
            checkBoxAutomaticallyChangeParameter.TabIndex = 405;
            checkBoxAutomaticallyChangeParameter.Text = "Automatically change parameter";
            checkBoxAutomaticallyChangeParameter.UseVisualStyleBackColor = true;
            //
            // numericBoxChangeParameterThreshold
            //
            // 260522Cl 変更: NumericUpDown → NumericBox (Increment→UpDown_Increment, decimal→double, Font(Tahoma)はテーマ任せに撤去, ShowUpDown=true で UpDown 表示を維持)
            numericBoxChangeParameterThreshold.DecimalPlaces = 2;
            numericBoxChangeParameterThreshold.Location = new System.Drawing.Point(363, 146);
            numericBoxChangeParameterThreshold.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            numericBoxChangeParameterThreshold.Maximum = 1D;
            numericBoxChangeParameterThreshold.Name = "numericBoxChangeParameterThreshold";
            toolTip.SetToolTip(numericBoxChangeParameterThreshold, resources.GetString("numericBoxChangeParameterThreshold.ToolTip")); // 260531Cl
            numericBoxChangeParameterThreshold.ShowUpDown = true;
            numericBoxChangeParameterThreshold.Size = new System.Drawing.Size(52, 22);
            numericBoxChangeParameterThreshold.TabIndex = 404;
            numericBoxChangeParameterThreshold.ThousandsSeparator = true;
            numericBoxChangeParameterThreshold.UpDown_Increment = 0.05D;
            numericBoxChangeParameterThreshold.Value = 0.6D;
            // 
            // tabPage6
            // 
            tabPage6.Controls.Add(textBox1);
            tabPage6.Controls.Add(graphControlResidual);
            tabPage6.Location = new System.Drawing.Point(4, 25);
            tabPage6.Margin = new System.Windows.Forms.Padding(4);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new System.Windows.Forms.Padding(4);
            tabPage6.Size = new System.Drawing.Size(514, 210);
            tabPage6.TabIndex = 1;
            tabPage6.Text = "Refinement results"; // 260521Cl Phase7: Refinment→Refinement typo
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            textBox1.Location = new System.Drawing.Point(3, 4);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            toolTip.SetToolTip(textBox1, resources.GetString("textBox1.ToolTip")); // 260531Cl
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(174, 190);
            textBox1.TabIndex = 164;
            // 
            // graphControlResidual
            // 
            graphControlResidual.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            graphControlResidual.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControlResidual.GraphTitle = "";
            graphControlResidual.Interpolation = false;
            graphControlResidual.Location = new System.Drawing.Point(184, 4);
            graphControlResidual.Margin = new System.Windows.Forms.Padding(4);
            graphControlResidual.Name = "graphControlResidual";
            graphControlResidual.Size = new System.Drawing.Size(322, 189);
            graphControlResidual.Smoothing = false;
            graphControlResidual.TabIndex = 163;
            graphControlResidual.UpperPanelFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            graphControlResidual.MousePositionVisible = false;
            // 
            // tabPage8
            // 
            tabPage8.Controls.Add(buttonSearchUnrelatedOrientations);
            tabPage8.Location = new System.Drawing.Point(4, 25);
            tabPage8.Margin = new System.Windows.Forms.Padding(4);
            tabPage8.Name = "tabPage8";
            tabPage8.Padding = new System.Windows.Forms.Padding(4);
            tabPage8.Size = new System.Drawing.Size(514, 210);
            tabPage8.TabIndex = 2;
            tabPage8.Text = "Debug";
            tabPage8.UseVisualStyleBackColor = true;
            // 
            // buttonSearchUnrelatedOrientations
            // 
            buttonSearchUnrelatedOrientations.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            buttonSearchUnrelatedOrientations.Location = new System.Drawing.Point(5, 32);
            buttonSearchUnrelatedOrientations.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            buttonSearchUnrelatedOrientations.Name = "buttonSearchUnrelatedOrientations";
            toolTip.SetToolTip(buttonSearchUnrelatedOrientations, resources.GetString("buttonSearchUnrelatedOrientations.ToolTip")); // 260531Cl
            buttonSearchUnrelatedOrientations.Size = new System.Drawing.Size(281, 35);
            buttonSearchUnrelatedOrientations.TabIndex = 68;
            buttonSearchUnrelatedOrientations.Text = "Search unrelated orientation";
            buttonSearchUnrelatedOrientations.BackColor = System.Drawing.Color.SteelBlue; // 260520Cl 追加: 主要アクション(検索)を水色に統一
            buttonSearchUnrelatedOrientations.ForeColor = System.Drawing.Color.White; // 260520Cl 追加
            buttonSearchUnrelatedOrientations.UseVisualStyleBackColor = false; // 260520Cl 変更: BackColor有効化のため
            buttonSearchUnrelatedOrientations.Click += new System.EventHandler(buttonSearchUnrelatedOrientations_Click);
            // 
            // groupBoxGonioScan
            // 
            groupBoxGonioScan.Controls.Add(checkBoxYusaGonioScan);
            groupBoxGonioScan.Controls.Add(numericBoxRxSpeed);
            groupBoxGonioScan.Controls.Add(checkBoxYusaGonio_ValidRx);
            groupBoxGonioScan.Controls.Add(label52);
            groupBoxGonioScan.Controls.Add(numericBoxYusaGonioRySpeed);
            groupBoxGonioScan.Controls.Add(label51);
            groupBoxGonioScan.Controls.Add(numericBoxYusaGonioRzSpeed);
            groupBoxGonioScan.Controls.Add(label53);
            groupBoxGonioScan.Controls.Add(label50);
            groupBoxGonioScan.Controls.Add(radioButtonZigzagScan);
            groupBoxGonioScan.Controls.Add(label54);
            groupBoxGonioScan.Controls.Add(label49);
            groupBoxGonioScan.Controls.Add(numericBoxYusaGonioRyStep);
            groupBoxGonioScan.Controls.Add(label55);
            groupBoxGonioScan.Controls.Add(label48);
            groupBoxGonioScan.Controls.Add(numericBoxYusaGonioRyOscillation);
            groupBoxGonioScan.Controls.Add(label56);
            groupBoxGonioScan.Controls.Add(label60);
            groupBoxGonioScan.Controls.Add(label47);
            groupBoxGonioScan.Controls.Add(numericBoxYusaGonioRzOscillation);
            groupBoxGonioScan.Controls.Add(label57);
            groupBoxGonioScan.Controls.Add(label59);
            groupBoxGonioScan.Controls.Add(checkBox2);
            groupBoxGonioScan.Controls.Add(checkBox3);
            groupBoxGonioScan.Controls.Add(label58);
            groupBoxGonioScan.Location = new System.Drawing.Point(6, 1026);
            groupBoxGonioScan.Margin = new System.Windows.Forms.Padding(4);
            groupBoxGonioScan.Name = "groupBoxGonioScan";
            groupBoxGonioScan.Padding = new System.Windows.Forms.Padding(4);
            groupBoxGonioScan.Size = new System.Drawing.Size(312, 182);
            groupBoxGonioScan.TabIndex = 1001;
            groupBoxGonioScan.TabStop = false;
            groupBoxGonioScan.Text = "groupBoxGonioScan";
            // 
            // checkBoxYusaGonioScan
            // 
            checkBoxYusaGonioScan.AutoSize = true;
            checkBoxYusaGonioScan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxYusaGonioScan.Location = new System.Drawing.Point(8, 25);
            checkBoxYusaGonioScan.Margin = new System.Windows.Forms.Padding(4);
            checkBoxYusaGonioScan.Name = "checkBoxYusaGonioScan";
            toolTip.SetToolTip(checkBoxYusaGonioScan, resources.GetString("checkBoxYusaGonioScan.ToolTip")); // 260531Cl
            checkBoxYusaGonioScan.Size = new System.Drawing.Size(142, 20);
            checkBoxYusaGonioScan.TabIndex = 101;
            checkBoxYusaGonioScan.Text = "Use YusaGonio Scan";
            checkBoxYusaGonioScan.UseVisualStyleBackColor = true;
            // 
            // numericBoxRxSpeed
            // 
            numericBoxRxSpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRxSpeed.Location = new System.Drawing.Point(126, 94);
            numericBoxRxSpeed.Margin = new System.Windows.Forms.Padding(1);
            numericBoxRxSpeed.MaximumSize = new System.Drawing.Size(1000, 24);
            numericBoxRxSpeed.MinimumSize = new System.Drawing.Size(1, 22);
            numericBoxRxSpeed.Name = "numericBoxRxSpeed";
            toolTip.SetToolTip(numericBoxRxSpeed, resources.GetString("numericBoxRxSpeed.ToolTip")); // 260531Cl
            numericBoxRxSpeed.Padding = new System.Windows.Forms.Padding(1);
            numericBoxRxSpeed.RadianValue = 0.31415926535897931D;
            numericBoxRxSpeed.Size = new System.Drawing.Size(61, 24);
            numericBoxRxSpeed.SkipEventDuringInput = false;
            numericBoxRxSpeed.SmartIncrement = true;
            numericBoxRxSpeed.TabIndex = 103;
            numericBoxRxSpeed.ValueFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxRxSpeed.ThousandsSeparator = true;
            numericBoxRxSpeed.Value = 18D;
            // 
            // checkBoxYusaGonio_ValidRx
            // 
            checkBoxYusaGonio_ValidRx.AutoSize = true;
            checkBoxYusaGonio_ValidRx.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBoxYusaGonio_ValidRx.Location = new System.Drawing.Point(28, 70);
            checkBoxYusaGonio_ValidRx.Margin = new System.Windows.Forms.Padding(4);
            checkBoxYusaGonio_ValidRx.Name = "checkBoxYusaGonio_ValidRx";
            toolTip.SetToolTip(checkBoxYusaGonio_ValidRx, resources.GetString("checkBoxYusaGonio_ValidRx.ToolTip")); // 260531Cl
            checkBoxYusaGonio_ValidRx.Size = new System.Drawing.Size(63, 20);
            checkBoxYusaGonio_ValidRx.TabIndex = 102;
            checkBoxYusaGonio_ValidRx.Text = "Rx (φ)";
            checkBoxYusaGonio_ValidRx.UseVisualStyleBackColor = true;
            // 
            // label52
            // 
            label52.AutoSize = true;
            label52.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label52.Location = new System.Drawing.Point(20, 99);
            label52.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label52.Name = "label52";
            toolTip.SetToolTip(label52, resources.GetString("label52.ToolTip")); // 260531Cl
            label52.Size = new System.Drawing.Size(79, 16);
            label52.TabIndex = 14;
            label52.Text = "motor speed";
            // 
            // numericBoxYusaGonioRySpeed
            // 
            numericBoxYusaGonioRySpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxYusaGonioRySpeed.Location = new System.Drawing.Point(149, 276);
            numericBoxYusaGonioRySpeed.Margin = new System.Windows.Forms.Padding(1);
            numericBoxYusaGonioRySpeed.MaximumSize = new System.Drawing.Size(1000, 24);
            numericBoxYusaGonioRySpeed.MinimumSize = new System.Drawing.Size(1, 22);
            numericBoxYusaGonioRySpeed.Name = "numericBoxYusaGonioRySpeed";
            toolTip.SetToolTip(numericBoxYusaGonioRySpeed, resources.GetString("numericBoxYusaGonioRySpeed.ToolTip")); // 260531Cl
            numericBoxYusaGonioRySpeed.Padding = new System.Windows.Forms.Padding(1);
            numericBoxYusaGonioRySpeed.RadianValue = 0.017453292519943295D;
            numericBoxYusaGonioRySpeed.Size = new System.Drawing.Size(61, 24);
            numericBoxYusaGonioRySpeed.SkipEventDuringInput = false;
            numericBoxYusaGonioRySpeed.SmartIncrement = true;
            numericBoxYusaGonioRySpeed.TabIndex = 109;
            numericBoxYusaGonioRySpeed.ValueFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxYusaGonioRySpeed.ThousandsSeparator = true;
            numericBoxYusaGonioRySpeed.Value = 1D;
            // 
            // label51
            // 
            label51.AutoSize = true;
            label51.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label51.Location = new System.Drawing.Point(44, 226);
            label51.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label51.Name = "label51";
            toolTip.SetToolTip(label51, resources.GetString("label51.ToolTip")); // 260531Cl
            label51.Size = new System.Drawing.Size(63, 16);
            label51.TabIndex = 16;
            label51.Text = "oscillation";
            // 
            // numericBoxYusaGonioRzSpeed
            // 
            numericBoxYusaGonioRzSpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxYusaGonioRzSpeed.Location = new System.Drawing.Point(149, 196);
            numericBoxYusaGonioRzSpeed.Margin = new System.Windows.Forms.Padding(1);
            numericBoxYusaGonioRzSpeed.MaximumSize = new System.Drawing.Size(1000, 24);
            numericBoxYusaGonioRzSpeed.MinimumSize = new System.Drawing.Size(1, 22);
            numericBoxYusaGonioRzSpeed.Name = "numericBoxYusaGonioRzSpeed";
            toolTip.SetToolTip(numericBoxYusaGonioRzSpeed, resources.GetString("numericBoxYusaGonioRzSpeed.ToolTip")); // 260531Cl
            numericBoxYusaGonioRzSpeed.Padding = new System.Windows.Forms.Padding(1);
            numericBoxYusaGonioRzSpeed.RadianValue = 0.034906585039886591D;
            numericBoxYusaGonioRzSpeed.Size = new System.Drawing.Size(61, 24);
            numericBoxYusaGonioRzSpeed.SkipEventDuringInput = false;
            numericBoxYusaGonioRzSpeed.SmartIncrement = true;
            numericBoxYusaGonioRzSpeed.TabIndex = 106;
            numericBoxYusaGonioRzSpeed.ValueFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxYusaGonioRzSpeed.ThousandsSeparator = true;
            numericBoxYusaGonioRzSpeed.Value = 2D;
            // 
            // label53
            // 
            label53.AutoSize = true;
            label53.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label53.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label53.Location = new System.Drawing.Point(214, 341);
            label53.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label53.Name = "label53";
            toolTip.SetToolTip(label53, resources.GetString("label53.ToolTip")); // 260531Cl
            label53.Size = new System.Drawing.Size(29, 13);
            label53.TabIndex = 15;
            label53.Text = "deg.";
            // 
            // label50
            // 
            label50.AutoSize = true;
            label50.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label50.Location = new System.Drawing.Point(44, 311);
            label50.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label50.Name = "label50";
            toolTip.SetToolTip(label50, resources.GetString("label50.ToolTip")); // 260531Cl
            label50.Size = new System.Drawing.Size(63, 16);
            label50.TabIndex = 18;
            label50.Text = "oscillation";
            // 
            // radioButtonZigzagScan
            // 
            radioButtonZigzagScan.Checked = true;
            radioButtonZigzagScan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            radioButtonZigzagScan.Location = new System.Drawing.Point(28, 122);
            radioButtonZigzagScan.Margin = new System.Windows.Forms.Padding(4);
            radioButtonZigzagScan.Name = "radioButtonZigzagScan";
            toolTip.SetToolTip(radioButtonZigzagScan, resources.GetString("radioButtonZigzagScan.ToolTip")); // 260531Cl
            radioButtonZigzagScan.Size = new System.Drawing.Size(205, 52);
            radioButtonZigzagScan.TabIndex = 104;
            radioButtonZigzagScan.TabStop = true;
            radioButtonZigzagScan.Text = "+θ > +ω > -θ > +ω .... (Zigzag scan)";
            radioButtonZigzagScan.UseVisualStyleBackColor = true;
            // 
            // label54
            // 
            label54.AutoSize = true;
            label54.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label54.Location = new System.Drawing.Point(42, 281);
            label54.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label54.Name = "label54";
            toolTip.SetToolTip(label54, resources.GetString("label54.ToolTip")); // 260531Cl
            label54.Size = new System.Drawing.Size(79, 16);
            label54.TabIndex = 19;
            label54.Text = "motor speed";
            // 
            // label49
            // 
            label49.AutoSize = true;
            label49.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label49.Location = new System.Drawing.Point(60, 342);
            label49.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label49.Name = "label49";
            toolTip.SetToolTip(label49, resources.GetString("label49.ToolTip")); // 260531Cl
            label49.Size = new System.Drawing.Size(31, 16);
            label49.TabIndex = 25;
            label49.Text = "step";
            // 
            // numericBoxYusaGonioRyStep
            // 
            numericBoxYusaGonioRyStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxYusaGonioRyStep.Location = new System.Drawing.Point(149, 336);
            numericBoxYusaGonioRyStep.Margin = new System.Windows.Forms.Padding(1);
            numericBoxYusaGonioRyStep.MaximumSize = new System.Drawing.Size(1000, 24);
            numericBoxYusaGonioRyStep.MinimumSize = new System.Drawing.Size(1, 22);
            numericBoxYusaGonioRyStep.Name = "numericBoxYusaGonioRyStep";
            toolTip.SetToolTip(numericBoxYusaGonioRyStep, resources.GetString("numericBoxYusaGonioRyStep.ToolTip")); // 260531Cl
            numericBoxYusaGonioRyStep.Padding = new System.Windows.Forms.Padding(1);
            numericBoxYusaGonioRyStep.RadianValue = 0.0034906585039886592D;
            numericBoxYusaGonioRyStep.Size = new System.Drawing.Size(61, 24);
            numericBoxYusaGonioRyStep.SkipEventDuringInput = false;
            numericBoxYusaGonioRyStep.SmartIncrement = true;
            numericBoxYusaGonioRyStep.TabIndex = 111;
            numericBoxYusaGonioRyStep.ValueFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxYusaGonioRyStep.ThousandsSeparator = true;
            numericBoxYusaGonioRyStep.Value = 0.2D;
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label55.Location = new System.Drawing.Point(214, 310);
            label55.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label55.Name = "label55";
            toolTip.SetToolTip(label55, resources.GetString("label55.ToolTip")); // 260531Cl
            label55.Size = new System.Drawing.Size(29, 13);
            label55.TabIndex = 23;
            label55.Text = "deg.";
            // 
            // label48
            // 
            label48.AutoSize = true;
            label48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label48.Location = new System.Drawing.Point(128, 231);
            label48.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label48.Name = "label48";
            toolTip.SetToolTip(label48, resources.GetString("label48.ToolTip")); // 260531Cl
            label48.Size = new System.Drawing.Size(15, 16);
            label48.TabIndex = 27;
            label48.Text = "±";
            // 
            // numericBoxYusaGonioRyOscillation
            // 
            numericBoxYusaGonioRyOscillation.BackColor = System.Drawing.SystemColors.Control;
            numericBoxYusaGonioRyOscillation.Location = new System.Drawing.Point(149, 306);
            numericBoxYusaGonioRyOscillation.Margin = new System.Windows.Forms.Padding(1);
            numericBoxYusaGonioRyOscillation.MaximumSize = new System.Drawing.Size(1000, 24);
            numericBoxYusaGonioRyOscillation.MinimumSize = new System.Drawing.Size(1, 22);
            numericBoxYusaGonioRyOscillation.Name = "numericBoxYusaGonioRyOscillation";
            toolTip.SetToolTip(numericBoxYusaGonioRyOscillation, resources.GetString("numericBoxYusaGonioRyOscillation.ToolTip")); // 260531Cl
            numericBoxYusaGonioRyOscillation.Padding = new System.Windows.Forms.Padding(1);
            numericBoxYusaGonioRyOscillation.RadianValue = 0.069813170079773182D;
            numericBoxYusaGonioRyOscillation.Size = new System.Drawing.Size(61, 24);
            numericBoxYusaGonioRyOscillation.SkipEventDuringInput = false;
            numericBoxYusaGonioRyOscillation.SmartIncrement = true;
            numericBoxYusaGonioRyOscillation.TabIndex = 110;
            numericBoxYusaGonioRyOscillation.ValueFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxYusaGonioRyOscillation.ThousandsSeparator = true;
            numericBoxYusaGonioRyOscillation.Value = 4D;
            // 
            // label56
            // 
            label56.AutoSize = true;
            label56.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label56.Location = new System.Drawing.Point(42, 196);
            label56.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label56.Name = "label56";
            toolTip.SetToolTip(label56, resources.GetString("label56.ToolTip")); // 260531Cl
            label56.Size = new System.Drawing.Size(79, 16);
            label56.TabIndex = 24;
            label56.Text = "motor speed";
            // 
            // label60
            // 
            label60.AutoSize = true;
            label60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label60.Location = new System.Drawing.Point(192, 99);
            label60.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label60.Name = "label60";
            toolTip.SetToolTip(label60, resources.GetString("label60.ToolTip")); // 260531Cl
            label60.Size = new System.Drawing.Size(68, 16);
            label60.TabIndex = 1000;
            label60.Text = "°/s"; // 260520Cl: unit unification (deg. / sec. → °/s)
            // 
            // label47
            // 
            label47.AutoSize = true;
            label47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label47.Location = new System.Drawing.Point(128, 311);
            label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label47.Name = "label47";
            toolTip.SetToolTip(label47, resources.GetString("label47.ToolTip")); // 260531Cl
            label47.Size = new System.Drawing.Size(15, 16);
            label47.TabIndex = 26;
            label47.Text = "±";
            // 
            // numericBoxYusaGonioRzOscillation
            // 
            numericBoxYusaGonioRzOscillation.BackColor = System.Drawing.SystemColors.Control;
            numericBoxYusaGonioRzOscillation.Location = new System.Drawing.Point(149, 226);
            numericBoxYusaGonioRzOscillation.Margin = new System.Windows.Forms.Padding(1);
            numericBoxYusaGonioRzOscillation.MaximumSize = new System.Drawing.Size(1000, 24);
            numericBoxYusaGonioRzOscillation.MinimumSize = new System.Drawing.Size(1, 22);
            numericBoxYusaGonioRzOscillation.Name = "numericBoxYusaGonioRzOscillation";
            toolTip.SetToolTip(numericBoxYusaGonioRzOscillation, resources.GetString("numericBoxYusaGonioRzOscillation.ToolTip")); // 260531Cl
            numericBoxYusaGonioRzOscillation.Padding = new System.Windows.Forms.Padding(1);
            numericBoxYusaGonioRzOscillation.RadianValue = 0.069813170079773182D;
            numericBoxYusaGonioRzOscillation.Size = new System.Drawing.Size(61, 24);
            numericBoxYusaGonioRzOscillation.SkipEventDuringInput = false;
            numericBoxYusaGonioRzOscillation.SmartIncrement = true;
            numericBoxYusaGonioRzOscillation.TabIndex = 107;
            numericBoxYusaGonioRzOscillation.ValueFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBoxYusaGonioRzOscillation.ThousandsSeparator = true;
            numericBoxYusaGonioRzOscillation.Value = 4D;
            // 
            // label57
            // 
            label57.AutoSize = true;
            label57.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label57.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label57.Location = new System.Drawing.Point(214, 230);
            label57.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label57.Name = "label57";
            toolTip.SetToolTip(label57, resources.GetString("label57.ToolTip")); // 260531Cl
            label57.Size = new System.Drawing.Size(29, 13);
            label57.TabIndex = 1000;
            label57.Text = "°"; // 260520Cl: unit unification (deg. → °)
            // 
            // label59
            // 
            label59.AutoSize = true;
            label59.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label59.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label59.Location = new System.Drawing.Point(214, 201);
            label59.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label59.Name = "label59";
            toolTip.SetToolTip(label59, resources.GetString("label59.ToolTip")); // 260531Cl
            label59.Size = new System.Drawing.Size(59, 13);
            label59.TabIndex = 1000;
            label59.Text = "°/s"; // 260520Cl: unit unification (deg. / sec. → °/s)
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBox2.Location = new System.Drawing.Point(44, 171);
            checkBox2.Margin = new System.Windows.Forms.Padding(4);
            checkBox2.Name = "checkBox2";
            toolTip.SetToolTip(checkBox2, resources.GetString("checkBox2.ToolTip")); // 260531Cl
            checkBox2.Size = new System.Drawing.Size(61, 20);
            checkBox2.TabIndex = 105;
            checkBox2.Text = "Rz (θ)";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            checkBox3.Location = new System.Drawing.Point(44, 255);
            checkBox3.Margin = new System.Windows.Forms.Padding(4);
            checkBox3.Name = "checkBox3";
            toolTip.SetToolTip(checkBox3, resources.GetString("checkBox3.ToolTip")); // 260531Cl
            checkBox3.Size = new System.Drawing.Size(63, 20);
            checkBox3.TabIndex = 108;
            checkBox3.Text = "Ry (ω)";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // label58
            // 
            label58.AutoSize = true;
            label58.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label58.Location = new System.Drawing.Point(214, 281);
            label58.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label58.Name = "label58";
            toolTip.SetToolTip(label58, resources.GetString("label58.ToolTip")); // 260531Cl
            label58.Size = new System.Drawing.Size(59, 13);
            label58.TabIndex = 20;
            label58.Text = "°/s"; // 260520Cl: unit unification (deg. / sec. → °/s)
            // 
            // groupBoxPatterns
            // 
            groupBoxPatterns.Controls.Add(groupBoxAppearance);
            groupBoxPatterns.Controls.Add(listBoxReferrence);
            groupBoxPatterns.Controls.Add(buttonAddRefferencePattern);
            groupBoxPatterns.Controls.Add(buttonRemoveReferrencePattern);
            groupBoxPatterns.Controls.Add(tabControl1);
            groupBoxPatterns.Dock = System.Windows.Forms.DockStyle.Fill;
            groupBoxPatterns.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            groupBoxPatterns.Location = new System.Drawing.Point(0, 0);
            groupBoxPatterns.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            groupBoxPatterns.Name = "groupBoxPatterns";
            groupBoxPatterns.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            groupBoxPatterns.Size = new System.Drawing.Size(1001, 911);
            groupBoxPatterns.TabIndex = 129;
            groupBoxPatterns.TabStop = false;
            groupBoxPatterns.Text = "Simulated / refference patterns";
            // 
            // groupBoxAppearance
            // 
            groupBoxAppearance.Controls.Add(comboBoxScale2);
            groupBoxAppearance.Controls.Add(label26);
            groupBoxAppearance.Controls.Add(comboBoxGradient);
            groupBoxAppearance.Controls.Add(label30);
            groupBoxAppearance.Controls.Add(comboBoxScale1);
            groupBoxAppearance.Controls.Add(label28);
            groupBoxAppearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            groupBoxAppearance.Location = new System.Drawing.Point(305, 28);
            groupBoxAppearance.Margin = new System.Windows.Forms.Padding(4);
            groupBoxAppearance.Name = "groupBoxAppearance";
            groupBoxAppearance.Padding = new System.Windows.Forms.Padding(4);
            groupBoxAppearance.Size = new System.Drawing.Size(650, 60);
            groupBoxAppearance.TabIndex = 130;
            groupBoxAppearance.TabStop = false;
            groupBoxAppearance.Text = "Appearance";
            // 
            // comboBoxScale2
            // 
            comboBoxScale2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxScale2.FormattingEnabled = true;
            comboBoxScale2.Items.AddRange(new object[] {
            "Gray scale",
            "Cold-Warm scale"});
            comboBoxScale2.Location = new System.Drawing.Point(498, 22);
            comboBoxScale2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            comboBoxScale2.Name = "comboBoxScale2";
            toolTip.SetToolTip(comboBoxScale2, resources.GetString("comboBoxScale2.ToolTip")); // 260531Cl
            comboBoxScale2.Size = new System.Drawing.Size(134, 22);
            comboBoxScale2.TabIndex = 303;
            comboBoxScale2.SelectedIndexChanged += new System.EventHandler(comboBoxScale_SelectedIndexChanged);
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label26.Location = new System.Drawing.Point(432, 26);
            label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label26.Name = "label26";
            toolTip.SetToolTip(label26, resources.GetString("label26.ToolTip")); // 260531Cl
            label26.Size = new System.Drawing.Size(46, 14);
            label26.TabIndex = 152;
            label26.Text = "Scale 2";
            // 
            // comboBoxGradient
            // 
            comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxGradient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxGradient.FormattingEnabled = true;
            comboBoxGradient.Items.AddRange(new object[] {
            "Positive",
            "Negative"});
            comboBoxGradient.Location = new System.Drawing.Point(80, 22);
            comboBoxGradient.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            comboBoxGradient.Name = "comboBoxGradient";
            toolTip.SetToolTip(comboBoxGradient, resources.GetString("comboBoxGradient.ToolTip")); // 260531Cl
            comboBoxGradient.Size = new System.Drawing.Size(135, 22);
            comboBoxGradient.TabIndex = 301;
            comboBoxGradient.SelectedIndexChanged += new System.EventHandler(comboBoxScale_SelectedIndexChanged);
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label30.Location = new System.Drawing.Point(8, 26);
            label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label30.Name = "label30";
            toolTip.SetToolTip(label30, resources.GetString("label30.ToolTip")); // 260531Cl
            label30.Size = new System.Drawing.Size(53, 14);
            label30.TabIndex = 149;
            label30.Text = "Gradient";
            // 
            // comboBoxScale1
            // 
            comboBoxScale1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            comboBoxScale1.FormattingEnabled = true;
            comboBoxScale1.Items.AddRange(new object[] {
            "Log Scale",
            "Linear Scale"}); // 260521Cl Phase7: Liner→Linear typo
            comboBoxScale1.Location = new System.Drawing.Point(289, 22);
            comboBoxScale1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            comboBoxScale1.Name = "comboBoxScale1";
            toolTip.SetToolTip(comboBoxScale1, resources.GetString("comboBoxScale1.ToolTip")); // 260531Cl
            comboBoxScale1.Size = new System.Drawing.Size(135, 22);
            comboBoxScale1.TabIndex = 302;
            comboBoxScale1.SelectedIndexChanged += new System.EventHandler(comboBoxScale_SelectedIndexChanged);
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label28.Location = new System.Drawing.Point(224, 26);
            label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label28.Name = "label28";
            toolTip.SetToolTip(label28, resources.GetString("label28.ToolTip")); // 260531Cl
            label28.Size = new System.Drawing.Size(46, 14);
            label28.TabIndex = 151;
            label28.Text = "Scale 1";
            // 
            // listBoxReferrence
            // 
            listBoxReferrence.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            listBoxReferrence.FormattingEnabled = true;
            listBoxReferrence.ItemHeight = 16;
            listBoxReferrence.Location = new System.Drawing.Point(10, 29);
            listBoxReferrence.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            listBoxReferrence.Name = "listBoxReferrence";
            toolTip.SetToolTip(listBoxReferrence, resources.GetString("listBoxReferrence.ToolTip")); // 260531Cl
            listBoxReferrence.Size = new System.Drawing.Size(190, 36);
            listBoxReferrence.TabIndex = 162;
            listBoxReferrence.SelectedIndexChanged += new System.EventHandler(listBoxReferrence_SelectedIndexChanged);
            // 
            // buttonAddRefferencePattern
            // 
            buttonAddRefferencePattern.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonAddRefferencePattern.Location = new System.Drawing.Point(204, 28);
            buttonAddRefferencePattern.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            buttonAddRefferencePattern.Name = "buttonAddRefferencePattern";
            toolTip.SetToolTip(buttonAddRefferencePattern, resources.GetString("buttonAddRefferencePattern.ToolTip")); // 260531Cl
            buttonAddRefferencePattern.Size = new System.Drawing.Size(94, 34);
            buttonAddRefferencePattern.TabIndex = 142;
            buttonAddRefferencePattern.Text = "Add";
            buttonAddRefferencePattern.UseVisualStyleBackColor = true;
            buttonAddRefferencePattern.Click += new System.EventHandler(buttonAddRefferencePattern_Click);
            // 
            // buttonRemoveReferrencePattern
            // 
            buttonRemoveReferrencePattern.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            buttonRemoveReferrencePattern.Location = new System.Drawing.Point(204, 61);
            buttonRemoveReferrencePattern.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            buttonRemoveReferrencePattern.Name = "buttonRemoveReferrencePattern";
            toolTip.SetToolTip(buttonRemoveReferrencePattern, resources.GetString("buttonRemoveReferrencePattern.ToolTip")); // 260531Cl
            buttonRemoveReferrencePattern.Size = new System.Drawing.Size(94, 34);
            buttonRemoveReferrencePattern.TabIndex = 142;
            buttonRemoveReferrencePattern.Text = "Remove";
            buttonRemoveReferrencePattern.UseVisualStyleBackColor = true;
            buttonRemoveReferrencePattern.Click += new System.EventHandler(buttonRemoveReferrencePattern_Click);
            // 
            // tabControl1
            // 
            tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            tabControl1.Location = new System.Drawing.Point(9, 103);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(987, 805);
            tabControl1.TabIndex = 162;
            tabControl1.SelectedIndexChanged += new System.EventHandler(tabControl1_SelectedIndexChanged);
            tabControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(tabControl1_MouseDoubleClick);
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(diffractionPatternControlSimulation);
            tabPage1.Location = new System.Drawing.Point(4, 25);
            tabPage1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            tabPage1.Size = new System.Drawing.Size(979, 776);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Simulated Pattern";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // diffractionPatternControlSimulation
            // 
            diffractionPatternControlSimulation.BackColor = System.Drawing.SystemColors.Control;
            diffractionPatternControlSimulation.Cameralength = 450D;
            // this.diffractionPatternControlSimulation.Center = ((Crystallography.PointD)(resources.GetObject("diffractionPatternControlSimulation.Center"))); // (260322Ch) Hidden 指定プロパティは Designer 非依存に戻す
            diffractionPatternControlSimulation.Convergence = 0.0008726646259971648D;
            diffractionPatternControlSimulation.ConvergenceDegree = 0.05D;
            diffractionPatternControlSimulation.Crystals = null;
            // this.diffractionPatternControlSimulation.DetectorProperty = ((Crystallography.AreaDetector)(resources.GetObject("diffractionPatternControlSimulation.DetectorProperty"))); // (260322Ch) Hidden 指定プロパティは Designer 非依存に戻す
            diffractionPatternControlSimulation.Dock = System.Windows.Forms.DockStyle.Fill;
            diffractionPatternControlSimulation.FilmBlur = 200D;
            diffractionPatternControlSimulation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            diffractionPatternControlSimulation.ImageHeight = 900;
            diffractionPatternControlSimulation.ImageWidth = 900;
            diffractionPatternControlSimulation.IsReferrenceImage = false;
            diffractionPatternControlSimulation.Location = new System.Drawing.Point(4, 2);
            diffractionPatternControlSimulation.Margin = new System.Windows.Forms.Padding(4);
            diffractionPatternControlSimulation.Monochromaticity = 1E-05D;
            diffractionPatternControlSimulation.Name = "diffractionPatternControlSimulation";
            diffractionPatternControlSimulation.Phi = 0D;
            diffractionPatternControlSimulation.Resolution = 0.3D;
            diffractionPatternControlSimulation.SimulationCheck = false;
            diffractionPatternControlSimulation.Size = new System.Drawing.Size(971, 772);
            diffractionPatternControlSimulation.TabIndex = 0;
            diffractionPatternControlSimulation.Tau = 0D;
            diffractionPatternControlSimulation.Wavelength = 0.041328040768899996D;
            // this.diffractionPatternControlSimulation.WaveProperty = ((Crystallography.WaveProperty)(resources.GetObject("diffractionPatternControlSimulation.WaveProperty"))); // (260322Ch) Hidden 指定プロパティは Designer 非依存に戻す
            diffractionPatternControlSimulation.WaveSource = Crystallography.WaveSource.Xray;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripStatusLabel,
            toolStripProgressBar,
            toolStripStatusLabelProgress});
            statusStrip1.Location = new System.Drawing.Point(0, 911);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            statusStrip1.Size = new System.Drawing.Size(1538, 22);
            statusStrip1.TabIndex = 133;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new System.Drawing.Size(10, 17);
            toolStripStatusLabel.Text = " ";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new System.Drawing.Size(125, 16);
            // 
            // toolStripStatusLabelProgress
            // 
            toolStripStatusLabelProgress.Name = "toolStripStatusLabelProgress";
            toolStripStatusLabelProgress.Size = new System.Drawing.Size(13, 17);
            toolStripStatusLabelProgress.Text = "  ";
            // 
            // FormPolycrystallineDiffractionSimulator
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F); // 260329Cl 追加: None→Dpi, 96dpi基準に統一
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(1538, 933);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip1);
            Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            Name = "FormPolycrystallineDiffractionSimulator";
            Text = "Powder Diffraction Simulator";
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormPolycrystallineDiffractionSimulator_FormClosing);
            Load += new System.EventHandler(FormPolycrystallineDiffractionSimulator_Load);
            VisibleChanged += new System.EventHandler(FormPolycrystallineDiffractionSimulator_VisibleChanged);
            DragDrop += new System.Windows.Forms.DragEventHandler(FormPolycrystallineDiffractionSimulator_DragDrop);
            DragEnter += new System.Windows.Forms.DragEventHandler(FormPolycrystallineDiffractionSimulator_DragEnter);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(splitContainer1)).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBoxCrystalProperty.ResumeLayout(false);
            groupBoxCrystalProperty.PerformLayout();
            tabControlCrystals.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            groupBoxOrientationFitting.ResumeLayout(false);
            groupBoxOrientationFitting.PerformLayout();
            tabControl3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            groupBoxPreferredOrientation.ResumeLayout(false);
            groupBoxPreferredOrientation.PerformLayout();
            groupBoxFittingOptions.ResumeLayout(false);
            groupBoxFittingOptions.PerformLayout();
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            tabPage8.ResumeLayout(false);
            groupBoxGonioScan.ResumeLayout(false);
            groupBoxGonioScan.PerformLayout();
            groupBoxPatterns.ResumeLayout(false);
            groupBoxAppearance.ResumeLayout(false);
            groupBoxAppearance.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip; // 260531Cl
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
        private System.Windows.Forms.GroupBox groupBoxOrientationFitting;
        private System.Windows.Forms.GroupBox groupBoxPatterns;
        private System.Windows.Forms.ListBox listBoxReferrence;
        private System.Windows.Forms.Button buttonRemoveReferrencePattern;
        private DiffractionPatternControl diffractionPatternControlSimulation;
        private System.ComponentModel.BackgroundWorker backgroundWorkerMain;
        private Crystallography.Controls.GraphControl graphControlResidual;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonSaveCurrentSetting;
        private System.Windows.Forms.GroupBox groupBoxAppearance;
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
        private System.Windows.Forms.GroupBox groupBoxFittingOptions;
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
        private System.Windows.Forms.GroupBox groupBoxGonioScan;
        private Crystallography.Controls.CrystalControl crystalControl1;
        private System.Windows.Forms.Button buttonSimulateDebyeRing;
        private System.Windows.Forms.GroupBox groupBoxCrystalProperty;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProgress;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private Crystallography.Controls.NumericBox numericBoxChangeParameterThreshold;                                                                // 260522Cl 変更: NumericUpDown → NumericBox
        private System.Windows.Forms.TabControl tabControlCrystals;
        private System.Windows.Forms.TabPage tabPage2;
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
        private System.Windows.Forms.GroupBox groupBoxPreferredOrientation;
    }
}
