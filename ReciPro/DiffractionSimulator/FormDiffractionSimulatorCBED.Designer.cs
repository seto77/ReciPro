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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiffractionSimulatorCBED));
            buttonExecute = new System.Windows.Forms.Button();
            checkBoxDrawGuideCircles = new System.Windows.Forms.CheckBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            labelDivisionNumber = new System.Windows.Forms.Label();
            radioButtonLACBED = new System.Windows.Forms.RadioButton();
            numericBoxAlphaMax = new NumericBox();
            radioButtonCBED = new System.Windows.Forms.RadioButton();
            comboBoxSolver = new System.Windows.Forms.ComboBox();
            numericBoxThread = new NumericBox();
            numericBoxThicknessStep = new NumericBox();
            numericBoxMaxNumOfG = new NumericBox();
            numericBoxWholeThicknessStart = new NumericBox();
            numericBoxThicknessEnd = new NumericBox();
            buttonStop = new System.Windows.Forms.Button();
            numericBoxDiskResolution = new NumericBox();
            label13 = new System.Windows.Forms.Label();
            groupBoxOutput = new System.Windows.Forms.GroupBox();
            radioButtonIndividualDisk = new System.Windows.Forms.RadioButton();
            radioButtonAllDisks = new System.Windows.Forms.RadioButton();
            label3 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            comboBoxGradient = new System.Windows.Forms.ComboBox();
            comboBoxScale = new System.Windows.Forms.ComboBox();
            trackBarOutputThickness = new System.Windows.Forms.TrackBar();
            trackBarGamma = new System.Windows.Forms.TrackBar();
            trackBarIntensityBrightnessMax = new System.Windows.Forms.TrackBar();
            trackBarIntensityBrightnessMin = new System.Windows.Forms.TrackBar();
            textBoxThickness = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            labelGamma = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            statusStrip2 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolTip = new System.Windows.Forms.ToolTip(components);
            groupBox1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            groupBoxOutput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).BeginInit();
            statusStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // buttonExecute
            // 
            resources.ApplyResources(buttonExecute, "buttonExecute");
            buttonExecute.BackColor = System.Drawing.Color.LightSteelBlue;
            buttonExecute.ForeColor = System.Drawing.Color.RoyalBlue;
            buttonExecute.Name = "buttonExecute";
            toolTip.SetToolTip(buttonExecute, resources.GetString("buttonExecute.ToolTip"));
            buttonExecute.UseVisualStyleBackColor = false;
            buttonExecute.Click += buttonExecute_Click;
            // 
            // checkBoxDrawGuideCircles
            // 
            resources.ApplyResources(checkBoxDrawGuideCircles, "checkBoxDrawGuideCircles");
            checkBoxDrawGuideCircles.Checked = true;
            checkBoxDrawGuideCircles.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawGuideCircles.Name = "checkBoxDrawGuideCircles";
            toolTip.SetToolTip(checkBoxDrawGuideCircles, resources.GetString("checkBoxDrawGuideCircles.ToolTip"));
            checkBoxDrawGuideCircles.UseVisualStyleBackColor = true;
            checkBoxDrawGuideCircles.CheckedChanged += CheckBoxDrawGuideCircles_CheckedChanged;
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(flowLayoutPanel1);
            groupBox1.Controls.Add(radioButtonLACBED);
            groupBox1.Controls.Add(numericBoxAlphaMax);
            groupBox1.Controls.Add(checkBoxDrawGuideCircles);
            groupBox1.Controls.Add(radioButtonCBED);
            groupBox1.Controls.Add(comboBoxSolver);
            groupBox1.Controls.Add(numericBoxThread);
            groupBox1.Controls.Add(numericBoxThicknessStep);
            groupBox1.Controls.Add(numericBoxMaxNumOfG);
            groupBox1.Controls.Add(numericBoxWholeThicknessStart);
            groupBox1.Controls.Add(numericBoxThicknessEnd);
            groupBox1.Controls.Add(buttonStop);
            groupBox1.Controls.Add(buttonExecute);
            groupBox1.Controls.Add(numericBoxDiskResolution);
            groupBox1.Controls.Add(label13);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            toolTip.SetToolTip(groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(labelDivisionNumber);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            toolTip.SetToolTip(flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
            flowLayoutPanel1.Paint += flowLayoutPanel1_Paint;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            label2.Click += labelDivisionNumber_Click;
            // 
            // labelDivisionNumber
            // 
            resources.ApplyResources(labelDivisionNumber, "labelDivisionNumber");
            labelDivisionNumber.BackColor = System.Drawing.Color.Transparent;
            labelDivisionNumber.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            labelDivisionNumber.Name = "labelDivisionNumber";
            toolTip.SetToolTip(labelDivisionNumber, resources.GetString("labelDivisionNumber.ToolTip"));
            labelDivisionNumber.Click += labelDivisionNumber_Click;
            // 
            // radioButtonLACBED
            // 
            resources.ApplyResources(radioButtonLACBED, "radioButtonLACBED");
            radioButtonLACBED.Name = "radioButtonLACBED";
            toolTip.SetToolTip(radioButtonLACBED, resources.GetString("radioButtonLACBED.ToolTip"));
            radioButtonLACBED.UseVisualStyleBackColor = true;
            // 
            // numericBoxAlphaMax
            // 
            resources.ApplyResources(numericBoxAlphaMax, "numericBoxAlphaMax");
            numericBoxAlphaMax.BackColor = System.Drawing.Color.Transparent;
            numericBoxAlphaMax.DecimalPlaces = 1;
            numericBoxAlphaMax.Maximum = 100D;
            numericBoxAlphaMax.Minimum = 0D;
            numericBoxAlphaMax.Name = "numericBoxAlphaMax";
            numericBoxAlphaMax.RadianValue = 0.10471975511965977D;
            numericBoxAlphaMax.RoundErrorAccuracy = -1;
            numericBoxAlphaMax.ShowUpDown = true;
            numericBoxAlphaMax.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxAlphaMax, resources.GetString("numericBoxAlphaMax.ToolTip"));
            numericBoxAlphaMax.Value = 6D;
            numericBoxAlphaMax.ValueChanged += numericBoxAlphaMax_ValueChanged;
            // 
            // radioButtonCBED
            // 
            resources.ApplyResources(radioButtonCBED, "radioButtonCBED");
            radioButtonCBED.Checked = true;
            radioButtonCBED.Name = "radioButtonCBED";
            radioButtonCBED.TabStop = true;
            toolTip.SetToolTip(radioButtonCBED, resources.GetString("radioButtonCBED.ToolTip"));
            radioButtonCBED.UseVisualStyleBackColor = true;
            radioButtonCBED.CheckedChanged += radioButtonCBED_CheckedChanged;
            // 
            // comboBoxSolver
            // 
            resources.ApplyResources(comboBoxSolver, "comboBoxSolver");
            comboBoxSolver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxSolver.FormattingEnabled = true;
            comboBoxSolver.Items.AddRange(new object[] { resources.GetString("comboBoxSolver.Items"), resources.GetString("comboBoxSolver.Items1"), resources.GetString("comboBoxSolver.Items2"), resources.GetString("comboBoxSolver.Items3"), resources.GetString("comboBoxSolver.Items4") });
            comboBoxSolver.Name = "comboBoxSolver";
            toolTip.SetToolTip(comboBoxSolver, resources.GetString("comboBoxSolver.ToolTip"));
            comboBoxSolver.SelectedIndexChanged += ComboBoxSolver_SelectedIndexChanged;
            // 
            // numericBoxThread
            // 
            resources.ApplyResources(numericBoxThread, "numericBoxThread");
            numericBoxThread.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThread.DecimalPlaces = 0;
            numericBoxThread.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThread.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThread.Maximum = 128D;
            numericBoxThread.Minimum = 1D;
            numericBoxThread.Name = "numericBoxThread";
            numericBoxThread.RadianValue = 0.069813170079773182D;
            numericBoxThread.RoundErrorAccuracy = -1;
            numericBoxThread.ShowUpDown = true;
            numericBoxThread.SmartIncrement = true;
            numericBoxThread.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxThread, resources.GetString("numericBoxThread.ToolTip"));
            numericBoxThread.Value = 4D;
            numericBoxThread.ValueChanged += NumericBoxWholeThicknessStart_ValueChanged;
            // 
            // numericBoxThicknessStep
            // 
            resources.ApplyResources(numericBoxThicknessStep, "numericBoxThicknessStep");
            numericBoxThicknessStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.Maximum = 1000D;
            numericBoxThicknessStep.Minimum = 1D;
            numericBoxThicknessStep.Name = "numericBoxThicknessStep";
            numericBoxThicknessStep.RadianValue = 0.3490658503988659D;
            numericBoxThicknessStep.RoundErrorAccuracy = -1;
            numericBoxThicknessStep.ShowUpDown = true;
            numericBoxThicknessStep.SmartIncrement = true;
            numericBoxThicknessStep.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessStep, resources.GetString("numericBoxThicknessStep.ToolTip"));
            numericBoxThicknessStep.Value = 20D;
            numericBoxThicknessStep.ValueChanged += NumericBoxWholeThicknessStart_ValueChanged;
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
            numericBoxMaxNumOfG.RadianValue = 1.1170107212763709D;
            numericBoxMaxNumOfG.RoundErrorAccuracy = -1;
            numericBoxMaxNumOfG.ShowUpDown = true;
            numericBoxMaxNumOfG.SmartIncrement = true;
            numericBoxMaxNumOfG.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxMaxNumOfG, resources.GetString("numericBoxMaxNumOfG.ToolTip"));
            numericBoxMaxNumOfG.Value = 64D;
            numericBoxMaxNumOfG.ValueChanged += numericBoxMaxNumOfG_ValueChanged;
            // 
            // numericBoxWholeThicknessStart
            // 
            resources.ApplyResources(numericBoxWholeThicknessStart, "numericBoxWholeThicknessStart");
            numericBoxWholeThicknessStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxWholeThicknessStart.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxWholeThicknessStart.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxWholeThicknessStart.Maximum = 1000D;
            numericBoxWholeThicknessStart.Minimum = 1D;
            numericBoxWholeThicknessStart.Name = "numericBoxWholeThicknessStart";
            numericBoxWholeThicknessStart.RadianValue = 0.3490658503988659D;
            numericBoxWholeThicknessStart.RoundErrorAccuracy = -1;
            numericBoxWholeThicknessStart.ShowUpDown = true;
            numericBoxWholeThicknessStart.SmartIncrement = true;
            numericBoxWholeThicknessStart.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxWholeThicknessStart, resources.GetString("numericBoxWholeThicknessStart.ToolTip"));
            numericBoxWholeThicknessStart.Value = 20D;
            numericBoxWholeThicknessStart.ValueChanged += NumericBoxWholeThicknessStart_ValueChanged;
            // 
            // numericBoxThicknessEnd
            // 
            resources.ApplyResources(numericBoxThicknessEnd, "numericBoxThicknessEnd");
            numericBoxThicknessEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.Maximum = 1000D;
            numericBoxThicknessEnd.Minimum = 1D;
            numericBoxThicknessEnd.Name = "numericBoxThicknessEnd";
            numericBoxThicknessEnd.RadianValue = 3.4906585039886591D;
            numericBoxThicknessEnd.RoundErrorAccuracy = -1;
            numericBoxThicknessEnd.ShowUpDown = true;
            numericBoxThicknessEnd.SmartIncrement = true;
            numericBoxThicknessEnd.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessEnd, resources.GetString("numericBoxThicknessEnd.ToolTip"));
            numericBoxThicknessEnd.Value = 200D;
            numericBoxThicknessEnd.ValueChanged += NumericBoxWholeThicknessStart_ValueChanged;
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
            // numericBoxDiskResolution
            // 
            resources.ApplyResources(numericBoxDiskResolution, "numericBoxDiskResolution");
            numericBoxDiskResolution.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDiskResolution.DecimalPlaces = 3;
            numericBoxDiskResolution.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDiskResolution.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDiskResolution.Maximum = 1D;
            numericBoxDiskResolution.Minimum = 0.001D;
            numericBoxDiskResolution.Name = "numericBoxDiskResolution";
            numericBoxDiskResolution.RadianValue = 0.0017453292519943296D;
            numericBoxDiskResolution.RoundErrorAccuracy = -1;
            numericBoxDiskResolution.ShowUpDown = true;
            numericBoxDiskResolution.SkipEventDuringInput = false;
            numericBoxDiskResolution.SmartIncrement = true;
            numericBoxDiskResolution.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxDiskResolution, resources.GetString("numericBoxDiskResolution.ToolTip"));
            numericBoxDiskResolution.Value = 0.1D;
            numericBoxDiskResolution.ValueChanged += NumericBoxDivision_ValueChanged;
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip"));
            // 
            // groupBoxOutput
            // 
            resources.ApplyResources(groupBoxOutput, "groupBoxOutput");
            groupBoxOutput.Controls.Add(radioButtonIndividualDisk);
            groupBoxOutput.Controls.Add(radioButtonAllDisks);
            groupBoxOutput.Controls.Add(label3);
            groupBoxOutput.Controls.Add(label11);
            groupBoxOutput.Controls.Add(comboBoxGradient);
            groupBoxOutput.Controls.Add(comboBoxScale);
            groupBoxOutput.Controls.Add(trackBarOutputThickness);
            groupBoxOutput.Controls.Add(trackBarGamma);
            groupBoxOutput.Controls.Add(trackBarIntensityBrightnessMax);
            groupBoxOutput.Controls.Add(trackBarIntensityBrightnessMin);
            groupBoxOutput.Controls.Add(textBoxThickness);
            groupBoxOutput.Controls.Add(label6);
            groupBoxOutput.Controls.Add(label4);
            groupBoxOutput.Controls.Add(labelGamma);
            groupBoxOutput.Controls.Add(label8);
            groupBoxOutput.Controls.Add(label7);
            groupBoxOutput.Controls.Add(label1);
            groupBoxOutput.Controls.Add(label5);
            groupBoxOutput.Name = "groupBoxOutput";
            groupBoxOutput.TabStop = false;
            toolTip.SetToolTip(groupBoxOutput, resources.GetString("groupBoxOutput.ToolTip"));
            // 
            // radioButtonIndividualDisk
            // 
            resources.ApplyResources(radioButtonIndividualDisk, "radioButtonIndividualDisk");
            radioButtonIndividualDisk.Name = "radioButtonIndividualDisk";
            toolTip.SetToolTip(radioButtonIndividualDisk, resources.GetString("radioButtonIndividualDisk.ToolTip"));
            radioButtonIndividualDisk.UseVisualStyleBackColor = true;
            radioButtonIndividualDisk.CheckedChanged += radioButtonAllDisks_CheckedChanged;
            // 
            // radioButtonAllDisks
            // 
            resources.ApplyResources(radioButtonAllDisks, "radioButtonAllDisks");
            radioButtonAllDisks.Checked = true;
            radioButtonAllDisks.Name = "radioButtonAllDisks";
            radioButtonAllDisks.TabStop = true;
            toolTip.SetToolTip(radioButtonAllDisks, resources.GetString("radioButtonAllDisks.ToolTip"));
            radioButtonAllDisks.UseVisualStyleBackColor = true;
            radioButtonAllDisks.CheckedChanged += radioButtonAllDisks_CheckedChanged;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip"));
            // 
            // comboBoxGradient
            // 
            resources.ApplyResources(comboBoxGradient, "comboBoxGradient");
            comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxGradient.FormattingEnabled = true;
            comboBoxGradient.Items.AddRange(new object[] { resources.GetString("comboBoxGradient.Items"), resources.GetString("comboBoxGradient.Items1") });
            comboBoxGradient.Name = "comboBoxGradient";
            toolTip.SetToolTip(comboBoxGradient, resources.GetString("comboBoxGradient.ToolTip"));
            comboBoxGradient.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // comboBoxScale
            // 
            resources.ApplyResources(comboBoxScale, "comboBoxScale");
            comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale.FormattingEnabled = true;
            comboBoxScale.Items.AddRange(new object[] { resources.GetString("comboBoxScale.Items"), resources.GetString("comboBoxScale.Items1"), resources.GetString("comboBoxScale.Items2"), resources.GetString("comboBoxScale.Items3") });
            comboBoxScale.Name = "comboBoxScale";
            toolTip.SetToolTip(comboBoxScale, resources.GetString("comboBoxScale.ToolTip"));
            comboBoxScale.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // trackBarOutputThickness
            // 
            resources.ApplyResources(trackBarOutputThickness, "trackBarOutputThickness");
            trackBarOutputThickness.LargeChange = 1;
            trackBarOutputThickness.Maximum = 9;
            trackBarOutputThickness.Name = "trackBarOutputThickness";
            toolTip.SetToolTip(trackBarOutputThickness, resources.GetString("trackBarOutputThickness.ToolTip"));
            trackBarOutputThickness.Scroll += TrackBarOutputThickness_Scroll;
            trackBarOutputThickness.ValueChanged += TrackBarOutputThickness_Scroll;
            // 
            // trackBarGamma
            // 
            resources.ApplyResources(trackBarGamma, "trackBarGamma");
            trackBarGamma.LargeChange = 200;
            trackBarGamma.Maximum = 2000;
            trackBarGamma.Name = "trackBarGamma";
            trackBarGamma.SmallChange = 50;
            trackBarGamma.TickFrequency = 50;
            toolTip.SetToolTip(trackBarGamma, resources.GetString("trackBarGamma.ToolTip"));
            trackBarGamma.ValueChanged += trackBarGamma_ValueChanged;
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
            // textBoxThickness
            // 
            resources.ApplyResources(textBoxThickness, "textBoxThickness");
            textBoxThickness.Name = "textBoxThickness";
            textBoxThickness.ReadOnly = true;
            toolTip.SetToolTip(textBoxThickness, resources.GetString("textBoxThickness.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // labelGamma
            // 
            resources.ApplyResources(labelGamma, "labelGamma");
            labelGamma.Name = "labelGamma";
            toolTip.SetToolTip(labelGamma, resources.GetString("labelGamma.ToolTip"));
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
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // statusStrip2
            // 
            resources.ApplyResources(statusStrip2, "statusStrip2");
            statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusLabel2, toolStripStatusLabel1 });
            statusStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            statusStrip2.Name = "statusStrip2";
            statusStrip2.SizingGrip = false;
            toolTip.SetToolTip(statusStrip2, resources.GetString("statusStrip2.ToolTip"));
            // 
            // toolStripProgressBar
            // 
            resources.ApplyResources(toolStripProgressBar, "toolStripProgressBar");
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripStatusLabel2
            // 
            resources.ApplyResources(toolStripStatusLabel2, "toolStripStatusLabel2");
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel1
            // 
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // FormDiffractionSimulatorCBED
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ControlBox = false;
            Controls.Add(groupBoxOutput);
            Controls.Add(groupBox1);
            Controls.Add(statusStrip2);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormDiffractionSimulatorCBED";
            ShowIcon = false;
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            FormClosing += FormDiffractionSimulatorMultislice_FormClosing;
            VisibleChanged += FormDiffractionSimulatorCBED_VisibleChanged;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBoxOutput.ResumeLayout(false);
            groupBoxOutput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).EndInit();
            statusStrip2.ResumeLayout(false);
            statusStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button buttonExecute;
        private Crystallography.Controls.NumericBox numericBoxWholeThicknessStart;
        private System.Windows.Forms.CheckBox checkBoxDrawGuideCircles;
        private Crystallography.Controls.NumericBox numericBoxMaxNumOfG;
        private Crystallography.Controls.NumericBox numericBoxDiskResolution;
        private Crystallography.Controls.NumericBox numericBoxThicknessEnd;
        private Crystallography.Controls.NumericBox numericBoxThicknessStep;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.TrackBar trackBarGamma;
        private System.Windows.Forms.TrackBar trackBarIntensityBrightnessMax;
        private System.Windows.Forms.TrackBar trackBarIntensityBrightnessMin;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelGamma;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox textBoxThickness;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ComboBox comboBoxScale;
        public System.Windows.Forms.TrackBar trackBarOutputThickness;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox comboBoxGradient;
        private System.Windows.Forms.Label labelDivisionNumber;
        private System.Windows.Forms.ComboBox comboBoxSolver;
        private Crystallography.Controls.NumericBox numericBoxThread;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.RadioButton radioButtonIndividualDisk;
        private System.Windows.Forms.RadioButton radioButtonAllDisks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip;
        private NumericBox numericBoxAlphaMax;
        private System.Windows.Forms.RadioButton radioButtonLACBED;
        private System.Windows.Forms.RadioButton radioButtonCBED;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
    }
}