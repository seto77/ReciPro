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
            this.label60 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.textBoxZnumber = new System.Windows.Forms.TextBox();
            this.numericalTextBoxVolume = new Crystallography.Controls.NumericBox();
            this.textBoxDensity = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.colorControl = new Crystallography.Controls.ColorControl();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
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
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.numericTextBoxGammaErr = new Crystallography.Controls.NumericBox();
            this.numericTextBoxBetaErr = new Crystallography.Controls.NumericBox();
            this.numericTextBoxAlphaErr = new Crystallography.Controls.NumericBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.numericTextBoxAlpha = new Crystallography.Controls.NumericBox();
            this.numericTextBoxGamma = new Crystallography.Controls.NumericBox();
            this.numericTextBoxBeta = new Crystallography.Controls.NumericBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label45 = new System.Windows.Forms.Label();
            this.numericTextBoxAErr = new Crystallography.Controls.NumericBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.numericTextBoxCErr = new Crystallography.Controls.NumericBox();
            this.numericTextBoxBErr = new Crystallography.Controls.NumericBox();
            this.label25 = new System.Windows.Forms.Label();
            this.numericTextBoxA = new Crystallography.Controls.NumericBox();
            this.numericTextBoxB = new Crystallography.Controls.NumericBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericTextBoxC = new Crystallography.Controls.NumericBox();
            this.groupBoxSymmetry = new System.Windows.Forms.GroupBox();
            this.comboBoxSpaceGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxPointGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxCrystalSystem = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.comboBoxSearchResult = new System.Windows.Forms.ComboBox();
            this.tabPageAtom = new System.Windows.Forms.TabPage();
            this.panelAtom = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.listBoxAtoms = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAddAtom = new System.Windows.Forms.Button();
            this.buttonAtomUp = new System.Windows.Forms.Button();
            this.buttonChangeAtom = new System.Windows.Forms.Button();
            this.buttonAtomDown = new System.Windows.Forms.Button();
            this.buttonDeleteAtom = new System.Windows.Forms.Button();
            this.atomInput = new Crystallography.Controls.AtomInputControl();
            this.tabPageAtomAdvanced = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.colorControlAtomColor = new Crystallography.Controls.ColorControl();
            this.label62 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownAtomTransparency = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAtomAmbient = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.numericUpDownAtomShininess = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAtomSpecular = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAtomDiffusion = new System.Windows.Forms.NumericUpDown();
            this.label38 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.numericUpDownAtomEmmision = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAtomRadius = new System.Windows.Forms.NumericUpDown();
            this.buttonChangeToSameElement = new System.Windows.Forms.Button();
            this.tabPageBondsPolyhedra = new System.Windows.Forms.TabPage();
            this.buttonAddBond = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonAutoDetect = new System.Windows.Forms.Button();
            this.buttonChangeBond = new System.Windows.Forms.Button();
            this.buttonDeleteBond = new System.Windows.Forms.Button();
            this.colorControlBond = new Crystallography.Controls.ColorControl();
            this.listBoxBondsAndPolyhedra = new System.Windows.Forms.ListBox();
            this.label63 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.checkBoxShowPolyhedron = new System.Windows.Forms.CheckBox();
            this.numericUpDownBondTrasparency = new System.Windows.Forms.NumericUpDown();
            this.label53 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.numericUpDownBondMaxLength = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBondMinLength = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownBondRadius = new System.Windows.Forms.NumericUpDown();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.comboBoxBondingAtom1 = new System.Windows.Forms.ComboBox();
            this.comboBoxBondingAtom2 = new System.Windows.Forms.ComboBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.groupBoxPolyhedron = new System.Windows.Forms.GroupBox();
            this.colorControlPlyhedron = new Crystallography.Controls.ColorControl();
            this.checkBoxShowEdges = new System.Windows.Forms.CheckBox();
            this.groupBoxEdge = new System.Windows.Forms.GroupBox();
            this.colorControlEdges = new Crystallography.Controls.ColorControl();
            this.label55 = new System.Windows.Forms.Label();
            this.numericUpDownEdgeLineWidth = new System.Windows.Forms.NumericUpDown();
            this.label56 = new System.Windows.Forms.Label();
            this.numericUpDownPolyhedronPlaneAlpha = new System.Windows.Forms.NumericUpDown();
            this.checkBoxShowInnerBonds = new System.Windows.Forms.CheckBox();
            this.checkBoxShowVertexAtoms = new System.Windows.Forms.CheckBox();
            this.checkBoxShowCenterAtom = new System.Windows.Forms.CheckBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
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
            this.tabPageStraingStress = new System.Windows.Forms.TabPage();
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
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxSymmetry.SuspendLayout();
            this.tabPageAtom.SuspendLayout();
            this.panelAtom.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tabPageAtomAdvanced.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomTransparency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomAmbient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomShininess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomSpecular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomDiffusion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomEmmision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomRadius)).BeginInit();
            this.tabPageBondsPolyhedra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBondTrasparency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBondMaxLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBondMinLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBondRadius)).BeginInit();
            this.groupBoxPolyhedron.SuspendLayout();
            this.groupBoxEdge.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeLineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPolyhedronPlaneAlpha)).BeginInit();
            this.tabPageReference.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPageEOS.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPageElasticity.SuspendLayout();
            this.tabPageStraingStress.SuspendLayout();
            this.tabPagePolycrystalline.SuspendLayout();
            this.contextMenuStripPoleFigure.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleResolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngleSubDivision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCrystallineSize)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.AllowDrop = true;
            this.tabControl.Controls.Add(this.tabPageBasicInfo);
            this.tabControl.Controls.Add(this.tabPageAtom);
            this.tabControl.Controls.Add(this.tabPageAtomAdvanced);
            this.tabControl.Controls.Add(this.tabPageBondsPolyhedra);
            this.tabControl.Controls.Add(this.tabPageReference);
            this.tabControl.Controls.Add(this.tabPageEOS);
            this.tabControl.Controls.Add(this.tabPageElasticity);
            this.tabControl.Controls.Add(this.tabPageStraingStress);
            this.tabControl.Controls.Add(this.tabPagePolycrystalline);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.HotTrack = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            this.tabControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragDrop);
            this.tabControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragEnter);
            // 
            // tabPageBasicInfo
            // 
            this.tabPageBasicInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageBasicInfo.Controls.Add(this.panel5);
            this.tabPageBasicInfo.Controls.Add(this.tableLayoutPanel3);
            resources.ApplyResources(this.tabPageBasicInfo, "tabPageBasicInfo");
            this.tabPageBasicInfo.Name = "tabPageBasicInfo";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label60);
            this.panel5.Controls.Add(this.label33);
            this.panel5.Controls.Add(this.textBoxZnumber);
            this.panel5.Controls.Add(this.numericalTextBoxVolume);
            this.panel5.Controls.Add(this.textBoxDensity);
            this.panel5.Controls.Add(this.label32);
            this.panel5.Controls.Add(this.label50);
            this.panel5.Controls.Add(this.label31);
            this.panel5.Controls.Add(this.colorControl);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // label60
            // 
            resources.ApplyResources(this.label60, "label60");
            this.label60.Name = "label60";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // textBoxZnumber
            // 
            resources.ApplyResources(this.textBoxZnumber, "textBoxZnumber");
            this.textBoxZnumber.Name = "textBoxZnumber";
            this.textBoxZnumber.ReadOnly = true;
            this.textBoxZnumber.TabStop = false;
            this.toolTip.SetToolTip(this.textBoxZnumber, resources.GetString("textBoxZnumber.ToolTip"));
            // 
            // numericalTextBoxVolume
            // 
            this.numericalTextBoxVolume.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxVolume, "numericalTextBoxVolume");
            this.numericalTextBoxVolume.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxVolume.DecimalPlaces = -1;
            this.numericalTextBoxVolume.Maximum = double.PositiveInfinity;
            this.numericalTextBoxVolume.Minimum = double.NegativeInfinity;
            this.numericalTextBoxVolume.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxVolume.MouseSpeed = 1D;
            this.numericalTextBoxVolume.Multiline = false;
            this.numericalTextBoxVolume.Name = "numericalTextBoxVolume";
            this.numericalTextBoxVolume.RadianValue = 0D;
            this.numericalTextBoxVolume.ReadOnly = true;
            this.numericalTextBoxVolume.RestrictLimitValue = false;
            this.numericalTextBoxVolume.ShowFraction = false;
            this.numericalTextBoxVolume.ShowPositiveSign = false;
            this.numericalTextBoxVolume.ShowUpDown = false;
            this.numericalTextBoxVolume.SkipEventDuringInput = false;
            this.numericalTextBoxVolume.SmartIncrement = true;
            this.numericalTextBoxVolume.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxVolume.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxVolume.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxVolume.ThonsandsSeparator = false;
            this.toolTip.SetToolTip(this.numericalTextBoxVolume, resources.GetString("numericalTextBoxVolume.ToolTip"));
            this.numericalTextBoxVolume.UpDown_Increment = 1D;
            this.numericalTextBoxVolume.Value = 0D;
            this.numericalTextBoxVolume.WordWrap = true;
            // 
            // textBoxDensity
            // 
            resources.ApplyResources(this.textBoxDensity, "textBoxDensity");
            this.textBoxDensity.Name = "textBoxDensity";
            this.textBoxDensity.ReadOnly = true;
            this.textBoxDensity.TabStop = false;
            this.toolTip.SetToolTip(this.textBoxDensity, resources.GetString("textBoxDensity.ToolTip"));
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // label50
            // 
            resources.ApplyResources(this.label50, "label50");
            this.label50.Name = "label50";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // colorControl
            // 
            this.colorControl.Argb = -986896;
            this.colorControl.Blue = 240;
            this.colorControl.BlueF = 0.9411765F;
            this.colorControl.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            resources.ApplyResources(this.colorControl, "colorControl");
            this.colorControl.Green = 240;
            this.colorControl.GreenF = 0.9411765F;
            this.colorControl.Name = "colorControl";
            this.colorControl.Red = 240;
            this.colorControl.RedF = 0.9411765F;
            this.colorControl.ToolTip = "結晶の表示色を指定します";
            this.toolTip.SetToolTip(this.colorControl, resources.GetString("colorControl.ToolTip"));
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.groupBox4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBoxSymmetry, 1, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // groupBox4
            // 
            this.groupBox4.ContextMenuStrip = this.contextMenuStrip;
            this.groupBox4.Controls.Add(this.tableLayoutPanel4);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // contextMenuStrip
            // 
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
            // clearAllDataToolStripMenuItem
            // 
            this.clearAllDataToolStripMenuItem.Name = "clearAllDataToolStripMenuItem";
            resources.ApplyResources(this.clearAllDataToolStripMenuItem, "clearAllDataToolStripMenuItem");
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
            // tableLayoutPanel4
            // 
            resources.ApplyResources(this.tableLayoutPanel4, "tableLayoutPanel4");
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.numericTextBoxGammaErr, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericTextBoxBetaErr, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.numericTextBoxAlphaErr, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label10, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericTextBoxAlpha, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.numericTextBoxGamma, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.numericTextBoxBeta, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label28, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label27, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label26, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label46, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label47, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.label48, 4, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // numericTextBoxGammaErr
            // 
            this.numericTextBoxGammaErr.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxGammaErr, "numericTextBoxGammaErr");
            this.numericTextBoxGammaErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxGammaErr.DecimalPlaces = -1;
            this.numericTextBoxGammaErr.Maximum = double.PositiveInfinity;
            this.numericTextBoxGammaErr.Minimum = double.NegativeInfinity;
            this.numericTextBoxGammaErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxGammaErr.MouseSpeed = 1D;
            this.numericTextBoxGammaErr.Multiline = false;
            this.numericTextBoxGammaErr.Name = "numericTextBoxGammaErr";
            this.numericTextBoxGammaErr.RadianValue = 0D;
            this.numericTextBoxGammaErr.ReadOnly = false;
            this.numericTextBoxGammaErr.RestrictLimitValue = false;
            this.numericTextBoxGammaErr.ShowFraction = false;
            this.numericTextBoxGammaErr.ShowPositiveSign = false;
            this.numericTextBoxGammaErr.ShowUpDown = false;
            this.numericTextBoxGammaErr.SkipEventDuringInput = false;
            this.numericTextBoxGammaErr.SmartIncrement = true;
            this.numericTextBoxGammaErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxGammaErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxGammaErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxGammaErr.ThonsandsSeparator = false;
            this.numericTextBoxGammaErr.UpDown_Increment = 1D;
            this.numericTextBoxGammaErr.Value = 0D;
            this.numericTextBoxGammaErr.WordWrap = true;
            this.numericTextBoxGammaErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            // 
            // numericTextBoxBetaErr
            // 
            this.numericTextBoxBetaErr.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxBetaErr, "numericTextBoxBetaErr");
            this.numericTextBoxBetaErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxBetaErr.DecimalPlaces = -1;
            this.numericTextBoxBetaErr.Maximum = double.PositiveInfinity;
            this.numericTextBoxBetaErr.Minimum = double.NegativeInfinity;
            this.numericTextBoxBetaErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxBetaErr.MouseSpeed = 1D;
            this.numericTextBoxBetaErr.Multiline = false;
            this.numericTextBoxBetaErr.Name = "numericTextBoxBetaErr";
            this.numericTextBoxBetaErr.RadianValue = 0D;
            this.numericTextBoxBetaErr.ReadOnly = false;
            this.numericTextBoxBetaErr.RestrictLimitValue = false;
            this.numericTextBoxBetaErr.ShowFraction = false;
            this.numericTextBoxBetaErr.ShowPositiveSign = false;
            this.numericTextBoxBetaErr.ShowUpDown = false;
            this.numericTextBoxBetaErr.SkipEventDuringInput = false;
            this.numericTextBoxBetaErr.SmartIncrement = true;
            this.numericTextBoxBetaErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxBetaErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxBetaErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxBetaErr.ThonsandsSeparator = false;
            this.numericTextBoxBetaErr.UpDown_Increment = 1D;
            this.numericTextBoxBetaErr.Value = 0D;
            this.numericTextBoxBetaErr.WordWrap = true;
            this.numericTextBoxBetaErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            // 
            // numericTextBoxAlphaErr
            // 
            this.numericTextBoxAlphaErr.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxAlphaErr, "numericTextBoxAlphaErr");
            this.numericTextBoxAlphaErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxAlphaErr.DecimalPlaces = -1;
            this.numericTextBoxAlphaErr.Maximum = double.PositiveInfinity;
            this.numericTextBoxAlphaErr.Minimum = double.NegativeInfinity;
            this.numericTextBoxAlphaErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxAlphaErr.MouseSpeed = 1D;
            this.numericTextBoxAlphaErr.Multiline = false;
            this.numericTextBoxAlphaErr.Name = "numericTextBoxAlphaErr";
            this.numericTextBoxAlphaErr.RadianValue = 0D;
            this.numericTextBoxAlphaErr.ReadOnly = false;
            this.numericTextBoxAlphaErr.RestrictLimitValue = false;
            this.numericTextBoxAlphaErr.ShowFraction = false;
            this.numericTextBoxAlphaErr.ShowPositiveSign = false;
            this.numericTextBoxAlphaErr.ShowUpDown = false;
            this.numericTextBoxAlphaErr.SkipEventDuringInput = false;
            this.numericTextBoxAlphaErr.SmartIncrement = true;
            this.numericTextBoxAlphaErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxAlphaErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxAlphaErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxAlphaErr.ThonsandsSeparator = false;
            this.numericTextBoxAlphaErr.UpDown_Increment = 1D;
            this.numericTextBoxAlphaErr.Value = 0D;
            this.numericTextBoxAlphaErr.WordWrap = true;
            this.numericTextBoxAlphaErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // numericTextBoxAlpha
            // 
            this.numericTextBoxAlpha.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxAlpha, "numericTextBoxAlpha");
            this.numericTextBoxAlpha.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxAlpha.DecimalPlaces = -1;
            this.numericTextBoxAlpha.Maximum = double.PositiveInfinity;
            this.numericTextBoxAlpha.Minimum = double.NegativeInfinity;
            this.numericTextBoxAlpha.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxAlpha.MouseSpeed = 1D;
            this.numericTextBoxAlpha.Multiline = false;
            this.numericTextBoxAlpha.Name = "numericTextBoxAlpha";
            this.numericTextBoxAlpha.RadianValue = 0D;
            this.numericTextBoxAlpha.ReadOnly = false;
            this.numericTextBoxAlpha.RestrictLimitValue = false;
            this.numericTextBoxAlpha.ShowFraction = false;
            this.numericTextBoxAlpha.ShowPositiveSign = false;
            this.numericTextBoxAlpha.ShowUpDown = false;
            this.numericTextBoxAlpha.SkipEventDuringInput = false;
            this.numericTextBoxAlpha.SmartIncrement = true;
            this.numericTextBoxAlpha.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxAlpha.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxAlpha.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxAlpha.ThonsandsSeparator = false;
            this.numericTextBoxAlpha.UpDown_Increment = 1D;
            this.numericTextBoxAlpha.Value = 0D;
            this.numericTextBoxAlpha.WordWrap = true;
            this.numericTextBoxAlpha.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            this.numericTextBoxAlpha.ReadOnlyChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericTextBoxErr_ReadOnlyChanged);
            // 
            // numericTextBoxGamma
            // 
            this.numericTextBoxGamma.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxGamma, "numericTextBoxGamma");
            this.numericTextBoxGamma.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxGamma.DecimalPlaces = -1;
            this.numericTextBoxGamma.Maximum = double.PositiveInfinity;
            this.numericTextBoxGamma.Minimum = double.NegativeInfinity;
            this.numericTextBoxGamma.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxGamma.MouseSpeed = 1D;
            this.numericTextBoxGamma.Multiline = false;
            this.numericTextBoxGamma.Name = "numericTextBoxGamma";
            this.numericTextBoxGamma.RadianValue = 0D;
            this.numericTextBoxGamma.ReadOnly = false;
            this.numericTextBoxGamma.RestrictLimitValue = false;
            this.numericTextBoxGamma.ShowFraction = false;
            this.numericTextBoxGamma.ShowPositiveSign = false;
            this.numericTextBoxGamma.ShowUpDown = false;
            this.numericTextBoxGamma.SkipEventDuringInput = false;
            this.numericTextBoxGamma.SmartIncrement = true;
            this.numericTextBoxGamma.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxGamma.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxGamma.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxGamma.ThonsandsSeparator = false;
            this.numericTextBoxGamma.UpDown_Increment = 1D;
            this.numericTextBoxGamma.Value = 0D;
            this.numericTextBoxGamma.WordWrap = true;
            this.numericTextBoxGamma.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            this.numericTextBoxGamma.ReadOnlyChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericTextBoxErr_ReadOnlyChanged);
            // 
            // numericTextBoxBeta
            // 
            this.numericTextBoxBeta.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxBeta, "numericTextBoxBeta");
            this.numericTextBoxBeta.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxBeta.DecimalPlaces = -1;
            this.numericTextBoxBeta.Maximum = double.PositiveInfinity;
            this.numericTextBoxBeta.Minimum = double.NegativeInfinity;
            this.numericTextBoxBeta.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxBeta.MouseSpeed = 1D;
            this.numericTextBoxBeta.Multiline = false;
            this.numericTextBoxBeta.Name = "numericTextBoxBeta";
            this.numericTextBoxBeta.RadianValue = 0D;
            this.numericTextBoxBeta.ReadOnly = false;
            this.numericTextBoxBeta.RestrictLimitValue = false;
            this.numericTextBoxBeta.ShowFraction = false;
            this.numericTextBoxBeta.ShowPositiveSign = false;
            this.numericTextBoxBeta.ShowUpDown = false;
            this.numericTextBoxBeta.SkipEventDuringInput = false;
            this.numericTextBoxBeta.SmartIncrement = true;
            this.numericTextBoxBeta.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxBeta.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxBeta.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxBeta.ThonsandsSeparator = false;
            this.numericTextBoxBeta.UpDown_Increment = 1D;
            this.numericTextBoxBeta.Value = 0D;
            this.numericTextBoxBeta.WordWrap = true;
            this.numericTextBoxBeta.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            this.numericTextBoxBeta.ReadOnlyChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericTextBoxErr_ReadOnlyChanged);
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            this.toolTip.SetToolTip(this.label28, resources.GetString("label28.ToolTip"));
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            this.toolTip.SetToolTip(this.label27, resources.GetString("label27.ToolTip"));
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            this.toolTip.SetToolTip(this.label26, resources.GetString("label26.ToolTip"));
            // 
            // label46
            // 
            resources.ApplyResources(this.label46, "label46");
            this.label46.Name = "label46";
            // 
            // label47
            // 
            resources.ApplyResources(this.label47, "label47");
            this.label47.Name = "label47";
            // 
            // label48
            // 
            resources.ApplyResources(this.label48, "label48");
            this.label48.Name = "label48";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label45, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericTextBoxAErr, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label44, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label23, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label18, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label24, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericTextBoxCErr, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericTextBoxBErr, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label25, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.numericTextBoxA, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.numericTextBoxB, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.numericTextBoxC, 1, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label45
            // 
            resources.ApplyResources(this.label45, "label45");
            this.label45.Name = "label45";
            this.toolTip.SetToolTip(this.label45, resources.GetString("label45.ToolTip"));
            // 
            // numericTextBoxAErr
            // 
            this.numericTextBoxAErr.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxAErr, "numericTextBoxAErr");
            this.numericTextBoxAErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxAErr.DecimalPlaces = -1;
            this.numericTextBoxAErr.Maximum = double.PositiveInfinity;
            this.numericTextBoxAErr.Minimum = double.NegativeInfinity;
            this.numericTextBoxAErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxAErr.MouseSpeed = 1D;
            this.numericTextBoxAErr.Multiline = false;
            this.numericTextBoxAErr.Name = "numericTextBoxAErr";
            this.numericTextBoxAErr.RadianValue = 0D;
            this.numericTextBoxAErr.ReadOnly = false;
            this.numericTextBoxAErr.RestrictLimitValue = false;
            this.numericTextBoxAErr.ShowFraction = false;
            this.numericTextBoxAErr.ShowPositiveSign = false;
            this.numericTextBoxAErr.ShowUpDown = false;
            this.numericTextBoxAErr.SkipEventDuringInput = false;
            this.numericTextBoxAErr.SmartIncrement = true;
            this.numericTextBoxAErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxAErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxAErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxAErr.ThonsandsSeparator = false;
            this.numericTextBoxAErr.UpDown_Increment = 1D;
            this.numericTextBoxAErr.Value = 0D;
            this.numericTextBoxAErr.WordWrap = true;
            this.numericTextBoxAErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            // 
            // label44
            // 
            resources.ApplyResources(this.label44, "label44");
            this.label44.Name = "label44";
            this.toolTip.SetToolTip(this.label44, resources.GetString("label44.ToolTip"));
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            this.toolTip.SetToolTip(this.label23, resources.GetString("label23.ToolTip"));
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            this.toolTip.SetToolTip(this.label18, resources.GetString("label18.ToolTip"));
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            this.toolTip.SetToolTip(this.label24, resources.GetString("label24.ToolTip"));
            // 
            // numericTextBoxCErr
            // 
            this.numericTextBoxCErr.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxCErr, "numericTextBoxCErr");
            this.numericTextBoxCErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxCErr.DecimalPlaces = -1;
            this.numericTextBoxCErr.Maximum = double.PositiveInfinity;
            this.numericTextBoxCErr.Minimum = double.NegativeInfinity;
            this.numericTextBoxCErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxCErr.MouseSpeed = 1D;
            this.numericTextBoxCErr.Multiline = false;
            this.numericTextBoxCErr.Name = "numericTextBoxCErr";
            this.numericTextBoxCErr.RadianValue = 0D;
            this.numericTextBoxCErr.ReadOnly = false;
            this.numericTextBoxCErr.RestrictLimitValue = false;
            this.numericTextBoxCErr.ShowFraction = false;
            this.numericTextBoxCErr.ShowPositiveSign = false;
            this.numericTextBoxCErr.ShowUpDown = false;
            this.numericTextBoxCErr.SkipEventDuringInput = false;
            this.numericTextBoxCErr.SmartIncrement = true;
            this.numericTextBoxCErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxCErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxCErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxCErr.ThonsandsSeparator = false;
            this.numericTextBoxCErr.UpDown_Increment = 1D;
            this.numericTextBoxCErr.Value = 0D;
            this.numericTextBoxCErr.WordWrap = true;
            this.numericTextBoxCErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            // 
            // numericTextBoxBErr
            // 
            this.numericTextBoxBErr.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxBErr, "numericTextBoxBErr");
            this.numericTextBoxBErr.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxBErr.DecimalPlaces = -1;
            this.numericTextBoxBErr.Maximum = double.PositiveInfinity;
            this.numericTextBoxBErr.Minimum = double.NegativeInfinity;
            this.numericTextBoxBErr.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxBErr.MouseSpeed = 1D;
            this.numericTextBoxBErr.Multiline = false;
            this.numericTextBoxBErr.Name = "numericTextBoxBErr";
            this.numericTextBoxBErr.RadianValue = 0D;
            this.numericTextBoxBErr.ReadOnly = false;
            this.numericTextBoxBErr.RestrictLimitValue = false;
            this.numericTextBoxBErr.ShowFraction = false;
            this.numericTextBoxBErr.ShowPositiveSign = false;
            this.numericTextBoxBErr.ShowUpDown = false;
            this.numericTextBoxBErr.SkipEventDuringInput = false;
            this.numericTextBoxBErr.SmartIncrement = true;
            this.numericTextBoxBErr.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxBErr.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxBErr.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxBErr.ThonsandsSeparator = false;
            this.numericTextBoxBErr.UpDown_Increment = 1D;
            this.numericTextBoxBErr.Value = 0D;
            this.numericTextBoxBErr.WordWrap = true;
            this.numericTextBoxBErr.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            this.toolTip.SetToolTip(this.label25, resources.GetString("label25.ToolTip"));
            // 
            // numericTextBoxA
            // 
            this.numericTextBoxA.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxA, "numericTextBoxA");
            this.numericTextBoxA.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxA.DecimalPlaces = -1;
            this.numericTextBoxA.Maximum = double.PositiveInfinity;
            this.numericTextBoxA.Minimum = double.NegativeInfinity;
            this.numericTextBoxA.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxA.MouseSpeed = 1D;
            this.numericTextBoxA.Multiline = false;
            this.numericTextBoxA.Name = "numericTextBoxA";
            this.numericTextBoxA.RadianValue = 0D;
            this.numericTextBoxA.ReadOnly = false;
            this.numericTextBoxA.RestrictLimitValue = false;
            this.numericTextBoxA.ShowFraction = false;
            this.numericTextBoxA.ShowPositiveSign = false;
            this.numericTextBoxA.ShowUpDown = false;
            this.numericTextBoxA.SkipEventDuringInput = false;
            this.numericTextBoxA.SmartIncrement = true;
            this.numericTextBoxA.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxA.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxA.ThonsandsSeparator = false;
            this.numericTextBoxA.UpDown_Increment = 1D;
            this.numericTextBoxA.Value = 0D;
            this.numericTextBoxA.WordWrap = true;
            this.numericTextBoxA.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            this.numericTextBoxA.ReadOnlyChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericTextBoxErr_ReadOnlyChanged);
            // 
            // numericTextBoxB
            // 
            this.numericTextBoxB.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxB, "numericTextBoxB");
            this.numericTextBoxB.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxB.DecimalPlaces = -1;
            this.numericTextBoxB.Maximum = double.PositiveInfinity;
            this.numericTextBoxB.Minimum = double.NegativeInfinity;
            this.numericTextBoxB.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxB.MouseSpeed = 1D;
            this.numericTextBoxB.Multiline = false;
            this.numericTextBoxB.Name = "numericTextBoxB";
            this.numericTextBoxB.RadianValue = 0D;
            this.numericTextBoxB.ReadOnly = false;
            this.numericTextBoxB.RestrictLimitValue = false;
            this.numericTextBoxB.ShowFraction = false;
            this.numericTextBoxB.ShowPositiveSign = false;
            this.numericTextBoxB.ShowUpDown = false;
            this.numericTextBoxB.SkipEventDuringInput = false;
            this.numericTextBoxB.SmartIncrement = true;
            this.numericTextBoxB.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxB.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxB.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxB.ThonsandsSeparator = false;
            this.numericTextBoxB.UpDown_Increment = 1D;
            this.numericTextBoxB.Value = 0D;
            this.numericTextBoxB.WordWrap = true;
            this.numericTextBoxB.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            this.numericTextBoxB.ReadOnlyChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericTextBoxErr_ReadOnlyChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numericTextBoxC
            // 
            this.numericTextBoxC.AllowMouseControl = false;
            resources.ApplyResources(this.numericTextBoxC, "numericTextBoxC");
            this.numericTextBoxC.BackColor = System.Drawing.SystemColors.Control;
            this.numericTextBoxC.DecimalPlaces = -1;
            this.numericTextBoxC.Maximum = double.PositiveInfinity;
            this.numericTextBoxC.Minimum = double.NegativeInfinity;
            this.numericTextBoxC.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericTextBoxC.MouseSpeed = 1D;
            this.numericTextBoxC.Multiline = false;
            this.numericTextBoxC.Name = "numericTextBoxC";
            this.numericTextBoxC.RadianValue = 0D;
            this.numericTextBoxC.ReadOnly = false;
            this.numericTextBoxC.RestrictLimitValue = false;
            this.numericTextBoxC.ShowFraction = false;
            this.numericTextBoxC.ShowPositiveSign = false;
            this.numericTextBoxC.ShowUpDown = false;
            this.numericTextBoxC.SkipEventDuringInput = false;
            this.numericTextBoxC.SmartIncrement = true;
            this.numericTextBoxC.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericTextBoxC.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericTextBoxC.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericTextBoxC.ThonsandsSeparator = false;
            this.numericTextBoxC.UpDown_Increment = 1D;
            this.numericTextBoxC.Value = 0D;
            this.numericTextBoxC.WordWrap = true;
            this.numericTextBoxC.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxUnitCell_ValueChanged);
            this.numericTextBoxC.ReadOnlyChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericTextBoxErr_ReadOnlyChanged);
            // 
            // groupBoxSymmetry
            // 
            this.groupBoxSymmetry.Controls.Add(this.comboBoxSpaceGroup);
            this.groupBoxSymmetry.Controls.Add(this.comboBoxPointGroup);
            this.groupBoxSymmetry.Controls.Add(this.comboBoxCrystalSystem);
            this.groupBoxSymmetry.Controls.Add(this.label20);
            this.groupBoxSymmetry.Controls.Add(this.label17);
            this.groupBoxSymmetry.Controls.Add(this.label19);
            this.groupBoxSymmetry.Controls.Add(this.textBoxSearch);
            this.groupBoxSymmetry.Controls.Add(this.label21);
            this.groupBoxSymmetry.Controls.Add(this.comboBoxSearchResult);
            resources.ApplyResources(this.groupBoxSymmetry, "groupBoxSymmetry");
            this.groupBoxSymmetry.Name = "groupBoxSymmetry";
            this.groupBoxSymmetry.TabStop = false;
            // 
            // comboBoxSpaceGroup
            // 
            resources.ApplyResources(this.comboBoxSpaceGroup, "comboBoxSpaceGroup");
            this.comboBoxSpaceGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSpaceGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpaceGroup.DropDownWidth = 200;
            this.comboBoxSpaceGroup.Name = "comboBoxSpaceGroup";
            this.toolTip.SetToolTip(this.comboBoxSpaceGroup, resources.GetString("comboBoxSpaceGroup.ToolTip"));
            this.comboBoxSpaceGroup.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxSpaceGroup_DrawItem);
            this.comboBoxSpaceGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxSpaceGroup_SelectedIndexChanged);
            // 
            // comboBoxPointGroup
            // 
            resources.ApplyResources(this.comboBoxPointGroup, "comboBoxPointGroup");
            this.comboBoxPointGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxPointGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPointGroup.Name = "comboBoxPointGroup";
            this.toolTip.SetToolTip(this.comboBoxPointGroup, resources.GetString("comboBoxPointGroup.ToolTip"));
            this.comboBoxPointGroup.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxSpaceGroup_DrawItem);
            this.comboBoxPointGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxPointGroup_SelectedIndexChanged);
            // 
            // comboBoxCrystalSystem
            // 
            resources.ApplyResources(this.comboBoxCrystalSystem, "comboBoxCrystalSystem");
            this.comboBoxCrystalSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCrystalSystem.Items.AddRange(new object[] {
            resources.GetString("comboBoxCrystalSystem.Items"),
            resources.GetString("comboBoxCrystalSystem.Items1"),
            resources.GetString("comboBoxCrystalSystem.Items2"),
            resources.GetString("comboBoxCrystalSystem.Items3"),
            resources.GetString("comboBoxCrystalSystem.Items4"),
            resources.GetString("comboBoxCrystalSystem.Items5"),
            resources.GetString("comboBoxCrystalSystem.Items6"),
            resources.GetString("comboBoxCrystalSystem.Items7")});
            this.comboBoxCrystalSystem.Name = "comboBoxCrystalSystem";
            this.toolTip.SetToolTip(this.comboBoxCrystalSystem, resources.GetString("comboBoxCrystalSystem.ToolTip"));
            this.comboBoxCrystalSystem.SelectedIndexChanged += new System.EventHandler(this.comboBoxCrystalSystem_SelectedIndexChanged);
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // textBoxSearch
            // 
            resources.ApplyResources(this.textBoxSearch, "textBoxSearch");
            this.textBoxSearch.Name = "textBoxSearch";
            this.toolTip.SetToolTip(this.textBoxSearch, resources.GetString("textBoxSearch.ToolTip"));
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // comboBoxSearchResult
            // 
            resources.ApplyResources(this.comboBoxSearchResult, "comboBoxSearchResult");
            this.comboBoxSearchResult.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxSearchResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchResult.DropDownWidth = 200;
            this.comboBoxSearchResult.Name = "comboBoxSearchResult";
            this.toolTip.SetToolTip(this.comboBoxSearchResult, resources.GetString("comboBoxSearchResult.ToolTip"));
            this.comboBoxSearchResult.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxSpaceGroup_DrawItem);
            this.comboBoxSearchResult.SelectedIndexChanged += new System.EventHandler(this.comboBoxSearchResult_SelectedIndexChanged);
            // 
            // tabPageAtom
            // 
            this.tabPageAtom.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAtom.Controls.Add(this.panelAtom);
            this.tabPageAtom.Controls.Add(this.atomInput);
            resources.ApplyResources(this.tabPageAtom, "tabPageAtom");
            this.tabPageAtom.Name = "tabPageAtom";
            // 
            // panelAtom
            // 
            resources.ApplyResources(this.panelAtom, "panelAtom");
            this.panelAtom.Controls.Add(this.panel2);
            this.panelAtom.Controls.Add(this.panel3);
            this.panelAtom.Name = "panelAtom";
            this.toolTip.SetToolTip(this.panelAtom, resources.GetString("panelAtom.ToolTip"));
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.listBoxAtoms);
            this.panel2.Controls.Add(this.listBox1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // listBoxAtoms
            // 
            resources.ApplyResources(this.listBoxAtoms, "listBoxAtoms");
            this.listBoxAtoms.Name = "listBoxAtoms";
            this.toolTip.SetToolTip(this.listBoxAtoms, resources.GetString("listBoxAtoms.ToolTip"));
            this.listBoxAtoms.Click += new System.EventHandler(this.listBoxAtoms_SelectedIndexChanged);
            this.listBoxAtoms.SelectedIndexChanged += new System.EventHandler(this.listBoxAtoms_SelectedIndexChanged);
            this.listBoxAtoms.MouseLeave += new System.EventHandler(this.listBoxAtoms_MouseLeave);
            this.listBoxAtoms.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxAtoms_MouseUp);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Control;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox1.TabStop = false;
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.ContextMenuStrip = this.contextMenuStrip2;
            this.panel3.Controls.Add(this.buttonAddAtom);
            this.panel3.Controls.Add(this.buttonAtomUp);
            this.panel3.Controls.Add(this.buttonChangeAtom);
            this.panel3.Controls.Add(this.buttonAtomDown);
            this.panel3.Controls.Add(this.buttonDeleteAtom);
            this.panel3.Name = "panel3";
            // 
            // contextMenuStrip2
            // 
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
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
            // buttonAddAtom
            // 
            resources.ApplyResources(this.buttonAddAtom, "buttonAddAtom");
            this.buttonAddAtom.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAddAtom.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.buttonAddAtom.Name = "buttonAddAtom";
            this.toolTip.SetToolTip(this.buttonAddAtom, resources.GetString("buttonAddAtom.ToolTip"));
            this.buttonAddAtom.UseVisualStyleBackColor = false;
            this.buttonAddAtom.Click += new System.EventHandler(this.buttonAddAtom_Click);
            // 
            // buttonAtomUp
            // 
            resources.ApplyResources(this.buttonAtomUp, "buttonAtomUp");
            this.buttonAtomUp.BackColor = System.Drawing.SystemColors.Control;
            this.buttonAtomUp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonAtomUp.Name = "buttonAtomUp";
            this.buttonAtomUp.UseVisualStyleBackColor = true;
            this.buttonAtomUp.Click += new System.EventHandler(this.buttonAtomUp_Click);
            // 
            // buttonChangeAtom
            // 
            resources.ApplyResources(this.buttonChangeAtom, "buttonChangeAtom");
            this.buttonChangeAtom.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonChangeAtom.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.buttonChangeAtom.Name = "buttonChangeAtom";
            this.toolTip.SetToolTip(this.buttonChangeAtom, resources.GetString("buttonChangeAtom.ToolTip"));
            this.buttonChangeAtom.UseVisualStyleBackColor = false;
            this.buttonChangeAtom.Click += new System.EventHandler(this.buttonChangeAtom_Click);
            // 
            // buttonAtomDown
            // 
            resources.ApplyResources(this.buttonAtomDown, "buttonAtomDown");
            this.buttonAtomDown.BackColor = System.Drawing.SystemColors.Control;
            this.buttonAtomDown.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonAtomDown.Name = "buttonAtomDown";
            this.buttonAtomDown.UseVisualStyleBackColor = true;
            this.buttonAtomDown.Click += new System.EventHandler(this.buttonAtomDown_Click);
            // 
            // buttonDeleteAtom
            // 
            resources.ApplyResources(this.buttonDeleteAtom, "buttonDeleteAtom");
            this.buttonDeleteAtom.BackColor = System.Drawing.Color.IndianRed;
            this.buttonDeleteAtom.ForeColor = System.Drawing.Color.White;
            this.buttonDeleteAtom.Name = "buttonDeleteAtom";
            this.toolTip.SetToolTip(this.buttonDeleteAtom, resources.GetString("buttonDeleteAtom.ToolTip"));
            this.buttonDeleteAtom.UseVisualStyleBackColor = false;
            this.buttonDeleteAtom.Click += new System.EventHandler(this.buttonDeleteAtom_Click);
            // 
            // atomInput
            // 
            this.atomInput.AtomNo = 0;
            this.atomInput.AtomSubNoElectron = -1;
            this.atomInput.AtomSubNoXray = -1;
            resources.ApplyResources(this.atomInput, "atomInput");
            this.atomInput.B11 = 0D;
            this.atomInput.B11Err = 0D;
            this.atomInput.B12 = 0D;
            this.atomInput.B12Err = 0D;
            this.atomInput.B13 = 0D;
            this.atomInput.B13Err = 0D;
            this.atomInput.B22 = 0D;
            this.atomInput.B22Err = 0D;
            this.atomInput.B23 = 0D;
            this.atomInput.B23Err = 0D;
            this.atomInput.B33 = 0D;
            this.atomInput.B33Err = 0D;
            this.atomInput.Biso = 0D;
            this.atomInput.BisoErr = 0D;
            this.atomInput.Details1 = false;
            this.atomInput.Details2 = false;
            this.atomInput.IsotopicComposition = null;
            this.atomInput.Istoropy = false;
            this.atomInput.Label = "";
            this.atomInput.Name = "atomInput";
            this.atomInput.Occ = 0D;
            this.atomInput.OccErr = 0D;
            this.atomInput.X = 0D;
            this.atomInput.XErr = 0D;
            this.atomInput.Y = 0D;
            this.atomInput.YErr = 0D;
            this.atomInput.Z = 0D;
            this.atomInput.ZErr = 0D;
            // 
            // tabPageAtomAdvanced
            // 
            this.tabPageAtomAdvanced.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAtomAdvanced.Controls.Add(this.panel1);
            resources.ApplyResources(this.tabPageAtomAdvanced, "tabPageAtomAdvanced");
            this.tabPageAtomAdvanced.Name = "tabPageAtomAdvanced";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.colorControlAtomColor);
            this.panel1.Controls.Add(this.label62);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.numericUpDownAtomRadius);
            this.panel1.Controls.Add(this.buttonChangeToSameElement);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // colorControlAtomColor
            // 
            this.colorControlAtomColor.Argb = -986896;
            this.colorControlAtomColor.Blue = 240;
            this.colorControlAtomColor.BlueF = 0.9411765F;
            this.colorControlAtomColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            resources.ApplyResources(this.colorControlAtomColor, "colorControlAtomColor");
            this.colorControlAtomColor.Green = 240;
            this.colorControlAtomColor.GreenF = 0.9411765F;
            this.colorControlAtomColor.Name = "colorControlAtomColor";
            this.colorControlAtomColor.Red = 240;
            this.colorControlAtomColor.RedF = 0.9411765F;
            this.colorControlAtomColor.ToolTip = "";
            // 
            // label62
            // 
            resources.ApplyResources(this.label62, "label62");
            this.label62.Name = "label62";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownAtomTransparency);
            this.groupBox1.Controls.Add(this.numericUpDownAtomAmbient);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.numericUpDownAtomShininess);
            this.groupBox1.Controls.Add(this.numericUpDownAtomSpecular);
            this.groupBox1.Controls.Add(this.numericUpDownAtomDiffusion);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.numericUpDownAtomEmmision);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // numericUpDownAtomTransparency
            // 
            this.numericUpDownAtomTransparency.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownAtomTransparency, "numericUpDownAtomTransparency");
            this.numericUpDownAtomTransparency.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomTransparency.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtomTransparency.Name = "numericUpDownAtomTransparency";
            this.toolTip.SetToolTip(this.numericUpDownAtomTransparency, resources.GetString("numericUpDownAtomTransparency.ToolTip"));
            this.numericUpDownAtomTransparency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownAtomAmbient
            // 
            this.numericUpDownAtomAmbient.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownAtomAmbient, "numericUpDownAtomAmbient");
            this.numericUpDownAtomAmbient.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomAmbient.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtomAmbient.Name = "numericUpDownAtomAmbient";
            this.toolTip.SetToolTip(this.numericUpDownAtomAmbient, resources.GetString("numericUpDownAtomAmbient.ToolTip"));
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label37
            // 
            resources.ApplyResources(this.label37, "label37");
            this.label37.Name = "label37";
            // 
            // numericUpDownAtomShininess
            // 
            resources.ApplyResources(this.numericUpDownAtomShininess, "numericUpDownAtomShininess");
            this.numericUpDownAtomShininess.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownAtomShininess.Name = "numericUpDownAtomShininess";
            this.toolTip.SetToolTip(this.numericUpDownAtomShininess, resources.GetString("numericUpDownAtomShininess.ToolTip"));
            // 
            // numericUpDownAtomSpecular
            // 
            this.numericUpDownAtomSpecular.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownAtomSpecular, "numericUpDownAtomSpecular");
            this.numericUpDownAtomSpecular.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomSpecular.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtomSpecular.Name = "numericUpDownAtomSpecular";
            this.toolTip.SetToolTip(this.numericUpDownAtomSpecular, resources.GetString("numericUpDownAtomSpecular.ToolTip"));
            // 
            // numericUpDownAtomDiffusion
            // 
            this.numericUpDownAtomDiffusion.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownAtomDiffusion, "numericUpDownAtomDiffusion");
            this.numericUpDownAtomDiffusion.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomDiffusion.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtomDiffusion.Name = "numericUpDownAtomDiffusion";
            this.toolTip.SetToolTip(this.numericUpDownAtomDiffusion, resources.GetString("numericUpDownAtomDiffusion.ToolTip"));
            this.numericUpDownAtomDiffusion.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.Name = "label38";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // numericUpDownAtomEmmision
            // 
            this.numericUpDownAtomEmmision.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownAtomEmmision, "numericUpDownAtomEmmision");
            this.numericUpDownAtomEmmision.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomEmmision.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtomEmmision.Name = "numericUpDownAtomEmmision";
            this.toolTip.SetToolTip(this.numericUpDownAtomEmmision, resources.GetString("numericUpDownAtomEmmision.ToolTip"));
            // 
            // numericUpDownAtomRadius
            // 
            this.numericUpDownAtomRadius.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDownAtomRadius, "numericUpDownAtomRadius");
            this.numericUpDownAtomRadius.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomRadius.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            65536});
            this.numericUpDownAtomRadius.Name = "numericUpDownAtomRadius";
            this.toolTip.SetToolTip(this.numericUpDownAtomRadius, resources.GetString("numericUpDownAtomRadius.ToolTip"));
            // 
            // buttonChangeToSameElement
            // 
            this.buttonChangeToSameElement.BackColor = System.Drawing.Color.SteelBlue;
            resources.ApplyResources(this.buttonChangeToSameElement, "buttonChangeToSameElement");
            this.buttonChangeToSameElement.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.buttonChangeToSameElement.Name = "buttonChangeToSameElement";
            this.toolTip.SetToolTip(this.buttonChangeToSameElement, resources.GetString("buttonChangeToSameElement.ToolTip"));
            this.buttonChangeToSameElement.UseVisualStyleBackColor = false;
            this.buttonChangeToSameElement.Click += new System.EventHandler(this.buttonChangeToSameElement_Click);
            // 
            // tabPageBondsPolyhedra
            // 
            this.tabPageBondsPolyhedra.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageBondsPolyhedra.Controls.Add(this.buttonAddBond);
            this.tabPageBondsPolyhedra.Controls.Add(this.button1);
            this.tabPageBondsPolyhedra.Controls.Add(this.buttonAutoDetect);
            this.tabPageBondsPolyhedra.Controls.Add(this.buttonChangeBond);
            this.tabPageBondsPolyhedra.Controls.Add(this.buttonDeleteBond);
            this.tabPageBondsPolyhedra.Controls.Add(this.colorControlBond);
            this.tabPageBondsPolyhedra.Controls.Add(this.listBoxBondsAndPolyhedra);
            this.tabPageBondsPolyhedra.Controls.Add(this.label63);
            this.tabPageBondsPolyhedra.Controls.Add(this.label59);
            this.tabPageBondsPolyhedra.Controls.Add(this.checkBoxShowPolyhedron);
            this.tabPageBondsPolyhedra.Controls.Add(this.numericUpDownBondTrasparency);
            this.tabPageBondsPolyhedra.Controls.Add(this.label53);
            this.tabPageBondsPolyhedra.Controls.Add(this.listBox3);
            this.tabPageBondsPolyhedra.Controls.Add(this.numericUpDownBondMaxLength);
            this.tabPageBondsPolyhedra.Controls.Add(this.numericUpDownBondMinLength);
            this.tabPageBondsPolyhedra.Controls.Add(this.numericUpDownBondRadius);
            this.tabPageBondsPolyhedra.Controls.Add(this.label51);
            this.tabPageBondsPolyhedra.Controls.Add(this.label52);
            this.tabPageBondsPolyhedra.Controls.Add(this.label43);
            this.tabPageBondsPolyhedra.Controls.Add(this.comboBoxBondingAtom1);
            this.tabPageBondsPolyhedra.Controls.Add(this.comboBoxBondingAtom2);
            this.tabPageBondsPolyhedra.Controls.Add(this.label41);
            this.tabPageBondsPolyhedra.Controls.Add(this.label58);
            this.tabPageBondsPolyhedra.Controls.Add(this.label57);
            this.tabPageBondsPolyhedra.Controls.Add(this.label39);
            this.tabPageBondsPolyhedra.Controls.Add(this.label40);
            this.tabPageBondsPolyhedra.Controls.Add(this.groupBoxPolyhedron);
            resources.ApplyResources(this.tabPageBondsPolyhedra, "tabPageBondsPolyhedra");
            this.tabPageBondsPolyhedra.Name = "tabPageBondsPolyhedra";
            // 
            // buttonAddBond
            // 
            resources.ApplyResources(this.buttonAddBond, "buttonAddBond");
            this.buttonAddBond.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAddBond.ForeColor = System.Drawing.Color.White;
            this.buttonAddBond.Name = "buttonAddBond";
            this.toolTip.SetToolTip(this.buttonAddBond, resources.GetString("buttonAddBond.ToolTip"));
            this.buttonAddBond.UseVisualStyleBackColor = false;
            this.buttonAddBond.Click += new System.EventHandler(this.buttonAddBond_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            // 
            // buttonAutoDetect
            // 
            resources.ApplyResources(this.buttonAutoDetect, "buttonAutoDetect");
            this.buttonAutoDetect.Name = "buttonAutoDetect";
            // 
            // buttonChangeBond
            // 
            resources.ApplyResources(this.buttonChangeBond, "buttonChangeBond");
            this.buttonChangeBond.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonChangeBond.ForeColor = System.Drawing.Color.White;
            this.buttonChangeBond.Name = "buttonChangeBond";
            this.toolTip.SetToolTip(this.buttonChangeBond, resources.GetString("buttonChangeBond.ToolTip"));
            this.buttonChangeBond.UseVisualStyleBackColor = false;
            this.buttonChangeBond.Click += new System.EventHandler(this.buttonChangeBond_Click);
            // 
            // buttonDeleteBond
            // 
            resources.ApplyResources(this.buttonDeleteBond, "buttonDeleteBond");
            this.buttonDeleteBond.BackColor = System.Drawing.Color.IndianRed;
            this.buttonDeleteBond.ForeColor = System.Drawing.Color.White;
            this.buttonDeleteBond.Name = "buttonDeleteBond";
            this.toolTip.SetToolTip(this.buttonDeleteBond, resources.GetString("buttonDeleteBond.ToolTip"));
            this.buttonDeleteBond.UseVisualStyleBackColor = false;
            this.buttonDeleteBond.Click += new System.EventHandler(this.buttonDeleteBond_Click);
            // 
            // colorControlBond
            // 
            this.colorControlBond.Argb = -986896;
            this.colorControlBond.Blue = 240;
            this.colorControlBond.BlueF = 0.9411765F;
            this.colorControlBond.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControlBond.Green = 240;
            this.colorControlBond.GreenF = 0.9411765F;
            resources.ApplyResources(this.colorControlBond, "colorControlBond");
            this.colorControlBond.Name = "colorControlBond";
            this.colorControlBond.Red = 240;
            this.colorControlBond.RedF = 0.9411765F;
            this.colorControlBond.ToolTip = "";
            // 
            // listBoxBondsAndPolyhedra
            // 
            resources.ApplyResources(this.listBoxBondsAndPolyhedra, "listBoxBondsAndPolyhedra");
            this.listBoxBondsAndPolyhedra.Name = "listBoxBondsAndPolyhedra";
            this.toolTip.SetToolTip(this.listBoxBondsAndPolyhedra, resources.GetString("listBoxBondsAndPolyhedra.ToolTip"));
            this.listBoxBondsAndPolyhedra.Click += new System.EventHandler(this.listBoxBondsAndPolyhedra_SelectedIndexChanged);
            this.listBoxBondsAndPolyhedra.SelectedIndexChanged += new System.EventHandler(this.listBoxBondsAndPolyhedra_SelectedIndexChanged);
            // 
            // label63
            // 
            resources.ApplyResources(this.label63, "label63");
            this.label63.Name = "label63";
            // 
            // label59
            // 
            resources.ApplyResources(this.label59, "label59");
            this.label59.Name = "label59";
            // 
            // checkBoxShowPolyhedron
            // 
            resources.ApplyResources(this.checkBoxShowPolyhedron, "checkBoxShowPolyhedron");
            this.checkBoxShowPolyhedron.Checked = true;
            this.checkBoxShowPolyhedron.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowPolyhedron.Name = "checkBoxShowPolyhedron";
            this.toolTip.SetToolTip(this.checkBoxShowPolyhedron, resources.GetString("checkBoxShowPolyhedron.ToolTip"));
            this.checkBoxShowPolyhedron.UseVisualStyleBackColor = true;
            this.checkBoxShowPolyhedron.CheckStateChanged += new System.EventHandler(this.checkBoxShowPolyhedron_CheckedChanged);
            // 
            // numericUpDownBondTrasparency
            // 
            this.numericUpDownBondTrasparency.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownBondTrasparency, "numericUpDownBondTrasparency");
            this.numericUpDownBondTrasparency.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownBondTrasparency.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBondTrasparency.Name = "numericUpDownBondTrasparency";
            this.toolTip.SetToolTip(this.numericUpDownBondTrasparency, resources.GetString("numericUpDownBondTrasparency.ToolTip"));
            this.numericUpDownBondTrasparency.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // label53
            // 
            resources.ApplyResources(this.label53, "label53");
            this.label53.Name = "label53";
            // 
            // listBox3
            // 
            this.listBox3.BackColor = System.Drawing.SystemColors.Control;
            this.listBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.listBox3, "listBox3");
            this.listBox3.Items.AddRange(new object[] {
            resources.GetString("listBox3.Items")});
            this.listBox3.Name = "listBox3";
            this.listBox3.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox3.TabStop = false;
            // 
            // numericUpDownBondMaxLength
            // 
            this.numericUpDownBondMaxLength.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDownBondMaxLength, "numericUpDownBondMaxLength");
            this.numericUpDownBondMaxLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownBondMaxLength.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            65536});
            this.numericUpDownBondMaxLength.Name = "numericUpDownBondMaxLength";
            this.toolTip.SetToolTip(this.numericUpDownBondMaxLength, resources.GetString("numericUpDownBondMaxLength.ToolTip"));
            this.numericUpDownBondMaxLength.Value = new decimal(new int[] {
            16,
            0,
            0,
            65536});
            // 
            // numericUpDownBondMinLength
            // 
            this.numericUpDownBondMinLength.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDownBondMinLength, "numericUpDownBondMinLength");
            this.numericUpDownBondMinLength.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownBondMinLength.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            65536});
            this.numericUpDownBondMinLength.Name = "numericUpDownBondMinLength";
            this.toolTip.SetToolTip(this.numericUpDownBondMinLength, resources.GetString("numericUpDownBondMinLength.ToolTip"));
            // 
            // numericUpDownBondRadius
            // 
            this.numericUpDownBondRadius.DecimalPlaces = 3;
            resources.ApplyResources(this.numericUpDownBondRadius, "numericUpDownBondRadius");
            this.numericUpDownBondRadius.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            this.numericUpDownBondRadius.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBondRadius.Name = "numericUpDownBondRadius";
            this.toolTip.SetToolTip(this.numericUpDownBondRadius, resources.GetString("numericUpDownBondRadius.ToolTip"));
            this.numericUpDownBondRadius.Value = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            // 
            // label51
            // 
            resources.ApplyResources(this.label51, "label51");
            this.label51.Name = "label51";
            // 
            // label52
            // 
            resources.ApplyResources(this.label52, "label52");
            this.label52.Name = "label52";
            // 
            // label43
            // 
            resources.ApplyResources(this.label43, "label43");
            this.label43.Name = "label43";
            // 
            // comboBoxBondingAtom1
            // 
            this.comboBoxBondingAtom1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxBondingAtom1, "comboBoxBondingAtom1");
            this.comboBoxBondingAtom1.Name = "comboBoxBondingAtom1";
            this.toolTip.SetToolTip(this.comboBoxBondingAtom1, resources.GetString("comboBoxBondingAtom1.ToolTip"));
            // 
            // comboBoxBondingAtom2
            // 
            this.comboBoxBondingAtom2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxBondingAtom2, "comboBoxBondingAtom2");
            this.comboBoxBondingAtom2.Items.AddRange(new object[] {
            resources.GetString("comboBoxBondingAtom2.Items")});
            this.comboBoxBondingAtom2.Name = "comboBoxBondingAtom2";
            this.toolTip.SetToolTip(this.comboBoxBondingAtom2, resources.GetString("comboBoxBondingAtom2.ToolTip"));
            // 
            // label41
            // 
            resources.ApplyResources(this.label41, "label41");
            this.label41.Name = "label41";
            // 
            // label58
            // 
            resources.ApplyResources(this.label58, "label58");
            this.label58.Name = "label58";
            // 
            // label57
            // 
            resources.ApplyResources(this.label57, "label57");
            this.label57.Name = "label57";
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.Name = "label39";
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.Name = "label40";
            // 
            // groupBoxPolyhedron
            // 
            this.groupBoxPolyhedron.Controls.Add(this.colorControlPlyhedron);
            this.groupBoxPolyhedron.Controls.Add(this.checkBoxShowEdges);
            this.groupBoxPolyhedron.Controls.Add(this.groupBoxEdge);
            this.groupBoxPolyhedron.Controls.Add(this.numericUpDownPolyhedronPlaneAlpha);
            this.groupBoxPolyhedron.Controls.Add(this.checkBoxShowInnerBonds);
            this.groupBoxPolyhedron.Controls.Add(this.checkBoxShowVertexAtoms);
            this.groupBoxPolyhedron.Controls.Add(this.checkBoxShowCenterAtom);
            this.groupBoxPolyhedron.Controls.Add(this.label54);
            this.groupBoxPolyhedron.Controls.Add(this.label42);
            resources.ApplyResources(this.groupBoxPolyhedron, "groupBoxPolyhedron");
            this.groupBoxPolyhedron.Name = "groupBoxPolyhedron";
            this.groupBoxPolyhedron.TabStop = false;
            // 
            // colorControlPlyhedron
            // 
            this.colorControlPlyhedron.Argb = -986896;
            this.colorControlPlyhedron.Blue = 240;
            this.colorControlPlyhedron.BlueF = 0.9411765F;
            this.colorControlPlyhedron.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControlPlyhedron.Green = 240;
            this.colorControlPlyhedron.GreenF = 0.9411765F;
            resources.ApplyResources(this.colorControlPlyhedron, "colorControlPlyhedron");
            this.colorControlPlyhedron.Name = "colorControlPlyhedron";
            this.colorControlPlyhedron.Red = 240;
            this.colorControlPlyhedron.RedF = 0.9411765F;
            this.colorControlPlyhedron.ToolTip = "";
            // 
            // checkBoxShowEdges
            // 
            resources.ApplyResources(this.checkBoxShowEdges, "checkBoxShowEdges");
            this.checkBoxShowEdges.Checked = true;
            this.checkBoxShowEdges.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowEdges.Name = "checkBoxShowEdges";
            this.toolTip.SetToolTip(this.checkBoxShowEdges, resources.GetString("checkBoxShowEdges.ToolTip"));
            this.checkBoxShowEdges.UseVisualStyleBackColor = true;
            this.checkBoxShowEdges.CheckedChanged += new System.EventHandler(this.checkBoxShowEdges_CheckedChanged);
            // 
            // groupBoxEdge
            // 
            this.groupBoxEdge.Controls.Add(this.colorControlEdges);
            this.groupBoxEdge.Controls.Add(this.label55);
            this.groupBoxEdge.Controls.Add(this.numericUpDownEdgeLineWidth);
            this.groupBoxEdge.Controls.Add(this.label56);
            resources.ApplyResources(this.groupBoxEdge, "groupBoxEdge");
            this.groupBoxEdge.Name = "groupBoxEdge";
            this.groupBoxEdge.TabStop = false;
            // 
            // colorControlEdges
            // 
            this.colorControlEdges.Argb = -986896;
            this.colorControlEdges.Blue = 240;
            this.colorControlEdges.BlueF = 0.9411765F;
            this.colorControlEdges.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControlEdges.Green = 240;
            this.colorControlEdges.GreenF = 0.9411765F;
            resources.ApplyResources(this.colorControlEdges, "colorControlEdges");
            this.colorControlEdges.Name = "colorControlEdges";
            this.colorControlEdges.Red = 240;
            this.colorControlEdges.RedF = 0.9411765F;
            this.colorControlEdges.ToolTip = "";
            // 
            // label55
            // 
            resources.ApplyResources(this.label55, "label55");
            this.label55.Name = "label55";
            // 
            // numericUpDownEdgeLineWidth
            // 
            this.numericUpDownEdgeLineWidth.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownEdgeLineWidth, "numericUpDownEdgeLineWidth");
            this.numericUpDownEdgeLineWidth.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownEdgeLineWidth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownEdgeLineWidth.Name = "numericUpDownEdgeLineWidth";
            this.toolTip.SetToolTip(this.numericUpDownEdgeLineWidth, resources.GetString("numericUpDownEdgeLineWidth.ToolTip"));
            this.numericUpDownEdgeLineWidth.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label56
            // 
            resources.ApplyResources(this.label56, "label56");
            this.label56.Name = "label56";
            // 
            // numericUpDownPolyhedronPlaneAlpha
            // 
            this.numericUpDownPolyhedronPlaneAlpha.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownPolyhedronPlaneAlpha, "numericUpDownPolyhedronPlaneAlpha");
            this.numericUpDownPolyhedronPlaneAlpha.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownPolyhedronPlaneAlpha.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPolyhedronPlaneAlpha.Name = "numericUpDownPolyhedronPlaneAlpha";
            this.toolTip.SetToolTip(this.numericUpDownPolyhedronPlaneAlpha, resources.GetString("numericUpDownPolyhedronPlaneAlpha.ToolTip"));
            this.numericUpDownPolyhedronPlaneAlpha.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // checkBoxShowInnerBonds
            // 
            resources.ApplyResources(this.checkBoxShowInnerBonds, "checkBoxShowInnerBonds");
            this.checkBoxShowInnerBonds.Checked = true;
            this.checkBoxShowInnerBonds.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowInnerBonds.Name = "checkBoxShowInnerBonds";
            this.toolTip.SetToolTip(this.checkBoxShowInnerBonds, resources.GetString("checkBoxShowInnerBonds.ToolTip"));
            this.checkBoxShowInnerBonds.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowVertexAtoms
            // 
            resources.ApplyResources(this.checkBoxShowVertexAtoms, "checkBoxShowVertexAtoms");
            this.checkBoxShowVertexAtoms.Checked = true;
            this.checkBoxShowVertexAtoms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowVertexAtoms.Name = "checkBoxShowVertexAtoms";
            this.toolTip.SetToolTip(this.checkBoxShowVertexAtoms, resources.GetString("checkBoxShowVertexAtoms.ToolTip"));
            this.checkBoxShowVertexAtoms.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowCenterAtom
            // 
            resources.ApplyResources(this.checkBoxShowCenterAtom, "checkBoxShowCenterAtom");
            this.checkBoxShowCenterAtom.Checked = true;
            this.checkBoxShowCenterAtom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowCenterAtom.Name = "checkBoxShowCenterAtom";
            this.toolTip.SetToolTip(this.checkBoxShowCenterAtom, resources.GetString("checkBoxShowCenterAtom.ToolTip"));
            this.checkBoxShowCenterAtom.UseVisualStyleBackColor = true;
            // 
            // label54
            // 
            resources.ApplyResources(this.label54, "label54");
            this.label54.Name = "label54";
            // 
            // label42
            // 
            resources.ApplyResources(this.label42, "label42");
            this.label42.Name = "label42";
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
            this.toolTip.SetToolTip(this.textBoxTitle, resources.GetString("textBoxTitle.ToolTip"));
            this.textBoxTitle.TextChanged += new System.EventHandler(this.textBoxReferenfeChanged_TextChanged);
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
            this.toolTip.SetToolTip(this.textBoxAuthor, resources.GetString("textBoxAuthor.ToolTip"));
            this.textBoxAuthor.TextChanged += new System.EventHandler(this.textBoxReferenfeChanged_TextChanged);
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
            this.toolTip.SetToolTip(this.textBoxJournal, resources.GetString("textBoxJournal.ToolTip"));
            this.textBoxJournal.TextChanged += new System.EventHandler(this.textBoxReferenfeChanged_TextChanged);
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
            this.toolTip.SetToolTip(this.textBoxMemo, resources.GetString("textBoxMemo.ToolTip"));
            this.textBoxMemo.TextChanged += new System.EventHandler(this.textBoxReferenfeChanged_TextChanged);
            // 
            // tabPageEOS
            // 
            this.tabPageEOS.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageEOS.Controls.Add(this.label83);
            this.tabPageEOS.Controls.Add(this.textBoxEOS_Note);
            this.tabPageEOS.Controls.Add(this.checkBoxUseEOS);
            this.tabPageEOS.Controls.Add(this.groupBox3);
            this.tabPageEOS.Controls.Add(this.groupBox2);
            this.tabPageEOS.Controls.Add(this.numericBoxEOS_T0);
            this.tabPageEOS.Controls.Add(this.numericalTextBoxTemperature);
            this.tabPageEOS.Controls.Add(this.numericBoxPressure);
            resources.ApplyResources(this.tabPageEOS, "tabPageEOS");
            this.tabPageEOS.Name = "tabPageEOS";
            // 
            // label83
            // 
            resources.ApplyResources(this.label83, "label83");
            this.label83.Name = "label83";
            // 
            // textBoxEOS_Note
            // 
            resources.ApplyResources(this.textBoxEOS_Note, "textBoxEOS_Note");
            this.textBoxEOS_Note.Name = "textBoxEOS_Note";
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
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // numericBoxEOS_C
            // 
            this.numericBoxEOS_C.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_C, "numericBoxEOS_C");
            this.numericBoxEOS_C.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_C.DecimalPlaces = -1;
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
            this.numericBoxEOS_C.UpDown_Increment = 1D;
            this.numericBoxEOS_C.Value = 0D;
            this.numericBoxEOS_C.WordWrap = true;
            this.numericBoxEOS_C.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_B
            // 
            this.numericBoxEOS_B.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_B, "numericBoxEOS_B");
            this.numericBoxEOS_B.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_B.DecimalPlaces = -1;
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
            this.numericBoxEOS_B.UpDown_Increment = 1D;
            this.numericBoxEOS_B.Value = 0D;
            this.numericBoxEOS_B.WordWrap = true;
            this.numericBoxEOS_B.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // radioButtonTdependenceK0andV0
            // 
            resources.ApplyResources(this.radioButtonTdependenceK0andV0, "radioButtonTdependenceK0andV0");
            this.radioButtonTdependenceK0andV0.Name = "radioButtonTdependenceK0andV0";
            this.radioButtonTdependenceK0andV0.UseVisualStyleBackColor = true;
            this.radioButtonTdependenceK0andV0.CheckedChanged += new System.EventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_A
            // 
            this.numericBoxEOS_A.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_A, "numericBoxEOS_A");
            this.numericBoxEOS_A.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_A.DecimalPlaces = -1;
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
            this.numericBoxEOS_A.UpDown_Increment = 1D;
            this.numericBoxEOS_A.Value = 0D;
            this.numericBoxEOS_A.WordWrap = true;
            this.numericBoxEOS_A.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_KperT
            // 
            this.numericBoxEOS_KperT.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_KperT, "numericBoxEOS_KperT");
            this.numericBoxEOS_KperT.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KperT.DecimalPlaces = -1;
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
            this.numericBoxEOS_KperT.UpDown_Increment = 1D;
            this.numericBoxEOS_KperT.Value = 0D;
            this.numericBoxEOS_KperT.WordWrap = true;
            this.numericBoxEOS_KperT.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_Gamma0
            // 
            this.numericBoxEOS_Gamma0.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_Gamma0, "numericBoxEOS_Gamma0");
            this.numericBoxEOS_Gamma0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Gamma0.DecimalPlaces = -1;
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
            this.toolTip.SetToolTip(this.numericBoxEOS_Gamma0, resources.GetString("numericBoxEOS_Gamma0.ToolTip"));
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
            // 
            // label69
            // 
            resources.ApplyResources(this.label69, "label69");
            this.label69.Name = "label69";
            // 
            // label72
            // 
            resources.ApplyResources(this.label72, "label72");
            this.label72.Name = "label72";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label91
            // 
            resources.ApplyResources(this.label91, "label91");
            this.label91.Name = "label91";
            // 
            // label95
            // 
            resources.ApplyResources(this.label95, "label95");
            this.label95.Name = "label95";
            // 
            // label93
            // 
            resources.ApplyResources(this.label93, "label93");
            this.label93.Name = "label93";
            // 
            // label85
            // 
            resources.ApplyResources(this.label85, "label85");
            this.label85.Name = "label85";
            // 
            // label86
            // 
            resources.ApplyResources(this.label86, "label86");
            this.label86.Name = "label86";
            // 
            // label96
            // 
            resources.ApplyResources(this.label96, "label96");
            this.label96.Name = "label96";
            // 
            // label97
            // 
            resources.ApplyResources(this.label97, "label97");
            this.label97.Name = "label97";
            // 
            // label94
            // 
            resources.ApplyResources(this.label94, "label94");
            this.label94.Name = "label94";
            // 
            // label92
            // 
            resources.ApplyResources(this.label92, "label92");
            this.label92.Name = "label92";
            // 
            // label84
            // 
            resources.ApplyResources(this.label84, "label84");
            this.label84.Name = "label84";
            // 
            // label64
            // 
            resources.ApplyResources(this.label64, "label64");
            this.label64.Name = "label64";
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
            this.numericBoxEOS_Theta0.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_Theta0, "numericBoxEOS_Theta0");
            this.numericBoxEOS_Theta0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Theta0.DecimalPlaces = -1;
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
            this.toolTip.SetToolTip(this.numericBoxEOS_Theta0, resources.GetString("numericBoxEOS_Theta0.ToolTip"));
            this.numericBoxEOS_Theta0.UpDown_Increment = 1D;
            this.numericBoxEOS_Theta0.Value = 300D;
            this.numericBoxEOS_Theta0.WordWrap = true;
            this.numericBoxEOS_Theta0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_Q
            // 
            this.numericBoxEOS_Q.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_Q, "numericBoxEOS_Q");
            this.numericBoxEOS_Q.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_Q.DecimalPlaces = -1;
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
            this.toolTip.SetToolTip(this.numericBoxEOS_Q, resources.GetString("numericBoxEOS_Q.ToolTip"));
            this.numericBoxEOS_Q.UpDown_Increment = 1D;
            this.numericBoxEOS_Q.Value = 0D;
            this.numericBoxEOS_Q.WordWrap = true;
            this.numericBoxEOS_Q.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // radioButtonMieGruneisen
            // 
            this.radioButtonMieGruneisen.Checked = true;
            resources.ApplyResources(this.radioButtonMieGruneisen, "radioButtonMieGruneisen");
            this.radioButtonMieGruneisen.Name = "radioButtonMieGruneisen";
            this.radioButtonMieGruneisen.TabStop = true;
            this.radioButtonMieGruneisen.UseVisualStyleBackColor = true;
            this.radioButtonMieGruneisen.CheckedChanged += new System.EventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // label98
            // 
            resources.ApplyResources(this.label98, "label98");
            this.label98.Name = "label98";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            // 
            // groupBox2
            // 
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
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label75
            // 
            resources.ApplyResources(this.label75, "label75");
            this.label75.Name = "label75";
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
            this.numericalTextBoxEOS_V0perMol.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxEOS_V0perMol, "numericalTextBoxEOS_V0perMol");
            this.numericalTextBoxEOS_V0perMol.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxEOS_V0perMol.DecimalPlaces = -1;
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
            this.numericalTextBoxEOS_V0perMol.UpDown_Increment = 1D;
            this.numericalTextBoxEOS_V0perMol.Value = 300D;
            this.numericalTextBoxEOS_V0perMol.WordWrap = true;
            this.numericalTextBoxEOS_V0perMol.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_V0perMol_ValueChanged);
            this.numericalTextBoxEOS_V0perMol.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_V0perMol_Click2);
            // 
            // numericBoxEOS_V0perCell
            // 
            this.numericBoxEOS_V0perCell.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_V0perCell, "numericBoxEOS_V0perCell");
            this.numericBoxEOS_V0perCell.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_V0perCell.DecimalPlaces = -1;
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
            this.toolTip.SetToolTip(this.numericBoxEOS_V0perCell, resources.GetString("numericBoxEOS_V0perCell.ToolTip"));
            this.numericBoxEOS_V0perCell.UpDown_Increment = 1D;
            this.numericBoxEOS_V0perCell.Value = 300D;
            this.numericBoxEOS_V0perCell.WordWrap = true;
            this.numericBoxEOS_V0perCell.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            this.numericBoxEOS_V0perCell.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_V0perCell_Click2);
            // 
            // numericBoxEOS_KT0
            // 
            this.numericBoxEOS_KT0.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_KT0, "numericBoxEOS_KT0");
            this.numericBoxEOS_KT0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KT0.DecimalPlaces = -1;
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
            this.toolTip.SetToolTip(this.numericBoxEOS_KT0, resources.GetString("numericBoxEOS_KT0.ToolTip"));
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
            // 
            // label68
            // 
            resources.ApplyResources(this.label68, "label68");
            this.label68.Name = "label68";
            this.toolTip.SetToolTip(this.label68, resources.GetString("label68.ToolTip"));
            // 
            // numericBoxEOS_KprimeT0
            // 
            this.numericBoxEOS_KprimeT0.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_KprimeT0, "numericBoxEOS_KprimeT0");
            this.numericBoxEOS_KprimeT0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_KprimeT0.DecimalPlaces = -1;
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
            this.toolTip.SetToolTip(this.numericBoxEOS_KprimeT0, resources.GetString("numericBoxEOS_KprimeT0.ToolTip"));
            this.numericBoxEOS_KprimeT0.UpDown_Increment = 1D;
            this.numericBoxEOS_KprimeT0.Value = 4D;
            this.numericBoxEOS_KprimeT0.WordWrap = true;
            this.numericBoxEOS_KprimeT0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // radioButtonVinet
            // 
            resources.ApplyResources(this.radioButtonVinet, "radioButtonVinet");
            this.radioButtonVinet.Name = "radioButtonVinet";
            this.radioButtonVinet.UseVisualStyleBackColor = true;
            this.radioButtonVinet.CheckedChanged += new System.EventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // radioButtonBirchMurnaghan
            // 
            this.radioButtonBirchMurnaghan.Checked = true;
            resources.ApplyResources(this.radioButtonBirchMurnaghan, "radioButtonBirchMurnaghan");
            this.radioButtonBirchMurnaghan.Name = "radioButtonBirchMurnaghan";
            this.radioButtonBirchMurnaghan.TabStop = true;
            this.radioButtonBirchMurnaghan.UseVisualStyleBackColor = true;
            this.radioButtonBirchMurnaghan.CheckedChanged += new System.EventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxEOS_T0
            // 
            this.numericBoxEOS_T0.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxEOS_T0, "numericBoxEOS_T0");
            this.numericBoxEOS_T0.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxEOS_T0.DecimalPlaces = -1;
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
            this.toolTip.SetToolTip(this.numericBoxEOS_T0, resources.GetString("numericBoxEOS_T0.ToolTip"));
            this.numericBoxEOS_T0.UpDown_Increment = 1D;
            this.numericBoxEOS_T0.Value = 300D;
            this.numericBoxEOS_T0.WordWrap = true;
            this.numericBoxEOS_T0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericalTextBoxTemperature
            // 
            this.numericalTextBoxTemperature.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxTemperature, "numericalTextBoxTemperature");
            this.numericalTextBoxTemperature.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxTemperature.DecimalPlaces = -1;
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
            this.numericalTextBoxTemperature.UpDown_Increment = 1D;
            this.numericalTextBoxTemperature.Value = 300D;
            this.numericalTextBoxTemperature.WordWrap = true;
            this.numericalTextBoxTemperature.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxEOS_State_ValueChanged);
            // 
            // numericBoxPressure
            // 
            this.numericBoxPressure.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxPressure, "numericBoxPressure");
            this.numericBoxPressure.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPressure.DecimalPlaces = 5;
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
            this.numericBoxPressure.UpDown_Increment = 1D;
            this.numericBoxPressure.Value = 0D;
            this.numericBoxPressure.WordWrap = true;
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
            // tabPageStraingStress
            // 
            this.tabPageStraingStress.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxHill);
            this.tabPageStraingStress.Controls.Add(this.label116);
            this.tabPageStraingStress.Controls.Add(this.label117);
            this.tabPageStraingStress.Controls.Add(this.label109);
            this.tabPageStraingStress.Controls.Add(this.label110);
            this.tabPageStraingStress.Controls.Add(this.label111);
            this.tabPageStraingStress.Controls.Add(this.label112);
            this.tabPageStraingStress.Controls.Add(this.label113);
            this.tabPageStraingStress.Controls.Add(this.label114);
            this.tabPageStraingStress.Controls.Add(this.label115);
            this.tabPageStraingStress.Controls.Add(this.label102);
            this.tabPageStraingStress.Controls.Add(this.label103);
            this.tabPageStraingStress.Controls.Add(this.label104);
            this.tabPageStraingStress.Controls.Add(this.label105);
            this.tabPageStraingStress.Controls.Add(this.label106);
            this.tabPageStraingStress.Controls.Add(this.label107);
            this.tabPageStraingStress.Controls.Add(this.label108);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStress33);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStress22);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStress11);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStress23);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStress13);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStress12);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStrain33);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStrain11);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStrain22);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStrain12);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStrain23);
            this.tabPageStraingStress.Controls.Add(this.numericalTextBoxStrain13);
            resources.ApplyResources(this.tabPageStraingStress, "tabPageStraingStress");
            this.tabPageStraingStress.Name = "tabPageStraingStress";
            // 
            // numericalTextBoxHill
            // 
            this.numericalTextBoxHill.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxHill, "numericalTextBoxHill");
            this.numericalTextBoxHill.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxHill.DecimalPlaces = -1;
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
            this.numericalTextBoxHill.UpDown_Increment = 1D;
            this.numericalTextBoxHill.Value = 1D;
            this.numericalTextBoxHill.WordWrap = true;
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
            // numericalTextBoxStress33
            // 
            this.numericalTextBoxStress33.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStress33, "numericalTextBoxStress33");
            this.numericalTextBoxStress33.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress33.DecimalPlaces = -1;
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
            this.numericalTextBoxStress33.UpDown_Increment = 1D;
            this.numericalTextBoxStress33.Value = 0D;
            this.numericalTextBoxStress33.WordWrap = true;
            // 
            // numericalTextBoxStress22
            // 
            this.numericalTextBoxStress22.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStress22, "numericalTextBoxStress22");
            this.numericalTextBoxStress22.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress22.DecimalPlaces = -1;
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
            this.numericalTextBoxStress22.UpDown_Increment = 1D;
            this.numericalTextBoxStress22.Value = 0D;
            this.numericalTextBoxStress22.WordWrap = true;
            // 
            // numericalTextBoxStress11
            // 
            this.numericalTextBoxStress11.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStress11, "numericalTextBoxStress11");
            this.numericalTextBoxStress11.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress11.DecimalPlaces = -1;
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
            this.numericalTextBoxStress11.UpDown_Increment = 1D;
            this.numericalTextBoxStress11.Value = 0D;
            this.numericalTextBoxStress11.WordWrap = true;
            // 
            // numericalTextBoxStress23
            // 
            this.numericalTextBoxStress23.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStress23, "numericalTextBoxStress23");
            this.numericalTextBoxStress23.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress23.DecimalPlaces = -1;
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
            this.numericalTextBoxStress23.UpDown_Increment = 1D;
            this.numericalTextBoxStress23.Value = 0D;
            this.numericalTextBoxStress23.WordWrap = true;
            // 
            // numericalTextBoxStress13
            // 
            this.numericalTextBoxStress13.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStress13, "numericalTextBoxStress13");
            this.numericalTextBoxStress13.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress13.DecimalPlaces = -1;
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
            this.numericalTextBoxStress13.UpDown_Increment = 1D;
            this.numericalTextBoxStress13.Value = 0D;
            this.numericalTextBoxStress13.WordWrap = true;
            // 
            // numericalTextBoxStress12
            // 
            this.numericalTextBoxStress12.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStress12, "numericalTextBoxStress12");
            this.numericalTextBoxStress12.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStress12.DecimalPlaces = -1;
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
            this.numericalTextBoxStress12.UpDown_Increment = 1D;
            this.numericalTextBoxStress12.Value = 0D;
            this.numericalTextBoxStress12.WordWrap = true;
            // 
            // numericalTextBoxStrain33
            // 
            this.numericalTextBoxStrain33.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStrain33, "numericalTextBoxStrain33");
            this.numericalTextBoxStrain33.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain33.DecimalPlaces = -1;
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
            this.numericalTextBoxStrain33.UpDown_Increment = 1D;
            this.numericalTextBoxStrain33.Value = 0D;
            this.numericalTextBoxStrain33.WordWrap = true;
            // 
            // numericalTextBoxStrain11
            // 
            this.numericalTextBoxStrain11.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStrain11, "numericalTextBoxStrain11");
            this.numericalTextBoxStrain11.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain11.DecimalPlaces = -1;
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
            this.numericalTextBoxStrain11.UpDown_Increment = 1D;
            this.numericalTextBoxStrain11.Value = 0D;
            this.numericalTextBoxStrain11.WordWrap = true;
            // 
            // numericalTextBoxStrain22
            // 
            this.numericalTextBoxStrain22.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStrain22, "numericalTextBoxStrain22");
            this.numericalTextBoxStrain22.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain22.DecimalPlaces = -1;
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
            this.numericalTextBoxStrain22.UpDown_Increment = 1D;
            this.numericalTextBoxStrain22.Value = 0D;
            this.numericalTextBoxStrain22.WordWrap = true;
            // 
            // numericalTextBoxStrain12
            // 
            this.numericalTextBoxStrain12.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStrain12, "numericalTextBoxStrain12");
            this.numericalTextBoxStrain12.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain12.DecimalPlaces = -1;
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
            this.numericalTextBoxStrain12.UpDown_Increment = 1D;
            this.numericalTextBoxStrain12.Value = 0D;
            this.numericalTextBoxStrain12.WordWrap = true;
            // 
            // numericalTextBoxStrain23
            // 
            this.numericalTextBoxStrain23.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStrain23, "numericalTextBoxStrain23");
            this.numericalTextBoxStrain23.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain23.DecimalPlaces = -1;
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
            this.numericalTextBoxStrain23.UpDown_Increment = 1D;
            this.numericalTextBoxStrain23.Value = 0D;
            this.numericalTextBoxStrain23.WordWrap = true;
            // 
            // numericalTextBoxStrain13
            // 
            this.numericalTextBoxStrain13.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxStrain13, "numericalTextBoxStrain13");
            this.numericalTextBoxStrain13.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxStrain13.DecimalPlaces = -1;
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
            this.numericalTextBoxStrain13.UpDown_Increment = 1D;
            this.numericalTextBoxStrain13.Value = 0D;
            this.numericalTextBoxStrain13.WordWrap = true;
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
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBoxFormula);
            this.panel4.Controls.Add(this.label90);
            this.panel4.Controls.Add(this.textBoxName);
            this.panel4.Controls.Add(this.label22);
            this.panel4.Controls.Add(this.buttonReset);
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
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CrystalControl";
            this.Load += new System.EventHandler(this.CrystalForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormCrystal_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CrystalControl_KeyDown);
            this.Resize += new System.EventHandler(this.CrystalControl_Resize);
            this.tabControl.ResumeLayout(false);
            this.tabPageBasicInfo.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBoxSymmetry.ResumeLayout(false);
            this.groupBoxSymmetry.PerformLayout();
            this.tabPageAtom.ResumeLayout(false);
            this.tabPageAtom.PerformLayout();
            this.panelAtom.ResumeLayout(false);
            this.panelAtom.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabPageAtomAdvanced.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomTransparency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomAmbient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomShininess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomSpecular)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomDiffusion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomEmmision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomRadius)).EndInit();
            this.tabPageBondsPolyhedra.ResumeLayout(false);
            this.tabPageBondsPolyhedra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBondTrasparency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBondMaxLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBondMinLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBondRadius)).EndInit();
            this.groupBoxPolyhedron.ResumeLayout(false);
            this.groupBoxPolyhedron.PerformLayout();
            this.groupBoxEdge.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEdgeLineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPolyhedronPlaneAlpha)).EndInit();
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
            this.tabPageStraingStress.ResumeLayout(false);
            this.tabPageStraingStress.PerformLayout();
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
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage tabPageAtom;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBoxAtoms;
        private System.Windows.Forms.Button buttonChangeAtom;
        private System.Windows.Forms.Button buttonDeleteAtom;
        private System.Windows.Forms.Button buttonAddAtom;
        private System.Windows.Forms.TabPage tabPageReference;
        public System.Windows.Forms.TextBox textBoxTitle;
        public System.Windows.Forms.TextBox textBoxMemo;
        public System.Windows.Forms.TextBox textBoxAuthor;
        public System.Windows.Forms.TextBox textBoxJournal;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.GroupBox groupBoxSymmetry;
        public System.Windows.Forms.ComboBox comboBoxSpaceGroup;
        public System.Windows.Forms.ComboBox comboBoxPointGroup;
        public System.Windows.Forms.ComboBox comboBoxCrystalSystem;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox textBoxSearch;
        public System.Windows.Forms.ComboBox comboBoxSearchResult;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.TextBox textBoxFormula;
        public System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label22;
        public System.Windows.Forms.TextBox textBoxZnumber;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        public System.Windows.Forms.TextBox textBoxDensity;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownAtomShininess;
        private System.Windows.Forms.NumericUpDown numericUpDownAtomEmmision;
        private System.Windows.Forms.NumericUpDown numericUpDownAtomDiffusion;
        private System.Windows.Forms.NumericUpDown numericUpDownAtomAmbient;
        private System.Windows.Forms.NumericUpDown numericUpDownAtomTransparency;
        private System.Windows.Forms.Label label37;
        public System.Windows.Forms.TabPage tabPageAtomAdvanced;
        public System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBondsPolyhedra;
        private System.Windows.Forms.ListBox listBoxBondsAndPolyhedra;
        private System.Windows.Forms.NumericUpDown numericUpDownAtomSpecular;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Button buttonChangeToSameElement;
        private System.Windows.Forms.ComboBox comboBoxBondingAtom2;
        private System.Windows.Forms.ComboBox comboBoxBondingAtom1;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.NumericUpDown numericUpDownAtomRadius;
        private System.Windows.Forms.NumericUpDown numericUpDownBondMinLength;
        private System.Windows.Forms.NumericUpDown numericUpDownBondRadius;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Button buttonAddBond;
        private System.Windows.Forms.Button buttonChangeBond;
        private System.Windows.Forms.Button buttonDeleteBond;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.NumericUpDown numericUpDownBondMaxLength;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.NumericUpDown numericUpDownBondTrasparency;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.GroupBox groupBoxPolyhedron;
        private System.Windows.Forms.CheckBox checkBoxShowVertexAtoms;
        private System.Windows.Forms.CheckBox checkBoxShowCenterAtom;
        private System.Windows.Forms.NumericUpDown numericUpDownPolyhedronPlaneAlpha;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.CheckBox checkBoxShowEdges;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.NumericUpDown numericUpDownEdgeLineWidth;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.GroupBox groupBoxEdge;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.CheckBox checkBoxShowPolyhedron;
        private System.Windows.Forms.CheckBox checkBoxShowInnerBonds;
        private System.Windows.Forms.Label label58;
        public System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabPage tabPageBasicInfo;
        private System.Windows.Forms.Label label59;
        private NumericBox numericTextBoxA;
        private NumericBox numericTextBoxAlpha;
        private NumericBox numericTextBoxBeta;
        private NumericBox numericTextBoxGamma;
        private NumericBox numericTextBoxC;
        private NumericBox numericTextBoxB;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Button buttonAutoDetect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelAtom;
        private NumericBox numericTextBoxGammaErr;
        private NumericBox numericTextBoxCErr;
        private NumericBox numericTextBoxAlphaErr;
        private NumericBox numericTextBoxBetaErr;
        private NumericBox numericTextBoxBErr;
        private NumericBox numericTextBoxAErr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private ColorControl colorControl;
        private ColorControl colorControlAtomColor;
        private ColorControl colorControlBond;
        private ColorControl colorControlPlyhedron;
        private ColorControl colorControlEdges;
        private AtomInputControl atomInput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAtomDown;
        private System.Windows.Forms.Button buttonAtomUp;
        private System.Windows.Forms.ToolStripMenuItem importCrystalFromCIFAMCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendThisCrystalToOtherSoftwareToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scatteringFactorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem clearAllDataToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.Panel panel2;
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
        private NumericBox numericalTextBoxVolume;
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
        private System.Windows.Forms.TabPage tabPageStraingStress;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numericUpDownAngleSubDivision;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPoleFigure;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asTXTFileAllEulerAngleAndDensityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ToolStripMenuItem revertCellConstantsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem strainControlToolStripMenuItem;
    }
}
