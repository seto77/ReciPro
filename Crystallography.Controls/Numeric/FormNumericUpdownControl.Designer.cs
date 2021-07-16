namespace Crystallography.Controls
{
    partial class FormNumericUpdownControl
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
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericBoxDecimalIncrement = new Crystallography.Controls.NumericBox();
            this.numericBoxDecimalPlace = new Crystallography.Controls.NumericBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Increment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-2, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Decimal Places";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(2, 57);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(83, 57);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // numericBoxDecimalIncrement
            // 
            this.numericBoxDecimalIncrement.DecimalPlaces = -1;
            this.numericBoxDecimalIncrement.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxDecimalIncrement.Location = new System.Drawing.Point(94, 3);
            this.numericBoxDecimalIncrement.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                       this.numericBoxDecimalIncrement.Name = "numericBoxDecimalIncrement";
            this.numericBoxDecimalIncrement.RadianValue = 0;
                        
            this.numericBoxDecimalIncrement.Size = new System.Drawing.Size(61, 22);
            this.numericBoxDecimalIncrement.TabIndex = 0;
            this.numericBoxDecimalIncrement.Value = 0;
                        // 
            // numericBoxDecimalPlace
            // 
            this.numericBoxDecimalPlace.DecimalPlaces = -1;
            this.numericBoxDecimalPlace.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericBoxDecimalPlace.Location = new System.Drawing.Point(94, 28);
            this.numericBoxDecimalPlace.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
                       this.numericBoxDecimalPlace.Name = "numericBoxDecimalPlace";
            this.numericBoxDecimalPlace.RadianValue = 0;
                        
            this.numericBoxDecimalPlace.Size = new System.Drawing.Size(61, 22);
            this.numericBoxDecimalPlace.TabIndex = 0;
            this.numericBoxDecimalPlace.Value = 0;
                        // 
            // FormNumericUpdownControl
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(166, 83);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericBoxDecimalIncrement);
            this.Controls.Add(this.numericBoxDecimalPlace);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNumericUpdownControl";
            this.ShowIcon = false;
            this.Text = "NumericUpdown Control";
            this.Load += new System.EventHandler(this.FormNumericUpdownControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private NumericBox numericBoxDecimalPlace;
        private NumericBox numericBoxDecimalIncrement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}