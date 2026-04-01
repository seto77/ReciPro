namespace Crystallography.Controls
{
    partial class FormPeriodicTable
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
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            buttonLa = new System.Windows.Forms.Button();
            buttonAc = new System.Windows.Forms.Button();
            labelLa = new System.Windows.Forms.Label();
            labelAc = new System.Windows.Forms.Label();
            buttonMustInclude = new System.Windows.Forms.Button();
            buttonMustExclude = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            buttonMayInclude = new System.Windows.Forms.Button();
            buttonOK = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // buttonLa
            // 
            buttonLa.BackColor = System.Drawing.Color.MistyRose;
            buttonLa.Enabled = false;
            buttonLa.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonLa.ForeColor = System.Drawing.Color.MediumBlue;
            buttonLa.Location = new System.Drawing.Point(95, 124);
            buttonLa.Margin = new System.Windows.Forms.Padding(0);
            buttonLa.Name = "buttonLa";
            buttonLa.Size = new System.Drawing.Size(29, 25);
            buttonLa.TabIndex = 0;
            buttonLa.Text = "La";
            buttonLa.UseVisualStyleBackColor = false;
            // 
            // buttonAc
            // 
            buttonAc.BackColor = System.Drawing.Color.MistyRose;
            buttonAc.Enabled = false;
            buttonAc.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonAc.ForeColor = System.Drawing.Color.MediumBlue;
            buttonAc.Location = new System.Drawing.Point(95, 149);
            buttonAc.Margin = new System.Windows.Forms.Padding(0);
            buttonAc.Name = "buttonAc";
            buttonAc.Size = new System.Drawing.Size(29, 27);
            buttonAc.TabIndex = 0;
            buttonAc.Text = "Ac";
            buttonAc.UseVisualStyleBackColor = false;
            // 
            // labelLa
            // 
            labelLa.AutoSize = true;
            labelLa.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            labelLa.ForeColor = System.Drawing.Color.Crimson;
            labelLa.Location = new System.Drawing.Point(9, 187);
            labelLa.Margin = new System.Windows.Forms.Padding(0);
            labelLa.Name = "labelLa";
            labelLa.Size = new System.Drawing.Size(81, 15);
            labelLa.TabIndex = 1;
            labelLa.Text = "La: lanthanide";
            // 
            // labelAc
            // 
            labelAc.AutoSize = true;
            labelAc.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            labelAc.ForeColor = System.Drawing.Color.Crimson;
            labelAc.Location = new System.Drawing.Point(9, 202);
            labelAc.Margin = new System.Windows.Forms.Padding(0);
            labelAc.Name = "labelAc";
            labelAc.Size = new System.Drawing.Size(69, 15);
            labelAc.TabIndex = 1;
            labelAc.Text = "Ac: actinide";
            // 
            // buttonMustInclude
            // 
            buttonMustInclude.BackColor = System.Drawing.Color.LightBlue;
            buttonMustInclude.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonMustInclude.Location = new System.Drawing.Point(243, 3);
            buttonMustInclude.Margin = new System.Windows.Forms.Padding(0);
            buttonMustInclude.Name = "buttonMustInclude";
            buttonMustInclude.Size = new System.Drawing.Size(25, 25);
            buttonMustInclude.TabIndex = 0;
            buttonMustInclude.UseVisualStyleBackColor = false;
            buttonMustInclude.Click += buttonMustInclude_Click;
            // 
            // buttonMustExclude
            // 
            buttonMustExclude.BackColor = System.Drawing.Color.LightCoral;
            buttonMustExclude.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonMustExclude.Location = new System.Drawing.Point(243, 31);
            buttonMustExclude.Margin = new System.Windows.Forms.Padding(0);
            buttonMustExclude.Name = "buttonMustExclude";
            buttonMustExclude.Size = new System.Drawing.Size(25, 25);
            buttonMustExclude.TabIndex = 0;
            buttonMustExclude.UseVisualStyleBackColor = false;
            buttonMustExclude.Click += buttonMustExclude_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label3.Location = new System.Drawing.Point(274, 9);
            label3.Margin = new System.Windows.Forms.Padding(0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(76, 15);
            label3.TabIndex = 1;
            label3.Text = "must include";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label4.Location = new System.Drawing.Point(273, 36);
            label4.Margin = new System.Windows.Forms.Padding(0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(77, 15);
            label4.TabIndex = 1;
            label4.Text = "must exclude";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            label5.Location = new System.Drawing.Point(133, 21);
            label5.Margin = new System.Windows.Forms.Padding(0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(107, 15);
            label5.TabIndex = 1;
            label5.Text = "may or not include";
            // 
            // buttonMayInclude
            // 
            buttonMayInclude.BackColor = System.Drawing.Color.LightYellow;
            buttonMayInclude.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonMayInclude.Location = new System.Drawing.Point(108, 16);
            buttonMayInclude.Margin = new System.Windows.Forms.Padding(0);
            buttonMayInclude.Name = "buttonMayInclude";
            buttonMayInclude.Size = new System.Drawing.Size(25, 25);
            buttonMayInclude.TabIndex = 0;
            buttonMayInclude.UseVisualStyleBackColor = false;
            buttonMayInclude.Click += buttonMayInclude_Click;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonOK.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            buttonOK.Location = new System.Drawing.Point(476, 257);
            buttonOK.Margin = new System.Windows.Forms.Padding(0);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(118, 28);
            buttonOK.TabIndex = 0;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // FormPeriodicTable
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            captureExtender.SetCapture(this, true);
            ClientSize = new System.Drawing.Size(595, 286);
            Controls.Add(buttonLa);
            Controls.Add(buttonMayInclude);
            Controls.Add(buttonMustInclude);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(buttonMustExclude);
            Controls.Add(buttonOK);
            Controls.Add(buttonAc);
            Controls.Add(labelAc);
            Controls.Add(labelLa);
            Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(64, 32, 64, 32);
            Name = "FormPeriodicTable";
            StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            Text = "Periodic Table";
            FormClosing += FormPeriodicTable_FormClosing;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLa;
        private System.Windows.Forms.Button buttonAc;
        private System.Windows.Forms.Label labelLa;
        private System.Windows.Forms.Label labelAc;
        private System.Windows.Forms.Button buttonMustInclude;
        private System.Windows.Forms.Button buttonMustExclude;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonMayInclude;
        private System.Windows.Forms.Button buttonOK;
    }
}