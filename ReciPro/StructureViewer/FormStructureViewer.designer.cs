namespace ReciPro
{
    partial class FormStructureViewer
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
        // groupBox1 -> groupBoxProjection
        // groupBox2 -> groupBoxAccessoryControls
        // groupBox3 -> groupBoxRenderingQuality
        // groupBox4 -> groupBoxLabel
        // groupBox5 -> groupBoxTransparency
        // groupBox6 -> groupBoxBondedAtoms
        // groupBox7 -> groupBoxProjectionCenter
        // flowLayoutPanel1 -> flowLayoutPanelLatticePlaneOptions
        // flowLayoutPanel2 -> flowLayoutPanelBoundType
        // flowLayoutPanel3 -> flowLayoutPanelLatticePlaneOpacity
        // flowLayoutPanel4 -> flowLayoutPanelGraphicsInfo
        // flowLayoutPanel5 -> flowLayoutPanelCellEdgeColors
        // flowLayoutPanel6 -> flowLayoutPanelCellPlaneColors
        // panel1 -> panelClientSize
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStructureViewer));
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            textBoxCalcInformation = new System.Windows.Forms.TextBox();
            label11 = new System.Windows.Forms.Label();
            textBoxAtomInformation = new System.Windows.Forms.TextBox();
            label13 = new System.Windows.Forms.Label();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            flowLayoutPanelLegend = new System.Windows.Forms.FlowLayoutPanel();
            tabControl = new System.Windows.Forms.TabControl();
            tabPageBounds = new System.Windows.Forms.TabPage();
            tabControlBoundOption = new System.Windows.Forms.TabControl();
            tabPageBoundUnitcell = new System.Windows.Forms.TabPage();
            buttonSetRange2 = new System.Windows.Forms.Button();
            buttonSetRange4 = new System.Windows.Forms.Button();
            buttonSetRange3 = new System.Windows.Forms.Button();
            buttonSetCenter1 = new System.Windows.Forms.Button();
            buttonCenter2 = new System.Windows.Forms.Button();
            buttonSetCenter3 = new System.Windows.Forms.Button();
            buttonSetRange0 = new System.Windows.Forms.Button();
            buttonSetRange1 = new System.Windows.Forms.Button();
            numericBoxCRange = new NumericBox();
            numericBoxBRange = new NumericBox();
            numericBoxARange = new NumericBox();
            numericBoxCCenter = new NumericBox();
            numericBoxBCenter = new NumericBox();
            numericBoxACenter = new NumericBox();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            tabPageBoundPlane = new System.Windows.Forms.TabPage();
            flowLayoutPanelLatticePlaneOptions = new System.Windows.Forms.FlowLayoutPanel();
            checkBoxShowBoundPlanes = new System.Windows.Forms.CheckBox();
            numericBoxBoundPlanesOpacity = new NumericBox();
            checkBoxClipObjects = new System.Windows.Forms.CheckBox();
            checkBoxHideAllAtoms = new System.Windows.Forms.CheckBox();
            flowLayoutPanelBoundType = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonBoundUnitCell = new System.Windows.Forms.RadioButton();
            radioButtonBoundPlane = new System.Windows.Forms.RadioButton();
            tabPageAtom = new System.Windows.Forms.TabPage();
            labelMessage = new System.Windows.Forms.Label();
            tabPageBond = new System.Windows.Forms.TabPage();
            tabPageUnitCell = new System.Windows.Forms.TabPage();
            checkBoxUnitCell = new System.Windows.Forms.CheckBox();
            groupBoxShowUnitCell = new System.Windows.Forms.GroupBox();
            flowLayoutPanelCellPlaneColors = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonCellPlaneColorAll = new System.Windows.Forms.RadioButton();
            colorControlCellPlane = new ColorControl();
            radioButtonCellPlaneColorEach = new System.Windows.Forms.RadioButton();
            colorControlCellPlaneA = new ColorControl();
            colorControlCellPlaneB = new ColorControl();
            colorControlCellPlaneC = new ColorControl();
            numericBoxCellPlaneAlpha = new NumericBox();
            flowLayoutPanelCellEdgeColors = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonCellEdgeColorAll = new System.Windows.Forms.RadioButton();
            colorControlCellEdge = new ColorControl();
            radioButtonCellEdgeColorEach = new System.Windows.Forms.RadioButton();
            colorControlCellEdgeA = new ColorControl();
            colorControlCellEdgeB = new ColorControl();
            colorControlCellEdgeC = new ColorControl();
            label8 = new System.Windows.Forms.Label();
            trackBarCellEdgeWidth = new System.Windows.Forms.TrackBar();
            numericBoxCellTranslationC = new NumericBox();
            numericBoxCellTranslationB = new NumericBox();
            numericBoxCellTranslationA = new NumericBox();
            checkBoxShowSubCell = new System.Windows.Forms.CheckBox();
            checkBoxCellShowEdge = new System.Windows.Forms.CheckBox();
            numericUpDownSubCellB = new System.Windows.Forms.NumericUpDown();
            label10 = new System.Windows.Forms.Label();
            checkBoxCellShowPlane = new System.Windows.Forms.CheckBox();
            numericUpDownSubCellC = new System.Windows.Forms.NumericUpDown();
            numericUpDownSubCellA = new System.Windows.Forms.NumericUpDown();
            label17 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            tabPageLatticePlane = new System.Windows.Forms.TabPage();
            flowLayoutPanelLatticePlaneOpacity = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxLatticePlaneOpacity = new NumericBox();
            tabPageCoordinateInformation = new System.Windows.Forms.TabPage();
            atomCoordinateTable1 = new AtomCoordinateTable();
            tabPageInformation = new System.Windows.Forms.TabPage();
            flowLayoutPanelGraphicsInfo = new System.Windows.Forms.FlowLayoutPanel();
            labelGraphicsCard = new System.Windows.Forms.Label();
            labelGraphicsDriver = new System.Windows.Forms.Label();
            labelOpenGLversion = new System.Windows.Forms.Label();
            tabPageProjection = new System.Windows.Forms.TabPage();
            groupBoxProjectionCenter = new System.Windows.Forms.GroupBox();
            flowLayoutPanelProjectionCenter = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxProjectionCenterX = new NumericBox();
            numericBoxProjectionCenterY = new NumericBox();
            numericBoxProjectionCenterZ = new NumericBox();
            label9 = new System.Windows.Forms.Label();
            radioButtonProjectionCenterCustom = new System.Windows.Forms.RadioButton();
            radioButtonProjectionCenter1 = new System.Windows.Forms.RadioButton();
            radioButtonProjectionCenter2 = new System.Windows.Forms.RadioButton();
            groupBoxProjection = new System.Windows.Forms.GroupBox();
            comboBoxProjectionMode = new System.Windows.Forms.ComboBox();
            trackBarPerspective = new System.Windows.Forms.TrackBar();
            groupBoxTransparency = new System.Windows.Forms.GroupBox();
            comboBoxTransparency = new System.Windows.Forms.ComboBox();
            checkBoxDepthFadingOut = new System.Windows.Forms.CheckBox();
            groupBoxRenderingQuality = new System.Windows.Forms.GroupBox();
            comboBoxRenderingQuality = new System.Windows.Forms.ComboBox();
            groupBoxDepthCueing = new System.Windows.Forms.GroupBox();
            trackBarAdvancedDepthCueingNear = new TrackBarAdvanced();
            label6 = new System.Windows.Forms.Label();
            trackBarAdvancedDepthCueingFar = new TrackBarAdvanced();
            label5 = new System.Windows.Forms.Label();
            tabPageMisc = new System.Windows.Forms.TabPage();
            groupBoxBondedAtoms = new System.Windows.Forms.GroupBox();
            checkBoxShowBondedAtoms = new System.Windows.Forms.CheckBox();
            groupBoxLabel = new System.Windows.Forms.GroupBox();
            colorControlLabelColor = new ColorControl();
            checkBoxShowLabel = new System.Windows.Forms.CheckBox();
            radioButtonUseMaterialColor = new System.Windows.Forms.RadioButton();
            radioButtonLabelUseFixedColor = new System.Windows.Forms.RadioButton();
            numericBoxLabelSize = new NumericBox();
            checkBoxLabelWhiteEdge = new System.Windows.Forms.CheckBox();
            label7 = new System.Windows.Forms.Label();
            groupBoxAccessoryControls = new System.Windows.Forms.GroupBox();
            checkBoxGroupByElement = new System.Windows.Forms.CheckBox();
            numericBoxLegendSize = new NumericBox();
            numericBoxAxesSize = new NumericBox();
            numericBoxLightSize = new NumericBox();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStripButtonCrystalAxes = new System.Windows.Forms.ToolStripButton();
            toolStripButtonLightDirection = new System.Windows.Forms.ToolStripButton();
            toolStripButtonLegend = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            toolStripLabelStatusInitialization = new System.Windows.Forms.ToolStripLabel();
            toolStripLabelStatusRendering = new System.Windows.Forms.ToolStripLabel();
            toolStripButtonLikeVesta = new System.Windows.Forms.ToolStripButton();
            toolStripButtonResetRotation = new System.Windows.Forms.ToolStripButton();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            saveMainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveLegendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveAxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyMainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyLegendToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            copyAxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveMovieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            SaveMovieMainImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            SaveMovieCrystalAxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            iLikeVESTAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            toolTip = new System.Windows.Forms.ToolTip(components);
            numericBoxClientHeight = new NumericBox();
            numericBoxClientWidth = new NumericBox();
            printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            tabPageCrystal = new System.Windows.Forms.TabPage();
            panelClientSize = new System.Windows.Forms.Panel();
            timer = new System.Windows.Forms.Timer(components);
            printDialog1 = new System.Windows.Forms.PrintDialog();
            pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageBounds.SuspendLayout();
            tabControlBoundOption.SuspendLayout();
            tabPageBoundUnitcell.SuspendLayout();
            flowLayoutPanelLatticePlaneOptions.SuspendLayout();
            flowLayoutPanelBoundType.SuspendLayout();
            tabPageAtom.SuspendLayout();
            tabPageUnitCell.SuspendLayout();
            groupBoxShowUnitCell.SuspendLayout();
            flowLayoutPanelCellPlaneColors.SuspendLayout();
            flowLayoutPanelCellEdgeColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarCellEdgeWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSubCellB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSubCellC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSubCellA).BeginInit();
            tabPageLatticePlane.SuspendLayout();
            flowLayoutPanelLatticePlaneOpacity.SuspendLayout();
            tabPageCoordinateInformation.SuspendLayout();
            tabPageInformation.SuspendLayout();
            flowLayoutPanelGraphicsInfo.SuspendLayout();
            tabPageProjection.SuspendLayout();
            groupBoxProjectionCenter.SuspendLayout();
            flowLayoutPanelProjectionCenter.SuspendLayout();
            groupBoxProjection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPerspective).BeginInit();
            groupBoxTransparency.SuspendLayout();
            groupBoxRenderingQuality.SuspendLayout();
            groupBoxDepthCueing.SuspendLayout();
            tabPageMisc.SuspendLayout();
            groupBoxBondedAtoms.SuspendLayout();
            groupBoxLabel.SuspendLayout();
            groupBoxAccessoryControls.SuspendLayout();
            toolStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panelClientSize.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer2
            // 
            resources.ApplyResources(splitContainer2, "splitContainer2");
            splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            resources.ApplyResources(splitContainer2.Panel1, "splitContainer2.Panel1");
            splitContainer2.Panel1.Controls.Add(textBoxCalcInformation);
            splitContainer2.Panel1.Controls.Add(label11);
            toolTip.SetToolTip(splitContainer2.Panel1, resources.GetString("splitContainer2.Panel1.ToolTip"));
            // 
            // splitContainer2.Panel2
            // 
            resources.ApplyResources(splitContainer2.Panel2, "splitContainer2.Panel2");
            splitContainer2.Panel2.Controls.Add(textBoxAtomInformation);
            splitContainer2.Panel2.Controls.Add(label13);
            toolTip.SetToolTip(splitContainer2.Panel2, resources.GetString("splitContainer2.Panel2.ToolTip"));
            toolTip.SetToolTip(splitContainer2, resources.GetString("splitContainer2.ToolTip"));
            // 
            // textBoxCalcInformation
            // 
            resources.ApplyResources(textBoxCalcInformation, "textBoxCalcInformation");
            textBoxCalcInformation.Name = "textBoxCalcInformation";
            textBoxCalcInformation.ReadOnly = true;
            toolTip.SetToolTip(textBoxCalcInformation, resources.GetString("textBoxCalcInformation.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip"));
            // 
            // textBoxAtomInformation
            // 
            resources.ApplyResources(textBoxAtomInformation, "textBoxAtomInformation");
            textBoxAtomInformation.Name = "textBoxAtomInformation";
            textBoxAtomInformation.ReadOnly = true;
            toolTip.SetToolTip(textBoxAtomInformation, resources.GetString("textBoxAtomInformation.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip"));
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(splitContainer1.Panel1, "splitContainer1.Panel1");
            splitContainer1.Panel1.Controls.Add(flowLayoutPanelLegend);
            toolTip.SetToolTip(splitContainer1.Panel1, resources.GetString("splitContainer1.Panel1.ToolTip"));
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(splitContainer1.Panel2, "splitContainer1.Panel2");
            splitContainer1.Panel2.Controls.Add(tabControl);
            toolTip.SetToolTip(splitContainer1.Panel2, resources.GetString("splitContainer1.Panel2.ToolTip"));
            toolTip.SetToolTip(splitContainer1, resources.GetString("splitContainer1.ToolTip"));
            // 
            // flowLayoutPanelLegend
            // 
            resources.ApplyResources(flowLayoutPanelLegend, "flowLayoutPanelLegend");
            flowLayoutPanelLegend.BackColor = System.Drawing.Color.White;
            flowLayoutPanelLegend.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            flowLayoutPanelLegend.Name = "flowLayoutPanelLegend";
            toolTip.SetToolTip(flowLayoutPanelLegend, resources.GetString("flowLayoutPanelLegend.ToolTip"));
            // 
            // tabControl
            // 
            resources.ApplyResources(tabControl, "tabControl");
            tabControl.Controls.Add(tabPageBounds);
            tabControl.Controls.Add(tabPageAtom);
            tabControl.Controls.Add(tabPageBond);
            tabControl.Controls.Add(tabPageUnitCell);
            tabControl.Controls.Add(tabPageLatticePlane);
            tabControl.Controls.Add(tabPageCoordinateInformation);
            tabControl.Controls.Add(tabPageInformation);
            tabControl.Controls.Add(tabPageProjection);
            tabControl.Controls.Add(tabPageMisc);
            tabControl.HotTrack = true;
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            toolTip.SetToolTip(tabControl, resources.GetString("tabControl.ToolTip"));
            tabControl.SelectedIndexChanged += tabControl_SelectedIndexChanged;
            // 
            // tabPageBounds
            // 
            resources.ApplyResources(tabPageBounds, "tabPageBounds");
            tabPageBounds.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageBounds, true);
            tabPageBounds.Controls.Add(tabControlBoundOption);
            tabPageBounds.Controls.Add(flowLayoutPanelLatticePlaneOptions);
            tabPageBounds.Controls.Add(flowLayoutPanelBoundType);
            tabPageBounds.Name = "tabPageBounds";
            toolTip.SetToolTip(tabPageBounds, resources.GetString("tabPageBounds.ToolTip"));
            // 
            // tabControlBoundOption
            // 
            resources.ApplyResources(tabControlBoundOption, "tabControlBoundOption");
            tabControlBoundOption.Controls.Add(tabPageBoundUnitcell);
            tabControlBoundOption.Controls.Add(tabPageBoundPlane);
            tabControlBoundOption.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            tabControlBoundOption.Multiline = true;
            tabControlBoundOption.Name = "tabControlBoundOption";
            tabControlBoundOption.SelectedIndex = 0;
            tabControlBoundOption.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            toolTip.SetToolTip(tabControlBoundOption, resources.GetString("tabControlBoundOption.ToolTip"));
            // 
            // tabPageBoundUnitcell
            // 
            resources.ApplyResources(tabPageBoundUnitcell, "tabPageBoundUnitcell");
            tabPageBoundUnitcell.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            captureExtender.SetCapture(tabPageBoundUnitcell, true);
            tabPageBoundUnitcell.Controls.Add(buttonSetRange2);
            tabPageBoundUnitcell.Controls.Add(buttonSetRange4);
            tabPageBoundUnitcell.Controls.Add(buttonSetRange3);
            tabPageBoundUnitcell.Controls.Add(buttonSetCenter1);
            tabPageBoundUnitcell.Controls.Add(buttonCenter2);
            tabPageBoundUnitcell.Controls.Add(buttonSetCenter3);
            tabPageBoundUnitcell.Controls.Add(buttonSetRange0);
            tabPageBoundUnitcell.Controls.Add(buttonSetRange1);
            tabPageBoundUnitcell.Controls.Add(numericBoxCRange);
            tabPageBoundUnitcell.Controls.Add(numericBoxBRange);
            tabPageBoundUnitcell.Controls.Add(numericBoxARange);
            tabPageBoundUnitcell.Controls.Add(numericBoxCCenter);
            tabPageBoundUnitcell.Controls.Add(numericBoxBCenter);
            tabPageBoundUnitcell.Controls.Add(numericBoxACenter);
            tabPageBoundUnitcell.Controls.Add(label2);
            tabPageBoundUnitcell.Controls.Add(label4);
            tabPageBoundUnitcell.Controls.Add(label3);
            tabPageBoundUnitcell.Controls.Add(label1);
            tabPageBoundUnitcell.Name = "tabPageBoundUnitcell";
            toolTip.SetToolTip(tabPageBoundUnitcell, resources.GetString("tabPageBoundUnitcell.ToolTip"));
            // 
            // buttonSetRange2
            // 
            resources.ApplyResources(buttonSetRange2, "buttonSetRange2");
            buttonSetRange2.Name = "buttonSetRange2";
            buttonSetRange2.Tag = "0.75";
            toolTip.SetToolTip(buttonSetRange2, resources.GetString("buttonSetRange2.ToolTip"));
            buttonSetRange2.UseVisualStyleBackColor = true;
            buttonSetRange2.Click += buttonSetCenterOrRange_Click;
            // 
            // buttonSetRange4
            // 
            resources.ApplyResources(buttonSetRange4, "buttonSetRange4");
            buttonSetRange4.Name = "buttonSetRange4";
            buttonSetRange4.Tag = "1.5";
            toolTip.SetToolTip(buttonSetRange4, resources.GetString("buttonSetRange4.ToolTip"));
            buttonSetRange4.UseVisualStyleBackColor = true;
            buttonSetRange4.Click += buttonSetCenterOrRange_Click;
            // 
            // buttonSetRange3
            // 
            resources.ApplyResources(buttonSetRange3, "buttonSetRange3");
            buttonSetRange3.Name = "buttonSetRange3";
            buttonSetRange3.Tag = "1";
            toolTip.SetToolTip(buttonSetRange3, resources.GetString("buttonSetRange3.ToolTip"));
            buttonSetRange3.UseVisualStyleBackColor = true;
            buttonSetRange3.Click += buttonSetCenterOrRange_Click;
            // 
            // buttonSetCenter1
            // 
            resources.ApplyResources(buttonSetCenter1, "buttonSetCenter1");
            buttonSetCenter1.Name = "buttonSetCenter1";
            buttonSetCenter1.Tag = "0";
            toolTip.SetToolTip(buttonSetCenter1, resources.GetString("buttonSetCenter1.ToolTip"));
            buttonSetCenter1.UseVisualStyleBackColor = true;
            buttonSetCenter1.Click += buttonSetCenterOrRange_Click;
            // 
            // buttonCenter2
            // 
            resources.ApplyResources(buttonCenter2, "buttonCenter2");
            buttonCenter2.Name = "buttonCenter2";
            buttonCenter2.Tag = "0.25";
            toolTip.SetToolTip(buttonCenter2, resources.GetString("buttonCenter2.ToolTip"));
            buttonCenter2.UseVisualStyleBackColor = true;
            buttonCenter2.Click += buttonSetCenterOrRange_Click;
            // 
            // buttonSetCenter3
            // 
            resources.ApplyResources(buttonSetCenter3, "buttonSetCenter3");
            buttonSetCenter3.Name = "buttonSetCenter3";
            buttonSetCenter3.Tag = "0.5";
            toolTip.SetToolTip(buttonSetCenter3, resources.GetString("buttonSetCenter3.ToolTip"));
            buttonSetCenter3.UseVisualStyleBackColor = true;
            buttonSetCenter3.Click += buttonSetCenterOrRange_Click;
            // 
            // buttonSetRange0
            // 
            resources.ApplyResources(buttonSetRange0, "buttonSetRange0");
            buttonSetRange0.Name = "buttonSetRange0";
            buttonSetRange0.Tag = "0.25";
            toolTip.SetToolTip(buttonSetRange0, resources.GetString("buttonSetRange0.ToolTip"));
            buttonSetRange0.UseVisualStyleBackColor = true;
            buttonSetRange0.Click += buttonSetCenterOrRange_Click;
            // 
            // buttonSetRange1
            // 
            resources.ApplyResources(buttonSetRange1, "buttonSetRange1");
            buttonSetRange1.Name = "buttonSetRange1";
            buttonSetRange1.Tag = "0.5";
            toolTip.SetToolTip(buttonSetRange1, resources.GetString("buttonSetRange1.ToolTip"));
            buttonSetRange1.UseVisualStyleBackColor = true;
            buttonSetRange1.Click += buttonSetCenterOrRange_Click;
            // 
            // numericBoxCRange
            // 
            resources.ApplyResources(numericBoxCRange, "numericBoxCRange");
            numericBoxCRange.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCRange.DecimalPlaces = 2;
            numericBoxCRange.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCRange.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCRange.Maximum = 10D;
            numericBoxCRange.Minimum = 0D;
            numericBoxCRange.Name = "numericBoxCRange";
            numericBoxCRange.RadianValue = 0.0087266462599716477D;
            numericBoxCRange.ShowFraction = true;
            numericBoxCRange.ShowUpDown = true;
            numericBoxCRange.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxCRange, resources.GetString("numericBoxCRange.ToolTip"));
            numericBoxCRange.UpDown_Increment = 0.25D;
            numericBoxCRange.Value = 0.5D;
            numericBoxCRange.ValueChanged += numericBoxCMax_ValueChanged;
            // 
            // numericBoxBRange
            // 
            resources.ApplyResources(numericBoxBRange, "numericBoxBRange");
            numericBoxBRange.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBRange.DecimalPlaces = 2;
            numericBoxBRange.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBRange.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBRange.Maximum = 10D;
            numericBoxBRange.Minimum = 0D;
            numericBoxBRange.Name = "numericBoxBRange";
            numericBoxBRange.RadianValue = 0.0087266462599716477D;
            numericBoxBRange.ShowFraction = true;
            numericBoxBRange.ShowUpDown = true;
            numericBoxBRange.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxBRange, resources.GetString("numericBoxBRange.ToolTip"));
            numericBoxBRange.UpDown_Increment = 0.25D;
            numericBoxBRange.Value = 0.5D;
            numericBoxBRange.ValueChanged += numericBoxCMax_ValueChanged;
            // 
            // numericBoxARange
            // 
            resources.ApplyResources(numericBoxARange, "numericBoxARange");
            numericBoxARange.BackColor = System.Drawing.SystemColors.Control;
            numericBoxARange.DecimalPlaces = 2;
            numericBoxARange.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxARange.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxARange.Maximum = 10D;
            numericBoxARange.Minimum = 0D;
            numericBoxARange.Name = "numericBoxARange";
            numericBoxARange.RadianValue = 0.0087266462599716477D;
            numericBoxARange.ShowFraction = true;
            numericBoxARange.ShowUpDown = true;
            numericBoxARange.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxARange, resources.GetString("numericBoxARange.ToolTip"));
            numericBoxARange.UpDown_Increment = 0.25D;
            numericBoxARange.Value = 0.5D;
            numericBoxARange.ValueChanged += numericBoxCMax_ValueChanged;
            // 
            // numericBoxCCenter
            // 
            resources.ApplyResources(numericBoxCCenter, "numericBoxCCenter");
            numericBoxCCenter.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCCenter.DecimalPlaces = 2;
            numericBoxCCenter.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCCenter.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCCenter.Maximum = 10D;
            numericBoxCCenter.Minimum = 0D;
            numericBoxCCenter.Name = "numericBoxCCenter";
            numericBoxCCenter.ShowFraction = true;
            numericBoxCCenter.ShowUpDown = true;
            numericBoxCCenter.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxCCenter, resources.GetString("numericBoxCCenter.ToolTip"));
            numericBoxCCenter.UpDown_Increment = 0.25D;
            numericBoxCCenter.ValueChanged += numericBoxCMax_ValueChanged;
            // 
            // numericBoxBCenter
            // 
            resources.ApplyResources(numericBoxBCenter, "numericBoxBCenter");
            numericBoxBCenter.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBCenter.DecimalPlaces = 2;
            numericBoxBCenter.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBCenter.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBCenter.Maximum = 10D;
            numericBoxBCenter.Minimum = 0D;
            numericBoxBCenter.Name = "numericBoxBCenter";
            numericBoxBCenter.ShowFraction = true;
            numericBoxBCenter.ShowUpDown = true;
            numericBoxBCenter.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxBCenter, resources.GetString("numericBoxBCenter.ToolTip"));
            numericBoxBCenter.UpDown_Increment = 0.25D;
            numericBoxBCenter.ValueChanged += numericBoxCMax_ValueChanged;
            // 
            // numericBoxACenter
            // 
            resources.ApplyResources(numericBoxACenter, "numericBoxACenter");
            numericBoxACenter.BackColor = System.Drawing.SystemColors.Control;
            numericBoxACenter.DecimalPlaces = 2;
            numericBoxACenter.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxACenter.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxACenter.Maximum = 10D;
            numericBoxACenter.Minimum = 0D;
            numericBoxACenter.Name = "numericBoxACenter";
            numericBoxACenter.ShowFraction = true;
            numericBoxACenter.ShowUpDown = true;
            numericBoxACenter.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxACenter, resources.GetString("numericBoxACenter.ToolTip"));
            numericBoxACenter.UpDown_Increment = 0.25D;
            numericBoxACenter.ValueChanged += numericBoxCMax_ValueChanged;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // tabPageBoundPlane
            // 
            resources.ApplyResources(tabPageBoundPlane, "tabPageBoundPlane");
            tabPageBoundPlane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            captureExtender.SetCapture(tabPageBoundPlane, true);
            tabPageBoundPlane.Name = "tabPageBoundPlane";
            toolTip.SetToolTip(tabPageBoundPlane, resources.GetString("tabPageBoundPlane.ToolTip"));
            // 
            // flowLayoutPanelLatticePlaneOptions
            // 
            resources.ApplyResources(flowLayoutPanelLatticePlaneOptions, "flowLayoutPanelLatticePlaneOptions");
            flowLayoutPanelLatticePlaneOptions.Controls.Add(checkBoxShowBoundPlanes);
            flowLayoutPanelLatticePlaneOptions.Controls.Add(numericBoxBoundPlanesOpacity);
            flowLayoutPanelLatticePlaneOptions.Controls.Add(checkBoxClipObjects);
            flowLayoutPanelLatticePlaneOptions.Controls.Add(checkBoxHideAllAtoms);
            flowLayoutPanelLatticePlaneOptions.Name = "flowLayoutPanelLatticePlaneOptions";
            toolTip.SetToolTip(flowLayoutPanelLatticePlaneOptions, resources.GetString("flowLayoutPanelLatticePlaneOptions.ToolTip"));
            // 
            // checkBoxShowBoundPlanes
            // 
            resources.ApplyResources(checkBoxShowBoundPlanes, "checkBoxShowBoundPlanes");
            checkBoxShowBoundPlanes.Name = "checkBoxShowBoundPlanes";
            toolTip.SetToolTip(checkBoxShowBoundPlanes, resources.GetString("checkBoxShowBoundPlanes.ToolTip"));
            checkBoxShowBoundPlanes.UseVisualStyleBackColor = true;
            checkBoxShowBoundPlanes.CheckedChanged += checkBoxShowBoundPlanes_CheckedChanged;
            // 
            // numericBoxBoundPlanesOpacity
            // 
            resources.ApplyResources(numericBoxBoundPlanesOpacity, "numericBoxBoundPlanesOpacity");
            numericBoxBoundPlanesOpacity.BackColor = System.Drawing.SystemColors.Control;
            numericBoxBoundPlanesOpacity.DecimalPlaces = 1;
            numericBoxBoundPlanesOpacity.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxBoundPlanesOpacity.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxBoundPlanesOpacity.Maximum = 1D;
            numericBoxBoundPlanesOpacity.Minimum = 0D;
            numericBoxBoundPlanesOpacity.Name = "numericBoxBoundPlanesOpacity";
            numericBoxBoundPlanesOpacity.RadianValue = 0.012217304763960306D;
            numericBoxBoundPlanesOpacity.ShowUpDown = true;
            numericBoxBoundPlanesOpacity.SkipEventDuringInput = false;
            numericBoxBoundPlanesOpacity.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxBoundPlanesOpacity, resources.GetString("numericBoxBoundPlanesOpacity.ToolTip"));
            numericBoxBoundPlanesOpacity.UpDown_Increment = 0.1D;
            numericBoxBoundPlanesOpacity.Value = 0.7D;
            numericBoxBoundPlanesOpacity.ValueChanged += checkBoxShowBoundPlanes_CheckedChanged;
            // 
            // checkBoxClipObjects
            // 
            resources.ApplyResources(checkBoxClipObjects, "checkBoxClipObjects");
            checkBoxClipObjects.Name = "checkBoxClipObjects";
            toolTip.SetToolTip(checkBoxClipObjects, resources.GetString("checkBoxClipObjects.ToolTip"));
            checkBoxClipObjects.UseVisualStyleBackColor = true;
            checkBoxClipObjects.CheckedChanged += checkBoxShowBoundPlanes_CheckedChanged;
            // 
            // checkBoxHideAllAtoms
            // 
            resources.ApplyResources(checkBoxHideAllAtoms, "checkBoxHideAllAtoms");
            checkBoxHideAllAtoms.Name = "checkBoxHideAllAtoms";
            toolTip.SetToolTip(checkBoxHideAllAtoms, resources.GetString("checkBoxHideAllAtoms.ToolTip"));
            checkBoxHideAllAtoms.UseVisualStyleBackColor = true;
            checkBoxHideAllAtoms.CheckedChanged += checkBoxShowBoundPlanes_CheckedChanged;
            // 
            // flowLayoutPanelBoundType
            // 
            resources.ApplyResources(flowLayoutPanelBoundType, "flowLayoutPanelBoundType");
            flowLayoutPanelBoundType.Controls.Add(radioButtonBoundUnitCell);
            flowLayoutPanelBoundType.Controls.Add(radioButtonBoundPlane);
            flowLayoutPanelBoundType.Name = "flowLayoutPanelBoundType";
            toolTip.SetToolTip(flowLayoutPanelBoundType, resources.GetString("flowLayoutPanelBoundType.ToolTip"));
            // 
            // radioButtonBoundUnitCell
            // 
            resources.ApplyResources(radioButtonBoundUnitCell, "radioButtonBoundUnitCell");
            radioButtonBoundUnitCell.Checked = true;
            radioButtonBoundUnitCell.Name = "radioButtonBoundUnitCell";
            radioButtonBoundUnitCell.TabStop = true;
            toolTip.SetToolTip(radioButtonBoundUnitCell, resources.GetString("radioButtonBoundUnitCell.ToolTip"));
            radioButtonBoundUnitCell.UseVisualStyleBackColor = true;
            radioButtonBoundUnitCell.CheckedChanged += radioButtonUnitCell_CheckedChanged;
            // 
            // radioButtonBoundPlane
            // 
            resources.ApplyResources(radioButtonBoundPlane, "radioButtonBoundPlane");
            radioButtonBoundPlane.Name = "radioButtonBoundPlane";
            toolTip.SetToolTip(radioButtonBoundPlane, resources.GetString("radioButtonBoundPlane.ToolTip"));
            radioButtonBoundPlane.UseVisualStyleBackColor = true;
            // 
            // tabPageAtom
            // 
            resources.ApplyResources(tabPageAtom, "tabPageAtom");
            captureExtender.SetCapture(tabPageAtom, true);
            tabPageAtom.Controls.Add(labelMessage);
            tabPageAtom.Name = "tabPageAtom";
            toolTip.SetToolTip(tabPageAtom, resources.GetString("tabPageAtom.ToolTip"));
            // 
            // labelMessage
            // 
            resources.ApplyResources(labelMessage, "labelMessage");
            labelMessage.ForeColor = System.Drawing.Color.Red;
            labelMessage.Name = "labelMessage";
            toolTip.SetToolTip(labelMessage, resources.GetString("labelMessage.ToolTip"));
            // 
            // tabPageBond
            // 
            resources.ApplyResources(tabPageBond, "tabPageBond");
            captureExtender.SetCapture(tabPageBond, true);
            tabPageBond.Name = "tabPageBond";
            toolTip.SetToolTip(tabPageBond, resources.GetString("tabPageBond.ToolTip"));
            // 
            // tabPageUnitCell
            // 
            resources.ApplyResources(tabPageUnitCell, "tabPageUnitCell");
            tabPageUnitCell.BackColor = System.Drawing.Color.Transparent;
            captureExtender.SetCapture(tabPageUnitCell, true);
            tabPageUnitCell.Controls.Add(checkBoxUnitCell);
            tabPageUnitCell.Controls.Add(groupBoxShowUnitCell);
            tabPageUnitCell.Name = "tabPageUnitCell";
            toolTip.SetToolTip(tabPageUnitCell, resources.GetString("tabPageUnitCell.ToolTip"));
            tabPageUnitCell.UseVisualStyleBackColor = true;
            // 
            // checkBoxUnitCell
            // 
            resources.ApplyResources(checkBoxUnitCell, "checkBoxUnitCell");
            checkBoxUnitCell.BackColor = System.Drawing.SystemColors.Control;
            checkBoxUnitCell.Checked = true;
            checkBoxUnitCell.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxUnitCell.Name = "checkBoxUnitCell";
            toolTip.SetToolTip(checkBoxUnitCell, resources.GetString("checkBoxUnitCell.ToolTip"));
            checkBoxUnitCell.UseVisualStyleBackColor = false;
            checkBoxUnitCell.CheckedChanged += unitCell_PropertyChanged;
            // 
            // groupBoxShowUnitCell
            // 
            resources.ApplyResources(groupBoxShowUnitCell, "groupBoxShowUnitCell");
            groupBoxShowUnitCell.BackColor = System.Drawing.SystemColors.Control;
            groupBoxShowUnitCell.Controls.Add(flowLayoutPanelCellPlaneColors);
            groupBoxShowUnitCell.Controls.Add(flowLayoutPanelCellEdgeColors);
            groupBoxShowUnitCell.Controls.Add(numericBoxCellTranslationC);
            groupBoxShowUnitCell.Controls.Add(numericBoxCellTranslationB);
            groupBoxShowUnitCell.Controls.Add(numericBoxCellTranslationA);
            groupBoxShowUnitCell.Controls.Add(checkBoxShowSubCell);
            groupBoxShowUnitCell.Controls.Add(checkBoxCellShowEdge);
            groupBoxShowUnitCell.Controls.Add(numericUpDownSubCellB);
            groupBoxShowUnitCell.Controls.Add(label10);
            groupBoxShowUnitCell.Controls.Add(checkBoxCellShowPlane);
            groupBoxShowUnitCell.Controls.Add(numericUpDownSubCellC);
            groupBoxShowUnitCell.Controls.Add(numericUpDownSubCellA);
            groupBoxShowUnitCell.Controls.Add(label17);
            groupBoxShowUnitCell.Controls.Add(label16);
            groupBoxShowUnitCell.Controls.Add(label12);
            groupBoxShowUnitCell.Name = "groupBoxShowUnitCell";
            groupBoxShowUnitCell.TabStop = false;
            toolTip.SetToolTip(groupBoxShowUnitCell, resources.GetString("groupBoxShowUnitCell.ToolTip"));
            // 
            // flowLayoutPanelCellPlaneColors
            // 
            resources.ApplyResources(flowLayoutPanelCellPlaneColors, "flowLayoutPanelCellPlaneColors");
            flowLayoutPanelCellPlaneColors.Controls.Add(radioButtonCellPlaneColorAll);
            flowLayoutPanelCellPlaneColors.Controls.Add(colorControlCellPlane);
            flowLayoutPanelCellPlaneColors.Controls.Add(radioButtonCellPlaneColorEach);
            flowLayoutPanelCellPlaneColors.Controls.Add(colorControlCellPlaneA);
            flowLayoutPanelCellPlaneColors.Controls.Add(colorControlCellPlaneB);
            flowLayoutPanelCellPlaneColors.Controls.Add(colorControlCellPlaneC);
            flowLayoutPanelCellPlaneColors.Controls.Add(numericBoxCellPlaneAlpha);
            flowLayoutPanelCellPlaneColors.Name = "flowLayoutPanelCellPlaneColors";
            toolTip.SetToolTip(flowLayoutPanelCellPlaneColors, resources.GetString("flowLayoutPanelCellPlaneColors.ToolTip"));
            // 
            // radioButtonCellPlaneColorAll
            // 
            resources.ApplyResources(radioButtonCellPlaneColorAll, "radioButtonCellPlaneColorAll");
            radioButtonCellPlaneColorAll.Checked = true;
            radioButtonCellPlaneColorAll.Name = "radioButtonCellPlaneColorAll";
            radioButtonCellPlaneColorAll.TabStop = true;
            toolTip.SetToolTip(radioButtonCellPlaneColorAll, resources.GetString("radioButtonCellPlaneColorAll.ToolTip"));
            radioButtonCellPlaneColorAll.UseVisualStyleBackColor = true;
            radioButtonCellPlaneColorAll.CheckedChanged += unitCell_PropertyChanged;
            // 
            // colorControlCellPlane
            // 
            resources.ApplyResources(colorControlCellPlane, "colorControlCellPlane");
            colorControlCellPlane.Argb = -4144960;
            colorControlCellPlane.BackColor = System.Drawing.SystemColors.Control;
            colorControlCellPlane.Blue = 192;
            colorControlCellPlane.BlueF = 0.7529412F;
            colorControlCellPlane.BoxSize = new System.Drawing.Size(20, 20);
            colorControlCellPlane.Color = System.Drawing.Color.FromArgb(192, 192, 192);
            colorControlCellPlane.Green = 192;
            colorControlCellPlane.GreenF = 0.7529412F;
            colorControlCellPlane.Name = "colorControlCellPlane";
            colorControlCellPlane.Red = 192;
            colorControlCellPlane.RedF = 0.7529412F;
            toolTip.SetToolTip(colorControlCellPlane, resources.GetString("colorControlCellPlane.ToolTip1"));
            colorControlCellPlane.ColorChanged += unitCell_PropertyChanged;
            // 
            // radioButtonCellPlaneColorEach
            // 
            resources.ApplyResources(radioButtonCellPlaneColorEach, "radioButtonCellPlaneColorEach");
            radioButtonCellPlaneColorEach.Name = "radioButtonCellPlaneColorEach";
            toolTip.SetToolTip(radioButtonCellPlaneColorEach, resources.GetString("radioButtonCellPlaneColorEach.ToolTip"));
            radioButtonCellPlaneColorEach.UseVisualStyleBackColor = true;
            radioButtonCellPlaneColorEach.CheckedChanged += unitCell_PropertyChanged;
            // 
            // colorControlCellPlaneA
            // 
            resources.ApplyResources(colorControlCellPlaneA, "colorControlCellPlaneA");
            colorControlCellPlaneA.Argb = -65536;
            colorControlCellPlaneA.BackColor = System.Drawing.SystemColors.Control;
            colorControlCellPlaneA.Blue = 0;
            colorControlCellPlaneA.BlueF = 0F;
            colorControlCellPlaneA.BoxSize = new System.Drawing.Size(20, 20);
            colorControlCellPlaneA.Color = System.Drawing.Color.FromArgb(255, 0, 0);
            colorControlCellPlaneA.Green = 0;
            colorControlCellPlaneA.GreenF = 0F;
            colorControlCellPlaneA.Name = "colorControlCellPlaneA";
            colorControlCellPlaneA.Red = 255;
            colorControlCellPlaneA.RedF = 1F;
            toolTip.SetToolTip(colorControlCellPlaneA, resources.GetString("colorControlCellPlaneA.ToolTip1"));
            colorControlCellPlaneA.ColorChanged += unitCell_PropertyChanged;
            // 
            // colorControlCellPlaneB
            // 
            resources.ApplyResources(colorControlCellPlaneB, "colorControlCellPlaneB");
            colorControlCellPlaneB.Argb = -16744448;
            colorControlCellPlaneB.BackColor = System.Drawing.SystemColors.Control;
            colorControlCellPlaneB.Blue = 0;
            colorControlCellPlaneB.BlueF = 0F;
            colorControlCellPlaneB.BoxSize = new System.Drawing.Size(20, 20);
            colorControlCellPlaneB.Color = System.Drawing.Color.FromArgb(0, 128, 0);
            colorControlCellPlaneB.Green = 128;
            colorControlCellPlaneB.GreenF = 0.5019608F;
            colorControlCellPlaneB.Name = "colorControlCellPlaneB";
            colorControlCellPlaneB.Red = 0;
            colorControlCellPlaneB.RedF = 0F;
            toolTip.SetToolTip(colorControlCellPlaneB, resources.GetString("colorControlCellPlaneB.ToolTip1"));
            colorControlCellPlaneB.ColorChanged += unitCell_PropertyChanged;
            // 
            // colorControlCellPlaneC
            // 
            resources.ApplyResources(colorControlCellPlaneC, "colorControlCellPlaneC");
            colorControlCellPlaneC.Argb = -16776961;
            colorControlCellPlaneC.BackColor = System.Drawing.SystemColors.Control;
            colorControlCellPlaneC.Blue = 255;
            colorControlCellPlaneC.BlueF = 1F;
            colorControlCellPlaneC.BoxSize = new System.Drawing.Size(20, 20);
            colorControlCellPlaneC.Color = System.Drawing.Color.FromArgb(0, 0, 255);
            colorControlCellPlaneC.Green = 0;
            colorControlCellPlaneC.GreenF = 0F;
            colorControlCellPlaneC.Name = "colorControlCellPlaneC";
            colorControlCellPlaneC.Red = 0;
            colorControlCellPlaneC.RedF = 0F;
            toolTip.SetToolTip(colorControlCellPlaneC, resources.GetString("colorControlCellPlaneC.ToolTip1"));
            colorControlCellPlaneC.ColorChanged += unitCell_PropertyChanged;
            // 
            // numericBoxCellPlaneAlpha
            // 
            resources.ApplyResources(numericBoxCellPlaneAlpha, "numericBoxCellPlaneAlpha");
            numericBoxCellPlaneAlpha.BackColor = System.Drawing.Color.Transparent;
            numericBoxCellPlaneAlpha.DecimalPlaces = 1;
            numericBoxCellPlaneAlpha.Maximum = 1D;
            numericBoxCellPlaneAlpha.Minimum = 0D;
            numericBoxCellPlaneAlpha.Name = "numericBoxCellPlaneAlpha";
            numericBoxCellPlaneAlpha.RadianValue = 0.0052359877559829881D;
            numericBoxCellPlaneAlpha.ShowUpDown = true;
            numericBoxCellPlaneAlpha.SkipEventDuringInput = false;
            numericBoxCellPlaneAlpha.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxCellPlaneAlpha, resources.GetString("numericBoxCellPlaneAlpha.ToolTip"));
            numericBoxCellPlaneAlpha.UpDown_Increment = 0.1D;
            numericBoxCellPlaneAlpha.Value = 0.3D;
            numericBoxCellPlaneAlpha.ValueChanged += unitCell_PropertyChanged;
            // 
            // flowLayoutPanelCellEdgeColors
            // 
            resources.ApplyResources(flowLayoutPanelCellEdgeColors, "flowLayoutPanelCellEdgeColors");
            flowLayoutPanelCellEdgeColors.Controls.Add(radioButtonCellEdgeColorAll);
            flowLayoutPanelCellEdgeColors.Controls.Add(colorControlCellEdge);
            flowLayoutPanelCellEdgeColors.Controls.Add(radioButtonCellEdgeColorEach);
            flowLayoutPanelCellEdgeColors.Controls.Add(colorControlCellEdgeA);
            flowLayoutPanelCellEdgeColors.Controls.Add(colorControlCellEdgeB);
            flowLayoutPanelCellEdgeColors.Controls.Add(colorControlCellEdgeC);
            flowLayoutPanelCellEdgeColors.Controls.Add(label8);
            flowLayoutPanelCellEdgeColors.Controls.Add(trackBarCellEdgeWidth);
            flowLayoutPanelCellEdgeColors.Name = "flowLayoutPanelCellEdgeColors";
            toolTip.SetToolTip(flowLayoutPanelCellEdgeColors, resources.GetString("flowLayoutPanelCellEdgeColors.ToolTip"));
            // 
            // radioButtonCellEdgeColorAll
            // 
            resources.ApplyResources(radioButtonCellEdgeColorAll, "radioButtonCellEdgeColorAll");
            radioButtonCellEdgeColorAll.Name = "radioButtonCellEdgeColorAll";
            toolTip.SetToolTip(radioButtonCellEdgeColorAll, resources.GetString("radioButtonCellEdgeColorAll.ToolTip"));
            radioButtonCellEdgeColorAll.UseVisualStyleBackColor = true;
            radioButtonCellEdgeColorAll.CheckedChanged += unitCell_PropertyChanged;
            // 
            // colorControlCellEdge
            // 
            resources.ApplyResources(colorControlCellEdge, "colorControlCellEdge");
            colorControlCellEdge.Argb = -8355712;
            colorControlCellEdge.BackColor = System.Drawing.SystemColors.Control;
            colorControlCellEdge.Blue = 128;
            colorControlCellEdge.BlueF = 0.5019608F;
            colorControlCellEdge.BoxSize = new System.Drawing.Size(20, 20);
            colorControlCellEdge.Color = System.Drawing.Color.FromArgb(128, 128, 128);
            colorControlCellEdge.Green = 128;
            colorControlCellEdge.GreenF = 0.5019608F;
            colorControlCellEdge.Name = "colorControlCellEdge";
            colorControlCellEdge.Red = 128;
            colorControlCellEdge.RedF = 0.5019608F;
            toolTip.SetToolTip(colorControlCellEdge, resources.GetString("colorControlCellEdge.ToolTip1"));
            colorControlCellEdge.ColorChanged += unitCell_PropertyChanged;
            // 
            // radioButtonCellEdgeColorEach
            // 
            resources.ApplyResources(radioButtonCellEdgeColorEach, "radioButtonCellEdgeColorEach");
            radioButtonCellEdgeColorEach.Checked = true;
            radioButtonCellEdgeColorEach.Name = "radioButtonCellEdgeColorEach";
            radioButtonCellEdgeColorEach.TabStop = true;
            toolTip.SetToolTip(radioButtonCellEdgeColorEach, resources.GetString("radioButtonCellEdgeColorEach.ToolTip"));
            radioButtonCellEdgeColorEach.UseVisualStyleBackColor = true;
            radioButtonCellEdgeColorEach.CheckedChanged += unitCell_PropertyChanged;
            // 
            // colorControlCellEdgeA
            // 
            resources.ApplyResources(colorControlCellEdgeA, "colorControlCellEdgeA");
            colorControlCellEdgeA.Argb = -65536;
            colorControlCellEdgeA.BackColor = System.Drawing.SystemColors.Control;
            colorControlCellEdgeA.Blue = 0;
            colorControlCellEdgeA.BlueF = 0F;
            colorControlCellEdgeA.BoxSize = new System.Drawing.Size(20, 20);
            colorControlCellEdgeA.Color = System.Drawing.Color.FromArgb(255, 0, 0);
            colorControlCellEdgeA.Green = 0;
            colorControlCellEdgeA.GreenF = 0F;
            colorControlCellEdgeA.Name = "colorControlCellEdgeA";
            colorControlCellEdgeA.Red = 255;
            colorControlCellEdgeA.RedF = 1F;
            toolTip.SetToolTip(colorControlCellEdgeA, resources.GetString("colorControlCellEdgeA.ToolTip1"));
            colorControlCellEdgeA.ColorChanged += unitCell_PropertyChanged;
            // 
            // colorControlCellEdgeB
            // 
            resources.ApplyResources(colorControlCellEdgeB, "colorControlCellEdgeB");
            colorControlCellEdgeB.Argb = -16744448;
            colorControlCellEdgeB.BackColor = System.Drawing.SystemColors.Control;
            colorControlCellEdgeB.Blue = 0;
            colorControlCellEdgeB.BlueF = 0F;
            colorControlCellEdgeB.BoxSize = new System.Drawing.Size(20, 20);
            colorControlCellEdgeB.Color = System.Drawing.Color.FromArgb(0, 128, 0);
            colorControlCellEdgeB.Green = 128;
            colorControlCellEdgeB.GreenF = 0.5019608F;
            colorControlCellEdgeB.Name = "colorControlCellEdgeB";
            colorControlCellEdgeB.Red = 0;
            colorControlCellEdgeB.RedF = 0F;
            toolTip.SetToolTip(colorControlCellEdgeB, resources.GetString("colorControlCellEdgeB.ToolTip1"));
            colorControlCellEdgeB.ColorChanged += unitCell_PropertyChanged;
            // 
            // colorControlCellEdgeC
            // 
            resources.ApplyResources(colorControlCellEdgeC, "colorControlCellEdgeC");
            colorControlCellEdgeC.Argb = -16776961;
            colorControlCellEdgeC.BackColor = System.Drawing.SystemColors.Control;
            colorControlCellEdgeC.Blue = 255;
            colorControlCellEdgeC.BlueF = 1F;
            colorControlCellEdgeC.BoxSize = new System.Drawing.Size(20, 20);
            colorControlCellEdgeC.Color = System.Drawing.Color.FromArgb(0, 0, 255);
            colorControlCellEdgeC.Green = 0;
            colorControlCellEdgeC.GreenF = 0F;
            colorControlCellEdgeC.Name = "colorControlCellEdgeC";
            colorControlCellEdgeC.Red = 0;
            colorControlCellEdgeC.RedF = 0F;
            toolTip.SetToolTip(colorControlCellEdgeC, resources.GetString("colorControlCellEdgeC.ToolTip1"));
            colorControlCellEdgeC.ColorChanged += unitCell_PropertyChanged;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            toolTip.SetToolTip(label8, resources.GetString("label8.ToolTip"));
            // 
            // trackBarCellEdgeWidth
            // 
            resources.ApplyResources(trackBarCellEdgeWidth, "trackBarCellEdgeWidth");
            trackBarCellEdgeWidth.LargeChange = 1;
            trackBarCellEdgeWidth.Maximum = 5;
            trackBarCellEdgeWidth.Minimum = 1;
            trackBarCellEdgeWidth.Name = "trackBarCellEdgeWidth";
            toolTip.SetToolTip(trackBarCellEdgeWidth, resources.GetString("trackBarCellEdgeWidth.ToolTip"));
            trackBarCellEdgeWidth.Value = 1;
            trackBarCellEdgeWidth.ValueChanged += unitCell_PropertyChanged;
            // 
            // numericBoxCellTranslationC
            // 
            resources.ApplyResources(numericBoxCellTranslationC, "numericBoxCellTranslationC");
            numericBoxCellTranslationC.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCellTranslationC.DecimalPlaces = 2;
            numericBoxCellTranslationC.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCellTranslationC.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCellTranslationC.Maximum = 10D;
            numericBoxCellTranslationC.Minimum = -10D;
            numericBoxCellTranslationC.Name = "numericBoxCellTranslationC";
            numericBoxCellTranslationC.ShowUpDown = true;
            numericBoxCellTranslationC.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxCellTranslationC, resources.GetString("numericBoxCellTranslationC.ToolTip"));
            numericBoxCellTranslationC.UpDown_Increment = 0.1D;
            numericBoxCellTranslationC.ValueChanged += unitCell_PropertyChanged;
            // 
            // numericBoxCellTranslationB
            // 
            resources.ApplyResources(numericBoxCellTranslationB, "numericBoxCellTranslationB");
            numericBoxCellTranslationB.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCellTranslationB.DecimalPlaces = 2;
            numericBoxCellTranslationB.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCellTranslationB.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCellTranslationB.Maximum = 10D;
            numericBoxCellTranslationB.Minimum = -10D;
            numericBoxCellTranslationB.Name = "numericBoxCellTranslationB";
            numericBoxCellTranslationB.ShowUpDown = true;
            numericBoxCellTranslationB.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxCellTranslationB, resources.GetString("numericBoxCellTranslationB.ToolTip"));
            numericBoxCellTranslationB.UpDown_Increment = 0.1D;
            numericBoxCellTranslationB.ValueChanged += unitCell_PropertyChanged;
            // 
            // numericBoxCellTranslationA
            // 
            resources.ApplyResources(numericBoxCellTranslationA, "numericBoxCellTranslationA");
            numericBoxCellTranslationA.BackColor = System.Drawing.SystemColors.Control;
            numericBoxCellTranslationA.DecimalPlaces = 2;
            numericBoxCellTranslationA.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxCellTranslationA.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxCellTranslationA.Maximum = 10D;
            numericBoxCellTranslationA.Minimum = -10D;
            numericBoxCellTranslationA.Name = "numericBoxCellTranslationA";
            numericBoxCellTranslationA.ShowUpDown = true;
            numericBoxCellTranslationA.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxCellTranslationA, resources.GetString("numericBoxCellTranslationA.ToolTip"));
            numericBoxCellTranslationA.UpDown_Increment = 0.1D;
            numericBoxCellTranslationA.ValueChanged += unitCell_PropertyChanged;
            // 
            // checkBoxShowSubCell
            // 
            resources.ApplyResources(checkBoxShowSubCell, "checkBoxShowSubCell");
            checkBoxShowSubCell.BackColor = System.Drawing.SystemColors.Control;
            checkBoxShowSubCell.Name = "checkBoxShowSubCell";
            toolTip.SetToolTip(checkBoxShowSubCell, resources.GetString("checkBoxShowSubCell.ToolTip"));
            checkBoxShowSubCell.UseVisualStyleBackColor = false;
            checkBoxShowSubCell.CheckedChanged += unitCell_PropertyChanged;
            // 
            // checkBoxCellShowEdge
            // 
            resources.ApplyResources(checkBoxCellShowEdge, "checkBoxCellShowEdge");
            checkBoxCellShowEdge.Checked = true;
            checkBoxCellShowEdge.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxCellShowEdge.Name = "checkBoxCellShowEdge";
            toolTip.SetToolTip(checkBoxCellShowEdge, resources.GetString("checkBoxCellShowEdge.ToolTip"));
            checkBoxCellShowEdge.UseVisualStyleBackColor = true;
            checkBoxCellShowEdge.CheckedChanged += unitCell_PropertyChanged;
            // 
            // numericUpDownSubCellB
            // 
            resources.ApplyResources(numericUpDownSubCellB, "numericUpDownSubCellB");
            numericUpDownSubCellB.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownSubCellB.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownSubCellB.Name = "numericUpDownSubCellB";
            toolTip.SetToolTip(numericUpDownSubCellB, resources.GetString("numericUpDownSubCellB.ToolTip"));
            numericUpDownSubCellB.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownSubCellB.ValueChanged += unitCell_PropertyChanged;
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            toolTip.SetToolTip(label10, resources.GetString("label10.ToolTip"));
            // 
            // checkBoxCellShowPlane
            // 
            resources.ApplyResources(checkBoxCellShowPlane, "checkBoxCellShowPlane");
            checkBoxCellShowPlane.Name = "checkBoxCellShowPlane";
            toolTip.SetToolTip(checkBoxCellShowPlane, resources.GetString("checkBoxCellShowPlane.ToolTip"));
            checkBoxCellShowPlane.UseVisualStyleBackColor = true;
            checkBoxCellShowPlane.CheckedChanged += unitCell_PropertyChanged;
            // 
            // numericUpDownSubCellC
            // 
            resources.ApplyResources(numericUpDownSubCellC, "numericUpDownSubCellC");
            numericUpDownSubCellC.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownSubCellC.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownSubCellC.Name = "numericUpDownSubCellC";
            toolTip.SetToolTip(numericUpDownSubCellC, resources.GetString("numericUpDownSubCellC.ToolTip"));
            numericUpDownSubCellC.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownSubCellC.ValueChanged += unitCell_PropertyChanged;
            // 
            // numericUpDownSubCellA
            // 
            resources.ApplyResources(numericUpDownSubCellA, "numericUpDownSubCellA");
            numericUpDownSubCellA.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownSubCellA.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownSubCellA.Name = "numericUpDownSubCellA";
            toolTip.SetToolTip(numericUpDownSubCellA, resources.GetString("numericUpDownSubCellA.ToolTip"));
            numericUpDownSubCellA.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownSubCellA.ValueChanged += unitCell_PropertyChanged;
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            toolTip.SetToolTip(label17, resources.GetString("label17.ToolTip"));
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            label16.Name = "label16";
            toolTip.SetToolTip(label16, resources.GetString("label16.ToolTip"));
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            label12.Name = "label12";
            toolTip.SetToolTip(label12, resources.GetString("label12.ToolTip"));
            // 
            // tabPageLatticePlane
            // 
            resources.ApplyResources(tabPageLatticePlane, "tabPageLatticePlane");
            tabPageLatticePlane.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageLatticePlane, true);
            tabPageLatticePlane.Controls.Add(flowLayoutPanelLatticePlaneOpacity);
            tabPageLatticePlane.Name = "tabPageLatticePlane";
            toolTip.SetToolTip(tabPageLatticePlane, resources.GetString("tabPageLatticePlane.ToolTip"));
            // 
            // flowLayoutPanelLatticePlaneOpacity
            // 
            resources.ApplyResources(flowLayoutPanelLatticePlaneOpacity, "flowLayoutPanelLatticePlaneOpacity");
            flowLayoutPanelLatticePlaneOpacity.Controls.Add(numericBoxLatticePlaneOpacity);
            flowLayoutPanelLatticePlaneOpacity.Name = "flowLayoutPanelLatticePlaneOpacity";
            toolTip.SetToolTip(flowLayoutPanelLatticePlaneOpacity, resources.GetString("flowLayoutPanelLatticePlaneOpacity.ToolTip"));
            // 
            // numericBoxLatticePlaneOpacity
            // 
            resources.ApplyResources(numericBoxLatticePlaneOpacity, "numericBoxLatticePlaneOpacity");
            numericBoxLatticePlaneOpacity.BackColor = System.Drawing.Color.Transparent;
            numericBoxLatticePlaneOpacity.Maximum = 1D;
            numericBoxLatticePlaneOpacity.Minimum = 0D;
            numericBoxLatticePlaneOpacity.Name = "numericBoxLatticePlaneOpacity";
            numericBoxLatticePlaneOpacity.RadianValue = 0.0087266462599716477D;
            numericBoxLatticePlaneOpacity.ShowUpDown = true;
            numericBoxLatticePlaneOpacity.SkipEventDuringInput = false;
            numericBoxLatticePlaneOpacity.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxLatticePlaneOpacity, resources.GetString("numericBoxLatticePlaneOpacity.ToolTip"));
            numericBoxLatticePlaneOpacity.UpDown_Increment = 0.1D;
            numericBoxLatticePlaneOpacity.Value = 0.5D;
            numericBoxLatticePlaneOpacity.ValueChanged += numericBoxLatticePlaneOpacity_ValueChanged;
            // 
            // tabPageCoordinateInformation
            // 
            resources.ApplyResources(tabPageCoordinateInformation, "tabPageCoordinateInformation");
            captureExtender.SetCapture(tabPageCoordinateInformation, true);
            tabPageCoordinateInformation.Controls.Add(atomCoordinateTable1);
            tabPageCoordinateInformation.Name = "tabPageCoordinateInformation";
            toolTip.SetToolTip(tabPageCoordinateInformation, resources.GetString("tabPageCoordinateInformation.ToolTip"));
            // 
            // atomCoordinateTable1
            // 
            resources.ApplyResources(atomCoordinateTable1, "atomCoordinateTable1");
            atomCoordinateTable1.Name = "atomCoordinateTable1";
            toolTip.SetToolTip(atomCoordinateTable1, resources.GetString("atomCoordinateTable1.ToolTip"));
            // 
            // tabPageInformation
            // 
            resources.ApplyResources(tabPageInformation, "tabPageInformation");
            captureExtender.SetCapture(tabPageInformation, true);
            tabPageInformation.Controls.Add(splitContainer2);
            tabPageInformation.Controls.Add(flowLayoutPanelGraphicsInfo);
            tabPageInformation.Name = "tabPageInformation";
            toolTip.SetToolTip(tabPageInformation, resources.GetString("tabPageInformation.ToolTip"));
            // 
            // flowLayoutPanelGraphicsInfo
            // 
            resources.ApplyResources(flowLayoutPanelGraphicsInfo, "flowLayoutPanelGraphicsInfo");
            flowLayoutPanelGraphicsInfo.Controls.Add(labelGraphicsCard);
            flowLayoutPanelGraphicsInfo.Controls.Add(labelGraphicsDriver);
            flowLayoutPanelGraphicsInfo.Controls.Add(labelOpenGLversion);
            flowLayoutPanelGraphicsInfo.Name = "flowLayoutPanelGraphicsInfo";
            toolTip.SetToolTip(flowLayoutPanelGraphicsInfo, resources.GetString("flowLayoutPanelGraphicsInfo.ToolTip"));
            // 
            // labelGraphicsCard
            // 
            resources.ApplyResources(labelGraphicsCard, "labelGraphicsCard");
            labelGraphicsCard.Name = "labelGraphicsCard";
            toolTip.SetToolTip(labelGraphicsCard, resources.GetString("labelGraphicsCard.ToolTip"));
            // 
            // labelGraphicsDriver
            // 
            resources.ApplyResources(labelGraphicsDriver, "labelGraphicsDriver");
            labelGraphicsDriver.Name = "labelGraphicsDriver";
            toolTip.SetToolTip(labelGraphicsDriver, resources.GetString("labelGraphicsDriver.ToolTip"));
            // 
            // labelOpenGLversion
            // 
            resources.ApplyResources(labelOpenGLversion, "labelOpenGLversion");
            labelOpenGLversion.Name = "labelOpenGLversion";
            toolTip.SetToolTip(labelOpenGLversion, resources.GetString("labelOpenGLversion.ToolTip"));
            // 
            // tabPageProjection
            // 
            resources.ApplyResources(tabPageProjection, "tabPageProjection");
            tabPageProjection.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageProjection, true);
            tabPageProjection.Controls.Add(groupBoxProjectionCenter);
            tabPageProjection.Controls.Add(groupBoxProjection);
            tabPageProjection.Controls.Add(groupBoxTransparency);
            tabPageProjection.Controls.Add(checkBoxDepthFadingOut);
            tabPageProjection.Controls.Add(groupBoxRenderingQuality);
            tabPageProjection.Controls.Add(groupBoxDepthCueing);
            tabPageProjection.Name = "tabPageProjection";
            toolTip.SetToolTip(tabPageProjection, resources.GetString("tabPageProjection.ToolTip"));
            // 
            // groupBoxProjectionCenter
            // 
            resources.ApplyResources(groupBoxProjectionCenter, "groupBoxProjectionCenter");
            groupBoxProjectionCenter.Controls.Add(flowLayoutPanelProjectionCenter);
            groupBoxProjectionCenter.Controls.Add(label9);
            groupBoxProjectionCenter.Controls.Add(radioButtonProjectionCenterCustom);
            groupBoxProjectionCenter.Controls.Add(radioButtonProjectionCenter1);
            groupBoxProjectionCenter.Controls.Add(radioButtonProjectionCenter2);
            groupBoxProjectionCenter.Name = "groupBoxProjectionCenter";
            groupBoxProjectionCenter.TabStop = false;
            toolTip.SetToolTip(groupBoxProjectionCenter, resources.GetString("groupBoxProjectionCenter.ToolTip"));
            // 
            // flowLayoutPanelProjectionCenter
            // 
            resources.ApplyResources(flowLayoutPanelProjectionCenter, "flowLayoutPanelProjectionCenter");
            flowLayoutPanelProjectionCenter.Controls.Add(numericBoxProjectionCenterX);
            flowLayoutPanelProjectionCenter.Controls.Add(numericBoxProjectionCenterY);
            flowLayoutPanelProjectionCenter.Controls.Add(numericBoxProjectionCenterZ);
            flowLayoutPanelProjectionCenter.Name = "flowLayoutPanelProjectionCenter";
            toolTip.SetToolTip(flowLayoutPanelProjectionCenter, resources.GetString("flowLayoutPanelProjectionCenter.ToolTip"));
            // 
            // numericBoxProjectionCenterX
            // 
            resources.ApplyResources(numericBoxProjectionCenterX, "numericBoxProjectionCenterX");
            numericBoxProjectionCenterX.BackColor = System.Drawing.SystemColors.Control;
            numericBoxProjectionCenterX.DecimalPlaces = 2;
            numericBoxProjectionCenterX.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxProjectionCenterX.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxProjectionCenterX.Maximum = 1D;
            numericBoxProjectionCenterX.Minimum = -1D;
            numericBoxProjectionCenterX.Name = "numericBoxProjectionCenterX";
            numericBoxProjectionCenterX.RadianValue = 0.0087266462599716477D;
            numericBoxProjectionCenterX.ShowUpDown = true;
            numericBoxProjectionCenterX.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxProjectionCenterX, resources.GetString("numericBoxProjectionCenterX.ToolTip"));
            numericBoxProjectionCenterX.UpDown_Increment = 0.1D;
            numericBoxProjectionCenterX.Value = 0.5D;
            numericBoxProjectionCenterX.ValueChanged += numericBoxProjectionCenterX_ValueChanged;
            // 
            // numericBoxProjectionCenterY
            // 
            resources.ApplyResources(numericBoxProjectionCenterY, "numericBoxProjectionCenterY");
            numericBoxProjectionCenterY.BackColor = System.Drawing.SystemColors.Control;
            numericBoxProjectionCenterY.DecimalPlaces = 2;
            numericBoxProjectionCenterY.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxProjectionCenterY.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxProjectionCenterY.Maximum = 1D;
            numericBoxProjectionCenterY.Minimum = -1D;
            numericBoxProjectionCenterY.Name = "numericBoxProjectionCenterY";
            numericBoxProjectionCenterY.RadianValue = 0.0087266462599716477D;
            numericBoxProjectionCenterY.ShowUpDown = true;
            numericBoxProjectionCenterY.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxProjectionCenterY, resources.GetString("numericBoxProjectionCenterY.ToolTip"));
            numericBoxProjectionCenterY.UpDown_Increment = 0.1D;
            numericBoxProjectionCenterY.Value = 0.5D;
            numericBoxProjectionCenterY.ValueChanged += numericBoxProjectionCenterX_ValueChanged;
            // 
            // numericBoxProjectionCenterZ
            // 
            resources.ApplyResources(numericBoxProjectionCenterZ, "numericBoxProjectionCenterZ");
            numericBoxProjectionCenterZ.BackColor = System.Drawing.SystemColors.Control;
            numericBoxProjectionCenterZ.DecimalPlaces = 2;
            numericBoxProjectionCenterZ.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxProjectionCenterZ.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxProjectionCenterZ.Maximum = 1D;
            numericBoxProjectionCenterZ.Minimum = -1D;
            numericBoxProjectionCenterZ.Name = "numericBoxProjectionCenterZ";
            numericBoxProjectionCenterZ.RadianValue = 0.0087266462599716477D;
            numericBoxProjectionCenterZ.ShowUpDown = true;
            numericBoxProjectionCenterZ.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxProjectionCenterZ, resources.GetString("numericBoxProjectionCenterZ.ToolTip"));
            numericBoxProjectionCenterZ.UpDown_Increment = 0.1D;
            numericBoxProjectionCenterZ.Value = 0.5D;
            numericBoxProjectionCenterZ.ValueChanged += numericBoxProjectionCenterX_ValueChanged;
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            toolTip.SetToolTip(label9, resources.GetString("label9.ToolTip"));
            // 
            // radioButtonProjectionCenterCustom
            // 
            resources.ApplyResources(radioButtonProjectionCenterCustom, "radioButtonProjectionCenterCustom");
            radioButtonProjectionCenterCustom.Name = "radioButtonProjectionCenterCustom";
            toolTip.SetToolTip(radioButtonProjectionCenterCustom, resources.GetString("radioButtonProjectionCenterCustom.ToolTip"));
            radioButtonProjectionCenterCustom.UseVisualStyleBackColor = true;
            radioButtonProjectionCenterCustom.CheckedChanged += radioButtonScreenCenter1_CheckedChanged;
            // 
            // radioButtonProjectionCenter1
            // 
            resources.ApplyResources(radioButtonProjectionCenter1, "radioButtonProjectionCenter1");
            radioButtonProjectionCenter1.Name = "radioButtonProjectionCenter1";
            toolTip.SetToolTip(radioButtonProjectionCenter1, resources.GetString("radioButtonProjectionCenter1.ToolTip"));
            radioButtonProjectionCenter1.UseVisualStyleBackColor = true;
            radioButtonProjectionCenter1.CheckedChanged += radioButtonScreenCenter1_CheckedChanged;
            // 
            // radioButtonProjectionCenter2
            // 
            resources.ApplyResources(radioButtonProjectionCenter2, "radioButtonProjectionCenter2");
            radioButtonProjectionCenter2.Checked = true;
            radioButtonProjectionCenter2.Name = "radioButtonProjectionCenter2";
            radioButtonProjectionCenter2.TabStop = true;
            toolTip.SetToolTip(radioButtonProjectionCenter2, resources.GetString("radioButtonProjectionCenter2.ToolTip"));
            radioButtonProjectionCenter2.UseVisualStyleBackColor = true;
            radioButtonProjectionCenter2.CheckedChanged += radioButtonScreenCenter1_CheckedChanged;
            // 
            // groupBoxProjection
            // 
            resources.ApplyResources(groupBoxProjection, "groupBoxProjection");
            groupBoxProjection.Controls.Add(comboBoxProjectionMode);
            groupBoxProjection.Controls.Add(trackBarPerspective);
            groupBoxProjection.Name = "groupBoxProjection";
            groupBoxProjection.TabStop = false;
            toolTip.SetToolTip(groupBoxProjection, resources.GetString("groupBoxProjection.ToolTip"));
            // 
            // comboBoxProjectionMode
            // 
            resources.ApplyResources(comboBoxProjectionMode, "comboBoxProjectionMode");
            comboBoxProjectionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxProjectionMode.FormattingEnabled = true;
            comboBoxProjectionMode.Items.AddRange(new object[] { resources.GetString("comboBoxProjectionMode.Items"), resources.GetString("comboBoxProjectionMode.Items1") });
            comboBoxProjectionMode.Name = "comboBoxProjectionMode";
            toolTip.SetToolTip(comboBoxProjectionMode, resources.GetString("comboBoxProjectionMode.ToolTip"));
            comboBoxProjectionMode.SelectedIndexChanged += comboBoxProjectionMode_SelectedIndexChanged;
            // 
            // trackBarPerspective
            // 
            resources.ApplyResources(trackBarPerspective, "trackBarPerspective");
            trackBarPerspective.Maximum = 120;
            trackBarPerspective.Name = "trackBarPerspective";
            trackBarPerspective.SmallChange = 10;
            trackBarPerspective.TickFrequency = 3;
            toolTip.SetToolTip(trackBarPerspective, resources.GetString("trackBarPerspective.ToolTip"));
            trackBarPerspective.Value = 100;
            trackBarPerspective.Scroll += trackBarPerspective_Scroll;
            // 
            // groupBoxTransparency
            // 
            resources.ApplyResources(groupBoxTransparency, "groupBoxTransparency");
            groupBoxTransparency.Controls.Add(comboBoxTransparency);
            groupBoxTransparency.Name = "groupBoxTransparency";
            groupBoxTransparency.TabStop = false;
            toolTip.SetToolTip(groupBoxTransparency, resources.GetString("groupBoxTransparency.ToolTip"));
            // 
            // comboBoxTransparency
            // 
            resources.ApplyResources(comboBoxTransparency, "comboBoxTransparency");
            comboBoxTransparency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxTransparency.DropDownWidth = 500;
            comboBoxTransparency.FormattingEnabled = true;
            comboBoxTransparency.Items.AddRange(new object[] { resources.GetString("comboBoxTransparency.Items"), resources.GetString("comboBoxTransparency.Items1"), resources.GetString("comboBoxTransparency.Items2") });
            comboBoxTransparency.Name = "comboBoxTransparency";
            toolTip.SetToolTip(comboBoxTransparency, resources.GetString("comboBoxTransparency.ToolTip"));
            comboBoxTransparency.SelectedIndexChanged += comboBoxTransparency_SelectedIndexChanged;
            // 
            // checkBoxDepthFadingOut
            // 
            resources.ApplyResources(checkBoxDepthFadingOut, "checkBoxDepthFadingOut");
            checkBoxDepthFadingOut.Checked = true;
            checkBoxDepthFadingOut.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxDepthFadingOut.Name = "checkBoxDepthFadingOut";
            toolTip.SetToolTip(checkBoxDepthFadingOut, resources.GetString("checkBoxDepthFadingOut.ToolTip"));
            checkBoxDepthFadingOut.UseVisualStyleBackColor = true;
            checkBoxDepthFadingOut.CheckedChanged += checkBoxDepthCueing_CheckedChanged;
            // 
            // groupBoxRenderingQuality
            // 
            resources.ApplyResources(groupBoxRenderingQuality, "groupBoxRenderingQuality");
            groupBoxRenderingQuality.Controls.Add(comboBoxRenderingQuality);
            groupBoxRenderingQuality.Name = "groupBoxRenderingQuality";
            groupBoxRenderingQuality.TabStop = false;
            toolTip.SetToolTip(groupBoxRenderingQuality, resources.GetString("groupBoxRenderingQuality.ToolTip"));
            // 
            // comboBoxRenderingQuality
            // 
            resources.ApplyResources(comboBoxRenderingQuality, "comboBoxRenderingQuality");
            comboBoxRenderingQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxRenderingQuality.DropDownWidth = 100;
            comboBoxRenderingQuality.FormattingEnabled = true;
            comboBoxRenderingQuality.Items.AddRange(new object[] { resources.GetString("comboBoxRenderingQuality.Items"), resources.GetString("comboBoxRenderingQuality.Items1"), resources.GetString("comboBoxRenderingQuality.Items2") });
            comboBoxRenderingQuality.Name = "comboBoxRenderingQuality";
            toolTip.SetToolTip(comboBoxRenderingQuality, resources.GetString("comboBoxRenderingQuality.ToolTip"));
            comboBoxRenderingQuality.SelectedIndexChanged += comboBoxRenderingQuality_SelectedIndexChanged;
            // 
            // groupBoxDepthCueing
            // 
            resources.ApplyResources(groupBoxDepthCueing, "groupBoxDepthCueing");
            groupBoxDepthCueing.Controls.Add(trackBarAdvancedDepthCueingNear);
            groupBoxDepthCueing.Controls.Add(label6);
            groupBoxDepthCueing.Controls.Add(trackBarAdvancedDepthCueingFar);
            groupBoxDepthCueing.Controls.Add(label5);
            groupBoxDepthCueing.Name = "groupBoxDepthCueing";
            groupBoxDepthCueing.TabStop = false;
            toolTip.SetToolTip(groupBoxDepthCueing, resources.GetString("groupBoxDepthCueing.ToolTip"));
            // 
            // trackBarAdvancedDepthCueingNear
            // 
            resources.ApplyResources(trackBarAdvancedDepthCueingNear, "trackBarAdvancedDepthCueingNear");
            trackBarAdvancedDepthCueingNear.ControlHeight = 25;
            trackBarAdvancedDepthCueingNear.DecimalPlaces = 1;
            trackBarAdvancedDepthCueingNear.LogScrollBar = false;
            trackBarAdvancedDepthCueingNear.Maximum = 30D;
            trackBarAdvancedDepthCueingNear.Minimum = -30D;
            trackBarAdvancedDepthCueingNear.Name = "trackBarAdvancedDepthCueingNear";
            trackBarAdvancedDepthCueingNear.NumericBoxSize = 105;
            trackBarAdvancedDepthCueingNear.Orientation = System.Windows.Forms.Orientation.Vertical;
            trackBarAdvancedDepthCueingNear.Smart_Increment = true;
            trackBarAdvancedDepthCueingNear.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            toolTip.SetToolTip(trackBarAdvancedDepthCueingNear, resources.GetString("trackBarAdvancedDepthCueingNear.ToolTip"));
            trackBarAdvancedDepthCueingNear.UpDown_Increment = 1D;
            trackBarAdvancedDepthCueingNear.Value = 5D;
            trackBarAdvancedDepthCueingNear.ValueChanged += trackBarAdvanced2_ValueChanged;
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // trackBarAdvancedDepthCueingFar
            // 
            resources.ApplyResources(trackBarAdvancedDepthCueingFar, "trackBarAdvancedDepthCueingFar");
            trackBarAdvancedDepthCueingFar.ControlHeight = 25;
            trackBarAdvancedDepthCueingFar.DecimalPlaces = 1;
            trackBarAdvancedDepthCueingFar.LogScrollBar = false;
            trackBarAdvancedDepthCueingFar.Maximum = 30D;
            trackBarAdvancedDepthCueingFar.Minimum = -30D;
            trackBarAdvancedDepthCueingFar.Name = "trackBarAdvancedDepthCueingFar";
            trackBarAdvancedDepthCueingFar.NumericBoxSize = 105;
            trackBarAdvancedDepthCueingFar.Orientation = System.Windows.Forms.Orientation.Vertical;
            trackBarAdvancedDepthCueingFar.Smart_Increment = true;
            trackBarAdvancedDepthCueingFar.TickStyle = System.Windows.Forms.TickStyle.BottomRight;
            toolTip.SetToolTip(trackBarAdvancedDepthCueingFar, resources.GetString("trackBarAdvancedDepthCueingFar.ToolTip"));
            trackBarAdvancedDepthCueingFar.UpDown_Increment = 1D;
            trackBarAdvancedDepthCueingFar.Value = -15D;
            trackBarAdvancedDepthCueingFar.ValueChanged += trackBarAdvanced2_ValueChanged;
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // tabPageMisc
            // 
            resources.ApplyResources(tabPageMisc, "tabPageMisc");
            captureExtender.SetCapture(tabPageMisc, true);
            tabPageMisc.Controls.Add(groupBoxBondedAtoms);
            tabPageMisc.Controls.Add(groupBoxLabel);
            tabPageMisc.Controls.Add(groupBoxAccessoryControls);
            tabPageMisc.Name = "tabPageMisc";
            toolTip.SetToolTip(tabPageMisc, resources.GetString("tabPageMisc.ToolTip"));
            // 
            // groupBoxBondedAtoms
            // 
            resources.ApplyResources(groupBoxBondedAtoms, "groupBoxBondedAtoms");
            groupBoxBondedAtoms.Controls.Add(checkBoxShowBondedAtoms);
            groupBoxBondedAtoms.Name = "groupBoxBondedAtoms";
            groupBoxBondedAtoms.TabStop = false;
            toolTip.SetToolTip(groupBoxBondedAtoms, resources.GetString("groupBoxBondedAtoms.ToolTip"));
            // 
            // checkBoxShowBondedAtoms
            // 
            resources.ApplyResources(checkBoxShowBondedAtoms, "checkBoxShowBondedAtoms");
            checkBoxShowBondedAtoms.Checked = true;
            checkBoxShowBondedAtoms.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowBondedAtoms.Name = "checkBoxShowBondedAtoms";
            toolTip.SetToolTip(checkBoxShowBondedAtoms, resources.GetString("checkBoxShowBondedAtoms.ToolTip"));
            checkBoxShowBondedAtoms.UseVisualStyleBackColor = true;
            checkBoxShowBondedAtoms.CheckedChanged += checkBoxShowBoundPlanes_CheckedChanged;
            // 
            // groupBoxLabel
            // 
            resources.ApplyResources(groupBoxLabel, "groupBoxLabel");
            groupBoxLabel.Controls.Add(colorControlLabelColor);
            groupBoxLabel.Controls.Add(checkBoxShowLabel);
            groupBoxLabel.Controls.Add(radioButtonUseMaterialColor);
            groupBoxLabel.Controls.Add(radioButtonLabelUseFixedColor);
            groupBoxLabel.Controls.Add(numericBoxLabelSize);
            groupBoxLabel.Controls.Add(checkBoxLabelWhiteEdge);
            groupBoxLabel.Controls.Add(label7);
            groupBoxLabel.Name = "groupBoxLabel";
            groupBoxLabel.TabStop = false;
            toolTip.SetToolTip(groupBoxLabel, resources.GetString("groupBoxLabel.ToolTip"));
            // 
            // colorControlLabelColor
            // 
            resources.ApplyResources(colorControlLabelColor, "colorControlLabelColor");
            colorControlLabelColor.Argb = -16777216;
            colorControlLabelColor.BackColor = System.Drawing.SystemColors.Control;
            colorControlLabelColor.Blue = 0;
            colorControlLabelColor.BlueF = 0F;
            colorControlLabelColor.BoxSize = new System.Drawing.Size(20, 20);
            colorControlLabelColor.Color = System.Drawing.Color.FromArgb(0, 0, 0);
            colorControlLabelColor.Green = 0;
            colorControlLabelColor.GreenF = 0F;
            colorControlLabelColor.Name = "colorControlLabelColor";
            colorControlLabelColor.Red = 0;
            colorControlLabelColor.RedF = 0F;
            toolTip.SetToolTip(colorControlLabelColor, resources.GetString("colorControlLabelColor.ToolTip1"));
            colorControlLabelColor.ColorChanged += numericBoxLabelSize_ValueChanged;
            // 
            // checkBoxShowLabel
            // 
            resources.ApplyResources(checkBoxShowLabel, "checkBoxShowLabel");
            checkBoxShowLabel.Name = "checkBoxShowLabel";
            toolTip.SetToolTip(checkBoxShowLabel, resources.GetString("checkBoxShowLabel.ToolTip"));
            checkBoxShowLabel.UseVisualStyleBackColor = true;
            checkBoxShowLabel.CheckedChanged += checkBoxShowLabel_CheckedChanged;
            // 
            // radioButtonUseMaterialColor
            // 
            resources.ApplyResources(radioButtonUseMaterialColor, "radioButtonUseMaterialColor");
            radioButtonUseMaterialColor.Checked = true;
            radioButtonUseMaterialColor.Name = "radioButtonUseMaterialColor";
            radioButtonUseMaterialColor.TabStop = true;
            toolTip.SetToolTip(radioButtonUseMaterialColor, resources.GetString("radioButtonUseMaterialColor.ToolTip"));
            radioButtonUseMaterialColor.UseVisualStyleBackColor = true;
            radioButtonUseMaterialColor.CheckedChanged += numericBoxLabelSize_ValueChanged;
            // 
            // radioButtonLabelUseFixedColor
            // 
            resources.ApplyResources(radioButtonLabelUseFixedColor, "radioButtonLabelUseFixedColor");
            radioButtonLabelUseFixedColor.Name = "radioButtonLabelUseFixedColor";
            toolTip.SetToolTip(radioButtonLabelUseFixedColor, resources.GetString("radioButtonLabelUseFixedColor.ToolTip"));
            radioButtonLabelUseFixedColor.UseVisualStyleBackColor = true;
            // 
            // numericBoxLabelSize
            // 
            resources.ApplyResources(numericBoxLabelSize, "numericBoxLabelSize");
            numericBoxLabelSize.BackColor = System.Drawing.Color.Transparent;
            numericBoxLabelSize.DecimalPlaces = 0;
            numericBoxLabelSize.Maximum = 200D;
            numericBoxLabelSize.Minimum = 0D;
            numericBoxLabelSize.Name = "numericBoxLabelSize";
            numericBoxLabelSize.RadianValue = 0.20943951023931953D;
            numericBoxLabelSize.ShowUpDown = true;
            numericBoxLabelSize.SmartIncrement = true;
            numericBoxLabelSize.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxLabelSize, resources.GetString("numericBoxLabelSize.ToolTip"));
            numericBoxLabelSize.Value = 12D;
            numericBoxLabelSize.ValueChanged += numericBoxLabelSize_ValueChanged;
            // 
            // checkBoxLabelWhiteEdge
            // 
            resources.ApplyResources(checkBoxLabelWhiteEdge, "checkBoxLabelWhiteEdge");
            checkBoxLabelWhiteEdge.Checked = true;
            checkBoxLabelWhiteEdge.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxLabelWhiteEdge.Name = "checkBoxLabelWhiteEdge";
            toolTip.SetToolTip(checkBoxLabelWhiteEdge, resources.GetString("checkBoxLabelWhiteEdge.ToolTip"));
            checkBoxLabelWhiteEdge.UseVisualStyleBackColor = true;
            checkBoxLabelWhiteEdge.CheckedChanged += numericBoxLabelSize_ValueChanged;
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip"));
            // 
            // groupBoxAccessoryControls
            // 
            resources.ApplyResources(groupBoxAccessoryControls, "groupBoxAccessoryControls");
            groupBoxAccessoryControls.Controls.Add(checkBoxGroupByElement);
            groupBoxAccessoryControls.Controls.Add(numericBoxLegendSize);
            groupBoxAccessoryControls.Controls.Add(numericBoxAxesSize);
            groupBoxAccessoryControls.Controls.Add(numericBoxLightSize);
            groupBoxAccessoryControls.Name = "groupBoxAccessoryControls";
            groupBoxAccessoryControls.TabStop = false;
            toolTip.SetToolTip(groupBoxAccessoryControls, resources.GetString("groupBoxAccessoryControls.ToolTip"));
            // 
            // checkBoxGroupByElement
            // 
            resources.ApplyResources(checkBoxGroupByElement, "checkBoxGroupByElement");
            checkBoxGroupByElement.Name = "checkBoxGroupByElement";
            toolTip.SetToolTip(checkBoxGroupByElement, resources.GetString("checkBoxGroupByElement.ToolTip"));
            checkBoxGroupByElement.UseVisualStyleBackColor = true;
            checkBoxGroupByElement.CheckedChanged += numericBoxLegendSize_ValueChanged;
            // 
            // numericBoxLegendSize
            // 
            resources.ApplyResources(numericBoxLegendSize, "numericBoxLegendSize");
            numericBoxLegendSize.BackColor = System.Drawing.Color.Transparent;
            numericBoxLegendSize.DecimalPlaces = 0;
            numericBoxLegendSize.Maximum = 200D;
            numericBoxLegendSize.Minimum = 0D;
            numericBoxLegendSize.Name = "numericBoxLegendSize";
            numericBoxLegendSize.RadianValue = 0.87266462599716477D;
            numericBoxLegendSize.ShowUpDown = true;
            numericBoxLegendSize.SmartIncrement = true;
            numericBoxLegendSize.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxLegendSize, resources.GetString("numericBoxLegendSize.ToolTip"));
            numericBoxLegendSize.Value = 50D;
            numericBoxLegendSize.ValueChanged += numericBoxLegendSize_ValueChanged;
            // 
            // numericBoxAxesSize
            // 
            resources.ApplyResources(numericBoxAxesSize, "numericBoxAxesSize");
            numericBoxAxesSize.BackColor = System.Drawing.Color.Transparent;
            numericBoxAxesSize.DecimalPlaces = 0;
            numericBoxAxesSize.Maximum = 200D;
            numericBoxAxesSize.Minimum = 0D;
            numericBoxAxesSize.Name = "numericBoxAxesSize";
            numericBoxAxesSize.RadianValue = 1.3962634015954636D;
            numericBoxAxesSize.ShowUpDown = true;
            numericBoxAxesSize.SmartIncrement = true;
            numericBoxAxesSize.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBoxAxesSize.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxAxesSize, resources.GetString("numericBoxAxesSize.ToolTip"));
            numericBoxAxesSize.Value = 80D;
            numericBoxAxesSize.ValueChanged += numericBoxAxesSize_ValueChanged;
            // 
            // numericBoxLightSize
            // 
            resources.ApplyResources(numericBoxLightSize, "numericBoxLightSize");
            numericBoxLightSize.BackColor = System.Drawing.Color.Transparent;
            numericBoxLightSize.DecimalPlaces = 0;
            numericBoxLightSize.Maximum = 200D;
            numericBoxLightSize.Minimum = 0D;
            numericBoxLightSize.Name = "numericBoxLightSize";
            numericBoxLightSize.RadianValue = 1.3962634015954636D;
            numericBoxLightSize.ShowUpDown = true;
            numericBoxLightSize.SmartIncrement = true;
            numericBoxLightSize.TextBoxBackColor = System.Drawing.SystemColors.Control;
            numericBoxLightSize.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxLightSize, resources.GetString("numericBoxLightSize.ToolTip"));
            numericBoxLightSize.Value = 80D;
            numericBoxLightSize.ValueChanged += numericBoxLightSize_ValueChanged;
            // 
            // toolStrip1
            // 
            resources.ApplyResources(toolStrip1, "toolStrip1");
            captureExtender.SetCapture(toolStrip1, true);
            toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButtonCrystalAxes, toolStripButtonLightDirection, toolStripButtonLegend, toolStripSeparator3, toolStripLabelStatusInitialization, toolStripLabelStatusRendering, toolStripButtonLikeVesta, toolStripButtonResetRotation });
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Stretch = true;
            toolTip.SetToolTip(toolStrip1, resources.GetString("toolStrip1.ToolTip"));
            // 
            // toolStripButtonCrystalAxes
            // 
            resources.ApplyResources(toolStripButtonCrystalAxes, "toolStripButtonCrystalAxes");
            toolStripButtonCrystalAxes.Checked = true;
            toolStripButtonCrystalAxes.CheckOnClick = true;
            toolStripButtonCrystalAxes.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripButtonCrystalAxes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonCrystalAxes.Name = "toolStripButtonCrystalAxes";
            toolStripButtonCrystalAxes.CheckedChanged += toolStripButtonCrystalAxes_CheckedChanged;
            // 
            // toolStripButtonLightDirection
            // 
            resources.ApplyResources(toolStripButtonLightDirection, "toolStripButtonLightDirection");
            toolStripButtonLightDirection.BackColor = System.Drawing.SystemColors.Control;
            toolStripButtonLightDirection.Checked = true;
            toolStripButtonLightDirection.CheckOnClick = true;
            toolStripButtonLightDirection.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripButtonLightDirection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonLightDirection.Name = "toolStripButtonLightDirection";
            toolStripButtonLightDirection.CheckedChanged += toolStripButtonLightingBall_CheckedChanged;
            // 
            // toolStripButtonLegend
            // 
            resources.ApplyResources(toolStripButtonLegend, "toolStripButtonLegend");
            toolStripButtonLegend.Checked = true;
            toolStripButtonLegend.CheckOnClick = true;
            toolStripButtonLegend.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripButtonLegend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonLegend.Name = "toolStripButtonLegend";
            toolStripButtonLegend.CheckedChanged += toolStripButtonLegend_CheckedChanged;
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // toolStripLabelStatusInitialization
            // 
            resources.ApplyResources(toolStripLabelStatusInitialization, "toolStripLabelStatusInitialization");
            toolStripLabelStatusInitialization.Name = "toolStripLabelStatusInitialization";
            // 
            // toolStripLabelStatusRendering
            // 
            resources.ApplyResources(toolStripLabelStatusRendering, "toolStripLabelStatusRendering");
            toolStripLabelStatusRendering.Name = "toolStripLabelStatusRendering";
            // 
            // toolStripButtonLikeVesta
            // 
            resources.ApplyResources(toolStripButtonLikeVesta, "toolStripButtonLikeVesta");
            toolStripButtonLikeVesta.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripButtonLikeVesta.BackColor = System.Drawing.Color.SteelBlue;
            toolStripButtonLikeVesta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonLikeVesta.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            toolStripButtonLikeVesta.Name = "toolStripButtonLikeVesta";
            toolStripButtonLikeVesta.Click += toolStripButtonLikeVesta_Click;
            // 
            // toolStripButtonResetRotation
            // 
            resources.ApplyResources(toolStripButtonResetRotation, "toolStripButtonResetRotation");
            toolStripButtonResetRotation.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            toolStripButtonResetRotation.BackColor = System.Drawing.Color.IndianRed;
            toolStripButtonResetRotation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripButtonResetRotation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            toolStripButtonResetRotation.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
            toolStripButtonResetRotation.Name = "toolStripButtonResetRotation";
            toolStripButtonResetRotation.Click += toolStripButtonResetRotation_Click;
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            captureExtender.SetCapture(menuStrip1, true);
            menuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { saveImageToolStripMenuItem, toolToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Name = "menuStrip1";
            toolTip.SetToolTip(menuStrip1, resources.GetString("menuStrip1.ToolTip"));
            // 
            // saveImageToolStripMenuItem
            // 
            resources.ApplyResources(saveImageToolStripMenuItem, "saveImageToolStripMenuItem");
            captureExtender.SetCapture(saveImageToolStripMenuItem, true);
            saveImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveImageToolStripMenuItem1, copyToClipboardToolStripMenuItem, saveMovieToolStripMenuItem });
            saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            // 
            // saveImageToolStripMenuItem1
            // 
            resources.ApplyResources(saveImageToolStripMenuItem1, "saveImageToolStripMenuItem1");
            saveImageToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveMainToolStripMenuItem, saveLegendToolStripMenuItem, saveAxesToolStripMenuItem, saveLightToolStripMenuItem });
            saveImageToolStripMenuItem1.Name = "saveImageToolStripMenuItem1";
            // 
            // saveMainToolStripMenuItem
            // 
            resources.ApplyResources(saveMainToolStripMenuItem, "saveMainToolStripMenuItem");
            saveMainToolStripMenuItem.Name = "saveMainToolStripMenuItem";
            saveMainToolStripMenuItem.Click += saveImageToolStripMenuItem_Click;
            // 
            // saveLegendToolStripMenuItem
            // 
            resources.ApplyResources(saveLegendToolStripMenuItem, "saveLegendToolStripMenuItem");
            saveLegendToolStripMenuItem.Name = "saveLegendToolStripMenuItem";
            // 
            // saveAxesToolStripMenuItem
            // 
            resources.ApplyResources(saveAxesToolStripMenuItem, "saveAxesToolStripMenuItem");
            saveAxesToolStripMenuItem.Name = "saveAxesToolStripMenuItem";
            saveAxesToolStripMenuItem.Click += saveImageToolStripMenuItem_Click;
            // 
            // saveLightToolStripMenuItem
            // 
            resources.ApplyResources(saveLightToolStripMenuItem, "saveLightToolStripMenuItem");
            saveLightToolStripMenuItem.Name = "saveLightToolStripMenuItem";
            saveLightToolStripMenuItem.Click += saveImageToolStripMenuItem_Click;
            // 
            // copyToClipboardToolStripMenuItem
            // 
            resources.ApplyResources(copyToClipboardToolStripMenuItem, "copyToClipboardToolStripMenuItem");
            copyToClipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { copyMainToolStripMenuItem, copyLegendToolStripMenuItem1, copyAxesToolStripMenuItem, copyLightToolStripMenuItem });
            copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            // 
            // copyMainToolStripMenuItem
            // 
            resources.ApplyResources(copyMainToolStripMenuItem, "copyMainToolStripMenuItem");
            copyMainToolStripMenuItem.Name = "copyMainToolStripMenuItem";
            copyMainToolStripMenuItem.Click += saveImageToolStripMenuItem_Click;
            // 
            // copyLegendToolStripMenuItem1
            // 
            resources.ApplyResources(copyLegendToolStripMenuItem1, "copyLegendToolStripMenuItem1");
            copyLegendToolStripMenuItem1.Name = "copyLegendToolStripMenuItem1";
            // 
            // copyAxesToolStripMenuItem
            // 
            resources.ApplyResources(copyAxesToolStripMenuItem, "copyAxesToolStripMenuItem");
            copyAxesToolStripMenuItem.Name = "copyAxesToolStripMenuItem";
            copyAxesToolStripMenuItem.Click += saveImageToolStripMenuItem_Click;
            // 
            // copyLightToolStripMenuItem
            // 
            resources.ApplyResources(copyLightToolStripMenuItem, "copyLightToolStripMenuItem");
            copyLightToolStripMenuItem.Name = "copyLightToolStripMenuItem";
            copyLightToolStripMenuItem.Click += saveImageToolStripMenuItem_Click;
            // 
            // saveMovieToolStripMenuItem
            // 
            resources.ApplyResources(saveMovieToolStripMenuItem, "saveMovieToolStripMenuItem");
            saveMovieToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { SaveMovieMainImageToolStripMenuItem, SaveMovieCrystalAxesToolStripMenuItem });
            saveMovieToolStripMenuItem.Name = "saveMovieToolStripMenuItem";
            // 
            // SaveMovieMainImageToolStripMenuItem
            // 
            resources.ApplyResources(SaveMovieMainImageToolStripMenuItem, "SaveMovieMainImageToolStripMenuItem");
            SaveMovieMainImageToolStripMenuItem.Name = "SaveMovieMainImageToolStripMenuItem";
            SaveMovieMainImageToolStripMenuItem.Click += SaveMovieMainImageToolStripMenuItem_Click;
            // 
            // SaveMovieCrystalAxesToolStripMenuItem
            // 
            resources.ApplyResources(SaveMovieCrystalAxesToolStripMenuItem, "SaveMovieCrystalAxesToolStripMenuItem");
            SaveMovieCrystalAxesToolStripMenuItem.Name = "SaveMovieCrystalAxesToolStripMenuItem";
            SaveMovieCrystalAxesToolStripMenuItem.Click += SaveMovieMainImageToolStripMenuItem_Click;
            // 
            // toolToolStripMenuItem
            // 
            resources.ApplyResources(toolToolStripMenuItem, "toolToolStripMenuItem");
            captureExtender.SetCapture(toolToolStripMenuItem, true);
            toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { iLikeVESTAToolStripMenuItem });
            toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            // 
            // iLikeVESTAToolStripMenuItem
            // 
            resources.ApplyResources(iLikeVESTAToolStripMenuItem, "iLikeVESTAToolStripMenuItem");
            iLikeVESTAToolStripMenuItem.Name = "iLikeVESTAToolStripMenuItem";
            iLikeVESTAToolStripMenuItem.Click += toolStripButtonLikeVesta_Click;
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem, cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem, toolStripMenuItem2 });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem
            // 
            resources.ApplyResources(cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem, "cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem");
            cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem.Name = "cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem";
            // 
            // cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem
            // 
            resources.ApplyResources(cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem, "cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem");
            cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem.Name = "cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem";
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(toolStripMenuItem2, "toolStripMenuItem2");
            toolStripMenuItem2.ForeColor = System.Drawing.SystemColors.HotTrack;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 100;
            // 
            // numericBoxClientHeight
            // 
            resources.ApplyResources(numericBoxClientHeight, "numericBoxClientHeight");
            numericBoxClientHeight.BackColor = System.Drawing.SystemColors.Control;
            numericBoxClientHeight.DecimalPlaces = 0;
            numericBoxClientHeight.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxClientHeight.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxClientHeight.Maximum = 4000D;
            numericBoxClientHeight.Minimum = 1D;
            numericBoxClientHeight.Name = "numericBoxClientHeight";
            numericBoxClientHeight.RadianValue = 17.453292519943293D;
            numericBoxClientHeight.ShowUpDown = true;
            numericBoxClientHeight.SmartIncrement = true;
            numericBoxClientHeight.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxClientHeight, resources.GetString("numericBoxClientHeight.ToolTip"));
            numericBoxClientHeight.Value = 1000D;
            numericBoxClientHeight.ValueChanged += numericBoxClientWidth_ValueChanged;
            // 
            // numericBoxClientWidth
            // 
            resources.ApplyResources(numericBoxClientWidth, "numericBoxClientWidth");
            numericBoxClientWidth.BackColor = System.Drawing.SystemColors.Control;
            numericBoxClientWidth.DecimalPlaces = 0;
            numericBoxClientWidth.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxClientWidth.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxClientWidth.Maximum = 4000D;
            numericBoxClientWidth.Minimum = 1D;
            numericBoxClientWidth.Name = "numericBoxClientWidth";
            numericBoxClientWidth.RadianValue = 17.453292519943293D;
            numericBoxClientWidth.ShowUpDown = true;
            numericBoxClientWidth.SmartIncrement = true;
            numericBoxClientWidth.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxClientWidth, resources.GetString("numericBoxClientWidth.ToolTip"));
            numericBoxClientWidth.Value = 1000D;
            numericBoxClientWidth.ValueChanged += numericBoxClientWidth_ValueChanged;
            // 
            // printPreviewDialog1
            // 
            resources.ApplyResources(printPreviewDialog1, "printPreviewDialog1");
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Name = "printPreviewDialog1";
            toolTip.SetToolTip(printPreviewDialog1, resources.GetString("printPreviewDialog1.ToolTip"));
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // tabPageCrystal
            // 
            resources.ApplyResources(tabPageCrystal, "tabPageCrystal");
            tabPageCrystal.Name = "tabPageCrystal";
            toolTip.SetToolTip(tabPageCrystal, resources.GetString("tabPageCrystal.ToolTip"));
            // 
            // panelClientSize
            // 
            resources.ApplyResources(panelClientSize, "panelClientSize");
            panelClientSize.Controls.Add(numericBoxClientWidth);
            panelClientSize.Controls.Add(numericBoxClientHeight);
            panelClientSize.Name = "panelClientSize";
            toolTip.SetToolTip(panelClientSize, resources.GetString("panelClientSize.ToolTip"));
            // 
            // printDialog1
            // 
            printDialog1.Document = printDocument1;
            printDialog1.UseEXDialog = true;
            // 
            // pageSetupDialog1
            // 
            pageSetupDialog1.Document = printDocument1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.DataPropertyName = "Atom";
            resources.ApplyResources(dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.DataPropertyName = "Atom";
            resources.ApplyResources(dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.DataPropertyName = "Atom";
            resources.ApplyResources(dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.DataPropertyName = "Atom";
            resources.ApplyResources(dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // FormStructureViewer
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(splitContainer1);
            Controls.Add(panelClientSize);
            Controls.Add(menuStrip1);
            Controls.Add(toolStrip1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "FormStructureViewer";
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            FormClosing += FormStructureViewer_FormClosing;
            Load += FormStructureViewer_Load;
            ResizeBegin += FormStructureViewer_ResizeBegin;
            ResizeEnd += FormStructureViewer_ResizeEnd;
            VisibleChanged += FormStructureViewer_VisibleChanged;
            KeyDown += FormStructureViewer_KeyDown;
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabPageBounds.ResumeLayout(false);
            tabPageBounds.PerformLayout();
            tabControlBoundOption.ResumeLayout(false);
            tabPageBoundUnitcell.ResumeLayout(false);
            tabPageBoundUnitcell.PerformLayout();
            flowLayoutPanelLatticePlaneOptions.ResumeLayout(false);
            flowLayoutPanelLatticePlaneOptions.PerformLayout();
            flowLayoutPanelBoundType.ResumeLayout(false);
            flowLayoutPanelBoundType.PerformLayout();
            tabPageAtom.ResumeLayout(false);
            tabPageAtom.PerformLayout();
            tabPageUnitCell.ResumeLayout(false);
            tabPageUnitCell.PerformLayout();
            groupBoxShowUnitCell.ResumeLayout(false);
            groupBoxShowUnitCell.PerformLayout();
            flowLayoutPanelCellPlaneColors.ResumeLayout(false);
            flowLayoutPanelCellPlaneColors.PerformLayout();
            flowLayoutPanelCellEdgeColors.ResumeLayout(false);
            flowLayoutPanelCellEdgeColors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarCellEdgeWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSubCellB).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSubCellC).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSubCellA).EndInit();
            tabPageLatticePlane.ResumeLayout(false);
            tabPageLatticePlane.PerformLayout();
            flowLayoutPanelLatticePlaneOpacity.ResumeLayout(false);
            tabPageCoordinateInformation.ResumeLayout(false);
            tabPageInformation.ResumeLayout(false);
            tabPageInformation.PerformLayout();
            flowLayoutPanelGraphicsInfo.ResumeLayout(false);
            flowLayoutPanelGraphicsInfo.PerformLayout();
            tabPageProjection.ResumeLayout(false);
            tabPageProjection.PerformLayout();
            groupBoxProjectionCenter.ResumeLayout(false);
            groupBoxProjectionCenter.PerformLayout();
            flowLayoutPanelProjectionCenter.ResumeLayout(false);
            groupBoxProjection.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)trackBarPerspective).EndInit();
            groupBoxTransparency.ResumeLayout(false);
            groupBoxRenderingQuality.ResumeLayout(false);
            groupBoxDepthCueing.ResumeLayout(false);
            groupBoxDepthCueing.PerformLayout();
            tabPageMisc.ResumeLayout(false);
            groupBoxBondedAtoms.ResumeLayout(false);
            groupBoxBondedAtoms.PerformLayout();
            groupBoxLabel.ResumeLayout(false);
            groupBoxLabel.PerformLayout();
            groupBoxAccessoryControls.ResumeLayout(false);
            groupBoxAccessoryControls.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelClientSize.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        public System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveMainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLegendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAxesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyMainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyLegendToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyAxesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonCrystalAxes;
        private System.Windows.Forms.ToolStripButton toolStripButtonLightDirection;
        private System.Windows.Forms.ToolStripButton toolStripButtonLegend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.TabPage tabPageCrystal;
        private System.Windows.Forms.ToolStripLabel toolStripLabelStatusInitialization;
        private System.Windows.Forms.ToolStripLabel toolStripLabelStatusRendering;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iLikeVESTAToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonResetRotation;
        private System.Windows.Forms.ToolStripButton toolStripButtonLikeVesta;
        private System.Windows.Forms.ToolStripMenuItem saveMovieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveMovieMainImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveMovieCrystalAxesToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageBounds;
        private System.Windows.Forms.TabControl tabControlBoundOption;
        private System.Windows.Forms.TabPage tabPageBoundUnitcell;
        private System.Windows.Forms.Button buttonSetRange2;
        private System.Windows.Forms.Button buttonSetRange4;
        private System.Windows.Forms.Button buttonSetRange3;
        private System.Windows.Forms.Button buttonSetCenter1;
        private System.Windows.Forms.Button buttonCenter2;
        private System.Windows.Forms.Button buttonSetCenter3;
        private System.Windows.Forms.Button buttonSetRange0;
        private System.Windows.Forms.Button buttonSetRange1;
        private NumericBox numericBoxCRange;
        private NumericBox numericBoxBRange;
        private NumericBox numericBoxARange;
        private NumericBox numericBoxCCenter;
        private NumericBox numericBoxBCenter;
        private NumericBox numericBoxACenter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageBoundPlane;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLatticePlaneOptions;
        private System.Windows.Forms.CheckBox checkBoxShowBoundPlanes;
        private NumericBox numericBoxBoundPlanesOpacity;
        private System.Windows.Forms.CheckBox checkBoxClipObjects;
        private System.Windows.Forms.CheckBox checkBoxHideAllAtoms;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBoundType;
        private System.Windows.Forms.RadioButton radioButtonBoundUnitCell;
        private System.Windows.Forms.RadioButton radioButtonBoundPlane;
        private System.Windows.Forms.TabPage tabPageAtom;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TabPage tabPageBond;
        private System.Windows.Forms.TabPage tabPageUnitCell;
        private System.Windows.Forms.CheckBox checkBoxUnitCell;
        private System.Windows.Forms.GroupBox groupBoxShowUnitCell;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCellPlaneColors;
        private System.Windows.Forms.RadioButton radioButtonCellPlaneColorAll;
        private ColorControl colorControlCellPlane;
        private System.Windows.Forms.RadioButton radioButtonCellPlaneColorEach;
        private ColorControl colorControlCellPlaneA;
        private ColorControl colorControlCellPlaneB;
        private ColorControl colorControlCellPlaneC;
        private NumericBox numericBoxCellPlaneAlpha;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCellEdgeColors;
        private System.Windows.Forms.RadioButton radioButtonCellEdgeColorAll;
        private ColorControl colorControlCellEdge;
        private System.Windows.Forms.RadioButton radioButtonCellEdgeColorEach;
        private ColorControl colorControlCellEdgeA;
        private ColorControl colorControlCellEdgeB;
        private ColorControl colorControlCellEdgeC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TrackBar trackBarCellEdgeWidth;
        private NumericBox numericBoxCellTranslationC;
        private NumericBox numericBoxCellTranslationB;
        private NumericBox numericBoxCellTranslationA;
        private System.Windows.Forms.CheckBox checkBoxShowSubCell;
        private System.Windows.Forms.CheckBox checkBoxCellShowEdge;
        private System.Windows.Forms.NumericUpDown numericUpDownSubCellB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxCellShowPlane;
        private System.Windows.Forms.NumericUpDown numericUpDownSubCellC;
        private System.Windows.Forms.NumericUpDown numericUpDownSubCellA;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabPageLatticePlane;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLatticePlaneOpacity;
        private NumericBox numericBoxLatticePlaneOpacity;
        private System.Windows.Forms.TabPage tabPageCoordinateInformation;
        private AtomCoordinateTable atomCoordinateTable1;
        private System.Windows.Forms.TabPage tabPageInformation;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxCalcInformation;
        private System.Windows.Forms.Panel panelClientSize;
        public NumericBox numericBoxClientWidth;
        public NumericBox numericBoxClientHeight;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelGraphicsInfo;
        private System.Windows.Forms.Label labelGraphicsCard;
        private System.Windows.Forms.Label labelGraphicsDriver;
        private System.Windows.Forms.Label labelOpenGLversion;
        private System.Windows.Forms.TabPage tabPageProjection;
        private System.Windows.Forms.GroupBox groupBoxProjectionCenter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProjectionCenter;
        private NumericBox numericBoxProjectionCenterX;
        private NumericBox numericBoxProjectionCenterY;
        private NumericBox numericBoxProjectionCenterZ;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radioButtonProjectionCenterCustom;
        private System.Windows.Forms.RadioButton radioButtonProjectionCenter1;
        private System.Windows.Forms.RadioButton radioButtonProjectionCenter2;
        private System.Windows.Forms.GroupBox groupBoxProjection;
        private System.Windows.Forms.ComboBox comboBoxProjectionMode;
        private System.Windows.Forms.TrackBar trackBarPerspective;
        private System.Windows.Forms.GroupBox groupBoxTransparency;
        private System.Windows.Forms.ComboBox comboBoxTransparency;
        private System.Windows.Forms.CheckBox checkBoxDepthFadingOut;
        private System.Windows.Forms.GroupBox groupBoxRenderingQuality;
        private System.Windows.Forms.ComboBox comboBoxRenderingQuality;
        private System.Windows.Forms.GroupBox groupBoxDepthCueing;
        private TrackBarAdvanced trackBarAdvancedDepthCueingNear;
        private System.Windows.Forms.Label label6;
        private TrackBarAdvanced trackBarAdvancedDepthCueingFar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPageMisc;
        private System.Windows.Forms.GroupBox groupBoxBondedAtoms;
        private System.Windows.Forms.CheckBox checkBoxShowBondedAtoms;
        private System.Windows.Forms.GroupBox groupBoxLabel;
        private ColorControl colorControlLabelColor;
        private System.Windows.Forms.CheckBox checkBoxShowLabel;
        private System.Windows.Forms.RadioButton radioButtonUseMaterialColor;
        private System.Windows.Forms.RadioButton radioButtonLabelUseFixedColor;
        private NumericBox numericBoxLabelSize;
        private System.Windows.Forms.CheckBox checkBoxLabelWhiteEdge;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBoxAccessoryControls;
        private System.Windows.Forms.CheckBox checkBoxGroupByElement;
        private NumericBox numericBoxLegendSize;
        private NumericBox numericBoxAxesSize;
        private NumericBox numericBoxLightSize;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLegend;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBoxAtomInformation;
        private System.Windows.Forms.Label label13;
    }
}
