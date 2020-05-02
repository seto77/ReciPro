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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BondInputControl));
            this.checkBoxShowPolyhedron = new System.Windows.Forms.CheckBox();
            this.comboBoxBondingAtom1 = new System.Windows.Forms.ComboBox();
            this.comboBoxBondingAtom2 = new System.Windows.Forms.ComboBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.groupBoxPolyhedron = new System.Windows.Forms.GroupBox();
            this.checkBoxShowEdges = new System.Windows.Forms.CheckBox();
            this.groupBoxEdge = new System.Windows.Forms.GroupBox();
            this.checkBoxShowInnerBonds = new System.Windows.Forms.CheckBox();
            this.checkBoxShowVertexAtoms = new System.Windows.Forms.CheckBox();
            this.checkBoxShowCenterAtom = new System.Windows.Forms.CheckBox();
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
            this.dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn6 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn7 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn8 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn9 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn10 = new System.Windows.Forms.DataGridViewImageColumn();
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
            resources.ApplyResources(this.checkBoxShowPolyhedron, "checkBoxShowPolyhedron");
            this.checkBoxShowPolyhedron.Checked = true;
            this.checkBoxShowPolyhedron.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowPolyhedron.Name = "checkBoxShowPolyhedron";
            this.checkBoxShowPolyhedron.UseVisualStyleBackColor = true;
            this.checkBoxShowPolyhedron.CheckedChanged += new System.EventHandler(this.checkBoxShowPolyhedron_CheckedChanged);
            // 
            // comboBoxBondingAtom1
            // 
            this.comboBoxBondingAtom1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxBondingAtom1, "comboBoxBondingAtom1");
            this.comboBoxBondingAtom1.Name = "comboBoxBondingAtom1";
            // 
            // comboBoxBondingAtom2
            // 
            this.comboBoxBondingAtom2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxBondingAtom2, "comboBoxBondingAtom2");
            this.comboBoxBondingAtom2.Items.AddRange(new object[] {
            resources.GetString("comboBoxBondingAtom2.Items")});
            this.comboBoxBondingAtom2.Name = "comboBoxBondingAtom2";
            // 
            // label58
            // 
            resources.ApplyResources(this.label58, "label58");
            this.label58.Name = "label58";
            // 
            // label57
            // 
            resources.ApplyResources(this.label57, "label57");
            this.label57.Name = "label57";
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.Name = "label39";
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.Name = "label40";
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
            resources.ApplyResources(this.groupBoxPolyhedron, "groupBoxPolyhedron");
            this.groupBoxPolyhedron.Name = "groupBoxPolyhedron";
            this.groupBoxPolyhedron.TabStop = false;
            // 
            // checkBoxShowEdges
            // 
            resources.ApplyResources(this.checkBoxShowEdges, "checkBoxShowEdges");
            this.checkBoxShowEdges.Checked = true;
            this.checkBoxShowEdges.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowEdges.Name = "checkBoxShowEdges";
            this.checkBoxShowEdges.UseVisualStyleBackColor = true;
            this.checkBoxShowEdges.CheckedChanged += new System.EventHandler(this.checkBoxShowEdges_CheckedChanged);
            // 
            // groupBoxEdge
            // 
            this.groupBoxEdge.Controls.Add(this.numericBoxEdgeWidth);
            this.groupBoxEdge.Controls.Add(this.colorControlEdges);
            resources.ApplyResources(this.groupBoxEdge, "groupBoxEdge");
            this.groupBoxEdge.Name = "groupBoxEdge";
            this.groupBoxEdge.TabStop = false;
            // 
            // checkBoxShowInnerBonds
            // 
            resources.ApplyResources(this.checkBoxShowInnerBonds, "checkBoxShowInnerBonds");
            this.checkBoxShowInnerBonds.Checked = true;
            this.checkBoxShowInnerBonds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowInnerBonds.Name = "checkBoxShowInnerBonds";
            this.checkBoxShowInnerBonds.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowVertexAtoms
            // 
            resources.ApplyResources(this.checkBoxShowVertexAtoms, "checkBoxShowVertexAtoms");
            this.checkBoxShowVertexAtoms.Checked = true;
            this.checkBoxShowVertexAtoms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowVertexAtoms.Name = "checkBoxShowVertexAtoms";
            this.checkBoxShowVertexAtoms.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowCenterAtom
            // 
            resources.ApplyResources(this.checkBoxShowCenterAtom, "checkBoxShowCenterAtom");
            this.checkBoxShowCenterAtom.Checked = true;
            this.checkBoxShowCenterAtom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowCenterAtom.Name = "checkBoxShowCenterAtom";
            this.checkBoxShowCenterAtom.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxBondingAtom1);
            this.groupBox1.Controls.Add(this.colorControlBond);
            this.groupBox1.Controls.Add(this.numericBoxBondAlpha);
            this.groupBox1.Controls.Add(this.numericBoxBondRadius);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.numericBoxBondMaxLength);
            this.groupBox1.Controls.Add(this.numericBoxBondMinLength);
            this.groupBox1.Controls.Add(this.comboBoxBondingAtom2);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.label57);
            this.groupBox1.Controls.Add(this.label58);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonAddBond
            // 
            this.buttonAddBond.BackColor = System.Drawing.Color.SteelBlue;
            resources.ApplyResources(this.buttonAddBond, "buttonAddBond");
            this.buttonAddBond.ForeColor = System.Drawing.Color.White;
            this.buttonAddBond.Name = "buttonAddBond";
            this.buttonAddBond.UseVisualStyleBackColor = false;
            this.buttonAddBond.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonChangeBond
            // 
            this.buttonChangeBond.BackColor = System.Drawing.Color.SteelBlue;
            resources.ApplyResources(this.buttonChangeBond, "buttonChangeBond");
            this.buttonChangeBond.ForeColor = System.Drawing.Color.White;
            this.buttonChangeBond.Name = "buttonChangeBond";
            this.buttonChangeBond.UseVisualStyleBackColor = false;
            this.buttonChangeBond.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // buttonDeleteBond
            // 
            resources.ApplyResources(this.buttonDeleteBond, "buttonDeleteBond");
            this.buttonDeleteBond.BackColor = System.Drawing.Color.IndianRed;
            this.buttonDeleteBond.ForeColor = System.Drawing.Color.White;
            this.buttonDeleteBond.Name = "buttonDeleteBond";
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
            resources.ApplyResources(this.dataGridView, "dataGridView");
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            resources.ApplyResources(this.enabledDataGridViewCheckBoxColumn, "enabledDataGridViewCheckBoxColumn");
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            // 
            // centerDataGridViewTextBoxColumn
            // 
            this.centerDataGridViewTextBoxColumn.DataPropertyName = "Center";
            resources.ApplyResources(this.centerDataGridViewTextBoxColumn, "centerDataGridViewTextBoxColumn");
            this.centerDataGridViewTextBoxColumn.Name = "centerDataGridViewTextBoxColumn";
            this.centerDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // vertexDataGridViewTextBoxColumn
            // 
            this.vertexDataGridViewTextBoxColumn.DataPropertyName = "Vertex";
            resources.ApplyResources(this.vertexDataGridViewTextBoxColumn, "vertexDataGridViewTextBoxColumn");
            this.vertexDataGridViewTextBoxColumn.Name = "vertexDataGridViewTextBoxColumn";
            this.vertexDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // maxLenDataGridViewTextBoxColumn
            // 
            this.maxLenDataGridViewTextBoxColumn.DataPropertyName = "Max len.";
            resources.ApplyResources(this.maxLenDataGridViewTextBoxColumn, "maxLenDataGridViewTextBoxColumn");
            this.maxLenDataGridViewTextBoxColumn.Name = "maxLenDataGridViewTextBoxColumn";
            this.maxLenDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // minLenDataGridViewTextBoxColumn
            // 
            this.minLenDataGridViewTextBoxColumn.DataPropertyName = "Min len.";
            resources.ApplyResources(this.minLenDataGridViewTextBoxColumn, "minLenDataGridViewTextBoxColumn");
            this.minLenDataGridViewTextBoxColumn.Name = "minLenDataGridViewTextBoxColumn";
            this.minLenDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonAddBond);
            this.panel1.Controls.Add(this.buttonChangeBond);
            this.panel1.Controls.Add(this.buttonDeleteBond);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.checkBoxShowPolyhedron);
            this.panel2.Controls.Add(this.groupBoxPolyhedron);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Bond color";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.DataPropertyName = "Bond color";
            resources.ApplyResources(this.dataGridViewImageColumn1, "dataGridViewImageColumn1");
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(this.dataGridViewImageColumn2, "dataGridViewImageColumn2");
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.DataPropertyName = "Bond color";
            resources.ApplyResources(this.dataGridViewImageColumn3, "dataGridViewImageColumn3");
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn4
            // 
            this.dataGridViewImageColumn4.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(this.dataGridViewImageColumn4, "dataGridViewImageColumn4");
            this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            this.dataGridViewImageColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn5
            // 
            this.dataGridViewImageColumn5.DataPropertyName = "Bond color";
            resources.ApplyResources(this.dataGridViewImageColumn5, "dataGridViewImageColumn5");
            this.dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            this.dataGridViewImageColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn6
            // 
            this.dataGridViewImageColumn6.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(this.dataGridViewImageColumn6, "dataGridViewImageColumn6");
            this.dataGridViewImageColumn6.Name = "dataGridViewImageColumn6";
            this.dataGridViewImageColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn7
            // 
            this.dataGridViewImageColumn7.DataPropertyName = "Bond color";
            resources.ApplyResources(this.dataGridViewImageColumn7, "dataGridViewImageColumn7");
            this.dataGridViewImageColumn7.Name = "dataGridViewImageColumn7";
            this.dataGridViewImageColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn8
            // 
            this.dataGridViewImageColumn8.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(this.dataGridViewImageColumn8, "dataGridViewImageColumn8");
            this.dataGridViewImageColumn8.Name = "dataGridViewImageColumn8";
            this.dataGridViewImageColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn9
            // 
            this.dataGridViewImageColumn9.DataPropertyName = "Bond color";
            resources.ApplyResources(this.dataGridViewImageColumn9, "dataGridViewImageColumn9");
            this.dataGridViewImageColumn9.Name = "dataGridViewImageColumn9";
            this.dataGridViewImageColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn10
            // 
            this.dataGridViewImageColumn10.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(this.dataGridViewImageColumn10, "dataGridViewImageColumn10");
            this.dataGridViewImageColumn10.Name = "dataGridViewImageColumn10";
            this.dataGridViewImageColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // bondColorDataGridViewTextBoxColumn
            // 
            this.bondColorDataGridViewTextBoxColumn.DataPropertyName = "Bond color";
            resources.ApplyResources(this.bondColorDataGridViewTextBoxColumn, "bondColorDataGridViewTextBoxColumn");
            this.bondColorDataGridViewTextBoxColumn.Name = "bondColorDataGridViewTextBoxColumn";
            this.bondColorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // polyhedronColorDataGridViewTextBoxColumn
            // 
            this.polyhedronColorDataGridViewTextBoxColumn.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(this.polyhedronColorDataGridViewTextBoxColumn, "polyhedronColorDataGridViewTextBoxColumn");
            this.polyhedronColorDataGridViewTextBoxColumn.Name = "polyhedronColorDataGridViewTextBoxColumn";
            this.polyhedronColorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colorControlBond
            // 
            this.colorControlBond.Argb = -986896;
            resources.ApplyResources(this.colorControlBond, "colorControlBond");
            this.colorControlBond.Blue = 240;
            this.colorControlBond.BlueF = 0.9411765F;
            this.colorControlBond.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlBond.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControlBond.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlBond.Green = 240;
            this.colorControlBond.GreenF = 0.9411765F;
            this.colorControlBond.Name = "colorControlBond";
            this.colorControlBond.Red = 240;
            this.colorControlBond.RedF = 0.9411765F;
            this.colorControlBond.ToolTip = "";
            // 
            // numericBoxBondAlpha
            // 
            this.numericBoxBondAlpha.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxBondAlpha, "numericBoxBondAlpha");
            this.numericBoxBondAlpha.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondAlpha.DecimalPlaces = 1;
            this.numericBoxBondAlpha.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondAlpha.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondAlpha.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondAlpha.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondAlpha.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxBondAlpha.Maximum = 1D;
            this.numericBoxBondAlpha.Minimum = 0D;
            this.numericBoxBondAlpha.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxBondAlpha.MouseSpeed = 1D;
            this.numericBoxBondAlpha.Multiline = false;
            this.numericBoxBondAlpha.Name = "numericBoxBondAlpha";
            this.numericBoxBondAlpha.RadianValue = 0.012217304763960307D;
            this.numericBoxBondAlpha.ReadOnly = false;
            this.numericBoxBondAlpha.RestrictLimitValue = true;
            this.numericBoxBondAlpha.ShowFraction = false;
            this.numericBoxBondAlpha.ShowPositiveSign = false;
            this.numericBoxBondAlpha.ShowUpDown = true;
            this.numericBoxBondAlpha.SkipEventDuringInput = false;
            this.numericBoxBondAlpha.SmartIncrement = true;
            this.numericBoxBondAlpha.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBondAlpha.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBondAlpha.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondAlpha.ThonsandsSeparator = true;
            this.numericBoxBondAlpha.UpDown_Increment = 0.1D;
            this.numericBoxBondAlpha.Value = 0.7D;
            this.numericBoxBondAlpha.WordWrap = true;
            // 
            // numericBoxBondRadius
            // 
            this.numericBoxBondRadius.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxBondRadius, "numericBoxBondRadius");
            this.numericBoxBondRadius.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondRadius.DecimalPlaces = 3;
            this.numericBoxBondRadius.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondRadius.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondRadius.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondRadius.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondRadius.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxBondRadius.Maximum = 9.9D;
            this.numericBoxBondRadius.Minimum = 0.1D;
            this.numericBoxBondRadius.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxBondRadius.MouseSpeed = 1D;
            this.numericBoxBondRadius.Multiline = false;
            this.numericBoxBondRadius.Name = "numericBoxBondRadius";
            this.numericBoxBondRadius.RadianValue = 0.0017453292519943296D;
            this.numericBoxBondRadius.ReadOnly = false;
            this.numericBoxBondRadius.RestrictLimitValue = true;
            this.numericBoxBondRadius.ShowFraction = false;
            this.numericBoxBondRadius.ShowPositiveSign = false;
            this.numericBoxBondRadius.ShowUpDown = true;
            this.numericBoxBondRadius.SkipEventDuringInput = false;
            this.numericBoxBondRadius.SmartIncrement = true;
            this.numericBoxBondRadius.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBondRadius.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBondRadius.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondRadius.ThonsandsSeparator = true;
            this.numericBoxBondRadius.UpDown_Increment = 0.02D;
            this.numericBoxBondRadius.Value = 0.1D;
            this.numericBoxBondRadius.WordWrap = true;
            // 
            // numericBoxBondMaxLength
            // 
            this.numericBoxBondMaxLength.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxBondMaxLength, "numericBoxBondMaxLength");
            this.numericBoxBondMaxLength.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMaxLength.DecimalPlaces = 3;
            this.numericBoxBondMaxLength.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMaxLength.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondMaxLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMaxLength.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondMaxLength.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxBondMaxLength.Maximum = 9.9D;
            this.numericBoxBondMaxLength.Minimum = 0.1D;
            this.numericBoxBondMaxLength.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxBondMaxLength.MouseSpeed = 1D;
            this.numericBoxBondMaxLength.Multiline = false;
            this.numericBoxBondMaxLength.Name = "numericBoxBondMaxLength";
            this.numericBoxBondMaxLength.RadianValue = 0.027925268031909273D;
            this.numericBoxBondMaxLength.ReadOnly = false;
            this.numericBoxBondMaxLength.RestrictLimitValue = true;
            this.numericBoxBondMaxLength.ShowFraction = false;
            this.numericBoxBondMaxLength.ShowPositiveSign = false;
            this.numericBoxBondMaxLength.ShowUpDown = true;
            this.numericBoxBondMaxLength.SkipEventDuringInput = false;
            this.numericBoxBondMaxLength.SmartIncrement = true;
            this.numericBoxBondMaxLength.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBondMaxLength.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBondMaxLength.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondMaxLength.ThonsandsSeparator = true;
            this.numericBoxBondMaxLength.UpDown_Increment = 0.1D;
            this.numericBoxBondMaxLength.Value = 1.6D;
            this.numericBoxBondMaxLength.WordWrap = true;
            // 
            // numericBoxBondMinLength
            // 
            this.numericBoxBondMinLength.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxBondMinLength, "numericBoxBondMinLength");
            this.numericBoxBondMinLength.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMinLength.DecimalPlaces = 3;
            this.numericBoxBondMinLength.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMinLength.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondMinLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBondMinLength.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBondMinLength.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxBondMinLength.Maximum = 9.9D;
            this.numericBoxBondMinLength.Minimum = 0D;
            this.numericBoxBondMinLength.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxBondMinLength.MouseSpeed = 1D;
            this.numericBoxBondMinLength.Multiline = false;
            this.numericBoxBondMinLength.Name = "numericBoxBondMinLength";
            this.numericBoxBondMinLength.RadianValue = 0.0017453292519943296D;
            this.numericBoxBondMinLength.ReadOnly = false;
            this.numericBoxBondMinLength.RestrictLimitValue = true;
            this.numericBoxBondMinLength.ShowFraction = false;
            this.numericBoxBondMinLength.ShowPositiveSign = false;
            this.numericBoxBondMinLength.ShowUpDown = true;
            this.numericBoxBondMinLength.SkipEventDuringInput = false;
            this.numericBoxBondMinLength.SmartIncrement = true;
            this.numericBoxBondMinLength.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBondMinLength.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBondMinLength.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBondMinLength.ThonsandsSeparator = true;
            this.numericBoxBondMinLength.UpDown_Increment = 0.1D;
            this.numericBoxBondMinLength.Value = 0.1D;
            this.numericBoxBondMinLength.WordWrap = true;
            // 
            // numericBoxPolyhedronAlpha
            // 
            this.numericBoxPolyhedronAlpha.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxPolyhedronAlpha, "numericBoxPolyhedronAlpha");
            this.numericBoxPolyhedronAlpha.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPolyhedronAlpha.DecimalPlaces = 1;
            this.numericBoxPolyhedronAlpha.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPolyhedronAlpha.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxPolyhedronAlpha.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPolyhedronAlpha.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxPolyhedronAlpha.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxPolyhedronAlpha.Maximum = 1D;
            this.numericBoxPolyhedronAlpha.Minimum = 0D;
            this.numericBoxPolyhedronAlpha.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxPolyhedronAlpha.MouseSpeed = 1D;
            this.numericBoxPolyhedronAlpha.Multiline = false;
            this.numericBoxPolyhedronAlpha.Name = "numericBoxPolyhedronAlpha";
            this.numericBoxPolyhedronAlpha.RadianValue = 0.012217304763960307D;
            this.numericBoxPolyhedronAlpha.ReadOnly = false;
            this.numericBoxPolyhedronAlpha.RestrictLimitValue = true;
            this.numericBoxPolyhedronAlpha.ShowFraction = false;
            this.numericBoxPolyhedronAlpha.ShowPositiveSign = false;
            this.numericBoxPolyhedronAlpha.ShowUpDown = true;
            this.numericBoxPolyhedronAlpha.SkipEventDuringInput = false;
            this.numericBoxPolyhedronAlpha.SmartIncrement = true;
            this.numericBoxPolyhedronAlpha.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxPolyhedronAlpha.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxPolyhedronAlpha.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxPolyhedronAlpha.ThonsandsSeparator = true;
            this.numericBoxPolyhedronAlpha.UpDown_Increment = 0.1D;
            this.numericBoxPolyhedronAlpha.Value = 0.7D;
            this.numericBoxPolyhedronAlpha.WordWrap = true;
            // 
            // colorControlPlyhedron
            // 
            this.colorControlPlyhedron.Argb = -986896;
            resources.ApplyResources(this.colorControlPlyhedron, "colorControlPlyhedron");
            this.colorControlPlyhedron.Blue = 240;
            this.colorControlPlyhedron.BlueF = 0.9411765F;
            this.colorControlPlyhedron.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlPlyhedron.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControlPlyhedron.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlPlyhedron.Green = 240;
            this.colorControlPlyhedron.GreenF = 0.9411765F;
            this.colorControlPlyhedron.Name = "colorControlPlyhedron";
            this.colorControlPlyhedron.Red = 240;
            this.colorControlPlyhedron.RedF = 0.9411765F;
            this.colorControlPlyhedron.ToolTip = "";
            // 
            // numericBoxEdgeWidth
            // 
            this.numericBoxEdgeWidth.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEdgeWidth, "numericBoxEdgeWidth");
            this.numericBoxEdgeWidth.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEdgeWidth.DecimalPlaces = 1;
            this.numericBoxEdgeWidth.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEdgeWidth.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEdgeWidth.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEdgeWidth.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEdgeWidth.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEdgeWidth.Maximum = 1D;
            this.numericBoxEdgeWidth.Minimum = 0D;
            this.numericBoxEdgeWidth.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxEdgeWidth.MouseSpeed = 1D;
            this.numericBoxEdgeWidth.Multiline = false;
            this.numericBoxEdgeWidth.Name = "numericBoxEdgeWidth";
            this.numericBoxEdgeWidth.RadianValue = 0.012217304763960307D;
            this.numericBoxEdgeWidth.ReadOnly = false;
            this.numericBoxEdgeWidth.RestrictLimitValue = true;
            this.numericBoxEdgeWidth.ShowFraction = false;
            this.numericBoxEdgeWidth.ShowPositiveSign = false;
            this.numericBoxEdgeWidth.ShowUpDown = true;
            this.numericBoxEdgeWidth.SkipEventDuringInput = false;
            this.numericBoxEdgeWidth.SmartIncrement = true;
            this.numericBoxEdgeWidth.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEdgeWidth.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEdgeWidth.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxEdgeWidth.ThonsandsSeparator = true;
            this.numericBoxEdgeWidth.UpDown_Increment = 0.1D;
            this.numericBoxEdgeWidth.Value = 0.7D;
            this.numericBoxEdgeWidth.WordWrap = true;
            // 
            // colorControlEdges
            // 
            this.colorControlEdges.Argb = -986896;
            resources.ApplyResources(this.colorControlEdges, "colorControlEdges");
            this.colorControlEdges.Blue = 240;
            this.colorControlEdges.BlueF = 0.9411765F;
            this.colorControlEdges.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlEdges.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControlEdges.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlEdges.Green = 240;
            this.colorControlEdges.GreenF = 0.9411765F;
            this.colorControlEdges.Name = "colorControlEdges";
            this.colorControlEdges.Red = 240;
            this.colorControlEdges.RedF = 0.9411765F;
            this.colorControlEdges.ToolTip = "";
            // 
            // BondInputControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "BondInputControl";
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
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.GroupBox groupBoxPolyhedron;
        private ColorControl colorControlPlyhedron;
        private System.Windows.Forms.CheckBox checkBoxShowEdges;
        private System.Windows.Forms.GroupBox groupBoxEdge;
        private ColorControl colorControlEdges;
        private System.Windows.Forms.CheckBox checkBoxShowInnerBonds;
        private System.Windows.Forms.CheckBox checkBoxShowVertexAtoms;
        private System.Windows.Forms.CheckBox checkBoxShowCenterAtom;
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
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn6;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn7;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn8;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn9;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn10;
    }
}
