namespace ReciPro
{
    partial class FormDiffractionSimulatorHolder
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
        // groupBox1 -> groupBoxTEMSettings
        // groupBox2 -> groupBoxLink
        // groupBox3 -> groupBoxHolderAngles
        // groupBox4 -> groupBoxColorAndSize
        // groupBox5 -> groupBoxStereonetProperties
        // groupBox6 -> groupBoxIndexRange
        // flowLayoutPanel1 -> flowLayoutPanelStereonetColor
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiffractionSimulatorHolder));
            graphicsBox = new GraphicsBox(components);
            label1 = new System.Windows.Forms.Label();
            numericBoxTiltXDirection = new NumericBox();
            numericBoxLinkTiltX = new NumericBox();
            numericBoxLinkTiltY = new NumericBox();
            label4 = new System.Windows.Forms.Label();
            groupBoxTEMSettings = new System.Windows.Forms.GroupBox();
            radioButtonTiltY_Plus = new System.Windows.Forms.RadioButton();
            radioButtonTiltY_Minus = new System.Windows.Forms.RadioButton();
            groupBoxLink = new System.Windows.Forms.GroupBox();
            buttonRotate180 = new System.Windows.Forms.Button();
            buttonLink = new System.Windows.Forms.Button();
            groupBoxStereonetProperties = new System.Windows.Forms.GroupBox();
            groupBoxColorAndSize = new System.Windows.Forms.GroupBox();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            label18 = new System.Windows.Forms.Label();
            trackBarPointSize = new System.Windows.Forms.TrackBar();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            label17 = new System.Windows.Forms.Label();
            trackBarStrSize = new System.Windows.Forms.TrackBar();
            flowLayoutPanelStereonetColor = new System.Windows.Forms.FlowLayoutPanel();
            colorControlBackGround = new ColorControl();
            colorControlHolder = new ColorControl();
            colorControl90DegLine = new ColorControl();
            colorControl10DegLine = new ColorControl();
            colorControl1DegLine = new ColorControl();
            colorControlUniqueAxis = new ColorControl();
            colorControlGeneralAxis = new ColorControl();
            colorControlTiltX = new ColorControl();
            colorControlTiltY = new ColorControl();
            checkBox1DegLine = new System.Windows.Forms.CheckBox();
            checkBoxTiltDirections = new System.Windows.Forms.CheckBox();
            checkBoxShowIndexLabels = new System.Windows.Forms.CheckBox();
            groupBoxIndexRange = new System.Windows.Forms.GroupBox();
            checkBoxIncludingEquivalent = new System.Windows.Forms.CheckBox();
            indexControl = new IndexControl();
            numericBoxDrawingArea = new NumericBox();
            label1MousePosition = new System.Windows.Forms.Label();
            groupBoxHolderAngles = new System.Windows.Forms.GroupBox();
            numericBoxArrowStep = new NumericBox();
            label19 = new System.Windows.Forms.Label();
            numericBoxTiltX = new NumericBox();
            label20 = new System.Windows.Forms.Label();
            numericBoxTiltY = new NumericBox();
            checkBoxEnableArrow = new System.Windows.Forms.CheckBox();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            panel1 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            groupBoxTEMSettings.SuspendLayout();
            groupBoxLink.SuspendLayout();
            groupBoxStereonetProperties.SuspendLayout();
            groupBoxColorAndSize.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            flowLayoutPanelStereonetColor.SuspendLayout();
            groupBoxIndexRange.SuspendLayout();
            groupBoxHolderAngles.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // graphicsBox
            // 
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(graphicsBox, "graphicsBox");
            graphicsBox.Fonts = new System.Drawing.Font("Segoe UI", 9.75F);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.TabStop = false;
            toolTip1.SetToolTip(graphicsBox, resources.GetString("graphicsBox.ToolTip"));
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            graphicsBox.Resize += graphicsBox_Resize;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip1.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // numericBoxTiltXDirection
            // 
            numericBoxTiltXDirection.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltXDirection.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxTiltXDirection, "numericBoxTiltXDirection");
            toolTip1.SetToolTip(numericBoxTiltXDirection, resources.GetString("numericBoxTiltXDirection.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            numericBoxTiltXDirection.Maximum = 180D;
            numericBoxTiltXDirection.Minimum = -180D;
            numericBoxTiltXDirection.Name = "numericBoxTiltXDirection";
            numericBoxTiltXDirection.ShowUpDown = true;
            numericBoxTiltXDirection.Value = -29D;
            numericBoxTiltXDirection.ValueFontSize = 9F;
            numericBoxTiltXDirection.ValueChanged += numericBoxPrimaryAxisDirection_ValueChanged;
            // 
            // numericBoxLinkTiltX
            // 
            numericBoxLinkTiltX.BackColor = System.Drawing.Color.Transparent;
            numericBoxLinkTiltX.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxLinkTiltX, "numericBoxLinkTiltX");
            toolTip1.SetToolTip(numericBoxLinkTiltX, resources.GetString("numericBoxLinkTiltX.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            numericBoxLinkTiltX.Maximum = 180D;
            numericBoxLinkTiltX.Minimum = -180D;
            numericBoxLinkTiltX.Name = "numericBoxLinkTiltX";
            numericBoxLinkTiltX.ValueBoxWidth = 30;
            numericBoxLinkTiltX.ValueFontSize = 9F;
            // 
            // numericBoxLinkTiltY
            // 
            numericBoxLinkTiltY.BackColor = System.Drawing.Color.Transparent;
            numericBoxLinkTiltY.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxLinkTiltY, "numericBoxLinkTiltY");
            toolTip1.SetToolTip(numericBoxLinkTiltY, resources.GetString("numericBoxLinkTiltY.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            numericBoxLinkTiltY.Maximum = 180D;
            numericBoxLinkTiltY.Minimum = -180D;
            numericBoxLinkTiltY.Name = "numericBoxLinkTiltY";
            numericBoxLinkTiltY.ValueBoxWidth = 30;
            numericBoxLinkTiltY.ValueFontSize = 9F;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip1.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // groupBoxTEMSettings
            // 
            captureExtender.SetCapture(groupBoxTEMSettings, true);
            groupBoxTEMSettings.Controls.Add(radioButtonTiltY_Plus);
            groupBoxTEMSettings.Controls.Add(radioButtonTiltY_Minus);
            groupBoxTEMSettings.Controls.Add(label1);
            groupBoxTEMSettings.Controls.Add(numericBoxTiltXDirection);
            groupBoxTEMSettings.Controls.Add(label4);
            resources.ApplyResources(groupBoxTEMSettings, "groupBoxTEMSettings");
            groupBoxTEMSettings.Name = "groupBoxTEMSettings";
            groupBoxTEMSettings.TabStop = false;
            // 
            // radioButtonTiltY_Plus
            // 
            resources.ApplyResources(radioButtonTiltY_Plus, "radioButtonTiltY_Plus");
            radioButtonTiltY_Plus.Checked = true;
            radioButtonTiltY_Plus.Name = "radioButtonTiltY_Plus";
            radioButtonTiltY_Plus.TabStop = true;
            toolTip1.SetToolTip(radioButtonTiltY_Plus, resources.GetString("radioButtonTiltY_Plus.ToolTip"));
            radioButtonTiltY_Plus.UseVisualStyleBackColor = true;
            radioButtonTiltY_Plus.CheckedChanged += numericBoxPrimaryAxisDirection_ValueChanged;
            // 
            // radioButtonTiltY_Minus
            // 
            resources.ApplyResources(radioButtonTiltY_Minus, "radioButtonTiltY_Minus");
            radioButtonTiltY_Minus.Name = "radioButtonTiltY_Minus";
            toolTip1.SetToolTip(radioButtonTiltY_Minus, resources.GetString("radioButtonTiltY_Minus.ToolTip"));
            radioButtonTiltY_Minus.UseVisualStyleBackColor = true;
            // 
            // groupBoxLink
            // 
            captureExtender.SetCapture(groupBoxLink, true);
            groupBoxLink.Controls.Add(buttonRotate180);
            groupBoxLink.Controls.Add(buttonLink);
            groupBoxLink.Controls.Add(numericBoxLinkTiltX);
            groupBoxLink.Controls.Add(numericBoxLinkTiltY);
            resources.ApplyResources(groupBoxLink, "groupBoxLink");
            groupBoxLink.Name = "groupBoxLink";
            groupBoxLink.TabStop = false;
            // 
            // buttonRotate180
            // 
            resources.ApplyResources(buttonRotate180, "buttonRotate180");
            buttonRotate180.Name = "buttonRotate180";
            toolTip1.SetToolTip(buttonRotate180, resources.GetString("buttonRotate180.ToolTip"));
            buttonRotate180.UseVisualStyleBackColor = true;
            buttonRotate180.Click += buttonRotate180_Click;
            // 
            // buttonLink
            // 
            resources.ApplyResources(buttonLink, "buttonLink");
            buttonLink.Name = "buttonLink";
            toolTip1.SetToolTip(buttonLink, resources.GetString("buttonLink.ToolTip"));
            buttonLink.UseVisualStyleBackColor = true;
            buttonLink.Click += buttonLink_Click;
            // 
            // groupBoxStereonetProperties
            // 
            captureExtender.SetCapture(groupBoxStereonetProperties, true);
            groupBoxStereonetProperties.Controls.Add(groupBoxColorAndSize);
            groupBoxStereonetProperties.Controls.Add(checkBox1DegLine);
            groupBoxStereonetProperties.Controls.Add(checkBoxTiltDirections);
            groupBoxStereonetProperties.Controls.Add(checkBoxShowIndexLabels);
            groupBoxStereonetProperties.Controls.Add(groupBoxIndexRange);
            groupBoxStereonetProperties.Controls.Add(numericBoxDrawingArea);
            resources.ApplyResources(groupBoxStereonetProperties, "groupBoxStereonetProperties");
            groupBoxStereonetProperties.Name = "groupBoxStereonetProperties";
            groupBoxStereonetProperties.TabStop = false;
            // 
            // groupBoxColorAndSize
            // 
            resources.ApplyResources(groupBoxColorAndSize, "groupBoxColorAndSize");
            groupBoxColorAndSize.Controls.Add(flowLayoutPanel2);
            groupBoxColorAndSize.Controls.Add(flowLayoutPanel1);
            groupBoxColorAndSize.Controls.Add(flowLayoutPanelStereonetColor);
            groupBoxColorAndSize.Name = "groupBoxColorAndSize";
            groupBoxColorAndSize.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(label18);
            flowLayoutPanel2.Controls.Add(trackBarPointSize);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // label18
            // 
            resources.ApplyResources(label18, "label18");
            label18.Name = "label18";
            toolTip1.SetToolTip(label18, resources.GetString("label18.ToolTip"));
            // 
            // trackBarPointSize
            // 
            resources.ApplyResources(trackBarPointSize, "trackBarPointSize");
            trackBarPointSize.Maximum = 20;
            trackBarPointSize.Minimum = 1;
            trackBarPointSize.Name = "trackBarPointSize";
            trackBarPointSize.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip1.SetToolTip(trackBarPointSize, resources.GetString("trackBarPointSize.ToolTip"));
            trackBarPointSize.Value = 4;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(label17);
            flowLayoutPanel1.Controls.Add(trackBarStrSize);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            toolTip1.SetToolTip(label17, resources.GetString("label17.ToolTip"));
            // 
            // trackBarStrSize
            // 
            resources.ApplyResources(trackBarStrSize, "trackBarStrSize");
            trackBarStrSize.Maximum = 200;
            trackBarStrSize.Minimum = 1;
            trackBarStrSize.Name = "trackBarStrSize";
            trackBarStrSize.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip1.SetToolTip(trackBarStrSize, resources.GetString("trackBarStrSize.ToolTip"));
            trackBarStrSize.Value = 60;
            // 
            // flowLayoutPanelStereonetColor
            // 
            resources.ApplyResources(flowLayoutPanelStereonetColor, "flowLayoutPanelStereonetColor");
            flowLayoutPanelStereonetColor.Controls.Add(colorControlBackGround);
            flowLayoutPanelStereonetColor.Controls.Add(colorControlHolder);
            flowLayoutPanelStereonetColor.Controls.Add(colorControl90DegLine);
            flowLayoutPanelStereonetColor.Controls.Add(colorControl10DegLine);
            flowLayoutPanelStereonetColor.Controls.Add(colorControl1DegLine);
            flowLayoutPanelStereonetColor.Controls.Add(colorControlUniqueAxis);
            flowLayoutPanelStereonetColor.Controls.Add(colorControlGeneralAxis);
            flowLayoutPanelStereonetColor.Controls.Add(colorControlTiltX);
            flowLayoutPanelStereonetColor.Controls.Add(colorControlTiltY);
            flowLayoutPanelStereonetColor.Name = "flowLayoutPanelStereonetColor";
            // 
            // colorControlBackGround
            // 
            resources.ApplyResources(colorControlBackGround, "colorControlBackGround");
            toolTip1.SetToolTip(colorControlBackGround, resources.GetString("colorControlBackGround.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            colorControlBackGround.BackColor = System.Drawing.Color.White;
            colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlBackGround.Name = "colorControlBackGround";
            colorControlBackGround.TabStop = false;
            colorControlBackGround.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlHolder
            // 
            resources.ApplyResources(colorControlHolder, "colorControlHolder");
            toolTip1.SetToolTip(colorControlHolder, resources.GetString("colorControlHolder.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            colorControlHolder.BackColor = System.Drawing.Color.White;
            colorControlHolder.BoxSize = new System.Drawing.Size(20, 20);
            colorControlHolder.Color = System.Drawing.Color.FromArgb(255, 128, 0);
            colorControlHolder.Name = "colorControlHolder";
            colorControlHolder.TabStop = false;
            colorControlHolder.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl90DegLine
            // 
            resources.ApplyResources(colorControl90DegLine, "colorControl90DegLine");
            toolTip1.SetToolTip(colorControl90DegLine, resources.GetString("colorControl90DegLine.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            colorControl90DegLine.BackColor = System.Drawing.Color.Blue;
            colorControl90DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl90DegLine.Color = System.Drawing.Color.FromArgb(0, 0, 255);
            colorControl90DegLine.Name = "colorControl90DegLine";
            colorControl90DegLine.TabStop = false;
            colorControl90DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl10DegLine
            // 
            resources.ApplyResources(colorControl10DegLine, "colorControl10DegLine");
            toolTip1.SetToolTip(colorControl10DegLine, resources.GetString("colorControl10DegLine.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            colorControl10DegLine.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
            colorControl10DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl10DegLine.Color = System.Drawing.Color.FromArgb(128, 128, 255);
            colorControl10DegLine.Name = "colorControl10DegLine";
            colorControl10DegLine.TabStop = false;
            colorControl10DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl1DegLine
            // 
            resources.ApplyResources(colorControl1DegLine, "colorControl1DegLine");
            toolTip1.SetToolTip(colorControl1DegLine, resources.GetString("colorControl1DegLine.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            colorControl1DegLine.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControl1DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl1DegLine.Color = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControl1DegLine.Name = "colorControl1DegLine";
            colorControl1DegLine.TabStop = false;
            colorControl1DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlUniqueAxis
            // 
            resources.ApplyResources(colorControlUniqueAxis, "colorControlUniqueAxis");
            toolTip1.SetToolTip(colorControlUniqueAxis, resources.GetString("colorControlUniqueAxis.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            colorControlUniqueAxis.BackColor = System.Drawing.Color.Red;
            colorControlUniqueAxis.BoxSize = new System.Drawing.Size(20, 20);
            colorControlUniqueAxis.Color = System.Drawing.Color.FromArgb(139, 0, 0);
            colorControlUniqueAxis.Name = "colorControlUniqueAxis";
            colorControlUniqueAxis.TabStop = false;
            colorControlUniqueAxis.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlGeneralAxis
            // 
            resources.ApplyResources(colorControlGeneralAxis, "colorControlGeneralAxis");
            toolTip1.SetToolTip(colorControlGeneralAxis, resources.GetString("colorControlGeneralAxis.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            colorControlGeneralAxis.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
            colorControlGeneralAxis.BoxSize = new System.Drawing.Size(20, 20);
            colorControlGeneralAxis.Color = System.Drawing.Color.FromArgb(255, 0, 0);
            colorControlGeneralAxis.Name = "colorControlGeneralAxis";
            colorControlGeneralAxis.TabStop = false;
            colorControlGeneralAxis.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlTiltX
            // 
            resources.ApplyResources(colorControlTiltX, "colorControlTiltX");
            toolTip1.SetToolTip(colorControlTiltX, resources.GetString("colorControlTiltX.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            colorControlTiltX.BackColor = System.Drawing.Color.Lime;
            colorControlTiltX.BoxSize = new System.Drawing.Size(20, 20);
            colorControlTiltX.Color = System.Drawing.Color.FromArgb(0, 200, 0);
            colorControlTiltX.Name = "colorControlTiltX";
            colorControlTiltX.TabStop = false;
            // 
            // colorControlTiltY
            // 
            resources.ApplyResources(colorControlTiltY, "colorControlTiltY");
            toolTip1.SetToolTip(colorControlTiltY, resources.GetString("colorControlTiltY.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            colorControlTiltY.BackColor = System.Drawing.Color.Lime;
            colorControlTiltY.BoxSize = new System.Drawing.Size(20, 20);
            colorControlTiltY.Color = System.Drawing.Color.FromArgb(255, 0, 255);
            colorControlTiltY.Name = "colorControlTiltY";
            colorControlTiltY.TabStop = false;
            // 
            // checkBox1DegLine
            // 
            resources.ApplyResources(checkBox1DegLine, "checkBox1DegLine");
            checkBox1DegLine.Checked = true;
            checkBox1DegLine.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox1DegLine.Name = "checkBox1DegLine";
            toolTip1.SetToolTip(checkBox1DegLine, resources.GetString("checkBox1DegLine.ToolTip"));
            checkBox1DegLine.CheckedChanged += checkBox1DegLine_CheckedChanged;
            // 
            // checkBoxTiltDirections
            // 
            resources.ApplyResources(checkBoxTiltDirections, "checkBoxTiltDirections");
            checkBoxTiltDirections.Checked = true;
            checkBoxTiltDirections.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxTiltDirections.Name = "checkBoxTiltDirections";
            toolTip1.SetToolTip(checkBoxTiltDirections, resources.GetString("checkBoxTiltDirections.ToolTip"));
            checkBoxTiltDirections.CheckedChanged += checkBox1DegLine_CheckedChanged;
            // 
            // checkBoxShowIndexLabels
            // 
            resources.ApplyResources(checkBoxShowIndexLabels, "checkBoxShowIndexLabels");
            checkBoxShowIndexLabels.Checked = true;
            checkBoxShowIndexLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowIndexLabels.Name = "checkBoxShowIndexLabels";
            toolTip1.SetToolTip(checkBoxShowIndexLabels, resources.GetString("checkBoxShowIndexLabels.ToolTip"));
            checkBoxShowIndexLabels.UseVisualStyleBackColor = true;
            // 
            // groupBoxIndexRange
            // 
            groupBoxIndexRange.Controls.Add(checkBoxIncludingEquivalent);
            groupBoxIndexRange.Controls.Add(indexControl);
            resources.ApplyResources(groupBoxIndexRange, "groupBoxIndexRange");
            groupBoxIndexRange.Name = "groupBoxIndexRange";
            groupBoxIndexRange.TabStop = false;
            // 
            // checkBoxIncludingEquivalent
            // 
            resources.ApplyResources(checkBoxIncludingEquivalent, "checkBoxIncludingEquivalent");
            checkBoxIncludingEquivalent.Checked = true;
            checkBoxIncludingEquivalent.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxIncludingEquivalent.Name = "checkBoxIncludingEquivalent";
            toolTip1.SetToolTip(checkBoxIncludingEquivalent, resources.GetString("checkBoxIncludingEquivalent.ToolTip"));
            checkBoxIncludingEquivalent.UseVisualStyleBackColor = true;
            checkBoxIncludingEquivalent.CheckedChanged += checkBoxIncludingEquivalent_CheckedChanged;
            // 
            // indexControl
            // 
            resources.ApplyResources(indexControl, "indexControl");
            indexControl.Mode = IndexControl.ModeEnum.Axis;
            indexControl.Name = "indexControl";
            indexControl.PlusMinus = true;
            toolTip1.SetToolTip(indexControl, resources.GetString("indexControl.ToolTip"));
            indexControl.UpDownWidth = 16;
            indexControl.Values = ((int, int, int))resources.GetObject("indexControl.Values");
            indexControl.ValueChanged += numericBoxU_ValueChanged;
            // 
            // numericBoxDrawingArea
            // 
            numericBoxDrawingArea.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxDrawingArea, "numericBoxDrawingArea");
            toolTip1.SetToolTip(numericBoxDrawingArea, resources.GetString("numericBoxDrawingArea.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            numericBoxDrawingArea.Maximum = 90D;
            numericBoxDrawingArea.Minimum = 1D;
            numericBoxDrawingArea.Name = "numericBoxDrawingArea";
            numericBoxDrawingArea.ShowUpDown = true;
            numericBoxDrawingArea.SmartIncrement = true;
            numericBoxDrawingArea.Value = 30D;
            numericBoxDrawingArea.ValueBoxWidth = 45;
            numericBoxDrawingArea.ValueChanged += numericBoxDrawingArea_ValueChanged;
            // 
            // label1MousePosition
            // 
            resources.ApplyResources(label1MousePosition, "label1MousePosition");
            label1MousePosition.BackColor = System.Drawing.Color.White;
            label1MousePosition.Name = "label1MousePosition";
            // 
            // groupBoxHolderAngles
            // 
            captureExtender.SetCapture(groupBoxHolderAngles, true);
            groupBoxHolderAngles.Controls.Add(numericBoxArrowStep);
            groupBoxHolderAngles.Controls.Add(label19);
            groupBoxHolderAngles.Controls.Add(numericBoxTiltX);
            groupBoxHolderAngles.Controls.Add(label20);
            groupBoxHolderAngles.Controls.Add(numericBoxTiltY);
            groupBoxHolderAngles.Controls.Add(checkBoxEnableArrow);
            resources.ApplyResources(groupBoxHolderAngles, "groupBoxHolderAngles");
            groupBoxHolderAngles.Name = "groupBoxHolderAngles";
            groupBoxHolderAngles.TabStop = false;
            // 
            // numericBoxArrowStep
            // 
            numericBoxArrowStep.BackColor = System.Drawing.Color.Transparent;
            numericBoxArrowStep.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxArrowStep, "numericBoxArrowStep");
            toolTip1.SetToolTip(numericBoxArrowStep, resources.GetString("numericBoxArrowStep.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            numericBoxArrowStep.Maximum = 2D;
            numericBoxArrowStep.Minimum = 0.1D;
            numericBoxArrowStep.Name = "numericBoxArrowStep";
            numericBoxArrowStep.ShowUpDown = true;
            numericBoxArrowStep.UpDown_Increment = 0.1D;
            numericBoxArrowStep.Value = 0.2D;
            numericBoxArrowStep.ValueBoxWidth = 24;
            numericBoxArrowStep.ValueFontSize = 9F;
            numericBoxArrowStep.ValueChanged += numericBoxTilt_ValueChanged;
            // 
            // label19
            // 
            resources.ApplyResources(label19, "label19");
            label19.Name = "label19";
            toolTip1.SetToolTip(label19, resources.GetString("label19.ToolTip"));
            // 
            // numericBoxTiltX
            // 
            numericBoxTiltX.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltX.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxTiltX, "numericBoxTiltX");
            toolTip1.SetToolTip(numericBoxTiltX, resources.GetString("numericBoxTiltX.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            numericBoxTiltX.Maximum = 180D;
            numericBoxTiltX.Minimum = -180D;
            numericBoxTiltX.Name = "numericBoxTiltX";
            numericBoxTiltX.ShowUpDown = true;
            numericBoxTiltX.ValueBoxWidth = 36;
            numericBoxTiltX.ValueFontSize = 9F;
            numericBoxTiltX.ValueChanged += numericBoxTilt_ValueChanged;
            // 
            // label20
            // 
            resources.ApplyResources(label20, "label20");
            label20.Name = "label20";
            toolTip1.SetToolTip(label20, resources.GetString("label20.ToolTip"));
            // 
            // numericBoxTiltY
            // 
            numericBoxTiltY.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltY.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxTiltY, "numericBoxTiltY");
            toolTip1.SetToolTip(numericBoxTiltY, resources.GetString("numericBoxTiltY.ToolTip")); // 260903Cl 追加: 文案だけあって配線が無く、Designer 再生成で resx から消えたので配線して保持
            numericBoxTiltY.Maximum = 180D;
            numericBoxTiltY.Minimum = -180D;
            numericBoxTiltY.Name = "numericBoxTiltY";
            numericBoxTiltY.ShowUpDown = true;
            numericBoxTiltY.ValueBoxWidth = 36;
            numericBoxTiltY.ValueFontSize = 9F;
            numericBoxTiltY.ValueChanged += numericBoxTilt_ValueChanged;
            // 
            // checkBoxEnableArrow
            // 
            resources.ApplyResources(checkBoxEnableArrow, "checkBoxEnableArrow");
            checkBoxEnableArrow.Name = "checkBoxEnableArrow";
            toolTip1.SetToolTip(checkBoxEnableArrow, resources.GetString("checkBoxEnableArrow.ToolTip"));
            checkBoxEnableArrow.UseVisualStyleBackColor = true;
            checkBoxEnableArrow.CheckedChanged += checkBoxEnableArrow_CheckedChanged;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 10000;
            toolTip1.InitialDelay = 500;
            toolTip1.IsBalloon = true;
            toolTip1.ReshowDelay = 100;
            // 
            // panel1
            // 
            panel1.Controls.Add(groupBoxTEMSettings);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(groupBoxLink);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(groupBoxHolderAngles);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // panel3
            // 
            resources.ApplyResources(panel3, "panel3");
            panel3.Name = "panel3";
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // FormDiffractionSimulatorHolder
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(graphicsBox);
            Controls.Add(groupBoxStereonetProperties);
            Controls.Add(panel1);
            Controls.Add(label1MousePosition);
            KeyPreview = true;
            Name = "FormDiffractionSimulatorHolder";
            FormClosing += FormDiffractionSimulatorHolder_FormClosing;
            Load += FormDiffractionSimulatorHolder_Load;
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            groupBoxTEMSettings.ResumeLayout(false);
            groupBoxTEMSettings.PerformLayout();
            groupBoxLink.ResumeLayout(false);
            groupBoxLink.PerformLayout();
            groupBoxStereonetProperties.ResumeLayout(false);
            groupBoxStereonetProperties.PerformLayout();
            groupBoxColorAndSize.ResumeLayout(false);
            groupBoxColorAndSize.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            flowLayoutPanelStereonetColor.ResumeLayout(false);
            flowLayoutPanelStereonetColor.PerformLayout();
            groupBoxIndexRange.ResumeLayout(false);
            groupBoxIndexRange.PerformLayout();
            groupBoxHolderAngles.ResumeLayout(false);
            groupBoxHolderAngles.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        // public ImagingSolution.Control.GraphicsBox graphicsBox; // (260322Ch) 旧 GraphicsBox 型
        // public Crystallography.Controls.GraphicBox2 graphicsBox; // (260322Ch) 仮名 GraphicBox2
        public Crystallography.Controls.GraphicsBox graphicsBox; // (260322Ch) 正式名 GraphicBox へ移行
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxTEMSettings;
        private System.Windows.Forms.RadioButton radioButtonTiltY_Plus;
        private System.Windows.Forms.RadioButton radioButtonTiltY_Minus;
        private System.Windows.Forms.GroupBox groupBoxLink;
        private System.Windows.Forms.Button buttonLink;
        private System.Windows.Forms.GroupBox groupBoxStereonetProperties;
        private NumericBox numericBoxLinkTiltX;
        private NumericBox numericBoxLinkTiltY;
        private NumericBox numericBoxDrawingArea;
        private NumericBox numericBoxTiltXDirection;
        private NumericBox numericBoxTiltX;
        private NumericBox numericBoxTiltY;
        private System.Windows.Forms.CheckBox checkBoxShowIndexLabels;
        private System.Windows.Forms.CheckBox checkBox1DegLine;

        public ColorControl colorControlUniqueAxis;
        public ColorControl colorControlGeneralAxis;
        public ColorControl colorControlBackGround;
        public ColorControl colorControl10DegLine;
        public ColorControl colorControl1DegLine;
        public ColorControl colorControl90DegLine;
        public ColorControl colorControlTiltX;
        public ColorControl colorControlTiltY;
        public ColorControl colorControlHolder;
        private System.Windows.Forms.CheckBox checkBoxTiltDirections;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelStereonetColor;
        private System.Windows.Forms.Label label1MousePosition;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TrackBar trackBarPointSize;
        private System.Windows.Forms.TrackBar trackBarStrSize;
        private System.Windows.Forms.GroupBox groupBoxHolderAngles;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonRotate180;
        private System.Windows.Forms.CheckBox checkBoxEnableArrow;
        private NumericBox numericBoxArrowStep;
        private System.Windows.Forms.GroupBox groupBoxColorAndSize;
        private System.Windows.Forms.CheckBox checkBoxIncludingEquivalent;
        private System.Windows.Forms.GroupBox groupBoxIndexRange;
        private IndexControl indexControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
    }
}
