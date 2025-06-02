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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrystalDatabaseControl));
            ReadDatabaseWorker = new System.ComponentModel.BackgroundWorker();
            dataGridView = new System.Windows.Forms.DataGridView();
            nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            densityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            formulaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            aDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            cDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            alphaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            betaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            gammaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            crystalSystemDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            pointGroupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            spaceGroupDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            authorsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            titleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            journalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bindingSource = new System.Windows.Forms.BindingSource(components);
            dataSet = new DataSet();
            SaveDatabaseWorker = new System.ComponentModel.BackgroundWorker();
            bindingNavigator = new System.Windows.Forms.BindingNavigator(components);
            bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            DownloadCodWorker = new System.ComponentModel.BackgroundWorker();
            checkBoxAMCSD = new System.Windows.Forms.CheckBox();
            checkBoxCOD = new System.Windows.Forms.CheckBox();
            flowLayoutPanelDatabase = new System.Windows.Forms.FlowLayoutPanel();
            textBox1 = new System.Windows.Forms.TextBox();
            panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingNavigator).BeginInit();
            bindingNavigator.SuspendLayout();
            flowLayoutPanelDatabase.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ReadDatabaseWorker
            // 
            ReadDatabaseWorker.WorkerReportsProgress = true;
            ReadDatabaseWorker.WorkerSupportsCancellation = true;
            ReadDatabaseWorker.DoWork += ReadDatabaseWorker_DoWork;
            ReadDatabaseWorker.ProgressChanged += ReadDatabaseWorker_ProgressChanged;
            ReadDatabaseWorker.RunWorkerCompleted += ReadDatabaseWorker_RunWorkerCompleted;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AutoGenerateColumns = false;
            dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { nameDataGridViewTextBoxColumn, densityDataGridViewTextBoxColumn, formulaDataGridViewTextBoxColumn, aDataGridViewTextBoxColumn, bDataGridViewTextBoxColumn, cDataGridViewTextBoxColumn, alphaDataGridViewTextBoxColumn, betaDataGridViewTextBoxColumn, gammaDataGridViewTextBoxColumn, crystalSystemDataGridViewTextBoxColumn, pointGroupDataGridViewTextBoxColumn, spaceGroupDataGridViewTextBoxColumn, authorsDataGridViewTextBoxColumn, titleDataGridViewTextBoxColumn, journalDataGridViewTextBoxColumn });
            dataGridView.DataSource = bindingSource;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView.DefaultCellStyle = dataGridViewCellStyle9;
            dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView.Location = new System.Drawing.Point(0, 70);
            dataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidth = 44;
            dataGridView.RowTemplate.Height = 21;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new System.Drawing.Size(913, 526);
            dataGridView.TabIndex = 76;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // densityDataGridViewTextBoxColumn
            // 
            densityDataGridViewTextBoxColumn.DataPropertyName = "Density";
            dataGridViewCellStyle2.Format = "N4";
            dataGridViewCellStyle2.NullValue = null;
            densityDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            densityDataGridViewTextBoxColumn.HeaderText = "Density";
            densityDataGridViewTextBoxColumn.Name = "densityDataGridViewTextBoxColumn";
            densityDataGridViewTextBoxColumn.ReadOnly = true;
            densityDataGridViewTextBoxColumn.Width = 65;
            // 
            // formulaDataGridViewTextBoxColumn
            // 
            formulaDataGridViewTextBoxColumn.DataPropertyName = "Formula";
            formulaDataGridViewTextBoxColumn.HeaderText = "Formula";
            formulaDataGridViewTextBoxColumn.Name = "formulaDataGridViewTextBoxColumn";
            formulaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // aDataGridViewTextBoxColumn
            // 
            aDataGridViewTextBoxColumn.DataPropertyName = "A";
            dataGridViewCellStyle3.Format = "#.#####";
            dataGridViewCellStyle3.NullValue = null;
            aDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            aDataGridViewTextBoxColumn.HeaderText = "a";
            aDataGridViewTextBoxColumn.Name = "aDataGridViewTextBoxColumn";
            aDataGridViewTextBoxColumn.ReadOnly = true;
            aDataGridViewTextBoxColumn.Width = 60;
            // 
            // bDataGridViewTextBoxColumn
            // 
            bDataGridViewTextBoxColumn.DataPropertyName = "B";
            dataGridViewCellStyle4.Format = "#.#####";
            dataGridViewCellStyle4.NullValue = null;
            bDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
            bDataGridViewTextBoxColumn.HeaderText = "b";
            bDataGridViewTextBoxColumn.Name = "bDataGridViewTextBoxColumn";
            bDataGridViewTextBoxColumn.ReadOnly = true;
            bDataGridViewTextBoxColumn.Width = 60;
            // 
            // cDataGridViewTextBoxColumn
            // 
            cDataGridViewTextBoxColumn.DataPropertyName = "C";
            dataGridViewCellStyle5.Format = "#.#####";
            dataGridViewCellStyle5.NullValue = null;
            cDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
            cDataGridViewTextBoxColumn.HeaderText = "c";
            cDataGridViewTextBoxColumn.Name = "cDataGridViewTextBoxColumn";
            cDataGridViewTextBoxColumn.ReadOnly = true;
            cDataGridViewTextBoxColumn.Width = 60;
            // 
            // alphaDataGridViewTextBoxColumn
            // 
            alphaDataGridViewTextBoxColumn.DataPropertyName = "Alpha";
            dataGridViewCellStyle6.Format = "#.#####";
            dataGridViewCellStyle6.NullValue = null;
            alphaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle6;
            alphaDataGridViewTextBoxColumn.HeaderText = "α";
            alphaDataGridViewTextBoxColumn.Name = "alphaDataGridViewTextBoxColumn";
            alphaDataGridViewTextBoxColumn.ReadOnly = true;
            alphaDataGridViewTextBoxColumn.Width = 60;
            // 
            // betaDataGridViewTextBoxColumn
            // 
            betaDataGridViewTextBoxColumn.DataPropertyName = "Beta";
            dataGridViewCellStyle7.Format = "#.#####";
            dataGridViewCellStyle7.NullValue = null;
            betaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle7;
            betaDataGridViewTextBoxColumn.HeaderText = "β";
            betaDataGridViewTextBoxColumn.Name = "betaDataGridViewTextBoxColumn";
            betaDataGridViewTextBoxColumn.ReadOnly = true;
            betaDataGridViewTextBoxColumn.Width = 60;
            // 
            // gammaDataGridViewTextBoxColumn
            // 
            gammaDataGridViewTextBoxColumn.DataPropertyName = "Gamma";
            dataGridViewCellStyle8.Format = "#.#####";
            dataGridViewCellStyle8.NullValue = null;
            gammaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            gammaDataGridViewTextBoxColumn.HeaderText = "γ";
            gammaDataGridViewTextBoxColumn.Name = "gammaDataGridViewTextBoxColumn";
            gammaDataGridViewTextBoxColumn.ReadOnly = true;
            gammaDataGridViewTextBoxColumn.Width = 60;
            // 
            // crystalSystemDataGridViewTextBoxColumn
            // 
            crystalSystemDataGridViewTextBoxColumn.DataPropertyName = "CrystalSystem";
            crystalSystemDataGridViewTextBoxColumn.HeaderText = "Crystal System";
            crystalSystemDataGridViewTextBoxColumn.Name = "crystalSystemDataGridViewTextBoxColumn";
            crystalSystemDataGridViewTextBoxColumn.ReadOnly = true;
            crystalSystemDataGridViewTextBoxColumn.Width = 115;
            // 
            // pointGroupDataGridViewTextBoxColumn
            // 
            pointGroupDataGridViewTextBoxColumn.DataPropertyName = "PointGroup";
            pointGroupDataGridViewTextBoxColumn.HeaderText = "Point Group";
            pointGroupDataGridViewTextBoxColumn.Name = "pointGroupDataGridViewTextBoxColumn";
            pointGroupDataGridViewTextBoxColumn.ReadOnly = true;
            pointGroupDataGridViewTextBoxColumn.Width = 105;
            // 
            // spaceGroupDataGridViewTextBoxColumn
            // 
            spaceGroupDataGridViewTextBoxColumn.DataPropertyName = "SpaceGroup";
            spaceGroupDataGridViewTextBoxColumn.HeaderText = "Space Group";
            spaceGroupDataGridViewTextBoxColumn.Name = "spaceGroupDataGridViewTextBoxColumn";
            spaceGroupDataGridViewTextBoxColumn.ReadOnly = true;
            spaceGroupDataGridViewTextBoxColumn.Width = 110;
            // 
            // authorsDataGridViewTextBoxColumn
            // 
            authorsDataGridViewTextBoxColumn.DataPropertyName = "Authors";
            authorsDataGridViewTextBoxColumn.HeaderText = "Authors";
            authorsDataGridViewTextBoxColumn.Name = "authorsDataGridViewTextBoxColumn";
            authorsDataGridViewTextBoxColumn.ReadOnly = true;
            authorsDataGridViewTextBoxColumn.Width = 85;
            // 
            // titleDataGridViewTextBoxColumn
            // 
            titleDataGridViewTextBoxColumn.DataPropertyName = "Title";
            titleDataGridViewTextBoxColumn.HeaderText = "Title";
            titleDataGridViewTextBoxColumn.Name = "titleDataGridViewTextBoxColumn";
            titleDataGridViewTextBoxColumn.ReadOnly = true;
            titleDataGridViewTextBoxColumn.Width = 85;
            // 
            // journalDataGridViewTextBoxColumn
            // 
            journalDataGridViewTextBoxColumn.DataPropertyName = "Journal";
            journalDataGridViewTextBoxColumn.HeaderText = "Journal";
            journalDataGridViewTextBoxColumn.Name = "journalDataGridViewTextBoxColumn";
            journalDataGridViewTextBoxColumn.ReadOnly = true;
            journalDataGridViewTextBoxColumn.Width = 85;
            // 
            // bindingSource
            // 
            bindingSource.DataMember = "DataTableCrystalDatabase";
            bindingSource.DataSource = dataSet;
            bindingSource.CurrentChanged += bindingSource_CurrentChanged;
            // 
            // dataSet
            // 
            dataSet.DataSetName = "DataSet";
            dataSet.Namespace = "http://tempuri.org/DataSet1.xsd";
            dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SaveDatabaseWorker
            // 
            SaveDatabaseWorker.WorkerReportsProgress = true;
            SaveDatabaseWorker.WorkerSupportsCancellation = true;
            SaveDatabaseWorker.DoWork += SaveDatabaseWorker_DoWork;
            SaveDatabaseWorker.ProgressChanged += SaveDatabaseWorker_ProgressChanged;
            SaveDatabaseWorker.RunWorkerCompleted += SaveDatabaseWorker_RunWorkerCompleted;
            // 
            // bindingNavigator
            // 
            bindingNavigator.AddNewItem = null;
            bindingNavigator.BindingSource = bindingSource;
            bindingNavigator.CountItem = bindingNavigatorCountItem;
            bindingNavigator.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            bindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { bindingNavigatorMoveFirstItem, bindingNavigatorMovePreviousItem, bindingNavigatorSeparator, bindingNavigatorPositionItem, bindingNavigatorCountItem, bindingNavigatorSeparator1, bindingNavigatorMoveNextItem, bindingNavigatorMoveLastItem });
            bindingNavigator.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            bindingNavigator.Location = new System.Drawing.Point(0, 47);
            bindingNavigator.MoveFirstItem = bindingNavigatorMoveFirstItem;
            bindingNavigator.MoveLastItem = bindingNavigatorMoveLastItem;
            bindingNavigator.MoveNextItem = bindingNavigatorMoveNextItem;
            bindingNavigator.MovePreviousItem = bindingNavigatorMovePreviousItem;
            bindingNavigator.Name = "bindingNavigator";
            bindingNavigator.PositionItem = bindingNavigatorPositionItem;
            bindingNavigator.Size = new System.Drawing.Size(913, 23);
            bindingNavigator.TabIndex = 77;
            bindingNavigator.Text = "bindingNavigator";
            // 
            // bindingNavigatorCountItem
            // 
            bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            bindingNavigatorCountItem.Size = new System.Drawing.Size(28, 17);
            bindingNavigatorCountItem.Text = "/{0}";
            bindingNavigatorCountItem.ToolTipText = "項目の総数";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bindingNavigatorMoveFirstItem.Image = (System.Drawing.Image)resources.GetObject("bindingNavigatorMoveFirstItem.Image");
            bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 20);
            bindingNavigatorMoveFirstItem.Text = "最初に移動";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bindingNavigatorMovePreviousItem.Image = (System.Drawing.Image)resources.GetObject("bindingNavigatorMovePreviousItem.Image");
            bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 20);
            bindingNavigatorMovePreviousItem.Text = "前に戻る";
            // 
            // bindingNavigatorSeparator
            // 
            bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 23);
            // 
            // bindingNavigatorPositionItem
            // 
            bindingNavigatorPositionItem.AccessibleName = "位置";
            bindingNavigatorPositionItem.AutoSize = false;
            bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            bindingNavigatorPositionItem.Text = "0";
            bindingNavigatorPositionItem.ToolTipText = "現在の場所";
            // 
            // bindingNavigatorSeparator1
            // 
            bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // bindingNavigatorMoveNextItem
            // 
            bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bindingNavigatorMoveNextItem.Image = (System.Drawing.Image)resources.GetObject("bindingNavigatorMoveNextItem.Image");
            bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 20);
            bindingNavigatorMoveNextItem.Text = "次に移動";
            // 
            // bindingNavigatorMoveLastItem
            // 
            bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            bindingNavigatorMoveLastItem.Image = (System.Drawing.Image)resources.GetObject("bindingNavigatorMoveLastItem.Image");
            bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 20);
            bindingNavigatorMoveLastItem.Text = "最後に移動";
            // 
            // DownloadCodWorker
            // 
            DownloadCodWorker.WorkerReportsProgress = true;
            DownloadCodWorker.WorkerSupportsCancellation = true;
            DownloadCodWorker.DoWork += DownloadCodWorker_DoWork;
            DownloadCodWorker.ProgressChanged += DownloadCodWorker_ProgressChanged;
            DownloadCodWorker.RunWorkerCompleted += DownloadCodWorker_RunWorkerCompleted;
            // 
            // checkBoxAMCSD
            // 
            checkBoxAMCSD.AutoSize = true;
            checkBoxAMCSD.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            checkBoxAMCSD.Location = new System.Drawing.Point(3, 0);
            checkBoxAMCSD.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            checkBoxAMCSD.Name = "checkBoxAMCSD";
            checkBoxAMCSD.Size = new System.Drawing.Size(71, 21);
            checkBoxAMCSD.TabIndex = 0;
            checkBoxAMCSD.Text = "AMCSD";
            checkBoxAMCSD.UseVisualStyleBackColor = true;
            checkBoxAMCSD.CheckedChanged += checkBoxAMCSD_CheckedChanged;
            // 
            // checkBoxCOD
            // 
            checkBoxCOD.AutoSize = true;
            checkBoxCOD.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            checkBoxCOD.Location = new System.Drawing.Point(3, 21);
            checkBoxCOD.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            checkBoxCOD.Name = "checkBoxCOD";
            checkBoxCOD.Size = new System.Drawing.Size(54, 21);
            checkBoxCOD.TabIndex = 0;
            checkBoxCOD.Text = "COD";
            checkBoxCOD.UseVisualStyleBackColor = true;
            checkBoxCOD.CheckedChanged += checkBoxCOD_CheckedChanged;
            // 
            // flowLayoutPanelDatabase
            // 
            flowLayoutPanelDatabase.AutoSize = true;
            flowLayoutPanelDatabase.Controls.Add(checkBoxAMCSD);
            flowLayoutPanelDatabase.Controls.Add(checkBoxCOD);
            flowLayoutPanelDatabase.Dock = System.Windows.Forms.DockStyle.Left;
            flowLayoutPanelDatabase.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanelDatabase.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanelDatabase.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanelDatabase.Name = "flowLayoutPanelDatabase";
            flowLayoutPanelDatabase.Size = new System.Drawing.Size(77, 47);
            flowLayoutPanelDatabase.TabIndex = 4;
            flowLayoutPanelDatabase.Visible = false;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            textBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 8F);
            textBox1.Location = new System.Drawing.Point(77, 0);
            textBox1.Margin = new System.Windows.Forms.Padding(0);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(836, 47);
            textBox1.TabIndex = 2;
            textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // panel1
            // 
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(flowLayoutPanelDatabase);
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(913, 47);
            panel1.TabIndex = 86;
            // 
            // CrystalDatabaseControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(dataGridView);
            Controls.Add(bindingNavigator);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            Name = "CrystalDatabaseControl";
            Size = new System.Drawing.Size(913, 596);
            Resize += CrystalDatabaseControl_Resize;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingNavigator).EndInit();
            bindingNavigator.ResumeLayout(false);
            bindingNavigator.PerformLayout();
            flowLayoutPanelDatabase.ResumeLayout(false);
            flowLayoutPanelDatabase.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSource;
        private System.Windows.Forms.DataGridView dataGridView;
        private DataSet dataSet;
        public System.ComponentModel.BackgroundWorker SaveDatabaseWorker;
        public System.ComponentModel.BackgroundWorker ReadDatabaseWorker;
        private System.Windows.Forms.BindingNavigator bindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
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
        public System.ComponentModel.BackgroundWorker DownloadCodWorker;
        private System.Windows.Forms.CheckBox checkBoxCOD;
        private System.Windows.Forms.CheckBox checkBoxAMCSD;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDatabase;
        private System.Windows.Forms.Panel panel1;
    }
}
