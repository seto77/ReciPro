namespace ReciPro
{
    partial class FormDiffractionSimulatorGeometry
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
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.buttonClearPicture = new System.Windows.Forms.Button();
            this.trackBarMaxInt = new System.Windows.Forms.TrackBar();
            this.buttonRot90 = new System.Windows.Forms.Button();
            this.buttonReadPicture = new System.Windows.Forms.Button();
            this.trackBarMinInt = new System.Windows.Forms.TrackBar();
            this.trackBarPictureOpacity1 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxScale2 = new System.Windows.Forms.ComboBox();
            this.comboBoxScale1 = new System.Windows.Forms.ComboBox();
            this.comboBoxGradient = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.numericalTextBoxFootY = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxPixelWidth = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxFootX = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxPixelHeight = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxPixelSize = new Crystallography.Controls.NumericBox();
            this.checkBoxDetectorSizePosition = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numericBoxCameraLength2 = new Crystallography.Controls.NumericBox();
            this.numericBoxTau = new Crystallography.Controls.NumericBox();
            this.checkBoxSchematicDiagram = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelSchematicDiagram = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMinInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPictureOpacity1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanelSchematicDiagram.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label3.Location = new System.Drawing.Point(373, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 51);
            this.label3.TabIndex = 1;
            this.label3.Text = "Foot of the\r\n perpendicular\r\n from the sample ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxFileName);
            this.groupBox1.Controls.Add(this.buttonClearPicture);
            this.groupBox1.Controls.Add(this.trackBarMaxInt);
            this.groupBox1.Controls.Add(this.buttonRot90);
            this.groupBox1.Controls.Add(this.buttonReadPicture);
            this.groupBox1.Controls.Add(this.trackBarMinInt);
            this.groupBox1.Controls.Add(this.trackBarPictureOpacity1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.comboBoxScale2);
            this.groupBox1.Controls.Add(this.comboBoxScale1);
            this.groupBox1.Controls.Add(this.comboBoxGradient);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.numericalTextBoxFootY);
            this.groupBox1.Controls.Add(this.numericalTextBoxPixelWidth);
            this.groupBox1.Controls.Add(this.numericalTextBoxFootX);
            this.groupBox1.Controls.Add(this.numericalTextBoxPixelHeight);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numericalTextBoxPixelSize);
            this.groupBox1.Enabled = false;
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Size = new System.Drawing.Size(624, 182);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.textBoxFileName.Location = new System.Drawing.Point(7, 21);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.ReadOnly = true;
            this.textBoxFileName.Size = new System.Drawing.Size(443, 23);
            this.textBoxFileName.TabIndex = 103;
            this.textBoxFileName.TextChanged += new System.EventHandler(this.textBoxFileName_TextChanged);
            // 
            // buttonClearPicture
            // 
            this.buttonClearPicture.AutoSize = true;
            this.buttonClearPicture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonClearPicture.Location = new System.Drawing.Point(502, 19);
            this.buttonClearPicture.Margin = new System.Windows.Forms.Padding(1, 4, 1, 4);
            this.buttonClearPicture.Name = "buttonClearPicture";
            this.buttonClearPicture.Size = new System.Drawing.Size(48, 27);
            this.buttonClearPicture.TabIndex = 89;
            this.buttonClearPicture.Text = "Clear";
            this.buttonClearPicture.UseVisualStyleBackColor = true;
            this.buttonClearPicture.Click += new System.EventHandler(this.buttonClearPicture_Click);
            // 
            // trackBarMaxInt
            // 
            this.trackBarMaxInt.AutoSize = false;
            this.trackBarMaxInt.Location = new System.Drawing.Point(113, 160);
            this.trackBarMaxInt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackBarMaxInt.Maximum = 100001;
            this.trackBarMaxInt.Minimum = 1;
            this.trackBarMaxInt.Name = "trackBarMaxInt";
            this.trackBarMaxInt.Size = new System.Drawing.Size(505, 19);
            this.trackBarMaxInt.SmallChange = 10000;
            this.trackBarMaxInt.TabIndex = 102;
            this.trackBarMaxInt.TickFrequency = 10;
            this.trackBarMaxInt.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMaxInt.Value = 100001;
            this.trackBarMaxInt.ValueChanged += new System.EventHandler(this.trackBarMaxInt_ValueChanged);
            // 
            // buttonRot90
            // 
            this.buttonRot90.AutoSize = true;
            this.buttonRot90.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonRot90.Location = new System.Drawing.Point(556, 19);
            this.buttonRot90.Margin = new System.Windows.Forms.Padding(1, 4, 1, 4);
            this.buttonRot90.Name = "buttonRot90";
            this.buttonRot90.Size = new System.Drawing.Size(61, 27);
            this.buttonRot90.TabIndex = 88;
            this.buttonRot90.Text = "Rot 90°";
            this.buttonRot90.UseVisualStyleBackColor = true;
            this.buttonRot90.Click += new System.EventHandler(this.buttonRot90_Click);
            // 
            // buttonReadPicture
            // 
            this.buttonReadPicture.AutoSize = true;
            this.buttonReadPicture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonReadPicture.Location = new System.Drawing.Point(454, 19);
            this.buttonReadPicture.Margin = new System.Windows.Forms.Padding(1, 4, 1, 4);
            this.buttonReadPicture.Name = "buttonReadPicture";
            this.buttonReadPicture.Size = new System.Drawing.Size(48, 27);
            this.buttonReadPicture.TabIndex = 88;
            this.buttonReadPicture.Text = "Read";
            this.buttonReadPicture.UseVisualStyleBackColor = true;
            this.buttonReadPicture.Click += new System.EventHandler(this.buttonReadPicture_Click);
            // 
            // trackBarMinInt
            // 
            this.trackBarMinInt.AutoSize = false;
            this.trackBarMinInt.Location = new System.Drawing.Point(113, 142);
            this.trackBarMinInt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackBarMinInt.Maximum = 100000;
            this.trackBarMinInt.Name = "trackBarMinInt";
            this.trackBarMinInt.Size = new System.Drawing.Size(505, 19);
            this.trackBarMinInt.SmallChange = 10000;
            this.trackBarMinInt.TabIndex = 102;
            this.trackBarMinInt.TickFrequency = 10;
            this.trackBarMinInt.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarMinInt.Value = 1;
            this.trackBarMinInt.ValueChanged += new System.EventHandler(this.trackBarMaxInt_ValueChanged);
            // 
            // trackBarPictureOpacity1
            // 
            this.trackBarPictureOpacity1.AutoSize = false;
            this.trackBarPictureOpacity1.Location = new System.Drawing.Point(57, 113);
            this.trackBarPictureOpacity1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackBarPictureOpacity1.Maximum = 100;
            this.trackBarPictureOpacity1.Name = "trackBarPictureOpacity1";
            this.trackBarPictureOpacity1.Size = new System.Drawing.Size(108, 19);
            this.trackBarPictureOpacity1.TabIndex = 102;
            this.trackBarPictureOpacity1.TickFrequency = 10;
            this.trackBarPictureOpacity1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarPictureOpacity1.Value = 100;
            this.trackBarPictureOpacity1.ValueChanged += new System.EventHandler(this.trackBarPictureOpacity1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(68, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 101;
            this.label2.Text = "Min int.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 101;
            this.label1.Text = "Max int.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 101;
            this.label4.Text = "Brightness";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 15);
            this.label10.TabIndex = 101;
            this.label10.Text = "Opacity";
            // 
            // comboBoxScale2
            // 
            this.comboBoxScale2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScale2.FormattingEnabled = true;
            this.comboBoxScale2.Items.AddRange(new object[] {
            "Gray scale",
            "Cold-Warm scale"});
            this.comboBoxScale2.Location = new System.Drawing.Point(522, 108);
            this.comboBoxScale2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxScale2.Name = "comboBoxScale2";
            this.comboBoxScale2.Size = new System.Drawing.Size(93, 25);
            this.comboBoxScale2.TabIndex = 97;
            this.comboBoxScale2.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxScale2_SelectedIndexChanged);
            // 
            // comboBoxScale1
            // 
            this.comboBoxScale1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScale1.FormattingEnabled = true;
            this.comboBoxScale1.Items.AddRange(new object[] {
            "Log Scale",
            "Liner Scale"});
            this.comboBoxScale1.Location = new System.Drawing.Point(376, 108);
            this.comboBoxScale1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxScale1.Name = "comboBoxScale1";
            this.comboBoxScale1.Size = new System.Drawing.Size(94, 25);
            this.comboBoxScale1.TabIndex = 96;
            this.comboBoxScale1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxScale_SelectedIndexChanged);
            // 
            // comboBoxGradient
            // 
            this.comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGradient.FormattingEnabled = true;
            this.comboBoxGradient.Items.AddRange(new object[] {
            "Positive Film",
            "Negative Film"});
            this.comboBoxGradient.Location = new System.Drawing.Point(229, 108);
            this.comboBoxGradient.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxGradient.Name = "comboBoxGradient";
            this.comboBoxGradient.Size = new System.Drawing.Size(94, 25);
            this.comboBoxGradient.TabIndex = 95;
            this.comboBoxGradient.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxGradient_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(474, 111);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(49, 17);
            this.label22.TabIndex = 100;
            this.label22.Text = "Scale 2";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(329, 111);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(49, 17);
            this.label23.TabIndex = 99;
            this.label23.Text = "Scale 1";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(171, 111);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(58, 17);
            this.label24.TabIndex = 98;
            this.label24.Text = "Gradient";
            // 
            // numericalTextBoxFootY
            // 
            this.numericalTextBoxFootY.AllowMouseControl = false;
            this.numericalTextBoxFootY.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericalTextBoxFootY.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxFootY.DecimalPlaces = -1;
            this.numericalTextBoxFootY.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootY.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootY.FooterText = "pixel";
            this.numericalTextBoxFootY.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootY.HeaderText = "fy";
            this.numericalTextBoxFootY.Location = new System.Drawing.Point(491, 78);
            this.numericalTextBoxFootY.Margin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxFootY.Maximum = double.PositiveInfinity;
            this.numericalTextBoxFootY.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericalTextBoxFootY.Minimum = double.NegativeInfinity;
            this.numericalTextBoxFootY.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericalTextBoxFootY.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxFootY.MouseSpeed = 1D;
            this.numericalTextBoxFootY.Multiline = false;
            this.numericalTextBoxFootY.Name = "numericalTextBoxFootY";
            this.numericalTextBoxFootY.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericalTextBoxFootY.RadianValue = 8.9360857702109673D;
            this.numericalTextBoxFootY.ReadOnly = false;
            this.numericalTextBoxFootY.RestrictLimitValue = true;
            this.numericalTextBoxFootY.ShowFraction = false;
            this.numericalTextBoxFootY.ShowPositiveSign = false;
            this.numericalTextBoxFootY.ShowUpDown = false;
            this.numericalTextBoxFootY.Size = new System.Drawing.Size(123, 25);
            this.numericalTextBoxFootY.SkipEventDuringInput = false;
            this.numericalTextBoxFootY.SmartIncrement = true;
            this.numericalTextBoxFootY.TabIndex = 0;
            this.numericalTextBoxFootY.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxFootY.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxFootY.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootY.ThonsandsSeparator = true;
            this.numericalTextBoxFootY.ToolTip = "";
            this.numericalTextBoxFootY.UpDown_Increment = 1D;
            this.numericalTextBoxFootY.Value = 512D;
            this.numericalTextBoxFootY.WordWrap = true;
            // 
            // numericalTextBoxPixelWidth
            // 
            this.numericalTextBoxPixelWidth.AllowMouseControl = false;
            this.numericalTextBoxPixelWidth.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericalTextBoxPixelWidth.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxPixelWidth.DecimalPlaces = -1;
            this.numericalTextBoxPixelWidth.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelWidth.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelWidth.FooterText = "pix.";
            this.numericalTextBoxPixelWidth.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelWidth.HeaderText = "Detector    width";
            this.numericalTextBoxPixelWidth.Location = new System.Drawing.Point(7, 51);
            this.numericalTextBoxPixelWidth.Margin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxPixelWidth.Maximum = double.PositiveInfinity;
            this.numericalTextBoxPixelWidth.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericalTextBoxPixelWidth.Minimum = double.NegativeInfinity;
            this.numericalTextBoxPixelWidth.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericalTextBoxPixelWidth.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxPixelWidth.MouseSpeed = 1D;
            this.numericalTextBoxPixelWidth.Multiline = false;
            this.numericalTextBoxPixelWidth.Name = "numericalTextBoxPixelWidth";
            this.numericalTextBoxPixelWidth.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericalTextBoxPixelWidth.RadianValue = 17.872171540421935D;
            this.numericalTextBoxPixelWidth.ReadOnly = false;
            this.numericalTextBoxPixelWidth.RestrictLimitValue = true;
            this.numericalTextBoxPixelWidth.ShowFraction = false;
            this.numericalTextBoxPixelWidth.ShowPositiveSign = false;
            this.numericalTextBoxPixelWidth.ShowUpDown = false;
            this.numericalTextBoxPixelWidth.Size = new System.Drawing.Size(174, 25);
            this.numericalTextBoxPixelWidth.SkipEventDuringInput = false;
            this.numericalTextBoxPixelWidth.SmartIncrement = true;
            this.numericalTextBoxPixelWidth.TabIndex = 0;
            this.numericalTextBoxPixelWidth.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxPixelWidth.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxPixelWidth.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelWidth.ThonsandsSeparator = true;
            this.numericalTextBoxPixelWidth.ToolTip = "";
            this.numericalTextBoxPixelWidth.UpDown_Increment = 1D;
            this.numericalTextBoxPixelWidth.Value = 1024D;
            this.numericalTextBoxPixelWidth.WordWrap = true;
            // 
            // numericalTextBoxFootX
            // 
            this.numericalTextBoxFootX.AllowMouseControl = false;
            this.numericalTextBoxFootX.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericalTextBoxFootX.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxFootX.DecimalPlaces = -1;
            this.numericalTextBoxFootX.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootX.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootX.FooterText = "pixel";
            this.numericalTextBoxFootX.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootX.HeaderText = "fx";
            this.numericalTextBoxFootX.Location = new System.Drawing.Point(491, 51);
            this.numericalTextBoxFootX.Margin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxFootX.Maximum = double.PositiveInfinity;
            this.numericalTextBoxFootX.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericalTextBoxFootX.Minimum = double.NegativeInfinity;
            this.numericalTextBoxFootX.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericalTextBoxFootX.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxFootX.MouseSpeed = 1D;
            this.numericalTextBoxFootX.Multiline = false;
            this.numericalTextBoxFootX.Name = "numericalTextBoxFootX";
            this.numericalTextBoxFootX.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericalTextBoxFootX.RadianValue = 8.9360857702109673D;
            this.numericalTextBoxFootX.ReadOnly = false;
            this.numericalTextBoxFootX.RestrictLimitValue = true;
            this.numericalTextBoxFootX.ShowFraction = false;
            this.numericalTextBoxFootX.ShowPositiveSign = false;
            this.numericalTextBoxFootX.ShowUpDown = false;
            this.numericalTextBoxFootX.Size = new System.Drawing.Size(123, 25);
            this.numericalTextBoxFootX.SkipEventDuringInput = false;
            this.numericalTextBoxFootX.SmartIncrement = true;
            this.numericalTextBoxFootX.TabIndex = 0;
            this.numericalTextBoxFootX.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxFootX.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxFootX.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxFootX.ThonsandsSeparator = true;
            this.numericalTextBoxFootX.ToolTip = "";
            this.numericalTextBoxFootX.UpDown_Increment = 1D;
            this.numericalTextBoxFootX.Value = 512D;
            this.numericalTextBoxFootX.WordWrap = true;
            // 
            // numericalTextBoxPixelHeight
            // 
            this.numericalTextBoxPixelHeight.AllowMouseControl = false;
            this.numericalTextBoxPixelHeight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericalTextBoxPixelHeight.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxPixelHeight.DecimalPlaces = -1;
            this.numericalTextBoxPixelHeight.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelHeight.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelHeight.FooterText = "pix.";
            this.numericalTextBoxPixelHeight.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelHeight.HeaderText = "height";
            this.numericalTextBoxPixelHeight.Location = new System.Drawing.Point(68, 78);
            this.numericalTextBoxPixelHeight.Margin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxPixelHeight.Maximum = double.PositiveInfinity;
            this.numericalTextBoxPixelHeight.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericalTextBoxPixelHeight.Minimum = double.NegativeInfinity;
            this.numericalTextBoxPixelHeight.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericalTextBoxPixelHeight.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxPixelHeight.MouseSpeed = 1D;
            this.numericalTextBoxPixelHeight.Multiline = false;
            this.numericalTextBoxPixelHeight.Name = "numericalTextBoxPixelHeight";
            this.numericalTextBoxPixelHeight.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericalTextBoxPixelHeight.RadianValue = 17.872171540421935D;
            this.numericalTextBoxPixelHeight.ReadOnly = false;
            this.numericalTextBoxPixelHeight.RestrictLimitValue = true;
            this.numericalTextBoxPixelHeight.ShowFraction = false;
            this.numericalTextBoxPixelHeight.ShowPositiveSign = false;
            this.numericalTextBoxPixelHeight.ShowUpDown = false;
            this.numericalTextBoxPixelHeight.Size = new System.Drawing.Size(113, 25);
            this.numericalTextBoxPixelHeight.SkipEventDuringInput = false;
            this.numericalTextBoxPixelHeight.SmartIncrement = true;
            this.numericalTextBoxPixelHeight.TabIndex = 0;
            this.numericalTextBoxPixelHeight.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxPixelHeight.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxPixelHeight.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelHeight.ThonsandsSeparator = true;
            this.numericalTextBoxPixelHeight.ToolTip = "";
            this.numericalTextBoxPixelHeight.UpDown_Increment = 1D;
            this.numericalTextBoxPixelHeight.Value = 1024D;
            this.numericalTextBoxPixelHeight.WordWrap = true;
            // 
            // numericalTextBoxPixelSize
            // 
            this.numericalTextBoxPixelSize.AllowMouseControl = false;
            this.numericalTextBoxPixelSize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericalTextBoxPixelSize.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxPixelSize.DecimalPlaces = -1;
            this.numericalTextBoxPixelSize.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelSize.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelSize.FooterText = "mm";
            this.numericalTextBoxPixelSize.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelSize.HeaderText = "pix. size";
            this.numericalTextBoxPixelSize.Location = new System.Drawing.Point(194, 51);
            this.numericalTextBoxPixelSize.Margin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxPixelSize.Maximum = double.PositiveInfinity;
            this.numericalTextBoxPixelSize.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericalTextBoxPixelSize.Minimum = double.NegativeInfinity;
            this.numericalTextBoxPixelSize.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericalTextBoxPixelSize.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxPixelSize.MouseSpeed = 1D;
            this.numericalTextBoxPixelSize.Multiline = false;
            this.numericalTextBoxPixelSize.Name = "numericalTextBoxPixelSize";
            this.numericalTextBoxPixelSize.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericalTextBoxPixelSize.RadianValue = 0.0017453292519943296D;
            this.numericalTextBoxPixelSize.ReadOnly = false;
            this.numericalTextBoxPixelSize.RestrictLimitValue = true;
            this.numericalTextBoxPixelSize.ShowFraction = false;
            this.numericalTextBoxPixelSize.ShowPositiveSign = false;
            this.numericalTextBoxPixelSize.ShowUpDown = false;
            this.numericalTextBoxPixelSize.Size = new System.Drawing.Size(143, 25);
            this.numericalTextBoxPixelSize.SkipEventDuringInput = false;
            this.numericalTextBoxPixelSize.SmartIncrement = true;
            this.numericalTextBoxPixelSize.TabIndex = 0;
            this.numericalTextBoxPixelSize.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxPixelSize.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxPixelSize.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxPixelSize.ThonsandsSeparator = true;
            this.numericalTextBoxPixelSize.ToolTip = "";
            this.numericalTextBoxPixelSize.UpDown_Increment = 1D;
            this.numericalTextBoxPixelSize.Value = 0.1D;
            this.numericalTextBoxPixelSize.WordWrap = true;
            // 
            // checkBoxDetectorSizePosition
            // 
            this.checkBoxDetectorSizePosition.AutoSize = true;
            this.checkBoxDetectorSizePosition.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDetectorSizePosition.Location = new System.Drawing.Point(5, 0);
            this.checkBoxDetectorSizePosition.Name = "checkBoxDetectorSizePosition";
            this.checkBoxDetectorSizePosition.Size = new System.Drawing.Size(256, 21);
            this.checkBoxDetectorSizePosition.TabIndex = 6;
            this.checkBoxDetectorSizePosition.Text = "Set detector area && overlapped image.";
            this.checkBoxDetectorSizePosition.UseVisualStyleBackColor = true;
            this.checkBoxDetectorSizePosition.CheckedChanged += new System.EventHandler(this.checkBoxDetectorSizePosition_CheckedChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ReciPro.Properties.Resources.geometry2;
            this.pictureBox2.Location = new System.Drawing.Point(10, 30);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(10, 30, 0, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(267, 202);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ReciPro.Properties.Resources.geometry1;
            this.pictureBox1.Location = new System.Drawing.Point(287, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(10, 5, 0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(353, 302);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // numericBoxCameraLength2
            // 
            this.numericBoxCameraLength2.AllowMouseControl = false;
            this.numericBoxCameraLength2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCameraLength2.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCameraLength2.DecimalPlaces = -2;
            this.numericBoxCameraLength2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCameraLength2.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCameraLength2.FooterText = "mm";
            this.numericBoxCameraLength2.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCameraLength2.HeaderText = "Camera length 2";
            this.numericBoxCameraLength2.Location = new System.Drawing.Point(0, 0);
            this.numericBoxCameraLength2.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCameraLength2.Maximum = 1000000D;
            this.numericBoxCameraLength2.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCameraLength2.Minimum = 1D;
            this.numericBoxCameraLength2.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCameraLength2.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxCameraLength2.MouseSpeed = 1D;
            this.numericBoxCameraLength2.Multiline = false;
            this.numericBoxCameraLength2.Name = "numericBoxCameraLength2";
            this.numericBoxCameraLength2.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCameraLength2.RadianValue = 17.453292519943293D;
            this.numericBoxCameraLength2.ReadOnly = false;
            this.numericBoxCameraLength2.RestrictLimitValue = true;
            this.numericBoxCameraLength2.ShowFraction = false;
            this.numericBoxCameraLength2.ShowPositiveSign = false;
            this.numericBoxCameraLength2.ShowUpDown = false;
            this.numericBoxCameraLength2.Size = new System.Drawing.Size(220, 25);
            this.numericBoxCameraLength2.SkipEventDuringInput = false;
            this.numericBoxCameraLength2.SmartIncrement = true;
            this.numericBoxCameraLength2.TabIndex = 2;
            this.numericBoxCameraLength2.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCameraLength2.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCameraLength2.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCameraLength2.ThonsandsSeparator = true;
            this.numericBoxCameraLength2.ToolTip = "";
            this.numericBoxCameraLength2.UpDown_Increment = 1D;
            this.numericBoxCameraLength2.Value = 1000D;
            this.numericBoxCameraLength2.WordWrap = true;
            this.numericBoxCameraLength2.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCameraLength2_ValueChanged);
            // 
            // numericBoxTau
            // 
            this.numericBoxTau.AllowMouseControl = false;
            this.numericBoxTau.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxTau.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTau.DecimalPlaces = -2;
            this.numericBoxTau.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxTau.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxTau.FooterText = "°";
            this.numericBoxTau.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 11F);
            this.numericBoxTau.HeaderText = "τ";
            this.numericBoxTau.Location = new System.Drawing.Point(244, 0);
            this.numericBoxTau.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxTau.Maximum = double.PositiveInfinity;
            this.numericBoxTau.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxTau.Minimum = double.NegativeInfinity;
            this.numericBoxTau.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxTau.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxTau.MouseSpeed = 1D;
            this.numericBoxTau.Multiline = false;
            this.numericBoxTau.Name = "numericBoxTau";
            this.numericBoxTau.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxTau.RadianValue = 0D;
            this.numericBoxTau.ReadOnly = false;
            this.numericBoxTau.RestrictLimitValue = true;
            this.numericBoxTau.ShowFraction = false;
            this.numericBoxTau.ShowPositiveSign = false;
            this.numericBoxTau.ShowUpDown = false;
            this.numericBoxTau.Size = new System.Drawing.Size(115, 25);
            this.numericBoxTau.SkipEventDuringInput = false;
            this.numericBoxTau.SmartIncrement = true;
            this.numericBoxTau.TabIndex = 2;
            this.numericBoxTau.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxTau.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxTau.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxTau.ThonsandsSeparator = true;
            this.numericBoxTau.ToolTip = "";
            this.numericBoxTau.UpDown_Increment = 1D;
            this.numericBoxTau.Value = 0D;
            this.numericBoxTau.WordWrap = true;
            this.numericBoxTau.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxTau_ValueChanged);
            // 
            // checkBoxSchematicDiagram
            // 
            this.checkBoxSchematicDiagram.AutoSize = true;
            this.checkBoxSchematicDiagram.Location = new System.Drawing.Point(391, 0);
            this.checkBoxSchematicDiagram.Name = "checkBoxSchematicDiagram";
            this.checkBoxSchematicDiagram.Size = new System.Drawing.Size(172, 21);
            this.checkBoxSchematicDiagram.TabIndex = 55;
            this.checkBoxSchematicDiagram.Text = "Show schematic diagram";
            this.checkBoxSchematicDiagram.UseVisualStyleBackColor = true;
            this.checkBoxSchematicDiagram.CheckedChanged += new System.EventHandler(this.CheckBoxShowSchematicDiagram_CheckedChanged);
            // 
            // flowLayoutPanelSchematicDiagram
            // 
            this.flowLayoutPanelSchematicDiagram.AutoSize = true;
            this.flowLayoutPanelSchematicDiagram.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelSchematicDiagram.Controls.Add(this.pictureBox2);
            this.flowLayoutPanelSchematicDiagram.Controls.Add(this.pictureBox1);
            this.flowLayoutPanelSchematicDiagram.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanelSchematicDiagram.Location = new System.Drawing.Point(0, 25);
            this.flowLayoutPanelSchematicDiagram.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelSchematicDiagram.Name = "flowLayoutPanelSchematicDiagram";
            this.flowLayoutPanelSchematicDiagram.Size = new System.Drawing.Size(644, 307);
            this.flowLayoutPanelSchematicDiagram.TabIndex = 56;
            this.flowLayoutPanelSchematicDiagram.Visible = false;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.checkBoxDetectorSizePosition);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 332);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(644, 190);
            this.panel2.TabIndex = 57;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.checkBoxSchematicDiagram);
            this.panel1.Controls.Add(this.numericBoxCameraLength2);
            this.panel1.Controls.Add(this.numericBoxTau);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 25);
            this.panel1.TabIndex = 58;
            // 
            // FormDiffractionSimulatorGeometry
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(644, 523);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.flowLayoutPanelSchematicDiagram);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDiffractionSimulatorGeometry";
            this.ShowIcon = false;
            this.Text = "Detector geometry";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDiffractionSimulatorGeometry_FormClosing);
            this.Load += new System.EventHandler(this.FormDiffractionSimulatorGeometry_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDiffractionSimulatorGeometry_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDiffractionSimulatorGeometry_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMinInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPictureOpacity1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanelSchematicDiagram.ResumeLayout(false);
            this.flowLayoutPanelSchematicDiagram.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Crystallography.Controls.NumericBox numericBoxTau;
        private Crystallography.Controls.NumericBox numericBoxCameraLength2;
        private Crystallography.Controls.NumericBox numericalTextBoxFootY;
        private Crystallography.Controls.NumericBox numericalTextBoxFootX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private Crystallography.Controls.NumericBox numericalTextBoxPixelWidth;
        private Crystallography.Controls.NumericBox numericalTextBoxPixelHeight;
        private Crystallography.Controls.NumericBox numericalTextBoxPixelSize;
        private System.Windows.Forms.CheckBox checkBoxDetectorSizePosition;
        public System.Windows.Forms.ComboBox comboBoxScale2;
        public System.Windows.Forms.ComboBox comboBoxScale1;
        public System.Windows.Forms.ComboBox comboBoxGradient;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TrackBar trackBarPictureOpacity1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TrackBar trackBarMaxInt;
        private System.Windows.Forms.TrackBar trackBarMinInt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClearPicture;
        private System.Windows.Forms.Button buttonReadPicture;
        private System.Windows.Forms.Button buttonRot90;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.CheckBox checkBoxSchematicDiagram;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSchematicDiagram;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}