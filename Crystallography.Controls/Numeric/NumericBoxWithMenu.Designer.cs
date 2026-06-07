namespace Crystallography.Controls
{
    partial class NumericBoxWithMenu
    {
        /// <summary>必要なデザイナー変数です。</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>使用中のリソースをすべてクリーンアップします。</summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NumericBoxWithMenu)); // 260531Cl
            components = new System.ComponentModel.Container();
            contextMenuStripUpDown = new System.Windows.Forms.ContextMenuStrip(components);
            incrementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            smartIncrementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripComboBoxIncrement = new System.Windows.Forms.ToolStripComboBox();
            contextMenuStripBody = new System.Windows.Forms.ContextMenuStrip(components);
            decimalPlacesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripComboBoxDecimalPlaces = new System.Windows.Forms.ToolStripComboBox();
            thousandsSeparatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripMenuItemRestrictLimit = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripTextBoxMaximum = new System.Windows.Forms.ToolStripTextBox();
            toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripTextBoxMimimum = new System.Windows.Forms.ToolStripTextBox();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            allowMouseContlolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripTextBoxMouseSpeed = new System.Windows.Forms.ToolStripTextBox();
            toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripComboBoxMouseDirection = new System.Windows.Forms.ToolStripComboBox();
            contextMenuStripUpDown.SuspendLayout();
            contextMenuStripBody.SuspendLayout();
            SuspendLayout();
            // 
            // contextMenuStripUpDown
            // 
            contextMenuStripUpDown.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            contextMenuStripUpDown.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            incrementToolStripMenuItem});
            contextMenuStripUpDown.Name = "contextMenuStrip1";
            contextMenuStripUpDown.Size = new System.Drawing.Size(134, 26);
            // 
            // incrementToolStripMenuItem
            // 
            incrementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            smartIncrementToolStripMenuItem,
            toolStripComboBoxIncrement});
            incrementToolStripMenuItem.Name = "incrementToolStripMenuItem";
            incrementToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            incrementToolStripMenuItem.Text = "Increment";
            incrementToolStripMenuItem.CheckedChanged += new System.EventHandler(smartIncrementToolStripMenuItem_CheckedChanged);
            // 
            // smartIncrementToolStripMenuItem
            // 
            smartIncrementToolStripMenuItem.Checked = true;
            smartIncrementToolStripMenuItem.CheckOnClick = true;
            smartIncrementToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            smartIncrementToolStripMenuItem.Name = "smartIncrementToolStripMenuItem";
            smartIncrementToolStripMenuItem.ToolTipText = resources.GetString("smartIncrementToolStripMenuItem.ToolTipText"); // 260531Cl
            smartIncrementToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            smartIncrementToolStripMenuItem.Text = "Smart increment";
            // 
            // toolStripComboBoxIncrement
            // 
            toolStripComboBoxIncrement.AutoCompleteCustomSource.AddRange(new string[] {
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
            toolStripComboBoxIncrement.DropDownHeight = 200;
            toolStripComboBoxIncrement.Enabled = false;
            toolStripComboBoxIncrement.IntegralHeight = false;
            toolStripComboBoxIncrement.Items.AddRange(new object[] {
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
            toolStripComboBoxIncrement.MaxDropDownItems = 15;
            toolStripComboBoxIncrement.Name = "toolStripComboBoxIncrement";
            toolStripComboBoxIncrement.ToolTipText = resources.GetString("toolStripComboBoxIncrement.ToolTipText"); // 260531Cl
            toolStripComboBoxIncrement.Size = new System.Drawing.Size(121, 23);
            toolStripComboBoxIncrement.SelectedIndexChanged += new System.EventHandler(toolStripComboBoxIncrement_SelectedIndexChanged);
            toolStripComboBoxIncrement.TextUpdate += new System.EventHandler(toolStripComboBoxIncrement_SelectedIndexChanged);
            // 
            // contextMenuStripBody
            // 
            contextMenuStripBody.BackColor = System.Drawing.SystemColors.Window;
            contextMenuStripBody.ImeMode = System.Windows.Forms.ImeMode.Disable;
            contextMenuStripBody.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            decimalPlacesToolStripMenuItem,
            thousandsSeparatorToolStripMenuItem,
            toolStripSeparator1,
            toolStripMenuItemRestrictLimit,
            toolStripMenuItem1,
            toolStripMenuItem2,
            toolStripSeparator2,
            allowMouseContlolToolStripMenuItem,
            toolStripMenuItem3,
            toolStripMenuItem4});
            contextMenuStripBody.Margin = new System.Windows.Forms.Padding(20, 0, 0, 0);
            contextMenuStripBody.Name = "contextMenuStripBody";
            contextMenuStripBody.Size = new System.Drawing.Size(185, 214);
            contextMenuStripBody.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(contextMenuStripBody_Closing);
            contextMenuStripBody.Opening += new System.ComponentModel.CancelEventHandler(contextMenuStripBody_Opening);
            // 
            // decimalPlacesToolStripMenuItem
            // 
            decimalPlacesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripComboBoxDecimalPlaces});
            decimalPlacesToolStripMenuItem.Name = "decimalPlacesToolStripMenuItem";
            decimalPlacesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            decimalPlacesToolStripMenuItem.Text = "DecimalPlaces";
            // 
            // toolStripComboBoxDecimalPlaces
            // 
            toolStripComboBoxDecimalPlaces.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxDecimalPlaces.Items.AddRange(new object[] {
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
            toolStripComboBoxDecimalPlaces.Name = "toolStripComboBoxDecimalPlaces";
            toolStripComboBoxDecimalPlaces.ToolTipText = resources.GetString("toolStripComboBoxDecimalPlaces.ToolTipText"); // 260531Cl
            toolStripComboBoxDecimalPlaces.Size = new System.Drawing.Size(121, 23);
            toolStripComboBoxDecimalPlaces.SelectedIndexChanged += new System.EventHandler(toolStripComboBoxDecimalPlaces_SelectedIndexChanged);
            // 
            // thousandsSeparatorToolStripMenuItem
            // 
            thousandsSeparatorToolStripMenuItem.Checked = true;
            thousandsSeparatorToolStripMenuItem.CheckOnClick = true;
            thousandsSeparatorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            thousandsSeparatorToolStripMenuItem.Name = "thousandsSeparatorToolStripMenuItem";
            thousandsSeparatorToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            thousandsSeparatorToolStripMenuItem.Text = "Thousands Separator";
            thousandsSeparatorToolStripMenuItem.CheckedChanged += new System.EventHandler(thousandsSeparatorToolStripMenuItem_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // toolStripMenuItemRestrictLimit
            // 
            toolStripMenuItemRestrictLimit.Checked = true;
            toolStripMenuItemRestrictLimit.CheckOnClick = true;
            toolStripMenuItemRestrictLimit.CheckState = System.Windows.Forms.CheckState.Checked;
            toolStripMenuItemRestrictLimit.Name = "toolStripMenuItemRestrictLimit";
            toolStripMenuItemRestrictLimit.ToolTipText = resources.GetString("toolStripMenuItemRestrictLimit.ToolTipText"); // 260531Cl
            toolStripMenuItemRestrictLimit.Size = new System.Drawing.Size(184, 22);
            toolStripMenuItemRestrictLimit.Text = "Restrict limit";
            toolStripMenuItemRestrictLimit.CheckedChanged += new System.EventHandler(toolStripMenuItemRestrictLimit_CheckedChanged);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripTextBoxMaximum});
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
            toolStripMenuItem1.Text = "      Maximum";
            // 
            // toolStripTextBoxMaximum
            // 
            toolStripTextBoxMaximum.BackColor = System.Drawing.SystemColors.Window;
            toolStripTextBoxMaximum.Font = new System.Drawing.Font("Segoe UI", 9F);
            toolStripTextBoxMaximum.Name = "toolStripTextBoxMaximum";
            toolStripTextBoxMaximum.ToolTipText = resources.GetString("toolStripTextBoxMaximum.ToolTipText"); // 260531Cl
            toolStripTextBoxMaximum.Size = new System.Drawing.Size(100, 23);
            toolStripTextBoxMaximum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxMaximum_KeyPress);
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripTextBoxMimimum});
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new System.Drawing.Size(184, 22);
            toolStripMenuItem2.Text = "      Mimimum";
            // 
            // toolStripTextBoxMimimum
            // 
            toolStripTextBoxMimimum.BackColor = System.Drawing.SystemColors.Window;
            toolStripTextBoxMimimum.Font = new System.Drawing.Font("Segoe UI", 9F);
            toolStripTextBoxMimimum.Name = "toolStripTextBoxMimimum";
            toolStripTextBoxMimimum.ToolTipText = resources.GetString("toolStripTextBoxMimimum.ToolTipText"); // 260531Cl
            toolStripTextBoxMimimum.Size = new System.Drawing.Size(100, 23);
            toolStripTextBoxMimimum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxMaximum_KeyPress);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
            // 
            // allowMouseContlolToolStripMenuItem
            // 
            allowMouseContlolToolStripMenuItem.CheckOnClick = true;
            allowMouseContlolToolStripMenuItem.Name = "allowMouseContlolToolStripMenuItem";
            allowMouseContlolToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            allowMouseContlolToolStripMenuItem.Text = "Allow Mouse Contlol";
            allowMouseContlolToolStripMenuItem.CheckedChanged += new System.EventHandler(allowMouseContlolToolStripMenuItem_CheckedChanged);
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripTextBoxMouseSpeed});
            toolStripMenuItem3.Enabled = false;
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new System.Drawing.Size(184, 22);
            toolStripMenuItem3.Text = "      Mouse Speed";
            // 
            // toolStripTextBoxMouseSpeed
            // 
            toolStripTextBoxMouseSpeed.Font = new System.Drawing.Font("Segoe UI", 9F);
            toolStripTextBoxMouseSpeed.Name = "toolStripTextBoxMouseSpeed";
            toolStripTextBoxMouseSpeed.ToolTipText = resources.GetString("toolStripTextBoxMouseSpeed.ToolTipText"); // 260531Cl
            toolStripTextBoxMouseSpeed.Size = new System.Drawing.Size(100, 23);
            toolStripTextBoxMouseSpeed.Text = "1";
            toolStripTextBoxMouseSpeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxMaximum_KeyPress);
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripComboBoxMouseDirection});
            toolStripMenuItem4.Enabled = false;
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new System.Drawing.Size(184, 22);
            toolStripMenuItem4.Text = "      Direction";
            // 
            // toolStripComboBoxMouseDirection
            // 
            toolStripComboBoxMouseDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxMouseDirection.Items.AddRange(new object[] {
            "Vertical",
            "Horizontal"});
            toolStripComboBoxMouseDirection.Name = "toolStripComboBoxMouseDirection";
            toolStripComboBoxMouseDirection.ToolTipText = resources.GetString("toolStripComboBoxMouseDirection.ToolTipText"); // 260531Cl
            toolStripComboBoxMouseDirection.Size = new System.Drawing.Size(121, 23);
            // 
            // NumericBoxWithMenu
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            MaximumSize = new System.Drawing.Size(1000, 25);
            MinimumSize = new System.Drawing.Size(1, 25);
            Name = "NumericBoxWithMenu";
            Size = new System.Drawing.Size(70, 25);
            contextMenuStripUpDown.ResumeLayout(false);
            contextMenuStripBody.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

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
