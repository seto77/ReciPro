namespace Crystallography.Controls;

partial class SearchCrystalControl
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchCrystalControl));
        flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
        checkBoxSearchName = new System.Windows.Forms.CheckBox();
        textBoxSearchName = new System.Windows.Forms.TextBox();
        checkBoxSearchElements = new System.Windows.Forms.CheckBox();
        buttonPeriodicTable = new System.Windows.Forms.Button();
        checkBoxSearchRefference = new System.Windows.Forms.CheckBox();
        textBoxSearchRefference = new System.Windows.Forms.TextBox();
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
        flowLayoutPanel1.Controls.Add(checkBoxSearchRefference);
        flowLayoutPanel1.Controls.Add(textBoxSearchRefference);
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
        checkBoxSearchName.Name = "checkBoxSearchName";
        checkBoxSearchName.UseVisualStyleBackColor = true;
        checkBoxSearchName.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // textBoxSearchName
        // 
        resources.ApplyResources(textBoxSearchName, "textBoxSearchName");
        textBoxSearchName.Name = "textBoxSearchName";
        textBoxSearchName.KeyDown += textBoxSearchName_KeyDown;
        // 
        // checkBoxSearchElements
        // 
        resources.ApplyResources(checkBoxSearchElements, "checkBoxSearchElements");
        checkBoxSearchElements.Name = "checkBoxSearchElements";
        checkBoxSearchElements.UseVisualStyleBackColor = true;
        checkBoxSearchElements.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // buttonPeriodicTable
        // 
        resources.ApplyResources(buttonPeriodicTable, "buttonPeriodicTable");
        buttonPeriodicTable.Name = "buttonPeriodicTable";
        buttonPeriodicTable.UseVisualStyleBackColor = true;
        buttonPeriodicTable.Click += buttonPeriodicTable_Click;
        // 
        // checkBoxSearchRefference
        // 
        resources.ApplyResources(checkBoxSearchRefference, "checkBoxSearchRefference");
        checkBoxSearchRefference.Name = "checkBoxSearchRefference";
        checkBoxSearchRefference.UseVisualStyleBackColor = true;
        checkBoxSearchRefference.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // textBoxSearchRefference
        // 
        resources.ApplyResources(textBoxSearchRefference, "textBoxSearchRefference");
        textBoxSearchRefference.Name = "textBoxSearchRefference";
        textBoxSearchRefference.KeyDown += textBoxSearchName_KeyDown;
        // 
        // checkBoxSearchCrystalSystem
        // 
        resources.ApplyResources(checkBoxSearchCrystalSystem, "checkBoxSearchCrystalSystem");
        checkBoxSearchCrystalSystem.Name = "checkBoxSearchCrystalSystem";
        checkBoxSearchCrystalSystem.UseVisualStyleBackColor = true;
        checkBoxSearchCrystalSystem.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // comboBoxSearchCrystalSystem
        // 
        comboBoxSearchCrystalSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        resources.ApplyResources(comboBoxSearchCrystalSystem, "comboBoxSearchCrystalSystem");
        comboBoxSearchCrystalSystem.Items.AddRange(new object[] { resources.GetString("comboBoxSearchCrystalSystem.Items"), resources.GetString("comboBoxSearchCrystalSystem.Items1"), resources.GetString("comboBoxSearchCrystalSystem.Items2"), resources.GetString("comboBoxSearchCrystalSystem.Items3"), resources.GetString("comboBoxSearchCrystalSystem.Items4"), resources.GetString("comboBoxSearchCrystalSystem.Items5"), resources.GetString("comboBoxSearchCrystalSystem.Items6"), resources.GetString("comboBoxSearchCrystalSystem.Items7") });
        comboBoxSearchCrystalSystem.Name = "comboBoxSearchCrystalSystem";
        // 
        // checkBoxSearchCellParameter
        // 
        resources.ApplyResources(checkBoxSearchCellParameter, "checkBoxSearchCellParameter");
        checkBoxSearchCellParameter.Name = "checkBoxSearchCellParameter";
        checkBoxSearchCellParameter.UseVisualStyleBackColor = true;
        checkBoxSearchCellParameter.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // groupBoxCellParameter
        // 
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
        numericBoxCellGamma.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellGamma.Maximum = 179D;
        numericBoxCellGamma.Minimum = 0D;
        numericBoxCellGamma.Name = "numericBoxCellGamma";
        numericBoxCellGamma.SkipEventDuringInput = false;
        numericBoxCellGamma.SmartIncrement = true;
        numericBoxCellGamma.ThonsandsSeparator = true;
        numericBoxCellGamma.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellAngleErr
        // 
        resources.ApplyResources(numericBoxCellAngleErr, "numericBoxCellAngleErr");
        numericBoxCellAngleErr.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellAngleErr.DecimalPlaces = 1;
        numericBoxCellAngleErr.Maximum = 50D;
        numericBoxCellAngleErr.Minimum = 0D;
        numericBoxCellAngleErr.Name = "numericBoxCellAngleErr";
        numericBoxCellAngleErr.RadianValue = 0.052359877559829883D;
        numericBoxCellAngleErr.ShowUpDown = true;
        numericBoxCellAngleErr.SkipEventDuringInput = false;
        numericBoxCellAngleErr.ThonsandsSeparator = true;
        numericBoxCellAngleErr.UpDown_Increment = 0.5D;
        numericBoxCellAngleErr.Value = 3D;
        numericBoxCellAngleErr.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellLengthErr
        // 
        resources.ApplyResources(numericBoxCellLengthErr, "numericBoxCellLengthErr");
        numericBoxCellLengthErr.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellLengthErr.DecimalPlaces = 1;
        numericBoxCellLengthErr.Maximum = 50D;
        numericBoxCellLengthErr.Minimum = 0D;
        numericBoxCellLengthErr.Name = "numericBoxCellLengthErr";
        numericBoxCellLengthErr.RadianValue = 0.052359877559829883D;
        numericBoxCellLengthErr.ShowUpDown = true;
        numericBoxCellLengthErr.SkipEventDuringInput = false;
        numericBoxCellLengthErr.ThonsandsSeparator = true;
        numericBoxCellLengthErr.UpDown_Increment = 0.5D;
        numericBoxCellLengthErr.Value = 3D;
        numericBoxCellLengthErr.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellC
        // 
        resources.ApplyResources(numericBoxCellC, "numericBoxCellC");
        numericBoxCellC.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellC.Maximum = 100D;
        numericBoxCellC.Minimum = 0D;
        numericBoxCellC.Name = "numericBoxCellC";
        numericBoxCellC.SkipEventDuringInput = false;
        numericBoxCellC.SmartIncrement = true;
        numericBoxCellC.ThonsandsSeparator = true;
        numericBoxCellC.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellBeta
        // 
        resources.ApplyResources(numericBoxCellBeta, "numericBoxCellBeta");
        numericBoxCellBeta.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellBeta.Maximum = 179D;
        numericBoxCellBeta.Minimum = 0D;
        numericBoxCellBeta.Name = "numericBoxCellBeta";
        numericBoxCellBeta.SkipEventDuringInput = false;
        numericBoxCellBeta.SmartIncrement = true;
        numericBoxCellBeta.ThonsandsSeparator = true;
        numericBoxCellBeta.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellAlpha
        // 
        resources.ApplyResources(numericBoxCellAlpha, "numericBoxCellAlpha");
        numericBoxCellAlpha.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellAlpha.Maximum = 179D;
        numericBoxCellAlpha.Minimum = 0D;
        numericBoxCellAlpha.Name = "numericBoxCellAlpha";
        numericBoxCellAlpha.SkipEventDuringInput = false;
        numericBoxCellAlpha.SmartIncrement = true;
        numericBoxCellAlpha.ThonsandsSeparator = true;
        numericBoxCellAlpha.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellB
        // 
        resources.ApplyResources(numericBoxCellB, "numericBoxCellB");
        numericBoxCellB.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellB.Maximum = 100D;
        numericBoxCellB.Minimum = 0D;
        numericBoxCellB.Name = "numericBoxCellB";
        numericBoxCellB.SkipEventDuringInput = false;
        numericBoxCellB.SmartIncrement = true;
        numericBoxCellB.ThonsandsSeparator = true;
        numericBoxCellB.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxCellA
        // 
        resources.ApplyResources(numericBoxCellA, "numericBoxCellA");
        numericBoxCellA.BackColor = System.Drawing.Color.Transparent;
        numericBoxCellA.Maximum = 100D;
        numericBoxCellA.Minimum = 0D;
        numericBoxCellA.Name = "numericBoxCellA";
        numericBoxCellA.SkipEventDuringInput = false;
        numericBoxCellA.SmartIncrement = true;
        numericBoxCellA.ThonsandsSeparator = true;
        numericBoxCellA.KeyDown += textBoxSearchName_KeyDown;
        // 
        // checkBoxDspacing
        // 
        resources.ApplyResources(checkBoxDspacing, "checkBoxDspacing");
        checkBoxDspacing.Name = "checkBoxDspacing";
        checkBoxDspacing.UseVisualStyleBackColor = true;
        checkBoxDspacing.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // groupBoxDspacing
        // 
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
        checkBoxD3.Name = "checkBoxD3";
        checkBoxD3.UseVisualStyleBackColor = true;
        checkBoxD3.CheckedChanged += checkBoxD3_CheckedChanged;
        // 
        // numericBoxD3Err
        // 
        resources.ApplyResources(numericBoxD3Err, "numericBoxD3Err");
        numericBoxD3Err.BackColor = System.Drawing.Color.Transparent;
        numericBoxD3Err.DecimalPlaces = 1;
        numericBoxD3Err.Maximum = 50D;
        numericBoxD3Err.Minimum = 0D;
        numericBoxD3Err.Name = "numericBoxD3Err";
        numericBoxD3Err.RadianValue = 0.052359877559829883D;
        numericBoxD3Err.ShowUpDown = true;
        numericBoxD3Err.SkipEventDuringInput = false;
        numericBoxD3Err.ThonsandsSeparator = true;
        numericBoxD3Err.UpDown_Increment = 0.5D;
        numericBoxD3Err.Value = 3D;
        numericBoxD3Err.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxD2Err
        // 
        resources.ApplyResources(numericBoxD2Err, "numericBoxD2Err");
        numericBoxD2Err.BackColor = System.Drawing.Color.Transparent;
        numericBoxD2Err.DecimalPlaces = 1;
        numericBoxD2Err.Maximum = 50D;
        numericBoxD2Err.Minimum = 0D;
        numericBoxD2Err.Name = "numericBoxD2Err";
        numericBoxD2Err.RadianValue = 0.052359877559829883D;
        numericBoxD2Err.ShowUpDown = true;
        numericBoxD2Err.SkipEventDuringInput = false;
        numericBoxD2Err.ThonsandsSeparator = true;
        numericBoxD2Err.UpDown_Increment = 0.5D;
        numericBoxD2Err.Value = 3D;
        numericBoxD2Err.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxD1Err
        // 
        resources.ApplyResources(numericBoxD1Err, "numericBoxD1Err");
        numericBoxD1Err.BackColor = System.Drawing.Color.Transparent;
        numericBoxD1Err.DecimalPlaces = 1;
        numericBoxD1Err.Maximum = 50D;
        numericBoxD1Err.Minimum = 0D;
        numericBoxD1Err.Name = "numericBoxD1Err";
        numericBoxD1Err.RadianValue = 0.052359877559829883D;
        numericBoxD1Err.ShowUpDown = true;
        numericBoxD1Err.SkipEventDuringInput = false;
        numericBoxD1Err.ThonsandsSeparator = true;
        numericBoxD1Err.UpDown_Increment = 0.5D;
        numericBoxD1Err.Value = 3D;
        numericBoxD1Err.KeyDown += textBoxSearchName_KeyDown;
        // 
        // checkBoxD2
        // 
        resources.ApplyResources(checkBoxD2, "checkBoxD2");
        checkBoxD2.Name = "checkBoxD2";
        checkBoxD2.UseVisualStyleBackColor = true;
        checkBoxD2.CheckedChanged += checkBoxD2_CheckedChanged;
        // 
        // checkBoxD1
        // 
        resources.ApplyResources(checkBoxD1, "checkBoxD1");
        checkBoxD1.Checked = true;
        checkBoxD1.CheckState = System.Windows.Forms.CheckState.Checked;
        checkBoxD1.Name = "checkBoxD1";
        checkBoxD1.UseVisualStyleBackColor = true;
        checkBoxD1.CheckedChanged += checkBoxD1_CheckedChanged;
        // 
        // numericBoxD3
        // 
        resources.ApplyResources(numericBoxD3, "numericBoxD3");
        numericBoxD3.BackColor = System.Drawing.Color.Transparent;
        numericBoxD3.DecimalPlaces = 2;
        numericBoxD3.Maximum = 100D;
        numericBoxD3.Minimum = 0D;
        numericBoxD3.Name = "numericBoxD3";
        numericBoxD3.SkipEventDuringInput = false;
        numericBoxD3.SmartIncrement = true;
        numericBoxD3.ThonsandsSeparator = true;
        numericBoxD3.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxD2
        // 
        resources.ApplyResources(numericBoxD2, "numericBoxD2");
        numericBoxD2.BackColor = System.Drawing.Color.Transparent;
        numericBoxD2.DecimalPlaces = 2;
        numericBoxD2.Maximum = 100D;
        numericBoxD2.Minimum = 0D;
        numericBoxD2.Name = "numericBoxD2";
        numericBoxD2.SkipEventDuringInput = false;
        numericBoxD2.SmartIncrement = true;
        numericBoxD2.ThonsandsSeparator = true;
        numericBoxD2.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxD1
        // 
        resources.ApplyResources(numericBoxD1, "numericBoxD1");
        numericBoxD1.BackColor = System.Drawing.Color.Transparent;
        numericBoxD1.DecimalPlaces = 2;
        numericBoxD1.Maximum = 100D;
        numericBoxD1.Minimum = 0D;
        numericBoxD1.Name = "numericBoxD1";
        numericBoxD1.SkipEventDuringInput = false;
        numericBoxD1.SmartIncrement = true;
        numericBoxD1.ThonsandsSeparator = true;
        numericBoxD1.KeyDown += textBoxSearchName_KeyDown;
        // 
        // checkBoxIgnoreScatteringFactor
        // 
        resources.ApplyResources(checkBoxIgnoreScatteringFactor, "checkBoxIgnoreScatteringFactor");
        checkBoxIgnoreScatteringFactor.Name = "checkBoxIgnoreScatteringFactor";
        checkBoxIgnoreScatteringFactor.UseVisualStyleBackColor = true;
        // 
        // checkBoxDensity
        // 
        resources.ApplyResources(checkBoxDensity, "checkBoxDensity");
        checkBoxDensity.Name = "checkBoxDensity";
        checkBoxDensity.UseVisualStyleBackColor = true;
        checkBoxDensity.CheckedChanged += checkBoxSearch_CheckedChanged;
        // 
        // groupBoxDensity
        // 
        groupBoxDensity.Controls.Add(numericBoxDensity);
        groupBoxDensity.Controls.Add(numericBoxDensityErr);
        resources.ApplyResources(groupBoxDensity, "groupBoxDensity");
        groupBoxDensity.Name = "groupBoxDensity";
        groupBoxDensity.TabStop = false;
        // 
        // numericBoxDensity
        // 
        resources.ApplyResources(numericBoxDensity, "numericBoxDensity");
        numericBoxDensity.BackColor = System.Drawing.Color.Transparent;
        numericBoxDensity.DecimalPlaces = 3;
        numericBoxDensity.Maximum = 100D;
        numericBoxDensity.Minimum = 0D;
        numericBoxDensity.Name = "numericBoxDensity";
        numericBoxDensity.SkipEventDuringInput = false;
        numericBoxDensity.SmartIncrement = true;
        numericBoxDensity.ThonsandsSeparator = true;
        numericBoxDensity.KeyDown += textBoxSearchName_KeyDown;
        // 
        // numericBoxDensityErr
        // 
        resources.ApplyResources(numericBoxDensityErr, "numericBoxDensityErr");
        numericBoxDensityErr.BackColor = System.Drawing.Color.Transparent;
        numericBoxDensityErr.DecimalPlaces = 1;
        numericBoxDensityErr.Maximum = 50D;
        numericBoxDensityErr.Minimum = 0D;
        numericBoxDensityErr.Name = "numericBoxDensityErr";
        numericBoxDensityErr.RadianValue = 0.052359877559829883D;
        numericBoxDensityErr.ShowUpDown = true;
        numericBoxDensityErr.SkipEventDuringInput = false;
        numericBoxDensityErr.ThonsandsSeparator = true;
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
        buttonSearch.BackColor = System.Drawing.Color.Chocolate;
        buttonSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
        buttonSearch.Name = "buttonSearch";
        buttonSearch.UseVisualStyleBackColor = false;
        buttonSearch.Click += buttonSearch_Click;
        // 
        // SearchCrystalControl
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
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

    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.CheckBox checkBoxSearchName;
    private System.Windows.Forms.TextBox textBoxSearchName;
    private System.Windows.Forms.CheckBox checkBoxSearchElements;
    private System.Windows.Forms.Button buttonPeriodicTable;
    private System.Windows.Forms.CheckBox checkBoxSearchRefference;
    private System.Windows.Forms.TextBox textBoxSearchRefference;
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
