namespace ReciPro
{
    partial class FormRotationMatrix
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
        // groupBox1 -> groupBoxReciProCoordinate
        // groupBox2 -> groupBoxExperimentalCoordinate
        // flowLayoutPanel1 -> flowLayoutPanelFirstAxis
        // flowLayoutPanel2 -> flowLayoutPanelSecondAxis
        // flowLayoutPanel3 -> flowLayoutPanelThirdAxis
        // panel2 -> panelViewOptions
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRotationMatrix));
            toolTip = new System.Windows.Forms.ToolTip(components);
            buttonViewAlongBeam = new System.Windows.Forms.Button();
            buttonViewIsometric = new System.Windows.Forms.Button();
            checkBoxLink = new System.Windows.Forms.CheckBox();
            buttonCopy = new System.Windows.Forms.Button();
            buttonPaste = new System.Windows.Forms.Button();
            numericBoxPsi = new NumericBox();
            numericBoxTheta = new NumericBox();
            numericBoxPhi = new NumericBox();
            numericBoxExp1 = new NumericBox();
            numericBoxExp2 = new NumericBox();
            numericBoxExp3 = new NumericBox();
            checkBoxEnable2nd = new System.Windows.Forms.CheckBox();
            checkBoxFix1st = new System.Windows.Forms.CheckBox();
            checkBoxFix2nd = new System.Windows.Forms.CheckBox();
            checkBoxFix3rd = new System.Windows.Forms.CheckBox();
            checkBoxEnable3rd = new System.Windows.Forms.CheckBox();
            radioButton3rdZ = new System.Windows.Forms.RadioButton();
            radioButton3rdY = new System.Windows.Forms.RadioButton();
            radioButton3rdX = new System.Windows.Forms.RadioButton();
            radioButton2ndZ = new System.Windows.Forms.RadioButton();
            radioButton2ndY = new System.Windows.Forms.RadioButton();
            radioButton2ndX = new System.Windows.Forms.RadioButton();
            radioButton1stZ = new System.Windows.Forms.RadioButton();
            radioButton1stY = new System.Windows.Forms.RadioButton();
            radioButton1stX = new System.Windows.Forms.RadioButton();
            buttonResetExpEuler = new System.Windows.Forms.Button();
            numericBox11 = new NumericBox();
            numericBox12 = new NumericBox();
            numericBox13 = new NumericBox();
            numericBox21 = new NumericBox();
            numericBox22 = new NumericBox();
            numericBox33 = new NumericBox();
            numericBox23 = new NumericBox();
            numericBox31 = new NumericBox();
            numericBox32 = new NumericBox();
            label5 = new System.Windows.Forms.Label();
            panelViewOptions = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            groupBoxReciProCoordinate = new System.Windows.Forms.GroupBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxExperimentalCoordinate = new System.Windows.Forms.GroupBox();
            flowLayoutPanelFirstAxis = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelSecondAxis = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelThirdAxis = new System.Windows.Forms.FlowLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            panelViewOptions.SuspendLayout();
            groupBoxReciProCoordinate.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            groupBoxExperimentalCoordinate.SuspendLayout();
            flowLayoutPanelFirstAxis.SuspendLayout();
            flowLayoutPanelSecondAxis.SuspendLayout();
            flowLayoutPanelThirdAxis.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 100;
            // 
            // buttonViewAlongBeam
            // 
            resources.ApplyResources(buttonViewAlongBeam, "buttonViewAlongBeam");
            buttonViewAlongBeam.Name = "buttonViewAlongBeam";
            toolTip.SetToolTip(buttonViewAlongBeam, resources.GetString("buttonViewAlongBeam.ToolTip"));
            buttonViewAlongBeam.UseVisualStyleBackColor = true;
            buttonViewAlongBeam.Click += ButtonViewAlongBeam_Click;
            // 
            // buttonViewIsometric
            // 
            resources.ApplyResources(buttonViewIsometric, "buttonViewIsometric");
            buttonViewIsometric.Name = "buttonViewIsometric";
            toolTip.SetToolTip(buttonViewIsometric, resources.GetString("buttonViewIsometric.ToolTip"));
            buttonViewIsometric.UseVisualStyleBackColor = true;
            buttonViewIsometric.Click += ButtonViewIsometric_Click;
            // 
            // checkBoxLink
            // 
            resources.ApplyResources(checkBoxLink, "checkBoxLink");
            checkBoxLink.Name = "checkBoxLink";
            toolTip.SetToolTip(checkBoxLink, resources.GetString("checkBoxLink.ToolTip"));
            checkBoxLink.UseVisualStyleBackColor = true;
            checkBoxLink.CheckedChanged += CheckBoxLink_CheckedChanged;
            // 
            // buttonCopy
            // 
            resources.ApplyResources(buttonCopy, "buttonCopy");
            buttonCopy.Name = "buttonCopy";
            toolTip.SetToolTip(buttonCopy, resources.GetString("buttonCopy.ToolTip"));
            buttonCopy.UseVisualStyleBackColor = true;
            buttonCopy.Click += buttonCopy_Click;
            // 
            // buttonPaste
            // 
            resources.ApplyResources(buttonPaste, "buttonPaste");
            buttonPaste.Name = "buttonPaste";
            toolTip.SetToolTip(buttonPaste, resources.GetString("buttonPaste.ToolTip"));
            buttonPaste.UseVisualStyleBackColor = true;
            buttonPaste.Click += buttonPaste_Click;
            // 
            // numericBoxPsi
            // 
            numericBoxPsi.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPsi.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxPsi, "numericBoxPsi");
            numericBoxPsi.Name = "numericBoxPsi";
            numericBoxPsi.ReadOnly = true;
            numericBoxPsi.SkipEventDuringInput = false;
            numericBoxPsi.SmartIncrement = true;
            numericBoxPsi.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxPsi, resources.GetString("numericBoxPsi.ToolTip"));
            numericBoxPsi.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxPsi.ValueFontSize = 9F;
            // 
            // numericBoxTheta
            // 
            numericBoxTheta.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTheta.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxTheta, "numericBoxTheta");
            numericBoxTheta.Name = "numericBoxTheta";
            numericBoxTheta.ReadOnly = true;
            numericBoxTheta.SkipEventDuringInput = false;
            numericBoxTheta.SmartIncrement = true;
            numericBoxTheta.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxTheta, resources.GetString("numericBoxTheta.ToolTip"));
            numericBoxTheta.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxTheta.ValueFontSize = 9F;
            // 
            // numericBoxPhi
            // 
            numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxPhi, "numericBoxPhi");
            numericBoxPhi.Name = "numericBoxPhi";
            numericBoxPhi.ReadOnly = true;
            numericBoxPhi.SkipEventDuringInput = false;
            numericBoxPhi.SmartIncrement = true;
            numericBoxPhi.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxPhi, resources.GetString("numericBoxPhi.ToolTip"));
            numericBoxPhi.ValueBackColor = System.Drawing.Color.Yellow;
            numericBoxPhi.ValueBoxWidth = 48;
            numericBoxPhi.ValueFontSize = 9F;
            // 
            // numericBoxExp1
            // 
            numericBoxExp1.BackColor = System.Drawing.SystemColors.Control;
            numericBoxExp1.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxExp1, "numericBoxExp1");
            numericBoxExp1.Name = "numericBoxExp1";
            numericBoxExp1.ShowUpDown = true;
            numericBoxExp1.SkipEventDuringInput = false;
            numericBoxExp1.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxExp1, resources.GetString("numericBoxExp1.ToolTip"));
            numericBoxExp1.ValueBoxWidth = 60;
            numericBoxExp1.ValueFontSize = 9F;
            numericBoxExp1.ValueChanged += NumericBoxExp_ValueChanged;
            // 
            // numericBoxExp2
            // 
            numericBoxExp2.BackColor = System.Drawing.SystemColors.Control;
            numericBoxExp2.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxExp2, "numericBoxExp2");
            numericBoxExp2.Name = "numericBoxExp2";
            numericBoxExp2.ShowUpDown = true;
            numericBoxExp2.SkipEventDuringInput = false;
            numericBoxExp2.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxExp2, resources.GetString("numericBoxExp2.ToolTip"));
            numericBoxExp2.ValueBoxWidth = 60;
            numericBoxExp2.ValueFontSize = 9F;
            numericBoxExp2.ValueChanged += NumericBoxExp_ValueChanged;
            // 
            // numericBoxExp3
            // 
            numericBoxExp3.BackColor = System.Drawing.SystemColors.Control;
            numericBoxExp3.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxExp3, "numericBoxExp3");
            numericBoxExp3.Name = "numericBoxExp3";
            numericBoxExp3.ShowUpDown = true;
            numericBoxExp3.SkipEventDuringInput = false;
            numericBoxExp3.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxExp3, resources.GetString("numericBoxExp3.ToolTip"));
            numericBoxExp3.ValueBoxWidth = 60;
            numericBoxExp3.ValueFontSize = 9F;
            numericBoxExp3.ValueChanged += NumericBoxExp_ValueChanged;
            // 
            // checkBoxEnable2nd
            // 
            resources.ApplyResources(checkBoxEnable2nd, "checkBoxEnable2nd");
            checkBoxEnable2nd.Checked = true;
            checkBoxEnable2nd.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxEnable2nd.Name = "checkBoxEnable2nd";
            toolTip.SetToolTip(checkBoxEnable2nd, resources.GetString("checkBoxEnable2nd.ToolTip"));
            checkBoxEnable2nd.UseVisualStyleBackColor = true;
            checkBoxEnable2nd.CheckedChanged += checkBox1st_CheckedChanged;
            // 
            // checkBoxFix1st
            // 
            resources.ApplyResources(checkBoxFix1st, "checkBoxFix1st");
            checkBoxFix1st.Name = "checkBoxFix1st";
            toolTip.SetToolTip(checkBoxFix1st, resources.GetString("checkBoxFix1st.ToolTip"));
            checkBoxFix1st.UseVisualStyleBackColor = true;
            // 
            // checkBoxFix2nd
            // 
            resources.ApplyResources(checkBoxFix2nd, "checkBoxFix2nd");
            checkBoxFix2nd.Name = "checkBoxFix2nd";
            toolTip.SetToolTip(checkBoxFix2nd, resources.GetString("checkBoxFix2nd.ToolTip"));
            checkBoxFix2nd.UseVisualStyleBackColor = true;
            // 
            // checkBoxFix3rd
            // 
            resources.ApplyResources(checkBoxFix3rd, "checkBoxFix3rd");
            checkBoxFix3rd.Name = "checkBoxFix3rd";
            toolTip.SetToolTip(checkBoxFix3rd, resources.GetString("checkBoxFix3rd.ToolTip"));
            checkBoxFix3rd.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnable3rd
            // 
            resources.ApplyResources(checkBoxEnable3rd, "checkBoxEnable3rd");
            checkBoxEnable3rd.Checked = true;
            checkBoxEnable3rd.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxEnable3rd.Name = "checkBoxEnable3rd";
            toolTip.SetToolTip(checkBoxEnable3rd, resources.GetString("checkBoxEnable3rd.ToolTip"));
            checkBoxEnable3rd.UseVisualStyleBackColor = true;
            checkBoxEnable3rd.CheckedChanged += checkBox1st_CheckedChanged;
            // 
            // radioButton3rdZ
            // 
            resources.ApplyResources(radioButton3rdZ, "radioButton3rdZ");
            radioButton3rdZ.ForeColor = System.Drawing.Color.Blue;
            radioButton3rdZ.Name = "radioButton3rdZ";
            toolTip.SetToolTip(radioButton3rdZ, resources.GetString("radioButton3rdZ.ToolTip"));
            radioButton3rdZ.UseVisualStyleBackColor = true;
            radioButton3rdZ.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // radioButton3rdY
            // 
            resources.ApplyResources(radioButton3rdY, "radioButton3rdY");
            radioButton3rdY.Checked = true;
            radioButton3rdY.ForeColor = System.Drawing.Color.Green;
            radioButton3rdY.Name = "radioButton3rdY";
            radioButton3rdY.TabStop = true;
            toolTip.SetToolTip(radioButton3rdY, resources.GetString("radioButton3rdY.ToolTip"));
            radioButton3rdY.UseVisualStyleBackColor = true;
            radioButton3rdY.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // radioButton3rdX
            // 
            resources.ApplyResources(radioButton3rdX, "radioButton3rdX");
            radioButton3rdX.ForeColor = System.Drawing.Color.Red;
            radioButton3rdX.Name = "radioButton3rdX";
            toolTip.SetToolTip(radioButton3rdX, resources.GetString("radioButton3rdX.ToolTip"));
            radioButton3rdX.UseVisualStyleBackColor = true;
            radioButton3rdX.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // radioButton2ndZ
            // 
            resources.ApplyResources(radioButton2ndZ, "radioButton2ndZ");
            radioButton2ndZ.Checked = true;
            radioButton2ndZ.ForeColor = System.Drawing.Color.Blue;
            radioButton2ndZ.Name = "radioButton2ndZ";
            radioButton2ndZ.TabStop = true;
            toolTip.SetToolTip(radioButton2ndZ, resources.GetString("radioButton2ndZ.ToolTip"));
            radioButton2ndZ.UseVisualStyleBackColor = true;
            radioButton2ndZ.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // radioButton2ndY
            // 
            resources.ApplyResources(radioButton2ndY, "radioButton2ndY");
            radioButton2ndY.ForeColor = System.Drawing.Color.Green;
            radioButton2ndY.Name = "radioButton2ndY";
            toolTip.SetToolTip(radioButton2ndY, resources.GetString("radioButton2ndY.ToolTip"));
            radioButton2ndY.UseVisualStyleBackColor = true;
            radioButton2ndY.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // radioButton2ndX
            // 
            resources.ApplyResources(radioButton2ndX, "radioButton2ndX");
            radioButton2ndX.ForeColor = System.Drawing.Color.Red;
            radioButton2ndX.Name = "radioButton2ndX";
            toolTip.SetToolTip(radioButton2ndX, resources.GetString("radioButton2ndX.ToolTip"));
            radioButton2ndX.UseVisualStyleBackColor = true;
            radioButton2ndX.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // radioButton1stZ
            // 
            resources.ApplyResources(radioButton1stZ, "radioButton1stZ");
            radioButton1stZ.ForeColor = System.Drawing.Color.Blue;
            radioButton1stZ.Name = "radioButton1stZ";
            toolTip.SetToolTip(radioButton1stZ, resources.GetString("radioButton1stZ.ToolTip"));
            radioButton1stZ.UseVisualStyleBackColor = true;
            radioButton1stZ.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // radioButton1stY
            // 
            resources.ApplyResources(radioButton1stY, "radioButton1stY");
            radioButton1stY.ForeColor = System.Drawing.Color.Green;
            radioButton1stY.Name = "radioButton1stY";
            toolTip.SetToolTip(radioButton1stY, resources.GetString("radioButton1stY.ToolTip"));
            radioButton1stY.UseVisualStyleBackColor = true;
            radioButton1stY.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // radioButton1stX
            // 
            resources.ApplyResources(radioButton1stX, "radioButton1stX");
            radioButton1stX.Checked = true;
            radioButton1stX.ForeColor = System.Drawing.Color.Red;
            radioButton1stX.Name = "radioButton1stX";
            radioButton1stX.TabStop = true;
            toolTip.SetToolTip(radioButton1stX, resources.GetString("radioButton1stX.ToolTip"));
            radioButton1stX.UseVisualStyleBackColor = true;
            radioButton1stX.CheckedChanged += RadioButton_CheckedChanged;
            // 
            // buttonResetExpEuler
            // 
            resources.ApplyResources(buttonResetExpEuler, "buttonResetExpEuler");
            buttonResetExpEuler.Name = "buttonResetExpEuler";
            toolTip.SetToolTip(buttonResetExpEuler, resources.GetString("buttonResetExpEuler.ToolTip"));
            buttonResetExpEuler.UseVisualStyleBackColor = true;
            buttonResetExpEuler.Click += buttonResetExpEuler_Click;
            // 
            // numericBox11
            // 
            numericBox11.BackColor = System.Drawing.SystemColors.Control;
            numericBox11.DecimalPlaces = 5;
            resources.ApplyResources(numericBox11, "numericBox11");
            numericBox11.Name = "numericBox11";
            numericBox11.ReadOnly = true;
            numericBox11.SkipEventDuringInput = false;
            numericBox11.SmartIncrement = true;
            numericBox11.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBox11, resources.GetString("numericBox11.ToolTip"));
            numericBox11.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBox11.ValueFontSize = 9F;
            numericBox11.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox12
            // 
            numericBox12.BackColor = System.Drawing.SystemColors.Control;
            numericBox12.DecimalPlaces = 5;
            resources.ApplyResources(numericBox12, "numericBox12");
            numericBox12.Name = "numericBox12";
            numericBox12.ReadOnly = true;
            numericBox12.SkipEventDuringInput = false;
            numericBox12.SmartIncrement = true;
            numericBox12.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBox12, resources.GetString("numericBox12.ToolTip"));
            numericBox12.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBox12.ValueFontSize = 9F;
            numericBox12.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox13
            // 
            numericBox13.BackColor = System.Drawing.SystemColors.Control;
            numericBox13.DecimalPlaces = 5;
            resources.ApplyResources(numericBox13, "numericBox13");
            numericBox13.Name = "numericBox13";
            numericBox13.ReadOnly = true;
            numericBox13.SkipEventDuringInput = false;
            numericBox13.SmartIncrement = true;
            numericBox13.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBox13, resources.GetString("numericBox13.ToolTip"));
            numericBox13.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBox13.ValueFontSize = 9F;
            numericBox13.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox21
            // 
            numericBox21.BackColor = System.Drawing.SystemColors.Control;
            numericBox21.DecimalPlaces = 5;
            resources.ApplyResources(numericBox21, "numericBox21");
            numericBox21.Name = "numericBox21";
            numericBox21.ReadOnly = true;
            numericBox21.SkipEventDuringInput = false;
            numericBox21.SmartIncrement = true;
            numericBox21.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBox21, resources.GetString("numericBox21.ToolTip"));
            numericBox21.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBox21.ValueFontSize = 9F;
            numericBox21.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox22
            // 
            numericBox22.BackColor = System.Drawing.SystemColors.Control;
            numericBox22.DecimalPlaces = 5;
            resources.ApplyResources(numericBox22, "numericBox22");
            numericBox22.Name = "numericBox22";
            numericBox22.ReadOnly = true;
            numericBox22.SkipEventDuringInput = false;
            numericBox22.SmartIncrement = true;
            numericBox22.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBox22, resources.GetString("numericBox22.ToolTip"));
            numericBox22.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBox22.ValueFontSize = 9F;
            numericBox22.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox33
            // 
            numericBox33.BackColor = System.Drawing.SystemColors.Control;
            numericBox33.DecimalPlaces = 5;
            resources.ApplyResources(numericBox33, "numericBox33");
            numericBox33.Name = "numericBox33";
            numericBox33.ReadOnly = true;
            numericBox33.SkipEventDuringInput = false;
            numericBox33.SmartIncrement = true;
            numericBox33.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBox33, resources.GetString("numericBox33.ToolTip"));
            numericBox33.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBox33.ValueFontSize = 9F;
            numericBox33.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox23
            // 
            numericBox23.BackColor = System.Drawing.SystemColors.Control;
            numericBox23.DecimalPlaces = 5;
            resources.ApplyResources(numericBox23, "numericBox23");
            numericBox23.Name = "numericBox23";
            numericBox23.ReadOnly = true;
            numericBox23.SkipEventDuringInput = false;
            numericBox23.SmartIncrement = true;
            numericBox23.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBox23, resources.GetString("numericBox23.ToolTip"));
            numericBox23.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBox23.ValueFontSize = 9F;
            numericBox23.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox31
            // 
            numericBox31.BackColor = System.Drawing.SystemColors.Control;
            numericBox31.DecimalPlaces = 5;
            resources.ApplyResources(numericBox31, "numericBox31");
            numericBox31.Name = "numericBox31";
            numericBox31.ReadOnly = true;
            numericBox31.SkipEventDuringInput = false;
            numericBox31.SmartIncrement = true;
            numericBox31.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBox31, resources.GetString("numericBox31.ToolTip"));
            numericBox31.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBox31.ValueFontSize = 9F;
            numericBox31.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox32
            // 
            numericBox32.BackColor = System.Drawing.SystemColors.Control;
            numericBox32.DecimalPlaces = 5;
            resources.ApplyResources(numericBox32, "numericBox32");
            numericBox32.Name = "numericBox32";
            numericBox32.ReadOnly = true;
            numericBox32.SkipEventDuringInput = false;
            numericBox32.SmartIncrement = true;
            numericBox32.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBox32, resources.GetString("numericBox32.ToolTip"));
            numericBox32.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBox32.ValueFontSize = 9F;
            numericBox32.ValueChanged += numericBox_ValueChanged;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // panelViewOptions
            // 
            resources.ApplyResources(panelViewOptions, "panelViewOptions");
            panelViewOptions.Controls.Add(checkBoxLink);
            panelViewOptions.Controls.Add(panel1);
            panelViewOptions.Controls.Add(buttonViewIsometric);
            panelViewOptions.Controls.Add(buttonViewAlongBeam);
            panelViewOptions.Name = "panelViewOptions";
            // 
            // panel1
            // 
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // groupBoxReciProCoordinate
            // 
            groupBoxReciProCoordinate.Controls.Add(flowLayoutPanel1);
            resources.ApplyResources(groupBoxReciProCoordinate, "groupBoxReciProCoordinate");
            groupBoxReciProCoordinate.Name = "groupBoxReciProCoordinate";
            groupBoxReciProCoordinate.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(tableLayoutPanel1);
            flowLayoutPanel1.Controls.Add(flowLayoutPanel2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(numericBoxPsi, 0, 2);
            tableLayoutPanel1.Controls.Add(numericBox11, 2, 0);
            tableLayoutPanel1.Controls.Add(numericBox12, 3, 0);
            tableLayoutPanel1.Controls.Add(numericBox13, 4, 0);
            tableLayoutPanel1.Controls.Add(numericBox21, 2, 1);
            tableLayoutPanel1.Controls.Add(numericBox22, 3, 1);
            tableLayoutPanel1.Controls.Add(numericBox33, 4, 2);
            tableLayoutPanel1.Controls.Add(numericBox23, 4, 1);
            tableLayoutPanel1.Controls.Add(numericBox31, 2, 2);
            tableLayoutPanel1.Controls.Add(numericBox32, 3, 2);
            tableLayoutPanel1.Controls.Add(numericBoxPhi, 0, 0);
            tableLayoutPanel1.Controls.Add(numericBoxTheta, 0, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(buttonCopy);
            flowLayoutPanel2.Controls.Add(buttonPaste);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // groupBoxExperimentalCoordinate
            // 
            groupBoxExperimentalCoordinate.Controls.Add(buttonResetExpEuler);
            groupBoxExperimentalCoordinate.Controls.Add(flowLayoutPanelFirstAxis);
            groupBoxExperimentalCoordinate.Controls.Add(flowLayoutPanelSecondAxis);
            groupBoxExperimentalCoordinate.Controls.Add(flowLayoutPanelThirdAxis);
            groupBoxExperimentalCoordinate.Controls.Add(checkBoxEnable3rd);
            groupBoxExperimentalCoordinate.Controls.Add(checkBoxFix3rd);
            groupBoxExperimentalCoordinate.Controls.Add(checkBoxFix2nd);
            groupBoxExperimentalCoordinate.Controls.Add(checkBoxFix1st);
            groupBoxExperimentalCoordinate.Controls.Add(checkBoxEnable2nd);
            groupBoxExperimentalCoordinate.Controls.Add(numericBoxExp3);
            groupBoxExperimentalCoordinate.Controls.Add(numericBoxExp2);
            groupBoxExperimentalCoordinate.Controls.Add(numericBoxExp1);
            groupBoxExperimentalCoordinate.Controls.Add(label5);
            resources.ApplyResources(groupBoxExperimentalCoordinate, "groupBoxExperimentalCoordinate");
            groupBoxExperimentalCoordinate.Name = "groupBoxExperimentalCoordinate";
            groupBoxExperimentalCoordinate.TabStop = false;
            // 
            // flowLayoutPanelFirstAxis
            // 
            resources.ApplyResources(flowLayoutPanelFirstAxis, "flowLayoutPanelFirstAxis");
            flowLayoutPanelFirstAxis.Controls.Add(radioButton1stX);
            flowLayoutPanelFirstAxis.Controls.Add(radioButton1stY);
            flowLayoutPanelFirstAxis.Controls.Add(radioButton1stZ);
            flowLayoutPanelFirstAxis.Name = "flowLayoutPanelFirstAxis";
            // 
            // flowLayoutPanelSecondAxis
            // 
            resources.ApplyResources(flowLayoutPanelSecondAxis, "flowLayoutPanelSecondAxis");
            flowLayoutPanelSecondAxis.Controls.Add(radioButton2ndX);
            flowLayoutPanelSecondAxis.Controls.Add(radioButton2ndY);
            flowLayoutPanelSecondAxis.Controls.Add(radioButton2ndZ);
            flowLayoutPanelSecondAxis.Name = "flowLayoutPanelSecondAxis";
            // 
            // flowLayoutPanelThirdAxis
            // 
            resources.ApplyResources(flowLayoutPanelThirdAxis, "flowLayoutPanelThirdAxis");
            flowLayoutPanelThirdAxis.Controls.Add(radioButton3rdX);
            flowLayoutPanelThirdAxis.Controls.Add(radioButton3rdY);
            flowLayoutPanelThirdAxis.Controls.Add(radioButton3rdZ);
            flowLayoutPanelThirdAxis.Name = "flowLayoutPanelThirdAxis";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
            tableLayoutPanel2.Controls.Add(groupBoxReciProCoordinate, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBoxExperimentalCoordinate, 2, 0);
            tableLayoutPanel2.Controls.Add(panelViewOptions, 1, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // FormRotationMatrix
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(tableLayoutPanel2);
            Name = "FormRotationMatrix";
            FormClosing += FormRotationMatrix_FormClosing;
            Load += FormRotationMatrix_Load;
            VisibleChanged += FormRotationMatrix_VisibleChanged;
            panelViewOptions.ResumeLayout(false);
            panelViewOptions.PerformLayout();
            groupBoxReciProCoordinate.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            groupBoxExperimentalCoordinate.ResumeLayout(false);
            groupBoxExperimentalCoordinate.PerformLayout();
            flowLayoutPanelFirstAxis.ResumeLayout(false);
            flowLayoutPanelFirstAxis.PerformLayout();
            flowLayoutPanelSecondAxis.ResumeLayout(false);
            flowLayoutPanelSecondAxis.PerformLayout();
            flowLayoutPanelThirdAxis.ResumeLayout(false);
            flowLayoutPanelThirdAxis.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panelViewOptions;
        private System.Windows.Forms.CheckBox checkBoxLink;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonViewIsometric;
        private System.Windows.Forms.Button buttonViewAlongBeam;
        private System.Windows.Forms.GroupBox groupBoxReciProCoordinate;
        private NumericBox numericBoxPhi;
        private NumericBox numericBoxTheta;
        private NumericBox numericBoxPsi;
        private System.Windows.Forms.Button buttonPaste;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private NumericBox numericBox11;
        private NumericBox numericBox12;
        private NumericBox numericBox13;
        private NumericBox numericBox21;
        private NumericBox numericBox22;
        private NumericBox numericBox33;
        private NumericBox numericBox23;
        private NumericBox numericBox31;
        private NumericBox numericBox32;
        private System.Windows.Forms.GroupBox groupBoxExperimentalCoordinate;
        private System.Windows.Forms.Button buttonResetExpEuler;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFirstAxis;
        private System.Windows.Forms.RadioButton radioButton1stX;
        private System.Windows.Forms.RadioButton radioButton1stY;
        private System.Windows.Forms.RadioButton radioButton1stZ;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSecondAxis;
        private System.Windows.Forms.RadioButton radioButton2ndX;
        private System.Windows.Forms.RadioButton radioButton2ndY;
        private System.Windows.Forms.RadioButton radioButton2ndZ;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelThirdAxis;
        private System.Windows.Forms.RadioButton radioButton3rdX;
        private System.Windows.Forms.RadioButton radioButton3rdY;
        private System.Windows.Forms.RadioButton radioButton3rdZ;
        private System.Windows.Forms.CheckBox checkBoxEnable3rd;
        private System.Windows.Forms.CheckBox checkBoxFix3rd;
        private System.Windows.Forms.CheckBox checkBoxFix2nd;
        private System.Windows.Forms.CheckBox checkBoxFix1st;
        private System.Windows.Forms.CheckBox checkBoxEnable2nd;
        private NumericBox numericBoxExp3;
        private NumericBox numericBoxExp2;
        private NumericBox numericBoxExp1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    }
}
