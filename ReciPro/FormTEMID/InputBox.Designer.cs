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
            this.label72 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.numericalTextBoxLength = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxDvalue = new Crystallography.Controls.NumericBox();
            this.numericalTextBoxGlength = new Crystallography.Controls.NumericBox();
            this.SuspendLayout();
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("Arial Unicode MS", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label72.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label72.Location = new System.Drawing.Point(225, 3);
            this.label72.Margin = new System.Windows.Forms.Padding(0);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(12, 11);
            this.label72.TabIndex = 72;
            this.label72.Text = "-1";
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("Arial Unicode MS", 9F);
            this.label64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label64.Location = new System.Drawing.Point(203, 5);
            this.label64.Margin = new System.Windows.Forms.Padding(0);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(26, 16);
            this.label64.TabIndex = 73;
            this.label64.Text = "nm";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("Arial Unicode MS", 9F);
            this.label60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label60.Location = new System.Drawing.Point(132, 6);
            this.label60.Margin = new System.Windows.Forms.Padding(0);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(16, 16);
            this.label60.TabIndex = 74;
            this.label60.Text = "Å";
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(48, 4);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(30, 16);
            this.label50.TabIndex = 71;
            this.label50.Text = "mm";
            // 
            // numericalTextBoxLength
            // 
            this.numericalTextBoxLength.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxLength.DecimalPlaces = -1;
            this.numericalTextBoxLength.Location = new System.Drawing.Point(0, 0);
            this.numericalTextBoxLength.Margin = new System.Windows.Forms.Padding(1);
            this.numericalTextBoxLength.Multiline = false;
            this.numericalTextBoxLength.Name = "numericalTextBoxLength";
            this.numericalTextBoxLength.Padding = new System.Windows.Forms.Padding(1);
            this.numericalTextBoxLength.RadianValue = 0D;
            this.numericalTextBoxLength.ReadOnly = false;
            this.numericalTextBoxLength.ShowFraction = false;
            this.numericalTextBoxLength.Size = new System.Drawing.Size(50, 21);
            this.numericalTextBoxLength.TabIndex = 75;
            this.numericalTextBoxLength.Value = 0D;
            this.numericalTextBoxLength.WordWrap = true;
            this.numericalTextBoxLength.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxLength_ValueChanged);
            this.numericalTextBoxLength.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxlength_Click);
            // 
            // numericalTextBoxDvalue
            // 
            this.numericalTextBoxDvalue.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxDvalue.DecimalPlaces = -1;
            this.numericalTextBoxDvalue.Location = new System.Drawing.Point(82, 0);
            this.numericalTextBoxDvalue.Margin = new System.Windows.Forms.Padding(1);
            this.numericalTextBoxDvalue.Multiline = false;
            this.numericalTextBoxDvalue.Name = "numericalTextBoxDvalue";
            this.numericalTextBoxDvalue.Padding = new System.Windows.Forms.Padding(1);
            this.numericalTextBoxDvalue.RadianValue = 0D;
            this.numericalTextBoxDvalue.ReadOnly = true;
            this.numericalTextBoxDvalue.ShowFraction = false;
            this.numericalTextBoxDvalue.Size = new System.Drawing.Size(50, 21);
            this.numericalTextBoxDvalue.TabIndex = 75;
            this.numericalTextBoxDvalue.Value = 0D;
            this.numericalTextBoxDvalue.WordWrap = true;
            this.numericalTextBoxDvalue.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxDvalue_ValueChanged);
            this.numericalTextBoxDvalue.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxDvalue_Click);
            // 
            // numericalTextBoxGlength
            // 
            this.numericalTextBoxGlength.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxGlength.DecimalPlaces = -1;
            this.numericalTextBoxGlength.Location = new System.Drawing.Point(154, 0);
            this.numericalTextBoxGlength.Margin = new System.Windows.Forms.Padding(1);
            this.numericalTextBoxGlength.Multiline = false;
            this.numericalTextBoxGlength.Name = "numericalTextBoxGlength";
            this.numericalTextBoxGlength.Padding = new System.Windows.Forms.Padding(1);
            this.numericalTextBoxGlength.RadianValue = 0D;
            this.numericalTextBoxGlength.ReadOnly = true;
            this.numericalTextBoxGlength.ShowFraction = false;
            this.numericalTextBoxGlength.Size = new System.Drawing.Size(50, 21);
            this.numericalTextBoxGlength.TabIndex = 75;
            this.numericalTextBoxGlength.Value = 0D;
            this.numericalTextBoxGlength.WordWrap = true;
            this.numericalTextBoxGlength.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxGlength_ValueChanged);
            this.numericalTextBoxGlength.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericalTextBoxGlength_Click);
            // 
            // InputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.numericalTextBoxGlength);
            this.Controls.Add(this.numericalTextBoxDvalue);
            this.Controls.Add(this.numericalTextBoxLength);
            this.Controls.Add(this.label72);
            this.Controls.Add(this.label64);
            this.Controls.Add(this.label60);
            this.Controls.Add(this.label50);
            this.Name = "InputBox";
            this.Size = new System.Drawing.Size(237, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label50;
        private Crystallography.Controls.NumericBox numericalTextBoxLength;
        private Crystallography.Controls.NumericBox numericalTextBoxDvalue;
        private Crystallography.Controls.NumericBox numericalTextBoxGlength;
    }
}
