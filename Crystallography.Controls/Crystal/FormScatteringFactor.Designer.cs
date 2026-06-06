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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormScatteringFactor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            dataGridView = new DpiAwareDataGridView();
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
            checkBoxHideProhibitedPlanes = new System.Windows.Forms.CheckBox();
            checkBoxHideEquivalentPlane = new System.Windows.Forms.CheckBox();
            buttonCopyClipboard = new System.Windows.Forms.Button();
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
            numericBoxL_step = new NumericBox();
            numericBoxK_step = new NumericBox();
            numericBoxH_step = new NumericBox();
            numericBoxL_max = new NumericBox();
            numericBoxK_max = new NumericBox();
            numericBoxL_min = new NumericBox();
            numericBoxK_min = new NumericBox();
            numericBoxH_max = new NumericBox();
            numericBoxH_min = new NumericBox();
            numericBoxCutoffD = new NumericBox();
            labelModel = new System.Windows.Forms.Label();
            radioButtonXrayFs = new System.Windows.Forms.RadioButton();
            radioButtonXrayFqSq = new System.Windows.Forms.RadioButton();
            radioButtonElectronPeng = new System.Windows.Forms.RadioButton();
            radioButtonElectronKirkland = new System.Windows.Forms.RadioButton();
            radioButtonElectronEightGaussian = new System.Windows.Forms.RadioButton();
            checkBoxDebyeWaller = new System.Windows.Forms.CheckBox();
            panel1 = new System.Windows.Forms.Panel();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPageReflections = new System.Windows.Forms.TabPage();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            graphControl2 = new GraphControl();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            button1 = new System.Windows.Forms.Button();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            tabPageScatteringFactors = new System.Windows.Forms.TabPage();
            miniTable1 = new MiniTable();
            Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            flowLayoutPanelModel = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelModel_Xray = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelModel_Electron = new System.Windows.Forms.FlowLayoutPanel();
            graphControlScatteringFactor = new GraphControl();
            tabPageAttenuations = new System.Windows.Forms.TabPage();
            tlpAtten = new System.Windows.Forms.TableLayoutPanel();
            graphAtten = new GraphControl();
            numAttenThickness = new NumericBox();
            dgvAttenScalars = new MiniTable();
            flowAttenDetail = new System.Windows.Forms.FlowLayoutPanel();
            dgvAttenEdges = new MiniTable();
            colEdgeElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEdgeZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEdgeEdge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEdgeKeV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEdgeJump = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dgvAttenElectron = new MiniTable();
            colElecElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colElecZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colElecAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colElecA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dgvAttenNeutron = new MiniTable();
            colNeutElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNeutBcoh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNeutScoh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNeutAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            tabPageFluorescence = new System.Windows.Forms.TabPage();
            tlpFluor = new System.Windows.Forms.TableLayoutPanel();
            graphFluor = new GraphControl();
            dgvFluorScalars = new MiniTable();
            dgvFluorLines = new MiniTable();
            colFlElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFlLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFlE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFlRelI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFlOmega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            labelFluorNA = new System.Windows.Forms.Label();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceScatteringFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPageReflections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tabPageScatteringFactors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)miniTable1).BeginInit();
            flowLayoutPanelModel.SuspendLayout();
            flowLayoutPanelModel_Xray.SuspendLayout();
            flowLayoutPanelModel_Electron.SuspendLayout();
            tabPageAttenuations.SuspendLayout();
            tlpAtten.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAttenScalars).BeginInit();
            flowAttenDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAttenEdges).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAttenElectron).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvAttenNeutron).BeginInit();
            tabPageFluorescence.SuspendLayout();
            tlpFluor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFluorScalars).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFluorLines).BeginInit();
            flowLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            toolTip.SetToolTip(dataGridView, resources.GetString("dataGridView.ToolTip"));
            dataGridView.CellFormatting += dataGridView_CellFormatting;
            // 
            // dataGridViewTextBoxColumnH
            // 
            dataGridViewTextBoxColumnH.DataPropertyName = "H";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewTextBoxColumnH.DefaultCellStyle = dataGridViewCellStyle2;
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
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewTextBoxColumnL.DefaultCellStyle = dataGridViewCellStyle3;
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
            dataGridViewCellStyle4.Format = "G7";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewTextBoxColumnD.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(dataGridViewTextBoxColumnD, "dataGridViewTextBoxColumnD");
            dataGridViewTextBoxColumnD.Name = "dataGridViewTextBoxColumnD";
            dataGridViewTextBoxColumnD.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnQ
            // 
            dataGridViewTextBoxColumnQ.DataPropertyName = "Q";
            dataGridViewCellStyle5.Format = "G7";
            dataGridViewTextBoxColumnQ.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(dataGridViewTextBoxColumnQ, "dataGridViewTextBoxColumnQ");
            dataGridViewTextBoxColumnQ.Name = "dataGridViewTextBoxColumnQ";
            dataGridViewTextBoxColumnQ.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnTwoTheta
            // 
            dataGridViewTextBoxColumnTwoTheta.DataPropertyName = "TwoTheta";
            dataGridViewCellStyle6.Format = "G7";
            dataGridViewTextBoxColumnTwoTheta.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(dataGridViewTextBoxColumnTwoTheta, "dataGridViewTextBoxColumnTwoTheta");
            dataGridViewTextBoxColumnTwoTheta.Name = "dataGridViewTextBoxColumnTwoTheta";
            dataGridViewTextBoxColumnTwoTheta.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnFreal
            // 
            dataGridViewTextBoxColumnFreal.DataPropertyName = "F_real";
            dataGridViewCellStyle7.Format = "G7";
            dataGridViewTextBoxColumnFreal.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(dataGridViewTextBoxColumnFreal, "dataGridViewTextBoxColumnFreal");
            dataGridViewTextBoxColumnFreal.Name = "dataGridViewTextBoxColumnFreal";
            dataGridViewTextBoxColumnFreal.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnFinv
            // 
            dataGridViewTextBoxColumnFinv.DataPropertyName = "F_inv";
            dataGridViewCellStyle8.Format = "G7";
            dataGridViewTextBoxColumnFinv.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(dataGridViewTextBoxColumnFinv, "dataGridViewTextBoxColumnFinv");
            dataGridViewTextBoxColumnFinv.Name = "dataGridViewTextBoxColumnFinv";
            dataGridViewTextBoxColumnFinv.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnFabs
            // 
            dataGridViewTextBoxColumnFabs.DataPropertyName = "F";
            dataGridViewCellStyle9.Format = "G7";
            dataGridViewTextBoxColumnFabs.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(dataGridViewTextBoxColumnFabs, "dataGridViewTextBoxColumnFabs");
            dataGridViewTextBoxColumnFabs.Name = "dataGridViewTextBoxColumnFabs";
            dataGridViewTextBoxColumnFabs.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnFsq
            // 
            dataGridViewTextBoxColumnFsq.DataPropertyName = "F2";
            dataGridViewCellStyle10.Format = "G7";
            dataGridViewTextBoxColumnFsq.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(dataGridViewTextBoxColumnFsq, "dataGridViewTextBoxColumnFsq");
            dataGridViewTextBoxColumnFsq.Name = "dataGridViewTextBoxColumnFsq";
            dataGridViewTextBoxColumnFsq.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumnIntPercent
            // 
            dataGridViewTextBoxColumnIntPercent.DataPropertyName = "RelInt";
            dataGridViewCellStyle11.Format = "G7";
            dataGridViewTextBoxColumnIntPercent.DefaultCellStyle = dataGridViewCellStyle11;
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
            // checkBoxHideProhibitedPlanes
            // 
            resources.ApplyResources(checkBoxHideProhibitedPlanes, "checkBoxHideProhibitedPlanes");
            checkBoxHideProhibitedPlanes.Checked = true;
            checkBoxHideProhibitedPlanes.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideProhibitedPlanes.Name = "checkBoxHideProhibitedPlanes";
            toolTip.SetToolTip(checkBoxHideProhibitedPlanes, resources.GetString("checkBoxHideProhibitedPlanes.ToolTip"));
            checkBoxHideProhibitedPlanes.CheckedChanged += numericBoxCutoffD_ValueChanged;
            // 
            // checkBoxHideEquivalentPlane
            // 
            resources.ApplyResources(checkBoxHideEquivalentPlane, "checkBoxHideEquivalentPlane");
            checkBoxHideEquivalentPlane.Checked = true;
            checkBoxHideEquivalentPlane.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxHideEquivalentPlane.Name = "checkBoxHideEquivalentPlane";
            toolTip.SetToolTip(checkBoxHideEquivalentPlane, resources.GetString("checkBoxHideEquivalentPlane.ToolTip"));
            checkBoxHideEquivalentPlane.CheckedChanged += numericBoxCutoffD_ValueChanged;
            // 
            // buttonCopyClipboard
            // 
            resources.ApplyResources(buttonCopyClipboard, "buttonCopyClipboard");
            buttonCopyClipboard.Name = "buttonCopyClipboard";
            toolTip.SetToolTip(buttonCopyClipboard, resources.GetString("buttonCopyClipboard.ToolTip"));
            buttonCopyClipboard.UseVisualStyleBackColor = true;
            buttonCopyClipboard.Click += buttonCopyClipboard_Click;
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
            captureExtender.SetCapture(waveLengthControl1, true);
            waveLengthControl1.DirectionWaveEnergy = System.Windows.Forms.FlowDirection.LeftToRight;
            waveLengthControl1.DirectionWhole = System.Windows.Forms.FlowDirection.LeftToRight;
            waveLengthControl1.Energy = 8.04114721D;
            waveLengthControl1.Monochrome = true;
            waveLengthControl1.Name = "waveLengthControl1";
            waveLengthControl1.ShowWaveSource = true;
            toolTip.SetToolTip(waveLengthControl1, resources.GetString("waveLengthControl1.ToolTip"));
            waveLengthControl1.WaveLength = 0.154187106667D;
            waveLengthControl1.WaveSource = WaveSource.Xray;
            waveLengthControl1.XrayWaveSourceElementNumber = 29;
            waveLengthControl1.XrayWaveSourceLine = XrayLine.Ka;
            waveLengthControl1.WavelengthChanged += waveLengthControl1_WavelengthChanged;
            waveLengthControl1.WavelengthUnitChanged += radioButtonNanoMeter_CheckedChanged;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 100;
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
            toolTip.SetToolTip(numericBoxL_step, resources.GetString("numericBoxL_step.ToolTip1"));
            numericBoxL_step.Value = 0.01D;
            numericBoxL_step.ValueFontSize = 9F;
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
            toolTip.SetToolTip(numericBoxK_step, resources.GetString("numericBoxK_step.ToolTip1"));
            numericBoxK_step.Value = 0.01D;
            numericBoxK_step.ValueFontSize = 9F;
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
            toolTip.SetToolTip(numericBoxH_step, resources.GetString("numericBoxH_step.ToolTip1"));
            numericBoxH_step.Value = 0.01D;
            numericBoxH_step.ValueFontSize = 9F;
            numericBoxH_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_max
            // 
            numericBoxL_max.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxL_max, "numericBoxL_max");
            numericBoxL_max.Name = "numericBoxL_max";
            numericBoxL_max.RadianValue = 0.017453292519943295D;
            numericBoxL_max.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxL_max, resources.GetString("numericBoxL_max.ToolTip1"));
            numericBoxL_max.Value = 1D;
            numericBoxL_max.ValueFontSize = 9F;
            numericBoxL_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_max
            // 
            numericBoxK_max.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxK_max, "numericBoxK_max");
            numericBoxK_max.Name = "numericBoxK_max";
            numericBoxK_max.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxK_max, resources.GetString("numericBoxK_max.ToolTip1"));
            numericBoxK_max.ValueFontSize = 9F;
            numericBoxK_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_min
            // 
            numericBoxL_min.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxL_min, "numericBoxL_min");
            numericBoxL_min.Name = "numericBoxL_min";
            numericBoxL_min.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxL_min, resources.GetString("numericBoxL_min.ToolTip1"));
            numericBoxL_min.ValueFontSize = 9F;
            numericBoxL_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_min
            // 
            numericBoxK_min.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxK_min, "numericBoxK_min");
            numericBoxK_min.Name = "numericBoxK_min";
            numericBoxK_min.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxK_min, resources.GetString("numericBoxK_min.ToolTip1"));
            numericBoxK_min.ValueFontSize = 9F;
            numericBoxK_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_max
            // 
            numericBoxH_max.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxH_max, "numericBoxH_max");
            numericBoxH_max.Name = "numericBoxH_max";
            numericBoxH_max.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxH_max, resources.GetString("numericBoxH_max.ToolTip1"));
            numericBoxH_max.ValueFontSize = 9F;
            numericBoxH_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_min
            // 
            numericBoxH_min.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxH_min, "numericBoxH_min");
            numericBoxH_min.Name = "numericBoxH_min";
            numericBoxH_min.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxH_min, resources.GetString("numericBoxH_min.ToolTip1"));
            numericBoxH_min.ValueFontSize = 9F;
            numericBoxH_min.ValueChanged += numericBoxH_min_ValueChanged;
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
            toolTip.SetToolTip(numericBoxCutoffD, resources.GetString("numericBoxCutoffD.ToolTip1"));
            numericBoxCutoffD.Value = 1D;
            numericBoxCutoffD.ValueChanged += numericBoxCutoffD_ValueChanged;
            // 
            // labelModel
            // 
            resources.ApplyResources(labelModel, "labelModel");
            labelModel.Name = "labelModel";
            toolTip.SetToolTip(labelModel, resources.GetString("labelModel.ToolTip"));
            // 
            // radioButtonXrayFs
            // 
            resources.ApplyResources(radioButtonXrayFs, "radioButtonXrayFs");
            radioButtonXrayFs.Name = "radioButtonXrayFs";
            radioButtonXrayFs.TabStop = true;
            toolTip.SetToolTip(radioButtonXrayFs, resources.GetString("radioButtonXrayFs.ToolTip"));
            radioButtonXrayFs.UseVisualStyleBackColor = true;
            // 
            // radioButtonXrayFqSq
            // 
            resources.ApplyResources(radioButtonXrayFqSq, "radioButtonXrayFqSq");
            radioButtonXrayFqSq.Name = "radioButtonXrayFqSq";
            radioButtonXrayFqSq.TabStop = true;
            toolTip.SetToolTip(radioButtonXrayFqSq, resources.GetString("radioButtonXrayFqSq.ToolTip"));
            radioButtonXrayFqSq.UseVisualStyleBackColor = true;
            // 
            // radioButtonElectronPeng
            // 
            resources.ApplyResources(radioButtonElectronPeng, "radioButtonElectronPeng");
            radioButtonElectronPeng.Name = "radioButtonElectronPeng";
            radioButtonElectronPeng.TabStop = true;
            toolTip.SetToolTip(radioButtonElectronPeng, resources.GetString("radioButtonElectronPeng.ToolTip"));
            radioButtonElectronPeng.UseVisualStyleBackColor = true;
            // 
            // radioButtonElectronKirkland
            // 
            resources.ApplyResources(radioButtonElectronKirkland, "radioButtonElectronKirkland");
            radioButtonElectronKirkland.Name = "radioButtonElectronKirkland";
            radioButtonElectronKirkland.TabStop = true;
            toolTip.SetToolTip(radioButtonElectronKirkland, resources.GetString("radioButtonElectronKirkland.ToolTip"));
            radioButtonElectronKirkland.UseVisualStyleBackColor = true;
            // 
            // radioButtonElectronEightGaussian
            // 
            resources.ApplyResources(radioButtonElectronEightGaussian, "radioButtonElectronEightGaussian");
            radioButtonElectronEightGaussian.Name = "radioButtonElectronEightGaussian";
            radioButtonElectronEightGaussian.TabStop = true;
            toolTip.SetToolTip(radioButtonElectronEightGaussian, resources.GetString("radioButtonElectronEightGaussian.ToolTip"));
            radioButtonElectronEightGaussian.UseVisualStyleBackColor = true;
            // 
            // checkBoxDebyeWaller
            // 
            resources.ApplyResources(checkBoxDebyeWaller, "checkBoxDebyeWaller");
            checkBoxDebyeWaller.Name = "checkBoxDebyeWaller";
            toolTip.SetToolTip(checkBoxDebyeWaller, resources.GetString("checkBoxDebyeWaller.ToolTip"));
            checkBoxDebyeWaller.UseVisualStyleBackColor = true;
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
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPageReflections);
            tabControl1.Controls.Add(tabPageScatteringFactors);
            tabControl1.Controls.Add(tabPageAttenuations);
            tabControl1.Controls.Add(tabPageFluorescence);
            resources.ApplyResources(tabControl1, "tabControl1");
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            // 
            // tabPageReflections
            // 
            tabPageReflections.Controls.Add(splitContainer1);
            tabPageReflections.Controls.Add(flowLayoutPanel3);
            tabPageReflections.Controls.Add(flowLayoutPanel2);
            tabPageReflections.Controls.Add(flowLayoutPanel1);
            tabPageReflections.Controls.Add(flowLayoutPanel4);
            resources.ApplyResources(tabPageReflections, "tabPageReflections");
            tabPageReflections.Name = "tabPageReflections";
            tabPageReflections.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(dataGridView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(graphControl2);
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel6);
            // 
            // graphControl2
            // 
            graphControl2.AllowMouseOperation = true;
            graphControl2.AxisLineColor = System.Drawing.Color.Gray;
            graphControl2.AxisTextColor = System.Drawing.Color.Black;
            graphControl2.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControl2.AxisXTextVisible = true;
            graphControl2.AxisYTextVisible = true;
            graphControl2.BackgroundColor = System.Drawing.Color.White;
            graphControl2.BottomMargin = 0D;
            graphControl2.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControl2.DivisionLineXVisible = true;
            graphControl2.DivisionLineYVisible = true;
            resources.ApplyResources(graphControl2, "graphControl2");
            graphControl2.FixRangeHorizontal = false;
            graphControl2.FixRangeVertical = false;
            graphControl2.GraphTitle = "";
            graphControl2.IsIntegerX = false;
            graphControl2.IsIntegerY = false;
            graphControl2.LabelX = "X:";
            graphControl2.LabelY = "Y:";
            graphControl2.LeftMargin = 0F;
            graphControl2.LineWidth = 1F;
            graphControl2.LowerX = 0D;
            graphControl2.LowerY = 0D;
            graphControl2.MaximalX = 1D;
            graphControl2.MaximalY = 1D;
            graphControl2.MinimalX = 0D;
            graphControl2.MinimalY = 0D;
            graphControl2.Mode = GraphControl.DrawingMode.Line;
            graphControl2.MousePositionVisible = false;
            graphControl2.MousePositionXDigit = -1;
            graphControl2.MousePositionYDigit = -1;
            graphControl2.Name = "graphControl2";
            graphControl2.OriginPosition = new System.Drawing.Point(40, 20);
            graphControl2.UnitX = "";
            graphControl2.UnitY = "";
            graphControl2.UpperPanelFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControl2.UpperPanelVisible = false;
            graphControl2.UpperX = 1D;
            graphControl2.UpperY = 1D;
            graphControl2.UseLineWidth = true;
            graphControl2.VerticalLineColor = System.Drawing.Color.Red;
            graphControl2.VerticalLineMarkerRadius = 3.5F;
            graphControl2.VerticalLineMarkerVisible = false;
            graphControl2.XLog = false;
            graphControl2.YLog = false;
            // 
            // flowLayoutPanel6
            // 
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Controls.Add(button1);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(checkBoxTest);
            flowLayoutPanel3.Controls.Add(panel1);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(numericBoxCutoffD);
            flowLayoutPanel2.Controls.Add(buttonCopyClipboard);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(checkBoxBragBrentano);
            flowLayoutPanel1.Controls.Add(checkBoxHideProhibitedPlanes);
            flowLayoutPanel1.Controls.Add(checkBoxHideEquivalentPlane);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // tabPageScatteringFactors
            // 
            tabPageScatteringFactors.Controls.Add(miniTable1);
            tabPageScatteringFactors.Controls.Add(flowLayoutPanelModel);
            tabPageScatteringFactors.Controls.Add(graphControlScatteringFactor);
            resources.ApplyResources(tabPageScatteringFactors, "tabPageScatteringFactors");
            tabPageScatteringFactors.Name = "tabPageScatteringFactors";
            tabPageScatteringFactors.UseVisualStyleBackColor = true;
            // 
            // miniTable1
            // 
            miniTable1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            miniTable1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { Column1, Column2, Column3 });
            resources.ApplyResources(miniTable1, "miniTable1");
            miniTable1.Name = "miniTable1";
            miniTable1.TabStop = false;
            // 
            // Column1
            // 
            resources.ApplyResources(Column1, "Column1");
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            resources.ApplyResources(Column2, "Column2");
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            resources.ApplyResources(Column3, "Column3");
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // flowLayoutPanelModel
            // 
            resources.ApplyResources(flowLayoutPanelModel, "flowLayoutPanelModel");
            flowLayoutPanelModel.Controls.Add(labelModel);
            flowLayoutPanelModel.Controls.Add(flowLayoutPanelModel_Xray);
            flowLayoutPanelModel.Controls.Add(flowLayoutPanelModel_Electron);
            flowLayoutPanelModel.Controls.Add(checkBoxDebyeWaller);
            flowLayoutPanelModel.Name = "flowLayoutPanelModel";
            // 
            // flowLayoutPanelModel_Xray
            // 
            resources.ApplyResources(flowLayoutPanelModel_Xray, "flowLayoutPanelModel_Xray");
            flowLayoutPanelModel_Xray.Controls.Add(radioButtonXrayFs);
            flowLayoutPanelModel_Xray.Controls.Add(radioButtonXrayFqSq);
            flowLayoutPanelModel_Xray.Name = "flowLayoutPanelModel_Xray";
            // 
            // flowLayoutPanelModel_Electron
            // 
            resources.ApplyResources(flowLayoutPanelModel_Electron, "flowLayoutPanelModel_Electron");
            flowLayoutPanelModel_Electron.Controls.Add(radioButtonElectronPeng);
            flowLayoutPanelModel_Electron.Controls.Add(radioButtonElectronKirkland);
            flowLayoutPanelModel_Electron.Controls.Add(radioButtonElectronEightGaussian);
            flowLayoutPanelModel_Electron.Name = "flowLayoutPanelModel_Electron";
            // 
            // graphControlScatteringFactor
            // 
            graphControlScatteringFactor.AllowMouseOperation = true;
            graphControlScatteringFactor.AxisLineColor = System.Drawing.Color.Gray;
            graphControlScatteringFactor.AxisTextColor = System.Drawing.Color.Black;
            graphControlScatteringFactor.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControlScatteringFactor.AxisXTextVisible = true;
            graphControlScatteringFactor.AxisYTextVisible = true;
            graphControlScatteringFactor.BackgroundColor = System.Drawing.Color.White;
            graphControlScatteringFactor.BottomMargin = 0D;
            graphControlScatteringFactor.DivisionLineColor = System.Drawing.Color.LightGray;
            graphControlScatteringFactor.DivisionLineXVisible = true;
            graphControlScatteringFactor.DivisionLineYVisible = true;
            graphControlScatteringFactor.FixRangeHorizontal = false;
            graphControlScatteringFactor.FixRangeVertical = false;
            resources.ApplyResources(graphControlScatteringFactor, "graphControlScatteringFactor");
            graphControlScatteringFactor.GraphTitle = "";
            graphControlScatteringFactor.IsIntegerX = false;
            graphControlScatteringFactor.IsIntegerY = false;
            graphControlScatteringFactor.LabelX = "X:";
            graphControlScatteringFactor.LabelY = "Y:";
            graphControlScatteringFactor.LeftMargin = 0F;
            graphControlScatteringFactor.LineWidth = 1F;
            graphControlScatteringFactor.LowerX = 0D;
            graphControlScatteringFactor.LowerY = 0D;
            graphControlScatteringFactor.MaximalX = 1D;
            graphControlScatteringFactor.MaximalY = 1D;
            graphControlScatteringFactor.MinimalX = 0D;
            graphControlScatteringFactor.MinimalY = 0D;
            graphControlScatteringFactor.Mode = GraphControl.DrawingMode.Line;
            graphControlScatteringFactor.MousePositionVisible = false;
            graphControlScatteringFactor.MousePositionXDigit = -1;
            graphControlScatteringFactor.MousePositionYDigit = -1;
            graphControlScatteringFactor.Name = "graphControlScatteringFactor";
            graphControlScatteringFactor.OriginPosition = new System.Drawing.Point(40, 20);
            graphControlScatteringFactor.UnitX = "";
            graphControlScatteringFactor.UnitY = "";
            graphControlScatteringFactor.UpperPanelFont = new System.Drawing.Font("Segoe UI", 9F);
            graphControlScatteringFactor.UpperPanelVisible = false;
            graphControlScatteringFactor.UpperX = 1D;
            graphControlScatteringFactor.UpperY = 1D;
            graphControlScatteringFactor.UseLineWidth = true;
            graphControlScatteringFactor.VerticalLineColor = System.Drawing.Color.Red;
            graphControlScatteringFactor.VerticalLineMarkerRadius = 3.5F;
            graphControlScatteringFactor.VerticalLineMarkerVisible = false;
            graphControlScatteringFactor.XLog = false;
            graphControlScatteringFactor.YLog = false;
            // 
            // tabPageAttenuations
            // 
            tabPageAttenuations.Controls.Add(tlpAtten);
            resources.ApplyResources(tabPageAttenuations, "tabPageAttenuations");
            tabPageAttenuations.Name = "tabPageAttenuations";
            tabPageAttenuations.UseVisualStyleBackColor = true;
            // 
            // tlpAtten
            // 
            resources.ApplyResources(tlpAtten, "tlpAtten");
            tlpAtten.Controls.Add(graphAtten, 0, 0);
            tlpAtten.Controls.Add(numAttenThickness, 1, 0);
            tlpAtten.Controls.Add(dgvAttenScalars, 1, 1);
            tlpAtten.Controls.Add(flowAttenDetail, 1, 2);
            tlpAtten.Name = "tlpAtten";
            // 
            // graphAtten
            // 
            graphAtten.AllowMouseOperation = true;
            graphAtten.AxisLineColor = System.Drawing.Color.Gray;
            graphAtten.AxisTextColor = System.Drawing.Color.Black;
            graphAtten.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphAtten.AxisXTextVisible = true;
            graphAtten.AxisYTextVisible = true;
            graphAtten.BackgroundColor = System.Drawing.Color.White;
            graphAtten.BottomMargin = 0D;
            graphAtten.DivisionLineColor = System.Drawing.Color.LightGray;
            graphAtten.DivisionLineXVisible = true;
            graphAtten.DivisionLineYVisible = true;
            resources.ApplyResources(graphAtten, "graphAtten");
            graphAtten.FixRangeHorizontal = false;
            graphAtten.FixRangeVertical = false;
            graphAtten.GraphTitle = "";
            graphAtten.IsIntegerX = false;
            graphAtten.IsIntegerY = false;
            graphAtten.LabelX = "X:";
            graphAtten.LabelY = "Y:";
            graphAtten.LeftMargin = 0F;
            graphAtten.LineWidth = 1F;
            graphAtten.LowerX = 0D;
            graphAtten.LowerY = 0D;
            graphAtten.MaximalX = 1D;
            graphAtten.MaximalY = 1D;
            graphAtten.MinimalX = 0D;
            graphAtten.MinimalY = 0D;
            graphAtten.Mode = GraphControl.DrawingMode.Line;
            graphAtten.MousePositionVisible = true;
            graphAtten.MousePositionXDigit = -1;
            graphAtten.MousePositionYDigit = -1;
            graphAtten.Name = "graphAtten";
            graphAtten.OriginPosition = new System.Drawing.Point(40, 20);
            tlpAtten.SetRowSpan(graphAtten, 3);
            graphAtten.UnitX = "";
            graphAtten.UnitY = "";
            graphAtten.UpperPanelFont = new System.Drawing.Font("Segoe UI", 9F);
            graphAtten.UpperPanelVisible = true;
            graphAtten.UpperX = 1D;
            graphAtten.UpperY = 1D;
            graphAtten.UseLineWidth = true;
            graphAtten.VerticalLineColor = System.Drawing.Color.Red;
            graphAtten.VerticalLineMarkerRadius = 3.5F;
            graphAtten.VerticalLineMarkerVisible = false;
            graphAtten.XLog = false;
            graphAtten.YLog = false;
            // 
            // numAttenThickness
            // 
            numAttenThickness.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numAttenThickness, "numAttenThickness");
            numAttenThickness.Maximum = 10000000D;
            numAttenThickness.Minimum = 0D;
            numAttenThickness.Name = "numAttenThickness";
            numAttenThickness.RadianValue = 1.7453292519943295D;
            numAttenThickness.Value = 100D;
            toolTip.SetToolTip(numAttenThickness, resources.GetString("numAttenThickness.ToolTip1"));//260606Cl ツールチップ配線
            // 
            // dgvAttenScalars
            // 
            resources.ApplyResources(dgvAttenScalars, "dgvAttenScalars");
            dgvAttenScalars.Name = "dgvAttenScalars";
            dgvAttenScalars.TabStop = false;
            //
            // flowAttenDetail
            //
            flowAttenDetail.AutoScroll = true;
            flowAttenDetail.Controls.Add(dgvAttenEdges);
            flowAttenDetail.Controls.Add(dgvAttenElectron);
            flowAttenDetail.Controls.Add(dgvAttenNeutron);
            flowAttenDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            flowAttenDetail.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowAttenDetail.Location = new System.Drawing.Point(369, 205);
            flowAttenDetail.Name = "flowAttenDetail";
            flowAttenDetail.Size = new System.Drawing.Size(222, 192);
            flowAttenDetail.TabIndex = 4;
            flowAttenDetail.WrapContents = false;
            //
            // dgvAttenEdges
            //
            dgvAttenEdges.AllowVerticalScroll = true;
            dgvAttenEdges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colEdgeElem, colEdgeZ, colEdgeEdge, colEdgeKeV, colEdgeJump });
            dgvAttenEdges.Location = new System.Drawing.Point(3, 3);
            dgvAttenEdges.Name = "dgvAttenEdges";
            dgvAttenEdges.Size = new System.Drawing.Size(210, 185);
            dgvAttenEdges.TabStop = false;
            //
            // colEdgeElem
            //
            colEdgeElem.HeaderText = "Elem";
            colEdgeElem.Name = "colEdgeElem";
            //
            // colEdgeZ
            //
            colEdgeZ.HeaderText = "Z";
            colEdgeZ.Name = "colEdgeZ";
            //
            // colEdgeEdge
            //
            colEdgeEdge.HeaderText = "Edge";
            colEdgeEdge.Name = "colEdgeEdge";
            //
            // colEdgeKeV
            //
            colEdgeKeV.HeaderText = "keV";
            colEdgeKeV.Name = "colEdgeKeV";
            //
            // colEdgeJump
            //
            colEdgeJump.HeaderText = "Jump";
            colEdgeJump.Name = "colEdgeJump";
            //
            // dgvAttenElectron
            //
            dgvAttenElectron.AllowVerticalScroll = true;
            dgvAttenElectron.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colElecElem, colElecZ, colElecAt, colElecA });
            dgvAttenElectron.Location = new System.Drawing.Point(3, 194);
            dgvAttenElectron.Name = "dgvAttenElectron";
            dgvAttenElectron.Size = new System.Drawing.Size(210, 185);
            dgvAttenElectron.TabStop = false;
            //
            // colElecElem
            //
            colElecElem.HeaderText = "Elem";
            colElecElem.Name = "colElecElem";
            //
            // colElecZ
            //
            colElecZ.HeaderText = "Z";
            colElecZ.Name = "colElecZ";
            //
            // colElecAt
            //
            colElecAt.HeaderText = "at%";
            colElecAt.Name = "colElecAt";
            //
            // colElecA
            //
            colElecA.HeaderText = "σ (nm²)";// 260606Cl A(原子量)→ 元素別弾性断面積 σ に変更 (§6.5。フィールド名は colElecAt との接頭辞衝突回避で colElecA 据え置き)
            colElecA.Name = "colElecA";
            //
            // dgvAttenNeutron
            //
            dgvAttenNeutron.AllowVerticalScroll = true;
            dgvAttenNeutron.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colNeutElem, colNeutBcoh, colNeutScoh, colNeutAt });
            dgvAttenNeutron.Location = new System.Drawing.Point(3, 385);
            dgvAttenNeutron.Name = "dgvAttenNeutron";
            dgvAttenNeutron.Size = new System.Drawing.Size(210, 185);
            dgvAttenNeutron.TabStop = false;
            //
            // colNeutElem
            //
            colNeutElem.HeaderText = "Elem";
            colNeutElem.Name = "colNeutElem";
            //
            // colNeutBcoh
            //
            colNeutBcoh.HeaderText = "b_coh (fm)";
            colNeutBcoh.Name = "colNeutBcoh";
            //
            // colNeutScoh
            //
            colNeutScoh.HeaderText = "σ_coh (barn)";
            colNeutScoh.Name = "colNeutScoh";
            //
            // colNeutAt
            //
            colNeutAt.HeaderText = "at%";
            colNeutAt.Name = "colNeutAt";
            // 
            // tabPageFluorescence
            // 
            tabPageFluorescence.Controls.Add(tlpFluor);
            tabPageFluorescence.Controls.Add(labelFluorNA);
            resources.ApplyResources(tabPageFluorescence, "tabPageFluorescence");
            tabPageFluorescence.Name = "tabPageFluorescence";
            tabPageFluorescence.UseVisualStyleBackColor = true;
            // 
            // tlpFluor
            // 
            resources.ApplyResources(tlpFluor, "tlpFluor");
            tlpFluor.Controls.Add(graphFluor, 0, 0);
            tlpFluor.Controls.Add(dgvFluorScalars, 1, 0);
            tlpFluor.Controls.Add(dgvFluorLines, 1, 1);
            tlpFluor.Name = "tlpFluor";
            // 
            // graphFluor
            // 
            graphFluor.AllowMouseOperation = true;
            graphFluor.AxisLineColor = System.Drawing.Color.Gray;
            graphFluor.AxisTextColor = System.Drawing.Color.Black;
            graphFluor.AxisTextFont = new System.Drawing.Font("Segoe UI", 9F);
            graphFluor.AxisXTextVisible = true;
            graphFluor.AxisYTextVisible = true;
            graphFluor.BackgroundColor = System.Drawing.Color.White;
            graphFluor.BottomMargin = 0D;
            graphFluor.DivisionLineColor = System.Drawing.Color.LightGray;
            graphFluor.DivisionLineXVisible = true;
            graphFluor.DivisionLineYVisible = true;
            resources.ApplyResources(graphFluor, "graphFluor");
            graphFluor.FixRangeHorizontal = false;
            graphFluor.FixRangeVertical = false;
            graphFluor.GraphTitle = "";
            graphFluor.IsIntegerX = false;
            graphFluor.IsIntegerY = false;
            graphFluor.LabelX = "X:";
            graphFluor.LabelY = "Y:";
            graphFluor.LeftMargin = 0F;
            graphFluor.LineWidth = 1F;
            graphFluor.LowerX = 0D;
            graphFluor.LowerY = 0D;
            graphFluor.MaximalX = 1D;
            graphFluor.MaximalY = 1D;
            graphFluor.MinimalX = 0D;
            graphFluor.MinimalY = 0D;
            graphFluor.Mode = GraphControl.DrawingMode.Line;
            graphFluor.MousePositionVisible = false;
            graphFluor.MousePositionXDigit = -1;
            graphFluor.MousePositionYDigit = -1;
            graphFluor.Name = "graphFluor";
            graphFluor.OriginPosition = new System.Drawing.Point(40, 20);
            tlpFluor.SetRowSpan(graphFluor, 2);
            graphFluor.UnitX = "";
            graphFluor.UnitY = "";
            graphFluor.UpperPanelFont = new System.Drawing.Font("Segoe UI", 9F);
            graphFluor.UpperPanelVisible = false;
            graphFluor.UpperX = 1D;
            graphFluor.UpperY = 1D;
            graphFluor.UseLineWidth = true;
            graphFluor.VerticalLineColor = System.Drawing.Color.Red;
            graphFluor.VerticalLineMarkerRadius = 3.5F;
            graphFluor.VerticalLineMarkerVisible = false;
            graphFluor.XLog = false;
            graphFluor.YLog = false;
            // 
            // dgvFluorScalars
            // 
            resources.ApplyResources(dgvFluorScalars, "dgvFluorScalars");
            dgvFluorScalars.Name = "dgvFluorScalars";
            dgvFluorScalars.TabStop = false;
            //
            // dgvFluorLines
            //
            dgvFluorLines.AllowVerticalScroll = true;
            dgvFluorLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colFlElem, colFlLine, colFlE, colFlRelI, colFlOmega });
            resources.ApplyResources(dgvFluorLines, "dgvFluorLines");
            dgvFluorLines.Name = "dgvFluorLines";
            dgvFluorLines.TabStop = false;
            //
            // colFlElem
            //
            colFlElem.HeaderText = "Elem";
            colFlElem.Name = "colFlElem";
            //
            // colFlLine
            //
            colFlLine.HeaderText = "Line";
            colFlLine.Name = "colFlLine";
            //
            // colFlE
            //
            colFlE.HeaderText = "E keV";
            colFlE.Name = "colFlE";
            //
            // colFlRelI
            //
            colFlRelI.HeaderText = "Rel.I";
            colFlRelI.Name = "colFlRelI";
            //
            // colFlOmega
            //
            colFlOmega.HeaderText = "ω";
            colFlOmega.Name = "colFlOmega";
            // 
            // labelFluorNA
            // 
            resources.ApplyResources(labelFluorNA, "labelFluorNA");
            labelFluorNA.Name = "labelFluorNA";
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(waveLengthControl1);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            // 
            // FormScatteringFactor
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(tabControl1);
            Controls.Add(flowLayoutPanel5);
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
            tabControl1.ResumeLayout(false);
            tabPageReflections.ResumeLayout(false);
            tabPageReflections.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tabPageScatteringFactors.ResumeLayout(false);
            tabPageScatteringFactors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)miniTable1).EndInit();
            flowLayoutPanelModel.ResumeLayout(false);
            flowLayoutPanelModel.PerformLayout();
            flowLayoutPanelModel_Xray.ResumeLayout(false);
            flowLayoutPanelModel_Xray.PerformLayout();
            flowLayoutPanelModel_Electron.ResumeLayout(false);
            flowLayoutPanelModel_Electron.PerformLayout();
            tabPageAttenuations.ResumeLayout(false);
            tlpAtten.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAttenScalars).EndInit();
            flowAttenDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAttenEdges).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAttenElectron).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvAttenNeutron).EndInit();
            tabPageFluorescence.ResumeLayout(false);
            tlpFluor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvFluorScalars).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFluorLines).EndInit();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxHideEquivalentPlane;
        private System.Windows.Forms.CheckBox checkBoxHideProhibitedPlanes;
        // private System.Windows.Forms.DataGridView dataGridView; // 260518Cl 旧実装
        private DpiAwareDataGridView dataGridView; // 260518Cl
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
        private NumericBox numericBoxCutoffD;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageReflections;
        private System.Windows.Forms.TabPage tabPageScatteringFactors;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
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
        private GraphControl graphControlScatteringFactor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private GraphControl graphControl2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPageAttenuations;
        private System.Windows.Forms.TabPage tabPageFluorescence;
        private System.Windows.Forms.TableLayoutPanel tlpAtten;     // 260606Cl
        private GraphControl graphAtten;                            // 260606Cl
        private NumericBox numAttenThickness;                       // 260606Cl
        private MiniTable dgvAttenScalars;                          // 260606Cl
        private System.Windows.Forms.FlowLayoutPanel flowAttenDetail; // 260606Cl
        private MiniTable dgvAttenEdges;                            // 260606Cl
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdgeElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdgeZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdgeEdge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdgeKeV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdgeJump;
        private MiniTable dgvAttenElectron;                         // 260606Cl
        private System.Windows.Forms.DataGridViewTextBoxColumn colElecElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colElecZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colElecAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colElecA;
        private MiniTable dgvAttenNeutron;                          // 260606Cl
        private System.Windows.Forms.DataGridViewTextBoxColumn colNeutElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNeutBcoh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNeutScoh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNeutAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlRelI;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlOmega;
        private System.Windows.Forms.TableLayoutPanel tlpFluor;     // 260606Cl
        private GraphControl graphFluor;                            // 260606Cl
        private MiniTable dgvFluorScalars;                          // 260606Cl
        private MiniTable dgvFluorLines;                            // 260606Cl
        private System.Windows.Forms.Label labelFluorNA;            // 260606Cl
        // private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8; // 260606Cl 旧名(リネーム取りこぼし)
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelModel_Electron; // 260606Cl
        private System.Windows.Forms.RadioButton radioButtonElectronPeng;
        private System.Windows.Forms.RadioButton radioButtonElectronKirkland;
        private System.Windows.Forms.RadioButton radioButtonElectronEightGaussian;
        // private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7; // 260606Cl 旧名(リネーム取りこぼし)
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelModel_Xray; // 260606Cl
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.RadioButton radioButtonXrayFqSq;
        private System.Windows.Forms.RadioButton radioButtonXrayFs;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelModel;
        private System.Windows.Forms.CheckBox checkBoxDebyeWaller;
        private MiniTable miniTable1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}
