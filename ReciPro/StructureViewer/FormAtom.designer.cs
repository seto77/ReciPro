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
            this.components = new System.ComponentModel.Container();
            this.groupBoxMaterial = new System.Windows.Forms.GroupBox();
            this.numericBoxAtomTransparency = new Crystallography.Controls.NumericBox();
            this.numericBoxAtomShininess = new Crystallography.Controls.NumericBox();
            this.numericBoxAtomSpecular = new Crystallography.Controls.NumericBox();
            this.numericBoxAtomEmmision = new Crystallography.Controls.NumericBox();
            this.numericBoxAtomDiffusion = new Crystallography.Controls.NumericBox();
            this.label37 = new System.Windows.Forms.Label();
            this.numericBoxAtomAmbient = new Crystallography.Controls.NumericBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBoxAtomColor = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericBoxAtomRadius = new Crystallography.Controls.NumericBox();
            this.label9 = new System.Windows.Forms.Label();
            this.radioButtonApplyEquivalentAtoms = new System.Windows.Forms.RadioButton();
            this.radioButtonApplyThis = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxIsDraw = new System.Windows.Forms.CheckBox();
            this.radioButtonAllSameElement = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip1.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            this.groupBoxMaterial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAtomColor)).BeginInit();                                                          // 260522Cl: numericBoxAtom* は NumericBox 化に伴い ISupportInitialize 不要
            this.SuspendLayout();
            // 
            // groupBoxMaterial
            // 
            this.groupBoxMaterial.Controls.Add(this.numericBoxAtomTransparency);
            this.groupBoxMaterial.Controls.Add(this.numericBoxAtomShininess);
            this.groupBoxMaterial.Controls.Add(this.numericBoxAtomSpecular);
            this.groupBoxMaterial.Controls.Add(this.numericBoxAtomEmmision);
            this.groupBoxMaterial.Controls.Add(this.numericBoxAtomDiffusion);
            this.groupBoxMaterial.Controls.Add(this.label37);
            this.groupBoxMaterial.Controls.Add(this.numericBoxAtomAmbient);
            this.groupBoxMaterial.Controls.Add(this.label36);
            this.groupBoxMaterial.Controls.Add(this.label38);
            this.groupBoxMaterial.Controls.Add(this.label35);
            this.groupBoxMaterial.Controls.Add(this.label34);
            this.groupBoxMaterial.Controls.Add(this.label12);
            this.groupBoxMaterial.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxMaterial.Location = new System.Drawing.Point(0, 0);
            this.groupBoxMaterial.Name = "groupBoxMaterial";
            this.groupBoxMaterial.Size = new System.Drawing.Size(211, 91);
            this.groupBoxMaterial.TabIndex = 96;
            this.groupBoxMaterial.TabStop = false;
            this.groupBoxMaterial.Text = "Material";
            // 
            // numericBoxAtomTransparency
            // 
            this.numericBoxAtomTransparency.DecimalPlaces = 1;
            this.numericBoxAtomTransparency.UpDown_Increment = 0.1D;
            this.numericBoxAtomTransparency.Location = new System.Drawing.Point(165, 63);
            this.numericBoxAtomTransparency.Maximum = 1D;
            this.numericBoxAtomTransparency.Name = "numericBoxAtomTransparency";
            this.numericBoxAtomTransparency.Minimum = 0D;                                                                                              // 260522Cl 追加: NumericUpDown 既定 Minimum=0 を維持
            this.numericBoxAtomTransparency.ShowUpDown = true;
            this.numericBoxAtomTransparency.Size = new System.Drawing.Size(41, 21);
            this.numericBoxAtomTransparency.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericBoxAtomTransparency, resources.GetString("numericBoxAtomTransparency.ToolTip"));
            this.numericBoxAtomTransparency.Value = 1D;
            // 
            // numericBoxAtomShininess
            // 
            this.numericBoxAtomShininess.Location = new System.Drawing.Point(165, 39);
            this.numericBoxAtomShininess.Maximum = 50D;
            this.numericBoxAtomShininess.Name = "numericBoxAtomShininess";
            this.numericBoxAtomShininess.DecimalPlaces = 0;                                                                                            // 260522Cl 追加: NumericUpDown 既定 DecimalPlaces=0 (整数表示) を維持
            this.numericBoxAtomShininess.Minimum = 0D;
            this.numericBoxAtomShininess.ShowUpDown = true;
            this.numericBoxAtomShininess.Size = new System.Drawing.Size(41, 21);
            this.numericBoxAtomShininess.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericBoxAtomShininess, resources.GetString("numericBoxAtomShininess.ToolTip"));
            // 
            // numericBoxAtomSpecular
            // 
            this.numericBoxAtomSpecular.DecimalPlaces = 1;
            this.numericBoxAtomSpecular.UpDown_Increment = 0.1D;
            this.numericBoxAtomSpecular.Location = new System.Drawing.Point(165, 14);
            this.numericBoxAtomSpecular.Maximum = 1D;
            this.numericBoxAtomSpecular.Name = "numericBoxAtomSpecular";
            this.numericBoxAtomSpecular.Minimum = 0D;
            this.numericBoxAtomSpecular.ShowUpDown = true;
            this.numericBoxAtomSpecular.Size = new System.Drawing.Size(41, 21);
            this.numericBoxAtomSpecular.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericBoxAtomSpecular, resources.GetString("numericBoxAtomSpecular.ToolTip"));
            // 
            // numericBoxAtomEmmision
            // 
            this.numericBoxAtomEmmision.DecimalPlaces = 1;
            this.numericBoxAtomEmmision.UpDown_Increment = 0.1D;
            this.numericBoxAtomEmmision.Location = new System.Drawing.Point(69, 64);
            this.numericBoxAtomEmmision.Maximum = 1D;
            this.numericBoxAtomEmmision.Name = "numericBoxAtomEmmision";
            this.numericBoxAtomEmmision.Minimum = 0D;
            this.numericBoxAtomEmmision.ShowUpDown = true;
            this.numericBoxAtomEmmision.Size = new System.Drawing.Size(41, 21);
            this.numericBoxAtomEmmision.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericBoxAtomEmmision, resources.GetString("numericBoxAtomEmmision.ToolTip"));
            // 
            // numericBoxAtomDiffusion
            // 
            this.numericBoxAtomDiffusion.DecimalPlaces = 1;
            this.numericBoxAtomDiffusion.UpDown_Increment = 0.1D;
            this.numericBoxAtomDiffusion.Location = new System.Drawing.Point(60, 38);
            this.numericBoxAtomDiffusion.Maximum = 1D;
            this.numericBoxAtomDiffusion.Name = "numericBoxAtomDiffusion";
            this.numericBoxAtomDiffusion.Minimum = 0D;
            this.numericBoxAtomDiffusion.ShowUpDown = true;
            this.numericBoxAtomDiffusion.Size = new System.Drawing.Size(41, 21);
            this.numericBoxAtomDiffusion.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericBoxAtomDiffusion, resources.GetString("numericBoxAtomDiffusion.ToolTip"));
            this.numericBoxAtomDiffusion.Value = 0.7D;
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(125, 67);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(40, 19);
            this.label37.TabIndex = 88;
            this.label37.Text = "Alpha";
            // 
            // numericBoxAtomAmbient
            // 
            this.numericBoxAtomAmbient.DecimalPlaces = 1;
            this.numericBoxAtomAmbient.UpDown_Increment = 0.1D;
            this.numericBoxAtomAmbient.Location = new System.Drawing.Point(60, 14);
            this.numericBoxAtomAmbient.Maximum = 1D;
            this.numericBoxAtomAmbient.Name = "numericBoxAtomAmbient";
            this.numericBoxAtomAmbient.Minimum = 0D;
            this.numericBoxAtomAmbient.ShowUpDown = true;
            this.numericBoxAtomAmbient.Size = new System.Drawing.Size(41, 21);
            this.numericBoxAtomAmbient.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericBoxAtomAmbient, resources.GetString("numericBoxAtomAmbient.ToolTip"));
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(104, 43);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(76, 16);
            this.label36.TabIndex = 88;
            this.label36.Text = "Shininess";
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(106, 16);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(76, 19);
            this.label38.TabIndex = 88;
            this.label38.Text = "Specular";
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(4, 66);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(76, 19);
            this.label35.TabIndex = 88;
            this.label35.Text = "Emission";
            // 
            // label34
            // 
            this.label34.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(4, 42);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(59, 17);
            this.label34.TabIndex = 88;
            this.label34.Text = "Diffusion";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 19);
            this.label12.TabIndex = 88;
            this.label12.Text = "Ambient";
            // 
            // pictureBoxAtomColor
            // 
            this.pictureBoxAtomColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxAtomColor.Location = new System.Drawing.Point(43, 122);
            this.pictureBoxAtomColor.Name = "pictureBoxAtomColor";
            this.pictureBoxAtomColor.Size = new System.Drawing.Size(20, 21);
            this.pictureBoxAtomColor.TabIndex = 95;
            this.pictureBoxAtomColor.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxAtomColor, resources.GetString("pictureBoxAtomColor.ToolTip"));
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 19);
            this.label8.TabIndex = 92;
            this.label8.Text = "Color";
            // 
            // numericBoxAtomRadius
            // 
            this.numericBoxAtomRadius.DecimalPlaces = 3;
            this.numericBoxAtomRadius.UpDown_Increment = 0.1D;
            this.numericBoxAtomRadius.Location = new System.Drawing.Point(47, 94);
            this.numericBoxAtomRadius.Maximum = 9.9D;
            this.numericBoxAtomRadius.Name = "numericBoxAtomRadius";
            this.numericBoxAtomRadius.Minimum = 0D;
            this.numericBoxAtomRadius.ShowUpDown = true;
            this.numericBoxAtomRadius.Size = new System.Drawing.Size(60, 21);
            this.numericBoxAtomRadius.TabIndex = 94;
            this.toolTip1.SetToolTip(this.numericBoxAtomRadius, resources.GetString("numericBoxAtomRadius.ToolTip"));
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 19);
            this.label9.TabIndex = 93;
            this.label9.Text = "Radius";
            // 
            // radioButtonApplyEquivalentAtoms
            // 
            this.radioButtonApplyEquivalentAtoms.AutoSize = true;
            this.radioButtonApplyEquivalentAtoms.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonApplyEquivalentAtoms.Location = new System.Drawing.Point(107, 143);
            this.radioButtonApplyEquivalentAtoms.Name = "radioButtonApplyEquivalentAtoms";
            this.radioButtonApplyEquivalentAtoms.Size = new System.Drawing.Size(151, 19);
            this.radioButtonApplyEquivalentAtoms.TabIndex = 97;
            this.radioButtonApplyEquivalentAtoms.Text = "Apply equivalent atoms";
            this.toolTip1.SetToolTip(this.radioButtonApplyEquivalentAtoms, resources.GetString("radioButtonApplyEquivalentAtoms.ToolTip"));
            this.radioButtonApplyEquivalentAtoms.UseVisualStyleBackColor = true;
            // 
            // radioButtonApplyThis
            // 
            this.radioButtonApplyThis.AutoSize = true;
            this.radioButtonApplyThis.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonApplyThis.Location = new System.Drawing.Point(107, 159);
            this.radioButtonApplyThis.Name = "radioButtonApplyThis";
            this.radioButtonApplyThis.Size = new System.Drawing.Size(133, 19);
            this.radioButtonApplyThis.TabIndex = 97;
            this.radioButtonApplyThis.Text = "Apply only this atom";
            this.toolTip1.SetToolTip(this.radioButtonApplyThis, resources.GetString("radioButtonApplyThis.ToolTip"));
            this.radioButtonApplyThis.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(180, 184);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(72, 24);
            this.buttonCancel.TabIndex = 100;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.Location = new System.Drawing.Point(101, 184);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(72, 24);
            this.buttonOK.TabIndex = 99;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            // 
            // checkBoxIsDraw
            // 
            this.checkBoxIsDraw.AutoSize = true;
            this.checkBoxIsDraw.Checked = true;
            this.checkBoxIsDraw.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsDraw.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxIsDraw.Location = new System.Drawing.Point(150, 95);
            this.checkBoxIsDraw.Name = "checkBoxIsDraw";
            this.checkBoxIsDraw.Size = new System.Drawing.Size(63, 19);
            this.checkBoxIsDraw.TabIndex = 101;
            this.checkBoxIsDraw.Text = "Visible";
            this.toolTip1.SetToolTip(this.checkBoxIsDraw, resources.GetString("checkBoxIsDraw.ToolTip"));
            this.checkBoxIsDraw.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllSameElement
            // 
            this.radioButtonAllSameElement.AutoSize = true;
            this.radioButtonAllSameElement.Checked = true;
            this.radioButtonAllSameElement.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAllSameElement.Location = new System.Drawing.Point(107, 126);
            this.radioButtonAllSameElement.Name = "radioButtonAllSameElement";
            this.radioButtonAllSameElement.Size = new System.Drawing.Size(153, 19);
            this.radioButtonAllSameElement.TabIndex = 97;
            this.radioButtonAllSameElement.TabStop = true;
            this.radioButtonAllSameElement.Text = "Apply all same element";
            this.toolTip1.SetToolTip(this.radioButtonAllSameElement, resources.GetString("radioButtonAllSameElement.ToolTip"));
            this.radioButtonAllSameElement.UseVisualStyleBackColor = true;
            // 
            // FormAtom
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(467, 213);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxIsDraw);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.radioButtonApplyThis);
            this.Controls.Add(this.radioButtonAllSameElement);
            this.Controls.Add(this.radioButtonApplyEquivalentAtoms);
            this.Controls.Add(this.groupBoxMaterial);
            this.Controls.Add(this.pictureBoxAtomColor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericBoxAtomRadius);
            this.Controls.Add(this.label9);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormAtom";
            this.Text = "Atom property";
            this.groupBoxMaterial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAtomColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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