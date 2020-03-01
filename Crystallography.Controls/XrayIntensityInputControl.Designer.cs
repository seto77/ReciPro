namespace Crystallography.Controls
{
    partial class XrayIntensityInputControl
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.comboBoxLine = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.numericalTextBoxTotalCount = new Crystallography.Controls.NumericalTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.numericalTextBoxCountTime = new Crystallography.Controls.NumericalTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.label14);
            this.flowLayoutPanel2.Controls.Add(this.comboBoxLine);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(90, 28);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 8);
            this.label14.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 12);
            this.label14.TabIndex = 1;
            this.label14.Text = "Line";
            // 
            // comboBoxLine
            // 
            this.comboBoxLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLine.FormattingEnabled = true;
            this.comboBoxLine.Items.AddRange(new object[] {
            "Ka",
            "La"});
            this.comboBoxLine.Location = new System.Drawing.Point(35, 4);
            this.comboBoxLine.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxLine.Name = "comboBoxLine";
            this.comboBoxLine.Size = new System.Drawing.Size(52, 20);
            this.comboBoxLine.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 8);
            this.label15.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 12);
            this.label15.TabIndex = 1;
            this.label15.Text = "Total count";
            // 
            // numericalTextBoxTotalCount
            // 
            this.numericalTextBoxTotalCount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericalTextBoxTotalCount.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxTotalCount.DecimalPlaces = -1;
            this.numericalTextBoxTotalCount.Location = new System.Drawing.Point(69, 4);
            this.numericalTextBoxTotalCount.Margin = new System.Windows.Forms.Padding(0, 4, 1, 2);
            this.numericalTextBoxTotalCount.Multiline = false;
            this.numericalTextBoxTotalCount.Name = "numericalTextBoxTotalCount";
            this.numericalTextBoxTotalCount.RadianValue = 0D;
            this.numericalTextBoxTotalCount.ReadOnly = false;
            this.numericalTextBoxTotalCount.ShowFraction = false;
            this.numericalTextBoxTotalCount.ShowPositiveSign = false;
            this.numericalTextBoxTotalCount.Size = new System.Drawing.Size(112, 25);
            this.numericalTextBoxTotalCount.TabIndex = 8;
            this.numericalTextBoxTotalCount.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericalTextBoxTotalCount.Value = 0D;
            this.numericalTextBoxTotalCount.WordWrap = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 39);
            this.label17.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(61, 12);
            this.label17.TabIndex = 1;
            this.label17.Text = "Count time";
            // 
            // numericalTextBoxCountTime
            // 
            this.numericalTextBoxCountTime.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.numericalTextBoxCountTime.BackColor = System.Drawing.SystemColors.Control;
            this.numericalTextBoxCountTime.DecimalPlaces = -1;
            this.numericalTextBoxCountTime.Location = new System.Drawing.Point(68, 37);
            this.numericalTextBoxCountTime.Margin = new System.Windows.Forms.Padding(1, 6, 1, 2);
            this.numericalTextBoxCountTime.Multiline = false;
            this.numericalTextBoxCountTime.Name = "numericalTextBoxCountTime";
            this.numericalTextBoxCountTime.RadianValue = 0D;
            this.numericalTextBoxCountTime.ReadOnly = false;
            this.numericalTextBoxCountTime.ShowFraction = false;
            this.numericalTextBoxCountTime.ShowPositiveSign = false;
            this.numericalTextBoxCountTime.Size = new System.Drawing.Size(69, 25);
            this.numericalTextBoxCountTime.TabIndex = 8;
            this.numericalTextBoxCountTime.TextFont = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.numericalTextBoxCountTime.Value = 0D;
            this.numericalTextBoxCountTime.WordWrap = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(141, 39);
            this.label18.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(25, 12);
            this.label18.TabIndex = 1;
            this.label18.Text = "sec.";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label15);
            this.flowLayoutPanel1.Controls.Add(this.numericalTextBoxTotalCount);
            this.flowLayoutPanel1.Controls.Add(this.label17);
            this.flowLayoutPanel1.Controls.Add(this.numericalTextBoxCountTime);
            this.flowLayoutPanel1.Controls.Add(this.label18);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 31);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 67);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // XrayIntensityInputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Name = "XrayIntensityInputControl";
            this.Size = new System.Drawing.Size(203, 101);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxLine;
        private System.Windows.Forms.Label label15;
        private NumericalTextBox numericalTextBoxTotalCount;
        private System.Windows.Forms.Label label17;
        private NumericalTextBox numericalTextBoxCountTime;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
