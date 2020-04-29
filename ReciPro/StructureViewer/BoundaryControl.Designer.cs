namespace ReciPro
{
    partial class BoundsControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.checkBoxEnable = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxEquivalency = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.numericBoxH = new Crystallography.Controls.NumericBox();
            this.numericBoxK = new Crystallography.Controls.NumericBox();
            this.numericBoxL = new Crystallography.Controls.NumericBox();
            this.numericBoxDistance = new Crystallography.Controls.NumericBox();
            this.numericBox1 = new Crystallography.Controls.NumericBox();
            this.colorControl = new Crystallography.Controls.ColorControl();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.checkBoxEnable);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.checkBoxEquivalency);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel4);
            this.flowLayoutPanel1.Controls.Add(this.colorControl);
            this.flowLayoutPanel1.Controls.Add(this.buttonDelete);
            this.flowLayoutPanel1.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(554, 31);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // checkBoxEnable
            // 
            this.checkBoxEnable.AutoSize = true;
            this.checkBoxEnable.Checked = true;
            this.checkBoxEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnable.Location = new System.Drawing.Point(15, 8);
            this.checkBoxEnable.Margin = new System.Windows.Forms.Padding(15, 8, 15, 0);
            this.checkBoxEnable.Name = "checkBoxEnable";
            this.checkBoxEnable.Size = new System.Drawing.Size(15, 14);
            this.checkBoxEnable.TabIndex = 4;
            this.checkBoxEnable.UseVisualStyleBackColor = true;
            this.checkBoxEnable.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Controls.Add(this.numericBoxH);
            this.flowLayoutPanel2.Controls.Add(this.numericBoxK);
            this.flowLayoutPanel2.Controls.Add(this.numericBoxL);
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(45, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(153, 31);
            this.flowLayoutPanel2.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label3.Location = new System.Drawing.Point(0, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "{";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label1.Location = new System.Drawing.Point(141, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "}";
            // 
            // checkBoxEquivalency
            // 
            this.checkBoxEquivalency.AutoSize = true;
            this.checkBoxEquivalency.Checked = true;
            this.checkBoxEquivalency.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEquivalency.Location = new System.Drawing.Point(228, 8);
            this.checkBoxEquivalency.Margin = new System.Windows.Forms.Padding(30, 8, 15, 0);
            this.checkBoxEquivalency.Name = "checkBoxEquivalency";
            this.checkBoxEquivalency.Size = new System.Drawing.Size(15, 14);
            this.checkBoxEquivalency.TabIndex = 8;
            this.checkBoxEquivalency.UseVisualStyleBackColor = true;
            this.checkBoxEquivalency.CheckedChanged += new System.EventHandler(this.checkBoxEquivalency_CheckedChanged);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Font = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(268, 0);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(0, 0);
            this.flowLayoutPanel3.TabIndex = 8;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoSize = true;
            this.flowLayoutPanel4.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel4.Controls.Add(this.numericBoxDistance);
            this.flowLayoutPanel4.Controls.Add(this.label2);
            this.flowLayoutPanel4.Controls.Add(this.numericBox1);
            this.flowLayoutPanel4.Controls.Add(this.label4);
            this.flowLayoutPanel4.Location = new System.Drawing.Point(271, 0);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(173, 31);
            this.flowLayoutPanel4.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label2.Location = new System.Drawing.Point(52, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Å (= d ×";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.label4.Location = new System.Drawing.Point(161, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = ")";
            // 
            // buttonDelete
            // 
            this.buttonDelete.AutoSize = true;
            this.buttonDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonDelete.BackColor = System.Drawing.Color.IndianRed;
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(499, 2);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 2, 0, 0);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(55, 27);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // numericBoxH
            // 
            this.numericBoxH.AllowMouseControl = false;
            this.numericBoxH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxH.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxH.DecimalPlaces = -2;
            this.numericBoxH.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxH.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxH.FooterText = "";
            this.numericBoxH.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxH.HeaderText = "";
            this.numericBoxH.Location = new System.Drawing.Point(12, 3);
            this.numericBoxH.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.numericBoxH.Maximum = 10D;
            this.numericBoxH.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxH.Minimum = -10D;
            this.numericBoxH.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxH.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxH.MouseSpeed = 1D;
            this.numericBoxH.Multiline = false;
            this.numericBoxH.Name = "numericBoxH";
            this.numericBoxH.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxH.RadianValue = -0.087266462599716474D;
            this.numericBoxH.ReadOnly = false;
            this.numericBoxH.RestrictLimitValue = true;
            this.numericBoxH.ShowFraction = false;
            this.numericBoxH.ShowPositiveSign = false;
            this.numericBoxH.ShowUpDown = true;
            this.numericBoxH.Size = new System.Drawing.Size(40, 25);
            this.numericBoxH.SkipEventDuringInput = false;
            this.numericBoxH.SmartIncrement = false;
            this.numericBoxH.TabIndex = 0;
            this.numericBoxH.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxH.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxH.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxH.ThonsandsSeparator = true;
            this.numericBoxH.ToolTip = "";
            this.numericBoxH.UpDown_Increment = 1D;
            this.numericBoxH.Value = -5D;
            this.numericBoxH.WordWrap = true;
            this.numericBoxH.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.ValueChanged);
            // 
            // numericBoxK
            // 
            this.numericBoxK.AllowMouseControl = false;
            this.numericBoxK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxK.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxK.DecimalPlaces = -2;
            this.numericBoxK.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxK.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxK.FooterText = "";
            this.numericBoxK.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxK.HeaderText = "";
            this.numericBoxK.Location = new System.Drawing.Point(55, 3);
            this.numericBoxK.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.numericBoxK.Maximum = 10D;
            this.numericBoxK.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxK.Minimum = -10D;
            this.numericBoxK.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxK.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxK.MouseSpeed = 1D;
            this.numericBoxK.Multiline = false;
            this.numericBoxK.Name = "numericBoxK";
            this.numericBoxK.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxK.RadianValue = -0.087266462599716474D;
            this.numericBoxK.ReadOnly = false;
            this.numericBoxK.RestrictLimitValue = true;
            this.numericBoxK.ShowFraction = false;
            this.numericBoxK.ShowPositiveSign = false;
            this.numericBoxK.ShowUpDown = true;
            this.numericBoxK.Size = new System.Drawing.Size(40, 25);
            this.numericBoxK.SkipEventDuringInput = false;
            this.numericBoxK.SmartIncrement = false;
            this.numericBoxK.TabIndex = 0;
            this.numericBoxK.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxK.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxK.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxK.ThonsandsSeparator = true;
            this.numericBoxK.ToolTip = "";
            this.numericBoxK.UpDown_Increment = 1D;
            this.numericBoxK.Value = -5D;
            this.numericBoxK.WordWrap = true;
            this.numericBoxK.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.ValueChanged);
            // 
            // numericBoxL
            // 
            this.numericBoxL.AllowMouseControl = false;
            this.numericBoxL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxL.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxL.DecimalPlaces = -2;
            this.numericBoxL.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxL.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxL.FooterText = "";
            this.numericBoxL.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxL.HeaderText = "";
            this.numericBoxL.Location = new System.Drawing.Point(101, 3);
            this.numericBoxL.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.numericBoxL.Maximum = 10D;
            this.numericBoxL.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxL.Minimum = -10D;
            this.numericBoxL.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxL.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxL.MouseSpeed = 1D;
            this.numericBoxL.Multiline = false;
            this.numericBoxL.Name = "numericBoxL";
            this.numericBoxL.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxL.RadianValue = -0.087266462599716474D;
            this.numericBoxL.ReadOnly = false;
            this.numericBoxL.RestrictLimitValue = true;
            this.numericBoxL.ShowFraction = false;
            this.numericBoxL.ShowPositiveSign = false;
            this.numericBoxL.ShowUpDown = true;
            this.numericBoxL.Size = new System.Drawing.Size(40, 25);
            this.numericBoxL.SkipEventDuringInput = false;
            this.numericBoxL.SmartIncrement = false;
            this.numericBoxL.TabIndex = 0;
            this.numericBoxL.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxL.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxL.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxL.ThonsandsSeparator = true;
            this.numericBoxL.ToolTip = "";
            this.numericBoxL.UpDown_Increment = 1D;
            this.numericBoxL.Value = -5D;
            this.numericBoxL.WordWrap = true;
            this.numericBoxL.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.ValueChanged);
            // 
            // numericBoxDistance
            // 
            this.numericBoxDistance.AllowMouseControl = false;
            this.numericBoxDistance.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBoxDistance.BackColor = System.Drawing.SystemColors.Control;
            this.numericBoxDistance.DecimalPlaces = 4;
            this.numericBoxDistance.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.FooterText = "";
            this.numericBoxDistance.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.HeaderText = "";
            this.numericBoxDistance.Location = new System.Drawing.Point(5, 3);
            this.numericBoxDistance.Margin = new System.Windows.Forms.Padding(5, 3, 0, 3);
            this.numericBoxDistance.Maximum = 100D;
            this.numericBoxDistance.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxDistance.Minimum = 0D;
            this.numericBoxDistance.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxDistance.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBoxDistance.MouseSpeed = 1D;
            this.numericBoxDistance.Multiline = false;
            this.numericBoxDistance.Name = "numericBoxDistance";
            this.numericBoxDistance.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBoxDistance.RadianValue = 0.0087266462599716477D;
            this.numericBoxDistance.ReadOnly = false;
            this.numericBoxDistance.RestrictLimitValue = true;
            this.numericBoxDistance.ShowFraction = false;
            this.numericBoxDistance.ShowPositiveSign = false;
            this.numericBoxDistance.ShowUpDown = false;
            this.numericBoxDistance.Size = new System.Drawing.Size(47, 25);
            this.numericBoxDistance.SkipEventDuringInput = false;
            this.numericBoxDistance.SmartIncrement = false;
            this.numericBoxDistance.TabIndex = 0;
            this.numericBoxDistance.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBoxDistance.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBoxDistance.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxDistance.ThonsandsSeparator = true;
            this.numericBoxDistance.ToolTip = "";
            this.numericBoxDistance.UpDown_Increment = 0.1D;
            this.numericBoxDistance.Value = 0.5D;
            this.numericBoxDistance.WordWrap = true;
            this.numericBoxDistance.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.ValueChanged);
            // 
            // numericBox1
            // 
            this.numericBox1.AllowMouseControl = false;
            this.numericBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericBox1.BackColor = System.Drawing.SystemColors.Control;
            this.numericBox1.DecimalPlaces = 2;
            this.numericBox1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBox1.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBox1.FooterText = "";
            this.numericBox1.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBox1.HeaderText = "";
            this.numericBox1.Location = new System.Drawing.Point(110, 3);
            this.numericBox1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.numericBox1.Maximum = 100D;
            this.numericBox1.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBox1.Minimum = 0D;
            this.numericBox1.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBox1.MouseDirection = Crystallography.VH_DirectionEnum.Vertical;
            this.numericBox1.MouseSpeed = 1D;
            this.numericBox1.Multiline = false;
            this.numericBox1.Name = "numericBox1";
            this.numericBox1.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0);
            this.numericBox1.RadianValue = 0.0087266462599716477D;
            this.numericBox1.ReadOnly = false;
            this.numericBox1.RestrictLimitValue = true;
            this.numericBox1.ShowFraction = false;
            this.numericBox1.ShowPositiveSign = false;
            this.numericBox1.ShowUpDown = true;
            this.numericBox1.Size = new System.Drawing.Size(51, 25);
            this.numericBox1.SkipEventDuringInput = false;
            this.numericBox1.SmartIncrement = false;
            this.numericBox1.TabIndex = 0;
            this.numericBox1.TextBoxBackColor = System.Drawing.SystemColors.Window;
            this.numericBox1.TextBoxForeColor = System.Drawing.SystemColors.WindowText;
            this.numericBox1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBox1.ThonsandsSeparator = true;
            this.numericBox1.ToolTip = "";
            this.numericBox1.UpDown_Increment = 0.1D;
            this.numericBox1.Value = 0.5D;
            this.numericBox1.WordWrap = true;
            this.numericBox1.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.ValueChanged);
            // 
            // colorControl
            // 
            this.colorControl.Argb = -986896;
            this.colorControl.AutoSize = true;
            this.colorControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.colorControl.Blue = 240;
            this.colorControl.BlueF = 0.9411765F;
            this.colorControl.Color = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colorControl.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorControl.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorControl.FooterText = "";
            this.colorControl.Green = 240;
            this.colorControl.GreenF = 0.9411765F;
            this.colorControl.Location = new System.Drawing.Point(462, 6);
            this.colorControl.Margin = new System.Windows.Forms.Padding(15, 6, 15, 0);
            this.colorControl.Name = "colorControl";
            this.colorControl.Red = 240;
            this.colorControl.RedF = 0.9411765F;
            this.colorControl.Size = new System.Drawing.Size(18, 18);
            this.colorControl.TabIndex = 2;
            this.colorControl.ToolTip = "";
            this.colorControl.ColorChanged += new Crystallography.Controls.ColorControl.MyEventHandler(this.colorControl_ColorChanged);
            // 
            // BoundsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "BoundsControl";
            this.Size = new System.Drawing.Size(554, 31);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Crystallography.Controls.NumericBox numericBoxH;
        private Crystallography.Controls.NumericBox numericBoxK;
        private Crystallography.Controls.NumericBox numericBoxL;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBoxEnable;
        private Crystallography.Controls.ColorControl colorControl;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxEquivalency;
        public Crystallography.Controls.NumericBox numericBoxDistance;
        private System.Windows.Forms.Label label3;
        public Crystallography.Controls.NumericBox numericBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}
