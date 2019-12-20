namespace ReciPro
{
    partial class FormAtom
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numericUpDownAtomTransparency = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAtomShininess = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAtomSpecular = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAtomEmmision = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownAtomDiffusion = new System.Windows.Forms.NumericUpDown();
            this.label37 = new System.Windows.Forms.Label();
            this.numericUpDownAtomAmbient = new System.Windows.Forms.NumericUpDown();
            this.label36 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBoxAtomColor = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownAtomRadius = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.radioButtonApplyEquivalentAtoms = new System.Windows.Forms.RadioButton();
            this.radioButtonApplyThis = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxIsDraw = new System.Windows.Forms.CheckBox();
            this.radioButtonAllSameElement = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomTransparency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomShininess)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomSpecular)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomEmmision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomDiffusion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomAmbient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAtomColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomRadius)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownAtomTransparency);
            this.groupBox1.Controls.Add(this.numericUpDownAtomShininess);
            this.groupBox1.Controls.Add(this.numericUpDownAtomSpecular);
            this.groupBox1.Controls.Add(this.numericUpDownAtomEmmision);
            this.groupBox1.Controls.Add(this.numericUpDownAtomDiffusion);
            this.groupBox1.Controls.Add(this.label37);
            this.groupBox1.Controls.Add(this.numericUpDownAtomAmbient);
            this.groupBox1.Controls.Add(this.label36);
            this.groupBox1.Controls.Add(this.label38);
            this.groupBox1.Controls.Add(this.label35);
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 91);
            this.groupBox1.TabIndex = 96;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Material";
            // 
            // numericUpDownAtomTransparency
            // 
            this.numericUpDownAtomTransparency.DecimalPlaces = 1;
            this.numericUpDownAtomTransparency.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomTransparency.Location = new System.Drawing.Point(165, 63);
            this.numericUpDownAtomTransparency.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtomTransparency.Name = "numericUpDownAtomTransparency";
            this.numericUpDownAtomTransparency.Size = new System.Drawing.Size(41, 21);
            this.numericUpDownAtomTransparency.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericUpDownAtomTransparency, "透明度");
            this.numericUpDownAtomTransparency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownAtomShininess
            // 
            this.numericUpDownAtomShininess.Location = new System.Drawing.Point(165, 39);
            this.numericUpDownAtomShininess.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownAtomShininess.Name = "numericUpDownAtomShininess";
            this.numericUpDownAtomShininess.Size = new System.Drawing.Size(41, 21);
            this.numericUpDownAtomShininess.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericUpDownAtomShininess, "反射光の強度");
            // 
            // numericUpDownAtomSpecular
            // 
            this.numericUpDownAtomSpecular.DecimalPlaces = 1;
            this.numericUpDownAtomSpecular.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAtomSpecular.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomSpecular.Location = new System.Drawing.Point(165, 14);
            this.numericUpDownAtomSpecular.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtomSpecular.Name = "numericUpDownAtomSpecular";
            this.numericUpDownAtomSpecular.Size = new System.Drawing.Size(41, 21);
            this.numericUpDownAtomSpecular.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericUpDownAtomSpecular, "反射光");
            // 
            // numericUpDownAtomEmmision
            // 
            this.numericUpDownAtomEmmision.DecimalPlaces = 1;
            this.numericUpDownAtomEmmision.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAtomEmmision.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomEmmision.Location = new System.Drawing.Point(69, 64);
            this.numericUpDownAtomEmmision.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtomEmmision.Name = "numericUpDownAtomEmmision";
            this.numericUpDownAtomEmmision.Size = new System.Drawing.Size(41, 21);
            this.numericUpDownAtomEmmision.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericUpDownAtomEmmision, "放射光");
            // 
            // numericUpDownAtomDiffusion
            // 
            this.numericUpDownAtomDiffusion.DecimalPlaces = 1;
            this.numericUpDownAtomDiffusion.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAtomDiffusion.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomDiffusion.Location = new System.Drawing.Point(60, 38);
            this.numericUpDownAtomDiffusion.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtomDiffusion.Name = "numericUpDownAtomDiffusion";
            this.numericUpDownAtomDiffusion.Size = new System.Drawing.Size(41, 21);
            this.numericUpDownAtomDiffusion.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericUpDownAtomDiffusion, "拡散光");
            this.numericUpDownAtomDiffusion.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(125, 67);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(40, 19);
            this.label37.TabIndex = 88;
            this.label37.Text = "Alpha";
            // 
            // numericUpDownAtomAmbient
            // 
            this.numericUpDownAtomAmbient.DecimalPlaces = 1;
            this.numericUpDownAtomAmbient.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAtomAmbient.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomAmbient.Location = new System.Drawing.Point(60, 14);
            this.numericUpDownAtomAmbient.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAtomAmbient.Name = "numericUpDownAtomAmbient";
            this.numericUpDownAtomAmbient.Size = new System.Drawing.Size(41, 21);
            this.numericUpDownAtomAmbient.TabIndex = 89;
            this.toolTip1.SetToolTip(this.numericUpDownAtomAmbient, "環境光");
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(104, 43);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(76, 16);
            this.label36.TabIndex = 88;
            this.label36.Text = "Shininess";
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(106, 16);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(76, 19);
            this.label38.TabIndex = 88;
            this.label38.Text = "Specular";
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(4, 66);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(76, 19);
            this.label35.TabIndex = 88;
            this.label35.Text = "Emission";
            // 
            // label34
            // 
            this.label34.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(4, 42);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(59, 17);
            this.label34.TabIndex = 88;
            this.label34.Text = "Diffusion";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.toolTip1.SetToolTip(this.pictureBoxAtomColor, "原子の色を設定します");
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 19);
            this.label8.TabIndex = 92;
            this.label8.Text = "Color";
            // 
            // numericUpDownAtomRadius
            // 
            this.numericUpDownAtomRadius.DecimalPlaces = 3;
            this.numericUpDownAtomRadius.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAtomRadius.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownAtomRadius.Location = new System.Drawing.Point(47, 94);
            this.numericUpDownAtomRadius.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            65536});
            this.numericUpDownAtomRadius.Name = "numericUpDownAtomRadius";
            this.numericUpDownAtomRadius.Size = new System.Drawing.Size(60, 21);
            this.numericUpDownAtomRadius.TabIndex = 94;
            this.toolTip1.SetToolTip(this.numericUpDownAtomRadius, "原子の半径を設定します。");
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 19);
            this.label9.TabIndex = 93;
            this.label9.Text = "Radius";
            // 
            // radioButtonApplyEquivalentAtoms
            // 
            this.radioButtonApplyEquivalentAtoms.AutoSize = true;
            this.radioButtonApplyEquivalentAtoms.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonApplyEquivalentAtoms.Location = new System.Drawing.Point(107, 143);
            this.radioButtonApplyEquivalentAtoms.Name = "radioButtonApplyEquivalentAtoms";
            this.radioButtonApplyEquivalentAtoms.Size = new System.Drawing.Size(151, 19);
            this.radioButtonApplyEquivalentAtoms.TabIndex = 97;
            this.radioButtonApplyEquivalentAtoms.Text = "Apply equivalent atoms";
            this.toolTip1.SetToolTip(this.radioButtonApplyEquivalentAtoms, "等価な位置にある原子に設定内容を反映させます。");
            this.radioButtonApplyEquivalentAtoms.UseVisualStyleBackColor = true;
            // 
            // radioButtonApplyThis
            // 
            this.radioButtonApplyThis.AutoSize = true;
            this.radioButtonApplyThis.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonApplyThis.Location = new System.Drawing.Point(107, 159);
            this.radioButtonApplyThis.Name = "radioButtonApplyThis";
            this.radioButtonApplyThis.Size = new System.Drawing.Size(133, 19);
            this.radioButtonApplyThis.TabIndex = 97;
            this.radioButtonApplyThis.Text = "Apply only this atom";
            this.toolTip1.SetToolTip(this.radioButtonApplyThis, "選択された原子にのみ設定内容を反映させます。");
            this.radioButtonApplyThis.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.buttonOK.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.checkBoxIsDraw.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxIsDraw.Location = new System.Drawing.Point(150, 95);
            this.checkBoxIsDraw.Name = "checkBoxIsDraw";
            this.checkBoxIsDraw.Size = new System.Drawing.Size(63, 19);
            this.checkBoxIsDraw.TabIndex = 101;
            this.checkBoxIsDraw.Text = "Visible";
            this.toolTip1.SetToolTip(this.checkBoxIsDraw, "一時的に原子の描画を停止します。\r\n元の結晶構造には影響しません。");
            this.checkBoxIsDraw.UseVisualStyleBackColor = true;
            // 
            // radioButtonAllSameElement
            // 
            this.radioButtonAllSameElement.AutoSize = true;
            this.radioButtonAllSameElement.Checked = true;
            this.radioButtonAllSameElement.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonAllSameElement.Location = new System.Drawing.Point(107, 126);
            this.radioButtonAllSameElement.Name = "radioButtonAllSameElement";
            this.radioButtonAllSameElement.Size = new System.Drawing.Size(153, 19);
            this.radioButtonAllSameElement.TabIndex = 97;
            this.radioButtonAllSameElement.TabStop = true;
            this.radioButtonAllSameElement.Text = "Apply all same element";
            this.toolTip1.SetToolTip(this.radioButtonAllSameElement, "同種の元素の全てに設定内容を反映させます。");
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
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBoxAtomColor);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.numericUpDownAtomRadius);
            this.Controls.Add(this.label9);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormAtom";
            this.Text = "Atom property";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomTransparency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomShininess)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomSpecular)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomEmmision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomDiffusion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomAmbient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAtomColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomRadius)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
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
        public System.Windows.Forms.NumericUpDown numericUpDownAtomTransparency;
        public System.Windows.Forms.NumericUpDown numericUpDownAtomShininess;
        public System.Windows.Forms.NumericUpDown numericUpDownAtomSpecular;
        public System.Windows.Forms.NumericUpDown numericUpDownAtomEmmision;
        public System.Windows.Forms.NumericUpDown numericUpDownAtomDiffusion;
        public System.Windows.Forms.NumericUpDown numericUpDownAtomAmbient;
        public System.Windows.Forms.PictureBox pictureBoxAtomColor;
        public System.Windows.Forms.NumericUpDown numericUpDownAtomRadius;
        private System.Windows.Forms.RadioButton radioButtonAllSameElement;
        public System.Windows.Forms.CheckBox checkBoxIsDraw;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}