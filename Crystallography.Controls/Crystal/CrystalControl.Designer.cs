namespace Crystallography.Controls
{
    partial class CrystalControl
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
        // (260323Ch) renamed numeric container controls:
        // groupBox5 -> groupBoxNote
        // groupBox6 -> groupBoxAuthors
        // groupBox7 -> groupBoxJournal
        // groupBox8 -> groupBoxTitle
        // panel5 -> panelSymmetryInfo
        // flowLayoutPanel4 -> flowLayoutPanelCellProperties
        // flowLayoutPanel5 -> flowLayoutPanelSymmetryActions
        // flowLayoutPanel3 -> flowLayoutPanelRandomOrientationActions
        // flowLayoutPanel2 -> flowLayoutPanelRandomOrientationSettings
        // panel1 -> panelFormula
        // panel4 -> panelNameAndActions
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CrystalControl));
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            groupBoxAuthors = new System.Windows.Forms.GroupBox();
            textBoxAuthor = new System.Windows.Forms.TextBox();
            groupBoxTitle = new System.Windows.Forms.GroupBox();
            textBoxTitle = new System.Windows.Forms.TextBox();
            tabControl = new System.Windows.Forms.TabControl();
            tabPageBasicInfo = new System.Windows.Forms.TabPage();
            panelSymmetryInfo = new System.Windows.Forms.Panel();
            flowLayoutPanelCellProperties = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxVolumeAng = new NumericBox();
            numericBoxCellVolumeNm = new NumericBox();
            numericBoxCellMass = new NumericBox();
            numericBoxMolarVolume = new NumericBox();
            numericBoxMolarMass = new NumericBox();
            numericBoxDensity = new NumericBox();
            flowLayoutPanelSymmetryActions = new System.Windows.Forms.FlowLayoutPanel();
            buttonConvertToSubgroup = new System.Windows.Forms.Button();
            buttonChangeAxesOriginSetting = new System.Windows.Forms.Button();
            symmetryControl = new SymmetryControl();
            tabPageAtom = new System.Windows.Forms.TabPage();
            atomControl = new AtomControl();
            panelAtom = new System.Windows.Forms.Panel();
            panel3 = new System.Windows.Forms.Panel();
            contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(components);
            resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tabPageBondsPolyhedra = new System.Windows.Forms.TabPage();
            bondControl = new BondInputControl();
            tabPageReference = new System.Windows.Forms.TabPage();
            groupBoxJournal = new System.Windows.Forms.GroupBox();
            textBoxJournal = new System.Windows.Forms.TextBox();
            groupBoxNote = new System.Windows.Forms.GroupBox();
            textBoxMemo = new System.Windows.Forms.TextBox();
            tabPageEOS = new System.Windows.Forms.TabPage();
            eosControl = new EOSControl();
            tabPageElasticity = new System.Windows.Forms.TabPage();
            elasticityControl1 = new ElasticityControl();
            tabPageStrainStress = new System.Windows.Forms.TabPage();
            buttonStressSet = new System.Windows.Forms.Button();
            numericBoxStrain33 = new NumericBox();
            numericBoxHill = new NumericBox();
            label116 = new System.Windows.Forms.Label();
            label117 = new System.Windows.Forms.Label();
            label109 = new System.Windows.Forms.Label();
            label110 = new System.Windows.Forms.Label();
            label111 = new System.Windows.Forms.Label();
            label112 = new System.Windows.Forms.Label();
            label113 = new System.Windows.Forms.Label();
            label114 = new System.Windows.Forms.Label();
            label115 = new System.Windows.Forms.Label();
            label102 = new System.Windows.Forms.Label();
            label103 = new System.Windows.Forms.Label();
            label104 = new System.Windows.Forms.Label();
            label105 = new System.Windows.Forms.Label();
            label106 = new System.Windows.Forms.Label();
            label107 = new System.Windows.Forms.Label();
            label108 = new System.Windows.Forms.Label();
            numericBoxStress33 = new NumericBox();
            numericBoxStress22 = new NumericBox();
            numericBoxStress11 = new NumericBox();
            numericBoxStress23 = new NumericBox();
            numericBoxStress13 = new NumericBox();
            numericBoxStress12 = new NumericBox();
            numericBoxStrain11 = new NumericBox();
            numericBoxStrain22 = new NumericBox();
            numericBoxStrain12 = new NumericBox();
            numericBoxStrain23 = new NumericBox();
            numericBoxStrain13 = new NumericBox();
            tabPagePolycrystalline = new System.Windows.Forms.TabPage();
            contextMenuStripPoleFigure = new System.Windows.Forms.ContextMenuStrip(components);
            readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            asTXTFileAllEulerAngleAndDensityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            poleFigureControl = new PoleFigureControl();
            flowLayoutPanelRandomOrientationActions = new System.Windows.Forms.FlowLayoutPanel();
            buttonGenerateRandomOrientations = new System.Windows.Forms.Button();
            flowLayoutPanelRandomOrientationSettings = new System.Windows.Forms.FlowLayoutPanel();
            label5 = new System.Windows.Forms.Label();
            numericUpDownAngleResolution = new System.Windows.Forms.NumericUpDown();
            label29 = new System.Windows.Forms.Label();
            numericUpDownAngleSubDivision = new System.Windows.Forms.NumericUpDown();
            label101 = new System.Windows.Forms.Label();
            numericUpDownCrystallineSize = new System.Windows.Forms.NumericUpDown();
            label99 = new System.Windows.Forms.Label();
            tabPageBounds = new System.Windows.Forms.TabPage();
            boundControl = new BoundControl();
            tabPageLatticePlane = new System.Windows.Forms.TabPage();
            latticePlaneControl = new LatticePlaneControl();
            colorControl = new ColorControl();
            panelFormula = new System.Windows.Forms.Panel();
            textBoxFormula = new System.Windows.Forms.TextBox();
            label90 = new System.Windows.Forms.Label();
            numericBoxZnumber = new NumericBox();
            contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
            scatteringFactorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            symmetryInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            importCrystalFromCIFAMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportThisCrystalAsCIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            sendThisCrystalToOtherSoftwareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            revertCellConstantsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            strainControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            convertToP1SymmetryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            convertToSuperstructureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            convertToAnotherSpacegroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            textBoxName = new System.Windows.Forms.TextBox();
            label22 = new System.Windows.Forms.Label();
            toolTip = new System.Windows.Forms.ToolTip(components);
            buttonScatteringFactor = new System.Windows.Forms.Button();
            buttonSymmetryInfo = new System.Windows.Forms.Button();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            panelNameAndActions = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            groupBoxAuthors.SuspendLayout();
            groupBoxTitle.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageBasicInfo.SuspendLayout();
            panelSymmetryInfo.SuspendLayout();
            flowLayoutPanelCellProperties.SuspendLayout();
            flowLayoutPanelSymmetryActions.SuspendLayout();
            tabPageAtom.SuspendLayout();
            panelAtom.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            tabPageBondsPolyhedra.SuspendLayout();
            tabPageReference.SuspendLayout();
            groupBoxJournal.SuspendLayout();
            groupBoxNote.SuspendLayout();
            tabPageEOS.SuspendLayout();
            tabPageElasticity.SuspendLayout();
            tabPageStrainStress.SuspendLayout();
            tabPagePolycrystalline.SuspendLayout();
            contextMenuStripPoleFigure.SuspendLayout();
            flowLayoutPanelRandomOrientationActions.SuspendLayout();
            flowLayoutPanelRandomOrientationSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAngleResolution).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAngleSubDivision).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCrystallineSize).BeginInit();
            tabPageBounds.SuspendLayout();
            tabPageLatticePlane.SuspendLayout();
            panelFormula.SuspendLayout();
            contextMenuStrip.SuspendLayout();
            panelNameAndActions.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(groupBoxAuthors);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBoxTitle);
            // 
            // groupBoxAuthors
            // 
            groupBoxAuthors.Controls.Add(textBoxAuthor);
            resources.ApplyResources(groupBoxAuthors, "groupBoxAuthors");
            groupBoxAuthors.Name = "groupBoxAuthors";
            groupBoxAuthors.TabStop = false;
            // 
            // textBoxAuthor
            // 
            textBoxAuthor.AcceptsReturn = true;
            resources.ApplyResources(textBoxAuthor, "textBoxAuthor");
            textBoxAuthor.Name = "textBoxAuthor";
            // 
            // groupBoxTitle
            // 
            groupBoxTitle.Controls.Add(textBoxTitle);
            resources.ApplyResources(groupBoxTitle, "groupBoxTitle");
            groupBoxTitle.Name = "groupBoxTitle";
            groupBoxTitle.TabStop = false;
            // 
            // textBoxTitle
            // 
            textBoxTitle.AcceptsReturn = true;
            resources.ApplyResources(textBoxTitle, "textBoxTitle");
            textBoxTitle.Name = "textBoxTitle";
            // 
            // tabControl
            // 
            tabControl.AllowDrop = true;
            tabControl.Controls.Add(tabPageBasicInfo);
            tabControl.Controls.Add(tabPageAtom);
            tabControl.Controls.Add(tabPageBondsPolyhedra);
            tabControl.Controls.Add(tabPageReference);
            tabControl.Controls.Add(tabPageEOS);
            tabControl.Controls.Add(tabPageElasticity);
            tabControl.Controls.Add(tabPageStrainStress);
            tabControl.Controls.Add(tabPagePolycrystalline);
            tabControl.Controls.Add(tabPageBounds);
            tabControl.Controls.Add(tabPageLatticePlane);
            resources.ApplyResources(tabControl, "tabControl");
            tabControl.HotTrack = true;
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.DragDrop += FormCrystal_DragDrop;
            tabControl.DragEnter += FormCrystal_DragEnter;
            // 
            // tabPageBasicInfo
            // 
            tabPageBasicInfo.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageBasicInfo, true);
            tabPageBasicInfo.Controls.Add(panelSymmetryInfo);
            resources.ApplyResources(tabPageBasicInfo, "tabPageBasicInfo");
            tabPageBasicInfo.Name = "tabPageBasicInfo";
            toolTip.SetToolTip(tabPageBasicInfo, resources.GetString("tabPageBasicInfo.ToolTip"));
            // 
            // panelSymmetryInfo
            // 
            panelSymmetryInfo.Controls.Add(flowLayoutPanelCellProperties);
            panelSymmetryInfo.Controls.Add(flowLayoutPanelSymmetryActions);
            panelSymmetryInfo.Controls.Add(symmetryControl);
            resources.ApplyResources(panelSymmetryInfo, "panelSymmetryInfo");
            panelSymmetryInfo.Name = "panelSymmetryInfo";
            // 
            // flowLayoutPanelCellProperties
            // 
            resources.ApplyResources(flowLayoutPanelCellProperties, "flowLayoutPanelCellProperties");
            flowLayoutPanelCellProperties.Controls.Add(numericBoxVolumeAng);
            flowLayoutPanelCellProperties.Controls.Add(numericBoxCellVolumeNm);
            flowLayoutPanelCellProperties.Controls.Add(numericBoxCellMass);
            flowLayoutPanelCellProperties.Controls.Add(numericBoxMolarVolume);
            flowLayoutPanelCellProperties.Controls.Add(numericBoxMolarMass);
            flowLayoutPanelCellProperties.Controls.Add(numericBoxDensity);
            flowLayoutPanelCellProperties.Name = "flowLayoutPanelCellProperties";
            // 
            // numericBoxVolumeAng
            // 
            resources.ApplyResources(numericBoxVolumeAng, "numericBoxVolumeAng");
            numericBoxVolumeAng.BackColor = System.Drawing.SystemColors.Control;
            numericBoxVolumeAng.DecimalPlaces = 4;
            numericBoxVolumeAng.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxVolumeAng.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxVolumeAng.Name = "numericBoxVolumeAng";
            numericBoxVolumeAng.ReadOnly = true;
            numericBoxVolumeAng.RestrictLimitValue = false;
            numericBoxVolumeAng.SkipEventDuringInput = false;
            numericBoxVolumeAng.SmartIncrement = true;
            numericBoxVolumeAng.TabStop = false;
            numericBoxVolumeAng.TextBoxBackColor = System.Drawing.SystemColors.Control;
            toolTip.SetToolTip(numericBoxVolumeAng, resources.GetString("numericBoxVolumeAng.ToolTip"));
            // 
            // numericBoxCellVolumeNm
            // 
            resources.ApplyResources(numericBoxCellVolumeNm, "numericBoxCellVolumeNm");
            numericBoxCellVolumeNm.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCellVolumeNm.DecimalPlaces = 7;
            numericBoxCellVolumeNm.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCellVolumeNm.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCellVolumeNm.Name = "numericBoxCellVolumeNm";
            numericBoxCellVolumeNm.ReadOnly = true;
            numericBoxCellVolumeNm.RestrictLimitValue = false;
            numericBoxCellVolumeNm.SkipEventDuringInput = false;
            numericBoxCellVolumeNm.SmartIncrement = true;
            numericBoxCellVolumeNm.TabStop = false;
            numericBoxCellVolumeNm.TextBoxBackColor = System.Drawing.SystemColors.Control;
            // 
            // numericBoxCellMass
            // 
            resources.ApplyResources(numericBoxCellMass, "numericBoxCellMass");
            numericBoxCellMass.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCellMass.DecimalPlaces = 4;
            numericBoxCellMass.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCellMass.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCellMass.Name = "numericBoxCellMass";
            numericBoxCellMass.ReadOnly = true;
            numericBoxCellMass.RestrictLimitValue = false;
            numericBoxCellMass.SkipEventDuringInput = false;
            numericBoxCellMass.SmartIncrement = true;
            numericBoxCellMass.TabStop = false;
            numericBoxCellMass.TextBoxBackColor = System.Drawing.SystemColors.Control;
            toolTip.SetToolTip(numericBoxCellMass, resources.GetString("numericBoxCellMass.ToolTip"));
            // 
            // numericBoxMolarVolume
            // 
            resources.ApplyResources(numericBoxMolarVolume, "numericBoxMolarVolume");
            numericBoxMolarVolume.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMolarVolume.DecimalPlaces = 4;
            numericBoxMolarVolume.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxMolarVolume.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxMolarVolume.Name = "numericBoxMolarVolume";
            numericBoxMolarVolume.ReadOnly = true;
            numericBoxMolarVolume.RestrictLimitValue = false;
            numericBoxMolarVolume.SkipEventDuringInput = false;
            numericBoxMolarVolume.SmartIncrement = true;
            numericBoxMolarVolume.TabStop = false;
            numericBoxMolarVolume.TextBoxBackColor = System.Drawing.SystemColors.Control;
            toolTip.SetToolTip(numericBoxMolarVolume, resources.GetString("numericBoxMolarVolume.ToolTip"));
            // 
            // numericBoxMolarMass
            // 
            resources.ApplyResources(numericBoxMolarMass, "numericBoxMolarMass");
            numericBoxMolarMass.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMolarMass.DecimalPlaces = 4;
            numericBoxMolarMass.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxMolarMass.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxMolarMass.Name = "numericBoxMolarMass";
            numericBoxMolarMass.ReadOnly = true;
            numericBoxMolarMass.RestrictLimitValue = false;
            numericBoxMolarMass.SkipEventDuringInput = false;
            numericBoxMolarMass.SmartIncrement = true;
            numericBoxMolarMass.TabStop = false;
            numericBoxMolarMass.TextBoxBackColor = System.Drawing.SystemColors.Control;
            toolTip.SetToolTip(numericBoxMolarMass, resources.GetString("numericBoxMolarMass.ToolTip"));
            // 
            // numericBoxDensity
            // 
            resources.ApplyResources(numericBoxDensity, "numericBoxDensity");
            numericBoxDensity.BackColor = System.Drawing.Color.Transparent;
            numericBoxDensity.DecimalPlaces = 4;
            numericBoxDensity.Name = "numericBoxDensity";
            numericBoxDensity.ReadOnly = true;
            numericBoxDensity.SkipEventDuringInput = false;
            numericBoxDensity.SmartIncrement = true;
            numericBoxDensity.TabStop = false;
            numericBoxDensity.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBoxDensity.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxDensity, resources.GetString("numericBoxDensity.ToolTip"));
            // 
            // flowLayoutPanelSymmetryActions
            // 
            resources.ApplyResources(flowLayoutPanelSymmetryActions, "flowLayoutPanelSymmetryActions");
            flowLayoutPanelSymmetryActions.Controls.Add(buttonConvertToSubgroup);
            flowLayoutPanelSymmetryActions.Controls.Add(buttonChangeAxesOriginSetting);
            flowLayoutPanelSymmetryActions.Name = "flowLayoutPanelSymmetryActions";
            // 
            // buttonConvertToSubgroup
            // 
            resources.ApplyResources(buttonConvertToSubgroup, "buttonConvertToSubgroup");
            buttonConvertToSubgroup.Name = "buttonConvertToSubgroup";
            buttonConvertToSubgroup.UseVisualStyleBackColor = true;
            // 
            // buttonChangeAxesOriginSetting
            // 
            resources.ApplyResources(buttonChangeAxesOriginSetting, "buttonChangeAxesOriginSetting");
            buttonChangeAxesOriginSetting.Name = "buttonChangeAxesOriginSetting";
            buttonChangeAxesOriginSetting.UseVisualStyleBackColor = true;
            buttonChangeAxesOriginSetting.Click += buttonChangeAxesOriginSetting_Click;
            // 
            // symmetryControl
            // 
            symmetryControl.A = 0D;
            symmetryControl.Alpha = 0D;
            symmetryControl.B = 0D;
            symmetryControl.Beta = 0D;
            symmetryControl.C = 0D;
            resources.ApplyResources(symmetryControl, "symmetryControl");
            symmetryControl.Gamma = 0D;
            symmetryControl.LengthUnit = LengthUnitEnum.Angstrom;
            symmetryControl.Name = "symmetryControl";
            symmetryControl.ShowError = false;
            symmetryControl.SymmetrySeriesNumber = 0;
            symmetryControl.ItemChanged += symmetryControl_ItemChanged;
            // 
            // tabPageAtom
            // 
            tabPageAtom.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageAtom, true);
            tabPageAtom.Controls.Add(atomControl);
            tabPageAtom.Controls.Add(panelAtom);
            resources.ApplyResources(tabPageAtom, "tabPageAtom");
            tabPageAtom.Name = "tabPageAtom";
            toolTip.SetToolTip(tabPageAtom, resources.GetString("tabPageAtom.ToolTip"));
            // 
            // atomControl
            // 
            atomControl.Alpha = 0F;
            atomControl.Ambient = 0F;
            atomControl.Aniso11 = 0D;
            atomControl.Aniso11Err = 0D;
            atomControl.Aniso12 = 0D;
            atomControl.Aniso12Err = 0D;
            atomControl.Aniso13 = 0D;
            atomControl.Aniso13Err = 0D;
            atomControl.Aniso22 = 0D;
            atomControl.Aniso22Err = 0D;
            atomControl.Aniso23 = 0D;
            atomControl.Aniso23Err = 0D;
            atomControl.Aniso33 = 0D;
            atomControl.Aniso33Err = 0D;
            atomControl.AppearanceTabVisible = false;
            atomControl.AtomColor = System.Drawing.Color.FromArgb(240, 240, 240);
            atomControl.AtomicPositionError = false;
            atomControl.AtomNo = 0;
            atomControl.AtomSubNoElectron = -1;
            atomControl.AtomSubNoXray = -1;
            resources.ApplyResources(atomControl, "atomControl");
            atomControl.DebyeWallerError = false;
            atomControl.DebyeWallerTabVisible = true;
            atomControl.Diffusion = 0F;
            atomControl.ElementAndPositionTabVisible = true;
            atomControl.Emission = 0F;
            atomControl.Iso = 0D;
            atomControl.IsoErr = 0D;
            atomControl.Label = "";
            atomControl.Name = "atomControl";
            atomControl.Occ = 0D;
            atomControl.OccErr = 0D;
            atomControl.OriginShiftVisible = true;
            atomControl.Radius = 0D;
            atomControl.ScatteringFactorTabVisible = true;
            atomControl.SelectedTabIndex = 0;
            atomControl.Shininess = 0F;
            atomControl.ShowLabel = false;
            atomControl.Specular = 0F;
            atomControl.UseIsotropy = false;
            atomControl.UseTypeU = false;
            atomControl.X = 0D;
            atomControl.XErr = 0D;
            atomControl.Y = 0D;
            atomControl.YErr = 0D;
            atomControl.Z = 0D;
            atomControl.ZErr = 0D;
            atomControl.ItemsChanged += atomControl_AtomsChanged;
            // 
            // panelAtom
            // 
            resources.ApplyResources(panelAtom, "panelAtom");
            panelAtom.Controls.Add(panel3);
            panelAtom.Name = "panelAtom";
            // 
            // panel3
            // 
            resources.ApplyResources(panel3, "panel3");
            panel3.ContextMenuStrip = contextMenuStrip2;
            panel3.Name = "panel3";
            // 
            // contextMenuStrip2
            // 
            resources.ApplyResources(contextMenuStrip2, "contextMenuStrip2");
            contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { resetToolStripMenuItem });
            contextMenuStrip2.Name = "contextMenuStrip2";
            // 
            // resetToolStripMenuItem
            // 
            resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            resources.ApplyResources(resetToolStripMenuItem, "resetToolStripMenuItem");
            resetToolStripMenuItem.Click += resetToolStripMenuItem_Click;
            // 
            // tabPageBondsPolyhedra
            // 
            tabPageBondsPolyhedra.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageBondsPolyhedra, true);
            tabPageBondsPolyhedra.Controls.Add(bondControl);
            resources.ApplyResources(tabPageBondsPolyhedra, "tabPageBondsPolyhedra");
            tabPageBondsPolyhedra.Name = "tabPageBondsPolyhedra";
            // 
            // bondControl
            // 
            resources.ApplyResources(bondControl, "bondControl");
            bondControl.Name = "bondControl";
            // 
            // tabPageReference
            // 
            tabPageReference.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageReference, true);
            tabPageReference.Controls.Add(splitContainer1);
            tabPageReference.Controls.Add(groupBoxJournal);
            tabPageReference.Controls.Add(groupBoxNote);
            resources.ApplyResources(tabPageReference, "tabPageReference");
            tabPageReference.Name = "tabPageReference";
            // 
            // groupBoxJournal
            // 
            groupBoxJournal.Controls.Add(textBoxJournal);
            resources.ApplyResources(groupBoxJournal, "groupBoxJournal");
            groupBoxJournal.Name = "groupBoxJournal";
            groupBoxJournal.TabStop = false;
            // 
            // textBoxJournal
            // 
            textBoxJournal.AcceptsReturn = true;
            resources.ApplyResources(textBoxJournal, "textBoxJournal");
            textBoxJournal.Name = "textBoxJournal";
            // 
            // groupBoxNote
            // 
            groupBoxNote.Controls.Add(textBoxMemo);
            resources.ApplyResources(groupBoxNote, "groupBoxNote");
            groupBoxNote.Name = "groupBoxNote";
            groupBoxNote.TabStop = false;
            // 
            // textBoxMemo
            // 
            resources.ApplyResources(textBoxMemo, "textBoxMemo");
            textBoxMemo.Name = "textBoxMemo";
            // 
            // tabPageEOS
            // 
            tabPageEOS.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageEOS, true);
            tabPageEOS.Controls.Add(eosControl);
            resources.ApplyResources(tabPageEOS, "tabPageEOS");
            tabPageEOS.Name = "tabPageEOS";
            // 
            // eosControl
            // 
            resources.ApplyResources(eosControl, "eosControl");
            eosControl.Name = "eosControl";
            // 
            // tabPageElasticity
            // 
            tabPageElasticity.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageElasticity, true);
            tabPageElasticity.Controls.Add(elasticityControl1);
            resources.ApplyResources(tabPageElasticity, "tabPageElasticity");
            tabPageElasticity.Name = "tabPageElasticity";
            // 
            // elasticityControl1
            // 
            resources.ApplyResources(elasticityControl1, "elasticityControl1");
            elasticityControl1.Mode = Elasticity.Mode.Stiffness;
            elasticityControl1.Name = "elasticityControl1";
            elasticityControl1.SymmetrySeriesNumber = 1;
            elasticityControl1.ValueChanged += elasticityControl1_ValueChanged;
            // 
            // tabPageStrainStress
            // 
            tabPageStrainStress.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageStrainStress, true);
            tabPageStrainStress.Controls.Add(buttonStressSet);
            tabPageStrainStress.Controls.Add(numericBoxStrain33);
            tabPageStrainStress.Controls.Add(numericBoxHill);
            tabPageStrainStress.Controls.Add(label116);
            tabPageStrainStress.Controls.Add(label117);
            tabPageStrainStress.Controls.Add(label109);
            tabPageStrainStress.Controls.Add(label110);
            tabPageStrainStress.Controls.Add(label111);
            tabPageStrainStress.Controls.Add(label112);
            tabPageStrainStress.Controls.Add(label113);
            tabPageStrainStress.Controls.Add(label114);
            tabPageStrainStress.Controls.Add(label115);
            tabPageStrainStress.Controls.Add(label102);
            tabPageStrainStress.Controls.Add(label103);
            tabPageStrainStress.Controls.Add(label104);
            tabPageStrainStress.Controls.Add(label105);
            tabPageStrainStress.Controls.Add(label106);
            tabPageStrainStress.Controls.Add(label107);
            tabPageStrainStress.Controls.Add(label108);
            tabPageStrainStress.Controls.Add(numericBoxStress33);
            tabPageStrainStress.Controls.Add(numericBoxStress22);
            tabPageStrainStress.Controls.Add(numericBoxStress11);
            tabPageStrainStress.Controls.Add(numericBoxStress23);
            tabPageStrainStress.Controls.Add(numericBoxStress13);
            tabPageStrainStress.Controls.Add(numericBoxStress12);
            tabPageStrainStress.Controls.Add(numericBoxStrain11);
            tabPageStrainStress.Controls.Add(numericBoxStrain22);
            tabPageStrainStress.Controls.Add(numericBoxStrain12);
            tabPageStrainStress.Controls.Add(numericBoxStrain23);
            tabPageStrainStress.Controls.Add(numericBoxStrain13);
            resources.ApplyResources(tabPageStrainStress, "tabPageStrainStress");
            tabPageStrainStress.Name = "tabPageStrainStress";
            // 
            // buttonStressSet
            // 
            resources.ApplyResources(buttonStressSet, "buttonStressSet");
            buttonStressSet.Name = "buttonStressSet";
            buttonStressSet.UseVisualStyleBackColor = true;
            buttonStressSet.Click += buttonStressSet_Click;
            // 
            // numericBoxStrain33
            // 
            resources.ApplyResources(numericBoxStrain33, "numericBoxStrain33");
            numericBoxStrain33.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain33.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain33.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain33.Name = "numericBoxStrain33";
            numericBoxStrain33.RestrictLimitValue = false;
            numericBoxStrain33.SkipEventDuringInput = false;
            numericBoxStrain33.SmartIncrement = true;
            // 
            // numericBoxHill
            // 
            resources.ApplyResources(numericBoxHill, "numericBoxHill");
            numericBoxHill.BackColor = System.Drawing.SystemColors.Control;
            numericBoxHill.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxHill.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxHill.Name = "numericBoxHill";
            numericBoxHill.RadianValue = 0.017453292519943295D;
            numericBoxHill.RestrictLimitValue = false;
            numericBoxHill.SkipEventDuringInput = false;
            numericBoxHill.SmartIncrement = true;
            numericBoxHill.Value = 1D;
            // 
            // label116
            // 
            resources.ApplyResources(label116, "label116");
            label116.Name = "label116";
            // 
            // label117
            // 
            resources.ApplyResources(label117, "label117");
            label117.Name = "label117";
            // 
            // label109
            // 
            resources.ApplyResources(label109, "label109");
            label109.Name = "label109";
            // 
            // label110
            // 
            resources.ApplyResources(label110, "label110");
            label110.Name = "label110";
            // 
            // label111
            // 
            resources.ApplyResources(label111, "label111");
            label111.Name = "label111";
            // 
            // label112
            // 
            resources.ApplyResources(label112, "label112");
            label112.Name = "label112";
            // 
            // label113
            // 
            resources.ApplyResources(label113, "label113");
            label113.Name = "label113";
            // 
            // label114
            // 
            resources.ApplyResources(label114, "label114");
            label114.Name = "label114";
            // 
            // label115
            // 
            resources.ApplyResources(label115, "label115");
            label115.Name = "label115";
            // 
            // label102
            // 
            resources.ApplyResources(label102, "label102");
            label102.Name = "label102";
            // 
            // label103
            // 
            resources.ApplyResources(label103, "label103");
            label103.Name = "label103";
            // 
            // label104
            // 
            resources.ApplyResources(label104, "label104");
            label104.Name = "label104";
            // 
            // label105
            // 
            resources.ApplyResources(label105, "label105");
            label105.Name = "label105";
            // 
            // label106
            // 
            resources.ApplyResources(label106, "label106");
            label106.Name = "label106";
            // 
            // label107
            // 
            resources.ApplyResources(label107, "label107");
            label107.Name = "label107";
            // 
            // label108
            // 
            resources.ApplyResources(label108, "label108");
            label108.Name = "label108";
            // 
            // numericBoxStress33
            // 
            resources.ApplyResources(numericBoxStress33, "numericBoxStress33");
            numericBoxStress33.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStress33.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress33.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress33.Name = "numericBoxStress33";
            numericBoxStress33.RestrictLimitValue = false;
            numericBoxStress33.SkipEventDuringInput = false;
            numericBoxStress33.SmartIncrement = true;
            // 
            // numericBoxStress22
            // 
            resources.ApplyResources(numericBoxStress22, "numericBoxStress22");
            numericBoxStress22.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStress22.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress22.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress22.Name = "numericBoxStress22";
            numericBoxStress22.RestrictLimitValue = false;
            numericBoxStress22.SkipEventDuringInput = false;
            numericBoxStress22.SmartIncrement = true;
            // 
            // numericBoxStress11
            // 
            resources.ApplyResources(numericBoxStress11, "numericBoxStress11");
            numericBoxStress11.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStress11.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress11.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress11.Name = "numericBoxStress11";
            numericBoxStress11.RestrictLimitValue = false;
            numericBoxStress11.SkipEventDuringInput = false;
            numericBoxStress11.SmartIncrement = true;
            // 
            // numericBoxStress23
            // 
            resources.ApplyResources(numericBoxStress23, "numericBoxStress23");
            numericBoxStress23.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStress23.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress23.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress23.Name = "numericBoxStress23";
            numericBoxStress23.RestrictLimitValue = false;
            numericBoxStress23.SkipEventDuringInput = false;
            numericBoxStress23.SmartIncrement = true;
            // 
            // numericBoxStress13
            // 
            resources.ApplyResources(numericBoxStress13, "numericBoxStress13");
            numericBoxStress13.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStress13.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress13.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress13.Name = "numericBoxStress13";
            numericBoxStress13.RestrictLimitValue = false;
            numericBoxStress13.SkipEventDuringInput = false;
            numericBoxStress13.SmartIncrement = true;
            // 
            // numericBoxStress12
            // 
            resources.ApplyResources(numericBoxStress12, "numericBoxStress12");
            numericBoxStress12.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStress12.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress12.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStress12.Name = "numericBoxStress12";
            numericBoxStress12.RestrictLimitValue = false;
            numericBoxStress12.SkipEventDuringInput = false;
            numericBoxStress12.SmartIncrement = true;
            // 
            // numericBoxStrain11
            // 
            resources.ApplyResources(numericBoxStrain11, "numericBoxStrain11");
            numericBoxStrain11.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain11.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain11.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain11.Name = "numericBoxStrain11";
            numericBoxStrain11.RestrictLimitValue = false;
            numericBoxStrain11.SkipEventDuringInput = false;
            numericBoxStrain11.SmartIncrement = true;
            // 
            // numericBoxStrain22
            // 
            resources.ApplyResources(numericBoxStrain22, "numericBoxStrain22");
            numericBoxStrain22.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain22.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain22.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain22.Name = "numericBoxStrain22";
            numericBoxStrain22.RestrictLimitValue = false;
            numericBoxStrain22.SkipEventDuringInput = false;
            numericBoxStrain22.SmartIncrement = true;
            // 
            // numericBoxStrain12
            // 
            resources.ApplyResources(numericBoxStrain12, "numericBoxStrain12");
            numericBoxStrain12.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain12.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain12.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain12.Name = "numericBoxStrain12";
            numericBoxStrain12.RestrictLimitValue = false;
            numericBoxStrain12.SkipEventDuringInput = false;
            numericBoxStrain12.SmartIncrement = true;
            // 
            // numericBoxStrain23
            // 
            resources.ApplyResources(numericBoxStrain23, "numericBoxStrain23");
            numericBoxStrain23.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain23.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain23.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain23.Name = "numericBoxStrain23";
            numericBoxStrain23.RestrictLimitValue = false;
            numericBoxStrain23.SkipEventDuringInput = false;
            numericBoxStrain23.SmartIncrement = true;
            // 
            // numericBoxStrain13
            // 
            resources.ApplyResources(numericBoxStrain13, "numericBoxStrain13");
            numericBoxStrain13.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain13.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain13.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxStrain13.Name = "numericBoxStrain13";
            numericBoxStrain13.RestrictLimitValue = false;
            numericBoxStrain13.SkipEventDuringInput = false;
            numericBoxStrain13.SmartIncrement = true;
            // 
            // tabPagePolycrystalline
            // 
            tabPagePolycrystalline.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPagePolycrystalline, true);
            tabPagePolycrystalline.ContextMenuStrip = contextMenuStripPoleFigure;
            tabPagePolycrystalline.Controls.Add(poleFigureControl);
            tabPagePolycrystalline.Controls.Add(flowLayoutPanelRandomOrientationActions);
            tabPagePolycrystalline.Controls.Add(flowLayoutPanelRandomOrientationSettings);
            resources.ApplyResources(tabPagePolycrystalline, "tabPagePolycrystalline");
            tabPagePolycrystalline.Name = "tabPagePolycrystalline";
            // 
            // contextMenuStripPoleFigure
            // 
            contextMenuStripPoleFigure.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStripPoleFigure.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { readToolStripMenuItem, saveToolStripMenuItem, exportToolStripMenuItem });
            contextMenuStripPoleFigure.Name = "contextMenuStripPoleFigure";
            resources.ApplyResources(contextMenuStripPoleFigure, "contextMenuStripPoleFigure");
            // 
            // readToolStripMenuItem
            // 
            readToolStripMenuItem.Name = "readToolStripMenuItem";
            resources.ApplyResources(readToolStripMenuItem, "readToolStripMenuItem");
            readToolStripMenuItem.Click += readToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(saveToolStripMenuItem, "saveToolStripMenuItem");
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem, asTXTFileAllEulerAngleAndDensityToolStripMenuItem, asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            resources.ApplyResources(exportToolStripMenuItem, "exportToolStripMenuItem");
            // 
            // asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem
            // 
            asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem.Name = "asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem";
            resources.ApplyResources(asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem, "asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem");
            asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem.Click += asCTFFilecomatibleToCHANNEL5FileToolStripMenuItem_Click;
            // 
            // asTXTFileAllEulerAngleAndDensityToolStripMenuItem
            // 
            asTXTFileAllEulerAngleAndDensityToolStripMenuItem.Name = "asTXTFileAllEulerAngleAndDensityToolStripMenuItem";
            resources.ApplyResources(asTXTFileAllEulerAngleAndDensityToolStripMenuItem, "asTXTFileAllEulerAngleAndDensityToolStripMenuItem");
            asTXTFileAllEulerAngleAndDensityToolStripMenuItem.Click += asTXTFileAllEulerAngleAndDensityToolStripMenuItem_Click;
            // 
            // asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem
            // 
            asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem.Name = "asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem";
            resources.ApplyResources(asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem, "asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem");
            asTXTFileallEulerAngleAndDensitySortedToolStripMenuItem.Click += asTXTFileAllEulerAngleAndDensityToolStripMenuItem_Click;
            // 
            // poleFigureControl
            // 
            resources.ApplyResources(poleFigureControl, "poleFigureControl");
            poleFigureControl.Name = "poleFigureControl";
            // 
            // flowLayoutPanelRandomOrientationActions
            // 
            resources.ApplyResources(flowLayoutPanelRandomOrientationActions, "flowLayoutPanelRandomOrientationActions");
            flowLayoutPanelRandomOrientationActions.Controls.Add(buttonGenerateRandomOrientations);
            flowLayoutPanelRandomOrientationActions.Name = "flowLayoutPanelRandomOrientationActions";
            // 
            // buttonGenerateRandomOrientations
            // 
            resources.ApplyResources(buttonGenerateRandomOrientations, "buttonGenerateRandomOrientations");
            buttonGenerateRandomOrientations.Name = "buttonGenerateRandomOrientations";
            buttonGenerateRandomOrientations.UseVisualStyleBackColor = true;
            buttonGenerateRandomOrientations.Click += buttonGenerateRandomOrientations_Click;
            // 
            // flowLayoutPanelRandomOrientationSettings
            // 
            resources.ApplyResources(flowLayoutPanelRandomOrientationSettings, "flowLayoutPanelRandomOrientationSettings");
            flowLayoutPanelRandomOrientationSettings.Controls.Add(label5);
            flowLayoutPanelRandomOrientationSettings.Controls.Add(numericUpDownAngleResolution);
            flowLayoutPanelRandomOrientationSettings.Controls.Add(label29);
            flowLayoutPanelRandomOrientationSettings.Controls.Add(numericUpDownAngleSubDivision);
            flowLayoutPanelRandomOrientationSettings.Controls.Add(label101);
            flowLayoutPanelRandomOrientationSettings.Controls.Add(numericUpDownCrystallineSize);
            flowLayoutPanelRandomOrientationSettings.Controls.Add(label99);
            flowLayoutPanelRandomOrientationSettings.Name = "flowLayoutPanelRandomOrientationSettings";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // numericUpDownAngleResolution
            // 
            numericUpDownAngleResolution.DecimalPlaces = 1;
            resources.ApplyResources(numericUpDownAngleResolution, "numericUpDownAngleResolution");
            numericUpDownAngleResolution.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            numericUpDownAngleResolution.Maximum = new decimal(new int[] { 45, 0, 0, 0 });
            numericUpDownAngleResolution.Minimum = new decimal(new int[] { 5, 0, 0, 65536 });
            numericUpDownAngleResolution.Name = "numericUpDownAngleResolution";
            numericUpDownAngleResolution.Value = new decimal(new int[] { 2, 0, 0, 0 });
            numericUpDownAngleResolution.ValueChanged += numericUpDownAngleResolution_ValueChanged;
            // 
            // label29
            // 
            resources.ApplyResources(label29, "label29");
            label29.Name = "label29";
            // 
            // numericUpDownAngleSubDivision
            // 
            resources.ApplyResources(numericUpDownAngleSubDivision, "numericUpDownAngleSubDivision");
            numericUpDownAngleSubDivision.Maximum = new decimal(new int[] { 45, 0, 0, 0 });
            numericUpDownAngleSubDivision.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownAngleSubDivision.Name = "numericUpDownAngleSubDivision";
            numericUpDownAngleSubDivision.Value = new decimal(new int[] { 4, 0, 0, 0 });
            numericUpDownAngleSubDivision.ValueChanged += numericUpDownAngleResolution_ValueChanged;
            // 
            // label101
            // 
            resources.ApplyResources(label101, "label101");
            label101.Name = "label101";
            // 
            // numericUpDownCrystallineSize
            // 
            resources.ApplyResources(numericUpDownCrystallineSize, "numericUpDownCrystallineSize");
            numericUpDownCrystallineSize.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownCrystallineSize.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownCrystallineSize.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownCrystallineSize.Name = "numericUpDownCrystallineSize";
            numericUpDownCrystallineSize.Value = new decimal(new int[] { 100, 0, 0, 0 });
            numericUpDownCrystallineSize.ValueChanged += numericUpDownAngleResolution_ValueChanged;
            // 
            // label99
            // 
            resources.ApplyResources(label99, "label99");
            label99.Name = "label99";
            // 
            // tabPageBounds
            // 
            captureExtender.SetCapture(tabPageBounds, true);
            tabPageBounds.Controls.Add(boundControl);
            resources.ApplyResources(tabPageBounds, "tabPageBounds");
            tabPageBounds.Name = "tabPageBounds";
            // 
            // boundControl
            // 
            resources.ApplyResources(boundControl, "boundControl");
            boundControl.Name = "boundControl";
            // 
            // tabPageLatticePlane
            // 
            tabPageLatticePlane.Controls.Add(latticePlaneControl);
            resources.ApplyResources(tabPageLatticePlane, "tabPageLatticePlane");
            tabPageLatticePlane.Name = "tabPageLatticePlane";
            // 
            // latticePlaneControl
            // 
            captureExtender.SetCapture(latticePlaneControl, true);
            resources.ApplyResources(latticePlaneControl, "latticePlaneControl");
            latticePlaneControl.Name = "latticePlaneControl";
            // 
            // colorControl
            // 
            colorControl.Argb = -986896;
            resources.ApplyResources(colorControl, "colorControl");
            colorControl.BackColor = System.Drawing.SystemColors.Control;
            colorControl.Blue = 240;
            colorControl.BlueF = 0.9411765F;
            colorControl.BoxSize = new System.Drawing.Size(20, 20);
            colorControl.Color = System.Drawing.Color.FromArgb(240, 240, 240);
            colorControl.Green = 240;
            colorControl.GreenF = 0.9411765F;
            colorControl.Name = "colorControl";
            colorControl.Red = 240;
            colorControl.RedF = 0.9411765F;
            toolTip.SetToolTip(colorControl, resources.GetString("colorControl.ToolTip1"));
            // 
            // panelFormula
            // 
            panelFormula.Controls.Add(textBoxFormula);
            panelFormula.Controls.Add(label90);
            panelFormula.Controls.Add(numericBoxZnumber);
            panelFormula.Controls.Add(colorControl);
            resources.ApplyResources(panelFormula, "panelFormula");
            panelFormula.Name = "panelFormula";
            // 
            // textBoxFormula
            // 
            resources.ApplyResources(textBoxFormula, "textBoxFormula");
            textBoxFormula.Name = "textBoxFormula";
            textBoxFormula.ReadOnly = true;
            textBoxFormula.TabStop = false;
            toolTip.SetToolTip(textBoxFormula, resources.GetString("textBoxFormula.ToolTip"));
            // 
            // label90
            // 
            resources.ApplyResources(label90, "label90");
            label90.Name = "label90";
            toolTip.SetToolTip(label90, resources.GetString("label90.ToolTip"));
            // 
            // numericBoxZnumber
            // 
            resources.ApplyResources(numericBoxZnumber, "numericBoxZnumber");
            numericBoxZnumber.BackColor = System.Drawing.Color.Transparent;
            numericBoxZnumber.Name = "numericBoxZnumber";
            numericBoxZnumber.ReadOnly = true;
            numericBoxZnumber.SkipEventDuringInput = false;
            numericBoxZnumber.SmartIncrement = true;
            numericBoxZnumber.TabStop = false;
            numericBoxZnumber.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBoxZnumber.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxZnumber, resources.GetString("numericBoxZnumber.ToolTip"));
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { scatteringFactorToolStripMenuItem, symmetryInformationToolStripMenuItem, toolStripSeparator2, importCrystalFromCIFAMCToolStripMenuItem, exportThisCrystalAsCIFToolStripMenuItem, sendThisCrystalToOtherSoftwareToolStripMenuItem, toolStripSeparator1, revertCellConstantsToolStripMenuItem, toolStripSeparator3, strainControlToolStripMenuItem, convertToP1SymmetryToolStripMenuItem, convertToSuperstructureToolStripMenuItem, convertToAnotherSpacegroupToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip1";
            resources.ApplyResources(contextMenuStrip, "contextMenuStrip");
            // 
            // scatteringFactorToolStripMenuItem
            // 
            scatteringFactorToolStripMenuItem.Name = "scatteringFactorToolStripMenuItem";
            resources.ApplyResources(scatteringFactorToolStripMenuItem, "scatteringFactorToolStripMenuItem");
            scatteringFactorToolStripMenuItem.Click += scatteringFactorToolStripMenuItem_Click;
            // 
            // symmetryInformationToolStripMenuItem
            // 
            symmetryInformationToolStripMenuItem.Name = "symmetryInformationToolStripMenuItem";
            resources.ApplyResources(symmetryInformationToolStripMenuItem, "symmetryInformationToolStripMenuItem");
            symmetryInformationToolStripMenuItem.Click += symmetryInformationToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // importCrystalFromCIFAMCToolStripMenuItem
            // 
            importCrystalFromCIFAMCToolStripMenuItem.Name = "importCrystalFromCIFAMCToolStripMenuItem";
            resources.ApplyResources(importCrystalFromCIFAMCToolStripMenuItem, "importCrystalFromCIFAMCToolStripMenuItem");
            importCrystalFromCIFAMCToolStripMenuItem.Click += importCrystalFromCIFAMCToolStripMenuItem_Click;
            // 
            // exportThisCrystalAsCIFToolStripMenuItem
            // 
            exportThisCrystalAsCIFToolStripMenuItem.Name = "exportThisCrystalAsCIFToolStripMenuItem";
            resources.ApplyResources(exportThisCrystalAsCIFToolStripMenuItem, "exportThisCrystalAsCIFToolStripMenuItem");
            exportThisCrystalAsCIFToolStripMenuItem.Click += exportThisCrystalAsCIFToolStripMenuItem_Click;
            // 
            // sendThisCrystalToOtherSoftwareToolStripMenuItem
            // 
            sendThisCrystalToOtherSoftwareToolStripMenuItem.Name = "sendThisCrystalToOtherSoftwareToolStripMenuItem";
            resources.ApplyResources(sendThisCrystalToOtherSoftwareToolStripMenuItem, "sendThisCrystalToOtherSoftwareToolStripMenuItem");
            sendThisCrystalToOtherSoftwareToolStripMenuItem.Click += sendThisCrystalToOtherSoftwareToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // revertCellConstantsToolStripMenuItem
            // 
            revertCellConstantsToolStripMenuItem.Name = "revertCellConstantsToolStripMenuItem";
            resources.ApplyResources(revertCellConstantsToolStripMenuItem, "revertCellConstantsToolStripMenuItem");
            revertCellConstantsToolStripMenuItem.Click += revertCellConstantsToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // strainControlToolStripMenuItem
            // 
            strainControlToolStripMenuItem.Name = "strainControlToolStripMenuItem";
            resources.ApplyResources(strainControlToolStripMenuItem, "strainControlToolStripMenuItem");
            strainControlToolStripMenuItem.Click += strainControlToolStripMenuItem_Click;
            // 
            // convertToP1SymmetryToolStripMenuItem
            // 
            convertToP1SymmetryToolStripMenuItem.Name = "convertToP1SymmetryToolStripMenuItem";
            resources.ApplyResources(convertToP1SymmetryToolStripMenuItem, "convertToP1SymmetryToolStripMenuItem");
            convertToP1SymmetryToolStripMenuItem.Click += convertToP1ToolStripMenuItem_Click;
            // 
            // convertToSuperstructureToolStripMenuItem
            // 
            convertToSuperstructureToolStripMenuItem.Name = "convertToSuperstructureToolStripMenuItem";
            resources.ApplyResources(convertToSuperstructureToolStripMenuItem, "convertToSuperstructureToolStripMenuItem");
            convertToSuperstructureToolStripMenuItem.Click += convertToSuperstructureToolStripMenuItem_Click;
            // 
            // convertToAnotherSpacegroupToolStripMenuItem
            // 
            convertToAnotherSpacegroupToolStripMenuItem.Name = "convertToAnotherSpacegroupToolStripMenuItem";
            resources.ApplyResources(convertToAnotherSpacegroupToolStripMenuItem, "convertToAnotherSpacegroupToolStripMenuItem");
            convertToAnotherSpacegroupToolStripMenuItem.Click += convertToAnotherSpacegroupToolStripMenuItem_Click;
            // 
            // textBoxName
            // 
            resources.ApplyResources(textBoxName, "textBoxName");
            textBoxName.Name = "textBoxName";
            toolTip.SetToolTip(textBoxName, resources.GetString("textBoxName.ToolTip"));
            // 
            // label22
            // 
            resources.ApplyResources(label22, "label22");
            label22.Name = "label22";
            // 
            // buttonScatteringFactor
            // 
            resources.ApplyResources(buttonScatteringFactor, "buttonScatteringFactor");
            buttonScatteringFactor.BackColor = System.Drawing.Color.SteelBlue;
            buttonScatteringFactor.ForeColor = System.Drawing.Color.White;
            buttonScatteringFactor.Name = "buttonScatteringFactor";
            toolTip.SetToolTip(buttonScatteringFactor, resources.GetString("buttonScatteringFactor.ToolTip"));
            buttonScatteringFactor.UseVisualStyleBackColor = false;
            buttonScatteringFactor.Click += buttonScatteringFactor_Click;
            // 
            // buttonSymmetryInfo
            // 
            resources.ApplyResources(buttonSymmetryInfo, "buttonSymmetryInfo");
            buttonSymmetryInfo.BackColor = System.Drawing.Color.SteelBlue;
            buttonSymmetryInfo.ForeColor = System.Drawing.Color.White;
            buttonSymmetryInfo.Name = "buttonSymmetryInfo";
            toolTip.SetToolTip(buttonSymmetryInfo, resources.GetString("buttonSymmetryInfo.ToolTip"));
            buttonSymmetryInfo.UseVisualStyleBackColor = false;
            buttonSymmetryInfo.Click += buttonSymmetryInfo_Click;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // panelNameAndActions
            // 
            panelNameAndActions.Controls.Add(textBoxName);
            panelNameAndActions.Controls.Add(buttonSymmetryInfo);
            panelNameAndActions.Controls.Add(buttonScatteringFactor);
            panelNameAndActions.Controls.Add(label22);
            resources.ApplyResources(panelNameAndActions, "panelNameAndActions");
            panelNameAndActions.Name = "panelNameAndActions";
            // 
            // CrystalControl
            // 
            AllowDrop = true;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            ContextMenuStrip = contextMenuStrip;
            Controls.Add(tabControl);
            Controls.Add(panelFormula);
            Controls.Add(panelNameAndActions);
            Controls.Add(flowLayoutPanel1);
            DoubleBuffered = true;
            Name = "CrystalControl";
            Load += CrystalForm_Load;
            DragDrop += FormCrystal_DragDrop;
            DragEnter += FormCrystal_DragEnter;
            KeyDown += CrystalControl_KeyDown;
            Resize += CrystalControl_Resize_1;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            groupBoxAuthors.ResumeLayout(false);
            groupBoxAuthors.PerformLayout();
            groupBoxTitle.ResumeLayout(false);
            groupBoxTitle.PerformLayout();
            tabControl.ResumeLayout(false);
            tabPageBasicInfo.ResumeLayout(false);
            panelSymmetryInfo.ResumeLayout(false);
            panelSymmetryInfo.PerformLayout();
            flowLayoutPanelCellProperties.ResumeLayout(false);
            flowLayoutPanelSymmetryActions.ResumeLayout(false);
            flowLayoutPanelSymmetryActions.PerformLayout();
            tabPageAtom.ResumeLayout(false);
            tabPageAtom.PerformLayout();
            panelAtom.ResumeLayout(false);
            panelAtom.PerformLayout();
            contextMenuStrip2.ResumeLayout(false);
            tabPageBondsPolyhedra.ResumeLayout(false);
            tabPageBondsPolyhedra.PerformLayout();
            tabPageReference.ResumeLayout(false);
            groupBoxJournal.ResumeLayout(false);
            groupBoxJournal.PerformLayout();
            groupBoxNote.ResumeLayout(false);
            groupBoxNote.PerformLayout();
            tabPageEOS.ResumeLayout(false);
            tabPageElasticity.ResumeLayout(false);
            tabPageElasticity.PerformLayout();
            tabPageStrainStress.ResumeLayout(false);
            tabPageStrainStress.PerformLayout();
            tabPagePolycrystalline.ResumeLayout(false);
            tabPagePolycrystalline.PerformLayout();
            contextMenuStripPoleFigure.ResumeLayout(false);
            flowLayoutPanelRandomOrientationActions.ResumeLayout(false);
            flowLayoutPanelRandomOrientationActions.PerformLayout();
            flowLayoutPanelRandomOrientationSettings.ResumeLayout(false);
            flowLayoutPanelRandomOrientationSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAngleResolution).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAngleSubDivision).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCrystallineSize).EndInit();
            tabPageBounds.ResumeLayout(false);
            tabPageLatticePlane.ResumeLayout(false);
            panelFormula.ResumeLayout(false);
            panelFormula.PerformLayout();
            contextMenuStrip.ResumeLayout(false);
            panelNameAndActions.ResumeLayout(false);
            panelNameAndActions.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private NumericBox numericBoxVolumeAng;
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
        private System.Windows.Forms.Panel panelNameAndActions;
        private System.Windows.Forms.GroupBox groupBoxTitle;
        private System.Windows.Forms.GroupBox groupBoxAuthors;
        private System.Windows.Forms.GroupBox groupBoxJournal;
        private System.Windows.Forms.GroupBox groupBoxNote;
        private System.Windows.Forms.NumericUpDown numericUpDownAngleSubDivision;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Panel panelSymmetryInfo;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRandomOrientationActions;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRandomOrientationSettings;
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCellProperties;
        private NumericBox numericBoxZnumber;
        private NumericBox numericBoxDensity;
        public SymmetryControl symmetryControl;
        private System.Windows.Forms.Panel panelFormula;
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSymmetryActions;
        private System.Windows.Forms.Button buttonConvertToSubgroup;
        private System.Windows.Forms.Button buttonChangeAxesOriginSetting;
        private NumericBox numericBoxCellVolumeNm;
    }
}