namespace Crystallography.Controls
{
    partial class GraphControl
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
            toolTip = new System.Windows.Forms.ToolTip(components);
            numericBoxXMin = new NumericBox();
            numericBoxXMax = new NumericBox();
            numericBoxYMin = new NumericBox();
            numericBoxYMax = new NumericBox();
            buttonCopy = new System.Windows.Forms.Button();
            labelBlank = new System.Windows.Forms.Label();
            labelY1 = new System.Windows.Forms.Label();
            labelYValue = new System.Windows.Forms.Label();
            labelXValue = new System.Windows.Forms.Label();
            labelGraphTitle = new System.Windows.Forms.Label();
            labelX1 = new System.Windows.Forms.Label();
            pictureBox = new System.Windows.Forms.PictureBox();
            flowLayoutPanelMousePosition = new System.Windows.Forms.FlowLayoutPanel();
            contextMenuStripY = new System.Windows.Forms.ContextMenuStrip(components);
            toolStripMenuItemLogScaleX = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemLogScaleY = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItemScaleLineX = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItemScaleLineY = new System.Windows.Forms.ToolStripMenuItem();
            panelMain = new System.Windows.Forms.Panel();
            flowLayoutPanelRange = new System.Windows.Forms.FlowLayoutPanel();
            label1 = new System.Windows.Forms.Label();
            labelX2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            labelY2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            panelTitleAndMouse = new System.Windows.Forms.Panel();
            panelRangeAndCopy = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            flowLayoutPanelMousePosition.SuspendLayout();
            contextMenuStripY.SuspendLayout();
            panelMain.SuspendLayout();
            flowLayoutPanelRange.SuspendLayout();
            panelTitleAndMouse.SuspendLayout();
            panelRangeAndCopy.SuspendLayout();
            SuspendLayout();
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 10000;
            toolTip.InitialDelay = 500;
            toolTip.IsBalloon = true;
            toolTip.ReshowDelay = 100;
            // 
            // numericBoxXMin
            // 
            numericBoxXMin.BackColor = System.Drawing.Color.Transparent;
            numericBoxXMin.FooterFont = new System.Drawing.Font("Segoe UI", 9.5F);
            numericBoxXMin.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxXMin.HeaderFont = new System.Drawing.Font("Segoe UI", 9.5F);
            numericBoxXMin.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxXMin.Location = new System.Drawing.Point(84, 0);
            numericBoxXMin.Margin = new System.Windows.Forms.Padding(0);
            numericBoxXMin.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxXMin.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxXMin.Name = "numericBoxXMin";
            numericBoxXMin.ShowUpDown = true;
            numericBoxXMin.Size = new System.Drawing.Size(66, 26);
            numericBoxXMin.SmartIncrement = true;
            numericBoxXMin.TabIndex = 0;
            toolTip.SetToolTip(numericBoxXMin, "Minimum of the X-axis plot range (left edge).\r\nOn a log axis, enter the real (non-log) value.");
            numericBoxXMin.ValueFontSize = 9.5F;
            numericBoxXMin.ValueChanged += numericBoxRange_ValueChanged;
            // 
            // numericBoxXMax
            // 
            numericBoxXMax.BackColor = System.Drawing.Color.Transparent;
            numericBoxXMax.FooterFont = new System.Drawing.Font("Segoe UI", 9.5F);
            numericBoxXMax.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxXMax.HeaderFont = new System.Drawing.Font("Segoe UI", 9.5F);
            numericBoxXMax.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxXMax.Location = new System.Drawing.Point(169, 0);
            numericBoxXMax.Margin = new System.Windows.Forms.Padding(0);
            numericBoxXMax.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxXMax.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxXMax.Name = "numericBoxXMax";
            numericBoxXMax.ShowUpDown = true;
            numericBoxXMax.Size = new System.Drawing.Size(66, 26);
            numericBoxXMax.SmartIncrement = true;
            numericBoxXMax.TabIndex = 0;
            toolTip.SetToolTip(numericBoxXMax, "Maximum of the X-axis plot range (right edge).\r\nOn a log axis, enter the real (non-log) value.");
            numericBoxXMax.ValueFontSize = 9.5F;
            numericBoxXMax.ValueChanged += numericBoxRange_ValueChanged;
            // 
            // numericBoxYMin
            // 
            numericBoxYMin.BackColor = System.Drawing.Color.Transparent;
            numericBoxYMin.FooterFont = new System.Drawing.Font("Segoe UI", 9.5F);
            numericBoxYMin.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxYMin.HeaderFont = new System.Drawing.Font("Segoe UI", 9.5F);
            numericBoxYMin.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxYMin.Location = new System.Drawing.Point(284, 0);
            numericBoxYMin.Margin = new System.Windows.Forms.Padding(0);
            numericBoxYMin.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxYMin.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxYMin.Name = "numericBoxYMin";
            numericBoxYMin.ShowUpDown = true;
            numericBoxYMin.Size = new System.Drawing.Size(66, 26);
            numericBoxYMin.SmartIncrement = true;
            numericBoxYMin.TabIndex = 0;
            toolTip.SetToolTip(numericBoxYMin, "Minimum of the Y-axis plot range (bottom edge).\r\nOn a log axis, enter the real (non-log) value.");
            numericBoxYMin.ValueFontSize = 9.5F;
            numericBoxYMin.ValueChanged += numericBoxRange_ValueChanged;
            // 
            // numericBoxYMax
            // 
            numericBoxYMax.BackColor = System.Drawing.Color.Transparent;
            numericBoxYMax.FooterFont = new System.Drawing.Font("Segoe UI", 9.5F);
            numericBoxYMax.FooterPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxYMax.HeaderFont = new System.Drawing.Font("Segoe UI", 9.5F);
            numericBoxYMax.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxYMax.Location = new System.Drawing.Point(369, 0);
            numericBoxYMax.Margin = new System.Windows.Forms.Padding(0);
            numericBoxYMax.MaximumSize = new System.Drawing.Size(1000, 100);
            numericBoxYMax.MinimumSize = new System.Drawing.Size(10, 20);
            numericBoxYMax.Name = "numericBoxYMax";
            numericBoxYMax.ShowUpDown = true;
            numericBoxYMax.Size = new System.Drawing.Size(66, 26);
            numericBoxYMax.SmartIncrement = true;
            numericBoxYMax.TabIndex = 0;
            toolTip.SetToolTip(numericBoxYMax, "Maximum of the Y-axis plot range (top edge).\r\nOn a log axis, enter the real (non-log) value.");
            numericBoxYMax.ValueFontSize = 9.5F;
            numericBoxYMax.ValueChanged += numericBoxRange_ValueChanged;
            // 
            // buttonCopy
            // 
            buttonCopy.AutoSize = true;
            buttonCopy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            buttonCopy.Dock = System.Windows.Forms.DockStyle.Right;
            buttonCopy.Location = new System.Drawing.Point(478, 0);
            buttonCopy.Margin = new System.Windows.Forms.Padding(0);
            buttonCopy.Name = "buttonCopy";
            buttonCopy.Size = new System.Drawing.Size(45, 25);
            buttonCopy.TabIndex = 13;
            buttonCopy.Text = "Copy";
            toolTip.SetToolTip(buttonCopy, "Copy the graph area to the clipboard as a vector image\r\n(Enhanced Metafile, EMF). Paste it into Word, PowerPoint,\r\nIllustrator, etc.; it stays sharp at any size, unlike a bitmap.");
            buttonCopy.UseVisualStyleBackColor = true;
            buttonCopy.Visible = false;
            buttonCopy.Click += buttonCopy_Click;
            // 
            // labelBlank
            // 
            labelBlank.AutoSize = true;
            labelBlank.Location = new System.Drawing.Point(73, 0);
            labelBlank.Name = "labelBlank";
            labelBlank.Size = new System.Drawing.Size(24, 17);
            labelBlank.TabIndex = 8;
            labelBlank.Text = "    ";
            // 
            // labelY1
            // 
            labelY1.AutoSize = true;
            labelY1.Location = new System.Drawing.Point(103, 0);
            labelY1.Name = "labelY1";
            labelY1.Size = new System.Drawing.Size(18, 17);
            labelY1.TabIndex = 1;
            labelY1.Text = "Y:";
            // 
            // labelYValue
            // 
            labelYValue.AutoSize = true;
            labelYValue.Location = new System.Drawing.Point(127, 0);
            labelYValue.Name = "labelYValue";
            labelYValue.Size = new System.Drawing.Size(29, 17);
            labelYValue.TabIndex = 1;
            labelYValue.Text = "000";
            // 
            // labelXValue
            // 
            labelXValue.AutoSize = true;
            labelXValue.Location = new System.Drawing.Point(38, 0);
            labelXValue.Name = "labelXValue";
            labelXValue.Size = new System.Drawing.Size(29, 17);
            labelXValue.TabIndex = 5;
            labelXValue.Text = "000";
            // 
            // labelGraphTitle
            // 
            labelGraphTitle.AutoSize = true;
            labelGraphTitle.Dock = System.Windows.Forms.DockStyle.Left;
            labelGraphTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            labelGraphTitle.Location = new System.Drawing.Point(0, 0);
            labelGraphTitle.Name = "labelGraphTitle";
            labelGraphTitle.Size = new System.Drawing.Size(0, 17);
            labelGraphTitle.TabIndex = 8;
            // 
            // labelX1
            // 
            labelX1.AutoSize = true;
            labelX1.Location = new System.Drawing.Point(13, 0);
            labelX1.Name = "labelX1";
            labelX1.Size = new System.Drawing.Size(19, 17);
            labelX1.TabIndex = 1;
            labelX1.Text = "X:";
            // 
            // pictureBox
            // 
            pictureBox.BackColor = System.Drawing.Color.White;
            pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            pictureBox.Location = new System.Drawing.Point(0, 0);
            pictureBox.Margin = new System.Windows.Forms.Padding(0);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new System.Drawing.Size(519, 146);
            pictureBox.TabIndex = 4;
            pictureBox.TabStop = false;
            pictureBox.Paint += pictureBox_Paint;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.MouseMove += pictureBox_MouseMove;
            pictureBox.MouseUp += pictureBox_MouseUp;
            // 
            // flowLayoutPanelMousePosition
            // 
            flowLayoutPanelMousePosition.AutoSize = true;
            flowLayoutPanelMousePosition.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanelMousePosition.Controls.Add(labelX1);
            flowLayoutPanelMousePosition.Controls.Add(labelXValue);
            flowLayoutPanelMousePosition.Controls.Add(labelBlank);
            flowLayoutPanelMousePosition.Controls.Add(labelY1);
            flowLayoutPanelMousePosition.Controls.Add(labelYValue);
            flowLayoutPanelMousePosition.Dock = System.Windows.Forms.DockStyle.Fill;
            flowLayoutPanelMousePosition.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            flowLayoutPanelMousePosition.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanelMousePosition.Name = "flowLayoutPanelMousePosition";
            flowLayoutPanelMousePosition.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            flowLayoutPanelMousePosition.Size = new System.Drawing.Size(523, 25);
            flowLayoutPanelMousePosition.TabIndex = 8;
            flowLayoutPanelMousePosition.Visible = false;
            flowLayoutPanelMousePosition.WrapContents = false;
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
            toolStripMenuItemLogScaleX.ToolTipText = "Toggle the X axis between linear\r\nand base-10 logarithmic scale.";
            toolStripMenuItemLogScaleX.Click += toolStripMenuItemLogScaleX_Click;
            // 
            // toolStripMenuItemLogScaleY
            // 
            toolStripMenuItemLogScaleY.Name = "toolStripMenuItemLogScaleY";
            toolStripMenuItemLogScaleY.Size = new System.Drawing.Size(126, 22);
            toolStripMenuItemLogScaleY.Text = "Log Scale";
            toolStripMenuItemLogScaleY.ToolTipText = "Toggle the Y axis between linear\r\nand base-10 logarithmic scale.";
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
            toolStripMenuItemScaleLineX.ToolTipText = "Show or hide the vertical gridlines\r\ndrawn at the X-axis tick marks.";
            toolStripMenuItemScaleLineX.Click += toolStripMenuItemScaleLineX_Click;
            // 
            // toolStripMenuItemScaleLineY
            // 
            toolStripMenuItemScaleLineY.Checked = true;
            toolStripMenuItemScaleLineY.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripMenuItemScaleLineY.Name = "toolStripMenuItemScaleLineY";
            toolStripMenuItemScaleLineY.Size = new System.Drawing.Size(126, 22);
            toolStripMenuItemScaleLineY.Text = "Scale Line";
            toolStripMenuItemScaleLineY.ToolTipText = "Show or hide the horizontal gridlines\r\ndrawn at the Y-axis tick marks.";
            toolStripMenuItemScaleLineY.Click += toolStripMenuItemScaleLineY_Click;
            // 
            // panelMain
            // 
            panelMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            panelMain.Controls.Add(pictureBox);
            panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            panelMain.Location = new System.Drawing.Point(0, 50);
            panelMain.Name = "panelMain";
            panelMain.Size = new System.Drawing.Size(523, 150);
            panelMain.TabIndex = 11;
            // 
            // flowLayoutPanelRange
            // 
            flowLayoutPanelRange.AutoSize = true;
            flowLayoutPanelRange.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            flowLayoutPanelRange.Controls.Add(label1);
            flowLayoutPanelRange.Controls.Add(labelX2);
            flowLayoutPanelRange.Controls.Add(numericBoxXMin);
            flowLayoutPanelRange.Controls.Add(label3);
            flowLayoutPanelRange.Controls.Add(numericBoxXMax);
            flowLayoutPanelRange.Controls.Add(label6);
            flowLayoutPanelRange.Controls.Add(labelY2);
            flowLayoutPanelRange.Controls.Add(numericBoxYMin);
            flowLayoutPanelRange.Controls.Add(label4);
            flowLayoutPanelRange.Controls.Add(numericBoxYMax);
            flowLayoutPanelRange.Dock = System.Windows.Forms.DockStyle.Left;
            flowLayoutPanelRange.Location = new System.Drawing.Point(0, 0);
            flowLayoutPanelRange.Name = "flowLayoutPanelRange";
            flowLayoutPanelRange.Size = new System.Drawing.Size(435, 25);
            flowLayoutPanelRange.TabIndex = 12;
            flowLayoutPanelRange.Visible = false;
            flowLayoutPanelRange.WrapContents = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            label1.Location = new System.Drawing.Point(3, 3);
            label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(53, 17);
            label1.TabIndex = 1;
            label1.Text = "Range  ";
            // 
            // labelX2
            // 
            labelX2.AutoSize = true;
            labelX2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            labelX2.Location = new System.Drawing.Point(62, 3);
            labelX2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            labelX2.Name = "labelX2";
            labelX2.Size = new System.Drawing.Size(19, 17);
            labelX2.TabIndex = 1;
            labelX2.Text = "X:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            label3.Location = new System.Drawing.Point(153, 3);
            label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(13, 17);
            label3.TabIndex = 1;
            label3.Text = "-";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(238, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(19, 15);
            label6.TabIndex = 8;
            label6.Text = "    ";
            // 
            // labelY2
            // 
            labelY2.AutoSize = true;
            labelY2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            labelY2.Location = new System.Drawing.Point(263, 3);
            labelY2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            labelY2.Name = "labelY2";
            labelY2.Size = new System.Drawing.Size(18, 17);
            labelY2.TabIndex = 1;
            labelY2.Text = "Y:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            label4.Location = new System.Drawing.Point(353, 3);
            label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(13, 17);
            label4.TabIndex = 1;
            label4.Text = "-";
            // 
            // panelTitleAndMouse
            // 
            panelTitleAndMouse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panelTitleAndMouse.Controls.Add(flowLayoutPanelMousePosition);
            panelTitleAndMouse.Controls.Add(labelGraphTitle);
            panelTitleAndMouse.Dock = System.Windows.Forms.DockStyle.Top;
            panelTitleAndMouse.Location = new System.Drawing.Point(0, 0);
            panelTitleAndMouse.Margin = new System.Windows.Forms.Padding(0);
            panelTitleAndMouse.Name = "panelTitleAndMouse";
            panelTitleAndMouse.Size = new System.Drawing.Size(523, 25);
            panelTitleAndMouse.TabIndex = 14;
            panelTitleAndMouse.Visible = false;
            // 
            // panelRangeAndCopy
            // 
            panelRangeAndCopy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            panelRangeAndCopy.Controls.Add(flowLayoutPanelRange);
            panelRangeAndCopy.Controls.Add(buttonCopy);
            panelRangeAndCopy.Dock = System.Windows.Forms.DockStyle.Top;
            panelRangeAndCopy.Location = new System.Drawing.Point(0, 25);
            panelRangeAndCopy.Name = "panelRangeAndCopy";
            panelRangeAndCopy.Size = new System.Drawing.Size(523, 25);
            panelRangeAndCopy.TabIndex = 15;
            panelRangeAndCopy.Visible = false;
            // 
            // GraphControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Controls.Add(panelMain);
            Controls.Add(panelRangeAndCopy);
            Controls.Add(panelTitleAndMouse);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            Name = "GraphControl";
            Size = new System.Drawing.Size(523, 200);
            Resize += GraphControl_Resize;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            flowLayoutPanelMousePosition.ResumeLayout(false);
            flowLayoutPanelMousePosition.PerformLayout();
            contextMenuStripY.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            flowLayoutPanelRange.ResumeLayout(false);
            flowLayoutPanelRange.PerformLayout();
            panelTitleAndMouse.ResumeLayout(false);
            panelTitleAndMouse.PerformLayout();
            panelRangeAndCopy.ResumeLayout(false);
            panelRangeAndCopy.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)

        private System.Windows.Forms.Label labelGraphTitle;
        private System.Windows.Forms.Label labelX1;
        private System.Windows.Forms.Label labelY1;
        private System.Windows.Forms.Label labelYValue;
        private System.Windows.Forms.Label labelXValue;
        private System.Windows.Forms.Label labelBlank;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMousePosition;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripY;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogScaleX;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLogScaleY;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScaleLineX;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScaleLineY;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRange;
        private NumericBox numericBoxXMin;
        private NumericBox numericBoxXMax;
        private NumericBox numericBoxYMin;
        private NumericBox numericBoxYMax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelY2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelX2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Panel panelTitleAndMouse;
        private System.Windows.Forms.Panel panelRangeAndCopy;
    }
}
