namespace ReciPro
{
    partial class FormDiffractionSimulatorDynamicCompression
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericBoxCompressedThetaB = new Crystallography.Controls.NumericBox();
            this.numericBoxCompressedOmega = new Crystallography.Controls.NumericBox();
            this.numericBoxCompressedThetaA = new Crystallography.Controls.NumericBox();
            this.numericBoxCompressedOmegaSigma = new Crystallography.Controls.NumericBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericBoxReleasedThetaB = new Crystallography.Controls.NumericBox();
            this.numericBoxReleasedOmegaSigma = new Crystallography.Controls.NumericBox();
            this.numericBoxReleasedOmega = new Crystallography.Controls.NumericBox();
            this.numericBoxReleasedThetaA = new Crystallography.Controls.NumericBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericBoxEOS_K0 = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_Kprime = new Crystallography.Controls.NumericBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericBoxShockedPlaneH = new Crystallography.Controls.NumericBox();
            this.numericBoxShockedPlaneK = new Crystallography.Controls.NumericBox();
            this.numericBoxShockedPlaneL = new Crystallography.Controls.NumericBox();
            this.buttonExecute = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.checkBoxSkipDrawing = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButtonCompressedIsotropic = new System.Windows.Forms.RadioButton();
            this.radioButtonCompressedUniaxial = new System.Windows.Forms.RadioButton();
            this.numericBoxUp = new Crystallography.Controls.NumericBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButtonReleasedIsotropic = new System.Windows.Forms.RadioButton();
            this.numericBoxUr = new Crystallography.Controls.NumericBox();
            this.radioButtonReleasedUniaxial = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.numericBoxMassAbsorption = new Crystallography.Controls.NumericBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxSaveSimulatedPattern = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonSetFolder = new System.Windows.Forms.Button();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.flowLayoutPanelSavePatterns = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelOmegaStep = new System.Windows.Forms.FlowLayoutPanel();
            this.numericBoxOmegaStep = new Crystallography.Controls.NumericBox();
            this.numericBoxOmegaTimes = new Crystallography.Controls.NumericBox();
            this.checkBoxOmegaStep = new System.Windows.Forms.CheckBox();
            this.numericBoxDivisionOfRotationAngle = new Crystallography.Controls.NumericBox();
            this.numericBoxDivisionOfRotationAxis = new Crystallography.Controls.NumericBox();
            this.trackBarAdvancedBack = new Crystallography.Controls.TrackBarAdvanced();
            this.trackBarAdvancedTime = new Crystallography.Controls.TrackBarAdvanced();
            this.trackBarAdvancedFront = new Crystallography.Controls.TrackBarAdvanced();
            this.graphControl = new Crystallography.Controls.GraphControl();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.radioButton2019Model = new System.Windows.Forms.RadioButton();
            this.radioButton2018Model = new System.Windows.Forms.RadioButton();
            this.groupBoxSlipPlane = new System.Windows.Forms.GroupBox();
            this.numericBoxSlipPlaneH = new Crystallography.Controls.NumericBox();
            this.numericBoxSlipPlaneK = new Crystallography.Controls.NumericBox();
            this.numericBoxSlipPlaneL = new Crystallography.Controls.NumericBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.flowLayoutPanelSavePatterns.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.flowLayoutPanelOmegaStep.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBoxSlipPlane.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "h        k        l";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 557);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Front";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 590);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Back";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.numericBoxCompressedThetaB);
            this.groupBox1.Controls.Add(this.numericBoxCompressedOmega);
            this.groupBox1.Controls.Add(this.numericBoxCompressedThetaA);
            this.groupBox1.Controls.Add(this.numericBoxCompressedOmegaSigma);
            this.groupBox1.Location = new System.Drawing.Point(12, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(143, 135);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rotation distribution";
            // 
            // numericBoxCompressedThetaB
            // 
            this.numericBoxCompressedThetaB.AllowMouseControl = false;
            this.numericBoxCompressedThetaB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCompressedThetaB.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCompressedThetaB.DecimalPlaces = -2;
            this.numericBoxCompressedThetaB.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedThetaB.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedThetaB.FooterText = "deg/ns";
            this.numericBoxCompressedThetaB.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedThetaB.HeaderText = "b";
            this.numericBoxCompressedThetaB.Location = new System.Drawing.Point(17, 104);
            this.numericBoxCompressedThetaB.Margin = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.numericBoxCompressedThetaB.Maximum = 10000D;
            this.numericBoxCompressedThetaB.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCompressedThetaB.Minimum = 0D;
            this.numericBoxCompressedThetaB.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCompressedThetaB.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxCompressedThetaB.MouseSpeed = 1D;
            this.numericBoxCompressedThetaB.Multiline = false;
            this.numericBoxCompressedThetaB.Name = "numericBoxCompressedThetaB";
            this.numericBoxCompressedThetaB.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCompressedThetaB.RadianValue = 0.0087266462599716477D;
            this.numericBoxCompressedThetaB.ReadOnly = false;
            this.numericBoxCompressedThetaB.RestrictLimitValue = true;
            this.numericBoxCompressedThetaB.ShowFraction = false;
            this.numericBoxCompressedThetaB.ShowPositiveSign = false;
            this.numericBoxCompressedThetaB.ShowUpDown = false;
            this.numericBoxCompressedThetaB.Size = new System.Drawing.Size(120, 25);
            this.numericBoxCompressedThetaB.SkipEventDuringInput = false;
            this.numericBoxCompressedThetaB.SmartIncrement = true;
            this.numericBoxCompressedThetaB.TabIndex = 2;
            this.numericBoxCompressedThetaB.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCompressedThetaB.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCompressedThetaB.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedThetaB.ThonsandsSeparator = true;
            this.numericBoxCompressedThetaB.ToolTip = "";
            this.numericBoxCompressedThetaB.UpDown_Increment = 1D;
            this.numericBoxCompressedThetaB.Value = 0.5D;
            this.numericBoxCompressedThetaB.WordWrap = true;
            // 
            // numericBoxCompressedOmega
            // 
            this.numericBoxCompressedOmega.AllowMouseControl = false;
            this.numericBoxCompressedOmega.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCompressedOmega.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCompressedOmega.DecimalPlaces = -2;
            this.numericBoxCompressedOmega.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedOmega.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedOmega.FooterText = "deg/ns";
            this.numericBoxCompressedOmega.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedOmega.HeaderText = "ω";
            this.numericBoxCompressedOmega.Location = new System.Drawing.Point(14, 21);
            this.numericBoxCompressedOmega.Margin = new System.Windows.Forms.Padding(12, 0, 0, 3);
            this.numericBoxCompressedOmega.Maximum = 10000D;
            this.numericBoxCompressedOmega.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCompressedOmega.Minimum = 0D;
            this.numericBoxCompressedOmega.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCompressedOmega.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxCompressedOmega.MouseSpeed = 1D;
            this.numericBoxCompressedOmega.Multiline = false;
            this.numericBoxCompressedOmega.Name = "numericBoxCompressedOmega";
            this.numericBoxCompressedOmega.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCompressedOmega.RadianValue = 0.00017453292519943296D;
            this.numericBoxCompressedOmega.ReadOnly = false;
            this.numericBoxCompressedOmega.RestrictLimitValue = true;
            this.numericBoxCompressedOmega.ShowFraction = false;
            this.numericBoxCompressedOmega.ShowPositiveSign = false;
            this.numericBoxCompressedOmega.ShowUpDown = false;
            this.numericBoxCompressedOmega.Size = new System.Drawing.Size(124, 25);
            this.numericBoxCompressedOmega.SkipEventDuringInput = false;
            this.numericBoxCompressedOmega.SmartIncrement = true;
            this.numericBoxCompressedOmega.TabIndex = 2;
            this.numericBoxCompressedOmega.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCompressedOmega.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCompressedOmega.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedOmega.ThonsandsSeparator = true;
            this.numericBoxCompressedOmega.ToolTip = "";
            this.numericBoxCompressedOmega.UpDown_Increment = 1D;
            this.numericBoxCompressedOmega.Value = 0.01D;
            this.numericBoxCompressedOmega.WordWrap = true;
            // 
            // numericBoxCompressedThetaA
            // 
            this.numericBoxCompressedThetaA.AllowMouseControl = false;
            this.numericBoxCompressedThetaA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCompressedThetaA.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCompressedThetaA.DecimalPlaces = -2;
            this.numericBoxCompressedThetaA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedThetaA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedThetaA.FooterText = "deg";
            this.numericBoxCompressedThetaA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedThetaA.HeaderText = "a";
            this.numericBoxCompressedThetaA.Location = new System.Drawing.Point(18, 77);
            this.numericBoxCompressedThetaA.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.numericBoxCompressedThetaA.Maximum = 10000D;
            this.numericBoxCompressedThetaA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCompressedThetaA.Minimum = 0D;
            this.numericBoxCompressedThetaA.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCompressedThetaA.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxCompressedThetaA.MouseSpeed = 1D;
            this.numericBoxCompressedThetaA.Multiline = false;
            this.numericBoxCompressedThetaA.Name = "numericBoxCompressedThetaA";
            this.numericBoxCompressedThetaA.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCompressedThetaA.RadianValue = 0D;
            this.numericBoxCompressedThetaA.ReadOnly = false;
            this.numericBoxCompressedThetaA.RestrictLimitValue = true;
            this.numericBoxCompressedThetaA.ShowFraction = false;
            this.numericBoxCompressedThetaA.ShowPositiveSign = false;
            this.numericBoxCompressedThetaA.ShowUpDown = false;
            this.numericBoxCompressedThetaA.Size = new System.Drawing.Size(102, 25);
            this.numericBoxCompressedThetaA.SkipEventDuringInput = false;
            this.numericBoxCompressedThetaA.SmartIncrement = true;
            this.numericBoxCompressedThetaA.TabIndex = 2;
            this.numericBoxCompressedThetaA.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCompressedThetaA.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCompressedThetaA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedThetaA.ThonsandsSeparator = true;
            this.numericBoxCompressedThetaA.ToolTip = "";
            this.numericBoxCompressedThetaA.UpDown_Increment = 1D;
            this.numericBoxCompressedThetaA.Value = 0D;
            this.numericBoxCompressedThetaA.WordWrap = true;
            // 
            // numericBoxCompressedOmegaSigma
            // 
            this.numericBoxCompressedOmegaSigma.AllowMouseControl = false;
            this.numericBoxCompressedOmegaSigma.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCompressedOmegaSigma.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCompressedOmegaSigma.DecimalPlaces = -2;
            this.numericBoxCompressedOmegaSigma.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedOmegaSigma.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedOmegaSigma.FooterText = "deg/ns";
            this.numericBoxCompressedOmegaSigma.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedOmegaSigma.HeaderText = "σ_ω";
            this.numericBoxCompressedOmegaSigma.Location = new System.Drawing.Point(2, 49);
            this.numericBoxCompressedOmegaSigma.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.numericBoxCompressedOmegaSigma.Maximum = 10000D;
            this.numericBoxCompressedOmegaSigma.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCompressedOmegaSigma.Minimum = 0D;
            this.numericBoxCompressedOmegaSigma.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCompressedOmegaSigma.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxCompressedOmegaSigma.MouseSpeed = 1D;
            this.numericBoxCompressedOmegaSigma.Multiline = false;
            this.numericBoxCompressedOmegaSigma.Name = "numericBoxCompressedOmegaSigma";
            this.numericBoxCompressedOmegaSigma.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCompressedOmegaSigma.RadianValue = 0.00017453292519943296D;
            this.numericBoxCompressedOmegaSigma.ReadOnly = false;
            this.numericBoxCompressedOmegaSigma.RestrictLimitValue = true;
            this.numericBoxCompressedOmegaSigma.ShowFraction = false;
            this.numericBoxCompressedOmegaSigma.ShowPositiveSign = false;
            this.numericBoxCompressedOmegaSigma.ShowUpDown = false;
            this.numericBoxCompressedOmegaSigma.Size = new System.Drawing.Size(136, 25);
            this.numericBoxCompressedOmegaSigma.SkipEventDuringInput = false;
            this.numericBoxCompressedOmegaSigma.SmartIncrement = true;
            this.numericBoxCompressedOmegaSigma.TabIndex = 2;
            this.numericBoxCompressedOmegaSigma.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCompressedOmegaSigma.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCompressedOmegaSigma.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCompressedOmegaSigma.ThonsandsSeparator = true;
            this.numericBoxCompressedOmegaSigma.ToolTip = "";
            this.numericBoxCompressedOmegaSigma.UpDown_Increment = 1D;
            this.numericBoxCompressedOmegaSigma.Value = 0.01D;
            this.numericBoxCompressedOmegaSigma.WordWrap = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericBoxReleasedThetaB);
            this.groupBox2.Controls.Add(this.numericBoxReleasedOmegaSigma);
            this.groupBox2.Controls.Add(this.numericBoxReleasedOmega);
            this.groupBox2.Controls.Add(this.numericBoxReleasedThetaA);
            this.groupBox2.Location = new System.Drawing.Point(7, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(143, 135);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rotation distribution";
            // 
            // numericBoxReleasedThetaB
            // 
            this.numericBoxReleasedThetaB.AllowMouseControl = false;
            this.numericBoxReleasedThetaB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxReleasedThetaB.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxReleasedThetaB.DecimalPlaces = -2;
            this.numericBoxReleasedThetaB.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedThetaB.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedThetaB.FooterText = "deg/ns";
            this.numericBoxReleasedThetaB.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedThetaB.HeaderText = "b";
            this.numericBoxReleasedThetaB.Location = new System.Drawing.Point(18, 104);
            this.numericBoxReleasedThetaB.Margin = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.numericBoxReleasedThetaB.Maximum = 10000D;
            this.numericBoxReleasedThetaB.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxReleasedThetaB.Minimum = 0D;
            this.numericBoxReleasedThetaB.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxReleasedThetaB.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxReleasedThetaB.MouseSpeed = 1D;
            this.numericBoxReleasedThetaB.Multiline = false;
            this.numericBoxReleasedThetaB.Name = "numericBoxReleasedThetaB";
            this.numericBoxReleasedThetaB.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxReleasedThetaB.RadianValue = 0.0087266462599716477D;
            this.numericBoxReleasedThetaB.ReadOnly = false;
            this.numericBoxReleasedThetaB.RestrictLimitValue = true;
            this.numericBoxReleasedThetaB.ShowFraction = false;
            this.numericBoxReleasedThetaB.ShowPositiveSign = false;
            this.numericBoxReleasedThetaB.ShowUpDown = false;
            this.numericBoxReleasedThetaB.Size = new System.Drawing.Size(120, 25);
            this.numericBoxReleasedThetaB.SkipEventDuringInput = false;
            this.numericBoxReleasedThetaB.SmartIncrement = true;
            this.numericBoxReleasedThetaB.TabIndex = 2;
            this.numericBoxReleasedThetaB.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxReleasedThetaB.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxReleasedThetaB.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedThetaB.ThonsandsSeparator = true;
            this.numericBoxReleasedThetaB.ToolTip = "";
            this.numericBoxReleasedThetaB.UpDown_Increment = 1D;
            this.numericBoxReleasedThetaB.Value = 0.5D;
            this.numericBoxReleasedThetaB.WordWrap = true;
            // 
            // numericBoxReleasedOmegaSigma
            // 
            this.numericBoxReleasedOmegaSigma.AllowMouseControl = false;
            this.numericBoxReleasedOmegaSigma.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxReleasedOmegaSigma.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxReleasedOmegaSigma.DecimalPlaces = -2;
            this.numericBoxReleasedOmegaSigma.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedOmegaSigma.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedOmegaSigma.FooterText = "deg/ns";
            this.numericBoxReleasedOmegaSigma.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedOmegaSigma.HeaderText = "σ_ω";
            this.numericBoxReleasedOmegaSigma.Location = new System.Drawing.Point(4, 49);
            this.numericBoxReleasedOmegaSigma.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxReleasedOmegaSigma.Maximum = 10000D;
            this.numericBoxReleasedOmegaSigma.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxReleasedOmegaSigma.Minimum = 0D;
            this.numericBoxReleasedOmegaSigma.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxReleasedOmegaSigma.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxReleasedOmegaSigma.MouseSpeed = 1D;
            this.numericBoxReleasedOmegaSigma.Multiline = false;
            this.numericBoxReleasedOmegaSigma.Name = "numericBoxReleasedOmegaSigma";
            this.numericBoxReleasedOmegaSigma.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxReleasedOmegaSigma.RadianValue = 0.0087266462599716477D;
            this.numericBoxReleasedOmegaSigma.ReadOnly = false;
            this.numericBoxReleasedOmegaSigma.RestrictLimitValue = true;
            this.numericBoxReleasedOmegaSigma.ShowFraction = false;
            this.numericBoxReleasedOmegaSigma.ShowPositiveSign = false;
            this.numericBoxReleasedOmegaSigma.ShowUpDown = false;
            this.numericBoxReleasedOmegaSigma.Size = new System.Drawing.Size(136, 25);
            this.numericBoxReleasedOmegaSigma.SkipEventDuringInput = false;
            this.numericBoxReleasedOmegaSigma.SmartIncrement = true;
            this.numericBoxReleasedOmegaSigma.TabIndex = 2;
            this.numericBoxReleasedOmegaSigma.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxReleasedOmegaSigma.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxReleasedOmegaSigma.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedOmegaSigma.ThonsandsSeparator = true;
            this.numericBoxReleasedOmegaSigma.ToolTip = "";
            this.numericBoxReleasedOmegaSigma.UpDown_Increment = 1D;
            this.numericBoxReleasedOmegaSigma.Value = 0.5D;
            this.numericBoxReleasedOmegaSigma.WordWrap = true;
            // 
            // numericBoxReleasedOmega
            // 
            this.numericBoxReleasedOmega.AllowMouseControl = false;
            this.numericBoxReleasedOmega.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxReleasedOmega.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxReleasedOmega.DecimalPlaces = -2;
            this.numericBoxReleasedOmega.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedOmega.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedOmega.FooterText = "deg/ns";
            this.numericBoxReleasedOmega.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedOmega.HeaderText = "ω";
            this.numericBoxReleasedOmega.Location = new System.Drawing.Point(16, 21);
            this.numericBoxReleasedOmega.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxReleasedOmega.Maximum = 10000D;
            this.numericBoxReleasedOmega.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxReleasedOmega.Minimum = 0D;
            this.numericBoxReleasedOmega.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxReleasedOmega.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxReleasedOmega.MouseSpeed = 1D;
            this.numericBoxReleasedOmega.Multiline = false;
            this.numericBoxReleasedOmega.Name = "numericBoxReleasedOmega";
            this.numericBoxReleasedOmega.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxReleasedOmega.RadianValue = 0.0087266462599716477D;
            this.numericBoxReleasedOmega.ReadOnly = false;
            this.numericBoxReleasedOmega.RestrictLimitValue = true;
            this.numericBoxReleasedOmega.ShowFraction = false;
            this.numericBoxReleasedOmega.ShowPositiveSign = false;
            this.numericBoxReleasedOmega.ShowUpDown = false;
            this.numericBoxReleasedOmega.Size = new System.Drawing.Size(124, 25);
            this.numericBoxReleasedOmega.SkipEventDuringInput = false;
            this.numericBoxReleasedOmega.SmartIncrement = true;
            this.numericBoxReleasedOmega.TabIndex = 2;
            this.numericBoxReleasedOmega.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxReleasedOmega.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxReleasedOmega.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedOmega.ThonsandsSeparator = true;
            this.numericBoxReleasedOmega.ToolTip = "";
            this.numericBoxReleasedOmega.UpDown_Increment = 1D;
            this.numericBoxReleasedOmega.Value = 0.5D;
            this.numericBoxReleasedOmega.WordWrap = true;
            // 
            // numericBoxReleasedThetaA
            // 
            this.numericBoxReleasedThetaA.AllowMouseControl = false;
            this.numericBoxReleasedThetaA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxReleasedThetaA.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxReleasedThetaA.DecimalPlaces = -2;
            this.numericBoxReleasedThetaA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedThetaA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedThetaA.FooterText = "deg";
            this.numericBoxReleasedThetaA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedThetaA.HeaderText = "a";
            this.numericBoxReleasedThetaA.Location = new System.Drawing.Point(20, 77);
            this.numericBoxReleasedThetaA.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.numericBoxReleasedThetaA.Maximum = 10000D;
            this.numericBoxReleasedThetaA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxReleasedThetaA.Minimum = 0D;
            this.numericBoxReleasedThetaA.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxReleasedThetaA.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxReleasedThetaA.MouseSpeed = 1D;
            this.numericBoxReleasedThetaA.Multiline = false;
            this.numericBoxReleasedThetaA.Name = "numericBoxReleasedThetaA";
            this.numericBoxReleasedThetaA.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxReleasedThetaA.RadianValue = 0D;
            this.numericBoxReleasedThetaA.ReadOnly = false;
            this.numericBoxReleasedThetaA.RestrictLimitValue = true;
            this.numericBoxReleasedThetaA.ShowFraction = false;
            this.numericBoxReleasedThetaA.ShowPositiveSign = false;
            this.numericBoxReleasedThetaA.ShowUpDown = false;
            this.numericBoxReleasedThetaA.Size = new System.Drawing.Size(102, 25);
            this.numericBoxReleasedThetaA.SkipEventDuringInput = false;
            this.numericBoxReleasedThetaA.SmartIncrement = true;
            this.numericBoxReleasedThetaA.TabIndex = 2;
            this.numericBoxReleasedThetaA.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxReleasedThetaA.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxReleasedThetaA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxReleasedThetaA.ThonsandsSeparator = true;
            this.numericBoxReleasedThetaA.ToolTip = "";
            this.numericBoxReleasedThetaA.UpDown_Increment = 1D;
            this.numericBoxReleasedThetaA.Value = 0D;
            this.numericBoxReleasedThetaA.WordWrap = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericBoxEOS_K0);
            this.groupBox3.Controls.Add(this.numericBoxEOS_Kprime);
            this.groupBox3.Location = new System.Drawing.Point(12, 87);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(141, 74);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EOS";
            // 
            // numericBoxEOS_K0
            // 
            this.numericBoxEOS_K0.AllowMouseControl = false;
            this.numericBoxEOS_K0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_K0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_K0.DecimalPlaces = -2;
            this.numericBoxEOS_K0.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxEOS_K0.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxEOS_K0.FooterText = "GPa";
            this.numericBoxEOS_K0.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxEOS_K0.HeaderText = "K0";
            this.numericBoxEOS_K0.Location = new System.Drawing.Point(14, 18);
            this.numericBoxEOS_K0.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_K0.Maximum = 10000D;
            this.numericBoxEOS_K0.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_K0.Minimum = 0D;
            this.numericBoxEOS_K0.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxEOS_K0.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_K0.MouseSpeed = 1D;
            this.numericBoxEOS_K0.Multiline = false;
            this.numericBoxEOS_K0.Name = "numericBoxEOS_K0";
            this.numericBoxEOS_K0.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxEOS_K0.RadianValue = 0.647866218340295D;
            this.numericBoxEOS_K0.ReadOnly = false;
            this.numericBoxEOS_K0.RestrictLimitValue = true;
            this.numericBoxEOS_K0.ShowFraction = false;
            this.numericBoxEOS_K0.ShowPositiveSign = false;
            this.numericBoxEOS_K0.ShowUpDown = false;
            this.numericBoxEOS_K0.Size = new System.Drawing.Size(114, 25);
            this.numericBoxEOS_K0.SkipEventDuringInput = false;
            this.numericBoxEOS_K0.SmartIncrement = true;
            this.numericBoxEOS_K0.TabIndex = 2;
            this.numericBoxEOS_K0.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_K0.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_K0.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxEOS_K0.ThonsandsSeparator = true;
            this.numericBoxEOS_K0.ToolTip = "";
            this.numericBoxEOS_K0.UpDown_Increment = 1D;
            this.numericBoxEOS_K0.Value = 37.12D;
            this.numericBoxEOS_K0.WordWrap = true;
            // 
            // numericBoxEOS_Kprime
            // 
            this.numericBoxEOS_Kprime.AllowMouseControl = false;
            this.numericBoxEOS_Kprime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_Kprime.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Kprime.DecimalPlaces = -2;
            this.numericBoxEOS_Kprime.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxEOS_Kprime.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxEOS_Kprime.FooterText = "";
            this.numericBoxEOS_Kprime.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxEOS_Kprime.HeaderText = "K\'0";
            this.numericBoxEOS_Kprime.Location = new System.Drawing.Point(12, 44);
            this.numericBoxEOS_Kprime.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_Kprime.Maximum = 10000D;
            this.numericBoxEOS_Kprime.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_Kprime.Minimum = 0D;
            this.numericBoxEOS_Kprime.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxEOS_Kprime.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_Kprime.MouseSpeed = 1D;
            this.numericBoxEOS_Kprime.Multiline = false;
            this.numericBoxEOS_Kprime.Name = "numericBoxEOS_Kprime";
            this.numericBoxEOS_Kprime.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxEOS_Kprime.RadianValue = 0.10454522219446034D;
            this.numericBoxEOS_Kprime.ReadOnly = false;
            this.numericBoxEOS_Kprime.RestrictLimitValue = true;
            this.numericBoxEOS_Kprime.ShowFraction = false;
            this.numericBoxEOS_Kprime.ShowPositiveSign = false;
            this.numericBoxEOS_Kprime.ShowUpDown = false;
            this.numericBoxEOS_Kprime.Size = new System.Drawing.Size(83, 25);
            this.numericBoxEOS_Kprime.SkipEventDuringInput = false;
            this.numericBoxEOS_Kprime.SmartIncrement = true;
            this.numericBoxEOS_Kprime.TabIndex = 2;
            this.numericBoxEOS_Kprime.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_Kprime.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_Kprime.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxEOS_Kprime.ThonsandsSeparator = true;
            this.numericBoxEOS_Kprime.ToolTip = "";
            this.numericBoxEOS_Kprime.UpDown_Increment = 1D;
            this.numericBoxEOS_Kprime.Value = 5.99D;
            this.numericBoxEOS_Kprime.WordWrap = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numericBoxShockedPlaneH);
            this.groupBox4.Controls.Add(this.numericBoxShockedPlaneK);
            this.groupBox4.Controls.Add(this.numericBoxShockedPlaneL);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(12, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(144, 62);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Shocked plane";
            // 
            // numericBoxShockedPlaneH
            // 
            this.numericBoxShockedPlaneH.AllowMouseControl = false;
            this.numericBoxShockedPlaneH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxShockedPlaneH.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxShockedPlaneH.DecimalPlaces = -2;
            this.numericBoxShockedPlaneH.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneH.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneH.FooterText = "";
            this.numericBoxShockedPlaneH.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneH.HeaderText = "(";
            this.numericBoxShockedPlaneH.Location = new System.Drawing.Point(3, 33);
            this.numericBoxShockedPlaneH.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxShockedPlaneH.Maximum = 10D;
            this.numericBoxShockedPlaneH.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxShockedPlaneH.Minimum = -10D;
            this.numericBoxShockedPlaneH.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxShockedPlaneH.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxShockedPlaneH.MouseSpeed = 1D;
            this.numericBoxShockedPlaneH.Multiline = false;
            this.numericBoxShockedPlaneH.Name = "numericBoxShockedPlaneH";
            this.numericBoxShockedPlaneH.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxShockedPlaneH.RadianValue = 0.017453292519943295D;
            this.numericBoxShockedPlaneH.ReadOnly = false;
            this.numericBoxShockedPlaneH.RestrictLimitValue = true;
            this.numericBoxShockedPlaneH.ShowFraction = false;
            this.numericBoxShockedPlaneH.ShowPositiveSign = false;
            this.numericBoxShockedPlaneH.ShowUpDown = true;
            this.numericBoxShockedPlaneH.Size = new System.Drawing.Size(50, 25);
            this.numericBoxShockedPlaneH.SkipEventDuringInput = false;
            this.numericBoxShockedPlaneH.SmartIncrement = false;
            this.numericBoxShockedPlaneH.TabIndex = 2;
            this.numericBoxShockedPlaneH.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxShockedPlaneH.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxShockedPlaneH.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneH.ThonsandsSeparator = true;
            this.numericBoxShockedPlaneH.ToolTip = "";
            this.numericBoxShockedPlaneH.UpDown_Increment = 1D;
            this.numericBoxShockedPlaneH.Value = 1D;
            this.numericBoxShockedPlaneH.WordWrap = true;
            // 
            // numericBoxShockedPlaneK
            // 
            this.numericBoxShockedPlaneK.AllowMouseControl = false;
            this.numericBoxShockedPlaneK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxShockedPlaneK.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxShockedPlaneK.DecimalPlaces = -2;
            this.numericBoxShockedPlaneK.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneK.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneK.FooterText = "";
            this.numericBoxShockedPlaneK.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneK.HeaderText = "";
            this.numericBoxShockedPlaneK.Location = new System.Drawing.Point(53, 33);
            this.numericBoxShockedPlaneK.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxShockedPlaneK.Maximum = 10D;
            this.numericBoxShockedPlaneK.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxShockedPlaneK.Minimum = -10D;
            this.numericBoxShockedPlaneK.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxShockedPlaneK.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxShockedPlaneK.MouseSpeed = 1D;
            this.numericBoxShockedPlaneK.Multiline = false;
            this.numericBoxShockedPlaneK.Name = "numericBoxShockedPlaneK";
            this.numericBoxShockedPlaneK.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxShockedPlaneK.RadianValue = 0D;
            this.numericBoxShockedPlaneK.ReadOnly = false;
            this.numericBoxShockedPlaneK.RestrictLimitValue = true;
            this.numericBoxShockedPlaneK.ShowFraction = false;
            this.numericBoxShockedPlaneK.ShowPositiveSign = false;
            this.numericBoxShockedPlaneK.ShowUpDown = true;
            this.numericBoxShockedPlaneK.Size = new System.Drawing.Size(38, 25);
            this.numericBoxShockedPlaneK.SkipEventDuringInput = false;
            this.numericBoxShockedPlaneK.SmartIncrement = false;
            this.numericBoxShockedPlaneK.TabIndex = 2;
            this.numericBoxShockedPlaneK.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxShockedPlaneK.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxShockedPlaneK.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneK.ThonsandsSeparator = true;
            this.numericBoxShockedPlaneK.ToolTip = "";
            this.numericBoxShockedPlaneK.UpDown_Increment = 1D;
            this.numericBoxShockedPlaneK.Value = 0D;
            this.numericBoxShockedPlaneK.WordWrap = true;
            // 
            // numericBoxShockedPlaneL
            // 
            this.numericBoxShockedPlaneL.AllowMouseControl = false;
            this.numericBoxShockedPlaneL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxShockedPlaneL.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxShockedPlaneL.DecimalPlaces = -2;
            this.numericBoxShockedPlaneL.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneL.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneL.FooterText = ")";
            this.numericBoxShockedPlaneL.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneL.HeaderText = "";
            this.numericBoxShockedPlaneL.Location = new System.Drawing.Point(91, 33);
            this.numericBoxShockedPlaneL.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxShockedPlaneL.Maximum = 1000D;
            this.numericBoxShockedPlaneL.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxShockedPlaneL.Minimum = 0D;
            this.numericBoxShockedPlaneL.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxShockedPlaneL.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxShockedPlaneL.MouseSpeed = 1D;
            this.numericBoxShockedPlaneL.Multiline = false;
            this.numericBoxShockedPlaneL.Name = "numericBoxShockedPlaneL";
            this.numericBoxShockedPlaneL.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxShockedPlaneL.RadianValue = 0D;
            this.numericBoxShockedPlaneL.ReadOnly = false;
            this.numericBoxShockedPlaneL.RestrictLimitValue = true;
            this.numericBoxShockedPlaneL.ShowFraction = false;
            this.numericBoxShockedPlaneL.ShowPositiveSign = false;
            this.numericBoxShockedPlaneL.ShowUpDown = true;
            this.numericBoxShockedPlaneL.Size = new System.Drawing.Size(50, 25);
            this.numericBoxShockedPlaneL.SkipEventDuringInput = false;
            this.numericBoxShockedPlaneL.SmartIncrement = false;
            this.numericBoxShockedPlaneL.TabIndex = 2;
            this.numericBoxShockedPlaneL.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxShockedPlaneL.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxShockedPlaneL.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShockedPlaneL.ThonsandsSeparator = true;
            this.numericBoxShockedPlaneL.ToolTip = "";
            this.numericBoxShockedPlaneL.UpDown_Increment = 1D;
            this.numericBoxShockedPlaneL.Value = 0D;
            this.numericBoxShockedPlaneL.WordWrap = true;
            // 
            // buttonExecute
            // 
            this.buttonExecute.AutoSize = true;
            this.buttonExecute.Location = new System.Drawing.Point(472, 740);
            this.buttonExecute.Name = "buttonExecute";
            this.buttonExecute.Size = new System.Drawing.Size(75, 27);
            this.buttonExecute.TabIndex = 7;
            this.buttonExecute.Text = "Execute";
            this.buttonExecute.UseVisualStyleBackColor = true;
            this.buttonExecute.Click += new System.EventHandler(this.buttonExecute_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 770);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(556, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(150, 16);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel1.Text = "   ";
            // 
            // checkBoxSkipDrawing
            // 
            this.checkBoxSkipDrawing.AutoSize = true;
            this.checkBoxSkipDrawing.Checked = true;
            this.checkBoxSkipDrawing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSkipDrawing.Location = new System.Drawing.Point(269, 742);
            this.checkBoxSkipDrawing.Name = "checkBoxSkipDrawing";
            this.checkBoxSkipDrawing.Size = new System.Drawing.Size(203, 21);
            this.checkBoxSkipDrawing.TabIndex = 10;
            this.checkBoxSkipDrawing.Text = "Skip drawing during execution";
            this.checkBoxSkipDrawing.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButtonCompressedIsotropic);
            this.groupBox5.Controls.Add(this.radioButtonCompressedUniaxial);
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Controls.Add(this.numericBoxUp);
            this.groupBox5.Location = new System.Drawing.Point(6, 85);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(166, 240);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Compressed area";
            // 
            // radioButtonCompressedIsotropic
            // 
            this.radioButtonCompressedIsotropic.AutoSize = true;
            this.radioButtonCompressedIsotropic.Location = new System.Drawing.Point(6, 73);
            this.radioButtonCompressedIsotropic.Name = "radioButtonCompressedIsotropic";
            this.radioButtonCompressedIsotropic.Size = new System.Drawing.Size(156, 21);
            this.radioButtonCompressedIsotropic.TabIndex = 5;
            this.radioButtonCompressedIsotropic.Text = "Isotropic compression";
            this.radioButtonCompressedIsotropic.UseVisualStyleBackColor = true;
            // 
            // radioButtonCompressedUniaxial
            // 
            this.radioButtonCompressedUniaxial.AutoSize = true;
            this.radioButtonCompressedUniaxial.Checked = true;
            this.radioButtonCompressedUniaxial.Location = new System.Drawing.Point(6, 51);
            this.radioButtonCompressedUniaxial.Name = "radioButtonCompressedUniaxial";
            this.radioButtonCompressedUniaxial.Size = new System.Drawing.Size(150, 21);
            this.radioButtonCompressedUniaxial.TabIndex = 5;
            this.radioButtonCompressedUniaxial.TabStop = true;
            this.radioButtonCompressedUniaxial.Text = "Uniaxial compression";
            this.radioButtonCompressedUniaxial.UseVisualStyleBackColor = true;
            // 
            // numericBoxUp
            // 
            this.numericBoxUp.AllowMouseControl = false;
            this.numericBoxUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxUp.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxUp.DecimalPlaces = -2;
            this.numericBoxUp.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxUp.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxUp.FooterText = "km/s";
            this.numericBoxUp.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxUp.HeaderText = "Us";
            this.numericBoxUp.Location = new System.Drawing.Point(7, 24);
            this.numericBoxUp.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxUp.Maximum = 10000D;
            this.numericBoxUp.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxUp.Minimum = 0D;
            this.numericBoxUp.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxUp.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxUp.MouseSpeed = 1D;
            this.numericBoxUp.Multiline = false;
            this.numericBoxUp.Name = "numericBoxUp";
            this.numericBoxUp.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxUp.RadianValue = 0.09599310885968812D;
            this.numericBoxUp.ReadOnly = false;
            this.numericBoxUp.RestrictLimitValue = true;
            this.numericBoxUp.ShowFraction = false;
            this.numericBoxUp.ShowPositiveSign = false;
            this.numericBoxUp.ShowUpDown = false;
            this.numericBoxUp.Size = new System.Drawing.Size(143, 25);
            this.numericBoxUp.SkipEventDuringInput = false;
            this.numericBoxUp.SmartIncrement = true;
            this.numericBoxUp.TabIndex = 2;
            this.numericBoxUp.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxUp.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxUp.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxUp.ThonsandsSeparator = true;
            this.numericBoxUp.ToolTip = "";
            this.numericBoxUp.UpDown_Increment = 1D;
            this.numericBoxUp.Value = 5.5D;
            this.numericBoxUp.WordWrap = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButtonReleasedIsotropic);
            this.groupBox6.Controls.Add(this.numericBoxUr);
            this.groupBox6.Controls.Add(this.radioButtonReleasedUniaxial);
            this.groupBox6.Controls.Add(this.groupBox2);
            this.groupBox6.Location = new System.Drawing.Point(178, 85);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(166, 240);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Released area";
            // 
            // radioButtonReleasedIsotropic
            // 
            this.radioButtonReleasedIsotropic.AutoSize = true;
            this.radioButtonReleasedIsotropic.Location = new System.Drawing.Point(7, 73);
            this.radioButtonReleasedIsotropic.Name = "radioButtonReleasedIsotropic";
            this.radioButtonReleasedIsotropic.Size = new System.Drawing.Size(156, 21);
            this.radioButtonReleasedIsotropic.TabIndex = 5;
            this.radioButtonReleasedIsotropic.Text = "Isotropic compression";
            this.radioButtonReleasedIsotropic.UseVisualStyleBackColor = true;
            // 
            // numericBoxUr
            // 
            this.numericBoxUr.AllowMouseControl = false;
            this.numericBoxUr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxUr.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxUr.DecimalPlaces = -2;
            this.numericBoxUr.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxUr.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxUr.FooterText = "km/s";
            this.numericBoxUr.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxUr.HeaderText = "Ur";
            this.numericBoxUr.Location = new System.Drawing.Point(7, 24);
            this.numericBoxUr.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxUr.Maximum = 10000D;
            this.numericBoxUr.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxUr.Minimum = 0D;
            this.numericBoxUr.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxUr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxUr.MouseSpeed = 1D;
            this.numericBoxUr.Multiline = false;
            this.numericBoxUr.Name = "numericBoxUr";
            this.numericBoxUr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxUr.RadianValue = 0.09599310885968812D;
            this.numericBoxUr.ReadOnly = false;
            this.numericBoxUr.RestrictLimitValue = true;
            this.numericBoxUr.ShowFraction = false;
            this.numericBoxUr.ShowPositiveSign = false;
            this.numericBoxUr.ShowUpDown = false;
            this.numericBoxUr.Size = new System.Drawing.Size(143, 25);
            this.numericBoxUr.SkipEventDuringInput = false;
            this.numericBoxUr.SmartIncrement = true;
            this.numericBoxUr.TabIndex = 2;
            this.numericBoxUr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxUr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxUr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxUr.ThonsandsSeparator = true;
            this.numericBoxUr.ToolTip = "";
            this.numericBoxUr.UpDown_Increment = 1D;
            this.numericBoxUr.Value = 5.5D;
            this.numericBoxUr.WordWrap = true;
            // 
            // radioButtonReleasedUniaxial
            // 
            this.radioButtonReleasedUniaxial.AutoSize = true;
            this.radioButtonReleasedUniaxial.Checked = true;
            this.radioButtonReleasedUniaxial.Location = new System.Drawing.Point(7, 51);
            this.radioButtonReleasedUniaxial.Name = "radioButtonReleasedUniaxial";
            this.radioButtonReleasedUniaxial.Size = new System.Drawing.Size(150, 21);
            this.radioButtonReleasedUniaxial.TabIndex = 5;
            this.radioButtonReleasedUniaxial.TabStop = true;
            this.radioButtonReleasedUniaxial.Text = "Uniaxial compression";
            this.radioButtonReleasedUniaxial.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox3);
            this.groupBox7.Controls.Add(this.numericBoxMassAbsorption);
            this.groupBox7.Controls.Add(this.groupBox4);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Location = new System.Drawing.Point(2, 1);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(176, 227);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Sample parameters";
            // 
            // numericBoxMassAbsorption
            // 
            this.numericBoxMassAbsorption.AllowMouseControl = false;
            this.numericBoxMassAbsorption.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxMassAbsorption.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMassAbsorption.DecimalPlaces = -2;
            this.numericBoxMassAbsorption.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMassAbsorption.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMassAbsorption.FooterText = "cm^2/g";
            this.numericBoxMassAbsorption.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMassAbsorption.HeaderText = "";
            this.numericBoxMassAbsorption.Location = new System.Drawing.Point(76, 187);
            this.numericBoxMassAbsorption.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxMassAbsorption.Maximum = 10000D;
            this.numericBoxMassAbsorption.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxMassAbsorption.Minimum = 0D;
            this.numericBoxMassAbsorption.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxMassAbsorption.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxMassAbsorption.MouseSpeed = 1D;
            this.numericBoxMassAbsorption.Multiline = false;
            this.numericBoxMassAbsorption.Name = "numericBoxMassAbsorption";
            this.numericBoxMassAbsorption.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxMassAbsorption.RadianValue = 0.33196162372932142D;
            this.numericBoxMassAbsorption.ReadOnly = false;
            this.numericBoxMassAbsorption.RestrictLimitValue = true;
            this.numericBoxMassAbsorption.ShowFraction = false;
            this.numericBoxMassAbsorption.ShowPositiveSign = false;
            this.numericBoxMassAbsorption.ShowUpDown = false;
            this.numericBoxMassAbsorption.Size = new System.Drawing.Size(97, 25);
            this.numericBoxMassAbsorption.SkipEventDuringInput = false;
            this.numericBoxMassAbsorption.SmartIncrement = true;
            this.numericBoxMassAbsorption.TabIndex = 2;
            this.numericBoxMassAbsorption.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxMassAbsorption.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxMassAbsorption.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMassAbsorption.ThonsandsSeparator = true;
            this.numericBoxMassAbsorption.ToolTip = "";
            this.numericBoxMassAbsorption.UpDown_Increment = 1D;
            this.numericBoxMassAbsorption.Value = 19.02D;
            this.numericBoxMassAbsorption.WordWrap = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 34);
            this.label4.TabIndex = 3;
            this.label4.Text = "Mass absorption\r\ncoefficient";
            // 
            // checkBoxSaveSimulatedPattern
            // 
            this.checkBoxSaveSimulatedPattern.AutoSize = true;
            this.checkBoxSaveSimulatedPattern.Location = new System.Drawing.Point(8, 84);
            this.checkBoxSaveSimulatedPattern.Name = "checkBoxSaveSimulatedPattern";
            this.checkBoxSaveSimulatedPattern.Size = new System.Drawing.Size(106, 21);
            this.checkBoxSaveSimulatedPattern.TabIndex = 10;
            this.checkBoxSaveSimulatedPattern.Text = "Save patterns";
            this.checkBoxSaveSimulatedPattern.UseVisualStyleBackColor = true;
            this.checkBoxSaveSimulatedPattern.CheckedChanged += new System.EventHandler(this.checkBoxSaveSimulatedPattern_CheckedChanged);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Set the output folder";
            // 
            // buttonSetFolder
            // 
            this.buttonSetFolder.AutoSize = true;
            this.buttonSetFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSetFolder.Location = new System.Drawing.Point(3, 0);
            this.buttonSetFolder.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.buttonSetFolder.Name = "buttonSetFolder";
            this.buttonSetFolder.Size = new System.Drawing.Size(79, 27);
            this.buttonSetFolder.TabIndex = 7;
            this.buttonSetFolder.Text = "Set  folder";
            this.buttonSetFolder.UseVisualStyleBackColor = true;
            this.buttonSetFolder.Click += new System.EventHandler(this.buttonSetFolder_Click);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(158, 3);
            this.textBoxFileName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(150, 25);
            this.textBoxFileName.TabIndex = 14;
            this.textBoxFileName.Text = "pattern";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(311, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "#.tif";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(85, 6);
            this.label8.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 17);
            this.label8.TabIndex = 3;
            this.label8.Text = "File name: ";
            // 
            // flowLayoutPanelSavePatterns
            // 
            this.flowLayoutPanelSavePatterns.AutoSize = true;
            this.flowLayoutPanelSavePatterns.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelSavePatterns.Controls.Add(this.buttonSetFolder);
            this.flowLayoutPanelSavePatterns.Controls.Add(this.label8);
            this.flowLayoutPanelSavePatterns.Controls.Add(this.textBoxFileName);
            this.flowLayoutPanelSavePatterns.Controls.Add(this.label7);
            this.flowLayoutPanelSavePatterns.Enabled = false;
            this.flowLayoutPanelSavePatterns.Location = new System.Drawing.Point(117, 80);
            this.flowLayoutPanelSavePatterns.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelSavePatterns.Name = "flowLayoutPanelSavePatterns";
            this.flowLayoutPanelSavePatterns.Size = new System.Drawing.Size(341, 28);
            this.flowLayoutPanelSavePatterns.TabIndex = 15;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.flowLayoutPanelOmegaStep);
            this.groupBox8.Controls.Add(this.checkBoxOmegaStep);
            this.groupBox8.Controls.Add(this.checkBoxSaveSimulatedPattern);
            this.groupBox8.Controls.Add(this.numericBoxDivisionOfRotationAngle);
            this.groupBox8.Controls.Add(this.numericBoxDivisionOfRotationAxis);
            this.groupBox8.Controls.Add(this.flowLayoutPanelSavePatterns);
            this.groupBox8.Location = new System.Drawing.Point(0, 622);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(547, 115);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Output parameters";
            // 
            // flowLayoutPanelOmegaStep
            // 
            this.flowLayoutPanelOmegaStep.AutoSize = true;
            this.flowLayoutPanelOmegaStep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelOmegaStep.Controls.Add(this.numericBoxOmegaStep);
            this.flowLayoutPanelOmegaStep.Controls.Add(this.numericBoxOmegaTimes);
            this.flowLayoutPanelOmegaStep.Enabled = false;
            this.flowLayoutPanelOmegaStep.Location = new System.Drawing.Point(120, 51);
            this.flowLayoutPanelOmegaStep.Name = "flowLayoutPanelOmegaStep";
            this.flowLayoutPanelOmegaStep.Size = new System.Drawing.Size(211, 25);
            this.flowLayoutPanelOmegaStep.TabIndex = 16;
            // 
            // numericBoxOmegaStep
            // 
            this.numericBoxOmegaStep.AllowMouseControl = false;
            this.numericBoxOmegaStep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxOmegaStep.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxOmegaStep.DecimalPlaces = 2;
            this.numericBoxOmegaStep.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxOmegaStep.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxOmegaStep.FooterText = "° step ×";
            this.numericBoxOmegaStep.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxOmegaStep.HeaderText = "";
            this.numericBoxOmegaStep.Location = new System.Drawing.Point(0, 0);
            this.numericBoxOmegaStep.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxOmegaStep.Maximum = 360D;
            this.numericBoxOmegaStep.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxOmegaStep.Minimum = -360D;
            this.numericBoxOmegaStep.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxOmegaStep.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxOmegaStep.MouseSpeed = 1D;
            this.numericBoxOmegaStep.Multiline = false;
            this.numericBoxOmegaStep.Name = "numericBoxOmegaStep";
            this.numericBoxOmegaStep.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxOmegaStep.RadianValue = 0.0017453292519943296D;
            this.numericBoxOmegaStep.ReadOnly = false;
            this.numericBoxOmegaStep.RestrictLimitValue = true;
            this.numericBoxOmegaStep.ShowFraction = false;
            this.numericBoxOmegaStep.ShowPositiveSign = true;
            this.numericBoxOmegaStep.ShowUpDown = true;
            this.numericBoxOmegaStep.Size = new System.Drawing.Size(116, 25);
            this.numericBoxOmegaStep.SkipEventDuringInput = false;
            this.numericBoxOmegaStep.SmartIncrement = true;
            this.numericBoxOmegaStep.TabIndex = 2;
            this.numericBoxOmegaStep.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxOmegaStep.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxOmegaStep.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxOmegaStep.ThonsandsSeparator = true;
            this.numericBoxOmegaStep.ToolTip = "";
            this.numericBoxOmegaStep.UpDown_Increment = 0.1D;
            this.numericBoxOmegaStep.Value = 0.1D;
            this.numericBoxOmegaStep.WordWrap = true;
            // 
            // numericBoxOmegaTimes
            // 
            this.numericBoxOmegaTimes.AllowMouseControl = false;
            this.numericBoxOmegaTimes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxOmegaTimes.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxOmegaTimes.DecimalPlaces = 0;
            this.numericBoxOmegaTimes.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxOmegaTimes.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxOmegaTimes.FooterText = "times";
            this.numericBoxOmegaTimes.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxOmegaTimes.HeaderText = "";
            this.numericBoxOmegaTimes.Location = new System.Drawing.Point(116, 0);
            this.numericBoxOmegaTimes.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxOmegaTimes.Maximum = 100D;
            this.numericBoxOmegaTimes.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxOmegaTimes.Minimum = 1D;
            this.numericBoxOmegaTimes.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxOmegaTimes.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxOmegaTimes.MouseSpeed = 1D;
            this.numericBoxOmegaTimes.Multiline = false;
            this.numericBoxOmegaTimes.Name = "numericBoxOmegaTimes";
            this.numericBoxOmegaTimes.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxOmegaTimes.RadianValue = 0.17453292519943295D;
            this.numericBoxOmegaTimes.ReadOnly = false;
            this.numericBoxOmegaTimes.RestrictLimitValue = true;
            this.numericBoxOmegaTimes.ShowFraction = false;
            this.numericBoxOmegaTimes.ShowPositiveSign = false;
            this.numericBoxOmegaTimes.ShowUpDown = true;
            this.numericBoxOmegaTimes.Size = new System.Drawing.Size(95, 25);
            this.numericBoxOmegaTimes.SkipEventDuringInput = false;
            this.numericBoxOmegaTimes.SmartIncrement = true;
            this.numericBoxOmegaTimes.TabIndex = 2;
            this.numericBoxOmegaTimes.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxOmegaTimes.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxOmegaTimes.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxOmegaTimes.ThonsandsSeparator = true;
            this.numericBoxOmegaTimes.ToolTip = "";
            this.numericBoxOmegaTimes.UpDown_Increment = 0.1D;
            this.numericBoxOmegaTimes.Value = 10D;
            this.numericBoxOmegaTimes.WordWrap = true;
            // 
            // checkBoxOmegaStep
            // 
            this.checkBoxOmegaStep.AutoSize = true;
            this.checkBoxOmegaStep.Location = new System.Drawing.Point(9, 53);
            this.checkBoxOmegaStep.Name = "checkBoxOmegaStep";
            this.checkBoxOmegaStep.Size = new System.Drawing.Size(98, 21);
            this.checkBoxOmegaStep.TabIndex = 10;
            this.checkBoxOmegaStep.Text = "Increment Ω";
            this.checkBoxOmegaStep.UseVisualStyleBackColor = true;
            this.checkBoxOmegaStep.CheckedChanged += new System.EventHandler(this.checkBoxOmegaStep_CheckedChanged);
            // 
            // numericBoxDivisionOfRotationAngle
            // 
            this.numericBoxDivisionOfRotationAngle.AllowMouseControl = false;
            this.numericBoxDivisionOfRotationAngle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDivisionOfRotationAngle.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDivisionOfRotationAngle.DecimalPlaces = -2;
            this.numericBoxDivisionOfRotationAngle.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDivisionOfRotationAngle.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDivisionOfRotationAngle.FooterText = "";
            this.numericBoxDivisionOfRotationAngle.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDivisionOfRotationAngle.HeaderText = "rotation speed";
            this.numericBoxDivisionOfRotationAngle.Location = new System.Drawing.Point(203, 21);
            this.numericBoxDivisionOfRotationAngle.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDivisionOfRotationAngle.Maximum = 10000D;
            this.numericBoxDivisionOfRotationAngle.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDivisionOfRotationAngle.Minimum = 0D;
            this.numericBoxDivisionOfRotationAngle.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDivisionOfRotationAngle.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxDivisionOfRotationAngle.MouseSpeed = 1D;
            this.numericBoxDivisionOfRotationAngle.Multiline = false;
            this.numericBoxDivisionOfRotationAngle.Name = "numericBoxDivisionOfRotationAngle";
            this.numericBoxDivisionOfRotationAngle.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDivisionOfRotationAngle.RadianValue = 0.17453292519943295D;
            this.numericBoxDivisionOfRotationAngle.ReadOnly = false;
            this.numericBoxDivisionOfRotationAngle.RestrictLimitValue = true;
            this.numericBoxDivisionOfRotationAngle.ShowFraction = false;
            this.numericBoxDivisionOfRotationAngle.ShowPositiveSign = false;
            this.numericBoxDivisionOfRotationAngle.ShowUpDown = true;
            this.numericBoxDivisionOfRotationAngle.Size = new System.Drawing.Size(138, 25);
            this.numericBoxDivisionOfRotationAngle.SkipEventDuringInput = false;
            this.numericBoxDivisionOfRotationAngle.SmartIncrement = true;
            this.numericBoxDivisionOfRotationAngle.TabIndex = 2;
            this.numericBoxDivisionOfRotationAngle.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDivisionOfRotationAngle.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDivisionOfRotationAngle.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDivisionOfRotationAngle.ThonsandsSeparator = true;
            this.numericBoxDivisionOfRotationAngle.ToolTip = "";
            this.numericBoxDivisionOfRotationAngle.UpDown_Increment = 1D;
            this.numericBoxDivisionOfRotationAngle.Value = 10D;
            this.numericBoxDivisionOfRotationAngle.WordWrap = true;
            // 
            // numericBoxDivisionOfRotationAxis
            // 
            this.numericBoxDivisionOfRotationAxis.AllowMouseControl = false;
            this.numericBoxDivisionOfRotationAxis.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDivisionOfRotationAxis.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDivisionOfRotationAxis.DecimalPlaces = -2;
            this.numericBoxDivisionOfRotationAxis.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDivisionOfRotationAxis.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDivisionOfRotationAxis.FooterText = "";
            this.numericBoxDivisionOfRotationAxis.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDivisionOfRotationAxis.HeaderText = "Division of rotation axis";
            this.numericBoxDivisionOfRotationAxis.Location = new System.Drawing.Point(6, 21);
            this.numericBoxDivisionOfRotationAxis.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDivisionOfRotationAxis.Maximum = 360D;
            this.numericBoxDivisionOfRotationAxis.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDivisionOfRotationAxis.Minimum = 10D;
            this.numericBoxDivisionOfRotationAxis.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDivisionOfRotationAxis.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxDivisionOfRotationAxis.MouseSpeed = 1D;
            this.numericBoxDivisionOfRotationAxis.Multiline = false;
            this.numericBoxDivisionOfRotationAxis.Name = "numericBoxDivisionOfRotationAxis";
            this.numericBoxDivisionOfRotationAxis.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDivisionOfRotationAxis.RadianValue = 1.0471975511965976D;
            this.numericBoxDivisionOfRotationAxis.ReadOnly = false;
            this.numericBoxDivisionOfRotationAxis.RestrictLimitValue = true;
            this.numericBoxDivisionOfRotationAxis.ShowFraction = false;
            this.numericBoxDivisionOfRotationAxis.ShowPositiveSign = false;
            this.numericBoxDivisionOfRotationAxis.ShowUpDown = true;
            this.numericBoxDivisionOfRotationAxis.Size = new System.Drawing.Size(195, 25);
            this.numericBoxDivisionOfRotationAxis.SkipEventDuringInput = false;
            this.numericBoxDivisionOfRotationAxis.SmartIncrement = true;
            this.numericBoxDivisionOfRotationAxis.TabIndex = 2;
            this.numericBoxDivisionOfRotationAxis.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDivisionOfRotationAxis.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDivisionOfRotationAxis.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDivisionOfRotationAxis.ThonsandsSeparator = true;
            this.numericBoxDivisionOfRotationAxis.ToolTip = "";
            this.numericBoxDivisionOfRotationAxis.UpDown_Increment = 1D;
            this.numericBoxDivisionOfRotationAxis.Value = 60D;
            this.numericBoxDivisionOfRotationAxis.WordWrap = true;
            // 
            // trackBarAdvancedBack
            // 
            this.trackBarAdvancedBack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.trackBarAdvancedBack.ControlHeight = 26;
            this.trackBarAdvancedBack.DecimalPlaces = 2;
            this.trackBarAdvancedBack.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.trackBarAdvancedBack.FooterText = "";
            this.trackBarAdvancedBack.HeaderText = "";
            this.trackBarAdvancedBack.Location = new System.Drawing.Point(36, 586);
            this.trackBarAdvancedBack.LogScrollBar = false;
            this.trackBarAdvancedBack.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarAdvancedBack.Maximum = 100D;
            this.trackBarAdvancedBack.Minimum = 0D;
            this.trackBarAdvancedBack.Name = "trackBarAdvancedBack";
            this.trackBarAdvancedBack.NumericBoxSize = 60;
            this.trackBarAdvancedBack.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAdvancedBack.Size = new System.Drawing.Size(517, 26);
            this.trackBarAdvancedBack.Smart_Increment = true;
            this.trackBarAdvancedBack.TabIndex = 1;
            this.trackBarAdvancedBack.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.trackBarAdvancedBack.UpDown_Increment = 1D;
            this.trackBarAdvancedBack.Value = 10D;
            this.trackBarAdvancedBack.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(this.trackBarAdvancedBack_ValueChanged);
            // 
            // trackBarAdvancedTime
            // 
            this.trackBarAdvancedTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.trackBarAdvancedTime.ControlHeight = 26;
            this.trackBarAdvancedTime.DecimalPlaces = 3;
            this.trackBarAdvancedTime.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.trackBarAdvancedTime.FooterText = "ns";
            this.trackBarAdvancedTime.HeaderText = "Time";
            this.trackBarAdvancedTime.Location = new System.Drawing.Point(0, 335);
            this.trackBarAdvancedTime.LogScrollBar = false;
            this.trackBarAdvancedTime.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarAdvancedTime.Maximum = 100D;
            this.trackBarAdvancedTime.Minimum = 0D;
            this.trackBarAdvancedTime.Name = "trackBarAdvancedTime";
            this.trackBarAdvancedTime.NumericBoxSize = 128;
            this.trackBarAdvancedTime.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAdvancedTime.Size = new System.Drawing.Size(547, 26);
            this.trackBarAdvancedTime.Smart_Increment = false;
            this.trackBarAdvancedTime.TabIndex = 1;
            this.trackBarAdvancedTime.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.trackBarAdvancedTime.UpDown_Increment = 0.005D;
            this.trackBarAdvancedTime.Value = 0D;
            this.trackBarAdvancedTime.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(this.trackBarAdvancedTime_ValueChanged);
            // 
            // trackBarAdvancedFront
            // 
            this.trackBarAdvancedFront.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.trackBarAdvancedFront.ControlHeight = 26;
            this.trackBarAdvancedFront.DecimalPlaces = 2;
            this.trackBarAdvancedFront.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.trackBarAdvancedFront.FooterText = "";
            this.trackBarAdvancedFront.HeaderText = "";
            this.trackBarAdvancedFront.Location = new System.Drawing.Point(36, 555);
            this.trackBarAdvancedFront.LogScrollBar = false;
            this.trackBarAdvancedFront.Margin = new System.Windows.Forms.Padding(0);
            this.trackBarAdvancedFront.Maximum = 100D;
            this.trackBarAdvancedFront.Minimum = 0D;
            this.trackBarAdvancedFront.Name = "trackBarAdvancedFront";
            this.trackBarAdvancedFront.NumericBoxSize = 60;
            this.trackBarAdvancedFront.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarAdvancedFront.Size = new System.Drawing.Size(517, 26);
            this.trackBarAdvancedFront.Smart_Increment = true;
            this.trackBarAdvancedFront.TabIndex = 1;
            this.trackBarAdvancedFront.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            this.trackBarAdvancedFront.UpDown_Increment = 1D;
            this.trackBarAdvancedFront.Value = 0D;
            this.trackBarAdvancedFront.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(this.trackBarAdvancedBack_ValueChanged);
            // 
            // graphControl
            // 
            this.graphControl.AllowMouseOperation = true;
            this.graphControl.BackgroundColor = System.Drawing.Color.White;
            this.graphControl.BottomMargin = 0D;
            this.graphControl.DivisionLineColor = System.Drawing.Color.Gray;
            this.graphControl.DivisionSubLineColor = System.Drawing.Color.LightGray;
            this.graphControl.FixRangeHorizontal = false;
            this.graphControl.FixRangeVertical = false;
            this.graphControl.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphControl.GraphName = "";
            this.graphControl.HorizontalGradiationTextVisivle = true;
            this.graphControl.Interpolation = false;
            this.graphControl.IsIntegerX = false;
            this.graphControl.IsIntegerY = false;
            this.graphControl.LabelX = "X:";
            this.graphControl.LabelY = "Y:";
            this.graphControl.LeftMargin = 0F;
            this.graphControl.LineColor = System.Drawing.Color.Red;
            this.graphControl.LineWidth = 1F;
            this.graphControl.Location = new System.Drawing.Point(72, 365);
            this.graphControl.LowerX = 0D;
            this.graphControl.LowerY = 0D;
            this.graphControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.graphControl.MaximalX = 1D;
            this.graphControl.MaximalY = 1D;
            this.graphControl.MinimalX = 0D;
            this.graphControl.MinimalY = 0D;
            this.graphControl.Mode = Crystallography.Controls.GraphControl.DrawingMode.Line;
            this.graphControl.MousePositionVisible = true;
            this.graphControl.Name = "graphControl";
            this.graphControl.OriginPosition = new System.Drawing.Point(40, 20);
            this.graphControl.Size = new System.Drawing.Size(475, 186);
            this.graphControl.Smoothing = false;
            this.graphControl.TabIndex = 0;
            this.graphControl.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphControl.UnitX = " nm";
            this.graphControl.UnitY = " MBar";
            this.graphControl.UpperText = "";
            this.graphControl.UpperTextVisible = false;
            this.graphControl.UpperX = 1D;
            this.graphControl.UpperY = 1D;
            this.graphControl.UseLineWidth = true;
            this.graphControl.VerticalGradiationTextVisivle = true;
            this.graphControl.XLog = false;
            this.graphControl.XScaleLineVisible = true;
            this.graphControl.YLog = false;
            this.graphControl.YScaleLineVisible = true;
            this.graphControl.LinePositionChanged += new Crystallography.Controls.GraphControl.LinePositionChengedEventHandler(this.graphControl1_LinePositionChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.radioButton2019Model);
            this.groupBox9.Controls.Add(this.radioButton2018Model);
            this.groupBox9.Controls.Add(this.groupBoxSlipPlane);
            this.groupBox9.Controls.Add(this.groupBox6);
            this.groupBox9.Controls.Add(this.groupBox5);
            this.groupBox9.Location = new System.Drawing.Point(184, 1);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(353, 331);
            this.groupBox9.TabIndex = 17;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Compression && rotation model";
            // 
            // radioButton2019Model
            // 
            this.radioButton2019Model.AutoSize = true;
            this.radioButton2019Model.Checked = true;
            this.radioButton2019Model.Location = new System.Drawing.Point(5, 48);
            this.radioButton2019Model.Name = "radioButton2019Model";
            this.radioButton2019Model.Size = new System.Drawing.Size(95, 21);
            this.radioButton2019Model.TabIndex = 7;
            this.radioButton2019Model.TabStop = true;
            this.radioButton2019Model.Text = "2019 model";
            this.radioButton2019Model.UseVisualStyleBackColor = true;
            this.radioButton2019Model.CheckedChanged += new System.EventHandler(this.radioButton2019Model_CheckedChanged);
            // 
            // radioButton2018Model
            // 
            this.radioButton2018Model.AutoSize = true;
            this.radioButton2018Model.Location = new System.Drawing.Point(6, 24);
            this.radioButton2018Model.Name = "radioButton2018Model";
            this.radioButton2018Model.Size = new System.Drawing.Size(95, 21);
            this.radioButton2018Model.TabIndex = 7;
            this.radioButton2018Model.Text = "2018 model";
            this.radioButton2018Model.UseVisualStyleBackColor = true;
            this.radioButton2018Model.CheckedChanged += new System.EventHandler(this.radioButton2019Model_CheckedChanged);
            // 
            // groupBoxSlipPlane
            // 
            this.groupBoxSlipPlane.Controls.Add(this.numericBoxSlipPlaneH);
            this.groupBoxSlipPlane.Controls.Add(this.numericBoxSlipPlaneK);
            this.groupBoxSlipPlane.Controls.Add(this.numericBoxSlipPlaneL);
            this.groupBoxSlipPlane.Controls.Add(this.label5);
            this.groupBoxSlipPlane.Location = new System.Drawing.Point(107, 17);
            this.groupBoxSlipPlane.Name = "groupBoxSlipPlane";
            this.groupBoxSlipPlane.Size = new System.Drawing.Size(142, 62);
            this.groupBoxSlipPlane.TabIndex = 6;
            this.groupBoxSlipPlane.TabStop = false;
            this.groupBoxSlipPlane.Text = "Slip plane";
            // 
            // numericBoxSlipPlaneH
            // 
            this.numericBoxSlipPlaneH.AllowMouseControl = false;
            this.numericBoxSlipPlaneH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxSlipPlaneH.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxSlipPlaneH.DecimalPlaces = -2;
            this.numericBoxSlipPlaneH.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneH.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneH.FooterText = "";
            this.numericBoxSlipPlaneH.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneH.HeaderText = "(";
            this.numericBoxSlipPlaneH.Location = new System.Drawing.Point(3, 33);
            this.numericBoxSlipPlaneH.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxSlipPlaneH.Maximum = 10D;
            this.numericBoxSlipPlaneH.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxSlipPlaneH.Minimum = -10D;
            this.numericBoxSlipPlaneH.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxSlipPlaneH.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxSlipPlaneH.MouseSpeed = 1D;
            this.numericBoxSlipPlaneH.Multiline = false;
            this.numericBoxSlipPlaneH.Name = "numericBoxSlipPlaneH";
            this.numericBoxSlipPlaneH.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxSlipPlaneH.RadianValue = 0.017453292519943295D;
            this.numericBoxSlipPlaneH.ReadOnly = false;
            this.numericBoxSlipPlaneH.RestrictLimitValue = true;
            this.numericBoxSlipPlaneH.ShowFraction = false;
            this.numericBoxSlipPlaneH.ShowPositiveSign = false;
            this.numericBoxSlipPlaneH.ShowUpDown = true;
            this.numericBoxSlipPlaneH.Size = new System.Drawing.Size(50, 25);
            this.numericBoxSlipPlaneH.SkipEventDuringInput = false;
            this.numericBoxSlipPlaneH.SmartIncrement = false;
            this.numericBoxSlipPlaneH.TabIndex = 2;
            this.numericBoxSlipPlaneH.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxSlipPlaneH.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxSlipPlaneH.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneH.ThonsandsSeparator = true;
            this.numericBoxSlipPlaneH.ToolTip = "";
            this.numericBoxSlipPlaneH.UpDown_Increment = 1D;
            this.numericBoxSlipPlaneH.Value = 1D;
            this.numericBoxSlipPlaneH.WordWrap = true;
            // 
            // numericBoxSlipPlaneK
            // 
            this.numericBoxSlipPlaneK.AllowMouseControl = false;
            this.numericBoxSlipPlaneK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxSlipPlaneK.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxSlipPlaneK.DecimalPlaces = -2;
            this.numericBoxSlipPlaneK.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneK.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneK.FooterText = "";
            this.numericBoxSlipPlaneK.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneK.HeaderText = "";
            this.numericBoxSlipPlaneK.Location = new System.Drawing.Point(53, 33);
            this.numericBoxSlipPlaneK.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxSlipPlaneK.Maximum = 10D;
            this.numericBoxSlipPlaneK.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxSlipPlaneK.Minimum = -10D;
            this.numericBoxSlipPlaneK.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxSlipPlaneK.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxSlipPlaneK.MouseSpeed = 1D;
            this.numericBoxSlipPlaneK.Multiline = false;
            this.numericBoxSlipPlaneK.Name = "numericBoxSlipPlaneK";
            this.numericBoxSlipPlaneK.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxSlipPlaneK.RadianValue = 0D;
            this.numericBoxSlipPlaneK.ReadOnly = false;
            this.numericBoxSlipPlaneK.RestrictLimitValue = true;
            this.numericBoxSlipPlaneK.ShowFraction = false;
            this.numericBoxSlipPlaneK.ShowPositiveSign = false;
            this.numericBoxSlipPlaneK.ShowUpDown = true;
            this.numericBoxSlipPlaneK.Size = new System.Drawing.Size(38, 25);
            this.numericBoxSlipPlaneK.SkipEventDuringInput = false;
            this.numericBoxSlipPlaneK.SmartIncrement = false;
            this.numericBoxSlipPlaneK.TabIndex = 2;
            this.numericBoxSlipPlaneK.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxSlipPlaneK.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxSlipPlaneK.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneK.ThonsandsSeparator = true;
            this.numericBoxSlipPlaneK.ToolTip = "";
            this.numericBoxSlipPlaneK.UpDown_Increment = 1D;
            this.numericBoxSlipPlaneK.Value = 0D;
            this.numericBoxSlipPlaneK.WordWrap = true;
            // 
            // numericBoxSlipPlaneL
            // 
            this.numericBoxSlipPlaneL.AllowMouseControl = false;
            this.numericBoxSlipPlaneL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxSlipPlaneL.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxSlipPlaneL.DecimalPlaces = -2;
            this.numericBoxSlipPlaneL.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneL.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneL.FooterText = ")";
            this.numericBoxSlipPlaneL.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneL.HeaderText = "";
            this.numericBoxSlipPlaneL.Location = new System.Drawing.Point(91, 33);
            this.numericBoxSlipPlaneL.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxSlipPlaneL.Maximum = 1000D;
            this.numericBoxSlipPlaneL.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxSlipPlaneL.Minimum = 0D;
            this.numericBoxSlipPlaneL.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxSlipPlaneL.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxSlipPlaneL.MouseSpeed = 1D;
            this.numericBoxSlipPlaneL.Multiline = false;
            this.numericBoxSlipPlaneL.Name = "numericBoxSlipPlaneL";
            this.numericBoxSlipPlaneL.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxSlipPlaneL.RadianValue = 0D;
            this.numericBoxSlipPlaneL.ReadOnly = false;
            this.numericBoxSlipPlaneL.RestrictLimitValue = true;
            this.numericBoxSlipPlaneL.ShowFraction = false;
            this.numericBoxSlipPlaneL.ShowPositiveSign = false;
            this.numericBoxSlipPlaneL.ShowUpDown = true;
            this.numericBoxSlipPlaneL.Size = new System.Drawing.Size(50, 25);
            this.numericBoxSlipPlaneL.SkipEventDuringInput = false;
            this.numericBoxSlipPlaneL.SmartIncrement = false;
            this.numericBoxSlipPlaneL.TabIndex = 2;
            this.numericBoxSlipPlaneL.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxSlipPlaneL.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxSlipPlaneL.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxSlipPlaneL.ThonsandsSeparator = true;
            this.numericBoxSlipPlaneL.ToolTip = "";
            this.numericBoxSlipPlaneL.UpDown_Increment = 1D;
            this.numericBoxSlipPlaneL.Value = 0D;
            this.numericBoxSlipPlaneL.WordWrap = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(19, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "h        k        l";
            // 
            // FormDiffractionSimulatorDynamicCompression
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 792);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonExecute);
            this.Controls.Add(this.checkBoxSkipDrawing);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBarAdvancedBack);
            this.Controls.Add(this.trackBarAdvancedTime);
            this.Controls.Add(this.trackBarAdvancedFront);
            this.Controls.Add(this.graphControl);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDiffractionSimulatorDynamicCompression";
            this.Text = "Dynamic Compression";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDiffractionSimulatorDynamicCompression_FormClosing);
            this.Load += new System.EventHandler(this.FormDiffractionSimulatorDynamicCompression_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDiffractionSimulatorDynamicCompression_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDiffractionSimulatorDynamicCompression_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.flowLayoutPanelSavePatterns.ResumeLayout(false);
            this.flowLayoutPanelSavePatterns.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.flowLayoutPanelOmegaStep.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBoxSlipPlane.ResumeLayout(false);
            this.groupBoxSlipPlane.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Crystallography.Controls.GraphControl graphControl;
        private Crystallography.Controls.TrackBarAdvanced trackBarAdvancedFront;
        private Crystallography.Controls.TrackBarAdvanced trackBarAdvancedBack;
        private Crystallography.Controls.NumericBox numericBoxShockedPlaneH;
        private Crystallography.Controls.NumericBox numericBoxShockedPlaneK;
        private Crystallography.Controls.NumericBox numericBoxShockedPlaneL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Crystallography.Controls.NumericBox numericBoxEOS_K0;
        private Crystallography.Controls.NumericBox numericBoxEOS_Kprime;
        private Crystallography.Controls.NumericBox numericBoxCompressedOmega;
        private Crystallography.Controls.NumericBox numericBoxCompressedOmegaSigma;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Crystallography.Controls.NumericBox numericBoxReleasedOmegaSigma;
        private Crystallography.Controls.NumericBox numericBoxReleasedOmega;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonExecute;
        private Crystallography.Controls.NumericBox numericBoxUp;
        private Crystallography.Controls.NumericBox numericBoxUr;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox checkBoxSkipDrawing;
        private Crystallography.Controls.NumericBox numericBoxDivisionOfRotationAngle;
        private Crystallography.Controls.NumericBox numericBoxDivisionOfRotationAxis;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButtonCompressedIsotropic;
        private System.Windows.Forms.RadioButton radioButtonCompressedUniaxial;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButtonReleasedIsotropic;
        private System.Windows.Forms.RadioButton radioButtonReleasedUniaxial;
        private System.Windows.Forms.GroupBox groupBox7;
        private Crystallography.Controls.NumericBox numericBoxMassAbsorption;
        private System.Windows.Forms.Label label4;
        private Crystallography.Controls.TrackBarAdvanced trackBarAdvancedTime;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.CheckBox checkBoxSaveSimulatedPattern;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonSetFolder;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSavePatterns;
        private System.Windows.Forms.GroupBox groupBox8;
        private Crystallography.Controls.NumericBox numericBoxOmegaStep;
        private System.Windows.Forms.CheckBox checkBoxOmegaStep;
        private Crystallography.Controls.NumericBox numericBoxOmegaTimes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOmegaStep;
        private Crystallography.Controls.NumericBox numericBoxCompressedThetaB;
        private Crystallography.Controls.NumericBox numericBoxCompressedThetaA;
        private Crystallography.Controls.NumericBox numericBoxReleasedThetaB;
        private Crystallography.Controls.NumericBox numericBoxReleasedThetaA;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton radioButton2019Model;
        private System.Windows.Forms.RadioButton radioButton2018Model;
        private System.Windows.Forms.GroupBox groupBoxSlipPlane;
        private Crystallography.Controls.NumericBox numericBoxSlipPlaneH;
        private Crystallography.Controls.NumericBox numericBoxSlipPlaneK;
        private Crystallography.Controls.NumericBox numericBoxSlipPlaneL;
        private System.Windows.Forms.Label label5;
    }
}