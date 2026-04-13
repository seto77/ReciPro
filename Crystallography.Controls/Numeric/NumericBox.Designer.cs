namespace Crystallography.Controls
{
    partial class NumericBox
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
            labelFooter = new System.Windows.Forms.Label();
            spinButtonPanel = new System.Windows.Forms.Panel();                                                                                       // 260413Cl 追加 SpinButtonの入れ物
            toolTip = new System.Windows.Forms.ToolTip(components);
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
            labelHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            labelHeader.Name = "labelHeader";
            labelHeader.UseCompatibleTextRendering = false;                                                                                           // 260413Cl 追加 textBox(GDI)と描画エンジンを揃えベースラインずれを解消
            //
            // labelFooter
            //
            resources.ApplyResources(labelFooter, "labelFooter");
            labelFooter.Name = "labelFooter";
            labelFooter.UseCompatibleTextRendering = false;                                                                                           // 260413Cl 追加 textBox(GDI)と描画エンジンを揃えベースラインずれを解消
            //
            // spinButtonPanel                                                                                                                        // 260413Cl 追加
            //
            resources.ApplyResources(spinButtonPanel, "spinButtonPanel");
            spinButtonPanel.Name = "spinButtonPanel";
            spinButtonPanel.BackColor = System.Drawing.Color.Transparent;
            //
            // NumericBox
            //
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            BackColor = System.Drawing.Color.Transparent;
            Controls.Add(textBox);
            Controls.Add(spinButtonPanel);                                                                                                            // 260413Cl 追加 textBoxの直後に配置
            Controls.Add(labelHeader);
            Controls.Add(labelFooter);
            Name = "NumericBox";
            SizeChanged += numericBox_SizeChanged;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Label labelFooter;
        public System.Windows.Forms.ToolTip toolTip;
        internal System.Windows.Forms.TextBox textBox;
        //internal System.Windows.Forms.NumericUpDown numericUpDown;                                                                                  // 260413Cl
        internal System.Windows.Forms.Panel spinButtonPanel;                                                                                          // 260413Cl 追加 SpinButtonの入れ物
        internal SpinButton spinButton;                                                                                                               // 260413Cl 追加
    }
}
