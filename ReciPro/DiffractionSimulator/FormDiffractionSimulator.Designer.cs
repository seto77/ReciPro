namespace ReciPro
{
    partial class FormDiffractionSimulator
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
            //if (context != null)
            //     context.Dispose();
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        // (260323Ch) renamed numeric container controls:
        // groupBox1 -> groupBoxDetectorGeometry
        // groupBox2 -> groupBoxMisc
        // groupBox3 -> groupBoxColor
        // groupBox4 -> groupBoxStringSize
        // groupBox5 -> groupBoxDeveloperCode
        // groupBox6 -> groupBoxMonitor
        // groupBox7 -> groupBoxOptions
        // groupBox8 -> groupBoxViewDirection
        // flowLayoutPanel1 -> flowLayoutPanelScaleDivision
        // panel4 -> panelViewStep
        // panel3 -> panelReciprocalSpace
        // flowLayoutPanel13 -> flowLayoutPanelImageOrientation
        // flowLayoutPanel12 -> flowLayoutPanelResolutionUnit
        // panel5 -> panelDetectorAndMisc
        // panel2 -> panelSimulationOptions
        // flowLayoutPanel4 -> flowLayoutPanelSpotShape
        // flowLayoutPanel7 -> flowLayoutPanelSpotOpacity
        // flowLayoutPanel8 -> flowLayoutPanelPointSpreadIntensity
        // flowLayoutPanel9 -> flowLayoutPanelScaleColor
        // flowLayoutPanel3 -> flowLayoutPanelIntensity
        // flowLayoutPanel5 -> flowLayoutPanelBeamMode
        // flowLayoutPanel10 -> flowLayoutPanelBeamType
        // flowLayoutPanel11 -> flowLayoutPanelWaveLength
        // (260520Cl) typo fix: numericBoxKikuchiThreadSholdOfStructureFactor -> numericBoxKikuchiThresholdOfStructureFactor (旧 typo "ThreadShold")
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDiffractionSimulator));
            toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            toolStrip3 = new System.Windows.Forms.ToolStrip();
            toolStripButtonDiffractionSpots = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonKikuchiLines = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonDebyeRing = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonScale = new System.Windows.Forms.ToolStripButton();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStripButtonIndexLabels = new System.Windows.Forms.ToolStripButton();
            toolStripButtonDspacing = new System.Windows.Forms.ToolStripButton();
            toolStripButtonDspacingInv = new System.Windows.Forms.ToolStripButton();
            toolStripButtonDistance = new System.Windows.Forms.ToolStripButton();
            toolStripButtonExcitationError = new System.Windows.Forms.ToolStripButton();
            toolStripButtonFg = new System.Windows.Forms.ToolStripButton();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabelTimeForSearchingG = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelTimeForDrawing = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelTimeForBethe = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            panelMain = new System.Windows.Forms.Panel();
            tabControl = new System.Windows.Forms.TabControl();
            tabPageGeneral = new System.Windows.Forms.TabPage();
            groupBoxStringSize = new System.Windows.Forms.GroupBox();
            trackBarStrSize = new System.Windows.Forms.TrackBar();
            groupBoxColor = new System.Windows.Forms.GroupBox();
            colorControlString = new ColorControl();
            label14 = new System.Windows.Forms.Label();
            colorControlOrigin = new ColorControl();
            colorControlFoot = new ColorControl();
            checkBoxShowFootPosition = new System.Windows.Forms.CheckBox();
            checkBoxShowDirectPosition = new System.Windows.Forms.CheckBox();
            colorControlBackGround = new ColorControl();
            tabPageKikuchi = new System.Windows.Forms.TabPage();
            radioButtonKikuchiThresholdOfStructureFactor = new System.Windows.Forms.RadioButton();
            checkBoxKikuchiLine_Kinematical = new System.Windows.Forms.CheckBox();
            radioButtonKikuchiThresholdOfLength = new System.Windows.Forms.RadioButton();
            numericBoxKikuchiThresholdOfStructureFactor = new NumericBox();
            numericBoxKikuchiThresholdOfLength = new NumericBox();
            colorControlExcessLine = new ColorControl();
            trackBarLineWidth = new System.Windows.Forms.TrackBar();
            label11 = new System.Windows.Forms.Label();
            tabPageDebye = new System.Windows.Forms.TabPage();
            colorControlDebyeRing = new ColorControl();
            checkBoxDebyeRingLabel = new System.Windows.Forms.CheckBox();
            checkBoxDebyeRingIgnoreIntensity = new System.Windows.Forms.CheckBox();
            label6 = new System.Windows.Forms.Label();
            trackBarDebyeRingWidth = new System.Windows.Forms.TrackBar();
            tabPageScale = new System.Windows.Forms.TabPage();
            checkBoxScaleLabel = new System.Windows.Forms.CheckBox();
            label12 = new System.Windows.Forms.Label();
            trackBarScaleLineWidth = new System.Windows.Forms.TrackBar();
            label16 = new System.Windows.Forms.Label();
            flowLayoutPanelScaleDivision = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonScaleDivisionFine = new System.Windows.Forms.RadioButton();
            radioButtonScaleDivisionMedium = new System.Windows.Forms.RadioButton();
            radioButtonScaleDivisionCoarse = new System.Windows.Forms.RadioButton();
            colorControlScaleAzimuth = new ColorControl();
            colorControlScale2Theta = new ColorControl();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            graphicsBox = new GraphicsBox(components);
            groupBoxOptions = new System.Windows.Forms.GroupBox();
            colorControl3D_SpotsNear = new ColorControl();
            numericBox3D_SpotRadius = new NumericBox();
            trackBar3D_Transparency = new System.Windows.Forms.TrackBar();
            checkBox3D_ShowIndices = new System.Windows.Forms.CheckBox();
            colorControl3D_SpotsFar = new ColorControl();
            checkBox3D_MakeSpotsTransparent = new System.Windows.Forms.CheckBox();
            colorControl3D_Background = new ColorControl();
            numericBoxReciprocalThreshold = new NumericBox();
            groupBoxViewDirection = new System.Windows.Forms.GroupBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            buttonAntiClock = new System.Windows.Forms.Button();
            buttonClock = new System.Windows.Forms.Button();
            buttonTopLeft = new System.Windows.Forms.Button();
            buttonLeft = new System.Windows.Forms.Button();
            buttonBottomLeft = new System.Windows.Forms.Button();
            buttonBottom = new System.Windows.Forms.Button();
            buttonBottomRight = new System.Windows.Forms.Button();
            buttonTop = new System.Windows.Forms.Button();
            buttonTopRight = new System.Windows.Forms.Button();
            buttonRight = new System.Windows.Forms.Button();
            panelViewStep = new System.Windows.Forms.Panel();
            numericBoxStep = new NumericBox();
            buttonResetAngle = new System.Windows.Forms.Button();
            label21 = new System.Windows.Forms.Label();
            colorControl3D_EwaldSphere = new ColorControl();
            colorControl3D_Origin = new ColorControl();
            colorControl3D_rightDirection = new ColorControl();
            colorControl3D_topDirection = new ColorControl();
            colorControl3D_beamDirection = new ColorControl();
            colorControl3D_lText = new ColorControl();
            checkBox3D_DirectionGuide = new System.Windows.Forms.CheckBox();
            checkBox3D_EwaldSphere = new System.Windows.Forms.CheckBox();
            panelReciprocalSpace = new System.Windows.Forms.Panel();
            checkBoxReciprocalSpace = new System.Windows.Forms.CheckBox();
            panelMousePosition = new System.Windows.Forms.Panel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            label24 = new System.Windows.Forms.Label();
            labelD = new System.Windows.Forms.Label();
            labelDinv = new System.Windows.Forms.Label();
            labelTwoThetaDeg = new System.Windows.Forms.Label();
            labelTwoThetaRad = new System.Windows.Forms.Label();
            tableLayoutPanelMousePotionDetailed = new System.Windows.Forms.TableLayoutPanel();
            labelMousePositionDetector = new System.Windows.Forms.Label();
            labelMousePositionReal = new System.Windows.Forms.Label();
            labelMousePositionReciprocal = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            checkBoxMousePositionDetailes = new System.Windows.Forms.CheckBox();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxMonitor = new System.Windows.Forms.GroupBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxFlipHorizontally = new System.Windows.Forms.CheckBox();
            checkBoxFlipVertically = new System.Windows.Forms.CheckBox();
            checkBoxNegativeImage = new System.Windows.Forms.CheckBox();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            buttonResetCenter = new System.Windows.Forms.Button();
            comboBoxCenter = new System.Windows.Forms.ComboBox();
            checkBoxFixCenter = new System.Windows.Forms.CheckBox();
            flowLayoutPanelImageOrientation = new System.Windows.Forms.FlowLayoutPanel();
            sizeControl1 = new SizeControl();
            flowLayoutPanelResolutionUnit = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxResolution = new NumericBox();
            radioButtonResoUnitMilliMeter = new System.Windows.Forms.RadioButton();
            radioButtonResoUnitNanometerInv = new System.Windows.Forms.RadioButton();
            panelDetectorAndMisc = new System.Windows.Forms.Panel();
            groupBoxMisc = new System.Windows.Forms.GroupBox();
            trackBarRotationSpeed = new System.Windows.Forms.TrackBar();
            buttonHolderSimulation = new System.Windows.Forms.Button();
            label23 = new System.Windows.Forms.Label();
            groupBoxDetectorGeometry = new System.Windows.Forms.GroupBox();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxCameraLength2 = new NumericBox();
            buttonDetailedGeometry = new System.Windows.Forms.Button();
            groupBoxDeveloperCode = new System.Windows.Forms.GroupBox();
            numericBoxDev = new NumericBox();
            button1 = new System.Windows.Forms.Button();
            numericBoxAcc = new NumericBox();
            button2 = new System.Windows.Forms.Button();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAsMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveDetectorAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveDetectorAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveDetectorAsMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveCBEDPatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveCBEDasPngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveCBEDasTiffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            asPixelByPixelImagePNGFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            asCollectiveImageTiffFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyImageToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyAsMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyDetectorAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyDetectorAsImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyDetectorAsMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyCBEDPatternToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyCBEDasImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemSaveMovieDiffractionPattern = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemSaveMovieReciprocalSpace = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            dynamicCompressionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            presetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            electron300KVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            electron200KVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            electron120KeVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            xray30KeVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            xray20KeVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            xrayMoKαToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            xrayCuKαToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            basicConceptOfBethesMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            waveLengthControl = new WaveLengthControl();
            radioButtonIntensityDynamical = new System.Windows.Forms.RadioButton();
            checkBoxUseCrystalColor = new System.Windows.Forms.CheckBox();
            checkBoxAnomalousDispersion = new System.Windows.Forms.CheckBox();
            checkBoxExtinctionAll = new System.Windows.Forms.CheckBox();
            checkBoxExtinctionLattice = new System.Windows.Forms.CheckBox();
            groupBoxSpotProperty = new System.Windows.Forms.GroupBox();
            panelSimulationOptions = new System.Windows.Forms.Panel();
            flowLayoutPanelPED = new System.Windows.Forms.FlowLayoutPanel();
            label5 = new System.Windows.Forms.Label();
            numericBoxPED_Semiangle = new NumericBox();
            numericBoxPED_Step = new NumericBox();
            flowLayoutPanelBethe = new System.Windows.Forms.FlowLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            numericBoxNumOfBlochWave = new NumericBox();
            numericBoxThickness = new NumericBox();
            flowLayoutPanelAppearance = new System.Windows.Forms.FlowLayoutPanel();
            label19 = new System.Windows.Forms.Label();
            flowLayoutPanelSpotShape = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonCircleArea = new System.Windows.Forms.RadioButton();
            radioButtonPointSpread = new System.Windows.Forms.RadioButton();
            flowLayoutPanelSpotOpacity = new System.Windows.Forms.FlowLayoutPanel();
            label8 = new System.Windows.Forms.Label();
            trackBarSpotOpacity = new System.Windows.Forms.TrackBar();
            numericBoxSpotRadius = new NumericBox();
            checkBoxDrawSameSize = new System.Windows.Forms.CheckBox();
            flowLayoutPanelGaussianOption = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelPointSpreadIntensity = new System.Windows.Forms.FlowLayoutPanel();
            label10 = new System.Windows.Forms.Label();
            trackBarIntensityForPointSpread = new System.Windows.Forms.TrackBar();
            flowLayoutPanelScaleColor = new System.Windows.Forms.FlowLayoutPanel();
            label25 = new System.Windows.Forms.Label();
            comboBoxScaleColorScale = new System.Windows.Forms.ComboBox();
            checkBoxLogScale = new System.Windows.Forms.CheckBox();
            flowLayoutPanelSpotColor = new System.Windows.Forms.FlowLayoutPanel();
            label2 = new System.Windows.Forms.Label();
            colorControlNoCondition = new ColorControl();
            colorControlScrewGlide = new ColorControl();
            colorControlForbiddenLattice = new ColorControl();
            flowLayoutPanelIntensity = new System.Windows.Forms.FlowLayoutPanel();
            label7 = new System.Windows.Forms.Label();
            radioButtonIntensityExcitation = new System.Windows.Forms.RadioButton();
            flowLayoutPanelExtinctionOption = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonIntensityKinematical = new System.Windows.Forms.RadioButton();
            buttonDetailsOfSpots = new System.Windows.Forms.Button();
            flowLayoutPanelBeamMode = new System.Windows.Forms.FlowLayoutPanel();
            label13 = new System.Windows.Forms.Label();
            radioButtonBeamParallel = new System.Windows.Forms.RadioButton();
            radioButtonBeamPrecessionElectron = new System.Windows.Forms.RadioButton();
            radioButtonBeamConvergence = new System.Windows.Forms.RadioButton();
            radioButtonBeamBackLaue = new System.Windows.Forms.RadioButton();
            radioButtonBeamPrecessionXray = new System.Windows.Forms.RadioButton();
            flowLayoutPanelWaveLength = new System.Windows.Forms.FlowLayoutPanel();
            label3 = new System.Windows.Forms.Label();
            toolTip = new System.Windows.Forms.ToolTip(components);
            printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            panel1 = new System.Windows.Forms.Panel();
            pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            printDialog1 = new System.Windows.Forms.PrintDialog();
            timerBlinkSpot = new System.Windows.Forms.Timer(components);
            timerBlinkKikuchiLine = new System.Windows.Forms.Timer(components);
            timerBlinkDebyeRing = new System.Windows.Forms.Timer(components);
            timerBlinkScale = new System.Windows.Forms.Timer(components);
            toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            toolStrip3.SuspendLayout();
            toolStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            panelMain.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageGeneral.SuspendLayout();
            groupBoxStringSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            groupBoxColor.SuspendLayout();
            tabPageKikuchi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).BeginInit();
            tabPageDebye.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarDebyeRingWidth).BeginInit();
            tabPageScale.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarScaleLineWidth).BeginInit();
            flowLayoutPanelScaleDivision.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            groupBoxOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar3D_Transparency).BeginInit();
            groupBoxViewDirection.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panelViewStep.SuspendLayout();
            panelReciprocalSpace.SuspendLayout();
            panelMousePosition.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanelMousePotionDetailed.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            groupBoxMonitor.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanelResolutionUnit.SuspendLayout();
            panelDetectorAndMisc.SuspendLayout();
            groupBoxMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarRotationSpeed).BeginInit();
            groupBoxDetectorGeometry.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            groupBoxDeveloperCode.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupBoxSpotProperty.SuspendLayout();
            panelSimulationOptions.SuspendLayout();
            flowLayoutPanelPED.SuspendLayout();
            flowLayoutPanelBethe.SuspendLayout();
            flowLayoutPanelAppearance.SuspendLayout();
            flowLayoutPanelSpotShape.SuspendLayout();
            flowLayoutPanelSpotOpacity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarSpotOpacity).BeginInit();
            flowLayoutPanelGaussianOption.SuspendLayout();
            flowLayoutPanelPointSpreadIntensity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityForPointSpread).BeginInit();
            flowLayoutPanelScaleColor.SuspendLayout();
            flowLayoutPanelSpotColor.SuspendLayout();
            flowLayoutPanelIntensity.SuspendLayout();
            flowLayoutPanelExtinctionOption.SuspendLayout();
            flowLayoutPanelBeamMode.SuspendLayout();
            flowLayoutPanelWaveLength.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            toolStripContainer1.BottomToolStripPanel.Controls.Add(toolStrip3);
            toolStripContainer1.BottomToolStripPanel.Controls.Add(toolStrip1);
            toolStripContainer1.BottomToolStripPanel.Controls.Add(statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.Controls.Add(panelMain);
            toolStripContainer1.ContentPanel.Controls.Add(panelMousePosition);
            toolStripContainer1.ContentPanel.Controls.Add(flowLayoutPanel6);
            resources.ApplyResources(toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            resources.ApplyResources(toolStripContainer1, "toolStripContainer1");
            toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            toolStripContainer1.TopToolStripPanel.Controls.Add(menuStrip1);
            // 
            // toolStrip3
            // 
            resources.ApplyResources(toolStrip3, "toolStrip3");
            toolStrip3.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(toolStrip3, true);
            toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButtonDiffractionSpots, toolStripSeparator2, toolStripButtonKikuchiLines, toolStripSeparator3, toolStripButtonDebyeRing, toolStripSeparator6, toolStripButtonScale });
            toolStrip3.Name = "toolStrip3";
            toolStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStripButtonDiffractionSpots
            // 
            toolStripButtonDiffractionSpots.BackColor = System.Drawing.SystemColors.Control;
            toolStripButtonDiffractionSpots.Checked = true;
            toolStripButtonDiffractionSpots.CheckOnClick = true;
            toolStripButtonDiffractionSpots.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripButtonDiffractionSpots.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonDiffractionSpots.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            resources.ApplyResources(toolStripButtonDiffractionSpots, "toolStripButtonDiffractionSpots");
            toolStripButtonDiffractionSpots.Name = "toolStripButtonDiffractionSpots";
            toolStripButtonDiffractionSpots.CheckedChanged += ToolStripButtonDiffractionSpots_CheckedChanged;
            toolStripButtonDiffractionSpots.MouseDown += toolStripButtonDiffractionSpots_MouseDown;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripButtonKikuchiLines
            // 
            toolStripButtonKikuchiLines.CheckOnClick = true;
            toolStripButtonKikuchiLines.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonKikuchiLines.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            resources.ApplyResources(toolStripButtonKikuchiLines, "toolStripButtonKikuchiLines");
            toolStripButtonKikuchiLines.Name = "toolStripButtonKikuchiLines";
            toolStripButtonKikuchiLines.CheckedChanged += ToolStripButtonDiffractionSpots_CheckedChanged;
            toolStripButtonKikuchiLines.MouseDown += toolStripButtonDiffractionSpots_MouseDown;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripButtonDebyeRing
            // 
            toolStripButtonDebyeRing.BackColor = System.Drawing.SystemColors.Control;
            toolStripButtonDebyeRing.CheckOnClick = true;
            toolStripButtonDebyeRing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonDebyeRing.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            resources.ApplyResources(toolStripButtonDebyeRing, "toolStripButtonDebyeRing");
            toolStripButtonDebyeRing.Name = "toolStripButtonDebyeRing";
            toolStripButtonDebyeRing.CheckedChanged += ToolStripButtonDiffractionSpots_CheckedChanged;
            toolStripButtonDebyeRing.MouseDown += toolStripButtonDiffractionSpots_MouseDown;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
            // 
            // toolStripButtonScale
            // 
            toolStripButtonScale.BackColor = System.Drawing.SystemColors.Control;
            toolStripButtonScale.CheckOnClick = true;
            toolStripButtonScale.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonScale.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            resources.ApplyResources(toolStripButtonScale, "toolStripButtonScale");
            toolStripButtonScale.Name = "toolStripButtonScale";
            toolStripButtonScale.CheckedChanged += ToolStripButtonDiffractionSpots_CheckedChanged;
            toolStripButtonScale.MouseDown += toolStripButtonDiffractionSpots_MouseDown;
            // 
            // toolStrip1
            // 
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButtonIndexLabels, toolStripButtonDspacing, toolStripButtonDspacingInv, toolStripButtonDistance, toolStripButtonExcitationError, toolStripButtonFg });
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // toolStripButtonIndexLabels
            // 
            toolStripButtonIndexLabels.Checked = true;
            toolStripButtonIndexLabels.CheckOnClick = true;
            toolStripButtonIndexLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripButtonIndexLabels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonIndexLabels.ForeColor = System.Drawing.Color.Salmon;
            resources.ApplyResources(toolStripButtonIndexLabels, "toolStripButtonIndexLabels");
            toolStripButtonIndexLabels.Name = "toolStripButtonIndexLabels";
            toolStripButtonIndexLabels.CheckedChanged += ToolStripButtonDiffractionSpots_CheckedChanged;
            // 
            // toolStripButtonDspacing
            // 
            toolStripButtonDspacing.CheckOnClick = true;
            toolStripButtonDspacing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonDspacing.ForeColor = System.Drawing.Color.Salmon;
            resources.ApplyResources(toolStripButtonDspacing, "toolStripButtonDspacing");
            toolStripButtonDspacing.Name = "toolStripButtonDspacing";
            toolStripButtonDspacing.CheckedChanged += ToolStripButtonDiffractionSpots_CheckedChanged;
            // 
            // toolStripButtonDspacingInv
            // 
            toolStripButtonDspacingInv.CheckOnClick = true;
            toolStripButtonDspacingInv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonDspacingInv.ForeColor = System.Drawing.Color.Salmon;
            resources.ApplyResources(toolStripButtonDspacingInv, "toolStripButtonDspacingInv");
            toolStripButtonDspacingInv.Name = "toolStripButtonDspacingInv";
            toolStripButtonDspacingInv.CheckedChanged += ToolStripButtonDiffractionSpots_CheckedChanged;
            // 
            // toolStripButtonDistance
            // 
            toolStripButtonDistance.CheckOnClick = true;
            toolStripButtonDistance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonDistance.ForeColor = System.Drawing.Color.Salmon;
            resources.ApplyResources(toolStripButtonDistance, "toolStripButtonDistance");
            toolStripButtonDistance.Name = "toolStripButtonDistance";
            toolStripButtonDistance.CheckedChanged += ToolStripButtonDiffractionSpots_CheckedChanged;
            // 
            // toolStripButtonExcitationError
            // 
            toolStripButtonExcitationError.CheckOnClick = true;
            toolStripButtonExcitationError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonExcitationError.ForeColor = System.Drawing.Color.Salmon;
            resources.ApplyResources(toolStripButtonExcitationError, "toolStripButtonExcitationError");
            toolStripButtonExcitationError.Name = "toolStripButtonExcitationError";
            toolStripButtonExcitationError.CheckedChanged += ToolStripButtonDiffractionSpots_CheckedChanged;
            // 
            // toolStripButtonFg
            // 
            toolStripButtonFg.CheckOnClick = true;
            toolStripButtonFg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonFg.ForeColor = System.Drawing.Color.Salmon;
            resources.ApplyResources(toolStripButtonFg, "toolStripButtonFg");
            toolStripButtonFg.Name = "toolStripButtonFg";
            toolStripButtonFg.CheckedChanged += ToolStripButtonDiffractionSpots_CheckedChanged;
            // 
            // statusStrip1
            // 
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabelTimeForSearchingG, toolStripStatusLabelTimeForDrawing, toolStripStatusLabelTimeForBethe, toolStripStatusLabel3 });
            statusStrip1.Name = "statusStrip1";
            statusStrip1.MouseDown += statusStrip1_MouseDown;
            // 
            // toolStripStatusLabelTimeForSearchingG
            // 
            toolStripStatusLabelTimeForSearchingG.Name = "toolStripStatusLabelTimeForSearchingG";
            resources.ApplyResources(toolStripStatusLabelTimeForSearchingG, "toolStripStatusLabelTimeForSearchingG");
            // 
            // toolStripStatusLabelTimeForDrawing
            // 
            toolStripStatusLabelTimeForDrawing.Name = "toolStripStatusLabelTimeForDrawing";
            resources.ApplyResources(toolStripStatusLabelTimeForDrawing, "toolStripStatusLabelTimeForDrawing");
            // 
            // toolStripStatusLabelTimeForBethe
            // 
            toolStripStatusLabelTimeForBethe.Name = "toolStripStatusLabelTimeForBethe";
            resources.ApplyResources(toolStripStatusLabelTimeForBethe, "toolStripStatusLabelTimeForBethe");
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            resources.ApplyResources(toolStripStatusLabel3, "toolStripStatusLabel3");
            // 
            // panelMain
            // 
            resources.ApplyResources(panelMain, "panelMain");
            panelMain.Controls.Add(tabControl);
            panelMain.Controls.Add(splitContainer1);
            panelMain.Controls.Add(panelReciprocalSpace);
            panelMain.Name = "panelMain";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageGeneral);
            tabControl.Controls.Add(tabPageKikuchi);
            tabControl.Controls.Add(tabPageDebye);
            tabControl.Controls.Add(tabPageScale);
            tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            resources.ApplyResources(tabControl, "tabControl");
            tabControl.HotTrack = true;
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.DrawItem += tabControl_DrawItem;
            tabControl.Selecting += tabControl_Selecting;
            tabControl.Click += tabControl_Click;
            // 
            // tabPageGeneral
            // 
            tabPageGeneral.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageGeneral, true);
            tabPageGeneral.Controls.Add(groupBoxStringSize);
            tabPageGeneral.Controls.Add(groupBoxColor);
            resources.ApplyResources(tabPageGeneral, "tabPageGeneral");
            tabPageGeneral.Name = "tabPageGeneral";
            // 
            // groupBoxStringSize
            // 
            groupBoxStringSize.Controls.Add(trackBarStrSize);
            resources.ApplyResources(groupBoxStringSize, "groupBoxStringSize");
            groupBoxStringSize.Name = "groupBoxStringSize";
            groupBoxStringSize.TabStop = false;
            toolTip.SetToolTip(groupBoxStringSize, resources.GetString("groupBoxStringSize.ToolTip"));
            // 
            // trackBarStrSize
            // 
            resources.ApplyResources(trackBarStrSize, "trackBarStrSize");
            trackBarStrSize.LargeChange = 50;
            trackBarStrSize.Maximum = 200;
            trackBarStrSize.Minimum = 1;
            trackBarStrSize.Name = "trackBarStrSize";
            trackBarStrSize.SmallChange = 10;
            trackBarStrSize.TickFrequency = 500;
            trackBarStrSize.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarStrSize, resources.GetString("trackBarStrSize.ToolTip"));
            trackBarStrSize.Value = 80;
            trackBarStrSize.ValueChanged += Draw;
            // 
            // groupBoxColor
            // 
            groupBoxColor.Controls.Add(colorControlString);
            groupBoxColor.Controls.Add(label14);
            groupBoxColor.Controls.Add(colorControlOrigin);
            groupBoxColor.Controls.Add(colorControlFoot);
            groupBoxColor.Controls.Add(checkBoxShowFootPosition);
            groupBoxColor.Controls.Add(checkBoxShowDirectPosition);
            groupBoxColor.Controls.Add(colorControlBackGround);
            resources.ApplyResources(groupBoxColor, "groupBoxColor");
            groupBoxColor.Name = "groupBoxColor";
            groupBoxColor.TabStop = false;
            // 
            // colorControlString
            // 
            resources.ApplyResources(colorControlString, "colorControlString");
            colorControlString.BackColor = System.Drawing.SystemColors.Control;
            colorControlString.BoxSize = new System.Drawing.Size(20, 20);
            colorControlString.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlString.Name = "colorControlString";
            toolTip.SetToolTip(colorControlString, resources.GetString("colorControlString.ToolTip1"));
            colorControlString.ColorChanged += Draw;
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            label14.Name = "label14";
            toolTip.SetToolTip(label14, resources.GetString("label14.ToolTip"));
            // 
            // colorControlOrigin
            // 
            resources.ApplyResources(colorControlOrigin, "colorControlOrigin");
            colorControlOrigin.BackColor = System.Drawing.Color.Transparent;
            colorControlOrigin.BoxSize = new System.Drawing.Size(20, 20);
            colorControlOrigin.Color = System.Drawing.Color.FromArgb(255, 0, 0);
            colorControlOrigin.Name = "colorControlOrigin";
            toolTip.SetToolTip(colorControlOrigin, resources.GetString("colorControlOrigin.ToolTip1"));
            colorControlOrigin.ColorChanged += Draw;
            // 
            // colorControlFoot
            // 
            resources.ApplyResources(colorControlFoot, "colorControlFoot");
            colorControlFoot.BackColor = System.Drawing.SystemColors.Control;
            colorControlFoot.BoxSize = new System.Drawing.Size(20, 20);
            colorControlFoot.Color = System.Drawing.Color.FromArgb(0, 192, 0);
            colorControlFoot.Name = "colorControlFoot";
            colorControlFoot.ColorChanged += Draw;
            // 
            // checkBoxShowFootPosition
            // 
            resources.ApplyResources(checkBoxShowFootPosition, "checkBoxShowFootPosition");
            checkBoxShowFootPosition.Checked = true;
            checkBoxShowFootPosition.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowFootPosition.Name = "checkBoxShowFootPosition";
            toolTip.SetToolTip(checkBoxShowFootPosition, resources.GetString("checkBoxShowFootPosition.ToolTip"));
            checkBoxShowFootPosition.UseVisualStyleBackColor = true;
            checkBoxShowFootPosition.CheckedChanged += Draw;
            // 
            // checkBoxShowDirectPosition
            // 
            resources.ApplyResources(checkBoxShowDirectPosition, "checkBoxShowDirectPosition");
            checkBoxShowDirectPosition.Checked = true;
            checkBoxShowDirectPosition.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowDirectPosition.Name = "checkBoxShowDirectPosition";
            toolTip.SetToolTip(checkBoxShowDirectPosition, resources.GetString("checkBoxShowDirectPosition.ToolTip"));
            checkBoxShowDirectPosition.UseVisualStyleBackColor = true;
            checkBoxShowDirectPosition.CheckedChanged += Draw;
            // 
            // colorControlBackGround
            // 
            resources.ApplyResources(colorControlBackGround, "colorControlBackGround");
            colorControlBackGround.BackColor = System.Drawing.SystemColors.Control;
            colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(32, 32, 32);
            colorControlBackGround.Name = "colorControlBackGround";
            toolTip.SetToolTip(colorControlBackGround, resources.GetString("colorControlBackGround.ToolTip1"));
            colorControlBackGround.ColorChanged += Draw;
            // 
            // tabPageKikuchi
            // 
            tabPageKikuchi.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageKikuchi, true);
            tabPageKikuchi.Controls.Add(radioButtonKikuchiThresholdOfStructureFactor);
            tabPageKikuchi.Controls.Add(checkBoxKikuchiLine_Kinematical);
            tabPageKikuchi.Controls.Add(radioButtonKikuchiThresholdOfLength);
            tabPageKikuchi.Controls.Add(numericBoxKikuchiThresholdOfStructureFactor);
            tabPageKikuchi.Controls.Add(numericBoxKikuchiThresholdOfLength);
            tabPageKikuchi.Controls.Add(colorControlExcessLine);
            tabPageKikuchi.Controls.Add(trackBarLineWidth);
            tabPageKikuchi.Controls.Add(label11);
            resources.ApplyResources(tabPageKikuchi, "tabPageKikuchi");
            tabPageKikuchi.Name = "tabPageKikuchi";
            // 
            // radioButtonKikuchiThresholdOfStructureFactor
            // 
            resources.ApplyResources(radioButtonKikuchiThresholdOfStructureFactor, "radioButtonKikuchiThresholdOfStructureFactor");
            radioButtonKikuchiThresholdOfStructureFactor.Checked = true;
            radioButtonKikuchiThresholdOfStructureFactor.Name = "radioButtonKikuchiThresholdOfStructureFactor";
            radioButtonKikuchiThresholdOfStructureFactor.TabStop = true;
            toolTip.SetToolTip(radioButtonKikuchiThresholdOfStructureFactor, resources.GetString("radioButtonKikuchiThresholdOfStructureFactor.ToolTip"));
            radioButtonKikuchiThresholdOfStructureFactor.UseVisualStyleBackColor = true;
            radioButtonKikuchiThresholdOfStructureFactor.CheckedChanged += radioButtonKikuchiThresholdOfLength_CheckedChanged;
            // 
            // checkBoxKikuchiLine_Kinematical
            // 
            resources.ApplyResources(checkBoxKikuchiLine_Kinematical, "checkBoxKikuchiLine_Kinematical");
            checkBoxKikuchiLine_Kinematical.Checked = true;
            checkBoxKikuchiLine_Kinematical.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxKikuchiLine_Kinematical.Name = "checkBoxKikuchiLine_Kinematical";
            toolTip.SetToolTip(checkBoxKikuchiLine_Kinematical, resources.GetString("checkBoxKikuchiLine_Kinematical.ToolTip"));
            checkBoxKikuchiLine_Kinematical.UseVisualStyleBackColor = true;
            checkBoxKikuchiLine_Kinematical.CheckedChanged += checkBoxKikuchiLine_Kinematical_CheckedChanged;
            // 
            // radioButtonKikuchiThresholdOfLength
            // 
            resources.ApplyResources(radioButtonKikuchiThresholdOfLength, "radioButtonKikuchiThresholdOfLength");
            radioButtonKikuchiThresholdOfLength.Name = "radioButtonKikuchiThresholdOfLength";
            toolTip.SetToolTip(radioButtonKikuchiThresholdOfLength, resources.GetString("radioButtonKikuchiThresholdOfLength.ToolTip"));
            radioButtonKikuchiThresholdOfLength.UseVisualStyleBackColor = true;
            radioButtonKikuchiThresholdOfLength.CheckedChanged += radioButtonKikuchiThresholdOfLength_CheckedChanged;
            // 
            // numericBoxKikuchiThresholdOfStructureFactor
            // 
            numericBoxKikuchiThresholdOfStructureFactor.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxKikuchiThresholdOfStructureFactor, "numericBoxKikuchiThresholdOfStructureFactor");
            numericBoxKikuchiThresholdOfStructureFactor.Maximum = 1000D;
            numericBoxKikuchiThresholdOfStructureFactor.Minimum = 1D;
            numericBoxKikuchiThresholdOfStructureFactor.Name = "numericBoxKikuchiThresholdOfStructureFactor";
            numericBoxKikuchiThresholdOfStructureFactor.RadianValue = 1.7453292519943295D;
            numericBoxKikuchiThresholdOfStructureFactor.ShowUpDown = true;
            numericBoxKikuchiThresholdOfStructureFactor.SmartIncrement = true;
            numericBoxKikuchiThresholdOfStructureFactor.Value = 100D;
            numericBoxKikuchiThresholdOfStructureFactor.ValueFontSize = 9F;
            numericBoxKikuchiThresholdOfStructureFactor.ValueChanged += numericBoxKikuchiLineThreshold_ValueChanged;
            // 
            // numericBoxKikuchiThresholdOfLength
            // 
            numericBoxKikuchiThresholdOfLength.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxKikuchiThresholdOfLength, "numericBoxKikuchiThresholdOfLength");
            numericBoxKikuchiThresholdOfLength.Maximum = 100D;
            numericBoxKikuchiThresholdOfLength.Minimum = 0D;
            numericBoxKikuchiThresholdOfLength.Name = "numericBoxKikuchiThresholdOfLength";
            numericBoxKikuchiThresholdOfLength.RadianValue = 0.17453292519943295D;
            numericBoxKikuchiThresholdOfLength.ShowUpDown = true;
            numericBoxKikuchiThresholdOfLength.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxKikuchiThresholdOfLength, resources.GetString("numericBoxKikuchiThresholdOfLength.ToolTip"));
            numericBoxKikuchiThresholdOfLength.Value = 10D;
            numericBoxKikuchiThresholdOfLength.ValueFontSize = 9F;
            numericBoxKikuchiThresholdOfLength.ValueChanged += numericBoxKikuchiLineThreshold_ValueChanged;
            // 
            // colorControlExcessLine
            // 
            resources.ApplyResources(colorControlExcessLine, "colorControlExcessLine");
            colorControlExcessLine.BackColor = System.Drawing.SystemColors.Control;
            colorControlExcessLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControlExcessLine.Color = System.Drawing.Color.FromArgb(224, 224, 224);
            colorControlExcessLine.Name = "colorControlExcessLine";
            colorControlExcessLine.ColorChanged += Draw;
            // 
            // trackBarLineWidth
            // 
            resources.ApplyResources(trackBarLineWidth, "trackBarLineWidth");
            trackBarLineWidth.Maximum = 10000;
            trackBarLineWidth.Minimum = 1;
            trackBarLineWidth.Name = "trackBarLineWidth";
            trackBarLineWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarLineWidth, resources.GetString("trackBarLineWidth.ToolTip"));
            trackBarLineWidth.Value = 4000;
            trackBarLineWidth.ValueChanged += numericUpDownResolution_ValueChanged;
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip"));
            // 
            // tabPageDebye
            // 
            tabPageDebye.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageDebye, true);
            tabPageDebye.Controls.Add(colorControlDebyeRing);
            tabPageDebye.Controls.Add(checkBoxDebyeRingLabel);
            tabPageDebye.Controls.Add(checkBoxDebyeRingIgnoreIntensity);
            tabPageDebye.Controls.Add(label6);
            tabPageDebye.Controls.Add(trackBarDebyeRingWidth);
            resources.ApplyResources(tabPageDebye, "tabPageDebye");
            tabPageDebye.Name = "tabPageDebye";
            // 
            // colorControlDebyeRing
            // 
            resources.ApplyResources(colorControlDebyeRing, "colorControlDebyeRing");
            colorControlDebyeRing.BackColor = System.Drawing.SystemColors.Control;
            colorControlDebyeRing.BoxSize = new System.Drawing.Size(20, 20);
            colorControlDebyeRing.Color = System.Drawing.Color.FromArgb(255, 255, 0);
            colorControlDebyeRing.Name = "colorControlDebyeRing";
            toolTip.SetToolTip(colorControlDebyeRing, resources.GetString("colorControlDebyeRing.ToolTip1"));
            colorControlDebyeRing.ColorChanged += Draw;
            // 
            // checkBoxDebyeRingLabel
            // 
            resources.ApplyResources(checkBoxDebyeRingLabel, "checkBoxDebyeRingLabel");
            checkBoxDebyeRingLabel.Name = "checkBoxDebyeRingLabel";
            toolTip.SetToolTip(checkBoxDebyeRingLabel, resources.GetString("checkBoxDebyeRingLabel.ToolTip"));
            checkBoxDebyeRingLabel.UseVisualStyleBackColor = true;
            checkBoxDebyeRingLabel.CheckedChanged += Draw;
            // 
            // checkBoxDebyeRingIgnoreIntensity
            // 
            resources.ApplyResources(checkBoxDebyeRingIgnoreIntensity, "checkBoxDebyeRingIgnoreIntensity");
            checkBoxDebyeRingIgnoreIntensity.Name = "checkBoxDebyeRingIgnoreIntensity";
            toolTip.SetToolTip(checkBoxDebyeRingIgnoreIntensity, resources.GetString("checkBoxDebyeRingIgnoreIntensity.ToolTip"));
            checkBoxDebyeRingIgnoreIntensity.UseVisualStyleBackColor = true;
            checkBoxDebyeRingIgnoreIntensity.CheckedChanged += Draw;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // trackBarDebyeRingWidth
            // 
            resources.ApplyResources(trackBarDebyeRingWidth, "trackBarDebyeRingWidth");
            trackBarDebyeRingWidth.LargeChange = 1;
            trackBarDebyeRingWidth.Minimum = 1;
            trackBarDebyeRingWidth.Name = "trackBarDebyeRingWidth";
            trackBarDebyeRingWidth.TickFrequency = 500;
            trackBarDebyeRingWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarDebyeRingWidth, resources.GetString("trackBarDebyeRingWidth.ToolTip"));
            trackBarDebyeRingWidth.Value = 5;
            trackBarDebyeRingWidth.ValueChanged += Draw;
            // 
            // tabPageScale
            // 
            tabPageScale.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageScale, true);
            tabPageScale.Controls.Add(checkBoxScaleLabel);
            tabPageScale.Controls.Add(label12);
            tabPageScale.Controls.Add(trackBarScaleLineWidth);
            tabPageScale.Controls.Add(label16);
            tabPageScale.Controls.Add(flowLayoutPanelScaleDivision);
            tabPageScale.Controls.Add(colorControlScaleAzimuth);
            tabPageScale.Controls.Add(colorControlScale2Theta);
            resources.ApplyResources(tabPageScale, "tabPageScale");
            tabPageScale.Name = "tabPageScale";
            // 
            // checkBoxScaleLabel
            // 
            resources.ApplyResources(checkBoxScaleLabel, "checkBoxScaleLabel");
            checkBoxScaleLabel.Checked = true;
            checkBoxScaleLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxScaleLabel.Name = "checkBoxScaleLabel";
            toolTip.SetToolTip(checkBoxScaleLabel, resources.GetString("checkBoxScaleLabel.ToolTip"));
            checkBoxScaleLabel.UseVisualStyleBackColor = true;
            checkBoxScaleLabel.CheckedChanged += Draw;
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            label12.Name = "label12";
            toolTip.SetToolTip(label12, resources.GetString("label12.ToolTip"));
            // 
            // trackBarScaleLineWidth
            // 
            resources.ApplyResources(trackBarScaleLineWidth, "trackBarScaleLineWidth");
            trackBarScaleLineWidth.Minimum = 1;
            trackBarScaleLineWidth.Name = "trackBarScaleLineWidth";
            trackBarScaleLineWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarScaleLineWidth, resources.GetString("trackBarScaleLineWidth.ToolTip"));
            trackBarScaleLineWidth.Value = 3;
            trackBarScaleLineWidth.Scroll += Draw;
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            label16.Name = "label16";
            toolTip.SetToolTip(label16, resources.GetString("label16.ToolTip"));
            // 
            // flowLayoutPanelScaleDivision
            // 
            resources.ApplyResources(flowLayoutPanelScaleDivision, "flowLayoutPanelScaleDivision");
            flowLayoutPanelScaleDivision.Controls.Add(radioButtonScaleDivisionFine);
            flowLayoutPanelScaleDivision.Controls.Add(radioButtonScaleDivisionMedium);
            flowLayoutPanelScaleDivision.Controls.Add(radioButtonScaleDivisionCoarse);
            flowLayoutPanelScaleDivision.Name = "flowLayoutPanelScaleDivision";
            // 
            // radioButtonScaleDivisionFine
            // 
            resources.ApplyResources(radioButtonScaleDivisionFine, "radioButtonScaleDivisionFine");
            radioButtonScaleDivisionFine.Name = "radioButtonScaleDivisionFine";
            toolTip.SetToolTip(radioButtonScaleDivisionFine, resources.GetString("radioButtonScaleDivisionFine.ToolTip"));
            radioButtonScaleDivisionFine.UseVisualStyleBackColor = true;
            radioButtonScaleDivisionFine.CheckedChanged += Draw;
            // 
            // radioButtonScaleDivisionMedium
            // 
            resources.ApplyResources(radioButtonScaleDivisionMedium, "radioButtonScaleDivisionMedium");
            radioButtonScaleDivisionMedium.Checked = true;
            radioButtonScaleDivisionMedium.Name = "radioButtonScaleDivisionMedium";
            radioButtonScaleDivisionMedium.TabStop = true;
            toolTip.SetToolTip(radioButtonScaleDivisionMedium, resources.GetString("radioButtonScaleDivisionMedium.ToolTip"));
            radioButtonScaleDivisionMedium.UseVisualStyleBackColor = true;
            radioButtonScaleDivisionMedium.CheckedChanged += Draw;
            // 
            // radioButtonScaleDivisionCoarse
            // 
            resources.ApplyResources(radioButtonScaleDivisionCoarse, "radioButtonScaleDivisionCoarse");
            radioButtonScaleDivisionCoarse.Name = "radioButtonScaleDivisionCoarse";
            toolTip.SetToolTip(radioButtonScaleDivisionCoarse, resources.GetString("radioButtonScaleDivisionCoarse.ToolTip"));
            radioButtonScaleDivisionCoarse.UseVisualStyleBackColor = true;
            // 
            // colorControlScaleAzimuth
            // 
            resources.ApplyResources(colorControlScaleAzimuth, "colorControlScaleAzimuth");
            colorControlScaleAzimuth.BackColor = System.Drawing.SystemColors.Control;
            colorControlScaleAzimuth.BoxSize = new System.Drawing.Size(20, 20);
            colorControlScaleAzimuth.Color = System.Drawing.Color.FromArgb(119, 68, 70);
            colorControlScaleAzimuth.Name = "colorControlScaleAzimuth";
            toolTip.SetToolTip(colorControlScaleAzimuth, resources.GetString("colorControlScaleAzimuth.ToolTip1"));
            colorControlScaleAzimuth.ColorChanged += Draw;
            // 
            // colorControlScale2Theta
            // 
            resources.ApplyResources(colorControlScale2Theta, "colorControlScale2Theta");
            colorControlScale2Theta.BackColor = System.Drawing.SystemColors.Control;
            colorControlScale2Theta.BoxSize = new System.Drawing.Size(20, 20);
            colorControlScale2Theta.Color = System.Drawing.Color.FromArgb(68, 68, 120);
            colorControlScale2Theta.Name = "colorControlScale2Theta";
            toolTip.SetToolTip(colorControlScale2Theta, resources.GetString("colorControlScale2Theta.ToolTip1"));
            colorControlScale2Theta.ColorChanged += Draw;
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(graphicsBox);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBoxOptions);
            // 
            // graphicsBox
            // 
            graphicsBox.BackColor = System.Drawing.Color.Transparent;
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(graphicsBox, "graphicsBox");
            graphicsBox.Fonts = new System.Drawing.Font("Segoe UI", 11F);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.TabStop = false;
            toolTip.SetToolTip(graphicsBox, resources.GetString("graphicsBox.ToolTip"));
            graphicsBox.ClientSizeChanged += graphicsBox_ClientSizeChanged;
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            graphicsBox.MouseUp += graphicsBox_MouseUp;
            graphicsBox.Move += Draw;
            graphicsBox.Resize += graphicsBox_Resize;
            // 
            // groupBoxOptions
            // 
            groupBoxOptions.Controls.Add(colorControl3D_SpotsNear);
            groupBoxOptions.Controls.Add(numericBox3D_SpotRadius);
            groupBoxOptions.Controls.Add(trackBar3D_Transparency);
            groupBoxOptions.Controls.Add(checkBox3D_ShowIndices);
            groupBoxOptions.Controls.Add(colorControl3D_SpotsFar);
            groupBoxOptions.Controls.Add(checkBox3D_MakeSpotsTransparent);
            groupBoxOptions.Controls.Add(colorControl3D_Background);
            groupBoxOptions.Controls.Add(numericBoxReciprocalThreshold);
            groupBoxOptions.Controls.Add(groupBoxViewDirection);
            groupBoxOptions.Controls.Add(label21);
            groupBoxOptions.Controls.Add(colorControl3D_EwaldSphere);
            groupBoxOptions.Controls.Add(colorControl3D_Origin);
            groupBoxOptions.Controls.Add(colorControl3D_rightDirection);
            groupBoxOptions.Controls.Add(colorControl3D_topDirection);
            groupBoxOptions.Controls.Add(colorControl3D_beamDirection);
            groupBoxOptions.Controls.Add(colorControl3D_lText);
            groupBoxOptions.Controls.Add(checkBox3D_DirectionGuide);
            groupBoxOptions.Controls.Add(checkBox3D_EwaldSphere);
            resources.ApplyResources(groupBoxOptions, "groupBoxOptions");
            groupBoxOptions.Name = "groupBoxOptions";
            groupBoxOptions.TabStop = false;
            // 
            // colorControl3D_SpotsNear
            // 
            resources.ApplyResources(colorControl3D_SpotsNear, "colorControl3D_SpotsNear");
            colorControl3D_SpotsNear.BackColor = System.Drawing.SystemColors.Control;
            colorControl3D_SpotsNear.BoxSize = new System.Drawing.Size(20, 20);
            colorControl3D_SpotsNear.Color = System.Drawing.Color.FromArgb(255, 255, 0);
            colorControl3D_SpotsNear.Name = "colorControl3D_SpotsNear";
            //toolTip.SetToolTip(colorControl3D_SpotsNear, resources.GetString("colorControl3D_SpotsNear.ToolTip1")); // 260703Cl 変更前: EN resx 内の日本語文言 (.ToolTip1) を参照し英語 UI で日本語チップが出ていた
            toolTip.SetToolTip(colorControl3D_SpotsNear, resources.GetString("colorControl3D_SpotsNear.ToolTip")); // 260703Cl 正キーへ統一
            colorControl3D_SpotsNear.ColorChanged += colorControlReciprocalBackground_ColorChanged;
            // 
            // numericBox3D_SpotRadius
            // 
            numericBox3D_SpotRadius.BackColor = System.Drawing.SystemColors.Control;
            numericBox3D_SpotRadius.DecimalPlaces = 4;
            resources.ApplyResources(numericBox3D_SpotRadius, "numericBox3D_SpotRadius");
            numericBox3D_SpotRadius.Maximum = 1D;
            numericBox3D_SpotRadius.Minimum = 0.01D;
            numericBox3D_SpotRadius.Name = "numericBox3D_SpotRadius";
            numericBox3D_SpotRadius.RadianValue = 0.0034906585039886592D;
            numericBox3D_SpotRadius.ShowUpDown = true;
            numericBox3D_SpotRadius.SkipEventDuringInput = false;
            numericBox3D_SpotRadius.SmartIncrement = true;
            numericBox3D_SpotRadius.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBox3D_SpotRadius, resources.GetString("numericBox3D_SpotRadius.ToolTip"));
            numericBox3D_SpotRadius.UpDown_Increment = 0.01D;
            numericBox3D_SpotRadius.Value = 0.2D;
            numericBox3D_SpotRadius.ValueFontSize = 9F;
            numericBox3D_SpotRadius.ValueChanged += numericBox3D_SpotRadius_ValueChanged;
            // 
            // trackBar3D_Transparency
            // 
            resources.ApplyResources(trackBar3D_Transparency, "trackBar3D_Transparency");
            trackBar3D_Transparency.LargeChange = 50;
            trackBar3D_Transparency.Maximum = 100;
            trackBar3D_Transparency.Minimum = 1;
            trackBar3D_Transparency.Name = "trackBar3D_Transparency";
            trackBar3D_Transparency.SmallChange = 10;
            trackBar3D_Transparency.TickFrequency = 500;
            trackBar3D_Transparency.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBar3D_Transparency, resources.GetString("trackBar3D_Transparency.ToolTip"));
            trackBar3D_Transparency.Value = 25;
            trackBar3D_Transparency.ValueChanged += trackBar1_ValueChanged;
            // 
            // checkBox3D_ShowIndices
            // 
            resources.ApplyResources(checkBox3D_ShowIndices, "checkBox3D_ShowIndices");
            checkBox3D_ShowIndices.Checked = true;
            checkBox3D_ShowIndices.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3D_ShowIndices.Name = "checkBox3D_ShowIndices";
            toolTip.SetToolTip(checkBox3D_ShowIndices, resources.GetString("checkBox3D_ShowIndices.ToolTip"));
            checkBox3D_ShowIndices.UseVisualStyleBackColor = true;
            checkBox3D_ShowIndices.CheckedChanged += checkBox3D_ShowIndices_CheckedChanged;
            // 
            // colorControl3D_SpotsFar
            // 
            resources.ApplyResources(colorControl3D_SpotsFar, "colorControl3D_SpotsFar");
            colorControl3D_SpotsFar.BackColor = System.Drawing.SystemColors.Control;
            colorControl3D_SpotsFar.BoxSize = new System.Drawing.Size(20, 20);
            colorControl3D_SpotsFar.Color = System.Drawing.Color.FromArgb(192, 192, 192);
            colorControl3D_SpotsFar.Name = "colorControl3D_SpotsFar";
            colorControl3D_SpotsFar.ColorChanged += colorControlReciprocalBackground_ColorChanged;
            // 
            // checkBox3D_MakeSpotsTransparent
            // 
            resources.ApplyResources(checkBox3D_MakeSpotsTransparent, "checkBox3D_MakeSpotsTransparent");
            checkBox3D_MakeSpotsTransparent.Checked = true;
            checkBox3D_MakeSpotsTransparent.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3D_MakeSpotsTransparent.Name = "checkBox3D_MakeSpotsTransparent";
            toolTip.SetToolTip(checkBox3D_MakeSpotsTransparent, resources.GetString("checkBox3D_MakeSpotsTransparent.ToolTip"));
            checkBox3D_MakeSpotsTransparent.UseVisualStyleBackColor = true;
            checkBox3D_MakeSpotsTransparent.CheckedChanged += checkBoxShowEwaldSphere_CheckedChanged;
            // 
            // colorControl3D_Background
            // 
            resources.ApplyResources(colorControl3D_Background, "colorControl3D_Background");
            colorControl3D_Background.BackColor = System.Drawing.SystemColors.Control;
            colorControl3D_Background.BoxSize = new System.Drawing.Size(20, 20);
            colorControl3D_Background.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControl3D_Background.Name = "colorControl3D_Background";
            toolTip.SetToolTip(colorControl3D_Background, resources.GetString("colorControl3D_Background.ToolTip1"));
            colorControl3D_Background.ColorChanged += colorControlReciprocalBackground_ColorChanged;
            // 
            // numericBoxReciprocalThreshold
            // 
            numericBoxReciprocalThreshold.BackColor = System.Drawing.SystemColors.Control;
            numericBoxReciprocalThreshold.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxReciprocalThreshold, "numericBoxReciprocalThreshold");
            numericBoxReciprocalThreshold.Maximum = 100D;
            numericBoxReciprocalThreshold.Minimum = 0D;
            numericBoxReciprocalThreshold.Name = "numericBoxReciprocalThreshold";
            numericBoxReciprocalThreshold.RadianValue = 0.017453292519943295D;
            numericBoxReciprocalThreshold.ShowUpDown = true;
            numericBoxReciprocalThreshold.SmartIncrement = true;
            numericBoxReciprocalThreshold.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxReciprocalThreshold, resources.GetString("numericBoxReciprocalThreshold.ToolTip"));
            numericBoxReciprocalThreshold.Value = 1D;
            numericBoxReciprocalThreshold.ValueFontSize = 9F;
            numericBoxReciprocalThreshold.ValueChanged += numericBoxReciprocalThreshold_ValueChanged;
            // 
            // groupBoxViewDirection
            // 
            groupBoxViewDirection.Controls.Add(tableLayoutPanel1);
            groupBoxViewDirection.Controls.Add(panelViewStep);
            groupBoxViewDirection.Controls.Add(buttonResetAngle);
            resources.ApplyResources(groupBoxViewDirection, "groupBoxViewDirection");
            groupBoxViewDirection.Name = "groupBoxViewDirection";
            groupBoxViewDirection.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 1);
            tableLayoutPanel1.Controls.Add(buttonTopLeft, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonLeft, 0, 1);
            tableLayoutPanel1.Controls.Add(buttonBottomLeft, 0, 2);
            tableLayoutPanel1.Controls.Add(buttonBottom, 1, 2);
            tableLayoutPanel1.Controls.Add(buttonBottomRight, 2, 2);
            tableLayoutPanel1.Controls.Add(buttonTop, 1, 0);
            tableLayoutPanel1.Controls.Add(buttonTopRight, 2, 0);
            tableLayoutPanel1.Controls.Add(buttonRight, 2, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(tableLayoutPanel3, "tableLayoutPanel3");
            tableLayoutPanel3.Controls.Add(buttonAntiClock, 1, 0);
            tableLayoutPanel3.Controls.Add(buttonClock, 0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // buttonAntiClock
            // 
            resources.ApplyResources(buttonAntiClock, "buttonAntiClock");
            buttonAntiClock.Name = "buttonAntiClock";
            toolTip.SetToolTip(buttonAntiClock, resources.GetString("buttonAntiClock.ToolTip"));
            buttonAntiClock.UseVisualStyleBackColor = true;
            buttonAntiClock.Click += buttonTopLeft_Click;
            // 
            // buttonClock
            // 
            resources.ApplyResources(buttonClock, "buttonClock");
            buttonClock.Name = "buttonClock";
            toolTip.SetToolTip(buttonClock, resources.GetString("buttonClock.ToolTip"));
            buttonClock.UseVisualStyleBackColor = true;
            buttonClock.Click += buttonTopLeft_Click;
            // 
            // buttonTopLeft
            // 
            resources.ApplyResources(buttonTopLeft, "buttonTopLeft");
            buttonTopLeft.Name = "buttonTopLeft";
            toolTip.SetToolTip(buttonTopLeft, resources.GetString("buttonTopLeft.ToolTip"));
            buttonTopLeft.UseVisualStyleBackColor = true;
            buttonTopLeft.Click += buttonTopLeft_Click;
            // 
            // buttonLeft
            // 
            resources.ApplyResources(buttonLeft, "buttonLeft");
            buttonLeft.Name = "buttonLeft";
            toolTip.SetToolTip(buttonLeft, resources.GetString("buttonLeft.ToolTip"));
            buttonLeft.UseVisualStyleBackColor = true;
            buttonLeft.Click += buttonTopLeft_Click;
            // 
            // buttonBottomLeft
            // 
            resources.ApplyResources(buttonBottomLeft, "buttonBottomLeft");
            buttonBottomLeft.Name = "buttonBottomLeft";
            toolTip.SetToolTip(buttonBottomLeft, resources.GetString("buttonBottomLeft.ToolTip"));
            buttonBottomLeft.UseVisualStyleBackColor = false;
            buttonBottomLeft.Click += buttonTopLeft_Click;
            // 
            // buttonBottom
            // 
            resources.ApplyResources(buttonBottom, "buttonBottom");
            buttonBottom.Name = "buttonBottom";
            toolTip.SetToolTip(buttonBottom, resources.GetString("buttonBottom.ToolTip"));
            buttonBottom.UseVisualStyleBackColor = true;
            buttonBottom.Click += buttonTopLeft_Click;
            // 
            // buttonBottomRight
            // 
            resources.ApplyResources(buttonBottomRight, "buttonBottomRight");
            buttonBottomRight.Name = "buttonBottomRight";
            toolTip.SetToolTip(buttonBottomRight, resources.GetString("buttonBottomRight.ToolTip"));
            buttonBottomRight.UseVisualStyleBackColor = true;
            buttonBottomRight.Click += buttonTopLeft_Click;
            // 
            // buttonTop
            // 
            resources.ApplyResources(buttonTop, "buttonTop");
            buttonTop.Name = "buttonTop";
            toolTip.SetToolTip(buttonTop, resources.GetString("buttonTop.ToolTip"));
            buttonTop.UseVisualStyleBackColor = false;
            buttonTop.Click += buttonTopLeft_Click;
            // 
            // buttonTopRight
            // 
            resources.ApplyResources(buttonTopRight, "buttonTopRight");
            buttonTopRight.Name = "buttonTopRight";
            toolTip.SetToolTip(buttonTopRight, resources.GetString("buttonTopRight.ToolTip"));
            buttonTopRight.UseVisualStyleBackColor = false;
            buttonTopRight.Click += buttonTopLeft_Click;
            // 
            // buttonRight
            // 
            resources.ApplyResources(buttonRight, "buttonRight");
            buttonRight.Name = "buttonRight";
            toolTip.SetToolTip(buttonRight, resources.GetString("buttonRight.ToolTip"));
            buttonRight.UseVisualStyleBackColor = false;
            buttonRight.Click += buttonTopLeft_Click;
            // 
            // panelViewStep
            // 
            resources.ApplyResources(panelViewStep, "panelViewStep");
            panelViewStep.Controls.Add(numericBoxStep);
            panelViewStep.Name = "panelViewStep";
            // 
            // numericBoxStep
            // 
            numericBoxStep.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxStep, "numericBoxStep");
            numericBoxStep.Maximum = 360D;
            numericBoxStep.Minimum = 0.001D;
            numericBoxStep.Name = "numericBoxStep";
            numericBoxStep.RadianValue = 0.17453292519943295D;
            numericBoxStep.ShowUpDown = true;
            numericBoxStep.SmartIncrement = true;
            numericBoxStep.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxStep, resources.GetString("numericBoxStep.ToolTip"));
            numericBoxStep.Value = 10D;
            numericBoxStep.ValueFontSize = 9F;
            // 
            // buttonResetAngle
            // 
            resources.ApplyResources(buttonResetAngle, "buttonResetAngle");
            buttonResetAngle.Name = "buttonResetAngle";
            toolTip.SetToolTip(buttonResetAngle, resources.GetString("buttonResetAngle.ToolTip"));
            buttonResetAngle.UseVisualStyleBackColor = true;
            buttonResetAngle.Click += buttonResetAngle_Click;
            // 
            // label21
            // 
            resources.ApplyResources(label21, "label21");
            label21.Name = "label21";
            toolTip.SetToolTip(label21, resources.GetString("label21.ToolTip"));
            // 
            // colorControl3D_EwaldSphere
            // 
            resources.ApplyResources(colorControl3D_EwaldSphere, "colorControl3D_EwaldSphere");
            colorControl3D_EwaldSphere.BackColor = System.Drawing.SystemColors.Control;
            colorControl3D_EwaldSphere.BoxSize = new System.Drawing.Size(20, 20);
            colorControl3D_EwaldSphere.Color = System.Drawing.Color.FromArgb(128, 128, 255);
            colorControl3D_EwaldSphere.Name = "colorControl3D_EwaldSphere";
            colorControl3D_EwaldSphere.ColorChanged += colorControlReciprocalBackground_ColorChanged;
            // 
            // colorControl3D_Origin
            // 
            resources.ApplyResources(colorControl3D_Origin, "colorControl3D_Origin");
            colorControl3D_Origin.BackColor = System.Drawing.SystemColors.Control;
            colorControl3D_Origin.BoxSize = new System.Drawing.Size(20, 20);
            colorControl3D_Origin.Color = System.Drawing.Color.FromArgb(255, 0, 0);
            colorControl3D_Origin.Name = "colorControl3D_Origin";
            colorControl3D_Origin.ColorChanged += colorControlReciprocalBackground_ColorChanged;
            // 
            // colorControl3D_rightDirection
            // 
            resources.ApplyResources(colorControl3D_rightDirection, "colorControl3D_rightDirection");
            colorControl3D_rightDirection.BackColor = System.Drawing.SystemColors.Control;
            colorControl3D_rightDirection.BoxSize = new System.Drawing.Size(20, 20);
            colorControl3D_rightDirection.Color = System.Drawing.Color.FromArgb(128, 255, 128);
            colorControl3D_rightDirection.Name = "colorControl3D_rightDirection";
            colorControl3D_rightDirection.ColorChanged += colorControlReciprocalBackground_ColorChanged;
            // 
            // colorControl3D_topDirection
            // 
            resources.ApplyResources(colorControl3D_topDirection, "colorControl3D_topDirection");
            colorControl3D_topDirection.BackColor = System.Drawing.SystemColors.Control;
            colorControl3D_topDirection.BoxSize = new System.Drawing.Size(20, 20);
            colorControl3D_topDirection.Color = System.Drawing.Color.FromArgb(255, 192, 255);
            colorControl3D_topDirection.Name = "colorControl3D_topDirection";
            colorControl3D_topDirection.ColorChanged += colorControlReciprocalBackground_ColorChanged;
            // 
            // colorControl3D_beamDirection
            // 
            resources.ApplyResources(colorControl3D_beamDirection, "colorControl3D_beamDirection");
            colorControl3D_beamDirection.BackColor = System.Drawing.SystemColors.Control;
            colorControl3D_beamDirection.BoxSize = new System.Drawing.Size(20, 20);
            colorControl3D_beamDirection.Color = System.Drawing.Color.FromArgb(255, 128, 0);
            colorControl3D_beamDirection.Name = "colorControl3D_beamDirection";
            colorControl3D_beamDirection.ColorChanged += colorControlReciprocalBackground_ColorChanged;
            // 
            // colorControl3D_lText
            // 
            resources.ApplyResources(colorControl3D_lText, "colorControl3D_lText");
            colorControl3D_lText.BackColor = System.Drawing.SystemColors.Control;
            colorControl3D_lText.BoxSize = new System.Drawing.Size(20, 20);
            colorControl3D_lText.Color = System.Drawing.Color.FromArgb(0, 0, 0);
            colorControl3D_lText.Name = "colorControl3D_lText";
            //toolTip.SetToolTip(colorControl3D_lText, resources.GetString("colorControl3D_lText.ToolTip1")); // 260703Cl 変更前: EN resx 内の日本語文言 (.ToolTip1) を参照し英語 UI で日本語チップが出ていた
            toolTip.SetToolTip(colorControl3D_lText, resources.GetString("colorControl3D_lText.ToolTip")); // 260703Cl 正キーへ統一
            colorControl3D_lText.ColorChanged += colorControlReciprocalBackground_ColorChanged;
            // 
            // checkBox3D_DirectionGuide
            // 
            resources.ApplyResources(checkBox3D_DirectionGuide, "checkBox3D_DirectionGuide");
            checkBox3D_DirectionGuide.Checked = true;
            checkBox3D_DirectionGuide.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3D_DirectionGuide.Name = "checkBox3D_DirectionGuide";
            toolTip.SetToolTip(checkBox3D_DirectionGuide, resources.GetString("checkBox3D_DirectionGuide.ToolTip"));
            checkBox3D_DirectionGuide.UseVisualStyleBackColor = true;
            checkBox3D_DirectionGuide.CheckedChanged += checkBoxShowEwaldSphere_CheckedChanged;
            // 
            // checkBox3D_EwaldSphere
            // 
            resources.ApplyResources(checkBox3D_EwaldSphere, "checkBox3D_EwaldSphere");
            checkBox3D_EwaldSphere.Checked = true;
            checkBox3D_EwaldSphere.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3D_EwaldSphere.Name = "checkBox3D_EwaldSphere";
            toolTip.SetToolTip(checkBox3D_EwaldSphere, resources.GetString("checkBox3D_EwaldSphere.ToolTip"));
            checkBox3D_EwaldSphere.UseVisualStyleBackColor = true;
            checkBox3D_EwaldSphere.CheckedChanged += checkBoxShowEwaldSphere_CheckedChanged;
            // 
            // panelReciprocalSpace
            // 
            panelReciprocalSpace.Controls.Add(checkBoxReciprocalSpace);
            resources.ApplyResources(panelReciprocalSpace, "panelReciprocalSpace");
            panelReciprocalSpace.Name = "panelReciprocalSpace";
            // 
            // checkBoxReciprocalSpace
            // 
            resources.ApplyResources(checkBoxReciprocalSpace, "checkBoxReciprocalSpace");
            checkBoxReciprocalSpace.Name = "checkBoxReciprocalSpace";
            toolTip.SetToolTip(checkBoxReciprocalSpace, resources.GetString("checkBoxReciprocalSpace.ToolTip"));
            checkBoxReciprocalSpace.UseVisualStyleBackColor = true;
            checkBoxReciprocalSpace.CheckedChanged += checkBoxReciprocalSpace_CheckedChanged;
            // 
            // panelMousePosition
            // 
            resources.ApplyResources(panelMousePosition, "panelMousePosition");
            panelMousePosition.Controls.Add(tableLayoutPanel2);
            panelMousePosition.Controls.Add(tableLayoutPanelMousePotionDetailed);
            panelMousePosition.Controls.Add(checkBoxMousePositionDetailes);
            panelMousePosition.Name = "panelMousePosition";
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
            tableLayoutPanel2.Controls.Add(label24, 0, 0);
            tableLayoutPanel2.Controls.Add(labelD, 1, 0);
            tableLayoutPanel2.Controls.Add(labelDinv, 2, 0);
            tableLayoutPanel2.Controls.Add(labelTwoThetaDeg, 3, 0);
            tableLayoutPanel2.Controls.Add(labelTwoThetaRad, 4, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // label24
            // 
            resources.ApplyResources(label24, "label24");
            label24.Name = "label24";
            toolTip.SetToolTip(label24, resources.GetString("label24.ToolTip"));
            // 
            // labelD
            // 
            resources.ApplyResources(labelD, "labelD");
            labelD.Name = "labelD";
            toolTip.SetToolTip(labelD, resources.GetString("labelD.ToolTip"));
            // 
            // labelDinv
            // 
            resources.ApplyResources(labelDinv, "labelDinv");
            labelDinv.Name = "labelDinv";
            toolTip.SetToolTip(labelDinv, resources.GetString("labelDinv.ToolTip"));
            // 
            // labelTwoThetaDeg
            // 
            resources.ApplyResources(labelTwoThetaDeg, "labelTwoThetaDeg");
            labelTwoThetaDeg.Name = "labelTwoThetaDeg";
            toolTip.SetToolTip(labelTwoThetaDeg, resources.GetString("labelTwoThetaDeg.ToolTip"));
            // 
            // labelTwoThetaRad
            // 
            resources.ApplyResources(labelTwoThetaRad, "labelTwoThetaRad");
            labelTwoThetaRad.Name = "labelTwoThetaRad";
            toolTip.SetToolTip(labelTwoThetaRad, resources.GetString("labelTwoThetaRad.ToolTip"));
            // 
            // tableLayoutPanelMousePotionDetailed
            // 
            resources.ApplyResources(tableLayoutPanelMousePotionDetailed, "tableLayoutPanelMousePotionDetailed");
            tableLayoutPanelMousePotionDetailed.Controls.Add(labelMousePositionDetector, 1, 0);
            tableLayoutPanelMousePotionDetailed.Controls.Add(labelMousePositionReal, 1, 1);
            tableLayoutPanelMousePotionDetailed.Controls.Add(labelMousePositionReciprocal, 1, 2);
            tableLayoutPanelMousePotionDetailed.Controls.Add(label17, 0, 1);
            tableLayoutPanelMousePotionDetailed.Controls.Add(label20, 0, 2);
            tableLayoutPanelMousePotionDetailed.Controls.Add(label9, 0, 0);
            tableLayoutPanelMousePotionDetailed.Name = "tableLayoutPanelMousePotionDetailed";
            // 
            // labelMousePositionDetector
            // 
            resources.ApplyResources(labelMousePositionDetector, "labelMousePositionDetector");
            labelMousePositionDetector.Name = "labelMousePositionDetector";
            toolTip.SetToolTip(labelMousePositionDetector, resources.GetString("labelMousePositionDetector.ToolTip"));
            // 
            // labelMousePositionReal
            // 
            resources.ApplyResources(labelMousePositionReal, "labelMousePositionReal");
            labelMousePositionReal.Name = "labelMousePositionReal";
            toolTip.SetToolTip(labelMousePositionReal, resources.GetString("labelMousePositionReal.ToolTip"));
            // 
            // labelMousePositionReciprocal
            // 
            resources.ApplyResources(labelMousePositionReciprocal, "labelMousePositionReciprocal");
            labelMousePositionReciprocal.Name = "labelMousePositionReciprocal";
            toolTip.SetToolTip(labelMousePositionReciprocal, resources.GetString("labelMousePositionReciprocal.ToolTip"));
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            toolTip.SetToolTip(label17, resources.GetString("label17.ToolTip"));
            // 
            // label20
            // 
            resources.ApplyResources(label20, "label20");
            label20.Name = "label20";
            toolTip.SetToolTip(label20, resources.GetString("label20.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            toolTip.SetToolTip(label9, resources.GetString("label9.ToolTip"));
            // 
            // checkBoxMousePositionDetailes
            // 
            resources.ApplyResources(checkBoxMousePositionDetailes, "checkBoxMousePositionDetailes");
            checkBoxMousePositionDetailes.Name = "checkBoxMousePositionDetailes";
            toolTip.SetToolTip(checkBoxMousePositionDetailes, resources.GetString("checkBoxMousePositionDetailes.ToolTip"));
            checkBoxMousePositionDetailes.UseVisualStyleBackColor = true;
            checkBoxMousePositionDetailes.CheckedChanged += checkBoxMousePositionDetailes_CheckedChanged;
            // 
            // flowLayoutPanel6
            // 
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Controls.Add(groupBoxMonitor);
            flowLayoutPanel6.Controls.Add(panelDetectorAndMisc);
            flowLayoutPanel6.Controls.Add(groupBoxDeveloperCode);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            // 
            // groupBoxMonitor
            // 
            resources.ApplyResources(groupBoxMonitor, "groupBoxMonitor");
            captureExtender.SetCapture(groupBoxMonitor, true);
            groupBoxMonitor.Controls.Add(flowLayoutPanel3);
            groupBoxMonitor.Controls.Add(flowLayoutPanel2);
            groupBoxMonitor.Controls.Add(flowLayoutPanelImageOrientation);
            groupBoxMonitor.Controls.Add(sizeControl1);
            groupBoxMonitor.Controls.Add(flowLayoutPanelResolutionUnit);
            groupBoxMonitor.Name = "groupBoxMonitor";
            groupBoxMonitor.TabStop = false;
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(checkBoxFlipHorizontally);
            flowLayoutPanel3.Controls.Add(checkBoxFlipVertically);
            flowLayoutPanel3.Controls.Add(checkBoxNegativeImage);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // checkBoxFlipHorizontally
            // 
            resources.ApplyResources(checkBoxFlipHorizontally, "checkBoxFlipHorizontally");
            checkBoxFlipHorizontally.Name = "checkBoxFlipHorizontally";
            toolTip.SetToolTip(checkBoxFlipHorizontally, resources.GetString("checkBoxFlipHorizontally.ToolTip"));
            checkBoxFlipHorizontally.UseVisualStyleBackColor = true;
            checkBoxFlipHorizontally.CheckedChanged += checkBoxFlipHorizontally_CheckedChanged;
            // 
            // checkBoxFlipVertically
            // 
            resources.ApplyResources(checkBoxFlipVertically, "checkBoxFlipVertically");
            checkBoxFlipVertically.Name = "checkBoxFlipVertically";
            toolTip.SetToolTip(checkBoxFlipVertically, resources.GetString("checkBoxFlipVertically.ToolTip"));
            checkBoxFlipVertically.UseVisualStyleBackColor = true;
            checkBoxFlipVertically.CheckedChanged += checkBoxFlipHorizontally_CheckedChanged;
            // 
            // checkBoxNegativeImage
            // 
            resources.ApplyResources(checkBoxNegativeImage, "checkBoxNegativeImage");
            checkBoxNegativeImage.Name = "checkBoxNegativeImage";
            toolTip.SetToolTip(checkBoxNegativeImage, resources.GetString("checkBoxNegativeImage.ToolTip"));
            checkBoxNegativeImage.UseVisualStyleBackColor = true;
            checkBoxNegativeImage.CheckedChanged += checkBoxNegativeImage_CheckedChanged;
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(buttonResetCenter);
            flowLayoutPanel2.Controls.Add(comboBoxCenter);
            flowLayoutPanel2.Controls.Add(checkBoxFixCenter);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // buttonResetCenter
            // 
            resources.ApplyResources(buttonResetCenter, "buttonResetCenter");
            buttonResetCenter.Name = "buttonResetCenter";
            toolTip.SetToolTip(buttonResetCenter, resources.GetString("buttonResetCenter.ToolTip"));
            buttonResetCenter.UseVisualStyleBackColor = true;
            buttonResetCenter.Click += ButtonResetCenter_Click;
            // 
            // comboBoxCenter
            // 
            comboBoxCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxCenter, "comboBoxCenter");
            comboBoxCenter.FormattingEnabled = true;
            comboBoxCenter.Items.AddRange(new object[] { resources.GetString("comboBoxCenter.Items"), resources.GetString("comboBoxCenter.Items1"), resources.GetString("comboBoxCenter.Items2") });
            comboBoxCenter.Name = "comboBoxCenter";
            toolTip.SetToolTip(comboBoxCenter, resources.GetString("comboBoxCenter.ToolTip"));
            comboBoxCenter.SelectedIndexChanged += comboBoxCenter_SelectedIndexChanged;
            // 
            // checkBoxFixCenter
            // 
            resources.ApplyResources(checkBoxFixCenter, "checkBoxFixCenter");
            checkBoxFixCenter.Checked = true;
            checkBoxFixCenter.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxFixCenter.Name = "checkBoxFixCenter";
            toolTip.SetToolTip(checkBoxFixCenter, resources.GetString("checkBoxFixCenter.ToolTip"));
            checkBoxFixCenter.UseVisualStyleBackColor = true;
            checkBoxFixCenter.CheckedChanged += CheckBoxFixCenter_CheckedChanged;
            // 
            // flowLayoutPanelImageOrientation
            // 
            resources.ApplyResources(flowLayoutPanelImageOrientation, "flowLayoutPanelImageOrientation");
            flowLayoutPanelImageOrientation.Name = "flowLayoutPanelImageOrientation";
            // 
            // sizeControl1
            // 
            resources.ApplyResources(sizeControl1, "sizeControl1");
            sizeControl1.Maximum = 2000;
            sizeControl1.Name = "sizeControl1";
            sizeControl1.ValueChanged += sizeControl1_ValueChanged;
            // 
            // flowLayoutPanelResolutionUnit
            // 
            resources.ApplyResources(flowLayoutPanelResolutionUnit, "flowLayoutPanelResolutionUnit");
            flowLayoutPanelResolutionUnit.Controls.Add(numericBoxResolution);
            flowLayoutPanelResolutionUnit.Controls.Add(radioButtonResoUnitMilliMeter);
            flowLayoutPanelResolutionUnit.Controls.Add(radioButtonResoUnitNanometerInv);
            flowLayoutPanelResolutionUnit.Name = "flowLayoutPanelResolutionUnit";
            // 
            // numericBoxResolution
            // 
            numericBoxResolution.BackColor = System.Drawing.SystemColors.Control;
            numericBoxResolution.DecimalPlaces = 6;
            resources.ApplyResources(numericBoxResolution, "numericBoxResolution");
            numericBoxResolution.Maximum = 10D;
            numericBoxResolution.Minimum = 1E-05D;
            numericBoxResolution.Name = "numericBoxResolution";
            numericBoxResolution.RadianValue = 0.0013962634015954637D;
            numericBoxResolution.ShowUpDown = true;
            numericBoxResolution.SmartIncrement = true;
            numericBoxResolution.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxResolution, resources.GetString("numericBoxResolution.ToolTip"));
            numericBoxResolution.Value = 0.08D;
            numericBoxResolution.ValueBoxWidth = 60;
            numericBoxResolution.ValueChanged += numericUpDownResolution_ValueChanged;
            // 
            // radioButtonResoUnitMilliMeter
            // 
            resources.ApplyResources(radioButtonResoUnitMilliMeter, "radioButtonResoUnitMilliMeter");
            radioButtonResoUnitMilliMeter.Checked = true;
            radioButtonResoUnitMilliMeter.Name = "radioButtonResoUnitMilliMeter";
            radioButtonResoUnitMilliMeter.TabStop = true;
            toolTip.SetToolTip(radioButtonResoUnitMilliMeter, resources.GetString("radioButtonResoUnitMilliMeter.ToolTip"));
            radioButtonResoUnitMilliMeter.UseVisualStyleBackColor = true;
            radioButtonResoUnitMilliMeter.CheckedChanged += radioButtonResoUnit_CheckedChanged;
            // 
            // radioButtonResoUnitNanometerInv
            // 
            resources.ApplyResources(radioButtonResoUnitNanometerInv, "radioButtonResoUnitNanometerInv");
            radioButtonResoUnitNanometerInv.Name = "radioButtonResoUnitNanometerInv";
            toolTip.SetToolTip(radioButtonResoUnitNanometerInv, resources.GetString("radioButtonResoUnitNanometerInv.ToolTip"));
            radioButtonResoUnitNanometerInv.UseVisualStyleBackColor = true;
            radioButtonResoUnitNanometerInv.CheckedChanged += radioButtonResoUnit_CheckedChanged;
            // 
            // panelDetectorAndMisc
            // 
            panelDetectorAndMisc.Controls.Add(groupBoxMisc);
            panelDetectorAndMisc.Controls.Add(groupBoxDetectorGeometry);
            resources.ApplyResources(panelDetectorAndMisc, "panelDetectorAndMisc");
            panelDetectorAndMisc.Name = "panelDetectorAndMisc";
            // 
            // groupBoxMisc
            // 
            captureExtender.SetCapture(groupBoxMisc, true);
            groupBoxMisc.Controls.Add(trackBarRotationSpeed);
            groupBoxMisc.Controls.Add(buttonHolderSimulation);
            groupBoxMisc.Controls.Add(label23);
            resources.ApplyResources(groupBoxMisc, "groupBoxMisc");
            groupBoxMisc.Name = "groupBoxMisc";
            groupBoxMisc.TabStop = false;
            // 
            // trackBarRotationSpeed
            // 
            resources.ApplyResources(trackBarRotationSpeed, "trackBarRotationSpeed");
            trackBarRotationSpeed.Maximum = 30;
            trackBarRotationSpeed.Minimum = 1;
            trackBarRotationSpeed.Name = "trackBarRotationSpeed";
            trackBarRotationSpeed.TickFrequency = 5;
            trackBarRotationSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarRotationSpeed, resources.GetString("trackBarRotationSpeed.ToolTip"));
            trackBarRotationSpeed.Value = 10;
            // 
            // buttonHolderSimulation
            // 
            resources.ApplyResources(buttonHolderSimulation, "buttonHolderSimulation");
            buttonHolderSimulation.Name = "buttonHolderSimulation";
            toolTip.SetToolTip(buttonHolderSimulation, resources.GetString("buttonHolderSimulation.ToolTip"));
            buttonHolderSimulation.UseVisualStyleBackColor = true;
            buttonHolderSimulation.Click += buttonHolderSimulation_Click;
            // 
            // label23
            // 
            resources.ApplyResources(label23, "label23");
            label23.Name = "label23";
            toolTip.SetToolTip(label23, resources.GetString("label23.ToolTip"));
            // 
            // groupBoxDetectorGeometry
            // 
            captureExtender.SetCapture(groupBoxDetectorGeometry, true);
            groupBoxDetectorGeometry.Controls.Add(flowLayoutPanel4);
            resources.ApplyResources(groupBoxDetectorGeometry, "groupBoxDetectorGeometry");
            groupBoxDetectorGeometry.Name = "groupBoxDetectorGeometry";
            groupBoxDetectorGeometry.TabStop = false;
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(numericBoxCameraLength2);
            flowLayoutPanel4.Controls.Add(buttonDetailedGeometry);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // numericBoxCameraLength2
            // 
            numericBoxCameraLength2.BackColor = System.Drawing.Color.Transparent;
            numericBoxCameraLength2.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxCameraLength2, "numericBoxCameraLength2");
            numericBoxCameraLength2.Minimum = 0D;
            numericBoxCameraLength2.Name = "numericBoxCameraLength2";
            numericBoxCameraLength2.RadianValue = 17.453292519943293D;
            numericBoxCameraLength2.ShowUpDown = true;
            numericBoxCameraLength2.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxCameraLength2, resources.GetString("numericBoxCameraLength2.ToolTip"));
            numericBoxCameraLength2.Value = 1000D;
            numericBoxCameraLength2.ValueBoxWidth = 55;
            numericBoxCameraLength2.ValueChanged += numericBoxCamaraLength2_ValueChanged;
            // 
            // buttonDetailedGeometry
            // 
            resources.ApplyResources(buttonDetailedGeometry, "buttonDetailedGeometry");
            buttonDetailedGeometry.Name = "buttonDetailedGeometry";
            toolTip.SetToolTip(buttonDetailedGeometry, resources.GetString("buttonDetailedGeometry.ToolTip"));
            buttonDetailedGeometry.UseVisualStyleBackColor = true;
            buttonDetailedGeometry.Click += buttonDetailedGeometry_Click;
            // 
            // groupBoxDeveloperCode
            // 
            groupBoxDeveloperCode.Controls.Add(numericBoxDev);
            groupBoxDeveloperCode.Controls.Add(button1);
            groupBoxDeveloperCode.Controls.Add(numericBoxAcc);
            groupBoxDeveloperCode.Controls.Add(button2);
            resources.ApplyResources(groupBoxDeveloperCode, "groupBoxDeveloperCode");
            groupBoxDeveloperCode.Name = "groupBoxDeveloperCode";
            groupBoxDeveloperCode.TabStop = false;
            // 
            // numericBoxDev
            // 
            numericBoxDev.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxDev, "numericBoxDev");
            numericBoxDev.Name = "numericBoxDev";
            numericBoxDev.RadianValue = 0.023911010752322315D;
            numericBoxDev.SkipEventDuringInput = false;
            numericBoxDev.SmartIncrement = true;
            numericBoxDev.ThousandsSeparator = true;
            numericBoxDev.Value = 1.37D;
            numericBoxDev.ValueFontSize = 9F;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // numericBoxAcc
            // 
            numericBoxAcc.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxAcc, "numericBoxAcc");
            numericBoxAcc.Name = "numericBoxAcc";
            numericBoxAcc.RadianValue = 216.42082724729684D;
            numericBoxAcc.SkipEventDuringInput = false;
            numericBoxAcc.SmartIncrement = true;
            numericBoxAcc.ThousandsSeparator = true;
            numericBoxAcc.Value = 12400D;
            numericBoxAcc.ValueFontSize = 9F;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, optionToolStripMenuItem, presetToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            captureExtender.SetCapture(fileToolStripMenuItem, true);
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveImageToolStripMenuItem, saveDetectorAreaToolStripMenuItem, saveCBEDPatternToolStripMenuItem, copyImageToClipboardToolStripMenuItem, copyDetectorAreaToolStripMenuItem, copyCBEDPatternToolStripMenuItem, toolStripSeparator1, toolStripMenuItem1, toolStripSeparator8, pageSetupToolStripMenuItem, printPreviewToolStripMenuItem, printToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // saveImageToolStripMenuItem
            // 
            saveImageToolStripMenuItem.AutoToolTip = true;
            saveImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveAsImageToolStripMenuItem, saveAsMetafileToolStripMenuItem });
            saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            resources.ApplyResources(saveImageToolStripMenuItem, "saveImageToolStripMenuItem");
            // 
            // saveAsImageToolStripMenuItem
            // 
            saveAsImageToolStripMenuItem.Name = "saveAsImageToolStripMenuItem";
            resources.ApplyResources(saveAsImageToolStripMenuItem, "saveAsImageToolStripMenuItem");
            saveAsImageToolStripMenuItem.Click += SaveAsImageToolStripMenuItem_Click;
            // 
            // saveAsMetafileToolStripMenuItem
            // 
            saveAsMetafileToolStripMenuItem.Name = "saveAsMetafileToolStripMenuItem";
            resources.ApplyResources(saveAsMetafileToolStripMenuItem, "saveAsMetafileToolStripMenuItem");
            saveAsMetafileToolStripMenuItem.Click += SaveAsMetafileToolStripMenuItem_Click;
            // 
            // saveDetectorAreaToolStripMenuItem
            // 
            saveDetectorAreaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveDetectorAsImageToolStripMenuItem, saveDetectorAsMetafileToolStripMenuItem });
            saveDetectorAreaToolStripMenuItem.Name = "saveDetectorAreaToolStripMenuItem";
            resources.ApplyResources(saveDetectorAreaToolStripMenuItem, "saveDetectorAreaToolStripMenuItem");
            // 
            // saveDetectorAsImageToolStripMenuItem
            // 
            saveDetectorAsImageToolStripMenuItem.Name = "saveDetectorAsImageToolStripMenuItem";
            resources.ApplyResources(saveDetectorAsImageToolStripMenuItem, "saveDetectorAsImageToolStripMenuItem");
            saveDetectorAsImageToolStripMenuItem.Click += SaveDetectorAsImageToolStripMenuItem_Click;
            // 
            // saveDetectorAsMetafileToolStripMenuItem
            // 
            saveDetectorAsMetafileToolStripMenuItem.Name = "saveDetectorAsMetafileToolStripMenuItem";
            resources.ApplyResources(saveDetectorAsMetafileToolStripMenuItem, "saveDetectorAsMetafileToolStripMenuItem");
            saveDetectorAsMetafileToolStripMenuItem.Click += SaveDetectorAsMetafileToolStripMenuItem_Click;
            // 
            // saveCBEDPatternToolStripMenuItem
            // 
            saveCBEDPatternToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveCBEDasPngToolStripMenuItem, saveCBEDasTiffToolStripMenuItem, asPixelByPixelImagePNGFormatToolStripMenuItem, asCollectiveImageTiffFormatToolStripMenuItem });
            saveCBEDPatternToolStripMenuItem.Name = "saveCBEDPatternToolStripMenuItem";
            resources.ApplyResources(saveCBEDPatternToolStripMenuItem, "saveCBEDPatternToolStripMenuItem");
            // 
            // saveCBEDasPngToolStripMenuItem
            // 
            saveCBEDasPngToolStripMenuItem.Name = "saveCBEDasPngToolStripMenuItem";
            resources.ApplyResources(saveCBEDasPngToolStripMenuItem, "saveCBEDasPngToolStripMenuItem");
            saveCBEDasPngToolStripMenuItem.Click += SaveCBEDasPngToolStripMenuItem_Click;
            // 
            // saveCBEDasTiffToolStripMenuItem
            // 
            saveCBEDasTiffToolStripMenuItem.Name = "saveCBEDasTiffToolStripMenuItem";
            resources.ApplyResources(saveCBEDasTiffToolStripMenuItem, "saveCBEDasTiffToolStripMenuItem");
            saveCBEDasTiffToolStripMenuItem.Click += SaveCBEDasTiffToolStripMenuItem_Click;
            // 
            // asPixelByPixelImagePNGFormatToolStripMenuItem
            // 
            asPixelByPixelImagePNGFormatToolStripMenuItem.Name = "asPixelByPixelImagePNGFormatToolStripMenuItem";
            resources.ApplyResources(asPixelByPixelImagePNGFormatToolStripMenuItem, "asPixelByPixelImagePNGFormatToolStripMenuItem");
            asPixelByPixelImagePNGFormatToolStripMenuItem.Click += SaveCBEDasCollectiveImageToolStripMenuItem_Click;
            // 
            // asCollectiveImageTiffFormatToolStripMenuItem
            // 
            asCollectiveImageTiffFormatToolStripMenuItem.Name = "asCollectiveImageTiffFormatToolStripMenuItem";
            resources.ApplyResources(asCollectiveImageTiffFormatToolStripMenuItem, "asCollectiveImageTiffFormatToolStripMenuItem");
            asCollectiveImageTiffFormatToolStripMenuItem.Click += AsCollectiveImageTiffFormatToolStripMenuItem_Click;
            // 
            // copyImageToClipboardToolStripMenuItem
            // 
            copyImageToClipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { copyAsImageToolStripMenuItem, copyAsMetafileToolStripMenuItem });
            copyImageToClipboardToolStripMenuItem.Name = "copyImageToClipboardToolStripMenuItem";
            resources.ApplyResources(copyImageToClipboardToolStripMenuItem, "copyImageToClipboardToolStripMenuItem");
            // 
            // copyAsImageToolStripMenuItem
            // 
            copyAsImageToolStripMenuItem.Name = "copyAsImageToolStripMenuItem";
            resources.ApplyResources(copyAsImageToolStripMenuItem, "copyAsImageToolStripMenuItem");
            copyAsImageToolStripMenuItem.Click += CopyAsImageToolStripMenuItem1_Click;
            // 
            // copyAsMetafileToolStripMenuItem
            // 
            copyAsMetafileToolStripMenuItem.Name = "copyAsMetafileToolStripMenuItem";
            resources.ApplyResources(copyAsMetafileToolStripMenuItem, "copyAsMetafileToolStripMenuItem");
            copyAsMetafileToolStripMenuItem.Click += CopyAsMetafileToolStripMenuItem1_Click;
            // 
            // copyDetectorAreaToolStripMenuItem
            // 
            copyDetectorAreaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { copyDetectorAsImageToolStripMenuItem, copyDetectorAsMetafileToolStripMenuItem });
            copyDetectorAreaToolStripMenuItem.Name = "copyDetectorAreaToolStripMenuItem";
            resources.ApplyResources(copyDetectorAreaToolStripMenuItem, "copyDetectorAreaToolStripMenuItem");
            // 
            // copyDetectorAsImageToolStripMenuItem
            // 
            copyDetectorAsImageToolStripMenuItem.Name = "copyDetectorAsImageToolStripMenuItem";
            resources.ApplyResources(copyDetectorAsImageToolStripMenuItem, "copyDetectorAsImageToolStripMenuItem");
            copyDetectorAsImageToolStripMenuItem.Click += CopyDetectorAsImageWithOverlappeImageToolStripMenuItem_Click;
            // 
            // copyDetectorAsMetafileToolStripMenuItem
            // 
            copyDetectorAsMetafileToolStripMenuItem.Name = "copyDetectorAsMetafileToolStripMenuItem";
            resources.ApplyResources(copyDetectorAsMetafileToolStripMenuItem, "copyDetectorAsMetafileToolStripMenuItem");
            copyDetectorAsMetafileToolStripMenuItem.Click += CopyDetectorAsMetafileWithOverlappedImageToolStripMenuItem_Click;
            // 
            // copyCBEDPatternToolStripMenuItem
            // 
            copyCBEDPatternToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { copyCBEDasImageToolStripMenuItem });
            copyCBEDPatternToolStripMenuItem.Name = "copyCBEDPatternToolStripMenuItem";
            resources.ApplyResources(copyCBEDPatternToolStripMenuItem, "copyCBEDPatternToolStripMenuItem");
            // 
            // copyCBEDasImageToolStripMenuItem
            // 
            copyCBEDasImageToolStripMenuItem.Name = "copyCBEDasImageToolStripMenuItem";
            resources.ApplyResources(copyCBEDasImageToolStripMenuItem, "copyCBEDasImageToolStripMenuItem");
            copyCBEDasImageToolStripMenuItem.Click += CopyCBEDasImageToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.AutoToolTip = true;
            toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemSaveMovieDiffractionPattern, toolStripMenuItemSaveMovieReciprocalSpace });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // toolStripMenuItemSaveMovieDiffractionPattern
            // 
            toolStripMenuItemSaveMovieDiffractionPattern.Name = "toolStripMenuItemSaveMovieDiffractionPattern";
            resources.ApplyResources(toolStripMenuItemSaveMovieDiffractionPattern, "toolStripMenuItemSaveMovieDiffractionPattern");
            toolStripMenuItemSaveMovieDiffractionPattern.Click += toolStripMenuItemSaveMovieDiffractionPattern_Click;
            // 
            // toolStripMenuItemSaveMovieReciprocalSpace
            // 
            toolStripMenuItemSaveMovieReciprocalSpace.Name = "toolStripMenuItemSaveMovieReciprocalSpace";
            resources.ApplyResources(toolStripMenuItemSaveMovieReciprocalSpace, "toolStripMenuItemSaveMovieReciprocalSpace");
            toolStripMenuItemSaveMovieReciprocalSpace.Click += toolStripMenuItemSaveMovieReciprocalSpace_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(toolStripSeparator8, "toolStripSeparator8");
            // 
            // pageSetupToolStripMenuItem
            // 
            pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
            resources.ApplyResources(pageSetupToolStripMenuItem, "pageSetupToolStripMenuItem");
            pageSetupToolStripMenuItem.Click += pageSetupToolStripMenuItem_Click;
            // 
            // printPreviewToolStripMenuItem
            // 
            printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            resources.ApplyResources(printPreviewToolStripMenuItem, "printPreviewToolStripMenuItem");
            printPreviewToolStripMenuItem.Click += printPreviewToolStripMenuItem_Click;
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            resources.ApplyResources(printToolStripMenuItem, "printToolStripMenuItem");
            printToolStripMenuItem.Click += printToolStripMenuItem_Click;
            // 
            // optionToolStripMenuItem
            // 
            optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripSeparator4, dynamicCompressionToolStripMenuItem, toolStripSeparator5 });
            optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            resources.ApplyResources(optionToolStripMenuItem, "optionToolStripMenuItem");
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
            // 
            // dynamicCompressionToolStripMenuItem
            // 
            dynamicCompressionToolStripMenuItem.Name = "dynamicCompressionToolStripMenuItem";
            resources.ApplyResources(dynamicCompressionToolStripMenuItem, "dynamicCompressionToolStripMenuItem");
            dynamicCompressionToolStripMenuItem.Click += dynamicCompressionToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
            // 
            // presetToolStripMenuItem
            // 
            captureExtender.SetCapture(presetToolStripMenuItem, true);
            presetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { electron300KVToolStripMenuItem, electron200KVToolStripMenuItem, electron120KeVToolStripMenuItem, toolStripSeparator7, xray30KeVToolStripMenuItem, xray20KeVToolStripMenuItem, xrayMoKαToolStripMenuItem, xrayCuKαToolStripMenuItem });
            presetToolStripMenuItem.Name = "presetToolStripMenuItem";
            resources.ApplyResources(presetToolStripMenuItem, "presetToolStripMenuItem");
            // 
            // electron300KVToolStripMenuItem
            // 
            electron300KVToolStripMenuItem.Name = "electron300KVToolStripMenuItem";
            resources.ApplyResources(electron300KVToolStripMenuItem, "electron300KVToolStripMenuItem");
            electron300KVToolStripMenuItem.Click += presetToolStripMenuItem_Click;
            // 
            // electron200KVToolStripMenuItem
            // 
            electron200KVToolStripMenuItem.Name = "electron200KVToolStripMenuItem";
            resources.ApplyResources(electron200KVToolStripMenuItem, "electron200KVToolStripMenuItem");
            electron200KVToolStripMenuItem.Click += presetToolStripMenuItem_Click;
            // 
            // electron120KeVToolStripMenuItem
            // 
            electron120KeVToolStripMenuItem.Name = "electron120KeVToolStripMenuItem";
            resources.ApplyResources(electron120KeVToolStripMenuItem, "electron120KeVToolStripMenuItem");
            electron120KeVToolStripMenuItem.Click += presetToolStripMenuItem_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(toolStripSeparator7, "toolStripSeparator7");
            // 
            // xray30KeVToolStripMenuItem
            // 
            xray30KeVToolStripMenuItem.Name = "xray30KeVToolStripMenuItem";
            resources.ApplyResources(xray30KeVToolStripMenuItem, "xray30KeVToolStripMenuItem");
            xray30KeVToolStripMenuItem.Click += presetToolStripMenuItem_Click;
            // 
            // xray20KeVToolStripMenuItem
            // 
            xray20KeVToolStripMenuItem.Name = "xray20KeVToolStripMenuItem";
            resources.ApplyResources(xray20KeVToolStripMenuItem, "xray20KeVToolStripMenuItem");
            xray20KeVToolStripMenuItem.Click += presetToolStripMenuItem_Click;
            // 
            // xrayMoKαToolStripMenuItem
            // 
            xrayMoKαToolStripMenuItem.Name = "xrayMoKαToolStripMenuItem";
            resources.ApplyResources(xrayMoKαToolStripMenuItem, "xrayMoKαToolStripMenuItem");
            xrayMoKαToolStripMenuItem.Click += presetToolStripMenuItem_Click;
            // 
            // xrayCuKαToolStripMenuItem
            // 
            xrayCuKαToolStripMenuItem.Name = "xrayCuKαToolStripMenuItem";
            resources.ApplyResources(xrayCuKαToolStripMenuItem, "xrayCuKαToolStripMenuItem");
            xrayCuKαToolStripMenuItem.Click += presetToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { basicConceptOfBethesMethodToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // basicConceptOfBethesMethodToolStripMenuItem
            // 
            basicConceptOfBethesMethodToolStripMenuItem.Name = "basicConceptOfBethesMethodToolStripMenuItem";
            resources.ApplyResources(basicConceptOfBethesMethodToolStripMenuItem, "basicConceptOfBethesMethodToolStripMenuItem");
            basicConceptOfBethesMethodToolStripMenuItem.Click += basicConceptOfBethesMethodToolStripMenuItem_Click;
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            waveLengthControl.Energy = 199.99999993D;
            waveLengthControl.Name = "waveLengthControl";
            toolTip.SetToolTip(waveLengthControl, resources.GetString("waveLengthControl.ToolTip"));
            waveLengthControl.WaveLength = 0.0025079347460000003D;
            waveLengthControl.WaveSource = WaveSource.Electron;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            waveLengthControl.WavelengthChanged += waveLengthControl_WavelengthChanged;
            waveLengthControl.WaveSourceChanged += WaveLengthControl_WaveSourceChanged;
            // 
            // radioButtonIntensityDynamical
            // 
            resources.ApplyResources(radioButtonIntensityDynamical, "radioButtonIntensityDynamical");
            radioButtonIntensityDynamical.Name = "radioButtonIntensityDynamical";
            toolTip.SetToolTip(radioButtonIntensityDynamical, resources.GetString("radioButtonIntensityDynamical.ToolTip"));
            radioButtonIntensityDynamical.UseVisualStyleBackColor = true;
            radioButtonIntensityDynamical.CheckedChanged += radioButtonIntensityCalculationMethod_CheckedChanged;
            // 
            // checkBoxUseCrystalColor
            // 
            resources.ApplyResources(checkBoxUseCrystalColor, "checkBoxUseCrystalColor");
            checkBoxUseCrystalColor.Name = "checkBoxUseCrystalColor";
            toolTip.SetToolTip(checkBoxUseCrystalColor, resources.GetString("checkBoxUseCrystalColor.ToolTip"));
            checkBoxUseCrystalColor.CheckedChanged += checkBoxUseCrystalColor_CheckedChanged;
            // 
            // checkBoxAnomalousDispersion
            // 
            resources.ApplyResources(checkBoxAnomalousDispersion, "checkBoxAnomalousDispersion");
            checkBoxAnomalousDispersion.Checked = true;
            checkBoxAnomalousDispersion.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxAnomalousDispersion.Name = "checkBoxAnomalousDispersion";
            toolTip.SetToolTip(checkBoxAnomalousDispersion, resources.GetString("checkBoxAnomalousDispersion.ToolTip"));
            checkBoxAnomalousDispersion.UseVisualStyleBackColor = true;
            checkBoxAnomalousDispersion.CheckedChanged += checkBoxAnomalousDispersion_CheckedChanged;
            // 
            // checkBoxExtinctionAll
            // 
            resources.ApplyResources(checkBoxExtinctionAll, "checkBoxExtinctionAll");
            checkBoxExtinctionAll.Name = "checkBoxExtinctionAll";
            toolTip.SetToolTip(checkBoxExtinctionAll, resources.GetString("checkBoxExtinctionAll.ToolTip"));
            checkBoxExtinctionAll.CheckedChanged += CheckBoxExtinctionAll_CheckedChanged;
            // 
            // checkBoxExtinctionLattice
            // 
            resources.ApplyResources(checkBoxExtinctionLattice, "checkBoxExtinctionLattice");
            checkBoxExtinctionLattice.Checked = true;
            checkBoxExtinctionLattice.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxExtinctionLattice.Name = "checkBoxExtinctionLattice";
            toolTip.SetToolTip(checkBoxExtinctionLattice, resources.GetString("checkBoxExtinctionLattice.ToolTip"));
            checkBoxExtinctionLattice.CheckedChanged += CheckBoxExtinctionAll_CheckedChanged;
            // 
            // groupBoxSpotProperty
            // 
            groupBoxSpotProperty.Controls.Add(panelSimulationOptions);
            resources.ApplyResources(groupBoxSpotProperty, "groupBoxSpotProperty");
            groupBoxSpotProperty.Name = "groupBoxSpotProperty";
            groupBoxSpotProperty.TabStop = false;
            // 
            // panelSimulationOptions
            // 
            resources.ApplyResources(panelSimulationOptions, "panelSimulationOptions");
            panelSimulationOptions.Controls.Add(flowLayoutPanelPED);
            panelSimulationOptions.Controls.Add(flowLayoutPanelBethe);
            panelSimulationOptions.Controls.Add(flowLayoutPanelAppearance);
            panelSimulationOptions.Controls.Add(flowLayoutPanelIntensity);
            panelSimulationOptions.Controls.Add(flowLayoutPanelBeamMode);
            panelSimulationOptions.Controls.Add(flowLayoutPanelWaveLength);
            panelSimulationOptions.Name = "panelSimulationOptions";
            // 
            // flowLayoutPanelPED
            // 
            resources.ApplyResources(flowLayoutPanelPED, "flowLayoutPanelPED");
            flowLayoutPanelPED.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            captureExtender.SetCapture(flowLayoutPanelPED, true);
            flowLayoutPanelPED.Controls.Add(label5);
            flowLayoutPanelPED.Controls.Add(numericBoxPED_Semiangle);
            flowLayoutPanelPED.Controls.Add(numericBoxPED_Step);
            flowLayoutPanelPED.Name = "flowLayoutPanelPED";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // numericBoxPED_Semiangle
            // 
            numericBoxPED_Semiangle.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxPED_Semiangle, "numericBoxPED_Semiangle");
            numericBoxPED_Semiangle.Maximum = 500D;
            numericBoxPED_Semiangle.Minimum = 0.1D;
            numericBoxPED_Semiangle.Name = "numericBoxPED_Semiangle";
            numericBoxPED_Semiangle.RadianValue = 0.87266462599716477D;
            numericBoxPED_Semiangle.ShowUpDown = true;
            numericBoxPED_Semiangle.SmartIncrement = true;
            numericBoxPED_Semiangle.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxPED_Semiangle, resources.GetString("numericBoxPED_Semiangle.ToolTip"));
            numericBoxPED_Semiangle.Value = 50D;
            numericBoxPED_Semiangle.ValueChanged += Draw;
            // 
            // numericBoxPED_Step
            // 
            numericBoxPED_Step.BackColor = System.Drawing.SystemColors.Control;
            numericBoxPED_Step.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxPED_Step, "numericBoxPED_Step");
            numericBoxPED_Step.Maximum = 1080D;
            numericBoxPED_Step.Minimum = 2D;
            numericBoxPED_Step.Name = "numericBoxPED_Step";
            numericBoxPED_Step.RadianValue = 0.62831853071795862D;
            numericBoxPED_Step.ShowUpDown = true;
            numericBoxPED_Step.SmartIncrement = true;
            numericBoxPED_Step.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxPED_Step, resources.GetString("numericBoxPED_Step.ToolTip"));
            numericBoxPED_Step.Value = 36D;
            numericBoxPED_Step.ValueChanged += Draw;
            // 
            // flowLayoutPanelBethe
            // 
            resources.ApplyResources(flowLayoutPanelBethe, "flowLayoutPanelBethe");
            flowLayoutPanelBethe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            captureExtender.SetCapture(flowLayoutPanelBethe, true);
            flowLayoutPanelBethe.Controls.Add(label1);
            flowLayoutPanelBethe.Controls.Add(numericBoxNumOfBlochWave);
            flowLayoutPanelBethe.Controls.Add(numericBoxThickness);
            flowLayoutPanelBethe.Name = "flowLayoutPanelBethe";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // numericBoxNumOfBlochWave
            // 
            numericBoxNumOfBlochWave.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(numericBoxNumOfBlochWave, "numericBoxNumOfBlochWave");
            numericBoxNumOfBlochWave.Maximum = 1000D;
            numericBoxNumOfBlochWave.Minimum = 8D;
            numericBoxNumOfBlochWave.Name = "numericBoxNumOfBlochWave";
            numericBoxNumOfBlochWave.RadianValue = 4.1887902047863905D;
            numericBoxNumOfBlochWave.ShowUpDown = true;
            numericBoxNumOfBlochWave.SmartIncrement = true;
            numericBoxNumOfBlochWave.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxNumOfBlochWave, resources.GetString("numericBoxNumOfBlochWave.ToolTip"));
            numericBoxNumOfBlochWave.Value = 240D;
            numericBoxNumOfBlochWave.ValueChanged += Draw;
            // 
            // numericBoxThickness
            // 
            numericBoxThickness.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThickness.DecimalPlaces = 2;
            resources.ApplyResources(numericBoxThickness, "numericBoxThickness");
            numericBoxThickness.Maximum = 10000D;
            numericBoxThickness.Minimum = 0.01D;
            numericBoxThickness.Name = "numericBoxThickness";
            numericBoxThickness.RadianValue = 0.87266462599716477D;
            numericBoxThickness.ShowUpDown = true;
            numericBoxThickness.SkipEventDuringInput = false;
            numericBoxThickness.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThickness, resources.GetString("numericBoxThickness.ToolTip"));
            numericBoxThickness.UpDown_Increment = 10D;
            numericBoxThickness.Value = 50D;
            numericBoxThickness.ValueChanged += Draw;
            // 
            // flowLayoutPanelAppearance
            // 
            resources.ApplyResources(flowLayoutPanelAppearance, "flowLayoutPanelAppearance");
            flowLayoutPanelAppearance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            captureExtender.SetCapture(flowLayoutPanelAppearance, true);
            flowLayoutPanelAppearance.Controls.Add(label19);
            flowLayoutPanelAppearance.Controls.Add(flowLayoutPanelSpotShape);
            flowLayoutPanelAppearance.Controls.Add(flowLayoutPanelSpotOpacity);
            flowLayoutPanelAppearance.Controls.Add(numericBoxSpotRadius);
            flowLayoutPanelAppearance.Controls.Add(checkBoxDrawSameSize);
            flowLayoutPanelAppearance.Controls.Add(flowLayoutPanelGaussianOption);
            flowLayoutPanelAppearance.Controls.Add(flowLayoutPanelSpotColor);
            flowLayoutPanelAppearance.Name = "flowLayoutPanelAppearance";
            flowLayoutPanelAppearance.Paint += flowLayoutPanelAppearance_Paint;
            // 
            // label19
            // 
            resources.ApplyResources(label19, "label19");
            label19.Name = "label19";
            toolTip.SetToolTip(label19, resources.GetString("label19.ToolTip"));
            // 
            // flowLayoutPanelSpotShape
            // 
            resources.ApplyResources(flowLayoutPanelSpotShape, "flowLayoutPanelSpotShape");
            flowLayoutPanelSpotShape.Controls.Add(radioButtonCircleArea);
            flowLayoutPanelSpotShape.Controls.Add(radioButtonPointSpread);
            flowLayoutPanelSpotShape.Name = "flowLayoutPanelSpotShape";
            // 
            // radioButtonCircleArea
            // 
            resources.ApplyResources(radioButtonCircleArea, "radioButtonCircleArea");
            radioButtonCircleArea.Checked = true;
            radioButtonCircleArea.Name = "radioButtonCircleArea";
            radioButtonCircleArea.TabStop = true;
            toolTip.SetToolTip(radioButtonCircleArea, resources.GetString("radioButtonCircleArea.ToolTip"));
            radioButtonCircleArea.UseVisualStyleBackColor = true;
            radioButtonCircleArea.CheckedChanged += radioButtonPointSpread_CheckedChanged;
            // 
            // radioButtonPointSpread
            // 
            resources.ApplyResources(radioButtonPointSpread, "radioButtonPointSpread");
            radioButtonPointSpread.Name = "radioButtonPointSpread";
            toolTip.SetToolTip(radioButtonPointSpread, resources.GetString("radioButtonPointSpread.ToolTip"));
            radioButtonPointSpread.UseVisualStyleBackColor = true;
            radioButtonPointSpread.CheckedChanged += radioButtonPointSpread_CheckedChanged;
            // 
            // flowLayoutPanelSpotOpacity
            // 
            resources.ApplyResources(flowLayoutPanelSpotOpacity, "flowLayoutPanelSpotOpacity");
            flowLayoutPanelSpotOpacity.Controls.Add(label8);
            flowLayoutPanelSpotOpacity.Controls.Add(trackBarSpotOpacity);
            flowLayoutPanelSpotOpacity.Name = "flowLayoutPanelSpotOpacity";
            toolTip.SetToolTip(flowLayoutPanelSpotOpacity, resources.GetString("flowLayoutPanelSpotOpacity.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            toolTip.SetToolTip(label8, resources.GetString("label8.ToolTip"));
            // 
            // trackBarSpotOpacity
            // 
            resources.ApplyResources(trackBarSpotOpacity, "trackBarSpotOpacity");
            trackBarSpotOpacity.LargeChange = 20;
            trackBarSpotOpacity.Maximum = 100;
            trackBarSpotOpacity.Name = "trackBarSpotOpacity";
            trackBarSpotOpacity.SmallChange = 10;
            trackBarSpotOpacity.TickFrequency = 500;
            trackBarSpotOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarSpotOpacity, resources.GetString("trackBarSpotOpacity.ToolTip"));
            trackBarSpotOpacity.Value = 100;
            trackBarSpotOpacity.ValueChanged += Draw;
            // 
            // numericBoxSpotRadius
            // 
            numericBoxSpotRadius.BackColor = System.Drawing.SystemColors.Control;
            numericBoxSpotRadius.DecimalPlaces = 4;
            resources.ApplyResources(numericBoxSpotRadius, "numericBoxSpotRadius");
            numericBoxSpotRadius.Maximum = 1D;
            numericBoxSpotRadius.Minimum = 0.01D;
            numericBoxSpotRadius.Name = "numericBoxSpotRadius";
            numericBoxSpotRadius.RadianValue = 0.0034906585039886592D;
            numericBoxSpotRadius.ShowUpDown = true;
            numericBoxSpotRadius.SmartIncrement = true;
            numericBoxSpotRadius.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxSpotRadius, resources.GetString("numericBoxSpotRadius.ToolTip"));
            numericBoxSpotRadius.UpDown_Increment = 0.01D;
            numericBoxSpotRadius.Value = 0.2D;
            numericBoxSpotRadius.ValueChanged += Draw;
            // 
            // checkBoxDrawSameSize
            // 
            resources.ApplyResources(checkBoxDrawSameSize, "checkBoxDrawSameSize");
            checkBoxDrawSameSize.Name = "checkBoxDrawSameSize";
            toolTip.SetToolTip(checkBoxDrawSameSize, resources.GetString("checkBoxDrawSameSize.ToolTip"));
            checkBoxDrawSameSize.UseVisualStyleBackColor = true;
            checkBoxDrawSameSize.CheckedChanged += Draw;
            // 
            // flowLayoutPanelGaussianOption
            // 
            resources.ApplyResources(flowLayoutPanelGaussianOption, "flowLayoutPanelGaussianOption");
            flowLayoutPanelGaussianOption.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            flowLayoutPanelGaussianOption.Controls.Add(flowLayoutPanelPointSpreadIntensity);
            flowLayoutPanelGaussianOption.Controls.Add(flowLayoutPanelScaleColor);
            flowLayoutPanelGaussianOption.Controls.Add(checkBoxLogScale);
            flowLayoutPanelGaussianOption.Name = "flowLayoutPanelGaussianOption";
            // 
            // flowLayoutPanelPointSpreadIntensity
            // 
            flowLayoutPanelPointSpreadIntensity.Controls.Add(label10);
            flowLayoutPanelPointSpreadIntensity.Controls.Add(trackBarIntensityForPointSpread);
            resources.ApplyResources(flowLayoutPanelPointSpreadIntensity, "flowLayoutPanelPointSpreadIntensity");
            flowLayoutPanelPointSpreadIntensity.Name = "flowLayoutPanelPointSpreadIntensity";
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            toolTip.SetToolTip(label10, resources.GetString("label10.ToolTip"));
            // 
            // trackBarIntensityForPointSpread
            // 
            resources.ApplyResources(trackBarIntensityForPointSpread, "trackBarIntensityForPointSpread");
            trackBarIntensityForPointSpread.LargeChange = 50;
            trackBarIntensityForPointSpread.Maximum = 800;
            trackBarIntensityForPointSpread.Minimum = 1;
            trackBarIntensityForPointSpread.Name = "trackBarIntensityForPointSpread";
            trackBarIntensityForPointSpread.SmallChange = 10;
            trackBarIntensityForPointSpread.TickFrequency = 500;
            trackBarIntensityForPointSpread.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarIntensityForPointSpread, resources.GetString("trackBarIntensityForPointSpread.ToolTip"));
            trackBarIntensityForPointSpread.Value = 400;
            trackBarIntensityForPointSpread.ValueChanged += Draw;
            // 
            // flowLayoutPanelScaleColor
            // 
            flowLayoutPanelScaleColor.Controls.Add(label25);
            flowLayoutPanelScaleColor.Controls.Add(comboBoxScaleColorScale);
            resources.ApplyResources(flowLayoutPanelScaleColor, "flowLayoutPanelScaleColor");
            flowLayoutPanelScaleColor.Name = "flowLayoutPanelScaleColor";
            // 
            // label25
            // 
            resources.ApplyResources(label25, "label25");
            label25.Name = "label25";
            toolTip.SetToolTip(label25, resources.GetString("label25.ToolTip"));
            // 
            // comboBoxScaleColorScale
            // 
            comboBoxScaleColorScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(comboBoxScaleColorScale, "comboBoxScaleColorScale");
            comboBoxScaleColorScale.FormattingEnabled = true;
            comboBoxScaleColorScale.Items.AddRange(new object[] { resources.GetString("comboBoxScaleColorScale.Items"), resources.GetString("comboBoxScaleColorScale.Items1"), resources.GetString("comboBoxScaleColorScale.Items2"), resources.GetString("comboBoxScaleColorScale.Items3") });
            comboBoxScaleColorScale.Name = "comboBoxScaleColorScale";
            toolTip.SetToolTip(comboBoxScaleColorScale, resources.GetString("comboBoxScaleColorScale.ToolTip"));
            comboBoxScaleColorScale.SelectedIndexChanged += comboBoxScaleColorScale_SelectedIndexChanged;
            // 
            // checkBoxLogScale
            // 
            resources.ApplyResources(checkBoxLogScale, "checkBoxLogScale");
            checkBoxLogScale.Name = "checkBoxLogScale";
            toolTip.SetToolTip(checkBoxLogScale, resources.GetString("checkBoxLogScale.ToolTip"));
            checkBoxLogScale.UseVisualStyleBackColor = true;
            checkBoxLogScale.CheckedChanged += Draw;
            // 
            // flowLayoutPanelSpotColor
            // 
            resources.ApplyResources(flowLayoutPanelSpotColor, "flowLayoutPanelSpotColor");
            flowLayoutPanelSpotColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            flowLayoutPanelSpotColor.Controls.Add(label2);
            flowLayoutPanelSpotColor.Controls.Add(checkBoxUseCrystalColor);
            flowLayoutPanelSpotColor.Controls.Add(colorControlNoCondition);
            flowLayoutPanelSpotColor.Controls.Add(colorControlScrewGlide);
            flowLayoutPanelSpotColor.Controls.Add(colorControlForbiddenLattice);
            flowLayoutPanelSpotColor.Name = "flowLayoutPanelSpotColor";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // colorControlNoCondition
            // 
            resources.ApplyResources(colorControlNoCondition, "colorControlNoCondition");
            colorControlNoCondition.BackColor = System.Drawing.SystemColors.Control;
            colorControlNoCondition.BoxSize = new System.Drawing.Size(20, 20);
            colorControlNoCondition.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlNoCondition.Name = "colorControlNoCondition";
            toolTip.SetToolTip(colorControlNoCondition, resources.GetString("colorControlNoCondition.ToolTip1"));
            colorControlNoCondition.ColorChanged += Draw;
            // 
            // colorControlScrewGlide
            // 
            resources.ApplyResources(colorControlScrewGlide, "colorControlScrewGlide");
            colorControlScrewGlide.BackColor = System.Drawing.SystemColors.Control;
            colorControlScrewGlide.BoxSize = new System.Drawing.Size(20, 20);
            colorControlScrewGlide.Color = System.Drawing.Color.FromArgb(255, 192, 192);
            colorControlScrewGlide.Name = "colorControlScrewGlide";
            toolTip.SetToolTip(colorControlScrewGlide, resources.GetString("colorControlScrewGlide.ToolTip1"));
            colorControlScrewGlide.ColorChanged += Draw;
            // 
            // colorControlForbiddenLattice
            // 
            resources.ApplyResources(colorControlForbiddenLattice, "colorControlForbiddenLattice");
            colorControlForbiddenLattice.BackColor = System.Drawing.SystemColors.Control;
            colorControlForbiddenLattice.BoxSize = new System.Drawing.Size(20, 20);
            colorControlForbiddenLattice.Color = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControlForbiddenLattice.Name = "colorControlForbiddenLattice";
            toolTip.SetToolTip(colorControlForbiddenLattice, resources.GetString("colorControlForbiddenLattice.ToolTip1"));
            colorControlForbiddenLattice.ColorChanged += Draw;
            // 
            // flowLayoutPanelIntensity
            // 
            resources.ApplyResources(flowLayoutPanelIntensity, "flowLayoutPanelIntensity");
            flowLayoutPanelIntensity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            captureExtender.SetCapture(flowLayoutPanelIntensity, true);
            flowLayoutPanelIntensity.Controls.Add(label7);
            flowLayoutPanelIntensity.Controls.Add(radioButtonIntensityExcitation);
            flowLayoutPanelIntensity.Controls.Add(flowLayoutPanelExtinctionOption);
            flowLayoutPanelIntensity.Controls.Add(radioButtonIntensityKinematical);
            flowLayoutPanelIntensity.Controls.Add(checkBoxAnomalousDispersion);
            flowLayoutPanelIntensity.Controls.Add(radioButtonIntensityDynamical);
            flowLayoutPanelIntensity.Controls.Add(buttonDetailsOfSpots);
            flowLayoutPanelIntensity.Name = "flowLayoutPanelIntensity";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip"));
            // 
            // radioButtonIntensityExcitation
            // 
            resources.ApplyResources(radioButtonIntensityExcitation, "radioButtonIntensityExcitation");
            radioButtonIntensityExcitation.Name = "radioButtonIntensityExcitation";
            toolTip.SetToolTip(radioButtonIntensityExcitation, resources.GetString("radioButtonIntensityExcitation.ToolTip"));
            radioButtonIntensityExcitation.UseVisualStyleBackColor = true;
            radioButtonIntensityExcitation.CheckedChanged += radioButtonIntensityCalculationMethod_CheckedChanged;
            // 
            // flowLayoutPanelExtinctionOption
            // 
            resources.ApplyResources(flowLayoutPanelExtinctionOption, "flowLayoutPanelExtinctionOption");
            flowLayoutPanelExtinctionOption.Controls.Add(checkBoxExtinctionAll);
            flowLayoutPanelExtinctionOption.Controls.Add(checkBoxExtinctionLattice);
            flowLayoutPanelExtinctionOption.Name = "flowLayoutPanelExtinctionOption";
            // 
            // radioButtonIntensityKinematical
            // 
            resources.ApplyResources(radioButtonIntensityKinematical, "radioButtonIntensityKinematical");
            radioButtonIntensityKinematical.Checked = true;
            radioButtonIntensityKinematical.Name = "radioButtonIntensityKinematical";
            radioButtonIntensityKinematical.TabStop = true;
            toolTip.SetToolTip(radioButtonIntensityKinematical, resources.GetString("radioButtonIntensityKinematical.ToolTip"));
            radioButtonIntensityKinematical.UseVisualStyleBackColor = true;
            radioButtonIntensityKinematical.CheckedChanged += radioButtonIntensityCalculationMethod_CheckedChanged;
            // 
            // buttonDetailsOfSpots
            // 
            resources.ApplyResources(buttonDetailsOfSpots, "buttonDetailsOfSpots");
            buttonDetailsOfSpots.Name = "buttonDetailsOfSpots";
            toolTip.SetToolTip(buttonDetailsOfSpots, resources.GetString("buttonDetailsOfSpots.ToolTip"));
            buttonDetailsOfSpots.UseVisualStyleBackColor = true;
            buttonDetailsOfSpots.Click += ButtonDetailsOfSpots_Click;
            // 
            // flowLayoutPanelBeamMode
            // 
            resources.ApplyResources(flowLayoutPanelBeamMode, "flowLayoutPanelBeamMode");
            flowLayoutPanelBeamMode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            captureExtender.SetCapture(flowLayoutPanelBeamMode, true);
            flowLayoutPanelBeamMode.Controls.Add(label13);
            flowLayoutPanelBeamMode.Controls.Add(radioButtonBeamParallel);
            flowLayoutPanelBeamMode.Controls.Add(radioButtonBeamPrecessionElectron);
            flowLayoutPanelBeamMode.Controls.Add(radioButtonBeamConvergence);
            flowLayoutPanelBeamMode.Controls.Add(radioButtonBeamBackLaue);
            flowLayoutPanelBeamMode.Controls.Add(radioButtonBeamPrecessionXray);
            flowLayoutPanelBeamMode.Name = "flowLayoutPanelBeamMode";
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip"));
            // 
            // radioButtonBeamParallel
            // 
            resources.ApplyResources(radioButtonBeamParallel, "radioButtonBeamParallel");
            radioButtonBeamParallel.Checked = true;
            radioButtonBeamParallel.Name = "radioButtonBeamParallel";
            radioButtonBeamParallel.TabStop = true;
            toolTip.SetToolTip(radioButtonBeamParallel, resources.GetString("radioButtonBeamParallel.ToolTip"));
            radioButtonBeamParallel.UseVisualStyleBackColor = true;
            radioButtonBeamParallel.CheckedChanged += radioButtonBeamParallel_CheckedChanged;
            // 
            // radioButtonBeamPrecessionElectron
            // 
            resources.ApplyResources(radioButtonBeamPrecessionElectron, "radioButtonBeamPrecessionElectron");
            radioButtonBeamPrecessionElectron.Name = "radioButtonBeamPrecessionElectron";
            toolTip.SetToolTip(radioButtonBeamPrecessionElectron, resources.GetString("radioButtonBeamPrecessionElectron.ToolTip"));
            radioButtonBeamPrecessionElectron.UseVisualStyleBackColor = true;
            radioButtonBeamPrecessionElectron.CheckedChanged += radioButtonBeamParallel_CheckedChanged;
            // 
            // radioButtonBeamConvergence
            // 
            resources.ApplyResources(radioButtonBeamConvergence, "radioButtonBeamConvergence");
            radioButtonBeamConvergence.Name = "radioButtonBeamConvergence";
            toolTip.SetToolTip(radioButtonBeamConvergence, resources.GetString("radioButtonBeamConvergence.ToolTip"));
            radioButtonBeamConvergence.UseVisualStyleBackColor = true;
            radioButtonBeamConvergence.CheckedChanged += radioButtonBeamParallel_CheckedChanged;
            // 
            // radioButtonBeamBackLaue
            // 
            resources.ApplyResources(radioButtonBeamBackLaue, "radioButtonBeamBackLaue");
            radioButtonBeamBackLaue.Name = "radioButtonBeamBackLaue";
            toolTip.SetToolTip(radioButtonBeamBackLaue, resources.GetString("radioButtonBeamBackLaue.ToolTip"));
            radioButtonBeamBackLaue.UseVisualStyleBackColor = true;
            radioButtonBeamBackLaue.CheckedChanged += radioButtonBeamParallel_CheckedChanged;
            // 
            // radioButtonBeamPrecessionXray
            // 
            resources.ApplyResources(radioButtonBeamPrecessionXray, "radioButtonBeamPrecessionXray");
            radioButtonBeamPrecessionXray.Name = "radioButtonBeamPrecessionXray";
            toolTip.SetToolTip(radioButtonBeamPrecessionXray, resources.GetString("radioButtonBeamPrecessionXray.ToolTip"));
            radioButtonBeamPrecessionXray.UseVisualStyleBackColor = true;
            radioButtonBeamPrecessionXray.CheckedChanged += radioButtonBeamParallel_CheckedChanged;
            // 
            // flowLayoutPanelWaveLength
            // 
            resources.ApplyResources(flowLayoutPanelWaveLength, "flowLayoutPanelWaveLength");
            flowLayoutPanelWaveLength.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            captureExtender.SetCapture(flowLayoutPanelWaveLength, true);
            flowLayoutPanelWaveLength.Controls.Add(label3);
            flowLayoutPanelWaveLength.Controls.Add(waveLengthControl);
            flowLayoutPanelWaveLength.Name = "flowLayoutPanelWaveLength";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip"));
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 100;
            // 
            // printPreviewDialog1
            // 
            resources.ApplyResources(printPreviewDialog1, "printPreviewDialog1");
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Name = "printPreviewDialog1";
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // pageSetupDialog1
            // 
            pageSetupDialog1.Document = printDocument1;
            pageSetupDialog1.ShowHelp = true;
            // 
            // printDialog1
            // 
            printDialog1.AllowCurrentPage = true;
            printDialog1.AllowSelection = true;
            printDialog1.AllowSomePages = true;
            printDialog1.Document = printDocument1;
            printDialog1.PrintToFile = true;
            printDialog1.UseEXDialog = true;
            // 
            // timerBlinkSpot
            // 
            timerBlinkSpot.Interval = 400;
            timerBlinkSpot.Tag = "true";
            timerBlinkSpot.Tick += timerBlinkSpot_Tick;
            // 
            // timerBlinkKikuchiLine
            // 
            timerBlinkKikuchiLine.Interval = 400;
            timerBlinkKikuchiLine.Tick += timerBlinkKikuchiLine_Tick;
            // 
            // timerBlinkDebyeRing
            // 
            timerBlinkDebyeRing.Interval = 400;
            timerBlinkDebyeRing.Tick += timerBlinkDebyering_Tick;
            // 
            // timerBlinkScale
            // 
            timerBlinkScale.Interval = 400;
            timerBlinkScale.Tag = "";
            timerBlinkScale.Tick += timerBlinkScale_Tick;
            // 
            // FormDiffractionSimulator
            // 
            AllowDrop = true;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(toolStripContainer1);
            Controls.Add(panel1);
            Controls.Add(groupBoxSpotProperty);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "FormDiffractionSimulator";
            FormClosing += FormElectronDiffraction_FormClosing;
            Load += FormElectronDiffraction_Load;
            ResizeBegin += FormDiffractionSimulator_ResizeBegin;
            ResizeEnd += FormElectronDiffraction_ResizeEnd;
            VisibleChanged += FormElectronDiffraction_VisibleChanged;
            DragDrop += FormDiffractionSimulator_DragDrop;
            DragEnter += FormDiffractionSimulator_DragEnter;
            Paint += FormDiffractionSimulator_Paint;
            toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            toolStripContainer1.BottomToolStripPanel.PerformLayout();
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.ContentPanel.PerformLayout();
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            toolStrip3.ResumeLayout(false);
            toolStrip3.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panelMain.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabPageGeneral.ResumeLayout(false);
            groupBoxStringSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            groupBoxColor.ResumeLayout(false);
            groupBoxColor.PerformLayout();
            tabPageKikuchi.ResumeLayout(false);
            tabPageKikuchi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).EndInit();
            tabPageDebye.ResumeLayout(false);
            tabPageDebye.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarDebyeRingWidth).EndInit();
            tabPageScale.ResumeLayout(false);
            tabPageScale.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarScaleLineWidth).EndInit();
            flowLayoutPanelScaleDivision.ResumeLayout(false);
            flowLayoutPanelScaleDivision.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            groupBoxOptions.ResumeLayout(false);
            groupBoxOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar3D_Transparency).EndInit();
            groupBoxViewDirection.ResumeLayout(false);
            groupBoxViewDirection.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            panelViewStep.ResumeLayout(false);
            panelReciprocalSpace.ResumeLayout(false);
            panelReciprocalSpace.PerformLayout();
            panelMousePosition.ResumeLayout(false);
            panelMousePosition.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanelMousePotionDetailed.ResumeLayout(false);
            tableLayoutPanelMousePotionDetailed.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            groupBoxMonitor.ResumeLayout(false);
            groupBoxMonitor.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanelResolutionUnit.ResumeLayout(false);
            flowLayoutPanelResolutionUnit.PerformLayout();
            panelDetectorAndMisc.ResumeLayout(false);
            groupBoxMisc.ResumeLayout(false);
            groupBoxMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarRotationSpeed).EndInit();
            groupBoxDetectorGeometry.ResumeLayout(false);
            groupBoxDetectorGeometry.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            groupBoxDeveloperCode.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBoxSpotProperty.ResumeLayout(false);
            panelSimulationOptions.ResumeLayout(false);
            panelSimulationOptions.PerformLayout();
            flowLayoutPanelPED.ResumeLayout(false);
            flowLayoutPanelPED.PerformLayout();
            flowLayoutPanelBethe.ResumeLayout(false);
            flowLayoutPanelBethe.PerformLayout();
            flowLayoutPanelAppearance.ResumeLayout(false);
            flowLayoutPanelAppearance.PerformLayout();
            flowLayoutPanelSpotShape.ResumeLayout(false);
            flowLayoutPanelSpotShape.PerformLayout();
            flowLayoutPanelSpotOpacity.ResumeLayout(false);
            flowLayoutPanelSpotOpacity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarSpotOpacity).EndInit();
            flowLayoutPanelGaussianOption.ResumeLayout(false);
            flowLayoutPanelGaussianOption.PerformLayout();
            flowLayoutPanelPointSpreadIntensity.ResumeLayout(false);
            flowLayoutPanelPointSpreadIntensity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityForPointSpread).EndInit();
            flowLayoutPanelScaleColor.ResumeLayout(false);
            flowLayoutPanelScaleColor.PerformLayout();
            flowLayoutPanelSpotColor.ResumeLayout(false);
            flowLayoutPanelSpotColor.PerformLayout();
            flowLayoutPanelIntensity.ResumeLayout(false);
            flowLayoutPanelIntensity.PerformLayout();
            flowLayoutPanelExtinctionOption.ResumeLayout(false);
            flowLayoutPanelExtinctionOption.PerformLayout();
            flowLayoutPanelBeamMode.ResumeLayout(false);
            flowLayoutPanelBeamMode.PerformLayout();
            flowLayoutPanelWaveLength.ResumeLayout(false);
            flowLayoutPanelWaveLength.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxExtinctionAll;
        private System.Windows.Forms.CheckBox checkBoxExtinctionLattice;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar trackBarLineWidth;
        private System.Windows.Forms.TrackBar trackBarStrSize;
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
        private System.Windows.Forms.GroupBox groupBoxDetectorGeometry;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTimeForSearchingG;
        private System.Windows.Forms.TrackBar trackBarRotationSpeed;
        public WaveLengthControl waveLengthControl;
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
        public NumericBox numericBoxResolution;
        // public ImagingSolution.Control.GraphicsBox graphicsBox; // (260322Ch) 旧 GraphicsBox 型
        // public Crystallography.Controls.GraphicBox2 graphicsBox; // (260322Ch) 仮名 GraphicBox2
        public Crystallography.Controls.GraphicsBox graphicsBox; // (260322Ch) 正式名 GraphicBox へ移行
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
        private System.Windows.Forms.Label labelTwoThetaDeg;
        private System.Windows.Forms.Timer timerBlinkSpot;
        private System.Windows.Forms.Timer timerBlinkKikuchiLine;
        private System.Windows.Forms.Timer timerBlinkDebyeRing;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelIntensity;
        private System.Windows.Forms.Label label7;
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
        private System.Windows.Forms.CheckBox checkBoxAnomalousDispersion;//260606Cl 追加: X線異常分散トグル
        private System.Windows.Forms.CheckBox checkBoxLogScale;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBethe;
        private System.Windows.Forms.ToolStripMenuItem dynamicCompressionToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButtonIntensityKinematical;
        public System.Windows.Forms.RadioButton radioButtonIntensityDynamical;
        private System.Windows.Forms.ToolStripMenuItem saveCBEDasPngToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxFixCenter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem saveCBEDasTiffToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTimeForBethe;
        // 260521Cl: numericBoxClientWidth/Height は sizeControl1 へ置換したため削除
        private System.Windows.Forms.Button button1;
        private NumericBox numericBoxDev;
        private NumericBox numericBoxAcc;
        private NumericBox numericBoxPED_Semiangle;
        private NumericBox numericBoxPED_Step;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPED;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBeamMode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.RadioButton radioButtonBeamParallel;
        private System.Windows.Forms.RadioButton radioButtonBeamPrecessionElectron;
        public System.Windows.Forms.RadioButton radioButtonBeamConvergence;
        private System.Windows.Forms.Label labelDinv;
        private System.Windows.Forms.Button buttonDetailsOfSpots;
        public NumericBox numericBoxNumOfBlochWave;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.ComboBox comboBoxScaleColorScale;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem basicConceptOfBethesMethodToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBoxColor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSpotShape;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGaussianOption;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPointSpreadIntensity;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelScaleColor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSpotOpacity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private NumericBox numericBoxSpotRadius;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExtinctionOption;
        private System.Windows.Forms.GroupBox groupBoxStringSize;
        public ColorControl colorControlOrigin;
        public ColorControl colorControlNoCondition;
        public ColorControl colorControlForbiddenLattice;
        public ColorControl colorControlScrewGlide;
        private System.Windows.Forms.Label label14;
        public ColorControl colorControlString;
        public ColorControl colorControlFoot;
        public ColorControl colorControlBackGround;
        public ColorControl colorControlExcessLine;
        public ColorControl colorControlDebyeRing;
        public NumericBox numericBoxThickness;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSpotColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPageScale;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton toolStripButtonScale;
        private System.Windows.Forms.CheckBox checkBoxScaleLabel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar trackBarScaleLineWidth;
        private System.Windows.Forms.Label label16;
        private ColorControl colorControlScale2Theta;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelScaleDivision;
        private System.Windows.Forms.RadioButton radioButtonScaleDivisionFine;
        private System.Windows.Forms.RadioButton radioButtonScaleDivisionMedium;
        private System.Windows.Forms.RadioButton radioButtonScaleDivisionCoarse;
        private System.Windows.Forms.Timer timerBlinkScale;
        private ColorControl colorControlScaleAzimuth;
        private System.Windows.Forms.Label labelMousePositionReal;
        private System.Windows.Forms.GroupBox groupBoxMonitor;
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem copyCBEDasImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asPixelByPixelImagePNGFormatToolStripMenuItem;
        private System.Windows.Forms.Panel panelSimulationOptions;
        private System.Windows.Forms.Label labelTwoThetaRad;
        private System.Windows.Forms.CheckBox checkBoxKikuchiLine_Kinematical;
        private System.Windows.Forms.ToolStripMenuItem asCollectiveImageTiffFormatToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelWaveLength;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonBeamPrecessionXray;
        private System.Windows.Forms.ComboBox comboBoxCenter;
        public System.Windows.Forms.ToolStripMenuItem saveCBEDPatternToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem copyCBEDPatternToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonDspacingInv;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMousePotionDetailed;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelReciprocalSpace;
        private System.Windows.Forms.CheckBox checkBoxReciprocalSpace;
        private System.Windows.Forms.GroupBox groupBoxOptions;
        private System.Windows.Forms.CheckBox checkBox3D_EwaldSphere;
        private System.Windows.Forms.GroupBox groupBoxViewDirection;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button buttonAntiClock;
        private System.Windows.Forms.Button buttonClock;
        private System.Windows.Forms.Button buttonTopLeft;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonBottomLeft;
        private System.Windows.Forms.Button buttonBottom;
        private System.Windows.Forms.Button buttonBottomRight;
        private System.Windows.Forms.Button buttonTop;
        private System.Windows.Forms.Button buttonTopRight;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.Panel panelViewStep;
        private NumericBox numericBoxStep;
        private System.Windows.Forms.Button buttonResetAngle;
        private System.Windows.Forms.CheckBox checkBox3D_ShowIndices;
        public ColorControl colorControl3D_EwaldSphere;
        public ColorControl colorControl3D_Origin;
        public ColorControl colorControl3D_SpotsFar;
        public ColorControl colorControl3D_SpotsNear;
        public ColorControl colorControl3D_Background;
        public ColorControl colorControl3D_lText;
        private System.Windows.Forms.TrackBar trackBar3D_Transparency;
        private System.Windows.Forms.CheckBox checkBox3D_MakeSpotsTransparent;
        public NumericBox numericBoxReciprocalThreshold;
        private NumericBox numericBox3D_SpotRadius;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox checkBox3D_DirectionGuide;
        public ColorControl colorControl3D_beamDirection;
        public ColorControl colorControl3D_rightDirection;
        public ColorControl colorControl3D_topDirection;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveMovieDiffractionPattern;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveMovieReciprocalSpace;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.RadioButton radioButtonBeamBackLaue;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelResolutionUnit;
        private System.Windows.Forms.RadioButton radioButtonResoUnitMilliMeter;
        private System.Windows.Forms.RadioButton radioButtonResoUnitNanometerInv;
        private System.Windows.Forms.CheckBox checkBoxDrawSameSize;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelImageOrientation;
        private System.Windows.Forms.CheckBox checkBoxFlipHorizontally;
        private System.Windows.Forms.CheckBox checkBoxFlipVertically;
        private System.Windows.Forms.CheckBox checkBoxNegativeImage;
        private System.Windows.Forms.RadioButton radioButtonKikuchiThresholdOfLength;
        private NumericBox numericBoxKikuchiThresholdOfLength;
        private System.Windows.Forms.RadioButton radioButtonKikuchiThresholdOfStructureFactor;
        private NumericBox numericBoxKikuchiThresholdOfStructureFactor;
        private System.Windows.Forms.CheckBox checkBoxShowDirectPosition;
        private System.Windows.Forms.CheckBox checkBoxShowFootPosition;
        private System.Windows.Forms.Panel panelDetectorAndMisc;
        private System.Windows.Forms.GroupBox groupBoxMisc;
        private System.Windows.Forms.Button buttonHolderSimulation;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBoxDeveloperCode;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private NumericBox numericBoxCameraLength2;
        private SizeControl sizeControl1;
    }
}
