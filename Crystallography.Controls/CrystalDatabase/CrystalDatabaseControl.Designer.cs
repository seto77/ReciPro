namespace Crystallography.Controls
{
    partial class CrystalDatabaseControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrystalDatabaseControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ReadDatabaseWorker = new System.ComponentModel.BackgroundWorker();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.SaveDatabaseWorker = new System.ComponentModel.BackgroundWorker();
            this.bindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.densityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formulaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.aDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.alphaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.betaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gammaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.crystalSystemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pointGroupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spaceGroupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.authorsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.journalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new Crystallography.Controls.DataSet();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).BeginInit();
            this.bindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // ReadDatabaseWorker
            // 
            this.ReadDatabaseWorker.WorkerReportsProgress = true;
            this.ReadDatabaseWorker.WorkerSupportsCancellation = true;
            this.ReadDatabaseWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReadDatabaseWorker_DoWork);
            this.ReadDatabaseWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ReadDatabaseWorker_ProgressChanged);
            this.ReadDatabaseWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ReadDatabaseWorker_RunWorkerCompleted);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.densityDataGridViewTextBoxColumn,
            this.formulaDataGridViewTextBoxColumn,
            this.aDataGridViewTextBoxColumn,
            this.bDataGridViewTextBoxColumn,
            this.cDataGridViewTextBoxColumn,
            this.alphaDataGridViewTextBoxColumn,
            this.betaDataGridViewTextBoxColumn,
            this.gammaDataGridViewTextBoxColumn,
            this.crystalSystemDataGridViewTextBoxColumn,
            this.pointGroupDataGridViewTextBoxColumn,
            this.spaceGroupDataGridViewTextBoxColumn,
            this.authorsDataGridViewTextBoxColumn,
            this.titleDataGridViewTextBoxColumn,
            this.journalDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.bindingSource;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 23);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 44;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1191, 131);
            this.dataGridView.TabIndex = 76;
            // 
            // SaveDatabaseWorker
            // 
            this.SaveDatabaseWorker.WorkerReportsProgress = true;
            this.SaveDatabaseWorker.WorkerSupportsCancellation = true;
            this.SaveDatabaseWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SaveDatabaseWorker_DoWork);
            this.SaveDatabaseWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SaveDatabaseWorker_ProgressChanged);
            this.SaveDatabaseWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SaveDatabaseWorker_RunWorkerCompleted);
            // 
            // bindingNavigator
            // 
            this.bindingNavigator.AddNewItem = null;
            this.bindingNavigator.BindingSource = this.bindingSource;
            this.bindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorDeleteItem});
            this.bindingNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.bindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator.Name = "bindingNavigator";
            this.bindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator.Size = new System.Drawing.Size(1191, 23);
            this.bindingNavigator.TabIndex = 77;
            this.bindingNavigator.Text = "bindingNavigator";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(32, 17);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "項目の総数";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorDeleteItem.Text = "削除";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveFirstItem.Text = "最初に移動";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMovePreviousItem.Text = "前に戻る";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 23);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "現在の場所";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveNextItem.Text = "次に移動";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 20);
            this.bindingNavigatorMoveLastItem.Text = "最後に移動";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // densityDataGridViewTextBoxColumn
            // 
            this.densityDataGridViewTextBoxColumn.DataPropertyName = "Density";
            dataGridViewCellStyle2.Format = "N4";
            dataGridViewCellStyle2.NullValue = null;
            this.densityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.densityDataGridViewTextBoxColumn.HeaderText = "Density";
            this.densityDataGridViewTextBoxColumn.Name = "densityDataGridViewTextBoxColumn";
            this.densityDataGridViewTextBoxColumn.ReadOnly = true;
            this.densityDataGridViewTextBoxColumn.Width = 65;
            // 
            // formulaDataGridViewTextBoxColumn
            // 
            this.formulaDataGridViewTextBoxColumn.DataPropertyName = "Formula";
            this.formulaDataGridViewTextBoxColumn.HeaderText = "Formula";
            this.formulaDataGridViewTextBoxColumn.Name = "formulaDataGridViewTextBoxColumn";
            this.formulaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aDataGridViewTextBoxColumn
            // 
            this.aDataGridViewTextBoxColumn.DataPropertyName = "A";
            dataGridViewCellStyle3.Format = "#.######";
            dataGridViewCellStyle3.NullValue = null;
            this.aDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.aDataGridViewTextBoxColumn.HeaderText = "a";
            this.aDataGridViewTextBoxColumn.Name = "aDataGridViewTextBoxColumn";
            this.aDataGridViewTextBoxColumn.ReadOnly = true;
            this.aDataGridViewTextBoxColumn.Width = 60;
            // 
            // bDataGridViewTextBoxColumn
            // 
            this.bDataGridViewTextBoxColumn.DataPropertyName = "B";
            dataGridViewCellStyle4.Format = "#.######";
            dataGridViewCellStyle4.NullValue = null;
            this.bDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.bDataGridViewTextBoxColumn.HeaderText = "b";
            this.bDataGridViewTextBoxColumn.Name = "bDataGridViewTextBoxColumn";
            this.bDataGridViewTextBoxColumn.ReadOnly = true;
            this.bDataGridViewTextBoxColumn.Width = 60;
            // 
            // cDataGridViewTextBoxColumn
            // 
            this.cDataGridViewTextBoxColumn.DataPropertyName = "C";
            dataGridViewCellStyle5.Format = "#.######";
            dataGridViewCellStyle5.NullValue = null;
            this.cDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.cDataGridViewTextBoxColumn.HeaderText = "c";
            this.cDataGridViewTextBoxColumn.Name = "cDataGridViewTextBoxColumn";
            this.cDataGridViewTextBoxColumn.ReadOnly = true;
            this.cDataGridViewTextBoxColumn.Width = 60;
            // 
            // alphaDataGridViewTextBoxColumn
            // 
            this.alphaDataGridViewTextBoxColumn.DataPropertyName = "Alpha";
            dataGridViewCellStyle6.Format = "#.######";
            dataGridViewCellStyle6.NullValue = null;
            this.alphaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            this.alphaDataGridViewTextBoxColumn.HeaderText = "α";
            this.alphaDataGridViewTextBoxColumn.Name = "alphaDataGridViewTextBoxColumn";
            this.alphaDataGridViewTextBoxColumn.ReadOnly = true;
            this.alphaDataGridViewTextBoxColumn.Width = 60;
            // 
            // betaDataGridViewTextBoxColumn
            // 
            this.betaDataGridViewTextBoxColumn.DataPropertyName = "Beta";
            dataGridViewCellStyle7.Format = "#.######";
            dataGridViewCellStyle7.NullValue = null;
            this.betaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            this.betaDataGridViewTextBoxColumn.HeaderText = "β";
            this.betaDataGridViewTextBoxColumn.Name = "betaDataGridViewTextBoxColumn";
            this.betaDataGridViewTextBoxColumn.ReadOnly = true;
            this.betaDataGridViewTextBoxColumn.Width = 60;
            // 
            // gammaDataGridViewTextBoxColumn
            // 
            this.gammaDataGridViewTextBoxColumn.DataPropertyName = "Gamma";
            dataGridViewCellStyle8.Format = "#.######";
            dataGridViewCellStyle8.NullValue = null;
            this.gammaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            this.gammaDataGridViewTextBoxColumn.HeaderText = "γ";
            this.gammaDataGridViewTextBoxColumn.Name = "gammaDataGridViewTextBoxColumn";
            this.gammaDataGridViewTextBoxColumn.ReadOnly = true;
            this.gammaDataGridViewTextBoxColumn.Width = 60;
            // 
            // crystalSystemDataGridViewTextBoxColumn
            // 
            this.crystalSystemDataGridViewTextBoxColumn.DataPropertyName = "CrystalSystem";
            this.crystalSystemDataGridViewTextBoxColumn.HeaderText = "Crystal System";
            this.crystalSystemDataGridViewTextBoxColumn.Name = "crystalSystemDataGridViewTextBoxColumn";
            this.crystalSystemDataGridViewTextBoxColumn.ReadOnly = true;
            this.crystalSystemDataGridViewTextBoxColumn.Width = 115;
            // 
            // pointGroupDataGridViewTextBoxColumn
            // 
            this.pointGroupDataGridViewTextBoxColumn.DataPropertyName = "PointGroup";
            this.pointGroupDataGridViewTextBoxColumn.HeaderText = "Point Group";
            this.pointGroupDataGridViewTextBoxColumn.Name = "pointGroupDataGridViewTextBoxColumn";
            this.pointGroupDataGridViewTextBoxColumn.ReadOnly = true;
            this.pointGroupDataGridViewTextBoxColumn.Width = 105;
            // 
            // spaceGroupDataGridViewTextBoxColumn
            // 
            this.spaceGroupDataGridViewTextBoxColumn.DataPropertyName = "SpaceGroup";
            this.spaceGroupDataGridViewTextBoxColumn.HeaderText = "Space Group";
            this.spaceGroupDataGridViewTextBoxColumn.Name = "spaceGroupDataGridViewTextBoxColumn";
            this.spaceGroupDataGridViewTextBoxColumn.ReadOnly = true;
            this.spaceGroupDataGridViewTextBoxColumn.Width = 110;
            // 
            // authorsDataGridViewTextBoxColumn
            // 
            this.authorsDataGridViewTextBoxColumn.DataPropertyName = "Authors";
            this.authorsDataGridViewTextBoxColumn.HeaderText = "Authors";
            this.authorsDataGridViewTextBoxColumn.Name = "authorsDataGridViewTextBoxColumn";
            this.authorsDataGridViewTextBoxColumn.ReadOnly = true;
            this.authorsDataGridViewTextBoxColumn.Width = 85;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            this.titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            this.titleDataGridViewTextBoxColumn.HeaderText = "Title";
            this.titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            this.titleDataGridViewTextBoxColumn.ReadOnly = true;
            this.titleDataGridViewTextBoxColumn.Width = 85;
            // 
            // journalDataGridViewTextBoxColumn
            // 
            this.journalDataGridViewTextBoxColumn.DataPropertyName = "Journal";
            this.journalDataGridViewTextBoxColumn.HeaderText = "Journal";
            this.journalDataGridViewTextBoxColumn.Name = "journalDataGridViewTextBoxColumn";
            this.journalDataGridViewTextBoxColumn.ReadOnly = true;
            this.journalDataGridViewTextBoxColumn.Width = 85;
            // 
            // bindingSource
            // 
            this.bindingSource.DataMember = "DataTableCrystalDatabase";
            this.bindingSource.DataSource = this.dataSet;
            this.bindingSource.CurrentChanged += new System.EventHandler(this.bindingSource_CurrentChanged);
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // CrystalDatabaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.bindingNavigator);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CrystalDatabaseControl";
            this.Size = new System.Drawing.Size(1191, 154);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator)).EndInit();
            this.bindingNavigator.ResumeLayout(false);
            this.bindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.DataGridView dataGridView;
        private DataSet dataSet;
        public System.ComponentModel.BackgroundWorker SaveDatabaseWorker;
        public System.ComponentModel.BackgroundWorker ReadDatabaseWorker;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn densityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn formulaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn alphaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn betaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn gammaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn crystalSystemDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pointGroupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn spaceGroupDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn authorsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn journalDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingNavigator bindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
    }
}
