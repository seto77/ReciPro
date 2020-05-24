namespace Crystallography.Controls
{
    partial class SaclaControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericBoxPixelWidth = new Crystallography.Controls.NumericBox();
            this.numericBoxPixelHeight = new Crystallography.Controls.NumericBox();
            this.numericBoxPixelSize = new Crystallography.Controls.NumericBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericBoxFootY = new Crystallography.Controls.NumericBox();
            this.numericBoxFootX = new Crystallography.Controls.NumericBox();
            this.numericBoxDistance = new Crystallography.Controls.NumericBox();
            this.numericBoxTau = new Crystallography.Controls.NumericBox();
            this.numericBoxPhi = new Crystallography.Controls.NumericBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.label3.Location = new System.Drawing.Point(6, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Foot";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericBoxPixelWidth);
            this.groupBox1.Controls.Add(this.numericBoxPixelHeight);
            this.groupBox1.Controls.Add(this.numericBoxPixelSize);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detector property";
            // 
            // numericBoxPixelWidth
            // 
                       this.numericBoxPixelWidth.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelWidth.DecimalPlaces = -1;
            this.numericBoxPixelWidth.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
                        this.numericBoxPixelWidth.FooterText = "pixel";
                        this.numericBoxPixelWidth.HeaderText = "Width";
            this.numericBoxPixelWidth.Location = new System.Drawing.Point(7, 21);
            this.numericBoxPixelWidth.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelWidth.Maximum = double.PositiveInfinity;
            this.numericBoxPixelWidth.MaximumSize = new System.Drawing.Size(1000, 25);
                        this.numericBoxPixelWidth.MinimumSize = new System.Drawing.Size(1, 25);
                       this.numericBoxPixelWidth.Name = "numericBoxPixelWidth";
            this.numericBoxPixelWidth.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelWidth.RadianValue = 17.872171540421935D;
                        this.numericBoxPixelWidth.RestrictLimitValue = true;
            this.numericBoxPixelWidth.ShowFraction = false;
            this.numericBoxPixelWidth.ShowPositiveSign = false;
                        this.numericBoxPixelWidth.Size = new System.Drawing.Size(122, 25);
            this.numericBoxPixelWidth.SkipEventDuringInput = false;
            this.numericBoxPixelWidth.SmartIncrement = true;
            this.numericBoxPixelWidth.TabIndex = 0;
                        this.numericBoxPixelWidth.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
                        this.numericBoxPixelWidth.ThonsandsSeparator = true;
            this.numericBoxPixelWidth.ToolTip = "";
            this.numericBoxPixelWidth.UpDown_Increment = 1D;
            this.numericBoxPixelWidth.Value = 1024D;
                        this.numericBoxPixelWidth.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxPixelHeight
            // 
                       this.numericBoxPixelHeight.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelHeight.DecimalPlaces = -1;
            this.numericBoxPixelHeight.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
                        this.numericBoxPixelHeight.FooterText = "pixel";
                        this.numericBoxPixelHeight.HeaderText = "Height";
            this.numericBoxPixelHeight.Location = new System.Drawing.Point(7, 46);
            this.numericBoxPixelHeight.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelHeight.Maximum = double.PositiveInfinity;
            this.numericBoxPixelHeight.MaximumSize = new System.Drawing.Size(1000, 25);
                        this.numericBoxPixelHeight.MinimumSize = new System.Drawing.Size(1, 25);
                       this.numericBoxPixelHeight.Name = "numericBoxPixelHeight";
            this.numericBoxPixelHeight.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelHeight.RadianValue = 17.872171540421935D;
                        this.numericBoxPixelHeight.RestrictLimitValue = true;
            this.numericBoxPixelHeight.ShowFraction = false;
            this.numericBoxPixelHeight.ShowPositiveSign = false;
                        this.numericBoxPixelHeight.Size = new System.Drawing.Size(122, 25);
            this.numericBoxPixelHeight.SkipEventDuringInput = false;
            this.numericBoxPixelHeight.SmartIncrement = true;
            this.numericBoxPixelHeight.TabIndex = 0;
                        this.numericBoxPixelHeight.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
                        this.numericBoxPixelHeight.ThonsandsSeparator = true;
            this.numericBoxPixelHeight.ToolTip = "";
            this.numericBoxPixelHeight.UpDown_Increment = 1D;
            this.numericBoxPixelHeight.Value = 1024D;
                        this.numericBoxPixelHeight.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxPixelSize
            // 
                       this.numericBoxPixelSize.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelSize.DecimalPlaces = -1;
            this.numericBoxPixelSize.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
                        this.numericBoxPixelSize.FooterText = "μm";
                        this.numericBoxPixelSize.HeaderText = "Pix. size";
            this.numericBoxPixelSize.Location = new System.Drawing.Point(7, 71);
            this.numericBoxPixelSize.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelSize.Maximum = double.PositiveInfinity;
            this.numericBoxPixelSize.MaximumSize = new System.Drawing.Size(1000, 25);
                        this.numericBoxPixelSize.MinimumSize = new System.Drawing.Size(1, 25);
                       this.numericBoxPixelSize.Name = "numericBoxPixelSize";
            this.numericBoxPixelSize.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelSize.RadianValue = 0.0008726646259971648D;
                        this.numericBoxPixelSize.RestrictLimitValue = true;
            this.numericBoxPixelSize.ShowFraction = false;
            this.numericBoxPixelSize.ShowPositiveSign = false;
                        this.numericBoxPixelSize.Size = new System.Drawing.Size(122, 25);
            this.numericBoxPixelSize.SkipEventDuringInput = false;
            this.numericBoxPixelSize.SmartIncrement = true;
            this.numericBoxPixelSize.TabIndex = 0;
                        this.numericBoxPixelSize.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
                        this.numericBoxPixelSize.ThonsandsSeparator = true;
            this.numericBoxPixelSize.ToolTip = "";
            this.numericBoxPixelSize.UpDown_Increment = 1D;
            this.numericBoxPixelSize.Value = 0.05D;
                        this.numericBoxPixelSize.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericBoxFootY);
            this.groupBox2.Controls.Add(this.numericBoxFootX);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericBoxDistance);
            this.groupBox2.Controls.Add(this.numericBoxPhi);
            this.groupBox2.Controls.Add(this.numericBoxTau);
            this.groupBox2.Location = new System.Drawing.Point(137, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(235, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Optical property";
            // 
            // numericBoxFootY
            // 
                       this.numericBoxFootY.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootY.DecimalPlaces = -1;
            this.numericBoxFootY.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
                        this.numericBoxFootY.FooterText = "pix";
                        this.numericBoxFootY.HeaderText = "y";
            this.numericBoxFootY.Location = new System.Drawing.Point(147, 72);
            this.numericBoxFootY.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxFootY.Maximum = double.PositiveInfinity;
            this.numericBoxFootY.MaximumSize = new System.Drawing.Size(1000, 25);
                        this.numericBoxFootY.MinimumSize = new System.Drawing.Size(1, 25);
                       this.numericBoxFootY.Name = "numericBoxFootY";
            this.numericBoxFootY.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxFootY.RadianValue = 8.9360857702109673D;
                        this.numericBoxFootY.RestrictLimitValue = true;
            
            this.numericBoxFootY.ShowPositiveSign = false;
                        this.numericBoxFootY.Size = new System.Drawing.Size(88, 25);
            this.numericBoxFootY.SkipEventDuringInput = false;
            this.numericBoxFootY.SmartIncrement = true;
            this.numericBoxFootY.TabIndex = 0;
                        this.numericBoxFootY.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
                        this.numericBoxFootY.ThonsandsSeparator = true;
            this.numericBoxFootY.ToolTip = "";
            this.numericBoxFootY.UpDown_Increment = 1D;
            this.numericBoxFootY.Value = 512D;
                        this.numericBoxFootY.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxFootX
            // 
                       this.numericBoxFootX.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootX.DecimalPlaces = -1;
            this.numericBoxFootX.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
                        this.numericBoxFootX.FooterText = "pix";
                        this.numericBoxFootX.HeaderText = "x";
            this.numericBoxFootX.Location = new System.Drawing.Point(53, 72);
            this.numericBoxFootX.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxFootX.Maximum = double.PositiveInfinity;
            this.numericBoxFootX.MaximumSize = new System.Drawing.Size(1000, 25);
                        this.numericBoxFootX.MinimumSize = new System.Drawing.Size(1, 25);
                       this.numericBoxFootX.Name = "numericBoxFootX";
            this.numericBoxFootX.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxFootX.RadianValue = 8.9360857702109673D;
                        this.numericBoxFootX.RestrictLimitValue = true;
            this.numericBoxFootX.ShowFraction = false;
            this.numericBoxFootX.ShowPositiveSign = false;
                        this.numericBoxFootX.Size = new System.Drawing.Size(88, 25);
            this.numericBoxFootX.SkipEventDuringInput = false;
            this.numericBoxFootX.SmartIncrement = true;
            this.numericBoxFootX.TabIndex = 0;
                        this.numericBoxFootX.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
                        this.numericBoxFootX.ThonsandsSeparator = true;
            this.numericBoxFootX.ToolTip = "";
            this.numericBoxFootX.UpDown_Increment = 1D;
            this.numericBoxFootX.Value = 512D;
                        this.numericBoxFootX.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxDistance
            // 
                       this.numericBoxDistance.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.DecimalPlaces = -1;
            this.numericBoxDistance.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
                        this.numericBoxDistance.FooterText = "mm";
                        this.numericBoxDistance.HeaderText = "Cameralength 2";
            this.numericBoxDistance.Location = new System.Drawing.Point(3, 21);
            this.numericBoxDistance.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDistance.Maximum = double.PositiveInfinity;
            this.numericBoxDistance.MaximumSize = new System.Drawing.Size(1000, 25);
                        this.numericBoxDistance.MinimumSize = new System.Drawing.Size(1, 25);
                       this.numericBoxDistance.Name = "numericBoxDistance";
            this.numericBoxDistance.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDistance.RadianValue = 5.2359877559829888D;
                        this.numericBoxDistance.RestrictLimitValue = true;
            this.numericBoxDistance.ShowFraction = false;
            this.numericBoxDistance.ShowPositiveSign = false;
                        this.numericBoxDistance.Size = new System.Drawing.Size(187, 25);
            this.numericBoxDistance.SkipEventDuringInput = false;
            this.numericBoxDistance.SmartIncrement = true;
            this.numericBoxDistance.TabIndex = 0;
                        this.numericBoxDistance.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
                        this.numericBoxDistance.ThonsandsSeparator = true;
            this.numericBoxDistance.ToolTip = "";
            this.numericBoxDistance.UpDown_Increment = 1D;
            this.numericBoxDistance.Value = 300D;
                        this.numericBoxDistance.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxTau
            // 
                       this.numericBoxTau.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTau.DecimalPlaces = -1;
            this.numericBoxTau.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
                        this.numericBoxTau.FooterText = "°";
                        this.numericBoxTau.HeaderText = "τ";
            this.numericBoxTau.Location = new System.Drawing.Point(9, 46);
            this.numericBoxTau.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxTau.Maximum = 90D;
            this.numericBoxTau.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxTau.Minimum = -90D;
            this.numericBoxTau.MinimumSize = new System.Drawing.Size(1, 25);
                       this.numericBoxTau.Name = "numericBoxTau";
            this.numericBoxTau.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxTau.RadianValue = 0.3490658503988659D;
                        this.numericBoxTau.RestrictLimitValue = true;
            this.numericBoxTau.ShowFraction = false;
            this.numericBoxTau.ShowPositiveSign = false;
                        this.numericBoxTau.Size = new System.Drawing.Size(83, 25);
            this.numericBoxTau.SkipEventDuringInput = false;
            this.numericBoxTau.SmartIncrement = true;
            this.numericBoxTau.TabIndex = 0;
                        this.numericBoxTau.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
                        this.numericBoxTau.ThonsandsSeparator = true;
            this.numericBoxTau.ToolTip = "";
            this.numericBoxTau.UpDown_Increment = 1D;
            this.numericBoxTau.Value = 20D;
                        this.numericBoxTau.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxPhi
            // 
                       this.numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.DecimalPlaces = -1;
            this.numericBoxPhi.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
                        this.numericBoxPhi.FooterText = "°";
                        this.numericBoxPhi.HeaderText = "φ";
            this.numericBoxPhi.Location = new System.Drawing.Point(107, 47);
            this.numericBoxPhi.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPhi.Maximum = 360D;
            this.numericBoxPhi.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPhi.Minimum = -360D;
            this.numericBoxPhi.MinimumSize = new System.Drawing.Size(1, 25);
                       this.numericBoxPhi.Name = "numericBoxPhi";
            this.numericBoxPhi.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPhi.RadianValue = 0D;
                        this.numericBoxPhi.RestrictLimitValue = true;
            this.numericBoxPhi.ShowFraction = false;
            this.numericBoxPhi.ShowPositiveSign = false;
                        this.numericBoxPhi.Size = new System.Drawing.Size(83, 25);
            this.numericBoxPhi.SkipEventDuringInput = false;
            this.numericBoxPhi.SmartIncrement = true;
            this.numericBoxPhi.TabIndex = 0;
                        this.numericBoxPhi.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
                        this.numericBoxPhi.ThonsandsSeparator = true;
            this.numericBoxPhi.ToolTip = "";
            this.numericBoxPhi.UpDown_Increment = 1D;
                                    this.numericBoxPhi.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // SaclaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SaclaControl";
            this.Size = new System.Drawing.Size(375, 103);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NumericBox numericBoxPixelWidth;
        private NumericBox numericBoxPixelHeight;
        private NumericBox numericBoxPixelSize;
        private NumericBox numericBoxTau;
        private NumericBox numericBoxFootX;
        private NumericBox numericBoxFootY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private NumericBox numericBoxDistance;
        private NumericBox numericBoxPhi;
    }
}
