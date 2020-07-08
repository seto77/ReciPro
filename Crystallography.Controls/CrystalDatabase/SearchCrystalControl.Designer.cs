namespace Crystallography.Controls
{
    partial class SearchCrystalControl
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxSearchName = new System.Windows.Forms.CheckBox();
            this.textBoxSearchName = new System.Windows.Forms.TextBox();
            this.checkBoxSearchElements = new System.Windows.Forms.CheckBox();
            this.buttonPeriodicTable = new System.Windows.Forms.Button();
            this.checkBoxSearchRefference = new System.Windows.Forms.CheckBox();
            this.textBoxSearchRefference = new System.Windows.Forms.TextBox();
            this.checkBoxSearchCrystalSystem = new System.Windows.Forms.CheckBox();
            this.comboBoxSearchCrystalSystem = new System.Windows.Forms.ComboBox();
            this.checkBoxSearchCellParameter = new System.Windows.Forms.CheckBox();
            this.groupBoxCellParameter = new System.Windows.Forms.GroupBox();
            this.numericBoxCellGamma = new Crystallography.Controls.NumericBox();
            this.numericBoxCellAngleErr = new Crystallography.Controls.NumericBox();
            this.numericBoxCellLengthErr = new Crystallography.Controls.NumericBox();
            this.numericBoxCellC = new Crystallography.Controls.NumericBox();
            this.numericBoxCellBeta = new Crystallography.Controls.NumericBox();
            this.numericBoxCellAlpha = new Crystallography.Controls.NumericBox();
            this.numericBoxCellB = new Crystallography.Controls.NumericBox();
            this.numericBoxCellA = new Crystallography.Controls.NumericBox();
            this.checkBoxDspacing = new System.Windows.Forms.CheckBox();
            this.groupBoxDspacing = new System.Windows.Forms.GroupBox();
            this.checkBoxD3 = new System.Windows.Forms.CheckBox();
            this.numericBoxD3Err = new Crystallography.Controls.NumericBox();
            this.numericBoxD2Err = new Crystallography.Controls.NumericBox();
            this.numericBoxD1Err = new Crystallography.Controls.NumericBox();
            this.checkBoxD2 = new System.Windows.Forms.CheckBox();
            this.checkBoxD1 = new System.Windows.Forms.CheckBox();
            this.numericBoxD3 = new Crystallography.Controls.NumericBox();
            this.numericBoxD2 = new Crystallography.Controls.NumericBox();
            this.numericBoxD1 = new Crystallography.Controls.NumericBox();
            this.checkBoxDensity = new System.Windows.Forms.CheckBox();
            this.groupBoxDensity = new System.Windows.Forms.GroupBox();
            this.numericBoxDensity = new Crystallography.Controls.NumericBox();
            this.numericBoxDensityErr = new Crystallography.Controls.NumericBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxCellParameter.SuspendLayout();
            this.groupBoxDspacing.SuspendLayout();
            this.groupBoxDensity.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSearchName);
            this.flowLayoutPanel1.Controls.Add(this.textBoxSearchName);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSearchElements);
            this.flowLayoutPanel1.Controls.Add(this.buttonPeriodicTable);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSearchRefference);
            this.flowLayoutPanel1.Controls.Add(this.textBoxSearchRefference);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSearchCrystalSystem);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxSearchCrystalSystem);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSearchCellParameter);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxCellParameter);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxDspacing);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxDspacing);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxDensity);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxDensity);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(188, 563);
            this.flowLayoutPanel1.TabIndex = 78;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // checkBoxSearchName
            // 
            this.checkBoxSearchName.AutoSize = true;
            this.checkBoxSearchName.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxSearchName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSearchName.Location = new System.Drawing.Point(0, 0);
            this.checkBoxSearchName.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxSearchName.Name = "checkBoxSearchName";
            this.checkBoxSearchName.Size = new System.Drawing.Size(62, 21);
            this.checkBoxSearchName.TabIndex = 64;
            this.checkBoxSearchName.Text = "Name";
            this.checkBoxSearchName.UseVisualStyleBackColor = true;
            this.checkBoxSearchName.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // textBoxSearchName
            // 
            this.textBoxSearchName.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.textBoxSearchName.Location = new System.Drawing.Point(10, 21);
            this.textBoxSearchName.Margin = new System.Windows.Forms.Padding(10, 0, 0, 6);
            this.textBoxSearchName.Name = "textBoxSearchName";
            this.textBoxSearchName.Size = new System.Drawing.Size(174, 25);
            this.textBoxSearchName.TabIndex = 0;
            this.textBoxSearchName.Visible = false;
            // 
            // checkBoxSearchElements
            // 
            this.checkBoxSearchElements.AutoSize = true;
            this.checkBoxSearchElements.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxSearchElements.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSearchElements.Location = new System.Drawing.Point(0, 52);
            this.checkBoxSearchElements.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxSearchElements.Name = "checkBoxSearchElements";
            this.checkBoxSearchElements.Size = new System.Drawing.Size(73, 21);
            this.checkBoxSearchElements.TabIndex = 64;
            this.checkBoxSearchElements.Text = "Element";
            this.checkBoxSearchElements.UseVisualStyleBackColor = true;
            this.checkBoxSearchElements.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // buttonPeriodicTable
            // 
            this.buttonPeriodicTable.AutoSize = true;
            this.buttonPeriodicTable.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.buttonPeriodicTable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonPeriodicTable.Location = new System.Drawing.Point(10, 73);
            this.buttonPeriodicTable.Margin = new System.Windows.Forms.Padding(10, 0, 0, 6);
            this.buttonPeriodicTable.Name = "buttonPeriodicTable";
            this.buttonPeriodicTable.Size = new System.Drawing.Size(174, 30);
            this.buttonPeriodicTable.TabIndex = 65;
            this.buttonPeriodicTable.Text = "Periodic Table";
            this.buttonPeriodicTable.UseVisualStyleBackColor = true;
            this.buttonPeriodicTable.Visible = false;
            this.buttonPeriodicTable.Click += new System.EventHandler(this.buttonPeriodicTable_Click);
            // 
            // checkBoxSearchRefference
            // 
            this.checkBoxSearchRefference.AutoSize = true;
            this.checkBoxSearchRefference.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxSearchRefference.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSearchRefference.Location = new System.Drawing.Point(0, 109);
            this.checkBoxSearchRefference.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxSearchRefference.Name = "checkBoxSearchRefference";
            this.checkBoxSearchRefference.Size = new System.Drawing.Size(89, 21);
            this.checkBoxSearchRefference.TabIndex = 64;
            this.checkBoxSearchRefference.Text = "Refference";
            this.checkBoxSearchRefference.UseVisualStyleBackColor = true;
            this.checkBoxSearchRefference.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // textBoxSearchRefference
            // 
            this.textBoxSearchRefference.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.textBoxSearchRefference.Location = new System.Drawing.Point(10, 130);
            this.textBoxSearchRefference.Margin = new System.Windows.Forms.Padding(10, 0, 0, 6);
            this.textBoxSearchRefference.Name = "textBoxSearchRefference";
            this.textBoxSearchRefference.Size = new System.Drawing.Size(173, 25);
            this.textBoxSearchRefference.TabIndex = 0;
            this.textBoxSearchRefference.Visible = false;
            // 
            // checkBoxSearchCrystalSystem
            // 
            this.checkBoxSearchCrystalSystem.AutoSize = true;
            this.checkBoxSearchCrystalSystem.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxSearchCrystalSystem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSearchCrystalSystem.Location = new System.Drawing.Point(0, 161);
            this.checkBoxSearchCrystalSystem.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxSearchCrystalSystem.Name = "checkBoxSearchCrystalSystem";
            this.checkBoxSearchCrystalSystem.Size = new System.Drawing.Size(111, 21);
            this.checkBoxSearchCrystalSystem.TabIndex = 64;
            this.checkBoxSearchCrystalSystem.Text = "Crystal System";
            this.checkBoxSearchCrystalSystem.UseVisualStyleBackColor = true;
            this.checkBoxSearchCrystalSystem.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // comboBoxSearchCrystalSystem
            // 
            this.comboBoxSearchCrystalSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchCrystalSystem.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.comboBoxSearchCrystalSystem.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBoxSearchCrystalSystem.Items.AddRange(new object[] {
            "Unknown",
            "triclinic",
            "monoclinic",
            "orthorhombic",
            "tetragonal",
            "trigonal",
            "hexagonal",
            "cubic"});
            this.comboBoxSearchCrystalSystem.Location = new System.Drawing.Point(10, 182);
            this.comboBoxSearchCrystalSystem.Margin = new System.Windows.Forms.Padding(10, 0, 0, 6);
            this.comboBoxSearchCrystalSystem.Name = "comboBoxSearchCrystalSystem";
            this.comboBoxSearchCrystalSystem.Size = new System.Drawing.Size(174, 25);
            this.comboBoxSearchCrystalSystem.TabIndex = 3;
            this.comboBoxSearchCrystalSystem.Visible = false;
            // 
            // checkBoxSearchCellParameter
            // 
            this.checkBoxSearchCellParameter.AutoSize = true;
            this.checkBoxSearchCellParameter.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxSearchCellParameter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxSearchCellParameter.Location = new System.Drawing.Point(0, 213);
            this.checkBoxSearchCellParameter.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxSearchCellParameter.Name = "checkBoxSearchCellParameter";
            this.checkBoxSearchCellParameter.Size = new System.Drawing.Size(89, 21);
            this.checkBoxSearchCellParameter.TabIndex = 64;
            this.checkBoxSearchCellParameter.Text = "Cell Param";
            this.checkBoxSearchCellParameter.UseVisualStyleBackColor = true;
            this.checkBoxSearchCellParameter.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // groupBoxCellParameter
            // 
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellGamma);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellAngleErr);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellLengthErr);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellC);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellBeta);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellAlpha);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellB);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellA);
            this.groupBoxCellParameter.Font = new System.Drawing.Font("Arial", 9F);
            this.groupBoxCellParameter.Location = new System.Drawing.Point(10, 234);
            this.groupBoxCellParameter.Margin = new System.Windows.Forms.Padding(10, 0, 0, 6);
            this.groupBoxCellParameter.Name = "groupBoxCellParameter";
            this.groupBoxCellParameter.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxCellParameter.Size = new System.Drawing.Size(174, 125);
            this.groupBoxCellParameter.TabIndex = 56;
            this.groupBoxCellParameter.TabStop = false;
            this.groupBoxCellParameter.Visible = false;
            // 
            // numericBoxCellGamma
            // 
            this.numericBoxCellGamma.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCellGamma.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellGamma.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellGamma.FooterText = "°";
            this.numericBoxCellGamma.HeaderText = "γ";
            this.numericBoxCellGamma.Location = new System.Drawing.Point(99, 63);
            this.numericBoxCellGamma.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCellGamma.Maximum = 179D;
            this.numericBoxCellGamma.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCellGamma.Minimum = 1D;
            this.numericBoxCellGamma.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCellGamma.Name = "numericBoxCellGamma";
            this.numericBoxCellGamma.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCellGamma.RadianValue = 1.5707963267948966D;
            this.numericBoxCellGamma.Size = new System.Drawing.Size(70, 25);
            this.numericBoxCellGamma.SkipEventDuringInput = false;
            this.numericBoxCellGamma.SmartIncrement = true;
            this.numericBoxCellGamma.TabIndex = 66;
            this.numericBoxCellGamma.ThonsandsSeparator = true;
            this.numericBoxCellGamma.Value = 90D;
            // 
            // numericBoxCellAngleErr
            // 
            this.numericBoxCellAngleErr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCellAngleErr.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellAngleErr.DecimalPlaces = 1;
            this.numericBoxCellAngleErr.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellAngleErr.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxCellAngleErr.FooterText = "%";
            this.numericBoxCellAngleErr.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxCellAngleErr.HeaderText = "±";
            this.numericBoxCellAngleErr.Location = new System.Drawing.Point(94, 93);
            this.numericBoxCellAngleErr.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCellAngleErr.Maximum = 50D;
            this.numericBoxCellAngleErr.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCellAngleErr.Minimum = 0D;
            this.numericBoxCellAngleErr.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCellAngleErr.Name = "numericBoxCellAngleErr";
            this.numericBoxCellAngleErr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCellAngleErr.RadianValue = 0.052359877559829883D;
            this.numericBoxCellAngleErr.ShowUpDown = true;
            this.numericBoxCellAngleErr.Size = new System.Drawing.Size(78, 25);
            this.numericBoxCellAngleErr.SkipEventDuringInput = false;
            this.numericBoxCellAngleErr.TabIndex = 66;
            this.numericBoxCellAngleErr.ThonsandsSeparator = true;
            this.numericBoxCellAngleErr.UpDown_Increment = 0.5D;
            this.numericBoxCellAngleErr.Value = 3D;
            // 
            // numericBoxCellLengthErr
            // 
            this.numericBoxCellLengthErr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCellLengthErr.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellLengthErr.DecimalPlaces = 1;
            this.numericBoxCellLengthErr.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellLengthErr.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxCellLengthErr.FooterText = "%";
            this.numericBoxCellLengthErr.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxCellLengthErr.HeaderText = "±";
            this.numericBoxCellLengthErr.Location = new System.Drawing.Point(7, 93);
            this.numericBoxCellLengthErr.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCellLengthErr.Maximum = 50D;
            this.numericBoxCellLengthErr.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCellLengthErr.Minimum = 0D;
            this.numericBoxCellLengthErr.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCellLengthErr.Name = "numericBoxCellLengthErr";
            this.numericBoxCellLengthErr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCellLengthErr.RadianValue = 0.052359877559829883D;
            this.numericBoxCellLengthErr.ShowUpDown = true;
            this.numericBoxCellLengthErr.Size = new System.Drawing.Size(78, 25);
            this.numericBoxCellLengthErr.SkipEventDuringInput = false;
            this.numericBoxCellLengthErr.TabIndex = 66;
            this.numericBoxCellLengthErr.ThonsandsSeparator = true;
            this.numericBoxCellLengthErr.UpDown_Increment = 0.5D;
            this.numericBoxCellLengthErr.Value = 3D;
            // 
            // numericBoxCellC
            // 
            this.numericBoxCellC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCellC.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellC.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellC.FooterText = "Å";
            this.numericBoxCellC.HeaderText = "c";
            this.numericBoxCellC.Location = new System.Drawing.Point(4, 64);
            this.numericBoxCellC.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCellC.Maximum = 100D;
            this.numericBoxCellC.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCellC.Minimum = 0D;
            this.numericBoxCellC.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCellC.Name = "numericBoxCellC";
            this.numericBoxCellC.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCellC.Size = new System.Drawing.Size(87, 25);
            this.numericBoxCellC.SkipEventDuringInput = false;
            this.numericBoxCellC.SmartIncrement = true;
            this.numericBoxCellC.TabIndex = 66;
            this.numericBoxCellC.ThonsandsSeparator = true;
            // 
            // numericBoxCellBeta
            // 
            this.numericBoxCellBeta.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCellBeta.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellBeta.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellBeta.FooterText = "°";
            this.numericBoxCellBeta.HeaderText = "β";
            this.numericBoxCellBeta.Location = new System.Drawing.Point(99, 37);
            this.numericBoxCellBeta.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCellBeta.Maximum = 179D;
            this.numericBoxCellBeta.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCellBeta.Minimum = 1D;
            this.numericBoxCellBeta.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCellBeta.Name = "numericBoxCellBeta";
            this.numericBoxCellBeta.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCellBeta.RadianValue = 1.5707963267948966D;
            this.numericBoxCellBeta.Size = new System.Drawing.Size(70, 25);
            this.numericBoxCellBeta.SkipEventDuringInput = false;
            this.numericBoxCellBeta.SmartIncrement = true;
            this.numericBoxCellBeta.TabIndex = 66;
            this.numericBoxCellBeta.ThonsandsSeparator = true;
            this.numericBoxCellBeta.Value = 90D;
            // 
            // numericBoxCellAlpha
            // 
            this.numericBoxCellAlpha.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCellAlpha.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellAlpha.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellAlpha.FooterText = "°";
            this.numericBoxCellAlpha.HeaderText = "α";
            this.numericBoxCellAlpha.Location = new System.Drawing.Point(98, 11);
            this.numericBoxCellAlpha.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCellAlpha.Maximum = 179D;
            this.numericBoxCellAlpha.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCellAlpha.Minimum = 1D;
            this.numericBoxCellAlpha.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCellAlpha.Name = "numericBoxCellAlpha";
            this.numericBoxCellAlpha.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCellAlpha.RadianValue = 1.5707963267948966D;
            this.numericBoxCellAlpha.Size = new System.Drawing.Size(71, 25);
            this.numericBoxCellAlpha.SkipEventDuringInput = false;
            this.numericBoxCellAlpha.SmartIncrement = true;
            this.numericBoxCellAlpha.TabIndex = 66;
            this.numericBoxCellAlpha.ThonsandsSeparator = true;
            this.numericBoxCellAlpha.Value = 90D;
            // 
            // numericBoxCellB
            // 
            this.numericBoxCellB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCellB.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellB.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellB.FooterText = "Å";
            this.numericBoxCellB.HeaderText = "b";
            this.numericBoxCellB.Location = new System.Drawing.Point(2, 38);
            this.numericBoxCellB.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCellB.Maximum = 100D;
            this.numericBoxCellB.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCellB.Minimum = 0D;
            this.numericBoxCellB.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCellB.Name = "numericBoxCellB";
            this.numericBoxCellB.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCellB.Size = new System.Drawing.Size(89, 25);
            this.numericBoxCellB.SkipEventDuringInput = false;
            this.numericBoxCellB.SmartIncrement = true;
            this.numericBoxCellB.TabIndex = 66;
            this.numericBoxCellB.ThonsandsSeparator = true;
            // 
            // numericBoxCellA
            // 
            this.numericBoxCellA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCellA.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellA.FooterText = "Å";
            this.numericBoxCellA.HeaderText = "a";
            this.numericBoxCellA.Location = new System.Drawing.Point(3, 12);
            this.numericBoxCellA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCellA.Maximum = 100D;
            this.numericBoxCellA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCellA.Minimum = 0D;
            this.numericBoxCellA.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxCellA.Name = "numericBoxCellA";
            this.numericBoxCellA.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCellA.Size = new System.Drawing.Size(88, 25);
            this.numericBoxCellA.SkipEventDuringInput = false;
            this.numericBoxCellA.SmartIncrement = true;
            this.numericBoxCellA.TabIndex = 66;
            this.numericBoxCellA.ThonsandsSeparator = true;
            // 
            // checkBoxDspacing
            // 
            this.checkBoxDspacing.AutoSize = true;
            this.checkBoxDspacing.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxDspacing.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxDspacing.Location = new System.Drawing.Point(0, 365);
            this.checkBoxDspacing.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxDspacing.Name = "checkBoxDspacing";
            this.checkBoxDspacing.Size = new System.Drawing.Size(85, 21);
            this.checkBoxDspacing.TabIndex = 64;
            this.checkBoxDspacing.Text = "d-spacing";
            this.checkBoxDspacing.UseVisualStyleBackColor = true;
            this.checkBoxDspacing.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // groupBoxDspacing
            // 
            this.groupBoxDspacing.Controls.Add(this.checkBoxD3);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD3Err);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD2Err);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD1Err);
            this.groupBoxDspacing.Controls.Add(this.checkBoxD2);
            this.groupBoxDspacing.Controls.Add(this.checkBoxD1);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD3);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD2);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD1);
            this.groupBoxDspacing.Font = new System.Drawing.Font("Arial", 9F);
            this.groupBoxDspacing.Location = new System.Drawing.Point(10, 386);
            this.groupBoxDspacing.Margin = new System.Windows.Forms.Padding(10, 0, 0, 4);
            this.groupBoxDspacing.Name = "groupBoxDspacing";
            this.groupBoxDspacing.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxDspacing.Size = new System.Drawing.Size(174, 100);
            this.groupBoxDspacing.TabIndex = 56;
            this.groupBoxDspacing.TabStop = false;
            this.groupBoxDspacing.Visible = false;
            // 
            // checkBoxD3
            // 
            this.checkBoxD3.AutoSize = true;
            this.checkBoxD3.Font = new System.Drawing.Font("Arial", 8.25F);
            this.checkBoxD3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxD3.Location = new System.Drawing.Point(9, 74);
            this.checkBoxD3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxD3.Name = "checkBoxD3";
            this.checkBoxD3.Size = new System.Drawing.Size(15, 14);
            this.checkBoxD3.TabIndex = 64;
            this.checkBoxD3.UseVisualStyleBackColor = true;
            this.checkBoxD3.CheckedChanged += new System.EventHandler(this.checkBoxD3_CheckedChanged);
            // 
            // numericBoxD3Err
            // 
            this.numericBoxD3Err.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxD3Err.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD3Err.DecimalPlaces = 1;
            this.numericBoxD3Err.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxD3Err.FooterText = "%";
            this.numericBoxD3Err.HeaderText = "±";
            this.numericBoxD3Err.Location = new System.Drawing.Point(90, 69);
            this.numericBoxD3Err.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxD3Err.Maximum = 50D;
            this.numericBoxD3Err.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxD3Err.Minimum = 0D;
            this.numericBoxD3Err.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxD3Err.Name = "numericBoxD3Err";
            this.numericBoxD3Err.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxD3Err.RadianValue = 0.052359877559829883D;
            this.numericBoxD3Err.ShowUpDown = true;
            this.numericBoxD3Err.Size = new System.Drawing.Size(81, 25);
            this.numericBoxD3Err.SkipEventDuringInput = false;
            this.numericBoxD3Err.TabIndex = 66;
            this.numericBoxD3Err.ThonsandsSeparator = true;
            this.numericBoxD3Err.UpDown_Increment = 0.5D;
            this.numericBoxD3Err.Value = 3D;
            // 
            // numericBoxD2Err
            // 
            this.numericBoxD2Err.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxD2Err.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD2Err.DecimalPlaces = 1;
            this.numericBoxD2Err.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxD2Err.FooterText = "%";
            this.numericBoxD2Err.HeaderText = "±";
            this.numericBoxD2Err.Location = new System.Drawing.Point(90, 40);
            this.numericBoxD2Err.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxD2Err.Maximum = 50D;
            this.numericBoxD2Err.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxD2Err.Minimum = 0D;
            this.numericBoxD2Err.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxD2Err.Name = "numericBoxD2Err";
            this.numericBoxD2Err.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxD2Err.RadianValue = 0.052359877559829883D;
            this.numericBoxD2Err.ShowUpDown = true;
            this.numericBoxD2Err.Size = new System.Drawing.Size(81, 25);
            this.numericBoxD2Err.SkipEventDuringInput = false;
            this.numericBoxD2Err.TabIndex = 66;
            this.numericBoxD2Err.ThonsandsSeparator = true;
            this.numericBoxD2Err.UpDown_Increment = 0.5D;
            this.numericBoxD2Err.Value = 3D;
            // 
            // numericBoxD1Err
            // 
            this.numericBoxD1Err.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxD1Err.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD1Err.DecimalPlaces = 1;
            this.numericBoxD1Err.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxD1Err.FooterText = "%";
            this.numericBoxD1Err.HeaderText = "±";
            this.numericBoxD1Err.Location = new System.Drawing.Point(90, 10);
            this.numericBoxD1Err.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxD1Err.Maximum = 50D;
            this.numericBoxD1Err.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxD1Err.Minimum = 0D;
            this.numericBoxD1Err.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxD1Err.Name = "numericBoxD1Err";
            this.numericBoxD1Err.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxD1Err.RadianValue = 0.052359877559829883D;
            this.numericBoxD1Err.ShowUpDown = true;
            this.numericBoxD1Err.Size = new System.Drawing.Size(81, 25);
            this.numericBoxD1Err.SkipEventDuringInput = false;
            this.numericBoxD1Err.TabIndex = 66;
            this.numericBoxD1Err.ThonsandsSeparator = true;
            this.numericBoxD1Err.UpDown_Increment = 0.5D;
            this.numericBoxD1Err.Value = 3D;
            // 
            // checkBoxD2
            // 
            this.checkBoxD2.AutoSize = true;
            this.checkBoxD2.Font = new System.Drawing.Font("Arial", 8.25F);
            this.checkBoxD2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxD2.Location = new System.Drawing.Point(9, 45);
            this.checkBoxD2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxD2.Name = "checkBoxD2";
            this.checkBoxD2.Size = new System.Drawing.Size(15, 14);
            this.checkBoxD2.TabIndex = 64;
            this.checkBoxD2.UseVisualStyleBackColor = true;
            this.checkBoxD2.CheckedChanged += new System.EventHandler(this.checkBoxD2_CheckedChanged);
            // 
            // checkBoxD1
            // 
            this.checkBoxD1.AutoSize = true;
            this.checkBoxD1.Checked = true;
            this.checkBoxD1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxD1.Font = new System.Drawing.Font("Arial", 8.25F);
            this.checkBoxD1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxD1.Location = new System.Drawing.Point(9, 16);
            this.checkBoxD1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxD1.Name = "checkBoxD1";
            this.checkBoxD1.Size = new System.Drawing.Size(15, 14);
            this.checkBoxD1.TabIndex = 64;
            this.checkBoxD1.UseVisualStyleBackColor = true;
            this.checkBoxD1.CheckedChanged += new System.EventHandler(this.checkBoxD1_CheckedChanged);
            // 
            // numericBoxD3
            // 
            this.numericBoxD3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxD3.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD3.DecimalPlaces = 2;
            this.numericBoxD3.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxD3.FooterText = "Å";
            this.numericBoxD3.Location = new System.Drawing.Point(29, 69);
            this.numericBoxD3.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxD3.Maximum = 100D;
            this.numericBoxD3.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxD3.Minimum = 0D;
            this.numericBoxD3.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxD3.Name = "numericBoxD3";
            this.numericBoxD3.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxD3.Size = new System.Drawing.Size(62, 25);
            this.numericBoxD3.SkipEventDuringInput = false;
            this.numericBoxD3.SmartIncrement = true;
            this.numericBoxD3.TabIndex = 66;
            this.numericBoxD3.ThonsandsSeparator = true;
            // 
            // numericBoxD2
            // 
            this.numericBoxD2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxD2.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD2.DecimalPlaces = 2;
            this.numericBoxD2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxD2.FooterText = "Å";
            this.numericBoxD2.Location = new System.Drawing.Point(29, 40);
            this.numericBoxD2.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxD2.Maximum = 100D;
            this.numericBoxD2.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxD2.Minimum = 0D;
            this.numericBoxD2.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxD2.Name = "numericBoxD2";
            this.numericBoxD2.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxD2.Size = new System.Drawing.Size(62, 25);
            this.numericBoxD2.SkipEventDuringInput = false;
            this.numericBoxD2.SmartIncrement = true;
            this.numericBoxD2.TabIndex = 66;
            this.numericBoxD2.ThonsandsSeparator = true;
            // 
            // numericBoxD1
            // 
            this.numericBoxD1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxD1.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD1.DecimalPlaces = 2;
            this.numericBoxD1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxD1.FooterText = "Å";
            this.numericBoxD1.Location = new System.Drawing.Point(29, 10);
            this.numericBoxD1.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxD1.Maximum = 100D;
            this.numericBoxD1.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxD1.Minimum = 0D;
            this.numericBoxD1.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxD1.Name = "numericBoxD1";
            this.numericBoxD1.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxD1.Size = new System.Drawing.Size(62, 25);
            this.numericBoxD1.SkipEventDuringInput = false;
            this.numericBoxD1.SmartIncrement = true;
            this.numericBoxD1.TabIndex = 66;
            this.numericBoxD1.ThonsandsSeparator = true;
            // 
            // checkBoxDensity
            // 
            this.checkBoxDensity.AutoSize = true;
            this.checkBoxDensity.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxDensity.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxDensity.Location = new System.Drawing.Point(0, 490);
            this.checkBoxDensity.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxDensity.Name = "checkBoxDensity";
            this.checkBoxDensity.Size = new System.Drawing.Size(69, 21);
            this.checkBoxDensity.TabIndex = 64;
            this.checkBoxDensity.Text = "Density";
            this.checkBoxDensity.UseVisualStyleBackColor = true;
            this.checkBoxDensity.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // groupBoxDensity
            // 
            this.groupBoxDensity.Controls.Add(this.numericBoxDensity);
            this.groupBoxDensity.Controls.Add(this.numericBoxDensityErr);
            this.groupBoxDensity.Font = new System.Drawing.Font("Arial", 9F);
            this.groupBoxDensity.Location = new System.Drawing.Point(10, 511);
            this.groupBoxDensity.Margin = new System.Windows.Forms.Padding(10, 0, 0, 4);
            this.groupBoxDensity.Name = "groupBoxDensity";
            this.groupBoxDensity.Size = new System.Drawing.Size(174, 44);
            this.groupBoxDensity.TabIndex = 67;
            this.groupBoxDensity.TabStop = false;
            this.groupBoxDensity.Visible = false;
            // 
            // numericBoxDensity
            // 
            this.numericBoxDensity.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDensity.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxDensity.DecimalPlaces = 3;
            this.numericBoxDensity.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDensity.FooterText = "g/cc";
            this.numericBoxDensity.Location = new System.Drawing.Point(7, 13);
            this.numericBoxDensity.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.numericBoxDensity.Maximum = 100D;
            this.numericBoxDensity.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDensity.Minimum = 0D;
            this.numericBoxDensity.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDensity.Name = "numericBoxDensity";
            this.numericBoxDensity.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDensity.Size = new System.Drawing.Size(86, 25);
            this.numericBoxDensity.SkipEventDuringInput = false;
            this.numericBoxDensity.SmartIncrement = true;
            this.numericBoxDensity.TabIndex = 66;
            this.numericBoxDensity.ThonsandsSeparator = true;
            // 
            // numericBoxDensityErr
            // 
            this.numericBoxDensityErr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDensityErr.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxDensityErr.DecimalPlaces = 1;
            this.numericBoxDensityErr.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDensityErr.FooterText = "%";
            this.numericBoxDensityErr.HeaderText = "±";
            this.numericBoxDensityErr.Location = new System.Drawing.Point(94, 13);
            this.numericBoxDensityErr.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDensityErr.Maximum = 50D;
            this.numericBoxDensityErr.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDensityErr.Minimum = 0D;
            this.numericBoxDensityErr.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDensityErr.Name = "numericBoxDensityErr";
            this.numericBoxDensityErr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDensityErr.RadianValue = 0.052359877559829883D;
            this.numericBoxDensityErr.ShowUpDown = true;
            this.numericBoxDensityErr.Size = new System.Drawing.Size(81, 25);
            this.numericBoxDensityErr.SkipEventDuringInput = false;
            this.numericBoxDensityErr.TabIndex = 66;
            this.numericBoxDensityErr.ThonsandsSeparator = true;
            this.numericBoxDensityErr.UpDown_Increment = 0.5D;
            this.numericBoxDensityErr.Value = 3D;
            // 
            // SearchCrystalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SearchCrystalControl";
            this.Size = new System.Drawing.Size(188, 563);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBoxCellParameter.ResumeLayout(false);
            this.groupBoxDspacing.ResumeLayout(false);
            this.groupBoxDspacing.PerformLayout();
            this.groupBoxDensity.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBoxSearchName;
        private System.Windows.Forms.TextBox textBoxSearchName;
        private System.Windows.Forms.CheckBox checkBoxSearchElements;
        private System.Windows.Forms.Button buttonPeriodicTable;
        private System.Windows.Forms.CheckBox checkBoxSearchRefference;
        private System.Windows.Forms.TextBox textBoxSearchRefference;
        private System.Windows.Forms.CheckBox checkBoxSearchCrystalSystem;
        public System.Windows.Forms.ComboBox comboBoxSearchCrystalSystem;
        private System.Windows.Forms.CheckBox checkBoxSearchCellParameter;
        private System.Windows.Forms.GroupBox groupBoxCellParameter;
        private NumericBox numericBoxCellGamma;
        private NumericBox numericBoxCellAngleErr;
        private NumericBox numericBoxCellLengthErr;
        private NumericBox numericBoxCellC;
        private NumericBox numericBoxCellBeta;
        private NumericBox numericBoxCellAlpha;
        private NumericBox numericBoxCellB;
        private NumericBox numericBoxCellA;
        private System.Windows.Forms.CheckBox checkBoxDspacing;
        private System.Windows.Forms.GroupBox groupBoxDspacing;
        private System.Windows.Forms.CheckBox checkBoxD3;
        private NumericBox numericBoxD3Err;
        private NumericBox numericBoxD2Err;
        private NumericBox numericBoxD1Err;
        private System.Windows.Forms.CheckBox checkBoxD2;
        private System.Windows.Forms.CheckBox checkBoxD1;
        private NumericBox numericBoxD3;
        private NumericBox numericBoxD2;
        private NumericBox numericBoxD1;
        private System.Windows.Forms.CheckBox checkBoxDensity;
        private System.Windows.Forms.GroupBox groupBoxDensity;
        private NumericBox numericBoxDensity;
        private NumericBox numericBoxDensityErr;
    }
}
