namespace ReciPro
{
    partial class FormSpotIDv1
    {
        /// <summary>必要なデザイナ変数です。</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>使用中のリソースをすべてクリーンアップします。</summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        // (260323Ch) renamed numeric container controls:
        // groupBox1 -> groupBoxTEMCondition
        // groupBox2 -> groupBoxPhoto1
        // groupBox3 -> groupBoxPhoto1HolderCondition
        // groupBox4 -> groupBoxPhoto1Pattern
        // groupBox6 -> groupBoxPhoto2HolderCondition
        // groupBox7 -> groupBoxPhoto2Pattern
        // groupBox9 -> groupBoxPhoto3HolderCondition
        // groupBox10 -> groupBoxPhoto3Pattern
        // flowLayoutPanel1 -> flowLayoutPanelPhotos
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container(); // (260531Ch)
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSpotIDv1));
            groupBoxPhoto1Pattern = new System.Windows.Forms.GroupBox();
            buttonSearchPhoto1 = new System.Windows.Forms.Button();
            numericBoxP1Theta = new NumericBox();
            pictureBoxPhoto1 = new System.Windows.Forms.PictureBox();
            radioButtonPhoto1Mode2 = new System.Windows.Forms.RadioButton();
            radioButtonPhoto1Mode1 = new System.Windows.Forms.RadioButton();
            numericBoxPhoto1L1Err = new Crystallography.Controls.NumericBox();
            inputBoxP1L3 = new InputBox();
            numericBoxPhoto1L2Err = new Crystallography.Controls.NumericBox();
            inputBoxP1L2 = new InputBox();
            numericBoxPhoto1ThetaErr = new Crystallography.Controls.NumericBox();
            inputBoxP1L1 = new InputBox();
            label4 = new System.Windows.Forms.Label();
            labelPhoto1Mode1_4 = new System.Windows.Forms.Label();
            label131 = new System.Windows.Forms.Label();
            numericBoxPhoto1L3Err = new Crystallography.Controls.NumericBox();
            labelPhoto1Mode2_6 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            labelPhoto1Mode1_1 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label75 = new System.Windows.Forms.Label();
            label71 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label70 = new System.Windows.Forms.Label();
            label69 = new System.Windows.Forms.Label();
            labelPhoto1Mode1_3 = new System.Windows.Forms.Label();
            groupBoxTEMCondition = new System.Windows.Forms.GroupBox();
            textBoxWaveLength = new System.Windows.Forms.TextBox();
            label67 = new System.Windows.Forms.Label();
            numericBoxCamaraLength = new Crystallography.Controls.NumericBox();
            label18 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            numericBoxAccVol = new Crystallography.Controls.NumericBox();
            label19 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label68 = new System.Windows.Forms.Label();
            groupBoxPhoto1 = new System.Windows.Forms.GroupBox();
            groupBoxPhoto1HolderCondition = new System.Windows.Forms.GroupBox();
            numericBoxP1Tilt2 = new NumericBox();
            numericBoxP1Tilt1 = new NumericBox();
            label14 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            numericBoxPhoto1Tilt1Err = new Crystallography.Controls.NumericBox();
            label48 = new System.Windows.Forms.Label();
            numericBoxPhoto1Tilt2Err = new Crystallography.Controls.NumericBox();
            label49 = new System.Windows.Forms.Label();
            label53 = new System.Windows.Forms.Label();
            label54 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            panel4 = new System.Windows.Forms.Panel();
            label59 = new System.Windows.Forms.Label();
            textBoxAngleBetween12 = new System.Windows.Forms.TextBox();
            textBoxAngleBetween31 = new System.Windows.Forms.TextBox();
            label176 = new System.Windows.Forms.Label();
            textBoxAngleBetween23 = new System.Windows.Forms.TextBox();
            panel2 = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            groupBoxPhoto2Pattern = new System.Windows.Forms.GroupBox();
            buttonSearchPhoto2 = new System.Windows.Forms.Button();
            numericBoxP2Theta = new NumericBox();
            pictureBoxPhoto2 = new System.Windows.Forms.PictureBox();
            inputBoxP2L3 = new InputBox();
            radioButtonPhoto2Mode2 = new System.Windows.Forms.RadioButton();
            inputBoxP2L2 = new InputBox();
            radioButtonPhoto2Mode1 = new System.Windows.Forms.RadioButton();
            inputBoxP2L1 = new InputBox();
            numericBoxPhoto2L1Err = new Crystallography.Controls.NumericBox();
            numericBoxPhoto2L2Err = new Crystallography.Controls.NumericBox();
            numericBoxPhoto2ThetaErr = new Crystallography.Controls.NumericBox();
            label22 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            label24 = new System.Windows.Forms.Label();
            numericBoxPhoto2L3Err = new Crystallography.Controls.NumericBox();
            label26 = new System.Windows.Forms.Label();
            label27 = new System.Windows.Forms.Label();
            label28 = new System.Windows.Forms.Label();
            label29 = new System.Windows.Forms.Label();
            label30 = new System.Windows.Forms.Label();
            label31 = new System.Windows.Forms.Label();
            label79 = new System.Windows.Forms.Label();
            label76 = new System.Windows.Forms.Label();
            label77 = new System.Windows.Forms.Label();
            label78 = new System.Windows.Forms.Label();
            groupBoxPhoto2HolderCondition = new System.Windows.Forms.GroupBox();
            numericBoxP2Tilt2 = new NumericBox();
            numericBoxP2Tilt1 = new NumericBox();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            numericBoxPhoto2Tilt1Err = new Crystallography.Controls.NumericBox();
            label8 = new System.Windows.Forms.Label();
            numericBoxPhoto2Tilt2Err = new Crystallography.Controls.NumericBox();
            label13 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            groupBoxPhoto2 = new System.Windows.Forms.GroupBox();
            groupBoxPhoto3Pattern = new System.Windows.Forms.GroupBox();
            inputBoxP3L3 = new InputBox();
            numericBoxP3Theta = new NumericBox();
            inputBoxP3L2 = new InputBox();
            buttonSearchPhoto3 = new System.Windows.Forms.Button();
            inputBoxP3L1 = new InputBox();
            pictureBoxPhoto3 = new System.Windows.Forms.PictureBox();
            radioButtonPhoto3Mode2 = new System.Windows.Forms.RadioButton();
            radioButtonPhoto3Mode1 = new System.Windows.Forms.RadioButton();
            numericBoxPhoto3L1Err = new Crystallography.Controls.NumericBox();
            numericBoxPhoto3L2Err = new Crystallography.Controls.NumericBox();
            numericBoxPhoto3ThetaErr = new Crystallography.Controls.NumericBox();
            label42 = new System.Windows.Forms.Label();
            label43 = new System.Windows.Forms.Label();
            label44 = new System.Windows.Forms.Label();
            numericBoxPhoto3L3Err = new Crystallography.Controls.NumericBox();
            label46 = new System.Windows.Forms.Label();
            label47 = new System.Windows.Forms.Label();
            label52 = new System.Windows.Forms.Label();
            label55 = new System.Windows.Forms.Label();
            label56 = new System.Windows.Forms.Label();
            label57 = new System.Windows.Forms.Label();
            label101 = new System.Windows.Forms.Label();
            label100 = new System.Windows.Forms.Label();
            label99 = new System.Windows.Forms.Label();
            label98 = new System.Windows.Forms.Label();
            groupBoxPhoto3HolderCondition = new System.Windows.Forms.GroupBox();
            numericBoxP3Tilt2 = new NumericBox();
            label34 = new System.Windows.Forms.Label();
            numericBoxP3Tilt1 = new NumericBox();
            label35 = new System.Windows.Forms.Label();
            numericBoxPhoto3Tilt1Err = new Crystallography.Controls.NumericBox();
            label36 = new System.Windows.Forms.Label();
            numericBoxPhoto3Tilt2Err = new Crystallography.Controls.NumericBox();
            label37 = new System.Windows.Forms.Label();
            label38 = new System.Windows.Forms.Label();
            label39 = new System.Windows.Forms.Label();
            label32 = new System.Windows.Forms.Label();
            label33 = new System.Windows.Forms.Label();
            groupBoxPhoto3 = new System.Windows.Forms.GroupBox();
            checkBoxPhoto2 = new System.Windows.Forms.CheckBox();
            checkBoxPhoto3 = new System.Windows.Forms.CheckBox();
            checkBoxEquivalentPhoto1L1Photo2L1 = new System.Windows.Forms.CheckBox();
            checkBoxEquivalentPhoto2L1Photo3L1 = new System.Windows.Forms.CheckBox();
            checkBoxEquivalentPhoto2L2Photo3L2 = new System.Windows.Forms.CheckBox();
            buttonSearchAll = new System.Windows.Forms.Button();
            panel5 = new System.Windows.Forms.Panel();
            checkBoxEquivalentPhoto1L2Photo2L2 = new System.Windows.Forms.CheckBox();
            label58 = new System.Windows.Forms.Label();
            flowLayoutPanelPhotos = new System.Windows.Forms.FlowLayoutPanel();
            panelPhoto2 = new System.Windows.Forms.Panel();
            panel6 = new System.Windows.Forms.Panel();
            panel8 = new System.Windows.Forms.Panel();
            panelPhoto3 = new System.Windows.Forms.Panel();
            panel9 = new System.Windows.Forms.Panel();
            groupBoxPhoto1Pattern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto1).BeginInit();
            groupBoxTEMCondition.SuspendLayout();
            groupBoxPhoto1.SuspendLayout();
            groupBoxPhoto1HolderCondition.SuspendLayout();
            panel4.SuspendLayout();
            groupBoxPhoto2Pattern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto2).BeginInit();
            groupBoxPhoto2HolderCondition.SuspendLayout();
            groupBoxPhoto2.SuspendLayout();
            groupBoxPhoto3Pattern.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto3).BeginInit();
            groupBoxPhoto3HolderCondition.SuspendLayout();
            groupBoxPhoto3.SuspendLayout();
            flowLayoutPanelPhotos.SuspendLayout();
            panelPhoto2.SuspendLayout();
            panelPhoto3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxPhoto1Pattern
            // 
            groupBoxPhoto1Pattern.Controls.Add(buttonSearchPhoto1);
            groupBoxPhoto1Pattern.Controls.Add(numericBoxP1Theta);
            groupBoxPhoto1Pattern.Controls.Add(pictureBoxPhoto1);
            groupBoxPhoto1Pattern.Controls.Add(radioButtonPhoto1Mode2);
            groupBoxPhoto1Pattern.Controls.Add(radioButtonPhoto1Mode1);
            groupBoxPhoto1Pattern.Controls.Add(numericBoxPhoto1L1Err);
            groupBoxPhoto1Pattern.Controls.Add(inputBoxP1L3);
            groupBoxPhoto1Pattern.Controls.Add(numericBoxPhoto1L2Err);
            groupBoxPhoto1Pattern.Controls.Add(inputBoxP1L2);
            groupBoxPhoto1Pattern.Controls.Add(numericBoxPhoto1ThetaErr);
            groupBoxPhoto1Pattern.Controls.Add(inputBoxP1L1);
            groupBoxPhoto1Pattern.Controls.Add(label4);
            groupBoxPhoto1Pattern.Controls.Add(labelPhoto1Mode1_4);
            groupBoxPhoto1Pattern.Controls.Add(label131);
            groupBoxPhoto1Pattern.Controls.Add(numericBoxPhoto1L3Err);
            groupBoxPhoto1Pattern.Controls.Add(labelPhoto1Mode2_6);
            groupBoxPhoto1Pattern.Controls.Add(label20);
            groupBoxPhoto1Pattern.Controls.Add(labelPhoto1Mode1_1);
            groupBoxPhoto1Pattern.Controls.Add(label12);
            groupBoxPhoto1Pattern.Controls.Add(label75);
            groupBoxPhoto1Pattern.Controls.Add(label71);
            groupBoxPhoto1Pattern.Controls.Add(label11);
            groupBoxPhoto1Pattern.Controls.Add(label70);
            groupBoxPhoto1Pattern.Controls.Add(label69);
            groupBoxPhoto1Pattern.Controls.Add(labelPhoto1Mode1_3);
            resources.ApplyResources(groupBoxPhoto1Pattern, "groupBoxPhoto1Pattern");
            groupBoxPhoto1Pattern.Name = "groupBoxPhoto1Pattern";
            groupBoxPhoto1Pattern.TabStop = false;
            // 
            // buttonSearchPhoto1
            // 
            resources.ApplyResources(buttonSearchPhoto1, "buttonSearchPhoto1");
            toolTip.SetToolTip(buttonSearchPhoto1, resources.GetString("buttonSearchPhoto1.ToolTip")); // 260531Cl
            buttonSearchPhoto1.Name = "buttonSearchPhoto1";
            buttonSearchPhoto1.BackColor = System.Drawing.Color.SteelBlue; // 260520Cl 追加: 主要アクション(検索)を水色に統一
            buttonSearchPhoto1.ForeColor = System.Drawing.Color.White; // 260520Cl 追加
            buttonSearchPhoto1.UseVisualStyleBackColor = false; // 260520Cl 変更: BackColor有効化のため
            buttonSearchPhoto1.Click += buttonSearch_Click;
            // 
            // numericBoxP1Theta
            // 
            numericBoxP1Theta.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxP1Theta, "numericBoxP1Theta");
            toolTip.SetToolTip(numericBoxP1Theta, resources.GetString("numericBoxP1Theta.ToolTip")); // 260531Cl
            numericBoxP1Theta.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxP1Theta.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxP1Theta.Name = "numericBoxP1Theta";
            numericBoxP1Theta.SkipEventDuringInput = false;
            numericBoxP1Theta.SmartIncrement = true;
            numericBoxP1Theta.ThousandsSeparator = true;
            numericBoxP1Theta.ValueChanged += textBox_TextChanged;
            numericBoxP1Theta.Click2 += numericBoxP1Theta_Click2;
            // 
            // pictureBoxPhoto1
            // 
            pictureBoxPhoto1.BackColor = System.Drawing.SystemColors.Window;
            pictureBoxPhoto1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(pictureBoxPhoto1, "pictureBoxPhoto1");
            pictureBoxPhoto1.Name = "pictureBoxPhoto1";
            pictureBoxPhoto1.TabStop = false;
            // 
            // radioButtonPhoto1Mode2
            // 
            resources.ApplyResources(radioButtonPhoto1Mode2, "radioButtonPhoto1Mode2");
            toolTip.SetToolTip(radioButtonPhoto1Mode2, resources.GetString("radioButtonPhoto1Mode2.ToolTip")); // 260531Cl
            radioButtonPhoto1Mode2.Name = "radioButtonPhoto1Mode2";
            radioButtonPhoto1Mode2.CheckedChanged += radioButtonPhoto1Mode1_CheckedChanged;
            // 
            // radioButtonPhoto1Mode1
            // 
            resources.ApplyResources(radioButtonPhoto1Mode1, "radioButtonPhoto1Mode1");
            toolTip.SetToolTip(radioButtonPhoto1Mode1, resources.GetString("radioButtonPhoto1Mode1.ToolTip")); // 260531Cl
            radioButtonPhoto1Mode1.Checked = true;
            radioButtonPhoto1Mode1.Name = "radioButtonPhoto1Mode1";
            radioButtonPhoto1Mode1.TabStop = true;
            // 
            // numericBoxPhoto1L1Err
            // 
            resources.ApplyResources(numericBoxPhoto1L1Err, "numericBoxPhoto1L1Err");
            toolTip.SetToolTip(numericBoxPhoto1L1Err, resources.GetString("numericBoxPhoto1L1Err.ToolTip")); // 260531Cl
            numericBoxPhoto1L1Err.Maximum = 50D;
            numericBoxPhoto1L1Err.Name = "numericBoxPhoto1L1Err";
            numericBoxPhoto1L1Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center; // 260522Cl 追加: 旧 NumericUpDown.TextAlign=Center を ValueTextAlign で再現
            numericBoxPhoto1L1Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定(Minimum=0,DecimalPlaces=0,スピンボタン表示)を保持
            numericBoxPhoto1L1Err.DecimalPlaces = 0;
            numericBoxPhoto1L1Err.ShowUpDown = true;
            numericBoxPhoto1L1Err.Value = 5D;
            // 
            // inputBoxP1L3
            // 
            resources.ApplyResources(inputBoxP1L3, "inputBoxP1L3");
            inputBoxP1L3.CameraLength = 0D;
            inputBoxP1L3.Length = 0D;
            inputBoxP1L3.Name = "inputBoxP1L3";
            inputBoxP1L3.WaveLength = 0D;
            inputBoxP1L3.ValueChanged += textBox_TextChanged;
            inputBoxP1L3.Click2 += inputBoxP1L3_Click;
            // 
            // numericBoxPhoto1L2Err
            // 
            resources.ApplyResources(numericBoxPhoto1L2Err, "numericBoxPhoto1L2Err");
            toolTip.SetToolTip(numericBoxPhoto1L2Err, resources.GetString("numericBoxPhoto1L2Err.ToolTip")); // 260531Cl
            numericBoxPhoto1L2Err.Maximum = 50D;
            numericBoxPhoto1L2Err.Name = "numericBoxPhoto1L2Err";
            numericBoxPhoto1L2Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto1L2Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto1L2Err.DecimalPlaces = 0;
            numericBoxPhoto1L2Err.ShowUpDown = true;
            numericBoxPhoto1L2Err.Value = 5D;
            // 
            // inputBoxP1L2
            // 
            resources.ApplyResources(inputBoxP1L2, "inputBoxP1L2");
            inputBoxP1L2.CameraLength = 0D;
            inputBoxP1L2.Length = 0D;
            inputBoxP1L2.Name = "inputBoxP1L2";
            inputBoxP1L2.WaveLength = 0D;
            inputBoxP1L2.ValueChanged += textBox_TextChanged;
            // 
            // numericBoxPhoto1ThetaErr
            // 
            resources.ApplyResources(numericBoxPhoto1ThetaErr, "numericBoxPhoto1ThetaErr");
            toolTip.SetToolTip(numericBoxPhoto1ThetaErr, resources.GetString("numericBoxPhoto1ThetaErr.ToolTip")); // 260531Cl
            numericBoxPhoto1ThetaErr.Maximum = 30D;
            numericBoxPhoto1ThetaErr.Name = "numericBoxPhoto1ThetaErr";
            numericBoxPhoto1ThetaErr.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto1ThetaErr.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto1ThetaErr.DecimalPlaces = 0;
            numericBoxPhoto1ThetaErr.ShowUpDown = true;
            numericBoxPhoto1ThetaErr.Value = 3D;
            // 
            // inputBoxP1L1
            // 
            resources.ApplyResources(inputBoxP1L1, "inputBoxP1L1");
            inputBoxP1L1.CameraLength = 0D;
            inputBoxP1L1.Length = 0D;
            inputBoxP1L1.Name = "inputBoxP1L1";
            inputBoxP1L1.WaveLength = 0D;
            inputBoxP1L1.ValueChanged += textBox_TextChanged;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip")); // 260531Cl
            label4.Name = "label4";
            // 
            // labelPhoto1Mode1_4
            // 
            resources.ApplyResources(labelPhoto1Mode1_4, "labelPhoto1Mode1_4");
            toolTip.SetToolTip(labelPhoto1Mode1_4, resources.GetString("labelPhoto1Mode1_4.ToolTip")); // 260531Cl
            labelPhoto1Mode1_4.Name = "labelPhoto1Mode1_4";
            // 
            // label131
            // 
            resources.ApplyResources(label131, "label131");
            toolTip.SetToolTip(label131, resources.GetString("label131.ToolTip")); // 260531Cl
            label131.Name = "label131";
            // 
            // numericBoxPhoto1L3Err
            // 
            resources.ApplyResources(numericBoxPhoto1L3Err, "numericBoxPhoto1L3Err");
            toolTip.SetToolTip(numericBoxPhoto1L3Err, resources.GetString("numericBoxPhoto1L3Err.ToolTip")); // 260531Cl
            numericBoxPhoto1L3Err.Maximum = 50D;
            numericBoxPhoto1L3Err.Name = "numericBoxPhoto1L3Err";
            numericBoxPhoto1L3Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto1L3Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto1L3Err.DecimalPlaces = 0;
            numericBoxPhoto1L3Err.ShowUpDown = true;
            numericBoxPhoto1L3Err.Value = 5D;
            // 
            // labelPhoto1Mode2_6
            // 
            resources.ApplyResources(labelPhoto1Mode2_6, "labelPhoto1Mode2_6");
            toolTip.SetToolTip(labelPhoto1Mode2_6, resources.GetString("labelPhoto1Mode2_6.ToolTip")); // 260531Cl
            labelPhoto1Mode2_6.Name = "labelPhoto1Mode2_6";
            // 
            // label20
            // 
            resources.ApplyResources(label20, "label20");
            toolTip.SetToolTip(label20, resources.GetString("label20.ToolTip")); // 260531Cl
            label20.Name = "label20";
            // 
            // labelPhoto1Mode1_1
            // 
            resources.ApplyResources(labelPhoto1Mode1_1, "labelPhoto1Mode1_1");
            toolTip.SetToolTip(labelPhoto1Mode1_1, resources.GetString("labelPhoto1Mode1_1.ToolTip")); // 260531Cl
            labelPhoto1Mode1_1.Name = "labelPhoto1Mode1_1";
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            toolTip.SetToolTip(label12, resources.GetString("label12.ToolTip")); // 260531Cl
            label12.Name = "label12";
            // 
            // label75
            // 
            resources.ApplyResources(label75, "label75");
            toolTip.SetToolTip(label75, resources.GetString("label75.ToolTip")); // 260531Cl
            label75.Name = "label75";
            // 
            // label71
            // 
            resources.ApplyResources(label71, "label71");
            toolTip.SetToolTip(label71, resources.GetString("label71.ToolTip")); // 260531Cl
            label71.Name = "label71";
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip")); // 260531Cl
            label11.Name = "label11";
            // 
            // label70
            // 
            resources.ApplyResources(label70, "label70");
            toolTip.SetToolTip(label70, resources.GetString("label70.ToolTip")); // 260531Cl
            label70.Name = "label70";
            // 
            // label69
            // 
            resources.ApplyResources(label69, "label69");
            toolTip.SetToolTip(label69, resources.GetString("label69.ToolTip")); // 260531Cl
            label69.Name = "label69";
            // 
            // labelPhoto1Mode1_3
            // 
            resources.ApplyResources(labelPhoto1Mode1_3, "labelPhoto1Mode1_3");
            toolTip.SetToolTip(labelPhoto1Mode1_3, resources.GetString("labelPhoto1Mode1_3.ToolTip")); // 260531Cl
            labelPhoto1Mode1_3.Name = "labelPhoto1Mode1_3";
            // 
            // groupBoxTEMCondition
            // 
            groupBoxTEMCondition.Controls.Add(textBoxWaveLength);
            groupBoxTEMCondition.Controls.Add(label67);
            groupBoxTEMCondition.Controls.Add(numericBoxCamaraLength);
            groupBoxTEMCondition.Controls.Add(label18);
            groupBoxTEMCondition.Controls.Add(label15);
            groupBoxTEMCondition.Controls.Add(numericBoxAccVol);
            groupBoxTEMCondition.Controls.Add(label19);
            groupBoxTEMCondition.Controls.Add(label2);
            groupBoxTEMCondition.Controls.Add(label68);
            resources.ApplyResources(groupBoxTEMCondition, "groupBoxTEMCondition");
            groupBoxTEMCondition.Name = "groupBoxTEMCondition";
            groupBoxTEMCondition.TabStop = false;
            // 
            // textBoxWaveLength
            // 
            resources.ApplyResources(textBoxWaveLength, "textBoxWaveLength");
            toolTip.SetToolTip(textBoxWaveLength, resources.GetString("textBoxWaveLength.ToolTip")); // 260531Cl
            textBoxWaveLength.Name = "textBoxWaveLength";
            textBoxWaveLength.ReadOnly = true;
            // 
            // label67
            // 
            resources.ApplyResources(label67, "label67");
            toolTip.SetToolTip(label67, resources.GetString("label67.ToolTip")); // 260531Cl
            label67.Name = "label67";
            // 
            // numericBoxCamaraLength
            // 
            numericBoxCamaraLength.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxCamaraLength, "numericBoxCamaraLength");
            toolTip.SetToolTip(numericBoxCamaraLength, resources.GetString("numericBoxCamaraLength.ToolTip")); // 260531Cl
            numericBoxCamaraLength.UpDown_Increment = 10D;
            numericBoxCamaraLength.Maximum = 10000D;
            numericBoxCamaraLength.Minimum = 1D;
            numericBoxCamaraLength.Name = "numericBoxCamaraLength";
            numericBoxCamaraLength.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxCamaraLength.ShowUpDown = true; // 260522Cl 変更: NumericUpDown → NumericBox (スピンボタン表示を保持)
            numericBoxCamaraLength.Value = 1000D;
            numericBoxCamaraLength.ValueChanged += textBox_TextChanged;
            // 
            // label18
            // 
            resources.ApplyResources(label18, "label18");
            label18.Name = "label18";
            // 
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            toolTip.SetToolTip(label15, resources.GetString("label15.ToolTip")); // 260531Cl
            label15.Name = "label15";
            // 
            // numericBoxAccVol
            // 
            numericBoxAccVol.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxAccVol, "numericBoxAccVol");
            toolTip.SetToolTip(numericBoxAccVol, resources.GetString("numericBoxAccVol.ToolTip")); // 260531Cl
            numericBoxAccVol.Maximum = 10000D;
            numericBoxAccVol.Minimum = 1D;
            numericBoxAccVol.Name = "numericBoxAccVol";
            numericBoxAccVol.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxAccVol.ShowUpDown = true; // 260522Cl 追加: スピンボタン表示を保持
            numericBoxAccVol.Value = 200D;
            numericBoxAccVol.ValueChanged += numericBoxAccVol_ValueChanged;
            // 
            // label19
            // 
            resources.ApplyResources(label19, "label19");
            label19.Name = "label19";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip")); // 260531Cl
            label2.Name = "label2";
            // 
            // label68
            // 
            resources.ApplyResources(label68, "label68");
            label68.Name = "label68";
            // 
            // groupBoxPhoto1
            // 
            groupBoxPhoto1.Controls.Add(groupBoxPhoto1HolderCondition);
            groupBoxPhoto1.Controls.Add(groupBoxPhoto1Pattern);
            resources.ApplyResources(groupBoxPhoto1, "groupBoxPhoto1");
            groupBoxPhoto1.Name = "groupBoxPhoto1";
            groupBoxPhoto1.TabStop = false;
            // 
            // groupBoxPhoto1HolderCondition
            // 
            groupBoxPhoto1HolderCondition.Controls.Add(numericBoxP1Tilt2);
            groupBoxPhoto1HolderCondition.Controls.Add(numericBoxP1Tilt1);
            groupBoxPhoto1HolderCondition.Controls.Add(label14);
            groupBoxPhoto1HolderCondition.Controls.Add(label1);
            groupBoxPhoto1HolderCondition.Controls.Add(numericBoxPhoto1Tilt1Err);
            groupBoxPhoto1HolderCondition.Controls.Add(label48);
            groupBoxPhoto1HolderCondition.Controls.Add(numericBoxPhoto1Tilt2Err);
            groupBoxPhoto1HolderCondition.Controls.Add(label49);
            groupBoxPhoto1HolderCondition.Controls.Add(label53);
            groupBoxPhoto1HolderCondition.Controls.Add(label54);
            groupBoxPhoto1HolderCondition.Controls.Add(label9);
            groupBoxPhoto1HolderCondition.Controls.Add(label10);
            resources.ApplyResources(groupBoxPhoto1HolderCondition, "groupBoxPhoto1HolderCondition");
            groupBoxPhoto1HolderCondition.Name = "groupBoxPhoto1HolderCondition";
            groupBoxPhoto1HolderCondition.TabStop = false;
            // 
            // numericBoxP1Tilt2
            // 
            numericBoxP1Tilt2.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxP1Tilt2, "numericBoxP1Tilt2");
            toolTip.SetToolTip(numericBoxP1Tilt2, resources.GetString("numericBoxP1Tilt2.ToolTip")); // 260531Cl
            numericBoxP1Tilt2.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxP1Tilt2.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxP1Tilt2.Name = "numericBoxP1Tilt2";
            numericBoxP1Tilt2.SkipEventDuringInput = false;
            numericBoxP1Tilt2.SmartIncrement = true;
            numericBoxP1Tilt2.ThousandsSeparator = true;
            numericBoxP1Tilt2.ValueChanged += textBoxTilt_TextChanged;
            // 
            // numericBoxP1Tilt1
            // 
            numericBoxP1Tilt1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxP1Tilt1, "numericBoxP1Tilt1");
            toolTip.SetToolTip(numericBoxP1Tilt1, resources.GetString("numericBoxP1Tilt1.ToolTip")); // 260531Cl
            numericBoxP1Tilt1.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxP1Tilt1.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxP1Tilt1.Name = "numericBoxP1Tilt1";
            numericBoxP1Tilt1.SkipEventDuringInput = false;
            numericBoxP1Tilt1.SmartIncrement = true;
            numericBoxP1Tilt1.ThousandsSeparator = true;
            numericBoxP1Tilt1.ValueChanged += textBoxTilt_TextChanged;
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            toolTip.SetToolTip(label14, resources.GetString("label14.ToolTip")); // 260531Cl
            label14.Name = "label14";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip")); // 260531Cl
            label1.Name = "label1";
            // 
            // numericBoxPhoto1Tilt1Err
            // 
            resources.ApplyResources(numericBoxPhoto1Tilt1Err, "numericBoxPhoto1Tilt1Err");
            toolTip.SetToolTip(numericBoxPhoto1Tilt1Err, resources.GetString("numericBoxPhoto1Tilt1Err.ToolTip")); // 260531Cl
            numericBoxPhoto1Tilt1Err.Maximum = 10D;
            numericBoxPhoto1Tilt1Err.Name = "numericBoxPhoto1Tilt1Err";
            numericBoxPhoto1Tilt1Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto1Tilt1Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto1Tilt1Err.DecimalPlaces = 0;
            numericBoxPhoto1Tilt1Err.ShowUpDown = true;
            numericBoxPhoto1Tilt1Err.Value = 3D;
            // 
            // label48
            // 
            resources.ApplyResources(label48, "label48");
            label48.Name = "label48";
            // 
            // numericBoxPhoto1Tilt2Err
            // 
            resources.ApplyResources(numericBoxPhoto1Tilt2Err, "numericBoxPhoto1Tilt2Err");
            toolTip.SetToolTip(numericBoxPhoto1Tilt2Err, resources.GetString("numericBoxPhoto1Tilt2Err.ToolTip")); // 260531Cl
            numericBoxPhoto1Tilt2Err.Maximum = 10D;
            numericBoxPhoto1Tilt2Err.Name = "numericBoxPhoto1Tilt2Err";
            numericBoxPhoto1Tilt2Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto1Tilt2Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto1Tilt2Err.DecimalPlaces = 0;
            numericBoxPhoto1Tilt2Err.ShowUpDown = true;
            numericBoxPhoto1Tilt2Err.Value = 3D;
            // 
            // label49
            // 
            resources.ApplyResources(label49, "label49");
            label49.Name = "label49";
            // 
            // label53
            // 
            resources.ApplyResources(label53, "label53");
            label53.Name = "label53";
            // 
            // label54
            // 
            resources.ApplyResources(label54, "label54");
            label54.Name = "label54";
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            // 
            // panel4
            // 
            panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel4.Controls.Add(label59);
            resources.ApplyResources(panel4, "panel4");
            panel4.Name = "panel4";
            // 
            // label59
            // 
            resources.ApplyResources(label59, "label59");
            label59.Name = "label59";
            // 
            // textBoxAngleBetween12
            // 
            resources.ApplyResources(textBoxAngleBetween12, "textBoxAngleBetween12");
            toolTip.SetToolTip(textBoxAngleBetween12, resources.GetString("textBoxAngleBetween12.ToolTip")); // 260531Cl
            textBoxAngleBetween12.Name = "textBoxAngleBetween12";
            textBoxAngleBetween12.ReadOnly = true;
            textBoxAngleBetween12.TabStop = false;
            // 
            // textBoxAngleBetween31
            // 
            resources.ApplyResources(textBoxAngleBetween31, "textBoxAngleBetween31");
            toolTip.SetToolTip(textBoxAngleBetween31, resources.GetString("textBoxAngleBetween31.ToolTip")); // 260531Cl
            textBoxAngleBetween31.Name = "textBoxAngleBetween31";
            textBoxAngleBetween31.ReadOnly = true;
            textBoxAngleBetween31.TabStop = false;
            // 
            // label176
            // 
            resources.ApplyResources(label176, "label176");
            label176.Name = "label176";
            // 
            // textBoxAngleBetween23
            // 
            resources.ApplyResources(textBoxAngleBetween23, "textBoxAngleBetween23");
            toolTip.SetToolTip(textBoxAngleBetween23, resources.GetString("textBoxAngleBetween23.ToolTip")); // 260531Cl
            textBoxAngleBetween23.Name = "textBoxAngleBetween23";
            textBoxAngleBetween23.ReadOnly = true;
            textBoxAngleBetween23.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(panel2, "panel2");
            panel2.ForeColor = System.Drawing.SystemColors.GrayText;
            panel2.Name = "panel2";
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(panel3, "panel3");
            panel3.ForeColor = System.Drawing.SystemColors.GrayText;
            panel3.Name = "panel3";
            // 
            // groupBoxPhoto2Pattern
            // 
            groupBoxPhoto2Pattern.Controls.Add(buttonSearchPhoto2);
            groupBoxPhoto2Pattern.Controls.Add(numericBoxP2Theta);
            groupBoxPhoto2Pattern.Controls.Add(pictureBoxPhoto2);
            groupBoxPhoto2Pattern.Controls.Add(inputBoxP2L3);
            groupBoxPhoto2Pattern.Controls.Add(radioButtonPhoto2Mode2);
            groupBoxPhoto2Pattern.Controls.Add(inputBoxP2L2);
            groupBoxPhoto2Pattern.Controls.Add(radioButtonPhoto2Mode1);
            groupBoxPhoto2Pattern.Controls.Add(inputBoxP2L1);
            groupBoxPhoto2Pattern.Controls.Add(numericBoxPhoto2L1Err);
            groupBoxPhoto2Pattern.Controls.Add(numericBoxPhoto2L2Err);
            groupBoxPhoto2Pattern.Controls.Add(numericBoxPhoto2ThetaErr);
            groupBoxPhoto2Pattern.Controls.Add(label22);
            groupBoxPhoto2Pattern.Controls.Add(label23);
            groupBoxPhoto2Pattern.Controls.Add(label24);
            groupBoxPhoto2Pattern.Controls.Add(numericBoxPhoto2L3Err);
            groupBoxPhoto2Pattern.Controls.Add(label26);
            groupBoxPhoto2Pattern.Controls.Add(label27);
            groupBoxPhoto2Pattern.Controls.Add(label28);
            groupBoxPhoto2Pattern.Controls.Add(label29);
            groupBoxPhoto2Pattern.Controls.Add(label30);
            groupBoxPhoto2Pattern.Controls.Add(label31);
            groupBoxPhoto2Pattern.Controls.Add(label79);
            groupBoxPhoto2Pattern.Controls.Add(label76);
            groupBoxPhoto2Pattern.Controls.Add(label77);
            groupBoxPhoto2Pattern.Controls.Add(label78);
            resources.ApplyResources(groupBoxPhoto2Pattern, "groupBoxPhoto2Pattern");
            groupBoxPhoto2Pattern.Name = "groupBoxPhoto2Pattern";
            groupBoxPhoto2Pattern.TabStop = false;
            // 
            // buttonSearchPhoto2
            // 
            resources.ApplyResources(buttonSearchPhoto2, "buttonSearchPhoto2");
            toolTip.SetToolTip(buttonSearchPhoto2, resources.GetString("buttonSearchPhoto2.ToolTip")); // 260531Cl
            buttonSearchPhoto2.Name = "buttonSearchPhoto2";
            buttonSearchPhoto2.BackColor = System.Drawing.Color.SteelBlue; // 260520Cl 追加: 主要アクション(検索)を水色に統一
            buttonSearchPhoto2.ForeColor = System.Drawing.Color.White; // 260520Cl 追加
            buttonSearchPhoto2.UseVisualStyleBackColor = false; // 260520Cl 変更: BackColor有効化のため
            buttonSearchPhoto2.Click += buttonSearch_Click;
            // 
            // numericBoxP2Theta
            // 
            numericBoxP2Theta.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxP2Theta, "numericBoxP2Theta");
            toolTip.SetToolTip(numericBoxP2Theta, resources.GetString("numericBoxP2Theta.ToolTip")); // 260531Cl
            numericBoxP2Theta.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxP2Theta.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxP2Theta.Name = "numericBoxP2Theta";
            numericBoxP2Theta.SkipEventDuringInput = false;
            numericBoxP2Theta.SmartIncrement = true;
            numericBoxP2Theta.ThousandsSeparator = true;
            numericBoxP2Theta.ValueChanged += textBox_TextChanged;
            numericBoxP2Theta.Click2 += numericBoxP2Theta_Click2;
            // 
            // pictureBoxPhoto2
            // 
            pictureBoxPhoto2.BackColor = System.Drawing.SystemColors.Window;
            pictureBoxPhoto2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(pictureBoxPhoto2, "pictureBoxPhoto2");
            pictureBoxPhoto2.Name = "pictureBoxPhoto2";
            pictureBoxPhoto2.TabStop = false;
            // 
            // inputBoxP2L3
            // 
            resources.ApplyResources(inputBoxP2L3, "inputBoxP2L3");
            inputBoxP2L3.CameraLength = 0D;
            inputBoxP2L3.Length = 0D;
            inputBoxP2L3.Name = "inputBoxP2L3";
            inputBoxP2L3.WaveLength = 0D;
            inputBoxP2L3.ValueChanged += textBox_TextChanged;
            inputBoxP2L3.Click2 += inputBoxP2L3_Click2;
            // 
            // radioButtonPhoto2Mode2
            // 
            resources.ApplyResources(radioButtonPhoto2Mode2, "radioButtonPhoto2Mode2");
            toolTip.SetToolTip(radioButtonPhoto2Mode2, resources.GetString("radioButtonPhoto2Mode2.ToolTip")); // 260531Cl
            radioButtonPhoto2Mode2.Name = "radioButtonPhoto2Mode2";
            radioButtonPhoto2Mode2.CheckedChanged += radioButtonPhoto1Mode1_CheckedChanged;
            // 
            // inputBoxP2L2
            // 
            resources.ApplyResources(inputBoxP2L2, "inputBoxP2L2");
            inputBoxP2L2.CameraLength = 0D;
            inputBoxP2L2.Length = 0D;
            inputBoxP2L2.Name = "inputBoxP2L2";
            inputBoxP2L2.WaveLength = 0D;
            inputBoxP2L2.ValueChanged += textBox_TextChanged;
            // 
            // radioButtonPhoto2Mode1
            // 
            resources.ApplyResources(radioButtonPhoto2Mode1, "radioButtonPhoto2Mode1");
            toolTip.SetToolTip(radioButtonPhoto2Mode1, resources.GetString("radioButtonPhoto2Mode1.ToolTip")); // 260531Cl
            radioButtonPhoto2Mode1.Checked = true;
            radioButtonPhoto2Mode1.Name = "radioButtonPhoto2Mode1";
            radioButtonPhoto2Mode1.TabStop = true;
            // 
            // inputBoxP2L1
            // 
            resources.ApplyResources(inputBoxP2L1, "inputBoxP2L1");
            inputBoxP2L1.CameraLength = 0D;
            inputBoxP2L1.Length = 0D;
            inputBoxP2L1.Name = "inputBoxP2L1";
            inputBoxP2L1.WaveLength = 0D;
            inputBoxP2L1.ValueChanged += textBox_TextChanged;
            // 
            // numericBoxPhoto2L1Err
            // 
            resources.ApplyResources(numericBoxPhoto2L1Err, "numericBoxPhoto2L1Err");
            toolTip.SetToolTip(numericBoxPhoto2L1Err, resources.GetString("numericBoxPhoto2L1Err.ToolTip")); // 260531Cl
            numericBoxPhoto2L1Err.Maximum = 50D;
            numericBoxPhoto2L1Err.Name = "numericBoxPhoto2L1Err";
            numericBoxPhoto2L1Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto2L1Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto2L1Err.DecimalPlaces = 0;
            numericBoxPhoto2L1Err.ShowUpDown = true;
            numericBoxPhoto2L1Err.Value = 5D;
            // 
            // numericBoxPhoto2L2Err
            // 
            resources.ApplyResources(numericBoxPhoto2L2Err, "numericBoxPhoto2L2Err");
            toolTip.SetToolTip(numericBoxPhoto2L2Err, resources.GetString("numericBoxPhoto2L2Err.ToolTip")); // 260531Cl
            numericBoxPhoto2L2Err.Maximum = 50D;
            numericBoxPhoto2L2Err.Name = "numericBoxPhoto2L2Err";
            numericBoxPhoto2L2Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto2L2Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto2L2Err.DecimalPlaces = 0;
            numericBoxPhoto2L2Err.ShowUpDown = true;
            numericBoxPhoto2L2Err.Value = 5D;
            // 
            // numericBoxPhoto2ThetaErr
            // 
            resources.ApplyResources(numericBoxPhoto2ThetaErr, "numericBoxPhoto2ThetaErr");
            toolTip.SetToolTip(numericBoxPhoto2ThetaErr, resources.GetString("numericBoxPhoto2ThetaErr.ToolTip")); // 260531Cl
            numericBoxPhoto2ThetaErr.Maximum = 30D;
            numericBoxPhoto2ThetaErr.Name = "numericBoxPhoto2ThetaErr";
            numericBoxPhoto2ThetaErr.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto2ThetaErr.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto2ThetaErr.DecimalPlaces = 0;
            numericBoxPhoto2ThetaErr.ShowUpDown = true;
            numericBoxPhoto2ThetaErr.Value = 3D;
            // 
            // label22
            // 
            resources.ApplyResources(label22, "label22");
            toolTip.SetToolTip(label22, resources.GetString("label22.ToolTip")); // 260531Cl
            label22.Name = "label22";
            // 
            // label23
            // 
            resources.ApplyResources(label23, "label23");
            toolTip.SetToolTip(label23, resources.GetString("label23.ToolTip")); // 260531Cl
            label23.Name = "label23";
            // 
            // label24
            // 
            resources.ApplyResources(label24, "label24");
            toolTip.SetToolTip(label24, resources.GetString("label24.ToolTip")); // 260531Cl
            label24.Name = "label24";
            // 
            // numericBoxPhoto2L3Err
            // 
            resources.ApplyResources(numericBoxPhoto2L3Err, "numericBoxPhoto2L3Err");
            toolTip.SetToolTip(numericBoxPhoto2L3Err, resources.GetString("numericBoxPhoto2L3Err.ToolTip")); // 260531Cl
            numericBoxPhoto2L3Err.Maximum = 50D;
            numericBoxPhoto2L3Err.Name = "numericBoxPhoto2L3Err";
            numericBoxPhoto2L3Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto2L3Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto2L3Err.DecimalPlaces = 0;
            numericBoxPhoto2L3Err.ShowUpDown = true;
            numericBoxPhoto2L3Err.Value = 5D;
            // 
            // label26
            // 
            resources.ApplyResources(label26, "label26");
            toolTip.SetToolTip(label26, resources.GetString("label26.ToolTip")); // 260531Cl
            label26.Name = "label26";
            // 
            // label27
            // 
            resources.ApplyResources(label27, "label27");
            toolTip.SetToolTip(label27, resources.GetString("label27.ToolTip")); // 260531Cl
            label27.Name = "label27";
            // 
            // label28
            // 
            resources.ApplyResources(label28, "label28");
            toolTip.SetToolTip(label28, resources.GetString("label28.ToolTip")); // 260531Cl
            label28.Name = "label28";
            // 
            // label29
            // 
            resources.ApplyResources(label29, "label29");
            toolTip.SetToolTip(label29, resources.GetString("label29.ToolTip")); // 260531Cl
            label29.Name = "label29";
            // 
            // label30
            // 
            resources.ApplyResources(label30, "label30");
            toolTip.SetToolTip(label30, resources.GetString("label30.ToolTip")); // 260531Cl
            label30.Name = "label30";
            // 
            // label31
            // 
            resources.ApplyResources(label31, "label31");
            toolTip.SetToolTip(label31, resources.GetString("label31.ToolTip")); // 260531Cl
            label31.Name = "label31";
            // 
            // label79
            // 
            resources.ApplyResources(label79, "label79");
            toolTip.SetToolTip(label79, resources.GetString("label79.ToolTip")); // 260531Cl
            label79.Name = "label79";
            // 
            // label76
            // 
            resources.ApplyResources(label76, "label76");
            toolTip.SetToolTip(label76, resources.GetString("label76.ToolTip")); // 260531Cl
            label76.Name = "label76";
            // 
            // label77
            // 
            resources.ApplyResources(label77, "label77");
            toolTip.SetToolTip(label77, resources.GetString("label77.ToolTip")); // 260531Cl
            label77.Name = "label77";
            // 
            // label78
            // 
            resources.ApplyResources(label78, "label78");
            toolTip.SetToolTip(label78, resources.GetString("label78.ToolTip")); // 260531Cl
            label78.Name = "label78";
            // 
            // groupBoxPhoto2HolderCondition
            // 
            groupBoxPhoto2HolderCondition.Controls.Add(numericBoxP2Tilt2);
            groupBoxPhoto2HolderCondition.Controls.Add(numericBoxP2Tilt1);
            groupBoxPhoto2HolderCondition.Controls.Add(label6);
            groupBoxPhoto2HolderCondition.Controls.Add(label7);
            groupBoxPhoto2HolderCondition.Controls.Add(numericBoxPhoto2Tilt1Err);
            groupBoxPhoto2HolderCondition.Controls.Add(label8);
            groupBoxPhoto2HolderCondition.Controls.Add(numericBoxPhoto2Tilt2Err);
            groupBoxPhoto2HolderCondition.Controls.Add(label13);
            groupBoxPhoto2HolderCondition.Controls.Add(label16);
            groupBoxPhoto2HolderCondition.Controls.Add(label17);
            groupBoxPhoto2HolderCondition.Controls.Add(label3);
            groupBoxPhoto2HolderCondition.Controls.Add(label5);
            resources.ApplyResources(groupBoxPhoto2HolderCondition, "groupBoxPhoto2HolderCondition");
            groupBoxPhoto2HolderCondition.Name = "groupBoxPhoto2HolderCondition";
            groupBoxPhoto2HolderCondition.TabStop = false;
            // 
            // numericBoxP2Tilt2
            // 
            numericBoxP2Tilt2.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxP2Tilt2, "numericBoxP2Tilt2");
            toolTip.SetToolTip(numericBoxP2Tilt2, resources.GetString("numericBoxP2Tilt2.ToolTip")); // 260531Cl
            numericBoxP2Tilt2.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxP2Tilt2.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxP2Tilt2.Name = "numericBoxP2Tilt2";
            numericBoxP2Tilt2.SkipEventDuringInput = false;
            numericBoxP2Tilt2.SmartIncrement = true;
            numericBoxP2Tilt2.ThousandsSeparator = true;
            numericBoxP2Tilt2.ValueChanged += textBoxTilt_TextChanged;
            // 
            // numericBoxP2Tilt1
            // 
            numericBoxP2Tilt1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxP2Tilt1, "numericBoxP2Tilt1");
            toolTip.SetToolTip(numericBoxP2Tilt1, resources.GetString("numericBoxP2Tilt1.ToolTip")); // 260531Cl
            numericBoxP2Tilt1.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxP2Tilt1.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxP2Tilt1.Name = "numericBoxP2Tilt1";
            numericBoxP2Tilt1.SkipEventDuringInput = false;
            numericBoxP2Tilt1.SmartIncrement = true;
            numericBoxP2Tilt1.ThousandsSeparator = true;
            numericBoxP2Tilt1.ValueChanged += textBoxTilt_TextChanged;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip")); // 260531Cl
            label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip")); // 260531Cl
            label7.Name = "label7";
            // 
            // numericBoxPhoto2Tilt1Err
            // 
            resources.ApplyResources(numericBoxPhoto2Tilt1Err, "numericBoxPhoto2Tilt1Err");
            toolTip.SetToolTip(numericBoxPhoto2Tilt1Err, resources.GetString("numericBoxPhoto2Tilt1Err.ToolTip")); // 260531Cl
            numericBoxPhoto2Tilt1Err.Maximum = 10D;
            numericBoxPhoto2Tilt1Err.Name = "numericBoxPhoto2Tilt1Err";
            numericBoxPhoto2Tilt1Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto2Tilt1Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto2Tilt1Err.DecimalPlaces = 0;
            numericBoxPhoto2Tilt1Err.ShowUpDown = true;
            numericBoxPhoto2Tilt1Err.Value = 3D;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            // 
            // numericBoxPhoto2Tilt2Err
            // 
            resources.ApplyResources(numericBoxPhoto2Tilt2Err, "numericBoxPhoto2Tilt2Err");
            toolTip.SetToolTip(numericBoxPhoto2Tilt2Err, resources.GetString("numericBoxPhoto2Tilt2Err.ToolTip")); // 260531Cl
            numericBoxPhoto2Tilt2Err.Maximum = 10D;
            numericBoxPhoto2Tilt2Err.Name = "numericBoxPhoto2Tilt2Err";
            numericBoxPhoto2Tilt2Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto2Tilt2Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto2Tilt2Err.DecimalPlaces = 0;
            numericBoxPhoto2Tilt2Err.ShowUpDown = true;
            numericBoxPhoto2Tilt2Err.Value = 3D;
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            label16.Name = "label16";
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // groupBoxPhoto2
            // 
            groupBoxPhoto2.Controls.Add(groupBoxPhoto2HolderCondition);
            groupBoxPhoto2.Controls.Add(groupBoxPhoto2Pattern);
            resources.ApplyResources(groupBoxPhoto2, "groupBoxPhoto2");
            groupBoxPhoto2.Name = "groupBoxPhoto2";
            groupBoxPhoto2.TabStop = false;
            // 
            // groupBoxPhoto3Pattern
            // 
            groupBoxPhoto3Pattern.Controls.Add(inputBoxP3L3);
            groupBoxPhoto3Pattern.Controls.Add(numericBoxP3Theta);
            groupBoxPhoto3Pattern.Controls.Add(inputBoxP3L2);
            groupBoxPhoto3Pattern.Controls.Add(buttonSearchPhoto3);
            groupBoxPhoto3Pattern.Controls.Add(inputBoxP3L1);
            groupBoxPhoto3Pattern.Controls.Add(pictureBoxPhoto3);
            groupBoxPhoto3Pattern.Controls.Add(radioButtonPhoto3Mode2);
            groupBoxPhoto3Pattern.Controls.Add(radioButtonPhoto3Mode1);
            groupBoxPhoto3Pattern.Controls.Add(numericBoxPhoto3L1Err);
            groupBoxPhoto3Pattern.Controls.Add(numericBoxPhoto3L2Err);
            groupBoxPhoto3Pattern.Controls.Add(numericBoxPhoto3ThetaErr);
            groupBoxPhoto3Pattern.Controls.Add(label42);
            groupBoxPhoto3Pattern.Controls.Add(label43);
            groupBoxPhoto3Pattern.Controls.Add(label44);
            groupBoxPhoto3Pattern.Controls.Add(numericBoxPhoto3L3Err);
            groupBoxPhoto3Pattern.Controls.Add(label46);
            groupBoxPhoto3Pattern.Controls.Add(label47);
            groupBoxPhoto3Pattern.Controls.Add(label52);
            groupBoxPhoto3Pattern.Controls.Add(label55);
            groupBoxPhoto3Pattern.Controls.Add(label56);
            groupBoxPhoto3Pattern.Controls.Add(label57);
            groupBoxPhoto3Pattern.Controls.Add(label101);
            groupBoxPhoto3Pattern.Controls.Add(label100);
            groupBoxPhoto3Pattern.Controls.Add(label99);
            groupBoxPhoto3Pattern.Controls.Add(label98);
            resources.ApplyResources(groupBoxPhoto3Pattern, "groupBoxPhoto3Pattern");
            groupBoxPhoto3Pattern.Name = "groupBoxPhoto3Pattern";
            groupBoxPhoto3Pattern.TabStop = false;
            // 
            // inputBoxP3L3
            // 
            resources.ApplyResources(inputBoxP3L3, "inputBoxP3L3");
            inputBoxP3L3.CameraLength = 0D;
            inputBoxP3L3.Length = 0D;
            inputBoxP3L3.Name = "inputBoxP3L3";
            inputBoxP3L3.WaveLength = 0D;
            inputBoxP3L3.ValueChanged += textBox_TextChanged;
            // 
            // numericBoxP3Theta
            // 
            numericBoxP3Theta.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxP3Theta, "numericBoxP3Theta");
            toolTip.SetToolTip(numericBoxP3Theta, resources.GetString("numericBoxP3Theta.ToolTip")); // 260531Cl
            numericBoxP3Theta.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxP3Theta.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxP3Theta.Name = "numericBoxP3Theta";
            numericBoxP3Theta.SkipEventDuringInput = false;
            numericBoxP3Theta.SmartIncrement = true;
            numericBoxP3Theta.ThousandsSeparator = true;
            numericBoxP3Theta.ValueChanged += textBox_TextChanged;
            numericBoxP3Theta.Click2 += numericBoxP3Theta_Click2;
            // 
            // inputBoxP3L2
            // 
            resources.ApplyResources(inputBoxP3L2, "inputBoxP3L2");
            inputBoxP3L2.CameraLength = 0D;
            inputBoxP3L2.Length = 0D;
            inputBoxP3L2.Name = "inputBoxP3L2";
            inputBoxP3L2.WaveLength = 0D;
            inputBoxP3L2.ValueChanged += textBox_TextChanged;
            // 
            // buttonSearchPhoto3
            // 
            resources.ApplyResources(buttonSearchPhoto3, "buttonSearchPhoto3");
            toolTip.SetToolTip(buttonSearchPhoto3, resources.GetString("buttonSearchPhoto3.ToolTip")); // 260531Cl
            buttonSearchPhoto3.Name = "buttonSearchPhoto3";
            buttonSearchPhoto3.BackColor = System.Drawing.Color.SteelBlue; // 260520Cl 追加: 主要アクション(検索)を水色に統一
            buttonSearchPhoto3.ForeColor = System.Drawing.Color.White; // 260520Cl 追加
            buttonSearchPhoto3.UseVisualStyleBackColor = false; // 260520Cl 変更: BackColor有効化のため
            buttonSearchPhoto3.Click += buttonSearch_Click;
            // 
            // inputBoxP3L1
            // 
            resources.ApplyResources(inputBoxP3L1, "inputBoxP3L1");
            inputBoxP3L1.CameraLength = 0D;
            inputBoxP3L1.Length = 0D;
            inputBoxP3L1.Name = "inputBoxP3L1";
            inputBoxP3L1.WaveLength = 0D;
            inputBoxP3L1.ValueChanged += textBox_TextChanged;
            inputBoxP3L1.Click2 += inputBoxP3L1_Click2;
            // 
            // pictureBoxPhoto3
            // 
            pictureBoxPhoto3.BackColor = System.Drawing.SystemColors.Window;
            pictureBoxPhoto3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(pictureBoxPhoto3, "pictureBoxPhoto3");
            pictureBoxPhoto3.Name = "pictureBoxPhoto3";
            pictureBoxPhoto3.TabStop = false;
            // 
            // radioButtonPhoto3Mode2
            // 
            resources.ApplyResources(radioButtonPhoto3Mode2, "radioButtonPhoto3Mode2");
            toolTip.SetToolTip(radioButtonPhoto3Mode2, resources.GetString("radioButtonPhoto3Mode2.ToolTip")); // 260531Cl
            radioButtonPhoto3Mode2.Name = "radioButtonPhoto3Mode2";
            radioButtonPhoto3Mode2.CheckedChanged += radioButtonPhoto1Mode1_CheckedChanged;
            // 
            // radioButtonPhoto3Mode1
            // 
            resources.ApplyResources(radioButtonPhoto3Mode1, "radioButtonPhoto3Mode1");
            toolTip.SetToolTip(radioButtonPhoto3Mode1, resources.GetString("radioButtonPhoto3Mode1.ToolTip")); // 260531Cl
            radioButtonPhoto3Mode1.Checked = true;
            radioButtonPhoto3Mode1.Name = "radioButtonPhoto3Mode1";
            radioButtonPhoto3Mode1.TabStop = true;
            // 
            // numericBoxPhoto3L1Err
            // 
            resources.ApplyResources(numericBoxPhoto3L1Err, "numericBoxPhoto3L1Err");
            toolTip.SetToolTip(numericBoxPhoto3L1Err, resources.GetString("numericBoxPhoto3L1Err.ToolTip")); // 260531Cl
            numericBoxPhoto3L1Err.Maximum = 50D;
            numericBoxPhoto3L1Err.Name = "numericBoxPhoto3L1Err";
            numericBoxPhoto3L1Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto3L1Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto3L1Err.DecimalPlaces = 0;
            numericBoxPhoto3L1Err.ShowUpDown = true;
            numericBoxPhoto3L1Err.Value = 5D;
            // 
            // numericBoxPhoto3L2Err
            // 
            resources.ApplyResources(numericBoxPhoto3L2Err, "numericBoxPhoto3L2Err");
            toolTip.SetToolTip(numericBoxPhoto3L2Err, resources.GetString("numericBoxPhoto3L2Err.ToolTip")); // 260531Cl
            numericBoxPhoto3L2Err.Maximum = 50D;
            numericBoxPhoto3L2Err.Name = "numericBoxPhoto3L2Err";
            numericBoxPhoto3L2Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto3L2Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto3L2Err.DecimalPlaces = 0;
            numericBoxPhoto3L2Err.ShowUpDown = true;
            numericBoxPhoto3L2Err.Value = 5D;
            // 
            // numericBoxPhoto3ThetaErr
            // 
            resources.ApplyResources(numericBoxPhoto3ThetaErr, "numericBoxPhoto3ThetaErr");
            toolTip.SetToolTip(numericBoxPhoto3ThetaErr, resources.GetString("numericBoxPhoto3ThetaErr.ToolTip")); // 260531Cl
            numericBoxPhoto3ThetaErr.Maximum = 30D;
            numericBoxPhoto3ThetaErr.Name = "numericBoxPhoto3ThetaErr";
            numericBoxPhoto3ThetaErr.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto3ThetaErr.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto3ThetaErr.DecimalPlaces = 0;
            numericBoxPhoto3ThetaErr.ShowUpDown = true;
            numericBoxPhoto3ThetaErr.Value = 3D;
            // 
            // label42
            // 
            resources.ApplyResources(label42, "label42");
            label42.Name = "label42";
            // 
            // label43
            // 
            resources.ApplyResources(label43, "label43");
            label43.Name = "label43";
            // 
            // label44
            // 
            resources.ApplyResources(label44, "label44");
            label44.Name = "label44";
            // 
            // numericBoxPhoto3L3Err
            // 
            resources.ApplyResources(numericBoxPhoto3L3Err, "numericBoxPhoto3L3Err");
            toolTip.SetToolTip(numericBoxPhoto3L3Err, resources.GetString("numericBoxPhoto3L3Err.ToolTip")); // 260531Cl
            numericBoxPhoto3L3Err.Maximum = 50D;
            numericBoxPhoto3L3Err.Name = "numericBoxPhoto3L3Err";
            numericBoxPhoto3L3Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto3L3Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto3L3Err.DecimalPlaces = 0;
            numericBoxPhoto3L3Err.ShowUpDown = true;
            numericBoxPhoto3L3Err.Value = 5D;
            // 
            // label46
            // 
            resources.ApplyResources(label46, "label46");
            label46.Name = "label46";
            // 
            // label47
            // 
            resources.ApplyResources(label47, "label47");
            toolTip.SetToolTip(label47, resources.GetString("label47.ToolTip")); // 260531Cl
            label47.Name = "label47";
            // 
            // label52
            // 
            resources.ApplyResources(label52, "label52");
            toolTip.SetToolTip(label52, resources.GetString("label52.ToolTip")); // 260531Cl
            label52.Name = "label52";
            // 
            // label55
            // 
            resources.ApplyResources(label55, "label55");
            toolTip.SetToolTip(label55, resources.GetString("label55.ToolTip")); // 260531Cl
            label55.Name = "label55";
            // 
            // label56
            // 
            resources.ApplyResources(label56, "label56");
            toolTip.SetToolTip(label56, resources.GetString("label56.ToolTip")); // 260531Cl
            label56.Name = "label56";
            // 
            // label57
            // 
            resources.ApplyResources(label57, "label57");
            label57.Name = "label57";
            // 
            // label101
            // 
            resources.ApplyResources(label101, "label101");
            label101.Name = "label101";
            // 
            // label100
            // 
            resources.ApplyResources(label100, "label100");
            label100.Name = "label100";
            // 
            // label99
            // 
            resources.ApplyResources(label99, "label99");
            label99.Name = "label99";
            // 
            // label98
            // 
            resources.ApplyResources(label98, "label98");
            label98.Name = "label98";
            // 
            // groupBoxPhoto3HolderCondition
            // 
            groupBoxPhoto3HolderCondition.Controls.Add(numericBoxP3Tilt2);
            groupBoxPhoto3HolderCondition.Controls.Add(label34);
            groupBoxPhoto3HolderCondition.Controls.Add(numericBoxP3Tilt1);
            groupBoxPhoto3HolderCondition.Controls.Add(label35);
            groupBoxPhoto3HolderCondition.Controls.Add(numericBoxPhoto3Tilt1Err);
            groupBoxPhoto3HolderCondition.Controls.Add(label36);
            groupBoxPhoto3HolderCondition.Controls.Add(numericBoxPhoto3Tilt2Err);
            groupBoxPhoto3HolderCondition.Controls.Add(label37);
            groupBoxPhoto3HolderCondition.Controls.Add(label38);
            groupBoxPhoto3HolderCondition.Controls.Add(label39);
            groupBoxPhoto3HolderCondition.Controls.Add(label32);
            groupBoxPhoto3HolderCondition.Controls.Add(label33);
            resources.ApplyResources(groupBoxPhoto3HolderCondition, "groupBoxPhoto3HolderCondition");
            groupBoxPhoto3HolderCondition.Name = "groupBoxPhoto3HolderCondition";
            groupBoxPhoto3HolderCondition.TabStop = false;
            // 
            // numericBoxP3Tilt2
            // 
            numericBoxP3Tilt2.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxP3Tilt2, "numericBoxP3Tilt2");
            toolTip.SetToolTip(numericBoxP3Tilt2, resources.GetString("numericBoxP3Tilt2.ToolTip")); // 260531Cl
            numericBoxP3Tilt2.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxP3Tilt2.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxP3Tilt2.Name = "numericBoxP3Tilt2";
            numericBoxP3Tilt2.SkipEventDuringInput = false;
            numericBoxP3Tilt2.SmartIncrement = true;
            numericBoxP3Tilt2.ThousandsSeparator = true;
            numericBoxP3Tilt2.ValueChanged += textBoxTilt_TextChanged;
            // 
            // label34
            // 
            resources.ApplyResources(label34, "label34");
            toolTip.SetToolTip(label34, resources.GetString("label34.ToolTip")); // 260531Cl
            label34.Name = "label34";
            // 
            // numericBoxP3Tilt1
            // 
            numericBoxP3Tilt1.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxP3Tilt1, "numericBoxP3Tilt1");
            toolTip.SetToolTip(numericBoxP3Tilt1, resources.GetString("numericBoxP3Tilt1.ToolTip")); // 260531Cl
            numericBoxP3Tilt1.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxP3Tilt1.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxP3Tilt1.Name = "numericBoxP3Tilt1";
            numericBoxP3Tilt1.SkipEventDuringInput = false;
            numericBoxP3Tilt1.SmartIncrement = true;
            numericBoxP3Tilt1.ThousandsSeparator = true;
            numericBoxP3Tilt1.ValueChanged += textBoxTilt_TextChanged;
            // 
            // label35
            // 
            resources.ApplyResources(label35, "label35");
            toolTip.SetToolTip(label35, resources.GetString("label35.ToolTip")); // 260531Cl
            label35.Name = "label35";
            // 
            // numericBoxPhoto3Tilt1Err
            // 
            resources.ApplyResources(numericBoxPhoto3Tilt1Err, "numericBoxPhoto3Tilt1Err");
            toolTip.SetToolTip(numericBoxPhoto3Tilt1Err, resources.GetString("numericBoxPhoto3Tilt1Err.ToolTip")); // 260531Cl
            numericBoxPhoto3Tilt1Err.Maximum = 10D;
            numericBoxPhoto3Tilt1Err.Name = "numericBoxPhoto3Tilt1Err";
            numericBoxPhoto3Tilt1Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto3Tilt1Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto3Tilt1Err.DecimalPlaces = 0;
            numericBoxPhoto3Tilt1Err.ShowUpDown = true;
            numericBoxPhoto3Tilt1Err.Value = 3D;
            // 
            // label36
            // 
            resources.ApplyResources(label36, "label36");
            label36.Name = "label36";
            // 
            // numericBoxPhoto3Tilt2Err
            // 
            resources.ApplyResources(numericBoxPhoto3Tilt2Err, "numericBoxPhoto3Tilt2Err");
            toolTip.SetToolTip(numericBoxPhoto3Tilt2Err, resources.GetString("numericBoxPhoto3Tilt2Err.ToolTip")); // 260531Cl
            numericBoxPhoto3Tilt2Err.Maximum = 10D;
            numericBoxPhoto3Tilt2Err.Name = "numericBoxPhoto3Tilt2Err";
            numericBoxPhoto3Tilt2Err.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            numericBoxPhoto3Tilt2Err.Minimum = 0D; // 260522Cl 追加: NumericUpDownの既定を保持
            numericBoxPhoto3Tilt2Err.DecimalPlaces = 0;
            numericBoxPhoto3Tilt2Err.ShowUpDown = true;
            numericBoxPhoto3Tilt2Err.Value = 3D;
            // 
            // label37
            // 
            resources.ApplyResources(label37, "label37");
            label37.Name = "label37";
            // 
            // label38
            // 
            resources.ApplyResources(label38, "label38");
            label38.Name = "label38";
            // 
            // label39
            // 
            resources.ApplyResources(label39, "label39");
            label39.Name = "label39";
            // 
            // label32
            // 
            resources.ApplyResources(label32, "label32");
            label32.Name = "label32";
            // 
            // label33
            // 
            resources.ApplyResources(label33, "label33");
            label33.Name = "label33";
            // 
            // groupBoxPhoto3
            // 
            groupBoxPhoto3.Controls.Add(groupBoxPhoto3HolderCondition);
            groupBoxPhoto3.Controls.Add(groupBoxPhoto3Pattern);
            resources.ApplyResources(groupBoxPhoto3, "groupBoxPhoto3");
            groupBoxPhoto3.Name = "groupBoxPhoto3";
            groupBoxPhoto3.TabStop = false;
            // 
            // checkBoxPhoto2
            // 
            resources.ApplyResources(checkBoxPhoto2, "checkBoxPhoto2");
            toolTip.SetToolTip(checkBoxPhoto2, resources.GetString("checkBoxPhoto2.ToolTip")); // 260531Cl
            checkBoxPhoto2.Name = "checkBoxPhoto2";
            checkBoxPhoto2.UseVisualStyleBackColor = true;
            checkBoxPhoto2.CheckedChanged += checkBoxPhoto2_CheckedChanged;
            // 
            // checkBoxPhoto3
            // 
            resources.ApplyResources(checkBoxPhoto3, "checkBoxPhoto3");
            toolTip.SetToolTip(checkBoxPhoto3, resources.GetString("checkBoxPhoto3.ToolTip")); // 260531Cl
            checkBoxPhoto3.Name = "checkBoxPhoto3";
            checkBoxPhoto3.UseVisualStyleBackColor = true;
            checkBoxPhoto3.CheckedChanged += checkBoxPhoto2_CheckedChanged;
            // 
            // checkBoxEquivalentPhoto1L1Photo2L1
            // 
            resources.ApplyResources(checkBoxEquivalentPhoto1L1Photo2L1, "checkBoxEquivalentPhoto1L1Photo2L1");
            toolTip.SetToolTip(checkBoxEquivalentPhoto1L1Photo2L1, resources.GetString("checkBoxEquivalentPhoto1L1Photo2L1.ToolTip")); // 260531Cl
            checkBoxEquivalentPhoto1L1Photo2L1.Name = "checkBoxEquivalentPhoto1L1Photo2L1";
            checkBoxEquivalentPhoto1L1Photo2L1.UseVisualStyleBackColor = true;
            checkBoxEquivalentPhoto1L1Photo2L1.CheckedChanged += checkBoxEquivalentPhoto1L1Photo2L1_CheckedChanged;
            // 
            // checkBoxEquivalentPhoto2L1Photo3L1
            // 
            resources.ApplyResources(checkBoxEquivalentPhoto2L1Photo3L1, "checkBoxEquivalentPhoto2L1Photo3L1");
            toolTip.SetToolTip(checkBoxEquivalentPhoto2L1Photo3L1, resources.GetString("checkBoxEquivalentPhoto2L1Photo3L1.ToolTip")); // 260531Cl
            checkBoxEquivalentPhoto2L1Photo3L1.Name = "checkBoxEquivalentPhoto2L1Photo3L1";
            checkBoxEquivalentPhoto2L1Photo3L1.UseVisualStyleBackColor = true;
            checkBoxEquivalentPhoto2L1Photo3L1.CheckedChanged += checkBoxEquivalentPhoto1L1Photo2L1_CheckedChanged;
            // 
            // checkBoxEquivalentPhoto2L2Photo3L2
            // 
            resources.ApplyResources(checkBoxEquivalentPhoto2L2Photo3L2, "checkBoxEquivalentPhoto2L2Photo3L2");
            toolTip.SetToolTip(checkBoxEquivalentPhoto2L2Photo3L2, resources.GetString("checkBoxEquivalentPhoto2L2Photo3L2.ToolTip")); // 260531Cl
            checkBoxEquivalentPhoto2L2Photo3L2.Name = "checkBoxEquivalentPhoto2L2Photo3L2";
            checkBoxEquivalentPhoto2L2Photo3L2.UseVisualStyleBackColor = true;
            checkBoxEquivalentPhoto2L2Photo3L2.CheckedChanged += checkBoxEquivalentPhoto1L1Photo2L1_CheckedChanged;
            // 
            // buttonSearchAll
            // 
            resources.ApplyResources(buttonSearchAll, "buttonSearchAll");
            toolTip.SetToolTip(buttonSearchAll, resources.GetString("buttonSearchAll.ToolTip")); // 260531Cl
            buttonSearchAll.Name = "buttonSearchAll";
            buttonSearchAll.BackColor = System.Drawing.Color.SteelBlue; // 260520Cl 追加: 主要アクション(検索)を水色に統一
            buttonSearchAll.ForeColor = System.Drawing.Color.White; // 260520Cl 追加
            buttonSearchAll.UseVisualStyleBackColor = false; // 260520Cl 変更: BackColor有効化のため
            buttonSearchAll.Click += buttonSearchAll_Click;
            // 
            // panel5
            // 
            panel5.BackColor = System.Drawing.SystemColors.ControlLight;
            panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(panel5, "panel5");
            panel5.ForeColor = System.Drawing.SystemColors.GrayText;
            panel5.Name = "panel5";
            // 
            // checkBoxEquivalentPhoto1L2Photo2L2
            // 
            resources.ApplyResources(checkBoxEquivalentPhoto1L2Photo2L2, "checkBoxEquivalentPhoto1L2Photo2L2");
            toolTip.SetToolTip(checkBoxEquivalentPhoto1L2Photo2L2, resources.GetString("checkBoxEquivalentPhoto1L2Photo2L2.ToolTip")); // 260531Cl
            checkBoxEquivalentPhoto1L2Photo2L2.Name = "checkBoxEquivalentPhoto1L2Photo2L2";
            checkBoxEquivalentPhoto1L2Photo2L2.UseVisualStyleBackColor = true;
            checkBoxEquivalentPhoto1L2Photo2L2.CheckedChanged += checkBoxEquivalentPhoto1L1Photo2L1_CheckedChanged;
            // 
            // label58
            // 
            resources.ApplyResources(label58, "label58");
            label58.Name = "label58";
            // 
            // flowLayoutPanelPhotos
            // 
            resources.ApplyResources(flowLayoutPanelPhotos, "flowLayoutPanelPhotos");
            flowLayoutPanelPhotos.Controls.Add(groupBoxPhoto1);
            flowLayoutPanelPhotos.Controls.Add(panelPhoto2);
            flowLayoutPanelPhotos.Controls.Add(panelPhoto3);
            flowLayoutPanelPhotos.Name = "flowLayoutPanelPhotos";
            // 
            // panelPhoto2
            // 
            resources.ApplyResources(panelPhoto2, "panelPhoto2");
            panelPhoto2.Controls.Add(checkBoxEquivalentPhoto1L1Photo2L1);
            panelPhoto2.Controls.Add(checkBoxEquivalentPhoto1L2Photo2L2);
            panelPhoto2.Controls.Add(textBoxAngleBetween12);
            panelPhoto2.Controls.Add(groupBoxPhoto2);
            panelPhoto2.Controls.Add(panel5);
            panelPhoto2.Controls.Add(panel6);
            panelPhoto2.Controls.Add(label58);
            panelPhoto2.Controls.Add(panel8);
            panelPhoto2.Name = "panelPhoto2";
            // 
            // panel6
            // 
            panel6.BackColor = System.Drawing.SystemColors.ControlLight;
            panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(panel6, "panel6");
            panel6.ForeColor = System.Drawing.SystemColors.GrayText;
            panel6.Name = "panel6";
            // 
            // panel8
            // 
            panel8.BackColor = System.Drawing.SystemColors.ControlLight;
            panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(panel8, "panel8");
            panel8.ForeColor = System.Drawing.SystemColors.GrayText;
            panel8.Name = "panel8";
            // 
            // panelPhoto3
            // 
            resources.ApplyResources(panelPhoto3, "panelPhoto3");
            panelPhoto3.Controls.Add(checkBoxEquivalentPhoto2L1Photo3L1);
            panelPhoto3.Controls.Add(groupBoxPhoto3);
            panelPhoto3.Controls.Add(panel3);
            panelPhoto3.Controls.Add(textBoxAngleBetween23);
            panelPhoto3.Controls.Add(label176);
            panelPhoto3.Controls.Add(checkBoxEquivalentPhoto2L2Photo3L2);
            panelPhoto3.Controls.Add(panel2);
            panelPhoto3.Controls.Add(panel9);
            panelPhoto3.Name = "panelPhoto3";
            // 
            // panel9
            // 
            panel9.BackColor = System.Drawing.SystemColors.ControlLight;
            panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(panel9, "panel9");
            panel9.ForeColor = System.Drawing.SystemColors.GrayText;
            panel9.Name = "panel9";
            // 
            // FormSpotIDv1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F); // 260329Cl 追加: None→Dpi, 96dpi基準に統一
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            resources.ApplyResources(this, "$this");
            captureExtender.SetCapture(this, true);
            Controls.Add(flowLayoutPanelPhotos);
            Controls.Add(textBoxAngleBetween31);
            Controls.Add(checkBoxPhoto2);
            Controls.Add(buttonSearchAll);
            Controls.Add(checkBoxPhoto3);
            Controls.Add(groupBoxTEMCondition);
            Controls.Add(panel4);
            KeyPreview = true;
            Name = "FormSpotIDv1";
            FormClosing += FormTEMID_FormClosing;
            Load += FormTEMID_Load;
            groupBoxPhoto1Pattern.ResumeLayout(false);
            groupBoxPhoto1Pattern.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto1).EndInit();
            groupBoxTEMCondition.ResumeLayout(false);
            groupBoxTEMCondition.PerformLayout();
            groupBoxPhoto1.ResumeLayout(false);
            groupBoxPhoto1HolderCondition.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            groupBoxPhoto2Pattern.ResumeLayout(false);
            groupBoxPhoto2Pattern.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto2).EndInit();
            groupBoxPhoto2HolderCondition.ResumeLayout(false);
            groupBoxPhoto2.ResumeLayout(false);
            groupBoxPhoto3Pattern.ResumeLayout(false);
            groupBoxPhoto3Pattern.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto3).EndInit();
            groupBoxPhoto3HolderCondition.ResumeLayout(false);
            groupBoxPhoto3.ResumeLayout(false);
            flowLayoutPanelPhotos.ResumeLayout(false);
            flowLayoutPanelPhotos.PerformLayout();
            panelPhoto2.ResumeLayout(false);
            panelPhoto2.PerformLayout();
            panelPhoto3.ResumeLayout(false);
            panelPhoto3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)

        private System.Windows.Forms.GroupBox groupBoxPhoto1Pattern;
        private System.Windows.Forms.RadioButton radioButtonPhoto1Mode2;
        private Crystallography.Controls.NumericBox numericBoxPhoto1L1Err;
        private Crystallography.Controls.NumericBox numericBoxPhoto1L2Err;
        private Crystallography.Controls.NumericBox numericBoxPhoto1ThetaErr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPhoto1Mode1_4;
        private System.Windows.Forms.Label label131;
        private Crystallography.Controls.NumericBox numericBoxPhoto1L3Err;
        private System.Windows.Forms.RadioButton radioButtonPhoto1Mode1;
        private System.Windows.Forms.Label labelPhoto1Mode2_6;
        private System.Windows.Forms.Label labelPhoto1Mode1_1;
        private System.Windows.Forms.Label labelPhoto1Mode1_3;
        private System.Windows.Forms.GroupBox groupBoxTEMCondition;
        public Crystallography.Controls.NumericBox numericBoxCamaraLength;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        public Crystallography.Controls.NumericBox numericBoxAccVol;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxPhoto1;
        private System.Windows.Forms.GroupBox groupBoxPhoto1;
        private System.Windows.Forms.GroupBox groupBoxPhoto1HolderCondition;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label1;
        private Crystallography.Controls.NumericBox numericBoxPhoto1Tilt1Err;
        private System.Windows.Forms.Label label48;
        private Crystallography.Controls.NumericBox numericBoxPhoto1Tilt2Err;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Button buttonSearchPhoto1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxAngleBetween12;
        private System.Windows.Forms.TextBox textBoxAngleBetween31;
        private System.Windows.Forms.Label label176;
        private System.Windows.Forms.TextBox textBoxAngleBetween23;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBoxPhoto2Pattern;
        private System.Windows.Forms.Button buttonSearchPhoto2;
        private System.Windows.Forms.RadioButton radioButtonPhoto2Mode2;
        private System.Windows.Forms.RadioButton radioButtonPhoto2Mode1;
        private Crystallography.Controls.NumericBox numericBoxPhoto2L1Err;
        private Crystallography.Controls.NumericBox numericBoxPhoto2L2Err;
        private Crystallography.Controls.NumericBox numericBoxPhoto2ThetaErr;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private Crystallography.Controls.NumericBox numericBoxPhoto2L3Err;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.PictureBox pictureBoxPhoto2;
        private System.Windows.Forms.GroupBox groupBoxPhoto2HolderCondition;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private Crystallography.Controls.NumericBox numericBoxPhoto2Tilt1Err;
        private System.Windows.Forms.Label label8;
        private Crystallography.Controls.NumericBox numericBoxPhoto2Tilt2Err;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBoxPhoto2;
        private System.Windows.Forms.GroupBox groupBoxPhoto3Pattern;
        private System.Windows.Forms.Button buttonSearchPhoto3;
        private System.Windows.Forms.RadioButton radioButtonPhoto3Mode2;
        private System.Windows.Forms.RadioButton radioButtonPhoto3Mode1;
        private Crystallography.Controls.NumericBox numericBoxPhoto3L1Err;
        private Crystallography.Controls.NumericBox numericBoxPhoto3L2Err;
        private Crystallography.Controls.NumericBox numericBoxPhoto3ThetaErr;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private Crystallography.Controls.NumericBox numericBoxPhoto3L3Err;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.PictureBox pictureBoxPhoto3;
        private System.Windows.Forms.GroupBox groupBoxPhoto3HolderCondition;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private Crystallography.Controls.NumericBox numericBoxPhoto3Tilt1Err;
        private System.Windows.Forms.Label label36;
        private Crystallography.Controls.NumericBox numericBoxPhoto3Tilt2Err;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.GroupBox groupBoxPhoto3;
        private System.Windows.Forms.CheckBox checkBoxPhoto2;
        private System.Windows.Forms.CheckBox checkBoxPhoto3;
        private System.Windows.Forms.CheckBox checkBoxEquivalentPhoto1L1Photo2L1;
        private System.Windows.Forms.CheckBox checkBoxEquivalentPhoto2L1Photo3L1;
        private System.Windows.Forms.CheckBox checkBoxEquivalentPhoto2L2Photo3L2;
        private System.Windows.Forms.Button buttonSearchAll;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox checkBoxEquivalentPhoto1L2Photo2L2;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.TextBox textBoxWaveLength;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPhotos;
        private System.Windows.Forms.Panel panelPhoto2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panelPhoto3;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label98;
        private InputBox inputBoxP1L1;
        private InputBox inputBoxP1L2;
        private InputBox inputBoxP1L3;
        private InputBox inputBoxP2L1;
        private InputBox inputBoxP2L2;
        private InputBox inputBoxP2L3;
        private InputBox inputBoxP3L1;
        private InputBox inputBoxP3L2;
        private InputBox inputBoxP3L3;
        private Crystallography.Controls.NumericBox numericBoxP1Tilt2;
        private Crystallography.Controls.NumericBox numericBoxP1Tilt1;
        private Crystallography.Controls.NumericBox numericBoxP2Tilt2;
        private Crystallography.Controls.NumericBox numericBoxP2Tilt1;
        private Crystallography.Controls.NumericBox numericBoxP3Tilt2;
        private Crystallography.Controls.NumericBox numericBoxP3Tilt1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private NumericBox numericBoxP1Theta;
        private NumericBox numericBoxP2Theta;
        private NumericBox numericBoxP3Theta;
    }
}