namespace ReciPro
{
    partial class FormSpotIDv1Results
    {
        /// <summary>必要なデザイナ変数です。</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>使用中のリソースをすべてクリーンアップします。</summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            captureExtender.SetCapture(this, true); // 260521Cl 追加: GUI監査キャプチャ対象 (フォーム全体)
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSpotIDv1Results));
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            bindingSource2 = new System.Windows.Forms.BindingSource(components);
            dataSet = new System.Data.DataSet();
            dataTable1 = new System.Data.DataTable();
            dataColumnNumber = new System.Data.DataColumn();
            dataColumnZoneAxis = new System.Data.DataColumn();
            dataColumnG1 = new System.Data.DataColumn();
            dataColumnD1 = new System.Data.DataColumn();
            dataColumnG2 = new System.Data.DataColumn();
            dataColumnD2 = new System.Data.DataColumn();
            dataColumnG3 = new System.Data.DataColumn();
            dataColumnD3 = new System.Data.DataColumn();
            dataColumnTheta = new System.Data.DataColumn();
            dataColumn1 = new System.Data.DataColumn();
            dataTable2 = new System.Data.DataTable();
            dataColumnID = new System.Data.DataColumn();
            dataColumnPhoto1ZoneAxis = new System.Data.DataColumn();
            dataColumnPhoto2ZoneAxis = new System.Data.DataColumn();
            dataColumnPhoto3ZoneAxis = new System.Data.DataColumn();
            dataColumnAngleBet12 = new System.Data.DataColumn();
            dataColumnAngleBet23 = new System.Data.DataColumn();
            dataColumnAngleBet31 = new System.Data.DataColumn();
            dataColumn2 = new System.Data.DataColumn();
            label1 = new System.Windows.Forms.Label();
            // this.dataGridView1 = new System.Windows.Forms.DataGridView(); // 260518Cl 旧実装: DPI変更時に列幅が追従しない
            dataGridView1 = new Crystallography.Controls.DpiAwareDataGridView(); // 260518Cl
            bindingSource1 = new System.Windows.Forms.BindingSource(components);
            // this.dataGridView2 = new System.Windows.Forms.DataGridView(); // 260518Cl 旧実装
            dataGridView2 = new Crystallography.Controls.DpiAwareDataGridView(); // 260518Cl
            Phase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            zoneAxisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            g1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            d1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            g2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            d2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            g3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            d3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            θDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            photo1ZoneAxisDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            photo2ZoneAxisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            photo3ZoneAxisDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            angleBetweenPhoto12DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            angleBetweenPhoto23DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            angleBetweenPhoto31DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(bindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridView2)).BeginInit();
            SuspendLayout();
            // 
            // bindingSource2
            // 
            bindingSource2.DataMember = "Table2";
            bindingSource2.DataSource = dataSet;
            // 
            // dataSet
            // 
            dataSet.DataSetName = "NewDataSet";
            dataSet.Tables.AddRange(new System.Data.DataTable[] {
            dataTable1,
            dataTable2});
            // 
            // dataTable1
            // 
            dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            dataColumnNumber,
            dataColumnZoneAxis,
            dataColumnG1,
            dataColumnD1,
            dataColumnG2,
            dataColumnD2,
            dataColumnG3,
            dataColumnD3,
            dataColumnTheta,
            dataColumn1});
            dataTable1.TableName = "Table1";
            // 
            // dataColumnNumber
            // 
            dataColumnNumber.ColumnName = "Number";
            dataColumnNumber.DataType = typeof(int);
            // 
            // dataColumnZoneAxis
            // 
            dataColumnZoneAxis.ColumnName = "Zone Axis";
            // 
            // dataColumnG1
            // 
            dataColumnG1.ColumnName = "g1";
            // 
            // dataColumnD1
            // 
            dataColumnD1.ColumnName = "d1";
            // 
            // dataColumnG2
            // 
            dataColumnG2.Caption = "g2";
            dataColumnG2.ColumnName = "g2";
            // 
            // dataColumnD2
            // 
            dataColumnD2.ColumnName = "d2";
            // 
            // dataColumnG3
            // 
            dataColumnG3.Caption = "g3";
            dataColumnG3.ColumnName = "g3";
            // 
            // dataColumnD3
            // 
            dataColumnD3.ColumnName = "d3";
            // 
            // dataColumnTheta
            // 
            dataColumnTheta.ColumnName = "θ";
            // 
            // dataColumn1
            // 
            dataColumn1.Caption = "Phase";
            dataColumn1.ColumnName = "Phase";
            // 
            // dataTable2
            // 
            dataTable2.Columns.AddRange(new System.Data.DataColumn[] {
            dataColumnID,
            dataColumnPhoto1ZoneAxis,
            dataColumnPhoto2ZoneAxis,
            dataColumnPhoto3ZoneAxis,
            dataColumnAngleBet12,
            dataColumnAngleBet23,
            dataColumnAngleBet31,
            dataColumn2});
            dataTable2.TableName = "Table2";
            // 
            // dataColumnID
            // 
            dataColumnID.ColumnName = "Number";
            dataColumnID.DataType = typeof(int);
            // 
            // dataColumnPhoto1ZoneAxis
            // 
            dataColumnPhoto1ZoneAxis.ColumnName = "Photo 1 Zone Axis";
            // 
            // dataColumnPhoto2ZoneAxis
            // 
            dataColumnPhoto2ZoneAxis.ColumnName = "Photo 2 Zone Axis";
            // 
            // dataColumnPhoto3ZoneAxis
            // 
            dataColumnPhoto3ZoneAxis.ColumnName = "Photo 3 Zone Axis";
            // 
            // dataColumnAngleBet12
            // 
            dataColumnAngleBet12.ColumnName = "Angle Between Photo 1 & 2";
            // 
            // dataColumnAngleBet23
            // 
            dataColumnAngleBet23.ColumnName = "Angle Between Photo 2 & 3";
            // 
            // dataColumnAngleBet31
            // 
            dataColumnAngleBet31.ColumnName = "Angle Between Photo 3 & 1";
            // 
            // dataColumn2
            // 
            dataColumn2.Caption = "Phase";
            dataColumn2.ColumnName = "Phase";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(5, 5);
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip")); // 260531Cl
            label1.Size = new System.Drawing.Size(38, 15);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            Phase,
            zoneAxisDataGridViewTextBoxColumn,
            g1DataGridViewTextBoxColumn,
            d1DataGridViewTextBoxColumn,
            g2DataGridViewTextBoxColumn,
            d2DataGridViewTextBoxColumn,
            g3DataGridViewTextBoxColumn,
            d3DataGridViewTextBoxColumn,
            θDataGridViewTextBoxColumn});
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.Location = new System.Drawing.Point(0, 31);
            dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            toolTip.SetToolTip(dataGridView1, resources.GetString("dataGridView1.ToolTip")); // 260531Cl
            dataGridView1.ReadOnly = true;
            //this.dataGridView1.RowTemplate.Height = 21;                                                                                             // 260413Cl DPIスケーリング対応のため削除
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size(974, 309);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            // 
            // bindingSource1
            // 
            bindingSource1.DataMember = "Table1";
            bindingSource1.DataSource = dataSet;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            dataGridViewTextBoxColumn1,
            photo1ZoneAxisDataGridViewTextBoxColumn1,
            photo2ZoneAxisDataGridViewTextBoxColumn,
            photo3ZoneAxisDataGridViewTextBoxColumn,
            angleBetweenPhoto12DataGridViewTextBoxColumn,
            angleBetweenPhoto23DataGridViewTextBoxColumn,
            angleBetweenPhoto31DataGridViewTextBoxColumn});
            dataGridView2.DataSource = bindingSource2;
            dataGridView2.Location = new System.Drawing.Point(0, 31);
            dataGridView2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataGridView2.MultiSelect = false;
            dataGridView2.Name = "dataGridView2";
            toolTip.SetToolTip(dataGridView2, resources.GetString("dataGridView2.ToolTip")); // 260531Cl
            dataGridView2.ReadOnly = true;
            //this.dataGridView2.RowTemplate.Height = 21;                                                                                             // 260413Cl DPIスケーリング対応のため削除
            dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Size = new System.Drawing.Size(974, 309);
            dataGridView2.TabIndex = 3;
            dataGridView2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView2_CellDoubleClick);
            // 
            // Phase
            // 
            Phase.DataPropertyName = "Phase";
            Phase.HeaderText = "Phase";
            Phase.Name = "Phase";
            Phase.ReadOnly = true;
            // 
            // zoneAxisDataGridViewTextBoxColumn
            // 
            zoneAxisDataGridViewTextBoxColumn.DataPropertyName = "Zone Axis";
            zoneAxisDataGridViewTextBoxColumn.HeaderText = "Zone Axis";
            zoneAxisDataGridViewTextBoxColumn.Name = "zoneAxisDataGridViewTextBoxColumn";
            zoneAxisDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // g1DataGridViewTextBoxColumn
            // 
            g1DataGridViewTextBoxColumn.DataPropertyName = "g1";
            g1DataGridViewTextBoxColumn.HeaderText = "g1";
            g1DataGridViewTextBoxColumn.Name = "g1DataGridViewTextBoxColumn";
            g1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // d1DataGridViewTextBoxColumn
            // 
            d1DataGridViewTextBoxColumn.DataPropertyName = "d1";
            d1DataGridViewTextBoxColumn.HeaderText = "d1";
            d1DataGridViewTextBoxColumn.Name = "d1DataGridViewTextBoxColumn";
            d1DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // g2DataGridViewTextBoxColumn
            // 
            g2DataGridViewTextBoxColumn.DataPropertyName = "g2";
            g2DataGridViewTextBoxColumn.HeaderText = "g2";
            g2DataGridViewTextBoxColumn.Name = "g2DataGridViewTextBoxColumn";
            g2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // d2DataGridViewTextBoxColumn
            // 
            d2DataGridViewTextBoxColumn.DataPropertyName = "d2";
            d2DataGridViewTextBoxColumn.HeaderText = "d2";
            d2DataGridViewTextBoxColumn.Name = "d2DataGridViewTextBoxColumn";
            d2DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // g3DataGridViewTextBoxColumn
            // 
            g3DataGridViewTextBoxColumn.DataPropertyName = "g3";
            g3DataGridViewTextBoxColumn.HeaderText = "g3";
            g3DataGridViewTextBoxColumn.Name = "g3DataGridViewTextBoxColumn";
            g3DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // d3DataGridViewTextBoxColumn
            // 
            d3DataGridViewTextBoxColumn.DataPropertyName = "d3";
            d3DataGridViewTextBoxColumn.HeaderText = "d3";
            d3DataGridViewTextBoxColumn.Name = "d3DataGridViewTextBoxColumn";
            d3DataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // θDataGridViewTextBoxColumn
            // 
            θDataGridViewTextBoxColumn.DataPropertyName = "θ";
            θDataGridViewTextBoxColumn.HeaderText = "θ";
            θDataGridViewTextBoxColumn.Name = "θDataGridViewTextBoxColumn";
            θDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "Phase";
            dataGridViewTextBoxColumn1.HeaderText = "Phase";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // photo1ZoneAxisDataGridViewTextBoxColumn1
            // 
            photo1ZoneAxisDataGridViewTextBoxColumn1.DataPropertyName = "Photo 1 Zone Axis";
            photo1ZoneAxisDataGridViewTextBoxColumn1.HeaderText = "Photo 1 Zone Axis";
            photo1ZoneAxisDataGridViewTextBoxColumn1.Name = "photo1ZoneAxisDataGridViewTextBoxColumn1";
            photo1ZoneAxisDataGridViewTextBoxColumn1.ReadOnly = true;
            photo1ZoneAxisDataGridViewTextBoxColumn1.Width = 125;
            // 
            // photo2ZoneAxisDataGridViewTextBoxColumn
            // 
            photo2ZoneAxisDataGridViewTextBoxColumn.DataPropertyName = "Photo 2 Zone Axis";
            photo2ZoneAxisDataGridViewTextBoxColumn.HeaderText = "Photo 2 Zone Axis";
            photo2ZoneAxisDataGridViewTextBoxColumn.Name = "photo2ZoneAxisDataGridViewTextBoxColumn";
            photo2ZoneAxisDataGridViewTextBoxColumn.ReadOnly = true;
            photo2ZoneAxisDataGridViewTextBoxColumn.Width = 125;
            // 
            // photo3ZoneAxisDataGridViewTextBoxColumn
            // 
            photo3ZoneAxisDataGridViewTextBoxColumn.DataPropertyName = "Photo 3 Zone Axis";
            photo3ZoneAxisDataGridViewTextBoxColumn.HeaderText = "Photo 3 Zone Axis";
            photo3ZoneAxisDataGridViewTextBoxColumn.Name = "photo3ZoneAxisDataGridViewTextBoxColumn";
            photo3ZoneAxisDataGridViewTextBoxColumn.ReadOnly = true;
            photo3ZoneAxisDataGridViewTextBoxColumn.Width = 125;
            // 
            // angleBetweenPhoto12DataGridViewTextBoxColumn
            // 
            angleBetweenPhoto12DataGridViewTextBoxColumn.DataPropertyName = "Angle Between Photo 1 & 2";
            angleBetweenPhoto12DataGridViewTextBoxColumn.HeaderText = "Angle Between Photo 1 & 2";
            angleBetweenPhoto12DataGridViewTextBoxColumn.Name = "angleBetweenPhoto12DataGridViewTextBoxColumn";
            angleBetweenPhoto12DataGridViewTextBoxColumn.ReadOnly = true;
            angleBetweenPhoto12DataGridViewTextBoxColumn.Width = 130;
            // 
            // angleBetweenPhoto23DataGridViewTextBoxColumn
            // 
            angleBetweenPhoto23DataGridViewTextBoxColumn.DataPropertyName = "Angle Between Photo 2 & 3";
            angleBetweenPhoto23DataGridViewTextBoxColumn.HeaderText = "Angle Between Photo 2 & 3";
            angleBetweenPhoto23DataGridViewTextBoxColumn.Name = "angleBetweenPhoto23DataGridViewTextBoxColumn";
            angleBetweenPhoto23DataGridViewTextBoxColumn.ReadOnly = true;
            angleBetweenPhoto23DataGridViewTextBoxColumn.Width = 130;
            // 
            // angleBetweenPhoto31DataGridViewTextBoxColumn
            // 
            angleBetweenPhoto31DataGridViewTextBoxColumn.DataPropertyName = "Angle Between Photo 3 & 1";
            angleBetweenPhoto31DataGridViewTextBoxColumn.HeaderText = "Angle Between Photo 3 & 1";
            angleBetweenPhoto31DataGridViewTextBoxColumn.Name = "angleBetweenPhoto31DataGridViewTextBoxColumn";
            angleBetweenPhoto31DataGridViewTextBoxColumn.ReadOnly = true;
            angleBetweenPhoto31DataGridViewTextBoxColumn.Width = 130;
            // 
            // FormTEMIDResults
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F); // 260329Cl 変更: Font→Dpi, 96dpi基準に統一
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(976, 341);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(dataGridView2);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "FormTEMIDResults";
            Text = "FormTEMIDResults";
            ((System.ComponentModel.ISupportInitialize)(bindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dataGridView2)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)

        private System.Data.DataSet dataSet;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumnZoneAxis;
        private System.Windows.Forms.BindingSource bindingSource2;
        private System.Data.DataColumn dataColumnG1;
        private System.Data.DataColumn dataColumnD1;
        private System.Data.DataColumn dataColumnG2;
        private System.Data.DataColumn dataColumnD2;
        private System.Data.DataColumn dataColumnG3;
        private System.Data.DataColumn dataColumnD3;
        private System.Data.DataColumn dataColumnTheta;
        private System.Windows.Forms.Label label1;
        private System.Data.DataTable dataTable2;
        private System.Data.DataColumn dataColumnPhoto1ZoneAxis;
        private System.Data.DataColumn dataColumnPhoto2ZoneAxis;
        private System.Data.DataColumn dataColumnPhoto3ZoneAxis;
        private System.Data.DataColumn dataColumnAngleBet12;
        private System.Data.DataColumn dataColumnAngleBet23;
        private System.Data.DataColumn dataColumnAngleBet31;
        // private System.Windows.Forms.DataGridView dataGridView1; // 260518Cl 旧実装
        private Crystallography.Controls.DpiAwareDataGridView dataGridView1; // 260518Cl
        // private System.Windows.Forms.DataGridView dataGridView2; // 260518Cl 旧実装
        private Crystallography.Controls.DpiAwareDataGridView dataGridView2; // 260518Cl
        private System.Data.DataColumn dataColumnNumber;
        private System.Data.DataColumn dataColumnID;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phase;
        private System.Windows.Forms.DataGridViewTextBoxColumn zoneAxisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn g1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn d1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn g2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn d2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn g3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn d3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn θDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn photo1ZoneAxisDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn photo2ZoneAxisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn photo3ZoneAxisDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn angleBetweenPhoto12DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn angleBetweenPhoto23DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn angleBetweenPhoto31DataGridViewTextBoxColumn;
    }
}