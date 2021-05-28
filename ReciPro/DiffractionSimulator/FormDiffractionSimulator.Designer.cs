namespace ReciPro
{
    partial class FormDiffractionSimulator
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
            //if (context != null)
           //     context.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiffractionSimulator));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonDiffractionSpots = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonKikuchiLines = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDebyeRing = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonScale = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonIndexLabels = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDspacing = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDistance = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonExcitationError = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFg = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelTimeForSearchingG = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTimeForDrawing = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTimeForBethe = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageWave = new System.Windows.Forms.TabPage();
            this.waveLengthControl = new Crystallography.Controls.WaveLengthControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.trackBarStrSize = new System.Windows.Forms.TrackBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.colorControlString = new Crystallography.Controls.ColorControl();
            this.label14 = new System.Windows.Forms.Label();
            this.colorControlFoot = new Crystallography.Controls.ColorControl();
            this.colorControlBackGround = new Crystallography.Controls.ColorControl();
            this.tabPageKikuchi = new System.Windows.Forms.TabPage();
            this.numericBoxKikuchiLineThreshold = new Crystallography.Controls.NumericBox();
            this.colorControlDefectLine = new Crystallography.Controls.ColorControl();
            this.colorControlExcessLine = new Crystallography.Controls.ColorControl();
            this.trackBarLineWidth = new System.Windows.Forms.TrackBar();
            this.label11 = new System.Windows.Forms.Label();
            this.tabPageDebye = new System.Windows.Forms.TabPage();
            this.colorControlDebyeRing = new Crystallography.Controls.ColorControl();
            this.checkBoxDebyeRingLabel = new System.Windows.Forms.CheckBox();
            this.checkBoxDebyeRingIgnoreIntensity = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.trackBarDebyeRingWidth = new System.Windows.Forms.TrackBar();
            this.tabPageScale = new System.Windows.Forms.TabPage();
            this.checkBoxScaleLabel = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.trackBarScaleLineWidth = new System.Windows.Forms.TrackBar();
            this.label16 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonScaleDivisionFine = new System.Windows.Forms.RadioButton();
            this.radioButtonScaleDivisionMedium = new System.Windows.Forms.RadioButton();
            this.radioButtonScaleDivisionCoarse = new System.Windows.Forms.RadioButton();
            this.colorControlScaleAzimuth = new Crystallography.Controls.ColorControl();
            this.colorControlScale2Theta = new Crystallography.Controls.ColorControl();
            this.tabPageMisc = new System.Windows.Forms.TabPage();
            this.numericBoxDev = new Crystallography.Controls.NumericBox();
            this.numericBoxAcc = new Crystallography.Controls.NumericBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.trackBarRotationSpeed = new System.Windows.Forms.TrackBar();
            this.graphicsBox = new ImagingSolution.Control.GraphicsBox(this.components);
            this.labelDummy = new System.Windows.Forms.Label();
            this.panelMousePosition = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.labelMousePositionDetector = new System.Windows.Forms.Label();
            this.labelMousePositionReal = new System.Windows.Forms.Label();
            this.labelDinv = new System.Windows.Forms.Label();
            this.checkBoxMousePositionDetailes = new System.Windows.Forms.CheckBox();
            this.labelMousePositionReciprocal = new System.Windows.Forms.Label();
            this.labelTwoTheta = new System.Windows.Forms.Label();
            this.labelD = new System.Windows.Forms.Label();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numericBoxClientHeight = new Crystallography.Controls.NumericBox();
            this.numericBoxClientWidth = new Crystallography.Controls.NumericBox();
            this.numericBoxResolution = new Crystallography.Controls.NumericBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownCamaraLength2 = new System.Windows.Forms.NumericUpDown();
            this.buttonDetailedGeometry = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonResetCenter = new System.Windows.Forms.Button();
            this.radioButtonCenterToDetector = new System.Windows.Forms.RadioButton();
            this.radioButtonCenterToFoot = new System.Windows.Forms.RadioButton();
            this.checkBoxFixCenter = new System.Windows.Forms.CheckBox();
            this.radioButtonCenterToDirect = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDetectorAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDetectorAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDetectorAsMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCBEDPatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCBEDasPngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCBEDasTiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asPixelByPixelImagePNGFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyImageToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDetectorAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDetectorAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDetectorAsMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCBEDPatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyCBEDasImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemBackLaue = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.dynamicCompressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.presetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.electron300KVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.electron200KVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.electron120KeVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.xray30KeVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xray20KeVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xrayMoKαToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xrayCuKαToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.basicConceptOfBethesMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radioButtonIntensityBethe = new System.Windows.Forms.RadioButton();
            this.checkBoxUseCrystalColor = new System.Windows.Forms.CheckBox();
            this.checkBoxExtinctionAll = new System.Windows.Forms.CheckBox();
            this.checkBoxExtinctionLattice = new System.Windows.Forms.CheckBox();
            this.groupBoxSpotProperty = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelPED = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.numericBoxPED_Semiangle = new Crystallography.Controls.NumericBox();
            this.numericBoxPED_Step = new Crystallography.Controls.NumericBox();
            this.flowLayoutPanelBethe = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.numericBoxNumOfBlochWave = new Crystallography.Controls.NumericBox();
            this.numericBoxThickness = new Crystallography.Controls.NumericBox();
            this.flowLayoutPanelAppearance = new System.Windows.Forms.FlowLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonCircleArea = new System.Windows.Forms.RadioButton();
            this.radioButtonPointSpread = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.trackBarSpotOpacity = new System.Windows.Forms.TrackBar();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.numericBoxSpotRadius = new Crystallography.Controls.NumericBox();
            this.flowLayoutPanelGaussianOption = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.trackBarIntensityForPointSpread = new System.Windows.Forms.TrackBar();
            this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.label25 = new System.Windows.Forms.Label();
            this.comboBoxScaleColorScale = new System.Windows.Forms.ComboBox();
            this.checkBoxLogScale = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelColorScale = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelSpotColor = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.colorControlOrigin = new Crystallography.Controls.ColorControl();
            this.colorControlNoCondition = new Crystallography.Controls.ColorControl();
            this.colorControlScrewGlide = new Crystallography.Controls.ColorControl();
            this.colorControlForbiddenLattice = new Crystallography.Controls.ColorControl();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButtonIntensityExcitation = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanelExtinctionOption = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonIntensityKinematical = new System.Windows.Forms.RadioButton();
            this.buttonDetailsOfSpots = new System.Windows.Forms.Button();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonBeamParallel = new System.Windows.Forms.RadioButton();
            this.radioButtonBeamPrecession = new System.Windows.Forms.RadioButton();
            this.radioButtonBeamConvergence = new System.Windows.Forms.RadioButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.timerBlinkSpot = new System.Windows.Forms.Timer(this.components);
            this.timerBlinkKikuchiLine = new System.Windows.Forms.Timer(this.components);
            this.timerBlinkDebyeRing = new System.Windows.Forms.Timer(this.components);
            this.timerBlinkScale = new System.Windows.Forms.Timer(this.components);
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageWave.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStrSize)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabPageKikuchi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLineWidth)).BeginInit();
            this.tabPageDebye.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDebyeRingWidth)).BeginInit();
            this.tabPageScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScaleLineWidth)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPageMisc.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotationSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphicsBox)).BeginInit();
            this.panelMousePosition.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCamaraLength2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBoxSpotProperty.SuspendLayout();
            this.flowLayoutPanelPED.SuspendLayout();
            this.flowLayoutPanelBethe.SuspendLayout();
            this.flowLayoutPanelAppearance.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpotOpacity)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanelGaussianOption.SuspendLayout();
            this.flowLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIntensityForPointSpread)).BeginInit();
            this.flowLayoutPanel9.SuspendLayout();
            this.flowLayoutPanelSpotColor.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanelExtinctionOption.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.flowLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            resources.ApplyResources(this.toolStripContainer1, "toolStripContainer1");
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.BottomToolStripPanel, "toolStripContainer1.BottomToolStripPanel");
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip3);
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.toolStrip1);
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            this.toolTip.SetToolTip(this.toolStripContainer1.BottomToolStripPanel, resources.GetString("toolStripContainer1.BottomToolStripPanel.ToolTip"));
            // 
            // toolStripContainer1.ContentPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelMain);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.panelMousePosition);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.flowLayoutPanel6);
            this.toolTip.SetToolTip(this.toolStripContainer1.ContentPanel, resources.GetString("toolStripContainer1.ContentPanel.ToolTip"));
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.LeftToolStripPanel, "toolStripContainer1.LeftToolStripPanel");
            this.toolTip.SetToolTip(this.toolStripContainer1.LeftToolStripPanel, resources.GetString("toolStripContainer1.LeftToolStripPanel.ToolTip"));
            this.toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.RightToolStripPanel, "toolStripContainer1.RightToolStripPanel");
            this.toolTip.SetToolTip(this.toolStripContainer1.RightToolStripPanel, resources.GetString("toolStripContainer1.RightToolStripPanel.ToolTip"));
            this.toolTip.SetToolTip(this.toolStripContainer1, resources.GetString("toolStripContainer1.ToolTip"));
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            resources.ApplyResources(this.toolStripContainer1.TopToolStripPanel, "toolStripContainer1.TopToolStripPanel");
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolTip.SetToolTip(this.toolStripContainer1.TopToolStripPanel, resources.GetString("toolStripContainer1.TopToolStripPanel.ToolTip"));
            // 
            // toolStrip3
            // 
            resources.ApplyResources(this.toolStrip3, "toolStrip3");
            this.toolStrip3.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonDiffractionSpots,
            this.toolStripSeparator2,
            this.toolStripButtonKikuchiLines,
            this.toolStripSeparator3,
            this.toolStripButtonDebyeRing,
            this.toolStripSeparator6,
            this.toolStripButtonScale});
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolTip.SetToolTip(this.toolStrip3, resources.GetString("toolStrip3.ToolTip"));
            // 
            // toolStripButtonDiffractionSpots
            // 
            resources.ApplyResources(this.toolStripButtonDiffractionSpots, "toolStripButtonDiffractionSpots");
            this.toolStripButtonDiffractionSpots.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButtonDiffractionSpots.Checked = true;
            this.toolStripButtonDiffractionSpots.CheckOnClick = true;
            this.toolStripButtonDiffractionSpots.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonDiffractionSpots.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDiffractionSpots.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.toolStripButtonDiffractionSpots.Name = "toolStripButtonDiffractionSpots";
            this.toolStripButtonDiffractionSpots.CheckedChanged += new System.EventHandler(this.toolStripButtonDiffractionSpots_CheckedChanged);
            this.toolStripButtonDiffractionSpots.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripButtonDiffractionSpots_MouseDown);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // toolStripButtonKikuchiLines
            // 
            resources.ApplyResources(this.toolStripButtonKikuchiLines, "toolStripButtonKikuchiLines");
            this.toolStripButtonKikuchiLines.CheckOnClick = true;
            this.toolStripButtonKikuchiLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonKikuchiLines.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.toolStripButtonKikuchiLines.Name = "toolStripButtonKikuchiLines";
            this.toolStripButtonKikuchiLines.CheckedChanged += new System.EventHandler(this.toolStripButtonDiffractionSpots_CheckedChanged);
            this.toolStripButtonKikuchiLines.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripButtonDiffractionSpots_MouseDown);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // toolStripButtonDebyeRing
            // 
            resources.ApplyResources(this.toolStripButtonDebyeRing, "toolStripButtonDebyeRing");
            this.toolStripButtonDebyeRing.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButtonDebyeRing.CheckOnClick = true;
            this.toolStripButtonDebyeRing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDebyeRing.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.toolStripButtonDebyeRing.Name = "toolStripButtonDebyeRing";
            this.toolStripButtonDebyeRing.CheckedChanged += new System.EventHandler(this.toolStripButtonDiffractionSpots_CheckedChanged);
            this.toolStripButtonDebyeRing.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripButtonDiffractionSpots_MouseDown);
            // 
            // toolStripSeparator6
            // 
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // toolStripButtonScale
            // 
            resources.ApplyResources(this.toolStripButtonScale, "toolStripButtonScale");
            this.toolStripButtonScale.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButtonScale.CheckOnClick = true;
            this.toolStripButtonScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonScale.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.toolStripButtonScale.Name = "toolStripButtonScale";
            this.toolStripButtonScale.CheckedChanged += new System.EventHandler(this.toolStripButtonDiffractionSpots_CheckedChanged);
            this.toolStripButtonScale.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripButtonDiffractionSpots_MouseDown);
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonIndexLabels,
            this.toolStripButtonDspacing,
            this.toolStripButtonDistance,
            this.toolStripButtonExcitationError,
            this.toolStripButtonFg});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolTip.SetToolTip(this.toolStrip1, resources.GetString("toolStrip1.ToolTip"));
            // 
            // toolStripButtonIndexLabels
            // 
            resources.ApplyResources(this.toolStripButtonIndexLabels, "toolStripButtonIndexLabels");
            this.toolStripButtonIndexLabels.Checked = true;
            this.toolStripButtonIndexLabels.CheckOnClick = true;
            this.toolStripButtonIndexLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonIndexLabels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonIndexLabels.ForeColor = System.Drawing.Color.Salmon;
            this.toolStripButtonIndexLabels.Name = "toolStripButtonIndexLabels";
            this.toolStripButtonIndexLabels.CheckedChanged += new System.EventHandler(this.toolStripButtonDiffractionSpots_CheckedChanged);
            // 
            // toolStripButtonDspacing
            // 
            resources.ApplyResources(this.toolStripButtonDspacing, "toolStripButtonDspacing");
            this.toolStripButtonDspacing.CheckOnClick = true;
            this.toolStripButtonDspacing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDspacing.ForeColor = System.Drawing.Color.Salmon;
            this.toolStripButtonDspacing.Name = "toolStripButtonDspacing";
            this.toolStripButtonDspacing.CheckedChanged += new System.EventHandler(this.toolStripButtonDiffractionSpots_CheckedChanged);
            // 
            // toolStripButtonDistance
            // 
            resources.ApplyResources(this.toolStripButtonDistance, "toolStripButtonDistance");
            this.toolStripButtonDistance.CheckOnClick = true;
            this.toolStripButtonDistance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDistance.ForeColor = System.Drawing.Color.Salmon;
            this.toolStripButtonDistance.Name = "toolStripButtonDistance";
            this.toolStripButtonDistance.CheckedChanged += new System.EventHandler(this.toolStripButtonDiffractionSpots_CheckedChanged);
            // 
            // toolStripButtonExcitationError
            // 
            resources.ApplyResources(this.toolStripButtonExcitationError, "toolStripButtonExcitationError");
            this.toolStripButtonExcitationError.CheckOnClick = true;
            this.toolStripButtonExcitationError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonExcitationError.ForeColor = System.Drawing.Color.Salmon;
            this.toolStripButtonExcitationError.Name = "toolStripButtonExcitationError";
            this.toolStripButtonExcitationError.CheckedChanged += new System.EventHandler(this.toolStripButtonDiffractionSpots_CheckedChanged);
            // 
            // toolStripButtonFg
            // 
            resources.ApplyResources(this.toolStripButtonFg, "toolStripButtonFg");
            this.toolStripButtonFg.CheckOnClick = true;
            this.toolStripButtonFg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonFg.ForeColor = System.Drawing.Color.Salmon;
            this.toolStripButtonFg.Name = "toolStripButtonFg";
            this.toolStripButtonFg.CheckedChanged += new System.EventHandler(this.toolStripButtonDiffractionSpots_CheckedChanged);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelTimeForSearchingG,
            this.toolStripStatusLabelTimeForDrawing,
            this.toolStripStatusLabelTimeForBethe,
            this.toolStripStatusLabel3});
            this.statusStrip1.Name = "statusStrip1";
            this.toolTip.SetToolTip(this.statusStrip1, resources.GetString("statusStrip1.ToolTip"));
            this.statusStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.statusStrip1_MouseDown);
            // 
            // toolStripStatusLabelTimeForSearchingG
            // 
            resources.ApplyResources(this.toolStripStatusLabelTimeForSearchingG, "toolStripStatusLabelTimeForSearchingG");
            this.toolStripStatusLabelTimeForSearchingG.Name = "toolStripStatusLabelTimeForSearchingG";
            // 
            // toolStripStatusLabelTimeForDrawing
            // 
            resources.ApplyResources(this.toolStripStatusLabelTimeForDrawing, "toolStripStatusLabelTimeForDrawing");
            this.toolStripStatusLabelTimeForDrawing.Name = "toolStripStatusLabelTimeForDrawing";
            // 
            // toolStripStatusLabelTimeForBethe
            // 
            resources.ApplyResources(this.toolStripStatusLabelTimeForBethe, "toolStripStatusLabelTimeForBethe");
            this.toolStripStatusLabelTimeForBethe.Name = "toolStripStatusLabelTimeForBethe";
            // 
            // toolStripStatusLabel3
            // 
            resources.ApplyResources(this.toolStripStatusLabel3, "toolStripStatusLabel3");
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            // 
            // panelMain
            // 
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Controls.Add(this.tabControl);
            this.panelMain.Controls.Add(this.graphicsBox);
            this.panelMain.Controls.Add(this.labelDummy);
            this.panelMain.Name = "panelMain";
            this.toolTip.SetToolTip(this.panelMain, resources.GetString("panelMain.ToolTip"));
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPageWave);
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Controls.Add(this.tabPageKikuchi);
            this.tabControl.Controls.Add(this.tabPageDebye);
            this.tabControl.Controls.Add(this.tabPageScale);
            this.tabControl.Controls.Add(this.tabPageMisc);
            this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl.HotTrack = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.toolTip.SetToolTip(this.tabControl, resources.GetString("tabControl.ToolTip"));
            this.tabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl_DrawItem);
            this.tabControl.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl_Selecting);
            this.tabControl.Click += new System.EventHandler(this.tabControl_Click);
            // 
            // tabPageWave
            // 
            resources.ApplyResources(this.tabPageWave, "tabPageWave");
            this.tabPageWave.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageWave.Controls.Add(this.waveLengthControl);
            this.tabPageWave.Name = "tabPageWave";
            this.toolTip.SetToolTip(this.tabPageWave, resources.GetString("tabPageWave.ToolTip"));
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(this.waveLengthControl, "waveLengthControl");
            this.waveLengthControl.Energy = 199.99999999999997D;
            this.waveLengthControl.Name = "waveLengthControl";
            this.waveLengthControl.ShowWaveSource = true;
            this.waveLengthControl.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolTip.SetToolTip(this.waveLengthControl, resources.GetString("waveLengthControl.ToolTip"));
            this.waveLengthControl.WaveLength = 0.00250793474552456D;
            this.waveLengthControl.WaveSource = Crystallography.WaveSource.Electron;
            this.waveLengthControl.XrayWaveSourceElementNumber = 0;
            this.waveLengthControl.XrayWaveSourceLine = Crystallography.XrayLine.Ka1;
            this.waveLengthControl.WavelengthChanged += new System.EventHandler(this.waveLengthControl_WavelengthChanged);
            this.waveLengthControl.WaveSourceChanged += new System.EventHandler(this.WaveLengthControl_WaveSourceChanged);
            // 
            // tabPageGeneral
            // 
            resources.ApplyResources(this.tabPageGeneral, "tabPageGeneral");
            this.tabPageGeneral.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageGeneral.Controls.Add(this.groupBox4);
            this.tabPageGeneral.Controls.Add(this.groupBox3);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.toolTip.SetToolTip(this.tabPageGeneral, resources.GetString("tabPageGeneral.ToolTip"));
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.trackBarStrSize);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox4, resources.GetString("groupBox4.ToolTip"));
            // 
            // trackBarStrSize
            // 
            resources.ApplyResources(this.trackBarStrSize, "trackBarStrSize");
            this.trackBarStrSize.LargeChange = 50;
            this.trackBarStrSize.Maximum = 200;
            this.trackBarStrSize.Minimum = 1;
            this.trackBarStrSize.Name = "trackBarStrSize";
            this.trackBarStrSize.SmallChange = 10;
            this.trackBarStrSize.TickFrequency = 500;
            this.trackBarStrSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip.SetToolTip(this.trackBarStrSize, resources.GetString("trackBarStrSize.ToolTip"));
            this.trackBarStrSize.Value = 80;
            this.trackBarStrSize.ValueChanged += new System.EventHandler(this.Draw);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.colorControlString);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.colorControlFoot);
            this.groupBox3.Controls.Add(this.colorControlBackGround);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox3, resources.GetString("groupBox3.ToolTip"));
            // 
            // colorControlString
            // 
            resources.ApplyResources(this.colorControlString, "colorControlString");
            this.colorControlString.Argb = -1;
            this.colorControlString.Blue = 255;
            this.colorControlString.BlueF = 1F;
            this.colorControlString.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlString.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.colorControlString.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlString.Green = 255;
            this.colorControlString.GreenF = 1F;
            this.colorControlString.Name = "colorControlString";
            this.colorControlString.Red = 255;
            this.colorControlString.RedF = 1F;
            this.toolTip.SetToolTip(this.colorControlString, resources.GetString("colorControlString.ToolTip"));
            this.colorControlString.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            this.toolTip.SetToolTip(this.label14, resources.GetString("label14.ToolTip"));
            // 
            // colorControlFoot
            // 
            resources.ApplyResources(this.colorControlFoot, "colorControlFoot");
            this.colorControlFoot.Argb = -16728064;
            this.colorControlFoot.Blue = 0;
            this.colorControlFoot.BlueF = 0F;
            this.colorControlFoot.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlFoot.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.colorControlFoot.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlFoot.Green = 192;
            this.colorControlFoot.GreenF = 0.7529412F;
            this.colorControlFoot.Name = "colorControlFoot";
            this.colorControlFoot.Red = 0;
            this.colorControlFoot.RedF = 0F;
            this.toolTip.SetToolTip(this.colorControlFoot, resources.GetString("colorControlFoot.ToolTip"));
            this.colorControlFoot.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // colorControlBackGround
            // 
            resources.ApplyResources(this.colorControlBackGround, "colorControlBackGround");
            this.colorControlBackGround.Argb = -14671840;
            this.colorControlBackGround.Blue = 32;
            this.colorControlBackGround.BlueF = 0.1254902F;
            this.colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlBackGround.Color = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.colorControlBackGround.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlBackGround.Green = 32;
            this.colorControlBackGround.GreenF = 0.1254902F;
            this.colorControlBackGround.Name = "colorControlBackGround";
            this.colorControlBackGround.Red = 32;
            this.colorControlBackGround.RedF = 0.1254902F;
            this.toolTip.SetToolTip(this.colorControlBackGround, resources.GetString("colorControlBackGround.ToolTip"));
            this.colorControlBackGround.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // tabPageKikuchi
            // 
            resources.ApplyResources(this.tabPageKikuchi, "tabPageKikuchi");
            this.tabPageKikuchi.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageKikuchi.Controls.Add(this.numericBoxKikuchiLineThreshold);
            this.tabPageKikuchi.Controls.Add(this.colorControlDefectLine);
            this.tabPageKikuchi.Controls.Add(this.colorControlExcessLine);
            this.tabPageKikuchi.Controls.Add(this.trackBarLineWidth);
            this.tabPageKikuchi.Controls.Add(this.label11);
            this.tabPageKikuchi.Name = "tabPageKikuchi";
            this.toolTip.SetToolTip(this.tabPageKikuchi, resources.GetString("tabPageKikuchi.ToolTip"));
            // 
            // numericBoxKikuchiLineThreshold
            // 
            resources.ApplyResources(this.numericBoxKikuchiLineThreshold, "numericBoxKikuchiLineThreshold");
            this.numericBoxKikuchiLineThreshold.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxKikuchiLineThreshold.Maximum = 10D;
            this.numericBoxKikuchiLineThreshold.Minimum = 0D;
            this.numericBoxKikuchiLineThreshold.Name = "numericBoxKikuchiLineThreshold";
            this.numericBoxKikuchiLineThreshold.RadianValue = 0.0069813170079773184D;
            this.numericBoxKikuchiLineThreshold.ShowUpDown = true;
            this.numericBoxKikuchiLineThreshold.SmartIncrement = true;
            this.numericBoxKikuchiLineThreshold.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolTip.SetToolTip(this.numericBoxKikuchiLineThreshold, resources.GetString("numericBoxKikuchiLineThreshold.ToolTip"));
            this.numericBoxKikuchiLineThreshold.Value = 0.4D;
            this.numericBoxKikuchiLineThreshold.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxKikuchiLineThreshold_ValueChanged);
            // 
            // colorControlDefectLine
            // 
            resources.ApplyResources(this.colorControlDefectLine, "colorControlDefectLine");
            this.colorControlDefectLine.Argb = -16777216;
            this.colorControlDefectLine.Blue = 0;
            this.colorControlDefectLine.BlueF = 0F;
            this.colorControlDefectLine.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlDefectLine.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorControlDefectLine.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlDefectLine.Green = 0;
            this.colorControlDefectLine.GreenF = 0F;
            this.colorControlDefectLine.Name = "colorControlDefectLine";
            this.colorControlDefectLine.Red = 0;
            this.colorControlDefectLine.RedF = 0F;
            this.toolTip.SetToolTip(this.colorControlDefectLine, resources.GetString("colorControlDefectLine.ToolTip"));
            this.colorControlDefectLine.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // colorControlExcessLine
            // 
            resources.ApplyResources(this.colorControlExcessLine, "colorControlExcessLine");
            this.colorControlExcessLine.Argb = -2039584;
            this.colorControlExcessLine.Blue = 224;
            this.colorControlExcessLine.BlueF = 0.8784314F;
            this.colorControlExcessLine.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlExcessLine.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.colorControlExcessLine.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlExcessLine.Green = 224;
            this.colorControlExcessLine.GreenF = 0.8784314F;
            this.colorControlExcessLine.Name = "colorControlExcessLine";
            this.colorControlExcessLine.Red = 224;
            this.colorControlExcessLine.RedF = 0.8784314F;
            this.toolTip.SetToolTip(this.colorControlExcessLine, resources.GetString("colorControlExcessLine.ToolTip"));
            this.colorControlExcessLine.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // trackBarLineWidth
            // 
            resources.ApplyResources(this.trackBarLineWidth, "trackBarLineWidth");
            this.trackBarLineWidth.Maximum = 10000;
            this.trackBarLineWidth.Minimum = 1;
            this.trackBarLineWidth.Name = "trackBarLineWidth";
            this.trackBarLineWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip.SetToolTip(this.trackBarLineWidth, resources.GetString("trackBarLineWidth.ToolTip"));
            this.trackBarLineWidth.Value = 2000;
            this.trackBarLineWidth.ValueChanged += new System.EventHandler(this.numericUpDownResolution_ValueChanged);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            this.toolTip.SetToolTip(this.label11, resources.GetString("label11.ToolTip"));
            // 
            // tabPageDebye
            // 
            resources.ApplyResources(this.tabPageDebye, "tabPageDebye");
            this.tabPageDebye.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageDebye.Controls.Add(this.colorControlDebyeRing);
            this.tabPageDebye.Controls.Add(this.checkBoxDebyeRingLabel);
            this.tabPageDebye.Controls.Add(this.checkBoxDebyeRingIgnoreIntensity);
            this.tabPageDebye.Controls.Add(this.label6);
            this.tabPageDebye.Controls.Add(this.trackBarDebyeRingWidth);
            this.tabPageDebye.Name = "tabPageDebye";
            this.toolTip.SetToolTip(this.tabPageDebye, resources.GetString("tabPageDebye.ToolTip"));
            // 
            // colorControlDebyeRing
            // 
            resources.ApplyResources(this.colorControlDebyeRing, "colorControlDebyeRing");
            this.colorControlDebyeRing.Argb = -256;
            this.colorControlDebyeRing.Blue = 0;
            this.colorControlDebyeRing.BlueF = 0F;
            this.colorControlDebyeRing.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlDebyeRing.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.colorControlDebyeRing.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlDebyeRing.Green = 255;
            this.colorControlDebyeRing.GreenF = 1F;
            this.colorControlDebyeRing.Name = "colorControlDebyeRing";
            this.colorControlDebyeRing.Red = 255;
            this.colorControlDebyeRing.RedF = 1F;
            this.toolTip.SetToolTip(this.colorControlDebyeRing, resources.GetString("colorControlDebyeRing.ToolTip"));
            this.colorControlDebyeRing.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // checkBoxDebyeRingLabel
            // 
            resources.ApplyResources(this.checkBoxDebyeRingLabel, "checkBoxDebyeRingLabel");
            this.checkBoxDebyeRingLabel.Name = "checkBoxDebyeRingLabel";
            this.toolTip.SetToolTip(this.checkBoxDebyeRingLabel, resources.GetString("checkBoxDebyeRingLabel.ToolTip"));
            this.checkBoxDebyeRingLabel.UseVisualStyleBackColor = true;
            this.checkBoxDebyeRingLabel.CheckedChanged += new System.EventHandler(this.Draw);
            // 
            // checkBoxDebyeRingIgnoreIntensity
            // 
            resources.ApplyResources(this.checkBoxDebyeRingIgnoreIntensity, "checkBoxDebyeRingIgnoreIntensity");
            this.checkBoxDebyeRingIgnoreIntensity.Name = "checkBoxDebyeRingIgnoreIntensity";
            this.toolTip.SetToolTip(this.checkBoxDebyeRingIgnoreIntensity, resources.GetString("checkBoxDebyeRingIgnoreIntensity.ToolTip"));
            this.checkBoxDebyeRingIgnoreIntensity.UseVisualStyleBackColor = true;
            this.checkBoxDebyeRingIgnoreIntensity.CheckedChanged += new System.EventHandler(this.Draw);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // trackBarDebyeRingWidth
            // 
            resources.ApplyResources(this.trackBarDebyeRingWidth, "trackBarDebyeRingWidth");
            this.trackBarDebyeRingWidth.LargeChange = 1;
            this.trackBarDebyeRingWidth.Minimum = 1;
            this.trackBarDebyeRingWidth.Name = "trackBarDebyeRingWidth";
            this.trackBarDebyeRingWidth.TickFrequency = 500;
            this.trackBarDebyeRingWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip.SetToolTip(this.trackBarDebyeRingWidth, resources.GetString("trackBarDebyeRingWidth.ToolTip"));
            this.trackBarDebyeRingWidth.Value = 5;
            this.trackBarDebyeRingWidth.ValueChanged += new System.EventHandler(this.Draw);
            // 
            // tabPageScale
            // 
            resources.ApplyResources(this.tabPageScale, "tabPageScale");
            this.tabPageScale.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageScale.Controls.Add(this.checkBoxScaleLabel);
            this.tabPageScale.Controls.Add(this.label12);
            this.tabPageScale.Controls.Add(this.trackBarScaleLineWidth);
            this.tabPageScale.Controls.Add(this.label16);
            this.tabPageScale.Controls.Add(this.flowLayoutPanel1);
            this.tabPageScale.Controls.Add(this.colorControlScaleAzimuth);
            this.tabPageScale.Controls.Add(this.colorControlScale2Theta);
            this.tabPageScale.Name = "tabPageScale";
            this.toolTip.SetToolTip(this.tabPageScale, resources.GetString("tabPageScale.ToolTip"));
            // 
            // checkBoxScaleLabel
            // 
            resources.ApplyResources(this.checkBoxScaleLabel, "checkBoxScaleLabel");
            this.checkBoxScaleLabel.Checked = true;
            this.checkBoxScaleLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxScaleLabel.Name = "checkBoxScaleLabel";
            this.toolTip.SetToolTip(this.checkBoxScaleLabel, resources.GetString("checkBoxScaleLabel.ToolTip"));
            this.checkBoxScaleLabel.UseVisualStyleBackColor = true;
            this.checkBoxScaleLabel.CheckedChanged += new System.EventHandler(this.Draw);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            this.toolTip.SetToolTip(this.label12, resources.GetString("label12.ToolTip"));
            // 
            // trackBarScaleLineWidth
            // 
            resources.ApplyResources(this.trackBarScaleLineWidth, "trackBarScaleLineWidth");
            this.trackBarScaleLineWidth.Minimum = 1;
            this.trackBarScaleLineWidth.Name = "trackBarScaleLineWidth";
            this.trackBarScaleLineWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip.SetToolTip(this.trackBarScaleLineWidth, resources.GetString("trackBarScaleLineWidth.ToolTip"));
            this.trackBarScaleLineWidth.Value = 3;
            this.trackBarScaleLineWidth.Scroll += new System.EventHandler(this.Draw);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            this.toolTip.SetToolTip(this.label16, resources.GetString("label16.ToolTip"));
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.radioButtonScaleDivisionFine);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonScaleDivisionMedium);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonScaleDivisionCoarse);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.toolTip.SetToolTip(this.flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
            // 
            // radioButtonScaleDivisionFine
            // 
            resources.ApplyResources(this.radioButtonScaleDivisionFine, "radioButtonScaleDivisionFine");
            this.radioButtonScaleDivisionFine.Name = "radioButtonScaleDivisionFine";
            this.toolTip.SetToolTip(this.radioButtonScaleDivisionFine, resources.GetString("radioButtonScaleDivisionFine.ToolTip"));
            this.radioButtonScaleDivisionFine.UseVisualStyleBackColor = true;
            this.radioButtonScaleDivisionFine.CheckedChanged += new System.EventHandler(this.Draw);
            // 
            // radioButtonScaleDivisionMedium
            // 
            resources.ApplyResources(this.radioButtonScaleDivisionMedium, "radioButtonScaleDivisionMedium");
            this.radioButtonScaleDivisionMedium.Checked = true;
            this.radioButtonScaleDivisionMedium.Name = "radioButtonScaleDivisionMedium";
            this.radioButtonScaleDivisionMedium.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonScaleDivisionMedium, resources.GetString("radioButtonScaleDivisionMedium.ToolTip"));
            this.radioButtonScaleDivisionMedium.UseVisualStyleBackColor = true;
            this.radioButtonScaleDivisionMedium.CheckedChanged += new System.EventHandler(this.Draw);
            // 
            // radioButtonScaleDivisionCoarse
            // 
            resources.ApplyResources(this.radioButtonScaleDivisionCoarse, "radioButtonScaleDivisionCoarse");
            this.radioButtonScaleDivisionCoarse.Name = "radioButtonScaleDivisionCoarse";
            this.toolTip.SetToolTip(this.radioButtonScaleDivisionCoarse, resources.GetString("radioButtonScaleDivisionCoarse.ToolTip"));
            this.radioButtonScaleDivisionCoarse.UseVisualStyleBackColor = true;
            // 
            // colorControlScaleAzimuth
            // 
            resources.ApplyResources(this.colorControlScaleAzimuth, "colorControlScaleAzimuth");
            this.colorControlScaleAzimuth.Argb = -8960954;
            this.colorControlScaleAzimuth.Blue = 70;
            this.colorControlScaleAzimuth.BlueF = 0.2745098F;
            this.colorControlScaleAzimuth.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlScaleAzimuth.Color = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(68)))), ((int)(((byte)(70)))));
            this.colorControlScaleAzimuth.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlScaleAzimuth.Green = 68;
            this.colorControlScaleAzimuth.GreenF = 0.2666667F;
            this.colorControlScaleAzimuth.Name = "colorControlScaleAzimuth";
            this.colorControlScaleAzimuth.Red = 119;
            this.colorControlScaleAzimuth.RedF = 0.4666667F;
            this.toolTip.SetToolTip(this.colorControlScaleAzimuth, resources.GetString("colorControlScaleAzimuth.ToolTip"));
            this.colorControlScaleAzimuth.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // colorControlScale2Theta
            // 
            resources.ApplyResources(this.colorControlScale2Theta, "colorControlScale2Theta");
            this.colorControlScale2Theta.Argb = -12303240;
            this.colorControlScale2Theta.Blue = 120;
            this.colorControlScale2Theta.BlueF = 0.4705882F;
            this.colorControlScale2Theta.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlScale2Theta.Color = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(120)))));
            this.colorControlScale2Theta.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlScale2Theta.Green = 68;
            this.colorControlScale2Theta.GreenF = 0.2666667F;
            this.colorControlScale2Theta.Name = "colorControlScale2Theta";
            this.colorControlScale2Theta.Red = 68;
            this.colorControlScale2Theta.RedF = 0.2666667F;
            this.toolTip.SetToolTip(this.colorControlScale2Theta, resources.GetString("colorControlScale2Theta.ToolTip"));
            this.colorControlScale2Theta.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // tabPageMisc
            // 
            resources.ApplyResources(this.tabPageMisc, "tabPageMisc");
            this.tabPageMisc.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMisc.Controls.Add(this.numericBoxDev);
            this.tabPageMisc.Controls.Add(this.numericBoxAcc);
            this.tabPageMisc.Controls.Add(this.button2);
            this.tabPageMisc.Controls.Add(this.button1);
            this.tabPageMisc.Controls.Add(this.groupBox5);
            this.tabPageMisc.Name = "tabPageMisc";
            this.toolTip.SetToolTip(this.tabPageMisc, resources.GetString("tabPageMisc.ToolTip"));
            // 
            // numericBoxDev
            // 
            resources.ApplyResources(this.numericBoxDev, "numericBoxDev");
            this.numericBoxDev.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDev.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDev.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDev.Name = "numericBoxDev";
            this.numericBoxDev.RadianValue = 0.023911010752322315D;
            this.numericBoxDev.SkipEventDuringInput = false;
            this.numericBoxDev.SmartIncrement = true;
            this.numericBoxDev.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDev.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxDev, resources.GetString("numericBoxDev.ToolTip"));
            this.numericBoxDev.Value = 1.37D;
            // 
            // numericBoxAcc
            // 
            resources.ApplyResources(this.numericBoxAcc, "numericBoxAcc");
            this.numericBoxAcc.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAcc.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAcc.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxAcc.Name = "numericBoxAcc";
            this.numericBoxAcc.RadianValue = 216.42082724729684D;
            this.numericBoxAcc.SkipEventDuringInput = false;
            this.numericBoxAcc.SmartIncrement = true;
            this.numericBoxAcc.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxAcc.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxAcc, resources.GetString("numericBoxAcc.ToolTip"));
            this.numericBoxAcc.Value = 12400D;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.toolTip.SetToolTip(this.button2, resources.GetString("button2.ToolTip"));
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.toolTip.SetToolTip(this.button1, resources.GetString("button1.ToolTip"));
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.trackBarRotationSpeed);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox5, resources.GetString("groupBox5.ToolTip"));
            // 
            // trackBarRotationSpeed
            // 
            resources.ApplyResources(this.trackBarRotationSpeed, "trackBarRotationSpeed");
            this.trackBarRotationSpeed.Maximum = 600;
            this.trackBarRotationSpeed.Minimum = 1;
            this.trackBarRotationSpeed.Name = "trackBarRotationSpeed";
            this.trackBarRotationSpeed.TickFrequency = 10000;
            this.trackBarRotationSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip.SetToolTip(this.trackBarRotationSpeed, resources.GetString("trackBarRotationSpeed.ToolTip"));
            this.trackBarRotationSpeed.Value = 150;
            // 
            // graphicsBox
            // 
            resources.ApplyResources(this.graphicsBox, "graphicsBox");
            this.graphicsBox.BackColor = System.Drawing.Color.Transparent;
            this.graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.graphicsBox.Name = "graphicsBox";
            this.graphicsBox.TabStop = false;
            this.toolTip.SetToolTip(this.graphicsBox, resources.GetString("graphicsBox.ToolTip"));
            this.graphicsBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.graphicsBox_MouseDown);
            this.graphicsBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graphicsBox_MouseMove);
            this.graphicsBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.graphicsBox_MouseUp);
            this.graphicsBox.Move += new System.EventHandler(this.Draw);
            this.graphicsBox.Resize += new System.EventHandler(this.graphicsBox_Resize);
            // 
            // labelDummy
            // 
            resources.ApplyResources(this.labelDummy, "labelDummy");
            this.labelDummy.Name = "labelDummy";
            this.toolTip.SetToolTip(this.labelDummy, resources.GetString("labelDummy.ToolTip"));
            // 
            // panelMousePosition
            // 
            resources.ApplyResources(this.panelMousePosition, "panelMousePosition");
            this.panelMousePosition.Controls.Add(this.label24);
            this.panelMousePosition.Controls.Add(this.labelMousePositionDetector);
            this.panelMousePosition.Controls.Add(this.labelMousePositionReal);
            this.panelMousePosition.Controls.Add(this.labelDinv);
            this.panelMousePosition.Controls.Add(this.checkBoxMousePositionDetailes);
            this.panelMousePosition.Controls.Add(this.labelMousePositionReciprocal);
            this.panelMousePosition.Controls.Add(this.labelTwoTheta);
            this.panelMousePosition.Controls.Add(this.labelD);
            this.panelMousePosition.Name = "panelMousePosition";
            this.toolTip.SetToolTip(this.panelMousePosition, resources.GetString("panelMousePosition.ToolTip"));
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            this.toolTip.SetToolTip(this.label24, resources.GetString("label24.ToolTip"));
            // 
            // labelMousePositionDetector
            // 
            resources.ApplyResources(this.labelMousePositionDetector, "labelMousePositionDetector");
            this.labelMousePositionDetector.Name = "labelMousePositionDetector";
            this.toolTip.SetToolTip(this.labelMousePositionDetector, resources.GetString("labelMousePositionDetector.ToolTip"));
            // 
            // labelMousePositionReal
            // 
            resources.ApplyResources(this.labelMousePositionReal, "labelMousePositionReal");
            this.labelMousePositionReal.Name = "labelMousePositionReal";
            this.toolTip.SetToolTip(this.labelMousePositionReal, resources.GetString("labelMousePositionReal.ToolTip"));
            // 
            // labelDinv
            // 
            resources.ApplyResources(this.labelDinv, "labelDinv");
            this.labelDinv.Name = "labelDinv";
            this.toolTip.SetToolTip(this.labelDinv, resources.GetString("labelDinv.ToolTip"));
            // 
            // checkBoxMousePositionDetailes
            // 
            resources.ApplyResources(this.checkBoxMousePositionDetailes, "checkBoxMousePositionDetailes");
            this.checkBoxMousePositionDetailes.Name = "checkBoxMousePositionDetailes";
            this.toolTip.SetToolTip(this.checkBoxMousePositionDetailes, resources.GetString("checkBoxMousePositionDetailes.ToolTip"));
            this.checkBoxMousePositionDetailes.UseVisualStyleBackColor = true;
            this.checkBoxMousePositionDetailes.CheckedChanged += new System.EventHandler(this.checkBoxMousePositionDetailes_CheckedChanged);
            // 
            // labelMousePositionReciprocal
            // 
            resources.ApplyResources(this.labelMousePositionReciprocal, "labelMousePositionReciprocal");
            this.labelMousePositionReciprocal.Name = "labelMousePositionReciprocal";
            this.toolTip.SetToolTip(this.labelMousePositionReciprocal, resources.GetString("labelMousePositionReciprocal.ToolTip"));
            // 
            // labelTwoTheta
            // 
            resources.ApplyResources(this.labelTwoTheta, "labelTwoTheta");
            this.labelTwoTheta.Name = "labelTwoTheta";
            this.toolTip.SetToolTip(this.labelTwoTheta, resources.GetString("labelTwoTheta.ToolTip"));
            // 
            // labelD
            // 
            resources.ApplyResources(this.labelD, "labelD");
            this.labelD.Name = "labelD";
            this.toolTip.SetToolTip(this.labelD, resources.GetString("labelD.ToolTip"));
            // 
            // flowLayoutPanel6
            // 
            resources.ApplyResources(this.flowLayoutPanel6, "flowLayoutPanel6");
            this.flowLayoutPanel6.Controls.Add(this.groupBox6);
            this.flowLayoutPanel6.Controls.Add(this.groupBox1);
            this.flowLayoutPanel6.Controls.Add(this.groupBox2);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.toolTip.SetToolTip(this.flowLayoutPanel6, resources.GetString("flowLayoutPanel6.ToolTip"));
            // 
            // groupBox6
            // 
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Controls.Add(this.numericBoxClientHeight);
            this.groupBox6.Controls.Add(this.numericBoxClientWidth);
            this.groupBox6.Controls.Add(this.numericBoxResolution);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox6, resources.GetString("groupBox6.ToolTip"));
            // 
            // numericBoxClientHeight
            // 
            resources.ApplyResources(this.numericBoxClientHeight, "numericBoxClientHeight");
            this.numericBoxClientHeight.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxClientHeight.DecimalPlaces = 0;
            this.numericBoxClientHeight.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxClientHeight.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxClientHeight.Maximum = 2000D;
            this.numericBoxClientHeight.Minimum = 1D;
            this.numericBoxClientHeight.Name = "numericBoxClientHeight";
            this.numericBoxClientHeight.RadianValue = 17.453292519943293D;
            this.numericBoxClientHeight.ShowUpDown = true;
            this.numericBoxClientHeight.SmartIncrement = true;
            this.numericBoxClientHeight.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxClientHeight.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxClientHeight, resources.GetString("numericBoxClientHeight.ToolTip"));
            this.numericBoxClientHeight.Value = 1000D;
            this.numericBoxClientHeight.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.NumericBoxClientSize_ValueChanged);
            // 
            // numericBoxClientWidth
            // 
            resources.ApplyResources(this.numericBoxClientWidth, "numericBoxClientWidth");
            this.numericBoxClientWidth.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxClientWidth.DecimalPlaces = 0;
            this.numericBoxClientWidth.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxClientWidth.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxClientWidth.Maximum = 2000D;
            this.numericBoxClientWidth.Minimum = 1D;
            this.numericBoxClientWidth.Name = "numericBoxClientWidth";
            this.numericBoxClientWidth.RadianValue = 17.453292519943293D;
            this.numericBoxClientWidth.ShowUpDown = true;
            this.numericBoxClientWidth.SmartIncrement = true;
            this.numericBoxClientWidth.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxClientWidth.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxClientWidth, resources.GetString("numericBoxClientWidth.ToolTip"));
            this.numericBoxClientWidth.Value = 1000D;
            this.numericBoxClientWidth.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.NumericBoxClientSize_ValueChanged);
            // 
            // numericBoxResolution
            // 
            resources.ApplyResources(this.numericBoxResolution, "numericBoxResolution");
            this.numericBoxResolution.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxResolution.DecimalPlaces = 5;
            this.numericBoxResolution.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxResolution.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxResolution.Maximum = 10D;
            this.numericBoxResolution.Minimum = 1E-05D;
            this.numericBoxResolution.Name = "numericBoxResolution";
            this.numericBoxResolution.RadianValue = 0.0013962634015954637D;
            this.numericBoxResolution.ShowUpDown = true;
            this.numericBoxResolution.SmartIncrement = true;
            this.numericBoxResolution.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxResolution.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxResolution, resources.GetString("numericBoxResolution.ToolTip"));
            this.numericBoxResolution.Value = 0.08D;
            this.numericBoxResolution.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericUpDownResolution_ValueChanged);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.numericUpDownCamaraLength2);
            this.groupBox1.Controls.Add(this.buttonDetailedGeometry);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // numericUpDownCamaraLength2
            // 
            resources.ApplyResources(this.numericUpDownCamaraLength2, "numericUpDownCamaraLength2");
            this.numericUpDownCamaraLength2.DecimalPlaces = 3;
            this.numericUpDownCamaraLength2.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCamaraLength2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownCamaraLength2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCamaraLength2.Name = "numericUpDownCamaraLength2";
            this.toolTip.SetToolTip(this.numericUpDownCamaraLength2, resources.GetString("numericUpDownCamaraLength2.ToolTip"));
            this.numericUpDownCamaraLength2.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownCamaraLength2.ValueChanged += new System.EventHandler(this.numericUpDownCamaraLength2_ValueChanged);
            // 
            // buttonDetailedGeometry
            // 
            resources.ApplyResources(this.buttonDetailedGeometry, "buttonDetailedGeometry");
            this.buttonDetailedGeometry.Name = "buttonDetailedGeometry";
            this.toolTip.SetToolTip(this.buttonDetailedGeometry, resources.GetString("buttonDetailedGeometry.ToolTip"));
            this.buttonDetailedGeometry.UseVisualStyleBackColor = true;
            this.buttonDetailedGeometry.Click += new System.EventHandler(this.buttonDetailedGeometry_Click);
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            this.toolTip.SetToolTip(this.label18, resources.GetString("label18.ToolTip"));
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            this.toolTip.SetToolTip(this.label15, resources.GetString("label15.ToolTip"));
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.buttonResetCenter);
            this.groupBox2.Controls.Add(this.radioButtonCenterToDetector);
            this.groupBox2.Controls.Add(this.radioButtonCenterToFoot);
            this.groupBox2.Controls.Add(this.checkBoxFixCenter);
            this.groupBox2.Controls.Add(this.radioButtonCenterToDirect);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // buttonResetCenter
            // 
            resources.ApplyResources(this.buttonResetCenter, "buttonResetCenter");
            this.buttonResetCenter.Name = "buttonResetCenter";
            this.toolTip.SetToolTip(this.buttonResetCenter, resources.GetString("buttonResetCenter.ToolTip"));
            this.buttonResetCenter.UseVisualStyleBackColor = true;
            this.buttonResetCenter.Click += new System.EventHandler(this.buttonResetCenter_Click_1);
            // 
            // radioButtonCenterToDetector
            // 
            resources.ApplyResources(this.radioButtonCenterToDetector, "radioButtonCenterToDetector");
            this.radioButtonCenterToDetector.Name = "radioButtonCenterToDetector";
            this.radioButtonCenterToDetector.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonCenterToDetector, resources.GetString("radioButtonCenterToDetector.ToolTip"));
            this.radioButtonCenterToDetector.UseVisualStyleBackColor = true;
            this.radioButtonCenterToDetector.CheckedChanged += new System.EventHandler(this.radioButtonCenterTo_CheckedChanged);
            // 
            // radioButtonCenterToFoot
            // 
            resources.ApplyResources(this.radioButtonCenterToFoot, "radioButtonCenterToFoot");
            this.radioButtonCenterToFoot.Checked = true;
            this.radioButtonCenterToFoot.Name = "radioButtonCenterToFoot";
            this.radioButtonCenterToFoot.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonCenterToFoot, resources.GetString("radioButtonCenterToFoot.ToolTip"));
            this.radioButtonCenterToFoot.UseVisualStyleBackColor = true;
            this.radioButtonCenterToFoot.CheckedChanged += new System.EventHandler(this.radioButtonCenterTo_CheckedChanged);
            // 
            // checkBoxFixCenter
            // 
            resources.ApplyResources(this.checkBoxFixCenter, "checkBoxFixCenter");
            this.checkBoxFixCenter.Name = "checkBoxFixCenter";
            this.toolTip.SetToolTip(this.checkBoxFixCenter, resources.GetString("checkBoxFixCenter.ToolTip"));
            this.checkBoxFixCenter.UseVisualStyleBackColor = true;
            this.checkBoxFixCenter.CheckedChanged += new System.EventHandler(this.checkBoxFixCenter_CheckedChanged);
            // 
            // radioButtonCenterToDirect
            // 
            resources.ApplyResources(this.radioButtonCenterToDirect, "radioButtonCenterToDirect");
            this.radioButtonCenterToDirect.Name = "radioButtonCenterToDirect";
            this.toolTip.SetToolTip(this.radioButtonCenterToDirect, resources.GetString("radioButtonCenterToDirect.ToolTip"));
            this.radioButtonCenterToDirect.UseVisualStyleBackColor = true;
            this.radioButtonCenterToDirect.CheckedChanged += new System.EventHandler(this.radioButtonCenterTo_CheckedChanged);
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.presetToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            this.toolTip.SetToolTip(this.menuStrip1, resources.GetString("menuStrip1.ToolTip"));
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem,
            this.saveDetectorAreaToolStripMenuItem,
            this.saveCBEDPatternToolStripMenuItem,
            this.copyImageToClipboardToolStripMenuItem,
            this.copyDetectorAreaToolStripMenuItem,
            this.copyCBEDPatternToolStripMenuItem,
            this.toolStripSeparator1,
            this.pageSetupToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.printToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // saveImageToolStripMenuItem
            // 
            resources.ApplyResources(this.saveImageToolStripMenuItem, "saveImageToolStripMenuItem");
            this.saveImageToolStripMenuItem.AutoToolTip = true;
            this.saveImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsImageToolStripMenuItem,
            this.saveAsMetafileToolStripMenuItem});
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            // 
            // saveAsImageToolStripMenuItem
            // 
            resources.ApplyResources(this.saveAsImageToolStripMenuItem, "saveAsImageToolStripMenuItem");
            this.saveAsImageToolStripMenuItem.Name = "saveAsImageToolStripMenuItem";
            this.saveAsImageToolStripMenuItem.Click += new System.EventHandler(this.saveAsImageToolStripMenuItem_Click);
            // 
            // saveAsMetafileToolStripMenuItem
            // 
            resources.ApplyResources(this.saveAsMetafileToolStripMenuItem, "saveAsMetafileToolStripMenuItem");
            this.saveAsMetafileToolStripMenuItem.Name = "saveAsMetafileToolStripMenuItem";
            this.saveAsMetafileToolStripMenuItem.Click += new System.EventHandler(this.saveAsMetafileToolStripMenuItem_Click);
            // 
            // saveDetectorAreaToolStripMenuItem
            // 
            resources.ApplyResources(this.saveDetectorAreaToolStripMenuItem, "saveDetectorAreaToolStripMenuItem");
            this.saveDetectorAreaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveDetectorAsImageToolStripMenuItem,
            this.saveDetectorAsMetafileToolStripMenuItem});
            this.saveDetectorAreaToolStripMenuItem.Name = "saveDetectorAreaToolStripMenuItem";
            // 
            // saveDetectorAsImageToolStripMenuItem
            // 
            resources.ApplyResources(this.saveDetectorAsImageToolStripMenuItem, "saveDetectorAsImageToolStripMenuItem");
            this.saveDetectorAsImageToolStripMenuItem.Name = "saveDetectorAsImageToolStripMenuItem";
            this.saveDetectorAsImageToolStripMenuItem.Click += new System.EventHandler(this.saveDetectorAsImageToolStripMenuItem_Click);
            // 
            // saveDetectorAsMetafileToolStripMenuItem
            // 
            resources.ApplyResources(this.saveDetectorAsMetafileToolStripMenuItem, "saveDetectorAsMetafileToolStripMenuItem");
            this.saveDetectorAsMetafileToolStripMenuItem.Name = "saveDetectorAsMetafileToolStripMenuItem";
            this.saveDetectorAsMetafileToolStripMenuItem.Click += new System.EventHandler(this.saveDetectorAsMetafileToolStripMenuItem_Click);
            // 
            // saveCBEDPatternToolStripMenuItem
            // 
            resources.ApplyResources(this.saveCBEDPatternToolStripMenuItem, "saveCBEDPatternToolStripMenuItem");
            this.saveCBEDPatternToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveCBEDasPngToolStripMenuItem,
            this.saveCBEDasTiffToolStripMenuItem,
            this.asPixelByPixelImagePNGFormatToolStripMenuItem});
            this.saveCBEDPatternToolStripMenuItem.Name = "saveCBEDPatternToolStripMenuItem";
            // 
            // saveCBEDasPngToolStripMenuItem
            // 
            resources.ApplyResources(this.saveCBEDasPngToolStripMenuItem, "saveCBEDasPngToolStripMenuItem");
            this.saveCBEDasPngToolStripMenuItem.Name = "saveCBEDasPngToolStripMenuItem";
            this.saveCBEDasPngToolStripMenuItem.Click += new System.EventHandler(this.saveCBEDasPngToolStripMenuItem_Click);
            // 
            // saveCBEDasTiffToolStripMenuItem
            // 
            resources.ApplyResources(this.saveCBEDasTiffToolStripMenuItem, "saveCBEDasTiffToolStripMenuItem");
            this.saveCBEDasTiffToolStripMenuItem.Name = "saveCBEDasTiffToolStripMenuItem";
            this.saveCBEDasTiffToolStripMenuItem.Click += new System.EventHandler(this.saveCBEDasTiffToolStripMenuItem_Click);
            // 
            // asPixelByPixelImagePNGFormatToolStripMenuItem
            // 
            resources.ApplyResources(this.asPixelByPixelImagePNGFormatToolStripMenuItem, "asPixelByPixelImagePNGFormatToolStripMenuItem");
            this.asPixelByPixelImagePNGFormatToolStripMenuItem.Name = "asPixelByPixelImagePNGFormatToolStripMenuItem";
            this.asPixelByPixelImagePNGFormatToolStripMenuItem.Click += new System.EventHandler(this.saveCBEDasCollectiveImageToolStripMenuItem_Click);
            // 
            // copyImageToClipboardToolStripMenuItem
            // 
            resources.ApplyResources(this.copyImageToClipboardToolStripMenuItem, "copyImageToClipboardToolStripMenuItem");
            this.copyImageToClipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAsImageToolStripMenuItem,
            this.copyAsMetafileToolStripMenuItem});
            this.copyImageToClipboardToolStripMenuItem.Name = "copyImageToClipboardToolStripMenuItem";
            // 
            // copyAsImageToolStripMenuItem
            // 
            resources.ApplyResources(this.copyAsImageToolStripMenuItem, "copyAsImageToolStripMenuItem");
            this.copyAsImageToolStripMenuItem.Name = "copyAsImageToolStripMenuItem";
            this.copyAsImageToolStripMenuItem.Click += new System.EventHandler(this.copyAsImageToolStripMenuItem1_Click);
            // 
            // copyAsMetafileToolStripMenuItem
            // 
            resources.ApplyResources(this.copyAsMetafileToolStripMenuItem, "copyAsMetafileToolStripMenuItem");
            this.copyAsMetafileToolStripMenuItem.Name = "copyAsMetafileToolStripMenuItem";
            this.copyAsMetafileToolStripMenuItem.Click += new System.EventHandler(this.copyAsMetafileToolStripMenuItem1_Click);
            // 
            // copyDetectorAreaToolStripMenuItem
            // 
            resources.ApplyResources(this.copyDetectorAreaToolStripMenuItem, "copyDetectorAreaToolStripMenuItem");
            this.copyDetectorAreaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyDetectorAsImageToolStripMenuItem,
            this.copyDetectorAsMetafileToolStripMenuItem});
            this.copyDetectorAreaToolStripMenuItem.Name = "copyDetectorAreaToolStripMenuItem";
            // 
            // copyDetectorAsImageToolStripMenuItem
            // 
            resources.ApplyResources(this.copyDetectorAsImageToolStripMenuItem, "copyDetectorAsImageToolStripMenuItem");
            this.copyDetectorAsImageToolStripMenuItem.Name = "copyDetectorAsImageToolStripMenuItem";
            this.copyDetectorAsImageToolStripMenuItem.Click += new System.EventHandler(this.copyDetectorAsImageWithOverlappeImageToolStripMenuItem_Click);
            // 
            // copyDetectorAsMetafileToolStripMenuItem
            // 
            resources.ApplyResources(this.copyDetectorAsMetafileToolStripMenuItem, "copyDetectorAsMetafileToolStripMenuItem");
            this.copyDetectorAsMetafileToolStripMenuItem.Name = "copyDetectorAsMetafileToolStripMenuItem";
            this.copyDetectorAsMetafileToolStripMenuItem.Click += new System.EventHandler(this.copyDetectorAsMetafileWithOverlappedImageToolStripMenuItem_Click);
            // 
            // copyCBEDPatternToolStripMenuItem
            // 
            resources.ApplyResources(this.copyCBEDPatternToolStripMenuItem, "copyCBEDPatternToolStripMenuItem");
            this.copyCBEDPatternToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyCBEDasImageToolStripMenuItem});
            this.copyCBEDPatternToolStripMenuItem.Name = "copyCBEDPatternToolStripMenuItem";
            // 
            // copyCBEDasImageToolStripMenuItem
            // 
            resources.ApplyResources(this.copyCBEDasImageToolStripMenuItem, "copyCBEDasImageToolStripMenuItem");
            this.copyCBEDasImageToolStripMenuItem.Name = "copyCBEDasImageToolStripMenuItem";
            this.copyCBEDasImageToolStripMenuItem.Click += new System.EventHandler(this.copyCBEDasImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // pageSetupToolStripMenuItem
            // 
            resources.ApplyResources(this.pageSetupToolStripMenuItem, "pageSetupToolStripMenuItem");
            this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
            this.pageSetupToolStripMenuItem.Click += new System.EventHandler(this.pageSetupToolStripMenuItem_Click);
            // 
            // printPreviewToolStripMenuItem
            // 
            resources.ApplyResources(this.printPreviewToolStripMenuItem, "printPreviewToolStripMenuItem");
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            resources.ApplyResources(this.optionToolStripMenuItem, "optionToolStripMenuItem");
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemBackLaue,
            this.toolStripSeparator4,
            this.dynamicCompressionToolStripMenuItem,
            this.toolStripSeparator5});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            // 
            // toolStripMenuItemBackLaue
            // 
            resources.ApplyResources(this.toolStripMenuItemBackLaue, "toolStripMenuItemBackLaue");
            this.toolStripMenuItemBackLaue.CheckOnClick = true;
            this.toolStripMenuItemBackLaue.Name = "toolStripMenuItemBackLaue";
            this.toolStripMenuItemBackLaue.CheckedChanged += new System.EventHandler(this.Draw);
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // dynamicCompressionToolStripMenuItem
            // 
            resources.ApplyResources(this.dynamicCompressionToolStripMenuItem, "dynamicCompressionToolStripMenuItem");
            this.dynamicCompressionToolStripMenuItem.Name = "dynamicCompressionToolStripMenuItem";
            this.dynamicCompressionToolStripMenuItem.Click += new System.EventHandler(this.dynamicCompressionToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // presetToolStripMenuItem
            // 
            resources.ApplyResources(this.presetToolStripMenuItem, "presetToolStripMenuItem");
            this.presetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.electron300KVToolStripMenuItem,
            this.electron200KVToolStripMenuItem,
            this.electron120KeVToolStripMenuItem,
            this.toolStripSeparator7,
            this.xray30KeVToolStripMenuItem,
            this.xray20KeVToolStripMenuItem,
            this.xrayMoKαToolStripMenuItem,
            this.xrayCuKαToolStripMenuItem});
            this.presetToolStripMenuItem.Name = "presetToolStripMenuItem";
            // 
            // electron300KVToolStripMenuItem
            // 
            resources.ApplyResources(this.electron300KVToolStripMenuItem, "electron300KVToolStripMenuItem");
            this.electron300KVToolStripMenuItem.Name = "electron300KVToolStripMenuItem";
            this.electron300KVToolStripMenuItem.Click += new System.EventHandler(this.presetToolStripMenuItem_Click);
            // 
            // electron200KVToolStripMenuItem
            // 
            resources.ApplyResources(this.electron200KVToolStripMenuItem, "electron200KVToolStripMenuItem");
            this.electron200KVToolStripMenuItem.Name = "electron200KVToolStripMenuItem";
            this.electron200KVToolStripMenuItem.Click += new System.EventHandler(this.presetToolStripMenuItem_Click);
            // 
            // electron120KeVToolStripMenuItem
            // 
            resources.ApplyResources(this.electron120KeVToolStripMenuItem, "electron120KeVToolStripMenuItem");
            this.electron120KeVToolStripMenuItem.Name = "electron120KeVToolStripMenuItem";
            this.electron120KeVToolStripMenuItem.Click += new System.EventHandler(this.presetToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            // 
            // xray30KeVToolStripMenuItem
            // 
            resources.ApplyResources(this.xray30KeVToolStripMenuItem, "xray30KeVToolStripMenuItem");
            this.xray30KeVToolStripMenuItem.Name = "xray30KeVToolStripMenuItem";
            this.xray30KeVToolStripMenuItem.Click += new System.EventHandler(this.presetToolStripMenuItem_Click);
            // 
            // xray20KeVToolStripMenuItem
            // 
            resources.ApplyResources(this.xray20KeVToolStripMenuItem, "xray20KeVToolStripMenuItem");
            this.xray20KeVToolStripMenuItem.Name = "xray20KeVToolStripMenuItem";
            this.xray20KeVToolStripMenuItem.Click += new System.EventHandler(this.presetToolStripMenuItem_Click);
            // 
            // xrayMoKαToolStripMenuItem
            // 
            resources.ApplyResources(this.xrayMoKαToolStripMenuItem, "xrayMoKαToolStripMenuItem");
            this.xrayMoKαToolStripMenuItem.Name = "xrayMoKαToolStripMenuItem";
            this.xrayMoKαToolStripMenuItem.Click += new System.EventHandler(this.presetToolStripMenuItem_Click);
            // 
            // xrayCuKαToolStripMenuItem
            // 
            resources.ApplyResources(this.xrayCuKαToolStripMenuItem, "xrayCuKαToolStripMenuItem");
            this.xrayCuKαToolStripMenuItem.Name = "xrayCuKαToolStripMenuItem";
            this.xrayCuKαToolStripMenuItem.Click += new System.EventHandler(this.presetToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.basicConceptOfBethesMethodToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // basicConceptOfBethesMethodToolStripMenuItem
            // 
            resources.ApplyResources(this.basicConceptOfBethesMethodToolStripMenuItem, "basicConceptOfBethesMethodToolStripMenuItem");
            this.basicConceptOfBethesMethodToolStripMenuItem.Name = "basicConceptOfBethesMethodToolStripMenuItem";
            this.basicConceptOfBethesMethodToolStripMenuItem.Click += new System.EventHandler(this.basicConceptOfBethesMethodToolStripMenuItem_Click);
            // 
            // radioButtonIntensityBethe
            // 
            resources.ApplyResources(this.radioButtonIntensityBethe, "radioButtonIntensityBethe");
            this.radioButtonIntensityBethe.Name = "radioButtonIntensityBethe";
            this.toolTip.SetToolTip(this.radioButtonIntensityBethe, resources.GetString("radioButtonIntensityBethe.ToolTip"));
            this.radioButtonIntensityBethe.UseVisualStyleBackColor = true;
            this.radioButtonIntensityBethe.CheckedChanged += new System.EventHandler(this.radioButtonIntensityCalculationMethod_CheckedChanged);
            // 
            // checkBoxUseCrystalColor
            // 
            resources.ApplyResources(this.checkBoxUseCrystalColor, "checkBoxUseCrystalColor");
            this.checkBoxUseCrystalColor.Name = "checkBoxUseCrystalColor";
            this.toolTip.SetToolTip(this.checkBoxUseCrystalColor, resources.GetString("checkBoxUseCrystalColor.ToolTip"));
            this.checkBoxUseCrystalColor.CheckedChanged += new System.EventHandler(this.checkBoxUseCrystalColor_CheckedChanged);
            // 
            // checkBoxExtinctionAll
            // 
            resources.ApplyResources(this.checkBoxExtinctionAll, "checkBoxExtinctionAll");
            this.checkBoxExtinctionAll.Name = "checkBoxExtinctionAll";
            this.toolTip.SetToolTip(this.checkBoxExtinctionAll, resources.GetString("checkBoxExtinctionAll.ToolTip"));
            this.checkBoxExtinctionAll.CheckedChanged += new System.EventHandler(this.checkBoxExtinctionAll_CheckedChanged);
            // 
            // checkBoxExtinctionLattice
            // 
            resources.ApplyResources(this.checkBoxExtinctionLattice, "checkBoxExtinctionLattice");
            this.checkBoxExtinctionLattice.Checked = true;
            this.checkBoxExtinctionLattice.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxExtinctionLattice.Name = "checkBoxExtinctionLattice";
            this.toolTip.SetToolTip(this.checkBoxExtinctionLattice, resources.GetString("checkBoxExtinctionLattice.ToolTip"));
            this.checkBoxExtinctionLattice.CheckedChanged += new System.EventHandler(this.checkBoxExtinctionAll_CheckedChanged);
            // 
            // groupBoxSpotProperty
            // 
            resources.ApplyResources(this.groupBoxSpotProperty, "groupBoxSpotProperty");
            this.groupBoxSpotProperty.Controls.Add(this.flowLayoutPanelPED);
            this.groupBoxSpotProperty.Controls.Add(this.flowLayoutPanelBethe);
            this.groupBoxSpotProperty.Controls.Add(this.flowLayoutPanelAppearance);
            this.groupBoxSpotProperty.Controls.Add(this.flowLayoutPanel3);
            this.groupBoxSpotProperty.Controls.Add(this.flowLayoutPanel5);
            this.groupBoxSpotProperty.Name = "groupBoxSpotProperty";
            this.groupBoxSpotProperty.TabStop = false;
            this.toolTip.SetToolTip(this.groupBoxSpotProperty, resources.GetString("groupBoxSpotProperty.ToolTip"));
            // 
            // flowLayoutPanelPED
            // 
            resources.ApplyResources(this.flowLayoutPanelPED, "flowLayoutPanelPED");
            this.flowLayoutPanelPED.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanelPED.Controls.Add(this.label5);
            this.flowLayoutPanelPED.Controls.Add(this.numericBoxPED_Semiangle);
            this.flowLayoutPanelPED.Controls.Add(this.numericBoxPED_Step);
            this.flowLayoutPanelPED.Name = "flowLayoutPanelPED";
            this.toolTip.SetToolTip(this.flowLayoutPanelPED, resources.GetString("flowLayoutPanelPED.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // numericBoxPED_Semiangle
            // 
            resources.ApplyResources(this.numericBoxPED_Semiangle, "numericBoxPED_Semiangle");
            this.numericBoxPED_Semiangle.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPED_Semiangle.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPED_Semiangle.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPED_Semiangle.Maximum = 500D;
            this.numericBoxPED_Semiangle.Minimum = 0.1D;
            this.numericBoxPED_Semiangle.Name = "numericBoxPED_Semiangle";
            this.numericBoxPED_Semiangle.RadianValue = 0.87266462599716477D;
            this.numericBoxPED_Semiangle.ShowUpDown = true;
            this.numericBoxPED_Semiangle.SmartIncrement = true;
            this.numericBoxPED_Semiangle.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPED_Semiangle.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxPED_Semiangle, resources.GetString("numericBoxPED_Semiangle.ToolTip"));
            this.numericBoxPED_Semiangle.Value = 50D;
            this.numericBoxPED_Semiangle.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.Draw);
            // 
            // numericBoxPED_Step
            // 
            resources.ApplyResources(this.numericBoxPED_Step, "numericBoxPED_Step");
            this.numericBoxPED_Step.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPED_Step.DecimalPlaces = 0;
            this.numericBoxPED_Step.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPED_Step.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPED_Step.Maximum = 1080D;
            this.numericBoxPED_Step.Minimum = 2D;
            this.numericBoxPED_Step.Name = "numericBoxPED_Step";
            this.numericBoxPED_Step.RadianValue = 0.62831853071795862D;
            this.numericBoxPED_Step.ShowUpDown = true;
            this.numericBoxPED_Step.SmartIncrement = true;
            this.numericBoxPED_Step.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPED_Step.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxPED_Step, resources.GetString("numericBoxPED_Step.ToolTip"));
            this.numericBoxPED_Step.Value = 36D;
            this.numericBoxPED_Step.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.Draw);
            // 
            // flowLayoutPanelBethe
            // 
            resources.ApplyResources(this.flowLayoutPanelBethe, "flowLayoutPanelBethe");
            this.flowLayoutPanelBethe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanelBethe.Controls.Add(this.label1);
            this.flowLayoutPanelBethe.Controls.Add(this.numericBoxNumOfBlochWave);
            this.flowLayoutPanelBethe.Controls.Add(this.numericBoxThickness);
            this.flowLayoutPanelBethe.Name = "flowLayoutPanelBethe";
            this.toolTip.SetToolTip(this.flowLayoutPanelBethe, resources.GetString("flowLayoutPanelBethe.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // numericBoxNumOfBlochWave
            // 
            resources.ApplyResources(this.numericBoxNumOfBlochWave, "numericBoxNumOfBlochWave");
            this.numericBoxNumOfBlochWave.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxNumOfBlochWave.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxNumOfBlochWave.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxNumOfBlochWave.Maximum = 1000D;
            this.numericBoxNumOfBlochWave.Minimum = 8D;
            this.numericBoxNumOfBlochWave.Name = "numericBoxNumOfBlochWave";
            this.numericBoxNumOfBlochWave.RadianValue = 4.1887902047863905D;
            this.numericBoxNumOfBlochWave.ShowUpDown = true;
            this.numericBoxNumOfBlochWave.SmartIncrement = true;
            this.numericBoxNumOfBlochWave.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxNumOfBlochWave.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxNumOfBlochWave, resources.GetString("numericBoxNumOfBlochWave.ToolTip"));
            this.numericBoxNumOfBlochWave.Value = 240D;
            this.numericBoxNumOfBlochWave.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.Draw);
            // 
            // numericBoxThickness
            // 
            resources.ApplyResources(this.numericBoxThickness, "numericBoxThickness");
            this.numericBoxThickness.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxThickness.DecimalPlaces = 2;
            this.numericBoxThickness.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxThickness.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxThickness.Maximum = 10000D;
            this.numericBoxThickness.Minimum = 0.01D;
            this.numericBoxThickness.Name = "numericBoxThickness";
            this.numericBoxThickness.RadianValue = 0.87266462599716477D;
            this.numericBoxThickness.ShowUpDown = true;
            this.numericBoxThickness.SkipEventDuringInput = false;
            this.numericBoxThickness.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxThickness.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxThickness, resources.GetString("numericBoxThickness.ToolTip"));
            this.numericBoxThickness.UpDown_Increment = 10D;
            this.numericBoxThickness.Value = 50D;
            this.numericBoxThickness.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.Draw);
            // 
            // flowLayoutPanelAppearance
            // 
            resources.ApplyResources(this.flowLayoutPanelAppearance, "flowLayoutPanelAppearance");
            this.flowLayoutPanelAppearance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanelAppearance.Controls.Add(this.label19);
            this.flowLayoutPanelAppearance.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanelAppearance.Controls.Add(this.flowLayoutPanel7);
            this.flowLayoutPanelAppearance.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanelAppearance.Controls.Add(this.flowLayoutPanelGaussianOption);
            this.flowLayoutPanelAppearance.Controls.Add(this.flowLayoutPanelSpotColor);
            this.flowLayoutPanelAppearance.Name = "flowLayoutPanelAppearance";
            this.toolTip.SetToolTip(this.flowLayoutPanelAppearance, resources.GetString("flowLayoutPanelAppearance.ToolTip"));
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            this.toolTip.SetToolTip(this.label19, resources.GetString("label19.ToolTip"));
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(this.flowLayoutPanel4, "flowLayoutPanel4");
            this.flowLayoutPanel4.Controls.Add(this.radioButtonCircleArea);
            this.flowLayoutPanel4.Controls.Add(this.radioButtonPointSpread);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.toolTip.SetToolTip(this.flowLayoutPanel4, resources.GetString("flowLayoutPanel4.ToolTip"));
            // 
            // radioButtonCircleArea
            // 
            resources.ApplyResources(this.radioButtonCircleArea, "radioButtonCircleArea");
            this.radioButtonCircleArea.Checked = true;
            this.radioButtonCircleArea.Name = "radioButtonCircleArea";
            this.radioButtonCircleArea.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonCircleArea, resources.GetString("radioButtonCircleArea.ToolTip"));
            this.radioButtonCircleArea.UseVisualStyleBackColor = true;
            this.radioButtonCircleArea.CheckedChanged += new System.EventHandler(this.radioButtonPointSpread_CheckedChanged);
            // 
            // radioButtonPointSpread
            // 
            resources.ApplyResources(this.radioButtonPointSpread, "radioButtonPointSpread");
            this.radioButtonPointSpread.Name = "radioButtonPointSpread";
            this.toolTip.SetToolTip(this.radioButtonPointSpread, resources.GetString("radioButtonPointSpread.ToolTip"));
            this.radioButtonPointSpread.UseVisualStyleBackColor = true;
            this.radioButtonPointSpread.CheckedChanged += new System.EventHandler(this.radioButtonPointSpread_CheckedChanged);
            // 
            // flowLayoutPanel7
            // 
            resources.ApplyResources(this.flowLayoutPanel7, "flowLayoutPanel7");
            this.flowLayoutPanel7.Controls.Add(this.label8);
            this.flowLayoutPanel7.Controls.Add(this.trackBarSpotOpacity);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.toolTip.SetToolTip(this.flowLayoutPanel7, resources.GetString("flowLayoutPanel7.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.toolTip.SetToolTip(this.label8, resources.GetString("label8.ToolTip"));
            // 
            // trackBarSpotOpacity
            // 
            resources.ApplyResources(this.trackBarSpotOpacity, "trackBarSpotOpacity");
            this.trackBarSpotOpacity.LargeChange = 20;
            this.trackBarSpotOpacity.Maximum = 100;
            this.trackBarSpotOpacity.Name = "trackBarSpotOpacity";
            this.trackBarSpotOpacity.SmallChange = 10;
            this.trackBarSpotOpacity.TickFrequency = 500;
            this.trackBarSpotOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip.SetToolTip(this.trackBarSpotOpacity, resources.GetString("trackBarSpotOpacity.ToolTip"));
            this.trackBarSpotOpacity.Value = 100;
            this.trackBarSpotOpacity.ValueChanged += new System.EventHandler(this.Draw);
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Controls.Add(this.numericBoxSpotRadius);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.toolTip.SetToolTip(this.flowLayoutPanel2, resources.GetString("flowLayoutPanel2.ToolTip"));
            // 
            // numericBoxSpotRadius
            // 
            resources.ApplyResources(this.numericBoxSpotRadius, "numericBoxSpotRadius");
            this.numericBoxSpotRadius.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxSpotRadius.DecimalPlaces = 4;
            this.numericBoxSpotRadius.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxSpotRadius.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxSpotRadius.Maximum = 1D;
            this.numericBoxSpotRadius.Minimum = 0.01D;
            this.numericBoxSpotRadius.Name = "numericBoxSpotRadius";
            this.numericBoxSpotRadius.RadianValue = 0.0034906585039886592D;
            this.numericBoxSpotRadius.ShowUpDown = true;
            this.numericBoxSpotRadius.SkipEventDuringInput = false;
            this.numericBoxSpotRadius.SmartIncrement = true;
            this.numericBoxSpotRadius.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxSpotRadius.ThonsandsSeparator = true;
            this.toolTip.SetToolTip(this.numericBoxSpotRadius, resources.GetString("numericBoxSpotRadius.ToolTip"));
            this.numericBoxSpotRadius.UpDown_Increment = 0.01D;
            this.numericBoxSpotRadius.Value = 0.2D;
            this.numericBoxSpotRadius.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.Draw);
            // 
            // flowLayoutPanelGaussianOption
            // 
            resources.ApplyResources(this.flowLayoutPanelGaussianOption, "flowLayoutPanelGaussianOption");
            this.flowLayoutPanelGaussianOption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanelGaussianOption.Controls.Add(this.flowLayoutPanel8);
            this.flowLayoutPanelGaussianOption.Controls.Add(this.flowLayoutPanel9);
            this.flowLayoutPanelGaussianOption.Controls.Add(this.checkBoxLogScale);
            this.flowLayoutPanelGaussianOption.Controls.Add(this.flowLayoutPanelColorScale);
            this.flowLayoutPanelGaussianOption.Name = "flowLayoutPanelGaussianOption";
            this.toolTip.SetToolTip(this.flowLayoutPanelGaussianOption, resources.GetString("flowLayoutPanelGaussianOption.ToolTip"));
            // 
            // flowLayoutPanel8
            // 
            resources.ApplyResources(this.flowLayoutPanel8, "flowLayoutPanel8");
            this.flowLayoutPanel8.Controls.Add(this.label10);
            this.flowLayoutPanel8.Controls.Add(this.trackBarIntensityForPointSpread);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.toolTip.SetToolTip(this.flowLayoutPanel8, resources.GetString("flowLayoutPanel8.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            this.toolTip.SetToolTip(this.label10, resources.GetString("label10.ToolTip"));
            // 
            // trackBarIntensityForPointSpread
            // 
            resources.ApplyResources(this.trackBarIntensityForPointSpread, "trackBarIntensityForPointSpread");
            this.trackBarIntensityForPointSpread.LargeChange = 50;
            this.trackBarIntensityForPointSpread.Maximum = 800;
            this.trackBarIntensityForPointSpread.Minimum = 1;
            this.trackBarIntensityForPointSpread.Name = "trackBarIntensityForPointSpread";
            this.trackBarIntensityForPointSpread.SmallChange = 10;
            this.trackBarIntensityForPointSpread.TickFrequency = 500;
            this.trackBarIntensityForPointSpread.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip.SetToolTip(this.trackBarIntensityForPointSpread, resources.GetString("trackBarIntensityForPointSpread.ToolTip"));
            this.trackBarIntensityForPointSpread.Value = 400;
            this.trackBarIntensityForPointSpread.ValueChanged += new System.EventHandler(this.Draw);
            // 
            // flowLayoutPanel9
            // 
            resources.ApplyResources(this.flowLayoutPanel9, "flowLayoutPanel9");
            this.flowLayoutPanel9.Controls.Add(this.label25);
            this.flowLayoutPanel9.Controls.Add(this.comboBoxScaleColorScale);
            this.flowLayoutPanel9.Name = "flowLayoutPanel9";
            this.toolTip.SetToolTip(this.flowLayoutPanel9, resources.GetString("flowLayoutPanel9.ToolTip"));
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            this.toolTip.SetToolTip(this.label25, resources.GetString("label25.ToolTip"));
            // 
            // comboBoxScaleColorScale
            // 
            resources.ApplyResources(this.comboBoxScaleColorScale, "comboBoxScaleColorScale");
            this.comboBoxScaleColorScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScaleColorScale.FormattingEnabled = true;
            this.comboBoxScaleColorScale.Items.AddRange(new object[] {
            resources.GetString("comboBoxScaleColorScale.Items"),
            resources.GetString("comboBoxScaleColorScale.Items1")});
            this.comboBoxScaleColorScale.Name = "comboBoxScaleColorScale";
            this.toolTip.SetToolTip(this.comboBoxScaleColorScale, resources.GetString("comboBoxScaleColorScale.ToolTip"));
            this.comboBoxScaleColorScale.SelectedIndexChanged += new System.EventHandler(this.comboBoxScaleColorScale_SelectedIndexChanged);
            // 
            // checkBoxLogScale
            // 
            resources.ApplyResources(this.checkBoxLogScale, "checkBoxLogScale");
            this.checkBoxLogScale.Name = "checkBoxLogScale";
            this.toolTip.SetToolTip(this.checkBoxLogScale, resources.GetString("checkBoxLogScale.ToolTip"));
            this.checkBoxLogScale.UseVisualStyleBackColor = true;
            this.checkBoxLogScale.CheckedChanged += new System.EventHandler(this.Draw);
            // 
            // flowLayoutPanelColorScale
            // 
            resources.ApplyResources(this.flowLayoutPanelColorScale, "flowLayoutPanelColorScale");
            this.flowLayoutPanelColorScale.Name = "flowLayoutPanelColorScale";
            this.toolTip.SetToolTip(this.flowLayoutPanelColorScale, resources.GetString("flowLayoutPanelColorScale.ToolTip"));
            // 
            // flowLayoutPanelSpotColor
            // 
            resources.ApplyResources(this.flowLayoutPanelSpotColor, "flowLayoutPanelSpotColor");
            this.flowLayoutPanelSpotColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanelSpotColor.Controls.Add(this.label2);
            this.flowLayoutPanelSpotColor.Controls.Add(this.checkBoxUseCrystalColor);
            this.flowLayoutPanelSpotColor.Controls.Add(this.colorControlOrigin);
            this.flowLayoutPanelSpotColor.Controls.Add(this.colorControlNoCondition);
            this.flowLayoutPanelSpotColor.Controls.Add(this.colorControlScrewGlide);
            this.flowLayoutPanelSpotColor.Controls.Add(this.colorControlForbiddenLattice);
            this.flowLayoutPanelSpotColor.Name = "flowLayoutPanelSpotColor";
            this.toolTip.SetToolTip(this.flowLayoutPanelSpotColor, resources.GetString("flowLayoutPanelSpotColor.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // colorControlOrigin
            // 
            resources.ApplyResources(this.colorControlOrigin, "colorControlOrigin");
            this.colorControlOrigin.Argb = -65536;
            this.colorControlOrigin.BackColor = System.Drawing.Color.Transparent;
            this.colorControlOrigin.Blue = 0;
            this.colorControlOrigin.BlueF = 0F;
            this.colorControlOrigin.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlOrigin.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorControlOrigin.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlOrigin.Green = 0;
            this.colorControlOrigin.GreenF = 0F;
            this.colorControlOrigin.Name = "colorControlOrigin";
            this.colorControlOrigin.Red = 255;
            this.colorControlOrigin.RedF = 1F;
            this.toolTip.SetToolTip(this.colorControlOrigin, resources.GetString("colorControlOrigin.ToolTip1"));
            this.colorControlOrigin.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // colorControlNoCondition
            // 
            resources.ApplyResources(this.colorControlNoCondition, "colorControlNoCondition");
            this.colorControlNoCondition.Argb = -1;
            this.colorControlNoCondition.Blue = 255;
            this.colorControlNoCondition.BlueF = 1F;
            this.colorControlNoCondition.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlNoCondition.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.colorControlNoCondition.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlNoCondition.Green = 255;
            this.colorControlNoCondition.GreenF = 1F;
            this.colorControlNoCondition.Name = "colorControlNoCondition";
            this.colorControlNoCondition.Red = 255;
            this.colorControlNoCondition.RedF = 1F;
            this.toolTip.SetToolTip(this.colorControlNoCondition, resources.GetString("colorControlNoCondition.ToolTip1"));
            this.colorControlNoCondition.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // colorControlScrewGlide
            // 
            resources.ApplyResources(this.colorControlScrewGlide, "colorControlScrewGlide");
            this.colorControlScrewGlide.Argb = -16192;
            this.colorControlScrewGlide.Blue = 192;
            this.colorControlScrewGlide.BlueF = 0.7529412F;
            this.colorControlScrewGlide.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlScrewGlide.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.colorControlScrewGlide.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlScrewGlide.Green = 192;
            this.colorControlScrewGlide.GreenF = 0.7529412F;
            this.colorControlScrewGlide.Name = "colorControlScrewGlide";
            this.colorControlScrewGlide.Red = 255;
            this.colorControlScrewGlide.RedF = 1F;
            this.toolTip.SetToolTip(this.colorControlScrewGlide, resources.GetString("colorControlScrewGlide.ToolTip1"));
            this.colorControlScrewGlide.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // colorControlForbiddenLattice
            // 
            resources.ApplyResources(this.colorControlForbiddenLattice, "colorControlForbiddenLattice");
            this.colorControlForbiddenLattice.Argb = -4144897;
            this.colorControlForbiddenLattice.Blue = 255;
            this.colorControlForbiddenLattice.BlueF = 1F;
            this.colorControlForbiddenLattice.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlForbiddenLattice.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.colorControlForbiddenLattice.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlForbiddenLattice.Green = 192;
            this.colorControlForbiddenLattice.GreenF = 0.7529412F;
            this.colorControlForbiddenLattice.Name = "colorControlForbiddenLattice";
            this.colorControlForbiddenLattice.Red = 192;
            this.colorControlForbiddenLattice.RedF = 0.7529412F;
            this.toolTip.SetToolTip(this.colorControlForbiddenLattice, resources.GetString("colorControlForbiddenLattice.ToolTip1"));
            this.colorControlForbiddenLattice.ColorChanged += new System.EventHandler(this.Draw);
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel3.Controls.Add(this.label7);
            this.flowLayoutPanel3.Controls.Add(this.radioButtonIntensityExcitation);
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanelExtinctionOption);
            this.flowLayoutPanel3.Controls.Add(this.radioButtonIntensityKinematical);
            this.flowLayoutPanel3.Controls.Add(this.radioButtonIntensityBethe);
            this.flowLayoutPanel3.Controls.Add(this.buttonDetailsOfSpots);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.toolTip.SetToolTip(this.flowLayoutPanel3, resources.GetString("flowLayoutPanel3.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // radioButtonIntensityExcitation
            // 
            resources.ApplyResources(this.radioButtonIntensityExcitation, "radioButtonIntensityExcitation");
            this.radioButtonIntensityExcitation.Checked = true;
            this.radioButtonIntensityExcitation.Name = "radioButtonIntensityExcitation";
            this.radioButtonIntensityExcitation.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonIntensityExcitation, resources.GetString("radioButtonIntensityExcitation.ToolTip"));
            this.radioButtonIntensityExcitation.UseVisualStyleBackColor = true;
            this.radioButtonIntensityExcitation.CheckedChanged += new System.EventHandler(this.radioButtonIntensityCalculationMethod_CheckedChanged);
            // 
            // flowLayoutPanelExtinctionOption
            // 
            resources.ApplyResources(this.flowLayoutPanelExtinctionOption, "flowLayoutPanelExtinctionOption");
            this.flowLayoutPanelExtinctionOption.Controls.Add(this.checkBoxExtinctionAll);
            this.flowLayoutPanelExtinctionOption.Controls.Add(this.checkBoxExtinctionLattice);
            this.flowLayoutPanelExtinctionOption.Name = "flowLayoutPanelExtinctionOption";
            this.toolTip.SetToolTip(this.flowLayoutPanelExtinctionOption, resources.GetString("flowLayoutPanelExtinctionOption.ToolTip"));
            // 
            // radioButtonIntensityKinematical
            // 
            resources.ApplyResources(this.radioButtonIntensityKinematical, "radioButtonIntensityKinematical");
            this.radioButtonIntensityKinematical.Name = "radioButtonIntensityKinematical";
            this.toolTip.SetToolTip(this.radioButtonIntensityKinematical, resources.GetString("radioButtonIntensityKinematical.ToolTip"));
            this.radioButtonIntensityKinematical.UseVisualStyleBackColor = true;
            this.radioButtonIntensityKinematical.CheckedChanged += new System.EventHandler(this.radioButtonIntensityCalculationMethod_CheckedChanged);
            // 
            // buttonDetailsOfSpots
            // 
            resources.ApplyResources(this.buttonDetailsOfSpots, "buttonDetailsOfSpots");
            this.buttonDetailsOfSpots.Name = "buttonDetailsOfSpots";
            this.toolTip.SetToolTip(this.buttonDetailsOfSpots, resources.GetString("buttonDetailsOfSpots.ToolTip"));
            this.buttonDetailsOfSpots.UseVisualStyleBackColor = true;
            this.buttonDetailsOfSpots.Click += new System.EventHandler(this.ButtonDetailsOfSpots_Click);
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(this.flowLayoutPanel5, "flowLayoutPanel5");
            this.flowLayoutPanel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanel5.Controls.Add(this.label13);
            this.flowLayoutPanel5.Controls.Add(this.flowLayoutPanel10);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.toolTip.SetToolTip(this.flowLayoutPanel5, resources.GetString("flowLayoutPanel5.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            this.toolTip.SetToolTip(this.label13, resources.GetString("label13.ToolTip"));
            // 
            // flowLayoutPanel10
            // 
            resources.ApplyResources(this.flowLayoutPanel10, "flowLayoutPanel10");
            this.flowLayoutPanel10.Controls.Add(this.radioButtonBeamParallel);
            this.flowLayoutPanel10.Controls.Add(this.radioButtonBeamPrecession);
            this.flowLayoutPanel10.Controls.Add(this.radioButtonBeamConvergence);
            this.flowLayoutPanel10.Name = "flowLayoutPanel10";
            this.toolTip.SetToolTip(this.flowLayoutPanel10, resources.GetString("flowLayoutPanel10.ToolTip"));
            // 
            // radioButtonBeamParallel
            // 
            resources.ApplyResources(this.radioButtonBeamParallel, "radioButtonBeamParallel");
            this.radioButtonBeamParallel.Checked = true;
            this.radioButtonBeamParallel.Name = "radioButtonBeamParallel";
            this.radioButtonBeamParallel.TabStop = true;
            this.toolTip.SetToolTip(this.radioButtonBeamParallel, resources.GetString("radioButtonBeamParallel.ToolTip"));
            this.radioButtonBeamParallel.UseVisualStyleBackColor = true;
            this.radioButtonBeamParallel.CheckedChanged += new System.EventHandler(this.radioButtonIntensityCalculationMethod_CheckedChanged);
            // 
            // radioButtonBeamPrecession
            // 
            resources.ApplyResources(this.radioButtonBeamPrecession, "radioButtonBeamPrecession");
            this.radioButtonBeamPrecession.Name = "radioButtonBeamPrecession";
            this.toolTip.SetToolTip(this.radioButtonBeamPrecession, resources.GetString("radioButtonBeamPrecession.ToolTip"));
            this.radioButtonBeamPrecession.UseVisualStyleBackColor = true;
            this.radioButtonBeamPrecession.CheckedChanged += new System.EventHandler(this.radioButtonIntensityCalculationMethod_CheckedChanged);
            // 
            // radioButtonBeamConvergence
            // 
            resources.ApplyResources(this.radioButtonBeamConvergence, "radioButtonBeamConvergence");
            this.radioButtonBeamConvergence.Name = "radioButtonBeamConvergence";
            this.toolTip.SetToolTip(this.radioButtonBeamConvergence, resources.GetString("radioButtonBeamConvergence.ToolTip"));
            this.radioButtonBeamConvergence.UseVisualStyleBackColor = true;
            this.radioButtonBeamConvergence.CheckedChanged += new System.EventHandler(this.radioButtonIntensityCalculationMethod_CheckedChanged);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            // 
            // printPreviewDialog1
            // 
            resources.ApplyResources(this.printPreviewDialog1, "printPreviewDialog1");
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.toolTip.SetToolTip(this.printPreviewDialog1, resources.GetString("printPreviewDialog1.ToolTip"));
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.toolTip.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            this.pageSetupDialog1.ShowHelp = true;
            // 
            // printDialog1
            // 
            this.printDialog1.AllowCurrentPage = true;
            this.printDialog1.AllowSelection = true;
            this.printDialog1.AllowSomePages = true;
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.PrintToFile = true;
            this.printDialog1.UseEXDialog = true;
            // 
            // timerBlinkSpot
            // 
            this.timerBlinkSpot.Interval = 400;
            this.timerBlinkSpot.Tag = "true";
            this.timerBlinkSpot.Tick += new System.EventHandler(this.timerBlinkSpot_Tick);
            // 
            // timerBlinkKikuchiLine
            // 
            this.timerBlinkKikuchiLine.Interval = 400;
            this.timerBlinkKikuchiLine.Tick += new System.EventHandler(this.timerBlinkKikuchiLine_Tick);
            // 
            // timerBlinkDebyeRing
            // 
            this.timerBlinkDebyeRing.Interval = 400;
            this.timerBlinkDebyeRing.Tick += new System.EventHandler(this.timerBlinkDebyering_Tick);
            // 
            // timerBlinkScale
            // 
            this.timerBlinkScale.Interval = 400;
            this.timerBlinkScale.Tag = "";
            this.timerBlinkScale.Tick += new System.EventHandler(this.timerBlinkScale_Tick);
            // 
            // FormDiffractionSimulator
            // 
            resources.ApplyResources(this, "$this");
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBoxSpotProperty);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormDiffractionSimulator";
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormElectronDiffraction_FormClosing);
            this.Load += new System.EventHandler(this.FormElectronDiffraction_Load);
            this.ResizeBegin += new System.EventHandler(this.FormDiffractionSimulator_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.FormElectronDiffraction_ResizeEnd);
            this.VisibleChanged += new System.EventHandler(this.FormElectronDiffraction_VisibleChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDiffractionSimulator_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDiffractionSimulator_DragEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormDiffractionSimulator_Paint);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageWave.ResumeLayout(false);
            this.tabPageWave.PerformLayout();
            this.tabPageGeneral.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStrSize)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPageKikuchi.ResumeLayout(false);
            this.tabPageKikuchi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLineWidth)).EndInit();
            this.tabPageDebye.ResumeLayout(false);
            this.tabPageDebye.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDebyeRingWidth)).EndInit();
            this.tabPageScale.ResumeLayout(false);
            this.tabPageScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScaleLineWidth)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabPageMisc.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotationSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.graphicsBox)).EndInit();
            this.panelMousePosition.ResumeLayout(false);
            this.panelMousePosition.PerformLayout();
            this.flowLayoutPanel6.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCamaraLength2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxSpotProperty.ResumeLayout(false);
            this.groupBoxSpotProperty.PerformLayout();
            this.flowLayoutPanelPED.ResumeLayout(false);
            this.flowLayoutPanelPED.PerformLayout();
            this.flowLayoutPanelBethe.ResumeLayout(false);
            this.flowLayoutPanelBethe.PerformLayout();
            this.flowLayoutPanelAppearance.ResumeLayout(false);
            this.flowLayoutPanelAppearance.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSpotOpacity)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanelGaussianOption.ResumeLayout(false);
            this.flowLayoutPanelGaussianOption.PerformLayout();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.flowLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarIntensityForPointSpread)).EndInit();
            this.flowLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel9.PerformLayout();
            this.flowLayoutPanelSpotColor.ResumeLayout(false);
            this.flowLayoutPanelSpotColor.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanelExtinctionOption.ResumeLayout(false);
            this.flowLayoutPanelExtinctionOption.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.flowLayoutPanel10.ResumeLayout(false);
            this.flowLayoutPanel10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxExtinctionAll;
        private System.Windows.Forms.CheckBox checkBoxExtinctionLattice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar trackBarLineWidth;
        private System.Windows.Forms.TrackBar trackBarStrSize;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.NumericUpDown numericUpDownCamaraLength2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageKikuchi;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyImageToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem pageSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonIndexLabels;
        private System.Windows.Forms.ToolStripButton toolStripButtonDspacing;
        private System.Windows.Forms.ToolStripButton toolStripButtonDistance;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton toolStripButtonKikuchiLines;
        private System.Windows.Forms.ToolStripButton toolStripButtonExcitationError;
        private System.Windows.Forms.ToolStripButton toolStripButtonFg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTimeForSearchingG;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemBackLaue;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TrackBar trackBarRotationSpeed;
        public Crystallography.Controls.WaveLengthControl waveLengthControl;
        private System.Windows.Forms.Label labelD;
        private System.Windows.Forms.Label labelMousePositionDetector;
        private System.Windows.Forms.Label labelMousePositionReciprocal;
        private System.Windows.Forms.TrackBar trackBarIntensityForPointSpread;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonDetailedGeometry;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageDebye;
        private System.Windows.Forms.GroupBox groupBoxSpotProperty;
        private System.Windows.Forms.RadioButton radioButtonCircleArea;
        private System.Windows.Forms.RadioButton radioButtonPointSpread;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonResetCenter;
        public Crystallography.Controls.NumericBox numericBoxResolution;
        public ImagingSolution.Control.GraphicsBox graphicsBox;
        private System.Windows.Forms.ToolStripMenuItem saveAsImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsMetafileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAsMetafileToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonDiffractionSpots;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonDebyeRing;
        private System.Windows.Forms.CheckBox checkBoxDebyeRingIgnoreIntensity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar trackBarDebyeRingWidth;
        private System.Windows.Forms.RadioButton radioButtonIntensityExcitation;
        private System.Windows.Forms.Label labelTwoTheta;
        private System.Windows.Forms.Timer timerBlinkSpot;
        private System.Windows.Forms.Timer timerBlinkKikuchiLine;
        private System.Windows.Forms.Timer timerBlinkDebyeRing;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAppearance;
        private System.Windows.Forms.CheckBox checkBoxDebyeRingLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBarSpotOpacity;
        private System.Windows.Forms.ToolStripMenuItem copyAsImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyDetectorAsImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyDetectorAsMetafileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem copyDetectorAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDetectorAsImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDetectorAsMetafileToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem saveDetectorAreaToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTimeForDrawing;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.CheckBox checkBoxUseCrystalColor;
        private System.Windows.Forms.CheckBox checkBoxLogScale;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBethe;
        private System.Windows.Forms.ToolStripMenuItem dynamicCompressionToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButtonIntensityKinematical;
        public System.Windows.Forms.RadioButton radioButtonIntensityBethe;
        private System.Windows.Forms.ToolStripMenuItem saveCBEDPatternToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCBEDasPngToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxFixCenter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem saveCBEDasTiffToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTimeForBethe;
        private System.Windows.Forms.TabPage tabPageMisc;
        public Crystallography.Controls.NumericBox numericBoxClientHeight;
        public Crystallography.Controls.NumericBox numericBoxClientWidth;
        private System.Windows.Forms.Button button1;
        private Crystallography.Controls.NumericBox numericBoxDev;
        private Crystallography.Controls.NumericBox numericBoxAcc;
        private Crystallography.Controls.NumericBox numericBoxPED_Semiangle;
        private Crystallography.Controls.NumericBox numericBoxPED_Step;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPED;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton radioButtonBeamParallel;
        private System.Windows.Forms.RadioButton radioButtonBeamPrecession;
        public System.Windows.Forms.RadioButton radioButtonBeamConvergence;
        private System.Windows.Forms.Label labelDinv;
        private System.Windows.Forms.Button buttonDetailsOfSpots;
        public Crystallography.Controls.NumericBox numericBoxNumOfBlochWave;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.ComboBox comboBoxScaleColorScale;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelColorScale;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem basicConceptOfBethesMethodToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPageWave;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGaussianOption;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private Crystallography.Controls.NumericBox numericBoxSpotRadius;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExtinctionOption;
        private System.Windows.Forms.GroupBox groupBox4;
        public Crystallography.Controls.ColorControl colorControlOrigin;
        public Crystallography.Controls.ColorControl colorControlNoCondition;
        public Crystallography.Controls.ColorControl colorControlForbiddenLattice;
        public Crystallography.Controls.ColorControl colorControlScrewGlide;
        private System.Windows.Forms.Label label14;
        public Crystallography.Controls.ColorControl colorControlString;
        public Crystallography.Controls.ColorControl colorControlFoot;
        public Crystallography.Controls.ColorControl colorControlBackGround;
        public Crystallography.Controls.ColorControl colorControlDefectLine;
        public Crystallography.Controls.ColorControl colorControlExcessLine;
        public Crystallography.Controls.ColorControl colorControlDebyeRing;
        public Crystallography.Controls.NumericBox numericBoxThickness;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSpotColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageScale;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButtonScale;
        private System.Windows.Forms.CheckBox checkBoxScaleLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar trackBarScaleLineWidth;
        private System.Windows.Forms.Label label16;
        private Crystallography.Controls.ColorControl colorControlScale2Theta;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButtonScaleDivisionFine;
        private System.Windows.Forms.RadioButton radioButtonScaleDivisionMedium;
        private System.Windows.Forms.RadioButton radioButtonScaleDivisionCoarse;
        private System.Windows.Forms.Timer timerBlinkScale;
        private Crystallography.Controls.ColorControl colorControlScaleAzimuth;
        private System.Windows.Forms.Label labelMousePositionReal;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox checkBoxMousePositionDetailes;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelMousePosition;
        private System.Windows.Forms.ToolStripMenuItem presetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem electron200KVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem electron120KeVToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem xray30KeVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xray20KeVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xrayMoKαToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xrayCuKαToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem electron300KVToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButtonCenterToDirect;
        private System.Windows.Forms.RadioButton radioButtonCenterToDetector;
        private System.Windows.Forms.RadioButton radioButtonCenterToFoot;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelDummy;
        private Crystallography.Controls.NumericBox numericBoxKikuchiLineThreshold;
        private System.Windows.Forms.ToolStripMenuItem copyCBEDPatternToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyCBEDasImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asPixelByPixelImagePNGFormatToolStripMenuItem;
    }
}