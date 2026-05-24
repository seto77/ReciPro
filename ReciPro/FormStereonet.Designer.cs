using System;

namespace ReciPro
{
    partial class FormStereonet
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
            if (strFont != null)
                strFont.Dispose();
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        // (260323Ch) renamed numeric container controls:
        // groupBox1 -> groupBoxColor
        // groupBox2 -> groupBoxMode
        // groupBox3 -> groupBoxOutline
        // groupBox4 -> groupBoxSize
        // groupBox5 -> groupBoxIndices
        // groupBox6 -> groupBoxDelimiter
        // groupBox7 -> groupBoxProjectionObject
        // groupBox8 -> groupBoxSphere
        // groupBox9 -> groupBoxProjectionScheme
        // flowLayoutPanel1 -> flowLayoutPanelProjectionObject
        // flowLayoutPanel2 -> flowLayoutPanelProjectionScheme
        // flowLayoutPanel3 -> flowLayoutPanelIndices
        // flowLayoutPanel4 -> flowLayoutPanelSphere
        // flowLayoutPanel5 -> flowLayoutPanelIndexFilter
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStereonet));
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            graphicsBox = new GraphicsBox(components);
            trackBarStrSize = new System.Windows.Forms.TrackBar();
            trackBarPointSize = new System.Windows.Forms.TrackBar();
            groupBoxMode = new System.Windows.Forms.GroupBox();
            groupBoxSphere = new System.Windows.Forms.GroupBox();
            flowLayoutPanelSphere = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonUpperSphere = new System.Windows.Forms.RadioButton();
            radioButtonLowerSphere = new System.Windows.Forms.RadioButton();
            groupBoxProjectionScheme = new System.Windows.Forms.GroupBox();
            flowLayoutPanelProjectionScheme = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonWulff = new System.Windows.Forms.RadioButton();
            radioButtonSchmidt = new System.Windows.Forms.RadioButton();
            panel2 = new System.Windows.Forms.Panel();
            groupBoxProjectionObject = new System.Windows.Forms.GroupBox();
            checkBoxReflectStructureFactor = new System.Windows.Forms.CheckBox();
            checkBoxShowIndexLabels = new System.Windows.Forms.CheckBox();
            flowLayoutPanelProjectionObject = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonAxes = new System.Windows.Forms.RadioButton();
            radioButtonPlanes = new System.Windows.Forms.RadioButton();
            radioButtonKikuchiLinePairs = new System.Windows.Forms.RadioButton();
            labelXYpos = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBoxOutline = new System.Windows.Forms.GroupBox();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonOutlineEquator = new System.Windows.Forms.RadioButton();
            radioButtonOutlinePole = new System.Windows.Forms.RadioButton();
            checkBox1DegLine = new System.Windows.Forms.CheckBox();
            tabControl = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            groupBoxDelimiter = new System.Windows.Forms.GroupBox();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonDelimiterNone = new System.Windows.Forms.RadioButton();
            radioButtonDelimiterSpace = new System.Windows.Forms.RadioButton();
            radioButtonDelimiterComma = new System.Windows.Forms.RadioButton();
            groupBoxSize = new System.Windows.Forms.GroupBox();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            groupBoxColor = new System.Windows.Forms.GroupBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            colorControlBackGround = new ColorControl();
            colorControl90DegLine = new ColorControl();
            colorControl10DegLine = new ColorControl();
            colorControl1DegLine = new ColorControl();
            colorControlKikuchi = new ColorControl();
            colorControlString = new ColorControl();
            tabPage2 = new System.Windows.Forms.TabPage();
            flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonCircleByAxis = new System.Windows.Forms.RadioButton();
            flowLayoutPanelCircleAxis = new System.Windows.Forms.FlowLayoutPanel();
            indexControlAxis = new IndexControl();
            radioButtonCircleByPlanes = new System.Windows.Forms.RadioButton();
            flowLayoutPanelCirclePlanes = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            indexControlCirclePlane1 = new IndexControl();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            indexControlCirclePlane2 = new IndexControl();
            colorControlGreatCircle = new ColorControl();
            buttonAddCircle = new System.Windows.Forms.Button();
            buttonDeleteCircle = new System.Windows.Forms.Button();
            checkedListBoxCircles = new System.Windows.Forms.CheckedListBox();
            tabPage3 = new System.Windows.Forms.TabPage();
            buttonYusaModeStop = new System.Windows.Forms.Button();
            buttonYusaModeStart = new System.Windows.Forms.Button();
            radioButtonRotationalScan = new System.Windows.Forms.RadioButton();
            radioButtonZigzagScan = new System.Windows.Forms.RadioButton();
            checkBox3 = new System.Windows.Forms.CheckBox();
            checkBox2 = new System.Windows.Forms.CheckBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            label25 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            numericBoxRxSpeed = new NumericBox();
            numericBoxRySpeed = new NumericBox();
            numericBoxRzSpeed = new NumericBox();
            numericBoxTotalTime = new NumericBox();
            numericBoxAngularSpeed = new NumericBox();
            numericBoxRyStep = new NumericBox();
            numericBoxRadialAngle = new NumericBox();
            numericBoxRyOscillation = new NumericBox();
            numericBoxRzOscillation = new NumericBox();
            tabPage4 = new System.Windows.Forms.TabPage();
            waveLengthControl = new WaveLengthControl();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            asImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            asMetafileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            asBitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            asMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemSaveMovieStereonet = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemSaveMovie3D = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolTip = new System.Windows.Forms.ToolTip(components);
            radioButtonRange = new System.Windows.Forms.RadioButton();
            radioButtonSpecifiedIndices = new System.Windows.Forms.RadioButton();
            radioButtonHighStructureFactor = new System.Windows.Forms.RadioButton();
            printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            groupBoxIndices = new System.Windows.Forms.GroupBox();
            panelSpecifiedIndices = new System.Windows.Forms.Panel();
            listBoxSpecifiedIndices = new System.Windows.Forms.ListBox();
            flowLayoutPanelAddRemove = new System.Windows.Forms.FlowLayoutPanel();
            buttonAddIndex = new System.Windows.Forms.Button();
            buttonRemoveIndex = new System.Windows.Forms.Button();
            colorControlIndex = new ColorControl();
            checkBoxRotateColor = new System.Windows.Forms.CheckBox();
            checkBoxIncludingEquivalentPlanes = new System.Windows.Forms.CheckBox();
            flowLayoutPanelIndices = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelIndexFilter = new System.Windows.Forms.FlowLayoutPanel();
            numericBoxHighStructureFactor = new NumericBox();
            indexControlDrawing = new IndexControl();
            panel3 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            panel3DOptions = new System.Windows.Forms.Panel();
            checkBoxDisplay3D = new System.Windows.Forms.CheckBox();
            groupBox3DOptions = new System.Windows.Forms.GroupBox();
            flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            button3D_reset = new System.Windows.Forms.Button();
            checkBox3dOptionLabel = new System.Windows.Forms.CheckBox();
            checkBox3dOptionSemisphere = new System.Windows.Forms.CheckBox();
            checkBox3dOptionStereonet = new System.Windows.Forms.CheckBox();
            flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            checkBox3dOptionSphere = new System.Windows.Forms.CheckBox();
            checkBox3dOptionProjectionLine = new System.Windows.Forms.CheckBox();
            label29 = new System.Windows.Forms.Label();
            trackBarDepthFadingOut = new System.Windows.Forms.TrackBar();
            labelAxisPlane = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            printDialog1 = new System.Windows.Forms.PrintDialog();
            flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).BeginInit();
            groupBoxMode.SuspendLayout();
            groupBoxSphere.SuspendLayout();
            flowLayoutPanelSphere.SuspendLayout();
            groupBoxProjectionScheme.SuspendLayout();
            flowLayoutPanelProjectionScheme.SuspendLayout();
            groupBoxProjectionObject.SuspendLayout();
            flowLayoutPanelProjectionObject.SuspendLayout();
            groupBoxOutline.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBoxDelimiter.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            groupBoxSize.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            groupBoxColor.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            tabPage2.SuspendLayout();
            flowLayoutPanel7.SuspendLayout();
            flowLayoutPanelCircleAxis.SuspendLayout();
            flowLayoutPanelCirclePlanes.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupBoxIndices.SuspendLayout();
            panelSpecifiedIndices.SuspendLayout();
            flowLayoutPanelAddRemove.SuspendLayout();
            flowLayoutPanelIndices.SuspendLayout();
            flowLayoutPanelIndexFilter.SuspendLayout();
            panel3.SuspendLayout();
            panel3DOptions.SuspendLayout();
            groupBox3DOptions.SuspendLayout();
            flowLayoutPanel9.SuspendLayout();
            flowLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarDepthFadingOut).BeginInit();
            flowLayoutPanel10.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(graphicsBox);
            // 
            // graphicsBox
            // 
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(graphicsBox, "graphicsBox");
            graphicsBox.Fonts = new System.Drawing.Font("Segoe UI", 9.75F);
            graphicsBox.Name = "graphicsBox";
            graphicsBox.TabStop = false;
            toolTip.SetToolTip(graphicsBox, resources.GetString("graphicsBox.ToolTip"));
            graphicsBox.MouseDown += graphicsBox_MouseDown;
            graphicsBox.MouseMove += graphicsBox_MouseMove;
            graphicsBox.MouseUp += graphicsBox_MouseUp;
            graphicsBox.Resize += formStereonet_Resize;
            // 
            // trackBarStrSize
            // 
            resources.ApplyResources(trackBarStrSize, "trackBarStrSize");
            trackBarStrSize.Maximum = 200;
            trackBarStrSize.Minimum = 1;
            trackBarStrSize.Name = "trackBarStrSize";
            trackBarStrSize.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarStrSize, resources.GetString("trackBarStrSize.ToolTip"));
            trackBarStrSize.Value = 80;
            trackBarStrSize.Scroll += trackBarStrSize_Scroll;
            // 
            // trackBarPointSize
            // 
            resources.ApplyResources(trackBarPointSize, "trackBarPointSize");
            trackBarPointSize.Maximum = 20;
            trackBarPointSize.Minimum = 1;
            trackBarPointSize.Name = "trackBarPointSize";
            trackBarPointSize.TickStyle = System.Windows.Forms.TickStyle.None;
            toolTip.SetToolTip(trackBarPointSize, resources.GetString("trackBarPointSize.ToolTip"));
            trackBarPointSize.Value = 5;
            trackBarPointSize.Scroll += trackBarStrSize_Scroll;
            // 
            // groupBoxMode
            // 
            captureExtender.SetCapture(groupBoxMode, true);
            groupBoxMode.Controls.Add(groupBoxSphere);
            groupBoxMode.Controls.Add(groupBoxProjectionScheme);
            groupBoxMode.Controls.Add(panel2);
            groupBoxMode.Controls.Add(groupBoxProjectionObject);
            resources.ApplyResources(groupBoxMode, "groupBoxMode");
            groupBoxMode.Name = "groupBoxMode";
            groupBoxMode.TabStop = false;
            // 
            // groupBoxSphere
            // 
            groupBoxSphere.Controls.Add(flowLayoutPanelSphere);
            resources.ApplyResources(groupBoxSphere, "groupBoxSphere");
            groupBoxSphere.Name = "groupBoxSphere";
            groupBoxSphere.TabStop = false;
            // 
            // flowLayoutPanelSphere
            // 
            resources.ApplyResources(flowLayoutPanelSphere, "flowLayoutPanelSphere");
            flowLayoutPanelSphere.Controls.Add(radioButtonUpperSphere);
            flowLayoutPanelSphere.Controls.Add(radioButtonLowerSphere);
            flowLayoutPanelSphere.Name = "flowLayoutPanelSphere";
            // 
            // radioButtonUpperSphere
            // 
            resources.ApplyResources(radioButtonUpperSphere, "radioButtonUpperSphere");
            radioButtonUpperSphere.Checked = true;
            radioButtonUpperSphere.Name = "radioButtonUpperSphere";
            radioButtonUpperSphere.TabStop = true;
            toolTip.SetToolTip(radioButtonUpperSphere, resources.GetString("radioButtonUpperSphere.ToolTip"));
            radioButtonUpperSphere.CheckedChanged += radioButtonUpperSphere_CheckedChanged;
            // 
            // radioButtonLowerSphere
            // 
            resources.ApplyResources(radioButtonLowerSphere, "radioButtonLowerSphere");
            radioButtonLowerSphere.Name = "radioButtonLowerSphere";
            toolTip.SetToolTip(radioButtonLowerSphere, resources.GetString("radioButtonLowerSphere.ToolTip"));
            // 
            // groupBoxProjectionScheme
            // 
            groupBoxProjectionScheme.Controls.Add(flowLayoutPanelProjectionScheme);
            resources.ApplyResources(groupBoxProjectionScheme, "groupBoxProjectionScheme");
            groupBoxProjectionScheme.Name = "groupBoxProjectionScheme";
            groupBoxProjectionScheme.TabStop = false;
            // 
            // flowLayoutPanelProjectionScheme
            // 
            resources.ApplyResources(flowLayoutPanelProjectionScheme, "flowLayoutPanelProjectionScheme");
            flowLayoutPanelProjectionScheme.Controls.Add(radioButtonWulff);
            flowLayoutPanelProjectionScheme.Controls.Add(radioButtonSchmidt);
            flowLayoutPanelProjectionScheme.Name = "flowLayoutPanelProjectionScheme";
            // 
            // radioButtonWulff
            // 
            resources.ApplyResources(radioButtonWulff, "radioButtonWulff");
            radioButtonWulff.Checked = true;
            radioButtonWulff.Name = "radioButtonWulff";
            radioButtonWulff.TabStop = true;
            toolTip.SetToolTip(radioButtonWulff, resources.GetString("radioButtonWulff.ToolTip"));
            radioButtonWulff.CheckedChanged += radioButtonWulff_CheckedChanged;
            // 
            // radioButtonSchmidt
            // 
            resources.ApplyResources(radioButtonSchmidt, "radioButtonSchmidt");
            radioButtonSchmidt.Name = "radioButtonSchmidt";
            toolTip.SetToolTip(radioButtonSchmidt, resources.GetString("radioButtonSchmidt.ToolTip"));
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.Name = "panel2";
            // 
            // groupBoxProjectionObject
            // 
            resources.ApplyResources(groupBoxProjectionObject, "groupBoxProjectionObject");
            groupBoxProjectionObject.Controls.Add(checkBoxReflectStructureFactor);
            groupBoxProjectionObject.Controls.Add(checkBoxShowIndexLabels);
            groupBoxProjectionObject.Controls.Add(flowLayoutPanelProjectionObject);
            groupBoxProjectionObject.Name = "groupBoxProjectionObject";
            groupBoxProjectionObject.TabStop = false;
            // 
            // checkBoxReflectStructureFactor
            // 
            resources.ApplyResources(checkBoxReflectStructureFactor, "checkBoxReflectStructureFactor");
            checkBoxReflectStructureFactor.Name = "checkBoxReflectStructureFactor";
            checkBoxReflectStructureFactor.CheckedChanged += checkBoxReflectStructureFactor_CheckedChanged;
            // 
            // checkBoxShowIndexLabels
            // 
            resources.ApplyResources(checkBoxShowIndexLabels, "checkBoxShowIndexLabels");
            checkBoxShowIndexLabels.Checked = true;
            checkBoxShowIndexLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowIndexLabels.Name = "checkBoxShowIndexLabels";
            checkBoxShowIndexLabels.UseVisualStyleBackColor = true;
            checkBoxShowIndexLabels.CheckedChanged += checkBoxShowIndexLabels_CheckedChanged;
            // 
            // flowLayoutPanelProjectionObject
            // 
            resources.ApplyResources(flowLayoutPanelProjectionObject, "flowLayoutPanelProjectionObject");
            flowLayoutPanelProjectionObject.Controls.Add(radioButtonAxes);
            flowLayoutPanelProjectionObject.Controls.Add(radioButtonPlanes);
            flowLayoutPanelProjectionObject.Controls.Add(radioButtonKikuchiLinePairs);
            flowLayoutPanelProjectionObject.Name = "flowLayoutPanelProjectionObject";
            // 
            // radioButtonAxes
            // 
            resources.ApplyResources(radioButtonAxes, "radioButtonAxes");
            radioButtonAxes.Checked = true;
            radioButtonAxes.Name = "radioButtonAxes";
            radioButtonAxes.TabStop = true;
            toolTip.SetToolTip(radioButtonAxes, resources.GetString("radioButtonAxes.ToolTip"));
            radioButtonAxes.CheckedChanged += radioButtonAxes_CheckedChanged;
            // 
            // radioButtonPlanes
            // 
            resources.ApplyResources(radioButtonPlanes, "radioButtonPlanes");
            radioButtonPlanes.Name = "radioButtonPlanes";
            toolTip.SetToolTip(radioButtonPlanes, resources.GetString("radioButtonPlanes.ToolTip"));
            radioButtonPlanes.CheckedChanged += radioButtonAxes_CheckedChanged;
            // 
            // radioButtonKikuchiLinePairs
            // 
            resources.ApplyResources(radioButtonKikuchiLinePairs, "radioButtonKikuchiLinePairs");
            radioButtonKikuchiLinePairs.Name = "radioButtonKikuchiLinePairs";
            toolTip.SetToolTip(radioButtonKikuchiLinePairs, resources.GetString("radioButtonKikuchiLinePairs.ToolTip"));
            radioButtonKikuchiLinePairs.CheckedChanged += radioButtonAxes_CheckedChanged;
            // 
            // labelXYpos
            // 
            resources.ApplyResources(labelXYpos, "labelXYpos");
            labelXYpos.Name = "labelXYpos";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            toolTip.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            // 
            // groupBoxOutline
            // 
            groupBoxOutline.Controls.Add(flowLayoutPanel5);
            resources.ApplyResources(groupBoxOutline, "groupBoxOutline");
            groupBoxOutline.Name = "groupBoxOutline";
            groupBoxOutline.TabStop = false;
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.Controls.Add(radioButtonOutlineEquator);
            flowLayoutPanel5.Controls.Add(radioButtonOutlinePole);
            flowLayoutPanel5.Controls.Add(checkBox1DegLine);
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            // 
            // radioButtonOutlineEquator
            // 
            resources.ApplyResources(radioButtonOutlineEquator, "radioButtonOutlineEquator");
            radioButtonOutlineEquator.Checked = true;
            radioButtonOutlineEquator.Name = "radioButtonOutlineEquator";
            radioButtonOutlineEquator.TabStop = true;
            toolTip.SetToolTip(radioButtonOutlineEquator, resources.GetString("radioButtonOutlineEquator.ToolTip"));
            radioButtonOutlineEquator.CheckedChanged += radioButtonOutlineEquator_CheckedChanged;
            // 
            // radioButtonOutlinePole
            // 
            resources.ApplyResources(radioButtonOutlinePole, "radioButtonOutlinePole");
            radioButtonOutlinePole.Name = "radioButtonOutlinePole";
            toolTip.SetToolTip(radioButtonOutlinePole, resources.GetString("radioButtonOutlinePole.ToolTip"));
            // 
            // checkBox1DegLine
            // 
            resources.ApplyResources(checkBox1DegLine, "checkBox1DegLine");
            checkBox1DegLine.Name = "checkBox1DegLine";
            toolTip.SetToolTip(checkBox1DegLine, resources.GetString("checkBox1DegLine.ToolTip"));
            checkBox1DegLine.CheckedChanged += checkBox1DegLine_CheckedChanged;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Controls.Add(tabPage3);
            tabControl.Controls.Add(tabPage4);
            resources.ApplyResources(tabControl, "tabControl");
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Click += tabControl_Click;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPage1, true);
            tabPage1.Controls.Add(groupBoxDelimiter);
            tabPage1.Controls.Add(groupBoxSize);
            tabPage1.Controls.Add(groupBoxOutline);
            tabPage1.Controls.Add(groupBoxColor);
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.Name = "tabPage1";
            // 
            // groupBoxDelimiter
            // 
            groupBoxDelimiter.Controls.Add(flowLayoutPanel4);
            resources.ApplyResources(groupBoxDelimiter, "groupBoxDelimiter");
            groupBoxDelimiter.Name = "groupBoxDelimiter";
            groupBoxDelimiter.TabStop = false;
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(radioButtonDelimiterNone);
            flowLayoutPanel4.Controls.Add(radioButtonDelimiterSpace);
            flowLayoutPanel4.Controls.Add(radioButtonDelimiterComma);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // radioButtonDelimiterNone
            // 
            resources.ApplyResources(radioButtonDelimiterNone, "radioButtonDelimiterNone");
            radioButtonDelimiterNone.Checked = true;
            radioButtonDelimiterNone.Name = "radioButtonDelimiterNone";
            radioButtonDelimiterNone.TabStop = true;
            toolTip.SetToolTip(radioButtonDelimiterNone, resources.GetString("radioButtonDelimiterNone.ToolTip"));
            radioButtonDelimiterNone.CheckedChanged += radioButtonDelimiterNone_CheckedChanged;
            // 
            // radioButtonDelimiterSpace
            // 
            resources.ApplyResources(radioButtonDelimiterSpace, "radioButtonDelimiterSpace");
            radioButtonDelimiterSpace.Name = "radioButtonDelimiterSpace";
            toolTip.SetToolTip(radioButtonDelimiterSpace, resources.GetString("radioButtonDelimiterSpace.ToolTip"));
            radioButtonDelimiterSpace.CheckedChanged += radioButtonDelimiterNone_CheckedChanged;
            // 
            // radioButtonDelimiterComma
            // 
            resources.ApplyResources(radioButtonDelimiterComma, "radioButtonDelimiterComma");
            radioButtonDelimiterComma.Name = "radioButtonDelimiterComma";
            toolTip.SetToolTip(radioButtonDelimiterComma, resources.GetString("radioButtonDelimiterComma.ToolTip"));
            radioButtonDelimiterComma.CheckedChanged += radioButtonDelimiterNone_CheckedChanged;
            // 
            // groupBoxSize
            // 
            groupBoxSize.Controls.Add(flowLayoutPanel6);
            resources.ApplyResources(groupBoxSize, "groupBoxSize");
            groupBoxSize.Name = "groupBoxSize";
            groupBoxSize.TabStop = false;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.Controls.Add(label1);
            flowLayoutPanel6.Controls.Add(trackBarStrSize);
            flowLayoutPanel6.Controls.Add(label6);
            flowLayoutPanel6.Controls.Add(trackBarPointSize);
            resources.ApplyResources(flowLayoutPanel6, "flowLayoutPanel6");
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            // 
            // groupBoxColor
            // 
            groupBoxColor.Controls.Add(flowLayoutPanel3);
            resources.ApplyResources(groupBoxColor, "groupBoxColor");
            groupBoxColor.Name = "groupBoxColor";
            groupBoxColor.TabStop = false;
            toolTip.SetToolTip(groupBoxColor, resources.GetString("groupBoxColor.ToolTip"));
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(colorControlBackGround);
            flowLayoutPanel3.Controls.Add(colorControl90DegLine);
            flowLayoutPanel3.Controls.Add(colorControl10DegLine);
            flowLayoutPanel3.Controls.Add(colorControl1DegLine);
            flowLayoutPanel3.Controls.Add(colorControlKikuchi);
            flowLayoutPanel3.Controls.Add(colorControlString);
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // colorControlBackGround
            // 
            colorControlBackGround.Argb = -1;
            resources.ApplyResources(colorControlBackGround, "colorControlBackGround");
            colorControlBackGround.BackColor = System.Drawing.Color.White;
            colorControlBackGround.Blue = 255;
            colorControlBackGround.BlueF = 1F;
            colorControlBackGround.BoxSize = new System.Drawing.Size(20, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlBackGround.Green = 255;
            colorControlBackGround.GreenF = 1F;
            colorControlBackGround.Name = "colorControlBackGround";
            colorControlBackGround.Red = 255;
            colorControlBackGround.RedF = 1F;
            colorControlBackGround.TabStop = false;
            colorControlBackGround.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControl90DegLine
            // 
            colorControl90DegLine.Argb = -12829441;
            resources.ApplyResources(colorControl90DegLine, "colorControl90DegLine");
            colorControl90DegLine.BackColor = System.Drawing.Color.Blue;
            colorControl90DegLine.Blue = 255;
            colorControl90DegLine.BlueF = 1F;
            colorControl90DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl90DegLine.Color = System.Drawing.Color.FromArgb(60, 60, 255);
            colorControl90DegLine.Green = 60;
            colorControl90DegLine.GreenF = 0.235294119F;
            colorControl90DegLine.Name = "colorControl90DegLine";
            colorControl90DegLine.Red = 60;
            colorControl90DegLine.RedF = 0.235294119F;
            colorControl90DegLine.TabStop = false;
            colorControl90DegLine.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControl10DegLine
            // 
            colorControl10DegLine.Argb = -7697665;
            resources.ApplyResources(colorControl10DegLine, "colorControl10DegLine");
            colorControl10DegLine.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
            colorControl10DegLine.Blue = 255;
            colorControl10DegLine.BlueF = 1F;
            colorControl10DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl10DegLine.Color = System.Drawing.Color.FromArgb(138, 138, 255);
            colorControl10DegLine.Green = 138;
            colorControl10DegLine.GreenF = 0.5411765F;
            colorControl10DegLine.Name = "colorControl10DegLine";
            colorControl10DegLine.Red = 138;
            colorControl10DegLine.RedF = 0.5411765F;
            colorControl10DegLine.TabStop = false;
            colorControl10DegLine.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControl1DegLine
            // 
            colorControl1DegLine.Argb = -4144897;
            resources.ApplyResources(colorControl1DegLine, "colorControl1DegLine");
            colorControl1DegLine.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControl1DegLine.Blue = 255;
            colorControl1DegLine.BlueF = 1F;
            colorControl1DegLine.BoxSize = new System.Drawing.Size(20, 20);
            colorControl1DegLine.Color = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControl1DegLine.Green = 192;
            colorControl1DegLine.GreenF = 0.7529412F;
            colorControl1DegLine.Name = "colorControl1DegLine";
            colorControl1DegLine.Red = 192;
            colorControl1DegLine.RedF = 0.7529412F;
            colorControl1DegLine.TabStop = false;
            colorControl1DegLine.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControlKikuchi
            // 
            colorControlKikuchi.Argb = -16777088;
            resources.ApplyResources(colorControlKikuchi, "colorControlKikuchi");
            colorControlKikuchi.BackColor = System.Drawing.Color.White;
            colorControlKikuchi.Blue = 128;
            colorControlKikuchi.BlueF = 0.5019608F;
            colorControlKikuchi.BoxSize = new System.Drawing.Size(20, 20);
            colorControlKikuchi.Color = System.Drawing.Color.FromArgb(0, 0, 128);
            colorControlKikuchi.Green = 0;
            colorControlKikuchi.GreenF = 0F;
            colorControlKikuchi.Name = "colorControlKikuchi";
            colorControlKikuchi.Red = 0;
            colorControlKikuchi.RedF = 0F;
            colorControlKikuchi.TabStop = false;
            colorControlKikuchi.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControlString
            // 
            colorControlString.Argb = -16777088;
            resources.ApplyResources(colorControlString, "colorControlString");
            colorControlString.BackColor = System.Drawing.Color.Black;
            colorControlString.Blue = 128;
            colorControlString.BlueF = 0.5019608F;
            colorControlString.BoxSize = new System.Drawing.Size(20, 20);
            colorControlString.Color = System.Drawing.Color.FromArgb(0, 0, 128);
            colorControlString.Green = 0;
            colorControlString.GreenF = 0F;
            colorControlString.Name = "colorControlString";
            colorControlString.Red = 0;
            colorControlString.RedF = 0F;
            colorControlString.TabStop = false;
            colorControlString.ColorChanged += colorControl_ColorChanged;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPage2, true);
            tabPage2.Controls.Add(checkedListBoxCircles);
            tabPage2.Controls.Add(flowLayoutPanel10);
            tabPage2.Controls.Add(flowLayoutPanel7);
            resources.ApplyResources(tabPage2, "tabPage2");
            tabPage2.Name = "tabPage2";
            // 
            // flowLayoutPanel7
            // 
            flowLayoutPanel7.Controls.Add(radioButtonCircleByAxis);
            flowLayoutPanel7.Controls.Add(flowLayoutPanelCircleAxis);
            flowLayoutPanel7.Controls.Add(radioButtonCircleByPlanes);
            flowLayoutPanel7.Controls.Add(flowLayoutPanelCirclePlanes);
            resources.ApplyResources(flowLayoutPanel7, "flowLayoutPanel7");
            flowLayoutPanel7.Name = "flowLayoutPanel7";
            // 
            // radioButtonCircleByAxis
            // 
            resources.ApplyResources(radioButtonCircleByAxis, "radioButtonCircleByAxis");
            radioButtonCircleByAxis.Checked = true;
            radioButtonCircleByAxis.Name = "radioButtonCircleByAxis";
            radioButtonCircleByAxis.TabStop = true;
            radioButtonCircleByAxis.UseVisualStyleBackColor = true;
            radioButtonCircleByAxis.CheckedChanged += radioButtonCircleByAxis_CheckedChanged;
            // 
            // flowLayoutPanelCircleAxis
            // 
            resources.ApplyResources(flowLayoutPanelCircleAxis, "flowLayoutPanelCircleAxis");
            flowLayoutPanelCircleAxis.Controls.Add(indexControlAxis);
            flowLayoutPanelCircleAxis.Name = "flowLayoutPanelCircleAxis";
            // 
            // indexControlAxis
            // 
            resources.ApplyResources(indexControlAxis, "indexControlAxis");
            indexControlAxis.BoxWidth = 40;
            indexControlAxis.LabelVisible = false;
            indexControlAxis.Mode = IndexControl.ModeEnum.Axis;
            indexControlAxis.Name = "indexControlAxis";
            indexControlAxis.Values = ((int, int, int))resources.GetObject("indexControlAxis.Values");
            // 
            // radioButtonCircleByPlanes
            // 
            resources.ApplyResources(radioButtonCircleByPlanes, "radioButtonCircleByPlanes");
            radioButtonCircleByPlanes.Name = "radioButtonCircleByPlanes";
            radioButtonCircleByPlanes.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanelCirclePlanes
            // 
            resources.ApplyResources(flowLayoutPanelCirclePlanes, "flowLayoutPanelCirclePlanes");
            flowLayoutPanelCirclePlanes.Controls.Add(flowLayoutPanel1);
            flowLayoutPanelCirclePlanes.Controls.Add(flowLayoutPanel2);
            flowLayoutPanelCirclePlanes.Name = "flowLayoutPanelCirclePlanes";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(indexControlCirclePlane1);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // indexControlCirclePlane1
            // 
            resources.ApplyResources(indexControlCirclePlane1, "indexControlCirclePlane1");
            indexControlCirclePlane1.BoxWidth = 42;
            indexControlCirclePlane1.LabelVisible = false;
            indexControlCirclePlane1.Name = "indexControlCirclePlane1";
            indexControlCirclePlane1.SubScript = "1";
            indexControlCirclePlane1.Values = ((int, int, int))resources.GetObject("indexControlCirclePlane1.Values");
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(indexControlCirclePlane2);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // indexControlCirclePlane2
            // 
            resources.ApplyResources(indexControlCirclePlane2, "indexControlCirclePlane2");
            indexControlCirclePlane2.BoxWidth = 42;
            indexControlCirclePlane2.LabelVisible = false;
            indexControlCirclePlane2.Name = "indexControlCirclePlane2";
            indexControlCirclePlane2.SubScript = "2";
            indexControlCirclePlane2.Values = ((int, int, int))resources.GetObject("indexControlCirclePlane2.Values");
            // 
            // colorControlGreatCircle
            // 
            colorControlGreatCircle.Argb = -32768;
            resources.ApplyResources(colorControlGreatCircle, "colorControlGreatCircle");
            colorControlGreatCircle.BackColor = System.Drawing.SystemColors.Control;
            colorControlGreatCircle.Blue = 0;
            colorControlGreatCircle.BlueF = 0F;
            colorControlGreatCircle.BoxSize = new System.Drawing.Size(20, 20);
            colorControlGreatCircle.Color = System.Drawing.Color.FromArgb(255, 128, 0);
            colorControlGreatCircle.Green = 128;
            colorControlGreatCircle.GreenF = 0.5019608F;
            colorControlGreatCircle.Name = "colorControlGreatCircle";
            colorControlGreatCircle.Red = 255;
            colorControlGreatCircle.RedF = 1F;
            colorControlGreatCircle.ColorChanged += colorControl_ColorChanged;
            // 
            // buttonAddCircle
            // 
            resources.ApplyResources(buttonAddCircle, "buttonAddCircle");
            buttonAddCircle.BackColor = System.Drawing.Color.SteelBlue;
            buttonAddCircle.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonAddCircle.Name = "buttonAddCircle";
            buttonAddCircle.UseVisualStyleBackColor = false;
            buttonAddCircle.Click += buttonAddCircle_Click;
            // 
            // buttonDeleteCircle
            // 
            resources.ApplyResources(buttonDeleteCircle, "buttonDeleteCircle");
            buttonDeleteCircle.BackColor = System.Drawing.Color.IndianRed;
            buttonDeleteCircle.ForeColor = System.Drawing.Color.White;
            buttonDeleteCircle.Name = "buttonDeleteCircle";
            buttonDeleteCircle.UseVisualStyleBackColor = false;
            buttonDeleteCircle.Click += buttonDeleteCircle_Click;
            // 
            // checkedListBoxCircles
            // 
            resources.ApplyResources(checkedListBoxCircles, "checkedListBoxCircles");
            checkedListBoxCircles.FormattingEnabled = true;
            checkedListBoxCircles.Name = "checkedListBoxCircles";
            checkedListBoxCircles.ItemCheck += checkedListBoxCircles_ItemCheck;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(buttonYusaModeStop);
            tabPage3.Controls.Add(buttonYusaModeStart);
            tabPage3.Controls.Add(radioButtonRotationalScan);
            tabPage3.Controls.Add(radioButtonZigzagScan);
            tabPage3.Controls.Add(checkBox3);
            tabPage3.Controls.Add(checkBox2);
            tabPage3.Controls.Add(checkBox1);
            tabPage3.Controls.Add(label25);
            tabPage3.Controls.Add(label22);
            tabPage3.Controls.Add(numericBoxRxSpeed);
            tabPage3.Controls.Add(numericBoxRySpeed);
            tabPage3.Controls.Add(numericBoxRzSpeed);
            tabPage3.Controls.Add(numericBoxTotalTime);
            tabPage3.Controls.Add(numericBoxAngularSpeed);
            tabPage3.Controls.Add(numericBoxRyStep);
            tabPage3.Controls.Add(numericBoxRadialAngle);
            tabPage3.Controls.Add(numericBoxRyOscillation);
            tabPage3.Controls.Add(numericBoxRzOscillation);
            resources.ApplyResources(tabPage3, "tabPage3");
            tabPage3.Name = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonYusaModeStop
            // 
            resources.ApplyResources(buttonYusaModeStop, "buttonYusaModeStop");
            buttonYusaModeStop.Name = "buttonYusaModeStop";
            buttonYusaModeStop.UseVisualStyleBackColor = true;
            buttonYusaModeStop.Click += buttonYusaModeStop_Click;
            // 
            // buttonYusaModeStart
            // 
            resources.ApplyResources(buttonYusaModeStart, "buttonYusaModeStart");
            buttonYusaModeStart.BackColor = System.Drawing.Color.SteelBlue;
            buttonYusaModeStart.ForeColor = System.Drawing.Color.White;
            buttonYusaModeStart.Name = "buttonYusaModeStart";
            buttonYusaModeStart.UseVisualStyleBackColor = false;
            buttonYusaModeStart.Click += buttonYusaModeStart_Click;
            // 
            // radioButtonRotationalScan
            // 
            resources.ApplyResources(radioButtonRotationalScan, "radioButtonRotationalScan");
            radioButtonRotationalScan.Name = "radioButtonRotationalScan";
            radioButtonRotationalScan.UseVisualStyleBackColor = true;
            // 
            // radioButtonZigzagScan
            // 
            resources.ApplyResources(radioButtonZigzagScan, "radioButtonZigzagScan");
            radioButtonZigzagScan.Checked = true;
            radioButtonZigzagScan.Name = "radioButtonZigzagScan";
            radioButtonZigzagScan.TabStop = true;
            radioButtonZigzagScan.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            resources.ApplyResources(checkBox3, "checkBox3");
            checkBox3.Name = "checkBox3";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(checkBox2, "checkBox2");
            checkBox2.Name = "checkBox2";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            resources.ApplyResources(checkBox1, "checkBox1");
            checkBox1.Name = "checkBox1";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            resources.ApplyResources(label25, "label25");
            label25.Name = "label25";
            // 
            // label22
            // 
            resources.ApplyResources(label22, "label22");
            label22.Name = "label22";
            // 
            // numericBoxRxSpeed
            // 
            resources.ApplyResources(numericBoxRxSpeed, "numericBoxRxSpeed");
            numericBoxRxSpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRxSpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRxSpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRxSpeed.Name = "numericBoxRxSpeed";
            numericBoxRxSpeed.RadianValue = 0.31415926535897931D;
            numericBoxRxSpeed.SkipEventDuringInput = false;
            numericBoxRxSpeed.SmartIncrement = true;
            numericBoxRxSpeed.ThousandsSeparator = true;
            numericBoxRxSpeed.Value = 18D;
            numericBoxRxSpeed.ValueFontSize = 8F;
            // 
            // numericBoxRySpeed
            // 
            resources.ApplyResources(numericBoxRySpeed, "numericBoxRySpeed");
            numericBoxRySpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRySpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRySpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRySpeed.Name = "numericBoxRySpeed";
            numericBoxRySpeed.RadianValue = 0.017453292519943295D;
            numericBoxRySpeed.SkipEventDuringInput = false;
            numericBoxRySpeed.SmartIncrement = true;
            numericBoxRySpeed.ThousandsSeparator = true;
            numericBoxRySpeed.Value = 1D;
            numericBoxRySpeed.ValueFontSize = 8F;
            // 
            // numericBoxRzSpeed
            // 
            resources.ApplyResources(numericBoxRzSpeed, "numericBoxRzSpeed");
            numericBoxRzSpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRzSpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRzSpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRzSpeed.Name = "numericBoxRzSpeed";
            numericBoxRzSpeed.RadianValue = 0.034906585039886591D;
            numericBoxRzSpeed.SkipEventDuringInput = false;
            numericBoxRzSpeed.SmartIncrement = true;
            numericBoxRzSpeed.ThousandsSeparator = true;
            numericBoxRzSpeed.Value = 2D;
            numericBoxRzSpeed.ValueFontSize = 8F;
            // 
            // numericBoxTotalTime
            // 
            resources.ApplyResources(numericBoxTotalTime, "numericBoxTotalTime");
            numericBoxTotalTime.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTotalTime.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTotalTime.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTotalTime.Name = "numericBoxTotalTime";
            numericBoxTotalTime.RadianValue = 1.7453292519943295D;
            numericBoxTotalTime.SkipEventDuringInput = false;
            numericBoxTotalTime.SmartIncrement = true;
            numericBoxTotalTime.ThousandsSeparator = true;
            numericBoxTotalTime.Value = 100D;
            numericBoxTotalTime.ValueFontSize = 8F;
            // 
            // numericBoxAngularSpeed
            // 
            resources.ApplyResources(numericBoxAngularSpeed, "numericBoxAngularSpeed");
            numericBoxAngularSpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxAngularSpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxAngularSpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxAngularSpeed.Name = "numericBoxAngularSpeed";
            numericBoxAngularSpeed.RadianValue = 0.52359877559829882D;
            numericBoxAngularSpeed.SkipEventDuringInput = false;
            numericBoxAngularSpeed.SmartIncrement = true;
            numericBoxAngularSpeed.ThousandsSeparator = true;
            numericBoxAngularSpeed.Value = 30D;
            numericBoxAngularSpeed.ValueFontSize = 8F;
            // 
            // numericBoxRyStep
            // 
            resources.ApplyResources(numericBoxRyStep, "numericBoxRyStep");
            numericBoxRyStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRyStep.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRyStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRyStep.Name = "numericBoxRyStep";
            numericBoxRyStep.RadianValue = 0.0034906585039886592D;
            numericBoxRyStep.SkipEventDuringInput = false;
            numericBoxRyStep.SmartIncrement = true;
            numericBoxRyStep.ThousandsSeparator = true;
            numericBoxRyStep.Value = 0.2D;
            numericBoxRyStep.ValueFontSize = 8F;
            // 
            // numericBoxRadialAngle
            // 
            resources.ApplyResources(numericBoxRadialAngle, "numericBoxRadialAngle");
            numericBoxRadialAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRadialAngle.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRadialAngle.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRadialAngle.Name = "numericBoxRadialAngle";
            numericBoxRadialAngle.RadianValue = 0.13962634015954636D;
            numericBoxRadialAngle.SkipEventDuringInput = false;
            numericBoxRadialAngle.SmartIncrement = true;
            numericBoxRadialAngle.ThousandsSeparator = true;
            numericBoxRadialAngle.Value = 8D;
            numericBoxRadialAngle.ValueFontSize = 8F;
            // 
            // numericBoxRyOscillation
            // 
            resources.ApplyResources(numericBoxRyOscillation, "numericBoxRyOscillation");
            numericBoxRyOscillation.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRyOscillation.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRyOscillation.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRyOscillation.Name = "numericBoxRyOscillation";
            numericBoxRyOscillation.RadianValue = 0.13962634015954636D;
            numericBoxRyOscillation.SkipEventDuringInput = false;
            numericBoxRyOscillation.SmartIncrement = true;
            numericBoxRyOscillation.ThousandsSeparator = true;
            numericBoxRyOscillation.Value = 8D;
            numericBoxRyOscillation.ValueFontSize = 8F;
            // 
            // numericBoxRzOscillation
            // 
            resources.ApplyResources(numericBoxRzOscillation, "numericBoxRzOscillation");
            numericBoxRzOscillation.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRzOscillation.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRzOscillation.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRzOscillation.Name = "numericBoxRzOscillation";
            numericBoxRzOscillation.RadianValue = 0.13962634015954636D;
            numericBoxRzOscillation.SkipEventDuringInput = false;
            numericBoxRzOscillation.SmartIncrement = true;
            numericBoxRzOscillation.ThousandsSeparator = true;
            numericBoxRzOscillation.Value = 8D;
            numericBoxRzOscillation.ValueFontSize = 8F;
            // 
            // tabPage4
            // 
            tabPage4.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPage4, true);
            tabPage4.Controls.Add(waveLengthControl);
            resources.ApplyResources(tabPage4, "tabPage4");
            tabPage4.Name = "tabPage4";
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            waveLengthControl.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl.Energy = 20D;
            waveLengthControl.Monochrome = true;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = true;
            waveLengthControl.WaveLength = 0.008588514105D;
            waveLengthControl.WaveSource = WaveSource.Electron;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            waveLengthControl.XrayWaveSourceLine = XrayLine.Ka1;
            waveLengthControl.WavelengthChanged += waveLengthControl_WaveSourceChanged;
            waveLengthControl.WaveSourceChanged += waveLengthControl_WaveSourceChanged;
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveImageToolStripMenuItem, toolStripMenuItem1, toolStripSeparator1, toolStripMenuItem2, toolStripSeparator2, pageSetupToolStripMenuItem, printPreviewToolStripMenuItem, printToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // saveImageToolStripMenuItem
            // 
            saveImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { asImageToolStripMenuItem, asMetafileToolStripMenuItem1 });
            saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            resources.ApplyResources(saveImageToolStripMenuItem, "saveImageToolStripMenuItem");
            // 
            // asImageToolStripMenuItem
            // 
            asImageToolStripMenuItem.Name = "asImageToolStripMenuItem";
            resources.ApplyResources(asImageToolStripMenuItem, "asImageToolStripMenuItem");
            asImageToolStripMenuItem.Click += saveImageToolStripMenuItem_Click;
            // 
            // asMetafileToolStripMenuItem1
            // 
            resources.ApplyResources(asMetafileToolStripMenuItem1, "asMetafileToolStripMenuItem1");
            asMetafileToolStripMenuItem1.Name = "asMetafileToolStripMenuItem1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { asBitmapToolStripMenuItem, asMetafileToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // asBitmapToolStripMenuItem
            // 
            asBitmapToolStripMenuItem.Name = "asBitmapToolStripMenuItem";
            resources.ApplyResources(asBitmapToolStripMenuItem, "asBitmapToolStripMenuItem");
            asBitmapToolStripMenuItem.Click += copyImageToClipboardToolStripMenuItem_Click;
            // 
            // asMetafileToolStripMenuItem
            // 
            asMetafileToolStripMenuItem.Name = "asMetafileToolStripMenuItem";
            resources.ApplyResources(asMetafileToolStripMenuItem, "asMetafileToolStripMenuItem");
            asMetafileToolStripMenuItem.Click += copyMetafileToClipboardToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemSaveMovieStereonet, toolStripMenuItemSaveMovie3D });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // toolStripMenuItemSaveMovieStereonet
            // 
            toolStripMenuItemSaveMovieStereonet.Name = "toolStripMenuItemSaveMovieStereonet";
            resources.ApplyResources(toolStripMenuItemSaveMovieStereonet, "toolStripMenuItemSaveMovieStereonet");
            toolStripMenuItemSaveMovieStereonet.Click += toolStripMenuItemSaveMovieStereonet_Click;
            // 
            // toolStripMenuItemSaveMovie3D
            // 
            toolStripMenuItemSaveMovie3D.Name = "toolStripMenuItemSaveMovie3D";
            resources.ApplyResources(toolStripMenuItemSaveMovie3D, "toolStripMenuItemSaveMovie3D");
            toolStripMenuItemSaveMovie3D.Click += toolStripMenuItemSaveMovie3D_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
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
            // toolTip
            // 
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.ReshowDelay = 100;
            // 
            // radioButtonRange
            // 
            resources.ApplyResources(radioButtonRange, "radioButtonRange");
            radioButtonRange.Checked = true;
            radioButtonRange.Name = "radioButtonRange";
            radioButtonRange.TabStop = true;
            toolTip.SetToolTip(radioButtonRange, resources.GetString("radioButtonRange.ToolTip"));
            radioButtonRange.UseVisualStyleBackColor = true;
            radioButtonRange.CheckedChanged += radioButtonRange_CheckedChanged;
            // 
            // radioButtonSpecifiedIndices
            // 
            resources.ApplyResources(radioButtonSpecifiedIndices, "radioButtonSpecifiedIndices");
            radioButtonSpecifiedIndices.Name = "radioButtonSpecifiedIndices";
            toolTip.SetToolTip(radioButtonSpecifiedIndices, resources.GetString("radioButtonSpecifiedIndices.ToolTip"));
            radioButtonSpecifiedIndices.UseVisualStyleBackColor = true;
            radioButtonSpecifiedIndices.CheckedChanged += radioButtonRange_CheckedChanged;
            // 
            // radioButtonHighStructureFactor
            // 
            resources.ApplyResources(radioButtonHighStructureFactor, "radioButtonHighStructureFactor");
            radioButtonHighStructureFactor.Name = "radioButtonHighStructureFactor";
            toolTip.SetToolTip(radioButtonHighStructureFactor, resources.GetString("radioButtonHighStructureFactor.ToolTip"));
            radioButtonHighStructureFactor.UseVisualStyleBackColor = true;
            radioButtonHighStructureFactor.CheckedChanged += radioButtonRange_CheckedChanged;
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
            // groupBoxIndices
            // 
            captureExtender.SetCapture(groupBoxIndices, true);
            groupBoxIndices.Controls.Add(panelSpecifiedIndices);
            groupBoxIndices.Controls.Add(flowLayoutPanelIndices);
            resources.ApplyResources(groupBoxIndices, "groupBoxIndices");
            groupBoxIndices.Name = "groupBoxIndices";
            groupBoxIndices.TabStop = false;
            // 
            // panelSpecifiedIndices
            // 
            panelSpecifiedIndices.Controls.Add(listBoxSpecifiedIndices);
            panelSpecifiedIndices.Controls.Add(flowLayoutPanelAddRemove);
            resources.ApplyResources(panelSpecifiedIndices, "panelSpecifiedIndices");
            panelSpecifiedIndices.Name = "panelSpecifiedIndices";
            // 
            // listBoxSpecifiedIndices
            // 
            resources.ApplyResources(listBoxSpecifiedIndices, "listBoxSpecifiedIndices");
            listBoxSpecifiedIndices.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            listBoxSpecifiedIndices.FormattingEnabled = true;
            listBoxSpecifiedIndices.MultiColumn = true;
            listBoxSpecifiedIndices.Name = "listBoxSpecifiedIndices";
            listBoxSpecifiedIndices.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            listBoxSpecifiedIndices.DrawItem += listBoxSpecifiedIndices_DrawItem;
            listBoxSpecifiedIndices.SelectedIndexChanged += listBoxSpecifiedIndices_SelectedIndexChanged;
            // 
            // flowLayoutPanelAddRemove
            // 
            resources.ApplyResources(flowLayoutPanelAddRemove, "flowLayoutPanelAddRemove");
            flowLayoutPanelAddRemove.Controls.Add(buttonAddIndex);
            flowLayoutPanelAddRemove.Controls.Add(buttonRemoveIndex);
            flowLayoutPanelAddRemove.Controls.Add(colorControlIndex);
            flowLayoutPanelAddRemove.Controls.Add(checkBoxRotateColor);
            flowLayoutPanelAddRemove.Controls.Add(checkBoxIncludingEquivalentPlanes);
            flowLayoutPanelAddRemove.Name = "flowLayoutPanelAddRemove";
            // 
            // buttonAddIndex
            // 
            resources.ApplyResources(buttonAddIndex, "buttonAddIndex");
            buttonAddIndex.BackColor = System.Drawing.Color.SteelBlue;
            buttonAddIndex.ForeColor = System.Drawing.Color.White;
            buttonAddIndex.Name = "buttonAddIndex";
            buttonAddIndex.UseVisualStyleBackColor = false;
            buttonAddIndex.Click += buttonAddIndex_Click;
            // 
            // buttonRemoveIndex
            // 
            resources.ApplyResources(buttonRemoveIndex, "buttonRemoveIndex");
            buttonRemoveIndex.BackColor = System.Drawing.Color.IndianRed;
            buttonRemoveIndex.ForeColor = System.Drawing.Color.White;
            buttonRemoveIndex.Name = "buttonRemoveIndex";
            buttonRemoveIndex.UseVisualStyleBackColor = false;
            buttonRemoveIndex.Click += buttonRemoveIndex_Click;
            // 
            // colorControlIndex
            // 
            colorControlIndex.Argb = -65536;
            resources.ApplyResources(colorControlIndex, "colorControlIndex");
            colorControlIndex.BackColor = System.Drawing.SystemColors.Control;
            colorControlIndex.Blue = 0;
            colorControlIndex.BlueF = 0F;
            colorControlIndex.BoxSize = new System.Drawing.Size(20, 20);
            colorControlIndex.Color = System.Drawing.Color.FromArgb(255, 0, 0);
            colorControlIndex.Green = 0;
            colorControlIndex.GreenF = 0F;
            colorControlIndex.Name = "colorControlIndex";
            colorControlIndex.Red = 255;
            colorControlIndex.RedF = 1F;
            colorControlIndex.ColorChanged += colorControlIndex_ColorChanged;
            // 
            // checkBoxRotateColor
            // 
            resources.ApplyResources(checkBoxRotateColor, "checkBoxRotateColor");
            checkBoxRotateColor.Name = "checkBoxRotateColor";
            checkBoxRotateColor.UseVisualStyleBackColor = true;
            checkBoxRotateColor.CheckedChanged += checkBoxRotateColor_CheckedChanged;
            // 
            // checkBoxIncludingEquivalentPlanes
            // 
            resources.ApplyResources(checkBoxIncludingEquivalentPlanes, "checkBoxIncludingEquivalentPlanes");
            checkBoxIncludingEquivalentPlanes.Checked = true;
            checkBoxIncludingEquivalentPlanes.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxIncludingEquivalentPlanes.Name = "checkBoxIncludingEquivalentPlanes";
            checkBoxIncludingEquivalentPlanes.UseVisualStyleBackColor = true;
            checkBoxIncludingEquivalentPlanes.CheckedChanged += checkBoxIncludingEquivalentPlanes_CheckedChanged;
            // 
            // flowLayoutPanelIndices
            // 
            resources.ApplyResources(flowLayoutPanelIndices, "flowLayoutPanelIndices");
            flowLayoutPanelIndices.Controls.Add(flowLayoutPanelIndexFilter);
            flowLayoutPanelIndices.Controls.Add(indexControlDrawing);
            flowLayoutPanelIndices.Name = "flowLayoutPanelIndices";
            // 
            // flowLayoutPanelIndexFilter
            // 
            resources.ApplyResources(flowLayoutPanelIndexFilter, "flowLayoutPanelIndexFilter");
            flowLayoutPanelIndexFilter.Controls.Add(radioButtonRange);
            flowLayoutPanelIndexFilter.Controls.Add(radioButtonSpecifiedIndices);
            flowLayoutPanelIndexFilter.Controls.Add(radioButtonHighStructureFactor);
            flowLayoutPanelIndexFilter.Controls.Add(numericBoxHighStructureFactor);
            flowLayoutPanelIndexFilter.Name = "flowLayoutPanelIndexFilter";
            // 
            // numericBoxHighStructureFactor
            // 
            numericBoxHighStructureFactor.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(numericBoxHighStructureFactor, "numericBoxHighStructureFactor");
            numericBoxHighStructureFactor.Maximum = 1000D;
            numericBoxHighStructureFactor.Minimum = 1D;
            numericBoxHighStructureFactor.Name = "numericBoxHighStructureFactor";
            numericBoxHighStructureFactor.RadianValue = 1.7453292519943295D;
            numericBoxHighStructureFactor.ShowUpDown = true;
            numericBoxHighStructureFactor.SmartIncrement = true;
            numericBoxHighStructureFactor.Value = 100D;
            numericBoxHighStructureFactor.ValueFontSize = 9F;
            numericBoxHighStructureFactor.ValueChanged += numericBoxHighStructureFactor_ValueChanged;
            // 
            // indexControlDrawing
            // 
            resources.ApplyResources(indexControlDrawing, "indexControlDrawing");
            indexControlDrawing.BoxWidth = 42;
            indexControlDrawing.Mode = IndexControl.ModeEnum.Axis;
            indexControlDrawing.Name = "indexControlDrawing";
            indexControlDrawing.PlusMinus = true;
            indexControlDrawing.Values = ((int, int, int))resources.GetObject("indexControlDrawing.Values");
            indexControlDrawing.ValueChanged += numericUpDown_ValueChanged;
            // 
            // panel3
            // 
            panel3.Controls.Add(groupBoxIndices);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(panel1);
            panel3.Controls.Add(groupBoxMode);
            panel3.Controls.Add(panel3DOptions);
            resources.ApplyResources(panel3, "panel3");
            panel3.Name = "panel3";
            // 
            // panel4
            // 
            resources.ApplyResources(panel4, "panel4");
            panel4.Name = "panel4";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // panel3DOptions
            // 
            resources.ApplyResources(panel3DOptions, "panel3DOptions");
            captureExtender.SetCapture(panel3DOptions, true);
            panel3DOptions.Controls.Add(checkBoxDisplay3D);
            panel3DOptions.Controls.Add(groupBox3DOptions);
            panel3DOptions.Name = "panel3DOptions";
            // 
            // checkBoxDisplay3D
            // 
            resources.ApplyResources(checkBoxDisplay3D, "checkBoxDisplay3D");
            checkBoxDisplay3D.Name = "checkBoxDisplay3D";
            checkBoxDisplay3D.UseVisualStyleBackColor = true;
            checkBoxDisplay3D.CheckedChanged += checkBoxDisplay3D_CheckedChanged;
            // 
            // groupBox3DOptions
            // 
            resources.ApplyResources(groupBox3DOptions, "groupBox3DOptions");
            groupBox3DOptions.Controls.Add(flowLayoutPanel9);
            groupBox3DOptions.Controls.Add(flowLayoutPanel8);
            groupBox3DOptions.Name = "groupBox3DOptions";
            groupBox3DOptions.TabStop = false;
            // 
            // flowLayoutPanel9
            // 
            flowLayoutPanel9.Controls.Add(button3D_reset);
            flowLayoutPanel9.Controls.Add(checkBox3dOptionLabel);
            flowLayoutPanel9.Controls.Add(checkBox3dOptionSemisphere);
            flowLayoutPanel9.Controls.Add(checkBox3dOptionStereonet);
            resources.ApplyResources(flowLayoutPanel9, "flowLayoutPanel9");
            flowLayoutPanel9.Name = "flowLayoutPanel9";
            // 
            // button3D_reset
            // 
            resources.ApplyResources(button3D_reset, "button3D_reset");
            button3D_reset.Name = "button3D_reset";
            button3D_reset.UseVisualStyleBackColor = true;
            button3D_reset.Click += button3D_reset_Click;
            // 
            // checkBox3dOptionLabel
            // 
            resources.ApplyResources(checkBox3dOptionLabel, "checkBox3dOptionLabel");
            checkBox3dOptionLabel.Checked = true;
            checkBox3dOptionLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3dOptionLabel.Name = "checkBox3dOptionLabel";
            checkBox3dOptionLabel.UseVisualStyleBackColor = true;
            checkBox3dOptionLabel.CheckedChanged += checkBox3dOptionSphere_CheckedChanged;
            // 
            // checkBox3dOptionSemisphere
            // 
            resources.ApplyResources(checkBox3dOptionSemisphere, "checkBox3dOptionSemisphere");
            checkBox3dOptionSemisphere.Checked = true;
            checkBox3dOptionSemisphere.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3dOptionSemisphere.Name = "checkBox3dOptionSemisphere";
            checkBox3dOptionSemisphere.UseVisualStyleBackColor = true;
            checkBox3dOptionSemisphere.CheckedChanged += checkBox3dOptionSphere_CheckedChanged;
            // 
            // checkBox3dOptionStereonet
            // 
            resources.ApplyResources(checkBox3dOptionStereonet, "checkBox3dOptionStereonet");
            checkBox3dOptionStereonet.Checked = true;
            checkBox3dOptionStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3dOptionStereonet.Name = "checkBox3dOptionStereonet";
            checkBox3dOptionStereonet.UseVisualStyleBackColor = true;
            checkBox3dOptionStereonet.CheckedChanged += checkBox3dOptionSphere_CheckedChanged;
            // 
            // flowLayoutPanel8
            // 
            flowLayoutPanel8.Controls.Add(checkBox3dOptionSphere);
            flowLayoutPanel8.Controls.Add(checkBox3dOptionProjectionLine);
            flowLayoutPanel8.Controls.Add(label29);
            flowLayoutPanel8.Controls.Add(trackBarDepthFadingOut);
            resources.ApplyResources(flowLayoutPanel8, "flowLayoutPanel8");
            flowLayoutPanel8.Name = "flowLayoutPanel8";
            // 
            // checkBox3dOptionSphere
            // 
            resources.ApplyResources(checkBox3dOptionSphere, "checkBox3dOptionSphere");
            checkBox3dOptionSphere.Checked = true;
            checkBox3dOptionSphere.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3dOptionSphere.Name = "checkBox3dOptionSphere";
            checkBox3dOptionSphere.UseVisualStyleBackColor = true;
            checkBox3dOptionSphere.CheckedChanged += checkBox3dOptionSphere_CheckedChanged;
            // 
            // checkBox3dOptionProjectionLine
            // 
            resources.ApplyResources(checkBox3dOptionProjectionLine, "checkBox3dOptionProjectionLine");
            checkBox3dOptionProjectionLine.Checked = true;
            checkBox3dOptionProjectionLine.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3dOptionProjectionLine.Name = "checkBox3dOptionProjectionLine";
            checkBox3dOptionProjectionLine.UseVisualStyleBackColor = true;
            checkBox3dOptionProjectionLine.CheckedChanged += checkBox3dOptionProjectionLine_CheckedChanged;
            // 
            // label29
            // 
            resources.ApplyResources(label29, "label29");
            label29.Name = "label29";
            // 
            // trackBarDepthFadingOut
            // 
            resources.ApplyResources(trackBarDepthFadingOut, "trackBarDepthFadingOut");
            trackBarDepthFadingOut.Name = "trackBarDepthFadingOut";
            trackBarDepthFadingOut.Value = 5;
            trackBarDepthFadingOut.Scroll += trackBarDepthFadingOut_Scroll;
            // 
            // labelAxisPlane
            // 
            resources.ApplyResources(labelAxisPlane, "labelAxisPlane");
            labelAxisPlane.Name = "labelAxisPlane";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // pageSetupDialog1
            // 
            pageSetupDialog1.Document = printDocument1;
            // 
            // printDialog1
            // 
            printDialog1.Document = printDocument1;
            printDialog1.UseEXDialog = true;
            // 
            // flowLayoutPanel10
            // 
            resources.ApplyResources(flowLayoutPanel10, "flowLayoutPanel10");
            flowLayoutPanel10.Controls.Add(buttonAddCircle);
            flowLayoutPanel10.Controls.Add(buttonDeleteCircle);
            flowLayoutPanel10.Controls.Add(colorControlGreatCircle);
            flowLayoutPanel10.Name = "flowLayoutPanel10";
            // 
            // FormStereonet
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            Controls.Add(label3);
            Controls.Add(labelXYpos);
            Controls.Add(label2);
            Controls.Add(labelAxisPlane);
            Controls.Add(panel3);
            Controls.Add(tabControl);
            Controls.Add(menuStrip1);
            Controls.Add(splitContainer1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "FormStereonet";
            FormClosing += FormStereonet_FormClosing;
            Load += FormStereonet_Load;
            VisibleChanged += FormStereonet_VisibleChanged;
            Paint += FormStereonet_Paint;
            splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)graphicsBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).EndInit();
            groupBoxMode.ResumeLayout(false);
            groupBoxSphere.ResumeLayout(false);
            groupBoxSphere.PerformLayout();
            flowLayoutPanelSphere.ResumeLayout(false);
            flowLayoutPanelSphere.PerformLayout();
            groupBoxProjectionScheme.ResumeLayout(false);
            groupBoxProjectionScheme.PerformLayout();
            flowLayoutPanelProjectionScheme.ResumeLayout(false);
            flowLayoutPanelProjectionScheme.PerformLayout();
            groupBoxProjectionObject.ResumeLayout(false);
            groupBoxProjectionObject.PerformLayout();
            flowLayoutPanelProjectionObject.ResumeLayout(false);
            flowLayoutPanelProjectionObject.PerformLayout();
            groupBoxOutline.ResumeLayout(false);
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBoxDelimiter.ResumeLayout(false);
            groupBoxDelimiter.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            groupBoxSize.ResumeLayout(false);
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            groupBoxColor.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            flowLayoutPanel7.ResumeLayout(false);
            flowLayoutPanel7.PerformLayout();
            flowLayoutPanelCircleAxis.ResumeLayout(false);
            flowLayoutPanelCircleAxis.PerformLayout();
            flowLayoutPanelCirclePlanes.ResumeLayout(false);
            flowLayoutPanelCirclePlanes.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBoxIndices.ResumeLayout(false);
            panelSpecifiedIndices.ResumeLayout(false);
            panelSpecifiedIndices.PerformLayout();
            flowLayoutPanelAddRemove.ResumeLayout(false);
            flowLayoutPanelAddRemove.PerformLayout();
            flowLayoutPanelIndices.ResumeLayout(false);
            flowLayoutPanelIndices.PerformLayout();
            flowLayoutPanelIndexFilter.ResumeLayout(false);
            flowLayoutPanelIndexFilter.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel3DOptions.ResumeLayout(false);
            panel3DOptions.PerformLayout();
            groupBox3DOptions.ResumeLayout(false);
            flowLayoutPanel9.ResumeLayout(false);
            flowLayoutPanel9.PerformLayout();
            flowLayoutPanel8.ResumeLayout(false);
            flowLayoutPanel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarDepthFadingOut).EndInit();
            flowLayoutPanel10.ResumeLayout(false);
            flowLayoutPanel10.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarStrSize;
        private System.Windows.Forms.TrackBar trackBarPointSize;
        private System.Windows.Forms.GroupBox groupBoxMode;
        private System.Windows.Forms.RadioButton radioButtonAxes;
        private System.Windows.Forms.RadioButton radioButtonPlanes;
        private System.Windows.Forms.Label labelXYpos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxOutline;
        private System.Windows.Forms.CheckBox checkBox1DegLine;
        private System.Windows.Forms.RadioButton radioButtonOutlinePole;
        private System.Windows.Forms.RadioButton radioButtonOutlineEquator;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBoxColor;
        private System.Windows.Forms.GroupBox groupBoxSize;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem pageSetupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonAddCircle;
        private System.Windows.Forms.Button buttonDeleteCircle;
        private System.Windows.Forms.RadioButton radioButtonCircleByAxis;
        private System.Windows.Forms.RadioButton radioButtonCircleByPlanes;
        private System.Windows.Forms.CheckedListBox checkedListBoxCircles;
        public ColorControl colorControlString;
        public ColorControl colorControlBackGround;
        public ColorControl colorControl90DegLine;
        public ColorControl colorControl10DegLine;
        public ColorControl colorControl1DegLine;
        public ColorControl colorControlGreatCircle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProjectionScheme;
        private System.Windows.Forms.RadioButton radioButtonSchmidt;
        private System.Windows.Forms.RadioButton radioButtonWulff;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProjectionObject;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button buttonYusaModeStop;
        private System.Windows.Forms.Button buttonYusaModeStart;
        private System.Windows.Forms.Label label25;
        public NumericBox numericBoxRxSpeed;
        public NumericBox numericBoxRySpeed;
        public NumericBox numericBoxRzSpeed;
        public NumericBox numericBoxRyOscillation;
        public NumericBox numericBoxRzOscillation;
        public NumericBox numericBoxRyStep;
        private System.Windows.Forms.GroupBox groupBoxIndices;
        // 260517Cl 削除 (孤児フィールド): private System.Windows.Forms.Label labelKV; — Designer 内で一度も new/Add/参照されていない
        private System.Windows.Forms.RadioButton radioButtonRange;
        private System.Windows.Forms.RadioButton radioButtonSpecifiedIndices;
        private System.Windows.Forms.Panel panelSpecifiedIndices;
        private System.Windows.Forms.Button buttonRemoveIndex;
        private System.Windows.Forms.Button buttonAddIndex;
        private System.Windows.Forms.ListBox listBoxSpecifiedIndices;
        private System.Windows.Forms.CheckBox checkBoxIncludingEquivalentPlanes;
        public NumericBox numericBoxTotalTime;
        public NumericBox numericBoxAngularSpeed;
        public NumericBox numericBoxRadialAngle;
        public System.Windows.Forms.RadioButton radioButtonRotationalScan;
        public System.Windows.Forms.RadioButton radioButtonZigzagScan;
        private System.Windows.Forms.Panel panel3;
        // public ImagingSolution.Control.GraphicsBox graphicsBox; // (260322Ch) 旧 GraphicsBox 型
        // public Crystallography.Controls.GraphicBox2 graphicsBox; // (260322Ch) 仮名 GraphicBox2
        public Crystallography.Controls.GraphicsBox graphicsBox; // (260322Ch) 正式名 GraphicBox へ移行
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem asBitmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asMetafileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asMetafileToolStripMenuItem1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelIndices;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBoxDisplay3D;
        private System.Windows.Forms.CheckBox checkBox3dOptionProjectionLine;
        private System.Windows.Forms.CheckBox checkBox3dOptionSphere;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button3D_reset;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TrackBar trackBarDepthFadingOut;
        private System.Windows.Forms.CheckBox checkBox3dOptionLabel;
        private System.Windows.Forms.CheckBox checkBox3dOptionSemisphere;
        private System.Windows.Forms.CheckBox checkBox3dOptionStereonet;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveMovieStereonet;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveMovie3D;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel3DOptions;
        private System.Windows.Forms.GroupBox groupBox3DOptions;
        // 260517Cl 削除 (孤児フィールド): private System.Windows.Forms.Label labelHU; — 未 new / 未 Add / 未参照
        // 260517Cl 削除 (孤児フィールド): private System.Windows.Forms.Label labelLW; — 未 new / 未 Add / 未参照
        private System.Windows.Forms.RadioButton radioButtonKikuchiLinePairs;
        private System.Windows.Forms.TabPage tabPage4;
        private WaveLengthControl waveLengthControl;
        private System.Windows.Forms.RadioButton radioButtonHighStructureFactor;
        private NumericBox numericBoxHighStructureFactor;
        private System.Windows.Forms.CheckBox checkBoxReflectStructureFactor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSphere;
        private System.Windows.Forms.RadioButton radioButtonUpperSphere;
        private System.Windows.Forms.RadioButton radioButtonLowerSphere;
        private System.Windows.Forms.GroupBox groupBoxSphere;
        private System.Windows.Forms.GroupBox groupBoxProjectionObject;
        private System.Windows.Forms.GroupBox groupBoxProjectionScheme;
        private System.Windows.Forms.CheckBox checkBoxShowIndexLabels;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelIndexFilter;
        // 260517Cl 削除 (孤児フィールド): private System.Windows.Forms.Label labelI; — 未 new / 未 Add / 未参照
        // 260517Cl 削除 (孤児フィールド): private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelIndex; — flowLayoutPanelIndexFilter と命名混同、未 new / 未 Add / 未参照
        private System.Windows.Forms.GroupBox groupBoxDelimiter;
        private System.Windows.Forms.RadioButton radioButtonDelimiterComma;
        private System.Windows.Forms.RadioButton radioButtonDelimiterSpace;
        private System.Windows.Forms.RadioButton radioButtonDelimiterNone;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelAddRemove;
        private System.Windows.Forms.CheckBox checkBoxRotateColor;
        private ColorControl colorControlIndex;
        public ColorControl colorControlKikuchi;
        private System.Windows.Forms.Label labelAxisPlane;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCircleAxis;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCirclePlanes;
        private IndexControl indexControlCirclePlane1;
        private IndexControl indexControlCirclePlane2;
        private IndexControl indexControlAxis;
        private IndexControl indexControlDrawing;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
    }
}
