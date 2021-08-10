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
            this.numericBoxPhi = new Crystallography.Controls.NumericBox();
            this.numericBoxTau = new Crystallography.Controls.NumericBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(6, 77);
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
            this.numericBoxPixelWidth.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPixelWidth.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelWidth.FooterText = "pixel";
            this.numericBoxPixelWidth.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelWidth.HeaderText = "Width";
            this.numericBoxPixelWidth.Location = new System.Drawing.Point(7, 21);
            this.numericBoxPixelWidth.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelWidth.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPixelWidth.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPixelWidth.Name = "numericBoxPixelWidth";
            this.numericBoxPixelWidth.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelWidth.RadianValue = 17.872171540421935D;
            this.numericBoxPixelWidth.RoundErrorAccuracy = -1;
            this.numericBoxPixelWidth.Size = new System.Drawing.Size(122, 25);
            this.numericBoxPixelWidth.SkipEventDuringInput = false;
            this.numericBoxPixelWidth.SmartIncrement = true;
            this.numericBoxPixelWidth.TabIndex = 0;
            this.numericBoxPixelWidth.ThonsandsSeparator = true;
            this.numericBoxPixelWidth.Value = 1024D;
            this.numericBoxPixelWidth.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxPixelHeight
            // 
            this.numericBoxPixelHeight.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelHeight.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPixelHeight.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelHeight.FooterText = "pixel";
            this.numericBoxPixelHeight.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelHeight.HeaderText = "Height";
            this.numericBoxPixelHeight.Location = new System.Drawing.Point(7, 46);
            this.numericBoxPixelHeight.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelHeight.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPixelHeight.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPixelHeight.Name = "numericBoxPixelHeight";
            this.numericBoxPixelHeight.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelHeight.RadianValue = 17.872171540421935D;
            this.numericBoxPixelHeight.RoundErrorAccuracy = -1;
            this.numericBoxPixelHeight.Size = new System.Drawing.Size(122, 25);
            this.numericBoxPixelHeight.SkipEventDuringInput = false;
            this.numericBoxPixelHeight.SmartIncrement = true;
            this.numericBoxPixelHeight.TabIndex = 0;
            this.numericBoxPixelHeight.ThonsandsSeparator = true;
            this.numericBoxPixelHeight.Value = 1024D;
            this.numericBoxPixelHeight.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxPixelSize
            // 
            this.numericBoxPixelSize.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelSize.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPixelSize.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelSize.FooterText = "mm";
            this.numericBoxPixelSize.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPixelSize.HeaderText = "Pix. size";
            this.numericBoxPixelSize.Location = new System.Drawing.Point(7, 71);
            this.numericBoxPixelSize.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPixelSize.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPixelSize.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPixelSize.Name = "numericBoxPixelSize";
            this.numericBoxPixelSize.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPixelSize.RadianValue = 0.0008726646259971648D;
            this.numericBoxPixelSize.RoundErrorAccuracy = -1;
            this.numericBoxPixelSize.Size = new System.Drawing.Size(122, 25);
            this.numericBoxPixelSize.SkipEventDuringInput = false;
            this.numericBoxPixelSize.SmartIncrement = true;
            this.numericBoxPixelSize.TabIndex = 0;
            this.numericBoxPixelSize.ThonsandsSeparator = true;
            this.numericBoxPixelSize.Value = 0.05D;
            this.numericBoxPixelSize.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericBoxFootY);
            this.groupBox2.Controls.Add(this.numericBoxFootX);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numericBoxPhi);
            this.groupBox2.Controls.Add(this.numericBoxTau);
            this.groupBox2.Controls.Add(this.numericBoxDistance);
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
            this.numericBoxFootY.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxFootY.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootY.FooterText = "pix";
            this.numericBoxFootY.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootY.HeaderText = "y";
            this.numericBoxFootY.Location = new System.Drawing.Point(147, 72);
            this.numericBoxFootY.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxFootY.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxFootY.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxFootY.Name = "numericBoxFootY";
            this.numericBoxFootY.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxFootY.RadianValue = 8.9360857702109673D;
            this.numericBoxFootY.RoundErrorAccuracy = 10;
            this.numericBoxFootY.Size = new System.Drawing.Size(88, 25);
            this.numericBoxFootY.SkipEventDuringInput = false;
            this.numericBoxFootY.SmartIncrement = true;
            this.numericBoxFootY.TabIndex = 0;
            this.numericBoxFootY.ThonsandsSeparator = true;
            this.numericBoxFootY.Value = 512D;
            this.numericBoxFootY.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxFootX
            // 
            this.numericBoxFootX.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootX.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxFootX.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootX.FooterText = "pix";
            this.numericBoxFootX.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxFootX.HeaderText = "x";
            this.numericBoxFootX.Location = new System.Drawing.Point(53, 72);
            this.numericBoxFootX.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxFootX.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxFootX.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxFootX.Name = "numericBoxFootX";
            this.numericBoxFootX.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxFootX.RadianValue = 8.9360857702109673D;
            this.numericBoxFootX.RoundErrorAccuracy = 10;
            this.numericBoxFootX.Size = new System.Drawing.Size(88, 25);
            this.numericBoxFootX.SkipEventDuringInput = false;
            this.numericBoxFootX.SmartIncrement = true;
            this.numericBoxFootX.TabIndex = 0;
            this.numericBoxFootX.ThonsandsSeparator = true;
            this.numericBoxFootX.Value = 512D;
            this.numericBoxFootX.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxDistance
            // 
            this.numericBoxDistance.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDistance.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.FooterText = "mm";
            this.numericBoxDistance.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.HeaderText = "Cameralength 2";
            this.numericBoxDistance.Location = new System.Drawing.Point(3, 21);
            this.numericBoxDistance.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxDistance.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDistance.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDistance.Name = "numericBoxDistance";
            this.numericBoxDistance.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDistance.RadianValue = 5.2359877559829888D;
            this.numericBoxDistance.RoundErrorAccuracy = 10;
            this.numericBoxDistance.Size = new System.Drawing.Size(187, 25);
            this.numericBoxDistance.SkipEventDuringInput = false;
            this.numericBoxDistance.SmartIncrement = true;
            this.numericBoxDistance.TabIndex = 0;
            this.numericBoxDistance.ThonsandsSeparator = true;
            this.numericBoxDistance.Value = 300D;
            this.numericBoxDistance.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxPhi
            // 
            this.numericBoxPhi.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxPhi.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.FooterText = "°";
            this.numericBoxPhi.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxPhi.HeaderText = "φ";
            this.numericBoxPhi.Location = new System.Drawing.Point(107, 47);
            this.numericBoxPhi.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxPhi.Maximum = 360D;
            this.numericBoxPhi.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxPhi.Minimum = -360D;
            this.numericBoxPhi.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxPhi.Name = "numericBoxPhi";
            this.numericBoxPhi.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxPhi.RoundErrorAccuracy = 10;
            this.numericBoxPhi.Size = new System.Drawing.Size(83, 25);
            this.numericBoxPhi.SkipEventDuringInput = false;
            this.numericBoxPhi.SmartIncrement = true;
            this.numericBoxPhi.TabIndex = 0;
            this.numericBoxPhi.ThonsandsSeparator = true;
            this.numericBoxPhi.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // numericBoxTau
            // 
            this.numericBoxTau.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTau.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxTau.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxTau.FooterText = "°";
            this.numericBoxTau.HeaderBackColor = System.Drawing.SystemColors.Control;
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
            this.numericBoxTau.RoundErrorAccuracy = 10;
            this.numericBoxTau.Size = new System.Drawing.Size(83, 25);
            this.numericBoxTau.SkipEventDuringInput = false;
            this.numericBoxTau.SmartIncrement = true;
            this.numericBoxTau.TabIndex = 0;
            this.numericBoxTau.ThonsandsSeparator = true;
            this.numericBoxTau.Value = 20D;
            this.numericBoxTau.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxPixelWidth_ValueChanged);
            // 
            // SaclaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
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
