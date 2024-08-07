using System;

namespace ReciPro
{
    partial class FormStereonet
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
            if (strFont != null)
                strFont.Dispose();
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStereonet));
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            graphicsBox = new ImagingSolution.Control.GraphicsBox(components);
            trackBarStrSize = new System.Windows.Forms.TrackBar();
            trackBarPointSize = new System.Windows.Forms.TrackBar();
            groupBox2 = new System.Windows.Forms.GroupBox();
            groupBox8 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonUpperSphere = new System.Windows.Forms.RadioButton();
            radioButtonLowerSphere = new System.Windows.Forms.RadioButton();
            groupBox9 = new System.Windows.Forms.GroupBox();
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonWulff = new System.Windows.Forms.RadioButton();
            radioButtonSchmidt = new System.Windows.Forms.RadioButton();
            groupBox7 = new System.Windows.Forms.GroupBox();
            checkBoxReflectStructureFactor = new System.Windows.Forms.CheckBox();
            checkBoxShowIndexLabels = new System.Windows.Forms.CheckBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            radioButtonAxes = new System.Windows.Forms.RadioButton();
            radioButtonPlanes = new System.Windows.Forms.RadioButton();
            radioButtonKikuchiLinePairs = new System.Windows.Forms.RadioButton();
            labelYpos = new System.Windows.Forms.Label();
            labelXpos = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            checkBox1DegLine = new System.Windows.Forms.CheckBox();
            radioButtonOutlinePole = new System.Windows.Forms.RadioButton();
            radioButtonOutlineEquator = new System.Windows.Forms.RadioButton();
            tabControl = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            groupBox4 = new System.Windows.Forms.GroupBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            colorControlString = new ColorControl();
            colorControl1 = new ColorControl();
            colorControlUniqueAxis = new ColorControl();
            colorControlGeneralAxis = new ColorControl();
            colorControlBackGround = new ColorControl();
            colorControlUniquePlane = new ColorControl();
            colorControl10DegLine = new ColorControl();
            colorControlGeneralPlane = new ColorControl();
            colorControl1DegLine = new ColorControl();
            colorControl90DegLine = new ColorControl();
            tabPage2 = new System.Windows.Forms.TabPage();
            panelPlanes = new System.Windows.Forms.Panel();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            numericUpDownCircleH1 = new System.Windows.Forms.NumericUpDown();
            numericUpDownCircleH2 = new System.Windows.Forms.NumericUpDown();
            numericUpDownCircleL2 = new System.Windows.Forms.NumericUpDown();
            label15 = new System.Windows.Forms.Label();
            numericUpDownCircleL1 = new System.Windows.Forms.NumericUpDown();
            numericUpDownCircleK1 = new System.Windows.Forms.NumericUpDown();
            numericUpDownCircleK2 = new System.Windows.Forms.NumericUpDown();
            colorControlGreatCircle = new ColorControl();
            panelAxis = new System.Windows.Forms.Panel();
            label11 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            numericUpDownCircleU = new System.Windows.Forms.NumericUpDown();
            numericUpDownCircleV = new System.Windows.Forms.NumericUpDown();
            numericUpDownCircleW = new System.Windows.Forms.NumericUpDown();
            radioButtonCircleByPlanes = new System.Windows.Forms.RadioButton();
            radioButtonCircleByAxis = new System.Windows.Forms.RadioButton();
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
            label37 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label32 = new System.Windows.Forms.Label();
            label26 = new System.Windows.Forms.Label();
            label27 = new System.Windows.Forms.Label();
            label36 = new System.Windows.Forms.Label();
            label30 = new System.Windows.Forms.Label();
            checkBox1 = new System.Windows.Forms.CheckBox();
            label28 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label25 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label34 = new System.Windows.Forms.Label();
            label33 = new System.Windows.Forms.Label();
            label24 = new System.Windows.Forms.Label();
            label31 = new System.Windows.Forms.Label();
            label21 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
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
            labelKV = new System.Windows.Forms.Label();
            labelHU = new System.Windows.Forms.Label();
            labelLW = new System.Windows.Forms.Label();
            printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            groupBox5 = new System.Windows.Forms.GroupBox();
            panelSpecifiedIndices = new System.Windows.Forms.Panel();
            checkBoxIncludingEquivalentPlanes = new System.Windows.Forms.CheckBox();
            buttonRemoveIndex = new System.Windows.Forms.Button();
            buttonAddIndex = new System.Windows.Forms.Button();
            listBoxSpecifiedIndices = new System.Windows.Forms.ListBox();
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            numericBox3 = new NumericBox();
            numericBox2 = new NumericBox();
            numericBox1 = new NumericBox();
            numericBoxHighStructureFactor = new NumericBox();
            panel3 = new System.Windows.Forms.Panel();
            panel4 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            panel3DOptions = new System.Windows.Forms.Panel();
            checkBoxDisplay3D = new System.Windows.Forms.CheckBox();
            groupBox3DOptions = new System.Windows.Forms.GroupBox();
            label29 = new System.Windows.Forms.Label();
            checkBox3dOptionSphere = new System.Windows.Forms.CheckBox();
            checkBox3dOptionSemisphere = new System.Windows.Forms.CheckBox();
            trackBarDepthFadingOut = new System.Windows.Forms.TrackBar();
            button3D_reset = new System.Windows.Forms.Button();
            checkBox3dOptionProjectionLine = new System.Windows.Forms.CheckBox();
            checkBox3dOptionStereonet = new System.Windows.Forms.CheckBox();
            checkBox3dOptionLabel = new System.Windows.Forms.CheckBox();
            scalablePictureBoxAdvanced1 = new ScalablePictureBoxAdvanced();
            scalablePictureBoxAdvanced2 = new ScalablePictureBoxAdvanced();
            pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            printDialog1 = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarStrSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPointSize).BeginInit();
            groupBox2.SuspendLayout();
            groupBox8.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            groupBox9.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            groupBox7.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            groupBox3.SuspendLayout();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage2.SuspendLayout();
            panelPlanes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleH1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleH2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleL2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleL1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleK1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleK2).BeginInit();
            panelAxis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleU).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleW).BeginInit();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupBox5.SuspendLayout();
            panelSpecifiedIndices.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel3.SuspendLayout();
            panel3DOptions.SuspendLayout();
            groupBox3DOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarDepthFadingOut).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(splitContainer1.Panel1, "splitContainer1.Panel1");
            splitContainer1.Panel1.Controls.Add(graphicsBox);
            toolTip.SetToolTip(splitContainer1.Panel1, resources.GetString("splitContainer1.Panel1.ToolTip"));
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(splitContainer1.Panel2, "splitContainer1.Panel2");
            toolTip.SetToolTip(splitContainer1.Panel2, resources.GetString("splitContainer1.Panel2.ToolTip"));
            toolTip.SetToolTip(splitContainer1, resources.GetString("splitContainer1.ToolTip"));
            // 
            // graphicsBox
            // 
            resources.ApplyResources(graphicsBox, "graphicsBox");
            graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
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
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(groupBox8);
            groupBox2.Controls.Add(groupBox9);
            groupBox2.Controls.Add(groupBox7);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            toolTip.SetToolTip(groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // groupBox8
            // 
            resources.ApplyResources(groupBox8, "groupBox8");
            groupBox8.Controls.Add(flowLayoutPanel4);
            groupBox8.Name = "groupBox8";
            groupBox8.TabStop = false;
            toolTip.SetToolTip(groupBox8, resources.GetString("groupBox8.ToolTip"));
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(flowLayoutPanel4, "flowLayoutPanel4");
            flowLayoutPanel4.Controls.Add(radioButtonUpperSphere);
            flowLayoutPanel4.Controls.Add(radioButtonLowerSphere);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            toolTip.SetToolTip(flowLayoutPanel4, resources.GetString("flowLayoutPanel4.ToolTip"));
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
            // groupBox9
            // 
            resources.ApplyResources(groupBox9, "groupBox9");
            groupBox9.Controls.Add(flowLayoutPanel2);
            groupBox9.Name = "groupBox9";
            groupBox9.TabStop = false;
            toolTip.SetToolTip(groupBox9, resources.GetString("groupBox9.ToolTip"));
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(flowLayoutPanel2, "flowLayoutPanel2");
            flowLayoutPanel2.Controls.Add(radioButtonWulff);
            flowLayoutPanel2.Controls.Add(radioButtonSchmidt);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            toolTip.SetToolTip(flowLayoutPanel2, resources.GetString("flowLayoutPanel2.ToolTip"));
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
            // groupBox7
            // 
            resources.ApplyResources(groupBox7, "groupBox7");
            groupBox7.Controls.Add(checkBoxReflectStructureFactor);
            groupBox7.Controls.Add(checkBoxShowIndexLabels);
            groupBox7.Controls.Add(flowLayoutPanel1);
            groupBox7.Name = "groupBox7";
            groupBox7.TabStop = false;
            toolTip.SetToolTip(groupBox7, resources.GetString("groupBox7.ToolTip"));
            // 
            // checkBoxReflectStructureFactor
            // 
            resources.ApplyResources(checkBoxReflectStructureFactor, "checkBoxReflectStructureFactor");
            checkBoxReflectStructureFactor.Name = "checkBoxReflectStructureFactor";
            toolTip.SetToolTip(checkBoxReflectStructureFactor, resources.GetString("checkBoxReflectStructureFactor.ToolTip"));
            checkBoxReflectStructureFactor.CheckedChanged += checkBoxReflectStructureFactor_CheckedChanged;
            // 
            // checkBoxShowIndexLabels
            // 
            resources.ApplyResources(checkBoxShowIndexLabels, "checkBoxShowIndexLabels");
            checkBoxShowIndexLabels.Checked = true;
            checkBoxShowIndexLabels.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxShowIndexLabels.Name = "checkBoxShowIndexLabels";
            toolTip.SetToolTip(checkBoxShowIndexLabels, resources.GetString("checkBoxShowIndexLabels.ToolTip"));
            checkBoxShowIndexLabels.UseVisualStyleBackColor = true;
            checkBoxShowIndexLabels.CheckedChanged += checkBoxShowIndexLabels_CheckedChanged;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Controls.Add(radioButtonAxes);
            flowLayoutPanel1.Controls.Add(radioButtonPlanes);
            flowLayoutPanel1.Controls.Add(radioButtonKikuchiLinePairs);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            toolTip.SetToolTip(flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
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
            // labelYpos
            // 
            resources.ApplyResources(labelYpos, "labelYpos");
            labelYpos.Name = "labelYpos";
            toolTip.SetToolTip(labelYpos, resources.GetString("labelYpos.ToolTip"));
            // 
            // labelXpos
            // 
            resources.ApplyResources(labelXpos, "labelXpos");
            labelXpos.Name = "labelXpos";
            toolTip.SetToolTip(labelXpos, resources.GetString("labelXpos.ToolTip"));
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
            // groupBox3
            // 
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Controls.Add(checkBox1DegLine);
            groupBox3.Controls.Add(radioButtonOutlinePole);
            groupBox3.Controls.Add(radioButtonOutlineEquator);
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            toolTip.SetToolTip(groupBox3, resources.GetString("groupBox3.ToolTip"));
            // 
            // checkBox1DegLine
            // 
            resources.ApplyResources(checkBox1DegLine, "checkBox1DegLine");
            checkBox1DegLine.Name = "checkBox1DegLine";
            toolTip.SetToolTip(checkBox1DegLine, resources.GetString("checkBox1DegLine.ToolTip"));
            checkBox1DegLine.CheckedChanged += checkBox1DegLine_CheckedChanged;
            // 
            // radioButtonOutlinePole
            // 
            resources.ApplyResources(radioButtonOutlinePole, "radioButtonOutlinePole");
            radioButtonOutlinePole.Name = "radioButtonOutlinePole";
            toolTip.SetToolTip(radioButtonOutlinePole, resources.GetString("radioButtonOutlinePole.ToolTip"));
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
            // tabControl
            // 
            resources.ApplyResources(tabControl, "tabControl");
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Controls.Add(tabPage3);
            tabControl.Controls.Add(tabPage4);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            toolTip.SetToolTip(tabControl, resources.GetString("tabControl.ToolTip"));
            tabControl.Click += tabControl_Click;
            // 
            // tabPage1
            // 
            resources.ApplyResources(tabPage1, "tabPage1");
            tabPage1.BackColor = System.Drawing.SystemColors.Control;
            tabPage1.Controls.Add(groupBox4);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Name = "tabPage1";
            toolTip.SetToolTip(tabPage1, resources.GetString("tabPage1.ToolTip"));
            // 
            // groupBox4
            // 
            resources.ApplyResources(groupBox4, "groupBox4");
            groupBox4.Controls.Add(label1);
            groupBox4.Controls.Add(label6);
            groupBox4.Controls.Add(trackBarPointSize);
            groupBox4.Controls.Add(trackBarStrSize);
            groupBox4.Name = "groupBox4";
            groupBox4.TabStop = false;
            toolTip.SetToolTip(groupBox4, resources.GetString("groupBox4.ToolTip"));
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(colorControlString);
            groupBox1.Controls.Add(colorControl1);
            groupBox1.Controls.Add(colorControlUniqueAxis);
            groupBox1.Controls.Add(colorControlGeneralAxis);
            groupBox1.Controls.Add(colorControlBackGround);
            groupBox1.Controls.Add(colorControlUniquePlane);
            groupBox1.Controls.Add(colorControl10DegLine);
            groupBox1.Controls.Add(colorControlGeneralPlane);
            groupBox1.Controls.Add(colorControl1DegLine);
            groupBox1.Controls.Add(colorControl90DegLine);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            toolTip.SetToolTip(groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // colorControlString
            // 
            resources.ApplyResources(colorControlString, "colorControlString");
            colorControlString.Argb = -16777216;
            colorControlString.BackColor = System.Drawing.Color.Black;
            colorControlString.Blue = 0;
            colorControlString.BlueF = 0F;
            colorControlString.BoxSize = new System.Drawing.Size(22, 20);
            colorControlString.Color = System.Drawing.Color.FromArgb(0, 0, 0);
            colorControlString.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlString.Green = 0;
            colorControlString.GreenF = 0F;
            colorControlString.Inversion = false;
            colorControlString.Name = "colorControlString";
            colorControlString.Red = 0;
            colorControlString.RedF = 0F;
            colorControlString.TabStop = false;
            toolTip.SetToolTip(colorControlString, resources.GetString("colorControlString.ToolTip"));
            colorControlString.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControl1
            // 
            resources.ApplyResources(colorControl1, "colorControl1");
            colorControl1.Argb = -32768;
            colorControl1.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            colorControl1.Blue = 0;
            colorControl1.BlueF = 0F;
            colorControl1.BoxSize = new System.Drawing.Size(22, 20);
            colorControl1.Color = System.Drawing.Color.FromArgb(255, 128, 0);
            colorControl1.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControl1.Green = 128;
            colorControl1.GreenF = 0.5019608F;
            colorControl1.Inversion = false;
            colorControl1.Name = "colorControl1";
            colorControl1.Red = 255;
            colorControl1.RedF = 1F;
            colorControl1.TabStop = false;
            toolTip.SetToolTip(colorControl1, resources.GetString("colorControl1.ToolTip"));
            colorControl1.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControlUniqueAxis
            // 
            resources.ApplyResources(colorControlUniqueAxis, "colorControlUniqueAxis");
            colorControlUniqueAxis.Argb = -7667712;
            colorControlUniqueAxis.BackColor = System.Drawing.Color.Red;
            colorControlUniqueAxis.Blue = 0;
            colorControlUniqueAxis.BlueF = 0F;
            colorControlUniqueAxis.BoxSize = new System.Drawing.Size(22, 20);
            colorControlUniqueAxis.Color = System.Drawing.Color.FromArgb(139, 0, 0);
            colorControlUniqueAxis.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlUniqueAxis.Green = 0;
            colorControlUniqueAxis.GreenF = 0F;
            colorControlUniqueAxis.Inversion = false;
            colorControlUniqueAxis.Name = "colorControlUniqueAxis";
            colorControlUniqueAxis.Red = 139;
            colorControlUniqueAxis.RedF = 0.545098066F;
            colorControlUniqueAxis.TabStop = false;
            toolTip.SetToolTip(colorControlUniqueAxis, resources.GetString("colorControlUniqueAxis.ToolTip"));
            colorControlUniqueAxis.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControlGeneralAxis
            // 
            resources.ApplyResources(colorControlGeneralAxis, "colorControlGeneralAxis");
            colorControlGeneralAxis.Argb = -65536;
            colorControlGeneralAxis.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
            colorControlGeneralAxis.Blue = 0;
            colorControlGeneralAxis.BlueF = 0F;
            colorControlGeneralAxis.BoxSize = new System.Drawing.Size(22, 20);
            colorControlGeneralAxis.Color = System.Drawing.Color.FromArgb(255, 0, 0);
            colorControlGeneralAxis.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlGeneralAxis.Green = 0;
            colorControlGeneralAxis.GreenF = 0F;
            colorControlGeneralAxis.Inversion = false;
            colorControlGeneralAxis.Name = "colorControlGeneralAxis";
            colorControlGeneralAxis.Red = 255;
            colorControlGeneralAxis.RedF = 1F;
            colorControlGeneralAxis.TabStop = false;
            toolTip.SetToolTip(colorControlGeneralAxis, resources.GetString("colorControlGeneralAxis.ToolTip"));
            colorControlGeneralAxis.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControlBackGround
            // 
            resources.ApplyResources(colorControlBackGround, "colorControlBackGround");
            colorControlBackGround.Argb = -1;
            colorControlBackGround.BackColor = System.Drawing.Color.White;
            colorControlBackGround.Blue = 255;
            colorControlBackGround.BlueF = 1F;
            colorControlBackGround.BoxSize = new System.Drawing.Size(22, 20);
            colorControlBackGround.Color = System.Drawing.Color.FromArgb(255, 255, 255);
            colorControlBackGround.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlBackGround.Green = 255;
            colorControlBackGround.GreenF = 1F;
            colorControlBackGround.Inversion = false;
            colorControlBackGround.Name = "colorControlBackGround";
            colorControlBackGround.Red = 255;
            colorControlBackGround.RedF = 1F;
            colorControlBackGround.TabStop = false;
            toolTip.SetToolTip(colorControlBackGround, resources.GetString("colorControlBackGround.ToolTip"));
            colorControlBackGround.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControlUniquePlane
            // 
            resources.ApplyResources(colorControlUniquePlane, "colorControlUniquePlane");
            colorControlUniquePlane.Argb = -16751616;
            colorControlUniquePlane.BackColor = System.Drawing.Color.Lime;
            colorControlUniquePlane.Blue = 0;
            colorControlUniquePlane.BlueF = 0F;
            colorControlUniquePlane.BoxSize = new System.Drawing.Size(22, 20);
            colorControlUniquePlane.Color = System.Drawing.Color.FromArgb(0, 100, 0);
            colorControlUniquePlane.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlUniquePlane.Green = 100;
            colorControlUniquePlane.GreenF = 0.392156869F;
            colorControlUniquePlane.Inversion = false;
            colorControlUniquePlane.Name = "colorControlUniquePlane";
            colorControlUniquePlane.Red = 0;
            colorControlUniquePlane.RedF = 0F;
            colorControlUniquePlane.TabStop = false;
            toolTip.SetToolTip(colorControlUniquePlane, resources.GetString("colorControlUniquePlane.ToolTip"));
            colorControlUniquePlane.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControl10DegLine
            // 
            resources.ApplyResources(colorControl10DegLine, "colorControl10DegLine");
            colorControl10DegLine.Argb = -8355585;
            colorControl10DegLine.BackColor = System.Drawing.Color.FromArgb(128, 128, 255);
            colorControl10DegLine.Blue = 255;
            colorControl10DegLine.BlueF = 1F;
            colorControl10DegLine.BoxSize = new System.Drawing.Size(22, 20);
            colorControl10DegLine.Color = System.Drawing.Color.FromArgb(128, 128, 255);
            colorControl10DegLine.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControl10DegLine.Green = 128;
            colorControl10DegLine.GreenF = 0.5019608F;
            colorControl10DegLine.Inversion = false;
            colorControl10DegLine.Name = "colorControl10DegLine";
            colorControl10DegLine.Red = 128;
            colorControl10DegLine.RedF = 0.5019608F;
            colorControl10DegLine.TabStop = false;
            toolTip.SetToolTip(colorControl10DegLine, resources.GetString("colorControl10DegLine.ToolTip"));
            colorControl10DegLine.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControlGeneralPlane
            // 
            resources.ApplyResources(colorControlGeneralPlane, "colorControlGeneralPlane");
            colorControlGeneralPlane.Argb = -14578910;
            colorControlGeneralPlane.BackColor = System.Drawing.Color.FromArgb(128, 255, 128);
            colorControlGeneralPlane.Blue = 34;
            colorControlGeneralPlane.BlueF = 0.13333334F;
            colorControlGeneralPlane.BoxSize = new System.Drawing.Size(22, 20);
            colorControlGeneralPlane.Color = System.Drawing.Color.FromArgb(33, 139, 34);
            colorControlGeneralPlane.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlGeneralPlane.Green = 139;
            colorControlGeneralPlane.GreenF = 0.545098066F;
            colorControlGeneralPlane.Inversion = false;
            colorControlGeneralPlane.Name = "colorControlGeneralPlane";
            colorControlGeneralPlane.Red = 33;
            colorControlGeneralPlane.RedF = 0.129411772F;
            colorControlGeneralPlane.TabStop = false;
            toolTip.SetToolTip(colorControlGeneralPlane, resources.GetString("colorControlGeneralPlane.ToolTip"));
            colorControlGeneralPlane.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControl1DegLine
            // 
            resources.ApplyResources(colorControl1DegLine, "colorControl1DegLine");
            colorControl1DegLine.Argb = -4144897;
            colorControl1DegLine.BackColor = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControl1DegLine.Blue = 255;
            colorControl1DegLine.BlueF = 1F;
            colorControl1DegLine.BoxSize = new System.Drawing.Size(22, 20);
            colorControl1DegLine.Color = System.Drawing.Color.FromArgb(192, 192, 255);
            colorControl1DegLine.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControl1DegLine.Green = 192;
            colorControl1DegLine.GreenF = 0.7529412F;
            colorControl1DegLine.Inversion = false;
            colorControl1DegLine.Name = "colorControl1DegLine";
            colorControl1DegLine.Red = 192;
            colorControl1DegLine.RedF = 0.7529412F;
            colorControl1DegLine.TabStop = false;
            toolTip.SetToolTip(colorControl1DegLine, resources.GetString("colorControl1DegLine.ToolTip"));
            colorControl1DegLine.ColorChanged += colorControl_ColorChanged;
            // 
            // colorControl90DegLine
            // 
            resources.ApplyResources(colorControl90DegLine, "colorControl90DegLine");
            colorControl90DegLine.Argb = -16776961;
            colorControl90DegLine.BackColor = System.Drawing.Color.Blue;
            colorControl90DegLine.Blue = 255;
            colorControl90DegLine.BlueF = 1F;
            colorControl90DegLine.BoxSize = new System.Drawing.Size(22, 20);
            colorControl90DegLine.Color = System.Drawing.Color.FromArgb(0, 0, 255);
            colorControl90DegLine.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControl90DegLine.Green = 0;
            colorControl90DegLine.GreenF = 0F;
            colorControl90DegLine.Inversion = false;
            colorControl90DegLine.Name = "colorControl90DegLine";
            colorControl90DegLine.Red = 0;
            colorControl90DegLine.RedF = 0F;
            colorControl90DegLine.TabStop = false;
            toolTip.SetToolTip(colorControl90DegLine, resources.GetString("colorControl90DegLine.ToolTip"));
            colorControl90DegLine.ColorChanged += colorControl_ColorChanged;
            // 
            // tabPage2
            // 
            resources.ApplyResources(tabPage2, "tabPage2");
            tabPage2.BackColor = System.Drawing.SystemColors.Control;
            tabPage2.Controls.Add(panelPlanes);
            tabPage2.Controls.Add(colorControlGreatCircle);
            tabPage2.Controls.Add(panelAxis);
            tabPage2.Controls.Add(radioButtonCircleByPlanes);
            tabPage2.Controls.Add(radioButtonCircleByAxis);
            tabPage2.Controls.Add(buttonAddCircle);
            tabPage2.Controls.Add(buttonDeleteCircle);
            tabPage2.Controls.Add(checkedListBoxCircles);
            tabPage2.Name = "tabPage2";
            toolTip.SetToolTip(tabPage2, resources.GetString("tabPage2.ToolTip"));
            // 
            // panelPlanes
            // 
            resources.ApplyResources(panelPlanes, "panelPlanes");
            panelPlanes.Controls.Add(label12);
            panelPlanes.Controls.Add(label13);
            panelPlanes.Controls.Add(label16);
            panelPlanes.Controls.Add(label14);
            panelPlanes.Controls.Add(label17);
            panelPlanes.Controls.Add(numericUpDownCircleH1);
            panelPlanes.Controls.Add(numericUpDownCircleH2);
            panelPlanes.Controls.Add(numericUpDownCircleL2);
            panelPlanes.Controls.Add(label15);
            panelPlanes.Controls.Add(numericUpDownCircleL1);
            panelPlanes.Controls.Add(numericUpDownCircleK1);
            panelPlanes.Controls.Add(numericUpDownCircleK2);
            panelPlanes.Name = "panelPlanes";
            toolTip.SetToolTip(panelPlanes, resources.GetString("panelPlanes.ToolTip"));
            // 
            // label12
            // 
            resources.ApplyResources(label12, "label12");
            label12.Name = "label12";
            toolTip.SetToolTip(label12, resources.GetString("label12.ToolTip"));
            // 
            // label13
            // 
            resources.ApplyResources(label13, "label13");
            label13.Name = "label13";
            toolTip.SetToolTip(label13, resources.GetString("label13.ToolTip"));
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            label16.Name = "label16";
            toolTip.SetToolTip(label16, resources.GetString("label16.ToolTip"));
            // 
            // label14
            // 
            resources.ApplyResources(label14, "label14");
            label14.Name = "label14";
            toolTip.SetToolTip(label14, resources.GetString("label14.ToolTip"));
            // 
            // label17
            // 
            resources.ApplyResources(label17, "label17");
            label17.Name = "label17";
            toolTip.SetToolTip(label17, resources.GetString("label17.ToolTip"));
            // 
            // numericUpDownCircleH1
            // 
            resources.ApplyResources(numericUpDownCircleH1, "numericUpDownCircleH1");
            numericUpDownCircleH1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownCircleH1.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDownCircleH1.Name = "numericUpDownCircleH1";
            toolTip.SetToolTip(numericUpDownCircleH1, resources.GetString("numericUpDownCircleH1.ToolTip"));
            numericUpDownCircleH1.ValueChanged += numericUpDown_ValueChanged;
            // 
            // numericUpDownCircleH2
            // 
            resources.ApplyResources(numericUpDownCircleH2, "numericUpDownCircleH2");
            numericUpDownCircleH2.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownCircleH2.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDownCircleH2.Name = "numericUpDownCircleH2";
            toolTip.SetToolTip(numericUpDownCircleH2, resources.GetString("numericUpDownCircleH2.ToolTip"));
            numericUpDownCircleH2.ValueChanged += numericUpDown_ValueChanged;
            // 
            // numericUpDownCircleL2
            // 
            resources.ApplyResources(numericUpDownCircleL2, "numericUpDownCircleL2");
            numericUpDownCircleL2.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownCircleL2.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDownCircleL2.Name = "numericUpDownCircleL2";
            toolTip.SetToolTip(numericUpDownCircleL2, resources.GetString("numericUpDownCircleL2.ToolTip"));
            numericUpDownCircleL2.ValueChanged += numericUpDown_ValueChanged;
            // 
            // label15
            // 
            resources.ApplyResources(label15, "label15");
            label15.Name = "label15";
            toolTip.SetToolTip(label15, resources.GetString("label15.ToolTip"));
            // 
            // numericUpDownCircleL1
            // 
            resources.ApplyResources(numericUpDownCircleL1, "numericUpDownCircleL1");
            numericUpDownCircleL1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownCircleL1.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDownCircleL1.Name = "numericUpDownCircleL1";
            toolTip.SetToolTip(numericUpDownCircleL1, resources.GetString("numericUpDownCircleL1.ToolTip"));
            numericUpDownCircleL1.ValueChanged += numericUpDown_ValueChanged;
            // 
            // numericUpDownCircleK1
            // 
            resources.ApplyResources(numericUpDownCircleK1, "numericUpDownCircleK1");
            numericUpDownCircleK1.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownCircleK1.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDownCircleK1.Name = "numericUpDownCircleK1";
            toolTip.SetToolTip(numericUpDownCircleK1, resources.GetString("numericUpDownCircleK1.ToolTip"));
            numericUpDownCircleK1.ValueChanged += numericUpDown_ValueChanged;
            // 
            // numericUpDownCircleK2
            // 
            resources.ApplyResources(numericUpDownCircleK2, "numericUpDownCircleK2");
            numericUpDownCircleK2.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownCircleK2.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDownCircleK2.Name = "numericUpDownCircleK2";
            toolTip.SetToolTip(numericUpDownCircleK2, resources.GetString("numericUpDownCircleK2.ToolTip"));
            numericUpDownCircleK2.ValueChanged += numericUpDown_ValueChanged;
            // 
            // colorControlGreatCircle
            // 
            resources.ApplyResources(colorControlGreatCircle, "colorControlGreatCircle");
            colorControlGreatCircle.Argb = -32768;
            colorControlGreatCircle.BackColor = System.Drawing.SystemColors.Control;
            colorControlGreatCircle.Blue = 0;
            colorControlGreatCircle.BlueF = 0F;
            colorControlGreatCircle.BoxSize = new System.Drawing.Size(24, 24);
            colorControlGreatCircle.Color = System.Drawing.Color.FromArgb(255, 128, 0);
            colorControlGreatCircle.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            colorControlGreatCircle.Green = 128;
            colorControlGreatCircle.GreenF = 0.5019608F;
            colorControlGreatCircle.Inversion = false;
            colorControlGreatCircle.Name = "colorControlGreatCircle";
            colorControlGreatCircle.Red = 255;
            colorControlGreatCircle.RedF = 1F;
            toolTip.SetToolTip(colorControlGreatCircle, resources.GetString("colorControlGreatCircle.ToolTip"));
            colorControlGreatCircle.ColorChanged += colorControl_ColorChanged;
            // 
            // panelAxis
            // 
            resources.ApplyResources(panelAxis, "panelAxis");
            panelAxis.Controls.Add(label11);
            panelAxis.Controls.Add(label5);
            panelAxis.Controls.Add(label7);
            panelAxis.Controls.Add(numericUpDownCircleU);
            panelAxis.Controls.Add(numericUpDownCircleV);
            panelAxis.Controls.Add(numericUpDownCircleW);
            panelAxis.Name = "panelAxis";
            toolTip.SetToolTip(panelAxis, resources.GetString("panelAxis.ToolTip"));
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            toolTip.SetToolTip(label11, resources.GetString("label11.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            toolTip.SetToolTip(label5, resources.GetString("label5.ToolTip"));
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            toolTip.SetToolTip(label7, resources.GetString("label7.ToolTip"));
            // 
            // numericUpDownCircleU
            // 
            resources.ApplyResources(numericUpDownCircleU, "numericUpDownCircleU");
            numericUpDownCircleU.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownCircleU.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDownCircleU.Name = "numericUpDownCircleU";
            toolTip.SetToolTip(numericUpDownCircleU, resources.GetString("numericUpDownCircleU.ToolTip"));
            numericUpDownCircleU.ValueChanged += numericUpDown_ValueChanged;
            // 
            // numericUpDownCircleV
            // 
            resources.ApplyResources(numericUpDownCircleV, "numericUpDownCircleV");
            numericUpDownCircleV.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownCircleV.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDownCircleV.Name = "numericUpDownCircleV";
            toolTip.SetToolTip(numericUpDownCircleV, resources.GetString("numericUpDownCircleV.ToolTip"));
            numericUpDownCircleV.ValueChanged += numericUpDown_ValueChanged;
            // 
            // numericUpDownCircleW
            // 
            resources.ApplyResources(numericUpDownCircleW, "numericUpDownCircleW");
            numericUpDownCircleW.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownCircleW.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            numericUpDownCircleW.Name = "numericUpDownCircleW";
            toolTip.SetToolTip(numericUpDownCircleW, resources.GetString("numericUpDownCircleW.ToolTip"));
            numericUpDownCircleW.ValueChanged += numericUpDown_ValueChanged;
            // 
            // radioButtonCircleByPlanes
            // 
            resources.ApplyResources(radioButtonCircleByPlanes, "radioButtonCircleByPlanes");
            radioButtonCircleByPlanes.Name = "radioButtonCircleByPlanes";
            toolTip.SetToolTip(radioButtonCircleByPlanes, resources.GetString("radioButtonCircleByPlanes.ToolTip"));
            radioButtonCircleByPlanes.UseVisualStyleBackColor = true;
            // 
            // radioButtonCircleByAxis
            // 
            resources.ApplyResources(radioButtonCircleByAxis, "radioButtonCircleByAxis");
            radioButtonCircleByAxis.Checked = true;
            radioButtonCircleByAxis.Name = "radioButtonCircleByAxis";
            radioButtonCircleByAxis.TabStop = true;
            toolTip.SetToolTip(radioButtonCircleByAxis, resources.GetString("radioButtonCircleByAxis.ToolTip"));
            radioButtonCircleByAxis.UseVisualStyleBackColor = true;
            radioButtonCircleByAxis.CheckedChanged += radioButtonCircleByAxis_CheckedChanged;
            // 
            // buttonAddCircle
            // 
            resources.ApplyResources(buttonAddCircle, "buttonAddCircle");
            buttonAddCircle.BackColor = System.Drawing.Color.SteelBlue;
            buttonAddCircle.ForeColor = System.Drawing.SystemColors.HighlightText;
            buttonAddCircle.Name = "buttonAddCircle";
            toolTip.SetToolTip(buttonAddCircle, resources.GetString("buttonAddCircle.ToolTip"));
            buttonAddCircle.UseVisualStyleBackColor = false;
            buttonAddCircle.Click += buttonAddCircle_Click;
            // 
            // buttonDeleteCircle
            // 
            resources.ApplyResources(buttonDeleteCircle, "buttonDeleteCircle");
            buttonDeleteCircle.BackColor = System.Drawing.Color.IndianRed;
            buttonDeleteCircle.ForeColor = System.Drawing.Color.White;
            buttonDeleteCircle.Name = "buttonDeleteCircle";
            toolTip.SetToolTip(buttonDeleteCircle, resources.GetString("buttonDeleteCircle.ToolTip"));
            buttonDeleteCircle.UseVisualStyleBackColor = false;
            buttonDeleteCircle.Click += buttonDeleteCircle_Click;
            // 
            // checkedListBoxCircles
            // 
            resources.ApplyResources(checkedListBoxCircles, "checkedListBoxCircles");
            checkedListBoxCircles.FormattingEnabled = true;
            checkedListBoxCircles.Name = "checkedListBoxCircles";
            toolTip.SetToolTip(checkedListBoxCircles, resources.GetString("checkedListBoxCircles.ToolTip"));
            // 
            // tabPage3
            // 
            resources.ApplyResources(tabPage3, "tabPage3");
            tabPage3.Controls.Add(buttonYusaModeStop);
            tabPage3.Controls.Add(buttonYusaModeStart);
            tabPage3.Controls.Add(radioButtonRotationalScan);
            tabPage3.Controls.Add(radioButtonZigzagScan);
            tabPage3.Controls.Add(checkBox3);
            tabPage3.Controls.Add(checkBox2);
            tabPage3.Controls.Add(label37);
            tabPage3.Controls.Add(label9);
            tabPage3.Controls.Add(label8);
            tabPage3.Controls.Add(label32);
            tabPage3.Controls.Add(label26);
            tabPage3.Controls.Add(label27);
            tabPage3.Controls.Add(label36);
            tabPage3.Controls.Add(label30);
            tabPage3.Controls.Add(checkBox1);
            tabPage3.Controls.Add(label28);
            tabPage3.Controls.Add(label2);
            tabPage3.Controls.Add(label25);
            tabPage3.Controls.Add(label23);
            tabPage3.Controls.Add(label10);
            tabPage3.Controls.Add(label34);
            tabPage3.Controls.Add(label33);
            tabPage3.Controls.Add(label24);
            tabPage3.Controls.Add(label31);
            tabPage3.Controls.Add(label21);
            tabPage3.Controls.Add(label20);
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
            tabPage3.Name = "tabPage3";
            toolTip.SetToolTip(tabPage3, resources.GetString("tabPage3.ToolTip"));
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonYusaModeStop
            // 
            resources.ApplyResources(buttonYusaModeStop, "buttonYusaModeStop");
            buttonYusaModeStop.Name = "buttonYusaModeStop";
            toolTip.SetToolTip(buttonYusaModeStop, resources.GetString("buttonYusaModeStop.ToolTip"));
            buttonYusaModeStop.UseVisualStyleBackColor = true;
            buttonYusaModeStop.Click += buttonYusaModeStop_Click;
            // 
            // buttonYusaModeStart
            // 
            resources.ApplyResources(buttonYusaModeStart, "buttonYusaModeStart");
            buttonYusaModeStart.Name = "buttonYusaModeStart";
            toolTip.SetToolTip(buttonYusaModeStart, resources.GetString("buttonYusaModeStart.ToolTip"));
            buttonYusaModeStart.UseVisualStyleBackColor = true;
            buttonYusaModeStart.Click += buttonYusaModeStart_Click;
            // 
            // radioButtonRotationalScan
            // 
            resources.ApplyResources(radioButtonRotationalScan, "radioButtonRotationalScan");
            radioButtonRotationalScan.Name = "radioButtonRotationalScan";
            toolTip.SetToolTip(radioButtonRotationalScan, resources.GetString("radioButtonRotationalScan.ToolTip"));
            radioButtonRotationalScan.UseVisualStyleBackColor = true;
            // 
            // radioButtonZigzagScan
            // 
            resources.ApplyResources(radioButtonZigzagScan, "radioButtonZigzagScan");
            radioButtonZigzagScan.Checked = true;
            radioButtonZigzagScan.Name = "radioButtonZigzagScan";
            radioButtonZigzagScan.TabStop = true;
            toolTip.SetToolTip(radioButtonZigzagScan, resources.GetString("radioButtonZigzagScan.ToolTip"));
            radioButtonZigzagScan.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            resources.ApplyResources(checkBox3, "checkBox3");
            checkBox3.Name = "checkBox3";
            toolTip.SetToolTip(checkBox3, resources.GetString("checkBox3.ToolTip"));
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(checkBox2, "checkBox2");
            checkBox2.Name = "checkBox2";
            toolTip.SetToolTip(checkBox2, resources.GetString("checkBox2.ToolTip"));
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            resources.ApplyResources(label37, "label37");
            label37.Name = "label37";
            toolTip.SetToolTip(label37, resources.GetString("label37.ToolTip"));
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            toolTip.SetToolTip(label9, resources.GetString("label9.ToolTip"));
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            toolTip.SetToolTip(label8, resources.GetString("label8.ToolTip"));
            // 
            // label32
            // 
            resources.ApplyResources(label32, "label32");
            label32.Name = "label32";
            toolTip.SetToolTip(label32, resources.GetString("label32.ToolTip"));
            // 
            // label26
            // 
            resources.ApplyResources(label26, "label26");
            label26.Name = "label26";
            toolTip.SetToolTip(label26, resources.GetString("label26.ToolTip"));
            // 
            // label27
            // 
            resources.ApplyResources(label27, "label27");
            label27.Name = "label27";
            toolTip.SetToolTip(label27, resources.GetString("label27.ToolTip"));
            // 
            // label36
            // 
            resources.ApplyResources(label36, "label36");
            label36.Name = "label36";
            toolTip.SetToolTip(label36, resources.GetString("label36.ToolTip"));
            // 
            // label30
            // 
            resources.ApplyResources(label30, "label30");
            label30.Name = "label30";
            toolTip.SetToolTip(label30, resources.GetString("label30.ToolTip"));
            // 
            // checkBox1
            // 
            resources.ApplyResources(checkBox1, "checkBox1");
            checkBox1.Name = "checkBox1";
            toolTip.SetToolTip(checkBox1, resources.GetString("checkBox1.ToolTip"));
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            resources.ApplyResources(label28, "label28");
            label28.Name = "label28";
            toolTip.SetToolTip(label28, resources.GetString("label28.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            // 
            // label25
            // 
            resources.ApplyResources(label25, "label25");
            label25.Name = "label25";
            toolTip.SetToolTip(label25, resources.GetString("label25.ToolTip"));
            // 
            // label23
            // 
            resources.ApplyResources(label23, "label23");
            label23.Name = "label23";
            toolTip.SetToolTip(label23, resources.GetString("label23.ToolTip"));
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            toolTip.SetToolTip(label10, resources.GetString("label10.ToolTip"));
            // 
            // label34
            // 
            resources.ApplyResources(label34, "label34");
            label34.Name = "label34";
            toolTip.SetToolTip(label34, resources.GetString("label34.ToolTip"));
            // 
            // label33
            // 
            resources.ApplyResources(label33, "label33");
            label33.Name = "label33";
            toolTip.SetToolTip(label33, resources.GetString("label33.ToolTip"));
            // 
            // label24
            // 
            resources.ApplyResources(label24, "label24");
            label24.Name = "label24";
            toolTip.SetToolTip(label24, resources.GetString("label24.ToolTip"));
            // 
            // label31
            // 
            resources.ApplyResources(label31, "label31");
            label31.Name = "label31";
            toolTip.SetToolTip(label31, resources.GetString("label31.ToolTip"));
            // 
            // label21
            // 
            resources.ApplyResources(label21, "label21");
            label21.Name = "label21";
            toolTip.SetToolTip(label21, resources.GetString("label21.ToolTip"));
            // 
            // label20
            // 
            resources.ApplyResources(label20, "label20");
            label20.Name = "label20";
            toolTip.SetToolTip(label20, resources.GetString("label20.ToolTip"));
            // 
            // label22
            // 
            resources.ApplyResources(label22, "label22");
            label22.Name = "label22";
            toolTip.SetToolTip(label22, resources.GetString("label22.ToolTip"));
            // 
            // numericBoxRxSpeed
            // 
            resources.ApplyResources(numericBoxRxSpeed, "numericBoxRxSpeed");
            numericBoxRxSpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRxSpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRxSpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRxSpeed.Name = "numericBoxRxSpeed";
            numericBoxRxSpeed.RadianValue = 0.31415926535897931D;
            numericBoxRxSpeed.RoundErrorAccuracy = -1;
            numericBoxRxSpeed.SkipEventDuringInput = false;
            numericBoxRxSpeed.SmartIncrement = true;
            numericBoxRxSpeed.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxRxSpeed, resources.GetString("numericBoxRxSpeed.ToolTip"));
            numericBoxRxSpeed.Value = 18D;
            // 
            // numericBoxRySpeed
            // 
            resources.ApplyResources(numericBoxRySpeed, "numericBoxRySpeed");
            numericBoxRySpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRySpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRySpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRySpeed.Name = "numericBoxRySpeed";
            numericBoxRySpeed.RadianValue = 0.017453292519943295D;
            numericBoxRySpeed.RoundErrorAccuracy = -1;
            numericBoxRySpeed.SkipEventDuringInput = false;
            numericBoxRySpeed.SmartIncrement = true;
            numericBoxRySpeed.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxRySpeed, resources.GetString("numericBoxRySpeed.ToolTip"));
            numericBoxRySpeed.Value = 1D;
            // 
            // numericBoxRzSpeed
            // 
            resources.ApplyResources(numericBoxRzSpeed, "numericBoxRzSpeed");
            numericBoxRzSpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRzSpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRzSpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRzSpeed.Name = "numericBoxRzSpeed";
            numericBoxRzSpeed.RadianValue = 0.034906585039886591D;
            numericBoxRzSpeed.RoundErrorAccuracy = -1;
            numericBoxRzSpeed.SkipEventDuringInput = false;
            numericBoxRzSpeed.SmartIncrement = true;
            numericBoxRzSpeed.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxRzSpeed, resources.GetString("numericBoxRzSpeed.ToolTip"));
            numericBoxRzSpeed.Value = 2D;
            // 
            // numericBoxTotalTime
            // 
            resources.ApplyResources(numericBoxTotalTime, "numericBoxTotalTime");
            numericBoxTotalTime.BackColor = System.Drawing.SystemColors.Control;
            numericBoxTotalTime.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxTotalTime.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxTotalTime.Name = "numericBoxTotalTime";
            numericBoxTotalTime.RadianValue = 1.7453292519943295D;
            numericBoxTotalTime.RoundErrorAccuracy = -1;
            numericBoxTotalTime.SkipEventDuringInput = false;
            numericBoxTotalTime.SmartIncrement = true;
            numericBoxTotalTime.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxTotalTime, resources.GetString("numericBoxTotalTime.ToolTip"));
            numericBoxTotalTime.Value = 100D;
            // 
            // numericBoxAngularSpeed
            // 
            resources.ApplyResources(numericBoxAngularSpeed, "numericBoxAngularSpeed");
            numericBoxAngularSpeed.BackColor = System.Drawing.SystemColors.Control;
            numericBoxAngularSpeed.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxAngularSpeed.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxAngularSpeed.Name = "numericBoxAngularSpeed";
            numericBoxAngularSpeed.RadianValue = 0.52359877559829882D;
            numericBoxAngularSpeed.RoundErrorAccuracy = -1;
            numericBoxAngularSpeed.SkipEventDuringInput = false;
            numericBoxAngularSpeed.SmartIncrement = true;
            numericBoxAngularSpeed.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxAngularSpeed, resources.GetString("numericBoxAngularSpeed.ToolTip"));
            numericBoxAngularSpeed.Value = 30D;
            // 
            // numericBoxRyStep
            // 
            resources.ApplyResources(numericBoxRyStep, "numericBoxRyStep");
            numericBoxRyStep.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRyStep.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRyStep.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRyStep.Name = "numericBoxRyStep";
            numericBoxRyStep.RadianValue = 0.0034906585039886592D;
            numericBoxRyStep.RoundErrorAccuracy = -1;
            numericBoxRyStep.SkipEventDuringInput = false;
            numericBoxRyStep.SmartIncrement = true;
            numericBoxRyStep.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxRyStep, resources.GetString("numericBoxRyStep.ToolTip"));
            numericBoxRyStep.Value = 0.2D;
            // 
            // numericBoxRadialAngle
            // 
            resources.ApplyResources(numericBoxRadialAngle, "numericBoxRadialAngle");
            numericBoxRadialAngle.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRadialAngle.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRadialAngle.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRadialAngle.Name = "numericBoxRadialAngle";
            numericBoxRadialAngle.RadianValue = 0.13962634015954636D;
            numericBoxRadialAngle.RoundErrorAccuracy = -1;
            numericBoxRadialAngle.SkipEventDuringInput = false;
            numericBoxRadialAngle.SmartIncrement = true;
            numericBoxRadialAngle.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxRadialAngle, resources.GetString("numericBoxRadialAngle.ToolTip"));
            numericBoxRadialAngle.Value = 8D;
            // 
            // numericBoxRyOscillation
            // 
            resources.ApplyResources(numericBoxRyOscillation, "numericBoxRyOscillation");
            numericBoxRyOscillation.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRyOscillation.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRyOscillation.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRyOscillation.Name = "numericBoxRyOscillation";
            numericBoxRyOscillation.RadianValue = 0.13962634015954636D;
            numericBoxRyOscillation.RoundErrorAccuracy = -1;
            numericBoxRyOscillation.SkipEventDuringInput = false;
            numericBoxRyOscillation.SmartIncrement = true;
            numericBoxRyOscillation.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxRyOscillation, resources.GetString("numericBoxRyOscillation.ToolTip"));
            numericBoxRyOscillation.Value = 8D;
            // 
            // numericBoxRzOscillation
            // 
            resources.ApplyResources(numericBoxRzOscillation, "numericBoxRzOscillation");
            numericBoxRzOscillation.BackColor = System.Drawing.SystemColors.Control;
            numericBoxRzOscillation.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBoxRzOscillation.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBoxRzOscillation.Name = "numericBoxRzOscillation";
            numericBoxRzOscillation.RadianValue = 0.13962634015954636D;
            numericBoxRzOscillation.RoundErrorAccuracy = -1;
            numericBoxRzOscillation.SkipEventDuringInput = false;
            numericBoxRzOscillation.SmartIncrement = true;
            numericBoxRzOscillation.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBoxRzOscillation, resources.GetString("numericBoxRzOscillation.ToolTip"));
            numericBoxRzOscillation.Value = 8D;
            // 
            // tabPage4
            // 
            resources.ApplyResources(tabPage4, "tabPage4");
            tabPage4.Controls.Add(waveLengthControl);
            tabPage4.Name = "tabPage4";
            toolTip.SetToolTip(tabPage4, resources.GetString("tabPage4.ToolTip"));
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // waveLengthControl
            // 
            resources.ApplyResources(waveLengthControl, "waveLengthControl");
            waveLengthControl.Direction = System.Windows.Forms.FlowDirection.TopDown;
            waveLengthControl.Energy = 20D;
            waveLengthControl.Monochrome = true;
            waveLengthControl.Name = "waveLengthControl";
            waveLengthControl.ShowWaveSource = true;
            toolTip.SetToolTip(waveLengthControl, resources.GetString("waveLengthControl.ToolTip"));
            waveLengthControl.WaveLength = 0.0085885141045000009D;
            waveLengthControl.WaveSource = WaveSource.Electron;
            waveLengthControl.XrayWaveSourceElementNumber = 0;
            waveLengthControl.XrayWaveSourceLine = XrayLine.Ka1;
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Name = "menuStrip1";
            toolTip.SetToolTip(menuStrip1, resources.GetString("menuStrip1.ToolTip"));
            // 
            // fileToolStripMenuItem
            // 
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveImageToolStripMenuItem, toolStripMenuItem1, toolStripSeparator1, toolStripMenuItem2, toolStripSeparator2, pageSetupToolStripMenuItem, printPreviewToolStripMenuItem, printToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            // 
            // saveImageToolStripMenuItem
            // 
            resources.ApplyResources(saveImageToolStripMenuItem, "saveImageToolStripMenuItem");
            saveImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { asImageToolStripMenuItem, asMetafileToolStripMenuItem1 });
            saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            // 
            // asImageToolStripMenuItem
            // 
            resources.ApplyResources(asImageToolStripMenuItem, "asImageToolStripMenuItem");
            asImageToolStripMenuItem.Name = "asImageToolStripMenuItem";
            asImageToolStripMenuItem.Click += saveImageToolStripMenuItem_Click;
            // 
            // asMetafileToolStripMenuItem1
            // 
            resources.ApplyResources(asMetafileToolStripMenuItem1, "asMetafileToolStripMenuItem1");
            asMetafileToolStripMenuItem1.Name = "asMetafileToolStripMenuItem1";
            // 
            // toolStripMenuItem1
            // 
            resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
            toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { asBitmapToolStripMenuItem, asMetafileToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // asBitmapToolStripMenuItem
            // 
            resources.ApplyResources(asBitmapToolStripMenuItem, "asBitmapToolStripMenuItem");
            asBitmapToolStripMenuItem.Name = "asBitmapToolStripMenuItem";
            asBitmapToolStripMenuItem.Click += copyImageToClipboardToolStripMenuItem_Click;
            // 
            // asMetafileToolStripMenuItem
            // 
            resources.ApplyResources(asMetafileToolStripMenuItem, "asMetafileToolStripMenuItem");
            asMetafileToolStripMenuItem.Name = "asMetafileToolStripMenuItem";
            asMetafileToolStripMenuItem.Click += copyMetafileToClipboardToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // toolStripMenuItem2
            // 
            resources.ApplyResources(toolStripMenuItem2, "toolStripMenuItem2");
            toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemSaveMovieStereonet, toolStripMenuItemSaveMovie3D });
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            // 
            // toolStripMenuItemSaveMovieStereonet
            // 
            resources.ApplyResources(toolStripMenuItemSaveMovieStereonet, "toolStripMenuItemSaveMovieStereonet");
            toolStripMenuItemSaveMovieStereonet.Name = "toolStripMenuItemSaveMovieStereonet";
            toolStripMenuItemSaveMovieStereonet.Click += toolStripMenuItemSaveMovieStereonet_Click;
            // 
            // toolStripMenuItemSaveMovie3D
            // 
            resources.ApplyResources(toolStripMenuItemSaveMovie3D, "toolStripMenuItemSaveMovie3D");
            toolStripMenuItemSaveMovie3D.Name = "toolStripMenuItemSaveMovie3D";
            toolStripMenuItemSaveMovie3D.Click += toolStripMenuItemSaveMovie3D_Click;
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // pageSetupToolStripMenuItem
            // 
            resources.ApplyResources(pageSetupToolStripMenuItem, "pageSetupToolStripMenuItem");
            pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
            pageSetupToolStripMenuItem.Click += pageSetupToolStripMenuItem_Click;
            // 
            // printPreviewToolStripMenuItem
            // 
            resources.ApplyResources(printPreviewToolStripMenuItem, "printPreviewToolStripMenuItem");
            printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            printPreviewToolStripMenuItem.Click += printPreviewToolStripMenuItem_Click;
            // 
            // printToolStripMenuItem
            // 
            resources.ApplyResources(printToolStripMenuItem, "printToolStripMenuItem");
            printToolStripMenuItem.Name = "printToolStripMenuItem";
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
            // labelKV
            // 
            resources.ApplyResources(labelKV, "labelKV");
            labelKV.Name = "labelKV";
            toolTip.SetToolTip(labelKV, resources.GetString("labelKV.ToolTip"));
            // 
            // labelHU
            // 
            resources.ApplyResources(labelHU, "labelHU");
            labelHU.Name = "labelHU";
            toolTip.SetToolTip(labelHU, resources.GetString("labelHU.ToolTip"));
            // 
            // labelLW
            // 
            resources.ApplyResources(labelLW, "labelLW");
            labelLW.Name = "labelLW";
            toolTip.SetToolTip(labelLW, resources.GetString("labelLW.ToolTip"));
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
            // groupBox5
            // 
            resources.ApplyResources(groupBox5, "groupBox5");
            groupBox5.Controls.Add(panelSpecifiedIndices);
            groupBox5.Controls.Add(flowLayoutPanel3);
            groupBox5.Name = "groupBox5";
            groupBox5.TabStop = false;
            toolTip.SetToolTip(groupBox5, resources.GetString("groupBox5.ToolTip"));
            // 
            // panelSpecifiedIndices
            // 
            resources.ApplyResources(panelSpecifiedIndices, "panelSpecifiedIndices");
            panelSpecifiedIndices.Controls.Add(checkBoxIncludingEquivalentPlanes);
            panelSpecifiedIndices.Controls.Add(buttonRemoveIndex);
            panelSpecifiedIndices.Controls.Add(buttonAddIndex);
            panelSpecifiedIndices.Controls.Add(listBoxSpecifiedIndices);
            panelSpecifiedIndices.Name = "panelSpecifiedIndices";
            toolTip.SetToolTip(panelSpecifiedIndices, resources.GetString("panelSpecifiedIndices.ToolTip"));
            // 
            // checkBoxIncludingEquivalentPlanes
            // 
            resources.ApplyResources(checkBoxIncludingEquivalentPlanes, "checkBoxIncludingEquivalentPlanes");
            checkBoxIncludingEquivalentPlanes.Name = "checkBoxIncludingEquivalentPlanes";
            toolTip.SetToolTip(checkBoxIncludingEquivalentPlanes, resources.GetString("checkBoxIncludingEquivalentPlanes.ToolTip"));
            checkBoxIncludingEquivalentPlanes.UseVisualStyleBackColor = true;
            checkBoxIncludingEquivalentPlanes.CheckedChanged += checkBoxIncludingEquivalentPlanes_CheckedChanged;
            // 
            // buttonRemoveIndex
            // 
            resources.ApplyResources(buttonRemoveIndex, "buttonRemoveIndex");
            buttonRemoveIndex.BackColor = System.Drawing.Color.IndianRed;
            buttonRemoveIndex.ForeColor = System.Drawing.Color.White;
            buttonRemoveIndex.Name = "buttonRemoveIndex";
            toolTip.SetToolTip(buttonRemoveIndex, resources.GetString("buttonRemoveIndex.ToolTip"));
            buttonRemoveIndex.UseVisualStyleBackColor = false;
            buttonRemoveIndex.Click += buttonRemoveIndex_Click;
            // 
            // buttonAddIndex
            // 
            resources.ApplyResources(buttonAddIndex, "buttonAddIndex");
            buttonAddIndex.BackColor = System.Drawing.Color.SteelBlue;
            buttonAddIndex.ForeColor = System.Drawing.Color.White;
            buttonAddIndex.Name = "buttonAddIndex";
            toolTip.SetToolTip(buttonAddIndex, resources.GetString("buttonAddIndex.ToolTip"));
            buttonAddIndex.UseVisualStyleBackColor = false;
            buttonAddIndex.Click += buttonAddIndex_Click;
            // 
            // listBoxSpecifiedIndices
            // 
            resources.ApplyResources(listBoxSpecifiedIndices, "listBoxSpecifiedIndices");
            listBoxSpecifiedIndices.FormattingEnabled = true;
            listBoxSpecifiedIndices.MultiColumn = true;
            listBoxSpecifiedIndices.Name = "listBoxSpecifiedIndices";
            toolTip.SetToolTip(listBoxSpecifiedIndices, resources.GetString("listBoxSpecifiedIndices.ToolTip"));
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(flowLayoutPanel3, "flowLayoutPanel3");
            flowLayoutPanel3.Controls.Add(flowLayoutPanel5);
            flowLayoutPanel3.Controls.Add(tableLayoutPanel3);
            flowLayoutPanel3.Controls.Add(numericBoxHighStructureFactor);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            toolTip.SetToolTip(flowLayoutPanel3, resources.GetString("flowLayoutPanel3.ToolTip"));
            // 
            // flowLayoutPanel5
            // 
            resources.ApplyResources(flowLayoutPanel5, "flowLayoutPanel5");
            flowLayoutPanel5.Controls.Add(radioButtonRange);
            flowLayoutPanel5.Controls.Add(radioButtonSpecifiedIndices);
            flowLayoutPanel5.Controls.Add(radioButtonHighStructureFactor);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            toolTip.SetToolTip(flowLayoutPanel5, resources.GetString("flowLayoutPanel5.ToolTip"));
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(tableLayoutPanel3, "tableLayoutPanel3");
            tableLayoutPanel3.Controls.Add(labelKV, 1, 0);
            tableLayoutPanel3.Controls.Add(numericBox3, 2, 1);
            tableLayoutPanel3.Controls.Add(numericBox2, 1, 1);
            tableLayoutPanel3.Controls.Add(numericBox1, 0, 1);
            tableLayoutPanel3.Controls.Add(labelHU, 0, 0);
            tableLayoutPanel3.Controls.Add(labelLW, 2, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            toolTip.SetToolTip(tableLayoutPanel3, resources.GetString("tableLayoutPanel3.ToolTip"));
            // 
            // numericBox3
            // 
            resources.ApplyResources(numericBox3, "numericBox3");
            numericBox3.BackColor = System.Drawing.Color.Transparent;
            numericBox3.Maximum = 20D;
            numericBox3.Minimum = 0D;
            numericBox3.Name = "numericBox3";
            numericBox3.RadianValue = 0.034906585039886591D;
            numericBox3.RoundErrorAccuracy = -1;
            numericBox3.ShowUpDown = true;
            numericBox3.SkipEventDuringInput = false;
            numericBox3.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBox3, resources.GetString("numericBox3.ToolTip"));
            numericBox3.Value = 2D;
            numericBox3.ValueChanged += numericUpDown_ValueChanged;
            // 
            // numericBox2
            // 
            resources.ApplyResources(numericBox2, "numericBox2");
            numericBox2.BackColor = System.Drawing.Color.Transparent;
            numericBox2.Maximum = 20D;
            numericBox2.Minimum = 0D;
            numericBox2.Name = "numericBox2";
            numericBox2.RadianValue = 0.034906585039886591D;
            numericBox2.RoundErrorAccuracy = -1;
            numericBox2.ShowUpDown = true;
            numericBox2.SkipEventDuringInput = false;
            numericBox2.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBox2, resources.GetString("numericBox2.ToolTip"));
            numericBox2.Value = 2D;
            numericBox2.ValueChanged += numericUpDown_ValueChanged;
            // 
            // numericBox1
            // 
            resources.ApplyResources(numericBox1, "numericBox1");
            numericBox1.BackColor = System.Drawing.Color.Transparent;
            numericBox1.Maximum = 20D;
            numericBox1.Minimum = 0D;
            numericBox1.Name = "numericBox1";
            numericBox1.RadianValue = 0.034906585039886591D;
            numericBox1.RoundErrorAccuracy = -1;
            numericBox1.ShowUpDown = true;
            numericBox1.SkipEventDuringInput = false;
            numericBox1.ThonsandsSeparator = true;
            toolTip.SetToolTip(numericBox1, resources.GetString("numericBox1.ToolTip"));
            numericBox1.Value = 2D;
            numericBox1.ValueChanged += numericUpDown_ValueChanged;
            // 
            // numericBoxHighStructureFactor
            // 
            resources.ApplyResources(numericBoxHighStructureFactor, "numericBoxHighStructureFactor");
            numericBoxHighStructureFactor.BackColor = System.Drawing.Color.Transparent;
            numericBoxHighStructureFactor.Maximum = 1000D;
            numericBoxHighStructureFactor.Minimum = 1D;
            numericBoxHighStructureFactor.Name = "numericBoxHighStructureFactor";
            numericBoxHighStructureFactor.RadianValue = 1.7453292519943295D;
            numericBoxHighStructureFactor.RoundErrorAccuracy = -1;
            numericBoxHighStructureFactor.ShowUpDown = true;
            numericBoxHighStructureFactor.SmartIncrement = true;
            toolTip.SetToolTip(numericBoxHighStructureFactor, resources.GetString("numericBoxHighStructureFactor.ToolTip1"));
            numericBoxHighStructureFactor.Value = 100D;
            numericBoxHighStructureFactor.ValueChanged += numericBoxHighStructureFactor_ValueChanged;
            // 
            // panel3
            // 
            resources.ApplyResources(panel3, "panel3");
            panel3.Controls.Add(groupBox5);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(panel1);
            panel3.Controls.Add(groupBox2);
            panel3.Controls.Add(panel3DOptions);
            panel3.Name = "panel3";
            toolTip.SetToolTip(panel3, resources.GetString("panel3.ToolTip"));
            // 
            // panel4
            // 
            resources.ApplyResources(panel4, "panel4");
            panel4.Name = "panel4";
            toolTip.SetToolTip(panel4, resources.GetString("panel4.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            toolTip.SetToolTip(panel1, resources.GetString("panel1.ToolTip"));
            // 
            // panel3DOptions
            // 
            resources.ApplyResources(panel3DOptions, "panel3DOptions");
            panel3DOptions.Controls.Add(checkBoxDisplay3D);
            panel3DOptions.Controls.Add(groupBox3DOptions);
            panel3DOptions.Name = "panel3DOptions";
            toolTip.SetToolTip(panel3DOptions, resources.GetString("panel3DOptions.ToolTip"));
            // 
            // checkBoxDisplay3D
            // 
            resources.ApplyResources(checkBoxDisplay3D, "checkBoxDisplay3D");
            checkBoxDisplay3D.Name = "checkBoxDisplay3D";
            toolTip.SetToolTip(checkBoxDisplay3D, resources.GetString("checkBoxDisplay3D.ToolTip"));
            checkBoxDisplay3D.UseVisualStyleBackColor = true;
            checkBoxDisplay3D.CheckedChanged += checkBoxDisplay3D_CheckedChanged;
            // 
            // groupBox3DOptions
            // 
            resources.ApplyResources(groupBox3DOptions, "groupBox3DOptions");
            groupBox3DOptions.Controls.Add(label29);
            groupBox3DOptions.Controls.Add(checkBox3dOptionSphere);
            groupBox3DOptions.Controls.Add(checkBox3dOptionSemisphere);
            groupBox3DOptions.Controls.Add(trackBarDepthFadingOut);
            groupBox3DOptions.Controls.Add(button3D_reset);
            groupBox3DOptions.Controls.Add(checkBox3dOptionProjectionLine);
            groupBox3DOptions.Controls.Add(checkBox3dOptionStereonet);
            groupBox3DOptions.Controls.Add(checkBox3dOptionLabel);
            groupBox3DOptions.Name = "groupBox3DOptions";
            groupBox3DOptions.TabStop = false;
            toolTip.SetToolTip(groupBox3DOptions, resources.GetString("groupBox3DOptions.ToolTip"));
            // 
            // label29
            // 
            resources.ApplyResources(label29, "label29");
            label29.Name = "label29";
            toolTip.SetToolTip(label29, resources.GetString("label29.ToolTip"));
            // 
            // checkBox3dOptionSphere
            // 
            resources.ApplyResources(checkBox3dOptionSphere, "checkBox3dOptionSphere");
            checkBox3dOptionSphere.Checked = true;
            checkBox3dOptionSphere.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3dOptionSphere.Name = "checkBox3dOptionSphere";
            toolTip.SetToolTip(checkBox3dOptionSphere, resources.GetString("checkBox3dOptionSphere.ToolTip"));
            checkBox3dOptionSphere.UseVisualStyleBackColor = true;
            checkBox3dOptionSphere.CheckedChanged += checkBox3dOptionSphere_CheckedChanged;
            // 
            // checkBox3dOptionSemisphere
            // 
            resources.ApplyResources(checkBox3dOptionSemisphere, "checkBox3dOptionSemisphere");
            checkBox3dOptionSemisphere.Checked = true;
            checkBox3dOptionSemisphere.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3dOptionSemisphere.Name = "checkBox3dOptionSemisphere";
            toolTip.SetToolTip(checkBox3dOptionSemisphere, resources.GetString("checkBox3dOptionSemisphere.ToolTip"));
            checkBox3dOptionSemisphere.UseVisualStyleBackColor = true;
            checkBox3dOptionSemisphere.CheckedChanged += checkBox3dOptionSphere_CheckedChanged;
            // 
            // trackBarDepthFadingOut
            // 
            resources.ApplyResources(trackBarDepthFadingOut, "trackBarDepthFadingOut");
            trackBarDepthFadingOut.Name = "trackBarDepthFadingOut";
            toolTip.SetToolTip(trackBarDepthFadingOut, resources.GetString("trackBarDepthFadingOut.ToolTip"));
            trackBarDepthFadingOut.Value = 5;
            trackBarDepthFadingOut.Scroll += trackBarDepthFadingOut_Scroll;
            // 
            // button3D_reset
            // 
            resources.ApplyResources(button3D_reset, "button3D_reset");
            button3D_reset.Name = "button3D_reset";
            toolTip.SetToolTip(button3D_reset, resources.GetString("button3D_reset.ToolTip"));
            button3D_reset.UseVisualStyleBackColor = true;
            button3D_reset.Click += button3D_reset_Click;
            // 
            // checkBox3dOptionProjectionLine
            // 
            resources.ApplyResources(checkBox3dOptionProjectionLine, "checkBox3dOptionProjectionLine");
            checkBox3dOptionProjectionLine.Checked = true;
            checkBox3dOptionProjectionLine.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3dOptionProjectionLine.Name = "checkBox3dOptionProjectionLine";
            toolTip.SetToolTip(checkBox3dOptionProjectionLine, resources.GetString("checkBox3dOptionProjectionLine.ToolTip"));
            checkBox3dOptionProjectionLine.UseVisualStyleBackColor = true;
            checkBox3dOptionProjectionLine.CheckedChanged += checkBox3dOptionProjectionLine_CheckedChanged;
            // 
            // checkBox3dOptionStereonet
            // 
            resources.ApplyResources(checkBox3dOptionStereonet, "checkBox3dOptionStereonet");
            checkBox3dOptionStereonet.Checked = true;
            checkBox3dOptionStereonet.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3dOptionStereonet.Name = "checkBox3dOptionStereonet";
            toolTip.SetToolTip(checkBox3dOptionStereonet, resources.GetString("checkBox3dOptionStereonet.ToolTip"));
            checkBox3dOptionStereonet.UseVisualStyleBackColor = true;
            checkBox3dOptionStereonet.CheckedChanged += checkBox3dOptionSphere_CheckedChanged;
            // 
            // checkBox3dOptionLabel
            // 
            resources.ApplyResources(checkBox3dOptionLabel, "checkBox3dOptionLabel");
            checkBox3dOptionLabel.Checked = true;
            checkBox3dOptionLabel.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3dOptionLabel.Name = "checkBox3dOptionLabel";
            toolTip.SetToolTip(checkBox3dOptionLabel, resources.GetString("checkBox3dOptionLabel.ToolTip"));
            checkBox3dOptionLabel.UseVisualStyleBackColor = true;
            checkBox3dOptionLabel.CheckedChanged += checkBox3dOptionSphere_CheckedChanged;
            // 
            // scalablePictureBoxAdvanced1
            // 
            resources.ApplyResources(scalablePictureBoxAdvanced1, "scalablePictureBoxAdvanced1");
            scalablePictureBoxAdvanced1.FixZoomAndCenter = false;
            scalablePictureBoxAdvanced1.FrequencyGraphVisible = true;
            scalablePictureBoxAdvanced1.ImageFilter_DustAndScratches = false;
            scalablePictureBoxAdvanced1.ImageFilter_DustAndScratchesRadius = 1D;
            scalablePictureBoxAdvanced1.ImageFilter_DustAndScratchesThreshold = 3D;
            scalablePictureBoxAdvanced1.ImageFilter_DustAndScratchesVisible = true;
            scalablePictureBoxAdvanced1.ImageFilter_GaussianBlur = false;
            scalablePictureBoxAdvanced1.ImageFilter_GaussianBlurRadius = 1D;
            scalablePictureBoxAdvanced1.ImageFilter_GaussianBlurVisible = true;
            scalablePictureBoxAdvanced1.ImageFilterVisible = true;
            scalablePictureBoxAdvanced1.LogScaleBar = false;
            scalablePictureBoxAdvanced1.LowerIntensity = 0D;
            scalablePictureBoxAdvanced1.MaximumIntensity = 255D;
            scalablePictureBoxAdvanced1.MinimumIntensity = 0D;
            scalablePictureBoxAdvanced1.MousePositionLabelVisible = true;
            scalablePictureBoxAdvanced1.Name = "scalablePictureBoxAdvanced1";
            scalablePictureBoxAdvanced1.PictureSize = new System.Drawing.Size(410, 1241843762);
            scalablePictureBoxAdvanced1.ShowGradiaent = true;
            scalablePictureBoxAdvanced1.SkipDrawing = false;
            scalablePictureBoxAdvanced1.StatusLabel = " ";
            scalablePictureBoxAdvanced1.StatusProgress = 0D;
            scalablePictureBoxAdvanced1.StatusVisible = true;
            scalablePictureBoxAdvanced1.Title = ((string, System.Drawing.Font, System.Drawing.Color, System.Drawing.Color))resources.GetObject("scalablePictureBoxAdvanced1.Title");
            scalablePictureBoxAdvanced1.TitleVisible = false;
            toolTip.SetToolTip(scalablePictureBoxAdvanced1, resources.GetString("scalablePictureBoxAdvanced1.ToolTip"));
            scalablePictureBoxAdvanced1.TrackBarVisible = true;
            scalablePictureBoxAdvanced1.UpperIntensity = 255D;
            scalablePictureBoxAdvanced1.VisibleGradient = true;
            // 
            // scalablePictureBoxAdvanced2
            // 
            resources.ApplyResources(scalablePictureBoxAdvanced2, "scalablePictureBoxAdvanced2");
            scalablePictureBoxAdvanced2.FixZoomAndCenter = false;
            scalablePictureBoxAdvanced2.FrequencyGraphVisible = true;
            scalablePictureBoxAdvanced2.ImageFilter_DustAndScratches = false;
            scalablePictureBoxAdvanced2.ImageFilter_DustAndScratchesRadius = 1D;
            scalablePictureBoxAdvanced2.ImageFilter_DustAndScratchesThreshold = 3D;
            scalablePictureBoxAdvanced2.ImageFilter_DustAndScratchesVisible = true;
            scalablePictureBoxAdvanced2.ImageFilter_GaussianBlur = false;
            scalablePictureBoxAdvanced2.ImageFilter_GaussianBlurRadius = 1D;
            scalablePictureBoxAdvanced2.ImageFilter_GaussianBlurVisible = true;
            scalablePictureBoxAdvanced2.ImageFilterVisible = true;
            scalablePictureBoxAdvanced2.LogScaleBar = false;
            scalablePictureBoxAdvanced2.LowerIntensity = 0D;
            scalablePictureBoxAdvanced2.MaximumIntensity = 255D;
            scalablePictureBoxAdvanced2.MinimumIntensity = 0D;
            scalablePictureBoxAdvanced2.MousePositionLabelVisible = true;
            scalablePictureBoxAdvanced2.Name = "scalablePictureBoxAdvanced2";
            scalablePictureBoxAdvanced2.PictureSize = new System.Drawing.Size(410, 1241843762);
            scalablePictureBoxAdvanced2.ShowGradiaent = true;
            scalablePictureBoxAdvanced2.SkipDrawing = false;
            scalablePictureBoxAdvanced2.StatusLabel = " ";
            scalablePictureBoxAdvanced2.StatusProgress = 0D;
            scalablePictureBoxAdvanced2.StatusVisible = true;
            scalablePictureBoxAdvanced2.Title = ((string, System.Drawing.Font, System.Drawing.Color, System.Drawing.Color))resources.GetObject("scalablePictureBoxAdvanced2.Title");
            scalablePictureBoxAdvanced2.TitleVisible = false;
            toolTip.SetToolTip(scalablePictureBoxAdvanced2, resources.GetString("scalablePictureBoxAdvanced2.ToolTip"));
            scalablePictureBoxAdvanced2.TrackBarVisible = true;
            scalablePictureBoxAdvanced2.UpperIntensity = 255D;
            scalablePictureBoxAdvanced2.VisibleGradient = true;
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
            // FormStereonet
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(panel3);
            Controls.Add(tabControl);
            Controls.Add(labelYpos);
            Controls.Add(labelXpos);
            Controls.Add(menuStrip1);
            Controls.Add(splitContainer1);
            KeyPreview = true;
            MainMenuStrip = menuStrip1;
            Name = "FormStereonet";
            toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
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
            groupBox2.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            panelPlanes.ResumeLayout(false);
            panelPlanes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleH1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleH2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleL2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleL1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleK1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleK2).EndInit();
            panelAxis.ResumeLayout(false);
            panelAxis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleU).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleV).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCircleW).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox5.ResumeLayout(false);
            panelSpecifiedIndices.ResumeLayout(false);
            panelSpecifiedIndices.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel3DOptions.ResumeLayout(false);
            panel3DOptions.PerformLayout();
            groupBox3DOptions.ResumeLayout(false);
            groupBox3DOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarDepthFadingOut).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarStrSize;
        private System.Windows.Forms.TrackBar trackBarPointSize;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonAxes;
        private System.Windows.Forms.RadioButton radioButtonPlanes;
        private System.Windows.Forms.Label labelYpos;
        private System.Windows.Forms.Label labelXpos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox1DegLine;
        private System.Windows.Forms.RadioButton radioButtonOutlinePole;
        private System.Windows.Forms.RadioButton radioButtonOutlineEquator;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
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
        public System.Windows.Forms.NumericUpDown numericUpDownCircleW;
        public System.Windows.Forms.NumericUpDown numericUpDownCircleV;
        public System.Windows.Forms.NumericUpDown numericUpDownCircleU;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button buttonAddCircle;
        private System.Windows.Forms.Button buttonDeleteCircle;
        private System.Windows.Forms.RadioButton radioButtonCircleByAxis;
        private System.Windows.Forms.RadioButton radioButtonCircleByPlanes;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.NumericUpDown numericUpDownCircleL2;
        public System.Windows.Forms.NumericUpDown numericUpDownCircleL1;
        public System.Windows.Forms.NumericUpDown numericUpDownCircleK2;
        public System.Windows.Forms.NumericUpDown numericUpDownCircleK1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.NumericUpDown numericUpDownCircleH2;
        public System.Windows.Forms.NumericUpDown numericUpDownCircleH1;
        private System.Windows.Forms.CheckedListBox checkedListBoxCircles;
        private System.Windows.Forms.Panel panelPlanes;
        private System.Windows.Forms.Panel panelAxis;
        public ColorControl colorControlString;
        public ColorControl colorControlUniqueAxis;
        public ColorControl colorControlUniquePlane;
        public ColorControl colorControlGeneralAxis;
        public ColorControl colorControlGeneralPlane;
        public ColorControl colorControlBackGround;
        public ColorControl colorControl90DegLine;
        public ColorControl colorControl10DegLine;
        public ColorControl colorControl1DegLine;
        public ColorControl colorControlGreatCircle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton radioButtonSchmidt;
        private System.Windows.Forms.RadioButton radioButtonWulff;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button buttonYusaModeStop;
        private System.Windows.Forms.Button buttonYusaModeStart;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        public NumericBox numericBoxRxSpeed;
        public NumericBox numericBoxRySpeed;
        public NumericBox numericBoxRzSpeed;
        public NumericBox numericBoxRyOscillation;
        public NumericBox numericBoxRzOscillation;
        public NumericBox numericBoxRyStep;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label labelKV;
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
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.RadioButton radioButtonRotationalScan;
        public System.Windows.Forms.RadioButton radioButtonZigzagScan;
        private System.Windows.Forms.Panel panel3;
        private ScalablePictureBoxAdvanced scalablePictureBoxAdvanced1;
        private ScalablePictureBoxAdvanced scalablePictureBoxAdvanced2;
        public ImagingSolution.Control.GraphicsBox graphicsBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem asBitmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asMetafileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asMetafileToolStripMenuItem1;
        private NumericBox numericBox3;
        private NumericBox numericBox2;
        private NumericBox numericBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelHU;
        private System.Windows.Forms.Label labelLW;
        private System.Windows.Forms.RadioButton radioButtonKikuchiLinePairs;
        private System.Windows.Forms.TabPage tabPage4;
        private WaveLengthControl waveLengthControl;
        private System.Windows.Forms.RadioButton radioButtonHighStructureFactor;
        private NumericBox numericBoxHighStructureFactor;
        private System.Windows.Forms.CheckBox checkBoxReflectStructureFactor;
        public ColorControl colorControl1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.RadioButton radioButtonUpperSphere;
        private System.Windows.Forms.RadioButton radioButtonLowerSphere;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.CheckBox checkBoxShowIndexLabels;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
    }
}