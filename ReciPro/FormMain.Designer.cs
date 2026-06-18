namespace ReciPro
{
    partial class FormMain
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
            //if (contextAxes != null)
            //    contextAxes.Dispose();
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        // (260323Ch) renamed numeric container controls:
        // groupBox2 -> groupBoxArrows
        // groupBox5 -> groupBoxProjectAlong
        // groupBox6 -> groupBoxCrystalInformation
        // flowLayoutPanel1 -> flowLayoutPanelSetAxis
        // flowLayoutPanel2 -> flowLayoutPanelSetPlane
        // flowLayoutPanel3 -> flowLayoutPanelCrystalEdit
        // flowLayoutPanel4 -> flowLayoutPanelCrystalOrder
        // panel4 -> panelArrowStep
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            splitContainer = new System.Windows.Forms.SplitContainer();
            groupBoxCrystalList = new System.Windows.Forms.GroupBox();
            listBox = new System.Windows.Forms.ListBox();
            flowLayoutPanelCrystalOrder = new System.Windows.Forms.FlowLayoutPanel();
            buttonClearAllCrystals = new System.Windows.Forms.Button();
            buttonDelete = new System.Windows.Forms.Button();
            buttonDuplicate = new System.Windows.Forms.Button();
            buttonDown = new System.Windows.Forms.Button();
            buttonUp = new System.Windows.Forms.Button();
            groupBoxCrystalInformation = new System.Windows.Forms.GroupBox();
            crystalControl = new CrystalControl();
            flowLayoutPanelCrystalEdit = new System.Windows.Forms.FlowLayoutPanel();
            buttonAdd = new System.Windows.Forms.Button();
            buttonReplace = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            groupBoxProjectAlong = new System.Windows.Forms.GroupBox();
            indexControlPlane = new IndexControl();
            flowLayoutPanelSetPlane = new System.Windows.Forms.FlowLayoutPanel();
            buttonSetPlane = new System.Windows.Forms.Button();
            checkBoxFixePlane = new System.Windows.Forms.CheckBox();
            indexControlAxis = new IndexControl();
            flowLayoutPanelSetAxis = new System.Windows.Forms.FlowLayoutPanel();
            buttonSetAxis = new System.Windows.Forms.Button();
            checkBoxFixAxis = new System.Windows.Forms.CheckBox();
            panel3 = new System.Windows.Forms.Panel();
            groupBoxArrows = new System.Windows.Forms.GroupBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
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
            panelArrowStep = new System.Windows.Forms.Panel();
            numericBoxStep = new NumericBox();
            checkBoxAnimation = new System.Windows.Forms.CheckBox();
            panel2 = new System.Windows.Forms.Panel();
            groupBoxCurrentDirection = new System.Windows.Forms.GroupBox();
            panelCrystalDirection = new System.Windows.Forms.Panel();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            label6 = new System.Windows.Forms.Label();
            numericBoxEulerPsi = new NumericBox();
            label2 = new System.Windows.Forms.Label();
            numericBoxEulerTheta = new NumericBox();
            numericBoxEulerPhi = new NumericBox();
            label5 = new System.Windows.Forms.Label();
            labelLaTex1 = new LabelLaTeX();
            labelLaTex2 = new LabelLaTeX();
            labelLaTex3 = new LabelLaTeX();
            label7 = new System.Windows.Forms.Label();
            buttonReset = new System.Windows.Forms.Button();
            numericBoxMaxUVW = new NumericBox();
            labelCurrentIndex = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonDatabase = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonSymmetryInformation = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonBeamInteraction = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonRotation = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonStructureViewer = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonStereonet = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonDiffractionSingle = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonTrajectorySimulator = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonImageSimulator = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonSpotIDv1 = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonSpotIDv2 = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonEBSD = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonDiffractionPoly = new System.Windows.Forms.ToolStripButton();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            readCrystalDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            readCrystalDataAndAddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemReadInitialCrystalList = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            readCrystalFromCIFOrAMCFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            saveCrystalDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemExportCIF = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolTipToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemUseMillerBravais = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            resetRegistryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemDisableNative = new System.Windows.Forms.ToolStripMenuItem();
            disableOpneGLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemDisableTextRendering = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemUseMKL = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemIonicScattering = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemDarkMode = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            powderDiffractionFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparatorCapture = new System.Windows.Forms.ToolStripSeparator();
            captureGUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            checkUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            hintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            versionHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            githubPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            reportBugsRequestsOrCommentsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            helpwebToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            // 260618Cl: 言語サブメニュー項目 (english/japanese/german) は Designer 固定をやめ、
            //   FormMain.PopulateLanguageMenu() が SupportedCultures.All.Where(Released) から動的生成する。
            macroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            editorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            contextMenuStripListBox = new System.Windows.Forms.ContextMenuStrip(components);
            renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exportAsCIFFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolTip = new System.Windows.Forms.ToolTip(components);
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            timer = new System.Windows.Forms.Timer(components);
            toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.RightToolStripPanel.SuspendLayout();
            toolStripContainer1.TopToolStripPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            groupBoxCrystalList.SuspendLayout();
            flowLayoutPanelCrystalOrder.SuspendLayout();
            groupBoxCrystalInformation.SuspendLayout();
            flowLayoutPanelCrystalEdit.SuspendLayout();
            panel1.SuspendLayout();
            groupBoxProjectAlong.SuspendLayout();
            flowLayoutPanelSetPlane.SuspendLayout();
            flowLayoutPanelSetAxis.SuspendLayout();
            groupBoxArrows.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panelArrowStep.SuspendLayout();
            groupBoxCurrentDirection.SuspendLayout();
            panelCrystalDirection.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            toolStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            contextMenuStripListBox.SuspendLayout();
            SuspendLayout();
            // 
            // toolStripContainer1
            // 
            resources.ApplyResources(toolStripContainer1, "toolStripContainer1");
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            resources.ApplyResources(toolStripContainer1.BottomToolStripPanel, "toolStripContainer1.BottomToolStripPanel");
            toolStripContainer1.BottomToolStripPanel.Controls.Add(statusStrip1);
            toolTip.SetToolTip(toolStripContainer1.BottomToolStripPanel, resources.GetString("toolStripContainer1.BottomToolStripPanel.ToolTip"));
            // 
            // toolStripContainer1.ContentPanel
            // 
            resources.ApplyResources(toolStripContainer1.ContentPanel, "toolStripContainer1.ContentPanel");
            toolStripContainer1.ContentPanel.Controls.Add(splitContainer);
            toolStripContainer1.ContentPanel.Controls.Add(panel1);
            toolTip.SetToolTip(toolStripContainer1.ContentPanel, resources.GetString("toolStripContainer1.ContentPanel.ToolTip"));
            // 
            // toolStripContainer1.LeftToolStripPanel
            // 
            resources.ApplyResources(toolStripContainer1.LeftToolStripPanel, "toolStripContainer1.LeftToolStripPanel");
            toolTip.SetToolTip(toolStripContainer1.LeftToolStripPanel, resources.GetString("toolStripContainer1.LeftToolStripPanel.ToolTip"));
            toolStripContainer1.Name = "toolStripContainer1";
            // 
            // toolStripContainer1.RightToolStripPanel
            // 
            resources.ApplyResources(toolStripContainer1.RightToolStripPanel, "toolStripContainer1.RightToolStripPanel");
            toolStripContainer1.RightToolStripPanel.Controls.Add(toolStrip1);
            toolStripContainer1.RightToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            toolTip.SetToolTip(toolStripContainer1.RightToolStripPanel, resources.GetString("toolStripContainer1.RightToolStripPanel.ToolTip"));
            toolTip.SetToolTip(toolStripContainer1, resources.GetString("toolStripContainer1.ToolTip"));
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            resources.ApplyResources(toolStripContainer1.TopToolStripPanel, "toolStripContainer1.TopToolStripPanel");
            toolStripContainer1.TopToolStripPanel.Controls.Add(menuStrip1);
            toolTip.SetToolTip(toolStripContainer1.TopToolStripPanel, resources.GetString("toolStripContainer1.TopToolStripPanel.ToolTip"));
            // 
            // statusStrip1
            // 
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 16);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripProgressBar, toolStripStatusLabel });
            statusStrip1.Name = "statusStrip1";
            toolTip.SetToolTip(statusStrip1, resources.GetString("statusStrip1.ToolTip"));
            // 
            // toolStripProgressBar
            // 
            resources.ApplyResources(toolStripProgressBar, "toolStripProgressBar");
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Value = 100;
            // 
            // toolStripStatusLabel
            // 
            resources.ApplyResources(toolStripStatusLabel, "toolStripStatusLabel");
            toolStripStatusLabel.Margin = new System.Windows.Forms.Padding(0);
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            // 
            // splitContainer
            // 
            resources.ApplyResources(splitContainer, "splitContainer");
            splitContainer.BackColor = System.Drawing.SystemColors.ControlLightLight;
            splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            resources.ApplyResources(splitContainer.Panel1, "splitContainer.Panel1");
            splitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            splitContainer.Panel1.Controls.Add(groupBoxCrystalList);
            toolTip.SetToolTip(splitContainer.Panel1, resources.GetString("splitContainer.Panel1.ToolTip"));
            // 
            // splitContainer.Panel2
            // 
            resources.ApplyResources(splitContainer.Panel2, "splitContainer.Panel2");
            splitContainer.Panel2.BackColor = System.Drawing.SystemColors.Control;
            splitContainer.Panel2.Controls.Add(groupBoxCrystalInformation);
            splitContainer.Panel2.Controls.Add(flowLayoutPanelCrystalEdit);
            toolTip.SetToolTip(splitContainer.Panel2, resources.GetString("splitContainer.Panel2.ToolTip"));
            toolTip.SetToolTip(splitContainer, resources.GetString("splitContainer.ToolTip"));
            // 
            // groupBoxCrystalList
            // 
            resources.ApplyResources(groupBoxCrystalList, "groupBoxCrystalList");
            groupBoxCrystalList.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(groupBoxCrystalList, true);
            groupBoxCrystalList.Controls.Add(listBox);
            groupBoxCrystalList.Controls.Add(flowLayoutPanelCrystalOrder);
            groupBoxCrystalList.Name = "groupBoxCrystalList";
            groupBoxCrystalList.TabStop = false;
            toolTip.SetToolTip(groupBoxCrystalList, resources.GetString("groupBoxCrystalList.ToolTip"));
            // 
            // listBox
            // 
            resources.ApplyResources(listBox, "listBox");
            listBox.FormattingEnabled = true;
            listBox.MultiColumn = true;
            listBox.Name = "listBox";
            toolTip.SetToolTip(listBox, resources.GetString("listBox.ToolTip"));
            listBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;
            listBox.MouseDown += listBox_MouseDown;
            // 
            // flowLayoutPanelCrystalOrder
            // 
            resources.ApplyResources(flowLayoutPanelCrystalOrder, "flowLayoutPanelCrystalOrder");
            flowLayoutPanelCrystalOrder.Controls.Add(buttonClearAllCrystals);
            flowLayoutPanelCrystalOrder.Controls.Add(buttonDelete);
            flowLayoutPanelCrystalOrder.Controls.Add(buttonDuplicate);
            flowLayoutPanelCrystalOrder.Controls.Add(buttonDown);
            flowLayoutPanelCrystalOrder.Controls.Add(buttonUp);
            flowLayoutPanelCrystalOrder.Name = "flowLayoutPanelCrystalOrder";
            toolTip.SetToolTip(flowLayoutPanelCrystalOrder, resources.GetString("flowLayoutPanelCrystalOrder.ToolTip"));
            // 
            // buttonClearAllCrystals
            // 
            resources.ApplyResources(buttonClearAllCrystals, "buttonClearAllCrystals");
            buttonClearAllCrystals.BackColor = System.Drawing.Color.IndianRed;
            buttonClearAllCrystals.ForeColor = System.Drawing.Color.White;
            buttonClearAllCrystals.Name = "buttonClearAllCrystals";
            toolTip.SetToolTip(buttonClearAllCrystals, resources.GetString("buttonClearAllCrystals.ToolTip"));
            buttonClearAllCrystals.UseVisualStyleBackColor = false;
            buttonClearAllCrystals.Click += ButtonAllClear_Click;
            // 
            // buttonDelete
            // 
            resources.ApplyResources(buttonDelete, "buttonDelete");
            buttonDelete.BackColor = System.Drawing.Color.IndianRed;
            buttonDelete.ForeColor = System.Drawing.Color.White;
            buttonDelete.Name = "buttonDelete";
            toolTip.SetToolTip(buttonDelete, resources.GetString("buttonDelete.ToolTip"));
            buttonDelete.UseVisualStyleBackColor = false;
            buttonDelete.Click += ButtonDelete_Click;
            // 
            // buttonDuplicate
            // 
            resources.ApplyResources(buttonDuplicate, "buttonDuplicate");
            buttonDuplicate.BackColor = System.Drawing.Color.SteelBlue;
            buttonDuplicate.ForeColor = System.Drawing.Color.White;
            buttonDuplicate.Name = "buttonDuplicate";
            toolTip.SetToolTip(buttonDuplicate, resources.GetString("buttonDuplicate.ToolTip"));
            buttonDuplicate.UseVisualStyleBackColor = false;
            buttonDuplicate.Click += buttonDuplicate_Click;
            // 
            // buttonDown
            // 
            resources.ApplyResources(buttonDown, "buttonDown");
            buttonDown.Name = "buttonDown";
            toolTip.SetToolTip(buttonDown, resources.GetString("buttonDown.ToolTip"));
            buttonDown.UseVisualStyleBackColor = false;
            buttonDown.Click += ButtonDown_Click;
            // 
            // buttonUp
            // 
            resources.ApplyResources(buttonUp, "buttonUp");
            buttonUp.Name = "buttonUp";
            toolTip.SetToolTip(buttonUp, resources.GetString("buttonUp.ToolTip"));
            buttonUp.UseVisualStyleBackColor = false;
            buttonUp.Click += ButtonUp_Click;
            // 
            // groupBoxCrystalInformation
            // 
            resources.ApplyResources(groupBoxCrystalInformation, "groupBoxCrystalInformation");
            captureExtender.SetCapture(groupBoxCrystalInformation, true);
            groupBoxCrystalInformation.Controls.Add(crystalControl);
            groupBoxCrystalInformation.ForeColor = System.Drawing.SystemColors.ControlText;
            groupBoxCrystalInformation.Name = "groupBoxCrystalInformation";
            groupBoxCrystalInformation.TabStop = false;
            toolTip.SetToolTip(groupBoxCrystalInformation, resources.GetString("groupBoxCrystalInformation.ToolTip"));
            // 
            // crystalControl
            // 
            resources.ApplyResources(crystalControl, "crystalControl");
            crystalControl.AllowDrop = true;
            crystalControl.ColorControlVisible = false;
            crystalControl.Name = "crystalControl";
            toolTip.SetToolTip(crystalControl, resources.GetString("crystalControl.ToolTip"));
            crystalControl.VisibleBondsPolyhedraTab = false;
            crystalControl.VisibleElasticityTab = false; // 260604Cl 弾性定数タブは ReciPro では非表示。旧: = true
            crystalControl.VisibleEOSTab = false; // 260604Cl EOS タブは ReciPro では非表示 (状態方程式は PDIndexer 用機能)。旧: = true
            crystalControl.CrystalChanged += crystalControl_CrystalChanged;
            crystalControl.BeamInteraction_VisibleChanged += crystalControl_BeamInteraction_VisibleChanged;
            crystalControl.SymmetryInformation_VisibleChanged += CrystalControl_SymmetryInformation_VisibleChanged;
            // 
            // flowLayoutPanelCrystalEdit
            // 
            resources.ApplyResources(flowLayoutPanelCrystalEdit, "flowLayoutPanelCrystalEdit");
            captureExtender.SetCapture(flowLayoutPanelCrystalEdit, true);
            flowLayoutPanelCrystalEdit.Controls.Add(buttonAdd);
            flowLayoutPanelCrystalEdit.Controls.Add(buttonReplace);
            flowLayoutPanelCrystalEdit.Name = "flowLayoutPanelCrystalEdit";
            toolTip.SetToolTip(flowLayoutPanelCrystalEdit, resources.GetString("flowLayoutPanelCrystalEdit.ToolTip"));
            // 
            // buttonAdd
            // 
            resources.ApplyResources(buttonAdd, "buttonAdd");
            buttonAdd.BackColor = System.Drawing.Color.SteelBlue;
            buttonAdd.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonAdd.Name = "buttonAdd";
            toolTip.SetToolTip(buttonAdd, resources.GetString("buttonAdd.ToolTip"));
            buttonAdd.UseVisualStyleBackColor = false;
            buttonAdd.Click += ButtonAdd_Click;
            // 
            // buttonReplace
            // 
            resources.ApplyResources(buttonReplace, "buttonReplace");
            buttonReplace.BackColor = System.Drawing.Color.SteelBlue;
            buttonReplace.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonReplace.Name = "buttonReplace";
            toolTip.SetToolTip(buttonReplace, resources.GetString("buttonReplace.ToolTip"));
            buttonReplace.UseVisualStyleBackColor = false;
            buttonReplace.Click += ButtonReplace_Click;
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            captureExtender.SetCapture(panel1, true);
            panel1.Controls.Add(groupBoxProjectAlong);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(groupBoxArrows);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(groupBoxCurrentDirection);
            panel1.Controls.Add(label8);
            panel1.Name = "panel1";
            toolTip.SetToolTip(panel1, resources.GetString("panel1.ToolTip"));
            // 
            // groupBoxProjectAlong
            // 
            resources.ApplyResources(groupBoxProjectAlong, "groupBoxProjectAlong");
            captureExtender.SetCapture(groupBoxProjectAlong, true);
            groupBoxProjectAlong.Controls.Add(indexControlPlane);
            groupBoxProjectAlong.Controls.Add(flowLayoutPanelSetPlane);
            groupBoxProjectAlong.Controls.Add(indexControlAxis);
            groupBoxProjectAlong.Controls.Add(flowLayoutPanelSetAxis);
            groupBoxProjectAlong.Name = "groupBoxProjectAlong";
            groupBoxProjectAlong.TabStop = false;
            toolTip.SetToolTip(groupBoxProjectAlong, resources.GetString("groupBoxProjectAlong.ToolTip"));
            // 
            // indexControlPlane
            // 
            resources.ApplyResources(indexControlPlane, "indexControlPlane");
            indexControlPlane.BoxWidth = 40;
            indexControlPlane.BoxWidthEnabled = false;
            indexControlPlane.Name = "indexControlPlane";
            toolTip.SetToolTip(indexControlPlane, resources.GetString("indexControlPlane.ToolTip"));
            indexControlPlane.Values = ((int, int, int))resources.GetObject("indexControlPlane.Values");
            // 
            // flowLayoutPanelSetPlane
            // 
            resources.ApplyResources(flowLayoutPanelSetPlane, "flowLayoutPanelSetPlane");
            flowLayoutPanelSetPlane.Controls.Add(buttonSetPlane);
            flowLayoutPanelSetPlane.Controls.Add(checkBoxFixePlane);
            flowLayoutPanelSetPlane.Name = "flowLayoutPanelSetPlane";
            toolTip.SetToolTip(flowLayoutPanelSetPlane, resources.GetString("flowLayoutPanelSetPlane.ToolTip"));
            // 
            // buttonSetPlane
            // 
            resources.ApplyResources(buttonSetPlane, "buttonSetPlane");
            buttonSetPlane.Name = "buttonSetPlane";
            toolTip.SetToolTip(buttonSetPlane, resources.GetString("buttonSetPlane.ToolTip"));
            buttonSetPlane.UseVisualStyleBackColor = true;
            buttonSetPlane.Click += buttonSetVector_Click;
            // 
            // checkBoxFixePlane
            // 
            resources.ApplyResources(checkBoxFixePlane, "checkBoxFixePlane");
            checkBoxFixePlane.Name = "checkBoxFixePlane";
            toolTip.SetToolTip(checkBoxFixePlane, resources.GetString("checkBoxFixePlane.ToolTip"));
            checkBoxFixePlane.UseVisualStyleBackColor = true;
            checkBoxFixePlane.CheckedChanged += checkBoxFixPlane_CheckedChanged;
            // 
            // indexControlAxis
            // 
            resources.ApplyResources(indexControlAxis, "indexControlAxis");
            indexControlAxis.BoxWidth = 40;
            indexControlAxis.BoxWidthEnabled = false;
            indexControlAxis.Mode = IndexControl.ModeEnum.Axis;
            indexControlAxis.Name = "indexControlAxis";
            toolTip.SetToolTip(indexControlAxis, resources.GetString("indexControlAxis.ToolTip"));
            indexControlAxis.Values = ((int, int, int))resources.GetObject("indexControlAxis.Values");
            // 
            // flowLayoutPanelSetAxis
            // 
            resources.ApplyResources(flowLayoutPanelSetAxis, "flowLayoutPanelSetAxis");
            flowLayoutPanelSetAxis.Controls.Add(buttonSetAxis);
            flowLayoutPanelSetAxis.Controls.Add(checkBoxFixAxis);
            flowLayoutPanelSetAxis.Name = "flowLayoutPanelSetAxis";
            toolTip.SetToolTip(flowLayoutPanelSetAxis, resources.GetString("flowLayoutPanelSetAxis.ToolTip"));
            // 
            // buttonSetAxis
            // 
            resources.ApplyResources(buttonSetAxis, "buttonSetAxis");
            buttonSetAxis.Name = "buttonSetAxis";
            toolTip.SetToolTip(buttonSetAxis, resources.GetString("buttonSetAxis.ToolTip"));
            buttonSetAxis.UseVisualStyleBackColor = true;
            buttonSetAxis.Click += buttonSetVector_Click;
            // 
            // checkBoxFixAxis
            // 
            resources.ApplyResources(checkBoxFixAxis, "checkBoxFixAxis");
            checkBoxFixAxis.Name = "checkBoxFixAxis";
            toolTip.SetToolTip(checkBoxFixAxis, resources.GetString("checkBoxFixAxis.ToolTip"));
            checkBoxFixAxis.UseVisualStyleBackColor = true;
            checkBoxFixAxis.CheckedChanged += checkBoxFixAxis_CheckedChanged;
            // 
            // panel3
            // 
            resources.ApplyResources(panel3, "panel3");
            panel3.Name = "panel3";
            toolTip.SetToolTip(panel3, resources.GetString("panel3.ToolTip"));
            // 
            // groupBoxArrows
            // 
            resources.ApplyResources(groupBoxArrows, "groupBoxArrows");
            captureExtender.SetCapture(groupBoxArrows, true);
            groupBoxArrows.Controls.Add(tableLayoutPanel1);
            groupBoxArrows.Controls.Add(panelArrowStep);
            groupBoxArrows.Controls.Add(checkBoxAnimation);
            groupBoxArrows.Name = "groupBoxArrows";
            groupBoxArrows.TabStop = false;
            toolTip.SetToolTip(groupBoxArrows, resources.GetString("groupBoxArrows.ToolTip"));
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 1);
            tableLayoutPanel1.Controls.Add(buttonTopLeft, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonLeft, 0, 1);
            tableLayoutPanel1.Controls.Add(buttonBottomLeft, 0, 2);
            tableLayoutPanel1.Controls.Add(buttonBottom, 1, 2);
            tableLayoutPanel1.Controls.Add(buttonBottomRight, 2, 2);
            tableLayoutPanel1.Controls.Add(buttonTop, 1, 0);
            tableLayoutPanel1.Controls.Add(buttonTopRight, 2, 0);
            tableLayoutPanel1.Controls.Add(buttonRight, 2, 1);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            toolTip.SetToolTip(tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(tableLayoutPanel2, "tableLayoutPanel2");
            tableLayoutPanel2.Controls.Add(buttonAntiClock, 1, 0);
            tableLayoutPanel2.Controls.Add(buttonClock, 0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            toolTip.SetToolTip(tableLayoutPanel2, resources.GetString("tableLayoutPanel2.ToolTip"));
            // 
            // buttonAntiClock
            // 
            resources.ApplyResources(buttonAntiClock, "buttonAntiClock");
            buttonAntiClock.Name = "buttonAntiClock";
            toolTip.SetToolTip(buttonAntiClock, resources.GetString("buttonAntiClock.ToolTip"));
            buttonAntiClock.UseVisualStyleBackColor = true;
            buttonAntiClock.Click += ButtonDirection_Click;
            // 
            // buttonClock
            // 
            resources.ApplyResources(buttonClock, "buttonClock");
            buttonClock.Name = "buttonClock";
            toolTip.SetToolTip(buttonClock, resources.GetString("buttonClock.ToolTip"));
            buttonClock.UseVisualStyleBackColor = true;
            buttonClock.Click += ButtonDirection_Click;
            // 
            // buttonTopLeft
            // 
            resources.ApplyResources(buttonTopLeft, "buttonTopLeft");
            buttonTopLeft.Name = "buttonTopLeft";
            toolTip.SetToolTip(buttonTopLeft, resources.GetString("buttonTopLeft.ToolTip"));
            buttonTopLeft.UseVisualStyleBackColor = true;
            buttonTopLeft.Click += ButtonDirection_Click;
            // 
            // buttonLeft
            // 
            resources.ApplyResources(buttonLeft, "buttonLeft");
            buttonLeft.Name = "buttonLeft";
            toolTip.SetToolTip(buttonLeft, resources.GetString("buttonLeft.ToolTip"));
            buttonLeft.UseVisualStyleBackColor = true;
            buttonLeft.Click += ButtonDirection_Click;
            // 
            // buttonBottomLeft
            // 
            resources.ApplyResources(buttonBottomLeft, "buttonBottomLeft");
            buttonBottomLeft.Name = "buttonBottomLeft";
            toolTip.SetToolTip(buttonBottomLeft, resources.GetString("buttonBottomLeft.ToolTip"));
            buttonBottomLeft.UseVisualStyleBackColor = false;
            buttonBottomLeft.Click += ButtonDirection_Click;
            // 
            // buttonBottom
            // 
            resources.ApplyResources(buttonBottom, "buttonBottom");
            buttonBottom.Name = "buttonBottom";
            toolTip.SetToolTip(buttonBottom, resources.GetString("buttonBottom.ToolTip"));
            buttonBottom.UseVisualStyleBackColor = true;
            buttonBottom.Click += ButtonDirection_Click;
            // 
            // buttonBottomRight
            // 
            resources.ApplyResources(buttonBottomRight, "buttonBottomRight");
            buttonBottomRight.Name = "buttonBottomRight";
            toolTip.SetToolTip(buttonBottomRight, resources.GetString("buttonBottomRight.ToolTip"));
            buttonBottomRight.UseVisualStyleBackColor = true;
            buttonBottomRight.Click += ButtonDirection_Click;
            // 
            // buttonTop
            // 
            resources.ApplyResources(buttonTop, "buttonTop");
            buttonTop.Name = "buttonTop";
            toolTip.SetToolTip(buttonTop, resources.GetString("buttonTop.ToolTip"));
            buttonTop.UseVisualStyleBackColor = false;
            buttonTop.Click += ButtonDirection_Click;
            // 
            // buttonTopRight
            // 
            resources.ApplyResources(buttonTopRight, "buttonTopRight");
            buttonTopRight.Name = "buttonTopRight";
            toolTip.SetToolTip(buttonTopRight, resources.GetString("buttonTopRight.ToolTip"));
            buttonTopRight.UseVisualStyleBackColor = false;
            buttonTopRight.Click += ButtonDirection_Click;
            // 
            // buttonRight
            // 
            resources.ApplyResources(buttonRight, "buttonRight");
            buttonRight.Name = "buttonRight";
            toolTip.SetToolTip(buttonRight, resources.GetString("buttonRight.ToolTip"));
            buttonRight.UseVisualStyleBackColor = false;
            buttonRight.Click += ButtonDirection_Click;
            // 
            // panelArrowStep
            // 
            resources.ApplyResources(panelArrowStep, "panelArrowStep");
            panelArrowStep.Controls.Add(numericBoxStep);
            panelArrowStep.Name = "panelArrowStep";
            toolTip.SetToolTip(panelArrowStep, resources.GetString("panelArrowStep.ToolTip"));
            // 
            // numericBoxStep
            // 
            resources.ApplyResources(numericBoxStep, "numericBoxStep");
            numericBoxStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxStep.Maximum = 360D;
            numericBoxStep.Minimum = 0.001D;
            numericBoxStep.Name = "numericBoxStep";
            numericBoxStep.RadianValue = 0.17453292519943295D;
            numericBoxStep.ShowUpDown = true;
            numericBoxStep.SmartIncrement = true;
            numericBoxStep.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxStep, resources.GetString("numericBoxStep.ToolTip1"));
            numericBoxStep.Value = 10D;
            // 
            // checkBoxAnimation
            // 
            resources.ApplyResources(checkBoxAnimation, "checkBoxAnimation");
            checkBoxAnimation.Name = "checkBoxAnimation";
            toolTip.SetToolTip(checkBoxAnimation, resources.GetString("checkBoxAnimation.ToolTip"));
            checkBoxAnimation.UseVisualStyleBackColor = true;
            checkBoxAnimation.CheckedChanged += CheckBoxAnimation_CheckedChanged;
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            toolTip.SetToolTip(panel2, resources.GetString("panel2.ToolTip"));
            // 
            // groupBoxCurrentDirection
            // 
            resources.ApplyResources(groupBoxCurrentDirection, "groupBoxCurrentDirection");
            captureExtender.SetCapture(groupBoxCurrentDirection, true);
            groupBoxCurrentDirection.Controls.Add(panelCrystalDirection);
            groupBoxCurrentDirection.Controls.Add(labelCurrentIndex);
            groupBoxCurrentDirection.Name = "groupBoxCurrentDirection";
            groupBoxCurrentDirection.TabStop = false;
            toolTip.SetToolTip(groupBoxCurrentDirection, resources.GetString("groupBoxCurrentDirection.ToolTip"));
            // 
            // panelCrystalDirection
            // 
            resources.ApplyResources(panelCrystalDirection, "panelCrystalDirection");
            panelCrystalDirection.Controls.Add(tableLayoutPanel3);
            panelCrystalDirection.Controls.Add(label7);
            panelCrystalDirection.Controls.Add(buttonReset);
            panelCrystalDirection.Controls.Add(numericBoxMaxUVW);
            panelCrystalDirection.Name = "panelCrystalDirection";
            toolTip.SetToolTip(panelCrystalDirection, resources.GetString("panelCrystalDirection.ToolTip"));
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(tableLayoutPanel3, "tableLayoutPanel3");
            tableLayoutPanel3.Controls.Add(label6, 2, 2);
            tableLayoutPanel3.Controls.Add(numericBoxEulerPsi, 1, 2);
            tableLayoutPanel3.Controls.Add(label2, 2, 0);
            tableLayoutPanel3.Controls.Add(numericBoxEulerTheta, 1, 1);
            tableLayoutPanel3.Controls.Add(numericBoxEulerPhi, 1, 0);
            tableLayoutPanel3.Controls.Add(label5, 2, 1);
            tableLayoutPanel3.Controls.Add(labelLaTex1, 0, 1);
            tableLayoutPanel3.Controls.Add(labelLaTex2, 0, 2);
            tableLayoutPanel3.Controls.Add(labelLaTex3, 0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            toolTip.SetToolTip(tableLayoutPanel3, resources.GetString("tableLayoutPanel3.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // numericBoxEulerPsi
            // 
            resources.ApplyResources(numericBoxEulerPsi, "numericBoxEulerPsi");
            numericBoxEulerPsi.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEulerPsi.DecimalPlaces = 3;
            numericBoxEulerPsi.Maximum = 360D;
            numericBoxEulerPsi.Minimum = -360D;
            numericBoxEulerPsi.Name = "numericBoxEulerPsi";
            numericBoxEulerPsi.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxEulerPsi, resources.GetString("numericBoxEulerPsi.ToolTip1"));
            numericBoxEulerPsi.ValueChanged += numericBoxEulerAngle_ValueChanged;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // numericBoxEulerTheta
            // 
            resources.ApplyResources(numericBoxEulerTheta, "numericBoxEulerTheta");
            numericBoxEulerTheta.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEulerTheta.DecimalPlaces = 3;
            numericBoxEulerTheta.Maximum = 360D;
            numericBoxEulerTheta.Minimum = -360D;
            numericBoxEulerTheta.Name = "numericBoxEulerTheta";
            numericBoxEulerTheta.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxEulerTheta, resources.GetString("numericBoxEulerTheta.ToolTip1"));
            numericBoxEulerTheta.ValueChanged += numericBoxEulerAngle_ValueChanged;
            // 
            // numericBoxEulerPhi
            // 
            resources.ApplyResources(numericBoxEulerPhi, "numericBoxEulerPhi");
            numericBoxEulerPhi.BackColor = System.Drawing.SystemColors.Control;
            numericBoxEulerPhi.DecimalPlaces = 3;
            numericBoxEulerPhi.Maximum = 360D;
            numericBoxEulerPhi.Minimum = -360D;
            numericBoxEulerPhi.Name = "numericBoxEulerPhi";
            numericBoxEulerPhi.ShowUpDown = true;
            toolTip.SetToolTip(numericBoxEulerPhi, resources.GetString("numericBoxEulerPhi.ToolTip1"));
            numericBoxEulerPhi.ValueChanged += numericBoxEulerAngle_ValueChanged;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // labelLaTex1
            // 
            resources.ApplyResources(labelLaTex1, "labelLaTex1");
            labelLaTex1.Name = "labelLaTex1";
            labelLaTex1.Thickness = 0.5D;
            toolTip.SetToolTip(labelLaTex1, resources.GetString("labelLaTex1.ToolTip"));
            // 
            // labelLaTex2
            // 
            resources.ApplyResources(labelLaTex2, "labelLaTex2");
            labelLaTex2.Name = "labelLaTex2";
            labelLaTex2.Thickness = 0.5D;
            toolTip.SetToolTip(labelLaTex2, resources.GetString("labelLaTex2.ToolTip"));
            // 
            // labelLaTex3
            // 
            resources.ApplyResources(labelLaTex3, "labelLaTex3");
            labelLaTex3.Name = "labelLaTex3";
            labelLaTex3.Thickness = 0.5D;
            toolTip.SetToolTip(labelLaTex3, resources.GetString("labelLaTex3.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip"));
            // 
            // buttonReset
            // 
            resources.ApplyResources(buttonReset, "buttonReset");
            buttonReset.Name = "buttonReset";
            toolTip.SetToolTip(buttonReset, resources.GetString("buttonReset.ToolTip"));
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += ButtonReset_Click;
            // 
            // numericBoxMaxUVW
            // 
            resources.ApplyResources(numericBoxMaxUVW, "numericBoxMaxUVW");
            numericBoxMaxUVW.BackColor = System.Drawing.SystemColors.Control;
            numericBoxMaxUVW.Maximum = 100D;
            numericBoxMaxUVW.Minimum = 1D;
            numericBoxMaxUVW.Name = "numericBoxMaxUVW";
            numericBoxMaxUVW.RadianValue = 0.52359877559829882D;
            numericBoxMaxUVW.ShowUpDown = true;
            numericBoxMaxUVW.SkipEventDuringInput = false;
            numericBoxMaxUVW.ThousandsSeparator = true;
            toolTip.SetToolTip(numericBoxMaxUVW, resources.GetString("numericBoxMaxUVW.ToolTip1"));
            numericBoxMaxUVW.Value = 30D;
            numericBoxMaxUVW.ValueChanged += numericBoxMaxUVW_ValueChanged;
            // 
            // labelCurrentIndex
            // 
            resources.ApplyResources(labelCurrentIndex, "labelCurrentIndex");
            labelCurrentIndex.BackColor = System.Drawing.Color.Transparent;
            labelCurrentIndex.Name = "labelCurrentIndex";
            toolTip.SetToolTip(labelCurrentIndex, resources.GetString("labelCurrentIndex.ToolTip"));
            labelCurrentIndex.DoubleClick += labelCurrentIndex_DoubleClick;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            toolTip.SetToolTip(label8, resources.GetString("label8.ToolTip"));
            // 
            // toolStrip1
            // 
            resources.ApplyResources(toolStrip1, "toolStrip1");
            captureExtender.SetCapture(toolStrip1, true);
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripLabel1, toolStripSeparator15, toolStripButtonDatabase, toolStripSeparator14, toolStripButtonSymmetryInformation, toolStripSeparator9, toolStripButtonBeamInteraction, toolStripSeparator12, toolStripButtonRotation, toolStripSeparator8, toolStripButtonStructureViewer, toolStripSeparator1, toolStripButtonStereonet, toolStripSeparator7, toolStripButtonDiffractionSingle, toolStripSeparator4, toolStripButtonTrajectorySimulator, toolStripSeparator10, toolStripButtonImageSimulator, toolStripSeparator13, toolStripButtonSpotIDv1, toolStripSeparator2, toolStripButtonSpotIDv2, toolStripSeparator24, toolStripButtonEBSD, toolStripSeparator19, toolStripButtonDiffractionPoly });
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            toolTip.SetToolTip(toolStrip1, resources.GetString("toolStrip1.ToolTip"));
            // 
            // toolStripLabel1
            // 
            resources.ApplyResources(toolStripLabel1, "toolStripLabel1");
            toolStripLabel1.Name = "toolStripLabel1";
            // 
            // toolStripSeparator15
            // 
            resources.ApplyResources(toolStripSeparator15, "toolStripSeparator15");
            toolStripSeparator15.Name = "toolStripSeparator15";
            // 
            // toolStripButtonDatabase
            // 
            resources.ApplyResources(toolStripButtonDatabase, "toolStripButtonDatabase");
            toolStripButtonDatabase.Name = "toolStripButtonDatabase";
            toolStripButtonDatabase.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator14
            // 
            resources.ApplyResources(toolStripSeparator14, "toolStripSeparator14");
            toolStripSeparator14.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            toolStripSeparator14.Name = "toolStripSeparator14";
            // 
            // toolStripButtonSymmetryInformation
            // 
            resources.ApplyResources(toolStripButtonSymmetryInformation, "toolStripButtonSymmetryInformation");
            toolStripButtonSymmetryInformation.Name = "toolStripButtonSymmetryInformation";
            toolStripButtonSymmetryInformation.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator9
            // 
            resources.ApplyResources(toolStripSeparator9, "toolStripSeparator9");
            toolStripSeparator9.Name = "toolStripSeparator9";
            // 
            // toolStripButtonBeamInteraction
            // 
            resources.ApplyResources(toolStripButtonBeamInteraction, "toolStripButtonBeamInteraction");
            toolStripButtonBeamInteraction.Name = "toolStripButtonBeamInteraction";
            toolStripButtonBeamInteraction.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator12
            // 
            resources.ApplyResources(toolStripSeparator12, "toolStripSeparator12");
            toolStripSeparator12.Name = "toolStripSeparator12";
            // 
            // toolStripButtonRotation
            // 
            resources.ApplyResources(toolStripButtonRotation, "toolStripButtonRotation");
            toolStripButtonRotation.Name = "toolStripButtonRotation";
            toolStripButtonRotation.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator8
            // 
            resources.ApplyResources(toolStripSeparator8, "toolStripSeparator8");
            toolStripSeparator8.Name = "toolStripSeparator8";
            // 
            // toolStripButtonStructureViewer
            // 
            resources.ApplyResources(toolStripButtonStructureViewer, "toolStripButtonStructureViewer");
            toolStripButtonStructureViewer.DoubleClickEnabled = true;
            toolStripButtonStructureViewer.Name = "toolStripButtonStructureViewer";
            toolStripButtonStructureViewer.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            toolStripButtonStructureViewer.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripButtonStereonet
            // 
            resources.ApplyResources(toolStripButtonStereonet, "toolStripButtonStereonet");
            toolStripButtonStereonet.Name = "toolStripButtonStereonet";
            toolStripButtonStereonet.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator7
            // 
            resources.ApplyResources(toolStripSeparator7, "toolStripSeparator7");
            toolStripSeparator7.Name = "toolStripSeparator7";
            // 
            // toolStripButtonDiffractionSingle
            // 
            resources.ApplyResources(toolStripButtonDiffractionSingle, "toolStripButtonDiffractionSingle");
            toolStripButtonDiffractionSingle.Name = "toolStripButtonDiffractionSingle";
            toolStripButtonDiffractionSingle.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator4
            // 
            resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
            toolStripSeparator4.Name = "toolStripSeparator4";
            // 
            // toolStripButtonTrajectorySimulator
            // 
            resources.ApplyResources(toolStripButtonTrajectorySimulator, "toolStripButtonTrajectorySimulator");
            toolStripButtonTrajectorySimulator.Name = "toolStripButtonTrajectorySimulator";
            toolStripButtonTrajectorySimulator.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator10
            // 
            resources.ApplyResources(toolStripSeparator10, "toolStripSeparator10");
            toolStripSeparator10.Name = "toolStripSeparator10";
            // 
            // toolStripButtonImageSimulator
            // 
            resources.ApplyResources(toolStripButtonImageSimulator, "toolStripButtonImageSimulator");
            toolStripButtonImageSimulator.Name = "toolStripButtonImageSimulator";
            toolStripButtonImageSimulator.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator13
            // 
            resources.ApplyResources(toolStripSeparator13, "toolStripSeparator13");
            toolStripSeparator13.Name = "toolStripSeparator13";
            // 
            // toolStripButtonSpotIDv1
            // 
            resources.ApplyResources(toolStripButtonSpotIDv1, "toolStripButtonSpotIDv1");
            toolStripButtonSpotIDv1.Name = "toolStripButtonSpotIDv1";
            toolStripButtonSpotIDv1.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // toolStripButtonSpotIDv2
            // 
            resources.ApplyResources(toolStripButtonSpotIDv2, "toolStripButtonSpotIDv2");
            toolStripButtonSpotIDv2.Name = "toolStripButtonSpotIDv2";
            toolStripButtonSpotIDv2.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator24
            // 
            resources.ApplyResources(toolStripSeparator24, "toolStripSeparator24");
            toolStripSeparator24.Name = "toolStripSeparator24";
            // 
            // toolStripButtonEBSD
            // 
            resources.ApplyResources(toolStripButtonEBSD, "toolStripButtonEBSD");
            toolStripButtonEBSD.Name = "toolStripButtonEBSD";
            toolStripButtonEBSD.MouseDown += toolStripButtons_MouseDown;
            // 
            // toolStripSeparator19
            // 
            resources.ApplyResources(toolStripSeparator19, "toolStripSeparator19");
            toolStripSeparator19.Name = "toolStripSeparator19";
            // 
            // toolStripButtonDiffractionPoly
            // 
            resources.ApplyResources(toolStripButtonDiffractionPoly, "toolStripButtonDiffractionPoly");
            toolStripButtonDiffractionPoly.Name = "toolStripButtonDiffractionPoly";
            toolStripButtonDiffractionPoly.CheckedChanged += toolStripButtonPolycrystallineDiffraction_CheckedChanged;
            toolStripButtonDiffractionPoly.MouseDown += toolStripButtons_MouseDown;
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.GripMargin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, optionToolStripMenuItem, helpToolStripMenuItem, languageToolStripMenuItem, macroToolStripMenuItem });
            menuStrip1.Name = "menuStrip1";
            toolTip.SetToolTip(menuStrip1, resources.GetString("menuStrip1.ToolTip"));
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            captureExtender.SetCapture(fileToolStripMenuItem, true);
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { readCrystalDataToolStripMenuItem, readCrystalDataAndAddToolStripMenuItem, toolStripMenuItemReadInitialCrystalList, toolStripSeparator21, readCrystalFromCIFOrAMCFileToolStripMenuItem, toolStripSeparator6, saveCrystalDataToolStripMenuItem, toolStripMenuItemExportCIF, toolStripSeparator5, toolStripMenuItem1, toolStripSeparator3, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // readCrystalDataToolStripMenuItem
            // 
            resources.ApplyResources(readCrystalDataToolStripMenuItem, "readCrystalDataToolStripMenuItem");
            readCrystalDataToolStripMenuItem.Name = "readCrystalDataToolStripMenuItem";
            readCrystalDataToolStripMenuItem.Click += readCrystalDataToolStripMenuItem_Click;
            // 
            // readCrystalDataAndAddToolStripMenuItem
            // 
            resources.ApplyResources(readCrystalDataAndAddToolStripMenuItem, "readCrystalDataAndAddToolStripMenuItem");
            readCrystalDataAndAddToolStripMenuItem.Name = "readCrystalDataAndAddToolStripMenuItem";
            readCrystalDataAndAddToolStripMenuItem.Click += readCrystalDataAndAddtoolStripMenuItem_Click;
            // 
            // toolStripMenuItemReadInitialCrystalList
            // 
            resources.ApplyResources(toolStripMenuItemReadInitialCrystalList, "toolStripMenuItemReadInitialCrystalList");
            toolStripMenuItemReadInitialCrystalList.Name = "toolStripMenuItemReadInitialCrystalList";
            toolStripMenuItemReadInitialCrystalList.Click += ToolStripMenuItemReadInitialCrystalList_Click;
            // 
            // toolStripSeparator21
            // 
            resources.ApplyResources(toolStripSeparator21, "toolStripSeparator21");
            toolStripSeparator21.Name = "toolStripSeparator21";
            // 
            // readCrystalFromCIFOrAMCFileToolStripMenuItem
            // 
            resources.ApplyResources(readCrystalFromCIFOrAMCFileToolStripMenuItem, "readCrystalFromCIFOrAMCFileToolStripMenuItem");
            readCrystalFromCIFOrAMCFileToolStripMenuItem.Name = "readCrystalFromCIFOrAMCFileToolStripMenuItem";
            readCrystalFromCIFOrAMCFileToolStripMenuItem.Click += readCrystalFromCIFOrAMCFileToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
            toolStripSeparator6.Name = "toolStripSeparator6";
            // 
            // saveCrystalDataToolStripMenuItem
            // 
            resources.ApplyResources(saveCrystalDataToolStripMenuItem, "saveCrystalDataToolStripMenuItem");
            saveCrystalDataToolStripMenuItem.Name = "saveCrystalDataToolStripMenuItem";
            saveCrystalDataToolStripMenuItem.Click += SaveCrystalDataToolStripMenuItem_Click;
            // 
            // toolStripMenuItemExportCIF
            // 
            resources.ApplyResources(toolStripMenuItemExportCIF, "toolStripMenuItemExportCIF");
            toolStripMenuItemExportCIF.Name = "toolStripMenuItemExportCIF";
            toolStripMenuItemExportCIF.Click += toolStripMenuItemExportCIF_Click;
            // 
            // toolStripSeparator5
            // 
            resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
            toolStripSeparator5.Name = "toolStripSeparator5";
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // exitToolStripMenuItem
            // 
            resources.ApplyResources(exitToolStripMenuItem, "exitToolStripMenuItem");
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // optionToolStripMenuItem
            // 
            resources.ApplyResources(optionToolStripMenuItem, "optionToolStripMenuItem");
            captureExtender.SetCapture(optionToolStripMenuItem, true);
            optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolTipToolStripMenuItem, toolStripMenuItemUseMillerBravais, toolStripSeparator11, resetRegistryToolStripMenuItem, toolStripMenuItemDisableNative, disableOpneGLToolStripMenuItem, toolStripMenuItemDisableTextRendering, toolStripMenuItemUseMKL, toolStripMenuItemIonicScattering, toolStripMenuItemDarkMode, toolStripSeparator20, powderDiffractionFunctionToolStripMenuItem, toolStripSeparatorCapture, captureGUIToolStripMenuItem });
            optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            // 
            // toolTipToolStripMenuItem
            // 
            resources.ApplyResources(toolTipToolStripMenuItem, "toolTipToolStripMenuItem");
            toolTipToolStripMenuItem.Checked = true;
            toolTipToolStripMenuItem.CheckOnClick = true;
            toolTipToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            toolTipToolStripMenuItem.Name = "toolTipToolStripMenuItem";
            toolTipToolStripMenuItem.CheckedChanged += toolTipToolStripMenuItem_CheckedChanged;
            // 
            // toolStripMenuItemUseMillerBravais
            // 
            resources.ApplyResources(toolStripMenuItemUseMillerBravais, "toolStripMenuItemUseMillerBravais");
            toolStripMenuItemUseMillerBravais.CheckOnClick = true;
            toolStripMenuItemUseMillerBravais.Name = "toolStripMenuItemUseMillerBravais";
            toolStripMenuItemUseMillerBravais.CheckedChanged += toolStripMenuItemUseMillerBravais_CheckedChanged;
            // 
            // toolStripSeparator11
            // 
            resources.ApplyResources(toolStripSeparator11, "toolStripSeparator11");
            toolStripSeparator11.Name = "toolStripSeparator11";
            // 
            // resetRegistryToolStripMenuItem
            // 
            resources.ApplyResources(resetRegistryToolStripMenuItem, "resetRegistryToolStripMenuItem");
            resetRegistryToolStripMenuItem.CheckOnClick = true;
            resetRegistryToolStripMenuItem.Name = "resetRegistryToolStripMenuItem";
            // 
            // toolStripMenuItemDisableNative
            // 
            resources.ApplyResources(toolStripMenuItemDisableNative, "toolStripMenuItemDisableNative");
            toolStripMenuItemDisableNative.CheckOnClick = true;
            toolStripMenuItemDisableNative.Name = "toolStripMenuItemDisableNative";
            // 
            // disableOpneGLToolStripMenuItem
            // 
            resources.ApplyResources(disableOpneGLToolStripMenuItem, "disableOpneGLToolStripMenuItem");
            disableOpneGLToolStripMenuItem.CheckOnClick = true;
            disableOpneGLToolStripMenuItem.Name = "disableOpneGLToolStripMenuItem";
            // 
            // toolStripMenuItemDisableTextRendering
            // 
            resources.ApplyResources(toolStripMenuItemDisableTextRendering, "toolStripMenuItemDisableTextRendering");
            toolStripMenuItemDisableTextRendering.CheckOnClick = true;
            toolStripMenuItemDisableTextRendering.Name = "toolStripMenuItemDisableTextRendering";
            // 
            // toolStripMenuItemUseMKL
            // 
            resources.ApplyResources(toolStripMenuItemUseMKL, "toolStripMenuItemUseMKL");
            toolStripMenuItemUseMKL.CheckOnClick = true;
            toolStripMenuItemUseMKL.Name = "toolStripMenuItemUseMKL";
            toolStripMenuItemUseMKL.CheckedChanged += toolStripMenuItemUseMKL_CheckedChanged;
            //
            // toolStripMenuItemIonicScattering
            //
            resources.ApplyResources(toolStripMenuItemIonicScattering, "toolStripMenuItemIonicScattering");
            toolStripMenuItemIonicScattering.CheckOnClick = true;
            toolStripMenuItemIonicScattering.Name = "toolStripMenuItemIonicScattering";
            toolStripMenuItemIonicScattering.CheckedChanged += toolStripMenuItemIonicScattering_CheckedChanged;
            //
            // toolStripMenuItemDarkMode
            //
            resources.ApplyResources(toolStripMenuItemDarkMode, "toolStripMenuItemDarkMode");
            toolStripMenuItemDarkMode.CheckOnClick = true;
            toolStripMenuItemDarkMode.Name = "toolStripMenuItemDarkMode";
            toolStripMenuItemDarkMode.CheckedChanged += toolStripMenuItemDarkMode_CheckedChanged;
            // 
            // toolStripSeparator20
            // 
            resources.ApplyResources(toolStripSeparator20, "toolStripSeparator20");
            toolStripSeparator20.Name = "toolStripSeparator20";
            // 
            // powderDiffractionFunctionToolStripMenuItem
            // 
            resources.ApplyResources(powderDiffractionFunctionToolStripMenuItem, "powderDiffractionFunctionToolStripMenuItem");
            powderDiffractionFunctionToolStripMenuItem.CheckOnClick = true;
            powderDiffractionFunctionToolStripMenuItem.Name = "powderDiffractionFunctionToolStripMenuItem";
            powderDiffractionFunctionToolStripMenuItem.CheckedChanged += powderDiffractionFunctionsToolStripMenuItem_CheckedChanged;
            // 
            // toolStripSeparatorCapture
            // 
            resources.ApplyResources(toolStripSeparatorCapture, "toolStripSeparatorCapture");
            toolStripSeparatorCapture.Name = "toolStripSeparatorCapture";
            // 
            // captureGUIToolStripMenuItem
            // 
            resources.ApplyResources(captureGUIToolStripMenuItem, "captureGUIToolStripMenuItem");
            captureGUIToolStripMenuItem.Name = "captureGUIToolStripMenuItem";
            captureGUIToolStripMenuItem.Click += captureGUIToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
            captureExtender.SetCapture(helpToolStripMenuItem, true);
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { checkUpdatesToolStripMenuItem, toolStripSeparator16, hintToolStripMenuItem, versionHistoryToolStripMenuItem, licenseToolStripMenuItem, toolStripSeparator18, githubPageToolStripMenuItem, reportBugsRequestsOrCommentsToolStripMenuItem1, toolStripSeparator17, helpwebToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // checkUpdatesToolStripMenuItem
            // 
            resources.ApplyResources(checkUpdatesToolStripMenuItem, "checkUpdatesToolStripMenuItem");
            checkUpdatesToolStripMenuItem.Name = "checkUpdatesToolStripMenuItem";
            checkUpdatesToolStripMenuItem.Click += checkUpdatesToolStripMenuItem_Click;
            // 
            // toolStripSeparator16
            // 
            resources.ApplyResources(toolStripSeparator16, "toolStripSeparator16");
            toolStripSeparator16.Name = "toolStripSeparator16";
            // 
            // hintToolStripMenuItem
            // 
            resources.ApplyResources(hintToolStripMenuItem, "hintToolStripMenuItem");
            hintToolStripMenuItem.Name = "hintToolStripMenuItem";
            hintToolStripMenuItem.Click += hintToolStripMenuItem_Click;
            // 
            // versionHistoryToolStripMenuItem
            // 
            resources.ApplyResources(versionHistoryToolStripMenuItem, "versionHistoryToolStripMenuItem");
            versionHistoryToolStripMenuItem.Name = "versionHistoryToolStripMenuItem";
            versionHistoryToolStripMenuItem.Click += versionHistoryToolStripMenuItem_Click;
            // 
            // licenseToolStripMenuItem
            // 
            resources.ApplyResources(licenseToolStripMenuItem, "licenseToolStripMenuItem");
            licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            licenseToolStripMenuItem.Click += licenseToolStripMenuItem_Click;
            // 
            // toolStripSeparator18
            // 
            resources.ApplyResources(toolStripSeparator18, "toolStripSeparator18");
            toolStripSeparator18.Name = "toolStripSeparator18";
            // 
            // githubPageToolStripMenuItem
            // 
            resources.ApplyResources(githubPageToolStripMenuItem, "githubPageToolStripMenuItem");
            githubPageToolStripMenuItem.Name = "githubPageToolStripMenuItem";
            githubPageToolStripMenuItem.Click += githubPageToolStripMenuItem_Click;
            // 
            // reportBugsRequestsOrCommentsToolStripMenuItem1
            // 
            resources.ApplyResources(reportBugsRequestsOrCommentsToolStripMenuItem1, "reportBugsRequestsOrCommentsToolStripMenuItem1");
            reportBugsRequestsOrCommentsToolStripMenuItem1.Name = "reportBugsRequestsOrCommentsToolStripMenuItem1";
            reportBugsRequestsOrCommentsToolStripMenuItem1.Click += reportBugsRequestsOrCommentsToolStripMenuItem1_Click;
            // 
            // toolStripSeparator17
            // 
            resources.ApplyResources(toolStripSeparator17, "toolStripSeparator17");
            toolStripSeparator17.Name = "toolStripSeparator17";
            // 
            // helpwebToolStripMenuItem
            // 
            resources.ApplyResources(helpwebToolStripMenuItem, "helpwebToolStripMenuItem");
            helpwebToolStripMenuItem.Name = "helpwebToolStripMenuItem";
            helpwebToolStripMenuItem.Click += helpwebToolStripMenuItem_Click;
            // 
            // languageToolStripMenuItem
            // 
            resources.ApplyResources(languageToolStripMenuItem, "languageToolStripMenuItem");
            languageToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            captureExtender.SetCapture(languageToolStripMenuItem, true);
            languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            // 260618Cl: 言語サブメニュー項目は Designer 固定 (english/japanese/german) を廃止し、
            //   FormMain.PopulateLanguageMenu() が SupportedCultures.All.Where(Released) から動的生成する
            //   (旧 DropDownItems.AddRange / 各項目の ApplyResources・Tag・Click 配線はそちらへ移動)。
            //
            // macroToolStripMenuItem
            // 
            resources.ApplyResources(macroToolStripMenuItem, "macroToolStripMenuItem");
            captureExtender.SetCapture(macroToolStripMenuItem, true);
            macroToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { editorToolStripMenuItem, toolStripSeparator22 });
            macroToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            macroToolStripMenuItem.Name = "macroToolStripMenuItem";
            // 
            // editorToolStripMenuItem
            // 
            resources.ApplyResources(editorToolStripMenuItem, "editorToolStripMenuItem");
            editorToolStripMenuItem.Name = "editorToolStripMenuItem";
            editorToolStripMenuItem.Click += editorToolStripMenuItem_Click;
            // 
            // toolStripSeparator22
            // 
            resources.ApplyResources(toolStripSeparator22, "toolStripSeparator22");
            toolStripSeparator22.Name = "toolStripSeparator22";
            // 
            // contextMenuStripListBox
            // 
            resources.ApplyResources(contextMenuStripListBox, "contextMenuStripListBox");
            contextMenuStripListBox.ImageScalingSize = new System.Drawing.Size(0, 0);
            contextMenuStripListBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { renameToolStripMenuItem, exportAsCIFFormatToolStripMenuItem, duplicateToolStripMenuItem, deleteToolStripMenuItem });
            contextMenuStripListBox.Name = "contextMenuStrip1";
            contextMenuStripListBox.ShowImageMargin = false;
            toolTip.SetToolTip(contextMenuStripListBox, resources.GetString("contextMenuStripListBox.ToolTip"));
            // 
            // renameToolStripMenuItem
            // 
            resources.ApplyResources(renameToolStripMenuItem, "renameToolStripMenuItem");
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Click += renameToolStripMenuItem_Click;
            // 
            // exportAsCIFFormatToolStripMenuItem
            // 
            resources.ApplyResources(exportAsCIFFormatToolStripMenuItem, "exportAsCIFFormatToolStripMenuItem");
            exportAsCIFFormatToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            exportAsCIFFormatToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            exportAsCIFFormatToolStripMenuItem.Name = "exportAsCIFFormatToolStripMenuItem";
            exportAsCIFFormatToolStripMenuItem.Padding = new System.Windows.Forms.Padding(0);
            exportAsCIFFormatToolStripMenuItem.Click += exportAsCIFFormatToolStripMenuItem_Click;
            // 
            // duplicateToolStripMenuItem
            // 
            resources.ApplyResources(duplicateToolStripMenuItem, "duplicateToolStripMenuItem");
            duplicateToolStripMenuItem.BackColor = System.Drawing.Color.SteelBlue;
            duplicateToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            duplicateToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
            duplicateToolStripMenuItem.Click += duplicateToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            resources.ApplyResources(deleteToolStripMenuItem, "deleteToolStripMenuItem");
            deleteToolStripMenuItem.BackColor = System.Drawing.Color.IndianRed;
            deleteToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            deleteToolStripMenuItem.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // timer
            // 
            timer.Interval = 2;
            timer.Tick += Timer_Tick;
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            AllowDrop = true;
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(toolStripContainer1);
            ForeColor = System.Drawing.SystemColors.ControlText;
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            FormClosing += FormMain_FormClosing;
            Load += FormMain_Load;
            DragDrop += FormMain_DragDrop;
            DragEnter += FormMain_DragEnter;
            KeyDown += FormMain_KeyDown;
            KeyUp += FormMain_KeyUp;
            toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            toolStripContainer1.BottomToolStripPanel.PerformLayout();
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.RightToolStripPanel.ResumeLayout(false);
            toolStripContainer1.RightToolStripPanel.PerformLayout();
            toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            toolStripContainer1.TopToolStripPanel.PerformLayout();
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            groupBoxCrystalList.ResumeLayout(false);
            groupBoxCrystalList.PerformLayout();
            flowLayoutPanelCrystalOrder.ResumeLayout(false);
            flowLayoutPanelCrystalOrder.PerformLayout();
            groupBoxCrystalInformation.ResumeLayout(false);
            flowLayoutPanelCrystalEdit.ResumeLayout(false);
            flowLayoutPanelCrystalEdit.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBoxProjectAlong.ResumeLayout(false);
            groupBoxProjectAlong.PerformLayout();
            flowLayoutPanelSetPlane.ResumeLayout(false);
            flowLayoutPanelSetPlane.PerformLayout();
            flowLayoutPanelSetAxis.ResumeLayout(false);
            flowLayoutPanelSetAxis.PerformLayout();
            groupBoxArrows.ResumeLayout(false);
            groupBoxArrows.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panelArrowStep.ResumeLayout(false);
            groupBoxCurrentDirection.ResumeLayout(false);
            groupBoxCurrentDirection.PerformLayout();
            panelCrystalDirection.ResumeLayout(false);
            panelCrystalDirection.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            contextMenuStripListBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readCrystalDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCrystalDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        public System.Windows.Forms.ToolStripButton toolStripButtonStructureViewer;
        public System.Windows.Forms.ToolStripButton toolStripButtonStereonet;
        public System.Windows.Forms.ToolStripButton toolStripButtonDiffractionSingle;
        public System.Windows.Forms.ToolStripButton toolStripButtonSpotIDv2;
        private System.Windows.Forms.GroupBox groupBoxProjectAlong;
        private System.Windows.Forms.Button buttonSetAxis;
        private System.Windows.Forms.GroupBox groupBoxArrows;
        private System.Windows.Forms.Button buttonClock;
        private System.Windows.Forms.Button buttonAntiClock;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonBottomRight;
        private System.Windows.Forms.Button buttonBottomLeft;
        private System.Windows.Forms.Button buttonTopRight;
        private System.Windows.Forms.Button buttonTopLeft;
        private System.Windows.Forms.Button buttonBottom;
        private System.Windows.Forms.Button buttonTop;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonRight;
        private System.Windows.Forms.GroupBox groupBoxCrystalList;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDown;
        public System.Windows.Forms.Button buttonAdd;
        public System.Windows.Forms.Button buttonDelete;
        public System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.ToolStripMenuItem helpwebToolStripMenuItem;
        public CrystalControl crystalControl;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem toolTipToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        public System.Windows.Forms.Timer timer;
        private System.Windows.Forms.GroupBox groupBoxCrystalInformation;
        private System.Windows.Forms.Button buttonSetPlane;
        private System.Windows.Forms.SplitContainer splitContainer;
        public System.Windows.Forms.Button buttonClearAllCrystals;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripMenuItem hintToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readCrystalDataAndAddToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        public System.Windows.Forms.ToolStripButton toolStripButtonDiffractionPoly;
        public System.Windows.Forms.ToolStripButton toolStripButtonBeamInteraction;
        public System.Windows.Forms.ToolStripButton toolStripButtonSymmetryInformation;
        private System.Windows.Forms.ToolStripMenuItem resetRegistryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExportCIF;
        private System.Windows.Forms.ToolStripMenuItem checkUpdatesToolStripMenuItem;
        public System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label6;
        private NumericBox numericBoxEulerPsi;                                                                                                      // 260519Cl 変更: NumericUpDown → NumericBox
        private System.Windows.Forms.Label label2;
        private NumericBox numericBoxEulerTheta;                                                                                                    // 260519Cl 変更: NumericUpDown → NumericBox
        private NumericBox numericBoxEulerPhi;                                                                                                      // 260519Cl 変更: NumericUpDown → NumericBox
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBoxCurrentDirection;
        private System.Windows.Forms.Panel panelArrowStep;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        // 260618Cl: english/japanese/german の各フィールドは廃止 (言語メニューは FormMain.PopulateLanguageMenu で動的生成)。
        public System.Windows.Forms.ToolStripButton toolStripButtonSpotIDv1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label labelCurrentIndex;
        private NumericBox numericBoxMaxUVW;
        private NumericBox numericBoxStep;
        private System.Windows.Forms.CheckBox checkBoxAnimation;

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSetAxis;
        private System.Windows.Forms.CheckBox checkBoxFixAxis;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSetPlane;
        private System.Windows.Forms.CheckBox checkBoxFixePlane;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem disableOpneGLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReadInitialCrystalList;
        public System.Windows.Forms.ToolStripButton toolStripButtonRotation;
        public System.Windows.Forms.ToolStripButton toolStripButtonImageSimulator;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripMenuItem versionHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        public System.Windows.Forms.ToolStripButton toolStripButtonDatabase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem githubPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripMenuItem reportBugsRequestsOrCommentsToolStripMenuItem1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCrystalOrder;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCrystalEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripMenuItem powderDiffractionFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripMenuItem readCrystalFromCIFOrAMCFileToolStripMenuItem;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem macroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisableTextRendering;
        public System.Windows.Forms.ToolStripButton toolStripButtonTrajectorySimulator;
        private System.Windows.Forms.Panel panelCrystalDirection;
        public System.Windows.Forms.ToolStripButton toolStripButtonEBSD;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListBox;
        private System.Windows.Forms.ToolStripMenuItem exportAsCIFFormatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
        public System.Windows.Forms.Button buttonDuplicate;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDisableNative;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUseMKL;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemIonicScattering; // 260613Cl 追加
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDarkMode; // 260428Cl 追加
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUseMillerBravais;                                                                 // 260421Cl 追加
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorCapture; // 260323Cl 追加
        private System.Windows.Forms.ToolStripMenuItem captureGUIToolStripMenuItem; // 260323Cl 追加
        private LabelLaTeX labelLaTex1;
        private LabelLaTeX labelLaTex2;
        private LabelLaTeX labelLaTex3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private IndexControl indexControlAxis;
        private IndexControl indexControlPlane;
    }
}
