namespace Crystallography.Controls
{
    partial class BoundControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoundControl));
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.numericBoxDistanceD = new Crystallography.Controls.NumericBox();
            this.numericBoxDistance = new Crystallography.Controls.NumericBox();
            this.colorControl = new Crystallography.Controls.ColorControl();
            this.numericBoxL = new Crystallography.Controls.NumericBox();
            this.numericBoxK = new Crystallography.Controls.NumericBox();
            this.checkBoxEquivalency = new System.Windows.Forms.CheckBox();
            this.numericBoxH = new Crystallography.Controls.NumericBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new Crystallography.Controls.DataSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonAddBond = new System.Windows.Forms.Button();
            this.buttonChangeBond = new System.Windows.Forms.Button();
            this.buttonDeleteBond = new System.Windows.Forms.Button();
            this.dataGridViewImageColumn3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn6 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn7 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn8 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn9 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn10 = new System.Windows.Forms.DataGridViewImageColumn();
            this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.hDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.equivalencyDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MultipleOfD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.distanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.colorControl);
            this.panel2.Controls.Add(this.numericBoxL);
            this.panel2.Controls.Add(this.numericBoxK);
            this.panel2.Controls.Add(this.checkBoxEquivalency);
            this.panel2.Controls.Add(this.numericBoxH);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Name = "panel2";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.numericBoxDistanceD);
            this.flowLayoutPanel1.Controls.Add(this.numericBoxDistance);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // numericBoxDistanceD
            // 
            resources.ApplyResources(this.numericBoxDistanceD, "numericBoxDistanceD");
            this.numericBoxDistanceD.AllowMouseControl = false;
            this.numericBoxDistanceD.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistanceD.DecimalPlaces = 3;
            this.numericBoxDistanceD.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistanceD.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxDistanceD.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistanceD.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxDistanceD.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxDistanceD.Maximum = 20D;
            this.numericBoxDistanceD.Minimum = -20D;
            this.numericBoxDistanceD.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxDistanceD.MouseSpeed = 1D;
            this.numericBoxDistanceD.Multiline = false;
            this.numericBoxDistanceD.Name = "numericBoxDistanceD";
            this.numericBoxDistanceD.RadianValue = 0.017453292519943295D;
            this.numericBoxDistanceD.ReadOnly = false;
            this.numericBoxDistanceD.RestrictLimitValue = true;
            this.numericBoxDistanceD.ShowFraction = false;
            this.numericBoxDistanceD.ShowPositiveSign = false;
            this.numericBoxDistanceD.ShowUpDown = true;
            this.numericBoxDistanceD.SkipEventDuringInput = false;
            this.numericBoxDistanceD.SmartIncrement = false;
            this.numericBoxDistanceD.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDistanceD.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDistanceD.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistanceD.ThonsandsSeparator = true;
            this.numericBoxDistanceD.UpDown_Increment = 0.1D;
            this.numericBoxDistanceD.Value = 1D;
            this.numericBoxDistanceD.WordWrap = true;
            this.numericBoxDistanceD.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxDistanceD_ValueChanged);
            // 
            // numericBoxDistance
            // 
            resources.ApplyResources(this.numericBoxDistance, "numericBoxDistance");
            this.numericBoxDistance.AllowMouseControl = false;
            this.numericBoxDistance.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.DecimalPlaces = 3;
            this.numericBoxDistance.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxDistance.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxDistance.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxDistance.Maximum = 100D;
            this.numericBoxDistance.Minimum = -100D;
            this.numericBoxDistance.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxDistance.MouseSpeed = 1D;
            this.numericBoxDistance.Multiline = false;
            this.numericBoxDistance.Name = "numericBoxDistance";
            this.numericBoxDistance.RadianValue = 0D;
            this.numericBoxDistance.ReadOnly = false;
            this.numericBoxDistance.RestrictLimitValue = true;
            this.numericBoxDistance.ShowFraction = false;
            this.numericBoxDistance.ShowPositiveSign = false;
            this.numericBoxDistance.ShowUpDown = false;
            this.numericBoxDistance.SkipEventDuringInput = false;
            this.numericBoxDistance.SmartIncrement = false;
            this.numericBoxDistance.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDistance.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDistance.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.ThonsandsSeparator = true;
            this.numericBoxDistance.UpDown_Increment = 1D;
            this.numericBoxDistance.Value = 0D;
            this.numericBoxDistance.WordWrap = true;
            this.numericBoxDistance.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxDistance_ValueChanged);
            // 
            // colorControl
            // 
            resources.ApplyResources(this.colorControl, "colorControl");
            this.colorControl.Argb = -16728064;
            this.colorControl.Blue = 0;
            this.colorControl.BlueF = 0F;
            this.colorControl.BoxSize = new System.Drawing.Size(20, 20);
            this.colorControl.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.colorControl.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.colorControl.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorControl.FooterMargin = new System.Windows.Forms.Padding(0);
            this.colorControl.FooterText = "";
            this.colorControl.Green = 192;
            this.colorControl.GreenF = 0.7529412F;
            this.colorControl.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorControl.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.colorControl.HeaderText = "Color";
            this.colorControl.Name = "colorControl";
            this.colorControl.Red = 0;
            this.colorControl.RedF = 0F;
            this.colorControl.ToolTip = "";
            // 
            // numericBoxL
            // 
            resources.ApplyResources(this.numericBoxL, "numericBoxL");
            this.numericBoxL.AllowMouseControl = false;
            this.numericBoxL.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxL.DecimalPlaces = 0;
            this.numericBoxL.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxL.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxL.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxL.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxL.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxL.Maximum = 10D;
            this.numericBoxL.Minimum = -10D;
            this.numericBoxL.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxL.MouseSpeed = 1D;
            this.numericBoxL.Multiline = false;
            this.numericBoxL.Name = "numericBoxL";
            this.numericBoxL.RadianValue = 0D;
            this.numericBoxL.ReadOnly = false;
            this.numericBoxL.RestrictLimitValue = true;
            this.numericBoxL.ShowFraction = false;
            this.numericBoxL.ShowPositiveSign = false;
            this.numericBoxL.ShowUpDown = true;
            this.numericBoxL.SkipEventDuringInput = false;
            this.numericBoxL.SmartIncrement = false;
            this.numericBoxL.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxL.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxL.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxL.ThonsandsSeparator = true;
            this.numericBoxL.UpDown_Increment = 1D;
            this.numericBoxL.Value = 0D;
            this.numericBoxL.WordWrap = true;
            this.numericBoxL.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxDistanceD_ValueChanged);
            // 
            // numericBoxK
            // 
            resources.ApplyResources(this.numericBoxK, "numericBoxK");
            this.numericBoxK.AllowMouseControl = false;
            this.numericBoxK.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxK.DecimalPlaces = 0;
            this.numericBoxK.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxK.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxK.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxK.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxK.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxK.Maximum = 10D;
            this.numericBoxK.Minimum = -10D;
            this.numericBoxK.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxK.MouseSpeed = 1D;
            this.numericBoxK.Multiline = false;
            this.numericBoxK.Name = "numericBoxK";
            this.numericBoxK.RadianValue = 0D;
            this.numericBoxK.ReadOnly = false;
            this.numericBoxK.RestrictLimitValue = true;
            this.numericBoxK.ShowFraction = false;
            this.numericBoxK.ShowPositiveSign = false;
            this.numericBoxK.ShowUpDown = true;
            this.numericBoxK.SkipEventDuringInput = false;
            this.numericBoxK.SmartIncrement = false;
            this.numericBoxK.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxK.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxK.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxK.ThonsandsSeparator = true;
            this.numericBoxK.UpDown_Increment = 1D;
            this.numericBoxK.Value = 0D;
            this.numericBoxK.WordWrap = true;
            this.numericBoxK.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxDistanceD_ValueChanged);
            // 
            // checkBoxEquivalency
            // 
            resources.ApplyResources(this.checkBoxEquivalency, "checkBoxEquivalency");
            this.checkBoxEquivalency.Checked = true;
            this.checkBoxEquivalency.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEquivalency.Name = "checkBoxEquivalency";
            this.checkBoxEquivalency.UseVisualStyleBackColor = true;
            this.checkBoxEquivalency.CheckedChanged += new System.EventHandler(this.checkBoxEquivalency_CheckedChanged);
            // 
            // numericBoxH
            // 
            resources.ApplyResources(this.numericBoxH, "numericBoxH");
            this.numericBoxH.AllowMouseControl = false;
            this.numericBoxH.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxH.DecimalPlaces = 0;
            this.numericBoxH.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxH.FooterForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxH.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxH.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.numericBoxH.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.numericBoxH.Maximum = 10D;
            this.numericBoxH.Minimum = -10D;
            this.numericBoxH.MouseDirection = Crystallography.VH_DirectionEnum.Horizontal;
            this.numericBoxH.MouseSpeed = 1D;
            this.numericBoxH.Multiline = false;
            this.numericBoxH.Name = "numericBoxH";
            this.numericBoxH.RadianValue = 0D;
            this.numericBoxH.ReadOnly = false;
            this.numericBoxH.RestrictLimitValue = true;
            this.numericBoxH.ShowFraction = false;
            this.numericBoxH.ShowPositiveSign = false;
            this.numericBoxH.ShowUpDown = true;
            this.numericBoxH.SkipEventDuringInput = false;
            this.numericBoxH.SmartIncrement = false;
            this.numericBoxH.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxH.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxH.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxH.ThonsandsSeparator = true;
            this.numericBoxH.UpDown_Increment = 1D;
            this.numericBoxH.Value = 0D;
            this.numericBoxH.WordWrap = true;
            this.numericBoxH.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxDistanceD_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
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
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Bond color";
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.DataPropertyName = "Bond color";
            resources.ApplyResources(this.dataGridViewImageColumn1, "dataGridViewImageColumn1");
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.DataPropertyName = "Polyhedron color";
            resources.ApplyResources(this.dataGridViewImageColumn2, "dataGridViewImageColumn2");
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            this.dataGridViewImageColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridView
            // 
            resources.ApplyResources(this.dataGridView, "dataGridView");
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.enabledDataGridViewCheckBoxColumn,
            this.hDataGridViewTextBoxColumn,
            this.kDataGridViewTextBoxColumn,
            this.lDataGridViewTextBoxColumn,
            this.equivalencyDataGridViewCheckBoxColumn,
            this.MultipleOfD,
            this.distanceDataGridViewTextBoxColumn,
            this.colorDataGridViewTextBoxColumn});
            this.dataGridView.DataSource = this.bindingSource;
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowTemplate.Height = 21;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellValueChanged);
            this.dataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridView_CurrentCellDirtyStateChanged);
            // 
            // bindingSource
            // 
            this.bindingSource.DataMember = "DataTableBound";
            this.bindingSource.DataSource = this.dataSet;
            this.bindingSource.CurrentChanged += new System.EventHandler(this.bindingSource_PositionChanged);
            this.bindingSource.PositionChanged += new System.EventHandler(this.bindingSource_PositionChanged);
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.buttonAddBond);
            this.panel1.Controls.Add(this.buttonChangeBond);
            this.panel1.Controls.Add(this.buttonDeleteBond);
            this.panel1.Name = "panel1";
            // 
            // buttonAddBond
            // 
            resources.ApplyResources(this.buttonAddBond, "buttonAddBond");
            this.buttonAddBond.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonAddBond.ForeColor = System.Drawing.Color.White;
            this.buttonAddBond.Name = "buttonAddBond";
            this.buttonAddBond.UseVisualStyleBackColor = false;
            this.buttonAddBond.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonChangeBond
            // 
            resources.ApplyResources(this.buttonChangeBond, "buttonChangeBond");
            this.buttonChangeBond.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonChangeBond.ForeColor = System.Drawing.Color.White;
            this.buttonChangeBond.Name = "buttonChangeBond";
            this.buttonChangeBond.UseVisualStyleBackColor = false;
            this.buttonChangeBond.Click += new System.EventHandler(this.buttonChange_Click);
            // 
            // buttonDeleteBond
            // 
            resources.ApplyResources(this.buttonDeleteBond, "buttonDeleteBond");
            this.buttonDeleteBond.BackColor = System.Drawing.Color.IndianRed;
            this.buttonDeleteBond.ForeColor = System.Drawing.Color.White;
            this.buttonDeleteBond.Name = "buttonDeleteBond";
            this.buttonDeleteBond.UseVisualStyleBackColor = false;
            this.buttonDeleteBond.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // dataGridViewImageColumn3
            // 
            this.dataGridViewImageColumn3.DataPropertyName = "Color";
            resources.ApplyResources(this.dataGridViewImageColumn3, "dataGridViewImageColumn3");
            this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
            this.dataGridViewImageColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn4
            // 
            this.dataGridViewImageColumn4.DataPropertyName = "Color";
            resources.ApplyResources(this.dataGridViewImageColumn4, "dataGridViewImageColumn4");
            this.dataGridViewImageColumn4.Name = "dataGridViewImageColumn4";
            this.dataGridViewImageColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn5
            // 
            this.dataGridViewImageColumn5.DataPropertyName = "Color";
            resources.ApplyResources(this.dataGridViewImageColumn5, "dataGridViewImageColumn5");
            this.dataGridViewImageColumn5.Name = "dataGridViewImageColumn5";
            this.dataGridViewImageColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn6
            // 
            this.dataGridViewImageColumn6.DataPropertyName = "Color";
            resources.ApplyResources(this.dataGridViewImageColumn6, "dataGridViewImageColumn6");
            this.dataGridViewImageColumn6.Name = "dataGridViewImageColumn6";
            this.dataGridViewImageColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn7
            // 
            this.dataGridViewImageColumn7.DataPropertyName = "Color";
            resources.ApplyResources(this.dataGridViewImageColumn7, "dataGridViewImageColumn7");
            this.dataGridViewImageColumn7.Name = "dataGridViewImageColumn7";
            this.dataGridViewImageColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn8
            // 
            this.dataGridViewImageColumn8.DataPropertyName = "Color";
            resources.ApplyResources(this.dataGridViewImageColumn8, "dataGridViewImageColumn8");
            this.dataGridViewImageColumn8.Name = "dataGridViewImageColumn8";
            this.dataGridViewImageColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn9
            // 
            this.dataGridViewImageColumn9.DataPropertyName = "Color";
            resources.ApplyResources(this.dataGridViewImageColumn9, "dataGridViewImageColumn9");
            this.dataGridViewImageColumn9.Name = "dataGridViewImageColumn9";
            this.dataGridViewImageColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dataGridViewImageColumn10
            // 
            this.dataGridViewImageColumn10.DataPropertyName = "Color";
            resources.ApplyResources(this.dataGridViewImageColumn10, "dataGridViewImageColumn10");
            this.dataGridViewImageColumn10.Name = "dataGridViewImageColumn10";
            this.dataGridViewImageColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // enabledDataGridViewCheckBoxColumn
            // 
            this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
            resources.ApplyResources(this.enabledDataGridViewCheckBoxColumn, "enabledDataGridViewCheckBoxColumn");
            this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
            // 
            // hDataGridViewTextBoxColumn
            // 
            this.hDataGridViewTextBoxColumn.DataPropertyName = "h";
            resources.ApplyResources(this.hDataGridViewTextBoxColumn, "hDataGridViewTextBoxColumn");
            this.hDataGridViewTextBoxColumn.Name = "hDataGridViewTextBoxColumn";
            this.hDataGridViewTextBoxColumn.ReadOnly = true;
            this.hDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // kDataGridViewTextBoxColumn
            // 
            this.kDataGridViewTextBoxColumn.DataPropertyName = "k";
            resources.ApplyResources(this.kDataGridViewTextBoxColumn, "kDataGridViewTextBoxColumn");
            this.kDataGridViewTextBoxColumn.Name = "kDataGridViewTextBoxColumn";
            this.kDataGridViewTextBoxColumn.ReadOnly = true;
            this.kDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lDataGridViewTextBoxColumn
            // 
            this.lDataGridViewTextBoxColumn.DataPropertyName = "l";
            resources.ApplyResources(this.lDataGridViewTextBoxColumn, "lDataGridViewTextBoxColumn");
            this.lDataGridViewTextBoxColumn.Name = "lDataGridViewTextBoxColumn";
            this.lDataGridViewTextBoxColumn.ReadOnly = true;
            this.lDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // equivalencyDataGridViewCheckBoxColumn
            // 
            this.equivalencyDataGridViewCheckBoxColumn.DataPropertyName = "Equivalency";
            resources.ApplyResources(this.equivalencyDataGridViewCheckBoxColumn, "equivalencyDataGridViewCheckBoxColumn");
            this.equivalencyDataGridViewCheckBoxColumn.Name = "equivalencyDataGridViewCheckBoxColumn";
            this.equivalencyDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // MultipleOfD
            // 
            this.MultipleOfD.DataPropertyName = "MultipleOfD";
            resources.ApplyResources(this.MultipleOfD, "MultipleOfD");
            this.MultipleOfD.Name = "MultipleOfD";
            this.MultipleOfD.ReadOnly = true;
            this.MultipleOfD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // distanceDataGridViewTextBoxColumn
            // 
            this.distanceDataGridViewTextBoxColumn.DataPropertyName = "Distance";
            resources.ApplyResources(this.distanceDataGridViewTextBoxColumn, "distanceDataGridViewTextBoxColumn");
            this.distanceDataGridViewTextBoxColumn.Name = "distanceDataGridViewTextBoxColumn";
            this.distanceDataGridViewTextBoxColumn.ReadOnly = true;
            this.distanceDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colorDataGridViewTextBoxColumn
            // 
            this.colorDataGridViewTextBoxColumn.DataPropertyName = "Color";
            resources.ApplyResources(this.colorDataGridViewTextBoxColumn, "colorDataGridViewTextBoxColumn");
            this.colorDataGridViewTextBoxColumn.Name = "colorDataGridViewTextBoxColumn";
            this.colorDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // BoundControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "BoundControl";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource bindingSource;
        private DataSet dataSet;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAddBond;
        private System.Windows.Forms.Button buttonChangeBond;
        private System.Windows.Forms.Button buttonDeleteBond;
        private NumericBox numericBoxL;
        private NumericBox numericBoxK;
        private System.Windows.Forms.CheckBox checkBoxEquivalency;
        private NumericBox numericBoxH;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private NumericBox numericBoxDistanceD;
        private NumericBox numericBoxDistance;
        private System.Windows.Forms.Label label6;
        private ColorControl colorControl;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn3;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn4;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn5;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn6;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn8;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn9;
        private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn hDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn kDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn equivalencyDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MultipleOfD;
        private System.Windows.Forms.DataGridViewTextBoxColumn distanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn colorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn10;
    }
}
