namespace ReciPro
{
    partial class FormDiffractionSimulatorHolder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiffractionSimulatorHolder));
            graphicsBox = new ImagingSolution.Control.GraphicsBox(components);
            label1 = new System.Windows.Forms.Label();
            numericBoxTiltXDirection = new NumericBox();
            numericBoxLinkTiltX = new NumericBox();
            numericBoxLinkTiltY = new NumericBox();
            label4 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            radioButtonTiltY_Plus = new System.Windows.Forms.RadioButton();
            radioButtonTiltY_Minus = new System.Windows.Forms.RadioButton();
            groupBox2 = new System.Windows.Forms.GroupBox();
            buttonRotate180 = new System.Windows.Forms.Button();
            buttonLink = new System.Windows.Forms.Button();
            groupBox5 = new System.Windows.Forms.GroupBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            numericBoxU = new NumericBox();
            label14 = new System.Windows.Forms.Label();
            checkBoxIncludingEquivalent = new System.Windows.Forms.CheckBox();
            label15 = new System.Windows.Forms.Label();
            numericBoxV = new NumericBox();
            label16 = new System.Windows.Forms.Label();
            numericBoxW = new NumericBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
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
            groupBox3 = new System.Windows.Forms.GroupBox();
            numericBoxArrowStep = new NumericBox();
            label19 = new System.Windows.Forms.Label();
            numericBoxTiltX = new NumericBox();
            label20 = new System.Windows.Forms.Label();
            numericBoxTiltY = new NumericBox();
            checkBoxEnableArrow = new System.Windows.Forms.CheckBox();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox4.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // graphicsBox
            // 
            resources.ApplyResources(graphicsBox, "graphicsBox");
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            graphicsBox.Name = "graphicsBox";
            graphicsBox.TabStop = false;
            toolTip1.SetToolTip(graphicsBox, resources.GetString("graphicsBox.ToolTip"));
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip1.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // numericBoxTiltXDirection
            // 
            resources.ApplyResources(numericBoxTiltXDirection, "numericBoxTiltXDirection");
            numericBoxTiltXDirection.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltXDirection.DecimalPlaces = 1;
            numericBoxTiltXDirection.Maximum = 180D;
            numericBoxTiltXDirection.Minimum = -180D;
            numericBoxTiltXDirection.Name = "numericBoxTiltXDirection";
            numericBoxTiltXDirection.RadianValue = -0.50614548307835561D;
            numericBoxTiltXDirection.ShowUpDown = true;
            toolTip1.SetToolTip(numericBoxTiltXDirection, resources.GetString("numericBoxTiltXDirection.ToolTip"));
            numericBoxTiltXDirection.Value = -29D;
            numericBoxTiltXDirection.ValueChanged += numericBoxPrimaryAxisDirection_ValueChanged;
            // 
            // numericBoxLinkTiltX
            // 
            resources.ApplyResources(numericBoxLinkTiltX, "numericBoxLinkTiltX");
            numericBoxLinkTiltX.BackColor = System.Drawing.Color.Transparent;
            numericBoxLinkTiltX.DecimalPlaces = 1;
            numericBoxLinkTiltX.Maximum = 180D;
            numericBoxLinkTiltX.Minimum = -180D;
            numericBoxLinkTiltX.Name = "numericBoxLinkTiltX";
            toolTip1.SetToolTip(numericBoxLinkTiltX, resources.GetString("numericBoxLinkTiltX.ToolTip"));
            // 
            // numericBoxLinkTiltY
            // 
            resources.ApplyResources(numericBoxLinkTiltY, "numericBoxLinkTiltY");
            numericBoxLinkTiltY.BackColor = System.Drawing.Color.Transparent;
            numericBoxLinkTiltY.DecimalPlaces = 1;
            numericBoxLinkTiltY.Maximum = 180D;
            numericBoxLinkTiltY.Minimum = -180D;
            numericBoxLinkTiltY.Name = "numericBoxLinkTiltY";
            toolTip1.SetToolTip(numericBoxLinkTiltY, resources.GetString("numericBoxLinkTiltY.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip1.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(radioButtonTiltY_Plus);
            groupBox1.Controls.Add(radioButtonTiltY_Minus);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(numericBoxTiltXDirection);
            groupBox1.Controls.Add(label4);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            toolTip1.SetToolTip(groupBox1, resources.GetString("groupBox1.ToolTip"));
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
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(buttonRotate180);
            groupBox2.Controls.Add(buttonLink);
            groupBox2.Controls.Add(numericBoxLinkTiltX);
            groupBox2.Controls.Add(numericBoxLinkTiltY);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            toolTip1.SetToolTip(groupBox2, resources.GetString("groupBox2.ToolTip"));
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
            // groupBox5
            // 
            resources.ApplyResources(groupBox5, "groupBox5");
            groupBox5.Controls.Add(groupBox6);
            groupBox5.Controls.Add(groupBox4);
            groupBox5.Controls.Add(numericBoxDrawingArea);
            groupBox5.Controls.Add(checkBoxTiltDirections);
            groupBox5.Controls.Add(checkBox1DegLine);
            groupBox5.Controls.Add(checkBoxShowIndexLabels);
            groupBox5.Name = "groupBox5";
            groupBox5.TabStop = false;
            toolTip1.SetToolTip(groupBox5, resources.GetString("groupBox5.ToolTip"));
            // 
            // groupBox6
            // 
            resources.ApplyResources(groupBox6, "groupBox6");
            groupBox6.Controls.Add(numericBoxU);
            groupBox6.Controls.Add(label14);
            groupBox6.Controls.Add(checkBoxIncludingEquivalent);
            groupBox6.Controls.Add(label15);
            groupBox6.Controls.Add(numericBoxV);
            groupBox6.Controls.Add(label16);
            groupBox6.Controls.Add(numericBoxW);
            groupBox6.Name = "groupBox6";
            groupBox6.TabStop = false;
            toolTip1.SetToolTip(groupBox6, resources.GetString("groupBox6.ToolTip"));
            // 
            // numericBoxU
            // 
            resources.ApplyResources(numericBoxU, "numericBoxU");
            numericBoxU.BackColor = System.Drawing.Color.Transparent;
            numericBoxU.Maximum = 20D;
            numericBoxU.Minimum = 0D;
            numericBoxU.Name = "numericBoxU";
            numericBoxU.RadianValue = 0.034906585039886591D;
            numericBoxU.ShowUpDown = true;
            numericBoxU.SkipEventDuringInput = false;
            numericBoxU.ThonsandsSeparator = true;
            toolTip1.SetToolTip(numericBoxU, resources.GetString("numericBoxU.ToolTip"));
            numericBoxU.Value = 2D;
            numericBoxU.ValueChanged += numericBoxU_ValueChanged;
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            label14.Name = "label14";
            toolTip1.SetToolTip(label14, resources.GetString("label14.ToolTip"));
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
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            label15.Name = "label15";
            toolTip1.SetToolTip(label15, resources.GetString("label15.ToolTip"));
            // 
            // numericBoxV
            // 
            resources.ApplyResources(numericBoxV, "numericBoxV");
            numericBoxV.BackColor = System.Drawing.Color.Transparent;
            numericBoxV.Maximum = 20D;
            numericBoxV.Minimum = 0D;
            numericBoxV.Name = "numericBoxV";
            numericBoxV.RadianValue = 0.034906585039886591D;
            numericBoxV.ShowUpDown = true;
            numericBoxV.SkipEventDuringInput = false;
            numericBoxV.ThonsandsSeparator = true;
            toolTip1.SetToolTip(numericBoxV, resources.GetString("numericBoxV.ToolTip"));
            numericBoxV.Value = 2D;
            numericBoxV.ValueChanged += numericBoxU_ValueChanged;
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            label16.Name = "label16";
            toolTip1.SetToolTip(label16, resources.GetString("label16.ToolTip"));
            // 
            // numericBoxW
            // 
            resources.ApplyResources(numericBoxW, "numericBoxW");
            numericBoxW.BackColor = System.Drawing.Color.Transparent;
            numericBoxW.Maximum = 20D;
            numericBoxW.Minimum = 0D;
            numericBoxW.Name = "numericBoxW";
            numericBoxW.RadianValue = 0.034906585039886591D;
            numericBoxW.ShowUpDown = true;
            numericBoxW.SkipEventDuringInput = false;
            numericBoxW.ThonsandsSeparator = true;
            toolTip1.SetToolTip(numericBoxW, resources.GetString("numericBoxW.ToolTip"));
            numericBoxW.Value = 2D;
            numericBoxW.ReadOnlyChanged += numericBoxU_ValueChanged;
            // 
            // groupBox4
            // 
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Controls.Add(flowLayoutPanel1);
            groupBox4.Controls.Add(label17);
            groupBox4.Controls.Add(label18);
            groupBox4.Controls.Add(trackBarStrSize);
            groupBox4.Controls.Add(trackBarPointSize);
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            toolTip1.SetToolTip(groupBox4, resources.GetString("groupBox4.ToolTip"));
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(colorControlBackGround);
            flowLayoutPanel1.Controls.Add(colorControlHolder);
            flowLayoutPanel1.Controls.Add(colorControl90DegLine);
            flowLayoutPanel1.Controls.Add(colorControl10DegLine);
            flowLayoutPanel1.Controls.Add(colorControl1DegLine);
            flowLayoutPanel1.Controls.Add(colorControlUniqueAxis);
            flowLayoutPanel1.Controls.Add(colorControlGeneralAxis);
            flowLayoutPanel1.Controls.Add(colorControlTiltX);
            flowLayoutPanel1.Controls.Add(colorControlTiltY);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            toolTip1.SetToolTip(flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
            // 
            // colorControlBackGround
            // 
            resources.ApplyResources(colorControlBackGround, "colorControlBackGround");
            colorControlBackGround.Argb = -1;
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
            toolTip1.SetToolTip(colorControlBackGround, resources.GetString("colorControlBackGround.ToolTip1"));
            colorControlBackGround.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlHolder
            // 
            resources.ApplyResources(colorControlHolder, "colorControlHolder");
            colorControlHolder.Argb = -32768;
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
            toolTip1.SetToolTip(colorControlHolder, resources.GetString("colorControlHolder.ToolTip1"));
            colorControlHolder.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl90DegLine
            // 
            resources.ApplyResources(colorControl90DegLine, "colorControl90DegLine");
            colorControl90DegLine.Argb = -16776961;
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
            toolTip1.SetToolTip(colorControl90DegLine, resources.GetString("colorControl90DegLine.ToolTip1"));
            colorControl90DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl10DegLine
            // 
            resources.ApplyResources(colorControl10DegLine, "colorControl10DegLine");
            colorControl10DegLine.Argb = -8355585;
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
            toolTip1.SetToolTip(colorControl10DegLine, resources.GetString("colorControl10DegLine.ToolTip1"));
            colorControl10DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControl1DegLine
            // 
            resources.ApplyResources(colorControl1DegLine, "colorControl1DegLine");
            colorControl1DegLine.Argb = -4144897;
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
            toolTip1.SetToolTip(colorControl1DegLine, resources.GetString("colorControl1DegLine.ToolTip1"));
            colorControl1DegLine.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlUniqueAxis
            // 
            resources.ApplyResources(colorControlUniqueAxis, "colorControlUniqueAxis");
            colorControlUniqueAxis.Argb = -7667712;
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
            toolTip1.SetToolTip(colorControlUniqueAxis, resources.GetString("colorControlUniqueAxis.ToolTip1"));
            colorControlUniqueAxis.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlGeneralAxis
            // 
            resources.ApplyResources(colorControlGeneralAxis, "colorControlGeneralAxis");
            colorControlGeneralAxis.Argb = -65536;
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
            toolTip1.SetToolTip(colorControlGeneralAxis, resources.GetString("colorControlGeneralAxis.ToolTip1"));
            colorControlGeneralAxis.ColorChanged += colorControlUniqueAxis_ColorChanged;
            // 
            // colorControlTiltX
            // 
            resources.ApplyResources(colorControlTiltX, "colorControlTiltX");
            colorControlTiltX.Argb = -16726016;
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
            toolTip1.SetToolTip(colorControlTiltX, resources.GetString("colorControlTiltX.ToolTip1"));
            // 
            // colorControlTiltY
            // 
            resources.ApplyResources(colorControlTiltY, "colorControlTiltY");
            colorControlTiltY.Argb = -65281;
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
            toolTip1.SetToolTip(colorControlTiltY, resources.GetString("colorControlTiltY.ToolTip1"));
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            toolTip1.SetToolTip(label17, resources.GetString("label17.ToolTip"));
            // 
            // label18
            // 
            resources.ApplyResources(label18, "label18");
            label18.Name = "label18";
            toolTip1.SetToolTip(label18, resources.GetString("label18.ToolTip"));
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
            // numericBoxDrawingArea
            // 
            resources.ApplyResources(numericBoxDrawingArea, "numericBoxDrawingArea");
            numericBoxDrawingArea.BackColor = System.Drawing.Color.Transparent;
            numericBoxDrawingArea.Maximum = 90D;
            numericBoxDrawingArea.Minimum = 1D;
            numericBoxDrawingArea.Name = "numericBoxDrawingArea";
            numericBoxDrawingArea.RadianValue = 0.52359877559829882D;
            numericBoxDrawingArea.ShowUpDown = true;
            numericBoxDrawingArea.SmartIncrement = true;
            toolTip1.SetToolTip(numericBoxDrawingArea, resources.GetString("numericBoxDrawingArea.ToolTip"));
            numericBoxDrawingArea.Value = 30D;
            numericBoxDrawingArea.ValueChanged += numericBoxDrawingArea_ValueChanged;
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
            // checkBox1DegLine
            // 
            resources.ApplyResources(checkBox1DegLine, "checkBox1DegLine");
            checkBox1DegLine.Checked = true;
            checkBox1DegLine.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox1DegLine.Name = "checkBox1DegLine";
            toolTip1.SetToolTip(checkBox1DegLine, resources.GetString("checkBox1DegLine.ToolTip"));
            checkBox1DegLine.CheckedChanged += checkBox1DegLine_CheckedChanged;
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
            // label1MousePosition
            // 
            resources.ApplyResources(label1MousePosition, "label1MousePosition");
            label1MousePosition.BackColor = System.Drawing.Color.White;
            label1MousePosition.Name = "label1MousePosition";
            toolTip1.SetToolTip(label1MousePosition, resources.GetString("label1MousePosition.ToolTip"));
            // 
            // groupBox3
            // 
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Controls.Add(numericBoxArrowStep);
            groupBox3.Controls.Add(label19);
            groupBox3.Controls.Add(numericBoxTiltX);
            groupBox3.Controls.Add(label20);
            groupBox3.Controls.Add(numericBoxTiltY);
            groupBox3.Controls.Add(checkBoxEnableArrow);
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            toolTip1.SetToolTip(groupBox3, resources.GetString("groupBox3.ToolTip"));
            // 
            // numericBoxArrowStep
            // 
            resources.ApplyResources(numericBoxArrowStep, "numericBoxArrowStep");
            numericBoxArrowStep.BackColor = System.Drawing.Color.Transparent;
            numericBoxArrowStep.DecimalPlaces = 1;
            numericBoxArrowStep.Maximum = 2D;
            numericBoxArrowStep.Minimum = 0.1D;
            numericBoxArrowStep.Name = "numericBoxArrowStep";
            numericBoxArrowStep.RadianValue = 0.0034906585039886592D;
            numericBoxArrowStep.ShowUpDown = true;
            toolTip1.SetToolTip(numericBoxArrowStep, resources.GetString("numericBoxArrowStep.ToolTip"));
            numericBoxArrowStep.UpDown_Increment = 0.1D;
            numericBoxArrowStep.Value = 0.2D;
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
            resources.ApplyResources(numericBoxTiltX, "numericBoxTiltX");
            numericBoxTiltX.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltX.DecimalPlaces = 1;
            numericBoxTiltX.Maximum = 180D;
            numericBoxTiltX.Minimum = -180D;
            numericBoxTiltX.Name = "numericBoxTiltX";
            numericBoxTiltX.ShowUpDown = true;
            toolTip1.SetToolTip(numericBoxTiltX, resources.GetString("numericBoxTiltX.ToolTip"));
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
            resources.ApplyResources(numericBoxTiltY, "numericBoxTiltY");
            numericBoxTiltY.BackColor = System.Drawing.Color.Transparent;
            numericBoxTiltY.DecimalPlaces = 1;
            numericBoxTiltY.Maximum = 180D;
            numericBoxTiltY.Minimum = -180D;
            numericBoxTiltY.Name = "numericBoxTiltY";
            numericBoxTiltY.ShowUpDown = true;
            toolTip1.SetToolTip(numericBoxTiltY, resources.GetString("numericBoxTiltY.ToolTip"));
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
            // FormDiffractionSimulatorHolder
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(graphicsBox);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label1MousePosition);
            Controls.Add(groupBox5);
            KeyPreview = true;
            Name = "FormDiffractionSimulatorHolder";
            toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            FormClosing += FormDiffractionSimulatorHolder_FormClosing;
            Load += FormDiffractionSimulatorHolder_Load;
            KeyDown += FormDiffractionSimulatorHolder_KeyDown;
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public ImagingSolution.Control.GraphicsBox graphicsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonTiltY_Plus;
        private System.Windows.Forms.RadioButton radioButtonTiltY_Minus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonLink;
        private System.Windows.Forms.GroupBox groupBox5;
        private NumericBox numericBoxLinkTiltX;
        private NumericBox numericBoxLinkTiltY;
        private NumericBox numericBoxV;
        private NumericBox numericBoxW;
        private NumericBox numericBoxU;
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


        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox checkBoxTiltDirections;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1MousePosition;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TrackBar trackBarPointSize;
        private System.Windows.Forms.TrackBar trackBarStrSize;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonRotate180;
        private System.Windows.Forms.CheckBox checkBoxEnableArrow;
        private NumericBox numericBoxArrowStep;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBoxIncludingEquivalent;
        private System.Windows.Forms.GroupBox groupBox6;
    }
}