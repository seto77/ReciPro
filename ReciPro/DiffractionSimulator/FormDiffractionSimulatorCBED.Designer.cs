namespace ReciPro
{
    partial class FormDiffractionSimulatorCBED
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
        // groupBox1 -> groupBoxInputParameters
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiffractionSimulatorCBED));
            buttonSimulate = new System.Windows.Forms.Button();
            checkBoxDrawGuideCircles = new System.Windows.Forms.CheckBox();
            groupBoxInputParameters = new System.Windows.Forms.GroupBox();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonCBED = new System.Windows.Forms.RadioButton();
            radioButtonLACBED = new System.Windows.Forms.RadioButton();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxMaxNumOfG = new NumericBox();
            numericBoxAlphaMax = new NumericBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxDiskResolution = new NumericBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            labelDivisionNumber = new System.Windows.Forms.Label();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxWholeThicknessStart = new NumericBox();
            numericBoxThicknessEnd = new NumericBox();
            numericBoxThicknessStep = new NumericBox();
            comboBoxSolver = new System.Windows.Forms.ComboBox();
            numericBoxThread = new NumericBox();
            buttonStop = new System.Windows.Forms.Button();
            label13 = new System.Windows.Forms.Label();
            groupBoxOutput = new System.Windows.Forms.GroupBox();
            flowLayoutPanel12 = new System.Windows.Forms.FlowLayoutPanel();
            label3 = new System.Windows.Forms.Label();
            comboBoxGradient = new System.Windows.Forms.ComboBox();
            label11 = new System.Windows.Forms.Label();
            comboBoxScale = new System.Windows.Forms.ComboBox();
            flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
            labelGamma = new System.Windows.Forms.Label();
            trackBarGamma = new System.Windows.Forms.TrackBar();
            flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            label5 = new System.Windows.Forms.Label();
            flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            label7 = new System.Windows.Forms.Label();
            trackBarIntensityBrightnessMin = new System.Windows.Forms.TrackBar();
            label8 = new System.Windows.Forms.Label();
            trackBarIntensityBrightnessMax = new System.Windows.Forms.TrackBar();
            flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            radioButtonAllDisks = new System.Windows.Forms.RadioButton();
            radioButtonIndividualDisk = new System.Windows.Forms.RadioButton();
            flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            label4 = new System.Windows.Forms.Label();
            textBoxThickness = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            trackBarOutputThickness = new System.Windows.Forms.TrackBar();
            statusStrip2 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolTip = new System.Windows.Forms.ToolTip(components);
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            flowLayoutPanel13 = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxInputParameters.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            groupBoxOutput.SuspendLayout();
            flowLayoutPanel12.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).BeginInit();
            flowLayoutPanel10.SuspendLayout();
            flowLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).BeginInit();
            flowLayoutPanel8.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).BeginInit();
            statusStrip2.SuspendLayout();
            flowLayoutPanel13.SuspendLayout();
            SuspendLayout();
            // 
            // buttonSimulate
            // 
            resources.ApplyResources(buttonSimulate, "buttonSimulate");
            buttonSimulate.BackColor = System.Drawing.Color.SteelBlue;
            buttonSimulate.ForeColor = System.Drawing.Color.White;
            buttonSimulate.Name = "buttonSimulate";
            toolTip.SetToolTip(buttonSimulate, resources.GetString("buttonSimulate.ToolTip"));
            buttonSimulate.UseVisualStyleBackColor = false;
            buttonSimulate.Click += buttonSimulate_Click;
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
            // groupBoxInputParameters
            // 
            captureExtender.SetCapture(groupBoxInputParameters, true);
            groupBoxInputParameters.Controls.Add(flowLayoutPanel13);
            groupBoxInputParameters.Controls.Add(flowLayoutPanel6);
            groupBoxInputParameters.Controls.Add(buttonStop);
            groupBoxInputParameters.Controls.Add(buttonSimulate);
            resources.ApplyResources(groupBoxInputParameters, "groupBoxInputParameters");
            groupBoxInputParameters.Name = "groupBoxInputParameters";
            groupBoxInputParameters.TabStop = false;
            // 
            // flowLayoutPanel6
            // 
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Controls.Add(flowLayoutPanel5);
            flowLayoutPanel6.Controls.Add(flowLayoutPanel4);
            flowLayoutPanel6.Controls.Add(flowLayoutPanel3);
            flowLayoutPanel6.Controls.Add(flowLayoutPanel2);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(radioButtonCBED);
            flowLayoutPanel5.Controls.Add(radioButtonLACBED);
            flowLayoutPanel5.Controls.Add(checkBoxDrawGuideCircles);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
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
            // radioButtonLACBED
            // 
            resources.ApplyResources(radioButtonLACBED, "radioButtonLACBED");
            radioButtonLACBED.Name = "radioButtonLACBED";
            toolTip.SetToolTip(radioButtonLACBED, resources.GetString("radioButtonLACBED.ToolTip"));
            radioButtonLACBED.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(numericBoxMaxNumOfG);
            flowLayoutPanel4.Controls.Add(numericBoxAlphaMax);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
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
            numericBoxMaxNumOfG.ShowUpDown = true;
            numericBoxMaxNumOfG.SmartIncrement = true;
            numericBoxMaxNumOfG.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxMaxNumOfG, resources.GetString("numericBoxMaxNumOfG.ToolTip"));
            numericBoxMaxNumOfG.Value = 64D;
            numericBoxMaxNumOfG.ValueFontSize = 9F;
            numericBoxMaxNumOfG.ValueChanged += numericBoxMaxNumOfG_ValueChanged;
            // 
            // numericBoxAlphaMax
            // 
            numericBoxAlphaMax.BackColor = System.Drawing.Color.Transparent;
            numericBoxAlphaMax.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxAlphaMax, "numericBoxAlphaMax");
            numericBoxAlphaMax.Maximum = 2000D;
            numericBoxAlphaMax.Minimum = 0D;
            numericBoxAlphaMax.Name = "numericBoxAlphaMax";
            numericBoxAlphaMax.RadianValue = 0.10471975511965977D;
            numericBoxAlphaMax.ShowUpDown = true;
            numericBoxAlphaMax.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxAlphaMax, resources.GetString("numericBoxAlphaMax.ToolTip"));
            numericBoxAlphaMax.Value = 6D;
            numericBoxAlphaMax.ValueFontSize = 9F;
            numericBoxAlphaMax.ValueChanged += numericBoxAlphaMax_ValueChanged;
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(numericBoxDiskResolution);
            flowLayoutPanel3.Controls.Add(flowLayoutPanel1);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // numericBoxDiskResolution
            // 
            numericBoxDiskResolution.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDiskResolution.DecimalPlaces = 3;
            numericBoxDiskResolution.FooterBackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxDiskResolution, "numericBoxDiskResolution");
            numericBoxDiskResolution.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDiskResolution.Maximum = 100D;
            numericBoxDiskResolution.Minimum = 0.001D;
            numericBoxDiskResolution.Name = "numericBoxDiskResolution";
            numericBoxDiskResolution.RadianValue = 0.0017453292519943296D;
            numericBoxDiskResolution.ShowUpDown = true;
            numericBoxDiskResolution.SmartIncrement = true;
            numericBoxDiskResolution.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxDiskResolution, resources.GetString("numericBoxDiskResolution.ToolTip"));
            numericBoxDiskResolution.Value = 0.1D;
            numericBoxDiskResolution.ValueFontSize = 9F;
            numericBoxDiskResolution.ValueChanged += NumericBoxDivision_ValueChanged;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(labelDivisionNumber);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip")); // (260531Ch)
            // 
            // labelDivisionNumber
            // 
            resources.ApplyResources(labelDivisionNumber, "labelDivisionNumber");
            labelDivisionNumber.BackColor = System.Drawing.Color.Transparent;
            labelDivisionNumber.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            labelDivisionNumber.Name = "labelDivisionNumber";
            toolTip.SetToolTip(labelDivisionNumber, resources.GetString("labelDivisionNumber.ToolTip")); // (260531Ch)
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(numericBoxWholeThicknessStart);
            flowLayoutPanel2.Controls.Add(numericBoxThicknessEnd);
            flowLayoutPanel2.Controls.Add(numericBoxThicknessStep);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
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
            numericBoxWholeThicknessStart.ShowUpDown = true;
            numericBoxWholeThicknessStart.SmartIncrement = true;
            numericBoxWholeThicknessStart.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxWholeThicknessStart, resources.GetString("numericBoxWholeThicknessStart.ToolTip"));
            numericBoxWholeThicknessStart.Value = 20D;
            numericBoxWholeThicknessStart.ValueFontSize = 9F;
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
            numericBoxThicknessEnd.ShowUpDown = true;
            numericBoxThicknessEnd.SmartIncrement = true;
            numericBoxThicknessEnd.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessEnd, resources.GetString("numericBoxThicknessEnd.ToolTip"));
            numericBoxThicknessEnd.Value = 200D;
            numericBoxThicknessEnd.ValueFontSize = 9F;
            numericBoxThicknessEnd.ValueChanged += NumericBoxWholeThicknessStart_ValueChanged;
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
            numericBoxThicknessStep.ShowUpDown = true;
            numericBoxThicknessStep.SmartIncrement = true;
            numericBoxThicknessStep.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessStep, resources.GetString("numericBoxThicknessStep.ToolTip"));
            numericBoxThicknessStep.Value = 20D;
            numericBoxThicknessStep.ValueFontSize = 9F;
            numericBoxThicknessStep.ValueChanged += NumericBoxWholeThicknessStart_ValueChanged;
            // 
            // comboBoxSolver
            // 
            comboBoxSolver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxSolver, "comboBoxSolver");
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
            numericBoxThread.ShowUpDown = true;
            numericBoxThread.SmartIncrement = true;
            numericBoxThread.ThousandsSeparator = true;
            // toolTip.SetToolTip(numericBoxThread, resources.GetString("numericBoxThread.ToolTip1")); // (260531Ch) 旧キー
            toolTip.SetToolTip(numericBoxThread, resources.GetString("numericBoxThread.ToolTip")); // (260531Ch)
            numericBoxThread.Value = 4D;
            numericBoxThread.ValueChanged += NumericBoxWholeThicknessStart_ValueChanged;
            // 
            // buttonStop
            // 
            buttonStop.BackColor = System.Drawing.Color.IndianRed;
            buttonStop.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(buttonStop, "buttonStop");
            buttonStop.Name = "buttonStop";
            toolTip.SetToolTip(buttonStop, resources.GetString("buttonStop.ToolTip"));
            buttonStop.UseVisualStyleBackColor = false;
            buttonStop.Click += buttonStop_Click;
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip")); // (260531Ch)
            // 
            // groupBoxOutput
            // 
            captureExtender.SetCapture(groupBoxOutput, true);
            groupBoxOutput.Controls.Add(flowLayoutPanel12);
            groupBoxOutput.Controls.Add(flowLayoutPanel11);
            groupBoxOutput.Controls.Add(flowLayoutPanel10);
            groupBoxOutput.Controls.Add(flowLayoutPanel8);
            groupBoxOutput.Controls.Add(flowLayoutPanel7);
            resources.ApplyResources(groupBoxOutput, "groupBoxOutput");
            groupBoxOutput.Name = "groupBoxOutput";
            groupBoxOutput.TabStop = false;
            // 
            // flowLayoutPanel12
            // 
            resources.ApplyResources(flowLayoutPanel12, "flowLayoutPanel12");
            flowLayoutPanel12.Controls.Add(label3);
            flowLayoutPanel12.Controls.Add(comboBoxGradient);
            flowLayoutPanel12.Controls.Add(label11);
            flowLayoutPanel12.Controls.Add(comboBoxScale);
            flowLayoutPanel12.Name = "flowLayoutPanel12";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip")); // (260531Ch)
            // 
            // comboBoxGradient
            // 
            comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxGradient, "comboBoxGradient");
            comboBoxGradient.FormattingEnabled = true;
            comboBoxGradient.Items.AddRange(new object[] { resources.GetString("comboBoxGradient.Items"), resources.GetString("comboBoxGradient.Items1") });
            comboBoxGradient.Name = "comboBoxGradient";
            toolTip.SetToolTip(comboBoxGradient, resources.GetString("comboBoxGradient.ToolTip"));
            comboBoxGradient.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip")); // (260531Ch)
            // 
            // comboBoxScale
            // 
            comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxScale, "comboBoxScale");
            comboBoxScale.FormattingEnabled = true;
            comboBoxScale.Items.AddRange(new object[] { resources.GetString("comboBoxScale.Items"), resources.GetString("comboBoxScale.Items1"), resources.GetString("comboBoxScale.Items2"), resources.GetString("comboBoxScale.Items3") });
            comboBoxScale.Name = "comboBoxScale";
            toolTip.SetToolTip(comboBoxScale, resources.GetString("comboBoxScale.ToolTip"));
            comboBoxScale.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // flowLayoutPanel11
            // 
            resources.ApplyResources(flowLayoutPanel11, "flowLayoutPanel11");
            flowLayoutPanel11.Controls.Add(labelGamma);
            flowLayoutPanel11.Controls.Add(trackBarGamma);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            // 
            // labelGamma
            // 
            resources.ApplyResources(labelGamma, "labelGamma");
            labelGamma.Name = "labelGamma";
            toolTip.SetToolTip(labelGamma, resources.GetString("labelGamma.ToolTip")); // (260531Ch)
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
            // flowLayoutPanel10
            // 
            resources.ApplyResources(flowLayoutPanel10, "flowLayoutPanel10");
            flowLayoutPanel10.Controls.Add(label5);
            flowLayoutPanel10.Controls.Add(flowLayoutPanel9);
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip")); // (260531Ch)
            // 
            // flowLayoutPanel9
            // 
            flowLayoutPanel9.Controls.Add(label7);
            flowLayoutPanel9.Controls.Add(trackBarIntensityBrightnessMin);
            flowLayoutPanel9.Controls.Add(label8);
            flowLayoutPanel9.Controls.Add(trackBarIntensityBrightnessMax);
            resources.ApplyResources(flowLayoutPanel9, "flowLayoutPanel9");
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip")); // (260531Ch)
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
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            toolTip.SetToolTip(label8, resources.GetString("label8.ToolTip")); // (260531Ch)
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
            // flowLayoutPanel8
            // 
            resources.ApplyResources(flowLayoutPanel8, "flowLayoutPanel8");
            flowLayoutPanel8.Controls.Add(label1);
            flowLayoutPanel8.Controls.Add(radioButtonAllDisks);
            flowLayoutPanel8.Controls.Add(radioButtonIndividualDisk);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip")); // (260531Ch)
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
            // radioButtonIndividualDisk
            // 
            resources.ApplyResources(radioButtonIndividualDisk, "radioButtonIndividualDisk");
            radioButtonIndividualDisk.Name = "radioButtonIndividualDisk";
            toolTip.SetToolTip(radioButtonIndividualDisk, resources.GetString("radioButtonIndividualDisk.ToolTip"));
            radioButtonIndividualDisk.UseVisualStyleBackColor = true;
            radioButtonIndividualDisk.CheckedChanged += radioButtonAllDisks_CheckedChanged;
            // 
            // flowLayoutPanel7
            // 
            resources.ApplyResources(flowLayoutPanel7, "flowLayoutPanel7");
            flowLayoutPanel7.Controls.Add(label4);
            flowLayoutPanel7.Controls.Add(textBoxThickness);
            flowLayoutPanel7.Controls.Add(label6);
            flowLayoutPanel7.Controls.Add(trackBarOutputThickness);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip")); // (260531Ch)
            // 
            // textBoxThickness
            // 
            resources.ApplyResources(textBoxThickness, "textBoxThickness");
            textBoxThickness.Name = "textBoxThickness";
            textBoxThickness.ReadOnly = true;
            toolTip.SetToolTip(textBoxThickness, resources.GetString("textBoxThickness.ToolTip")); // (260531Ch)
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip")); // (260531Ch)
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
            // statusStrip2
            // 
            statusStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusLabel2, toolStripStatusLabel1 });
            statusStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            resources.ApplyResources(statusStrip2, "statusStrip2");
            statusStrip2.Name = "statusStrip2";
            statusStrip2.SizingGrip = false;
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            resources.ApplyResources(toolStripProgressBar, "toolStripProgressBar");
            toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // toolStripStatusLabel2
            // 
            resources.ApplyResources(toolStripStatusLabel2, "toolStripStatusLabel2");
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // flowLayoutPanel13
            // 
            resources.ApplyResources(flowLayoutPanel13, "flowLayoutPanel13");
            flowLayoutPanel13.Controls.Add(label13);
            flowLayoutPanel13.Controls.Add(comboBoxSolver);
            flowLayoutPanel13.Controls.Add(numericBoxThread);
            flowLayoutPanel13.Name = "flowLayoutPanel13";
            // 
            // FormDiffractionSimulatorCBED
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            ControlBox = false;
            Controls.Add(groupBoxOutput);
            Controls.Add(groupBoxInputParameters);
            Controls.Add(statusStrip2);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormDiffractionSimulatorCBED";
            ShowIcon = false;
            FormClosing += FormDiffractionSimulatorMultislice_FormClosing;
            VisibleChanged += FormDiffractionSimulatorCBED_VisibleChanged;
            groupBoxInputParameters.ResumeLayout(false);
            groupBoxInputParameters.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            groupBoxOutput.ResumeLayout(false);
            groupBoxOutput.PerformLayout();
            flowLayoutPanel12.ResumeLayout(false);
            flowLayoutPanel12.PerformLayout();
            flowLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel11.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarGamma).EndInit();
            flowLayoutPanel10.ResumeLayout(false);
            flowLayoutPanel10.PerformLayout();
            flowLayoutPanel9.ResumeLayout(false);
            flowLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).EndInit();
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).EndInit();
            statusStrip2.ResumeLayout(false);
            statusStrip2.PerformLayout();
            flowLayoutPanel13.ResumeLayout(false);
            flowLayoutPanel13.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button buttonSimulate;
        private Crystallography.Controls.NumericBox numericBoxWholeThicknessStart;
        private System.Windows.Forms.CheckBox checkBoxDrawGuideCircles;
        private Crystallography.Controls.NumericBox numericBoxMaxNumOfG;
        private Crystallography.Controls.NumericBox numericBoxDiskResolution;
        private Crystallography.Controls.NumericBox numericBoxThicknessEnd;
        private Crystallography.Controls.NumericBox numericBoxThicknessStep;
        private System.Windows.Forms.GroupBox groupBoxInputParameters;
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel12;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel11;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel13;
    }
}
