namespace Crystallography.Controls
{
    partial class FormBeamInteraction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBeamInteraction));
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
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            dataGridView = new DpiAwareDataGridView();
            dataGridViewTextBoxColumnH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumnK = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            dataGridViewTextBoxColumnI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            bindingSourceScatteringFactor = new System.Windows.Forms.BindingSource(components);
            dataSet = new DataSet();
            graphControlReflections = new GraphControl();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            comboBoxReflXAxis = new System.Windows.Forms.ComboBox();
            checkBoxReflLog = new System.Windows.Forms.CheckBox();
            checkBoxHideProhibitedPlanes = new System.Windows.Forms.CheckBox();
            checkBoxHideEquivalentPlane = new System.Windows.Forms.CheckBox();
            buttonCopyClipboard = new System.Windows.Forms.Button();
            checkBoxBragBrentano = new System.Windows.Forms.CheckBox();
            waveLengthControl = new WaveLengthControl();
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
            numericBoxAttenThickness = new NumericBox();
            panel1 = new System.Windows.Forms.Panel();
            tabControl = new System.Windows.Forms.TabControl();
            tabPageReflections = new System.Windows.Forms.TabPage();
            panel5 = new System.Windows.Forms.Panel();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            tabPageAttenuations = new System.Windows.Forms.TabPage();
            graphControlAtten = new GraphControl();
            flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            miniTableAttenScalars = new MiniTable();
            miniTableAttenEdges = new MiniTable();
            colEdgeElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEdgeZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEdgeEdge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEdgeKeV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colEdgeJump = new System.Windows.Forms.DataGridViewTextBoxColumn();
            miniTableAttenElectron = new MiniTable();
            colElecElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colElecZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colElecAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colElecA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            miniTableAttenNeutron = new MiniTable();
            colNeutElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNeutBcoh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNeutScoh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colNeutAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            flowLayoutPanelAttenuationModel = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelAttenCoeff = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonAttenMassMu = new System.Windows.Forms.RadioButton();
            radioButtonAttenLinMu = new System.Windows.Forms.RadioButton();
            radioButtonAttenTrans = new System.Windows.Forms.RadioButton();
            flowLayoutPanelElecQuantity = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonElecAll = new System.Windows.Forms.RadioButton();
            radioButtonElecSigma = new System.Windows.Forms.RadioButton();
            radioButtonElecEMFP = new System.Windows.Forms.RadioButton();
            radioButtonElecDeds = new System.Windows.Forms.RadioButton();
            radioButtonElecIMFP = new System.Windows.Forms.RadioButton();
            radioButtonElecRange = new System.Windows.Forms.RadioButton();
            tabPageScatteringFactors = new System.Windows.Forms.TabPage();
            graphControlScatteringFactor = new GraphControl();
            flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            miniTableScatteringFactorsXray = new MiniTable();
            colSfxElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSfxZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSfxFs = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSfxFp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSfxFpp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            miniTableScatteringFactorsElectron = new MiniTable();
            colSfeElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSfeZ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSfeFe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSfeModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            miniTableScatteringFactorsNeutron = new MiniTable();
            colSfnElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSfnBcoh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSfnScoh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colSfnSinc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            flowLayoutPanelScatteringFactorModel = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelModel_Xray = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelModel_Electron = new System.Windows.Forms.FlowLayoutPanel();
            tabPageFluorescence = new System.Windows.Forms.TabPage();
            panel4 = new System.Windows.Forms.Panel();
            graphControlFluor = new GraphControl();
            flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            miniTableFluorScalars = new MiniTable();
            miniTableFluorLines = new MiniTable();
            colFlElem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFlLine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFlE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFlRelI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            colFlOmega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceScatteringFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            flowLayoutPanel6.SuspendLayout();
            panel1.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageReflections.SuspendLayout();
            panel5.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tabPageAttenuations.SuspendLayout();
            flowLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)miniTableAttenScalars).BeginInit();
            ((System.ComponentModel.ISupportInitialize)miniTableAttenEdges).BeginInit();
            ((System.ComponentModel.ISupportInitialize)miniTableAttenElectron).BeginInit();
            ((System.ComponentModel.ISupportInitialize)miniTableAttenNeutron).BeginInit();
            flowLayoutPanelAttenuationModel.SuspendLayout();
            flowLayoutPanelAttenCoeff.SuspendLayout();
            flowLayoutPanelElecQuantity.SuspendLayout();
            tabPageScatteringFactors.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)miniTableScatteringFactorsXray).BeginInit();
            ((System.ComponentModel.ISupportInitialize)miniTableScatteringFactorsElectron).BeginInit();
            ((System.ComponentModel.ISupportInitialize)miniTableScatteringFactorsNeutron).BeginInit();
            flowLayoutPanelScatteringFactorModel.SuspendLayout();
            flowLayoutPanelModel_Xray.SuspendLayout();
            flowLayoutPanelModel_Electron.SuspendLayout();
            tabPageFluorescence.SuspendLayout();
            panel4.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)miniTableFluorScalars).BeginInit();
            ((System.ComponentModel.ISupportInitialize)miniTableFluorLines).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(splitContainer1.Panel1, "splitContainer1.Panel1");
            splitContainer1.Panel1.Controls.Add(dataGridView);
            toolTip.SetToolTip(splitContainer1.Panel1, resources.GetString("splitContainer1.Panel1.ToolTip"));
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(splitContainer1.Panel2, "splitContainer1.Panel2");
            splitContainer1.Panel2.Controls.Add(graphControlReflections);
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel6);
            toolTip.SetToolTip(splitContainer1.Panel2, resources.GetString("splitContainer1.Panel2.ToolTip"));
            toolTip.SetToolTip(splitContainer1, resources.GetString("splitContainer1.ToolTip"));
            // 
            // dataGridView
            // 
            resources.ApplyResources(dataGridView, "dataGridView");
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
            dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dataGridViewTextBoxColumnH, dataGridViewTextBoxColumnK, dataGridViewTextBoxColumnL, dataGridViewTextBoxColumnMulti, dataGridViewTextBoxColumnD, dataGridViewTextBoxColumnQ, dataGridViewTextBoxColumnTwoTheta, dataGridViewTextBoxColumnFreal, dataGridViewTextBoxColumnFinv, dataGridViewTextBoxColumnFabs, dataGridViewTextBoxColumnFsq, dataGridViewTextBoxColumnIntPercent, dataGridViewTextBoxColumnIntCondition, dataGridViewTextBoxColumnI });
            dataGridView.DataSource = bindingSourceScatteringFactor;
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
            // dataGridViewTextBoxColumnI
            // 
            resources.ApplyResources(dataGridViewTextBoxColumnI, "dataGridViewTextBoxColumnI");
            dataGridViewTextBoxColumnI.Name = "dataGridViewTextBoxColumnI";
            dataGridViewTextBoxColumnI.ReadOnly = true;
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
            // graphControlReflections
            // 
            resources.ApplyResources(graphControlReflections, "graphControlReflections");
            graphControlReflections.CopyVisible = true;
            graphControlReflections.FixLowerXToZero = true;
            graphControlReflections.Name = "graphControlReflections";
            graphControlReflections.RangePanelVisible = true;
            toolTip.SetToolTip(graphControlReflections, resources.GetString("graphControlReflections.ToolTip"));
            // 
            // flowLayoutPanel6
            // 
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Controls.Add(comboBoxReflXAxis);
            flowLayoutPanel6.Controls.Add(checkBoxReflLog);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            toolTip.SetToolTip(flowLayoutPanel6, resources.GetString("flowLayoutPanel6.ToolTip"));
            // 
            // comboBoxReflXAxis
            // 
            resources.ApplyResources(comboBoxReflXAxis, "comboBoxReflXAxis");
            comboBoxReflXAxis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxReflXAxis.FormattingEnabled = true;
            comboBoxReflXAxis.Name = "comboBoxReflXAxis";
            toolTip.SetToolTip(comboBoxReflXAxis, resources.GetString("comboBoxReflXAxis.ToolTip"));
            comboBoxReflXAxis.SelectedIndexChanged += reflectionsView_OptionChanged;
            // 
            // checkBoxReflLog
            // 
            resources.ApplyResources(checkBoxReflLog, "checkBoxReflLog");
            checkBoxReflLog.Name = "checkBoxReflLog";
            toolTip.SetToolTip(checkBoxReflLog, resources.GetString("checkBoxReflLog.ToolTip"));
            checkBoxReflLog.UseVisualStyleBackColor = true;
            checkBoxReflLog.CheckedChanged += reflectionsView_OptionChanged;
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
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            captureExtender.SetCapture(waveLengthControl, true);
            waveLengthControl.DirectionWaveEnergy = System.Windows.Forms.FlowDirection.LeftToRight;
            waveLengthControl.DirectionWhole = System.Windows.Forms.FlowDirection.LeftToRight;
            waveLengthControl.Energy = 8.04114721D;
            waveLengthControl.Name = "waveLengthControl";
            toolTip.SetToolTip(waveLengthControl, resources.GetString("waveLengthControl.ToolTip"));
            waveLengthControl.WaveLength = 0.154187106667D;
            waveLengthControl.XrayWaveSourceElementNumber = 29;
            waveLengthControl.XrayWaveSourceLine = XrayLine.Ka;
            waveLengthControl.WavelengthChanged += waveLengthControl1_WavelengthChanged;
            waveLengthControl.WavelengthUnitChanged += radioButtonNanoMeter_CheckedChanged;
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
            resources.ApplyResources(numericBoxL_step, "numericBoxL_step");
            numericBoxL_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxL_step.DecimalPlaces = 3;
            numericBoxL_step.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxL_step.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxL_step.Maximum = 1D;
            numericBoxL_step.Minimum = 0.001D;
            numericBoxL_step.Name = "numericBoxL_step";
            numericBoxL_step.RadianValue = 0.00017453292519943296D;
            numericBoxL_step.ShowUpDown = true;
            numericBoxL_step.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxL_step, resources.GetString("numericBoxL_step.ToolTip"));
            numericBoxL_step.Value = 0.01D;
            numericBoxL_step.ValueFontSize = 9F;
            numericBoxL_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_step
            // 
            resources.ApplyResources(numericBoxK_step, "numericBoxK_step");
            numericBoxK_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_step.DecimalPlaces = 3;
            numericBoxK_step.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxK_step.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxK_step.Maximum = 1D;
            numericBoxK_step.Minimum = 0.001D;
            numericBoxK_step.Name = "numericBoxK_step";
            numericBoxK_step.RadianValue = 0.00017453292519943296D;
            numericBoxK_step.ShowUpDown = true;
            numericBoxK_step.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxK_step, resources.GetString("numericBoxK_step.ToolTip"));
            numericBoxK_step.Value = 0.01D;
            numericBoxK_step.ValueFontSize = 9F;
            numericBoxK_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_step
            // 
            resources.ApplyResources(numericBoxH_step, "numericBoxH_step");
            numericBoxH_step.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_step.DecimalPlaces = 3;
            numericBoxH_step.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxH_step.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxH_step.Maximum = 1D;
            numericBoxH_step.Minimum = 0.001D;
            numericBoxH_step.Name = "numericBoxH_step";
            numericBoxH_step.RadianValue = 0.00017453292519943296D;
            numericBoxH_step.ShowUpDown = true;
            numericBoxH_step.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxH_step, resources.GetString("numericBoxH_step.ToolTip"));
            numericBoxH_step.Value = 0.01D;
            numericBoxH_step.ValueFontSize = 9F;
            numericBoxH_step.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_max
            // 
            resources.ApplyResources(numericBoxL_max, "numericBoxL_max");
            numericBoxL_max.BackColor = System.Drawing.Color.Transparent;
            numericBoxL_max.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxL_max.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxL_max.Name = "numericBoxL_max";
            numericBoxL_max.RadianValue = 0.017453292519943295D;
            numericBoxL_max.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxL_max, resources.GetString("numericBoxL_max.ToolTip"));
            numericBoxL_max.Value = 1D;
            numericBoxL_max.ValueFontSize = 9F;
            numericBoxL_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_max
            // 
            resources.ApplyResources(numericBoxK_max, "numericBoxK_max");
            numericBoxK_max.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_max.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxK_max.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxK_max.Name = "numericBoxK_max";
            numericBoxK_max.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxK_max, resources.GetString("numericBoxK_max.ToolTip"));
            numericBoxK_max.ValueFontSize = 9F;
            numericBoxK_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxL_min
            // 
            resources.ApplyResources(numericBoxL_min, "numericBoxL_min");
            numericBoxL_min.BackColor = System.Drawing.Color.Transparent;
            numericBoxL_min.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxL_min.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxL_min.Name = "numericBoxL_min";
            numericBoxL_min.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxL_min, resources.GetString("numericBoxL_min.ToolTip"));
            numericBoxL_min.ValueFontSize = 9F;
            numericBoxL_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxK_min
            // 
            resources.ApplyResources(numericBoxK_min, "numericBoxK_min");
            numericBoxK_min.BackColor = System.Drawing.Color.Transparent;
            numericBoxK_min.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxK_min.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxK_min.Name = "numericBoxK_min";
            numericBoxK_min.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxK_min, resources.GetString("numericBoxK_min.ToolTip"));
            numericBoxK_min.ValueFontSize = 9F;
            numericBoxK_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_max
            // 
            resources.ApplyResources(numericBoxH_max, "numericBoxH_max");
            numericBoxH_max.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_max.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxH_max.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxH_max.Name = "numericBoxH_max";
            numericBoxH_max.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxH_max, resources.GetString("numericBoxH_max.ToolTip"));
            numericBoxH_max.ValueFontSize = 9F;
            numericBoxH_max.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxH_min
            // 
            resources.ApplyResources(numericBoxH_min, "numericBoxH_min");
            numericBoxH_min.BackColor = System.Drawing.Color.Transparent;
            numericBoxH_min.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxH_min.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxH_min.Name = "numericBoxH_min";
            numericBoxH_min.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxH_min, resources.GetString("numericBoxH_min.ToolTip"));
            numericBoxH_min.ValueFontSize = 9F;
            numericBoxH_min.ValueChanged += numericBoxH_min_ValueChanged;
            // 
            // numericBoxCutoffD
            // 
            resources.ApplyResources(numericBoxCutoffD, "numericBoxCutoffD");
            numericBoxCutoffD.BackColor = System.Drawing.Color.Transparent;
            numericBoxCutoffD.DecimalPlaces = 3;
            numericBoxCutoffD.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxCutoffD.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxCutoffD.Maximum = 10D;
            numericBoxCutoffD.Minimum = 0.001D;
            numericBoxCutoffD.Name = "numericBoxCutoffD";
            numericBoxCutoffD.RadianValue = 0.017453292519943295D;
            numericBoxCutoffD.ShowUpDown = true;
            numericBoxCutoffD.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxCutoffD, resources.GetString("numericBoxCutoffD.ToolTip"));
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
            radioButtonXrayFs.CheckedChanged += scattering_OptionChanged;
            // 
            // radioButtonXrayFqSq
            // 
            resources.ApplyResources(radioButtonXrayFqSq, "radioButtonXrayFqSq");
            radioButtonXrayFqSq.Name = "radioButtonXrayFqSq";
            radioButtonXrayFqSq.TabStop = true;
            toolTip.SetToolTip(radioButtonXrayFqSq, resources.GetString("radioButtonXrayFqSq.ToolTip"));
            radioButtonXrayFqSq.UseVisualStyleBackColor = true;
            radioButtonXrayFqSq.CheckedChanged += scattering_OptionChanged;
            // 
            // radioButtonElectronPeng
            // 
            resources.ApplyResources(radioButtonElectronPeng, "radioButtonElectronPeng");
            radioButtonElectronPeng.Name = "radioButtonElectronPeng";
            radioButtonElectronPeng.TabStop = true;
            toolTip.SetToolTip(radioButtonElectronPeng, resources.GetString("radioButtonElectronPeng.ToolTip"));
            radioButtonElectronPeng.UseVisualStyleBackColor = true;
            radioButtonElectronPeng.CheckedChanged += scattering_OptionChanged;
            // 
            // radioButtonElectronKirkland
            // 
            resources.ApplyResources(radioButtonElectronKirkland, "radioButtonElectronKirkland");
            radioButtonElectronKirkland.Name = "radioButtonElectronKirkland";
            radioButtonElectronKirkland.TabStop = true;
            toolTip.SetToolTip(radioButtonElectronKirkland, resources.GetString("radioButtonElectronKirkland.ToolTip"));
            radioButtonElectronKirkland.UseVisualStyleBackColor = true;
            radioButtonElectronKirkland.CheckedChanged += scattering_OptionChanged;
            // 
            // radioButtonElectronEightGaussian
            // 
            resources.ApplyResources(radioButtonElectronEightGaussian, "radioButtonElectronEightGaussian");
            radioButtonElectronEightGaussian.Name = "radioButtonElectronEightGaussian";
            radioButtonElectronEightGaussian.TabStop = true;
            toolTip.SetToolTip(radioButtonElectronEightGaussian, resources.GetString("radioButtonElectronEightGaussian.ToolTip"));
            radioButtonElectronEightGaussian.UseVisualStyleBackColor = true;
            radioButtonElectronEightGaussian.CheckedChanged += scattering_OptionChanged;
            // 
            // checkBoxDebyeWaller
            // 
            resources.ApplyResources(checkBoxDebyeWaller, "checkBoxDebyeWaller");
            checkBoxDebyeWaller.Name = "checkBoxDebyeWaller";
            toolTip.SetToolTip(checkBoxDebyeWaller, resources.GetString("checkBoxDebyeWaller.ToolTip"));
            checkBoxDebyeWaller.UseVisualStyleBackColor = true;
            checkBoxDebyeWaller.CheckedChanged += scattering_OptionChanged;
            // 
            // numericBoxAttenThickness
            // 
            resources.ApplyResources(numericBoxAttenThickness, "numericBoxAttenThickness");
            numericBoxAttenThickness.BackColor = System.Drawing.Color.Transparent;
            numericBoxAttenThickness.FooterBackColor = System.Drawing.Color.Transparent;
            numericBoxAttenThickness.HeaderBackColor = System.Drawing.Color.Transparent;
            numericBoxAttenThickness.Maximum = 10000000D;
            numericBoxAttenThickness.Minimum = 0D;
            numericBoxAttenThickness.Name = "numericBoxAttenThickness";
            numericBoxAttenThickness.RadianValue = 1.7453292519943295D;
            toolTip.SetToolTip(numericBoxAttenThickness, resources.GetString("numericBoxAttenThickness.ToolTip"));
            numericBoxAttenThickness.Value = 100D;
            numericBoxAttenThickness.ValueChanged += numericBoxAttenThickness_ValueChanged;
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
            // tabControl
            // 
            resources.ApplyResources(tabControl, "tabControl");
            tabControl.Controls.Add(tabPageReflections);
            tabControl.Controls.Add(tabPageAttenuations);
            tabControl.Controls.Add(tabPageScatteringFactors);
            tabControl.Controls.Add(tabPageFluorescence);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            toolTip.SetToolTip(tabControl, resources.GetString("tabControl.ToolTip"));
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabPageReflections
            // 
            resources.ApplyResources(tabPageReflections, "tabPageReflections");
            tabPageReflections.Controls.Add(splitContainer1);
            tabPageReflections.Controls.Add(panel5);
            tabPageReflections.Controls.Add(flowLayoutPanel3);
            tabPageReflections.Controls.Add(flowLayoutPanel1);
            tabPageReflections.Controls.Add(flowLayoutPanel4);
            tabPageReflections.Name = "tabPageReflections";
            toolTip.SetToolTip(tabPageReflections, resources.GetString("tabPageReflections.ToolTip"));
            tabPageReflections.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            resources.ApplyResources(panel5, "panel5");
            panel5.Controls.Add(buttonCopyClipboard);
            panel5.Controls.Add(numericBoxCutoffD);
            panel5.Name = "panel5";
            toolTip.SetToolTip(panel5, resources.GetString("panel5.ToolTip"));
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(checkBoxTest);
            flowLayoutPanel3.Controls.Add(panel1);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            toolTip.SetToolTip(flowLayoutPanel3, resources.GetString("flowLayoutPanel3.ToolTip"));
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(checkBoxBragBrentano);
            flowLayoutPanel1.Controls.Add(checkBoxHideProhibitedPlanes);
            flowLayoutPanel1.Controls.Add(checkBoxHideEquivalentPlane);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            toolTip.SetToolTip(flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            toolTip.SetToolTip(flowLayoutPanel4, resources.GetString("flowLayoutPanel4.ToolTip"));
            // 
            // tabPageAttenuations
            // 
            resources.ApplyResources(tabPageAttenuations, "tabPageAttenuations");
            tabPageAttenuations.Controls.Add(graphControlAtten);
            tabPageAttenuations.Controls.Add(flowLayoutPanel9);
            tabPageAttenuations.Controls.Add(flowLayoutPanelAttenuationModel);
            tabPageAttenuations.Name = "tabPageAttenuations";
            toolTip.SetToolTip(tabPageAttenuations, resources.GetString("tabPageAttenuations.ToolTip"));
            tabPageAttenuations.UseVisualStyleBackColor = true;
            // 
            // graphControlAtten
            // 
            resources.ApplyResources(graphControlAtten, "graphControlAtten");
            graphControlAtten.CopyVisible = true;
            graphControlAtten.FixLowerXToZero = true;
            graphControlAtten.Name = "graphControlAtten";
            graphControlAtten.RangePanelVisible = true;
            toolTip.SetToolTip(graphControlAtten, resources.GetString("graphControlAtten.ToolTip"));
            graphControlAtten.VerticalLineMarkerVisible = true;
            // 
            // flowLayoutPanel9
            // 
            resources.ApplyResources(flowLayoutPanel9, "flowLayoutPanel9");
            flowLayoutPanel9.Controls.Add(miniTableAttenScalars);
            flowLayoutPanel9.Controls.Add(miniTableAttenEdges);
            flowLayoutPanel9.Controls.Add(miniTableAttenElectron);
            flowLayoutPanel9.Controls.Add(miniTableAttenNeutron);
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            toolTip.SetToolTip(flowLayoutPanel9, resources.GetString("flowLayoutPanel9.ToolTip"));
            // 
            // miniTableAttenScalars
            // 
            resources.ApplyResources(miniTableAttenScalars, "miniTableAttenScalars");
            miniTableAttenScalars.AutoFitHeight = true;
            miniTableAttenScalars.Name = "miniTableAttenScalars";
            miniTableAttenScalars.TabStop = false;
            toolTip.SetToolTip(miniTableAttenScalars, resources.GetString("miniTableAttenScalars.ToolTip"));
            // 
            // miniTableAttenEdges
            // 
            resources.ApplyResources(miniTableAttenEdges, "miniTableAttenEdges");
            miniTableAttenEdges.AllowVerticalScroll = true;
            miniTableAttenEdges.AutoFitHeight = true;
            miniTableAttenEdges.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colEdgeElem, colEdgeZ, colEdgeEdge, colEdgeKeV, colEdgeJump });
            miniTableAttenEdges.Name = "miniTableAttenEdges";
            miniTableAttenEdges.TabStop = false;
            toolTip.SetToolTip(miniTableAttenEdges, resources.GetString("miniTableAttenEdges.ToolTip"));
            // 
            // colEdgeElem
            // 
            colEdgeElem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colEdgeElem, "colEdgeElem");
            colEdgeElem.Name = "colEdgeElem";
            colEdgeElem.ReadOnly = true;
            // 
            // colEdgeZ
            // 
            colEdgeZ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colEdgeZ, "colEdgeZ");
            colEdgeZ.Name = "colEdgeZ";
            colEdgeZ.ReadOnly = true;
            // 
            // colEdgeEdge
            // 
            colEdgeEdge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colEdgeEdge, "colEdgeEdge");
            colEdgeEdge.Name = "colEdgeEdge";
            colEdgeEdge.ReadOnly = true;
            // 
            // colEdgeKeV
            // 
            colEdgeKeV.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colEdgeKeV, "colEdgeKeV");
            colEdgeKeV.Name = "colEdgeKeV";
            colEdgeKeV.ReadOnly = true;
            // 
            // colEdgeJump
            // 
            colEdgeJump.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colEdgeJump, "colEdgeJump");
            colEdgeJump.Name = "colEdgeJump";
            colEdgeJump.ReadOnly = true;
            // 
            // miniTableAttenElectron
            // 
            resources.ApplyResources(miniTableAttenElectron, "miniTableAttenElectron");
            miniTableAttenElectron.AllowVerticalScroll = true;
            miniTableAttenElectron.AutoFitHeight = true;
            miniTableAttenElectron.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colElecElem, colElecZ, colElecAt, colElecA });
            miniTableAttenElectron.Name = "miniTableAttenElectron";
            miniTableAttenElectron.TabStop = false;
            toolTip.SetToolTip(miniTableAttenElectron, resources.GetString("miniTableAttenElectron.ToolTip"));
            // 
            // colElecElem
            // 
            colElecElem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colElecElem, "colElecElem");
            colElecElem.Name = "colElecElem";
            colElecElem.ReadOnly = true;
            // 
            // colElecZ
            // 
            colElecZ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colElecZ, "colElecZ");
            colElecZ.Name = "colElecZ";
            colElecZ.ReadOnly = true;
            // 
            // colElecAt
            // 
            colElecAt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colElecAt, "colElecAt");
            colElecAt.Name = "colElecAt";
            colElecAt.ReadOnly = true;
            // 
            // colElecA
            // 
            colElecA.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colElecA, "colElecA");
            colElecA.Name = "colElecA";
            colElecA.ReadOnly = true;
            // 
            // miniTableAttenNeutron
            // 
            resources.ApplyResources(miniTableAttenNeutron, "miniTableAttenNeutron");
            miniTableAttenNeutron.AllowVerticalScroll = true;
            miniTableAttenNeutron.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colNeutElem, colNeutBcoh, colNeutScoh, colNeutAt });
            miniTableAttenNeutron.Name = "miniTableAttenNeutron";
            miniTableAttenNeutron.TabStop = false;
            toolTip.SetToolTip(miniTableAttenNeutron, resources.GetString("miniTableAttenNeutron.ToolTip"));
            // 
            // colNeutElem
            // 
            colNeutElem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colNeutElem, "colNeutElem");
            colNeutElem.Name = "colNeutElem";
            colNeutElem.ReadOnly = true;
            // 
            // colNeutBcoh
            // 
            colNeutBcoh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colNeutBcoh, "colNeutBcoh");
            colNeutBcoh.Name = "colNeutBcoh";
            colNeutBcoh.ReadOnly = true;
            // 
            // colNeutScoh
            // 
            colNeutScoh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colNeutScoh, "colNeutScoh");
            colNeutScoh.Name = "colNeutScoh";
            colNeutScoh.ReadOnly = true;
            // 
            // colNeutAt
            // 
            colNeutAt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colNeutAt, "colNeutAt");
            colNeutAt.Name = "colNeutAt";
            colNeutAt.ReadOnly = true;
            // 
            // flowLayoutPanelAttenuationModel
            // 
            resources.ApplyResources(flowLayoutPanelAttenuationModel, "flowLayoutPanelAttenuationModel");
            flowLayoutPanelAttenuationModel.Controls.Add(flowLayoutPanelAttenCoeff);
            flowLayoutPanelAttenuationModel.Controls.Add(flowLayoutPanelElecQuantity);
            flowLayoutPanelAttenuationModel.Name = "flowLayoutPanelAttenuationModel";
            toolTip.SetToolTip(flowLayoutPanelAttenuationModel, resources.GetString("flowLayoutPanelAttenuationModel.ToolTip"));
            // 
            // flowLayoutPanelAttenCoeff
            // 
            resources.ApplyResources(flowLayoutPanelAttenCoeff, "flowLayoutPanelAttenCoeff");
            flowLayoutPanelAttenCoeff.Controls.Add(radioButtonAttenMassMu);
            flowLayoutPanelAttenCoeff.Controls.Add(radioButtonAttenLinMu);
            flowLayoutPanelAttenCoeff.Controls.Add(radioButtonAttenTrans);
            flowLayoutPanelAttenCoeff.Controls.Add(numericBoxAttenThickness);
            flowLayoutPanelAttenCoeff.Name = "flowLayoutPanelAttenCoeff";
            toolTip.SetToolTip(flowLayoutPanelAttenCoeff, resources.GetString("flowLayoutPanelAttenCoeff.ToolTip"));
            // 
            // radioButtonAttenMassMu
            // 
            resources.ApplyResources(radioButtonAttenMassMu, "radioButtonAttenMassMu");
            radioButtonAttenMassMu.Checked = true;
            radioButtonAttenMassMu.Name = "radioButtonAttenMassMu";
            radioButtonAttenMassMu.TabStop = true;
            toolTip.SetToolTip(radioButtonAttenMassMu, resources.GetString("radioButtonAttenMassMu.ToolTip"));
            radioButtonAttenMassMu.UseVisualStyleBackColor = true;
            radioButtonAttenMassMu.CheckedChanged += attenCoeff_OptionChanged;
            // 
            // radioButtonAttenLinMu
            // 
            resources.ApplyResources(radioButtonAttenLinMu, "radioButtonAttenLinMu");
            radioButtonAttenLinMu.Name = "radioButtonAttenLinMu";
            toolTip.SetToolTip(radioButtonAttenLinMu, resources.GetString("radioButtonAttenLinMu.ToolTip"));
            radioButtonAttenLinMu.UseVisualStyleBackColor = true;
            radioButtonAttenLinMu.CheckedChanged += attenCoeff_OptionChanged;
            // 
            // radioButtonAttenTrans
            // 
            resources.ApplyResources(radioButtonAttenTrans, "radioButtonAttenTrans");
            radioButtonAttenTrans.Name = "radioButtonAttenTrans";
            toolTip.SetToolTip(radioButtonAttenTrans, resources.GetString("radioButtonAttenTrans.ToolTip"));
            radioButtonAttenTrans.UseVisualStyleBackColor = true;
            radioButtonAttenTrans.CheckedChanged += attenCoeff_OptionChanged;
            // 
            // flowLayoutPanelElecQuantity
            // 
            resources.ApplyResources(flowLayoutPanelElecQuantity, "flowLayoutPanelElecQuantity");
            flowLayoutPanelElecQuantity.Controls.Add(radioButtonElecAll);
            flowLayoutPanelElecQuantity.Controls.Add(radioButtonElecSigma);
            flowLayoutPanelElecQuantity.Controls.Add(radioButtonElecEMFP);
            flowLayoutPanelElecQuantity.Controls.Add(radioButtonElecDeds);
            flowLayoutPanelElecQuantity.Controls.Add(radioButtonElecIMFP);
            flowLayoutPanelElecQuantity.Controls.Add(radioButtonElecRange);
            flowLayoutPanelElecQuantity.Name = "flowLayoutPanelElecQuantity";
            toolTip.SetToolTip(flowLayoutPanelElecQuantity, resources.GetString("flowLayoutPanelElecQuantity.ToolTip"));
            // 
            // radioButtonElecAll
            // 
            resources.ApplyResources(radioButtonElecAll, "radioButtonElecAll");
            radioButtonElecAll.Checked = true;
            radioButtonElecAll.Name = "radioButtonElecAll";
            radioButtonElecAll.TabStop = true;
            toolTip.SetToolTip(radioButtonElecAll, resources.GetString("radioButtonElecAll.ToolTip"));
            radioButtonElecAll.UseVisualStyleBackColor = true;
            radioButtonElecAll.CheckedChanged += elecQuantity_OptionChanged;
            // 
            // radioButtonElecSigma
            // 
            resources.ApplyResources(radioButtonElecSigma, "radioButtonElecSigma");
            radioButtonElecSigma.Name = "radioButtonElecSigma";
            toolTip.SetToolTip(radioButtonElecSigma, resources.GetString("radioButtonElecSigma.ToolTip"));
            radioButtonElecSigma.UseVisualStyleBackColor = true;
            radioButtonElecSigma.CheckedChanged += elecQuantity_OptionChanged;
            // 
            // radioButtonElecEMFP
            // 
            resources.ApplyResources(radioButtonElecEMFP, "radioButtonElecEMFP");
            radioButtonElecEMFP.Name = "radioButtonElecEMFP";
            toolTip.SetToolTip(radioButtonElecEMFP, resources.GetString("radioButtonElecEMFP.ToolTip"));
            radioButtonElecEMFP.UseVisualStyleBackColor = true;
            radioButtonElecEMFP.CheckedChanged += elecQuantity_OptionChanged;
            // 
            // radioButtonElecDeds
            // 
            resources.ApplyResources(radioButtonElecDeds, "radioButtonElecDeds");
            radioButtonElecDeds.Name = "radioButtonElecDeds";
            toolTip.SetToolTip(radioButtonElecDeds, resources.GetString("radioButtonElecDeds.ToolTip"));
            radioButtonElecDeds.UseVisualStyleBackColor = true;
            radioButtonElecDeds.CheckedChanged += elecQuantity_OptionChanged;
            // 
            // radioButtonElecIMFP
            // 
            resources.ApplyResources(radioButtonElecIMFP, "radioButtonElecIMFP");
            radioButtonElecIMFP.Name = "radioButtonElecIMFP";
            toolTip.SetToolTip(radioButtonElecIMFP, resources.GetString("radioButtonElecIMFP.ToolTip"));
            radioButtonElecIMFP.UseVisualStyleBackColor = true;
            radioButtonElecIMFP.CheckedChanged += elecQuantity_OptionChanged;
            // 
            // radioButtonElecRange
            // 
            resources.ApplyResources(radioButtonElecRange, "radioButtonElecRange");
            radioButtonElecRange.Name = "radioButtonElecRange";
            toolTip.SetToolTip(radioButtonElecRange, resources.GetString("radioButtonElecRange.ToolTip"));
            radioButtonElecRange.UseVisualStyleBackColor = true;
            radioButtonElecRange.CheckedChanged += elecQuantity_OptionChanged;
            // 
            // tabPageScatteringFactors
            // 
            resources.ApplyResources(tabPageScatteringFactors, "tabPageScatteringFactors");
            tabPageScatteringFactors.Controls.Add(graphControlScatteringFactor);
            tabPageScatteringFactors.Controls.Add(flowLayoutPanel8);
            tabPageScatteringFactors.Controls.Add(flowLayoutPanelScatteringFactorModel);
            tabPageScatteringFactors.Name = "tabPageScatteringFactors";
            toolTip.SetToolTip(tabPageScatteringFactors, resources.GetString("tabPageScatteringFactors.ToolTip"));
            tabPageScatteringFactors.UseVisualStyleBackColor = true;
            // 
            // graphControlScatteringFactor
            // 
            resources.ApplyResources(graphControlScatteringFactor, "graphControlScatteringFactor");
            graphControlScatteringFactor.CopyVisible = true;
            graphControlScatteringFactor.FixLowerXToZero = true;
            graphControlScatteringFactor.Name = "graphControlScatteringFactor";
            graphControlScatteringFactor.RangePanelVisible = true;
            toolTip.SetToolTip(graphControlScatteringFactor, resources.GetString("graphControlScatteringFactor.ToolTip"));
            graphControlScatteringFactor.VerticalLineMarkerVisible = true;
            graphControlScatteringFactor.LinePositionChanged += scattering_LinePositionChanged;
            // 
            // flowLayoutPanel8
            // 
            resources.ApplyResources(flowLayoutPanel8, "flowLayoutPanel8");
            flowLayoutPanel8.Controls.Add(miniTableScatteringFactorsXray);
            flowLayoutPanel8.Controls.Add(miniTableScatteringFactorsElectron);
            flowLayoutPanel8.Controls.Add(miniTableScatteringFactorsNeutron);
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            toolTip.SetToolTip(flowLayoutPanel8, resources.GetString("flowLayoutPanel8.ToolTip"));
            // 
            // miniTableScatteringFactorsXray
            // 
            resources.ApplyResources(miniTableScatteringFactorsXray, "miniTableScatteringFactorsXray");
            miniTableScatteringFactorsXray.AutoFitHeight = true;
            miniTableScatteringFactorsXray.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            miniTableScatteringFactorsXray.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colSfxElem, colSfxZ, colSfxFs, colSfxFp, colSfxFpp });
            miniTableScatteringFactorsXray.Name = "miniTableScatteringFactorsXray";
            miniTableScatteringFactorsXray.TabStop = false;
            toolTip.SetToolTip(miniTableScatteringFactorsXray, resources.GetString("miniTableScatteringFactorsXray.ToolTip"));
            // 
            // colSfxElem
            // 
            colSfxElem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfxElem, "colSfxElem");
            colSfxElem.Name = "colSfxElem";
            colSfxElem.ReadOnly = true;
            // 
            // colSfxZ
            // 
            colSfxZ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfxZ, "colSfxZ");
            colSfxZ.Name = "colSfxZ";
            colSfxZ.ReadOnly = true;
            // 
            // colSfxFs
            // 
            colSfxFs.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfxFs, "colSfxFs");
            colSfxFs.Name = "colSfxFs";
            colSfxFs.ReadOnly = true;
            // 
            // colSfxFp
            // 
            colSfxFp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfxFp, "colSfxFp");
            colSfxFp.Name = "colSfxFp";
            colSfxFp.ReadOnly = true;
            // 
            // colSfxFpp
            // 
            colSfxFpp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfxFpp, "colSfxFpp");
            colSfxFpp.Name = "colSfxFpp";
            colSfxFpp.ReadOnly = true;
            // 
            // miniTableScatteringFactorsElectron
            // 
            resources.ApplyResources(miniTableScatteringFactorsElectron, "miniTableScatteringFactorsElectron");
            miniTableScatteringFactorsElectron.AutoFitHeight = true;
            miniTableScatteringFactorsElectron.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            miniTableScatteringFactorsElectron.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colSfeElem, colSfeZ, colSfeFe, colSfeModel });
            miniTableScatteringFactorsElectron.Name = "miniTableScatteringFactorsElectron";
            miniTableScatteringFactorsElectron.TabStop = false;
            toolTip.SetToolTip(miniTableScatteringFactorsElectron, resources.GetString("miniTableScatteringFactorsElectron.ToolTip"));
            // 
            // colSfeElem
            // 
            colSfeElem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfeElem, "colSfeElem");
            colSfeElem.Name = "colSfeElem";
            colSfeElem.ReadOnly = true;
            // 
            // colSfeZ
            // 
            colSfeZ.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfeZ, "colSfeZ");
            colSfeZ.Name = "colSfeZ";
            colSfeZ.ReadOnly = true;
            // 
            // colSfeFe
            // 
            colSfeFe.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfeFe, "colSfeFe");
            colSfeFe.Name = "colSfeFe";
            colSfeFe.ReadOnly = true;
            // 
            // colSfeModel
            // 
            colSfeModel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfeModel, "colSfeModel");
            colSfeModel.Name = "colSfeModel";
            colSfeModel.ReadOnly = true;
            // 
            // miniTableScatteringFactorsNeutron
            // 
            resources.ApplyResources(miniTableScatteringFactorsNeutron, "miniTableScatteringFactorsNeutron");
            miniTableScatteringFactorsNeutron.AutoFitHeight = true;
            miniTableScatteringFactorsNeutron.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            miniTableScatteringFactorsNeutron.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colSfnElem, colSfnBcoh, colSfnScoh, colSfnSinc });
            miniTableScatteringFactorsNeutron.Name = "miniTableScatteringFactorsNeutron";
            miniTableScatteringFactorsNeutron.TabStop = false;
            toolTip.SetToolTip(miniTableScatteringFactorsNeutron, resources.GetString("miniTableScatteringFactorsNeutron.ToolTip"));
            // 
            // colSfnElem
            // 
            colSfnElem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfnElem, "colSfnElem");
            colSfnElem.Name = "colSfnElem";
            colSfnElem.ReadOnly = true;
            // 
            // colSfnBcoh
            // 
            colSfnBcoh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfnBcoh, "colSfnBcoh");
            colSfnBcoh.Name = "colSfnBcoh";
            colSfnBcoh.ReadOnly = true;
            // 
            // colSfnScoh
            // 
            colSfnScoh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfnScoh, "colSfnScoh");
            colSfnScoh.Name = "colSfnScoh";
            colSfnScoh.ReadOnly = true;
            // 
            // colSfnSinc
            // 
            colSfnSinc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colSfnSinc, "colSfnSinc");
            colSfnSinc.Name = "colSfnSinc";
            colSfnSinc.ReadOnly = true;
            // 
            // flowLayoutPanelScatteringFactorModel
            // 
            resources.ApplyResources(flowLayoutPanelScatteringFactorModel, "flowLayoutPanelScatteringFactorModel");
            flowLayoutPanelScatteringFactorModel.Controls.Add(labelModel);
            flowLayoutPanelScatteringFactorModel.Controls.Add(flowLayoutPanelModel_Xray);
            flowLayoutPanelScatteringFactorModel.Controls.Add(flowLayoutPanelModel_Electron);
            flowLayoutPanelScatteringFactorModel.Controls.Add(checkBoxDebyeWaller);
            flowLayoutPanelScatteringFactorModel.Name = "flowLayoutPanelScatteringFactorModel";
            toolTip.SetToolTip(flowLayoutPanelScatteringFactorModel, resources.GetString("flowLayoutPanelScatteringFactorModel.ToolTip"));
            // 
            // flowLayoutPanelModel_Xray
            // 
            resources.ApplyResources(flowLayoutPanelModel_Xray, "flowLayoutPanelModel_Xray");
            flowLayoutPanelModel_Xray.Controls.Add(radioButtonXrayFs);
            flowLayoutPanelModel_Xray.Controls.Add(radioButtonXrayFqSq);
            flowLayoutPanelModel_Xray.Name = "flowLayoutPanelModel_Xray";
            toolTip.SetToolTip(flowLayoutPanelModel_Xray, resources.GetString("flowLayoutPanelModel_Xray.ToolTip"));
            // 
            // flowLayoutPanelModel_Electron
            // 
            resources.ApplyResources(flowLayoutPanelModel_Electron, "flowLayoutPanelModel_Electron");
            flowLayoutPanelModel_Electron.Controls.Add(radioButtonElectronPeng);
            flowLayoutPanelModel_Electron.Controls.Add(radioButtonElectronEightGaussian);
            flowLayoutPanelModel_Electron.Controls.Add(radioButtonElectronKirkland);
            flowLayoutPanelModel_Electron.Name = "flowLayoutPanelModel_Electron";
            toolTip.SetToolTip(flowLayoutPanelModel_Electron, resources.GetString("flowLayoutPanelModel_Electron.ToolTip"));
            // 
            // tabPageFluorescence
            // 
            resources.ApplyResources(tabPageFluorescence, "tabPageFluorescence");
            tabPageFluorescence.Controls.Add(panel4);
            tabPageFluorescence.Controls.Add(flowLayoutPanel7);
            tabPageFluorescence.Name = "tabPageFluorescence";
            toolTip.SetToolTip(tabPageFluorescence, resources.GetString("tabPageFluorescence.ToolTip"));
            tabPageFluorescence.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            resources.ApplyResources(panel4, "panel4");
            panel4.Controls.Add(graphControlFluor);
            panel4.Name = "panel4";
            toolTip.SetToolTip(panel4, resources.GetString("panel4.ToolTip"));
            // 
            // graphControlFluor
            // 
            resources.ApplyResources(graphControlFluor, "graphControlFluor");
            graphControlFluor.CopyVisible = true;
            graphControlFluor.FixLowerXToZero = true;
            graphControlFluor.Name = "graphControlFluor";
            graphControlFluor.RangePanelVisible = true;
            toolTip.SetToolTip(graphControlFluor, resources.GetString("graphControlFluor.ToolTip"));
            graphControlFluor.VerticalLineMarkerVisible = true;
            // 
            // flowLayoutPanel7
            // 
            resources.ApplyResources(flowLayoutPanel7, "flowLayoutPanel7");
            flowLayoutPanel7.Controls.Add(miniTableFluorScalars);
            flowLayoutPanel7.Controls.Add(miniTableFluorLines);
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            toolTip.SetToolTip(flowLayoutPanel7, resources.GetString("flowLayoutPanel7.ToolTip"));
            // 
            // miniTableFluorScalars
            // 
            resources.ApplyResources(miniTableFluorScalars, "miniTableFluorScalars");
            miniTableFluorScalars.AutoFitHeight = true;
            miniTableFluorScalars.Name = "miniTableFluorScalars";
            miniTableFluorScalars.TabStop = false;
            toolTip.SetToolTip(miniTableFluorScalars, resources.GetString("miniTableFluorScalars.ToolTip"));
            // 
            // miniTableFluorLines
            // 
            resources.ApplyResources(miniTableFluorLines, "miniTableFluorLines");
            miniTableFluorLines.AllowVerticalScroll = true;
            miniTableFluorLines.AutoFitHeight = true;
            miniTableFluorLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colFlElem, colFlLine, colFlE, colFlRelI, colFlOmega });
            miniTableFluorLines.Name = "miniTableFluorLines";
            miniTableFluorLines.TabStop = false;
            toolTip.SetToolTip(miniTableFluorLines, resources.GetString("miniTableFluorLines.ToolTip"));
            // 
            // colFlElem
            // 
            colFlElem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colFlElem, "colFlElem");
            colFlElem.Name = "colFlElem";
            colFlElem.ReadOnly = true;
            // 
            // colFlLine
            // 
            colFlLine.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colFlLine, "colFlLine");
            colFlLine.Name = "colFlLine";
            colFlLine.ReadOnly = true;
            // 
            // colFlE
            // 
            colFlE.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colFlE, "colFlE");
            colFlE.Name = "colFlE";
            colFlE.ReadOnly = true;
            // 
            // colFlRelI
            // 
            colFlRelI.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colFlRelI, "colFlRelI");
            colFlRelI.Name = "colFlRelI";
            colFlRelI.ReadOnly = true;
            // 
            // colFlOmega
            // 
            colFlOmega.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            resources.ApplyResources(colFlOmega, "colFlOmega");
            colFlOmega.Name = "colFlOmega";
            colFlOmega.ReadOnly = true;
            // 
            // FormBeamInteraction
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(tabControl);
            Controls.Add(waveLengthControl);
            Name = "FormBeamInteraction";
            ShowIcon = false;
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            FormClosing += FormBeamInteraction_FormClosing;
            Load += FormBeamInteraction_Load;
            VisibleChanged += FormBeamInteraction_VisibleChanged;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSourceScatteringFactor).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabControl.ResumeLayout(false);
            tabPageReflections.ResumeLayout(false);
            tabPageReflections.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tabPageAttenuations.ResumeLayout(false);
            tabPageAttenuations.PerformLayout();
            flowLayoutPanel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)miniTableAttenScalars).EndInit();
            ((System.ComponentModel.ISupportInitialize)miniTableAttenEdges).EndInit();
            ((System.ComponentModel.ISupportInitialize)miniTableAttenElectron).EndInit();
            ((System.ComponentModel.ISupportInitialize)miniTableAttenNeutron).EndInit();
            flowLayoutPanelAttenuationModel.ResumeLayout(false);
            flowLayoutPanelAttenuationModel.PerformLayout();
            flowLayoutPanelAttenCoeff.ResumeLayout(false);
            flowLayoutPanelAttenCoeff.PerformLayout();
            flowLayoutPanelElecQuantity.ResumeLayout(false);
            flowLayoutPanelElecQuantity.PerformLayout();
            tabPageScatteringFactors.ResumeLayout(false);
            tabPageScatteringFactors.PerformLayout();
            flowLayoutPanel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)miniTableScatteringFactorsXray).EndInit();
            ((System.ComponentModel.ISupportInitialize)miniTableScatteringFactorsElectron).EndInit();
            ((System.ComponentModel.ISupportInitialize)miniTableScatteringFactorsNeutron).EndInit();
            flowLayoutPanelScatteringFactorModel.ResumeLayout(false);
            flowLayoutPanelScatteringFactorModel.PerformLayout();
            flowLayoutPanelModel_Xray.ResumeLayout(false);
            flowLayoutPanelModel_Xray.PerformLayout();
            flowLayoutPanelModel_Electron.ResumeLayout(false);
            flowLayoutPanelModel_Electron.PerformLayout();
            tabPageFluorescence.ResumeLayout(false);
            panel4.ResumeLayout(false);
            flowLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)miniTableFluorScalars).EndInit();
            ((System.ComponentModel.ISupportInitialize)miniTableFluorLines).EndInit();
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
        private WaveLengthControl waveLengthControl;
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
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageReflections;
        private System.Windows.Forms.TabPage tabPageScatteringFactors;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private GraphControl graphControlScatteringFactor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private GraphControl graphControlReflections;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.TabPage tabPageAttenuations;
        private System.Windows.Forms.TabPage tabPageFluorescence;
        private GraphControl graphControlAtten;                            // 260606Cl
        private NumericBox numericBoxAttenThickness;                       // 260606Cl
        private MiniTable miniTableAttenScalars;                          // 260606Cl
        private MiniTable miniTableAttenEdges;                            // 260606Cl
        private MiniTable miniTableAttenElectron;                         // 260606Cl
        private MiniTable miniTableAttenNeutron;                          // 260606Cl
        private GraphControl graphControlFluor;                            // 260606Cl
        private MiniTable miniTableFluorScalars;                          // 260606Cl
        private MiniTable miniTableFluorLines;                            // 260606Cl
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelScatteringFactorModel;
        private System.Windows.Forms.CheckBox checkBoxDebyeWaller;
        private MiniTable miniTableScatteringFactorsXray;
        private MiniTable miniTableScatteringFactorsElectron;
        private MiniTable miniTableScatteringFactorsNeutron;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        // 260607Cl 追加コントロール (Reflections軸/対数トグル, Attenuation 係数モード/電子量セレクタ)。レイアウトはコード既定値で配置し VS デザイナで微調整する。
        private System.Windows.Forms.ComboBox comboBoxReflXAxis;
        private System.Windows.Forms.CheckBox checkBoxReflLog;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAttenCoeff;
        private System.Windows.Forms.RadioButton radioButtonAttenMassMu;
        private System.Windows.Forms.RadioButton radioButtonAttenLinMu;
        private System.Windows.Forms.RadioButton radioButtonAttenTrans;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelElecQuantity;
        private System.Windows.Forms.RadioButton radioButtonElecAll;
        private System.Windows.Forms.RadioButton radioButtonElecSigma;
        private System.Windows.Forms.RadioButton radioButtonElecEMFP;
        private System.Windows.Forms.RadioButton radioButtonElecDeds;
        private System.Windows.Forms.RadioButton radioButtonElecIMFP;
        private System.Windows.Forms.RadioButton radioButtonElecRange;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAttenuationModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfxElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfxZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfxFs;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfxFp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfxFpp;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfnElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfnBcoh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfnScoh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfnSinc;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfeElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfeZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfeFe;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSfeModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNeutElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNeutBcoh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNeutScoh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNeutAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colElecElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colElecZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colElecAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colElecA;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdgeElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdgeZ;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdgeEdge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdgeKeV;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEdgeJump;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlElem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlLine;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlE;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlRelI;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFlOmega;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnH;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnK;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumnI;
    }
}
