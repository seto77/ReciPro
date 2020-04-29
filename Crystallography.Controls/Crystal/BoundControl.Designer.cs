namespace Crystallography.Controls
{
    partial class BoundControl
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
            this.bondColorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.centerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vertexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maxLenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.minLenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.polyhedronColorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new Crystallography.Controls.DataSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAddBond = new System.Windows.Forms.Button();
            this.buttonChangeBond = new System.Windows.Forms.Button();
            this.buttonDeleteBond = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericBoxH = new Crystallography.Controls.NumericBox();
            this.numericBoxK = new Crystallography.Controls.NumericBox();
            this.numericBoxL = new Crystallography.Controls.NumericBox();
            this.checkBoxEquivalency = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericBoxDistance = new Crystallography.Controls.NumericBox();
            this.numericBoxDistanceD = new Crystallography.Controls.NumericBox();
            this.label11 = new System.Windows.Forms.Label();
            this.colorControl = new Crystallography.Controls.ColorControl();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bondColorDataGridViewTextBoxColumn
            // 
            this.bondColorDataGridViewTextBoxColumn.DataPropertyName = "Bond color";
            this.bondColorDataGridViewTextBoxColumn.HeaderText = "Bond color";
            this.bondColorDataGridViewTextBoxColumn.Name = "bondColorDataGridViewTextBoxColumn";
            this.bondColorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.bondColorDataGridViewTextBoxColumn.Width = 80;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.colorControl);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.numericBoxDistanceD);
            this.panel2.Controls.Add(this.numericBoxDistance);
            this.panel2.Controls.Add(this.numericBoxL);
            this.panel2.Controls.Add(this.numericBoxK);
            this.panel2.Controls.Add(this.checkBoxEquivalency);
            this.panel2.Controls.Add(this.numericBoxH);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 318);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(550, 47);
            this.panel2.TabIndex = 129;
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
            this.dataGridView.Size = new System.Drawing.Size(550, 286);
            this.dataGridView.TabIndex = 127;
            // 
            // polyhedronColorDataGridViewTextBoxColumn
            // 
            this.polyhedronColorDataGridViewTextBoxColumn.DataPropertyName = "Polyhedron color";
            this.polyhedronColorDataGridViewTextBoxColumn.HeaderText = "Polyhedron color";
            this.polyhedronColorDataGridViewTextBoxColumn.Name = "polyhedronColorDataGridViewTextBoxColumn";
            this.polyhedronColorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.polyhedronColorDataGridViewTextBoxColumn.Width = 80;
            // 
            // bindingSource
            // 
            this.bindingSource.DataMember = "DataTableBond";
            this.bindingSource.DataSource = this.dataSet;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonAddBond);
            this.panel1.Controls.Add(this.buttonChangeBond);
            this.panel1.Controls.Add(this.buttonDeleteBond);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 286);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 32);
            this.panel1.TabIndex = 128;
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
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 12F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(7, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 21);
            this.label1.TabIndex = 90;
            this.label1.Text = "{";
            // 
            // numericBoxH
            // 
            this.numericBoxH.AllowMouseControl = false;
            this.numericBoxH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxH.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxH.DecimalPlaces = 0;
            this.numericBoxH.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxH.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxH.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxH.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxH.FooterText = "";
            this.numericBoxH.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxH.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxH.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxH.HeaderText = "";
            this.numericBoxH.Location = new System.Drawing.Point(21, 18);
            this.numericBoxH.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxH.Maximum = 10D;
            this.numericBoxH.MaximumSize = new System.Drawing.Size(1000, 27);
            this.numericBoxH.Minimum = -10D;
            this.numericBoxH.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxH.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxH.MouseSpeed = 1D;
            this.numericBoxH.Multiline = false;
            this.numericBoxH.Name = "numericBoxH";
            this.numericBoxH.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxH.RadianValue = 0D;
            this.numericBoxH.ReadOnly = false;
            this.numericBoxH.RestrictLimitValue = true;
            this.numericBoxH.ShowFraction = false;
            this.numericBoxH.ShowPositiveSign = false;
            this.numericBoxH.ShowUpDown = true;
            this.numericBoxH.Size = new System.Drawing.Size(40, 27);
            this.numericBoxH.SkipEventDuringInput = false;
            this.numericBoxH.SmartIncrement = false;
            this.numericBoxH.TabIndex = 119;
            this.numericBoxH.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxH.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxH.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxH.ThonsandsSeparator = true;
            this.numericBoxH.ToolTip = "";
            this.numericBoxH.UpDown_Increment = 1D;
            this.numericBoxH.Value = 0D;
            this.numericBoxH.WordWrap = true;
            // 
            // numericBoxK
            // 
            this.numericBoxK.AllowMouseControl = false;
            this.numericBoxK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxK.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxK.DecimalPlaces = 0;
            this.numericBoxK.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxK.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxK.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxK.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxK.FooterText = "";
            this.numericBoxK.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxK.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxK.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxK.HeaderText = "";
            this.numericBoxK.Location = new System.Drawing.Point(61, 18);
            this.numericBoxK.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxK.Maximum = 10D;
            this.numericBoxK.MaximumSize = new System.Drawing.Size(1000, 27);
            this.numericBoxK.Minimum = -10D;
            this.numericBoxK.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxK.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxK.MouseSpeed = 1D;
            this.numericBoxK.Multiline = false;
            this.numericBoxK.Name = "numericBoxK";
            this.numericBoxK.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxK.RadianValue = 0D;
            this.numericBoxK.ReadOnly = false;
            this.numericBoxK.RestrictLimitValue = true;
            this.numericBoxK.ShowFraction = false;
            this.numericBoxK.ShowPositiveSign = false;
            this.numericBoxK.ShowUpDown = true;
            this.numericBoxK.Size = new System.Drawing.Size(40, 27);
            this.numericBoxK.SkipEventDuringInput = false;
            this.numericBoxK.SmartIncrement = false;
            this.numericBoxK.TabIndex = 119;
            this.numericBoxK.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxK.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxK.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxK.ThonsandsSeparator = true;
            this.numericBoxK.ToolTip = "";
            this.numericBoxK.UpDown_Increment = 1D;
            this.numericBoxK.Value = 0D;
            this.numericBoxK.WordWrap = true;
            // 
            // numericBoxL
            // 
            this.numericBoxL.AllowMouseControl = false;
            this.numericBoxL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxL.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxL.DecimalPlaces = 0;
            this.numericBoxL.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxL.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxL.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxL.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxL.FooterText = "";
            this.numericBoxL.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxL.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxL.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxL.HeaderText = "";
            this.numericBoxL.Location = new System.Drawing.Point(101, 18);
            this.numericBoxL.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxL.Maximum = 10D;
            this.numericBoxL.MaximumSize = new System.Drawing.Size(1000, 27);
            this.numericBoxL.Minimum = -10D;
            this.numericBoxL.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxL.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxL.MouseSpeed = 1D;
            this.numericBoxL.Multiline = false;
            this.numericBoxL.Name = "numericBoxL";
            this.numericBoxL.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxL.RadianValue = 0D;
            this.numericBoxL.ReadOnly = false;
            this.numericBoxL.RestrictLimitValue = true;
            this.numericBoxL.ShowFraction = false;
            this.numericBoxL.ShowPositiveSign = false;
            this.numericBoxL.ShowUpDown = true;
            this.numericBoxL.Size = new System.Drawing.Size(40, 27);
            this.numericBoxL.SkipEventDuringInput = false;
            this.numericBoxL.SmartIncrement = false;
            this.numericBoxL.TabIndex = 119;
            this.numericBoxL.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxL.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxL.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxL.ThonsandsSeparator = true;
            this.numericBoxL.ToolTip = "";
            this.numericBoxL.UpDown_Increment = 1D;
            this.numericBoxL.Value = 0D;
            this.numericBoxL.WordWrap = true;
            // 
            // checkBoxEquivalency
            // 
            this.checkBoxEquivalency.AutoSize = true;
            this.checkBoxEquivalency.Checked = true;
            this.checkBoxEquivalency.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEquivalency.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.checkBoxEquivalency.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.checkBoxEquivalency.Location = new System.Drawing.Point(161, 21);
            this.checkBoxEquivalency.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxEquivalency.Name = "checkBoxEquivalency";
            this.checkBoxEquivalency.Size = new System.Drawing.Size(89, 19);
            this.checkBoxEquivalency.TabIndex = 5;
            this.checkBoxEquivalency.Text = "Equivalency";
            this.checkBoxEquivalency.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 12F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(140, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 21);
            this.label2.TabIndex = 90;
            this.label2.Text = "}";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(23, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 15);
            this.label3.TabIndex = 90;
            this.label3.Text = "h";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(62, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 15);
            this.label4.TabIndex = 90;
            this.label4.Text = "k";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(103, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 15);
            this.label5.TabIndex = 90;
            this.label5.Text = "l";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(276, 2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(115, 15);
            this.label6.TabIndex = 90;
            this.label6.Text = "Distance from origin";
            // 
            // numericBoxDistance
            // 
            this.numericBoxDistance.AllowMouseControl = false;
            this.numericBoxDistance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDistance.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.DecimalPlaces = 4;
            this.numericBoxDistance.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxDistance.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxDistance.FooterText = "Å (= d×";
            this.numericBoxDistance.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxDistance.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxDistance.HeaderText = "";
            this.numericBoxDistance.Location = new System.Drawing.Point(260, 18);
            this.numericBoxDistance.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDistance.Maximum = 20D;
            this.numericBoxDistance.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDistance.Minimum = 0D;
            this.numericBoxDistance.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDistance.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxDistance.MouseSpeed = 1D;
            this.numericBoxDistance.Multiline = false;
            this.numericBoxDistance.Name = "numericBoxDistance";
            this.numericBoxDistance.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDistance.RadianValue = 0D;
            this.numericBoxDistance.ReadOnly = false;
            this.numericBoxDistance.RestrictLimitValue = true;
            this.numericBoxDistance.ShowFraction = false;
            this.numericBoxDistance.ShowPositiveSign = false;
            this.numericBoxDistance.ShowUpDown = false;
            this.numericBoxDistance.Size = new System.Drawing.Size(102, 25);
            this.numericBoxDistance.SkipEventDuringInput = false;
            this.numericBoxDistance.SmartIncrement = false;
            this.numericBoxDistance.TabIndex = 119;
            this.numericBoxDistance.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDistance.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDistance.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.ThonsandsSeparator = true;
            this.numericBoxDistance.ToolTip = "";
            this.numericBoxDistance.UpDown_Increment = 1D;
            this.numericBoxDistance.Value = 0D;
            this.numericBoxDistance.WordWrap = true;
            // 
            // numericBoxDistanceD
            // 
            this.numericBoxDistanceD.AllowMouseControl = false;
            this.numericBoxDistanceD.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDistanceD.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistanceD.DecimalPlaces = 4;
            this.numericBoxDistanceD.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistanceD.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistanceD.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxDistanceD.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxDistanceD.FooterText = ")";
            this.numericBoxDistanceD.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistanceD.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxDistanceD.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxDistanceD.HeaderText = "";
            this.numericBoxDistanceD.Location = new System.Drawing.Point(363, 18);
            this.numericBoxDistanceD.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDistanceD.Maximum = 20D;
            this.numericBoxDistanceD.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDistanceD.Minimum = 0D;
            this.numericBoxDistanceD.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDistanceD.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxDistanceD.MouseSpeed = 1D;
            this.numericBoxDistanceD.Multiline = false;
            this.numericBoxDistanceD.Name = "numericBoxDistanceD";
            this.numericBoxDistanceD.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDistanceD.RadianValue = 0D;
            this.numericBoxDistanceD.ReadOnly = false;
            this.numericBoxDistanceD.RestrictLimitValue = true;
            this.numericBoxDistanceD.ShowFraction = false;
            this.numericBoxDistanceD.ShowPositiveSign = false;
            this.numericBoxDistanceD.ShowUpDown = true;
            this.numericBoxDistanceD.Size = new System.Drawing.Size(74, 25);
            this.numericBoxDistanceD.SkipEventDuringInput = false;
            this.numericBoxDistanceD.SmartIncrement = false;
            this.numericBoxDistanceD.TabIndex = 119;
            this.numericBoxDistanceD.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDistanceD.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDistanceD.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistanceD.ThonsandsSeparator = true;
            this.numericBoxDistanceD.ToolTip = "";
            this.numericBoxDistanceD.UpDown_Increment = 1D;
            this.numericBoxDistanceD.Value = 0D;
            this.numericBoxDistanceD.WordWrap = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 9F);
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(453, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 15);
            this.label11.TabIndex = 120;
            this.label11.Text = "Color";
            // 
            // colorControl
            // 
            this.colorControl.Argb = -986896;
            this.colorControl.AutoSize = true;
            this.colorControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.colorControl.Blue = 240;
            this.colorControl.BlueF = 0.9411765F;
            this.colorControl.Color = System.Drawing.SystemColors.Control;
            this.colorControl.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorControl.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorControl.FooterText = "";
            this.colorControl.Green = 240;
            this.colorControl.GreenF = 0.9411765F;
            this.colorControl.Location = new System.Drawing.Point(491, 20);
            this.colorControl.Margin = new System.Windows.Forms.Padding(0);
            this.colorControl.Name = "colorControl";
            this.colorControl.Red = 240;
            this.colorControl.RedF = 0.9411765F;
            this.colorControl.Size = new System.Drawing.Size(18, 18);
            this.colorControl.TabIndex = 121;
            this.colorControl.ToolTip = "";
            // 
            // BoundControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "BoundControl";
            this.Size = new System.Drawing.Size(550, 365);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewImageColumn bondColorDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn centerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vertexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxLenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minLenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewImageColumn polyhedronColorDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource bindingSource;
        private DataSet dataSet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddBond;
        private System.Windows.Forms.Button buttonChangeBond;
        private System.Windows.Forms.Button buttonDeleteBond;
        private NumericBox numericBoxL;
        private NumericBox numericBoxK;
        private System.Windows.Forms.CheckBox checkBoxEquivalency;
        private NumericBox numericBoxH;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private NumericBox numericBoxDistanceD;
        private NumericBox numericBoxDistance;
        private System.Windows.Forms.Label label6;
        private ColorControl colorControl;
        private System.Windows.Forms.Label label11;
    }
}
