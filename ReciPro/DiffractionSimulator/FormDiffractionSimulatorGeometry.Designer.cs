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
            label3 = new System.Windows.Forms.Label();
            groupBoxDetectorAndOverlappedImage = new System.Windows.Forms.GroupBox();
            textBoxFileName = new System.Windows.Forms.TextBox();
            buttonClearPicture = new System.Windows.Forms.Button();
            trackBarMaxInt = new System.Windows.Forms.TrackBar();
            buttonRot90 = new System.Windows.Forms.Button();
            buttonReadPicture = new System.Windows.Forms.Button();
            trackBarMinInt = new System.Windows.Forms.TrackBar();
            trackBarPictureOpacity1 = new System.Windows.Forms.TrackBar();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            comboBoxScale2 = new System.Windows.Forms.ComboBox();
            comboBoxScale1 = new System.Windows.Forms.ComboBox();
            comboBoxGradient = new System.Windows.Forms.ComboBox();
            label22 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            label24 = new System.Windows.Forms.Label();
            numericBoxFootY = new NumericBox();
            numericBoxPixelWidth = new NumericBox();
            numericBoxFootX = new NumericBox();
            numericBoxPixelHeight = new NumericBox();
            numericBoxPixelSize = new NumericBox();
            checkBoxDetectorSizePosition = new System.Windows.Forms.CheckBox();
            numericBoxCameraLength2 = new NumericBox();
            numericBoxTau = new NumericBox();
            checkBoxSchematicDiagram = new System.Windows.Forms.CheckBox();
            panel2 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            numericBoxPhi = new NumericBox();
            panelSchematicDiagram = new System.Windows.Forms.Panel();
            pictureBoxSchematicDiagram = new System.Windows.Forms.PictureBox();
            groupBoxDetectorAndOverlappedImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMaxInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMinInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPictureOpacity1).BeginInit();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            panelSchematicDiagram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSchematicDiagram).BeginInit();
            SuspendLayout();
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label3.Location = new System.Drawing.Point(373, 50);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(112, 51);
            label3.TabIndex = 1;
            label3.Text = "Foot of the\r\n perpendicular\r\n from the sample ";
            // 
            // groupBoxDetectorAndOverlappedImage
            // 
            groupBoxDetectorAndOverlappedImage.Controls.Add(textBoxFileName);
            groupBoxDetectorAndOverlappedImage.Controls.Add(buttonClearPicture);
            groupBoxDetectorAndOverlappedImage.Controls.Add(trackBarMaxInt);
            groupBoxDetectorAndOverlappedImage.Controls.Add(buttonRot90);
            groupBoxDetectorAndOverlappedImage.Controls.Add(buttonReadPicture);
            groupBoxDetectorAndOverlappedImage.Controls.Add(trackBarMinInt);
            groupBoxDetectorAndOverlappedImage.Controls.Add(trackBarPictureOpacity1);
            groupBoxDetectorAndOverlappedImage.Controls.Add(label2);
            groupBoxDetectorAndOverlappedImage.Controls.Add(label1);
            groupBoxDetectorAndOverlappedImage.Controls.Add(label4);
            groupBoxDetectorAndOverlappedImage.Controls.Add(label10);
            groupBoxDetectorAndOverlappedImage.Controls.Add(comboBoxScale2);
            groupBoxDetectorAndOverlappedImage.Controls.Add(comboBoxScale1);
            groupBoxDetectorAndOverlappedImage.Controls.Add(comboBoxGradient);
            groupBoxDetectorAndOverlappedImage.Controls.Add(label22);
            groupBoxDetectorAndOverlappedImage.Controls.Add(label23);
            groupBoxDetectorAndOverlappedImage.Controls.Add(label24);
            groupBoxDetectorAndOverlappedImage.Controls.Add(numericBoxFootY);
            groupBoxDetectorAndOverlappedImage.Controls.Add(numericBoxPixelWidth);
            groupBoxDetectorAndOverlappedImage.Controls.Add(numericBoxFootX);
            groupBoxDetectorAndOverlappedImage.Controls.Add(numericBoxPixelHeight);
            groupBoxDetectorAndOverlappedImage.Controls.Add(label3);
            groupBoxDetectorAndOverlappedImage.Controls.Add(numericBoxPixelSize);
            groupBoxDetectorAndOverlappedImage.Enabled = false;
            groupBoxDetectorAndOverlappedImage.Location = new System.Drawing.Point(7, 3);
            groupBoxDetectorAndOverlappedImage.Name = "groupBoxDetectorAndOverlappedImage";
            groupBoxDetectorAndOverlappedImage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxDetectorAndOverlappedImage.Size = new System.Drawing.Size(624, 182);
            groupBoxDetectorAndOverlappedImage.TabIndex = 5;
            groupBoxDetectorAndOverlappedImage.TabStop = false;
            // 
            // textBoxFileName
            // 
            textBoxFileName.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            textBoxFileName.Location = new System.Drawing.Point(7, 21);
            textBoxFileName.Name = "textBoxFileName";
            textBoxFileName.ReadOnly = true;
            textBoxFileName.Size = new System.Drawing.Size(443, 23);
            textBoxFileName.TabIndex = 103;
            textBoxFileName.TextChanged += textBoxFileName_TextChanged;
            // 
            // buttonClearPicture
            // 
            buttonClearPicture.AutoSize = true;
            buttonClearPicture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonClearPicture.Location = new System.Drawing.Point(502, 19);
            buttonClearPicture.Margin = new System.Windows.Forms.Padding(1, 4, 1, 4);
            buttonClearPicture.Name = "buttonClearPicture";
            buttonClearPicture.Size = new System.Drawing.Size(48, 27);
            buttonClearPicture.TabIndex = 89;
            buttonClearPicture.Text = "Clear";
            buttonClearPicture.UseVisualStyleBackColor = true;
            buttonClearPicture.Click += buttonClearPicture_Click;
            // 
            // trackBarMaxInt
            // 
            trackBarMaxInt.AutoSize = false;
            trackBarMaxInt.Location = new System.Drawing.Point(113, 160);
            trackBarMaxInt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            trackBarMaxInt.Maximum = 100001;
            trackBarMaxInt.Minimum = 1;
            trackBarMaxInt.Name = "trackBarMaxInt";
            trackBarMaxInt.Size = new System.Drawing.Size(505, 19);
            trackBarMaxInt.SmallChange = 10000;
            trackBarMaxInt.TabIndex = 102;
            trackBarMaxInt.TickFrequency = 10;
            trackBarMaxInt.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarMaxInt.Value = 100001;
            trackBarMaxInt.ValueChanged += trackBarMaxInt_ValueChanged;
            // 
            // buttonRot90
            // 
            buttonRot90.AutoSize = true;
            buttonRot90.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonRot90.Location = new System.Drawing.Point(556, 19);
            buttonRot90.Margin = new System.Windows.Forms.Padding(1, 4, 1, 4);
            buttonRot90.Name = "buttonRot90";
            buttonRot90.Size = new System.Drawing.Size(61, 27);
            buttonRot90.TabIndex = 88;
            buttonRot90.Text = "Rot 90°";
            buttonRot90.UseVisualStyleBackColor = true;
            buttonRot90.Click += buttonRot90_Click;
            // 
            // buttonReadPicture
            // 
            buttonReadPicture.AutoSize = true;
            buttonReadPicture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonReadPicture.Location = new System.Drawing.Point(454, 19);
            buttonReadPicture.Margin = new System.Windows.Forms.Padding(1, 4, 1, 4);
            buttonReadPicture.Name = "buttonReadPicture";
            buttonReadPicture.Size = new System.Drawing.Size(48, 27);
            buttonReadPicture.TabIndex = 88;
            buttonReadPicture.Text = "Read";
            buttonReadPicture.UseVisualStyleBackColor = true;
            buttonReadPicture.Click += buttonReadPicture_Click;
            // 
            // trackBarMinInt
            // 
            trackBarMinInt.AutoSize = false;
            trackBarMinInt.Location = new System.Drawing.Point(113, 142);
            trackBarMinInt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            trackBarMinInt.Maximum = 100000;
            trackBarMinInt.Name = "trackBarMinInt";
            trackBarMinInt.Size = new System.Drawing.Size(505, 19);
            trackBarMinInt.SmallChange = 10000;
            trackBarMinInt.TabIndex = 102;
            trackBarMinInt.TickFrequency = 10;
            trackBarMinInt.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarMinInt.Value = 1;
            trackBarMinInt.ValueChanged += trackBarMaxInt_ValueChanged;
            // 
            // trackBarPictureOpacity1
            // 
            trackBarPictureOpacity1.AutoSize = false;
            trackBarPictureOpacity1.Location = new System.Drawing.Point(57, 113);
            trackBarPictureOpacity1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            trackBarPictureOpacity1.Maximum = 100;
            trackBarPictureOpacity1.Name = "trackBarPictureOpacity1";
            trackBarPictureOpacity1.Size = new System.Drawing.Size(108, 19);
            trackBarPictureOpacity1.TabIndex = 102;
            trackBarPictureOpacity1.TickFrequency = 10;
            trackBarPictureOpacity1.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarPictureOpacity1.Value = 100;
            trackBarPictureOpacity1.ValueChanged += trackBarPictureOpacity1_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial", 9F);
            label2.Location = new System.Drawing.Point(68, 141);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(45, 15);
            label2.TabIndex = 101;
            label2.Text = "Min int.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial", 9F);
            label1.Location = new System.Drawing.Point(67, 160);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(47, 15);
            label1.TabIndex = 101;
            label1.Text = "Max int.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Arial", 9F);
            label4.Location = new System.Drawing.Point(2, 147);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(67, 15);
            label4.TabIndex = 101;
            label4.Text = "Brightness";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Arial", 9F);
            label10.Location = new System.Drawing.Point(4, 113);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(47, 15);
            label10.TabIndex = 101;
            label10.Text = "Opacity";
            // 
            // comboBoxScale2
            // 
            comboBoxScale2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale2.FormattingEnabled = true;
            comboBoxScale2.Items.AddRange(new object[] { "Gray", "Cold-Warm", "Spectrum", "Fire" });
            comboBoxScale2.Location = new System.Drawing.Point(522, 108);
            comboBoxScale2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            comboBoxScale2.Name = "comboBoxScale2";
            comboBoxScale2.Size = new System.Drawing.Size(93, 25);
            comboBoxScale2.TabIndex = 97;
            comboBoxScale2.SelectedIndexChanged += toolStripComboBoxScale2_SelectedIndexChanged;
            // 
            // comboBoxScale1
            // 
            comboBoxScale1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale1.FormattingEnabled = true;
            comboBoxScale1.Items.AddRange(new object[] { "Log Scale", "Liner Scale" });
            comboBoxScale1.Location = new System.Drawing.Point(376, 108);
            comboBoxScale1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            comboBoxScale1.Name = "comboBoxScale1";
            comboBoxScale1.Size = new System.Drawing.Size(94, 25);
            comboBoxScale1.TabIndex = 96;
            comboBoxScale1.SelectedIndexChanged += toolStripComboBoxScale_SelectedIndexChanged;
            // 
            // comboBoxGradient
            // 
            comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxGradient.FormattingEnabled = true;
            comboBoxGradient.Items.AddRange(new object[] { "Positive Film", "Negative Film" });
            comboBoxGradient.Location = new System.Drawing.Point(229, 108);
            comboBoxGradient.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            comboBoxGradient.Name = "comboBoxGradient";
            comboBoxGradient.Size = new System.Drawing.Size(94, 25);
            comboBoxGradient.TabIndex = 95;
            comboBoxGradient.SelectedIndexChanged += toolStripComboBoxGradient_SelectedIndexChanged;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new System.Drawing.Point(474, 111);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(49, 17);
            label22.TabIndex = 100;
            label22.Text = "Scale 2";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new System.Drawing.Point(329, 111);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(49, 17);
            label23.TabIndex = 99;
            label23.Text = "Scale 1";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new System.Drawing.Point(171, 111);
            label24.Name = "label24";
            label24.Size = new System.Drawing.Size(58, 17);
            label24.TabIndex = 98;
            label24.Text = "Gradient";
            // 
            // numericBoxFootY
            // 
            numericBoxFootY.BackColor = System.Drawing.SystemColors.Control;
            numericBoxFootY.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxFootY.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxFootY.FooterText = "pixel";
            numericBoxFootY.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxFootY.HeaderText = "fy";
            numericBoxFootY.Location = new System.Drawing.Point(491, 78);
            numericBoxFootY.Margin = new System.Windows.Forms.Padding(0);
            numericBoxFootY.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxFootY.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxFootY.Name = "numericBoxFootY";
            numericBoxFootY.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxFootY.RadianValue = 8.9360857702109673D;
            numericBoxFootY.RoundErrorAccuracy = -1;
            numericBoxFootY.Size = new System.Drawing.Size(123, 27);
            numericBoxFootY.SkipEventDuringInput = false;
            numericBoxFootY.SmartIncrement = true;
            numericBoxFootY.TabIndex = 0;
            numericBoxFootY.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxFootY.ThonsandsSeparator = true;
            numericBoxFootY.Value = 512D;
            // 
            // numericBoxPixelWidth
            // 
            numericBoxPixelWidth.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelWidth.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxPixelWidth.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelWidth.FooterText = "pix.";
            numericBoxPixelWidth.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelWidth.HeaderText = "Detector    width";
            numericBoxPixelWidth.Location = new System.Drawing.Point(7, 51);
            numericBoxPixelWidth.Margin = new System.Windows.Forms.Padding(0);
            numericBoxPixelWidth.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxPixelWidth.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxPixelWidth.Name = "numericBoxPixelWidth";
            numericBoxPixelWidth.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxPixelWidth.RadianValue = 17.872171540421935D;
            numericBoxPixelWidth.RoundErrorAccuracy = -1;
            numericBoxPixelWidth.Size = new System.Drawing.Size(174, 27);
            numericBoxPixelWidth.SkipEventDuringInput = false;
            numericBoxPixelWidth.SmartIncrement = true;
            numericBoxPixelWidth.TabIndex = 0;
            numericBoxPixelWidth.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxPixelWidth.ThonsandsSeparator = true;
            numericBoxPixelWidth.Value = 1024D;
            // 
            // numericBoxFootX
            // 
            numericBoxFootX.BackColor = System.Drawing.SystemColors.Control;
            numericBoxFootX.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxFootX.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxFootX.FooterText = "pixel";
            numericBoxFootX.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxFootX.HeaderText = "fx";
            numericBoxFootX.Location = new System.Drawing.Point(491, 51);
            numericBoxFootX.Margin = new System.Windows.Forms.Padding(0);
            numericBoxFootX.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxFootX.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxFootX.Name = "numericBoxFootX";
            numericBoxFootX.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxFootX.RadianValue = 8.9360857702109673D;
            numericBoxFootX.RoundErrorAccuracy = -1;
            numericBoxFootX.Size = new System.Drawing.Size(123, 27);
            numericBoxFootX.SkipEventDuringInput = false;
            numericBoxFootX.SmartIncrement = true;
            numericBoxFootX.TabIndex = 0;
            numericBoxFootX.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxFootX.ThonsandsSeparator = true;
            numericBoxFootX.Value = 512D;
            // 
            // numericBoxPixelHeight
            // 
            numericBoxPixelHeight.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelHeight.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxPixelHeight.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelHeight.FooterText = "pix.";
            numericBoxPixelHeight.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelHeight.HeaderText = "height";
            numericBoxPixelHeight.Location = new System.Drawing.Point(68, 78);
            numericBoxPixelHeight.Margin = new System.Windows.Forms.Padding(0);
            numericBoxPixelHeight.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxPixelHeight.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxPixelHeight.Name = "numericBoxPixelHeight";
            numericBoxPixelHeight.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxPixelHeight.RadianValue = 17.872171540421935D;
            numericBoxPixelHeight.RoundErrorAccuracy = -1;
            numericBoxPixelHeight.Size = new System.Drawing.Size(113, 27);
            numericBoxPixelHeight.SkipEventDuringInput = false;
            numericBoxPixelHeight.SmartIncrement = true;
            numericBoxPixelHeight.TabIndex = 0;
            numericBoxPixelHeight.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxPixelHeight.ThonsandsSeparator = true;
            numericBoxPixelHeight.Value = 1024D;
            // 
            // numericBoxPixelSize
            // 
            numericBoxPixelSize.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelSize.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxPixelSize.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelSize.FooterText = "mm";
            numericBoxPixelSize.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelSize.HeaderText = "pix. size";
            numericBoxPixelSize.Location = new System.Drawing.Point(194, 51);
            numericBoxPixelSize.Margin = new System.Windows.Forms.Padding(0);
            numericBoxPixelSize.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxPixelSize.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxPixelSize.Name = "numericBoxPixelSize";
            numericBoxPixelSize.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxPixelSize.RadianValue = 0.0017453292519943296D;
            numericBoxPixelSize.RoundErrorAccuracy = -1;
            numericBoxPixelSize.Size = new System.Drawing.Size(143, 27);
            numericBoxPixelSize.SkipEventDuringInput = false;
            numericBoxPixelSize.SmartIncrement = true;
            numericBoxPixelSize.TabIndex = 0;
            numericBoxPixelSize.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxPixelSize.ThonsandsSeparator = true;
            numericBoxPixelSize.Value = 0.1D;
            // 
            // checkBoxDetectorSizePosition
            // 
            checkBoxDetectorSizePosition.AutoSize = true;
            checkBoxDetectorSizePosition.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            checkBoxDetectorSizePosition.Location = new System.Drawing.Point(5, 0);
            checkBoxDetectorSizePosition.Name = "checkBoxDetectorSizePosition";
            checkBoxDetectorSizePosition.Size = new System.Drawing.Size(256, 21);
            checkBoxDetectorSizePosition.TabIndex = 6;
            checkBoxDetectorSizePosition.Text = "Set detector area && overlapped image.";
            checkBoxDetectorSizePosition.UseVisualStyleBackColor = true;
            checkBoxDetectorSizePosition.CheckedChanged += checkBoxDetectorSizePosition_CheckedChanged;
            // 
            // numericBoxCameraLength2
            // 
            numericBoxCameraLength2.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxCameraLength2.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength2.FooterText = "mm";
            numericBoxCameraLength2.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength2.HeaderText = "Camera length 2";
            numericBoxCameraLength2.Location = new System.Drawing.Point(0, 0);
            numericBoxCameraLength2.Margin = new System.Windows.Forms.Padding(0);
            numericBoxCameraLength2.Maximum = 1000000D;
            numericBoxCameraLength2.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxCameraLength2.Minimum = 1D;
            numericBoxCameraLength2.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxCameraLength2.Name = "numericBoxCameraLength2";
            numericBoxCameraLength2.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxCameraLength2.RadianValue = 17.453292519943293D;
            numericBoxCameraLength2.RoundErrorAccuracy = -1;
            numericBoxCameraLength2.Size = new System.Drawing.Size(220, 27);
            numericBoxCameraLength2.SkipEventDuringInput = false;
            numericBoxCameraLength2.SmartIncrement = true;
            numericBoxCameraLength2.TabIndex = 2;
            numericBoxCameraLength2.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxCameraLength2.ThonsandsSeparator = true;
            numericBoxCameraLength2.Value = 1000D;
            numericBoxCameraLength2.ValueChanged += numericBoxCameraLength2_ValueChanged;
            // 
            // numericBoxTau
            // 
            numericBoxTau.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTau.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxTau.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTau.FooterText = "°";
            numericBoxTau.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTau.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 11F);
            numericBoxTau.HeaderText = "τ";
            numericBoxTau.Location = new System.Drawing.Point(339, 0);
            numericBoxTau.Margin = new System.Windows.Forms.Padding(0);
            numericBoxTau.Maximum = 200D;
            numericBoxTau.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxTau.Minimum = -200D;
            numericBoxTau.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxTau.Name = "numericBoxTau";
            numericBoxTau.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxTau.RoundErrorAccuracy = -1;
            numericBoxTau.Size = new System.Drawing.Size(87, 27);
            numericBoxTau.SkipEventDuringInput = false;
            numericBoxTau.SmartIncrement = true;
            numericBoxTau.TabIndex = 2;
            numericBoxTau.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxTau.ThonsandsSeparator = true;
            numericBoxTau.ValueChanged += numericBoxTau_ValueChanged;
            // 
            // checkBoxSchematicDiagram
            // 
            checkBoxSchematicDiagram.AutoSize = true;
            checkBoxSchematicDiagram.Location = new System.Drawing.Point(443, 0);
            checkBoxSchematicDiagram.Name = "checkBoxSchematicDiagram";
            checkBoxSchematicDiagram.Size = new System.Drawing.Size(138, 21);
            checkBoxSchematicDiagram.TabIndex = 55;
            checkBoxSchematicDiagram.Text = "Schematic diagram";
            checkBoxSchematicDiagram.UseVisualStyleBackColor = true;
            checkBoxSchematicDiagram.CheckedChanged += CheckBoxShowSchematicDiagram_CheckedChanged;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panel2.Controls.Add(checkBoxDetectorSizePosition);
            panel2.Controls.Add(groupBoxDetectorAndOverlappedImage);
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(0, 657);
            panel2.Margin = new System.Windows.Forms.Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(1007, 188);
            panel2.TabIndex = 57;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(checkBoxSchematicDiagram);
            panel1.Controls.Add(numericBoxCameraLength2);
            panel1.Controls.Add(numericBoxPhi);
            panel1.Controls.Add(numericBoxTau);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Margin = new System.Windows.Forms.Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1007, 27);
            panel1.TabIndex = 58;
            // 
            // numericBoxPhi
            // 
            numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxPhi.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.FooterText = "°";
            numericBoxPhi.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 11F);
            numericBoxPhi.HeaderText = "φ";
            numericBoxPhi.Location = new System.Drawing.Point(236, 0);
            numericBoxPhi.Margin = new System.Windows.Forms.Padding(0);
            numericBoxPhi.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxPhi.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxPhi.Name = "numericBoxPhi";
            numericBoxPhi.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxPhi.RoundErrorAccuracy = -1;
            numericBoxPhi.Size = new System.Drawing.Size(87, 27);
            numericBoxPhi.SkipEventDuringInput = false;
            numericBoxPhi.SmartIncrement = true;
            numericBoxPhi.TabIndex = 2;
            numericBoxPhi.TextFont = new System.Drawing.Font("メイリオ", 9F);
            numericBoxPhi.ThonsandsSeparator = true;
            numericBoxPhi.ValueChanged += numericBoxTau_ValueChanged;
            // 
            // panelSchematicDiagram
            // 
            panelSchematicDiagram.Controls.Add(pictureBoxSchematicDiagram);
            panelSchematicDiagram.Dock = System.Windows.Forms.DockStyle.Top;
            panelSchematicDiagram.Location = new System.Drawing.Point(0, 27);
            panelSchematicDiagram.Margin = new System.Windows.Forms.Padding(0);
            panelSchematicDiagram.Name = "panelSchematicDiagram";
            panelSchematicDiagram.Size = new System.Drawing.Size(1007, 630);
            panelSchematicDiagram.TabIndex = 7;
            // 
            // pictureBoxSchematicDiagram
            // 
            pictureBoxSchematicDiagram.BackColor = System.Drawing.Color.White;
            pictureBoxSchematicDiagram.Image = Properties.Resources.geometry;
            pictureBoxSchematicDiagram.Location = new System.Drawing.Point(0, 0);
            pictureBoxSchematicDiagram.Margin = new System.Windows.Forms.Padding(0);
            pictureBoxSchematicDiagram.Name = "pictureBoxSchematicDiagram";
            pictureBoxSchematicDiagram.Size = new System.Drawing.Size(1006, 631);
            pictureBoxSchematicDiagram.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureBoxSchematicDiagram.TabIndex = 59;
            pictureBoxSchematicDiagram.TabStop = false;
            // 
            // FormDiffractionSimulatorGeometry
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(1007, 845);
            Controls.Add(panel2);
            Controls.Add(panelSchematicDiagram);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormDiffractionSimulatorGeometry";
            ShowIcon = false;
            Text = "Detector geometry";
            FormClosing += FormDiffractionSimulatorGeometry_FormClosing;
            Load += FormDiffractionSimulatorGeometry_Load;
            DragDrop += FormDiffractionSimulatorGeometry_DragDrop;
            DragEnter += FormDiffractionSimulatorGeometry_DragEnter;
            groupBoxDetectorAndOverlappedImage.ResumeLayout(false);
            groupBoxDetectorAndOverlappedImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMaxInt).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMinInt).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPictureOpacity1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelSchematicDiagram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxSchematicDiagram).EndInit();
            ResumeLayout(false);
            PerformLayout();
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