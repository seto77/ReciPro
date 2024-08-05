﻿namespace Crystallography.Controls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScatteringFactor));
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
            resources.ApplyResources(dataGridView2, "dataGridView2");
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            dataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dataGridViewTextBoxColumn1, K, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7, dataGridViewTextBoxColumn9, dataGridViewTextBoxColumn10, F, dataGridViewTextBoxColumn11, RelInt, Condition });
            dataGridView2.DataSource = bindingSourceScatteringFactor;
            dataGridView2.MultiSelect = false;
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView2.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridView2.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dataGridView2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            dataGridView2.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridView2.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridView2.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridView2.RowTemplate.Height = 21;
            dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            toolTip.SetToolTip(dataGridView2, resources.GetString("dataGridView2.ToolTip"));
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "H";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // K
            // 
            K.DataPropertyName = "K";
            resources.ApplyResources(K, "K");
            K.Name = "K";
            K.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.DataPropertyName = "L";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.DataPropertyName = "Multi";
            resources.ApplyResources(dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.DataPropertyName = "D";
            dataGridViewCellStyle4.Format = "G7";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.DataPropertyName = "Q";
            dataGridViewCellStyle5.Format = "G7";
            dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.DataPropertyName = "TwoTheta";
            dataGridViewCellStyle6.Format = "G7";
            dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.DataPropertyName = "F_real";
            dataGridViewCellStyle7.Format = "G7";
            dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            dataGridViewTextBoxColumn9.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.DataPropertyName = "F_inv";
            dataGridViewCellStyle8.Format = "G7";
            dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // F
            // 
            F.DataPropertyName = "F";
            dataGridViewCellStyle9.Format = "G7";
            F.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(F, "F");
            F.Name = "F";
            F.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewTextBoxColumn11.DataPropertyName = "F2";
            dataGridViewCellStyle10.Format = "G7";
            dataGridViewTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
            dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // RelInt
            // 
            RelInt.DataPropertyName = "RelInt";
            dataGridViewCellStyle11.Format = "G7";
            RelInt.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(RelInt, "RelInt");
            RelInt.Name = "RelInt";
            RelInt.ReadOnly = true;
            // 
            // Condition
            // 
            Condition.DataPropertyName = "Condition";
            resources.ApplyResources(Condition, "Condition");
            Condition.Name = "Condition";
            Condition.ReadOnly = true;
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
            resources.ApplyResources(numericUpDownThresholdD, "numericUpDownThresholdD");
            numericUpDownThresholdD.DecimalPlaces = 4;
            numericUpDownThresholdD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownThresholdD.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownThresholdD.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownThresholdD.Name = "numericUpDownThresholdD";
            toolTip.SetToolTip(numericUpDownThresholdD, resources.GetString("numericUpDownThresholdD.ToolTip"));
            numericUpDownThresholdD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownThresholdD.ValueChanged += numericUpDownThresholdD_ValueChanged;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // checkBoxHideProhibitedPlanes
            // 
            resources.ApplyResources(checkBoxHideProhibitedPlanes, "checkBoxHideProhibitedPlanes");
            checkBoxHideProhibitedPlanes.Checked = true;
            checkBoxHideProhibitedPlanes.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideProhibitedPlanes.Name = "checkBoxHideProhibitedPlanes";
            toolTip.SetToolTip(checkBoxHideProhibitedPlanes, resources.GetString("checkBoxHideProhibitedPlanes.ToolTip"));
            checkBoxHideProhibitedPlanes.CheckedChanged += numericUpDownThresholdD_ValueChanged;
            // 
            // checkBoxHideEquivalentPlane
            // 
            resources.ApplyResources(checkBoxHideEquivalentPlane, "checkBoxHideEquivalentPlane");
            checkBoxHideEquivalentPlane.Checked = true;
            checkBoxHideEquivalentPlane.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideEquivalentPlane.Name = "checkBoxHideEquivalentPlane";
            toolTip.SetToolTip(checkBoxHideEquivalentPlane, resources.GetString("checkBoxHideEquivalentPlane.ToolTip"));
            checkBoxHideEquivalentPlane.CheckedChanged += numericUpDownThresholdD_ValueChanged;
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip"));
            // 
            // buttonCopyClipboard
            // 
            resources.ApplyResources(buttonCopyClipboard, "buttonCopyClipboard");
            buttonCopyClipboard.Name = "buttonCopyClipboard";
            toolTip.SetToolTip(buttonCopyClipboard, resources.GetString("buttonCopyClipboard.ToolTip"));
            buttonCopyClipboard.UseVisualStyleBackColor = true;
            buttonCopyClipboard.Click += buttonCopyClipBoard_Click;
            // 
            // label18
            // 
            resources.ApplyResources(label18, "label18");
            label18.Name = "label18";
            toolTip.SetToolTip(label18, resources.GetString("label18.ToolTip"));
            // 
            // checkBoxBragBrentano
            // 
            resources.ApplyResources(checkBoxBragBrentano, "checkBoxBragBrentano");
            checkBoxBragBrentano.Name = "checkBoxBragBrentano";
            toolTip.SetToolTip(checkBoxBragBrentano, resources.GetString("checkBoxBragBrentano.ToolTip"));
            checkBoxBragBrentano.CheckedChanged += checkBoxBragBrentano_CheckedChanged;
            // 
            // waveLengthControl1
            // 
            resources.ApplyResources(waveLengthControl1, "waveLengthControl1");
            waveLengthControl1.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl1.Energy = 8.04114721D;
            waveLengthControl1.Monochrome = true;
            waveLengthControl1.Name = "waveLengthControl1";
            waveLengthControl1.ShowWaveSource = true;
            toolTip.SetToolTip(waveLengthControl1, resources.GetString("waveLengthControl1.ToolTip"));
            waveLengthControl1.WaveLength = 0.1541871066667D;
            waveLengthControl1.WaveSource = WaveSource.Xray;
            waveLengthControl1.XrayWaveSourceElementNumber = 29;
            waveLengthControl1.XrayWaveSourceLine = XrayLine.Ka;
            waveLengthControl1.WavelengthChanged += waveLengthControl1_WavelengthChanged;
            // 
            // checkBoxTest
            // 
            resources.ApplyResources(checkBoxTest, "checkBoxTest");
            checkBoxTest.Name = "checkBoxTest";
            toolTip.SetToolTip(checkBoxTest, resources.GetString("checkBoxTest.ToolTip"));
            checkBoxTest.UseVisualStyleBackColor = true;
            checkBoxTest.CheckedChanged += checkBoxTest_CheckedChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
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
            panel1.Name = "panel1";
            toolTip.SetToolTip(panel1, resources.GetString("panel1.ToolTip"));
            // 
            // numericBoxL_step
            // 
            resources.ApplyResources(numericBoxL_step, "numericBoxL_step");
            numericBoxL_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxL_step.DecimalPlaces = 3;
            numericBoxL_step.Maximum = 1D;
            numericBoxL_step.Minimum = 0.001D;
            numericBoxL_step.Name = "numericBoxL_step";
            numericBoxL_step.RadianValue = 0.00017453292519943296D;
            numericBoxL_step.RoundErrorAccuracy = -1;
            numericBoxL_step.ShowUpDown = true;
            numericBoxL_step.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxL_step, resources.GetString("numericBoxL_step.ToolTip"));
            numericBoxL_step.Value = 0.01D;
            numericBoxL_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_step
            // 
            resources.ApplyResources(numericBoxK_step, "numericBoxK_step");
            numericBoxK_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_step.DecimalPlaces = 3;
            numericBoxK_step.Maximum = 1D;
            numericBoxK_step.Minimum = 0.001D;
            numericBoxK_step.Name = "numericBoxK_step";
            numericBoxK_step.RadianValue = 0.00017453292519943296D;
            numericBoxK_step.RoundErrorAccuracy = -1;
            numericBoxK_step.ShowUpDown = true;
            numericBoxK_step.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxK_step, resources.GetString("numericBoxK_step.ToolTip"));
            numericBoxK_step.Value = 0.01D;
            numericBoxK_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_step
            // 
            resources.ApplyResources(numericBoxH_step, "numericBoxH_step");
            numericBoxH_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_step.DecimalPlaces = 3;
            numericBoxH_step.Maximum = 1D;
            numericBoxH_step.Minimum = 0.001D;
            numericBoxH_step.Name = "numericBoxH_step";
            numericBoxH_step.RadianValue = 0.00017453292519943296D;
            numericBoxH_step.RoundErrorAccuracy = -1;
            numericBoxH_step.ShowUpDown = true;
            numericBoxH_step.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxH_step, resources.GetString("numericBoxH_step.ToolTip"));
            numericBoxH_step.Value = 0.01D;
            numericBoxH_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_max
            // 
            resources.ApplyResources(numericBoxL_max, "numericBoxL_max");
            numericBoxL_max.BackColor = System.Drawing.Color.Transparent;
            numericBoxL_max.Name = "numericBoxL_max";
            numericBoxL_max.RadianValue = 0.017453292519943295D;
            numericBoxL_max.RoundErrorAccuracy = -1;
            numericBoxL_max.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxL_max, resources.GetString("numericBoxL_max.ToolTip"));
            numericBoxL_max.Value = 1D;
            numericBoxL_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_max
            // 
            resources.ApplyResources(numericBoxK_max, "numericBoxK_max");
            numericBoxK_max.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_max.Name = "numericBoxK_max";
            numericBoxK_max.RoundErrorAccuracy = -1;
            numericBoxK_max.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxK_max, resources.GetString("numericBoxK_max.ToolTip"));
            numericBoxK_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_min
            // 
            resources.ApplyResources(numericBoxL_min, "numericBoxL_min");
            numericBoxL_min.BackColor = System.Drawing.Color.Transparent;
            numericBoxL_min.Name = "numericBoxL_min";
            numericBoxL_min.RoundErrorAccuracy = -1;
            numericBoxL_min.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxL_min, resources.GetString("numericBoxL_min.ToolTip"));
            numericBoxL_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_min
            // 
            resources.ApplyResources(numericBoxK_min, "numericBoxK_min");
            numericBoxK_min.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_min.Name = "numericBoxK_min";
            numericBoxK_min.RoundErrorAccuracy = -1;
            numericBoxK_min.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxK_min, resources.GetString("numericBoxK_min.ToolTip"));
            numericBoxK_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_max
            // 
            resources.ApplyResources(numericBoxH_max, "numericBoxH_max");
            numericBoxH_max.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_max.Name = "numericBoxH_max";
            numericBoxH_max.RoundErrorAccuracy = -1;
            numericBoxH_max.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxH_max, resources.GetString("numericBoxH_max.ToolTip"));
            numericBoxH_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_min
            // 
            resources.ApplyResources(numericBoxH_min, "numericBoxH_min");
            numericBoxH_min.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_min.Name = "numericBoxH_min";
            numericBoxH_min.RoundErrorAccuracy = -1;
            numericBoxH_min.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxH_min, resources.GetString("numericBoxH_min.ToolTip"));
            numericBoxH_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // FormScatteringFactor
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
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
            Name = "FormScatteringFactor";
            ShowIcon = false;
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
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