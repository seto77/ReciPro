namespace ReciPro
{
    partial class FormResolution
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
            components = new System.ComponentModel.Container(); // (260531Ch)
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            captureExtender.SetCapture(this, true); // 260521Cl 追加: GUI監査キャプチャ対象 (フォーム全体)
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormResolution));
            buttonOK = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            checkBoxKeepAspect = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownHeight)).BeginInit();
            SuspendLayout();
            // 
            // buttonOK
            // 
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonOK.Location = new System.Drawing.Point(126, 47);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(65, 25);
            buttonOK.TabIndex = 0;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonCancel.Location = new System.Drawing.Point(193, 47);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(65, 25);
            buttonCancel.TabIndex = 0;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // numericUpDownWidth
            // 
            numericUpDownWidth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            numericUpDownWidth.Increment = new decimal(new int[] {
            256,
            0,
            0,
            0});
            numericUpDownWidth.Location = new System.Drawing.Point(47, 22);
            numericUpDownWidth.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            numericUpDownWidth.Name = "numericUpDownWidth";
            toolTip.SetToolTip(numericUpDownWidth, resources.GetString("numericUpDownWidth.ToolTip")); // 260531Cl
            numericUpDownWidth.Size = new System.Drawing.Size(47, 21);
            numericUpDownWidth.TabIndex = 1;
            numericUpDownWidth.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            numericUpDownWidth.ValueChanged += new System.EventHandler(numericUpDownWidth_ValueChanged);
            // 
            // numericUpDownHeight
            // 
            numericUpDownHeight.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            numericUpDownHeight.Increment = new decimal(new int[] {
            256,
            0,
            0,
            0});
            numericUpDownHeight.Location = new System.Drawing.Point(179, 22);
            numericUpDownHeight.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            numericUpDownHeight.Name = "numericUpDownHeight";
            toolTip.SetToolTip(numericUpDownHeight, resources.GetString("numericUpDownHeight.ToolTip")); // 260531Cl
            numericUpDownHeight.Size = new System.Drawing.Size(47, 21);
            numericUpDownHeight.TabIndex = 1;
            numericUpDownHeight.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            numericUpDownHeight.ValueChanged += new System.EventHandler(numericUpDownHeight_ValueChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(8, 24);
            label1.Name = "label1";
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip")); // 260531Cl
            label1.Size = new System.Drawing.Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "Width";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(134, 24);
            label2.Name = "label2";
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip")); // 260531Cl
            label2.Size = new System.Drawing.Size(43, 15);
            label2.TabIndex = 2;
            label2.Text = "Height";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label4.Location = new System.Drawing.Point(226, 24);
            label4.Name = "label4";
            toolTip.SetToolTip(label4, resources.GetString("label4.ToolTip")); // 260531Cl
            label4.Size = new System.Drawing.Size(32, 15);
            label4.TabIndex = 2;
            label4.Text = "pixel";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(96, 24);
            label3.Name = "label3";
            toolTip.SetToolTip(label3, resources.GetString("label3.ToolTip")); // 260531Cl
            label3.Size = new System.Drawing.Size(32, 15);
            label3.TabIndex = 2;
            label3.Text = "pixel";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label5.Location = new System.Drawing.Point(1, 2);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(88, 15);
            label5.TabIndex = 2;
            label5.Text = "Set image size";
            // 
            // checkBoxKeepAspect
            // 
            checkBoxKeepAspect.AutoSize = true;
            checkBoxKeepAspect.Checked = true;
            checkBoxKeepAspect.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxKeepAspect.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            checkBoxKeepAspect.Location = new System.Drawing.Point(0, 52);
            checkBoxKeepAspect.Name = "checkBoxKeepAspect";
            toolTip.SetToolTip(checkBoxKeepAspect, resources.GetString("checkBoxKeepAspect.ToolTip")); // 260531Cl
            checkBoxKeepAspect.Size = new System.Drawing.Size(126, 19);
            checkBoxKeepAspect.TabIndex = 3;
            checkBoxKeepAspect.Text = "Keep Aspect Ratio";
            checkBoxKeepAspect.UseVisualStyleBackColor = true;
            // 
            // FormResolution
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new System.Drawing.Size(259, 76);
            ControlBox = false;
            Controls.Add(checkBoxKeepAspect);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label5);
            Controls.Add(numericUpDownHeight);
            Controls.Add(numericUpDownWidth);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(label1);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormResolution";
            Text = "Image Size";
            ((System.ComponentModel.ISupportInitialize)(numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownHeight)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown numericUpDownWidth;
        public System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxKeepAspect;
    }
}