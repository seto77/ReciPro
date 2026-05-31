namespace Crystallography.Controls
{
    partial class BoundControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoundControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            // dataGridView = new System.Windows.Forms.DataGridView(); // 260518Cl 旧実装: DPI変更時に列幅が追従しない
            dataGridView = new DpiAwareDataGridView(); // 260518Cl
            enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            kDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            iDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn(); // 260517Cl 追加: Miller-Bravais i 列
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
            toolTip = new System.Windows.Forms.ToolTip(components);
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            checkBoxEquivalency = new System.Windows.Forms.CheckBox();
            numericBoxTranslation = new NumericBox();
            label6 = new System.Windows.Forms.Label();
            colorControl = new ColorControl();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxDistanceD = new NumericBox();
            numericBoxDistance = new NumericBox();
            panel2 = new System.Windows.Forms.Panel();
            label3 = new System.Windows.Forms.Label();
            indexControl = new IndexControl();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
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
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(dataGridView, "dataGridView");
            toolTip.SetToolTip(dataGridView, resources.GetString("dataGridView.ToolTip")); // 260531Cl
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // 260517Cl iDataGridViewTextBoxColumn を k と l の間に挿入 (LatticePlaneControl と同じ並び)
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { enabledDataGridViewCheckBoxColumn, hDataGridViewTextBoxColumn, kDataGridViewTextBoxColumn, iDataGridViewTextBoxColumn, lDataGridViewTextBoxColumn, equivalencyDataGridViewCheckBoxColumn, MultipleOfD, distanceDataGridViewTextBoxColumn, colorDataGridViewTextBoxColumn });
            dataGridView.DataSource = bindingSource;
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView.CellFormatting += dataGridView_CellFormatting; // 260517Cl 追加: i = -(h+k) を CellFormatting で生成
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
            // iDataGridViewTextBoxColumn  260517Cl 追加 (Miller-Bravais 表示専用、DataPropertyName なし。値は CellFormatting で生成)
            //
            resources.ApplyResources(iDataGridViewTextBoxColumn, "iDataGridViewTextBoxColumn");
            iDataGridViewTextBoxColumn.Name = "iDataGridViewTextBoxColumn";
            iDataGridViewTextBoxColumn.ReadOnly = true;
            iDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            panel1.Controls.Add(buttonAddBond);
            panel1.Controls.Add(numericBoxMaximumDistanceFromOrigin);
            panel1.Controls.Add(buttonChangeBond);
            panel1.Controls.Add(buttonDeleteBond);
            panel1.Controls.Add(checkBoxImmediateUpdate);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // buttonAddBond
            // 
            buttonAddBond.BackColor = System.Drawing.Color.SteelBlue;
            resources.ApplyResources(buttonAddBond, "buttonAddBond");
            toolTip.SetToolTip(buttonAddBond, resources.GetString("buttonAddBond.ToolTip")); // 260531Cl
            buttonAddBond.ForeColor = System.Drawing.Color.White;
            buttonAddBond.Name = "buttonAddBond";
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
            numericBoxMaximumDistanceFromOrigin.ShowUpDown = true;
            numericBoxMaximumDistanceFromOrigin.SkipEventDuringInput = false;
            numericBoxMaximumDistanceFromOrigin.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxMaximumDistanceFromOrigin, resources.GetString("numericBoxMaximumDistanceFromOrigin.ToolTip1"));
            numericBoxMaximumDistanceFromOrigin.Value = 15D;
            numericBoxMaximumDistanceFromOrigin.ValueChanged += numericBoxMaximumDistanceFromOrigin_ValueChanged;
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
            // checkBoxImmediateUpdate
            // 
            resources.ApplyResources(checkBoxImmediateUpdate, "checkBoxImmediateUpdate");
            checkBoxImmediateUpdate.Name = "checkBoxImmediateUpdate";
            toolTip.SetToolTip(checkBoxImmediateUpdate, resources.GetString("checkBoxImmediateUpdate.ToolTip"));
            checkBoxImmediateUpdate.UseVisualStyleBackColor = true;
            checkBoxImmediateUpdate.CheckedChanged += checkBoxEquivalency_CheckedChanged;
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
            numericBoxTranslation.ShowUpDown = true;
            numericBoxTranslation.SkipEventDuringInput = false;
            numericBoxTranslation.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxTranslation, resources.GetString("numericBoxTranslation.ToolTip1"));
            numericBoxTranslation.UpDown_Increment = 0.1D;
            numericBoxTranslation.ValueChanged += numericBoxDistanceD_ValueChanged;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip")); // 260531Cl
            label6.Name = "label6";
            // 
            // colorControl
            // 
            colorControl.Argb = -16728064;
            resources.ApplyResources(colorControl, "colorControl");
            toolTip.SetToolTip(colorControl, resources.GetString("colorControl.ToolTip")); // 260531Cl
            colorControl.BackColor = System.Drawing.SystemColors.Control;
            colorControl.Blue = 0;
            colorControl.BlueF = 0F;
            colorControl.Color = System.Drawing.Color.FromArgb(0, 192, 0);
            colorControl.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            colorControl.Green = 192;
            colorControl.GreenF = 0.7529412F;
            colorControl.Name = "colorControl";
            colorControl.Red = 0;
            colorControl.RedF = 0F;
            colorControl.ColorChanged += colorControl_ColorChanged;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(numericBoxDistanceD);
            flowLayoutPanel1.Controls.Add(numericBoxDistance);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // numericBoxDistanceD
            // 
            resources.ApplyResources(numericBoxDistanceD, "numericBoxDistanceD");
            toolTip.SetToolTip(numericBoxDistanceD, resources.GetString("numericBoxDistanceD.ToolTip")); // 260531Cl
            numericBoxDistanceD.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDistanceD.DecimalPlaces = 3;
            numericBoxDistanceD.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistanceD.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistanceD.Maximum = 20D;
            numericBoxDistanceD.Minimum = -20D;
            numericBoxDistanceD.Name = "numericBoxDistanceD";
            numericBoxDistanceD.RadianValue = 0.017453292519943295D;
            numericBoxDistanceD.ShowUpDown = true;
            numericBoxDistanceD.SkipEventDuringInput = false;
            numericBoxDistanceD.ThousandsSeparator = true;
            numericBoxDistanceD.UpDown_Increment = 0.1D;
            numericBoxDistanceD.Value = 1D;
            numericBoxDistanceD.ValueChanged += numericBoxDistanceD_ValueChanged;
            // 
            // numericBoxDistance
            // 
            resources.ApplyResources(numericBoxDistance, "numericBoxDistance");
            toolTip.SetToolTip(numericBoxDistance, resources.GetString("numericBoxDistance.ToolTip")); // 260531Cl
            numericBoxDistance.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.DecimalPlaces = 3;
            numericBoxDistance.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.Maximum = 100D;
            numericBoxDistance.Minimum = -100D;
            numericBoxDistance.Name = "numericBoxDistance";
            numericBoxDistance.SkipEventDuringInput = false;
            numericBoxDistance.ThousandsSeparator = true;
            numericBoxDistance.ValueChanged += numericBoxDistance_ValueChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(indexControl);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(numericBoxTranslation);
            panel2.Controls.Add(flowLayoutPanel1);
            panel2.Controls.Add(colorControl);
            panel2.Controls.Add(checkBoxEquivalency);
            panel2.Controls.Add(label3);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip")); // 260531Cl
            label3.Name = "label3";
            // 
            // indexControl1
            // 
            resources.ApplyResources(indexControl, "indexControl1");
            toolTip.SetToolTip(indexControl, resources.GetString("indexControl1.ToolTip")); // 260531Cl
            indexControl.BoxWidth = 38;
            indexControl.Bracket = IndexControl.BracketEnum.Round;
            indexControl.Mode = IndexControl.ModeEnum.Plane;
            indexControl.Name = "indexControl1";
            indexControl.SubScript = "";
            indexControl.UpDownWidth = 17;
            indexControl.Values = ((int, int, int))resources.GetObject("indexControl1.Values");
            // 
            // BoundControl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(dataGridView);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "BoundControl";
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
        // private System.Windows.Forms.DataGridView dataGridView; // 260518Cl 旧実装
        private DpiAwareDataGridView dataGridView; // 260518Cl
        private System.Windows.Forms.BindingSource bindingSource;
        private DataSet dataSet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddBond;
        private System.Windows.Forms.Button buttonChangeBond;
        private System.Windows.Forms.Button buttonDeleteBond;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDataGridViewTextBoxColumn; // 260517Cl 追加
        private System.Windows.Forms.DataGridViewTextBoxColumn lDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn equivalencyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MultipleOfD;
        private System.Windows.Forms.DataGridViewTextBoxColumn distanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn colorDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxEquivalency;
        private ColorControl colorControl;
        private NumericBox numericBoxMaximumDistanceFromOrigin;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private NumericBox numericBoxDistanceD;
        private NumericBox numericBoxDistance;
        private System.Windows.Forms.Panel panel2;
        private NumericBox numericBoxTranslation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxImmediateUpdate;
        private IndexControl indexControl;
    }
}
