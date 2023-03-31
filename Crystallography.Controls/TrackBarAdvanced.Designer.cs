namespace Crystallography.Controls
{
    partial class TrackBarAdvanced
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
            trackBar = new System.Windows.Forms.TrackBar();
            numericBox = new NumericBox();
            splitContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)trackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // trackBar
            // 
            trackBar.AutoSize = false;
            trackBar.LargeChange = 20000;
            trackBar.Location = new System.Drawing.Point(2, 0);
            trackBar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            trackBar.Maximum = 1000000;
            trackBar.Name = "trackBar";
            trackBar.Size = new System.Drawing.Size(166, 26);
            trackBar.SmallChange = 2000;
            trackBar.TabIndex = 1;
            trackBar.TickFrequency = 20000;
            trackBar.ValueChanged += trackBar_ValueChanged;
            // 
            // numericBox
            // 
            numericBox.BackColor = System.Drawing.SystemColors.Control;
            numericBox.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            numericBox.FooterBackColor = System.Drawing.SystemColors.Control;
            numericBox.FooterPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBox.HeaderBackColor = System.Drawing.SystemColors.Control;
            numericBox.HeaderPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBox.Location = new System.Drawing.Point(0, 1);
            numericBox.Margin = new System.Windows.Forms.Padding(0);
            numericBox.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBox.MinimumSize = new System.Drawing.Size(1, 20);
            numericBox.Name = "numericBox";
            numericBox.RoundErrorAccuracy = -1;
            numericBox.ShowUpDown = true;
            numericBox.Size = new System.Drawing.Size(84, 25);
            numericBox.SmartIncrement = true;
            numericBox.TabIndex = 2;
            numericBox.ThonsandsSeparator = true;
            numericBox.ValueChanged += numericBox_ValueChanged;
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            splitContainer.Location = new System.Drawing.Point(0, 0);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(numericBox);
            splitContainer.Panel1MinSize = 1;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(trackBar);
            splitContainer.Panel2MinSize = 1;
            splitContainer.Size = new System.Drawing.Size(256, 27);
            splitContainer.SplitterDistance = 84;
            splitContainer.SplitterWidth = 2;
            splitContainer.TabIndex = 3;
            // 
            // TrackBarAdvanced
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Controls.Add(splitContainer);
            Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "TrackBarAdvanced";
            Size = new System.Drawing.Size(256, 27);
            Load += TrackBarAdvanced_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar).EndInit();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private NumericBox numericBox;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}
