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
            if(Bmp!=null)
                Bmp.Dispose();
            if (DivisionTextBrush != null)
                DivisionTextBrush.Dispose();
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelYLabel = new System.Windows.Forms.Label();
            this.labelXValue = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelUpperText = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.contextMenuStripY = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemLogScaleX = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLogScaleY = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemScaleLineX = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemScaleLineY = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.flowLayoutPanel.SuspendLayout();
            this.contextMenuStripY.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.labelY);
            this.flowLayoutPanel2.Controls.Add(this.labelYLabel);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(69, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(79, 15);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "    ";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(28, 0);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(17, 15);
            this.labelY.TabIndex = 1;
            this.labelY.Text = "Y:";
            // 
            // labelYLabel
            // 
            this.labelYLabel.AutoSize = true;
            this.labelYLabel.Location = new System.Drawing.Point(51, 0);
            this.labelYLabel.Name = "labelYLabel";
            this.labelYLabel.Size = new System.Drawing.Size(25, 15);
            this.labelYLabel.TabIndex = 1;
            this.labelYLabel.Text = "000";
            // 
            // labelXValue
            // 
            this.labelXValue.AutoSize = true;
            this.labelXValue.Location = new System.Drawing.Point(32, 0);
            this.labelXValue.Name = "labelXValue";
            this.labelXValue.Size = new System.Drawing.Size(25, 15);
            this.labelXValue.TabIndex = 5;
            this.labelXValue.Text = "000";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.labelUpperText);
            this.flowLayoutPanel1.Controls.Add(this.labelX);
            this.flowLayoutPanel1.Controls.Add(this.labelXValue);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(60, 15);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // labelUpperText
            // 
            this.labelUpperText.AutoSize = true;
            this.labelUpperText.Location = new System.Drawing.Point(3, 0);
            this.labelUpperText.Name = "labelUpperText";
            this.labelUpperText.Size = new System.Drawing.Size(0, 15);
            this.labelUpperText.TabIndex = 8;
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(9, 0);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(17, 15);
            this.labelX.TabIndex = 1;
            this.labelX.Text = "X:";
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.White;
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(396, 175);
            this.pictureBox.TabIndex = 4;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDoubleClick);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutPanel.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(400, 21);
            this.flowLayoutPanel.TabIndex = 8;
            // 
            // contextMenuStripY
            // 
            this.contextMenuStripY.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemLogScaleX,
            this.toolStripMenuItemLogScaleY,
            this.toolStripSeparator1,
            this.toolStripMenuItemScaleLineX,
            this.toolStripMenuItemScaleLineY});
            this.contextMenuStripY.Name = "contextMenuStripY";
            this.contextMenuStripY.Size = new System.Drawing.Size(127, 98);
            // 
            // toolStripMenuItemLogScaleX
            // 
            this.toolStripMenuItemLogScaleX.Name = "toolStripMenuItemLogScaleX";
            this.toolStripMenuItemLogScaleX.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItemLogScaleX.Text = "Log Scale";
            this.toolStripMenuItemLogScaleX.Click += new System.EventHandler(this.toolStripMenuItemLogScaleX_Click);
            // 
            // toolStripMenuItemLogScaleY
            // 
            this.toolStripMenuItemLogScaleY.Name = "toolStripMenuItemLogScaleY";
            this.toolStripMenuItemLogScaleY.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItemLogScaleY.Text = "Log Scale";
            this.toolStripMenuItemLogScaleY.Click += new System.EventHandler(this.toolStripMenuItemLogScaleY_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // toolStripMenuItemScaleLineX
            // 
            this.toolStripMenuItemScaleLineX.Checked = true;
            this.toolStripMenuItemScaleLineX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemScaleLineX.Name = "toolStripMenuItemScaleLineX";
            this.toolStripMenuItemScaleLineX.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItemScaleLineX.Text = "Scale Line";
            this.toolStripMenuItemScaleLineX.Click += new System.EventHandler(this.toolStripMenuItemScaleLineX_Click);
            // 
            // toolStripMenuItemScaleLineY
            // 
            this.toolStripMenuItemScaleLineY.Checked = true;
            this.toolStripMenuItemScaleLineY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemScaleLineY.Name = "toolStripMenuItemScaleLineY";
            this.toolStripMenuItemScaleLineY.Size = new System.Drawing.Size(126, 22);
            this.toolStripMenuItemScaleLineY.Text = "Scale Line";
            this.toolStripMenuItemScaleLineY.Click += new System.EventHandler(this.toolStripMenuItemScaleLineY_Click);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "GraphControl";
            this.Size = new System.Drawing.Size(400, 200);
            this.Resize += new System.EventHandler(this.GraphControl_Resize);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            this.contextMenuStripY.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelYLabel;
        private System.Windows.Forms.Label labelXValue;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label labelUpperText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripY;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogScaleX;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogScaleY;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScaleLineX;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScaleLineY;
    }
}
