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
            this.numericBoxVolume = new Crystallography.Controls.NumericBox();
            this.numericBoxCellMass = new Crystallography.Controls.NumericBox();
            this.numericBoxMolarVolume = new Crystallography.Controls.NumericBox();
            this.numericBoxMolarMass = new Crystallography.Controls.NumericBox();
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
            this.eosControl = new Crystallography.Controls.EOSControl();
            this.tabPageElasticity = new System.Windows.Forms.TabPage();
            this.elasticityControl1 = new Crystallography.Controls.ElasticityControl();
            this.tabPageStrainStress = new System.Windows.Forms.TabPage();
            this.buttonStressSet = new System.Windows.Forms.Button();
            this.numericBoxStrain33 = new Crystallography.Controls.NumericBox();
            this.numericBoxHill = new Crystallography.Controls.NumericBox();
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
            this.numericBoxStress33 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress22 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress11 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress23 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress13 = new Crystallography.Controls.NumericBox();
            this.numericBoxStress12 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain11 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain22 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain12 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain23 = new Crystallography.Controls.NumericBox();
            this.numericBoxStrain13 = new Crystallography.Controls.NumericBox();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxFormula = new System.Windows.Forms.TextBox();
            this.numericBoxZnumber = new Crystallography.Controls.NumericBox();
            this.label90 = new System.Windows.Forms.Label();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.scatteringFactorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symmetryInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.importCrystalFromCIFAMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportThisCrystalAsCIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendThisCrystalToOtherSoftwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.revertCellConstantsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.strainControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToP1SymmetryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToSuperstructureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToAnotherSpacegroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonScatteringFactor = new System.Windows.Forms.Button();
            this.buttonSymmetryInfo = new System.Windows.Forms.Button();
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
            this.panel1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
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
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.HotTrack = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragDrop);
            this.tabControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragEnter);
            // 
            // tabPageBasicInfo
            // 
            this.tabPageBasicInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageBasicInfo.Controls.Add(this.panel5);
            resources.ApplyResources(this.tabPageBasicInfo, "tabPageBasicInfo");
            this.tabPageBasicInfo.Name = "tabPageBasicInfo";
            this.toolTip.SetToolTip(this.tabPageBasicInfo, resources.GetString("tabPageBasicInfo.ToolTip"));
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.flowLayoutPanel4);
            this.panel5.Controls.Add(this.symmetryControl);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(this.flowLayoutPanel4, "flowLayoutPanel4");
            this.flowLayoutPanel4.Controls.Add(this.numericBoxVolume);
            this.flowLayoutPanel4.Controls.Add(this.numericBoxCellMass);
            this.flowLayoutPanel4.Controls.Add(this.numericBoxMolarVolume);
            this.flowLayoutPanel4.Controls.Add(this.numericBoxMolarMass);
            this.flowLayoutPanel4.Controls.Add(this.numericBoxDensity);
            this.flowLayoutPanel4.Controls.Add(this.colorControl);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // numericBoxVolume
            // 
            resources.ApplyResources(this.numericBoxVolume, "numericBoxVolume");
            this.numericBoxVolume.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxVolume.DecimalPlaces = 4;
            this.numericBoxVolume.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxVolume.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxVolume.Name = "numericBoxVolume";
            this.numericBoxVolume.ReadOnly = true;
            this.numericBoxVolume.RestrictLimitValue = false;
            this.numericBoxVolume.RoundErrorAccuracy = -1;
            this.numericBoxVolume.SkipEventDuringInput = false;
            this.numericBoxVolume.SmartIncrement = true;
            this.numericBoxVolume.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxVolume.TextFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolTip.SetToolTip(this.numericBoxVolume, resources.GetString("numericBoxVolume.ToolTip"));
            // 
            // numericBoxCellMass
            // 
            resources.ApplyResources(this.numericBoxCellMass, "numericBoxCellMass");
            this.numericBoxCellMass.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellMass.DecimalPlaces = 4;
            this.numericBoxCellMass.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellMass.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellMass.Name = "numericBoxCellMass";
            this.numericBoxCellMass.ReadOnly = true;
            this.numericBoxCellMass.RestrictLimitValue = false;
            this.numericBoxCellMass.RoundErrorAccuracy = -1;
            this.numericBoxCellMass.SkipEventDuringInput = false;
            this.numericBoxCellMass.SmartIncrement = true;
            this.numericBoxCellMass.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellMass.TextFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolTip.SetToolTip(this.numericBoxCellMass, resources.GetString("numericBoxCellMass.ToolTip"));
            // 
            // numericBoxMolarVolume
            // 
            resources.ApplyResources(this.numericBoxMolarVolume, "numericBoxMolarVolume");
            this.numericBoxMolarVolume.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMolarVolume.DecimalPlaces = 4;
            this.numericBoxMolarVolume.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMolarVolume.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMolarVolume.Name = "numericBoxMolarVolume";
            this.numericBoxMolarVolume.ReadOnly = true;
            this.numericBoxMolarVolume.RestrictLimitValue = false;
            this.numericBoxMolarVolume.RoundErrorAccuracy = -1;
            this.numericBoxMolarVolume.SkipEventDuringInput = false;
            this.numericBoxMolarVolume.SmartIncrement = true;
            this.numericBoxMolarVolume.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMolarVolume.TextFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolTip.SetToolTip(this.numericBoxMolarVolume, resources.GetString("numericBoxMolarVolume.ToolTip"));
            // 
            // numericBoxMolarMass
            // 
            resources.ApplyResources(this.numericBoxMolarMass, "numericBoxMolarMass");
            this.numericBoxMolarMass.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMolarMass.DecimalPlaces = 4;
            this.numericBoxMolarMass.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMolarMass.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMolarMass.Name = "numericBoxMolarMass";
            this.numericBoxMolarMass.ReadOnly = true;
            this.numericBoxMolarMass.RestrictLimitValue = false;
            this.numericBoxMolarMass.RoundErrorAccuracy = -1;
            this.numericBoxMolarMass.SkipEventDuringInput = false;
            this.numericBoxMolarMass.SmartIncrement = true;
            this.numericBoxMolarMass.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxMolarMass.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolTip.SetToolTip(this.numericBoxMolarMass, resources.GetString("numericBoxMolarMass.ToolTip"));
            // 
            // numericBoxDensity
            // 
            resources.ApplyResources(this.numericBoxDensity, "numericBoxDensity");
            this.numericBoxDensity.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxDensity.DecimalPlaces = 4;
            this.numericBoxDensity.Name = "numericBoxDensity";
            this.numericBoxDensity.ReadOnly = true;
            this.numericBoxDensity.RoundErrorAccuracy = -1;
            this.numericBoxDensity.SkipEventDuringInput = false;
            this.numericBoxDensity.SmartIncrement = true;
            this.numericBoxDensity.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDensity.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDensity.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxDensity, resources.GetString("numericBoxDensity.ToolTip"));
            // 
            // colorControl
            // 
            this.colorControl.Argb = -986896;
            resources.ApplyResources(this.colorControl, "colorControl");
            this.colorControl.Blue = 240;
            this.colorControl.BlueF = 0.9411765F;
            this.colorControl.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControl.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControl.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControl.Green = 240;
            this.colorControl.GreenF = 0.9411765F;
            this.colorControl.Name = "colorControl";
            this.colorControl.Red = 240;
            this.colorControl.RedF = 0.9411765F;
            this.toolTip.SetToolTip(this.colorControl, resources.GetString("colorControl.ToolTip1"));
            // 
            // symmetryControl
            // 
            this.symmetryControl.A = 0D;
            this.symmetryControl.Alpha = 0D;
            this.symmetryControl.B = 0D;
            this.symmetryControl.Beta = 0D;
            this.symmetryControl.C = 0D;
            resources.ApplyResources(this.symmetryControl, "symmetryControl");
            this.symmetryControl.Gamma = 0D;
            this.symmetryControl.Name = "symmetryControl";
            this.symmetryControl.ShowError = false;
            this.symmetryControl.SkipEvent = false;
            this.symmetryControl.SymmetrySeriesNumber = 0;
            this.symmetryControl.ItemChanged += new System.EventHandler(this.symmetryControl_ItemChanged);
            // 
            // tabPageAtom
            // 
            this.tabPageAtom.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAtom.Controls.Add(this.atomControl);
            this.tabPageAtom.Controls.Add(this.panelAtom);
            resources.ApplyResources(this.tabPageAtom, "tabPageAtom");
            this.tabPageAtom.Name = "tabPageAtom";
            this.toolTip.SetToolTip(this.tabPageAtom, resources.GetString("tabPageAtom.ToolTip"));
            // 
            // atomControl
            // 
            this.atomControl.Alpha = 0F;
            this.atomControl.Ambient = 0F;
            this.atomControl.Aniso11 = 0D;
            this.atomControl.Aniso11Err = 0D;
            this.atomControl.Aniso12 = 0D;
            this.atomControl.Aniso12Err = 0D;
            this.atomControl.Aniso13 = 0D;
            this.atomControl.Aniso13Err = 0D;
            this.atomControl.Aniso22 = 0D;
            this.atomControl.Aniso22Err = 0D;
            this.atomControl.Aniso23 = 0D;
            this.atomControl.Aniso23Err = 0D;
            this.atomControl.Aniso33 = 0D;
            this.atomControl.Aniso33Err = 0D;
            this.atomControl.AppearanceTabVisible = false;
            this.atomControl.AtomColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.atomControl.AtomicPositionError = false;
            this.atomControl.AtomNo = 0;
            this.atomControl.AtomSubNoElectron = -1;
            this.atomControl.AtomSubNoXray = -1;
            resources.ApplyResources(this.atomControl, "atomControl");
            this.atomControl.Crystal = null;
            this.atomControl.DebyeWallerError = false;
            this.atomControl.DebyeWallerTabVisible = true;
            this.atomControl.Diffusion = 0F;
            this.atomControl.ElementAndPositionTabVisible = true;
            this.atomControl.Emission = 0F;
            this.atomControl.Iso = 0D;
            this.atomControl.IsoErr = 0D;
            this.atomControl.IsotopicComposition = null;
            this.atomControl.Label = "";
            this.atomControl.Name = "atomControl";
            this.atomControl.Occ = 0D;
            this.atomControl.OccErr = 0D;
            this.atomControl.OriginShiftVisible = true;
            this.atomControl.Radius = 0D;
            this.atomControl.ScatteringFactorTabVisible = true;
            this.atomControl.SelectedTabIndex = 0;
            this.atomControl.Shininess = 0F;
            this.atomControl.ShowLabel = false;
            this.atomControl.SkipEvent = false;
            this.atomControl.Specular = 0F;
            this.atomControl.UseIsotropy = false;
            this.atomControl.UseTypeU = false;
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
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.ContextMenuStrip = this.contextMenuStrip2;
            this.panel3.Name = "panel3";
            // 
            // contextMenuStrip2
            // 
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            resources.ApplyResources(this.resetToolStripMenuItem, "resetToolStripMenuItem");
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // tabPageBondsPolyhedra
            // 
            this.tabPageBondsPolyhedra.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageBondsPolyhedra.Controls.Add(this.bondControl);
            resources.ApplyResources(this.tabPageBondsPolyhedra, "tabPageBondsPolyhedra");
            this.tabPageBondsPolyhedra.Name = "tabPageBondsPolyhedra";
            // 
            // bondControl
            // 
            resources.ApplyResources(this.bondControl, "bondControl");
            this.bondControl.Crystal = null;
            this.bondControl.ElementList = null;
            this.bondControl.Name = "bondControl";
            this.bondControl.SkipEvent = false;
            // 
            // tabPageReference
            // 
            this.tabPageReference.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageReference.Controls.Add(this.groupBox8);
            this.tabPageReference.Controls.Add(this.groupBox6);
            this.tabPageReference.Controls.Add(this.groupBox7);
            this.tabPageReference.Controls.Add(this.groupBox5);
            resources.ApplyResources(this.tabPageReference, "tabPageReference");
            this.tabPageReference.Name = "tabPageReference";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.textBoxTitle);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxTitle, "textBoxTitle");
            this.textBoxTitle.Name = "textBoxTitle";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBoxAuthor);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // textBoxAuthor
            // 
            this.textBoxAuthor.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxAuthor, "textBoxAuthor");
            this.textBoxAuthor.Name = "textBoxAuthor";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.textBoxJournal);
            resources.ApplyResources(this.groupBox7, "groupBox7");
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.TabStop = false;
            // 
            // textBoxJournal
            // 
            this.textBoxJournal.AcceptsReturn = true;
            resources.ApplyResources(this.textBoxJournal, "textBoxJournal");
            this.textBoxJournal.Name = "textBoxJournal";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxMemo);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // textBoxMemo
            // 
            resources.ApplyResources(this.textBoxMemo, "textBoxMemo");
            this.textBoxMemo.Name = "textBoxMemo";
            // 
            // tabPageEOS
            // 
            this.tabPageEOS.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageEOS.Controls.Add(this.eosControl);
            resources.ApplyResources(this.tabPageEOS, "tabPageEOS");
            this.tabPageEOS.Name = "tabPageEOS";
            // 
            // eosControl
            // 
            this.eosControl.Crystal = null;
            resources.ApplyResources(this.eosControl, "eosControl");
            this.eosControl.Name = "eosControl";
            this.eosControl.SkipEvent = false;
            // 
            // tabPageElasticity
            // 
            this.tabPageElasticity.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageElasticity.Controls.Add(this.elasticityControl1);
            resources.ApplyResources(this.tabPageElasticity, "tabPageElasticity");
            this.tabPageElasticity.Name = "tabPageElasticity";
            // 
            // elasticityControl1
            // 
            resources.ApplyResources(this.elasticityControl1, "elasticityControl1");
            this.elasticityControl1.Mode = Crystallography.Elasticity.Mode.Stiffness;
            this.elasticityControl1.Name = "elasticityControl1";
            this.elasticityControl1.SymmetrySeriesNumber = 1;
            this.elasticityControl1.ValueChanged += new Crystallography.Controls.ElasticityControl.MyEventHandler(this.elasticityControl1_ValueChanged);
            // 
            // tabPageStrainStress
            // 
            this.tabPageStrainStress.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageStrainStress.Controls.Add(this.buttonStressSet);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStrain33);
            this.tabPageStrainStress.Controls.Add(this.numericBoxHill);
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
            this.tabPageStrainStress.Controls.Add(this.numericBoxStress33);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStress22);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStress11);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStress23);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStress13);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStress12);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStrain11);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStrain22);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStrain12);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStrain23);
            this.tabPageStrainStress.Controls.Add(this.numericBoxStrain13);
            resources.ApplyResources(this.tabPageStrainStress, "tabPageStrainStress");
            this.tabPageStrainStress.Name = "tabPageStrainStress";
            // 
            // buttonStressSet
            // 
            resources.ApplyResources(this.buttonStressSet, "buttonStressSet");
            this.buttonStressSet.Name = "buttonStressSet";
            this.buttonStressSet.UseVisualStyleBackColor = true;
            this.buttonStressSet.Click += new System.EventHandler(this.buttonStressSet_Click);
            // 
            // numericBoxStrain33
            // 
            resources.ApplyResources(this.numericBoxStrain33, "numericBoxStrain33");
            this.numericBoxStrain33.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain33.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain33.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain33.Name = "numericBoxStrain33";
            this.numericBoxStrain33.RestrictLimitValue = false;
            this.numericBoxStrain33.RoundErrorAccuracy = -1;
            this.numericBoxStrain33.SkipEventDuringInput = false;
            this.numericBoxStrain33.SmartIncrement = true;
            this.numericBoxStrain33.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxHill
            // 
            resources.ApplyResources(this.numericBoxHill, "numericBoxHill");
            this.numericBoxHill.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxHill.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxHill.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxHill.Name = "numericBoxHill";
            this.numericBoxHill.RadianValue = 0.017453292519943295D;
            this.numericBoxHill.RestrictLimitValue = false;
            this.numericBoxHill.RoundErrorAccuracy = -1;
            this.numericBoxHill.SkipEventDuringInput = false;
            this.numericBoxHill.SmartIncrement = true;
            this.numericBoxHill.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxHill.Value = 1D;
            // 
            // label116
            // 
            resources.ApplyResources(this.label116, "label116");
            this.label116.Name = "label116";
            // 
            // label117
            // 
            resources.ApplyResources(this.label117, "label117");
            this.label117.Name = "label117";
            // 
            // label109
            // 
            resources.ApplyResources(this.label109, "label109");
            this.label109.Name = "label109";
            // 
            // label110
            // 
            resources.ApplyResources(this.label110, "label110");
            this.label110.Name = "label110";
            // 
            // label111
            // 
            resources.ApplyResources(this.label111, "label111");
            this.label111.Name = "label111";
            // 
            // label112
            // 
            resources.ApplyResources(this.label112, "label112");
            this.label112.Name = "label112";
            // 
            // label113
            // 
            resources.ApplyResources(this.label113, "label113");
            this.label113.Name = "label113";
            // 
            // label114
            // 
            resources.ApplyResources(this.label114, "label114");
            this.label114.Name = "label114";
            // 
            // label115
            // 
            resources.ApplyResources(this.label115, "label115");
            this.label115.Name = "label115";
            // 
            // label102
            // 
            resources.ApplyResources(this.label102, "label102");
            this.label102.Name = "label102";
            // 
            // label103
            // 
            resources.ApplyResources(this.label103, "label103");
            this.label103.Name = "label103";
            // 
            // label104
            // 
            resources.ApplyResources(this.label104, "label104");
            this.label104.Name = "label104";
            // 
            // label105
            // 
            resources.ApplyResources(this.label105, "label105");
            this.label105.Name = "label105";
            // 
            // label106
            // 
            resources.ApplyResources(this.label106, "label106");
            this.label106.Name = "label106";
            // 
            // label107
            // 
            resources.ApplyResources(this.label107, "label107");
            this.label107.Name = "label107";
            // 
            // label108
            // 
            resources.ApplyResources(this.label108, "label108");
            this.label108.Name = "label108";
            // 
            // numericBoxStress33
            // 
            resources.ApplyResources(this.numericBoxStress33, "numericBoxStress33");
            this.numericBoxStress33.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress33.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress33.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress33.Name = "numericBoxStress33";
            this.numericBoxStress33.RestrictLimitValue = false;
            this.numericBoxStress33.RoundErrorAccuracy = -1;
            this.numericBoxStress33.SkipEventDuringInput = false;
            this.numericBoxStress33.SmartIncrement = true;
            this.numericBoxStress33.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStress22
            // 
            resources.ApplyResources(this.numericBoxStress22, "numericBoxStress22");
            this.numericBoxStress22.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress22.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress22.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress22.Name = "numericBoxStress22";
            this.numericBoxStress22.RestrictLimitValue = false;
            this.numericBoxStress22.RoundErrorAccuracy = -1;
            this.numericBoxStress22.SkipEventDuringInput = false;
            this.numericBoxStress22.SmartIncrement = true;
            this.numericBoxStress22.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStress11
            // 
            resources.ApplyResources(this.numericBoxStress11, "numericBoxStress11");
            this.numericBoxStress11.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress11.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress11.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress11.Name = "numericBoxStress11";
            this.numericBoxStress11.RestrictLimitValue = false;
            this.numericBoxStress11.RoundErrorAccuracy = -1;
            this.numericBoxStress11.SkipEventDuringInput = false;
            this.numericBoxStress11.SmartIncrement = true;
            this.numericBoxStress11.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStress23
            // 
            resources.ApplyResources(this.numericBoxStress23, "numericBoxStress23");
            this.numericBoxStress23.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress23.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress23.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress23.Name = "numericBoxStress23";
            this.numericBoxStress23.RestrictLimitValue = false;
            this.numericBoxStress23.RoundErrorAccuracy = -1;
            this.numericBoxStress23.SkipEventDuringInput = false;
            this.numericBoxStress23.SmartIncrement = true;
            this.numericBoxStress23.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStress13
            // 
            resources.ApplyResources(this.numericBoxStress13, "numericBoxStress13");
            this.numericBoxStress13.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress13.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress13.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress13.Name = "numericBoxStress13";
            this.numericBoxStress13.RestrictLimitValue = false;
            this.numericBoxStress13.RoundErrorAccuracy = -1;
            this.numericBoxStress13.SkipEventDuringInput = false;
            this.numericBoxStress13.SmartIncrement = true;
            this.numericBoxStress13.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStress12
            // 
            resources.ApplyResources(this.numericBoxStress12, "numericBoxStress12");
            this.numericBoxStress12.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress12.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress12.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStress12.Name = "numericBoxStress12";
            this.numericBoxStress12.RestrictLimitValue = false;
            this.numericBoxStress12.RoundErrorAccuracy = -1;
            this.numericBoxStress12.SkipEventDuringInput = false;
            this.numericBoxStress12.SmartIncrement = true;
            this.numericBoxStress12.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStrain11
            // 
            resources.ApplyResources(this.numericBoxStrain11, "numericBoxStrain11");
            this.numericBoxStrain11.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain11.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain11.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain11.Name = "numericBoxStrain11";
            this.numericBoxStrain11.RestrictLimitValue = false;
            this.numericBoxStrain11.RoundErrorAccuracy = -1;
            this.numericBoxStrain11.SkipEventDuringInput = false;
            this.numericBoxStrain11.SmartIncrement = true;
            this.numericBoxStrain11.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStrain22
            // 
            resources.ApplyResources(this.numericBoxStrain22, "numericBoxStrain22");
            this.numericBoxStrain22.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain22.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain22.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain22.Name = "numericBoxStrain22";
            this.numericBoxStrain22.RestrictLimitValue = false;
            this.numericBoxStrain22.RoundErrorAccuracy = -1;
            this.numericBoxStrain22.SkipEventDuringInput = false;
            this.numericBoxStrain22.SmartIncrement = true;
            this.numericBoxStrain22.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStrain12
            // 
            resources.ApplyResources(this.numericBoxStrain12, "numericBoxStrain12");
            this.numericBoxStrain12.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain12.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain12.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain12.Name = "numericBoxStrain12";
            this.numericBoxStrain12.RestrictLimitValue = false;
            this.numericBoxStrain12.RoundErrorAccuracy = -1;
            this.numericBoxStrain12.SkipEventDuringInput = false;
            this.numericBoxStrain12.SmartIncrement = true;
            this.numericBoxStrain12.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStrain23
            // 
            resources.ApplyResources(this.numericBoxStrain23, "numericBoxStrain23");
            this.numericBoxStrain23.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain23.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain23.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain23.Name = "numericBoxStrain23";
            this.numericBoxStrain23.RestrictLimitValue = false;
            this.numericBoxStrain23.RoundErrorAccuracy = -1;
            this.numericBoxStrain23.SkipEventDuringInput = false;
            this.numericBoxStrain23.SmartIncrement = true;
            this.numericBoxStrain23.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // numericBoxStrain13
            // 
            resources.ApplyResources(this.numericBoxStrain13, "numericBoxStrain13");
            this.numericBoxStrain13.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain13.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain13.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxStrain13.Name = "numericBoxStrain13";
            this.numericBoxStrain13.RestrictLimitValue = false;
            this.numericBoxStrain13.RoundErrorAccuracy = -1;
            this.numericBoxStrain13.SkipEventDuringInput = false;
            this.numericBoxStrain13.SmartIncrement = true;
            this.numericBoxStrain13.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            // 
            // tabPagePolycrystalline
            // 
            this.tabPagePolycrystalline.BackColor = System.Drawing.SystemColors.Control;
            this.tabPagePolycrystalline.ContextMenuStrip = this.contextMenuStripPoleFigure;
            this.tabPagePolycrystalline.Controls.Add(this.poleFigureControl);
            this.tabPagePolycrystalline.Controls.Add(this.flowLayoutPanel3);
            this.tabPagePolycrystalline.Controls.Add(this.flowLayoutPanel2);
            resources.ApplyResources(this.tabPagePolycrystalline, "tabPagePolycrystalline");
            this.tabPagePolycrystalline.Name = "tabPagePolycrystalline";
            // 
            // contextMenuStripPoleFigure
            // 
            this.contextMenuStripPoleFigure.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripPoleFigure.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.contextMenuStripPoleFigure.Name = "contextMenuStripPoleFigure";
            resources.ApplyResources(this.contextMenuStripPoleFigure, "contextMenuStripPoleFigure");
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            resources.ApplyResources(this.readToolStripMenuItem, "readToolStripMenuItem");
            this.readToolStripMenuItem.Click += new System.EventHandler(this.readToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem,
            this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem,
            this.asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
            // 
            // asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem
            // 
            this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem.Name = "asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem";
            resources.ApplyResources(this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem, "asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem");
            this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem.Click += new System.EventHandler(this.asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem_Click);
            // 
            // asTXTFileAllEulerAngleAndDensityToolStripMenuItem
            // 
            this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem.Name = "asTXTFileAllEulerAngleAndDensityToolStripMenuItem";
            resources.ApplyResources(this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem, "asTXTFileAllEulerAngleAndDensityToolStripMenuItem");
            this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem.Click += new System.EventHandler(this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem_Click);
            // 
            // asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem
            // 
            this.asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem.Name = "asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem";
            resources.ApplyResources(this.asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem, "asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem");
            this.asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem.Click += new System.EventHandler(this.asTXTFileAllEulerAngleAndDensityToolStripMenuItem_Click);
            // 
            // poleFigureControl
            // 
            this.poleFigureControl.Crystal = null;
            resources.ApplyResources(this.poleFigureControl, "poleFigureControl");
            this.poleFigureControl.Name = "poleFigureControl";
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Controls.Add(this.buttonGenerateRandomOrientations);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // buttonGenerateRandomOrientations
            // 
            resources.ApplyResources(this.buttonGenerateRandomOrientations, "buttonGenerateRandomOrientations");
            this.buttonGenerateRandomOrientations.Name = "buttonGenerateRandomOrientations";
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
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // numericUpDownAngleResolution
            // 
            this.numericUpDownAngleResolution.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownAngleResolution, "numericUpDownAngleResolution");
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
            // 
            // tabPageBounds
            // 
            this.tabPageBounds.Controls.Add(this.boundControl);
            resources.ApplyResources(this.tabPageBounds, "tabPageBounds");
            this.tabPageBounds.Name = "tabPageBounds";
            // 
            // boundControl
            // 
            this.boundControl.Crystal = null;
            resources.ApplyResources(this.boundControl, "boundControl");
            this.boundControl.Name = "boundControl";
            this.boundControl.SkipEvent = false;
            // 
            // tabPageLatticePlane
            // 
            this.tabPageLatticePlane.Controls.Add(this.latticePlaneControl);
            resources.ApplyResources(this.tabPageLatticePlane, "tabPageLatticePlane");
            this.tabPageLatticePlane.Name = "tabPageLatticePlane";
            // 
            // latticePlaneControl
            // 
            this.latticePlaneControl.Crystal = null;
            resources.ApplyResources(this.latticePlaneControl, "latticePlaneControl");
            this.latticePlaneControl.Name = "latticePlaneControl";
            this.latticePlaneControl.SkipEvent = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxFormula);
            this.panel1.Controls.Add(this.numericBoxZnumber);
            this.panel1.Controls.Add(this.label90);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // textBoxFormula
            // 
            resources.ApplyResources(this.textBoxFormula, "textBoxFormula");
            this.textBoxFormula.Name = "textBoxFormula";
            this.textBoxFormula.ReadOnly = true;
            this.toolTip.SetToolTip(this.textBoxFormula, resources.GetString("textBoxFormula.ToolTip"));
            // 
            // numericBoxZnumber
            // 
            resources.ApplyResources(this.numericBoxZnumber, "numericBoxZnumber");
            this.numericBoxZnumber.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxZnumber.Name = "numericBoxZnumber";
            this.numericBoxZnumber.ReadOnly = true;
            this.numericBoxZnumber.RoundErrorAccuracy = -1;
            this.numericBoxZnumber.SkipEventDuringInput = false;
            this.numericBoxZnumber.SmartIncrement = true;
            this.numericBoxZnumber.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxZnumber.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxZnumber.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxZnumber, resources.GetString("numericBoxZnumber.ToolTip"));
            // 
            // label90
            // 
            resources.ApplyResources(this.label90, "label90");
            this.label90.Name = "label90";
            this.toolTip.SetToolTip(this.label90, resources.GetString("label90.ToolTip"));
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.scatteringFactorToolStripMenuItem,
            this.symmetryInformationToolStripMenuItem,
            this.toolStripSeparator2,
            this.importCrystalFromCIFAMCToolStripMenuItem,
            this.exportThisCrystalAsCIFToolStripMenuItem,
            this.sendThisCrystalToOtherSoftwareToolStripMenuItem,
            this.toolStripSeparator1,
            this.revertCellConstantsToolStripMenuItem,
            this.toolStripSeparator3,
            this.strainControlToolStripMenuItem,
            this.convertToP1SymmetryToolStripMenuItem,
            this.convertToSuperstructureToolStripMenuItem,
            this.convertToAnotherSpacegroupToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            // 
            // scatteringFactorToolStripMenuItem
            // 
            this.scatteringFactorToolStripMenuItem.Name = "scatteringFactorToolStripMenuItem";
            resources.ApplyResources(this.scatteringFactorToolStripMenuItem, "scatteringFactorToolStripMenuItem");
            this.scatteringFactorToolStripMenuItem.Click += new System.EventHandler(this.scatteringFactorToolStripMenuItem_Click);
            // 
            // symmetryInformationToolStripMenuItem
            // 
            this.symmetryInformationToolStripMenuItem.Name = "symmetryInformationToolStripMenuItem";
            resources.ApplyResources(this.symmetryInformationToolStripMenuItem, "symmetryInformationToolStripMenuItem");
            this.symmetryInformationToolStripMenuItem.Click += new System.EventHandler(this.symmetryInformationToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // importCrystalFromCIFAMCToolStripMenuItem
            // 
            this.importCrystalFromCIFAMCToolStripMenuItem.Name = "importCrystalFromCIFAMCToolStripMenuItem";
            resources.ApplyResources(this.importCrystalFromCIFAMCToolStripMenuItem, "importCrystalFromCIFAMCToolStripMenuItem");
            this.importCrystalFromCIFAMCToolStripMenuItem.Click += new System.EventHandler(this.importCrystalFromCIFAMCToolStripMenuItem_Click);
            // 
            // exportThisCrystalAsCIFToolStripMenuItem
            // 
            this.exportThisCrystalAsCIFToolStripMenuItem.Name = "exportThisCrystalAsCIFToolStripMenuItem";
            resources.ApplyResources(this.exportThisCrystalAsCIFToolStripMenuItem, "exportThisCrystalAsCIFToolStripMenuItem");
            this.exportThisCrystalAsCIFToolStripMenuItem.Click += new System.EventHandler(this.exportThisCrystalAsCIFToolStripMenuItem_Click);
            // 
            // sendThisCrystalToOtherSoftwareToolStripMenuItem
            // 
            this.sendThisCrystalToOtherSoftwareToolStripMenuItem.Name = "sendThisCrystalToOtherSoftwareToolStripMenuItem";
            resources.ApplyResources(this.sendThisCrystalToOtherSoftwareToolStripMenuItem, "sendThisCrystalToOtherSoftwareToolStripMenuItem");
            this.sendThisCrystalToOtherSoftwareToolStripMenuItem.Click += new System.EventHandler(this.sendThisCrystalToOtherSoftwareToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // revertCellConstantsToolStripMenuItem
            // 
            this.revertCellConstantsToolStripMenuItem.Name = "revertCellConstantsToolStripMenuItem";
            resources.ApplyResources(this.revertCellConstantsToolStripMenuItem, "revertCellConstantsToolStripMenuItem");
            this.revertCellConstantsToolStripMenuItem.Click += new System.EventHandler(this.revertCellConstantsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // strainControlToolStripMenuItem
            // 
            this.strainControlToolStripMenuItem.Name = "strainControlToolStripMenuItem";
            resources.ApplyResources(this.strainControlToolStripMenuItem, "strainControlToolStripMenuItem");
            this.strainControlToolStripMenuItem.Click += new System.EventHandler(this.strainControlToolStripMenuItem_Click);
            // 
            // convertToP1SymmetryToolStripMenuItem
            // 
            this.convertToP1SymmetryToolStripMenuItem.Name = "convertToP1SymmetryToolStripMenuItem";
            resources.ApplyResources(this.convertToP1SymmetryToolStripMenuItem, "convertToP1SymmetryToolStripMenuItem");
            this.convertToP1SymmetryToolStripMenuItem.Click += new System.EventHandler(this.convertToP1ToolStripMenuItem_Click);
            // 
            // convertToSuperstructureToolStripMenuItem
            // 
            this.convertToSuperstructureToolStripMenuItem.Name = "convertToSuperstructureToolStripMenuItem";
            resources.ApplyResources(this.convertToSuperstructureToolStripMenuItem, "convertToSuperstructureToolStripMenuItem");
            this.convertToSuperstructureToolStripMenuItem.Click += new System.EventHandler(this.convertToSuperstructureToolStripMenuItem_Click);
            // 
            // convertToAnotherSpacegroupToolStripMenuItem
            // 
            this.convertToAnotherSpacegroupToolStripMenuItem.Name = "convertToAnotherSpacegroupToolStripMenuItem";
            resources.ApplyResources(this.convertToAnotherSpacegroupToolStripMenuItem, "convertToAnotherSpacegroupToolStripMenuItem");
            this.convertToAnotherSpacegroupToolStripMenuItem.Click += new System.EventHandler(this.convertToAnotherSpacegroupToolStripMenuItem_Click);
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
            // 
            // buttonScatteringFactor
            // 
            resources.ApplyResources(this.buttonScatteringFactor, "buttonScatteringFactor");
            this.buttonScatteringFactor.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonScatteringFactor.ForeColor = System.Drawing.Color.White;
            this.buttonScatteringFactor.Name = "buttonScatteringFactor";
            this.toolTip.SetToolTip(this.buttonScatteringFactor, resources.GetString("buttonScatteringFactor.ToolTip"));
            this.buttonScatteringFactor.UseVisualStyleBackColor = false;
            this.buttonScatteringFactor.Click += new System.EventHandler(this.buttonScatteringFactor_Click);
            // 
            // buttonSymmetryInfo
            // 
            resources.ApplyResources(this.buttonSymmetryInfo, "buttonSymmetryInfo");
            this.buttonSymmetryInfo.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonSymmetryInfo.ForeColor = System.Drawing.Color.White;
            this.buttonSymmetryInfo.Name = "buttonSymmetryInfo";
            this.toolTip.SetToolTip(this.buttonSymmetryInfo, resources.GetString("buttonSymmetryInfo.ToolTip"));
            this.buttonSymmetryInfo.UseVisualStyleBackColor = false;
            this.buttonSymmetryInfo.Click += new System.EventHandler(this.buttonSymmetryInfo_Click);
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBoxName);
            this.panel4.Controls.Add(this.buttonSymmetryInfo);
            this.panel4.Controls.Add(this.buttonScatteringFactor);
            this.panel4.Controls.Add(this.label22);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // CrystalControl
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.flowLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "CrystalControl";
            this.Load += new System.EventHandler(this.CrystalForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CrystalControl_KeyDown);
            this.Resize += new System.EventHandler(this.CrystalControl_Resize_1);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageEOS;
        private NumericBox numericBoxVolume;
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
        private NumericBox numericBoxStrain33;
        private NumericBox numericBoxStrain11;
        private NumericBox numericBoxStrain22;
        private NumericBox numericBoxStrain12;
        private System.Windows.Forms.Label label103;
        private NumericBox numericBoxStrain23;
        private System.Windows.Forms.Label label104;
        private NumericBox numericBoxStrain13;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.Label label108;
        private NumericBox numericBoxStress33;
        private System.Windows.Forms.Label label109;
        private NumericBox numericBoxStress22;
        private NumericBox numericBoxStress11;
        private NumericBox numericBoxStress23;
        private NumericBox numericBoxStress13;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label label111;
        private NumericBox numericBoxStress12;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.Label label115;
        private NumericBox numericBoxHill;
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
        private System.Windows.Forms.Panel panel1;
        private NumericBox numericBoxMolarVolume;
        private NumericBox numericBoxCellMass;
        private NumericBox numericBoxMolarMass;
        private System.Windows.Forms.Button buttonSymmetryInfo;
        private System.Windows.Forms.Button buttonScatteringFactor;
        private EOSControl eosControl;
        private System.Windows.Forms.Button buttonStressSet;
        private System.Windows.Forms.ToolStripMenuItem convertToP1SymmetryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToSuperstructureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToAnotherSpacegroupToolStripMenuItem;
    }
}