namespace ReciPro
{
    partial class FormExport3DModel
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
            captureExtender.SetCapture(this, true); // 260803Cl 追加: GUI監査キャプチャ対象 (フォーム全体)
            labelInfo = new System.Windows.Forms.Label();
            labelSizeAng = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            numericUpDownMaxSize = new System.Windows.Forms.NumericUpDown();
            label2 = new System.Windows.Forms.Label();
            checkBoxCellEdges = new System.Windows.Forms.CheckBox();
            numericUpDownEdgeDia = new System.Windows.Forms.NumericUpDown();
            label3 = new System.Windows.Forms.Label();
            labelResult = new System.Windows.Forms.Label();
            labelWarning = new System.Windows.Forms.Label();
            buttonOK = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownMaxSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownEdgeDia)).BeginInit();
            SuspendLayout();
            //
            // labelInfo
            //
            labelInfo.AutoSize = true;
            labelInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelInfo.Location = new System.Drawing.Point(10, 10);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new System.Drawing.Size(140, 15);
            labelInfo.TabIndex = 0;
            labelInfo.Text = "Objects: 0,  Triangles: 0";
            //
            // labelSizeAng
            //
            labelSizeAng.AutoSize = true;
            labelSizeAng.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelSizeAng.Location = new System.Drawing.Point(10, 30);
            labelSizeAng.Name = "labelSizeAng";
            labelSizeAng.Size = new System.Drawing.Size(120, 15);
            labelSizeAng.TabIndex = 1;
            labelSizeAng.Text = "Model size: 0 × 0 × 0 Å";
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label1.Location = new System.Drawing.Point(10, 59);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(110, 15);
            label1.TabIndex = 2;
            label1.Text = "Largest dimension:";
            //
            // numericUpDownMaxSize
            //
            numericUpDownMaxSize.DecimalPlaces = 1;
            numericUpDownMaxSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            numericUpDownMaxSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            numericUpDownMaxSize.Location = new System.Drawing.Point(126, 55);
            numericUpDownMaxSize.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            numericUpDownMaxSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            numericUpDownMaxSize.Name = "numericUpDownMaxSize";
            numericUpDownMaxSize.Size = new System.Drawing.Size(62, 21);
            numericUpDownMaxSize.TabIndex = 3;
            numericUpDownMaxSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDownMaxSize.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            numericUpDownMaxSize.ValueChanged += update;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label2.Location = new System.Drawing.Point(192, 59);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(28, 15);
            label2.TabIndex = 4;
            label2.Text = "mm";
            //
            // checkBoxCellEdges (260803Cl 追加: 単位胞枠の円柱化)
            //
            checkBoxCellEdges.AutoSize = true;
            checkBoxCellEdges.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            checkBoxCellEdges.Location = new System.Drawing.Point(12, 84);
            checkBoxCellEdges.Name = "checkBoxCellEdges";
            checkBoxCellEdges.Size = new System.Drawing.Size(220, 19);
            checkBoxCellEdges.TabIndex = 5;
            checkBoxCellEdges.Text = "Unit cell edges as cylinders,  dia.:";
            checkBoxCellEdges.UseVisualStyleBackColor = true;
            checkBoxCellEdges.CheckedChanged += update;
            //
            // numericUpDownEdgeDia
            //
            numericUpDownEdgeDia.DecimalPlaces = 1;
            numericUpDownEdgeDia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            numericUpDownEdgeDia.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            numericUpDownEdgeDia.Location = new System.Drawing.Point(224, 82);
            numericUpDownEdgeDia.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            65536});
            numericUpDownEdgeDia.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            numericUpDownEdgeDia.Name = "numericUpDownEdgeDia";
            numericUpDownEdgeDia.Size = new System.Drawing.Size(52, 21);
            numericUpDownEdgeDia.TabIndex = 6;
            numericUpDownEdgeDia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            numericUpDownEdgeDia.Value = new decimal(new int[] {
            16,
            0,
            0,
            65536});
            //
            // label3
            //
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(280, 84);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(28, 15);
            label3.TabIndex = 7;
            label3.Text = "mm";
            //
            // labelResult
            //
            labelResult.AutoSize = true;
            labelResult.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelResult.Location = new System.Drawing.Point(10, 112);
            labelResult.Name = "labelResult";
            labelResult.Size = new System.Drawing.Size(200, 15);
            labelResult.TabIndex = 8;
            labelResult.Text = "Scale: 1.000 mm/Å,   Output size: 0 × 0 × 0 mm";
            //
            // labelWarning (260803Cl 追加: 印刷適性チェック簡易版の警告表示)
            //
            labelWarning.AutoSize = true;
            labelWarning.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelWarning.ForeColor = System.Drawing.Color.Crimson;
            labelWarning.Location = new System.Drawing.Point(10, 134);
            labelWarning.MaximumSize = new System.Drawing.Size(360, 0);
            labelWarning.Name = "labelWarning";
            labelWarning.Size = new System.Drawing.Size(0, 15);
            labelWarning.TabIndex = 9;
            //
            // buttonOK
            //
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonOK.Location = new System.Drawing.Point(196, 172);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(100, 25);
            buttonOK.TabIndex = 10;
            buttonOK.Text = "Save...";
            buttonOK.UseVisualStyleBackColor = true;
            //
            // buttonCancel
            //
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonCancel.Location = new System.Drawing.Point(302, 172);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(65, 25);
            buttonCancel.TabIndex = 11;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            //
            // FormExport3DModel
            //
            AcceptButton = buttonOK;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new System.Drawing.Size(377, 207);
            Controls.Add(labelInfo);
            Controls.Add(labelSizeAng);
            Controls.Add(label1);
            Controls.Add(numericUpDownMaxSize);
            Controls.Add(label2);
            Controls.Add(checkBoxCellEdges);
            Controls.Add(numericUpDownEdgeDia);
            Controls.Add(label3);
            Controls.Add(labelResult);
            Controls.Add(labelWarning);
            Controls.Add(buttonOK);
            Controls.Add(buttonCancel);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormExport3DModel";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Export 3D Model";
            ((System.ComponentModel.ISupportInitialize)(numericUpDownMaxSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(numericUpDownEdgeDia)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelSizeAng;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUpDownMaxSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxCellEdges;
        public System.Windows.Forms.NumericUpDown numericUpDownEdgeDia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Label labelWarning;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
    }
}
