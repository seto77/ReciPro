namespace Crystallography.Controls
{
    partial class BondInputControl
    {
        /// <summary>必要なデザイナー変数です。</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>使用中のリソースをすべてクリーンアップします。</summary>
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BondInputControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            checkBoxShowPolyhedron = new System.Windows.Forms.CheckBox();
            comboBoxBondingAtom1 = new System.Windows.Forms.ComboBox();
            comboBoxBondingAtom2 = new System.Windows.Forms.ComboBox();
            label58 = new System.Windows.Forms.Label();
            label57 = new System.Windows.Forms.Label();
            label39 = new System.Windows.Forms.Label();
            label40 = new System.Windows.Forms.Label();
            groupBoxPolyhedron = new System.Windows.Forms.GroupBox();
            numericBoxPolyhedronAlpha = new NumericBox();
            checkBoxShowEdges = new System.Windows.Forms.CheckBox();
            groupBoxEdge = new System.Windows.Forms.GroupBox();
            numericBoxEdgeWidth = new NumericBox();
            checkBoxShowInnerBonds = new System.Windows.Forms.CheckBox();
            checkBoxShowVertexAtoms = new System.Windows.Forms.CheckBox();
            checkBoxShowCenterAtom = new System.Windows.Forms.CheckBox();
            groupBoxBonds = new System.Windows.Forms.GroupBox();
            numericBoxBondAlpha = new NumericBox();
            numericBoxBondRadius = new NumericBox();
            numericBoxBondMaxLength = new NumericBox();
            numericBoxBondMinLength = new NumericBox();
            checkBoxShowBonds = new System.Windows.Forms.CheckBox();
            buttonAddBond = new System.Windows.Forms.Button();
            buttonChangeBond = new System.Windows.Forms.Button();
            buttonDeleteBond = new System.Windows.Forms.Button();
            dataGridView = new System.Windows.Forms.DataGridView();
            enabledDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            centerDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            vertexDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            minLenDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            maxLenDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            showBondsDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            showPolyhedronDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            bindingSource = new System.Windows.Forms.BindingSource(components);
            dataSet = new DataSet();
            panel1 = new System.Windows.Forms.Panel();
            colorControlEdges = new ColorControl();
            colorControlPolyhedron = new ColorControl();
            colorControlBond = new ColorControl();
            panel2 = new System.Windows.Forms.Panel();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn6 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn7 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn8 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn9 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn10 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn11 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn12 = new System.Windows.Forms.DataGridViewImageColumn();
            enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            centerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            vertexDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            minLenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            maxLenDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            Center = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Vertex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            toolTip = new System.Windows.Forms.ToolTip(components);
            groupBoxPolyhedron.SuspendLayout();
            groupBoxEdge.SuspendLayout();
            groupBoxBonds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // checkBoxShowPolyhedron
            // 
            resources.ApplyResources(checkBoxShowPolyhedron, "checkBoxShowPolyhedron");
            checkBoxShowPolyhedron.Checked = true;
            checkBoxShowPolyhedron.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowPolyhedron.Name = "checkBoxShowPolyhedron";
            checkBoxShowPolyhedron.UseVisualStyleBackColor = true;
            checkBoxShowPolyhedron.CheckedChanged += checkBoxShowPolyhedron_CheckedChanged;
            // 
            // comboBoxBondingAtom1
            // 
            comboBoxBondingAtom1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxBondingAtom1, "comboBoxBondingAtom1");
            comboBoxBondingAtom1.Name = "comboBoxBondingAtom1";
            toolTip.SetToolTip(comboBoxBondingAtom1, resources.GetString("comboBoxBondingAtom1.ToolTip"));
            // 
            // comboBoxBondingAtom2
            // 
            comboBoxBondingAtom2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxBondingAtom2, "comboBoxBondingAtom2");
            comboBoxBondingAtom2.Items.AddRange(new object[] { resources.GetString("comboBoxBondingAtom2.Items") });
            comboBoxBondingAtom2.Name = "comboBoxBondingAtom2";
            toolTip.SetToolTip(comboBoxBondingAtom2, resources.GetString("comboBoxBondingAtom2.ToolTip"));
            // 
            // label58
            // 
            resources.ApplyResources(label58, "label58");
            label58.Name = "label58";
            // 
            // label57
            // 
            resources.ApplyResources(label57, "label57");
            label57.Name = "label57";
            // 
            // label39
            // 
            resources.ApplyResources(label39, "label39");
            label39.Name = "label39";
            // 
            // label40
            // 
            resources.ApplyResources(label40, "label40");
            label40.Name = "label40";
            // 
            // groupBoxPolyhedron
            // 
            groupBoxPolyhedron.Controls.Add(numericBoxPolyhedronAlpha);
            groupBoxPolyhedron.Controls.Add(checkBoxShowEdges);
            groupBoxPolyhedron.Controls.Add(groupBoxEdge);
            groupBoxPolyhedron.Controls.Add(checkBoxShowInnerBonds);
            groupBoxPolyhedron.Controls.Add(checkBoxShowVertexAtoms);
            groupBoxPolyhedron.Controls.Add(checkBoxShowCenterAtom);
            resources.ApplyResources(groupBoxPolyhedron, "groupBoxPolyhedron");
            groupBoxPolyhedron.Name = "groupBoxPolyhedron";
            groupBoxPolyhedron.TabStop = false;
            // 
            // numericBoxPolyhedronAlpha
            // 
            resources.ApplyResources(numericBoxPolyhedronAlpha, "numericBoxPolyhedronAlpha");
            numericBoxPolyhedronAlpha.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPolyhedronAlpha.DecimalPlaces = 1;
            numericBoxPolyhedronAlpha.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxPolyhedronAlpha.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxPolyhedronAlpha.Maximum = 1D;
            numericBoxPolyhedronAlpha.Minimum = 0D;
            numericBoxPolyhedronAlpha.Name = "numericBoxPolyhedronAlpha";
            numericBoxPolyhedronAlpha.RadianValue = 0.012217304763960306D;
            numericBoxPolyhedronAlpha.ShowUpDown = true;
            numericBoxPolyhedronAlpha.SkipEventDuringInput = false;
            numericBoxPolyhedronAlpha.SmartIncrement = true;
            numericBoxPolyhedronAlpha.ThonsandsSeparator = true;
            numericBoxPolyhedronAlpha.UpDown_Increment = 0.1D;
            numericBoxPolyhedronAlpha.Value = 0.7D;
            // 
            // checkBoxShowEdges
            // 
            resources.ApplyResources(checkBoxShowEdges, "checkBoxShowEdges");
            checkBoxShowEdges.Checked = true;
            checkBoxShowEdges.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowEdges.Name = "checkBoxShowEdges";
            checkBoxShowEdges.UseVisualStyleBackColor = true;
            checkBoxShowEdges.CheckedChanged += checkBoxShowEdges_CheckedChanged;
            // 
            // groupBoxEdge
            // 
            groupBoxEdge.Controls.Add(numericBoxEdgeWidth);
            resources.ApplyResources(groupBoxEdge, "groupBoxEdge");
            groupBoxEdge.Name = "groupBoxEdge";
            groupBoxEdge.TabStop = false;
            // 
            // numericBoxEdgeWidth
            // 
            resources.ApplyResources(numericBoxEdgeWidth, "numericBoxEdgeWidth");
            numericBoxEdgeWidth.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEdgeWidth.DecimalPlaces = 1;
            numericBoxEdgeWidth.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxEdgeWidth.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxEdgeWidth.Maximum = 1D;
            numericBoxEdgeWidth.Minimum = 0D;
            numericBoxEdgeWidth.Name = "numericBoxEdgeWidth";
            numericBoxEdgeWidth.RadianValue = 0.012217304763960306D;
            numericBoxEdgeWidth.ShowUpDown = true;
            numericBoxEdgeWidth.SkipEventDuringInput = false;
            numericBoxEdgeWidth.SmartIncrement = true;
            numericBoxEdgeWidth.ThonsandsSeparator = true;
            numericBoxEdgeWidth.UpDown_Increment = 0.1D;
            numericBoxEdgeWidth.Value = 0.7D;
            // 
            // checkBoxShowInnerBonds
            // 
            resources.ApplyResources(checkBoxShowInnerBonds, "checkBoxShowInnerBonds");
            checkBoxShowInnerBonds.Checked = true;
            checkBoxShowInnerBonds.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowInnerBonds.Name = "checkBoxShowInnerBonds";
            checkBoxShowInnerBonds.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowVertexAtoms
            // 
            resources.ApplyResources(checkBoxShowVertexAtoms, "checkBoxShowVertexAtoms");
            checkBoxShowVertexAtoms.Checked = true;
            checkBoxShowVertexAtoms.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowVertexAtoms.Name = "checkBoxShowVertexAtoms";
            checkBoxShowVertexAtoms.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowCenterAtom
            // 
            resources.ApplyResources(checkBoxShowCenterAtom, "checkBoxShowCenterAtom");
            checkBoxShowCenterAtom.Checked = true;
            checkBoxShowCenterAtom.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowCenterAtom.Name = "checkBoxShowCenterAtom";
            checkBoxShowCenterAtom.UseVisualStyleBackColor = true;
            // 
            // groupBoxBonds
            // 
            groupBoxBonds.Controls.Add(comboBoxBondingAtom1);
            groupBoxBonds.Controls.Add(numericBoxBondAlpha);
            groupBoxBonds.Controls.Add(numericBoxBondRadius);
            groupBoxBonds.Controls.Add(label39);
            groupBoxBonds.Controls.Add(numericBoxBondMaxLength);
            groupBoxBonds.Controls.Add(numericBoxBondMinLength);
            groupBoxBonds.Controls.Add(comboBoxBondingAtom2);
            groupBoxBonds.Controls.Add(label40);
            groupBoxBonds.Controls.Add(label57);
            groupBoxBonds.Controls.Add(label58);
            resources.ApplyResources(groupBoxBonds, "groupBoxBonds");
            groupBoxBonds.Name = "groupBoxBonds";
            groupBoxBonds.TabStop = false;
            // 
            // numericBoxBondAlpha
            // 
            resources.ApplyResources(numericBoxBondAlpha, "numericBoxBondAlpha");
            numericBoxBondAlpha.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBondAlpha.DecimalPlaces = 1;
            numericBoxBondAlpha.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBondAlpha.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBondAlpha.Maximum = 1D;
            numericBoxBondAlpha.Minimum = 0D;
            numericBoxBondAlpha.Name = "numericBoxBondAlpha";
            numericBoxBondAlpha.RadianValue = 0.012217304763960306D;
            numericBoxBondAlpha.ShowUpDown = true;
            numericBoxBondAlpha.SkipEventDuringInput = false;
            numericBoxBondAlpha.SmartIncrement = true;
            numericBoxBondAlpha.ThonsandsSeparator = true;
            numericBoxBondAlpha.UpDown_Increment = 0.1D;
            numericBoxBondAlpha.Value = 0.7D;
            // 
            // numericBoxBondRadius
            // 
            resources.ApplyResources(numericBoxBondRadius, "numericBoxBondRadius");
            numericBoxBondRadius.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBondRadius.DecimalPlaces = 3;
            numericBoxBondRadius.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBondRadius.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBondRadius.Maximum = 9.9D;
            numericBoxBondRadius.Minimum = 0.1D;
            numericBoxBondRadius.Name = "numericBoxBondRadius";
            numericBoxBondRadius.RadianValue = 0.0017453292519943296D;
            numericBoxBondRadius.ShowUpDown = true;
            numericBoxBondRadius.SkipEventDuringInput = false;
            numericBoxBondRadius.SmartIncrement = true;
            numericBoxBondRadius.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxBondRadius, resources.GetString("numericBoxBondRadius.ToolTip"));
            numericBoxBondRadius.UpDown_Increment = 0.02D;
            numericBoxBondRadius.Value = 0.1D;
            // 
            // numericBoxBondMaxLength
            // 
            resources.ApplyResources(numericBoxBondMaxLength, "numericBoxBondMaxLength");
            numericBoxBondMaxLength.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBondMaxLength.DecimalPlaces = 3;
            numericBoxBondMaxLength.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBondMaxLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBondMaxLength.Maximum = 9.9D;
            numericBoxBondMaxLength.Minimum = 0.1D;
            numericBoxBondMaxLength.Name = "numericBoxBondMaxLength";
            numericBoxBondMaxLength.RadianValue = 0.027925268031909273D;
            numericBoxBondMaxLength.ShowUpDown = true;
            numericBoxBondMaxLength.SkipEventDuringInput = false;
            numericBoxBondMaxLength.SmartIncrement = true;
            numericBoxBondMaxLength.ThonsandsSeparator = true;
            numericBoxBondMaxLength.UpDown_Increment = 0.1D;
            numericBoxBondMaxLength.Value = 1.6D;
            // 
            // numericBoxBondMinLength
            // 
            resources.ApplyResources(numericBoxBondMinLength, "numericBoxBondMinLength");
            numericBoxBondMinLength.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBondMinLength.DecimalPlaces = 3;
            numericBoxBondMinLength.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBondMinLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBondMinLength.Maximum = 9.9D;
            numericBoxBondMinLength.Minimum = 0D;
            numericBoxBondMinLength.Name = "numericBoxBondMinLength";
            numericBoxBondMinLength.RadianValue = 0.0017453292519943296D;
            numericBoxBondMinLength.ShowUpDown = true;
            numericBoxBondMinLength.SkipEventDuringInput = false;
            numericBoxBondMinLength.SmartIncrement = true;
            numericBoxBondMinLength.ThonsandsSeparator = true;
            numericBoxBondMinLength.UpDown_Increment = 0.1D;
            numericBoxBondMinLength.Value = 0.1D;
            // 
            // checkBoxShowBonds
            // 
            resources.ApplyResources(checkBoxShowBonds, "checkBoxShowBonds");
            checkBoxShowBonds.Checked = true;
            checkBoxShowBonds.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowBonds.Name = "checkBoxShowBonds";
            checkBoxShowBonds.UseVisualStyleBackColor = true;
            checkBoxShowBonds.CheckedChanged += checkBoxShowBonds_CheckedChanged;
            // 
            // buttonAddBond
            // 
            buttonAddBond.BackColor = System.Drawing.Color.SteelBlue;
            resources.ApplyResources(buttonAddBond, "buttonAddBond");
            buttonAddBond.ForeColor = System.Drawing.Color.White;
            buttonAddBond.Name = "buttonAddBond";
            toolTip.SetToolTip(buttonAddBond, resources.GetString("buttonAddBond.ToolTip"));
            buttonAddBond.UseVisualStyleBackColor = false;
            buttonAddBond.Click += buttonAdd_Click;
            // 
            // buttonChangeBond
            // 
            buttonChangeBond.BackColor = System.Drawing.Color.SteelBlue;
            resources.ApplyResources(buttonChangeBond, "buttonChangeBond");
            buttonChangeBond.ForeColor = System.Drawing.Color.White;
            buttonChangeBond.Name = "buttonChangeBond";
            toolTip.SetToolTip(buttonChangeBond, resources.GetString("buttonChangeBond.ToolTip"));
            buttonChangeBond.UseVisualStyleBackColor = false;
            buttonChangeBond.Click += buttonChange_Click;
            // 
            // buttonDeleteBond
            // 
            resources.ApplyResources(buttonDeleteBond, "buttonDeleteBond");
            buttonDeleteBond.BackColor = System.Drawing.Color.IndianRed;
            buttonDeleteBond.ForeColor = System.Drawing.Color.White;
            buttonDeleteBond.Name = "buttonDeleteBond";
            toolTip.SetToolTip(buttonDeleteBond, resources.GetString("buttonDeleteBond.ToolTip"));
            buttonDeleteBond.UseVisualStyleBackColor = false;
            buttonDeleteBond.Click += buttonDelete_Click;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("メイリオ", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { enabledDataGridViewCheckBoxColumn1, centerDataGridViewTextBoxColumn1, vertexDataGridViewTextBoxColumn1, minLenDataGridViewTextBoxColumn1, maxLenDataGridViewTextBoxColumn1, showBondsDataGridViewCheckBoxColumn, showPolyhedronDataGridViewCheckBoxColumn });
            dataGridView.DataSource = bindingSource;
            resources.ApplyResources(dataGridView, "dataGridView");
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;
            dataGridView.CurrentCellDirtyStateChanged += dataGridView_CurrentCellDirtyStateChanged;
            // 
            // enabledDataGridViewCheckBoxColumn1
            // 
            enabledDataGridViewCheckBoxColumn1.DataPropertyName = "Enabled";
            resources.ApplyResources(enabledDataGridViewCheckBoxColumn1, "enabledDataGridViewCheckBoxColumn1");
            enabledDataGridViewCheckBoxColumn1.Name = "enabledDataGridViewCheckBoxColumn1";
            // 
            // centerDataGridViewTextBoxColumn1
            // 
            centerDataGridViewTextBoxColumn1.DataPropertyName = "Center";
            resources.ApplyResources(centerDataGridViewTextBoxColumn1, "centerDataGridViewTextBoxColumn1");
            centerDataGridViewTextBoxColumn1.Name = "centerDataGridViewTextBoxColumn1";
            centerDataGridViewTextBoxColumn1.ReadOnly = true;
            centerDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // vertexDataGridViewTextBoxColumn1
            // 
            vertexDataGridViewTextBoxColumn1.DataPropertyName = "Vertex";
            resources.ApplyResources(vertexDataGridViewTextBoxColumn1, "vertexDataGridViewTextBoxColumn1");
            vertexDataGridViewTextBoxColumn1.Name = "vertexDataGridViewTextBoxColumn1";
            vertexDataGridViewTextBoxColumn1.ReadOnly = true;
            vertexDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // minLenDataGridViewTextBoxColumn1
            // 
            minLenDataGridViewTextBoxColumn1.DataPropertyName = "Min len.";
            resources.ApplyResources(minLenDataGridViewTextBoxColumn1, "minLenDataGridViewTextBoxColumn1");
            minLenDataGridViewTextBoxColumn1.Name = "minLenDataGridViewTextBoxColumn1";
            minLenDataGridViewTextBoxColumn1.ReadOnly = true;
            minLenDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // maxLenDataGridViewTextBoxColumn1
            // 
            maxLenDataGridViewTextBoxColumn1.DataPropertyName = "Max len.";
            resources.ApplyResources(maxLenDataGridViewTextBoxColumn1, "maxLenDataGridViewTextBoxColumn1");
            maxLenDataGridViewTextBoxColumn1.Name = "maxLenDataGridViewTextBoxColumn1";
            maxLenDataGridViewTextBoxColumn1.ReadOnly = true;
            maxLenDataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // showBondsDataGridViewCheckBoxColumn
            // 
            showBondsDataGridViewCheckBoxColumn.DataPropertyName = "Show bonds";
            resources.ApplyResources(showBondsDataGridViewCheckBoxColumn, "showBondsDataGridViewCheckBoxColumn");
            showBondsDataGridViewCheckBoxColumn.Name = "showBondsDataGridViewCheckBoxColumn";
            // 
            // showPolyhedronDataGridViewCheckBoxColumn
            // 
            showPolyhedronDataGridViewCheckBoxColumn.DataPropertyName = "Show Polyhedron";
            resources.ApplyResources(showPolyhedronDataGridViewCheckBoxColumn, "showPolyhedronDataGridViewCheckBoxColumn");
            showPolyhedronDataGridViewCheckBoxColumn.Name = "showPolyhedronDataGridViewCheckBoxColumn";
            // 
            // bindingSource
            // 
            bindingSource.DataMember = "DataTableBond";
            bindingSource.DataSource = dataSet;
            bindingSource.CurrentChanged += bindingSource_PositionChanged;
            bindingSource.PositionChanged += bindingSource_PositionChanged;
            // 
            // dataSet
            // 
            dataSet.DataSetName = "DataSet";
            dataSet.Namespace = "http://tempuri.org/DataSet1.xsd";
            dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonAddBond);
            panel1.Controls.Add(colorControlEdges);
            panel1.Controls.Add(colorControlPolyhedron);
            panel1.Controls.Add(colorControlBond);
            panel1.Controls.Add(buttonChangeBond);
            panel1.Controls.Add(buttonDeleteBond);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // colorControlEdges
            // 
            colorControlEdges.Argb = -986896;
            resources.ApplyResources(colorControlEdges, "colorControlEdges");
            colorControlEdges.BackColor = System.Drawing.SystemColors.Control;
            colorControlEdges.Blue = 240;
            colorControlEdges.BlueF = 0.9411765F;
            colorControlEdges.BoxSize = new System.Drawing.Size(20, 20);
            colorControlEdges.Color = System.Drawing.Color.FromArgb(240, 240, 240);
            colorControlEdges.Green = 240;
            colorControlEdges.GreenF = 0.9411765F;
            colorControlEdges.Name = "colorControlEdges";
            colorControlEdges.Red = 240;
            colorControlEdges.RedF = 0.9411765F;
            // 
            // colorControlPolyhedron
            // 
            colorControlPolyhedron.Argb = -986896;
            resources.ApplyResources(colorControlPolyhedron, "colorControlPolyhedron");
            colorControlPolyhedron.BackColor = System.Drawing.SystemColors.Control;
            colorControlPolyhedron.Blue = 240;
            colorControlPolyhedron.BlueF = 0.9411765F;
            colorControlPolyhedron.BoxSize = new System.Drawing.Size(20, 20);
            colorControlPolyhedron.Color = System.Drawing.Color.FromArgb(240, 240, 240);
            colorControlPolyhedron.Green = 240;
            colorControlPolyhedron.GreenF = 0.9411765F;
            colorControlPolyhedron.Name = "colorControlPolyhedron";
            colorControlPolyhedron.Red = 240;
            colorControlPolyhedron.RedF = 0.9411765F;
            // 
            // colorControlBond
            // 
            colorControlBond.Argb = -986896;
            resources.ApplyResources(colorControlBond, "colorControlBond");
            colorControlBond.BackColor = System.Drawing.SystemColors.Control;
            colorControlBond.Blue = 240;
            colorControlBond.BlueF = 0.9411765F;
            colorControlBond.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBond.Color = System.Drawing.Color.FromArgb(240, 240, 240);
            colorControlBond.Green = 240;
            colorControlBond.GreenF = 0.9411765F;
            colorControlBond.Name = "colorControlBond";
            colorControlBond.Red = 240;
            colorControlBond.RedF = 0.9411765F;
            // 
            // panel2
            // 
            panel2.Controls.Add(checkBoxShowBonds);
            panel2.Controls.Add(groupBoxBonds);
            panel2.Controls.Add(checkBoxShowPolyhedron);
            panel2.Controls.Add(groupBoxPolyhedron);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "Bond color";
            resources.ApplyResources(dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewImageColumn1
            // 
            dataGridViewImageColumn1.DataPropertyName = "Bond color";
            resources.ApplyResources(dataGridViewImageColumn1, "dataGridViewImageColumn1");
            dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn2
            // 
            dataGridViewImageColumn2.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(dataGridViewImageColumn2, "dataGridViewImageColumn2");
            dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn3
            // 
            dataGridViewImageColumn3.DataPropertyName = "Bond color";
            resources.ApplyResources(dataGridViewImageColumn3, "dataGridViewImageColumn3");
            dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn4
            // 
            dataGridViewImageColumn4.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(dataGridViewImageColumn4, "dataGridViewImageColumn4");
            dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            dataGridViewImageColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn5
            // 
            dataGridViewImageColumn5.DataPropertyName = "Bond color";
            resources.ApplyResources(dataGridViewImageColumn5, "dataGridViewImageColumn5");
            dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            dataGridViewImageColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn6
            // 
            dataGridViewImageColumn6.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(dataGridViewImageColumn6, "dataGridViewImageColumn6");
            dataGridViewImageColumn6.Name = "dataGridViewImageColumn6";
            dataGridViewImageColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn7
            // 
            dataGridViewImageColumn7.DataPropertyName = "Bond color";
            resources.ApplyResources(dataGridViewImageColumn7, "dataGridViewImageColumn7");
            dataGridViewImageColumn7.Name = "dataGridViewImageColumn7";
            dataGridViewImageColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn8
            // 
            dataGridViewImageColumn8.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(dataGridViewImageColumn8, "dataGridViewImageColumn8");
            dataGridViewImageColumn8.Name = "dataGridViewImageColumn8";
            dataGridViewImageColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn9
            // 
            dataGridViewImageColumn9.DataPropertyName = "Bond color";
            resources.ApplyResources(dataGridViewImageColumn9, "dataGridViewImageColumn9");
            dataGridViewImageColumn9.Name = "dataGridViewImageColumn9";
            dataGridViewImageColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn10
            // 
            dataGridViewImageColumn10.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(dataGridViewImageColumn10, "dataGridViewImageColumn10");
            dataGridViewImageColumn10.Name = "dataGridViewImageColumn10";
            dataGridViewImageColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn11
            // 
            dataGridViewImageColumn11.DataPropertyName = "Bond color";
            resources.ApplyResources(dataGridViewImageColumn11, "dataGridViewImageColumn11");
            dataGridViewImageColumn11.Name = "dataGridViewImageColumn11";
            dataGridViewImageColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn12
            // 
            dataGridViewImageColumn12.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(dataGridViewImageColumn12, "dataGridViewImageColumn12");
            dataGridViewImageColumn12.Name = "dataGridViewImageColumn12";
            dataGridViewImageColumn12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            resources.ApplyResources(enabledDataGridViewCheckBoxColumn, "enabledDataGridViewCheckBoxColumn");
            enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            // 
            // centerDataGridViewTextBoxColumn
            // 
            centerDataGridViewTextBoxColumn.DataPropertyName = "Center";
            resources.ApplyResources(centerDataGridViewTextBoxColumn, "centerDataGridViewTextBoxColumn");
            centerDataGridViewTextBoxColumn.Name = "centerDataGridViewTextBoxColumn";
            centerDataGridViewTextBoxColumn.ReadOnly = true;
            centerDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // vertexDataGridViewTextBoxColumn
            // 
            vertexDataGridViewTextBoxColumn.DataPropertyName = "Vertex";
            resources.ApplyResources(vertexDataGridViewTextBoxColumn, "vertexDataGridViewTextBoxColumn");
            vertexDataGridViewTextBoxColumn.Name = "vertexDataGridViewTextBoxColumn";
            vertexDataGridViewTextBoxColumn.ReadOnly = true;
            vertexDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // minLenDataGridViewTextBoxColumn
            // 
            minLenDataGridViewTextBoxColumn.DataPropertyName = "Min len.";
            resources.ApplyResources(minLenDataGridViewTextBoxColumn, "minLenDataGridViewTextBoxColumn");
            minLenDataGridViewTextBoxColumn.Name = "minLenDataGridViewTextBoxColumn";
            minLenDataGridViewTextBoxColumn.ReadOnly = true;
            minLenDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // maxLenDataGridViewTextBoxColumn
            // 
            maxLenDataGridViewTextBoxColumn.DataPropertyName = "Max len.";
            resources.ApplyResources(maxLenDataGridViewTextBoxColumn, "maxLenDataGridViewTextBoxColumn");
            maxLenDataGridViewTextBoxColumn.Name = "maxLenDataGridViewTextBoxColumn";
            maxLenDataGridViewTextBoxColumn.ReadOnly = true;
            maxLenDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Enabled
            // 
            Enabled.DataPropertyName = "Enabled";
            resources.ApplyResources(Enabled, "Enabled");
            Enabled.Name = "Enabled";
            // 
            // Center
            // 
            Center.DataPropertyName = "Center";
            resources.ApplyResources(Center, "Center");
            Center.Name = "Center";
            Center.ReadOnly = true;
            // 
            // Vertex
            // 
            Vertex.DataPropertyName = "Vertex";
            resources.ApplyResources(Vertex, "Vertex");
            Vertex.Name = "Vertex";
            Vertex.ReadOnly = true;
            // 
            // BondInputControl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(dataGridView);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "BondInputControl";
            groupBoxPolyhedron.ResumeLayout(false);
            groupBoxPolyhedron.PerformLayout();
            groupBoxEdge.ResumeLayout(false);
            groupBoxBonds.ResumeLayout(false);
            groupBoxBonds.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);

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
        private ColorControl colorControlPolyhedron;
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
        private System.Windows.Forms.GroupBox groupBoxBonds;
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
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn11;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn12;
        private System.Windows.Forms.CheckBox checkBoxShowBonds;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn centerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vertexDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minLenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxLenDataGridViewTextBoxColumn;
        new private System.Windows.Forms.DataGridViewCheckBoxColumn Enabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn Center;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vertex;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn centerDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn vertexDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn minLenDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxLenDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn showBondsDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn showPolyhedronDataGridViewCheckBoxColumn;
    }
}
