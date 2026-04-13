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
            dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            panel1 = new System.Windows.Forms.Panel();
            buttonAddBond = new System.Windows.Forms.Button();
            buttonChangeBond = new System.Windows.Forms.Button();
            buttonDeleteBond = new System.Windows.Forms.Button();
            dataGridView = new System.Windows.Forms.DataGridView();
            enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            hDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            kDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Translation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
            bindingSource = new System.Windows.Forms.BindingSource(components);
            dataSet = new DataSet();
            panel2 = new System.Windows.Forms.Panel();
            numericBoxDistance = new NumericBox();
            numericBoxK = new NumericBox();
            label5 = new System.Windows.Forms.Label();
            numericBoxL = new NumericBox();
            label4 = new System.Windows.Forms.Label();
            colorControl = new ColorControl();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            numericBoxH = new NumericBox();
            label6 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn6 = new System.Windows.Forms.DataGridViewImageColumn();
            dataGridViewImageColumn7 = new System.Windows.Forms.DataGridViewImageColumn();
            toolTip = new System.Windows.Forms.ToolTip(components);
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridViewImageColumn3
            // 
            dataGridViewImageColumn3.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn3, "dataGridViewImageColumn3");
            dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            buttonAddBond.BackColor = System.Drawing.Color.SteelBlue;
            buttonAddBond.ForeColor = System.Drawing.Color.White;
            buttonAddBond.Name = "buttonAddBond";
            buttonAddBond.UseVisualStyleBackColor = false;
            buttonAddBond.Click += buttonAdd_Click;
            // 
            // buttonChangeBond
            // 
            resources.ApplyResources(buttonChangeBond, "buttonChangeBond");
            buttonChangeBond.BackColor = System.Drawing.Color.SteelBlue;
            buttonChangeBond.ForeColor = System.Drawing.Color.White;
            buttonChangeBond.Name = "buttonChangeBond";
            buttonChangeBond.UseVisualStyleBackColor = false;
            buttonChangeBond.Click += buttonChange_Click;
            // 
            // buttonDeleteBond
            // 
            resources.ApplyResources(buttonDeleteBond, "buttonDeleteBond");
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
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { enabledDataGridViewCheckBoxColumn, hDataGridViewTextBoxColumn, kDataGridViewTextBoxColumn, lDataGridViewTextBoxColumn, Translation, colorDataGridViewTextBoxColumn });
            dataGridView.DataSource = bindingSource;
            resources.ApplyResources(dataGridView, "dataGridView");
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
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
            panel2.Controls.Add(numericBoxDistance);
            panel2.Controls.Add(numericBoxK);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(numericBoxL);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(colorControl);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(numericBoxH);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label2);
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // numericBoxDistance
            // 
            resources.ApplyResources(numericBoxDistance, "numericBoxDistance");
            numericBoxDistance.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxDistance.Maximum = 0.5D;
            numericBoxDistance.Minimum = -0.5D;
            numericBoxDistance.Name = "numericBoxDistance";
            numericBoxDistance.ShowUpDown = true;
            numericBoxDistance.SkipEventDuringInput = false;
            numericBoxDistance.ThonsandsSeparator = true;
            numericBoxDistance.UpDown_Increment = 0.1D;
            // 
            // numericBoxK
            // 
            resources.ApplyResources(numericBoxK, "numericBoxK");
            numericBoxK.BackColor = System.Drawing.SystemColors.Control;
            numericBoxK.DecimalPlaces = 0;
            numericBoxK.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxK.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxK.Maximum = 24D;
            numericBoxK.Minimum = -24D;
            numericBoxK.Name = "numericBoxK";
            numericBoxK.ShowUpDown = true;
            numericBoxK.SkipEventDuringInput = false;
            numericBoxK.ThonsandsSeparator = true;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // numericBoxL
            // 
            resources.ApplyResources(numericBoxL, "numericBoxL");
            numericBoxL.BackColor = System.Drawing.SystemColors.Control;
            numericBoxL.DecimalPlaces = 0;
            numericBoxL.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxL.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxL.Maximum = 24D;
            numericBoxL.Minimum = -24D;
            numericBoxL.Name = "numericBoxL";
            numericBoxL.ShowUpDown = true;
            numericBoxL.SkipEventDuringInput = false;
            numericBoxL.ThonsandsSeparator = true;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // colorControl
            // 
            colorControl.Argb = -16192;
            resources.ApplyResources(colorControl, "colorControl");
            colorControl.BackColor = System.Drawing.SystemColors.Control;
            colorControl.Blue = 192;
            colorControl.BlueF = 0.7529412F;
            colorControl.BoxSize = new System.Drawing.Size(20, 20);
            colorControl.Color = System.Drawing.Color.FromArgb(255, 192, 192);
            colorControl.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            colorControl.Green = 192;
            colorControl.GreenF = 0.7529412F;
            colorControl.Name = "colorControl";
            colorControl.Red = 255;
            colorControl.RedF = 1F;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // numericBoxH
            // 
            resources.ApplyResources(numericBoxH, "numericBoxH");
            numericBoxH.BackColor = System.Drawing.SystemColors.Control;
            numericBoxH.DecimalPlaces = 0;
            numericBoxH.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxH.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxH.Maximum = 24D;
            numericBoxH.Minimum = -24D;
            numericBoxH.Name = "numericBoxH";
            numericBoxH.ShowUpDown = true;
            numericBoxH.SkipEventDuringInput = false;
            numericBoxH.ThonsandsSeparator = true;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
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
            // dataGridViewImageColumn4
            // 
            dataGridViewImageColumn4.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn4, "dataGridViewImageColumn4");
            dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            dataGridViewImageColumn4.ReadOnly = true;
            dataGridViewImageColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn5
            // 
            dataGridViewImageColumn5.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn5, "dataGridViewImageColumn5");
            dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            dataGridViewImageColumn5.ReadOnly = true;
            dataGridViewImageColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn6
            // 
            dataGridViewImageColumn6.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn6, "dataGridViewImageColumn6");
            dataGridViewImageColumn6.Name = "dataGridViewImageColumn6";
            dataGridViewImageColumn6.ReadOnly = true;
            dataGridViewImageColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn7
            // 
            dataGridViewImageColumn7.DataPropertyName = "Color";
            resources.ApplyResources(dataGridViewImageColumn7, "dataGridViewImageColumn7");
            dataGridViewImageColumn7.Name = "dataGridViewImageColumn7";
            dataGridViewImageColumn7.ReadOnly = true;
            dataGridViewImageColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
        private ColorControl colorControl;
        private NumericBox numericBoxDistance;
        private NumericBox numericBoxL;
        private NumericBox numericBoxK;
        private NumericBox numericBoxH;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddBond;
        private System.Windows.Forms.Button buttonChangeBond;
        private System.Windows.Forms.Button buttonDeleteBond;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource bindingSource;
        private DataSet dataSet;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn4;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Translation;
        private System.Windows.Forms.DataGridViewImageColumn colorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn6;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn7;
        private System.Windows.Forms.ToolTip toolTip;
    }
}
