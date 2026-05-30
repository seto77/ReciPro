namespace ReciPro
{
    partial class FormDiffractionSimulatorGeometry
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
            components = new System.ComponentModel.Container();
            toolTip = new System.Windows.Forms.ToolTip(components);
            groupBoxDetectorAndOverlappedImage = new System.Windows.Forms.GroupBox();
            textBoxFileName = new System.Windows.Forms.TextBox();
            buttonClearPicture = new System.Windows.Forms.Button();
            trackBarMaxInt = new System.Windows.Forms.TrackBar();
            buttonRot90 = new System.Windows.Forms.Button();
            buttonLoadPicture = new System.Windows.Forms.Button();
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
            sizeControl1 = new SizeControl();
            numericBoxFootX = new NumericBox();
            numericBoxPixelSize = new NumericBox();
            checkBoxDetectorSizePosition = new System.Windows.Forms.CheckBox();
            numericBoxCameraLength2 = new NumericBox();
            numericBoxTau = new NumericBox();
            checkBoxSchematicDiagram = new System.Windows.Forms.CheckBox();
            panelDetectorAreaAndOverlappedImage = new System.Windows.Forms.Panel();
            panelDetectorGeometry = new System.Windows.Forms.Panel();
            numericBoxPhi = new NumericBox();
            panelSchematicDiagram = new System.Windows.Forms.Panel();
            pictureBoxSchematicDiagram = new System.Windows.Forms.PictureBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxDetectorAndOverlappedImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarMaxInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMinInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPictureOpacity1).BeginInit();
            panelDetectorAreaAndOverlappedImage.SuspendLayout();
            panelDetectorGeometry.SuspendLayout();
            panelSchematicDiagram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSchematicDiagram).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxDetectorAndOverlappedImage
            // 
            groupBoxDetectorAndOverlappedImage.Controls.Add(flowLayoutPanel4);
            groupBoxDetectorAndOverlappedImage.Controls.Add(label4);
            groupBoxDetectorAndOverlappedImage.Controls.Add(flowLayoutPanel3);
            groupBoxDetectorAndOverlappedImage.Controls.Add(flowLayoutPanel1);
            groupBoxDetectorAndOverlappedImage.Controls.Add(flowLayoutPanel2);
            groupBoxDetectorAndOverlappedImage.Enabled = false;
            groupBoxDetectorAndOverlappedImage.Location = new System.Drawing.Point(7, 3);
            groupBoxDetectorAndOverlappedImage.Name = "groupBoxDetectorAndOverlappedImage";
            groupBoxDetectorAndOverlappedImage.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxDetectorAndOverlappedImage.Size = new System.Drawing.Size(623, 166);
            groupBoxDetectorAndOverlappedImage.TabIndex = 5;
            groupBoxDetectorAndOverlappedImage.TabStop = false;
            // 
            // textBoxFileName
            // 
            textBoxFileName.Font = new System.Drawing.Font("Segoe UI", 9F);
            textBoxFileName.Location = new System.Drawing.Point(3, 3);
            textBoxFileName.Name = "textBoxFileName";
            textBoxFileName.ReadOnly = true;
            textBoxFileName.Size = new System.Drawing.Size(451, 23);
            textBoxFileName.TabIndex = 103;
            textBoxFileName.TextChanged += textBoxFileName_TextChanged;
            // 
            // buttonClearPicture
            // 
            buttonClearPicture.AutoSize = true;
            buttonClearPicture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonClearPicture.Font = new System.Drawing.Font("Segoe UI", 9F);
            buttonClearPicture.Location = new System.Drawing.Point(458, 0);
            buttonClearPicture.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            buttonClearPicture.Name = "buttonClearPicture";
            buttonClearPicture.Size = new System.Drawing.Size(44, 25);
            buttonClearPicture.TabIndex = 89;
            buttonClearPicture.Text = "Clear";
            buttonClearPicture.UseVisualStyleBackColor = true;
            buttonClearPicture.Click += buttonClearPicture_Click;
            // 
            // trackBarMaxInt
            // 
            trackBarMaxInt.AutoSize = false;
            trackBarMaxInt.Location = new System.Drawing.Point(52, 31);
            trackBarMaxInt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            trackBarMaxInt.Maximum = 100001;
            trackBarMaxInt.Minimum = 1;
            trackBarMaxInt.Name = "trackBarMaxInt";
            trackBarMaxInt.Size = new System.Drawing.Size(490, 19);
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
            buttonRot90.Font = new System.Drawing.Font("Segoe UI", 9F);
            buttonRot90.Location = new System.Drawing.Point(549, 0);
            buttonRot90.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            buttonRot90.Name = "buttonRot90";
            buttonRot90.Size = new System.Drawing.Size(55, 25);
            buttonRot90.TabIndex = 88;
            buttonRot90.Text = "Rot 90°";
            buttonRot90.UseVisualStyleBackColor = true;
            buttonRot90.Click += buttonRot90_Click;
            // 
            // buttonLoadPicture
            // 
            buttonLoadPicture.AutoSize = true;
            buttonLoadPicture.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonLoadPicture.Font = new System.Drawing.Font("Segoe UI", 9F);
            buttonLoadPicture.Location = new System.Drawing.Point(504, 0);
            buttonLoadPicture.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            buttonLoadPicture.Name = "buttonLoadPicture";
            buttonLoadPicture.Size = new System.Drawing.Size(43, 25);
            buttonLoadPicture.TabIndex = 88;
            buttonLoadPicture.Text = "Load";
            buttonLoadPicture.UseVisualStyleBackColor = true;
            buttonLoadPicture.Click += buttonLoadPicture_Click;
            // 
            // trackBarMinInt
            // 
            trackBarMinInt.AutoSize = false;
            trackBarMinInt.Location = new System.Drawing.Point(51, 4);
            trackBarMinInt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            trackBarMinInt.Maximum = 100000;
            trackBarMinInt.Name = "trackBarMinInt";
            trackBarMinInt.Size = new System.Drawing.Size(490, 19);
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
            trackBarPictureOpacity1.Location = new System.Drawing.Point(51, 4);
            trackBarPictureOpacity1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            trackBarPictureOpacity1.Maximum = 100;
            trackBarPictureOpacity1.Name = "trackBarPictureOpacity1";
            trackBarPictureOpacity1.Size = new System.Drawing.Size(100, 19);
            trackBarPictureOpacity1.TabIndex = 102;
            trackBarPictureOpacity1.TickFrequency = 10;
            trackBarPictureOpacity1.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarPictureOpacity1.Value = 100;
            trackBarPictureOpacity1.ValueChanged += trackBarPictureOpacity1_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            label2.Location = new System.Drawing.Point(0, 0);
            label2.Margin = new System.Windows.Forms.Padding(0);
            label2.Name = "label2";
            label2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label2.Size = new System.Drawing.Size(48, 18);
            label2.TabIndex = 101;
            label2.Text = "Min int.";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            label1.Location = new System.Drawing.Point(0, 27);
            label1.Margin = new System.Windows.Forms.Padding(0);
            label1.Name = "label1";
            label1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label1.Size = new System.Drawing.Size(49, 18);
            label1.TabIndex = 101;
            label1.Text = "Max int.";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = System.Windows.Forms.DockStyle.Left;
            label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            label4.Location = new System.Drawing.Point(3, 110);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(62, 15);
            label4.TabIndex = 101;
            label4.Text = "Brightness";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Segoe UI", 9F);
            label10.Location = new System.Drawing.Point(0, 0);
            label10.Margin = new System.Windows.Forms.Padding(0);
            label10.Name = "label10";
            label10.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label10.Size = new System.Drawing.Size(48, 18);
            label10.TabIndex = 101;
            label10.Text = "Opacity";
            // 
            // comboBoxScale2
            // 
            comboBoxScale2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale2.FormattingEnabled = true;
            comboBoxScale2.Items.AddRange(new object[] { "Gray", "Cold-Warm", "Spectrum", "Fire" });
            comboBoxScale2.Location = new System.Drawing.Point(514, 0);
            comboBoxScale2.Margin = new System.Windows.Forms.Padding(0);
            comboBoxScale2.Name = "comboBoxScale2";
            comboBoxScale2.Size = new System.Drawing.Size(93, 25);
            comboBoxScale2.TabIndex = 97;
            comboBoxScale2.SelectedIndexChanged += toolStripComboBoxScale2_SelectedIndexChanged;
            // 
            // comboBoxScale1
            // 
            comboBoxScale1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale1.FormattingEnabled = true;
            comboBoxScale1.Items.AddRange(new object[] { "Log Scale", "Linear Scale" });
            comboBoxScale1.Location = new System.Drawing.Point(363, 0);
            comboBoxScale1.Margin = new System.Windows.Forms.Padding(0);
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
            comboBoxGradient.Location = new System.Drawing.Point(212, 0);
            comboBoxGradient.Margin = new System.Windows.Forms.Padding(0);
            comboBoxGradient.Name = "comboBoxGradient";
            comboBoxGradient.Size = new System.Drawing.Size(94, 25);
            comboBoxGradient.TabIndex = 95;
            comboBoxGradient.SelectedIndexChanged += toolStripComboBoxGradient_SelectedIndexChanged;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new System.Drawing.Point(457, 0);
            label22.Margin = new System.Windows.Forms.Padding(0);
            label22.Name = "label22";
            label22.Padding = new System.Windows.Forms.Padding(8, 3, 0, 0);
            label22.Size = new System.Drawing.Size(57, 20);
            label22.TabIndex = 100;
            label22.Text = "Scale 2";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new System.Drawing.Point(306, 0);
            label23.Margin = new System.Windows.Forms.Padding(0);
            label23.Name = "label23";
            label23.Padding = new System.Windows.Forms.Padding(8, 3, 0, 0);
            label23.Size = new System.Drawing.Size(57, 20);
            label23.TabIndex = 99;
            label23.Text = "Scale 1";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Location = new System.Drawing.Point(154, 0);
            label24.Margin = new System.Windows.Forms.Padding(0);
            label24.Name = "label24";
            label24.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label24.Size = new System.Drawing.Size(58, 20);
            label24.TabIndex = 98;
            label24.Text = "Gradient";
            // 
            // numericBoxFootY
            // 
            numericBoxFootY.BackColor = System.Drawing.SystemColors.Control;
            numericBoxFootY.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxFootY.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxFootY.FooterPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxFootY.FooterText = "px";
            numericBoxFootY.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxFootY.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxFootY.HeaderPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxFootY.HeaderText = "fy";
            numericBoxFootY.Location = new System.Drawing.Point(342, 3);
            numericBoxFootY.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxFootY.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxFootY.Name = "numericBoxFootY";
            numericBoxFootY.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxFootY.RadianValue = 8.9360857702109673D;
            numericBoxFootY.Size = new System.Drawing.Size(103, 25);
            numericBoxFootY.SkipEventDuringInput = false;
            numericBoxFootY.SmartIncrement = true;
            numericBoxFootY.TabIndex = 0;
            numericBoxFootY.ThousandsSeparator = true;
            numericBoxFootY.Value = 512D;
            numericBoxFootY.ValueFontSize = 9F;
            // 
            // sizeControl1
            // 
            sizeControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            sizeControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            sizeControl1.HeaderText = "Detector";
            sizeControl1.LabelFont = new System.Drawing.Font("Segoe UI", 9F);
            sizeControl1.Location = new System.Drawing.Point(3, 3);
            sizeControl1.Maximum = 100000;
            sizeControl1.Name = "sizeControl1";
            sizeControl1.Size = new System.Drawing.Size(193, 25);
            sizeControl1.TabIndex = 0;
            sizeControl1.Value = new System.Drawing.Size(1024, 1024);
            sizeControl1.ValueFontSize = 9F;
            // 
            // numericBoxFootX
            // 
            numericBoxFootX.BackColor = System.Drawing.SystemColors.Control;
            numericBoxFootX.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxFootX.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxFootX.FooterPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxFootX.FooterText = "px";
            numericBoxFootX.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxFootX.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxFootX.HeaderPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxFootX.HeaderText = "Foot:  fx";
            numericBoxFootX.Location = new System.Drawing.Point(202, 3);
            numericBoxFootX.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxFootX.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxFootX.Name = "numericBoxFootX";
            numericBoxFootX.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxFootX.RadianValue = 8.9360857702109673D;
            numericBoxFootX.Size = new System.Drawing.Size(134, 25);
            numericBoxFootX.SkipEventDuringInput = false;
            numericBoxFootX.SmartIncrement = true;
            numericBoxFootX.TabIndex = 0;
            numericBoxFootX.ThousandsSeparator = true;
            numericBoxFootX.Value = 512D;
            numericBoxFootX.ValueFontSize = 9F;
            // 
            // numericBoxPixelSize
            // 
            numericBoxPixelSize.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelSize.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelSize.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxPixelSize.FooterPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxPixelSize.FooterText = "mm";
            numericBoxPixelSize.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPixelSize.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxPixelSize.HeaderPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxPixelSize.HeaderText = "pix. size";
            numericBoxPixelSize.Location = new System.Drawing.Point(448, 0);
            numericBoxPixelSize.Margin = new System.Windows.Forms.Padding(0);
            numericBoxPixelSize.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxPixelSize.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxPixelSize.Name = "numericBoxPixelSize";
            numericBoxPixelSize.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxPixelSize.RadianValue = 0.0017453292519943296D;
            numericBoxPixelSize.Size = new System.Drawing.Size(119, 27);
            numericBoxPixelSize.SkipEventDuringInput = false;
            numericBoxPixelSize.SmartIncrement = true;
            numericBoxPixelSize.TabIndex = 0;
            numericBoxPixelSize.ThousandsSeparator = true;
            numericBoxPixelSize.Value = 0.1D;
            numericBoxPixelSize.ValueFontSize = 9F;
            // 
            // checkBoxDetectorSizePosition
            // 
            checkBoxDetectorSizePosition.AutoSize = true;
            checkBoxDetectorSizePosition.Font = new System.Drawing.Font("Segoe UI", 9.75F);
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
            numericBoxCameraLength2.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength2.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxCameraLength2.FooterPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            numericBoxCameraLength2.FooterText = "mm";
            numericBoxCameraLength2.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength2.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxCameraLength2.HeaderPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
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
            numericBoxCameraLength2.Size = new System.Drawing.Size(220, 25);
            numericBoxCameraLength2.SkipEventDuringInput = false;
            numericBoxCameraLength2.SmartIncrement = true;
            numericBoxCameraLength2.TabIndex = 2;
            numericBoxCameraLength2.ThousandsSeparator = true;
            numericBoxCameraLength2.Value = 1000D;
            numericBoxCameraLength2.ValueChanged += numericBoxCameraLength2_ValueChanged;
            // 
            // numericBoxTau
            // 
            numericBoxTau.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTau.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTau.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxTau.FooterPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            numericBoxTau.FooterText = "°";
            numericBoxTau.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTau.HeaderFont = new System.Drawing.Font("Segoe UI", 11F);
            numericBoxTau.HeaderPadding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            numericBoxTau.HeaderText = "τ";
            numericBoxTau.Location = new System.Drawing.Point(339, 0);
            numericBoxTau.Margin = new System.Windows.Forms.Padding(0);
            numericBoxTau.Maximum = 200D;
            numericBoxTau.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxTau.Minimum = -200D;
            numericBoxTau.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxTau.Name = "numericBoxTau";
            numericBoxTau.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxTau.Size = new System.Drawing.Size(87, 25);
            numericBoxTau.SkipEventDuringInput = false;
            numericBoxTau.SmartIncrement = true;
            numericBoxTau.TabIndex = 2;
            numericBoxTau.ThousandsSeparator = true;
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
            // panelDetectorAreaAndOverlappedImage
            // 
            panelDetectorAreaAndOverlappedImage.AutoSize = true;
            panelDetectorAreaAndOverlappedImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            captureExtender.SetCapture(panelDetectorAreaAndOverlappedImage, true);
            panelDetectorAreaAndOverlappedImage.Controls.Add(checkBoxDetectorSizePosition);
            panelDetectorAreaAndOverlappedImage.Controls.Add(groupBoxDetectorAndOverlappedImage);
            panelDetectorAreaAndOverlappedImage.Dock = System.Windows.Forms.DockStyle.Top;
            panelDetectorAreaAndOverlappedImage.Location = new System.Drawing.Point(0, 655);
            panelDetectorAreaAndOverlappedImage.Margin = new System.Windows.Forms.Padding(0);
            panelDetectorAreaAndOverlappedImage.Name = "panelDetectorAreaAndOverlappedImage";
            panelDetectorAreaAndOverlappedImage.Size = new System.Drawing.Size(1007, 172);
            panelDetectorAreaAndOverlappedImage.TabIndex = 57;
            // 
            // panelDetectorGeometry
            // 
            panelDetectorGeometry.AutoSize = true;
            panelDetectorGeometry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            captureExtender.SetCapture(panelDetectorGeometry, true);
            panelDetectorGeometry.Controls.Add(checkBoxSchematicDiagram);
            panelDetectorGeometry.Controls.Add(numericBoxCameraLength2);
            panelDetectorGeometry.Controls.Add(numericBoxPhi);
            panelDetectorGeometry.Controls.Add(numericBoxTau);
            panelDetectorGeometry.Dock = System.Windows.Forms.DockStyle.Top;
            panelDetectorGeometry.Location = new System.Drawing.Point(0, 0);
            panelDetectorGeometry.Margin = new System.Windows.Forms.Padding(0);
            panelDetectorGeometry.Name = "panelDetectorGeometry";
            panelDetectorGeometry.Size = new System.Drawing.Size(1007, 25);
            panelDetectorGeometry.TabIndex = 58;
            // 
            // numericBoxPhi
            // 
            numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxPhi.FooterPadding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            numericBoxPhi.FooterText = "°";
            numericBoxPhi.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.HeaderFont = new System.Drawing.Font("Segoe UI", 11F);
            numericBoxPhi.HeaderPadding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            numericBoxPhi.HeaderText = "φ";
            numericBoxPhi.Location = new System.Drawing.Point(236, 0);
            numericBoxPhi.Margin = new System.Windows.Forms.Padding(0);
            numericBoxPhi.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxPhi.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxPhi.Name = "numericBoxPhi";
            numericBoxPhi.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxPhi.Size = new System.Drawing.Size(87, 25);
            numericBoxPhi.SkipEventDuringInput = false;
            numericBoxPhi.SmartIncrement = true;
            numericBoxPhi.TabIndex = 2;
            numericBoxPhi.ThousandsSeparator = true;
            numericBoxPhi.ValueChanged += numericBoxTau_ValueChanged;
            // 
            // panelSchematicDiagram
            // 
            panelSchematicDiagram.Controls.Add(pictureBoxSchematicDiagram);
            panelSchematicDiagram.Dock = System.Windows.Forms.DockStyle.Top;
            panelSchematicDiagram.Location = new System.Drawing.Point(0, 25);
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
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Controls.Add(sizeControl1);
            flowLayoutPanel1.Controls.Add(numericBoxFootX);
            flowLayoutPanel1.Controls.Add(numericBoxFootY);
            flowLayoutPanel1.Controls.Add(numericBoxPixelSize);
            flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel1.Location = new System.Drawing.Point(3, 52);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(617, 31);
            flowLayoutPanel1.TabIndex = 104;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel2.Controls.Add(textBoxFileName);
            flowLayoutPanel2.Controls.Add(buttonClearPicture);
            flowLayoutPanel2.Controls.Add(buttonLoadPicture);
            flowLayoutPanel2.Controls.Add(buttonRot90);
            flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel2.Location = new System.Drawing.Point(3, 23);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(617, 29);
            flowLayoutPanel2.TabIndex = 7;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel3.Controls.Add(label10);
            flowLayoutPanel3.Controls.Add(trackBarPictureOpacity1);
            flowLayoutPanel3.Controls.Add(label24);
            flowLayoutPanel3.Controls.Add(comboBoxGradient);
            flowLayoutPanel3.Controls.Add(label23);
            flowLayoutPanel3.Controls.Add(comboBoxScale1);
            flowLayoutPanel3.Controls.Add(label22);
            flowLayoutPanel3.Controls.Add(comboBoxScale2);
            flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel3.Location = new System.Drawing.Point(3, 83);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new System.Drawing.Size(617, 27);
            flowLayoutPanel3.TabIndex = 7;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Controls.Add(label2);
            flowLayoutPanel4.Controls.Add(trackBarMinInt);
            flowLayoutPanel4.Controls.Add(label1);
            flowLayoutPanel4.Controls.Add(trackBarMaxInt);
            flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanel4.Location = new System.Drawing.Point(65, 110);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new System.Drawing.Size(555, 51);
            flowLayoutPanel4.TabIndex = 7;
            // 
            // FormDiffractionSimulatorGeometry
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.SystemColors.Control;
            ClientSize = new System.Drawing.Size(1007, 830);
            Controls.Add(panelDetectorAreaAndOverlappedImage);
            Controls.Add(panelSchematicDiagram);
            Controls.Add(panelDetectorGeometry);
            Font = new System.Drawing.Font("Segoe UI", 9.75F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            MaximizeBox = false;
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
            panelDetectorAreaAndOverlappedImage.ResumeLayout(false);
            panelDetectorAreaAndOverlappedImage.PerformLayout();
            panelDetectorGeometry.ResumeLayout(false);
            panelDetectorGeometry.PerformLayout();
            panelSchematicDiagram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxSchematicDiagram).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            // 260530Cl 追加: ツールチップ (SetToolTip方式)
            toolTip.SetToolTip(comboBoxGradient, "Selects positive or negative film gradient (negative inverts the displayed intensity scale of the overlapped image).");
            toolTip.SetToolTip(comboBoxScale1, "Switches the intensity mapping of the overlapped image between logarithmic and linear scale.");
            toolTip.SetToolTip(comboBoxScale2, "Selects the color scale (Gray, Cold-Warm, Spectrum, or Fire) used to render the overlapped image.");
            toolTip.SetToolTip(trackBarPictureOpacity1, "Adjusts the opacity (0-100%) of the overlapped image on the diffraction pattern.");
            toolTip.SetToolTip(label10, "Adjusts the opacity (0-100%) of the overlapped image on the diffraction pattern.");
            toolTip.SetToolTip(label24, "Selects positive or negative film gradient (negative inverts the displayed intensity scale of the overlapped image).");
            toolTip.SetToolTip(label23, "Switches the intensity mapping of the overlapped image between logarithmic and linear scale.");
            toolTip.SetToolTip(label22, "Selects the color scale (Gray, Cold-Warm, Spectrum, or Fire) used to render the overlapped image.");
            toolTip.SetToolTip(numericBoxFootX, "Sets the X pixel coordinate of the foot point (direct-beam center) on the detector.");
            toolTip.SetToolTip(numericBoxFootY, "Sets the Y pixel coordinate of the foot point (direct-beam center) on the detector.");
            toolTip.SetToolTip(numericBoxPixelSize, "Sets the physical size of one detector pixel in millimeters.");
            toolTip.SetToolTip(sizeControl1, "Sets the detector size (width x height) in pixels.");
            toolTip.SetToolTip(buttonClearPicture, "Clears the loaded overlapped image.");
            toolTip.SetToolTip(buttonLoadPicture, "Loads an image file to overlap on the diffraction pattern.");
            toolTip.SetToolTip(buttonRot90, "Rotates the overlapped image and detector dimensions by 90 degrees.");
            toolTip.SetToolTip(textBoxFileName, "Shows the file path of the loaded overlapped image (read-only).");
            toolTip.SetToolTip(trackBarMaxInt, "Sets the upper intensity limit for displaying the overlapped image.");
            toolTip.SetToolTip(trackBarMinInt, "Sets the lower intensity limit for displaying the overlapped image.");
            toolTip.SetToolTip(label1, "Sets the upper intensity limit for displaying the overlapped image.");
            toolTip.SetToolTip(label2, "Sets the lower intensity limit for displaying the overlapped image.");
            toolTip.SetToolTip(checkBoxSchematicDiagram, "When checked, shows the schematic diagram of the detector geometry.");
            toolTip.SetToolTip(numericBoxCameraLength2, "Sets the camera length to the detector plane in millimeters.");
            toolTip.SetToolTip(numericBoxPhi, "Sets the azimuthal angle phi of the detector tilt axis in degrees.");
            toolTip.SetToolTip(numericBoxTau, "Sets the detector tilt angle tau about the tilt axis in degrees.");
            toolTip.SetToolTip(label4, "Sets the upper intensity limit for displaying the overlapped image.");
            toolTip.SetToolTip(checkBoxDetectorSizePosition, "When checked, draws the detector area on the diffraction pattern and enables the overlapped-image settings.");
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Crystallography.Controls.NumericBox numericBoxTau;
        private Crystallography.Controls.NumericBox numericBoxCameraLength2;
        private Crystallography.Controls.NumericBox numericBoxFootY;
        private Crystallography.Controls.NumericBox numericBoxFootX;
        private System.Windows.Forms.GroupBox groupBoxDetectorAndOverlappedImage;
        private Crystallography.Controls.SizeControl sizeControl1; // 260521Cl: numericBoxPixelWidth/Height を置換
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
        private System.Windows.Forms.Button buttonLoadPicture;
        private System.Windows.Forms.Button buttonRot90;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.CheckBox checkBoxSchematicDiagram;
        private System.Windows.Forms.Panel panelDetectorAreaAndOverlappedImage;
        private System.Windows.Forms.Panel panelDetectorGeometry;
        private System.Windows.Forms.PictureBox pictureBoxSchematicDiagram;
        private System.Windows.Forms.Panel panelSchematicDiagram;
        private Crystallography.Controls.NumericBox numericBoxPhi;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
    }
}
