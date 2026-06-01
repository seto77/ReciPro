namespace Crystallography.Controls;

partial class SearchCrystalControl
{
    /// <summary>必要なデザイナー変数です。</summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>使用中のリソースをすべてクリーンアップします。</summary>
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
            components = new System.ComponentModel.Container(); // (260531Ch)
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchCrystalControl));
        flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
        checkBoxSearchName = new System.Windows.Forms.CheckBox();
        textBoxSearchName = new System.Windows.Forms.TextBox();
        checkBoxSearchElements = new System.Windows.Forms.CheckBox();
        buttonPeriodicTable = new System.Windows.Forms.Button();
        checkBoxSearchReference = new System.Windows.Forms.CheckBox();
        textBoxSearchReference = new System.Windows.Forms.TextBox();
        checkBoxSearchCrystalSystem = new System.Windows.Forms.CheckBox();
        comboBoxSearchCrystalSystem = new System.Windows.Forms.ComboBox();
        checkBoxSearchCellParameter = new System.Windows.Forms.CheckBox();
        groupBoxCellParameter = new System.Windows.Forms.GroupBox();
        numericBoxCellGamma = new NumericBox();
        numericBoxCellAngleErr = new NumericBox();
        numericBoxCellLengthErr = new NumericBox();
        numericBoxCellC = new NumericBox();
        numericBoxCellBeta = new NumericBox();
        numericBoxCellAlpha = new NumericBox();
        numericBoxCellB = new NumericBox();
        numericBoxCellA = new NumericBox();
        checkBoxDspacing = new System.Windows.Forms.CheckBox();
        groupBoxDspacing = new System.Windows.Forms.GroupBox();
        checkBoxD3 = new System.Windows.Forms.CheckBox();
        numericBoxD3Err = new NumericBox();
        numericBoxD2Err = new NumericBox();
        numericBoxD1Err = new NumericBox();
        checkBoxD2 = new System.Windows.Forms.CheckBox();
        checkBoxD1 = new System.Windows.Forms.CheckBox();
        numericBoxD3 = new NumericBox();
        numericBoxD2 = new NumericBox();
        numericBoxD1 = new NumericBox();
        checkBoxIgnoreScatteringFactor = new System.Windows.Forms.CheckBox();
        checkBoxDensity = new System.Windows.Forms.CheckBox();
        groupBoxDensity = new System.Windows.Forms.GroupBox();
        numericBoxDensity = new NumericBox();
        numericBoxDensityErr = new NumericBox();
        backgroundWorkerSearch = new System.ComponentModel.BackgroundWorker();
        buttonSearch = new System.Windows.Forms.Button();
        flowLayoutPanel1.SuspendLayout();
        groupBoxCellParameter.SuspendLayout();
        groupBoxDspacing.SuspendLayout();
        groupBoxDensity.SuspendLayout();
        SuspendLayout();
        // 
        // flowLayoutPanel1
        // 
        resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
        flowLayoutPanel1.Controls.Add(checkBoxSearchName);
        flowLayoutPanel1.Controls.Add(textBoxSearchName);
        flowLayoutPanel1.Controls.Add(checkBoxSearchElements);
        flowLayoutPanel1.Controls.Add(buttonPeriodicTable);
        flowLayoutPanel1.Controls.Add(checkBoxSearchReference);
        flowLayoutPanel1.Controls.Add(textBoxSearchReference);
        flowLayoutPanel1.Controls.Add(checkBoxSearchCrystalSystem);
        flowLayoutPanel1.Controls.Add(comboBoxSearchCrystalSystem);
        flowLayoutPanel1.Controls.Add(checkBoxSearchCellParameter);
        flowLayoutPanel1.Controls.Add(groupBoxCellParameter);
        flowLayoutPanel1.Controls.Add(checkBoxDspacing);
        flowLayoutPanel1.Controls.Add(groupBoxDspacing);
        flowLayoutPanel1.Controls.Add(checkBoxDensity);
        flowLayoutPanel1.Controls.Add(groupBoxDensity);
        flowLayoutPanel1.Name = "flowLayoutPanel1";
        // 
        // checkBoxSearchName
        // 
        resources.ApplyResources(checkBoxSearchName, "checkBoxSearchName");
        toolTip.SetToolTip(checkBoxSearchName, resources.GetString("checkBoxSearchName.ToolTip")); // 260531Cl
        checkBoxSearchName.Name = "checkBoxSearchName";
        checkBoxSearchName.UseVisualStyleBackColor = true;
        checkBoxSearchName.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // textBoxSearchName
        // 
        resources.ApplyResources(textBoxSearchName, "textBoxSearchName");
        toolTip.SetToolTip(textBoxSearchName, resources.GetString("textBoxSearchName.ToolTip")); // 260531Cl
        textBoxSearchName.Name = "textBoxSearchName";
        textBoxSearchName.KeyDown += textBoxSearchName_KeyDown;
        // 
        // checkBoxSearchElements
        // 
        resources.ApplyResources(checkBoxSearchElements, "checkBoxSearchElements");
        toolTip.SetToolTip(checkBoxSearchElements, resources.GetString("checkBoxSearchElements.ToolTip")); // 260531Cl
        checkBoxSearchElements.Name = "checkBoxSearchElements";
        checkBoxSearchElements.UseVisualStyleBackColor = true;
        checkBoxSearchElements.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // buttonPeriodicTable
        // 
        resources.ApplyResources(buttonPeriodicTable, "buttonPeriodicTable");
        toolTip.SetToolTip(buttonPeriodicTable, resources.GetString("buttonPeriodicTable.ToolTip")); // 260531Cl
        buttonPeriodicTable.Name = "buttonPeriodicTable";
        buttonPeriodicTable.UseVisualStyleBackColor = true;
        buttonPeriodicTable.Click += buttonPeriodicTable_Click;
        // 
        // checkBoxSearchReference
        // 
        resources.ApplyResources(checkBoxSearchReference, "checkBoxSearchReference");
        toolTip.SetToolTip(checkBoxSearchReference, resources.GetString("checkBoxSearchReference.ToolTip")); // 260531Cl
        checkBoxSearchReference.Name = "checkBoxSearchReference";
        checkBoxSearchReference.UseVisualStyleBackColor = true;
        checkBoxSearchReference.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // textBoxSearchReference
        // 
        resources.ApplyResources(textBoxSearchReference, "textBoxSearchReference");
        toolTip.SetToolTip(textBoxSearchReference, resources.GetString("textBoxSearchReference.ToolTip")); // 260531Cl
        textBoxSearchReference.Name = "textBoxSearchReference";
        textBoxSearchReference.KeyDown += textBoxSearchName_KeyDown;
        // 
        // checkBoxSearchCrystalSystem
        // 
        resources.ApplyResources(checkBoxSearchCrystalSystem, "checkBoxSearchCrystalSystem");
        toolTip.SetToolTip(checkBoxSearchCrystalSystem, resources.GetString("checkBoxSearchCrystalSystem.ToolTip")); // 260531Cl
        checkBoxSearchCrystalSystem.Name = "checkBoxSearchCrystalSystem";
        checkBoxSearchCrystalSystem.UseVisualStyleBackColor = true;
        checkBoxSearchCrystalSystem.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // comboBoxSearchCrystalSystem
        // 
        comboBoxSearchCrystalSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        resources.ApplyResources(comboBoxSearchCrystalSystem, "comboBoxSearchCrystalSystem");
        toolTip.SetToolTip(comboBoxSearchCrystalSystem, resources.GetString("comboBoxSearchCrystalSystem.ToolTip")); // 260531Cl
        comboBoxSearchCrystalSystem.Items.AddRange(new object[] { resources.GetString("comboBoxSearchCrystalSystem.Items"), resources.GetString("comboBoxSearchCrystalSystem.Items1"), resources.GetString("comboBoxSearchCrystalSystem.Items2"), resources.GetString("comboBoxSearchCrystalSystem.Items3"), resources.GetString("comboBoxSearchCrystalSystem.Items4"), resources.GetString("comboBoxSearchCrystalSystem.Items5"), resources.GetString("comboBoxSearchCrystalSystem.Items6"), resources.GetString("comboBoxSearchCrystalSystem.Items7") });
        comboBoxSearchCrystalSystem.Name = "comboBoxSearchCrystalSystem";
        // 
        // checkBoxSearchCellParameter
        // 
        resources.ApplyResources(checkBoxSearchCellParameter, "checkBoxSearchCellParameter");
        toolTip.SetToolTip(checkBoxSearchCellParameter, resources.GetString("checkBoxSearchCellParameter.ToolTip")); // 260531Cl
        checkBoxSearchCellParameter.Name = "checkBoxSearchCellParameter";
        checkBoxSearchCellParameter.UseVisualStyleBackColor = true;
        checkBoxSearchCellParameter.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // groupBoxCellParameter
        // 
        captureExtender.SetCapture(groupBoxCellParameter, true);
        groupBoxCellParameter.Controls.Add(numericBoxCellGamma);
        groupBoxCellParameter.Controls.Add(numericBoxCellAngleErr);
        groupBoxCellParameter.Controls.Add(numericBoxCellLengthErr);
        groupBoxCellParameter.Controls.Add(numericBoxCellC);
        groupBoxCellParameter.Controls.Add(numericBoxCellBeta);
        groupBoxCellParameter.Controls.Add(numericBoxCellAlpha);
        groupBoxCellParameter.Controls.Add(numericBoxCellB);
        groupBoxCellParameter.Controls.Add(numericBoxCellA);
        resources.ApplyResources(groupBoxCellParameter, "groupBoxCellParameter");
        groupBoxCellParameter.Name = "groupBoxCellParameter";
        groupBoxCellParameter.TabStop = false;
        // 
        // numericBoxCellGamma
        // 
        resources.ApplyResources(numericBoxCellGamma, "numericBoxCellGamma");
        toolTip.SetToolTip(numericBoxCellGamma, resources.GetString("numericBoxCellGamma.ToolTip")); // 260531Cl
        numericBoxCellGamma.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellGamma.Maximum = 179D;
        numericBoxCellGamma.Minimum = 0D;
        numericBoxCellGamma.Name = "numericBoxCellGamma";
        numericBoxCellGamma.SkipEventDuringInput = false;
        numericBoxCellGamma.SmartIncrement = true;
        numericBoxCellGamma.ThousandsSeparator = true;
        numericBoxCellGamma.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellAngleErr
        // 
        resources.ApplyResources(numericBoxCellAngleErr, "numericBoxCellAngleErr");
        toolTip.SetToolTip(numericBoxCellAngleErr, resources.GetString("numericBoxCellAngleErr.ToolTip")); // 260531Cl
        numericBoxCellAngleErr.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellAngleErr.DecimalPlaces = 1;
        numericBoxCellAngleErr.Maximum = 50D;
        numericBoxCellAngleErr.Minimum = 0D;
        numericBoxCellAngleErr.Name = "numericBoxCellAngleErr";
        numericBoxCellAngleErr.RadianValue = 0.052359877559829883D;
        numericBoxCellAngleErr.ShowUpDown = true;
        numericBoxCellAngleErr.SkipEventDuringInput = false;
        numericBoxCellAngleErr.ThousandsSeparator = true;
        numericBoxCellAngleErr.UpDown_Increment = 0.5D;
        numericBoxCellAngleErr.Value = 3D;
        numericBoxCellAngleErr.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellLengthErr
        // 
        resources.ApplyResources(numericBoxCellLengthErr, "numericBoxCellLengthErr");
        toolTip.SetToolTip(numericBoxCellLengthErr, resources.GetString("numericBoxCellLengthErr.ToolTip")); // 260531Cl
        numericBoxCellLengthErr.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellLengthErr.DecimalPlaces = 1;
        numericBoxCellLengthErr.Maximum = 50D;
        numericBoxCellLengthErr.Minimum = 0D;
        numericBoxCellLengthErr.Name = "numericBoxCellLengthErr";
        numericBoxCellLengthErr.RadianValue = 0.052359877559829883D;
        numericBoxCellLengthErr.ShowUpDown = true;
        numericBoxCellLengthErr.SkipEventDuringInput = false;
        numericBoxCellLengthErr.ThousandsSeparator = true;
        numericBoxCellLengthErr.UpDown_Increment = 0.5D;
        numericBoxCellLengthErr.Value = 3D;
        numericBoxCellLengthErr.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellC
        // 
        resources.ApplyResources(numericBoxCellC, "numericBoxCellC");
        toolTip.SetToolTip(numericBoxCellC, resources.GetString("numericBoxCellC.ToolTip")); // 260531Cl
        numericBoxCellC.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellC.Maximum = 100D;
        numericBoxCellC.Minimum = 0D;
        numericBoxCellC.Name = "numericBoxCellC";
        numericBoxCellC.SkipEventDuringInput = false;
        numericBoxCellC.SmartIncrement = true;
        numericBoxCellC.ThousandsSeparator = true;
        numericBoxCellC.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellBeta
        // 
        resources.ApplyResources(numericBoxCellBeta, "numericBoxCellBeta");
        toolTip.SetToolTip(numericBoxCellBeta, resources.GetString("numericBoxCellBeta.ToolTip")); // 260531Cl
        numericBoxCellBeta.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellBeta.Maximum = 179D;
        numericBoxCellBeta.Minimum = 0D;
        numericBoxCellBeta.Name = "numericBoxCellBeta";
        numericBoxCellBeta.SkipEventDuringInput = false;
        numericBoxCellBeta.SmartIncrement = true;
        numericBoxCellBeta.ThousandsSeparator = true;
        numericBoxCellBeta.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellAlpha
        // 
        resources.ApplyResources(numericBoxCellAlpha, "numericBoxCellAlpha");
        toolTip.SetToolTip(numericBoxCellAlpha, resources.GetString("numericBoxCellAlpha.ToolTip")); // 260531Cl
        numericBoxCellAlpha.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellAlpha.Maximum = 179D;
        numericBoxCellAlpha.Minimum = 0D;
        numericBoxCellAlpha.Name = "numericBoxCellAlpha";
        numericBoxCellAlpha.SkipEventDuringInput = false;
        numericBoxCellAlpha.SmartIncrement = true;
        numericBoxCellAlpha.ThousandsSeparator = true;
        numericBoxCellAlpha.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellB
        // 
        resources.ApplyResources(numericBoxCellB, "numericBoxCellB");
        toolTip.SetToolTip(numericBoxCellB, resources.GetString("numericBoxCellB.ToolTip")); // 260531Cl
        numericBoxCellB.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellB.Maximum = 100D;
        numericBoxCellB.Minimum = 0D;
        numericBoxCellB.Name = "numericBoxCellB";
        numericBoxCellB.SkipEventDuringInput = false;
        numericBoxCellB.SmartIncrement = true;
        numericBoxCellB.ThousandsSeparator = true;
        numericBoxCellB.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellA
        // 
        resources.ApplyResources(numericBoxCellA, "numericBoxCellA");
        toolTip.SetToolTip(numericBoxCellA, resources.GetString("numericBoxCellA.ToolTip")); // 260531Cl
        numericBoxCellA.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellA.Maximum = 100D;
        numericBoxCellA.Minimum = 0D;
        numericBoxCellA.Name = "numericBoxCellA";
        numericBoxCellA.SkipEventDuringInput = false;
        numericBoxCellA.SmartIncrement = true;
        numericBoxCellA.ThousandsSeparator = true;
        numericBoxCellA.KeyDown += textBoxSearchName_KeyDown;
        // 
        // checkBoxDspacing
        // 
        resources.ApplyResources(checkBoxDspacing, "checkBoxDspacing");
        toolTip.SetToolTip(checkBoxDspacing, resources.GetString("checkBoxDspacing.ToolTip")); // 260531Cl
        checkBoxDspacing.Name = "checkBoxDspacing";
        checkBoxDspacing.UseVisualStyleBackColor = true;
        checkBoxDspacing.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // groupBoxDspacing
        // 
        captureExtender.SetCapture(groupBoxDspacing, true);
        groupBoxDspacing.Controls.Add(checkBoxD3);
        groupBoxDspacing.Controls.Add(numericBoxD3Err);
        groupBoxDspacing.Controls.Add(numericBoxD2Err);
        groupBoxDspacing.Controls.Add(numericBoxD1Err);
        groupBoxDspacing.Controls.Add(checkBoxD2);
        groupBoxDspacing.Controls.Add(checkBoxD1);
        groupBoxDspacing.Controls.Add(numericBoxD3);
        groupBoxDspacing.Controls.Add(numericBoxD2);
        groupBoxDspacing.Controls.Add(numericBoxD1);
        groupBoxDspacing.Controls.Add(checkBoxIgnoreScatteringFactor);
        resources.ApplyResources(groupBoxDspacing, "groupBoxDspacing");
        groupBoxDspacing.Name = "groupBoxDspacing";
        groupBoxDspacing.TabStop = false;
        // 
        // checkBoxD3
        // 
        resources.ApplyResources(checkBoxD3, "checkBoxD3");
        toolTip.SetToolTip(checkBoxD3, resources.GetString("checkBoxD3.ToolTip")); // 260531Cl
        checkBoxD3.Name = "checkBoxD3";
        checkBoxD3.UseVisualStyleBackColor = true;
        checkBoxD3.CheckedChanged += checkBoxD3_CheckedChanged;
        // 
        // numericBoxD3Err
        // 
        resources.ApplyResources(numericBoxD3Err, "numericBoxD3Err");
        toolTip.SetToolTip(numericBoxD3Err, resources.GetString("numericBoxD3Err.ToolTip")); // 260531Cl
        numericBoxD3Err.BackColor = System.Drawing.Color.Transparent;
        numericBoxD3Err.DecimalPlaces = 1;
        numericBoxD3Err.Maximum = 50D;
        numericBoxD3Err.Minimum = 0D;
        numericBoxD3Err.Name = "numericBoxD3Err";
        numericBoxD3Err.RadianValue = 0.052359877559829883D;
        numericBoxD3Err.ShowUpDown = true;
        numericBoxD3Err.SkipEventDuringInput = false;
        numericBoxD3Err.ThousandsSeparator = true;
        numericBoxD3Err.UpDown_Increment = 0.5D;
        numericBoxD3Err.Value = 3D;
        numericBoxD3Err.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxD2Err
        // 
        resources.ApplyResources(numericBoxD2Err, "numericBoxD2Err");
        toolTip.SetToolTip(numericBoxD2Err, resources.GetString("numericBoxD2Err.ToolTip")); // 260531Cl
        numericBoxD2Err.BackColor = System.Drawing.Color.Transparent;
        numericBoxD2Err.DecimalPlaces = 1;
        numericBoxD2Err.Maximum = 50D;
        numericBoxD2Err.Minimum = 0D;
        numericBoxD2Err.Name = "numericBoxD2Err";
        numericBoxD2Err.RadianValue = 0.052359877559829883D;
        numericBoxD2Err.ShowUpDown = true;
        numericBoxD2Err.SkipEventDuringInput = false;
        numericBoxD2Err.ThousandsSeparator = true;
        numericBoxD2Err.UpDown_Increment = 0.5D;
        numericBoxD2Err.Value = 3D;
        numericBoxD2Err.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxD1Err
        // 
        resources.ApplyResources(numericBoxD1Err, "numericBoxD1Err");
        toolTip.SetToolTip(numericBoxD1Err, resources.GetString("numericBoxD1Err.ToolTip")); // 260531Cl
        numericBoxD1Err.BackColor = System.Drawing.Color.Transparent;
        numericBoxD1Err.DecimalPlaces = 1;
        numericBoxD1Err.Maximum = 50D;
        numericBoxD1Err.Minimum = 0D;
        numericBoxD1Err.Name = "numericBoxD1Err";
        numericBoxD1Err.RadianValue = 0.052359877559829883D;
        numericBoxD1Err.ShowUpDown = true;
        numericBoxD1Err.SkipEventDuringInput = false;
        numericBoxD1Err.ThousandsSeparator = true;
        numericBoxD1Err.UpDown_Increment = 0.5D;
        numericBoxD1Err.Value = 3D;
        numericBoxD1Err.KeyDown += textBoxSearchName_KeyDown;
        // 
        // checkBoxD2
        // 
        resources.ApplyResources(checkBoxD2, "checkBoxD2");
        toolTip.SetToolTip(checkBoxD2, resources.GetString("checkBoxD2.ToolTip")); // 260531Cl
        checkBoxD2.Name = "checkBoxD2";
        checkBoxD2.UseVisualStyleBackColor = true;
        checkBoxD2.CheckedChanged += checkBoxD2_CheckedChanged;
        // 
        // checkBoxD1
        // 
        resources.ApplyResources(checkBoxD1, "checkBoxD1");
        toolTip.SetToolTip(checkBoxD1, resources.GetString("checkBoxD1.ToolTip")); // 260531Cl
        checkBoxD1.Checked = true;
        checkBoxD1.CheckState = System.Windows.Forms.CheckState.Checked;
        checkBoxD1.Name = "checkBoxD1";
        checkBoxD1.UseVisualStyleBackColor = true;
        checkBoxD1.CheckedChanged += checkBoxD1_CheckedChanged;
        // 
        // numericBoxD3
        // 
        resources.ApplyResources(numericBoxD3, "numericBoxD3");
        toolTip.SetToolTip(numericBoxD3, resources.GetString("numericBoxD3.ToolTip")); // 260531Cl
        numericBoxD3.BackColor = System.Drawing.Color.Transparent;
        numericBoxD3.DecimalPlaces = 2;
        numericBoxD3.Maximum = 100D;
        numericBoxD3.Minimum = 0D;
        numericBoxD3.Name = "numericBoxD3";
        numericBoxD3.SkipEventDuringInput = false;
        numericBoxD3.SmartIncrement = true;
        numericBoxD3.ThousandsSeparator = true;
        numericBoxD3.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxD2
        // 
        resources.ApplyResources(numericBoxD2, "numericBoxD2");
        toolTip.SetToolTip(numericBoxD2, resources.GetString("numericBoxD2.ToolTip")); // 260531Cl
        numericBoxD2.BackColor = System.Drawing.Color.Transparent;
        numericBoxD2.DecimalPlaces = 2;
        numericBoxD2.Maximum = 100D;
        numericBoxD2.Minimum = 0D;
        numericBoxD2.Name = "numericBoxD2";
        numericBoxD2.SkipEventDuringInput = false;
        numericBoxD2.SmartIncrement = true;
        numericBoxD2.ThousandsSeparator = true;
        numericBoxD2.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxD1
        // 
        resources.ApplyResources(numericBoxD1, "numericBoxD1");
        toolTip.SetToolTip(numericBoxD1, resources.GetString("numericBoxD1.ToolTip")); // 260531Cl
        numericBoxD1.BackColor = System.Drawing.Color.Transparent;
        numericBoxD1.DecimalPlaces = 2;
        numericBoxD1.Maximum = 100D;
        numericBoxD1.Minimum = 0D;
        numericBoxD1.Name = "numericBoxD1";
        numericBoxD1.SkipEventDuringInput = false;
        numericBoxD1.SmartIncrement = true;
        numericBoxD1.ThousandsSeparator = true;
        numericBoxD1.KeyDown += textBoxSearchName_KeyDown;
        // 
        // checkBoxIgnoreScatteringFactor
        // 
        resources.ApplyResources(checkBoxIgnoreScatteringFactor, "checkBoxIgnoreScatteringFactor");
        toolTip.SetToolTip(checkBoxIgnoreScatteringFactor, resources.GetString("checkBoxIgnoreScatteringFactor.ToolTip")); // 260531Cl
        checkBoxIgnoreScatteringFactor.Name = "checkBoxIgnoreScatteringFactor";
        checkBoxIgnoreScatteringFactor.UseVisualStyleBackColor = true;
        // 
        // checkBoxDensity
        // 
        resources.ApplyResources(checkBoxDensity, "checkBoxDensity");
        toolTip.SetToolTip(checkBoxDensity, resources.GetString("checkBoxDensity.ToolTip")); // 260531Cl
        checkBoxDensity.Name = "checkBoxDensity";
        checkBoxDensity.UseVisualStyleBackColor = true;
        checkBoxDensity.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // groupBoxDensity
        // 
        captureExtender.SetCapture(groupBoxDensity, true);
        groupBoxDensity.Controls.Add(numericBoxDensity);
        groupBoxDensity.Controls.Add(numericBoxDensityErr);
        resources.ApplyResources(groupBoxDensity, "groupBoxDensity");
        groupBoxDensity.Name = "groupBoxDensity";
        groupBoxDensity.TabStop = false;
        // 
        // numericBoxDensity
        // 
        resources.ApplyResources(numericBoxDensity, "numericBoxDensity");
        toolTip.SetToolTip(numericBoxDensity, resources.GetString("numericBoxDensity.ToolTip")); // 260531Cl
        numericBoxDensity.BackColor = System.Drawing.Color.Transparent;
        numericBoxDensity.DecimalPlaces = 3;
        numericBoxDensity.Maximum = 100D;
        numericBoxDensity.Minimum = 0D;
        numericBoxDensity.Name = "numericBoxDensity";
        numericBoxDensity.SkipEventDuringInput = false;
        numericBoxDensity.SmartIncrement = true;
        numericBoxDensity.ThousandsSeparator = true;
        numericBoxDensity.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxDensityErr
        // 
        resources.ApplyResources(numericBoxDensityErr, "numericBoxDensityErr");
        toolTip.SetToolTip(numericBoxDensityErr, resources.GetString("numericBoxDensityErr.ToolTip")); // 260531Cl
        numericBoxDensityErr.BackColor = System.Drawing.Color.Transparent;
        numericBoxDensityErr.DecimalPlaces = 1;
        numericBoxDensityErr.Maximum = 50D;
        numericBoxDensityErr.Minimum = 0D;
        numericBoxDensityErr.Name = "numericBoxDensityErr";
        numericBoxDensityErr.RadianValue = 0.052359877559829883D;
        numericBoxDensityErr.ShowUpDown = true;
        numericBoxDensityErr.SkipEventDuringInput = false;
        numericBoxDensityErr.ThousandsSeparator = true;
        numericBoxDensityErr.UpDown_Increment = 0.5D;
        numericBoxDensityErr.Value = 3D;
        numericBoxDensityErr.KeyDown += textBoxSearchName_KeyDown;
        // 
        // backgroundWorkerSearch
        // 
        backgroundWorkerSearch.WorkerReportsProgress = true;
        backgroundWorkerSearch.DoWork += backgroundWorkerSearch_DoWork;
        backgroundWorkerSearch.ProgressChanged += backgroundWorkerSearch_ProgressChanged;
        backgroundWorkerSearch.RunWorkerCompleted += backgroundWorkerSearch_RunWorkerCompleted;
        // 
        // buttonSearch
        // 
        resources.ApplyResources(buttonSearch, "buttonSearch");
        toolTip.SetToolTip(buttonSearch, resources.GetString("buttonSearch.ToolTip")); // 260531Cl
        //buttonSearch.BackColor = System.Drawing.Color.Chocolate; // 260520Cl: orange→SteelBlue (主要アクション色を統一)
        buttonSearch.BackColor = System.Drawing.Color.SteelBlue;
        buttonSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
        buttonSearch.Name = "buttonSearch";
        buttonSearch.UseVisualStyleBackColor = false;
        buttonSearch.Click += buttonSearch_Click;
        // 
        // SearchCrystalControl
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
        captureExtender.SetCapture(this, true);
        Controls.Add(flowLayoutPanel1);
        Controls.Add(buttonSearch);
        Name = "SearchCrystalControl";
        flowLayoutPanel1.ResumeLayout(false);
        flowLayoutPanel1.PerformLayout();
        groupBoxCellParameter.ResumeLayout(false);
        groupBoxDspacing.ResumeLayout(false);
        groupBoxDspacing.PerformLayout();
        groupBoxDensity.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();

    }

    #endregion

        public System.Windows.Forms.ToolTip toolTip; // (260531Ch) 260531Cl private->public: FormMainのToolTip表示トグルから一括制御するため

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.CheckBox checkBoxSearchName;
    private System.Windows.Forms.TextBox textBoxSearchName;
    private System.Windows.Forms.CheckBox checkBoxSearchElements;
    private System.Windows.Forms.Button buttonPeriodicTable;
    private System.Windows.Forms.CheckBox checkBoxSearchReference;
    private System.Windows.Forms.TextBox textBoxSearchReference;
    private System.Windows.Forms.CheckBox checkBoxSearchCrystalSystem;
    public System.Windows.Forms.ComboBox comboBoxSearchCrystalSystem;
    private System.Windows.Forms.CheckBox checkBoxSearchCellParameter;
    private System.Windows.Forms.GroupBox groupBoxCellParameter;
    private NumericBox numericBoxCellGamma;
    private NumericBox numericBoxCellAngleErr;
    private NumericBox numericBoxCellLengthErr;
    private NumericBox numericBoxCellC;
    private NumericBox numericBoxCellBeta;
    private NumericBox numericBoxCellAlpha;
    private NumericBox numericBoxCellB;
    private NumericBox numericBoxCellA;
    private System.Windows.Forms.CheckBox checkBoxDspacing;
    private System.Windows.Forms.GroupBox groupBoxDspacing;
    private System.Windows.Forms.CheckBox checkBoxD3;
    private NumericBox numericBoxD3Err;
    private NumericBox numericBoxD2Err;
    private NumericBox numericBoxD1Err;
    private System.Windows.Forms.CheckBox checkBoxD2;
    private System.Windows.Forms.CheckBox checkBoxD1;
    private NumericBox numericBoxD3;
    private NumericBox numericBoxD2;
    private NumericBox numericBoxD1;
    private System.Windows.Forms.CheckBox checkBoxDensity;
    private System.Windows.Forms.GroupBox groupBoxDensity;
    private NumericBox numericBoxDensity;
    private NumericBox numericBoxDensityErr;
    private System.Windows.Forms.CheckBox checkBoxIgnoreScatteringFactor;
    private System.ComponentModel.BackgroundWorker backgroundWorkerSearch;
    public System.Windows.Forms.Button buttonSearch;
}
