namespace Crystallography.Controls
{
    partial class PoleFigureControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
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
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoleFigureControl));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonAxes = new System.Windows.Forms.RadioButton();
            this.radioButtonPlanes = new System.Windows.Forms.RadioButton();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.labelV = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownResolution = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownFullscale = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButtonPoleFigure = new System.Windows.Forms.RadioButton();
            this.radioButtonInversePoleFigure = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxScale = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxColor = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelU = new System.Windows.Forms.Label();
            this.labelW = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFullscale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // radioButtonAxes
            // 
            resources.ApplyResources(this.radioButtonAxes, "radioButtonAxes");
            this.radioButtonAxes.Checked = true;
            this.radioButtonAxes.Name = "radioButtonAxes";
            this.radioButtonAxes.TabStop = true;
            this.radioButtonAxes.CheckedChanged += new System.EventHandler(this.radioButtonAxes_CheckedChanged);
            // 
            // radioButtonPlanes
            // 
            resources.ApplyResources(this.radioButtonPlanes, "radioButtonPlanes");
            this.radioButtonPlanes.Name = "radioButtonPlanes";
            // 
            // numericUpDown2
            // 
            resources.ApplyResources(this.numericUpDown2, "numericUpDown2");
            this.numericUpDown2.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // numericUpDown3
            // 
            resources.ApplyResources(this.numericUpDown3, "numericUpDown3");
            this.numericUpDown3.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // labelV
            // 
            resources.ApplyResources(this.labelV, "labelV");
            this.labelV.Name = "labelV";
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // pictureBox
            // 
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            this.pictureBox.DoubleClick += new System.EventHandler(this.pictureBox_DoubleClick);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // numericUpDownResolution
            // 
            resources.ApplyResources(this.numericUpDownResolution, "numericUpDownResolution");
            this.numericUpDownResolution.DecimalPlaces = 2;
            this.numericUpDownResolution.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownResolution.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownResolution.Name = "numericUpDownResolution";
            this.numericUpDownResolution.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownResolution.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            this.numericUpDownResolution.Click += new System.EventHandler(this.numericUpDownResolution_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // numericUpDownFullscale
            // 
            resources.ApplyResources(this.numericUpDownFullscale, "numericUpDownFullscale");
            this.numericUpDownFullscale.DecimalPlaces = 1;
            this.numericUpDownFullscale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownFullscale.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDownFullscale.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            -2147483648});
            this.numericUpDownFullscale.Name = "numericUpDownFullscale";
            this.numericUpDownFullscale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFullscale.ValueChanged += new System.EventHandler(this.numericUpDown4_ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // radioButtonPoleFigure
            // 
            resources.ApplyResources(this.radioButtonPoleFigure, "radioButtonPoleFigure");
            this.radioButtonPoleFigure.Checked = true;
            this.radioButtonPoleFigure.Name = "radioButtonPoleFigure";
            this.radioButtonPoleFigure.TabStop = true;
            this.radioButtonPoleFigure.CheckedChanged += new System.EventHandler(this.radioButtonPoleFigure_CheckedChanged);
            // 
            // radioButtonInversePoleFigure
            // 
            resources.ApplyResources(this.radioButtonInversePoleFigure, "radioButtonInversePoleFigure");
            this.radioButtonInversePoleFigure.Name = "radioButtonInversePoleFigure";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.pictureBox);
            this.tabPage1.Name = "tabPage1";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comboBoxScale);
            this.groupBox3.Controls.Add(this.numericUpDownResolution);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.comboBoxColor);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numericUpDownFullscale);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // comboBoxScale
            // 
            resources.ApplyResources(this.comboBoxScale, "comboBoxScale");
            this.comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScale.FormattingEnabled = true;
            this.comboBoxScale.Items.AddRange(new object[] {
            resources.GetString("comboBoxScale.Items"),
            resources.GetString("comboBoxScale.Items1")});
            this.comboBoxScale.Name = "comboBoxScale";
            this.comboBoxScale.SelectedIndexChanged += new System.EventHandler(this.Combobox_SelectedIndexChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // comboBoxColor
            // 
            resources.ApplyResources(this.comboBoxColor, "comboBoxColor");
            this.comboBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColor.FormattingEnabled = true;
            this.comboBoxColor.Items.AddRange(new object[] {
            resources.GetString("comboBoxColor.Items"),
            resources.GetString("comboBoxColor.Items1")});
            this.comboBoxColor.Name = "comboBoxColor";
            this.comboBoxColor.SelectedIndexChanged += new System.EventHandler(this.Combobox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.radioButtonAxes);
            this.groupBox2.Controls.Add(this.radioButtonPlanes);
            this.groupBox2.Controls.Add(this.numericUpDown2);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.numericUpDown3);
            this.groupBox2.Controls.Add(this.labelU);
            this.groupBox2.Controls.Add(this.labelW);
            this.groupBox2.Controls.Add(this.labelV);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // labelU
            // 
            resources.ApplyResources(this.labelU, "labelU");
            this.labelU.Name = "labelU";
            // 
            // labelW
            // 
            resources.ApplyResources(this.labelW, "labelW");
            this.labelW.Name = "labelW";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.radioButtonInversePoleFigure);
            this.groupBox1.Controls.Add(this.radioButtonPoleFigure);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // PoleFigureControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "PoleFigureControl";
            this.Load += new System.EventHandler(this.PoleFigureControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownResolution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFullscale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButtonAxes;
        private System.Windows.Forms.RadioButton radioButtonPlanes;
        public System.Windows.Forms.NumericUpDown numericUpDown2;
        public System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label labelV;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownResolution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDownFullscale;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioButtonPoleFigure;
        private System.Windows.Forms.RadioButton radioButtonInversePoleFigure;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox comboBoxScale;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox comboBoxColor;
        private System.Windows.Forms.Label labelU;
        private System.Windows.Forms.Label labelW;
    }
}
