namespace ReciPro
{
    partial class InputBox
    {
        /// <summary>必要なデザイナー変数です。</summary>
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputBox)); // 260531Cl
            components = new System.ComponentModel.Container(); // 260531Cl
            toolTip = new System.Windows.Forms.ToolTip(components); // 260531Cl
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            toolTip.AutoPopDelay = 10000; // 260601Cl 追加: 長文表示時間を延長(共通標準値)
            toolTip.InitialDelay = 500; // 260601Cl 追加
            toolTip.ReshowDelay = 100; // 260601Cl 追加
            label64 = new System.Windows.Forms.Label();
            label60 = new System.Windows.Forms.Label();
            label50 = new System.Windows.Forms.Label();
            numericBoxLength = new Crystallography.Controls.NumericBox();
            numericBoxGlength = new Crystallography.Controls.NumericBox();
            numericBoxDvalue = new Crystallography.Controls.NumericBox();
            SuspendLayout();
            // 
            // label64
            // 
            label64.AutoSize = true;
            label64.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label64.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label64.Location = new System.Drawing.Point(203, 5);
            label64.Margin = new System.Windows.Forms.Padding(0);
            label64.Name = "label64";
            toolTip.SetToolTip(label64, resources.GetString("label64.ToolTip")); // 260531Cl
            label64.Size = new System.Drawing.Size(33, 15);
            label64.TabIndex = 73;
            label64.Text = "nm⁻¹";
            label64.Click += new System.EventHandler(label50_Click);
            // 
            // label60
            // 
            label60.AutoSize = true;
            label60.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label60.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            label60.Location = new System.Drawing.Point(132, 6);
            label60.Margin = new System.Windows.Forms.Padding(0);
            label60.Name = "label60";
            toolTip.SetToolTip(label60, resources.GetString("label60.ToolTip")); // 260531Cl
            label60.Size = new System.Drawing.Size(15, 15);
            label60.TabIndex = 74;
            label60.Text = "Å";
            label60.Click += new System.EventHandler(label50_Click);
            // 
            // label50
            // 
            label50.AutoSize = true;
            label50.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label50.Location = new System.Drawing.Point(48, 4);
            label50.Name = "label50";
            toolTip.SetToolTip(label50, resources.GetString("label50.ToolTip")); // 260531Cl
            label50.Size = new System.Drawing.Size(29, 15);
            label50.TabIndex = 71;
            label50.Text = "mm";
            label50.Click += new System.EventHandler(label50_Click);
            // 
            // numericBoxLength
            // 
            numericBoxLength.BackColor = System.Drawing.SystemColors.Control;
            numericBoxLength.Location = new System.Drawing.Point(0, 0);
            numericBoxLength.Margin = new System.Windows.Forms.Padding(1);
            numericBoxLength.MaximumSize = new System.Drawing.Size(1000, 27);
            numericBoxLength.MinimumSize = new System.Drawing.Size(1, 25);
            numericBoxLength.Name = "numericBoxLength";
            toolTip.SetToolTip(numericBoxLength, resources.GetString("numericBoxLength.ToolTip")); // 260531Cl
            numericBoxLength.Padding = new System.Windows.Forms.Padding(1);
            numericBoxLength.Size = new System.Drawing.Size(50, 27);
            numericBoxLength.TabIndex = 75;
            // 260522Cl: numericBox の ValueFont(Yu Gothic UI) ハードコードを撤去
            numericBoxLength.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(numericBoxLength_ValueChanged);
            numericBoxLength.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(numericBoxlength_Click);
            // 
            // numericBoxGlength
            // 
            numericBoxGlength.BackColor = System.Drawing.SystemColors.Control;
            numericBoxGlength.Location = new System.Drawing.Point(154, 0);
            numericBoxGlength.Margin = new System.Windows.Forms.Padding(1);
            numericBoxGlength.MaximumSize = new System.Drawing.Size(1000, 27);
            numericBoxGlength.MinimumSize = new System.Drawing.Size(1, 25);
            numericBoxGlength.Name = "numericBoxGlength";
            toolTip.SetToolTip(numericBoxGlength, resources.GetString("numericBoxGlength.ToolTip")); // 260531Cl
            numericBoxGlength.Padding = new System.Windows.Forms.Padding(1);
            numericBoxGlength.Size = new System.Drawing.Size(50, 27);
            numericBoxGlength.TabIndex = 75;
            numericBoxGlength.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(numericBoxGlength_ValueChanged);
            numericBoxGlength.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(numericBoxGlength_Click);
            // 
            // numericBoxDvalue
            // 
            numericBoxDvalue.BackColor = System.Drawing.SystemColors.Control;
            numericBoxDvalue.Location = new System.Drawing.Point(82, 0);
            numericBoxDvalue.Margin = new System.Windows.Forms.Padding(1);
            numericBoxDvalue.MaximumSize = new System.Drawing.Size(1000, 27);
            numericBoxDvalue.MinimumSize = new System.Drawing.Size(1, 25);
            numericBoxDvalue.Name = "numericBoxDvalue";
            toolTip.SetToolTip(numericBoxDvalue, resources.GetString("numericBoxDvalue.ToolTip")); // 260531Cl
            numericBoxDvalue.Padding = new System.Windows.Forms.Padding(1);
            numericBoxDvalue.Size = new System.Drawing.Size(50, 27);
            numericBoxDvalue.TabIndex = 75;
            numericBoxDvalue.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(numericBoxDvalue_ValueChanged);
            numericBoxDvalue.Click2 += new Crystallography.Controls.NumericBox.MyEventHandler(numericBoxDvalue_Click);
            // 
            // InputBox
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Controls.Add(numericBoxGlength);
            Controls.Add(numericBoxDvalue);
            Controls.Add(numericBoxLength);
            Controls.Add(label64);
            Controls.Add(label60);
            Controls.Add(label50);
            Name = "InputBox";
            Size = new System.Drawing.Size(236, 28);
            Click += new System.EventHandler(InputBox_Click);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip; // 260531Cl
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label50;
        private Crystallography.Controls.NumericBox numericBoxLength;
        private NumericBox numericBoxGlength;
        private NumericBox numericBoxDvalue;
    }
}
