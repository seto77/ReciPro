namespace ReciPro
{
    partial class DiffractionPatternControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            if (pseudoBitmap != null)
                pseudoBitmap.Dispose();
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiffractionPatternControl));
            this.numericUpDownMaxInt = new System.Windows.Forms.NumericUpDown();
            this.label36 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.numericUpDownMinInt = new System.Windows.Forms.NumericUpDown();
            this.checkBoxSimulation = new System.Windows.Forms.CheckBox();
            this.checkBoxResidual = new System.Windows.Forms.CheckBox();
            this.trackBarOpacity = new System.Windows.Forms.TrackBar();
            this.checkBoxMaskDonut = new System.Windows.Forms.CheckBox();
            this.checkBoxMaskRectangle = new System.Windows.Forms.CheckBox();
            this.groupBoxCircleMask = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownCircleStart = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.numericUpDownCircleEnd = new System.Windows.Forms.NumericUpDown();
            this.buttonUnmaskAll = new System.Windows.Forms.Button();
            this.groupBoxRectangle = new System.Windows.Forms.GroupBox();
            this.numericUpDownRectangleAngle = new System.Windows.Forms.NumericUpDown();
            this.comboBoxRectangleDirection = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.numericUpDownRectangleBand = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.checkBoxRectangleIsBothSide = new System.Windows.Forms.CheckBox();
            this.buttonSaveMask = new System.Windows.Forms.Button();
            this.buttonApplyMask = new System.Windows.Forms.Button();
            this.groupBoxGeometry = new System.Windows.Forms.GroupBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.numericBoxTau = new Crystallography.Controls.NumericBox();
            this.numericBoxPhi = new Crystallography.Controls.NumericBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBarBgH = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.trackBarBgR = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.trackBarBgA = new System.Windows.Forms.TrackBar();
            this.groupBoxBackground = new System.Windows.Forms.GroupBox();
            this.checkBoxInitialBackground = new System.Windows.Forms.CheckBox();
            this.buttonSaveBackGround = new System.Windows.Forms.Button();
            this.panelSimulationCheck = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numericBoxConvergentAngle = new Crystallography.Controls.NumericBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.waveLengthControl = new Crystallography.Controls.WaveLengthControl();
            this.numericBoxMonochromaticity = new Crystallography.Controls.NumericBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericUpDownImageWidth = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.numericBoxImageResolution = new Crystallography.Controls.NumericBox();
            this.numericBoxMonitorResolution = new Crystallography.Controls.NumericBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericBoxCameraLength = new Crystallography.Controls.NumericBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.numericUpDownImageHeight = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.numericBoxCenterY = new Crystallography.Controls.NumericBox();
            this.numericBoxCenterX = new Crystallography.Controls.NumericBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.checkBoxFilmBlur = new System.Windows.Forms.CheckBox();
            this.label40 = new System.Windows.Forms.Label();
            this.numericBoxFilmBlur = new Crystallography.Controls.NumericBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxMaskDiffractionPeaks = new System.Windows.Forms.CheckBox();
            this.checkBoxMaskManual = new System.Windows.Forms.CheckBox();
            this.groupBoxPeakIndices = new System.Windows.Forms.GroupBox();
            this.checkedListBoxPlaneList = new System.Windows.Forms.CheckedListBox();
            this.buttonCheckAllIndices = new System.Windows.Forms.Button();
            this.buttonUnmaskSelectedPeaks = new System.Windows.Forms.Button();
            this.buttonUncheckAllIndices = new System.Windows.Forms.Button();
            this.groupBoxManualSpot = new System.Windows.Forms.GroupBox();
            this.radioButtonManualDonut = new System.Windows.Forms.RadioButton();
            this.radioButtonManualCircle = new System.Windows.Forms.RadioButton();
            this.radioButtonManualRectangle = new System.Windows.Forms.RadioButton();
            this.radioButtonManualSpot = new System.Windows.Forms.RadioButton();
            this.comboBoxSpotSize = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.buttonUnmask = new System.Windows.Forms.Button();
            this.buttonMaskAll = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.textBoxDiffractionInformation = new System.Windows.Forms.TextBox();
            this.checkBoxShowMaskedArea = new System.Windows.Forms.CheckBox();
            this.buttonSaveImage = new System.Windows.Forms.Button();
            this.graphControlFrequency = new Crystallography.Controls.GraphControl();
            this.scalablePictureBox = new Crystallography.Controls.ScalablePictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).BeginInit();
            this.groupBoxCircleMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleEnd)).BeginInit();
            this.groupBoxRectangle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRectangleAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRectangleBand)).BeginInit();
            this.groupBoxGeometry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBgH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBgR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBgA)).BeginInit();
            this.groupBoxBackground.SuspendLayout();
            this.panelSimulationCheck.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageHeight)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBoxPeakIndices.SuspendLayout();
            this.groupBoxManualSpot.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownMaxInt
            // 
            this.numericUpDownMaxInt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownMaxInt.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
            this.numericUpDownMaxInt.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownMaxInt.Location = new System.Drawing.Point(893, 555);
            this.numericUpDownMaxInt.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.numericUpDownMaxInt.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownMaxInt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxInt.Name = "numericUpDownMaxInt";
            this.numericUpDownMaxInt.Size = new System.Drawing.Size(73, 25);
            this.numericUpDownMaxInt.TabIndex = 167;
            this.numericUpDownMaxInt.Value = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownMaxInt.ValueChanged += new System.EventHandler(this.numericUpDownMaxInt_ValueChanged);
            // 
            // label36
            // 
            this.label36.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
            this.label36.Location = new System.Drawing.Point(714, 558);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(33, 18);
            this.label36.TabIndex = 166;
            this.label36.Text = "Min.";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
            this.label25.Location = new System.Drawing.Point(843, 557);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(37, 18);
            this.label25.TabIndex = 165;
            this.label25.Text = "Max.";
            // 
            // numericUpDownMinInt
            // 
            this.numericUpDownMinInt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownMinInt.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
            this.numericUpDownMinInt.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownMinInt.Location = new System.Drawing.Point(757, 555);
            this.numericUpDownMinInt.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.numericUpDownMinInt.Maximum = new decimal(new int[] {
            65534,
            0,
            0,
            0});
            this.numericUpDownMinInt.Name = "numericUpDownMinInt";
            this.numericUpDownMinInt.Size = new System.Drawing.Size(73, 25);
            this.numericUpDownMinInt.TabIndex = 168;
            this.numericUpDownMinInt.ValueChanged += new System.EventHandler(this.numericUpDownMinInt_ValueChanged);
            // 
            // checkBoxSimulation
            // 
            this.checkBoxSimulation.AutoSize = true;
            this.checkBoxSimulation.Location = new System.Drawing.Point(0, 3);
            this.checkBoxSimulation.Name = "checkBoxSimulation";
            this.checkBoxSimulation.Size = new System.Drawing.Size(86, 20);
            this.checkBoxSimulation.TabIndex = 179;
            this.checkBoxSimulation.Text = "Simulation";
            this.checkBoxSimulation.UseVisualStyleBackColor = true;
            this.checkBoxSimulation.CheckedChanged += new System.EventHandler(this.checkBoxSimulation_CheckedChanged);
            // 
            // checkBoxResidual
            // 
            this.checkBoxResidual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxResidual.AutoSize = true;
            this.checkBoxResidual.Location = new System.Drawing.Point(201, 3);
            this.checkBoxResidual.Name = "checkBoxResidual";
            this.checkBoxResidual.Size = new System.Drawing.Size(76, 20);
            this.checkBoxResidual.TabIndex = 182;
            this.checkBoxResidual.Text = "Residual";
            this.checkBoxResidual.UseVisualStyleBackColor = true;
            this.checkBoxResidual.CheckedChanged += new System.EventHandler(this.checkBoxResidual_CheckedChanged);
            // 
            // trackBarOpacity
            // 
            this.trackBarOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarOpacity.AutoSize = false;
            this.trackBarOpacity.Location = new System.Drawing.Point(83, 3);
            this.trackBarOpacity.Maximum = 100;
            this.trackBarOpacity.Name = "trackBarOpacity";
            this.trackBarOpacity.Size = new System.Drawing.Size(112, 20);
            this.trackBarOpacity.TabIndex = 181;
            this.trackBarOpacity.TickFrequency = 10;
            this.trackBarOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarOpacity.Value = 100;
            this.trackBarOpacity.Scroll += new System.EventHandler(this.trackBarOpacity_Scroll);
            // 
            // checkBoxMaskDonut
            // 
            this.checkBoxMaskDonut.AutoSize = true;
            this.checkBoxMaskDonut.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMaskDonut.Location = new System.Drawing.Point(8, 77);
            this.checkBoxMaskDonut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxMaskDonut.Name = "checkBoxMaskDonut";
            this.checkBoxMaskDonut.Size = new System.Drawing.Size(89, 18);
            this.checkBoxMaskDonut.TabIndex = 160;
            this.checkBoxMaskDonut.Text = "Donut Mask";
            this.checkBoxMaskDonut.UseVisualStyleBackColor = true;
            this.checkBoxMaskDonut.CheckedChanged += new System.EventHandler(this.checkBoxCircleMask_CheckedChanged);
            // 
            // checkBoxMaskRectangle
            // 
            this.checkBoxMaskRectangle.AutoSize = true;
            this.checkBoxMaskRectangle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMaskRectangle.Location = new System.Drawing.Point(8, 2);
            this.checkBoxMaskRectangle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxMaskRectangle.Name = "checkBoxMaskRectangle";
            this.checkBoxMaskRectangle.Size = new System.Drawing.Size(108, 18);
            this.checkBoxMaskRectangle.TabIndex = 161;
            this.checkBoxMaskRectangle.Text = "Rectangle Mask";
            this.checkBoxMaskRectangle.UseVisualStyleBackColor = true;
            this.checkBoxMaskRectangle.CheckedChanged += new System.EventHandler(this.checkBoxRectangleMask_CheckedChanged);
            // 
            // groupBoxCircleMask
            // 
            this.groupBoxCircleMask.Controls.Add(this.label1);
            this.groupBoxCircleMask.Controls.Add(this.numericUpDownCircleStart);
            this.groupBoxCircleMask.Controls.Add(this.label9);
            this.groupBoxCircleMask.Controls.Add(this.label21);
            this.groupBoxCircleMask.Controls.Add(this.numericUpDownCircleEnd);
            this.groupBoxCircleMask.Enabled = false;
            this.groupBoxCircleMask.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxCircleMask.Location = new System.Drawing.Point(3, 77);
            this.groupBoxCircleMask.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxCircleMask.Name = "groupBoxCircleMask";
            this.groupBoxCircleMask.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxCircleMask.Size = new System.Drawing.Size(227, 48);
            this.groupBoxCircleMask.TabIndex = 158;
            this.groupBoxCircleMask.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label1.Location = new System.Drawing.Point(103, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 14);
            this.label1.TabIndex = 15;
            this.label1.Text = "End";
            // 
            // numericUpDownCircleStart
            // 
            this.numericUpDownCircleStart.DecimalPlaces = 1;
            this.numericUpDownCircleStart.Font = new System.Drawing.Font("Tahoma", 9F);
            this.numericUpDownCircleStart.Location = new System.Drawing.Point(43, 19);
            this.numericUpDownCircleStart.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.numericUpDownCircleStart.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownCircleStart.Name = "numericUpDownCircleStart";
            this.numericUpDownCircleStart.Size = new System.Drawing.Size(49, 22);
            this.numericUpDownCircleStart.TabIndex = 16;
            this.numericUpDownCircleStart.ValueChanged += new System.EventHandler(this.numericUpDownCircleStart_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label9.Location = new System.Drawing.Point(3, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 14);
            this.label9.TabIndex = 15;
            this.label9.Text = "Start";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label21.Location = new System.Drawing.Point(8, 22);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(32, 14);
            this.label21.TabIndex = 15;
            this.label21.Text = "Start";
            // 
            // numericUpDownCircleEnd
            // 
            this.numericUpDownCircleEnd.DecimalPlaces = 1;
            this.numericUpDownCircleEnd.Font = new System.Drawing.Font("Tahoma", 9F);
            this.numericUpDownCircleEnd.Location = new System.Drawing.Point(134, 20);
            this.numericUpDownCircleEnd.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.numericUpDownCircleEnd.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownCircleEnd.Name = "numericUpDownCircleEnd";
            this.numericUpDownCircleEnd.Size = new System.Drawing.Size(49, 22);
            this.numericUpDownCircleEnd.TabIndex = 16;
            this.numericUpDownCircleEnd.ValueChanged += new System.EventHandler(this.numericUpDownCircleStart_ValueChanged);
            // 
            // buttonUnmaskAll
            // 
            this.buttonUnmaskAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUnmaskAll.Location = new System.Drawing.Point(79, 423);
            this.buttonUnmaskAll.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonUnmaskAll.Name = "buttonUnmaskAll";
            this.buttonUnmaskAll.Size = new System.Drawing.Size(74, 24);
            this.buttonUnmaskAll.TabIndex = 145;
            this.buttonUnmaskAll.Text = "Unmask All";
            this.buttonUnmaskAll.UseVisualStyleBackColor = true;
            this.buttonUnmaskAll.Click += new System.EventHandler(this.buttonClearMask_Click);
            // 
            // groupBoxRectangle
            // 
            this.groupBoxRectangle.Controls.Add(this.numericUpDownRectangleAngle);
            this.groupBoxRectangle.Controls.Add(this.comboBoxRectangleDirection);
            this.groupBoxRectangle.Controls.Add(this.label22);
            this.groupBoxRectangle.Controls.Add(this.label23);
            this.groupBoxRectangle.Controls.Add(this.numericUpDownRectangleBand);
            this.groupBoxRectangle.Controls.Add(this.label24);
            this.groupBoxRectangle.Controls.Add(this.checkBoxRectangleIsBothSide);
            this.groupBoxRectangle.Enabled = false;
            this.groupBoxRectangle.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxRectangle.Location = new System.Drawing.Point(3, 2);
            this.groupBoxRectangle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxRectangle.Name = "groupBoxRectangle";
            this.groupBoxRectangle.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxRectangle.Size = new System.Drawing.Size(226, 71);
            this.groupBoxRectangle.TabIndex = 159;
            this.groupBoxRectangle.TabStop = false;
            // 
            // numericUpDownRectangleAngle
            // 
            this.numericUpDownRectangleAngle.DecimalPlaces = 1;
            this.numericUpDownRectangleAngle.Font = new System.Drawing.Font("Tahoma", 9F);
            this.numericUpDownRectangleAngle.Location = new System.Drawing.Point(165, 43);
            this.numericUpDownRectangleAngle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.numericUpDownRectangleAngle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownRectangleAngle.Name = "numericUpDownRectangleAngle";
            this.numericUpDownRectangleAngle.Size = new System.Drawing.Size(54, 22);
            this.numericUpDownRectangleAngle.TabIndex = 16;
            this.numericUpDownRectangleAngle.Value = new decimal(new int[] {
            270,
            0,
            0,
            0});
            this.numericUpDownRectangleAngle.ValueChanged += new System.EventHandler(this.numericUpDownRectangleAngle_ValueChanged);
            // 
            // comboBoxRectangleDirection
            // 
            this.comboBoxRectangleDirection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxRectangleDirection.Font = new System.Drawing.Font("Tahoma", 9F);
            this.comboBoxRectangleDirection.IntegralHeight = false;
            this.comboBoxRectangleDirection.Items.AddRange(new object[] {
            "Top",
            "Bottom",
            "Right",
            "Left",
            "Vertical",
            "Horizontal",
            "Free"});
            this.comboBoxRectangleDirection.Location = new System.Drawing.Point(65, 19);
            this.comboBoxRectangleDirection.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.comboBoxRectangleDirection.Name = "comboBoxRectangleDirection";
            this.comboBoxRectangleDirection.Size = new System.Drawing.Size(68, 22);
            this.comboBoxRectangleDirection.TabIndex = 15;
            this.comboBoxRectangleDirection.Text = "Top";
            this.comboBoxRectangleDirection.SelectedIndexChanged += new System.EventHandler(this.comboBoxRectangleDirection_SelectedIndexChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label22.Location = new System.Drawing.Point(129, 46);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(38, 14);
            this.label22.TabIndex = 15;
            this.label22.Text = "Angle";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label23.Location = new System.Drawing.Point(4, 46);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(71, 14);
            this.label23.TabIndex = 15;
            this.label23.Text = "Band Width";
            // 
            // numericUpDownRectangleBand
            // 
            this.numericUpDownRectangleBand.Font = new System.Drawing.Font("Tahoma", 9F);
            this.numericUpDownRectangleBand.Location = new System.Drawing.Point(74, 43);
            this.numericUpDownRectangleBand.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.numericUpDownRectangleBand.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownRectangleBand.Name = "numericUpDownRectangleBand";
            this.numericUpDownRectangleBand.Size = new System.Drawing.Size(47, 22);
            this.numericUpDownRectangleBand.TabIndex = 16;
            this.numericUpDownRectangleBand.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRectangleBand.ValueChanged += new System.EventHandler(this.numericUpDownRectangleBand_ValueChanged);
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label24.Location = new System.Drawing.Point(4, 22);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 21);
            this.label24.TabIndex = 15;
            this.label24.Text = "Direction";
            // 
            // checkBoxRectangleIsBothSide
            // 
            this.checkBoxRectangleIsBothSide.AutoSize = true;
            this.checkBoxRectangleIsBothSide.Font = new System.Drawing.Font("Tahoma", 9F);
            this.checkBoxRectangleIsBothSide.Location = new System.Drawing.Point(138, 21);
            this.checkBoxRectangleIsBothSide.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.checkBoxRectangleIsBothSide.Name = "checkBoxRectangleIsBothSide";
            this.checkBoxRectangleIsBothSide.Size = new System.Drawing.Size(78, 18);
            this.checkBoxRectangleIsBothSide.TabIndex = 13;
            this.checkBoxRectangleIsBothSide.Text = "Both Side";
            this.checkBoxRectangleIsBothSide.UseVisualStyleBackColor = true;
            this.checkBoxRectangleIsBothSide.CheckedChanged += new System.EventHandler(this.checkBoxRectangleIsBothSide_CheckedChanged);
            // 
            // buttonSaveMask
            // 
            this.buttonSaveMask.AutoSize = true;
            this.buttonSaveMask.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSaveMask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveMask.Location = new System.Drawing.Point(154, 423);
            this.buttonSaveMask.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonSaveMask.Name = "buttonSaveMask";
            this.buttonSaveMask.Size = new System.Drawing.Size(73, 24);
            this.buttonSaveMask.TabIndex = 144;
            this.buttonSaveMask.Text = "Save mask";
            this.buttonSaveMask.UseVisualStyleBackColor = true;
            this.buttonSaveMask.Click += new System.EventHandler(this.buttonSaveMask_Click);
            // 
            // buttonApplyMask
            // 
            this.buttonApplyMask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonApplyMask.Location = new System.Drawing.Point(0, 395);
            this.buttonApplyMask.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonApplyMask.Name = "buttonApplyMask";
            this.buttonApplyMask.Size = new System.Drawing.Size(106, 24);
            this.buttonApplyMask.TabIndex = 144;
            this.buttonApplyMask.Text = "Mask";
            this.buttonApplyMask.UseVisualStyleBackColor = true;
            this.buttonApplyMask.Click += new System.EventHandler(this.buttonMask_Click);
            // 
            // groupBoxGeometry
            // 
            this.groupBoxGeometry.Controls.Add(this.label29);
            this.groupBoxGeometry.Controls.Add(this.label28);
            this.groupBoxGeometry.Controls.Add(this.numericBoxTau);
            this.groupBoxGeometry.Controls.Add(this.numericBoxPhi);
            this.groupBoxGeometry.Controls.Add(this.label5);
            this.groupBoxGeometry.Controls.Add(this.label4);
            this.groupBoxGeometry.Enabled = false;
            this.groupBoxGeometry.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.groupBoxGeometry.Location = new System.Drawing.Point(2, 2);
            this.groupBoxGeometry.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxGeometry.Name = "groupBoxGeometry";
            this.groupBoxGeometry.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxGeometry.Size = new System.Drawing.Size(210, 50);
            this.groupBoxGeometry.TabIndex = 183;
            this.groupBoxGeometry.TabStop = false;
            this.groupBoxGeometry.Text = "Geometry";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label29.Location = new System.Drawing.Point(79, 19);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(14, 16);
            this.label29.TabIndex = 189;
            this.label29.Text = "°";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label28.Location = new System.Drawing.Point(175, 21);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(14, 16);
            this.label28.TabIndex = 189;
            this.label28.Text = "°";
            // 
            // numericBoxTau
            // 
                       this.numericBoxTau.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTau.DecimalPlaces = 1;
            this.numericBoxTau.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxTau.Location = new System.Drawing.Point(126, 16);
            this.numericBoxTau.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxTau.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxTau.MinimumSize = new System.Drawing.Size(1, 23);
                       this.numericBoxTau.Name = "numericBoxTau";
            this.numericBoxTau.RadianValue = 0D;
                        
            this.numericBoxTau.ShowPositiveSign = false;
            this.numericBoxTau.Size = new System.Drawing.Size(44, 23);
            this.numericBoxTau.TabIndex = 5;
            this.numericBoxTau.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
                                    // 
            // numericBoxPhi
            // 
                       this.numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.DecimalPlaces = 1;
            this.numericBoxPhi.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxPhi.Location = new System.Drawing.Point(24, 17);
            this.numericBoxPhi.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPhi.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxPhi.MinimumSize = new System.Drawing.Size(1, 23);
                       this.numericBoxPhi.Name = "numericBoxPhi";
            this.numericBoxPhi.RadianValue = 0D;
                        
            this.numericBoxPhi.ShowPositiveSign = false;
            this.numericBoxPhi.Size = new System.Drawing.Size(44, 23);
            this.numericBoxPhi.TabIndex = 5;
            this.numericBoxPhi.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
                                    // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Symbol", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label5.Location = new System.Drawing.Point(2, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "j";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Symbol", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.label4.Location = new System.Drawing.Point(101, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(16, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "t";
            // 
            // trackBarBgH
            // 
            this.trackBarBgH.AutoSize = false;
            this.trackBarBgH.Location = new System.Drawing.Point(21, 42);
            this.trackBarBgH.Maximum = 255;
            this.trackBarBgH.Name = "trackBarBgH";
            this.trackBarBgH.Size = new System.Drawing.Size(195, 16);
            this.trackBarBgH.TabIndex = 181;
            this.trackBarBgH.TickFrequency = 10;
            this.trackBarBgH.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarBgH.Value = 50;
            this.trackBarBgH.Scroll += new System.EventHandler(this.trackBarBg_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 16);
            this.label6.TabIndex = 185;
            this.label6.Text = "H";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 16);
            this.label7.TabIndex = 185;
            this.label7.Text = "R";
            // 
            // trackBarBgR
            // 
            this.trackBarBgR.AutoSize = false;
            this.trackBarBgR.Location = new System.Drawing.Point(21, 60);
            this.trackBarBgR.Maximum = 255;
            this.trackBarBgR.Name = "trackBarBgR";
            this.trackBarBgR.Size = new System.Drawing.Size(195, 16);
            this.trackBarBgR.TabIndex = 181;
            this.trackBarBgR.TickFrequency = 10;
            this.trackBarBgR.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarBgR.Value = 50;
            this.trackBarBgR.Scroll += new System.EventHandler(this.trackBarBg_Scroll);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 16);
            this.label8.TabIndex = 185;
            this.label8.Text = "A";
            // 
            // trackBarBgA
            // 
            this.trackBarBgA.AutoSize = false;
            this.trackBarBgA.Location = new System.Drawing.Point(21, 22);
            this.trackBarBgA.Maximum = 1000;
            this.trackBarBgA.Name = "trackBarBgA";
            this.trackBarBgA.Size = new System.Drawing.Size(195, 16);
            this.trackBarBgA.TabIndex = 181;
            this.trackBarBgA.TickFrequency = 10;
            this.trackBarBgA.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarBgA.Value = 300;
            this.trackBarBgA.Scroll += new System.EventHandler(this.trackBarBg_Scroll);
            // 
            // groupBoxBackground
            // 
            this.groupBoxBackground.Controls.Add(this.label7);
            this.groupBoxBackground.Controls.Add(this.trackBarBgH);
            this.groupBoxBackground.Controls.Add(this.label6);
            this.groupBoxBackground.Controls.Add(this.trackBarBgR);
            this.groupBoxBackground.Controls.Add(this.trackBarBgA);
            this.groupBoxBackground.Controls.Add(this.label8);
            this.groupBoxBackground.Enabled = false;
            this.groupBoxBackground.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxBackground.Location = new System.Drawing.Point(6, 5);
            this.groupBoxBackground.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxBackground.Name = "groupBoxBackground";
            this.groupBoxBackground.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxBackground.Size = new System.Drawing.Size(222, 103);
            this.groupBoxBackground.TabIndex = 159;
            this.groupBoxBackground.TabStop = false;
            // 
            // checkBoxInitialBackground
            // 
            this.checkBoxInitialBackground.AutoSize = true;
            this.checkBoxInitialBackground.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxInitialBackground.Location = new System.Drawing.Point(17, 4);
            this.checkBoxInitialBackground.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxInitialBackground.Name = "checkBoxInitialBackground";
            this.checkBoxInitialBackground.Size = new System.Drawing.Size(144, 18);
            this.checkBoxInitialBackground.TabIndex = 160;
            this.checkBoxInitialBackground.Text = "Set initial background";
            this.checkBoxInitialBackground.UseVisualStyleBackColor = true;
            this.checkBoxInitialBackground.CheckedChanged += new System.EventHandler(this.checkBoxInitialBackground_CheckedChanged);
            // 
            // buttonSaveBackGround
            // 
            this.buttonSaveBackGround.AutoSize = true;
            this.buttonSaveBackGround.Enabled = false;
            this.buttonSaveBackGround.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveBackGround.Location = new System.Drawing.Point(181, 118);
            this.buttonSaveBackGround.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonSaveBackGround.Name = "buttonSaveBackGround";
            this.buttonSaveBackGround.Size = new System.Drawing.Size(50, 28);
            this.buttonSaveBackGround.TabIndex = 144;
            this.buttonSaveBackGround.Text = "Save";
            this.buttonSaveBackGround.UseVisualStyleBackColor = true;
            this.buttonSaveBackGround.Click += new System.EventHandler(this.buttonSaveBackGround_Click);
            // 
            // panelSimulationCheck
            // 
            this.panelSimulationCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSimulationCheck.Controls.Add(this.checkBoxSimulation);
            this.panelSimulationCheck.Controls.Add(this.checkBoxResidual);
            this.panelSimulationCheck.Controls.Add(this.trackBarOpacity);
            this.panelSimulationCheck.Location = new System.Drawing.Point(696, 675);
            this.panelSimulationCheck.Name = "panelSimulationCheck";
            this.panelSimulationCheck.Size = new System.Drawing.Size(277, 23);
            this.panelSimulationCheck.TabIndex = 185;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(679, 3);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(297, 512);
            this.tabControl1.TabIndex = 186;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBoxGeometry);
            this.tabPage1.Location = new System.Drawing.Point(4, 46);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(289, 462);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Detector condition";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numericBoxConvergentAngle);
            this.groupBox5.Controls.Add(this.label46);
            this.groupBox5.Controls.Add(this.label45);
            this.groupBox5.Controls.Add(this.label44);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.waveLengthControl);
            this.groupBox5.Controls.Add(this.numericBoxMonochromaticity);
            this.groupBox5.Location = new System.Drawing.Point(6, 60);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(277, 169);
            this.groupBox5.TabIndex = 185;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Wave source";
            // 
            // numericBoxConvergentAngle
            // 
                       this.numericBoxConvergentAngle.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxConvergentAngle.DecimalPlaces = -1;
            this.numericBoxConvergentAngle.Location = new System.Drawing.Point(129, 114);
            this.numericBoxConvergentAngle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericBoxConvergentAngle.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxConvergentAngle.MinimumSize = new System.Drawing.Size(1, 23);
                       this.numericBoxConvergentAngle.Name = "numericBoxConvergentAngle";
            this.numericBoxConvergentAngle.RadianValue = 0.00034906585039886593D;
                        
            this.numericBoxConvergentAngle.ShowPositiveSign = false;
            this.numericBoxConvergentAngle.Size = new System.Drawing.Size(45, 23);
            this.numericBoxConvergentAngle.TabIndex = 191;
            this.numericBoxConvergentAngle.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            this.numericBoxConvergentAngle.Value = 0.02D;
                        // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label46.Location = new System.Drawing.Point(1, 143);
            this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(103, 14);
            this.label46.TabIndex = 65;
            this.label46.Text = "Monochromaticity";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label45.Location = new System.Drawing.Point(1, 117);
            this.label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(76, 14);
            this.label45.TabIndex = 65;
            this.label45.Text = "Convergence";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label44.Location = new System.Drawing.Point(176, 145);
            this.label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(20, 16);
            this.label44.TabIndex = 67;
            this.label44.Text = "%";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.label12.Location = new System.Drawing.Point(159, 99);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(10, 14);
            this.label12.TabIndex = 67;
            this.label12.Text = "°";
            // 
            // waveLengthControl
            // 
            this.waveLengthControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.waveLengthControl.Energy = 30.000000000000028D;
            this.waveLengthControl.EnergyText = "30";
            this.waveLengthControl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.waveLengthControl.Location = new System.Drawing.Point(2, 17);
            this.waveLengthControl.Margin = new System.Windows.Forms.Padding(0);
            this.waveLengthControl.MinimumSize = new System.Drawing.Size(190, 0);
            this.waveLengthControl.Name = "waveLengthControl";
            this.waveLengthControl.Property = ((Crystallography.WaveProperty)(resources.GetObject("waveLengthControl.Property")));
            this.waveLengthControl.ShowWaveSource = false;
            this.waveLengthControl.Size = new System.Drawing.Size(234, 95);
            this.waveLengthControl.TabIndex = 72;
            this.waveLengthControl.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            this.waveLengthControl.WaveLength = 0.0413280407688684D;
            this.waveLengthControl.WaveLengthText = "0.413280407688684";
            this.waveLengthControl.WaveSource = Crystallography.WaveSource.Xray;
            this.waveLengthControl.XrayWaveSourceElementNumber = 0;
            this.waveLengthControl.XrayWaveSourceLine = Crystallography.XrayLine.Ka1;
            // 
            // numericBoxMonochromaticity
            // 
                       this.numericBoxMonochromaticity.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMonochromaticity.DecimalPlaces = -1;
            this.numericBoxMonochromaticity.Location = new System.Drawing.Point(130, 142);
            this.numericBoxMonochromaticity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericBoxMonochromaticity.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxMonochromaticity.MinimumSize = new System.Drawing.Size(1, 23);
                       this.numericBoxMonochromaticity.Name = "numericBoxMonochromaticity";
            this.numericBoxMonochromaticity.RadianValue = 0D;
                        
            this.numericBoxMonochromaticity.ShowPositiveSign = false;
            this.numericBoxMonochromaticity.Size = new System.Drawing.Size(44, 23);
            this.numericBoxMonochromaticity.TabIndex = 191;
            this.numericBoxMonochromaticity.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
                                    // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numericUpDownImageWidth);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.numericBoxImageResolution);
            this.groupBox4.Controls.Add(this.numericBoxMonitorResolution);
            this.groupBox4.Controls.Add(this.label32);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.numericBoxCameraLength);
            this.groupBox4.Controls.Add(this.label31);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Controls.Add(this.numericUpDownImageHeight);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.numericBoxCenterY);
            this.groupBox4.Controls.Add(this.numericBoxCenterX);
            this.groupBox4.Controls.Add(this.label37);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label33);
            this.groupBox4.Controls.Add(this.label39);
            this.groupBox4.Controls.Add(this.checkBoxFilmBlur);
            this.groupBox4.Controls.Add(this.label40);
            this.groupBox4.Controls.Add(this.numericBoxFilmBlur);
            this.groupBox4.Location = new System.Drawing.Point(3, 230);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(280, 219);
            this.groupBox4.TabIndex = 184;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Detector Property";
            // 
            // numericUpDownImageWidth
            // 
            this.numericUpDownImageWidth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownImageWidth.Location = new System.Drawing.Point(55, 57);
            this.numericUpDownImageWidth.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownImageWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownImageWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageWidth.Name = "numericUpDownImageWidth";
            this.numericUpDownImageWidth.Size = new System.Drawing.Size(57, 23);
            this.numericUpDownImageWidth.TabIndex = 2;
            this.numericUpDownImageWidth.ThousandsSeparator = true;
            this.numericUpDownImageWidth.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(199, 159);
            this.label20.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(57, 14);
            this.label20.TabIndex = 67;
            this.label20.Text = "mm/pixel";
            // 
            // numericBoxImageResolution
            // 
                       this.numericBoxImageResolution.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxImageResolution.DecimalPlaces = 5;
            this.numericBoxImageResolution.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxImageResolution.Location = new System.Drawing.Point(119, 124);
            this.numericBoxImageResolution.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxImageResolution.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxImageResolution.MinimumSize = new System.Drawing.Size(1, 23);
                       this.numericBoxImageResolution.Name = "numericBoxImageResolution";
            this.numericBoxImageResolution.RadianValue = 0.0034906585039886592D;
                        
            this.numericBoxImageResolution.ShowPositiveSign = false;
            this.numericBoxImageResolution.Size = new System.Drawing.Size(77, 23);
            this.numericBoxImageResolution.TabIndex = 5;
            this.numericBoxImageResolution.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            this.numericBoxImageResolution.Value = 0.2D;
                        // 
            // numericBoxMonitorResolution
            // 
                       this.numericBoxMonitorResolution.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMonitorResolution.DecimalPlaces = 5;
            this.numericBoxMonitorResolution.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxMonitorResolution.Location = new System.Drawing.Point(119, 156);
            this.numericBoxMonitorResolution.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxMonitorResolution.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxMonitorResolution.MinimumSize = new System.Drawing.Size(1, 23);
                       this.numericBoxMonitorResolution.Name = "numericBoxMonitorResolution";
            this.numericBoxMonitorResolution.RadianValue = 0.0017453292519943296D;
                        
            this.numericBoxMonitorResolution.ShowPositiveSign = false;
            this.numericBoxMonitorResolution.Size = new System.Drawing.Size(77, 23);
            this.numericBoxMonitorResolution.TabIndex = 5;
            this.numericBoxMonitorResolution.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            this.numericBoxMonitorResolution.Value = 0.1D;
                        this.numericBoxMonitorResolution.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxMonitorResolution_ValueChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(199, 128);
            this.label32.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(57, 14);
            this.label32.TabIndex = 67;
            this.label32.Text = "mm/pixel";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(7, 159);
            this.label19.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(108, 14);
            this.label19.TabIndex = 67;
            this.label19.Text = "Monitor Resolution";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label2.Location = new System.Drawing.Point(7, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 14);
            this.label2.TabIndex = 47;
            this.label2.Text = "Camera Length";
            // 
            // numericBoxCameraLength
            // 
                       this.numericBoxCameraLength.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCameraLength.DecimalPlaces = 5;
            this.numericBoxCameraLength.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxCameraLength.Location = new System.Drawing.Point(116, 20);
            this.numericBoxCameraLength.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCameraLength.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxCameraLength.MinimumSize = new System.Drawing.Size(1, 23);
                       this.numericBoxCameraLength.Name = "numericBoxCameraLength";
            this.numericBoxCameraLength.RadianValue = 5.2359877559829888D;
                        
            this.numericBoxCameraLength.ShowPositiveSign = false;
            this.numericBoxCameraLength.Size = new System.Drawing.Size(93, 23);
            this.numericBoxCameraLength.TabIndex = 5;
            this.numericBoxCameraLength.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            this.numericBoxCameraLength.Value = 300D;
                        // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(7, 128);
            this.label31.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(100, 14);
            this.label31.TabIndex = 67;
            this.label31.Text = "Image Resolution";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(224, 91);
            this.label13.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 14);
            this.label13.TabIndex = 67;
            this.label13.Text = "pixel";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(224, 60);
            this.label30.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(32, 14);
            this.label30.TabIndex = 67;
            this.label30.Text = "pixel";
            // 
            // numericUpDownImageHeight
            // 
            this.numericUpDownImageHeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownImageHeight.Location = new System.Drawing.Point(165, 57);
            this.numericUpDownImageHeight.Margin = new System.Windows.Forms.Padding(0);
            this.numericUpDownImageHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownImageHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageHeight.Name = "numericUpDownImageHeight";
            this.numericUpDownImageHeight.Size = new System.Drawing.Size(57, 23);
            this.numericUpDownImageHeight.TabIndex = 3;
            this.numericUpDownImageHeight.ThousandsSeparator = true;
            this.numericUpDownImageHeight.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(116, 60);
            this.label10.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 14);
            this.label10.TabIndex = 67;
            this.label10.Text = "Height";
            // 
            // numericBoxCenterY
            // 
                       this.numericBoxCenterY.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCenterY.DecimalPlaces = -1;
            this.numericBoxCenterY.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxCenterY.Location = new System.Drawing.Point(165, 87);
            this.numericBoxCenterY.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCenterY.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxCenterY.MinimumSize = new System.Drawing.Size(1, 23);
                       this.numericBoxCenterY.Name = "numericBoxCenterY";
            this.numericBoxCenterY.RadianValue = 6.9813170079773181D;
                        
            this.numericBoxCenterY.ShowPositiveSign = false;
            this.numericBoxCenterY.Size = new System.Drawing.Size(57, 23);
            this.numericBoxCenterY.TabIndex = 5;
            this.numericBoxCenterY.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            this.numericBoxCenterY.Value = 400D;
                        // 
            // numericBoxCenterX
            // 
                       this.numericBoxCenterX.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCenterX.DecimalPlaces = -1;
            this.numericBoxCenterX.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxCenterX.Location = new System.Drawing.Point(55, 87);
            this.numericBoxCenterX.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCenterX.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxCenterX.MinimumSize = new System.Drawing.Size(1, 23);
                       this.numericBoxCenterX.Name = "numericBoxCenterX";
            this.numericBoxCenterX.RadianValue = 6.9813170079773181D;
                        
            this.numericBoxCenterX.ShowPositiveSign = false;
            this.numericBoxCenterX.Size = new System.Drawing.Size(57, 23);
            this.numericBoxCenterX.TabIndex = 4;
            this.numericBoxCenterX.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            this.numericBoxCenterX.Value = 400D;
                        // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(113, 91);
            this.label37.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(52, 14);
            this.label37.TabIndex = 67;
            this.label37.Text = "Center Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 60);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 14);
            this.label3.TabIndex = 67;
            this.label3.Text = "Width";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(3, 91);
            this.label33.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(52, 14);
            this.label33.TabIndex = 67;
            this.label33.Text = "Center X";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label39.Location = new System.Drawing.Point(224, 23);
            this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(23, 13);
            this.label39.TabIndex = 1;
            this.label39.Text = "mm";
            // 
            // checkBoxFilmBlur
            // 
            this.checkBoxFilmBlur.AutoSize = true;
            this.checkBoxFilmBlur.Font = new System.Drawing.Font("Tahoma", 9F);
            this.checkBoxFilmBlur.Location = new System.Drawing.Point(17, 190);
            this.checkBoxFilmBlur.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxFilmBlur.Name = "checkBoxFilmBlur";
            this.checkBoxFilmBlur.Size = new System.Drawing.Size(73, 18);
            this.checkBoxFilmBlur.TabIndex = 7;
            this.checkBoxFilmBlur.Text = "Film Blur";
            this.checkBoxFilmBlur.UseVisualStyleBackColor = true;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label40.Location = new System.Drawing.Point(163, 193);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(21, 13);
            this.label40.TabIndex = 67;
            this.label40.Text = "μm";
            // 
            // numericBoxFilmBlur
            // 
                       this.numericBoxFilmBlur.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFilmBlur.DecimalPlaces = -1;
            this.numericBoxFilmBlur.Location = new System.Drawing.Point(108, 187);
            this.numericBoxFilmBlur.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericBoxFilmBlur.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxFilmBlur.MinimumSize = new System.Drawing.Size(1, 23);
                       this.numericBoxFilmBlur.Name = "numericBoxFilmBlur";
            this.numericBoxFilmBlur.RadianValue = 0D;
                        
            this.numericBoxFilmBlur.ShowPositiveSign = false;
            this.numericBoxFilmBlur.Size = new System.Drawing.Size(45, 23);
            this.numericBoxFilmBlur.TabIndex = 191;
            this.numericBoxFilmBlur.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
                                    // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxMaskDiffractionPeaks);
            this.tabPage2.Controls.Add(this.checkBoxMaskManual);
            this.tabPage2.Controls.Add(this.checkBoxMaskDonut);
            this.tabPage2.Controls.Add(this.groupBoxPeakIndices);
            this.tabPage2.Controls.Add(this.groupBoxManualSpot);
            this.tabPage2.Controls.Add(this.checkBoxMaskRectangle);
            this.tabPage2.Controls.Add(this.groupBoxCircleMask);
            this.tabPage2.Controls.Add(this.buttonUnmask);
            this.tabPage2.Controls.Add(this.buttonApplyMask);
            this.tabPage2.Controls.Add(this.groupBoxRectangle);
            this.tabPage2.Controls.Add(this.buttonMaskAll);
            this.tabPage2.Controls.Add(this.buttonUnmaskAll);
            this.tabPage2.Controls.Add(this.buttonSaveMask);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mask";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxMaskDiffractionPeaks
            // 
            this.checkBoxMaskDiffractionPeaks.AutoSize = true;
            this.checkBoxMaskDiffractionPeaks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMaskDiffractionPeaks.Location = new System.Drawing.Point(9, 208);
            this.checkBoxMaskDiffractionPeaks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxMaskDiffractionPeaks.Name = "checkBoxMaskDiffractionPeaks";
            this.checkBoxMaskDiffractionPeaks.Size = new System.Drawing.Size(111, 18);
            this.checkBoxMaskDiffractionPeaks.TabIndex = 160;
            this.checkBoxMaskDiffractionPeaks.Text = "Diffraction peak";
            this.checkBoxMaskDiffractionPeaks.UseVisualStyleBackColor = true;
            this.checkBoxMaskDiffractionPeaks.CheckedChanged += new System.EventHandler(this.checkBoxDiffractionPeaks_CheckedChanged);
            // 
            // checkBoxMaskManual
            // 
            this.checkBoxMaskManual.AutoSize = true;
            this.checkBoxMaskManual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMaskManual.Location = new System.Drawing.Point(8, 130);
            this.checkBoxMaskManual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBoxMaskManual.Name = "checkBoxMaskManual";
            this.checkBoxMaskManual.Size = new System.Drawing.Size(94, 18);
            this.checkBoxMaskManual.TabIndex = 160;
            this.checkBoxMaskManual.Text = "Manual Mask";
            this.checkBoxMaskManual.UseVisualStyleBackColor = true;
            this.checkBoxMaskManual.CheckedChanged += new System.EventHandler(this.checkBoxManualMask_CheckedChanged);
            // 
            // groupBoxPeakIndices
            // 
            this.groupBoxPeakIndices.Controls.Add(this.checkedListBoxPlaneList);
            this.groupBoxPeakIndices.Controls.Add(this.buttonCheckAllIndices);
            this.groupBoxPeakIndices.Controls.Add(this.buttonUnmaskSelectedPeaks);
            this.groupBoxPeakIndices.Controls.Add(this.buttonUncheckAllIndices);
            this.groupBoxPeakIndices.Enabled = false;
            this.groupBoxPeakIndices.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxPeakIndices.Location = new System.Drawing.Point(3, 208);
            this.groupBoxPeakIndices.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxPeakIndices.Name = "groupBoxPeakIndices";
            this.groupBoxPeakIndices.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxPeakIndices.Size = new System.Drawing.Size(227, 182);
            this.groupBoxPeakIndices.TabIndex = 158;
            this.groupBoxPeakIndices.TabStop = false;
            // 
            // checkedListBoxPlaneList
            // 
            this.checkedListBoxPlaneList.ColumnWidth = 70;
            this.checkedListBoxPlaneList.FormattingEnabled = true;
            this.checkedListBoxPlaneList.HorizontalExtent = 40;
            this.checkedListBoxPlaneList.HorizontalScrollbar = true;
            this.checkedListBoxPlaneList.Location = new System.Drawing.Point(9, 47);
            this.checkedListBoxPlaneList.MultiColumn = true;
            this.checkedListBoxPlaneList.Name = "checkedListBoxPlaneList";
            this.checkedListBoxPlaneList.Size = new System.Drawing.Size(210, 80);
            this.checkedListBoxPlaneList.TabIndex = 162;
            // 
            // buttonCheckAllIndices
            // 
            this.buttonCheckAllIndices.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCheckAllIndices.Location = new System.Drawing.Point(63, 23);
            this.buttonCheckAllIndices.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonCheckAllIndices.Name = "buttonCheckAllIndices";
            this.buttonCheckAllIndices.Size = new System.Drawing.Size(78, 24);
            this.buttonCheckAllIndices.TabIndex = 144;
            this.buttonCheckAllIndices.Text = "Check All";
            this.buttonCheckAllIndices.UseVisualStyleBackColor = true;
            this.buttonCheckAllIndices.Click += new System.EventHandler(this.buttonCheckAllIndices_Click);
            // 
            // buttonUnmaskSelectedPeaks
            // 
            this.buttonUnmaskSelectedPeaks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUnmaskSelectedPeaks.Location = new System.Drawing.Point(11, 154);
            this.buttonUnmaskSelectedPeaks.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonUnmaskSelectedPeaks.Name = "buttonUnmaskSelectedPeaks";
            this.buttonUnmaskSelectedPeaks.Size = new System.Drawing.Size(188, 23);
            this.buttonUnmaskSelectedPeaks.TabIndex = 144;
            this.buttonUnmaskSelectedPeaks.Text = "Select area around the peaks";
            this.buttonUnmaskSelectedPeaks.UseVisualStyleBackColor = true;
            this.buttonUnmaskSelectedPeaks.Click += new System.EventHandler(this.buttonUnmaskSelectedPeaks_Click);
            // 
            // buttonUncheckAllIndices
            // 
            this.buttonUncheckAllIndices.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUncheckAllIndices.Location = new System.Drawing.Point(141, 23);
            this.buttonUncheckAllIndices.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonUncheckAllIndices.Name = "buttonUncheckAllIndices";
            this.buttonUncheckAllIndices.Size = new System.Drawing.Size(80, 24);
            this.buttonUncheckAllIndices.TabIndex = 144;
            this.buttonUncheckAllIndices.Text = "Uncheck All";
            this.buttonUncheckAllIndices.UseVisualStyleBackColor = true;
            this.buttonUncheckAllIndices.Click += new System.EventHandler(this.buttonUncheckAllIndices_Click);
            // 
            // groupBoxManualSpot
            // 
            this.groupBoxManualSpot.Controls.Add(this.radioButtonManualDonut);
            this.groupBoxManualSpot.Controls.Add(this.radioButtonManualCircle);
            this.groupBoxManualSpot.Controls.Add(this.radioButtonManualRectangle);
            this.groupBoxManualSpot.Controls.Add(this.radioButtonManualSpot);
            this.groupBoxManualSpot.Controls.Add(this.comboBoxSpotSize);
            this.groupBoxManualSpot.Controls.Add(this.label11);
            this.groupBoxManualSpot.Controls.Add(this.label17);
            this.groupBoxManualSpot.Enabled = false;
            this.groupBoxManualSpot.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.groupBoxManualSpot.Location = new System.Drawing.Point(3, 130);
            this.groupBoxManualSpot.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxManualSpot.Name = "groupBoxManualSpot";
            this.groupBoxManualSpot.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBoxManualSpot.Size = new System.Drawing.Size(227, 73);
            this.groupBoxManualSpot.TabIndex = 158;
            this.groupBoxManualSpot.TabStop = false;
            // 
            // radioButtonManualDonut
            // 
            this.radioButtonManualDonut.AutoSize = true;
            this.radioButtonManualDonut.Location = new System.Drawing.Point(158, 22);
            this.radioButtonManualDonut.Name = "radioButtonManualDonut";
            this.radioButtonManualDonut.Size = new System.Drawing.Size(59, 20);
            this.radioButtonManualDonut.TabIndex = 18;
            this.radioButtonManualDonut.Text = "Donut";
            this.radioButtonManualDonut.UseVisualStyleBackColor = true;
            // 
            // radioButtonManualCircle
            // 
            this.radioButtonManualCircle.AutoSize = true;
            this.radioButtonManualCircle.Location = new System.Drawing.Point(11, 22);
            this.radioButtonManualCircle.Name = "radioButtonManualCircle";
            this.radioButtonManualCircle.Size = new System.Drawing.Size(58, 20);
            this.radioButtonManualCircle.TabIndex = 18;
            this.radioButtonManualCircle.Text = "Circle";
            this.radioButtonManualCircle.UseVisualStyleBackColor = true;
            // 
            // radioButtonManualRectangle
            // 
            this.radioButtonManualRectangle.AutoSize = true;
            this.radioButtonManualRectangle.Location = new System.Drawing.Point(75, 22);
            this.radioButtonManualRectangle.Name = "radioButtonManualRectangle";
            this.radioButtonManualRectangle.Size = new System.Drawing.Size(82, 20);
            this.radioButtonManualRectangle.TabIndex = 18;
            this.radioButtonManualRectangle.Text = "Rectangle";
            this.radioButtonManualRectangle.UseVisualStyleBackColor = true;
            // 
            // radioButtonManualSpot
            // 
            this.radioButtonManualSpot.AutoSize = true;
            this.radioButtonManualSpot.Checked = true;
            this.radioButtonManualSpot.Location = new System.Drawing.Point(12, 45);
            this.radioButtonManualSpot.Name = "radioButtonManualSpot";
            this.radioButtonManualSpot.Size = new System.Drawing.Size(51, 20);
            this.radioButtonManualSpot.TabIndex = 18;
            this.radioButtonManualSpot.TabStop = true;
            this.radioButtonManualSpot.Text = "Spot";
            this.radioButtonManualSpot.UseVisualStyleBackColor = true;
            // 
            // comboBoxSpotSize
            // 
            this.comboBoxSpotSize.FormattingEnabled = true;
            this.comboBoxSpotSize.Items.AddRange(new object[] {
            "1",
            "2",
            "4",
            "8",
            "16",
            "32",
            "64",
            "128"});
            this.comboBoxSpotSize.Location = new System.Drawing.Point(144, 44);
            this.comboBoxSpotSize.Name = "comboBoxSpotSize";
            this.comboBoxSpotSize.Size = new System.Drawing.Size(45, 24);
            this.comboBoxSpotSize.TabIndex = 17;
            this.comboBoxSpotSize.Text = "8";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label11.Location = new System.Drawing.Point(192, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(32, 14);
            this.label11.TabIndex = 15;
            this.label11.Text = "pixel";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label17.Location = new System.Drawing.Point(71, 48);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 14);
            this.label17.TabIndex = 15;
            this.label17.Text = "Size (radius)";
            // 
            // buttonUnmask
            // 
            this.buttonUnmask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUnmask.Location = new System.Drawing.Point(109, 395);
            this.buttonUnmask.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonUnmask.Name = "buttonUnmask";
            this.buttonUnmask.Size = new System.Drawing.Size(106, 24);
            this.buttonUnmask.TabIndex = 144;
            this.buttonUnmask.Text = "Unmask";
            this.buttonUnmask.UseVisualStyleBackColor = true;
            this.buttonUnmask.Click += new System.EventHandler(this.buttonUnmask_Click);
            // 
            // buttonMaskAll
            // 
            this.buttonMaskAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMaskAll.Location = new System.Drawing.Point(2, 423);
            this.buttonMaskAll.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonMaskAll.Name = "buttonMaskAll";
            this.buttonMaskAll.Size = new System.Drawing.Size(77, 24);
            this.buttonMaskAll.TabIndex = 145;
            this.buttonMaskAll.Text = "Mask All";
            this.buttonMaskAll.UseVisualStyleBackColor = true;
            this.buttonMaskAll.Click += new System.EventHandler(this.buttonMaskAll_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBoxInitialBackground);
            this.tabPage3.Controls.Add(this.groupBoxBackground);
            this.tabPage3.Controls.Add(this.buttonSaveBackGround);
            this.tabPage3.Location = new System.Drawing.Point(4, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(192, 56);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Background";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.textBoxDiffractionInformation);
            this.tabPage4.Location = new System.Drawing.Point(4, 58);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(192, 38);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Diffraction Information";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // textBoxDiffractionInformation
            // 
            this.textBoxDiffractionInformation.Location = new System.Drawing.Point(0, 0);
            this.textBoxDiffractionInformation.Multiline = true;
            this.textBoxDiffractionInformation.Name = "textBoxDiffractionInformation";
            this.textBoxDiffractionInformation.Size = new System.Drawing.Size(270, 450);
            this.textBoxDiffractionInformation.TabIndex = 0;
            // 
            // checkBoxShowMaskedArea
            // 
            this.checkBoxShowMaskedArea.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxShowMaskedArea.AutoSize = true;
            this.checkBoxShowMaskedArea.Location = new System.Drawing.Point(721, 526);
            this.checkBoxShowMaskedArea.Name = "checkBoxShowMaskedArea";
            this.checkBoxShowMaskedArea.Size = new System.Drawing.Size(133, 20);
            this.checkBoxShowMaskedArea.TabIndex = 179;
            this.checkBoxShowMaskedArea.Text = "Show masked area";
            this.checkBoxShowMaskedArea.UseVisualStyleBackColor = true;
            this.checkBoxShowMaskedArea.CheckedChanged += new System.EventHandler(this.checkBoxShowMaskedArea_CheckedChanged);
            // 
            // buttonSaveImage
            // 
            this.buttonSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSaveImage.AutoSize = true;
            this.buttonSaveImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonSaveImage.Location = new System.Drawing.Point(888, 521);
            this.buttonSaveImage.Name = "buttonSaveImage";
            this.buttonSaveImage.Size = new System.Drawing.Size(84, 26);
            this.buttonSaveImage.TabIndex = 187;
            this.buttonSaveImage.Text = "Save Image";
            this.buttonSaveImage.UseVisualStyleBackColor = true;
            this.buttonSaveImage.Click += new System.EventHandler(this.buttonSaveImage_Click);
            // 
            // graphControlFrequency
            // 
            this.graphControlFrequency.AllowMouseOperation = true;
            this.graphControlFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphControlFrequency.BackgroundColor = System.Drawing.Color.White;
            this.graphControlFrequency.BottomMargin = 0F;
            this.graphControlFrequency.DivisionLineColor = System.Drawing.Color.Gray;
            this.graphControlFrequency.DivisionSubLineColor = System.Drawing.Color.LightGray;
            this.graphControlFrequency.DrawingRange = ((Crystallography.RectangleD)(resources.GetObject("graphControlFrequency.DrawingRange")));
            this.graphControlFrequency.FixRangeHorizontal = false;
            this.graphControlFrequency.FixRangeVertical = false;
            this.graphControlFrequency.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F);
            this.graphControlFrequency.GraphName = "";
            this.graphControlFrequency.HorizontalGradiationTextVisivle = true;
            this.graphControlFrequency.IsIntegerX = true;
            this.graphControlFrequency.IsIntegerY = true;
            this.graphControlFrequency.LabelX = "X:";
            this.graphControlFrequency.LabelY = "Y:";
            this.graphControlFrequency.LeftMargin = 0F;
            this.graphControlFrequency.LineColor = System.Drawing.Color.Red;
            this.graphControlFrequency.LineList = new Crystallography.PointD[0];
            this.graphControlFrequency.LineWidth = 1F;
            this.graphControlFrequency.Location = new System.Drawing.Point(679, 591);
            this.graphControlFrequency.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.graphControlFrequency.Mode = Crystallography.Controls.GraphControl.DrawingMode.Line;
            this.graphControlFrequency.MousePositionVisible = true;
            this.graphControlFrequency.Name = "graphControlFrequency";
            this.graphControlFrequency.OriginPosition = new System.Drawing.Point(40, 20);
            this.graphControlFrequency.Peaks = new Crystallography.PeakFunction[0];
            this.graphControlFrequency.Profile = null;
            this.graphControlFrequency.Size = new System.Drawing.Size(297, 80);
            this.graphControlFrequency.Smoothing = false;
            this.graphControlFrequency.TabIndex = 164;
            this.graphControlFrequency.TextFont = new System.Drawing.Font("Arial Unicode MS", 9.75F);
            this.graphControlFrequency.UpperTextVisible = false;
            this.graphControlFrequency.UseLineWidth = true;
            this.graphControlFrequency.VerticalGradiationTextVisivle = true;
            this.graphControlFrequency.XLog = true;
            this.graphControlFrequency.YLog = true;
            this.graphControlFrequency.LinePositionChanged += new Crystallography.Controls.GraphControl.LinePositionChengedEventHandler(this.graphControlFrequency_LinePositionChanged);
            // 
            // scalablePictureBox
            // 
            this.scalablePictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scalablePictureBox.AreaRectangle = ((Crystallography.RectangleD)(resources.GetObject("scalablePictureBox.AreaRectangle")));
            this.scalablePictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.scalablePictureBox.Center = ((Crystallography.PointD)(resources.GetObject("scalablePictureBox.Center")));
            this.scalablePictureBox.FocusEventEnabled = false;
            this.scalablePictureBox.HorizontalFlip = false;
            this.scalablePictureBox.Location = new System.Drawing.Point(0, 0);
            this.scalablePictureBox.ManualSpotMode = false;
            this.scalablePictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.scalablePictureBox.MouseScaling = true;
            this.scalablePictureBox.Name = "scalablePictureBox";
            this.scalablePictureBox.PseudoBitmap = ((Crystallography.PseudoBitmap)(resources.GetObject("scalablePictureBox.PseudoBitmap")));
            this.scalablePictureBox.ShowAreaRectangle = false;
            this.scalablePictureBox.ShowRimRentangle = false;
            this.scalablePictureBox.Size = new System.Drawing.Size(676, 701);
            this.scalablePictureBox.TabIndex = 169;
            this.scalablePictureBox.TextColor = System.Drawing.SystemColors.ControlText;
            this.scalablePictureBox.VerticalFlip = false;
            this.scalablePictureBox.Zoom = 128D;
            this.scalablePictureBox.Paint2 += new System.Windows.Forms.PaintEventHandler(this.scalablePictureBox_Paint2);
            this.scalablePictureBox.MouseMove2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox_MouseMove2);
            this.scalablePictureBox.MouseUp2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox_MouseUp2);
            this.scalablePictureBox.MouseDown2 += new Crystallography.Controls.ScalablePictureBox.MouseEvent(this.scalablePictureBox_MouseDown2);
            // 
            // DiffractionPatternControl
            // 
            this.Controls.Add(this.buttonSaveImage);
            this.Controls.Add(this.numericUpDownMaxInt);
            this.Controls.Add(this.numericUpDownMinInt);
            this.Controls.Add(this.checkBoxShowMaskedArea);
            this.Controls.Add(this.graphControlFrequency);
            this.Controls.Add(this.panelSimulationCheck);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.scalablePictureBox);
            this.Font = new System.Drawing.Font("Arial Unicode MS", 9F);
            this.Name = "DiffractionPatternControl";
            this.Size = new System.Drawing.Size(979, 701);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DiffractionPatternControl_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DiffractionPatternControl_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).EndInit();
            this.groupBoxCircleMask.ResumeLayout(false);
            this.groupBoxCircleMask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleEnd)).EndInit();
            this.groupBoxRectangle.ResumeLayout(false);
            this.groupBoxRectangle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRectangleAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRectangleBand)).EndInit();
            this.groupBoxGeometry.ResumeLayout(false);
            this.groupBoxGeometry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBgH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBgR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBgA)).EndInit();
            this.groupBoxBackground.ResumeLayout(false);
            this.groupBoxBackground.PerformLayout();
            this.panelSimulationCheck.ResumeLayout(false);
            this.panelSimulationCheck.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageHeight)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBoxPeakIndices.ResumeLayout(false);
            this.groupBoxManualSpot.ResumeLayout(false);
            this.groupBoxManualSpot.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label25;
        private Crystallography.Controls.GraphControl graphControlFrequency;
        private System.Windows.Forms.CheckBox checkBoxMaskDonut;
        private System.Windows.Forms.CheckBox checkBoxMaskRectangle;
        public System.Windows.Forms.GroupBox groupBoxCircleMask;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label21;
        public System.Windows.Forms.NumericUpDown numericUpDownCircleStart;
        public System.Windows.Forms.NumericUpDown numericUpDownCircleEnd;
        private System.Windows.Forms.Button buttonUnmaskAll;
        private System.Windows.Forms.Button buttonApplyMask;
        public System.Windows.Forms.GroupBox groupBoxRectangle;
        public System.Windows.Forms.CheckBox checkBoxRectangleIsBothSide;
        public System.Windows.Forms.ComboBox comboBoxRectangleDirection;
        public System.Windows.Forms.Label label22;
        public System.Windows.Forms.Label label23;
        public System.Windows.Forms.NumericUpDown numericUpDownRectangleBand;
        public System.Windows.Forms.NumericUpDown numericUpDownRectangleAngle;
        public System.Windows.Forms.Label label24;
        public System.Windows.Forms.GroupBox groupBoxGeometry;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBarBgH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar trackBarBgR;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBarBgA;
        public System.Windows.Forms.GroupBox groupBoxBackground;
        private System.Windows.Forms.CheckBox checkBoxInitialBackground;
        public System.Windows.Forms.Label label9;
        private Crystallography.Controls.NumericBox numericBoxConvergentAngle;
        private Crystallography.Controls.NumericBox numericBoxFilmBlur;
        private System.Windows.Forms.Button buttonSaveMask;
        private System.Windows.Forms.Button buttonSaveBackGround;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxInt;
        public System.Windows.Forms.NumericUpDown numericUpDownMinInt;
        public Crystallography.Controls.ScalablePictureBox scalablePictureBox;
        public System.Windows.Forms.CheckBox checkBoxSimulation;
        public System.Windows.Forms.CheckBox checkBoxResidual;
        public System.Windows.Forms.TrackBar trackBarOpacity;
        private System.Windows.Forms.Panel panelSimulationCheck;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckedListBox checkedListBoxPlaneList;
        private System.Windows.Forms.Button buttonUnmaskSelectedPeaks;
        public System.Windows.Forms.CheckBox checkBoxShowMaskedArea;
        private System.Windows.Forms.CheckBox checkBoxMaskManual;
        public System.Windows.Forms.GroupBox groupBoxManualSpot;
        private System.Windows.Forms.ComboBox comboBoxSpotSize;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label17;
        private System.Windows.Forms.RadioButton radioButtonManualDonut;
        private System.Windows.Forms.RadioButton radioButtonManualRectangle;
        private System.Windows.Forms.RadioButton radioButtonManualSpot;
        private System.Windows.Forms.Button buttonUnmask;
        private System.Windows.Forms.Button buttonUncheckAllIndices;
        private System.Windows.Forms.Button buttonCheckAllIndices;
        private System.Windows.Forms.RadioButton radioButtonManualCircle;
        private System.Windows.Forms.CheckBox checkBoxMaskDiffractionPeaks;
        public System.Windows.Forms.GroupBox groupBoxPeakIndices;
        private System.Windows.Forms.Button buttonMaskAll;
        private Crystallography.Controls.NumericBox numericBoxMonochromaticity;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox textBoxDiffractionInformation;
        private System.Windows.Forms.Button buttonSaveImage;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownImageWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDownImageHeight;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private Crystallography.Controls.NumericBox numericBoxCenterY;
        public Crystallography.Controls.NumericBox numericBoxCenterX;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.CheckBox checkBoxFilmBlur;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label12;
        public Crystallography.Controls.WaveLengthControl waveLengthControl;
        private System.Windows.Forms.Label label13;
        private Crystallography.Controls.NumericBox numericBoxImageResolution;
        private Crystallography.Controls.NumericBox numericBoxMonitorResolution;
        private Crystallography.Controls.NumericBox numericBoxCameraLength;
        private Crystallography.Controls.NumericBox numericBoxPhi;
        private Crystallography.Controls.NumericBox numericBoxTau;
    }
}
