namespace ReciPro
{
    partial class DiffractionPatternControl
    {
        /// <summary>必要なデザイナー変数です。</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>使用中のリソースをすべてクリーンアップします。</summary>
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
        // (260323Ch) renamed numeric container controls:
        // groupBox4 -> groupBoxDetectorProperty
        // groupBox5 -> groupBoxWaveSource
        private void InitializeComponent()
        {
            numericUpDownMaxInt = new System.Windows.Forms.NumericUpDown();
            label36 = new System.Windows.Forms.Label();
            label25 = new System.Windows.Forms.Label();
            numericUpDownMinInt = new System.Windows.Forms.NumericUpDown();
            checkBoxSimulation = new System.Windows.Forms.CheckBox();
            checkBoxResidual = new System.Windows.Forms.CheckBox();
            trackBarOpacity = new System.Windows.Forms.TrackBar();
            checkBoxMaskDonut = new System.Windows.Forms.CheckBox();
            checkBoxMaskRectangle = new System.Windows.Forms.CheckBox();
            groupBoxCircleMask = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            numericUpDownCircleStart = new System.Windows.Forms.NumericUpDown();
            label9 = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            numericUpDownCircleEnd = new System.Windows.Forms.NumericUpDown();
            buttonUnmaskAll = new System.Windows.Forms.Button();
            groupBoxRectangle = new System.Windows.Forms.GroupBox();
            numericUpDownRectangleAngle = new System.Windows.Forms.NumericUpDown();
            comboBoxRectangleDirection = new System.Windows.Forms.ComboBox();
            label22 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            numericUpDownRectangleBand = new System.Windows.Forms.NumericUpDown();
            label24 = new System.Windows.Forms.Label();
            checkBoxRectangleIsBothSide = new System.Windows.Forms.CheckBox();
            buttonSaveMask = new System.Windows.Forms.Button();
            buttonApplyMask = new System.Windows.Forms.Button();
            groupBoxGeometry = new System.Windows.Forms.GroupBox();
            label29 = new System.Windows.Forms.Label();
            label28 = new System.Windows.Forms.Label();
            numericBoxTau = new NumericBox();
            numericBoxPhi = new NumericBox();
            trackBarBgH = new System.Windows.Forms.TrackBar();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            trackBarBgR = new System.Windows.Forms.TrackBar();
            label8 = new System.Windows.Forms.Label();
            trackBarBgA = new System.Windows.Forms.TrackBar();
            groupBoxBackground = new System.Windows.Forms.GroupBox();
            checkBoxInitialBackground = new System.Windows.Forms.CheckBox();
            buttonSaveBackGround = new System.Windows.Forms.Button();
            panelSimulationCheck = new System.Windows.Forms.Panel();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            groupBoxWaveSource = new System.Windows.Forms.GroupBox();
            numericBoxConvergentAngle = new NumericBox();
            label46 = new System.Windows.Forms.Label();
            label45 = new System.Windows.Forms.Label();
            label44 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            waveLengthControl = new WaveLengthControl();
            numericBoxMonochromaticity = new NumericBox();
            groupBoxDetectorProperty = new System.Windows.Forms.GroupBox();
            numericUpDownImageWidth = new System.Windows.Forms.NumericUpDown();
            label20 = new System.Windows.Forms.Label();
            numericBoxImageResolution = new NumericBox();
            numericBoxMonitorResolution = new NumericBox();
            label32 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            numericBoxCameraLength = new NumericBox();
            label31 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label30 = new System.Windows.Forms.Label();
            numericUpDownImageHeight = new System.Windows.Forms.NumericUpDown();
            label10 = new System.Windows.Forms.Label();
            numericBoxCenterY = new NumericBox();
            numericBoxCenterX = new NumericBox();
            label37 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label33 = new System.Windows.Forms.Label();
            label39 = new System.Windows.Forms.Label();
            checkBoxFilmBlur = new System.Windows.Forms.CheckBox();
            label40 = new System.Windows.Forms.Label();
            numericBoxFilmBlur = new NumericBox();
            tabPage2 = new System.Windows.Forms.TabPage();
            checkBoxMaskDiffractionPeaks = new System.Windows.Forms.CheckBox();
            checkBoxMaskManual = new System.Windows.Forms.CheckBox();
            groupBoxPeakIndices = new System.Windows.Forms.GroupBox();
            checkedListBoxPlaneList = new System.Windows.Forms.CheckedListBox();
            buttonCheckAllIndices = new System.Windows.Forms.Button();
            buttonUnmaskSelectedPeaks = new System.Windows.Forms.Button();
            buttonUncheckAllIndices = new System.Windows.Forms.Button();
            groupBoxManualSpot = new System.Windows.Forms.GroupBox();
            radioButtonManualDonut = new System.Windows.Forms.RadioButton();
            radioButtonManualCircle = new System.Windows.Forms.RadioButton();
            radioButtonManualRectangle = new System.Windows.Forms.RadioButton();
            radioButtonManualSpot = new System.Windows.Forms.RadioButton();
            comboBoxSpotSize = new System.Windows.Forms.ComboBox();
            label11 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            buttonUnmask = new System.Windows.Forms.Button();
            buttonMaskAll = new System.Windows.Forms.Button();
            tabPage3 = new System.Windows.Forms.TabPage();
            tabPage4 = new System.Windows.Forms.TabPage();
            textBoxDiffractionInformation = new System.Windows.Forms.TextBox();
            checkBoxShowMaskedArea = new System.Windows.Forms.CheckBox();
            buttonSaveImage = new System.Windows.Forms.Button();
            graphControlFrequency = new GraphControl();
            scalablePictureBox = new ScalablePictureBox();
            labelLaTex1 = new LabelLaTeX();
            labelLaTex2 = new LabelLaTeX();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMaxInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOpacity).BeginInit();
            groupBoxCircleMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleEnd).BeginInit();
            groupBoxRectangle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRectangleAngle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRectangleBand).BeginInit();
            groupBoxGeometry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarBgH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBgR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBgA).BeginInit();
            groupBoxBackground.SuspendLayout();
            panelSimulationCheck.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBoxWaveSource.SuspendLayout();
            groupBoxDetectorProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImageWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImageHeight).BeginInit();
            tabPage2.SuspendLayout();
            groupBoxPeakIndices.SuspendLayout();
            groupBoxManualSpot.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // numericUpDownMaxInt
            // 
            numericUpDownMaxInt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            numericUpDownMaxInt.Font = new System.Drawing.Font("Segoe UI Variable Text", 9.75F);
            numericUpDownMaxInt.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownMaxInt.Location = new System.Drawing.Point(893, 555);
            numericUpDownMaxInt.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            numericUpDownMaxInt.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numericUpDownMaxInt.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownMaxInt.Name = "numericUpDownMaxInt";
            numericUpDownMaxInt.Size = new System.Drawing.Size(73, 22);
            numericUpDownMaxInt.TabIndex = 167;
            numericUpDownMaxInt.Value = new decimal(new int[] { 65535, 0, 0, 0 });
            numericUpDownMaxInt.ValueChanged += numericUpDownMaxInt_ValueChanged;
            // 
            // label36
            // 
            label36.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label36.AutoSize = true;
            label36.Font = new System.Drawing.Font("Segoe UI Variable Text", 9.75F);
            label36.Location = new System.Drawing.Point(714, 558);
            label36.Name = "label36";
            label36.Size = new System.Drawing.Size(31, 16);
            label36.TabIndex = 166;
            label36.Text = "Min.";
            // 
            // label25
            // 
            label25.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label25.AutoSize = true;
            label25.Font = new System.Drawing.Font("Segoe UI Variable Text", 9.75F);
            label25.Location = new System.Drawing.Point(843, 557);
            label25.Name = "label25";
            label25.Size = new System.Drawing.Size(35, 16);
            label25.TabIndex = 165;
            label25.Text = "Max.";
            // 
            // numericUpDownMinInt
            // 
            numericUpDownMinInt.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            numericUpDownMinInt.Font = new System.Drawing.Font("Segoe UI Variable Text", 9.75F);
            numericUpDownMinInt.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownMinInt.Location = new System.Drawing.Point(757, 555);
            numericUpDownMinInt.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            numericUpDownMinInt.Maximum = new decimal(new int[] { 65534, 0, 0, 0 });
            numericUpDownMinInt.Name = "numericUpDownMinInt";
            numericUpDownMinInt.Size = new System.Drawing.Size(73, 22);
            numericUpDownMinInt.TabIndex = 168;
            numericUpDownMinInt.ValueChanged += numericUpDownMinInt_ValueChanged;
            // 
            // checkBoxSimulation
            // 
            checkBoxSimulation.AutoSize = true;
            checkBoxSimulation.Location = new System.Drawing.Point(0, 3);
            checkBoxSimulation.Name = "checkBoxSimulation";
            checkBoxSimulation.Size = new System.Drawing.Size(85, 19);
            checkBoxSimulation.TabIndex = 179;
            checkBoxSimulation.Text = "Simulation";
            checkBoxSimulation.UseVisualStyleBackColor = true;
            checkBoxSimulation.CheckedChanged += checkBoxSimulation_CheckedChanged;
            // 
            // checkBoxResidual
            // 
            checkBoxResidual.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            checkBoxResidual.AutoSize = true;
            checkBoxResidual.Location = new System.Drawing.Point(202, 3);
            checkBoxResidual.Name = "checkBoxResidual";
            checkBoxResidual.Size = new System.Drawing.Size(75, 19);
            checkBoxResidual.TabIndex = 182;
            checkBoxResidual.Text = "Residual";
            checkBoxResidual.UseVisualStyleBackColor = true;
            checkBoxResidual.CheckedChanged += checkBoxResidual_CheckedChanged;
            // 
            // trackBarOpacity
            // 
            trackBarOpacity.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            trackBarOpacity.AutoSize = false;
            trackBarOpacity.Location = new System.Drawing.Point(83, 3);
            trackBarOpacity.Maximum = 100;
            trackBarOpacity.Name = "trackBarOpacity";
            trackBarOpacity.Size = new System.Drawing.Size(112, 20);
            trackBarOpacity.TabIndex = 181;
            trackBarOpacity.TickFrequency = 10;
            trackBarOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarOpacity.Value = 100;
            trackBarOpacity.Scroll += trackBarOpacity_Scroll;
            // 
            // checkBoxMaskDonut
            // 
            checkBoxMaskDonut.AutoSize = true;
            checkBoxMaskDonut.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            checkBoxMaskDonut.Location = new System.Drawing.Point(8, 77);
            checkBoxMaskDonut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            checkBoxMaskDonut.Name = "checkBoxMaskDonut";
            checkBoxMaskDonut.Size = new System.Drawing.Size(90, 18);
            checkBoxMaskDonut.TabIndex = 160;
            checkBoxMaskDonut.Text = "Donut Mask";
            checkBoxMaskDonut.UseVisualStyleBackColor = true;
            checkBoxMaskDonut.CheckedChanged += checkBoxCircleMask_CheckedChanged;
            // 
            // checkBoxMaskRectangle
            // 
            checkBoxMaskRectangle.AutoSize = true;
            checkBoxMaskRectangle.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            checkBoxMaskRectangle.Location = new System.Drawing.Point(8, 2);
            checkBoxMaskRectangle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            checkBoxMaskRectangle.Name = "checkBoxMaskRectangle";
            checkBoxMaskRectangle.Size = new System.Drawing.Size(110, 18);
            checkBoxMaskRectangle.TabIndex = 161;
            checkBoxMaskRectangle.Text = "Rectangle Mask";
            checkBoxMaskRectangle.UseVisualStyleBackColor = true;
            checkBoxMaskRectangle.CheckedChanged += checkBoxRectangleMask_CheckedChanged;
            // 
            // groupBoxCircleMask
            // 
            groupBoxCircleMask.Controls.Add(label1);
            groupBoxCircleMask.Controls.Add(numericUpDownCircleStart);
            groupBoxCircleMask.Controls.Add(label9);
            groupBoxCircleMask.Controls.Add(label21);
            groupBoxCircleMask.Controls.Add(numericUpDownCircleEnd);
            groupBoxCircleMask.Enabled = false;
            groupBoxCircleMask.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            groupBoxCircleMask.Location = new System.Drawing.Point(3, 77);
            groupBoxCircleMask.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxCircleMask.Name = "groupBoxCircleMask";
            groupBoxCircleMask.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxCircleMask.Size = new System.Drawing.Size(227, 48);
            groupBoxCircleMask.TabIndex = 158;
            groupBoxCircleMask.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Tahoma", 9F);
            label1.Location = new System.Drawing.Point(103, 22);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(28, 14);
            label1.TabIndex = 15;
            label1.Text = "End";
            // 
            // numericUpDownCircleStart
            // 
            numericUpDownCircleStart.DecimalPlaces = 1;
            numericUpDownCircleStart.Font = new System.Drawing.Font("Tahoma", 9F);
            numericUpDownCircleStart.Location = new System.Drawing.Point(43, 19);
            numericUpDownCircleStart.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            numericUpDownCircleStart.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownCircleStart.Name = "numericUpDownCircleStart";
            numericUpDownCircleStart.Size = new System.Drawing.Size(49, 22);
            numericUpDownCircleStart.TabIndex = 16;
            numericUpDownCircleStart.ValueChanged += numericUpDownCircleStart_ValueChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Tahoma", 9F);
            label9.Location = new System.Drawing.Point(3, 22);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(34, 14);
            label9.TabIndex = 15;
            label9.Text = "Start";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new System.Drawing.Font("Tahoma", 9F);
            label21.Location = new System.Drawing.Point(8, 22);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(34, 14);
            label21.TabIndex = 15;
            label21.Text = "Start";
            // 
            // numericUpDownCircleEnd
            // 
            numericUpDownCircleEnd.DecimalPlaces = 1;
            numericUpDownCircleEnd.Font = new System.Drawing.Font("Tahoma", 9F);
            numericUpDownCircleEnd.Location = new System.Drawing.Point(134, 20);
            numericUpDownCircleEnd.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            numericUpDownCircleEnd.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numericUpDownCircleEnd.Name = "numericUpDownCircleEnd";
            numericUpDownCircleEnd.Size = new System.Drawing.Size(49, 22);
            numericUpDownCircleEnd.TabIndex = 16;
            numericUpDownCircleEnd.ValueChanged += numericUpDownCircleStart_ValueChanged;
            // 
            // buttonUnmaskAll
            // 
            buttonUnmaskAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            buttonUnmaskAll.Location = new System.Drawing.Point(79, 423);
            buttonUnmaskAll.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            buttonUnmaskAll.Name = "buttonUnmaskAll";
            buttonUnmaskAll.Size = new System.Drawing.Size(74, 24);
            buttonUnmaskAll.TabIndex = 145;
            buttonUnmaskAll.Text = "Unmask All";
            buttonUnmaskAll.UseVisualStyleBackColor = true;
            buttonUnmaskAll.Click += buttonClearMask_Click;
            // 
            // groupBoxRectangle
            // 
            groupBoxRectangle.Controls.Add(numericUpDownRectangleAngle);
            groupBoxRectangle.Controls.Add(comboBoxRectangleDirection);
            groupBoxRectangle.Controls.Add(label22);
            groupBoxRectangle.Controls.Add(label23);
            groupBoxRectangle.Controls.Add(numericUpDownRectangleBand);
            groupBoxRectangle.Controls.Add(label24);
            groupBoxRectangle.Controls.Add(checkBoxRectangleIsBothSide);
            groupBoxRectangle.Enabled = false;
            groupBoxRectangle.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            groupBoxRectangle.Location = new System.Drawing.Point(3, 2);
            groupBoxRectangle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxRectangle.Name = "groupBoxRectangle";
            groupBoxRectangle.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxRectangle.Size = new System.Drawing.Size(226, 71);
            groupBoxRectangle.TabIndex = 159;
            groupBoxRectangle.TabStop = false;
            // 
            // numericUpDownRectangleAngle
            // 
            numericUpDownRectangleAngle.DecimalPlaces = 1;
            numericUpDownRectangleAngle.Font = new System.Drawing.Font("Tahoma", 9F);
            numericUpDownRectangleAngle.Location = new System.Drawing.Point(165, 43);
            numericUpDownRectangleAngle.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            numericUpDownRectangleAngle.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numericUpDownRectangleAngle.Name = "numericUpDownRectangleAngle";
            numericUpDownRectangleAngle.Size = new System.Drawing.Size(54, 22);
            numericUpDownRectangleAngle.TabIndex = 16;
            numericUpDownRectangleAngle.Value = new decimal(new int[] { 270, 0, 0, 0 });
            numericUpDownRectangleAngle.ValueChanged += numericUpDownRectangleAngle_ValueChanged;
            // 
            // comboBoxRectangleDirection
            // 
            comboBoxRectangleDirection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            comboBoxRectangleDirection.Font = new System.Drawing.Font("Tahoma", 9F);
            comboBoxRectangleDirection.IntegralHeight = false;
            comboBoxRectangleDirection.Items.AddRange(new object[] { "Top", "Bottom", "Right", "Left", "Vertical", "Horizontal", "Free" });
            comboBoxRectangleDirection.Location = new System.Drawing.Point(65, 19);
            comboBoxRectangleDirection.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            comboBoxRectangleDirection.Name = "comboBoxRectangleDirection";
            comboBoxRectangleDirection.Size = new System.Drawing.Size(68, 22);
            comboBoxRectangleDirection.TabIndex = 15;
            comboBoxRectangleDirection.Text = "Top";
            comboBoxRectangleDirection.SelectedIndexChanged += comboBoxRectangleDirection_SelectedIndexChanged;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new System.Drawing.Font("Tahoma", 9F);
            label22.Location = new System.Drawing.Point(129, 46);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(38, 14);
            label22.TabIndex = 15;
            label22.Text = "Angle";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new System.Drawing.Font("Tahoma", 9F);
            label23.Location = new System.Drawing.Point(4, 46);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(71, 14);
            label23.TabIndex = 15;
            label23.Text = "Band Width";
            // 
            // numericUpDownRectangleBand
            // 
            numericUpDownRectangleBand.Font = new System.Drawing.Font("Tahoma", 9F);
            numericUpDownRectangleBand.Location = new System.Drawing.Point(74, 43);
            numericUpDownRectangleBand.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            numericUpDownRectangleBand.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownRectangleBand.Name = "numericUpDownRectangleBand";
            numericUpDownRectangleBand.Size = new System.Drawing.Size(47, 22);
            numericUpDownRectangleBand.TabIndex = 16;
            numericUpDownRectangleBand.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownRectangleBand.ValueChanged += numericUpDownRectangleBand_ValueChanged;
            // 
            // label24
            // 
            label24.Font = new System.Drawing.Font("Tahoma", 9F);
            label24.Location = new System.Drawing.Point(4, 22);
            label24.Name = "label24";
            label24.Size = new System.Drawing.Size(65, 21);
            label24.TabIndex = 15;
            label24.Text = "Direction";
            // 
            // checkBoxRectangleIsBothSide
            // 
            checkBoxRectangleIsBothSide.AutoSize = true;
            checkBoxRectangleIsBothSide.Font = new System.Drawing.Font("Tahoma", 9F);
            checkBoxRectangleIsBothSide.Location = new System.Drawing.Point(138, 21);
            checkBoxRectangleIsBothSide.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            checkBoxRectangleIsBothSide.Name = "checkBoxRectangleIsBothSide";
            checkBoxRectangleIsBothSide.Size = new System.Drawing.Size(79, 18);
            checkBoxRectangleIsBothSide.TabIndex = 13;
            checkBoxRectangleIsBothSide.Text = "Both Side";
            checkBoxRectangleIsBothSide.UseVisualStyleBackColor = true;
            checkBoxRectangleIsBothSide.CheckedChanged += checkBoxRectangleIsBothSide_CheckedChanged;
            // 
            // buttonSaveMask
            // 
            buttonSaveMask.AutoSize = true;
            buttonSaveMask.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonSaveMask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            buttonSaveMask.Location = new System.Drawing.Point(154, 423);
            buttonSaveMask.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            buttonSaveMask.Name = "buttonSaveMask";
            buttonSaveMask.Size = new System.Drawing.Size(74, 24);
            buttonSaveMask.TabIndex = 144;
            buttonSaveMask.Text = "Save mask";
            buttonSaveMask.UseVisualStyleBackColor = true;
            buttonSaveMask.Click += buttonSaveMask_Click;
            // 
            // buttonApplyMask
            // 
            buttonApplyMask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            buttonApplyMask.Location = new System.Drawing.Point(0, 395);
            buttonApplyMask.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            buttonApplyMask.Name = "buttonApplyMask";
            buttonApplyMask.Size = new System.Drawing.Size(106, 24);
            buttonApplyMask.TabIndex = 144;
            buttonApplyMask.Text = "Mask";
            buttonApplyMask.UseVisualStyleBackColor = true;
            buttonApplyMask.Click += buttonMask_Click;
            // 
            // groupBoxGeometry
            // 
            groupBoxGeometry.Controls.Add(label29);
            groupBoxGeometry.Controls.Add(label28);
            groupBoxGeometry.Controls.Add(numericBoxTau);
            groupBoxGeometry.Controls.Add(numericBoxPhi);
            groupBoxGeometry.Controls.Add(labelLaTex2);
            groupBoxGeometry.Controls.Add(labelLaTex1);
            groupBoxGeometry.Enabled = false;
            groupBoxGeometry.Font = new System.Drawing.Font("Tahoma", 9.75F);
            groupBoxGeometry.Location = new System.Drawing.Point(2, 2);
            groupBoxGeometry.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxGeometry.Name = "groupBoxGeometry";
            groupBoxGeometry.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxGeometry.Size = new System.Drawing.Size(210, 50);
            groupBoxGeometry.TabIndex = 183;
            groupBoxGeometry.TabStop = false;
            groupBoxGeometry.Text = "Geometry";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new System.Drawing.Font("Tahoma", 9.75F);
            label29.Location = new System.Drawing.Point(79, 19);
            label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label29.Name = "label29";
            label29.Size = new System.Drawing.Size(13, 16);
            label29.TabIndex = 189;
            label29.Text = "°";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new System.Drawing.Font("Tahoma", 9.75F);
            label28.Location = new System.Drawing.Point(175, 21);
            label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label28.Name = "label28";
            label28.Size = new System.Drawing.Size(13, 16);
            label28.TabIndex = 189;
            label28.Text = "°";
            // 
            // numericBoxTau
            // 
            numericBoxTau.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTau.DecimalPlaces = 1;
            numericBoxTau.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTau.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTau.Location = new System.Drawing.Point(126, 16);
            numericBoxTau.Margin = new System.Windows.Forms.Padding(0);
            numericBoxTau.MaximumSize = new System.Drawing.Size(1000, 23);
            numericBoxTau.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxTau.Name = "numericBoxTau";
            numericBoxTau.Size = new System.Drawing.Size(44, 23);
            numericBoxTau.TabIndex = 5;
            numericBoxTau.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            // 
            // numericBoxPhi
            // 
            numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.DecimalPlaces = 1;
            numericBoxPhi.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPhi.Location = new System.Drawing.Point(24, 17);
            numericBoxPhi.Margin = new System.Windows.Forms.Padding(0);
            numericBoxPhi.MaximumSize = new System.Drawing.Size(1000, 23);
            numericBoxPhi.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxPhi.Name = "numericBoxPhi";
            numericBoxPhi.Size = new System.Drawing.Size(44, 23);
            numericBoxPhi.TabIndex = 5;
            numericBoxPhi.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            // 
            // trackBarBgH
            // 
            trackBarBgH.AutoSize = false;
            trackBarBgH.Location = new System.Drawing.Point(21, 42);
            trackBarBgH.Maximum = 255;
            trackBarBgH.Name = "trackBarBgH";
            trackBarBgH.Size = new System.Drawing.Size(195, 16);
            trackBarBgH.TabIndex = 181;
            trackBarBgH.TickFrequency = 10;
            trackBarBgH.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarBgH.Value = 50;
            trackBarBgH.Scroll += trackBarBg_Scroll;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(6, 41);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(16, 15);
            label6.TabIndex = 185;
            label6.Text = "H";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(6, 61);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(16, 15);
            label7.TabIndex = 185;
            label7.Text = "R";
            // 
            // trackBarBgR
            // 
            trackBarBgR.AutoSize = false;
            trackBarBgR.Location = new System.Drawing.Point(21, 60);
            trackBarBgR.Maximum = 255;
            trackBarBgR.Name = "trackBarBgR";
            trackBarBgR.Size = new System.Drawing.Size(195, 16);
            trackBarBgR.TabIndex = 181;
            trackBarBgR.TickFrequency = 10;
            trackBarBgR.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarBgR.Value = 50;
            trackBarBgR.Scroll += trackBarBg_Scroll;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(6, 20);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(14, 15);
            label8.TabIndex = 185;
            label8.Text = "A";
            // 
            // trackBarBgA
            // 
            trackBarBgA.AutoSize = false;
            trackBarBgA.Location = new System.Drawing.Point(21, 22);
            trackBarBgA.Maximum = 1000;
            trackBarBgA.Name = "trackBarBgA";
            trackBarBgA.Size = new System.Drawing.Size(195, 16);
            trackBarBgA.TabIndex = 181;
            trackBarBgA.TickFrequency = 10;
            trackBarBgA.TickStyle = System.Windows.Forms.TickStyle.None;
            trackBarBgA.Value = 300;
            trackBarBgA.Scroll += trackBarBg_Scroll;
            // 
            // groupBoxBackground
            // 
            groupBoxBackground.Controls.Add(label7);
            groupBoxBackground.Controls.Add(trackBarBgH);
            groupBoxBackground.Controls.Add(label6);
            groupBoxBackground.Controls.Add(trackBarBgR);
            groupBoxBackground.Controls.Add(trackBarBgA);
            groupBoxBackground.Controls.Add(label8);
            groupBoxBackground.Enabled = false;
            groupBoxBackground.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            groupBoxBackground.Location = new System.Drawing.Point(6, 5);
            groupBoxBackground.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxBackground.Name = "groupBoxBackground";
            groupBoxBackground.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxBackground.Size = new System.Drawing.Size(222, 103);
            groupBoxBackground.TabIndex = 159;
            groupBoxBackground.TabStop = false;
            // 
            // checkBoxInitialBackground
            // 
            checkBoxInitialBackground.AutoSize = true;
            checkBoxInitialBackground.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            checkBoxInitialBackground.Location = new System.Drawing.Point(17, 4);
            checkBoxInitialBackground.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            checkBoxInitialBackground.Name = "checkBoxInitialBackground";
            checkBoxInitialBackground.Size = new System.Drawing.Size(143, 18);
            checkBoxInitialBackground.TabIndex = 160;
            checkBoxInitialBackground.Text = "Set initial background";
            checkBoxInitialBackground.UseVisualStyleBackColor = true;
            checkBoxInitialBackground.CheckedChanged += checkBoxInitialBackground_CheckedChanged;
            // 
            // buttonSaveBackGround
            // 
            buttonSaveBackGround.AutoSize = true;
            buttonSaveBackGround.Enabled = false;
            buttonSaveBackGround.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            buttonSaveBackGround.Location = new System.Drawing.Point(181, 118);
            buttonSaveBackGround.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            buttonSaveBackGround.Name = "buttonSaveBackGround";
            buttonSaveBackGround.Size = new System.Drawing.Size(50, 28);
            buttonSaveBackGround.TabIndex = 144;
            buttonSaveBackGround.Text = "Save";
            buttonSaveBackGround.UseVisualStyleBackColor = true;
            buttonSaveBackGround.Click += buttonSaveBackGround_Click;
            // 
            // panelSimulationCheck
            // 
            panelSimulationCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            panelSimulationCheck.Controls.Add(checkBoxSimulation);
            panelSimulationCheck.Controls.Add(checkBoxResidual);
            panelSimulationCheck.Controls.Add(trackBarOpacity);
            panelSimulationCheck.Location = new System.Drawing.Point(696, 675);
            panelSimulationCheck.Name = "panelSimulationCheck";
            panelSimulationCheck.Size = new System.Drawing.Size(277, 23);
            panelSimulationCheck.TabIndex = 185;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new System.Drawing.Point(679, 3);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(297, 512);
            tabControl1.TabIndex = 186;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBoxWaveSource);
            tabPage1.Controls.Add(groupBoxDetectorProperty);
            tabPage1.Controls.Add(groupBoxGeometry);
            tabPage1.Location = new System.Drawing.Point(4, 44);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(289, 464);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Detector condition";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBoxWaveSource
            // 
            groupBoxWaveSource.Controls.Add(numericBoxConvergentAngle);
            groupBoxWaveSource.Controls.Add(label46);
            groupBoxWaveSource.Controls.Add(label45);
            groupBoxWaveSource.Controls.Add(label44);
            groupBoxWaveSource.Controls.Add(label12);
            groupBoxWaveSource.Controls.Add(waveLengthControl);
            groupBoxWaveSource.Controls.Add(numericBoxMonochromaticity);
            groupBoxWaveSource.Location = new System.Drawing.Point(6, 60);
            groupBoxWaveSource.Name = "groupBoxWaveSource";
            groupBoxWaveSource.Size = new System.Drawing.Size(277, 169);
            groupBoxWaveSource.TabIndex = 185;
            groupBoxWaveSource.TabStop = false;
            groupBoxWaveSource.Text = "Wave source";
            // 
            // numericBoxConvergentAngle
            // 
            numericBoxConvergentAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxConvergentAngle.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxConvergentAngle.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxConvergentAngle.Location = new System.Drawing.Point(129, 114);
            numericBoxConvergentAngle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            numericBoxConvergentAngle.MaximumSize = new System.Drawing.Size(1000, 23);
            numericBoxConvergentAngle.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxConvergentAngle.Name = "numericBoxConvergentAngle";
            numericBoxConvergentAngle.RadianValue = 0.00034906585039886593D;
            numericBoxConvergentAngle.Size = new System.Drawing.Size(45, 23);
            numericBoxConvergentAngle.TabIndex = 191;
            numericBoxConvergentAngle.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            numericBoxConvergentAngle.Value = 0.02D;
            // 
            // label46
            // 
            label46.AutoSize = true;
            label46.Font = new System.Drawing.Font("Tahoma", 9F);
            label46.Location = new System.Drawing.Point(1, 143);
            label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label46.Name = "label46";
            label46.Size = new System.Drawing.Size(103, 14);
            label46.TabIndex = 65;
            label46.Text = "Monochromaticity";
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Font = new System.Drawing.Font("Tahoma", 9F);
            label45.Location = new System.Drawing.Point(1, 117);
            label45.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label45.Name = "label45";
            label45.Size = new System.Drawing.Size(79, 14);
            label45.TabIndex = 65;
            label45.Text = "Convergence";
            // 
            // label44
            // 
            label44.AutoSize = true;
            label44.Font = new System.Drawing.Font("Tahoma", 9.75F);
            label44.Location = new System.Drawing.Point(176, 145);
            label44.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label44.Name = "label44";
            label44.Size = new System.Drawing.Size(19, 16);
            label44.TabIndex = 67;
            label44.Text = "%";
            // 
            // label12
            // 
            label12.Font = new System.Drawing.Font("Tahoma", 9.75F);
            label12.Location = new System.Drawing.Point(159, 99);
            label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(10, 14);
            label12.TabIndex = 67;
            label12.Text = "°";
            // 
            // waveLengthControl
            // 
            waveLengthControl.AutoSize = true;
            waveLengthControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            waveLengthControl.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl.Energy = 30D;
            waveLengthControl.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            waveLengthControl.Location = new System.Drawing.Point(2, 17);
            waveLengthControl.Margin = new System.Windows.Forms.Padding(0);
            waveLengthControl.MaximumSize = new System.Drawing.Size(500, 500);
            waveLengthControl.MinimumSize = new System.Drawing.Size(190, 0);
            waveLengthControl.Monochrome = true;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = false;
            waveLengthControl.Size = new System.Drawing.Size(190, 80);
            waveLengthControl.TabIndex = 72;
            waveLengthControl.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            waveLengthControl.WaveLength = 0.041328040768899996D;
            waveLengthControl.WaveSource = WaveSource.Xray;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            waveLengthControl.XrayWaveSourceLine = XrayLine.Ka1;
            // 
            // numericBoxMonochromaticity
            // 
            numericBoxMonochromaticity.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMonochromaticity.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxMonochromaticity.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxMonochromaticity.Location = new System.Drawing.Point(130, 142);
            numericBoxMonochromaticity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            numericBoxMonochromaticity.MaximumSize = new System.Drawing.Size(1000, 23);
            numericBoxMonochromaticity.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxMonochromaticity.Name = "numericBoxMonochromaticity";
            numericBoxMonochromaticity.Size = new System.Drawing.Size(44, 23);
            numericBoxMonochromaticity.TabIndex = 191;
            numericBoxMonochromaticity.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            // 
            // groupBoxDetectorProperty
            // 
            groupBoxDetectorProperty.Controls.Add(numericUpDownImageWidth);
            groupBoxDetectorProperty.Controls.Add(label20);
            groupBoxDetectorProperty.Controls.Add(numericBoxImageResolution);
            groupBoxDetectorProperty.Controls.Add(numericBoxMonitorResolution);
            groupBoxDetectorProperty.Controls.Add(label32);
            groupBoxDetectorProperty.Controls.Add(label19);
            groupBoxDetectorProperty.Controls.Add(label2);
            groupBoxDetectorProperty.Controls.Add(numericBoxCameraLength);
            groupBoxDetectorProperty.Controls.Add(label31);
            groupBoxDetectorProperty.Controls.Add(label13);
            groupBoxDetectorProperty.Controls.Add(label30);
            groupBoxDetectorProperty.Controls.Add(numericUpDownImageHeight);
            groupBoxDetectorProperty.Controls.Add(label10);
            groupBoxDetectorProperty.Controls.Add(numericBoxCenterY);
            groupBoxDetectorProperty.Controls.Add(numericBoxCenterX);
            groupBoxDetectorProperty.Controls.Add(label37);
            groupBoxDetectorProperty.Controls.Add(label3);
            groupBoxDetectorProperty.Controls.Add(label33);
            groupBoxDetectorProperty.Controls.Add(label39);
            groupBoxDetectorProperty.Controls.Add(checkBoxFilmBlur);
            groupBoxDetectorProperty.Controls.Add(label40);
            groupBoxDetectorProperty.Controls.Add(numericBoxFilmBlur);
            groupBoxDetectorProperty.Location = new System.Drawing.Point(3, 230);
            groupBoxDetectorProperty.Name = "groupBoxDetectorProperty";
            groupBoxDetectorProperty.Size = new System.Drawing.Size(280, 219);
            groupBoxDetectorProperty.TabIndex = 184;
            groupBoxDetectorProperty.TabStop = false;
            groupBoxDetectorProperty.Text = "Detector Property";
            // 
            // numericUpDownImageWidth
            // 
            numericUpDownImageWidth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            numericUpDownImageWidth.Location = new System.Drawing.Point(55, 57);
            numericUpDownImageWidth.Margin = new System.Windows.Forms.Padding(0);
            numericUpDownImageWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownImageWidth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownImageWidth.Name = "numericUpDownImageWidth";
            numericUpDownImageWidth.Size = new System.Drawing.Size(57, 23);
            numericUpDownImageWidth.TabIndex = 2;
            numericUpDownImageWidth.ThousandsSeparator = true;
            numericUpDownImageWidth.Value = new decimal(new int[] { 800, 0, 0, 0 });
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label20.Location = new System.Drawing.Point(199, 159);
            label20.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(56, 14);
            label20.TabIndex = 67;
            label20.Text = "mm/pixel";
            // 
            // numericBoxImageResolution
            // 
            numericBoxImageResolution.BackColor = System.Drawing.SystemColors.Control;
            numericBoxImageResolution.DecimalPlaces = 5;
            numericBoxImageResolution.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxImageResolution.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxImageResolution.Location = new System.Drawing.Point(119, 124);
            numericBoxImageResolution.Margin = new System.Windows.Forms.Padding(0);
            numericBoxImageResolution.MaximumSize = new System.Drawing.Size(1000, 23);
            numericBoxImageResolution.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxImageResolution.Name = "numericBoxImageResolution";
            numericBoxImageResolution.RadianValue = 0.0034906585039886592D;
            numericBoxImageResolution.Size = new System.Drawing.Size(77, 23);
            numericBoxImageResolution.TabIndex = 5;
            numericBoxImageResolution.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            numericBoxImageResolution.Value = 0.2D;
            // 
            // numericBoxMonitorResolution
            // 
            numericBoxMonitorResolution.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMonitorResolution.DecimalPlaces = 5;
            numericBoxMonitorResolution.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxMonitorResolution.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxMonitorResolution.Location = new System.Drawing.Point(119, 156);
            numericBoxMonitorResolution.Margin = new System.Windows.Forms.Padding(0);
            numericBoxMonitorResolution.MaximumSize = new System.Drawing.Size(1000, 23);
            numericBoxMonitorResolution.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxMonitorResolution.Name = "numericBoxMonitorResolution";
            numericBoxMonitorResolution.RadianValue = 0.0017453292519943296D;
            numericBoxMonitorResolution.Size = new System.Drawing.Size(77, 23);
            numericBoxMonitorResolution.TabIndex = 5;
            numericBoxMonitorResolution.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            numericBoxMonitorResolution.Value = 0.1D;
            numericBoxMonitorResolution.ValueChanged += numericBoxMonitorResolution_ValueChanged;
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label32.Location = new System.Drawing.Point(199, 128);
            label32.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label32.Name = "label32";
            label32.Size = new System.Drawing.Size(56, 14);
            label32.TabIndex = 67;
            label32.Text = "mm/pixel";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label19.Location = new System.Drawing.Point(7, 159);
            label19.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(108, 14);
            label19.TabIndex = 67;
            label19.Text = "Monitor Resolution";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Tahoma", 9F);
            label2.Location = new System.Drawing.Point(7, 22);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(90, 14);
            label2.TabIndex = 47;
            label2.Text = "Camera Length";
            // 
            // numericBoxCameraLength
            // 
            numericBoxCameraLength.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength.DecimalPlaces = 5;
            numericBoxCameraLength.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCameraLength.Location = new System.Drawing.Point(116, 20);
            numericBoxCameraLength.Margin = new System.Windows.Forms.Padding(0);
            numericBoxCameraLength.MaximumSize = new System.Drawing.Size(1000, 23);
            numericBoxCameraLength.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxCameraLength.Name = "numericBoxCameraLength";
            numericBoxCameraLength.RadianValue = 5.2359877559829888D;
            numericBoxCameraLength.Size = new System.Drawing.Size(93, 23);
            numericBoxCameraLength.TabIndex = 5;
            numericBoxCameraLength.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            numericBoxCameraLength.Value = 300D;
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label31.Location = new System.Drawing.Point(7, 128);
            label31.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label31.Name = "label31";
            label31.Size = new System.Drawing.Size(101, 14);
            label31.TabIndex = 67;
            label31.Text = "Image Resolution";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label13.Location = new System.Drawing.Point(224, 91);
            label13.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(31, 14);
            label13.TabIndex = 67;
            label13.Text = "pixel";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label30.Location = new System.Drawing.Point(224, 60);
            label30.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label30.Name = "label30";
            label30.Size = new System.Drawing.Size(31, 14);
            label30.TabIndex = 67;
            label30.Text = "pixel";
            // 
            // numericUpDownImageHeight
            // 
            numericUpDownImageHeight.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            numericUpDownImageHeight.Location = new System.Drawing.Point(165, 57);
            numericUpDownImageHeight.Margin = new System.Windows.Forms.Padding(0);
            numericUpDownImageHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownImageHeight.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownImageHeight.Name = "numericUpDownImageHeight";
            numericUpDownImageHeight.Size = new System.Drawing.Size(57, 23);
            numericUpDownImageHeight.TabIndex = 3;
            numericUpDownImageHeight.ThousandsSeparator = true;
            numericUpDownImageHeight.Value = new decimal(new int[] { 800, 0, 0, 0 });
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label10.Location = new System.Drawing.Point(116, 60);
            label10.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(43, 14);
            label10.TabIndex = 67;
            label10.Text = "Height";
            // 
            // numericBoxCenterY
            // 
            numericBoxCenterY.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCenterY.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCenterY.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCenterY.Location = new System.Drawing.Point(165, 87);
            numericBoxCenterY.Margin = new System.Windows.Forms.Padding(0);
            numericBoxCenterY.MaximumSize = new System.Drawing.Size(1000, 23);
            numericBoxCenterY.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxCenterY.Name = "numericBoxCenterY";
            numericBoxCenterY.RadianValue = 6.9813170079773181D;
            numericBoxCenterY.Size = new System.Drawing.Size(57, 23);
            numericBoxCenterY.TabIndex = 5;
            numericBoxCenterY.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            numericBoxCenterY.Value = 400D;
            // 
            // numericBoxCenterX
            // 
            numericBoxCenterX.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCenterX.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCenterX.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCenterX.Location = new System.Drawing.Point(55, 87);
            numericBoxCenterX.Margin = new System.Windows.Forms.Padding(0);
            numericBoxCenterX.MaximumSize = new System.Drawing.Size(1000, 23);
            numericBoxCenterX.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxCenterX.Name = "numericBoxCenterX";
            numericBoxCenterX.RadianValue = 6.9813170079773181D;
            numericBoxCenterX.Size = new System.Drawing.Size(57, 23);
            numericBoxCenterX.TabIndex = 4;
            numericBoxCenterX.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            numericBoxCenterX.Value = 400D;
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label37.Location = new System.Drawing.Point(113, 91);
            label37.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label37.Name = "label37";
            label37.Size = new System.Drawing.Size(56, 14);
            label37.TabIndex = 67;
            label37.Text = "Center Y";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label3.Location = new System.Drawing.Point(15, 60);
            label3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(40, 14);
            label3.TabIndex = 67;
            label3.Text = "Width";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label33.Location = new System.Drawing.Point(3, 91);
            label33.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            label33.Name = "label33";
            label33.Size = new System.Drawing.Size(55, 14);
            label33.TabIndex = 67;
            label33.Text = "Center X";
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Font = new System.Drawing.Font("Tahoma", 8F);
            label39.Location = new System.Drawing.Point(224, 23);
            label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label39.Name = "label39";
            label39.Size = new System.Drawing.Size(23, 13);
            label39.TabIndex = 1;
            label39.Text = "mm";
            // 
            // checkBoxFilmBlur
            // 
            checkBoxFilmBlur.AutoSize = true;
            checkBoxFilmBlur.Font = new System.Drawing.Font("Tahoma", 9F);
            checkBoxFilmBlur.Location = new System.Drawing.Point(17, 190);
            checkBoxFilmBlur.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            checkBoxFilmBlur.Name = "checkBoxFilmBlur";
            checkBoxFilmBlur.Size = new System.Drawing.Size(70, 18);
            checkBoxFilmBlur.TabIndex = 7;
            checkBoxFilmBlur.Text = "Film Blur";
            checkBoxFilmBlur.UseVisualStyleBackColor = true;
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Font = new System.Drawing.Font("Tahoma", 8F);
            label40.Location = new System.Drawing.Point(163, 193);
            label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label40.Name = "label40";
            label40.Size = new System.Drawing.Size(21, 13);
            label40.TabIndex = 67;
            label40.Text = "μm";
            // 
            // numericBoxFilmBlur
            // 
            numericBoxFilmBlur.BackColor = System.Drawing.SystemColors.Control;
            numericBoxFilmBlur.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxFilmBlur.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxFilmBlur.Location = new System.Drawing.Point(108, 187);
            numericBoxFilmBlur.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            numericBoxFilmBlur.MaximumSize = new System.Drawing.Size(1000, 23);
            numericBoxFilmBlur.MinimumSize = new System.Drawing.Size(1, 23);
            numericBoxFilmBlur.Name = "numericBoxFilmBlur";
            numericBoxFilmBlur.Size = new System.Drawing.Size(45, 23);
            numericBoxFilmBlur.TabIndex = 191;
            numericBoxFilmBlur.TextFont = new System.Drawing.Font("Tahoma", 9.75F);
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(checkBoxMaskDiffractionPeaks);
            tabPage2.Controls.Add(checkBoxMaskManual);
            tabPage2.Controls.Add(checkBoxMaskDonut);
            tabPage2.Controls.Add(groupBoxPeakIndices);
            tabPage2.Controls.Add(groupBoxManualSpot);
            tabPage2.Controls.Add(checkBoxMaskRectangle);
            tabPage2.Controls.Add(groupBoxCircleMask);
            tabPage2.Controls.Add(buttonUnmask);
            tabPage2.Controls.Add(buttonApplyMask);
            tabPage2.Controls.Add(groupBoxRectangle);
            tabPage2.Controls.Add(buttonMaskAll);
            tabPage2.Controls.Add(buttonUnmaskAll);
            tabPage2.Controls.Add(buttonSaveMask);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(192, 72);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Mask";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxMaskDiffractionPeaks
            // 
            checkBoxMaskDiffractionPeaks.AutoSize = true;
            checkBoxMaskDiffractionPeaks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            checkBoxMaskDiffractionPeaks.Location = new System.Drawing.Point(9, 208);
            checkBoxMaskDiffractionPeaks.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            checkBoxMaskDiffractionPeaks.Name = "checkBoxMaskDiffractionPeaks";
            checkBoxMaskDiffractionPeaks.Size = new System.Drawing.Size(111, 18);
            checkBoxMaskDiffractionPeaks.TabIndex = 160;
            checkBoxMaskDiffractionPeaks.Text = "Diffraction peak";
            checkBoxMaskDiffractionPeaks.UseVisualStyleBackColor = true;
            checkBoxMaskDiffractionPeaks.CheckedChanged += checkBoxDiffractionPeaks_CheckedChanged;
            // 
            // checkBoxMaskManual
            // 
            checkBoxMaskManual.AutoSize = true;
            checkBoxMaskManual.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            checkBoxMaskManual.Location = new System.Drawing.Point(8, 130);
            checkBoxMaskManual.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            checkBoxMaskManual.Name = "checkBoxMaskManual";
            checkBoxMaskManual.Size = new System.Drawing.Size(93, 18);
            checkBoxMaskManual.TabIndex = 160;
            checkBoxMaskManual.Text = "Manual Mask";
            checkBoxMaskManual.UseVisualStyleBackColor = true;
            checkBoxMaskManual.CheckedChanged += checkBoxManualMask_CheckedChanged;
            // 
            // groupBoxPeakIndices
            // 
            groupBoxPeakIndices.Controls.Add(checkedListBoxPlaneList);
            groupBoxPeakIndices.Controls.Add(buttonCheckAllIndices);
            groupBoxPeakIndices.Controls.Add(buttonUnmaskSelectedPeaks);
            groupBoxPeakIndices.Controls.Add(buttonUncheckAllIndices);
            groupBoxPeakIndices.Enabled = false;
            groupBoxPeakIndices.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            groupBoxPeakIndices.Location = new System.Drawing.Point(3, 208);
            groupBoxPeakIndices.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxPeakIndices.Name = "groupBoxPeakIndices";
            groupBoxPeakIndices.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxPeakIndices.Size = new System.Drawing.Size(227, 182);
            groupBoxPeakIndices.TabIndex = 158;
            groupBoxPeakIndices.TabStop = false;
            // 
            // checkedListBoxPlaneList
            // 
            checkedListBoxPlaneList.ColumnWidth = 70;
            checkedListBoxPlaneList.FormattingEnabled = true;
            checkedListBoxPlaneList.HorizontalExtent = 40;
            checkedListBoxPlaneList.HorizontalScrollbar = true;
            checkedListBoxPlaneList.Location = new System.Drawing.Point(9, 47);
            checkedListBoxPlaneList.MultiColumn = true;
            checkedListBoxPlaneList.Name = "checkedListBoxPlaneList";
            checkedListBoxPlaneList.Size = new System.Drawing.Size(210, 68);
            checkedListBoxPlaneList.TabIndex = 162;
            // 
            // buttonCheckAllIndices
            // 
            buttonCheckAllIndices.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            buttonCheckAllIndices.Location = new System.Drawing.Point(63, 23);
            buttonCheckAllIndices.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            buttonCheckAllIndices.Name = "buttonCheckAllIndices";
            buttonCheckAllIndices.Size = new System.Drawing.Size(78, 24);
            buttonCheckAllIndices.TabIndex = 144;
            buttonCheckAllIndices.Text = "Check All";
            buttonCheckAllIndices.UseVisualStyleBackColor = true;
            buttonCheckAllIndices.Click += buttonCheckAllIndices_Click;
            // 
            // buttonUnmaskSelectedPeaks
            // 
            buttonUnmaskSelectedPeaks.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            buttonUnmaskSelectedPeaks.Location = new System.Drawing.Point(11, 154);
            buttonUnmaskSelectedPeaks.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            buttonUnmaskSelectedPeaks.Name = "buttonUnmaskSelectedPeaks";
            buttonUnmaskSelectedPeaks.Size = new System.Drawing.Size(188, 23);
            buttonUnmaskSelectedPeaks.TabIndex = 144;
            buttonUnmaskSelectedPeaks.Text = "Select area around the peaks";
            buttonUnmaskSelectedPeaks.UseVisualStyleBackColor = true;
            buttonUnmaskSelectedPeaks.Click += buttonUnmaskSelectedPeaks_Click;
            // 
            // buttonUncheckAllIndices
            // 
            buttonUncheckAllIndices.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            buttonUncheckAllIndices.Location = new System.Drawing.Point(141, 23);
            buttonUncheckAllIndices.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            buttonUncheckAllIndices.Name = "buttonUncheckAllIndices";
            buttonUncheckAllIndices.Size = new System.Drawing.Size(80, 24);
            buttonUncheckAllIndices.TabIndex = 144;
            buttonUncheckAllIndices.Text = "Uncheck All";
            buttonUncheckAllIndices.UseVisualStyleBackColor = true;
            buttonUncheckAllIndices.Click += buttonUncheckAllIndices_Click;
            // 
            // groupBoxManualSpot
            // 
            groupBoxManualSpot.Controls.Add(radioButtonManualDonut);
            groupBoxManualSpot.Controls.Add(radioButtonManualCircle);
            groupBoxManualSpot.Controls.Add(radioButtonManualRectangle);
            groupBoxManualSpot.Controls.Add(radioButtonManualSpot);
            groupBoxManualSpot.Controls.Add(comboBoxSpotSize);
            groupBoxManualSpot.Controls.Add(label11);
            groupBoxManualSpot.Controls.Add(label17);
            groupBoxManualSpot.Enabled = false;
            groupBoxManualSpot.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            groupBoxManualSpot.Location = new System.Drawing.Point(3, 130);
            groupBoxManualSpot.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxManualSpot.Name = "groupBoxManualSpot";
            groupBoxManualSpot.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            groupBoxManualSpot.Size = new System.Drawing.Size(227, 73);
            groupBoxManualSpot.TabIndex = 158;
            groupBoxManualSpot.TabStop = false;
            // 
            // radioButtonManualDonut
            // 
            radioButtonManualDonut.AutoSize = true;
            radioButtonManualDonut.Location = new System.Drawing.Point(158, 22);
            radioButtonManualDonut.Name = "radioButtonManualDonut";
            radioButtonManualDonut.Size = new System.Drawing.Size(58, 19);
            radioButtonManualDonut.TabIndex = 18;
            radioButtonManualDonut.Text = "Donut";
            radioButtonManualDonut.UseVisualStyleBackColor = true;
            // 
            // radioButtonManualCircle
            // 
            radioButtonManualCircle.AutoSize = true;
            radioButtonManualCircle.Location = new System.Drawing.Point(11, 22);
            radioButtonManualCircle.Name = "radioButtonManualCircle";
            radioButtonManualCircle.Size = new System.Drawing.Size(56, 19);
            radioButtonManualCircle.TabIndex = 18;
            radioButtonManualCircle.Text = "Circle";
            radioButtonManualCircle.UseVisualStyleBackColor = true;
            // 
            // radioButtonManualRectangle
            // 
            radioButtonManualRectangle.AutoSize = true;
            radioButtonManualRectangle.Location = new System.Drawing.Point(75, 22);
            radioButtonManualRectangle.Name = "radioButtonManualRectangle";
            radioButtonManualRectangle.Size = new System.Drawing.Size(81, 19);
            radioButtonManualRectangle.TabIndex = 18;
            radioButtonManualRectangle.Text = "Rectangle";
            radioButtonManualRectangle.UseVisualStyleBackColor = true;
            // 
            // radioButtonManualSpot
            // 
            radioButtonManualSpot.AutoSize = true;
            radioButtonManualSpot.Checked = true;
            radioButtonManualSpot.Location = new System.Drawing.Point(12, 45);
            radioButtonManualSpot.Name = "radioButtonManualSpot";
            radioButtonManualSpot.Size = new System.Drawing.Size(50, 19);
            radioButtonManualSpot.TabIndex = 18;
            radioButtonManualSpot.TabStop = true;
            radioButtonManualSpot.Text = "Spot";
            radioButtonManualSpot.UseVisualStyleBackColor = true;
            // 
            // comboBoxSpotSize
            // 
            comboBoxSpotSize.FormattingEnabled = true;
            comboBoxSpotSize.Items.AddRange(new object[] { "1", "2", "4", "8", "16", "32", "64", "128" });
            comboBoxSpotSize.Location = new System.Drawing.Point(144, 44);
            comboBoxSpotSize.Name = "comboBoxSpotSize";
            comboBoxSpotSize.Size = new System.Drawing.Size(45, 23);
            comboBoxSpotSize.TabIndex = 17;
            comboBoxSpotSize.Text = "8";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Tahoma", 9F);
            label11.Location = new System.Drawing.Point(192, 48);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(31, 14);
            label11.TabIndex = 15;
            label11.Text = "pixel";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new System.Drawing.Font("Tahoma", 9F);
            label17.Location = new System.Drawing.Point(71, 48);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(73, 14);
            label17.TabIndex = 15;
            label17.Text = "Size (radius)";
            // 
            // buttonUnmask
            // 
            buttonUnmask.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            buttonUnmask.Location = new System.Drawing.Point(109, 395);
            buttonUnmask.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            buttonUnmask.Name = "buttonUnmask";
            buttonUnmask.Size = new System.Drawing.Size(106, 24);
            buttonUnmask.TabIndex = 144;
            buttonUnmask.Text = "Unmask";
            buttonUnmask.UseVisualStyleBackColor = true;
            buttonUnmask.Click += buttonUnmask_Click;
            // 
            // buttonMaskAll
            // 
            buttonMaskAll.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            buttonMaskAll.Location = new System.Drawing.Point(2, 423);
            buttonMaskAll.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            buttonMaskAll.Name = "buttonMaskAll";
            buttonMaskAll.Size = new System.Drawing.Size(77, 24);
            buttonMaskAll.TabIndex = 145;
            buttonMaskAll.Text = "Mask All";
            buttonMaskAll.UseVisualStyleBackColor = true;
            buttonMaskAll.Click += buttonMaskAll_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(checkBoxInitialBackground);
            tabPage3.Controls.Add(groupBoxBackground);
            tabPage3.Controls.Add(buttonSaveBackGround);
            tabPage3.Location = new System.Drawing.Point(4, 44);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new System.Windows.Forms.Padding(3);
            tabPage3.Size = new System.Drawing.Size(192, 52);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Background";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(textBoxDiffractionInformation);
            tabPage4.Location = new System.Drawing.Point(4, 64);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new System.Windows.Forms.Padding(3);
            tabPage4.Size = new System.Drawing.Size(192, 32);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Diffraction Information";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // textBoxDiffractionInformation
            // 
            textBoxDiffractionInformation.Location = new System.Drawing.Point(0, 0);
            textBoxDiffractionInformation.Multiline = true;
            textBoxDiffractionInformation.Name = "textBoxDiffractionInformation";
            textBoxDiffractionInformation.Size = new System.Drawing.Size(270, 450);
            textBoxDiffractionInformation.TabIndex = 0;
            // 
            // checkBoxShowMaskedArea
            // 
            checkBoxShowMaskedArea.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            checkBoxShowMaskedArea.AutoSize = true;
            checkBoxShowMaskedArea.Location = new System.Drawing.Point(722, 526);
            checkBoxShowMaskedArea.Name = "checkBoxShowMaskedArea";
            checkBoxShowMaskedArea.Size = new System.Drawing.Size(132, 19);
            checkBoxShowMaskedArea.TabIndex = 179;
            checkBoxShowMaskedArea.Text = "Show masked area";
            checkBoxShowMaskedArea.UseVisualStyleBackColor = true;
            checkBoxShowMaskedArea.CheckedChanged += checkBoxShowMaskedArea_CheckedChanged;
            // 
            // buttonSaveImage
            // 
            buttonSaveImage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            buttonSaveImage.AutoSize = true;
            buttonSaveImage.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonSaveImage.Location = new System.Drawing.Point(890, 521);
            buttonSaveImage.Name = "buttonSaveImage";
            buttonSaveImage.Size = new System.Drawing.Size(82, 25);
            buttonSaveImage.TabIndex = 187;
            buttonSaveImage.Text = "Save Image";
            buttonSaveImage.UseVisualStyleBackColor = true;
            buttonSaveImage.Click += buttonSaveImage_Click;
            // 
            // graphControlFrequency
            // 
            graphControlFrequency.AllowMouseOperation = true;
            graphControlFrequency.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            graphControlFrequency.AxisLineColor = System.Drawing.Color.Gray;
            graphControlFrequency.AxisTextColor = System.Drawing.Color.Black;
            graphControlFrequency.AxisTextFont = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            graphControlFrequency.AxisXTextVisible = true;
            graphControlFrequency.AxisYTextVisible = true;
            graphControlFrequency.BackgroundColor = System.Drawing.Color.White;
            graphControlFrequency.BottomMargin = 0D;
            graphControlFrequency.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlFrequency.DivisionLineXVisible = true;
            graphControlFrequency.DivisionLineYVisible = true;
            graphControlFrequency.FixRangeHorizontal = false;
            graphControlFrequency.FixRangeVertical = false;
            graphControlFrequency.Font = new System.Drawing.Font("Segoe UI Variable Text", 9.75F);
            graphControlFrequency.GraphTitle = "";
            graphControlFrequency.IsIntegerX = true;
            graphControlFrequency.IsIntegerY = true;
            graphControlFrequency.LabelX = "X:";
            graphControlFrequency.LabelY = "Y:";
            graphControlFrequency.LeftMargin = 0F;
            graphControlFrequency.LineWidth = 1F;
            graphControlFrequency.Location = new System.Drawing.Point(679, 591);
            graphControlFrequency.LowerX = 0D;
            graphControlFrequency.LowerY = 0D;
            graphControlFrequency.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            graphControlFrequency.MaximalX = 1D;
            graphControlFrequency.MaximalY = 1D;
            graphControlFrequency.MinimalX = 0D;
            graphControlFrequency.MinimalY = 0D;
            graphControlFrequency.Mode = GraphControl.DrawingMode.Line;
            graphControlFrequency.MousePositionVisible = false;
            graphControlFrequency.MousePositionXDigit = -1;
            graphControlFrequency.MousePositionYDigit = -1;
            graphControlFrequency.Name = "graphControlFrequency";
            graphControlFrequency.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlFrequency.Size = new System.Drawing.Size(297, 80);
            graphControlFrequency.TabIndex = 164;
            graphControlFrequency.UnitX = "";
            graphControlFrequency.UnitY = "";
            graphControlFrequency.UpperPanelFont = new System.Drawing.Font("Segoe UI Variable Text", 9.75F);
            graphControlFrequency.UpperPanelVisible = false;
            graphControlFrequency.UpperX = 1D;
            graphControlFrequency.UpperY = 1D;
            graphControlFrequency.UseLineWidth = true;
            graphControlFrequency.VerticalLineColor = System.Drawing.Color.Red;
            graphControlFrequency.XLog = true;
            graphControlFrequency.YLog = true;
            graphControlFrequency.LinePositionChanged += graphControlFrequency_LinePositionChanged;
            // 
            // scalablePictureBox
            // 
            scalablePictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            scalablePictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            scalablePictureBox.FixZoomAndCenter = false;
            scalablePictureBox.FocusEventEnabled = false;
            scalablePictureBox.HorizontalFlip = false;
            scalablePictureBox.Location = new System.Drawing.Point(0, 0);
            scalablePictureBox.ManualSpotMode = false;
            scalablePictureBox.Margin = new System.Windows.Forms.Padding(0);
            scalablePictureBox.MouseScaling = true;
            scalablePictureBox.MouseTranslation = false;
            scalablePictureBox.Name = "scalablePictureBox";
            scalablePictureBox.ShowAreaRectangle = false;
            scalablePictureBox.ShowRimRentangle = false;
            scalablePictureBox.Size = new System.Drawing.Size(676, 701);
            scalablePictureBox.TabIndex = 169;
            scalablePictureBox.TitleVisible = false;
            scalablePictureBox.VerticalFlip = false;
            scalablePictureBox.Zoom = 128D;
            scalablePictureBox.Paint2 += scalablePictureBox_Paint2;
            scalablePictureBox.MouseMove2 += scalablePictureBox_MouseMove2;
            scalablePictureBox.MouseUp2 += scalablePictureBox_MouseUp2;
            scalablePictureBox.MouseDown2 += scalablePictureBox_MouseDown2;
            // 
            // labelLaTex1
            // 
            labelLaTex1.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F);
            labelLaTex1.Location = new System.Drawing.Point(6, 19);
            labelLaTex1.Name = "labelLaTex1";
            labelLaTex1.Size = new System.Drawing.Size(18, 23);
            labelLaTex1.TabIndex = 190;
            labelLaTex1.Text = "\\varphi";
            labelLaTex1.Thickness = 0.6D;
            // 
            // labelLaTex2
            // 
            labelLaTex2.Font = new System.Drawing.Font("Segoe UI Variable Text", 12F);
            labelLaTex2.Location = new System.Drawing.Point(109, 17);
            labelLaTex2.Name = "labelLaTex2";
            labelLaTex2.Size = new System.Drawing.Size(18, 23);
            labelLaTex2.TabIndex = 190;
            labelLaTex2.Text = "\\tau";
            labelLaTex2.Thickness = 0.6D;
            // 
            // DiffractionPatternControl
            // 
            Controls.Add(buttonSaveImage);
            Controls.Add(numericUpDownMaxInt);
            Controls.Add(numericUpDownMinInt);
            Controls.Add(checkBoxShowMaskedArea);
            Controls.Add(graphControlFrequency);
            Controls.Add(panelSimulationCheck);
            Controls.Add(label25);
            Controls.Add(label36);
            Controls.Add(tabControl1);
            Controls.Add(scalablePictureBox);
            Font = new System.Drawing.Font("Segoe UI Variable Text", 9F);
            Name = "DiffractionPatternControl";
            Size = new System.Drawing.Size(979, 701);
            DragDrop += DiffractionPatternControl_DragDrop;
            DragEnter += DiffractionPatternControl_DragEnter;
            ((System.ComponentModel.ISupportInitialize)numericUpDownMaxInt).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinInt).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOpacity).EndInit();
            groupBoxCircleMask.ResumeLayout(false);
            groupBoxCircleMask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleEnd).EndInit();
            groupBoxRectangle.ResumeLayout(false);
            groupBoxRectangle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRectangleAngle).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRectangleBand).EndInit();
            groupBoxGeometry.ResumeLayout(false);
            groupBoxGeometry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarBgH).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBgR).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBgA).EndInit();
            groupBoxBackground.ResumeLayout(false);
            groupBoxBackground.PerformLayout();
            panelSimulationCheck.ResumeLayout(false);
            panelSimulationCheck.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBoxWaveSource.ResumeLayout(false);
            groupBoxWaveSource.PerformLayout();
            groupBoxDetectorProperty.ResumeLayout(false);
            groupBoxDetectorProperty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImageWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImageHeight).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            groupBoxPeakIndices.ResumeLayout(false);
            groupBoxManualSpot.ResumeLayout(false);
            groupBoxManualSpot.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

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
        private System.Windows.Forms.GroupBox groupBoxDetectorProperty;
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
        private System.Windows.Forms.GroupBox groupBoxWaveSource;
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
        private LabelLaTeX labelLaTex2;
        private LabelLaTeX labelLaTex1;
    }
}
