namespace ReciPro
{
    partial class FormStructureViewer
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
            /*
            if (contextMain != null)
                contextMain.Dispose();
            if (contextLight != null)
                contextLight.Dispose();
            if (contextLegend != null)
                contextLegend.Dispose();
            if (contextAxes != null)
                contextAxes.Dispose();
                */
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStructureViewer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonBoost = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCrystalAxes = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLightDirection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLegend = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLegendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyLegendToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyLightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPerviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxUnitCell = new System.Windows.Forms.CheckBox();
            this.groupBoxShowUnitCell = new System.Windows.Forms.GroupBox();
            this.checkBoxShowSubCell = new System.Windows.Forms.CheckBox();
            this.checkBoxCellShowEdge = new System.Windows.Forms.CheckBox();
            this.pictureBoxCellPlaneColor = new System.Windows.Forms.PictureBox();
            this.pictureBoxCellEdgeColor = new System.Windows.Forms.PictureBox();
            this.numericUpDownSubCellB = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBoxCellShowPlane = new System.Windows.Forms.CheckBox();
            this.numericUpDownSubCellC = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSubCellA = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCellPlaneAlpha = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBoxInformation = new System.Windows.Forms.TextBox();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelStatusInitialization = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelStatusRendering = new System.Windows.Forms.ToolStripLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.glControlAxes = new Crystallography.OpenGL.GLControlAlpha();
            this.glControlLight = new Crystallography.OpenGL.GLControlAlpha();
            this.glControlMain = new Crystallography.OpenGL.GLControlAlpha();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCoordinateInfromatin = new System.Windows.Forms.TabPage();
            this.tabPageBounds = new System.Windows.Forms.TabPage();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label29 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label28 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanelBounds = new System.Windows.Forms.FlowLayoutPanel();
            this.label21 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.AddBoundary = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.checkBoxHideAllAtoms = new System.Windows.Forms.CheckBox();
            this.checkBoxClipObjects = new System.Windows.Forms.CheckBox();
            this.checkBoxShowBoundPlanes = new System.Windows.Forms.CheckBox();
            this.tabPageUnitCell = new System.Windows.Forms.TabPage();
            this.tabPageLatticePlane = new System.Windows.Forms.TabPage();
            this.flowLayoutPanelLatticePlanes = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAddLatticePlane = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageText = new System.Windows.Forms.TabPage();
            this.tabPageAtom = new System.Windows.Forms.TabPage();
            this.tabPageBond = new System.Windows.Forms.TabPage();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageCrystal = new System.Windows.Forms.TabPage();
            this.atomCoordinateTable1 = new Crystallography.Controls.AtomCoordinateTable();
            this.numericBox6 = new Crystallography.Controls.NumericBox();
            this.numericBox4 = new Crystallography.Controls.NumericBox();
            this.numericBox2 = new Crystallography.Controls.NumericBox();
            this.numericBox5 = new Crystallography.Controls.NumericBox();
            this.numericBox3 = new Crystallography.Controls.NumericBox();
            this.numericBox1 = new Crystallography.Controls.NumericBox();
            this.numericBoxBoundPlanesOpacity = new Crystallography.Controls.NumericBox();
            this.numericBoxCellTransrationC = new Crystallography.Controls.NumericBox();
            this.numericBoxCellTransrationB = new Crystallography.Controls.NumericBox();
            this.numericBoxCellTransrationA = new Crystallography.Controls.NumericBox();
            this.numericBoxLatticePlaneOpacity = new Crystallography.Controls.NumericBox();
            this.labelMessage = new System.Windows.Forms.Label();
            this.boundsControl1 = new ReciPro.BoundsControl();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBoxShowUnitCell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellPlaneColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellEdgeColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCellPlaneAlpha)).BeginInit();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageCoordinateInfromatin.SuspendLayout();
            this.tabPageBounds.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.flowLayoutPanelBounds.SuspendLayout();
            this.tabPageUnitCell.SuspendLayout();
            this.tabPageLatticePlane.SuspendLayout();
            this.tabPageText.SuspendLayout();
            this.tabPageBond.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonBoost,
            this.toolStripSeparator2,
            this.toolStripButtonCrystalAxes,
            this.toolStripButtonLightDirection,
            this.toolStripButtonLegend,
            this.toolStripSeparator3});
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Stretch = true;
            // 
            // toolStripButtonBoost
            // 
            this.toolStripButtonBoost.CheckOnClick = true;
            this.toolStripButtonBoost.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.toolStripButtonBoost, "toolStripButtonBoost");
            this.toolStripButtonBoost.Name = "toolStripButtonBoost";
            this.toolStripButtonBoost.CheckedChanged += new System.EventHandler(this.toolStripButtonBoost_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripButtonCrystalAxes
            // 
            this.toolStripButtonCrystalAxes.Checked = true;
            this.toolStripButtonCrystalAxes.CheckOnClick = true;
            this.toolStripButtonCrystalAxes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonCrystalAxes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.toolStripButtonCrystalAxes, "toolStripButtonCrystalAxes");
            this.toolStripButtonCrystalAxes.Name = "toolStripButtonCrystalAxes";
            this.toolStripButtonCrystalAxes.CheckedChanged += new System.EventHandler(this.checkBoxShowCrystalAxes_CheckedChanged);
            // 
            // toolStripButtonLightDirection
            // 
            this.toolStripButtonLightDirection.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripButtonLightDirection.Checked = true;
            this.toolStripButtonLightDirection.CheckOnClick = true;
            this.toolStripButtonLightDirection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonLightDirection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.toolStripButtonLightDirection, "toolStripButtonLightDirection");
            this.toolStripButtonLightDirection.Name = "toolStripButtonLightDirection";
            this.toolStripButtonLightDirection.CheckedChanged += new System.EventHandler(this.checkBoxShowLightingBall_CheckedChanged);
            // 
            // toolStripButtonLegend
            // 
            this.toolStripButtonLegend.Checked = true;
            this.toolStripButtonLegend.CheckOnClick = true;
            this.toolStripButtonLegend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonLegend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.toolStripButtonLegend, "toolStripButtonLegend");
            this.toolStripButtonLegend.Name = "toolStripButtonLegend";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem1,
            this.copyToClipboardToolStripMenuItem,
            this.toolStripSeparator1,
            this.pageSetupToolStripMenuItem,
            this.printPerviewToolStripMenuItem,
            this.printToolStripMenuItem});
            resources.ApplyResources(this.saveImageToolStripMenuItem, "saveImageToolStripMenuItem");
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            // 
            // saveImageToolStripMenuItem1
            // 
            this.saveImageToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMainToolStripMenuItem,
            this.saveLegendToolStripMenuItem,
            this.saveAxesToolStripMenuItem,
            this.saveLightToolStripMenuItem});
            this.saveImageToolStripMenuItem1.Name = "saveImageToolStripMenuItem1";
            resources.ApplyResources(this.saveImageToolStripMenuItem1, "saveImageToolStripMenuItem1");
            // 
            // saveMainToolStripMenuItem
            // 
            this.saveMainToolStripMenuItem.Name = "saveMainToolStripMenuItem";
            resources.ApplyResources(this.saveMainToolStripMenuItem, "saveMainToolStripMenuItem");
            this.saveMainToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // saveLegendToolStripMenuItem
            // 
            resources.ApplyResources(this.saveLegendToolStripMenuItem, "saveLegendToolStripMenuItem");
            this.saveLegendToolStripMenuItem.Name = "saveLegendToolStripMenuItem";
            // 
            // saveAxesToolStripMenuItem
            // 
            this.saveAxesToolStripMenuItem.Name = "saveAxesToolStripMenuItem";
            resources.ApplyResources(this.saveAxesToolStripMenuItem, "saveAxesToolStripMenuItem");
            this.saveAxesToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // saveLightToolStripMenuItem
            // 
            this.saveLightToolStripMenuItem.Name = "saveLightToolStripMenuItem";
            resources.ApplyResources(this.saveLightToolStripMenuItem, "saveLightToolStripMenuItem");
            this.saveLightToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMainToolStripMenuItem,
            this.copyLegendToolStripMenuItem1,
            this.copyAxesToolStripMenuItem,
            this.copyLightToolStripMenuItem});
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            resources.ApplyResources(this.copyToClipboardToolStripMenuItem, "copyToClipboardToolStripMenuItem");
            // 
            // copyMainToolStripMenuItem
            // 
            this.copyMainToolStripMenuItem.Name = "copyMainToolStripMenuItem";
            resources.ApplyResources(this.copyMainToolStripMenuItem, "copyMainToolStripMenuItem");
            this.copyMainToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // copyLegendToolStripMenuItem1
            // 
            resources.ApplyResources(this.copyLegendToolStripMenuItem1, "copyLegendToolStripMenuItem1");
            this.copyLegendToolStripMenuItem1.Name = "copyLegendToolStripMenuItem1";
            // 
            // copyAxesToolStripMenuItem
            // 
            this.copyAxesToolStripMenuItem.Name = "copyAxesToolStripMenuItem";
            resources.ApplyResources(this.copyAxesToolStripMenuItem, "copyAxesToolStripMenuItem");
            this.copyAxesToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // copyLightToolStripMenuItem
            // 
            this.copyLightToolStripMenuItem.Name = "copyLightToolStripMenuItem";
            resources.ApplyResources(this.copyLightToolStripMenuItem, "copyLightToolStripMenuItem");
            this.copyLightToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // pageSetupToolStripMenuItem
            // 
            resources.ApplyResources(this.pageSetupToolStripMenuItem, "pageSetupToolStripMenuItem");
            this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
            this.pageSetupToolStripMenuItem.Click += new System.EventHandler(this.pageSetupToolStripMenuItem_Click);
            // 
            // printPerviewToolStripMenuItem
            // 
            resources.ApplyResources(this.printPerviewToolStripMenuItem, "printPerviewToolStripMenuItem");
            this.printPerviewToolStripMenuItem.Name = "printPerviewToolStripMenuItem";
            this.printPerviewToolStripMenuItem.Click += new System.EventHandler(this.printPerviewToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItem1,
            this.cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem,
            this.toolStripMenuItem2});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem
            // 
            resources.ApplyResources(this.cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem, "cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlte" +
        "rnatelyToolStripMenuItem");
            this.cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem.Name = "cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlte" +
    "rnatelyToolStripMenuItem";
            // 
            // toolStripMenuItem3
            // 
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem
            // 
            resources.ApplyResources(this.cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem, "cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem");
            this.cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem.Name = "cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem";
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.IsBalloon = true;
            this.toolTip.ReshowDelay = 100;
            // 
            // checkBoxUnitCell
            // 
            resources.ApplyResources(this.checkBoxUnitCell, "checkBoxUnitCell");
            this.checkBoxUnitCell.BackColor = System.Drawing.SystemColors.Control;
            this.checkBoxUnitCell.Name = "checkBoxUnitCell";
            this.toolTip.SetToolTip(this.checkBoxUnitCell, resources.GetString("checkBoxUnitCell.ToolTip"));
            this.checkBoxUnitCell.UseVisualStyleBackColor = false;
            this.checkBoxUnitCell.CheckedChanged += new System.EventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // groupBoxShowUnitCell
            // 
            this.groupBoxShowUnitCell.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxShowUnitCell.Controls.Add(this.numericBoxCellTransrationC);
            this.groupBoxShowUnitCell.Controls.Add(this.numericBoxCellTransrationB);
            this.groupBoxShowUnitCell.Controls.Add(this.numericBoxCellTransrationA);
            this.groupBoxShowUnitCell.Controls.Add(this.checkBoxShowSubCell);
            this.groupBoxShowUnitCell.Controls.Add(this.checkBoxCellShowEdge);
            this.groupBoxShowUnitCell.Controls.Add(this.pictureBoxCellPlaneColor);
            this.groupBoxShowUnitCell.Controls.Add(this.pictureBoxCellEdgeColor);
            this.groupBoxShowUnitCell.Controls.Add(this.numericUpDownSubCellB);
            this.groupBoxShowUnitCell.Controls.Add(this.label13);
            this.groupBoxShowUnitCell.Controls.Add(this.label14);
            this.groupBoxShowUnitCell.Controls.Add(this.label11);
            this.groupBoxShowUnitCell.Controls.Add(this.label10);
            this.groupBoxShowUnitCell.Controls.Add(this.checkBoxCellShowPlane);
            this.groupBoxShowUnitCell.Controls.Add(this.numericUpDownSubCellC);
            this.groupBoxShowUnitCell.Controls.Add(this.numericUpDownSubCellA);
            this.groupBoxShowUnitCell.Controls.Add(this.numericUpDownCellPlaneAlpha);
            this.groupBoxShowUnitCell.Controls.Add(this.label17);
            this.groupBoxShowUnitCell.Controls.Add(this.label16);
            this.groupBoxShowUnitCell.Controls.Add(this.label9);
            this.groupBoxShowUnitCell.Controls.Add(this.label12);
            this.groupBoxShowUnitCell.Controls.Add(this.label8);
            this.groupBoxShowUnitCell.Controls.Add(this.label7);
            resources.ApplyResources(this.groupBoxShowUnitCell, "groupBoxShowUnitCell");
            this.groupBoxShowUnitCell.Name = "groupBoxShowUnitCell";
            this.groupBoxShowUnitCell.TabStop = false;
            this.toolTip.SetToolTip(this.groupBoxShowUnitCell, resources.GetString("groupBoxShowUnitCell.ToolTip"));
            // 
            // checkBoxShowSubCell
            // 
            resources.ApplyResources(this.checkBoxShowSubCell, "checkBoxShowSubCell");
            this.checkBoxShowSubCell.BackColor = System.Drawing.SystemColors.Control;
            this.checkBoxShowSubCell.Name = "checkBoxShowSubCell";
            this.checkBoxShowSubCell.UseVisualStyleBackColor = false;
            this.checkBoxShowSubCell.CheckedChanged += new System.EventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // checkBoxCellShowEdge
            // 
            resources.ApplyResources(this.checkBoxCellShowEdge, "checkBoxCellShowEdge");
            this.checkBoxCellShowEdge.Checked = true;
            this.checkBoxCellShowEdge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCellShowEdge.Name = "checkBoxCellShowEdge";
            this.toolTip.SetToolTip(this.checkBoxCellShowEdge, resources.GetString("checkBoxCellShowEdge.ToolTip"));
            this.checkBoxCellShowEdge.UseVisualStyleBackColor = true;
            this.checkBoxCellShowEdge.CheckedChanged += new System.EventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // pictureBoxCellPlaneColor
            // 
            this.pictureBoxCellPlaneColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pictureBoxCellPlaneColor, "pictureBoxCellPlaneColor");
            this.pictureBoxCellPlaneColor.Name = "pictureBoxCellPlaneColor";
            this.pictureBoxCellPlaneColor.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBoxCellPlaneColor, resources.GetString("pictureBoxCellPlaneColor.ToolTip"));
            this.pictureBoxCellPlaneColor.Click += new System.EventHandler(this.pictureBoxColor_Click);
            // 
            // pictureBoxCellEdgeColor
            // 
            this.pictureBoxCellEdgeColor.BackColor = System.Drawing.Color.Black;
            this.pictureBoxCellEdgeColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.pictureBoxCellEdgeColor, "pictureBoxCellEdgeColor");
            this.pictureBoxCellEdgeColor.Name = "pictureBoxCellEdgeColor";
            this.pictureBoxCellEdgeColor.TabStop = false;
            this.toolTip.SetToolTip(this.pictureBoxCellEdgeColor, resources.GetString("pictureBoxCellEdgeColor.ToolTip"));
            this.pictureBoxCellEdgeColor.Click += new System.EventHandler(this.pictureBoxColor_Click);
            // 
            // numericUpDownSubCellB
            // 
            resources.ApplyResources(this.numericUpDownSubCellB, "numericUpDownSubCellB");
            this.numericUpDownSubCellB.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSubCellB.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSubCellB.Name = "numericUpDownSubCellB";
            this.numericUpDownSubCellB.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSubCellB.ValueChanged += new System.EventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // checkBoxCellShowPlane
            // 
            resources.ApplyResources(this.checkBoxCellShowPlane, "checkBoxCellShowPlane");
            this.checkBoxCellShowPlane.Checked = true;
            this.checkBoxCellShowPlane.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCellShowPlane.Name = "checkBoxCellShowPlane";
            this.toolTip.SetToolTip(this.checkBoxCellShowPlane, resources.GetString("checkBoxCellShowPlane.ToolTip"));
            this.checkBoxCellShowPlane.UseVisualStyleBackColor = true;
            this.checkBoxCellShowPlane.CheckedChanged += new System.EventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // numericUpDownSubCellC
            // 
            resources.ApplyResources(this.numericUpDownSubCellC, "numericUpDownSubCellC");
            this.numericUpDownSubCellC.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSubCellC.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSubCellC.Name = "numericUpDownSubCellC";
            this.numericUpDownSubCellC.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSubCellC.ValueChanged += new System.EventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // numericUpDownSubCellA
            // 
            resources.ApplyResources(this.numericUpDownSubCellA, "numericUpDownSubCellA");
            this.numericUpDownSubCellA.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSubCellA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSubCellA.Name = "numericUpDownSubCellA";
            this.numericUpDownSubCellA.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSubCellA.ValueChanged += new System.EventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // numericUpDownCellPlaneAlpha
            // 
            this.numericUpDownCellPlaneAlpha.DecimalPlaces = 1;
            resources.ApplyResources(this.numericUpDownCellPlaneAlpha, "numericUpDownCellPlaneAlpha");
            this.numericUpDownCellPlaneAlpha.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownCellPlaneAlpha.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCellPlaneAlpha.Name = "numericUpDownCellPlaneAlpha";
            this.toolTip.SetToolTip(this.numericUpDownCellPlaneAlpha, resources.GetString("numericUpDownCellPlaneAlpha.ToolTip"));
            this.numericUpDownCellPlaneAlpha.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numericUpDownCellPlaneAlpha.ValueChanged += new System.EventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            this.toolTip.SetToolTip(this.label26, resources.GetString("label26.ToolTip"));
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            this.toolTip.SetToolTip(this.label27, resources.GetString("label27.ToolTip"));
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            this.toolTip.SetToolTip(this.label23, resources.GetString("label23.ToolTip"));
            // 
            // textBoxInformation
            // 
            resources.ApplyResources(this.textBoxInformation, "textBoxInformation");
            this.textBoxInformation.HideSelection = false;
            this.textBoxInformation.Name = "textBoxInformation";
            this.textBoxInformation.ReadOnly = true;
            this.toolTip.SetToolTip(this.textBoxInformation, resources.GetString("textBoxInformation.ToolTip"));
            // 
            // printPreviewDialog1
            // 
            resources.ApplyResources(this.printPreviewDialog1, "printPreviewDialog1");
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // toolStrip2
            // 
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelStatusInitialization,
            this.toolStripLabelStatusRendering});
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip2.Stretch = true;
            // 
            // toolStripLabelStatusInitialization
            // 
            this.toolStripLabelStatusInitialization.Name = "toolStripLabelStatusInitialization";
            resources.ApplyResources(this.toolStripLabelStatusInitialization, "toolStripLabelStatusInitialization");
            // 
            // toolStripLabelStatusRendering
            // 
            this.toolStripLabelStatusRendering.Name = "toolStripLabelStatusRendering";
            resources.ApplyResources(this.toolStripLabelStatusRendering, "toolStripLabelStatusRendering");
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Atom";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Atom";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.glControlAxes);
            this.splitContainer1.Panel1.Controls.Add(this.glControlLight);
            this.splitContainer1.Panel1.Controls.Add(this.glControlMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl);
            // 
            // glControlAxes
            // 
            this.glControlAxes.AllowMouseRotation = false;
            this.glControlAxes.AllowMouseScaling = false;
            this.glControlAxes.AllowMouseTranslating = false;
            resources.ApplyResources(this.glControlAxes, "glControlAxes");
            this.glControlAxes.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControlAxes.DisablingOpenGL = false;
            this.glControlAxes.MaxHeight = 1;
            this.glControlAxes.MaxWidth = 1;
            this.glControlAxes.Name = "glControlAxes";
            this.glControlAxes.NodeCoefficient = 1;
            this.glControlAxes.ProjectionMode = Crystallography.OpenGL.GLControlAlpha.ProjectionModes.Orhographic;
            this.glControlAxes.ProjWidth = 4D;
            this.glControlAxes.RenderingTransparency = Crystallography.OpenGL.GLControlAlpha.RenderingTransparencyModes.Never;
            this.glControlAxes.RotationMode = Crystallography.OpenGL.GLControlAlpha.RotationModes.Object;
            this.glControlAxes.TranslatingMode = Crystallography.OpenGL.GLControlAlpha.TranslatingModes.View;
            this.glControlAxes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControlAxes_MouseMove);
            // 
            // glControlLight
            // 
            this.glControlLight.AllowMouseRotation = false;
            this.glControlLight.AllowMouseScaling = false;
            this.glControlLight.AllowMouseTranslating = false;
            resources.ApplyResources(this.glControlLight, "glControlLight");
            this.glControlLight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControlLight.DisablingOpenGL = false;
            this.glControlLight.MaxHeight = 1;
            this.glControlLight.MaxWidth = 1;
            this.glControlLight.Name = "glControlLight";
            this.glControlLight.NodeCoefficient = 1;
            this.glControlLight.ProjectionMode = Crystallography.OpenGL.GLControlAlpha.ProjectionModes.Orhographic;
            this.glControlLight.ProjWidth = 4D;
            this.glControlLight.RenderingTransparency = Crystallography.OpenGL.GLControlAlpha.RenderingTransparencyModes.Never;
            this.glControlLight.RotationMode = Crystallography.OpenGL.GLControlAlpha.RotationModes.Object;
            this.glControlLight.TranslatingMode = Crystallography.OpenGL.GLControlAlpha.TranslatingModes.View;
            this.glControlLight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControlLight_MouseMove);
            // 
            // glControlMain
            // 
            this.glControlMain.AllowMouseRotation = false;
            this.glControlMain.AllowMouseScaling = true;
            this.glControlMain.AllowMouseTranslating = true;
            resources.ApplyResources(this.glControlMain, "glControlMain");
            this.glControlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControlMain.DisablingOpenGL = false;
            this.glControlMain.MaxHeight = 1440;
            this.glControlMain.MaxWidth = 2560;
            this.glControlMain.Name = "glControlMain";
            this.glControlMain.NodeCoefficient = 8;
            this.glControlMain.ProjectionMode = Crystallography.OpenGL.GLControlAlpha.ProjectionModes.Orhographic;
            this.glControlMain.ProjWidth = 4D;
            this.glControlMain.RenderingTransparency = Crystallography.OpenGL.GLControlAlpha.RenderingTransparencyModes.Always;
            this.glControlMain.RotationMode = Crystallography.OpenGL.GLControlAlpha.RotationModes.Object;
            this.glControlMain.TranslatingMode = Crystallography.OpenGL.GLControlAlpha.TranslatingModes.View;
            this.glControlMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseDown);
            this.glControlMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControlMain_MouseMove);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCoordinateInfromatin);
            this.tabControl.Controls.Add(this.tabPageBounds);
            this.tabControl.Controls.Add(this.tabPageUnitCell);
            this.tabControl.Controls.Add(this.tabPageLatticePlane);
            this.tabControl.Controls.Add(this.tabPageText);
            this.tabControl.Controls.Add(this.tabPageAtom);
            this.tabControl.Controls.Add(this.tabPageBond);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.HotTrack = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            this.tabControl.VisibleChanged += new System.EventHandler(this.tabControl_VisibleChanged);
            // 
            // tabPageCoordinateInfromatin
            // 
            this.tabPageCoordinateInfromatin.Controls.Add(this.atomCoordinateTable1);
            resources.ApplyResources(this.tabPageCoordinateInfromatin, "tabPageCoordinateInfromatin");
            this.tabPageCoordinateInfromatin.Name = "tabPageCoordinateInfromatin";
            // 
            // tabPageBounds
            // 
            this.tabPageBounds.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageBounds.Controls.Add(this.radioButton2);
            this.tabPageBounds.Controls.Add(this.tabControl1);
            this.tabPageBounds.Controls.Add(this.radioButton1);
            this.tabPageBounds.Controls.Add(this.checkBoxHideAllAtoms);
            this.tabPageBounds.Controls.Add(this.checkBoxClipObjects);
            this.tabPageBounds.Controls.Add(this.checkBoxShowBoundPlanes);
            this.tabPageBounds.Controls.Add(this.numericBoxBoundPlanesOpacity);
            resources.ApplyResources(this.tabPageBounds, "tabPageBounds");
            this.tabPageBounds.Name = "tabPageBounds";
            // 
            // radioButton2
            // 
            resources.ApplyResources(this.radioButton2, "radioButton2");
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label29);
            this.tabPage2.Controls.Add(this.label24);
            this.tabPage2.Controls.Add(this.label45);
            this.tabPage2.Controls.Add(this.label44);
            this.tabPage2.Controls.Add(this.label33);
            this.tabPage2.Controls.Add(this.label32);
            this.tabPage2.Controls.Add(this.label31);
            this.tabPage2.Controls.Add(this.label22);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.numericBox6);
            this.tabPage2.Controls.Add(this.numericBox4);
            this.tabPage2.Controls.Add(this.numericBox2);
            this.tabPage2.Controls.Add(this.numericBox5);
            this.tabPage2.Controls.Add(this.numericBox3);
            this.tabPage2.Controls.Add(this.numericBox1);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // label45
            // 
            resources.ApplyResources(this.label45, "label45");
            this.label45.Name = "label45";
            // 
            // label44
            // 
            resources.ApplyResources(this.label44, "label44");
            this.label44.Name = "label44";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label28);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.flowLayoutPanelBounds);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.label30);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.AddBoundary);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label19);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // flowLayoutPanelBounds
            // 
            resources.ApplyResources(this.flowLayoutPanelBounds, "flowLayoutPanelBounds");
            this.flowLayoutPanelBounds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelBounds.Controls.Add(this.boundsControl1);
            this.flowLayoutPanelBounds.Name = "flowLayoutPanelBounds";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // AddBoundary
            // 
            resources.ApplyResources(this.AddBoundary, "AddBoundary");
            this.AddBoundary.BackColor = System.Drawing.Color.SteelBlue;
            this.AddBoundary.ForeColor = System.Drawing.Color.White;
            this.AddBoundary.Name = "AddBoundary";
            this.AddBoundary.UseVisualStyleBackColor = false;
            this.AddBoundary.Click += new System.EventHandler(this.AddBoundary_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // radioButton1
            // 
            resources.ApplyResources(this.radioButton1, "radioButton1");
            this.radioButton1.Checked = true;
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // checkBoxHideAllAtoms
            // 
            resources.ApplyResources(this.checkBoxHideAllAtoms, "checkBoxHideAllAtoms");
            this.checkBoxHideAllAtoms.Name = "checkBoxHideAllAtoms";
            this.checkBoxHideAllAtoms.UseVisualStyleBackColor = true;
            this.checkBoxHideAllAtoms.CheckedChanged += new System.EventHandler(this.checkBoxShowBoundPlanes_CheckedChanged);
            // 
            // checkBoxClipObjects
            // 
            resources.ApplyResources(this.checkBoxClipObjects, "checkBoxClipObjects");
            this.checkBoxClipObjects.Name = "checkBoxClipObjects";
            this.checkBoxClipObjects.UseVisualStyleBackColor = true;
            this.checkBoxClipObjects.CheckedChanged += new System.EventHandler(this.checkBoxShowBoundPlanes_CheckedChanged);
            // 
            // checkBoxShowBoundPlanes
            // 
            resources.ApplyResources(this.checkBoxShowBoundPlanes, "checkBoxShowBoundPlanes");
            this.checkBoxShowBoundPlanes.Name = "checkBoxShowBoundPlanes";
            this.checkBoxShowBoundPlanes.UseVisualStyleBackColor = true;
            this.checkBoxShowBoundPlanes.CheckedChanged += new System.EventHandler(this.checkBoxShowBoundPlanes_CheckedChanged);
            // 
            // tabPageUnitCell
            // 
            this.tabPageUnitCell.BackColor = System.Drawing.Color.Transparent;
            this.tabPageUnitCell.Controls.Add(this.checkBoxUnitCell);
            this.tabPageUnitCell.Controls.Add(this.groupBoxShowUnitCell);
            resources.ApplyResources(this.tabPageUnitCell, "tabPageUnitCell");
            this.tabPageUnitCell.Name = "tabPageUnitCell";
            this.tabPageUnitCell.UseVisualStyleBackColor = true;
            // 
            // tabPageLatticePlane
            // 
            this.tabPageLatticePlane.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageLatticePlane.Controls.Add(this.flowLayoutPanelLatticePlanes);
            this.tabPageLatticePlane.Controls.Add(this.buttonAddLatticePlane);
            this.tabPageLatticePlane.Controls.Add(this.label26);
            this.tabPageLatticePlane.Controls.Add(this.label27);
            this.tabPageLatticePlane.Controls.Add(this.label3);
            this.tabPageLatticePlane.Controls.Add(this.label25);
            this.tabPageLatticePlane.Controls.Add(this.label23);
            this.tabPageLatticePlane.Controls.Add(this.label4);
            this.tabPageLatticePlane.Controls.Add(this.numericBoxLatticePlaneOpacity);
            resources.ApplyResources(this.tabPageLatticePlane, "tabPageLatticePlane");
            this.tabPageLatticePlane.Name = "tabPageLatticePlane";
            // 
            // flowLayoutPanelLatticePlanes
            // 
            resources.ApplyResources(this.flowLayoutPanelLatticePlanes, "flowLayoutPanelLatticePlanes");
            this.flowLayoutPanelLatticePlanes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelLatticePlanes.Name = "flowLayoutPanelLatticePlanes";
            // 
            // buttonAddLatticePlane
            // 
            resources.ApplyResources(this.buttonAddLatticePlane, "buttonAddLatticePlane");
            this.buttonAddLatticePlane.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAddLatticePlane.ForeColor = System.Drawing.Color.White;
            this.buttonAddLatticePlane.Name = "buttonAddLatticePlane";
            this.buttonAddLatticePlane.UseVisualStyleBackColor = false;
            this.buttonAddLatticePlane.Click += new System.EventHandler(this.AddLatticePlane_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // tabPageText
            // 
            this.tabPageText.Controls.Add(this.textBoxInformation);
            resources.ApplyResources(this.tabPageText, "tabPageText");
            this.tabPageText.Name = "tabPageText";
            this.tabPageText.UseVisualStyleBackColor = true;
            // 
            // tabPageAtom
            // 
            resources.ApplyResources(this.tabPageAtom, "tabPageAtom");
            this.tabPageAtom.Name = "tabPageAtom";
            // 
            // tabPageBond
            // 
            this.tabPageBond.Controls.Add(this.labelMessage);
            resources.ApplyResources(this.tabPageBond, "tabPageBond");
            this.tabPageBond.Name = "tabPageBond";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Atom";
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Atom";
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // tabPageCrystal
            // 
            resources.ApplyResources(this.tabPageCrystal, "tabPageCrystal");
            this.tabPageCrystal.Name = "tabPageCrystal";
            // 
            // atomCoordinateTable1
            // 
            this.atomCoordinateTable1.Atom = null;
            this.atomCoordinateTable1.Crystal = null;
            resources.ApplyResources(this.atomCoordinateTable1, "atomCoordinateTable1");
            this.atomCoordinateTable1.Name = "atomCoordinateTable1";
            // 
            // numericBox6
            // 
            this.numericBox6.AllowMouseControl = false;
            resources.ApplyResources(this.numericBox6, "numericBox6");
            this.numericBox6.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox6.DecimalPlaces = 1;
            this.numericBox6.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox6.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox6.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox6.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox6.Maximum = 10D;
            this.numericBox6.Minimum = -10D;
            this.numericBox6.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBox6.MouseSpeed = 1D;
            this.numericBox6.Multiline = false;
            this.numericBox6.Name = "numericBox6";
            this.numericBox6.RadianValue = 0.0087266462599716477D;
            this.numericBox6.ReadOnly = false;
            this.numericBox6.RestrictLimitValue = true;
            this.numericBox6.ShowFraction = false;
            this.numericBox6.ShowPositiveSign = false;
            this.numericBox6.ShowUpDown = false;
            this.numericBox6.SkipEventDuringInput = false;
            this.numericBox6.SmartIncrement = true;
            this.numericBox6.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBox6.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBox6.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBox6.ThonsandsSeparator = true;
            this.numericBox6.UpDown_Increment = 0.1D;
            this.numericBox6.Value = 0.5D;
            this.numericBox6.WordWrap = true;
            // 
            // numericBox4
            // 
            this.numericBox4.AllowMouseControl = false;
            resources.ApplyResources(this.numericBox4, "numericBox4");
            this.numericBox4.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox4.DecimalPlaces = 1;
            this.numericBox4.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox4.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox4.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox4.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox4.Maximum = 10D;
            this.numericBox4.Minimum = -10D;
            this.numericBox4.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBox4.MouseSpeed = 1D;
            this.numericBox4.Multiline = false;
            this.numericBox4.Name = "numericBox4";
            this.numericBox4.RadianValue = 0.0087266462599716477D;
            this.numericBox4.ReadOnly = false;
            this.numericBox4.RestrictLimitValue = true;
            this.numericBox4.ShowFraction = false;
            this.numericBox4.ShowPositiveSign = false;
            this.numericBox4.ShowUpDown = false;
            this.numericBox4.SkipEventDuringInput = false;
            this.numericBox4.SmartIncrement = true;
            this.numericBox4.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBox4.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBox4.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBox4.ThonsandsSeparator = true;
            this.numericBox4.UpDown_Increment = 0.1D;
            this.numericBox4.Value = 0.5D;
            this.numericBox4.WordWrap = true;
            // 
            // numericBox2
            // 
            this.numericBox2.AllowMouseControl = false;
            resources.ApplyResources(this.numericBox2, "numericBox2");
            this.numericBox2.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox2.DecimalPlaces = 1;
            this.numericBox2.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox2.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox2.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox2.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox2.Maximum = 10D;
            this.numericBox2.Minimum = -10D;
            this.numericBox2.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBox2.MouseSpeed = 1D;
            this.numericBox2.Multiline = false;
            this.numericBox2.Name = "numericBox2";
            this.numericBox2.RadianValue = 0.0087266462599716477D;
            this.numericBox2.ReadOnly = false;
            this.numericBox2.RestrictLimitValue = true;
            this.numericBox2.ShowFraction = false;
            this.numericBox2.ShowPositiveSign = false;
            this.numericBox2.ShowUpDown = false;
            this.numericBox2.SkipEventDuringInput = false;
            this.numericBox2.SmartIncrement = true;
            this.numericBox2.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBox2.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBox2.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBox2.ThonsandsSeparator = true;
            this.numericBox2.UpDown_Increment = 0.1D;
            this.numericBox2.Value = 0.5D;
            this.numericBox2.WordWrap = true;
            // 
            // numericBox5
            // 
            this.numericBox5.AllowMouseControl = false;
            resources.ApplyResources(this.numericBox5, "numericBox5");
            this.numericBox5.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox5.DecimalPlaces = 1;
            this.numericBox5.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox5.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox5.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox5.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox5.Maximum = 10D;
            this.numericBox5.Minimum = -10D;
            this.numericBox5.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBox5.MouseSpeed = 1D;
            this.numericBox5.Multiline = false;
            this.numericBox5.Name = "numericBox5";
            this.numericBox5.RadianValue = -0.0087266462599716477D;
            this.numericBox5.ReadOnly = false;
            this.numericBox5.RestrictLimitValue = true;
            this.numericBox5.ShowFraction = false;
            this.numericBox5.ShowPositiveSign = false;
            this.numericBox5.ShowUpDown = false;
            this.numericBox5.SkipEventDuringInput = false;
            this.numericBox5.SmartIncrement = true;
            this.numericBox5.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBox5.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBox5.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBox5.ThonsandsSeparator = true;
            this.numericBox5.UpDown_Increment = 0.1D;
            this.numericBox5.Value = -0.5D;
            this.numericBox5.WordWrap = true;
            // 
            // numericBox3
            // 
            this.numericBox3.AllowMouseControl = false;
            resources.ApplyResources(this.numericBox3, "numericBox3");
            this.numericBox3.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox3.DecimalPlaces = 1;
            this.numericBox3.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox3.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox3.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox3.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox3.Maximum = 10D;
            this.numericBox3.Minimum = -10D;
            this.numericBox3.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBox3.MouseSpeed = 1D;
            this.numericBox3.Multiline = false;
            this.numericBox3.Name = "numericBox3";
            this.numericBox3.RadianValue = -0.0087266462599716477D;
            this.numericBox3.ReadOnly = false;
            this.numericBox3.RestrictLimitValue = true;
            this.numericBox3.ShowFraction = false;
            this.numericBox3.ShowPositiveSign = false;
            this.numericBox3.ShowUpDown = false;
            this.numericBox3.SkipEventDuringInput = false;
            this.numericBox3.SmartIncrement = true;
            this.numericBox3.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBox3.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBox3.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBox3.ThonsandsSeparator = true;
            this.numericBox3.UpDown_Increment = 0.1D;
            this.numericBox3.Value = -0.5D;
            this.numericBox3.WordWrap = true;
            // 
            // numericBox1
            // 
            this.numericBox1.AllowMouseControl = false;
            resources.ApplyResources(this.numericBox1, "numericBox1");
            this.numericBox1.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox1.DecimalPlaces = 1;
            this.numericBox1.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBox1.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox1.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBox1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBox1.Maximum = 10D;
            this.numericBox1.Minimum = -10D;
            this.numericBox1.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBox1.MouseSpeed = 1D;
            this.numericBox1.Multiline = false;
            this.numericBox1.Name = "numericBox1";
            this.numericBox1.RadianValue = -0.0087266462599716477D;
            this.numericBox1.ReadOnly = false;
            this.numericBox1.RestrictLimitValue = true;
            this.numericBox1.ShowFraction = false;
            this.numericBox1.ShowPositiveSign = false;
            this.numericBox1.ShowUpDown = false;
            this.numericBox1.SkipEventDuringInput = false;
            this.numericBox1.SmartIncrement = true;
            this.numericBox1.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBox1.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBox1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBox1.ThonsandsSeparator = true;
            this.numericBox1.UpDown_Increment = 0.1D;
            this.numericBox1.Value = -0.5D;
            this.numericBox1.WordWrap = true;
            // 
            // numericBoxBoundPlanesOpacity
            // 
            this.numericBoxBoundPlanesOpacity.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxBoundPlanesOpacity, "numericBoxBoundPlanesOpacity");
            this.numericBoxBoundPlanesOpacity.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBoundPlanesOpacity.DecimalPlaces = 1;
            this.numericBoxBoundPlanesOpacity.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBoundPlanesOpacity.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBoundPlanesOpacity.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBoundPlanesOpacity.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBoundPlanesOpacity.Maximum = 1D;
            this.numericBoxBoundPlanesOpacity.Minimum = 0D;
            this.numericBoxBoundPlanesOpacity.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxBoundPlanesOpacity.MouseSpeed = 1D;
            this.numericBoxBoundPlanesOpacity.Multiline = false;
            this.numericBoxBoundPlanesOpacity.Name = "numericBoxBoundPlanesOpacity";
            this.numericBoxBoundPlanesOpacity.RadianValue = 0.012217304763960307D;
            this.numericBoxBoundPlanesOpacity.ReadOnly = false;
            this.numericBoxBoundPlanesOpacity.RestrictLimitValue = true;
            this.numericBoxBoundPlanesOpacity.ShowFraction = false;
            this.numericBoxBoundPlanesOpacity.ShowPositiveSign = false;
            this.numericBoxBoundPlanesOpacity.ShowUpDown = false;
            this.numericBoxBoundPlanesOpacity.SkipEventDuringInput = false;
            this.numericBoxBoundPlanesOpacity.SmartIncrement = false;
            this.numericBoxBoundPlanesOpacity.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBoundPlanesOpacity.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBoundPlanesOpacity.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBoundPlanesOpacity.ThonsandsSeparator = true;
            this.numericBoxBoundPlanesOpacity.UpDown_Increment = 0.1D;
            this.numericBoxBoundPlanesOpacity.Value = 0.7D;
            this.numericBoxBoundPlanesOpacity.WordWrap = true;
            this.numericBoxBoundPlanesOpacity.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.checkBoxShowBoundPlanes_CheckedChanged);
            // 
            // numericBoxCellTransrationC
            // 
            this.numericBoxCellTransrationC.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxCellTransrationC, "numericBoxCellTransrationC");
            this.numericBoxCellTransrationC.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellTransrationC.DecimalPlaces = 2;
            this.numericBoxCellTransrationC.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellTransrationC.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCellTransrationC.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellTransrationC.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCellTransrationC.Maximum = 10D;
            this.numericBoxCellTransrationC.Minimum = -10D;
            this.numericBoxCellTransrationC.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxCellTransrationC.MouseSpeed = 1D;
            this.numericBoxCellTransrationC.Multiline = false;
            this.numericBoxCellTransrationC.Name = "numericBoxCellTransrationC";
            this.numericBoxCellTransrationC.RadianValue = 0D;
            this.numericBoxCellTransrationC.ReadOnly = false;
            this.numericBoxCellTransrationC.RestrictLimitValue = true;
            this.numericBoxCellTransrationC.ShowFraction = false;
            this.numericBoxCellTransrationC.ShowPositiveSign = false;
            this.numericBoxCellTransrationC.ShowUpDown = false;
            this.numericBoxCellTransrationC.SkipEventDuringInput = true;
            this.numericBoxCellTransrationC.SmartIncrement = true;
            this.numericBoxCellTransrationC.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCellTransrationC.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCellTransrationC.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellTransrationC.ThonsandsSeparator = true;
            this.numericBoxCellTransrationC.UpDown_Increment = 0.1D;
            this.numericBoxCellTransrationC.Value = 0D;
            this.numericBoxCellTransrationC.WordWrap = true;
            this.numericBoxCellTransrationC.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // numericBoxCellTransrationB
            // 
            this.numericBoxCellTransrationB.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxCellTransrationB, "numericBoxCellTransrationB");
            this.numericBoxCellTransrationB.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellTransrationB.DecimalPlaces = 2;
            this.numericBoxCellTransrationB.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellTransrationB.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCellTransrationB.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellTransrationB.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCellTransrationB.Maximum = 10D;
            this.numericBoxCellTransrationB.Minimum = -10D;
            this.numericBoxCellTransrationB.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxCellTransrationB.MouseSpeed = 1D;
            this.numericBoxCellTransrationB.Multiline = false;
            this.numericBoxCellTransrationB.Name = "numericBoxCellTransrationB";
            this.numericBoxCellTransrationB.RadianValue = 0D;
            this.numericBoxCellTransrationB.ReadOnly = false;
            this.numericBoxCellTransrationB.RestrictLimitValue = true;
            this.numericBoxCellTransrationB.ShowFraction = false;
            this.numericBoxCellTransrationB.ShowPositiveSign = false;
            this.numericBoxCellTransrationB.ShowUpDown = false;
            this.numericBoxCellTransrationB.SkipEventDuringInput = true;
            this.numericBoxCellTransrationB.SmartIncrement = true;
            this.numericBoxCellTransrationB.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCellTransrationB.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCellTransrationB.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellTransrationB.ThonsandsSeparator = true;
            this.numericBoxCellTransrationB.UpDown_Increment = 0.1D;
            this.numericBoxCellTransrationB.Value = 0D;
            this.numericBoxCellTransrationB.WordWrap = true;
            this.numericBoxCellTransrationB.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // numericBoxCellTransrationA
            // 
            this.numericBoxCellTransrationA.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxCellTransrationA, "numericBoxCellTransrationA");
            this.numericBoxCellTransrationA.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellTransrationA.DecimalPlaces = 2;
            this.numericBoxCellTransrationA.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellTransrationA.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCellTransrationA.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellTransrationA.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCellTransrationA.Maximum = 10D;
            this.numericBoxCellTransrationA.Minimum = -10D;
            this.numericBoxCellTransrationA.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxCellTransrationA.MouseSpeed = 1D;
            this.numericBoxCellTransrationA.Multiline = false;
            this.numericBoxCellTransrationA.Name = "numericBoxCellTransrationA";
            this.numericBoxCellTransrationA.RadianValue = 0D;
            this.numericBoxCellTransrationA.ReadOnly = false;
            this.numericBoxCellTransrationA.RestrictLimitValue = true;
            this.numericBoxCellTransrationA.ShowFraction = false;
            this.numericBoxCellTransrationA.ShowPositiveSign = false;
            this.numericBoxCellTransrationA.ShowUpDown = false;
            this.numericBoxCellTransrationA.SkipEventDuringInput = true;
            this.numericBoxCellTransrationA.SmartIncrement = true;
            this.numericBoxCellTransrationA.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCellTransrationA.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCellTransrationA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellTransrationA.ThonsandsSeparator = true;
            this.numericBoxCellTransrationA.UpDown_Increment = 0.1D;
            this.numericBoxCellTransrationA.Value = 0D;
            this.numericBoxCellTransrationA.WordWrap = true;
            this.numericBoxCellTransrationA.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // numericBoxLatticePlaneOpacity
            // 
            this.numericBoxLatticePlaneOpacity.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxLatticePlaneOpacity, "numericBoxLatticePlaneOpacity");
            this.numericBoxLatticePlaneOpacity.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxLatticePlaneOpacity.DecimalPlaces = -2;
            this.numericBoxLatticePlaneOpacity.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxLatticePlaneOpacity.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxLatticePlaneOpacity.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxLatticePlaneOpacity.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxLatticePlaneOpacity.Maximum = 1D;
            this.numericBoxLatticePlaneOpacity.Minimum = 0D;
            this.numericBoxLatticePlaneOpacity.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxLatticePlaneOpacity.MouseSpeed = 1D;
            this.numericBoxLatticePlaneOpacity.Multiline = false;
            this.numericBoxLatticePlaneOpacity.Name = "numericBoxLatticePlaneOpacity";
            this.numericBoxLatticePlaneOpacity.RadianValue = 0.012217304763960307D;
            this.numericBoxLatticePlaneOpacity.ReadOnly = false;
            this.numericBoxLatticePlaneOpacity.RestrictLimitValue = true;
            this.numericBoxLatticePlaneOpacity.ShowFraction = false;
            this.numericBoxLatticePlaneOpacity.ShowPositiveSign = false;
            this.numericBoxLatticePlaneOpacity.ShowUpDown = false;
            this.numericBoxLatticePlaneOpacity.SkipEventDuringInput = false;
            this.numericBoxLatticePlaneOpacity.SmartIncrement = true;
            this.numericBoxLatticePlaneOpacity.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxLatticePlaneOpacity.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxLatticePlaneOpacity.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxLatticePlaneOpacity.ThonsandsSeparator = true;
            this.numericBoxLatticePlaneOpacity.UpDown_Increment = 0.1D;
            this.numericBoxLatticePlaneOpacity.Value = 0.7D;
            this.numericBoxLatticePlaneOpacity.WordWrap = true;
            this.numericBoxLatticePlaneOpacity.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.LatticePlanes_Changed);
            // 
            // labelMessage
            // 
            resources.ApplyResources(this.labelMessage, "labelMessage");
            this.labelMessage.ForeColor = System.Drawing.Color.Red;
            this.labelMessage.Name = "labelMessage";
            // 
            // boundsControl1
            // 
            resources.ApplyResources(this.boundsControl1, "boundsControl1");
            this.boundsControl1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.boundsControl1.Crystal = null;
            this.boundsControl1.Distance = 0.5D;
            this.boundsControl1.Equivalency = true;
            this.boundsControl1.FullOption = false;
            this.boundsControl1.Name = "boundsControl1";
            // 
            // FormStructureViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormStructureViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStructureViewer_FormClosing);
            this.Load += new System.EventHandler(this.FormStructureViewer_Load);
            this.VisibleChanged += new System.EventHandler(this.FormStructureViewer_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormStructureViewer_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxShowUnitCell.ResumeLayout(false);
            this.groupBoxShowUnitCell.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellPlaneColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellEdgeColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCellPlaneAlpha)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageCoordinateInfromatin.ResumeLayout(false);
            this.tabPageBounds.ResumeLayout(false);
            this.tabPageBounds.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.flowLayoutPanelBounds.ResumeLayout(false);
            this.flowLayoutPanelBounds.PerformLayout();
            this.tabPageUnitCell.ResumeLayout(false);
            this.tabPageUnitCell.PerformLayout();
            this.tabPageLatticePlane.ResumeLayout(false);
            this.tabPageLatticePlane.PerformLayout();
            this.tabPageText.ResumeLayout(false);
            this.tabPageText.PerformLayout();
            this.tabPageBond.ResumeLayout(false);
            this.tabPageBond.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxShowUnitCell;
        private System.Windows.Forms.CheckBox checkBoxUnitCell;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBoxCellPlaneColor;
        private System.Windows.Forms.PictureBox pictureBoxCellEdgeColor;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDownCellPlaneAlpha;
        private System.Windows.Forms.CheckBox checkBoxCellShowPlane;
        private System.Windows.Forms.CheckBox checkBoxCellShowEdge;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxInformation;
        private System.Windows.Forms.Label label25;
        public System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageLatticePlane;
        private System.Windows.Forms.TabPage tabPageUnitCell;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem pageSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPerviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.CheckBox checkBoxShowSubCell;
        private System.Windows.Forms.NumericUpDown numericUpDownSubCellB;
        private System.Windows.Forms.NumericUpDown numericUpDownSubCellC;
        private System.Windows.Forms.NumericUpDown numericUpDownSubCellA;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Crystallography.Controls.AtomCoordinateTable atomCoordinateTable1;
        private System.Windows.Forms.ToolStripMenuItem saveMainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLegendToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAxesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyMainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyLegendToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyAxesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyLightToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonBoost;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonCrystalAxes;
        private System.Windows.Forms.ToolStripButton toolStripButtonLightDirection;
        private System.Windows.Forms.ToolStripButton toolStripButtonLegend;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private Crystallography.OpenGL.GLControlAlpha glControlMain;
        private Crystallography.OpenGL.GLControlAlpha glControlAxes;
        private Crystallography.Controls.NumericBox numericBoxCellTransrationC;
        private Crystallography.Controls.NumericBox numericBoxCellTransrationB;
        private Crystallography.Controls.NumericBox numericBoxCellTransrationA;
        private Crystallography.OpenGL.GLControlAlpha glControlLight;
        private System.Windows.Forms.ToolStripLabel toolStripLabelStatusInitialization;
        private System.Windows.Forms.ToolStripLabel toolStripLabelStatusRendering;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBounds;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button AddBoundary;
        private System.Windows.Forms.CheckBox checkBoxClipObjects;
        private System.Windows.Forms.CheckBox checkBoxShowBoundPlanes;
        private Crystallography.Controls.NumericBox numericBoxBoundPlanesOpacity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageBounds;
        private Crystallography.Controls.NumericBox numericBoxLatticePlaneOpacity;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLatticePlanes;
        private System.Windows.Forms.Button buttonAddLatticePlane;
        private System.Windows.Forms.CheckBox checkBoxHideAllAtoms;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TabPage tabPageCoordinateInfromatin;
        private System.Windows.Forms.TabPage tabPageText;
        private System.Windows.Forms.TabPage tabPageAtom;
        private System.Windows.Forms.TabPage tabPageBond;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private BoundsControl boundsControl1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private Crystallography.Controls.NumericBox numericBox6;
        private Crystallography.Controls.NumericBox numericBox4;
        private Crystallography.Controls.NumericBox numericBox2;
        private Crystallography.Controls.NumericBox numericBox5;
        private Crystallography.Controls.NumericBox numericBox3;
        private Crystallography.Controls.NumericBox numericBox1;
        private System.Windows.Forms.BindingSource bindingSourceAtoms;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.TabPage tabPageCrystal;
        private System.Windows.Forms.Label labelMessage;
    }
}