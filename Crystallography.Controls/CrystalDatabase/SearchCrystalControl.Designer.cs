namespace Crystallography.Controls
{
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxSearchName = new System.Windows.Forms.CheckBox();
            this.textBoxSearchName = new System.Windows.Forms.TextBox();
            this.checkBoxSearchElements = new System.Windows.Forms.CheckBox();
            this.buttonPeriodicTable = new System.Windows.Forms.Button();
            this.checkBoxSearchRefference = new System.Windows.Forms.CheckBox();
            this.textBoxSearchRefference = new System.Windows.Forms.TextBox();
            this.checkBoxSearchCrystalSystem = new System.Windows.Forms.CheckBox();
            this.comboBoxSearchCrystalSystem = new System.Windows.Forms.ComboBox();
            this.checkBoxSearchCellParameter = new System.Windows.Forms.CheckBox();
            this.groupBoxCellParameter = new System.Windows.Forms.GroupBox();
            this.numericBoxCellGamma = new Crystallography.Controls.NumericBox();
            this.numericBoxCellAngleErr = new Crystallography.Controls.NumericBox();
            this.numericBoxCellLengthErr = new Crystallography.Controls.NumericBox();
            this.numericBoxCellC = new Crystallography.Controls.NumericBox();
            this.numericBoxCellBeta = new Crystallography.Controls.NumericBox();
            this.numericBoxCellAlpha = new Crystallography.Controls.NumericBox();
            this.numericBoxCellB = new Crystallography.Controls.NumericBox();
            this.numericBoxCellA = new Crystallography.Controls.NumericBox();
            this.checkBoxDspacing = new System.Windows.Forms.CheckBox();
            this.groupBoxDspacing = new System.Windows.Forms.GroupBox();
            this.checkBoxD3 = new System.Windows.Forms.CheckBox();
            this.numericBoxD3Err = new Crystallography.Controls.NumericBox();
            this.numericBoxD2Err = new Crystallography.Controls.NumericBox();
            this.numericBoxD1Err = new Crystallography.Controls.NumericBox();
            this.checkBoxD2 = new System.Windows.Forms.CheckBox();
            this.checkBoxD1 = new System.Windows.Forms.CheckBox();
            this.numericBoxD3 = new Crystallography.Controls.NumericBox();
            this.numericBoxD2 = new Crystallography.Controls.NumericBox();
            this.numericBoxD1 = new Crystallography.Controls.NumericBox();
            this.checkBoxIgnoreScatteringFactor = new System.Windows.Forms.CheckBox();
            this.checkBoxDensity = new System.Windows.Forms.CheckBox();
            this.groupBoxDensity = new System.Windows.Forms.GroupBox();
            this.numericBoxDensity = new Crystallography.Controls.NumericBox();
            this.numericBoxDensityErr = new Crystallography.Controls.NumericBox();
            this.backgroundWorkerSearch = new System.ComponentModel.BackgroundWorker();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxCellParameter.SuspendLayout();
            this.groupBoxDspacing.SuspendLayout();
            this.groupBoxDensity.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSearchName);
            this.flowLayoutPanel1.Controls.Add(this.textBoxSearchName);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSearchElements);
            this.flowLayoutPanel1.Controls.Add(this.buttonPeriodicTable);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSearchRefference);
            this.flowLayoutPanel1.Controls.Add(this.textBoxSearchRefference);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSearchCrystalSystem);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxSearchCrystalSystem);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxSearchCellParameter);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxCellParameter);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxDspacing);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxDspacing);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxDensity);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxDensity);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // checkBoxSearchName
            // 
            resources.ApplyResources(this.checkBoxSearchName, "checkBoxSearchName");
            this.checkBoxSearchName.Name = "checkBoxSearchName";
            this.checkBoxSearchName.UseVisualStyleBackColor = true;
            this.checkBoxSearchName.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // textBoxSearchName
            // 
            resources.ApplyResources(this.textBoxSearchName, "textBoxSearchName");
            this.textBoxSearchName.Name = "textBoxSearchName";
            this.textBoxSearchName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearchName_KeyDown);
            // 
            // checkBoxSearchElements
            // 
            resources.ApplyResources(this.checkBoxSearchElements, "checkBoxSearchElements");
            this.checkBoxSearchElements.Name = "checkBoxSearchElements";
            this.checkBoxSearchElements.UseVisualStyleBackColor = true;
            this.checkBoxSearchElements.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // buttonPeriodicTable
            // 
            resources.ApplyResources(this.buttonPeriodicTable, "buttonPeriodicTable");
            this.buttonPeriodicTable.Name = "buttonPeriodicTable";
            this.buttonPeriodicTable.UseVisualStyleBackColor = true;
            this.buttonPeriodicTable.Click += new System.EventHandler(this.buttonPeriodicTable_Click);
            // 
            // checkBoxSearchRefference
            // 
            resources.ApplyResources(this.checkBoxSearchRefference, "checkBoxSearchRefference");
            this.checkBoxSearchRefference.Name = "checkBoxSearchRefference";
            this.checkBoxSearchRefference.UseVisualStyleBackColor = true;
            this.checkBoxSearchRefference.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // textBoxSearchRefference
            // 
            resources.ApplyResources(this.textBoxSearchRefference, "textBoxSearchRefference");
            this.textBoxSearchRefference.Name = "textBoxSearchRefference";
            this.textBoxSearchRefference.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearchName_KeyDown);
            // 
            // checkBoxSearchCrystalSystem
            // 
            resources.ApplyResources(this.checkBoxSearchCrystalSystem, "checkBoxSearchCrystalSystem");
            this.checkBoxSearchCrystalSystem.Name = "checkBoxSearchCrystalSystem";
            this.checkBoxSearchCrystalSystem.UseVisualStyleBackColor = true;
            this.checkBoxSearchCrystalSystem.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // comboBoxSearchCrystalSystem
            // 
            resources.ApplyResources(this.comboBoxSearchCrystalSystem, "comboBoxSearchCrystalSystem");
            this.comboBoxSearchCrystalSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchCrystalSystem.Items.AddRange(new object[] {
            resources.GetString("comboBoxSearchCrystalSystem.Items"),
            resources.GetString("comboBoxSearchCrystalSystem.Items1"),
            resources.GetString("comboBoxSearchCrystalSystem.Items2"),
            resources.GetString("comboBoxSearchCrystalSystem.Items3"),
            resources.GetString("comboBoxSearchCrystalSystem.Items4"),
            resources.GetString("comboBoxSearchCrystalSystem.Items5"),
            resources.GetString("comboBoxSearchCrystalSystem.Items6"),
            resources.GetString("comboBoxSearchCrystalSystem.Items7")});
            this.comboBoxSearchCrystalSystem.Name = "comboBoxSearchCrystalSystem";
            // 
            // checkBoxSearchCellParameter
            // 
            resources.ApplyResources(this.checkBoxSearchCellParameter, "checkBoxSearchCellParameter");
            this.checkBoxSearchCellParameter.Name = "checkBoxSearchCellParameter";
            this.checkBoxSearchCellParameter.UseVisualStyleBackColor = true;
            this.checkBoxSearchCellParameter.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // groupBoxCellParameter
            // 
            resources.ApplyResources(this.groupBoxCellParameter, "groupBoxCellParameter");
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellGamma);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellAngleErr);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellLengthErr);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellC);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellBeta);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellAlpha);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellB);
            this.groupBoxCellParameter.Controls.Add(this.numericBoxCellA);
            this.groupBoxCellParameter.Name = "groupBoxCellParameter";
            this.groupBoxCellParameter.TabStop = false;
            // 
            // numericBoxCellGamma
            // 
            resources.ApplyResources(this.numericBoxCellGamma, "numericBoxCellGamma");
            this.numericBoxCellGamma.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellGamma.Maximum = 179D;
            this.numericBoxCellGamma.Minimum = 0D;
            this.numericBoxCellGamma.Name = "numericBoxCellGamma";
            this.numericBoxCellGamma.RoundErrorAccuracy = -1;
            this.numericBoxCellGamma.SkipEventDuringInput = false;
            this.numericBoxCellGamma.SmartIncrement = true;
            this.numericBoxCellGamma.ThonsandsSeparator = true;
            // 
            // numericBoxCellAngleErr
            // 
            resources.ApplyResources(this.numericBoxCellAngleErr, "numericBoxCellAngleErr");
            this.numericBoxCellAngleErr.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellAngleErr.DecimalPlaces = 1;
            this.numericBoxCellAngleErr.Maximum = 50D;
            this.numericBoxCellAngleErr.Minimum = 0D;
            this.numericBoxCellAngleErr.Name = "numericBoxCellAngleErr";
            this.numericBoxCellAngleErr.RadianValue = 0.052359877559829883D;
            this.numericBoxCellAngleErr.RoundErrorAccuracy = -1;
            this.numericBoxCellAngleErr.ShowUpDown = true;
            this.numericBoxCellAngleErr.SkipEventDuringInput = false;
            this.numericBoxCellAngleErr.ThonsandsSeparator = true;
            this.numericBoxCellAngleErr.UpDown_Increment = 0.5D;
            this.numericBoxCellAngleErr.Value = 3D;
            // 
            // numericBoxCellLengthErr
            // 
            resources.ApplyResources(this.numericBoxCellLengthErr, "numericBoxCellLengthErr");
            this.numericBoxCellLengthErr.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellLengthErr.DecimalPlaces = 1;
            this.numericBoxCellLengthErr.Maximum = 50D;
            this.numericBoxCellLengthErr.Minimum = 0D;
            this.numericBoxCellLengthErr.Name = "numericBoxCellLengthErr";
            this.numericBoxCellLengthErr.RadianValue = 0.052359877559829883D;
            this.numericBoxCellLengthErr.RoundErrorAccuracy = -1;
            this.numericBoxCellLengthErr.ShowUpDown = true;
            this.numericBoxCellLengthErr.SkipEventDuringInput = false;
            this.numericBoxCellLengthErr.ThonsandsSeparator = true;
            this.numericBoxCellLengthErr.UpDown_Increment = 0.5D;
            this.numericBoxCellLengthErr.Value = 3D;
            // 
            // numericBoxCellC
            // 
            resources.ApplyResources(this.numericBoxCellC, "numericBoxCellC");
            this.numericBoxCellC.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellC.Maximum = 100D;
            this.numericBoxCellC.Minimum = 0D;
            this.numericBoxCellC.Name = "numericBoxCellC";
            this.numericBoxCellC.RoundErrorAccuracy = -1;
            this.numericBoxCellC.SkipEventDuringInput = false;
            this.numericBoxCellC.SmartIncrement = true;
            this.numericBoxCellC.ThonsandsSeparator = true;
            // 
            // numericBoxCellBeta
            // 
            resources.ApplyResources(this.numericBoxCellBeta, "numericBoxCellBeta");
            this.numericBoxCellBeta.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellBeta.Maximum = 179D;
            this.numericBoxCellBeta.Minimum = 0D;
            this.numericBoxCellBeta.Name = "numericBoxCellBeta";
            this.numericBoxCellBeta.RoundErrorAccuracy = -1;
            this.numericBoxCellBeta.SkipEventDuringInput = false;
            this.numericBoxCellBeta.SmartIncrement = true;
            this.numericBoxCellBeta.ThonsandsSeparator = true;
            // 
            // numericBoxCellAlpha
            // 
            resources.ApplyResources(this.numericBoxCellAlpha, "numericBoxCellAlpha");
            this.numericBoxCellAlpha.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellAlpha.Maximum = 179D;
            this.numericBoxCellAlpha.Minimum = 0D;
            this.numericBoxCellAlpha.Name = "numericBoxCellAlpha";
            this.numericBoxCellAlpha.RoundErrorAccuracy = -1;
            this.numericBoxCellAlpha.SkipEventDuringInput = false;
            this.numericBoxCellAlpha.SmartIncrement = true;
            this.numericBoxCellAlpha.ThonsandsSeparator = true;
            // 
            // numericBoxCellB
            // 
            resources.ApplyResources(this.numericBoxCellB, "numericBoxCellB");
            this.numericBoxCellB.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellB.Maximum = 100D;
            this.numericBoxCellB.Minimum = 0D;
            this.numericBoxCellB.Name = "numericBoxCellB";
            this.numericBoxCellB.RoundErrorAccuracy = -1;
            this.numericBoxCellB.SkipEventDuringInput = false;
            this.numericBoxCellB.SmartIncrement = true;
            this.numericBoxCellB.ThonsandsSeparator = true;
            // 
            // numericBoxCellA
            // 
            resources.ApplyResources(this.numericBoxCellA, "numericBoxCellA");
            this.numericBoxCellA.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellA.Maximum = 100D;
            this.numericBoxCellA.Minimum = 0D;
            this.numericBoxCellA.Name = "numericBoxCellA";
            this.numericBoxCellA.RoundErrorAccuracy = -1;
            this.numericBoxCellA.SkipEventDuringInput = false;
            this.numericBoxCellA.SmartIncrement = true;
            this.numericBoxCellA.ThonsandsSeparator = true;
            // 
            // checkBoxDspacing
            // 
            resources.ApplyResources(this.checkBoxDspacing, "checkBoxDspacing");
            this.checkBoxDspacing.Name = "checkBoxDspacing";
            this.checkBoxDspacing.UseVisualStyleBackColor = true;
            this.checkBoxDspacing.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // groupBoxDspacing
            // 
            resources.ApplyResources(this.groupBoxDspacing, "groupBoxDspacing");
            this.groupBoxDspacing.Controls.Add(this.checkBoxD3);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD3Err);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD2Err);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD1Err);
            this.groupBoxDspacing.Controls.Add(this.checkBoxD2);
            this.groupBoxDspacing.Controls.Add(this.checkBoxD1);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD3);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD2);
            this.groupBoxDspacing.Controls.Add(this.numericBoxD1);
            this.groupBoxDspacing.Controls.Add(this.checkBoxIgnoreScatteringFactor);
            this.groupBoxDspacing.Name = "groupBoxDspacing";
            this.groupBoxDspacing.TabStop = false;
            // 
            // checkBoxD3
            // 
            resources.ApplyResources(this.checkBoxD3, "checkBoxD3");
            this.checkBoxD3.Name = "checkBoxD3";
            this.checkBoxD3.UseVisualStyleBackColor = true;
            this.checkBoxD3.CheckedChanged += new System.EventHandler(this.checkBoxD3_CheckedChanged);
            // 
            // numericBoxD3Err
            // 
            resources.ApplyResources(this.numericBoxD3Err, "numericBoxD3Err");
            this.numericBoxD3Err.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD3Err.DecimalPlaces = 1;
            this.numericBoxD3Err.Maximum = 50D;
            this.numericBoxD3Err.Minimum = 0D;
            this.numericBoxD3Err.Name = "numericBoxD3Err";
            this.numericBoxD3Err.RadianValue = 0.052359877559829883D;
            this.numericBoxD3Err.RoundErrorAccuracy = -1;
            this.numericBoxD3Err.ShowUpDown = true;
            this.numericBoxD3Err.SkipEventDuringInput = false;
            this.numericBoxD3Err.ThonsandsSeparator = true;
            this.numericBoxD3Err.UpDown_Increment = 0.5D;
            this.numericBoxD3Err.Value = 3D;
            // 
            // numericBoxD2Err
            // 
            resources.ApplyResources(this.numericBoxD2Err, "numericBoxD2Err");
            this.numericBoxD2Err.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD2Err.DecimalPlaces = 1;
            this.numericBoxD2Err.Maximum = 50D;
            this.numericBoxD2Err.Minimum = 0D;
            this.numericBoxD2Err.Name = "numericBoxD2Err";
            this.numericBoxD2Err.RadianValue = 0.052359877559829883D;
            this.numericBoxD2Err.RoundErrorAccuracy = -1;
            this.numericBoxD2Err.ShowUpDown = true;
            this.numericBoxD2Err.SkipEventDuringInput = false;
            this.numericBoxD2Err.ThonsandsSeparator = true;
            this.numericBoxD2Err.UpDown_Increment = 0.5D;
            this.numericBoxD2Err.Value = 3D;
            // 
            // numericBoxD1Err
            // 
            resources.ApplyResources(this.numericBoxD1Err, "numericBoxD1Err");
            this.numericBoxD1Err.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD1Err.DecimalPlaces = 1;
            this.numericBoxD1Err.Maximum = 50D;
            this.numericBoxD1Err.Minimum = 0D;
            this.numericBoxD1Err.Name = "numericBoxD1Err";
            this.numericBoxD1Err.RadianValue = 0.052359877559829883D;
            this.numericBoxD1Err.RoundErrorAccuracy = -1;
            this.numericBoxD1Err.ShowUpDown = true;
            this.numericBoxD1Err.SkipEventDuringInput = false;
            this.numericBoxD1Err.ThonsandsSeparator = true;
            this.numericBoxD1Err.UpDown_Increment = 0.5D;
            this.numericBoxD1Err.Value = 3D;
            // 
            // checkBoxD2
            // 
            resources.ApplyResources(this.checkBoxD2, "checkBoxD2");
            this.checkBoxD2.Name = "checkBoxD2";
            this.checkBoxD2.UseVisualStyleBackColor = true;
            this.checkBoxD2.CheckedChanged += new System.EventHandler(this.checkBoxD2_CheckedChanged);
            // 
            // checkBoxD1
            // 
            resources.ApplyResources(this.checkBoxD1, "checkBoxD1");
            this.checkBoxD1.Checked = true;
            this.checkBoxD1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxD1.Name = "checkBoxD1";
            this.checkBoxD1.UseVisualStyleBackColor = true;
            this.checkBoxD1.CheckedChanged += new System.EventHandler(this.checkBoxD1_CheckedChanged);
            // 
            // numericBoxD3
            // 
            resources.ApplyResources(this.numericBoxD3, "numericBoxD3");
            this.numericBoxD3.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD3.DecimalPlaces = 2;
            this.numericBoxD3.Maximum = 100D;
            this.numericBoxD3.Minimum = 0D;
            this.numericBoxD3.Name = "numericBoxD3";
            this.numericBoxD3.RoundErrorAccuracy = -1;
            this.numericBoxD3.SkipEventDuringInput = false;
            this.numericBoxD3.SmartIncrement = true;
            this.numericBoxD3.ThonsandsSeparator = true;
            // 
            // numericBoxD2
            // 
            resources.ApplyResources(this.numericBoxD2, "numericBoxD2");
            this.numericBoxD2.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD2.DecimalPlaces = 2;
            this.numericBoxD2.Maximum = 100D;
            this.numericBoxD2.Minimum = 0D;
            this.numericBoxD2.Name = "numericBoxD2";
            this.numericBoxD2.RoundErrorAccuracy = -1;
            this.numericBoxD2.SkipEventDuringInput = false;
            this.numericBoxD2.SmartIncrement = true;
            this.numericBoxD2.ThonsandsSeparator = true;
            // 
            // numericBoxD1
            // 
            resources.ApplyResources(this.numericBoxD1, "numericBoxD1");
            this.numericBoxD1.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxD1.DecimalPlaces = 2;
            this.numericBoxD1.Maximum = 100D;
            this.numericBoxD1.Minimum = 0D;
            this.numericBoxD1.Name = "numericBoxD1";
            this.numericBoxD1.RoundErrorAccuracy = -1;
            this.numericBoxD1.SkipEventDuringInput = false;
            this.numericBoxD1.SmartIncrement = true;
            this.numericBoxD1.ThonsandsSeparator = true;
            // 
            // checkBoxIgnoreScatteringFactor
            // 
            resources.ApplyResources(this.checkBoxIgnoreScatteringFactor, "checkBoxIgnoreScatteringFactor");
            this.checkBoxIgnoreScatteringFactor.Name = "checkBoxIgnoreScatteringFactor";
            this.checkBoxIgnoreScatteringFactor.UseVisualStyleBackColor = true;
            // 
            // checkBoxDensity
            // 
            resources.ApplyResources(this.checkBoxDensity, "checkBoxDensity");
            this.checkBoxDensity.Name = "checkBoxDensity";
            this.checkBoxDensity.UseVisualStyleBackColor = true;
            this.checkBoxDensity.CheckedChanged += new System.EventHandler(this.checkBoxSearch_CheckedChanged);
            // 
            // groupBoxDensity
            // 
            resources.ApplyResources(this.groupBoxDensity, "groupBoxDensity");
            this.groupBoxDensity.Controls.Add(this.numericBoxDensity);
            this.groupBoxDensity.Controls.Add(this.numericBoxDensityErr);
            this.groupBoxDensity.Name = "groupBoxDensity";
            this.groupBoxDensity.TabStop = false;
            // 
            // numericBoxDensity
            // 
            resources.ApplyResources(this.numericBoxDensity, "numericBoxDensity");
            this.numericBoxDensity.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxDensity.DecimalPlaces = 3;
            this.numericBoxDensity.Maximum = 100D;
            this.numericBoxDensity.Minimum = 0D;
            this.numericBoxDensity.Name = "numericBoxDensity";
            this.numericBoxDensity.RoundErrorAccuracy = -1;
            this.numericBoxDensity.SkipEventDuringInput = false;
            this.numericBoxDensity.SmartIncrement = true;
            this.numericBoxDensity.ThonsandsSeparator = true;
            // 
            // numericBoxDensityErr
            // 
            resources.ApplyResources(this.numericBoxDensityErr, "numericBoxDensityErr");
            this.numericBoxDensityErr.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxDensityErr.DecimalPlaces = 1;
            this.numericBoxDensityErr.Maximum = 50D;
            this.numericBoxDensityErr.Minimum = 0D;
            this.numericBoxDensityErr.Name = "numericBoxDensityErr";
            this.numericBoxDensityErr.RadianValue = 0.052359877559829883D;
            this.numericBoxDensityErr.RoundErrorAccuracy = -1;
            this.numericBoxDensityErr.ShowUpDown = true;
            this.numericBoxDensityErr.SkipEventDuringInput = false;
            this.numericBoxDensityErr.ThonsandsSeparator = true;
            this.numericBoxDensityErr.UpDown_Increment = 0.5D;
            this.numericBoxDensityErr.Value = 3D;
            // 
            // backgroundWorkerSearch
            // 
            this.backgroundWorkerSearch.WorkerReportsProgress = true;
            this.backgroundWorkerSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSearch_DoWork);
            this.backgroundWorkerSearch.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerSearch_ProgressChanged);
            this.backgroundWorkerSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerSearch_RunWorkerCompleted);
            // 
            // buttonSearch
            // 
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.BackColor = System.Drawing.Color.Chocolate;
            this.buttonSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // SearchCrystalControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.buttonSearch);
            this.Name = "SearchCrystalControl";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBoxCellParameter.ResumeLayout(false);
            this.groupBoxDspacing.ResumeLayout(false);
            this.groupBoxDspacing.PerformLayout();
            this.groupBoxDensity.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
}
