namespace ReciPro
{
    partial class FormRotationMatrix
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
            panel2 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            groupBox1 = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            numericBox11 = new NumericBox();
            numericBox12 = new NumericBox();
            numericBox13 = new NumericBox();
            numericBox21 = new NumericBox();
            numericBox22 = new NumericBox();
            numericBox33 = new NumericBox();
            numericBox23 = new NumericBox();
            numericBox31 = new NumericBox();
            numericBox32 = new NumericBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            groupBox2 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            label5 = new System.Windows.Forms.Label();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            panel2.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
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
            resources.ApplyResources(numericBoxPsi, "numericBoxPsi");
            numericBoxPsi.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPsi.DecimalPlaces = 3;
            numericBoxPsi.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPsi.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPsi.Name = "numericBoxPsi";
            numericBoxPsi.ReadOnly = true;
            numericBoxPsi.RoundErrorAccuracy = -1;
            numericBoxPsi.SkipEventDuringInput = false;
            numericBoxPsi.SmartIncrement = true;
            numericBoxPsi.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBoxPsi.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxPsi, resources.GetString("numericBoxPsi.ToolTip"));
            // 
            // numericBoxTheta
            // 
            resources.ApplyResources(numericBoxTheta, "numericBoxTheta");
            numericBoxTheta.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTheta.DecimalPlaces = 3;
            numericBoxTheta.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTheta.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTheta.Name = "numericBoxTheta";
            numericBoxTheta.ReadOnly = true;
            numericBoxTheta.RoundErrorAccuracy = -1;
            numericBoxTheta.SkipEventDuringInput = false;
            numericBoxTheta.SmartIncrement = true;
            numericBoxTheta.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBoxTheta.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxTheta, resources.GetString("numericBoxTheta.ToolTip"));
            // 
            // numericBoxPhi
            // 
            resources.ApplyResources(numericBoxPhi, "numericBoxPhi");
            numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.DecimalPlaces = 3;
            numericBoxPhi.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.Name = "numericBoxPhi";
            numericBoxPhi.ReadOnly = true;
            numericBoxPhi.RoundErrorAccuracy = -1;
            numericBoxPhi.SkipEventDuringInput = false;
            numericBoxPhi.SmartIncrement = true;
            numericBoxPhi.TextBoxBackColor = System.Drawing.Color.Yellow;
            numericBoxPhi.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxPhi, resources.GetString("numericBoxPhi.ToolTip"));
            // 
            // numericBoxExp1
            // 
            resources.ApplyResources(numericBoxExp1, "numericBoxExp1");
            numericBoxExp1.BackColor = System.Drawing.SystemColors.Control;
            numericBoxExp1.DecimalPlaces = 3;
            numericBoxExp1.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxExp1.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxExp1.Name = "numericBoxExp1";
            numericBoxExp1.RoundErrorAccuracy = -1;
            numericBoxExp1.ShowUpDown = true;
            numericBoxExp1.SkipEventDuringInput = false;
            numericBoxExp1.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxExp1, resources.GetString("numericBoxExp1.ToolTip"));
            numericBoxExp1.ValueChanged += NumericBoxExp_ValueChanged;
            // 
            // numericBoxExp2
            // 
            resources.ApplyResources(numericBoxExp2, "numericBoxExp2");
            numericBoxExp2.BackColor = System.Drawing.SystemColors.Control;
            numericBoxExp2.DecimalPlaces = 3;
            numericBoxExp2.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxExp2.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxExp2.Name = "numericBoxExp2";
            numericBoxExp2.RoundErrorAccuracy = -1;
            numericBoxExp2.ShowUpDown = true;
            numericBoxExp2.SkipEventDuringInput = false;
            numericBoxExp2.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxExp2, resources.GetString("numericBoxExp2.ToolTip"));
            numericBoxExp2.ValueChanged += NumericBoxExp_ValueChanged;
            // 
            // numericBoxExp3
            // 
            resources.ApplyResources(numericBoxExp3, "numericBoxExp3");
            numericBoxExp3.BackColor = System.Drawing.SystemColors.Control;
            numericBoxExp3.DecimalPlaces = 3;
            numericBoxExp3.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxExp3.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxExp3.Name = "numericBoxExp3";
            numericBoxExp3.RoundErrorAccuracy = -1;
            numericBoxExp3.ShowUpDown = true;
            numericBoxExp3.SkipEventDuringInput = false;
            numericBoxExp3.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxExp3, resources.GetString("numericBoxExp3.ToolTip"));
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
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Controls.Add(checkBoxLink);
            panel2.Controls.Add(panel1);
            panel2.Controls.Add(buttonViewIsometric);
            panel2.Controls.Add(buttonViewAlongBeam);
            panel2.Name = "panel2";
            // 
            // panel1
            // 
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(numericBoxPhi);
            groupBox1.Controls.Add(numericBoxTheta);
            groupBox1.Controls.Add(numericBoxPsi);
            groupBox1.Controls.Add(buttonPaste);
            groupBox1.Controls.Add(buttonCopy);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(numericBox11, 0, 0);
            tableLayoutPanel1.Controls.Add(numericBox12, 1, 0);
            tableLayoutPanel1.Controls.Add(numericBox13, 2, 0);
            tableLayoutPanel1.Controls.Add(numericBox21, 0, 1);
            tableLayoutPanel1.Controls.Add(numericBox22, 1, 1);
            tableLayoutPanel1.Controls.Add(numericBox33, 2, 2);
            tableLayoutPanel1.Controls.Add(numericBox23, 2, 1);
            tableLayoutPanel1.Controls.Add(numericBox31, 0, 2);
            tableLayoutPanel1.Controls.Add(numericBox32, 1, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // numericBox11
            // 
            resources.ApplyResources(numericBox11, "numericBox11");
            numericBox11.BackColor = System.Drawing.SystemColors.Control;
            numericBox11.DecimalPlaces = 5;
            numericBox11.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBox11.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBox11.Name = "numericBox11";
            numericBox11.ReadOnly = true;
            numericBox11.RoundErrorAccuracy = -1;
            numericBox11.SkipEventDuringInput = false;
            numericBox11.SmartIncrement = true;
            numericBox11.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBox11.ThonsandsSeparator = true;
            numericBox11.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox12
            // 
            resources.ApplyResources(numericBox12, "numericBox12");
            numericBox12.BackColor = System.Drawing.SystemColors.Control;
            numericBox12.DecimalPlaces = 5;
            numericBox12.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBox12.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBox12.Name = "numericBox12";
            numericBox12.ReadOnly = true;
            numericBox12.RoundErrorAccuracy = -1;
            numericBox12.SkipEventDuringInput = false;
            numericBox12.SmartIncrement = true;
            numericBox12.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBox12.ThonsandsSeparator = true;
            numericBox12.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox13
            // 
            resources.ApplyResources(numericBox13, "numericBox13");
            numericBox13.BackColor = System.Drawing.SystemColors.Control;
            numericBox13.DecimalPlaces = 5;
            numericBox13.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBox13.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBox13.Name = "numericBox13";
            numericBox13.ReadOnly = true;
            numericBox13.RoundErrorAccuracy = -1;
            numericBox13.SkipEventDuringInput = false;
            numericBox13.SmartIncrement = true;
            numericBox13.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBox13.ThonsandsSeparator = true;
            numericBox13.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox21
            // 
            resources.ApplyResources(numericBox21, "numericBox21");
            numericBox21.BackColor = System.Drawing.SystemColors.Control;
            numericBox21.DecimalPlaces = 5;
            numericBox21.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBox21.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBox21.Name = "numericBox21";
            numericBox21.ReadOnly = true;
            numericBox21.RoundErrorAccuracy = -1;
            numericBox21.SkipEventDuringInput = false;
            numericBox21.SmartIncrement = true;
            numericBox21.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBox21.ThonsandsSeparator = true;
            numericBox21.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox22
            // 
            resources.ApplyResources(numericBox22, "numericBox22");
            numericBox22.BackColor = System.Drawing.SystemColors.Control;
            numericBox22.DecimalPlaces = 5;
            numericBox22.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBox22.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBox22.Name = "numericBox22";
            numericBox22.ReadOnly = true;
            numericBox22.RoundErrorAccuracy = -1;
            numericBox22.SkipEventDuringInput = false;
            numericBox22.SmartIncrement = true;
            numericBox22.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBox22.ThonsandsSeparator = true;
            numericBox22.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox33
            // 
            resources.ApplyResources(numericBox33, "numericBox33");
            numericBox33.BackColor = System.Drawing.SystemColors.Control;
            numericBox33.DecimalPlaces = 5;
            numericBox33.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBox33.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBox33.Name = "numericBox33";
            numericBox33.ReadOnly = true;
            numericBox33.RoundErrorAccuracy = -1;
            numericBox33.SkipEventDuringInput = false;
            numericBox33.SmartIncrement = true;
            numericBox33.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBox33.ThonsandsSeparator = true;
            numericBox33.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox23
            // 
            resources.ApplyResources(numericBox23, "numericBox23");
            numericBox23.BackColor = System.Drawing.SystemColors.Control;
            numericBox23.DecimalPlaces = 5;
            numericBox23.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBox23.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBox23.Name = "numericBox23";
            numericBox23.ReadOnly = true;
            numericBox23.RoundErrorAccuracy = -1;
            numericBox23.SkipEventDuringInput = false;
            numericBox23.SmartIncrement = true;
            numericBox23.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBox23.ThonsandsSeparator = true;
            numericBox23.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox31
            // 
            resources.ApplyResources(numericBox31, "numericBox31");
            numericBox31.BackColor = System.Drawing.SystemColors.Control;
            numericBox31.DecimalPlaces = 5;
            numericBox31.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBox31.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBox31.Name = "numericBox31";
            numericBox31.ReadOnly = true;
            numericBox31.RoundErrorAccuracy = -1;
            numericBox31.SkipEventDuringInput = false;
            numericBox31.SmartIncrement = true;
            numericBox31.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBox31.ThonsandsSeparator = true;
            numericBox31.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBox32
            // 
            resources.ApplyResources(numericBox32, "numericBox32");
            numericBox32.BackColor = System.Drawing.SystemColors.Control;
            numericBox32.DecimalPlaces = 5;
            numericBox32.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBox32.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBox32.Name = "numericBox32";
            numericBox32.ReadOnly = true;
            numericBox32.RoundErrorAccuracy = -1;
            numericBox32.SkipEventDuringInput = false;
            numericBox32.SmartIncrement = true;
            numericBox32.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBox32.ThonsandsSeparator = true;
            numericBox32.ValueChanged += numericBox_ValueChanged;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(buttonResetExpEuler);
            groupBox2.Controls.Add(flowLayoutPanel1);
            groupBox2.Controls.Add(flowLayoutPanel2);
            groupBox2.Controls.Add(flowLayoutPanel3);
            groupBox2.Controls.Add(checkBoxEnable3rd);
            groupBox2.Controls.Add(checkBoxFix3rd);
            groupBox2.Controls.Add(checkBoxFix2nd);
            groupBox2.Controls.Add(checkBoxFix1st);
            groupBox2.Controls.Add(checkBoxEnable2nd);
            groupBox2.Controls.Add(numericBoxExp3);
            groupBox2.Controls.Add(numericBoxExp2);
            groupBox2.Controls.Add(numericBoxExp1);
            groupBox2.Controls.Add(label5);
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(radioButton1stX);
            flowLayoutPanel1.Controls.Add(radioButton1stY);
            flowLayoutPanel1.Controls.Add(radioButton1stZ);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(radioButton2ndX);
            flowLayoutPanel2.Controls.Add(radioButton2ndY);
            flowLayoutPanel2.Controls.Add(radioButton2ndZ);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(radioButton3rdX);
            flowLayoutPanel3.Controls.Add(radioButton3rdY);
            flowLayoutPanel3.Controls.Add(radioButton3rdZ);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
            tableLayoutPanel2.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(groupBox2, 2, 0);
            tableLayoutPanel2.Controls.Add(panel2, 1, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // FormRotationMatrix
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(tableLayoutPanel2);
            Name = "FormRotationMatrix";
            FormClosing += FormRotationMatrix_FormClosing;
            Load += FormRotationMatrix_Load;
            VisibleChanged += FormRotationMatrix_VisibleChanged;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBoxLink;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonViewIsometric;
        private System.Windows.Forms.Button buttonViewAlongBeam;
        private System.Windows.Forms.GroupBox groupBox1;
        private NumericBox numericBoxPhi;
        private NumericBox numericBoxTheta;
        private NumericBox numericBoxPsi;
        private System.Windows.Forms.Button buttonPaste;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonResetExpEuler;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButton1stX;
        private System.Windows.Forms.RadioButton radioButton1stY;
        private System.Windows.Forms.RadioButton radioButton1stZ;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton radioButton2ndX;
        private System.Windows.Forms.RadioButton radioButton2ndY;
        private System.Windows.Forms.RadioButton radioButton2ndZ;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
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
    }
}