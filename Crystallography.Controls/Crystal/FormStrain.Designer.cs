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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStrain));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericBoxA = new Crystallography.Controls.NumericBox();
            this.numericBoxAlpha = new Crystallography.Controls.NumericBox();
            this.numericBoxC = new Crystallography.Controls.NumericBox();
            this.numericBoxBeta = new Crystallography.Controls.NumericBox();
            this.numericBoxGamma = new Crystallography.Controls.NumericBox();
            this.numericBoxB = new Crystallography.Controls.NumericBox();
            this.elasticityControl1 = new Crystallography.Controls.ElasticityControl();
            this.numericBoxStress11 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress12 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress22 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress13 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress33 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress23 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain11 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain13 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain22 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain23 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain33 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain12 = new Crystallography.Controls.NumericBox();
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
            this.numericBoxA.AllowMouseControl = false;
            this.numericBoxA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxA.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxA.DecimalPlaces = -1;
            this.numericBoxA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxA.FooterText = "";
            this.numericBoxA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxA.HeaderText = "a";
            this.numericBoxA.Location = new System.Drawing.Point(4, 22);
            this.numericBoxA.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxA.Maximum = double.PositiveInfinity;
            this.numericBoxA.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxA.Minimum = double.NegativeInfinity;
            this.numericBoxA.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxA.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxA.MouseSpeed = 1D;
            this.numericBoxA.Multiline = false;
            this.numericBoxA.Name = "numericBoxA";
            this.numericBoxA.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxA.RadianValue = 0D;
            this.numericBoxA.ReadOnly = false;
            this.numericBoxA.RestrictLimitValue = false;
            this.numericBoxA.ShowFraction = false;
            this.numericBoxA.ShowPositiveSign = false;
            this.numericBoxA.ShowUpDown = false;
            this.numericBoxA.Size = new System.Drawing.Size(90, 22);
            this.numericBoxA.SkipEventDuringInput = false;
            this.numericBoxA.SmartIncrement = true;
            this.numericBoxA.TabIndex = 263;
            this.numericBoxA.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxA.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxA.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxA.ThonsandsSeparator = false;
            this.numericBoxA.ToolTip = "";
            this.numericBoxA.UpDown_Increment = 1D;
            this.numericBoxA.Value = 0D;
            this.numericBoxA.WordWrap = true;
            // 
            // numericBoxAlpha
            // 
            this.numericBoxAlpha.AllowMouseControl = false;
            this.numericBoxAlpha.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxAlpha.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAlpha.DecimalPlaces = -1;
            this.numericBoxAlpha.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxAlpha.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxAlpha.FooterText = "";
            this.numericBoxAlpha.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxAlpha.HeaderText = "α";
            this.numericBoxAlpha.Location = new System.Drawing.Point(115, 22);
            this.numericBoxAlpha.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxAlpha.Maximum = double.PositiveInfinity;
            this.numericBoxAlpha.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxAlpha.Minimum = double.NegativeInfinity;
            this.numericBoxAlpha.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxAlpha.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxAlpha.MouseSpeed = 1D;
            this.numericBoxAlpha.Multiline = false;
            this.numericBoxAlpha.Name = "numericBoxAlpha";
            this.numericBoxAlpha.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxAlpha.RadianValue = 0D;
            this.numericBoxAlpha.ReadOnly = false;
            this.numericBoxAlpha.RestrictLimitValue = false;
            this.numericBoxAlpha.ShowFraction = false;
            this.numericBoxAlpha.ShowPositiveSign = false;
            this.numericBoxAlpha.ShowUpDown = false;
            this.numericBoxAlpha.Size = new System.Drawing.Size(90, 22);
            this.numericBoxAlpha.SkipEventDuringInput = false;
            this.numericBoxAlpha.SmartIncrement = true;
            this.numericBoxAlpha.TabIndex = 265;
            this.numericBoxAlpha.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxAlpha.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxAlpha.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxAlpha.ThonsandsSeparator = false;
            this.numericBoxAlpha.ToolTip = "";
            this.numericBoxAlpha.UpDown_Increment = 1D;
            this.numericBoxAlpha.Value = 0D;
            this.numericBoxAlpha.WordWrap = true;
            // 
            // numericBoxC
            // 
            this.numericBoxC.AllowMouseControl = false;
            this.numericBoxC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxC.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxC.DecimalPlaces = -1;
            this.numericBoxC.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxC.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxC.FooterText = "";
            this.numericBoxC.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxC.HeaderText = "c";
            this.numericBoxC.Location = new System.Drawing.Point(4, 99);
            this.numericBoxC.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxC.Maximum = double.PositiveInfinity;
            this.numericBoxC.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxC.Minimum = double.NegativeInfinity;
            this.numericBoxC.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxC.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxC.MouseSpeed = 1D;
            this.numericBoxC.Multiline = false;
            this.numericBoxC.Name = "numericBoxC";
            this.numericBoxC.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxC.RadianValue = 0D;
            this.numericBoxC.ReadOnly = false;
            this.numericBoxC.RestrictLimitValue = false;
            this.numericBoxC.ShowFraction = false;
            this.numericBoxC.ShowPositiveSign = false;
            this.numericBoxC.ShowUpDown = false;
            this.numericBoxC.Size = new System.Drawing.Size(90, 22);
            this.numericBoxC.SkipEventDuringInput = false;
            this.numericBoxC.SmartIncrement = true;
            this.numericBoxC.TabIndex = 266;
            this.numericBoxC.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxC.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxC.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxC.ThonsandsSeparator = false;
            this.numericBoxC.ToolTip = "";
            this.numericBoxC.UpDown_Increment = 1D;
            this.numericBoxC.Value = 0D;
            this.numericBoxC.WordWrap = true;
            // 
            // numericBoxBeta
            // 
            this.numericBoxBeta.AllowMouseControl = false;
            this.numericBoxBeta.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxBeta.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBeta.DecimalPlaces = -1;
            this.numericBoxBeta.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBeta.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBeta.FooterText = "";
            this.numericBoxBeta.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBeta.HeaderText = "β";
            this.numericBoxBeta.Location = new System.Drawing.Point(115, 60);
            this.numericBoxBeta.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxBeta.Maximum = double.PositiveInfinity;
            this.numericBoxBeta.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxBeta.Minimum = double.NegativeInfinity;
            this.numericBoxBeta.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxBeta.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxBeta.MouseSpeed = 1D;
            this.numericBoxBeta.Multiline = false;
            this.numericBoxBeta.Name = "numericBoxBeta";
            this.numericBoxBeta.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxBeta.RadianValue = 0D;
            this.numericBoxBeta.ReadOnly = false;
            this.numericBoxBeta.RestrictLimitValue = false;
            this.numericBoxBeta.ShowFraction = false;
            this.numericBoxBeta.ShowPositiveSign = false;
            this.numericBoxBeta.ShowUpDown = false;
            this.numericBoxBeta.Size = new System.Drawing.Size(90, 22);
            this.numericBoxBeta.SkipEventDuringInput = false;
            this.numericBoxBeta.SmartIncrement = true;
            this.numericBoxBeta.TabIndex = 267;
            this.numericBoxBeta.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBeta.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBeta.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxBeta.ThonsandsSeparator = false;
            this.numericBoxBeta.ToolTip = "";
            this.numericBoxBeta.UpDown_Increment = 1D;
            this.numericBoxBeta.Value = 0D;
            this.numericBoxBeta.WordWrap = true;
            // 
            // numericBoxGamma
            // 
            this.numericBoxGamma.AllowMouseControl = false;
            this.numericBoxGamma.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxGamma.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGamma.DecimalPlaces = -1;
            this.numericBoxGamma.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxGamma.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxGamma.FooterText = "";
            this.numericBoxGamma.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxGamma.HeaderText = "γ";
            this.numericBoxGamma.Location = new System.Drawing.Point(115, 99);
            this.numericBoxGamma.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxGamma.Maximum = double.PositiveInfinity;
            this.numericBoxGamma.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxGamma.Minimum = double.NegativeInfinity;
            this.numericBoxGamma.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxGamma.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxGamma.MouseSpeed = 1D;
            this.numericBoxGamma.Multiline = false;
            this.numericBoxGamma.Name = "numericBoxGamma";
            this.numericBoxGamma.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxGamma.RadianValue = 0D;
            this.numericBoxGamma.ReadOnly = false;
            this.numericBoxGamma.RestrictLimitValue = false;
            this.numericBoxGamma.ShowFraction = false;
            this.numericBoxGamma.ShowPositiveSign = false;
            this.numericBoxGamma.ShowUpDown = false;
            this.numericBoxGamma.Size = new System.Drawing.Size(90, 22);
            this.numericBoxGamma.SkipEventDuringInput = false;
            this.numericBoxGamma.SmartIncrement = true;
            this.numericBoxGamma.TabIndex = 268;
            this.numericBoxGamma.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxGamma.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxGamma.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxGamma.ThonsandsSeparator = false;
            this.numericBoxGamma.ToolTip = "";
            this.numericBoxGamma.UpDown_Increment = 1D;
            this.numericBoxGamma.Value = 0D;
            this.numericBoxGamma.WordWrap = true;
            // 
            // numericBoxB
            // 
            this.numericBoxB.AllowMouseControl = false;
            this.numericBoxB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxB.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxB.DecimalPlaces = -1;
            this.numericBoxB.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxB.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxB.FooterText = "";
            this.numericBoxB.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxB.HeaderText = "b";
            this.numericBoxB.Location = new System.Drawing.Point(4, 60);
            this.numericBoxB.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxB.Maximum = double.PositiveInfinity;
            this.numericBoxB.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxB.Minimum = double.NegativeInfinity;
            this.numericBoxB.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxB.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxB.MouseSpeed = 1D;
            this.numericBoxB.Multiline = false;
            this.numericBoxB.Name = "numericBoxB";
            this.numericBoxB.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxB.RadianValue = 0D;
            this.numericBoxB.ReadOnly = false;
            this.numericBoxB.RestrictLimitValue = false;
            this.numericBoxB.ShowFraction = false;
            this.numericBoxB.ShowPositiveSign = false;
            this.numericBoxB.ShowUpDown = false;
            this.numericBoxB.Size = new System.Drawing.Size(90, 22);
            this.numericBoxB.SkipEventDuringInput = false;
            this.numericBoxB.SmartIncrement = true;
            this.numericBoxB.TabIndex = 264;
            this.numericBoxB.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxB.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxB.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxB.ThonsandsSeparator = false;
            this.numericBoxB.ToolTip = "";
            this.numericBoxB.UpDown_Increment = 1D;
            this.numericBoxB.Value = 0D;
            this.numericBoxB.WordWrap = true;
            // 
            // elasticityControl1
            // 
            this.elasticityControl1.AutoSize = true;
            this.elasticityControl1.Location = new System.Drawing.Point(6, 24);
            this.elasticityControl1.Mode = Crystallography.Elasticity.Mode.Stiffness;
            this.elasticityControl1.Name = "elasticityControl1";
            this.elasticityControl1.Size = new System.Drawing.Size(460, 143);
            this.elasticityControl1.SymmetrySeriesNumber = 1;
            this.elasticityControl1.TabIndex = 291;
            this.elasticityControl1.ValueChanged += new Crystallography.Controls.ElasticityControl.MyEventHandler(this.elasticityControl1_ValueChanged);
            // 
            // numericalTextBoxStress11
            // 
            this.numericBoxStress11.AllowMouseControl = false;
            this.numericBoxStress11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStress11.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress11.DecimalPlaces = -1;
            this.numericBoxStress11.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress11.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress11.FooterText = "";
            this.numericBoxStress11.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress11.HeaderText = "σ11";
            this.numericBoxStress11.Location = new System.Drawing.Point(4, 22);
            this.numericBoxStress11.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress11.Maximum = double.PositiveInfinity;
            this.numericBoxStress11.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStress11.Minimum = double.NegativeInfinity;
            this.numericBoxStress11.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress11.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStress11.MouseSpeed = 1D;
            this.numericBoxStress11.Multiline = false;
            this.numericBoxStress11.Name = "numericalTextBoxStress11";
            this.numericBoxStress11.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress11.RadianValue = 0D;
            this.numericBoxStress11.ReadOnly = false;
            this.numericBoxStress11.RestrictLimitValue = false;
            this.numericBoxStress11.ShowFraction = false;
            this.numericBoxStress11.ShowPositiveSign = false;
            this.numericBoxStress11.ShowUpDown = false;
            this.numericBoxStress11.Size = new System.Drawing.Size(71, 22);
            this.numericBoxStress11.SkipEventDuringInput = false;
            this.numericBoxStress11.SmartIncrement = true;
            this.numericBoxStress11.TabIndex = 269;
            this.numericBoxStress11.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStress11.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStress11.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStress11.ThonsandsSeparator = false;
            this.numericBoxStress11.ToolTip = "";
            this.numericBoxStress11.UpDown_Increment = 1D;
            this.numericBoxStress11.Value = 0D;
            this.numericBoxStress11.WordWrap = true;
            // 
            // numericalTextBoxStress12
            // 
            this.numericBoxStress12.AllowMouseControl = false;
            this.numericBoxStress12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStress12.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress12.DecimalPlaces = -1;
            this.numericBoxStress12.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress12.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress12.FooterText = "";
            this.numericBoxStress12.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress12.HeaderText = "σ12";
            this.numericBoxStress12.Location = new System.Drawing.Point(76, 22);
            this.numericBoxStress12.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress12.Maximum = double.PositiveInfinity;
            this.numericBoxStress12.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStress12.Minimum = double.NegativeInfinity;
            this.numericBoxStress12.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress12.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStress12.MouseSpeed = 1D;
            this.numericBoxStress12.Multiline = false;
            this.numericBoxStress12.Name = "numericalTextBoxStress12";
            this.numericBoxStress12.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress12.RadianValue = 0D;
            this.numericBoxStress12.ReadOnly = false;
            this.numericBoxStress12.RestrictLimitValue = false;
            this.numericBoxStress12.ShowFraction = false;
            this.numericBoxStress12.ShowPositiveSign = false;
            this.numericBoxStress12.ShowUpDown = false;
            this.numericBoxStress12.Size = new System.Drawing.Size(71, 22);
            this.numericBoxStress12.SkipEventDuringInput = false;
            this.numericBoxStress12.SmartIncrement = true;
            this.numericBoxStress12.TabIndex = 270;
            this.numericBoxStress12.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStress12.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStress12.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStress12.ThonsandsSeparator = false;
            this.numericBoxStress12.ToolTip = "";
            this.numericBoxStress12.UpDown_Increment = 1D;
            this.numericBoxStress12.Value = 0D;
            this.numericBoxStress12.WordWrap = true;
            // 
            // numericalTextBoxStress22
            // 
            this.numericBoxStress22.AllowMouseControl = false;
            this.numericBoxStress22.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStress22.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress22.DecimalPlaces = -1;
            this.numericBoxStress22.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress22.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress22.FooterText = "";
            this.numericBoxStress22.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress22.HeaderText = "σ22";
            this.numericBoxStress22.Location = new System.Drawing.Point(76, 61);
            this.numericBoxStress22.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress22.Maximum = double.PositiveInfinity;
            this.numericBoxStress22.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStress22.Minimum = double.NegativeInfinity;
            this.numericBoxStress22.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress22.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStress22.MouseSpeed = 1D;
            this.numericBoxStress22.Multiline = false;
            this.numericBoxStress22.Name = "numericalTextBoxStress22";
            this.numericBoxStress22.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress22.RadianValue = 0D;
            this.numericBoxStress22.ReadOnly = false;
            this.numericBoxStress22.RestrictLimitValue = false;
            this.numericBoxStress22.ShowFraction = false;
            this.numericBoxStress22.ShowPositiveSign = false;
            this.numericBoxStress22.ShowUpDown = false;
            this.numericBoxStress22.Size = new System.Drawing.Size(71, 22);
            this.numericBoxStress22.SkipEventDuringInput = false;
            this.numericBoxStress22.SmartIncrement = true;
            this.numericBoxStress22.TabIndex = 272;
            this.numericBoxStress22.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStress22.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStress22.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStress22.ThonsandsSeparator = false;
            this.numericBoxStress22.ToolTip = "";
            this.numericBoxStress22.UpDown_Increment = 1D;
            this.numericBoxStress22.Value = 0D;
            this.numericBoxStress22.WordWrap = true;
            // 
            // numericalTextBoxStress13
            // 
            this.numericBoxStress13.AllowMouseControl = false;
            this.numericBoxStress13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStress13.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress13.DecimalPlaces = -1;
            this.numericBoxStress13.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress13.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress13.FooterText = "";
            this.numericBoxStress13.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress13.HeaderText = "σ13";
            this.numericBoxStress13.Location = new System.Drawing.Point(149, 22);
            this.numericBoxStress13.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress13.Maximum = double.PositiveInfinity;
            this.numericBoxStress13.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStress13.Minimum = double.NegativeInfinity;
            this.numericBoxStress13.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress13.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStress13.MouseSpeed = 1D;
            this.numericBoxStress13.Multiline = false;
            this.numericBoxStress13.Name = "numericalTextBoxStress13";
            this.numericBoxStress13.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress13.RadianValue = 0D;
            this.numericBoxStress13.ReadOnly = false;
            this.numericBoxStress13.RestrictLimitValue = false;
            this.numericBoxStress13.ShowFraction = false;
            this.numericBoxStress13.ShowPositiveSign = false;
            this.numericBoxStress13.ShowUpDown = false;
            this.numericBoxStress13.Size = new System.Drawing.Size(71, 22);
            this.numericBoxStress13.SkipEventDuringInput = false;
            this.numericBoxStress13.SmartIncrement = true;
            this.numericBoxStress13.TabIndex = 271;
            this.numericBoxStress13.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStress13.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStress13.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStress13.ThonsandsSeparator = false;
            this.numericBoxStress13.ToolTip = "";
            this.numericBoxStress13.UpDown_Increment = 1D;
            this.numericBoxStress13.Value = 0D;
            this.numericBoxStress13.WordWrap = true;
            // 
            // numericalTextBoxStress33
            // 
            this.numericBoxStress33.AllowMouseControl = false;
            this.numericBoxStress33.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStress33.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress33.DecimalPlaces = -1;
            this.numericBoxStress33.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress33.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress33.FooterText = "";
            this.numericBoxStress33.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress33.HeaderText = "σ33";
            this.numericBoxStress33.Location = new System.Drawing.Point(149, 100);
            this.numericBoxStress33.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress33.Maximum = double.PositiveInfinity;
            this.numericBoxStress33.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStress33.Minimum = double.NegativeInfinity;
            this.numericBoxStress33.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress33.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStress33.MouseSpeed = 1D;
            this.numericBoxStress33.Multiline = false;
            this.numericBoxStress33.Name = "numericalTextBoxStress33";
            this.numericBoxStress33.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress33.RadianValue = 0D;
            this.numericBoxStress33.ReadOnly = false;
            this.numericBoxStress33.RestrictLimitValue = false;
            this.numericBoxStress33.ShowFraction = false;
            this.numericBoxStress33.ShowPositiveSign = false;
            this.numericBoxStress33.ShowUpDown = false;
            this.numericBoxStress33.Size = new System.Drawing.Size(71, 22);
            this.numericBoxStress33.SkipEventDuringInput = false;
            this.numericBoxStress33.SmartIncrement = true;
            this.numericBoxStress33.TabIndex = 274;
            this.numericBoxStress33.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStress33.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStress33.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStress33.ThonsandsSeparator = false;
            this.numericBoxStress33.ToolTip = "";
            this.numericBoxStress33.UpDown_Increment = 1D;
            this.numericBoxStress33.Value = 0D;
            this.numericBoxStress33.WordWrap = true;
            // 
            // numericalTextBoxStress23
            // 
            this.numericBoxStress23.AllowMouseControl = false;
            this.numericBoxStress23.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStress23.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress23.DecimalPlaces = -1;
            this.numericBoxStress23.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress23.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress23.FooterText = "";
            this.numericBoxStress23.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStress23.HeaderText = "σ23";
            this.numericBoxStress23.Location = new System.Drawing.Point(149, 61);
            this.numericBoxStress23.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStress23.Maximum = double.PositiveInfinity;
            this.numericBoxStress23.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStress23.Minimum = double.NegativeInfinity;
            this.numericBoxStress23.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStress23.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStress23.MouseSpeed = 1D;
            this.numericBoxStress23.Multiline = false;
            this.numericBoxStress23.Name = "numericalTextBoxStress23";
            this.numericBoxStress23.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStress23.RadianValue = 0D;
            this.numericBoxStress23.ReadOnly = false;
            this.numericBoxStress23.RestrictLimitValue = false;
            this.numericBoxStress23.ShowFraction = false;
            this.numericBoxStress23.ShowPositiveSign = false;
            this.numericBoxStress23.ShowUpDown = false;
            this.numericBoxStress23.Size = new System.Drawing.Size(71, 22);
            this.numericBoxStress23.SkipEventDuringInput = false;
            this.numericBoxStress23.SmartIncrement = true;
            this.numericBoxStress23.TabIndex = 273;
            this.numericBoxStress23.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStress23.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStress23.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStress23.ThonsandsSeparator = false;
            this.numericBoxStress23.ToolTip = "";
            this.numericBoxStress23.UpDown_Increment = 1D;
            this.numericBoxStress23.Value = 0D;
            this.numericBoxStress23.WordWrap = true;
            // 
            // numericalTextBoxStrain11
            // 
            this.numericBoxStrain11.AllowMouseControl = true;
            this.numericBoxStrain11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStrain11.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain11.DecimalPlaces = -1;
            this.numericBoxStrain11.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain11.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain11.FooterText = "";
            this.numericBoxStrain11.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain11.HeaderText = "ε11";
            this.numericBoxStrain11.Location = new System.Drawing.Point(4, 22);
            this.numericBoxStrain11.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain11.Maximum = double.PositiveInfinity;
            this.numericBoxStrain11.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStrain11.Minimum = double.NegativeInfinity;
            this.numericBoxStrain11.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain11.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStrain11.MouseSpeed = 0.0001D;
            this.numericBoxStrain11.Multiline = false;
            this.numericBoxStrain11.Name = "numericalTextBoxStrain11";
            this.numericBoxStrain11.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain11.RadianValue = 0D;
            this.numericBoxStrain11.ReadOnly = false;
            this.numericBoxStrain11.RestrictLimitValue = false;
            this.numericBoxStrain11.ShowFraction = false;
            this.numericBoxStrain11.ShowPositiveSign = false;
            this.numericBoxStrain11.ShowUpDown = false;
            this.numericBoxStrain11.Size = new System.Drawing.Size(90, 22);
            this.numericBoxStrain11.SkipEventDuringInput = false;
            this.numericBoxStrain11.SmartIncrement = true;
            this.numericBoxStrain11.TabIndex = 263;
            this.numericBoxStrain11.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStrain11.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStrain11.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStrain11.ThonsandsSeparator = false;
            this.numericBoxStrain11.ToolTip = "";
            this.numericBoxStrain11.UpDown_Increment = 1D;
            this.numericBoxStrain11.Value = 0D;
            this.numericBoxStrain11.WordWrap = true;
            this.numericBoxStrain11.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxStrain_ValueChanged);
            // 
            // numericalTextBoxStrain13
            // 
            this.numericBoxStrain13.AllowMouseControl = true;
            this.numericBoxStrain13.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStrain13.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain13.DecimalPlaces = -1;
            this.numericBoxStrain13.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain13.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain13.FooterText = "";
            this.numericBoxStrain13.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain13.HeaderText = "ε13";
            this.numericBoxStrain13.Location = new System.Drawing.Point(221, 22);
            this.numericBoxStrain13.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain13.Maximum = double.PositiveInfinity;
            this.numericBoxStrain13.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStrain13.Minimum = double.NegativeInfinity;
            this.numericBoxStrain13.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain13.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStrain13.MouseSpeed = 0.0001D;
            this.numericBoxStrain13.Multiline = false;
            this.numericBoxStrain13.Name = "numericalTextBoxStrain13";
            this.numericBoxStrain13.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain13.RadianValue = 0D;
            this.numericBoxStrain13.ReadOnly = false;
            this.numericBoxStrain13.RestrictLimitValue = false;
            this.numericBoxStrain13.ShowFraction = false;
            this.numericBoxStrain13.ShowPositiveSign = false;
            this.numericBoxStrain13.ShowUpDown = false;
            this.numericBoxStrain13.Size = new System.Drawing.Size(90, 22);
            this.numericBoxStrain13.SkipEventDuringInput = false;
            this.numericBoxStrain13.SmartIncrement = true;
            this.numericBoxStrain13.TabIndex = 265;
            this.numericBoxStrain13.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStrain13.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStrain13.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStrain13.ThonsandsSeparator = false;
            this.numericBoxStrain13.ToolTip = "";
            this.numericBoxStrain13.UpDown_Increment = 1D;
            this.numericBoxStrain13.Value = 0D;
            this.numericBoxStrain13.WordWrap = true;
            this.numericBoxStrain13.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxStrain_ValueChanged);
            // 
            // numericalTextBoxStrain22
            // 
            this.numericBoxStrain22.AllowMouseControl = true;
            this.numericBoxStrain22.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStrain22.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain22.DecimalPlaces = -1;
            this.numericBoxStrain22.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain22.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain22.FooterText = "";
            this.numericBoxStrain22.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain22.HeaderText = "ε22";
            this.numericBoxStrain22.Location = new System.Drawing.Point(111, 60);
            this.numericBoxStrain22.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain22.Maximum = double.PositiveInfinity;
            this.numericBoxStrain22.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStrain22.Minimum = double.NegativeInfinity;
            this.numericBoxStrain22.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain22.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStrain22.MouseSpeed = 0.0001D;
            this.numericBoxStrain22.Multiline = false;
            this.numericBoxStrain22.Name = "numericalTextBoxStrain22";
            this.numericBoxStrain22.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain22.RadianValue = 0D;
            this.numericBoxStrain22.ReadOnly = false;
            this.numericBoxStrain22.RestrictLimitValue = false;
            this.numericBoxStrain22.ShowFraction = false;
            this.numericBoxStrain22.ShowPositiveSign = false;
            this.numericBoxStrain22.ShowUpDown = false;
            this.numericBoxStrain22.Size = new System.Drawing.Size(90, 22);
            this.numericBoxStrain22.SkipEventDuringInput = false;
            this.numericBoxStrain22.SmartIncrement = true;
            this.numericBoxStrain22.TabIndex = 266;
            this.numericBoxStrain22.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStrain22.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStrain22.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStrain22.ThonsandsSeparator = false;
            this.numericBoxStrain22.ToolTip = "";
            this.numericBoxStrain22.UpDown_Increment = 1D;
            this.numericBoxStrain22.Value = 0D;
            this.numericBoxStrain22.WordWrap = true;
            this.numericBoxStrain22.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxStrain_ValueChanged);
            // 
            // numericalTextBoxStrain23
            // 
            this.numericBoxStrain23.AllowMouseControl = true;
            this.numericBoxStrain23.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStrain23.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain23.DecimalPlaces = -1;
            this.numericBoxStrain23.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain23.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain23.FooterText = "";
            this.numericBoxStrain23.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain23.HeaderText = "ε23";
            this.numericBoxStrain23.Location = new System.Drawing.Point(221, 60);
            this.numericBoxStrain23.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain23.Maximum = double.PositiveInfinity;
            this.numericBoxStrain23.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStrain23.Minimum = double.NegativeInfinity;
            this.numericBoxStrain23.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain23.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStrain23.MouseSpeed = 0.0001D;
            this.numericBoxStrain23.Multiline = false;
            this.numericBoxStrain23.Name = "numericalTextBoxStrain23";
            this.numericBoxStrain23.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain23.RadianValue = 0D;
            this.numericBoxStrain23.ReadOnly = false;
            this.numericBoxStrain23.RestrictLimitValue = false;
            this.numericBoxStrain23.ShowFraction = false;
            this.numericBoxStrain23.ShowPositiveSign = false;
            this.numericBoxStrain23.ShowUpDown = false;
            this.numericBoxStrain23.Size = new System.Drawing.Size(90, 22);
            this.numericBoxStrain23.SkipEventDuringInput = false;
            this.numericBoxStrain23.SmartIncrement = true;
            this.numericBoxStrain23.TabIndex = 267;
            this.numericBoxStrain23.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStrain23.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStrain23.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStrain23.ThonsandsSeparator = false;
            this.numericBoxStrain23.ToolTip = "";
            this.numericBoxStrain23.UpDown_Increment = 1D;
            this.numericBoxStrain23.Value = 0D;
            this.numericBoxStrain23.WordWrap = true;
            this.numericBoxStrain23.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxStrain_ValueChanged);
            // 
            // numericalTextBoxStrain33
            // 
            this.numericBoxStrain33.AllowMouseControl = true;
            this.numericBoxStrain33.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStrain33.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain33.DecimalPlaces = -1;
            this.numericBoxStrain33.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain33.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain33.FooterText = "";
            this.numericBoxStrain33.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain33.HeaderText = "ε33";
            this.numericBoxStrain33.Location = new System.Drawing.Point(221, 99);
            this.numericBoxStrain33.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain33.Maximum = double.PositiveInfinity;
            this.numericBoxStrain33.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStrain33.Minimum = double.NegativeInfinity;
            this.numericBoxStrain33.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain33.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStrain33.MouseSpeed = 0.0001D;
            this.numericBoxStrain33.Multiline = false;
            this.numericBoxStrain33.Name = "numericalTextBoxStrain33";
            this.numericBoxStrain33.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain33.RadianValue = 0D;
            this.numericBoxStrain33.ReadOnly = false;
            this.numericBoxStrain33.RestrictLimitValue = false;
            this.numericBoxStrain33.ShowFraction = false;
            this.numericBoxStrain33.ShowPositiveSign = false;
            this.numericBoxStrain33.ShowUpDown = false;
            this.numericBoxStrain33.Size = new System.Drawing.Size(90, 22);
            this.numericBoxStrain33.SkipEventDuringInput = false;
            this.numericBoxStrain33.SmartIncrement = true;
            this.numericBoxStrain33.TabIndex = 268;
            this.numericBoxStrain33.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStrain33.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStrain33.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStrain33.ThonsandsSeparator = false;
            this.numericBoxStrain33.ToolTip = "";
            this.numericBoxStrain33.UpDown_Increment = 1D;
            this.numericBoxStrain33.Value = 0D;
            this.numericBoxStrain33.WordWrap = true;
            this.numericBoxStrain33.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxStrain_ValueChanged);
            // 
            // numericalTextBoxStrain12
            // 
            this.numericBoxStrain12.AllowMouseControl = true;
            this.numericBoxStrain12.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxStrain12.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain12.DecimalPlaces = -1;
            this.numericBoxStrain12.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain12.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain12.FooterText = "";
            this.numericBoxStrain12.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxStrain12.HeaderText = "ε12";
            this.numericBoxStrain12.Location = new System.Drawing.Point(111, 22);
            this.numericBoxStrain12.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxStrain12.Maximum = double.PositiveInfinity;
            this.numericBoxStrain12.MaximumSize = new System.Drawing.Size(1000, 22);
            this.numericBoxStrain12.Minimum = double.NegativeInfinity;
            this.numericBoxStrain12.MinimumSize = new System.Drawing.Size(1, 22);
            this.numericBoxStrain12.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxStrain12.MouseSpeed = 0.0001D;
            this.numericBoxStrain12.Multiline = false;
            this.numericBoxStrain12.Name = "numericalTextBoxStrain12";
            this.numericBoxStrain12.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxStrain12.RadianValue = 0D;
            this.numericBoxStrain12.ReadOnly = false;
            this.numericBoxStrain12.RestrictLimitValue = false;
            this.numericBoxStrain12.ShowFraction = false;
            this.numericBoxStrain12.ShowPositiveSign = false;
            this.numericBoxStrain12.ShowUpDown = false;
            this.numericBoxStrain12.Size = new System.Drawing.Size(90, 22);
            this.numericBoxStrain12.SkipEventDuringInput = false;
            this.numericBoxStrain12.SmartIncrement = true;
            this.numericBoxStrain12.TabIndex = 264;
            this.numericBoxStrain12.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxStrain12.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxStrain12.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxStrain12.ThonsandsSeparator = false;
            this.numericBoxStrain12.ToolTip = "";
            this.numericBoxStrain12.UpDown_Increment = 1D;
            this.numericBoxStrain12.Value = 0D;
            this.numericBoxStrain12.WordWrap = true;
            this.numericBoxStrain12.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxStrain_ValueChanged);
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(723, 327);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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