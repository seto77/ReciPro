namespace Crystallography.Controls
{
    partial class FormStrain
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericBoxStrain11 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain13 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain22 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain23 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain33 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain12 = new Crystallography.Controls.NumericBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericBoxStress11 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress12 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress22 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress13 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress33 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress23 = new Crystallography.Controls.NumericBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.elasticityControl1 = new Crystallography.Controls.ElasticityControl();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericBoxA = new Crystallography.Controls.NumericBox();
            this.numericBoxAlpha = new Crystallography.Controls.NumericBox();
            this.numericBoxC = new Crystallography.Controls.NumericBox();
            this.numericBoxBeta = new Crystallography.Controls.NumericBox();
            this.numericBoxGamma = new Crystallography.Controls.NumericBox();
            this.numericBoxB = new Crystallography.Controls.NumericBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericBoxStrain11);
            this.groupBox1.Controls.Add(this.numericBoxStrain13);
            this.groupBox1.Controls.Add(this.numericBoxStrain22);
            this.groupBox1.Controls.Add(this.numericBoxStrain23);
            this.groupBox1.Controls.Add(this.numericBoxStrain33);
            this.groupBox1.Controls.Add(this.numericBoxStrain12);
            this.groupBox1.Location = new System.Drawing.Point(2, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 130);
            this.groupBox1.TabIndex = 294;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Strain";
            // 
            // numericBoxStrain11
            // 
            this.numericBoxStrain11.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain11.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain11.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain11.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain11.HeaderText = "ε11";
            this.numericBoxStrain11.Location = new System.Drawing.Point(4, 22);
            this.numericBoxStrain11.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain11.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStrain11.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain11.Name = "numericBoxStrain11";
            this.numericBoxStrain11.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain11.RestrictLimitValue = false;
            this.numericBoxStrain11.RoundErrorAccuracy = -1;
            this.numericBoxStrain11.Size = new System.Drawing.Size(90, 24);
            this.numericBoxStrain11.SkipEventDuringInput = false;
            this.numericBoxStrain11.SmartIncrement = true;
            this.numericBoxStrain11.TabIndex = 263;
            this.numericBoxStrain11.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain11.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxStrain_ValueChanged);
            // 
            // numericBoxStrain13
            // 
            this.numericBoxStrain13.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain13.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain13.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain13.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain13.HeaderText = "ε13";
            this.numericBoxStrain13.Location = new System.Drawing.Point(221, 22);
            this.numericBoxStrain13.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain13.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStrain13.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain13.Name = "numericBoxStrain13";
            this.numericBoxStrain13.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain13.RestrictLimitValue = false;
            this.numericBoxStrain13.RoundErrorAccuracy = -1;
            this.numericBoxStrain13.Size = new System.Drawing.Size(90, 24);
            this.numericBoxStrain13.SkipEventDuringInput = false;
            this.numericBoxStrain13.SmartIncrement = true;
            this.numericBoxStrain13.TabIndex = 265;
            this.numericBoxStrain13.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain13.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxStrain_ValueChanged);
            // 
            // numericBoxStrain22
            // 
            this.numericBoxStrain22.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain22.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain22.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain22.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain22.HeaderText = "ε22";
            this.numericBoxStrain22.Location = new System.Drawing.Point(111, 60);
            this.numericBoxStrain22.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain22.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStrain22.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain22.Name = "numericBoxStrain22";
            this.numericBoxStrain22.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain22.RestrictLimitValue = false;
            this.numericBoxStrain22.RoundErrorAccuracy = -1;
            this.numericBoxStrain22.Size = new System.Drawing.Size(90, 24);
            this.numericBoxStrain22.SkipEventDuringInput = false;
            this.numericBoxStrain22.SmartIncrement = true;
            this.numericBoxStrain22.TabIndex = 266;
            this.numericBoxStrain22.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain22.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxStrain_ValueChanged);
            // 
            // numericBoxStrain23
            // 
            this.numericBoxStrain23.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain23.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain23.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain23.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain23.HeaderText = "ε23";
            this.numericBoxStrain23.Location = new System.Drawing.Point(221, 60);
            this.numericBoxStrain23.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain23.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStrain23.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain23.Name = "numericBoxStrain23";
            this.numericBoxStrain23.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain23.RestrictLimitValue = false;
            this.numericBoxStrain23.RoundErrorAccuracy = -1;
            this.numericBoxStrain23.Size = new System.Drawing.Size(90, 24);
            this.numericBoxStrain23.SkipEventDuringInput = false;
            this.numericBoxStrain23.SmartIncrement = true;
            this.numericBoxStrain23.TabIndex = 267;
            this.numericBoxStrain23.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain23.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxStrain_ValueChanged);
            // 
            // numericBoxStrain33
            // 
            this.numericBoxStrain33.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain33.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain33.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain33.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain33.HeaderText = "ε33";
            this.numericBoxStrain33.Location = new System.Drawing.Point(221, 99);
            this.numericBoxStrain33.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain33.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStrain33.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain33.Name = "numericBoxStrain33";
            this.numericBoxStrain33.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain33.RestrictLimitValue = false;
            this.numericBoxStrain33.RoundErrorAccuracy = -1;
            this.numericBoxStrain33.Size = new System.Drawing.Size(90, 24);
            this.numericBoxStrain33.SkipEventDuringInput = false;
            this.numericBoxStrain33.SmartIncrement = true;
            this.numericBoxStrain33.TabIndex = 268;
            this.numericBoxStrain33.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain33.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxStrain_ValueChanged);
            // 
            // numericBoxStrain12
            // 
            this.numericBoxStrain12.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain12.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain12.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain12.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain12.HeaderText = "ε12";
            this.numericBoxStrain12.Location = new System.Drawing.Point(111, 22);
            this.numericBoxStrain12.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain12.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStrain12.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain12.Name = "numericBoxStrain12";
            this.numericBoxStrain12.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain12.RestrictLimitValue = false;
            this.numericBoxStrain12.RoundErrorAccuracy = -1;
            this.numericBoxStrain12.Size = new System.Drawing.Size(90, 24);
            this.numericBoxStrain12.SkipEventDuringInput = false;
            this.numericBoxStrain12.SmartIncrement = true;
            this.numericBoxStrain12.TabIndex = 264;
            this.numericBoxStrain12.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStrain12.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxStrain_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericBoxStress11);
            this.groupBox2.Controls.Add(this.numericBoxStress12);
            this.groupBox2.Controls.Add(this.numericBoxStress22);
            this.groupBox2.Controls.Add(this.numericBoxStress13);
            this.groupBox2.Controls.Add(this.numericBoxStress33);
            this.groupBox2.Controls.Add(this.numericBoxStress23);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(490, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 130);
            this.groupBox2.TabIndex = 295;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Stress";
            // 
            // numericBoxStress11
            // 
            this.numericBoxStress11.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress11.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStress11.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress11.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress11.HeaderText = "σ11";
            this.numericBoxStress11.Location = new System.Drawing.Point(4, 22);
            this.numericBoxStress11.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress11.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStress11.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress11.Name = "numericBoxStress11";
            this.numericBoxStress11.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress11.RestrictLimitValue = false;
            this.numericBoxStress11.RoundErrorAccuracy = -1;
            this.numericBoxStress11.Size = new System.Drawing.Size(71, 24);
            this.numericBoxStress11.SkipEventDuringInput = false;
            this.numericBoxStress11.SmartIncrement = true;
            this.numericBoxStress11.TabIndex = 269;
            this.numericBoxStress11.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStress12
            // 
            this.numericBoxStress12.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress12.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStress12.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress12.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress12.HeaderText = "σ12";
            this.numericBoxStress12.Location = new System.Drawing.Point(76, 22);
            this.numericBoxStress12.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress12.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStress12.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress12.Name = "numericBoxStress12";
            this.numericBoxStress12.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress12.RestrictLimitValue = false;
            this.numericBoxStress12.RoundErrorAccuracy = -1;
            this.numericBoxStress12.Size = new System.Drawing.Size(71, 24);
            this.numericBoxStress12.SkipEventDuringInput = false;
            this.numericBoxStress12.SmartIncrement = true;
            this.numericBoxStress12.TabIndex = 270;
            this.numericBoxStress12.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStress22
            // 
            this.numericBoxStress22.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress22.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStress22.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress22.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress22.HeaderText = "σ22";
            this.numericBoxStress22.Location = new System.Drawing.Point(76, 61);
            this.numericBoxStress22.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress22.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStress22.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress22.Name = "numericBoxStress22";
            this.numericBoxStress22.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress22.RestrictLimitValue = false;
            this.numericBoxStress22.RoundErrorAccuracy = -1;
            this.numericBoxStress22.Size = new System.Drawing.Size(71, 24);
            this.numericBoxStress22.SkipEventDuringInput = false;
            this.numericBoxStress22.SmartIncrement = true;
            this.numericBoxStress22.TabIndex = 272;
            this.numericBoxStress22.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStress13
            // 
            this.numericBoxStress13.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress13.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStress13.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress13.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress13.HeaderText = "σ13";
            this.numericBoxStress13.Location = new System.Drawing.Point(149, 22);
            this.numericBoxStress13.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress13.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStress13.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress13.Name = "numericBoxStress13";
            this.numericBoxStress13.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress13.RestrictLimitValue = false;
            this.numericBoxStress13.RoundErrorAccuracy = -1;
            this.numericBoxStress13.Size = new System.Drawing.Size(71, 24);
            this.numericBoxStress13.SkipEventDuringInput = false;
            this.numericBoxStress13.SmartIncrement = true;
            this.numericBoxStress13.TabIndex = 271;
            this.numericBoxStress13.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStress33
            // 
            this.numericBoxStress33.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress33.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStress33.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress33.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress33.HeaderText = "σ33";
            this.numericBoxStress33.Location = new System.Drawing.Point(149, 100);
            this.numericBoxStress33.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress33.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStress33.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress33.Name = "numericBoxStress33";
            this.numericBoxStress33.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress33.RestrictLimitValue = false;
            this.numericBoxStress33.RoundErrorAccuracy = -1;
            this.numericBoxStress33.Size = new System.Drawing.Size(71, 24);
            this.numericBoxStress33.SkipEventDuringInput = false;
            this.numericBoxStress33.SmartIncrement = true;
            this.numericBoxStress33.TabIndex = 274;
            this.numericBoxStress33.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStress23
            // 
            this.numericBoxStress23.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress23.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxStress23.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress23.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress23.HeaderText = "σ23";
            this.numericBoxStress23.Location = new System.Drawing.Point(149, 61);
            this.numericBoxStress23.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress23.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxStress23.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress23.Name = "numericBoxStress23";
            this.numericBoxStress23.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress23.RestrictLimitValue = false;
            this.numericBoxStress23.RoundErrorAccuracy = -1;
            this.numericBoxStress23.Size = new System.Drawing.Size(71, 24);
            this.numericBoxStress23.SkipEventDuringInput = false;
            this.numericBoxStress23.SmartIncrement = true;
            this.numericBoxStress23.TabIndex = 273;
            this.numericBoxStress23.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.elasticityControl1);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(6, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(478, 175);
            this.groupBox3.TabIndex = 296;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Elastic constant";
            // 
            // elasticityControl1
            // 
            this.elasticityControl1.AutoSize = true;
            this.elasticityControl1.Location = new System.Drawing.Point(6, 24);
            this.elasticityControl1.Mode = Crystallography.Elasticity.Mode.Stiffness;
            this.elasticityControl1.Name = "elasticityControl1";
            this.elasticityControl1.Size = new System.Drawing.Size(460, 145);
            this.elasticityControl1.SymmetrySeriesNumber = 1;
            this.elasticityControl1.TabIndex = 291;
            this.elasticityControl1.ValueChanged += new Crystallography.Controls.ElasticityControl.MyEventHandler(this.elasticityControl1_ValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numericBoxA);
            this.groupBox4.Controls.Add(this.numericBoxAlpha);
            this.groupBox4.Controls.Add(this.numericBoxC);
            this.groupBox4.Controls.Add(this.numericBoxBeta);
            this.groupBox4.Controls.Add(this.numericBoxGamma);
            this.groupBox4.Controls.Add(this.numericBoxB);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(339, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(230, 130);
            this.groupBox4.TabIndex = 297;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cell constants";
            // 
            // numericBoxA
            // 
            this.numericBoxA.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxA.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxA.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxA.HeaderText = "a";
            this.numericBoxA.Location = new System.Drawing.Point(4, 22);
            this.numericBoxA.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxA.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxA.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxA.Name = "numericBoxA";
            this.numericBoxA.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxA.RestrictLimitValue = false;
            this.numericBoxA.RoundErrorAccuracy = -1;
            this.numericBoxA.Size = new System.Drawing.Size(90, 24);
            this.numericBoxA.SkipEventDuringInput = false;
            this.numericBoxA.SmartIncrement = true;
            this.numericBoxA.TabIndex = 263;
            this.numericBoxA.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxAlpha
            // 
            this.numericBoxAlpha.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAlpha.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxAlpha.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAlpha.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAlpha.HeaderText = "α";
            this.numericBoxAlpha.Location = new System.Drawing.Point(115, 22);
            this.numericBoxAlpha.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxAlpha.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxAlpha.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxAlpha.Name = "numericBoxAlpha";
            this.numericBoxAlpha.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxAlpha.RestrictLimitValue = false;
            this.numericBoxAlpha.RoundErrorAccuracy = -1;
            this.numericBoxAlpha.Size = new System.Drawing.Size(90, 24);
            this.numericBoxAlpha.SkipEventDuringInput = false;
            this.numericBoxAlpha.SmartIncrement = true;
            this.numericBoxAlpha.TabIndex = 265;
            this.numericBoxAlpha.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxC
            // 
            this.numericBoxC.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxC.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxC.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxC.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxC.HeaderText = "c";
            this.numericBoxC.Location = new System.Drawing.Point(4, 99);
            this.numericBoxC.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxC.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxC.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxC.Name = "numericBoxC";
            this.numericBoxC.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxC.RestrictLimitValue = false;
            this.numericBoxC.RoundErrorAccuracy = -1;
            this.numericBoxC.Size = new System.Drawing.Size(90, 24);
            this.numericBoxC.SkipEventDuringInput = false;
            this.numericBoxC.SmartIncrement = true;
            this.numericBoxC.TabIndex = 266;
            this.numericBoxC.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxBeta
            // 
            this.numericBoxBeta.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBeta.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxBeta.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBeta.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBeta.HeaderText = "β";
            this.numericBoxBeta.Location = new System.Drawing.Point(115, 60);
            this.numericBoxBeta.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxBeta.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxBeta.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxBeta.Name = "numericBoxBeta";
            this.numericBoxBeta.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxBeta.RestrictLimitValue = false;
            this.numericBoxBeta.RoundErrorAccuracy = -1;
            this.numericBoxBeta.Size = new System.Drawing.Size(90, 24);
            this.numericBoxBeta.SkipEventDuringInput = false;
            this.numericBoxBeta.SmartIncrement = true;
            this.numericBoxBeta.TabIndex = 267;
            this.numericBoxBeta.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxGamma
            // 
            this.numericBoxGamma.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGamma.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxGamma.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGamma.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGamma.HeaderText = "γ";
            this.numericBoxGamma.Location = new System.Drawing.Point(115, 99);
            this.numericBoxGamma.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxGamma.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxGamma.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxGamma.Name = "numericBoxGamma";
            this.numericBoxGamma.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxGamma.RestrictLimitValue = false;
            this.numericBoxGamma.RoundErrorAccuracy = -1;
            this.numericBoxGamma.Size = new System.Drawing.Size(90, 24);
            this.numericBoxGamma.SkipEventDuringInput = false;
            this.numericBoxGamma.SmartIncrement = true;
            this.numericBoxGamma.TabIndex = 268;
            this.numericBoxGamma.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxB
            // 
            this.numericBoxB.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxB.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxB.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxB.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxB.HeaderText = "b";
            this.numericBoxB.Location = new System.Drawing.Point(4, 60);
            this.numericBoxB.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxB.MaximumSize = new System.Drawing.Size(1000, 24);
            this.numericBoxB.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxB.Name = "numericBoxB";
            this.numericBoxB.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxB.RestrictLimitValue = false;
            this.numericBoxB.RoundErrorAccuracy = -1;
            this.numericBoxB.Size = new System.Drawing.Size(90, 24);
            this.numericBoxB.SkipEventDuringInput = false;
            this.numericBoxB.SmartIncrement = true;
            this.numericBoxB.TabIndex = 264;
            this.numericBoxB.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // buttonApply
            // 
            this.buttonApply.AutoSize = true;
            this.buttonApply.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonApply.Location = new System.Drawing.Point(586, 115);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(51, 27);
            this.buttonApply.TabIndex = 298;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // FormStrain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(723, 327);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "FormStrain";
            this.Text = "Strain Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStrain_FormClosing);
            this.Load += new System.EventHandler(this.FormStrain_Load);
            this.VisibleChanged += new System.EventHandler(this.FormStrain_VisibleChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private NumericBox numericBoxStress33;
        private NumericBox numericBoxStress22;
        private NumericBox numericBoxStress11;
        private NumericBox numericBoxStress23;
        private NumericBox numericBoxStress13;
        private NumericBox numericBoxStress12;
        private NumericBox numericBoxStrain33;
        private NumericBox numericBoxStrain11;
        private NumericBox numericBoxStrain22;
        private NumericBox numericBoxStrain12;
        private NumericBox numericBoxStrain23;
        private NumericBox numericBoxStrain13;
        private ElasticityControl elasticityControl1;
        private NumericBox numericBoxA;
        private NumericBox numericBoxAlpha;
        private NumericBox numericBoxBeta;
        private NumericBox numericBoxB;
        private NumericBox numericBoxGamma;
        private NumericBox numericBoxC;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonApply;
    }
}