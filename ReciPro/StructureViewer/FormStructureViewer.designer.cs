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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBoxHideAllAtoms = new System.Windows.Forms.CheckBox();
            this.checkBoxClipObjects = new System.Windows.Forms.CheckBox();
            this.numericBoxBoundPlanesOpacity = new Crystallography.Controls.NumericBox();
            this.flowLayoutPanelBounds = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxShowBoundPlanes = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddBoundary = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxUnitCell = new System.Windows.Forms.CheckBox();
            this.groupBoxShowUnitCell = new System.Windows.Forms.GroupBox();
            this.numericBoxCellTransrationC = new Crystallography.Controls.NumericBox();
            this.numericBoxCellTransrationB = new Crystallography.Controls.NumericBox();
            this.numericBoxCellTransrationA = new Crystallography.Controls.NumericBox();
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanelLatticePlanes = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAddLatticePlane = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericBoxLatticePlaneOpacity = new Crystallography.Controls.NumericBox();
            this.glControlAxes = new Crystallography.OpenGL.GLControlAlpha();
            this.glControlLight = new Crystallography.OpenGL.GLControlAlpha();
            this.glControlMain = new Crystallography.OpenGL.GLControlAlpha();
            this.textBoxInformation = new System.Windows.Forms.TextBox();
            this.atomCoordinateTable1 = new Crystallography.Controls.AtomCoordinateTable();
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
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelStatusInitialization = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelStatusRendering = new System.Windows.Forms.ToolStripLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxShowUnitCell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellPlaneColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellEdgeColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCellPlaneAlpha)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl);
            this.splitContainer1.Panel1.Controls.Add(this.glControlAxes);
            this.splitContainer1.Panel1.Controls.Add(this.glControlLight);
            this.splitContainer1.Panel1.Controls.Add(this.glControlMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxInformation);
            this.splitContainer1.Panel2.Controls.Add(this.atomCoordinateTable1);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.HotTrack = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.checkBoxHideAllAtoms);
            this.tabPage3.Controls.Add(this.checkBoxClipObjects);
            this.tabPage3.Controls.Add(this.numericBoxBoundPlanesOpacity);
            this.tabPage3.Controls.Add(this.flowLayoutPanelBounds);
            this.tabPage3.Controls.Add(this.checkBoxShowBoundPlanes);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.AddBoundary);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label30);
            this.tabPage3.Controls.Add(this.label28);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
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
            // numericBoxBoundPlanesOpacity
            // 
            this.numericBoxBoundPlanesOpacity.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxBoundPlanesOpacity, "numericBoxBoundPlanesOpacity");
            this.numericBoxBoundPlanesOpacity.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBoundPlanesOpacity.DecimalPlaces = 1;
            this.numericBoxBoundPlanesOpacity.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBoundPlanesOpacity.FooterText = "";
            this.numericBoxBoundPlanesOpacity.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBoundPlanesOpacity.HeaderText = "Opacity";
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
            this.numericBoxBoundPlanesOpacity.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxBoundPlanesOpacity.ThonsandsSeparator = true;
            this.numericBoxBoundPlanesOpacity.ToolTip = "";
            this.numericBoxBoundPlanesOpacity.UpDown_Increment = 0.1D;
            this.numericBoxBoundPlanesOpacity.Value = 0.7D;
            this.numericBoxBoundPlanesOpacity.WordWrap = true;
            this.numericBoxBoundPlanesOpacity.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.checkBoxShowBoundPlanes_CheckedChanged);
            // 
            // flowLayoutPanelBounds
            // 
            resources.ApplyResources(this.flowLayoutPanelBounds, "flowLayoutPanelBounds");
            this.flowLayoutPanelBounds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelBounds.Name = "flowLayoutPanelBounds";
            // 
            // checkBoxShowBoundPlanes
            // 
            resources.ApplyResources(this.checkBoxShowBoundPlanes, "checkBoxShowBoundPlanes");
            this.checkBoxShowBoundPlanes.Name = "checkBoxShowBoundPlanes";
            this.checkBoxShowBoundPlanes.UseVisualStyleBackColor = true;
            this.checkBoxShowBoundPlanes.CheckedChanged += new System.EventHandler(this.checkBoxShowBoundPlanes_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.checkBoxUnitCell);
            this.tabPage1.Controls.Add(this.groupBoxShowUnitCell);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            // numericBoxCellTransrationC
            // 
            this.numericBoxCellTransrationC.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxCellTransrationC, "numericBoxCellTransrationC");
            this.numericBoxCellTransrationC.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCellTransrationC.DecimalPlaces = 2;
            this.numericBoxCellTransrationC.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellTransrationC.FooterText = "";
            this.numericBoxCellTransrationC.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellTransrationC.HeaderText = "";
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
            this.numericBoxCellTransrationC.ToolTip = "";
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
            this.numericBoxCellTransrationB.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellTransrationB.FooterText = "";
            this.numericBoxCellTransrationB.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellTransrationB.HeaderText = "";
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
            this.numericBoxCellTransrationB.ToolTip = "";
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
            this.numericBoxCellTransrationA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellTransrationA.FooterText = "";
            this.numericBoxCellTransrationA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellTransrationA.HeaderText = "";
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
            this.numericBoxCellTransrationA.ToolTip = "";
            this.numericBoxCellTransrationA.UpDown_Increment = 0.1D;
            this.numericBoxCellTransrationA.Value = 0D;
            this.numericBoxCellTransrationA.WordWrap = true;
            this.numericBoxCellTransrationA.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.checkBoxShowUnitCell_CheckedChanged);
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
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.flowLayoutPanelLatticePlanes);
            this.tabPage2.Controls.Add(this.buttonAddLatticePlane);
            this.tabPage2.Controls.Add(this.label26);
            this.tabPage2.Controls.Add(this.label27);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label25);
            this.tabPage2.Controls.Add(this.label23);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.numericBoxLatticePlaneOpacity);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
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
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            this.toolTip.SetToolTip(this.label23, resources.GetString("label23.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // numericBoxLatticePlaneOpacity
            // 
            this.numericBoxLatticePlaneOpacity.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxLatticePlaneOpacity, "numericBoxLatticePlaneOpacity");
            this.numericBoxLatticePlaneOpacity.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxLatticePlaneOpacity.DecimalPlaces = -2;
            this.numericBoxLatticePlaneOpacity.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxLatticePlaneOpacity.FooterText = "";
            this.numericBoxLatticePlaneOpacity.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxLatticePlaneOpacity.HeaderText = "Opacity";
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
            this.numericBoxLatticePlaneOpacity.ToolTip = "";
            this.numericBoxLatticePlaneOpacity.UpDown_Increment = 0.1D;
            this.numericBoxLatticePlaneOpacity.Value = 0.7D;
            this.numericBoxLatticePlaneOpacity.WordWrap = true;
            this.numericBoxLatticePlaneOpacity.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.LatticePlanes_Changed);
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
            // textBoxInformation
            // 
            resources.ApplyResources(this.textBoxInformation, "textBoxInformation");
            this.textBoxInformation.HideSelection = false;
            this.textBoxInformation.Name = "textBoxInformation";
            this.textBoxInformation.ReadOnly = true;
            this.toolTip.SetToolTip(this.textBoxInformation, resources.GetString("textBoxInformation.ToolTip"));
            // 
            // atomCoordinateTable1
            // 
            this.atomCoordinateTable1.Atom = null;
            this.atomCoordinateTable1.Crystal = null;
            resources.ApplyResources(this.atomCoordinateTable1, "atomCoordinateTable1");
            this.atomCoordinateTable1.Name = "atomCoordinateTable1";
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
            // toolStripMenuItem1
            // 
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // toolStripMenuItem3
            // 
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            this.toolStripMenuItem3.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
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
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormStructureViewer_KeyPress);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBoxShowUnitCell.ResumeLayout(false);
            this.groupBoxShowUnitCell.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellPlaneColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellEdgeColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCellPlaneAlpha)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
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
        private System.Windows.Forms.TabPage tabPage3;
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
    }
}