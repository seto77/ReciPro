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
            this.numericBoxLength = new Crystallography.Controls.NumericBox();
            this.numericBoxDvalue = new Crystallography.Controls.NumericBox();
            this.numericBoxGlength = new Crystallography.Controls.NumericBox();
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
            // numericBoxLength
            // 
            this.numericBoxLength.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxLength.DecimalPlaces = -1;
            this.numericBoxLength.Location = new System.Drawing.Point(0, 0);
            this.numericBoxLength.Margin = new System.Windows.Forms.Padding(1);
                       this.numericBoxLength.Name = "numericBoxLength";
            this.numericBoxLength.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxLength.RadianValue = 0D;
                        
            this.numericBoxLength.Size = new System.Drawing.Size(50, 21);
            this.numericBoxLength.TabIndex = 75;
                                    this.numericBoxLength.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxLength_ValueChanged);
            this.numericBoxLength.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxlength_Click);
            // 
            // numericBoxDvalue
            // 
            this.numericBoxDvalue.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDvalue.DecimalPlaces = -1;
            this.numericBoxDvalue.Location = new System.Drawing.Point(82, 0);
            this.numericBoxDvalue.Margin = new System.Windows.Forms.Padding(1);
                       this.numericBoxDvalue.Name = "numericBoxDvalue";
            this.numericBoxDvalue.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxDvalue.RadianValue = 0D;
            this.numericBoxDvalue.ReadOnly = true;
            
            this.numericBoxDvalue.Size = new System.Drawing.Size(50, 21);
            this.numericBoxDvalue.TabIndex = 75;
                                    this.numericBoxDvalue.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxDvalue_ValueChanged);
            this.numericBoxDvalue.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxDvalue_Click);
            // 
            // numericBoxGlength
            // 
            this.numericBoxGlength.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxGlength.DecimalPlaces = -1;
            this.numericBoxGlength.Location = new System.Drawing.Point(154, 0);
            this.numericBoxGlength.Margin = new System.Windows.Forms.Padding(1);
                       this.numericBoxGlength.Name = "numericBoxGlength";
            this.numericBoxGlength.Padding = new System.Windows.Forms.Padding(1);
            this.numericBoxGlength.RadianValue = 0D;
            this.numericBoxGlength.ReadOnly = true;
            
            this.numericBoxGlength.Size = new System.Drawing.Size(50, 21);
            this.numericBoxGlength.TabIndex = 75;
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
        private Crystallography.Controls.NumericBox numericBoxLength;
        private Crystallography.Controls.NumericBox numericBoxDvalue;
        private Crystallography.Controls.NumericBox numericBoxGlength;
    }
}
