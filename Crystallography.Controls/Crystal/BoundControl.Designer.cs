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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoundControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridView = new System.Windows.Forms.DataGridView();
            enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            kDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            equivalencyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            MultipleOfD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            distanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
            bindingSource = new System.Windows.Forms.BindingSource(components);
            dataSet = new DataSet();
            panel1 = new System.Windows.Forms.Panel();
            buttonAddBond = new System.Windows.Forms.Button();
            numericBoxMaximumDistanceFromOrigin = new NumericBox();
            buttonChangeBond = new System.Windows.Forms.Button();
            buttonDeleteBond = new System.Windows.Forms.Button();
            checkBoxImmediateUpdate = new System.Windows.Forms.CheckBox();
            dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn6 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn7 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn8 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn9 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn10 = new System.Windows.Forms.DataGridViewImageColumn();
            toolTip = new System.Windows.Forms.ToolTip(components);
            label6 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            numericBoxH = new NumericBox();
            checkBoxEquivalency = new System.Windows.Forms.CheckBox();
            numericBoxK = new NumericBox();
            numericBoxL = new NumericBox();
            colorControl = new ColorControl();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxDistanceD = new NumericBox();
            numericBoxDistance = new NumericBox();
            panel2 = new System.Windows.Forms.Panel();
            numericBoxTranslation = new NumericBox();
            label3 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
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
            // dataGridView
            // 
            resources.ApplyResources(dataGridView, "dataGridView");
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
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { enabledDataGridViewCheckBoxColumn, hDataGridViewTextBoxColumn, kDataGridViewTextBoxColumn, lDataGridViewTextBoxColumn, equivalencyDataGridViewCheckBoxColumn, MultipleOfD, distanceDataGridViewTextBoxColumn, colorDataGridViewTextBoxColumn });
            dataGridView.DataSource = bindingSource;
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("メイリオ", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowTemplate.Height = 21;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            toolTip.SetToolTip(dataGridView, resources.GetString("dataGridView.ToolTip"));
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;
            dataGridView.CurrentCellDirtyStateChanged += dataGridView_CurrentCellDirtyStateChanged;
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            resources.ApplyResources(enabledDataGridViewCheckBoxColumn, "enabledDataGridViewCheckBoxColumn");
            enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            // 
            // hDataGridViewTextBoxColumn
            // 
            hDataGridViewTextBoxColumn.DataPropertyName = "h";
            resources.ApplyResources(hDataGridViewTextBoxColumn, "hDataGridViewTextBoxColumn");
            hDataGridViewTextBoxColumn.Name = "hDataGridViewTextBoxColumn";
            hDataGridViewTextBoxColumn.ReadOnly = true;
            hDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // kDataGridViewTextBoxColumn
            // 
            kDataGridViewTextBoxColumn.DataPropertyName = "k";
            resources.ApplyResources(kDataGridViewTextBoxColumn, "kDataGridViewTextBoxColumn");
            kDataGridViewTextBoxColumn.Name = "kDataGridViewTextBoxColumn";
            kDataGridViewTextBoxColumn.ReadOnly = true;
            kDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lDataGridViewTextBoxColumn
            // 
            lDataGridViewTextBoxColumn.DataPropertyName = "l";
            resources.ApplyResources(lDataGridViewTextBoxColumn, "lDataGridViewTextBoxColumn");
            lDataGridViewTextBoxColumn.Name = "lDataGridViewTextBoxColumn";
            lDataGridViewTextBoxColumn.ReadOnly = true;
            lDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // equivalencyDataGridViewCheckBoxColumn
            // 
            equivalencyDataGridViewCheckBoxColumn.DataPropertyName = "Equivalency";
            resources.ApplyResources(equivalencyDataGridViewCheckBoxColumn, "equivalencyDataGridViewCheckBoxColumn");
            equivalencyDataGridViewCheckBoxColumn.Name = "equivalencyDataGridViewCheckBoxColumn";
            equivalencyDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // MultipleOfD
            // 
            MultipleOfD.DataPropertyName = "MultipleOfD";
            resources.ApplyResources(MultipleOfD, "MultipleOfD");
            MultipleOfD.Name = "MultipleOfD";
            MultipleOfD.ReadOnly = true;
            MultipleOfD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // distanceDataGridViewTextBoxColumn
            // 
            distanceDataGridViewTextBoxColumn.DataPropertyName = "Distance";
            resources.ApplyResources(distanceDataGridViewTextBoxColumn, "distanceDataGridViewTextBoxColumn");
            distanceDataGridViewTextBoxColumn.Name = "distanceDataGridViewTextBoxColumn";
            distanceDataGridViewTextBoxColumn.ReadOnly = true;
            distanceDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colorDataGridViewTextBoxColumn
            // 
            colorDataGridViewTextBoxColumn.DataPropertyName = "Color";
            resources.ApplyResources(colorDataGridViewTextBoxColumn, "colorDataGridViewTextBoxColumn");
            colorDataGridViewTextBoxColumn.Name = "colorDataGridViewTextBoxColumn";
            colorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // bindingSource
            // 
            bindingSource.DataMember = "DataTableBound";
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
            resources.ApplyResources(panel1, "panel1");
            panel1.Controls.Add(buttonAddBond);
            panel1.Controls.Add(numericBoxMaximumDistanceFromOrigin);
            panel1.Controls.Add(buttonChangeBond);
            panel1.Controls.Add(buttonDeleteBond);
            panel1.Controls.Add(checkBoxImmediateUpdate);
            panel1.Name = "panel1";
            toolTip.SetToolTip(panel1, resources.GetString("panel1.ToolTip"));
            // 
            // buttonAddBond
            // 
            resources.ApplyResources(buttonAddBond, "buttonAddBond");
            buttonAddBond.BackColor = System.Drawing.Color.SteelBlue;
            buttonAddBond.ForeColor = System.Drawing.Color.White;
            buttonAddBond.Name = "buttonAddBond";
            toolTip.SetToolTip(buttonAddBond, resources.GetString("buttonAddBond.ToolTip"));
            buttonAddBond.UseVisualStyleBackColor = false;
            buttonAddBond.Click += buttonAdd_Click;
            // 
            // numericBoxMaximumDistanceFromOrigin
            // 
            resources.ApplyResources(numericBoxMaximumDistanceFromOrigin, "numericBoxMaximumDistanceFromOrigin");
            numericBoxMaximumDistanceFromOrigin.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMaximumDistanceFromOrigin.DecimalPlaces = 3;
            numericBoxMaximumDistanceFromOrigin.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxMaximumDistanceFromOrigin.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxMaximumDistanceFromOrigin.Maximum = 100D;
            numericBoxMaximumDistanceFromOrigin.Minimum = -100D;
            numericBoxMaximumDistanceFromOrigin.Name = "numericBoxMaximumDistanceFromOrigin";
            numericBoxMaximumDistanceFromOrigin.RadianValue = 0.26179938779914941D;
            numericBoxMaximumDistanceFromOrigin.RoundErrorAccuracy = -1;
            numericBoxMaximumDistanceFromOrigin.ShowUpDown = true;
            numericBoxMaximumDistanceFromOrigin.SkipEventDuringInput = false;
            numericBoxMaximumDistanceFromOrigin.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxMaximumDistanceFromOrigin, resources.GetString("numericBoxMaximumDistanceFromOrigin.ToolTip"));
            numericBoxMaximumDistanceFromOrigin.Value = 15D;
            numericBoxMaximumDistanceFromOrigin.ValueChanged += numericBoxMaximumDistanceFromOrigin_ValueChanged;
            // 
            // buttonChangeBond
            // 
            resources.ApplyResources(buttonChangeBond, "buttonChangeBond");
            buttonChangeBond.BackColor = System.Drawing.Color.SteelBlue;
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
            // checkBoxImmediateUpdate
            // 
            resources.ApplyResources(checkBoxImmediateUpdate, "checkBoxImmediateUpdate");
            checkBoxImmediateUpdate.Name = "checkBoxImmediateUpdate";
            toolTip.SetToolTip(checkBoxImmediateUpdate, resources.GetString("checkBoxImmediateUpdate.ToolTip"));
            checkBoxImmediateUpdate.UseVisualStyleBackColor = true;
            checkBoxImmediateUpdate.CheckedChanged += checkBoxEquivalency_CheckedChanged;
            // 
            // dataGridViewImageColumn3
            // 
            dataGridViewImageColumn3.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn3, "dataGridViewImageColumn3");
            dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn4
            // 
            dataGridViewImageColumn4.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn4, "dataGridViewImageColumn4");
            dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            dataGridViewImageColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn5
            // 
            dataGridViewImageColumn5.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn5, "dataGridViewImageColumn5");
            dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            dataGridViewImageColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn6
            // 
            dataGridViewImageColumn6.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn6, "dataGridViewImageColumn6");
            dataGridViewImageColumn6.Name = "dataGridViewImageColumn6";
            dataGridViewImageColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn7
            // 
            dataGridViewImageColumn7.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn7, "dataGridViewImageColumn7");
            dataGridViewImageColumn7.Name = "dataGridViewImageColumn7";
            dataGridViewImageColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn8
            // 
            dataGridViewImageColumn8.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn8, "dataGridViewImageColumn8");
            dataGridViewImageColumn8.Name = "dataGridViewImageColumn8";
            dataGridViewImageColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn9
            // 
            dataGridViewImageColumn9.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn9, "dataGridViewImageColumn9");
            dataGridViewImageColumn9.Name = "dataGridViewImageColumn9";
            dataGridViewImageColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn10
            // 
            dataGridViewImageColumn10.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn10, "dataGridViewImageColumn10");
            dataGridViewImageColumn10.Name = "dataGridViewImageColumn10";
            dataGridViewImageColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // numericBoxH
            // 
            resources.ApplyResources(numericBoxH, "numericBoxH");
            numericBoxH.BackColor = System.Drawing.SystemColors.Control;
            numericBoxH.DecimalPlaces = 0;
            numericBoxH.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxH.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxH.Maximum = 10D;
            numericBoxH.Minimum = -10D;
            numericBoxH.Name = "numericBoxH";
            numericBoxH.RoundErrorAccuracy = -1;
            numericBoxH.ShowUpDown = true;
            numericBoxH.SkipEventDuringInput = false;
            numericBoxH.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxH, resources.GetString("numericBoxH.ToolTip"));
            numericBoxH.ValueChanged += numericBoxDistanceD_ValueChanged;
            // 
            // checkBoxEquivalency
            // 
            resources.ApplyResources(checkBoxEquivalency, "checkBoxEquivalency");
            checkBoxEquivalency.Checked = true;
            checkBoxEquivalency.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxEquivalency.Name = "checkBoxEquivalency";
            toolTip.SetToolTip(checkBoxEquivalency, resources.GetString("checkBoxEquivalency.ToolTip"));
            checkBoxEquivalency.UseVisualStyleBackColor = true;
            checkBoxEquivalency.CheckedChanged += checkBoxEquivalency_CheckedChanged;
            // 
            // numericBoxK
            // 
            resources.ApplyResources(numericBoxK, "numericBoxK");
            numericBoxK.BackColor = System.Drawing.SystemColors.Control;
            numericBoxK.DecimalPlaces = 0;
            numericBoxK.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxK.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxK.Maximum = 10D;
            numericBoxK.Minimum = -10D;
            numericBoxK.Name = "numericBoxK";
            numericBoxK.RoundErrorAccuracy = -1;
            numericBoxK.ShowUpDown = true;
            numericBoxK.SkipEventDuringInput = false;
            numericBoxK.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxK, resources.GetString("numericBoxK.ToolTip"));
            numericBoxK.ValueChanged += numericBoxDistanceD_ValueChanged;
            // 
            // numericBoxL
            // 
            resources.ApplyResources(numericBoxL, "numericBoxL");
            numericBoxL.BackColor = System.Drawing.SystemColors.Control;
            numericBoxL.DecimalPlaces = 0;
            numericBoxL.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxL.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxL.Maximum = 10D;
            numericBoxL.Minimum = -10D;
            numericBoxL.Name = "numericBoxL";
            numericBoxL.RoundErrorAccuracy = -1;
            numericBoxL.ShowUpDown = true;
            numericBoxL.SkipEventDuringInput = false;
            numericBoxL.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxL, resources.GetString("numericBoxL.ToolTip"));
            numericBoxL.ValueChanged += numericBoxDistanceD_ValueChanged;
            // 
            // colorControl
            // 
            resources.ApplyResources(colorControl, "colorControl");
            colorControl.Argb = -16728064;
            colorControl.Blue = 0;
            colorControl.BlueF = 0F;
            colorControl.BoxSize = new System.Drawing.Size(24, 24);
            colorControl.Color = System.Drawing.Color.FromArgb(0, 192, 0);
            colorControl.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            colorControl.Green = 192;
            colorControl.GreenF = 0.7529412F;
            colorControl.Name = "colorControl";
            colorControl.Red = 0;
            colorControl.RedF = 0F;
            toolTip.SetToolTip(colorControl, resources.GetString("colorControl.ToolTip"));
            colorControl.ColorChanged += colorControl_ColorChanged;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(numericBoxDistanceD);
            flowLayoutPanel1.Controls.Add(numericBoxDistance);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            toolTip.SetToolTip(flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
            // 
            // numericBoxDistanceD
            // 
            resources.ApplyResources(numericBoxDistanceD, "numericBoxDistanceD");
            numericBoxDistanceD.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDistanceD.DecimalPlaces = 3;
            numericBoxDistanceD.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistanceD.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistanceD.Maximum = 20D;
            numericBoxDistanceD.Minimum = -20D;
            numericBoxDistanceD.Name = "numericBoxDistanceD";
            numericBoxDistanceD.RadianValue = 0.017453292519943295D;
            numericBoxDistanceD.RoundErrorAccuracy = -1;
            numericBoxDistanceD.ShowUpDown = true;
            numericBoxDistanceD.SkipEventDuringInput = false;
            numericBoxDistanceD.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxDistanceD, resources.GetString("numericBoxDistanceD.ToolTip1"));
            numericBoxDistanceD.UpDown_Increment = 0.1D;
            numericBoxDistanceD.Value = 1D;
            numericBoxDistanceD.ValueChanged += numericBoxDistanceD_ValueChanged;
            // 
            // numericBoxDistance
            // 
            resources.ApplyResources(numericBoxDistance, "numericBoxDistance");
            numericBoxDistance.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.DecimalPlaces = 3;
            numericBoxDistance.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.Maximum = 100D;
            numericBoxDistance.Minimum = -100D;
            numericBoxDistance.Name = "numericBoxDistance";
            numericBoxDistance.RoundErrorAccuracy = -1;
            numericBoxDistance.SkipEventDuringInput = false;
            numericBoxDistance.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxDistance, resources.GetString("numericBoxDistance.ToolTip1"));
            numericBoxDistance.ValueChanged += numericBoxDistance_ValueChanged;
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Controls.Add(numericBoxTranslation);
            panel2.Controls.Add(flowLayoutPanel1);
            panel2.Controls.Add(colorControl);
            panel2.Controls.Add(numericBoxL);
            panel2.Controls.Add(numericBoxK);
            panel2.Controls.Add(checkBoxEquivalency);
            panel2.Controls.Add(numericBoxH);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label6);
            panel2.Name = "panel2";
            toolTip.SetToolTip(panel2, resources.GetString("panel2.ToolTip"));
            // 
            // numericBoxTranslation
            // 
            resources.ApplyResources(numericBoxTranslation, "numericBoxTranslation");
            numericBoxTranslation.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTranslation.DecimalPlaces = 3;
            numericBoxTranslation.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTranslation.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTranslation.Maximum = 100D;
            numericBoxTranslation.Minimum = -100D;
            numericBoxTranslation.Name = "numericBoxTranslation";
            numericBoxTranslation.RoundErrorAccuracy = -1;
            numericBoxTranslation.ShowUpDown = true;
            numericBoxTranslation.SkipEventDuringInput = false;
            numericBoxTranslation.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxTranslation, resources.GetString("numericBoxTranslation.ToolTip"));
            numericBoxTranslation.UpDown_Increment = 0.1D;
            numericBoxTranslation.ValueChanged += numericBoxDistanceD_ValueChanged;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // BoundControl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(dataGridView);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "BoundControl";
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource bindingSource;
        private DataSet dataSet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddBond;
        private System.Windows.Forms.Button buttonChangeBond;
        private System.Windows.Forms.Button buttonDeleteBond;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn4;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn6;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn7;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn8;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn9;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn10;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn equivalencyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MultipleOfD;
        private System.Windows.Forms.DataGridViewTextBoxColumn distanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn colorDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private NumericBox numericBoxH;
        private System.Windows.Forms.CheckBox checkBoxEquivalency;
        private NumericBox numericBoxK;
        private NumericBox numericBoxL;
        private ColorControl colorControl;
        private NumericBox numericBoxMaximumDistanceFromOrigin;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private NumericBox numericBoxDistanceD;
        private NumericBox numericBoxDistance;
        private System.Windows.Forms.Panel panel2;
        private NumericBox numericBoxTranslation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxImmediateUpdate;
    }
}
