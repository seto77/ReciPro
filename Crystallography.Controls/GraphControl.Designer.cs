namespace Crystallography.Controls
{
    partial class GraphControl
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
            if (Bmp != null)
                Bmp.Dispose();
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
            labelBlank = new System.Windows.Forms.Label();
            labelY = new System.Windows.Forms.Label();
            labelYValue = new System.Windows.Forms.Label();
            labelXValue = new System.Windows.Forms.Label();
            labelGraphTitle = new System.Windows.Forms.Label();
            labelX = new System.Windows.Forms.Label();
            pictureBox = new System.Windows.Forms.PictureBox();
            flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            flowLayoutPanelMousePosition = new System.Windows.Forms.FlowLayoutPanel();
            contextMenuStripY = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItemLogScaleX = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemLogScaleY = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItemScaleLineX = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemScaleLineY = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            flowLayoutPanel.SuspendLayout();
            flowLayoutPanelMousePosition.SuspendLayout();
            contextMenuStripY.SuspendLayout();
            this.panel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelBlank
            // 
            labelBlank.AutoSize = true;
            labelBlank.Location = new System.Drawing.Point(57, 0);
            labelBlank.Name = "labelBlank";
            labelBlank.Size = new System.Drawing.Size(19, 15);
            labelBlank.TabIndex = 8;
            labelBlank.Text = "    ";
            // 
            // labelY
            // 
            labelY.AutoSize = true;
            labelY.Location = new System.Drawing.Point(82, 0);
            labelY.Name = "labelY";
            labelY.Size = new System.Drawing.Size(17, 15);
            labelY.TabIndex = 1;
            labelY.Text = "Y:";
            // 
            // labelYValue
            // 
            labelYValue.AutoSize = true;
            labelYValue.Location = new System.Drawing.Point(105, 0);
            labelYValue.Name = "labelYValue";
            labelYValue.Size = new System.Drawing.Size(25, 15);
            labelYValue.TabIndex = 1;
            labelYValue.Text = "000";
            // 
            // labelXValue
            // 
            labelXValue.AutoSize = true;
            labelXValue.Location = new System.Drawing.Point(26, 0);
            labelXValue.Name = "labelXValue";
            labelXValue.Size = new System.Drawing.Size(25, 15);
            labelXValue.TabIndex = 5;
            labelXValue.Text = "000";
            // 
            // labelGraphTitle
            // 
            labelGraphTitle.AutoSize = true;
            labelGraphTitle.Location = new System.Drawing.Point(3, 0);
            labelGraphTitle.Name = "labelGraphTitle";
            labelGraphTitle.Size = new System.Drawing.Size(25, 15);
            labelGraphTitle.TabIndex = 8;
            labelGraphTitle.Text = "aaa";
            // 
            // labelX
            // 
            labelX.AutoSize = true;
            labelX.Location = new System.Drawing.Point(3, 0);
            labelX.Name = "labelX";
            labelX.Size = new System.Drawing.Size(17, 15);
            labelX.TabIndex = 1;
            labelX.Text = "X:";
            // 
            // pictureBox
            // 
            pictureBox.BackColor = System.Drawing.Color.White;
            pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox.Location = new System.Drawing.Point(0, 0);
            pictureBox.Margin = new System.Windows.Forms.Padding(0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new System.Drawing.Size(396, 175);
            pictureBox.TabIndex = 4;
            pictureBox.TabStop = false;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // flowLayoutPanel
            // 
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.Controls.Add(labelGraphTitle);
            flowLayoutPanel.Controls.Add(flowLayoutPanelMousePosition);
            flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanel.Name = "flowLayoutPanel";
            flowLayoutPanel.Size = new System.Drawing.Size(400, 21);
            flowLayoutPanel.TabIndex = 8;
            // 
            // flowLayoutPanelMousePosition
            // 
            flowLayoutPanelMousePosition.AutoSize = true;
            flowLayoutPanelMousePosition.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanelMousePosition.Controls.Add(labelX);
            flowLayoutPanelMousePosition.Controls.Add(labelXValue);
            flowLayoutPanelMousePosition.Controls.Add(labelBlank);
            flowLayoutPanelMousePosition.Controls.Add(labelY);
            flowLayoutPanelMousePosition.Controls.Add(labelYValue);
            flowLayoutPanelMousePosition.Location = new System.Drawing.Point(34, 3);
            flowLayoutPanelMousePosition.Name = "flowLayoutPanelMousePosition";
            flowLayoutPanelMousePosition.Size = new System.Drawing.Size(133, 15);
            flowLayoutPanelMousePosition.TabIndex = 8;
            // 
            // contextMenuStripY
            // 
            contextMenuStripY.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItemLogScaleX, toolStripMenuItemLogScaleY, toolStripSeparator1, toolStripMenuItemScaleLineX, toolStripMenuItemScaleLineY });
            contextMenuStripY.Name = "contextMenuStripY";
            contextMenuStripY.Size = new System.Drawing.Size(127, 98);
            // 
            // toolStripMenuItemLogScaleX
            // 
            toolStripMenuItemLogScaleX.Name = "toolStripMenuItemLogScaleX";
            toolStripMenuItemLogScaleX.Size = new System.Drawing.Size(126, 22);
            toolStripMenuItemLogScaleX.Text = "Log Scale";
            toolStripMenuItemLogScaleX.Click += toolStripMenuItemLogScaleX_Click;
            // 
            // toolStripMenuItemLogScaleY
            // 
            toolStripMenuItemLogScaleY.Name = "toolStripMenuItemLogScaleY";
            toolStripMenuItemLogScaleY.Size = new System.Drawing.Size(126, 22);
            toolStripMenuItemLogScaleY.Text = "Log Scale";
            toolStripMenuItemLogScaleY.Click += toolStripMenuItemLogScaleY_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // toolStripMenuItemScaleLineX
            // 
            toolStripMenuItemScaleLineX.Checked = true;
            toolStripMenuItemScaleLineX.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripMenuItemScaleLineX.Name = "toolStripMenuItemScaleLineX";
            toolStripMenuItemScaleLineX.Size = new System.Drawing.Size(126, 22);
            toolStripMenuItemScaleLineX.Text = "Scale Line";
            toolStripMenuItemScaleLineX.Click += toolStripMenuItemScaleLineX_Click;
            // 
            // toolStripMenuItemScaleLineY
            // 
            toolStripMenuItemScaleLineY.Checked = true;
            toolStripMenuItemScaleLineY.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripMenuItemScaleLineY.Name = "toolStripMenuItemScaleLineY";
            toolStripMenuItemScaleLineY.Size = new System.Drawing.Size(126, 22);
            toolStripMenuItemScaleLineY.Text = "Scale Line";
            toolStripMenuItemScaleLineY.Click += toolStripMenuItemScaleLineY_Click;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 179);
            this.panel1.TabIndex = 11;
            // 
            // GraphControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel1);
            Controls.Add(flowLayoutPanel);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "GraphControl";
            Size = new System.Drawing.Size(400, 200);
            Resize += GraphControl_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            flowLayoutPanel.ResumeLayout(false);
            flowLayoutPanel.PerformLayout();
            flowLayoutPanelMousePosition.ResumeLayout(false);
            flowLayoutPanelMousePosition.PerformLayout();
            contextMenuStripY.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelGraphTitle;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelYValue;
        private System.Windows.Forms.Label labelXValue;
        private System.Windows.Forms.Label labelBlank;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMousePosition;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripY;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogScaleX;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogScaleY;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScaleLineX;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScaleLineY;
        private System.Windows.Forms.Panel panel1;
    }
}
