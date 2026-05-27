namespace Crystallography.Controls
{
    partial class FormSymmetryInformation
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
        // groupBox1 -> groupBoxPointGroup
        // groupBox2 -> groupBoxSpaceGroup
        // (260524Cl) tabPage1 -> tabPageGeometrics (Wiki クロップ名を Wyckoff/Conditions と揃えるため。Capture=true も付与)
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSymmetryInformation));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            panel4 = new System.Windows.Forms.Panel();
            graphicsBoxSymmetryElements = new GraphicsBox(components);
            flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            buttonCopyElements = new System.Windows.Forms.Button();
            panel3 = new System.Windows.Forms.Panel();
            graphicsBoxGeneralPositions = new GraphicsBox(components);
            flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            buttonCopyPositions = new System.Windows.Forms.Button();
            labelLaTex1 = new LabelLaTeX();
            numericBoxPositionA = new NumericBox();
            labelLaTex2 = new LabelLaTeX();
            numericBoxPositionB = new NumericBox();
            labelLaTex3 = new LabelLaTeX();
            numericBoxPositionC = new NumericBox();
            radioButtonBmp = new System.Windows.Forms.RadioButton();
            radioButtonEmf = new System.Windows.Forms.RadioButton();
            tabControl = new System.Windows.Forms.TabControl();
            tabPageGeometrics = new System.Windows.Forms.TabPage();
            numericBoxAnglePlanes = new NumericBox();
            textBoxZoneAxis = new System.Windows.Forms.TextBox();
            numericBoxAngleAxes = new NumericBox();
            flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            indexControlPlane2 = new IndexControl();
            numericBoxLengthPlane2 = new NumericBox();
            label40 = new System.Windows.Forms.Label();
            flowLayoutPanel14 = new System.Windows.Forms.FlowLayoutPanel();
            indexControlAxis2 = new IndexControl();
            numericBoxLengthAxis2 = new NumericBox();
            numericBoxAnglePlaneAxis1 = new NumericBox();
            flowLayoutPanel11 = new System.Windows.Forms.FlowLayoutPanel();
            indexControlAxis1 = new IndexControl();
            numericBoxLengthAxis1 = new NumericBox();
            numericBoxAnglePlaneAxis2 = new NumericBox();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            indexControlPlane1 = new IndexControl();
            numericBoxLengthPlane1 = new NumericBox();
            textBoxZonePlane = new System.Windows.Forms.TextBox();
            label42 = new System.Windows.Forms.Label();
            panel1 = new System.Windows.Forms.Panel();
            tabPageWyckoff = new System.Windows.Forms.TabPage();
            dataGridView1 = new DpiAwareDataGridView();
            columnMultiplicityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnWyckoffLetterDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnSiteSymmetryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnCoordinates1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnCoordinates2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnCoordinates3DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnCoordinates4DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            dataSet = new System.Data.DataSet();
            dataTableWyckoff = new System.Data.DataTable();
            dataColumn1 = new System.Data.DataColumn();
            dataColumn2 = new System.Data.DataColumn();
            dataColumn3 = new System.Data.DataColumn();
            dataColumn4 = new System.Data.DataColumn();
            dataColumn5 = new System.Data.DataColumn();
            dataColumn6 = new System.Data.DataColumn();
            dataColumn7 = new System.Data.DataColumn();
            dataTablePlanes = new System.Data.DataTable();
            dataColumnH = new System.Data.DataColumn();
            dataColumnK = new System.Data.DataColumn();
            dataColumnL = new System.Data.DataColumn();
            dataColumnMulti = new System.Data.DataColumn();
            dataColumnD = new System.Data.DataColumn();
            dataColumnCondition = new System.Data.DataColumn();
            dataColumnTwoTheta = new System.Data.DataColumn();
            dataColumnFreal = new System.Data.DataColumn();
            dataColumnFinverse = new System.Data.DataColumn();
            dataColumnF = new System.Data.DataColumn();
            dataColumn13 = new System.Data.DataColumn();
            dataColumnIntensity = new System.Data.DataColumn();
            tabPageConditions = new System.Windows.Forms.TabPage();
            flowLayoutPanelExtinctionRule = new System.Windows.Forms.FlowLayoutPanel();
            label49 = new System.Windows.Forms.Label();
            groupBoxSpaceGroup = new System.Windows.Forms.GroupBox();
            labelLaTexSG_Hall = new LabelLaTeX();
            labelLaTexSG_SF = new LabelLaTeX();
            labelLaTexHM_full = new LabelLaTeX();
            labelLaTexSG_HM = new LabelLaTeX();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            labelLaTexCS = new LabelLaTeX();
            label = new System.Windows.Forms.Label();
            groupBoxPointGroup = new System.Windows.Forms.GroupBox();
            labelLaTexPG_HM = new LabelLaTeX();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            labelLaTexPG_SF = new LabelLaTeX();
            bindingSourceScatteringFactor = new System.Windows.Forms.BindingSource(components);
            panel2 = new System.Windows.Forms.Panel();
            label4 = new System.Windows.Forms.Label();
            labelLaTexNumber = new LabelLaTeX();
            flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            label15 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            radioButtonDirectionA = new System.Windows.Forms.RadioButton();
            radioButtonDirectionB = new System.Windows.Forms.RadioButton();
            radioButtonDirectionC = new System.Windows.Forms.RadioButton();
            label12 = new System.Windows.Forms.Label();
            flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBoxSymmetryElements).BeginInit();
            flowLayoutPanel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBoxGeneralPositions).BeginInit();
            flowLayoutPanel3.SuspendLayout();
            tabControl.SuspendLayout();
            tabPageGeometrics.SuspendLayout();
            flowLayoutPanel6.SuspendLayout();
            flowLayoutPanel14.SuspendLayout();
            flowLayoutPanel11.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tabPageWyckoff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataTableWyckoff).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataTablePlanes).BeginInit();
            tabPageConditions.SuspendLayout();
            groupBoxSpaceGroup.SuspendLayout();
            groupBoxPointGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSourceScatteringFactor).BeginInit();
            panel2.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel5.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            captureExtender.SetCapture(tableLayoutPanel1, true);
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panel4, 0, 0);
            tableLayoutPanel1.Controls.Add(panel3, 1, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(4, 183);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1020, 561);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // panel4
            // 
            panel4.Controls.Add(graphicsBoxSymmetryElements);
            panel4.Controls.Add(flowLayoutPanel2);
            panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            panel4.Location = new System.Drawing.Point(3, 3);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(504, 555);
            panel4.TabIndex = 9;
            // 
            // graphicsBoxSymmetryElements
            // 
            graphicsBoxSymmetryElements.Dock = System.Windows.Forms.DockStyle.Fill;
            graphicsBoxSymmetryElements.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            graphicsBoxSymmetryElements.Fonts = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            graphicsBoxSymmetryElements.Location = new System.Drawing.Point(0, 25);
            graphicsBoxSymmetryElements.Name = "graphicsBoxSymmetryElements";
            graphicsBoxSymmetryElements.Size = new System.Drawing.Size(504, 530);
            graphicsBoxSymmetryElements.TabIndex = 0;
            graphicsBoxSymmetryElements.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(buttonCopyElements);
            flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new System.Drawing.Size(504, 25);
            flowLayoutPanel2.TabIndex = 2;
            // 
            // buttonCopyElements
            // 
            buttonCopyElements.AutoSize = true;
            buttonCopyElements.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonCopyElements.Font = new System.Drawing.Font("Segoe UI", 9F);
            buttonCopyElements.Location = new System.Drawing.Point(0, 0);
            buttonCopyElements.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            buttonCopyElements.Name = "buttonCopyElements";
            buttonCopyElements.Size = new System.Drawing.Size(45, 25);
            buttonCopyElements.TabIndex = 0;
            buttonCopyElements.Text = "Copy";
            buttonCopyElements.UseVisualStyleBackColor = true;
            buttonCopyElements.Click += buttonCopySymmetryElements_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(graphicsBoxGeneralPositions);
            panel3.Controls.Add(flowLayoutPanel3);
            panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            panel3.Location = new System.Drawing.Point(513, 3);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(504, 555);
            panel3.TabIndex = 8;
            // 
            // graphicsBoxGeneralPositions
            // 
            graphicsBoxGeneralPositions.Dock = System.Windows.Forms.DockStyle.Fill;
            graphicsBoxGeneralPositions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            graphicsBoxGeneralPositions.Fonts = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            graphicsBoxGeneralPositions.Location = new System.Drawing.Point(0, 25);
            graphicsBoxGeneralPositions.Name = "graphicsBoxGeneralPositions";
            graphicsBoxGeneralPositions.Size = new System.Drawing.Size(504, 530);
            graphicsBoxGeneralPositions.TabIndex = 0;
            graphicsBoxGeneralPositions.TabStop = false;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Controls.Add(buttonCopyPositions);
            flowLayoutPanel3.Controls.Add(labelLaTex1);
            flowLayoutPanel3.Controls.Add(numericBoxPositionA);
            flowLayoutPanel3.Controls.Add(labelLaTex2);
            flowLayoutPanel3.Controls.Add(numericBoxPositionB);
            flowLayoutPanel3.Controls.Add(labelLaTex3);
            flowLayoutPanel3.Controls.Add(numericBoxPositionC);
            flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new System.Drawing.Size(504, 25);
            flowLayoutPanel3.TabIndex = 3;
            // 
            // buttonCopyPositions
            // 
            buttonCopyPositions.AutoSize = true;
            buttonCopyPositions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonCopyPositions.Font = new System.Drawing.Font("Segoe UI", 9F);
            buttonCopyPositions.Location = new System.Drawing.Point(0, 0);
            buttonCopyPositions.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            buttonCopyPositions.Name = "buttonCopyPositions";
            buttonCopyPositions.Size = new System.Drawing.Size(45, 25);
            buttonCopyPositions.TabIndex = 0;
            buttonCopyPositions.Text = "Copy";
            buttonCopyPositions.UseVisualStyleBackColor = true;
            buttonCopyPositions.Click += buttonCopyGeneralPositions_Click;
            // 
            // labelLaTex1
            // 
            labelLaTex1.Font = new System.Drawing.Font("Segoe UI", 11F);
            labelLaTex1.Location = new System.Drawing.Point(55, 0);
            labelLaTex1.Margin = new System.Windows.Forms.Padding(0);
            labelLaTex1.Name = "labelLaTex1";
            labelLaTex1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            labelLaTex1.Size = new System.Drawing.Size(10, 25);
            labelLaTex1.TabIndex = 6;
            labelLaTex1.Text = "a";
            labelLaTex1.Thickness = 0.6D;
            // 
            // numericBoxPositionA
            // 
            numericBoxPositionA.BackColor = System.Drawing.Color.Transparent;
            numericBoxPositionA.DecimalPlaces = 2;
            numericBoxPositionA.FooterFont = new System.Drawing.Font("Segoe UI", 9F);
            numericBoxPositionA.FooterPadding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            numericBoxPositionA.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            numericBoxPositionA.HeaderPadding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            numericBoxPositionA.Location = new System.Drawing.Point(65, 0);
            numericBoxPositionA.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            numericBoxPositionA.Maximum = 1D;
            numericBoxPositionA.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxPositionA.Minimum = -1D;
            numericBoxPositionA.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxPositionA.Name = "numericBoxPositionA";
            numericBoxPositionA.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxPositionA.ShowUpDown = true;
            numericBoxPositionA.Size = new System.Drawing.Size(50, 25);
            numericBoxPositionA.TabIndex = 6;
            numericBoxPositionA.UpDown_Increment = 0.01D;
            numericBoxPositionA.ValueFontSize = 9F;
            numericBoxPositionA.ValueChanged += numericBoxPosition_ValueChanged;
            // 
            // labelLaTex2
            // 
            labelLaTex2.Font = new System.Drawing.Font("Segoe UI", 11F);
            labelLaTex2.Location = new System.Drawing.Point(119, 0);
            labelLaTex2.Margin = new System.Windows.Forms.Padding(0);
            labelLaTex2.Name = "labelLaTex2";
            labelLaTex2.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            labelLaTex2.Size = new System.Drawing.Size(10, 25);
            labelLaTex2.TabIndex = 6;
            labelLaTex2.Text = "b";
            labelLaTex2.Thickness = 0.6D;
            // 
            // numericBoxPositionB
            // 
            numericBoxPositionB.BackColor = System.Drawing.Color.Transparent;
            numericBoxPositionB.DecimalPlaces = 2;
            numericBoxPositionB.FooterFont = new System.Drawing.Font("Segoe UI", 9F);
            numericBoxPositionB.FooterPadding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            numericBoxPositionB.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            numericBoxPositionB.HeaderPadding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            numericBoxPositionB.Location = new System.Drawing.Point(129, 0);
            numericBoxPositionB.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            numericBoxPositionB.Maximum = 1D;
            numericBoxPositionB.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxPositionB.Minimum = -1D;
            numericBoxPositionB.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxPositionB.Name = "numericBoxPositionB";
            numericBoxPositionB.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxPositionB.ShowUpDown = true;
            numericBoxPositionB.Size = new System.Drawing.Size(50, 25);
            numericBoxPositionB.TabIndex = 6;
            numericBoxPositionB.UpDown_Increment = 0.01D;
            numericBoxPositionB.ValueFontSize = 9F;
            numericBoxPositionB.ValueChanged += numericBoxPosition_ValueChanged;
            // 
            // labelLaTex3
            // 
            labelLaTex3.Font = new System.Drawing.Font("Segoe UI", 11F);
            labelLaTex3.Location = new System.Drawing.Point(183, 0);
            labelLaTex3.Margin = new System.Windows.Forms.Padding(0);
            labelLaTex3.Name = "labelLaTex3";
            labelLaTex3.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            labelLaTex3.Size = new System.Drawing.Size(10, 25);
            labelLaTex3.TabIndex = 6;
            labelLaTex3.Text = "c";
            labelLaTex3.Thickness = 0.6D;
            // 
            // numericBoxPositionC
            // 
            numericBoxPositionC.BackColor = System.Drawing.Color.Transparent;
            numericBoxPositionC.DecimalPlaces = 2;
            numericBoxPositionC.FooterFont = new System.Drawing.Font("Segoe UI", 9F);
            numericBoxPositionC.FooterPadding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            numericBoxPositionC.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            numericBoxPositionC.HeaderPadding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            numericBoxPositionC.Location = new System.Drawing.Point(193, 0);
            numericBoxPositionC.Margin = new System.Windows.Forms.Padding(0);
            numericBoxPositionC.Maximum = 1D;
            numericBoxPositionC.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxPositionC.Minimum = -1D;
            numericBoxPositionC.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxPositionC.Name = "numericBoxPositionC";
            numericBoxPositionC.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxPositionC.ShowUpDown = true;
            numericBoxPositionC.Size = new System.Drawing.Size(50, 25);
            numericBoxPositionC.TabIndex = 6;
            numericBoxPositionC.UpDown_Increment = 0.01D;
            numericBoxPositionC.ValueFontSize = 9F;
            numericBoxPositionC.ValueChanged += numericBoxPosition_ValueChanged;
            // 
            // radioButtonBmp
            // 
            radioButtonBmp.AutoSize = true;
            radioButtonBmp.Font = new System.Drawing.Font("Segoe UI", 9F);
            radioButtonBmp.Location = new System.Drawing.Point(46, 0);
            radioButtonBmp.Margin = new System.Windows.Forms.Padding(0);
            radioButtonBmp.Name = "radioButtonBmp";
            radioButtonBmp.Size = new System.Drawing.Size(50, 19);
            radioButtonBmp.TabIndex = 1;
            radioButtonBmp.Text = "bmp";
            radioButtonBmp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            radioButtonBmp.UseVisualStyleBackColor = true;
            // 
            // radioButtonEmf
            // 
            radioButtonEmf.AutoSize = true;
            radioButtonEmf.Checked = true;
            radioButtonEmf.Font = new System.Drawing.Font("Segoe UI", 9F);
            radioButtonEmf.Location = new System.Drawing.Point(0, 0);
            radioButtonEmf.Margin = new System.Windows.Forms.Padding(0);
            radioButtonEmf.Name = "radioButtonEmf";
            radioButtonEmf.Size = new System.Drawing.Size(46, 19);
            radioButtonEmf.TabIndex = 1;
            radioButtonEmf.TabStop = true;
            radioButtonEmf.Text = "emf";
            radioButtonEmf.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            radioButtonEmf.UseVisualStyleBackColor = true;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPageGeometrics);
            tabControl.Controls.Add(tabPageWyckoff);
            tabControl.Controls.Add(tabPageConditions);
            tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            tabControl.Location = new System.Drawing.Point(337, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new System.Drawing.Size(681, 169);
            tabControl.TabIndex = 4;
            // 
            // tabPageGeometrics
            // 
            tabPageGeometrics.BackColor = System.Drawing.SystemColors.Control;
            captureExtender.SetCapture(tabPageGeometrics, true);
            tabPageGeometrics.Controls.Add(numericBoxAnglePlanes);
            tabPageGeometrics.Controls.Add(textBoxZoneAxis);
            tabPageGeometrics.Controls.Add(numericBoxAngleAxes);
            tabPageGeometrics.Controls.Add(flowLayoutPanel6);
            tabPageGeometrics.Controls.Add(label40);
            tabPageGeometrics.Controls.Add(flowLayoutPanel14);
            tabPageGeometrics.Controls.Add(numericBoxAnglePlaneAxis1);
            tabPageGeometrics.Controls.Add(flowLayoutPanel11);
            tabPageGeometrics.Controls.Add(numericBoxAnglePlaneAxis2);
            tabPageGeometrics.Controls.Add(flowLayoutPanel1);
            tabPageGeometrics.Controls.Add(textBoxZonePlane);
            tabPageGeometrics.Controls.Add(label42);
            tabPageGeometrics.Controls.Add(panel1);
            tabPageGeometrics.Location = new System.Drawing.Point(4, 24);
            tabPageGeometrics.Name = "tabPageGeometrics";
            tabPageGeometrics.Padding = new System.Windows.Forms.Padding(3);
            tabPageGeometrics.Size = new System.Drawing.Size(673, 141);
            tabPageGeometrics.TabIndex = 3;
            tabPageGeometrics.Text = "Geometrics Calculation";
            // 
            // numericBoxAnglePlanes
            // 
            numericBoxAnglePlanes.BackColor = System.Drawing.Color.Transparent;
            numericBoxAnglePlanes.DecimalPlaces = 4;
            numericBoxAnglePlanes.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxAnglePlanes.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxAnglePlanes.FooterText = "°";
            numericBoxAnglePlanes.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxAnglePlanes.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxAnglePlanes.Location = new System.Drawing.Point(10, 45);
            numericBoxAnglePlanes.Margin = new System.Windows.Forms.Padding(0);
            numericBoxAnglePlanes.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxAnglePlanes.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxAnglePlanes.Name = "numericBoxAnglePlanes";
            numericBoxAnglePlanes.ReadOnly = true;
            numericBoxAnglePlanes.Size = new System.Drawing.Size(72, 25);
            numericBoxAnglePlanes.TabIndex = 12;
            numericBoxAnglePlanes.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxAnglePlanes.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxZoneAxis
            // 
            textBoxZoneAxis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            textBoxZoneAxis.Location = new System.Drawing.Point(189, 111);
            textBoxZoneAxis.Name = "textBoxZoneAxis";
            textBoxZoneAxis.ReadOnly = true;
            textBoxZoneAxis.Size = new System.Drawing.Size(72, 23);
            textBoxZoneAxis.TabIndex = 18;
            textBoxZoneAxis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numericBoxAngleAxes
            // 
            numericBoxAngleAxes.BackColor = System.Drawing.Color.Transparent;
            numericBoxAngleAxes.DecimalPlaces = 4;
            numericBoxAngleAxes.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxAngleAxes.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxAngleAxes.FooterText = "°";
            numericBoxAngleAxes.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxAngleAxes.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxAngleAxes.Location = new System.Drawing.Point(590, 45);
            numericBoxAngleAxes.Margin = new System.Windows.Forms.Padding(0);
            numericBoxAngleAxes.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxAngleAxes.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxAngleAxes.Name = "numericBoxAngleAxes";
            numericBoxAngleAxes.ReadOnly = true;
            numericBoxAngleAxes.Size = new System.Drawing.Size(72, 25);
            numericBoxAngleAxes.TabIndex = 17;
            numericBoxAngleAxes.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxAngleAxes.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flowLayoutPanel6
            // 
            flowLayoutPanel6.AutoSize = true;
            flowLayoutPanel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel6.Controls.Add(indexControlPlane2);
            flowLayoutPanel6.Controls.Add(numericBoxLengthPlane2);
            flowLayoutPanel6.Location = new System.Drawing.Point(80, 66);
            flowLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel6.Name = "flowLayoutPanel6";
            flowLayoutPanel6.Size = new System.Drawing.Size(195, 41);
            flowLayoutPanel6.TabIndex = 5;
            // 
            // indexControlPlane2
            // 
            indexControlPlane2.AutoSize = true;
            indexControlPlane2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            indexControlPlane2.Location = new System.Drawing.Point(0, 0);
            indexControlPlane2.Margin = new System.Windows.Forms.Padding(0);
            indexControlPlane2.Name = "indexControlPlane2";
            indexControlPlane2.Size = new System.Drawing.Size(128, 41);
            indexControlPlane2.SubScript = "2";
            indexControlPlane2.TabIndex = 14;
            indexControlPlane2.Values = ((int, int, int))resources.GetObject("indexControlPlane2.Values");
            indexControlPlane2.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBoxLengthPlane2
            // 
            numericBoxLengthPlane2.BackColor = System.Drawing.Color.Transparent;
            numericBoxLengthPlane2.DecimalPlaces = 4;
            numericBoxLengthPlane2.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxLengthPlane2.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxLengthPlane2.FooterText = "Å";
            numericBoxLengthPlane2.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxLengthPlane2.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxLengthPlane2.Location = new System.Drawing.Point(128, 13);
            numericBoxLengthPlane2.Margin = new System.Windows.Forms.Padding(0, 13, 0, 0);
            numericBoxLengthPlane2.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxLengthPlane2.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxLengthPlane2.Name = "numericBoxLengthPlane2";
            numericBoxLengthPlane2.ReadOnly = true;
            numericBoxLengthPlane2.Size = new System.Drawing.Size(67, 25);
            numericBoxLengthPlane2.TabIndex = 14;
            numericBoxLengthPlane2.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxLengthPlane2.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label40
            // 
            label40.Font = new System.Drawing.Font("Segoe UI", 9F);
            label40.Location = new System.Drawing.Point(10, 114);
            label40.Name = "label40";
            label40.Size = new System.Drawing.Size(176, 16);
            label40.TabIndex = 6;
            label40.Text = "The axis normal to both planes";
            // 
            // flowLayoutPanel14
            // 
            flowLayoutPanel14.AutoSize = true;
            flowLayoutPanel14.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel14.Controls.Add(indexControlAxis2);
            flowLayoutPanel14.Controls.Add(numericBoxLengthAxis2);
            flowLayoutPanel14.Location = new System.Drawing.Point(400, 66);
            flowLayoutPanel14.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel14.Name = "flowLayoutPanel14";
            flowLayoutPanel14.Size = new System.Drawing.Size(195, 41);
            flowLayoutPanel14.TabIndex = 5;
            // 
            // indexControlAxis2
            // 
            indexControlAxis2.AutoSize = true;
            indexControlAxis2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            indexControlAxis2.Location = new System.Drawing.Point(0, 0);
            indexControlAxis2.Margin = new System.Windows.Forms.Padding(0);
            indexControlAxis2.Mode = IndexControl.ModeEnum.Axis;
            indexControlAxis2.Name = "indexControlAxis2";
            indexControlAxis2.Size = new System.Drawing.Size(128, 41);
            indexControlAxis2.SubScript = "2";
            indexControlAxis2.TabIndex = 14;
            indexControlAxis2.Values = ((int, int, int))resources.GetObject("indexControlAxis2.Values");
            indexControlAxis2.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBoxLengthAxis2
            // 
            numericBoxLengthAxis2.BackColor = System.Drawing.Color.Transparent;
            numericBoxLengthAxis2.DecimalPlaces = 4;
            numericBoxLengthAxis2.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxLengthAxis2.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxLengthAxis2.FooterText = "Å";
            numericBoxLengthAxis2.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxLengthAxis2.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxLengthAxis2.Location = new System.Drawing.Point(128, 13);
            numericBoxLengthAxis2.Margin = new System.Windows.Forms.Padding(0, 13, 0, 0);
            numericBoxLengthAxis2.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxLengthAxis2.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxLengthAxis2.Name = "numericBoxLengthAxis2";
            numericBoxLengthAxis2.ReadOnly = true;
            numericBoxLengthAxis2.Size = new System.Drawing.Size(67, 25);
            numericBoxLengthAxis2.TabIndex = 4;
            numericBoxLengthAxis2.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxLengthAxis2.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numericBoxAnglePlaneAxis1
            // 
            numericBoxAnglePlaneAxis1.BackColor = System.Drawing.Color.Transparent;
            numericBoxAnglePlaneAxis1.DecimalPlaces = 4;
            numericBoxAnglePlaneAxis1.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxAnglePlaneAxis1.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxAnglePlaneAxis1.FooterText = "°";
            numericBoxAnglePlaneAxis1.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxAnglePlaneAxis1.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxAnglePlaneAxis1.Location = new System.Drawing.Point(318, 18);
            numericBoxAnglePlaneAxis1.Margin = new System.Windows.Forms.Padding(0);
            numericBoxAnglePlaneAxis1.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxAnglePlaneAxis1.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxAnglePlaneAxis1.Name = "numericBoxAnglePlaneAxis1";
            numericBoxAnglePlaneAxis1.ReadOnly = true;
            numericBoxAnglePlaneAxis1.Size = new System.Drawing.Size(72, 25);
            numericBoxAnglePlaneAxis1.TabIndex = 15;
            numericBoxAnglePlaneAxis1.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxAnglePlaneAxis1.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flowLayoutPanel11
            // 
            flowLayoutPanel11.AutoSize = true;
            flowLayoutPanel11.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel11.Controls.Add(indexControlAxis1);
            flowLayoutPanel11.Controls.Add(numericBoxLengthAxis1);
            flowLayoutPanel11.Location = new System.Drawing.Point(399, 5);
            flowLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel11.Name = "flowLayoutPanel11";
            flowLayoutPanel11.Size = new System.Drawing.Size(195, 41);
            flowLayoutPanel11.TabIndex = 5;
            // 
            // indexControlAxis1
            // 
            indexControlAxis1.AutoSize = true;
            indexControlAxis1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            indexControlAxis1.Location = new System.Drawing.Point(0, 0);
            indexControlAxis1.Margin = new System.Windows.Forms.Padding(0);
            indexControlAxis1.Mode = IndexControl.ModeEnum.Axis;
            indexControlAxis1.Name = "indexControlAxis1";
            indexControlAxis1.Size = new System.Drawing.Size(128, 41);
            indexControlAxis1.SubScript = "1";
            indexControlAxis1.TabIndex = 14;
            indexControlAxis1.Values = ((int, int, int))resources.GetObject("indexControlAxis1.Values");
            indexControlAxis1.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBoxLengthAxis1
            // 
            numericBoxLengthAxis1.BackColor = System.Drawing.Color.Transparent;
            numericBoxLengthAxis1.DecimalPlaces = 4;
            numericBoxLengthAxis1.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxLengthAxis1.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxLengthAxis1.FooterText = "Å";
            numericBoxLengthAxis1.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxLengthAxis1.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxLengthAxis1.Location = new System.Drawing.Point(128, 13);
            numericBoxLengthAxis1.Margin = new System.Windows.Forms.Padding(0, 13, 0, 0);
            numericBoxLengthAxis1.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxLengthAxis1.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxLengthAxis1.Name = "numericBoxLengthAxis1";
            numericBoxLengthAxis1.ReadOnly = true;
            numericBoxLengthAxis1.Size = new System.Drawing.Size(67, 25);
            numericBoxLengthAxis1.TabIndex = 4;
            numericBoxLengthAxis1.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxLengthAxis1.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numericBoxAnglePlaneAxis2
            // 
            numericBoxAnglePlaneAxis2.BackColor = System.Drawing.Color.Transparent;
            numericBoxAnglePlaneAxis2.DecimalPlaces = 4;
            numericBoxAnglePlaneAxis2.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxAnglePlaneAxis2.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxAnglePlaneAxis2.FooterText = "°";
            numericBoxAnglePlaneAxis2.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxAnglePlaneAxis2.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxAnglePlaneAxis2.Location = new System.Drawing.Point(318, 79);
            numericBoxAnglePlaneAxis2.Margin = new System.Windows.Forms.Padding(0);
            numericBoxAnglePlaneAxis2.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxAnglePlaneAxis2.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxAnglePlaneAxis2.Name = "numericBoxAnglePlaneAxis2";
            numericBoxAnglePlaneAxis2.ReadOnly = true;
            numericBoxAnglePlaneAxis2.Size = new System.Drawing.Size(72, 25);
            numericBoxAnglePlaneAxis2.TabIndex = 16;
            numericBoxAnglePlaneAxis2.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxAnglePlaneAxis2.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            flowLayoutPanel1.Controls.Add(indexControlPlane1);
            flowLayoutPanel1.Controls.Add(numericBoxLengthPlane1);
            flowLayoutPanel1.Location = new System.Drawing.Point(80, 5);
            flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(195, 41);
            flowLayoutPanel1.TabIndex = 5;
            // 
            // indexControlPlane1
            // 
            indexControlPlane1.AutoSize = true;
            indexControlPlane1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            indexControlPlane1.Location = new System.Drawing.Point(0, 0);
            indexControlPlane1.Margin = new System.Windows.Forms.Padding(0);
            indexControlPlane1.Name = "indexControlPlane1";
            indexControlPlane1.Size = new System.Drawing.Size(128, 41);
            indexControlPlane1.SubScript = "1";
            indexControlPlane1.TabIndex = 14;
            indexControlPlane1.Values = ((int, int, int))resources.GetObject("indexControlPlane1.Values");
            indexControlPlane1.ValueChanged += numericBox_ValueChanged;
            // 
            // numericBoxLengthPlane1
            // 
            numericBoxLengthPlane1.BackColor = System.Drawing.Color.Transparent;
            numericBoxLengthPlane1.DecimalPlaces = 4;
            numericBoxLengthPlane1.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxLengthPlane1.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxLengthPlane1.FooterText = "Å";
            numericBoxLengthPlane1.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxLengthPlane1.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxLengthPlane1.Location = new System.Drawing.Point(128, 13);
            numericBoxLengthPlane1.Margin = new System.Windows.Forms.Padding(0, 13, 0, 0);
            numericBoxLengthPlane1.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxLengthPlane1.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxLengthPlane1.Name = "numericBoxLengthPlane1";
            numericBoxLengthPlane1.ReadOnly = true;
            numericBoxLengthPlane1.Size = new System.Drawing.Size(67, 25);
            numericBoxLengthPlane1.TabIndex = 13;
            numericBoxLengthPlane1.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxLengthPlane1.ValueTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxZonePlane
            // 
            textBoxZonePlane.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            textBoxZonePlane.Location = new System.Drawing.Point(544, 111);
            textBoxZonePlane.Name = "textBoxZonePlane";
            textBoxZonePlane.ReadOnly = true;
            textBoxZonePlane.Size = new System.Drawing.Size(72, 23);
            textBoxZonePlane.TabIndex = 19;
            textBoxZonePlane.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label42
            // 
            label42.Font = new System.Drawing.Font("Segoe UI", 9F);
            label42.Location = new System.Drawing.Point(363, 114);
            label42.Name = "label42";
            label42.Size = new System.Drawing.Size(175, 16);
            label42.TabIndex = 6;
            label42.Text = "The plane normal to both axes";
            // 
            // panel1
            // 
            panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            panel1.Location = new System.Drawing.Point(40, 31);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(591, 63);
            panel1.TabIndex = 7;
            // 
            // tabPageWyckoff
            // 
            captureExtender.SetCapture(tabPageWyckoff, true);
            tabPageWyckoff.Controls.Add(dataGridView1);
            tabPageWyckoff.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            tabPageWyckoff.Location = new System.Drawing.Point(4, 24);
            tabPageWyckoff.Name = "tabPageWyckoff";
            tabPageWyckoff.Size = new System.Drawing.Size(673, 141);
            tabPageWyckoff.TabIndex = 2;
            tabPageWyckoff.Text = "Wyckoff Positions";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { columnMultiplicityDataGridViewTextBoxColumn, columnWyckoffLetterDataGridViewTextBoxColumn, columnSiteSymmetryDataGridViewTextBoxColumn, columnCoordinates1DataGridViewTextBoxColumn, columnCoordinates2DataGridViewTextBoxColumn, columnCoordinates3DataGridViewTextBoxColumn, columnCoordinates4DataGridViewTextBoxColumn });
            dataGridView1.DataMember = "TableWyckoff";
            dataGridView1.DataSource = dataSet;
            dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView1.Location = new System.Drawing.Point(0, 0);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new System.Drawing.Size(673, 141);
            dataGridView1.TabIndex = 0;
            // 
            // columnMultiplicityDataGridViewTextBoxColumn
            // 
            columnMultiplicityDataGridViewTextBoxColumn.DataPropertyName = "ColumnMultiplicity";
            columnMultiplicityDataGridViewTextBoxColumn.HeaderText = "Mult.";
            columnMultiplicityDataGridViewTextBoxColumn.Name = "columnMultiplicityDataGridViewTextBoxColumn";
            columnMultiplicityDataGridViewTextBoxColumn.ReadOnly = true;
            columnMultiplicityDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            columnMultiplicityDataGridViewTextBoxColumn.Width = 40;
            // 
            // columnWyckoffLetterDataGridViewTextBoxColumn
            // 
            columnWyckoffLetterDataGridViewTextBoxColumn.DataPropertyName = "ColumnWyckoffLetter";
            columnWyckoffLetterDataGridViewTextBoxColumn.HeaderText = "Wyck. Let.";
            columnWyckoffLetterDataGridViewTextBoxColumn.Name = "columnWyckoffLetterDataGridViewTextBoxColumn";
            columnWyckoffLetterDataGridViewTextBoxColumn.ReadOnly = true;
            columnWyckoffLetterDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            columnWyckoffLetterDataGridViewTextBoxColumn.Width = 80;
            // 
            // columnSiteSymmetryDataGridViewTextBoxColumn
            // 
            columnSiteSymmetryDataGridViewTextBoxColumn.DataPropertyName = "ColumnSiteSymmetry";
            columnSiteSymmetryDataGridViewTextBoxColumn.HeaderText = "Site Sym.";
            columnSiteSymmetryDataGridViewTextBoxColumn.Name = "columnSiteSymmetryDataGridViewTextBoxColumn";
            columnSiteSymmetryDataGridViewTextBoxColumn.ReadOnly = true;
            columnSiteSymmetryDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            columnSiteSymmetryDataGridViewTextBoxColumn.Width = 80;
            // 
            // columnCoordinates1DataGridViewTextBoxColumn
            // 
            columnCoordinates1DataGridViewTextBoxColumn.DataPropertyName = "ColumnCoordinates1";
            columnCoordinates1DataGridViewTextBoxColumn.HeaderText = "Coordinates";
            columnCoordinates1DataGridViewTextBoxColumn.Name = "columnCoordinates1DataGridViewTextBoxColumn";
            columnCoordinates1DataGridViewTextBoxColumn.ReadOnly = true;
            columnCoordinates1DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnCoordinates2DataGridViewTextBoxColumn
            // 
            columnCoordinates2DataGridViewTextBoxColumn.DataPropertyName = "ColumnCoordinates2";
            columnCoordinates2DataGridViewTextBoxColumn.HeaderText = "";
            columnCoordinates2DataGridViewTextBoxColumn.Name = "columnCoordinates2DataGridViewTextBoxColumn";
            columnCoordinates2DataGridViewTextBoxColumn.ReadOnly = true;
            columnCoordinates2DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnCoordinates3DataGridViewTextBoxColumn
            // 
            columnCoordinates3DataGridViewTextBoxColumn.DataPropertyName = "ColumnCoordinates3";
            columnCoordinates3DataGridViewTextBoxColumn.HeaderText = "";
            columnCoordinates3DataGridViewTextBoxColumn.Name = "columnCoordinates3DataGridViewTextBoxColumn";
            columnCoordinates3DataGridViewTextBoxColumn.ReadOnly = true;
            columnCoordinates3DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // columnCoordinates4DataGridViewTextBoxColumn
            // 
            columnCoordinates4DataGridViewTextBoxColumn.DataPropertyName = "ColumnCoordinates4";
            columnCoordinates4DataGridViewTextBoxColumn.HeaderText = "";
            columnCoordinates4DataGridViewTextBoxColumn.Name = "columnCoordinates4DataGridViewTextBoxColumn";
            columnCoordinates4DataGridViewTextBoxColumn.ReadOnly = true;
            columnCoordinates4DataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataSet
            // 
            dataSet.DataSetName = "NewDataSet";
            dataSet.Tables.AddRange(new System.Data.DataTable[] { dataTableWyckoff, dataTablePlanes });
            // 
            // dataTableWyckoff
            // 
            dataTableWyckoff.Columns.AddRange(new System.Data.DataColumn[] { dataColumn1, dataColumn2, dataColumn3, dataColumn4, dataColumn5, dataColumn6, dataColumn7 });
            dataTableWyckoff.TableName = "TableWyckoff";
            // 
            // dataColumn1
            // 
            dataColumn1.ColumnName = "ColumnMultiplicity";
            // 
            // dataColumn2
            // 
            dataColumn2.ColumnName = "ColumnWyckoffLetter";
            // 
            // dataColumn3
            // 
            dataColumn3.ColumnName = "ColumnSiteSymmetry";
            // 
            // dataColumn4
            // 
            dataColumn4.ColumnName = "ColumnCoordinates1";
            // 
            // dataColumn5
            // 
            dataColumn5.ColumnName = "ColumnCoordinates2";
            // 
            // dataColumn6
            // 
            dataColumn6.ColumnName = "ColumnCoordinates3";
            // 
            // dataColumn7
            // 
            dataColumn7.ColumnName = "ColumnCoordinates4";
            // 
            // dataTablePlanes
            // 
            dataTablePlanes.Columns.AddRange(new System.Data.DataColumn[] { dataColumnH, dataColumnK, dataColumnL, dataColumnMulti, dataColumnD, dataColumnCondition, dataColumnTwoTheta, dataColumnFreal, dataColumnFinverse, dataColumnF, dataColumn13, dataColumnIntensity });
            dataTablePlanes.TableName = "TablePlanes";
            // 
            // dataColumnH
            // 
            dataColumnH.ColumnName = "ColumnH";
            // 
            // dataColumnK
            // 
            dataColumnK.ColumnName = "ColumnK";
            // 
            // dataColumnL
            // 
            dataColumnL.ColumnName = "ColumnL";
            // 
            // dataColumnMulti
            // 
            dataColumnMulti.ColumnName = "ColumnMulti";
            // 
            // dataColumnD
            // 
            dataColumnD.ColumnName = "ColumnD";
            // 
            // dataColumnCondition
            // 
            dataColumnCondition.ColumnName = "ColumnCondition";
            // 
            // dataColumnTwoTheta
            // 
            dataColumnTwoTheta.ColumnName = "ColumnTwoTheta";
            // 
            // dataColumnFreal
            // 
            dataColumnFreal.ColumnName = "ColumnFReal";
            // 
            // dataColumnFinverse
            // 
            dataColumnFinverse.ColumnName = "ColumnFInverse";
            // 
            // dataColumnF
            // 
            dataColumnF.ColumnName = "ColumnF";
            // 
            // dataColumn13
            // 
            dataColumn13.ColumnName = "ColumnF2";
            // 
            // dataColumnIntensity
            // 
            dataColumnIntensity.ColumnName = "ColumnIntensity";
            // 
            // tabPageConditions
            // 
            captureExtender.SetCapture(tabPageConditions, true);
            tabPageConditions.Controls.Add(flowLayoutPanelExtinctionRule);
            tabPageConditions.Controls.Add(label49);
            tabPageConditions.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            tabPageConditions.Location = new System.Drawing.Point(4, 24);
            tabPageConditions.Name = "tabPageConditions";
            tabPageConditions.Size = new System.Drawing.Size(673, 141);
            tabPageConditions.TabIndex = 1;
            tabPageConditions.Text = "Conditions";
            // 
            // flowLayoutPanelExtinctionRule
            // 
            flowLayoutPanelExtinctionRule.AutoScroll = true;
            flowLayoutPanelExtinctionRule.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanelExtinctionRule.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowLayoutPanelExtinctionRule.Location = new System.Drawing.Point(0, 21);
            flowLayoutPanelExtinctionRule.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanelExtinctionRule.Name = "flowLayoutPanelExtinctionRule";
            flowLayoutPanelExtinctionRule.Size = new System.Drawing.Size(673, 120);
            flowLayoutPanelExtinctionRule.TabIndex = 6;
            flowLayoutPanelExtinctionRule.WrapContents = false;
            // 
            // label49
            // 
            label49.AutoSize = true;
            label49.Dock = System.Windows.Forms.DockStyle.Top;
            label49.Font = new System.Drawing.Font("Segoe UI", 9F);
            label49.Location = new System.Drawing.Point(0, 0);
            label49.Name = "label49";
            label49.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            label49.Size = new System.Drawing.Size(213, 21);
            label49.TabIndex = 7;
            label49.Text = "Conditions limiting possible reflections";
            // 
            // groupBoxSpaceGroup
            // 
            groupBoxSpaceGroup.Controls.Add(labelLaTexSG_Hall);
            groupBoxSpaceGroup.Controls.Add(labelLaTexSG_SF);
            groupBoxSpaceGroup.Controls.Add(labelLaTexHM_full);
            groupBoxSpaceGroup.Controls.Add(labelLaTexSG_HM);
            groupBoxSpaceGroup.Controls.Add(label8);
            groupBoxSpaceGroup.Controls.Add(label9);
            groupBoxSpaceGroup.Controls.Add(label5);
            groupBoxSpaceGroup.Controls.Add(label6);
            groupBoxSpaceGroup.Font = new System.Drawing.Font("Segoe UI", 9F);
            groupBoxSpaceGroup.Location = new System.Drawing.Point(3, 77);
            groupBoxSpaceGroup.Name = "groupBoxSpaceGroup";
            groupBoxSpaceGroup.Size = new System.Drawing.Size(328, 95);
            groupBoxSpaceGroup.TabIndex = 5;
            groupBoxSpaceGroup.TabStop = false;
            groupBoxSpaceGroup.Text = "Space Group";
            // 
            // labelLaTexSG_Hall
            // 
            labelLaTexSG_Hall.Font = new System.Drawing.Font("Segoe UI", 12F);
            labelLaTexSG_Hall.Location = new System.Drawing.Point(209, 69);
            labelLaTexSG_Hall.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexSG_Hall.Name = "labelLaTexSG_Hall";
            labelLaTexSG_Hall.Size = new System.Drawing.Size(111, 22);
            labelLaTexSG_Hall.TabIndex = 6;
            labelLaTexSG_Hall.Text = "test";
            labelLaTexSG_Hall.Thickness = 0.6D;
            // 
            // labelLaTexSG_SF
            // 
            labelLaTexSG_SF.Font = new System.Drawing.Font("Segoe UI", 12F);
            labelLaTexSG_SF.Location = new System.Drawing.Point(80, 69);
            labelLaTexSG_SF.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexSG_SF.Name = "labelLaTexSG_SF";
            labelLaTexSG_SF.Size = new System.Drawing.Size(40, 22);
            labelLaTexSG_SF.TabIndex = 6;
            labelLaTexSG_SF.Text = "test";
            labelLaTexSG_SF.Thickness = 0.6D;
            // 
            // labelLaTexHM_full
            // 
            labelLaTexHM_full.Font = new System.Drawing.Font("Segoe UI", 12F);
            labelLaTexHM_full.Location = new System.Drawing.Point(125, 43);
            labelLaTexHM_full.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexHM_full.Name = "labelLaTexHM_full";
            labelLaTexHM_full.Size = new System.Drawing.Size(195, 22);
            labelLaTexHM_full.TabIndex = 6;
            labelLaTexHM_full.Text = "\\alpha \\beta";
            labelLaTexHM_full.Thickness = 0.6D;
            // 
            // labelLaTexSG_HM
            // 
            labelLaTexSG_HM.Font = new System.Drawing.Font("Segoe UI", 12F);
            labelLaTexSG_HM.Location = new System.Drawing.Point(125, 17);
            labelLaTexSG_HM.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexSG_HM.Name = "labelLaTexSG_HM";
            labelLaTexSG_HM.Size = new System.Drawing.Size(195, 22);
            labelLaTexSG_HM.TabIndex = 6;
            labelLaTexSG_HM.Text = "F\\, m\\, \\bar{3}\\, m  {}_1";
            labelLaTexSG_HM.Thickness = 0.6D;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            label8.Location = new System.Drawing.Point(12, 72);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(64, 15);
            label8.TabIndex = 1;
            label8.Text = "SF symbol:";
            label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Segoe UI", 9F);
            label9.Location = new System.Drawing.Point(134, 71);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(73, 15);
            label9.TabIndex = 1;
            label9.Text = "Hall symbol:";
            label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            label5.Location = new System.Drawing.Point(12, 20);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(110, 15);
            label5.TabIndex = 1;
            label5.Text = "HM symbol (short):";
            label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            label6.Location = new System.Drawing.Point(21, 46);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(100, 15);
            label6.TabIndex = 1;
            label6.Text = "HM symbol (full):";
            label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelLaTexCS
            // 
            labelLaTexCS.Font = new System.Drawing.Font("Segoe UI", 11F);
            labelLaTexCS.Location = new System.Drawing.Point(223, 3);
            labelLaTexCS.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexCS.Name = "labelLaTexCS";
            labelLaTexCS.Size = new System.Drawing.Size(86, 20);
            labelLaTexCS.TabIndex = 6;
            labelLaTexCS.Text = "test";
            labelLaTexCS.Thickness = 0.6D;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Segoe UI", 9F);
            label.Location = new System.Drawing.Point(135, 3);
            label.Name = "label";
            label.Size = new System.Drawing.Size(87, 15);
            label.TabIndex = 1;
            label.Text = "Crystal System:";
            label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBoxPointGroup
            // 
            groupBoxPointGroup.Controls.Add(labelLaTexPG_HM);
            groupBoxPointGroup.Controls.Add(label10);
            groupBoxPointGroup.Controls.Add(label11);
            groupBoxPointGroup.Controls.Add(labelLaTexPG_SF);
            groupBoxPointGroup.Font = new System.Drawing.Font("Segoe UI", 9F);
            groupBoxPointGroup.Location = new System.Drawing.Point(3, 25);
            groupBoxPointGroup.Name = "groupBoxPointGroup";
            groupBoxPointGroup.Size = new System.Drawing.Size(328, 46);
            groupBoxPointGroup.TabIndex = 4;
            groupBoxPointGroup.TabStop = false;
            groupBoxPointGroup.Text = "Point Group";
            // 
            // labelLaTexPG_HM
            // 
            labelLaTexPG_HM.Font = new System.Drawing.Font("Segoe UI", 12F);
            labelLaTexPG_HM.Location = new System.Drawing.Point(97, 22);
            labelLaTexPG_HM.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexPG_HM.Name = "labelLaTexPG_HM";
            labelLaTexPG_HM.Size = new System.Drawing.Size(74, 22);
            labelLaTexPG_HM.TabIndex = 6;
            labelLaTexPG_HM.Text = "test";
            labelLaTexPG_HM.Thickness = 0.6D;
            // 
            // label10
            // 
            label10.Font = new System.Drawing.Font("Segoe UI", 9F);
            label10.Location = new System.Drawing.Point(4, 24);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(83, 15);
            label10.TabIndex = 1;
            label10.Text = "HM symbol:";
            label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Segoe UI", 9F);
            label11.Location = new System.Drawing.Point(183, 24);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(64, 15);
            label11.TabIndex = 1;
            label11.Text = "SF symbol:";
            label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelLaTexPG_SF
            // 
            labelLaTexPG_SF.Font = new System.Drawing.Font("Segoe UI", 12F);
            labelLaTexPG_SF.Location = new System.Drawing.Point(256, 22);
            labelLaTexPG_SF.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexPG_SF.Name = "labelLaTexPG_SF";
            labelLaTexPG_SF.Size = new System.Drawing.Size(51, 22);
            labelLaTexPG_SF.TabIndex = 6;
            labelLaTexPG_SF.Text = "test";
            labelLaTexPG_SF.Thickness = 0.6D;
            // 
            // bindingSourceScatteringFactor
            // 
            bindingSourceScatteringFactor.DataMember = "TablePlanes";
            bindingSourceScatteringFactor.DataSource = dataSet;
            // 
            // panel2
            // 
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label);
            panel2.Controls.Add(tabControl);
            panel2.Controls.Add(groupBoxSpaceGroup);
            panel2.Controls.Add(labelLaTexNumber);
            panel2.Controls.Add(groupBoxPointGroup);
            panel2.Controls.Add(labelLaTexCS);
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point(4, 4);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(1020, 179);
            panel2.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            label4.Location = new System.Drawing.Point(8, 3);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(54, 15);
            label4.TabIndex = 1;
            label4.Text = "Number:";
            label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelLaTexNumber
            // 
            labelLaTexNumber.Font = new System.Drawing.Font("Segoe UI", 11F);
            labelLaTexNumber.Location = new System.Drawing.Point(64, 3);
            labelLaTexNumber.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexNumber.Name = "labelLaTexNumber";
            labelLaTexNumber.Size = new System.Drawing.Size(66, 20);
            labelLaTexNumber.TabIndex = 6;
            labelLaTexNumber.Text = "test";
            labelLaTexNumber.Thickness = 0.6D;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel4.Controls.Add(label15);
            flowLayoutPanel4.Controls.Add(label16);
            flowLayoutPanel4.Controls.Add(radioButtonDirectionA);
            flowLayoutPanel4.Controls.Add(radioButtonDirectionB);
            flowLayoutPanel4.Controls.Add(radioButtonDirectionC);
            flowLayoutPanel4.Controls.Add(label12);
            flowLayoutPanel4.Controls.Add(flowLayoutPanel5);
            flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            flowLayoutPanel4.Location = new System.Drawing.Point(4, 744);
            flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new System.Drawing.Size(1020, 19);
            flowLayoutPanel4.TabIndex = 8;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label15.Location = new System.Drawing.Point(0, 2);
            label15.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(57, 17);
            label15.TabIndex = 6;
            label15.Text = "Options";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("Segoe UI", 9F);
            label16.Location = new System.Drawing.Point(72, 2);
            label16.Margin = new System.Windows.Forms.Padding(15, 2, 0, 0);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(55, 15);
            label16.TabIndex = 6;
            label16.Text = "Direction";
            // 
            // radioButtonDirectionA
            // 
            radioButtonDirectionA.AutoSize = true;
            radioButtonDirectionA.Font = new System.Drawing.Font("Segoe UI", 9F);
            radioButtonDirectionA.Location = new System.Drawing.Point(127, 0);
            radioButtonDirectionA.Margin = new System.Windows.Forms.Padding(0);
            radioButtonDirectionA.Name = "radioButtonDirectionA";
            radioButtonDirectionA.Size = new System.Drawing.Size(31, 19);
            radioButtonDirectionA.TabIndex = 1;
            radioButtonDirectionA.Text = "a";
            radioButtonDirectionA.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            radioButtonDirectionA.UseVisualStyleBackColor = true;
            radioButtonDirectionA.CheckedChanged += radioButtonDirection_CheckedChanged;
            // 
            // radioButtonDirectionB
            // 
            radioButtonDirectionB.AutoSize = true;
            radioButtonDirectionB.Font = new System.Drawing.Font("Segoe UI", 9F);
            radioButtonDirectionB.Location = new System.Drawing.Point(158, 0);
            radioButtonDirectionB.Margin = new System.Windows.Forms.Padding(0);
            radioButtonDirectionB.Name = "radioButtonDirectionB";
            radioButtonDirectionB.Size = new System.Drawing.Size(32, 19);
            radioButtonDirectionB.TabIndex = 1;
            radioButtonDirectionB.Text = "b";
            radioButtonDirectionB.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            radioButtonDirectionB.UseVisualStyleBackColor = true;
            radioButtonDirectionB.CheckedChanged += radioButtonDirection_CheckedChanged;
            // 
            // radioButtonDirectionC
            // 
            radioButtonDirectionC.AutoSize = true;
            radioButtonDirectionC.Font = new System.Drawing.Font("Segoe UI", 9F);
            radioButtonDirectionC.Location = new System.Drawing.Point(190, 0);
            radioButtonDirectionC.Margin = new System.Windows.Forms.Padding(0);
            radioButtonDirectionC.Name = "radioButtonDirectionC";
            radioButtonDirectionC.Size = new System.Drawing.Size(31, 19);
            radioButtonDirectionC.TabIndex = 1;
            radioButtonDirectionC.Text = "c";
            radioButtonDirectionC.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            radioButtonDirectionC.UseVisualStyleBackColor = true;
            radioButtonDirectionC.CheckedChanged += radioButtonDirection_CheckedChanged;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Segoe UI", 9F);
            label12.Location = new System.Drawing.Point(236, 2);
            label12.Margin = new System.Windows.Forms.Padding(15, 2, 0, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(74, 15);
            label12.TabIndex = 6;
            label12.Text = "Copy format";
            // 
            // flowLayoutPanel5
            // 
            flowLayoutPanel5.AutoSize = true;
            flowLayoutPanel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel5.Controls.Add(radioButtonEmf);
            flowLayoutPanel5.Controls.Add(radioButtonBmp);
            flowLayoutPanel5.Location = new System.Drawing.Point(310, 0);
            flowLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel5.Name = "flowLayoutPanel5";
            flowLayoutPanel5.Size = new System.Drawing.Size(96, 19);
            flowLayoutPanel5.TabIndex = 7;
            // 
            // FormSymmetryInformation
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            ClientSize = new System.Drawing.Size(1028, 767);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(flowLayoutPanel4);
            Controls.Add(panel2);
            Font = new System.Drawing.Font("Segoe UI", 10F);
            Name = "FormSymmetryInformation";
            Padding = new System.Windows.Forms.Padding(4);
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Text = "Symmetry Information";
            FormClosing += FormCrystallographicInformation_FormClosing;
            Load += FormCrystallographicInformation_Load;
            tableLayoutPanel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBoxSymmetryElements).EndInit();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)graphicsBoxGeneralPositions).EndInit();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            tabControl.ResumeLayout(false);
            tabPageGeometrics.ResumeLayout(false);
            tabPageGeometrics.PerformLayout();
            flowLayoutPanel6.ResumeLayout(false);
            flowLayoutPanel6.PerformLayout();
            flowLayoutPanel14.ResumeLayout(false);
            flowLayoutPanel14.PerformLayout();
            flowLayoutPanel11.ResumeLayout(false);
            flowLayoutPanel11.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tabPageWyckoff.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataSet).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataTableWyckoff).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataTablePlanes).EndInit();
            tabPageConditions.ResumeLayout(false);
            tabPageConditions.PerformLayout();
            groupBoxSpaceGroup.ResumeLayout(false);
            groupBoxSpaceGroup.PerformLayout();
            groupBoxPointGroup.ResumeLayout(false);
            groupBoxPointGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSourceScatteringFactor).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel5.ResumeLayout(false);
            flowLayoutPanel5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.GroupBox groupBoxSpaceGroup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.GroupBox groupBoxPointGroup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TabPage tabPageConditions;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TabPage tabPageWyckoff;
        private Crystallography.Controls.NumericBox numericBoxAnglePlaneAxis2;
        private System.Windows.Forms.TextBox textBoxZonePlane;
        private Crystallography.Controls.NumericBox numericBoxLengthPlane1;
        private Crystallography.Controls.NumericBox numericBoxAnglePlaneAxis1;
        private Crystallography.Controls.NumericBox numericBoxLengthPlane2;
        private Crystallography.Controls.NumericBox numericBoxAngleAxes;
        private System.Windows.Forms.Label label40;
        private Crystallography.Controls.NumericBox numericBoxAnglePlanes;
        private System.Windows.Forms.Label label42;
        private Crystallography.Controls.NumericBox numericBoxLengthAxis1;
        private System.Windows.Forms.TextBox textBoxZoneAxis;
        private Crystallography.Controls.NumericBox numericBoxLengthAxis2;
        // private System.Windows.Forms.DataGridView dataGridView1; // 260518Cl 旧実装
        private DpiAwareDataGridView dataGridView1; // 260518Cl
        private System.Data.DataSet dataSet;
        private System.Data.DataTable dataTableWyckoff;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Data.DataColumn dataColumn6;
        private System.Data.DataColumn dataColumn7;
        private System.Data.DataTable dataTablePlanes;
        private System.Data.DataColumn dataColumnH;
        private System.Data.DataColumn dataColumnK;
        private System.Data.DataColumn dataColumnL;
        private System.Data.DataColumn dataColumnD;
        private System.Data.DataColumn dataColumnCondition;
        private System.Data.DataColumn dataColumnTwoTheta;
        private System.Windows.Forms.Panel panel1;
        private System.Data.DataColumn dataColumnFreal;
        private System.Data.DataColumn dataColumnFinverse;
        private System.Data.DataColumn dataColumnF;
        private System.Data.DataColumn dataColumn13;
        private System.Data.DataColumn dataColumnIntensity;
        private System.Data.DataColumn dataColumnMulti;
        private System.Windows.Forms.BindingSource bindingSourceScatteringFactor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel14;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private GraphicsBox graphicsBoxSymmetryElements;
        private GraphicsBox graphicsBoxGeneralPositions;
        private LabelLaTeX labelLaTexSG_HM;
        private LabelLaTeX labelLaTexSG_Hall;
        private LabelLaTeX labelLaTexSG_SF;
        private LabelLaTeX labelLaTexHM_full;
        private LabelLaTeX labelLaTexCS;
        private LabelLaTeX labelLaTexPG_SF;
        private LabelLaTeX labelLaTexPG_HM;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelExtinctionRule; // 260427Cl
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TabPage tabPageGeometrics;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private LabelLaTeX labelLaTexNumber;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button buttonCopyElements;
        private System.Windows.Forms.RadioButton radioButtonBmp;
        private System.Windows.Forms.RadioButton radioButtonEmf;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button buttonCopyPositions;
        private LabelLaTeX labelLaTex1;
        private NumericBox numericBoxPositionA;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label12;
        private LabelLaTeX labelLaTex2;
        private NumericBox numericBoxPositionB;
        private LabelLaTeX labelLaTex3;
        private NumericBox numericBoxPositionC;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.RadioButton radioButtonDirectionA;
        private System.Windows.Forms.RadioButton radioButtonDirectionB;
        private System.Windows.Forms.RadioButton radioButtonDirectionC;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnMultiplicityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnWyckoffLetterDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnSiteSymmetryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCoordinates1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCoordinates2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCoordinates3DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnCoordinates4DataGridViewTextBoxColumn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private IndexControl indexControlPlane1;
        private IndexControl indexControlPlane2;
        private IndexControl indexControlAxis2;
        private IndexControl indexControlAxis1;
    }
}
