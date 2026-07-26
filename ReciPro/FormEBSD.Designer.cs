namespace ReciPro
{
    partial class FormEBSD
    {
        /// <summary>Required designer variable.</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Clean up any resources being used.</summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            //260725Cl 追加: フォームが所有する PseudoBitmap を解放する (アプリ終了時のみ到達。FormClosing は Hide のため)。
            //patternBitmap / expImage は各 PseudoBitmap 内部キャッシュ (destBmp) への借用参照なので、ここでは Dispose せず参照だけ切る。
            if (disposing)
            {
                indexingCts?.Cancel(); //260725Cl: 探索・較正の実行中に実 Dispose された場合、解放後の状態を触らせない (Codex 指摘)
                Pbmp?.Dispose(); Pbmp = null; patternBitmap = null;
                expPbmp?.Dispose(); expPbmp = null; expImage = null;
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        // (260323Ch) renamed numeric container controls:
        // groupBox1 -> groupBoxSimulationParameters
        // groupBoxTextSettings -> groupBoxLatticePlanes
        // groupBox3 -> groupBoxMasterPattern
        // groupBox4 -> groupBoxSampleCondition
        // groupBox5 -> groupBoxEBSDGeometry
        // groupBox6 -> groupBoxEBSDPattern
        // flowLayoutPanelPatternBar -> flowLayoutPanelViewAlong
        // flowLayoutPanelMasterPattern3DCopy -> flowLayoutPanelOutputRange
        // (260520Cl) typo fix: numericBoxKikuchiThreadSholdOfStructureFactor -> numericBoxKikuchiThresholdOfStructureFactor (旧 typo "ThreadShold")
        // (260725Cl) renamed all remaining default-named controls (author request):
        //   flowLayoutPanel1..34 -> flowLayoutPanel{PatternBar, MaxNumOfGAndGrid, MasterPatternButtons, MasterPattern3DCopy,
        //     MasterPattern2DControls, ThicknessRange, EnergyRange, AbsorptionOptions, SimulationParameters, Overlays,
        //     DetectorGeometry, SampleCondition, ThresholdStructureFactor, ThresholdLength, TextSettings, DetectorPosition,
        //     DetectorSizeTilt, WithBseDistribution, Brightness, ColorScale, IndexingButtons, ExperimentalImageTab,
        //     ShowCheckBoxes, ViewSettings, Copy, ResolutionFlip, CopyOptions, MasterPatternDepth, MasterPatternControls,
        //     CopyRange, CopyButton, CopyFormat, CopyRadios}
        //   flowLayoutPanel1DetectorOutline -> flowLayoutPanelDetectorOutline / flowLayoutPanel1KikuchiLines -> flowLayoutPanelKikuchiLines
        //   label1..15 -> label{TextSize, DetectorCenter, Polarity, Color, DetectorSizeTilt, DetectorResolution, BrightnessMin,
        //     BrightnessMax, ExpBrightness, Brightness, LineWidth, BseDepth, BseDeltaE, BseStereonetNote}
        //   groupBox2 -> groupBoxTextSettings / panel1,3,4 -> panelSpacer{Left,Right,Bottom} / statusStrip1 -> statusStripMain
        //   tabControl1,2,3 -> tabControl{Settings, PatternSettings, MasterPattern} / tabPage1,2 -> tabPageMasterPattern{2D,3D}
        //   toolStripStatusLabel1,2,3 -> toolStripStatusLabel{Progress, Summary, Detail}
        //   radioButton1 -> radioButtonCopyEmf
        // 260726Cl 追加: ここから下 (InitializeComponent 本体) に書いたコメントは、VS デザイナがフォームを
        //   保存し直すたびに丸ごと捨てられる。実際 260725Cl の注記 21 件がデザイナ再生成で消えた
        //   (コードは残ったが「なぜその行が要るのか」の記録だけが失われた)。消えては困る来歴はここへ書くこと。
        //   InitializeComponent 内で、消えると機能が壊れる/意図が読めなくなる行 (260725Cl 由来):
        //     ・buttonMasterPattern2DCopy.Click / buttonMasterPattern3DCopy.Click の += 配線
        //       … 元々これが無く、Copy ボタンが両方とも無反応だったのを修正したもの。消さないこと。
        //     ・captureExtender.SetCapture(tabPageExperimentalImage, true)
        //       … 実測画像タブは既定選択タブでないため、これが無いとマニュアル用の自動キャプチャに写らない。
        //     ・toolTip.SetToolTip(...) 群 … resx に文案があっても、この行が無いと絶対に表示されない
        //       (文案だけ在って表示経路が無い、が過去に最も多かった不具合)。
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEBSD));
            toolTip = new System.Windows.Forms.ToolTip(components);
            numericBoxSampleTilt = new NumericBox();
            waveLengthControl = new WaveLengthControl();
            buttonViewQuarter = new System.Windows.Forms.Button();
            buttonViewFromSurfaceNormal = new System.Windows.Forms.Button();
            buttonFromX = new System.Windows.Forms.Button();
            buttonViewFromZ = new System.Windows.Forms.Button();
            buttonSimulateBSE = new System.Windows.Forms.Button();
            buttonFitNistElasticSampler = new System.Windows.Forms.Button();
            checkBoxDrawAxesInStereonet = new System.Windows.Forms.CheckBox();
            numericBoxDetTilt = new NumericBox();
            numericBoxZofDet = new NumericBox();
            numericBoxYofDet = new NumericBox();
            trackBarStrSize = new System.Windows.Forms.TrackBar();
            colorControlExcessLine = new ColorControl();
            trackBarLineWidth = new System.Windows.Forms.TrackBar();
            labelLineWidth = new System.Windows.Forms.Label();
            colorControlString = new ColorControl();
            colorControlBackGround = new ColorControl();
            radioButtonKikuchiThresholdOfStructureFactor = new System.Windows.Forms.RadioButton();
            checkBoxKikuchiLine_Kinematical = new System.Windows.Forms.CheckBox();
            radioButtonKikuchiThresholdOfLength = new System.Windows.Forms.RadioButton();
            numericBoxKikuchiThresholdOfStructureFactor = new NumericBox();
            numericBoxKikuchiThresholdOfLength = new NumericBox();
            labelTextSize = new System.Windows.Forms.Label();
            numericBoxThicknessStep = new NumericBox();
            numericBoxMaxNumOfG = new NumericBox();
            checkBoxNonLocalAbsorption = new System.Windows.Forms.CheckBox();
            checkBoxTDSBackground = new System.Windows.Forms.CheckBox();
            numericBoxThicknessStart = new NumericBox();
            numericBoxThicknessEnd = new NumericBox();
            buttonStop = new System.Windows.Forms.Button();
            numericBoxEnergy = new NumericBox();
            trackBarOutputEnergy = new System.Windows.Forms.TrackBar();
            numericBoxDepth = new NumericBox();
            trackBarOutputThickness = new System.Windows.Forms.TrackBar();
            labelPolarity = new System.Windows.Forms.Label();
            labelColor = new System.Windows.Forms.Label();
            checkBoxWithBSEDistribution = new System.Windows.Forms.CheckBox();
            comboBoxGradient = new System.Windows.Forms.ComboBox();
            comboBoxScale = new System.Windows.Forms.ComboBox();
            trackBarIntensityBrightnessMax = new System.Windows.Forms.TrackBar();
            trackBarIntensityBrightnessMin = new System.Windows.Forms.TrackBar();
            labelBrightnessMax = new System.Windows.Forms.Label();
            labelBrightnessMin = new System.Windows.Forms.Label();
            labelBrightness = new System.Windows.Forms.Label();
            checkBoxShowOverlays = new System.Windows.Forms.CheckBox();
            buttonCopyImage = new System.Windows.Forms.Button();
            numericBoxEnergyEnd = new NumericBox();
            numericBoxEnergyStart = new NumericBox();
            numericBoxEnergyStep = new NumericBox();
            labelBseDepth = new System.Windows.Forms.Label();
            labelBseDeltaE = new System.Windows.Forms.Label();
            checkBoxShowDyanmicalEBSD = new System.Windows.Forms.CheckBox();
            checkBoxDrawDetectorOutline = new System.Windows.Forms.CheckBox();
            labelBseStereonetNote = new System.Windows.Forms.Label();
            checkBoxShowKikuchiLines = new System.Windows.Forms.CheckBox();
            checkBoxShowGIndices = new System.Windows.Forms.CheckBox();
            checkBoxShowZoneAxisIndices = new System.Windows.Forms.CheckBox();
            labelMasterPatternGrid = new System.Windows.Forms.Label();
            comboBoxMasterPatternGrid = new System.Windows.Forms.ComboBox();
            numericBoxMasterPatternEnergy = new NumericBox();
            trackBarMasterPatternEnergy = new System.Windows.Forms.TrackBar();
            numericBoxMasterPatternDepth = new NumericBox();
            trackBarMasterPatternDepth = new System.Windows.Forms.TrackBar();
            labelMasterPattern2DHemisphere = new System.Windows.Forms.Label();
            comboBoxMasterPattern2DHemisphere = new System.Windows.Forms.ComboBox();
            buttonCreateMasterPattern = new System.Windows.Forms.Button();
            buttonMasterPattern2DCopy = new System.Windows.Forms.Button();
            buttonMasterPattern3DCopy = new System.Windows.Forms.Button();
            checkBoxMasterPattern3DAxisLabel = new System.Windows.Forms.CheckBox();
            buttonMasterPattern3DViewAlong = new System.Windows.Forms.Button();
            indexControl = new IndexControl();
            checkBoxShowMesh = new System.Windows.Forms.CheckBox();
            checkBoxShowCircle = new System.Windows.Forms.CheckBox();
            labelExpBrightness = new System.Windows.Forms.Label();
            checkBoxMasterPattern3DAxisArrows = new System.Windows.Forms.CheckBox();
            checkBoxFlipDetectorLeftRight = new System.Windows.Forms.CheckBox();
            checkBoxShowExperimentalImage = new System.Windows.Forms.CheckBox();
            labelExpMinInt = new System.Windows.Forms.Label();
            trackBarExpImageMinInt = new System.Windows.Forms.TrackBar();
            labelExpMaxInt = new System.Windows.Forms.Label();
            trackBarExpImageMaxInt = new System.Windows.Forms.TrackBar();
            labelExpOpacity = new System.Windows.Forms.Label();
            trackBarExpImageOpacity = new System.Windows.Forms.TrackBar();
            radioButtonIndexingRadon = new System.Windows.Forms.RadioButton();
            radioButtonIndexingDictionary = new System.Windows.Forms.RadioButton();
            buttonFindOrientation = new System.Windows.Forms.Button();
            dataGridViewEbsdCandidates = new System.Windows.Forms.DataGridView();
            buttonCalibrateGeometry = new System.Windows.Forms.Button();
            checkBoxMatchDetectorResolution = new System.Windows.Forms.CheckBox();
            radioButtonCopyCurrent = new System.Windows.Forms.RadioButton();
            radioButtonDetector = new System.Windows.Forms.RadioButton();
            radioButtonCopyEmf = new System.Windows.Forms.RadioButton();
            radioButtonCopyBmp = new System.Windows.Forms.RadioButton();
            panelGeometry = new System.Windows.Forms.Panel();
            flowLayoutPanelViewAlong = new System.Windows.Forms.FlowLayoutPanel();
            graphControlDepthProfile = new GraphControl();
            poleFigureControl = new PoleFigureControl2();
            graphControlEnergyProfile = new GraphControl();
            graphicsBox = new GraphicsBox(components);
            flowLayoutPanelColorScale = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelBrightness = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelOutputRange = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelWithBseDistribution = new System.Windows.Forms.FlowLayoutPanel();
            tabControlSettings = new System.Windows.Forms.TabControl();
            tabPageGeometry = new System.Windows.Forms.TabPage();
            groupBoxEBSDGeometry = new System.Windows.Forms.GroupBox();
            flowLayoutPanelDetectorGeometry = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelDetectorSizeTilt = new System.Windows.Forms.FlowLayoutPanel();
            labelDetectorSizeTilt = new System.Windows.Forms.Label();
            numericBoxDetWidth = new NumericBox();
            numericBoxDetHeight = new NumericBox();
            labelDetectorResolution = new System.Windows.Forms.Label();
            numericBoxDetResolution = new NumericBox();
            flowLayoutPanelDetectorPosition = new System.Windows.Forms.FlowLayoutPanel();
            labelDetectorCenter = new System.Windows.Forms.Label();
            numericBoxXofDet = new NumericBox();
            groupBoxSampleCondition = new System.Windows.Forms.GroupBox();
            flowLayoutPanelSampleCondition = new System.Windows.Forms.FlowLayoutPanel();
            tabPageBseDistribution = new System.Windows.Forms.TabPage();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tabPageOverlays = new System.Windows.Forms.TabPage();
            flowLayoutPanelOverlays = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelDetectorOutline = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelKikuchiLines = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxLatticePlanes = new System.Windows.Forms.GroupBox();
            flowLayoutPanelThresholdLength = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelThresholdStructureFactor = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxTextSettings = new System.Windows.Forms.GroupBox();
            flowLayoutPanelTextSettings = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelExperimentalImage = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelExpMinInt = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelExpMaxInt = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelExpOpacity = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxSimulationParameters = new System.Windows.Forms.GroupBox();
            flowLayoutPanelSimulationParameters = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelMaxNumOfGAndGrid = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelEnergyRange = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelThicknessRange = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelAbsorptionOptions = new System.Windows.Forms.FlowLayoutPanel();
            statusStripMain = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabelProgress = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelSummary = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelDetail = new System.Windows.Forms.ToolStripStatusLabel();
            scalablePictureBoxAdvancedMasterPattern2D = new ScalablePictureBoxAdvanced();
            flowLayoutPanelMasterPatternSelectors = new System.Windows.Forms.FlowLayoutPanel();
            panelMasterPattern3D = new System.Windows.Forms.Panel();
            panelMasterPattern3DAxes = new System.Windows.Forms.Panel();
            groupBoxMasterPattern = new System.Windows.Forms.GroupBox();
            tabControlMasterPattern = new System.Windows.Forms.TabControl();
            tabPageMasterPattern2D = new System.Windows.Forms.TabPage();
            flowLayoutPanelMasterPattern2DControls = new System.Windows.Forms.FlowLayoutPanel();
            tabPageMasterPattern3D = new System.Windows.Forms.TabPage();
            flowLayoutPanelMasterPattern3DCopy = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelMasterPattern3DViewAlong = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelMasterPatternControls = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelMasterPatternDepth = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelMasterPatternButtons = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxEBSDPattern = new System.Windows.Forms.GroupBox();
            tabControlPatternSettings = new System.Windows.Forms.TabControl();
            tabPageOutputParameter = new System.Windows.Forms.TabPage();
            tabPageExperimentalImage = new System.Windows.Forms.TabPage();
            flowLayoutPanelExperimentalImageTab = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelIndexingButtons = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelPatternBar = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelCopy = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelCopyOptions = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelCopyButton = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelCopyRadios = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelCopyRange = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelCopyFormat = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelViewSettings = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelResolutionFlip = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxResolution = new NumericBox();
            sizeControl = new SizeControl();
            flowLayoutPanelShowCheckBoxes = new System.Windows.Forms.FlowLayoutPanel();
            panelSpacerLeft = new System.Windows.Forms.Panel();
            panelSpacerRight = new System.Windows.Forms.Panel();
            panelSpacerBottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputEnergy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternEnergy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternDepth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageMinInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageMaxInt).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageOpacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEbsdCandidates).BeginInit();
            flowLayoutPanelViewAlong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            flowLayoutPanelColorScale.SuspendLayout();
            flowLayoutPanelBrightness.SuspendLayout();
            flowLayoutPanelOutputRange.SuspendLayout();
            flowLayoutPanelWithBseDistribution.SuspendLayout();
            tabControlSettings.SuspendLayout();
            tabPageGeometry.SuspendLayout();
            groupBoxEBSDGeometry.SuspendLayout();
            flowLayoutPanelDetectorGeometry.SuspendLayout();
            flowLayoutPanelDetectorSizeTilt.SuspendLayout();
            flowLayoutPanelDetectorPosition.SuspendLayout();
            groupBoxSampleCondition.SuspendLayout();
            flowLayoutPanelSampleCondition.SuspendLayout();
            tabPageBseDistribution.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tabPageOverlays.SuspendLayout();
            flowLayoutPanelOverlays.SuspendLayout();
            flowLayoutPanelDetectorOutline.SuspendLayout();
            flowLayoutPanelKikuchiLines.SuspendLayout();
            groupBoxLatticePlanes.SuspendLayout();
            flowLayoutPanelThresholdLength.SuspendLayout();
            flowLayoutPanelThresholdStructureFactor.SuspendLayout();
            groupBoxTextSettings.SuspendLayout();
            flowLayoutPanelTextSettings.SuspendLayout();
            flowLayoutPanelExperimentalImage.SuspendLayout();
            flowLayoutPanelExpMinInt.SuspendLayout();
            flowLayoutPanelExpMaxInt.SuspendLayout();
            flowLayoutPanelExpOpacity.SuspendLayout();
            groupBoxSimulationParameters.SuspendLayout();
            flowLayoutPanelSimulationParameters.SuspendLayout();
            flowLayoutPanelMaxNumOfGAndGrid.SuspendLayout();
            flowLayoutPanelEnergyRange.SuspendLayout();
            flowLayoutPanelThicknessRange.SuspendLayout();
            flowLayoutPanelAbsorptionOptions.SuspendLayout();
            statusStripMain.SuspendLayout();
            flowLayoutPanelMasterPatternSelectors.SuspendLayout();
            panelMasterPattern3D.SuspendLayout();
            groupBoxMasterPattern.SuspendLayout();
            tabControlMasterPattern.SuspendLayout();
            tabPageMasterPattern2D.SuspendLayout();
            flowLayoutPanelMasterPattern2DControls.SuspendLayout();
            tabPageMasterPattern3D.SuspendLayout();
            flowLayoutPanelMasterPattern3DCopy.SuspendLayout();
            flowLayoutPanelMasterPattern3DViewAlong.SuspendLayout();
            flowLayoutPanelMasterPatternControls.SuspendLayout();
            flowLayoutPanelMasterPatternDepth.SuspendLayout();
            flowLayoutPanelMasterPatternButtons.SuspendLayout();
            groupBoxEBSDPattern.SuspendLayout();
            tabControlPatternSettings.SuspendLayout();
            tabPageOutputParameter.SuspendLayout();
            tabPageExperimentalImage.SuspendLayout();
            flowLayoutPanelExperimentalImageTab.SuspendLayout();
            flowLayoutPanelIndexingButtons.SuspendLayout();
            flowLayoutPanelPatternBar.SuspendLayout();
            flowLayoutPanelCopy.SuspendLayout();
            flowLayoutPanelCopyOptions.SuspendLayout();
            flowLayoutPanelCopyButton.SuspendLayout();
            flowLayoutPanelCopyRadios.SuspendLayout();
            flowLayoutPanelCopyRange.SuspendLayout();
            flowLayoutPanelCopyFormat.SuspendLayout();
            flowLayoutPanelViewSettings.SuspendLayout();
            flowLayoutPanelResolutionFlip.SuspendLayout();
            flowLayoutPanelShowCheckBoxes.SuspendLayout();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 100;
            // 
            // numericBoxSampleTilt
            // 
            resources.ApplyResources(numericBoxSampleTilt, "numericBoxSampleTilt");
            numericBoxSampleTilt.BackColor = System.Drawing.Color.Transparent;
            numericBoxSampleTilt.Maximum = 0D;
            numericBoxSampleTilt.Minimum = -90D;
            numericBoxSampleTilt.Name = "numericBoxSampleTilt";
            numericBoxSampleTilt.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxSampleTilt, resources.GetString("numericBoxSampleTilt.ToolTip"));
            numericBoxSampleTilt.UpDown_Increment = 10D;
            numericBoxSampleTilt.Value = -70D;
            numericBoxSampleTilt.ValueBoxWidth = 50;
            numericBoxSampleTilt.ValueChanged += numericBoxSampleTilt_ValueChanged;
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            waveLengthControl.Energy = 20D;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = false;
            toolTip.SetToolTip(waveLengthControl, resources.GetString("waveLengthControl.ToolTip"));
            waveLengthControl.WaveLength = 0.008588514105D;
            waveLengthControl.WaveSource = WaveSource.Electron;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            waveLengthControl.WavelengthChanged += waveLengthControl_WavelengthChanged;
            // 
            // buttonViewQuarter
            // 
            resources.ApplyResources(buttonViewQuarter, "buttonViewQuarter");
            buttonViewQuarter.Name = "buttonViewQuarter";
            toolTip.SetToolTip(buttonViewQuarter, resources.GetString("buttonViewQuarter.ToolTip"));
            buttonViewQuarter.UseVisualStyleBackColor = true;
            buttonViewQuarter.Click += buttonViewQuarter_Click;
            // 
            // buttonViewFromSurfaceNormal
            // 
            resources.ApplyResources(buttonViewFromSurfaceNormal, "buttonViewFromSurfaceNormal");
            buttonViewFromSurfaceNormal.Name = "buttonViewFromSurfaceNormal";
            toolTip.SetToolTip(buttonViewFromSurfaceNormal, resources.GetString("buttonViewFromSurfaceNormal.ToolTip"));
            buttonViewFromSurfaceNormal.UseVisualStyleBackColor = true;
            buttonViewFromSurfaceNormal.Click += buttonFromSurfaceNormal_Click;
            // 
            // buttonFromX
            // 
            resources.ApplyResources(buttonFromX, "buttonFromX");
            buttonFromX.Name = "buttonFromX";
            toolTip.SetToolTip(buttonFromX, resources.GetString("buttonFromX.ToolTip"));
            buttonFromX.UseVisualStyleBackColor = true;
            buttonFromX.Click += buttonViewFromX_Click;
            // 
            // buttonViewFromZ
            // 
            resources.ApplyResources(buttonViewFromZ, "buttonViewFromZ");
            buttonViewFromZ.Name = "buttonViewFromZ";
            toolTip.SetToolTip(buttonViewFromZ, resources.GetString("buttonViewFromZ.ToolTip"));
            buttonViewFromZ.UseVisualStyleBackColor = true;
            buttonViewFromZ.Click += buttonViewFromZ_Click;
            // 
            // buttonSimulateBSE
            // 
            resources.ApplyResources(buttonSimulateBSE, "buttonSimulateBSE");
            buttonSimulateBSE.BackColor = System.Drawing.Color.SteelBlue;
            buttonSimulateBSE.ForeColor = System.Drawing.Color.White;
            buttonSimulateBSE.Name = "buttonSimulateBSE";
            toolTip.SetToolTip(buttonSimulateBSE, resources.GetString("buttonSimulateBSE.ToolTip"));
            buttonSimulateBSE.UseVisualStyleBackColor = false;
            buttonSimulateBSE.Click += buttonBSE_Click;
            // 
            // buttonFitNistElasticSampler
            // 
            resources.ApplyResources(buttonFitNistElasticSampler, "buttonFitNistElasticSampler");
            buttonFitNistElasticSampler.Name = "buttonFitNistElasticSampler";
            toolTip.SetToolTip(buttonFitNistElasticSampler, resources.GetString("buttonFitNistElasticSampler.ToolTip"));
            buttonFitNistElasticSampler.UseVisualStyleBackColor = true;
            buttonFitNistElasticSampler.Click += buttonFitNistElasticSampler_Click;
            // 
            // checkBoxDrawAxesInStereonet
            // 
            resources.ApplyResources(checkBoxDrawAxesInStereonet, "checkBoxDrawAxesInStereonet");
            checkBoxDrawAxesInStereonet.BackColor = System.Drawing.Color.White;
            checkBoxDrawAxesInStereonet.Checked = true;
            checkBoxDrawAxesInStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawAxesInStereonet.Name = "checkBoxDrawAxesInStereonet";
            toolTip.SetToolTip(checkBoxDrawAxesInStereonet, resources.GetString("checkBoxDrawAxesInStereonet.ToolTip"));
            checkBoxDrawAxesInStereonet.UseVisualStyleBackColor = false;
            // 
            // numericBoxDetTilt
            // 
            resources.ApplyResources(numericBoxDetTilt, "numericBoxDetTilt");
            numericBoxDetTilt.BackColor = System.Drawing.Color.Transparent;
            numericBoxDetTilt.DecimalPlaces = 2; // 260726Cl 追加: 既定 -1 (general) だと桁数無制限で数値欄が切れる
            numericBoxDetTilt.Maximum = 180D;
            numericBoxDetTilt.Minimum = 0D;
            numericBoxDetTilt.Name = "numericBoxDetTilt";
            numericBoxDetTilt.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxDetTilt, resources.GetString("numericBoxDetTilt.ToolTip"));
            numericBoxDetTilt.UpDown_Increment = 10D;
            numericBoxDetTilt.Value = 90D;
            //numericBoxDetTilt.ValueBoxWidth = 50; // 260726Cl 旧: 隣のヘッダ (独/露の訳語) に幅を回すため縮小。Maximum=180 なので要実機確認
            //numericBoxDetTilt.ValueBoxWidth = 40; // 260726Cl 旧: 最長値「180.000」が数値欄に入らず切れていた (--diagnose ValueBoxClipped)
            numericBoxDetTilt.ValueBoxWidth = 54; // 260726Cl // 260726Cl
            numericBoxDetTilt.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // numericBoxZofDet
            // 
            resources.ApplyResources(numericBoxZofDet, "numericBoxZofDet");
            numericBoxZofDet.BackColor = System.Drawing.Color.Transparent;
            numericBoxZofDet.DecimalPlaces = 3;
            numericBoxZofDet.Maximum = 1000D;
            numericBoxZofDet.Minimum = -1000D;
            numericBoxZofDet.Name = "numericBoxZofDet";
            numericBoxZofDet.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxZofDet, resources.GetString("numericBoxZofDet.ToolTip"));
            numericBoxZofDet.Value = 30D;
            //numericBoxZofDet.ValueBoxWidth = 52; // 260726Cl 旧: CJK フォントだと「-35.000」の末尾が切れる
            numericBoxZofDet.ValueBoxWidth = 62; // 260726Cl
            numericBoxZofDet.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // numericBoxYofDet
            // 
            resources.ApplyResources(numericBoxYofDet, "numericBoxYofDet");
            numericBoxYofDet.BackColor = System.Drawing.Color.Transparent;
            numericBoxYofDet.DecimalPlaces = 3;
            numericBoxYofDet.Maximum = 1000D;
            numericBoxYofDet.Minimum = -1000D;
            numericBoxYofDet.Name = "numericBoxYofDet";
            numericBoxYofDet.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxYofDet, resources.GetString("numericBoxYofDet.ToolTip"));
            numericBoxYofDet.Value = -35D;
            //numericBoxYofDet.ValueBoxWidth = 52; // 260726Cl 旧: CJK フォントだと「-35.000」の末尾が切れる
            numericBoxYofDet.ValueBoxWidth = 62; // 260726Cl
            numericBoxYofDet.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
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
            trackBarStrSize.ValueChanged += colorControlExcessLine_ColorChanged;
            // 
            // colorControlExcessLine
            // 
            resources.ApplyResources(colorControlExcessLine, "colorControlExcessLine");
            colorControlExcessLine.BackColor = System.Drawing.SystemColors.Control;
            colorControlExcessLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControlExcessLine.Color = System.Drawing.Color.FromArgb(255, 128, 0);
            colorControlExcessLine.Name = "colorControlExcessLine";
            toolTip.SetToolTip(colorControlExcessLine, resources.GetString("colorControlExcessLine.ToolTip"));
            colorControlExcessLine.ColorChanged += colorControlExcessLine_ColorChanged;
            colorControlExcessLine.Load += colorControlExcessLine_Load;
            // 
            // trackBarLineWidth
            // 
            resources.ApplyResources(trackBarLineWidth, "trackBarLineWidth");
            trackBarLineWidth.Maximum = 10000;
            trackBarLineWidth.Minimum = 1;
            trackBarLineWidth.Name = "trackBarLineWidth";
            trackBarLineWidth.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarLineWidth, resources.GetString("trackBarLineWidth.ToolTip"));
            trackBarLineWidth.Value = 1;
            trackBarLineWidth.ValueChanged += colorControlExcessLine_ColorChanged;
            // 
            // labelLineWidth
            // 
            resources.ApplyResources(labelLineWidth, "labelLineWidth");
            labelLineWidth.Name = "labelLineWidth";
            toolTip.SetToolTip(labelLineWidth, resources.GetString("labelLineWidth.ToolTip"));
            // 
            // colorControlString
            // 
            resources.ApplyResources(colorControlString, "colorControlString");
            colorControlString.BackColor = System.Drawing.SystemColors.Control;
            colorControlString.BoxSize = new System.Drawing.Size(20, 20);
            colorControlString.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlString.Name = "colorControlString";
            toolTip.SetToolTip(colorControlString, resources.GetString("colorControlString.ToolTip"));
            colorControlString.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // colorControlBackGround
            // 
            resources.ApplyResources(colorControlBackGround, "colorControlBackGround");
            colorControlBackGround.BackColor = System.Drawing.SystemColors.Control;
            colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(32, 32, 32);
            colorControlBackGround.Name = "colorControlBackGround";
            toolTip.SetToolTip(colorControlBackGround, resources.GetString("colorControlBackGround.ToolTip"));
            colorControlBackGround.ColorChanged += colorControlExcessLine_ColorChanged;
            // 
            // radioButtonKikuchiThresholdOfStructureFactor
            // 
            resources.ApplyResources(radioButtonKikuchiThresholdOfStructureFactor, "radioButtonKikuchiThresholdOfStructureFactor");
            radioButtonKikuchiThresholdOfStructureFactor.Checked = true;
            radioButtonKikuchiThresholdOfStructureFactor.Name = "radioButtonKikuchiThresholdOfStructureFactor";
            radioButtonKikuchiThresholdOfStructureFactor.TabStop = true;
            toolTip.SetToolTip(radioButtonKikuchiThresholdOfStructureFactor, resources.GetString("radioButtonKikuchiThresholdOfStructureFactor.ToolTip"));
            radioButtonKikuchiThresholdOfStructureFactor.UseVisualStyleBackColor = true;
            radioButtonKikuchiThresholdOfStructureFactor.CheckedChanged += radioButtonKikuchiThresholdOfStructureFactor_CheckedChanged;
            // 
            // checkBoxKikuchiLine_Kinematical
            // 
            resources.ApplyResources(checkBoxKikuchiLine_Kinematical, "checkBoxKikuchiLine_Kinematical");
            checkBoxKikuchiLine_Kinematical.Checked = true;
            checkBoxKikuchiLine_Kinematical.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxKikuchiLine_Kinematical.Name = "checkBoxKikuchiLine_Kinematical";
            toolTip.SetToolTip(checkBoxKikuchiLine_Kinematical, resources.GetString("checkBoxKikuchiLine_Kinematical.ToolTip"));
            checkBoxKikuchiLine_Kinematical.UseVisualStyleBackColor = true;
            checkBoxKikuchiLine_Kinematical.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // radioButtonKikuchiThresholdOfLength
            // 
            resources.ApplyResources(radioButtonKikuchiThresholdOfLength, "radioButtonKikuchiThresholdOfLength");
            radioButtonKikuchiThresholdOfLength.Name = "radioButtonKikuchiThresholdOfLength";
            toolTip.SetToolTip(radioButtonKikuchiThresholdOfLength, resources.GetString("radioButtonKikuchiThresholdOfLength.ToolTip"));
            radioButtonKikuchiThresholdOfLength.UseVisualStyleBackColor = true;
            // 
            // numericBoxKikuchiThresholdOfStructureFactor
            // 
            resources.ApplyResources(numericBoxKikuchiThresholdOfStructureFactor, "numericBoxKikuchiThresholdOfStructureFactor");
            numericBoxKikuchiThresholdOfStructureFactor.BackColor = System.Drawing.Color.Transparent;
            numericBoxKikuchiThresholdOfStructureFactor.Maximum = 1000D;
            numericBoxKikuchiThresholdOfStructureFactor.Minimum = 1D;
            numericBoxKikuchiThresholdOfStructureFactor.Name = "numericBoxKikuchiThresholdOfStructureFactor";
            numericBoxKikuchiThresholdOfStructureFactor.ShowUpDown = true;
            numericBoxKikuchiThresholdOfStructureFactor.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxKikuchiThresholdOfStructureFactor, resources.GetString("numericBoxKikuchiThresholdOfStructureFactor.ToolTip"));
            numericBoxKikuchiThresholdOfStructureFactor.Value = 40D;
            numericBoxKikuchiThresholdOfStructureFactor.ValueChanged += numericBoxKikuchiThresholdOfStructureFactor_ValueChanged;
            // 
            // numericBoxKikuchiThresholdOfLength
            // 
            resources.ApplyResources(numericBoxKikuchiThresholdOfLength, "numericBoxKikuchiThresholdOfLength");
            numericBoxKikuchiThresholdOfLength.BackColor = System.Drawing.Color.Transparent;
            numericBoxKikuchiThresholdOfLength.Maximum = 100D;
            numericBoxKikuchiThresholdOfLength.Minimum = 0D;
            numericBoxKikuchiThresholdOfLength.Name = "numericBoxKikuchiThresholdOfLength";
            numericBoxKikuchiThresholdOfLength.ShowUpDown = true;
            numericBoxKikuchiThresholdOfLength.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxKikuchiThresholdOfLength, resources.GetString("numericBoxKikuchiThresholdOfLength.ToolTip"));
            numericBoxKikuchiThresholdOfLength.Value = 10D;
            numericBoxKikuchiThresholdOfLength.ValueChanged += numericBoxKikuchiThresholdOfStructureFactor_ValueChanged;
            // 
            // labelTextSize
            // 
            resources.ApplyResources(labelTextSize, "labelTextSize");
            labelTextSize.Name = "labelTextSize";
            toolTip.SetToolTip(labelTextSize, resources.GetString("labelTextSize.ToolTip"));
            // 
            // numericBoxThicknessStep
            // 
            resources.ApplyResources(numericBoxThicknessStep, "numericBoxThicknessStep");
            numericBoxThicknessStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStep.DecimalPlaces = 2;
            numericBoxThicknessStep.Maximum = 10D;
            numericBoxThicknessStep.Minimum = 0.001D;
            numericBoxThicknessStep.Name = "numericBoxThicknessStep";
            numericBoxThicknessStep.ShowUpDown = true;
            numericBoxThicknessStep.SmartIncrement = true;
            numericBoxThicknessStep.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessStep, resources.GetString("numericBoxThicknessStep.ToolTip"));
            numericBoxThicknessStep.Value = 1D;
            numericBoxThicknessStep.ValueBoxWidth = 38;
            numericBoxThicknessStep.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxMaxNumOfG
            // 
            resources.ApplyResources(numericBoxMaxNumOfG, "numericBoxMaxNumOfG");
            numericBoxMaxNumOfG.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxNumOfG.DecimalPlaces = 0; // 260726Cl 追加: 回折波の本数 (ValueInteger で消費する整数)。既定 -1 は general 書式で小数が表示され得る
            numericBoxMaxNumOfG.Maximum = 2048D;
            numericBoxMaxNumOfG.Minimum = 1D;
            numericBoxMaxNumOfG.Name = "numericBoxMaxNumOfG";
            numericBoxMaxNumOfG.ShowUpDown = true;
            numericBoxMaxNumOfG.SmartIncrement = true;
            numericBoxMaxNumOfG.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxMaxNumOfG, resources.GetString("numericBoxMaxNumOfG.ToolTip"));
            numericBoxMaxNumOfG.Value = 32D;
            numericBoxMaxNumOfG.ValueBoxWidth = 40;
            // 
            // checkBoxNonLocalAbsorption
            // 
            resources.ApplyResources(checkBoxNonLocalAbsorption, "checkBoxNonLocalAbsorption");
            checkBoxNonLocalAbsorption.Name = "checkBoxNonLocalAbsorption";
            toolTip.SetToolTip(checkBoxNonLocalAbsorption, resources.GetString("checkBoxNonLocalAbsorption.ToolTip"));
            checkBoxNonLocalAbsorption.UseVisualStyleBackColor = true;
            // 
            // checkBoxTDSBackground
            // 
            resources.ApplyResources(checkBoxTDSBackground, "checkBoxTDSBackground");
            checkBoxTDSBackground.Name = "checkBoxTDSBackground";
            toolTip.SetToolTip(checkBoxTDSBackground, resources.GetString("checkBoxTDSBackground.ToolTip"));
            checkBoxTDSBackground.UseVisualStyleBackColor = true;
            // 
            // numericBoxThicknessStart
            // 
            resources.ApplyResources(numericBoxThicknessStart, "numericBoxThicknessStart");
            numericBoxThicknessStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessStart.DecimalPlaces = 2;
            numericBoxThicknessStart.Maximum = 1000D;
            numericBoxThicknessStart.Minimum = 0.001D;
            numericBoxThicknessStart.Name = "numericBoxThicknessStart";
            numericBoxThicknessStart.ShowUpDown = true;
            numericBoxThicknessStart.SmartIncrement = true;
            numericBoxThicknessStart.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessStart, resources.GetString("numericBoxThicknessStart.ToolTip"));
            numericBoxThicknessStart.Value = 1D;
            //numericBoxThicknessStart.ValueBoxWidth = 38; // 260726Cl 旧: 最長値「1000.00」が数値欄に入らず切れていた (--diagnose ValueBoxClipped)
            numericBoxThicknessStart.ValueBoxWidth = 54; // 260726Cl
            numericBoxThicknessStart.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // numericBoxThicknessEnd
            // 
            resources.ApplyResources(numericBoxThicknessEnd, "numericBoxThicknessEnd");
            numericBoxThicknessEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxThicknessEnd.DecimalPlaces = 2;
            numericBoxThicknessEnd.Maximum = 1000D;
            numericBoxThicknessEnd.Minimum = 0.001D;
            numericBoxThicknessEnd.Name = "numericBoxThicknessEnd";
            numericBoxThicknessEnd.ShowUpDown = true;
            numericBoxThicknessEnd.SmartIncrement = true;
            numericBoxThicknessEnd.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxThicknessEnd, resources.GetString("numericBoxThicknessEnd.ToolTip"));
            numericBoxThicknessEnd.Value = 50D;
            //numericBoxThicknessEnd.ValueBoxWidth = 40; // 260726Cl 旧: 最長値「1000.00」が数値欄に入らず切れていた (--diagnose ValueBoxClipped)
            numericBoxThicknessEnd.ValueBoxWidth = 54; // 260726Cl
            numericBoxThicknessEnd.ValueChanged += NumericBoxThicknessStart_ValueChanged;
            // 
            // buttonStop
            // 
            resources.ApplyResources(buttonStop, "buttonStop");
            buttonStop.BackColor = System.Drawing.Color.IndianRed;
            buttonStop.ForeColor = System.Drawing.Color.White;
            buttonStop.Name = "buttonStop";
            toolTip.SetToolTip(buttonStop, resources.GetString("buttonStop.ToolTip"));
            buttonStop.UseVisualStyleBackColor = false;
            // 
            // numericBoxEnergy
            // 
            resources.ApplyResources(numericBoxEnergy, "numericBoxEnergy");
            numericBoxEnergy.BackColor = System.Drawing.Color.Transparent;
            numericBoxEnergy.Name = "numericBoxEnergy";
            numericBoxEnergy.ReadOnly = true;
            toolTip.SetToolTip(numericBoxEnergy, resources.GetString("numericBoxEnergy.ToolTip"));
            numericBoxEnergy.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarOutputEnergy
            // 
            resources.ApplyResources(trackBarOutputEnergy, "trackBarOutputEnergy");
            trackBarOutputEnergy.LargeChange = 1;
            trackBarOutputEnergy.Maximum = 5;
            trackBarOutputEnergy.Name = "trackBarOutputEnergy";
            toolTip.SetToolTip(trackBarOutputEnergy, resources.GetString("trackBarOutputEnergy.ToolTip"));
            trackBarOutputEnergy.ValueChanged += trackBarOutputEnergy_ValueChanged;
            // 
            // numericBoxDepth
            // 
            resources.ApplyResources(numericBoxDepth, "numericBoxDepth");
            numericBoxDepth.BackColor = System.Drawing.Color.Transparent;
            numericBoxDepth.Name = "numericBoxDepth";
            numericBoxDepth.ReadOnly = true;
            toolTip.SetToolTip(numericBoxDepth, resources.GetString("numericBoxDepth.ToolTip"));
            numericBoxDepth.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarOutputThickness
            // 
            resources.ApplyResources(trackBarOutputThickness, "trackBarOutputThickness");
            trackBarOutputThickness.LargeChange = 1;
            trackBarOutputThickness.Maximum = 9;
            trackBarOutputThickness.Name = "trackBarOutputThickness";
            toolTip.SetToolTip(trackBarOutputThickness, resources.GetString("trackBarOutputThickness.ToolTip"));
            trackBarOutputThickness.ValueChanged += TrackBarOutputThickness_Scroll;
            // 
            // labelPolarity
            // 
            resources.ApplyResources(labelPolarity, "labelPolarity");
            labelPolarity.Name = "labelPolarity";
            toolTip.SetToolTip(labelPolarity, resources.GetString("labelPolarity.ToolTip"));
            // 
            // labelColor
            // 
            resources.ApplyResources(labelColor, "labelColor");
            labelColor.Name = "labelColor";
            toolTip.SetToolTip(labelColor, resources.GetString("labelColor.ToolTip"));
            // 
            // checkBoxWithBSEDistribution
            // 
            resources.ApplyResources(checkBoxWithBSEDistribution, "checkBoxWithBSEDistribution");
            checkBoxWithBSEDistribution.Checked = true;
            checkBoxWithBSEDistribution.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxWithBSEDistribution.Name = "checkBoxWithBSEDistribution";
            toolTip.SetToolTip(checkBoxWithBSEDistribution, resources.GetString("checkBoxWithBSEDistribution.ToolTip"));
            checkBoxWithBSEDistribution.UseVisualStyleBackColor = true;
            checkBoxWithBSEDistribution.CheckedChanged += checkBoxWithBSEDistribution_CheckedChanged;
            // 
            // comboBoxGradient
            // 
            resources.ApplyResources(comboBoxGradient, "comboBoxGradient");
            comboBoxGradient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxGradient.FormattingEnabled = true;
            comboBoxGradient.Items.AddRange(new object[] { resources.GetString("comboBoxGradient.Items"), resources.GetString("comboBoxGradient.Items1") });
            comboBoxGradient.Name = "comboBoxGradient";
            toolTip.SetToolTip(comboBoxGradient, resources.GetString("comboBoxGradient.ToolTip"));
            comboBoxGradient.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // comboBoxScale
            // 
            resources.ApplyResources(comboBoxScale, "comboBoxScale");
            comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale.FormattingEnabled = true;
            comboBoxScale.Items.AddRange(new object[] { resources.GetString("comboBoxScale.Items"), resources.GetString("comboBoxScale.Items1"), resources.GetString("comboBoxScale.Items2"), resources.GetString("comboBoxScale.Items3") });
            comboBoxScale.Name = "comboBoxScale";
            toolTip.SetToolTip(comboBoxScale, resources.GetString("comboBoxScale.ToolTip"));
            comboBoxScale.SelectedIndexChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // trackBarIntensityBrightnessMax
            // 
            resources.ApplyResources(trackBarIntensityBrightnessMax, "trackBarIntensityBrightnessMax");
            trackBarIntensityBrightnessMax.LargeChange = 10000;
            trackBarIntensityBrightnessMax.Maximum = 1000000;
            trackBarIntensityBrightnessMax.Minimum = 1;
            trackBarIntensityBrightnessMax.Name = "trackBarIntensityBrightnessMax";
            trackBarIntensityBrightnessMax.SmallChange = 100000;
            trackBarIntensityBrightnessMax.TickFrequency = 20000;
            toolTip.SetToolTip(trackBarIntensityBrightnessMax, resources.GetString("trackBarIntensityBrightnessMax.ToolTip"));
            trackBarIntensityBrightnessMax.Value = 1000000;
            trackBarIntensityBrightnessMax.ValueChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // trackBarIntensityBrightnessMin
            // 
            resources.ApplyResources(trackBarIntensityBrightnessMin, "trackBarIntensityBrightnessMin");
            trackBarIntensityBrightnessMin.LargeChange = 10000;
            trackBarIntensityBrightnessMin.Maximum = 999999;
            trackBarIntensityBrightnessMin.Name = "trackBarIntensityBrightnessMin";
            trackBarIntensityBrightnessMin.SmallChange = 100000;
            trackBarIntensityBrightnessMin.TickFrequency = 20000;
            toolTip.SetToolTip(trackBarIntensityBrightnessMin, resources.GetString("trackBarIntensityBrightnessMin.ToolTip"));
            trackBarIntensityBrightnessMin.ValueChanged += trackBarIntensityBrightnessMax_ValueChanged;
            // 
            // labelBrightnessMax
            // 
            resources.ApplyResources(labelBrightnessMax, "labelBrightnessMax");
            labelBrightnessMax.Name = "labelBrightnessMax";
            toolTip.SetToolTip(labelBrightnessMax, resources.GetString("labelBrightnessMax.ToolTip"));
            // 
            // labelBrightnessMin
            // 
            resources.ApplyResources(labelBrightnessMin, "labelBrightnessMin");
            labelBrightnessMin.Name = "labelBrightnessMin";
            toolTip.SetToolTip(labelBrightnessMin, resources.GetString("labelBrightnessMin.ToolTip"));
            // 
            // labelBrightness
            // 
            resources.ApplyResources(labelBrightness, "labelBrightness");
            labelBrightness.Name = "labelBrightness";
            toolTip.SetToolTip(labelBrightness, resources.GetString("labelBrightness.ToolTip"));
            // 
            // checkBoxShowOverlays
            // 
            resources.ApplyResources(checkBoxShowOverlays, "checkBoxShowOverlays");
            checkBoxShowOverlays.Checked = true;
            checkBoxShowOverlays.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowOverlays.Name = "checkBoxShowOverlays";
            toolTip.SetToolTip(checkBoxShowOverlays, resources.GetString("checkBoxShowOverlays.ToolTip"));
            checkBoxShowOverlays.UseVisualStyleBackColor = true;
            checkBoxShowOverlays.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // buttonCopyImage
            // 
            resources.ApplyResources(buttonCopyImage, "buttonCopyImage");
            buttonCopyImage.Name = "buttonCopyImage";
            toolTip.SetToolTip(buttonCopyImage, resources.GetString("buttonCopyImage.ToolTip"));
            buttonCopyImage.UseVisualStyleBackColor = true;
            buttonCopyImage.Click += buttonCopyImage_Click;
            // 
            // numericBoxEnergyEnd
            // 
            resources.ApplyResources(numericBoxEnergyEnd, "numericBoxEnergyEnd");
            numericBoxEnergyEnd.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyEnd.DecimalPlaces = 2;
            numericBoxEnergyEnd.Maximum = 1000D;
            numericBoxEnergyEnd.Minimum = 0.001D;
            numericBoxEnergyEnd.Name = "numericBoxEnergyEnd";
            numericBoxEnergyEnd.ShowUpDown = true;
            numericBoxEnergyEnd.SmartIncrement = true;
            numericBoxEnergyEnd.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxEnergyEnd, resources.GetString("numericBoxEnergyEnd.ToolTip"));
            numericBoxEnergyEnd.Value = 15D;
            //numericBoxEnergyEnd.ValueBoxWidth = 42; // 260726Cl 旧: 最長値「1000.00」が数値欄に入らず切れていた (--diagnose ValueBoxClipped)
            numericBoxEnergyEnd.ValueBoxWidth = 54; // 260726Cl
            numericBoxEnergyEnd.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // numericBoxEnergyStart
            // 
            resources.ApplyResources(numericBoxEnergyStart, "numericBoxEnergyStart");
            numericBoxEnergyStart.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStart.DecimalPlaces = 2;
            numericBoxEnergyStart.Maximum = 1000D;
            numericBoxEnergyStart.Minimum = 1D;
            numericBoxEnergyStart.Name = "numericBoxEnergyStart";
            numericBoxEnergyStart.ShowUpDown = true;
            numericBoxEnergyStart.SmartIncrement = true;
            numericBoxEnergyStart.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxEnergyStart, resources.GetString("numericBoxEnergyStart.ToolTip"));
            numericBoxEnergyStart.Value = 20D;
            numericBoxEnergyStart.ValueBackColor = System.Drawing.SystemColors.Control;
            //numericBoxEnergyStart.ValueBoxWidth = 42; // 260726Cl 旧: 最長値「1000.00」が数値欄に入らず切れていた (--diagnose ValueBoxClipped)
            numericBoxEnergyStart.ValueBoxWidth = 54; // 260726Cl
            numericBoxEnergyStart.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // numericBoxEnergyStep
            // 
            resources.ApplyResources(numericBoxEnergyStep, "numericBoxEnergyStep");
            numericBoxEnergyStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEnergyStep.DecimalPlaces = 2;
            numericBoxEnergyStep.Maximum = 10D;
            numericBoxEnergyStep.Minimum = 0.001D;
            numericBoxEnergyStep.Name = "numericBoxEnergyStep";
            numericBoxEnergyStep.ShowUpDown = true;
            numericBoxEnergyStep.SmartIncrement = true;
            numericBoxEnergyStep.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxEnergyStep, resources.GetString("numericBoxEnergyStep.ToolTip"));
            numericBoxEnergyStep.Value = 1D;
            numericBoxEnergyStep.ValueBoxWidth = 38;
            numericBoxEnergyStep.ValueChanged += NumericBoxEnergyStart_ValueChanged;
            // 
            // labelBseDepth
            // 
            resources.ApplyResources(labelBseDepth, "labelBseDepth");
            labelBseDepth.BackColor = System.Drawing.SystemColors.Control;
            labelBseDepth.Name = "labelBseDepth";
            toolTip.SetToolTip(labelBseDepth, resources.GetString("labelBseDepth.ToolTip"));
            // 
            // labelBseDeltaE
            // 
            resources.ApplyResources(labelBseDeltaE, "labelBseDeltaE");
            labelBseDeltaE.BackColor = System.Drawing.SystemColors.Control;
            labelBseDeltaE.Name = "labelBseDeltaE";
            toolTip.SetToolTip(labelBseDeltaE, resources.GetString("labelBseDeltaE.ToolTip"));
            // 
            // checkBoxShowDyanmicalEBSD
            // 
            resources.ApplyResources(checkBoxShowDyanmicalEBSD, "checkBoxShowDyanmicalEBSD");
            checkBoxShowDyanmicalEBSD.Checked = true;
            checkBoxShowDyanmicalEBSD.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowDyanmicalEBSD.Name = "checkBoxShowDyanmicalEBSD";
            toolTip.SetToolTip(checkBoxShowDyanmicalEBSD, resources.GetString("checkBoxShowDyanmicalEBSD.ToolTip"));
            checkBoxShowDyanmicalEBSD.UseVisualStyleBackColor = true;
            checkBoxShowDyanmicalEBSD.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxDrawDetectorOutline
            // 
            resources.ApplyResources(checkBoxDrawDetectorOutline, "checkBoxDrawDetectorOutline");
            checkBoxDrawDetectorOutline.Checked = true;
            checkBoxDrawDetectorOutline.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDrawDetectorOutline.Name = "checkBoxDrawDetectorOutline";
            toolTip.SetToolTip(checkBoxDrawDetectorOutline, resources.GetString("checkBoxDrawDetectorOutline.ToolTip"));
            checkBoxDrawDetectorOutline.UseVisualStyleBackColor = true;
            checkBoxDrawDetectorOutline.CheckedChanged += checkBoxDrawDetectorOutline_CheckedChanged;
            // 
            // labelBseStereonetNote
            // 
            resources.ApplyResources(labelBseStereonetNote, "labelBseStereonetNote");
            labelBseStereonetNote.BackColor = System.Drawing.Color.White;
            labelBseStereonetNote.Name = "labelBseStereonetNote";
            toolTip.SetToolTip(labelBseStereonetNote, resources.GetString("labelBseStereonetNote.ToolTip"));
            // 
            // checkBoxShowKikuchiLines
            // 
            resources.ApplyResources(checkBoxShowKikuchiLines, "checkBoxShowKikuchiLines");
            checkBoxShowKikuchiLines.Checked = true;
            checkBoxShowKikuchiLines.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowKikuchiLines.Name = "checkBoxShowKikuchiLines";
            toolTip.SetToolTip(checkBoxShowKikuchiLines, resources.GetString("checkBoxShowKikuchiLines.ToolTip"));
            checkBoxShowKikuchiLines.UseVisualStyleBackColor = true;
            checkBoxShowKikuchiLines.CheckedChanged += checkBoxShowKikuchiLines_CheckedChanged;
            // 
            // checkBoxShowGIndices
            // 
            resources.ApplyResources(checkBoxShowGIndices, "checkBoxShowGIndices");
            checkBoxShowGIndices.Name = "checkBoxShowGIndices";
            toolTip.SetToolTip(checkBoxShowGIndices, resources.GetString("checkBoxShowGIndices.ToolTip"));
            checkBoxShowGIndices.UseVisualStyleBackColor = true;
            checkBoxShowGIndices.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxShowZoneAxisIndices
            // 
            resources.ApplyResources(checkBoxShowZoneAxisIndices, "checkBoxShowZoneAxisIndices");
            checkBoxShowZoneAxisIndices.Checked = true;
            checkBoxShowZoneAxisIndices.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowZoneAxisIndices.Name = "checkBoxShowZoneAxisIndices";
            toolTip.SetToolTip(checkBoxShowZoneAxisIndices, resources.GetString("checkBoxShowZoneAxisIndices.ToolTip"));
            checkBoxShowZoneAxisIndices.UseVisualStyleBackColor = true;
            checkBoxShowZoneAxisIndices.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // labelMasterPatternGrid
            // 
            resources.ApplyResources(labelMasterPatternGrid, "labelMasterPatternGrid");
            labelMasterPatternGrid.Name = "labelMasterPatternGrid";
            toolTip.SetToolTip(labelMasterPatternGrid, resources.GetString("labelMasterPatternGrid.ToolTip"));
            // 
            // comboBoxMasterPatternGrid
            // 
            resources.ApplyResources(comboBoxMasterPatternGrid, "comboBoxMasterPatternGrid");
            comboBoxMasterPatternGrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxMasterPatternGrid.FormattingEnabled = true;
            comboBoxMasterPatternGrid.Items.AddRange(new object[] { resources.GetString("comboBoxMasterPatternGrid.Items"), resources.GetString("comboBoxMasterPatternGrid.Items1"), resources.GetString("comboBoxMasterPatternGrid.Items2"), resources.GetString("comboBoxMasterPatternGrid.Items3"), resources.GetString("comboBoxMasterPatternGrid.Items4"), resources.GetString("comboBoxMasterPatternGrid.Items5"), resources.GetString("comboBoxMasterPatternGrid.Items6"), resources.GetString("comboBoxMasterPatternGrid.Items7"), resources.GetString("comboBoxMasterPatternGrid.Items8"), resources.GetString("comboBoxMasterPatternGrid.Items9") });
            comboBoxMasterPatternGrid.Name = "comboBoxMasterPatternGrid";
            toolTip.SetToolTip(comboBoxMasterPatternGrid, resources.GetString("comboBoxMasterPatternGrid.ToolTip"));
            comboBoxMasterPatternGrid.SelectedIndexChanged += MasterPatternSelectionChanged;
            // 
            // numericBoxMasterPatternEnergy
            // 
            resources.ApplyResources(numericBoxMasterPatternEnergy, "numericBoxMasterPatternEnergy");
            numericBoxMasterPatternEnergy.BackColor = System.Drawing.Color.Transparent;
            numericBoxMasterPatternEnergy.Name = "numericBoxMasterPatternEnergy";
            numericBoxMasterPatternEnergy.ReadOnly = true;
            toolTip.SetToolTip(numericBoxMasterPatternEnergy, resources.GetString("numericBoxMasterPatternEnergy.ToolTip"));
            numericBoxMasterPatternEnergy.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarMasterPatternEnergy
            // 
            resources.ApplyResources(trackBarMasterPatternEnergy, "trackBarMasterPatternEnergy");
            trackBarMasterPatternEnergy.LargeChange = 1;
            trackBarMasterPatternEnergy.Maximum = 0;
            trackBarMasterPatternEnergy.Name = "trackBarMasterPatternEnergy";
            toolTip.SetToolTip(trackBarMasterPatternEnergy, resources.GetString("trackBarMasterPatternEnergy.ToolTip"));
            trackBarMasterPatternEnergy.ValueChanged += MasterPatternSelectionChanged;
            // 
            // numericBoxMasterPatternDepth
            // 
            resources.ApplyResources(numericBoxMasterPatternDepth, "numericBoxMasterPatternDepth");
            numericBoxMasterPatternDepth.BackColor = System.Drawing.Color.Transparent;
            numericBoxMasterPatternDepth.Name = "numericBoxMasterPatternDepth";
            numericBoxMasterPatternDepth.ReadOnly = true;
            toolTip.SetToolTip(numericBoxMasterPatternDepth, resources.GetString("numericBoxMasterPatternDepth.ToolTip"));
            numericBoxMasterPatternDepth.ValueBackColor = System.Drawing.SystemColors.Control;
            // 
            // trackBarMasterPatternDepth
            // 
            resources.ApplyResources(trackBarMasterPatternDepth, "trackBarMasterPatternDepth");
            trackBarMasterPatternDepth.LargeChange = 1;
            trackBarMasterPatternDepth.Maximum = 0;
            trackBarMasterPatternDepth.Name = "trackBarMasterPatternDepth";
            toolTip.SetToolTip(trackBarMasterPatternDepth, resources.GetString("trackBarMasterPatternDepth.ToolTip"));
            trackBarMasterPatternDepth.ValueChanged += MasterPatternSelectionChanged;
            // 
            // labelMasterPattern2DHemisphere
            // 
            resources.ApplyResources(labelMasterPattern2DHemisphere, "labelMasterPattern2DHemisphere");
            labelMasterPattern2DHemisphere.Name = "labelMasterPattern2DHemisphere";
            toolTip.SetToolTip(labelMasterPattern2DHemisphere, resources.GetString("labelMasterPattern2DHemisphere.ToolTip"));
            // 
            // comboBoxMasterPattern2DHemisphere
            // 
            resources.ApplyResources(comboBoxMasterPattern2DHemisphere, "comboBoxMasterPattern2DHemisphere");
            comboBoxMasterPattern2DHemisphere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxMasterPattern2DHemisphere.FormattingEnabled = true;
            comboBoxMasterPattern2DHemisphere.Items.AddRange(new object[] { resources.GetString("comboBoxMasterPattern2DHemisphere.Items"), resources.GetString("comboBoxMasterPattern2DHemisphere.Items1") });
            comboBoxMasterPattern2DHemisphere.Name = "comboBoxMasterPattern2DHemisphere";
            toolTip.SetToolTip(comboBoxMasterPattern2DHemisphere, resources.GetString("comboBoxMasterPattern2DHemisphere.ToolTip"));
            comboBoxMasterPattern2DHemisphere.SelectedIndexChanged += MasterPatternSelectionChanged;
            // 
            // buttonCreateMasterPattern
            // 
            resources.ApplyResources(buttonCreateMasterPattern, "buttonCreateMasterPattern");
            buttonCreateMasterPattern.BackColor = System.Drawing.Color.SteelBlue;
            buttonCreateMasterPattern.ForeColor = System.Drawing.Color.White;
            buttonCreateMasterPattern.Name = "buttonCreateMasterPattern";
            toolTip.SetToolTip(buttonCreateMasterPattern, resources.GetString("buttonCreateMasterPattern.ToolTip"));
            buttonCreateMasterPattern.UseVisualStyleBackColor = false;
            buttonCreateMasterPattern.Click += buttonCreateMasterPattern_Click;
            // 
            // buttonMasterPattern2DCopy
            // 
            resources.ApplyResources(buttonMasterPattern2DCopy, "buttonMasterPattern2DCopy");
            buttonMasterPattern2DCopy.Name = "buttonMasterPattern2DCopy";
            toolTip.SetToolTip(buttonMasterPattern2DCopy, resources.GetString("buttonMasterPattern2DCopy.ToolTip"));
            buttonMasterPattern2DCopy.UseVisualStyleBackColor = true;
            buttonMasterPattern2DCopy.Click += buttonMasterPattern2DCopy_Click;
            // 
            // buttonMasterPattern3DCopy
            // 
            resources.ApplyResources(buttonMasterPattern3DCopy, "buttonMasterPattern3DCopy");
            buttonMasterPattern3DCopy.Name = "buttonMasterPattern3DCopy";
            toolTip.SetToolTip(buttonMasterPattern3DCopy, resources.GetString("buttonMasterPattern3DCopy.ToolTip"));
            buttonMasterPattern3DCopy.UseVisualStyleBackColor = true;
            buttonMasterPattern3DCopy.Click += buttonMasterPattern3DCopy_Click;
            // 
            // checkBoxMasterPattern3DAxisLabel
            // 
            resources.ApplyResources(checkBoxMasterPattern3DAxisLabel, "checkBoxMasterPattern3DAxisLabel");
            checkBoxMasterPattern3DAxisLabel.Checked = true;
            checkBoxMasterPattern3DAxisLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxMasterPattern3DAxisLabel.Name = "checkBoxMasterPattern3DAxisLabel";
            toolTip.SetToolTip(checkBoxMasterPattern3DAxisLabel, resources.GetString("checkBoxMasterPattern3DAxisLabel.ToolTip"));
            checkBoxMasterPattern3DAxisLabel.UseVisualStyleBackColor = true;
            checkBoxMasterPattern3DAxisLabel.CheckedChanged += checkBoxMasterPattern3DAxisLabel_CheckedChanged;
            // 
            // buttonMasterPattern3DViewAlong
            // 
            resources.ApplyResources(buttonMasterPattern3DViewAlong, "buttonMasterPattern3DViewAlong");
            buttonMasterPattern3DViewAlong.Name = "buttonMasterPattern3DViewAlong";
            toolTip.SetToolTip(buttonMasterPattern3DViewAlong, resources.GetString("buttonMasterPattern3DViewAlong.ToolTip"));
            buttonMasterPattern3DViewAlong.UseVisualStyleBackColor = true;
            buttonMasterPattern3DViewAlong.Click += buttonMasterPattern3DViewAlong_Click;
            // 
            // indexControl
            // 
            resources.ApplyResources(indexControl, "indexControl");
            indexControl.LabelVisible = false;
            indexControl.Name = "indexControl";
            toolTip.SetToolTip(indexControl, resources.GetString("indexControl.ToolTip"));
            // 
            // checkBoxShowMesh
            // 
            resources.ApplyResources(checkBoxShowMesh, "checkBoxShowMesh");
            checkBoxShowMesh.Name = "checkBoxShowMesh";
            toolTip.SetToolTip(checkBoxShowMesh, resources.GetString("checkBoxShowMesh.ToolTip"));
            checkBoxShowMesh.UseVisualStyleBackColor = true;
            checkBoxShowMesh.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // checkBoxShowCircle
            // 
            resources.ApplyResources(checkBoxShowCircle, "checkBoxShowCircle");
            checkBoxShowCircle.Checked = true;
            checkBoxShowCircle.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowCircle.Name = "checkBoxShowCircle";
            toolTip.SetToolTip(checkBoxShowCircle, resources.GetString("checkBoxShowCircle.ToolTip"));
            checkBoxShowCircle.UseVisualStyleBackColor = true;
            checkBoxShowCircle.CheckedChanged += colorControlExcessLine_ColorChanged;
            // 
            // labelExpBrightness
            // 
            resources.ApplyResources(labelExpBrightness, "labelExpBrightness");
            labelExpBrightness.Name = "labelExpBrightness";
            toolTip.SetToolTip(labelExpBrightness, resources.GetString("labelExpBrightness.ToolTip"));
            // 
            // checkBoxMasterPattern3DAxisArrows
            // 
            resources.ApplyResources(checkBoxMasterPattern3DAxisArrows, "checkBoxMasterPattern3DAxisArrows");
            checkBoxMasterPattern3DAxisArrows.Checked = true;
            checkBoxMasterPattern3DAxisArrows.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxMasterPattern3DAxisArrows.Name = "checkBoxMasterPattern3DAxisArrows";
            toolTip.SetToolTip(checkBoxMasterPattern3DAxisArrows, resources.GetString("checkBoxMasterPattern3DAxisArrows.ToolTip"));
            checkBoxMasterPattern3DAxisArrows.UseVisualStyleBackColor = true;
            checkBoxMasterPattern3DAxisArrows.CheckedChanged += checkBoxMasterPattern3DAxisArrows_CheckedChanged;
            // 
            // checkBoxFlipDetectorLeftRight
            // 
            resources.ApplyResources(checkBoxFlipDetectorLeftRight, "checkBoxFlipDetectorLeftRight");
            checkBoxFlipDetectorLeftRight.Name = "checkBoxFlipDetectorLeftRight";
            toolTip.SetToolTip(checkBoxFlipDetectorLeftRight, resources.GetString("checkBoxFlipDetectorLeftRight.ToolTip"));
            checkBoxFlipDetectorLeftRight.UseVisualStyleBackColor = true;
            checkBoxFlipDetectorLeftRight.CheckedChanged += checkBoxFlipDetectorLeftRight_CheckedChanged;
            // 
            // checkBoxShowExperimentalImage
            // 
            resources.ApplyResources(checkBoxShowExperimentalImage, "checkBoxShowExperimentalImage");
            checkBoxShowExperimentalImage.Checked = true;
            checkBoxShowExperimentalImage.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowExperimentalImage.Name = "checkBoxShowExperimentalImage";
            toolTip.SetToolTip(checkBoxShowExperimentalImage, resources.GetString("checkBoxShowExperimentalImage.ToolTip"));
            checkBoxShowExperimentalImage.UseVisualStyleBackColor = true;
            checkBoxShowExperimentalImage.CheckedChanged += checkBoxShowExperimentalImage_CheckedChanged;
            // 
            // labelExpMinInt
            // 
            resources.ApplyResources(labelExpMinInt, "labelExpMinInt");
            labelExpMinInt.Name = "labelExpMinInt";
            toolTip.SetToolTip(labelExpMinInt, resources.GetString("labelExpMinInt.ToolTip"));
            // 
            // trackBarExpImageMinInt
            // 
            resources.ApplyResources(trackBarExpImageMinInt, "trackBarExpImageMinInt");
            trackBarExpImageMinInt.LargeChange = 50;
            trackBarExpImageMinInt.Maximum = 1000;
            trackBarExpImageMinInt.Name = "trackBarExpImageMinInt";
            trackBarExpImageMinInt.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarExpImageMinInt, resources.GetString("trackBarExpImageMinInt.ToolTip"));
            trackBarExpImageMinInt.ValueChanged += trackBarExpImageIntensity_ValueChanged;
            // 
            // labelExpMaxInt
            // 
            resources.ApplyResources(labelExpMaxInt, "labelExpMaxInt");
            labelExpMaxInt.Name = "labelExpMaxInt";
            toolTip.SetToolTip(labelExpMaxInt, resources.GetString("labelExpMaxInt.ToolTip"));
            // 
            // trackBarExpImageMaxInt
            // 
            resources.ApplyResources(trackBarExpImageMaxInt, "trackBarExpImageMaxInt");
            trackBarExpImageMaxInt.LargeChange = 50;
            trackBarExpImageMaxInt.Maximum = 1000;
            trackBarExpImageMaxInt.Minimum = 1;
            trackBarExpImageMaxInt.Name = "trackBarExpImageMaxInt";
            trackBarExpImageMaxInt.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarExpImageMaxInt, resources.GetString("trackBarExpImageMaxInt.ToolTip"));
            trackBarExpImageMaxInt.Value = 1000;
            trackBarExpImageMaxInt.ValueChanged += trackBarExpImageIntensity_ValueChanged;
            // 
            // labelExpOpacity
            // 
            resources.ApplyResources(labelExpOpacity, "labelExpOpacity");
            labelExpOpacity.Name = "labelExpOpacity";
            toolTip.SetToolTip(labelExpOpacity, resources.GetString("labelExpOpacity.ToolTip"));
            // 
            // trackBarExpImageOpacity
            // 
            resources.ApplyResources(trackBarExpImageOpacity, "trackBarExpImageOpacity");
            trackBarExpImageOpacity.LargeChange = 10;
            trackBarExpImageOpacity.Maximum = 100;
            trackBarExpImageOpacity.Name = "trackBarExpImageOpacity";
            trackBarExpImageOpacity.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarExpImageOpacity, resources.GetString("trackBarExpImageOpacity.ToolTip"));
            trackBarExpImageOpacity.Value = 100;
            trackBarExpImageOpacity.ValueChanged += trackBarExpImageOpacity_ValueChanged;
            // 
            // radioButtonIndexingRadon
            // 
            resources.ApplyResources(radioButtonIndexingRadon, "radioButtonIndexingRadon");
            radioButtonIndexingRadon.Checked = true;
            radioButtonIndexingRadon.Name = "radioButtonIndexingRadon";
            radioButtonIndexingRadon.TabStop = true;
            toolTip.SetToolTip(radioButtonIndexingRadon, resources.GetString("radioButtonIndexingRadon.ToolTip"));
            radioButtonIndexingRadon.UseVisualStyleBackColor = true;
            // 
            // radioButtonIndexingDictionary
            // 
            resources.ApplyResources(radioButtonIndexingDictionary, "radioButtonIndexingDictionary");
            radioButtonIndexingDictionary.Name = "radioButtonIndexingDictionary";
            toolTip.SetToolTip(radioButtonIndexingDictionary, resources.GetString("radioButtonIndexingDictionary.ToolTip"));
            radioButtonIndexingDictionary.UseVisualStyleBackColor = true;
            // 
            // buttonFindOrientation
            // 
            resources.ApplyResources(buttonFindOrientation, "buttonFindOrientation");
            buttonFindOrientation.Name = "buttonFindOrientation";
            toolTip.SetToolTip(buttonFindOrientation, resources.GetString("buttonFindOrientation.ToolTip"));
            buttonFindOrientation.UseVisualStyleBackColor = true;
            buttonFindOrientation.Click += buttonFindOrientation_Click;
            // 
            // dataGridViewEbsdCandidates
            // 
            resources.ApplyResources(dataGridViewEbsdCandidates, "dataGridViewEbsdCandidates");
            dataGridViewEbsdCandidates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewEbsdCandidates.Name = "dataGridViewEbsdCandidates";
            toolTip.SetToolTip(dataGridViewEbsdCandidates, resources.GetString("dataGridViewEbsdCandidates.ToolTip"));
            // 
            // buttonCalibrateGeometry
            // 
            resources.ApplyResources(buttonCalibrateGeometry, "buttonCalibrateGeometry");
            buttonCalibrateGeometry.Name = "buttonCalibrateGeometry";
            toolTip.SetToolTip(buttonCalibrateGeometry, resources.GetString("buttonCalibrateGeometry.ToolTip"));
            buttonCalibrateGeometry.UseVisualStyleBackColor = true;
            buttonCalibrateGeometry.Click += buttonCalibrateGeometry_Click;
            // 
            // checkBoxMatchDetectorResolution
            // 
            resources.ApplyResources(checkBoxMatchDetectorResolution, "checkBoxMatchDetectorResolution");
            checkBoxMatchDetectorResolution.Name = "checkBoxMatchDetectorResolution";
            toolTip.SetToolTip(checkBoxMatchDetectorResolution, resources.GetString("checkBoxMatchDetectorResolution.ToolTip"));
            checkBoxMatchDetectorResolution.UseVisualStyleBackColor = true;
            // 
            // radioButtonCopyCurrent
            // 
            resources.ApplyResources(radioButtonCopyCurrent, "radioButtonCopyCurrent");
            radioButtonCopyCurrent.Checked = true;
            radioButtonCopyCurrent.Name = "radioButtonCopyCurrent";
            radioButtonCopyCurrent.TabStop = true;
            toolTip.SetToolTip(radioButtonCopyCurrent, resources.GetString("radioButtonCopyCurrent.ToolTip"));
            radioButtonCopyCurrent.UseVisualStyleBackColor = true;
            // 
            // radioButtonDetector
            // 
            resources.ApplyResources(radioButtonDetector, "radioButtonDetector");
            radioButtonDetector.Name = "radioButtonDetector";
            toolTip.SetToolTip(radioButtonDetector, resources.GetString("radioButtonDetector.ToolTip"));
            radioButtonDetector.UseVisualStyleBackColor = true;
            // 
            // radioButtonCopyEmf
            // 
            resources.ApplyResources(radioButtonCopyEmf, "radioButtonCopyEmf");
            radioButtonCopyEmf.Checked = true;
            radioButtonCopyEmf.Name = "radioButtonCopyEmf";
            radioButtonCopyEmf.TabStop = true;
            toolTip.SetToolTip(radioButtonCopyEmf, resources.GetString("radioButtonCopyEmf.ToolTip"));
            radioButtonCopyEmf.UseVisualStyleBackColor = true;
            // 
            // radioButtonCopyBmp
            // 
            resources.ApplyResources(radioButtonCopyBmp, "radioButtonCopyBmp");
            radioButtonCopyBmp.Name = "radioButtonCopyBmp";
            toolTip.SetToolTip(radioButtonCopyBmp, resources.GetString("radioButtonCopyBmp.ToolTip"));
            radioButtonCopyBmp.UseVisualStyleBackColor = true;
            // 
            // panelGeometry
            // 
            resources.ApplyResources(panelGeometry, "panelGeometry");
            captureExtender.SetCapture(panelGeometry, true);
            panelGeometry.Name = "panelGeometry";
            toolTip.SetToolTip(panelGeometry, resources.GetString("panelGeometry.ToolTip"));
            // 
            // flowLayoutPanelViewAlong
            // 
            resources.ApplyResources(flowLayoutPanelViewAlong, "flowLayoutPanelViewAlong");
            flowLayoutPanelViewAlong.Controls.Add(buttonViewQuarter);
            flowLayoutPanelViewAlong.Controls.Add(buttonViewFromSurfaceNormal);
            flowLayoutPanelViewAlong.Controls.Add(buttonFromX);
            flowLayoutPanelViewAlong.Controls.Add(buttonViewFromZ);
            flowLayoutPanelViewAlong.Name = "flowLayoutPanelViewAlong";
            toolTip.SetToolTip(flowLayoutPanelViewAlong, resources.GetString("flowLayoutPanelViewAlong.ToolTip"));
            // 
            // graphControlDepthProfile
            // 
            resources.ApplyResources(graphControlDepthProfile, "graphControlDepthProfile");
            graphControlDepthProfile.Name = "graphControlDepthProfile";
            toolTip.SetToolTip(graphControlDepthProfile, resources.GetString("graphControlDepthProfile.ToolTip"));
            // 
            // poleFigureControl
            // 
            resources.ApplyResources(poleFigureControl, "poleFigureControl");
            poleFigureControl.Name = "poleFigureControl";
            toolTip.SetToolTip(poleFigureControl, resources.GetString("poleFigureControl.ToolTip"));
            // 
            // graphControlEnergyProfile
            // 
            resources.ApplyResources(graphControlEnergyProfile, "graphControlEnergyProfile");
            graphControlEnergyProfile.Name = "graphControlEnergyProfile";
            toolTip.SetToolTip(graphControlEnergyProfile, resources.GetString("graphControlEnergyProfile.ToolTip"));
            // 
            // graphicsBox
            // 
            resources.ApplyResources(graphicsBox, "graphicsBox");
            graphicsBox.AllowDrop = true;
            graphicsBox.BackColor = System.Drawing.Color.Transparent;
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            graphicsBox.Fonts = new System.Drawing.Font("Segoe UI", 9.75F);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.TabStop = false;
            toolTip.SetToolTip(graphicsBox, resources.GetString("graphicsBox.ToolTip"));
            graphicsBox.PaintOverlay += graphicsBox_PaintOverlay;
            graphicsBox.DragDrop += FormEBSD_DragDrop;
            graphicsBox.DragEnter += FormEBSD_DragEnter;
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            graphicsBox.MouseUp += graphicsBox_MouseUp;
            graphicsBox.Resize += graphicsBox_Resize;
            // 
            // flowLayoutPanelColorScale
            // 
            resources.ApplyResources(flowLayoutPanelColorScale, "flowLayoutPanelColorScale");
            flowLayoutPanelColorScale.Controls.Add(labelPolarity);
            flowLayoutPanelColorScale.Controls.Add(comboBoxGradient);
            flowLayoutPanelColorScale.Controls.Add(labelColor);
            flowLayoutPanelColorScale.Controls.Add(comboBoxScale);
            flowLayoutPanelColorScale.Name = "flowLayoutPanelColorScale";
            toolTip.SetToolTip(flowLayoutPanelColorScale, resources.GetString("flowLayoutPanelColorScale.ToolTip"));
            // 
            // flowLayoutPanelBrightness
            // 
            resources.ApplyResources(flowLayoutPanelBrightness, "flowLayoutPanelBrightness");
            flowLayoutPanelBrightness.Controls.Add(labelBrightness);
            flowLayoutPanelBrightness.Controls.Add(labelBrightnessMin);
            flowLayoutPanelBrightness.Controls.Add(trackBarIntensityBrightnessMin);
            flowLayoutPanelBrightness.Controls.Add(labelBrightnessMax);
            flowLayoutPanelBrightness.Controls.Add(trackBarIntensityBrightnessMax);
            flowLayoutPanelBrightness.Name = "flowLayoutPanelBrightness";
            toolTip.SetToolTip(flowLayoutPanelBrightness, resources.GetString("flowLayoutPanelBrightness.ToolTip"));
            // 
            // flowLayoutPanelOutputRange
            // 
            resources.ApplyResources(flowLayoutPanelOutputRange, "flowLayoutPanelOutputRange");
            flowLayoutPanelOutputRange.Controls.Add(numericBoxEnergy);
            flowLayoutPanelOutputRange.Controls.Add(trackBarOutputEnergy);
            flowLayoutPanelOutputRange.Controls.Add(numericBoxDepth);
            flowLayoutPanelOutputRange.Controls.Add(trackBarOutputThickness);
            flowLayoutPanelOutputRange.Name = "flowLayoutPanelOutputRange";
            toolTip.SetToolTip(flowLayoutPanelOutputRange, resources.GetString("flowLayoutPanelOutputRange.ToolTip"));
            // 
            // flowLayoutPanelWithBseDistribution
            // 
            resources.ApplyResources(flowLayoutPanelWithBseDistribution, "flowLayoutPanelWithBseDistribution");
            flowLayoutPanelWithBseDistribution.Controls.Add(checkBoxWithBSEDistribution);
            flowLayoutPanelWithBseDistribution.Name = "flowLayoutPanelWithBseDistribution";
            toolTip.SetToolTip(flowLayoutPanelWithBseDistribution, resources.GetString("flowLayoutPanelWithBseDistribution.ToolTip"));
            // 
            // tabControlSettings
            // 
            resources.ApplyResources(tabControlSettings, "tabControlSettings");
            tabControlSettings.Controls.Add(tabPageGeometry);
            tabControlSettings.Controls.Add(tabPageBseDistribution);
            tabControlSettings.Controls.Add(tabPageOverlays);
            tabControlSettings.HotTrack = true;
            tabControlSettings.Multiline = true;
            tabControlSettings.Name = "tabControlSettings";
            tabControlSettings.SelectedIndex = 0;
            toolTip.SetToolTip(tabControlSettings, resources.GetString("tabControlSettings.ToolTip"));
            // 
            // tabPageGeometry
            // 
            resources.ApplyResources(tabPageGeometry, "tabPageGeometry");
            tabPageGeometry.BackColor = System.Drawing.SystemColors.Control;
            tabPageGeometry.Controls.Add(panelGeometry);
            tabPageGeometry.Controls.Add(groupBoxEBSDGeometry);
            tabPageGeometry.Controls.Add(groupBoxSampleCondition);
            tabPageGeometry.Controls.Add(flowLayoutPanelViewAlong);
            tabPageGeometry.Name = "tabPageGeometry";
            toolTip.SetToolTip(tabPageGeometry, resources.GetString("tabPageGeometry.ToolTip"));
            // 
            // groupBoxEBSDGeometry
            // 
            resources.ApplyResources(groupBoxEBSDGeometry, "groupBoxEBSDGeometry");
            captureExtender.SetCapture(groupBoxEBSDGeometry, true);
            groupBoxEBSDGeometry.Controls.Add(flowLayoutPanelDetectorGeometry);
            groupBoxEBSDGeometry.Name = "groupBoxEBSDGeometry";
            groupBoxEBSDGeometry.TabStop = false;
            toolTip.SetToolTip(groupBoxEBSDGeometry, resources.GetString("groupBoxEBSDGeometry.ToolTip"));
            // 
            // flowLayoutPanelDetectorGeometry
            // 
            resources.ApplyResources(flowLayoutPanelDetectorGeometry, "flowLayoutPanelDetectorGeometry");
            flowLayoutPanelDetectorGeometry.Controls.Add(flowLayoutPanelDetectorSizeTilt);
            flowLayoutPanelDetectorGeometry.Controls.Add(flowLayoutPanelDetectorPosition);
            flowLayoutPanelDetectorGeometry.Name = "flowLayoutPanelDetectorGeometry";
            toolTip.SetToolTip(flowLayoutPanelDetectorGeometry, resources.GetString("flowLayoutPanelDetectorGeometry.ToolTip"));
            // 
            // flowLayoutPanelDetectorSizeTilt
            // 
            resources.ApplyResources(flowLayoutPanelDetectorSizeTilt, "flowLayoutPanelDetectorSizeTilt");
            flowLayoutPanelDetectorSizeTilt.Controls.Add(labelDetectorSizeTilt);
            flowLayoutPanelDetectorSizeTilt.Controls.Add(numericBoxDetTilt);
            flowLayoutPanelDetectorSizeTilt.Controls.Add(numericBoxDetWidth);
            flowLayoutPanelDetectorSizeTilt.Controls.Add(numericBoxDetHeight);
            flowLayoutPanelDetectorSizeTilt.Controls.Add(labelDetectorResolution);
            flowLayoutPanelDetectorSizeTilt.Controls.Add(numericBoxDetResolution);
            flowLayoutPanelDetectorSizeTilt.Name = "flowLayoutPanelDetectorSizeTilt";
            toolTip.SetToolTip(flowLayoutPanelDetectorSizeTilt, resources.GetString("flowLayoutPanelDetectorSizeTilt.ToolTip"));
            // 
            // labelDetectorSizeTilt
            // 
            resources.ApplyResources(labelDetectorSizeTilt, "labelDetectorSizeTilt");
            labelDetectorSizeTilt.Name = "labelDetectorSizeTilt";
            toolTip.SetToolTip(labelDetectorSizeTilt, resources.GetString("labelDetectorSizeTilt.ToolTip"));
            // 
            // numericBoxDetWidth
            // 
            resources.ApplyResources(numericBoxDetWidth, "numericBoxDetWidth");
            numericBoxDetWidth.BackColor = System.Drawing.Color.Transparent;
            numericBoxDetWidth.DecimalPlaces = 0; // 260726Cl 追加: ピクセル数なので整数。既定 -1 (general) だと計算値がフル桁で入り得る
            numericBoxDetWidth.Maximum = 4096D;
            numericBoxDetWidth.Minimum = 1D;
            numericBoxDetWidth.Name = "numericBoxDetWidth";
            numericBoxDetWidth.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxDetWidth, resources.GetString("numericBoxDetWidth.ToolTip"));
            numericBoxDetWidth.UpDown_Increment = 10D;
            numericBoxDetWidth.Value = 400D;
            //numericBoxDetWidth.ValueBoxWidth = 36; // 260726Cl 旧: 隣のヘッダに幅を回すため縮小。Maximum=4096 (4桁) なので要実機確認
            numericBoxDetWidth.ValueBoxWidth = 34; // 260726Cl
            numericBoxDetWidth.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // numericBoxDetHeight
            // 
            resources.ApplyResources(numericBoxDetHeight, "numericBoxDetHeight");
            numericBoxDetHeight.BackColor = System.Drawing.Color.Transparent;
            numericBoxDetHeight.DecimalPlaces = 0; // 260726Cl 追加: ピクセル数なので整数。既定 -1 (general) だと計算値がフル桁で入り得る
            numericBoxDetHeight.Maximum = 4096D;
            numericBoxDetHeight.Minimum = 1D;
            numericBoxDetHeight.Name = "numericBoxDetHeight";
            numericBoxDetHeight.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxDetHeight, resources.GetString("numericBoxDetHeight.ToolTip"));
            numericBoxDetHeight.UpDown_Increment = 10D;
            numericBoxDetHeight.Value = 400D;
            //numericBoxDetHeight.ValueBoxWidth = 36; // 260726Cl 旧: 隣のヘッダに幅を回すため縮小。Maximum=4096 (4桁) なので要実機確認
            numericBoxDetHeight.ValueBoxWidth = 34; // 260726Cl
            numericBoxDetHeight.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // labelDetectorResolution
            // 
            resources.ApplyResources(labelDetectorResolution, "labelDetectorResolution");
            labelDetectorResolution.Name = "labelDetectorResolution";
            toolTip.SetToolTip(labelDetectorResolution, resources.GetString("labelDetectorResolution.ToolTip"));
            // 
            // numericBoxDetResolution
            // 
            resources.ApplyResources(numericBoxDetResolution, "numericBoxDetResolution");
            numericBoxDetResolution.BackColor = System.Drawing.Color.Transparent;
            numericBoxDetResolution.DecimalPlaces = 3;
            numericBoxDetResolution.Maximum = 10D;
            numericBoxDetResolution.Minimum = 0.0001D;
            numericBoxDetResolution.Name = "numericBoxDetResolution";
            toolTip.SetToolTip(numericBoxDetResolution, resources.GetString("numericBoxDetResolution.ToolTip"));
            numericBoxDetResolution.UpDown_Increment = 0.01D;
            numericBoxDetResolution.Value = 0.1D;
            //numericBoxDetResolution.ValueBoxWidth = 50; // 260726Cl 旧: 隣のヘッダに幅を回すため縮小。Maximum=10・DecimalPlaces=3 (「10.000」) なので要実機確認
            //numericBoxDetResolution.ValueBoxWidth = 42; // 260726Cl 旧: 最長値「10.000」が数値欄に入らず切れていた (--diagnose ValueBoxClipped)
            numericBoxDetResolution.ValueBoxWidth = 48; // 260726Cl // 260726Cl
            numericBoxDetResolution.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // flowLayoutPanelDetectorPosition
            // 
            resources.ApplyResources(flowLayoutPanelDetectorPosition, "flowLayoutPanelDetectorPosition");
            flowLayoutPanelDetectorPosition.Controls.Add(labelDetectorCenter);
            flowLayoutPanelDetectorPosition.Controls.Add(numericBoxXofDet);
            flowLayoutPanelDetectorPosition.Controls.Add(numericBoxYofDet);
            flowLayoutPanelDetectorPosition.Controls.Add(numericBoxZofDet);
            flowLayoutPanelDetectorPosition.Name = "flowLayoutPanelDetectorPosition";
            toolTip.SetToolTip(flowLayoutPanelDetectorPosition, resources.GetString("flowLayoutPanelDetectorPosition.ToolTip"));
            // 
            // labelDetectorCenter
            // 
            resources.ApplyResources(labelDetectorCenter, "labelDetectorCenter");
            labelDetectorCenter.Name = "labelDetectorCenter";
            toolTip.SetToolTip(labelDetectorCenter, resources.GetString("labelDetectorCenter.ToolTip"));
            // 
            // numericBoxXofDet
            // 
            resources.ApplyResources(numericBoxXofDet, "numericBoxXofDet");
            numericBoxXofDet.BackColor = System.Drawing.Color.Transparent;
            numericBoxXofDet.DecimalPlaces = 3;
            numericBoxXofDet.Maximum = 1000D;
            numericBoxXofDet.Minimum = -1000D;
            numericBoxXofDet.Name = "numericBoxXofDet";
            numericBoxXofDet.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxXofDet, resources.GetString("numericBoxXofDet.ToolTip"));
            //numericBoxXofDet.ValueBoxWidth = 52; // 260726Cl 旧: CJK フォントだと「-35.000」の末尾が切れる
            numericBoxXofDet.ValueBoxWidth = 62; // 260726Cl
            numericBoxXofDet.ValueChanged += numericBoxDetectorGeometry_ValueChanged;
            // 
            // groupBoxSampleCondition
            // 
            resources.ApplyResources(groupBoxSampleCondition, "groupBoxSampleCondition");
            captureExtender.SetCapture(groupBoxSampleCondition, true);
            groupBoxSampleCondition.Controls.Add(flowLayoutPanelSampleCondition);
            groupBoxSampleCondition.Name = "groupBoxSampleCondition";
            groupBoxSampleCondition.TabStop = false;
            toolTip.SetToolTip(groupBoxSampleCondition, resources.GetString("groupBoxSampleCondition.ToolTip"));
            // 
            // flowLayoutPanelSampleCondition
            // 
            resources.ApplyResources(flowLayoutPanelSampleCondition, "flowLayoutPanelSampleCondition");
            flowLayoutPanelSampleCondition.Controls.Add(waveLengthControl);
            flowLayoutPanelSampleCondition.Controls.Add(numericBoxSampleTilt);
            flowLayoutPanelSampleCondition.Name = "flowLayoutPanelSampleCondition";
            toolTip.SetToolTip(flowLayoutPanelSampleCondition, resources.GetString("flowLayoutPanelSampleCondition.ToolTip"));
            // 
            // tabPageBseDistribution
            // 
            resources.ApplyResources(tabPageBseDistribution, "tabPageBseDistribution");
            tabPageBseDistribution.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageBseDistribution, true);
            tabPageBseDistribution.Controls.Add(tableLayoutPanel1);
            tabPageBseDistribution.Controls.Add(labelBseStereonetNote);
            tabPageBseDistribution.Controls.Add(buttonSimulateBSE);
            tabPageBseDistribution.Controls.Add(checkBoxDrawAxesInStereonet);
            tabPageBseDistribution.Controls.Add(poleFigureControl);
            tabPageBseDistribution.Name = "tabPageBseDistribution";
            toolTip.SetToolTip(tabPageBseDistribution, resources.GetString("tabPageBseDistribution.ToolTip"));
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(graphControlDepthProfile, 0, 3);
            tableLayoutPanel1.Controls.Add(graphControlEnergyProfile, 0, 1);
            tableLayoutPanel1.Controls.Add(labelBseDeltaE, 0, 0);
            tableLayoutPanel1.Controls.Add(labelBseDepth, 0, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            toolTip.SetToolTip(tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
            // 
            // tabPageOverlays
            // 
            resources.ApplyResources(tabPageOverlays, "tabPageOverlays");
            tabPageOverlays.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageOverlays, true);
            tabPageOverlays.Controls.Add(flowLayoutPanelOverlays);
            tabPageOverlays.Name = "tabPageOverlays";
            toolTip.SetToolTip(tabPageOverlays, resources.GetString("tabPageOverlays.ToolTip"));
            // 
            // flowLayoutPanelOverlays
            // 
            resources.ApplyResources(flowLayoutPanelOverlays, "flowLayoutPanelOverlays");
            flowLayoutPanelOverlays.Controls.Add(colorControlBackGround);
            flowLayoutPanelOverlays.Controls.Add(checkBoxDrawDetectorOutline);
            flowLayoutPanelOverlays.Controls.Add(flowLayoutPanelDetectorOutline);
            flowLayoutPanelOverlays.Controls.Add(checkBoxShowKikuchiLines);
            flowLayoutPanelOverlays.Controls.Add(flowLayoutPanelKikuchiLines);
            flowLayoutPanelOverlays.Controls.Add(groupBoxLatticePlanes);
            flowLayoutPanelOverlays.Controls.Add(checkBoxShowGIndices);
            flowLayoutPanelOverlays.Controls.Add(checkBoxShowZoneAxisIndices);
            flowLayoutPanelOverlays.Controls.Add(groupBoxTextSettings);
            flowLayoutPanelOverlays.Name = "flowLayoutPanelOverlays";
            toolTip.SetToolTip(flowLayoutPanelOverlays, resources.GetString("flowLayoutPanelOverlays.ToolTip"));
            // 
            // flowLayoutPanelDetectorOutline
            // 
            resources.ApplyResources(flowLayoutPanelDetectorOutline, "flowLayoutPanelDetectorOutline");
            flowLayoutPanelDetectorOutline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowLayoutPanelDetectorOutline.Controls.Add(checkBoxShowCircle);
            flowLayoutPanelDetectorOutline.Controls.Add(checkBoxShowMesh);
            flowLayoutPanelDetectorOutline.Name = "flowLayoutPanelDetectorOutline";
            toolTip.SetToolTip(flowLayoutPanelDetectorOutline, resources.GetString("flowLayoutPanelDetectorOutline.ToolTip"));
            // 
            // flowLayoutPanelKikuchiLines
            // 
            resources.ApplyResources(flowLayoutPanelKikuchiLines, "flowLayoutPanelKikuchiLines");
            flowLayoutPanelKikuchiLines.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            flowLayoutPanelKikuchiLines.Controls.Add(labelLineWidth);
            flowLayoutPanelKikuchiLines.Controls.Add(trackBarLineWidth);
            flowLayoutPanelKikuchiLines.Controls.Add(colorControlExcessLine);
            flowLayoutPanelKikuchiLines.Controls.Add(checkBoxKikuchiLine_Kinematical);
            flowLayoutPanelKikuchiLines.Name = "flowLayoutPanelKikuchiLines";
            toolTip.SetToolTip(flowLayoutPanelKikuchiLines, resources.GetString("flowLayoutPanelKikuchiLines.ToolTip"));
            // 
            // groupBoxLatticePlanes
            // 
            resources.ApplyResources(groupBoxLatticePlanes, "groupBoxLatticePlanes");
            groupBoxLatticePlanes.Controls.Add(flowLayoutPanelThresholdLength);
            groupBoxLatticePlanes.Controls.Add(flowLayoutPanelThresholdStructureFactor);
            groupBoxLatticePlanes.Name = "groupBoxLatticePlanes";
            groupBoxLatticePlanes.TabStop = false;
            toolTip.SetToolTip(groupBoxLatticePlanes, resources.GetString("groupBoxLatticePlanes.ToolTip"));
            // 
            // flowLayoutPanelThresholdLength
            // 
            resources.ApplyResources(flowLayoutPanelThresholdLength, "flowLayoutPanelThresholdLength");
            flowLayoutPanelThresholdLength.Controls.Add(radioButtonKikuchiThresholdOfLength);
            flowLayoutPanelThresholdLength.Controls.Add(numericBoxKikuchiThresholdOfLength);
            flowLayoutPanelThresholdLength.Name = "flowLayoutPanelThresholdLength";
            toolTip.SetToolTip(flowLayoutPanelThresholdLength, resources.GetString("flowLayoutPanelThresholdLength.ToolTip"));
            // 
            // flowLayoutPanelThresholdStructureFactor
            // 
            resources.ApplyResources(flowLayoutPanelThresholdStructureFactor, "flowLayoutPanelThresholdStructureFactor");
            flowLayoutPanelThresholdStructureFactor.Controls.Add(radioButtonKikuchiThresholdOfStructureFactor);
            flowLayoutPanelThresholdStructureFactor.Controls.Add(numericBoxKikuchiThresholdOfStructureFactor);
            flowLayoutPanelThresholdStructureFactor.Name = "flowLayoutPanelThresholdStructureFactor";
            toolTip.SetToolTip(flowLayoutPanelThresholdStructureFactor, resources.GetString("flowLayoutPanelThresholdStructureFactor.ToolTip"));
            // 
            // groupBoxTextSettings
            // 
            resources.ApplyResources(groupBoxTextSettings, "groupBoxTextSettings");
            groupBoxTextSettings.Controls.Add(flowLayoutPanelTextSettings);
            groupBoxTextSettings.Name = "groupBoxTextSettings";
            groupBoxTextSettings.TabStop = false;
            toolTip.SetToolTip(groupBoxTextSettings, resources.GetString("groupBoxTextSettings.ToolTip"));
            // 
            // flowLayoutPanelTextSettings
            // 
            resources.ApplyResources(flowLayoutPanelTextSettings, "flowLayoutPanelTextSettings");
            flowLayoutPanelTextSettings.Controls.Add(labelTextSize);
            flowLayoutPanelTextSettings.Controls.Add(trackBarStrSize);
            flowLayoutPanelTextSettings.Controls.Add(colorControlString);
            flowLayoutPanelTextSettings.Name = "flowLayoutPanelTextSettings";
            toolTip.SetToolTip(flowLayoutPanelTextSettings, resources.GetString("flowLayoutPanelTextSettings.ToolTip"));
            // 
            // flowLayoutPanelExperimentalImage
            // 
            resources.ApplyResources(flowLayoutPanelExperimentalImage, "flowLayoutPanelExperimentalImage");
            flowLayoutPanelExperimentalImage.Controls.Add(flowLayoutPanelExpMinInt);
            flowLayoutPanelExperimentalImage.Controls.Add(flowLayoutPanelExpMaxInt);
            flowLayoutPanelExperimentalImage.Name = "flowLayoutPanelExperimentalImage";
            toolTip.SetToolTip(flowLayoutPanelExperimentalImage, resources.GetString("flowLayoutPanelExperimentalImage.ToolTip"));
            // 
            // flowLayoutPanelExpMinInt
            // 
            resources.ApplyResources(flowLayoutPanelExpMinInt, "flowLayoutPanelExpMinInt");
            flowLayoutPanelExpMinInt.Controls.Add(labelExpBrightness);
            flowLayoutPanelExpMinInt.Controls.Add(labelExpMinInt);
            flowLayoutPanelExpMinInt.Controls.Add(trackBarExpImageMinInt);
            flowLayoutPanelExpMinInt.Name = "flowLayoutPanelExpMinInt";
            toolTip.SetToolTip(flowLayoutPanelExpMinInt, resources.GetString("flowLayoutPanelExpMinInt.ToolTip"));
            // 
            // flowLayoutPanelExpMaxInt
            // 
            resources.ApplyResources(flowLayoutPanelExpMaxInt, "flowLayoutPanelExpMaxInt");
            flowLayoutPanelExpMaxInt.Controls.Add(labelExpMaxInt);
            flowLayoutPanelExpMaxInt.Controls.Add(trackBarExpImageMaxInt);
            flowLayoutPanelExpMaxInt.Name = "flowLayoutPanelExpMaxInt";
            toolTip.SetToolTip(flowLayoutPanelExpMaxInt, resources.GetString("flowLayoutPanelExpMaxInt.ToolTip"));
            // 
            // flowLayoutPanelExpOpacity
            // 
            resources.ApplyResources(flowLayoutPanelExpOpacity, "flowLayoutPanelExpOpacity");
            flowLayoutPanelExpOpacity.Controls.Add(labelExpOpacity);
            flowLayoutPanelExpOpacity.Controls.Add(trackBarExpImageOpacity);
            flowLayoutPanelExpOpacity.Controls.Add(radioButtonIndexingRadon);
            flowLayoutPanelExpOpacity.Controls.Add(radioButtonIndexingDictionary);
            flowLayoutPanelExpOpacity.Name = "flowLayoutPanelExpOpacity";
            toolTip.SetToolTip(flowLayoutPanelExpOpacity, resources.GetString("flowLayoutPanelExpOpacity.ToolTip"));
            // 
            // groupBoxSimulationParameters
            // 
            resources.ApplyResources(groupBoxSimulationParameters, "groupBoxSimulationParameters");
            captureExtender.SetCapture(groupBoxSimulationParameters, true);
            groupBoxSimulationParameters.Controls.Add(flowLayoutPanelSimulationParameters);
            groupBoxSimulationParameters.Name = "groupBoxSimulationParameters";
            groupBoxSimulationParameters.TabStop = false;
            toolTip.SetToolTip(groupBoxSimulationParameters, resources.GetString("groupBoxSimulationParameters.ToolTip"));
            // 
            // flowLayoutPanelSimulationParameters
            // 
            resources.ApplyResources(flowLayoutPanelSimulationParameters, "flowLayoutPanelSimulationParameters");
            flowLayoutPanelSimulationParameters.Controls.Add(flowLayoutPanelMaxNumOfGAndGrid);
            flowLayoutPanelSimulationParameters.Controls.Add(flowLayoutPanelEnergyRange);
            flowLayoutPanelSimulationParameters.Controls.Add(flowLayoutPanelThicknessRange);
            flowLayoutPanelSimulationParameters.Controls.Add(flowLayoutPanelAbsorptionOptions);
            flowLayoutPanelSimulationParameters.Name = "flowLayoutPanelSimulationParameters";
            toolTip.SetToolTip(flowLayoutPanelSimulationParameters, resources.GetString("flowLayoutPanelSimulationParameters.ToolTip"));
            // 
            // flowLayoutPanelMaxNumOfGAndGrid
            // 
            resources.ApplyResources(flowLayoutPanelMaxNumOfGAndGrid, "flowLayoutPanelMaxNumOfGAndGrid");
            flowLayoutPanelMaxNumOfGAndGrid.Controls.Add(numericBoxMaxNumOfG);
            flowLayoutPanelMaxNumOfGAndGrid.Controls.Add(labelMasterPatternGrid);
            flowLayoutPanelMaxNumOfGAndGrid.Controls.Add(comboBoxMasterPatternGrid);
            flowLayoutPanelMaxNumOfGAndGrid.Name = "flowLayoutPanelMaxNumOfGAndGrid";
            toolTip.SetToolTip(flowLayoutPanelMaxNumOfGAndGrid, resources.GetString("flowLayoutPanelMaxNumOfGAndGrid.ToolTip"));
            // 
            // flowLayoutPanelEnergyRange
            // 
            resources.ApplyResources(flowLayoutPanelEnergyRange, "flowLayoutPanelEnergyRange");
            flowLayoutPanelEnergyRange.Controls.Add(numericBoxEnergyStart);
            flowLayoutPanelEnergyRange.Controls.Add(numericBoxEnergyEnd);
            flowLayoutPanelEnergyRange.Controls.Add(numericBoxEnergyStep);
            flowLayoutPanelEnergyRange.Name = "flowLayoutPanelEnergyRange";
            toolTip.SetToolTip(flowLayoutPanelEnergyRange, resources.GetString("flowLayoutPanelEnergyRange.ToolTip"));
            // 
            // flowLayoutPanelThicknessRange
            // 
            resources.ApplyResources(flowLayoutPanelThicknessRange, "flowLayoutPanelThicknessRange");
            flowLayoutPanelThicknessRange.Controls.Add(numericBoxThicknessStart);
            flowLayoutPanelThicknessRange.Controls.Add(numericBoxThicknessEnd);
            flowLayoutPanelThicknessRange.Controls.Add(numericBoxThicknessStep);
            flowLayoutPanelThicknessRange.Name = "flowLayoutPanelThicknessRange";
            toolTip.SetToolTip(flowLayoutPanelThicknessRange, resources.GetString("flowLayoutPanelThicknessRange.ToolTip"));
            // 
            // flowLayoutPanelAbsorptionOptions
            // 
            resources.ApplyResources(flowLayoutPanelAbsorptionOptions, "flowLayoutPanelAbsorptionOptions");
            flowLayoutPanelAbsorptionOptions.Controls.Add(checkBoxNonLocalAbsorption);
            flowLayoutPanelAbsorptionOptions.Controls.Add(checkBoxTDSBackground);
            flowLayoutPanelAbsorptionOptions.Name = "flowLayoutPanelAbsorptionOptions";
            toolTip.SetToolTip(flowLayoutPanelAbsorptionOptions, resources.GetString("flowLayoutPanelAbsorptionOptions.ToolTip"));
            // 
            // statusStripMain
            // 
            resources.ApplyResources(statusStripMain, "statusStripMain");
            statusStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusLabelProgress, toolStripStatusLabelSummary, toolStripStatusLabelDetail });
            statusStripMain.Name = "statusStripMain";
            toolTip.SetToolTip(statusStripMain, resources.GetString("statusStripMain.ToolTip"));
            // 
            // toolStripProgressBar
            // 
            resources.ApplyResources(toolStripProgressBar, "toolStripProgressBar");
            toolStripProgressBar.Name = "toolStripProgressBar";
            // 
            // toolStripStatusLabelProgress
            // 
            resources.ApplyResources(toolStripStatusLabelProgress, "toolStripStatusLabelProgress");
            toolStripStatusLabelProgress.Name = "toolStripStatusLabelProgress";
            // 
            // toolStripStatusLabelSummary
            // 
            resources.ApplyResources(toolStripStatusLabelSummary, "toolStripStatusLabelSummary");
            toolStripStatusLabelSummary.Name = "toolStripStatusLabelSummary";
            // 
            // toolStripStatusLabelDetail
            // 
            resources.ApplyResources(toolStripStatusLabelDetail, "toolStripStatusLabelDetail");
            toolStripStatusLabelDetail.Name = "toolStripStatusLabelDetail";
            // 
            // scalablePictureBoxAdvancedMasterPattern2D
            // 
            resources.ApplyResources(scalablePictureBoxAdvancedMasterPattern2D, "scalablePictureBoxAdvancedMasterPattern2D");
            scalablePictureBoxAdvancedMasterPattern2D.BackColor = System.Drawing.SystemColors.Control;
            scalablePictureBoxAdvancedMasterPattern2D.ClampIntensityRangeToNewData = false;
            scalablePictureBoxAdvancedMasterPattern2D.DecimalPlacesForIntensity = 5;
            scalablePictureBoxAdvancedMasterPattern2D.FrequencyGraphVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_DustAndScratchesVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilter_GaussianBlurVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.ImageFilterVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.MagInfoVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.MaximumIntensity = 1D;
            scalablePictureBoxAdvancedMasterPattern2D.MousePositionLabelVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.Name = "scalablePictureBoxAdvancedMasterPattern2D";
            scalablePictureBoxAdvancedMasterPattern2D.ScaleVisible = false;
            scalablePictureBoxAdvancedMasterPattern2D.StatusVisible = false;
            toolTip.SetToolTip(scalablePictureBoxAdvancedMasterPattern2D, resources.GetString("scalablePictureBoxAdvancedMasterPattern2D.ToolTip"));
            scalablePictureBoxAdvancedMasterPattern2D.UpperIntensity = 1D;
            scalablePictureBoxAdvancedMasterPattern2D.BrightnessAndColorChanged += scalablePictureBoxAdvancedMasterPattern2D_BrightnessAndColorChanged;
            // 
            // flowLayoutPanelMasterPatternSelectors
            // 
            resources.ApplyResources(flowLayoutPanelMasterPatternSelectors, "flowLayoutPanelMasterPatternSelectors");
            flowLayoutPanelMasterPatternSelectors.Controls.Add(numericBoxMasterPatternEnergy);
            flowLayoutPanelMasterPatternSelectors.Controls.Add(trackBarMasterPatternEnergy);
            flowLayoutPanelMasterPatternSelectors.Name = "flowLayoutPanelMasterPatternSelectors";
            toolTip.SetToolTip(flowLayoutPanelMasterPatternSelectors, resources.GetString("flowLayoutPanelMasterPatternSelectors.ToolTip"));
            // 
            // panelMasterPattern3D
            // 
            resources.ApplyResources(panelMasterPattern3D, "panelMasterPattern3D");
            panelMasterPattern3D.BackColor = System.Drawing.SystemColors.Control;
            panelMasterPattern3D.Controls.Add(panelMasterPattern3DAxes);
            panelMasterPattern3D.Name = "panelMasterPattern3D";
            toolTip.SetToolTip(panelMasterPattern3D, resources.GetString("panelMasterPattern3D.ToolTip"));
            // 
            // panelMasterPattern3DAxes
            // 
            resources.ApplyResources(panelMasterPattern3DAxes, "panelMasterPattern3DAxes");
            panelMasterPattern3DAxes.BackColor = System.Drawing.SystemColors.Control;
            panelMasterPattern3DAxes.Name = "panelMasterPattern3DAxes";
            toolTip.SetToolTip(panelMasterPattern3DAxes, resources.GetString("panelMasterPattern3DAxes.ToolTip"));
            // 
            // groupBoxMasterPattern
            // 
            resources.ApplyResources(groupBoxMasterPattern, "groupBoxMasterPattern");
            captureExtender.SetCapture(groupBoxMasterPattern, true);
            groupBoxMasterPattern.Controls.Add(tabControlMasterPattern);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanelMasterPatternControls);
            groupBoxMasterPattern.Controls.Add(groupBoxSimulationParameters);
            groupBoxMasterPattern.Controls.Add(flowLayoutPanelMasterPatternButtons);
            groupBoxMasterPattern.Name = "groupBoxMasterPattern";
            groupBoxMasterPattern.TabStop = false;
            toolTip.SetToolTip(groupBoxMasterPattern, resources.GetString("groupBoxMasterPattern.ToolTip"));
            // 
            // tabControlMasterPattern
            // 
            resources.ApplyResources(tabControlMasterPattern, "tabControlMasterPattern");
            tabControlMasterPattern.Controls.Add(tabPageMasterPattern2D);
            tabControlMasterPattern.Controls.Add(tabPageMasterPattern3D);
            tabControlMasterPattern.Name = "tabControlMasterPattern";
            tabControlMasterPattern.SelectedIndex = 0;
            toolTip.SetToolTip(tabControlMasterPattern, resources.GetString("tabControlMasterPattern.ToolTip"));
            // 
            // tabPageMasterPattern2D
            // 
            resources.ApplyResources(tabPageMasterPattern2D, "tabPageMasterPattern2D");
            tabPageMasterPattern2D.Controls.Add(scalablePictureBoxAdvancedMasterPattern2D);
            tabPageMasterPattern2D.Controls.Add(flowLayoutPanelMasterPattern2DControls);
            tabPageMasterPattern2D.Name = "tabPageMasterPattern2D";
            toolTip.SetToolTip(tabPageMasterPattern2D, resources.GetString("tabPageMasterPattern2D.ToolTip"));
            tabPageMasterPattern2D.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelMasterPattern2DControls
            // 
            resources.ApplyResources(flowLayoutPanelMasterPattern2DControls, "flowLayoutPanelMasterPattern2DControls");
            flowLayoutPanelMasterPattern2DControls.Controls.Add(buttonMasterPattern2DCopy);
            flowLayoutPanelMasterPattern2DControls.Controls.Add(labelMasterPattern2DHemisphere);
            flowLayoutPanelMasterPattern2DControls.Controls.Add(comboBoxMasterPattern2DHemisphere);
            flowLayoutPanelMasterPattern2DControls.Name = "flowLayoutPanelMasterPattern2DControls";
            toolTip.SetToolTip(flowLayoutPanelMasterPattern2DControls, resources.GetString("flowLayoutPanelMasterPattern2DControls.ToolTip"));
            // 
            // tabPageMasterPattern3D
            // 
            resources.ApplyResources(tabPageMasterPattern3D, "tabPageMasterPattern3D");
            tabPageMasterPattern3D.BackColor = System.Drawing.SystemColors.Control;
            tabPageMasterPattern3D.Controls.Add(panelMasterPattern3D);
            tabPageMasterPattern3D.Controls.Add(flowLayoutPanelMasterPattern3DCopy);
            tabPageMasterPattern3D.Controls.Add(flowLayoutPanelMasterPattern3DViewAlong);
            tabPageMasterPattern3D.Name = "tabPageMasterPattern3D";
            toolTip.SetToolTip(tabPageMasterPattern3D, resources.GetString("tabPageMasterPattern3D.ToolTip"));
            // 
            // flowLayoutPanelMasterPattern3DCopy
            // 
            resources.ApplyResources(flowLayoutPanelMasterPattern3DCopy, "flowLayoutPanelMasterPattern3DCopy");
            flowLayoutPanelMasterPattern3DCopy.Controls.Add(buttonMasterPattern3DCopy);
            flowLayoutPanelMasterPattern3DCopy.Name = "flowLayoutPanelMasterPattern3DCopy";
            toolTip.SetToolTip(flowLayoutPanelMasterPattern3DCopy, resources.GetString("flowLayoutPanelMasterPattern3DCopy.ToolTip"));
            // 
            // flowLayoutPanelMasterPattern3DViewAlong
            // 
            resources.ApplyResources(flowLayoutPanelMasterPattern3DViewAlong, "flowLayoutPanelMasterPattern3DViewAlong");
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(buttonMasterPattern3DViewAlong);
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(indexControl);
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(checkBoxMasterPattern3DAxisLabel);
            flowLayoutPanelMasterPattern3DViewAlong.Controls.Add(checkBoxMasterPattern3DAxisArrows);
            flowLayoutPanelMasterPattern3DViewAlong.Name = "flowLayoutPanelMasterPattern3DViewAlong";
            toolTip.SetToolTip(flowLayoutPanelMasterPattern3DViewAlong, resources.GetString("flowLayoutPanelMasterPattern3DViewAlong.ToolTip"));
            // 
            // flowLayoutPanelMasterPatternControls
            // 
            resources.ApplyResources(flowLayoutPanelMasterPatternControls, "flowLayoutPanelMasterPatternControls");
            flowLayoutPanelMasterPatternControls.Controls.Add(flowLayoutPanelMasterPatternSelectors);
            flowLayoutPanelMasterPatternControls.Controls.Add(flowLayoutPanelMasterPatternDepth);
            flowLayoutPanelMasterPatternControls.Name = "flowLayoutPanelMasterPatternControls";
            toolTip.SetToolTip(flowLayoutPanelMasterPatternControls, resources.GetString("flowLayoutPanelMasterPatternControls.ToolTip"));
            // 
            // flowLayoutPanelMasterPatternDepth
            // 
            resources.ApplyResources(flowLayoutPanelMasterPatternDepth, "flowLayoutPanelMasterPatternDepth");
            flowLayoutPanelMasterPatternDepth.Controls.Add(numericBoxMasterPatternDepth);
            flowLayoutPanelMasterPatternDepth.Controls.Add(trackBarMasterPatternDepth);
            flowLayoutPanelMasterPatternDepth.Name = "flowLayoutPanelMasterPatternDepth";
            toolTip.SetToolTip(flowLayoutPanelMasterPatternDepth, resources.GetString("flowLayoutPanelMasterPatternDepth.ToolTip"));
            // 
            // flowLayoutPanelMasterPatternButtons
            // 
            resources.ApplyResources(flowLayoutPanelMasterPatternButtons, "flowLayoutPanelMasterPatternButtons");
            flowLayoutPanelMasterPatternButtons.Controls.Add(buttonCreateMasterPattern);
            flowLayoutPanelMasterPatternButtons.Controls.Add(buttonStop);
            flowLayoutPanelMasterPatternButtons.Controls.Add(buttonFitNistElasticSampler);
            flowLayoutPanelMasterPatternButtons.Name = "flowLayoutPanelMasterPatternButtons";
            toolTip.SetToolTip(flowLayoutPanelMasterPatternButtons, resources.GetString("flowLayoutPanelMasterPatternButtons.ToolTip"));
            // 
            // groupBoxEBSDPattern
            // 
            resources.ApplyResources(groupBoxEBSDPattern, "groupBoxEBSDPattern");
            captureExtender.SetCapture(groupBoxEBSDPattern, true);
            groupBoxEBSDPattern.Controls.Add(graphicsBox);
            groupBoxEBSDPattern.Controls.Add(tabControlPatternSettings);
            groupBoxEBSDPattern.Controls.Add(flowLayoutPanelPatternBar);
            groupBoxEBSDPattern.Name = "groupBoxEBSDPattern";
            groupBoxEBSDPattern.TabStop = false;
            toolTip.SetToolTip(groupBoxEBSDPattern, resources.GetString("groupBoxEBSDPattern.ToolTip"));
            // 
            // tabControlPatternSettings
            // 
            resources.ApplyResources(tabControlPatternSettings, "tabControlPatternSettings");
            tabControlPatternSettings.Controls.Add(tabPageOutputParameter);
            tabControlPatternSettings.Controls.Add(tabPageExperimentalImage);
            tabControlPatternSettings.Name = "tabControlPatternSettings";
            tabControlPatternSettings.SelectedIndex = 0;
            toolTip.SetToolTip(tabControlPatternSettings, resources.GetString("tabControlPatternSettings.ToolTip"));
            // 
            // tabPageOutputParameter
            // 
            resources.ApplyResources(tabPageOutputParameter, "tabPageOutputParameter");
            tabPageOutputParameter.BackColor = System.Drawing.SystemColors.Control;
            tabPageOutputParameter.Controls.Add(flowLayoutPanelColorScale);
            tabPageOutputParameter.Controls.Add(flowLayoutPanelBrightness);
            tabPageOutputParameter.Controls.Add(flowLayoutPanelOutputRange);
            tabPageOutputParameter.Controls.Add(flowLayoutPanelWithBseDistribution);
            tabPageOutputParameter.Name = "tabPageOutputParameter";
            toolTip.SetToolTip(tabPageOutputParameter, resources.GetString("tabPageOutputParameter.ToolTip"));
            // 
            // tabPageExperimentalImage
            // 
            resources.ApplyResources(tabPageExperimentalImage, "tabPageExperimentalImage");
            tabPageExperimentalImage.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageExperimentalImage, true);
            tabPageExperimentalImage.Controls.Add(dataGridViewEbsdCandidates);
            tabPageExperimentalImage.Controls.Add(flowLayoutPanelExperimentalImageTab);
            tabPageExperimentalImage.Name = "tabPageExperimentalImage";
            toolTip.SetToolTip(tabPageExperimentalImage, resources.GetString("tabPageExperimentalImage.ToolTip"));
            // 
            // flowLayoutPanelExperimentalImageTab
            // 
            resources.ApplyResources(flowLayoutPanelExperimentalImageTab, "flowLayoutPanelExperimentalImageTab");
            flowLayoutPanelExperimentalImageTab.Controls.Add(flowLayoutPanelExperimentalImage);
            flowLayoutPanelExperimentalImageTab.Controls.Add(flowLayoutPanelExpOpacity);
            flowLayoutPanelExperimentalImageTab.Controls.Add(flowLayoutPanelIndexingButtons);
            flowLayoutPanelExperimentalImageTab.Name = "flowLayoutPanelExperimentalImageTab";
            toolTip.SetToolTip(flowLayoutPanelExperimentalImageTab, resources.GetString("flowLayoutPanelExperimentalImageTab.ToolTip"));
            // 
            // flowLayoutPanelIndexingButtons
            // 
            resources.ApplyResources(flowLayoutPanelIndexingButtons, "flowLayoutPanelIndexingButtons");
            flowLayoutPanelIndexingButtons.Controls.Add(buttonFindOrientation);
            flowLayoutPanelIndexingButtons.Controls.Add(buttonCalibrateGeometry);
            flowLayoutPanelIndexingButtons.Name = "flowLayoutPanelIndexingButtons";
            toolTip.SetToolTip(flowLayoutPanelIndexingButtons, resources.GetString("flowLayoutPanelIndexingButtons.ToolTip"));
            // 
            // flowLayoutPanelPatternBar
            // 
            resources.ApplyResources(flowLayoutPanelPatternBar, "flowLayoutPanelPatternBar");
            flowLayoutPanelPatternBar.Controls.Add(flowLayoutPanelCopy);
            flowLayoutPanelPatternBar.Controls.Add(flowLayoutPanelViewSettings);
            flowLayoutPanelPatternBar.Controls.Add(flowLayoutPanelShowCheckBoxes);
            flowLayoutPanelPatternBar.Name = "flowLayoutPanelPatternBar";
            toolTip.SetToolTip(flowLayoutPanelPatternBar, resources.GetString("flowLayoutPanelPatternBar.ToolTip"));
            // 
            // flowLayoutPanelCopy
            // 
            resources.ApplyResources(flowLayoutPanelCopy, "flowLayoutPanelCopy");
            flowLayoutPanelCopy.Controls.Add(flowLayoutPanelCopyOptions);
            flowLayoutPanelCopy.Name = "flowLayoutPanelCopy";
            toolTip.SetToolTip(flowLayoutPanelCopy, resources.GetString("flowLayoutPanelCopy.ToolTip"));
            // 
            // flowLayoutPanelCopyOptions
            // 
            resources.ApplyResources(flowLayoutPanelCopyOptions, "flowLayoutPanelCopyOptions");
            flowLayoutPanelCopyOptions.Controls.Add(flowLayoutPanelCopyButton);
            flowLayoutPanelCopyOptions.Controls.Add(flowLayoutPanelCopyRadios);
            flowLayoutPanelCopyOptions.Name = "flowLayoutPanelCopyOptions";
            toolTip.SetToolTip(flowLayoutPanelCopyOptions, resources.GetString("flowLayoutPanelCopyOptions.ToolTip"));
            // 
            // flowLayoutPanelCopyButton
            // 
            resources.ApplyResources(flowLayoutPanelCopyButton, "flowLayoutPanelCopyButton");
            flowLayoutPanelCopyButton.Controls.Add(buttonCopyImage);
            flowLayoutPanelCopyButton.Controls.Add(checkBoxMatchDetectorResolution);
            flowLayoutPanelCopyButton.Name = "flowLayoutPanelCopyButton";
            toolTip.SetToolTip(flowLayoutPanelCopyButton, resources.GetString("flowLayoutPanelCopyButton.ToolTip"));
            // 
            // flowLayoutPanelCopyRadios
            // 
            resources.ApplyResources(flowLayoutPanelCopyRadios, "flowLayoutPanelCopyRadios");
            flowLayoutPanelCopyRadios.Controls.Add(flowLayoutPanelCopyRange);
            flowLayoutPanelCopyRadios.Controls.Add(flowLayoutPanelCopyFormat);
            flowLayoutPanelCopyRadios.Name = "flowLayoutPanelCopyRadios";
            toolTip.SetToolTip(flowLayoutPanelCopyRadios, resources.GetString("flowLayoutPanelCopyRadios.ToolTip"));
            // 
            // flowLayoutPanelCopyRange
            // 
            resources.ApplyResources(flowLayoutPanelCopyRange, "flowLayoutPanelCopyRange");
            flowLayoutPanelCopyRange.Controls.Add(radioButtonCopyCurrent);
            flowLayoutPanelCopyRange.Controls.Add(radioButtonDetector);
            flowLayoutPanelCopyRange.Name = "flowLayoutPanelCopyRange";
            toolTip.SetToolTip(flowLayoutPanelCopyRange, resources.GetString("flowLayoutPanelCopyRange.ToolTip"));
            // 
            // flowLayoutPanelCopyFormat
            // 
            resources.ApplyResources(flowLayoutPanelCopyFormat, "flowLayoutPanelCopyFormat");
            flowLayoutPanelCopyFormat.Controls.Add(radioButtonCopyEmf);
            flowLayoutPanelCopyFormat.Controls.Add(radioButtonCopyBmp);
            flowLayoutPanelCopyFormat.Name = "flowLayoutPanelCopyFormat";
            toolTip.SetToolTip(flowLayoutPanelCopyFormat, resources.GetString("flowLayoutPanelCopyFormat.ToolTip"));
            // 
            // flowLayoutPanelViewSettings
            // 
            resources.ApplyResources(flowLayoutPanelViewSettings, "flowLayoutPanelViewSettings");
            flowLayoutPanelViewSettings.Controls.Add(flowLayoutPanelResolutionFlip);
            flowLayoutPanelViewSettings.Controls.Add(sizeControl);
            flowLayoutPanelViewSettings.Name = "flowLayoutPanelViewSettings";
            toolTip.SetToolTip(flowLayoutPanelViewSettings, resources.GetString("flowLayoutPanelViewSettings.ToolTip"));
            // 
            // flowLayoutPanelResolutionFlip
            // 
            resources.ApplyResources(flowLayoutPanelResolutionFlip, "flowLayoutPanelResolutionFlip");
            flowLayoutPanelResolutionFlip.Controls.Add(numericBoxResolution);
            flowLayoutPanelResolutionFlip.Controls.Add(checkBoxFlipDetectorLeftRight);
            flowLayoutPanelResolutionFlip.Name = "flowLayoutPanelResolutionFlip";
            toolTip.SetToolTip(flowLayoutPanelResolutionFlip, resources.GetString("flowLayoutPanelResolutionFlip.ToolTip"));
            // 
            // numericBoxResolution
            // 
            resources.ApplyResources(numericBoxResolution, "numericBoxResolution");
            numericBoxResolution.BackColor = System.Drawing.Color.Transparent;
            numericBoxResolution.DecimalPlaces = 3;
            numericBoxResolution.Maximum = 10D;
            numericBoxResolution.Minimum = 0.0001D;
            numericBoxResolution.Name = "numericBoxResolution";
            toolTip.SetToolTip(numericBoxResolution, resources.GetString("numericBoxResolution.ToolTip"));
            numericBoxResolution.Value = 0.1D;
            //numericBoxResolution.ValueBoxWidth = 40; // 260726Cl 旧: 最長値「10.000」が数値欄に入らず切れていた (--diagnose ValueBoxClipped)
            numericBoxResolution.ValueBoxWidth = 48; // 260726Cl
            numericBoxResolution.ValueChanged += numericBoxResolution_ValueChanged;
            // 
            // sizeControl
            // 
            resources.ApplyResources(sizeControl, "sizeControl");
            sizeControl.Name = "sizeControl";
            toolTip.SetToolTip(sizeControl, resources.GetString("sizeControl.ToolTip"));
            sizeControl.ValueChanged += sizeControl_ValueChanged;
            // 
            // flowLayoutPanelShowCheckBoxes
            // 
            resources.ApplyResources(flowLayoutPanelShowCheckBoxes, "flowLayoutPanelShowCheckBoxes");
            flowLayoutPanelShowCheckBoxes.Controls.Add(checkBoxShowOverlays);
            flowLayoutPanelShowCheckBoxes.Controls.Add(checkBoxShowDyanmicalEBSD);
            flowLayoutPanelShowCheckBoxes.Controls.Add(checkBoxShowExperimentalImage);
            flowLayoutPanelShowCheckBoxes.Name = "flowLayoutPanelShowCheckBoxes";
            toolTip.SetToolTip(flowLayoutPanelShowCheckBoxes, resources.GetString("flowLayoutPanelShowCheckBoxes.ToolTip"));
            // 
            // panelSpacerLeft
            // 
            resources.ApplyResources(panelSpacerLeft, "panelSpacerLeft");
            panelSpacerLeft.Name = "panelSpacerLeft";
            toolTip.SetToolTip(panelSpacerLeft, resources.GetString("panelSpacerLeft.ToolTip"));
            // 
            // panelSpacerRight
            // 
            resources.ApplyResources(panelSpacerRight, "panelSpacerRight");
            panelSpacerRight.Name = "panelSpacerRight";
            toolTip.SetToolTip(panelSpacerRight, resources.GetString("panelSpacerRight.ToolTip"));
            // 
            // panelSpacerBottom
            // 
            resources.ApplyResources(panelSpacerBottom, "panelSpacerBottom");
            panelSpacerBottom.Name = "panelSpacerBottom";
            toolTip.SetToolTip(panelSpacerBottom, resources.GetString("panelSpacerBottom.ToolTip"));
            // 
            // FormEBSD
            // 
            resources.ApplyResources(this, "$this");
            AllowDrop = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(groupBoxEBSDPattern);
            Controls.Add(panelSpacerRight);
            Controls.Add(panelSpacerLeft);
            Controls.Add(groupBoxMasterPattern);
            Controls.Add(tabControlSettings);
            Controls.Add(panelSpacerBottom);
            Controls.Add(statusStripMain);
            Name = "FormEBSD";
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            FormClosing += FormEBSD_FormClosing;
            Load += FormEBSD_Load;
            VisibleChanged += FormEBSD_VisibleChanged;
            DragDrop += FormEBSD_DragDrop;
            DragEnter += FormEBSD_DragEnter;
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLineWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputEnergy).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarOutputThickness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarIntensityBrightnessMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternEnergy).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarMasterPatternDepth).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageMinInt).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageMaxInt).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarExpImageOpacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewEbsdCandidates).EndInit();
            flowLayoutPanelViewAlong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            flowLayoutPanelColorScale.ResumeLayout(false);
            flowLayoutPanelColorScale.PerformLayout();
            flowLayoutPanelBrightness.ResumeLayout(false);
            flowLayoutPanelBrightness.PerformLayout();
            flowLayoutPanelOutputRange.ResumeLayout(false);
            flowLayoutPanelWithBseDistribution.ResumeLayout(false);
            flowLayoutPanelWithBseDistribution.PerformLayout();
            tabControlSettings.ResumeLayout(false);
            tabPageGeometry.ResumeLayout(false);
            tabPageGeometry.PerformLayout();
            groupBoxEBSDGeometry.ResumeLayout(false);
            groupBoxEBSDGeometry.PerformLayout();
            flowLayoutPanelDetectorGeometry.ResumeLayout(false);
            flowLayoutPanelDetectorGeometry.PerformLayout();
            flowLayoutPanelDetectorSizeTilt.ResumeLayout(false);
            flowLayoutPanelDetectorSizeTilt.PerformLayout();
            flowLayoutPanelDetectorPosition.ResumeLayout(false);
            flowLayoutPanelDetectorPosition.PerformLayout();
            groupBoxSampleCondition.ResumeLayout(false);
            groupBoxSampleCondition.PerformLayout();
            flowLayoutPanelSampleCondition.ResumeLayout(false);
            flowLayoutPanelSampleCondition.PerformLayout();
            tabPageBseDistribution.ResumeLayout(false);
            tabPageBseDistribution.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabPageOverlays.ResumeLayout(false);
            flowLayoutPanelOverlays.ResumeLayout(false);
            flowLayoutPanelOverlays.PerformLayout();
            flowLayoutPanelDetectorOutline.ResumeLayout(false);
            flowLayoutPanelDetectorOutline.PerformLayout();
            flowLayoutPanelKikuchiLines.ResumeLayout(false);
            flowLayoutPanelKikuchiLines.PerformLayout();
            groupBoxLatticePlanes.ResumeLayout(false);
            groupBoxLatticePlanes.PerformLayout();
            flowLayoutPanelThresholdLength.ResumeLayout(false);
            flowLayoutPanelThresholdLength.PerformLayout();
            flowLayoutPanelThresholdStructureFactor.ResumeLayout(false);
            flowLayoutPanelThresholdStructureFactor.PerformLayout();
            groupBoxTextSettings.ResumeLayout(false);
            groupBoxTextSettings.PerformLayout();
            flowLayoutPanelTextSettings.ResumeLayout(false);
            flowLayoutPanelTextSettings.PerformLayout();
            flowLayoutPanelExperimentalImage.ResumeLayout(false);
            flowLayoutPanelExperimentalImage.PerformLayout();
            flowLayoutPanelExpMinInt.ResumeLayout(false);
            flowLayoutPanelExpMinInt.PerformLayout();
            flowLayoutPanelExpMaxInt.ResumeLayout(false);
            flowLayoutPanelExpMaxInt.PerformLayout();
            flowLayoutPanelExpOpacity.ResumeLayout(false);
            flowLayoutPanelExpOpacity.PerformLayout();
            groupBoxSimulationParameters.ResumeLayout(false);
            groupBoxSimulationParameters.PerformLayout();
            flowLayoutPanelSimulationParameters.ResumeLayout(false);
            flowLayoutPanelSimulationParameters.PerformLayout();
            flowLayoutPanelMaxNumOfGAndGrid.ResumeLayout(false);
            flowLayoutPanelMaxNumOfGAndGrid.PerformLayout();
            flowLayoutPanelEnergyRange.ResumeLayout(false);
            flowLayoutPanelEnergyRange.PerformLayout();
            flowLayoutPanelThicknessRange.ResumeLayout(false);
            flowLayoutPanelThicknessRange.PerformLayout();
            flowLayoutPanelAbsorptionOptions.ResumeLayout(false);
            flowLayoutPanelAbsorptionOptions.PerformLayout();
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            flowLayoutPanelMasterPatternSelectors.ResumeLayout(false);
            panelMasterPattern3D.ResumeLayout(false);
            groupBoxMasterPattern.ResumeLayout(false);
            groupBoxMasterPattern.PerformLayout();
            tabControlMasterPattern.ResumeLayout(false);
            tabPageMasterPattern2D.ResumeLayout(false);
            tabPageMasterPattern2D.PerformLayout();
            flowLayoutPanelMasterPattern2DControls.ResumeLayout(false);
            flowLayoutPanelMasterPattern2DControls.PerformLayout();
            tabPageMasterPattern3D.ResumeLayout(false);
            tabPageMasterPattern3D.PerformLayout();
            flowLayoutPanelMasterPattern3DCopy.ResumeLayout(false);
            flowLayoutPanelMasterPattern3DCopy.PerformLayout();
            flowLayoutPanelMasterPattern3DViewAlong.ResumeLayout(false);
            flowLayoutPanelMasterPattern3DViewAlong.PerformLayout();
            flowLayoutPanelMasterPatternControls.ResumeLayout(false);
            flowLayoutPanelMasterPatternControls.PerformLayout();
            flowLayoutPanelMasterPatternDepth.ResumeLayout(false);
            flowLayoutPanelMasterPatternButtons.ResumeLayout(false);
            flowLayoutPanelMasterPatternButtons.PerformLayout();
            groupBoxEBSDPattern.ResumeLayout(false);
            groupBoxEBSDPattern.PerformLayout();
            tabControlPatternSettings.ResumeLayout(false);
            tabPageOutputParameter.ResumeLayout(false);
            tabPageOutputParameter.PerformLayout();
            tabPageExperimentalImage.ResumeLayout(false);
            tabPageExperimentalImage.PerformLayout();
            flowLayoutPanelExperimentalImageTab.ResumeLayout(false);
            flowLayoutPanelExperimentalImageTab.PerformLayout();
            flowLayoutPanelIndexingButtons.ResumeLayout(false);
            flowLayoutPanelIndexingButtons.PerformLayout();
            flowLayoutPanelPatternBar.ResumeLayout(false);
            flowLayoutPanelPatternBar.PerformLayout();
            flowLayoutPanelCopy.ResumeLayout(false);
            flowLayoutPanelCopy.PerformLayout();
            flowLayoutPanelCopyOptions.ResumeLayout(false);
            flowLayoutPanelCopyOptions.PerformLayout();
            flowLayoutPanelCopyButton.ResumeLayout(false);
            flowLayoutPanelCopyButton.PerformLayout();
            flowLayoutPanelCopyRadios.ResumeLayout(false);
            flowLayoutPanelCopyRadios.PerformLayout();
            flowLayoutPanelCopyRange.ResumeLayout(false);
            flowLayoutPanelCopyRange.PerformLayout();
            flowLayoutPanelCopyFormat.ResumeLayout(false);
            flowLayoutPanelCopyFormat.PerformLayout();
            flowLayoutPanelViewSettings.ResumeLayout(false);
            flowLayoutPanelViewSettings.PerformLayout();
            flowLayoutPanelResolutionFlip.ResumeLayout(false);
            flowLayoutPanelResolutionFlip.PerformLayout();
            flowLayoutPanelShowCheckBoxes.ResumeLayout(false);
            flowLayoutPanelShowCheckBoxes.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)

        private System.Windows.Forms.Panel panelGeometry;
        private NumericBox numericBoxSampleTilt;
        private WaveLengthControl waveLengthControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelViewAlong;
        private System.Windows.Forms.Button buttonViewFromZ;
        private System.Windows.Forms.Button buttonFromX;
        private System.Windows.Forms.Button buttonViewFromSurfaceNormal;
        private System.Windows.Forms.Button buttonSimulateBSE;
        private System.Windows.Forms.Button buttonFitNistElasticSampler;
        private PoleFigureControl2 poleFigureControl;
        private System.Windows.Forms.CheckBox checkBoxDrawAxesInStereonet;
        private NumericBox numericBoxDetTilt;
        private NumericBox numericBoxZofDet;
        private NumericBox numericBoxYofDet;
        // public ImagingSolution.Control.GraphicsBox graphicsBox; // (260322Ch) 旧 GraphicsBox 型
        // public Crystallography.Controls.GraphicBox2 graphicsBox; // (260322Ch) 仮名 GraphicBox2
        public Crystallography.Controls.GraphicsBox graphicsBox; // (260322Ch) 正式名 GraphicBox へ移行
        private System.Windows.Forms.TrackBar trackBarStrSize;
        public ColorControl colorControlExcessLine;
        private System.Windows.Forms.TrackBar trackBarLineWidth;
        private System.Windows.Forms.Label labelLineWidth;
        public ColorControl colorControlString;
        public ColorControl colorControlBackGround;
        private System.Windows.Forms.RadioButton radioButtonKikuchiThresholdOfStructureFactor;
        private System.Windows.Forms.CheckBox checkBoxKikuchiLine_Kinematical;
        private System.Windows.Forms.RadioButton radioButtonKikuchiThresholdOfLength;
        private NumericBox numericBoxKikuchiThresholdOfStructureFactor;
        private NumericBox numericBoxKikuchiThresholdOfLength;
        private System.Windows.Forms.Label labelTextSize;
        private NumericBox numericBoxThicknessStep;
        private NumericBox numericBoxMaxNumOfG;
        private NumericBox numericBoxThicknessStart;
        private NumericBox numericBoxThicknessEnd;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Label labelPolarity;
        private System.Windows.Forms.Label labelColor;
        public System.Windows.Forms.ComboBox comboBoxGradient;
        public System.Windows.Forms.ComboBox comboBoxScale;
        public System.Windows.Forms.TrackBar trackBarOutputThickness;
        private System.Windows.Forms.TrackBar trackBarIntensityBrightnessMax;
        private System.Windows.Forms.TrackBar trackBarIntensityBrightnessMin;
        private System.Windows.Forms.Label labelBrightnessMax;
        private System.Windows.Forms.Label labelBrightnessMin;
        private System.Windows.Forms.Label labelBrightness;
        private System.Windows.Forms.CheckBox checkBoxShowOverlays;
        private System.Windows.Forms.Button buttonCopyImage;
        private GraphControl graphControlEnergyProfile;
        private GraphControl graphControlDepthProfile;
        public System.Windows.Forms.TrackBar trackBarOutputEnergy;
        private NumericBox numericBoxEnergyEnd;
        private NumericBox numericBoxEnergyStart;
        private NumericBox numericBoxEnergyStep;
        private System.Windows.Forms.Button buttonViewQuarter;
        private System.Windows.Forms.Label labelBseDepth;
        private System.Windows.Forms.Label labelBseDeltaE;
        private System.Windows.Forms.CheckBox checkBoxShowDyanmicalEBSD;
        private System.Windows.Forms.CheckBox checkBoxDrawDetectorOutline;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabPageGeometry;
        private System.Windows.Forms.TabPage tabPageBseDistribution;
        private System.Windows.Forms.Label labelBseStereonetNote;
        private System.Windows.Forms.GroupBox groupBoxSimulationParameters;
        private System.Windows.Forms.TabPage tabPageOverlays;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProgress;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelSummary;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.CheckBox checkBoxNonLocalAbsorption;
        private System.Windows.Forms.CheckBox checkBoxTDSBackground;
        private System.Windows.Forms.CheckBox checkBoxWithBSEDistribution;
        private System.Windows.Forms.CheckBox checkBoxFlipDetectorLeftRight; // 260718Cl 追加: 検出器を背面から見た左右反転
        private System.Windows.Forms.GroupBox groupBoxLatticePlanes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOutputRange;
        private System.Windows.Forms.Panel panelMasterPattern3D;
        private ScalablePictureBoxAdvanced scalablePictureBoxAdvancedMasterPattern2D;
        private System.Windows.Forms.Label labelMasterPatternGrid;
        private System.Windows.Forms.ComboBox comboBoxMasterPatternGrid;
        private System.Windows.Forms.Label labelMasterPattern2DHemisphere;
        private System.Windows.Forms.ComboBox comboBoxMasterPattern2DHemisphere;
        private System.Windows.Forms.TrackBar trackBarMasterPatternEnergy;
        private System.Windows.Forms.TrackBar trackBarMasterPatternDepth;
        private System.Windows.Forms.Button buttonCreateMasterPattern;
        // private System.Windows.Forms.Label labelMasterPatternInfo; // 260406Cl 廃止
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMasterPatternSelectors;
        private System.Windows.Forms.GroupBox groupBoxMasterPattern;
        private System.Windows.Forms.Button buttonMasterPattern3DCopy;
        private System.Windows.Forms.Button buttonMasterPattern2DCopy;
        private System.Windows.Forms.GroupBox groupBoxSampleCondition;
        private System.Windows.Forms.GroupBox groupBoxEBSDGeometry;
        private System.Windows.Forms.GroupBox groupBoxEBSDPattern;
        private System.Windows.Forms.Panel panelSpacerLeft;
        private System.Windows.Forms.Panel panelSpacerRight;
        private System.Windows.Forms.Panel panelSpacerBottom;
        private System.Windows.Forms.CheckBox checkBoxMasterPattern3DAxisLabel;
        private System.Windows.Forms.Panel panelMasterPattern3DAxes;
        private System.Windows.Forms.Button buttonMasterPattern3DViewAlong;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMasterPattern3DViewAlong;
        // 260517Cl 削除: IndexControl 化により [u v w] のブラケット表示は IndexControl 内部 (labelLaTexStart/End) が担うため、外側ラベルは不要に。
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDetail;
        private System.Windows.Forms.CheckBox checkBoxShowGIndices;
        private System.Windows.Forms.CheckBox checkBoxShowZoneAxisIndices;
        private System.Windows.Forms.CheckBox checkBoxShowKikuchiLines;
        private System.Windows.Forms.GroupBox groupBoxTextSettings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPatternBar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMasterPatternButtons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMasterPattern2DControls;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMasterPattern3DCopy;
        private IndexControl indexControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSimulationParameters;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMaxNumOfGAndGrid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelEnergyRange;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelThicknessRange;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAbsorptionOptions;
        private NumericBox numericBoxMasterPatternEnergy;
        private NumericBox numericBoxMasterPatternDepth;
        private NumericBox numericBoxEnergy;
        private NumericBox numericBoxDepth;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelOverlays;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDetectorOutline;
        private System.Windows.Forms.CheckBox checkBoxShowCircle;
        private System.Windows.Forms.CheckBox checkBoxShowMesh;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelKikuchiLines;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDetectorGeometry;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSampleCondition;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelThresholdLength;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelThresholdStructureFactor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTextSettings;
        private NumericBox numericBoxXofDet;
        private SizeControl sizeControl;
        private System.Windows.Forms.Label labelDetectorCenter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDetectorPosition;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDetectorSizeTilt;
        private System.Windows.Forms.Label labelDetectorSizeTilt;
        private NumericBox numericBoxDetWidth;
        private NumericBox numericBoxDetHeight;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelWithBseDistribution;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelColorScale;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBrightness;
        private NumericBox numericBoxResolution;
        private System.Windows.Forms.Label labelDetectorResolution;
        private NumericBox numericBoxDetResolution;
        private System.Windows.Forms.CheckBox checkBoxShowExperimentalImage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExperimentalImage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExpOpacity;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExpMaxInt;
        private System.Windows.Forms.Label labelExpOpacity;
        private System.Windows.Forms.Label labelExpMaxInt;
        private System.Windows.Forms.TrackBar trackBarExpImageOpacity;
        private System.Windows.Forms.TrackBar trackBarExpImageMaxInt;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExpMinInt;
        private System.Windows.Forms.Label labelExpMinInt;
        private System.Windows.Forms.TrackBar trackBarExpImageMinInt;
        private System.Windows.Forms.Button buttonFindOrientation;
        private System.Windows.Forms.Button buttonCalibrateGeometry;
        private System.Windows.Forms.DataGridView dataGridViewEbsdCandidates;
        private System.Windows.Forms.TabControl tabControlPatternSettings;
        private System.Windows.Forms.TabPage tabPageOutputParameter;
        private System.Windows.Forms.TabPage tabPageExperimentalImage;
        private System.Windows.Forms.RadioButton radioButtonIndexingRadon; //260724Cl 追加: 方位探索エンジン切替 (Radon template matching)
        private System.Windows.Forms.RadioButton radioButtonIndexingDictionary; //260724Cl 追加: 同 (Dictionary indexing)
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelIndexingButtons;
        private System.Windows.Forms.Label labelExpBrightness;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExperimentalImageTab;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCopy;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelViewSettings;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelShowCheckBoxes;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelResolutionFlip;
        //private System.Windows.Forms.CheckBox checkBox1; //260724Cl デザイナのリネーム不整合を修正 (InitializeComponent側は新名)
        //private System.Windows.Forms.RadioButton radioButton1; //260724Cl 同上
        private System.Windows.Forms.CheckBox checkBoxMatchDetectorResolution; //260724Cl 追加
        private System.Windows.Forms.RadioButton radioButtonDetector; //260724Cl 追加
        private System.Windows.Forms.RadioButton radioButtonCopyCurrent;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCopyOptions;
        private System.Windows.Forms.TabControl tabControlMasterPattern;
        private System.Windows.Forms.TabPage tabPageMasterPattern2D;
        private System.Windows.Forms.TabPage tabPageMasterPattern3D;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMasterPatternDepth;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMasterPatternControls;
        private System.Windows.Forms.CheckBox checkBoxMasterPattern3DAxisArrows;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCopyRange;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCopyButton;
        private System.Windows.Forms.RadioButton radioButtonCopyEmf; //260725Cl リネーム radioButton1 -> radioButtonCopyEmf (作者依頼)
        private System.Windows.Forms.RadioButton radioButtonCopyBmp;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCopyFormat;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCopyRadios;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

