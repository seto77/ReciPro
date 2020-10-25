namespace Crystallography.Controls
{
    partial class EOSControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label83 = new System.Windows.Forms.Label();
            this.textBoxEOS_Note = new System.Windows.Forms.TextBox();
            this.checkBoxUseEOS = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonMieGruneisen = new System.Windows.Forms.RadioButton();
            this.radioButtonTdependenceK0andV0 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonEOS_ThirdBirchMurnaghan = new System.Windows.Forms.RadioButton();
            this.radioButtonEOS_FourthBirchMunaghan = new System.Windows.Forms.RadioButton();
            this.radioButtonEOS_Vinet = new System.Windows.Forms.RadioButton();
            this.radioButtonEOS_AP2 = new System.Windows.Forms.RadioButton();
            this.radioButtonEOS_Keane = new System.Windows.Forms.RadioButton();
            this.radioButtonEOS_Vinet3rd = new System.Windows.Forms.RadioButton();
            this.numericBoxEOS_Gamma0 = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_Theta0 = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_Q = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_C = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_B = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_KperT = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_A = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_V0perCell = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_V0perMol = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_K0 = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_Kp0 = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_Kpp0 = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_KpInfinity = new Crystallography.Controls.NumericBox();
            this.numericBox3rdVinetIta = new Crystallography.Controls.NumericBox();
            this.numericBox3rdVinetBeta = new Crystallography.Controls.NumericBox();
            this.numericBox3rdVinetPsi = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_T0 = new Crystallography.Controls.NumericBox();
            this.numericBoxTemperature = new Crystallography.Controls.NumericBox();
            this.numericBoxPressure = new Crystallography.Controls.NumericBox();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("Arial", 9F);
            this.label83.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label83.Location = new System.Drawing.Point(196, 206);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(33, 15);
            this.label83.TabIndex = 67;
            this.label83.Text = "Note";
            // 
            // textBoxEOS_Note
            // 
            this.textBoxEOS_Note.Location = new System.Drawing.Point(232, 202);
            this.textBoxEOS_Note.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxEOS_Note.Name = "textBoxEOS_Note";
            this.textBoxEOS_Note.Size = new System.Drawing.Size(238, 23);
            this.textBoxEOS_Note.TabIndex = 66;
            this.textBoxEOS_Note.TextChanged += new System.EventHandler(this.parameters_Changed);
            // 
            // checkBoxUseEOS
            // 
            this.checkBoxUseEOS.AutoSize = true;
            this.checkBoxUseEOS.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxUseEOS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxUseEOS.Location = new System.Drawing.Point(10, 4);
            this.checkBoxUseEOS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxUseEOS.Name = "checkBoxUseEOS";
            this.checkBoxUseEOS.Size = new System.Drawing.Size(69, 19);
            this.checkBoxUseEOS.TabIndex = 62;
            this.checkBoxUseEOS.Text = "Use EOS";
            this.checkBoxUseEOS.UseVisualStyleBackColor = true;
            this.checkBoxUseEOS.CheckedChanged += new System.EventHandler(this.parameters_Changed);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.flowLayoutPanel1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numericBoxEOS_C);
            this.groupBox3.Controls.Add(this.numericBoxEOS_B);
            this.groupBox3.Controls.Add(this.numericBoxEOS_KperT);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.numericBoxEOS_A);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.radioButtonMieGruneisen);
            this.groupBox3.Controls.Add(this.radioButtonTdependenceK0andV0);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(193, 26);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(281, 173);
            this.groupBox3.TabIndex = 65;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thermal pressure";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.numericBoxEOS_Gamma0);
            this.flowLayoutPanel1.Controls.Add(this.numericBoxEOS_Theta0);
            this.flowLayoutPanel1.Controls.Add(this.numericBoxEOS_Q);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(103, 18);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(171, 26);
            this.flowLayoutPanel1.TabIndex = 68;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 124);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 30);
            this.label1.TabIndex = 10;
            this.label1.Text = "K(T,P=0)=K(T₀, 0)+[∂K(T, 0)/∂T] (T - T₀)\r\nV(T,P=0) = V₀ exp[∫(a+bT+c/T²)dT ] ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(312, 255);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 15);
            this.label13.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(6, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 1;
            // 
            // radioButtonMieGruneisen
            // 
            this.radioButtonMieGruneisen.AutoSize = true;
            this.radioButtonMieGruneisen.Checked = true;
            this.radioButtonMieGruneisen.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.radioButtonMieGruneisen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonMieGruneisen.Location = new System.Drawing.Point(4, 21);
            this.radioButtonMieGruneisen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonMieGruneisen.Name = "radioButtonMieGruneisen";
            this.radioButtonMieGruneisen.Size = new System.Drawing.Size(103, 19);
            this.radioButtonMieGruneisen.TabIndex = 0;
            this.radioButtonMieGruneisen.TabStop = true;
            this.radioButtonMieGruneisen.Text = "Mie-Grüneisen";
            this.radioButtonMieGruneisen.UseVisualStyleBackColor = true;
            this.radioButtonMieGruneisen.CheckedChanged += new System.EventHandler(this.parameters_Changed);
            // 
            // radioButtonTdependenceK0andV0
            // 
            this.radioButtonTdependenceK0andV0.AutoSize = true;
            this.radioButtonTdependenceK0andV0.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.radioButtonTdependenceK0andV0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonTdependenceK0andV0.Location = new System.Drawing.Point(4, 47);
            this.radioButtonTdependenceK0andV0.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButtonTdependenceK0andV0.Name = "radioButtonTdependenceK0andV0";
            this.radioButtonTdependenceK0andV0.Size = new System.Drawing.Size(143, 19);
            this.radioButtonTdependenceK0andV0.TabIndex = 1;
            this.radioButtonTdependenceK0andV0.Text = "T-dependence K₀ && V₀";
            this.radioButtonTdependenceK0andV0.UseVisualStyleBackColor = true;
            this.radioButtonTdependenceK0andV0.CheckedChanged += new System.EventHandler(this.parameters_Changed);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel5);
            this.groupBox2.Controls.Add(this.flowLayoutPanel6);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.groupBox2.Location = new System.Drawing.Point(0, 26);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(191, 198);
            this.groupBox2.TabIndex = 64;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Isothermal pressure at T₀";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoScroll = true;
            this.flowLayoutPanel5.AutoSize = true;
            this.flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel5.Controls.Add(this.numericBoxEOS_V0perCell);
            this.flowLayoutPanel5.Controls.Add(this.numericBoxEOS_V0perMol);
            this.flowLayoutPanel5.Controls.Add(this.numericBoxEOS_K0);
            this.flowLayoutPanel5.Controls.Add(this.numericBoxEOS_Kp0);
            this.flowLayoutPanel5.Controls.Add(this.numericBoxEOS_Kpp0);
            this.flowLayoutPanel5.Controls.Add(this.numericBoxEOS_KpInfinity);
            this.flowLayoutPanel5.Controls.Add(this.numericBox3rdVinetIta);
            this.flowLayoutPanel5.Controls.Add(this.numericBox3rdVinetBeta);
            this.flowLayoutPanel5.Controls.Add(this.numericBox3rdVinetPsi);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 66);
            this.flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(185, 128);
            this.flowLayoutPanel5.TabIndex = 60;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.AutoSize = true;
            this.flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel6.Controls.Add(this.radioButtonEOS_ThirdBirchMurnaghan);
            this.flowLayoutPanel6.Controls.Add(this.radioButtonEOS_FourthBirchMunaghan);
            this.flowLayoutPanel6.Controls.Add(this.radioButtonEOS_Vinet);
            this.flowLayoutPanel6.Controls.Add(this.radioButtonEOS_Vinet3rd);
            this.flowLayoutPanel6.Controls.Add(this.radioButtonEOS_AP2);
            this.flowLayoutPanel6.Controls.Add(this.radioButtonEOS_Keane);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 20);
            this.flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.flowLayoutPanel6.Size = new System.Drawing.Size(185, 46);
            this.flowLayoutPanel6.TabIndex = 60;
            // 
            // radioButtonEOS_ThirdBirchMurnaghan
            // 
            this.radioButtonEOS_ThirdBirchMurnaghan.AutoSize = true;
            this.radioButtonEOS_ThirdBirchMurnaghan.Checked = true;
            this.radioButtonEOS_ThirdBirchMurnaghan.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.radioButtonEOS_ThirdBirchMurnaghan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonEOS_ThirdBirchMurnaghan.Location = new System.Drawing.Point(0, 1);
            this.radioButtonEOS_ThirdBirchMurnaghan.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.radioButtonEOS_ThirdBirchMurnaghan.Name = "radioButtonEOS_ThirdBirchMurnaghan";
            this.radioButtonEOS_ThirdBirchMurnaghan.Size = new System.Drawing.Size(63, 19);
            this.radioButtonEOS_ThirdBirchMurnaghan.TabIndex = 5;
            this.radioButtonEOS_ThirdBirchMurnaghan.TabStop = true;
            this.radioButtonEOS_ThirdBirchMurnaghan.Text = "3rd BM";
            this.radioButtonEOS_ThirdBirchMurnaghan.UseVisualStyleBackColor = true;
            this.radioButtonEOS_ThirdBirchMurnaghan.CheckedChanged += new System.EventHandler(this.parameters_Changed);
            // 
            // radioButtonEOS_FourthBirchMunaghan
            // 
            this.radioButtonEOS_FourthBirchMunaghan.AutoSize = true;
            this.radioButtonEOS_FourthBirchMunaghan.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.radioButtonEOS_FourthBirchMunaghan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonEOS_FourthBirchMunaghan.Location = new System.Drawing.Point(63, 1);
            this.radioButtonEOS_FourthBirchMunaghan.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.radioButtonEOS_FourthBirchMunaghan.Name = "radioButtonEOS_FourthBirchMunaghan";
            this.radioButtonEOS_FourthBirchMunaghan.Size = new System.Drawing.Size(63, 19);
            this.radioButtonEOS_FourthBirchMunaghan.TabIndex = 6;
            this.radioButtonEOS_FourthBirchMunaghan.Text = "4th BM";
            this.radioButtonEOS_FourthBirchMunaghan.UseVisualStyleBackColor = true;
            this.radioButtonEOS_FourthBirchMunaghan.CheckedChanged += new System.EventHandler(this.parameters_Changed);
            // 
            // radioButtonEOS_Vinet
            // 
            this.radioButtonEOS_Vinet.AutoSize = true;
            this.radioButtonEOS_Vinet.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.radioButtonEOS_Vinet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonEOS_Vinet.Location = new System.Drawing.Point(126, 1);
            this.radioButtonEOS_Vinet.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.radioButtonEOS_Vinet.Name = "radioButtonEOS_Vinet";
            this.radioButtonEOS_Vinet.Size = new System.Drawing.Size(52, 19);
            this.radioButtonEOS_Vinet.TabIndex = 6;
            this.radioButtonEOS_Vinet.Text = "Vinet";
            this.radioButtonEOS_Vinet.UseVisualStyleBackColor = true;
            this.radioButtonEOS_Vinet.CheckedChanged += new System.EventHandler(this.parameters_Changed);
            // 
            // radioButtonEOS_AP2
            // 
            this.radioButtonEOS_AP2.AutoSize = true;
            this.radioButtonEOS_AP2.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.radioButtonEOS_AP2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonEOS_AP2.Location = new System.Drawing.Point(72, 21);
            this.radioButtonEOS_AP2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.radioButtonEOS_AP2.Name = "radioButtonEOS_AP2";
            this.radioButtonEOS_AP2.Size = new System.Drawing.Size(46, 19);
            this.radioButtonEOS_AP2.TabIndex = 6;
            this.radioButtonEOS_AP2.Text = "AP2";
            this.radioButtonEOS_AP2.UseVisualStyleBackColor = true;
            this.radioButtonEOS_AP2.CheckedChanged += new System.EventHandler(this.parameters_Changed);
            // 
            // radioButtonEOS_Keane
            // 
            this.radioButtonEOS_Keane.AutoSize = true;
            this.radioButtonEOS_Keane.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.radioButtonEOS_Keane.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonEOS_Keane.Location = new System.Drawing.Point(118, 21);
            this.radioButtonEOS_Keane.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.radioButtonEOS_Keane.Name = "radioButtonEOS_Keane";
            this.radioButtonEOS_Keane.Size = new System.Drawing.Size(57, 19);
            this.radioButtonEOS_Keane.TabIndex = 6;
            this.radioButtonEOS_Keane.Text = "Keane";
            this.radioButtonEOS_Keane.UseVisualStyleBackColor = true;
            this.radioButtonEOS_Keane.CheckedChanged += new System.EventHandler(this.parameters_Changed);
            // 
            // radioButtonEOS_Vinet3rd
            // 
            this.radioButtonEOS_Vinet3rd.AutoSize = true;
            this.radioButtonEOS_Vinet3rd.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.radioButtonEOS_Vinet3rd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.radioButtonEOS_Vinet3rd.Location = new System.Drawing.Point(0, 21);
            this.radioButtonEOS_Vinet3rd.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.radioButtonEOS_Vinet3rd.Name = "radioButtonEOS_Vinet3rd";
            this.radioButtonEOS_Vinet3rd.Size = new System.Drawing.Size(72, 19);
            this.radioButtonEOS_Vinet3rd.TabIndex = 6;
            this.radioButtonEOS_Vinet3rd.Text = "3rd Vinet";
            this.radioButtonEOS_Vinet3rd.UseVisualStyleBackColor = true;
            this.radioButtonEOS_Vinet3rd.CheckedChanged += new System.EventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_Gamma0
            // 
            this.numericBoxEOS_Gamma0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_Gamma0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Gamma0.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_Gamma0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Gamma0.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Gamma0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Gamma0.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Gamma0.HeaderText = "γ₀";
            this.numericBoxEOS_Gamma0.Location = new System.Drawing.Point(0, 1);
            this.numericBoxEOS_Gamma0.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.numericBoxEOS_Gamma0.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_Gamma0.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_Gamma0.Name = "numericBoxEOS_Gamma0";
            this.numericBoxEOS_Gamma0.RestrictLimitValue = false;
            this.numericBoxEOS_Gamma0.Size = new System.Drawing.Size(52, 25);
            this.numericBoxEOS_Gamma0.SkipEventDuringInput = false;
            this.numericBoxEOS_Gamma0.SmartIncrement = true;
            this.numericBoxEOS_Gamma0.TabIndex = 2;
            this.numericBoxEOS_Gamma0.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Gamma0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_Theta0
            // 
            this.numericBoxEOS_Theta0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_Theta0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Theta0.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_Theta0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Theta0.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Theta0.FooterText = "K";
            this.numericBoxEOS_Theta0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Theta0.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Theta0.HeaderText = "θ₀";
            this.numericBoxEOS_Theta0.Location = new System.Drawing.Point(52, 1);
            this.numericBoxEOS_Theta0.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.numericBoxEOS_Theta0.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_Theta0.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_Theta0.Name = "numericBoxEOS_Theta0";
            this.numericBoxEOS_Theta0.RadianValue = 5.2359877559829888D;
            this.numericBoxEOS_Theta0.RestrictLimitValue = false;
            this.numericBoxEOS_Theta0.Size = new System.Drawing.Size(72, 25);
            this.numericBoxEOS_Theta0.SkipEventDuringInput = false;
            this.numericBoxEOS_Theta0.SmartIncrement = true;
            this.numericBoxEOS_Theta0.TabIndex = 3;
            this.numericBoxEOS_Theta0.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Theta0.Value = 300D;
            this.numericBoxEOS_Theta0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_Q
            // 
            this.numericBoxEOS_Q.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_Q.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Q.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_Q.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Q.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Q.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Q.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Q.HeaderText = "q";
            this.numericBoxEOS_Q.Location = new System.Drawing.Point(124, 1);
            this.numericBoxEOS_Q.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.numericBoxEOS_Q.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_Q.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_Q.Name = "numericBoxEOS_Q";
            this.numericBoxEOS_Q.RestrictLimitValue = false;
            this.numericBoxEOS_Q.Size = new System.Drawing.Size(47, 25);
            this.numericBoxEOS_Q.SkipEventDuringInput = false;
            this.numericBoxEOS_Q.SmartIncrement = true;
            this.numericBoxEOS_Q.TabIndex = 4;
            this.numericBoxEOS_Q.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Q.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_C
            // 
            this.numericBoxEOS_C.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_C.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_C.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_C.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_C.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_C.FooterText = "K";
            this.numericBoxEOS_C.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_C.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_C.HeaderText = "c";
            this.numericBoxEOS_C.Location = new System.Drawing.Point(205, 93);
            this.numericBoxEOS_C.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.numericBoxEOS_C.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_C.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_C.Name = "numericBoxEOS_C";
            this.numericBoxEOS_C.RestrictLimitValue = false;
            this.numericBoxEOS_C.Size = new System.Drawing.Size(72, 25);
            this.numericBoxEOS_C.SkipEventDuringInput = false;
            this.numericBoxEOS_C.SmartIncrement = true;
            this.numericBoxEOS_C.TabIndex = 9;
            this.numericBoxEOS_C.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_C.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_B
            // 
            this.numericBoxEOS_B.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_B.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_B.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_B.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_B.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_B.FooterText = "10⁻⁹/K²";
            this.numericBoxEOS_B.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_B.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_B.HeaderText = "b";
            this.numericBoxEOS_B.Location = new System.Drawing.Point(104, 93);
            this.numericBoxEOS_B.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.numericBoxEOS_B.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_B.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_B.Name = "numericBoxEOS_B";
            this.numericBoxEOS_B.RestrictLimitValue = false;
            this.numericBoxEOS_B.Size = new System.Drawing.Size(98, 25);
            this.numericBoxEOS_B.SkipEventDuringInput = false;
            this.numericBoxEOS_B.SmartIncrement = true;
            this.numericBoxEOS_B.TabIndex = 8;
            this.numericBoxEOS_B.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_B.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_KperT
            // 
            this.numericBoxEOS_KperT.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_KperT.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KperT.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_KperT.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KperT.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_KperT.FooterText = "GPa/K";
            this.numericBoxEOS_KperT.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KperT.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_KperT.HeaderText = "∂K(T,P=0)/∂T";
            this.numericBoxEOS_KperT.Location = new System.Drawing.Point(7, 67);
            this.numericBoxEOS_KperT.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.numericBoxEOS_KperT.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_KperT.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_KperT.Name = "numericBoxEOS_KperT";
            this.numericBoxEOS_KperT.RestrictLimitValue = false;
            this.numericBoxEOS_KperT.Size = new System.Drawing.Size(177, 25);
            this.numericBoxEOS_KperT.SkipEventDuringInput = false;
            this.numericBoxEOS_KperT.SmartIncrement = true;
            this.numericBoxEOS_KperT.TabIndex = 6;
            this.numericBoxEOS_KperT.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_KperT.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_A
            // 
            this.numericBoxEOS_A.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_A.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_A.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_A.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_A.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_A.FooterText = "10⁻⁵/K";
            this.numericBoxEOS_A.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_A.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_A.HeaderText = "a";
            this.numericBoxEOS_A.Location = new System.Drawing.Point(5, 93);
            this.numericBoxEOS_A.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.numericBoxEOS_A.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_A.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_A.Name = "numericBoxEOS_A";
            this.numericBoxEOS_A.RestrictLimitValue = false;
            this.numericBoxEOS_A.Size = new System.Drawing.Size(94, 25);
            this.numericBoxEOS_A.SkipEventDuringInput = false;
            this.numericBoxEOS_A.SmartIncrement = true;
            this.numericBoxEOS_A.TabIndex = 7;
            this.numericBoxEOS_A.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_A.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_V0perCell
            // 
            this.numericBoxEOS_V0perCell.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_V0perCell.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perCell.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_V0perCell.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perCell.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_V0perCell.FooterText = "Å³/cell";
            this.numericBoxEOS_V0perCell.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perCell.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_V0perCell.HeaderText = "V₀";
            this.numericBoxEOS_V0perCell.Location = new System.Drawing.Point(0, 0);
            this.numericBoxEOS_V0perCell.Margin = new System.Windows.Forms.Padding(0, 0, 5, 1);
            this.numericBoxEOS_V0perCell.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_V0perCell.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_V0perCell.Name = "numericBoxEOS_V0perCell";
            this.numericBoxEOS_V0perCell.RadianValue = 5.2359877559829888D;
            this.numericBoxEOS_V0perCell.RestrictLimitValue = false;
            this.numericBoxEOS_V0perCell.Size = new System.Drawing.Size(125, 25);
            this.numericBoxEOS_V0perCell.SkipEventDuringInput = false;
            this.numericBoxEOS_V0perCell.SmartIncrement = true;
            this.numericBoxEOS_V0perCell.TabIndex = 0;
            this.numericBoxEOS_V0perCell.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_V0perCell.Value = 300D;
            this.numericBoxEOS_V0perCell.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            this.numericBoxEOS_V0perCell.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxEOS_V0perCell_Click2);
            // 
            // numericBoxEOS_V0perMol
            // 
            this.numericBoxEOS_V0perMol.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_V0perMol.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perMol.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_V0perMol.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perMol.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_V0perMol.FooterText = "cm³/mol";
            this.numericBoxEOS_V0perMol.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perMol.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_V0perMol.HeaderText = "V₀";
            this.numericBoxEOS_V0perMol.Location = new System.Drawing.Point(0, 26);
            this.numericBoxEOS_V0perMol.Margin = new System.Windows.Forms.Padding(0, 0, 5, 1);
            this.numericBoxEOS_V0perMol.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_V0perMol.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_V0perMol.Name = "numericBoxEOS_V0perMol";
            this.numericBoxEOS_V0perMol.RadianValue = 5.2359877559829888D;
            this.numericBoxEOS_V0perMol.ReadOnly = true;
            this.numericBoxEOS_V0perMol.RestrictLimitValue = false;
            this.numericBoxEOS_V0perMol.Size = new System.Drawing.Size(135, 25);
            this.numericBoxEOS_V0perMol.SkipEventDuringInput = false;
            this.numericBoxEOS_V0perMol.SmartIncrement = true;
            this.numericBoxEOS_V0perMol.TabIndex = 1;
            this.numericBoxEOS_V0perMol.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perMol.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_V0perMol.Value = 300D;
            this.numericBoxEOS_V0perMol.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxEOS_V0perMol_Click2);
            // 
            // numericBoxEOS_K0
            // 
            this.numericBoxEOS_K0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_K0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_K0.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_K0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_K0.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_K0.FooterText = "GPa";
            this.numericBoxEOS_K0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_K0.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_K0.HeaderText = "K₀";
            this.numericBoxEOS_K0.Location = new System.Drawing.Point(0, 52);
            this.numericBoxEOS_K0.Margin = new System.Windows.Forms.Padding(0, 0, 5, 1);
            this.numericBoxEOS_K0.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_K0.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_K0.Name = "numericBoxEOS_K0";
            this.numericBoxEOS_K0.RestrictLimitValue = false;
            this.numericBoxEOS_K0.Size = new System.Drawing.Size(98, 25);
            this.numericBoxEOS_K0.SkipEventDuringInput = false;
            this.numericBoxEOS_K0.SmartIncrement = true;
            this.numericBoxEOS_K0.TabIndex = 2;
            this.numericBoxEOS_K0.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_K0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_Kp0
            // 
            this.numericBoxEOS_Kp0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_Kp0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Kp0.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_Kp0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Kp0.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Kp0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Kp0.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Kp0.HeaderText = "K\'₀";
            this.numericBoxEOS_Kp0.Location = new System.Drawing.Point(0, 78);
            this.numericBoxEOS_Kp0.Margin = new System.Windows.Forms.Padding(0, 0, 5, 1);
            this.numericBoxEOS_Kp0.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_Kp0.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_Kp0.Name = "numericBoxEOS_Kp0";
            this.numericBoxEOS_Kp0.RadianValue = 0.069813170079773182D;
            this.numericBoxEOS_Kp0.RestrictLimitValue = false;
            this.numericBoxEOS_Kp0.Size = new System.Drawing.Size(70, 25);
            this.numericBoxEOS_Kp0.SkipEventDuringInput = false;
            this.numericBoxEOS_Kp0.SmartIncrement = true;
            this.numericBoxEOS_Kp0.TabIndex = 3;
            this.numericBoxEOS_Kp0.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Kp0.Value = 4D;
            this.numericBoxEOS_Kp0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_Kpp0
            // 
            this.numericBoxEOS_Kpp0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_Kpp0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Kpp0.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_Kpp0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Kpp0.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Kpp0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Kpp0.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Kpp0.HeaderText = "K\'\'₀";
            this.numericBoxEOS_Kpp0.Location = new System.Drawing.Point(75, 78);
            this.numericBoxEOS_Kpp0.Margin = new System.Windows.Forms.Padding(0, 0, 5, 1);
            this.numericBoxEOS_Kpp0.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_Kpp0.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_Kpp0.Name = "numericBoxEOS_Kpp0";
            this.numericBoxEOS_Kpp0.RadianValue = 0.069813170079773182D;
            this.numericBoxEOS_Kpp0.RestrictLimitValue = false;
            this.numericBoxEOS_Kpp0.Size = new System.Drawing.Size(70, 25);
            this.numericBoxEOS_Kpp0.SkipEventDuringInput = false;
            this.numericBoxEOS_Kpp0.SmartIncrement = true;
            this.numericBoxEOS_Kpp0.TabIndex = 3;
            this.numericBoxEOS_Kpp0.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_Kpp0.Value = 4D;
            this.numericBoxEOS_Kpp0.Visible = false;
            this.numericBoxEOS_Kpp0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_KpInfinity
            // 
            this.numericBoxEOS_KpInfinity.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_KpInfinity.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KpInfinity.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_KpInfinity.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KpInfinity.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_KpInfinity.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KpInfinity.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_KpInfinity.HeaderText = "K\'∞";
            this.numericBoxEOS_KpInfinity.Location = new System.Drawing.Point(0, 104);
            this.numericBoxEOS_KpInfinity.Margin = new System.Windows.Forms.Padding(0, 0, 5, 1);
            this.numericBoxEOS_KpInfinity.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_KpInfinity.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEOS_KpInfinity.Name = "numericBoxEOS_KpInfinity";
            this.numericBoxEOS_KpInfinity.RadianValue = 0.069813170079773182D;
            this.numericBoxEOS_KpInfinity.RestrictLimitValue = false;
            this.numericBoxEOS_KpInfinity.Size = new System.Drawing.Size(70, 25);
            this.numericBoxEOS_KpInfinity.SkipEventDuringInput = false;
            this.numericBoxEOS_KpInfinity.SmartIncrement = true;
            this.numericBoxEOS_KpInfinity.TabIndex = 3;
            this.numericBoxEOS_KpInfinity.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEOS_KpInfinity.Value = 4D;
            this.numericBoxEOS_KpInfinity.Visible = false;
            this.numericBoxEOS_KpInfinity.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBox3rdVinetIta
            // 
            this.numericBox3rdVinetIta.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBox3rdVinetIta.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox3rdVinetIta.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBox3rdVinetIta.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox3rdVinetIta.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBox3rdVinetIta.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox3rdVinetIta.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBox3rdVinetIta.HeaderText = "η";
            this.numericBox3rdVinetIta.Location = new System.Drawing.Point(75, 104);
            this.numericBox3rdVinetIta.Margin = new System.Windows.Forms.Padding(0, 0, 5, 1);
            this.numericBox3rdVinetIta.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBox3rdVinetIta.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBox3rdVinetIta.Name = "numericBox3rdVinetIta";
            this.numericBox3rdVinetIta.RestrictLimitValue = false;
            this.numericBox3rdVinetIta.Size = new System.Drawing.Size(70, 25);
            this.numericBox3rdVinetIta.SkipEventDuringInput = false;
            this.numericBox3rdVinetIta.SmartIncrement = true;
            this.numericBox3rdVinetIta.TabIndex = 3;
            this.numericBox3rdVinetIta.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBox3rdVinetIta.Visible = false;
            this.numericBox3rdVinetIta.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBox3rdVinetBeta
            // 
            this.numericBox3rdVinetBeta.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBox3rdVinetBeta.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox3rdVinetBeta.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBox3rdVinetBeta.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox3rdVinetBeta.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBox3rdVinetBeta.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox3rdVinetBeta.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBox3rdVinetBeta.HeaderText = "β";
            this.numericBox3rdVinetBeta.Location = new System.Drawing.Point(0, 130);
            this.numericBox3rdVinetBeta.Margin = new System.Windows.Forms.Padding(0, 0, 5, 1);
            this.numericBox3rdVinetBeta.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBox3rdVinetBeta.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBox3rdVinetBeta.Name = "numericBox3rdVinetBeta";
            this.numericBox3rdVinetBeta.RestrictLimitValue = false;
            this.numericBox3rdVinetBeta.Size = new System.Drawing.Size(68, 25);
            this.numericBox3rdVinetBeta.SkipEventDuringInput = false;
            this.numericBox3rdVinetBeta.SmartIncrement = true;
            this.numericBox3rdVinetBeta.TabIndex = 3;
            this.numericBox3rdVinetBeta.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBox3rdVinetBeta.Visible = false;
            this.numericBox3rdVinetBeta.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBox3rdVinetPsi
            // 
            this.numericBox3rdVinetPsi.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBox3rdVinetPsi.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox3rdVinetPsi.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBox3rdVinetPsi.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox3rdVinetPsi.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBox3rdVinetPsi.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox3rdVinetPsi.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBox3rdVinetPsi.HeaderText = "ψ";
            this.numericBox3rdVinetPsi.Location = new System.Drawing.Point(73, 130);
            this.numericBox3rdVinetPsi.Margin = new System.Windows.Forms.Padding(0, 0, 5, 1);
            this.numericBox3rdVinetPsi.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBox3rdVinetPsi.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBox3rdVinetPsi.Name = "numericBox3rdVinetPsi";
            this.numericBox3rdVinetPsi.RestrictLimitValue = false;
            this.numericBox3rdVinetPsi.Size = new System.Drawing.Size(70, 25);
            this.numericBox3rdVinetPsi.SkipEventDuringInput = false;
            this.numericBox3rdVinetPsi.SmartIncrement = true;
            this.numericBox3rdVinetPsi.TabIndex = 3;
            this.numericBox3rdVinetPsi.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBox3rdVinetPsi.Visible = false;
            this.numericBox3rdVinetPsi.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxEOS_T0
            // 
            this.numericBoxEOS_T0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEOS_T0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_T0.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxEOS_T0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_T0.FooterText = "K";
            this.numericBoxEOS_T0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_T0.HeaderText = "T₀";
            this.numericBoxEOS_T0.Location = new System.Drawing.Point(95, 1);
            this.numericBoxEOS_T0.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.numericBoxEOS_T0.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEOS_T0.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxEOS_T0.Name = "numericBoxEOS_T0";
            this.numericBoxEOS_T0.RadianValue = 5.2359877559829888D;
            this.numericBoxEOS_T0.RestrictLimitValue = false;
            this.numericBoxEOS_T0.Size = new System.Drawing.Size(82, 25);
            this.numericBoxEOS_T0.SkipEventDuringInput = false;
            this.numericBoxEOS_T0.SmartIncrement = true;
            this.numericBoxEOS_T0.TabIndex = 60;
            this.numericBoxEOS_T0.Value = 300D;
            this.numericBoxEOS_T0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxTemperature
            // 
            this.numericBoxTemperature.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxTemperature.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTemperature.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxTemperature.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTemperature.FooterText = "K";
            this.numericBoxTemperature.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTemperature.HeaderText = "T";
            this.numericBoxTemperature.Location = new System.Drawing.Point(198, 1);
            this.numericBoxTemperature.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.numericBoxTemperature.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxTemperature.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxTemperature.Name = "numericBoxTemperature";
            this.numericBoxTemperature.RadianValue = 5.2359877559829888D;
            this.numericBoxTemperature.RestrictLimitValue = false;
            this.numericBoxTemperature.Size = new System.Drawing.Size(87, 25);
            this.numericBoxTemperature.SkipEventDuringInput = false;
            this.numericBoxTemperature.SmartIncrement = true;
            this.numericBoxTemperature.TabIndex = 63;
            this.numericBoxTemperature.Value = 300D;
            this.numericBoxTemperature.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.parameters_Changed);
            // 
            // numericBoxPressure
            // 
            this.numericBoxPressure.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxPressure.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPressure.DecimalPlaces = 5;
            this.numericBoxPressure.Font = new System.Drawing.Font("Arial", 9F);
            this.numericBoxPressure.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPressure.FooterText = "GPa";
            this.numericBoxPressure.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPressure.HeaderText = "P";
            this.numericBoxPressure.Location = new System.Drawing.Point(310, 2);
            this.numericBoxPressure.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.numericBoxPressure.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPressure.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPressure.Name = "numericBoxPressure";
            this.numericBoxPressure.ReadOnly = true;
            this.numericBoxPressure.RestrictLimitValue = false;
            this.numericBoxPressure.Size = new System.Drawing.Size(132, 25);
            this.numericBoxPressure.SkipEventDuringInput = false;
            this.numericBoxPressure.SmartIncrement = true;
            this.numericBoxPressure.TabIndex = 61;
            this.numericBoxPressure.TextBoxBackColor = System.Drawing.SystemColors.Control;
            // 
            // EOSControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.label83);
            this.Controls.Add(this.textBoxEOS_Note);
            this.Controls.Add(this.checkBoxUseEOS);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.numericBoxEOS_T0);
            this.Controls.Add(this.numericBoxTemperature);
            this.Controls.Add(this.numericBoxPressure);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "EOSControl";
            this.Size = new System.Drawing.Size(477, 227);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.TextBox textBoxEOS_Note;
        private System.Windows.Forms.CheckBox checkBoxUseEOS;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private NumericBox numericBoxEOS_C;
        private NumericBox numericBoxEOS_B;
        private NumericBox numericBoxEOS_KperT;
        private NumericBox numericBoxEOS_Gamma0;
        private System.Windows.Forms.Label label13;
        private NumericBox numericBoxEOS_A;
        private NumericBox numericBoxEOS_Theta0;
        private NumericBox numericBoxEOS_Q;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonMieGruneisen;
        private System.Windows.Forms.RadioButton radioButtonTdependenceK0andV0;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private NumericBox numericBoxEOS_V0perCell;
        private NumericBox numericBoxEOS_V0perMol;
        private NumericBox numericBoxEOS_K0;
        private NumericBox numericBoxEOS_Kp0;
        private NumericBox numericBoxEOS_Kpp0;
        private NumericBox numericBoxEOS_KpInfinity;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.RadioButton radioButtonEOS_ThirdBirchMurnaghan;
        private System.Windows.Forms.RadioButton radioButtonEOS_FourthBirchMunaghan;
        private System.Windows.Forms.RadioButton radioButtonEOS_Vinet;
        private System.Windows.Forms.RadioButton radioButtonEOS_AP2;
        private System.Windows.Forms.RadioButton radioButtonEOS_Keane;
        private NumericBox numericBoxEOS_T0;
        private NumericBox numericBoxTemperature;
        private NumericBox numericBoxPressure;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private NumericBox numericBox3rdVinetIta;
        private NumericBox numericBox3rdVinetBeta;
        private NumericBox numericBox3rdVinetPsi;
        private System.Windows.Forms.RadioButton radioButtonEOS_Vinet3rd;
    }
}
