namespace Crystallography.Controls
{
    partial class HorizontalAxisUserControl
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

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        // (260323Ch) renamed numeric container controls:
        // groupBox1 -> groupBoxWaveProperty
        // groupBox2 -> groupBoxHorizontalAxis
        // groupBox3 -> groupBoxColor
        // groupBox4 -> groupBoxSource
        // flowLayoutPanel7 -> flowLayoutPanelWaveProperty
        // flowLayoutPanel5 -> flowLayoutPanelSourceColor
        // flowLayoutPanel4 -> flowLayoutPanelSourceType
        // flowLayoutPanel2 -> flowLayoutPanelWaveMode
        // flowLayoutPanel3 -> flowLayoutPanelAxisSelection
        // flowLayoutPanel15 -> flowLayoutPanelAxisType
        // flowLayoutPanel12 -> flowLayoutPanelAxisValues
        // flowLayoutPanel6 -> flowLayoutPanelMain
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HorizontalAxisUserControl));
            groupBoxWaveProperty = new System.Windows.Forms.GroupBox();
            flowLayoutPanelWaveProperty = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelSourceColor = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxSource = new System.Windows.Forms.GroupBox();
            flowLayoutPanelSourceType = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonXray = new System.Windows.Forms.RadioButton();
            radioButtonElectron = new System.Windows.Forms.RadioButton();
            radioButtonNeutron = new System.Windows.Forms.RadioButton();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxColor = new System.Windows.Forms.GroupBox();
            flowLayoutPanelWaveMode = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonMonochro = new System.Windows.Forms.RadioButton();
            radioButtonFlatWhite = new System.Windows.Forms.RadioButton();
            radioButtonCustomWhite = new System.Windows.Forms.RadioButton();
            groupBoxTwoTheta = new System.Windows.Forms.GroupBox();
            waveLengthControl = new WaveLengthControl();
            groupBoxHorizontalAxis = new System.Windows.Forms.GroupBox();
            flowLayoutPanelAxisSelection = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelAxisType = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonTwoTheta = new System.Windows.Forms.RadioButton();
            radioButtonDspacing = new System.Windows.Forms.RadioButton();
            radioButtonWavenumber = new System.Windows.Forms.RadioButton();
            radioButtonEnergy = new System.Windows.Forms.RadioButton();
            radioButtonTOF = new System.Windows.Forms.RadioButton();
            groupBoxOption = new System.Windows.Forms.GroupBox();
            flowLayoutPanelAxisValues = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelTwoTheta = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonAngleUnitDegree = new System.Windows.Forms.RadioButton();
            radioButtonAngleUnitCentiDegree = new System.Windows.Forms.RadioButton();
            radioButtonAngleUnitRadian = new System.Windows.Forms.RadioButton();
            radioButtonAngleUnitMilliRadian = new System.Windows.Forms.RadioButton();
            flowLayoutPanelDspacing = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonDspacingUnitAng = new System.Windows.Forms.RadioButton();
            radioButtonDspacingUnitNm = new System.Windows.Forms.RadioButton();
            flowLayoutPanelWavenumber = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonWavenumberAngInv = new System.Windows.Forms.RadioButton();
            radioButtonWavenumberUnitNmInv = new System.Windows.Forms.RadioButton();
            flowLayoutPanelEnergy = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonEnergyUnitEv = new System.Windows.Forms.RadioButton();
            radioButtonEnergyUnitKev = new System.Windows.Forms.RadioButton();
            radioButtonEnergyUnitMev = new System.Windows.Forms.RadioButton();
            numericBoxTwoTheta = new NumericBox();
            flowLayoutPanelNeutronTOF = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonTofUnitMicroSec = new System.Windows.Forms.RadioButton();
            radioButtonTofUnitNanoSec = new System.Windows.Forms.RadioButton();
            numericBoxTofAngle = new NumericBox();
            numericBoxTofLength = new NumericBox();
            toolTip = new System.Windows.Forms.ToolTip(components);
            flowLayoutPanelMain = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxWaveProperty.SuspendLayout();
            flowLayoutPanelWaveProperty.SuspendLayout();
            flowLayoutPanelSourceColor.SuspendLayout();
            groupBoxSource.SuspendLayout();
            flowLayoutPanelSourceType.SuspendLayout();
            groupBoxColor.SuspendLayout();
            flowLayoutPanelWaveMode.SuspendLayout();
            groupBoxTwoTheta.SuspendLayout();
            groupBoxHorizontalAxis.SuspendLayout();
            flowLayoutPanelAxisSelection.SuspendLayout();
            flowLayoutPanelAxisType.SuspendLayout();
            groupBoxOption.SuspendLayout();
            flowLayoutPanelAxisValues.SuspendLayout();
            flowLayoutPanelTwoTheta.SuspendLayout();
            flowLayoutPanelDspacing.SuspendLayout();
            flowLayoutPanelWavenumber.SuspendLayout();
            flowLayoutPanelEnergy.SuspendLayout();
            flowLayoutPanelNeutronTOF.SuspendLayout();
            flowLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxWaveProperty
            // 
            resources.ApplyResources(groupBoxWaveProperty, "groupBoxWaveProperty");
            groupBoxWaveProperty.Controls.Add(flowLayoutPanelWaveProperty);
            groupBoxWaveProperty.Name = "groupBoxWaveProperty";
            groupBoxWaveProperty.TabStop = false;
            toolTip.SetToolTip(groupBoxWaveProperty, resources.GetString("groupBoxWaveProperty.ToolTip"));
            // 
            // flowLayoutPanelWaveProperty
            // 
            resources.ApplyResources(flowLayoutPanelWaveProperty, "flowLayoutPanelWaveProperty");
            flowLayoutPanelWaveProperty.Controls.Add(flowLayoutPanelSourceColor);
            flowLayoutPanelWaveProperty.Controls.Add(groupBoxTwoTheta);
            flowLayoutPanelWaveProperty.Name = "flowLayoutPanelWaveProperty";
            toolTip.SetToolTip(flowLayoutPanelWaveProperty, resources.GetString("flowLayoutPanelWaveProperty.ToolTip"));
            // 
            // flowLayoutPanelSourceColor
            // 
            resources.ApplyResources(flowLayoutPanelSourceColor, "flowLayoutPanelSourceColor");
            flowLayoutPanelSourceColor.Controls.Add(groupBoxSource);
            flowLayoutPanelSourceColor.Controls.Add(groupBoxColor);
            flowLayoutPanelSourceColor.Name = "flowLayoutPanelSourceColor";
            toolTip.SetToolTip(flowLayoutPanelSourceColor, resources.GetString("flowLayoutPanelSourceColor.ToolTip"));
            // 
            // groupBoxSource
            // 
            resources.ApplyResources(groupBoxSource, "groupBoxSource");
            groupBoxSource.Controls.Add(flowLayoutPanelSourceType);
            groupBoxSource.Controls.Add(flowLayoutPanel1);
            groupBoxSource.Name = "groupBoxSource";
            groupBoxSource.TabStop = false;
            toolTip.SetToolTip(groupBoxSource, resources.GetString("groupBoxSource.ToolTip"));
            // 
            // flowLayoutPanelSourceType
            // 
            resources.ApplyResources(flowLayoutPanelSourceType, "flowLayoutPanelSourceType");
            flowLayoutPanelSourceType.Controls.Add(radioButtonXray);
            flowLayoutPanelSourceType.Controls.Add(radioButtonElectron);
            flowLayoutPanelSourceType.Controls.Add(radioButtonNeutron);
            flowLayoutPanelSourceType.Name = "flowLayoutPanelSourceType";
            toolTip.SetToolTip(flowLayoutPanelSourceType, resources.GetString("flowLayoutPanelSourceType.ToolTip"));
            // 
            // radioButtonXray
            // 
            resources.ApplyResources(radioButtonXray, "radioButtonXray");
            radioButtonXray.Checked = true;
            radioButtonXray.Name = "radioButtonXray";
            radioButtonXray.TabStop = true;
            toolTip.SetToolTip(radioButtonXray, resources.GetString("radioButtonXray.ToolTip"));
            radioButtonXray.UseVisualStyleBackColor = true;
            radioButtonXray.CheckedChanged += radioButtonWaveSource_CheckedChanged;
            // 
            // radioButtonElectron
            // 
            resources.ApplyResources(radioButtonElectron, "radioButtonElectron");
            radioButtonElectron.Name = "radioButtonElectron";
            toolTip.SetToolTip(radioButtonElectron, resources.GetString("radioButtonElectron.ToolTip"));
            radioButtonElectron.UseVisualStyleBackColor = true;
            radioButtonElectron.CheckedChanged += radioButtonWaveSource_CheckedChanged;
            // 
            // radioButtonNeutron
            // 
            resources.ApplyResources(radioButtonNeutron, "radioButtonNeutron");
            radioButtonNeutron.Name = "radioButtonNeutron";
            toolTip.SetToolTip(radioButtonNeutron, resources.GetString("radioButtonNeutron.ToolTip"));
            radioButtonNeutron.UseVisualStyleBackColor = true;
            radioButtonNeutron.CheckedChanged += radioButtonWaveSource_CheckedChanged;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            toolTip.SetToolTip(flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
            // 
            // groupBoxColor
            // 
            resources.ApplyResources(groupBoxColor, "groupBoxColor");
            groupBoxColor.Controls.Add(flowLayoutPanelWaveMode);
            groupBoxColor.Name = "groupBoxColor";
            groupBoxColor.TabStop = false;
            toolTip.SetToolTip(groupBoxColor, resources.GetString("groupBoxColor.ToolTip"));
            // 
            // flowLayoutPanelWaveMode
            // 
            resources.ApplyResources(flowLayoutPanelWaveMode, "flowLayoutPanelWaveMode");
            flowLayoutPanelWaveMode.Controls.Add(radioButtonMonochro);
            flowLayoutPanelWaveMode.Controls.Add(radioButtonFlatWhite);
            flowLayoutPanelWaveMode.Controls.Add(radioButtonCustomWhite);
            flowLayoutPanelWaveMode.Name = "flowLayoutPanelWaveMode";
            toolTip.SetToolTip(flowLayoutPanelWaveMode, resources.GetString("flowLayoutPanelWaveMode.ToolTip"));
            // 
            // radioButtonMonochro
            // 
            resources.ApplyResources(radioButtonMonochro, "radioButtonMonochro");
            radioButtonMonochro.Checked = true;
            radioButtonMonochro.Name = "radioButtonMonochro";
            radioButtonMonochro.TabStop = true;
            toolTip.SetToolTip(radioButtonMonochro, resources.GetString("radioButtonMonochro.ToolTip"));
            radioButtonMonochro.UseVisualStyleBackColor = true;
            radioButtonMonochro.CheckedChanged += radioButtonMonochro_CheckedChanged;
            // 
            // radioButtonFlatWhite
            // 
            resources.ApplyResources(radioButtonFlatWhite, "radioButtonFlatWhite");
            radioButtonFlatWhite.Name = "radioButtonFlatWhite";
            toolTip.SetToolTip(radioButtonFlatWhite, resources.GetString("radioButtonFlatWhite.ToolTip"));
            radioButtonFlatWhite.UseVisualStyleBackColor = true;
            radioButtonFlatWhite.CheckedChanged += radioButtonMonochro_CheckedChanged;
            // 
            // radioButtonCustomWhite
            // 
            resources.ApplyResources(radioButtonCustomWhite, "radioButtonCustomWhite");
            radioButtonCustomWhite.Name = "radioButtonCustomWhite";
            toolTip.SetToolTip(radioButtonCustomWhite, resources.GetString("radioButtonCustomWhite.ToolTip"));
            radioButtonCustomWhite.UseVisualStyleBackColor = true;
            radioButtonCustomWhite.CheckedChanged += radioButtonMonochro_CheckedChanged;
            // 
            // groupBoxTwoTheta
            // 
            resources.ApplyResources(groupBoxTwoTheta, "groupBoxTwoTheta");
            groupBoxTwoTheta.Controls.Add(waveLengthControl);
            groupBoxTwoTheta.Name = "groupBoxTwoTheta";
            groupBoxTwoTheta.TabStop = false;
            toolTip.SetToolTip(groupBoxTwoTheta, resources.GetString("groupBoxTwoTheta.ToolTip"));
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            waveLengthControl.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl.Energy = 8.04114721D;
            waveLengthControl.Monochrome = true;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = false;
            toolTip.SetToolTip(waveLengthControl, resources.GetString("waveLengthControl.ToolTip"));
            waveLengthControl.WaveLength = 0.1541871066667D;
            waveLengthControl.WaveSource = WaveSource.Xray;
            waveLengthControl.XrayWaveSourceElementNumber = 29;
            waveLengthControl.XrayWaveSourceLine = XrayLine.Ka;
            waveLengthControl.WavelengthChanged += waveLengthControl_WavelengthChanged;
            // 
            // groupBoxHorizontalAxis
            // 
            resources.ApplyResources(groupBoxHorizontalAxis, "groupBoxHorizontalAxis");
            groupBoxHorizontalAxis.Controls.Add(flowLayoutPanelAxisSelection);
            groupBoxHorizontalAxis.Name = "groupBoxHorizontalAxis";
            groupBoxHorizontalAxis.TabStop = false;
            toolTip.SetToolTip(groupBoxHorizontalAxis, resources.GetString("groupBoxHorizontalAxis.ToolTip"));
            // 
            // flowLayoutPanelAxisSelection
            // 
            resources.ApplyResources(flowLayoutPanelAxisSelection, "flowLayoutPanelAxisSelection");
            flowLayoutPanelAxisSelection.Controls.Add(flowLayoutPanelAxisType);
            flowLayoutPanelAxisSelection.Controls.Add(groupBoxOption);
            flowLayoutPanelAxisSelection.Name = "flowLayoutPanelAxisSelection";
            toolTip.SetToolTip(flowLayoutPanelAxisSelection, resources.GetString("flowLayoutPanelAxisSelection.ToolTip"));
            // 
            // flowLayoutPanelAxisType
            // 
            resources.ApplyResources(flowLayoutPanelAxisType, "flowLayoutPanelAxisType");
            flowLayoutPanelAxisType.Controls.Add(radioButtonTwoTheta);
            flowLayoutPanelAxisType.Controls.Add(radioButtonDspacing);
            flowLayoutPanelAxisType.Controls.Add(radioButtonWavenumber);
            flowLayoutPanelAxisType.Controls.Add(radioButtonEnergy);
            flowLayoutPanelAxisType.Controls.Add(radioButtonTOF);
            flowLayoutPanelAxisType.Name = "flowLayoutPanelAxisType";
            toolTip.SetToolTip(flowLayoutPanelAxisType, resources.GetString("flowLayoutPanelAxisType.ToolTip"));
            // 
            // radioButtonTwoTheta
            // 
            resources.ApplyResources(radioButtonTwoTheta, "radioButtonTwoTheta");
            radioButtonTwoTheta.Checked = true;
            radioButtonTwoTheta.Name = "radioButtonTwoTheta";
            radioButtonTwoTheta.TabStop = true;
            toolTip.SetToolTip(radioButtonTwoTheta, resources.GetString("radioButtonTwoTheta.ToolTip"));
            radioButtonTwoTheta.UseVisualStyleBackColor = true;
            radioButtonTwoTheta.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonDspacing
            // 
            resources.ApplyResources(radioButtonDspacing, "radioButtonDspacing");
            radioButtonDspacing.Name = "radioButtonDspacing";
            toolTip.SetToolTip(radioButtonDspacing, resources.GetString("radioButtonDspacing.ToolTip"));
            radioButtonDspacing.UseVisualStyleBackColor = true;
            radioButtonDspacing.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonWavenumber
            // 
            resources.ApplyResources(radioButtonWavenumber, "radioButtonWavenumber");
            radioButtonWavenumber.Name = "radioButtonWavenumber";
            toolTip.SetToolTip(radioButtonWavenumber, resources.GetString("radioButtonWavenumber.ToolTip"));
            radioButtonWavenumber.UseVisualStyleBackColor = true;
            radioButtonWavenumber.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonEnergy
            // 
            resources.ApplyResources(radioButtonEnergy, "radioButtonEnergy");
            radioButtonEnergy.Name = "radioButtonEnergy";
            toolTip.SetToolTip(radioButtonEnergy, resources.GetString("radioButtonEnergy.ToolTip"));
            radioButtonEnergy.UseVisualStyleBackColor = true;
            radioButtonEnergy.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonTOF
            // 
            resources.ApplyResources(radioButtonTOF, "radioButtonTOF");
            radioButtonTOF.Name = "radioButtonTOF";
            toolTip.SetToolTip(radioButtonTOF, resources.GetString("radioButtonTOF.ToolTip"));
            radioButtonTOF.UseVisualStyleBackColor = true;
            radioButtonTOF.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // groupBoxOption
            // 
            resources.ApplyResources(groupBoxOption, "groupBoxOption");
            groupBoxOption.Controls.Add(flowLayoutPanelAxisValues);
            groupBoxOption.Name = "groupBoxOption";
            groupBoxOption.TabStop = false;
            toolTip.SetToolTip(groupBoxOption, resources.GetString("groupBoxOption.ToolTip"));
            // 
            // flowLayoutPanelAxisValues
            // 
            resources.ApplyResources(flowLayoutPanelAxisValues, "flowLayoutPanelAxisValues");
            flowLayoutPanelAxisValues.Controls.Add(flowLayoutPanelTwoTheta);
            flowLayoutPanelAxisValues.Controls.Add(flowLayoutPanelDspacing);
            flowLayoutPanelAxisValues.Controls.Add(flowLayoutPanelWavenumber);
            flowLayoutPanelAxisValues.Controls.Add(flowLayoutPanelEnergy);
            flowLayoutPanelAxisValues.Controls.Add(flowLayoutPanelNeutronTOF);
            flowLayoutPanelAxisValues.Name = "flowLayoutPanelAxisValues";
            toolTip.SetToolTip(flowLayoutPanelAxisValues, resources.GetString("flowLayoutPanelAxisValues.ToolTip"));
            // 
            // flowLayoutPanelTwoTheta
            // 
            resources.ApplyResources(flowLayoutPanelTwoTheta, "flowLayoutPanelTwoTheta");
            flowLayoutPanelTwoTheta.Controls.Add(radioButtonAngleUnitDegree);
            flowLayoutPanelTwoTheta.Controls.Add(radioButtonAngleUnitCentiDegree);
            flowLayoutPanelTwoTheta.Controls.Add(radioButtonAngleUnitRadian);
            flowLayoutPanelTwoTheta.Controls.Add(radioButtonAngleUnitMilliRadian);
            flowLayoutPanelTwoTheta.Name = "flowLayoutPanelTwoTheta";
            toolTip.SetToolTip(flowLayoutPanelTwoTheta, resources.GetString("flowLayoutPanelTwoTheta.ToolTip"));
            // 
            // radioButtonAngleUnitDegree
            // 
            resources.ApplyResources(radioButtonAngleUnitDegree, "radioButtonAngleUnitDegree");
            radioButtonAngleUnitDegree.Checked = true;
            radioButtonAngleUnitDegree.Name = "radioButtonAngleUnitDegree";
            radioButtonAngleUnitDegree.TabStop = true;
            toolTip.SetToolTip(radioButtonAngleUnitDegree, resources.GetString("radioButtonAngleUnitDegree.ToolTip"));
            radioButtonAngleUnitDegree.UseVisualStyleBackColor = true;
            radioButtonAngleUnitDegree.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonAngleUnitCentiDegree
            // 
            resources.ApplyResources(radioButtonAngleUnitCentiDegree, "radioButtonAngleUnitCentiDegree");
            radioButtonAngleUnitCentiDegree.Name = "radioButtonAngleUnitCentiDegree";
            toolTip.SetToolTip(radioButtonAngleUnitCentiDegree, resources.GetString("radioButtonAngleUnitCentiDegree.ToolTip"));
            radioButtonAngleUnitCentiDegree.UseVisualStyleBackColor = true;
            radioButtonAngleUnitCentiDegree.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonAngleUnitRadian
            // 
            resources.ApplyResources(radioButtonAngleUnitRadian, "radioButtonAngleUnitRadian");
            radioButtonAngleUnitRadian.Name = "radioButtonAngleUnitRadian";
            toolTip.SetToolTip(radioButtonAngleUnitRadian, resources.GetString("radioButtonAngleUnitRadian.ToolTip"));
            radioButtonAngleUnitRadian.UseVisualStyleBackColor = true;
            radioButtonAngleUnitRadian.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonAngleUnitMilliRadian
            // 
            resources.ApplyResources(radioButtonAngleUnitMilliRadian, "radioButtonAngleUnitMilliRadian");
            radioButtonAngleUnitMilliRadian.Name = "radioButtonAngleUnitMilliRadian";
            toolTip.SetToolTip(radioButtonAngleUnitMilliRadian, resources.GetString("radioButtonAngleUnitMilliRadian.ToolTip"));
            radioButtonAngleUnitMilliRadian.UseVisualStyleBackColor = true;
            radioButtonAngleUnitMilliRadian.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // flowLayoutPanelDspacing
            // 
            resources.ApplyResources(flowLayoutPanelDspacing, "flowLayoutPanelDspacing");
            flowLayoutPanelDspacing.Controls.Add(radioButtonDspacingUnitAng);
            flowLayoutPanelDspacing.Controls.Add(radioButtonDspacingUnitNm);
            flowLayoutPanelDspacing.Name = "flowLayoutPanelDspacing";
            toolTip.SetToolTip(flowLayoutPanelDspacing, resources.GetString("flowLayoutPanelDspacing.ToolTip"));
            // 
            // radioButtonDspacingUnitAng
            // 
            resources.ApplyResources(radioButtonDspacingUnitAng, "radioButtonDspacingUnitAng");
            radioButtonDspacingUnitAng.Checked = true;
            radioButtonDspacingUnitAng.Name = "radioButtonDspacingUnitAng";
            radioButtonDspacingUnitAng.TabStop = true;
            toolTip.SetToolTip(radioButtonDspacingUnitAng, resources.GetString("radioButtonDspacingUnitAng.ToolTip"));
            radioButtonDspacingUnitAng.UseVisualStyleBackColor = true;
            radioButtonDspacingUnitAng.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonDspacingUnitNm
            // 
            resources.ApplyResources(radioButtonDspacingUnitNm, "radioButtonDspacingUnitNm");
            radioButtonDspacingUnitNm.Name = "radioButtonDspacingUnitNm";
            toolTip.SetToolTip(radioButtonDspacingUnitNm, resources.GetString("radioButtonDspacingUnitNm.ToolTip"));
            radioButtonDspacingUnitNm.UseVisualStyleBackColor = true;
            radioButtonDspacingUnitNm.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // flowLayoutPanelWavenumber
            // 
            resources.ApplyResources(flowLayoutPanelWavenumber, "flowLayoutPanelWavenumber");
            flowLayoutPanelWavenumber.Controls.Add(radioButtonWavenumberAngInv);
            flowLayoutPanelWavenumber.Controls.Add(radioButtonWavenumberUnitNmInv);
            flowLayoutPanelWavenumber.Name = "flowLayoutPanelWavenumber";
            toolTip.SetToolTip(flowLayoutPanelWavenumber, resources.GetString("flowLayoutPanelWavenumber.ToolTip"));
            // 
            // radioButtonWavenumberAngInv
            // 
            resources.ApplyResources(radioButtonWavenumberAngInv, "radioButtonWavenumberAngInv");
            radioButtonWavenumberAngInv.Checked = true;
            radioButtonWavenumberAngInv.Name = "radioButtonWavenumberAngInv";
            radioButtonWavenumberAngInv.TabStop = true;
            toolTip.SetToolTip(radioButtonWavenumberAngInv, resources.GetString("radioButtonWavenumberAngInv.ToolTip"));
            radioButtonWavenumberAngInv.UseVisualStyleBackColor = true;
            radioButtonWavenumberAngInv.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonWavenumberUnitNmInv
            // 
            resources.ApplyResources(radioButtonWavenumberUnitNmInv, "radioButtonWavenumberUnitNmInv");
            radioButtonWavenumberUnitNmInv.Name = "radioButtonWavenumberUnitNmInv";
            toolTip.SetToolTip(radioButtonWavenumberUnitNmInv, resources.GetString("radioButtonWavenumberUnitNmInv.ToolTip"));
            radioButtonWavenumberUnitNmInv.UseVisualStyleBackColor = true;
            radioButtonWavenumberUnitNmInv.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // flowLayoutPanelEnergy
            // 
            resources.ApplyResources(flowLayoutPanelEnergy, "flowLayoutPanelEnergy");
            flowLayoutPanelEnergy.Controls.Add(radioButtonEnergyUnitEv);
            flowLayoutPanelEnergy.Controls.Add(radioButtonEnergyUnitKev);
            flowLayoutPanelEnergy.Controls.Add(radioButtonEnergyUnitMev);
            flowLayoutPanelEnergy.Controls.Add(numericBoxTwoTheta);
            flowLayoutPanelEnergy.Name = "flowLayoutPanelEnergy";
            toolTip.SetToolTip(flowLayoutPanelEnergy, resources.GetString("flowLayoutPanelEnergy.ToolTip"));
            // 
            // radioButtonEnergyUnitEv
            // 
            resources.ApplyResources(radioButtonEnergyUnitEv, "radioButtonEnergyUnitEv");
            radioButtonEnergyUnitEv.Checked = true;
            radioButtonEnergyUnitEv.Name = "radioButtonEnergyUnitEv";
            radioButtonEnergyUnitEv.TabStop = true;
            toolTip.SetToolTip(radioButtonEnergyUnitEv, resources.GetString("radioButtonEnergyUnitEv.ToolTip"));
            radioButtonEnergyUnitEv.UseVisualStyleBackColor = true;
            radioButtonEnergyUnitEv.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonEnergyUnitKev
            // 
            resources.ApplyResources(radioButtonEnergyUnitKev, "radioButtonEnergyUnitKev");
            radioButtonEnergyUnitKev.Name = "radioButtonEnergyUnitKev";
            toolTip.SetToolTip(radioButtonEnergyUnitKev, resources.GetString("radioButtonEnergyUnitKev.ToolTip"));
            radioButtonEnergyUnitKev.UseVisualStyleBackColor = true;
            radioButtonEnergyUnitKev.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonEnergyUnitMev
            // 
            resources.ApplyResources(radioButtonEnergyUnitMev, "radioButtonEnergyUnitMev");
            radioButtonEnergyUnitMev.Name = "radioButtonEnergyUnitMev";
            toolTip.SetToolTip(radioButtonEnergyUnitMev, resources.GetString("radioButtonEnergyUnitMev.ToolTip"));
            radioButtonEnergyUnitMev.UseVisualStyleBackColor = true;
            radioButtonEnergyUnitMev.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // numericBoxTwoTheta
            // 
            resources.ApplyResources(numericBoxTwoTheta, "numericBoxTwoTheta");
            numericBoxTwoTheta.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTwoTheta.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTwoTheta.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTwoTheta.Name = "numericBoxTwoTheta";
            numericBoxTwoTheta.SkipEventDuringInput = false;
            numericBoxTwoTheta.SmartIncrement = true;
            numericBoxTwoTheta.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxTwoTheta, resources.GetString("numericBoxTwoTheta.ToolTip"));
            numericBoxTwoTheta.ValueChanged += numericBoxTwoTheta_ValueChanged;
            // 
            // flowLayoutPanelNeutronTOF
            // 
            resources.ApplyResources(flowLayoutPanelNeutronTOF, "flowLayoutPanelNeutronTOF");
            flowLayoutPanelNeutronTOF.Controls.Add(radioButtonTofUnitMicroSec);
            flowLayoutPanelNeutronTOF.Controls.Add(radioButtonTofUnitNanoSec);
            flowLayoutPanelNeutronTOF.Controls.Add(numericBoxTofAngle);
            flowLayoutPanelNeutronTOF.Controls.Add(numericBoxTofLength);
            flowLayoutPanelNeutronTOF.Name = "flowLayoutPanelNeutronTOF";
            toolTip.SetToolTip(flowLayoutPanelNeutronTOF, resources.GetString("flowLayoutPanelNeutronTOF.ToolTip"));
            // 
            // radioButtonTofUnitMicroSec
            // 
            resources.ApplyResources(radioButtonTofUnitMicroSec, "radioButtonTofUnitMicroSec");
            radioButtonTofUnitMicroSec.Checked = true;
            radioButtonTofUnitMicroSec.Name = "radioButtonTofUnitMicroSec";
            radioButtonTofUnitMicroSec.TabStop = true;
            toolTip.SetToolTip(radioButtonTofUnitMicroSec, resources.GetString("radioButtonTofUnitMicroSec.ToolTip"));
            radioButtonTofUnitMicroSec.UseVisualStyleBackColor = true;
            radioButtonTofUnitMicroSec.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // radioButtonTofUnitNanoSec
            // 
            resources.ApplyResources(radioButtonTofUnitNanoSec, "radioButtonTofUnitNanoSec");
            radioButtonTofUnitNanoSec.Name = "radioButtonTofUnitNanoSec";
            toolTip.SetToolTip(radioButtonTofUnitNanoSec, resources.GetString("radioButtonTofUnitNanoSec.ToolTip"));
            radioButtonTofUnitNanoSec.UseVisualStyleBackColor = true;
            radioButtonTofUnitNanoSec.CheckedChanged += radioButtonTwoTheta_CheckedChanged;
            // 
            // numericBoxTofAngle
            // 
            resources.ApplyResources(numericBoxTofAngle, "numericBoxTofAngle");
            numericBoxTofAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTofAngle.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTofAngle.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTofAngle.Name = "numericBoxTofAngle";
            numericBoxTofAngle.RadianValue = 1.5707963267948966D;
            numericBoxTofAngle.SkipEventDuringInput = false;
            numericBoxTofAngle.SmartIncrement = true;
            numericBoxTofAngle.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxTofAngle, resources.GetString("numericBoxTofAngle.ToolTip"));
            numericBoxTofAngle.Value = 90D;
            numericBoxTofAngle.ValueChanged += numericBoxTwoTheta_ValueChanged;
            // 
            // numericBoxTofLength
            // 
            resources.ApplyResources(numericBoxTofLength, "numericBoxTofLength");
            numericBoxTofLength.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTofLength.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTofLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTofLength.Name = "numericBoxTofLength";
            numericBoxTofLength.RadianValue = 0.73303828583761843D;
            numericBoxTofLength.SkipEventDuringInput = false;
            numericBoxTofLength.SmartIncrement = true;
            numericBoxTofLength.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxTofLength, resources.GetString("numericBoxTofLength.ToolTip"));
            numericBoxTofLength.Value = 42D;
            numericBoxTofLength.ValueChanged += numericBoxTwoTheta_ValueChanged;
            // 
            // flowLayoutPanelMain
            // 
            resources.ApplyResources(flowLayoutPanelMain, "flowLayoutPanelMain");
            flowLayoutPanelMain.Controls.Add(groupBoxWaveProperty);
            flowLayoutPanelMain.Controls.Add(groupBoxHorizontalAxis);
            flowLayoutPanelMain.Name = "flowLayoutPanelMain";
            toolTip.SetToolTip(flowLayoutPanelMain, resources.GetString("flowLayoutPanelMain.ToolTip"));
            // 
            // HorizontalAxisUserControl
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(flowLayoutPanelMain);
            Name = "HorizontalAxisUserControl";
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            groupBoxWaveProperty.ResumeLayout(false);
            groupBoxWaveProperty.PerformLayout();
            flowLayoutPanelWaveProperty.ResumeLayout(false);
            flowLayoutPanelWaveProperty.PerformLayout();
            flowLayoutPanelSourceColor.ResumeLayout(false);
            flowLayoutPanelSourceColor.PerformLayout();
            groupBoxSource.ResumeLayout(false);
            groupBoxSource.PerformLayout();
            flowLayoutPanelSourceType.ResumeLayout(false);
            flowLayoutPanelSourceType.PerformLayout();
            groupBoxColor.ResumeLayout(false);
            groupBoxColor.PerformLayout();
            flowLayoutPanelWaveMode.ResumeLayout(false);
            flowLayoutPanelWaveMode.PerformLayout();
            groupBoxTwoTheta.ResumeLayout(false);
            groupBoxTwoTheta.PerformLayout();
            groupBoxHorizontalAxis.ResumeLayout(false);
            groupBoxHorizontalAxis.PerformLayout();
            flowLayoutPanelAxisSelection.ResumeLayout(false);
            flowLayoutPanelAxisSelection.PerformLayout();
            flowLayoutPanelAxisType.ResumeLayout(false);
            flowLayoutPanelAxisType.PerformLayout();
            groupBoxOption.ResumeLayout(false);
            groupBoxOption.PerformLayout();
            flowLayoutPanelAxisValues.ResumeLayout(false);
            flowLayoutPanelAxisValues.PerformLayout();
            flowLayoutPanelTwoTheta.ResumeLayout(false);
            flowLayoutPanelTwoTheta.PerformLayout();
            flowLayoutPanelDspacing.ResumeLayout(false);
            flowLayoutPanelDspacing.PerformLayout();
            flowLayoutPanelWavenumber.ResumeLayout(false);
            flowLayoutPanelWavenumber.PerformLayout();
            flowLayoutPanelEnergy.ResumeLayout(false);
            flowLayoutPanelEnergy.PerformLayout();
            flowLayoutPanelNeutronTOF.ResumeLayout(false);
            flowLayoutPanelNeutronTOF.PerformLayout();
            flowLayoutPanelMain.ResumeLayout(false);
            flowLayoutPanelMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxWaveProperty;
        private System.Windows.Forms.RadioButton radioButtonDspacing;
        private System.Windows.Forms.RadioButton radioButtonEnergy;
        private System.Windows.Forms.GroupBox groupBoxTwoTheta;
        private Crystallography.Controls.NumericBox numericBoxTwoTheta;
        private WaveLengthControl waveLengthControl;
        private System.Windows.Forms.RadioButton radioButtonTOF;
        private NumericBox numericBoxTofLength;
        private System.Windows.Forms.RadioButton radioButtonNeutron;
        private System.Windows.Forms.RadioButton radioButtonElectron;
        private System.Windows.Forms.RadioButton radioButtonXray;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private NumericBox numericBoxTofAngle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelWaveMode;
        private System.Windows.Forms.RadioButton radioButtonMonochro;
        private System.Windows.Forms.RadioButton radioButtonFlatWhite;
        private System.Windows.Forms.GroupBox groupBoxHorizontalAxis;
        private System.Windows.Forms.RadioButton radioButtonTwoTheta;
        public System.Windows.Forms.RadioButton radioButtonTofUnitNanoSec;
        public System.Windows.Forms.RadioButton radioButtonTofUnitMicroSec;
        private System.Windows.Forms.RadioButton radioButtonEnergyUnitEv;
        private System.Windows.Forms.RadioButton radioButtonEnergyUnitKev;
        public System.Windows.Forms.RadioButton radioButtonDspacingUnitAng;
        public System.Windows.Forms.RadioButton radioButtonDspacingUnitNm;
        private System.Windows.Forms.RadioButton radioButtonCustomWhite;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBoxColor;
        private System.Windows.Forms.GroupBox groupBoxSource;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSourceType;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSourceColor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAxisType;
        private System.Windows.Forms.RadioButton radioButtonWavenumber;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAxisValues;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDspacing;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelWavenumber;
        public System.Windows.Forms.RadioButton radioButtonWavenumberUnitNmInv;
        public System.Windows.Forms.RadioButton radioButtonWavenumberAngInv;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelEnergy;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelNeutronTOF;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAxisSelection;
        private System.Windows.Forms.GroupBox groupBoxOption;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelWaveProperty;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTwoTheta;
        public System.Windows.Forms.RadioButton radioButtonAngleUnitRadian;
        private System.Windows.Forms.RadioButton radioButtonEnergyUnitMev;
        public System.Windows.Forms.RadioButton radioButtonAngleUnitDegree;
        public System.Windows.Forms.RadioButton radioButtonAngleUnitCentiDegree;
        public System.Windows.Forms.RadioButton radioButtonAngleUnitMilliRadian;
    }
}
