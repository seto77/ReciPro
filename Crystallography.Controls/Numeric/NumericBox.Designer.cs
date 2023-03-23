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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumericBox));
            textBox = new System.Windows.Forms.TextBox();
            labelHeader = new System.Windows.Forms.Label();
            numericUpDown = new System.Windows.Forms.NumericUpDown();
            labelFooter = new System.Windows.Forms.Label();
            toolTip = new System.Windows.Forms.ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)numericUpDown).BeginInit();
            SuspendLayout();
            // 
            // textBox
            // 
            resources.ApplyResources(textBox, "textBox");
            textBox.Name = "textBox";
            textBox.Click += textBox_Click;
            textBox.ReadOnlyChanged += textBox_ReadOnlyChanged;
            textBox.FontChanged += textBox_FontChanged;
            textBox.TextChanged += textBox_TextChanged;
            textBox.Enter += textBox_Enter;
            textBox.KeyDown += textBox_KeyDown;
            textBox.KeyPress += textBox_KeyPress;
            textBox.Leave += textBox_Leave;
            // 
            // labelHeader
            // 
            resources.ApplyResources(labelHeader, "labelHeader");
            labelHeader.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            labelHeader.Name = "labelHeader";
            // 
            // numericUpDown
            // 
            resources.ApplyResources(numericUpDown, "numericUpDown");
            numericUpDown.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
            numericUpDown.Name = "numericUpDown";
            numericUpDown.TabStop = false;
            numericUpDown.ValueChanged += numericUpDown_ValueChanged;
            // 
            // labelFooter
            // 
            resources.ApplyResources(labelFooter, "labelFooter");
            labelFooter.Name = "labelFooter";
            // 
            // NumericBox
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(textBox);
            Controls.Add(numericUpDown);
            Controls.Add(labelHeader);
            Controls.Add(labelFooter);
            Name = "NumericBox";
            SizeChanged += numericBox_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)numericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Label labelFooter;
        public System.Windows.Forms.ToolTip toolTip;
        internal System.Windows.Forms.TextBox textBox;
        internal System.Windows.Forms.NumericUpDown numericUpDown;
    }
}
