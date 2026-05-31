namespace Crystallography.Controls
{
    partial class SizeControl
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
            components = new System.ComponentModel.Container(); // 260521Cl 追加: ToolTip 用
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SizeControl));
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            numericBoxHeight = new NumericBox();
            numericBoxWidth = new NumericBox();
            label1 = new System.Windows.Forms.Label();
            labelHeader = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            checkBoxKeepAspect = new System.Windows.Forms.CheckBox(); // 260521Cl 追加
            toolTip = new System.Windows.Forms.ToolTip(components); // 260521Cl 追加
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(numericBoxHeight, 3, 0);
            tableLayoutPanel1.Controls.Add(numericBoxWidth, 1, 0);
            tableLayoutPanel1.Controls.Add(label1, 2, 0);
            tableLayoutPanel1.Controls.Add(labelHeader, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 4, 0);
            tableLayoutPanel1.Controls.Add(checkBoxKeepAspect, 5, 0); // 260521Cl 追加
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // numericBoxHeight
            // 
            numericBoxHeight.BackColor = System.Drawing.Color.Transparent;
            numericBoxHeight.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxHeight, "numericBoxHeight");
            toolTip.SetToolTip(numericBoxHeight, resources.GetString("numericBoxHeight.ToolTip")); // 260531Cl
            numericBoxHeight.Maximum = 9999D;
            numericBoxHeight.Minimum = 1D;
            numericBoxHeight.Name = "numericBoxHeight";
            numericBoxHeight.RadianValue = 0.017453292519943295D;
            numericBoxHeight.ShowUpDown = true;
            numericBoxHeight.SmartIncrement = true;
            numericBoxHeight.ThousandsSeparator = true;
            numericBoxHeight.Value = 1D;
            numericBoxHeight.ValueChanged += numericBoxValueChanged;
            // 
            // numericBoxWidth
            // 
            numericBoxWidth.BackColor = System.Drawing.Color.Transparent;
            numericBoxWidth.DecimalPlaces = 0;
            resources.ApplyResources(numericBoxWidth, "numericBoxWidth");
            toolTip.SetToolTip(numericBoxWidth, resources.GetString("numericBoxWidth.ToolTip")); // 260531Cl
            numericBoxWidth.Maximum = 9999D;
            numericBoxWidth.Minimum = 1D;
            numericBoxWidth.Name = "numericBoxWidth";
            numericBoxWidth.RadianValue = 0.017453292519943295D;
            numericBoxWidth.ShowUpDown = true;
            numericBoxWidth.SmartIncrement = true;
            numericBoxWidth.ThousandsSeparator = true;
            numericBoxWidth.Value = 1D;
            numericBoxWidth.ValueChanged += numericBoxValueChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            toolTip.SetToolTip(label1, resources.GetString("label1.ToolTip")); // 260531Cl
            label1.Name = "label1";
            // 
            // labelHeader
            // 
            resources.ApplyResources(labelHeader, "labelHeader");
            toolTip.SetToolTip(labelHeader, resources.GetString("labelHeader.ToolTip")); // 260531Cl
            labelHeader.Name = "labelHeader";
            //
            // label2
            //
            resources.ApplyResources(label2, "label2");
            toolTip.SetToolTip(label2, resources.GetString("label2.ToolTip")); // 260531Cl
            label2.Name = "label2";
            //
            // checkBoxKeepAspect  260521Cl 追加
            //
            resources.ApplyResources(checkBoxKeepAspect, "checkBoxKeepAspect");
            toolTip.SetToolTip(checkBoxKeepAspect, resources.GetString("checkBoxKeepAspect.ToolTip")); // 260531Cl
            checkBoxKeepAspect.Name = "checkBoxKeepAspect";
            checkBoxKeepAspect.UseVisualStyleBackColor = true;
            checkBoxKeepAspect.CheckedChanged += checkBoxKeepAspect_CheckedChanged;
            //
            // SizeControl
            //
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi; // 260529Cl: DPI 追従 (resx の $this.AutoScaleDimensions は 96,96)
            Controls.Add(tableLayoutPanel1);
            Name = "SizeControl";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private NumericBox numericBoxHeight;
        private NumericBox numericBoxWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxKeepAspect; // 260521Cl 追加
        private System.Windows.Forms.ToolTip toolTip; // 260521Cl 追加
    }
}
