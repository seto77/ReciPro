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
            groupBoxIndexRange = new System.Windows.Forms.GroupBox();
            indexControl = new IndexControl();
            checkBoxIncludingEquivalent = new System.Windows.Forms.CheckBox();
            groupBoxColorAndSize = new System.Windows.Forms.GroupBox();
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
            label17 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            trackBarStrSize = new System.Windows.Forms.TrackBar();
            trackBarPointSize = new System.Windows.Forms.TrackBar();
            numericBoxDrawingArea = new NumericBox();
            checkBoxTiltDirections = new System.Windows.Forms.CheckBox();
            checkBox1DegLine = new System.Windows.Forms.CheckBox();
            checkBoxShowIndexLabels = new System.Windows.Forms.CheckBox();
            label1MousePosition = new System.Windows.Forms.Label();
            groupBoxHolderAngles = new System.Windows.Forms.GroupBox();
            numericBoxArrowStep = new NumericBox();
            label19 = new System.Windows.Forms.Label();
            numericBoxTiltX = new NumericBox();
            label20 = new System.Windows.Forms.Label();
            numericBoxTiltY = new NumericBox();
            checkBoxEnableArrow = new System.Windows.Forms.CheckBox();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            toolTip1.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip1.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip1.InitialDelay = 500; // 260601Cl 追加
            toolTip1.ReshowDelay = 100; // 260601Cl 追加
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            groupBoxTEMSettings.SuspendLayout();
            groupBoxLink.SuspendLayout();
            groupBoxStereonetProperties.SuspendLayout();
            groupBoxIndexRange.SuspendLayout();
            groupBoxColorAndSize.SuspendLayout();
            flowLayoutPanelStereonetColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).BeginInit();
            groupBoxHolderAngles.SuspendLayout();
            SuspendLayout();
            // 
            // graphicsBox
            // 
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(graphicsBox, "graphicsBox");
            graphicsBox.Fonts = new System.Drawing.Font("Segoe UI", 9.75F);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.TabStop = false;
            toolTip1.SetToolTip(graphicsBox, resources.GetString("graphicsBox.ToolTip")); // (260531Ch)
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip1.SetToolTip(label1, resources.GetString("label1.ToolTip")); // (260531Ch)
            // 
            // numericBoxTiltXDirection
            // 
            numericBoxTiltXDirection.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltXDirection.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxTiltXDirection, "numericBoxTiltXDirection");
            numericBoxTiltXDirection.Maximum = 180D;
            numericBoxTiltXDirection.Minimum = -180D;
            numericBoxTiltXDirection.Name = "numericBoxTiltXDirection";
            numericBoxTiltXDirection.RadianValue = -0.50614548307835561D;
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
            numericBoxLinkTiltX.Maximum = 180D;
            numericBoxLinkTiltX.Minimum = -180D;
            numericBoxLinkTiltX.Name = "numericBoxLinkTiltX";
            numericBoxLinkTiltX.ValueFontSize = 9F;
            // 
            // numericBoxLinkTiltY
            // 
            numericBoxLinkTiltY.BackColor = System.Drawing.Color.Transparent;
            numericBoxLinkTiltY.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxLinkTiltY, "numericBoxLinkTiltY");
            numericBoxLinkTiltY.Maximum = 180D;
            numericBoxLinkTiltY.Minimum = -180D;
            numericBoxLinkTiltY.Name = "numericBoxLinkTiltY";
            numericBoxLinkTiltY.ValueFontSize = 9F;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip1.SetToolTip(label4, resources.GetString("label4.ToolTip")); // (260531Ch)
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
            toolTip1.SetToolTip(radioButtonTiltY_Plus, resources.GetString("radioButtonTiltY_Plus.ToolTip")); // (260531Ch)
            radioButtonTiltY_Plus.UseVisualStyleBackColor = true;
            radioButtonTiltY_Plus.CheckedChanged += numericBoxPrimaryAxisDirection_ValueChanged;
            // 
            // radioButtonTiltY_Minus
            // 
            resources.ApplyResources(radioButtonTiltY_Minus, "radioButtonTiltY_Minus");
            radioButtonTiltY_Minus.Name = "radioButtonTiltY_Minus";
            toolTip1.SetToolTip(radioButtonTiltY_Minus, resources.GetString("radioButtonTiltY_Minus.ToolTip")); // (260531Ch)
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
            toolTip1.SetToolTip(buttonRotate180, resources.GetString("buttonRotate180.ToolTip")); // (260531Ch)
            buttonRotate180.UseVisualStyleBackColor = true;
            buttonRotate180.Click += buttonRotate180_Click;
            // 
            // buttonLink
            // 
            resources.ApplyResources(buttonLink, "buttonLink");
            buttonLink.Name = "buttonLink";
            toolTip1.SetToolTip(buttonLink, resources.GetString("buttonLink.ToolTip")); // (260531Ch)
            buttonLink.UseVisualStyleBackColor = true;
            buttonLink.Click += buttonLink_Click;
            // 
            // groupBoxStereonetProperties
            // 
            captureExtender.SetCapture(groupBoxStereonetProperties, true);
            groupBoxStereonetProperties.Controls.Add(groupBoxIndexRange);
            groupBoxStereonetProperties.Controls.Add(groupBoxColorAndSize);
            groupBoxStereonetProperties.Controls.Add(numericBoxDrawingArea);
            groupBoxStereonetProperties.Controls.Add(checkBoxTiltDirections);
            groupBoxStereonetProperties.Controls.Add(checkBox1DegLine);
            groupBoxStereonetProperties.Controls.Add(checkBoxShowIndexLabels);
            resources.ApplyResources(groupBoxStereonetProperties, "groupBoxStereonetProperties");
            groupBoxStereonetProperties.Name = "groupBoxStereonetProperties";
            groupBoxStereonetProperties.TabStop = false;
            // 
            // groupBoxIndexRange
            // 
            groupBoxIndexRange.Controls.Add(indexControl);
            groupBoxIndexRange.Controls.Add(checkBoxIncludingEquivalent);
            resources.ApplyResources(groupBoxIndexRange, "groupBoxIndexRange");
            groupBoxIndexRange.Name = "groupBoxIndexRange";
            groupBoxIndexRange.TabStop = false;
            // 
            // indexControl
            // 
            resources.ApplyResources(indexControl, "indexControl");
            indexControl.Mode = IndexControl.ModeEnum.Axis;
            indexControl.Name = "indexControl";
            indexControl.PlusMinus = true;
            indexControl.UpDownWidth = 16;
            indexControl.Values = ((int, int, int))resources.GetObject("indexControl.Values");
            indexControl.ValueChanged += numericBoxU_ValueChanged;
            // 
            // checkBoxIncludingEquivalent
            // 
            resources.ApplyResources(checkBoxIncludingEquivalent, "checkBoxIncludingEquivalent");
            checkBoxIncludingEquivalent.Checked = true;
            checkBoxIncludingEquivalent.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxIncludingEquivalent.Name = "checkBoxIncludingEquivalent";
            toolTip1.SetToolTip(checkBoxIncludingEquivalent, resources.GetString("checkBoxIncludingEquivalent.ToolTip")); // (260531Ch)
            checkBoxIncludingEquivalent.UseVisualStyleBackColor = true;
            checkBoxIncludingEquivalent.CheckedChanged += checkBoxIncludingEquivalent_CheckedChanged;
            // 
            // groupBoxColorAndSize
            // 
            groupBoxColorAndSize.Controls.Add(flowLayoutPanelStereonetColor);
            groupBoxColorAndSize.Controls.Add(label17);
            groupBoxColorAndSize.Controls.Add(label18);
            groupBoxColorAndSize.Controls.Add(trackBarStrSize);
            groupBoxColorAndSize.Controls.Add(trackBarPointSize);
            resources.ApplyResources(groupBoxColorAndSize, "groupBoxColorAndSize");
            groupBoxColorAndSize.Name = "groupBoxColorAndSize";
            groupBoxColorAndSize.TabStop = false;
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
            colorControlBackGround.Argb = -1;
            resources.ApplyResources(colorControlBackGround, "colorControlBackGround");
            colorControlBackGround.BackColor = System.Drawing.Color.White;
            colorControlBackGround.Blue = 255;
            colorControlBackGround.BlueF = 1F;
            colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlBackGround.Green = 255;
            colorControlBackGround.GreenF = 1F;
            colorControlBackGround.Name = "colorControlBackGround";
            colorControlBackGround.Red = 255;
            colorControlBackGround.RedF = 1F;
            colorControlBackGround.TabStop = false;
            colorControlBackGround.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlHolder
            // 
            colorControlHolder.Argb = -32768;
            resources.ApplyResources(colorControlHolder, "colorControlHolder");
            colorControlHolder.BackColor = System.Drawing.Color.White;
            colorControlHolder.Blue = 0;
            colorControlHolder.BlueF = 0F;
            colorControlHolder.BoxSize = new System.Drawing.Size(20, 20);
            colorControlHolder.Color = System.Drawing.Color.FromArgb(255, 128, 0);
            colorControlHolder.Green = 128;
            colorControlHolder.GreenF = 0.5019608F;
            colorControlHolder.Name = "colorControlHolder";
            colorControlHolder.Red = 255;
            colorControlHolder.RedF = 1F;
            colorControlHolder.TabStop = false;
            colorControlHolder.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl90DegLine
            // 
            colorControl90DegLine.Argb = -16776961;
            resources.ApplyResources(colorControl90DegLine, "colorControl90DegLine");
            colorControl90DegLine.BackColor = System.Drawing.Color.Blue;
            colorControl90DegLine.Blue = 255;
            colorControl90DegLine.BlueF = 1F;
            colorControl90DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl90DegLine.Color = System.Drawing.Color.FromArgb(0, 0, 255);
            colorControl90DegLine.Green = 0;
            colorControl90DegLine.GreenF = 0F;
            colorControl90DegLine.Name = "colorControl90DegLine";
            colorControl90DegLine.Red = 0;
            colorControl90DegLine.RedF = 0F;
            colorControl90DegLine.TabStop = false;
            colorControl90DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl10DegLine
            // 
            colorControl10DegLine.Argb = -8355585;
            resources.ApplyResources(colorControl10DegLine, "colorControl10DegLine");
            colorControl10DegLine.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
            colorControl10DegLine.Blue = 255;
            colorControl10DegLine.BlueF = 1F;
            colorControl10DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl10DegLine.Color = System.Drawing.Color.FromArgb(128, 128, 255);
            colorControl10DegLine.Green = 128;
            colorControl10DegLine.GreenF = 0.5019608F;
            colorControl10DegLine.Name = "colorControl10DegLine";
            colorControl10DegLine.Red = 128;
            colorControl10DegLine.RedF = 0.5019608F;
            colorControl10DegLine.TabStop = false;
            colorControl10DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl1DegLine
            // 
            colorControl1DegLine.Argb = -4144897;
            resources.ApplyResources(colorControl1DegLine, "colorControl1DegLine");
            colorControl1DegLine.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControl1DegLine.Blue = 255;
            colorControl1DegLine.BlueF = 1F;
            colorControl1DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl1DegLine.Color = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControl1DegLine.Green = 192;
            colorControl1DegLine.GreenF = 0.7529412F;
            colorControl1DegLine.Name = "colorControl1DegLine";
            colorControl1DegLine.Red = 192;
            colorControl1DegLine.RedF = 0.7529412F;
            colorControl1DegLine.TabStop = false;
            colorControl1DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlUniqueAxis
            // 
            colorControlUniqueAxis.Argb = -7667712;
            resources.ApplyResources(colorControlUniqueAxis, "colorControlUniqueAxis");
            colorControlUniqueAxis.BackColor = System.Drawing.Color.Red;
            colorControlUniqueAxis.Blue = 0;
            colorControlUniqueAxis.BlueF = 0F;
            colorControlUniqueAxis.BoxSize = new System.Drawing.Size(20, 20);
            colorControlUniqueAxis.Color = System.Drawing.Color.FromArgb(139, 0, 0);
            colorControlUniqueAxis.Green = 0;
            colorControlUniqueAxis.GreenF = 0F;
            colorControlUniqueAxis.Name = "colorControlUniqueAxis";
            colorControlUniqueAxis.Red = 139;
            colorControlUniqueAxis.RedF = 0.545098066F;
            colorControlUniqueAxis.TabStop = false;
            colorControlUniqueAxis.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlGeneralAxis
            // 
            colorControlGeneralAxis.Argb = -65536;
            resources.ApplyResources(colorControlGeneralAxis, "colorControlGeneralAxis");
            colorControlGeneralAxis.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
            colorControlGeneralAxis.Blue = 0;
            colorControlGeneralAxis.BlueF = 0F;
            colorControlGeneralAxis.BoxSize = new System.Drawing.Size(20, 20);
            colorControlGeneralAxis.Color = System.Drawing.Color.FromArgb(255, 0, 0);
            colorControlGeneralAxis.Green = 0;
            colorControlGeneralAxis.GreenF = 0F;
            colorControlGeneralAxis.Name = "colorControlGeneralAxis";
            colorControlGeneralAxis.Red = 255;
            colorControlGeneralAxis.RedF = 1F;
            colorControlGeneralAxis.TabStop = false;
            colorControlGeneralAxis.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlTiltX
            // 
            colorControlTiltX.Argb = -16726016;
            resources.ApplyResources(colorControlTiltX, "colorControlTiltX");
            colorControlTiltX.BackColor = System.Drawing.Color.Lime;
            colorControlTiltX.Blue = 0;
            colorControlTiltX.BlueF = 0F;
            colorControlTiltX.BoxSize = new System.Drawing.Size(20, 20);
            colorControlTiltX.Color = System.Drawing.Color.FromArgb(0, 200, 0);
            colorControlTiltX.Green = 200;
            colorControlTiltX.GreenF = 0.784313738F;
            colorControlTiltX.Name = "colorControlTiltX";
            colorControlTiltX.Red = 0;
            colorControlTiltX.RedF = 0F;
            colorControlTiltX.TabStop = false;
            // 
            // colorControlTiltY
            // 
            colorControlTiltY.Argb = -65281;
            resources.ApplyResources(colorControlTiltY, "colorControlTiltY");
            colorControlTiltY.BackColor = System.Drawing.Color.Lime;
            colorControlTiltY.Blue = 255;
            colorControlTiltY.BlueF = 1F;
            colorControlTiltY.BoxSize = new System.Drawing.Size(20, 20);
            colorControlTiltY.Color = System.Drawing.Color.FromArgb(255, 0, 255);
            colorControlTiltY.Green = 0;
            colorControlTiltY.GreenF = 0F;
            colorControlTiltY.Name = "colorControlTiltY";
            colorControlTiltY.Red = 255;
            colorControlTiltY.RedF = 1F;
            colorControlTiltY.TabStop = false;
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            toolTip1.SetToolTip(label17, resources.GetString("label17.ToolTip")); // (260531Ch)
            // 
            // label18
            // 
            resources.ApplyResources(label18, "label18");
            label18.Name = "label18";
            toolTip1.SetToolTip(label18, resources.GetString("label18.ToolTip")); // (260531Ch)
            // 
            // trackBarStrSize
            // 
            resources.ApplyResources(trackBarStrSize, "trackBarStrSize");
            trackBarStrSize.Maximum = 200;
            trackBarStrSize.Minimum = 1;
            trackBarStrSize.Name = "trackBarStrSize";
            trackBarStrSize.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip1.SetToolTip(trackBarStrSize, resources.GetString("trackBarStrSize.ToolTip")); // (260531Ch)
            trackBarStrSize.Value = 60;
            // 
            // trackBarPointSize
            // 
            resources.ApplyResources(trackBarPointSize, "trackBarPointSize");
            trackBarPointSize.Maximum = 20;
            trackBarPointSize.Minimum = 1;
            trackBarPointSize.Name = "trackBarPointSize";
            trackBarPointSize.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip1.SetToolTip(trackBarPointSize, resources.GetString("trackBarPointSize.ToolTip")); // (260531Ch)
            trackBarPointSize.Value = 4;
            // 
            // numericBoxDrawingArea
            // 
            numericBoxDrawingArea.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxDrawingArea, "numericBoxDrawingArea");
            numericBoxDrawingArea.Maximum = 90D;
            numericBoxDrawingArea.Minimum = 1D;
            numericBoxDrawingArea.Name = "numericBoxDrawingArea";
            numericBoxDrawingArea.RadianValue = 0.52359877559829882D;
            numericBoxDrawingArea.ShowUpDown = true;
            numericBoxDrawingArea.SmartIncrement = true;
            numericBoxDrawingArea.Value = 30D;
            numericBoxDrawingArea.ValueChanged += numericBoxDrawingArea_ValueChanged;
            // 
            // checkBoxTiltDirections
            // 
            resources.ApplyResources(checkBoxTiltDirections, "checkBoxTiltDirections");
            checkBoxTiltDirections.Checked = true;
            checkBoxTiltDirections.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxTiltDirections.Name = "checkBoxTiltDirections";
            toolTip1.SetToolTip(checkBoxTiltDirections, resources.GetString("checkBoxTiltDirections.ToolTip")); // (260531Ch)
            checkBoxTiltDirections.CheckedChanged += checkBox1DegLine_CheckedChanged;
            // 
            // checkBox1DegLine
            // 
            resources.ApplyResources(checkBox1DegLine, "checkBox1DegLine");
            checkBox1DegLine.Checked = true;
            checkBox1DegLine.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox1DegLine.Name = "checkBox1DegLine";
            toolTip1.SetToolTip(checkBox1DegLine, resources.GetString("checkBox1DegLine.ToolTip")); // (260531Ch)
            checkBox1DegLine.CheckedChanged += checkBox1DegLine_CheckedChanged;
            // 
            // checkBoxShowIndexLabels
            // 
            resources.ApplyResources(checkBoxShowIndexLabels, "checkBoxShowIndexLabels");
            checkBoxShowIndexLabels.Checked = true;
            checkBoxShowIndexLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowIndexLabels.Name = "checkBoxShowIndexLabels";
            toolTip1.SetToolTip(checkBoxShowIndexLabels, resources.GetString("checkBoxShowIndexLabels.ToolTip")); // (260531Ch)
            checkBoxShowIndexLabels.UseVisualStyleBackColor = true;
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
            numericBoxArrowStep.Maximum = 2D;
            numericBoxArrowStep.Minimum = 0.1D;
            numericBoxArrowStep.Name = "numericBoxArrowStep";
            numericBoxArrowStep.RadianValue = 0.0034906585039886592D;
            numericBoxArrowStep.ShowUpDown = true;
            numericBoxArrowStep.UpDown_Increment = 0.1D;
            numericBoxArrowStep.Value = 0.2D;
            numericBoxArrowStep.ValueFontSize = 9F;
            numericBoxArrowStep.ValueChanged += numericBoxTilt_ValueChanged;
            // 
            // label19
            // 
            resources.ApplyResources(label19, "label19");
            label19.Name = "label19";
            toolTip1.SetToolTip(label19, resources.GetString("label19.ToolTip")); // (260531Ch)
            // 
            // numericBoxTiltX
            // 
            numericBoxTiltX.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltX.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxTiltX, "numericBoxTiltX");
            numericBoxTiltX.Maximum = 180D;
            numericBoxTiltX.Minimum = -180D;
            numericBoxTiltX.Name = "numericBoxTiltX";
            numericBoxTiltX.ShowUpDown = true;
            numericBoxTiltX.ValueFontSize = 9F;
            numericBoxTiltX.ValueChanged += numericBoxTilt_ValueChanged;
            // 
            // label20
            // 
            resources.ApplyResources(label20, "label20");
            label20.Name = "label20";
            toolTip1.SetToolTip(label20, resources.GetString("label20.ToolTip")); // (260531Ch)
            // 
            // numericBoxTiltY
            // 
            numericBoxTiltY.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltY.DecimalPlaces = 1;
            resources.ApplyResources(numericBoxTiltY, "numericBoxTiltY");
            numericBoxTiltY.Maximum = 180D;
            numericBoxTiltY.Minimum = -180D;
            numericBoxTiltY.Name = "numericBoxTiltY";
            numericBoxTiltY.ShowUpDown = true;
            numericBoxTiltY.ValueFontSize = 9F;
            numericBoxTiltY.ValueChanged += numericBoxTilt_ValueChanged;
            // 
            // checkBoxEnableArrow
            // 
            resources.ApplyResources(checkBoxEnableArrow, "checkBoxEnableArrow");
            checkBoxEnableArrow.Name = "checkBoxEnableArrow";
            toolTip1.SetToolTip(checkBoxEnableArrow, resources.GetString("checkBoxEnableArrow.ToolTip")); // (260531Ch)
            checkBoxEnableArrow.UseVisualStyleBackColor = true;
            checkBoxEnableArrow.CheckedChanged += checkBoxEnableArrow_CheckedChanged;
            // 
            // FormDiffractionSimulatorHolder
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(graphicsBox);
            Controls.Add(groupBoxHolderAngles);
            Controls.Add(groupBoxLink);
            Controls.Add(groupBoxTEMSettings);
            Controls.Add(label1MousePosition);
            Controls.Add(groupBoxStereonetProperties);
            KeyPreview = true;
            Name = "FormDiffractionSimulatorHolder";
            FormClosing += FormDiffractionSimulatorHolder_FormClosing;
            Load += FormDiffractionSimulatorHolder_Load;
            KeyDown += FormDiffractionSimulatorHolder_KeyDown;
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            groupBoxTEMSettings.ResumeLayout(false);
            groupBoxTEMSettings.PerformLayout();
            groupBoxLink.ResumeLayout(false);
            groupBoxLink.PerformLayout();
            groupBoxStereonetProperties.ResumeLayout(false);
            groupBoxStereonetProperties.PerformLayout();
            groupBoxIndexRange.ResumeLayout(false);
            groupBoxIndexRange.PerformLayout();
            groupBoxColorAndSize.ResumeLayout(false);
            groupBoxColorAndSize.PerformLayout();
            flowLayoutPanelStereonetColor.ResumeLayout(false);
            flowLayoutPanelStereonetColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).EndInit();
            groupBoxHolderAngles.ResumeLayout(false);
            groupBoxHolderAngles.PerformLayout();
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
    }
}
