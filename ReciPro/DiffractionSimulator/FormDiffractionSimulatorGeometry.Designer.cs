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
                pseudBitmap.Dispose();
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
            this.groupBoxDetectorAndOverlappedImage = new System.Windows.Forms.GroupBox();
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
            this.numericBoxFootY = new Crystallography.Controls.NumericBox();
            this.numericBoxPixelWidth = new Crystallography.Controls.NumericBox();
            this.numericBoxFootX = new Crystallography.Controls.NumericBox();
            this.numericBoxPixelHeight = new Crystallography.Controls.NumericBox();
            this.numericBoxPixelSize = new Crystallography.Controls.NumericBox();
            this.checkBoxDetectorSizePosition = new System.Windows.Forms.CheckBox();
            this.numericBoxCameraLength2 = new Crystallography.Controls.NumericBox();
            this.numericBoxTau = new Crystallography.Controls.NumericBox();
            this.checkBoxSchematicDiagram = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericBoxPhi = new Crystallography.Controls.NumericBox();
            this.panelSchematicDiagram = new System.Windows.Forms.Panel();
            this.pictureBoxSchematicDiagram = new System.Windows.Forms.PictureBox();
            this.groupBoxDetectorAndOverlappedImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMinInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPictureOpacity1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelSchematicDiagram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSchematicDiagram)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(373, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 51);
            this.label3.TabIndex = 1;
            this.label3.Text = "Foot of the\r\n perpendicular\r\n from the sample ";
            // 
            // groupBoxDetectorAndOverlappedImage
            // 
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.textBoxFileName);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.buttonClearPicture);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.trackBarMaxInt);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.buttonRot90);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.buttonReadPicture);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.trackBarMinInt);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.trackBarPictureOpacity1);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.label2);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.label1);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.label4);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.label10);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.comboBoxScale2);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.comboBoxScale1);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.comboBoxGradient);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.label22);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.label23);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.label24);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.numericBoxFootY);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.numericBoxPixelWidth);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.numericBoxFootX);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.numericBoxPixelHeight);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.label3);
            this.groupBoxDetectorAndOverlappedImage.Controls.Add(this.numericBoxPixelSize);
            this.groupBoxDetectorAndOverlappedImage.Enabled = false;
            this.groupBoxDetectorAndOverlappedImage.Location = new System.Drawing.Point(7, 3);
            this.groupBoxDetectorAndOverlappedImage.Name = "groupBoxDetectorAndOverlappedImage";
            this.groupBoxDetectorAndOverlappedImage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxDetectorAndOverlappedImage.Size = new System.Drawing.Size(624, 182);
            this.groupBoxDetectorAndOverlappedImage.TabIndex = 5;
            this.groupBoxDetectorAndOverlappedImage.TabStop = false;
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(68, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 101;
            this.label2.Text = "Min int.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(67, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 15);
            this.label1.TabIndex = 101;
            this.label1.Text = "Max int.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(2, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 101;
            this.label4.Text = "Brightness";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
            // numericBoxFootY
            // 
            this.numericBoxFootY.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootY.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxFootY.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootY.FooterText = "pixel";
            this.numericBoxFootY.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootY.HeaderText = "fy";
            this.numericBoxFootY.Location = new System.Drawing.Point(491, 78);
            this.numericBoxFootY.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxFootY.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxFootY.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxFootY.Name = "numericBoxFootY";
            this.numericBoxFootY.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxFootY.RadianValue = 8.9360857702109673D;
            this.numericBoxFootY.Size = new System.Drawing.Size(123, 25);
            this.numericBoxFootY.SkipEventDuringInput = false;
            this.numericBoxFootY.SmartIncrement = true;
            this.numericBoxFootY.TabIndex = 0;
            this.numericBoxFootY.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxFootY.ThonsandsSeparator = true;
            this.numericBoxFootY.Value = 512D;
            // 
            // numericBoxPixelWidth
            // 
            this.numericBoxPixelWidth.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelWidth.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPixelWidth.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelWidth.FooterText = "pix.";
            this.numericBoxPixelWidth.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelWidth.HeaderText = "Detector    width";
            this.numericBoxPixelWidth.Location = new System.Drawing.Point(7, 51);
            this.numericBoxPixelWidth.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelWidth.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPixelWidth.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPixelWidth.Name = "numericBoxPixelWidth";
            this.numericBoxPixelWidth.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelWidth.RadianValue = 17.872171540421935D;
            this.numericBoxPixelWidth.Size = new System.Drawing.Size(174, 25);
            this.numericBoxPixelWidth.SkipEventDuringInput = false;
            this.numericBoxPixelWidth.SmartIncrement = true;
            this.numericBoxPixelWidth.TabIndex = 0;
            this.numericBoxPixelWidth.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPixelWidth.ThonsandsSeparator = true;
            this.numericBoxPixelWidth.Value = 1024D;
            // 
            // numericBoxFootX
            // 
            this.numericBoxFootX.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootX.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxFootX.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootX.FooterText = "pixel";
            this.numericBoxFootX.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootX.HeaderText = "fx";
            this.numericBoxFootX.Location = new System.Drawing.Point(491, 51);
            this.numericBoxFootX.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxFootX.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxFootX.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxFootX.Name = "numericBoxFootX";
            this.numericBoxFootX.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxFootX.RadianValue = 8.9360857702109673D;
            this.numericBoxFootX.Size = new System.Drawing.Size(123, 25);
            this.numericBoxFootX.SkipEventDuringInput = false;
            this.numericBoxFootX.SmartIncrement = true;
            this.numericBoxFootX.TabIndex = 0;
            this.numericBoxFootX.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxFootX.ThonsandsSeparator = true;
            this.numericBoxFootX.Value = 512D;
            // 
            // numericBoxPixelHeight
            // 
            this.numericBoxPixelHeight.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelHeight.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPixelHeight.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelHeight.FooterText = "pix.";
            this.numericBoxPixelHeight.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelHeight.HeaderText = "height";
            this.numericBoxPixelHeight.Location = new System.Drawing.Point(68, 78);
            this.numericBoxPixelHeight.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelHeight.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPixelHeight.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPixelHeight.Name = "numericBoxPixelHeight";
            this.numericBoxPixelHeight.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelHeight.RadianValue = 17.872171540421935D;
            this.numericBoxPixelHeight.Size = new System.Drawing.Size(113, 25);
            this.numericBoxPixelHeight.SkipEventDuringInput = false;
            this.numericBoxPixelHeight.SmartIncrement = true;
            this.numericBoxPixelHeight.TabIndex = 0;
            this.numericBoxPixelHeight.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPixelHeight.ThonsandsSeparator = true;
            this.numericBoxPixelHeight.Value = 1024D;
            // 
            // numericBoxPixelSize
            // 
            this.numericBoxPixelSize.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelSize.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPixelSize.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelSize.FooterText = "mm";
            this.numericBoxPixelSize.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelSize.HeaderText = "pix. size";
            this.numericBoxPixelSize.Location = new System.Drawing.Point(194, 51);
            this.numericBoxPixelSize.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelSize.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPixelSize.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPixelSize.Name = "numericBoxPixelSize";
            this.numericBoxPixelSize.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelSize.RadianValue = 0.0017453292519943296D;
            this.numericBoxPixelSize.Size = new System.Drawing.Size(143, 25);
            this.numericBoxPixelSize.SkipEventDuringInput = false;
            this.numericBoxPixelSize.SmartIncrement = true;
            this.numericBoxPixelSize.TabIndex = 0;
            this.numericBoxPixelSize.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPixelSize.ThonsandsSeparator = true;
            this.numericBoxPixelSize.Value = 0.1D;
            // 
            // checkBoxDetectorSizePosition
            // 
            this.checkBoxDetectorSizePosition.AutoSize = true;
            this.checkBoxDetectorSizePosition.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxDetectorSizePosition.Location = new System.Drawing.Point(5, 0);
            this.checkBoxDetectorSizePosition.Name = "checkBoxDetectorSizePosition";
            this.checkBoxDetectorSizePosition.Size = new System.Drawing.Size(256, 21);
            this.checkBoxDetectorSizePosition.TabIndex = 6;
            this.checkBoxDetectorSizePosition.Text = "Set detector area && overlapped image.";
            this.checkBoxDetectorSizePosition.UseVisualStyleBackColor = true;
            this.checkBoxDetectorSizePosition.CheckedChanged += new System.EventHandler(this.checkBoxDetectorSizePosition_CheckedChanged);
            // 
            // numericBoxCameraLength2
            // 
            this.numericBoxCameraLength2.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCameraLength2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxCameraLength2.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCameraLength2.FooterText = "mm";
            this.numericBoxCameraLength2.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCameraLength2.HeaderText = "Camera length 2";
            this.numericBoxCameraLength2.Location = new System.Drawing.Point(0, 0);
            this.numericBoxCameraLength2.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCameraLength2.Maximum = 1000000D;
            this.numericBoxCameraLength2.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCameraLength2.Minimum = 1D;
            this.numericBoxCameraLength2.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCameraLength2.Name = "numericBoxCameraLength2";
            this.numericBoxCameraLength2.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCameraLength2.RadianValue = 17.453292519943293D;
            this.numericBoxCameraLength2.Size = new System.Drawing.Size(220, 25);
            this.numericBoxCameraLength2.SkipEventDuringInput = false;
            this.numericBoxCameraLength2.SmartIncrement = true;
            this.numericBoxCameraLength2.TabIndex = 2;
            this.numericBoxCameraLength2.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxCameraLength2.ThonsandsSeparator = true;
            this.numericBoxCameraLength2.Value = 1000D;
            this.numericBoxCameraLength2.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCameraLength2_ValueChanged);
            // 
            // numericBoxTau
            // 
            this.numericBoxTau.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTau.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxTau.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTau.FooterText = "°";
            this.numericBoxTau.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTau.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxTau.HeaderText = "τ";
            this.numericBoxTau.Location = new System.Drawing.Point(339, 0);
            this.numericBoxTau.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxTau.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxTau.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxTau.Name = "numericBoxTau";
            this.numericBoxTau.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxTau.Size = new System.Drawing.Size(87, 25);
            this.numericBoxTau.SkipEventDuringInput = false;
            this.numericBoxTau.SmartIncrement = true;
            this.numericBoxTau.TabIndex = 2;
            this.numericBoxTau.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxTau.ThonsandsSeparator = true;
            this.numericBoxTau.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxTau_ValueChanged);
            // 
            // checkBoxSchematicDiagram
            // 
            this.checkBoxSchematicDiagram.AutoSize = true;
            this.checkBoxSchematicDiagram.Location = new System.Drawing.Point(443, 0);
            this.checkBoxSchematicDiagram.Name = "checkBoxSchematicDiagram";
            this.checkBoxSchematicDiagram.Size = new System.Drawing.Size(138, 21);
            this.checkBoxSchematicDiagram.TabIndex = 55;
            this.checkBoxSchematicDiagram.Text = "Schematic diagram";
            this.checkBoxSchematicDiagram.UseVisualStyleBackColor = true;
            this.checkBoxSchematicDiagram.CheckedChanged += new System.EventHandler(this.CheckBoxShowSchematicDiagram_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.checkBoxDetectorSizePosition);
            this.panel2.Controls.Add(this.groupBoxDetectorAndOverlappedImage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 655);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1007, 188);
            this.panel2.TabIndex = 57;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.checkBoxSchematicDiagram);
            this.panel1.Controls.Add(this.numericBoxCameraLength2);
            this.panel1.Controls.Add(this.numericBoxPhi);
            this.panel1.Controls.Add(this.numericBoxTau);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1007, 25);
            this.panel1.TabIndex = 58;
            // 
            // numericBoxPhi
            // 
            this.numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPhi.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.FooterText = "°";
            this.numericBoxPhi.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPhi.HeaderText = "φ";
            this.numericBoxPhi.Location = new System.Drawing.Point(236, 0);
            this.numericBoxPhi.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPhi.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPhi.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPhi.Name = "numericBoxPhi";
            this.numericBoxPhi.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPhi.Size = new System.Drawing.Size(87, 25);
            this.numericBoxPhi.SkipEventDuringInput = false;
            this.numericBoxPhi.SmartIncrement = true;
            this.numericBoxPhi.TabIndex = 2;
            this.numericBoxPhi.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPhi.ThonsandsSeparator = true;
            this.numericBoxPhi.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxTau_ValueChanged);
            // 
            // panelSchematicDiagram
            // 
            this.panelSchematicDiagram.Controls.Add(this.pictureBoxSchematicDiagram);
            this.panelSchematicDiagram.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSchematicDiagram.Location = new System.Drawing.Point(0, 25);
            this.panelSchematicDiagram.Margin = new System.Windows.Forms.Padding(0);
            this.panelSchematicDiagram.Name = "panelSchematicDiagram";
            this.panelSchematicDiagram.Size = new System.Drawing.Size(1007, 630);
            this.panelSchematicDiagram.TabIndex = 7;
            // 
            // pictureBoxSchematicDiagram
            // 
            this.pictureBoxSchematicDiagram.BackColor = System.Drawing.Color.White;
            this.pictureBoxSchematicDiagram.Image = global::ReciPro.Properties.Resources.geometry;
            this.pictureBoxSchematicDiagram.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxSchematicDiagram.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxSchematicDiagram.Name = "pictureBoxSchematicDiagram";
            this.pictureBoxSchematicDiagram.Size = new System.Drawing.Size(1006, 631);
            this.pictureBoxSchematicDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSchematicDiagram.TabIndex = 59;
            this.pictureBoxSchematicDiagram.TabStop = false;
            // 
            // FormDiffractionSimulatorGeometry
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1007, 845);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelSchematicDiagram);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
            this.groupBoxDetectorAndOverlappedImage.ResumeLayout(false);
            this.groupBoxDetectorAndOverlappedImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMaxInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMinInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPictureOpacity1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelSchematicDiagram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSchematicDiagram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Crystallography.Controls.NumericBox numericBoxTau;
        private Crystallography.Controls.NumericBox numericBoxCameraLength2;
        private Crystallography.Controls.NumericBox numericBoxFootY;
        private Crystallography.Controls.NumericBox numericBoxFootX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxDetectorAndOverlappedImage;
        private Crystallography.Controls.NumericBox numericBoxPixelWidth;
        private Crystallography.Controls.NumericBox numericBoxPixelHeight;
        private Crystallography.Controls.NumericBox numericBoxPixelSize;
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxSchematicDiagram;
        private System.Windows.Forms.Panel panelSchematicDiagram;
        private Crystallography.Controls.NumericBox numericBoxPhi;
    }
}