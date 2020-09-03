namespace Crystallography.Controls
{
    partial class NumericBoxWithMenu
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStripUpDown = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.incrementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smartIncrementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxIncrement = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuStripBody = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.decimalPlacesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxDecimalPlaces = new System.Windows.Forms.ToolStripComboBox();
            this.thousandsSeparatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemRestrictLimit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxMaximum = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxMimimum = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.allowMouseContlolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxMouseSpeed = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxMouseDirection = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuStripUpDown.SuspendLayout();
            this.contextMenuStripBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripUpDown
            // 
            this.contextMenuStripUpDown.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.contextMenuStripUpDown.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.incrementToolStripMenuItem});
            this.contextMenuStripUpDown.Name = "contextMenuStrip1";
            this.contextMenuStripUpDown.Size = new System.Drawing.Size(134, 26);
            // 
            // incrementToolStripMenuItem
            // 
            this.incrementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smartIncrementToolStripMenuItem,
            this.toolStripComboBoxIncrement});
            this.incrementToolStripMenuItem.Name = "incrementToolStripMenuItem";
            this.incrementToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.incrementToolStripMenuItem.Text = "Increment";
            this.incrementToolStripMenuItem.CheckedChanged += new System.EventHandler(this.smartIncrementToolStripMenuItem_CheckedChanged);
            // 
            // smartIncrementToolStripMenuItem
            // 
            this.smartIncrementToolStripMenuItem.Checked = true;
            this.smartIncrementToolStripMenuItem.CheckOnClick = true;
            this.smartIncrementToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smartIncrementToolStripMenuItem.Name = "smartIncrementToolStripMenuItem";
            this.smartIncrementToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.smartIncrementToolStripMenuItem.Text = "Smart increment";
            // 
            // toolStripComboBoxIncrement
            // 
            this.toolStripComboBoxIncrement.AutoCompleteCustomSource.AddRange(new string[] {
            "10E-5",
            "10E-4",
            "10E-3",
            "10E-2",
            "10E-1",
            "10E0",
            "10E+1",
            "10E+2",
            "10E+3",
            "10E+4"});
            this.toolStripComboBoxIncrement.DropDownHeight = 200;
            this.toolStripComboBoxIncrement.Enabled = false;
            this.toolStripComboBoxIncrement.IntegralHeight = false;
            this.toolStripComboBoxIncrement.Items.AddRange(new object[] {
            "10E-5",
            "10E-4",
            "10E-3",
            "10E-2",
            "10E-1",
            "10E0",
            "10E+1",
            "10E+2",
            "10E+3",
            "10E+4"});
            this.toolStripComboBoxIncrement.MaxDropDownItems = 15;
            this.toolStripComboBoxIncrement.Name = "toolStripComboBoxIncrement";
            this.toolStripComboBoxIncrement.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxIncrement.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxIncrement_SelectedIndexChanged);
            this.toolStripComboBoxIncrement.TextUpdate += new System.EventHandler(this.toolStripComboBoxIncrement_SelectedIndexChanged);
            // 
            // contextMenuStripBody
            // 
            this.contextMenuStripBody.BackColor = System.Drawing.SystemColors.Window;
            this.contextMenuStripBody.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.contextMenuStripBody.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.decimalPlacesToolStripMenuItem,
            this.thousandsSeparatorToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItemRestrictLimit,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripSeparator2,
            this.allowMouseContlolToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.contextMenuStripBody.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.contextMenuStripBody.Name = "contextMenuStripBody";
            this.contextMenuStripBody.Size = new System.Drawing.Size(185, 214);
            this.contextMenuStripBody.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuStripBody_Closing);
            this.contextMenuStripBody.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripBody_Opening);
            // 
            // decimalPlacesToolStripMenuItem
            // 
            this.decimalPlacesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxDecimalPlaces});
            this.decimalPlacesToolStripMenuItem.Name = "decimalPlacesToolStripMenuItem";
            this.decimalPlacesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.decimalPlacesToolStripMenuItem.Text = "DecimalPlaces";
            // 
            // toolStripComboBoxDecimalPlaces
            // 
            this.toolStripComboBoxDecimalPlaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxDecimalPlaces.Items.AddRange(new object[] {
            "No control",
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.toolStripComboBoxDecimalPlaces.Name = "toolStripComboBoxDecimalPlaces";
            this.toolStripComboBoxDecimalPlaces.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxDecimalPlaces.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxDecimalPlaces_SelectedIndexChanged);
            // 
            // thousandsSeparatorToolStripMenuItem
            // 
            this.thousandsSeparatorToolStripMenuItem.Checked = true;
            this.thousandsSeparatorToolStripMenuItem.CheckOnClick = true;
            this.thousandsSeparatorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.thousandsSeparatorToolStripMenuItem.Name = "thousandsSeparatorToolStripMenuItem";
            this.thousandsSeparatorToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.thousandsSeparatorToolStripMenuItem.Text = "Thousands Separator";
            this.thousandsSeparatorToolStripMenuItem.CheckedChanged += new System.EventHandler(this.thousandsSeparatorToolStripMenuItem_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // toolStripMenuItemRestrictLimit
            // 
            this.toolStripMenuItemRestrictLimit.Checked = true;
            this.toolStripMenuItemRestrictLimit.CheckOnClick = true;
            this.toolStripMenuItemRestrictLimit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemRestrictLimit.Name = "toolStripMenuItemRestrictLimit";
            this.toolStripMenuItemRestrictLimit.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItemRestrictLimit.Text = "Restrict limit";
            this.toolStripMenuItemRestrictLimit.CheckedChanged += new System.EventHandler(this.toolStripMenuItemRestrictLimit_CheckedChanged);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxMaximum});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem1.Text = "      Maximum";
            // 
            // toolStripTextBoxMaximum
            // 
            this.toolStripTextBoxMaximum.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripTextBoxMaximum.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.toolStripTextBoxMaximum.Name = "toolStripTextBoxMaximum";
            this.toolStripTextBoxMaximum.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBoxMaximum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxMaximum_KeyPress);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxMimimum});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem2.Text = "      Mimimum";
            // 
            // toolStripTextBoxMimimum
            // 
            this.toolStripTextBoxMimimum.BackColor = System.Drawing.SystemColors.Window;
            this.toolStripTextBoxMimimum.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.toolStripTextBoxMimimum.Name = "toolStripTextBoxMimimum";
            this.toolStripTextBoxMimimum.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBoxMimimum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxMaximum_KeyPress);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // allowMouseContlolToolStripMenuItem
            // 
            this.allowMouseContlolToolStripMenuItem.CheckOnClick = true;
            this.allowMouseContlolToolStripMenuItem.Name = "allowMouseContlolToolStripMenuItem";
            this.allowMouseContlolToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.allowMouseContlolToolStripMenuItem.Text = "Allow Mouse Contlol";
            this.allowMouseContlolToolStripMenuItem.CheckedChanged += new System.EventHandler(this.allowMouseContlolToolStripMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxMouseSpeed});
            this.toolStripMenuItem3.Enabled = false;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem3.Text = "      Mouse Speed";
            // 
            // toolStripTextBoxMouseSpeed
            // 
            this.toolStripTextBoxMouseSpeed.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.toolStripTextBoxMouseSpeed.Name = "toolStripTextBoxMouseSpeed";
            this.toolStripTextBoxMouseSpeed.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBoxMouseSpeed.Text = "1";
            this.toolStripTextBoxMouseSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxMaximum_KeyPress);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxMouseDirection});
            this.toolStripMenuItem4.Enabled = false;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(184, 22);
            this.toolStripMenuItem4.Text = "      Direction";
            // 
            // toolStripComboBoxMouseDirection
            // 
            this.toolStripComboBoxMouseDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxMouseDirection.Items.AddRange(new object[] {
            "Vertical",
            "Horizontal"});
            this.toolStripComboBoxMouseDirection.Name = "toolStripComboBoxMouseDirection";
            this.toolStripComboBoxMouseDirection.Size = new System.Drawing.Size(121, 23);
            // 
            // NumericBoxWithMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MaximumSize = new System.Drawing.Size(1000, 25);
            this.MinimumSize = new System.Drawing.Size(1, 25);
            this.Name = "NumericBoxWithMenu";
            this.Size = new System.Drawing.Size(70, 25);
            this.contextMenuStripUpDown.ResumeLayout(false);
            this.contextMenuStripBody.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripUpDown;
        private System.Windows.Forms.ToolStripMenuItem incrementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smartIncrementToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxIncrement;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripBody;
        private System.Windows.Forms.ToolStripMenuItem decimalPlacesToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxDecimalPlaces;
        private System.Windows.Forms.ToolStripMenuItem thousandsSeparatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRestrictLimit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxMaximum;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxMimimum;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem allowMouseContlolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxMouseSpeed;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxMouseDirection;
    }
}
