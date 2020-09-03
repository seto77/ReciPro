namespace Crystallography.Controls
{
    partial class NumericBox
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
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumericBox));
            this.textBox = new System.Windows.Forms.TextBox();
            this.labelHeader = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.labelFooter = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox
            // 
            resources.ApplyResources(this.textBox, "textBox");
            this.textBox.Name = "textBox";
            this.textBox.Click += new System.EventHandler(this.textBox_Click);
            this.textBox.ReadOnlyChanged += new System.EventHandler(this.textBox_ReadOnlyChanged);
            this.textBox.FontChanged += new System.EventHandler(this.textBox_FontChanged);
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBox.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            this.textBox.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // labelHeader
            // 
            resources.ApplyResources(this.labelHeader, "labelHeader");
            this.labelHeader.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelHeader.Name = "labelHeader";
            // 
            // numericUpDown
            // 
            resources.ApplyResources(this.numericUpDown, "numericUpDown");
            this.numericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.TabStop = false;
            this.numericUpDown.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            // 
            // labelFooter
            // 
            resources.ApplyResources(this.labelFooter, "labelFooter");
            this.labelFooter.Name = "labelFooter";
            // 
            // NumericBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.numericUpDown);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.labelFooter);
            this.Name = "NumericBox";
            this.SizeChanged += new System.EventHandler(this.numericBox_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Label labelFooter;
        public System.Windows.Forms.ToolTip toolTip;
        internal System.Windows.Forms.TextBox textBox;
        internal System.Windows.Forms.NumericUpDown numericUpDown;
    }
}
