namespace ReciPro
{
    partial class InputBox
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
            this.label64 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.numericBoxLength = new Crystallography.Controls.NumericBox();
            this.numericBoxDvalue = new Crystallography.Controls.NumericBox();
            this.numericBoxGlength = new Crystallography.Controls.NumericBox();
            this.SuspendLayout();
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label64.Location = new System.Drawing.Point(203, 5);
            this.label64.Margin = new System.Windows.Forms.Padding(0);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(33, 15);
            this.label64.TabIndex = 73;
            this.label64.Text = "nm⁻¹";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label60.Location = new System.Drawing.Point(132, 6);
            this.label60.Margin = new System.Windows.Forms.Padding(0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(15, 15);
            this.label60.TabIndex = 74;
            this.label60.Text = "Å";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label50.Location = new System.Drawing.Point(48, 4);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(29, 15);
            this.label50.TabIndex = 71;
            this.label50.Text = "mm";
            // 
            // numericBoxLength
            // 
            this.numericBoxLength.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxLength.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxLength.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxLength.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxLength.Location = new System.Drawing.Point(0, 0);
            this.numericBoxLength.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxLength.MaximumSize = new System.Drawing.Size(1000, 27);
            this.numericBoxLength.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxLength.Name = "numericBoxLength";
            this.numericBoxLength.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxLength.RoundErrorAccuracy = -1;
            this.numericBoxLength.Size = new System.Drawing.Size(50, 27);
            this.numericBoxLength.TabIndex = 75;
            this.numericBoxLength.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxLength_ValueChanged);
            this.numericBoxLength.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxlength_Click);
            // 
            // numericBoxDvalue
            // 
            this.numericBoxDvalue.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDvalue.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxDvalue.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDvalue.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDvalue.Location = new System.Drawing.Point(82, 0);
            this.numericBoxDvalue.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxDvalue.MaximumSize = new System.Drawing.Size(1000, 27);
            this.numericBoxDvalue.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDvalue.Name = "numericBoxDvalue";
            this.numericBoxDvalue.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxDvalue.ReadOnly = true;
            this.numericBoxDvalue.RoundErrorAccuracy = -1;
            this.numericBoxDvalue.Size = new System.Drawing.Size(50, 27);
            this.numericBoxDvalue.TabIndex = 75;
            this.numericBoxDvalue.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDvalue.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxDvalue_ValueChanged);
            this.numericBoxDvalue.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxDvalue_Click);
            // 
            // numericBoxGlength
            // 
            this.numericBoxGlength.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGlength.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxGlength.FooterBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGlength.HeaderBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGlength.Location = new System.Drawing.Point(154, 0);
            this.numericBoxGlength.Margin = new System.Windows.Forms.Padding(1);
            this.numericBoxGlength.MaximumSize = new System.Drawing.Size(1000, 27);
            this.numericBoxGlength.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxGlength.Name = "numericBoxGlength";
            this.numericBoxGlength.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxGlength.ReadOnly = true;
            this.numericBoxGlength.RoundErrorAccuracy = -1;
            this.numericBoxGlength.Size = new System.Drawing.Size(50, 27);
            this.numericBoxGlength.TabIndex = 75;
            this.numericBoxGlength.TextBoxBackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGlength.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxGlength_ValueChanged);
            this.numericBoxGlength.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxGlength_Click);
            // 
            // InputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.numericBoxGlength);
            this.Controls.Add(this.numericBoxDvalue);
            this.Controls.Add(this.numericBoxLength);
            this.Controls.Add(this.label64);
            this.Controls.Add(this.label60);
            this.Controls.Add(this.label50);
            this.Name = "InputBox";
            this.Size = new System.Drawing.Size(236, 28);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label50;
        private Crystallography.Controls.NumericBox numericBoxLength;
        private Crystallography.Controls.NumericBox numericBoxDvalue;
        private Crystallography.Controls.NumericBox numericBoxGlength;
    }
}
