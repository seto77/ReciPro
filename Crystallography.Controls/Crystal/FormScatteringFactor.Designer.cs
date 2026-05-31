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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
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
            // dataGridView = new System.Windows.Forms.DataGridView(); // 260518Cl 旧実装: DPI変更時に列幅が追従しない
            dataGridView = new DpiAwareDataGridView(); // 260518Cl
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
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
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
            //260516Cl resx の dataGridView.Font を継承するため AlternatingRowsDefaultCellStyle (Font のみ設定) は廃止
            //dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Variable Text", 9.75F);
            //dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            //260516Cl resx の dataGridView.Font を継承するため Font 設定を廃止
            //dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Variable Text", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dataGridViewTextBoxColumnH, dataGridViewTextBoxColumnK, dataGridViewTextBoxColumnI, dataGridViewTextBoxColumnL, dataGridViewTextBoxColumnMulti, dataGridViewTextBoxColumnD, dataGridViewTextBoxColumnQ, dataGridViewTextBoxColumnTwoTheta, dataGridViewTextBoxColumnFreal, dataGridViewTextBoxColumnFinv, dataGridViewTextBoxColumnFabs, dataGridViewTextBoxColumnFsq, dataGridViewTextBoxColumnIntPercent, dataGridViewTextBoxColumnIntCondition });
            dataGridView.DataSource = bindingSourceScatteringFactor;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Window;
            //260516Cl resx の dataGridView.Font を継承するため Font 設定を廃止
            //dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI Variable Text", 9.75F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            dataGridView.DefaultCellStyle = dataGridViewCellStyle13;
            resources.ApplyResources(dataGridView, "dataGridView");
            toolTip.SetToolTip(dataGridView, resources.GetString("dataGridView.ToolTip")); // 260531Cl
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersVisible = false;
            dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            //260516Cl resx の dataGridView.Font を継承するため Font 設定を廃止
            //dataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Variable Text", 9.75F);
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
            toolTip.SetToolTip(checkBoxHideProhibitedPlanes, resources.GetString("checkBoxHideProhibitedPlanes.ToolTip")); // 260531Cl
            checkBoxHideProhibitedPlanes.Checked = true;
            checkBoxHideProhibitedPlanes.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideProhibitedPlanes.Name = "checkBoxHideProhibitedPlanes";
            checkBoxHideProhibitedPlanes.CheckedChanged += numericBoxCutoffD_ValueChanged;
            // 
            // checkBoxHideEquivalentPlane
            // 
            resources.ApplyResources(checkBoxHideEquivalentPlane, "checkBoxHideEquivalentPlane");
            toolTip.SetToolTip(checkBoxHideEquivalentPlane, resources.GetString("checkBoxHideEquivalentPlane.ToolTip")); // 260531Cl
            checkBoxHideEquivalentPlane.Checked = true;
            checkBoxHideEquivalentPlane.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideEquivalentPlane.Name = "checkBoxHideEquivalentPlane";
            checkBoxHideEquivalentPlane.CheckedChanged += numericBoxCutoffD_ValueChanged;
            // 
            // buttonCopyClipboard
            // 
            resources.ApplyResources(buttonCopyClipboard, "buttonCopyClipboard");
            toolTip.SetToolTip(buttonCopyClipboard, resources.GetString("buttonCopyClipboard.ToolTip")); // 260531Cl
            buttonCopyClipboard.Name = "buttonCopyClipboard";
            buttonCopyClipboard.UseVisualStyleBackColor = true;
            buttonCopyClipboard.Click += buttonCopyClipboard_Click;
            // 
            // checkBoxBragBrentano
            // 
            resources.ApplyResources(checkBoxBragBrentano, "checkBoxBragBrentano");
            toolTip.SetToolTip(checkBoxBragBrentano, resources.GetString("checkBoxBragBrentano.ToolTip")); // 260531Cl
            checkBoxBragBrentano.Name = "checkBoxBragBrentano";
            checkBoxBragBrentano.CheckedChanged += checkBoxBragBrentano_CheckedChanged;
            // 
            // waveLengthControl1
            //
            resources.ApplyResources(waveLengthControl1, "waveLengthControl1");
            toolTip.SetToolTip(waveLengthControl1, resources.GetString("waveLengthControl1.ToolTip")); // 260531Cl
            captureExtender.SetCapture(waveLengthControl1, true); // 260524Cl: 波長/エネルギー選択を auto キャプチャ対象にする (旧: 手動キャプチャのみ)
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
            toolTip.SetToolTip(checkBoxTest, resources.GetString("checkBoxTest.ToolTip")); // 260531Cl
            checkBoxTest.Name = "checkBoxTest";
            checkBoxTest.UseVisualStyleBackColor = true;
            checkBoxTest.CheckedChanged += checkBoxTest_CheckedChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip")); // 260531Cl
            label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip")); // 260531Cl
            label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip")); // 260531Cl
            label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip")); // 260531Cl
            label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip")); // 260531Cl
            label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip")); // 260531Cl
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
            toolTip.SetToolTip(numericBoxL_step, resources.GetString("numericBoxL_step.ToolTip")); // 260531Cl
            numericBoxL_step.Maximum = 1D;
            numericBoxL_step.Minimum = 0.001D;
            numericBoxL_step.Name = "numericBoxL_step";
            numericBoxL_step.RadianValue = 0.00017453292519943296D;
            numericBoxL_step.ShowUpDown = true;
            numericBoxL_step.SmartIncrement = true;
            numericBoxL_step.ValueFontSize = 9F;
            numericBoxL_step.Value = 0.01D;
            numericBoxL_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_step
            // 
            numericBoxK_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_step.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxK_step, "numericBoxK_step");
            toolTip.SetToolTip(numericBoxK_step, resources.GetString("numericBoxK_step.ToolTip")); // 260531Cl
            numericBoxK_step.Maximum = 1D;
            numericBoxK_step.Minimum = 0.001D;
            numericBoxK_step.Name = "numericBoxK_step";
            numericBoxK_step.RadianValue = 0.00017453292519943296D;
            numericBoxK_step.ShowUpDown = true;
            numericBoxK_step.SmartIncrement = true;
            numericBoxK_step.ValueFontSize = 9F;
            numericBoxK_step.Value = 0.01D;
            numericBoxK_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_step
            // 
            numericBoxH_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_step.DecimalPlaces = 3;
            resources.ApplyResources(numericBoxH_step, "numericBoxH_step");
            toolTip.SetToolTip(numericBoxH_step, resources.GetString("numericBoxH_step.ToolTip")); // 260531Cl
            numericBoxH_step.Maximum = 1D;
            numericBoxH_step.Minimum = 0.001D;
            numericBoxH_step.Name = "numericBoxH_step";
            numericBoxH_step.RadianValue = 0.00017453292519943296D;
            numericBoxH_step.ShowUpDown = true;
            numericBoxH_step.SmartIncrement = true;
            numericBoxH_step.ValueFontSize = 9F;
            numericBoxH_step.Value = 0.01D;
            numericBoxH_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_max
            // 
            numericBoxL_max.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxL_max, "numericBoxL_max");
            toolTip.SetToolTip(numericBoxL_max, resources.GetString("numericBoxL_max.ToolTip")); // 260531Cl
            numericBoxL_max.Name = "numericBoxL_max";
            numericBoxL_max.RadianValue = 0.017453292519943295D;
            numericBoxL_max.ShowUpDown = true;
            numericBoxL_max.ValueFontSize = 9F;
            numericBoxL_max.Value = 1D;
            numericBoxL_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_max
            // 
            numericBoxK_max.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxK_max, "numericBoxK_max");
            toolTip.SetToolTip(numericBoxK_max, resources.GetString("numericBoxK_max.ToolTip")); // 260531Cl
            numericBoxK_max.Name = "numericBoxK_max";
            numericBoxK_max.ShowUpDown = true;
            numericBoxK_max.ValueFontSize = 9F;
            numericBoxK_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_min
            // 
            numericBoxL_min.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxL_min, "numericBoxL_min");
            toolTip.SetToolTip(numericBoxL_min, resources.GetString("numericBoxL_min.ToolTip")); // 260531Cl
            numericBoxL_min.Name = "numericBoxL_min";
            numericBoxL_min.ShowUpDown = true;
            numericBoxL_min.ValueFontSize = 9F;
            numericBoxL_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_min
            // 
            numericBoxK_min.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxK_min, "numericBoxK_min");
            toolTip.SetToolTip(numericBoxK_min, resources.GetString("numericBoxK_min.ToolTip")); // 260531Cl
            numericBoxK_min.Name = "numericBoxK_min";
            numericBoxK_min.ShowUpDown = true;
            numericBoxK_min.ValueFontSize = 9F;
            numericBoxK_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_max
            // 
            numericBoxH_max.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxH_max, "numericBoxH_max");
            toolTip.SetToolTip(numericBoxH_max, resources.GetString("numericBoxH_max.ToolTip")); // 260531Cl
            numericBoxH_max.Name = "numericBoxH_max";
            numericBoxH_max.ShowUpDown = true;
            numericBoxH_max.ValueFontSize = 9F;
            numericBoxH_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_min
            // 
            numericBoxH_min.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxH_min, "numericBoxH_min");
            toolTip.SetToolTip(numericBoxH_min, resources.GetString("numericBoxH_min.ToolTip")); // 260531Cl
            numericBoxH_min.Name = "numericBoxH_min";
            numericBoxH_min.ShowUpDown = true;
            numericBoxH_min.ValueFontSize = 9F;
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
            toolTip.SetToolTip(radioButtonNanoMeter, resources.GetString("radioButtonNanoMeter.ToolTip")); // 260531Cl
            radioButtonNanoMeter.Name = "radioButtonNanoMeter";
            radioButtonNanoMeter.UseVisualStyleBackColor = true;
            radioButtonNanoMeter.CheckedChanged += radioButtonNanoMeter_CheckedChanged;
            // 
            // radioButtonAngstrom
            // 
            resources.ApplyResources(radioButtonAngstrom, "radioButtonAngstrom");
            toolTip.SetToolTip(radioButtonAngstrom, resources.GetString("radioButtonAngstrom.ToolTip")); // 260531Cl
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
            toolTip.SetToolTip(numericBoxCutoffD, resources.GetString("numericBoxCutoffD.ToolTip")); // 260531Cl
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
        // private System.Windows.Forms.DataGridView dataGridView; // 260518Cl 旧実装
        private DpiAwareDataGridView dataGridView; // 260518Cl
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
