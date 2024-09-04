namespace Crystallography.Controls
{
    partial class PoleFigureControl2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PoleFigureControl2));
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            pictureBox = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            numericUpDownResolution = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            numericUpDownFullscale = new System.Windows.Forms.NumericUpDown();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            textBox1 = new System.Windows.Forms.TextBox();
            groupBox3 = new System.Windows.Forms.GroupBox();
            label7 = new System.Windows.Forms.Label();
            comboBoxScale = new System.Windows.Forms.ComboBox();
            label9 = new System.Windows.Forms.Label();
            comboBoxColor = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownResolution).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFullscale).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(flowLayoutPanel1, "flowLayoutPanel1");
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // pictureBox
            // 
            resources.ApplyResources(pictureBox, "pictureBox");
            pictureBox.Name = "pictureBox";
            pictureBox.TabStop = false;
            pictureBox.DoubleClick += pictureBox_DoubleClick;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // numericUpDownResolution
            // 
            resources.ApplyResources(numericUpDownResolution, "numericUpDownResolution");
            numericUpDownResolution.DecimalPlaces = 2;
            numericUpDownResolution.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            numericUpDownResolution.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownResolution.Name = "numericUpDownResolution";
            numericUpDownResolution.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownResolution.ValueChanged += numericUpDownResolution_ValueChanged;
            numericUpDownResolution.Click += numericUpDownResolution_Click;
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
            // numericUpDownFullscale
            // 
            resources.ApplyResources(numericUpDownFullscale, "numericUpDownFullscale");
            numericUpDownFullscale.DecimalPlaces = 1;
            numericUpDownFullscale.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numericUpDownFullscale.Maximum = new decimal(new int[] { 9, 0, 0, 0 });
            numericUpDownFullscale.Minimum = new decimal(new int[] { 9, 0, 0, int.MinValue });
            numericUpDownFullscale.Name = "numericUpDownFullscale";
            numericUpDownFullscale.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownFullscale.ValueChanged += numericUpDownFullScale_ValueChanged;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            // 
            // groupBox3
            // 
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(comboBoxScale);
            groupBox3.Controls.Add(numericUpDownResolution);
            groupBox3.Controls.Add(textBox1);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(comboBoxColor);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(numericUpDownFullscale);
            groupBox3.Controls.Add(label3);
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            // 
            // comboBoxScale
            // 
            resources.ApplyResources(comboBoxScale, "comboBoxScale");
            comboBoxScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScale.FormattingEnabled = true;
            comboBoxScale.Items.AddRange(new object[] { resources.GetString("comboBoxScale.Items"), resources.GetString("comboBoxScale.Items1") });
            comboBoxScale.Name = "comboBoxScale";
            comboBoxScale.SelectedIndexChanged += Combobox_SelectedIndexChanged;
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            // 
            // comboBoxColor
            // 
            resources.ApplyResources(comboBoxColor, "comboBoxColor");
            comboBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxColor.FormattingEnabled = true;
            comboBoxColor.Items.AddRange(new object[] { resources.GetString("comboBoxColor.Items"), resources.GetString("comboBoxColor.Items1") });
            comboBoxColor.Name = "comboBoxColor";
            comboBoxColor.SelectedIndexChanged += Combobox_SelectedIndexChanged;
            // 
            // PoleFigureControl2
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(groupBox3);
            Controls.Add(pictureBox1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(pictureBox);
            Name = "PoleFigureControl2";
            Load += PoleFigureControl_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownResolution).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFullscale).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownResolution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDownFullscale;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox comboBoxScale;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox comboBoxColor;
    }
}
