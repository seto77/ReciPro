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
            pictureBox1 = new System.Windows.Forms.PictureBox();
            numericBoxResolution = new NumericBox();
            label9 = new System.Windows.Forms.Label();
            comboBoxColor = new System.Windows.Forms.ComboBox();
            numericBoxMax = new NumericBox();
            numericBoxMin = new NumericBox();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            // 
            // numericBoxResolution
            // 
            resources.ApplyResources(numericBoxResolution, "numericBoxResolution");
            numericBoxResolution.BackColor = System.Drawing.Color.Transparent;
            numericBoxResolution.Maximum = 30D;
            numericBoxResolution.Minimum = 1D;
            numericBoxResolution.Name = "numericBoxResolution";
            numericBoxResolution.RadianValue = 0.10471975511965977D;
            numericBoxResolution.RoundErrorAccuracy = -1;
            numericBoxResolution.ShowUpDown = true;
            numericBoxResolution.SmartIncrement = true;
            numericBoxResolution.Value = 6D;
            numericBoxResolution.ValueChanged += numericUpDownResolution_Click;
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
            // numericBoxMax
            // 
            resources.ApplyResources(numericBoxMax, "numericBoxMax");
            numericBoxMax.BackColor = System.Drawing.Color.Transparent;
            numericBoxMax.Maximum = 1000000D;
            numericBoxMax.Minimum = 0D;
            numericBoxMax.Name = "numericBoxMax";
            numericBoxMax.RadianValue = 1.7453292519943295D;
            numericBoxMax.RoundErrorAccuracy = -1;
            numericBoxMax.ShowTrigonomeric = true;
            numericBoxMax.ShowUpDown = true;
            numericBoxMax.SmartIncrement = true;
            numericBoxMax.Value = 100D;
            numericBoxMax.ValueChanged += numericBoxMax_ValueChanged;
            // 
            // numericBoxMin
            // 
            resources.ApplyResources(numericBoxMin, "numericBoxMin");
            numericBoxMin.BackColor = System.Drawing.Color.Transparent;
            numericBoxMin.Maximum = 10000D;
            numericBoxMin.Minimum = 0D;
            numericBoxMin.Name = "numericBoxMin";
            numericBoxMin.RoundErrorAccuracy = -1;
            numericBoxMin.ShowTrigonomeric = true;
            numericBoxMin.ShowUpDown = true;
            numericBoxMin.SmartIncrement = true;
            numericBoxMin.ValueChanged += numericBoxMax_ValueChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.BackColor = System.Drawing.Color.White;
            label1.Name = "label1";
            // 
            // PoleFigureControl2
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(label1);
            Controls.Add(numericBoxResolution);
            Controls.Add(numericBoxMin);
            Controls.Add(label9);
            Controls.Add(comboBoxColor);
            Controls.Add(numericBoxMax);
            Controls.Add(pictureBox1);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(pictureBox);
            Name = "PoleFigureControl2";
            Load += PoleFigureControl_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox comboBoxColor;
        private NumericBox numericBoxMin;
        private NumericBox numericBoxMax;
        private NumericBox numericBoxResolution;
        private System.Windows.Forms.Label label1;
    }
}
