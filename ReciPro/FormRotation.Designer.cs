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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRotationMatrix));
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numericBox11 = new Crystallography.Controls.NumericBox();
            this.numericBox12 = new Crystallography.Controls.NumericBox();
            this.numericBox13 = new Crystallography.Controls.NumericBox();
            this.numericBox21 = new Crystallography.Controls.NumericBox();
            this.numericBox22 = new Crystallography.Controls.NumericBox();
            this.numericBox33 = new Crystallography.Controls.NumericBox();
            this.numericBox23 = new Crystallography.Controls.NumericBox();
            this.numericBox31 = new Crystallography.Controls.NumericBox();
            this.numericBox32 = new Crystallography.Controls.NumericBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonPaste = new System.Windows.Forms.Button();
            this.buttonViewIsometric = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericBoxPhi = new Crystallography.Controls.NumericBox();
            this.numericBoxTheta = new Crystallography.Controls.NumericBox();
            this.numericBoxPsi = new Crystallography.Controls.NumericBox();
            this.radioButton1stX = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButton1stY = new System.Windows.Forms.RadioButton();
            this.radioButton1stZ = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButton2ndX = new System.Windows.Forms.RadioButton();
            this.radioButton2ndY = new System.Windows.Forms.RadioButton();
            this.radioButton2ndZ = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButton3rdX = new System.Windows.Forms.RadioButton();
            this.radioButton3rdY = new System.Windows.Forms.RadioButton();
            this.radioButton3rdZ = new System.Windows.Forms.RadioButton();
            this.checkBoxLink = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonResetExpEuler = new System.Windows.Forms.Button();
            this.checkBoxEnable3rd = new System.Windows.Forms.CheckBox();
            this.checkBoxFix3rd = new System.Windows.Forms.CheckBox();
            this.checkBoxFix2nd = new System.Windows.Forms.CheckBox();
            this.checkBoxFix1st = new System.Windows.Forms.CheckBox();
            this.checkBoxEnable2nd = new System.Windows.Forms.CheckBox();
            this.numericBoxExp3 = new Crystallography.Controls.NumericBox();
            this.numericBoxExp2 = new Crystallography.Controls.NumericBox();
            this.numericBoxExp1 = new Crystallography.Controls.NumericBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonViewAlongBeam = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.numericBox11, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericBox12, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericBox13, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericBox21, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericBox22, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericBox33, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericBox23, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericBox31, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericBox32, 1, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // numericBox11
            // 
            resources.ApplyResources(this.numericBox11, "numericBox11");
            this.numericBox11.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox11.DecimalPlaces = 5;
            this.numericBox11.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox11.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox11.Name = "numericBox11";
            this.numericBox11.ReadOnly = true;
            this.numericBox11.SkipEventDuringInput = false;
            this.numericBox11.SmartIncrement = true;
            this.numericBox11.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBox11.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBox11.ThonsandsSeparator = true;
            this.numericBox11.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBox6_ValueChanged);
            // 
            // numericBox12
            // 
            resources.ApplyResources(this.numericBox12, "numericBox12");
            this.numericBox12.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox12.DecimalPlaces = 5;
            this.numericBox12.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox12.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox12.Name = "numericBox12";
            this.numericBox12.ReadOnly = true;
            this.numericBox12.SkipEventDuringInput = false;
            this.numericBox12.SmartIncrement = true;
            this.numericBox12.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBox12.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBox12.ThonsandsSeparator = true;
            this.numericBox12.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBox6_ValueChanged);
            // 
            // numericBox13
            // 
            resources.ApplyResources(this.numericBox13, "numericBox13");
            this.numericBox13.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox13.DecimalPlaces = 5;
            this.numericBox13.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox13.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox13.Name = "numericBox13";
            this.numericBox13.ReadOnly = true;
            this.numericBox13.SkipEventDuringInput = false;
            this.numericBox13.SmartIncrement = true;
            this.numericBox13.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBox13.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBox13.ThonsandsSeparator = true;
            this.numericBox13.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBox6_ValueChanged);
            // 
            // numericBox21
            // 
            resources.ApplyResources(this.numericBox21, "numericBox21");
            this.numericBox21.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox21.DecimalPlaces = 5;
            this.numericBox21.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox21.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox21.Name = "numericBox21";
            this.numericBox21.ReadOnly = true;
            this.numericBox21.SkipEventDuringInput = false;
            this.numericBox21.SmartIncrement = true;
            this.numericBox21.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBox21.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBox21.ThonsandsSeparator = true;
            this.numericBox21.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBox6_ValueChanged);
            // 
            // numericBox22
            // 
            resources.ApplyResources(this.numericBox22, "numericBox22");
            this.numericBox22.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox22.DecimalPlaces = 5;
            this.numericBox22.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox22.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox22.Name = "numericBox22";
            this.numericBox22.ReadOnly = true;
            this.numericBox22.SkipEventDuringInput = false;
            this.numericBox22.SmartIncrement = true;
            this.numericBox22.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBox22.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBox22.ThonsandsSeparator = true;
            this.numericBox22.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBox6_ValueChanged);
            // 
            // numericBox33
            // 
            resources.ApplyResources(this.numericBox33, "numericBox33");
            this.numericBox33.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox33.DecimalPlaces = 5;
            this.numericBox33.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox33.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox33.Name = "numericBox33";
            this.numericBox33.ReadOnly = true;
            this.numericBox33.SkipEventDuringInput = false;
            this.numericBox33.SmartIncrement = true;
            this.numericBox33.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBox33.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBox33.ThonsandsSeparator = true;
            this.numericBox33.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBox6_ValueChanged);
            // 
            // numericBox23
            // 
            resources.ApplyResources(this.numericBox23, "numericBox23");
            this.numericBox23.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox23.DecimalPlaces = 5;
            this.numericBox23.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox23.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox23.Name = "numericBox23";
            this.numericBox23.ReadOnly = true;
            this.numericBox23.SkipEventDuringInput = false;
            this.numericBox23.SmartIncrement = true;
            this.numericBox23.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBox23.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBox23.ThonsandsSeparator = true;
            this.numericBox23.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBox6_ValueChanged);
            // 
            // numericBox31
            // 
            resources.ApplyResources(this.numericBox31, "numericBox31");
            this.numericBox31.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox31.DecimalPlaces = 5;
            this.numericBox31.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox31.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox31.Name = "numericBox31";
            this.numericBox31.ReadOnly = true;
            this.numericBox31.SkipEventDuringInput = false;
            this.numericBox31.SmartIncrement = true;
            this.numericBox31.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBox31.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBox31.ThonsandsSeparator = true;
            this.numericBox31.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBox6_ValueChanged);
            // 
            // numericBox32
            // 
            resources.ApplyResources(this.numericBox32, "numericBox32");
            this.numericBox32.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox32.DecimalPlaces = 5;
            this.numericBox32.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox32.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox32.Name = "numericBox32";
            this.numericBox32.ReadOnly = true;
            this.numericBox32.SkipEventDuringInput = false;
            this.numericBox32.SmartIncrement = true;
            this.numericBox32.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBox32.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBox32.ThonsandsSeparator = true;
            this.numericBox32.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBox6_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonCopy
            // 
            resources.ApplyResources(this.buttonCopy, "buttonCopy");
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // buttonPaste
            // 
            resources.ApplyResources(this.buttonPaste, "buttonPaste");
            this.buttonPaste.Name = "buttonPaste";
            this.buttonPaste.UseVisualStyleBackColor = true;
            this.buttonPaste.Click += new System.EventHandler(this.buttonPaste_Click);
            // 
            // buttonViewIsometric
            // 
            resources.ApplyResources(this.buttonViewIsometric, "buttonViewIsometric");
            this.buttonViewIsometric.Name = "buttonViewIsometric";
            this.buttonViewIsometric.UseVisualStyleBackColor = true;
            this.buttonViewIsometric.Click += new System.EventHandler(this.ButtonViewIsometric_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.numericBoxPhi);
            this.groupBox1.Controls.Add(this.numericBoxTheta);
            this.groupBox1.Controls.Add(this.numericBoxPsi);
            this.groupBox1.Controls.Add(this.buttonPaste);
            this.groupBox1.Controls.Add(this.buttonCopy);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // numericBoxPhi
            // 
            resources.ApplyResources(this.numericBoxPhi, "numericBoxPhi");
            this.numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.DecimalPlaces = 3;
            this.numericBoxPhi.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.Name = "numericBoxPhi";
            this.numericBoxPhi.ReadOnly = true;
            this.numericBoxPhi.SkipEventDuringInput = false;
            this.numericBoxPhi.SmartIncrement = true;
            this.numericBoxPhi.TextBoxBackColor = System.Drawing.Color.Yellow;
            this.numericBoxPhi.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPhi.ThonsandsSeparator = true;
            // 
            // numericBoxTheta
            // 
            resources.ApplyResources(this.numericBoxTheta, "numericBoxTheta");
            this.numericBoxTheta.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTheta.DecimalPlaces = 3;
            this.numericBoxTheta.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTheta.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTheta.Name = "numericBoxTheta";
            this.numericBoxTheta.ReadOnly = true;
            this.numericBoxTheta.SkipEventDuringInput = false;
            this.numericBoxTheta.SmartIncrement = true;
            this.numericBoxTheta.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTheta.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxTheta.ThonsandsSeparator = true;
            // 
            // numericBoxPsi
            // 
            resources.ApplyResources(this.numericBoxPsi, "numericBoxPsi");
            this.numericBoxPsi.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPsi.DecimalPlaces = 3;
            this.numericBoxPsi.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPsi.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPsi.Name = "numericBoxPsi";
            this.numericBoxPsi.ReadOnly = true;
            this.numericBoxPsi.SkipEventDuringInput = false;
            this.numericBoxPsi.SmartIncrement = true;
            this.numericBoxPsi.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPsi.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPsi.ThonsandsSeparator = true;
            // 
            // radioButton1stX
            // 
            resources.ApplyResources(this.radioButton1stX, "radioButton1stX");
            this.radioButton1stX.Checked = true;
            this.radioButton1stX.ForeColor = System.Drawing.Color.Red;
            this.radioButton1stX.Name = "radioButton1stX";
            this.radioButton1stX.TabStop = true;
            this.radioButton1stX.UseVisualStyleBackColor = true;
            this.radioButton1stX.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.radioButton1stX);
            this.flowLayoutPanel1.Controls.Add(this.radioButton1stY);
            this.flowLayoutPanel1.Controls.Add(this.radioButton1stZ);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // radioButton1stY
            // 
            resources.ApplyResources(this.radioButton1stY, "radioButton1stY");
            this.radioButton1stY.ForeColor = System.Drawing.Color.Green;
            this.radioButton1stY.Name = "radioButton1stY";
            this.radioButton1stY.UseVisualStyleBackColor = true;
            this.radioButton1stY.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // radioButton1stZ
            // 
            resources.ApplyResources(this.radioButton1stZ, "radioButton1stZ");
            this.radioButton1stZ.ForeColor = System.Drawing.Color.Blue;
            this.radioButton1stZ.Name = "radioButton1stZ";
            this.radioButton1stZ.UseVisualStyleBackColor = true;
            this.radioButton1stZ.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Controls.Add(this.radioButton2ndX);
            this.flowLayoutPanel2.Controls.Add(this.radioButton2ndY);
            this.flowLayoutPanel2.Controls.Add(this.radioButton2ndZ);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // radioButton2ndX
            // 
            resources.ApplyResources(this.radioButton2ndX, "radioButton2ndX");
            this.radioButton2ndX.ForeColor = System.Drawing.Color.Red;
            this.radioButton2ndX.Name = "radioButton2ndX";
            this.radioButton2ndX.UseVisualStyleBackColor = true;
            this.radioButton2ndX.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // radioButton2ndY
            // 
            resources.ApplyResources(this.radioButton2ndY, "radioButton2ndY");
            this.radioButton2ndY.ForeColor = System.Drawing.Color.Green;
            this.radioButton2ndY.Name = "radioButton2ndY";
            this.radioButton2ndY.UseVisualStyleBackColor = true;
            this.radioButton2ndY.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // radioButton2ndZ
            // 
            resources.ApplyResources(this.radioButton2ndZ, "radioButton2ndZ");
            this.radioButton2ndZ.Checked = true;
            this.radioButton2ndZ.ForeColor = System.Drawing.Color.Blue;
            this.radioButton2ndZ.Name = "radioButton2ndZ";
            this.radioButton2ndZ.TabStop = true;
            this.radioButton2ndZ.UseVisualStyleBackColor = true;
            this.radioButton2ndZ.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Controls.Add(this.radioButton3rdX);
            this.flowLayoutPanel3.Controls.Add(this.radioButton3rdY);
            this.flowLayoutPanel3.Controls.Add(this.radioButton3rdZ);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // radioButton3rdX
            // 
            resources.ApplyResources(this.radioButton3rdX, "radioButton3rdX");
            this.radioButton3rdX.ForeColor = System.Drawing.Color.Red;
            this.radioButton3rdX.Name = "radioButton3rdX";
            this.radioButton3rdX.UseVisualStyleBackColor = true;
            this.radioButton3rdX.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // radioButton3rdY
            // 
            resources.ApplyResources(this.radioButton3rdY, "radioButton3rdY");
            this.radioButton3rdY.Checked = true;
            this.radioButton3rdY.ForeColor = System.Drawing.Color.Green;
            this.radioButton3rdY.Name = "radioButton3rdY";
            this.radioButton3rdY.TabStop = true;
            this.radioButton3rdY.UseVisualStyleBackColor = true;
            this.radioButton3rdY.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // radioButton3rdZ
            // 
            resources.ApplyResources(this.radioButton3rdZ, "radioButton3rdZ");
            this.radioButton3rdZ.ForeColor = System.Drawing.Color.Blue;
            this.radioButton3rdZ.Name = "radioButton3rdZ";
            this.radioButton3rdZ.UseVisualStyleBackColor = true;
            this.radioButton3rdZ.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // checkBoxLink
            // 
            resources.ApplyResources(this.checkBoxLink, "checkBoxLink");
            this.checkBoxLink.Name = "checkBoxLink";
            this.checkBoxLink.UseVisualStyleBackColor = true;
            this.checkBoxLink.CheckedChanged += new System.EventHandler(this.CheckBoxLink_CheckedChanged);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.buttonResetExpEuler);
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Controls.Add(this.flowLayoutPanel3);
            this.groupBox2.Controls.Add(this.checkBoxEnable3rd);
            this.groupBox2.Controls.Add(this.checkBoxFix3rd);
            this.groupBox2.Controls.Add(this.checkBoxFix2nd);
            this.groupBox2.Controls.Add(this.checkBoxFix1st);
            this.groupBox2.Controls.Add(this.checkBoxEnable2nd);
            this.groupBox2.Controls.Add(this.numericBoxExp3);
            this.groupBox2.Controls.Add(this.numericBoxExp2);
            this.groupBox2.Controls.Add(this.numericBoxExp1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // buttonResetExpEuler
            // 
            resources.ApplyResources(this.buttonResetExpEuler, "buttonResetExpEuler");
            this.buttonResetExpEuler.Name = "buttonResetExpEuler";
            this.buttonResetExpEuler.UseVisualStyleBackColor = true;
            this.buttonResetExpEuler.Click += new System.EventHandler(this.buttonResetExpEuler_Click);
            // 
            // checkBoxEnable3rd
            // 
            resources.ApplyResources(this.checkBoxEnable3rd, "checkBoxEnable3rd");
            this.checkBoxEnable3rd.Checked = true;
            this.checkBoxEnable3rd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnable3rd.Name = "checkBoxEnable3rd";
            this.checkBoxEnable3rd.UseVisualStyleBackColor = true;
            this.checkBoxEnable3rd.CheckedChanged += new System.EventHandler(this.checkBox1st_CheckedChanged);
            // 
            // checkBoxFix3rd
            // 
            resources.ApplyResources(this.checkBoxFix3rd, "checkBoxFix3rd");
            this.checkBoxFix3rd.Name = "checkBoxFix3rd";
            this.checkBoxFix3rd.UseVisualStyleBackColor = true;
            // 
            // checkBoxFix2nd
            // 
            resources.ApplyResources(this.checkBoxFix2nd, "checkBoxFix2nd");
            this.checkBoxFix2nd.Name = "checkBoxFix2nd";
            this.checkBoxFix2nd.UseVisualStyleBackColor = true;
            // 
            // checkBoxFix1st
            // 
            resources.ApplyResources(this.checkBoxFix1st, "checkBoxFix1st");
            this.checkBoxFix1st.Name = "checkBoxFix1st";
            this.checkBoxFix1st.UseVisualStyleBackColor = true;
            // 
            // checkBoxEnable2nd
            // 
            resources.ApplyResources(this.checkBoxEnable2nd, "checkBoxEnable2nd");
            this.checkBoxEnable2nd.Checked = true;
            this.checkBoxEnable2nd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnable2nd.Name = "checkBoxEnable2nd";
            this.checkBoxEnable2nd.UseVisualStyleBackColor = true;
            this.checkBoxEnable2nd.CheckedChanged += new System.EventHandler(this.checkBox1st_CheckedChanged);
            // 
            // numericBoxExp3
            // 
            resources.ApplyResources(this.numericBoxExp3, "numericBoxExp3");
            this.numericBoxExp3.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxExp3.DecimalPlaces = 3;
            this.numericBoxExp3.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxExp3.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxExp3.Name = "numericBoxExp3";
            this.numericBoxExp3.ShowUpDown = true;
            this.numericBoxExp3.SkipEventDuringInput = false;
            this.numericBoxExp3.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxExp3.ThonsandsSeparator = true;
            this.numericBoxExp3.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.NumericBoxExp_ValueChanged);
            // 
            // numericBoxExp2
            // 
            resources.ApplyResources(this.numericBoxExp2, "numericBoxExp2");
            this.numericBoxExp2.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxExp2.DecimalPlaces = 3;
            this.numericBoxExp2.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxExp2.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxExp2.Name = "numericBoxExp2";
            this.numericBoxExp2.ShowUpDown = true;
            this.numericBoxExp2.SkipEventDuringInput = false;
            this.numericBoxExp2.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxExp2.ThonsandsSeparator = true;
            this.numericBoxExp2.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.NumericBoxExp_ValueChanged);
            // 
            // numericBoxExp1
            // 
            resources.ApplyResources(this.numericBoxExp1, "numericBoxExp1");
            this.numericBoxExp1.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxExp1.DecimalPlaces = 3;
            this.numericBoxExp1.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxExp1.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxExp1.Name = "numericBoxExp1";
            this.numericBoxExp1.ShowUpDown = true;
            this.numericBoxExp1.SkipEventDuringInput = false;
            this.numericBoxExp1.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxExp1.ThonsandsSeparator = true;
            this.numericBoxExp1.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.NumericBoxExp_ValueChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Name = "panel1";
            // 
            // buttonViewAlongBeam
            // 
            resources.ApplyResources(this.buttonViewAlongBeam, "buttonViewAlongBeam");
            this.buttonViewAlongBeam.Name = "buttonViewAlongBeam";
            this.buttonViewAlongBeam.UseVisualStyleBackColor = true;
            this.buttonViewAlongBeam.Click += new System.EventHandler(this.ButtonViewAlongBeam_Click);
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.checkBoxLink);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.buttonViewIsometric);
            this.panel2.Controls.Add(this.buttonViewAlongBeam);
            this.panel2.Name = "panel2";
            // 
            // FormRotationMatrix
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRotationMatrix";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRotationMatrix_FormClosing);
            this.Load += new System.EventHandler(this.FormRotationMatrix_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Crystallography.Controls.NumericBox numericBox11;
        private Crystallography.Controls.NumericBox numericBox31;
        private Crystallography.Controls.NumericBox numericBox21;
        private Crystallography.Controls.NumericBox numericBoxPhi;
        private Crystallography.Controls.NumericBox numericBox32;
        private Crystallography.Controls.NumericBox numericBox22;
        private Crystallography.Controls.NumericBox numericBoxTheta;
        private Crystallography.Controls.NumericBox numericBox33;
        private Crystallography.Controls.NumericBox numericBoxPsi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private Crystallography.Controls.NumericBox numericBox12;
        private Crystallography.Controls.NumericBox numericBox13;
        private Crystallography.Controls.NumericBox numericBox23;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonPaste;

        private System.Windows.Forms.Button buttonViewIsometric;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1stX;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
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
        private Crystallography.Controls.NumericBox numericBoxExp1;
        private Crystallography.Controls.NumericBox numericBoxExp2;
        private Crystallography.Controls.NumericBox numericBoxExp3;

        private System.Windows.Forms.CheckBox checkBoxLink;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel1;

        private System.Windows.Forms.Button buttonViewAlongBeam;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox checkBoxEnable3rd;
        private System.Windows.Forms.CheckBox checkBoxEnable2nd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxFix3rd;
        private System.Windows.Forms.CheckBox checkBoxFix2nd;
        private System.Windows.Forms.CheckBox checkBoxFix1st;
        private System.Windows.Forms.Button buttonResetExpEuler;
    }
}