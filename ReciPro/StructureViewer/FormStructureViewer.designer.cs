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
            this.glControlAxes = new Crystallography.OpenGL.GLControlAlpha();
            this.glControlLight = new Crystallography.OpenGL.GLControlAlpha();
            this.glControlMain = new Crystallography.OpenGL.GLControlAlpha();
            this.flowLayoutPanelLegend = new System.Windows.Forms.FlowLayoutPanel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageBounds = new System.Windows.Forms.TabPage();
            this.tabControlBoundOption = new System.Windows.Forms.TabControl();
            this.tabPageBoundUnitcell = new System.Windows.Forms.TabPage();
            this.buttonSetRange2 = new System.Windows.Forms.Button();
            this.buttonSetRange3 = new System.Windows.Forms.Button();
            this.buttonSetCenter1 = new System.Windows.Forms.Button();
            this.buttonCenter2 = new System.Windows.Forms.Button();
            this.buttonSetCenter3 = new System.Windows.Forms.Button();
            this.buttonSetRange1 = new System.Windows.Forms.Button();
            this.numericBoxCRange = new Crystallography.Controls.NumericBox();
            this.numericBoxBRange = new Crystallography.Controls.NumericBox();
            this.numericBoxARange = new Crystallography.Controls.NumericBox();
            this.numericBoxCCenter = new Crystallography.Controls.NumericBox();
            this.numericBoxBCenter = new Crystallography.Controls.NumericBox();
            this.numericBoxACenter = new Crystallography.Controls.NumericBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageBoundPlane = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxShowBoundPlanes = new System.Windows.Forms.CheckBox();
            this.numericBoxBoundPlanesOpacity = new Crystallography.Controls.NumericBox();
            this.checkBoxClipObjects = new System.Windows.Forms.CheckBox();
            this.checkBoxHideAllAtoms = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonBoundUnitCell = new System.Windows.Forms.RadioButton();
            this.radioButtonBoundPlane = new System.Windows.Forms.RadioButton();
            this.tabPageAtom = new System.Windows.Forms.TabPage();
            this.labelMessage = new System.Windows.Forms.Label();
            this.tabPageBond = new System.Windows.Forms.TabPage();
            this.tabPageUnitCell = new System.Windows.Forms.TabPage();
            this.checkBoxUnitCell = new System.Windows.Forms.CheckBox();
            this.groupBoxShowUnitCell = new System.Windows.Forms.GroupBox();
            this.numericBoxCellPlaneAlpha = new Crystallography.Controls.NumericBox();
            this.colorControlCellPlane = new Crystallography.Controls.ColorControl();
            this.colorControlCellEdge = new Crystallography.Controls.ColorControl();
            this.numericBoxCellTransrationC = new Crystallography.Controls.NumericBox();
            this.numericBoxCellTransrationB = new Crystallography.Controls.NumericBox();
            this.numericBoxCellTransrationA = new Crystallography.Controls.NumericBox();
            this.checkBoxShowSubCell = new System.Windows.Forms.CheckBox();
            this.checkBoxCellShowEdge = new System.Windows.Forms.CheckBox();
            this.numericUpDownSubCellB = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.checkBoxCellShowPlane = new System.Windows.Forms.CheckBox();
            this.numericUpDownSubCellC = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSubCellA = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPageLatticePlane = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.numericBoxLatticePlaneOpacity = new Crystallography.Controls.NumericBox();
            this.tabPageCoordinateInfromatin = new System.Windows.Forms.TabPage();
            this.atomCoordinateTable1 = new Crystallography.Controls.AtomCoordinateTable();
            this.tabPageInformation = new System.Windows.Forms.TabPage();
            this.textBoxInformation = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonBoost = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCrystalAxes = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLightDirection = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLegend = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelStatusInitialization = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabelStatusRendering = new System.Windows.Forms.ToolStripLabel();
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
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.tabPageCrystal = new System.Windows.Forms.TabPage();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageBounds.SuspendLayout();
            this.tabControlBoundOption.SuspendLayout();
            this.tabPageBoundUnitcell.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tabPageAtom.SuspendLayout();
            this.tabPageUnitCell.SuspendLayout();
            this.groupBoxShowUnitCell.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellA)).BeginInit();
            this.tabPageLatticePlane.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tabPageCoordinateInfromatin.SuspendLayout();
            this.tabPageInformation.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.glControlAxes);
            this.splitContainer1.Panel1.Controls.Add(this.glControlLight);
            this.splitContainer1.Panel1.Controls.Add(this.glControlMain);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanelLegend);
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
            this.glControlLight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControlLight.DisablingOpenGL = false;
            resources.ApplyResources(this.glControlLight, "glControlLight");
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
            this.glControlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.glControlMain.DisablingOpenGL = false;
            resources.ApplyResources(this.glControlMain, "glControlMain");
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
            // flowLayoutPanelLegend
            // 
            resources.ApplyResources(this.flowLayoutPanelLegend, "flowLayoutPanelLegend");
            this.flowLayoutPanelLegend.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelLegend.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flowLayoutPanelLegend.Name = "flowLayoutPanelLegend";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageBounds);
            this.tabControl.Controls.Add(this.tabPageAtom);
            this.tabControl.Controls.Add(this.tabPageBond);
            this.tabControl.Controls.Add(this.tabPageUnitCell);
            this.tabControl.Controls.Add(this.tabPageLatticePlane);
            this.tabControl.Controls.Add(this.tabPageCoordinateInfromatin);
            this.tabControl.Controls.Add(this.tabPageInformation);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.HotTrack = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageBounds
            // 
            this.tabPageBounds.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageBounds.Controls.Add(this.tabControlBoundOption);
            this.tabPageBounds.Controls.Add(this.flowLayoutPanel1);
            this.tabPageBounds.Controls.Add(this.flowLayoutPanel2);
            resources.ApplyResources(this.tabPageBounds, "tabPageBounds");
            this.tabPageBounds.Name = "tabPageBounds";
            // 
            // tabControlBoundOption
            // 
            resources.ApplyResources(this.tabControlBoundOption, "tabControlBoundOption");
            this.tabControlBoundOption.Controls.Add(this.tabPageBoundUnitcell);
            this.tabControlBoundOption.Controls.Add(this.tabPageBoundPlane);
            this.tabControlBoundOption.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControlBoundOption.Multiline = true;
            this.tabControlBoundOption.Name = "tabControlBoundOption";
            this.tabControlBoundOption.SelectedIndex = 0;
            this.tabControlBoundOption.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            // 
            // tabPageBoundUnitcell
            // 
            this.tabPageBoundUnitcell.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPageBoundUnitcell.Controls.Add(this.buttonSetRange2);
            this.tabPageBoundUnitcell.Controls.Add(this.buttonSetRange3);
            this.tabPageBoundUnitcell.Controls.Add(this.buttonSetCenter1);
            this.tabPageBoundUnitcell.Controls.Add(this.buttonCenter2);
            this.tabPageBoundUnitcell.Controls.Add(this.buttonSetCenter3);
            this.tabPageBoundUnitcell.Controls.Add(this.buttonSetRange1);
            this.tabPageBoundUnitcell.Controls.Add(this.numericBoxCRange);
            this.tabPageBoundUnitcell.Controls.Add(this.numericBoxBRange);
            this.tabPageBoundUnitcell.Controls.Add(this.numericBoxARange);
            this.tabPageBoundUnitcell.Controls.Add(this.numericBoxCCenter);
            this.tabPageBoundUnitcell.Controls.Add(this.numericBoxBCenter);
            this.tabPageBoundUnitcell.Controls.Add(this.numericBoxACenter);
            this.tabPageBoundUnitcell.Controls.Add(this.label2);
            this.tabPageBoundUnitcell.Controls.Add(this.label4);
            this.tabPageBoundUnitcell.Controls.Add(this.label3);
            this.tabPageBoundUnitcell.Controls.Add(this.label1);
            resources.ApplyResources(this.tabPageBoundUnitcell, "tabPageBoundUnitcell");
            this.tabPageBoundUnitcell.Name = "tabPageBoundUnitcell";
            // 
            // buttonSetRange2
            // 
            resources.ApplyResources(this.buttonSetRange2, "buttonSetRange2");
            this.buttonSetRange2.Name = "buttonSetRange2";
            this.buttonSetRange2.Tag = "0.75";
            this.buttonSetRange2.UseVisualStyleBackColor = true;
            this.buttonSetRange2.Click += new System.EventHandler(this.buttonSetCenterOrRange_Click);
            // 
            // buttonSetRange3
            // 
            resources.ApplyResources(this.buttonSetRange3, "buttonSetRange3");
            this.buttonSetRange3.Name = "buttonSetRange3";
            this.buttonSetRange3.Tag = "1";
            this.buttonSetRange3.UseVisualStyleBackColor = true;
            this.buttonSetRange3.Click += new System.EventHandler(this.buttonSetCenterOrRange_Click);
            // 
            // buttonSetCenter1
            // 
            resources.ApplyResources(this.buttonSetCenter1, "buttonSetCenter1");
            this.buttonSetCenter1.Name = "buttonSetCenter1";
            this.buttonSetCenter1.Tag = "0";
            this.buttonSetCenter1.UseVisualStyleBackColor = true;
            this.buttonSetCenter1.Click += new System.EventHandler(this.buttonSetCenterOrRange_Click);
            // 
            // buttonCenter2
            // 
            resources.ApplyResources(this.buttonCenter2, "buttonCenter2");
            this.buttonCenter2.Name = "buttonCenter2";
            this.buttonCenter2.Tag = "0.25";
            this.buttonCenter2.UseVisualStyleBackColor = true;
            this.buttonCenter2.Click += new System.EventHandler(this.buttonSetCenterOrRange_Click);
            // 
            // buttonSetCenter3
            // 
            resources.ApplyResources(this.buttonSetCenter3, "buttonSetCenter3");
            this.buttonSetCenter3.Name = "buttonSetCenter3";
            this.buttonSetCenter3.Tag = "0.5";
            this.buttonSetCenter3.UseVisualStyleBackColor = true;
            this.buttonSetCenter3.Click += new System.EventHandler(this.buttonSetCenterOrRange_Click);
            // 
            // buttonSetRange1
            // 
            resources.ApplyResources(this.buttonSetRange1, "buttonSetRange1");
            this.buttonSetRange1.Name = "buttonSetRange1";
            this.buttonSetRange1.Tag = "0.5";
            this.buttonSetRange1.UseVisualStyleBackColor = true;
            this.buttonSetRange1.Click += new System.EventHandler(this.buttonSetCenterOrRange_Click);
            // 
            // numericBoxCRange
            // 
            this.numericBoxCRange.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxCRange, "numericBoxCRange");
            this.numericBoxCRange.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCRange.DecimalPlaces = 2;
            this.numericBoxCRange.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCRange.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCRange.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCRange.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCRange.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxCRange.Maximum = 10D;
            this.numericBoxCRange.Minimum = 0D;
            this.numericBoxCRange.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxCRange.MouseSpeed = 1D;
            this.numericBoxCRange.Multiline = false;
            this.numericBoxCRange.Name = "numericBoxCRange";
            this.numericBoxCRange.RadianValue = 0.0087266462599716477D;
            this.numericBoxCRange.ReadOnly = false;
            this.numericBoxCRange.RestrictLimitValue = true;
            this.numericBoxCRange.ShowFraction = true;
            this.numericBoxCRange.ShowPositiveSign = false;
            this.numericBoxCRange.ShowUpDown = false;
            this.numericBoxCRange.SkipEventDuringInput = false;
            this.numericBoxCRange.SmartIncrement = false;
            this.numericBoxCRange.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCRange.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCRange.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCRange.ThonsandsSeparator = true;
            this.numericBoxCRange.UpDown_Increment = 0.25D;
            this.numericBoxCRange.Value = 0.5D;
            this.numericBoxCRange.WordWrap = true;
            this.numericBoxCRange.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCMax_ValueChanged);
            // 
            // numericBoxBRange
            // 
            this.numericBoxBRange.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxBRange, "numericBoxBRange");
            this.numericBoxBRange.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBRange.DecimalPlaces = 2;
            this.numericBoxBRange.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBRange.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBRange.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBRange.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBRange.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxBRange.Maximum = 10D;
            this.numericBoxBRange.Minimum = 0D;
            this.numericBoxBRange.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxBRange.MouseSpeed = 1D;
            this.numericBoxBRange.Multiline = false;
            this.numericBoxBRange.Name = "numericBoxBRange";
            this.numericBoxBRange.RadianValue = 0.0087266462599716477D;
            this.numericBoxBRange.ReadOnly = false;
            this.numericBoxBRange.RestrictLimitValue = true;
            this.numericBoxBRange.ShowFraction = true;
            this.numericBoxBRange.ShowPositiveSign = false;
            this.numericBoxBRange.ShowUpDown = false;
            this.numericBoxBRange.SkipEventDuringInput = false;
            this.numericBoxBRange.SmartIncrement = false;
            this.numericBoxBRange.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBRange.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBRange.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBRange.ThonsandsSeparator = true;
            this.numericBoxBRange.UpDown_Increment = 0.25D;
            this.numericBoxBRange.Value = 0.5D;
            this.numericBoxBRange.WordWrap = true;
            this.numericBoxBRange.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCMax_ValueChanged);
            // 
            // numericBoxARange
            // 
            this.numericBoxARange.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxARange, "numericBoxARange");
            this.numericBoxARange.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxARange.DecimalPlaces = 2;
            this.numericBoxARange.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxARange.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxARange.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxARange.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxARange.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxARange.Maximum = 10D;
            this.numericBoxARange.Minimum = 0D;
            this.numericBoxARange.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxARange.MouseSpeed = 1D;
            this.numericBoxARange.Multiline = false;
            this.numericBoxARange.Name = "numericBoxARange";
            this.numericBoxARange.RadianValue = 0.0087266462599716477D;
            this.numericBoxARange.ReadOnly = false;
            this.numericBoxARange.RestrictLimitValue = true;
            this.numericBoxARange.ShowFraction = true;
            this.numericBoxARange.ShowPositiveSign = false;
            this.numericBoxARange.ShowUpDown = false;
            this.numericBoxARange.SkipEventDuringInput = false;
            this.numericBoxARange.SmartIncrement = false;
            this.numericBoxARange.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxARange.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxARange.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxARange.ThonsandsSeparator = true;
            this.numericBoxARange.UpDown_Increment = 0.25D;
            this.numericBoxARange.Value = 0.5D;
            this.numericBoxARange.WordWrap = true;
            this.numericBoxARange.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCMax_ValueChanged);
            // 
            // numericBoxCCenter
            // 
            this.numericBoxCCenter.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxCCenter, "numericBoxCCenter");
            this.numericBoxCCenter.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCCenter.DecimalPlaces = 2;
            this.numericBoxCCenter.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCCenter.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCCenter.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxCCenter.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCCenter.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxCCenter.Maximum = 10D;
            this.numericBoxCCenter.Minimum = 0D;
            this.numericBoxCCenter.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxCCenter.MouseSpeed = 1D;
            this.numericBoxCCenter.Multiline = false;
            this.numericBoxCCenter.Name = "numericBoxCCenter";
            this.numericBoxCCenter.RadianValue = 0D;
            this.numericBoxCCenter.ReadOnly = false;
            this.numericBoxCCenter.RestrictLimitValue = true;
            this.numericBoxCCenter.ShowFraction = true;
            this.numericBoxCCenter.ShowPositiveSign = false;
            this.numericBoxCCenter.ShowUpDown = false;
            this.numericBoxCCenter.SkipEventDuringInput = false;
            this.numericBoxCCenter.SmartIncrement = false;
            this.numericBoxCCenter.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCCenter.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCCenter.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCCenter.ThonsandsSeparator = true;
            this.numericBoxCCenter.UpDown_Increment = 0.25D;
            this.numericBoxCCenter.Value = 0D;
            this.numericBoxCCenter.WordWrap = true;
            this.numericBoxCCenter.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCMax_ValueChanged);
            // 
            // numericBoxBCenter
            // 
            this.numericBoxBCenter.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxBCenter, "numericBoxBCenter");
            this.numericBoxBCenter.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBCenter.DecimalPlaces = 2;
            this.numericBoxBCenter.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBCenter.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBCenter.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxBCenter.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxBCenter.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxBCenter.Maximum = 10D;
            this.numericBoxBCenter.Minimum = 0D;
            this.numericBoxBCenter.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxBCenter.MouseSpeed = 1D;
            this.numericBoxBCenter.Multiline = false;
            this.numericBoxBCenter.Name = "numericBoxBCenter";
            this.numericBoxBCenter.RadianValue = 0D;
            this.numericBoxBCenter.ReadOnly = false;
            this.numericBoxBCenter.RestrictLimitValue = true;
            this.numericBoxBCenter.ShowFraction = true;
            this.numericBoxBCenter.ShowPositiveSign = false;
            this.numericBoxBCenter.ShowUpDown = false;
            this.numericBoxBCenter.SkipEventDuringInput = false;
            this.numericBoxBCenter.SmartIncrement = false;
            this.numericBoxBCenter.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxBCenter.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxBCenter.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxBCenter.ThonsandsSeparator = true;
            this.numericBoxBCenter.UpDown_Increment = 0.25D;
            this.numericBoxBCenter.Value = 0D;
            this.numericBoxBCenter.WordWrap = true;
            this.numericBoxBCenter.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCMax_ValueChanged);
            // 
            // numericBoxACenter
            // 
            this.numericBoxACenter.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxACenter, "numericBoxACenter");
            this.numericBoxACenter.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxACenter.DecimalPlaces = 2;
            this.numericBoxACenter.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxACenter.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxACenter.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxACenter.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxACenter.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxACenter.Maximum = 10D;
            this.numericBoxACenter.Minimum = 0D;
            this.numericBoxACenter.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxACenter.MouseSpeed = 1D;
            this.numericBoxACenter.Multiline = false;
            this.numericBoxACenter.Name = "numericBoxACenter";
            this.numericBoxACenter.RadianValue = 0D;
            this.numericBoxACenter.ReadOnly = false;
            this.numericBoxACenter.RestrictLimitValue = true;
            this.numericBoxACenter.ShowFraction = true;
            this.numericBoxACenter.ShowPositiveSign = false;
            this.numericBoxACenter.ShowUpDown = false;
            this.numericBoxACenter.SkipEventDuringInput = false;
            this.numericBoxACenter.SmartIncrement = false;
            this.numericBoxACenter.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxACenter.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxACenter.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxACenter.ThonsandsSeparator = true;
            this.numericBoxACenter.UpDown_Increment = 0.25D;
            this.numericBoxACenter.Value = 0D;
            this.numericBoxACenter.WordWrap = true;
            this.numericBoxACenter.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxCMax_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabPageBoundPlane
            // 
            this.tabPageBoundPlane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.tabPageBoundPlane, "tabPageBoundPlane");
            this.tabPageBoundPlane.Name = "tabPageBoundPlane";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.checkBoxShowBoundPlanes);
            this.flowLayoutPanel1.Controls.Add(this.numericBoxBoundPlanesOpacity);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxClipObjects);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxHideAllAtoms);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // checkBoxShowBoundPlanes
            // 
            resources.ApplyResources(this.checkBoxShowBoundPlanes, "checkBoxShowBoundPlanes");
            this.checkBoxShowBoundPlanes.Name = "checkBoxShowBoundPlanes";
            this.checkBoxShowBoundPlanes.UseVisualStyleBackColor = true;
            this.checkBoxShowBoundPlanes.CheckedChanged += new System.EventHandler(this.checkBoxShowBoundPlanes_CheckedChanged);
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
            this.numericBoxBoundPlanesOpacity.HeaderMargin = new System.Windows.Forms.Padding(0);
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
            this.numericBoxBoundPlanesOpacity.UpDown_Increment = 0.1D;
            this.numericBoxBoundPlanesOpacity.Value = 0.7D;
            this.numericBoxBoundPlanesOpacity.WordWrap = true;
            this.numericBoxBoundPlanesOpacity.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.checkBoxShowBoundPlanes_CheckedChanged);
            // 
            // checkBoxClipObjects
            // 
            resources.ApplyResources(this.checkBoxClipObjects, "checkBoxClipObjects");
            this.checkBoxClipObjects.Name = "checkBoxClipObjects";
            this.checkBoxClipObjects.UseVisualStyleBackColor = true;
            this.checkBoxClipObjects.CheckedChanged += new System.EventHandler(this.checkBoxShowBoundPlanes_CheckedChanged);
            // 
            // checkBoxHideAllAtoms
            // 
            resources.ApplyResources(this.checkBoxHideAllAtoms, "checkBoxHideAllAtoms");
            this.checkBoxHideAllAtoms.Name = "checkBoxHideAllAtoms";
            this.checkBoxHideAllAtoms.UseVisualStyleBackColor = true;
            this.checkBoxHideAllAtoms.CheckedChanged += new System.EventHandler(this.checkBoxShowBoundPlanes_CheckedChanged);
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Controls.Add(this.radioButtonBoundUnitCell);
            this.flowLayoutPanel2.Controls.Add(this.radioButtonBoundPlane);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // radioButtonBoundUnitCell
            // 
            resources.ApplyResources(this.radioButtonBoundUnitCell, "radioButtonBoundUnitCell");
            this.radioButtonBoundUnitCell.Checked = true;
            this.radioButtonBoundUnitCell.Name = "radioButtonBoundUnitCell";
            this.radioButtonBoundUnitCell.TabStop = true;
            this.radioButtonBoundUnitCell.UseVisualStyleBackColor = true;
            this.radioButtonBoundUnitCell.CheckedChanged += new System.EventHandler(this.radioButtonUnitCell_CheckedChanged);
            // 
            // radioButtonBoundPlane
            // 
            resources.ApplyResources(this.radioButtonBoundPlane, "radioButtonBoundPlane");
            this.radioButtonBoundPlane.Name = "radioButtonBoundPlane";
            this.radioButtonBoundPlane.UseVisualStyleBackColor = true;
            // 
            // tabPageAtom
            // 
            this.tabPageAtom.Controls.Add(this.labelMessage);
            resources.ApplyResources(this.tabPageAtom, "tabPageAtom");
            this.tabPageAtom.Name = "tabPageAtom";
            // 
            // labelMessage
            // 
            resources.ApplyResources(this.labelMessage, "labelMessage");
            this.labelMessage.ForeColor = System.Drawing.Color.Red;
            this.labelMessage.Name = "labelMessage";
            // 
            // tabPageBond
            // 
            resources.ApplyResources(this.tabPageBond, "tabPageBond");
            this.tabPageBond.Name = "tabPageBond";
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
            this.groupBoxShowUnitCell.Controls.Add(this.numericBoxCellPlaneAlpha);
            this.groupBoxShowUnitCell.Controls.Add(this.colorControlCellPlane);
            this.groupBoxShowUnitCell.Controls.Add(this.colorControlCellEdge);
            this.groupBoxShowUnitCell.Controls.Add(this.numericBoxCellTransrationC);
            this.groupBoxShowUnitCell.Controls.Add(this.numericBoxCellTransrationB);
            this.groupBoxShowUnitCell.Controls.Add(this.numericBoxCellTransrationA);
            this.groupBoxShowUnitCell.Controls.Add(this.checkBoxShowSubCell);
            this.groupBoxShowUnitCell.Controls.Add(this.checkBoxCellShowEdge);
            this.groupBoxShowUnitCell.Controls.Add(this.numericUpDownSubCellB);
            this.groupBoxShowUnitCell.Controls.Add(this.label10);
            this.groupBoxShowUnitCell.Controls.Add(this.checkBoxCellShowPlane);
            this.groupBoxShowUnitCell.Controls.Add(this.numericUpDownSubCellC);
            this.groupBoxShowUnitCell.Controls.Add(this.numericUpDownSubCellA);
            this.groupBoxShowUnitCell.Controls.Add(this.label17);
            this.groupBoxShowUnitCell.Controls.Add(this.label16);
            this.groupBoxShowUnitCell.Controls.Add(this.label12);
            resources.ApplyResources(this.groupBoxShowUnitCell, "groupBoxShowUnitCell");
            this.groupBoxShowUnitCell.Name = "groupBoxShowUnitCell";
            this.groupBoxShowUnitCell.TabStop = false;
            this.toolTip.SetToolTip(this.groupBoxShowUnitCell, resources.GetString("groupBoxShowUnitCell.ToolTip"));
            // 
            // numericBoxCellPlaneAlpha
            // 
            this.numericBoxCellPlaneAlpha.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxCellPlaneAlpha, "numericBoxCellPlaneAlpha");
            this.numericBoxCellPlaneAlpha.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellPlaneAlpha.DecimalPlaces = 1;
            this.numericBoxCellPlaneAlpha.FooterBackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellPlaneAlpha.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCellPlaneAlpha.HeaderBackColor = System.Drawing.Color.Transparent;
            this.numericBoxCellPlaneAlpha.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxCellPlaneAlpha.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxCellPlaneAlpha.Maximum = 1D;
            this.numericBoxCellPlaneAlpha.Minimum = 0D;
            this.numericBoxCellPlaneAlpha.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxCellPlaneAlpha.MouseSpeed = 1D;
            this.numericBoxCellPlaneAlpha.Multiline = false;
            this.numericBoxCellPlaneAlpha.Name = "numericBoxCellPlaneAlpha";
            this.numericBoxCellPlaneAlpha.RadianValue = 0.0087266462599716477D;
            this.numericBoxCellPlaneAlpha.ReadOnly = false;
            this.numericBoxCellPlaneAlpha.RestrictLimitValue = true;
            this.numericBoxCellPlaneAlpha.ShowFraction = false;
            this.numericBoxCellPlaneAlpha.ShowPositiveSign = false;
            this.numericBoxCellPlaneAlpha.ShowUpDown = false;
            this.numericBoxCellPlaneAlpha.SkipEventDuringInput = false;
            this.numericBoxCellPlaneAlpha.SmartIncrement = false;
            this.numericBoxCellPlaneAlpha.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCellPlaneAlpha.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCellPlaneAlpha.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellPlaneAlpha.ThonsandsSeparator = true;
            this.numericBoxCellPlaneAlpha.UpDown_Increment = 0.1D;
            this.numericBoxCellPlaneAlpha.Value = 0.5D;
            this.numericBoxCellPlaneAlpha.WordWrap = true;
            this.numericBoxCellPlaneAlpha.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // colorControlCellPlane
            // 
            this.colorControlCellPlane.Argb = -5192482;
            resources.ApplyResources(this.colorControlCellPlane, "colorControlCellPlane");
            this.colorControlCellPlane.Blue = 222;
            this.colorControlCellPlane.BlueF = 0.8705882F;
            this.colorControlCellPlane.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlCellPlane.Color = System.Drawing.Color.FromArgb(((int)(((byte)(176)))), ((int)(((byte)(196)))), ((int)(((byte)(222)))));
            this.colorControlCellPlane.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlCellPlane.Green = 196;
            this.colorControlCellPlane.GreenF = 0.7686275F;
            this.colorControlCellPlane.Name = "colorControlCellPlane";
            this.colorControlCellPlane.Red = 176;
            this.colorControlCellPlane.RedF = 0.6901961F;
            this.colorControlCellPlane.ColorChanged += new System.EventHandler(this.checkBoxShowUnitCell_CheckedChanged);
            // 
            // colorControlCellEdge
            // 
            this.colorControlCellEdge.Argb = -16777011;
            resources.ApplyResources(this.colorControlCellEdge, "colorControlCellEdge");
            this.colorControlCellEdge.Blue = 205;
            this.colorControlCellEdge.BlueF = 0.8039216F;
            this.colorControlCellEdge.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControlCellEdge.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(205)))));
            this.colorControlCellEdge.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.colorControlCellEdge.Green = 0;
            this.colorControlCellEdge.GreenF = 0F;
            this.colorControlCellEdge.Name = "colorControlCellEdge";
            this.colorControlCellEdge.Red = 0;
            this.colorControlCellEdge.RedF = 0F;
            this.colorControlCellEdge.ColorChanged += new System.EventHandler(this.checkBoxShowUnitCell_CheckedChanged);
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
            this.numericBoxCellTransrationC.HeaderMargin = new System.Windows.Forms.Padding(0);
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
            this.numericBoxCellTransrationC.SmartIncrement = false;
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
            this.numericBoxCellTransrationB.HeaderMargin = new System.Windows.Forms.Padding(0);
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
            this.numericBoxCellTransrationB.SmartIncrement = false;
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
            this.numericBoxCellTransrationA.HeaderMargin = new System.Windows.Forms.Padding(0);
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
            this.numericBoxCellTransrationA.SmartIncrement = false;
            this.numericBoxCellTransrationA.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxCellTransrationA.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxCellTransrationA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxCellTransrationA.ThonsandsSeparator = true;
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
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // tabPageLatticePlane
            // 
            this.tabPageLatticePlane.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageLatticePlane.Controls.Add(this.flowLayoutPanel3);
            resources.ApplyResources(this.tabPageLatticePlane, "tabPageLatticePlane");
            this.tabPageLatticePlane.Name = "tabPageLatticePlane";
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Controls.Add(this.numericBoxLatticePlaneOpacity);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // numericBoxLatticePlaneOpacity
            // 
            this.numericBoxLatticePlaneOpacity.AllowMouseControl = false;
            resources.ApplyResources(this.numericBoxLatticePlaneOpacity, "numericBoxLatticePlaneOpacity");
            this.numericBoxLatticePlaneOpacity.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxLatticePlaneOpacity.DecimalPlaces = -2;
            this.numericBoxLatticePlaneOpacity.FooterBackColor = System.Drawing.Color.Transparent;
            this.numericBoxLatticePlaneOpacity.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxLatticePlaneOpacity.HeaderBackColor = System.Drawing.Color.Transparent;
            this.numericBoxLatticePlaneOpacity.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxLatticePlaneOpacity.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxLatticePlaneOpacity.Maximum = 1D;
            this.numericBoxLatticePlaneOpacity.Minimum = 0D;
            this.numericBoxLatticePlaneOpacity.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxLatticePlaneOpacity.MouseSpeed = 1D;
            this.numericBoxLatticePlaneOpacity.Multiline = false;
            this.numericBoxLatticePlaneOpacity.Name = "numericBoxLatticePlaneOpacity";
            this.numericBoxLatticePlaneOpacity.RadianValue = 0.0087266462599716477D;
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
            this.numericBoxLatticePlaneOpacity.Value = 0.5D;
            this.numericBoxLatticePlaneOpacity.WordWrap = true;
            this.numericBoxLatticePlaneOpacity.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxLatticePlaneOpacity_ValueChanged);
            // 
            // tabPageCoordinateInfromatin
            // 
            this.tabPageCoordinateInfromatin.Controls.Add(this.atomCoordinateTable1);
            resources.ApplyResources(this.tabPageCoordinateInfromatin, "tabPageCoordinateInfromatin");
            this.tabPageCoordinateInfromatin.Name = "tabPageCoordinateInfromatin";
            // 
            // atomCoordinateTable1
            // 
            this.atomCoordinateTable1.Atom = null;
            this.atomCoordinateTable1.Crystal = null;
            resources.ApplyResources(this.atomCoordinateTable1, "atomCoordinateTable1");
            this.atomCoordinateTable1.Name = "atomCoordinateTable1";
            // 
            // tabPageInformation
            // 
            this.tabPageInformation.Controls.Add(this.textBoxInformation);
            resources.ApplyResources(this.tabPageInformation, "tabPageInformation");
            this.tabPageInformation.Name = "tabPageInformation";
            this.tabPageInformation.UseVisualStyleBackColor = true;
            // 
            // textBoxInformation
            // 
            resources.ApplyResources(this.textBoxInformation, "textBoxInformation");
            this.textBoxInformation.Name = "textBoxInformation";
            this.textBoxInformation.ReadOnly = true;
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
            this.toolStripSeparator3,
            this.toolStripLabelStatusInitialization,
            this.toolStripLabelStatusRendering});
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
            this.toolStripButtonCrystalAxes.CheckedChanged += new System.EventHandler(this.toolStripButtonCrystalAxes_CheckedChanged);
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
            this.toolStripButtonLightDirection.CheckedChanged += new System.EventHandler(this.toolStripButtonLightingBall_CheckedChanged);
            // 
            // toolStripButtonLegend
            // 
            this.toolStripButtonLegend.Checked = true;
            this.toolStripButtonLegend.CheckOnClick = true;
            this.toolStripButtonLegend.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonLegend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.toolStripButtonLegend, "toolStripButtonLegend");
            this.toolStripButtonLegend.Name = "toolStripButtonLegend";
            this.toolStripButtonLegend.CheckedChanged += new System.EventHandler(this.toolStripButtonLegend_CheckedChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
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
            // tabPageCrystal
            // 
            resources.ApplyResources(this.tabPageCrystal, "tabPageCrystal");
            this.tabPageCrystal.Name = "tabPageCrystal";
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
            // FormStructureViewer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormStructureViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStructureViewer_FormClosing);
            this.Load += new System.EventHandler(this.FormStructureViewer_Load);
            this.VisibleChanged += new System.EventHandler(this.FormStructureViewer_VisibleChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormStructureViewer_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageBounds.ResumeLayout(false);
            this.tabPageBounds.PerformLayout();
            this.tabControlBoundOption.ResumeLayout(false);
            this.tabPageBoundUnitcell.ResumeLayout(false);
            this.tabPageBoundUnitcell.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tabPageAtom.ResumeLayout(false);
            this.tabPageAtom.PerformLayout();
            this.tabPageUnitCell.ResumeLayout(false);
            this.tabPageUnitCell.PerformLayout();
            this.groupBoxShowUnitCell.ResumeLayout(false);
            this.groupBoxShowUnitCell.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSubCellA)).EndInit();
            this.tabPageLatticePlane.ResumeLayout(false);
            this.tabPageLatticePlane.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.tabPageCoordinateInfromatin.ResumeLayout(false);
            this.tabPageInformation.ResumeLayout(false);
            this.tabPageInformation.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxShowUnitCell;
        private System.Windows.Forms.CheckBox checkBoxUnitCell;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBoxCellShowPlane;
        private System.Windows.Forms.CheckBox checkBoxCellShowEdge;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
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
        private Crystallography.OpenGL.GLControlAlpha glControlMain;
        private Crystallography.OpenGL.GLControlAlpha glControlAxes;
        private Crystallography.Controls.NumericBox numericBoxCellTransrationC;
        private Crystallography.Controls.NumericBox numericBoxCellTransrationB;
        private Crystallography.Controls.NumericBox numericBoxCellTransrationA;
        private Crystallography.OpenGL.GLControlAlpha glControlLight;
        private System.Windows.Forms.CheckBox checkBoxClipObjects;
        private System.Windows.Forms.CheckBox checkBoxShowBoundPlanes;
        private Crystallography.Controls.NumericBox numericBoxBoundPlanesOpacity;
        private System.Windows.Forms.TabPage tabPageBounds;
        private System.Windows.Forms.CheckBox checkBoxHideAllAtoms;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cTRLSHIFTcMainImageToClipboardCTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cTRLRightDoubleClickChangePerspectiveOrthogonalAlternatelyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TabPage tabPageCoordinateInfromatin;
        private System.Windows.Forms.TabPage tabPageAtom;
        private System.Windows.Forms.TabPage tabPageBond;
        private System.Windows.Forms.RadioButton radioButtonBoundPlane;
        private System.Windows.Forms.RadioButton radioButtonBoundUnitCell;
        private System.Windows.Forms.TabControl tabControlBoundOption;
        private System.Windows.Forms.TabPage tabPageBoundUnitcell;
        private System.Windows.Forms.TabPage tabPageBoundPlane;
        private Crystallography.Controls.NumericBox numericBoxCRange;
        private Crystallography.Controls.NumericBox numericBoxBRange;
        private Crystallography.Controls.NumericBox numericBoxARange;
        private Crystallography.Controls.NumericBox numericBoxCCenter;
        private Crystallography.Controls.NumericBox numericBoxBCenter;
        private Crystallography.Controls.NumericBox numericBoxACenter;
        private System.Windows.Forms.BindingSource bindingSourceAtoms;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.TabPage tabPageCrystal;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TabPage tabPageInformation;
        private System.Windows.Forms.TextBox textBoxInformation;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonSetRange2;
        private System.Windows.Forms.Button buttonSetRange3;
        private System.Windows.Forms.Button buttonSetCenter1;
        private System.Windows.Forms.Button buttonSetCenter3;
        private System.Windows.Forms.Button buttonSetRange1;
        private System.Windows.Forms.ToolStripLabel toolStripLabelStatusInitialization;
        private System.Windows.Forms.ToolStripLabel toolStripLabelStatusRendering;
        private System.Windows.Forms.Button buttonCenter2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private Crystallography.Controls.NumericBox numericBoxLatticePlaneOpacity;
        private Crystallography.Controls.ColorControl colorControlCellPlane;
        private Crystallography.Controls.ColorControl colorControlCellEdge;
        private Crystallography.Controls.NumericBox numericBoxCellPlaneAlpha;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLegend;
    }
}