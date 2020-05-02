namespace Crystallography.Controls
{
    partial class SymmetryControl
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxSymmetry = new System.Windows.Forms.GroupBox();
            this.comboBoxSpaceGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxPointGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxCrystalSystem = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.comboBoxSearchResult = new System.Windows.Forms.ComboBox();
            this.numericBoxGammaErr = new Crystallography.Controls.NumericBox();
            this.numericBoxBetaErr = new Crystallography.Controls.NumericBox();
            this.numericBoxAlphaErr = new Crystallography.Controls.NumericBox();
            this.numericBoxAlpha = new Crystallography.Controls.NumericBox();
            this.numericBoxGamma = new Crystallography.Controls.NumericBox();
            this.numericBoxBeta = new Crystallography.Controls.NumericBox();
            this.numericBoxAErr = new Crystallography.Controls.NumericBox();
            this.numericBoxCErr = new Crystallography.Controls.NumericBox();
            this.numericBoxBErr = new Crystallography.Controls.NumericBox();
            this.numericBoxA = new Crystallography.Controls.NumericBox();
            this.numericBoxB = new Crystallography.Controls.NumericBox();
            this.numericBoxC = new Crystallography.Controls.NumericBox();
            this.tableLayoutPanel.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxSymmetry.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.groupBoxSymmetry, 1, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(411, 180);
            this.tableLayoutPanel.TabIndex = 93;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel4);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Arial", 9F);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(199, 174);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Cell constants";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(193, 154);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.numericBoxGammaErr, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericBoxBetaErr, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.numericBoxAlphaErr, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label10, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericBoxAlpha, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericBoxGamma, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericBoxBeta, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label28, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label27, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label26, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label46, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label47, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.label48, 4, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 78);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(191, 75);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(95, 50);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "±";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(95, 25);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "±";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(95, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 17);
            this.label11.TabIndex = 1;
            this.label11.Text = "±";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic);
            this.label28.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label28.Location = new System.Drawing.Point(0, 50);
            this.label28.Margin = new System.Windows.Forms.Padding(0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(14, 17);
            this.label28.TabIndex = 1;
            this.label28.Text = "γ";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic);
            this.label27.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label27.Location = new System.Drawing.Point(0, 25);
            this.label27.Margin = new System.Windows.Forms.Padding(0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(15, 17);
            this.label27.TabIndex = 1;
            this.label27.Text = "β";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic);
            this.label26.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label26.Location = new System.Drawing.Point(0, 0);
            this.label26.Margin = new System.Windows.Forms.Padding(0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(16, 17);
            this.label26.TabIndex = 1;
            this.label26.Text = "α";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label46.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label46.Location = new System.Drawing.Point(177, 0);
            this.label46.Margin = new System.Windows.Forms.Padding(0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(13, 17);
            this.label46.TabIndex = 1;
            this.label46.Text = "°";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label47.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label47.Location = new System.Drawing.Point(177, 25);
            this.label47.Margin = new System.Windows.Forms.Padding(0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(13, 17);
            this.label47.TabIndex = 1;
            this.label47.Text = "°";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label48.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label48.Location = new System.Drawing.Point(177, 50);
            this.label48.Margin = new System.Windows.Forms.Padding(0);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(13, 17);
            this.label48.TabIndex = 1;
            this.label48.Text = "°";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label45, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericBoxAErr, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label44, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label23, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label18, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label24, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericBoxCErr, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericBoxBErr, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label25, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericBoxA, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericBoxB, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericBoxC, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(191, 75);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label45.Location = new System.Drawing.Point(174, 25);
            this.label45.Margin = new System.Windows.Forms.Padding(0);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(16, 17);
            this.label45.TabIndex = 1;
            this.label45.Text = "Å";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label44.Location = new System.Drawing.Point(174, 50);
            this.label44.Margin = new System.Windows.Forms.Padding(0);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(16, 17);
            this.label44.TabIndex = 1;
            this.label44.Text = "Å";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic);
            this.label23.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label23.Location = new System.Drawing.Point(0, 0);
            this.label23.Margin = new System.Windows.Forms.Padding(0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(16, 17);
            this.label23.TabIndex = 0;
            this.label23.Text = "a";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(174, 0);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(16, 17);
            this.label18.TabIndex = 1;
            this.label18.Text = "Å";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic);
            this.label24.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label24.Location = new System.Drawing.Point(0, 25);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(16, 17);
            this.label24.TabIndex = 1;
            this.label24.Text = "b";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic);
            this.label25.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label25.Location = new System.Drawing.Point(0, 50);
            this.label25.Margin = new System.Windows.Forms.Padding(0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(15, 17);
            this.label25.TabIndex = 1;
            this.label25.Text = "c";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(94, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "±";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(94, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "±";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(94, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "±";
            // 
            // groupBoxSymmetry
            // 
            this.groupBoxSymmetry.Controls.Add(this.comboBoxSpaceGroup);
            this.groupBoxSymmetry.Controls.Add(this.comboBoxPointGroup);
            this.groupBoxSymmetry.Controls.Add(this.comboBoxCrystalSystem);
            this.groupBoxSymmetry.Controls.Add(this.label20);
            this.groupBoxSymmetry.Controls.Add(this.label17);
            this.groupBoxSymmetry.Controls.Add(this.label19);
            this.groupBoxSymmetry.Controls.Add(this.textBoxSearch);
            this.groupBoxSymmetry.Controls.Add(this.label21);
            this.groupBoxSymmetry.Controls.Add(this.comboBoxSearchResult);
            this.groupBoxSymmetry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSymmetry.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.groupBoxSymmetry.Location = new System.Drawing.Point(208, 3);
            this.groupBoxSymmetry.Name = "groupBoxSymmetry";
            this.groupBoxSymmetry.Size = new System.Drawing.Size(200, 174);
            this.groupBoxSymmetry.TabIndex = 1;
            this.groupBoxSymmetry.TabStop = false;
            this.groupBoxSymmetry.Text = "Symmetry";
            // 
            // comboBoxSpaceGroup
            // 
            this.comboBoxSpaceGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSpaceGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSpaceGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpaceGroup.DropDownWidth = 200;
            this.comboBoxSpaceGroup.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.comboBoxSpaceGroup.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBoxSpaceGroup.Location = new System.Drawing.Point(95, 74);
            this.comboBoxSpaceGroup.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxSpaceGroup.MaxDropDownItems = 30;
            this.comboBoxSpaceGroup.Name = "comboBoxSpaceGroup";
            this.comboBoxSpaceGroup.Size = new System.Drawing.Size(99, 26);
            this.comboBoxSpaceGroup.TabIndex = 2;
            this.comboBoxSpaceGroup.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxSpaceGroup_DrawItem);
            this.comboBoxSpaceGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpaceGroup_SelectedIndexChanged);
            // 
            // comboBoxPointGroup
            // 
            this.comboBoxPointGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPointGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxPointGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPointGroup.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.comboBoxPointGroup.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBoxPointGroup.Location = new System.Drawing.Point(95, 45);
            this.comboBoxPointGroup.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxPointGroup.Name = "comboBoxPointGroup";
            this.comboBoxPointGroup.Size = new System.Drawing.Size(99, 26);
            this.comboBoxPointGroup.TabIndex = 1;
            this.comboBoxPointGroup.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxSpaceGroup_DrawItem);
            this.comboBoxPointGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxPointGroup_SelectedIndexChanged);
            // 
            // comboBoxCrystalSystem
            // 
            this.comboBoxCrystalSystem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCrystalSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCrystalSystem.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Italic);
            this.comboBoxCrystalSystem.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBoxCrystalSystem.Items.AddRange(new object[] {
            "Unknown",
            "triclinic",
            "monoclinic",
            "orthorhombic",
            "tetragonal",
            "trigonal",
            "hexagonal",
            "cubic"});
            this.comboBoxCrystalSystem.Location = new System.Drawing.Point(95, 17);
            this.comboBoxCrystalSystem.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxCrystalSystem.Name = "comboBoxCrystalSystem";
            this.comboBoxCrystalSystem.Size = new System.Drawing.Size(99, 25);
            this.comboBoxCrystalSystem.TabIndex = 0;
            this.comboBoxCrystalSystem.SelectedIndexChanged += new System.EventHandler(this.comboBoxCrystalSystem_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(6, 79);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 17);
            this.label20.TabIndex = 1;
            this.label20.Text = "Space Group";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(6, 112);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 17);
            this.label17.TabIndex = 1;
            this.label17.Text = "Search";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(3, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(92, 17);
            this.label19.TabIndex = 1;
            this.label19.Text = "Crystal System";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.textBoxSearch.Location = new System.Drawing.Point(54, 110);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(64, 21);
            this.textBoxSearch.TabIndex = 3;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label21.Location = new System.Drawing.Point(10, 50);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(78, 17);
            this.label21.TabIndex = 1;
            this.label21.Text = "Point Group";
            // 
            // comboBoxSearchResult
            // 
            this.comboBoxSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSearchResult.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSearchResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchResult.DropDownWidth = 200;
            this.comboBoxSearchResult.Enabled = false;
            this.comboBoxSearchResult.Font = new System.Drawing.Font("Times New Roman", 11.25F);
            this.comboBoxSearchResult.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBoxSearchResult.Location = new System.Drawing.Point(120, 109);
            this.comboBoxSearchResult.MaxDropDownItems = 40;
            this.comboBoxSearchResult.Name = "comboBoxSearchResult";
            this.comboBoxSearchResult.Size = new System.Drawing.Size(74, 26);
            this.comboBoxSearchResult.TabIndex = 4;
            this.comboBoxSearchResult.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxSpaceGroup_DrawItem);
            this.comboBoxSearchResult.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchResult_SelectedIndexChanged);
            // 
            // numericBoxGammaErr
            // 
            this.numericBoxGammaErr.AllowMouseControl = false;
            this.numericBoxGammaErr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxGammaErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGammaErr.DecimalPlaces = -1;
            this.numericBoxGammaErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxGammaErr.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxGammaErr.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGammaErr.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxGammaErr.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxGammaErr.FooterText = "";
            this.numericBoxGammaErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGammaErr.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxGammaErr.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxGammaErr.HeaderText = "";
            this.numericBoxGammaErr.Location = new System.Drawing.Point(112, 50);
            this.numericBoxGammaErr.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxGammaErr.Maximum = double.PositiveInfinity;
            this.numericBoxGammaErr.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxGammaErr.Minimum = double.NegativeInfinity;
            this.numericBoxGammaErr.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxGammaErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxGammaErr.MouseSpeed = 1D;
            this.numericBoxGammaErr.Multiline = false;
            this.numericBoxGammaErr.Name = "numericBoxGammaErr";
            this.numericBoxGammaErr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxGammaErr.RadianValue = 0D;
            this.numericBoxGammaErr.ReadOnly = false;
            this.numericBoxGammaErr.RestrictLimitValue = false;
            this.numericBoxGammaErr.ShowFraction = false;
            this.numericBoxGammaErr.ShowPositiveSign = false;
            this.numericBoxGammaErr.ShowUpDown = false;
            this.numericBoxGammaErr.Size = new System.Drawing.Size(65, 25);
            this.numericBoxGammaErr.SkipEventDuringInput = false;
            this.numericBoxGammaErr.SmartIncrement = true;
            this.numericBoxGammaErr.TabIndex = 11;
            this.numericBoxGammaErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxGammaErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxGammaErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxGammaErr.ThonsandsSeparator = false;
            this.numericBoxGammaErr.ToolTip = "";
            this.numericBoxGammaErr.UpDown_Increment = 1D;
            this.numericBoxGammaErr.Value = 0D;
            this.numericBoxGammaErr.WordWrap = true;
            this.numericBoxGammaErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxBetaErr
            // 
            this.numericBoxBetaErr.AllowMouseControl = false;
            this.numericBoxBetaErr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxBetaErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBetaErr.DecimalPlaces = -1;
            this.numericBoxBetaErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxBetaErr.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBetaErr.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBetaErr.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBetaErr.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBetaErr.FooterText = "";
            this.numericBoxBetaErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBetaErr.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBetaErr.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBetaErr.HeaderText = "";
            this.numericBoxBetaErr.Location = new System.Drawing.Point(112, 25);
            this.numericBoxBetaErr.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxBetaErr.Maximum = double.PositiveInfinity;
            this.numericBoxBetaErr.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxBetaErr.Minimum = double.NegativeInfinity;
            this.numericBoxBetaErr.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxBetaErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxBetaErr.MouseSpeed = 1D;
            this.numericBoxBetaErr.Multiline = false;
            this.numericBoxBetaErr.Name = "numericBoxBetaErr";
            this.numericBoxBetaErr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxBetaErr.RadianValue = 0D;
            this.numericBoxBetaErr.ReadOnly = false;
            this.numericBoxBetaErr.RestrictLimitValue = false;
            this.numericBoxBetaErr.ShowFraction = false;
            this.numericBoxBetaErr.ShowPositiveSign = false;
            this.numericBoxBetaErr.ShowUpDown = false;
            this.numericBoxBetaErr.Size = new System.Drawing.Size(65, 25);
            this.numericBoxBetaErr.SkipEventDuringInput = false;
            this.numericBoxBetaErr.SmartIncrement = true;
            this.numericBoxBetaErr.TabIndex = 10;
            this.numericBoxBetaErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBetaErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBetaErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBetaErr.ThonsandsSeparator = false;
            this.numericBoxBetaErr.ToolTip = "";
            this.numericBoxBetaErr.UpDown_Increment = 1D;
            this.numericBoxBetaErr.Value = 0D;
            this.numericBoxBetaErr.WordWrap = true;
            this.numericBoxBetaErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxAlphaErr
            // 
            this.numericBoxAlphaErr.AllowMouseControl = false;
            this.numericBoxAlphaErr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxAlphaErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAlphaErr.DecimalPlaces = -1;
            this.numericBoxAlphaErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxAlphaErr.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxAlphaErr.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAlphaErr.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxAlphaErr.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxAlphaErr.FooterText = "";
            this.numericBoxAlphaErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAlphaErr.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxAlphaErr.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxAlphaErr.HeaderText = "";
            this.numericBoxAlphaErr.Location = new System.Drawing.Point(112, 0);
            this.numericBoxAlphaErr.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxAlphaErr.Maximum = double.PositiveInfinity;
            this.numericBoxAlphaErr.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxAlphaErr.Minimum = double.NegativeInfinity;
            this.numericBoxAlphaErr.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxAlphaErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxAlphaErr.MouseSpeed = 1D;
            this.numericBoxAlphaErr.Multiline = false;
            this.numericBoxAlphaErr.Name = "numericBoxAlphaErr";
            this.numericBoxAlphaErr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxAlphaErr.RadianValue = 0D;
            this.numericBoxAlphaErr.ReadOnly = false;
            this.numericBoxAlphaErr.RestrictLimitValue = false;
            this.numericBoxAlphaErr.ShowFraction = false;
            this.numericBoxAlphaErr.ShowPositiveSign = false;
            this.numericBoxAlphaErr.ShowUpDown = false;
            this.numericBoxAlphaErr.Size = new System.Drawing.Size(65, 25);
            this.numericBoxAlphaErr.SkipEventDuringInput = false;
            this.numericBoxAlphaErr.SmartIncrement = true;
            this.numericBoxAlphaErr.TabIndex = 9;
            this.numericBoxAlphaErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxAlphaErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxAlphaErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxAlphaErr.ThonsandsSeparator = false;
            this.numericBoxAlphaErr.ToolTip = "";
            this.numericBoxAlphaErr.UpDown_Increment = 1D;
            this.numericBoxAlphaErr.Value = 0D;
            this.numericBoxAlphaErr.WordWrap = true;
            this.numericBoxAlphaErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxAlpha
            // 
            this.numericBoxAlpha.AllowMouseControl = false;
            this.numericBoxAlpha.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxAlpha.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAlpha.DecimalPlaces = -1;
            this.numericBoxAlpha.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxAlpha.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxAlpha.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAlpha.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxAlpha.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxAlpha.FooterText = "";
            this.numericBoxAlpha.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAlpha.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxAlpha.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxAlpha.HeaderText = "";
            this.numericBoxAlpha.Location = new System.Drawing.Point(16, 0);
            this.numericBoxAlpha.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxAlpha.Maximum = double.PositiveInfinity;
            this.numericBoxAlpha.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxAlpha.Minimum = double.NegativeInfinity;
            this.numericBoxAlpha.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxAlpha.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxAlpha.MouseSpeed = 1D;
            this.numericBoxAlpha.Multiline = false;
            this.numericBoxAlpha.Name = "numericBoxAlpha";
            this.numericBoxAlpha.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxAlpha.RadianValue = 0D;
            this.numericBoxAlpha.ReadOnly = false;
            this.numericBoxAlpha.RestrictLimitValue = false;
            this.numericBoxAlpha.ShowFraction = false;
            this.numericBoxAlpha.ShowPositiveSign = false;
            this.numericBoxAlpha.ShowUpDown = false;
            this.numericBoxAlpha.Size = new System.Drawing.Size(79, 25);
            this.numericBoxAlpha.SkipEventDuringInput = false;
            this.numericBoxAlpha.SmartIncrement = true;
            this.numericBoxAlpha.TabIndex = 3;
            this.numericBoxAlpha.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxAlpha.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxAlpha.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxAlpha.ThonsandsSeparator = false;
            this.numericBoxAlpha.ToolTip = "Alpha in degree";
            this.numericBoxAlpha.UpDown_Increment = 1D;
            this.numericBoxAlpha.Value = 0D;
            this.numericBoxAlpha.WordWrap = true;
            this.numericBoxAlpha.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxGamma
            // 
            this.numericBoxGamma.AllowMouseControl = false;
            this.numericBoxGamma.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxGamma.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGamma.DecimalPlaces = -1;
            this.numericBoxGamma.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxGamma.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxGamma.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGamma.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxGamma.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxGamma.FooterText = "";
            this.numericBoxGamma.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGamma.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxGamma.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxGamma.HeaderText = "";
            this.numericBoxGamma.Location = new System.Drawing.Point(16, 50);
            this.numericBoxGamma.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxGamma.Maximum = double.PositiveInfinity;
            this.numericBoxGamma.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxGamma.Minimum = double.NegativeInfinity;
            this.numericBoxGamma.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxGamma.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxGamma.MouseSpeed = 1D;
            this.numericBoxGamma.Multiline = false;
            this.numericBoxGamma.Name = "numericBoxGamma";
            this.numericBoxGamma.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxGamma.RadianValue = 0D;
            this.numericBoxGamma.ReadOnly = false;
            this.numericBoxGamma.RestrictLimitValue = false;
            this.numericBoxGamma.ShowFraction = false;
            this.numericBoxGamma.ShowPositiveSign = false;
            this.numericBoxGamma.ShowUpDown = false;
            this.numericBoxGamma.Size = new System.Drawing.Size(79, 25);
            this.numericBoxGamma.SkipEventDuringInput = false;
            this.numericBoxGamma.SmartIncrement = true;
            this.numericBoxGamma.TabIndex = 5;
            this.numericBoxGamma.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxGamma.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxGamma.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxGamma.ThonsandsSeparator = false;
            this.numericBoxGamma.ToolTip = "Gamma in degree";
            this.numericBoxGamma.UpDown_Increment = 1D;
            this.numericBoxGamma.Value = 0D;
            this.numericBoxGamma.WordWrap = true;
            this.numericBoxGamma.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxBeta
            // 
            this.numericBoxBeta.AllowMouseControl = false;
            this.numericBoxBeta.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxBeta.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBeta.DecimalPlaces = -1;
            this.numericBoxBeta.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxBeta.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBeta.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBeta.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBeta.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBeta.FooterText = "";
            this.numericBoxBeta.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBeta.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBeta.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBeta.HeaderText = "";
            this.numericBoxBeta.Location = new System.Drawing.Point(16, 25);
            this.numericBoxBeta.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxBeta.Maximum = double.PositiveInfinity;
            this.numericBoxBeta.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxBeta.Minimum = double.NegativeInfinity;
            this.numericBoxBeta.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxBeta.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxBeta.MouseSpeed = 1D;
            this.numericBoxBeta.Multiline = false;
            this.numericBoxBeta.Name = "numericBoxBeta";
            this.numericBoxBeta.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxBeta.RadianValue = 0D;
            this.numericBoxBeta.ReadOnly = false;
            this.numericBoxBeta.RestrictLimitValue = false;
            this.numericBoxBeta.ShowFraction = false;
            this.numericBoxBeta.ShowPositiveSign = false;
            this.numericBoxBeta.ShowUpDown = false;
            this.numericBoxBeta.Size = new System.Drawing.Size(79, 25);
            this.numericBoxBeta.SkipEventDuringInput = false;
            this.numericBoxBeta.SmartIncrement = true;
            this.numericBoxBeta.TabIndex = 4;
            this.numericBoxBeta.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBeta.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBeta.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBeta.ThonsandsSeparator = false;
            this.numericBoxBeta.ToolTip = "Beta in degree";
            this.numericBoxBeta.UpDown_Increment = 1D;
            this.numericBoxBeta.Value = 0D;
            this.numericBoxBeta.WordWrap = true;
            this.numericBoxBeta.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxAErr
            // 
            this.numericBoxAErr.AllowMouseControl = false;
            this.numericBoxAErr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxAErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAErr.DecimalPlaces = -1;
            this.numericBoxAErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxAErr.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxAErr.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAErr.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxAErr.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxAErr.FooterText = "";
            this.numericBoxAErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAErr.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxAErr.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxAErr.HeaderText = "";
            this.numericBoxAErr.Location = new System.Drawing.Point(111, 0);
            this.numericBoxAErr.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxAErr.Maximum = double.PositiveInfinity;
            this.numericBoxAErr.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxAErr.Minimum = double.NegativeInfinity;
            this.numericBoxAErr.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxAErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxAErr.MouseSpeed = 1D;
            this.numericBoxAErr.Multiline = false;
            this.numericBoxAErr.Name = "numericBoxAErr";
            this.numericBoxAErr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxAErr.RadianValue = 0D;
            this.numericBoxAErr.ReadOnly = false;
            this.numericBoxAErr.RestrictLimitValue = false;
            this.numericBoxAErr.ShowFraction = false;
            this.numericBoxAErr.ShowPositiveSign = false;
            this.numericBoxAErr.ShowUpDown = false;
            this.numericBoxAErr.Size = new System.Drawing.Size(63, 25);
            this.numericBoxAErr.SkipEventDuringInput = false;
            this.numericBoxAErr.SmartIncrement = true;
            this.numericBoxAErr.TabIndex = 6;
            this.numericBoxAErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxAErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxAErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxAErr.ThonsandsSeparator = false;
            this.numericBoxAErr.ToolTip = "";
            this.numericBoxAErr.UpDown_Increment = 1D;
            this.numericBoxAErr.Value = 0D;
            this.numericBoxAErr.WordWrap = true;
            this.numericBoxAErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxCErr
            // 
            this.numericBoxCErr.AllowMouseControl = false;
            this.numericBoxCErr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxCErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCErr.DecimalPlaces = -1;
            this.numericBoxCErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxCErr.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCErr.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCErr.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxCErr.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCErr.FooterText = "";
            this.numericBoxCErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCErr.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxCErr.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCErr.HeaderText = "";
            this.numericBoxCErr.Location = new System.Drawing.Point(111, 50);
            this.numericBoxCErr.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxCErr.Maximum = double.PositiveInfinity;
            this.numericBoxCErr.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxCErr.Minimum = double.NegativeInfinity;
            this.numericBoxCErr.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxCErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxCErr.MouseSpeed = 1D;
            this.numericBoxCErr.Multiline = false;
            this.numericBoxCErr.Name = "numericBoxCErr";
            this.numericBoxCErr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxCErr.RadianValue = 0D;
            this.numericBoxCErr.ReadOnly = false;
            this.numericBoxCErr.RestrictLimitValue = false;
            this.numericBoxCErr.ShowFraction = false;
            this.numericBoxCErr.ShowPositiveSign = false;
            this.numericBoxCErr.ShowUpDown = false;
            this.numericBoxCErr.Size = new System.Drawing.Size(63, 25);
            this.numericBoxCErr.SkipEventDuringInput = false;
            this.numericBoxCErr.SmartIncrement = true;
            this.numericBoxCErr.TabIndex = 8;
            this.numericBoxCErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxCErr.ThonsandsSeparator = false;
            this.numericBoxCErr.ToolTip = "";
            this.numericBoxCErr.UpDown_Increment = 1D;
            this.numericBoxCErr.Value = 0D;
            this.numericBoxCErr.WordWrap = true;
            this.numericBoxCErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxBErr
            // 
            this.numericBoxBErr.AllowMouseControl = false;
            this.numericBoxBErr.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxBErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBErr.DecimalPlaces = -1;
            this.numericBoxBErr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxBErr.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBErr.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBErr.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBErr.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBErr.FooterText = "";
            this.numericBoxBErr.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBErr.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBErr.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBErr.HeaderText = "";
            this.numericBoxBErr.Location = new System.Drawing.Point(111, 25);
            this.numericBoxBErr.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxBErr.Maximum = double.PositiveInfinity;
            this.numericBoxBErr.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxBErr.Minimum = double.NegativeInfinity;
            this.numericBoxBErr.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxBErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxBErr.MouseSpeed = 1D;
            this.numericBoxBErr.Multiline = false;
            this.numericBoxBErr.Name = "numericBoxBErr";
            this.numericBoxBErr.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxBErr.RadianValue = 0D;
            this.numericBoxBErr.ReadOnly = false;
            this.numericBoxBErr.RestrictLimitValue = false;
            this.numericBoxBErr.ShowFraction = false;
            this.numericBoxBErr.ShowPositiveSign = false;
            this.numericBoxBErr.ShowUpDown = false;
            this.numericBoxBErr.Size = new System.Drawing.Size(63, 25);
            this.numericBoxBErr.SkipEventDuringInput = false;
            this.numericBoxBErr.SmartIncrement = true;
            this.numericBoxBErr.TabIndex = 7;
            this.numericBoxBErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBErr.ThonsandsSeparator = false;
            this.numericBoxBErr.ToolTip = "";
            this.numericBoxBErr.UpDown_Increment = 1D;
            this.numericBoxBErr.Value = 0D;
            this.numericBoxBErr.WordWrap = true;
            this.numericBoxBErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxA
            // 
            this.numericBoxA.AllowMouseControl = false;
            this.numericBoxA.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxA.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxA.DecimalPlaces = -1;
            this.numericBoxA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxA.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxA.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxA.FooterText = "";
            this.numericBoxA.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxA.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxA.HeaderText = "";
            this.numericBoxA.Location = new System.Drawing.Point(16, 0);
            this.numericBoxA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxA.Maximum = double.PositiveInfinity;
            this.numericBoxA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxA.Minimum = double.NegativeInfinity;
            this.numericBoxA.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxA.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxA.MouseSpeed = 1D;
            this.numericBoxA.Multiline = false;
            this.numericBoxA.Name = "numericBoxA";
            this.numericBoxA.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxA.RadianValue = 0D;
            this.numericBoxA.ReadOnly = false;
            this.numericBoxA.RestrictLimitValue = false;
            this.numericBoxA.ShowFraction = false;
            this.numericBoxA.ShowPositiveSign = false;
            this.numericBoxA.ShowUpDown = false;
            this.numericBoxA.Size = new System.Drawing.Size(78, 25);
            this.numericBoxA.SkipEventDuringInput = false;
            this.numericBoxA.SmartIncrement = true;
            this.numericBoxA.TabIndex = 0;
            this.numericBoxA.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxA.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxA.ThonsandsSeparator = false;
            this.numericBoxA.ToolTip = "A in angstrom";
            this.numericBoxA.UpDown_Increment = 1D;
            this.numericBoxA.Value = 0D;
            this.numericBoxA.WordWrap = true;
            this.numericBoxA.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxB
            // 
            this.numericBoxB.AllowMouseControl = false;
            this.numericBoxB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxB.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxB.DecimalPlaces = -1;
            this.numericBoxB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxB.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxB.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxB.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxB.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxB.FooterText = "";
            this.numericBoxB.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxB.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxB.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxB.HeaderText = "";
            this.numericBoxB.Location = new System.Drawing.Point(16, 25);
            this.numericBoxB.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxB.Maximum = double.PositiveInfinity;
            this.numericBoxB.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxB.Minimum = double.NegativeInfinity;
            this.numericBoxB.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxB.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxB.MouseSpeed = 1D;
            this.numericBoxB.Multiline = false;
            this.numericBoxB.Name = "numericBoxB";
            this.numericBoxB.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxB.RadianValue = 0D;
            this.numericBoxB.ReadOnly = false;
            this.numericBoxB.RestrictLimitValue = false;
            this.numericBoxB.ShowFraction = false;
            this.numericBoxB.ShowPositiveSign = false;
            this.numericBoxB.ShowUpDown = false;
            this.numericBoxB.Size = new System.Drawing.Size(78, 25);
            this.numericBoxB.SkipEventDuringInput = false;
            this.numericBoxB.SmartIncrement = true;
            this.numericBoxB.TabIndex = 1;
            this.numericBoxB.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxB.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxB.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxB.ThonsandsSeparator = false;
            this.numericBoxB.ToolTip = "B in angstrom";
            this.numericBoxB.UpDown_Increment = 1D;
            this.numericBoxB.Value = 0D;
            this.numericBoxB.WordWrap = true;
            this.numericBoxB.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // numericBoxC
            // 
            this.numericBoxC.AllowMouseControl = false;
            this.numericBoxC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxC.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxC.DecimalPlaces = -1;
            this.numericBoxC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericBoxC.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxC.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxC.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxC.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxC.FooterText = "";
            this.numericBoxC.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxC.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxC.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxC.HeaderText = "";
            this.numericBoxC.Location = new System.Drawing.Point(16, 50);
            this.numericBoxC.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxC.Maximum = double.PositiveInfinity;
            this.numericBoxC.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxC.Minimum = double.NegativeInfinity;
            this.numericBoxC.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxC.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxC.MouseSpeed = 1D;
            this.numericBoxC.Multiline = false;
            this.numericBoxC.Name = "numericBoxC";
            this.numericBoxC.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxC.RadianValue = 0D;
            this.numericBoxC.ReadOnly = false;
            this.numericBoxC.RestrictLimitValue = false;
            this.numericBoxC.ShowFraction = false;
            this.numericBoxC.ShowPositiveSign = false;
            this.numericBoxC.ShowUpDown = false;
            this.numericBoxC.Size = new System.Drawing.Size(78, 25);
            this.numericBoxC.SkipEventDuringInput = false;
            this.numericBoxC.SmartIncrement = true;
            this.numericBoxC.TabIndex = 2;
            this.numericBoxC.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxC.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxC.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxC.ThonsandsSeparator = false;
            this.numericBoxC.ToolTip = "C in angstrom";
            this.numericBoxC.UpDown_Increment = 1D;
            this.numericBoxC.Value = 0D;
            this.numericBoxC.WordWrap = true;
            this.numericBoxC.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCellConstants_ValueChanged);
            // 
            // SymmetryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "SymmetryControl";
            this.Size = new System.Drawing.Size(411, 180);
            this.tableLayoutPanel.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxSymmetry.ResumeLayout(false);
            this.groupBoxSymmetry.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private NumericBox numericBoxGammaErr;
        private NumericBox numericBoxBetaErr;
        private NumericBox numericBoxAlphaErr;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private NumericBox numericBoxAlpha;
        private NumericBox numericBoxGamma;
        private NumericBox numericBoxBeta;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label45;
        private NumericBox numericBoxAErr;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label24;
        private NumericBox numericBoxCErr;
        private NumericBox numericBoxBErr;
        private System.Windows.Forms.Label label25;
        private NumericBox numericBoxA;
        private NumericBox numericBoxB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private NumericBox numericBoxC;
        private System.Windows.Forms.GroupBox groupBoxSymmetry;
        public System.Windows.Forms.ComboBox comboBoxSpaceGroup;
        public System.Windows.Forms.ComboBox comboBoxPointGroup;
        public System.Windows.Forms.ComboBox comboBoxCrystalSystem;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label21;
        public System.Windows.Forms.ComboBox comboBoxSearchResult;
    }
}
