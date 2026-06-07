namespace ReciPro
{
    partial class FormAtom
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
        // (260323Ch) renamed numeric container controls:
        // groupBox1 -> groupBoxMaterial
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAtom)); // 260531Cl ToolTipを resx 化
            captureExtender.SetCapture(this, true); // 260521Cl 追加: GUI監査キャプチャ対象 (フォーム全体)
            components = new System.ComponentModel.Container();
            groupBoxMaterial = new System.Windows.Forms.GroupBox();
            numericBoxAtomTransparency = new Crystallography.Controls.NumericBox();
            numericBoxAtomShininess = new Crystallography.Controls.NumericBox();
            numericBoxAtomSpecular = new Crystallography.Controls.NumericBox();
            numericBoxAtomEmmision = new Crystallography.Controls.NumericBox();
            numericBoxAtomDiffusion = new Crystallography.Controls.NumericBox();
            label37 = new System.Windows.Forms.Label();
            numericBoxAtomAmbient = new Crystallography.Controls.NumericBox();
            label36 = new System.Windows.Forms.Label();
            label38 = new System.Windows.Forms.Label();
            label35 = new System.Windows.Forms.Label();
            label34 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            pictureBoxAtomColor = new System.Windows.Forms.PictureBox();
            label8 = new System.Windows.Forms.Label();
            numericBoxAtomRadius = new Crystallography.Controls.NumericBox();
            label9 = new System.Windows.Forms.Label();
            radioButtonApplyEquivalentAtoms = new System.Windows.Forms.RadioButton();
            radioButtonApplyThis = new System.Windows.Forms.RadioButton();
            buttonCancel = new System.Windows.Forms.Button();
            buttonOK = new System.Windows.Forms.Button();
            checkBoxIsDraw = new System.Windows.Forms.CheckBox();
            radioButtonAllSameElement = new System.Windows.Forms.RadioButton();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            toolTip1.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip1.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip1.InitialDelay = 500; // 260601Cl 追加
            toolTip1.ReshowDelay = 100; // 260601Cl 追加
            groupBoxMaterial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(pictureBoxAtomColor)).BeginInit();                                                          // 260522Cl: numericBoxAtom* は NumericBox 化に伴い ISupportInitialize 不要
            SuspendLayout();
            // 
            // groupBoxMaterial
            // 
            groupBoxMaterial.Controls.Add(numericBoxAtomTransparency);
            groupBoxMaterial.Controls.Add(numericBoxAtomShininess);
            groupBoxMaterial.Controls.Add(numericBoxAtomSpecular);
            groupBoxMaterial.Controls.Add(numericBoxAtomEmmision);
            groupBoxMaterial.Controls.Add(numericBoxAtomDiffusion);
            groupBoxMaterial.Controls.Add(label37);
            groupBoxMaterial.Controls.Add(numericBoxAtomAmbient);
            groupBoxMaterial.Controls.Add(label36);
            groupBoxMaterial.Controls.Add(label38);
            groupBoxMaterial.Controls.Add(label35);
            groupBoxMaterial.Controls.Add(label34);
            groupBoxMaterial.Controls.Add(label12);
            groupBoxMaterial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            groupBoxMaterial.Location = new System.Drawing.Point(0, 0);
            groupBoxMaterial.Name = "groupBoxMaterial";
            groupBoxMaterial.Size = new System.Drawing.Size(211, 91);
            groupBoxMaterial.TabIndex = 96;
            groupBoxMaterial.TabStop = false;
            groupBoxMaterial.Text = "Material";
            // 
            // numericBoxAtomTransparency
            // 
            numericBoxAtomTransparency.DecimalPlaces = 1;
            numericBoxAtomTransparency.UpDown_Increment = 0.1D;
            numericBoxAtomTransparency.Location = new System.Drawing.Point(165, 63);
            numericBoxAtomTransparency.Maximum = 1D;
            numericBoxAtomTransparency.Name = "numericBoxAtomTransparency";
            numericBoxAtomTransparency.Minimum = 0D;                                                                                              // 260522Cl 追加: NumericUpDown 既定 Minimum=0 を維持
            numericBoxAtomTransparency.ShowUpDown = true;
            numericBoxAtomTransparency.Size = new System.Drawing.Size(41, 21);
            numericBoxAtomTransparency.TabIndex = 89;
            toolTip1.SetToolTip(numericBoxAtomTransparency, resources.GetString("numericBoxAtomTransparency.ToolTip"));
            numericBoxAtomTransparency.Value = 1D;
            // 
            // numericBoxAtomShininess
            // 
            numericBoxAtomShininess.Location = new System.Drawing.Point(165, 39);
            numericBoxAtomShininess.Maximum = 50D;
            numericBoxAtomShininess.Name = "numericBoxAtomShininess";
            numericBoxAtomShininess.DecimalPlaces = 0;                                                                                            // 260522Cl 追加: NumericUpDown 既定 DecimalPlaces=0 (整数表示) を維持
            numericBoxAtomShininess.Minimum = 0D;
            numericBoxAtomShininess.ShowUpDown = true;
            numericBoxAtomShininess.Size = new System.Drawing.Size(41, 21);
            numericBoxAtomShininess.TabIndex = 89;
            toolTip1.SetToolTip(numericBoxAtomShininess, resources.GetString("numericBoxAtomShininess.ToolTip"));
            // 
            // numericBoxAtomSpecular
            // 
            numericBoxAtomSpecular.DecimalPlaces = 1;
            numericBoxAtomSpecular.UpDown_Increment = 0.1D;
            numericBoxAtomSpecular.Location = new System.Drawing.Point(165, 14);
            numericBoxAtomSpecular.Maximum = 1D;
            numericBoxAtomSpecular.Name = "numericBoxAtomSpecular";
            numericBoxAtomSpecular.Minimum = 0D;
            numericBoxAtomSpecular.ShowUpDown = true;
            numericBoxAtomSpecular.Size = new System.Drawing.Size(41, 21);
            numericBoxAtomSpecular.TabIndex = 89;
            toolTip1.SetToolTip(numericBoxAtomSpecular, resources.GetString("numericBoxAtomSpecular.ToolTip"));
            // 
            // numericBoxAtomEmmision
            // 
            numericBoxAtomEmmision.DecimalPlaces = 1;
            numericBoxAtomEmmision.UpDown_Increment = 0.1D;
            numericBoxAtomEmmision.Location = new System.Drawing.Point(69, 64);
            numericBoxAtomEmmision.Maximum = 1D;
            numericBoxAtomEmmision.Name = "numericBoxAtomEmmision";
            numericBoxAtomEmmision.Minimum = 0D;
            numericBoxAtomEmmision.ShowUpDown = true;
            numericBoxAtomEmmision.Size = new System.Drawing.Size(41, 21);
            numericBoxAtomEmmision.TabIndex = 89;
            toolTip1.SetToolTip(numericBoxAtomEmmision, resources.GetString("numericBoxAtomEmmision.ToolTip"));
            // 
            // numericBoxAtomDiffusion
            // 
            numericBoxAtomDiffusion.DecimalPlaces = 1;
            numericBoxAtomDiffusion.UpDown_Increment = 0.1D;
            numericBoxAtomDiffusion.Location = new System.Drawing.Point(60, 38);
            numericBoxAtomDiffusion.Maximum = 1D;
            numericBoxAtomDiffusion.Name = "numericBoxAtomDiffusion";
            numericBoxAtomDiffusion.Minimum = 0D;
            numericBoxAtomDiffusion.ShowUpDown = true;
            numericBoxAtomDiffusion.Size = new System.Drawing.Size(41, 21);
            numericBoxAtomDiffusion.TabIndex = 89;
            toolTip1.SetToolTip(numericBoxAtomDiffusion, resources.GetString("numericBoxAtomDiffusion.ToolTip"));
            numericBoxAtomDiffusion.Value = 0.7D;
            // 
            // label37
            // 
            label37.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label37.Location = new System.Drawing.Point(125, 67);
            label37.Name = "label37";
            label37.Size = new System.Drawing.Size(40, 19);
            label37.TabIndex = 88;
            label37.Text = "Alpha";
            // 
            // numericBoxAtomAmbient
            // 
            numericBoxAtomAmbient.DecimalPlaces = 1;
            numericBoxAtomAmbient.UpDown_Increment = 0.1D;
            numericBoxAtomAmbient.Location = new System.Drawing.Point(60, 14);
            numericBoxAtomAmbient.Maximum = 1D;
            numericBoxAtomAmbient.Name = "numericBoxAtomAmbient";
            numericBoxAtomAmbient.Minimum = 0D;
            numericBoxAtomAmbient.ShowUpDown = true;
            numericBoxAtomAmbient.Size = new System.Drawing.Size(41, 21);
            numericBoxAtomAmbient.TabIndex = 89;
            toolTip1.SetToolTip(numericBoxAtomAmbient, resources.GetString("numericBoxAtomAmbient.ToolTip"));
            // 
            // label36
            // 
            label36.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label36.Location = new System.Drawing.Point(104, 43);
            label36.Name = "label36";
            label36.Size = new System.Drawing.Size(76, 16);
            label36.TabIndex = 88;
            label36.Text = "Shininess";
            // 
            // label38
            // 
            label38.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label38.Location = new System.Drawing.Point(106, 16);
            label38.Name = "label38";
            label38.Size = new System.Drawing.Size(76, 19);
            label38.TabIndex = 88;
            label38.Text = "Specular";
            // 
            // label35
            // 
            label35.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label35.Location = new System.Drawing.Point(4, 66);
            label35.Name = "label35";
            label35.Size = new System.Drawing.Size(76, 19);
            label35.TabIndex = 88;
            label35.Text = "Emission";
            // 
            // label34
            // 
            label34.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label34.Location = new System.Drawing.Point(4, 42);
            label34.Name = "label34";
            label34.Size = new System.Drawing.Size(59, 17);
            label34.TabIndex = 88;
            label34.Text = "Diffusion";
            // 
            // label12
            // 
            label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label12.Location = new System.Drawing.Point(4, 17);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(59, 19);
            label12.TabIndex = 88;
            label12.Text = "Ambient";
            // 
            // pictureBoxAtomColor
            // 
            pictureBoxAtomColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            pictureBoxAtomColor.Location = new System.Drawing.Point(43, 122);
            pictureBoxAtomColor.Name = "pictureBoxAtomColor";
            pictureBoxAtomColor.Size = new System.Drawing.Size(20, 21);
            pictureBoxAtomColor.TabIndex = 95;
            pictureBoxAtomColor.TabStop = false;
            toolTip1.SetToolTip(pictureBoxAtomColor, resources.GetString("pictureBoxAtomColor.ToolTip"));
            // 
            // label8
            // 
            label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.Location = new System.Drawing.Point(3, 126);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(48, 19);
            label8.TabIndex = 92;
            label8.Text = "Color";
            // 
            // numericBoxAtomRadius
            // 
            numericBoxAtomRadius.DecimalPlaces = 3;
            numericBoxAtomRadius.UpDown_Increment = 0.1D;
            numericBoxAtomRadius.Location = new System.Drawing.Point(47, 94);
            numericBoxAtomRadius.Maximum = 9.9D;
            numericBoxAtomRadius.Name = "numericBoxAtomRadius";
            numericBoxAtomRadius.Minimum = 0D;
            numericBoxAtomRadius.ShowUpDown = true;
            numericBoxAtomRadius.Size = new System.Drawing.Size(60, 21);
            numericBoxAtomRadius.TabIndex = 94;
            toolTip1.SetToolTip(numericBoxAtomRadius, resources.GetString("numericBoxAtomRadius.ToolTip"));
            // 
            // label9
            // 
            label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label9.Location = new System.Drawing.Point(3, 98);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(48, 19);
            label9.TabIndex = 93;
            label9.Text = "Radius";
            // 
            // radioButtonApplyEquivalentAtoms
            // 
            radioButtonApplyEquivalentAtoms.AutoSize = true;
            radioButtonApplyEquivalentAtoms.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radioButtonApplyEquivalentAtoms.Location = new System.Drawing.Point(107, 143);
            radioButtonApplyEquivalentAtoms.Name = "radioButtonApplyEquivalentAtoms";
            radioButtonApplyEquivalentAtoms.Size = new System.Drawing.Size(151, 19);
            radioButtonApplyEquivalentAtoms.TabIndex = 97;
            radioButtonApplyEquivalentAtoms.Text = "Apply equivalent atoms";
            toolTip1.SetToolTip(radioButtonApplyEquivalentAtoms, resources.GetString("radioButtonApplyEquivalentAtoms.ToolTip"));
            radioButtonApplyEquivalentAtoms.UseVisualStyleBackColor = true;
            // 
            // radioButtonApplyThis
            // 
            radioButtonApplyThis.AutoSize = true;
            radioButtonApplyThis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radioButtonApplyThis.Location = new System.Drawing.Point(107, 159);
            radioButtonApplyThis.Name = "radioButtonApplyThis";
            radioButtonApplyThis.Size = new System.Drawing.Size(133, 19);
            radioButtonApplyThis.TabIndex = 97;
            radioButtonApplyThis.Text = "Apply only this atom";
            toolTip1.SetToolTip(radioButtonApplyThis, resources.GetString("radioButtonApplyThis.ToolTip"));
            radioButtonApplyThis.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonCancel.Location = new System.Drawing.Point(180, 184);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(72, 24);
            buttonCancel.TabIndex = 100;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = false;
            // 
            // buttonOK
            // 
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonOK.Location = new System.Drawing.Point(101, 184);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(72, 24);
            buttonOK.TabIndex = 99;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = false;
            // 
            // checkBoxIsDraw
            // 
            checkBoxIsDraw.AutoSize = true;
            checkBoxIsDraw.Checked = true;
            checkBoxIsDraw.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBoxIsDraw.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            checkBoxIsDraw.Location = new System.Drawing.Point(150, 95);
            checkBoxIsDraw.Name = "checkBoxIsDraw";
            checkBoxIsDraw.Size = new System.Drawing.Size(63, 19);
            checkBoxIsDraw.TabIndex = 101;
            checkBoxIsDraw.Text = "Visible";
            toolTip1.SetToolTip(checkBoxIsDraw, resources.GetString("checkBoxIsDraw.ToolTip"));
            checkBoxIsDraw.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllSameElement
            // 
            radioButtonAllSameElement.AutoSize = true;
            radioButtonAllSameElement.Checked = true;
            radioButtonAllSameElement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radioButtonAllSameElement.Location = new System.Drawing.Point(107, 126);
            radioButtonAllSameElement.Name = "radioButtonAllSameElement";
            radioButtonAllSameElement.Size = new System.Drawing.Size(153, 19);
            radioButtonAllSameElement.TabIndex = 97;
            radioButtonAllSameElement.TabStop = true;
            radioButtonAllSameElement.Text = "Apply all same element";
            toolTip1.SetToolTip(radioButtonAllSameElement, resources.GetString("radioButtonAllSameElement.ToolTip"));
            radioButtonAllSameElement.UseVisualStyleBackColor = true;
            // 
            // FormAtom
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new System.Drawing.Size(467, 213);
            ControlBox = false;
            Controls.Add(checkBoxIsDraw);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(radioButtonApplyThis);
            Controls.Add(radioButtonAllSameElement);
            Controls.Add(radioButtonApplyEquivalentAtoms);
            Controls.Add(groupBoxMaterial);
            Controls.Add(pictureBoxAtomColor);
            Controls.Add(label8);
            Controls.Add(numericBoxAtomRadius);
            Controls.Add(label9);
            Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Name = "FormAtom";
            Text = "Atom property";
            groupBoxMaterial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(pictureBoxAtomColor)).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMaterial;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton radioButtonApplyEquivalentAtoms;
        private System.Windows.Forms.RadioButton radioButtonApplyThis;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        public Crystallography.Controls.NumericBox numericBoxAtomTransparency;
        public Crystallography.Controls.NumericBox numericBoxAtomShininess;
        public Crystallography.Controls.NumericBox numericBoxAtomSpecular;
        public Crystallography.Controls.NumericBox numericBoxAtomEmmision;
        public Crystallography.Controls.NumericBox numericBoxAtomDiffusion;
        public Crystallography.Controls.NumericBox numericBoxAtomAmbient;
        public System.Windows.Forms.PictureBox pictureBoxAtomColor;
        public Crystallography.Controls.NumericBox numericBoxAtomRadius;
        private System.Windows.Forms.RadioButton radioButtonAllSameElement;
        public System.Windows.Forms.CheckBox checkBoxIsDraw;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}