using Crystallography.Controls;
using System.Windows.Forms;

namespace ReciPro
{
    partial class FormDiffractionSimulatorDynamicCompression
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
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        // (260323Ch) renamed numeric container controls:
        // groupBox1 -> groupBoxCompressedRotation
        // groupBox2 -> groupBoxReleasedRotation
        // groupBox3 -> groupBoxEOS
        // groupBox4 -> groupBoxShockedPlane
        // groupBox5 -> groupBoxCompressedArea
        // groupBox6 -> groupBoxReleasedArea
        // groupBox7 -> groupBoxSampleParameters
        // groupBox8 -> groupBoxOutputParameters
        // groupBox9 -> groupBoxCompressionModel
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            toolTip = new System.Windows.Forms.ToolTip(components);
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            captureExtender.SetCapture(this, true); // 260521Cl 追加: GUI監査キャプチャ対象 (フォーム全体)
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            groupBoxCompressedRotation = new System.Windows.Forms.GroupBox();
            numericBoxCompressedThetaB = new Crystallography.Controls.NumericBox();
            numericBoxCompressedOmega = new Crystallography.Controls.NumericBox();
            numericBoxCompressedThetaA = new Crystallography.Controls.NumericBox();
            numericBoxCompressedOmegaSigma = new Crystallography.Controls.NumericBox();
            groupBoxReleasedRotation = new System.Windows.Forms.GroupBox();
            numericBoxReleasedThetaB = new Crystallography.Controls.NumericBox();
            numericBoxReleasedOmegaSigma = new Crystallography.Controls.NumericBox();
            numericBoxReleasedOmega = new Crystallography.Controls.NumericBox();
            numericBoxReleasedThetaA = new Crystallography.Controls.NumericBox();
            groupBoxEOS = new System.Windows.Forms.GroupBox();
            numericBoxEOS_K0 = new Crystallography.Controls.NumericBox();
            numericBoxEOS_Kprime = new Crystallography.Controls.NumericBox();
            groupBoxShockedPlane = new System.Windows.Forms.GroupBox();
            numericBoxShockedPlaneH = new Crystallography.Controls.NumericBox();
            numericBoxShockedPlaneK = new Crystallography.Controls.NumericBox();
            numericBoxShockedPlaneL = new Crystallography.Controls.NumericBox();
            buttonSimulate = new System.Windows.Forms.Button();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            checkBoxSkipDrawing = new System.Windows.Forms.CheckBox();
            groupBoxCompressedArea = new System.Windows.Forms.GroupBox();
            radioButtonCompressedIsotropic = new System.Windows.Forms.RadioButton();
            radioButtonCompressedUniaxial = new System.Windows.Forms.RadioButton();
            numericBoxUp = new Crystallography.Controls.NumericBox();
            groupBoxReleasedArea = new System.Windows.Forms.GroupBox();
            radioButtonReleasedIsotropic = new System.Windows.Forms.RadioButton();
            numericBoxUr = new Crystallography.Controls.NumericBox();
            radioButtonReleasedUniaxial = new System.Windows.Forms.RadioButton();
            groupBoxSampleParameters = new System.Windows.Forms.GroupBox();
            numericBoxMassAbsorption = new Crystallography.Controls.NumericBox();
            label4 = new System.Windows.Forms.Label();
            checkBoxSaveSimulatedPattern = new System.Windows.Forms.CheckBox();
            folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            buttonSetFolder = new System.Windows.Forms.Button();
            textBoxFileName = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            flowLayoutPanelSavePatterns = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxOutputParameters = new System.Windows.Forms.GroupBox();
            flowLayoutPanelOmegaStep = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxOmegaStep = new Crystallography.Controls.NumericBox();
            numericBoxOmegaTimes = new Crystallography.Controls.NumericBox();
            checkBoxOmegaStep = new System.Windows.Forms.CheckBox();
            numericBoxDivisionOfRotationAngle = new Crystallography.Controls.NumericBox();
            numericBoxDivisionOfRotationAxis = new Crystallography.Controls.NumericBox();
            trackBarAdvancedBack = new Crystallography.Controls.TrackBarAdvanced();
            trackBarAdvancedTime = new Crystallography.Controls.TrackBarAdvanced();
            trackBarAdvancedFront = new Crystallography.Controls.TrackBarAdvanced();
            graphControl = new Crystallography.Controls.GraphControl();
            groupBoxCompressionModel = new System.Windows.Forms.GroupBox();
            radioButton2019Model = new System.Windows.Forms.RadioButton();
            radioButton2018Model = new System.Windows.Forms.RadioButton();
            groupBoxSlipPlane = new System.Windows.Forms.GroupBox();
            numericBoxSlipPlaneH = new Crystallography.Controls.NumericBox();
            numericBoxSlipPlaneK = new Crystallography.Controls.NumericBox();
            numericBoxSlipPlaneL = new Crystallography.Controls.NumericBox();
            label5 = new System.Windows.Forms.Label();
            groupBoxCompressedRotation.SuspendLayout();
            groupBoxReleasedRotation.SuspendLayout();
            groupBoxEOS.SuspendLayout();
            groupBoxShockedPlane.SuspendLayout();
            statusStrip1.SuspendLayout();
            groupBoxCompressedArea.SuspendLayout();
            groupBoxReleasedArea.SuspendLayout();
            groupBoxSampleParameters.SuspendLayout();
            flowLayoutPanelSavePatterns.SuspendLayout();
            groupBoxOutputParameters.SuspendLayout();
            flowLayoutPanelOmegaStep.SuspendLayout();
            groupBoxCompressionModel.SuspendLayout();
            groupBoxSlipPlane.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(19, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(88, 17);
            label1.TabIndex = 3;
            label1.Text = "h        k        l";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(-3, 557);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(38, 17);
            label2.TabIndex = 3;
            label2.Text = "Front";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(-3, 590);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(34, 17);
            label3.TabIndex = 3;
            label3.Text = "Back";
            // 
            // groupBoxCompressedRotation
            // 
            groupBoxCompressedRotation.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            groupBoxCompressedRotation.Controls.Add(numericBoxCompressedThetaB);
            groupBoxCompressedRotation.Controls.Add(numericBoxCompressedOmega);
            groupBoxCompressedRotation.Controls.Add(numericBoxCompressedThetaA);
            groupBoxCompressedRotation.Controls.Add(numericBoxCompressedOmegaSigma);
            groupBoxCompressedRotation.Location = new System.Drawing.Point(12, 99);
            groupBoxCompressedRotation.Name = "groupBoxCompressedRotation";
            groupBoxCompressedRotation.Size = new System.Drawing.Size(143, 135);
            groupBoxCompressedRotation.TabIndex = 4;
            groupBoxCompressedRotation.TabStop = false;
            groupBoxCompressedRotation.Text = "Rotation distribution";
            // 
            // numericBoxCompressedThetaB
            // 
                       numericBoxCompressedThetaB.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCompressedThetaB.DecimalPlaces = -2;
                        numericBoxCompressedThetaB.FooterText = "°/ns";
                        numericBoxCompressedThetaB.HeaderText = "b";
            numericBoxCompressedThetaB.Location = new System.Drawing.Point(17, 104);
            numericBoxCompressedThetaB.Margin = new System.Windows.Forms.Padding(14, 0, 0, 0);
            numericBoxCompressedThetaB.Maximum = 10000D;
            numericBoxCompressedThetaB.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxCompressedThetaB.Minimum = 0D;
            numericBoxCompressedThetaB.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxCompressedThetaB.Name = "numericBoxCompressedThetaB";
            numericBoxCompressedThetaB.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxCompressedThetaB.RadianValue = 0.0087266462599716477D;
                        numericBoxCompressedThetaB.Size = new System.Drawing.Size(120, 25);
            numericBoxCompressedThetaB.SkipEventDuringInput = false;
            numericBoxCompressedThetaB.SmartIncrement = true;
            numericBoxCompressedThetaB.TabIndex = 2;
                        numericBoxCompressedThetaB.ThousandsSeparator = true;
            numericBoxCompressedThetaB.Value = 0.5D;
                        // 
            // numericBoxCompressedOmega
            // 
                       numericBoxCompressedOmega.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCompressedOmega.DecimalPlaces = -2;
                        numericBoxCompressedOmega.FooterText = "°/ns";
                        numericBoxCompressedOmega.HeaderText = "ω";
            numericBoxCompressedOmega.Location = new System.Drawing.Point(14, 21);
            numericBoxCompressedOmega.Margin = new System.Windows.Forms.Padding(12, 0, 0, 3);
            numericBoxCompressedOmega.Maximum = 10000D;
            numericBoxCompressedOmega.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxCompressedOmega.Minimum = 0D;
            numericBoxCompressedOmega.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxCompressedOmega.Name = "numericBoxCompressedOmega";
            numericBoxCompressedOmega.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxCompressedOmega.RadianValue = 0.00017453292519943296D;
                        numericBoxCompressedOmega.Size = new System.Drawing.Size(124, 25);
            numericBoxCompressedOmega.SkipEventDuringInput = false;
            numericBoxCompressedOmega.SmartIncrement = true;
            numericBoxCompressedOmega.TabIndex = 2;
                        numericBoxCompressedOmega.ThousandsSeparator = true;
            numericBoxCompressedOmega.Value = 0.01D;
                        // 
            // numericBoxCompressedThetaA
            // 
                       numericBoxCompressedThetaA.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCompressedThetaA.DecimalPlaces = -2;
                        numericBoxCompressedThetaA.FooterText = "°";
                        numericBoxCompressedThetaA.HeaderText = "a";
            numericBoxCompressedThetaA.Location = new System.Drawing.Point(18, 77);
            numericBoxCompressedThetaA.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            numericBoxCompressedThetaA.Maximum = 10000D;
            numericBoxCompressedThetaA.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxCompressedThetaA.Minimum = 0D;
            numericBoxCompressedThetaA.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxCompressedThetaA.Name = "numericBoxCompressedThetaA";
            numericBoxCompressedThetaA.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
                        numericBoxCompressedThetaA.Size = new System.Drawing.Size(102, 25);
            numericBoxCompressedThetaA.SkipEventDuringInput = false;
            numericBoxCompressedThetaA.SmartIncrement = true;
            numericBoxCompressedThetaA.TabIndex = 2;
                        numericBoxCompressedThetaA.ThousandsSeparator = true;
                                    // 
            // numericBoxCompressedOmegaSigma
            // 
                       numericBoxCompressedOmegaSigma.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCompressedOmegaSigma.DecimalPlaces = -2;
                        numericBoxCompressedOmegaSigma.FooterText = "°/ns";
                        numericBoxCompressedOmegaSigma.HeaderText = "σ_ω";
            numericBoxCompressedOmegaSigma.Location = new System.Drawing.Point(2, 49);
            numericBoxCompressedOmegaSigma.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            numericBoxCompressedOmegaSigma.Maximum = 10000D;
            numericBoxCompressedOmegaSigma.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxCompressedOmegaSigma.Minimum = 0D;
            numericBoxCompressedOmegaSigma.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxCompressedOmegaSigma.Name = "numericBoxCompressedOmegaSigma";
            numericBoxCompressedOmegaSigma.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxCompressedOmegaSigma.RadianValue = 0.00017453292519943296D;
                        numericBoxCompressedOmegaSigma.Size = new System.Drawing.Size(136, 25);
            numericBoxCompressedOmegaSigma.SkipEventDuringInput = false;
            numericBoxCompressedOmegaSigma.SmartIncrement = true;
            numericBoxCompressedOmegaSigma.TabIndex = 2;
                        numericBoxCompressedOmegaSigma.ThousandsSeparator = true;
            numericBoxCompressedOmegaSigma.Value = 0.01D;
                        // 
            // groupBoxReleasedRotation
            // 
            groupBoxReleasedRotation.Controls.Add(numericBoxReleasedThetaB);
            groupBoxReleasedRotation.Controls.Add(numericBoxReleasedOmegaSigma);
            groupBoxReleasedRotation.Controls.Add(numericBoxReleasedOmega);
            groupBoxReleasedRotation.Controls.Add(numericBoxReleasedThetaA);
            groupBoxReleasedRotation.Location = new System.Drawing.Point(7, 99);
            groupBoxReleasedRotation.Name = "groupBoxReleasedRotation";
            groupBoxReleasedRotation.Size = new System.Drawing.Size(143, 135);
            groupBoxReleasedRotation.TabIndex = 4;
            groupBoxReleasedRotation.TabStop = false;
            groupBoxReleasedRotation.Text = "Rotation distribution";
            // 
            // numericBoxReleasedThetaB
            // 
                       numericBoxReleasedThetaB.BackColor = System.Drawing.SystemColors.Control;
            numericBoxReleasedThetaB.DecimalPlaces = -2;
                        numericBoxReleasedThetaB.FooterText = "°/ns";
                        numericBoxReleasedThetaB.HeaderText = "b";
            numericBoxReleasedThetaB.Location = new System.Drawing.Point(18, 104);
            numericBoxReleasedThetaB.Margin = new System.Windows.Forms.Padding(14, 0, 0, 0);
            numericBoxReleasedThetaB.Maximum = 10000D;
            numericBoxReleasedThetaB.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxReleasedThetaB.Minimum = 0D;
            numericBoxReleasedThetaB.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxReleasedThetaB.Name = "numericBoxReleasedThetaB";
            numericBoxReleasedThetaB.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxReleasedThetaB.RadianValue = 0.0087266462599716477D;
                        numericBoxReleasedThetaB.Size = new System.Drawing.Size(120, 25);
            numericBoxReleasedThetaB.SkipEventDuringInput = false;
            numericBoxReleasedThetaB.SmartIncrement = true;
            numericBoxReleasedThetaB.TabIndex = 2;
                        numericBoxReleasedThetaB.ThousandsSeparator = true;
            numericBoxReleasedThetaB.Value = 0.5D;
                        // 
            // numericBoxReleasedOmegaSigma
            // 
                       numericBoxReleasedOmegaSigma.BackColor = System.Drawing.SystemColors.Control;
            numericBoxReleasedOmegaSigma.DecimalPlaces = -2;
                        numericBoxReleasedOmegaSigma.FooterText = "°/ns";
                        numericBoxReleasedOmegaSigma.HeaderText = "σ_ω";
            numericBoxReleasedOmegaSigma.Location = new System.Drawing.Point(4, 49);
            numericBoxReleasedOmegaSigma.Margin = new System.Windows.Forms.Padding(0);
            numericBoxReleasedOmegaSigma.Maximum = 10000D;
            numericBoxReleasedOmegaSigma.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxReleasedOmegaSigma.Minimum = 0D;
            numericBoxReleasedOmegaSigma.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxReleasedOmegaSigma.Name = "numericBoxReleasedOmegaSigma";
            numericBoxReleasedOmegaSigma.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxReleasedOmegaSigma.RadianValue = 0.0087266462599716477D;
                        numericBoxReleasedOmegaSigma.Size = new System.Drawing.Size(136, 25);
            numericBoxReleasedOmegaSigma.SkipEventDuringInput = false;
            numericBoxReleasedOmegaSigma.SmartIncrement = true;
            numericBoxReleasedOmegaSigma.TabIndex = 2;
                        numericBoxReleasedOmegaSigma.ThousandsSeparator = true;
            numericBoxReleasedOmegaSigma.Value = 0.5D;
                        // 
            // numericBoxReleasedOmega
            // 
                       numericBoxReleasedOmega.BackColor = System.Drawing.SystemColors.Control;
            numericBoxReleasedOmega.DecimalPlaces = -2;
                        numericBoxReleasedOmega.FooterText = "°/ns";
                        numericBoxReleasedOmega.HeaderText = "ω";
            numericBoxReleasedOmega.Location = new System.Drawing.Point(16, 21);
            numericBoxReleasedOmega.Margin = new System.Windows.Forms.Padding(0);
            numericBoxReleasedOmega.Maximum = 10000D;
            numericBoxReleasedOmega.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxReleasedOmega.Minimum = 0D;
            numericBoxReleasedOmega.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxReleasedOmega.Name = "numericBoxReleasedOmega";
            numericBoxReleasedOmega.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxReleasedOmega.RadianValue = 0.0087266462599716477D;
                        numericBoxReleasedOmega.Size = new System.Drawing.Size(124, 25);
            numericBoxReleasedOmega.SkipEventDuringInput = false;
            numericBoxReleasedOmega.SmartIncrement = true;
            numericBoxReleasedOmega.TabIndex = 2;
                        numericBoxReleasedOmega.ThousandsSeparator = true;
            numericBoxReleasedOmega.Value = 0.5D;
                        // 
            // numericBoxReleasedThetaA
            // 
                       numericBoxReleasedThetaA.BackColor = System.Drawing.SystemColors.Control;
            numericBoxReleasedThetaA.DecimalPlaces = -2;
                        numericBoxReleasedThetaA.FooterText = "°";
                        numericBoxReleasedThetaA.HeaderText = "a";
            numericBoxReleasedThetaA.Location = new System.Drawing.Point(20, 77);
            numericBoxReleasedThetaA.Margin = new System.Windows.Forms.Padding(15, 0, 0, 0);
            numericBoxReleasedThetaA.Maximum = 10000D;
            numericBoxReleasedThetaA.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxReleasedThetaA.Minimum = 0D;
            numericBoxReleasedThetaA.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxReleasedThetaA.Name = "numericBoxReleasedThetaA";
            numericBoxReleasedThetaA.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
                        numericBoxReleasedThetaA.Size = new System.Drawing.Size(102, 25);
            numericBoxReleasedThetaA.SkipEventDuringInput = false;
            numericBoxReleasedThetaA.SmartIncrement = true;
            numericBoxReleasedThetaA.TabIndex = 2;
                        numericBoxReleasedThetaA.ThousandsSeparator = true;
                                    // 
            // groupBoxEOS
            // 
            groupBoxEOS.Controls.Add(numericBoxEOS_K0);
            groupBoxEOS.Controls.Add(numericBoxEOS_Kprime);
            groupBoxEOS.Location = new System.Drawing.Point(12, 87);
            groupBoxEOS.Name = "groupBoxEOS";
            groupBoxEOS.Size = new System.Drawing.Size(141, 74);
            groupBoxEOS.TabIndex = 5;
            groupBoxEOS.TabStop = false;
            groupBoxEOS.Text = "EOS";
            // 
            // numericBoxEOS_K0
            // 
                       numericBoxEOS_K0.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEOS_K0.DecimalPlaces = -2;
                        numericBoxEOS_K0.FooterText = "GPa";
                        numericBoxEOS_K0.HeaderText = "K0";
            numericBoxEOS_K0.Location = new System.Drawing.Point(14, 18);
            numericBoxEOS_K0.Margin = new System.Windows.Forms.Padding(0);
            numericBoxEOS_K0.Maximum = 10000D;
            numericBoxEOS_K0.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxEOS_K0.Minimum = 0D;
            numericBoxEOS_K0.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxEOS_K0.Name = "numericBoxEOS_K0";
            numericBoxEOS_K0.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxEOS_K0.RadianValue = 0.647866218340295D;
                        numericBoxEOS_K0.Size = new System.Drawing.Size(114, 25);
            numericBoxEOS_K0.SkipEventDuringInput = false;
            numericBoxEOS_K0.SmartIncrement = true;
            numericBoxEOS_K0.TabIndex = 2;
                        numericBoxEOS_K0.ThousandsSeparator = true;
            numericBoxEOS_K0.Value = 37.12D;
                        // 
            // numericBoxEOS_Kprime
            // 
                       numericBoxEOS_Kprime.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEOS_Kprime.DecimalPlaces = -2;
                                                numericBoxEOS_Kprime.HeaderText = "K\'0";
            numericBoxEOS_Kprime.Location = new System.Drawing.Point(12, 44);
            numericBoxEOS_Kprime.Margin = new System.Windows.Forms.Padding(0);
            numericBoxEOS_Kprime.Maximum = 10000D;
            numericBoxEOS_Kprime.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxEOS_Kprime.Minimum = 0D;
            numericBoxEOS_Kprime.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxEOS_Kprime.Name = "numericBoxEOS_Kprime";
            numericBoxEOS_Kprime.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxEOS_Kprime.RadianValue = 0.10454522219446034D;
                        numericBoxEOS_Kprime.Size = new System.Drawing.Size(83, 25);
            numericBoxEOS_Kprime.SkipEventDuringInput = false;
            numericBoxEOS_Kprime.SmartIncrement = true;
            numericBoxEOS_Kprime.TabIndex = 2;
                        numericBoxEOS_Kprime.ThousandsSeparator = true;
            numericBoxEOS_Kprime.Value = 5.99D;
                        // 
            // groupBoxShockedPlane
            // 
            groupBoxShockedPlane.Controls.Add(numericBoxShockedPlaneH);
            groupBoxShockedPlane.Controls.Add(numericBoxShockedPlaneK);
            groupBoxShockedPlane.Controls.Add(numericBoxShockedPlaneL);
            groupBoxShockedPlane.Controls.Add(label1);
            groupBoxShockedPlane.Location = new System.Drawing.Point(12, 19);
            groupBoxShockedPlane.Name = "groupBoxShockedPlane";
            groupBoxShockedPlane.Size = new System.Drawing.Size(144, 62);
            groupBoxShockedPlane.TabIndex = 6;
            groupBoxShockedPlane.TabStop = false;
            groupBoxShockedPlane.Text = "Shocked plane";
            // 
            // numericBoxShockedPlaneH
            // 
                       numericBoxShockedPlaneH.BackColor = System.Drawing.SystemColors.Control;
            numericBoxShockedPlaneH.DecimalPlaces = -2;
                                                numericBoxShockedPlaneH.HeaderText = "(";
            numericBoxShockedPlaneH.Location = new System.Drawing.Point(3, 33);
            numericBoxShockedPlaneH.Margin = new System.Windows.Forms.Padding(0);
            numericBoxShockedPlaneH.Maximum = 10D;
            numericBoxShockedPlaneH.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxShockedPlaneH.Minimum = -10D;
            numericBoxShockedPlaneH.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxShockedPlaneH.Name = "numericBoxShockedPlaneH";
            numericBoxShockedPlaneH.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxShockedPlaneH.RadianValue = 0.017453292519943295D;
            numericBoxShockedPlaneH.ShowUpDown = true;
            numericBoxShockedPlaneH.Size = new System.Drawing.Size(50, 25);
            numericBoxShockedPlaneH.SkipEventDuringInput = false;
            numericBoxShockedPlaneH.TabIndex = 2;
                        numericBoxShockedPlaneH.ThousandsSeparator = true;
            numericBoxShockedPlaneH.Value = 1D;
                        // 
            // numericBoxShockedPlaneK
            // 
                       numericBoxShockedPlaneK.BackColor = System.Drawing.SystemColors.Control;
            numericBoxShockedPlaneK.DecimalPlaces = -2;
            numericBoxShockedPlaneK.Location = new System.Drawing.Point(53, 33);
            numericBoxShockedPlaneK.Margin = new System.Windows.Forms.Padding(0);
            numericBoxShockedPlaneK.Maximum = 10D;
            numericBoxShockedPlaneK.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxShockedPlaneK.Minimum = -10D;
            numericBoxShockedPlaneK.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxShockedPlaneK.Name = "numericBoxShockedPlaneK";
            numericBoxShockedPlaneK.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxShockedPlaneK.ShowUpDown = true;
            numericBoxShockedPlaneK.Size = new System.Drawing.Size(38, 25);
            numericBoxShockedPlaneK.SkipEventDuringInput = false;
            numericBoxShockedPlaneK.TabIndex = 2;
                        numericBoxShockedPlaneK.ThousandsSeparator = true;
                                    // 
            // numericBoxShockedPlaneL
            // 
                       numericBoxShockedPlaneL.BackColor = System.Drawing.SystemColors.Control;
            numericBoxShockedPlaneL.DecimalPlaces = -2;
                        numericBoxShockedPlaneL.FooterText = ")";
            numericBoxShockedPlaneL.Location = new System.Drawing.Point(91, 33);
            numericBoxShockedPlaneL.Margin = new System.Windows.Forms.Padding(0);
            numericBoxShockedPlaneL.Maximum = 1000D;
            numericBoxShockedPlaneL.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxShockedPlaneL.Minimum = 0D;
            numericBoxShockedPlaneL.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxShockedPlaneL.Name = "numericBoxShockedPlaneL";
            numericBoxShockedPlaneL.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxShockedPlaneL.ShowUpDown = true;
            numericBoxShockedPlaneL.Size = new System.Drawing.Size(50, 25);
            numericBoxShockedPlaneL.SkipEventDuringInput = false;
            numericBoxShockedPlaneL.TabIndex = 2;
                        numericBoxShockedPlaneL.ThousandsSeparator = true;
                                    // 
            // buttonSimulate
            // 
            buttonSimulate.AutoSize = true;
            buttonSimulate.Location = new System.Drawing.Point(472, 740);
            buttonSimulate.Name = "buttonSimulate";
            buttonSimulate.Size = new System.Drawing.Size(75, 27);
            buttonSimulate.TabIndex = 7;
            buttonSimulate.Text = "Simulate"; // 260520Cl: Execute→Simulate (用語統一)
            buttonSimulate.BackColor = System.Drawing.Color.SteelBlue; // 260520Cl: 主要アクション色を統一
            buttonSimulate.ForeColor = System.Drawing.Color.White;
            buttonSimulate.UseVisualStyleBackColor = false;
            buttonSimulate.Click += new System.EventHandler(buttonSimulate_Click);
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripProgressBar,
            toolStripStatusLabel1});
            statusStrip1.Location = new System.Drawing.Point(0, 770);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(556, 22);
            statusStrip1.TabIndex = 9;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new System.Drawing.Size(150, 16);
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            toolStripStatusLabel1.Text = "   ";
            // 
            // checkBoxSkipDrawing
            // 
            checkBoxSkipDrawing.AutoSize = true;
            checkBoxSkipDrawing.Checked = true;
            checkBoxSkipDrawing.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxSkipDrawing.Location = new System.Drawing.Point(269, 742);
            checkBoxSkipDrawing.Name = "checkBoxSkipDrawing";
            checkBoxSkipDrawing.Size = new System.Drawing.Size(203, 21);
            checkBoxSkipDrawing.TabIndex = 10;
            checkBoxSkipDrawing.Text = "Skip drawing during execution";
            checkBoxSkipDrawing.UseVisualStyleBackColor = true;
            // 
            // groupBoxCompressedArea
            // 
            groupBoxCompressedArea.Controls.Add(radioButtonCompressedIsotropic);
            groupBoxCompressedArea.Controls.Add(radioButtonCompressedUniaxial);
            groupBoxCompressedArea.Controls.Add(groupBoxCompressedRotation);
            groupBoxCompressedArea.Controls.Add(numericBoxUp);
            groupBoxCompressedArea.Location = new System.Drawing.Point(6, 85);
            groupBoxCompressedArea.Name = "groupBoxCompressedArea";
            groupBoxCompressedArea.Size = new System.Drawing.Size(166, 240);
            groupBoxCompressedArea.TabIndex = 11;
            groupBoxCompressedArea.TabStop = false;
            groupBoxCompressedArea.Text = "Compressed area";
            // 
            // radioButtonCompressedIsotropic
            // 
            radioButtonCompressedIsotropic.AutoSize = true;
            radioButtonCompressedIsotropic.Location = new System.Drawing.Point(6, 73);
            radioButtonCompressedIsotropic.Name = "radioButtonCompressedIsotropic";
            radioButtonCompressedIsotropic.Size = new System.Drawing.Size(156, 21);
            radioButtonCompressedIsotropic.TabIndex = 5;
            radioButtonCompressedIsotropic.Text = "Isotropic compression";
            radioButtonCompressedIsotropic.UseVisualStyleBackColor = true;
            // 
            // radioButtonCompressedUniaxial
            // 
            radioButtonCompressedUniaxial.AutoSize = true;
            radioButtonCompressedUniaxial.Checked = true;
            radioButtonCompressedUniaxial.Location = new System.Drawing.Point(6, 51);
            radioButtonCompressedUniaxial.Name = "radioButtonCompressedUniaxial";
            radioButtonCompressedUniaxial.Size = new System.Drawing.Size(150, 21);
            radioButtonCompressedUniaxial.TabIndex = 5;
            radioButtonCompressedUniaxial.TabStop = true;
            radioButtonCompressedUniaxial.Text = "Uniaxial compression";
            radioButtonCompressedUniaxial.UseVisualStyleBackColor = true;
            // 
            // numericBoxUp
            // 
                       numericBoxUp.BackColor = System.Drawing.SystemColors.Control;
            numericBoxUp.DecimalPlaces = -2;
                        numericBoxUp.FooterText = "km/s";
                        numericBoxUp.HeaderText = "Us";
            numericBoxUp.Location = new System.Drawing.Point(7, 24);
            numericBoxUp.Margin = new System.Windows.Forms.Padding(0);
            numericBoxUp.Maximum = 10000D;
            numericBoxUp.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxUp.Minimum = 0D;
            numericBoxUp.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxUp.Name = "numericBoxUp";
            numericBoxUp.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxUp.RadianValue = 0.09599310885968812D;
                        numericBoxUp.Size = new System.Drawing.Size(143, 25);
            numericBoxUp.SkipEventDuringInput = false;
            numericBoxUp.SmartIncrement = true;
            numericBoxUp.TabIndex = 2;
                        numericBoxUp.ThousandsSeparator = true;
            numericBoxUp.Value = 5.5D;
                        // 
            // groupBoxReleasedArea
            // 
            groupBoxReleasedArea.Controls.Add(radioButtonReleasedIsotropic);
            groupBoxReleasedArea.Controls.Add(numericBoxUr);
            groupBoxReleasedArea.Controls.Add(radioButtonReleasedUniaxial);
            groupBoxReleasedArea.Controls.Add(groupBoxReleasedRotation);
            groupBoxReleasedArea.Location = new System.Drawing.Point(178, 85);
            groupBoxReleasedArea.Name = "groupBoxReleasedArea";
            groupBoxReleasedArea.Size = new System.Drawing.Size(166, 240);
            groupBoxReleasedArea.TabIndex = 12;
            groupBoxReleasedArea.TabStop = false;
            groupBoxReleasedArea.Text = "Released area";
            // 
            // radioButtonReleasedIsotropic
            // 
            radioButtonReleasedIsotropic.AutoSize = true;
            radioButtonReleasedIsotropic.Location = new System.Drawing.Point(7, 73);
            radioButtonReleasedIsotropic.Name = "radioButtonReleasedIsotropic";
            radioButtonReleasedIsotropic.Size = new System.Drawing.Size(156, 21);
            radioButtonReleasedIsotropic.TabIndex = 5;
            radioButtonReleasedIsotropic.Text = "Isotropic compression";
            radioButtonReleasedIsotropic.UseVisualStyleBackColor = true;
            // 
            // numericBoxUr
            // 
                       numericBoxUr.BackColor = System.Drawing.SystemColors.Control;
            numericBoxUr.DecimalPlaces = -2;
                        numericBoxUr.FooterText = "km/s";
                        numericBoxUr.HeaderText = "Ur";
            numericBoxUr.Location = new System.Drawing.Point(7, 24);
            numericBoxUr.Margin = new System.Windows.Forms.Padding(0);
            numericBoxUr.Maximum = 10000D;
            numericBoxUr.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxUr.Minimum = 0D;
            numericBoxUr.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxUr.Name = "numericBoxUr";
            numericBoxUr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxUr.RadianValue = 0.09599310885968812D;
                        numericBoxUr.Size = new System.Drawing.Size(143, 25);
            numericBoxUr.SkipEventDuringInput = false;
            numericBoxUr.SmartIncrement = true;
            numericBoxUr.TabIndex = 2;
                        numericBoxUr.ThousandsSeparator = true;
            numericBoxUr.Value = 5.5D;
                        // 
            // radioButtonReleasedUniaxial
            // 
            radioButtonReleasedUniaxial.AutoSize = true;
            radioButtonReleasedUniaxial.Checked = true;
            radioButtonReleasedUniaxial.Location = new System.Drawing.Point(7, 51);
            radioButtonReleasedUniaxial.Name = "radioButtonReleasedUniaxial";
            radioButtonReleasedUniaxial.Size = new System.Drawing.Size(150, 21);
            radioButtonReleasedUniaxial.TabIndex = 5;
            radioButtonReleasedUniaxial.TabStop = true;
            radioButtonReleasedUniaxial.Text = "Uniaxial compression";
            radioButtonReleasedUniaxial.UseVisualStyleBackColor = true;
            // 
            // groupBoxSampleParameters
            // 
            groupBoxSampleParameters.Controls.Add(groupBoxEOS);
            groupBoxSampleParameters.Controls.Add(numericBoxMassAbsorption);
            groupBoxSampleParameters.Controls.Add(groupBoxShockedPlane);
            groupBoxSampleParameters.Controls.Add(label4);
            groupBoxSampleParameters.Location = new System.Drawing.Point(2, 1);
            groupBoxSampleParameters.Name = "groupBoxSampleParameters";
            groupBoxSampleParameters.Size = new System.Drawing.Size(176, 227);
            groupBoxSampleParameters.TabIndex = 13;
            groupBoxSampleParameters.TabStop = false;
            groupBoxSampleParameters.Text = "Sample parameters";
            // 
            // numericBoxMassAbsorption
            // 
                       numericBoxMassAbsorption.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMassAbsorption.DecimalPlaces = -2;
                        numericBoxMassAbsorption.FooterText = "cm^2/g";
            numericBoxMassAbsorption.Location = new System.Drawing.Point(76, 187);
            numericBoxMassAbsorption.Margin = new System.Windows.Forms.Padding(0);
            numericBoxMassAbsorption.Maximum = 10000D;
            numericBoxMassAbsorption.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxMassAbsorption.Minimum = 0D;
            numericBoxMassAbsorption.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxMassAbsorption.Name = "numericBoxMassAbsorption";
            numericBoxMassAbsorption.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxMassAbsorption.RadianValue = 0.33196162372932142D;
                        numericBoxMassAbsorption.Size = new System.Drawing.Size(97, 25);
            numericBoxMassAbsorption.SkipEventDuringInput = false;
            numericBoxMassAbsorption.SmartIncrement = true;
            numericBoxMassAbsorption.TabIndex = 2;
                        numericBoxMassAbsorption.ThousandsSeparator = true;
            numericBoxMassAbsorption.Value = 19.02D;
                        // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(7, 167);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(107, 34);
            label4.TabIndex = 3;
            label4.Text = "Mass absorption\r\ncoefficient";
            // 
            // checkBoxSaveSimulatedPattern
            // 
            checkBoxSaveSimulatedPattern.AutoSize = true;
            checkBoxSaveSimulatedPattern.Location = new System.Drawing.Point(8, 84);
            checkBoxSaveSimulatedPattern.Name = "checkBoxSaveSimulatedPattern";
            checkBoxSaveSimulatedPattern.Size = new System.Drawing.Size(106, 21);
            checkBoxSaveSimulatedPattern.TabIndex = 10;
            checkBoxSaveSimulatedPattern.Text = "Save patterns";
            checkBoxSaveSimulatedPattern.UseVisualStyleBackColor = true;
            checkBoxSaveSimulatedPattern.CheckedChanged += new System.EventHandler(checkBoxSaveSimulatedPattern_CheckedChanged);
            // 
            // folderBrowserDialog
            // 
            folderBrowserDialog.Description = "Set the output folder";
            // 
            // buttonSetFolder
            // 
            buttonSetFolder.AutoSize = true;
            buttonSetFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonSetFolder.Location = new System.Drawing.Point(3, 0);
            buttonSetFolder.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            buttonSetFolder.Name = "buttonSetFolder";
            buttonSetFolder.Size = new System.Drawing.Size(79, 27);
            buttonSetFolder.TabIndex = 7;
            buttonSetFolder.Text = "Set  folder";
            buttonSetFolder.UseVisualStyleBackColor = true;
            buttonSetFolder.Click += new System.EventHandler(buttonSetFolder_Click);
            // 
            // textBoxFileName
            // 
            textBoxFileName.Location = new System.Drawing.Point(158, 3);
            textBoxFileName.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            textBoxFileName.Name = "textBoxFileName";
            textBoxFileName.Size = new System.Drawing.Size(150, 25);
            textBoxFileName.TabIndex = 14;
            textBoxFileName.Text = "pattern";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(311, 6);
            label7.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(30, 17);
            label7.TabIndex = 3;
            label7.Text = "#.tif";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(85, 6);
            label8.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(70, 17);
            label8.TabIndex = 3;
            label8.Text = "File name: ";
            // 
            // flowLayoutPanelSavePatterns
            // 
            flowLayoutPanelSavePatterns.AutoSize = true;
            flowLayoutPanelSavePatterns.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanelSavePatterns.Controls.Add(buttonSetFolder);
            flowLayoutPanelSavePatterns.Controls.Add(label8);
            flowLayoutPanelSavePatterns.Controls.Add(textBoxFileName);
            flowLayoutPanelSavePatterns.Controls.Add(label7);
            flowLayoutPanelSavePatterns.Enabled = false;
            flowLayoutPanelSavePatterns.Location = new System.Drawing.Point(117, 80);
            flowLayoutPanelSavePatterns.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanelSavePatterns.Name = "flowLayoutPanelSavePatterns";
            flowLayoutPanelSavePatterns.Size = new System.Drawing.Size(341, 28);
            flowLayoutPanelSavePatterns.TabIndex = 15;
            // 
            // groupBoxOutputParameters
            // 
            groupBoxOutputParameters.Controls.Add(flowLayoutPanelOmegaStep);
            groupBoxOutputParameters.Controls.Add(checkBoxOmegaStep);
            groupBoxOutputParameters.Controls.Add(checkBoxSaveSimulatedPattern);
            groupBoxOutputParameters.Controls.Add(numericBoxDivisionOfRotationAngle);
            groupBoxOutputParameters.Controls.Add(numericBoxDivisionOfRotationAxis);
            groupBoxOutputParameters.Controls.Add(flowLayoutPanelSavePatterns);
            groupBoxOutputParameters.Location = new System.Drawing.Point(0, 622);
            groupBoxOutputParameters.Name = "groupBoxOutputParameters";
            groupBoxOutputParameters.Size = new System.Drawing.Size(547, 115);
            groupBoxOutputParameters.TabIndex = 16;
            groupBoxOutputParameters.TabStop = false;
            groupBoxOutputParameters.Text = "Output parameters";
            // 
            // flowLayoutPanelOmegaStep
            // 
            flowLayoutPanelOmegaStep.AutoSize = true;
            flowLayoutPanelOmegaStep.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanelOmegaStep.Controls.Add(numericBoxOmegaStep);
            flowLayoutPanelOmegaStep.Controls.Add(numericBoxOmegaTimes);
            flowLayoutPanelOmegaStep.Enabled = false;
            flowLayoutPanelOmegaStep.Location = new System.Drawing.Point(120, 51);
            flowLayoutPanelOmegaStep.Name = "flowLayoutPanelOmegaStep";
            flowLayoutPanelOmegaStep.Size = new System.Drawing.Size(211, 25);
            flowLayoutPanelOmegaStep.TabIndex = 16;
            // 
            // numericBoxOmegaStep
            // 
                       numericBoxOmegaStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxOmegaStep.DecimalPlaces = 2;
                        numericBoxOmegaStep.FooterText = "° step ×";
            numericBoxOmegaStep.Location = new System.Drawing.Point(0, 0);
            numericBoxOmegaStep.Margin = new System.Windows.Forms.Padding(0);
            numericBoxOmegaStep.Maximum = 360D;
            numericBoxOmegaStep.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxOmegaStep.Minimum = -360D;
            numericBoxOmegaStep.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxOmegaStep.Name = "numericBoxOmegaStep";
            numericBoxOmegaStep.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxOmegaStep.RadianValue = 0.0017453292519943296D;
            numericBoxOmegaStep.ShowPositiveSign = true;
            numericBoxOmegaStep.ShowUpDown = true;
            numericBoxOmegaStep.Size = new System.Drawing.Size(116, 25);
            numericBoxOmegaStep.SkipEventDuringInput = false;
            numericBoxOmegaStep.SmartIncrement = true;
            numericBoxOmegaStep.TabIndex = 2;
                        numericBoxOmegaStep.ThousandsSeparator = true;
            numericBoxOmegaStep.UpDown_Increment = 0.1D;
            numericBoxOmegaStep.Value = 0.1D;
                        // 
            // numericBoxOmegaTimes
            // 
                       numericBoxOmegaTimes.BackColor = System.Drawing.SystemColors.Control;
            numericBoxOmegaTimes.DecimalPlaces = 0;
                        numericBoxOmegaTimes.FooterText = "times";
            numericBoxOmegaTimes.Location = new System.Drawing.Point(116, 0);
            numericBoxOmegaTimes.Margin = new System.Windows.Forms.Padding(0);
            numericBoxOmegaTimes.Maximum = 100D;
            numericBoxOmegaTimes.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxOmegaTimes.Minimum = 1D;
            numericBoxOmegaTimes.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxOmegaTimes.Name = "numericBoxOmegaTimes";
            numericBoxOmegaTimes.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxOmegaTimes.RadianValue = 0.17453292519943295D;
            numericBoxOmegaTimes.ShowUpDown = true;
            numericBoxOmegaTimes.Size = new System.Drawing.Size(95, 25);
            numericBoxOmegaTimes.SkipEventDuringInput = false;
            numericBoxOmegaTimes.SmartIncrement = true;
            numericBoxOmegaTimes.TabIndex = 2;
                        numericBoxOmegaTimes.ThousandsSeparator = true;
            numericBoxOmegaTimes.UpDown_Increment = 0.1D;
            numericBoxOmegaTimes.Value = 10D;
                        // 
            // checkBoxOmegaStep
            // 
            checkBoxOmegaStep.AutoSize = true;
            checkBoxOmegaStep.Location = new System.Drawing.Point(9, 53);
            checkBoxOmegaStep.Name = "checkBoxOmegaStep";
            checkBoxOmegaStep.Size = new System.Drawing.Size(98, 21);
            checkBoxOmegaStep.TabIndex = 10;
            checkBoxOmegaStep.Text = "Increment Ω";
            checkBoxOmegaStep.UseVisualStyleBackColor = true;
            checkBoxOmegaStep.CheckedChanged += new System.EventHandler(checkBoxOmegaStep_CheckedChanged);
            // 
            // numericBoxDivisionOfRotationAngle
            // 
                       numericBoxDivisionOfRotationAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDivisionOfRotationAngle.DecimalPlaces = -2;
                                                numericBoxDivisionOfRotationAngle.HeaderText = "rotation speed";
            numericBoxDivisionOfRotationAngle.Location = new System.Drawing.Point(203, 21);
            numericBoxDivisionOfRotationAngle.Margin = new System.Windows.Forms.Padding(0);
            numericBoxDivisionOfRotationAngle.Maximum = 10000D;
            numericBoxDivisionOfRotationAngle.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxDivisionOfRotationAngle.Minimum = 0D;
            numericBoxDivisionOfRotationAngle.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxDivisionOfRotationAngle.Name = "numericBoxDivisionOfRotationAngle";
            numericBoxDivisionOfRotationAngle.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxDivisionOfRotationAngle.RadianValue = 0.17453292519943295D;
            numericBoxDivisionOfRotationAngle.ShowUpDown = true;
            numericBoxDivisionOfRotationAngle.Size = new System.Drawing.Size(138, 25);
            numericBoxDivisionOfRotationAngle.SkipEventDuringInput = false;
            numericBoxDivisionOfRotationAngle.SmartIncrement = true;
            numericBoxDivisionOfRotationAngle.TabIndex = 2;
                        numericBoxDivisionOfRotationAngle.ThousandsSeparator = true;
            numericBoxDivisionOfRotationAngle.Value = 10D;
                        // 
            // numericBoxDivisionOfRotationAxis
            // 
                       numericBoxDivisionOfRotationAxis.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDivisionOfRotationAxis.DecimalPlaces = -2;
                                                numericBoxDivisionOfRotationAxis.HeaderText = "Division of rotation axis";
            numericBoxDivisionOfRotationAxis.Location = new System.Drawing.Point(6, 21);
            numericBoxDivisionOfRotationAxis.Margin = new System.Windows.Forms.Padding(0);
            numericBoxDivisionOfRotationAxis.Maximum = 360D;
            numericBoxDivisionOfRotationAxis.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxDivisionOfRotationAxis.Minimum = 10D;
            numericBoxDivisionOfRotationAxis.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxDivisionOfRotationAxis.Name = "numericBoxDivisionOfRotationAxis";
            numericBoxDivisionOfRotationAxis.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxDivisionOfRotationAxis.RadianValue = 1.0471975511965976D;
            numericBoxDivisionOfRotationAxis.ShowUpDown = true;
            numericBoxDivisionOfRotationAxis.Size = new System.Drawing.Size(195, 25);
            numericBoxDivisionOfRotationAxis.SkipEventDuringInput = false;
            numericBoxDivisionOfRotationAxis.SmartIncrement = true;
            numericBoxDivisionOfRotationAxis.TabIndex = 2;
                        numericBoxDivisionOfRotationAxis.ThousandsSeparator = true;
            numericBoxDivisionOfRotationAxis.Value = 60D;
                        // 
            // trackBarAdvancedBack
            // 
            trackBarAdvancedBack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            trackBarAdvancedBack.ControlHeight = 26;
            trackBarAdvancedBack.DecimalPlaces = 2;
            trackBarAdvancedBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128))); // 260522Cl 変更: Yu Gothic UI → Segoe UI (英語GUI)
            trackBarAdvancedBack.Location = new System.Drawing.Point(36, 586);
            trackBarAdvancedBack.Margin = new System.Windows.Forms.Padding(0);
            trackBarAdvancedBack.Maximum = 100D;
            trackBarAdvancedBack.Minimum = 0D;
            trackBarAdvancedBack.Name = "trackBarAdvancedBack";
            trackBarAdvancedBack.NumericBoxSize = 60;
            trackBarAdvancedBack.Size = new System.Drawing.Size(517, 26);
            trackBarAdvancedBack.TabIndex = 1;
            trackBarAdvancedBack.Value = 10D;
            trackBarAdvancedBack.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(trackBarAdvancedBack_ValueChanged);
            // 
            // trackBarAdvancedTime
            // 
            trackBarAdvancedTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            trackBarAdvancedTime.ControlHeight = 26;
            trackBarAdvancedTime.DecimalPlaces = 3;
            trackBarAdvancedTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128))); // 260522Cl 変更: Yu Gothic UI → Segoe UI (英語GUI)
            trackBarAdvancedTime.FooterText = "ns";
            trackBarAdvancedTime.HeaderText = "Time";
            trackBarAdvancedTime.Location = new System.Drawing.Point(0, 335);
            trackBarAdvancedTime.Margin = new System.Windows.Forms.Padding(0);
            trackBarAdvancedTime.Maximum = 100D;
            trackBarAdvancedTime.Minimum = 0D;
            trackBarAdvancedTime.Name = "trackBarAdvancedTime";
            trackBarAdvancedTime.NumericBoxSize = 128;
            trackBarAdvancedTime.Size = new System.Drawing.Size(547, 26);
            trackBarAdvancedTime.Smart_Increment = false;
            trackBarAdvancedTime.TabIndex = 1;
            trackBarAdvancedTime.UpDown_Increment = 0.005D;
            trackBarAdvancedTime.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(trackBarAdvancedTime_ValueChanged);
            // 
            // trackBarAdvancedFront
            // 
            trackBarAdvancedFront.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            trackBarAdvancedFront.ControlHeight = 26;
            trackBarAdvancedFront.DecimalPlaces = 2;
            trackBarAdvancedFront.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128))); // 260522Cl 変更: Yu Gothic UI → Segoe UI (英語GUI)
            trackBarAdvancedFront.Location = new System.Drawing.Point(36, 555);
            trackBarAdvancedFront.Margin = new System.Windows.Forms.Padding(0);
            trackBarAdvancedFront.Maximum = 100D;
            trackBarAdvancedFront.Minimum = 0D;
            trackBarAdvancedFront.Name = "trackBarAdvancedFront";
            trackBarAdvancedFront.NumericBoxSize = 60;
            trackBarAdvancedFront.Size = new System.Drawing.Size(517, 26);
            trackBarAdvancedFront.TabIndex = 1;
            trackBarAdvancedFront.ValueChanged += new Crystallography.Controls.TrackBarAdvanced.ValueChangedDelegate(trackBarAdvancedBack_ValueChanged);
            // 
            // graphControl
            // 
            graphControl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            graphControl.GraphTitle = "";
            graphControl.Interpolation = false;
            graphControl.Location = new System.Drawing.Point(72, 365);
            graphControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            graphControl.Name = "graphControl";
            graphControl.Size = new System.Drawing.Size(475, 186);
            graphControl.Smoothing = false;
            graphControl.TabIndex = 0;
            graphControl.UpperPanelFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            graphControl.UnitX = " nm";
            graphControl.UnitY = " MBar";
            graphControl.MousePositionVisible = false;
            graphControl.LinePositionChanged += new Crystallography.Controls.GraphControl.LinePositionChengedEventHandler(graphControl1_LinePositionChanged);
            // 
            // groupBoxCompressionModel
            // 
            groupBoxCompressionModel.Controls.Add(radioButton2019Model);
            groupBoxCompressionModel.Controls.Add(radioButton2018Model);
            groupBoxCompressionModel.Controls.Add(groupBoxSlipPlane);
            groupBoxCompressionModel.Controls.Add(groupBoxReleasedArea);
            groupBoxCompressionModel.Controls.Add(groupBoxCompressedArea);
            groupBoxCompressionModel.Location = new System.Drawing.Point(184, 1);
            groupBoxCompressionModel.Name = "groupBoxCompressionModel";
            groupBoxCompressionModel.Size = new System.Drawing.Size(353, 331);
            groupBoxCompressionModel.TabIndex = 17;
            groupBoxCompressionModel.TabStop = false;
            groupBoxCompressionModel.Text = "Compression && rotation model";
            // 
            // radioButton2019Model
            // 
            radioButton2019Model.AutoSize = true;
            radioButton2019Model.Checked = true;
            radioButton2019Model.Location = new System.Drawing.Point(5, 48);
            radioButton2019Model.Name = "radioButton2019Model";
            radioButton2019Model.Size = new System.Drawing.Size(95, 21);
            radioButton2019Model.TabIndex = 7;
            radioButton2019Model.TabStop = true;
            radioButton2019Model.Text = "2019 model";
            radioButton2019Model.UseVisualStyleBackColor = true;
            radioButton2019Model.CheckedChanged += new System.EventHandler(radioButton2019Model_CheckedChanged);
            // 
            // radioButton2018Model
            // 
            radioButton2018Model.AutoSize = true;
            radioButton2018Model.Location = new System.Drawing.Point(6, 24);
            radioButton2018Model.Name = "radioButton2018Model";
            radioButton2018Model.Size = new System.Drawing.Size(95, 21);
            radioButton2018Model.TabIndex = 7;
            radioButton2018Model.Text = "2018 model";
            radioButton2018Model.UseVisualStyleBackColor = true;
            radioButton2018Model.CheckedChanged += new System.EventHandler(radioButton2019Model_CheckedChanged);
            // 
            // groupBoxSlipPlane
            // 
            groupBoxSlipPlane.Controls.Add(numericBoxSlipPlaneH);
            groupBoxSlipPlane.Controls.Add(numericBoxSlipPlaneK);
            groupBoxSlipPlane.Controls.Add(numericBoxSlipPlaneL);
            groupBoxSlipPlane.Controls.Add(label5);
            groupBoxSlipPlane.Location = new System.Drawing.Point(107, 17);
            groupBoxSlipPlane.Name = "groupBoxSlipPlane";
            groupBoxSlipPlane.Size = new System.Drawing.Size(142, 62);
            groupBoxSlipPlane.TabIndex = 6;
            groupBoxSlipPlane.TabStop = false;
            groupBoxSlipPlane.Text = "Slip plane";
            // 
            // numericBoxSlipPlaneH
            // 
                       numericBoxSlipPlaneH.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSlipPlaneH.DecimalPlaces = -2;
                                                numericBoxSlipPlaneH.HeaderText = "(";
            numericBoxSlipPlaneH.Location = new System.Drawing.Point(3, 33);
            numericBoxSlipPlaneH.Margin = new System.Windows.Forms.Padding(0);
            numericBoxSlipPlaneH.Maximum = 10D;
            numericBoxSlipPlaneH.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxSlipPlaneH.Minimum = -10D;
            numericBoxSlipPlaneH.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxSlipPlaneH.Name = "numericBoxSlipPlaneH";
            numericBoxSlipPlaneH.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxSlipPlaneH.RadianValue = 0.017453292519943295D;
            numericBoxSlipPlaneH.ShowUpDown = true;
            numericBoxSlipPlaneH.Size = new System.Drawing.Size(50, 25);
            numericBoxSlipPlaneH.SkipEventDuringInput = false;
            numericBoxSlipPlaneH.TabIndex = 2;
                        numericBoxSlipPlaneH.ThousandsSeparator = true;
            numericBoxSlipPlaneH.Value = 1D;
                        // 
            // numericBoxSlipPlaneK
            // 
                       numericBoxSlipPlaneK.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSlipPlaneK.DecimalPlaces = -2;
            numericBoxSlipPlaneK.Location = new System.Drawing.Point(53, 33);
            numericBoxSlipPlaneK.Margin = new System.Windows.Forms.Padding(0);
            numericBoxSlipPlaneK.Maximum = 10D;
            numericBoxSlipPlaneK.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxSlipPlaneK.Minimum = -10D;
            numericBoxSlipPlaneK.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxSlipPlaneK.Name = "numericBoxSlipPlaneK";
            numericBoxSlipPlaneK.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxSlipPlaneK.ShowUpDown = true;
            numericBoxSlipPlaneK.Size = new System.Drawing.Size(38, 25);
            numericBoxSlipPlaneK.SkipEventDuringInput = false;
            numericBoxSlipPlaneK.TabIndex = 2;
                        numericBoxSlipPlaneK.ThousandsSeparator = true;
                                    // 
            // numericBoxSlipPlaneL
            // 
                       numericBoxSlipPlaneL.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSlipPlaneL.DecimalPlaces = -2;
                        numericBoxSlipPlaneL.FooterText = ")";
            numericBoxSlipPlaneL.Location = new System.Drawing.Point(91, 33);
            numericBoxSlipPlaneL.Margin = new System.Windows.Forms.Padding(0);
            numericBoxSlipPlaneL.Maximum = 1000D;
            numericBoxSlipPlaneL.MaximumSize = new System.Drawing.Size(1000, 25);
            numericBoxSlipPlaneL.Minimum = 0D;
            numericBoxSlipPlaneL.MinimumSize = new System.Drawing.Size(1, 25);
                       numericBoxSlipPlaneL.Name = "numericBoxSlipPlaneL";
            numericBoxSlipPlaneL.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            numericBoxSlipPlaneL.ShowUpDown = true;
            numericBoxSlipPlaneL.Size = new System.Drawing.Size(50, 25);
            numericBoxSlipPlaneL.SkipEventDuringInput = false;
            numericBoxSlipPlaneL.TabIndex = 2;
                        numericBoxSlipPlaneL.ThousandsSeparator = true;
                                    // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(19, 16);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(88, 17);
            label5.TabIndex = 3;
            label5.Text = "h        k        l";
            // 
            // FormDiffractionSimulatorDynamicCompression
            // 
            AllowDrop = true;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F); // 260329Cl 変更: Font→Dpi, 96dpi基準に統一
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(556, 792);
            Controls.Add(groupBoxCompressionModel);
            Controls.Add(groupBoxOutputParameters);
            Controls.Add(groupBoxSampleParameters);
            Controls.Add(statusStrip1);
            Controls.Add(buttonSimulate);
            Controls.Add(checkBoxSkipDrawing);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(trackBarAdvancedBack);
            Controls.Add(trackBarAdvancedTime);
            Controls.Add(trackBarAdvancedFront);
            Controls.Add(graphControl);
            DoubleBuffered = true;
            Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormDiffractionSimulatorDynamicCompression";
            Text = "Dynamic Compression";
            FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormDiffractionSimulatorDynamicCompression_FormClosing);
            Load += new System.EventHandler(FormDiffractionSimulatorDynamicCompression_Load);
            DragDrop += new System.Windows.Forms.DragEventHandler(FormDiffractionSimulatorDynamicCompression_DragDrop);
            DragEnter += new System.Windows.Forms.DragEventHandler(FormDiffractionSimulatorDynamicCompression_DragEnter);
            groupBoxCompressedRotation.ResumeLayout(false);
            groupBoxReleasedRotation.ResumeLayout(false);
            groupBoxEOS.ResumeLayout(false);
            groupBoxShockedPlane.ResumeLayout(false);
            groupBoxShockedPlane.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBoxCompressedArea.ResumeLayout(false);
            groupBoxCompressedArea.PerformLayout();
            groupBoxReleasedArea.ResumeLayout(false);
            groupBoxReleasedArea.PerformLayout();
            groupBoxSampleParameters.ResumeLayout(false);
            groupBoxSampleParameters.PerformLayout();
            flowLayoutPanelSavePatterns.ResumeLayout(false);
            flowLayoutPanelSavePatterns.PerformLayout();
            groupBoxOutputParameters.ResumeLayout(false);
            groupBoxOutputParameters.PerformLayout();
            flowLayoutPanelOmegaStep.ResumeLayout(false);
            groupBoxCompressionModel.ResumeLayout(false);
            groupBoxCompressionModel.PerformLayout();
            groupBoxSlipPlane.ResumeLayout(false);
            groupBoxSlipPlane.PerformLayout();
            // 260530Cl 追加: ツールチップ (SetToolTip方式)
            toolTip.SetToolTip(buttonSimulate, "Run the dynamic-compression diffraction simulation and accumulate the pattern.");
            toolTip.SetToolTip(checkBoxSkipDrawing, "When checked, skips on-screen drawing during the calculation to speed it up.");
            toolTip.SetToolTip(label2, "Depth of the front boundary separating the uncompressed and compressed regions.");
            toolTip.SetToolTip(label3, "Depth of the back boundary separating the compressed and released regions.");
            toolTip.SetToolTip(buttonSetFolder, "Choose the output folder where simulated TIFF patterns are saved.");
            toolTip.SetToolTip(textBoxFileName, "Base file name for the saved TIFF pattern; '.tif' is appended automatically.");
            toolTip.SetToolTip(label8, "Base file name for the saved TIFF pattern; '.tif' is appended automatically.");
            toolTip.SetToolTip(numericBoxCompressedOmega, "Mean crystal rotation rate ω in the compressed region (°/ns).");
            toolTip.SetToolTip(numericBoxCompressedOmegaSigma, "Standard deviation σ_ω of the rotation rate in the compressed region (°/ns).");
            toolTip.SetToolTip(numericBoxCompressedThetaA, "Constant term a (°) of the rotation-axis spread σ_θ for the compressed region (2019 model).");
            toolTip.SetToolTip(numericBoxCompressedThetaB, "Time coefficient b (°/ns) of the rotation-axis spread σ_θ for the compressed region (2019 model).");
            toolTip.SetToolTip(checkBoxOmegaStep, "When checked, repeats the simulation while incrementing the overall Ω angle by a fixed step.");
            toolTip.SetToolTip(checkBoxSaveSimulatedPattern, "When checked, saves each simulated pattern as a TIFF file to the chosen folder.");
            toolTip.SetToolTip(numericBoxDivisionOfRotationAngle, "Number of sampling divisions for integrating over the rotation-rate (speed) distribution.");
            toolTip.SetToolTip(numericBoxDivisionOfRotationAxis, "Number of azimuthal sampling divisions for the rotation axis.");
            toolTip.SetToolTip(numericBoxReleasedOmega, "Mean crystal rotation rate ω in the released region (°/ns).");
            toolTip.SetToolTip(numericBoxReleasedOmegaSigma, "Standard deviation σ_ω of the rotation rate in the released region (°/ns).");
            toolTip.SetToolTip(numericBoxReleasedThetaA, "Constant term a (°) of the rotation-axis spread σ_θ for the released region (2019 model).");
            toolTip.SetToolTip(numericBoxReleasedThetaB, "Time coefficient b (°/ns) of the rotation-axis spread σ_θ for the released region (2019 model).");
            toolTip.SetToolTip(numericBoxShockedPlaneH, "Miller index h of the shocked (compression) plane normal.");
            toolTip.SetToolTip(numericBoxShockedPlaneK, "Miller index k of the shocked (compression) plane normal.");
            toolTip.SetToolTip(numericBoxShockedPlaneL, "Miller index l of the shocked (compression) plane normal.");
            toolTip.SetToolTip(numericBoxSlipPlaneH, "Miller index h of the slip-plane normal (used to define the rotation axis in the 2019 model).");
            toolTip.SetToolTip(numericBoxSlipPlaneK, "Miller index k of the slip-plane normal (used to define the rotation axis in the 2019 model).");
            toolTip.SetToolTip(numericBoxSlipPlaneL, "Miller index l of the slip-plane normal (used to define the rotation axis in the 2019 model).");
            toolTip.SetToolTip(numericBoxUp, "Shock (compression) wave speed Us (km/s) used to convert depth into elapsed time in the compressed region.");
            toolTip.SetToolTip(radioButtonCompressedIsotropic, "Apply isotropic (hydrostatic) compression of the lattice in the compressed region.");
            toolTip.SetToolTip(radioButtonCompressedUniaxial, "Apply uniaxial compression of the lattice along the shocked-plane normal in the compressed region.");
            toolTip.SetToolTip(numericBoxUr, "Release wave speed Ur (km/s) used to convert depth into elapsed time in the released region.");
            toolTip.SetToolTip(radioButtonReleasedIsotropic, "Apply isotropic (hydrostatic) compression of the lattice in the released region.");
            toolTip.SetToolTip(radioButtonReleasedUniaxial, "Apply uniaxial compression of the lattice along the shocked-plane normal in the released region.");
            toolTip.SetToolTip(numericBoxOmegaStep, "Ω angle increment (°) applied per repeat when 'Increment Ω' is enabled.");
            toolTip.SetToolTip(numericBoxOmegaTimes, "Number of repeats when 'Increment Ω' is enabled.");
            toolTip.SetToolTip(radioButton2018Model, "Use the 2018 rotation model (rotation axes perpendicular to the compression axis).");
            toolTip.SetToolTip(radioButton2019Model, "Use the 2019 rotation model (rotation axis from the slip-plane / compression-plane cross product, with θ,φ spread).");
            toolTip.SetToolTip(numericBoxEOS_K0, "Bulk modulus K0 (GPa) for the third-order Birch–Murnaghan equation of state.");
            toolTip.SetToolTip(numericBoxEOS_Kprime, "Pressure derivative of the bulk modulus K'0 for the third-order Birch–Murnaghan equation of state.");
            toolTip.SetToolTip(numericBoxMassAbsorption, "Mass absorption coefficient (cm²/g) of the sample used to attenuate intensities through the layers.");
            toolTip.SetToolTip(label4, "Mass absorption coefficient (cm²/g) of the sample used to attenuate intensities through the layers.");
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Crystallography.Controls.GraphControl graphControl;
        private Crystallography.Controls.TrackBarAdvanced trackBarAdvancedFront;
        private Crystallography.Controls.TrackBarAdvanced trackBarAdvancedBack;
        private Crystallography.Controls.NumericBox numericBoxShockedPlaneH;
        private Crystallography.Controls.NumericBox numericBoxShockedPlaneK;
        private Crystallography.Controls.NumericBox numericBoxShockedPlaneL;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private Crystallography.Controls.NumericBox numericBoxEOS_K0;
        private Crystallography.Controls.NumericBox numericBoxEOS_Kprime;
        private Crystallography.Controls.NumericBox numericBoxCompressedOmega;
        private Crystallography.Controls.NumericBox numericBoxCompressedOmegaSigma;
        private System.Windows.Forms.GroupBox groupBoxCompressedRotation;
        private System.Windows.Forms.GroupBox groupBoxReleasedRotation;
        private Crystallography.Controls.NumericBox numericBoxReleasedOmegaSigma;
        private Crystallography.Controls.NumericBox numericBoxReleasedOmega;
        private System.Windows.Forms.GroupBox groupBoxEOS;
        private System.Windows.Forms.GroupBox groupBoxShockedPlane;
        private System.Windows.Forms.Button buttonSimulate;
        private Crystallography.Controls.NumericBox numericBoxUp;
        private Crystallography.Controls.NumericBox numericBoxUr;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox checkBoxSkipDrawing;
        private Crystallography.Controls.NumericBox numericBoxDivisionOfRotationAngle;
        private Crystallography.Controls.NumericBox numericBoxDivisionOfRotationAxis;
        private System.Windows.Forms.GroupBox groupBoxCompressedArea;
        private System.Windows.Forms.RadioButton radioButtonCompressedIsotropic;
        private System.Windows.Forms.RadioButton radioButtonCompressedUniaxial;
        private System.Windows.Forms.GroupBox groupBoxReleasedArea;
        private System.Windows.Forms.RadioButton radioButtonReleasedIsotropic;
        private System.Windows.Forms.RadioButton radioButtonReleasedUniaxial;
        private System.Windows.Forms.GroupBox groupBoxSampleParameters;
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
        private System.Windows.Forms.GroupBox groupBoxOutputParameters;
        private Crystallography.Controls.NumericBox numericBoxOmegaStep;
        private System.Windows.Forms.CheckBox checkBoxOmegaStep;
        private Crystallography.Controls.NumericBox numericBoxOmegaTimes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOmegaStep;
        private Crystallography.Controls.NumericBox numericBoxCompressedThetaB;
        private Crystallography.Controls.NumericBox numericBoxCompressedThetaA;
        private Crystallography.Controls.NumericBox numericBoxReleasedThetaB;
        private Crystallography.Controls.NumericBox numericBoxReleasedThetaA;
        private System.Windows.Forms.GroupBox groupBoxCompressionModel;
        private System.Windows.Forms.RadioButton radioButton2019Model;
        private System.Windows.Forms.RadioButton radioButton2018Model;
        private System.Windows.Forms.GroupBox groupBoxSlipPlane;
        private Crystallography.Controls.NumericBox numericBoxSlipPlaneH;
        private Crystallography.Controls.NumericBox numericBoxSlipPlaneK;
        private Crystallography.Controls.NumericBox numericBoxSlipPlaneL;
        private System.Windows.Forms.Label label5;
    }
}