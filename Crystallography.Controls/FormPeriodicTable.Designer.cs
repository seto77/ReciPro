namespace Crystallography.Controls
{
    partial class FormPeriodicTable
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
            this.buttonLa = new System.Windows.Forms.Button();
            this.buttonAc = new System.Windows.Forms.Button();
            this.labelLa = new System.Windows.Forms.Label();
            this.labelAc = new System.Windows.Forms.Label();
            this.buttonMustInclude = new System.Windows.Forms.Button();
            this.buttonMustExclude = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonMayInclude = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLa
            // 
            this.buttonLa.BackColor = System.Drawing.Color.MistyRose;
            this.buttonLa.Enabled = false;
            this.buttonLa.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLa.ForeColor = System.Drawing.Color.MediumBlue;
            this.buttonLa.Location = new System.Drawing.Point(95, 124);
            this.buttonLa.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLa.Name = "buttonLa";
            this.buttonLa.Size = new System.Drawing.Size(29, 25);
            this.buttonLa.TabIndex = 0;
            this.buttonLa.Text = "La";
            this.buttonLa.UseVisualStyleBackColor = false;
            // 
            // buttonAc
            // 
            this.buttonAc.BackColor = System.Drawing.Color.MistyRose;
            this.buttonAc.Enabled = false;
            this.buttonAc.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAc.ForeColor = System.Drawing.Color.MediumBlue;
            this.buttonAc.Location = new System.Drawing.Point(95, 149);
            this.buttonAc.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAc.Name = "buttonAc";
            this.buttonAc.Size = new System.Drawing.Size(29, 27);
            this.buttonAc.TabIndex = 0;
            this.buttonAc.Text = "Ac";
            this.buttonAc.UseVisualStyleBackColor = false;
            // 
            // labelLa
            // 
            this.labelLa.AutoSize = true;
            this.labelLa.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLa.ForeColor = System.Drawing.Color.Crimson;
            this.labelLa.Location = new System.Drawing.Point(9, 187);
            this.labelLa.Margin = new System.Windows.Forms.Padding(0);
            this.labelLa.Name = "labelLa";
            this.labelLa.Size = new System.Drawing.Size(81, 15);
            this.labelLa.TabIndex = 1;
            this.labelLa.Text = "La: lanthanide";
            // 
            // labelAc
            // 
            this.labelAc.AutoSize = true;
            this.labelAc.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAc.ForeColor = System.Drawing.Color.Crimson;
            this.labelAc.Location = new System.Drawing.Point(9, 202);
            this.labelAc.Margin = new System.Windows.Forms.Padding(0);
            this.labelAc.Name = "labelAc";
            this.labelAc.Size = new System.Drawing.Size(69, 15);
            this.labelAc.TabIndex = 1;
            this.labelAc.Text = "Ac: actinide";
            // 
            // buttonMustInclude
            // 
            this.buttonMustInclude.BackColor = System.Drawing.Color.LightBlue;
            this.buttonMustInclude.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMustInclude.Location = new System.Drawing.Point(243, 3);
            this.buttonMustInclude.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMustInclude.Name = "buttonMustInclude";
            this.buttonMustInclude.Size = new System.Drawing.Size(25, 25);
            this.buttonMustInclude.TabIndex = 0;
            this.buttonMustInclude.UseVisualStyleBackColor = false;
            this.buttonMustInclude.Click += new System.EventHandler(this.buttonMustInclude_Click);
            // 
            // buttonMustExclude
            // 
            this.buttonMustExclude.BackColor = System.Drawing.Color.LightCoral;
            this.buttonMustExclude.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMustExclude.Location = new System.Drawing.Point(243, 31);
            this.buttonMustExclude.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMustExclude.Name = "buttonMustExclude";
            this.buttonMustExclude.Size = new System.Drawing.Size(25, 25);
            this.buttonMustExclude.TabIndex = 0;
            this.buttonMustExclude.UseVisualStyleBackColor = false;
            this.buttonMustExclude.Click += new System.EventHandler(this.buttonMustExclude_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(277, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "must include";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(273, 36);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "must exclude";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(133, 21);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "may or not include";
            // 
            // buttonMayInclude
            // 
            this.buttonMayInclude.BackColor = System.Drawing.Color.LightYellow;
            this.buttonMayInclude.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMayInclude.Location = new System.Drawing.Point(108, 16);
            this.buttonMayInclude.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMayInclude.Name = "buttonMayInclude";
            this.buttonMayInclude.Size = new System.Drawing.Size(25, 25);
            this.buttonMayInclude.TabIndex = 0;
            this.buttonMayInclude.UseVisualStyleBackColor = false;
            this.buttonMayInclude.Click += new System.EventHandler(this.buttonMayInclude_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(530, 233);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(0);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(118, 28);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // FormPeriodicTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(648, 261);
            this.Controls.Add(this.buttonLa);
            this.Controls.Add(this.buttonMayInclude);
            this.Controls.Add(this.buttonMustInclude);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonMustExclude);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonAc);
            this.Controls.Add(this.labelAc);
            this.Controls.Add(this.labelLa);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(64, 32, 64, 32);
            this.Name = "FormPeriodicTable";
            this.Text = "Periodic Table";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPeriodicTable_FormClosing);
            this.Load += new System.EventHandler(this.FormPeriodicTable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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