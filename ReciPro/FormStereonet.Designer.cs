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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStereonet));
            this.trackBarStrSize = new System.Windows.Forms.TrackBar();
            this.trackBarPointSize = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonWulff = new System.Windows.Forms.RadioButton();
            this.radioButtonSchmidt = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonAxes = new System.Windows.Forms.RadioButton();
            this.radioButtonPlanes = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.labelPlusMinus2 = new System.Windows.Forms.Label();
            this.labelPlusMinus1 = new System.Windows.Forms.Label();
            this.labelPlusMinus3 = new System.Windows.Forms.Label();
            this.labelYpos = new System.Windows.Forms.Label();
            this.labelXpos = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1DegLine = new System.Windows.Forms.CheckBox();
            this.radioButtonOutlinePole = new System.Windows.Forms.RadioButton();
            this.radioButtonOutlineEquator = new System.Windows.Forms.RadioButton();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colorControlSmallCircle = new Crystallography.Controls.ColorControl();
            this.colorControlGreatCircle = new Crystallography.Controls.ColorControl();
            this.label4 = new System.Windows.Forms.Label();
            this.colorControlString = new Crystallography.Controls.ColorControl();
            this.colorControlUniqueAxis = new Crystallography.Controls.ColorControl();
            this.colorControlUniquePlane = new Crystallography.Controls.ColorControl();
            this.labelUniqueAxis = new System.Windows.Forms.Label();
            this.colorControlGeneralAxis = new Crystallography.Controls.ColorControl();
            this.labelGeneralAxis = new System.Windows.Forms.Label();
            this.labelUniquePlane = new System.Windows.Forms.Label();
            this.labelGeneralPlane = new System.Windows.Forms.Label();
            this.colorControlGeneralPlane = new Crystallography.Controls.ColorControl();
            this.colorControlBackGround = new Crystallography.Controls.ColorControl();
            this.labelBackGround = new System.Windows.Forms.Label();
            this.colorControl90DegLine = new Crystallography.Controls.ColorControl();
            this.label90DegLine = new System.Windows.Forms.Label();
            this.label10DegLine = new System.Windows.Forms.Label();
            this.colorControl10DegLine = new Crystallography.Controls.ColorControl();
            this.label1DegLine = new System.Windows.Forms.Label();
            this.colorControl1DegLine = new Crystallography.Controls.ColorControl();
            this.labelString = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelPlanes = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownCircleH1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCircleH2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCircleL2 = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownCircleL1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCircleK1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCircleK2 = new System.Windows.Forms.NumericUpDown();
            this.panelAxis = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownCircleU = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCircleV = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownCircleW = new System.Windows.Forms.NumericUpDown();
            this.radioButtonCircleByPlanes = new System.Windows.Forms.RadioButton();
            this.radioButtonCircleByAxis = new System.Windows.Forms.RadioButton();
            this.buttonAddCircle = new System.Windows.Forms.Button();
            this.buttonDeleteCircle = new System.Windows.Forms.Button();
            this.checkedListBoxCircles = new System.Windows.Forms.CheckedListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonYusaModeStop = new System.Windows.Forms.Button();
            this.buttonYusaModeStart = new System.Windows.Forms.Button();
            this.numericalTextBoxRxSpeed = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxRySpeed = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxRzSpeed = new Crystallography.Controls.NumericBox();
            this.radioButtonRotationalScan = new System.Windows.Forms.RadioButton();
            this.radioButtonZigzagScan = new System.Windows.Forms.RadioButton();
            this.numericalTextBoxTotalTime = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxAngularSpeed = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxRyStep = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxRadialAngle = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxRyOscillation = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxRzOscillation = new Crystallography.Controls.NumericBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asMetafileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.asBitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asMetafileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pageSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.labelHU = new System.Windows.Forms.Label();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panelSpecifiedIndices = new System.Windows.Forms.Panel();
            this.buttonRemoveIndex = new System.Windows.Forms.Button();
            this.buttonAddIndex = new System.Windows.Forms.Button();
            this.listBoxSpecifiedIndices = new System.Windows.Forms.ListBox();
            this.checkBoxIncludingEquivalentPlanes = new System.Windows.Forms.CheckBox();
            this.radioButtonRange = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButtonSpecifiedIndices = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.scalablePictureBoxAdvanced1 = new Crystallography.Controls.ScalablePictureBoxAdvanced();
            this.scalablePictureBoxAdvanced2 = new Crystallography.Controls.ScalablePictureBoxAdvanced();
            this.graphicsBox = new ImagingSolution.Control.GraphicsBox(this.components);
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStrSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPointSize)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panelPlanes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleH1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleH2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleL2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleL1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleK1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleK2)).BeginInit();
            this.panelAxis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleW)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.panelSpecifiedIndices.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.graphicsBox)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBarStrSize
            // 
            resources.ApplyResources(this.trackBarStrSize, "trackBarStrSize");
            this.trackBarStrSize.Maximum = 300;
            this.trackBarStrSize.Minimum = 1;
            this.trackBarStrSize.Name = "trackBarStrSize";
            this.trackBarStrSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip1.SetToolTip(this.trackBarStrSize, resources.GetString("trackBarStrSize.ToolTip"));
            this.trackBarStrSize.Value = 80;
            this.trackBarStrSize.Scroll += new System.EventHandler(this.trackBarStrSize_Scroll);
            // 
            // trackBarPointSize
            // 
            resources.ApplyResources(this.trackBarPointSize, "trackBarPointSize");
            this.trackBarPointSize.Maximum = 20;
            this.trackBarPointSize.Minimum = 1;
            this.trackBarPointSize.Name = "trackBarPointSize";
            this.trackBarPointSize.TickStyle = System.Windows.Forms.TickStyle.None;
            this.toolTip1.SetToolTip(this.trackBarPointSize, resources.GetString("trackBarPointSize.ToolTip"));
            this.trackBarPointSize.Value = 5;
            this.trackBarPointSize.Scroll += new System.EventHandler(this.trackBarStrSize_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel2);
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Controls.Add(this.radioButtonWulff);
            this.flowLayoutPanel2.Controls.Add(this.radioButtonSchmidt);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // radioButtonWulff
            // 
            resources.ApplyResources(this.radioButtonWulff, "radioButtonWulff");
            this.radioButtonWulff.Checked = true;
            this.radioButtonWulff.Name = "radioButtonWulff";
            this.radioButtonWulff.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonWulff, resources.GetString("radioButtonWulff.ToolTip"));
            this.radioButtonWulff.CheckedChanged += new System.EventHandler(this.radioButtonAxes_CheckedChanged);
            // 
            // radioButtonSchmidt
            // 
            resources.ApplyResources(this.radioButtonSchmidt, "radioButtonSchmidt");
            this.radioButtonSchmidt.Name = "radioButtonSchmidt";
            this.toolTip1.SetToolTip(this.radioButtonSchmidt, resources.GetString("radioButtonSchmidt.ToolTip"));
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.radioButtonAxes);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonPlanes);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // radioButtonAxes
            // 
            resources.ApplyResources(this.radioButtonAxes, "radioButtonAxes");
            this.radioButtonAxes.Checked = true;
            this.radioButtonAxes.Name = "radioButtonAxes";
            this.radioButtonAxes.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonAxes, resources.GetString("radioButtonAxes.ToolTip"));
            this.radioButtonAxes.CheckedChanged += new System.EventHandler(this.radioButtonAxes_CheckedChanged);
            // 
            // radioButtonPlanes
            // 
            resources.ApplyResources(this.radioButtonPlanes, "radioButtonPlanes");
            this.radioButtonPlanes.Name = "radioButtonPlanes";
            this.toolTip1.SetToolTip(this.radioButtonPlanes, resources.GetString("radioButtonPlanes.ToolTip"));
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // numericUpDown3
            // 
            resources.ApplyResources(this.numericUpDown3, "numericUpDown3");
            this.numericUpDown3.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.toolTip1.SetToolTip(this.numericUpDown3, resources.GetString("numericUpDown3.ToolTip"));
            this.numericUpDown3.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // labelPlusMinus2
            // 
            resources.ApplyResources(this.labelPlusMinus2, "labelPlusMinus2");
            this.labelPlusMinus2.Name = "labelPlusMinus2";
            this.toolTip1.SetToolTip(this.labelPlusMinus2, resources.GetString("labelPlusMinus2.ToolTip"));
            // 
            // labelPlusMinus1
            // 
            resources.ApplyResources(this.labelPlusMinus1, "labelPlusMinus1");
            this.labelPlusMinus1.Name = "labelPlusMinus1";
            this.toolTip1.SetToolTip(this.labelPlusMinus1, resources.GetString("labelPlusMinus1.ToolTip"));
            // 
            // labelPlusMinus3
            // 
            resources.ApplyResources(this.labelPlusMinus3, "labelPlusMinus3");
            this.labelPlusMinus3.Name = "labelPlusMinus3";
            this.toolTip1.SetToolTip(this.labelPlusMinus3, resources.GetString("labelPlusMinus3.ToolTip"));
            // 
            // labelYpos
            // 
            resources.ApplyResources(this.labelYpos, "labelYpos");
            this.labelYpos.Name = "labelYpos";
            // 
            // labelXpos
            // 
            resources.ApplyResources(this.labelXpos, "labelXpos");
            this.labelXpos.Name = "labelXpos";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1DegLine);
            this.groupBox3.Controls.Add(this.radioButtonOutlinePole);
            this.groupBox3.Controls.Add(this.radioButtonOutlineEquator);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // checkBox1DegLine
            // 
            resources.ApplyResources(this.checkBox1DegLine, "checkBox1DegLine");
            this.checkBox1DegLine.Name = "checkBox1DegLine";
            this.checkBox1DegLine.CheckedChanged += new System.EventHandler(this.checkBox1DegLine_CheckedChanged);
            // 
            // radioButtonOutlinePole
            // 
            resources.ApplyResources(this.radioButtonOutlinePole, "radioButtonOutlinePole");
            this.radioButtonOutlinePole.Name = "radioButtonOutlinePole";
            this.toolTip1.SetToolTip(this.radioButtonOutlinePole, resources.GetString("radioButtonOutlinePole.ToolTip"));
            // 
            // radioButtonOutlineEquator
            // 
            this.radioButtonOutlineEquator.Checked = true;
            resources.ApplyResources(this.radioButtonOutlineEquator, "radioButtonOutlineEquator");
            this.radioButtonOutlineEquator.Name = "radioButtonOutlineEquator";
            this.radioButtonOutlineEquator.TabStop = true;
            this.toolTip1.SetToolTip(this.radioButtonOutlineEquator, resources.GetString("radioButtonOutlineEquator.ToolTip"));
            this.radioButtonOutlineEquator.CheckedChanged += new System.EventHandler(this.radioButtonOutlineEquator_CheckedChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Click += new System.EventHandler(this.tabControl_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.trackBarPointSize);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.trackBarStrSize);
            this.groupBox4.Controls.Add(this.label6);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.colorControlSmallCircle);
            this.groupBox1.Controls.Add(this.colorControlGreatCircle);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.colorControlString);
            this.groupBox1.Controls.Add(this.colorControlUniqueAxis);
            this.groupBox1.Controls.Add(this.colorControlUniquePlane);
            this.groupBox1.Controls.Add(this.labelUniqueAxis);
            this.groupBox1.Controls.Add(this.colorControlGeneralAxis);
            this.groupBox1.Controls.Add(this.labelGeneralAxis);
            this.groupBox1.Controls.Add(this.labelUniquePlane);
            this.groupBox1.Controls.Add(this.labelGeneralPlane);
            this.groupBox1.Controls.Add(this.colorControlGeneralPlane);
            this.groupBox1.Controls.Add(this.colorControlBackGround);
            this.groupBox1.Controls.Add(this.labelBackGround);
            this.groupBox1.Controls.Add(this.colorControl90DegLine);
            this.groupBox1.Controls.Add(this.label90DegLine);
            this.groupBox1.Controls.Add(this.label10DegLine);
            this.groupBox1.Controls.Add(this.colorControl10DegLine);
            this.groupBox1.Controls.Add(this.label1DegLine);
            this.groupBox1.Controls.Add(this.colorControl1DegLine);
            this.groupBox1.Controls.Add(this.labelString);
            this.groupBox1.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // colorControlSmallCircle
            // 
            this.colorControlSmallCircle.Argb = -16256;
            this.colorControlSmallCircle.Blue = 128;
            this.colorControlSmallCircle.BlueF = 0.5019608F;
            this.colorControlSmallCircle.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.colorControlSmallCircle.Green = 192;
            this.colorControlSmallCircle.GreenF = 0.7529412F;
            resources.ApplyResources(this.colorControlSmallCircle, "colorControlSmallCircle");
            this.colorControlSmallCircle.Name = "colorControlSmallCircle";
            this.colorControlSmallCircle.Red = 255;
            this.colorControlSmallCircle.RedF = 1F;
            this.colorControlSmallCircle.ToolTip = "";
            this.colorControlSmallCircle.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // colorControlGreatCircle
            // 
            this.colorControlGreatCircle.Argb = -32768;
            this.colorControlGreatCircle.Blue = 0;
            this.colorControlGreatCircle.BlueF = 0F;
            this.colorControlGreatCircle.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.colorControlGreatCircle.Green = 128;
            this.colorControlGreatCircle.GreenF = 0.5019608F;
            resources.ApplyResources(this.colorControlGreatCircle, "colorControlGreatCircle");
            this.colorControlGreatCircle.Name = "colorControlGreatCircle";
            this.colorControlGreatCircle.Red = 255;
            this.colorControlGreatCircle.RedF = 1F;
            this.colorControlGreatCircle.ToolTip = "";
            this.colorControlGreatCircle.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // colorControlString
            // 
            this.colorControlString.Argb = -16777216;
            this.colorControlString.BackColor = System.Drawing.Color.Black;
            this.colorControlString.Blue = 0;
            this.colorControlString.BlueF = 0F;
            this.colorControlString.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorControlString.Cursor = System.Windows.Forms.Cursors.Default;
            this.colorControlString.Green = 0;
            this.colorControlString.GreenF = 0F;
            resources.ApplyResources(this.colorControlString, "colorControlString");
            this.colorControlString.Name = "colorControlString";
            this.colorControlString.Red = 0;
            this.colorControlString.RedF = 0F;
            this.colorControlString.TabStop = false;
            this.colorControlString.ToolTip = "指数文字の色";
            this.toolTip1.SetToolTip(this.colorControlString, resources.GetString("colorControlString.ToolTip"));
            this.colorControlString.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // colorControlUniqueAxis
            // 
            this.colorControlUniqueAxis.Argb = -7667712;
            this.colorControlUniqueAxis.BackColor = System.Drawing.Color.Red;
            this.colorControlUniqueAxis.Blue = 0;
            this.colorControlUniqueAxis.BlueF = 0F;
            this.colorControlUniqueAxis.Color = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorControlUniqueAxis.Cursor = System.Windows.Forms.Cursors.Default;
            this.colorControlUniqueAxis.Green = 0;
            this.colorControlUniqueAxis.GreenF = 0F;
            resources.ApplyResources(this.colorControlUniqueAxis, "colorControlUniqueAxis");
            this.colorControlUniqueAxis.Name = "colorControlUniqueAxis";
            this.colorControlUniqueAxis.Red = 139;
            this.colorControlUniqueAxis.RedF = 0.5450981F;
            this.colorControlUniqueAxis.TabStop = false;
            this.colorControlUniqueAxis.ToolTip = "軸表示時の[100], [010], [001]の表示色";
            this.toolTip1.SetToolTip(this.colorControlUniqueAxis, resources.GetString("colorControlUniqueAxis.ToolTip"));
            this.colorControlUniqueAxis.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // colorControlUniquePlane
            // 
            this.colorControlUniquePlane.Argb = -16751616;
            this.colorControlUniquePlane.BackColor = System.Drawing.Color.Lime;
            this.colorControlUniquePlane.Blue = 0;
            this.colorControlUniquePlane.BlueF = 0F;
            this.colorControlUniquePlane.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.colorControlUniquePlane.Cursor = System.Windows.Forms.Cursors.Default;
            this.colorControlUniquePlane.Green = 100;
            this.colorControlUniquePlane.GreenF = 0.3921569F;
            resources.ApplyResources(this.colorControlUniquePlane, "colorControlUniquePlane");
            this.colorControlUniquePlane.Name = "colorControlUniquePlane";
            this.colorControlUniquePlane.Red = 0;
            this.colorControlUniquePlane.RedF = 0F;
            this.colorControlUniquePlane.TabStop = false;
            this.colorControlUniquePlane.ToolTip = "結晶面(100), (010), (001)の色";
            this.toolTip1.SetToolTip(this.colorControlUniquePlane, resources.GetString("colorControlUniquePlane.ToolTip"));
            this.colorControlUniquePlane.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // labelUniqueAxis
            // 
            resources.ApplyResources(this.labelUniqueAxis, "labelUniqueAxis");
            this.labelUniqueAxis.Name = "labelUniqueAxis";
            this.toolTip1.SetToolTip(this.labelUniqueAxis, resources.GetString("labelUniqueAxis.ToolTip"));
            // 
            // colorControlGeneralAxis
            // 
            this.colorControlGeneralAxis.Argb = -65536;
            this.colorControlGeneralAxis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.colorControlGeneralAxis.Blue = 0;
            this.colorControlGeneralAxis.BlueF = 0F;
            this.colorControlGeneralAxis.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.colorControlGeneralAxis.Cursor = System.Windows.Forms.Cursors.Default;
            this.colorControlGeneralAxis.Green = 0;
            this.colorControlGeneralAxis.GreenF = 0F;
            resources.ApplyResources(this.colorControlGeneralAxis, "colorControlGeneralAxis");
            this.colorControlGeneralAxis.Name = "colorControlGeneralAxis";
            this.colorControlGeneralAxis.Red = 255;
            this.colorControlGeneralAxis.RedF = 1F;
            this.colorControlGeneralAxis.TabStop = false;
            this.colorControlGeneralAxis.ToolTip = "";
            this.colorControlGeneralAxis.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // labelGeneralAxis
            // 
            resources.ApplyResources(this.labelGeneralAxis, "labelGeneralAxis");
            this.labelGeneralAxis.Name = "labelGeneralAxis";
            this.toolTip1.SetToolTip(this.labelGeneralAxis, resources.GetString("labelGeneralAxis.ToolTip"));
            // 
            // labelUniquePlane
            // 
            resources.ApplyResources(this.labelUniquePlane, "labelUniquePlane");
            this.labelUniquePlane.Name = "labelUniquePlane";
            this.toolTip1.SetToolTip(this.labelUniquePlane, resources.GetString("labelUniquePlane.ToolTip"));
            // 
            // labelGeneralPlane
            // 
            resources.ApplyResources(this.labelGeneralPlane, "labelGeneralPlane");
            this.labelGeneralPlane.Name = "labelGeneralPlane";
            this.toolTip1.SetToolTip(this.labelGeneralPlane, resources.GetString("labelGeneralPlane.ToolTip"));
            // 
            // colorControlGeneralPlane
            // 
            this.colorControlGeneralPlane.Argb = -14578910;
            this.colorControlGeneralPlane.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.colorControlGeneralPlane.Blue = 34;
            this.colorControlGeneralPlane.BlueF = 0.1333333F;
            this.colorControlGeneralPlane.Color = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(139)))), ((int)(((byte)(34)))));
            this.colorControlGeneralPlane.Cursor = System.Windows.Forms.Cursors.Default;
            this.colorControlGeneralPlane.Green = 139;
            this.colorControlGeneralPlane.GreenF = 0.5450981F;
            resources.ApplyResources(this.colorControlGeneralPlane, "colorControlGeneralPlane");
            this.colorControlGeneralPlane.Name = "colorControlGeneralPlane";
            this.colorControlGeneralPlane.Red = 33;
            this.colorControlGeneralPlane.RedF = 0.1294118F;
            this.colorControlGeneralPlane.TabStop = false;
            this.colorControlGeneralPlane.ToolTip = "";
            this.colorControlGeneralPlane.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // colorControlBackGround
            // 
            this.colorControlBackGround.Argb = -1;
            this.colorControlBackGround.BackColor = System.Drawing.Color.White;
            this.colorControlBackGround.Blue = 255;
            this.colorControlBackGround.BlueF = 1F;
            this.colorControlBackGround.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.colorControlBackGround.Green = 255;
            this.colorControlBackGround.GreenF = 1F;
            resources.ApplyResources(this.colorControlBackGround, "colorControlBackGround");
            this.colorControlBackGround.Name = "colorControlBackGround";
            this.colorControlBackGround.Red = 255;
            this.colorControlBackGround.RedF = 1F;
            this.colorControlBackGround.TabStop = false;
            this.colorControlBackGround.ToolTip = "背景色";
            this.toolTip1.SetToolTip(this.colorControlBackGround, resources.GetString("colorControlBackGround.ToolTip"));
            this.colorControlBackGround.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // labelBackGround
            // 
            resources.ApplyResources(this.labelBackGround, "labelBackGround");
            this.labelBackGround.Name = "labelBackGround";
            this.toolTip1.SetToolTip(this.labelBackGround, resources.GetString("labelBackGround.ToolTip"));
            // 
            // colorControl90DegLine
            // 
            this.colorControl90DegLine.Argb = -16776961;
            this.colorControl90DegLine.BackColor = System.Drawing.Color.Blue;
            this.colorControl90DegLine.Blue = 255;
            this.colorControl90DegLine.BlueF = 1F;
            this.colorControl90DegLine.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.colorControl90DegLine.Cursor = System.Windows.Forms.Cursors.Default;
            this.colorControl90DegLine.Green = 0;
            this.colorControl90DegLine.GreenF = 0F;
            resources.ApplyResources(this.colorControl90DegLine, "colorControl90DegLine");
            this.colorControl90DegLine.Name = "colorControl90DegLine";
            this.colorControl90DegLine.Red = 0;
            this.colorControl90DegLine.RedF = 0F;
            this.colorControl90DegLine.TabStop = false;
            this.colorControl90DegLine.ToolTip = "90度線の色";
            this.toolTip1.SetToolTip(this.colorControl90DegLine, resources.GetString("colorControl90DegLine.ToolTip"));
            this.colorControl90DegLine.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // label90DegLine
            // 
            resources.ApplyResources(this.label90DegLine, "label90DegLine");
            this.label90DegLine.Name = "label90DegLine";
            this.toolTip1.SetToolTip(this.label90DegLine, resources.GetString("label90DegLine.ToolTip"));
            // 
            // label10DegLine
            // 
            resources.ApplyResources(this.label10DegLine, "label10DegLine");
            this.label10DegLine.Name = "label10DegLine";
            this.toolTip1.SetToolTip(this.label10DegLine, resources.GetString("label10DegLine.ToolTip"));
            // 
            // colorControl10DegLine
            // 
            this.colorControl10DegLine.Argb = -8355585;
            this.colorControl10DegLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.colorControl10DegLine.Blue = 255;
            this.colorControl10DegLine.BlueF = 1F;
            this.colorControl10DegLine.Color = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.colorControl10DegLine.Cursor = System.Windows.Forms.Cursors.Default;
            this.colorControl10DegLine.Green = 128;
            this.colorControl10DegLine.GreenF = 0.5019608F;
            resources.ApplyResources(this.colorControl10DegLine, "colorControl10DegLine");
            this.colorControl10DegLine.Name = "colorControl10DegLine";
            this.colorControl10DegLine.Red = 128;
            this.colorControl10DegLine.RedF = 0.5019608F;
            this.colorControl10DegLine.TabStop = false;
            this.colorControl10DegLine.ToolTip = "10度線の色";
            this.toolTip1.SetToolTip(this.colorControl10DegLine, resources.GetString("colorControl10DegLine.ToolTip"));
            this.colorControl10DegLine.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // label1DegLine
            // 
            resources.ApplyResources(this.label1DegLine, "label1DegLine");
            this.label1DegLine.Name = "label1DegLine";
            this.toolTip1.SetToolTip(this.label1DegLine, resources.GetString("label1DegLine.ToolTip"));
            // 
            // colorControl1DegLine
            // 
            this.colorControl1DegLine.Argb = -4144897;
            this.colorControl1DegLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.colorControl1DegLine.Blue = 255;
            this.colorControl1DegLine.BlueF = 1F;
            this.colorControl1DegLine.Color = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.colorControl1DegLine.Cursor = System.Windows.Forms.Cursors.Default;
            this.colorControl1DegLine.Green = 192;
            this.colorControl1DegLine.GreenF = 0.7529412F;
            resources.ApplyResources(this.colorControl1DegLine, "colorControl1DegLine");
            this.colorControl1DegLine.Name = "colorControl1DegLine";
            this.colorControl1DegLine.Red = 192;
            this.colorControl1DegLine.RedF = 0.7529412F;
            this.colorControl1DegLine.TabStop = false;
            this.colorControl1DegLine.ToolTip = "1度線の色\r\n「Show 1°line」がチェックされているとき有効";
            this.toolTip1.SetToolTip(this.colorControl1DegLine, resources.GetString("colorControl1DegLine.ToolTip"));
            this.colorControl1DegLine.ColorChanged += new EventHandler(this.colorControl_ColorChanged);
            // 
            // labelString
            // 
            resources.ApplyResources(this.labelString, "labelString");
            this.labelString.Name = "labelString";
            this.toolTip1.SetToolTip(this.labelString, resources.GetString("labelString.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.panelPlanes);
            this.tabPage2.Controls.Add(this.panelAxis);
            this.tabPage2.Controls.Add(this.radioButtonCircleByPlanes);
            this.tabPage2.Controls.Add(this.radioButtonCircleByAxis);
            this.tabPage2.Controls.Add(this.buttonAddCircle);
            this.tabPage2.Controls.Add(this.buttonDeleteCircle);
            this.tabPage2.Controls.Add(this.checkedListBoxCircles);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // panelPlanes
            // 
            this.panelPlanes.Controls.Add(this.label12);
            this.panelPlanes.Controls.Add(this.label13);
            this.panelPlanes.Controls.Add(this.label16);
            this.panelPlanes.Controls.Add(this.label14);
            this.panelPlanes.Controls.Add(this.label17);
            this.panelPlanes.Controls.Add(this.numericUpDownCircleH1);
            this.panelPlanes.Controls.Add(this.numericUpDownCircleH2);
            this.panelPlanes.Controls.Add(this.numericUpDownCircleL2);
            this.panelPlanes.Controls.Add(this.label15);
            this.panelPlanes.Controls.Add(this.numericUpDownCircleL1);
            this.panelPlanes.Controls.Add(this.numericUpDownCircleK1);
            this.panelPlanes.Controls.Add(this.numericUpDownCircleK2);
            resources.ApplyResources(this.panelPlanes, "panelPlanes");
            this.panelPlanes.Name = "panelPlanes";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // numericUpDownCircleH1
            // 
            resources.ApplyResources(this.numericUpDownCircleH1, "numericUpDownCircleH1");
            this.numericUpDownCircleH1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCircleH1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownCircleH1.Name = "numericUpDownCircleH1";
            this.numericUpDownCircleH1.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDownCircleH2
            // 
            resources.ApplyResources(this.numericUpDownCircleH2, "numericUpDownCircleH2");
            this.numericUpDownCircleH2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCircleH2.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownCircleH2.Name = "numericUpDownCircleH2";
            this.numericUpDownCircleH2.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDownCircleL2
            // 
            resources.ApplyResources(this.numericUpDownCircleL2, "numericUpDownCircleL2");
            this.numericUpDownCircleL2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCircleL2.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownCircleL2.Name = "numericUpDownCircleL2";
            this.numericUpDownCircleL2.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // numericUpDownCircleL1
            // 
            resources.ApplyResources(this.numericUpDownCircleL1, "numericUpDownCircleL1");
            this.numericUpDownCircleL1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCircleL1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownCircleL1.Name = "numericUpDownCircleL1";
            this.numericUpDownCircleL1.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDownCircleK1
            // 
            resources.ApplyResources(this.numericUpDownCircleK1, "numericUpDownCircleK1");
            this.numericUpDownCircleK1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCircleK1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownCircleK1.Name = "numericUpDownCircleK1";
            this.numericUpDownCircleK1.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDownCircleK2
            // 
            resources.ApplyResources(this.numericUpDownCircleK2, "numericUpDownCircleK2");
            this.numericUpDownCircleK2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCircleK2.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownCircleK2.Name = "numericUpDownCircleK2";
            this.numericUpDownCircleK2.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // panelAxis
            // 
            this.panelAxis.Controls.Add(this.label11);
            this.panelAxis.Controls.Add(this.label5);
            this.panelAxis.Controls.Add(this.label7);
            this.panelAxis.Controls.Add(this.numericUpDownCircleU);
            this.panelAxis.Controls.Add(this.numericUpDownCircleV);
            this.panelAxis.Controls.Add(this.numericUpDownCircleW);
            resources.ApplyResources(this.panelAxis, "panelAxis");
            this.panelAxis.Name = "panelAxis";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // numericUpDownCircleU
            // 
            resources.ApplyResources(this.numericUpDownCircleU, "numericUpDownCircleU");
            this.numericUpDownCircleU.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCircleU.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownCircleU.Name = "numericUpDownCircleU";
            this.numericUpDownCircleU.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDownCircleV
            // 
            resources.ApplyResources(this.numericUpDownCircleV, "numericUpDownCircleV");
            this.numericUpDownCircleV.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCircleV.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownCircleV.Name = "numericUpDownCircleV";
            this.numericUpDownCircleV.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDownCircleW
            // 
            resources.ApplyResources(this.numericUpDownCircleW, "numericUpDownCircleW");
            this.numericUpDownCircleW.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownCircleW.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.numericUpDownCircleW.Name = "numericUpDownCircleW";
            this.numericUpDownCircleW.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // radioButtonCircleByPlanes
            // 
            resources.ApplyResources(this.radioButtonCircleByPlanes, "radioButtonCircleByPlanes");
            this.radioButtonCircleByPlanes.Name = "radioButtonCircleByPlanes";
            this.radioButtonCircleByPlanes.UseVisualStyleBackColor = true;
            // 
            // radioButtonCircleByAxis
            // 
            resources.ApplyResources(this.radioButtonCircleByAxis, "radioButtonCircleByAxis");
            this.radioButtonCircleByAxis.Checked = true;
            this.radioButtonCircleByAxis.Name = "radioButtonCircleByAxis";
            this.radioButtonCircleByAxis.TabStop = true;
            this.radioButtonCircleByAxis.UseVisualStyleBackColor = true;
            this.radioButtonCircleByAxis.CheckedChanged += new System.EventHandler(this.radioButtonCircleByAxis_CheckedChanged);
            // 
            // buttonAddCircle
            // 
            resources.ApplyResources(this.buttonAddCircle, "buttonAddCircle");
            this.buttonAddCircle.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAddCircle.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.buttonAddCircle.Name = "buttonAddCircle";
            this.buttonAddCircle.UseVisualStyleBackColor = false;
            this.buttonAddCircle.Click += new System.EventHandler(this.buttonAddCircle_Click);
            // 
            // buttonDeleteCircle
            // 
            resources.ApplyResources(this.buttonDeleteCircle, "buttonDeleteCircle");
            this.buttonDeleteCircle.BackColor = System.Drawing.Color.IndianRed;
            this.buttonDeleteCircle.ForeColor = System.Drawing.Color.White;
            this.buttonDeleteCircle.Name = "buttonDeleteCircle";
            this.buttonDeleteCircle.UseVisualStyleBackColor = false;
            this.buttonDeleteCircle.Click += new System.EventHandler(this.buttonDeleteCircle_Click);
            // 
            // checkedListBoxCircles
            // 
            resources.ApplyResources(this.checkedListBoxCircles, "checkedListBoxCircles");
            this.checkedListBoxCircles.FormattingEnabled = true;
            this.checkedListBoxCircles.Name = "checkedListBoxCircles";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonYusaModeStop);
            this.tabPage3.Controls.Add(this.buttonYusaModeStart);
            this.tabPage3.Controls.Add(this.numericalTextBoxRxSpeed);
            this.tabPage3.Controls.Add(this.numericalTextBoxRySpeed);
            this.tabPage3.Controls.Add(this.numericalTextBoxRzSpeed);
            this.tabPage3.Controls.Add(this.radioButtonRotationalScan);
            this.tabPage3.Controls.Add(this.radioButtonZigzagScan);
            this.tabPage3.Controls.Add(this.numericalTextBoxTotalTime);
            this.tabPage3.Controls.Add(this.numericalTextBoxAngularSpeed);
            this.tabPage3.Controls.Add(this.numericalTextBoxRyStep);
            this.tabPage3.Controls.Add(this.numericalTextBoxRadialAngle);
            this.tabPage3.Controls.Add(this.numericalTextBoxRyOscillation);
            this.tabPage3.Controls.Add(this.numericalTextBoxRzOscillation);
            this.tabPage3.Controls.Add(this.checkBox3);
            this.tabPage3.Controls.Add(this.checkBox2);
            this.tabPage3.Controls.Add(this.label37);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label32);
            this.tabPage3.Controls.Add(this.label26);
            this.tabPage3.Controls.Add(this.label27);
            this.tabPage3.Controls.Add(this.label36);
            this.tabPage3.Controls.Add(this.label30);
            this.tabPage3.Controls.Add(this.checkBox1);
            this.tabPage3.Controls.Add(this.label28);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label25);
            this.tabPage3.Controls.Add(this.label23);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label34);
            this.tabPage3.Controls.Add(this.label33);
            this.tabPage3.Controls.Add(this.label24);
            this.tabPage3.Controls.Add(this.label31);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Controls.Add(this.label22);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonYusaModeStop
            // 
            resources.ApplyResources(this.buttonYusaModeStop, "buttonYusaModeStop");
            this.buttonYusaModeStop.Name = "buttonYusaModeStop";
            this.buttonYusaModeStop.UseVisualStyleBackColor = true;
            this.buttonYusaModeStop.Click += new System.EventHandler(this.buttonYusaModeStop_Click);
            // 
            // buttonYusaModeStart
            // 
            resources.ApplyResources(this.buttonYusaModeStart, "buttonYusaModeStart");
            this.buttonYusaModeStart.Name = "buttonYusaModeStart";
            this.buttonYusaModeStart.UseVisualStyleBackColor = true;
            this.buttonYusaModeStart.Click += new System.EventHandler(this.buttonYusaModeStart_Click);
            // 
            // numericalTextBoxRxSpeed
            // 
            this.numericalTextBoxRxSpeed.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxRxSpeed, "numericalTextBoxRxSpeed");
            this.numericalTextBoxRxSpeed.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxRxSpeed.DecimalPlaces = -1;
            this.numericalTextBoxRxSpeed.Maximum = double.PositiveInfinity;
            this.numericalTextBoxRxSpeed.Minimum = double.NegativeInfinity;
            this.numericalTextBoxRxSpeed.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxRxSpeed.MouseSpeed = 1D;
            this.numericalTextBoxRxSpeed.Multiline = false;
            this.numericalTextBoxRxSpeed.Name = "numericalTextBoxRxSpeed";
            this.numericalTextBoxRxSpeed.RadianValue = 0.31415926535897931D;
            this.numericalTextBoxRxSpeed.ReadOnly = false;
            this.numericalTextBoxRxSpeed.RestrictLimitValue = true;
            this.numericalTextBoxRxSpeed.ShowFraction = false;
            this.numericalTextBoxRxSpeed.ShowPositiveSign = false;
            this.numericalTextBoxRxSpeed.ShowUpDown = false;
            this.numericalTextBoxRxSpeed.SkipEventDuringInput = false;
            this.numericalTextBoxRxSpeed.SmartIncrement = true;
            this.numericalTextBoxRxSpeed.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxRxSpeed.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxRxSpeed.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxRxSpeed.ThonsandsSeparator = true;
            this.numericalTextBoxRxSpeed.UpDown_Increment = 1D;
            this.numericalTextBoxRxSpeed.Value = 18D;
            this.numericalTextBoxRxSpeed.WordWrap = true;
            // 
            // numericalTextBoxRySpeed
            // 
            this.numericalTextBoxRySpeed.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxRySpeed, "numericalTextBoxRySpeed");
            this.numericalTextBoxRySpeed.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxRySpeed.DecimalPlaces = -1;
            this.numericalTextBoxRySpeed.Maximum = double.PositiveInfinity;
            this.numericalTextBoxRySpeed.Minimum = double.NegativeInfinity;
            this.numericalTextBoxRySpeed.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxRySpeed.MouseSpeed = 1D;
            this.numericalTextBoxRySpeed.Multiline = false;
            this.numericalTextBoxRySpeed.Name = "numericalTextBoxRySpeed";
            this.numericalTextBoxRySpeed.RadianValue = 0.017453292519943295D;
            this.numericalTextBoxRySpeed.ReadOnly = false;
            this.numericalTextBoxRySpeed.RestrictLimitValue = true;
            this.numericalTextBoxRySpeed.ShowFraction = false;
            this.numericalTextBoxRySpeed.ShowPositiveSign = false;
            this.numericalTextBoxRySpeed.ShowUpDown = false;
            this.numericalTextBoxRySpeed.SkipEventDuringInput = false;
            this.numericalTextBoxRySpeed.SmartIncrement = true;
            this.numericalTextBoxRySpeed.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxRySpeed.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxRySpeed.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxRySpeed.ThonsandsSeparator = true;
            this.numericalTextBoxRySpeed.UpDown_Increment = 1D;
            this.numericalTextBoxRySpeed.Value = 1D;
            this.numericalTextBoxRySpeed.WordWrap = true;
            // 
            // numericalTextBoxRzSpeed
            // 
            this.numericalTextBoxRzSpeed.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxRzSpeed, "numericalTextBoxRzSpeed");
            this.numericalTextBoxRzSpeed.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxRzSpeed.DecimalPlaces = -1;
            this.numericalTextBoxRzSpeed.Maximum = double.PositiveInfinity;
            this.numericalTextBoxRzSpeed.Minimum = double.NegativeInfinity;
            this.numericalTextBoxRzSpeed.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxRzSpeed.MouseSpeed = 1D;
            this.numericalTextBoxRzSpeed.Multiline = false;
            this.numericalTextBoxRzSpeed.Name = "numericalTextBoxRzSpeed";
            this.numericalTextBoxRzSpeed.RadianValue = 0.034906585039886591D;
            this.numericalTextBoxRzSpeed.ReadOnly = false;
            this.numericalTextBoxRzSpeed.RestrictLimitValue = true;
            this.numericalTextBoxRzSpeed.ShowFraction = false;
            this.numericalTextBoxRzSpeed.ShowPositiveSign = false;
            this.numericalTextBoxRzSpeed.ShowUpDown = false;
            this.numericalTextBoxRzSpeed.SkipEventDuringInput = false;
            this.numericalTextBoxRzSpeed.SmartIncrement = true;
            this.numericalTextBoxRzSpeed.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxRzSpeed.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxRzSpeed.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxRzSpeed.ThonsandsSeparator = true;
            this.numericalTextBoxRzSpeed.UpDown_Increment = 1D;
            this.numericalTextBoxRzSpeed.Value = 2D;
            this.numericalTextBoxRzSpeed.WordWrap = true;
            // 
            // radioButtonRotationalScan
            // 
            resources.ApplyResources(this.radioButtonRotationalScan, "radioButtonRotationalScan");
            this.radioButtonRotationalScan.Name = "radioButtonRotationalScan";
            this.radioButtonRotationalScan.UseVisualStyleBackColor = true;
            // 
            // radioButtonZigzagScan
            // 
            resources.ApplyResources(this.radioButtonZigzagScan, "radioButtonZigzagScan");
            this.radioButtonZigzagScan.Checked = true;
            this.radioButtonZigzagScan.Name = "radioButtonZigzagScan";
            this.radioButtonZigzagScan.TabStop = true;
            this.radioButtonZigzagScan.UseVisualStyleBackColor = true;
            // 
            // numericalTextBoxTotalTime
            // 
            this.numericalTextBoxTotalTime.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxTotalTime, "numericalTextBoxTotalTime");
            this.numericalTextBoxTotalTime.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxTotalTime.DecimalPlaces = -1;
            this.numericalTextBoxTotalTime.Maximum = double.PositiveInfinity;
            this.numericalTextBoxTotalTime.Minimum = double.NegativeInfinity;
            this.numericalTextBoxTotalTime.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxTotalTime.MouseSpeed = 1D;
            this.numericalTextBoxTotalTime.Multiline = false;
            this.numericalTextBoxTotalTime.Name = "numericalTextBoxTotalTime";
            this.numericalTextBoxTotalTime.RadianValue = 1.7453292519943295D;
            this.numericalTextBoxTotalTime.ReadOnly = false;
            this.numericalTextBoxTotalTime.RestrictLimitValue = true;
            this.numericalTextBoxTotalTime.ShowFraction = false;
            this.numericalTextBoxTotalTime.ShowPositiveSign = false;
            this.numericalTextBoxTotalTime.ShowUpDown = false;
            this.numericalTextBoxTotalTime.SkipEventDuringInput = false;
            this.numericalTextBoxTotalTime.SmartIncrement = true;
            this.numericalTextBoxTotalTime.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxTotalTime.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxTotalTime.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxTotalTime.ThonsandsSeparator = true;
            this.numericalTextBoxTotalTime.UpDown_Increment = 1D;
            this.numericalTextBoxTotalTime.Value = 100D;
            this.numericalTextBoxTotalTime.WordWrap = true;
            // 
            // numericalTextBoxAngularSpeed
            // 
            this.numericalTextBoxAngularSpeed.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxAngularSpeed, "numericalTextBoxAngularSpeed");
            this.numericalTextBoxAngularSpeed.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxAngularSpeed.DecimalPlaces = -1;
            this.numericalTextBoxAngularSpeed.Maximum = double.PositiveInfinity;
            this.numericalTextBoxAngularSpeed.Minimum = double.NegativeInfinity;
            this.numericalTextBoxAngularSpeed.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxAngularSpeed.MouseSpeed = 1D;
            this.numericalTextBoxAngularSpeed.Multiline = false;
            this.numericalTextBoxAngularSpeed.Name = "numericalTextBoxAngularSpeed";
            this.numericalTextBoxAngularSpeed.RadianValue = 0.52359877559829882D;
            this.numericalTextBoxAngularSpeed.ReadOnly = false;
            this.numericalTextBoxAngularSpeed.RestrictLimitValue = true;
            this.numericalTextBoxAngularSpeed.ShowFraction = false;
            this.numericalTextBoxAngularSpeed.ShowPositiveSign = false;
            this.numericalTextBoxAngularSpeed.ShowUpDown = false;
            this.numericalTextBoxAngularSpeed.SkipEventDuringInput = false;
            this.numericalTextBoxAngularSpeed.SmartIncrement = true;
            this.numericalTextBoxAngularSpeed.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxAngularSpeed.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxAngularSpeed.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxAngularSpeed.ThonsandsSeparator = true;
            this.numericalTextBoxAngularSpeed.UpDown_Increment = 1D;
            this.numericalTextBoxAngularSpeed.Value = 30D;
            this.numericalTextBoxAngularSpeed.WordWrap = true;
            // 
            // numericalTextBoxRyStep
            // 
            this.numericalTextBoxRyStep.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxRyStep, "numericalTextBoxRyStep");
            this.numericalTextBoxRyStep.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxRyStep.DecimalPlaces = -1;
            this.numericalTextBoxRyStep.Maximum = double.PositiveInfinity;
            this.numericalTextBoxRyStep.Minimum = double.NegativeInfinity;
            this.numericalTextBoxRyStep.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxRyStep.MouseSpeed = 1D;
            this.numericalTextBoxRyStep.Multiline = false;
            this.numericalTextBoxRyStep.Name = "numericalTextBoxRyStep";
            this.numericalTextBoxRyStep.RadianValue = 0.0034906585039886592D;
            this.numericalTextBoxRyStep.ReadOnly = false;
            this.numericalTextBoxRyStep.RestrictLimitValue = true;
            this.numericalTextBoxRyStep.ShowFraction = false;
            this.numericalTextBoxRyStep.ShowPositiveSign = false;
            this.numericalTextBoxRyStep.ShowUpDown = false;
            this.numericalTextBoxRyStep.SkipEventDuringInput = false;
            this.numericalTextBoxRyStep.SmartIncrement = true;
            this.numericalTextBoxRyStep.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxRyStep.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxRyStep.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxRyStep.ThonsandsSeparator = true;
            this.numericalTextBoxRyStep.UpDown_Increment = 1D;
            this.numericalTextBoxRyStep.Value = 0.2D;
            this.numericalTextBoxRyStep.WordWrap = true;
            // 
            // numericalTextBoxRadialAngle
            // 
            this.numericalTextBoxRadialAngle.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxRadialAngle, "numericalTextBoxRadialAngle");
            this.numericalTextBoxRadialAngle.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxRadialAngle.DecimalPlaces = -1;
            this.numericalTextBoxRadialAngle.Maximum = double.PositiveInfinity;
            this.numericalTextBoxRadialAngle.Minimum = double.NegativeInfinity;
            this.numericalTextBoxRadialAngle.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxRadialAngle.MouseSpeed = 1D;
            this.numericalTextBoxRadialAngle.Multiline = false;
            this.numericalTextBoxRadialAngle.Name = "numericalTextBoxRadialAngle";
            this.numericalTextBoxRadialAngle.RadianValue = 0.13962634015954636D;
            this.numericalTextBoxRadialAngle.ReadOnly = false;
            this.numericalTextBoxRadialAngle.RestrictLimitValue = true;
            this.numericalTextBoxRadialAngle.ShowFraction = false;
            this.numericalTextBoxRadialAngle.ShowPositiveSign = false;
            this.numericalTextBoxRadialAngle.ShowUpDown = false;
            this.numericalTextBoxRadialAngle.SkipEventDuringInput = false;
            this.numericalTextBoxRadialAngle.SmartIncrement = true;
            this.numericalTextBoxRadialAngle.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxRadialAngle.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxRadialAngle.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxRadialAngle.ThonsandsSeparator = true;
            this.numericalTextBoxRadialAngle.UpDown_Increment = 1D;
            this.numericalTextBoxRadialAngle.Value = 8D;
            this.numericalTextBoxRadialAngle.WordWrap = true;
            // 
            // numericalTextBoxRyOscillation
            // 
            this.numericalTextBoxRyOscillation.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxRyOscillation, "numericalTextBoxRyOscillation");
            this.numericalTextBoxRyOscillation.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxRyOscillation.DecimalPlaces = -1;
            this.numericalTextBoxRyOscillation.Maximum = double.PositiveInfinity;
            this.numericalTextBoxRyOscillation.Minimum = double.NegativeInfinity;
            this.numericalTextBoxRyOscillation.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxRyOscillation.MouseSpeed = 1D;
            this.numericalTextBoxRyOscillation.Multiline = false;
            this.numericalTextBoxRyOscillation.Name = "numericalTextBoxRyOscillation";
            this.numericalTextBoxRyOscillation.RadianValue = 0.13962634015954636D;
            this.numericalTextBoxRyOscillation.ReadOnly = false;
            this.numericalTextBoxRyOscillation.RestrictLimitValue = true;
            this.numericalTextBoxRyOscillation.ShowFraction = false;
            this.numericalTextBoxRyOscillation.ShowPositiveSign = false;
            this.numericalTextBoxRyOscillation.ShowUpDown = false;
            this.numericalTextBoxRyOscillation.SkipEventDuringInput = false;
            this.numericalTextBoxRyOscillation.SmartIncrement = true;
            this.numericalTextBoxRyOscillation.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxRyOscillation.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxRyOscillation.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxRyOscillation.ThonsandsSeparator = true;
            this.numericalTextBoxRyOscillation.UpDown_Increment = 1D;
            this.numericalTextBoxRyOscillation.Value = 8D;
            this.numericalTextBoxRyOscillation.WordWrap = true;
            // 
            // numericalTextBoxRzOscillation
            // 
            this.numericalTextBoxRzOscillation.AllowMouseControl = false;
            resources.ApplyResources(this.numericalTextBoxRzOscillation, "numericalTextBoxRzOscillation");
            this.numericalTextBoxRzOscillation.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxRzOscillation.DecimalPlaces = -1;
            this.numericalTextBoxRzOscillation.Maximum = double.PositiveInfinity;
            this.numericalTextBoxRzOscillation.Minimum = double.NegativeInfinity;
            this.numericalTextBoxRzOscillation.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericalTextBoxRzOscillation.MouseSpeed = 1D;
            this.numericalTextBoxRzOscillation.Multiline = false;
            this.numericalTextBoxRzOscillation.Name = "numericalTextBoxRzOscillation";
            this.numericalTextBoxRzOscillation.RadianValue = 0.13962634015954636D;
            this.numericalTextBoxRzOscillation.ReadOnly = false;
            this.numericalTextBoxRzOscillation.RestrictLimitValue = true;
            this.numericalTextBoxRzOscillation.ShowFraction = false;
            this.numericalTextBoxRzOscillation.ShowPositiveSign = false;
            this.numericalTextBoxRzOscillation.ShowUpDown = false;
            this.numericalTextBoxRzOscillation.SkipEventDuringInput = false;
            this.numericalTextBoxRzOscillation.SmartIncrement = true;
            this.numericalTextBoxRzOscillation.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericalTextBoxRzOscillation.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericalTextBoxRzOscillation.TextFont = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericalTextBoxRzOscillation.ThonsandsSeparator = true;
            this.numericalTextBoxRzOscillation.UpDown_Increment = 1D;
            this.numericalTextBoxRzOscillation.Value = 8D;
            this.numericalTextBoxRzOscillation.WordWrap = true;
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label37
            // 
            resources.ApplyResources(this.label37, "label37");
            this.label37.Name = "label37";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // menuStrip1
            // 
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.pageSetupToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.printToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asImageToolStripMenuItem,
            this.asMetafileToolStripMenuItem1});
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            resources.ApplyResources(this.saveImageToolStripMenuItem, "saveImageToolStripMenuItem");
            // 
            // asImageToolStripMenuItem
            // 
            this.asImageToolStripMenuItem.Name = "asImageToolStripMenuItem";
            resources.ApplyResources(this.asImageToolStripMenuItem, "asImageToolStripMenuItem");
            this.asImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // asMetafileToolStripMenuItem1
            // 
            resources.ApplyResources(this.asMetafileToolStripMenuItem1, "asMetafileToolStripMenuItem1");
            this.asMetafileToolStripMenuItem1.Name = "asMetafileToolStripMenuItem1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asBitmapToolStripMenuItem,
            this.asMetafileToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // asBitmapToolStripMenuItem
            // 
            this.asBitmapToolStripMenuItem.Name = "asBitmapToolStripMenuItem";
            resources.ApplyResources(this.asBitmapToolStripMenuItem, "asBitmapToolStripMenuItem");
            this.asBitmapToolStripMenuItem.Click += new System.EventHandler(this.copyImageToClipboardToolStripMenuItem_Click);
            // 
            // asMetafileToolStripMenuItem
            // 
            this.asMetafileToolStripMenuItem.Name = "asMetafileToolStripMenuItem";
            resources.ApplyResources(this.asMetafileToolStripMenuItem, "asMetafileToolStripMenuItem");
            this.asMetafileToolStripMenuItem.Click += new System.EventHandler(this.copyMetafileToClipboardToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // pageSetupToolStripMenuItem
            // 
            this.pageSetupToolStripMenuItem.Name = "pageSetupToolStripMenuItem";
            resources.ApplyResources(this.pageSetupToolStripMenuItem, "pageSetupToolStripMenuItem");
            this.pageSetupToolStripMenuItem.Click += new System.EventHandler(this.pageSetupToolStripMenuItem_Click);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            resources.ApplyResources(this.printPreviewToolStripMenuItem, "printPreviewToolStripMenuItem");
            this.printPreviewToolStripMenuItem.Click += new System.EventHandler(this.printPreviewToolStripMenuItem_Click);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            resources.ApplyResources(this.printToolStripMenuItem, "printToolStripMenuItem");
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // numericUpDown2
            // 
            resources.ApplyResources(this.numericUpDown2, "numericUpDown2");
            this.numericUpDown2.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.toolTip1.SetToolTip(this.numericUpDown2, resources.GetString("numericUpDown2.ToolTip"));
            this.numericUpDown2.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.toolTip1.SetToolTip(this.numericUpDown1, resources.GetString("numericUpDown1.ToolTip"));
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // labelHU
            // 
            resources.ApplyResources(this.labelHU, "labelHU");
            this.labelHU.Name = "labelHU";
            this.toolTip1.SetToolTip(this.labelHU, resources.GetString("labelHU.ToolTip"));
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
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.panelSpecifiedIndices);
            this.groupBox5.Controls.Add(this.radioButtonRange);
            this.groupBox5.Controls.Add(this.panel2);
            this.groupBox5.Controls.Add(this.radioButtonSpecifiedIndices);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // panelSpecifiedIndices
            // 
            resources.ApplyResources(this.panelSpecifiedIndices, "panelSpecifiedIndices");
            this.panelSpecifiedIndices.Controls.Add(this.buttonRemoveIndex);
            this.panelSpecifiedIndices.Controls.Add(this.buttonAddIndex);
            this.panelSpecifiedIndices.Controls.Add(this.listBoxSpecifiedIndices);
            this.panelSpecifiedIndices.Controls.Add(this.checkBoxIncludingEquivalentPlanes);
            this.panelSpecifiedIndices.Name = "panelSpecifiedIndices";
            // 
            // buttonRemoveIndex
            // 
            resources.ApplyResources(this.buttonRemoveIndex, "buttonRemoveIndex");
            this.buttonRemoveIndex.Name = "buttonRemoveIndex";
            this.buttonRemoveIndex.UseVisualStyleBackColor = true;
            this.buttonRemoveIndex.Click += new System.EventHandler(this.buttonRemoveIndex_Click);
            // 
            // buttonAddIndex
            // 
            resources.ApplyResources(this.buttonAddIndex, "buttonAddIndex");
            this.buttonAddIndex.Name = "buttonAddIndex";
            this.buttonAddIndex.UseVisualStyleBackColor = true;
            this.buttonAddIndex.Click += new System.EventHandler(this.buttonAddIndex_Click);
            // 
            // listBoxSpecifiedIndices
            // 
            resources.ApplyResources(this.listBoxSpecifiedIndices, "listBoxSpecifiedIndices");
            this.listBoxSpecifiedIndices.FormattingEnabled = true;
            this.listBoxSpecifiedIndices.Name = "listBoxSpecifiedIndices";
            // 
            // checkBoxIncludingEquivalentPlanes
            // 
            resources.ApplyResources(this.checkBoxIncludingEquivalentPlanes, "checkBoxIncludingEquivalentPlanes");
            this.checkBoxIncludingEquivalentPlanes.Name = "checkBoxIncludingEquivalentPlanes";
            this.checkBoxIncludingEquivalentPlanes.UseVisualStyleBackColor = true;
            this.checkBoxIncludingEquivalentPlanes.CheckedChanged += new System.EventHandler(this.checkBoxIncludingEquivalentPlanes_CheckedChanged);
            // 
            // radioButtonRange
            // 
            resources.ApplyResources(this.radioButtonRange, "radioButtonRange");
            this.radioButtonRange.Checked = true;
            this.radioButtonRange.Name = "radioButtonRange";
            this.radioButtonRange.TabStop = true;
            this.radioButtonRange.UseVisualStyleBackColor = true;
            this.radioButtonRange.CheckedChanged += new System.EventHandler(this.radioButtonRange_CheckedChanged);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.numericUpDown1);
            this.panel2.Controls.Add(this.numericUpDown2);
            this.panel2.Controls.Add(this.labelPlusMinus2);
            this.panel2.Controls.Add(this.labelPlusMinus3);
            this.panel2.Controls.Add(this.numericUpDown3);
            this.panel2.Controls.Add(this.labelHU);
            this.panel2.Controls.Add(this.labelPlusMinus1);
            this.panel2.Name = "panel2";
            // 
            // radioButtonSpecifiedIndices
            // 
            resources.ApplyResources(this.radioButtonSpecifiedIndices, "radioButtonSpecifiedIndices");
            this.radioButtonSpecifiedIndices.Name = "radioButtonSpecifiedIndices";
            this.radioButtonSpecifiedIndices.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox5);
            this.panel3.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // scalablePictureBoxAdvanced1
            // 
            this.scalablePictureBoxAdvanced1.CopyButtonVisible = true;
            this.scalablePictureBoxAdvanced1.FixZoomAndCenter = false;
            resources.ApplyResources(this.scalablePictureBoxAdvanced1, "scalablePictureBoxAdvanced1");
            this.scalablePictureBoxAdvanced1.FrequencyGraphVisible = true;
            this.scalablePictureBoxAdvanced1.ImageFilter_DustAndScratches = false;
            this.scalablePictureBoxAdvanced1.ImageFilter_DustAndScratchesRadius = 1D;
            this.scalablePictureBoxAdvanced1.ImageFilter_DustAndScratchesThreshold = 3D;
            this.scalablePictureBoxAdvanced1.ImageFilter_DustAndScratchesVisible = true;
            this.scalablePictureBoxAdvanced1.ImageFilter_GaussianBlur = false;
            this.scalablePictureBoxAdvanced1.ImageFilter_GaussianBlurRadius = 1D;
            this.scalablePictureBoxAdvanced1.ImageFilter_GaussianBlurVisible = true;
            this.scalablePictureBoxAdvanced1.ImageFilterVisible = true;
            this.scalablePictureBoxAdvanced1.LogScaleBar = false;
            this.scalablePictureBoxAdvanced1.LowerIntensity = 0D;
            this.scalablePictureBoxAdvanced1.MaximumIntensity = 255D;
            this.scalablePictureBoxAdvanced1.MinimumIntensity = 0D;
            this.scalablePictureBoxAdvanced1.MousePositionLabelVisible = true;
            this.scalablePictureBoxAdvanced1.Name = "scalablePictureBoxAdvanced1";
            this.scalablePictureBoxAdvanced1.PictureSize = new System.Drawing.Size(410, -10973);
            this.scalablePictureBoxAdvanced1.ShowGradiaent = true;
            this.scalablePictureBoxAdvanced1.StatusLabel = " ";
            this.scalablePictureBoxAdvanced1.StatusProgress = 0D;
            this.scalablePictureBoxAdvanced1.StatusVisible = true;
            this.scalablePictureBoxAdvanced1.TrackBarVisible = true;
            this.scalablePictureBoxAdvanced1.UpperIntensity = 255D;
            this.scalablePictureBoxAdvanced1.VisibleGradient = true;
            // 
            // scalablePictureBoxAdvanced2
            // 
            this.scalablePictureBoxAdvanced2.CopyButtonVisible = true;
            this.scalablePictureBoxAdvanced2.FixZoomAndCenter = false;
            resources.ApplyResources(this.scalablePictureBoxAdvanced2, "scalablePictureBoxAdvanced2");
            this.scalablePictureBoxAdvanced2.FrequencyGraphVisible = true;
            this.scalablePictureBoxAdvanced2.ImageFilter_DustAndScratches = false;
            this.scalablePictureBoxAdvanced2.ImageFilter_DustAndScratchesRadius = 1D;
            this.scalablePictureBoxAdvanced2.ImageFilter_DustAndScratchesThreshold = 3D;
            this.scalablePictureBoxAdvanced2.ImageFilter_DustAndScratchesVisible = true;
            this.scalablePictureBoxAdvanced2.ImageFilter_GaussianBlur = false;
            this.scalablePictureBoxAdvanced2.ImageFilter_GaussianBlurRadius = 1D;
            this.scalablePictureBoxAdvanced2.ImageFilter_GaussianBlurVisible = true;
            this.scalablePictureBoxAdvanced2.ImageFilterVisible = true;
            this.scalablePictureBoxAdvanced2.LogScaleBar = false;
            this.scalablePictureBoxAdvanced2.LowerIntensity = 0D;
            this.scalablePictureBoxAdvanced2.MaximumIntensity = 255D;
            this.scalablePictureBoxAdvanced2.MinimumIntensity = 0D;
            this.scalablePictureBoxAdvanced2.MousePositionLabelVisible = true;
            this.scalablePictureBoxAdvanced2.Name = "scalablePictureBoxAdvanced2";
            this.scalablePictureBoxAdvanced2.PictureSize = new System.Drawing.Size(410, -10973);
            this.scalablePictureBoxAdvanced2.ShowGradiaent = true;
            this.scalablePictureBoxAdvanced2.StatusLabel = " ";
            this.scalablePictureBoxAdvanced2.StatusProgress = 0D;
            this.scalablePictureBoxAdvanced2.StatusVisible = true;
            this.scalablePictureBoxAdvanced2.TrackBarVisible = true;
            this.scalablePictureBoxAdvanced2.UpperIntensity = 255D;
            this.scalablePictureBoxAdvanced2.VisibleGradient = true;
            // 
            // graphicsBox
            // 
            resources.ApplyResources(this.graphicsBox, "graphicsBox");
            this.graphicsBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.graphicsBox.Name = "graphicsBox";
            this.graphicsBox.TabStop = false;
            this.graphicsBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.graphicsBox_MouseDown);
            this.graphicsBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.graphicsBox_MouseMove);
            this.graphicsBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.graphicsBox_MouseUp);
            this.graphicsBox.Resize += new System.EventHandler(this.formStereonet_Resize);
            // 
            // pageSetupDialog1
            // 
            this.pageSetupDialog1.Document = this.printDocument1;
            // 
            // printDialog1
            // 
            this.printDialog1.Document = this.printDocument1;
            this.printDialog1.UseEXDialog = true;
            // 
            // FormStereonet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.labelYpos);
            this.Controls.Add(this.labelXpos);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.graphicsBox);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormStereonet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStereonet_FormClosing);
            this.Load += new System.EventHandler(this.FormStereonet_Load);
            this.VisibleChanged += new System.EventHandler(this.FormStereonet_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormStereonet_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStrSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPointSize)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panelPlanes.ResumeLayout(false);
            this.panelPlanes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleH1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleH2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleL2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleL1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleK1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleK2)).EndInit();
            this.panelAxis.ResumeLayout(false);
            this.panelAxis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCircleW)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panelSpecifiedIndices.ResumeLayout(false);
            this.panelSpecifiedIndices.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.graphicsBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarStrSize;
        private System.Windows.Forms.TrackBar trackBarPointSize;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.RadioButton radioButtonAxes;
        private System.Windows.Forms.Label labelPlusMinus2;
        private System.Windows.Forms.Label labelPlusMinus1;
        private System.Windows.Forms.Label labelPlusMinus3;
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
        private System.Windows.Forms.Label labelUniqueAxis;
        private System.Windows.Forms.Label labelGeneralAxis;
        private System.Windows.Forms.Label labelUniquePlane;
        private System.Windows.Forms.Label labelGeneralPlane;
        private System.Windows.Forms.Label labelBackGround;
        private System.Windows.Forms.Label label90DegLine;
        private System.Windows.Forms.Label label10DegLine;
        private System.Windows.Forms.Label label1DegLine;
        private System.Windows.Forms.Label labelString;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
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
        public Crystallography.Controls.ColorControl colorControlString;
        public Crystallography.Controls.ColorControl colorControlUniqueAxis;
        public Crystallography.Controls.ColorControl colorControlUniquePlane;
        public Crystallography.Controls.ColorControl colorControlGeneralAxis;
        public Crystallography.Controls.ColorControl colorControlGeneralPlane;
        public Crystallography.Controls.ColorControl colorControlBackGround;
        public Crystallography.Controls.ColorControl colorControl90DegLine;
        public Crystallography.Controls.ColorControl colorControl10DegLine;
        public Crystallography.Controls.ColorControl colorControl1DegLine;
        public Crystallography.Controls.ColorControl colorControlGreatCircle;
        public Crystallography.Controls.ColorControl colorControlSmallCircle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.RadioButton radioButtonSchmidt;
        private System.Windows.Forms.RadioButton radioButtonWulff;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
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
        public Crystallography.Controls.NumericBox numericalTextBoxRxSpeed;
        public Crystallography.Controls.NumericBox numericalTextBoxRySpeed;
        public Crystallography.Controls.NumericBox numericalTextBoxRzSpeed;
        public Crystallography.Controls.NumericBox numericalTextBoxRyOscillation;
        public Crystallography.Controls.NumericBox numericalTextBoxRzOscillation;
        public Crystallography.Controls.NumericBox numericalTextBoxRyStep;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        public System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label labelHU;
        private System.Windows.Forms.RadioButton radioButtonRange;
        private System.Windows.Forms.RadioButton radioButtonSpecifiedIndices;
        private System.Windows.Forms.Panel panelSpecifiedIndices;
        private System.Windows.Forms.Button buttonRemoveIndex;
        private System.Windows.Forms.Button buttonAddIndex;
        private System.Windows.Forms.ListBox listBoxSpecifiedIndices;
        private System.Windows.Forms.CheckBox checkBoxIncludingEquivalentPlanes;
        public Crystallography.Controls.NumericBox numericalTextBoxTotalTime;
        public Crystallography.Controls.NumericBox numericalTextBoxAngularSpeed;
        public Crystallography.Controls.NumericBox numericalTextBoxRadialAngle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.RadioButton radioButtonRotationalScan;
        public System.Windows.Forms.RadioButton radioButtonZigzagScan;
        private System.Windows.Forms.Panel panel3;
        private Crystallography.Controls.ScalablePictureBoxAdvanced scalablePictureBoxAdvanced1;
        private Crystallography.Controls.ScalablePictureBoxAdvanced scalablePictureBoxAdvanced2;
        public ImagingSolution.Control.GraphicsBox graphicsBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem asBitmapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asMetafileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asMetafileToolStripMenuItem1;
    }
}