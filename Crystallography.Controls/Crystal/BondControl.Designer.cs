namespace Crystallography.Controls
{
    partial class BondInputControl
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
            this.components = new System.ComponentModel.Container();
            this.checkBoxShowPolyhedron = new System.Windows.Forms.CheckBox();
            this.comboBoxBondingAtom1 = new System.Windows.Forms.ComboBox();
            this.comboBoxBondingAtom2 = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.groupBoxPolyhedron = new System.Windows.Forms.GroupBox();
            this.checkBoxShowEdges = new System.Windows.Forms.CheckBox();
            this.groupBoxEdge = new System.Windows.Forms.GroupBox();
            this.label55 = new System.Windows.Forms.Label();
            this.checkBoxShowInnerBonds = new System.Windows.Forms.CheckBox();
            this.checkBoxShowVertexAtoms = new System.Windows.Forms.CheckBox();
            this.checkBoxShowCenterAtom = new System.Windows.Forms.CheckBox();
            this.label42 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonAddBond = new System.Windows.Forms.Button();
            this.buttonChangeBond = new System.Windows.Forms.Button();
            this.buttonDeleteBond = new System.Windows.Forms.Button();
            this.dataSet = new Crystallography.Controls.DataSet();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.centerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vertexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxLenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minLenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.bondColorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.polyhedronColorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.colorControlBond = new Crystallography.Controls.ColorControl();
            this.numericBoxBondAlpha = new Crystallography.Controls.NumericBox();
            this.numericBoxBondRadius = new Crystallography.Controls.NumericBox();
            this.numericBoxBondMaxLength = new Crystallography.Controls.NumericBox();
            this.numericBoxBondMinLength = new Crystallography.Controls.NumericBox();
            this.numericBoxPolyhedronAlpha = new Crystallography.Controls.NumericBox();
            this.colorControlPlyhedron = new Crystallography.Controls.ColorControl();
            this.numericBoxEdgeWidth = new Crystallography.Controls.NumericBox();
            this.colorControlEdges = new Crystallography.Controls.ColorControl();
            this.groupBoxPolyhedron.SuspendLayout();
            this.groupBoxEdge.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxShowPolyhedron
            // 
            this.checkBoxShowPolyhedron.AutoSize = true;
            this.checkBoxShowPolyhedron.Checked = true;
            this.checkBoxShowPolyhedron.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowPolyhedron.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.checkBoxShowPolyhedron.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxShowPolyhedron.Location = new System.Drawing.Point(283, 0);
            this.checkBoxShowPolyhedron.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxShowPolyhedron.Name = "checkBoxShowPolyhedron";
            this.checkBoxShowPolyhedron.Size = new System.Drawing.Size(119, 19);
            this.checkBoxShowPolyhedron.TabIndex = 108;
            this.checkBoxShowPolyhedron.Text = "Show Polyhedron";
            this.checkBoxShowPolyhedron.UseVisualStyleBackColor = true;
            this.checkBoxShowPolyhedron.CheckedChanged += new System.EventHandler(this.checkBoxShowPolyhedron_CheckedChanged);
            // 
            // comboBoxBondingAtom1
            // 
            this.comboBoxBondingAtom1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBondingAtom1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.comboBoxBondingAtom1.ItemHeight = 15;
            this.comboBoxBondingAtom1.Location = new System.Drawing.Point(98, 26);
            this.comboBoxBondingAtom1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxBondingAtom1.MaxDropDownItems = 15;
            this.comboBoxBondingAtom1.Name = "comboBoxBondingAtom1";
            this.comboBoxBondingAtom1.Size = new System.Drawing.Size(69, 23);
            this.comboBoxBondingAtom1.TabIndex = 101;
            // 
            // comboBoxBondingAtom2
            // 
            this.comboBoxBondingAtom2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBondingAtom2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.comboBoxBondingAtom2.ItemHeight = 15;
            this.comboBoxBondingAtom2.Items.AddRange(new object[] {
            ""});
            this.comboBoxBondingAtom2.Location = new System.Drawing.Point(182, 26);
            this.comboBoxBondingAtom2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxBondingAtom2.MaxDropDownItems = 15;
            this.comboBoxBondingAtom2.Name = "comboBoxBondingAtom2";
            this.comboBoxBondingAtom2.Size = new System.Drawing.Size(69, 23);
            this.comboBoxBondingAtom2.TabIndex = 102;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label41.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label41.Location = new System.Drawing.Point(210, 82);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(36, 15);
            this.label41.TabIndex = 115;
            this.label41.Text = "Color";
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label58.Location = new System.Drawing.Point(190, 12);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(46, 15);
            this.label58.TabIndex = 111;
            this.label58.Text = "(vertex)";
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label57.Location = new System.Drawing.Point(106, 12);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(48, 15);
            this.label57.TabIndex = 112;
            this.label57.Text = "(center)";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label39.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label39.Location = new System.Drawing.Point(3, 30);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(90, 15);
            this.label39.TabIndex = 113;
            this.label39.Text = "Bonding Atoms";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label40.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label40.Location = new System.Drawing.Point(169, 30);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(11, 15);
            this.label40.TabIndex = 110;
            this.label40.Text = "-";
            // 
            // groupBoxPolyhedron
            // 
            this.groupBoxPolyhedron.Controls.Add(this.numericBoxPolyhedronAlpha);
            this.groupBoxPolyhedron.Controls.Add(this.colorControlPlyhedron);
            this.groupBoxPolyhedron.Controls.Add(this.checkBoxShowEdges);
            this.groupBoxPolyhedron.Controls.Add(this.groupBoxEdge);
            this.groupBoxPolyhedron.Controls.Add(this.checkBoxShowInnerBonds);
            this.groupBoxPolyhedron.Controls.Add(this.checkBoxShowVertexAtoms);
            this.groupBoxPolyhedron.Controls.Add(this.checkBoxShowCenterAtom);
            this.groupBoxPolyhedron.Controls.Add(this.label42);
            this.groupBoxPolyhedron.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxPolyhedron.Location = new System.Drawing.Point(279, 0);
            this.groupBoxPolyhedron.Margin = new System.Windows.Forms.Padding(0);
            this.groupBoxPolyhedron.Name = "groupBoxPolyhedron";
            this.groupBoxPolyhedron.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxPolyhedron.Size = new System.Drawing.Size(268, 105);
            this.groupBoxPolyhedron.TabIndex = 109;
            this.groupBoxPolyhedron.TabStop = false;
            // 
            // checkBoxShowEdges
            // 
            this.checkBoxShowEdges.AutoSize = true;
            this.checkBoxShowEdges.Checked = true;
            this.checkBoxShowEdges.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowEdges.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.checkBoxShowEdges.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxShowEdges.Location = new System.Drawing.Point(111, 45);
            this.checkBoxShowEdges.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxShowEdges.Name = "checkBoxShowEdges";
            this.checkBoxShowEdges.Size = new System.Drawing.Size(89, 19);
            this.checkBoxShowEdges.TabIndex = 5;
            this.checkBoxShowEdges.Text = "Show Edges";
            this.checkBoxShowEdges.UseVisualStyleBackColor = true;
            this.checkBoxShowEdges.CheckedChanged += new System.EventHandler(this.checkBoxShowEdges_CheckedChanged);
            // 
            // groupBoxEdge
            // 
            this.groupBoxEdge.Controls.Add(this.numericBoxEdgeWidth);
            this.groupBoxEdge.Controls.Add(this.colorControlEdges);
            this.groupBoxEdge.Controls.Add(this.label55);
            this.groupBoxEdge.Location = new System.Drawing.Point(106, 45);
            this.groupBoxEdge.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxEdge.Name = "groupBoxEdge";
            this.groupBoxEdge.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBoxEdge.Size = new System.Drawing.Size(158, 49);
            this.groupBoxEdge.TabIndex = 6;
            this.groupBoxEdge.TabStop = false;
            // 
            // label55
            // 
            this.label55.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label55.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label55.Location = new System.Drawing.Point(5, 25);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(56, 20);
            this.label55.TabIndex = 90;
            this.label55.Text = "Color";
            // 
            // checkBoxShowInnerBonds
            // 
            this.checkBoxShowInnerBonds.AutoSize = true;
            this.checkBoxShowInnerBonds.Checked = true;
            this.checkBoxShowInnerBonds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowInnerBonds.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.checkBoxShowInnerBonds.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxShowInnerBonds.Location = new System.Drawing.Point(5, 25);
            this.checkBoxShowInnerBonds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxShowInnerBonds.Name = "checkBoxShowInnerBonds";
            this.checkBoxShowInnerBonds.Size = new System.Drawing.Size(89, 19);
            this.checkBoxShowInnerBonds.TabIndex = 1;
            this.checkBoxShowInnerBonds.Text = "Inner Bonds";
            this.checkBoxShowInnerBonds.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowVertexAtoms
            // 
            this.checkBoxShowVertexAtoms.AutoSize = true;
            this.checkBoxShowVertexAtoms.Checked = true;
            this.checkBoxShowVertexAtoms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowVertexAtoms.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.checkBoxShowVertexAtoms.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxShowVertexAtoms.Location = new System.Drawing.Point(5, 75);
            this.checkBoxShowVertexAtoms.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxShowVertexAtoms.Name = "checkBoxShowVertexAtoms";
            this.checkBoxShowVertexAtoms.Size = new System.Drawing.Size(96, 19);
            this.checkBoxShowVertexAtoms.TabIndex = 3;
            this.checkBoxShowVertexAtoms.Text = "Vertex Atoms";
            this.checkBoxShowVertexAtoms.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowCenterAtom
            // 
            this.checkBoxShowCenterAtom.AutoSize = true;
            this.checkBoxShowCenterAtom.Checked = true;
            this.checkBoxShowCenterAtom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowCenterAtom.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.checkBoxShowCenterAtom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxShowCenterAtom.Location = new System.Drawing.Point(5, 50);
            this.checkBoxShowCenterAtom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxShowCenterAtom.Name = "checkBoxShowCenterAtom";
            this.checkBoxShowCenterAtom.Size = new System.Drawing.Size(94, 19);
            this.checkBoxShowCenterAtom.TabIndex = 2;
            this.checkBoxShowCenterAtom.Text = "Center Atom";
            this.checkBoxShowCenterAtom.UseVisualStyleBackColor = true;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label42.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label42.Location = new System.Drawing.Point(110, 20);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(36, 15);
            this.label42.TabIndex = 90;
            this.label42.Text = "Color";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxBondingAtom1);
            this.groupBox1.Controls.Add(this.colorControlBond);
            this.groupBox1.Controls.Add(this.numericBoxBondAlpha);
            this.groupBox1.Controls.Add(this.label41);
            this.groupBox1.Controls.Add(this.numericBoxBondRadius);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.numericBoxBondMaxLength);
            this.groupBox1.Controls.Add(this.numericBoxBondMinLength);
            this.groupBox1.Controls.Add(this.comboBoxBondingAtom2);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.label57);
            this.groupBox1.Controls.Add(this.label58);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 105);
            this.groupBox1.TabIndex = 120;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bonds";
            // 
            // buttonAddBond
            // 
            this.buttonAddBond.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAddBond.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.buttonAddBond.ForeColor = System.Drawing.Color.White;
            this.buttonAddBond.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonAddBond.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAddBond.Location = new System.Drawing.Point(0, 2);
            this.buttonAddBond.Name = "buttonAddBond";
            this.buttonAddBond.Size = new System.Drawing.Size(64, 28);
            this.buttonAddBond.TabIndex = 121;
            this.buttonAddBond.Text = "Add";
            this.buttonAddBond.UseVisualStyleBackColor = false;
            this.buttonAddBond.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonChangeBond
            // 
            this.buttonChangeBond.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonChangeBond.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.buttonChangeBond.ForeColor = System.Drawing.Color.White;
            this.buttonChangeBond.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonChangeBond.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonChangeBond.Location = new System.Drawing.Point(64, 2);
            this.buttonChangeBond.Name = "buttonChangeBond";
            this.buttonChangeBond.Size = new System.Drawing.Size(64, 28);
            this.buttonChangeBond.TabIndex = 122;
            this.buttonChangeBond.Text = "Replace";
            this.buttonChangeBond.UseVisualStyleBackColor = false;
            this.buttonChangeBond.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // buttonDeleteBond
            // 
            this.buttonDeleteBond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteBond.AutoSize = true;
            this.buttonDeleteBond.BackColor = System.Drawing.Color.IndianRed;
            this.buttonDeleteBond.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.buttonDeleteBond.ForeColor = System.Drawing.Color.White;
            this.buttonDeleteBond.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonDeleteBond.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonDeleteBond.Location = new System.Drawing.Point(486, 2);
            this.buttonDeleteBond.Name = "buttonDeleteBond";
            this.buttonDeleteBond.Size = new System.Drawing.Size(64, 28);
            this.buttonDeleteBond.TabIndex = 123;
            this.buttonDeleteBond.Text = "Delete";
            this.buttonDeleteBond.UseVisualStyleBackColor = false;
            this.buttonDeleteBond.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingSource
            // 
            this.bindingSource.DataMember = "DataTableBond";
            this.bindingSource.DataSource = this.dataSet;
            this.bindingSource.CurrentChanged += new System.EventHandler(this.bindingSource_PositionChanged);
            this.bindingSource.PositionChanged += new System.EventHandler(this.bindingSource_PositionChanged);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enabledDataGridViewCheckBoxColumn,
            this.centerDataGridViewTextBoxColumn,
            this.vertexDataGridViewTextBoxColumn,
            this.maxLenDataGridViewTextBoxColumn,
            this.minLenDataGridViewTextBoxColumn,
            this.bondColorDataGridViewTextBoxColumn,
            this.polyhedronColorDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.bindingSource;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(550, 210);
            this.dataGridView.TabIndex = 124;
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            this.enabledDataGridViewCheckBoxColumn.HeaderText = "";
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            this.enabledDataGridViewCheckBoxColumn.Width = 30;
            // 
            // centerDataGridViewTextBoxColumn
            // 
            this.centerDataGridViewTextBoxColumn.DataPropertyName = "Center";
            this.centerDataGridViewTextBoxColumn.HeaderText = "Center";
            this.centerDataGridViewTextBoxColumn.Name = "centerDataGridViewTextBoxColumn";
            this.centerDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.centerDataGridViewTextBoxColumn.Width = 80;
            // 
            // vertexDataGridViewTextBoxColumn
            // 
            this.vertexDataGridViewTextBoxColumn.DataPropertyName = "Vertex";
            this.vertexDataGridViewTextBoxColumn.HeaderText = "Vertex";
            this.vertexDataGridViewTextBoxColumn.Name = "vertexDataGridViewTextBoxColumn";
            this.vertexDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.vertexDataGridViewTextBoxColumn.Width = 80;
            // 
            // maxLenDataGridViewTextBoxColumn
            // 
            this.maxLenDataGridViewTextBoxColumn.DataPropertyName = "Max len.";
            this.maxLenDataGridViewTextBoxColumn.HeaderText = "Max len.";
            this.maxLenDataGridViewTextBoxColumn.Name = "maxLenDataGridViewTextBoxColumn";
            this.maxLenDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.maxLenDataGridViewTextBoxColumn.Width = 80;
            // 
            // minLenDataGridViewTextBoxColumn
            // 
            this.minLenDataGridViewTextBoxColumn.DataPropertyName = "Min len.";
            this.minLenDataGridViewTextBoxColumn.HeaderText = "Min len.";
            this.minLenDataGridViewTextBoxColumn.Name = "minLenDataGridViewTextBoxColumn";
            this.minLenDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.minLenDataGridViewTextBoxColumn.Width = 80;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonAddBond);
            this.panel1.Controls.Add(this.buttonChangeBond);
            this.panel1.Controls.Add(this.buttonDeleteBond);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 210);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 32);
            this.panel1.TabIndex = 125;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.checkBoxShowPolyhedron);
            this.panel2.Controls.Add(this.groupBoxPolyhedron);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 242);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 108);
            this.panel2.TabIndex = 126;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Bond color";
            this.dataGridViewTextBoxColumn1.HeaderText = "Bond color";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 80;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Polyhedron color";
            this.dataGridViewTextBoxColumn2.HeaderText = "Polyhedron color";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.DataPropertyName = "Bond color";
            this.dataGridViewImageColumn1.HeaderText = "Bond color";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.Width = 80;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.DataPropertyName = "Polyhedron color";
            this.dataGridViewImageColumn2.HeaderText = "Polyhedron color";
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn2.Width = 80;
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.DataPropertyName = "Bond color";
            this.dataGridViewImageColumn3.HeaderText = "Bond color";
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn3.Width = 80;
            // 
            // dataGridViewImageColumn4
            // 
            this.dataGridViewImageColumn4.DataPropertyName = "Polyhedron color";
            this.dataGridViewImageColumn4.HeaderText = "Polyhedron color";
            this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            this.dataGridViewImageColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn4.Width = 80;
            // 
            // bondColorDataGridViewTextBoxColumn
            // 
            this.bondColorDataGridViewTextBoxColumn.DataPropertyName = "Bond color";
            this.bondColorDataGridViewTextBoxColumn.HeaderText = "Bond color";
            this.bondColorDataGridViewTextBoxColumn.Name = "bondColorDataGridViewTextBoxColumn";
            this.bondColorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.bondColorDataGridViewTextBoxColumn.Width = 80;
            // 
            // polyhedronColorDataGridViewTextBoxColumn
            // 
            this.polyhedronColorDataGridViewTextBoxColumn.DataPropertyName = "Polyhedron color";
            this.polyhedronColorDataGridViewTextBoxColumn.HeaderText = "Polyhedron color";
            this.polyhedronColorDataGridViewTextBoxColumn.Name = "polyhedronColorDataGridViewTextBoxColumn";
            this.polyhedronColorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.polyhedronColorDataGridViewTextBoxColumn.Width = 80;
            // 
            // colorControlBond
            // 
            this.colorControlBond.Argb = -986896;
            this.colorControlBond.AutoSize = true;
            this.colorControlBond.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.colorControlBond.Blue = 240;
            this.colorControlBond.BlueF = 0.9411765F;
            this.colorControlBond.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControlBond.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.colorControlBond.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorControlBond.FooterText = "";
            this.colorControlBond.Green = 240;
            this.colorControlBond.GreenF = 0.9411765F;
            this.colorControlBond.Location = new System.Drawing.Point(246, 81);
            this.colorControlBond.Margin = new System.Windows.Forms.Padding(0);
            this.colorControlBond.Name = "colorControlBond";
            this.colorControlBond.Red = 240;
            this.colorControlBond.RedF = 0.9411765F;
            this.colorControlBond.Size = new System.Drawing.Size(18, 18);
            this.colorControlBond.TabIndex = 103;
            this.colorControlBond.ToolTip = "";
            // 
            // numericBoxBondAlpha
            // 
            this.numericBoxBondAlpha.AllowMouseControl = false;
            this.numericBoxBondAlpha.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxBondAlpha.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondAlpha.DecimalPlaces = 1;
            this.numericBoxBondAlpha.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBondAlpha.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondAlpha.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondAlpha.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondAlpha.FooterText = "";
            this.numericBoxBondAlpha.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondAlpha.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondAlpha.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondAlpha.HeaderText = "Alpha";
            this.numericBoxBondAlpha.Location = new System.Drawing.Point(118, 80);
            this.numericBoxBondAlpha.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxBondAlpha.Maximum = 1D;
            this.numericBoxBondAlpha.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxBondAlpha.Minimum = 0D;
            this.numericBoxBondAlpha.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxBondAlpha.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxBondAlpha.MouseSpeed = 1D;
            this.numericBoxBondAlpha.Multiline = false;
            this.numericBoxBondAlpha.Name = "numericBoxBondAlpha";
            this.numericBoxBondAlpha.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxBondAlpha.RadianValue = 0.012217304763960307D;
            this.numericBoxBondAlpha.ReadOnly = false;
            this.numericBoxBondAlpha.RestrictLimitValue = true;
            this.numericBoxBondAlpha.ShowFraction = false;
            this.numericBoxBondAlpha.ShowPositiveSign = false;
            this.numericBoxBondAlpha.ShowUpDown = true;
            this.numericBoxBondAlpha.Size = new System.Drawing.Size(87, 25);
            this.numericBoxBondAlpha.SkipEventDuringInput = false;
            this.numericBoxBondAlpha.SmartIncrement = true;
            this.numericBoxBondAlpha.TabIndex = 119;
            this.numericBoxBondAlpha.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBondAlpha.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBondAlpha.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondAlpha.ThonsandsSeparator = true;
            this.numericBoxBondAlpha.ToolTip = "";
            this.numericBoxBondAlpha.UpDown_Increment = 0.1D;
            this.numericBoxBondAlpha.Value = 0.7D;
            this.numericBoxBondAlpha.WordWrap = true;
            // 
            // numericBoxBondRadius
            // 
            this.numericBoxBondRadius.AllowMouseControl = false;
            this.numericBoxBondRadius.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxBondRadius.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondRadius.DecimalPlaces = 3;
            this.numericBoxBondRadius.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBondRadius.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondRadius.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondRadius.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondRadius.FooterText = "Å";
            this.numericBoxBondRadius.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondRadius.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondRadius.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondRadius.HeaderText = "Radius";
            this.numericBoxBondRadius.Location = new System.Drawing.Point(3, 80);
            this.numericBoxBondRadius.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxBondRadius.Maximum = 9.9D;
            this.numericBoxBondRadius.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxBondRadius.Minimum = 0.1D;
            this.numericBoxBondRadius.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxBondRadius.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxBondRadius.MouseSpeed = 1D;
            this.numericBoxBondRadius.Multiline = false;
            this.numericBoxBondRadius.Name = "numericBoxBondRadius";
            this.numericBoxBondRadius.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxBondRadius.RadianValue = 0.0017453292519943296D;
            this.numericBoxBondRadius.ReadOnly = false;
            this.numericBoxBondRadius.RestrictLimitValue = true;
            this.numericBoxBondRadius.ShowFraction = false;
            this.numericBoxBondRadius.ShowPositiveSign = false;
            this.numericBoxBondRadius.ShowUpDown = true;
            this.numericBoxBondRadius.Size = new System.Drawing.Size(113, 25);
            this.numericBoxBondRadius.SkipEventDuringInput = false;
            this.numericBoxBondRadius.SmartIncrement = true;
            this.numericBoxBondRadius.TabIndex = 119;
            this.numericBoxBondRadius.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBondRadius.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBondRadius.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondRadius.ThonsandsSeparator = true;
            this.numericBoxBondRadius.ToolTip = "";
            this.numericBoxBondRadius.UpDown_Increment = 0.02D;
            this.numericBoxBondRadius.Value = 0.1D;
            this.numericBoxBondRadius.WordWrap = true;
            // 
            // numericBoxBondMaxLength
            // 
            this.numericBoxBondMaxLength.AllowMouseControl = false;
            this.numericBoxBondMaxLength.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxBondMaxLength.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMaxLength.DecimalPlaces = 3;
            this.numericBoxBondMaxLength.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBondMaxLength.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMaxLength.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondMaxLength.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondMaxLength.FooterText = "Å";
            this.numericBoxBondMaxLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMaxLength.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondMaxLength.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondMaxLength.HeaderText = "-";
            this.numericBoxBondMaxLength.Location = new System.Drawing.Point(163, 53);
            this.numericBoxBondMaxLength.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxBondMaxLength.Maximum = 9.9D;
            this.numericBoxBondMaxLength.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxBondMaxLength.Minimum = 0.1D;
            this.numericBoxBondMaxLength.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxBondMaxLength.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxBondMaxLength.MouseSpeed = 1D;
            this.numericBoxBondMaxLength.Multiline = false;
            this.numericBoxBondMaxLength.Name = "numericBoxBondMaxLength";
            this.numericBoxBondMaxLength.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxBondMaxLength.RadianValue = 0.027925268031909273D;
            this.numericBoxBondMaxLength.ReadOnly = false;
            this.numericBoxBondMaxLength.RestrictLimitValue = true;
            this.numericBoxBondMaxLength.ShowFraction = false;
            this.numericBoxBondMaxLength.ShowPositiveSign = false;
            this.numericBoxBondMaxLength.ShowUpDown = true;
            this.numericBoxBondMaxLength.Size = new System.Drawing.Size(88, 25);
            this.numericBoxBondMaxLength.SkipEventDuringInput = false;
            this.numericBoxBondMaxLength.SmartIncrement = true;
            this.numericBoxBondMaxLength.TabIndex = 119;
            this.numericBoxBondMaxLength.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBondMaxLength.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBondMaxLength.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondMaxLength.ThonsandsSeparator = true;
            this.numericBoxBondMaxLength.ToolTip = "";
            this.numericBoxBondMaxLength.UpDown_Increment = 0.1D;
            this.numericBoxBondMaxLength.Value = 1.6D;
            this.numericBoxBondMaxLength.WordWrap = true;
            // 
            // numericBoxBondMinLength
            // 
            this.numericBoxBondMinLength.AllowMouseControl = false;
            this.numericBoxBondMinLength.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxBondMinLength.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMinLength.DecimalPlaces = 3;
            this.numericBoxBondMinLength.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBondMinLength.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMinLength.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondMinLength.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondMinLength.FooterText = "";
            this.numericBoxBondMinLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMinLength.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondMinLength.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondMinLength.HeaderText = "Length between";
            this.numericBoxBondMinLength.Location = new System.Drawing.Point(3, 53);
            this.numericBoxBondMinLength.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxBondMinLength.Maximum = 9.9D;
            this.numericBoxBondMinLength.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxBondMinLength.Minimum = 0D;
            this.numericBoxBondMinLength.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxBondMinLength.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxBondMinLength.MouseSpeed = 1D;
            this.numericBoxBondMinLength.Multiline = false;
            this.numericBoxBondMinLength.Name = "numericBoxBondMinLength";
            this.numericBoxBondMinLength.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxBondMinLength.RadianValue = 0.0017453292519943296D;
            this.numericBoxBondMinLength.ReadOnly = false;
            this.numericBoxBondMinLength.RestrictLimitValue = true;
            this.numericBoxBondMinLength.ShowFraction = false;
            this.numericBoxBondMinLength.ShowPositiveSign = false;
            this.numericBoxBondMinLength.ShowUpDown = true;
            this.numericBoxBondMinLength.Size = new System.Drawing.Size(154, 25);
            this.numericBoxBondMinLength.SkipEventDuringInput = false;
            this.numericBoxBondMinLength.SmartIncrement = true;
            this.numericBoxBondMinLength.TabIndex = 119;
            this.numericBoxBondMinLength.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBondMinLength.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBondMinLength.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondMinLength.ThonsandsSeparator = true;
            this.numericBoxBondMinLength.ToolTip = "";
            this.numericBoxBondMinLength.UpDown_Increment = 0.1D;
            this.numericBoxBondMinLength.Value = 0.1D;
            this.numericBoxBondMinLength.WordWrap = true;
            // 
            // numericBoxPolyhedronAlpha
            // 
            this.numericBoxPolyhedronAlpha.AllowMouseControl = false;
            this.numericBoxPolyhedronAlpha.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxPolyhedronAlpha.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPolyhedronAlpha.DecimalPlaces = 1;
            this.numericBoxPolyhedronAlpha.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPolyhedronAlpha.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPolyhedronAlpha.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxPolyhedronAlpha.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxPolyhedronAlpha.FooterText = "";
            this.numericBoxPolyhedronAlpha.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPolyhedronAlpha.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxPolyhedronAlpha.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxPolyhedronAlpha.HeaderText = "Alpha";
            this.numericBoxPolyhedronAlpha.Location = new System.Drawing.Point(174, 17);
            this.numericBoxPolyhedronAlpha.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPolyhedronAlpha.Maximum = 1D;
            this.numericBoxPolyhedronAlpha.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPolyhedronAlpha.Minimum = 0D;
            this.numericBoxPolyhedronAlpha.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxPolyhedronAlpha.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxPolyhedronAlpha.MouseSpeed = 1D;
            this.numericBoxPolyhedronAlpha.Multiline = false;
            this.numericBoxPolyhedronAlpha.Name = "numericBoxPolyhedronAlpha";
            this.numericBoxPolyhedronAlpha.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPolyhedronAlpha.RadianValue = 0.012217304763960307D;
            this.numericBoxPolyhedronAlpha.ReadOnly = false;
            this.numericBoxPolyhedronAlpha.RestrictLimitValue = true;
            this.numericBoxPolyhedronAlpha.ShowFraction = false;
            this.numericBoxPolyhedronAlpha.ShowPositiveSign = false;
            this.numericBoxPolyhedronAlpha.ShowUpDown = true;
            this.numericBoxPolyhedronAlpha.Size = new System.Drawing.Size(87, 25);
            this.numericBoxPolyhedronAlpha.SkipEventDuringInput = false;
            this.numericBoxPolyhedronAlpha.SmartIncrement = true;
            this.numericBoxPolyhedronAlpha.TabIndex = 119;
            this.numericBoxPolyhedronAlpha.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxPolyhedronAlpha.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxPolyhedronAlpha.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxPolyhedronAlpha.ThonsandsSeparator = true;
            this.numericBoxPolyhedronAlpha.ToolTip = "";
            this.numericBoxPolyhedronAlpha.UpDown_Increment = 0.1D;
            this.numericBoxPolyhedronAlpha.Value = 0.7D;
            this.numericBoxPolyhedronAlpha.WordWrap = true;
            // 
            // colorControlPlyhedron
            // 
            this.colorControlPlyhedron.Argb = -986896;
            this.colorControlPlyhedron.AutoSize = true;
            this.colorControlPlyhedron.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.colorControlPlyhedron.Blue = 240;
            this.colorControlPlyhedron.BlueF = 0.9411765F;
            this.colorControlPlyhedron.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControlPlyhedron.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.colorControlPlyhedron.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorControlPlyhedron.FooterText = "";
            this.colorControlPlyhedron.Green = 240;
            this.colorControlPlyhedron.GreenF = 0.9411765F;
            this.colorControlPlyhedron.Location = new System.Drawing.Point(147, 18);
            this.colorControlPlyhedron.Margin = new System.Windows.Forms.Padding(0);
            this.colorControlPlyhedron.Name = "colorControlPlyhedron";
            this.colorControlPlyhedron.Red = 240;
            this.colorControlPlyhedron.RedF = 0.9411765F;
            this.colorControlPlyhedron.Size = new System.Drawing.Size(18, 18);
            this.colorControlPlyhedron.TabIndex = 102;
            this.colorControlPlyhedron.ToolTip = "";
            // 
            // numericBoxEdgeWidth
            // 
            this.numericBoxEdgeWidth.AllowMouseControl = false;
            this.numericBoxEdgeWidth.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxEdgeWidth.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEdgeWidth.DecimalPlaces = 1;
            this.numericBoxEdgeWidth.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxEdgeWidth.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEdgeWidth.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEdgeWidth.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEdgeWidth.FooterText = "";
            this.numericBoxEdgeWidth.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEdgeWidth.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEdgeWidth.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEdgeWidth.HeaderText = "Width";
            this.numericBoxEdgeWidth.Location = new System.Drawing.Point(68, 22);
            this.numericBoxEdgeWidth.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxEdgeWidth.Maximum = 1D;
            this.numericBoxEdgeWidth.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxEdgeWidth.Minimum = 0D;
            this.numericBoxEdgeWidth.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxEdgeWidth.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxEdgeWidth.MouseSpeed = 1D;
            this.numericBoxEdgeWidth.Multiline = false;
            this.numericBoxEdgeWidth.Name = "numericBoxEdgeWidth";
            this.numericBoxEdgeWidth.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxEdgeWidth.RadianValue = 0.012217304763960307D;
            this.numericBoxEdgeWidth.ReadOnly = false;
            this.numericBoxEdgeWidth.RestrictLimitValue = true;
            this.numericBoxEdgeWidth.ShowFraction = false;
            this.numericBoxEdgeWidth.ShowPositiveSign = false;
            this.numericBoxEdgeWidth.ShowUpDown = true;
            this.numericBoxEdgeWidth.Size = new System.Drawing.Size(87, 25);
            this.numericBoxEdgeWidth.SkipEventDuringInput = false;
            this.numericBoxEdgeWidth.SmartIncrement = true;
            this.numericBoxEdgeWidth.TabIndex = 119;
            this.numericBoxEdgeWidth.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEdgeWidth.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEdgeWidth.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEdgeWidth.ThonsandsSeparator = true;
            this.numericBoxEdgeWidth.ToolTip = "";
            this.numericBoxEdgeWidth.UpDown_Increment = 0.1D;
            this.numericBoxEdgeWidth.Value = 0.7D;
            this.numericBoxEdgeWidth.WordWrap = true;
            // 
            // colorControlEdges
            // 
            this.colorControlEdges.Argb = -986896;
            this.colorControlEdges.AutoSize = true;
            this.colorControlEdges.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.colorControlEdges.Blue = 240;
            this.colorControlEdges.BlueF = 0.9411765F;
            this.colorControlEdges.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControlEdges.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.colorControlEdges.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorControlEdges.FooterText = "";
            this.colorControlEdges.Green = 240;
            this.colorControlEdges.GreenF = 0.9411765F;
            this.colorControlEdges.Location = new System.Drawing.Point(41, 25);
            this.colorControlEdges.Margin = new System.Windows.Forms.Padding(0);
            this.colorControlEdges.Name = "colorControlEdges";
            this.colorControlEdges.Red = 240;
            this.colorControlEdges.RedF = 0.9411765F;
            this.colorControlEdges.Size = new System.Drawing.Size(18, 18);
            this.colorControlEdges.TabIndex = 102;
            this.colorControlEdges.ToolTip = "";
            // 
            // BondInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "BondInputControl";
            this.Size = new System.Drawing.Size(550, 350);
            this.groupBoxPolyhedron.ResumeLayout(false);
            this.groupBoxPolyhedron.PerformLayout();
            this.groupBoxEdge.ResumeLayout(false);
            this.groupBoxEdge.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ColorControl colorControlBond;
        private System.Windows.Forms.CheckBox checkBoxShowPolyhedron;
        private System.Windows.Forms.ComboBox comboBoxBondingAtom1;
        private System.Windows.Forms.ComboBox comboBoxBondingAtom2;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.GroupBox groupBoxPolyhedron;
        private ColorControl colorControlPlyhedron;
        private System.Windows.Forms.CheckBox checkBoxShowEdges;
        private System.Windows.Forms.GroupBox groupBoxEdge;
        private ColorControl colorControlEdges;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.CheckBox checkBoxShowInnerBonds;
        private System.Windows.Forms.CheckBox checkBoxShowVertexAtoms;
        private System.Windows.Forms.CheckBox checkBoxShowCenterAtom;
        private System.Windows.Forms.Label label42;
        private NumericBox numericBoxPolyhedronAlpha;
        private NumericBox numericBoxEdgeWidth;
        private NumericBox numericBoxBondMinLength;
        private NumericBox numericBoxBondMaxLength;
        private NumericBox numericBoxBondRadius;
        private NumericBox numericBoxBondAlpha;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAddBond;
        private System.Windows.Forms.Button buttonChangeBond;
        private System.Windows.Forms.Button buttonDeleteBond;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn centerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vertexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxLenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minLenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn bondColorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn polyhedronColorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn4;
    }
}
