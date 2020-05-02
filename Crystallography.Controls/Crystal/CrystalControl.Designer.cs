namespace Crystallography.Controls
{
    partial class CrystalControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrystalControl));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageBasicInfo = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.numericBoxZnumber = new Crystallography.Controls.NumericBox();
            this.numericBoxVolume = new Crystallography.Controls.NumericBox();
            this.numericBoxDensity = new Crystallography.Controls.NumericBox();
            this.colorControl = new Crystallography.Controls.ColorControl();
            this.symmetryControl = new Crystallography.Controls.SymmetryControl();
            this.tabPageAtom = new System.Windows.Forms.TabPage();
            this.atomControl = new Crystallography.Controls.AtomControl();
            this.panelAtom = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageBondsPolyhedra = new System.Windows.Forms.TabPage();
            this.bondControl = new Crystallography.Controls.BondInputControl();
            this.tabPageReference = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBoxAuthor = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.textBoxJournal = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxMemo = new System.Windows.Forms.TextBox();
            this.tabPageEOS = new System.Windows.Forms.TabPage();
            this.label83 = new System.Windows.Forms.Label();
            this.textBoxEOS_Note = new System.Windows.Forms.TextBox();
            this.checkBoxUseEOS = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericBoxEOS_C = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_B = new Crystallography.Controls.NumericBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.radioButtonTdependenceK0andV0 = new System.Windows.Forms.RadioButton();
            this.numericBoxEOS_A = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_KperT = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_Gamma0 = new Crystallography.Controls.NumericBox();
            this.label76 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label72 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label85 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.label97 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.numericBoxEOS_Theta0 = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_Q = new Crystallography.Controls.NumericBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonMieGruneisen = new System.Windows.Forms.RadioButton();
            this.label98 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label75 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericalTextBoxEOS_V0perMol = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_V0perCell = new Crystallography.Controls.NumericBox();
            this.numericBoxEOS_KT0 = new Crystallography.Controls.NumericBox();
            this.label70 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.numericBoxEOS_KprimeT0 = new Crystallography.Controls.NumericBox();
            this.radioButtonVinet = new System.Windows.Forms.RadioButton();
            this.radioButtonBirchMurnaghan = new System.Windows.Forms.RadioButton();
            this.numericBoxEOS_T0 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxTemperature = new Crystallography.Controls.NumericBox();
            this.numericBoxPressure = new Crystallography.Controls.NumericBox();
            this.tabPageElasticity = new System.Windows.Forms.TabPage();
            this.elasticityControl1 = new Crystallography.Controls.ElasticityControl();
            this.tabPageStrainStress = new System.Windows.Forms.TabPage();
            this.numericalTextBoxHill = new Crystallography.Controls.NumericBox();
            this.label116 = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.label111 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.label106 = new System.Windows.Forms.Label();
            this.label107 = new System.Windows.Forms.Label();
            this.label108 = new System.Windows.Forms.Label();
            this.numericalTextBoxStress33 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStress22 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStress11 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStress23 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStress13 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStress12 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStrain33 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStrain11 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStrain22 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStrain12 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStrain23 = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxStrain13 = new Crystallography.Controls.NumericBox();
            this.tabPagePolycrystalline = new System.Windows.Forms.TabPage();
            this.contextMenuStripPoleFigure = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.poleFigureControl = new Crystallography.Controls.PoleFigureControl();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonGenerateRandomOrientations = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownAngleResolution = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.numericUpDownAngleSubDivision = new System.Windows.Forms.NumericUpDown();
            this.label101 = new System.Windows.Forms.Label();
            this.numericUpDownCrystallineSize = new System.Windows.Forms.NumericUpDown();
            this.label99 = new System.Windows.Forms.Label();
            this.tabPageBounds = new System.Windows.Forms.TabPage();
            this.boundControl = new Crystallography.Controls.BoundControl();
            this.tabPageLatticePlane = new System.Windows.Forms.TabPage();
            this.latticePlaneControl = new Crystallography.Controls.LatticePlaneControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scatteringFactorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symmetryInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.importCrystalFromCIFAMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportThisCrystalAsCIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendThisCrystalToOtherSoftwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearAllDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.revertCellConstantsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.strainControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxFormula = new System.Windows.Forms.TextBox();
            this.label90 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonReset = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabControl.SuspendLayout();
            this.tabPageBasicInfo.SuspendLayout();
            this.panel5.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.tabPageAtom.SuspendLayout();
            this.panelAtom.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tabPageBondsPolyhedra.SuspendLayout();
            this.tabPageReference.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPageEOS.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPageElasticity.SuspendLayout();
            this.tabPageStrainStress.SuspendLayout();
            this.tabPagePolycrystalline.SuspendLayout();
            this.contextMenuStripPoleFigure.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleResolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleSubDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCrystallineSize)).BeginInit();
            this.tabPageBounds.SuspendLayout();
            this.tabPageLatticePlane.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.AllowDrop = true;
            this.tabControl.Controls.Add(this.tabPageBasicInfo);
            this.tabControl.Controls.Add(this.tabPageAtom);
            this.tabControl.Controls.Add(this.tabPageBondsPolyhedra);
            this.tabControl.Controls.Add(this.tabPageReference);
            this.tabControl.Controls.Add(this.tabPageEOS);
            this.tabControl.Controls.Add(this.tabPageElasticity);
            this.tabControl.Controls.Add(this.tabPageStrainStress);
            this.tabControl.Controls.Add(this.tabPagePolycrystalline);
            this.tabControl.Controls.Add(this.tabPageBounds);
            this.tabControl.Controls.Add(this.tabPageLatticePlane);
            this.tabControl.HotTrack = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.toolTip.SetToolTip(this.tabControl, resources.GetString("tabControl.ToolTip"));
            this.tabControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragDrop);
            this.tabControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragEnter);
            // 
            // tabPageBasicInfo
            // 
            resources.ApplyResources(this.tabPageBasicInfo, "tabPageBasicInfo");
            this.tabPageBasicInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageBasicInfo.Controls.Add(this.panel5);
            this.tabPageBasicInfo.Name = "tabPageBasicInfo";
            this.toolTip.SetToolTip(this.tabPageBasicInfo, resources.GetString("tabPageBasicInfo.ToolTip"));
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Controls.Add(this.flowLayoutPanel4);
            this.panel5.Controls.Add(this.symmetryControl);
            this.panel5.Name = "panel5";
            this.toolTip.SetToolTip(this.panel5, resources.GetString("panel5.ToolTip"));
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(this.flowLayoutPanel4, "flowLayoutPanel4");
            this.flowLayoutPanel4.Controls.Add(this.numericBoxZnumber);
            this.flowLayoutPanel4.Controls.Add(this.numericBoxVolume);
            this.flowLayoutPanel4.Controls.Add(this.numericBoxDensity);
            this.flowLayoutPanel4.Controls.Add(this.colorControl);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.toolTip.SetToolTip(this.flowLayoutPanel4, resources.GetString("flowLayoutPanel4.ToolTip"));
            // 
            // numericBoxZnumber
            // 
            resources.ApplyResources(this.numericBoxZnumber, "numericBoxZnumber");
            this.numericBoxZnumber.AllowMouseControl = false;
            this.numericBoxZnumber.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxZnumber.DecimalPlaces = -2;
            this.numericBoxZnumber.FooterBackColor = System.Drawing.Color.Transparent;
            this.numericBoxZnumber.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxZnumber.HeaderBackColor = System.Drawing.Color.Transparent;
            this.numericBoxZnumber.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxZnumber.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxZnumber.Maximum = double.PositiveInfinity;
            this.numericBoxZnumber.Minimum = double.NegativeInfinity;
            this.numericBoxZnumber.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxZnumber.MouseSpeed = 1D;
            this.numericBoxZnumber.Multiline = false;
            this.numericBoxZnumber.Name = "numericBoxZnumber";
            this.numericBoxZnumber.RadianValue = 0D;
            this.numericBoxZnumber.ReadOnly = true;
            this.numericBoxZnumber.RestrictLimitValue = true;
            this.numericBoxZnumber.ShowFraction = false;
            this.numericBoxZnumber.ShowPositiveSign = false;
            this.numericBoxZnumber.ShowUpDown = false;
            this.numericBoxZnumber.SkipEventDuringInput = false;
            this.numericBoxZnumber.SmartIncrement = true;
            this.numericBoxZnumber.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxZnumber.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxZnumber.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxZnumber.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxZnumber, resources.GetString("numericBoxZnumber.ToolTip1"));
            this.numericBoxZnumber.UpDown_Increment = 1D;
            this.numericBoxZnumber.Value = 0D;
            this.numericBoxZnumber.WordWrap = true;
            // 
            // numericBoxVolume
            // 
            resources.ApplyResources(this.numericBoxVolume, "numericBoxVolume");
            this.numericBoxVolume.AllowMouseControl = false;
            this.numericBoxVolume.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxVolume.DecimalPlaces = 4;
            this.numericBoxVolume.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxVolume.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxVolume.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxVolume.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxVolume.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxVolume.Maximum = double.PositiveInfinity;
            this.numericBoxVolume.Minimum = double.NegativeInfinity;
            this.numericBoxVolume.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxVolume.MouseSpeed = 1D;
            this.numericBoxVolume.Multiline = false;
            this.numericBoxVolume.Name = "numericBoxVolume";
            this.numericBoxVolume.RadianValue = 0D;
            this.numericBoxVolume.ReadOnly = true;
            this.numericBoxVolume.RestrictLimitValue = false;
            this.numericBoxVolume.ShowFraction = false;
            this.numericBoxVolume.ShowPositiveSign = false;
            this.numericBoxVolume.ShowUpDown = false;
            this.numericBoxVolume.SkipEventDuringInput = false;
            this.numericBoxVolume.SmartIncrement = true;
            this.numericBoxVolume.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxVolume.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxVolume.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxVolume.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxVolume, resources.GetString("numericBoxVolume.ToolTip1"));
            this.numericBoxVolume.UpDown_Increment = 1D;
            this.numericBoxVolume.Value = 0D;
            this.numericBoxVolume.WordWrap = true;
            // 
            // numericBoxDensity
            // 
            resources.ApplyResources(this.numericBoxDensity, "numericBoxDensity");
            this.numericBoxDensity.AllowMouseControl = false;
            this.numericBoxDensity.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxDensity.DecimalPlaces = 4;
            this.numericBoxDensity.FooterBackColor = System.Drawing.Color.Transparent;
            this.numericBoxDensity.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxDensity.HeaderBackColor = System.Drawing.Color.Transparent;
            this.numericBoxDensity.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxDensity.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxDensity.Maximum = double.PositiveInfinity;
            this.numericBoxDensity.Minimum = double.NegativeInfinity;
            this.numericBoxDensity.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxDensity.MouseSpeed = 1D;
            this.numericBoxDensity.Multiline = false;
            this.numericBoxDensity.Name = "numericBoxDensity";
            this.numericBoxDensity.RadianValue = 0D;
            this.numericBoxDensity.ReadOnly = true;
            this.numericBoxDensity.RestrictLimitValue = true;
            this.numericBoxDensity.ShowFraction = false;
            this.numericBoxDensity.ShowPositiveSign = false;
            this.numericBoxDensity.ShowUpDown = false;
            this.numericBoxDensity.SkipEventDuringInput = false;
            this.numericBoxDensity.SmartIncrement = true;
            this.numericBoxDensity.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDensity.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDensity.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDensity.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxDensity, resources.GetString("numericBoxDensity.ToolTip1"));
            this.numericBoxDensity.UpDown_Increment = 1D;
            this.numericBoxDensity.Value = 0D;
            this.numericBoxDensity.WordWrap = true;
            // 
            // colorControl
            // 
            resources.ApplyResources(this.colorControl, "colorControl");
            this.colorControl.Argb = -986896;
            this.colorControl.Blue = 240;
            this.colorControl.BlueF = 0.9411765F;
            this.colorControl.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControl.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControl.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControl.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.colorControl.FooterMargin = new System.Windows.Forms.Padding(0);
            this.colorControl.FooterText = "";
            this.colorControl.Green = 240;
            this.colorControl.GreenF = 0.9411765F;
            this.colorControl.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorControl.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.colorControl.HeaderText = "Color of Profile";
            this.colorControl.Name = "colorControl";
            this.colorControl.Red = 240;
            this.colorControl.RedF = 0.9411765F;
            this.colorControl.ToolTip = "";
            this.toolTip.SetToolTip(this.colorControl, resources.GetString("colorControl.ToolTip"));
            // 
            // symmetryControl
            // 
            resources.ApplyResources(this.symmetryControl, "symmetryControl");
            this.symmetryControl.CellConstants = ((System.ValueTuple<double, double, double, double, double, double>)(resources.GetObject("symmetryControl.CellConstants")));
            this.symmetryControl.CellConstantsErr = ((System.ValueTuple<double, double, double, double, double, double>)(resources.GetObject("symmetryControl.CellConstantsErr")));
            this.symmetryControl.Name = "symmetryControl";
            this.symmetryControl.SkipEvent = false;
            this.symmetryControl.SymmetrySeriesNumber = 0;
            this.toolTip.SetToolTip(this.symmetryControl, resources.GetString("symmetryControl.ToolTip"));
            this.symmetryControl.ItemChanged += new System.EventHandler(this.symmetryControl_ItemChanged);
            // 
            // tabPageAtom
            // 
            resources.ApplyResources(this.tabPageAtom, "tabPageAtom");
            this.tabPageAtom.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAtom.Controls.Add(this.atomControl);
            this.tabPageAtom.Controls.Add(this.panelAtom);
            this.tabPageAtom.Name = "tabPageAtom";
            this.toolTip.SetToolTip(this.tabPageAtom, resources.GetString("tabPageAtom.ToolTip"));
            // 
            // atomControl
            // 
            resources.ApplyResources(this.atomControl, "atomControl");
            this.atomControl.Alpha = 0D;
            this.atomControl.Ambient = 0D;
            this.atomControl.AppearanceTabVisible = true;
            this.atomControl.AtomColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.atomControl.AtomicPositionError = false;
            this.atomControl.AtomNo = 0;
            this.atomControl.AtomSubNoElectron = -1;
            this.atomControl.AtomSubNoXray = -1;
            this.atomControl.B11 = 0D;
            this.atomControl.B11Err = 0D;
            this.atomControl.B12 = 0D;
            this.atomControl.B12Err = 0D;
            this.atomControl.B13 = 0D;
            this.atomControl.B13Err = 0D;
            this.atomControl.B22 = 0D;
            this.atomControl.B22Err = 0D;
            this.atomControl.B23 = 0D;
            this.atomControl.B23Err = 0D;
            this.atomControl.B33 = 0D;
            this.atomControl.B33Err = 0D;
            this.atomControl.Biso = 0D;
            this.atomControl.BisoErr = 0D;
            this.atomControl.DebyeWallerError = false;
            this.atomControl.DebyeWallerTabVisible = true;
            this.atomControl.Diffusion = 0D;
            this.atomControl.ElementAndPositionTabVisible = true;
            this.atomControl.Emission = 0D;
            this.atomControl.IsotopicComposition = null;
            this.atomControl.Istoropy = false;
            this.atomControl.Label = "";
            this.atomControl.Name = "atomControl";
            this.atomControl.Occ = 0D;
            this.atomControl.OccErr = 0D;
            this.atomControl.OriginShiftVisible = true;
            this.atomControl.Radius = 0D;
            this.atomControl.ScatteringFactorTabVisible = true;
            this.atomControl.SelectedTabIndex = 0;
            this.atomControl.Shininess = 0D;
            this.atomControl.SkipEvent = false;
            this.atomControl.Specular = 0D;
            this.atomControl.SymmetrySeriesNumber = 0;
            this.toolTip.SetToolTip(this.atomControl, resources.GetString("atomControl.ToolTip"));
            this.atomControl.X = 0D;
            this.atomControl.XErr = 0D;
            this.atomControl.Y = 0D;
            this.atomControl.YErr = 0D;
            this.atomControl.Z = 0D;
            this.atomControl.ZErr = 0D;
            this.atomControl.ItemsChanged += new System.EventHandler(this.atomControl_AtomsChanged);
            // 
            // panelAtom
            // 
            resources.ApplyResources(this.panelAtom, "panelAtom");
            this.panelAtom.Controls.Add(this.panel3);
            this.panelAtom.Name = "panelAtom";
            this.toolTip.SetToolTip(this.panelAtom, resources.GetString("panelAtom.ToolTip"));
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.ContextMenuStrip = this.contextMenuStrip2;
            this.panel3.Name = "panel3";
            this.toolTip.SetToolTip(this.panel3, resources.GetString("panel3.ToolTip"));
            // 
            // contextMenuStrip2
            // 
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.toolTip.SetToolTip(this.contextMenuStrip2, resources.GetString("contextMenuStrip2.ToolTip"));
            // 
            // resetToolStripMenuItem
            // 
            resources.ApplyResources(this.resetToolStripMenuItem, "resetToolStripMenuItem");
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // tabPageBondsPolyhedra
            // 
            resources.ApplyResources(this.tabPageBondsPolyhedra, "tabPageBondsPolyhedra");
            this.tabPageBondsPolyhedra.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageBondsPolyhedra.Controls.Add(this.bondControl);
            this.tabPageBondsPolyhedra.Name = "tabPageBondsPolyhedra";
            this.toolTip.SetToolTip(this.tabPageBondsPolyhedra, resources.GetString("tabPageBondsPolyhedra.ToolTip"));
            // 
            // bondControl
            // 
            resources.ApplyResources(this.bondControl, "bondControl");
            this.bondControl.Crystal = null;
            this.bondControl.ElementList = new string[0];
            this.bondControl.Name = "bondControl";
            this.bondControl.SkipEvent = false;
            this.toolTip.SetToolTip(this.bondControl, resources.GetString("bondControl.ToolTip"));
            // 
            // tabPageReference
            // 
            resources.ApplyResources(this.tabPageReference, "tabPageReference");
            this.tabPageReference.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageReference.Controls.Add(this.groupBox8);
            this.tabPageReference.Controls.Add(this.groupBox6);
            this.tabPageReference.Controls.Add(this.groupBox7);
            this.tabPageReference.Controls.Add(this.groupBox5);
            this.tabPageReference.Name = "tabPageReference";
            this.toolTip.SetToolTip(this.tabPageReference, resources.GetString("tabPageReference.ToolTip"));
            // 
            // groupBox8
            // 
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Controls.Add(this.textBoxTitle);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox8, resources.GetString("groupBox8.ToolTip"));
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxTitle, "textBoxTitle");
            this.textBoxTitle.Name = "textBoxTitle";
            this.toolTip.SetToolTip(this.textBoxTitle, resources.GetString("textBoxTitle.ToolTip"));
            this.textBoxTitle.TextChanged += new System.EventHandler(this.textBoxReferenfeChanged_TextChanged);
            // 
            // groupBox6
            // 
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Controls.Add(this.textBoxAuthor);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox6, resources.GetString("groupBox6.ToolTip"));
            // 
            // textBoxAuthor
            // 
            this.textBoxAuthor.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxAuthor, "textBoxAuthor");
            this.textBoxAuthor.Name = "textBoxAuthor";
            this.toolTip.SetToolTip(this.textBoxAuthor, resources.GetString("textBoxAuthor.ToolTip"));
            this.textBoxAuthor.TextChanged += new System.EventHandler(this.textBoxReferenfeChanged_TextChanged);
            // 
            // groupBox7
            // 
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Controls.Add(this.textBoxJournal);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox7, resources.GetString("groupBox7.ToolTip"));
            // 
            // textBoxJournal
            // 
            this.textBoxJournal.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxJournal, "textBoxJournal");
            this.textBoxJournal.Name = "textBoxJournal";
            this.toolTip.SetToolTip(this.textBoxJournal, resources.GetString("textBoxJournal.ToolTip"));
            this.textBoxJournal.TextChanged += new System.EventHandler(this.textBoxReferenfeChanged_TextChanged);
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.textBoxMemo);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox5, resources.GetString("groupBox5.ToolTip"));
            // 
            // textBoxMemo
            // 
            resources.ApplyResources(this.textBoxMemo, "textBoxMemo");
            this.textBoxMemo.Name = "textBoxMemo";
            this.toolTip.SetToolTip(this.textBoxMemo, resources.GetString("textBoxMemo.ToolTip"));
            this.textBoxMemo.TextChanged += new System.EventHandler(this.textBoxReferenfeChanged_TextChanged);
            // 
            // tabPageEOS
            // 
            resources.ApplyResources(this.tabPageEOS, "tabPageEOS");
            this.tabPageEOS.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageEOS.Controls.Add(this.label83);
            this.tabPageEOS.Controls.Add(this.textBoxEOS_Note);
            this.tabPageEOS.Controls.Add(this.checkBoxUseEOS);
            this.tabPageEOS.Controls.Add(this.groupBox3);
            this.tabPageEOS.Controls.Add(this.groupBox2);
            this.tabPageEOS.Controls.Add(this.numericBoxEOS_T0);
            this.tabPageEOS.Controls.Add(this.numericalTextBoxTemperature);
            this.tabPageEOS.Controls.Add(this.numericBoxPressure);
            this.tabPageEOS.Name = "tabPageEOS";
            this.toolTip.SetToolTip(this.tabPageEOS, resources.GetString("tabPageEOS.ToolTip"));
            // 
            // label83
            // 
            resources.ApplyResources(this.label83, "label83");
            this.label83.Name = "label83";
            this.toolTip.SetToolTip(this.label83, resources.GetString("label83.ToolTip"));
            // 
            // textBoxEOS_Note
            // 
            resources.ApplyResources(this.textBoxEOS_Note, "textBoxEOS_Note");
            this.textBoxEOS_Note.Name = "textBoxEOS_Note";
            this.toolTip.SetToolTip(this.textBoxEOS_Note, resources.GetString("textBoxEOS_Note.ToolTip"));
            // 
            // checkBoxUseEOS
            // 
            resources.ApplyResources(this.checkBoxUseEOS, "checkBoxUseEOS");
            this.checkBoxUseEOS.Name = "checkBoxUseEOS";
            this.toolTip.SetToolTip(this.checkBoxUseEOS, resources.GetString("checkBoxUseEOS.ToolTip"));
            this.checkBoxUseEOS.UseVisualStyleBackColor = true;
            this.checkBoxUseEOS.CheckedChanged += new System.EventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.numericBoxEOS_C);
            this.groupBox3.Controls.Add(this.numericBoxEOS_B);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.radioButtonTdependenceK0andV0);
            this.groupBox3.Controls.Add(this.numericBoxEOS_A);
            this.groupBox3.Controls.Add(this.numericBoxEOS_KperT);
            this.groupBox3.Controls.Add(this.numericBoxEOS_Gamma0);
            this.groupBox3.Controls.Add(this.label76);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label69);
            this.groupBox3.Controls.Add(this.label72);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label91);
            this.groupBox3.Controls.Add(this.label95);
            this.groupBox3.Controls.Add(this.label93);
            this.groupBox3.Controls.Add(this.label85);
            this.groupBox3.Controls.Add(this.label86);
            this.groupBox3.Controls.Add(this.label96);
            this.groupBox3.Controls.Add(this.label97);
            this.groupBox3.Controls.Add(this.label94);
            this.groupBox3.Controls.Add(this.label92);
            this.groupBox3.Controls.Add(this.label84);
            this.groupBox3.Controls.Add(this.label64);
            this.groupBox3.Controls.Add(this.label73);
            this.groupBox3.Controls.Add(this.label65);
            this.groupBox3.Controls.Add(this.numericBoxEOS_Theta0);
            this.groupBox3.Controls.Add(this.numericBoxEOS_Q);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.radioButtonMieGruneisen);
            this.groupBox3.Controls.Add(this.label98);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox3, resources.GetString("groupBox3.ToolTip"));
            // 
            // numericBoxEOS_C
            // 
            resources.ApplyResources(this.numericBoxEOS_C, "numericBoxEOS_C");
            this.numericBoxEOS_C.AllowMouseControl = false;
            this.numericBoxEOS_C.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_C.DecimalPlaces = -1;
            this.numericBoxEOS_C.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_C.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_C.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_C.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_C.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_C.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_C.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_C.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_C.MouseSpeed = 1D;
            this.numericBoxEOS_C.Multiline = false;
            this.numericBoxEOS_C.Name = "numericBoxEOS_C";
            this.numericBoxEOS_C.RadianValue = 0D;
            this.numericBoxEOS_C.ReadOnly = false;
            this.numericBoxEOS_C.RestrictLimitValue = false;
            this.numericBoxEOS_C.ShowFraction = false;
            this.numericBoxEOS_C.ShowPositiveSign = false;
            this.numericBoxEOS_C.ShowUpDown = false;
            this.numericBoxEOS_C.SkipEventDuringInput = false;
            this.numericBoxEOS_C.SmartIncrement = true;
            this.numericBoxEOS_C.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_C.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_C.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxEOS_C.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_C, resources.GetString("numericBoxEOS_C.ToolTip1"));
            this.numericBoxEOS_C.UpDown_Increment = 1D;
            this.numericBoxEOS_C.Value = 0D;
            this.numericBoxEOS_C.WordWrap = true;
            this.numericBoxEOS_C.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_B
            // 
            resources.ApplyResources(this.numericBoxEOS_B, "numericBoxEOS_B");
            this.numericBoxEOS_B.AllowMouseControl = false;
            this.numericBoxEOS_B.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_B.DecimalPlaces = -1;
            this.numericBoxEOS_B.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_B.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_B.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_B.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_B.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_B.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_B.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_B.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_B.MouseSpeed = 1D;
            this.numericBoxEOS_B.Multiline = false;
            this.numericBoxEOS_B.Name = "numericBoxEOS_B";
            this.numericBoxEOS_B.RadianValue = 0D;
            this.numericBoxEOS_B.ReadOnly = false;
            this.numericBoxEOS_B.RestrictLimitValue = false;
            this.numericBoxEOS_B.ShowFraction = false;
            this.numericBoxEOS_B.ShowPositiveSign = false;
            this.numericBoxEOS_B.ShowUpDown = false;
            this.numericBoxEOS_B.SkipEventDuringInput = false;
            this.numericBoxEOS_B.SmartIncrement = true;
            this.numericBoxEOS_B.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_B.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_B.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxEOS_B.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_B, resources.GetString("numericBoxEOS_B.ToolTip1"));
            this.numericBoxEOS_B.UpDown_Increment = 1D;
            this.numericBoxEOS_B.Value = 0D;
            this.numericBoxEOS_B.WordWrap = true;
            this.numericBoxEOS_B.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.toolTip.SetToolTip(this.textBox1, resources.GetString("textBox1.ToolTip"));
            // 
            // radioButtonTdependenceK0andV0
            // 
            resources.ApplyResources(this.radioButtonTdependenceK0andV0, "radioButtonTdependenceK0andV0");
            this.radioButtonTdependenceK0andV0.Name = "radioButtonTdependenceK0andV0";
            this.toolTip.SetToolTip(this.radioButtonTdependenceK0andV0, resources.GetString("radioButtonTdependenceK0andV0.ToolTip"));
            this.radioButtonTdependenceK0andV0.UseVisualStyleBackColor = true;
            this.radioButtonTdependenceK0andV0.CheckedChanged += new System.EventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_A
            // 
            resources.ApplyResources(this.numericBoxEOS_A, "numericBoxEOS_A");
            this.numericBoxEOS_A.AllowMouseControl = false;
            this.numericBoxEOS_A.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_A.DecimalPlaces = -1;
            this.numericBoxEOS_A.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_A.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_A.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_A.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_A.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_A.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_A.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_A.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_A.MouseSpeed = 1D;
            this.numericBoxEOS_A.Multiline = false;
            this.numericBoxEOS_A.Name = "numericBoxEOS_A";
            this.numericBoxEOS_A.RadianValue = 0D;
            this.numericBoxEOS_A.ReadOnly = false;
            this.numericBoxEOS_A.RestrictLimitValue = false;
            this.numericBoxEOS_A.ShowFraction = false;
            this.numericBoxEOS_A.ShowPositiveSign = false;
            this.numericBoxEOS_A.ShowUpDown = false;
            this.numericBoxEOS_A.SkipEventDuringInput = false;
            this.numericBoxEOS_A.SmartIncrement = true;
            this.numericBoxEOS_A.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_A.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_A.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxEOS_A.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_A, resources.GetString("numericBoxEOS_A.ToolTip1"));
            this.numericBoxEOS_A.UpDown_Increment = 1D;
            this.numericBoxEOS_A.Value = 0D;
            this.numericBoxEOS_A.WordWrap = true;
            this.numericBoxEOS_A.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_KperT
            // 
            resources.ApplyResources(this.numericBoxEOS_KperT, "numericBoxEOS_KperT");
            this.numericBoxEOS_KperT.AllowMouseControl = false;
            this.numericBoxEOS_KperT.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KperT.DecimalPlaces = -1;
            this.numericBoxEOS_KperT.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KperT.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_KperT.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KperT.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_KperT.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_KperT.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_KperT.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_KperT.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_KperT.MouseSpeed = 1D;
            this.numericBoxEOS_KperT.Multiline = false;
            this.numericBoxEOS_KperT.Name = "numericBoxEOS_KperT";
            this.numericBoxEOS_KperT.RadianValue = 0D;
            this.numericBoxEOS_KperT.ReadOnly = false;
            this.numericBoxEOS_KperT.RestrictLimitValue = false;
            this.numericBoxEOS_KperT.ShowFraction = false;
            this.numericBoxEOS_KperT.ShowPositiveSign = false;
            this.numericBoxEOS_KperT.ShowUpDown = false;
            this.numericBoxEOS_KperT.SkipEventDuringInput = false;
            this.numericBoxEOS_KperT.SmartIncrement = true;
            this.numericBoxEOS_KperT.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_KperT.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_KperT.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxEOS_KperT.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_KperT, resources.GetString("numericBoxEOS_KperT.ToolTip1"));
            this.numericBoxEOS_KperT.UpDown_Increment = 1D;
            this.numericBoxEOS_KperT.Value = 0D;
            this.numericBoxEOS_KperT.WordWrap = true;
            this.numericBoxEOS_KperT.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_Gamma0
            // 
            resources.ApplyResources(this.numericBoxEOS_Gamma0, "numericBoxEOS_Gamma0");
            this.numericBoxEOS_Gamma0.AllowMouseControl = false;
            this.numericBoxEOS_Gamma0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Gamma0.DecimalPlaces = -1;
            this.numericBoxEOS_Gamma0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Gamma0.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_Gamma0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Gamma0.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_Gamma0.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_Gamma0.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_Gamma0.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_Gamma0.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_Gamma0.MouseSpeed = 1D;
            this.numericBoxEOS_Gamma0.Multiline = false;
            this.numericBoxEOS_Gamma0.Name = "numericBoxEOS_Gamma0";
            this.numericBoxEOS_Gamma0.RadianValue = 0D;
            this.numericBoxEOS_Gamma0.ReadOnly = false;
            this.numericBoxEOS_Gamma0.RestrictLimitValue = false;
            this.numericBoxEOS_Gamma0.ShowFraction = false;
            this.numericBoxEOS_Gamma0.ShowPositiveSign = false;
            this.numericBoxEOS_Gamma0.ShowUpDown = false;
            this.numericBoxEOS_Gamma0.SkipEventDuringInput = false;
            this.numericBoxEOS_Gamma0.SmartIncrement = true;
            this.numericBoxEOS_Gamma0.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_Gamma0.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_Gamma0.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxEOS_Gamma0.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_Gamma0, resources.GetString("numericBoxEOS_Gamma0.ToolTip1"));
            this.numericBoxEOS_Gamma0.UpDown_Increment = 1D;
            this.numericBoxEOS_Gamma0.Value = 0D;
            this.numericBoxEOS_Gamma0.WordWrap = true;
            this.numericBoxEOS_Gamma0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // label76
            // 
            resources.ApplyResources(this.label76, "label76");
            this.label76.Name = "label76";
            this.toolTip.SetToolTip(this.label76, resources.GetString("label76.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTip.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // label69
            // 
            resources.ApplyResources(this.label69, "label69");
            this.label69.Name = "label69";
            this.toolTip.SetToolTip(this.label69, resources.GetString("label69.ToolTip"));
            // 
            // label72
            // 
            resources.ApplyResources(this.label72, "label72");
            this.label72.Name = "label72";
            this.toolTip.SetToolTip(this.label72, resources.GetString("label72.ToolTip"));
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            this.toolTip.SetToolTip(this.label16, resources.GetString("label16.ToolTip"));
            // 
            // label91
            // 
            resources.ApplyResources(this.label91, "label91");
            this.label91.Name = "label91";
            this.toolTip.SetToolTip(this.label91, resources.GetString("label91.ToolTip"));
            // 
            // label95
            // 
            resources.ApplyResources(this.label95, "label95");
            this.label95.Name = "label95";
            this.toolTip.SetToolTip(this.label95, resources.GetString("label95.ToolTip"));
            // 
            // label93
            // 
            resources.ApplyResources(this.label93, "label93");
            this.label93.Name = "label93";
            this.toolTip.SetToolTip(this.label93, resources.GetString("label93.ToolTip"));
            // 
            // label85
            // 
            resources.ApplyResources(this.label85, "label85");
            this.label85.Name = "label85";
            this.toolTip.SetToolTip(this.label85, resources.GetString("label85.ToolTip"));
            // 
            // label86
            // 
            resources.ApplyResources(this.label86, "label86");
            this.label86.Name = "label86";
            this.toolTip.SetToolTip(this.label86, resources.GetString("label86.ToolTip"));
            // 
            // label96
            // 
            resources.ApplyResources(this.label96, "label96");
            this.label96.Name = "label96";
            this.toolTip.SetToolTip(this.label96, resources.GetString("label96.ToolTip"));
            // 
            // label97
            // 
            resources.ApplyResources(this.label97, "label97");
            this.label97.Name = "label97";
            this.toolTip.SetToolTip(this.label97, resources.GetString("label97.ToolTip"));
            // 
            // label94
            // 
            resources.ApplyResources(this.label94, "label94");
            this.label94.Name = "label94";
            this.toolTip.SetToolTip(this.label94, resources.GetString("label94.ToolTip"));
            // 
            // label92
            // 
            resources.ApplyResources(this.label92, "label92");
            this.label92.Name = "label92";
            this.toolTip.SetToolTip(this.label92, resources.GetString("label92.ToolTip"));
            // 
            // label84
            // 
            resources.ApplyResources(this.label84, "label84");
            this.label84.Name = "label84";
            this.toolTip.SetToolTip(this.label84, resources.GetString("label84.ToolTip"));
            // 
            // label64
            // 
            resources.ApplyResources(this.label64, "label64");
            this.label64.Name = "label64";
            this.toolTip.SetToolTip(this.label64, resources.GetString("label64.ToolTip"));
            // 
            // label73
            // 
            resources.ApplyResources(this.label73, "label73");
            this.label73.Name = "label73";
            this.toolTip.SetToolTip(this.label73, resources.GetString("label73.ToolTip"));
            // 
            // label65
            // 
            resources.ApplyResources(this.label65, "label65");
            this.label65.Name = "label65";
            this.toolTip.SetToolTip(this.label65, resources.GetString("label65.ToolTip"));
            // 
            // numericBoxEOS_Theta0
            // 
            resources.ApplyResources(this.numericBoxEOS_Theta0, "numericBoxEOS_Theta0");
            this.numericBoxEOS_Theta0.AllowMouseControl = false;
            this.numericBoxEOS_Theta0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Theta0.DecimalPlaces = -1;
            this.numericBoxEOS_Theta0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Theta0.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_Theta0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Theta0.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_Theta0.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_Theta0.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_Theta0.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_Theta0.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_Theta0.MouseSpeed = 1D;
            this.numericBoxEOS_Theta0.Multiline = false;
            this.numericBoxEOS_Theta0.Name = "numericBoxEOS_Theta0";
            this.numericBoxEOS_Theta0.RadianValue = 5.2359877559829888D;
            this.numericBoxEOS_Theta0.ReadOnly = false;
            this.numericBoxEOS_Theta0.RestrictLimitValue = false;
            this.numericBoxEOS_Theta0.ShowFraction = false;
            this.numericBoxEOS_Theta0.ShowPositiveSign = false;
            this.numericBoxEOS_Theta0.ShowUpDown = false;
            this.numericBoxEOS_Theta0.SkipEventDuringInput = false;
            this.numericBoxEOS_Theta0.SmartIncrement = true;
            this.numericBoxEOS_Theta0.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_Theta0.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_Theta0.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxEOS_Theta0.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_Theta0, resources.GetString("numericBoxEOS_Theta0.ToolTip1"));
            this.numericBoxEOS_Theta0.UpDown_Increment = 1D;
            this.numericBoxEOS_Theta0.Value = 300D;
            this.numericBoxEOS_Theta0.WordWrap = true;
            this.numericBoxEOS_Theta0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_Q
            // 
            resources.ApplyResources(this.numericBoxEOS_Q, "numericBoxEOS_Q");
            this.numericBoxEOS_Q.AllowMouseControl = false;
            this.numericBoxEOS_Q.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Q.DecimalPlaces = -1;
            this.numericBoxEOS_Q.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Q.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_Q.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Q.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_Q.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_Q.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_Q.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_Q.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_Q.MouseSpeed = 1D;
            this.numericBoxEOS_Q.Multiline = false;
            this.numericBoxEOS_Q.Name = "numericBoxEOS_Q";
            this.numericBoxEOS_Q.RadianValue = 0D;
            this.numericBoxEOS_Q.ReadOnly = false;
            this.numericBoxEOS_Q.RestrictLimitValue = false;
            this.numericBoxEOS_Q.ShowFraction = false;
            this.numericBoxEOS_Q.ShowPositiveSign = false;
            this.numericBoxEOS_Q.ShowUpDown = false;
            this.numericBoxEOS_Q.SkipEventDuringInput = false;
            this.numericBoxEOS_Q.SmartIncrement = true;
            this.numericBoxEOS_Q.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_Q.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_Q.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxEOS_Q.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_Q, resources.GetString("numericBoxEOS_Q.ToolTip1"));
            this.numericBoxEOS_Q.UpDown_Increment = 1D;
            this.numericBoxEOS_Q.Value = 0D;
            this.numericBoxEOS_Q.WordWrap = true;
            this.numericBoxEOS_Q.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // radioButtonMieGruneisen
            // 
            resources.ApplyResources(this.radioButtonMieGruneisen, "radioButtonMieGruneisen");
            this.radioButtonMieGruneisen.Checked = true;
            this.radioButtonMieGruneisen.Name = "radioButtonMieGruneisen";
            this.radioButtonMieGruneisen.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonMieGruneisen, resources.GetString("radioButtonMieGruneisen.ToolTip"));
            this.radioButtonMieGruneisen.UseVisualStyleBackColor = true;
            this.radioButtonMieGruneisen.CheckedChanged += new System.EventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // label98
            // 
            resources.ApplyResources(this.label98, "label98");
            this.label98.Name = "label98";
            this.toolTip.SetToolTip(this.label98, resources.GetString("label98.ToolTip"));
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.toolTip.SetToolTip(this.textBox2, resources.GetString("textBox2.ToolTip"));
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.label75);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label67);
            this.groupBox2.Controls.Add(this.label66);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numericalTextBoxEOS_V0perMol);
            this.groupBox2.Controls.Add(this.numericBoxEOS_V0perCell);
            this.groupBox2.Controls.Add(this.numericBoxEOS_KT0);
            this.groupBox2.Controls.Add(this.label70);
            this.groupBox2.Controls.Add(this.label81);
            this.groupBox2.Controls.Add(this.label71);
            this.groupBox2.Controls.Add(this.label80);
            this.groupBox2.Controls.Add(this.label68);
            this.groupBox2.Controls.Add(this.numericBoxEOS_KprimeT0);
            this.groupBox2.Controls.Add(this.radioButtonVinet);
            this.groupBox2.Controls.Add(this.radioButtonBirchMurnaghan);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // label75
            // 
            resources.ApplyResources(this.label75, "label75");
            this.label75.Name = "label75";
            this.toolTip.SetToolTip(this.label75, resources.GetString("label75.ToolTip"));
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            this.toolTip.SetToolTip(this.label15, resources.GetString("label15.ToolTip"));
            // 
            // label67
            // 
            resources.ApplyResources(this.label67, "label67");
            this.label67.Name = "label67";
            this.toolTip.SetToolTip(this.label67, resources.GetString("label67.ToolTip"));
            // 
            // label66
            // 
            resources.ApplyResources(this.label66, "label66");
            this.label66.Name = "label66";
            this.toolTip.SetToolTip(this.label66, resources.GetString("label66.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // numericalTextBoxEOS_V0perMol
            // 
            resources.ApplyResources(this.numericalTextBoxEOS_V0perMol, "numericalTextBoxEOS_V0perMol");
            this.numericalTextBoxEOS_V0perMol.AllowMouseControl = false;
            this.numericalTextBoxEOS_V0perMol.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxEOS_V0perMol.DecimalPlaces = -1;
            this.numericalTextBoxEOS_V0perMol.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxEOS_V0perMol.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxEOS_V0perMol.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxEOS_V0perMol.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxEOS_V0perMol.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxEOS_V0perMol.Maximum = double.PositiveInfinity;
            this.numericalTextBoxEOS_V0perMol.Minimum = double.NegativeInfinity;
            this.numericalTextBoxEOS_V0perMol.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxEOS_V0perMol.MouseSpeed = 1D;
            this.numericalTextBoxEOS_V0perMol.Multiline = false;
            this.numericalTextBoxEOS_V0perMol.Name = "numericalTextBoxEOS_V0perMol";
            this.numericalTextBoxEOS_V0perMol.RadianValue = 5.2359877559829888D;
            this.numericalTextBoxEOS_V0perMol.ReadOnly = true;
            this.numericalTextBoxEOS_V0perMol.RestrictLimitValue = false;
            this.numericalTextBoxEOS_V0perMol.ShowFraction = false;
            this.numericalTextBoxEOS_V0perMol.ShowPositiveSign = false;
            this.numericalTextBoxEOS_V0perMol.ShowUpDown = false;
            this.numericalTextBoxEOS_V0perMol.SkipEventDuringInput = false;
            this.numericalTextBoxEOS_V0perMol.SmartIncrement = true;
            this.numericalTextBoxEOS_V0perMol.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxEOS_V0perMol.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxEOS_V0perMol.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxEOS_V0perMol.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxEOS_V0perMol, resources.GetString("numericalTextBoxEOS_V0perMol.ToolTip1"));
            this.numericalTextBoxEOS_V0perMol.UpDown_Increment = 1D;
            this.numericalTextBoxEOS_V0perMol.Value = 300D;
            this.numericalTextBoxEOS_V0perMol.WordWrap = true;
            this.numericalTextBoxEOS_V0perMol.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_V0perMol_ValueChanged);
            this.numericalTextBoxEOS_V0perMol.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_V0perMol_Click2);
            // 
            // numericBoxEOS_V0perCell
            // 
            resources.ApplyResources(this.numericBoxEOS_V0perCell, "numericBoxEOS_V0perCell");
            this.numericBoxEOS_V0perCell.AllowMouseControl = false;
            this.numericBoxEOS_V0perCell.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perCell.DecimalPlaces = -1;
            this.numericBoxEOS_V0perCell.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perCell.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_V0perCell.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perCell.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_V0perCell.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_V0perCell.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_V0perCell.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_V0perCell.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_V0perCell.MouseSpeed = 1D;
            this.numericBoxEOS_V0perCell.Multiline = false;
            this.numericBoxEOS_V0perCell.Name = "numericBoxEOS_V0perCell";
            this.numericBoxEOS_V0perCell.RadianValue = 5.2359877559829888D;
            this.numericBoxEOS_V0perCell.ReadOnly = false;
            this.numericBoxEOS_V0perCell.RestrictLimitValue = false;
            this.numericBoxEOS_V0perCell.ShowFraction = false;
            this.numericBoxEOS_V0perCell.ShowPositiveSign = false;
            this.numericBoxEOS_V0perCell.ShowUpDown = false;
            this.numericBoxEOS_V0perCell.SkipEventDuringInput = false;
            this.numericBoxEOS_V0perCell.SmartIncrement = true;
            this.numericBoxEOS_V0perCell.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_V0perCell.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_V0perCell.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxEOS_V0perCell.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_V0perCell, resources.GetString("numericBoxEOS_V0perCell.ToolTip1"));
            this.numericBoxEOS_V0perCell.UpDown_Increment = 1D;
            this.numericBoxEOS_V0perCell.Value = 300D;
            this.numericBoxEOS_V0perCell.WordWrap = true;
            this.numericBoxEOS_V0perCell.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            this.numericBoxEOS_V0perCell.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_V0perCell_Click2);
            // 
            // numericBoxEOS_KT0
            // 
            resources.ApplyResources(this.numericBoxEOS_KT0, "numericBoxEOS_KT0");
            this.numericBoxEOS_KT0.AllowMouseControl = false;
            this.numericBoxEOS_KT0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KT0.DecimalPlaces = -1;
            this.numericBoxEOS_KT0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KT0.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_KT0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KT0.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_KT0.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_KT0.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_KT0.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_KT0.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_KT0.MouseSpeed = 1D;
            this.numericBoxEOS_KT0.Multiline = false;
            this.numericBoxEOS_KT0.Name = "numericBoxEOS_KT0";
            this.numericBoxEOS_KT0.RadianValue = 0D;
            this.numericBoxEOS_KT0.ReadOnly = false;
            this.numericBoxEOS_KT0.RestrictLimitValue = false;
            this.numericBoxEOS_KT0.ShowFraction = false;
            this.numericBoxEOS_KT0.ShowPositiveSign = false;
            this.numericBoxEOS_KT0.ShowUpDown = false;
            this.numericBoxEOS_KT0.SkipEventDuringInput = false;
            this.numericBoxEOS_KT0.SmartIncrement = true;
            this.numericBoxEOS_KT0.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_KT0.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_KT0.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxEOS_KT0.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_KT0, resources.GetString("numericBoxEOS_KT0.ToolTip1"));
            this.numericBoxEOS_KT0.UpDown_Increment = 1D;
            this.numericBoxEOS_KT0.Value = 0D;
            this.numericBoxEOS_KT0.WordWrap = true;
            this.numericBoxEOS_KT0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // label70
            // 
            resources.ApplyResources(this.label70, "label70");
            this.label70.Name = "label70";
            this.toolTip.SetToolTip(this.label70, resources.GetString("label70.ToolTip"));
            // 
            // label81
            // 
            resources.ApplyResources(this.label81, "label81");
            this.label81.Name = "label81";
            this.toolTip.SetToolTip(this.label81, resources.GetString("label81.ToolTip"));
            // 
            // label71
            // 
            resources.ApplyResources(this.label71, "label71");
            this.label71.Name = "label71";
            this.toolTip.SetToolTip(this.label71, resources.GetString("label71.ToolTip"));
            // 
            // label80
            // 
            resources.ApplyResources(this.label80, "label80");
            this.label80.Name = "label80";
            this.toolTip.SetToolTip(this.label80, resources.GetString("label80.ToolTip"));
            // 
            // label68
            // 
            resources.ApplyResources(this.label68, "label68");
            this.label68.Name = "label68";
            this.toolTip.SetToolTip(this.label68, resources.GetString("label68.ToolTip"));
            // 
            // numericBoxEOS_KprimeT0
            // 
            resources.ApplyResources(this.numericBoxEOS_KprimeT0, "numericBoxEOS_KprimeT0");
            this.numericBoxEOS_KprimeT0.AllowMouseControl = false;
            this.numericBoxEOS_KprimeT0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KprimeT0.DecimalPlaces = -1;
            this.numericBoxEOS_KprimeT0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KprimeT0.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_KprimeT0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KprimeT0.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_KprimeT0.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_KprimeT0.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_KprimeT0.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_KprimeT0.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_KprimeT0.MouseSpeed = 1D;
            this.numericBoxEOS_KprimeT0.Multiline = false;
            this.numericBoxEOS_KprimeT0.Name = "numericBoxEOS_KprimeT0";
            this.numericBoxEOS_KprimeT0.RadianValue = 0.069813170079773182D;
            this.numericBoxEOS_KprimeT0.ReadOnly = false;
            this.numericBoxEOS_KprimeT0.RestrictLimitValue = false;
            this.numericBoxEOS_KprimeT0.ShowFraction = false;
            this.numericBoxEOS_KprimeT0.ShowPositiveSign = false;
            this.numericBoxEOS_KprimeT0.ShowUpDown = false;
            this.numericBoxEOS_KprimeT0.SkipEventDuringInput = false;
            this.numericBoxEOS_KprimeT0.SmartIncrement = true;
            this.numericBoxEOS_KprimeT0.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_KprimeT0.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_KprimeT0.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxEOS_KprimeT0.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_KprimeT0, resources.GetString("numericBoxEOS_KprimeT0.ToolTip1"));
            this.numericBoxEOS_KprimeT0.UpDown_Increment = 1D;
            this.numericBoxEOS_KprimeT0.Value = 4D;
            this.numericBoxEOS_KprimeT0.WordWrap = true;
            this.numericBoxEOS_KprimeT0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // radioButtonVinet
            // 
            resources.ApplyResources(this.radioButtonVinet, "radioButtonVinet");
            this.radioButtonVinet.Name = "radioButtonVinet";
            this.toolTip.SetToolTip(this.radioButtonVinet, resources.GetString("radioButtonVinet.ToolTip"));
            this.radioButtonVinet.UseVisualStyleBackColor = true;
            this.radioButtonVinet.CheckedChanged += new System.EventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // radioButtonBirchMurnaghan
            // 
            resources.ApplyResources(this.radioButtonBirchMurnaghan, "radioButtonBirchMurnaghan");
            this.radioButtonBirchMurnaghan.Checked = true;
            this.radioButtonBirchMurnaghan.Name = "radioButtonBirchMurnaghan";
            this.radioButtonBirchMurnaghan.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonBirchMurnaghan, resources.GetString("radioButtonBirchMurnaghan.ToolTip"));
            this.radioButtonBirchMurnaghan.UseVisualStyleBackColor = true;
            this.radioButtonBirchMurnaghan.CheckedChanged += new System.EventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_T0
            // 
            resources.ApplyResources(this.numericBoxEOS_T0, "numericBoxEOS_T0");
            this.numericBoxEOS_T0.AllowMouseControl = false;
            this.numericBoxEOS_T0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_T0.DecimalPlaces = -1;
            this.numericBoxEOS_T0.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_T0.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_T0.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_T0.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxEOS_T0.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxEOS_T0.Maximum = double.PositiveInfinity;
            this.numericBoxEOS_T0.Minimum = double.NegativeInfinity;
            this.numericBoxEOS_T0.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxEOS_T0.MouseSpeed = 1D;
            this.numericBoxEOS_T0.Multiline = false;
            this.numericBoxEOS_T0.Name = "numericBoxEOS_T0";
            this.numericBoxEOS_T0.RadianValue = 5.2359877559829888D;
            this.numericBoxEOS_T0.ReadOnly = false;
            this.numericBoxEOS_T0.RestrictLimitValue = false;
            this.numericBoxEOS_T0.ShowFraction = false;
            this.numericBoxEOS_T0.ShowPositiveSign = false;
            this.numericBoxEOS_T0.ShowUpDown = false;
            this.numericBoxEOS_T0.SkipEventDuringInput = false;
            this.numericBoxEOS_T0.SmartIncrement = true;
            this.numericBoxEOS_T0.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxEOS_T0.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxEOS_T0.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxEOS_T0.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxEOS_T0, resources.GetString("numericBoxEOS_T0.ToolTip1"));
            this.numericBoxEOS_T0.UpDown_Increment = 1D;
            this.numericBoxEOS_T0.Value = 300D;
            this.numericBoxEOS_T0.WordWrap = true;
            this.numericBoxEOS_T0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericalTextBoxTemperature
            // 
            resources.ApplyResources(this.numericalTextBoxTemperature, "numericalTextBoxTemperature");
            this.numericalTextBoxTemperature.AllowMouseControl = false;
            this.numericalTextBoxTemperature.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxTemperature.DecimalPlaces = -1;
            this.numericalTextBoxTemperature.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxTemperature.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxTemperature.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxTemperature.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxTemperature.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxTemperature.Maximum = double.PositiveInfinity;
            this.numericalTextBoxTemperature.Minimum = double.NegativeInfinity;
            this.numericalTextBoxTemperature.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxTemperature.MouseSpeed = 1D;
            this.numericalTextBoxTemperature.Multiline = false;
            this.numericalTextBoxTemperature.Name = "numericalTextBoxTemperature";
            this.numericalTextBoxTemperature.RadianValue = 5.2359877559829888D;
            this.numericalTextBoxTemperature.ReadOnly = false;
            this.numericalTextBoxTemperature.RestrictLimitValue = false;
            this.numericalTextBoxTemperature.ShowFraction = false;
            this.numericalTextBoxTemperature.ShowPositiveSign = false;
            this.numericalTextBoxTemperature.ShowUpDown = false;
            this.numericalTextBoxTemperature.SkipEventDuringInput = false;
            this.numericalTextBoxTemperature.SmartIncrement = true;
            this.numericalTextBoxTemperature.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxTemperature.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxTemperature.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericalTextBoxTemperature.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxTemperature, resources.GetString("numericalTextBoxTemperature.ToolTip1"));
            this.numericalTextBoxTemperature.UpDown_Increment = 1D;
            this.numericalTextBoxTemperature.Value = 300D;
            this.numericalTextBoxTemperature.WordWrap = true;
            this.numericalTextBoxTemperature.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxPressure
            // 
            resources.ApplyResources(this.numericBoxPressure, "numericBoxPressure");
            this.numericBoxPressure.AllowMouseControl = false;
            this.numericBoxPressure.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPressure.DecimalPlaces = 5;
            this.numericBoxPressure.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPressure.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxPressure.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPressure.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxPressure.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxPressure.Maximum = double.PositiveInfinity;
            this.numericBoxPressure.Minimum = double.NegativeInfinity;
            this.numericBoxPressure.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxPressure.MouseSpeed = 1D;
            this.numericBoxPressure.Multiline = false;
            this.numericBoxPressure.Name = "numericBoxPressure";
            this.numericBoxPressure.RadianValue = 0D;
            this.numericBoxPressure.ReadOnly = true;
            this.numericBoxPressure.RestrictLimitValue = false;
            this.numericBoxPressure.ShowFraction = false;
            this.numericBoxPressure.ShowPositiveSign = false;
            this.numericBoxPressure.ShowUpDown = false;
            this.numericBoxPressure.SkipEventDuringInput = false;
            this.numericBoxPressure.SmartIncrement = true;
            this.numericBoxPressure.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPressure.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxPressure.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxPressure.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericBoxPressure, resources.GetString("numericBoxPressure.ToolTip1"));
            this.numericBoxPressure.UpDown_Increment = 1D;
            this.numericBoxPressure.Value = 0D;
            this.numericBoxPressure.WordWrap = true;
            // 
            // tabPageElasticity
            // 
            resources.ApplyResources(this.tabPageElasticity, "tabPageElasticity");
            this.tabPageElasticity.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageElasticity.Controls.Add(this.elasticityControl1);
            this.tabPageElasticity.Name = "tabPageElasticity";
            this.toolTip.SetToolTip(this.tabPageElasticity, resources.GetString("tabPageElasticity.ToolTip"));
            // 
            // elasticityControl1
            // 
            resources.ApplyResources(this.elasticityControl1, "elasticityControl1");
            this.elasticityControl1.Mode = Crystallography.Elasticity.Mode.Stiffness;
            this.elasticityControl1.Name = "elasticityControl1";
            this.elasticityControl1.SymmetrySeriesNumber = 1;
            this.toolTip.SetToolTip(this.elasticityControl1, resources.GetString("elasticityControl1.ToolTip"));
            this.elasticityControl1.ValueChanged += new Crystallography.Controls.ElasticityControl.MyEventHandler(this.elasticityControl1_ValueChanged);
            // 
            // tabPageStrainStress
            // 
            resources.ApplyResources(this.tabPageStrainStress, "tabPageStrainStress");
            this.tabPageStrainStress.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxHill);
            this.tabPageStrainStress.Controls.Add(this.label116);
            this.tabPageStrainStress.Controls.Add(this.label117);
            this.tabPageStrainStress.Controls.Add(this.label109);
            this.tabPageStrainStress.Controls.Add(this.label110);
            this.tabPageStrainStress.Controls.Add(this.label111);
            this.tabPageStrainStress.Controls.Add(this.label112);
            this.tabPageStrainStress.Controls.Add(this.label113);
            this.tabPageStrainStress.Controls.Add(this.label114);
            this.tabPageStrainStress.Controls.Add(this.label115);
            this.tabPageStrainStress.Controls.Add(this.label102);
            this.tabPageStrainStress.Controls.Add(this.label103);
            this.tabPageStrainStress.Controls.Add(this.label104);
            this.tabPageStrainStress.Controls.Add(this.label105);
            this.tabPageStrainStress.Controls.Add(this.label106);
            this.tabPageStrainStress.Controls.Add(this.label107);
            this.tabPageStrainStress.Controls.Add(this.label108);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStress33);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStress22);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStress11);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStress23);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStress13);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStress12);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStrain33);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStrain11);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStrain22);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStrain12);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStrain23);
            this.tabPageStrainStress.Controls.Add(this.numericalTextBoxStrain13);
            this.tabPageStrainStress.Name = "tabPageStrainStress";
            this.toolTip.SetToolTip(this.tabPageStrainStress, resources.GetString("tabPageStrainStress.ToolTip"));
            // 
            // numericalTextBoxHill
            // 
            resources.ApplyResources(this.numericalTextBoxHill, "numericalTextBoxHill");
            this.numericalTextBoxHill.AllowMouseControl = false;
            this.numericalTextBoxHill.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxHill.DecimalPlaces = -1;
            this.numericalTextBoxHill.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxHill.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxHill.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxHill.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxHill.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxHill.Maximum = double.PositiveInfinity;
            this.numericalTextBoxHill.Minimum = double.NegativeInfinity;
            this.numericalTextBoxHill.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxHill.MouseSpeed = 1D;
            this.numericalTextBoxHill.Multiline = false;
            this.numericalTextBoxHill.Name = "numericalTextBoxHill";
            this.numericalTextBoxHill.RadianValue = 0.017453292519943295D;
            this.numericalTextBoxHill.ReadOnly = false;
            this.numericalTextBoxHill.RestrictLimitValue = false;
            this.numericalTextBoxHill.ShowFraction = false;
            this.numericalTextBoxHill.ShowPositiveSign = false;
            this.numericalTextBoxHill.ShowUpDown = false;
            this.numericalTextBoxHill.SkipEventDuringInput = false;
            this.numericalTextBoxHill.SmartIncrement = true;
            this.numericalTextBoxHill.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxHill.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxHill.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxHill.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxHill, resources.GetString("numericalTextBoxHill.ToolTip1"));
            this.numericalTextBoxHill.UpDown_Increment = 1D;
            this.numericalTextBoxHill.Value = 1D;
            this.numericalTextBoxHill.WordWrap = true;
            // 
            // label116
            // 
            resources.ApplyResources(this.label116, "label116");
            this.label116.Name = "label116";
            this.toolTip.SetToolTip(this.label116, resources.GetString("label116.ToolTip"));
            // 
            // label117
            // 
            resources.ApplyResources(this.label117, "label117");
            this.label117.Name = "label117";
            this.toolTip.SetToolTip(this.label117, resources.GetString("label117.ToolTip"));
            // 
            // label109
            // 
            resources.ApplyResources(this.label109, "label109");
            this.label109.Name = "label109";
            this.toolTip.SetToolTip(this.label109, resources.GetString("label109.ToolTip"));
            // 
            // label110
            // 
            resources.ApplyResources(this.label110, "label110");
            this.label110.Name = "label110";
            this.toolTip.SetToolTip(this.label110, resources.GetString("label110.ToolTip"));
            // 
            // label111
            // 
            resources.ApplyResources(this.label111, "label111");
            this.label111.Name = "label111";
            this.toolTip.SetToolTip(this.label111, resources.GetString("label111.ToolTip"));
            // 
            // label112
            // 
            resources.ApplyResources(this.label112, "label112");
            this.label112.Name = "label112";
            this.toolTip.SetToolTip(this.label112, resources.GetString("label112.ToolTip"));
            // 
            // label113
            // 
            resources.ApplyResources(this.label113, "label113");
            this.label113.Name = "label113";
            this.toolTip.SetToolTip(this.label113, resources.GetString("label113.ToolTip"));
            // 
            // label114
            // 
            resources.ApplyResources(this.label114, "label114");
            this.label114.Name = "label114";
            this.toolTip.SetToolTip(this.label114, resources.GetString("label114.ToolTip"));
            // 
            // label115
            // 
            resources.ApplyResources(this.label115, "label115");
            this.label115.Name = "label115";
            this.toolTip.SetToolTip(this.label115, resources.GetString("label115.ToolTip"));
            // 
            // label102
            // 
            resources.ApplyResources(this.label102, "label102");
            this.label102.Name = "label102";
            this.toolTip.SetToolTip(this.label102, resources.GetString("label102.ToolTip"));
            // 
            // label103
            // 
            resources.ApplyResources(this.label103, "label103");
            this.label103.Name = "label103";
            this.toolTip.SetToolTip(this.label103, resources.GetString("label103.ToolTip"));
            // 
            // label104
            // 
            resources.ApplyResources(this.label104, "label104");
            this.label104.Name = "label104";
            this.toolTip.SetToolTip(this.label104, resources.GetString("label104.ToolTip"));
            // 
            // label105
            // 
            resources.ApplyResources(this.label105, "label105");
            this.label105.Name = "label105";
            this.toolTip.SetToolTip(this.label105, resources.GetString("label105.ToolTip"));
            // 
            // label106
            // 
            resources.ApplyResources(this.label106, "label106");
            this.label106.Name = "label106";
            this.toolTip.SetToolTip(this.label106, resources.GetString("label106.ToolTip"));
            // 
            // label107
            // 
            resources.ApplyResources(this.label107, "label107");
            this.label107.Name = "label107";
            this.toolTip.SetToolTip(this.label107, resources.GetString("label107.ToolTip"));
            // 
            // label108
            // 
            resources.ApplyResources(this.label108, "label108");
            this.label108.Name = "label108";
            this.toolTip.SetToolTip(this.label108, resources.GetString("label108.ToolTip"));
            // 
            // numericalTextBoxStress33
            // 
            resources.ApplyResources(this.numericalTextBoxStress33, "numericalTextBoxStress33");
            this.numericalTextBoxStress33.AllowMouseControl = false;
            this.numericalTextBoxStress33.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress33.DecimalPlaces = -1;
            this.numericalTextBoxStress33.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress33.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress33.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress33.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress33.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStress33.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStress33.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStress33.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStress33.MouseSpeed = 1D;
            this.numericalTextBoxStress33.Multiline = false;
            this.numericalTextBoxStress33.Name = "numericalTextBoxStress33";
            this.numericalTextBoxStress33.RadianValue = 0D;
            this.numericalTextBoxStress33.ReadOnly = false;
            this.numericalTextBoxStress33.RestrictLimitValue = false;
            this.numericalTextBoxStress33.ShowFraction = false;
            this.numericalTextBoxStress33.ShowPositiveSign = false;
            this.numericalTextBoxStress33.ShowUpDown = false;
            this.numericalTextBoxStress33.SkipEventDuringInput = false;
            this.numericalTextBoxStress33.SmartIncrement = true;
            this.numericalTextBoxStress33.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStress33.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStress33.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStress33.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStress33, resources.GetString("numericalTextBoxStress33.ToolTip1"));
            this.numericalTextBoxStress33.UpDown_Increment = 1D;
            this.numericalTextBoxStress33.Value = 0D;
            this.numericalTextBoxStress33.WordWrap = true;
            // 
            // numericalTextBoxStress22
            // 
            resources.ApplyResources(this.numericalTextBoxStress22, "numericalTextBoxStress22");
            this.numericalTextBoxStress22.AllowMouseControl = false;
            this.numericalTextBoxStress22.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress22.DecimalPlaces = -1;
            this.numericalTextBoxStress22.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress22.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress22.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress22.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress22.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStress22.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStress22.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStress22.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStress22.MouseSpeed = 1D;
            this.numericalTextBoxStress22.Multiline = false;
            this.numericalTextBoxStress22.Name = "numericalTextBoxStress22";
            this.numericalTextBoxStress22.RadianValue = 0D;
            this.numericalTextBoxStress22.ReadOnly = false;
            this.numericalTextBoxStress22.RestrictLimitValue = false;
            this.numericalTextBoxStress22.ShowFraction = false;
            this.numericalTextBoxStress22.ShowPositiveSign = false;
            this.numericalTextBoxStress22.ShowUpDown = false;
            this.numericalTextBoxStress22.SkipEventDuringInput = false;
            this.numericalTextBoxStress22.SmartIncrement = true;
            this.numericalTextBoxStress22.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStress22.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStress22.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStress22.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStress22, resources.GetString("numericalTextBoxStress22.ToolTip1"));
            this.numericalTextBoxStress22.UpDown_Increment = 1D;
            this.numericalTextBoxStress22.Value = 0D;
            this.numericalTextBoxStress22.WordWrap = true;
            // 
            // numericalTextBoxStress11
            // 
            resources.ApplyResources(this.numericalTextBoxStress11, "numericalTextBoxStress11");
            this.numericalTextBoxStress11.AllowMouseControl = false;
            this.numericalTextBoxStress11.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress11.DecimalPlaces = -1;
            this.numericalTextBoxStress11.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress11.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress11.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress11.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress11.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStress11.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStress11.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStress11.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStress11.MouseSpeed = 1D;
            this.numericalTextBoxStress11.Multiline = false;
            this.numericalTextBoxStress11.Name = "numericalTextBoxStress11";
            this.numericalTextBoxStress11.RadianValue = 0D;
            this.numericalTextBoxStress11.ReadOnly = false;
            this.numericalTextBoxStress11.RestrictLimitValue = false;
            this.numericalTextBoxStress11.ShowFraction = false;
            this.numericalTextBoxStress11.ShowPositiveSign = false;
            this.numericalTextBoxStress11.ShowUpDown = false;
            this.numericalTextBoxStress11.SkipEventDuringInput = false;
            this.numericalTextBoxStress11.SmartIncrement = true;
            this.numericalTextBoxStress11.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStress11.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStress11.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStress11.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStress11, resources.GetString("numericalTextBoxStress11.ToolTip1"));
            this.numericalTextBoxStress11.UpDown_Increment = 1D;
            this.numericalTextBoxStress11.Value = 0D;
            this.numericalTextBoxStress11.WordWrap = true;
            // 
            // numericalTextBoxStress23
            // 
            resources.ApplyResources(this.numericalTextBoxStress23, "numericalTextBoxStress23");
            this.numericalTextBoxStress23.AllowMouseControl = false;
            this.numericalTextBoxStress23.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress23.DecimalPlaces = -1;
            this.numericalTextBoxStress23.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress23.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress23.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress23.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress23.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStress23.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStress23.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStress23.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStress23.MouseSpeed = 1D;
            this.numericalTextBoxStress23.Multiline = false;
            this.numericalTextBoxStress23.Name = "numericalTextBoxStress23";
            this.numericalTextBoxStress23.RadianValue = 0D;
            this.numericalTextBoxStress23.ReadOnly = false;
            this.numericalTextBoxStress23.RestrictLimitValue = false;
            this.numericalTextBoxStress23.ShowFraction = false;
            this.numericalTextBoxStress23.ShowPositiveSign = false;
            this.numericalTextBoxStress23.ShowUpDown = false;
            this.numericalTextBoxStress23.SkipEventDuringInput = false;
            this.numericalTextBoxStress23.SmartIncrement = true;
            this.numericalTextBoxStress23.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStress23.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStress23.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStress23.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStress23, resources.GetString("numericalTextBoxStress23.ToolTip1"));
            this.numericalTextBoxStress23.UpDown_Increment = 1D;
            this.numericalTextBoxStress23.Value = 0D;
            this.numericalTextBoxStress23.WordWrap = true;
            // 
            // numericalTextBoxStress13
            // 
            resources.ApplyResources(this.numericalTextBoxStress13, "numericalTextBoxStress13");
            this.numericalTextBoxStress13.AllowMouseControl = false;
            this.numericalTextBoxStress13.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress13.DecimalPlaces = -1;
            this.numericalTextBoxStress13.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress13.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress13.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress13.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress13.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStress13.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStress13.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStress13.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStress13.MouseSpeed = 1D;
            this.numericalTextBoxStress13.Multiline = false;
            this.numericalTextBoxStress13.Name = "numericalTextBoxStress13";
            this.numericalTextBoxStress13.RadianValue = 0D;
            this.numericalTextBoxStress13.ReadOnly = false;
            this.numericalTextBoxStress13.RestrictLimitValue = false;
            this.numericalTextBoxStress13.ShowFraction = false;
            this.numericalTextBoxStress13.ShowPositiveSign = false;
            this.numericalTextBoxStress13.ShowUpDown = false;
            this.numericalTextBoxStress13.SkipEventDuringInput = false;
            this.numericalTextBoxStress13.SmartIncrement = true;
            this.numericalTextBoxStress13.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStress13.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStress13.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStress13.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStress13, resources.GetString("numericalTextBoxStress13.ToolTip1"));
            this.numericalTextBoxStress13.UpDown_Increment = 1D;
            this.numericalTextBoxStress13.Value = 0D;
            this.numericalTextBoxStress13.WordWrap = true;
            // 
            // numericalTextBoxStress12
            // 
            resources.ApplyResources(this.numericalTextBoxStress12, "numericalTextBoxStress12");
            this.numericalTextBoxStress12.AllowMouseControl = false;
            this.numericalTextBoxStress12.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress12.DecimalPlaces = -1;
            this.numericalTextBoxStress12.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress12.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress12.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress12.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStress12.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStress12.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStress12.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStress12.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStress12.MouseSpeed = 1D;
            this.numericalTextBoxStress12.Multiline = false;
            this.numericalTextBoxStress12.Name = "numericalTextBoxStress12";
            this.numericalTextBoxStress12.RadianValue = 0D;
            this.numericalTextBoxStress12.ReadOnly = false;
            this.numericalTextBoxStress12.RestrictLimitValue = false;
            this.numericalTextBoxStress12.ShowFraction = false;
            this.numericalTextBoxStress12.ShowPositiveSign = false;
            this.numericalTextBoxStress12.ShowUpDown = false;
            this.numericalTextBoxStress12.SkipEventDuringInput = false;
            this.numericalTextBoxStress12.SmartIncrement = true;
            this.numericalTextBoxStress12.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStress12.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStress12.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStress12.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStress12, resources.GetString("numericalTextBoxStress12.ToolTip1"));
            this.numericalTextBoxStress12.UpDown_Increment = 1D;
            this.numericalTextBoxStress12.Value = 0D;
            this.numericalTextBoxStress12.WordWrap = true;
            // 
            // numericalTextBoxStrain33
            // 
            resources.ApplyResources(this.numericalTextBoxStrain33, "numericalTextBoxStrain33");
            this.numericalTextBoxStrain33.AllowMouseControl = false;
            this.numericalTextBoxStrain33.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain33.DecimalPlaces = -1;
            this.numericalTextBoxStrain33.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain33.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain33.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain33.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain33.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStrain33.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStrain33.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStrain33.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStrain33.MouseSpeed = 1D;
            this.numericalTextBoxStrain33.Multiline = false;
            this.numericalTextBoxStrain33.Name = "numericalTextBoxStrain33";
            this.numericalTextBoxStrain33.RadianValue = 0D;
            this.numericalTextBoxStrain33.ReadOnly = false;
            this.numericalTextBoxStrain33.RestrictLimitValue = false;
            this.numericalTextBoxStrain33.ShowFraction = false;
            this.numericalTextBoxStrain33.ShowPositiveSign = false;
            this.numericalTextBoxStrain33.ShowUpDown = false;
            this.numericalTextBoxStrain33.SkipEventDuringInput = false;
            this.numericalTextBoxStrain33.SmartIncrement = true;
            this.numericalTextBoxStrain33.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStrain33.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStrain33.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStrain33.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStrain33, resources.GetString("numericalTextBoxStrain33.ToolTip1"));
            this.numericalTextBoxStrain33.UpDown_Increment = 1D;
            this.numericalTextBoxStrain33.Value = 0D;
            this.numericalTextBoxStrain33.WordWrap = true;
            // 
            // numericalTextBoxStrain11
            // 
            resources.ApplyResources(this.numericalTextBoxStrain11, "numericalTextBoxStrain11");
            this.numericalTextBoxStrain11.AllowMouseControl = false;
            this.numericalTextBoxStrain11.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain11.DecimalPlaces = -1;
            this.numericalTextBoxStrain11.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain11.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain11.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain11.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain11.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStrain11.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStrain11.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStrain11.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStrain11.MouseSpeed = 1D;
            this.numericalTextBoxStrain11.Multiline = false;
            this.numericalTextBoxStrain11.Name = "numericalTextBoxStrain11";
            this.numericalTextBoxStrain11.RadianValue = 0D;
            this.numericalTextBoxStrain11.ReadOnly = false;
            this.numericalTextBoxStrain11.RestrictLimitValue = false;
            this.numericalTextBoxStrain11.ShowFraction = false;
            this.numericalTextBoxStrain11.ShowPositiveSign = false;
            this.numericalTextBoxStrain11.ShowUpDown = false;
            this.numericalTextBoxStrain11.SkipEventDuringInput = false;
            this.numericalTextBoxStrain11.SmartIncrement = true;
            this.numericalTextBoxStrain11.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStrain11.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStrain11.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStrain11.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStrain11, resources.GetString("numericalTextBoxStrain11.ToolTip1"));
            this.numericalTextBoxStrain11.UpDown_Increment = 1D;
            this.numericalTextBoxStrain11.Value = 0D;
            this.numericalTextBoxStrain11.WordWrap = true;
            // 
            // numericalTextBoxStrain22
            // 
            resources.ApplyResources(this.numericalTextBoxStrain22, "numericalTextBoxStrain22");
            this.numericalTextBoxStrain22.AllowMouseControl = false;
            this.numericalTextBoxStrain22.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain22.DecimalPlaces = -1;
            this.numericalTextBoxStrain22.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain22.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain22.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain22.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain22.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStrain22.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStrain22.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStrain22.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStrain22.MouseSpeed = 1D;
            this.numericalTextBoxStrain22.Multiline = false;
            this.numericalTextBoxStrain22.Name = "numericalTextBoxStrain22";
            this.numericalTextBoxStrain22.RadianValue = 0D;
            this.numericalTextBoxStrain22.ReadOnly = false;
            this.numericalTextBoxStrain22.RestrictLimitValue = false;
            this.numericalTextBoxStrain22.ShowFraction = false;
            this.numericalTextBoxStrain22.ShowPositiveSign = false;
            this.numericalTextBoxStrain22.ShowUpDown = false;
            this.numericalTextBoxStrain22.SkipEventDuringInput = false;
            this.numericalTextBoxStrain22.SmartIncrement = true;
            this.numericalTextBoxStrain22.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStrain22.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStrain22.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStrain22.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStrain22, resources.GetString("numericalTextBoxStrain22.ToolTip1"));
            this.numericalTextBoxStrain22.UpDown_Increment = 1D;
            this.numericalTextBoxStrain22.Value = 0D;
            this.numericalTextBoxStrain22.WordWrap = true;
            // 
            // numericalTextBoxStrain12
            // 
            resources.ApplyResources(this.numericalTextBoxStrain12, "numericalTextBoxStrain12");
            this.numericalTextBoxStrain12.AllowMouseControl = false;
            this.numericalTextBoxStrain12.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain12.DecimalPlaces = -1;
            this.numericalTextBoxStrain12.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain12.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain12.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain12.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain12.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStrain12.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStrain12.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStrain12.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStrain12.MouseSpeed = 1D;
            this.numericalTextBoxStrain12.Multiline = false;
            this.numericalTextBoxStrain12.Name = "numericalTextBoxStrain12";
            this.numericalTextBoxStrain12.RadianValue = 0D;
            this.numericalTextBoxStrain12.ReadOnly = false;
            this.numericalTextBoxStrain12.RestrictLimitValue = false;
            this.numericalTextBoxStrain12.ShowFraction = false;
            this.numericalTextBoxStrain12.ShowPositiveSign = false;
            this.numericalTextBoxStrain12.ShowUpDown = false;
            this.numericalTextBoxStrain12.SkipEventDuringInput = false;
            this.numericalTextBoxStrain12.SmartIncrement = true;
            this.numericalTextBoxStrain12.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStrain12.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStrain12.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStrain12.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStrain12, resources.GetString("numericalTextBoxStrain12.ToolTip1"));
            this.numericalTextBoxStrain12.UpDown_Increment = 1D;
            this.numericalTextBoxStrain12.Value = 0D;
            this.numericalTextBoxStrain12.WordWrap = true;
            // 
            // numericalTextBoxStrain23
            // 
            resources.ApplyResources(this.numericalTextBoxStrain23, "numericalTextBoxStrain23");
            this.numericalTextBoxStrain23.AllowMouseControl = false;
            this.numericalTextBoxStrain23.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain23.DecimalPlaces = -1;
            this.numericalTextBoxStrain23.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain23.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain23.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain23.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain23.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStrain23.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStrain23.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStrain23.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStrain23.MouseSpeed = 1D;
            this.numericalTextBoxStrain23.Multiline = false;
            this.numericalTextBoxStrain23.Name = "numericalTextBoxStrain23";
            this.numericalTextBoxStrain23.RadianValue = 0D;
            this.numericalTextBoxStrain23.ReadOnly = false;
            this.numericalTextBoxStrain23.RestrictLimitValue = false;
            this.numericalTextBoxStrain23.ShowFraction = false;
            this.numericalTextBoxStrain23.ShowPositiveSign = false;
            this.numericalTextBoxStrain23.ShowUpDown = false;
            this.numericalTextBoxStrain23.SkipEventDuringInput = false;
            this.numericalTextBoxStrain23.SmartIncrement = true;
            this.numericalTextBoxStrain23.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStrain23.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStrain23.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStrain23.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStrain23, resources.GetString("numericalTextBoxStrain23.ToolTip1"));
            this.numericalTextBoxStrain23.UpDown_Increment = 1D;
            this.numericalTextBoxStrain23.Value = 0D;
            this.numericalTextBoxStrain23.WordWrap = true;
            // 
            // numericalTextBoxStrain13
            // 
            resources.ApplyResources(this.numericalTextBoxStrain13, "numericalTextBoxStrain13");
            this.numericalTextBoxStrain13.AllowMouseControl = false;
            this.numericalTextBoxStrain13.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain13.DecimalPlaces = -1;
            this.numericalTextBoxStrain13.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain13.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain13.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain13.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericalTextBoxStrain13.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericalTextBoxStrain13.Maximum = double.PositiveInfinity;
            this.numericalTextBoxStrain13.Minimum = double.NegativeInfinity;
            this.numericalTextBoxStrain13.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxStrain13.MouseSpeed = 1D;
            this.numericalTextBoxStrain13.Multiline = false;
            this.numericalTextBoxStrain13.Name = "numericalTextBoxStrain13";
            this.numericalTextBoxStrain13.RadianValue = 0D;
            this.numericalTextBoxStrain13.ReadOnly = false;
            this.numericalTextBoxStrain13.RestrictLimitValue = false;
            this.numericalTextBoxStrain13.ShowFraction = false;
            this.numericalTextBoxStrain13.ShowPositiveSign = false;
            this.numericalTextBoxStrain13.ShowUpDown = false;
            this.numericalTextBoxStrain13.SkipEventDuringInput = false;
            this.numericalTextBoxStrain13.SmartIncrement = true;
            this.numericalTextBoxStrain13.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxStrain13.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxStrain13.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxStrain13.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxStrain13, resources.GetString("numericalTextBoxStrain13.ToolTip1"));
            this.numericalTextBoxStrain13.UpDown_Increment = 1D;
            this.numericalTextBoxStrain13.Value = 0D;
            this.numericalTextBoxStrain13.WordWrap = true;
            // 
            // tabPagePolycrystalline
            // 
            resources.ApplyResources(this.tabPagePolycrystalline, "tabPagePolycrystalline");
            this.tabPagePolycrystalline.BackColor = System.Drawing.SystemColors.Control;
            this.tabPagePolycrystalline.ContextMenuStrip = this.contextMenuStripPoleFigure;
            this.tabPagePolycrystalline.Controls.Add(this.poleFigureControl);
            this.tabPagePolycrystalline.Controls.Add(this.flowLayoutPanel3);
            this.tabPagePolycrystalline.Controls.Add(this.flowLayoutPanel2);
            this.tabPagePolycrystalline.Name = "tabPagePolycrystalline";
            this.toolTip.SetToolTip(this.tabPagePolycrystalline, resources.GetString("tabPagePolycrystalline.ToolTip"));
            // 
            // contextMenuStripPoleFigure
            // 
            resources.ApplyResources(this.contextMenuStripPoleFigure, "contextMenuStripPoleFigure");
            this.contextMenuStripPoleFigure.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.contextMenuStripPoleFigure.Name = "contextMenuStripPoleFigure";
            this.toolTip.SetToolTip(this.contextMenuStripPoleFigure, resources.GetString("contextMenuStripPoleFigure.ToolTip"));
            // 
            // readToolStripMenuItem
            // 
            resources.ApplyResources(this.readToolStripMenuItem, "readToolStripMenuItem");
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Click += new System.EventHandler(this.readToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem,
            this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem,
            this.asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            // 
            // asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem
            // 
            resources.ApplyResources(this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem, "asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem");
            this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem.Name = "asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem";
            this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem.Click += new System.EventHandler(this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem_Click);
            // 
            // asTXTFileAllEulerAngleAndDensityToolStripMenuItem
            // 
            resources.ApplyResources(this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem, "asTXTFileAllEulerAngleAndDensityToolStripMenuItem");
            this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem.Name = "asTXTFileAllEulerAngleAndDensityToolStripMenuItem";
            this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem.Click += new System.EventHandler(this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem_Click);
            // 
            // asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem
            // 
            resources.ApplyResources(this.asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem, "asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem");
            this.asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem.Name = "asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem";
            this.asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem.Click += new System.EventHandler(this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem_Click);
            // 
            // poleFigureControl
            // 
            resources.ApplyResources(this.poleFigureControl, "poleFigureControl");
            this.poleFigureControl.Crystal = null;
            this.poleFigureControl.Name = "poleFigureControl";
            this.toolTip.SetToolTip(this.poleFigureControl, resources.GetString("poleFigureControl.ToolTip"));
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Controls.Add(this.buttonGenerateRandomOrientations);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.toolTip.SetToolTip(this.flowLayoutPanel3, resources.GetString("flowLayoutPanel3.ToolTip"));
            // 
            // buttonGenerateRandomOrientations
            // 
            resources.ApplyResources(this.buttonGenerateRandomOrientations, "buttonGenerateRandomOrientations");
            this.buttonGenerateRandomOrientations.Name = "buttonGenerateRandomOrientations";
            this.toolTip.SetToolTip(this.buttonGenerateRandomOrientations, resources.GetString("buttonGenerateRandomOrientations.ToolTip"));
            this.buttonGenerateRandomOrientations.UseVisualStyleBackColor = true;
            this.buttonGenerateRandomOrientations.Click += new System.EventHandler(this.buttonGenerateRandomOrientations_Click);
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Controls.Add(this.label5);
            this.flowLayoutPanel2.Controls.Add(this.numericUpDownAngleResolution);
            this.flowLayoutPanel2.Controls.Add(this.label29);
            this.flowLayoutPanel2.Controls.Add(this.numericUpDownAngleSubDivision);
            this.flowLayoutPanel2.Controls.Add(this.label101);
            this.flowLayoutPanel2.Controls.Add(this.numericUpDownCrystallineSize);
            this.flowLayoutPanel2.Controls.Add(this.label99);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.toolTip.SetToolTip(this.flowLayoutPanel2, resources.GetString("flowLayoutPanel2.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // numericUpDownAngleResolution
            // 
            resources.ApplyResources(this.numericUpDownAngleResolution, "numericUpDownAngleResolution");
            this.numericUpDownAngleResolution.DecimalPlaces = 1;
            this.numericUpDownAngleResolution.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownAngleResolution.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.numericUpDownAngleResolution.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownAngleResolution.Name = "numericUpDownAngleResolution";
            this.toolTip.SetToolTip(this.numericUpDownAngleResolution, resources.GetString("numericUpDownAngleResolution.ToolTip"));
            this.numericUpDownAngleResolution.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownAngleResolution.ValueChanged += new System.EventHandler(this.numericUpDownAngleResolution_ValueChanged);
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            this.toolTip.SetToolTip(this.label29, resources.GetString("label29.ToolTip"));
            // 
            // numericUpDownAngleSubDivision
            // 
            resources.ApplyResources(this.numericUpDownAngleSubDivision, "numericUpDownAngleSubDivision");
            this.numericUpDownAngleSubDivision.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.numericUpDownAngleSubDivision.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAngleSubDivision.Name = "numericUpDownAngleSubDivision";
            this.toolTip.SetToolTip(this.numericUpDownAngleSubDivision, resources.GetString("numericUpDownAngleSubDivision.ToolTip"));
            this.numericUpDownAngleSubDivision.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownAngleSubDivision.ValueChanged += new System.EventHandler(this.numericUpDownAngleResolution_ValueChanged);
            // 
            // label101
            // 
            resources.ApplyResources(this.label101, "label101");
            this.label101.Name = "label101";
            this.toolTip.SetToolTip(this.label101, resources.GetString("label101.ToolTip"));
            // 
            // numericUpDownCrystallineSize
            // 
            resources.ApplyResources(this.numericUpDownCrystallineSize, "numericUpDownCrystallineSize");
            this.numericUpDownCrystallineSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCrystallineSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownCrystallineSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCrystallineSize.Name = "numericUpDownCrystallineSize";
            this.toolTip.SetToolTip(this.numericUpDownCrystallineSize, resources.GetString("numericUpDownCrystallineSize.ToolTip"));
            this.numericUpDownCrystallineSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownCrystallineSize.ValueChanged += new System.EventHandler(this.numericUpDownAngleResolution_ValueChanged);
            // 
            // label99
            // 
            resources.ApplyResources(this.label99, "label99");
            this.label99.Name = "label99";
            this.toolTip.SetToolTip(this.label99, resources.GetString("label99.ToolTip"));
            // 
            // tabPageBounds
            // 
            resources.ApplyResources(this.tabPageBounds, "tabPageBounds");
            this.tabPageBounds.Controls.Add(this.boundControl);
            this.tabPageBounds.Name = "tabPageBounds";
            this.toolTip.SetToolTip(this.tabPageBounds, resources.GetString("tabPageBounds.ToolTip"));
            // 
            // boundControl
            // 
            resources.ApplyResources(this.boundControl, "boundControl");
            this.boundControl.Crystal = null;
            this.boundControl.Name = "boundControl";
            this.boundControl.SkipEvent = false;
            this.toolTip.SetToolTip(this.boundControl, resources.GetString("boundControl.ToolTip"));
            // 
            // tabPageLatticePlane
            // 
            resources.ApplyResources(this.tabPageLatticePlane, "tabPageLatticePlane");
            this.tabPageLatticePlane.Controls.Add(this.latticePlaneControl);
            this.tabPageLatticePlane.Name = "tabPageLatticePlane";
            this.toolTip.SetToolTip(this.tabPageLatticePlane, resources.GetString("tabPageLatticePlane.ToolTip"));
            // 
            // latticePlaneControl
            // 
            resources.ApplyResources(this.latticePlaneControl, "latticePlaneControl");
            this.latticePlaneControl.Crystal = null;
            this.latticePlaneControl.Name = "latticePlaneControl";
            this.latticePlaneControl.SkipEvent = false;
            this.toolTip.SetToolTip(this.latticePlaneControl, resources.GetString("latticePlaneControl.ToolTip"));
            // 
            // contextMenuStrip
            // 
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scatteringFactorToolStripMenuItem,
            this.symmetryInformationToolStripMenuItem,
            this.toolStripSeparator2,
            this.importCrystalFromCIFAMCToolStripMenuItem,
            this.exportThisCrystalAsCIFToolStripMenuItem,
            this.sendThisCrystalToOtherSoftwareToolStripMenuItem,
            this.toolStripSeparator1,
            this.clearAllDataToolStripMenuItem,
            this.revertCellConstantsToolStripMenuItem,
            this.toolStripSeparator3,
            this.strainControlToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.toolTip.SetToolTip(this.contextMenuStrip, resources.GetString("contextMenuStrip.ToolTip"));
            // 
            // scatteringFactorToolStripMenuItem
            // 
            resources.ApplyResources(this.scatteringFactorToolStripMenuItem, "scatteringFactorToolStripMenuItem");
            this.scatteringFactorToolStripMenuItem.Name = "scatteringFactorToolStripMenuItem";
            this.scatteringFactorToolStripMenuItem.Click += new System.EventHandler(this.scatteringFactorToolStripMenuItem_Click);
            // 
            // symmetryInformationToolStripMenuItem
            // 
            resources.ApplyResources(this.symmetryInformationToolStripMenuItem, "symmetryInformationToolStripMenuItem");
            this.symmetryInformationToolStripMenuItem.Name = "symmetryInformationToolStripMenuItem";
            this.symmetryInformationToolStripMenuItem.Click += new System.EventHandler(this.symmetryInformationToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // importCrystalFromCIFAMCToolStripMenuItem
            // 
            resources.ApplyResources(this.importCrystalFromCIFAMCToolStripMenuItem, "importCrystalFromCIFAMCToolStripMenuItem");
            this.importCrystalFromCIFAMCToolStripMenuItem.Name = "importCrystalFromCIFAMCToolStripMenuItem";
            this.importCrystalFromCIFAMCToolStripMenuItem.Click += new System.EventHandler(this.importCrystalFromCIFAMCToolStripMenuItem_Click);
            // 
            // exportThisCrystalAsCIFToolStripMenuItem
            // 
            resources.ApplyResources(this.exportThisCrystalAsCIFToolStripMenuItem, "exportThisCrystalAsCIFToolStripMenuItem");
            this.exportThisCrystalAsCIFToolStripMenuItem.Name = "exportThisCrystalAsCIFToolStripMenuItem";
            this.exportThisCrystalAsCIFToolStripMenuItem.Click += new System.EventHandler(this.exportThisCrystalAsCIFToolStripMenuItem_Click);
            // 
            // sendThisCrystalToOtherSoftwareToolStripMenuItem
            // 
            resources.ApplyResources(this.sendThisCrystalToOtherSoftwareToolStripMenuItem, "sendThisCrystalToOtherSoftwareToolStripMenuItem");
            this.sendThisCrystalToOtherSoftwareToolStripMenuItem.Name = "sendThisCrystalToOtherSoftwareToolStripMenuItem";
            this.sendThisCrystalToOtherSoftwareToolStripMenuItem.Click += new System.EventHandler(this.sendThisCrystalToOtherSoftwareToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // clearAllDataToolStripMenuItem
            // 
            resources.ApplyResources(this.clearAllDataToolStripMenuItem, "clearAllDataToolStripMenuItem");
            this.clearAllDataToolStripMenuItem.Name = "clearAllDataToolStripMenuItem";
            // 
            // revertCellConstantsToolStripMenuItem
            // 
            resources.ApplyResources(this.revertCellConstantsToolStripMenuItem, "revertCellConstantsToolStripMenuItem");
            this.revertCellConstantsToolStripMenuItem.Name = "revertCellConstantsToolStripMenuItem";
            this.revertCellConstantsToolStripMenuItem.Click += new System.EventHandler(this.revertCellConstantsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // strainControlToolStripMenuItem
            // 
            resources.ApplyResources(this.strainControlToolStripMenuItem, "strainControlToolStripMenuItem");
            this.strainControlToolStripMenuItem.Name = "strainControlToolStripMenuItem";
            this.strainControlToolStripMenuItem.Click += new System.EventHandler(this.strainControlToolStripMenuItem_Click);
            // 
            // textBoxFormula
            // 
            resources.ApplyResources(this.textBoxFormula, "textBoxFormula");
            this.textBoxFormula.Name = "textBoxFormula";
            this.textBoxFormula.ReadOnly = true;
            this.toolTip.SetToolTip(this.textBoxFormula, resources.GetString("textBoxFormula.ToolTip"));
            // 
            // label90
            // 
            resources.ApplyResources(this.label90, "label90");
            this.label90.Name = "label90";
            this.toolTip.SetToolTip(this.label90, resources.GetString("label90.ToolTip"));
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            this.toolTip.SetToolTip(this.textBoxName, resources.GetString("textBoxName.ToolTip"));
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            this.toolTip.SetToolTip(this.label22, resources.GetString("label22.ToolTip"));
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // buttonReset
            // 
            resources.ApplyResources(this.buttonReset, "buttonReset");
            this.buttonReset.BackColor = System.Drawing.Color.IndianRed;
            this.buttonReset.ForeColor = System.Drawing.Color.White;
            this.buttonReset.Name = "buttonReset";
            this.toolTip.SetToolTip(this.buttonReset, resources.GetString("buttonReset.ToolTip"));
            this.buttonReset.UseVisualStyleBackColor = false;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.toolTip.SetToolTip(this.flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.textBoxFormula);
            this.panel4.Controls.Add(this.label90);
            this.panel4.Controls.Add(this.textBoxName);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.buttonReset);
            this.panel4.Name = "panel4";
            this.toolTip.SetToolTip(this.panel4, resources.GetString("panel4.ToolTip"));
            // 
            // CrystalControl
            // 
            resources.ApplyResources(this, "$this");
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CrystalControl";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.CrystalForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CrystalControl_KeyDown);
            this.Resize += new System.EventHandler(this.CrystalControl_Resize);
            this.tabControl.ResumeLayout(false);
            this.tabPageBasicInfo.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.tabPageAtom.ResumeLayout(false);
            this.tabPageAtom.PerformLayout();
            this.panelAtom.ResumeLayout(false);
            this.panelAtom.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabPageBondsPolyhedra.ResumeLayout(false);
            this.tabPageBondsPolyhedra.PerformLayout();
            this.tabPageReference.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPageEOS.ResumeLayout(false);
            this.tabPageEOS.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageElasticity.ResumeLayout(false);
            this.tabPageElasticity.PerformLayout();
            this.tabPageStrainStress.ResumeLayout(false);
            this.tabPageStrainStress.PerformLayout();
            this.tabPagePolycrystalline.ResumeLayout(false);
            this.tabPagePolycrystalline.PerformLayout();
            this.contextMenuStripPoleFigure.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleResolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleSubDivision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCrystallineSize)).EndInit();
            this.tabPageBounds.ResumeLayout(false);
            this.tabPageLatticePlane.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabPage tabPageReference;
        public System.Windows.Forms.TextBox textBoxTitle;
        public System.Windows.Forms.TextBox textBoxMemo;
        public System.Windows.Forms.TextBox textBoxAuthor;
        public System.Windows.Forms.TextBox textBoxJournal;
        private System.Windows.Forms.Label label90;
        public System.Windows.Forms.TextBox textBoxFormula;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label22;
        public System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBondsPolyhedra;
        public System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tabPageBasicInfo;
        private System.Windows.Forms.Panel panelAtom;
        private ColorControl colorControl;
        private System.Windows.Forms.ToolStripMenuItem importCrystalFromCIFAMCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendThisCrystalToOtherSoftwareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scatteringFactorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearAllDataToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageEOS;
        private NumericBox numericBoxEOS_KT0;
        private System.Windows.Forms.Label label15;
        private NumericBox numericBoxEOS_KprimeT0;
        private NumericBox numericBoxEOS_T0;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Label label70;
        private NumericBox numericBoxEOS_Theta0;
        private NumericBox numericBoxEOS_V0perCell;
        private System.Windows.Forms.Label label76;
        private NumericBox numericBoxEOS_Gamma0;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label65;
        private NumericBox numericBoxEOS_Q;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.RadioButton radioButtonMieGruneisen;
        private System.Windows.Forms.RadioButton radioButtonTdependenceK0andV0;
        private System.Windows.Forms.Label label3;
        private NumericBox numericBoxEOS_A;
        private NumericBox numericBoxEOS_B;
        private NumericBox numericBoxEOS_KperT;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label64;
        private NumericBox numericBoxEOS_C;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.CheckBox checkBoxUseEOS;
        private System.Windows.Forms.Label label75;
        private NumericBox numericBoxPressure;
        private NumericBox numericalTextBoxTemperature;
        private NumericBox numericBoxVolume;
        private NumericBox numericalTextBoxEOS_V0perMol;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.TextBox textBoxEOS_Note;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RadioButton radioButtonVinet;
        private System.Windows.Forms.RadioButton radioButtonBirchMurnaghan;
        private System.Windows.Forms.ToolStripMenuItem symmetryInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TabPage tabPageElasticity;
        private ElasticityControl elasticityControl1;
        private System.Windows.Forms.ToolStripMenuItem exportThisCrystalAsCIFToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageStrainStress;
        private System.Windows.Forms.TabPage tabPagePolycrystalline;
        private PoleFigureControl poleFigureControl;
        private System.Windows.Forms.NumericUpDown numericUpDownAngleResolution;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.NumericUpDown numericUpDownCrystallineSize;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label102;
        private NumericBox numericalTextBoxStrain33;
        private NumericBox numericalTextBoxStrain11;
        private NumericBox numericalTextBoxStrain22;
        private NumericBox numericalTextBoxStrain12;
        private System.Windows.Forms.Label label103;
        private NumericBox numericalTextBoxStrain23;
        private System.Windows.Forms.Label label104;
        private NumericBox numericalTextBoxStrain13;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label label108;
        private NumericBox numericalTextBoxStress33;
        private System.Windows.Forms.Label label109;
        private NumericBox numericalTextBoxStress22;
        private NumericBox numericalTextBoxStress11;
        private NumericBox numericalTextBoxStress23;
        private NumericBox numericalTextBoxStress13;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label label111;
        private NumericBox numericalTextBoxStress12;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.Label label115;
        private NumericBox numericalTextBoxHill;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Button buttonGenerateRandomOrientations;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numericUpDownAngleSubDivision;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPoleFigure;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asTXTFileAllEulerAngleAndDensityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ToolStripMenuItem revertCellConstantsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem strainControlToolStripMenuItem;
        public BondInputControl bondControl;
        public AtomControl atomControl;
        public System.Windows.Forms.TabPage tabPageAtom;
        private System.Windows.Forms.TabPage tabPageBounds;
        private System.Windows.Forms.TabPage tabPageLatticePlane;
        public LatticePlaneControl latticePlaneControl;
        public BoundControl boundControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private NumericBox numericBoxZnumber;
        private NumericBox numericBoxDensity;
        public SymmetryControl symmetryControl;
    }
}