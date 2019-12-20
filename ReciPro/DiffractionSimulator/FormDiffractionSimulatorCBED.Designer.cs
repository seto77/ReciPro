namespace ReciPro
{
    partial class FormDiffractionSimulatorCBED
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiffractionSimulatorCBED));
            this.buttonExecute = new System.Windows.Forms.Button();
            this.checkBoxDrawGuideCircles = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDivisionNumber = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxSolver = new System.Windows.Forms.ComboBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxScale = new System.Windows.Forms.ComboBox();
            this.trackBarOutputThickness = new System.Windows.Forms.TrackBar();
            this.trackBarGamma = new System.Windows.Forms.TrackBar();
            this.trackBarIntensityBrightnessMax = new System.Windows.Forms.TrackBar();
            this.trackBarIntensityBrightnessMin = new System.Windows.Forms.TrackBar();
            this.textBoxThickness = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.numericBoxThread = new Crystallography.Controls.NumericBox();
            this.numericBoxThicknessStep = new Crystallography.Controls.NumericBox();
            this.numericBoxWholeThicknessStart = new Crystallography.Controls.NumericBox();
            this.numericBoxThicknessEnd = new Crystallography.Controls.NumericBox();
            this.numericBoxMaxNumOfG = new Crystallography.Controls.NumericBox();
            this.trackBarAdvancedAlphaMax = new Crystallography.Controls.TrackBarAdvanced();
            this.numericBoxDivision = new Crystallography.Controls.NumericBox();
            this.numericBoxImageSize = new Crystallography.Controls.NumericBox();
            this.comboBoxGradient = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOutputThickness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIntensityBrightnessMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIntensityBrightnessMin)).BeginInit();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExecute
            // 
            resources.ApplyResources(this.buttonExecute, "buttonExecute");
            this.buttonExecute.BackColor = System.Drawing.Color.LightSteelBlue;
            this.buttonExecute.ForeColor = System.Drawing.Color.RoyalBlue;
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.UseVisualStyleBackColor = false;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // checkBoxDrawGuideCircles
            // 
            resources.ApplyResources(this.checkBoxDrawGuideCircles, "checkBoxDrawGuideCircles");
            this.checkBoxDrawGuideCircles.Checked = true;
            this.checkBoxDrawGuideCircles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDrawGuideCircles.Name = "checkBoxDrawGuideCircles";
            this.checkBoxDrawGuideCircles.UseVisualStyleBackColor = true;
            this.checkBoxDrawGuideCircles.CheckedChanged += new System.EventHandler(this.CheckBoxDrawGuideCircles_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.SizingGrip = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // labelDivisionNumber
            // 
            resources.ApplyResources(this.labelDivisionNumber, "labelDivisionNumber");
            this.labelDivisionNumber.BackColor = System.Drawing.Color.Transparent;
            this.labelDivisionNumber.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelDivisionNumber.Name = "labelDivisionNumber";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelDivisionNumber);
            this.groupBox1.Controls.Add(this.comboBoxSolver);
            this.groupBox1.Controls.Add(this.numericBoxThread);
            this.groupBox1.Controls.Add(this.numericBoxThicknessStep);
            this.groupBox1.Controls.Add(this.numericBoxWholeThicknessStart);
            this.groupBox1.Controls.Add(this.numericBoxThicknessEnd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericBoxMaxNumOfG);
            this.groupBox1.Controls.Add(this.buttonStop);
            this.groupBox1.Controls.Add(this.buttonExecute);
            this.groupBox1.Controls.Add(this.checkBoxDrawGuideCircles);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.trackBarAdvancedAlphaMax);
            this.groupBox1.Controls.Add(this.numericBoxDivision);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // comboBoxSolver
            // 
            this.comboBoxSolver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxSolver, "comboBoxSolver");
            this.comboBoxSolver.FormattingEnabled = true;
            this.comboBoxSolver.Items.AddRange(new object[] {
            resources.GetString("comboBoxSolver.Items"),
            resources.GetString("comboBoxSolver.Items1"),
            resources.GetString("comboBoxSolver.Items2"),
            resources.GetString("comboBoxSolver.Items3")});
            this.comboBoxSolver.Name = "comboBoxSolver";
            this.comboBoxSolver.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSolver_SelectedIndexChanged);
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.IndianRed;
            this.buttonStop.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonStop, "buttonStop");
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.Controls.Add(this.label3);
            this.groupBoxOutput.Controls.Add(this.label11);
            this.groupBoxOutput.Controls.Add(this.comboBoxGradient);
            this.groupBoxOutput.Controls.Add(this.comboBoxScale);
            this.groupBoxOutput.Controls.Add(this.trackBarOutputThickness);
            this.groupBoxOutput.Controls.Add(this.trackBarGamma);
            this.groupBoxOutput.Controls.Add(this.trackBarIntensityBrightnessMax);
            this.groupBoxOutput.Controls.Add(this.trackBarIntensityBrightnessMin);
            this.groupBoxOutput.Controls.Add(this.textBoxThickness);
            this.groupBoxOutput.Controls.Add(this.label6);
            this.groupBoxOutput.Controls.Add(this.label4);
            this.groupBoxOutput.Controls.Add(this.label10);
            this.groupBoxOutput.Controls.Add(this.label8);
            this.groupBoxOutput.Controls.Add(this.label7);
            this.groupBoxOutput.Controls.Add(this.label5);
            this.groupBoxOutput.Controls.Add(this.numericBoxImageSize);
            resources.ApplyResources(this.groupBoxOutput, "groupBoxOutput");
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.TabStop = false;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // comboBoxScale
            // 
            this.comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxScale, "comboBoxScale");
            this.comboBoxScale.FormattingEnabled = true;
            this.comboBoxScale.Items.AddRange(new object[] {
            resources.GetString("comboBoxScale.Items"),
            resources.GetString("comboBoxScale.Items1")});
            this.comboBoxScale.Name = "comboBoxScale";
            this.comboBoxScale.SelectedIndexChanged += new System.EventHandler(this.trackBarIntensityBrightnessMax_ValueChanged);
            // 
            // trackBarOutputThickness
            // 
            resources.ApplyResources(this.trackBarOutputThickness, "trackBarOutputThickness");
            this.trackBarOutputThickness.LargeChange = 1;
            this.trackBarOutputThickness.Maximum = 9;
            this.trackBarOutputThickness.Name = "trackBarOutputThickness";
            this.trackBarOutputThickness.Scroll += new System.EventHandler(this.TrackBarOutputThickness_Scroll);
            // 
            // trackBarGamma
            // 
            resources.ApplyResources(this.trackBarGamma, "trackBarGamma");
            this.trackBarGamma.LargeChange = 10;
            this.trackBarGamma.Maximum = 1024;
            this.trackBarGamma.Name = "trackBarGamma";
            this.trackBarGamma.SmallChange = 10;
            this.trackBarGamma.TickFrequency = 10;
            this.trackBarGamma.ValueChanged += new System.EventHandler(this.trackBarSigma_ValueChanged);
            // 
            // trackBarIntensityBrightnessMax
            // 
            resources.ApplyResources(this.trackBarIntensityBrightnessMax, "trackBarIntensityBrightnessMax");
            this.trackBarIntensityBrightnessMax.LargeChange = 10000;
            this.trackBarIntensityBrightnessMax.Maximum = 1000000;
            this.trackBarIntensityBrightnessMax.Minimum = 1;
            this.trackBarIntensityBrightnessMax.Name = "trackBarIntensityBrightnessMax";
            this.trackBarIntensityBrightnessMax.SmallChange = 100000;
            this.trackBarIntensityBrightnessMax.TickFrequency = 10000;
            this.trackBarIntensityBrightnessMax.Value = 1000000;
            this.trackBarIntensityBrightnessMax.ValueChanged += new System.EventHandler(this.trackBarIntensityBrightnessMax_ValueChanged);
            // 
            // trackBarIntensityBrightnessMin
            // 
            resources.ApplyResources(this.trackBarIntensityBrightnessMin, "trackBarIntensityBrightnessMin");
            this.trackBarIntensityBrightnessMin.LargeChange = 10000;
            this.trackBarIntensityBrightnessMin.Maximum = 999999;
            this.trackBarIntensityBrightnessMin.Name = "trackBarIntensityBrightnessMin";
            this.trackBarIntensityBrightnessMin.SmallChange = 100000;
            this.trackBarIntensityBrightnessMin.TickFrequency = 10000;
            this.trackBarIntensityBrightnessMin.ValueChanged += new System.EventHandler(this.trackBarIntensityBrightnessMax_ValueChanged);
            // 
            // textBoxThickness
            // 
            resources.ApplyResources(this.textBoxThickness, "textBoxThickness");
            this.textBoxThickness.Name = "textBoxThickness";
            this.textBoxThickness.ReadOnly = true;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel2});
            this.statusStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            resources.ApplyResources(this.statusStrip2, "statusStrip2");
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.SizingGrip = false;
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            resources.ApplyResources(this.toolStripProgressBar, "toolStripProgressBar");
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            // 
            // numericBoxThread
            // 
            this.numericBoxThread.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxThread, "numericBoxThread");
            this.numericBoxThread.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxThread.DecimalPlaces = 0;
            this.numericBoxThread.Maximum = 128D;
            this.numericBoxThread.Minimum = 1D;
            this.numericBoxThread.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxThread.MouseSpeed = 1D;
            this.numericBoxThread.Multiline = false;
            this.numericBoxThread.Name = "numericBoxThread";
            this.numericBoxThread.RadianValue = 0.069813170079773182D;
            this.numericBoxThread.ReadOnly = false;
            this.numericBoxThread.RestrictLimitValue = true;
            this.numericBoxThread.ShowFraction = false;
            this.numericBoxThread.ShowPositiveSign = false;
            this.numericBoxThread.ShowUpDown = true;
            this.numericBoxThread.SkipEventDuringInput = true;
            this.numericBoxThread.SmartIncrement = true;
            this.numericBoxThread.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxThread.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxThread.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxThread.ThonsandsSeparator = true;
            this.numericBoxThread.UpDown_Increment = 1D;
            this.numericBoxThread.Value = 4D;
            this.numericBoxThread.WordWrap = true;
            this.numericBoxThread.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.NumericBoxWholeThicknessStart_ValueChanged);
            // 
            // numericBoxThicknessStep
            // 
            this.numericBoxThicknessStep.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxThicknessStep, "numericBoxThicknessStep");
            this.numericBoxThicknessStep.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxThicknessStep.DecimalPlaces = -2;
            this.numericBoxThicknessStep.Maximum = 1000D;
            this.numericBoxThicknessStep.Minimum = 1D;
            this.numericBoxThicknessStep.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxThicknessStep.MouseSpeed = 1D;
            this.numericBoxThicknessStep.Multiline = false;
            this.numericBoxThicknessStep.Name = "numericBoxThicknessStep";
            this.numericBoxThicknessStep.RadianValue = 0.3490658503988659D;
            this.numericBoxThicknessStep.ReadOnly = false;
            this.numericBoxThicknessStep.RestrictLimitValue = true;
            this.numericBoxThicknessStep.ShowFraction = false;
            this.numericBoxThicknessStep.ShowPositiveSign = false;
            this.numericBoxThicknessStep.ShowUpDown = true;
            this.numericBoxThicknessStep.SkipEventDuringInput = true;
            this.numericBoxThicknessStep.SmartIncrement = true;
            this.numericBoxThicknessStep.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxThicknessStep.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxThicknessStep.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxThicknessStep.ThonsandsSeparator = true;
            this.numericBoxThicknessStep.UpDown_Increment = 1D;
            this.numericBoxThicknessStep.Value = 20D;
            this.numericBoxThicknessStep.WordWrap = true;
            this.numericBoxThicknessStep.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.NumericBoxWholeThicknessStart_ValueChanged);
            // 
            // numericBoxWholeThicknessStart
            // 
            this.numericBoxWholeThicknessStart.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxWholeThicknessStart, "numericBoxWholeThicknessStart");
            this.numericBoxWholeThicknessStart.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxWholeThicknessStart.DecimalPlaces = -2;
            this.numericBoxWholeThicknessStart.Maximum = 1000D;
            this.numericBoxWholeThicknessStart.Minimum = 1D;
            this.numericBoxWholeThicknessStart.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxWholeThicknessStart.MouseSpeed = 1D;
            this.numericBoxWholeThicknessStart.Multiline = false;
            this.numericBoxWholeThicknessStart.Name = "numericBoxWholeThicknessStart";
            this.numericBoxWholeThicknessStart.RadianValue = 0.3490658503988659D;
            this.numericBoxWholeThicknessStart.ReadOnly = false;
            this.numericBoxWholeThicknessStart.RestrictLimitValue = true;
            this.numericBoxWholeThicknessStart.ShowFraction = false;
            this.numericBoxWholeThicknessStart.ShowPositiveSign = false;
            this.numericBoxWholeThicknessStart.ShowUpDown = true;
            this.numericBoxWholeThicknessStart.SkipEventDuringInput = true;
            this.numericBoxWholeThicknessStart.SmartIncrement = true;
            this.numericBoxWholeThicknessStart.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxWholeThicknessStart.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxWholeThicknessStart.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxWholeThicknessStart.ThonsandsSeparator = true;
            this.numericBoxWholeThicknessStart.UpDown_Increment = 1D;
            this.numericBoxWholeThicknessStart.Value = 20D;
            this.numericBoxWholeThicknessStart.WordWrap = true;
            this.numericBoxWholeThicknessStart.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.NumericBoxWholeThicknessStart_ValueChanged);
            // 
            // numericBoxThicknessEnd
            // 
            this.numericBoxThicknessEnd.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxThicknessEnd, "numericBoxThicknessEnd");
            this.numericBoxThicknessEnd.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxThicknessEnd.DecimalPlaces = -2;
            this.numericBoxThicknessEnd.Maximum = 1000D;
            this.numericBoxThicknessEnd.Minimum = 1D;
            this.numericBoxThicknessEnd.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxThicknessEnd.MouseSpeed = 1D;
            this.numericBoxThicknessEnd.Multiline = false;
            this.numericBoxThicknessEnd.Name = "numericBoxThicknessEnd";
            this.numericBoxThicknessEnd.RadianValue = 3.4906585039886591D;
            this.numericBoxThicknessEnd.ReadOnly = false;
            this.numericBoxThicknessEnd.RestrictLimitValue = true;
            this.numericBoxThicknessEnd.ShowFraction = false;
            this.numericBoxThicknessEnd.ShowPositiveSign = false;
            this.numericBoxThicknessEnd.ShowUpDown = true;
            this.numericBoxThicknessEnd.SkipEventDuringInput = true;
            this.numericBoxThicknessEnd.SmartIncrement = true;
            this.numericBoxThicknessEnd.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxThicknessEnd.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxThicknessEnd.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxThicknessEnd.ThonsandsSeparator = true;
            this.numericBoxThicknessEnd.UpDown_Increment = 1D;
            this.numericBoxThicknessEnd.Value = 200D;
            this.numericBoxThicknessEnd.WordWrap = true;
            this.numericBoxThicknessEnd.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.NumericBoxWholeThicknessStart_ValueChanged);
            // 
            // numericBoxMaxNumOfG
            // 
            this.numericBoxMaxNumOfG.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxMaxNumOfG, "numericBoxMaxNumOfG");
            this.numericBoxMaxNumOfG.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMaxNumOfG.DecimalPlaces = -2;
            this.numericBoxMaxNumOfG.Maximum = 2048D;
            this.numericBoxMaxNumOfG.Minimum = 1D;
            this.numericBoxMaxNumOfG.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxMaxNumOfG.MouseSpeed = 1D;
            this.numericBoxMaxNumOfG.Multiline = false;
            this.numericBoxMaxNumOfG.Name = "numericBoxMaxNumOfG";
            this.numericBoxMaxNumOfG.RadianValue = 1.1170107212763709D;
            this.numericBoxMaxNumOfG.ReadOnly = false;
            this.numericBoxMaxNumOfG.RestrictLimitValue = true;
            this.numericBoxMaxNumOfG.ShowFraction = false;
            this.numericBoxMaxNumOfG.ShowPositiveSign = false;
            this.numericBoxMaxNumOfG.ShowUpDown = true;
            this.numericBoxMaxNumOfG.SkipEventDuringInput = true;
            this.numericBoxMaxNumOfG.SmartIncrement = true;
            this.numericBoxMaxNumOfG.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxMaxNumOfG.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxMaxNumOfG.TextFont = new System.Drawing.Font("Segoe UI Symbol", 10F);
            this.numericBoxMaxNumOfG.ThonsandsSeparator = true;
            this.numericBoxMaxNumOfG.UpDown_Increment = 1D;
            this.numericBoxMaxNumOfG.Value = 64D;
            this.numericBoxMaxNumOfG.WordWrap = true;
            this.numericBoxMaxNumOfG.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxMaxNumOfG_ValueChanged);
            // 
            // trackBarAdvancedAlphaMax
            // 
            resources.ApplyResources(this.trackBarAdvancedAlphaMax, "trackBarAdvancedAlphaMax");
            this.trackBarAdvancedAlphaMax.ControlHeight = 53;
            this.trackBarAdvancedAlphaMax.DecimalPlaces = -2;
            this.trackBarAdvancedAlphaMax.LogScrollBar = false;
            this.trackBarAdvancedAlphaMax.Maximum = 100D;
            this.trackBarAdvancedAlphaMax.Minimum = 0D;
            this.trackBarAdvancedAlphaMax.Name = "trackBarAdvancedAlphaMax";
            this.trackBarAdvancedAlphaMax.NumericBoxSize = 24;
            this.trackBarAdvancedAlphaMax.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.trackBarAdvancedAlphaMax.Smart_Increment = true;
            this.trackBarAdvancedAlphaMax.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.trackBarAdvancedAlphaMax.UpDown_Increment = 1D;
            this.trackBarAdvancedAlphaMax.Value = 6D;
            this.trackBarAdvancedAlphaMax.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(this.trackBarAdvancedAlphaMax_ValueChanged);
            // 
            // numericBoxDivision
            // 
            this.numericBoxDivision.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxDivision, "numericBoxDivision");
            this.numericBoxDivision.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDivision.DecimalPlaces = -2;
            this.numericBoxDivision.Maximum = 500D;
            this.numericBoxDivision.Minimum = 1D;
            this.numericBoxDivision.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxDivision.MouseSpeed = 1D;
            this.numericBoxDivision.Multiline = false;
            this.numericBoxDivision.Name = "numericBoxDivision";
            this.numericBoxDivision.RadianValue = 2.2340214425527418D;
            this.numericBoxDivision.ReadOnly = false;
            this.numericBoxDivision.RestrictLimitValue = true;
            this.numericBoxDivision.ShowFraction = false;
            this.numericBoxDivision.ShowPositiveSign = false;
            this.numericBoxDivision.ShowUpDown = true;
            this.numericBoxDivision.SkipEventDuringInput = false;
            this.numericBoxDivision.SmartIncrement = true;
            this.numericBoxDivision.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDivision.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDivision.TextFont = new System.Drawing.Font("Segoe UI Symbol", 10F);
            this.numericBoxDivision.ThonsandsSeparator = true;
            this.numericBoxDivision.UpDown_Increment = 1D;
            this.numericBoxDivision.Value = 128D;
            this.numericBoxDivision.WordWrap = true;
            this.numericBoxDivision.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.NumericBoxDivision_ValueChanged);
            // 
            // numericBoxImageSize
            // 
            this.numericBoxImageSize.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxImageSize, "numericBoxImageSize");
            this.numericBoxImageSize.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxImageSize.DecimalPlaces = -2;
            this.numericBoxImageSize.Maximum = 3096D;
            this.numericBoxImageSize.Minimum = 1D;
            this.numericBoxImageSize.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxImageSize.MouseSpeed = 1D;
            this.numericBoxImageSize.Multiline = false;
            this.numericBoxImageSize.Name = "numericBoxImageSize";
            this.numericBoxImageSize.RadianValue = 8.9360857702109673D;
            this.numericBoxImageSize.ReadOnly = false;
            this.numericBoxImageSize.RestrictLimitValue = true;
            this.numericBoxImageSize.ShowFraction = false;
            this.numericBoxImageSize.ShowPositiveSign = false;
            this.numericBoxImageSize.ShowUpDown = true;
            this.numericBoxImageSize.SkipEventDuringInput = true;
            this.numericBoxImageSize.SmartIncrement = true;
            this.numericBoxImageSize.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxImageSize.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxImageSize.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxImageSize.ThonsandsSeparator = true;
            this.numericBoxImageSize.UpDown_Increment = 1D;
            this.numericBoxImageSize.Value = 512D;
            this.numericBoxImageSize.WordWrap = true;
            this.numericBoxImageSize.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxImageSize_ValueChanged);
            // 
            // comboBoxGradient
            // 
            this.comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxGradient, "comboBoxGradient");
            this.comboBoxGradient.FormattingEnabled = true;
            this.comboBoxGradient.Items.AddRange(new object[] {
            resources.GetString("comboBoxGradient.Items"),
            resources.GetString("comboBoxGradient.Items1")});
            this.comboBoxGradient.Name = "comboBoxGradient";
            this.comboBoxGradient.SelectedIndexChanged += new System.EventHandler(this.trackBarIntensityBrightnessMax_ValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // FormDiffractionSimulatorCBED
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.groupBoxOutput);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.statusStrip2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDiffractionSimulatorCBED";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDiffractionSimulatorMultislice_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOutputThickness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIntensityBrightnessMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIntensityBrightnessMin)).EndInit();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonExecute;
        private Crystallography.Controls.NumericBox numericBoxWholeThicknessStart;
        private System.Windows.Forms.CheckBox checkBoxDrawGuideCircles;
        private Crystallography.Controls.NumericBox numericBoxMaxNumOfG;
        private Crystallography.Controls.NumericBox numericBoxDivision;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelDivisionNumber;
        private Crystallography.Controls.NumericBox numericBoxThicknessEnd;
        private Crystallography.Controls.NumericBox numericBoxThicknessStep;
        private Crystallography.Controls.NumericBox numericBoxImageSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.TrackBar trackBarGamma;
        private System.Windows.Forms.TrackBar trackBarIntensityBrightnessMax;
        private System.Windows.Forms.TrackBar trackBarIntensityBrightnessMin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBoxThickness;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ComboBox comboBoxScale;
        public Crystallography.Controls.TrackBarAdvanced trackBarAdvancedAlphaMax;
        public System.Windows.Forms.TrackBar trackBarOutputThickness;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ComboBox comboBoxSolver;
        private Crystallography.Controls.NumericBox numericBoxThread;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBoxGradient;
    }
}