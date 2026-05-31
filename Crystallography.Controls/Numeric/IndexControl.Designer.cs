namespace Crystallography.Controls
{
    partial class IndexControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndexControl)); // 260531Cl
            components = new System.ComponentModel.Container(); // (260531Ch)
            toolTip = new System.Windows.Forms.ToolTip(components); // (260531Ch)
            toolTip.IsBalloon = true; // 260531Cl 追加: バルーン表示に統一
            numericBoxH = new NumericBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            numericBoxL = new NumericBox();
            labelLaTexPM4 = new LabelLaTeX();
            labelLaTexZ = new LabelLaTeX();
            numericBoxI = new NumericBox();
            labelLaTexW = new LabelLaTeX();
            labelLaTexY = new LabelLaTeX();
            numericBoxK = new NumericBox();
            labelLaTexPM2 = new LabelLaTeX();
            labelLaTexX = new LabelLaTeX();
            labelLaTexPM1 = new LabelLaTeX();
            labelLaTexStart = new LabelLaTeX();
            labelLaTexEnd = new LabelLaTeX();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // numericBoxH
            // 
            numericBoxH.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxH.BackColor = System.Drawing.Color.Transparent;
            numericBoxH.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxH.FooterPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxH.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            numericBoxH.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxH.Location = new System.Drawing.Point(17, 16);
            numericBoxH.Margin = new System.Windows.Forms.Padding(0);
            numericBoxH.Maximum = 20D;
            numericBoxH.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxH.Minimum = 0D;
            numericBoxH.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxH.Name = "numericBoxH";
            toolTip.SetToolTip(numericBoxH, resources.GetString("numericBoxH.ToolTip")); // 260531Cl
            numericBoxH.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxH.ShowUpDown = true;
            numericBoxH.Size = new System.Drawing.Size(38, 25);
            numericBoxH.SkipEventDuringInput = false;
            numericBoxH.TabIndex = 80;
            numericBoxH.ValueFontSize = 9F;
            numericBoxH.ThousandsSeparator = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 9;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(numericBoxL, 7, 1);
            tableLayoutPanel1.Controls.Add(labelLaTexPM4, 6, 1);
            tableLayoutPanel1.Controls.Add(labelLaTexZ, 7, 0);
            tableLayoutPanel1.Controls.Add(numericBoxI, 5, 1);
            tableLayoutPanel1.Controls.Add(labelLaTexW, 5, 0);
            tableLayoutPanel1.Controls.Add(labelLaTexY, 4, 0);
            tableLayoutPanel1.Controls.Add(numericBoxK, 4, 1);
            tableLayoutPanel1.Controls.Add(labelLaTexPM2, 3, 1);
            tableLayoutPanel1.Controls.Add(labelLaTexX, 2, 0);
            tableLayoutPanel1.Controls.Add(numericBoxH, 2, 1);
            tableLayoutPanel1.Controls.Add(labelLaTexPM1, 1, 1);
            tableLayoutPanel1.Controls.Add(labelLaTexStart, 0, 1);
            tableLayoutPanel1.Controls.Add(labelLaTexEnd, 8, 1);
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.Size = new System.Drawing.Size(180, 41);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // numericBoxL
            // 
            numericBoxL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxL.BackColor = System.Drawing.Color.Transparent;
            numericBoxL.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxL.FooterPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxL.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            numericBoxL.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxL.Location = new System.Drawing.Point(135, 16);
            numericBoxL.Margin = new System.Windows.Forms.Padding(0);
            numericBoxL.Maximum = 20D;
            numericBoxL.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxL.Minimum = 0D;
            numericBoxL.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxL.Name = "numericBoxL";
            toolTip.SetToolTip(numericBoxL, resources.GetString("numericBoxL.ToolTip")); // 260531Cl
            numericBoxL.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxL.ShowUpDown = true;
            numericBoxL.Size = new System.Drawing.Size(38, 25);
            numericBoxL.SkipEventDuringInput = false;
            numericBoxL.TabIndex = 80;
            numericBoxL.ValueFontSize = 9F;
            numericBoxL.ThousandsSeparator = true;
            // 
            // labelLaTexPM4
            // 
            labelLaTexPM4.Location = new System.Drawing.Point(125, 16);
            labelLaTexPM4.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexPM4.Name = "labelLaTexPM4";
            labelLaTexPM4.Size = new System.Drawing.Size(10, 20);
            labelLaTexPM4.TabIndex = 81;
            labelLaTexPM4.Text = "\\pm";
            labelLaTexPM4.Thickness = 0.7D;
            // 
            // labelLaTexZ
            // 
            labelLaTexZ.Location = new System.Drawing.Point(135, 0);
            labelLaTexZ.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexZ.Name = "labelLaTexZ";
            labelLaTexZ.Size = new System.Drawing.Size(16, 16);
            labelLaTexZ.TabIndex = 81;
            labelLaTexZ.Text = "l";
            labelLaTexZ.Thickness = 0.7D;
            // 
            // numericBoxI
            // 
            numericBoxI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxI.BackColor = System.Drawing.Color.Transparent;
            numericBoxI.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxI.FooterPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxI.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            numericBoxI.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxI.Location = new System.Drawing.Point(103, 16);
            numericBoxI.Margin = new System.Windows.Forms.Padding(0);
            numericBoxI.Maximum = 20D;
            numericBoxI.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxI.Minimum = 0D;
            numericBoxI.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxI.Name = "numericBoxI";
            toolTip.SetToolTip(numericBoxI, resources.GetString("numericBoxI.ToolTip")); // 260531Cl
            numericBoxI.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxI.ReadOnly = true;
            numericBoxI.Size = new System.Drawing.Size(22, 25);
            numericBoxI.SkipEventDuringInput = false;
            numericBoxI.TabIndex = 80;
            numericBoxI.ValueBackColor = System.Drawing.SystemColors.Control;
            numericBoxI.ValueFontSize = 9F;
            numericBoxI.ThousandsSeparator = true;
            // 
            // labelLaTexW
            // 
            labelLaTexW.Location = new System.Drawing.Point(103, 0);
            labelLaTexW.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexW.Name = "labelLaTexW";
            labelLaTexW.Size = new System.Drawing.Size(16, 16);
            labelLaTexW.TabIndex = 81;
            labelLaTexW.Text = "i";
            labelLaTexW.Thickness = 0.7D;
            // 
            // labelLaTexY
            // 
            labelLaTexY.Location = new System.Drawing.Point(65, 0);
            labelLaTexY.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexY.Name = "labelLaTexY";
            labelLaTexY.Size = new System.Drawing.Size(16, 16);
            labelLaTexY.TabIndex = 81;
            labelLaTexY.Text = "k";
            labelLaTexY.Thickness = 0.7D;
            // 
            // numericBoxK
            // 
            numericBoxK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            numericBoxK.BackColor = System.Drawing.Color.Transparent;
            numericBoxK.FooterFont = new System.Drawing.Font("Segoe UI", 9.75F);
            numericBoxK.FooterPadding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            numericBoxK.HeaderFont = new System.Drawing.Font("Segoe UI", 9F);
            numericBoxK.HeaderPadding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            numericBoxK.Location = new System.Drawing.Point(65, 16);
            numericBoxK.Margin = new System.Windows.Forms.Padding(0);
            numericBoxK.Maximum = 20D;
            numericBoxK.MaximumSize = new System.Drawing.Size(1000, 30);
            numericBoxK.Minimum = 0D;
            numericBoxK.MinimumSize = new System.Drawing.Size(1, 20);
            numericBoxK.Name = "numericBoxK";
            toolTip.SetToolTip(numericBoxK, resources.GetString("numericBoxK.ToolTip")); // 260531Cl
            numericBoxK.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            numericBoxK.ShowUpDown = true;
            numericBoxK.Size = new System.Drawing.Size(38, 25);
            numericBoxK.SkipEventDuringInput = false;
            numericBoxK.TabIndex = 80;
            numericBoxK.ValueFontSize = 9F;
            numericBoxK.ThousandsSeparator = true;
            // 
            // labelLaTexPM2
            // 
            labelLaTexPM2.Location = new System.Drawing.Point(55, 16);
            labelLaTexPM2.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexPM2.Name = "labelLaTexPM2";
            labelLaTexPM2.Size = new System.Drawing.Size(10, 20);
            labelLaTexPM2.TabIndex = 81;
            labelLaTexPM2.Text = "\\pm";
            labelLaTexPM2.Thickness = 0.7D;
            // 
            // labelLaTexX
            // 
            labelLaTexX.Location = new System.Drawing.Point(17, 0);
            labelLaTexX.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexX.Name = "labelLaTexX";
            labelLaTexX.Size = new System.Drawing.Size(16, 16);
            labelLaTexX.TabIndex = 81;
            labelLaTexX.Text = "h";
            labelLaTexX.Thickness = 0.7D;
            // 
            // labelLaTexPM1
            // 
            labelLaTexPM1.Location = new System.Drawing.Point(7, 16);
            labelLaTexPM1.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexPM1.Name = "labelLaTexPM1";
            labelLaTexPM1.Size = new System.Drawing.Size(10, 20);
            labelLaTexPM1.TabIndex = 81;
            labelLaTexPM1.Text = "\\pm";
            labelLaTexPM1.Thickness = 0.7D;
            // 
            // labelLaTexStart
            // 
            labelLaTexStart.Location = new System.Drawing.Point(0, 16);
            labelLaTexStart.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexStart.Name = "labelLaTexStart";
            labelLaTexStart.Size = new System.Drawing.Size(7, 24);
            labelLaTexStart.TabIndex = 81;
            labelLaTexStart.Text = "(";
            labelLaTexStart.Thickness = 0.7D;
            // 
            // labelLaTexEnd
            // 
            labelLaTexEnd.Location = new System.Drawing.Point(173, 16);
            labelLaTexEnd.Margin = new System.Windows.Forms.Padding(0);
            labelLaTexEnd.Name = "labelLaTexEnd";
            labelLaTexEnd.Size = new System.Drawing.Size(7, 24);
            labelLaTexEnd.TabIndex = 81;
            labelLaTexEnd.Text = ")";
            labelLaTexEnd.Thickness = 0.7D;
            // 
            // IndexControl
            // 
            // AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F); // 260529Cl 旧: Font モード時の値。Dpi では 96/7 倍に拡大されてしまう
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F); // 260529Cl
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi; // 260529Cl: DPI 追従
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Controls.Add(tableLayoutPanel1);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "IndexControl";
            Size = new System.Drawing.Size(180, 41);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip; // (260531Ch)
        private NumericBox numericBoxH;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private LabelLaTeX labelLaTexX;
        private LabelLaTeX labelLaTexPM1;
        private LabelLaTeX labelLaTexPM2;
        private LabelLaTeX labelLaTexY;
        private NumericBox numericBoxK;
        private NumericBox numericBoxI;
        private NumericBox numericBoxL;
        private LabelLaTeX labelLaTexW;
        private LabelLaTeX labelLaTexZ;
        private LabelLaTeX labelLaTexPM4;
        private LabelLaTeX labelLaTexStart;
        private LabelLaTeX labelLaTexEnd;
    }
}
