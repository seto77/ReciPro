namespace Crystallography.Controls
{
    partial class LatticePlaneControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LatticePlaneControl));
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            panel1 = new System.Windows.Forms.Panel();
            buttonAddBond = new System.Windows.Forms.Button();
            buttonChangeBond = new System.Windows.Forms.Button();
            buttonDeleteBond = new System.Windows.Forms.Button();
            // dataGridView = new System.Windows.Forms.DataGridView(); // 260518Cl 旧実装: DPI変更時に列幅が追従しない
            dataGridView = new DpiAwareDataGridView(); // 260518Cl
            enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            kDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            iDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Translation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
            bindingSource = new System.Windows.Forms.BindingSource(components);
            dataSet = new DataSet();
            panel2 = new System.Windows.Forms.Panel();
            numericBoxDistance = new NumericBox();
            colorControl = new ColorControl();
            label6 = new System.Windows.Forms.Label();
            indexControl = new IndexControl();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Controls.Add(buttonAddBond);
            panel1.Controls.Add(buttonChangeBond);
            panel1.Controls.Add(buttonDeleteBond);
            panel1.Name = "panel1";
            // 
            // buttonAddBond
            // 
            resources.ApplyResources(buttonAddBond, "buttonAddBond");
            toolTip.SetToolTip(buttonAddBond, resources.GetString("buttonAddBond.ToolTip")); // 260531Cl
            buttonAddBond.BackColor = System.Drawing.Color.SteelBlue;
            buttonAddBond.ForeColor = System.Drawing.Color.White;
            buttonAddBond.Name = "buttonAddBond";
            buttonAddBond.UseVisualStyleBackColor = false;
            buttonAddBond.Click += buttonAdd_Click;
            // 
            // buttonChangeBond
            // 
            resources.ApplyResources(buttonChangeBond, "buttonChangeBond");
            toolTip.SetToolTip(buttonChangeBond, resources.GetString("buttonChangeBond.ToolTip")); // 260531Cl
            buttonChangeBond.BackColor = System.Drawing.Color.SteelBlue;
            buttonChangeBond.ForeColor = System.Drawing.Color.White;
            buttonChangeBond.Name = "buttonChangeBond";
            buttonChangeBond.UseVisualStyleBackColor = false;
            buttonChangeBond.Click += buttonChange_Click;
            // 
            // buttonDeleteBond
            // 
            resources.ApplyResources(buttonDeleteBond, "buttonDeleteBond");
            toolTip.SetToolTip(buttonDeleteBond, resources.GetString("buttonDeleteBond.ToolTip")); // 260531Cl
            buttonDeleteBond.BackColor = System.Drawing.Color.IndianRed;
            buttonDeleteBond.ForeColor = System.Drawing.Color.White;
            buttonDeleteBond.Name = "buttonDeleteBond";
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
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { enabledDataGridViewCheckBoxColumn, hDataGridViewTextBoxColumn, kDataGridViewTextBoxColumn, iDataGridViewTextBoxColumn, lDataGridViewTextBoxColumn, Translation, colorDataGridViewTextBoxColumn });
            dataGridView.DataSource = bindingSource;
            resources.ApplyResources(dataGridView, "dataGridView");
            toolTip.SetToolTip(dataGridView, resources.GetString("dataGridView.ToolTip")); // 260531Cl
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView.CellFormatting += dataGridView_CellFormatting;
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
            // iDataGridViewTextBoxColumn
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
            // Translation
            // 
            Translation.DataPropertyName = "Translation";
            resources.ApplyResources(Translation, "Translation");
            Translation.Name = "Translation";
            Translation.ReadOnly = true;
            Translation.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colorDataGridViewTextBoxColumn
            // 
            colorDataGridViewTextBoxColumn.DataPropertyName = "Color";
            resources.ApplyResources(colorDataGridViewTextBoxColumn, "colorDataGridViewTextBoxColumn");
            colorDataGridViewTextBoxColumn.Name = "colorDataGridViewTextBoxColumn";
            colorDataGridViewTextBoxColumn.ReadOnly = true;
            colorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // bindingSource
            // 
            bindingSource.DataMember = "DataTableLatticePlane";
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
            // panel2
            // 
            panel2.Controls.Add(indexControl);
            panel2.Controls.Add(numericBoxDistance);
            panel2.Controls.Add(colorControl);
            panel2.Controls.Add(label6);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // numericBoxDistance
            // 
            resources.ApplyResources(numericBoxDistance, "numericBoxDistance");
            toolTip.SetToolTip(numericBoxDistance, resources.GetString("numericBoxDistance.ToolTip")); // 260531Cl
            numericBoxDistance.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.Maximum = 0.5D;
            numericBoxDistance.Minimum = -0.5D;
            numericBoxDistance.Name = "numericBoxDistance";
            numericBoxDistance.ShowUpDown = true;
            numericBoxDistance.SkipEventDuringInput = false;
            numericBoxDistance.ValueFontSize = 9F;
            numericBoxDistance.ThousandsSeparator = true;
            numericBoxDistance.UpDown_Increment = 0.1D;
            // 
            // colorControl
            // 
            resources.ApplyResources(colorControl, "colorControl");
            colorControl.BackColor = System.Drawing.SystemColors.Control;
            colorControl.BoxSize = new System.Drawing.Size(20, 20);
            colorControl.Color = System.Drawing.Color.FromArgb(255, 192, 192);
            colorControl.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            colorControl.Name = "colorControl";
            toolTip.SetToolTip(colorControl, resources.GetString("colorControl.ToolTip")); // (260531Ch)
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip")); // 260531Cl
            label6.Name = "label6";
            // 
            // indexControl
            // 
            resources.ApplyResources(indexControl, "indexControl");
            toolTip.SetToolTip(indexControl, resources.GetString("indexControl.ToolTip")); // 260531Cl
            indexControl.BoxWidth = 42;
            indexControl.Bracket = IndexControl.BracketEnum.Round;
            indexControl.Mode = IndexControl.ModeEnum.Plane;
            indexControl.Name = "indexControl";
            indexControl.SubScript = "";
            indexControl.UpDownWidth = 17;
            indexControl.Values = ((int, int, int))resources.GetObject("indexControl.Values");
            // 
            // LatticePlaneControl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(dataGridView);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Name = "LatticePlaneControl";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)
        private ColorControl colorControl;
        private NumericBox numericBoxDistance;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddBond;
        private System.Windows.Forms.Button buttonChangeBond;
        private System.Windows.Forms.Button buttonDeleteBond;
        // private System.Windows.Forms.DataGridView dataGridView; // 260518Cl 旧実装
        private DpiAwareDataGridView dataGridView; // 260518Cl
        private System.Windows.Forms.BindingSource bindingSource;
        private DataSet dataSet;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Translation;
        private System.Windows.Forms.DataGridViewImageColumn colorDataGridViewTextBoxColumn;
        private IndexControl indexControl;
    }
}
