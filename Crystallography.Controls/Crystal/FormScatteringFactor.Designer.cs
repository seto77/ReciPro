namespace Crystallography.Controls
{
    partial class FormScatteringFactor
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
            components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScatteringFactor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridView = new System.Windows.Forms.DataGridView();
            dataGridViewTextBoxColumnH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnMulti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnTwoTheta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnFreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnFinv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnFabs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnFsq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnIntPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnIntCondition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bindingSourceScatteringFactor = new System.Windows.Forms.BindingSource(components);
            dataSet = new DataSet();
            label2 = new System.Windows.Forms.Label();
            checkBoxHideProhibitedPlanes = new System.Windows.Forms.CheckBox();
            checkBoxHideEquivalentPlane = new System.Windows.Forms.CheckBox();
            buttonCopyClipboard = new System.Windows.Forms.Button();
            checkBoxBragBrentano = new System.Windows.Forms.CheckBox();
            waveLengthControl1 = new WaveLengthControl();
            toolTip = new System.Windows.Forms.ToolTip(components);
            label8 = new System.Windows.Forms.Label();
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
            panel2 = new System.Windows.Forms.Panel();
            radioButtonNanoMeter = new System.Windows.Forms.RadioButton();
            radioButtonAngstrom = new System.Windows.Forms.RadioButton();
            panel3 = new System.Windows.Forms.Panel();
            numericBoxCutoffD = new NumericBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceScatteringFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Variable Text", 9.75F);
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dataGridViewTextBoxColumnH, dataGridViewTextBoxColumnK, dataGridViewTextBoxColumnI, dataGridViewTextBoxColumnL, dataGridViewTextBoxColumnMulti, dataGridViewTextBoxColumnD, dataGridViewTextBoxColumnQ, dataGridViewTextBoxColumnTwoTheta, dataGridViewTextBoxColumnFreal, dataGridViewTextBoxColumnFinv, dataGridViewTextBoxColumnFabs, dataGridViewTextBoxColumnFsq, dataGridViewTextBoxColumnIntPercent, dataGridViewTextBoxColumnIntCondition });
            dataGridView.DataSource = bindingSourceScatteringFactor;
            resources.ApplyResources(dataGridView, "dataGridView");
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("BIZ UDPGothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 128);
            dataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            // 
            // dataGridViewTextBoxColumnH
            // 
            dataGridViewTextBoxColumnH.DataPropertyName = "H";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewTextBoxColumnH.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(dataGridViewTextBoxColumnH, "dataGridViewTextBoxColumnH");
            dataGridViewTextBoxColumnH.Name = "dataGridViewTextBoxColumnH";
            dataGridViewTextBoxColumnH.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnK
            // 
            dataGridViewTextBoxColumnK.DataPropertyName = "K";
            resources.ApplyResources(dataGridViewTextBoxColumnK, "dataGridViewTextBoxColumnK");
            dataGridViewTextBoxColumnK.Name = "dataGridViewTextBoxColumnK";
            dataGridViewTextBoxColumnK.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnI
            // 
            resources.ApplyResources(dataGridViewTextBoxColumnI, "dataGridViewTextBoxColumnI");
            dataGridViewTextBoxColumnI.Name = "dataGridViewTextBoxColumnI";
            dataGridViewTextBoxColumnI.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnL
            // 
            dataGridViewTextBoxColumnL.DataPropertyName = "L";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewTextBoxColumnL.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(dataGridViewTextBoxColumnL, "dataGridViewTextBoxColumnL");
            dataGridViewTextBoxColumnL.Name = "dataGridViewTextBoxColumnL";
            dataGridViewTextBoxColumnL.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnMulti
            // 
            dataGridViewTextBoxColumnMulti.DataPropertyName = "Multi";
            resources.ApplyResources(dataGridViewTextBoxColumnMulti, "dataGridViewTextBoxColumnMulti");
            dataGridViewTextBoxColumnMulti.Name = "dataGridViewTextBoxColumnMulti";
            dataGridViewTextBoxColumnMulti.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnD
            // 
            dataGridViewTextBoxColumnD.DataPropertyName = "D";
            dataGridViewCellStyle5.Format = "G7";
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewTextBoxColumnD.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(dataGridViewTextBoxColumnD, "dataGridViewTextBoxColumnD");
            dataGridViewTextBoxColumnD.Name = "dataGridViewTextBoxColumnD";
            dataGridViewTextBoxColumnD.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnQ
            // 
            dataGridViewTextBoxColumnQ.DataPropertyName = "Q";
            dataGridViewCellStyle6.Format = "G7";
            dataGridViewTextBoxColumnQ.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(dataGridViewTextBoxColumnQ, "dataGridViewTextBoxColumnQ");
            dataGridViewTextBoxColumnQ.Name = "dataGridViewTextBoxColumnQ";
            dataGridViewTextBoxColumnQ.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnTwoTheta
            // 
            dataGridViewTextBoxColumnTwoTheta.DataPropertyName = "TwoTheta";
            dataGridViewCellStyle7.Format = "G7";
            dataGridViewTextBoxColumnTwoTheta.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(dataGridViewTextBoxColumnTwoTheta, "dataGridViewTextBoxColumnTwoTheta");
            dataGridViewTextBoxColumnTwoTheta.Name = "dataGridViewTextBoxColumnTwoTheta";
            dataGridViewTextBoxColumnTwoTheta.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnFreal
            // 
            dataGridViewTextBoxColumnFreal.DataPropertyName = "F_real";
            dataGridViewCellStyle8.Format = "G7";
            dataGridViewTextBoxColumnFreal.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(dataGridViewTextBoxColumnFreal, "dataGridViewTextBoxColumnFreal");
            dataGridViewTextBoxColumnFreal.Name = "dataGridViewTextBoxColumnFreal";
            dataGridViewTextBoxColumnFreal.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnFinv
            // 
            dataGridViewTextBoxColumnFinv.DataPropertyName = "F_inv";
            dataGridViewCellStyle9.Format = "G7";
            dataGridViewTextBoxColumnFinv.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(dataGridViewTextBoxColumnFinv, "dataGridViewTextBoxColumnFinv");
            dataGridViewTextBoxColumnFinv.Name = "dataGridViewTextBoxColumnFinv";
            dataGridViewTextBoxColumnFinv.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnFabs
            // 
            dataGridViewTextBoxColumnFabs.DataPropertyName = "F";
            dataGridViewCellStyle10.Format = "G7";
            dataGridViewTextBoxColumnFabs.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(dataGridViewTextBoxColumnFabs, "dataGridViewTextBoxColumnFabs");
            dataGridViewTextBoxColumnFabs.Name = "dataGridViewTextBoxColumnFabs";
            dataGridViewTextBoxColumnFabs.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnFsq
            // 
            dataGridViewTextBoxColumnFsq.DataPropertyName = "F2";
            dataGridViewCellStyle11.Format = "G7";
            dataGridViewTextBoxColumnFsq.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(dataGridViewTextBoxColumnFsq, "dataGridViewTextBoxColumnFsq");
            dataGridViewTextBoxColumnFsq.Name = "dataGridViewTextBoxColumnFsq";
            dataGridViewTextBoxColumnFsq.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnIntPercent
            // 
            dataGridViewTextBoxColumnIntPercent.DataPropertyName = "RelInt";
            dataGridViewCellStyle12.Format = "G7";
            dataGridViewTextBoxColumnIntPercent.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(dataGridViewTextBoxColumnIntPercent, "dataGridViewTextBoxColumnIntPercent");
            dataGridViewTextBoxColumnIntPercent.Name = "dataGridViewTextBoxColumnIntPercent";
            dataGridViewTextBoxColumnIntPercent.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnIntCondition
            // 
            dataGridViewTextBoxColumnIntCondition.DataPropertyName = "Condition";
            resources.ApplyResources(dataGridViewTextBoxColumnIntCondition, "dataGridViewTextBoxColumnIntCondition");
            dataGridViewTextBoxColumnIntCondition.Name = "dataGridViewTextBoxColumnIntCondition";
            dataGridViewTextBoxColumnIntCondition.ReadOnly = true;
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
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // checkBoxHideProhibitedPlanes
            // 
            resources.ApplyResources(checkBoxHideProhibitedPlanes, "checkBoxHideProhibitedPlanes");
            checkBoxHideProhibitedPlanes.Checked = true;
            checkBoxHideProhibitedPlanes.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideProhibitedPlanes.Name = "checkBoxHideProhibitedPlanes";
            checkBoxHideProhibitedPlanes.CheckedChanged += numericBoxCutoffD_ValueChanged;
            // 
            // checkBoxHideEquivalentPlane
            // 
            resources.ApplyResources(checkBoxHideEquivalentPlane, "checkBoxHideEquivalentPlane");
            checkBoxHideEquivalentPlane.Checked = true;
            checkBoxHideEquivalentPlane.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideEquivalentPlane.Name = "checkBoxHideEquivalentPlane";
            checkBoxHideEquivalentPlane.CheckedChanged += numericBoxCutoffD_ValueChanged;
            // 
            // buttonCopyClipboard
            // 
            resources.ApplyResources(buttonCopyClipboard, "buttonCopyClipboard");
            buttonCopyClipboard.Name = "buttonCopyClipboard";
            buttonCopyClipboard.UseVisualStyleBackColor = true;
            buttonCopyClipboard.Click += buttonCopyClipboard_Click;
            // 
            // checkBoxBragBrentano
            // 
            resources.ApplyResources(checkBoxBragBrentano, "checkBoxBragBrentano");
            checkBoxBragBrentano.Name = "checkBoxBragBrentano";
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
            waveLengthControl1.WaveLength = 0.1541871066667D;
            waveLengthControl1.WaveSource = WaveSource.Xray;
            waveLengthControl1.XrayWaveSourceElementNumber = 29;
            waveLengthControl1.XrayWaveSourceLine = XrayLine.Ka;
            waveLengthControl1.WavelengthChanged += waveLengthControl1_WavelengthChanged;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            toolTip.SetToolTip(label8, resources.GetString("label8.ToolTip"));
            // 
            // checkBoxTest
            // 
            resources.ApplyResources(checkBoxTest, "checkBoxTest");
            checkBoxTest.Name = "checkBoxTest";
            checkBoxTest.UseVisualStyleBackColor = true;
            checkBoxTest.CheckedChanged += checkBoxTest_CheckedChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
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
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // numericBoxL_step
            // 
            numericBoxL_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxL_step.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxL_step, "numericBoxL_step");
            numericBoxL_step.Maximum = 1D;
            numericBoxL_step.Minimum = 0.001D;
            numericBoxL_step.Name = "numericBoxL_step";
            numericBoxL_step.RadianValue = 0.00017453292519943296D;
            numericBoxL_step.ShowUpDown = true;
            numericBoxL_step.SmartIncrement = true;
            numericBoxL_step.Value = 0.01D;
            numericBoxL_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_step
            // 
            numericBoxK_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_step.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxK_step, "numericBoxK_step");
            numericBoxK_step.Maximum = 1D;
            numericBoxK_step.Minimum = 0.001D;
            numericBoxK_step.Name = "numericBoxK_step";
            numericBoxK_step.RadianValue = 0.00017453292519943296D;
            numericBoxK_step.ShowUpDown = true;
            numericBoxK_step.SmartIncrement = true;
            numericBoxK_step.Value = 0.01D;
            numericBoxK_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_step
            // 
            numericBoxH_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_step.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxH_step, "numericBoxH_step");
            numericBoxH_step.Maximum = 1D;
            numericBoxH_step.Minimum = 0.001D;
            numericBoxH_step.Name = "numericBoxH_step";
            numericBoxH_step.RadianValue = 0.00017453292519943296D;
            numericBoxH_step.ShowUpDown = true;
            numericBoxH_step.SmartIncrement = true;
            numericBoxH_step.Value = 0.01D;
            numericBoxH_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_max
            // 
            numericBoxL_max.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxL_max, "numericBoxL_max");
            numericBoxL_max.Name = "numericBoxL_max";
            numericBoxL_max.RadianValue = 0.017453292519943295D;
            numericBoxL_max.ShowUpDown = true;
            numericBoxL_max.Value = 1D;
            numericBoxL_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_max
            // 
            numericBoxK_max.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxK_max, "numericBoxK_max");
            numericBoxK_max.Name = "numericBoxK_max";
            numericBoxK_max.ShowUpDown = true;
            numericBoxK_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_min
            // 
            numericBoxL_min.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxL_min, "numericBoxL_min");
            numericBoxL_min.Name = "numericBoxL_min";
            numericBoxL_min.ShowUpDown = true;
            numericBoxL_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_min
            // 
            numericBoxK_min.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxK_min, "numericBoxK_min");
            numericBoxK_min.Name = "numericBoxK_min";
            numericBoxK_min.ShowUpDown = true;
            numericBoxK_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_max
            // 
            numericBoxH_max.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxH_max, "numericBoxH_max");
            numericBoxH_max.Name = "numericBoxH_max";
            numericBoxH_max.ShowUpDown = true;
            numericBoxH_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_min
            // 
            numericBoxH_min.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxH_min, "numericBoxH_min");
            numericBoxH_min.Name = "numericBoxH_min";
            numericBoxH_min.ShowUpDown = true;
            numericBoxH_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Controls.Add(radioButtonNanoMeter);
            panel2.Controls.Add(radioButtonAngstrom);
            panel2.Controls.Add(label8);
            panel2.Name = "panel2";
            // 
            // radioButtonNanoMeter
            // 
            resources.ApplyResources(radioButtonNanoMeter, "radioButtonNanoMeter");
            radioButtonNanoMeter.Name = "radioButtonNanoMeter";
            radioButtonNanoMeter.UseVisualStyleBackColor = true;
            radioButtonNanoMeter.CheckedChanged += radioButtonNanoMeter_CheckedChanged;
            // 
            // radioButtonAngstrom
            // 
            resources.ApplyResources(radioButtonAngstrom, "radioButtonAngstrom");
            radioButtonAngstrom.Checked = true;
            radioButtonAngstrom.Name = "radioButtonAngstrom";
            radioButtonAngstrom.TabStop = true;
            radioButtonAngstrom.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(checkBoxTest);
            panel3.Controls.Add(numericBoxCutoffD);
            panel3.Controls.Add(waveLengthControl1);
            panel3.Controls.Add(panel2);
            panel3.Controls.Add(panel1);
            panel3.Controls.Add(checkBoxHideEquivalentPlane);
            panel3.Controls.Add(checkBoxBragBrentano);
            panel3.Controls.Add(buttonCopyClipboard);
            panel3.Controls.Add(checkBoxHideProhibitedPlanes);
            panel3.Controls.Add(label2);
            resources.ApplyResources(panel3, "panel3");
            panel3.Name = "panel3";
            // 
            // numericBoxCutoffD
            // 
            numericBoxCutoffD.BackColor = System.Drawing.Color.Transparent;
            numericBoxCutoffD.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxCutoffD, "numericBoxCutoffD");
            numericBoxCutoffD.Maximum = 10D;
            numericBoxCutoffD.Minimum = 0.001D;
            numericBoxCutoffD.Name = "numericBoxCutoffD";
            numericBoxCutoffD.RadianValue = 0.017453292519943295D;
            numericBoxCutoffD.ShowUpDown = true;
            numericBoxCutoffD.SmartIncrement = true;
            numericBoxCutoffD.Value = 1D;
            numericBoxCutoffD.ValueChanged += numericBoxCutoffD_ValueChanged;
            // 
            // FormScatteringFactor
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(dataGridView);
            Controls.Add(panel3);
            Name = "FormScatteringFactor";
            ShowIcon = false;
            FormClosing += FormCrystallographicInformation_FormClosing;
            Load += FormCrystallographicInformation_Load;
            VisibleChanged += FormScatteringFactor_VisibleChanged;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceScatteringFactor).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxHideEquivalentPlane;
        private System.Windows.Forms.CheckBox checkBoxHideProhibitedPlanes;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCopyClipboard;
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButtonNanoMeter;
        private System.Windows.Forms.RadioButton radioButtonAngstrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
        private NumericBox numericBoxCutoffD;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnH;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnI;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnL;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnMulti;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnD;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnTwoTheta;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnFreal;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnFinv;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnFabs;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnFsq;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnIntPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnIntCondition;
    }
}
