namespace Crystallography.Controls
{
    partial class ColorControl
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
            pictureBox = new System.Windows.Forms.PictureBox();
            toolTip = new System.Windows.Forms.ToolTip(components);
            labelHeader = new System.Windows.Forms.Label();
            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            labelFooter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            flowLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pictureBox.Location = new System.Drawing.Point(0, 0);
            pictureBox.Margin = new System.Windows.Forms.Padding(0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new System.Drawing.Size(24, 24);
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            pictureBox.BackColorChanged += pictureBox_BackColorChanged;
            pictureBox.Click += pictureBox_Click;
            // 
            // labelHeader
            // 
            labelHeader.AutoSize = true;
            labelHeader.Location = new System.Drawing.Point(0, 0);
            labelHeader.Margin = new System.Windows.Forms.Padding(0);
            labelHeader.Name = "labelHeader";
            labelHeader.Size = new System.Drawing.Size(0, 23);
            labelHeader.TabIndex = 2;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanel.Controls.Add(labelHeader);
            flowLayoutPanel.Controls.Add(pictureBox);
            flowLayoutPanel.Controls.Add(labelFooter);
            flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new System.Drawing.Size(24, 24);
            flowLayoutPanel.TabIndex = 4;
            // 
            // labelFooter
            // 
            labelFooter.AutoSize = true;
            labelFooter.Location = new System.Drawing.Point(24, 0);
            labelFooter.Margin = new System.Windows.Forms.Padding(0);
            labelFooter.Name = "labelFooter";
            labelFooter.Size = new System.Drawing.Size(0, 23);
            labelFooter.TabIndex = 3;
            // 
            // ColorControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Controls.Add(flowLayoutPanel);
            Font = new System.Drawing.Font("Segoe UI Symbol", 12.1875F);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "ColorControl";
            Size = new System.Drawing.Size(24, 24);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            flowLayoutPanel.ResumeLayout(false);
            flowLayoutPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label labelFooter;
    }
}
