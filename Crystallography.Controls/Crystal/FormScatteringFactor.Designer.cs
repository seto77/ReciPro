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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridView2 = new System.Windows.Forms.DataGridView();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            K = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            F = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            RelInt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Condition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bindingSourceScatteringFactor = new System.Windows.Forms.BindingSource(components);
            dataSet = new DataSet();
            numericUpDownThresholdD = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            checkBoxHideProhibitedPlanes = new System.Windows.Forms.CheckBox();
            checkBoxHideEquivalentPlane = new System.Windows.Forms.CheckBox();
            label13 = new System.Windows.Forms.Label();
            buttonCopyClipboard = new System.Windows.Forms.Button();
            label18 = new System.Windows.Forms.Label();
            checkBoxBragBrentano = new System.Windows.Forms.CheckBox();
            waveLengthControl1 = new WaveLengthControl();
            toolTip = new System.Windows.Forms.ToolTip(components);
            checkBoxTest = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            numericBoxL_step = new NumericBox();
            numericBoxK_step = new NumericBox();
            numericBoxH_step = new NumericBox();
            numericBoxL_max = new NumericBox();
            numericBoxK_max = new NumericBox();
            numericBoxL_min = new NumericBox();
            numericBoxK_min = new NumericBox();
            numericBoxH_max = new NumericBox();
            numericBoxH_min = new NumericBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceScatteringFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownThresholdD).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dataGridViewTextBoxColumn1, K, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn9, dataGridViewTextBoxColumn10, F, dataGridViewTextBoxColumn11, RelInt, Condition });
            dataGridView2.DataSource = bindingSourceScatteringFactor;
            dataGridView2.Location = new System.Drawing.Point(3, 110);
            dataGridView2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            dataGridView2.MultiSelect = false;
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView2.RowTemplate.Height = 21;
            dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Size = new System.Drawing.Size(824, 284);
            dataGridView2.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "H";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewTextBoxColumn1.HeaderText = "h";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.Width = 30;
            // 
            // K
            // 
            K.DataPropertyName = "K";
            K.HeaderText = "k";
            K.Name = "K";
            K.ReadOnly = true;
            K.Width = 30;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.DataPropertyName = "L";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewTextBoxColumn3.HeaderText = "l";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.Width = 30;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.DataPropertyName = "Multi";
            dataGridViewTextBoxColumn4.HeaderText = "Multi.";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Width = 45;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.DataPropertyName = "D";
            dataGridViewCellStyle4.Format = "G7";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewTextBoxColumn5.HeaderText = "d (Å)";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.DataPropertyName = "Q";
            dataGridViewCellStyle5.Format = "G7";
            dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewTextBoxColumn6.HeaderText = "q (2π/d)";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.Width = 75;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.DataPropertyName = "TwoTheta";
            dataGridViewCellStyle6.Format = "G7";
            dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewTextBoxColumn7.HeaderText = "2θ (°)";
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            dataGridViewTextBoxColumn7.Width = 70;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.DataPropertyName = "F_real";
            dataGridViewCellStyle7.Format = "G7";
            dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewTextBoxColumn9.HeaderText = "F_real";
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            dataGridViewTextBoxColumn9.ReadOnly = true;
            dataGridViewTextBoxColumn9.Width = 70;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.DataPropertyName = "F_inv";
            dataGridViewCellStyle8.Format = "G7";
            dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewTextBoxColumn10.HeaderText = "F_inv";
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            dataGridViewTextBoxColumn10.ReadOnly = true;
            dataGridViewTextBoxColumn10.Width = 70;
            // 
            // F
            // 
            F.DataPropertyName = "F";
            dataGridViewCellStyle9.Format = "G7";
            F.DefaultCellStyle = dataGridViewCellStyle9;
            F.HeaderText = "|F|";
            F.Name = "F";
            F.ReadOnly = true;
            F.Width = 70;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewTextBoxColumn11.DataPropertyName = "F2";
            dataGridViewCellStyle10.Format = "G7";
            dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle10;
            dataGridViewTextBoxColumn11.HeaderText = "F^2";
            dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            dataGridViewTextBoxColumn11.ReadOnly = true;
            dataGridViewTextBoxColumn11.Width = 70;
            // 
            // RelInt
            // 
            RelInt.DataPropertyName = "RelInt";
            dataGridViewCellStyle11.Format = "G7";
            RelInt.DefaultCellStyle = dataGridViewCellStyle11;
            RelInt.HeaderText = "Rel. Int. (%)";
            RelInt.Name = "RelInt";
            RelInt.ReadOnly = true;
            RelInt.Width = 90;
            // 
            // Condition
            // 
            Condition.DataPropertyName = "Condition";
            Condition.HeaderText = "Condition";
            Condition.Name = "Condition";
            Condition.ReadOnly = true;
            Condition.Width = 80;
            // 
            // bindingSourceScatteringFactor
            // 
            bindingSourceScatteringFactor.DataMember = "DataTableScatteringFactor";
            bindingSourceScatteringFactor.DataSource = dataSet;
            // 
            // dataSet
            // 
            dataSet.DataSetName = "DataSet";
            dataSet.Namespace = "http://tempuri.org/DataSet1.xsd";
            dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // numericUpDownThresholdD
            // 
            numericUpDownThresholdD.DecimalPlaces = 4;
            numericUpDownThresholdD.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericUpDownThresholdD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownThresholdD.Location = new System.Drawing.Point(505, 36);
            numericUpDownThresholdD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            numericUpDownThresholdD.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownThresholdD.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownThresholdD.Name = "numericUpDownThresholdD";
            numericUpDownThresholdD.Size = new System.Drawing.Size(61, 25);
            numericUpDownThresholdD.TabIndex = 5;
            numericUpDownThresholdD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownThresholdD.ValueChanged += numericUpDownThresholdD_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label2.Location = new System.Drawing.Point(440, 14);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(144, 17);
            label2.TabIndex = 6;
            label2.Text = "Threshold of d-spacing";
            // 
            // checkBoxHideProhibitedPlanes
            // 
            checkBoxHideProhibitedPlanes.AutoSize = true;
            checkBoxHideProhibitedPlanes.Checked = true;
            checkBoxHideProhibitedPlanes.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideProhibitedPlanes.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            checkBoxHideProhibitedPlanes.Location = new System.Drawing.Point(239, 78);
            checkBoxHideProhibitedPlanes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            checkBoxHideProhibitedPlanes.Name = "checkBoxHideProhibitedPlanes";
            checkBoxHideProhibitedPlanes.Size = new System.Drawing.Size(161, 21);
            checkBoxHideProhibitedPlanes.TabIndex = 8;
            checkBoxHideProhibitedPlanes.Text = "Hide prohibited planes";
            checkBoxHideProhibitedPlanes.CheckedChanged += numericUpDownThresholdD_ValueChanged;
            // 
            // checkBoxHideEquivalentPlane
            // 
            checkBoxHideEquivalentPlane.AutoSize = true;
            checkBoxHideEquivalentPlane.Checked = true;
            checkBoxHideEquivalentPlane.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideEquivalentPlane.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            checkBoxHideEquivalentPlane.Location = new System.Drawing.Point(239, 53);
            checkBoxHideEquivalentPlane.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            checkBoxHideEquivalentPlane.Name = "checkBoxHideEquivalentPlane";
            checkBoxHideEquivalentPlane.Size = new System.Drawing.Size(159, 21);
            checkBoxHideEquivalentPlane.TabIndex = 8;
            checkBoxHideEquivalentPlane.Text = "Hide equivalent planes";
            checkBoxHideEquivalentPlane.CheckedChanged += numericUpDownThresholdD_ValueChanged;
            // 
            // label13
            // 
            label13.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label13.Location = new System.Drawing.Point(567, 40);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(28, 20);
            label13.TabIndex = 6;
            label13.Text = "Å";
            // 
            // buttonCopyClipboard
            // 
            buttonCopyClipboard.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            buttonCopyClipboard.Location = new System.Drawing.Point(440, 78);
            buttonCopyClipboard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            buttonCopyClipboard.Name = "buttonCopyClipboard";
            buttonCopyClipboard.Size = new System.Drawing.Size(141, 28);
            buttonCopyClipboard.TabIndex = 8;
            buttonCopyClipboard.Text = "Copy to Clipboard";
            buttonCopyClipboard.UseVisualStyleBackColor = true;
            buttonCopyClipboard.Click += buttonCopyClipBoard_Click;
            // 
            // label18
            // 
            label18.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label18.Location = new System.Drawing.Point(487, 38);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(17, 20);
            label18.TabIndex = 6;
            label18.Text = "<";
            // 
            // checkBoxBragBrentano
            // 
            checkBoxBragBrentano.AutoSize = true;
            checkBoxBragBrentano.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            checkBoxBragBrentano.Location = new System.Drawing.Point(238, 1);
            checkBoxBragBrentano.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            checkBoxBragBrentano.Name = "checkBoxBragBrentano";
            checkBoxBragBrentano.Size = new System.Drawing.Size(199, 38);
            checkBoxBragBrentano.TabIndex = 8;
            checkBoxBragBrentano.Text = "Powder diffraction intensities \r\n(Bragg Brentano optics)";
            checkBoxBragBrentano.CheckedChanged += checkBoxBragBrentano_CheckedChanged;
            // 
            // waveLengthControl1
            // 
            waveLengthControl1.AutoSize = true;
            waveLengthControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            waveLengthControl1.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl1.Energy = 8.04114721D;
            waveLengthControl1.Font = new System.Drawing.Font("Arial", 9F);
            waveLengthControl1.Location = new System.Drawing.Point(10, 2);
            waveLengthControl1.Margin = new System.Windows.Forms.Padding(0);
            waveLengthControl1.MaximumSize = new System.Drawing.Size(500, 500);
            waveLengthControl1.MinimumSize = new System.Drawing.Size(200, 0);
            waveLengthControl1.Monochrome = true;
            waveLengthControl1.Name = "waveLengthControl1";
            waveLengthControl1.ShowWaveSource = true;
            waveLengthControl1.Size = new System.Drawing.Size(200, 103);
            waveLengthControl1.TabIndex = 9;
            waveLengthControl1.TextFont = new System.Drawing.Font("メイリオ", 9F);
            waveLengthControl1.WaveLength = 0.1541871066667D;
            waveLengthControl1.WaveSource = WaveSource.Xray;
            waveLengthControl1.XrayWaveSourceElementNumber = 29;
            waveLengthControl1.XrayWaveSourceLine = XrayLine.Ka;
            waveLengthControl1.WavelengthChanged += waveLengthControl1_WavelengthChanged;
            // 
            // checkBoxTest
            // 
            checkBoxTest.AutoSize = true;
            checkBoxTest.Location = new System.Drawing.Point(600, 2);
            checkBoxTest.Name = "checkBoxTest";
            checkBoxTest.Size = new System.Drawing.Size(48, 21);
            checkBoxTest.TabIndex = 10;
            checkBoxTest.Text = "test";
            checkBoxTest.UseVisualStyleBackColor = true;
            checkBoxTest.CheckedChanged += checkBoxTest_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label1.Location = new System.Drawing.Point(30, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(30, 17);
            label1.TabIndex = 6;
            label1.Text = "Min";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label3.Location = new System.Drawing.Point(80, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(33, 17);
            label3.TabIndex = 6;
            label3.Text = "Max";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label4.Location = new System.Drawing.Point(129, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(34, 17);
            label4.TabIndex = 6;
            label4.Text = "Step";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label5.Location = new System.Drawing.Point(9, 22);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(15, 17);
            label5.TabIndex = 6;
            label5.Text = "h";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label6.Location = new System.Drawing.Point(9, 47);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(14, 17);
            label6.TabIndex = 6;
            label6.Text = "k";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            label7.Location = new System.Drawing.Point(9, 72);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(11, 17);
            label7.TabIndex = 6;
            label7.Text = "l";
            // 
            // panel1
            // 
            panel1.Controls.Add(numericBoxL_step);
            panel1.Controls.Add(numericBoxK_step);
            panel1.Controls.Add(numericBoxH_step);
            panel1.Controls.Add(numericBoxL_max);
            panel1.Controls.Add(numericBoxK_max);
            panel1.Controls.Add(numericBoxL_min);
            panel1.Controls.Add(numericBoxK_min);
            panel1.Controls.Add(numericBoxH_max);
            panel1.Controls.Add(numericBoxH_min);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(label3);
            panel1.Location = new System.Drawing.Point(617, 8);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(202, 97);
            panel1.TabIndex = 12;
            panel1.Visible = false;
            // 
            // numericBoxL_step
            // 
            numericBoxL_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxL_step.DecimalPlaces = 3;
            numericBoxL_step.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxL_step.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxL_step.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxL_step.Location = new System.Drawing.Point(131, 69);
            numericBoxL_step.Margin = new System.Windows.Forms.Padding(0);
            numericBoxL_step.Maximum = 1D;
            numericBoxL_step.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxL_step.Minimum = 0.01D;
            numericBoxL_step.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxL_step.Name = "numericBoxL_step";
            numericBoxL_step.RadianValue = 0.00017453292519943296D;
            numericBoxL_step.RoundErrorAccuracy = -1;
            numericBoxL_step.ShowUpDown = true;
            numericBoxL_step.Size = new System.Drawing.Size(68, 24);
            numericBoxL_step.TabIndex = 7;
            numericBoxL_step.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxL_step.Value = 0.01D;
            numericBoxL_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_step
            // 
            numericBoxK_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_step.DecimalPlaces = 3;
            numericBoxK_step.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxK_step.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxK_step.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxK_step.Location = new System.Drawing.Point(131, 45);
            numericBoxK_step.Margin = new System.Windows.Forms.Padding(0);
            numericBoxK_step.Maximum = 1D;
            numericBoxK_step.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxK_step.Minimum = 0.01D;
            numericBoxK_step.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxK_step.Name = "numericBoxK_step";
            numericBoxK_step.RadianValue = 0.00017453292519943296D;
            numericBoxK_step.RoundErrorAccuracy = -1;
            numericBoxK_step.ShowUpDown = true;
            numericBoxK_step.Size = new System.Drawing.Size(68, 24);
            numericBoxK_step.TabIndex = 7;
            numericBoxK_step.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxK_step.Value = 0.01D;
            numericBoxK_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_step
            // 
            numericBoxH_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_step.DecimalPlaces = 3;
            numericBoxH_step.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxH_step.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxH_step.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxH_step.Location = new System.Drawing.Point(131, 20);
            numericBoxH_step.Margin = new System.Windows.Forms.Padding(0);
            numericBoxH_step.Maximum = 1D;
            numericBoxH_step.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxH_step.Minimum = 0.01D;
            numericBoxH_step.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxH_step.Name = "numericBoxH_step";
            numericBoxH_step.RadianValue = 0.00017453292519943296D;
            numericBoxH_step.RoundErrorAccuracy = -1;
            numericBoxH_step.ShowUpDown = true;
            numericBoxH_step.Size = new System.Drawing.Size(68, 24);
            numericBoxH_step.TabIndex = 7;
            numericBoxH_step.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxH_step.Value = 0.01D;
            numericBoxH_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_max
            // 
            numericBoxL_max.BackColor = System.Drawing.Color.Transparent;
            numericBoxL_max.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxL_max.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxL_max.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxL_max.Location = new System.Drawing.Point(80, 69);
            numericBoxL_max.Margin = new System.Windows.Forms.Padding(0);
            numericBoxL_max.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxL_max.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxL_max.Name = "numericBoxL_max";
            numericBoxL_max.RadianValue = 0.017453292519943295D;
            numericBoxL_max.RoundErrorAccuracy = -1;
            numericBoxL_max.ShowUpDown = true;
            numericBoxL_max.Size = new System.Drawing.Size(40, 24);
            numericBoxL_max.TabIndex = 7;
            numericBoxL_max.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxL_max.Value = 1D;
            numericBoxL_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_max
            // 
            numericBoxK_max.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_max.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxK_max.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxK_max.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxK_max.Location = new System.Drawing.Point(80, 45);
            numericBoxK_max.Margin = new System.Windows.Forms.Padding(0);
            numericBoxK_max.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxK_max.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxK_max.Name = "numericBoxK_max";
            numericBoxK_max.RoundErrorAccuracy = -1;
            numericBoxK_max.ShowUpDown = true;
            numericBoxK_max.Size = new System.Drawing.Size(40, 24);
            numericBoxK_max.TabIndex = 7;
            numericBoxK_max.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxK_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_min
            // 
            numericBoxL_min.BackColor = System.Drawing.Color.Transparent;
            numericBoxL_min.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxL_min.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxL_min.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxL_min.Location = new System.Drawing.Point(29, 69);
            numericBoxL_min.Margin = new System.Windows.Forms.Padding(0);
            numericBoxL_min.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxL_min.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxL_min.Name = "numericBoxL_min";
            numericBoxL_min.RoundErrorAccuracy = -1;
            numericBoxL_min.ShowUpDown = true;
            numericBoxL_min.Size = new System.Drawing.Size(40, 24);
            numericBoxL_min.TabIndex = 7;
            numericBoxL_min.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxL_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_min
            // 
            numericBoxK_min.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_min.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxK_min.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxK_min.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxK_min.Location = new System.Drawing.Point(29, 45);
            numericBoxK_min.Margin = new System.Windows.Forms.Padding(0);
            numericBoxK_min.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxK_min.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxK_min.Name = "numericBoxK_min";
            numericBoxK_min.RoundErrorAccuracy = -1;
            numericBoxK_min.ShowUpDown = true;
            numericBoxK_min.Size = new System.Drawing.Size(40, 24);
            numericBoxK_min.TabIndex = 7;
            numericBoxK_min.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxK_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_max
            // 
            numericBoxH_max.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_max.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxH_max.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxH_max.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxH_max.Location = new System.Drawing.Point(80, 20);
            numericBoxH_max.Margin = new System.Windows.Forms.Padding(0);
            numericBoxH_max.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxH_max.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxH_max.Name = "numericBoxH_max";
            numericBoxH_max.RoundErrorAccuracy = -1;
            numericBoxH_max.ShowUpDown = true;
            numericBoxH_max.Size = new System.Drawing.Size(40, 24);
            numericBoxH_max.TabIndex = 7;
            numericBoxH_max.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxH_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_min
            // 
            numericBoxH_min.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_min.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            numericBoxH_min.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxH_min.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxH_min.Location = new System.Drawing.Point(29, 20);
            numericBoxH_min.Margin = new System.Windows.Forms.Padding(0);
            numericBoxH_min.MaximumSize = new System.Drawing.Size(1000, 28);
            numericBoxH_min.MinimumSize = new System.Drawing.Size(1, 18);
            numericBoxH_min.Name = "numericBoxH_min";
            numericBoxH_min.RoundErrorAccuracy = -1;
            numericBoxH_min.ShowUpDown = true;
            numericBoxH_min.Size = new System.Drawing.Size(40, 24);
            numericBoxH_min.TabIndex = 7;
            numericBoxH_min.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            numericBoxH_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // FormScatteringFactor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(831, 398);
            Controls.Add(checkBoxTest);
            Controls.Add(waveLengthControl1);
            Controls.Add(buttonCopyClipboard);
            Controls.Add(dataGridView2);
            Controls.Add(label2);
            Controls.Add(label18);
            Controls.Add(numericUpDownThresholdD);
            Controls.Add(label13);
            Controls.Add(checkBoxHideProhibitedPlanes);
            Controls.Add(checkBoxBragBrentano);
            Controls.Add(checkBoxHideEquivalentPlane);
            Controls.Add(panel1);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "FormScatteringFactor";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Text = "Scattering Factor";
            FormClosing += FormCrystallographicInformation_FormClosing;
            Load += FormCrystallographicInformation_Load;
            VisibleChanged += FormScatteringFactor_VisibleChanged;
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceScatteringFactor).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownThresholdD).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox checkBoxTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private NumericBox numericBoxH_min;
        private NumericBox numericBoxL_step;
        private NumericBox numericBoxK_step;
        private NumericBox numericBoxH_step;
        private NumericBox numericBoxL_max;
        private NumericBox numericBoxK_max;
        private NumericBox numericBoxL_min;
        private NumericBox numericBoxK_min;
        private NumericBox numericBoxH_max;
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
    }
}