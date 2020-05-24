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
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.numericBox = new Crystallography.Controls.NumericBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar
            // 
            this.trackBar.AutoSize = false;
            this.trackBar.LargeChange = 20000;
            this.trackBar.Location = new System.Drawing.Point(2, 0);
            this.trackBar.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.trackBar.Maximum = 1000000;
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(166, 26);
            this.trackBar.SmallChange = 2000;
            this.trackBar.TabIndex = 1;
            this.trackBar.TickFrequency = 20000;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            // 
            // numericBox
            // 
                       this.numericBox.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox.DecimalPlaces = -2;
            this.numericBox.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
                                                this.numericBox.HeaderText = "";
            this.numericBox.Location = new System.Drawing.Point(0, 1);
            this.numericBox.Margin = new System.Windows.Forms.Padding(0);
            this.numericBox.Maximum = double.PositiveInfinity;
            this.numericBox.MaximumSize = new System.Drawing.Size(1000, 25);
                        this.numericBox.MinimumSize = new System.Drawing.Size(1, 25);
                       this.numericBox.Name = "numericBox";
            this.numericBox.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBox.RadianValue = 0D;
                        this.numericBox.RestrictLimitValue = true;
            this.numericBox.ShowFraction = false;
            this.numericBox.ShowPositiveSign = false;
            this.numericBox.ShowUpDown = true;
            this.numericBox.Size = new System.Drawing.Size(84, 25);
            this.numericBox.SkipEventDuringInput = true;
            this.numericBox.SmartIncrement = true;
            this.numericBox.TabIndex = 2;
                        this.numericBox.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
                        this.numericBox.ThonsandsSeparator = true;
            this.numericBox.ToolTip = "";
            this.numericBox.UpDown_Increment = 1D;
                                    this.numericBox.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBox_ValueChanged);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.numericBox);
            this.splitContainer.Panel1MinSize = 1;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.trackBar);
            this.splitContainer.Panel2MinSize = 1;
            this.splitContainer.Size = new System.Drawing.Size(256, 27);
            this.splitContainer.SplitterDistance = 84;
            this.splitContainer.SplitterWidth = 2;
            this.splitContainer.TabIndex = 3;
            // 
            // TrackBarAdvanced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TrackBarAdvanced";
            this.Size = new System.Drawing.Size(256, 27);
            this.Load += new System.EventHandler(this.TrackBarAdvanced_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar;
        private NumericBox numericBox;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}
