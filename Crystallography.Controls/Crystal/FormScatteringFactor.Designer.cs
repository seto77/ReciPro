namespace Crystallography.Controls
{
    partial class FormScatteringFactor
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.K = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RelInt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Condition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceScatteringFactor = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new Crystallography.Controls.DataSet();
            this.numericUpDownThresholdD = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxHideProhibitedPlanes = new System.Windows.Forms.CheckBox();
            this.checkBoxHideEquivalentPlane = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonCopyClipboard = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.checkBoxBragBrentano = new System.Windows.Forms.CheckBox();
            this.waveLengthControl1 = new Crystallography.Controls.WaveLengthControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceScatteringFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdD)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.AutoGenerateColumns = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.K,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.F,
            this.dataGridViewTextBoxColumn11,
            this.RelInt,
            this.Condition});
            this.dataGridView2.DataSource = this.bindingSourceScatteringFactor;
            this.dataGridView2.Location = new System.Drawing.Point(1, 111);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView2.RowTemplate.Height = 21;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(824, 285);
            this.dataGridView2.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "H";
            this.dataGridViewTextBoxColumn1.HeaderText = "h";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 30;
            // 
            // K
            // 
            this.K.DataPropertyName = "K";
            this.K.HeaderText = "k";
            this.K.Name = "K";
            this.K.ReadOnly = true;
            this.K.Width = 30;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "L";
            this.dataGridViewTextBoxColumn3.HeaderText = "l";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 30;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Multi";
            this.dataGridViewTextBoxColumn4.HeaderText = "Multi.";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 45;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "D";
            dataGridViewCellStyle2.Format = "G7";
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn5.HeaderText = "d (Å)";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Q";
            dataGridViewCellStyle3.Format = "G7";
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn6.HeaderText = "q (2π/d)";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 75;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "TwoTheta";
            dataGridViewCellStyle4.Format = "G7";
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn7.HeaderText = "2θ (°)";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 70;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "F_real";
            dataGridViewCellStyle5.Format = "G7";
            this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn9.HeaderText = "F_real";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 70;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "F_inv";
            dataGridViewCellStyle6.Format = "G7";
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn10.HeaderText = "F_inv";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 70;
            // 
            // F
            // 
            this.F.DataPropertyName = "F";
            dataGridViewCellStyle7.Format = "G7";
            this.F.DefaultCellStyle = dataGridViewCellStyle7;
            this.F.HeaderText = "|F|";
            this.F.Name = "F";
            this.F.ReadOnly = true;
            this.F.Width = 70;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "F2";
            dataGridViewCellStyle8.Format = "G7";
            this.dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn11.HeaderText = "F^2";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 70;
            // 
            // RelInt
            // 
            this.RelInt.DataPropertyName = "RelInt";
            dataGridViewCellStyle9.Format = "G7";
            this.RelInt.DefaultCellStyle = dataGridViewCellStyle9;
            this.RelInt.HeaderText = "Rel. Int. (%)";
            this.RelInt.Name = "RelInt";
            this.RelInt.ReadOnly = true;
            this.RelInt.Width = 90;
            // 
            // Condition
            // 
            this.Condition.DataPropertyName = "Condition";
            this.Condition.HeaderText = "Condition";
            this.Condition.Name = "Condition";
            this.Condition.ReadOnly = true;
            this.Condition.Width = 80;
            // 
            // bindingSourceScatteringFactor
            // 
            this.bindingSourceScatteringFactor.DataMember = "DataTableScatteringFactor";
            this.bindingSourceScatteringFactor.DataSource = this.dataSet;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.Namespace = "http://tempuri.org/DataSet1.xsd";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // numericUpDownThresholdD
            // 
            this.numericUpDownThresholdD.DecimalPlaces = 4;
            this.numericUpDownThresholdD.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownThresholdD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownThresholdD.Location = new System.Drawing.Point(574, 36);
            this.numericUpDownThresholdD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericUpDownThresholdD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownThresholdD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownThresholdD.Name = "numericUpDownThresholdD";
            this.numericUpDownThresholdD.Size = new System.Drawing.Size(61, 25);
            this.numericUpDownThresholdD.TabIndex = 5;
            this.numericUpDownThresholdD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownThresholdD.ValueChanged += new System.EventHandler(this.numericUpDownThresholdD_ValueChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(517, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Threshold of d-spacing";
            // 
            // checkBoxHideProhibitedPlanes
            // 
            this.checkBoxHideProhibitedPlanes.AutoSize = true;
            this.checkBoxHideProhibitedPlanes.Checked = true;
            this.checkBoxHideProhibitedPlanes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideProhibitedPlanes.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxHideProhibitedPlanes.Location = new System.Drawing.Point(305, 78);
            this.checkBoxHideProhibitedPlanes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxHideProhibitedPlanes.Name = "checkBoxHideProhibitedPlanes";
            this.checkBoxHideProhibitedPlanes.Size = new System.Drawing.Size(161, 21);
            this.checkBoxHideProhibitedPlanes.TabIndex = 8;
            this.checkBoxHideProhibitedPlanes.Text = "Hide prohibited planes";
            this.checkBoxHideProhibitedPlanes.CheckedChanged += new System.EventHandler(this.numericUpDownThresholdD_ValueChanged);
            // 
            // checkBoxHideEquivalentPlane
            // 
            this.checkBoxHideEquivalentPlane.AutoSize = true;
            this.checkBoxHideEquivalentPlane.Checked = true;
            this.checkBoxHideEquivalentPlane.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideEquivalentPlane.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxHideEquivalentPlane.Location = new System.Drawing.Point(305, 53);
            this.checkBoxHideEquivalentPlane.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxHideEquivalentPlane.Name = "checkBoxHideEquivalentPlane";
            this.checkBoxHideEquivalentPlane.Size = new System.Drawing.Size(159, 21);
            this.checkBoxHideEquivalentPlane.TabIndex = 8;
            this.checkBoxHideEquivalentPlane.Text = "Hide equivalent planes";
            this.checkBoxHideEquivalentPlane.CheckedChanged += new System.EventHandler(this.numericUpDownThresholdD_ValueChanged);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(638, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(28, 20);
            this.label13.TabIndex = 6;
            this.label13.Text = "Å";
            // 
            // buttonCopyClipboard
            // 
            this.buttonCopyClipboard.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCopyClipboard.Location = new System.Drawing.Point(506, 78);
            this.buttonCopyClipboard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCopyClipboard.Name = "buttonCopyClipboard";
            this.buttonCopyClipboard.Size = new System.Drawing.Size(141, 28);
            this.buttonCopyClipboard.TabIndex = 8;
            this.buttonCopyClipboard.Text = "Copy to Clipboard";
            this.buttonCopyClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyClipboard.Click += new System.EventHandler(this.buttonCopyClipBoard_Click);
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label18.Location = new System.Drawing.Point(517, 36);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(51, 20);
            this.label18.TabIndex = 6;
            this.label18.Text = "under";
            // 
            // checkBoxBragBrentano
            // 
            this.checkBoxBragBrentano.AutoSize = true;
            this.checkBoxBragBrentano.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxBragBrentano.Location = new System.Drawing.Point(307, 1);
            this.checkBoxBragBrentano.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxBragBrentano.Name = "checkBoxBragBrentano";
            this.checkBoxBragBrentano.Size = new System.Drawing.Size(199, 38);
            this.checkBoxBragBrentano.TabIndex = 8;
            this.checkBoxBragBrentano.Text = "Powder diffraction intensities \r\n(Bragg Brentano optics)";
            this.checkBoxBragBrentano.CheckedChanged += new System.EventHandler(this.checkBoxBragBrentano_CheckedChanged);
            // 
            // waveLengthControl1
            // 
            this.waveLengthControl1.AutoSize = true;
            this.waveLengthControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.waveLengthControl1.Energy = 8.041147213082D;
            this.waveLengthControl1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.waveLengthControl1.Location = new System.Drawing.Point(10, 2);
            this.waveLengthControl1.Margin = new System.Windows.Forms.Padding(0);
            this.waveLengthControl1.MinimumSize = new System.Drawing.Size(200, 0);
            this.waveLengthControl1.Name = "waveLengthControl1";
            this.waveLengthControl1.ShowWaveSource = true;
            this.waveLengthControl1.Size = new System.Drawing.Size(201, 104);
            this.waveLengthControl1.TabIndex = 9;
            this.waveLengthControl1.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.waveLengthControl1.WaveLength = 0.1541871066667D;
            this.waveLengthControl1.WaveSource = Crystallography.WaveSource.Xray;
            this.waveLengthControl1.XrayWaveSourceElementNumber = 29;
            this.waveLengthControl1.XrayWaveSourceLine = Crystallography.XrayLine.Ka;
            this.waveLengthControl1.WavelengthChanged += new System.EventHandler(this.waveLengthControl1_WavelengthChanged);
            // 
            // FormScatteringFactor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(831, 398);
            this.Controls.Add(this.waveLengthControl1);
            this.Controls.Add(this.buttonCopyClipboard);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.numericUpDownThresholdD);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.checkBoxHideProhibitedPlanes);
            this.Controls.Add(this.checkBoxBragBrentano);
            this.Controls.Add(this.checkBoxHideEquivalentPlane);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormScatteringFactor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Scattering Factor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCrystallographicInformation_FormClosing);
            this.Load += new System.EventHandler(this.FormCrystallographicInformation_Load);
            this.VisibleChanged += new System.EventHandler(this.FormScatteringFactor_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceScatteringFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThresholdD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxHideEquivalentPlane;
        private System.Windows.Forms.CheckBox checkBoxHideProhibitedPlanes;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.NumericUpDown numericUpDownThresholdD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonCopyClipboard;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox checkBoxBragBrentano;
        private System.Windows.Forms.BindingSource bindingSourceScatteringFactor;
        private WaveLengthControl waveLengthControl1;
        private DataSet dataSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn K;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn F;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelInt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Condition;
        private System.Windows.Forms.ToolTip toolTip;
    }
}